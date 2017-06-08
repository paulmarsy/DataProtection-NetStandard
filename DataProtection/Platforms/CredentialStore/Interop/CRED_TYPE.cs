namespace DataProtection.Platforms.CredentialStore.Interop
{
    internal enum CRED_TYPE : uint
    {
        CRED_TYPE_GENERIC = 1,
        CRED_TYPE_DOMAIN_PASSWORD = 2,
        CRED_TYPE_DOMAIN_CERTIFICATE = 3,
        CRED_TYPE_DOMAIN_VISIBLE_PASSWORD = 4,
        CRED_TYPE_GENERIC_CERTIFICATE = 5,
        CRED_TYPE_DOMAIN_EXTENDED = 6,
        CRED_TYPE_MAXIMUM = 7,
        CRED_TYPE_MAXIMUM_EX = CRED_TYPE_MAXIMUM + 1000
    }
}