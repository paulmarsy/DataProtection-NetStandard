using System;
using System.Collections.Generic;
using System.Text;

namespace DataProtection.Abstractions
{
    public interface IDataProtectionProvider
    {
        IDataProtector GetDataProtector();
        IVault GetVault();
    }
}
