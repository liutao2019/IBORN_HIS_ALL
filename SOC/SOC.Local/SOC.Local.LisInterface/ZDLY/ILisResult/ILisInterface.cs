using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SOC.Local.LisInterface.ZDLY.ILisResult
{
    /// <summary>
    /// 中大六院LIS结果查询
    /// </summary>
    class ILisInterface : FS.HISFC.BizProcess.Interface.Common.ILis
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
