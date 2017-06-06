using System;
using System.Collections.Generic;
using System.Text;
using DataProtection.Abstractions;
using DataProtection.Platforms.Win;
using DataProtection.Platforms.Win.Interop;

namespace DataProtection.Platforms
{
    public class WindowsDataProtectionProvider : ManagedDataProtectionProvider
    {

        public override IDataProtector GetDataProtector()
        {
            const string BCRYPT_LIB = "bcrypt.dll";
            SafeLibraryHandle bcryptLibHandle = null;
            try
            {
                bcryptLibHandle = SafeLibraryHandle.Open(BCRYPT_LIB);
            }
            catch
            {
                // we'll handle the exceptional case later
            }
            if (bcryptLibHandle != null)
            {
                using (bcryptLibHandle)
                {
                    if (bcryptLibHandle.DoesProcExist("BCryptKeyDerivation"))
                    {
                        // We're running on Win8+.
                        return new DpapiNGDataProtector();
                    }
                    else
                    {
                        // We're running on Win7+.
                        return new DpapiDataProtector();
                    }
                }
            }
            else
            {
                // Not running on Win7+.
                throw new NotSupportedException();
            }
        }
    }
}
