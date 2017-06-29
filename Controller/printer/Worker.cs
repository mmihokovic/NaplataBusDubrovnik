using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Controller.tickets;
using Model;
using Database;

namespace Controller.printer
{
    public class Worker
    {
        volatile bool magReaderEnable;

        volatile bool printerError;

        volatile bool selfDestruct;

        volatile bool reportError;

        Queue<IPrintObject> queue;

        private IPrinter printer;

        private int keepAliveCounter = 0;


        public Worker(IPrinter printer)
        {
            queue = new Queue<IPrintObject>();
            this.printer = printer;
            this.printerError = false;
            this.selfDestruct = false;
            this.reportError = true;
        }

        public void Print(IPrintObject printObject)
        {
            lock (queue)
            {
                queue.Enqueue(printObject);
            }
        }

        public bool PrintError
        {
            get { return this.printerError; }
        }

        public bool printDone
        {
            get
            {
                lock (this.queue)
                {
                    if (this.queue.Count > 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
        }

        public void Work()
        {
            try
            {
                while (true)
                {
                    lock (queue)
                    {
                        //printaj
                        while (queue.Count > 0)
                        {
                            //pogle prvog
                            IPrintObject printObject = queue.Peek();
                            bool error = printObject.Print();

                            //ako FAILa digni zastavicu greske i nemoj cupati iz reda
                            if (error)
                            {
                                printerError = true;
                                break;
                            }
                            //ako je uspijelo ukloni iz reda 
                            else
                            {
                                queue.Dequeue();
                                if (printerError)
                                {
                                    printerError = false;
                                }
                            }
                        }
                    }

                    //provijeri da li je pisac uspio isprintat
                    if (printerError)
                    {
                        printerErrorHandler();
                        Thread.Sleep(100);
                    }
                    Thread.Sleep(250);

                    if (keepAliveCounter < 50)
                    {
                        keepAliveCounter++;
                    }
                    else
                    {
                        keepAliveCounter = 0;
                        //printer.KeepAlive();
                        Controller.ChargeRegularUserController.HasInternet();
                    }

                    //samounistenje
                    if (selfDestruct)
                    {
                        //Printer.Close();
                        break;
                    }
                }
            }
            catch (ThreadAbortException ex)
            {
                Logger.Logger.Log(ex);
                //TODO: not implemented
            }
            finally
            {

            }
        }

        private void printerErrorHandler()
        {
            if (reportError)
            {
                reportError = false;
            }
             printer.Reconnect();
        }

        public void Exit()
        {
        }

        public bool SelfDestruct
        {
            get { return selfDestruct; }
            set { selfDestruct = value; }
        }
      

        private void ReadMagCard()
        {
            String[] data = printer.ReadMagCard(1500);
            if (data != null)
            {
                this.printerError = false;
            }

            if (data == null)
            {
                printerError = true;
            }
        }

        public bool MagReaderEnable
        {
            get { return magReaderEnable; }
            set { magReaderEnable = value; }
        }

        public bool ReportError
        {
            get { return reportError; }
            set { reportError = value; }
        }
    }
}
