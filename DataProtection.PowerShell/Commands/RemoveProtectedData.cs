using System.Management.Automation;

namespace DataProtection.PowerShell
{
    [Cmdlet(VerbsCommon.Remove, nameof(ProtectedData))]
    public class RemoveProtectedData : BaseProtectedDataCmdlet
    {
        protected override void ProcesInput(string key)
        {
            ProtectedData.Delete(key);
        }
    }
}