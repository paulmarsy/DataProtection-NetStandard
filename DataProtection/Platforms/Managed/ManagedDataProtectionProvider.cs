using System;
using System.Collections.Generic;
using System.Text;
using DataProtection.Abstractions;
using DataProtection.Platforms.Managed;

namespace DataProtection.Platforms
{
    public class ManagedDataProtectionProvider : IDataProtectionProvider
    {
        public virtual IDataProtector GetDataProtector() => new ManagedDataProtector();

        public virtual IVault GetVault() => new ManagedVault();
    }
}
