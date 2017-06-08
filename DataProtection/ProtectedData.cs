using DataProtection.Abstractions;
using DataProtection.Extensions;
using DataProtection.Platforms.CredentialStore;

namespace DataProtection
{
    public static class ProtectedData
    {
        private static readonly IDataProtectionProvider Provider = new DataProtectionProvider();

        public static byte[] Protect(byte[] plaintext)
        {
            return Provider.GetDataProtector().Protect(plaintext);
        }

        public static string Protect(string plaintext)
        {
            return Protect(plaintext.GetBytes()).ToBase64();
        }

        public static byte[] Unprotect(byte[] protectedData)
        {
            return Provider.GetDataProtector().Unprotect(protectedData);
        }

        public static string Unprotect(string protectedData)
        {
            return Unprotect(protectedData.FromBase64()).GetString();
        }

        public static byte[] GetBytes(string key)
        {
            return Provider.GetVault().Get(key);
        }

        public static string GetString(string key)
        {
            return GetBytes(key).GetString();
        }

        public static void Put(string key, byte[] blob)
        {
            Provider.GetVault().Put(key, blob);
        }

        public static void Put(string key, string blob)
        {
            Put(key, blob.GetBytes());
        }

        public static void Delete(string key)
        {
            Provider.GetVault().Delete(key);
        }
    }
}