using System;
using System.Collections.Generic;
using System.Text;

namespace FS.SOC.Public
{
    public class Math
    {
        public static decimal Round(decimal value,uint decimals)
        {
            return decimal.Parse(value.ToString("F" + decimals.ToString()));
        }
    }
}
