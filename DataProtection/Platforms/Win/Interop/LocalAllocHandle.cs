using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace DataProtection.Platforms.Win.Interop
{
    /// <summary>
    /// Represents a handle returned by LocalAlloc.
    /// </summary>
    internal class LocalAllocHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        // Called by P/Invoke when returning SafeHandles
        protected LocalAllocHandle()
            : base(ownsHandle: true) { }

        // Do not provide a finalizer - SafeHandle's critical finalizer will call ReleaseHandle for you.
        protected override bool ReleaseHandle()
        {
            Marshal.FreeHGlobal(handle); // actually calls LocalFree
            return true;
        }
    }
}
