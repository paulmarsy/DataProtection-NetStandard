using System;
using System.Collections.Generic;
using System.Text;
using DataProtection.Abstractions;

namespace DataProtection.Platforms.CredentialStore
{
    public class DataProtectionProvider : IDataProtectionProvider
    {
        public IDataProtector GetDataProtector()
        {
            throw new NotImplementedException();
        }

        public IVault GetVault()
        {
            throw new NotImplementedException();
        }
    }
}
