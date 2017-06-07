using System;
using System.Collections.Generic;
using System.Text;
using DataProtection.Abstractions;
using DataProtection.Platforms.Mac.Interop;
using System.Runtime.InteropServices;
using System.Security;

namespace DataProtection.Platforms.Mac
{
    public class KeyChainVault : IVault
    {
        public bool Exists(string id)
        {
            using (var keychainItem = KeyChainItem.FindGenericPassword(id))
            {
                return keychainItem.ItemRef != IntPtr.Zero;
            }
        }

        public void Delete(string id)
        {
            using (var keychainItem = KeyChainItem.FindGenericPassword(id))
            {
                if (keychainItem.ItemRef == IntPtr.Zero)
                    throw new KeyNotFoundException();

                var status = Security.SecKeychainItemDelete(keychainItem.ItemRef);
                if (status != SecStatusCode.Success)
                    throw new SecurityException(status.ToString());
            }
        }

        public byte[] Get(string id)
        {
            using (var keychainItem = KeyChainItem.FindGenericPassword(id))
            {
                if (keychainItem.ItemRef == IntPtr.Zero)
                    throw new KeyNotFoundException();

                var buffer = new byte[keychainItem.PasswordLength];
                Marshal.Copy(keychainItem.PasswordData, buffer, 0, buffer.Length);
                return buffer;
            }
        }

        public void Put(string id, byte[] buffer)
        {
            using (var keychainItem = KeyChainItem.FindGenericPassword(id))
            {
                var status = keychainItem.ItemRef == IntPtr.Zero
                    ? Security.SecKeychainAddGenericPassword(
                        IntPtr.Zero,
                        (uint) id.Length,
                        id,
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