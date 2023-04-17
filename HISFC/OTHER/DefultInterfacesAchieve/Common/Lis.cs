using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Collections;

namespace Neusoft.DefultInterfacesAchieve.Common
{
    public class Lis : Neusoft.HISFC.BizProcess.Interface.Common.ILis
    {
        public void SetTrans(System.Data.IDbTransaction t)
        {
            throw new NotImplementedException();
        }

        public void ShowForm()
        {
            throw new NotImplementedException();
        }

        public int ShowResult(string id)
        {
            throw new NotImplementedException();
        }

        public int ShowResultByPatient()
        {
            //增加LIS接口查看结果代码
            return 1;
        }

        #region ILis 成员


        public int PlaceOrder(ICollection<Neusoft.HISFC.Models.Order.Order> orders)
        {
            throw new NotImplementedException();
        }

        public int SetPatient(Neusoft.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region ILis 成员

        public bool CheckOrder(Neusoft.HISFC.Models.Order.Order order)
        {
            throw new NotImplementedException();
        }

        public int Commit()
        {
            throw new NotImplementedException();
        }

        public int Connect()
        {
            throw new NotImplementedException();
        }

        public int Disconnect()
        {
            throw new NotImplementedException();
        }

        public string ErrCode
        {
            get { throw new NotImplementedException(); }
        }

        public string ErrMsg
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsReportValid(string id)
        {
            throw new NotImplementedException();
        }

        public int PlaceOrder(Neusoft.HISFC.Models.Order.Order order)
        {
            throw new NotImplementedException();
        }

        public string[] QueryResult()
        {
            throw new NotImplementedException();
        }

        public int Rollback()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region ILis 成员


        public int SetPatient(Neusoft.HISFC.Models.RADT.Patient patient)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region ILis 成员


        public Neusoft.HISFC.Models.RADT.EnumPatientType PatientType
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion
    }
}
