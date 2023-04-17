using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using FS.SOC.HISFC.InpatientFee.Components.Balance;

namespace FS.SOC.HISFC.InpatientFee.Components
{
    /// <summary>
    /// [功能描述: 功能函数]<br></br>
    /// [创 建 者: zhaoj]<br></br>
    /// [创建时间: 2011-10]<br></br>
    /// </summary>
    public  class Function
    {
        #region 拆分费用


        /// <summary>
        /// 拆分费用
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="alFeeInfo"></param>
        /// <param name="errText"></param>
        /// <returns></returns>
        public static bool SplitFeeItem(FS.HISFC.Models.RADT.PatientInfo patientInfo, DateTime beginTime, DateTime endTime,ArrayList alFeeInfo, ref string errText)
        {
            //拆分费用
            if (Function.GetFeeBizProcess().IsExistNoSplitFee(patientInfo))
            {
                frmSplitFeeInfo frmSplitFeeInfo = new frmSplitFeeInfo();
                frmSplitFeeInfo.SetPatientFeeInfo(alFeeInfo);
                frmSplitFeeInfo.SetPatientInfo(patientInfo);
                frmSplitFeeInfo.ShowDialog();

                if (frmSplitFeeInfo.IsConfirm)
                {
                    if (frmSplitFeeInfo.IsSplit)
                    {
                        //拆分费用信息
                        if (Function.GetFeeBizProcess().ProcessSplitFeeInfo(patientInfo, beginTime, endTime) < 0)
                        {
                            errText = "拆分费用失败，" + Function.GetFeeBizProcess().Err;
                            return false;
                        }
                    }
                    else
                    {
                        //合并费用信息
                        if (Function.GetFeeBizProcess().ProcessCombineFeeInfo(patientInfo, beginTime, endTime) < 0)
                        {
                            errText = "合并费用失败，" + Function.GetFeeBizProcess().Err;
                            return false;
                        }

                    }
                }
            }

            return true;
        }

        /// <summary>
        /// 拆分费用
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="alFeeInfo"></param>
        /// <param name="errText"></param>
        /// <returns></returns>
        public static bool SplitFeeItem(FS.HISFC.Models.RADT.PatientInfo patientInfo, ArrayList alFeeInfo, ref string errText)
        {
            return SplitFeeItem(patientInfo, DateTime.MinValue, DateTime.MaxValue, alFeeInfo, ref errText);
        }

        /// <summary>
        /// 拆分费用
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="alFeeInfo"></param>
        /// <param name="errText"></param>
        /// <returns></returns>
        public static bool SplitFeeItem(FS.HISFC.Models.RADT.PatientInfo patientInfo, ref string errText)
        {
            FS.HISFC.BizLogic.Fee.InPatient inpatientManager = new FS.HISFC.BizLogic.Fee.InPatient();
            ArrayList alFeeInfo = inpatientManager.QueryFeeInfosAndChangeCostGroupByMinFeeByInpatientNO(patientInfo.ID, "0");

            return SplitFeeItem(patientInfo, DateTime.MinValue, DateTime.MaxValue, alFeeInfo, ref errText);
        }

        #endregion

        #region 医保

