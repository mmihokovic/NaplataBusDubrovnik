using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

using ZSDK_API.Discovery;
using ZSDK_API.Comm;
using ZebraBluetoothAdapter;
using System.Threading;
using System.Diagnostics;
using ZSDK_API.ApiException;
using System.IO.Ports;
using ZSDK_API.Printer;
using System.Media;


namespace Controller.printer
{
    public class Printer : IPrinter
    {
        private ZebraPrinterConnection ThePrinterConn;
        private List<String> buffer;
        private int lastYPos;
        private ZebraPrinter printer;
        private MagCardReader mcr;

        public Printer()
        {
            // com port

            // Conect to the printer using Bluetooth to serial port (in PDA) and serial connection in SDK 
            ThePrinterConn = ThePrinterConn = new BluetoothPrinterConnection("0022583bc091");


            //ostale inicijalizacije...
            buffer = new List<String>();
            lastYPos = 0;
        }

        public void AddTextLine(String line, int font, int size, int posx, int posy)
        {
            String modifiedLine = "TEXT " + font + " " + size + " " + posx + " " + posy + " " + line;
            buffer.Add(PalataliParser.Parser(modifiedLine));
            lastYPos = posy;
        }


        public void AddHorizontalLine(int posx, int posy)
        {
            String line = "TEXT 5 0 " + posx + " " + posy + " " + "___________________________________";
            buffer.Add(line);
            lastYPos = posy;
        }

        public void AddBarCode(string data, int posy)
        {
            String line = "BARCODE 128 1 1 100 40 " + posy + " " + data;//"BARCODE 128 1 2 50 40" + posy + " " + data;
            buffer.Add(line);
            line = "TEXT 7 0 100 " + (posy + 100).ToString() + data;
            buffer.Add(line);
            lastYPos = posy + 100;
        }

        public void SetJustification(String justification)
        {
            String line = null;
            if (justification.ToLower() == "left")
            {
                line = "LEFT";
            }
            else if (justification.ToLower() == "center")
            {
                line = "CENTER";
            }
            else if (justification.ToLower() == "right")
            {
                line = "RIGHT";
            }
            else
            {
                throw new ArgumentException();
            }
            buffer.Add(line);
        }

        private void clearBuffer()
        {
            buffer.Clear();
        }

        public void Close()
        {
            // Make sure the data got to the printer before closing the connection
            Thread.Sleep(100);

            // Close the connection to release resources.
            ThePrinterConn.Close();
            ThePrinterConn = null;
        }

        public void Reconnect()
        {
            if (ThePrinterConn != null)
            {
                ThePrinterConn.Close();
            }
            //thePrinterConn = new SerialPrinterConnection(comPort, 19200, 8, Parity.None, StopBits.One, Handshake.None);
            ThePrinterConn = new BluetoothPrinterConnection("0022583bc091");
            clearBuffer();
            lastYPos = 0;
        }

        public bool Print()
        {
            bool error = true;

            try
            {
                // Open the connection - physical connection is established here.
                ThePrinterConn.Open();

                printer = ZebraPrinterFactory.GetInstance(ThePrinterConn);
                PrinterStatus printerStatus = printer.GetCurrentStatus();
                bool ready = printerStatus.IsReadyToPrint;
                if (ready == false)
                {
                    return error;
                }



                //stvaranje oblika za slanje na printer
                String output = "! 0 200 200" + " " + (lastYPos + 100).ToString() + " " + "1\r\n";
                //output += "IN-MILLIMETERS\r\n";
                foreach (String s in buffer)
                {
                    output += s + "\r\n";
                }
                output += "PRINT\r\n";

                // Send the data to the printer as a byte array.
                ThePrinterConn.Write(Encoding.UTF8.GetBytes(output));
                //thePrinterConn.Close();
                error = false;
                
            }
            catch (Exception e)
            {
                Logger.Logger.Log(e);
                error = true;
            }
            finally
            {
                //brisi buffer
                buffer.Clear();

                //na kraju, baci gresku ako nije izvrseno do kraja...
                
            }
                return error;
        }

        public String[] ReadMagCard(int timeout)
        {
            bool error = true;

            try
            {
                ThePrinterConn.Open();
                printer = ZebraPrinterFactory.GetInstance(ThePrinterConn);
                mcr = printer.GetMagCardReader();

                PrinterStatus printerStatus = printer.GetCurrentStatus();
                bool ready = printerStatus.IsReadyToPrint;
                if (ready == false)
                {
                    return null;
                }

                //MagCardReader mcr = printer.GetMagCardReader();
                if (mcr != null)
                {
                    //read
                    String[] tracks = mcr.Read(timeout);

                    if (tracks[0] != "" || tracks[1] != "" || tracks[2] != "")
                    {
                        ready = printerStatus.IsReadyToPrint;
                        if (ready)
                        {
                            String header = "! 0 200 200 0 1";
                            ThePrinterConn.Write(Encoding.UTF8.GetBytes(header + "\r\nBEEP 1\r\nPRINT\r\n"));
                        }
                    }

                    //SystemSounds.Beep.Play();
                    return tracks;
                }

                //thePrinterConn.Close();

                error = false;
                
            }
            catch (Exception e)
            {
                Logger.Logger.Log(e);
                error = true;
            }
            if (error)
            {
                return null;
            }
            else
            {
                return new String[3] { "", "", "" };
            }
        }

        public void Beep(int lenght){
            try
            {
                printer = ZebraPrinterFactory.GetInstance(ThePrinterConn);

                PrinterStatus printerStatus = printer.GetCurrentStatus();
                bool ready = printerStatus.IsReadyToPrint;
                if (ready)
                {
                    String header = "! 0 200 200 0 1";
                    ThePrinterConn.Write(Encoding.UTF8.GetBytes(header + "\r\nBEEP " + lenght.ToString() + "\r\nPRINT\r\n"));
                }
            }
            catch (Exception e)
            {
                Logger.Logger.Log(e);
                return;
            }
        }

        public bool KeepAlive()
        {
            bool error = true;

            try
            {
                // Open the connection - physical connection is established here.
                ThePrinterConn.Open();

                printer = ZebraPrinterFactory.GetInstance(ThePrinterConn);
                PrinterStatus printerStatus = printer.GetCurrentStatus();
                bool ready = printerStatus.IsReadyToPrint;
                if (ready == false)
                {
                    return error;
                }



                //stvaranje oblika za slanje na printer
                String output = "! 0 200 200 100 1\r\n";
                output += "PRINT\r\n";

                // Send the data to the printer as a byte array.
                ThePrinterConn.Write(Encoding.UTF8.GetBytes(output));
                //thePrinterConn.Close();
                error = false;

            }
            catch (Exception e)
            {
                Logger.Logger.Log(e);
                error = true;
            }
            finally
            {

                //na kraju, baci gresku ako nije izvrseno do kraja...

            }
            return error;
        }
       
    }
}