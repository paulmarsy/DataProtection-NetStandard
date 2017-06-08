using System.Management.Automation;

namespace DataProtection.PowerShell
{
    [Cmdlet(VerbsSecurity.Unprotect, nameof(DataProtection))]
    public class UnprotectDataProtection : BaseDataProtectionCmdlet
    {
        protected override string ProcesInput(string inputObject)
        {
            return ProtectedData.Unprotect(inputObject);
        }
    }
}