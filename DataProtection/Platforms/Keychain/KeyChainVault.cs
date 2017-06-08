using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using DataProtection.Abstractions;
using DataProtection.Platforms.Mac.Interop;

namespace DataProtection.Platforms.Mac
{
    public class KeyChainVault : IVault
    {
        public bool Exists(string key)
        {
            using (var keychainItem = KeyChainItem.FindGenericPassword(key))
            {
                return keychainItem.ItemRef != IntPtr.Zero;
            }
        }

        public void Delete(string key)
        {
            using (var keychainItem = KeyChainItem.FindGenericPassword(key))
            {
                if (keychainItem.ItemRef == IntPtr.Zero)
                    throw new KeyNotFoundException();

                var status = Security.SecKeychainItemDelete(keychainItem.ItemRef);
                if (status != SecStatusCode.Success)
                    throw new SecurityException(status.ToString());
            }
        }

        public byte[] Get(string key)
        {
            using (var keychainItem = KeyChainItem.FindGenericPassword(key))
            {
                if (keychainItem.ItemRef == IntPtr.Zero)
                    throw new KeyNotFoundException();

                var buffer = new byte[keychainItem.PasswordLength];
                Marshal.Copy(keychainItem.PasswordData, buffer, 0, buffer.Length);
                return buffer;
            }
        }

        public void Put(string key, byte[] buffer)
        {
            using (var keychainItem = KeyChainItem.FindGenericPassword(key))
            {
                var status = keychainItem.ItemRef == IntPtr.Zero
                    ? Security.SecKeychainAddGenericPassword(
                        IntPtr.Zero,
                        (uint) key.Length,
                        key,
                        (uint) RuntimeEnvironmentHelper.AccountName.Length,
                        RuntimeEnvironmentHelper.AccountName,
                        (uint) buffer.Length,
                        buffer,
                        ref keychainItem.ItemRef)
                    : Security.SecKeychainItemModifyAttributesAndData(
                        keychainItem.ItemRef,
                        IntPtr.Zero,
                        (uint) buffer.Length,
                        buffer);
                if (status != SecStatusCode.Success)
                    throw new SecurityException(status.ToString());
            }
        }
    }
}