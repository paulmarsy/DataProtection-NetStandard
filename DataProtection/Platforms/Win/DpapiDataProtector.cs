using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using DataProtection.Abstractions;
using DataProtection.Platforms.Win.Interop;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using DataProtection.Interop.Win;

namespace DataProtection.Platforms.Win
{
    public unsafe class DpapiDataProtector :IDataProtector
    {
        private const uint CRYPTPROTECT_UI_FORBIDDEN = 0x1;
        private const uint CRYPTPROTECT_LOCAL_MACHINE = 0x4;
        public byte[] Protect(byte[] plaintext)
        {
            fixed (byte* pbPlaintextSecret = plaintext)
            {
                try
                {
                        return ProtectWithDpapi(pbPlaintextSecret, (uint)plaintext.Length, fLocalMachine: false);
                }
                finally
                {
                    // To limit exposure to the GC.
                    Array.Clear(plaintext, 0, plaintext.Length);
                }
            }
        }
        private  byte[] ProtectWithDpapi(byte* pbSecret, uint cbSecret, bool fLocalMachine = false)
        {
            byte dummy; // provides a valid memory address if the secret or entropy has zero length

            var dataIn = new DATA_BLOB()
            {
                cbData = cbSecret,
                pbData = (pbSecret != null) ? pbSecret : &dummy
            };
            var dataOut = default(DATA_BLOB);

            RuntimeHelpers.PrepareConstrainedRegions();

            try
            {
                var success = Crypt32.CryptProtectData(
                    pDataIn: &dataIn,
                    szDataDescr: IntPtr.Zero,
                    pOptionalEntropy: IntPtr.Zero,
                    pvReserved: IntPtr.Zero,
                    pPromptStruct: IntPtr.Zero,
                    dwFlags: CRYPTPROTECT_UI_FORBIDDEN | ((fLocalMachine) ? CRYPTPROTECT_LOCAL_MACHINE : 0),
                    pDataOut: out dataOut);
                if (!success)
                {
                    var errorCode = Marshal.GetLastWin32Error();
                    throw new CryptographicException(errorCode);
                }

                var dataLength = checked((int)dataOut.cbData);
                var retVal = new byte[dataLength];
                Marshal.Copy((IntPtr)dataOut.pbData, retVal, 0, dataLength);
                return retVal;
            }
            finally
            {
                // Free memory so that we don't leak.
                // FreeHGlobal actually calls LocalFree.
                if (dataOut.pbData != null)
                {
                    Marshal.FreeHGlobal((IntPtr)dataOut.pbData);
                }
            }
        }

        public byte[] Unprotect(byte[] protectedData)
        {
            fixed (byte* pbProtectedData = protectedData)
            {
                    return UnprotectWithDpapi(pbProtectedData, (uint)protectedData.Length);
            }
        }

        private byte[] UnprotectWithDpapi(byte* pbProtectedData, uint cbProtectedData)
        {
            byte dummy; // provides a valid memory address if the secret or entropy has zero length

            var dataIn = new DATA_BLOB()
            {
                cbData = cbProtectedData,
                pbData = (pbProtectedData != null) ? pbProtectedData : &dummy
            };
            var dataOut = default(DATA_BLOB);
            

            RuntimeHelpers.PrepareConstrainedRegions();

            try
            {
                var success = Crypt32.CryptUnprotectData(
                    pDataIn: &dataIn,
                    ppszDataDescr: IntPtr.Zero,
                    pOptionalEntropy: IntPtr.Zero,
                    pvReserved: IntPtr.Zero,
                    pPromptStruct: IntPtr.Zero,
                    dwFlags: CRYPTPROTECT_UI_FORBIDDEN,
                    pDataOut: out dataOut);
                if (!success)
                {
                    var errorCode = Marshal.GetLastWin32Error();
                    throw new CryptographicException(errorCode);
                }

                var dataLength = checked((int)dataOut.cbData);
                var retVal = new byte[dataLength];
                Marshal.Copy((IntPtr)dataOut.pbData, retVal, 0, dataLength);
                return retVal;
            }
            finally
            {
                // Zero and free memory so that we don't leak secrets.
                // FreeHGlobal actually calls LocalFree.
                if (dataOut.pbData != null)
                {
                    UnsafeBufferUtil.SecureZeroMemory(dataOut.pbData, dataOut.cbData);
                    Marshal.FreeHGlobal((IntPtr)dataOut.pbData);
                }
            }
        }
    }
}
