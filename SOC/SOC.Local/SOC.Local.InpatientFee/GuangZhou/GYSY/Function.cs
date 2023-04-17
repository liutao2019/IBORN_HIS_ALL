using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.SOC.Local.InpatientFee.GuangZhou.GYSY
{
    /// <summary>
    /// [功能描述: 功能函数]<br></br>
    /// [创 建 者: zhaoj]<br></br>
    /// [创建时间: 2011-10]<br></br>
    /// </summary>
    public  class Function
    {
        #region 医保

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
                ArrayList alBalanceListHead = inpatientFeeManager.QueryBalanceListsByInpatientNOAndBalanceNO(balance.Patient.ID,balance.Invoice.ID, FS.FrameWork.Function.NConvert.ToInt32(balance.ID));
                ArrayList alBalancePay = inpatientFeeManager.QueryBalancePaysByInvoiceNOAndBalanceNO(balance.Invoice.ID, FS.FrameWork.Function.NConvert.ToInt32(balance.ID));

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
                    balance.IsTempInvoice = false;
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

    }
}
