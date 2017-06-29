using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Model;
using Raverus.FiskalizacijaDEV;
using System.Xml;
using System.Globalization;
using System.Net;
using System.IO;

namespace Controller
{
    public static class ChargesController
    {
        static DateTime summerStart = new DateTime(DateTime.Now.Year, 6, 1);
        static DateTime summerEnd = new DateTime(DateTime.Now.Year, 9, 30);


        public static decimal GetCharge(string ticketType, string vehicleType, out decimal chargeBase, out decimal chargeTax)
        {
            Price price = Database.Prices.GetPrice(isSummer(), ticketType, vehicleType);
            decimal charge = (price != null) ? price.Charge : 0;
            if (price == null || price.Equals(0))
            {
                chargeTax = 0;
                chargeBase = 0;
            }
            else
            {
                chargeBase = Decimal.Round(charge / new Decimal(1.25), 2);
                chargeTax = Decimal.Round(charge - chargeBase, 2);
            }
            return Decimal.Round(charge, 2);
        }

        public static decimal GetCharge(string ticketType, string vehicleType, DateTime vehicleCheckedInDate, out decimal chargeBase, out decimal chargeTax)
        {
            Price price = Database.Prices.GetPrice(isSummer(), ticketType, vehicleType);
            int hours = Convert.ToInt32(Math.Ceiling((DateTime.Now - vehicleCheckedInDate).TotalHours));
            decimal charge = (price != null) ? price.Charge * hours : 0;
            if (price == null || price.Equals(0))
            {
                chargeTax = 0;
                chargeBase = 0;
            }
            else
            {
                chargeBase = Decimal.Round(charge / new Decimal(1.25), 2);
                chargeTax = Decimal.Round(charge - chargeBase, 2);
            }
            return Decimal.Round(charge, 2);
        }

        public static decimal GetCharge(string ticketType, string vehicleType)
        {
            return GetCharge(ticketType, vehicleType, isSummer());
        }

        public static decimal GetCharge(string ticketType, string vehicleType, bool summerTariff)
        {
            Price price = Database.Prices.GetPrice(summerTariff, ticketType, vehicleType);
            if (price == null)
            {
                return 0;
            }
            return price.Charge;
        }

        public static void UpdateCharge(decimal charge, bool summerTariff, string ticketType, string vehicleType)
        {
            Database.Prices.UpdatePrice(charge, summerTariff, ticketType, vehicleType);
        }

        private static bool isSummer(){
            DateTime now = DateTime.Now;
            return now >= summerStart && now <= summerEnd;
        }

        static ChargesController()
        {
            System.Net.ServicePointManager.CertificatePolicy = new Raverus.FiskalizacijaDEV.PopratneFunkcije.TrustAllCertificatePolicy();
        }

        public static void Fiskaliziraj(string licencePlate, DateTime checkedIn, String operaterOIB, decimal charge, decimal chargeBase, decimal chargeTax, out string ZIK, out string JIR)
        {
            var oib = "47148433806";
            var recipientCount = Controller.ChargeRegularUserController.GetRecipientCount().ToString();

            try
            {
                var uri = new Uri(String.Format("http://spinsplit.ddns.net:5000//api/Fiskalizacija/FiskalizirajLukaDubrovnikWinCe?oib={0}&charge={1}&chargeBase={2}&chargeTax={3}&recipientCount={4}&oibOper={5}",
                    oib, charge, chargeBase, chargeTax, recipientCount, operaterOIB));

                HttpWebRequest request = WebRequest.Create(uri) as HttpWebRequest;
                request.ContentType = "text/xml; encoding='utf-8'";
                
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.XmlResolver = null;
                xmlDoc.Load(response.GetResponseStream());

                JIR = xmlDoc.DocumentElement.ChildNodes[0].InnerText;
                ZIK = xmlDoc.DocumentElement.ChildNodes[1].InnerText;
                
            }
            catch (Exception e)
            {
                Logger.Logger.Log(e);
                JIR = "";
                ZIK = "";
                throw new Exception("Greška prilikom fiskalizacije. Da li je uređaj spojen na Internet?");
            }

        }
    }
}
