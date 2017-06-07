using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace DataProtection.Platforms.Mac.Interop
{
  internal static  class CoreFoundation
    {
        private const string CoreFoundationLib = "/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation";
        [DllImport(CoreFoundationLib, EntryPoint = "CFRelease")]
       private static extern void CFReleaseInternal(IntPtr cfRef);

      internal  static void CFRelease(IntPtr cfRef)
        {
            if (cfRef != IntPtr.Zero)
                CFReleaseInternal(cfRef);
        }
    }
}
