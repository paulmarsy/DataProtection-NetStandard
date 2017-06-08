using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using DataProtection.Abstractions;
using DataProtection.Platforms.CredentialStore.Interop;

namespace DataProtection.Platforms.CredentialStore
{
    public unsafe class CredentialStoreVault : IVault
    {
        public void Delete(string key)
        {
            var success = Advapi32.CredDelete(key, CRED_TYPE.CRED_TYPE_GENERIC, 0);
            if (!success)
                throw new Win32Exception(Marshal.GetLastWin32Error());
        }

        public bool Exists(string key)
        {
            return Get(key) != null;
        }

        public byte[] Get(string key)
        {
            var success = Advapi32.CredRead(key, CRED_TYPE.CRED_TYPE_GENERIC, 0, out IntPtr credentialPtr);
            if (!success)
                throw new Win32Exception(Marshal.GetLastWin32Error());

            using (var criticalCredentialHandle = new CriticalCredentialHandle(credentialPtr))
            {
                var credential = criticalCredentialHandle.GetCredential();

                var buffer = new byte[credential.CredentialBlobSize];
                Marshal.Copy((IntPtr) credential.CredentialBlob, buffer, 0, (int) credential.CredentialBlobSize);

                return buffer;
            }
        }

        public void Put(string key, byte[] blob)
        {
            fixed (byte* blobPtr = blob)
            {
                var credential = new CREDENTIAL
                {
                    Flags = 0x0,
                    Type = CRED_TYPE.CRED_TYPE_GENERIC,
                    TargetName = key,
                    Comment = null,
                    TargetAlias = null,
                    CredentialBlobSize = (uint) blob.Length,
                    CredentialBlob = blobPtr,
                    Persist = Advapi32.CRED_PERSIST_LOCAL_MACHINE,
                    AttributesCount = 0,
                    Attributes = IntPtr.Zero,
                    UserName = RuntimeEnvironmentHelper.AccountName
                };
                var success = Advapi32.CredWrite(ref credential, 0);
                if (!success)
                    throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }
    }
}