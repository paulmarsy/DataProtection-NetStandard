using System;
using System.Collections.Generic;
using System.Text;

namespace DataProtection.Abstractions
{
    public interface IVault
    {
        bool Exists(string id);
        byte[] Get(string id);
        void Put(string id, byte[] blob);
        void Delete(string id);
    }
}
