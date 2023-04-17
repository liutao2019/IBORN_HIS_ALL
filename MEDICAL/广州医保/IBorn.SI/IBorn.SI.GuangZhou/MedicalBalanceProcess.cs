using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IBorn.SI.GuangZhou
{
    /// <summary>
    /// 广州医保结算接口实现
    /// by 飞扬 2019-09-28
    /// </summary>
    public class MedicalBalanceProcess : IBorn.SI.BI.IBalance
    {
        IBorn.SI.GuangZhou.SILocalManager myInterface = new IBorn.SI.GuangZhou.SILocalManager();

        #region IBalance 成员

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

        public int Balance<T>(string registerType, T register)
        {
            if (register == null || string.IsNullOrEmpty(registerType))
            {
                this.errorMsg = "传参错误！";
                return -1;
            }

            if (registerType == "O")
            {
                var reg = register as FS.HISFC.Models.Registration.Register;

                #region 1、匹配医保登记信息
                string regNO = myInterface.GetPatientLastSIRegNO(reg.ID, registerType);
                reg.SIMainInfo.RegNo = regNO;
                IBorn.SI.GuangZhou.Controls.ucRegisterInfoOutPatient uc = new IBorn.SI.GuangZhou.Controls.ucRegisterInfoOutPatient();
                uc.Register = reg;
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                FS.FrameWork.WinForms.Classes.Function.ShowControl(uc);
                if (uc.IsOK)
                {
                    uc.Register.SIMainInfo.IsValid = true;
                    uc.Register.SIMainInfo.IsBalanced = false;
                    reg.SIMainInfo = uc.Register.SIMainInfo;
                    //if (string.IsNullOrEmpty(regNO))
                    //{
                    int iReturn = myInterface.SaveSIMainInfo(uc.Register);
                    if (iReturn <= 0)
                    {
                        MessageBox.Show("保存医保端的登记信息到本地失败!" + myInterface.Err);
                        return -1;
                    }
                    //}
                }
                else
                {
                    this.errorMsg = "未选择医保端的登记信息!";
                    return -1;
                }

                #endregion

                #region 2、上传费用到医保前置机
                System.Data.DataTable dtUploadFee = myInterface.QueryOutPatientNeedUploadFeeDetail(reg.ID, reg.SIMainInfo.InvoiceNo);
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

                #endregion

                #region 3、同步医保结算结果

                IBorn.SI.GuangZhou.Controls.ucBalanceClinic ucBlanace = new Controls.ucBalanceClinic();
                ucBlanace.RInfo = reg;
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                FS.FrameWork.WinForms.Classes.Function.ShowControl(ucBlanace);
                if (!ucBlanace.IsOK)
                {
                    this.errorMsg = "取消了获取医保结算结果操作";
                    return -1;
                }
                decimal uploadTotCost = 0;
                for (int i = 0; i < dtUploadFee.Rows.Count; i++)
                {
                    uploadTotCost += FS.FrameWork.Function.NConvert.ToDecimal(dtUploadFee.Rows[i]["JE"]);
                }
                if (ucBlanace.RInfo.SIMainInfo.TotCost - uploadTotCost > 0.5m)
                {
                    this.errorMsg = string.Format("结算总额不一致！\r\nHIS的医保总额为{0}，医保结算的总额为{1}。\r\n请检查后删除费用重新操作。", uploadTotCost, ucBlanace.RInfo.SIMainInfo.TotCost);
                    return -1;
                }
                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                myInterface.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                try
                {
                    //把医保结算信息存储到HIS
                    string jsid = string.Empty;
                    if (myInterface.SaveBlanceSIOutPatient(ucBlanace.RInfo, ref jsid) < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        this.errorMsg = "医保结算信息存储到HIS发生错误。\r\n" + myInterface.Err;
                        return -1;
                    }
                    //把已上传的费用明细保存到本地   
                    if (myInterface.InsertOutPatientUploadFeeDetail(reg, dtUploadFee, jsid) < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        this.errorMsg = "保存已上传到医保的费用明细到HIS失败。\r\n" + myInterface.Err;
                    }
                    FS.FrameWork.Management.PublicTrans.Commit();
                }
                catch (Exception ex)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.errorMsg = "保存已上传到医保的费用明细到HIS失败。\r\n" + ex.Message;
                    return -1;
                }
                #endregion
            }
            else if (registerType == "I")
            {
                var inPatient = register as FS.HISFC.Models.RADT.PatientInfo;

                #region 1、匹配医保登记信息
                string regNO = myInterface.GetPatientLastSIRegNO(inPatient.ID, registerType);
                inPatient.SIMainInfo.RegNo = regNO;
                IBorn.SI.GuangZhou.Controls.ucRegisterInfoInPatient uc = new IBorn.SI.GuangZhou.Controls.ucRegisterInfoInPatient();
                uc.Register = inPatient;
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                FS.FrameWork.WinForms.Classes.Function.ShowControl(uc);
                if (uc.IsOK)
                {
                    uc.Register.SIMainInfo.IsValid = true;
                    uc.Register.SIMainInfo.IsBalanced = false;
                    inPatient.SIMainInfo = uc.Register.SIMainInfo;
                    //if (string.IsNullOrEmpty(regNO))
                    //{
                    int iReturn = myInterface.SaveSIMainInfo(uc.Register);
                    if (iReturn <= 0)
                    {
                        MessageBox.Show("保存医保端的登记信息到本地失败!" + myInterface.Err);
                        return -1;
                    }
                    //}
                }
                else
                {
                    this.errorMsg = "未选择医保端的登记信息!";
                    return -1;
                }

                #endregion

                #region 2、上传费用到医保前置机

                //System.Data.DataTable dtUploadFee = myInterface.QueryInPatientNeedUploadFeeDetail(inPatient.ID, inPatient.SIMainInfo.InvoiceNo);
                System.Data.DataTable dtUploadFee = myInterface.QueryInPatientNeedUploadFeeDetail(inPatient.ID);
                if (dtUploadFee == null || dtUploadFee.Rows.Count == 0)
                {
                    this.errorMsg = "传入的费用明细为空!";
                    return -1;
                }
                IBorn.SI.GuangZhou.InPatient.UpLoadFeeDetail uploadManager = new IBorn.SI.GuangZhou.InPatient.UpLoadFeeDetail();
                string result = string.Empty;
                string dtNow = myInterface.GetSysDateTime("yyyy-MM-dd hh:mm:ss");
                if (uploadManager.CallService(inPatient, ref result, dtUploadFee, dtNow) <= 0)
                {
                    this.errorMsg = "上传费用失败，原因：" + uploadManager.ErrorMsg;
                    return -1;
                }

                #endregion

                #region 3、同步医保结算结果

                IBorn.SI.GuangZhou.Controls.ucBalanceInPatient ucBlanace = new Controls.ucBalanceInPatient();
                ucBlanace.RInfo = inPatient;
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                FS.FrameWork.WinForms.Classes.Function.ShowControl(ucBlanace);
                if (!ucBlanace.IsOK)
                {
                    this.errorMsg = "取消了获取医保结算结果操作";
                    return -1;
                }
                decimal uploadTotCost = 0;
                for (int i = 0; i < dtUploadFee.Rows.Count; i++)
                {
                    uploadTotCost += FS.FrameWork.Function.NConvert.ToDecimal(dtUploadFee.Rows[i]["JE"]);
                }
                if (ucBlanace.RInfo.SIMainInfo.TotCost - uploadTotCost > 0.5m)
                {
                    this.errorMsg = string.Format("结算总额不一致！\r\nHIS的医保总额为{0}，医保结算的总额为{1}。\r\n请检查后删除费用重新操作。", uploadTotCost, ucBlanace.RInfo.SIMainInfo.TotCost);
                    return -1;
                }
                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                myInterface.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                try
                {
                    string jsid = string.Empty;
                    //把医保结算信息存储到HIS
                    if (myInterface.SaveBlanceSIInPatient(ucBlanace.RInfo, ref jsid) < 0)
                    {
                        this.errorMsg = "医保结算信息存储到HIS发生错误。\r\n" + myInterface.Err;
                        return -1;
                    }
                    //把已上传的费用明细保存到本地
                    if (myInterface.InsertInPatientUploadFeeDetail(inPatient, dtUploadFee, jsid) < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        this.errorMsg = "保存已上传到医保的费用明细到HIS失败。\r\n" + myInterface.Err;
                        return -1;
                    }
                    FS.FrameWork.Management.PublicTrans.Commit();
                }
                catch (Exception ex)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.errorMsg = "保存已上传到医保的费用明细到HIS失败。\r\n" + ex.Message;
                    return -1;
                }

                #endregion
            }
            return 1;
        }

        //public int CancelBalance<T>(string registerType, T register)
        //{
        //    if (register == null || string.IsNullOrEmpty(registerType))
        //    {
        //        this.errorMsg = "CancelBalance方法传参错误！";
        //        return -1;
        //    }
        //    if (registerType == "O")
        //    {
        //        var reg = register as FS.HISFC.Models.Registration.Register;                
        //        if (string.IsNullOrEmpty(reg.ID) || string.IsNullOrEmpty(reg.SIMainInfo.InvoiceNo))
        //        {
        //            this.errorMsg = "CancelBalance方法参数错误。";
        //            return -1;
        //        }
        //        FS.FrameWork.Management.PublicTrans.BeginTransaction();
        //        myInterface.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
        //        try
        //        {
        //            #region 1、作废HIS本地存的医保结算信息

        //            if (myInterface.UpdateOutPatientSiBalanceDisable(reg.SIMainInfo.InvoiceNo, reg.ID) < 0)
        //            {
        //                FS.FrameWork.Management.PublicTrans.RollBack();
        //                this.errorMsg = "作废HIS本地存的医保结算信息失败。\r\n" + myInterface.Err;
        //                return -1;
        //            }

        //            #endregion

        //            #region 2、删除HIS本地存储的已上传医保的费用明细

        //            if (myInterface.DeleteOutPatientUploadFeeDetail(reg.ID, reg.SIMainInfo.InvoiceNo) < 0)
        //            {
        //                FS.FrameWork.Management.PublicTrans.RollBack();
        //                this.errorMsg = "删除HIS本地存储的已上传医保的费用明细失败。\r\n" + myInterface.Err;
        //                return -1;
        //            }

        //            #endregion

        //            #region 3、作废HIS本地存储的医保登记信息

        //            if (myInterface.UpdateSiMainInfoDisableByInvoiceNO(reg.ID, reg.SIMainInfo.InvoiceNo) < 0)
        //            {
        //                FS.FrameWork.Management.PublicTrans.RollBack();
        //                this.errorMsg = "作废HIS本地存储的医保登记信息失败。\r\n" + myInterface.Err;
        //                return -1;
        //            }
        //            #endregion

        //            FS.FrameWork.Management.PublicTrans.Commit();
        //        }
        //        catch (Exception)
        //        {
        //            FS.FrameWork.Management.PublicTrans.RollBack();
        //            throw;
        //        }
        //    }
        //    else if (registerType == "I")
        //    {
        //        var inPatient = register as FS.HISFC.Models.RADT.PatientInfo;
        //        if (string.IsNullOrEmpty(inPatient.ID) || string.IsNullOrEmpty(inPatient.SIMainInfo.InvoiceNo))
        //        {
        //            this.errorMsg = "CancelBalance方法参数错误。";
        //            return -1;
        //        }
        //        FS.FrameWork.Management.PublicTrans.BeginTransaction();
        //        myInterface.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
        //        try
        //        {
        //            #region 1、作废HIS本地存的医保结算信息

        //            if (myInterface.UpdateInPatientSiBalanceDisable(inPatient.SIMainInfo.InvoiceNo, inPatient.ID) < 0)
        //            {
        //                FS.FrameWork.Management.PublicTrans.RollBack();
        //                this.errorMsg = "作废HIS本地存的医保结算信息失败。\r\n" + myInterface.Err;
        //                return -1;
        //            }

        //            #endregion

        //            #region 2、删除HIS本地存储的已上传医保的费用明细

        //            if (myInterface.DeleteInPatientUploadFeeDetail(inPatient.ID, inPatient.SIMainInfo.InvoiceNo) < 0)
        //            {
        //                FS.FrameWork.Management.PublicTrans.RollBack();
        //                this.errorMsg = "删除HIS本地存储的已上传医保的费用明细失败。\r\n" + myInterface.Err;
        //                return -1;
        //            }

        //            #endregion

        //            #region 3、作废HIS本地存储的医保登记信息

        //            if (myInterface.UpdateSiMainInfoDisableByInvoiceNO(inPatient.ID, inPatient.SIMainInfo.InvoiceNo) < 0)
        //            {
        //                FS.FrameWork.Management.PublicTrans.RollBack();
        //                this.errorMsg = "作废HIS本地存储的医保登记信息失败。\r\n" + myInterface.Err;
        //                return -1;
        //            }

        //            #endregion

        //            FS.FrameWork.Management.PublicTrans.Commit();
        //        }
        //        catch (Exception)
        //        {
        //            FS.FrameWork.Management.PublicTrans.RollBack();
        //            throw;
        //        }
        //    }
        //    return 1;
        //}

        public int CancelBalance<T>(string registerType, T register)
        {
            if (register == null || string.IsNullOrEmpty(registerType))
            {
                this.errorMsg = "CancelBalance方法传参错误！";
                return -1;
            }
            if (registerType == "O")
            {
                var reg = register as FS.HISFC.Models.Registration.Register;
                if (string.IsNullOrEmpty(reg.ID) || string.IsNullOrEmpty(reg.SIMainInfo.BalNo))
                {
                    this.errorMsg = "CancelBalance方法参数错误。";
                    return -1;
                }
                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                myInterface.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                try
                {
                    #region 1、作废HIS本地存的医保结算信息

                    if (myInterface.UpdateOutPatientSiBalanceDisable(reg.SIMainInfo.BalNo) < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        this.errorMsg = "作废HIS本地存的医保结算信息失败。\r\n" + myInterface.Err;
                        return -1;
                    }

                    #endregion

                    #region 2、删除HIS本地存储的已上传医保的费用明细

                    if (myInterface.DeleteOutPatientUploadFeeDetail(reg.SIMainInfo.BalNo) < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        this.errorMsg = "删除HIS本地存储的已上传医保的费用明细失败。\r\n" + myInterface.Err;
                        return -1;
                    }

                    #endregion

                    #region 3、作废HIS本地存储的医保登记信息

                    if (myInterface.UpdateOutPatientSiMainInfoDisable(reg.ID, reg.SIMainInfo.RegNo) < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        this.errorMsg = "作废HIS本地存储的医保登记信息失败。\r\n" + myInterface.Err;
                        return -1;
                    }
                    #endregion

                    FS.FrameWork.Management.PublicTrans.Commit();
                }
                catch (Exception)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    throw;
                }
            }
            else if (registerType == "I")
            {
                var inPatient = register as FS.HISFC.Models.RADT.PatientInfo;
                if (string.IsNullOrEmpty(inPatient.ID) || string.IsNullOrEmpty(inPatient.SIMainInfo.BalNo))
                {
                    this.errorMsg = "CancelBalance方法参数错误。";
                    return -1;
                }
                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                myInterface.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                try
                {
                    #region 1、作废HIS本地存的医保结算信息

                    if (myInterface.UpdateInPatientSiBalanceDisable(inPatient.SIMainInfo.BalNo) < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        this.errorMsg = "作废HIS本地存的医保结算信息失败。\r\n" + myInterface.Err;
                        return -1;
                    }

                    #endregion

                    #region 2、删除HIS本地存储的已上传医保的费用明细

                    if (myInterface.DeleteInPatientUploadFeeDetail(inPatient.SIMainInfo.BalNo) < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        this.errorMsg = "删除HIS本地存储的已上传医保的费用明细失败。\r\n" + myInterface.Err;
                        return -1;
                    }

                    #endregion

                    #region 3、作废HIS本地存储的医保登记信息

                    if (myInterface.UpdateInPatientSiMainInfoDisable(inPatient.ID, inPatient.SIMainInfo.RegNo) < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        this.errorMsg = "作废HIS本地存储的医保登记信息失败。\r\n" + myInterface.Err;
                        return -1;
                    }

                    #endregion

                    FS.FrameWork.Management.PublicTrans.Commit();
                }
                catch (Exception)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    throw;
                }
            }
            return 1;
        }
        public int PreBalance<T>(string registerType, T balance)
        {
            return 1;
        }

        public int SyncMedicalBalance<T>(string registerType, T register)
        {
            if (register == null || string.IsNullOrEmpty(registerType))
            {
                this.errorMsg = "传参错误！";
                return -1;
            }
            if (registerType == "O")
            {
                var reg = register as FS.HISFC.Models.Registration.Register;
                IBorn.SI.GuangZhou.Controls.ucBalanceClinic ucBlanace = new Controls.ucBalanceClinic();
                ucBlanace.RInfo = reg;
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                FS.FrameWork.WinForms.Classes.Function.ShowControl(ucBlanace);
                if (!ucBlanace.IsOK)
                {
                    this.errorMsg = "取消了操作";
                    return -1;
                }
                //todo:调用存储过程生成医保结算明细和发票
            }
            else if (registerType == "I")
            {
                var inPatient = register as FS.HISFC.Models.RADT.PatientInfo;
                IBorn.SI.GuangZhou.InPatient.GetBalanceResult getBalance = new InPatient.GetBalanceResult();
                //if (getBalance.CallService(inPatient, ref inPatient) < 0)
                //{
                //    this.errorMsg = getBalance.ErrorMsg;
                //    return -1;
                //}
            }
            return 1;
        }

        #endregion

    }
}
