using System;
using System.Management.Automation;

namespace DataProtection.PowerShell
{
    [Cmdlet(VerbsSecurity.Protect, nameof(DataProtection))]
    public class ProtectDataProtection : PSCmdlet
    {
        [Parameter(ValueFromPipeline = true, Position = 0)]
        public string InputObject { get; set; }

        protected override void ProcessRecord()
        {
            if (string.IsNullOrWhiteSpace(InputObject))
                return;
        }
    }
}
