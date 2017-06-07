using Microsoft.Win32.SafeHandles;
using System;
using System.Runtime.InteropServices;

namespace DataProtection.Platforms.CredentialStore.Interop
{
    internal class CriticalCredentialHandle : CriticalHandleZeroOrMinusOneIsInvalid
    {
        public CriticalCredentialHandle(IntPtr preexistingHandle)
        {
            SetHandle(preexistingHandle);
        }

        public CREDENTIAL GetCredential()
        {
            if (!IsInvalid)
                return (CREDENTIAL) Marshal.PtrToStructure(handle, typeof(CREDENTIAL));

            throw new InvalidOperationException("Invalid CriticalHandle!");
        }

        protected override bool ReleaseHandle()
        {
            if (!IsInvalid)
            {
                Advapi32.CredFree(handle);
                SetHandleAsInvalid();
                return true;
            }
            return false;
        }
    }
}