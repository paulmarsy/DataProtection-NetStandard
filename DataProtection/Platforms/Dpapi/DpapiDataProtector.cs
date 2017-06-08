using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using DataProtection.Abstractions;
using DataProtection.Interop.Win;
using DataProtection.Platforms.Win.Interop;

namespace DataProtection.Platforms.Win
{
    public unsafe class DpapiDataProtector : IDataProtector
    {
        public byte[] Protect(byte[] plaintext)
        {
            fixed (byte* pbPlaintextSecret = plaintext)
            {
                return ProtectWithDpapi(pbPlaintextSecret, (uint) plaintext.Length, false);
            }
        }

        public byte[] Unprotect(byte[] protectedData)
        {
            fixed (byte* pbProtectedData = protectedData)
            {
                return UnprotectWithDpapi(pbProtectedData, (uint) protectedData.Length);
            }
        }

        private byte[] ProtectWithDpapi(byte* pbSecret, uint cbSecret, bool fLocalMachine = false)
        {
            byte dummy; // provides a valid memory address if the secret or entropy has zero length

            var dataIn = new DATA_BLOB
            {
                cbData = cbSecret,
                pbData = pbSecret != null ? pbSecret : &dummy
            };
            var dataOut = default(DATA_BLOB);

            RuntimeHelpers.PrepareConstrainedRegions();

            try
            {
                var success = Crypt32.CryptProtectData(
                    &dataIn,
                    IntPtr.Zero,
                    IntPtr.Zero,
                    IntPtr.Zero,
                    IntPtr.Zero,
                    Crypt32.CRYPTPROTECT_UI_FORBIDDEN | (fLocalMachine ? Crypt32.CRYPTPROTECT_LOCAL_MACHINE : 0),
                    out dataOut);
                if (!success)
                    throw new CryptographicException(Marshal.GetLastWin32Error());

                var dataLength = checked((int) dataOut.cbData);
                var buffer = new byte[dataLength];
                Marshal.Copy((IntPtr) dataOut.pbData, buffer, 0, dataLength);
                return buffer;
            }
            finally
            {
                if (dataOut.pbData != null)
                    Marshal.FreeHGlobal((IntPtr) dataOut.pbData);
            }
        }

        private byte[] UnprotectWithDpapi(byte* pbProtectedData, uint cbProtectedData)
        {
            byte dummy; // provides a valid memory address if the secret or entropy has zero length

            var dataIn = new DATA_BLOB
            {
                cbData = cbProtectedData,
                pbData = pbProtectedData != null ? pbProtectedData : &dummy
            };
            var dataOut = default(DATA_BLOB);

            RuntimeHelpers.PrepareConstrainedRegions();

            try
            {
                var success = Crypt32.CryptUnprotectData(
                    &dataIn,
                    IntPtr.Zero,
                    IntPtr.Zero,
                    IntPtr.Zero,
                    IntPtr.Zero,
                    Crypt32.CRYPTPROTECT_UI_FORBIDDEN,
                    out dataOut);
                if (!success)
                    throw new CryptographicException(Marshal.GetLastWin32Error());

                var dataLength = checked((int) dataOut.cbData);
                var buffer = new byte[dataLength];
                Marshal.Copy((IntPtr) dataOut.pbData, buffer, 0, dataLength);
                return buffer;
            }
            finally
            {
                if (dataOut.pbData != null)
                    Marshal.FreeHGlobal((IntPtr) dataOut.pbData);
            }
        }
    }
}