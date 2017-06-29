using System;
using System.IO;
using System.Xml;
using System.Diagnostics;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Raverus.FiskalizacijaDEV
{
    public class X509Certificate2CF
    {
        public int RSAKeySize { get; set; }
        public string RSAKeyExchangeAlgorithm { get; set; }
        public string RSASignatureAlgorithm { get; set; }
        public string SerialNumber { get; set; }
        public string Subject { get; set; }
        public string OIB { get; set; }
        public string Issuer { get; set; }
        public string SignatureAlgorithm { get; set; }
        public byte[] RawCertData { get; set; }
        public RSACryptoServiceProvider rsa { get; set; }


        public X509Certificate2CF(string certifikatDatoteka)
        {
            //popuni RSA i cert parametre iz XMLa na path-u  certifikatDatoteka (\\SDMMC\\fiskal_demo_cert.xml)
            if (File.Exists(certifikatDatoteka))
            {
                //kreiraj nove RSA parametre
                RSAParameters rsap = new RSAParameters();

                XmlReader xr = XmlReader.Create(certifikatDatoteka);
                string xname;
                while (xr.Read())
                {
                    if (xr.NodeType == XmlNodeType.Element)
                    {
                        xname = xr.Name;
                        if (xname == "RSAKeyValue")
                            continue;
                        xr.Read();

                        if (xr.NodeType == XmlNodeType.Text)
                        {
                            if (xname == "Modulus")
                                rsap.Modulus = Convert.FromBase64String(xr.Value);
                            else if (xname == "Exponent")
                                rsap.Exponent = Convert.FromBase64String(xr.Value);
                            else if (xname == "P")
                                rsap.P = Convert.FromBase64String(xr.Value);
                            else if (xname == "Q")
                                rsap.Q = Convert.FromBase64String(xr.Value);
                            else if (xname == "DP")
                                rsap.DP = Convert.FromBase64String(xr.Value);
                            else if (xname == "DQ")
                                rsap.DQ = Convert.FromBase64String(xr.Value);
                            else if (xname == "InverseQ")
                                rsap.InverseQ = Convert.FromBase64String(xr.Value);
                            else if (xname == "D")
                                rsap.D = Convert.FromBase64String(xr.Value);
                            else if (xname == "RSAKeySize")
                                RSAKeySize = Convert.ToInt32(xr.Value);
                            else if (xname == "RSAKeyExchangeAlgorithm")
                                RSAKeyExchangeAlgorithm = xr.Value;
                            else if (xname == "RSASignatureAlgorithm")
                                RSASignatureAlgorithm = xr.Value;
                            else if (xname == "SerialNumber")
                                SerialNumber = xr.Value;
                            else if (xname == "Subject")
                                Subject = xr.Value;
                            else if (xname == "Issuer")
                                Issuer = xr.Value;
                            else if (xname == "SignatureAlgorithm")
                                SignatureAlgorithm = xr.Value;
                            else if (xname == "RawCertData")
                                RawCertData = Convert.FromBase64String(xr.Value);
                        }
                    }
                }
                xr.Close();

                //dohvati OIB iz Subject
                OIB = Subject.Substring(Subject.LastIndexOf(", C=") - 11, 11);

                //kreiraj RSA servis sa duljinom ključa (KeySize) i RSA parametrima iz XML certifikata
                rsa = new RSACryptoServiceProvider(RSAKeySize);
                rsa.ImportParameters(rsap);
            }
        }
    }
}
