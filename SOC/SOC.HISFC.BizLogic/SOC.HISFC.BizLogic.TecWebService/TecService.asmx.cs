using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace SOC.HISFC.BizLogic.TecWebService
{
    /// <summary>
    /// TecService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class TecService : System.Web.Services.WebService
    {
        private FS.HISFC.BizLogic.Manager.DataBase dataManager = new FS.HISFC.BizLogic.Manager.DataBase();

        static TecService()
        {
            DataHelper.InitConnection();
        }

        string returnXml = @"<Response>
                           < ResultCode >{0}</ResultCode>
                           < ErrorMsg > {1}</ErrorMsg>
                            </Response>
                           ";



        [WebMethod]
        public string HelloWorld()
        {
            return dataManager.ExecSqlReturnOne("SELECT HOS_NAME FROM COM_HOSPITALINFO", "");

            return "Hello World";
        }


        /// <summary>
        /// 门诊终端确认
        /// </summary>
        /// <param name="type">0 门诊 1住院</param>
        /// <param name="patientID">门诊流水号CLINIC_CODE 住院流水号PATIENT_NO</param>
        /// <param name="orderID">医嘱流水号</param>
        /// <param name="state">回写状态: 0 取消确认；1 状态1（打印条码/已检查）；--2 状态2（已出报告）</param>
        /// <param name="sysType">系统类别 1：lis,2:超声,3:放射</param>
        /// <returns></returns>
        public string ConfirmApply(string type,string patientID,string orderID,string state,string sysType)
        {
            if (!string.IsNullOrEmpty(DataHelper.error))
            {
                return string.Format(returnXml, "0", DataHelper.error);
            }

          


            #region 门诊

            if (type == "0")
            {
                //门诊终端确认标记
                string confirmFlag = string.Empty;

                if (state == "0")
                {
                    confirmFlag = "0";
                }
                else
                {
                    confirmFlag = "1";
                }

                if (dataManager.ExecNoQueryByIndex("SOC.Terminal.OutPatient.Confirm", patientID, orderID, confirmFlag, state) < 0)
                {
                    return string.Format(returnXml, "0", dataManager.Err);
                }
                else 
                {
                    return string.Format(returnXml, "1", dataManager.Err);
                }




            }
            #endregion

            #region 住院
            else if (type == "1")
            {

            }
            #endregion

            return string.Empty;
        }



    }
}
