using System.Management.Automation;

namespace DataProtection.PowerShell
{
    public abstract class BaseDataProtectionCmdlet : PSCmdlet
    {
        [Parameter(ValueFromPipeline = true, Position = 0)]
        public string InputObject { get; set; }

        protected override void ProcessRecord()
        {
            if (string.IsNullOrWhiteSpace(InputObject))
                return;

            WriteObject(ProcesInput(InputObject));
        }

        protected abstract string ProcesInput(string inputObject);
    }
}