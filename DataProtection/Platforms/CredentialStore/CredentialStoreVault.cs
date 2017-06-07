using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using DataProtection.Abstractions;
using DataProtection.Platforms.CredentialStore.Interop;
using System.Runtime.InteropServices;

namespace DataProtection.Platforms.CredentialStore
{
    public unsafe class CredentialStoreVault : IVault
    {
        public void Delete(string id)
        {
            var success = Advapi32.CredDelete(id, CRED_TYPE.CRED_TYPE_GENERIC, 0);
            if (!success)
                throw new Win32Exception(Marshal.GetLastWin32Error());
        }

        public bool Exists(string id)
        {
            return Get(id) != null;
        }

        public byte[] Get(string id)
        {
        ///    Debugger.Launch();
        //    Debugger.Break();
            var success = Advapi32.CredRead(id, CRED_TYPE.CRED_TYPE_GENERIC, 0, out IntPtr credentialPtr);
            if (!success)
                throw new Win32Exception(Marshal.GetLastWin32Error());

            using (var criticalCredentialHandle = new CriticalCredentialHandle(credentialPtr))
            {
                var credential = criticalCredentialHandle.GetCredential();
                
                var buffer = new byte[credential.CredentialBlobSize];
                Marshal.Copy((IntPtr)credential.CredentialBlob, buffer, 0, (int)credential.CredentialBlobSize);

                return buffer;
            }
        }

        public void Put(string id, byte[] blob)
        {
            if (blob.Length > Advapi32.CRED_MAX_CREDENTIAL_BLOB_SIZE)
                throw new ArgumentOutOfRangeException(nameof(blob), "CRED_MAX_CREDENTIAL_BLOB_SIZE");
            fixed (byte* blobPtr = blob)
            {
                var credential = new CREDENTIAL()
                {
                    Flags = 0x0,
                    Type = CRED_TYPE.CRED_TYPE_GENERIC,
                    TargetName = id,
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
