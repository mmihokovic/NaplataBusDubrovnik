using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Threading;

namespace Controller.printer
{
    public class TestPrinter : IPrinter
    {
        private List<String> buffer;
        private int lastYPos;

        public static bool printFailed = false;
        public static bool magReadFailed = false;
        public static bool magCardPresent = false;

        private Bitmap paper;
        private Graphics graphics;

        const int paperWidth = 500;
        const int paperHeight = 1150;

        enum justifications { LEFT, CENTER, RIGHT };
        private justifications justification;



        public TestPrinter()
        {
            paper = new Bitmap(paperWidth, paperHeight);
            graphics = Graphics.FromImage(paper);

            justification = 0;

            //ostale inicijalizacije...
            buffer = new List<String>();
            lastYPos = 0;

        }

        public void Beep(int time)
        {

        }

        public void Close() { }

        public void AddTextLine(String line, int font, int size, int posx, int posy)
        {
            int tempPosition = posx;
            if (justification == justifications.CENTER)
            {
                tempPosition = paper.Size.Width / 2 - (line.Length / 2) * 7;
            }
            else if (justification == justifications.RIGHT)
            {
                tempPosition = paper.Size.Width - (line.Length * 7);
            }

            graphics.DrawString(line, new Font(FontFamily.GenericSansSerif, 10,
                FontStyle.Regular), new SolidBrush(Color.White), tempPosition, posy);
            lastYPos = posy;
        }


        public void AddHorizontalLine(int posx, int posy)
        {
            const String line = "____________________";

            int tempPosition = posx;
            if (justification == justifications.CENTER)
            {
                tempPosition = paper.Size.Width / 2 - (line.Length / 2) * 7;
            }
            else if (justification == justifications.RIGHT)
            {
                tempPosition = paper.Size.Width - (line.Length * 7);
            }

            graphics.DrawString(line, new Font(FontFamily.GenericSansSerif, 10,
                FontStyle.Regular), new SolidBrush(Color.White), tempPosition, posy);
            lastYPos = posy;
        }

        public void SetJustification(String newJustification)
        {
            if (newJustification.ToLower() == "left")
            {
                justification = justifications.LEFT;
            }
            else if (newJustification.ToLower() == "center")
            {
                justification = justifications.CENTER;
            }
            else if (newJustification.ToLower() == "right")
            {
                justification = justifications.RIGHT;
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public void clearBuffer()
        {
            buffer.Clear();
        }

        public void close()
        {
            // Make sure the data got to the printer before closing the connection
            Thread.Sleep(500);

            // Close the connection to release resources.
            paper.Save("paper.bmp", System.Drawing.Imaging.ImageFormat.Bmp);
            paper = new Bitmap(paperWidth, paperHeight);
            graphics = Graphics.FromImage(paper);
        }

        public void Reconnect()
        {
            if (paper != null)
            {
                paper.Dispose();
            }
            paper = new Bitmap(paperWidth, paperHeight);
            graphics = Graphics.FromImage(paper);

            clearBuffer();
            lastYPos = 0;
        }

        public bool Print()
        {
            bool error = false;



            if (!printFailed)
            {
                paper.Save(@"\Storage Card\paper.Bmp", System.Drawing.Imaging.ImageFormat.Bmp);
            }

            if (paper != null)
            {
                paper.Dispose();
            }
            paper = new Bitmap(paperWidth, paperHeight);
            graphics = Graphics.FromImage(paper);

            if (printFailed)
            {
                error = true;
            }

            return error;
        }


        public String[] ReadMagCard(int timeout)
        {

            if (magReadFailed || printFailed)
            {
                return null;
            }

            Thread.Sleep(timeout);


            if (magCardPresent)
            {
                return new String[] { "", "17655043160", "" };
            }
            else
            {
                return new String[] { "", "", "" };
            }



        }

        public void AddBarCode(string data, int ypos)
        {
            AddTextLine(data.ToString(), 5, 5, 150, ypos);
        }

        public bool KeepAlive() 
        {
            return false;
        }
    }
}
