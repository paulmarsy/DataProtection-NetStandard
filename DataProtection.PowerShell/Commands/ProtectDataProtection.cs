using System.Management.Automation;

namespace DataProtection.PowerShell
{
    [Cmdlet(VerbsSecurity.Protect, nameof(DataProtection))]
    public class ProtectDataProtection : BaseDataProtectionCmdlet
    {
        protected override string ProcesInput(string inputObject)
        {
            return ProtectedData.Protect(inputObject);
        }
    }
}