namespace DataProtection.Abstractions
{
    public interface IVault
    {
        bool Exists(string key);
        byte[] Get(string key);
        void Put(string key, byte[] blob);
        void Delete(string key);
    }
}