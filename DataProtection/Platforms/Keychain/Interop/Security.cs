﻿using System;
using System.Runtime.InteropServices;

namespace DataProtection.Platforms.Mac.Interop
{
    internal static class Security
    {
        private const string SecurityLib = "/System/Library/Frameworks/Security.framework/Security";

        [DllImport(SecurityLib)]
        internal static extern SecStatusCode SecKeychainAddGenericPassword(IntPtr keychain, uint serviceNameLength, string serviceName,
            uint accountNameLength, string accountName, uint passwordLength,
            byte[] passwordData, ref IntPtr itemRef);

        [DllImport(SecurityLib)]
        internal static extern SecStatusCode SecKeychainFindGenericPassword(IntPtr keychain, uint serviceNameLength, string serviceName,
            uint accountNameLength, string accountName, out uint passwordLength,
            out IntPtr passwordData, ref IntPtr itemRef);

        [DllImport(SecurityLib)]
        internal static extern SecStatusCode SecKeychainItemModifyAttributesAndData(IntPtr itemRef, IntPtr attrList, uint length, byte[] data);

        [DllImport(SecurityLib)]
        internal static extern SecStatusCode SecKeychainItemFreeContent(IntPtr attrList, IntPtr data);

        [DllImport(SecurityLib)]
        internal static extern SecStatusCode SecKeychainItemDelete(IntPtr itemRef);
    }
}