using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Xml;

namespace SOC.Local.LisInterface
{
    /// <summary>
    /// [功能描述: 医生站查看Lis结果]<br></br>
    /// [创 建 者: zhaoj]<br></br>
    /// [创建时间: 2009-07-23]<br></br>
    /// <修改记录
    ///		修改人=''
    ///		修改时间=''
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    public class QueryLisResult:FS.HISFC.BizProcess.Interface.Common.ILis
    {
        #region 域变量

        /// <summary>
        /// 患者类别
        /// </summary>
        private FS.HISFC.Models.RADT.EnumPatientType patientType = FS.HISFC.Models.RADT.EnumPatientType.C;

        private string errMsg = "";

        #endregion

        #region ILis 成员

        public bool CheckOrder(FS.HISFC.Models.Order.Order order)
        {
            return false;
        }

        public int Commit()
        {
            return 1;
        }

        public int Connect()
        {
            return 1;
        }

        public int Disconnect()
        {
            return 1;
        }

        public string ErrCode
        {
            get
            {
                return "";
            }
        }
        
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrMsg
        {
            get
            {
                return this.errMsg;
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
            return false;
        }

        public int PlaceOrder(ICollection<FS.HISFC.Models.Order.Order> orders)
        {
            return 1;
        }

        public int PlaceOrder(FS.HISFC.Models.Order.Order order)
        {
            return 1;
        }

        public string[] QueryResult()
        {
            return new string[] { };
        }

        public int Rollback()
        {
            return 1;
        }

        public void SetTrans(System.Data.IDbTransaction t)
        {
            return;
        }

        public int ShowResult(string id)
        {
            return 1;
        }

        /// <summary>
        /// 显示Lis结果
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <returns></returns>
        public int ShowResultByPatient()
        {
            string inpatientNo = myPatientInfo.PID.CardNO;

            string arguments = "";

            if (this.PatientType == FS.HISFC.Models.RADT.EnumPatientType.C)
            {
                if (string.IsNullOrEmpty(inpatientNo))
                {
                    this.errMsg = "门诊卡号不能为空！";
                    return -1;
                }
                arguments = inpatientNo + " " + "门诊";
            }
            else if (this.PatientType == FS.HISFC.Models.RADT.EnumPatientType.I)
            {
                if (string.IsNullOrEmpty(inpatientNo))
                {
                    this.errMsg = "住院号不能为空！";
                    return -1;
                }
                arguments = myPatientInfo.PID .PatientNO+ " " + "住院";
            }

            string loadPath = "";
            string process = "";
            if (System.IO.File.Exists(Application.StartupPath + "\\LisInterface.xml"))
            {
                XmlDocument file = new XmlDocument();
                file.Load(Application.StartupPath + "\\LisInterface.xml");
                XmlNode node = file.SelectSingleNode("Config/StartPath");
                if (node != null)
                {
                    loadPath = node.InnerText;
                }
                node = file.SelectSingleNode("Config/Process");
                if (node != null)
                {
                    process = node.InnerText;
                }
            }

            if (string.IsNullOrEmpty(loadPath + process))
            {
                this.errMsg = "请维护LIS程序的启动目录，配置文件为根目录“LisInteface.xml”";
                return -1;
            }

            //读取路径
            if (System.IO.File.Exists(Application.StartupPath + loadPath + process))
            {
                try
                {
                    //System.Diagnostics.Process.Start(Application.StartupPath + loadPath, arguments);

                    System.Diagnostics.Process pro = new System.Diagnostics.Process();
                    pro.StartInfo.WorkingDirectory = Application.StartupPath + loadPath;
                    System.Diagnostics.Process.Start(Application.StartupPath + loadPath, arguments);
                }
                catch (Exception e)
                {
                    this.errMsg = "调用Lis程序出错！" + e.Message;
                    return -1;
                }
            }
            else
            {
                this.errMsg = "不存在Lis启动程序！请按照在如下路径设置：根目录\"" + loadPath + "\"";
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 患者类别
        /// </summary>
        public FS.HISFC.Models.RADT.EnumPatientType PatientType
        {
            get
            {
                return this.patientType;
            }
            set
            {
                this.patientType = value;
            }
        }

        /// <summary>
        /// 当前患者
        /// </summary>
        FS.HISFC.Models.RADT.Patient myPatientInfo = null;

        public int SetPatient(FS.HISFC.Models.RADT.Patient patient)
        {
            myPatientInfo = patient;
            return 1;
        }

        #endregion
    }
}
