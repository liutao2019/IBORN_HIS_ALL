using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.Local.Pharmacy.Print.HuanShi
{
    public class Function
    {
        public static string GetCostDecimalString()
        {
            try
            {
                Extend.NanZhuang.BizExtendInterfaceImplement bizExtendInterfaceImplement = new FS.SOC.Local.Pharmacy.Extend.NanZhuang.BizExtendInterfaceImplement();

                uint costDecimal = bizExtendInterfaceImplement.GetCostDecimals("", "", "");

                return "F" + costDecimal.ToString("F0");
            }
            catch
            {
                return "F2";
            }
        }

        public static int GetCostDecimal()
        {
            try
            {
                Extend.NanZhuang.BizExtendInterfaceImplement bizExtendInterfaceImplement = new FS.SOC.Local.Pharmacy.Extend.NanZhuang.BizExtendInterfaceImplement();

                uint costDecimal = bizExtendInterfaceImplement.GetCostDecimals("", "", "");

                return (int)costDecimal;
            }
            catch
            {
                return 2;
            }
        }
    }
}
