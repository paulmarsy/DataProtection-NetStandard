using System;
using System.Collections.Generic;
using System.Text;
using DataProtection.Abstractions;

namespace DataProtection.Platforms.Managed
{
    public class ManagedVault : IVault
    {
        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public bool Exists(string id)
        {
            throw new NotImplementedException();
        }

        public byte[] Get(string id)
        {
            throw new NotImplementedException();
        }

        public void Put(string id, byte[] blob)
        {
            throw new NotImplementedException();
        }
    }
}
