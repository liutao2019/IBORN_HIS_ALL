using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.BizProcess.MessagePatternInterface;
using System.Collections;
using FS.HISFC.Models.Fee.Outpatient;
using System.Data;
using FS.HISFC.Models.Base;
using FS.HISFC.Models.Registration;
using FS.FrameWork.Function;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.HL7.FinancialManagement.DFT_P03
{
    public class RecipeFee 
    {
        FS.HISFC.BizProcess.Integrate.Fee feeManager = new FS.HISFC.BizProcess.Integrate.Fee();
        FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParamMgr = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
        private int processMessaege(NHapi.Model.V24.Message.DFT_P03 o, ref NHapi.Model.V24.Message.ACK ackMessage, ref string errInfo)
        {
            FS.HISFC.BizLogic.Registration.Register regMgr = new FS.HISFC.BizLogic.Registration.Register();
            regMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            for (int i = 0; i < o.FINANCIALRepetitionsUsed; i++)
            {
                #region FT1-挂号信息

                //门诊流水号
                string clinicCode = o.GetFINANCIAL(i).FT1.AssignedPatientLocation.PointOfCare.Value;

                if (string.IsNullOrEmpty(clinicCode))
                {
                    errInfo = "门诊流水号为空，DFT_P03-FT1-16";
                    return -1;
                }

                FS.HISFC.Models.Registration.Register register = regMgr.GetByClinic(clinicCode);
                if (register == null)
                {
                    errInfo = "获取挂号信息失败，原因：" + regMgr.Err;
                    return -1;
                }

                if (string.IsNullOrEmpty(register.ID))
                {
                    errInfo = "传入的门诊流水号系统无法找到对应的挂号记录，DFT_P03-FT1-16=" + clinicCode;
                    return -1;
                }

                #endregion

                #region FT1-交易信息

                //收费序列
                string recipeSeqence = o.GetFINANCIAL(i).FT1.TransactionID.Value;
                if (string.IsNullOrEmpty(recipeSeqence))
                {
                    errInfo = "处方单序号为空，DFT_P03-FT1-2";
                    return -1;
                }

                //临时发票号
                string invoiceNO = o.GetFINANCIAL(i).FT1.TransactionCode.Text.Value ;
                if (string.IsNullOrEmpty(invoiceNO))
                {
                    errInfo = "临时发票号为空，DFT_P03-FT1-7.2";
                    return -1;
                }

                //查找费用信息
               ArrayList alFeeDetail= feeManager.QueryFeeDetailByClinicCodeAndRecipeSeq(clinicCode, recipeSeqence, "0");
               if (alFeeDetail == null)
               {
                   errInfo = "查找费用信息失败，原因：" + feeManager.Err;
                   return -1;
               }

               if (alFeeDetail.Count == 0)
               {
                   errInfo = "序号为" + recipeSeqence + "的处方单可能已经收费，请重试！";
                   return -1;
               }

                string error="";
                //收费
                if (this.ChargeFee(register, alFeeDetail, invoiceNO, null,out error) <= 0)
                {
                    errInfo = "收费失败，原因：" + error + "，处方单序号：" + recipeSeqence;
                    return -1;
                }

                #endregion

                #region ZPY-社保支付项目信息

                for (int num = 0; num < o.GetFINANCIAL(0).FINANCIAL_PAYMENTRepetitionsUsed; num++)
                {
                    //医疗费用支付项目
                    string Zfxm = o.GetFINANCIAL(0).GetFINANCIAL_PAYMENT(num).ZPY.Zfxm.Value;
                    //医疗费用支付项目金额
                    decimal Je = FS.FrameWork.Function.NConvert.ToDecimal(o.GetFINANCIAL(0).GetFINANCIAL_PAYMENT(num).ZPY.Je.Value);

                    //进行支付项目的存储
                }

                //更新挂号记录的OWN_COST,PUB_COST,PAY_COST

                #endregion

                #region IN1-社保结算项目信息

                for (int num = 0; num < o.GetFINANCIAL(i).FINANCIAL_INSURANCERepetitionsUsed; num++)
                {
                    //医疗结算项目
                    string Zfxm = o.GetFINANCIAL(i).GetFINANCIAL_INSURANCE(num).IN1.PolicyDeductible.Price.Denomination.Value;
                    //医疗结算项目金额
                    decimal Je = FS.FrameWork.Function.NConvert.ToDecimal(o.GetFINANCIAL(i).GetFINANCIAL_INSURANCE(num).IN1.PolicyDeductible.FromValue.Value);

                    //进行结算项目的存储
                }

               
                #endregion

                #region ZCT-健康卡交易信息

                NHapi.Model.V24.Segment.ZCT ZCT = o.GetFINANCIAL(i).ZCT;

                #endregion

                #region ZPR-社保交易信息

                NHapi.Model.V24.Segment.ZPR ZPR = o.GetFINANCIAL(i).ZPR;

                #endregion

                #region ACK-返回信息


                #endregion
            }

            return 1;
        }

        #region 门诊收费

        /// <summary>
        /// 预交金流程收费操作
        /// </summary>
        /// <param name="r">病人挂号信息</param>
        /// <param name="feeItemList">费用明细信息</param>
        /// <param name="strMsg">提示信息</param>
        /// <returns> -1 失败，0 账户余额不足，1 成功扣费 </returns>
        public int ChargeFee(FS.HISFC.Models.Registration.Register r, ArrayList feeItemList, string invoiceNO, FS.HISFC.Models.Base.Employee employee, out string strMsg)
        {

            strMsg = "";
            int lngRes = 1;
            if (r == null || string.IsNullOrEmpty(r.ID) || string.IsNullOrEmpty(r.Pact.ID))
            {
                strMsg = "患者信息为空！";
                return -1;
            }

            // {9635BF11-D633-409e-8880-2DB29CB830F7}
            //if (FS.HISFC.BizProcess.Integrate.AccountFee.Function.LstUnTerminalPactCode.Contains(r.Pact.ID))
            //{
            //    lngRes = -1;
            //    strMsg = r.Pact.Name + " 身份病人，请到收费处收费！";
            //    return lngRes;
            //}

            if (feeItemList == null || feeItemList.Count <= 0)
            {
                strMsg = "患者费用信息为空！";
                return 1;
            }

            // 指定一个固定终端员工
            if (employee == null || employee.ID.StartsWith("T") == false)
            {
                // 系统必须定义一个 T00001 的员工 为医生站扣费时分配发票用
                employee = new FS.HISFC.Models.Base.Employee();
                employee.ID = "T00001"; // 终端全院
                employee.Name = "自助终端机";
                employee.UserCode = "99";
            }

            // 生成发票信息
            Balance invoiceInfo = null;
            List<BalanceList> lstInvoiceDetial = null;

            List<FeeItemList> lstFeeItem = new List<FeeItemList>();
            lstFeeItem.AddRange((FeeItemList[])feeItemList.ToArray(typeof(FeeItemList)));

            lngRes = this.BuildInvoiceInfo(employee, r, lstFeeItem, invoiceNO, out invoiceInfo, out lstInvoiceDetial, out strMsg);
            if (lngRes <= 0 || invoiceInfo == null || lstInvoiceDetial == null || lstInvoiceDetial.Count <= 0)
            {
                return -1;
            }

            // 生成支付方式信息
            List<BalancePay> lstPayModes = this.MakeInvoicePayModes(invoiceInfo, ref strMsg);
            if (lstPayModes == null || lstPayModes.Count <= 0)
            {
                return -1;
            }

            bool blnRes = false;
            // 
            ArrayList arlInvoices = new ArrayList();
            ArrayList arlInvoiceDetial = new ArrayList();
            ArrayList arlPayModes = new ArrayList();
            ArrayList arlFeeItem = new ArrayList();
            ArrayList arlTemp = new ArrayList();

            // 发票主信息
            arlInvoices.Add(invoiceInfo);
            // 发票明细信息
            arlTemp.AddRange(lstInvoiceDetial.ToArray());
            ArrayList arlTemp2 = new ArrayList();
            arlTemp2.Add(arlTemp);
            arlInvoiceDetial.Add(arlTemp2);
            // 支付方式
            arlPayModes.AddRange(lstPayModes.ToArray());
            // 费用明细
            foreach (FeeItemList item in lstFeeItem)
            {
                // 设置为帐户扣费
                item.IsAccounted = true;
            }
            arlFeeItem.AddRange(lstFeeItem.ToArray());

            strMsg = "";
            if (feeManager.ClinicFee(FS.HISFC.Models.Base.ChargeTypes.Fee, "C", true, r, arlInvoices, arlInvoiceDetial, arlFeeItem, new ArrayList(), arlPayModes, ref strMsg, employee) == false)
            {
                return -1;
            }

            return 1;
        }

        #endregion

        #region 生成发票信息

        /// <summary>
        /// 生成发票信息。
        /// 发票信息、发票明细信息
        /// </summary>
        /// <param name="register">挂号信息</param>
        /// <param name="lstFeeItems">费用明细信息</param>
        /// <param name="invoiceInfo">发票信息</param>
        /// <param name="lstInvoiceDetial">发票明细信息</param>
        /// <param name="errText">错误信息</param>
        /// <returns>1 成功，-1 失败</returns>
        public int BuildInvoiceInfo(Employee employee, Register register, List<FeeItemList> lstFeeItems,string invoiceNO, out Balance invoiceInfo, out List<BalanceList> lstInvoiceDetial, out string errText)
        {
            invoiceInfo = null;
            lstInvoiceDetial = null;
            errText = "";
            string realInvoiceNO = invoiceNO;

            #region 获取发票大类
            DataSet dsInvoice = new DataSet();

            //string invoicePrintDll = controlParamMgr.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.Const.INVOICEPRINT, false, string.Empty);
            //if (string.IsNullOrEmpty(invoicePrintDll))
            //{
            //    errText = "没有维护发票打印方案，请维护";

            //    return -1;
            //}

            //invoicePrintDll = FS.FrameWork.WinForms.Classes.Function.CurrentPath + invoicePrintDll;
            //FS.HISFC.BizProcess.Interface.FeeInterface.IInvoicePrint iInvoicePrint = null;
            //object obj = null;
            //System.Reflection.Assembly a = System.Reflection.Assembly.LoadFrom(invoicePrintDll);
            //try
            //{
            //    System.Type[] types = a.GetTypes();
            //    foreach (System.Type type in types)
            //    {
            //        if (type.GetInterface("IInvoicePrint") != null)
            //        {
            //            obj = System.Activator.CreateInstance(type);
            //            iInvoicePrint = obj as FS.HISFC.BizProcess.Interface.FeeInterface.IInvoicePrint;

            //            break;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    errText = ex.Message;
            //    return -1;
            //}
            //iInvoicePrint.Register = register;
            this.feeManager.GetInvoiceClass("MZ01", ref dsInvoice);

            if (dsInvoice == null || dsInvoice.Tables.Count <= 0)
            {
                errText = "没有维护发票大类，请维护";
                return -1;
            }
            if (dsInvoice.Tables[0].PrimaryKey.Length == 0)
            {
                dsInvoice.Tables[0].PrimaryKey = new DataColumn[] { dsInvoice.Tables[0].Columns["FEE_CODE"] };
            }
            #endregion

            invoiceInfo = this.MakeInvoiceInfo(lstFeeItems, register, invoiceNO, realInvoiceNO);
            if (invoiceInfo == null)
            {
                errText = "生成发票信息失败！";
                return -1;
            }

            lstInvoiceDetial = this.MakeInvoiceDetail(lstFeeItems, register, invoiceNO, dsInvoice.Tables[0], ref errText);
            if (lstInvoiceDetial == null)
            {
                return -1;
            }

            return 1;
        }

        #region 生成主发票信息
        /// <summary>
        /// 生成主发票信息
        /// </summary>
        /// <param name="feeItemLists">费用明细集合</param>
        /// <param name="register">挂号信息</param>
        /// <param name="invoiceNO">发票号</param>
        /// <param name="realInvoiceNO">实际发票号</param>
        /// <returns></returns>
        private Balance MakeInvoiceInfo(List<FeeItemList> lstFeeItems, Register register, string invoiceNO, string realInvoiceNO)
        {
            Balance invoice = new Balance();
            decimal totCost = 0;//总金额
            decimal ownCost = 0;//自费金额
            decimal pubCost = 0;//记帐金额
            decimal payCost = 0;//自付金额
            decimal rebateCost = 0;//优惠价格

            foreach (FeeItemList feeItem in lstFeeItems)
            {
                ownCost += feeItem.FT.OwnCost;
                pubCost += feeItem.FT.PubCost;
                payCost += feeItem.FT.PayCost;
                rebateCost += feeItem.FT.RebateCost;
            }
            totCost = ownCost + pubCost + payCost;

            invoice.Invoice.ID = invoiceNO;
            invoice.TransType = FS.HISFC.Models.Base.TransTypes.Positive;
            invoice.Patient = register.Clone();

            invoice.FT.OwnCost = ownCost;
            invoice.FT.PayCost = payCost;
            invoice.FT.PubCost = pubCost;
            invoice.FT.TotCost = totCost;
            invoice.FT.RebateCost = rebateCost;
            invoice.User01 = rebateCost.ToString();

            if (string.IsNullOrEmpty(register.ChkKind))
            {
                invoice.ExamineFlag = "0";
            }
            else
            {
                invoice.ExamineFlag = register.ChkKind;
            }

            invoice.CancelType = FS.HISFC.Models.Base.CancelTypes.Valid;
            invoice.CanceledInvoiceNO = "";
            invoice.IsDayBalanced = false;
            invoice.PrintTime =CommonInterface.CommonController.CreateInstance().GetSystemTime();
            invoice.PrintedInvoiceNO = realInvoiceNO;
            invoice.IsAccount = true;

            return invoice;
        }
        #endregion

        #region 生成发票明细
        /// <summary>
        /// 生成发票明细
        /// </summary>
        /// <param name="feeItemLists">费用明细集合</param>
        /// <param name="register">挂号信息</param>
        /// <param name="invoiceNO">发票号</param>
        /// <param name="dtInvoice">发票大类</param>
        /// <param name="errText">错误信息</param>
        /// <returns>成功: 发票明细集合 失败 null</returns>
        private List<BalanceList> MakeInvoiceDetail(List<FeeItemList> lstFeeItems, Register register, string invoiceNO, DataTable dtInvoice, ref string errText)
        {
            List<BalanceList> balanceLists = new List<BalanceList>();

            foreach (FeeItemList f in lstFeeItems)
            {
                DataRow rowFind = dtInvoice.Rows.Find(new object[] { f.Item.MinFee.ID });
                if (rowFind == null)
                {
                    errText = "最小费用为" + f.Item.MinFee.ID + "的最小费用没有在MZ01的发票大类中维护";
                    return null;
                }

                rowFind["TOT_COST"] = NConvert.ToDecimal(rowFind["TOT_COST"].ToString()) + f.FT.OwnCost + f.FT.PubCost + f.FT.PayCost;
                rowFind["OWN_COST"] = NConvert.ToDecimal(rowFind["OWN_COST"].ToString()) + f.FT.OwnCost;
                rowFind["PUB_COST"] = NConvert.ToDecimal(rowFind["PUB_COST"].ToString()) + f.FT.PubCost;
                rowFind["PAY_COST"] = NConvert.ToDecimal(rowFind["PAY_COST"].ToString()) + f.FT.PayCost;
            }

            BalanceList detail = null;//发票明细实体

            for (int i = 1; i < 100; i++)
            {
                //找到相同打印序号,即同一统计类别的费用集合
                DataRow[] rowFind = dtInvoice.Select("SEQ = " + i.ToString(), "SEQ ASC");
                //如果没有找到说明已经找过了最大的打印序号,所有费用已经累加完毕.
                if (rowFind.Length == 0)
                {

                }
                else
                {
                    detail = new BalanceList();
                    detail.BalanceBase.Invoice.ID = invoiceNO;
                    detail.BalanceBase.TransType = FS.HISFC.Models.Base.TransTypes.Positive;
                    detail.InvoiceSquence = i;
                    detail.FeeCodeStat.ID = rowFind[0]["FEE_STAT_CATE"].ToString();
                    detail.FeeCodeStat.Name = rowFind[0]["FEE_STAT_NAME"].ToString();

                    /// 保存打印序号到实体。
                    ///----------------------------------------------------
                    detail.FeeCodeStat.SortID = NConvert.ToInt32(rowFind[0]["SEQ"].ToString());
                    ///---------------------------------------------------
                    detail.BalanceBase.IsDayBalanced = false;
                    detail.BalanceBase.CancelType = FS.HISFC.Models.Base.CancelTypes.Valid;
                    foreach (DataRow row in rowFind)
                    {
                        detail.BalanceBase.FT.PubCost += NConvert.ToDecimal(row["PUB_COST"].ToString());
                        detail.BalanceBase.FT.OwnCost += NConvert.ToDecimal(row["OWN_COST"].ToString());
                        detail.BalanceBase.FT.PayCost += NConvert.ToDecimal(row["PAY_COST"].ToString());
                    }
                    detail.BalanceBase.FT.TotCost = detail.BalanceBase.FT.PubCost + detail.BalanceBase.FT.OwnCost + detail.BalanceBase.FT.PayCost;
                    //如果费用为0 说明次统计类别(打印序号)下没有费用
                    if (detail.BalanceBase.FT.TotCost <= 0)
                    {
                        continue;
                    }

                    balanceLists.Add(detail);
                }
            }

            return balanceLists;
        }
        #endregion

        #endregion

        #region 生成支付方式

        /// <summary>
        /// 生成发票支付方式
        /// 门诊账户支付
        /// </summary>
        /// <param name="invoiceInfo">发票信息</param>
        /// <param name="errText">错误信息</param>
        /// <returns></returns>
        public List<BalancePay> MakeInvoicePayModes(Balance invoiceInfo, ref string errText)
        {
            List<BalancePay> lstBalancePay = new List<BalancePay>();
            BalancePay payMode = null;

            if (invoiceInfo.FT.OwnCost > invoiceInfo.FT.RebateCost)
            {
                // 用健康卡支付，系统固定为“JK”
                payMode = new BalancePay();
                payMode.PayType.ID = "JK";
                payMode.PayType.Name = "健康卡记账";
                payMode.FT.TotCost = invoiceInfo.FT.OwnCost - invoiceInfo.FT.RebateCost;

                lstBalancePay.Add(payMode);
            }

            return lstBalancePay;
        }

        #endregion

        public  int ProcessMessage(NHapi.Model.V24.Message.DFT_P03 o, ref NHapi.Model.V24.Message.ACK ackMessage,ref string errInfo)
        {
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            feeManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            controlParamMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            if (this.processMessaege(o, ref ackMessage,ref errInfo) <= 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                return -1;
            }

            FS.FrameWork.Management.PublicTrans.Commit();
            return 1;
        }

    }
}
