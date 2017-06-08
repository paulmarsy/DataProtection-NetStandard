using System;
using System.Runtime.InteropServices;

namespace DataProtection
{
    public static class RuntimeEnvironmentHelper
    {
        public static string AccountName => Environment.UserName;

        public static bool IsWindows()
        {
#if NETSTANDARD2_0
            return RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
#else
            return System.Environment.OSVersion.Platform == PlatformID.Win32NT
                   || System.Environment.OSVersion.Platform == PlatformID.Win32S
                   || System.Environment.OSVersion.Platform == PlatformID.Win32Windows;
#endif
        }

        public static bool IsOSX()
        {
#if NETSTANDARD2_0
            return RuntimeInformation.IsOSPlatform(OSPlatform.OSX);
#else
            return false;
#endif
        }
    }
}