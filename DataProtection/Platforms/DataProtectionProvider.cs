using System;
using DataProtection.Abstractions;
using DataProtection.Platforms.Mac;
using DataProtection.Platforms.Win;

namespace DataProtection.Platforms.CredentialStore
{
    public class DataProtectionProvider : IDataProtectionProvider
    {
        public IDataProtector GetDataProtector()
        {
            if (RuntimeEnvironmentHelper.IsWindows())
                return new DpapiDataProtector();

            throw new PlatformNotSupportedException();
        }

        public IVault GetVault()
        {
            if (RuntimeEnvironmentHelper.IsWindows())
                return new CredentialStoreVault();
            if (RuntimeEnvironmentHelper.IsOSX())
                return new KeyChainVault();

            throw new PlatformNotSupportedException();
        }
    }
}