        /// <summary>
        /// 医保预结算
        /// </summary>
        /// <param name="medcareInterfaceProxy"></param>
        /// <param name="patientInfo"></param>
        /// <param name="alFeeItemList"></param>
        /// <param name="errText"></param>
        /// <returns></returns>
        public static long PreBalanceInpatient(ref FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy medcareInterfaceProxy, FS.HISFC.Models.RADT.PatientInfo patientInfo, ArrayList alFeeItemList, ref string errText)
        {
            //FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy medcareInterfaceProxy = new FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy();

            //long returnValue= medcareInterfaceProxy.SetPactCode(patientInfo.Pact.ID);
            //if (returnValue != 1)
            //{
            //    FS.FrameWork.Management.PublicTrans.RollBack();
            //    errText = medcareInterfaceProxy.ErrMsg;
            //    return -1;
            //}
            //FS.FrameWork.Management.PublicTrans.BeginTransaction();
            medcareInterfaceProxy.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            long returnValue = medcareInterfaceProxy.Connect();
            if (returnValue != 1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                errText = medcareInterfaceProxy.ErrMsg;
                return -1;
            }
            //{BF6500FD-71FE-4cce-B328-D10CB7CBF22B}添加读卡 注意：主要为了读取医保串，PreBalanceInpatient预结方法PreBalanceInpatient中应判断patient.SIMainInfo.Memo是否为空
            //如果为空从本地取
            returnValue = medcareInterfaceProxy.GetRegInfoInpatient(patientInfo);
            if (returnValue != 1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                errText = medcareInterfaceProxy.ErrMsg;
                return -1;
            }

            returnValue = medcareInterfaceProxy.PreBalanceInpatient(patientInfo, ref alFeeItemList);
            if (returnValue != 1)
            {
                medcareInterfaceProxy.Rollback();
                FS.FrameWork.Management.PublicTrans.RollBack();
                errText = medcareInterfaceProxy.ErrMsg;
                return -1;
            }

            returnValue = medcareInterfaceProxy.Disconnect();
            if (returnValue != 1)
            {
                medcareInterfaceProxy.Rollback();
                FS.FrameWork.Management.PublicTrans.RollBack();
                errText = medcareInterfaceProxy.ErrMsg;
                return -1;
            }
            //医保接口预结算完毕
            // {A9769CE2-E76B-40be-BB09-82A265CBB3CA}
            FS.FrameWork.Management.PublicTrans.RollBack();

            return 1;
        }

        /// <summary>
        /// 医保预结算
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="alFeeItemList"></param>
        /// <param name="errText"></param>
        /// <returns></returns>
        public static long PreBalanceInpatient( FS.HISFC.Models.RADT.PatientInfo patientInfo, ArrayList alFeeItemList, ref string errText)
        {
            FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy medcareInterfaceProxy = new FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy();

            long returnValue = medcareInterfaceProxy.SetPactCode(patientInfo.Pact.ID);
            if (returnValue != 1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                errText = medcareInterfaceProxy.ErrMsg;
                return -1;
            }
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            medcareInterfaceProxy.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            returnValue = medcareInterfaceProxy.Connect();
            if (returnValue != 1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                errText = medcareInterfaceProxy.ErrMsg;
                return -1;
            }
       

            //获取医保登记信息
            returnValue = medcareInterfaceProxy.GetRegInfoInpatient(patientInfo);
            if (returnValue != 1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                errText = medcareInterfaceProxy.ErrMsg;
                return -1;
            }
            //chenxin 保持数据一致，先取消上传再重新上传
            //取消医保上传数据,重新上传.保证数据一致.   nieaj 2010-11-12
            returnValue = medcareInterfaceProxy.DeleteUploadedFeeDetailsInpatient(patientInfo, ref alFeeItemList);
            if (returnValue != 1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                errText = medcareInterfaceProxy.ErrMsg;
                return -1;

            }
            returnValue = medcareInterfaceProxy.PreBalanceInpatient(patientInfo, ref alFeeItemList);
            if (returnValue != 1)
            {
                medcareInterfaceProxy.Rollback();
                FS.FrameWork.Management.PublicTrans.RollBack();
                errText = medcareInterfaceProxy.ErrMsg;
                return -1;
            }

            returnValue = medcareInterfaceProxy.Disconnect();
            if (returnValue != 1)
            {
                medcareInterfaceProxy.Rollback();
                FS.FrameWork.Management.PublicTrans.RollBack();
                errText = medcareInterfaceProxy.ErrMsg;
                return -1;
            }

            //医保接口预结算完毕
            FS.FrameWork.Management.PublicTrans.RollBack();

            return 1;
        }

