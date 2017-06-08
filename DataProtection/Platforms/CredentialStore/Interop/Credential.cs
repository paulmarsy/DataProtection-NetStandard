using System;
using System.Runtime.InteropServices;

namespace DataProtection.Platforms.CredentialStore.Interop
{
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct CREDENTIAL
    {
        public uint Flags;
        public CRED_TYPE Type;
        public string TargetName;
        public string Comment;
        public System.Runtime.InteropServices.ComTypes.FILETIME LastWritten;
        public uint CredentialBlobSize;
        public byte* CredentialBlob;
        public uint Persist;
        public uint AttributesCount;
        public IntPtr Attributes;
        public string TargetAlias;
        public string UserName;
    }
}