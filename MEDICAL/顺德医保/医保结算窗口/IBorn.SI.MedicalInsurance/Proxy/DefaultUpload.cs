using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBorn.SI.MedicalInsurance.FoShan.Proxy
{
    public class DefaultUpload : IBorn.SI.BI.IUpload
    {

        #region IUpload 成员

        public int DeleteFee<T>(string registerType, T register)
        {
            return 1;
        }

        public string ErrorMsg
        {
            get;
            set;
        }

        public List<T> GetNeedUploadFeeDetail<T>(string registerType, string registerID)
        {
            return null;
        }
             
        public int UploadFee<R, T>(string registerType, R register, T feeDetail)
        {
            return 1;
        }

        public System.Data.DataTable GetNeedUploadFeeDetail(string registerType, string registerID)
        {
            throw new NotImplementedException("接口没有实现");
        }

        

        public System.Data.DataTable GetNeedUploadFeeDetail(string registerType, string registerID, string invoiceNO)
        {
            throw new NotImplementedException();
        }

       
        public System.Data.DataTable GetNotUploadFeeDetail(string registerType, string registerID)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
