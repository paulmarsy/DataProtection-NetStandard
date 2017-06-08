using System.Runtime.InteropServices;

namespace DataProtection.Platforms.Win.Interop
{
    // http://msdn.microsoft.com/en-us/library/windows/desktop/aa381414(v=vs.85).aspx
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct DATA_BLOB
    {
        public uint cbData;
        public byte* pbData;
    }
}