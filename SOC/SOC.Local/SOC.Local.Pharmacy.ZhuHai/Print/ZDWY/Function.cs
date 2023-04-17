using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.Local.Pharmacy.ZhuHai.Print.ZDWY
{
    public class Function
    {
        public static string GetCostDecimalString()
        {
            try
            {
                Extend.ZDWY.BizExtendInterfaceImplement bizExtendInterfaceImplement = new FS.SOC.Local.Pharmacy.ZhuHai.Extend.ZDWY.BizExtendInterfaceImplement();

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
                Extend.ZDWY.BizExtendInterfaceImplement bizExtendInterfaceImplement = new FS.SOC.Local.Pharmacy.ZhuHai.Extend.ZDWY.BizExtendInterfaceImplement();

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
