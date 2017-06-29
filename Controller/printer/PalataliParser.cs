using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Controller
{
    class PalataliParser
    {
        public static string Parser(String ulaz){
            ulaz = zamijeni(ulaz, 'č', 'c');
            ulaz = zamijeni(ulaz, 'ć', 'c');
            ulaz = zamijeni(ulaz, 'š', 's');
            ulaz = zamijeni(ulaz, 'ž', 'z');
            ulaz = zamijeni(ulaz, 'đ', 'd');
            return ulaz;
        }

        private static string zamijeni(String ulaz, char trazeni, char zamjena)
        {
            char[] str = ulaz.ToCharArray();
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == trazeni)
                {
                    str[i] = zamjena;
                }
            }
            return new string(str);
        }
    }
}
