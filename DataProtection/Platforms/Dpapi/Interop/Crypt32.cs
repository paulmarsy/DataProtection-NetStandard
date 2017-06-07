using DataProtection.Platforms.Win.Interop;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace DataProtection.Interop.Win
{
    [SuppressUnmanagedCodeSecurity]
    internal unsafe static class Crypt32
    {
        internal const uint CRYPTPROTECT_UI_FORBIDDEN = 0x1;
        internal const uint CRYPTPROTECT_LOCAL_MACHINE = 0x4;
        private const string CRYPT32_LIB = "crypt32.dll";
        [DllImport(CRYPT32_LIB, CallingConvention = CallingConvention.Winapi, SetLastError = true)]
        // http://msdn.microsoft.com/en-us/library/windows/desktop/aa380261(v=vs.85).aspx
        internal static extern bool CryptProtectData(
            [In] DATA_BLOB* pDataIn,
            [In] IntPtr szDataDescr,
            [In] IntPtr pOptionalEntropy,
            [In] IntPtr pvReserved,
            [In] IntPtr pPromptStruct,
            [In] uint dwFlags,
            [Out] out DATA_BLOB pDataOut);

        [DllImport(CRYPT32_LIB, CallingConvention = CallingConvention.Winapi, SetLastError = true)]
        // http://msdn.microsoft.com/en-us/library/windows/desktop/aa380882(v=vs.85).aspx
        internal static extern bool CryptUnprotectData(
            [In] DATA_BLOB* pDataIn,
            [In] IntPtr ppszDataDescr,
            [In] IntPtr pOptionalEntropy,
            [In] IntPtr pvReserved,
            [In] IntPtr pPromptStruct,
            [In] uint dwFlags,
            [Out] out DATA_BLOB pDataOut);
    }
}
