namespace DataProtection.Abstractions
{
    public interface IDataProtectionProvider
    {
        IDataProtector GetDataProtector();
        IVault GetVault();
    }
}