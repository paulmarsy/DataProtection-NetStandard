using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace DataProtection.Platforms.CredentialStore.Interop
{
    internal static class Advapi32
    {
        public const int CRED_MAX_CREDENTIAL_BLOB_SIZE = 512;
        public const int CRED_PERSIST_LOCAL_MACHINE = 2;
        private const string ADVAPI32_LIB = "Advapi32.dll";

        [DllImport(ADVAPI32_LIB, SetLastError = true)]
        internal static extern bool CredDelete([In] string TargetName, [In] CRED_TYPE Type, [In] int Flags);
        [DllImport(ADVAPI32_LIB, SetLastError = true)]
        internal static extern bool CredRead([In] string TargetName, [In] CRED_TYPE Type, [In] int Flags, [Out] out IntPtr Credential);

        [DllImport(ADVAPI32_LIB, SetLastError = true)]
        internal static extern bool CredWrite([In] ref CREDENTIAL Credential, [In] UInt32 Flags);
        [DllImport(ADVAPI32_LIB, SetLastError = true)]
        internal static extern void CredFree([In] IntPtr Buffer);
    }
}
