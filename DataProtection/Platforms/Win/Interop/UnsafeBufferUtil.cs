using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading;

namespace DataProtection.Platforms.Win.Interop
{
    internal unsafe static class UnsafeBufferUtil
    {   /// <summary>
        /// Securely clears a memory buffer.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        public static void SecureZeroMemory(byte* buffer, uint byteCount)
        {
            if (byteCount != 0)
            {
                do
                {
                    buffer[--byteCount] = 0;
                } while (byteCount != 0);

                // Volatile to make sure the zero-writes don't get optimized away
                Volatile.Write(ref *buffer, 0);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        public static void BlockCopy(void* from, void* to, uint byteCount)
        {
            if (byteCount != 0)
            {
                BlockCopyCore((byte*)from, (byte*)to, byteCount);
            }
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void BlockCopyCore(byte* from, byte* to, ulong byteCount)
        {
            Buffer.MemoryCopy(from, to, byteCount, byteCount);
        }
    }
}
