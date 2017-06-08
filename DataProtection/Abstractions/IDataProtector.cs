namespace DataProtection.Abstractions
{
    public interface IDataProtector
    {
        byte[] Protect(byte[] plaintext);

        byte[] Unprotect(byte[] protectedData);
    }
}