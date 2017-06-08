using System.Management.Automation;

namespace DataProtection.PowerShell
{
    [Cmdlet(VerbsCommon.Add, nameof(ProtectedData))]
    public class AddProtectedData : BaseProtectedDataCmdlet
    {
        [Parameter(ValueFromPipeline = true, Position = 0)]
        public string InputObject { get; set; }

        protected override void ProcesInput(string key)
        {
            ProtectedData.Put(key, (string) InputObject);
        }
    }
}