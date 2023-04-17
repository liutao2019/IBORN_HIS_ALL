using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neusoft.SOC.Local.EMR.ZDLY.Class
{
    public class PacsClass
    {
        public PacsClass()
        { 
        }

        /// <summary>
        /// 华海系统的webservice地址
        /// </summary>
        private static string webServiceUrl = string.Empty;

        /// <summary>
        /// 常熟业务层
        /// </summary>
        private static Neusoft.HISFC.BizProcess.Integrate.Common.ControlParam controlParamMgr =
            new Neusoft.HISFC.BizProcess.Integrate.Common.ControlParam();

        /// <summary>
        /// 调用华海pacs查看影像结果
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public static int HuaHaiPacs(string inpatientNo, string errMsg)
        {
            try
            {
                webServiceUrl = controlParamMgr.GetControlParam<string>("HUAHAI");
                string[] strarr = new string[2];
                strarr[0] = "H";
                strarr[1] = inpatientNo;

                GetService.InvokeWebservice(webServiceUrl, "ConnectHISServiceClient", "InputParameter", strarr);
                return 1;
            }
            catch(Exception ex)
            {
                errMsg = ex.Message;
                return -1;
            }
        }
    }
}
