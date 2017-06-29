using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Controller.printer
{
    public interface IPrinter
    {
        void AddTextLine(String line, int font, int size, int posx, int posy);

        void AddHorizontalLine(int posx, int posy);

        void AddBarCode(string data, int posy);

        void SetJustification(String justification);

        void Close();

        void Reconnect();

        bool Print();

        String[] ReadMagCard(int timeout);

        void Beep(int lenght);

        bool KeepAlive();
    }
}
