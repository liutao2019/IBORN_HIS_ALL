using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBorn.SI.MedicalInsurance.Proxy
{
    public class MedicalFactoryProxy
    {
        /// <summary>
        /// 合同单位编码
        /// </summary>
        private string medicalID = string.Empty;//合同单位编码

        FS.HISFC.BizLogic.Manager.Constant managerConstant = new FS.HISFC.BizLogic.Manager.Constant();

        FS.HISFC.BizLogic.Fee.PactUnitInfo pactManager = new FS.HISFC.BizLogic.Fee.PactUnitInfo();

        //List<FS.HISFC.Models.Base.PactInfo> medicalTypeList = new List<FS.HISFC.Models.Base.PactInfo>();

        List<FS.FrameWork.Models.NeuObject> medicalTypeList = new List<FS.FrameWork.Models.NeuObject>();

        /// <summary>
        /// 存储接口实例信息
        /// </summary>
        Dictionary<string, IBorn.SI.BI.IMedical> DicIMedical = new Dictionary<string, IBorn.SI.BI.IMedical>();

        public MedicalFactoryProxy()
        {
        }

        private string errorMsg = string.Empty;
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMsg
        {
            get { return errorMsg; }
            set { errorMsg = value; }
        }

        public int SetMedical(string medicalID)
        {
            //根据ID查找对应的合同单位
            FS.FrameWork.Models.NeuObject medicalType = medicalTypeList.Find(a => { return a.ID == medicalID; });
            if (medicalType == null)
            {
                //从数据库中查询
                medicalType = this.managerConstant.GetConstant("SI_PACT", medicalID);
                if (medicalType == null)
                {
                    this.errorMsg = pactManager.Err;
                    return -1;
                }
                if (medicalType.ID.Length == 0)
                {
                    this.errorMsg = "该结算种类不是医保或没有维护走医保程序，请核对维护信息。结算种类ID：" + medicalID;
                    return -1;
                }
                medicalTypeList.Add(medicalType);
            }
            if (DicIMedical.Count > 0 && DicIMedical.ContainsKey(medicalID))
            {
                return 1;
            }
            IBorn.SI.BI.IMedical iMedicalInterface = null;
            //反射接口
            if (string.IsNullOrEmpty(medicalType.Memo))
            {
                //使用默认的接口
                iMedicalInterface = new DefaultMedical();
            }
            else
            {
                Type providerType = Type.GetType(medicalType.Memo);
                if (providerType == null)
                {
                    this.errorMsg = "获取结算种类维护的对应待遇算法接口失败。\r\nType：" + medicalType.Memo;
                    return -1;
                }
                object control = providerType.Assembly.CreateInstance(providerType.FullName);
                if (control is IBorn.SI.BI.IMedical)
                {
                    iMedicalInterface = control as IBorn.SI.BI.IMedical;
                }
                else
                {
                    this.errorMsg = "结算种类维护的待遇算法借款类型配置不正确，Type：" + medicalType.Memo;
                    return -1;
                }
            }
            DicIMedical.Add(medicalID, iMedicalInterface);
            return 1;
        }

        private bool InstantiatedMedicalType(string medicalTypeID)
        {
            if (DicIMedical.Count == 0 || !DicIMedical.ContainsKey(medicalTypeID))
            {
                if (this.SetMedical(medicalTypeID) < 0)
                {
                    return false;
                }
            }
            return true;
        }

        #region IRADT 成员

        #region 门诊

        /// <summary>
        /// 门诊登记
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public int RegisterOutpatient(FS.HISFC.Models.Registration.Register r)
        {
            try
            {
                if (!InstantiatedMedicalType(r.Pact.ID))
                {
                    return -1;
                }
                IBorn.SI.BI.IRADT iRADTInterface = DicIMedical[r.Pact.ID].CreateRADT();
                if (iRADTInterface == null)
                {
                    this.errorMsg = "请实现医保RADT接口";
                    return -1;
                }
                int i = iRADTInterface.Register<FS.HISFC.Models.Registration.Register>("O", r);
                this.errorMsg = iRADTInterface.ErrorMsg;
                return i;
            }
            catch (Exception e)
            {
                this.errorMsg = e.Message;
                return -1;
            }
        }

        /// <summary>
        /// 门诊取消登记
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public int CancelRegisterOutpatient(FS.HISFC.Models.Registration.Register r)
        {
            try
            {
                if (!InstantiatedMedicalType(r.Pact.ID))
                {
                    return -1;
                }
                IBorn.SI.BI.IRADT iRADTInterface = DicIMedical[r.Pact.ID].CreateRADT();
                if (iRADTInterface == null)
                {
                    this.errorMsg = "请实现医保RADT接口";
                    return -1;
                }
                int returnValue = iRADTInterface.CancelRegister<FS.HISFC.Models.Registration.Register>("O", r);
                if (returnValue <= 0)
                {
                    this.errorMsg = iRADTInterface.ErrorMsg;
                }
                return returnValue;
            }
            catch (Exception e)
            {
                this.errorMsg = e.Message;

                return -1;
            }
        }


        #endregion

        #region 住院


        /// <summary>
        /// 取消住院登记
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        public int CancelRegisterInpatient(FS.HISFC.Models.RADT.PatientInfo register)
        {
            try
            {
                if (!InstantiatedMedicalType(register.Pact.ID))
                {
                    return -1;
                }
                IBorn.SI.BI.IRADT iRADTInterface = DicIMedical[register.Pact.ID].CreateRADT();
                if (iRADTInterface == null)
                {
                    this.errorMsg = "请实现医保RADT接口";
                    return -1;
                }
                int i = iRADTInterface.CancelRegister<FS.HISFC.Models.RADT.PatientInfo>("I", register);
                this.errorMsg = iRADTInterface.ErrorMsg;
                return i;
            }
            catch (Exception e)
            {
                this.errorMsg = e.Message;

                return -1;
            }
        }

        /// <summary>
        /// 住院登记
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        public int RegisterInpatient(FS.HISFC.Models.RADT.PatientInfo register)
        {
            try
            {
                if (!InstantiatedMedicalType(register.Pact.ID))
                {
                    return -1;
                }
                IBorn.SI.BI.IRADT iRADTInterface = DicIMedical[register.Pact.ID].CreateRADT();
                if (iRADTInterface == null)
                {
                    this.errorMsg = "请实现医保RADT接口";
                    return -1;
                }
                int i = iRADTInterface.Register<FS.HISFC.Models.RADT.PatientInfo>("I", register);
                this.errorMsg = iRADTInterface.ErrorMsg;
                return i;
            }
            catch (Exception e)
            {
                this.errorMsg = e.Message;

                return -1;
            }
        }

        /// <summary>
        /// 出院登记
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        public int LeaveRegister(FS.HISFC.Models.RADT.PatientInfo register)
        {
            try
            {
                if (!InstantiatedMedicalType(register.Pact.ID))
                {
                    return -1;
                }
                IBorn.SI.BI.IRADT iRADTInterface = DicIMedical[register.Pact.ID].CreateRADT();
                if (iRADTInterface == null)
                {
                    this.errorMsg = "请实现医保RADT接口";
                    return -1;
                }
                int i = iRADTInterface.LeaveRegister<FS.HISFC.Models.RADT.PatientInfo>("I", register);
                this.errorMsg = iRADTInterface.ErrorMsg;
                return i;
            }
            catch (Exception e)
            {
                this.errorMsg = e.Message;

                return -1;
            }
        }

        /// <summary>
        /// 取消出院登记
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        public int CancelLeaveRegister(FS.HISFC.Models.RADT.PatientInfo register)
        {
            try
            {
                if (!InstantiatedMedicalType(register.Pact.ID))
                {
                    return -1;
                }
                IBorn.SI.BI.IRADT iRADTInterface = DicIMedical[register.Pact.ID].CreateRADT();
                if (iRADTInterface == null)
                {
                    this.errorMsg = "请实现医保RADT接口";
                    return -1;
                }
                int i = iRADTInterface.CancelLeaveRegister<FS.HISFC.Models.RADT.PatientInfo>("I", register);
                this.errorMsg = iRADTInterface.ErrorMsg;
                return i;
            }
            catch (Exception e)
            {
                this.errorMsg = e.Message;

                return -1;
            }
        }

        #endregion

        #endregion

        #region IUpload 成员


        ///// <summary>
        ///// 门诊上传费用明细
        ///// </summary>
        ///// <param name="register">挂号信息</param>
        ///// <param name="feeDetails">费用明细实体集合</param>
        ///// <returns>-1 失败 0 没有记录 >=1 成功</returns>
        //public int UploadFeeDetailsOutpatient(FS.HISFC.Models.Registration.Register register, List<FS.HISFC.Models.Fee.Outpatient.FeeItemList> feeDetails)
        //{
        //    try
        //    {
        //        if (!InstantiatedMedicalType(register.Pact.ID))
        //        {
        //            return -1;
        //        }
        //        IBorn.SI.BI.IUpload uploadInterface = DicIMedical[register.Pact.ID].CreateUpload();
        //        if (uploadInterface == null)
        //        {
        //            this.errorMsg = "请实现医保IUpload接口";
        //            return -1;
        //        }
        //        int i = uploadInterface.UploadFee<FS.HISFC.Models.Registration.Register, FS.HISFC.Models.Fee.Outpatient.FeeItemList>("O", register, feeDetails);
        //        this.errorMsg = uploadInterface.ErrorMsg;
        //        return i;
        //    }
        //    catch (Exception e)
        //    {
        //        this.errorMsg = e.Message;
        //        return -1;
        //    }
        //}

        /// <summary>
        /// 门诊费用上传
        /// </summary>
        /// <param name="register"></param>
        /// <param name="dtFeeDetails"></param>
        /// <returns></returns>
        public int UploadFeeDetailsOutpatient(FS.HISFC.Models.Registration.Register register, System.Data.DataTable dtFeeDetails)
        {
            try
            {
                if (!InstantiatedMedicalType(register.Pact.ID))
                {
                    return -1;
                }
                IBorn.SI.BI.IUpload uploadInterface = DicIMedical[register.Pact.ID].CreateUpload();
                if (uploadInterface == null)
                {
                    this.errorMsg = "请实现医保IUpload接口";
                    return -1;
                }
                int i = uploadInterface.UploadFee<FS.HISFC.Models.Registration.Register, System.Data.DataTable>("O", register, dtFeeDetails);
                this.errorMsg = uploadInterface.ErrorMsg;
                return i;
            }
            catch (Exception e)
            {
                this.errorMsg = e.Message;
                return -1;
            }
        }

        /// <summary>
        /// 住院费用上传
        /// </summary>
        /// <param name="register"></param>
        /// <param name="dtFeeDetails"></param>
        /// <returns></returns>
        public int UploadFeeDetailsInpatient(FS.HISFC.Models.RADT.PatientInfo register, System.Data.DataTable dtFeeDetails)
        {
            try
            {
                if (!InstantiatedMedicalType(register.Pact.ID))
                {
                    return -1;
                }
                IBorn.SI.BI.IUpload uploadInterface = DicIMedical[register.Pact.ID].CreateUpload();
                if (uploadInterface == null)
                {
                    this.errorMsg = "请实现医保IUpload接口";
                    return -1;
                }
                int i = uploadInterface.UploadFee<FS.HISFC.Models.RADT.PatientInfo, System.Data.DataTable>("I", register, dtFeeDetails);
                this.errorMsg = uploadInterface.ErrorMsg;
                return i;
            }
            catch (Exception e)
            {
                this.errorMsg = e.Message;
                return -1;
            }
        }

        /// <summary>
        /// 删除已上传的门诊费用明细
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        public int DeleteUploadedFeeOutpatient(FS.HISFC.Models.Registration.Register register)
        {
            try
            {
                if (!InstantiatedMedicalType(register.Pact.ID))
                {
                    return -1;
                }
                IBorn.SI.BI.IUpload uploadInterface = DicIMedical[register.Pact.ID].CreateUpload();
                if (uploadInterface == null)
                {
                    this.errorMsg = "请实现医保IUpload接口";
                    return -1;
                }
                int i = uploadInterface.DeleteFee("O", register);
                this.errorMsg = uploadInterface.ErrorMsg;
                return i;
            }
            catch (Exception e)
            {
                this.errorMsg = e.Message;
                return -1;
            }
        }


        /// 删除已上传的住院费用明细
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        public int DeleteUploadedFeeInpatient(FS.HISFC.Models.RADT.PatientInfo register)
        {
            try
            {
                if (!InstantiatedMedicalType(register.Pact.ID))
                {
                    return -1;
                }
                IBorn.SI.BI.IUpload uploadInterface = DicIMedical[register.Pact.ID].CreateUpload();
                if (uploadInterface == null)
                {
                    this.errorMsg = "请实现医保IUpload接口";
                    return -1;
                }
                int i = uploadInterface.DeleteFee("I", register);
                this.errorMsg = uploadInterface.ErrorMsg;
                return i;
            }
            catch (Exception e)
            {
                this.errorMsg = e.Message;
                return -1;
            }
        }

        /// <summary>
        /// 获取已上传的费用明细
        /// </summary>
        /// <returns></returns>
        public System.Data.DataTable QueryOutPatientUploadFee(FS.HISFC.Models.Registration.Register register)
        {
            try
            {
                if (!InstantiatedMedicalType(register.Pact.ID))
                {
                    return null;
                }
                IBorn.SI.BI.IUpload uploadInterface = DicIMedical[register.Pact.ID].CreateUpload();
                if (uploadInterface == null)
                {
                    this.errorMsg = "请实现医保IUpload接口";
                    return null;
                }
                var res = uploadInterface.GetNeedUploadFeeDetail("O", register.ID, register.SIMainInfo.InvoiceNo);
                this.errorMsg = uploadInterface.ErrorMsg;
                return res;
            }
            catch (Exception e)
            {
                this.errorMsg = e.Message;
                return null;
            }
        }

        /// <summary>
        /// 获取需要  上传的费用明细
        /// </summary>
        /// <returns></returns>
        public System.Data.DataTable QueryInPatientUploadFee(FS.HISFC.Models.RADT.PatientInfo register)
        {
            try
            {
                if (!InstantiatedMedicalType(register.Pact.ID))
                {
                    return null;
                }
                IBorn.SI.BI.IUpload uploadInterface = DicIMedical[register.Pact.ID].CreateUpload();
                if (uploadInterface == null)
                {
                    this.errorMsg = "请实现医保IUpload接口";
                    return null;
                }
                var res = uploadInterface.GetNeedUploadFeeDetail("I", register.ID);
                this.errorMsg = uploadInterface.ErrorMsg;
                return res;
            }
            catch (Exception e)
            {
                this.errorMsg = e.Message;
                return null;
            }
        }

        public System.Data.DataTable GetInPatientNotUploadFeeDetail(FS.HISFC.Models.RADT.PatientInfo register)
        {
            try
            {
                if (!InstantiatedMedicalType(register.Pact.ID))
                {
                    return null;
                }
                IBorn.SI.BI.IUpload uploadInterface = DicIMedical[register.Pact.ID].CreateUpload();
                if (uploadInterface == null)
                {
                    this.errorMsg = "请实现医保IUpload接口";
                    return null;
                }
                var res = uploadInterface.GetNotUploadFeeDetail("I", register.ID);
                this.errorMsg = uploadInterface.ErrorMsg;
                return res;
            }
            catch (Exception e)
            {
                this.errorMsg = e.Message;
                return null;
            }
        }

        #endregion

        #region IBalance 成员

        #region 门诊

        public int BalanceOutPatient(FS.HISFC.Models.Registration.Register register)
        {
            try
            {
                if (!InstantiatedMedicalType(register.Pact.ID))
                {
                    return -1;
                }
                IBorn.SI.BI.IBalance balanceInterface = DicIMedical[register.Pact.ID].CreateBalance();
                if (balanceInterface == null)
                {
                    this.errorMsg = "请实现医保IBalance接口";
                    return -1;
                }
                int i = balanceInterface.Balance<FS.HISFC.Models.Registration.Register>("O", register);
                this.errorMsg = balanceInterface.ErrorMsg;
                return i;
            }
            catch (Exception e)
            {
                this.errorMsg = e.Message;
                return -1;
            }
        }

        public int CancelBalanceOutPatient(FS.HISFC.Models.Registration.Register register)
        {
            try
            {
                if (!InstantiatedMedicalType(register.Pact.ID))
                {
                    return -1;
                }
                IBorn.SI.BI.IBalance balanceInterface = DicIMedical[register.Pact.ID].CreateBalance();
                if (balanceInterface == null)
                {
                    this.errorMsg = "请实现医保IBalance接口";
                    return -1;
                }
                int i = balanceInterface.CancelBalance<FS.HISFC.Models.Registration.Register>("O", register);
                this.errorMsg = balanceInterface.ErrorMsg;
                return i;
            }
            catch (Exception e)
            {
                this.errorMsg = e.Message;
                return -1;
            }
        }

        /// <summary>
        /// 同步门诊结算结果到本地
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        public int SyncOutpatientMedicalBalance(FS.HISFC.Models.Registration.Register register)
        {
            try
            {
                if (!InstantiatedMedicalType(register.Pact.ID))
                {
                    return -1;
                }
                IBorn.SI.BI.IBalance balanceInterface = DicIMedical[register.Pact.ID].CreateBalance();
                if (balanceInterface == null)
                {
                    this.errorMsg = "请实现医保IBalance接口";
                    return -1;
                }
                int i = balanceInterface.SyncMedicalBalance<FS.HISFC.Models.Registration.Register>("O", register);
                this.errorMsg = balanceInterface.ErrorMsg;
                return i;
            }
            catch (Exception e)
            {
                this.errorMsg = e.Message;
                return -1;
            }
        }

        #endregion

        #region 住院

        public int BalanceInPatient(FS.HISFC.Models.RADT.PatientInfo register)
        {
            try
            {
                if (!InstantiatedMedicalType(register.Pact.ID))
                {
                    return -1;
                }
                IBorn.SI.BI.IBalance balanceInterface = DicIMedical[register.Pact.ID].CreateBalance();
                if (balanceInterface == null)
                {
                    this.errorMsg = "请实现医保IBalance接口";
                    return -1;
                }
                int i = balanceInterface.Balance<FS.HISFC.Models.RADT.PatientInfo>("I", register);
                this.errorMsg = balanceInterface.ErrorMsg;
                return i;
            }
            catch (Exception e)
            {
                this.errorMsg = e.Message;
                return -1;
            }
        }

        public int CancelBalanceInPatient(FS.HISFC.Models.RADT.PatientInfo register)
        {
            try
            {
                if (!InstantiatedMedicalType(register.Pact.ID))
                {
                    return -1;
                }
                IBorn.SI.BI.IBalance balanceInterface = DicIMedical[register.Pact.ID].CreateBalance();
                if (balanceInterface == null)
                {
                    this.errorMsg = "请实现医保IBalance接口";
                    return -1;
                }
                int i = balanceInterface.CancelBalance<FS.HISFC.Models.RADT.PatientInfo>("I", register);
                this.errorMsg = balanceInterface.ErrorMsg;
                return i;
            }
            catch (Exception e)
            {
                this.errorMsg = e.Message;
                return -1;
            }
        }
        /// <summary>
        /// 同步住院结算结果到本地
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        public int SyncInpatientMedicalBalance(FS.HISFC.Models.RADT.PatientInfo register)
        {
            try
            {
                if (!InstantiatedMedicalType(register.Pact.ID))
                {
                    return -1;
                }
                IBorn.SI.BI.IBalance balanceInterface = DicIMedical[register.Pact.ID].CreateBalance();
                if (balanceInterface == null)
                {
                    this.errorMsg = "请实现医保IBalance接口";
                    return -1;
                }
                int i = balanceInterface.SyncMedicalBalance<FS.HISFC.Models.RADT.PatientInfo>("I", register);
                this.errorMsg = balanceInterface.ErrorMsg;
                return i;
            }
            catch (Exception e)
            {
                this.errorMsg = e.Message;
                return -1;
            }
        }

        #endregion

        #endregion

        #region ICompare 成员

        /// <summary>
        /// 下载医保中心项目目录
        /// </summary>
        /// <param name="medicalType"></param>
        /// <returns></returns>
        public int DownloadCenterItem(string medicalTypeID)
        {
            //if (DicIMedical.Count == 0 || !DicIMedical.ContainsKey(medicalTypeID))
            //{
            //    if (this.SetMedical(medicalTypeID) < 0)
            //    {
            //        return -1;
            //    }
            //}
            //IBorn.SI.BI.ICompare iCompareInterface = DicIMedical[medicalTypeID].CreateCompare();
            //if (iCompareInterface == null)
            //{
            //    this.errorMsg = "请实现医保ICompare接口";
            //    return -1;
            //}
            //int res = iCompareInterface.DownloadCenterItem(medicalTypeID);
            //if (res < 0)
            //{
            //    this.errorMsg = iCompareInterface.ErrorMsg;
            //    return -1;
            //}
            //return res;
            return 1;
        }



        /// <summary>
        /// 下载医保中心处理对照信息结果：通过审批
        /// </summary>
        /// <param name="medicalTypeID"></param>      
        /// <returns></returns>
        public int DownloadCompareItem(string medicalTypeID)
        {
            if (DicIMedical.Count == 0 || !DicIMedical.ContainsKey(medicalTypeID))
            {
                if (this.SetMedical(medicalTypeID) < 0)
                {
                    return -1;
                }
            }
            IBorn.SI.BI.ICompare iCompareInterface = DicIMedical[medicalTypeID].CreateCompare();
            if (iCompareInterface == null)
            {
                this.errorMsg = "请实现医保ICompare接口";
                return -1;
            }
            //if (iCompareInterface.DownloadCompareItem(medicalType, ref compareItem) < 0)
            //{
            //    this.errorMsg = iCompareInterface.ErrorMsg;
            //    return -1;
            //}
            return 1;
        }

        #endregion




    }
}
