using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neusoft.SOC.HISFC.Fee.BizProcess
{
    public class ControlParam
    {
        public ControlParam()
        {
        }

        public static String GetValue(string controlCode)
        {
            return GetValue(controlCode, false);
        }

        public static virtual String GetValue(string controlCode, bool isRefresh)
        {
            Neusoft.FrameWork.Management.ControlParam controlParamManager = new Neusoft.FrameWork.Management.ControlParam();
            return controlParamManager.QueryControlerInfo(controlCode,isRefresh);
        }

        public static int GetInt32(string controlCode)
        {
            return GetInt32(controlCode,false);
        }

        public static int GetInt32(string controlCode, bool isRefresh)
        {
            return Neusoft.FrameWork.Function.NConvert.ToInt32(GetValue(controlCode,isRefresh));
        }

        public static string GetString(string controlCode)
        {
            return GetString(controlCode,false);
        }

        public static string GetString(string controlCode, bool isRefresh)
        {
            return GetValue(controlCode,isRefresh);
        }

        public static bool GetBoolean(string controlCode)
        {
            return GetBoolean(controlCode,false);
        }

        public static bool GetBoolean(string controlCode, bool isRefresh)
        {
            return Neusoft.FrameWork.Function.NConvert.ToBoolean(GetValue(controlCode,isRefresh));
        }
    }
}