        /// <summary>
        /// 医保结算
        /// </summary>
        /// <param name="medcareInterfaceProxy"></param>
        /// <param name="patientInfo"></param>
        /// <param name="alFeeItemList"></param>
        /// <param name="isInBalance"></param>
        /// <param name="errText"></param>
        /// <returns></returns>
        public static long BalanceInpatient(ref FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy medcareInterfaceProxy, FS.HISFC.Models.RADT.PatientInfo patientInfo, ArrayList alFeeItemList, bool isInBalance, ref string errText)
        {
            //FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy medcareInterfaceProxy = new FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy();
            //long returnValue = medcareInterfaceProxy.SetPactCode(patientInfo.Pact.ID);
            //if (returnValue != 1)
            //{
            //    medcareInterfaceProxy.Rollback();
            //    errText = medcareInterfaceProxy.ErrMsg;
            //    return -1;
            //}
            ////待遇接口处理－－－－－－－－－－－－－－－－－－－－－－－－－－－－
            //FS.FrameWork.Management.PublicTrans.BeginTransaction();
            medcareInterfaceProxy.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            medcareInterfaceProxy.Connect();
            long returnValue = medcareInterfaceProxy.GetRegInfoInpatient(patientInfo);
            if (returnValue != 1)
            {
                medcareInterfaceProxy.Rollback();
                FS.FrameWork.Management.PublicTrans.RollBack();
                errText = medcareInterfaceProxy.ErrMsg;
                return -1;
            }

            if (isInBalance)
            {
                returnValue = medcareInterfaceProxy.MidBalanceInpatient(patientInfo, ref alFeeItemList);
            }
            else
            {
                returnValue = medcareInterfaceProxy.BalanceInpatient(patientInfo, ref alFeeItemList);
            }

            if (returnValue != 1)
            {
                medcareInterfaceProxy.Rollback();
                FS.FrameWork.Management.PublicTrans.RollBack();
                errText = medcareInterfaceProxy.ErrMsg;
                return -1;
            }

            returnValue = medcareInterfaceProxy.Disconnect();
            if (returnValue != 1)
            {
                medcareInterfaceProxy.Rollback();
                FS.FrameWork.Management.PublicTrans.RollBack();
                errText = medcareInterfaceProxy.ErrMsg;
                return -1;
            }
          //  FS.FrameWork.Management.PublicTrans.Commit();

            return returnValue;
        }

        #endregion

        #region 发票打印

        /// <summary>
        /// 打印发票
        /// </summary>
        /// <param name="balance"></param>
        /// <param name="alBalanceListHead"></param>
        /// <param name="alBalancePay"></param>
        public static int PrintInvoice(FS.HISFC.Models.RADT.PatientInfo patientInfo, List<FS.HISFC.Models.Fee.Inpatient.Balance> list,ref string errText)
        {
            FS.HISFC.BizProcess.Interface.FeeInterface.IBalanceInvoicePrintmy p = InterfaceManager.GetIBalanceInvoicePrint();
            if (p == null)
            {
                errText = "打印发票失败，发票接口未配置";
                return -1;
            }

            if (patientInfo.IsEncrypt)
            {
                patientInfo.Name = FS.FrameWork.WinForms.Classes.Function.Decrypt3DES(patientInfo.NormalName);
            }

            FS.HISFC.BizLogic.Fee.InPatient inpatientFeeManager = new FS.HISFC.BizLogic.Fee.InPatient();
            foreach (FS.HISFC.Models.Fee.Inpatient.Balance balance in list)
            {
                if (balance.IsTempInvoice)
                {
                    continue;
                }

                ArrayList alBalanceListHead = inpatientFeeManager.QueryBalanceListsByInpatientNOAndBalanceNO(balance.Patient.ID,balance.Invoice.ID, FS.FrameWork.Function.NConvert.ToInt32(balance.ID));
                ArrayList alBalancePay = inpatientFeeManager.QueryBalancePaysByInvoiceNOAndBalanceNO(balance.Invoice.ID, FS.FrameWork.Function.NConvert.ToInt32(balance.ID));

                p.Clear();
                if (alBalanceListHead.Count > 0)
                {
                    p.PatientInfo = patientInfo;
                    if (p.SetValueForPrint(patientInfo, balance, alBalanceListHead, alBalancePay) == -1)
                    {
                        errText = "打印发票失败，发票信息设置错误";
                        return -1;
                    }
                    //调打印类
                    p.Print();
                }
            }

            return 1;
        }

