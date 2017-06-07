using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace DataProtection.Platforms.CredentialStore.Interop
{
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct CREDENTIAL
    {
        public UInt32 Flags;
        public CRED_TYPE Type;
        public string TargetName;
        public string Comment;
        public System.Runtime.InteropServices.ComTypes.FILETIME LastWritten;
        public UInt32 CredentialBlobSize;
        public byte* CredentialBlob;
        public UInt32 Persist;
        public UInt32 AttributesCount;
        public IntPtr Attributes;
        public string TargetAlias;
        public string UserName;
    }
}
