using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Xml;

namespace Neusoft.SOC.Local.Order.OutPatientOrder.ZDLY
{
    class LisInterface : Neusoft.HISFC.BizProcess.Interface.Common.ILis
    {
        /// <summary>
        /// 患者类别
        /// </summary>
        private Neusoft.HISFC.Models.RADT.EnumPatientType patientType = Neusoft.HISFC.Models.RADT.EnumPatientType.O;

        private string errMsg = "";

        #region ILis 成员

        public bool CheckOrder(Neusoft.HISFC.Models.Order.Order order)
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

        public bool IsReportValid(string id)
        {
            return false;
        }

        public int PlaceOrder(ICollection<Neusoft.HISFC.Models.Order.Order> orders)
        {
            return 1;
        }

        public int PlaceOrder(Neusoft.HISFC.Models.Order.Order order)
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
            try
            {
                //string loadPath = "";
                //if (System.IO.File.Exists(Application.StartupPath + "\\LisInterface.xml"))
                //{
                //    XmlDocument file = new XmlDocument();
                //    file.Load(Application.StartupPath + "\\LisInterface.xml");
                //    XmlNode node = file.SelectSingleNode("Config/Path");
                //    if (node != null)
                //    {
                //        loadPath = node.InnerText;
                //    }
                //}

                //if (string.IsNullOrEmpty(loadPath))
                //{
                //    this.errMsg = "请维护LIS程序的启动目录，配置文件为根目录“LisInteface.xml”";
                //    return -1;
                //}
                //System.Reflection.Assembly asm = System.Reflection.Assembly.LoadFrom(Application.StartupPath + loadPath);//"PatientEls.dll");

                //Type[] t = asm.GetTypes();
                //foreach (Type tmp in t)
                //{
                //    if (tmp.Name == "HisExe")
                //    {
                //        object o = asm.GetType();
                //        System.Reflection.MethodInfo method = tmp.GetMethod("Show");
                //        object[] parms = new object[] { myPatientInfo.PID.CardNO };
                //        method.Invoke(o, parms);
                //    }
                //}

                ////PatientEls.HisExe.Show(myPatientInfo.PID.CardNO);
 

                switch (ResultType)
                {
                    case "1":
                        



                        break;
                    case "2":
                       



                        break;
                    default:
                        break;
                }
            }
            catch(Exception ex)
            {
                string err = ex.Message;
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 患者类别
        /// </summary>
        public Neusoft.HISFC.Models.RADT.EnumPatientType PatientType
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
        /// 结果类型
        /// </summary>
        string resultType;

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

        /// <summary>
        /// 当前患者
        /// </summary>
        Neusoft.HISFC.Models.RADT.Patient myPatientInfo = null;

        public int SetPatient(Neusoft.HISFC.Models.RADT.Patient patient)
        {
            myPatientInfo = patient;
            return 1;
        }

        #endregion
    }
}
