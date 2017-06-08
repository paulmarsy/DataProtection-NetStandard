using System.Management.Automation;

namespace DataProtection.PowerShell
{
    public abstract class BaseProtectedDataCmdlet : PSCmdlet
    {
        [Parameter(Position = 0)]
        public string Key { get; set; }

        protected override void ProcessRecord()
        {
            if (string.IsNullOrWhiteSpace(Key))
                return;

            ProcesInput(Key);
        }

        protected abstract void ProcesInput(string key);
    }
}