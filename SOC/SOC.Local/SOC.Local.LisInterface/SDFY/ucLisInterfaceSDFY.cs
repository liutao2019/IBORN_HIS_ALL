using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;

namespace SOC.Local.LisInterface.SDFY
{
    /// <summary>
    /// 顺德妇幼 杜峰科技 LIS接口实现
    /// </summary>
    class ucLisInterfaceSDFY : FS.HISFC.BizProcess.Interface.Common.ILis
    {
        #region ILis 成员

        public bool CheckOrder(FS.HISFC.Models.Order.Order order)
        {
            return true;
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

        string errCode = string.Empty;
        public string ErrCode
        {
            get
            {
                return this.errCode;
            }
        }

        string errMsg = string.Empty;
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
            return true;
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

        public void SetTrans(IDbTransaction t)
        {

        }

        public int ShowResult(string id)
        {
            return 1;
        }

        public int ShowResultByPatient()
        {
            if (lisResultShow.ShowResultByPatient() == -1)
            {
                this.errMsg = lisResultShow.ErrMsg;
                return -1;
            }
            //lisResultShow.ShowDialog();
            lisResultShow.Show();

            return 1;
        }

        private frmLisResultShow lisResultShow = null;

        FS.HISFC.Models.RADT.Patient myPatient = null;

        public int SetPatient(FS.HISFC.Models.RADT.Patient patient)
        {
            myPatient = patient;

            if (lisResultShow == null || lisResultShow.IsDisposed)
            {
                lisResultShow = new frmLisResultShow();
            }

            if (lisResultShow.SetPatient(patient) == -1)
            {
                this.errMsg = lisResultShow.ErrMsg;
                return -1;
            }
            return 1;
        }

        FS.HISFC.Models.RADT.EnumPatientType patientType = FS.HISFC.Models.RADT.EnumPatientType.C;

        public FS.HISFC.Models.RADT.EnumPatientType PatientType
        {
            get
            {
                return patientType;
            }
            set
            {
                patientType = value;
            }
        }

        #endregion
    }
}