        /// <summary>
        /// 打印发票
        /// </summary>
        /// <param name="balance"></param>
        /// <param name="alBalanceListHead"></param>
        /// <param name="alBalancePay"></param>
        public static int PrintPreviewInvoice(FS.HISFC.Models.RADT.PatientInfo patientInfo, List<FS.HISFC.Models.Fee.Inpatient.Balance> list, ref string errText)
        {
            FS.HISFC.BizProcess.Interface.FeeInterface.IBalanceInvoicePrintmy p = InterfaceManager.GetIBalanceInvoicePrint();
            if (p == null)
            {
                errText = "打印发票失败，发票接口未配置";
                return -1;
            }

            if (patientInfo.IsEncrypt)
            {
                patientInfo.Name = FS.FrameWork.WinForms.Classes.Function.Decrypt3DES(patientInfo.NormalName);
            }

            FS.HISFC.BizLogic.Fee.InPatient inpatientFeeManager = new FS.HISFC.BizLogic.Fee.InPatient();
            foreach (FS.HISFC.Models.Fee.Inpatient.Balance balance in list)
            {
                //if (balance.IsTempInvoice)
                //{
                //    continue;
                //}

                ArrayList alBalanceListHead = inpatientFeeManager.QueryBalanceListsByInpatientNOAndBalanceNO(balance.Patient.ID, balance.Invoice.ID, FS.FrameWork.Function.NConvert.ToInt32(balance.ID));
                ArrayList alBalancePay = inpatientFeeManager.QueryBalancePaysByInvoiceNOAndBalanceNO(balance.Invoice.ID, FS.FrameWork.Function.NConvert.ToInt32(balance.ID));

                p.Clear();
                if (alBalanceListHead.Count > 0)
                {
                    p.PatientInfo = patientInfo;
                    if (p.SetValueForPreview(patientInfo, balance, alBalanceListHead, alBalancePay) == -1)
                    {
                        errText = "打印发票失败，发票信息设置错误";
                        return -1;
                    }
                    //调打印类
                    p.PrintPreview();
                }
            }

            return 1;
        }
        #endregion

        #region 字符串替换

        public static String ReplaceValues(String template, Dictionary<String, Object> map)
        {
            if (HasReplaceableValues(template))
            {
                StringBuilder tempSb = new StringBuilder(template);
                string tempKey = string.Empty;
                foreach (string key in map.Keys)
                {
                    tempKey = string.Concat('&', key);
                    if (template.Contains(tempKey))
                    {
                        if (map[key] != null)
                        {
                            tempSb.Replace(tempKey, map[key].ToString());
                        }
                    }
                }

                return tempSb.ToString();
            }
            else
            {
                return template;
            }
        }

        public static bool HasReplaceableValues(String str)
        {
            return ((str != null) && (str.IndexOf("&") > -1));
        }

        public static bool HasSelect(string str)
        {
            return ((str != null) && (str.ToUpper().IndexOf("SELECT") > -1));
        }

        #endregion

        #region 中间类

        /// <summary>
        /// 日结报表综合类
        /// </summary>
        /// <returns></returns>
        public static FS.SOC.HISFC.InpatientFee.BizProcess.DayReport GetDayReportBizProcess()
        {
            return new FS.SOC.HISFC.InpatientFee.BizProcess.DayReport();
        }

        private static FS.SOC.HISFC.InpatientFee.BizProcess.Fee sorBizProcessFee = new FS.SOC.HISFC.InpatientFee.BizProcess.Fee();
        /// <summary>
        /// 费用总和类
        /// </summary>
        /// <returns></returns>
        public static FS.SOC.HISFC.InpatientFee.BizProcess.Fee GetFeeBizProcess()
        {
            return sorBizProcessFee;
        }

        /// <summary>
        /// 读卡接口
        /// </summary>
        /// <param name="cardNO"></param>
        /// <param name="errInfo"></param>
        /// <returns></returns>
        public static int OperCard(ref string cardNO, ref string errInfo)
        {
            if (InterfaceManager.GetIOperCard() == null)
            {
                errInfo = "没有维护读卡接口！";
                return -1;
            }

            int result = InterfaceManager.GetIOperCard().ReadCardNO(ref cardNO, ref  errInfo);
            if (result == -1)
            {
                errInfo = "读卡失败，请确认是否正确放置诊疗卡！";
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 读取物理卡号
        /// {119F302E-69D9-445c-BF56-4109D975AD98}
        /// </summary>
        /// <param name="cardNO"></param>
        /// <param name="errInfo"></param>
        /// <returns></returns>
        public static int OperMCard(ref string McardNO, ref string errInfo)
        {
            if (InterfaceManager.GetIOperCard() == null)
            {
                errInfo = "没有维护读卡接口！";
                return -1;
            }

            int result = InterfaceManager.GetIOperCard().ReadMCardNO(ref McardNO, ref  errInfo);
            if (result == -1)
            {
                errInfo = "读卡失败，请确认是否正确放置诊疗卡！";
                return -1;
            }

            return 1;
        }

        #endregion
    }
}
