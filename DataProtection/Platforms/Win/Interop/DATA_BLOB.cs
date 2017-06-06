using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

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
