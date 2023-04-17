using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace IBorn.SI.GuangZhou
{
    /// <summary>
    /// 广州医保费用上传接口实现
    /// by 飞扬 2019-09-28
    /// </summary>
    public class MedicalUploadProcess : IBorn.SI.BI.IUpload
    {
        IBorn.SI.GuangZhou.SILocalManager myInterface = new IBorn.SI.GuangZhou.SILocalManager();

        #region IUpload 成员
        private string errorMsg;
        /// <summary>
        /// 错误提示信息
        /// </summary>
        public string ErrorMsg
        {
            get
            {
                return this.errorMsg;
            }
        }

        public int DeleteFee<T>(string registerType, T register)
        {
            throw new NotImplementedException();
        }

        public List<T> GetNeedUploadFeeDetail<T>(string registerType, string registerID)
        {
            if (string.IsNullOrEmpty(registerID) || string.IsNullOrEmpty(registerType))
            {
                this.errorMsg = "传参错误！";
                return null;
            }
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取有匹配但手工选择不上传医保的项目
        /// </summary>
        /// <param name="registerType"></param>
        /// <param name="registerID"></param>
        /// <returns></returns>
        public System.Data.DataTable GetNotUploadFeeDetail(string registerType, string registerID)
        {
            if (string.IsNullOrEmpty(registerID) || string.IsNullOrEmpty(registerType))
            {
                this.errorMsg = "传参错误！";
                return null;
            }
            if (registerType == "O")
            {
               //todo:门诊暂时没处理
            }
            else if (registerType == "I")
            {
                return myInterface.QueryInPatientNotUploadFeeDetail(registerID);
            }
            return null;
        }

        /// <summary>
        /// 获取要上传到医保的费用明细
        /// </summary>
        /// <param name="registerType"></param>
        /// <param name="registerID"></param>
        /// <returns></returns>
        public System.Data.DataTable GetNeedUploadFeeDetail(string registerType, string registerID)
        {
            if (string.IsNullOrEmpty(registerID) || string.IsNullOrEmpty(registerType))
            {
                this.errorMsg = "传参错误！";
                return null;
            }
            if (registerType == "O")
            {   
                //根据就诊号号来查找需要上传的费用明细
                return myInterface.QueryOutPatientNeedUploadFeeDetail(registerID);
            }
            else if (registerType == "I")
            {
                return myInterface.QueryInPatientNeedUploadFeeDetail(registerID);
            }
            return null;
        }

        /// <summary>
        /// 获取要上传到医保的费用明细
        /// </summary>
        /// <param name="registerType"></param>
        /// <param name="registerID"></param>
        /// <param name="invoiceNO"></param>
        /// <returns></returns>
        public System.Data.DataTable GetNeedUploadFeeDetail(string registerType, string registerID, string invoiceNO)
        {
            if (string.IsNullOrEmpty(registerID) || string.IsNullOrEmpty(registerType) || string.IsNullOrEmpty(invoiceNO))
            {
                this.errorMsg = "GetNeedUploadFeeDetail方法传参错误！";
                return null;
            }
            if (registerType == "O")
            {                      
                //根据发票号来查找需要上传的费用明细
                return myInterface.QueryOutPatientNeedUploadFeeDetail(registerID, invoiceNO);
            }
            else if (registerType == "I")
            {
                return myInterface.QueryInPatientNeedUploadFeeDetail(registerID, invoiceNO);
            }
            return null;
        }

        public int UploadFee<R, T>(string registerType, R register, List<T> feeDetail)
        {
            if (register == null || string.IsNullOrEmpty(registerType))
            {
                this.errorMsg = "传参错误！";
                return -1;
            }
            if (registerType == "O")
            {
                var reg = register as FS.HISFC.Models.Registration.Register;
                if (reg == null || string.IsNullOrEmpty(reg.ID))
                {
                    this.errorMsg = "没有获得患者基本信息!";
                    return -1;
                }
                var details = feeDetail as List<FS.HISFC.Models.Fee.Outpatient.FeeItemList>;
                if (details == null || details.Count == 0)
                {
                    this.errorMsg = "传入的费用明细为空!";
                    return -1;
                }
                IBorn.SI.GuangZhou.OutPatient.UpLoadFeeDetail uploadManager = new IBorn.SI.GuangZhou.OutPatient.UpLoadFeeDetail();
                string result = string.Empty;
                string dtNow = myInterface.GetSysDateTime("yyyy-MM-dd hh:mm:ss");
                if (uploadManager.CallService(reg, ref result, details, dtNow) <= 0)
                {
                    this.errorMsg = "上传费用失败，原因：" + uploadManager.ErrorMsg;
                    return -1;
                }
            }
            else if (registerType == "I")
            {
                var inPatient = register as FS.HISFC.Models.RADT.PatientInfo;
            }
            return 1;
        }


        /// <summary>
        /// 费用上传
        /// </summary>
        /// <typeparam name="R">承载登记信息的类型</typeparam>
        /// <typeparam name="T">承载费用上传明细的类型，默认是DataTable</typeparam>
        /// <param name="registerType">登记类型</param>
        /// <param name="register">登记信息</param>
        /// <param name="feeDetail">费用明细</param>
        /// <returns></returns>
        public int UploadFee<R, T>(string registerType, R register, T feeDetail)
        {
            /*  暂时不用
            if (register == null || feeDetail == null || string.IsNullOrEmpty(registerType))
            {
                this.errorMsg = "传参错误！";
                return -1;
            }
            if (registerType == "O")
            {
                var reg = register as FS.HISFC.Models.Registration.Register;
                if (reg == null || string.IsNullOrEmpty(reg.ID))
                {
                    this.errorMsg = "没有获得患者基本信息!";
                    return -1;
                }
                if (feeDetail is System.Data.DataTable)
                {
                    System.Data.DataTable dtUploadFee = feeDetail as System.Data.DataTable;
                    if (dtUploadFee == null || dtUploadFee.Rows.Count == 0)
                    {
                        this.errorMsg = "传入的费用明细为空!";
                        return -1;
                    }
                    IBorn.SI.GuangZhou.OutPatient.UpLoadFeeDetail uploadManager = new IBorn.SI.GuangZhou.OutPatient.UpLoadFeeDetail();
                    string result = string.Empty;
                    string dtNow = myInterface.GetSysDateTime("yyyy-MM-dd hh:mm:ss");
                    if (uploadManager.CallService(reg, ref result, dtUploadFee, dtNow) <= 0)
                    {
                        this.errorMsg = "上传费用失败，原因：" + uploadManager.ErrorMsg;
                        return -1;
                    }
                    //把已上传的费用明细保存到本地
                    FS.FrameWork.Management.PublicTrans.BeginTransaction();
                    myInterface.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                    try
                    {
                        if (myInterface.InsertOutPatientUploadFeeDetail(reg, dtUploadFee) < 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            this.errorMsg = "保存已上传到医保的费用明细到HIS失败";
                        }
                        FS.FrameWork.Management.PublicTrans.Commit();
                    }
                    catch (Exception ex)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        this.errorMsg = "保存已上传到医保的费用明细到HIS失败。\r\n" + ex.Message;
                    }
                }
            }
            else if (registerType == "I")
            {
                var inPatient = register as FS.HISFC.Models.RADT.PatientInfo;
            }
             */
            return 1;
        }


        #endregion
    }
}
