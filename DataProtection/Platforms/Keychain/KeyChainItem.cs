﻿using System;
using System.Security;
using DataProtection.Platforms.Mac.Interop;

namespace DataProtection.Platforms.Mac
{
    public class KeyChainItem : IDisposable
    {
        public IntPtr ItemRef;
        public IntPtr PasswordData;
        public uint PasswordLength;

        private KeyChainItem(uint passwordLength, IntPtr passwordData, IntPtr itemRef)
        {
            PasswordLength = passwordLength;
            PasswordData = passwordData;
            ItemRef = itemRef;
        }

        public void Dispose()
        {
            if (PasswordData != IntPtr.Zero)
                Security.SecKeychainItemFreeContent(IntPtr.Zero, PasswordData);

            if (ItemRef != IntPtr.Zero)
                CoreFoundation.CFRelease(ItemRef);
        }

        internal static KeyChainItem FindGenericPassword(string id)
        {
            var itemRef = IntPtr.Zero;
            var status = Security.SecKeychainFindGenericPassword(
                IntPtr.Zero,
                (uint) id.Length,
                id,
                (uint) RuntimeEnvironmentHelper.AccountName.Length,
                RuntimeEnvironmentHelper.AccountName,
                out uint passwordLength,
                out IntPtr passwordData,
                ref itemRef);
            if (status != SecStatusCode.Success && status != SecStatusCode.ItemNotFound)
                throw new SecurityException(status.ToString());

            return new KeyChainItem(passwordLength, passwordData, itemRef);
        }
    }
}