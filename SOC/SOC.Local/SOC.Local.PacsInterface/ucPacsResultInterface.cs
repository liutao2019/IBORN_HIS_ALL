using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace SOC.Local.PacsInterface
{
    /// <summary>
    /// 东软PACS结果查询接口
    /// </summary>
    class ucPacsResultInterface : FS.HISFC.BizProcess.Interface.Common.IPacs
    {
        /// <summary>
        /// 初始化joint.dll
        /// </summary>
        /// <returns>0 失败 , 1 成功</returns>
        [DllImport("joint.dll")]
        public static extern int PacsInitialize();

        /// <summary>
        /// 断开PACS连接
        /// </summary>
        [DllImport("joint.dll")]
        public static extern void PacsRelease();

        /// <summary>
        /// 报告显示
        /// </summary>
        /// <param name="patientType">患者编号类型 ， 1 门诊 ， 2 住院 ， 3 处方号 ， 4社保号</param>
        /// <param name="cardNO"> 患者编号 门诊是cardNO， 住院是patientNO</param>
        /// <param name="imageType">图像类型， 1 图像 ， 2 报告</param>
        /// <returns>0 失败 , 1 成功 </returns>
        [DllImport("joint.dll")]
        public static extern int PacsView(int patientType, StringBuilder cardNO, int imageType);


        #region IPacs 成员

        public bool CheckOrder(FS.HISFC.Models.Order.Order Order)
        {
            return true;
        }

        public int Commit()
        {
            return 1;
        }

        public int Connect()
        {
            PacsInitialize();
            return 1;
        }

        public int Disconnect()
        {
            PacsRelease();
            return 1;
        }

        string errCode = "";

        string errMsg = "";

        public string ErrCode
        {
            get
            {
                return errCode;
            }
            set
            {
                errCode = value;
            }
        }

        public string ErrMsg
        {
            get
            {
                return errMsg;
            }
            set
            {
                errMsg = value;
            }
        }

        string resultType = "";
        public string ResultType
        {
            get
            {
                return resultType;
            }
            set
            {
                resultType = value;
            }
        }

        public bool IsReportValid(string id)
        {
            return true;
        }

        /// <summary>
        /// 患者编号类型 ， 1 门诊 ， 2 住院 ， 3 处方号 ， 4社保号
        /// </summary>
        string operationMode = "1";

        /// <summary>
        /// 患者编号类型 ， 1 门诊 ， 2 住院 ， 3 处方号 ， 4社保号
        /// </summary>
        public string OprationMode
        {
            get
            {
                return operationMode;
            }
            set
            {
                operationMode = value;
            }
        }

        /// <summary>
        /// 图像类型， 1 图像 ， 2 报告
        /// </summary>
        string pacsViewType = "2";

        /// <summary>
        /// 图像类型， 1 图像 ， 2 报告
        /// </summary>
        public string PacsViewType
        {
            get
            {
                return pacsViewType;
            }
            set
            {
                pacsViewType = value;
            }
        }

        public int PlaceOrder(List<FS.HISFC.Models.Order.Order> OrderList)
        {
            return 1;
        }

        public int PlaceOrder(FS.HISFC.Models.Order.Order Order)
        {
            return 1;
        }

        public string[] QueryResult()
        {
            return null;
        }

        public int Rollback()
        {
            return 1;
        }

        /// <summary>
        /// 当前患者
        /// </summary>
        FS.HISFC.Models.RADT.Patient myPatient = null;

        public int SetPatient(FS.HISFC.Models.RADT.Patient Patient)
        {
            myPatient = Patient;
            return 1;
        }

        public void SetTrans(System.Data.IDbTransaction t)
        {
            return;
        }

        public void ShowForm()
        {
            return;
        }

        public int ShowResult(string id)
        {
            return 1;
        }

        public int ShowResultByPatient()
        {
            if (myPatient == null)
            {
                errMsg = "患者信息为空！";
                return 0;
            }

            //#region Kill掉Pacs 进程 Pacs 自己不关进程

            //string s = "Display";
            //System.Diagnostics.Process[] proc = System.Diagnostics.Process.GetProcessesByName(s);
            //if (proc.Length > 0)
            //{
            //    for (int i = 0; i < proc.Length; i++)
            //    {
            //        proc[i].Kill();
            //    }
            //}
            //#endregion

            StringBuilder patientNo = new StringBuilder();

            if (this.operationMode == "1")
            {
                patientNo.Append(myPatient.PID.CardNO);
            }
            else if (operationMode == "2")
            {
                patientNo.Append(myPatient.ID);
            }
            else
            {
                return 1;
            }

            return PacsView(FS.FrameWork.Function.NConvert.ToInt32(this.operationMode), patientNo, FS.FrameWork.Function.NConvert.ToInt32(this.pacsViewType));
        }

        #endregion
    }
}
