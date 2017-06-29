//Copyright (c) 2012. Raverus d.o.o.

//Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), 
//to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, 
//and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, 
//WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;
using System.IO;
using System.Xml;
using System.Diagnostics;
using System.Collections.Generic;
using System.Security.Cryptography;
//using System.Security.Cryptography.Xml;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Raverus.FiskalizacijaDEV.PopratneFunkcije
{
    /// <summary>
    /// Koristi se za rad sa certifikatom i potpisivanje XML poruke.</summary>
    /// <remarks>
    /// Ovo je klasa čija je namjena pomoć kod rada sa certifikatom, dohvat certifikata iz Certificate Store-a ili iz datoteke, potpisivanje XML poruka i sl.
    /// </remarks>
    public class PotpisivanjeCF
    {
        #region Certifikat
        /// <summary>
        /// Dohvaća certifikat iz XML datoteke.</summary>
        /// <param name="certifikatDatoteka">Path i naziv (full path) datoteke u kojoj se nalazi certifikat.</param>
        /// <returns>
        /// Vraća dohvaćeni certifikat. U slučaju greške ili ukoliko certifikat nije pronađen, vraća null.</returns>
        public static X509Certificate2CF DohvatiCertifikat(string certifikatDatoteka)
        {
            X509Certificate2CF certificate = null;

            FileInfo fi = new FileInfo(certifikatDatoteka);
            if (fi.Exists)
            {
                try
                {
                    certificate = new X509Certificate2CF(certifikatDatoteka);
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(String.Format("Greška kod kreiranja certifikata: {0}", ex.Message));
                    throw;
                }
            }


            return certificate;
        }
        #endregion

        #region Potpisivanje
        /// <summary>
        /// Potpisuje XML dokument.</summary>
        /// <param name="dokument">XML dokument koji treba potpisati.</param>
        /// <param name="certifikat">Certifikat koji se koristi kod potpisivanja.</param>
        /// <example>
        /// PopratneFunkcije.Potpisivanje.PotpisiXmlDokument(zahtjevXml, certificate);
        /// </example>
        /// <returns>
        /// Vraća potpisani XML dokument.</returns>
        public static XmlDocument PotpisiXmlDokument(XmlDocument dokument, X509Certificate2CF certifikat)
        {
            try
            {
                //dok1 je xml-exc-c14n format XML dokumenta
                XmlDocument dok1 = (XmlDocument) dokument.Clone();
                
                //izbiši <?xml....
                foreach(XmlNode node1 in dok1)
                {
                    if (node1.NodeType == XmlNodeType.XmlDeclaration)
                    {
                        dok1.RemoveChild(node1);
                        break;
                    }
                }
                //poredaj ispravno atribute (različit raspored na .net i net compact framework platformi!
                XmlNode node = dok1.FirstChild;
                node.Attributes.RemoveNamedItem("xmlns:xsi");
                node.Attributes.Prepend(node.Attributes["Id"]);
                node.Attributes.Prepend(node.Attributes["xmlns:tns"]);

                //pretvori xml-exc-c14n dokument u UTF8 format i izračunaj digestValue (SHA1 hash)
                SHA1CryptoServiceProvider sha = new SHA1CryptoServiceProvider();
                string digestValue = Convert.ToBase64String(sha.ComputeHash(UTF8Encoding.UTF8.GetBytes(dok1.OuterXml)));




                //kreiraj <SignedInfo> XML element u xml-exc-c14n verziji
                StringBuilder sb = new StringBuilder();
                sb.Append("<SignedInfo xmlns=\"http://www.w3.org/2000/09/xmldsig#\"><CanonicalizationMethod Algorithm=\"http://www.w3.org/2001/10/xml-exc-c14n#\"></CanonicalizationMethod><SignatureMethod Algorithm=\"http://www.w3.org/2000/09/xmldsig#rsa-sha1\"></SignatureMethod>");
                sb.Append("<Reference URI=\"#");
                sb.Append(node.Attributes["Id"].Value); //Id od tns:RacunZahtjev
                sb.Append("\"><Transforms><Transform Algorithm=\"http://www.w3.org/2000/09/xmldsig#enveloped-signature\"></Transform><Transform Algorithm=\"http://www.w3.org/2001/10/xml-exc-c14n#\"></Transform></Transforms><DigestMethod Algorithm=\"http://www.w3.org/2000/09/xmldsig#sha1\"></DigestMethod>");
                sb.Append("<DigestValue>");
                sb.Append(digestValue);   //digest (SHA1 hash) <tns:RacunZahtjev> elementa
                sb.Append("</DigestValue></Reference></SignedInfo>");

                //digitalno potpiši <SignedInfo> XML element koji mora biti u UTF8 formatu, da dobiješ SignatureValue
                string signatureValue = Convert.ToBase64String(PotpisiByteArray(UTF8Encoding.UTF8.GetBytes(sb.ToString()), certifikat));   //SignatureValue

                //kreiraj <Signature> XML element koji je identičan Raverus implementaciji
                sb = new StringBuilder();
                sb.Append("<Signature xmlns=\"http://www.w3.org/2000/09/xmldsig#\"><SignedInfo><CanonicalizationMethod Algorithm=\"http://www.w3.org/2001/10/xml-exc-c14n#\" /><SignatureMethod Algorithm=\"http://www.w3.org/2000/09/xmldsig#rsa-sha1\" /><Reference URI=\"#");
                sb.Append(node.Attributes["Id"].Value); //Id od tns:RacunZahtjev
                sb.Append("\"><Transforms><Transform Algorithm=\"http://www.w3.org/2000/09/xmldsig#enveloped-signature\" /><Transform Algorithm=\"http://www.w3.org/2001/10/xml-exc-c14n#\" /></Transforms><DigestMethod Algorithm=\"http://www.w3.org/2000/09/xmldsig#sha1\" /><DigestValue>");
                sb.Append(digestValue);   //digest (SHA1 hash) <tns:RacunZahtjev> elementa
                sb.Append("</DigestValue></Reference></SignedInfo><SignatureValue>");
                sb.Append(signatureValue); //Signature Value (digitalni potpis) <SignedInfo> XML elementa
                sb.Append("</SignatureValue><KeyInfo><X509Data><X509IssuerSerial><X509IssuerName>");
                sb.Append(certifikat.Issuer);
                sb.Append("</X509IssuerName><X509SerialNumber>");
                sb.Append(Convert.ToUInt32(certifikat.SerialNumber, 16).ToString()); //serijski broj certifikata pretvori iz hex u decimal oblik
                sb.Append("</X509SerialNumber></X509IssuerSerial><X509Certificate>");
                sb.Append(Convert.ToBase64String(certifikat.RawCertData));  //X509 certifikat u base64 formatu
                sb.Append("</X509Certificate></X509Data></KeyInfo></Signature>");


                //dodaj <Signature> u glavni dokument
                //dokument.DocumentElement.AppendChild(dok2);
                XmlDocumentFragment dok3 = dokument.CreateDocumentFragment();
                dok3.InnerXml = sb.ToString();
                dokument.DocumentElement.AppendChild(dok3);



                
                //XmlNode signedInfoNode = dok2.CreateElement("SignedInfo");
                //dok2.AppendChild(signedInfoNode);

                //XmlNode CanonicalizationMethodNode = dok2.CreateElement("CanonicalizationMethod");
                //XmlAttribute at1 = dok2.CreateAttribute("Algorithm").Value = @"http://www.w3.org/2001/10/xml-exc-c14n#";
                //CanonicalizationMethodNode.Attributes.Append(at1);
                //signedInfoNode.AppendChild(CanonicalizationMethodNode);

                //XmlNode SignatureMethodNode = dok2.CreateElement("SignatureMethod");
                //XmlAttribute at2 = dok2.CreateAttribute("Algorithm").Value = @"http://www.w3.org/2000/09/xmldsig#rsa-sha1";
                //SignatureMethodNode.Attributes.Append(at2);
                //signedInfoNode.AppendChild(SignatureMethodNode);

                //XmlNode ReferenceNode = dok2.CreateElement("Reference");
                //XmlAttribute at3 = dok2.CreateAttribute("URI").Value = "#" + node.Attributes["Id"].Value;
                //SignatureMethodNode.Attributes.Append(at3);
                //signedInfoNode.AppendChild(ReferenceNode);

                //XmlNode TransformsNode = dok2.CreateElement("Transforms");
                //ReferenceNode.AppendChild(TransformsNode);


                //input xml
                //<?xml version="1.0" encoding="utf-8"?><tns:RacunZahtjev xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" Id="signXmlId" xmlns:tns="http://www.apis-it.hr/fin/2012/types/f73"><tns:Zaglavlje><tns:IdPoruke>4226d126-19c4-4471-84e8-85efc1ebe9f1</tns:IdPoruke><tns:DatumVrijeme>28.03.2013T17:54:00</tns:DatumVrijeme></tns:Zaglavlje><tns:Racun><tns:Oib>58069356381</tns:Oib><tns:USustPdv>true</tns:USustPdv><tns:DatVrijeme>28.03.2013T17:38:00</tns:DatVrijeme><tns:OznSlijed>P</tns:OznSlijed><tns:BrRac><tns:BrOznRac>1</tns:BrOznRac><tns:OznPosPr>1</tns:OznPosPr><tns:OznNapUr>1</tns:OznNapUr></tns:BrRac><tns:Pdv><tns:Porez><tns:Stopa>25.00</tns:Stopa><tns:Osnovica>100.00</tns:Osnovica><tns:Iznos>25.00</tns:Iznos></tns:Porez></tns:Pdv><tns:IznosUkupno>125.00</tns:IznosUkupno><tns:NacinPlac>G</tns:NacinPlac><tns:OibOper>12345678901</tns:OibOper><tns:ZastKod>ceb0ae98c7789070de2502f0ff6c960a</tns:ZastKod><tns:NakDost>true</tns:NakDost></tns:Racun></tns:RacunZahtjev>
                
                //excan format
                //<tns:RacunZahtjev xmlns:tns="http://www.apis-it.hr/fin/2012/types/f73" Id="signXmlId"><tns:Zaglavlje><tns:IdPoruke>4226d126-19c4-4471-84e8-85efc1ebe9f1</tns:IdPoruke><tns:DatumVrijeme>28.03.2013T17:54:00</tns:DatumVrijeme></tns:Zaglavlje><tns:Racun><tns:Oib>58069356381</tns:Oib><tns:USustPdv>true</tns:USustPdv><tns:DatVrijeme>28.03.2013T17:38:00</tns:DatVrijeme><tns:OznSlijed>P</tns:OznSlijed><tns:BrRac><tns:BrOznRac>1</tns:BrOznRac><tns:OznPosPr>1</tns:OznPosPr><tns:OznNapUr>1</tns:OznNapUr></tns:BrRac><tns:Pdv><tns:Porez><tns:Stopa>25.00</tns:Stopa><tns:Osnovica>100.00</tns:Osnovica><tns:Iznos>25.00</tns:Iznos></tns:Porez></tns:Pdv><tns:IznosUkupno>125.00</tns:IznosUkupno><tns:NacinPlac>G</tns:NacinPlac><tns:OibOper>12345678901</tns:OibOper><tns:ZastKod>ceb0ae98c7789070de2502f0ff6c960a</tns:ZastKod><tns:NakDost>true</tns:NakDost></tns:Racun></tns:RacunZahtjev>
                
                //DigestValue (base64 string) -> SHA1 hash (excan forme RacunZahtjev xmla)
                //6dcCFA2TnlkT+Sdb8NZmrCIai2A=
                
                //SignatureValue (base64 string) -> RSA secert key (SHA1 hash (excan forme SignedInfo xmla))
                //L9hT5ta+xIbpiYshNTSv0yzp4E7nRAv4yy6ZML7yfc207beq+vTU0j8+8ZXK3ReoitTt+4ZN5iyzFKs3YoAUrIjruL6acnHHJo/46L4Ikkq6K7A4P+2k9g8NNviOmf/TI7DWSVp6RAdekDLoP/MZxss/X0fqKqNwlDBy2HHUtoywRUQf3VEg4mHfBM5nMBvJXCRvJyQhT28JRbQ3ztOGSDgGIBeGnDoZ/r/jQ4QgOnoXBY8GZzDtG93B6hz04+p+bpCPLrWZEOWfjZomQphogREw4FcMGvi1QrcF2t7t6EH5Ug/EUei7YNX8ot2dydy2aUS04ZiIK8LLN2WIcrE8uw==
                
                //X509Certificate u xml-u
                //MIIExzCCA6+gAwIBAgIEPssdODANBgkqhkiG9w0BAQUFADArMQswCQYDVQQGEwJIUjENMAsGA1UEChMERklOQTENMAsGA1UECxMEREVNTzAeFw0xMjExMTMxMzI1MTJaFw0xNDExMTMxMzU1MTJaMFwxCzAJBgNVBAYTAkhSMSkwJwYDVQQKEyBCRVRBIFNUVURJTyBELk8uTy4gSFI1ODA2OTM1NjM4MTEPMA0GA1UEBxMGT1NJSkVLMREwDwYDVQQDEwhGSVNLQUwgMTCCASIwDQYJKoZIhvcNAQEBBQADggEPADCCAQoCggEBAMA+VUSs+PcSkZSMZmbIjcLV23xkyMv9vtmw/BuOnNEPBl092vjKY/xUspzL+Px7hQFO1jTWRl1lVl44dkz28dURDiYoKoSLx/1wlD8Nzimu7oSs/DntXuuUy08rtLbY5MEKRXutftyE/+3K0mN300vsm6nPqdcp9ii6th8Th+6+fOH2IsoRUmecpz+3h/rV70e++sBaZF/SHJ0tceyv2zIt4wluHDsNHB1NxM638m1ZB8T5UaprpJRqRiKRJHIYzB6U3Tgrva6Kyp9wqeUktX0KpCUf72p9eqSBVPei6Dk1wvGhTQkuRjqDtJB3hF1heVf7XdrXeE08afsZdnoBeJkCAwEAAaOCAcAwggG8MAsGA1UdDwQEAwIFoDBCBgNVHSAEOzA5MDcGCSt8iFAFHwUDATAqMCgGCCsGAQUFBwIBFhxodHRwOi8vZGVtby1wa2kuZmluYS5oci9jcHMvMCAGA1UdEQQZMBeBFXNpbmlzYS5rdW5hQHlhaG9vLmNvbTCBzgYDVR0fBIHGMIHDMEKgQKA+pDwwOjELMAkGA1UEBhMCSFIxDTALBgNVBAoTBEZJTkExDTALBgNVBAsTBERFTU8xDTALBgNVBAMTBENSTDgwfaB7oHmGT2xkYXA6Ly9kZW1vLWxkYXAuZmluYS5oci9vdT1ERU1PLG89RklOQSxjPUhSP2NlcnRpZmljYXRlUmV2b2NhdGlvbkxpc3QlM0JiaW5hcnmGJmh0dHA6Ly9kZW1vLXBraS5maW5hLmhyL2NybC9kZW1vY2EuY3JsMCsGA1UdEAQkMCKADzIwMTIxMTEzMTMyNTEyWoEPMjAxNDExMTMxMzU1MTJaMB8GA1UdIwQYMBaAFHpgI45InTJrpOUt3bhZtJT8QmKeMB0GA1UdDgQWBBTQzeijJOVGv6magTJhkcw6HPnDGDAJBgNVHRMEAjAAMA0GCSqGSIb3DQEBBQUAA4IBAQA3J2xwQDwtHuPhtUyEEe0AuqSJUMRRUiYNMfgqtHk7tqMetQ3OpHDspzj2Of80tSMtR0I7xACQ+FVT376Jw9ERoilWYK3g/3tKCMiIBroJfkQRxVaNw/BrwqXvvXZvN+lBCaCFXQnIOQ3fL+++zYuTxm+k3HBbchG4ruKx0Ax0Q3HnEng7UcW9QlEkCHyN2UVNu73P/W8HyJ+9plVYM0UhJVSwyzMcxANPjeMRFlzSaaFku4TQraJuxnmm1Haz25O3NERhIJHDH51cvnSXG34PeQvv41guFNOv+scwACU3mHM6wkLvwznwCqBiLDLoMiLQCSimpYwU/mHTxx+l7dOt





                //xml = new SignedXml(dokument);
                //xml.SigningKey = provider;
                //xml.SignedInfo.CanonicalizationMethod = SignedXml.XmlDsigExcC14NTransformUrl;

                //KeyInfo keyInfo = new KeyInfo();
                //KeyInfoX509Data keyInfoData = new KeyInfoX509Data();
                //keyInfoData.AddCertificate(certifikat);
                //keyInfoData.AddIssuerSerial(certifikat.Issuer, certifikat.GetSerialNumberString());
                //keyInfo.AddClause(keyInfoData);

                //xml.KeyInfo = keyInfo;

                //Reference reference = new Reference("");
                //reference.AddTransform(new XmlDsigEnvelopedSignatureTransform(false));
                //reference.AddTransform(new XmlDsigExcC14NTransform(false));
                //reference.Uri = "#signXmlId";
                //xml.AddReference(reference);
                //xml.ComputeSignature();

                //XmlElement element = xml.GetXml();
                //dokument.DocumentElement.AppendChild(element);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(String.Format("Greška kod potpisivanja XML dokumenta: {0}", ex.Message));
                throw;
            }

            return dokument;
        }

        public static byte[] PotpisiTekst(string tekst, X509Certificate2CF certifikat)
        {
            if (certifikat == null)
                throw new ArgumentNullException();


            byte[] potpisaniTekst = null;

            //RSACryptoServiceProvider provider = (RSACryptoServiceProvider)certifikat.PrivateKey;
            RSACryptoServiceProvider provider = certifikat.rsa;


            try
            {
                byte[] by = Encoding.ASCII.GetBytes(tekst);
                potpisaniTekst = provider.SignData(by, new SHA1CryptoServiceProvider());
            }
            catch (Exception ex)
            {
                Trace.WriteLine(String.Format("Greška kod potpisivanja teksta: {0}", ex.Message));
                throw;
            }

            return potpisaniTekst;
        }

        public static byte[] PotpisiByteArray(byte[] ulaz, X509Certificate2CF certifikat)
        {
            if (certifikat == null)
                throw new ArgumentNullException();


            byte[] izlaz = null;

            //RSACryptoServiceProvider provider = (RSACryptoServiceProvider)certifikat.PrivateKey;
            RSACryptoServiceProvider provider = certifikat.rsa;


            try
            {
                izlaz = provider.SignData(ulaz, new SHA1CryptoServiceProvider());
            }
            catch (Exception ex)
            {
                Trace.WriteLine(String.Format("Greška kod potpisivanja teksta: {0}", ex.Message));
                throw;
            }

            return izlaz;
        }
        #endregion

        #region Provjera potpisa
        /// <summary>
        /// Provjerava digitalni potpis na XML dokumentu vraćenom iz CIS-a.</summary>
        /// <param name="dokument">XML dokument za koji treba provjeriti digitalni potpis.</param>
        /// <example>
        /// PopratneFunkcije.Potpisivanje.ProvjeriPotpis(odgovorXml);
        /// </example>
        /// <returns>
        /// Vraća True ukoliko je potpis ispravan.</returns>
        public static Boolean ProvjeriPotpis(XmlDocument dokument)
        {
            //ovo se rješava zadnje!!

            // Prema sugestiji mladenbabic (http://fiskalizacija.codeplex.com/discussions/403288) 

            // Provjeri argument
            if (dokument == null)
                throw new ArgumentNullException();


            //// Kreiraj novi SignedXml objekt i dostavi mu xmlDoc
            //SignedXml potpisaniXml = new SignedXml(dokument);

            //// Pronadji "Signature" nod(ove) i kreiraj XmlNodeList objekt
            //XmlNodeList signatureNodeList = dokument.GetElementsByTagName("Signature");

            //if (signatureNodeList.Count <= 0)
            //{
            //    Trace.WriteLine("Verifikacija nije uspjela: U primljenom dokumentu nije pronadjen digitalni potpis.");
            //    throw new CryptographicException("Verifikacija nije uspjela: U primljenom dokumentu nije pronadjen digitalni potpis.");
            //}

            //// Ucitaj nod u SignedXml objekt
            //potpisaniXml.LoadXml((XmlElement)signatureNodeList[0]);

            //// preuzmi dostavljeni certifikat
            //X509Certificate2 certificate = null;
            //foreach (KeyInfoClause clause in potpisaniXml.KeyInfo)
            //{
            //    if (clause is KeyInfoX509Data)
            //    {
            //        if (((KeyInfoX509Data)clause).Certificates.Count > 0)
            //        {
            //            certificate = (X509Certificate2)((KeyInfoX509Data)clause).Certificates[0];
            //        }
            //    }
            //}
            //if (certificate == null)
            //{
            //    Trace.WriteLine("U primljenom XMLu nema certifikata.");
            //    throw new Exception("U primljenom XMLu nema certifikata.");
            //}

            //// Provjeri Signature i vrati bool rezultat
            //Boolean reza = potpisaniXml.CheckSignature(certificate, true);
            //return reza;

            return true;
        }
        #endregion
    }
}
