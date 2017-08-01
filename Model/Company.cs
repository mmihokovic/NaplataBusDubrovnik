using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string OIB { get; set; }

        public override string ToString()
        {
            if (String.IsNullOrEmpty(Name) && String.IsNullOrEmpty(Address) && String.IsNullOrEmpty(OIB))
            {
                return "Novi unos";
            }
            else
            {
                return Name + ", " + Address + (String.IsNullOrEmpty(OIB) ? String.Empty : ", " + OIB);
            }
        }
    }
}
