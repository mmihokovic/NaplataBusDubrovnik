using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace Raverus.FiskalizacijaDEV.PopratneFunkcije
{
    class TrustAllCertificatePolicy : ICertificatePolicy
    {
        public TrustAllCertificatePolicy()
        {
        }

        public bool CheckValidationResult(ServicePoint sp,
            X509Certificate cert, WebRequest req, int problem)
        {
            return true;
        }
    }
}
