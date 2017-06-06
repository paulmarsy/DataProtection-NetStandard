using System;
using System.Collections.Generic;
using System.Text;
using DataProtection.Abstractions;
using DataProtection.Platforms.Mac.Interop;
using System.Runtime.InteropServices;

namespace DataProtection.Platforms.Mac
{
    public class KeyChain : IVault
    {
        public bool Exists(string id)
        {
            throw new NotImplementedException();
        }

        public void Delete(string id)
        {

            IntPtr itemRef = IntPtr.Zero;
            IntPtr dataPtr = IntPtr.Zero;
            uint dataLen;
            try
            {
                var status = Security.SecKeychainFindGenericPassword(IntPtr.Zero, (uint)id.Length, id,
                    (uint)Environment.UserName.Length, Environment.UserName,
                    out dataLen, out dataPtr, ref itemRef);
                if (itemRef != IntPtr.Zero)
                {
                    status = Security.SecKeychainItemDelete(itemRef);

                }
            }
            finally
            {
                // Don't forget to free up memory
                if (dataPtr != IntPtr.Zero)
                    Security.SecKeychainItemFreeContent(IntPtr.Zero, dataPtr);

                if (itemRef != IntPtr.Zero)
                    CoreFoundation.CFRelease(itemRef);
            }
        }

        public byte[] Get(string id)
        {
            byte[] secret = null;

            IntPtr itemRef = IntPtr.Zero;
            IntPtr dataPtr = IntPtr.Zero;
            uint dataLen;
            try
            {
                 var status = Security.SecKeychainFindGenericPassword(IntPtr.Zero, (uint)id.Length, id,
                    (uint)Environment.UserName.Length, Environment.UserName,
                    out dataLen, out dataPtr, ref itemRef);

                if (itemRef != IntPtr.Zero)
                {
                    secret = new byte[dataLen];
                    Marshal.Copy(dataPtr, secret, 0, secret.Length);
                }
            }
            finally
            {
                // Don't forget to free up memory
                if (dataPtr != IntPtr.Zero)
                   Security.SecKeychainItemFreeContent(IntPtr.Zero, dataPtr);

                if (itemRef != IntPtr.Zero)
                    CoreFoundation.CFRelease(itemRef);
            }

            return secret;
        }
        


        public void Put(string id, byte[] buffer)
        {
            IntPtr itemRef = IntPtr.Zero;
            IntPtr dataPtr = IntPtr.Zero;
            uint dataLen = 0;
            string userName = Environment.UserName;

            try
            {

                var status = Security.SecKeychainFindGenericPassword(IntPtr.Zero, (uint)id.Length, id,
                    (uint)userName.Length, userName,
                    out dataLen, out dataPtr, ref itemRef);

                if (itemRef != IntPtr.Zero)
                {
                    // Update item
                    status = Security.SecKeychainItemModifyAttributesAndData(itemRef, IntPtr.Zero, (uint)buffer.Length, buffer);
                 }
                else
                {
                    // update item
                    // status: 0 = ok, -25299 = the item already exists
                    status = Security.SecKeychainAddGenericPassword(IntPtr.Zero, (uint)id.Length, id,
                        (uint)userName.Length, userName,
                        (uint)buffer.Length, buffer, ref itemRef);
                }
            }
            finally
            {
                // Don't forget to free up memory
                if (dataPtr != IntPtr.Zero)
                    Security.SecKeychainItemFreeContent(IntPtr.Zero, dataPtr);

                if (itemRef != IntPtr.Zero)
                    CoreFoundation.CFRelease(itemRef);
            }
        }
    }
}
