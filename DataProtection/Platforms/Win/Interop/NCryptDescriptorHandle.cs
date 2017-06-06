using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

namespace DataProtection.Platforms.Win.Interop
{
    // Represents a protection descriptor handle by DPAPI:NG
    internal sealed class NCryptProtectionDescriptorHandle : SafeHandle
    {
        // Called by P/Invoke when returning SafeHandles
        private NCryptProtectionDescriptorHandle() : base(IntPtr.Zero, ownsHandle: true) { }

        // Do not provide a finalizer - SafeHandle's critical finalizer will
        // call ReleaseHandle for you.

        public override bool IsInvalid
        {
            get { return handle == IntPtr.Zero; }
        }

        public static NCryptProtectionDescriptorHandle Create(string protectionDescriptor)
        {
            NCryptProtectionDescriptorHandle descriptorHandle;
            int status = NCrypt.NCryptCreateProtectionDescriptor(protectionDescriptor, 0, out descriptorHandle);
            if (status != 0)
                throw new CryptographicException(status);
            return descriptorHandle;
        }

        protected override bool ReleaseHandle()
        {
            int retVal = NCrypt.NCryptCloseProtectionDescriptor(handle);
            return (retVal == 0);
        }
    }
}