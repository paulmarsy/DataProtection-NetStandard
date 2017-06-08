using System.Management.Automation;

namespace DataProtection.PowerShell
{
    [Cmdlet(VerbsCommon.Get, nameof(ProtectedData))]
    public class GetProtectedData : BaseProtectedDataCmdlet
    {
        protected override void ProcesInput(string key)
        {
            WriteObject(ProtectedData.GetString(key));
        }
    }
}