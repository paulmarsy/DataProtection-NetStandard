using System;
using System.Collections.Generic;
using System.Text;
using DataProtection.Abstractions;

namespace DataProtection.Platforms.Managed
{
    public class ManagedDataProtector : IDataProtector
    {
   public     byte[] Protect(byte[] plaintext)
        {
            return null;
        }

        public byte[] Unprotect(byte[] protectedData)
        {
            return null;
        }
    }
}
