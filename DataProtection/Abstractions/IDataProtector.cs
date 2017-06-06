using System;
using System.Collections.Generic;
using System.Text;

namespace DataProtection.Abstractions
{
    public interface IDataProtector
    {
        byte[] Protect(byte[] plaintext);

        byte[] Unprotect(byte[] protectedData);
    }
}
