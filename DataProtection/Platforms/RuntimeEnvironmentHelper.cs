using System;
using System.Collections.Generic;
using System.Text;

namespace DataProtection
{
    public static class RuntimeEnvironmentHelper
    {
        public static string AccountName => Environment.UserName;
    }
}
