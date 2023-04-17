using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neusoft.SOC.Local.Pharmacy.Print.ZDLY
{
    public class Function
    {
        public static string GetCostDecimalString()
        {
            try
            {
                Extend.ZDLY.BizExtendInterfaceImplement bizExtendInterfaceImplement = new Neusoft.SOC.Local.Pharmacy.Extend.ZDLY.BizExtendInterfaceImplement();

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
                Extend.ZDLY.BizExtendInterfaceImplement bizExtendInterfaceImplement = new Neusoft.SOC.Local.Pharmacy.Extend.ZDLY.BizExtendInterfaceImplement();

                uint costDecimal = bizExtendInterfaceImplement.GetCostDecimals("", "", "");

                return (int)costDecimal;
            }
            catch
            {
                return 2;
            }
        }

        public static string GetQtyDecimalString()
        {
            return "F2";
        }

        public static int GetQtyDecimal()
        {
            return 2;
        }

        public static string GetPriceDecimalString()
        {
            return "F2";
        }

        public static int GetPriceDecimal()
        {
            return 2;
        }
    }
}
