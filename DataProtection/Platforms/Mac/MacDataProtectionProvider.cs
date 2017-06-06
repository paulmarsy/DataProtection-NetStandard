using System;
using System.Collections.Generic;
using System.Text;
using DataProtection.Abstractions;

namespace DataProtection.Platforms.Mac
{
 public   class MacDataProtectionProvider: ManagedDataProtectionProvider
 {
     public override IVault GetVault() => new KeyChain();
 }
}
