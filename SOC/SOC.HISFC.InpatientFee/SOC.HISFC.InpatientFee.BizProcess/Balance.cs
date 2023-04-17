using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using FS.HISFC.Models.Fee.Inpatient;
using FS.HISFC.Models.Account;
using FS.SOC.HISFC.BizProcess.CommonInterface;
using FS.SOC.HISFC.BizProcess.CommonInterface.Common;
using FS.HISFC.Models.MedicalPackage.Fee;

namespace FS.SOC.HISFC.InpatientFee.BizProcess
{
    /// <summary>
    /// [功能描述: 结算逻辑业务类]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2012-3]<br></br>
    /// </summary>
    public class Balance : AbstractBizProcess
    {
        FS.HISFC.BizLogic.Fee.InPatient inpatientFeeManager = new FS.HISFC.BizLogic.Fee.InPatient();
        /// <summary>
        /// 门诊帐户类业务层
        /// </summary>
        protected static FS.HISFC.BizLogic.Fee.Account accountManager = new FS.HISFC.BizLogic.Fee.Account();

        /// <summary>
        /// 门诊帐户类业务层{795AA18A-0093-492b-AAB9-DE654606A309}
        /// </summary>
        protected static FS.HISFC.BizProcess.Integrate.RADT RadtIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();

        FS.HISFC.BizProcess.Integrate.Fee packagecostMange = new FS.HISFC.BizProcess.Integrate.Fee();

        FS.HISFC.BizLogic.MedicalPackage.Fee.Package feepackManager = new FS.HISFC.BizLogic.MedicalPackage.Fee.Package();
        /// <summary>
        /// 控制参数
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        #region 内部方法

        /// <summary>
        /// 结算费用转为费用大类
        /// </summary>
        /// <param name="alFeeInfo">最小费用汇总费用信息</param>
        /// <param name="alBalanceList"></param>
        /// <param name="dtSys">结算时间</param>
        /// <param name="balNo">结算序号</param>
        /// <param name="invoiceNo">发票序列号</param>
        /// <param name="alFeeState">费用大类名称数组</param>
        /// <returns></returns>
        protected virtual int FeeInfoTransFeeStat(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.HISFC.Models.Base.EBlanceType balanceType, ArrayList alFeeInfo, List<FS.HISFC.Models.Fee.Inpatient.BalanceList> alBalanceList, DateTime dtSys, int balNo, string invoiceNO, ArrayList alFeeState)
        {
            inpatientFeeManager.SetTrans(this.Trans);

            #region BalanceList

            FS.HISFC.Models.Fee.Inpatient.FeeInfo f;
            for (int i = 0; i < alFeeInfo.Count; i++)
            {
                //取得一条费用记录

                f = (FS.HISFC.Models.Fee.Inpatient.FeeInfo)alFeeInfo[i];
                //判断是否存在和该条费用feecode对应的统计大类,存在即取出大类
                if (f.FT.TotCost == 0)
                {
                    continue;
                }

                FS.FrameWork.Models.NeuObject objFeeStat = new FS.FrameWork.Models.NeuObject();
                objFeeStat = this.GetFeeStatByFeeCode(f.Item.MinFee.ID, alFeeState);
                if (objFeeStat == null)
                {
                    string feeName = "";
                    feeName = inpatientFeeManager.GetMinFeeNameByCode(f.Item.MinFee.ID);
                    this.err = "请维护发票对照中最小费用为" + feeName + "的发票项目";
                    return -1;
                }
                //去balancelist相关的集合里去找有没有此大类费用有的话费用相加,没有数组add一条新记录
                FS.HISFC.Models.Fee.Inpatient.BalanceList balanceList;
                bool BFind = false;
                for (int j = 0; j < alBalanceList.Count; j++)
                {
                    balanceList = (FS.HISFC.Models.Fee.Inpatient.BalanceList)alBalanceList[j];
                    if (balanceList.FeeCodeStat.StatCate.ID == objFeeStat.ID && balanceList.BalanceBase.SplitFeeFlag == f.SplitFeeFlag)
                    {
                        BFind = true;
                        balanceList.BalanceBase.FT.OwnCost += f.FT.OwnCost;
                        balanceList.BalanceBase.FT.TotCost += f.FT.TotCost;
                        balanceList.BalanceBase.FT.PayCost += f.FT.PayCost;
                        balanceList.BalanceBase.FT.PubCost += f.FT.PubCost;
                        balanceList.BalanceBase.FT.DonateCost += f.FT.DonateCost; //{18AD984D-78C6-4c64-8066-6F31E3BDF7DA}
                        balanceList.BalanceBase.FT.RebateCost += f.FT.RebateCost;
                        balanceList.BalanceBase.FT.ArrearCost += f.FT.ArrearCost;
                    }
                }
                //没有找到所以像数组添加一条记录
                if (BFind == false)
                {
                    FS.HISFC.Models.Fee.Inpatient.BalanceList balanceListAdd = new FS.HISFC.Models.Fee.Inpatient.BalanceList();
                    //实体赋值

                    balanceListAdd.FeeCodeStat.StatCate.ID = objFeeStat.ID;
                    balanceListAdd.FeeCodeStat.StatCate.Name = objFeeStat.Name;
                    balanceListAdd.FeeCodeStat.SortID = int.Parse(objFeeStat.Memo);
                    balanceListAdd.BalanceBase.FT.TotCost = f.FT.TotCost;
                    balanceListAdd.BalanceBase.FT.OwnCost = f.FT.OwnCost;
                    balanceListAdd.BalanceBase.FT.PayCost = f.FT.PayCost;
                    balanceListAdd.BalanceBase.FT.PubCost = f.FT.PubCost;
                    balanceListAdd.BalanceBase.FT.DonateCost += f.FT.DonateCost; //{18AD984D-78C6-4c64-8066-6F31E3BDF7DA}
                    balanceListAdd.BalanceBase.FT.RebateCost += f.FT.RebateCost;
                    balanceListAdd.BalanceBase.FT.ArrearCost += f.FT.ArrearCost;
                    balanceListAdd.BalanceBase.Invoice.ID = invoiceNO;
                    balanceListAdd.BalanceBase.Patient.IsBaby = patientInfo.IsBaby;
                    balanceListAdd.ID = balNo.ToString();
                    balanceListAdd.BalanceBase.ID = balNo.ToString();
                    balanceListAdd.BalanceBase.TransType = FS.HISFC.Models.Base.TransTypes.Positive;
                    balanceListAdd.BalanceBase.BalanceOper.ID = inpatientFeeManager.Operator.ID;
                    balanceListAdd.BalanceBase.BalanceOper.OperTime = dtSys;
                    balanceListAdd.BalanceBase.CancelType = FS.HISFC.Models.Base.CancelTypes.Valid;
                    balanceListAdd.BalanceBase.SplitFeeFlag = f.SplitFeeFlag;
                    if (balanceType == FS.HISFC.Models.Base.EBlanceType.Mid)
                    {
                        balanceListAdd.BalanceBase.BalanceType.ID = FS.HISFC.Models.Fee.EnumBalanceType.I;
                    }
                    else if (balanceType == FS.HISFC.Models.Base.EBlanceType.Out)
                    {
                        balanceListAdd.BalanceBase.BalanceType.ID = FS.HISFC.Models.Fee.EnumBalanceType.O;
                    }
                    else if (balanceType == FS.HISFC.Models.Base.EBlanceType.Owe)
                    {
                        balanceListAdd.BalanceBase.BalanceType.ID = FS.HISFC.Models.Fee.EnumBalanceType.Q;
                    }
                    else if (balanceType == FS.HISFC.Models.Base.EBlanceType.ItemBalance)
                    {
                        balanceListAdd.BalanceBase.BalanceType.ID = FS.HISFC.Models.Fee.EnumBalanceType.D;
                    }
                    else if (balanceType == FS.HISFC.Models.Base.EBlanceType.OweMid)
                    {
                        balanceListAdd.BalanceBase.BalanceType.ID = FS.HISFC.Models.Fee.EnumBalanceType.Q;
                    }
                    else
                    {
                        balanceListAdd.BalanceBase.BalanceType.ID = FS.HISFC.Models.Fee.EnumBalanceType.O;
                    }

                    alBalanceList.Add(balanceListAdd);
                }
                BFind = false;

            }

            #endregion

            return 1;
        }
        /// <summary>
        /// 通过最小费用获取统计大类memo存打印顺序
        /// </summary>
        /// <param name="feeCode"></param>
        /// <param name="alInvoice"></param>
        /// <returns></returns>
        private FS.FrameWork.Models.NeuObject GetFeeStatByFeeCode(string feeCode, ArrayList al)
        {
            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
            FS.HISFC.Models.Fee.FeeCodeStat feeStat;

            for (int i = 0; i < al.Count; i++)
            {
                feeStat = (FS.HISFC.Models.Fee.FeeCodeStat)al[i];
                if (feeStat.MinFee.ID == feeCode)
                {
                    obj.ID = feeStat.StatCate.ID;
                    obj.Name = feeStat.StatCate.Name;
                    obj.Memo = feeStat.SortID.ToString();
                    return obj;
                }
            }
            return null;
        }

        #endregion

        #region 出院结算

        /// <summary>
        /// 住院结算
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="balanceType"></param>
        /// <param name="feeItemList"></param>
        /// <param name="prepayList"></param>
        /// <param name="balancePayList"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="splitInvoiceCost"></param>
        /// <param name="invoiceNO"></param>
        /// <param name="realInvoiceNO"></param>
        /// <returns></returns>
        public int OutBalance(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.HISFC.Models.Base.EBlanceType balanceType,
            List<FS.HISFC.Models.Fee.Inpatient.FeeInfo> feeInfoList,
            List<FS.HISFC.Models.Fee.Inpatient.Prepay> prepayList,
            List<FS.HISFC.Models.Fee.Inpatient.BalancePay> balancePayList,
            bool splitHighInvoice,
            decimal splitHighInvoiceCost,
            ref List<FS.HISFC.Models.Fee.Inpatient.Balance> listBalance)
        {
            if (patientInfo == null || string.IsNullOrEmpty(patientInfo.ID))
            {
                this.err = "结算失败，患者信息为空！";
                return -1;
            }

            this.BeginTransaction();

            #region 患者信息验证
            RADT radtManager = new RADT();
            FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParamMgr = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
            radtManager.SetTrans(this.Trans);
            controlParamMgr.SetTrans(this.Trans);
            //获取时间
            DateTime sysdate = inpatientFeeManager.GetDateTimeFromSysDateTime();

            DateTime inDate = patientInfo.PVisit.InTime;
            //获取上次结算日期
            ArrayList alBalance = inpatientFeeManager.QueryBalancesByInpatientNO(patientInfo.ID, "I");
            if (alBalance != null)
            {
                foreach (FS.HISFC.Models.Fee.Inpatient.Balance balancedHead in alBalance)
                {
                    if (balancedHead.CancelType == FS.HISFC.Models.Base.CancelTypes.Valid &&
                        inDate < balancedHead.EndTime)
                    {
                        inDate = balancedHead.EndTime.AddSeconds(1);
                    }
                }
            }

            //验证患者信息
            FS.HISFC.Models.RADT.PatientInfo patientInfoTemp = radtManager.GetPatientInfo(patientInfo.ID);
            if (patientInfoTemp == null || string.IsNullOrEmpty(patientInfoTemp.ID))
            {
                this.RollBack();
                this.err = "患者信息为空，" + radtManager.Err;
                return -1;
            }

            //已经出院的返回
            if (patientInfoTemp.PVisit.InState.ID.ToString().Equals(FS.HISFC.Models.Base.EnumInState.O))
            {
                this.RollBack();
                this.err = "患者已经出院结算";
                return -1;
            }
            //担保金允许出院结算后返回
            //非出院登记和封帐状态的患者提示错误
            if (!patientInfoTemp.PVisit.InState.ID.ToString().Equals(FS.HISFC.Models.Base.EnumInState.B.ToString())
                &&
                !patientInfoTemp.PVisit.InState.ID.ToString().Equals(FS.HISFC.Models.Base.EnumInState.C.ToString()))
            {
                this.RollBack();
                this.err = "患者状态不是出院登记,不能进行出院结算！" + patientInfo.PVisit.InState.ID;
                return -1;
            }


            #endregion

            #region 发票号和结算号

            //调业务层获取结算次数
            int balanceNO = 0;
            string balNo = inpatientFeeManager.GetNewBalanceNO(patientInfo.ID);
            if (balNo == null || balNo.Length == 0)
            {
                this.RollBack();
                this.err = "获取结算序号出错，" + inpatientFeeManager.Err;
                return -1;
            }
            else
            {
                balanceNO = int.Parse(balNo);
            }

            FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();
            feeIntegrate.SetTrans(this.Trans);
            inpatientFeeManager.SetTrans(this.Trans);
            string invoiceNO = string.Empty;
            string realInvoiceNO = string.Empty;
            string errText = "";

            if (feeIntegrate.GetInvoiceNO("I", ref invoiceNO, ref realInvoiceNO, ref errText) < 1 || string.IsNullOrEmpty(invoiceNO) || string.IsNullOrEmpty(realInvoiceNO))
            {
                this.RollBack();
                this.err = "获取发票失败，" + feeIntegrate.Err;
                return -1;
            }

            #endregion

            #region 处理预交金信息

            decimal TotPrepayCost = 0.00M;
            if (prepayList != null && prepayList.Count > 0)
            {
                foreach (FS.HISFC.Models.Fee.Inpatient.Prepay prepay in prepayList)
                {
                    prepay.BalanceNO = balanceNO;
                    prepay.Invoice.ID = invoiceNO;
                    prepay.BalanceOper.ID = this.inpatientFeeManager.Operator.ID;
                    prepay.BalanceOper.OperTime = sysdate;

                    TotPrepayCost += prepay.FT.PrepayCost;//预交金金额

                    //转入预交金处理（目前缺失更新单条的）
                    if (prepay.Name == "转入预交金")
                    {
                        if (inpatientFeeManager.UpdateChangePrepayBalanced(patientInfo.ID, balanceNO) == -1)
                        {
                            this.RollBack();
                            this.err = "结算失败，原因：" + inpatientFeeManager.Err;
                            return -1;
                        }
                    }
                    else
                    {
                        //更新预交发票为结算状态
                        if (inpatientFeeManager.UpdatePrepayBalanced(patientInfo, prepay) <= 0)
                        {
                            this.RollBack();
                            this.err = "结算失败，原因：" + inpatientFeeManager.Err;
                            return -1;
                        }
                    }
                }
            }

            #endregion

            #region 处理费用信息

            //出院结算更新费用
            //判断并发操作
            decimal TotOwnCost = 0.00M;
            decimal TotPubCost = 0.00M;
            decimal TotPayCost = 0.00M;
            decimal TotBalanceCost = 0.00M;
            decimal TotRebateCost = 0.00M;//获得费用总额
            decimal TotShouldPay = 0.00M;
            decimal TotDerateCost = 0.00M;
            //高收费
            decimal TotHighOwnCost = 0.00M;

            foreach (FS.HISFC.Models.Fee.Inpatient.FeeInfo feeInfoHead in feeInfoList)
            {
                TotBalanceCost += feeInfoHead.FT.BalancedCost;
                TotOwnCost += feeInfoHead.FT.OwnCost;
                TotPubCost += feeInfoHead.FT.PubCost;
                TotPayCost += feeInfoHead.FT.PayCost;
                TotRebateCost += feeInfoHead.FT.RebateCost;

                if (feeInfoHead.SplitFeeFlag)
                {
                    TotHighOwnCost += feeInfoHead.FT.OwnCost;
                }

                //如果是公费患者
                if (patientInfo.Pact.PayKind.ID == "03")
                {
                    TotShouldPay += feeInfoHead.FT.OwnCost + feeInfoHead.FT.PayCost;
                }
                else
                {
                    TotShouldPay += feeInfoHead.FT.OwnCost;
                }
            }

            if (patientInfo.Pact.PayKind.ID == "02")
            {
                TotShouldPay = patientInfo.SIMainInfo.OwnCost + patientInfo.SIMainInfo.PayCost;
            }

            //获取减免金额
            TotDerateCost = inpatientFeeManager.GetTotDerateCost(patientInfo.ID);
            if (TotDerateCost < 0)
            {
                this.RollBack();
                this.err = "获取减免总费用出错！" + this.inpatientFeeManager.Err;
                return -1;
            }

            TotShouldPay = TotShouldPay - TotDerateCost - TotRebateCost;

            int result = inpatientFeeManager.UpdateFeeInfoBalanced(patientInfo.ID, balanceNO, sysdate, invoiceNO, false);
            if (result == 0)
            {
                this.RollBack();
                this.err = "可能存在并发操作导致更新费用失败！";
                return -1;
            }

            if (result < 1)
            {
                this.RollBack();
                this.err = inpatientFeeManager.Err;
                return -1;
            }

            if (inpatientFeeManager.UpdateFeeItemListBalanced(patientInfo.ID, balanceNO, invoiceNO, false) == -1)
            {
                this.RollBack();
                this.err = inpatientFeeManager.Err;
                return -1;
            }

            if (inpatientFeeManager.UpdateMedItemListBalanced(patientInfo.ID, balanceNO, invoiceNO) == -1)
            {
                this.RollBack();
                this.err = inpatientFeeManager.Err;
                return -1;
            }

            //更新转入费用
            if (inpatientFeeManager.UpdateAllChangeCostBalanced(patientInfo.ID, balanceNO) == -1)
            {
                this.RollBack();
                this.err = inpatientFeeManager.Err;
                return -1;
            }

            //更新减免为已结算
            if (inpatientFeeManager.UpdateDerateBalanced(patientInfo.ID, balanceNO, invoiceNO) == -1)
            {
                this.RollBack();
                this.err = inpatientFeeManager.Err;
                return -1;
            }

            #endregion

            #region 处理支付方式
            decimal TotBalancePayCost = 0.00M;
            if (balancePayList != null && balancePayList.Count > 0)
            {
                decimal baseCost = 0;
                decimal donateCost = 0;
                string accountNo = string.Empty;
                string accountTypeCode = string.Empty;

                foreach (FS.HISFC.Models.Fee.Inpatient.BalancePay balancePay in balancePayList)
                {
                    TotBalancePayCost += balancePay.FT.TotCost;

                    balancePay.Invoice.ID = invoiceNO;
                    balancePay.BalanceNO = balanceNO;
                    balancePay.BalanceOper.ID = inpatientFeeManager.Operator.ID;
                    balancePay.BalanceOper.OperTime = sysdate;
                    //添加记录
                    if (inpatientFeeManager.InsertBalancePay(balancePay) == -1)
                    {
                        this.RollBack();
                        this.err = inpatientFeeManager.Err;
                        return -1;
                    }
                    if (balancePay.PayType.ID == "YS")// {970D1FA7-19B2-4fad-992E-922156E3F10D}
                    {
                        baseCost = -balancePay.FT.TotCost;
                        accountNo = balancePay.AccountNo;
                        accountTypeCode = balancePay.AccountTypeCode;
                    }

                    if (balancePay.PayType.ID == "DC")
                    {
                        donateCost = -balancePay.FT.TotCost;
                        accountNo = balancePay.AccountNo;
                        accountTypeCode = balancePay.AccountTypeCode;
                    }
                }
                #region 账户扣费
                if (baseCost != 0 || donateCost != 0)// {970D1FA7-19B2-4fad-992E-922156E3F10D}
                {
                    FS.HISFC.BizProcess.Integrate.Account.AccountPay accountPay = new FS.HISFC.BizProcess.Integrate.Account.AccountPay();
                    if (string.IsNullOrEmpty(accountNo))
                    {
                        errText = "没有找到账户信息!";
                        return -1;
                    }
                    int returnValue1 = accountPay.OutpatientPay(patientInfo, accountNo, accountTypeCode, baseCost, donateCost, invoiceNO, patientInfo, FS.HISFC.Models.Account.PayWayTypes.I, 1);
                    if (returnValue1 < 0)
                    {
                        errText = "账户结算出错!";
                        return -1;
                    }
                }
                #endregion
            }

            //欠费金额
            decimal arrearFeeCost = TotBalanceCost - TotBalancePayCost;
            arrearFeeCost = arrearFeeCost > 0 ? arrearFeeCost : 0;

            //欠费金额等于结算金额-支付方式金额
            if (arrearFeeCost > 0 && balanceType == FS.HISFC.Models.Base.EBlanceType.Owe)
            {
                FS.HISFC.Models.Fee.Inpatient.BalancePay balancePay = new FS.HISFC.Models.Fee.Inpatient.BalancePay();
                balancePay.PayType.ID = "QF";
                balancePay.PayType.Name = "欠费";
                balancePay.FT.TotCost = arrearFeeCost;
                balancePay.TransKind.ID = "1";
                balancePay.TransType = FS.HISFC.Models.Base.TransTypes.Positive;
                balancePay.Qty = 1;
                balancePay.RetrunOrSupplyFlag = "1";//欠费
                balancePay.Invoice.ID = invoiceNO;
                balancePay.BalanceNO = balanceNO;
                balancePay.BalanceOper.ID = inpatientFeeManager.Operator.ID;
                balancePay.BalanceOper.OperTime = sysdate;
                //添加记录
                if (inpatientFeeManager.InsertBalancePay(balancePay) == -1)
                {
                    this.RollBack();
                    this.err = inpatientFeeManager.Err;
                    return -1;
                }
            }
            #endregion

            #region 处理发票信息

            string invoiceType = "ZY01";

            FS.HISFC.BizLogic.Fee.FeeCodeStat feeCodeStat = new FS.HISFC.BizLogic.Fee.FeeCodeStat();
            ArrayList alFeeState = feeCodeStat.QueryFeeCodeStatByReportCode(invoiceType);
            if (alFeeState == null)
            {
                this.RollBack();
                this.err = "结算失败，原因：" + feeCodeStat.Err;
                return -1;
            }

            #region 欠费发票

            List<FS.HISFC.Models.Fee.Inpatient.BalanceList> alArrearBalanceList = new List<FS.HISFC.Models.Fee.Inpatient.BalanceList>();
            if (arrearFeeCost > 0 && balanceType == FS.HISFC.Models.Base.EBlanceType.OweMid)
            {
                ArrayList alArrearManualFeeInfo = new ArrayList();
                decimal arrearFeeCostTemp = arrearFeeCost;

                decimal splitRate = arrearFeeCost / TotBalanceCost;
                if (splitRate > 1)
                {
                    this.RollBack();
                    this.err = "欠费金额大于结算金额";
                    return -1;
                }
                //循环处理分票明细及主发票明细
                for (int i = 0; i < feeInfoList.Count; i++)
                {
                    FS.HISFC.Models.Fee.Inpatient.FeeInfo InvoiceHeadInfo = feeInfoList[i];
                    FS.HISFC.Models.Fee.Inpatient.FeeInfo InvoiceManualInfo = InvoiceHeadInfo.Clone();

                    InvoiceManualInfo.FT.OwnCost = FS.FrameWork.Function.NConvert.ToDecimal(FS.FrameWork.Public.String.FormatNumberReturnString(InvoiceHeadInfo.FT.OwnCost * splitRate, 2));
                    InvoiceManualInfo.FT.PayCost = 0.00M;
                    InvoiceManualInfo.FT.PubCost = 0.00M;
                    InvoiceManualInfo.FT.RebateCost = 0.00M;

                    InvoiceManualInfo.FT.TotCost = InvoiceManualInfo.FT.OwnCost + InvoiceManualInfo.FT.PayCost + InvoiceManualInfo.FT.PubCost + InvoiceManualInfo.FT.RebateCost;

                    InvoiceHeadInfo.FT.TotCost = InvoiceHeadInfo.FT.TotCost - InvoiceManualInfo.FT.TotCost;
                    InvoiceHeadInfo.FT.OwnCost = InvoiceHeadInfo.FT.OwnCost - InvoiceManualInfo.FT.OwnCost;
                    InvoiceHeadInfo.FT.PayCost = InvoiceHeadInfo.FT.PayCost - InvoiceManualInfo.FT.PayCost;
                    InvoiceHeadInfo.FT.PubCost = InvoiceHeadInfo.FT.PubCost - InvoiceManualInfo.FT.PubCost;
                    InvoiceHeadInfo.FT.RebateCost = InvoiceHeadInfo.FT.RebateCost - InvoiceManualInfo.FT.RebateCost;
                    arrearFeeCostTemp -= InvoiceManualInfo.FT.TotCost;

                    alArrearManualFeeInfo.Add(InvoiceManualInfo);
                }

                //如果没有分完整，平小数位
                if (Math.Abs(arrearFeeCostTemp) > 0)
                {
                    for (int i = 0; i < feeInfoList.Count; i++)
                    {
                        if (feeInfoList[i].FT.OwnCost + arrearFeeCostTemp > 0)
                        {
                            FS.HISFC.Models.Fee.Inpatient.FeeInfo InvoiceManualInfo = feeInfoList[i].Clone();
                            InvoiceManualInfo.FT.OwnCost = arrearFeeCostTemp;
                            InvoiceManualInfo.FT.PayCost = 0.00M;
                            InvoiceManualInfo.FT.PubCost = 0.00M;
                            InvoiceManualInfo.FT.RebateCost = 0.00M;
                            InvoiceManualInfo.FT.TotCost = arrearFeeCostTemp;
                            feeInfoList[i].FT.OwnCost -= arrearFeeCostTemp;
                            feeInfoList[i].FT.TotCost -= arrearFeeCostTemp;
                            alArrearManualFeeInfo.Add(InvoiceManualInfo);
                            break;
                        }
                    }
                }

                if (this.FeeInfoTransFeeStat(patientInfo, balanceType, alArrearManualFeeInfo, alArrearBalanceList, sysdate, balanceNO, "", alFeeState) == -1)
                {
                    this.RollBack();
                    return -1;
                }

            }

            #endregion

            #region 优惠发票

            List<FS.HISFC.Models.Fee.Inpatient.BalanceList> alBalanceListRebate = new List<FS.HISFC.Models.Fee.Inpatient.BalanceList>();
            bool IsRebatePrint = controlParamMgr.GetControlParam<bool>("100007");
            //判断优惠是否单独打印发票
            if (IsRebatePrint)
            {
                ArrayList alInvoiceRebateFeeInfo = new ArrayList();
                for (int i = 0; i < feeInfoList.Count; i++)
                {
                    FS.HISFC.Models.Fee.Inpatient.FeeInfo f = feeInfoList[i];
                    if (f.FT.RebateCost > 0)
                    {
                        FS.HISFC.Models.Fee.Inpatient.FeeInfo FeeInfoRebate = new FS.HISFC.Models.Fee.Inpatient.FeeInfo();
                        FeeInfoRebate.Item.MinFee.ID = f.Item.MinFee.ID;
                        FeeInfoRebate.FT.RebateCost = f.FT.RebateCost;
                        FeeInfoRebate.FT.TotCost = f.FT.TotCost;
                        alInvoiceRebateFeeInfo.Add(FeeInfoRebate);
                    }
                }

                if (this.FeeInfoTransFeeStat(patientInfo, balanceType, alInvoiceRebateFeeInfo, alBalanceListRebate, sysdate, balanceNO, invoiceNO, alFeeState) == -1)
                {
                    this.RollBack();
                    return -1;
                }
            }

            #endregion

            #region 高收费发票

            List<FS.HISFC.Models.Fee.Inpatient.BalanceList> listSplitFeeBalanceList = new List<FS.HISFC.Models.Fee.Inpatient.BalanceList>();
            ArrayList alSplitFeeBalanceList = new ArrayList();
            if (splitHighInvoice)
            {
                for (int i = 0; i < feeInfoList.Count; )
                {
                    FS.HISFC.Models.Fee.Inpatient.FeeInfo f = feeInfoList[i];
                    if (f.SplitFeeFlag)
                    {
                        feeInfoList.RemoveAt(i);
                        alSplitFeeBalanceList.Add(f);
                        continue;
                    }
                    i++;
                }

                //处理高收费发票明细大类
                if (alSplitFeeBalanceList.Count > 0)
                {
                    if (this.FeeInfoTransFeeStat(patientInfo, balanceType, alSplitFeeBalanceList, listSplitFeeBalanceList, sysdate, balanceNO, "", alFeeState) == -1)
                    {
                        this.RollBack();
                        return -1;
                    }
                }
            }


            #endregion

            #region 婴儿发票
            //List<FS.HISFC.Models.Fee.Inpatient.BalanceList> alBalanceListBaby = new List<FS.HISFC.Models.Fee.Inpatient.BalanceList>();
            //if (patientInfo.IsBaby)
            //{
            //    bool IsBabyPrint = FS.FrameWork.Function.NConvert.ToBoolean(controlParm.QueryControlerInfo("100004"));
            //    //判断婴儿费用是否单独打印发票 
            //    if (IsBabyPrint)
            //    {
            //        ArrayList alInvoiceBabyFeeInfo = new ArrayList();
            //        ArrayList al = inpatientFeeManager.QueryFeeInfosGroupByMinFeeForBaby(patientInfo.ID, beginTime, endTime, "0");
            //        if (al == null)
            //        {
            //            this.RollBack();
            //            this.err = "结算失败，原因：" + inpatientFeeManager.Err;
            //            return -1;
            //        }

            //        for (int i = 0; i < feeInfoList.Count; i++)
            //        {
            //            FS.HISFC.Models.Fee.Inpatient.FeeInfo f = feeInfoList[i];
            //            for (int j = 0; j < al.Count; j++)
            //            {
            //                FS.HISFC.Models.Fee.Inpatient.FeeInfo feeinfoBaby = (FS.HISFC.Models.Fee.Inpatient.FeeInfo)al[j];
            //                if (f.Item.MinFee.ID == feeinfoBaby.Item.MinFee.ID)
            //                {
            //                    if (f.FT.TotCost - feeinfoBaby.FT.TotCost < 0)
            //                    {
            //                        this.RollBack();
            //                        this.err = "结算失败，原因：输入的结算金额小于婴儿实际发生费用";
            //                        return -1;

            //                    }
            //                    f.FT.TotCost = f.FT.TotCost - feeinfoBaby.FT.TotCost;
            //                    f.FT.OwnCost = f.FT.OwnCost - feeinfoBaby.FT.OwnCost;
            //                    f.FT.PayCost = f.FT.PayCost - feeinfoBaby.FT.PayCost;
            //                    f.FT.PubCost = f.FT.PubCost - feeinfoBaby.FT.PubCost;
            //                    if (!IsRebatePrint)
            //                    {
            //                        f.FT.RebateCost = f.FT.RebateCost - feeinfoBaby.FT.RebateCost;
            //                    }
            //                    alInvoiceBabyFeeInfo.Add(feeinfoBaby);
            //                }
            //            }
            //        }

            //        if (this.FeeInfoTransFeeStat(patientInfo, balanceType, alInvoiceBabyFeeInfo, alBalanceListBaby, sysdate, balanceNO, "", alFeeState) == -1)
            //        {
            //            this.RollBack();
            //            return -1;
            //        }
            //    }

            //}

            #endregion

            #region 手工发票

            List<FS.HISFC.Models.Fee.Inpatient.BalanceList> alBalanceListMannal = new List<FS.HISFC.Models.Fee.Inpatient.BalanceList>();
            //if (splitInvoiceCost > 0)
            //{
            //    ArrayList alInvoiceManualFeeInfo = new ArrayList();
            //    decimal splitInvoiceCostTemp = splitInvoiceCost;

            //    decimal splitRate = splitInvoiceCost / TotBalanceCost;
            //    if (balanceType == FS.HISFC.Models.Base.EBlanceType.OweMid)
            //    {
            //        splitRate = splitInvoiceCost / (TotBalanceCost - arrearFeeCost);
            //    }

            //    if (splitRate > 1)
            //    {
            //        this.RollBack();
            //        this.err = "发票金额大于结算金额";
            //        return -1;
            //    }

            //    //循环处理分票明细及主发票明细
            //    for (int i = 0; i < feeInfoList.Count; i++)
            //    {
            //        FS.HISFC.Models.Fee.Inpatient.FeeInfo InvoiceHeadInfo = feeInfoList[i];
            //        FS.HISFC.Models.Fee.Inpatient.FeeInfo InvoiceManualInfo = InvoiceHeadInfo.Clone();

            //        InvoiceManualInfo.FT.OwnCost = FS.FrameWork.Function.NConvert.ToDecimal(FS.FrameWork.Public.String.FormatNumberReturnString(InvoiceHeadInfo.FT.OwnCost * splitRate, 2));
            //        InvoiceManualInfo.FT.PayCost = FS.FrameWork.Function.NConvert.ToDecimal(FS.FrameWork.Public.String.FormatNumberReturnString(InvoiceHeadInfo.FT.PayCost * splitRate, 2));
            //        InvoiceManualInfo.FT.PubCost = FS.FrameWork.Function.NConvert.ToDecimal(FS.FrameWork.Public.String.FormatNumberReturnString(InvoiceHeadInfo.FT.PubCost * splitRate, 2));
            //        InvoiceManualInfo.FT.RebateCost = FS.FrameWork.Function.NConvert.ToDecimal(FS.FrameWork.Public.String.FormatNumberReturnString(InvoiceHeadInfo.FT.RebateCost * splitRate, 2));

            //        InvoiceManualInfo.FT.TotCost = InvoiceManualInfo.FT.OwnCost + InvoiceManualInfo.FT.PayCost + InvoiceManualInfo.FT.PubCost + InvoiceManualInfo.FT.RebateCost;

            //        InvoiceHeadInfo.FT.TotCost = InvoiceHeadInfo.FT.TotCost - InvoiceManualInfo.FT.TotCost;
            //        InvoiceHeadInfo.FT.OwnCost = InvoiceHeadInfo.FT.OwnCost - InvoiceManualInfo.FT.OwnCost;
            //        InvoiceHeadInfo.FT.PayCost = InvoiceHeadInfo.FT.PayCost - InvoiceManualInfo.FT.PayCost;
            //        InvoiceHeadInfo.FT.PubCost = InvoiceHeadInfo.FT.PubCost - InvoiceManualInfo.FT.PubCost;
            //        InvoiceHeadInfo.FT.RebateCost = InvoiceHeadInfo.FT.RebateCost - InvoiceManualInfo.FT.RebateCost;
            //        splitInvoiceCostTemp -= InvoiceManualInfo.FT.TotCost;

            //        alInvoiceManualFeeInfo.Add(InvoiceManualInfo);
            //    }

            //    //如果没有分完整，平小数位
            //    if (Math.Abs(splitInvoiceCostTemp) > 0)
            //    {
            //        for (int i = 0; i < feeInfoList.Count; i++)
            //        {
            //            if (feeInfoList[i].FT.OwnCost + splitInvoiceCostTemp > 0)
            //            {
            //                FS.HISFC.Models.Fee.Inpatient.FeeInfo InvoiceManualInfo = feeInfoList[i].Clone();
            //                InvoiceManualInfo.FT.OwnCost = splitInvoiceCostTemp;
            //                InvoiceManualInfo.FT.PayCost = 0.00M;
            //                InvoiceManualInfo.FT.PubCost = 0.00M;
            //                InvoiceManualInfo.FT.RebateCost = 0.00M;
            //                InvoiceManualInfo.FT.TotCost = splitInvoiceCostTemp;
            //                feeInfoList[i].FT.OwnCost -= splitInvoiceCostTemp;
            //                feeInfoList[i].FT.TotCost -= splitInvoiceCostTemp;
            //                alInvoiceManualFeeInfo.Add(InvoiceManualInfo);
            //                break;
            //            }
            //        }
            //    }

            //    if (this.FeeInfoTransFeeStat(patientInfo, balanceType, alInvoiceManualFeeInfo, alBalanceListMannal, sysdate, balanceNO, "", alFeeState) == -1)
            //    {
            //        this.RollBack();
            //        return -1;
            //    }
            //}

            #endregion

            #region 主发票

            List<FS.HISFC.Models.Fee.Inpatient.BalanceList> balanceList = new List<FS.HISFC.Models.Fee.Inpatient.BalanceList>();
            if (feeInfoList.Count > 0)
            {
                if (this.FeeInfoTransFeeStat(patientInfo, balanceType, new ArrayList(feeInfoList), balanceList, sysdate, balanceNO, invoiceNO, alFeeState) == -1)
                {
                    this.RollBack();
                    return -1;
                }

            }

            #endregion

            #endregion

            #region 处理发票明细

            bool mainInvoice = true;

            #region 主发票

            if (balanceList.Count > 0)
            {
                decimal TotCost = 0m;
                decimal OwnCost = 0m;
                decimal PayCost = 0m;
                decimal PubCost = 0m;
                decimal RebateCost = 0M;
                foreach (FS.HISFC.Models.Fee.Inpatient.BalanceList balance in balanceList)
                {
                    TotCost += balance.BalanceBase.FT.TotCost;
                    OwnCost += balance.BalanceBase.FT.OwnCost;
                    PayCost += balance.BalanceBase.FT.PayCost;
                    PubCost += balance.BalanceBase.FT.PubCost;
                    RebateCost += balance.BalanceBase.FT.RebateCost;
                    if (balanceType == FS.HISFC.Models.Base.EBlanceType.OweMid)
                    {
                        balance.BalanceBase.BalanceType.ID = FS.HISFC.Models.Fee.EnumBalanceType.O;
                    }
                    balance.BalanceBase.Invoice.ID = invoiceNO;
                    if (this.inpatientFeeManager.InsertBalanceList(patientInfo, balance) < 1)
                    {
                        this.RollBack();
                        this.err = this.inpatientFeeManager.Err;
                        return -1;
                    }

                }

                FS.HISFC.Models.Fee.Inpatient.Balance balanceHead = ((FS.HISFC.Models.Fee.Inpatient.Balance)balanceList[0].BalanceBase).Clone();

                balanceHead.Invoice.ID = invoiceNO;
                balanceHead.BeginTime = inDate;//先取上次结算时间，如果没有就取本次入院时间
                balanceHead.EndTime = sysdate;//结算时间
                balanceHead.FT.TotCost = TotCost;
                balanceHead.FT.OwnCost = OwnCost;
                balanceHead.FT.PayCost = PayCost;
                balanceHead.FT.PubCost = PubCost;

                //如果减免金额大于自费金额，公费不能，说明分了发票
                if (TotHighOwnCost > 0)
                {
                    if (TotDerateCost - TotHighOwnCost > OwnCost)
                    {
                        balanceHead.FT.DerateCost = OwnCost;
                    }
                    else
                    {
                        balanceHead.FT.DerateCost = TotDerateCost > TotHighOwnCost ? TotDerateCost - TotHighOwnCost : 0;
                    }
                }
                else if (TotDerateCost > 0)
                {
                    if (TotDerateCost > OwnCost)
                    {
                        balanceHead.FT.DerateCost = OwnCost;
                    }
                    else
                    {
                        balanceHead.FT.DerateCost = TotDerateCost;
                    }
                }
                TotDerateCost = TotDerateCost - balanceHead.FT.DerateCost;

                balanceHead.CancelType = FS.HISFC.Models.Base.CancelTypes.Valid;
                balanceHead.BalanceSaveType = "0";
                balanceHead.IsMainInvoice = true;

                //优惠金额
                if (!IsRebatePrint)
                {
                    balanceHead.FT.RebateCost = RebateCost;
                }
                balanceHead.IsMainInvoice = mainInvoice;
                if (mainInvoice)
                {
                    mainInvoice = false;
                    balanceHead.FT.PrepayCost = TotPrepayCost;//预交金额
                    balanceHead.FT.TransferPrepayCost = 0.00M;//欠费金额

                    if (TotShouldPay == TotPrepayCost)
                    {
                        balanceHead.FT.SupplyCost = 0M;//应收金额
                        balanceHead.FT.ReturnCost = 0M;//应返金额
                    }
                    else if (TotShouldPay > TotPrepayCost)
                    {
                        balanceHead.FT.SupplyCost = TotShouldPay - TotPrepayCost;//应收金额
                        balanceHead.FT.ReturnCost = 0M;//应返金额
                    }
                    else
                    {
                        balanceHead.FT.SupplyCost = 0M;//应收金额
                        balanceHead.FT.ReturnCost = TotPrepayCost - TotShouldPay;//应返金额
                    }

                    //balanceHead.FT.SupplyCost = balanceHead.FT.SupplyCost - arrearFeeCost;

                    if (balanceHead.BalanceType.ID.ToString() == FS.HISFC.Models.Fee.EnumBalanceType.Q.ToString())
                    {
                        balanceHead.FT.ArrearCost = arrearFeeCost;
                    }
                }
                else
                {
                    if (balanceHead.BalanceType.ID.ToString() == FS.HISFC.Models.Fee.EnumBalanceType.Q.ToString())
                    {
                        balanceHead.FT.ArrearCost = arrearFeeCost;
                    }
                }

                if (patientInfo.Pact.PayKind.ID == "02")
                {
                    //balanceHead.FT.OwnCost = patientInfo.SIMainInfo.OwnCost - balanceHead.FT.DerateCost - balanceHead.FT.RebateCost;
                    balanceHead.FT.PayCost = patientInfo.SIMainInfo.PayCost;
                    balanceHead.FT.PubCost = patientInfo.SIMainInfo.PubCost;
                    balanceHead.FT.OwnCost = balanceHead.FT.TotCost - balanceHead.FT.PayCost - balanceHead.FT.PubCost;
                }

                //插入结算头表
                balanceHead.PrintedInvoiceNO = realInvoiceNO;
                if (this.inpatientFeeManager.InsertBalance(patientInfo, balanceHead) < 1)
                {
                    this.RollBack();
                    this.err = this.inpatientFeeManager.Err;
                    return -1;
                }

                if (feeIntegrate.InsertInvoiceExtend(invoiceNO, "I", realInvoiceNO, "00") < 1)
                {
                    this.RollBack();
                    this.err = this.inpatientFeeManager.Err;
                    return -1;
                }

                if (feeIntegrate.UseInvoiceNO("I", 1, ref invoiceNO, ref realInvoiceNO, ref this.err) != 1)
                {
                    this.RollBack();
                    return -1;
                }

                listBalance.Add(balanceHead);
            }

            #endregion

            #region 手工发票

            if (alBalanceListMannal.Count > 0)
            {
                if (feeIntegrate.GetInvoiceNO("I", ref invoiceNO, ref realInvoiceNO, ref errText) < 1 || string.IsNullOrEmpty(invoiceNO) || string.IsNullOrEmpty(realInvoiceNO))
                {
                    this.RollBack();
                    this.err = "获取发票失败，" + feeIntegrate.Err;
                    return -1;
                }

                decimal TotCost = 0m;
                decimal OwnCost = 0m;
                decimal PayCost = 0m;
                decimal PubCost = 0m;
                decimal RebateCost = 0M;
                foreach (FS.HISFC.Models.Fee.Inpatient.BalanceList balance in alBalanceListMannal)
                {
                    TotCost += balance.BalanceBase.FT.TotCost;
                    OwnCost += balance.BalanceBase.FT.OwnCost;
                    PayCost += balance.BalanceBase.FT.PayCost;
                    PubCost += balance.BalanceBase.FT.PubCost;
                    RebateCost += balance.BalanceBase.FT.RebateCost;

                    if (balanceType == FS.HISFC.Models.Base.EBlanceType.OweMid)
                    {
                        balance.BalanceBase.BalanceType.ID = FS.HISFC.Models.Fee.EnumBalanceType.O;
                    }

                    balance.BalanceBase.Invoice.ID = invoiceNO;
                    if (this.inpatientFeeManager.InsertBalanceList(patientInfo, balance) < 1)
                    {
                        this.RollBack();
                        this.err = this.inpatientFeeManager.Err;
                        return -1;
                    }

                }

                FS.HISFC.Models.Fee.Inpatient.Balance balanceHead = ((FS.HISFC.Models.Fee.Inpatient.Balance)alBalanceListMannal[0].BalanceBase).Clone();

                balanceHead.Invoice.ID = invoiceNO;
                balanceHead.BeginTime = inDate;//患者入院时间
                balanceHead.EndTime = sysdate;//结算时间
                balanceHead.FT.TotCost = TotCost;
                balanceHead.FT.OwnCost = OwnCost;
                balanceHead.FT.PayCost = PayCost;
                balanceHead.FT.PubCost = PubCost;


                balanceHead.CancelType = FS.HISFC.Models.Base.CancelTypes.Valid;
                balanceHead.IsMainInvoice = false;
                balanceHead.BalanceSaveType = "0";

                //优惠金额
                if (!IsRebatePrint)
                {
                    balanceHead.FT.RebateCost = RebateCost;
                }
                //医保处理 2012年7月24日18:10:28
                if (patientInfo.Pact.PayKind.ID == "02")
                {
                    //balanceHead.FT.OwnCost = patientInfo.SIMainInfo.OwnCost - balanceHead.FT.DerateCost - balanceHead.FT.RebateCost;
                    balanceHead.FT.PayCost = patientInfo.SIMainInfo.PayCost;
                    balanceHead.FT.PubCost = patientInfo.SIMainInfo.PubCost;
                    balanceHead.FT.OwnCost = balanceHead.FT.TotCost - balanceHead.FT.PayCost - balanceHead.FT.PubCost;
                }

                //如果减免金额大于自费金额，公费不能，说明分了发票
                if (TotHighOwnCost > 0)
                {
                    if (TotDerateCost - TotHighOwnCost > OwnCost)
                    {
                        balanceHead.FT.DerateCost = OwnCost;
                    }
                    else
                    {
                        balanceHead.FT.DerateCost = TotDerateCost > TotHighOwnCost ? TotDerateCost - TotHighOwnCost : 0;
                    }
                }
                else if (TotDerateCost > 0)
                {
                    if (TotDerateCost > OwnCost)
                    {
                        balanceHead.FT.DerateCost = OwnCost;
                    }
                    else
                    {
                        balanceHead.FT.DerateCost = TotDerateCost;
                    }
                }
                TotDerateCost = TotDerateCost - balanceHead.FT.DerateCost;


                balanceHead.IsMainInvoice = mainInvoice;
                if (mainInvoice)
                {
                    mainInvoice = false;
                    balanceHead.FT.PrepayCost = TotPrepayCost;//预交金额
                    balanceHead.FT.TransferPrepayCost = 0.00M;//欠费金额
                    if (TotShouldPay == TotPrepayCost)
                    {
                        balanceHead.FT.SupplyCost = 0M;//应收金额
                        balanceHead.FT.ReturnCost = 0M;//应返金额
                    }
                    else if (TotShouldPay > TotPrepayCost)
                    {
                        balanceHead.FT.SupplyCost = TotShouldPay - TotPrepayCost;//应收金额
                        balanceHead.FT.ReturnCost = 0M;//应返金额
                    }
                    else
                    {
                        balanceHead.FT.SupplyCost = 0M;//应收金额
                        balanceHead.FT.ReturnCost = TotPrepayCost - TotShouldPay;//应返金额
                    }

                    //balanceHead.FT.SupplyCost = balanceHead.FT.SupplyCost - arrearFeeCost;

                    if (balanceHead.BalanceType.ID.ToString() == FS.HISFC.Models.Fee.EnumBalanceType.Q.ToString())
                    {
                        balanceHead.FT.ArrearCost = arrearFeeCost;
                    }
                }
                else
                {
                    if (balanceHead.BalanceType.ID.ToString() == FS.HISFC.Models.Fee.EnumBalanceType.Q.ToString())
                    {
                        balanceHead.FT.ArrearCost = arrearFeeCost;
                    }
                }
                ////医保处理 2012年7月24日18:10:28
                //if (patientInfo.Pact.PayKind.ID == "02")
                //{
                //    balanceHead.FT.OwnCost = patientInfo.SIMainInfo.OwnCost - balanceHead.FT.DerateCost - balanceHead.FT.RebateCost;
                //    balanceHead.FT.PayCost = patientInfo.SIMainInfo.PayCost;
                //    balanceHead.FT.PubCost = patientInfo.SIMainInfo.PubCost;
                //}
                //插入结算头表
                balanceHead.PrintedInvoiceNO = realInvoiceNO;
                if (this.inpatientFeeManager.InsertBalance(patientInfo, balanceHead) < 1)
                {
                    this.RollBack();
                    this.err = this.inpatientFeeManager.Err;
                    return -1;
                }

                if (feeIntegrate.InsertInvoiceExtend(invoiceNO, "I", realInvoiceNO, "00") < 1)
                {
                    this.RollBack();
                    this.err = this.inpatientFeeManager.Err;
                    return -1;
                }

                if (feeIntegrate.UseInvoiceNO("I", 1, ref invoiceNO, ref realInvoiceNO, ref this.err) != 1)
                {
                    this.RollBack();
                    return -1;
                }

                listBalance.Add(balanceHead);
            }
            #endregion

            #region 高收费发票

            if (listSplitFeeBalanceList.Count > 0)
            {
                if (feeIntegrate.GetInvoiceNO("I", ref invoiceNO, ref realInvoiceNO, ref errText) < 1 || string.IsNullOrEmpty(invoiceNO) || string.IsNullOrEmpty(realInvoiceNO))
                {
                    this.RollBack();
                    this.err = "获取发票失败，" + feeIntegrate.Err;
                    return -1;
                }

                decimal TotCost = 0m;
                decimal OwnCost = 0m;
                decimal PayCost = 0m;
                decimal PubCost = 0m;
                decimal RebateCost = 0M;
                foreach (FS.HISFC.Models.Fee.Inpatient.BalanceList balance in listSplitFeeBalanceList)
                {
                    TotCost += balance.BalanceBase.FT.TotCost;
                    OwnCost += balance.BalanceBase.FT.TotCost;
                    PayCost += 0;
                    PubCost += 0;
                    RebateCost += balance.BalanceBase.FT.RebateCost;

                    if (balanceType == FS.HISFC.Models.Base.EBlanceType.OweMid)
                    {
                        balance.BalanceBase.BalanceType.ID = FS.HISFC.Models.Fee.EnumBalanceType.O;
                    }

                    balance.BalanceBase.Invoice.ID = invoiceNO;
                    balance.BalanceBase.SplitFeeFlag = false;//高收费标记
                    if (this.inpatientFeeManager.InsertBalanceList(patientInfo, balance) < 1)
                    {
                        this.RollBack();
                        this.err = this.inpatientFeeManager.Err;
                        return -1;
                    }

                }

                FS.HISFC.Models.Fee.Inpatient.Balance balanceHead = ((FS.HISFC.Models.Fee.Inpatient.Balance)listSplitFeeBalanceList[0].BalanceBase).Clone();
                balanceHead.Invoice.ID = invoiceNO;
                balanceHead.BeginTime = inDate;//患者入院时间
                balanceHead.EndTime = sysdate;//结算时间
                balanceHead.FT.TotCost = TotCost;
                balanceHead.FT.OwnCost = OwnCost;
                balanceHead.FT.PayCost = PayCost;
                balanceHead.FT.PubCost = PubCost;

                //如果减免金额大于自费金额，公费不能，说明分了发票
                if (TotDerateCost > OwnCost)
                {
                    balanceHead.FT.DerateCost = OwnCost;
                }
                else
                {
                    balanceHead.FT.DerateCost = TotDerateCost;
                }
                TotDerateCost = TotDerateCost - balanceHead.FT.DerateCost;

                balanceHead.CancelType = FS.HISFC.Models.Base.CancelTypes.Valid;
                balanceHead.IsMainInvoice = false;
                balanceHead.BalanceSaveType = "0";

                //优惠金额
                if (!IsRebatePrint)
                {
                    balanceHead.FT.RebateCost = RebateCost;
                }

                result = inpatientFeeManager.UpdateFeeInfoBalanced(patientInfo.ID, balanceNO, sysdate, invoiceNO, true);
                if (result == 0)
                {
                    this.RollBack();
                    this.err = "可能存在并发操作导致更新费用失败！";
                    return -1;
                }

                if (result < 1)
                {
                    this.RollBack();
                    this.err = inpatientFeeManager.Err;
                    return -1;
                }

                if (inpatientFeeManager.UpdateFeeItemListBalanced(patientInfo.ID, balanceNO, invoiceNO, true) == -1)
                {
                    this.RollBack();
                    this.err = inpatientFeeManager.Err;
                    return -1;
                }


                //插入结算头表
                balanceHead.PrintedInvoiceNO = realInvoiceNO;
                FS.HISFC.Models.Base.PactInfo pact = patientInfo.Pact.Clone();
                patientInfo.Pact.ID = "1";
                patientInfo.Pact.Name = "自费";
                patientInfo.Pact.PayKind.ID = "01";
                patientInfo.Pact.PayKind.Name = "自费";
                if (this.inpatientFeeManager.InsertBalance(patientInfo, balanceHead) < 1)
                {
                    this.RollBack();
                    this.err = this.inpatientFeeManager.Err;
                    patientInfo.Pact = pact;
                    return -1;
                }
                patientInfo.Pact = pact;

                if (feeIntegrate.InsertInvoiceExtend(invoiceNO, "I", realInvoiceNO, "00") < 1)
                {
                    this.RollBack();
                    this.err = this.inpatientFeeManager.Err;
                    return -1;
                }

                if (feeIntegrate.UseInvoiceNO("I", 1, ref invoiceNO, ref realInvoiceNO, ref this.err) != 1)
                {
                    this.RollBack();
                    return -1;
                }

                listBalance.Add(balanceHead);
            }
            else
            {
                result = inpatientFeeManager.UpdateFeeInfoBalanced(patientInfo.ID, balanceNO, sysdate, invoiceNO, true);

                if (result < 0)
                {
                    this.RollBack();
                    this.err = inpatientFeeManager.Err;
                    return -1;
                }

                if (inpatientFeeManager.UpdateFeeItemListBalanced(patientInfo.ID, balanceNO, invoiceNO, true) == -1)
                {
                    this.RollBack();
                    this.err = inpatientFeeManager.Err;
                    return -1;
                }
            }

            #endregion

            #region 优惠发票

            if (alBalanceListRebate.Count > 0)
            {
                if (feeIntegrate.GetInvoiceNO("I", ref invoiceNO, ref realInvoiceNO, ref errText) < 1 || string.IsNullOrEmpty(invoiceNO) || string.IsNullOrEmpty(realInvoiceNO))
                {
                    this.RollBack();
                    this.err = "获取发票失败，" + feeIntegrate.Err;
                    return -1;
                }
                decimal TotCost = 0m;
                decimal OwnCost = 0m;
                decimal PayCost = 0m;
                decimal PubCost = 0m;
                decimal RebateCost = 0M;
                foreach (FS.HISFC.Models.Fee.Inpatient.BalanceList balance in alBalanceListRebate)
                {
                    TotCost += balance.BalanceBase.FT.TotCost;
                    OwnCost += balance.BalanceBase.FT.OwnCost;
                    PayCost += balance.BalanceBase.FT.PayCost;
                    PubCost += balance.BalanceBase.FT.PubCost;
                    RebateCost += balance.BalanceBase.FT.RebateCost;
                    balance.BalanceBase.Invoice.ID = invoiceNO;
                    if (this.inpatientFeeManager.InsertBalanceList(patientInfo, balance) < 1)
                    {
                        this.RollBack();
                        this.err = this.inpatientFeeManager.Err;
                        return -1;
                    }
                }
                //头表实体赋值
                FS.HISFC.Models.Fee.Inpatient.Balance BalanceRebate = (FS.HISFC.Models.Fee.Inpatient.Balance)alBalanceListRebate[0].BalanceBase.Clone();
                BalanceRebate.FT.TotCost = TotCost;
                BalanceRebate.FT.OwnCost = OwnCost;
                BalanceRebate.FT.PayCost = PayCost;
                BalanceRebate.FT.PubCost = PubCost;
                BalanceRebate.FT.RebateCost = RebateCost;
                BalanceRebate.BeginTime = inDate;
                BalanceRebate.EndTime = sysdate;
                BalanceRebate.CancelType = FS.HISFC.Models.Base.CancelTypes.Valid;
                BalanceRebate.IsMainInvoice = false;
                BalanceRebate.BalanceSaveType = "0";
                //插入结算头表
                BalanceRebate.PrintedInvoiceNO = realInvoiceNO;
                if (this.inpatientFeeManager.InsertBalance(patientInfo, BalanceRebate) < 1)
                {
                    this.RollBack();
                    this.err = this.inpatientFeeManager.Err;
                    return -1;
                }

                if (feeIntegrate.InsertInvoiceExtend(invoiceNO, "I", realInvoiceNO, "00") < 1)
                {
                    this.RollBack();
                    this.err = feeIntegrate.Err;
                    return -1;
                }

                if (feeIntegrate.UseInvoiceNO("I", 1, ref invoiceNO, ref realInvoiceNO, ref this.err) != 1)
                {
                    this.RollBack();
                    return -1;
                }
                listBalance.Add(BalanceRebate);
            }

            #endregion

            #region 欠费发票

            if (alArrearBalanceList.Count > 0)
            {
                if (feeIntegrate.GetInvoiceNO(this.inpatientFeeManager.Operator as FS.HISFC.Models.Base.Employee, "I", true, ref invoiceNO, ref realInvoiceNO, ref this.err) != 1)
                {
                    this.RollBack();
                    return -1;
                }

                decimal TotCost = 0m;
                decimal OwnCost = 0m;
                decimal PayCost = 0m;
                decimal PubCost = 0m;
                decimal RebateCost = 0M;
                foreach (FS.HISFC.Models.Fee.Inpatient.BalanceList balance in alArrearBalanceList)
                {
                    TotCost += balance.BalanceBase.FT.TotCost;
                    OwnCost += balance.BalanceBase.FT.OwnCost;
                    PayCost += balance.BalanceBase.FT.PayCost;
                    PubCost += balance.BalanceBase.FT.PubCost;
                    RebateCost += balance.BalanceBase.FT.RebateCost;
                    ((FS.HISFC.Models.Fee.Inpatient.Balance)balance.BalanceBase).IsTempInvoice = true;
                    balance.BalanceBase.Invoice.ID = invoiceNO;
                    if (this.inpatientFeeManager.InsertBalanceList(patientInfo, balance) < 1)
                    {
                        this.RollBack();
                        this.err = this.inpatientFeeManager.Err;
                        return -1;
                    }

                }

                FS.HISFC.Models.Fee.Inpatient.Balance balanceHead = ((FS.HISFC.Models.Fee.Inpatient.Balance)alArrearBalanceList[0].BalanceBase).Clone();

                balanceHead.Invoice.ID = invoiceNO;
                balanceHead.BeginTime = inDate;//患者入院时间
                balanceHead.EndTime = sysdate;//结算时间
                balanceHead.FT.TotCost = TotCost;
                balanceHead.FT.OwnCost = OwnCost;
                balanceHead.FT.PayCost = PayCost;
                balanceHead.FT.PubCost = PubCost;
                balanceHead.IsTempInvoice = true;
                balanceHead.CancelType = FS.HISFC.Models.Base.CancelTypes.Valid;
                balanceHead.BalanceSaveType = "0";


                //如果减免金额大于自费金额，公费不能，说明分了发票
                if (TotDerateCost > OwnCost)
                {
                    balanceHead.FT.DerateCost = OwnCost;
                    TotDerateCost = TotDerateCost - OwnCost;
                }
                else
                {
                    balanceHead.FT.DerateCost = TotDerateCost;
                }

                //优惠金额
                balanceHead.FT.RebateCost = RebateCost;
                balanceHead.IsMainInvoice = mainInvoice;
                if (mainInvoice)
                {
                    mainInvoice = false;
                    balanceHead.FT.PrepayCost = TotPrepayCost;//预交金额
                    balanceHead.FT.TransferPrepayCost = 0.00M;//欠费金额

                    if (TotShouldPay == TotPrepayCost)
                    {
                        balanceHead.FT.SupplyCost = 0M;//应收金额
                        balanceHead.FT.ReturnCost = 0M;//应返金额
                    }
                    else if (TotShouldPay > TotPrepayCost)
                    {
                        balanceHead.FT.SupplyCost = TotShouldPay - TotPrepayCost;//应收金额
                        balanceHead.FT.ReturnCost = 0M;//应返金额
                    }
                    else
                    {
                        balanceHead.FT.SupplyCost = 0M;//应收金额
                        balanceHead.FT.ReturnCost = TotPrepayCost - TotShouldPay;//应返金额
                    }


                    balanceHead.FT.SupplyCost = balanceHead.FT.SupplyCost - arrearFeeCost;

                    if (balanceHead.BalanceType.ID.ToString() == FS.HISFC.Models.Fee.EnumBalanceType.Q.ToString())
                    {
                        balanceHead.FT.ArrearCost = arrearFeeCost;
                    }
                }
                else
                {
                    if (balanceHead.BalanceType.ID.ToString() == FS.HISFC.Models.Fee.EnumBalanceType.Q.ToString())
                    {
                        balanceHead.FT.ArrearCost = arrearFeeCost;
                    }
                }

                //插入结算头表
                balanceHead.PrintedInvoiceNO = realInvoiceNO;
                if (this.inpatientFeeManager.InsertBalance(patientInfo, balanceHead) < 1)
                {
                    this.RollBack();
                    this.err = this.inpatientFeeManager.Err;
                    return -1;
                }
            }

            #endregion

            #endregion

            #region 患者费用处理

            patientInfo.SIMainInfo.BalNo = balNo;
            patientInfo.SIMainInfo.InvoiceNo = invoiceNO;
            patientInfo.PVisit.InState.ID = FS.HISFC.Models.Base.EnumInState.O;//出院结算

            FS.HISFC.Models.Fee.Inpatient.FeeInfo feeInfo = new FS.HISFC.Models.Fee.Inpatient.FeeInfo();
            feeInfo.FT.PubCost = TotPubCost;
            feeInfo.FT.OwnCost = TotOwnCost;
            feeInfo.FT.PayCost = TotPayCost;
            feeInfo.FT.RebateCost = TotRebateCost;
            feeInfo.FT.TotCost = TotBalanceCost;
            feeInfo.FT.PrepayCost = TotPrepayCost;

            //医保处理一下 2012年7月24日16:57:22
            if (patientInfo.Pact.PayKind.ID == "02" && patientInfo.SIMainInfo.TotCost != 0)
            {
                feeInfo.FT.PubCost = patientInfo.SIMainInfo.PubCost;
                feeInfo.FT.OwnCost = patientInfo.SIMainInfo.OwnCost;
                feeInfo.FT.PayCost = patientInfo.SIMainInfo.PayCost;
                //feeInfo.FT.RebateCost = TotRebateCost;
                //feeInfo.FT.TotCost = TotBalanceCost;
                //feeInfo.FT.PrepayCost = TotPrepayCost;
            }

            //+ feeInfo.FT.RebateCost
            if (feeInfo.FT.TotCost != feeInfo.FT.OwnCost + feeInfo.FT.PayCost + feeInfo.FT.PubCost)
            {
                patientInfo.PVisit.InState.ID = FS.HISFC.Models.Base.EnumInState.B;
                this.RollBack();
                this.err = "总费用与分支费用不相等，请检查！";
                return -1;
            }

            if (TotBalancePayCost != feeInfo.FT.TotCost)
            {
                patientInfo.PVisit.InState.ID = FS.HISFC.Models.Base.EnumInState.B;
                this.RollBack();
                this.err = "消费金额与支付费用不等：消费金额为" + feeInfo.FT.TotCost.ToString() + " 支付金额为" + TotBalancePayCost.ToString();
                return -1;
            }

            if (this.inpatientFeeManager.UpdateInMainInfoBalanced(patientInfo, sysdate, balanceNO, feeInfo.FT) <= 0)
            {
                patientInfo.PVisit.InState.ID = FS.HISFC.Models.Base.EnumInState.B;
                this.RollBack();
                this.err = this.inpatientFeeManager.Err;
                return -1;
            }

            //开账
            if (this.inpatientFeeManager.OpenAccount(patientInfo.ID) < 0)
            {
                patientInfo.PVisit.InState.ID = FS.HISFC.Models.Base.EnumInState.B;
                this.RollBack();
                this.err = this.inpatientFeeManager.Err;
                return -1;
            }

            string motherPayAllFee = controlParamMgr.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.SysConst.Use_Mother_PayAllFee, false, "0");
            if (motherPayAllFee == "1")//婴儿的费用收在妈妈的身上 
            {
                ArrayList babyList = radtManager.QueryBabies(patientInfo.ID);
                if (babyList != null && babyList.Count > 0)
                {
                    foreach (FS.HISFC.Models.RADT.PatientInfo baby in babyList)
                    {
                        FS.HISFC.Models.RADT.PatientInfo pTemp = radtManager.GetPatientInfo(baby.ID);
                        if (pTemp != null && !string.IsNullOrEmpty(pTemp.ID))
                        {
                            pTemp.PVisit = patientInfo.PVisit.Clone();

                            if (this.inpatientFeeManager.UpdateInMainInfoBalanced(pTemp, sysdate, balanceNO, new FS.HISFC.Models.Base.FT()) < 0)
                            {
                                patientInfo.PVisit.InState.ID = FS.HISFC.Models.Base.EnumInState.B;
                                this.RollBack();
                                this.err = this.inpatientFeeManager.Err;
                                return -1;
                            }
                        }
                    }
                }
            }

            #endregion

            #region 结算变更记录

            FS.FrameWork.Models.NeuObject oldObj = new FS.FrameWork.Models.NeuObject();
            FS.FrameWork.Models.NeuObject newObj = new FS.FrameWork.Models.NeuObject();
            newObj.ID = patientInfo.SIMainInfo.BalNo;
            newObj.Name = "结算序号";
            if (radtManager.SaveShiftData(patientInfo.ID, FS.HISFC.Models.Base.EnumShiftType.BA, "出院结算", oldObj, newObj) == -1)
            {
                patientInfo.PVisit.InState.ID = FS.HISFC.Models.Base.EnumInState.B;
                this.RollBack();
                this.err = radtManager.Err;
                return -1;
            }

            #endregion

            #region 医保结算处理
            FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy medcareInterfaceProxy = new FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy();
            long returnValue = medcareInterfaceProxy.SetPactCode(patientInfo.Pact.ID);
            if (returnValue != 1)
            {
                patientInfo.PVisit.InState.ID = FS.HISFC.Models.Base.EnumInState.B;
                this.RollBack();
                this.err = medcareInterfaceProxy.ErrMsg;
                return -1;
            }
            ////待遇接口处理－－－－－－－－－－－－－－－－－－－－－－－－－－－－
            medcareInterfaceProxy.SetTrans(this.Trans);
            medcareInterfaceProxy.Connect();

            returnValue = medcareInterfaceProxy.GetRegInfoInpatient(patientInfo);
            if (returnValue != 1)
            {
                patientInfo.PVisit.InState.ID = FS.HISFC.Models.Base.EnumInState.B;
                this.RollBack();
                this.err = medcareInterfaceProxy.ErrMsg;
                return -1;
            }

            ArrayList alFeeItemList = new ArrayList(feeInfoList);
            returnValue = medcareInterfaceProxy.BalanceInpatient(patientInfo, ref alFeeItemList);
            if (returnValue != 1)
            {
                patientInfo.PVisit.InState.ID = FS.HISFC.Models.Base.EnumInState.B;
                this.RollBack();
                this.err = medcareInterfaceProxy.ErrMsg;
                return -1;
            }

            returnValue = medcareInterfaceProxy.Disconnect();
            if (returnValue != 1)
            {
                patientInfo.PVisit.InState.ID = FS.HISFC.Models.Base.EnumInState.B;
                this.RollBack();
                this.err = medcareInterfaceProxy.ErrMsg;
                return -1;
            }

            #endregion

            #region 更新托收单
            if (patientInfo.Pact.PayKind.ID == "03")
            {   //更新结算发票号
                if (this.Updategfhz(patientInfo, invoiceNO, "0") == -1)
                {
                    patientInfo.PVisit.InState.ID = FS.HISFC.Models.Base.EnumInState.B;
                    this.RollBack();
                    this.err = "更新托收单出错!" + this.inpatientFeeManager.Err;
                    return -1;

                }
            }
            #endregion

            //发送消息
            if (InterfaceManager.GetIADT() != null)
            {
                if (InterfaceManager.GetIADT().Balance(patientInfo, true) < 0)
                {
                    patientInfo.PVisit.InState.ID = FS.HISFC.Models.Base.EnumInState.B;
                    this.RollBack();
                    this.err = InterfaceManager.GetIADT().Err;
                    return -1;
                }
            }

            this.Commit();
            return 1;
        }

        /// <summary>
        /// 住院欠费结算
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="balanceType"></param>
        /// <param name="feeItemList"></param>
        /// <param name="prepayList"></param>
        /// <param name="balancePayList"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="splitInvoiceCost"></param>
        /// <param name="invoiceNO"></param>
        /// <param name="realInvoiceNO"></param>
        /// <returns></returns>
        public int OutBalanceOwe(FS.HISFC.Models.RADT.PatientInfo patientInfo,
            List<FS.HISFC.Models.Fee.Inpatient.FeeInfo> feeInfoList,
            List<FS.HISFC.Models.Fee.Inpatient.Prepay> prepayList,
            List<FS.HISFC.Models.Fee.Inpatient.BalancePay> balancePayList,
            bool splitHighInvoice,
            decimal splitHighInvoiceCost,
            ref List<FS.HISFC.Models.Fee.Inpatient.Balance> listBalance)
        {
            if (patientInfo == null || string.IsNullOrEmpty(patientInfo.ID))
            {
                this.err = "结算失败，患者信息为空！";
                return -1;
            }

            this.BeginTransaction();

            #region 患者信息验证
            RADT radtManager = new RADT();
            FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParamMgr = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
            radtManager.SetTrans(this.Trans);
            controlParamMgr.SetTrans(this.Trans);
            //获取时间
            DateTime sysdate = inpatientFeeManager.GetDateTimeFromSysDateTime();

            DateTime inDate = patientInfo.PVisit.InTime;
            //获取上次结算日期
            ArrayList alBalance = inpatientFeeManager.QueryBalancesByInpatientNO(patientInfo.ID, "I");
            if (alBalance != null)
            {
                foreach (FS.HISFC.Models.Fee.Inpatient.Balance balancedHead in alBalance)
                {
                    if (balancedHead.CancelType == FS.HISFC.Models.Base.CancelTypes.Valid &&
                        inDate < balancedHead.EndTime)
                    {
                        inDate = balancedHead.EndTime.AddSeconds(1);
                    }
                }
            }

            //验证患者信息
            FS.HISFC.Models.RADT.PatientInfo patientInfoTemp = radtManager.GetPatientInfo(patientInfo.ID);
            if (patientInfoTemp == null || string.IsNullOrEmpty(patientInfoTemp.ID))
            {
                this.RollBack();
                this.err = "患者信息为空，" + radtManager.Err;
                return -1;
            }

            //已经出院的返回
            if (patientInfoTemp.PVisit.InState.ID.ToString().Equals(FS.HISFC.Models.Base.EnumInState.O))
            {
                this.RollBack();
                this.err = "患者已经出院结算";
                return -1;
            }
            //担保金允许出院结算后返回
            //非出院登记和封帐状态的患者提示错误
            if (!patientInfoTemp.PVisit.InState.ID.ToString().Equals(FS.HISFC.Models.Base.EnumInState.B.ToString())
                &&
                !patientInfoTemp.PVisit.InState.ID.ToString().Equals(FS.HISFC.Models.Base.EnumInState.C.ToString()))
            {
                this.RollBack();
                this.err = "患者状态不是出院登记,不能进行出院结算！" + patientInfo.PVisit.InState.ID;
                return -1;
            }


            #endregion

            #region 发票号和结算号

            //调业务层获取结算次数
            int balanceNO = 0;
            string balNo = inpatientFeeManager.GetNewBalanceNO(patientInfo.ID);
            if (balNo == null || balNo.Length == 0)
            {
                this.RollBack();
                this.err = "获取结算序号出错，" + inpatientFeeManager.Err;
                return -1;
            }
            else
            {
                balanceNO = int.Parse(balNo);
            }

            FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();
            feeIntegrate.SetTrans(this.Trans);
            inpatientFeeManager.SetTrans(this.Trans);
            string invoiceNO = string.Empty;
            string realInvoiceNO = string.Empty;
            string errText = "";

            //获取临时发票信息
            //if (feeIntegrate.GetInvoiceNO(this.inpatientFeeManager.Operator as FS.HISFC.Models.Base.Employee, "I", true, ref invoiceNO, ref realInvoiceNO, ref this.err) != 1)
            //{
            //    this.RollBack();
            //    this.err = "获取发票失败，" + this.err;
            //    return -1;
            //}

            if (feeIntegrate.GetInvoiceNO("I", ref invoiceNO, ref realInvoiceNO, ref errText) < 1 || string.IsNullOrEmpty(invoiceNO) || string.IsNullOrEmpty(realInvoiceNO))
            {
                this.RollBack();
                this.err = "获取发票失败，" + feeIntegrate.Err;
                return -1;
            }

            #endregion

            #region 处理预交金信息

            decimal TotPrepayCost = 0.00M;
            if (prepayList != null && prepayList.Count > 0)
            {
                foreach (FS.HISFC.Models.Fee.Inpatient.Prepay prepay in prepayList)
                {
                    prepay.BalanceNO = balanceNO;
                    prepay.Invoice.ID = invoiceNO;
                    prepay.BalanceOper.ID = this.inpatientFeeManager.Operator.ID;
                    prepay.BalanceOper.OperTime = sysdate;

                    TotPrepayCost += prepay.FT.PrepayCost;//预交金金额

                    //转入预交金处理（目前缺失更新单条的）
                    if (prepay.Name == "转入预交金")
                    {
                        if (inpatientFeeManager.UpdateChangePrepayBalanced(patientInfo.ID, balanceNO) == -1)
                        {
                            this.RollBack();
                            this.err = "结算失败，原因：" + inpatientFeeManager.Err;
                            return -1;
                        }
                    }
                    else
                    {
                        //更新预交发票为结算状态
                        if (inpatientFeeManager.UpdatePrepayBalanced(patientInfo, prepay) <= 0)
                        {
                            this.RollBack();
                            this.err = "结算失败，原因：" + inpatientFeeManager.Err;
                            return -1;
                        }
                    }
                }
            }

            #endregion

            #region 处理费用信息

            //出院结算更新费用
            //判断并发操作
            decimal TotOwnCost = 0.00M;
            decimal TotPubCost = 0.00M;
            decimal TotPayCost = 0.00M;
            decimal TotBalanceCost = 0.00M;
            decimal TotRebateCost = 0.00M;//获得费用总额
            decimal TotShouldPay = 0.00M;
            decimal TotDerateCost = 0.00M;
            //高收费
            decimal TotHighOwnCost = 0.00M;

            foreach (FS.HISFC.Models.Fee.Inpatient.FeeInfo feeInfoHead in feeInfoList)
            {
                TotBalanceCost += feeInfoHead.FT.BalancedCost;
                TotOwnCost += feeInfoHead.FT.OwnCost;
                TotPubCost += feeInfoHead.FT.PubCost;
                TotPayCost += feeInfoHead.FT.PayCost;
                TotRebateCost += feeInfoHead.FT.RebateCost;

                if (feeInfoHead.SplitFeeFlag)
                {
                    TotHighOwnCost += feeInfoHead.FT.OwnCost;
                }

                //如果是公费患者
                if (patientInfo.Pact.PayKind.ID == "03")
                {
                    TotShouldPay += feeInfoHead.FT.OwnCost + feeInfoHead.FT.PayCost;
                }
                else
                {
                    TotShouldPay += feeInfoHead.FT.OwnCost;
                }
            }

            if (patientInfo.Pact.PayKind.ID == "02")
            {
                TotShouldPay = patientInfo.SIMainInfo.OwnCost + patientInfo.SIMainInfo.PayCost;
            }

            //获取减免金额
            TotDerateCost = inpatientFeeManager.GetTotDerateCost(patientInfo.ID);
            if (TotDerateCost < 0)
            {
                this.RollBack();
                this.err = "获取减免总费用出错！" + this.inpatientFeeManager.Err;
                return -1;
            }

            TotShouldPay = TotShouldPay - TotDerateCost - TotRebateCost;

            int result = inpatientFeeManager.UpdateFeeInfoBalanced(patientInfo.ID, balanceNO, sysdate, invoiceNO, false);
            if (result == 0)
            {
                this.RollBack();
                this.err = "可能存在并发操作导致更新费用失败！";
                return -1;
            }

            if (result < 1)
            {
                this.RollBack();
                this.err = inpatientFeeManager.Err;
                return -1;
            }

            if (inpatientFeeManager.UpdateFeeItemListBalanced(patientInfo.ID, balanceNO, invoiceNO, false) == -1)
            {
                this.RollBack();
                this.err = inpatientFeeManager.Err;
                return -1;
            }

            if (inpatientFeeManager.UpdateMedItemListBalanced(patientInfo.ID, balanceNO, invoiceNO) == -1)
            {
                this.RollBack();
                this.err = inpatientFeeManager.Err;
                return -1;
            }

            //更新转入费用
            if (inpatientFeeManager.UpdateAllChangeCostBalanced(patientInfo.ID, balanceNO) == -1)
            {
                this.RollBack();
                this.err = inpatientFeeManager.Err;
                return -1;
            }

            //更新减免为已结算
            if (inpatientFeeManager.UpdateDerateBalanced(patientInfo.ID, balanceNO, invoiceNO) == -1)
            {
                this.RollBack();
                this.err = inpatientFeeManager.Err;
                return -1;
            }

            #endregion

            #region 处理支付方式
            decimal TotBalancePayCost = 0.00M;
            if (balancePayList != null && balancePayList.Count > 0)
            {
                foreach (FS.HISFC.Models.Fee.Inpatient.BalancePay balancePay in balancePayList)
                {
                    TotBalancePayCost += balancePay.FT.TotCost;

                    balancePay.Invoice.ID = invoiceNO;
                    balancePay.BalanceNO = balanceNO;
                    balancePay.BalanceOper.ID = inpatientFeeManager.Operator.ID;
                    balancePay.BalanceOper.OperTime = sysdate;
                    //添加记录
                    if (inpatientFeeManager.InsertBalancePay(balancePay) == -1)
                    {
                        this.RollBack();
                        this.err = inpatientFeeManager.Err;
                        return -1;
                    }
                }
            }

            //欠费金额
            decimal arrearFeeCost = TotBalanceCost - TotBalancePayCost;
            arrearFeeCost = arrearFeeCost > 0 ? arrearFeeCost : 0;

            //欠费金额等于结算金额-支付方式金额
            //if (arrearFeeCost > 0)
            //{
            //    FS.HISFC.Models.Fee.Inpatient.BalancePay balancePay = new FS.HISFC.Models.Fee.Inpatient.BalancePay();
            //    balancePay.PayType.ID = "QF";
            //    balancePay.PayType.Name = "欠费";
            //    balancePay.FT.TotCost = arrearFeeCost;
            //    balancePay.TransKind.ID = "1";
            //    balancePay.TransType = FS.HISFC.Models.Base.TransTypes.Positive;
            //    balancePay.Qty = 1;
            //    balancePay.RetrunOrSupplyFlag = "1";//欠费
            //    balancePay.Invoice.ID = invoiceNO;
            //    balancePay.BalanceNO = balanceNO;
            //    balancePay.BalanceOper.ID = inpatientFeeManager.Operator.ID;
            //    balancePay.BalanceOper.OperTime = sysdate;
            //    //添加记录
            //    if (inpatientFeeManager.InsertBalancePay(balancePay) == -1)
            //    {
            //        this.RollBack();
            //        this.err = inpatientFeeManager.Err;
            //        return -1;
            //    }
            //}
            #endregion

            #region 处理发票信息

            string invoiceType = "ZY01";

            FS.HISFC.BizLogic.Fee.FeeCodeStat feeCodeStat = new FS.HISFC.BizLogic.Fee.FeeCodeStat();
            ArrayList alFeeState = feeCodeStat.QueryFeeCodeStatByReportCode(invoiceType);
            if (alFeeState == null)
            {
                this.RollBack();
                this.err = "结算失败，原因：" + feeCodeStat.Err;
                return -1;
            }

            #region 高收费发票

            List<FS.HISFC.Models.Fee.Inpatient.BalanceList> listSplitFeeBalanceList = new List<FS.HISFC.Models.Fee.Inpatient.BalanceList>();
            ArrayList alSplitFeeBalanceList = new ArrayList();
            if (splitHighInvoice)
            {
                for (int i = 0; i < feeInfoList.Count; )
                {
                    FS.HISFC.Models.Fee.Inpatient.FeeInfo f = feeInfoList[i];
                    if (f.SplitFeeFlag)
                    {
                        feeInfoList.RemoveAt(i);
                        alSplitFeeBalanceList.Add(f);
                        continue;
                    }
                    i++;
                }

                //处理高收费发票明细大类
                if (alSplitFeeBalanceList.Count > 0)
                {
                    if (this.FeeInfoTransFeeStat(patientInfo, FS.HISFC.Models.Base.EBlanceType.Owe, alSplitFeeBalanceList, listSplitFeeBalanceList, sysdate, balanceNO, "", alFeeState) == -1)
                    {
                        this.RollBack();
                        return -1;
                    }
                }
            }


            #endregion

            #region 主发票

            List<FS.HISFC.Models.Fee.Inpatient.BalanceList> balanceList = new List<FS.HISFC.Models.Fee.Inpatient.BalanceList>();
            if (feeInfoList.Count > 0)
            {

                ArrayList alArrearManualFeeInfo = new ArrayList();
                decimal arrearFeeCostTemp = arrearFeeCost;

                decimal splitRate = arrearFeeCost / TotBalanceCost;
                if (splitRate > 1)
                {
                    this.RollBack();
                    this.err = "欠费金额大于结算金额";
                    return -1;
                }
                //循环处理分票明细及主发票明细
                for (int i = 0; i < feeInfoList.Count; i++)
                {
                    FS.HISFC.Models.Fee.Inpatient.FeeInfo InvoiceHeadInfo = feeInfoList[i];
                    InvoiceHeadInfo.FT.ArrearCost = InvoiceHeadInfo.FT.OwnCost * splitRate;
                    arrearFeeCostTemp -= InvoiceHeadInfo.FT.ArrearCost;
                }

                //如果没有分完整，平小数位
                if (Math.Abs(arrearFeeCostTemp) > 0)
                {
                    for (int i = 0; i < feeInfoList.Count; i++)
                    {
                        if (feeInfoList[i].FT.OwnCost - feeInfoList[i].FT.ArrearCost > arrearFeeCostTemp)
                        {
                            feeInfoList[i].FT.ArrearCost += arrearFeeCostTemp;
                            arrearFeeCostTemp = 0;
                            break;
                        }
                        else
                        {
                            feeInfoList[i].FT.ArrearCost += feeInfoList[i].FT.OwnCost;
                            arrearFeeCostTemp -= feeInfoList[i].FT.OwnCost;
                        }
                    }
                }

                ArrayList alFeeInfo = new ArrayList(feeInfoList);

                if (this.FeeInfoTransFeeStat(patientInfo, FS.HISFC.Models.Base.EBlanceType.Owe, alFeeInfo, balanceList, sysdate, balanceNO, invoiceNO, alFeeState) == -1)
                {
                    this.RollBack();
                    return -1;
                }

            }

            #endregion

            #endregion

            #region 处理发票明细

            bool mainInvoice = true;

            #region 主发票

            if (balanceList.Count > 0)
            {
                decimal TotCost = 0m;
                decimal OwnCost = 0m;
                decimal PayCost = 0m;
                decimal PubCost = 0m;
                decimal RebateCost = 0M;
                foreach (FS.HISFC.Models.Fee.Inpatient.BalanceList balance in balanceList)
                {
                    TotCost += balance.BalanceBase.FT.TotCost;
                    OwnCost += balance.BalanceBase.FT.OwnCost;
                    PayCost += balance.BalanceBase.FT.PayCost;
                    PubCost += balance.BalanceBase.FT.PubCost;
                    RebateCost += balance.BalanceBase.FT.RebateCost;
                    balance.BalanceBase.Invoice.ID = invoiceNO;
                    if (this.inpatientFeeManager.InsertBalanceList(patientInfo, balance) < 1)
                    {
                        this.RollBack();
                        this.err = this.inpatientFeeManager.Err;
                        return -1;
                    }

                }

                FS.HISFC.Models.Fee.Inpatient.Balance balanceHead = ((FS.HISFC.Models.Fee.Inpatient.Balance)balanceList[0].BalanceBase).Clone();

                balanceHead.Invoice.ID = invoiceNO;
                balanceHead.BeginTime = inDate;//先取上次结算时间，如果没有就取本次入院时间
                balanceHead.EndTime = sysdate;//结算时间
                balanceHead.FT.TotCost = TotCost;
                balanceHead.FT.OwnCost = OwnCost;
                balanceHead.FT.PayCost = PayCost;
                balanceHead.FT.PubCost = PubCost;
                //如果减免金额大于自费金额，公费不能，说明分了发票
                if (TotHighOwnCost > 0)
                {
                    if (TotDerateCost - TotHighOwnCost > OwnCost)
                    {
                        balanceHead.FT.DerateCost = OwnCost;
                    }
                    else
                    {
                        balanceHead.FT.DerateCost = TotDerateCost > TotHighOwnCost ? TotDerateCost - TotHighOwnCost : 0;
                    }
                }
                else if (TotDerateCost > 0)
                {
                    if (TotDerateCost > OwnCost)
                    {
                        balanceHead.FT.DerateCost = OwnCost;
                    }
                    else
                    {
                        balanceHead.FT.DerateCost = TotDerateCost;
                    }
                }
                TotDerateCost = TotDerateCost - balanceHead.FT.DerateCost;

                balanceHead.CancelType = FS.HISFC.Models.Base.CancelTypes.Valid;
                balanceHead.BalanceSaveType = "0";
                balanceHead.IsMainInvoice = true;
                balanceHead.FT.RebateCost = RebateCost;
                balanceHead.IsMainInvoice = mainInvoice;
                balanceHead.IsTempInvoice = false;

                if (patientInfo.Pact.PayKind.ID == "02")
                {
                    balanceHead.FT.PayCost = patientInfo.SIMainInfo.PayCost;
                    balanceHead.FT.PubCost = patientInfo.SIMainInfo.PubCost;
                    balanceHead.FT.OwnCost = balanceHead.FT.TotCost - balanceHead.FT.PayCost - balanceHead.FT.PubCost;
                }

                if (mainInvoice)
                {
                    mainInvoice = false;
                    balanceHead.FT.PrepayCost = TotPrepayCost;//预交金额
                    balanceHead.FT.TransferPrepayCost = 0.00M;//欠费金额

                    if (TotShouldPay - arrearFeeCost == TotPrepayCost)
                    {
                        balanceHead.FT.SupplyCost = 0M;//应收金额
                        balanceHead.FT.ReturnCost = 0M;//应返金额
                    }
                    else if (TotShouldPay - arrearFeeCost > TotPrepayCost)
                    {
                        balanceHead.FT.SupplyCost = TotShouldPay - arrearFeeCost - TotPrepayCost;//应收金额
                        balanceHead.FT.ReturnCost = 0M;//应返金额
                    }
                    else
                    {
                        balanceHead.FT.SupplyCost = 0M;//应收金额
                        balanceHead.FT.ReturnCost = TotPrepayCost - arrearFeeCost - TotShouldPay;//应返金额
                    }

                    if (balanceHead.BalanceType.ID.ToString() == FS.HISFC.Models.Fee.EnumBalanceType.Q.ToString())
                    {
                        balanceHead.FT.ArrearCost = arrearFeeCost;
                    }
                }

                //插入结算头表
                balanceHead.PrintedInvoiceNO = realInvoiceNO;
                if (this.inpatientFeeManager.InsertBalance(patientInfo, balanceHead) < 1)
                {
                    this.RollBack();
                    this.err = this.inpatientFeeManager.Err;
                    return -1;
                }

                if (feeIntegrate.InsertInvoiceExtend(invoiceNO, "I", realInvoiceNO, "00") < 1)
                {
                    this.RollBack();
                    this.err = this.inpatientFeeManager.Err;
                    return -1;
                }

                if (feeIntegrate.UseInvoiceNO("I", 1, ref invoiceNO, ref realInvoiceNO, ref this.err) != 1)
                {
                    this.RollBack();
                    return -1;
                }

                listBalance.Add(balanceHead);
            }

            #endregion

            #region 高收费发票

            if (listSplitFeeBalanceList.Count > 0)
            {
                if (feeIntegrate.GetInvoiceNO(this.inpatientFeeManager.Operator as FS.HISFC.Models.Base.Employee, "I", true, ref invoiceNO, ref realInvoiceNO, ref this.err) != 1)
                {
                    this.RollBack();
                    this.err = "获取发票失败，" + this.err;
                    return -1;
                }

                decimal TotCost = 0m;
                decimal OwnCost = 0m;
                decimal PayCost = 0m;
                decimal PubCost = 0m;
                decimal RebateCost = 0M;
                foreach (FS.HISFC.Models.Fee.Inpatient.BalanceList balance in listSplitFeeBalanceList)
                {
                    TotCost += balance.BalanceBase.FT.TotCost;
                    OwnCost += balance.BalanceBase.FT.TotCost;
                    PayCost += 0;
                    PubCost += 0;
                    RebateCost += balance.BalanceBase.FT.RebateCost;
                    balance.BalanceBase.Invoice.ID = invoiceNO;
                    ((FS.HISFC.Models.Fee.Inpatient.Balance)balance.BalanceBase).IsTempInvoice = false;//暂时代替高收费发票数据
                    if (this.inpatientFeeManager.InsertBalanceList(patientInfo, balance) < 1)
                    {
                        this.RollBack();
                        this.err = this.inpatientFeeManager.Err;
                        return -1;
                    }

                }

                FS.HISFC.Models.Fee.Inpatient.Balance balanceHead = ((FS.HISFC.Models.Fee.Inpatient.Balance)listSplitFeeBalanceList[0].BalanceBase).Clone();
                balanceHead.Invoice.ID = invoiceNO;
                balanceHead.BeginTime = inDate;//患者入院时间
                balanceHead.EndTime = sysdate;//结算时间
                balanceHead.FT.TotCost = TotCost;
                balanceHead.FT.OwnCost = OwnCost;
                balanceHead.FT.PayCost = PayCost;
                balanceHead.FT.PubCost = PubCost;

                //如果减免金额大于自费金额，公费不能，说明分了发票
                if (TotDerateCost > OwnCost)
                {
                    balanceHead.FT.DerateCost = OwnCost;
                }
                else
                {
                    balanceHead.FT.DerateCost = TotDerateCost;
                }
                TotDerateCost = TotDerateCost - balanceHead.FT.DerateCost;

                balanceHead.CancelType = FS.HISFC.Models.Base.CancelTypes.Valid;
                balanceHead.IsMainInvoice = false;
                balanceHead.IsTempInvoice = false;
                balanceHead.BalanceSaveType = "0";
                //优惠金额
                balanceHead.FT.RebateCost = RebateCost;

                result = inpatientFeeManager.UpdateFeeInfoBalanced(patientInfo.ID, balanceNO, sysdate, invoiceNO, true);
                if (result == 0)
                {
                    this.RollBack();
                    this.err = "可能存在并发操作导致更新费用失败！";
                    return -1;
                }

                if (result < 1)
                {
                    this.RollBack();
                    this.err = inpatientFeeManager.Err;
                    return -1;
                }

                if (inpatientFeeManager.UpdateFeeItemListBalanced(patientInfo.ID, balanceNO, invoiceNO, true) == -1)
                {
                    this.RollBack();
                    this.err = inpatientFeeManager.Err;
                    return -1;
                }


                //插入结算头表
                balanceHead.PrintedInvoiceNO = realInvoiceNO;
                FS.HISFC.Models.Base.PactInfo pact = patientInfo.Pact.Clone();
                patientInfo.Pact.ID = "1";
                patientInfo.Pact.Name = "自费";
                patientInfo.Pact.PayKind.ID = "01";
                patientInfo.Pact.PayKind.Name = "自费";
                if (this.inpatientFeeManager.InsertBalance(patientInfo, balanceHead) < 1)
                {
                    this.RollBack();
                    this.err = this.inpatientFeeManager.Err;
                    patientInfo.Pact = pact;
                    return -1;
                }
                patientInfo.Pact = pact;

                listBalance.Add(balanceHead);
            }
            else
            {
                result = inpatientFeeManager.UpdateFeeInfoBalanced(patientInfo.ID, balanceNO, sysdate, invoiceNO, true);

                if (result < 0)
                {
                    this.RollBack();
                    this.err = inpatientFeeManager.Err;
                    return -1;
                }

                if (inpatientFeeManager.UpdateFeeItemListBalanced(patientInfo.ID, balanceNO, invoiceNO, true) == -1)
                {
                    this.RollBack();
                    this.err = inpatientFeeManager.Err;
                    return -1;
                }
            }

            #endregion

            #endregion

            #region 患者费用处理

            patientInfo.SIMainInfo.BalNo = balNo;
            patientInfo.SIMainInfo.InvoiceNo = invoiceNO;
            patientInfo.PVisit.InState.ID = FS.HISFC.Models.Base.EnumInState.O;//出院结算

            FS.HISFC.Models.Fee.Inpatient.FeeInfo feeInfo = new FS.HISFC.Models.Fee.Inpatient.FeeInfo();
            feeInfo.FT.PubCost = TotPubCost;
            feeInfo.FT.OwnCost = TotOwnCost;
            feeInfo.FT.PayCost = TotPayCost;
            feeInfo.FT.RebateCost = TotRebateCost;
            feeInfo.FT.TotCost = TotBalanceCost;
            feeInfo.FT.PrepayCost = TotPrepayCost;

            //医保处理一下 2012年7月24日16:57:22
            if (patientInfo.Pact.PayKind.ID == "02" && patientInfo.SIMainInfo.TotCost != 0)
            {
                feeInfo.FT.PubCost = patientInfo.SIMainInfo.PubCost;
                feeInfo.FT.OwnCost = patientInfo.SIMainInfo.OwnCost;
                feeInfo.FT.PayCost = patientInfo.SIMainInfo.PayCost;
                //feeInfo.FT.RebateCost = TotRebateCost;
                //feeInfo.FT.TotCost = TotBalanceCost;
                //feeInfo.FT.PrepayCost = TotPrepayCost;
            }

            if (feeInfo.FT.TotCost != feeInfo.FT.OwnCost + feeInfo.FT.PayCost + feeInfo.FT.PubCost + feeInfo.FT.RebateCost)
            {
                this.RollBack();
                this.err = "总费用与分支费用不相等，请检查！";
                return -1;
            }

            if (this.inpatientFeeManager.UpdateInMainInfoBalanced(patientInfo, sysdate, balanceNO, feeInfo.FT) <= 0)
            {
                this.RollBack();
                this.err = this.inpatientFeeManager.Err;
                return -1;
            }

            //开账
            if (this.inpatientFeeManager.OpenAccount(patientInfo.ID) < 0)
            {
                this.RollBack();
                this.err = this.inpatientFeeManager.Err;
                return -1;
            }

            string motherPayAllFee = controlParamMgr.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.SysConst.Use_Mother_PayAllFee, false, "0");
            if (motherPayAllFee == "1")//婴儿的费用收在妈妈的身上 
            {
                ArrayList babyList = radtManager.QueryBabies(patientInfo.ID);
                if (babyList != null && babyList.Count > 0)
                {
                    foreach (FS.HISFC.Models.RADT.PatientInfo baby in babyList)
                    {
                        FS.HISFC.Models.RADT.PatientInfo pTemp = radtManager.GetPatientInfo(baby.ID);
                        if (pTemp != null && !string.IsNullOrEmpty(pTemp.ID))
                        {
                            pTemp.PVisit = patientInfo.PVisit.Clone();

                            if (this.inpatientFeeManager.UpdateInMainInfoBalanced(pTemp, sysdate, balanceNO, new FS.HISFC.Models.Base.FT()) < 0)
                            {
                                this.RollBack();
                                this.err = this.inpatientFeeManager.Err;
                                return -1;
                            }
                        }
                    }
                }
            }

            #endregion

            #region 结算变更记录

            FS.FrameWork.Models.NeuObject oldObj = new FS.FrameWork.Models.NeuObject();
            FS.FrameWork.Models.NeuObject newObj = new FS.FrameWork.Models.NeuObject();
            newObj.ID = patientInfo.SIMainInfo.BalNo;
            newObj.Name = "结算序号";
            if (radtManager.SaveShiftData(patientInfo.ID, FS.HISFC.Models.Base.EnumShiftType.BA, "出院结算", oldObj, newObj) == -1)
            {
                this.RollBack();
                this.err = radtManager.Err;
                return -1;
            }

            #endregion

            #region 医保结算处理(欠费结算不处理医保)
            //FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy medcareInterfaceProxy = new FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy();
            //long returnValue = medcareInterfaceProxy.SetPactCode(patientInfo.Pact.ID);
            //if (returnValue != 1)
            //{
            //    this.RollBack();
            //    this.err = medcareInterfaceProxy.ErrMsg;
            //    return -1;
            //}
            //////待遇接口处理－－－－－－－－－－－－－－－－－－－－－－－－－－－－
            //medcareInterfaceProxy.SetTrans(this.Trans);
            //medcareInterfaceProxy.Connect();

            //returnValue = medcareInterfaceProxy.GetRegInfoInpatient(patientInfo);
            //if (returnValue != 1)
            //{
            //    this.RollBack();
            //    this.err = medcareInterfaceProxy.ErrMsg;
            //    return -1;
            //}

            //ArrayList alFeeItemList = new ArrayList(feeInfoList);
            //returnValue = medcareInterfaceProxy.BalanceInpatient(patientInfo, ref alFeeItemList);
            //if (returnValue != 1)
            //{
            //    this.RollBack();
            //    this.err = medcareInterfaceProxy.ErrMsg;
            //    return -1;
            //}

            //returnValue = medcareInterfaceProxy.Disconnect();
            //if (returnValue != 1)
            //{
            //    this.RollBack();
            //    this.err = medcareInterfaceProxy.ErrMsg;
            //    return -1;
            //}

            #endregion

            //发送消息
            if (InterfaceManager.GetIADT() != null)
            {
                if (InterfaceManager.GetIADT().Balance(patientInfo, true) < 0)
                {
                    this.RollBack();
                    this.err = InterfaceManager.GetIADT().Err;
                    return -1;
                }
            }

            this.Commit();

            return 1;
        }

        /// <summary>
        /// 住院非欠费宰账结算(中五新增)
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="balanceType"></param>
        /// <param name="feeItemList"></param>
        /// <param name="prepayList"></param>
        /// <param name="balancePayList"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="splitInvoiceCost"></param>
        /// <param name="invoiceNO"></param>
        /// <param name="realInvoiceNO"></param>
        /// <returns></returns>
        public int OutCreditBalance(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.HISFC.Models.Base.EBlanceType balanceType,
            List<FS.HISFC.Models.Fee.Inpatient.FeeInfo> feeInfoList,
            List<FS.HISFC.Models.Fee.Inpatient.Prepay> prepayList,
            List<FS.HISFC.Models.Fee.Inpatient.BalancePay> balancePayList,
            bool splitHighInvoice,
            decimal splitHighInvoiceCost,
            ref List<FS.HISFC.Models.Fee.Inpatient.Balance> listBalance)
        {
            if (patientInfo == null || string.IsNullOrEmpty(patientInfo.ID))
            {
                this.err = "结算失败，患者信息为空！";
                return -1;
            }

            this.BeginTransaction();

            #region 患者信息验证
            RADT radtManager = new RADT();
            FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParamMgr = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
            radtManager.SetTrans(this.Trans);
            controlParamMgr.SetTrans(this.Trans);
            //获取时间
            DateTime sysdate = inpatientFeeManager.GetDateTimeFromSysDateTime();

            DateTime inDate = patientInfo.PVisit.InTime;
            //获取上次结算日期
            ArrayList alBalance = inpatientFeeManager.QueryBalancesByInpatientNO(patientInfo.ID, "I");
            if (alBalance != null)
            {
                foreach (FS.HISFC.Models.Fee.Inpatient.Balance balancedHead in alBalance)
                {
                    if (balancedHead.CancelType == FS.HISFC.Models.Base.CancelTypes.Valid &&
                        inDate < balancedHead.EndTime)
                    {
                        inDate = balancedHead.EndTime.AddSeconds(1);
                    }
                }
            }

            //验证患者信息
            FS.HISFC.Models.RADT.PatientInfo patientInfoTemp = radtManager.GetPatientInfo(patientInfo.ID);
            if (patientInfoTemp == null || string.IsNullOrEmpty(patientInfoTemp.ID))
            {
                this.RollBack();
                this.err = "患者信息为空，" + radtManager.Err;
                return -1;
            }

            //已经出院的返回
            if (patientInfoTemp.PVisit.InState.ID.ToString().Equals(FS.HISFC.Models.Base.EnumInState.O))
            {
                this.RollBack();
                this.err = "患者已经出院结算";
                return -1;
            }
            //担保金允许出院结算后返回
            //非出院登记和封帐状态的患者提示错误
            if (!patientInfoTemp.PVisit.InState.ID.ToString().Equals(FS.HISFC.Models.Base.EnumInState.B.ToString())
                &&
                !patientInfoTemp.PVisit.InState.ID.ToString().Equals(FS.HISFC.Models.Base.EnumInState.C.ToString()))
            {
                this.RollBack();
                this.err = "患者状态不是出院登记,不能进行出院结算！" + patientInfo.PVisit.InState.ID;
                return -1;
            }


            #endregion

            #region 发票号和结算号

            //调业务层获取结算次数
            int balanceNO = 0;
            string balNo = inpatientFeeManager.GetNewBalanceNO(patientInfo.ID);
            if (balNo == null || balNo.Length == 0)
            {
                this.RollBack();
                this.err = "获取结算序号出错，" + inpatientFeeManager.Err;
                return -1;
            }
            else
            {
                balanceNO = int.Parse(balNo);
            }

            FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();
            feeIntegrate.SetTrans(this.Trans);
            inpatientFeeManager.SetTrans(this.Trans);
            string invoiceNO = string.Empty;
            string realInvoiceNO = string.Empty;
            string errText = "";

            if (feeIntegrate.GetInvoiceNO("I", ref invoiceNO, ref realInvoiceNO, ref errText) < 1 || string.IsNullOrEmpty(invoiceNO) || string.IsNullOrEmpty(realInvoiceNO))
            {
                this.RollBack();
                this.err = "获取发票失败，" + feeIntegrate.Err;
                return -1;
            }

            #endregion

            #region 处理预交金信息

            decimal TotPrepayCost = 0.00M;
            if (prepayList != null && prepayList.Count > 0)
            {
                foreach (FS.HISFC.Models.Fee.Inpatient.Prepay prepay in prepayList)
                {
                    prepay.BalanceNO = balanceNO;
                    prepay.Invoice.ID = invoiceNO;
                    prepay.BalanceOper.ID = this.inpatientFeeManager.Operator.ID;
                    prepay.BalanceOper.OperTime = sysdate;

                    TotPrepayCost += prepay.FT.PrepayCost;//预交金金额

                    //转入预交金处理（目前缺失更新单条的）
                    if (prepay.Name == "转入预交金")
                    {
                        if (inpatientFeeManager.UpdateChangePrepayBalanced(patientInfo.ID, balanceNO) == -1)
                        {
                            this.RollBack();
                            this.err = "结算失败，原因：" + inpatientFeeManager.Err;
                            return -1;
                        }
                    }
                    else
                    {
                        //更新预交发票为结算状态
                        if (inpatientFeeManager.UpdatePrepayBalanced(patientInfo, prepay) <= 0)
                        {
                            this.RollBack();
                            this.err = "结算失败，原因：" + inpatientFeeManager.Err;
                            return -1;
                        }
                    }
                }
            }

            #endregion

            #region 处理费用信息

            //出院结算更新费用
            //判断并发操作
            decimal TotOwnCost = 0.00M;
            decimal TotPubCost = 0.00M;
            decimal TotPayCost = 0.00M;
            decimal TotBalanceCost = 0.00M;
            decimal TotRebateCost = 0.00M;//获得费用总额
            decimal TotShouldPay = 0.00M;
            decimal TotDerateCost = 0.00M;
            //高收费
            decimal TotHighOwnCost = 0.00M;

            foreach (FS.HISFC.Models.Fee.Inpatient.FeeInfo feeInfoHead in feeInfoList)
            {
                TotBalanceCost += feeInfoHead.FT.BalancedCost;
                TotOwnCost += feeInfoHead.FT.OwnCost;
                TotPubCost += feeInfoHead.FT.PubCost;
                TotPayCost += feeInfoHead.FT.PayCost;
                TotRebateCost += feeInfoHead.FT.RebateCost;

                if (feeInfoHead.SplitFeeFlag)
                {
                    TotHighOwnCost += feeInfoHead.FT.OwnCost;
                }

                //如果是公费患者
                if (patientInfo.Pact.PayKind.ID == "03")
                {
                    TotShouldPay += feeInfoHead.FT.OwnCost + feeInfoHead.FT.PayCost;
                }
                else
                {
                    TotShouldPay += feeInfoHead.FT.OwnCost;
                }
            }

            if (patientInfo.Pact.PayKind.ID == "02")
            {
                TotShouldPay = patientInfo.SIMainInfo.OwnCost + patientInfo.SIMainInfo.PayCost;
            }

            //获取减免金额
            TotDerateCost = inpatientFeeManager.GetTotDerateCost(patientInfo.ID);
            if (TotDerateCost < 0)
            {
                this.RollBack();
                this.err = "获取减免总费用出错！" + this.inpatientFeeManager.Err;
                return -1;
            }

            TotShouldPay = TotShouldPay - TotDerateCost - TotRebateCost;

            int result = inpatientFeeManager.UpdateFeeInfoBalanced(patientInfo.ID, balanceNO, sysdate, invoiceNO, false);
            if (result == 0)
            {
                this.RollBack();
                this.err = "可能存在并发操作导致更新费用失败！";
                return -1;
            }

            if (result < 1)
            {
                this.RollBack();
                this.err = inpatientFeeManager.Err;
                return -1;
            }

            if (inpatientFeeManager.UpdateFeeItemListBalanced(patientInfo.ID, balanceNO, invoiceNO, false) == -1)
            {
                this.RollBack();
                this.err = inpatientFeeManager.Err;
                return -1;
            }

            if (inpatientFeeManager.UpdateMedItemListBalanced(patientInfo.ID, balanceNO, invoiceNO) == -1)
            {
                this.RollBack();
                this.err = inpatientFeeManager.Err;
                return -1;
            }

            //更新转入费用
            if (inpatientFeeManager.UpdateAllChangeCostBalanced(patientInfo.ID, balanceNO) == -1)
            {
                this.RollBack();
                this.err = inpatientFeeManager.Err;
                return -1;
            }

            //更新减免为已结算
            if (inpatientFeeManager.UpdateDerateBalanced(patientInfo.ID, balanceNO, invoiceNO) == -1)
            {
                this.RollBack();
                this.err = inpatientFeeManager.Err;
                return -1;
            }

            #endregion

            #region 处理支付方式
            decimal TotBalancePayCost = 0.00M;
            if (balancePayList != null && balancePayList.Count > 0)
            {
                foreach (FS.HISFC.Models.Fee.Inpatient.BalancePay balancePay in balancePayList)
                {
                    TotBalancePayCost += balancePay.FT.TotCost;

                    balancePay.Invoice.ID = invoiceNO;
                    balancePay.BalanceNO = balanceNO;
                    balancePay.BalanceOper.ID = inpatientFeeManager.Operator.ID;
                    balancePay.BalanceOper.OperTime = sysdate;
                    //添加记录
                    if (inpatientFeeManager.InsertBalancePay(balancePay) == -1)
                    {
                        this.RollBack();
                        this.err = inpatientFeeManager.Err;
                        return -1;
                    }
                }
            }

            //欠费金额
            decimal arrearFeeCost = TotBalanceCost - TotBalancePayCost;
            arrearFeeCost = arrearFeeCost > 0 ? arrearFeeCost : 0;

            //欠费金额等于结算金额-支付方式金额
            if (arrearFeeCost > 0 && balanceType == FS.HISFC.Models.Base.EBlanceType.Owe)
            {
                FS.HISFC.Models.Fee.Inpatient.BalancePay balancePay = new FS.HISFC.Models.Fee.Inpatient.BalancePay();
                balancePay.PayType.ID = "QF";
                balancePay.PayType.Name = "欠费";
                balancePay.FT.TotCost = arrearFeeCost;
                balancePay.TransKind.ID = "1";
                balancePay.TransType = FS.HISFC.Models.Base.TransTypes.Positive;
                balancePay.Qty = 1;
                balancePay.RetrunOrSupplyFlag = "1";//欠费
                balancePay.Invoice.ID = invoiceNO;
                balancePay.BalanceNO = balanceNO;
                balancePay.BalanceOper.ID = inpatientFeeManager.Operator.ID;
                balancePay.BalanceOper.OperTime = sysdate;
                //添加记录
                if (inpatientFeeManager.InsertBalancePay(balancePay) == -1)
                {
                    this.RollBack();
                    this.err = inpatientFeeManager.Err;
                    return -1;
                }
            }
            #endregion

            #region 处理发票信息

            string invoiceType = "ZY01";

            FS.HISFC.BizLogic.Fee.FeeCodeStat feeCodeStat = new FS.HISFC.BizLogic.Fee.FeeCodeStat();
            ArrayList alFeeState = feeCodeStat.QueryFeeCodeStatByReportCode(invoiceType);
            if (alFeeState == null)
            {
                this.RollBack();
                this.err = "结算失败，原因：" + feeCodeStat.Err;
                return -1;
            }

            #region 欠费发票

            List<FS.HISFC.Models.Fee.Inpatient.BalanceList> alArrearBalanceList = new List<FS.HISFC.Models.Fee.Inpatient.BalanceList>();
            if (arrearFeeCost > 0 && balanceType == FS.HISFC.Models.Base.EBlanceType.OweMid)
            {
                ArrayList alArrearManualFeeInfo = new ArrayList();
                decimal arrearFeeCostTemp = arrearFeeCost;

                decimal splitRate = arrearFeeCost / TotBalanceCost;
                if (splitRate > 1)
                {
                    this.RollBack();
                    this.err = "欠费金额大于结算金额";
                    return -1;
                }
                //循环处理分票明细及主发票明细
                for (int i = 0; i < feeInfoList.Count; i++)
                {
                    FS.HISFC.Models.Fee.Inpatient.FeeInfo InvoiceHeadInfo = feeInfoList[i];
                    FS.HISFC.Models.Fee.Inpatient.FeeInfo InvoiceManualInfo = InvoiceHeadInfo.Clone();

                    InvoiceManualInfo.FT.OwnCost = FS.FrameWork.Function.NConvert.ToDecimal(FS.FrameWork.Public.String.FormatNumberReturnString(InvoiceHeadInfo.FT.OwnCost * splitRate, 2));
                    InvoiceManualInfo.FT.PayCost = 0.00M;
                    InvoiceManualInfo.FT.PubCost = 0.00M;
                    InvoiceManualInfo.FT.RebateCost = 0.00M;

                    InvoiceManualInfo.FT.TotCost = InvoiceManualInfo.FT.OwnCost + InvoiceManualInfo.FT.PayCost + InvoiceManualInfo.FT.PubCost + InvoiceManualInfo.FT.RebateCost;

                    InvoiceHeadInfo.FT.TotCost = InvoiceHeadInfo.FT.TotCost - InvoiceManualInfo.FT.TotCost;
                    InvoiceHeadInfo.FT.OwnCost = InvoiceHeadInfo.FT.OwnCost - InvoiceManualInfo.FT.OwnCost;
                    InvoiceHeadInfo.FT.PayCost = InvoiceHeadInfo.FT.PayCost - InvoiceManualInfo.FT.PayCost;
                    InvoiceHeadInfo.FT.PubCost = InvoiceHeadInfo.FT.PubCost - InvoiceManualInfo.FT.PubCost;
                    InvoiceHeadInfo.FT.RebateCost = InvoiceHeadInfo.FT.RebateCost - InvoiceManualInfo.FT.RebateCost;
                    arrearFeeCostTemp -= InvoiceManualInfo.FT.TotCost;

                    alArrearManualFeeInfo.Add(InvoiceManualInfo);
                }

                //如果没有分完整，平小数位
                if (Math.Abs(arrearFeeCostTemp) > 0)
                {
                    for (int i = 0; i < feeInfoList.Count; i++)
                    {
                        if (feeInfoList[i].FT.OwnCost + arrearFeeCostTemp > 0)
                        {
                            FS.HISFC.Models.Fee.Inpatient.FeeInfo InvoiceManualInfo = feeInfoList[i].Clone();
                            InvoiceManualInfo.FT.OwnCost = arrearFeeCostTemp;
                            InvoiceManualInfo.FT.PayCost = 0.00M;
                            InvoiceManualInfo.FT.PubCost = 0.00M;
                            InvoiceManualInfo.FT.RebateCost = 0.00M;
                            InvoiceManualInfo.FT.TotCost = arrearFeeCostTemp;
                            feeInfoList[i].FT.OwnCost -= arrearFeeCostTemp;
                            feeInfoList[i].FT.TotCost -= arrearFeeCostTemp;
                            alArrearManualFeeInfo.Add(InvoiceManualInfo);
                            break;
                        }
                    }
                }

                if (this.FeeInfoTransFeeStat(patientInfo, balanceType, alArrearManualFeeInfo, alArrearBalanceList, sysdate, balanceNO, "", alFeeState) == -1)
                {
                    this.RollBack();
                    return -1;
                }

            }

            #endregion

            #region 优惠发票

            List<FS.HISFC.Models.Fee.Inpatient.BalanceList> alBalanceListRebate = new List<FS.HISFC.Models.Fee.Inpatient.BalanceList>();
            bool IsRebatePrint = controlParamMgr.GetControlParam<bool>("100007");
            //判断优惠是否单独打印发票
            if (IsRebatePrint)
            {
                ArrayList alInvoiceRebateFeeInfo = new ArrayList();
                for (int i = 0; i < feeInfoList.Count; i++)
                {
                    FS.HISFC.Models.Fee.Inpatient.FeeInfo f = feeInfoList[i];
                    if (f.FT.RebateCost > 0)
                    {
                        FS.HISFC.Models.Fee.Inpatient.FeeInfo FeeInfoRebate = new FS.HISFC.Models.Fee.Inpatient.FeeInfo();
                        FeeInfoRebate.Item.MinFee.ID = f.Item.MinFee.ID;
                        FeeInfoRebate.FT.RebateCost = f.FT.RebateCost;
                        FeeInfoRebate.FT.TotCost = f.FT.TotCost;
                        alInvoiceRebateFeeInfo.Add(FeeInfoRebate);
                    }
                }

                if (this.FeeInfoTransFeeStat(patientInfo, balanceType, alInvoiceRebateFeeInfo, alBalanceListRebate, sysdate, balanceNO, invoiceNO, alFeeState) == -1)
                {
                    this.RollBack();
                    return -1;
                }
            }

            #endregion

            #region 高收费发票

            List<FS.HISFC.Models.Fee.Inpatient.BalanceList> listSplitFeeBalanceList = new List<FS.HISFC.Models.Fee.Inpatient.BalanceList>();
            ArrayList alSplitFeeBalanceList = new ArrayList();
            if (splitHighInvoice)
            {
                for (int i = 0; i < feeInfoList.Count; )
                {
                    FS.HISFC.Models.Fee.Inpatient.FeeInfo f = feeInfoList[i];
                    if (f.SplitFeeFlag)
                    {
                        feeInfoList.RemoveAt(i);
                        alSplitFeeBalanceList.Add(f);
                        continue;
                    }
                    i++;
                }

                //处理高收费发票明细大类
                if (alSplitFeeBalanceList.Count > 0)
                {
                    if (this.FeeInfoTransFeeStat(patientInfo, balanceType, alSplitFeeBalanceList, listSplitFeeBalanceList, sysdate, balanceNO, "", alFeeState) == -1)
                    {
                        this.RollBack();
                        return -1;
                    }
                }
            }


            #endregion

            #region 婴儿发票
            //List<FS.HISFC.Models.Fee.Inpatient.BalanceList> alBalanceListBaby = new List<FS.HISFC.Models.Fee.Inpatient.BalanceList>();
            //if (patientInfo.IsBaby)
            //{
            //    bool IsBabyPrint = FS.FrameWork.Function.NConvert.ToBoolean(controlParm.QueryControlerInfo("100004"));
            //    //判断婴儿费用是否单独打印发票 
            //    if (IsBabyPrint)
            //    {
            //        ArrayList alInvoiceBabyFeeInfo = new ArrayList();
            //        ArrayList al = inpatientFeeManager.QueryFeeInfosGroupByMinFeeForBaby(patientInfo.ID, beginTime, endTime, "0");
            //        if (al == null)
            //        {
            //            this.RollBack();
            //            this.err = "结算失败，原因：" + inpatientFeeManager.Err;
            //            return -1;
            //        }

            //        for (int i = 0; i < feeInfoList.Count; i++)
            //        {
            //            FS.HISFC.Models.Fee.Inpatient.FeeInfo f = feeInfoList[i];
            //            for (int j = 0; j < al.Count; j++)
            //            {
            //                FS.HISFC.Models.Fee.Inpatient.FeeInfo feeinfoBaby = (FS.HISFC.Models.Fee.Inpatient.FeeInfo)al[j];
            //                if (f.Item.MinFee.ID == feeinfoBaby.Item.MinFee.ID)
            //                {
            //                    if (f.FT.TotCost - feeinfoBaby.FT.TotCost < 0)
            //                    {
            //                        this.RollBack();
            //                        this.err = "结算失败，原因：输入的结算金额小于婴儿实际发生费用";
            //                        return -1;

            //                    }
            //                    f.FT.TotCost = f.FT.TotCost - feeinfoBaby.FT.TotCost;
            //                    f.FT.OwnCost = f.FT.OwnCost - feeinfoBaby.FT.OwnCost;
            //                    f.FT.PayCost = f.FT.PayCost - feeinfoBaby.FT.PayCost;
            //                    f.FT.PubCost = f.FT.PubCost - feeinfoBaby.FT.PubCost;
            //                    if (!IsRebatePrint)
            //                    {
            //                        f.FT.RebateCost = f.FT.RebateCost - feeinfoBaby.FT.RebateCost;
            //                    }
            //                    alInvoiceBabyFeeInfo.Add(feeinfoBaby);
            //                }
            //            }
            //        }

            //        if (this.FeeInfoTransFeeStat(patientInfo, balanceType, alInvoiceBabyFeeInfo, alBalanceListBaby, sysdate, balanceNO, "", alFeeState) == -1)
            //        {
            //            this.RollBack();
            //            return -1;
            //        }
            //    }

            //}

            #endregion

            #region 手工发票

            List<FS.HISFC.Models.Fee.Inpatient.BalanceList> alBalanceListMannal = new List<FS.HISFC.Models.Fee.Inpatient.BalanceList>();
            //if (splitInvoiceCost > 0)
            //{
            //    ArrayList alInvoiceManualFeeInfo = new ArrayList();
            //    decimal splitInvoiceCostTemp = splitInvoiceCost;

            //    decimal splitRate = splitInvoiceCost / TotBalanceCost;
            //    if (balanceType == FS.HISFC.Models.Base.EBlanceType.OweMid)
            //    {
            //        splitRate = splitInvoiceCost / (TotBalanceCost - arrearFeeCost);
            //    }

            //    if (splitRate > 1)
            //    {
            //        this.RollBack();
            //        this.err = "发票金额大于结算金额";
            //        return -1;
            //    }

            //    //循环处理分票明细及主发票明细
            //    for (int i = 0; i < feeInfoList.Count; i++)
            //    {
            //        FS.HISFC.Models.Fee.Inpatient.FeeInfo InvoiceHeadInfo = feeInfoList[i];
            //        FS.HISFC.Models.Fee.Inpatient.FeeInfo InvoiceManualInfo = InvoiceHeadInfo.Clone();

            //        InvoiceManualInfo.FT.OwnCost = FS.FrameWork.Function.NConvert.ToDecimal(FS.FrameWork.Public.String.FormatNumberReturnString(InvoiceHeadInfo.FT.OwnCost * splitRate, 2));
            //        InvoiceManualInfo.FT.PayCost = FS.FrameWork.Function.NConvert.ToDecimal(FS.FrameWork.Public.String.FormatNumberReturnString(InvoiceHeadInfo.FT.PayCost * splitRate, 2));
            //        InvoiceManualInfo.FT.PubCost = FS.FrameWork.Function.NConvert.ToDecimal(FS.FrameWork.Public.String.FormatNumberReturnString(InvoiceHeadInfo.FT.PubCost * splitRate, 2));
            //        InvoiceManualInfo.FT.RebateCost = FS.FrameWork.Function.NConvert.ToDecimal(FS.FrameWork.Public.String.FormatNumberReturnString(InvoiceHeadInfo.FT.RebateCost * splitRate, 2));

            //        InvoiceManualInfo.FT.TotCost = InvoiceManualInfo.FT.OwnCost + InvoiceManualInfo.FT.PayCost + InvoiceManualInfo.FT.PubCost + InvoiceManualInfo.FT.RebateCost;

            //        InvoiceHeadInfo.FT.TotCost = InvoiceHeadInfo.FT.TotCost - InvoiceManualInfo.FT.TotCost;
            //        InvoiceHeadInfo.FT.OwnCost = InvoiceHeadInfo.FT.OwnCost - InvoiceManualInfo.FT.OwnCost;
            //        InvoiceHeadInfo.FT.PayCost = InvoiceHeadInfo.FT.PayCost - InvoiceManualInfo.FT.PayCost;
            //        InvoiceHeadInfo.FT.PubCost = InvoiceHeadInfo.FT.PubCost - InvoiceManualInfo.FT.PubCost;
            //        InvoiceHeadInfo.FT.RebateCost = InvoiceHeadInfo.FT.RebateCost - InvoiceManualInfo.FT.RebateCost;
            //        splitInvoiceCostTemp -= InvoiceManualInfo.FT.TotCost;

            //        alInvoiceManualFeeInfo.Add(InvoiceManualInfo);
            //    }

            //    //如果没有分完整，平小数位
            //    if (Math.Abs(splitInvoiceCostTemp) > 0)
            //    {
            //        for (int i = 0; i < feeInfoList.Count; i++)
            //        {
            //            if (feeInfoList[i].FT.OwnCost + splitInvoiceCostTemp > 0)
            //            {
            //                FS.HISFC.Models.Fee.Inpatient.FeeInfo InvoiceManualInfo = feeInfoList[i].Clone();
            //                InvoiceManualInfo.FT.OwnCost = splitInvoiceCostTemp;
            //                InvoiceManualInfo.FT.PayCost = 0.00M;
            //                InvoiceManualInfo.FT.PubCost = 0.00M;
            //                InvoiceManualInfo.FT.RebateCost = 0.00M;
            //                InvoiceManualInfo.FT.TotCost = splitInvoiceCostTemp;
            //                feeInfoList[i].FT.OwnCost -= splitInvoiceCostTemp;
            //                feeInfoList[i].FT.TotCost -= splitInvoiceCostTemp;
            //                alInvoiceManualFeeInfo.Add(InvoiceManualInfo);
            //                break;
            //            }
            //        }
            //    }

            //    if (this.FeeInfoTransFeeStat(patientInfo, balanceType, alInvoiceManualFeeInfo, alBalanceListMannal, sysdate, balanceNO, "", alFeeState) == -1)
            //    {
            //        this.RollBack();
            //        return -1;
            //    }
            //}

            #endregion

            #region 主发票

            List<FS.HISFC.Models.Fee.Inpatient.BalanceList> balanceList = new List<FS.HISFC.Models.Fee.Inpatient.BalanceList>();
            if (feeInfoList.Count > 0)
            {
                if (this.FeeInfoTransFeeStat(patientInfo, balanceType, new ArrayList(feeInfoList), balanceList, sysdate, balanceNO, invoiceNO, alFeeState) == -1)
                {
                    this.RollBack();
                    return -1;
                }
            }

            #endregion

            #endregion

            #region 处理发票明细

            bool mainInvoice = true;

            #region 主发票

            if (balanceList.Count > 0)
            {
                decimal TotCost = 0m;
                decimal OwnCost = 0m;
                decimal PayCost = 0m;
                decimal PubCost = 0m;
                decimal RebateCost = 0M;
                foreach (FS.HISFC.Models.Fee.Inpatient.BalanceList balance in balanceList)
                {
                    TotCost += balance.BalanceBase.FT.TotCost;
                    OwnCost += balance.BalanceBase.FT.OwnCost;
                    PayCost += balance.BalanceBase.FT.PayCost;
                    PubCost += balance.BalanceBase.FT.PubCost;
                    RebateCost += balance.BalanceBase.FT.RebateCost;
                    if (balanceType == FS.HISFC.Models.Base.EBlanceType.OweMid)
                    {
                        balance.BalanceBase.BalanceType.ID = FS.HISFC.Models.Fee.EnumBalanceType.O;
                    }
                    balance.BalanceBase.Invoice.ID = invoiceNO;
                    if (this.inpatientFeeManager.InsertBalanceList(patientInfo, balance) < 1)
                    {
                        this.RollBack();
                        this.err = this.inpatientFeeManager.Err;
                        return -1;
                    }

                }

                FS.HISFC.Models.Fee.Inpatient.Balance balanceHead = ((FS.HISFC.Models.Fee.Inpatient.Balance)balanceList[0].BalanceBase).Clone();

                balanceHead.Invoice.ID = invoiceNO;
                balanceHead.BeginTime = inDate;//先取上次结算时间，如果没有就取本次入院时间
                balanceHead.EndTime = sysdate;//结算时间
                balanceHead.FT.TotCost = TotCost;
                balanceHead.FT.OwnCost = OwnCost;
                balanceHead.FT.PayCost = PayCost;
                balanceHead.FT.PubCost = PubCost;

                //如果减免金额大于自费金额，公费不能，说明分了发票
                if (TotHighOwnCost > 0)
                {
                    if (TotDerateCost - TotHighOwnCost > OwnCost)
                    {
                        balanceHead.FT.DerateCost = OwnCost;
                    }
                    else
                    {
                        balanceHead.FT.DerateCost = TotDerateCost > TotHighOwnCost ? TotDerateCost - TotHighOwnCost : 0;
                    }
                }
                else if (TotDerateCost > 0)
                {
                    if (TotDerateCost > OwnCost)
                    {
                        balanceHead.FT.DerateCost = OwnCost;
                    }
                    else
                    {
                        balanceHead.FT.DerateCost = TotDerateCost;
                    }
                }
                TotDerateCost = TotDerateCost - balanceHead.FT.DerateCost;

                balanceHead.CancelType = FS.HISFC.Models.Base.CancelTypes.Valid;
                balanceHead.BalanceSaveType = "0";
                balanceHead.IsMainInvoice = true;

                //优惠金额
                if (!IsRebatePrint)
                {
                    balanceHead.FT.RebateCost = RebateCost;
                }
                balanceHead.IsMainInvoice = mainInvoice;
                if (mainInvoice)
                {
                    mainInvoice = false;
                    balanceHead.FT.PrepayCost = TotPrepayCost;//预交金额
                    balanceHead.FT.TransferPrepayCost = 0.00M;//欠费金额

                    if (TotShouldPay == TotPrepayCost)
                    {
                        balanceHead.FT.SupplyCost = 0M;//应收金额
                        balanceHead.FT.ReturnCost = 0M;//应返金额
                    }
                    else if (TotShouldPay > TotPrepayCost)
                    {
                        balanceHead.FT.SupplyCost = TotShouldPay - TotPrepayCost;//应收金额
                        balanceHead.FT.ReturnCost = 0M;//应返金额
                    }
                    else
                    {
                        balanceHead.FT.SupplyCost = 0M;//应收金额
                        balanceHead.FT.ReturnCost = TotPrepayCost - TotShouldPay;//应返金额
                    }

                    //balanceHead.FT.SupplyCost = balanceHead.FT.SupplyCost - arrearFeeCost;

                    if (balanceHead.BalanceType.ID.ToString() == FS.HISFC.Models.Fee.EnumBalanceType.Q.ToString())
                    {
                        balanceHead.FT.ArrearCost = arrearFeeCost;
                    }
                }
                else
                {
                    if (balanceHead.BalanceType.ID.ToString() == FS.HISFC.Models.Fee.EnumBalanceType.Q.ToString())
                    {
                        balanceHead.FT.ArrearCost = arrearFeeCost;
                    }
                }

                if (patientInfo.Pact.PayKind.ID == "02")
                {
                    //balanceHead.FT.OwnCost = patientInfo.SIMainInfo.OwnCost - balanceHead.FT.DerateCost - balanceHead.FT.RebateCost;
                    balanceHead.FT.PayCost = patientInfo.SIMainInfo.PayCost;
                    balanceHead.FT.PubCost = patientInfo.SIMainInfo.PubCost;
                    balanceHead.FT.OwnCost = balanceHead.FT.TotCost - balanceHead.FT.PayCost - balanceHead.FT.PubCost;
                }

                //插入结算头表
                balanceHead.PrintedInvoiceNO = realInvoiceNO;
                if (this.inpatientFeeManager.InsertBalance(patientInfo, balanceHead) < 1)
                {
                    this.RollBack();
                    this.err = this.inpatientFeeManager.Err;
                    return -1;
                }

                if (feeIntegrate.InsertInvoiceExtend(invoiceNO, "I", realInvoiceNO, "00") < 1)
                {
                    this.RollBack();
                    this.err = this.inpatientFeeManager.Err;
                    return -1;
                }

                if (feeIntegrate.UseInvoiceNO("I", 1, ref invoiceNO, ref realInvoiceNO, ref this.err) != 1)
                {
                    this.RollBack();
                    return -1;
                }

                listBalance.Add(balanceHead);
            }

            #endregion

            #region 手工发票

            if (alBalanceListMannal.Count > 0)
            {
                if (feeIntegrate.GetInvoiceNO("I", ref invoiceNO, ref realInvoiceNO, ref errText) < 1 || string.IsNullOrEmpty(invoiceNO) || string.IsNullOrEmpty(realInvoiceNO))
                {
                    this.RollBack();
                    this.err = "获取发票失败，" + feeIntegrate.Err;
                    return -1;
                }

                decimal TotCost = 0m;
                decimal OwnCost = 0m;
                decimal PayCost = 0m;
                decimal PubCost = 0m;
                decimal RebateCost = 0M;
                foreach (FS.HISFC.Models.Fee.Inpatient.BalanceList balance in alBalanceListMannal)
                {
                    TotCost += balance.BalanceBase.FT.TotCost;
                    OwnCost += balance.BalanceBase.FT.OwnCost;
                    PayCost += balance.BalanceBase.FT.PayCost;
                    PubCost += balance.BalanceBase.FT.PubCost;
                    RebateCost += balance.BalanceBase.FT.RebateCost;

                    if (balanceType == FS.HISFC.Models.Base.EBlanceType.OweMid)
                    {
                        balance.BalanceBase.BalanceType.ID = FS.HISFC.Models.Fee.EnumBalanceType.O;
                    }

                    balance.BalanceBase.Invoice.ID = invoiceNO;
                    if (this.inpatientFeeManager.InsertBalanceList(patientInfo, balance) < 1)
                    {
                        this.RollBack();
                        this.err = this.inpatientFeeManager.Err;
                        return -1;
                    }

                }

                FS.HISFC.Models.Fee.Inpatient.Balance balanceHead = ((FS.HISFC.Models.Fee.Inpatient.Balance)alBalanceListMannal[0].BalanceBase).Clone();

                balanceHead.Invoice.ID = invoiceNO;
                balanceHead.BeginTime = inDate;//患者入院时间
                balanceHead.EndTime = sysdate;//结算时间
                balanceHead.FT.TotCost = TotCost;
                balanceHead.FT.OwnCost = OwnCost;
                balanceHead.FT.PayCost = PayCost;
                balanceHead.FT.PubCost = PubCost;


                balanceHead.CancelType = FS.HISFC.Models.Base.CancelTypes.Valid;
                balanceHead.IsMainInvoice = false;
                balanceHead.BalanceSaveType = "0";

                //优惠金额
                if (!IsRebatePrint)
                {
                    balanceHead.FT.RebateCost = RebateCost;
                }
                //医保处理 2012年7月24日18:10:28
                if (patientInfo.Pact.PayKind.ID == "02")
                {
                    //balanceHead.FT.OwnCost = patientInfo.SIMainInfo.OwnCost - balanceHead.FT.DerateCost - balanceHead.FT.RebateCost;
                    balanceHead.FT.PayCost = patientInfo.SIMainInfo.PayCost;
                    balanceHead.FT.PubCost = patientInfo.SIMainInfo.PubCost;
                    balanceHead.FT.OwnCost = balanceHead.FT.TotCost - balanceHead.FT.PayCost - balanceHead.FT.PubCost;
                }

                //如果减免金额大于自费金额，公费不能，说明分了发票
                if (TotHighOwnCost > 0)
                {
                    if (TotDerateCost - TotHighOwnCost > OwnCost)
                    {
                        balanceHead.FT.DerateCost = OwnCost;
                    }
                    else
                    {
                        balanceHead.FT.DerateCost = TotDerateCost > TotHighOwnCost ? TotDerateCost - TotHighOwnCost : 0;
                    }
                }
                else if (TotDerateCost > 0)
                {
                    if (TotDerateCost > OwnCost)
                    {
                        balanceHead.FT.DerateCost = OwnCost;
                    }
                    else
                    {
                        balanceHead.FT.DerateCost = TotDerateCost;
                    }
                }
                TotDerateCost = TotDerateCost - balanceHead.FT.DerateCost;


                balanceHead.IsMainInvoice = mainInvoice;
                if (mainInvoice)
                {
                    mainInvoice = false;
                    balanceHead.FT.PrepayCost = TotPrepayCost;//预交金额
                    balanceHead.FT.TransferPrepayCost = 0.00M;//欠费金额
                    if (TotShouldPay == TotPrepayCost)
                    {
                        balanceHead.FT.SupplyCost = 0M;//应收金额
                        balanceHead.FT.ReturnCost = 0M;//应返金额
                    }
                    else if (TotShouldPay > TotPrepayCost)
                    {
                        balanceHead.FT.SupplyCost = TotShouldPay - TotPrepayCost;//应收金额
                        balanceHead.FT.ReturnCost = 0M;//应返金额
                    }
                    else
                    {
                        balanceHead.FT.SupplyCost = 0M;//应收金额
                        balanceHead.FT.ReturnCost = TotPrepayCost - TotShouldPay;//应返金额
                    }

                    //balanceHead.FT.SupplyCost = balanceHead.FT.SupplyCost - arrearFeeCost;

                    if (balanceHead.BalanceType.ID.ToString() == FS.HISFC.Models.Fee.EnumBalanceType.Q.ToString())
                    {
                        balanceHead.FT.ArrearCost = arrearFeeCost;
                    }
                }
                else
                {
                    if (balanceHead.BalanceType.ID.ToString() == FS.HISFC.Models.Fee.EnumBalanceType.Q.ToString())
                    {
                        balanceHead.FT.ArrearCost = arrearFeeCost;
                    }
                }
                ////医保处理 2012年7月24日18:10:28
                //if (patientInfo.Pact.PayKind.ID == "02")
                //{
                //    balanceHead.FT.OwnCost = patientInfo.SIMainInfo.OwnCost - balanceHead.FT.DerateCost - balanceHead.FT.RebateCost;
                //    balanceHead.FT.PayCost = patientInfo.SIMainInfo.PayCost;
                //    balanceHead.FT.PubCost = patientInfo.SIMainInfo.PubCost;
                //}
                //插入结算头表
                balanceHead.PrintedInvoiceNO = realInvoiceNO;
                if (this.inpatientFeeManager.InsertBalance(patientInfo, balanceHead) < 1)
                {
                    this.RollBack();
                    this.err = this.inpatientFeeManager.Err;
                    return -1;
                }

                if (feeIntegrate.InsertInvoiceExtend(invoiceNO, "I", realInvoiceNO, "00") < 1)
                {
                    this.RollBack();
                    this.err = this.inpatientFeeManager.Err;
                    return -1;
                }

                if (feeIntegrate.UseInvoiceNO("I", 1, ref invoiceNO, ref realInvoiceNO, ref this.err) != 1)
                {
                    this.RollBack();
                    return -1;
                }

                listBalance.Add(balanceHead);
            }
            #endregion

            #region 高收费发票

            if (listSplitFeeBalanceList.Count > 0)
            {
                if (feeIntegrate.GetInvoiceNO("I", ref invoiceNO, ref realInvoiceNO, ref errText) < 1 || string.IsNullOrEmpty(invoiceNO) || string.IsNullOrEmpty(realInvoiceNO))
                {
                    this.RollBack();
                    this.err = "获取发票失败，" + feeIntegrate.Err;
                    return -1;
                }

                decimal TotCost = 0m;
                decimal OwnCost = 0m;
                decimal PayCost = 0m;
                decimal PubCost = 0m;
                decimal RebateCost = 0M;
                foreach (FS.HISFC.Models.Fee.Inpatient.BalanceList balance in listSplitFeeBalanceList)
                {
                    TotCost += balance.BalanceBase.FT.TotCost;
                    OwnCost += balance.BalanceBase.FT.TotCost;
                    PayCost += 0;
                    PubCost += 0;
                    RebateCost += balance.BalanceBase.FT.RebateCost;

                    if (balanceType == FS.HISFC.Models.Base.EBlanceType.OweMid)
                    {
                        balance.BalanceBase.BalanceType.ID = FS.HISFC.Models.Fee.EnumBalanceType.O;
                    }

                    balance.BalanceBase.Invoice.ID = invoiceNO;
                    balance.BalanceBase.SplitFeeFlag = false;//高收费标记
                    if (this.inpatientFeeManager.InsertBalanceList(patientInfo, balance) < 1)
                    {
                        this.RollBack();
                        this.err = this.inpatientFeeManager.Err;
                        return -1;
                    }

                }

                FS.HISFC.Models.Fee.Inpatient.Balance balanceHead = ((FS.HISFC.Models.Fee.Inpatient.Balance)listSplitFeeBalanceList[0].BalanceBase).Clone();
                balanceHead.Invoice.ID = invoiceNO;
                balanceHead.BeginTime = inDate;//患者入院时间
                balanceHead.EndTime = sysdate;//结算时间
                balanceHead.FT.TotCost = TotCost;
                balanceHead.FT.OwnCost = OwnCost;
                balanceHead.FT.PayCost = PayCost;
                balanceHead.FT.PubCost = PubCost;

                //如果减免金额大于自费金额，公费不能，说明分了发票
                if (TotDerateCost > OwnCost)
                {
                    balanceHead.FT.DerateCost = OwnCost;
                }
                else
                {
                    balanceHead.FT.DerateCost = TotDerateCost;
                }
                TotDerateCost = TotDerateCost - balanceHead.FT.DerateCost;

                balanceHead.CancelType = FS.HISFC.Models.Base.CancelTypes.Valid;
                balanceHead.IsMainInvoice = false;
                balanceHead.BalanceSaveType = "0";

                //优惠金额
                if (!IsRebatePrint)
                {
                    balanceHead.FT.RebateCost = RebateCost;
                }

                result = inpatientFeeManager.UpdateFeeInfoBalanced(patientInfo.ID, balanceNO, sysdate, invoiceNO, true);
                if (result == 0)
                {
                    this.RollBack();
                    this.err = "可能存在并发操作导致更新费用失败！";
                    return -1;
                }

                if (result < 1)
                {
                    this.RollBack();
                    this.err = inpatientFeeManager.Err;
                    return -1;
                }

                if (inpatientFeeManager.UpdateFeeItemListBalanced(patientInfo.ID, balanceNO, invoiceNO, true) == -1)
                {
                    this.RollBack();
                    this.err = inpatientFeeManager.Err;
                    return -1;
                }


                //插入结算头表
                balanceHead.PrintedInvoiceNO = realInvoiceNO;
                FS.HISFC.Models.Base.PactInfo pact = patientInfo.Pact.Clone();
                patientInfo.Pact.ID = "1";
                patientInfo.Pact.Name = "自费";
                patientInfo.Pact.PayKind.ID = "01";
                patientInfo.Pact.PayKind.Name = "自费";
                if (this.inpatientFeeManager.InsertBalance(patientInfo, balanceHead) < 1)
                {
                    this.RollBack();
                    this.err = this.inpatientFeeManager.Err;
                    patientInfo.Pact = pact;
                    return -1;
                }
                patientInfo.Pact = pact;

                if (feeIntegrate.InsertInvoiceExtend(invoiceNO, "I", realInvoiceNO, "00") < 1)
                {
                    this.RollBack();
                    this.err = this.inpatientFeeManager.Err;
                    return -1;
                }

                if (feeIntegrate.UseInvoiceNO("I", 1, ref invoiceNO, ref realInvoiceNO, ref this.err) != 1)
                {
                    this.RollBack();
                    return -1;
                }

                listBalance.Add(balanceHead);
            }
            else
            {
                result = inpatientFeeManager.UpdateFeeInfoBalanced(patientInfo.ID, balanceNO, sysdate, invoiceNO, true);

                if (result < 0)
                {
                    this.RollBack();
                    this.err = inpatientFeeManager.Err;
                    return -1;
                }

                if (inpatientFeeManager.UpdateFeeItemListBalanced(patientInfo.ID, balanceNO, invoiceNO, true) == -1)
                {
                    this.RollBack();
                    this.err = inpatientFeeManager.Err;
                    return -1;
                }
            }

            #endregion

            #region 优惠发票

            if (alBalanceListRebate.Count > 0)
            {
                if (feeIntegrate.GetInvoiceNO("I", ref invoiceNO, ref realInvoiceNO, ref errText) < 1 || string.IsNullOrEmpty(invoiceNO) || string.IsNullOrEmpty(realInvoiceNO))
                {
                    this.RollBack();
                    this.err = "获取发票失败，" + feeIntegrate.Err;
                    return -1;
                }
                decimal TotCost = 0m;
                decimal OwnCost = 0m;
                decimal PayCost = 0m;
                decimal PubCost = 0m;
                decimal RebateCost = 0M;
                foreach (FS.HISFC.Models.Fee.Inpatient.BalanceList balance in alBalanceListRebate)
                {
                    TotCost += balance.BalanceBase.FT.TotCost;
                    OwnCost += balance.BalanceBase.FT.OwnCost;
                    PayCost += balance.BalanceBase.FT.PayCost;
                    PubCost += balance.BalanceBase.FT.PubCost;
                    RebateCost += balance.BalanceBase.FT.RebateCost;
                    balance.BalanceBase.Invoice.ID = invoiceNO;
                    if (this.inpatientFeeManager.InsertBalanceList(patientInfo, balance) < 1)
                    {
                        this.RollBack();
                        this.err = this.inpatientFeeManager.Err;
                        return -1;
                    }
                }
                //头表实体赋值
                FS.HISFC.Models.Fee.Inpatient.Balance BalanceRebate = (FS.HISFC.Models.Fee.Inpatient.Balance)alBalanceListRebate[0].BalanceBase.Clone();
                BalanceRebate.FT.TotCost = TotCost;
                BalanceRebate.FT.OwnCost = OwnCost;
                BalanceRebate.FT.PayCost = PayCost;
                BalanceRebate.FT.PubCost = PubCost;
                BalanceRebate.FT.RebateCost = RebateCost;
                BalanceRebate.BeginTime = inDate;
                BalanceRebate.EndTime = sysdate;
                BalanceRebate.CancelType = FS.HISFC.Models.Base.CancelTypes.Valid;
                BalanceRebate.IsMainInvoice = false;
                BalanceRebate.BalanceSaveType = "0";
                //插入结算头表
                BalanceRebate.PrintedInvoiceNO = realInvoiceNO;
                if (this.inpatientFeeManager.InsertBalance(patientInfo, BalanceRebate) < 1)
                {
                    this.RollBack();
                    this.err = this.inpatientFeeManager.Err;
                    return -1;
                }

                if (feeIntegrate.InsertInvoiceExtend(invoiceNO, "I", realInvoiceNO, "00") < 1)
                {
                    this.RollBack();
                    this.err = feeIntegrate.Err;
                    return -1;
                }

                if (feeIntegrate.UseInvoiceNO("I", 1, ref invoiceNO, ref realInvoiceNO, ref this.err) != 1)
                {
                    this.RollBack();
                    return -1;
                }
                listBalance.Add(BalanceRebate);
            }

            #endregion

            #region 欠费发票

            if (alArrearBalanceList.Count > 0)
            {
                if (feeIntegrate.GetInvoiceNO(this.inpatientFeeManager.Operator as FS.HISFC.Models.Base.Employee, "I", true, ref invoiceNO, ref realInvoiceNO, ref this.err) != 1)
                {
                    this.RollBack();
                    return -1;
                }

                decimal TotCost = 0m;
                decimal OwnCost = 0m;
                decimal PayCost = 0m;
                decimal PubCost = 0m;
                decimal RebateCost = 0M;
                foreach (FS.HISFC.Models.Fee.Inpatient.BalanceList balance in alArrearBalanceList)
                {
                    TotCost += balance.BalanceBase.FT.TotCost;
                    OwnCost += balance.BalanceBase.FT.OwnCost;
                    PayCost += balance.BalanceBase.FT.PayCost;
                    PubCost += balance.BalanceBase.FT.PubCost;
                    RebateCost += balance.BalanceBase.FT.RebateCost;
                    ((FS.HISFC.Models.Fee.Inpatient.Balance)balance.BalanceBase).IsTempInvoice = true;
                    balance.BalanceBase.Invoice.ID = invoiceNO;
                    if (this.inpatientFeeManager.InsertBalanceList(patientInfo, balance) < 1)
                    {
                        this.RollBack();
                        this.err = this.inpatientFeeManager.Err;
                        return -1;
                    }

                }

                FS.HISFC.Models.Fee.Inpatient.Balance balanceHead = ((FS.HISFC.Models.Fee.Inpatient.Balance)alArrearBalanceList[0].BalanceBase).Clone();

                balanceHead.Invoice.ID = invoiceNO;
                balanceHead.BeginTime = inDate;//患者入院时间
                balanceHead.EndTime = sysdate;//结算时间
                balanceHead.FT.TotCost = TotCost;
                balanceHead.FT.OwnCost = OwnCost;
                balanceHead.FT.PayCost = PayCost;
                balanceHead.FT.PubCost = PubCost;
                balanceHead.IsTempInvoice = true;
                balanceHead.CancelType = FS.HISFC.Models.Base.CancelTypes.Valid;
                balanceHead.BalanceSaveType = "0";


                //如果减免金额大于自费金额，公费不能，说明分了发票
                if (TotDerateCost > OwnCost)
                {
                    balanceHead.FT.DerateCost = OwnCost;
                    TotDerateCost = TotDerateCost - OwnCost;
                }
                else
                {
                    balanceHead.FT.DerateCost = TotDerateCost;
                }

                //优惠金额
                balanceHead.FT.RebateCost = RebateCost;
                balanceHead.IsMainInvoice = mainInvoice;
                if (mainInvoice)
                {
                    mainInvoice = false;
                    balanceHead.FT.PrepayCost = TotPrepayCost;//预交金额
                    balanceHead.FT.TransferPrepayCost = 0.00M;//欠费金额

                    if (TotShouldPay == TotPrepayCost)
                    {
                        balanceHead.FT.SupplyCost = 0M;//应收金额
                        balanceHead.FT.ReturnCost = 0M;//应返金额
                    }
                    else if (TotShouldPay > TotPrepayCost)
                    {
                        balanceHead.FT.SupplyCost = TotShouldPay - TotPrepayCost;//应收金额
                        balanceHead.FT.ReturnCost = 0M;//应返金额
                    }
                    else
                    {
                        balanceHead.FT.SupplyCost = 0M;//应收金额
                        balanceHead.FT.ReturnCost = TotPrepayCost - TotShouldPay;//应返金额
                    }


                    balanceHead.FT.SupplyCost = balanceHead.FT.SupplyCost - arrearFeeCost;

                    if (balanceHead.BalanceType.ID.ToString() == FS.HISFC.Models.Fee.EnumBalanceType.Q.ToString())
                    {
                        balanceHead.FT.ArrearCost = arrearFeeCost;
                    }
                }
                else
                {
                    if (balanceHead.BalanceType.ID.ToString() == FS.HISFC.Models.Fee.EnumBalanceType.Q.ToString())
                    {
                        balanceHead.FT.ArrearCost = arrearFeeCost;
                    }
                }

                //插入结算头表
                balanceHead.PrintedInvoiceNO = realInvoiceNO;
                if (this.inpatientFeeManager.InsertBalance(patientInfo, balanceHead) < 1)
                {
                    this.RollBack();
                    this.err = this.inpatientFeeManager.Err;
                    return -1;
                }
            }

            #endregion

            #endregion

            #region 患者费用处理

            patientInfo.SIMainInfo.BalNo = balNo;
            patientInfo.SIMainInfo.InvoiceNo = invoiceNO;
            patientInfo.PVisit.InState.ID = FS.HISFC.Models.Base.EnumInState.O;//出院结算

            FS.HISFC.Models.Fee.Inpatient.FeeInfo feeInfo = new FS.HISFC.Models.Fee.Inpatient.FeeInfo();
            feeInfo.FT.PubCost = TotPubCost;
            feeInfo.FT.OwnCost = TotOwnCost;
            feeInfo.FT.PayCost = TotPayCost;
            feeInfo.FT.RebateCost = TotRebateCost;
            feeInfo.FT.TotCost = TotBalanceCost;
            feeInfo.FT.PrepayCost = TotPrepayCost;

            //医保处理一下 2012年7月24日16:57:22
            if (patientInfo.Pact.PayKind.ID == "02" && patientInfo.SIMainInfo.TotCost != 0)
            {
                feeInfo.FT.PubCost = patientInfo.SIMainInfo.PubCost;
                feeInfo.FT.OwnCost = patientInfo.SIMainInfo.OwnCost;
                feeInfo.FT.PayCost = patientInfo.SIMainInfo.PayCost;
                //feeInfo.FT.RebateCost = TotRebateCost;
                //feeInfo.FT.TotCost = TotBalanceCost;
                //feeInfo.FT.PrepayCost = TotPrepayCost;
            }

            if (feeInfo.FT.TotCost != feeInfo.FT.OwnCost + feeInfo.FT.PayCost + feeInfo.FT.PubCost + feeInfo.FT.RebateCost)
            {
                this.RollBack();
                this.err = "总费用与分支费用不相等，请检查！";
                return -1;
            }

            if (this.inpatientFeeManager.UpdateInMainInfoBalanced(patientInfo, sysdate, balanceNO, feeInfo.FT) <= 0)
            {
                this.RollBack();
                this.err = this.inpatientFeeManager.Err;
                return -1;
            }

            //开账
            if (this.inpatientFeeManager.OpenAccount(patientInfo.ID) < 0)
            {
                this.RollBack();
                this.err = this.inpatientFeeManager.Err;
                return -1;
            }

            string motherPayAllFee = controlParamMgr.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.SysConst.Use_Mother_PayAllFee, false, "0");
            if (motherPayAllFee == "1")//婴儿的费用收在妈妈的身上 
            {
                ArrayList babyList = radtManager.QueryBabies(patientInfo.ID);
                if (babyList != null && babyList.Count > 0)
                {
                    foreach (FS.HISFC.Models.RADT.PatientInfo baby in babyList)
                    {
                        FS.HISFC.Models.RADT.PatientInfo pTemp = radtManager.GetPatientInfo(baby.ID);
                        if (pTemp != null && !string.IsNullOrEmpty(pTemp.ID))
                        {
                            pTemp.PVisit = patientInfo.PVisit.Clone();

                            if (this.inpatientFeeManager.UpdateInMainInfoBalanced(pTemp, sysdate, balanceNO, new FS.HISFC.Models.Base.FT()) < 0)
                            {
                                this.RollBack();
                                this.err = this.inpatientFeeManager.Err;
                                return -1;
                            }
                        }
                    }
                }
            }

            #endregion

            #region 结算变更记录

            FS.FrameWork.Models.NeuObject oldObj = new FS.FrameWork.Models.NeuObject();
            FS.FrameWork.Models.NeuObject newObj = new FS.FrameWork.Models.NeuObject();
            newObj.ID = patientInfo.SIMainInfo.BalNo;
            newObj.Name = "结算序号";
            if (radtManager.SaveShiftData(patientInfo.ID, FS.HISFC.Models.Base.EnumShiftType.BA, "出院结算", oldObj, newObj) == -1)
            {
                this.RollBack();
                this.err = radtManager.Err;
                return -1;
            }

            #endregion

            #region 医保结算处理
            FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy medcareInterfaceProxy = new FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy();
            long returnValue = medcareInterfaceProxy.SetPactCode(patientInfo.Pact.ID);
            if (returnValue != 1)
            {
                this.RollBack();
                this.err = medcareInterfaceProxy.ErrMsg;
                return -1;
            }
            ////待遇接口处理－－－－－－－－－－－－－－－－－－－－－－－－－－－－
            medcareInterfaceProxy.SetTrans(this.Trans);
            medcareInterfaceProxy.Connect();

            returnValue = medcareInterfaceProxy.GetRegInfoInpatient(patientInfo);
            if (returnValue != 1)
            {
                this.RollBack();
                this.err = medcareInterfaceProxy.ErrMsg;
                return -1;
            }

            ArrayList alFeeItemList = new ArrayList(feeInfoList);
            returnValue = medcareInterfaceProxy.BalanceInpatient(patientInfo, ref alFeeItemList);
            if (returnValue != 1)
            {
                this.RollBack();
                this.err = medcareInterfaceProxy.ErrMsg;
                return -1;
            }

            returnValue = medcareInterfaceProxy.Disconnect();
            if (returnValue != 1)
            {
                this.RollBack();
                this.err = medcareInterfaceProxy.ErrMsg;
                return -1;
            }

            #endregion

            #region 更新托收单
            if (patientInfo.Pact.PayKind.ID == "03")
            {   //更新结算发票号
                if (this.Updategfhz(patientInfo, invoiceNO, "0") == -1)
                {
                    this.RollBack();
                    this.err = "更新托收单出错!" + this.inpatientFeeManager.Err;
                    return -1;

                }
            }
            #endregion

            //发送消息
            if (InterfaceManager.GetIADT() != null)
            {
                if (InterfaceManager.GetIADT().Balance(patientInfo, true) < 0)
                {
                    this.RollBack();
                    this.err = InterfaceManager.GetIADT().Err;
                    return -1;
                }
            }

            this.Commit();
            return 1;
        }

        #endregion

        #region 项目明细结算

        /// <summary>
        /// 项目明细结算
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="balanceType"></param>
        /// <param name="feeItemList"></param>
        /// <param name="prepayList"></param>
        /// <param name="balancePayList"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="splitInvoiceCost"></param>
        /// <param name="invoiceNO"></param>
        /// <param name="realInvoiceNO"></param>
        /// <returns></returns>
        public int ItemBalance(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.HISFC.Models.Base.EBlanceType balanceType,
            List<FS.HISFC.Models.Fee.Inpatient.FeeInfo> feeInfoList,
            List<FS.HISFC.Models.Fee.Inpatient.Prepay> prepayList,
            List<FS.HISFC.Models.Fee.Inpatient.BalancePay> balancePayList,
            decimal splitInvoiceCost,
            ref List<FS.HISFC.Models.Fee.Inpatient.Balance> listBalance)
        {
            if (patientInfo == null || string.IsNullOrEmpty(patientInfo.ID))
            {
                this.err = "结算失败，患者信息为空！";
                return -1;
            }

            this.BeginTransaction();

            #region 患者信息验证
            RADT radtManager = new RADT();
            FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParamMgr = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
            radtManager.SetTrans(this.Trans);
            controlParamMgr.SetTrans(this.Trans);
            //获取时间
            DateTime sysdate = inpatientFeeManager.GetDateTimeFromSysDateTime();
            //验证患者信息
            FS.HISFC.Models.RADT.PatientInfo patientInfoTemp = radtManager.GetPatientInfo(patientInfo.ID);
            if (patientInfoTemp == null || string.IsNullOrEmpty(patientInfoTemp.ID))
            {
                this.RollBack();
                this.err = "患者信息为空，" + radtManager.Err;
                return -1;
            }

            //已经出院的返回
            if (patientInfo.PVisit.InState.ID.ToString().Equals(FS.HISFC.Models.Base.EnumInState.O))
            {
                this.RollBack();
                this.err = "患者已经出院结算";
                return -1;
            }

            #endregion

            #region 发票号和结算号

            FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();
            feeIntegrate.SetTrans(this.Trans);
            inpatientFeeManager.SetTrans(this.Trans);
            string invoiceNO = string.Empty;
            string realInvoiceNO = string.Empty;
            string errText = "";

            if (feeIntegrate.GetInvoiceNO("I", ref invoiceNO, ref realInvoiceNO, ref errText) < 1 || string.IsNullOrEmpty(invoiceNO) || string.IsNullOrEmpty(realInvoiceNO))
            {
                this.RollBack();
                this.err = "获取发票失败，" + feeIntegrate.Err;
                return -1;
            }

            //调业务层获取结算次数
            int balanceNO = 0;
            string balNo = inpatientFeeManager.GetNewBalanceNO(patientInfo.ID);
            if (balNo == null || balNo.Length == 0)
            {
                this.RollBack();
                this.err = "获取结算序号出错，" + feeIntegrate.Err;
                return -1;
            }
            else
            {
                balanceNO = int.Parse(balNo);
            }

            #endregion

            #region 处理预交金信息

            decimal TotPrepayCost = 0.00M;
            if (prepayList != null && prepayList.Count > 0)
            {
                foreach (FS.HISFC.Models.Fee.Inpatient.Prepay prepay in prepayList)
                {
                    prepay.BalanceNO = balanceNO;
                    prepay.Invoice.ID = invoiceNO;
                    prepay.BalanceOper.ID = this.inpatientFeeManager.Operator.ID;
                    prepay.BalanceOper.OperTime = sysdate;

                    TotPrepayCost += prepay.FT.PrepayCost;//预交金金额

                    //转入预交金处理（目前缺失更新单条的）
                    if (prepay.Name == "转入预交金")
                    {
                        if (inpatientFeeManager.UpdateChangePrepayBalanced(patientInfo.ID, balanceNO) == -1)
                        {
                            this.RollBack();
                            this.err = "结算失败，原因：" + inpatientFeeManager.Err;
                            return -1;
                        }
                    }
                    else
                    {
                        //更新预交发票为结算状态
                        if (inpatientFeeManager.UpdatePrepayBalanced(patientInfo, prepay) <= 0)
                        {
                            this.RollBack();
                            this.err = "结算失败，原因：" + inpatientFeeManager.Err;
                            return -1;
                        }
                    }
                }
            }

            #endregion

            #region 处理减免信息

            //if (balanceType == FS.HISFC.Models.Base.EBlanceType.Out || balanceType == FS.HISFC.Models.Base.EBlanceType.Owe)
            //{
            //    if (inpatientFeeManager.UpdateDerateBalanced(patientInfo.ID, balanceNO, invoiceNO) == -1)
            //    {
            //        this.RollBack();
            //        this.err = "结算失败，原因：" + inpatientFeeManager.Err;
            //        return -1;
            //    }
            //}

            #endregion

            #region 处理费用信息

            //记录需要插入的数据
            List<FS.HISFC.Models.Fee.Inpatient.FeeInfo> needInsertFeeInfoList = new List<FS.HISFC.Models.Fee.Inpatient.FeeInfo>();

            //出院结算更新费用
            //判断并发操作
            decimal TotOwnCost = 0.00M;
            decimal TotPubCost = 0.00M;
            decimal TotPayCost = 0.00M;
            decimal TotBalanceCost = 0.00M;
            decimal TotRebateCost = 0.00M;//获得费用总额
            foreach (FS.HISFC.Models.Fee.Inpatient.FeeInfo feeInfoHead in feeInfoList)
            {
                //if (feeInfoHead.FT.PubCost != 0)
                //{
                //    this.RollBack();
                //    this.err = inpatientFeeManager.Err;
                //    return -1;
                //}
                TotBalanceCost += feeInfoHead.FT.BalancedCost;
                TotOwnCost += feeInfoHead.FT.OwnCost;
                TotPubCost += feeInfoHead.FT.PubCost;
                TotPayCost += feeInfoHead.FT.PayCost;
                TotRebateCost += feeInfoHead.FT.RebateCost;


                if (feeInfoHead.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                {
                    //根据药品代码获取药品明细
                    ArrayList alDrug = inpatientFeeManager.QureyMedicineByDrugCode(patientInfo.ID, feeInfoHead.ID, "0");
                    for (int i = 0; i <= alDrug.Count - 1; ++i)
                    {
                        FS.HISFC.Models.Fee.Inpatient.FeeItemList feeitem = alDrug[i] as FS.HISFC.Models.Fee.Inpatient.FeeItemList;
                        string transType = this.GetTansType(feeitem.TransType);

                        decimal decTotcost = FS.FrameWork.Function.NConvert.ToDecimal(inpatientFeeManager.GetSumTotByRecipeNoAndSeq(feeitem.RecipeNO, feeitem.SequenceNO.ToString(), 0));


                        //更新药品费用结算状态
                        if (inpatientFeeManager.UpdateMedItemListByRecipeNoAndSeqNo(patientInfo.ID, balanceNO, invoiceNO, feeitem.RecipeNO, feeitem.SequenceNO) < 0)
                        {
                            this.RollBack();
                            this.err = inpatientFeeManager.Err;
                            return -1;
                        }
                        //获取费用汇总信息
                        ArrayList alFeeInfo = inpatientFeeManager.QueryFeeInfoByRecipeNo(patientInfo.ID, feeitem.RecipeNO, feeitem.Item.MinFee.ID, feeitem.ExecOper.Dept.ID, 0, "0");
                        if (alFeeInfo == null || alFeeInfo.Count == 0)
                        {
                            //身份变更数据，状态已经更新
                            alFeeInfo = inpatientFeeManager.QueryFeeInfoByRecipeNo(patientInfo.ID, feeitem.RecipeNO, feeitem.Item.MinFee.ID, feeitem.ExecOper.Dept.ID, balanceNO, "1");
                            if (alFeeInfo == null || alFeeInfo.Count == 0)
                            {
                                this.RollBack();
                                this.err = "根据费用项目代码获取相应的处方费用汇总信息失败！";
                                return -1;
                            }
                            continue;
                        }

                        //是否拆分费用汇总
                        bool isSlitFeeInfo = false;
                        FS.HISFC.Models.Base.FT tempFT = new FS.HISFC.Models.Base.FT();
                        FS.HISFC.Models.Fee.Inpatient.FeeInfo tempFeeInfo = null;
                        FS.HISFC.Models.Fee.Inpatient.FeeInfo FeeInfoObj = alFeeInfo[0] as FS.HISFC.Models.Fee.Inpatient.FeeInfo;
                        tempFeeInfo = FeeInfoObj.Clone();
                        tempFeeInfo.FT = tempFT;

                        foreach (FS.HISFC.Models.Fee.Inpatient.FeeInfo fObj in alFeeInfo)
                        {
                            if (fObj.FT.TotCost == 0 || fObj.FT.TotCost == feeitem.FT.TotCost)
                            {

                                continue;
                                //if (fObj.BalanceNO == balanceNO)
                                //{
                                //}
                                //else
                                //{
                                //    //身份变更的原费用记录直接更新/费用汇总信息只有一条费用明细的直接更新
                                //    int result = inpatientFeeManager.UpdateFeeInfoByRecipeNo(patientInfo.ID, balanceNO, invoiceNO, sysdate.ToString(), fObj.RecipeNO, fObj.Item.MinFee.ID, fObj.ExecOper.Dept.ID, fObj.BalanceNO);
                                //    if (result == -1)
                                //    {
                                //        this.RollBack();
                                //        this.err = "更新费用汇总状态信息失败！";
                                //        return -1;
                                //    }
                                //}
                            }
                            else if (FS.FrameWork.Function.NConvert.ToDecimal(decTotcost) == 0)
                            {
                                //退费记录，不做处理
                                continue;
                            }
                            else if (feeitem.FT.TotCost < fObj.FT.TotCost)
                            {
                                isSlitFeeInfo = true;
                                //费用汇总信息由多条费用明细的汇总而来
                                //首先更新汇总费用信息
                                if (inpatientFeeManager.UpdateFeeInfoForFT(patientInfo.ID, feeitem.RecipeNO, feeitem.Item.MinFee.ID, feeitem.ExecOper.Dept.ID, fObj.BalanceNO, "0", feeitem.FT) < 0)
                                {
                                    this.RollBack();
                                    this.err = "更新费用汇总费用信息失败！";
                                    return -1;
                                }

                                tempFeeInfo.FT.TotCost += feeitem.FT.TotCost;
                                tempFeeInfo.FT.PubCost += feeitem.FT.PubCost;
                                tempFeeInfo.FT.PayCost += feeitem.FT.PayCost;
                                tempFeeInfo.FT.OwnCost += feeitem.FT.OwnCost;
                                tempFeeInfo.FT.RebateCost += feeitem.FT.RebateCost;
                                tempFeeInfo.FT.DefTotCost += feeitem.FT.DefTotCost;

                            }

                        }

                        if (isSlitFeeInfo && tempFeeInfo.FT.TotCost != 0)
                        {
                            tempFeeInfo.Invoice.ID = invoiceNO;
                            tempFeeInfo.BalanceNO = balanceNO;
                            tempFeeInfo.BalanceState = "1";
                            tempFeeInfo.BalanceOper.ID = inpatientFeeManager.Operator.ID;
                            tempFeeInfo.BalanceOper.OperTime = inpatientFeeManager.GetDateTimeFromSysDateTime();

                            //拆分标志 项目结算的拆分数据
                            tempFeeInfo.ExtFlag1 = "S";
                            //先记录下来
                            needInsertFeeInfoList.Add(tempFeeInfo);
                            //if (-1 == inpatientFeeManager.InsertFeeInfo(patientInfo, tempFeeInfo))
                            //{
                            //    this.RollBack();
                            //    this.err = "插入已结费用汇总费用信息失败！";
                            //    return -1;
                            //}
                        }

                    }


                }
                else
                {
                    //根据非药品代码获取非药品明细
                    ArrayList alUnDrug = inpatientFeeManager.QureyItemListByItemCode(patientInfo.ID, feeInfoHead.ID, "0");
                    for (int i = 0; i <= alUnDrug.Count - 1; ++i)
                    {
                        FS.HISFC.Models.Fee.Inpatient.FeeItemList feeitem = alUnDrug[i] as FS.HISFC.Models.Fee.Inpatient.FeeItemList;
                        string transType = this.GetTansType(feeitem.TransType);

                        decimal decTotcost = FS.FrameWork.Function.NConvert.ToDecimal(inpatientFeeManager.GetSumTotByRecipeNoAndSeq(feeitem.RecipeNO, feeitem.SequenceNO.ToString(), 1));


                        //更新非药品费用结算状态
                        if (inpatientFeeManager.UpdateItemListByRecipeNoAndSeqNo(patientInfo.ID, balanceNO, invoiceNO, feeitem.RecipeNO, feeitem.SequenceNO) <= 0)
                        {
                            this.RollBack();
                            this.err = inpatientFeeManager.Err;
                            return -1;
                        }
                        //获取费用汇总信息
                        ArrayList alFeeInfo = inpatientFeeManager.QueryFeeInfoByRecipeNo(patientInfo.ID, feeitem.RecipeNO, feeitem.Item.MinFee.ID, feeitem.ExecOper.Dept.ID, 0, "0");
                        if (alFeeInfo == null || alFeeInfo.Count == 0)
                        {
                            //身份变更数据，状态已经更新
                            alFeeInfo = inpatientFeeManager.QueryFeeInfoByRecipeNo(patientInfo.ID, feeitem.RecipeNO, feeitem.Item.MinFee.ID, feeitem.ExecOper.Dept.ID, balanceNO, "1");
                            if (alFeeInfo == null || alFeeInfo.Count == 0)
                            {
                                this.RollBack();
                                this.err = "根据费用项目代码获取相应的处方费用汇总信息失败！";
                                return -1;
                            }
                            continue;

                        }

                        //是否拆分费用汇总
                        bool isSlitFeeInfo = false;
                        FS.HISFC.Models.Base.FT tempFT = new FS.HISFC.Models.Base.FT();
                        FS.HISFC.Models.Fee.Inpatient.FeeInfo tempFeeInfo = null;
                        FS.HISFC.Models.Fee.Inpatient.FeeInfo FeeInfoObj = alFeeInfo[0] as FS.HISFC.Models.Fee.Inpatient.FeeInfo;
                        tempFeeInfo = FeeInfoObj.Clone();
                        tempFeeInfo.FT = tempFT;

                        foreach (FS.HISFC.Models.Fee.Inpatient.FeeInfo fObj in alFeeInfo)
                        {
                            if (fObj.FT.TotCost == 0 || fObj.FT.TotCost == feeitem.FT.TotCost)
                            {
                                continue;
                                //if (fObj.BalanceNO == balanceNO)
                                //{
                                //}
                                //else
                                //{
                                //    //身份变更的原费用记录直接更新/费用汇总信息只有一条费用明细的直接更新
                                //    int result = inpatientFeeManager.UpdateFeeInfoByRecipeNo(patientInfo.ID, balanceNO, invoiceNO, sysdate.ToString(), fObj.RecipeNO, fObj.Item.MinFee.ID, fObj.ExecOper.Dept.ID, fObj.BalanceNO);
                                //    if (result == -1)
                                //    {
                                //        this.RollBack();
                                //        this.err = "更新费用汇总状态信息失败！";
                                //        return -1;
                                //    }
                                //}
                            }
                            else if (FS.FrameWork.Function.NConvert.ToDecimal(decTotcost) == 0)
                            {
                                //退费记录，不做处理
                                continue;
                            }
                            else if (feeitem.FT.TotCost < fObj.FT.TotCost)
                            {
                                isSlitFeeInfo = true;
                                //费用汇总信息由多条费用明细的汇总而来
                                //首先更新汇总费用信息
                                if (inpatientFeeManager.UpdateFeeInfoForFT(patientInfo.ID, feeitem.RecipeNO, feeitem.Item.MinFee.ID, feeitem.ExecOper.Dept.ID, fObj.BalanceNO, "0", feeitem.FT) < 0)
                                {
                                    this.RollBack();
                                    this.err = "更新费用汇总费用信息失败！";
                                    return -1;
                                }

                                tempFeeInfo.FT.TotCost += feeitem.FT.TotCost;
                                tempFeeInfo.FT.PubCost += feeitem.FT.PubCost;
                                tempFeeInfo.FT.PayCost += feeitem.FT.PayCost;
                                tempFeeInfo.FT.OwnCost += feeitem.FT.OwnCost;
                                tempFeeInfo.FT.RebateCost += feeitem.FT.RebateCost;
                                tempFeeInfo.FT.DefTotCost += feeitem.FT.DefTotCost;

                            }

                        }

                        if (isSlitFeeInfo && tempFeeInfo.FT.TotCost != 0)
                        {
                            tempFeeInfo.Invoice.ID = invoiceNO;
                            tempFeeInfo.BalanceNO = balanceNO;
                            tempFeeInfo.BalanceState = "1";
                            tempFeeInfo.BalanceOper.ID = inpatientFeeManager.Operator.ID;
                            tempFeeInfo.BalanceOper.OperTime = inpatientFeeManager.GetDateTimeFromSysDateTime();

                            //拆分标志 项目结算的拆分数据
                            tempFeeInfo.ExtFlag1 = "S";
                            //先记录下来
                            needInsertFeeInfoList.Add(tempFeeInfo);

                            //if (-1 == inpatientFeeManager.InsertFeeInfo(patientInfo, tempFeeInfo))
                            //{
                            //    this.RollBack();
                            //    this.err = "插入已结费用汇总费用信息失败！";
                            //    return -1;
                            //}
                        }

                    }

                }

            }

            Dictionary<string, FS.HISFC.Models.Fee.Inpatient.FeeInfo> dictionaryFeeInfo = new Dictionary<string, FS.HISFC.Models.Fee.Inpatient.FeeInfo>();

            foreach (FS.HISFC.Models.Fee.Inpatient.FeeInfo needFeeInfo in needInsertFeeInfoList)
            {
                string key = needFeeInfo.RecipeNO + needFeeInfo.Item.MinFee.ID + needFeeInfo.ExecOper.Dept.ID + needFeeInfo.BalanceNO;
                if (dictionaryFeeInfo.ContainsKey(key))
                {
                    dictionaryFeeInfo[key].FT.TotCost += needFeeInfo.FT.TotCost;
                    dictionaryFeeInfo[key].FT.PubCost += needFeeInfo.FT.PubCost;
                    dictionaryFeeInfo[key].FT.PayCost += needFeeInfo.FT.PayCost;
                    dictionaryFeeInfo[key].FT.OwnCost += needFeeInfo.FT.OwnCost;
                    dictionaryFeeInfo[key].FT.RebateCost += needFeeInfo.FT.RebateCost;
                    dictionaryFeeInfo[key].FT.DefTotCost += needFeeInfo.FT.DefTotCost;
                }
                else
                {
                    dictionaryFeeInfo[key] = needFeeInfo;
                }
            }
            foreach (FS.HISFC.Models.Fee.Inpatient.FeeInfo needFeeInfo in dictionaryFeeInfo.Values)
            {
                if (-1 == inpatientFeeManager.InsertFeeInfo(patientInfo, needFeeInfo))
                {
                    this.RollBack();
                    this.err = "插入已结费用汇总费用信息失败！";
                    return -1;
                }
            }

            //更新转入费用
            if (inpatientFeeManager.UpdateAllChangeCostBalanced(patientInfo.ID, balanceNO) == -1)
            {
                this.RollBack();
                this.err = inpatientFeeManager.Err;
                return -1;
            }


            #endregion

            #region 处理支付方式
            decimal TotBalancePayCost = 0.00M;
            if (balancePayList != null && balancePayList.Count > 0)
            {
                foreach (FS.HISFC.Models.Fee.Inpatient.BalancePay balancePay in balancePayList)
                {
                    TotBalancePayCost += balancePay.FT.TotCost;

                    balancePay.Invoice.ID = invoiceNO;
                    balancePay.BalanceNO = balanceNO;
                    balancePay.BalanceOper.ID = inpatientFeeManager.Operator.ID;
                    balancePay.BalanceOper.OperTime = sysdate;
                    //添加记录
                    if (inpatientFeeManager.InsertBalancePay(balancePay) == -1)
                    {
                        this.RollBack();
                        this.err = inpatientFeeManager.Err;
                        return -1;
                    }
                }
            }

            //欠费金额
            decimal arrearFeeCost = TotBalanceCost - TotBalancePayCost;
            arrearFeeCost = arrearFeeCost > 0 ? arrearFeeCost : 0;

            //欠费金额等于结算金额-支付方式金额
            if (arrearFeeCost > 0 && balanceType == FS.HISFC.Models.Base.EBlanceType.Owe)
            {
                FS.HISFC.Models.Fee.Inpatient.BalancePay balancePay = new FS.HISFC.Models.Fee.Inpatient.BalancePay();
                balancePay.PayType.ID = "QF";
                balancePay.PayType.Name = "欠费";
                balancePay.FT.TotCost = arrearFeeCost;
                balancePay.TransKind.ID = "1";
                balancePay.TransType = FS.HISFC.Models.Base.TransTypes.Positive;
                balancePay.Qty = 1;
                balancePay.RetrunOrSupplyFlag = "1";//欠费
                balancePay.Invoice.ID = invoiceNO;
                balancePay.BalanceNO = balanceNO;
                balancePay.BalanceOper.ID = inpatientFeeManager.Operator.ID;
                balancePay.BalanceOper.OperTime = sysdate;
                //添加记录
                if (inpatientFeeManager.InsertBalancePay(balancePay) == -1)
                {
                    this.RollBack();
                    this.err = inpatientFeeManager.Err;
                    return -1;
                }
            }
            #endregion

            #region 处理发票信息

            string invoiceType = "ZY01";

            FS.HISFC.BizLogic.Fee.FeeCodeStat feeCodeStat = new FS.HISFC.BizLogic.Fee.FeeCodeStat();
            ArrayList alFeeState = feeCodeStat.QueryFeeCodeStatByReportCode(invoiceType);
            if (alFeeState == null)
            {
                this.RollBack();
                this.err = "结算失败，原因：" + feeCodeStat.Err;
                return -1;
            }

            #region 欠费发票

            List<FS.HISFC.Models.Fee.Inpatient.BalanceList> alArrearBalanceList = new List<FS.HISFC.Models.Fee.Inpatient.BalanceList>();
            if (arrearFeeCost > 0 && balanceType == FS.HISFC.Models.Base.EBlanceType.OweMid)
            {
                ArrayList alArrearManualFeeInfo = new ArrayList();
                decimal arrearFeeCostTemp = arrearFeeCost;

                decimal splitRate = arrearFeeCost / TotBalanceCost;
                if (splitRate > 1)
                {
                    this.RollBack();
                    this.err = "欠费金额大于结算金额";
                    return -1;
                }
                //循环处理分票明细及主发票明细
                for (int i = 0; i < feeInfoList.Count; i++)
                {
                    FS.HISFC.Models.Fee.Inpatient.FeeInfo InvoiceHeadInfo = feeInfoList[i];
                    FS.HISFC.Models.Fee.Inpatient.FeeInfo InvoiceManualInfo = InvoiceHeadInfo.Clone();

                    InvoiceManualInfo.FT.OwnCost = FS.FrameWork.Function.NConvert.ToDecimal(FS.FrameWork.Public.String.FormatNumberReturnString(InvoiceHeadInfo.FT.OwnCost * splitRate, 2));
                    InvoiceManualInfo.FT.PayCost = 0.00M;
                    InvoiceManualInfo.FT.PubCost = 0.00M;
                    InvoiceManualInfo.FT.RebateCost = 0.00M;

                    InvoiceManualInfo.FT.TotCost = InvoiceManualInfo.FT.OwnCost + InvoiceManualInfo.FT.PayCost + InvoiceManualInfo.FT.PubCost + InvoiceManualInfo.FT.RebateCost;

                    InvoiceHeadInfo.FT.TotCost = InvoiceHeadInfo.FT.TotCost - InvoiceManualInfo.FT.TotCost;
                    InvoiceHeadInfo.FT.OwnCost = InvoiceHeadInfo.FT.OwnCost - InvoiceManualInfo.FT.OwnCost;
                    InvoiceHeadInfo.FT.PayCost = InvoiceHeadInfo.FT.PayCost - InvoiceManualInfo.FT.PayCost;
                    InvoiceHeadInfo.FT.PubCost = InvoiceHeadInfo.FT.PubCost - InvoiceManualInfo.FT.PubCost;
                    InvoiceHeadInfo.FT.RebateCost = InvoiceHeadInfo.FT.RebateCost - InvoiceManualInfo.FT.RebateCost;
                    arrearFeeCostTemp -= InvoiceManualInfo.FT.TotCost;

                    alArrearManualFeeInfo.Add(InvoiceManualInfo);
                }

                //如果没有分完整，平小数位
                //如果没有分完整，平小数位
                if (Math.Abs(arrearFeeCostTemp) > 0)
                {
                    for (int i = 0; i < feeInfoList.Count; i++)
                    {
                        if (feeInfoList[i].FT.OwnCost + arrearFeeCostTemp > 0)
                        {
                            FS.HISFC.Models.Fee.Inpatient.FeeInfo InvoiceManualInfo = feeInfoList[i].Clone();
                            InvoiceManualInfo.FT.OwnCost = arrearFeeCostTemp;
                            InvoiceManualInfo.FT.PayCost = 0.00M;
                            InvoiceManualInfo.FT.PubCost = 0.00M;
                            InvoiceManualInfo.FT.RebateCost = 0.00M;
                            InvoiceManualInfo.FT.TotCost = arrearFeeCostTemp;
                            feeInfoList[i].FT.OwnCost -= arrearFeeCostTemp;
                            feeInfoList[i].FT.TotCost -= arrearFeeCostTemp;
                            alArrearManualFeeInfo.Add(InvoiceManualInfo);
                            break;
                        }
                    }
                }

                if (this.FeeInfoTransFeeStat(patientInfo, balanceType, alArrearManualFeeInfo, alArrearBalanceList, sysdate, balanceNO, "", alFeeState) == -1)
                {
                    this.RollBack();
                    return -1;
                }

            }

            #endregion

            #region 减免发票

            //FS.HISFC.BizLogic.Fee.Derate feeDerate = new FS.HISFC.BizLogic.Fee.Derate();
            //feeDerate.SetTrans(this.Trans);
            //单病种减免时不允许其他减免
            //bool IsInputDerateFee = FS.FrameWork.Function.NConvert.ToBoolean(controlParm.QueryControlerInfo("100009"));
            //ArrayList alInvoiceDerateFeeInfo = null;
            //if (IsInputDerateFee)
            //{
            //    alInvoiceDerateFeeInfo = feeDerate.GetFeeCodeAndDerateCost(patientInfo.ID, derateFeeCost, totOwnCost);
            //    if (alInvoiceDerateFeeInfo == null)
            //    {
            //        this.RollBack();
            //        this.err = "结算失败，原因：分配减免金额失败，" + feeDerate.Err;
            //        return -1;
            //    }
            //    //循环插入减免记录表
            //    for (int i = 0; i < alInvoiceDerateFeeInfo.Count; i++)
            //    {
            //        FS.HISFC.Models.Fee.Rate rate = new FS.HISFC.Models.Fee.Rate();
            //        FS.HISFC.Models.Fee.Inpatient.FeeInfo f = (FS.HISFC.Models.Fee.Inpatient.FeeInfo)alInvoiceDerateFeeInfo[i];
            //        rate.derate_Cost = f.FT.TotCost;
            //        rate.FeeCode = f.Item.MinFee.ID;
            //        rate.clinicNo = patientInfo.ID;
            //        rate.derateKind = "1";
            //        rate.derate_cause = "结算减免";
            //        rate.deptCode = patientInfo.PVisit.PatientLocation.Dept.ID;
            //        rate.balanceState = "1";
            //        rate.BalanceNo = balanceNO;
            //        rate.invoiceNo = invoiceNO;

            //        if (feeDerate.InsertDerate(rate) == -1)
            //        {
            //            this.RollBack();
            //            this.err = "结算失败，原因：" + feeDerate.Err;
            //            return -1;
            //        }
            //    }

            //}
            //else
            //{
            //    alInvoiceDerateFeeInfo = feeDerate.GetFeeCodeAndDerateCost(patientInfo.ID, balNo);
            //    if (alInvoiceDerateFeeInfo == null)
            //    {
            //        this.RollBack();
            //        this.err = "结算失败，原因：提取减免费用信息失败，" + feeDerate.Err;
            //        return -1;
            //    }
            //}

            //List<FS.HISFC.Models.Fee.Inpatient.BalanceList> alBalanceListDerate = new List<FS.HISFC.Models.Fee.Inpatient.BalanceList>();
            //bool IsDeratePrint = FS.FrameWork.Function.NConvert.ToBoolean(controlParm.QueryControlerInfo("100005"));
            ////判断减免是否单独打印发票---如果单独打印将原有的owncost和totcost-减免cost
            //if (IsDeratePrint && (balanceType == FS.HISFC.Models.Base.EBlanceType.Out || balanceType == FS.HISFC.Models.Base.EBlanceType.Owe) && alInvoiceDerateFeeInfo.Count > 0)
            //{
            //    for (int i = 0; i < alInvoiceDerateFeeInfo.Count; i++)
            //    {
            //        FS.HISFC.Models.Fee.Inpatient.FeeInfo feeinfoDerate = (FS.HISFC.Models.Fee.Inpatient.FeeInfo)alInvoiceDerateFeeInfo[i];
            //        for (int j = 0; j < feeInfoList.Count; j++)
            //        {
            //            FS.HISFC.Models.Fee.Inpatient.FeeInfo feeinfoHead = feeInfoList[j];
            //            if (feeinfoHead.Item.MinFee.ID == feeinfoDerate.Item.MinFee.ID)
            //            {
            //                if (feeinfoHead.FT.OwnCost < feeinfoDerate.FT.OwnCost)
            //                {
            //                    this.RollBack();
            //                    this.err = "费用代码为" + feeinfoDerate.Item.MinFee.ID + "的减免费用大于该项可结算自费金额";
            //                    return -1;
            //                }
            //                feeinfoHead.FT.OwnCost = feeinfoHead.FT.OwnCost - feeinfoDerate.FT.OwnCost;
            //                feeinfoHead.FT.TotCost = feeinfoHead.FT.TotCost - feeinfoDerate.FT.TotCost;

            //            }
            //        }
            //    }

            //    if (this.FeeInfoTransFeeStat(patientInfo, balanceType, alInvoiceDerateFeeInfo, alBalanceListDerate, sysdate, balanceNO, "", alFeeState) == -1)
            //    {
            //        this.RollBack();
            //        return -1;
            //    }
            //}


            #endregion

            #region 优惠发票

            List<FS.HISFC.Models.Fee.Inpatient.BalanceList> alBalanceListRebate = new List<FS.HISFC.Models.Fee.Inpatient.BalanceList>();
            bool IsRebatePrint = controlParamMgr.GetControlParam<bool>("100007");
            //判断优惠是否单独打印发票
            if (IsRebatePrint)
            {
                ArrayList alInvoiceRebateFeeInfo = new ArrayList();
                for (int i = 0; i < feeInfoList.Count; i++)
                {
                    FS.HISFC.Models.Fee.Inpatient.FeeInfo f = feeInfoList[i];
                    if (f.FT.RebateCost > 0)
                    {
                        FS.HISFC.Models.Fee.Inpatient.FeeInfo FeeInfoRebate = new FS.HISFC.Models.Fee.Inpatient.FeeInfo();
                        FeeInfoRebate.Item.MinFee.ID = f.Item.MinFee.ID;
                        FeeInfoRebate.FT.RebateCost = f.FT.RebateCost;
                        FeeInfoRebate.FT.TotCost = f.FT.TotCost;
                        alInvoiceRebateFeeInfo.Add(FeeInfoRebate);
                    }
                }

                if (this.FeeInfoTransFeeStat(patientInfo, balanceType, alInvoiceRebateFeeInfo, alBalanceListRebate, sysdate, balanceNO, invoiceNO, alFeeState) == -1)
                {
                    this.RollBack();
                    return -1;
                }
            }

            #endregion

            #region 膳食发票

            //List<FS.HISFC.Models.Fee.Inpatient.BalanceList> alBalanceListFood = new List<FS.HISFC.Models.Fee.Inpatient.BalanceList>();
            //bool IsFoodPrint = FS.FrameWork.Function.NConvert.ToBoolean(controlParm.QueryControlerInfo("100006"));
            //if (IsFoodPrint)
            //{
            //    ArrayList alInvoiceFoodFeeInfo = new ArrayList();
            //    string foodMinfeeID = controlParm.QueryControlerInfo("100014");
            //    for (int i = 0; i < feeInfoList.Count; i++)
            //    {
            //        FS.HISFC.Models.Fee.Inpatient.FeeInfo f = (FS.HISFC.Models.Fee.Inpatient.FeeInfo)feeInfoList[i];
            //        if (f.Item.MinFee.ID == foodMinfeeID)
            //        {
            //            feeInfoList.RemoveAt(i);//？？？
            //            alInvoiceFoodFeeInfo.Add(f);
            //        }
            //    }

            //    //处理膳食发票明细大类
            //    if (alInvoiceFoodFeeInfo.Count > 0)
            //    {
            //        if (this.FeeInfoTransFeeStat(patientInfo, balanceType, alInvoiceFoodFeeInfo, alBalanceListFood, sysdate, balanceNO, "", alFeeState) == -1)
            //        {
            //            this.RollBack();
            //            return -1;
            //        }
            //    }

            //}

            #endregion

            #region 婴儿发票
            //List<FS.HISFC.Models.Fee.Inpatient.BalanceList> alBalanceListBaby = new List<FS.HISFC.Models.Fee.Inpatient.BalanceList>();
            //if (patientInfo.IsBaby)
            //{
            //    bool IsBabyPrint = FS.FrameWork.Function.NConvert.ToBoolean(controlParm.QueryControlerInfo("100004"));
            //    //判断婴儿费用是否单独打印发票 
            //    if (IsBabyPrint)
            //    {
            //        ArrayList alInvoiceBabyFeeInfo = new ArrayList();
            //        ArrayList al = inpatientFeeManager.QueryFeeInfosGroupByMinFeeForBaby(patientInfo.ID, beginTime, endTime, "0");
            //        if (al == null)
            //        {
            //            this.RollBack();
            //            this.err = "结算失败，原因：" + inpatientFeeManager.Err;
            //            return -1;
            //        }

            //        for (int i = 0; i < feeInfoList.Count; i++)
            //        {
            //            FS.HISFC.Models.Fee.Inpatient.FeeInfo f = feeInfoList[i];
            //            for (int j = 0; j < al.Count; j++)
            //            {
            //                FS.HISFC.Models.Fee.Inpatient.FeeInfo feeinfoBaby = (FS.HISFC.Models.Fee.Inpatient.FeeInfo)al[j];
            //                if (f.Item.MinFee.ID == feeinfoBaby.Item.MinFee.ID)
            //                {
            //                    if (f.FT.TotCost - feeinfoBaby.FT.TotCost < 0)
            //                    {
            //                        this.RollBack();
            //                        this.err = "结算失败，原因：输入的结算金额小于婴儿实际发生费用";
            //                        return -1;

            //                    }
            //                    f.FT.TotCost = f.FT.TotCost - feeinfoBaby.FT.TotCost;
            //                    f.FT.OwnCost = f.FT.OwnCost - feeinfoBaby.FT.OwnCost;
            //                    f.FT.PayCost = f.FT.PayCost - feeinfoBaby.FT.PayCost;
            //                    f.FT.PubCost = f.FT.PubCost - feeinfoBaby.FT.PubCost;
            //                    if (!IsRebatePrint)
            //                    {
            //                        f.FT.RebateCost = f.FT.RebateCost - feeinfoBaby.FT.RebateCost;
            //                    }
            //                    alInvoiceBabyFeeInfo.Add(feeinfoBaby);
            //                }
            //            }
            //        }

            //        if (this.FeeInfoTransFeeStat(patientInfo, balanceType, alInvoiceBabyFeeInfo, alBalanceListBaby, sysdate, balanceNO, "", alFeeState) == -1)
            //        {
            //            this.RollBack();
            //            return -1;
            //        }
            //    }

            //}

            #endregion

            #region 手工发票

            List<FS.HISFC.Models.Fee.Inpatient.BalanceList> alBalanceListMannal = new List<FS.HISFC.Models.Fee.Inpatient.BalanceList>();
            if (splitInvoiceCost > 0)
            {
                ArrayList alInvoiceManualFeeInfo = new ArrayList();
                decimal splitInvoiceCostTemp = splitInvoiceCost;

                decimal splitRate = splitInvoiceCost / TotBalanceCost;
                if (balanceType == FS.HISFC.Models.Base.EBlanceType.OweMid)
                {
                    splitRate = splitInvoiceCost / (TotBalanceCost - arrearFeeCost);
                }

                if (splitRate > 1)
                {
                    this.RollBack();
                    this.err = "发票金额大于结算金额";
                    return -1;
                }

                //循环处理分票明细及主发票明细
                for (int i = 0; i < feeInfoList.Count; i++)
                {
                    FS.HISFC.Models.Fee.Inpatient.FeeInfo InvoiceHeadInfo = feeInfoList[i];
                    FS.HISFC.Models.Fee.Inpatient.FeeInfo InvoiceManualInfo = InvoiceHeadInfo.Clone();

                    InvoiceManualInfo.FT.OwnCost = FS.FrameWork.Function.NConvert.ToDecimal(FS.FrameWork.Public.String.FormatNumberReturnString(InvoiceHeadInfo.FT.OwnCost * splitRate, 2));
                    InvoiceManualInfo.FT.PayCost = FS.FrameWork.Function.NConvert.ToDecimal(FS.FrameWork.Public.String.FormatNumberReturnString(InvoiceHeadInfo.FT.PayCost * splitRate, 2));
                    InvoiceManualInfo.FT.PubCost = FS.FrameWork.Function.NConvert.ToDecimal(FS.FrameWork.Public.String.FormatNumberReturnString(InvoiceHeadInfo.FT.PubCost * splitRate, 2));
                    InvoiceManualInfo.FT.RebateCost = FS.FrameWork.Function.NConvert.ToDecimal(FS.FrameWork.Public.String.FormatNumberReturnString(InvoiceHeadInfo.FT.RebateCost * splitRate, 2));

                    InvoiceManualInfo.FT.TotCost = InvoiceManualInfo.FT.OwnCost + InvoiceManualInfo.FT.PayCost + InvoiceManualInfo.FT.PubCost + InvoiceManualInfo.FT.RebateCost;

                    InvoiceHeadInfo.FT.TotCost = InvoiceHeadInfo.FT.TotCost - InvoiceManualInfo.FT.TotCost;
                    InvoiceHeadInfo.FT.OwnCost = InvoiceHeadInfo.FT.OwnCost - InvoiceManualInfo.FT.OwnCost;
                    InvoiceHeadInfo.FT.PayCost = InvoiceHeadInfo.FT.PayCost - InvoiceManualInfo.FT.PayCost;
                    InvoiceHeadInfo.FT.PubCost = InvoiceHeadInfo.FT.PubCost - InvoiceManualInfo.FT.PubCost;
                    InvoiceHeadInfo.FT.RebateCost = InvoiceHeadInfo.FT.RebateCost - InvoiceManualInfo.FT.RebateCost;
                    splitInvoiceCostTemp -= InvoiceManualInfo.FT.TotCost;

                    alInvoiceManualFeeInfo.Add(InvoiceManualInfo);
                }

                //如果没有分完整，平小数位
                if (Math.Abs(splitInvoiceCostTemp) > 0)
                {
                    for (int i = 0; i < feeInfoList.Count; i++)
                    {
                        if (feeInfoList[i].FT.OwnCost + splitInvoiceCostTemp > 0)
                        {
                            FS.HISFC.Models.Fee.Inpatient.FeeInfo InvoiceManualInfo = feeInfoList[i].Clone();
                            InvoiceManualInfo.FT.OwnCost = splitInvoiceCostTemp;
                            InvoiceManualInfo.FT.PayCost = 0.00M;
                            InvoiceManualInfo.FT.PubCost = 0.00M;
                            InvoiceManualInfo.FT.RebateCost = 0.00M;
                            InvoiceManualInfo.FT.TotCost = splitInvoiceCostTemp;
                            feeInfoList[i].FT.OwnCost -= splitInvoiceCostTemp;
                            feeInfoList[i].FT.TotCost -= splitInvoiceCostTemp;
                            alInvoiceManualFeeInfo.Add(InvoiceManualInfo);
                            break;
                        }
                    }
                }

                if (this.FeeInfoTransFeeStat(patientInfo, balanceType, alInvoiceManualFeeInfo, alBalanceListMannal, sysdate, balanceNO, "", alFeeState) == -1)
                {
                    this.RollBack();
                    return -1;
                }
            }

            #endregion

            #region 主发票

            List<FS.HISFC.Models.Fee.Inpatient.BalanceList> balanceList = new List<FS.HISFC.Models.Fee.Inpatient.BalanceList>();
            if (feeInfoList.Count > 0)
            {
                if (this.FeeInfoTransFeeStat(patientInfo, balanceType, new ArrayList(feeInfoList), balanceList, sysdate, balanceNO, invoiceNO, alFeeState) == -1)
                {
                    this.RollBack();
                    return -1;
                }

            }

            #endregion

            #endregion

            #region 处理发票明细

            bool mainInvoice = true;

            #region 主发票

            if (balanceList.Count > 0)
            {
                decimal TotCost = 0m;
                decimal OwnCost = 0m;
                decimal PayCost = 0m;
                decimal PubCost = 0m;
                decimal RebateCost = 0M;
                foreach (FS.HISFC.Models.Fee.Inpatient.BalanceList balance in balanceList)
                {
                    TotCost += balance.BalanceBase.FT.TotCost;
                    OwnCost += balance.BalanceBase.FT.OwnCost;
                    PayCost += balance.BalanceBase.FT.PayCost;
                    PubCost += balance.BalanceBase.FT.PubCost;
                    RebateCost += balance.BalanceBase.FT.RebateCost;
                    if (balanceType == FS.HISFC.Models.Base.EBlanceType.OweMid)
                    {
                        balance.BalanceBase.BalanceType.ID = FS.HISFC.Models.Fee.EnumBalanceType.O;
                    }
                    balance.BalanceBase.Invoice.ID = invoiceNO;
                    if (this.inpatientFeeManager.InsertBalanceList(patientInfo, balance) < 1)
                    {
                        this.RollBack();
                        this.err = this.inpatientFeeManager.Err;
                        return -1;
                    }

                }

                FS.HISFC.Models.Fee.Inpatient.Balance balanceHead = ((FS.HISFC.Models.Fee.Inpatient.Balance)balanceList[0].BalanceBase).Clone();

                balanceHead.Invoice.ID = invoiceNO;
                balanceHead.BeginTime = patientInfo.PVisit.InTime;//患者入院时间
                balanceHead.EndTime = sysdate;//结算时间
                balanceHead.FT.TotCost = TotCost;
                balanceHead.FT.OwnCost = OwnCost;
                balanceHead.FT.PayCost = PayCost;
                balanceHead.FT.PubCost = PubCost;

                balanceHead.CancelType = FS.HISFC.Models.Base.CancelTypes.Valid;
                balanceHead.BalanceSaveType = "0";
                balanceHead.IsMainInvoice = true;

                //优惠金额
                if (!IsRebatePrint)
                {
                    balanceHead.FT.RebateCost = RebateCost;
                }
                balanceHead.IsMainInvoice = mainInvoice;
                if (mainInvoice)
                {
                    mainInvoice = false;
                    balanceHead.FT.PrepayCost = TotPrepayCost;//预交金额
                    balanceHead.FT.TransferPrepayCost = 0.00M;//欠费金额
                    if (TotBalanceCost == TotPrepayCost)
                    {
                        balanceHead.FT.SupplyCost = 0M;//应收金额
                        balanceHead.FT.ReturnCost = 0M;//应返金额
                    }
                    else if (TotBalanceCost > TotPrepayCost)
                    {
                        balanceHead.FT.SupplyCost = TotBalanceCost - TotPrepayCost;//应收金额
                        balanceHead.FT.ReturnCost = 0M;//应返金额
                    }
                    else
                    {
                        balanceHead.FT.SupplyCost = 0M;//应收金额
                        balanceHead.FT.ReturnCost = TotPrepayCost - TotBalanceCost;//应返金额
                    }

                    balanceHead.FT.SupplyCost = balanceHead.FT.SupplyCost - arrearFeeCost;

                    if (balanceHead.BalanceType.ID.ToString() == FS.HISFC.Models.Fee.EnumBalanceType.Q.ToString())
                    {
                        balanceHead.FT.ArrearCost = arrearFeeCost;
                    }
                }
                else
                {
                    if (balanceHead.BalanceType.ID.ToString() == FS.HISFC.Models.Fee.EnumBalanceType.Q.ToString())
                    {
                        balanceHead.FT.ArrearCost = arrearFeeCost;
                    }
                }

                if (patientInfo.Pact.PayKind.ID == "02")
                {
                    balanceHead.FT.OwnCost = patientInfo.SIMainInfo.OwnCost - balanceHead.FT.DerateCost - balanceHead.FT.RebateCost;
                    balanceHead.FT.PayCost = patientInfo.SIMainInfo.PayCost;
                    balanceHead.FT.PubCost = patientInfo.SIMainInfo.PubCost;
                }

                //插入结算头表
                balanceHead.PrintedInvoiceNO = realInvoiceNO;
                if (this.inpatientFeeManager.InsertBalance(patientInfo, balanceHead) < 1)
                {
                    this.RollBack();
                    this.err = this.inpatientFeeManager.Err;
                    return -1;
                }

                if (feeIntegrate.InsertInvoiceExtend(invoiceNO, "I", realInvoiceNO, "00") < 1)
                {
                    this.RollBack();
                    this.err = this.inpatientFeeManager.Err;
                    return -1;
                }

                if (feeIntegrate.UseInvoiceNO("I", 1, ref invoiceNO, ref realInvoiceNO, ref this.err) != 1)
                {
                    this.RollBack();
                    return -1;
                }

                listBalance.Add(balanceHead);
            }

            #endregion

            #region 手工发票

            if (alBalanceListMannal.Count > 0)
            {
                if (feeIntegrate.GetInvoiceNO("I", ref invoiceNO, ref realInvoiceNO, ref errText) < 1 || string.IsNullOrEmpty(invoiceNO) || string.IsNullOrEmpty(realInvoiceNO))
                {
                    this.RollBack();
                    this.err = "获取发票失败，" + feeIntegrate.Err;
                    return -1;
                }

                decimal TotCost = 0m;
                decimal OwnCost = 0m;
                decimal PayCost = 0m;
                decimal PubCost = 0m;
                decimal RebateCost = 0M;
                foreach (FS.HISFC.Models.Fee.Inpatient.BalanceList balance in alBalanceListMannal)
                {
                    TotCost += balance.BalanceBase.FT.TotCost;
                    OwnCost += balance.BalanceBase.FT.OwnCost;
                    PayCost += balance.BalanceBase.FT.PayCost;
                    PubCost += balance.BalanceBase.FT.PubCost;
                    RebateCost += balance.BalanceBase.FT.RebateCost;

                    if (balanceType == FS.HISFC.Models.Base.EBlanceType.OweMid)
                    {
                        balance.BalanceBase.BalanceType.ID = FS.HISFC.Models.Fee.EnumBalanceType.O;
                    }

                    balance.BalanceBase.Invoice.ID = invoiceNO;
                    if (this.inpatientFeeManager.InsertBalanceList(patientInfo, balance) < 1)
                    {
                        this.RollBack();
                        this.err = this.inpatientFeeManager.Err;
                        return -1;
                    }

                }

                FS.HISFC.Models.Fee.Inpatient.Balance balanceHead = ((FS.HISFC.Models.Fee.Inpatient.Balance)alBalanceListMannal[0].BalanceBase).Clone();

                balanceHead.Invoice.ID = invoiceNO;
                balanceHead.BeginTime = patientInfo.PVisit.InTime;//患者入院时间
                balanceHead.EndTime = sysdate;//结算时间
                balanceHead.FT.TotCost = TotCost;
                balanceHead.FT.OwnCost = OwnCost;
                balanceHead.FT.PayCost = PayCost;
                balanceHead.FT.PubCost = PubCost;

                balanceHead.CancelType = FS.HISFC.Models.Base.CancelTypes.Valid;
                balanceHead.IsMainInvoice = false;
                balanceHead.BalanceSaveType = "0";

                //优惠金额
                if (!IsRebatePrint)
                {
                    balanceHead.FT.RebateCost = RebateCost;
                }

                balanceHead.IsMainInvoice = mainInvoice;
                if (mainInvoice)
                {
                    mainInvoice = false;
                    balanceHead.FT.PrepayCost = TotPrepayCost;//预交金额
                    balanceHead.FT.TransferPrepayCost = 0.00M;//欠费金额
                    if (TotBalanceCost == TotPrepayCost)
                    {
                        balanceHead.FT.SupplyCost = 0M;//应收金额
                        balanceHead.FT.ReturnCost = 0M;//应返金额
                    }
                    else if (TotBalanceCost > TotPrepayCost)
                    {
                        balanceHead.FT.SupplyCost = TotBalanceCost - TotPrepayCost;//应收金额
                        balanceHead.FT.ReturnCost = 0M;//应返金额
                    }
                    else
                    {
                        balanceHead.FT.SupplyCost = 0M;//应收金额
                        balanceHead.FT.ReturnCost = TotPrepayCost - TotBalanceCost;//应返金额
                    }

                    balanceHead.FT.SupplyCost = balanceHead.FT.SupplyCost - arrearFeeCost;

                    if (balanceHead.BalanceType.ID.ToString() == FS.HISFC.Models.Fee.EnumBalanceType.Q.ToString())
                    {
                        balanceHead.FT.ArrearCost = arrearFeeCost;
                    }
                }
                else
                {
                    if (balanceHead.BalanceType.ID.ToString() == FS.HISFC.Models.Fee.EnumBalanceType.Q.ToString())
                    {
                        balanceHead.FT.ArrearCost = arrearFeeCost;
                    }
                }
                //医保处理 2012年7月24日18:10:28
                if (patientInfo.Pact.PayKind.ID == "02")
                {
                    balanceHead.FT.OwnCost = patientInfo.SIMainInfo.OwnCost - balanceHead.FT.DerateCost - balanceHead.FT.RebateCost;
                    balanceHead.FT.PayCost = patientInfo.SIMainInfo.PayCost;
                    balanceHead.FT.PubCost = patientInfo.SIMainInfo.PubCost;
                }
                //插入结算头表
                balanceHead.PrintedInvoiceNO = realInvoiceNO;
                if (this.inpatientFeeManager.InsertBalance(patientInfo, balanceHead) < 1)
                {
                    this.RollBack();
                    this.err = this.inpatientFeeManager.Err;
                    return -1;
                }

                if (feeIntegrate.InsertInvoiceExtend(invoiceNO, "I", realInvoiceNO, "00") < 1)
                {
                    this.RollBack();
                    this.err = this.inpatientFeeManager.Err;
                    return -1;
                }

                if (feeIntegrate.UseInvoiceNO("I", 1, ref invoiceNO, ref realInvoiceNO, ref this.err) != 1)
                {
                    this.RollBack();
                    return -1;
                }

                listBalance.Add(balanceHead);
            }
            #endregion

            #region 优惠发票

            if (alBalanceListRebate.Count > 0)
            {
                if (feeIntegrate.GetInvoiceNO("I", ref invoiceNO, ref realInvoiceNO, ref errText) < 1 || string.IsNullOrEmpty(invoiceNO) || string.IsNullOrEmpty(realInvoiceNO))
                {
                    this.RollBack();
                    this.err = "获取发票失败，" + feeIntegrate.Err;
                    return -1;
                }
                decimal TotCost = 0m;
                decimal OwnCost = 0m;
                decimal PayCost = 0m;
                decimal PubCost = 0m;
                decimal RebateCost = 0M;
                foreach (FS.HISFC.Models.Fee.Inpatient.BalanceList balance in alBalanceListRebate)
                {
                    TotCost += balance.BalanceBase.FT.TotCost;
                    OwnCost += balance.BalanceBase.FT.OwnCost;
                    PayCost += balance.BalanceBase.FT.PayCost;
                    PubCost += balance.BalanceBase.FT.PubCost;
                    RebateCost += balance.BalanceBase.FT.RebateCost;
                    balance.BalanceBase.Invoice.ID = invoiceNO;
                    if (this.inpatientFeeManager.InsertBalanceList(patientInfo, balance) < 1)
                    {
                        this.RollBack();
                        this.err = this.inpatientFeeManager.Err;
                        return -1;
                    }
                }
                //头表实体赋值
                FS.HISFC.Models.Fee.Inpatient.Balance BalanceRebate = (FS.HISFC.Models.Fee.Inpatient.Balance)alBalanceListRebate[0].BalanceBase.Clone();
                BalanceRebate.FT.TotCost = TotCost;
                BalanceRebate.FT.OwnCost = OwnCost;
                BalanceRebate.FT.PayCost = PayCost;
                BalanceRebate.FT.PubCost = PubCost;
                BalanceRebate.FT.RebateCost = RebateCost;
                BalanceRebate.BeginTime = patientInfo.PVisit.InTime;
                BalanceRebate.EndTime = sysdate;
                BalanceRebate.CancelType = FS.HISFC.Models.Base.CancelTypes.Valid;
                BalanceRebate.IsMainInvoice = false;
                BalanceRebate.BalanceSaveType = "0";
                //插入结算头表
                BalanceRebate.PrintedInvoiceNO = realInvoiceNO;
                if (this.inpatientFeeManager.InsertBalance(patientInfo, BalanceRebate) < 1)
                {
                    this.RollBack();
                    this.err = this.inpatientFeeManager.Err;
                    return -1;
                }

                if (feeIntegrate.InsertInvoiceExtend(invoiceNO, "I", realInvoiceNO, "00") < 1)
                {
                    this.RollBack();
                    this.err = feeIntegrate.Err;
                    return -1;
                }

                if (feeIntegrate.UseInvoiceNO("I", 1, ref invoiceNO, ref realInvoiceNO, ref this.err) != 1)
                {
                    this.RollBack();
                    return -1;
                }
                listBalance.Add(BalanceRebate);
            }

            #endregion

            #region 欠费发票

            if (alArrearBalanceList.Count > 0)
            {
                if (feeIntegrate.GetInvoiceNO(this.inpatientFeeManager.Operator as FS.HISFC.Models.Base.Employee, "I", true, ref invoiceNO, ref realInvoiceNO, ref this.err) != 1)
                {
                    this.RollBack();
                    return -1;
                }

                decimal TotCost = 0m;
                decimal OwnCost = 0m;
                decimal PayCost = 0m;
                decimal PubCost = 0m;
                decimal RebateCost = 0M;
                foreach (FS.HISFC.Models.Fee.Inpatient.BalanceList balance in alArrearBalanceList)
                {
                    TotCost += balance.BalanceBase.FT.TotCost;
                    OwnCost += balance.BalanceBase.FT.OwnCost;
                    PayCost += balance.BalanceBase.FT.PayCost;
                    PubCost += balance.BalanceBase.FT.PubCost;
                    RebateCost += balance.BalanceBase.FT.RebateCost;
                    ((FS.HISFC.Models.Fee.Inpatient.Balance)balance.BalanceBase).IsTempInvoice = true;
                    balance.BalanceBase.Invoice.ID = invoiceNO;
                    if (this.inpatientFeeManager.InsertBalanceList(patientInfo, balance) < 1)
                    {
                        this.RollBack();
                        this.err = this.inpatientFeeManager.Err;
                        return -1;
                    }

                }

                FS.HISFC.Models.Fee.Inpatient.Balance balanceHead = ((FS.HISFC.Models.Fee.Inpatient.Balance)alArrearBalanceList[0].BalanceBase).Clone();

                balanceHead.Invoice.ID = invoiceNO;
                balanceHead.BeginTime = patientInfo.PVisit.InTime;//患者入院时间
                balanceHead.EndTime = sysdate;//结算时间
                balanceHead.FT.TotCost = TotCost;
                balanceHead.FT.OwnCost = OwnCost;
                balanceHead.FT.PayCost = PayCost;
                balanceHead.FT.PubCost = PubCost;
                balanceHead.IsTempInvoice = true;
                balanceHead.CancelType = FS.HISFC.Models.Base.CancelTypes.Valid;
                balanceHead.BalanceSaveType = "0";

                //优惠金额
                balanceHead.FT.RebateCost = RebateCost;
                balanceHead.IsMainInvoice = mainInvoice;
                if (mainInvoice)
                {
                    mainInvoice = false;
                    balanceHead.FT.PrepayCost = TotPrepayCost;//预交金额
                    balanceHead.FT.TransferPrepayCost = 0.00M;//欠费金额
                    if (TotBalanceCost == TotPrepayCost)
                    {
                        balanceHead.FT.SupplyCost = 0M;//应收金额
                        balanceHead.FT.ReturnCost = 0M;//应返金额
                    }
                    else if (TotBalanceCost > TotPrepayCost)
                    {
                        balanceHead.FT.SupplyCost = TotBalanceCost - TotPrepayCost;//应收金额
                        balanceHead.FT.ReturnCost = 0M;//应返金额
                    }
                    else
                    {
                        balanceHead.FT.SupplyCost = 0M;//应收金额
                        balanceHead.FT.ReturnCost = TotPrepayCost - TotBalanceCost;//应返金额
                    }


                    balanceHead.FT.SupplyCost = balanceHead.FT.SupplyCost - arrearFeeCost;

                    if (balanceHead.BalanceType.ID.ToString() == FS.HISFC.Models.Fee.EnumBalanceType.Q.ToString())
                    {
                        balanceHead.FT.ArrearCost = arrearFeeCost;
                    }
                }
                else
                {
                    if (balanceHead.BalanceType.ID.ToString() == FS.HISFC.Models.Fee.EnumBalanceType.Q.ToString())
                    {
                        balanceHead.FT.ArrearCost = arrearFeeCost;
                    }
                }

                //插入结算头表
                balanceHead.PrintedInvoiceNO = realInvoiceNO;
                if (this.inpatientFeeManager.InsertBalance(patientInfo, balanceHead) < 1)
                {
                    this.RollBack();
                    this.err = this.inpatientFeeManager.Err;
                    return -1;
                }
            }

            #endregion

            #endregion

            #region 患者费用处理

            patientInfo.SIMainInfo.BalNo = balNo;
            patientInfo.SIMainInfo.InvoiceNo = invoiceNO;
            // patientInfo.PVisit.InState.ID = FS.HISFC.Models.Base.EnumInState.O;//出院结算

            FS.HISFC.Models.Fee.Inpatient.FeeInfo feeInfo = new FS.HISFC.Models.Fee.Inpatient.FeeInfo();
            feeInfo.FT.PubCost = TotPubCost;
            feeInfo.FT.OwnCost = TotOwnCost;
            feeInfo.FT.PayCost = TotPayCost;
            feeInfo.FT.RebateCost = TotRebateCost;
            feeInfo.FT.TotCost = TotBalanceCost;
            feeInfo.FT.PrepayCost = TotPrepayCost;

            //医保记账不处理按项目结算，先屏蔽
            //if (patientInfo.Pact.PayKind.ID == "02")
            //{
            //    feeInfo.FT.PubCost = patientInfo.SIMainInfo.PubCost;
            //    feeInfo.FT.OwnCost = patientInfo.SIMainInfo.OwnCost;
            //    feeInfo.FT.PayCost = patientInfo.SIMainInfo.PayCost;
            //    //feeInfo.FT.RebateCost = TotRebateCost;
            //    //feeInfo.FT.TotCost = TotBalanceCost;
            //    //feeInfo.FT.PrepayCost = TotPrepayCost;
            //}

            if (feeInfo.FT.TotCost != feeInfo.FT.OwnCost + feeInfo.FT.PayCost + feeInfo.FT.PubCost + feeInfo.FT.RebateCost)
            {
                this.RollBack();
                this.err = "总费用与分支费用不相等，请检查！";
                return -1;
            }

            if (this.inpatientFeeManager.UpdateInMainInfoBalanced(patientInfo, sysdate, balanceNO, feeInfo.FT) <= 0)
            {
                this.RollBack();
                this.err = this.inpatientFeeManager.Err;
                return -1;
            }

            //开账
            if (this.inpatientFeeManager.OpenAccount(patientInfo.ID) < 0)
            {
                this.RollBack();
                this.err = this.inpatientFeeManager.Err;
                return -1;
            }

            string motherPayAllFee = controlParamMgr.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.SysConst.Use_Mother_PayAllFee, false, "0");
            if (motherPayAllFee == "1")//婴儿的费用收在妈妈的身上 
            {
                ArrayList babyList = radtManager.QueryBabies(patientInfo.ID);
                if (babyList != null && babyList.Count > 0)
                {
                    foreach (FS.HISFC.Models.RADT.PatientInfo baby in babyList)
                    {
                        FS.HISFC.Models.RADT.PatientInfo pTemp = radtManager.GetPatientInfo(baby.ID);
                        if (pTemp != null && !string.IsNullOrEmpty(pTemp.ID))
                        {
                            pTemp.PVisit = patientInfo.PVisit.Clone();

                            if (this.inpatientFeeManager.UpdateInMainInfoBalanced(pTemp, sysdate, balanceNO, new FS.HISFC.Models.Base.FT()) < 0)
                            {
                                this.RollBack();
                                this.err = this.inpatientFeeManager.Err;
                                return -1;
                            }
                        }
                    }
                }
            }

            #endregion

            #region 结算变更记录

            FS.FrameWork.Models.NeuObject oldObj = new FS.FrameWork.Models.NeuObject();
            FS.FrameWork.Models.NeuObject newObj = new FS.FrameWork.Models.NeuObject();
            newObj.ID = patientInfo.SIMainInfo.BalNo;
            newObj.Name = "结算序号";
            if (radtManager.SaveShiftData(patientInfo.ID, FS.HISFC.Models.Base.EnumShiftType.BA, "项目结算", oldObj, newObj) == -1)
            {
                this.RollBack();
                this.err = radtManager.Err;
                return -1;
            }

            #endregion

            #region 发票打印
            int returnValue = PrintInvoice(patientInfo, listBalance, ref this.err);
            if (returnValue != 1)
            {
                this.RollBack();
                return -1;
            }
            #endregion

            //发送消息
            if (InterfaceManager.GetIADT() != null)
            {
                if (InterfaceManager.GetIADT().Balance(patientInfo, true) < 0)
                {
                    this.RollBack();
                    this.err = InterfaceManager.GetIADT().Err;
                    return -1;
                }
            }

            this.Commit();
            return 1;
        }

        #endregion


        #region 爱博恩出院结算

        #region 出院结算

        /// <summary>
        /// {18AD984D-78C6-4c64-8066-6F31E3BDF7DA}
        /// 出院结算
        /// </summary>
        /// <param name="patientInfo">患者信息</param>
        /// <param name="balanceType">结算类型</param>
        /// <param name="feeItemList">是否套餐</param>
        /// <param name="feeItemList">结算费用</param>
        /// <param name="prepayList">预交金</param>
        /// <param name="balancePayList">费用明细</param>
        /// <param name="splitInvoiceCost">分发票</param>
        /// <param name="ecoCost">优惠金额</param>
        /// <param name="listBalance">结算信息返回</param>
        /// <returns></returns>
        public int NewBalance(FS.HISFC.Models.RADT.PatientInfo patientInfo,
                                 FS.HISFC.Models.Base.EBlanceType balanceType,
                                 bool packageBalance,
                                 List<FS.HISFC.Models.Fee.Inpatient.FeeItemList> feeItemList,
                                 List<FS.HISFC.Models.Fee.Inpatient.Prepay> prepayList,
                                 List<FS.HISFC.Models.Fee.Inpatient.BalancePay> balancePayList,
                                 decimal splitInvoiceCost,
                                 ArrayList packageList,
                                 ref List<FS.HISFC.Models.Fee.Inpatient.Balance> listBalance)
        {
            if (patientInfo == null || string.IsNullOrEmpty(patientInfo.ID))
            {
                this.err = "结算失败，患者信息为空！";
                return -1;
            }

            this.BeginTransaction();

            //{14CCBD16-9A45-42f8-896C-5A2CB00DAB1B}
            try
            {
                #region 患者信息验证
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在查询验证患者信息...");
                RADT radtManager = new RADT();
                FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParamMgr = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
                radtManager.SetTrans(this.Trans);
                controlParamMgr.SetTrans(this.Trans);
                //获取时间
                DateTime sysdate = inpatientFeeManager.GetDateTimeFromSysDateTime();
                //验证患者信息
                FS.HISFC.Models.RADT.PatientInfo patientInfoTemp = radtManager.GetPatientInfo(patientInfo.ID);
                if (patientInfoTemp == null || string.IsNullOrEmpty(patientInfoTemp.ID))
                {
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    this.RollBack();
                    this.err = "患者信息为空，" + radtManager.Err;
                    return -1;
                }

                //已经出院的返回
                if (patientInfo.PVisit.InState.ID.ToString().Equals(FS.HISFC.Models.Base.EnumInState.O))
                {
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    this.RollBack();
                    this.err = "患者已经出院结算";
                    return -1;
                }
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                #endregion


                #region 发票号和结算号
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在处理发票号...");
                FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();
                feeIntegrate.SetTrans(this.Trans);
                inpatientFeeManager.SetTrans(this.Trans);
                string invoiceNO = string.Empty;
                string realInvoiceNO = string.Empty;
                string errText = "";

                if (feeIntegrate.GetInvoiceNO("I", ref invoiceNO, ref realInvoiceNO, ref errText) < 1 || string.IsNullOrEmpty(invoiceNO) || string.IsNullOrEmpty(realInvoiceNO))
                {
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    this.RollBack();
                    this.err = "获取发票失败，" + feeIntegrate.Err;
                    return -1;
                }

                //调业务层获取结算次数
                int balanceNO = 0;
                string balNo = inpatientFeeManager.GetNewBalanceNO(patientInfo.ID);
                if (balNo == null || balNo.Length == 0)
                {
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    this.RollBack();
                    this.err = "获取结算序号出错，" + feeIntegrate.Err;
                    return -1;
                }
                else
                {
                    balanceNO = int.Parse(balNo);
                }
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                #endregion

                #region 处理预交金信息
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在处理预交金...");
                decimal TotPrepayCost = 0.00M;
                if (prepayList != null && prepayList.Count > 0)
                {
                    foreach (FS.HISFC.Models.Fee.Inpatient.Prepay prepay in prepayList)
                    {
                        prepay.BalanceNO = balanceNO;
                        prepay.Invoice.ID = invoiceNO;
                        prepay.BalanceOper.ID = this.inpatientFeeManager.Operator.ID;
                        prepay.BalanceOper.OperTime = sysdate;

                        TotPrepayCost += prepay.FT.PrepayCost;//预交金金额

                        //转入预交金处理（目前缺失更新单条的）
                        if (prepay.Name == "转入预交金")
                        {
                            if (inpatientFeeManager.UpdateChangePrepayBalanced(patientInfo.ID, balanceNO) == -1)
                            {
                                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                                this.RollBack();
                                this.err = "结算失败，原因：" + inpatientFeeManager.Err;
                                return -1;
                            }
                        }
                        else
                        {
                            //更新预交发票为结算状态
                            if (inpatientFeeManager.UpdatePrepayBalanced(patientInfo, prepay) <= 0)
                            {
                                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                                this.RollBack();
                                this.err = "结算失败，原因：" + inpatientFeeManager.Err;
                                return -1;
                            }
                        }
                    }
                }
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                #endregion

                #region 统计总金额和优惠金额

                decimal TotRealCost = 0.0m;    //实收支付
                decimal TotDonateCost = 0.0m;  //赠送支付
                decimal TotRebateCost = 0.0m;  //优惠支付


                //kind = 0 ,retrunflag = 1    +
                //       1               2    -
                //       1               1    +
                //       0               2    +
                if (balancePayList != null && balancePayList.Count > 0)
                {
                    foreach (FS.HISFC.Models.Fee.Inpatient.BalancePay balancePay in balancePayList)
                    {
                        //{333D2AD8-DC4A-4c30-A14E-D6815AC858F9}
                        //积分作为赠送支付分配
                        if (balancePay.PayType.ID == "DC" || balancePay.PayType.ID == "PD" || balancePay.PayType.ID == "CO")       //赠送金额
                        {
                            if (balancePay.TransKind.ID == "1" && balancePay.RetrunOrSupplyFlag == "2")   //返还
                            {
                                TotDonateCost -= balancePay.FT.TotCost;
                            }
                            else
                            {
                                TotDonateCost += balancePay.FT.TotCost;
                            }
                        }
                        else if (balancePay.PayType.ID == "RC" || balancePay.PayType.ID == "PY")  //优惠
                        {

                            if (balancePay.TransKind.ID == "1" && balancePay.RetrunOrSupplyFlag == "2")   //返还
                            {
                                TotRebateCost -= balancePay.FT.TotCost;
                            }
                            else
                            {
                                TotRebateCost += balancePay.FT.TotCost;
                            }
                        }
                        else                                                                      //实收
                        {
                            if (balancePay.TransKind.ID == "1" && balancePay.RetrunOrSupplyFlag == "2")  //返还
                            {
                                TotRealCost -= balancePay.FT.TotCost;
                            }
                            else
                            {
                                TotRealCost += balancePay.FT.TotCost;
                            }
                        }

                    }
                }

                #endregion

                #region 拆费用
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在进行费用拆分...");
                //出院结算更新费用
                //判断并发操作
                decimal TotBalance = 0.00M; //获得费用总额
                decimal TotOwn = 0.00M;     //实收
                decimal TotPub = 0.00M;     //公费
                decimal TotPay = 0.00M;     //医保
                decimal TotRebate = 0.00M;  //优惠
                decimal TotDonate = 0.00M;      //赠送

                decimal TotCostForCt = TotRealCost + TotRebateCost + TotDonateCost;
                decimal TotRealCostForCt = TotRealCost;      //实收支付
                decimal TotRebateCostForCt = TotRebateCost;  //优惠支付
                decimal TotDonateCostForCt = TotDonateCost;  //赠送支付

                decimal TotCostForSt = TotRealCost + TotRebateCost + TotDonateCost;
                decimal TotRealCostForSt = TotRealCost;      //实收支付
                decimal TotRebateCostForSt = TotRebateCost;  //优惠支付
                decimal TotDonateCostForSt = TotDonateCost;  //赠送支付

                #region 项目拆分
                //fin_ipb_feeinfo主键：RECIPE_NO, FEE_CODE, EXECUTE_DEPTCODE, BALANCE_NO
                //进行结算的feeInfoList
                List<FS.HISFC.Models.Fee.Inpatient.FeeInfo> feeInfoList = new List<FS.HISFC.Models.Fee.Inpatient.FeeInfo>();
                //进行结算的费用明细
                List<FS.HISFC.Models.Fee.Inpatient.FeeItemList> balanceFeeItemLists = new List<FS.HISFC.Models.Fee.Inpatient.FeeItemList>();

                //需要插入和更新的拆分出来的新项(如一条记录中存在5个项目，只需要其中的3个，
                //则原来的数据更新为2[NeedUpdateFeeInfoList]，新产生一条需要插入的记录[NeedInsertFeeInfoList])
                //如果全部都需要更新，则整条记录增加到[NeedUpdateFeeInfoList]
                Hashtable hsNeedUpdateFeeRecipeNO = new Hashtable(); //需要整条更新recipeNO
                Hashtable hsNeedInsertFeeItemList = new Hashtable(); //需要插入的新记录
                Hashtable hsNeedUpdateFeeItemList = new Hashtable(); //需要更新的旧记录(数量，金额，可退数量，确认数量等)

                Dictionary<string, string> dictionaryRecipeNOComb = new Dictionary<string, string>();

                //循环处理需要分拆的费用
                //feeItem中包含了项目名称，数量，如果是药品，还包含了项目的单位
                foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItem in feeItemList)
                {
                    decimal BalanceQty = feeItem.Item.Qty;
                    string BalanceUnit = feeItem.Item.PriceUnit;
                    //{677FBCED-9E43-4b37-9833-685A786C4D7F}
                    if (BalanceQty < 0)
                    {
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        this.RollBack();
                        this.err = "存在结算数量为负数的项目！";
                        return -1;
                    }
                    else if (BalanceQty == 0)
                    {
                        continue;
                    }

                    ArrayList ItemList = new ArrayList();
                    if (feeItem.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        ItemList = this.inpatientFeeManager.QureyMedicineByDrugCode(patientInfo.ID, feeItem.Item.ID, "0");
                    }
                    else
                    {
                        ItemList = this.inpatientFeeManager.QureyItemListByItemCode(patientInfo.ID, feeItem.Item.ID, "0");
                    }

                    foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList item in ItemList)
                    {

                        //当需要拆分的数量为0时,退出循环
                        if (BalanceQty == 0)
                        {
                            break;
                        }

                        //退费的项目
                        if (item.NoBackQty <= 0)
                        {
                            continue;
                        }

                        //拆分药品时，计价单位不相同或者价格不相同的时候不进行拆分
                        if (feeItem.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                        {
                            if (!feeItem.Item.PriceUnit.Equals(item.Item.PriceUnit) || feeItem.Item.Price != item.Item.Price)
                            {
                                continue;
                            }
                        }

                        //拆分非药品时，同一个项目属于不同的组合或者价格不相同时不进行拆分
                        if (feeItem.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.UnDrug)
                        {
                            if (!feeItem.UndrugComb.ID.Equals(item.UndrugComb.ID) || feeItem.Item.Price != item.Item.Price)
                            {
                                continue;
                            }
                        }

                        string hsKey = item.RecipeNO + item.Item.MinFee.ID + item.ExecOper.Dept.ID + item.BalanceNO;

                        if (BalanceQty >= item.NoBackQty)
                        {
                            //整条记录更新
                            if (hsNeedUpdateFeeRecipeNO.ContainsKey(hsKey))
                            {
                                ArrayList UpdateFeeRecipeNOArr = hsNeedUpdateFeeRecipeNO[hsKey] as ArrayList;
                                UpdateFeeRecipeNOArr.Add(item);
                            }
                            else
                            {
                                ArrayList UpdateFeeRecipeNOArr = new ArrayList();
                                UpdateFeeRecipeNOArr.Add(item);
                                hsNeedUpdateFeeRecipeNO.Add(hsKey, UpdateFeeRecipeNOArr);
                            }

                            //进行结算的费用项目
                            balanceFeeItemLists.Add(item);

                            TotBalance += item.FT.TotCost;
                            TotOwn += item.FT.OwnCost;
                            TotPub += item.FT.PubCost;
                            TotPay += item.FT.PayCost;
                            TotRebate += item.FT.RebateCost;
                            TotDonate += item.FT.DonateCost;

                            //减少计数
                            BalanceQty -= item.NoBackQty;
                        }
                        else
                        {
                            string ErrInfo = string.Empty;
                            FS.HISFC.Models.Fee.Inpatient.FeeItemList newItem = null;
                            if (item.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                            {
                                //拆分新费用
                                if (this.SplitMedicineItem(item, BalanceQty, ref newItem, ref ErrInfo) < 0)
                                {
                                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                                    this.RollBack();
                                    this.err = ErrInfo;
                                    return -1;
                                }
                            }
                            else
                            {
                                //拆分新费用
                                if (this.SplitUndrugItem(item, BalanceQty, ref newItem, ref ErrInfo) < 0)
                                {
                                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                                    this.RollBack();
                                    this.err = ErrInfo;
                                    return -1;
                                }
                            }

                            //需要插入的新记录
                            if (hsNeedInsertFeeItemList.ContainsKey(hsKey))
                            {
                                ArrayList InsertFeeItemListArr = hsNeedInsertFeeItemList[hsKey] as ArrayList;
                                InsertFeeItemListArr.Add(newItem);
                            }
                            else
                            {
                                ArrayList InsertFeeItemListArr = new ArrayList();
                                InsertFeeItemListArr.Add(newItem);
                                hsNeedInsertFeeItemList.Add(hsKey, InsertFeeItemListArr);
                            }

                            //进行结算的费用项目
                            balanceFeeItemLists.Add(newItem);
                            TotBalance += newItem.FT.TotCost;
                            TotOwn += newItem.FT.OwnCost;
                            TotPub += newItem.FT.PubCost;
                            TotPay += newItem.FT.PayCost;
                            TotRebate += newItem.FT.RebateCost;
                            TotDonate += newItem.FT.DonateCost;

                            //需要更新的旧记录
                            if (hsNeedUpdateFeeItemList.ContainsKey(hsKey))
                            {
                                ArrayList UpdateFeeItemListArr = hsNeedUpdateFeeItemList[hsKey] as ArrayList;
                                UpdateFeeItemListArr.Add(item);
                            }
                            else
                            {
                                ArrayList UpdateFeeItemListArr = new ArrayList();
                                UpdateFeeItemListArr.Add(item);
                                hsNeedUpdateFeeItemList.Add(hsKey, UpdateFeeItemListArr);
                            }

                            //减少计数
                            BalanceQty = 0;
                        }

                        //当需要拆分的数量为0时,退出循环
                        if (BalanceQty == 0)
                        {
                            break;
                        }
                    }

                    if (BalanceQty > 0)
                    {
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        this.RollBack();
                        this.err = "拆分项目【" + feeItem.Item.Name + "】数量不足，拆分失败！";
                        return -1;
                    }
                }

                //支付中的优惠金额小于项目已经存在的优惠金额的时候不允许通过
                if (TotRebateCost < TotRebate)
                {
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    this.RollBack();
                    this.err = "该患者此次结算的优惠金额不能少于" + TotRebate.ToString("F2") + "!";
                    return -1;
                }

                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                #endregion

                #region 处理需要整条更新的明细项目

                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在调整费用明细");
                //Hashtable hsNeedUpdateFeeRecipeNO = new Hashtable(); //需要整条更新recipeNO
                //Hashtable hsNeedInsertFeeItemList = new Hashtable(); //需要插入的新记录
                //Hashtable hsNeedUpdateFeeItemList = new Hashtable(); //需要更新的旧记录(数量，金额，可退数量，确认数量等)
                foreach (string Key in hsNeedUpdateFeeRecipeNO.Keys)
                {
                    ArrayList UpdateFeeRecipeNO = hsNeedUpdateFeeRecipeNO[Key] as ArrayList;
                    ArrayList InsertFeeItemList = new ArrayList();
                    if (hsNeedInsertFeeItemList.ContainsKey(Key))
                    {
                        InsertFeeItemList = hsNeedInsertFeeItemList[Key] as ArrayList;
                        if (InsertFeeItemList == null || InsertFeeItemList.Count == 0)
                        {
                            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                            this.RollBack();
                            this.err = "获取统计项目失败！";
                            return -1;
                        }
                    }

                    if (UpdateFeeRecipeNO == null || UpdateFeeRecipeNO.Count == 0)
                    {
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        this.RollBack();
                        this.err = "获取统计项目失败！";
                        return -1;
                    }

                    FS.HISFC.Models.Fee.Inpatient.FeeItemList firstFee = UpdateFeeRecipeNO[0] as FS.HISFC.Models.Fee.Inpatient.FeeItemList;
                    ArrayList alFeeInfo = inpatientFeeManager.QueryFeeInfoByRecipeNo(patientInfo.ID, firstFee.RecipeNO, firstFee.Item.MinFee.ID, firstFee.ExecOper.Dept.ID, 0, "0");
                    if (alFeeInfo == null || alFeeInfo.Count == 0)
                    {
                        continue;
                    }
                    FS.HISFC.Models.Fee.Inpatient.FeeInfo FeeInfoObj = alFeeInfo[0] as FS.HISFC.Models.Fee.Inpatient.FeeInfo;
                    FS.HISFC.Models.Fee.Inpatient.FeeInfo UpdateFeeInfo = FeeInfoObj.Clone();
                    FS.HISFC.Models.Fee.Inpatient.FeeInfo InsertFeeInfo = FeeInfoObj.Clone();
                    FS.HISFC.Models.Base.FT InsertFT = new FS.HISFC.Models.Base.FT();
                    InsertFeeInfo.FT = InsertFT;

                    foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList fee in UpdateFeeRecipeNO)
                    {
                        decimal DefTotCost = 0.0m;
                        if (fee.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                        {
                            DefTotCost = fee.Item.DefPrice;
                        }
                        else
                        {
                            DefTotCost = fee.Item.DefPrice * fee.Item.Qty;
                        }

                        UpdateFeeInfo.FT.TotCost -= fee.FT.TotCost;
                        UpdateFeeInfo.FT.PubCost -= fee.FT.PubCost;
                        UpdateFeeInfo.FT.PayCost -= fee.FT.PayCost;
                        UpdateFeeInfo.FT.OwnCost -= fee.FT.OwnCost;
                        UpdateFeeInfo.FT.RebateCost -= fee.FT.RebateCost;
                        UpdateFeeInfo.FT.DefTotCost -= DefTotCost;

                        InsertFeeInfo.FT.TotCost += fee.FT.TotCost;
                        InsertFeeInfo.FT.PubCost += fee.FT.PubCost;
                        InsertFeeInfo.FT.PayCost += fee.FT.PayCost;
                        InsertFeeInfo.FT.OwnCost += fee.FT.OwnCost;
                        InsertFeeInfo.FT.RebateCost += fee.FT.RebateCost;
                        InsertFeeInfo.FT.DefTotCost += DefTotCost;
                    }

                    foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList fee in InsertFeeItemList)
                    {
                        decimal DefTotCost = 0.0m;
                        if (fee.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                        {
                            DefTotCost = fee.Item.DefPrice;
                        }
                        else
                        {
                            DefTotCost = fee.Item.DefPrice * fee.Item.Qty;
                        }

                        UpdateFeeInfo.FT.TotCost -= fee.FT.TotCost;
                        UpdateFeeInfo.FT.PubCost -= fee.FT.PubCost;
                        UpdateFeeInfo.FT.PayCost -= fee.FT.PayCost;
                        UpdateFeeInfo.FT.OwnCost -= fee.FT.OwnCost;
                        UpdateFeeInfo.FT.RebateCost -= fee.FT.RebateCost;
                        UpdateFeeInfo.FT.DefTotCost -= DefTotCost;

                        InsertFeeInfo.FT.TotCost += fee.FT.TotCost;
                        InsertFeeInfo.FT.PubCost += fee.FT.PubCost;
                        InsertFeeInfo.FT.PayCost += fee.FT.PayCost;
                        InsertFeeInfo.FT.OwnCost += fee.FT.OwnCost;
                        InsertFeeInfo.FT.RebateCost += fee.FT.RebateCost;
                        InsertFeeInfo.FT.DefTotCost += DefTotCost;
                    }

                    string oldRecipeNO = firstFee.RecipeNO;
                    string newRecipeNO = string.Empty;
                    if (dictionaryRecipeNOComb.ContainsKey(oldRecipeNO))
                    {
                        newRecipeNO = dictionaryRecipeNOComb[oldRecipeNO];
                    }
                    else
                    {
                        if (firstFee.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                        {
                            newRecipeNO = this.inpatientFeeManager.GetDrugRecipeNO();
                        }
                        else
                        {
                            newRecipeNO = this.inpatientFeeManager.GetUndrugRecipeNO();
                        }

                        dictionaryRecipeNOComb.Add(oldRecipeNO, newRecipeNO);
                    }

                    FS.HISFC.Models.Fee.Inpatient.Balance balanceInfo = new FS.HISFC.Models.Fee.Inpatient.Balance();
                    balanceInfo.ID = balanceNO.ToString();
                    balanceInfo.Oper.ID = inpatientFeeManager.Operator.ID;
                    balanceInfo.Oper.OperTime = inpatientFeeManager.GetDateTimeFromSysDateTime();


                    //整条的直接更新recipeNO
                    foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList fee in UpdateFeeRecipeNO)
                    {
                        //{382350E2-DCD4-4bab-B56D-9F167937B949}
                        //if (this.inpatientFeeManager.UpdateFeeItemListRecipeNO(fee, fee.Patient.ID, fee.RecipeNO, (fee.TransType == FS.HISFC.Models.Base.TransTypes.Positive) ? 1 : 0, fee.SequenceNO.ToString(), fee.BalanceNO, "0", newRecipeNO) <= 0)
                        if (this.inpatientFeeManager.UpdateFeeItemListRecipeNO(fee, fee.Patient.ID, fee.RecipeNO, (fee.TransType == FS.HISFC.Models.Base.TransTypes.Positive) ? 1 : 2, fee.SequenceNO.ToString(), fee.BalanceNO, "0", newRecipeNO) <= 0)
                        {
                            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                            this.RollBack();
                            this.err = "更新明细费用处方号失败！";
                            return -1;
                        }

                        fee.RecipeNO = newRecipeNO;
                        if (fee.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                        {
                            //更新药品为结算状态
                            if (inpatientFeeManager.UpdateMedItemListByRecipeNoAndSeqNo(patientInfo.ID, balanceNO, invoiceNO, fee.RecipeNO, fee.SequenceNO) <= 0)
                            {
                                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                                this.RollBack();
                                this.err = inpatientFeeManager.Err;
                                return -1;
                            }
                        }
                        else
                        {
                            //更新非药品为结算状态
                            if (inpatientFeeManager.UpdateItemListByRecipeNoAndSeqNo(patientInfo.ID, balanceNO, invoiceNO, fee.RecipeNO, fee.SequenceNO) <= 0)
                            {
                                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                                this.RollBack();
                                this.err = inpatientFeeManager.Err;
                                return -1;
                            }
                        }
                    }

                    //部分的需要插入
                    foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList fee in InsertFeeItemList)
                    {
                        fee.ExtCode = oldRecipeNO;
                        fee.RecipeNO = newRecipeNO;
                        if (fee.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                        {
                            if (this.inpatientFeeManager.InsertMedItemList(fee) <= 0)
                            {
                                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                                this.RollBack();
                                this.err = "插入分解出来的新项目【" + fee.Item.Name + "】 时出现错误：" + this.inpatientFeeManager.Err;
                                return -1;
                            }

                            //更新药品为结算状态
                            if (inpatientFeeManager.UpdateMedItemListByRecipeNoAndSeqNo(patientInfo.ID, balanceNO, invoiceNO, fee.RecipeNO, fee.SequenceNO) <= 0)
                            {
                                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                                this.RollBack();
                                this.err = inpatientFeeManager.Err;
                                return -1;
                            }
                        }
                        else
                        {
                            if (this.inpatientFeeManager.InsertFeeItemList(fee) <= 0)
                            {
                                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                                this.RollBack();
                                this.err = "插入分解出来的新项目【" + fee.Item.Name + "】 时出现错误：" + this.inpatientFeeManager.Err;
                                return -1;
                            }

                            //更新非药品为结算状态
                            if (inpatientFeeManager.UpdateItemListByRecipeNoAndSeqNo(patientInfo.ID, balanceNO, invoiceNO, fee.RecipeNO, fee.SequenceNO) <= 0)
                            {
                                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                                this.RollBack();
                                this.err = inpatientFeeManager.Err;
                                return -1;
                            }
                        }
                    }

                    //{7BF7FAC4-FACC-4401-BB92-23DE8B5B698E}
                    int existZeroItem = this.inpatientFeeManager.GetFeeItemListCountByRecipeNOAndMinFeeCode(oldRecipeNO, firstFee.Item.MinFee.ID, firstFee.ExecOper.Dept.ID, "0");

                    //整条feeInfo都是需要结算，无需插入新的feeInfo,直接更新原来的
                    if (UpdateFeeInfo.FT.TotCost == 0 && existZeroItem == 0)
                    {
                        if (this.inpatientFeeManager.UpdateFeeInfoRecipeNO(FeeInfoObj, patientInfo.ID, oldRecipeNO, firstFee.Item.MinFee.ID, firstFee.ExecOper.Dept.ID, 0, "0", newRecipeNO) <= 0)
                        {
                            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                            this.RollBack();
                            this.err = "更新汇总费用处方号失败！";
                            return -1;
                        }

                        FeeInfoObj.RecipeNO = newRecipeNO;
                        FeeInfoObj.BalanceNO = balanceNO;
                        //更新汇总表结算状态
                        if (this.inpatientFeeManager.UpdateFeeInfoBalancedNew(FeeInfoObj, balanceInfo, invoiceNO) <= 0)
                        {
                            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                            this.RollBack();
                            this.err = "更新汇总费用结算状态失败！";
                            return -1;
                        }

                        FeeInfoObj.BalanceNO = balanceNO;
                        feeInfoList.Add(FeeInfoObj);
                    }
                    //{7BF7FAC4-FACC-4401-BB92-23DE8B5B698E}
                    else //需要进行拆分
                    {
                        InsertFeeInfo.Invoice.ID = invoiceNO;
                        InsertFeeInfo.BalanceNO = balanceNO;
                        InsertFeeInfo.BalanceState = "1";
                        InsertFeeInfo.BalanceOper.ID = inpatientFeeManager.Operator.ID;
                        InsertFeeInfo.BalanceOper.OperTime = inpatientFeeManager.GetDateTimeFromSysDateTime();


                        //先更新原来的记录fin_ipb_feeInfo表
                        if (inpatientFeeManager.UpdateFeeInfoForFTNew(patientInfo.ID, oldRecipeNO, firstFee.Item.MinFee.ID, firstFee.ExecOper.Dept.ID, 0, "0", InsertFeeInfo.FT) <= 0)
                        {
                            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                            this.RollBack();
                            this.err = "更新费用汇总费用信息失败！";
                            return -1;
                        }

                        //插入新的费用汇总记录表，插入记录已直接设置为已结算状态
                        InsertFeeInfo.RecipeNO = newRecipeNO;
                        InsertFeeInfo.ExtCode = oldRecipeNO;
                        if (this.inpatientFeeManager.InsertFeeInfo(patientInfo, InsertFeeInfo) <= 0)
                        {
                            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                            this.RollBack();
                            this.err = "插入分解出来的新汇总费时出现错误：" + this.inpatientFeeManager.Err;
                            return -1;
                        }

                        InsertFeeInfo.BalanceNO = balanceNO;
                        feeInfoList.Add(InsertFeeInfo);
                    }
                }
                #endregion

                #region 处理需要插入的项目
                foreach (string Key in hsNeedInsertFeeItemList.Keys)
                {
                    //如果此key包含在需要更新RecipNO中的话，说明此条记录已被处理
                    if (hsNeedUpdateFeeRecipeNO.ContainsKey(Key))
                    {
                        continue;
                    }
                    //这些是拆分出来的细项，需要生成新的recipeNO进行插入
                    else
                    {
                        ArrayList feeArr = hsNeedInsertFeeItemList[Key] as ArrayList;
                        FS.HISFC.Models.Base.FT InsertFT = new FS.HISFC.Models.Base.FT();

                        FS.HISFC.Models.Fee.Inpatient.FeeItemList firstFee = feeArr[0] as FS.HISFC.Models.Fee.Inpatient.FeeItemList;
                        string oldRecipeNO = firstFee.RecipeNO;
                        string newRecipeNO = string.Empty;

                        foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList fee in feeArr)
                        {

                            if (dictionaryRecipeNOComb.ContainsKey(oldRecipeNO))
                            {
                                newRecipeNO = dictionaryRecipeNOComb[oldRecipeNO];
                            }
                            else
                            {
                                if (fee.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                                {
                                    newRecipeNO = this.inpatientFeeManager.GetDrugRecipeNO();
                                }
                                else
                                {
                                    newRecipeNO = this.inpatientFeeManager.GetUndrugRecipeNO();
                                }

                                dictionaryRecipeNOComb.Add(oldRecipeNO, newRecipeNO);
                            }

                            fee.RecipeNO = newRecipeNO;
                            fee.ExtCode = oldRecipeNO;
                            if (fee.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                            {
                                fee.RecipeNO = newRecipeNO;
                                if (this.inpatientFeeManager.InsertMedItemList(fee) <= 0)
                                {
                                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                                    this.RollBack();
                                    this.err = "插入分解出来的新项目【" + fee.Item.Name + "】 时出现错误：" + this.inpatientFeeManager.Err;
                                    return -1;
                                }

                                //更新药品为结算状态
                                if (inpatientFeeManager.UpdateMedItemListByRecipeNoAndSeqNo(patientInfo.ID, balanceNO, invoiceNO, fee.RecipeNO, fee.SequenceNO) <= 0)
                                {
                                    this.RollBack();
                                    this.err = inpatientFeeManager.Err;
                                    return -1;
                                }
                            }
                            else
                            {
                                if (this.inpatientFeeManager.InsertFeeItemList(fee) <= 0)
                                {
                                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                                    this.RollBack();
                                    this.err = "插入分解出来的新项目【" + fee.Item.Name + "】 时出现错误：" + this.inpatientFeeManager.Err;
                                    return -1;
                                }

                                //更新非药品为结算状态
                                if (inpatientFeeManager.UpdateItemListByRecipeNoAndSeqNo(patientInfo.ID, balanceNO, invoiceNO, fee.RecipeNO, fee.SequenceNO) <= 0)
                                {
                                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                                    this.RollBack();
                                    this.err = inpatientFeeManager.Err;
                                    return -1;
                                }

                            }

                            InsertFT.TotCost += fee.FT.TotCost;
                            InsertFT.PubCost += fee.FT.PubCost;
                            InsertFT.PayCost += fee.FT.PayCost;
                            InsertFT.OwnCost += fee.FT.OwnCost;
                            InsertFT.RebateCost += fee.FT.RebateCost;
                            InsertFT.DefTotCost += fee.FT.DefTotCost;
                        }

                        FS.HISFC.Models.Fee.Inpatient.FeeItemList cloneFee = firstFee.Clone();
                        cloneFee.RecipeNO = newRecipeNO;
                        cloneFee.Invoice.ID = invoiceNO;
                        cloneFee.BalanceNO = balanceNO;
                        cloneFee.BalanceState = "1";
                        cloneFee.BalanceOper.ID = inpatientFeeManager.Operator.ID;
                        cloneFee.BalanceOper.OperTime = inpatientFeeManager.GetDateTimeFromSysDateTime();
                        cloneFee.FT = InsertFT;

                        FS.HISFC.Models.Fee.Inpatient.FeeInfo InsertfeeInfoInsert = this.inpatientFeeManager.ConvertFeeItemListToFeeInfoNew(cloneFee);
                        cloneFee.ExtCode = oldRecipeNO;
                        //插入新的汇总，直接为结算状态
                        if (this.inpatientFeeManager.InsertFeeInfo(patientInfo, InsertfeeInfoInsert) <= 0)
                        {
                            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                            this.RollBack();
                            this.err = "插入分解出来的新汇总费时出现错误：" + this.inpatientFeeManager.Err;
                            return -1;
                        }

                        //更新原来的汇总费用
                        if (this.inpatientFeeManager.UpdateFeeInfoForFTNew(InsertfeeInfoInsert.Patient.ID, oldRecipeNO, firstFee.Item.MinFee.ID, firstFee.ExecOper.Dept.ID, 0, "0", InsertfeeInfoInsert.FT) <= 0)
                        {
                            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                            this.RollBack();
                            this.err = "更新旧汇总费时出现错误：" + this.inpatientFeeManager.Err;
                            return -1;
                        }
                        InsertfeeInfoInsert.BalanceNO = balanceNO;
                        feeInfoList.Add(InsertfeeInfoInsert);

                    }
                }
                #endregion

                #region 处理被拆分的需要更新的项目
                //这些是拆分出来的旧项目，需要进行更新FT和确认数量以及可退数量
                foreach (string Key in hsNeedUpdateFeeItemList.Keys)
                {
                    ArrayList UpdateFeeArr = hsNeedUpdateFeeItemList[Key] as ArrayList;

                    foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList fee in UpdateFeeArr)
                    {
                        //药品表没有可退数量，费药品表没有原价总金额字段
                        if (fee.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                        {
                            //更新可退数量
                            if (this.inpatientFeeManager.UpdateNoBackQtyForDrugNew(fee.RecipeNO, fee.SequenceNO, fee.NoBackQty, "0", (fee.TransType == FS.HISFC.Models.Base.TransTypes.Positive) ? 1 : 2) <= 0)
                            {
                                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                                this.RollBack();
                                this.err = "更新项目【" + fee.Item.Name + "】 可退数量时出现错误：" + this.inpatientFeeManager.Err;
                                return -1;
                            }

                            //更新原价总金额
                            if (this.inpatientFeeManager.UpdateDefTotCostForDrug(fee.Patient.ID, fee.RecipeNO, (fee.TransType == FS.HISFC.Models.Base.TransTypes.Positive) ? 1 : 2, fee.SequenceNO, "0", "0", fee.FT.DefTotCost) <= 0)
                            {
                                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                                this.RollBack();
                                this.err = "更新项目【" + fee.Item.Name + "】 可退数量时出现错误：" + this.inpatientFeeManager.Err;
                                return -1;
                            }
                        }
                        else
                        {
                            //更新可退数量
                            if (this.inpatientFeeManager.UpdateNoBackQtyForUndrugNew(fee.RecipeNO, fee.SequenceNO, fee.NoBackQty, "0", (fee.TransType == FS.HISFC.Models.Base.TransTypes.Positive) ? 1 : 2) <= 0)
                            {
                                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                                this.RollBack();
                                this.err = "更新项目【" + fee.Item.Name + "】 可退数量时出现错误：" + this.inpatientFeeManager.Err;
                                return -1;
                            }

                            //更新确认数量
                            if (this.inpatientFeeManager.UpdateConfirmNumForUndrugNew(fee.RecipeNO, fee.SequenceNO, fee.ConfirmedQty, "0", (fee.TransType == FS.HISFC.Models.Base.TransTypes.Positive) ? 1 : 2) <= 0)
                            {
                                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                                this.RollBack();
                                this.err = "更新项目【" + fee.Item.Name + "】 确认数量时出现错误：" + this.inpatientFeeManager.Err;
                                return -1;
                            }
                        }

                        //更新费用信息
                        if (this.inpatientFeeManager.UpdateChargeInfo(fee) <= 0)
                        {
                            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                            this.RollBack();
                            this.err = "更新项目【" + fee.Item.Name + "】 费用信息时出现错误：" + this.inpatientFeeManager.Err;
                            return -1;
                        }
                    }
                }
                #endregion

                #region 分配收费明细实收赠送优惠金额

                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在分配实收金额,赠送金额，优惠金额至明细项目...");
                //TotRebateCost为某些项目单独享受的优惠，在平分优惠金额时
                //需要将此部分优惠金额区分开来，已经优惠的项目，权重为ownCost - debateCost
                TotCostForCt -= TotRebate;
                TotCostForSt -= TotRebate;
                TotRebateCostForCt -= TotRebate;
                TotRebateCostForSt -= TotRebate;

                for (int i = 0; i < balanceFeeItemLists.Count; i++)
                {
                    FS.HISFC.Models.Fee.Inpatient.FeeItemList itor = balanceFeeItemLists[i];
                    decimal weight = itor.FT.OwnCost - itor.FT.RebateCost;
                    //整体优惠后不再处理折扣
                    if (weight == 0)
                    {
                        continue;
                    }

                    decimal realCost = 0.0m;
                    decimal donateCost = 0.0m;
                    decimal rebateCost = 0.0m;

                    //最后一个项目为实际金额，优惠金额，赠送金额分别是余下的费用不在进行计算
                    if (i == balanceFeeItemLists.Count - 1)
                    {
                        realCost = TotRealCostForCt;
                        donateCost = TotDonateCostForCt;
                        rebateCost = TotRebateCostForCt;
                    }
                    else
                    {
                        //总是直接舍去小数点后两位以后的数值，以防止最后一个项目的价格出现负数
                        realCost = Math.Floor((weight * TotRealCostForSt * 100) / TotCostForSt) / 100;
                        donateCost = Math.Floor((weight * TotDonateCostForSt * 100) / TotCostForSt) / 100;
                        rebateCost = Math.Floor((weight * TotRebateCostForSt * 100) / TotCostForSt) / 100;
                    }

                    //因为直接切断小数点后两位的小数，不够的是先从TotRealCostForCt扣
                    //再从TotDonateCostForCt扣，再从TotRebateCostForCt扣,为了防止出现
                    //TotRealCostForCt或者TotDonateCostForCt小于零的情况
                    #region 小数处理

                    decimal deci = 0.0m;
                    if (realCost > TotRealCostForCt)
                    {
                        realCost = TotRealCostForCt;
                        deci = realCost - TotRealCostForCt;
                        TotRealCostForCt = 0;
                    }
                    else
                    {
                        TotRealCostForCt -= realCost;
                    }

                    if (donateCost + deci > TotDonateCostForCt)
                    {
                        donateCost = TotDonateCostForCt;
                        deci = donateCost + deci - TotDonateCostForCt;
                        TotDonateCostForCt = 0;
                    }
                    else
                    {
                        donateCost += deci;
                        TotDonateCostForCt -= donateCost;
                        deci = 0;
                    }

                    if (rebateCost + deci > TotRebateCostForCt)
                    {
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        this.RollBack();
                        this.err = "分配实收金额赠送金额优惠金额至细项时发生错误";
                        return -1;
                    }
                    else
                    {
                        rebateCost += deci;
                        TotRebateCostForCt -= rebateCost;
                    }

                    if (TotRealCostForCt < 0 || TotDonateCostForCt < 0 || TotRebateCostForCt < 0)
                    {
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        this.RollBack();
                        this.err = "分配实收金额赠送金额优惠金额至细项时发生错误";
                        return -1;
                    }

                    //当总价不等于优惠金额+实收金额+赠送金额的时候
                    //从实收金额开始扣余下部分直至扣费完全
                    if (weight > realCost + donateCost + rebateCost)
                    {
                        decimal diff = weight - realCost - donateCost - rebateCost;

                        if (TotRealCostForCt + TotDonateCostForCt + TotRebateCostForCt >= diff)
                        {
                            if (TotRealCostForCt >= diff)
                            {
                                realCost += diff;
                                TotRealCostForCt -= diff;
                            }
                            else
                            {
                                realCost += TotRealCostForCt;
                                diff -= TotRealCostForCt;
                                TotRealCostForCt = 0;

                                if (TotDonateCostForCt >= diff)
                                {
                                    TotDonateCostForCt -= diff;
                                    donateCost += diff;
                                }
                                else
                                {

                                    donateCost += TotDonateCostForCt;
                                    diff -= TotDonateCostForCt;
                                    TotDonateCostForCt = 0;

                                    if (TotRebateCostForCt >= diff)
                                    {
                                        TotRebateCostForCt -= diff;
                                        rebateCost += diff;
                                    }
                                    else
                                    {
                                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                                        this.RollBack();
                                        this.err = "分配实收金额赠送金额优惠金额至细项时发生错误";
                                        return -1;
                                    }
                                }
                            }
                        }
                        else
                        {
                            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                            this.RollBack();
                            this.err = "分配实收金额赠送金额优惠金额至细项时发生错误";
                            return -1;
                        }
                    }

                    itor.FT.OwnCost = realCost + donateCost + rebateCost + itor.FT.RebateCost;
                    itor.FT.DonateCost = donateCost;
                    itor.FT.RebateCost += rebateCost;

                    #endregion
                }
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                #endregion

                #region 设置主费用的实收赠送优惠金额

                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在分配实收金额,赠送金额，优惠金额至费用汇总表...");
                foreach (FS.HISFC.Models.Fee.Inpatient.FeeInfo feeItem in feeInfoList)
                {
                    feeItem.FT.OwnCost = 0.0m;
                    feeItem.FT.DonateCost = 0.0m;
                    feeItem.FT.RebateCost = 0.0m;
                    foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList item in balanceFeeItemLists)
                    {
                        if (item.RecipeNO == feeItem.RecipeNO &&
                           item.Item.MinFee.ID == feeItem.Item.MinFee.ID &&
                           item.ExecOper.Dept.ID == feeItem.ExecOper.Dept.ID)
                        {
                            feeItem.FT.OwnCost += item.FT.OwnCost;
                            feeItem.FT.DonateCost += item.FT.DonateCost;
                            feeItem.FT.RebateCost += item.FT.RebateCost;
                        }
                    }

                    if (feeItem.FT.TotCost != feeItem.FT.PayCost + feeItem.FT.PubCost + feeItem.FT.OwnCost)
                    {
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        this.RollBack();
                        this.err = "费用汇总表条目实收优惠赠送金额信息与总金额不符";
                        return -1;
                    }
                }
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                #endregion

                #region 更新明细表和主表的实收优惠赠送金额

                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在更新费用至数据库...");
                //更新主表
                foreach (FS.HISFC.Models.Fee.Inpatient.FeeInfo feeItem in feeInfoList)
                {
                    //更新费用信息
                    feeItem.PackageFlag = packageBalance ? "1" : "0";
                    if (this.inpatientFeeManager.UpdateChargeFeeInfo(feeItem) <= 0)
                    {
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        this.RollBack();
                        this.err = "更新费用汇总表实收优惠赠送金额信息时出现错误：" + this.inpatientFeeManager.Err;
                        return -1;
                    }
                }

                //更新明细表
                foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList item in balanceFeeItemLists)
                {
                    //更新费用信息
                    item.PackageFlag = packageBalance ? "1" : "0";
                    if (this.inpatientFeeManager.UpdateChargeInfo(item) != 1)
                    {
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        this.RollBack();
                        this.err = "更新项目【" + item.Item.Name + "】 实收优惠赠送金额信息时出现错误：" + this.inpatientFeeManager.Err;
                        return -1;
                    }
                }

                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                #endregion

                //{677FBCED-9E43-4b37-9833-685A786C4D7F}
                #region 处理收费之后又进行退费的项目(只有在出院结算的时候才处理这部分数据)
                if (balanceType == FS.HISFC.Models.Base.EBlanceType.Out)
                {
                    int result = inpatientFeeManager.UpdateFeeInfoBalanced(patientInfo.ID, balanceNO, sysdate, invoiceNO, false);

                    if (result < 0)
                    {
                        this.RollBack();
                        this.err = inpatientFeeManager.Err;
                        return -1;
                    }

                    int itemResult = inpatientFeeManager.UpdateFeeItemListBalanced(patientInfo.ID, balanceNO, invoiceNO, false);

                    if (itemResult < 0)
                    {
                        this.RollBack();
                        this.err = inpatientFeeManager.Err;
                        return -1;
                    }

                    int medResult = inpatientFeeManager.UpdateMedItemListBalanced(patientInfo.ID, balanceNO, invoiceNO);

                    if (medResult < 0)
                    {
                        this.RollBack();
                        this.err = inpatientFeeManager.Err;
                        return -1;
                    }

                    //{C21CA7FF-1DFB-481b-BD7F-E5C86ED30DF6}
                    //if (result == 0 && (medResult > 0 || itemResult > 0))
                    //{
                    //    this.RollBack();
                    //    this.err = "可能存在并发操作导致更新费用失败！";
                    //    return -1;
                    //}
                }
                #endregion

                //更新转入费用
                if (inpatientFeeManager.UpdateAllChangeCostBalanced(patientInfo.ID, balanceNO) == -1)
                {
                    this.RollBack();
                    this.err = inpatientFeeManager.Err;
                    return -1;
                }

                #endregion

                #region 处理支付方式

                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在处理支付方式...");

                //会员支付+会员代付
                Dictionary<string, List<BalancePay>> dictAcc = new Dictionary<string, List<BalancePay>>();
                string payInvoiceNo = string.Empty;

                decimal TotBalancePayCost = 0.00M;
                decimal couponCostAmount = 0.0m;
                decimal operateCouponAmount = 0.0m;
                if (balancePayList != null && balancePayList.Count > 0)
                {
                    //{9F2C17EC-0AE6-4df8-B959-F4D99314E227}
                    FS.HISFC.BizLogic.Manager.Constant constManager = new FS.HISFC.BizLogic.Manager.Constant();
                    //FS.FrameWork.Models.NeuObject obj = constManager.GetConstant("JFZFFS", "1");

                    //{F166B18B-62E3-4835-A729-4CA384F9ADEE}
                    FS.FrameWork.Models.NeuObject cashCouponPayMode = constManager.GetConstant("XJLZFFS", "1");


                    FS.HISFC.BizProcess.Integrate.Account.AccountPay accountPay = new FS.HISFC.BizProcess.Integrate.Account.AccountPay();

                    foreach (FS.HISFC.Models.Fee.Inpatient.BalancePay balancePay in balancePayList)
                    {
                        bool isReturnPrepay = false;

                        //判断是否是退回预交款
                        if (balancePay.TransKind.ID == "1" && balancePay.RetrunOrSupplyFlag == "2")
                        {
                            isReturnPrepay = true;
                        }

                        //本地积分功能已停用
                        //{9F2C17EC-0AE6-4df8-B959-F4D99314E227}
                        //判断该支付方式是否计算积分
                        //if (obj.Name.Contains(balancePay.PayType.ID.ToString()))
                        //{
                        //    if (accountPay.UpdateCoupon(patientInfo.PID.CardNO, isReturnPrepay ? -balancePay.FT.TotCost : balancePay.FT.TotCost, balancePay.Invoice.ID.ToString()) <= 0)
                        //    {
                        //        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        //        this.RollBack();
                        //        this.err = accountPay.Err;
                        //        return -1;
                        //    }
                        //}

                        //{333D2AD8-DC4A-4c30-A14E-D6815AC858F9}
                        if (balancePay.PayType.ID == "CO")
                        {
                            if (isReturnPrepay)
                            {
                                couponCostAmount -= balancePay.FT.TotCost;
                            }
                            else
                            {
                                couponCostAmount += balancePay.FT.TotCost;
                            }
                        }

                        //{F166B18B-62E3-4835-A729-4CA384F9ADEE}
                        //判断该支付方式是否是现金流支付方式
                        if (cashCouponPayMode.Name.Contains(balancePay.PayType.ID.ToString()))
                        {
                            if (isReturnPrepay)
                            {
                                operateCouponAmount -= balancePay.FT.TotCost;
                            }
                            else
                            {
                                operateCouponAmount += balancePay.FT.TotCost;
                            }
                        }

                        TotBalancePayCost += balancePay.FT.TotCost;

                        balancePay.Invoice.ID = invoiceNO;
                        balancePay.BalanceNO = balanceNO;
                        balancePay.BalanceOper.ID = inpatientFeeManager.Operator.ID;
                        balancePay.BalanceOper.OperTime = sysdate;
                        //添加记录////{B7E5CA30-EC53-4fea-BB60-55B8D8DE3CAB} 添加了备注信息
                        if (inpatientFeeManager.InsertBalancePay1(balancePay) == -1)
                        {
                            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                            this.RollBack();
                            this.err = inpatientFeeManager.Err;
                            return -1;
                        }

                        payInvoiceNo = balancePay.Invoice.ID;
                        //会员支付+会员代付处理
                        if ((!string.IsNullOrEmpty(balancePay.AccountNo) && !string.IsNullOrEmpty(balancePay.AccountTypeCode)) ||
                            balancePay.PayType.ID == "YS" || balancePay.PayType.ID == "DC")
                        {
                            string key = balancePay.AccountNo + "-" + balancePay.AccountTypeCode;
                            if (dictAcc.ContainsKey(key))
                            {
                                List<BalancePay> bpList = dictAcc[key];
                                bpList.Add(balancePay);
                            }
                            else
                            {
                                List<BalancePay> bpList = new List<BalancePay>();
                                bpList.Add(balancePay);
                                dictAcc.Add(key, bpList);
                            }
                        }

                        #region 废弃

                        ////补缴款采取进行扣费
                        //if (balancePay.TransKind.ID == "1" && (balancePay.PayType.ID == "YS" || balancePay.PayType.ID == "DC"))
                        //{
                        //    FS.HISFC.BizProcess.Integrate.Account.AccountPay accountPay = new FS.HISFC.BizProcess.Integrate.Account.AccountPay();
                        //    FS.HISFC.Models.RADT.PatientInfo empPatient = new FS.HISFC.Models.RADT.PatientInfo();
                        //    FS.HISFC.Models.Account.AccountDetail accountDetail = new FS.HISFC.Models.Account.AccountDetail();
                        //    List<FS.HISFC.Models.Account.AccountDetail> accountDetailList = new List<FS.HISFC.Models.Account.AccountDetail>();
                        //    if (string.IsNullOrEmpty(balancePay.AccountNo))
                        //    {
                        //        this.RollBack();
                        //        this.err = "账户扣费失败，账户支付时账户为空！";
                        //        return -1;
                        //    }

                        //    //{62DEB6B4-8656-419e-A106-C9EC7BF181BB}
                        //    accountDetailList = accountManager.GetAccountDetail(balancePay.AccountNo, balancePay.AccountTypeCode, "1");

                        //    if (accountDetailList.Count > 0)
                        //    {
                        //        accountDetail = accountDetailList[0] as FS.HISFC.Models.Account.AccountDetail;

                        //        empPatient = accountManager.GetPatientInfoByCardNO(accountDetail.CardNO);
                        //        int returnValue1 = accountPay.OutpatientPay(patientInfo,
                        //                                                    balancePay.AccountNo,
                        //                                                    balancePay.AccountTypeCode,
                        //                                                    balancePay.PayType.ID == "YS" ? -balancePay.FT.TotCost : 0,
                        //                                                    balancePay.PayType.ID == "DC" ? -balancePay.FT.TotCost : 0,
                        //                                                    balancePay.Invoice.ID,
                        //                                                    empPatient,
                        //                                                    FS.HISFC.Models.Account.PayWayTypes.I, 1);
                        //        if (returnValue1 < 0)
                        //        {
                        //            this.RollBack();
                        //            this.err = "账户扣费失败:" + accountPay.Err;
                        //            return -1;
                        //        }
                        //    }
                        //    else
                        //    {
                        //        this.RollBack();
                        //        this.err = "账户扣费失败:未找到账户信息";
                        //        return -1;
                        //    }
                        //}

                        #endregion
                    }

                    //if (operateCouponAmount > 0 || operateCouponAmount < 0)
                    //{
                    //    string errInfo = string.Empty;
                    //    FS.HISFC.BizProcess.Integrate.Account.CashCoupon cashCouponPrc = new FS.HISFC.BizProcess.Integrate.Account.CashCoupon();
                    //    if (cashCouponPrc.CashCouponSave("ZYSF", patientInfo.PID.CardNO, payInvoiceNo, operateCouponAmount, ref errInfo) <= 0)
                    //    {
                    //        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    //        this.RollBack();
                    //        this.err = "计算现金流积分出错!" + errInfo;
                    //        return -1;
                    //    }

                    //}
                }

                #region 账户扣费

                if (dictAcc != null && dictAcc.Count > 0)
                {
                    FS.HISFC.BizProcess.Integrate.Account.AccountPay accountPay = new FS.HISFC.BizProcess.Integrate.Account.AccountPay();

                    //结算患者
                    FS.HISFC.Models.RADT.PatientInfo selfPatient = accountManager.GetPatientInfoByCardNO(patientInfo.PID.CardNO);
                    if (selfPatient == null || string.IsNullOrEmpty(selfPatient.PID.CardNO))
                    {
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        this.RollBack();
                        this.err = accountManager.Err + "\r\n查询患者基本信息失败!";
                        return -1;
                    }

                    //会员患者
                    FS.HISFC.Models.RADT.PatientInfo empPatient = new FS.HISFC.Models.RADT.PatientInfo();

                    foreach (List<BalancePay> bpList in dictAcc.Values)
                    {
                        decimal baseCost = 0;                    //基本账户金额
                        decimal donateCost = 0;                  //赠送账户金额

                        BalancePay bp = new BalancePay();
                        for (int k = 0; k < bpList.Count; k++)
                        {
                            bp = bpList[k];
                            if (bp.PayType.ID == "YS")
                            {
                                baseCost -= bp.FT.TotCost;
                            }
                            else if (bp.PayType.ID.ToString() == "DC")
                            {
                                donateCost -= bp.FT.TotCost;
                            }
                        }

                        string accountNo = bp.AccountNo;      //账户
                        string accountTypeCode = bp.AccountTypeCode;   //账户类型
                        List<AccountDetail> accLists = accountManager.GetAccountDetail(accountNo, accountTypeCode, "1");
                        if (accLists == null || accLists.Count <= 0)
                        {
                            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                            this.RollBack();
                            this.err = accountManager.Err + "\r\n查找账户失败!";
                            return -1;
                        }
                        AccountDetail detailAcc = accLists[0];
                        if (Math.Abs(baseCost) > detailAcc.BaseVacancy)
                        {
                            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                            this.RollBack();
                            this.err = string.Format("会员账户中基本账户余额不足！\r\n需缴费：{0}元；基本账户余额：{1}元", Math.Abs(baseCost).ToString("F2"), detailAcc.BaseVacancy.ToString("F2"));
                            return -1;
                        }
                        if (Math.Abs(donateCost) > detailAcc.DonateVacancy)
                        {
                            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                            this.RollBack();
                            this.err = string.Format("会员账户中赠送账户余额不足！\r\n需缴费：{0}元；基本账户余额：{1}元", Math.Abs(donateCost).ToString("F2"), detailAcc.DonateVacancy.ToString("F2"));
                            return -1;
                        }

                        if (bp.IsEmpPay)
                        {
                            empPatient = accountManager.GetPatientInfoByCardNO(detailAcc.CardNO);
                            if (empPatient == null || string.IsNullOrEmpty(empPatient.PID.CardNO))
                            {
                                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                                this.RollBack();
                                this.err = string.Format("查找授权患者【{0}】基本信息失败!", detailAcc.CardNO);
                                return -1;
                            }
                        }
                        else
                        {
                            empPatient = selfPatient;
                        }

                        //会员账户结算
                        int returnValue = accountPay.OutpatientPay(selfPatient, accountNo, accountTypeCode, baseCost, donateCost, payInvoiceNo, empPatient, PayWayTypes.I, 1);
                        if (returnValue < 0)
                        {
                            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                            this.RollBack();
                            this.err = "账户结算出错!" + accountPay.Err;
                            return -1;
                        }

                    }
                }

                #endregion

                //欠费金额
                decimal arrearFeeCost = TotBalance - TotBalancePayCost;
                arrearFeeCost = arrearFeeCost > 0 ? arrearFeeCost : 0;

                //欠费金额等于结算金额-支付方式金额
                if (arrearFeeCost > 0 && balanceType == FS.HISFC.Models.Base.EBlanceType.Owe)
                {
                    FS.HISFC.Models.Fee.Inpatient.BalancePay balancePay = new FS.HISFC.Models.Fee.Inpatient.BalancePay();
                    balancePay.PayType.ID = "QF";
                    balancePay.PayType.Name = "欠费";
                    balancePay.FT.TotCost = arrearFeeCost;
                    balancePay.TransKind.ID = "1";
                    balancePay.TransType = FS.HISFC.Models.Base.TransTypes.Positive;
                    balancePay.Qty = 1;
                    balancePay.RetrunOrSupplyFlag = "1";//欠费
                    balancePay.Invoice.ID = invoiceNO;
                    balancePay.BalanceNO = balanceNO;
                    balancePay.BalanceOper.ID = inpatientFeeManager.Operator.ID;
                    balancePay.BalanceOper.OperTime = sysdate;
                    //添加记录
                    if (inpatientFeeManager.InsertBalancePay(balancePay) == -1)
                    {
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        this.RollBack();
                        this.err = inpatientFeeManager.Err;
                        return -1;
                    }
                }

                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

                #endregion

                #region 处理套餐消费

                foreach (FS.HISFC.Models.MedicalPackage.Fee.Package package in packageList)
                {
                    if (package == null || string.IsNullOrEmpty(package.ID))
                    {
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        this.RollBack();
                        return -1;
                    }
                    FS.HISFC.Models.MedicalPackage.Fee.Package freshPack = feepackManager.QueryByID(package.ID);

                    if (freshPack.Cancel_Flag != "0")
                    {
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        this.RollBack();
                        this.err = "套餐已退费，请刷新后重新匹配";//{795AA18A-0093-492b-AAB9-DE654606A309}
                        return -1;
                    }
                    if (this.inpatientFeeManager.UpdatePackageCostInfo(package.ID, "1", invoiceNO) <= 0)
                    {
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        this.RollBack();
                        this.err = inpatientFeeManager.Err;
                        return -1;
                    }
                    //套餐支付结算//{795AA18A-0093-492b-AAB9-DE654606A309}
                    FS.HISFC.BizLogic.MedicalPackage.Fee.PackageDetail dtailManger = new FS.HISFC.BizLogic.MedicalPackage.Fee.PackageDetail();
                    ArrayList alPack = dtailManger.QueryDetailByClinicNO(package.ID, "0");

                    if (this.packagecostMange.NewCostPackageDetailByType(alPack, invoiceNO, patientInfo, "ZY", patientInfo.ID, ref err) <= 0)
                    {
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        this.RollBack();
                        this.err = inpatientFeeManager.Err;
                        return -1;
                    }
                }

                #endregion

                #region 处理发票信息

                string invoiceType = "ZY01";

                FS.HISFC.BizLogic.Fee.FeeCodeStat feeCodeStat = new FS.HISFC.BizLogic.Fee.FeeCodeStat();
                ArrayList alFeeState = feeCodeStat.QueryFeeCodeStatByReportCode(invoiceType);
                if (alFeeState == null)
                {
                    this.RollBack();
                    this.err = "结算失败，原因：" + feeCodeStat.Err;
                    return -1;
                }

                #region 欠费发票

                List<FS.HISFC.Models.Fee.Inpatient.BalanceList> alArrearBalanceList = new List<FS.HISFC.Models.Fee.Inpatient.BalanceList>();
                if (arrearFeeCost > 0 && balanceType == FS.HISFC.Models.Base.EBlanceType.OweMid)
                {
                    ArrayList alArrearManualFeeInfo = new ArrayList();
                    decimal arrearFeeCostTemp = arrearFeeCost;

                    decimal splitRate = arrearFeeCost / TotBalance;
                    if (splitRate > 1)
                    {
                        this.RollBack();
                        this.err = "欠费金额大于结算金额";
                        return -1;
                    }
                    //循环处理分票明细及主发票明细
                    for (int i = 0; i < feeInfoList.Count; i++)
                    {
                        FS.HISFC.Models.Fee.Inpatient.FeeInfo InvoiceHeadInfo = feeInfoList[i];
                        FS.HISFC.Models.Fee.Inpatient.FeeInfo InvoiceManualInfo = InvoiceHeadInfo.Clone();

                        InvoiceManualInfo.FT.OwnCost = FS.FrameWork.Function.NConvert.ToDecimal(FS.FrameWork.Public.String.FormatNumberReturnString(InvoiceHeadInfo.FT.OwnCost * splitRate, 2));
                        InvoiceManualInfo.FT.PayCost = 0.00M;
                        InvoiceManualInfo.FT.PubCost = 0.00M;
                        InvoiceManualInfo.FT.RebateCost = 0.00M;

                        InvoiceManualInfo.FT.TotCost = InvoiceManualInfo.FT.OwnCost + InvoiceManualInfo.FT.PayCost + InvoiceManualInfo.FT.PubCost + InvoiceManualInfo.FT.RebateCost;

                        InvoiceHeadInfo.FT.TotCost = InvoiceHeadInfo.FT.TotCost - InvoiceManualInfo.FT.TotCost;
                        InvoiceHeadInfo.FT.OwnCost = InvoiceHeadInfo.FT.OwnCost - InvoiceManualInfo.FT.OwnCost;
                        InvoiceHeadInfo.FT.PayCost = InvoiceHeadInfo.FT.PayCost - InvoiceManualInfo.FT.PayCost;
                        InvoiceHeadInfo.FT.PubCost = InvoiceHeadInfo.FT.PubCost - InvoiceManualInfo.FT.PubCost;
                        InvoiceHeadInfo.FT.RebateCost = InvoiceHeadInfo.FT.RebateCost - InvoiceManualInfo.FT.RebateCost;
                        arrearFeeCostTemp -= InvoiceManualInfo.FT.TotCost;

                        alArrearManualFeeInfo.Add(InvoiceManualInfo);
                    }

                    //如果没有分完整，平小数位
                    //如果没有分完整，平小数位
                    if (Math.Abs(arrearFeeCostTemp) > 0)
                    {
                        for (int i = 0; i < feeInfoList.Count; i++)
                        {
                            if (feeInfoList[i].FT.OwnCost + arrearFeeCostTemp > 0)
                            {
                                FS.HISFC.Models.Fee.Inpatient.FeeInfo InvoiceManualInfo = feeInfoList[i].Clone();
                                InvoiceManualInfo.FT.OwnCost = arrearFeeCostTemp;
                                InvoiceManualInfo.FT.PayCost = 0.00M;
                                InvoiceManualInfo.FT.PubCost = 0.00M;
                                InvoiceManualInfo.FT.RebateCost = 0.00M;
                                InvoiceManualInfo.FT.TotCost = arrearFeeCostTemp;
                                feeInfoList[i].FT.OwnCost -= arrearFeeCostTemp;
                                feeInfoList[i].FT.TotCost -= arrearFeeCostTemp;
                                alArrearManualFeeInfo.Add(InvoiceManualInfo);
                                break;
                            }
                        }
                    }

                    if (this.FeeInfoTransFeeStat(patientInfo, balanceType, alArrearManualFeeInfo, alArrearBalanceList, sysdate, balanceNO, "", alFeeState) == -1)
                    {
                        this.RollBack();
                        return -1;
                    }

                }

                #endregion


                #region 优惠发票

                List<FS.HISFC.Models.Fee.Inpatient.BalanceList> alBalanceListRebate = new List<FS.HISFC.Models.Fee.Inpatient.BalanceList>();
                bool IsRebatePrint = controlParamMgr.GetControlParam<bool>("100007");
                //判断优惠是否单独打印发票
                if (IsRebatePrint)
                {
                    ArrayList alInvoiceRebateFeeInfo = new ArrayList();
                    for (int i = 0; i < feeInfoList.Count; i++)
                    {
                        FS.HISFC.Models.Fee.Inpatient.FeeInfo f = feeInfoList[i];
                        if (f.FT.RebateCost > 0)
                        {
                            FS.HISFC.Models.Fee.Inpatient.FeeInfo FeeInfoRebate = new FS.HISFC.Models.Fee.Inpatient.FeeInfo();
                            FeeInfoRebate.Item.MinFee.ID = f.Item.MinFee.ID;
                            FeeInfoRebate.FT.RebateCost = f.FT.RebateCost;
                            FeeInfoRebate.FT.TotCost = f.FT.TotCost;
                            alInvoiceRebateFeeInfo.Add(FeeInfoRebate);
                        }
                    }

                    if (this.FeeInfoTransFeeStat(patientInfo, balanceType, alInvoiceRebateFeeInfo, alBalanceListRebate, sysdate, balanceNO, invoiceNO, alFeeState) == -1)
                    {
                        this.RollBack();
                        return -1;
                    }
                }

                #endregion



                #region 手工发票

                List<FS.HISFC.Models.Fee.Inpatient.BalanceList> alBalanceListMannal = new List<FS.HISFC.Models.Fee.Inpatient.BalanceList>();
                if (splitInvoiceCost > 0)
                {
                    ArrayList alInvoiceManualFeeInfo = new ArrayList();
                    decimal splitInvoiceCostTemp = splitInvoiceCost;

                    decimal splitRate = splitInvoiceCost / TotBalance;
                    if (balanceType == FS.HISFC.Models.Base.EBlanceType.OweMid)
                    {
                        splitRate = splitInvoiceCost / (TotBalance - arrearFeeCost);
                    }

                    if (splitRate > 1)
                    {
                        this.RollBack();
                        this.err = "发票金额大于结算金额";
                        return -1;
                    }

                    //循环处理分票明细及主发票明细
                    for (int i = 0; i < feeInfoList.Count; i++)
                    {
                        FS.HISFC.Models.Fee.Inpatient.FeeInfo InvoiceHeadInfo = feeInfoList[i];
                        FS.HISFC.Models.Fee.Inpatient.FeeInfo InvoiceManualInfo = InvoiceHeadInfo.Clone();

                        InvoiceManualInfo.FT.OwnCost = FS.FrameWork.Function.NConvert.ToDecimal(FS.FrameWork.Public.String.FormatNumberReturnString(InvoiceHeadInfo.FT.OwnCost * splitRate, 2));
                        InvoiceManualInfo.FT.PayCost = FS.FrameWork.Function.NConvert.ToDecimal(FS.FrameWork.Public.String.FormatNumberReturnString(InvoiceHeadInfo.FT.PayCost * splitRate, 2));
                        InvoiceManualInfo.FT.PubCost = FS.FrameWork.Function.NConvert.ToDecimal(FS.FrameWork.Public.String.FormatNumberReturnString(InvoiceHeadInfo.FT.PubCost * splitRate, 2));
                        InvoiceManualInfo.FT.RebateCost = FS.FrameWork.Function.NConvert.ToDecimal(FS.FrameWork.Public.String.FormatNumberReturnString(InvoiceHeadInfo.FT.RebateCost * splitRate, 2));

                        InvoiceManualInfo.FT.TotCost = InvoiceManualInfo.FT.OwnCost + InvoiceManualInfo.FT.PayCost + InvoiceManualInfo.FT.PubCost + InvoiceManualInfo.FT.RebateCost;

                        InvoiceHeadInfo.FT.TotCost = InvoiceHeadInfo.FT.TotCost - InvoiceManualInfo.FT.TotCost;
                        InvoiceHeadInfo.FT.OwnCost = InvoiceHeadInfo.FT.OwnCost - InvoiceManualInfo.FT.OwnCost;
                        InvoiceHeadInfo.FT.PayCost = InvoiceHeadInfo.FT.PayCost - InvoiceManualInfo.FT.PayCost;
                        InvoiceHeadInfo.FT.PubCost = InvoiceHeadInfo.FT.PubCost - InvoiceManualInfo.FT.PubCost;
                        InvoiceHeadInfo.FT.RebateCost = InvoiceHeadInfo.FT.RebateCost - InvoiceManualInfo.FT.RebateCost;
                        splitInvoiceCostTemp -= InvoiceManualInfo.FT.TotCost;

                        alInvoiceManualFeeInfo.Add(InvoiceManualInfo);
                    }

                    //如果没有分完整，平小数位
                    if (Math.Abs(splitInvoiceCostTemp) > 0)
                    {
                        for (int i = 0; i < feeInfoList.Count; i++)
                        {
                            if (feeInfoList[i].FT.OwnCost + splitInvoiceCostTemp > 0)
                            {
                                FS.HISFC.Models.Fee.Inpatient.FeeInfo InvoiceManualInfo = feeInfoList[i].Clone();
                                InvoiceManualInfo.FT.OwnCost = splitInvoiceCostTemp;
                                InvoiceManualInfo.FT.PayCost = 0.00M;
                                InvoiceManualInfo.FT.PubCost = 0.00M;
                                InvoiceManualInfo.FT.RebateCost = 0.00M;
                                InvoiceManualInfo.FT.TotCost = splitInvoiceCostTemp;
                                feeInfoList[i].FT.OwnCost -= splitInvoiceCostTemp;
                                feeInfoList[i].FT.TotCost -= splitInvoiceCostTemp;
                                alInvoiceManualFeeInfo.Add(InvoiceManualInfo);
                                break;
                            }
                        }
                    }

                    if (this.FeeInfoTransFeeStat(patientInfo, balanceType, alInvoiceManualFeeInfo, alBalanceListMannal, sysdate, balanceNO, "", alFeeState) == -1)
                    {
                        this.RollBack();
                        return -1;
                    }
                }

                #endregion

                #region 主发票

                List<FS.HISFC.Models.Fee.Inpatient.BalanceList> balanceList = new List<FS.HISFC.Models.Fee.Inpatient.BalanceList>();
                if (feeInfoList.Count > 0)
                {
                    if (this.FeeInfoTransFeeStat(patientInfo, balanceType, new ArrayList(feeInfoList), balanceList, sysdate, balanceNO, invoiceNO, alFeeState) == -1)
                    {
                        this.RollBack();
                        return -1;
                    }

                }

                #endregion

                #endregion


                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在完成结算...");
                #region 处理发票明细

                bool mainInvoice = true;

                #region 主发票

                if (balanceList.Count > 0)
                {
                    decimal TotCost = 0m;
                    decimal OwnCost = 0m;
                    decimal PayCost = 0m;
                    decimal PubCost = 0m;
                    decimal DonateCost = 0m;
                    decimal RebateCost = 0M;
                    foreach (FS.HISFC.Models.Fee.Inpatient.BalanceList balance in balanceList)
                    {
                        TotCost += balance.BalanceBase.FT.TotCost;
                        OwnCost += balance.BalanceBase.FT.OwnCost;
                        PayCost += balance.BalanceBase.FT.PayCost;
                        PubCost += balance.BalanceBase.FT.PubCost;
                        DonateCost += balance.BalanceBase.FT.DonateCost;
                        RebateCost += balance.BalanceBase.FT.RebateCost;
                        balance.BalanceBase.PackageFlag = packageBalance ? "1" : "0";
                        if (balanceType == FS.HISFC.Models.Base.EBlanceType.OweMid)
                        {
                            balance.BalanceBase.BalanceType.ID = FS.HISFC.Models.Fee.EnumBalanceType.O;
                        }
                        balance.BalanceBase.Invoice.ID = invoiceNO;
                        if (this.inpatientFeeManager.InsertBalanceList(patientInfo, balance) < 1)
                        {
                            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                            this.RollBack();
                            this.err = this.inpatientFeeManager.Err;
                            return -1;
                        }

                    }

                    FS.HISFC.Models.Fee.Inpatient.Balance balanceHead = ((FS.HISFC.Models.Fee.Inpatient.Balance)balanceList[0].BalanceBase).Clone();

                    balanceHead.Invoice.ID = invoiceNO;
                    balanceHead.BeginTime = patientInfo.PVisit.InTime;//患者入院时间
                    balanceHead.EndTime = sysdate;//结算时间
                    balanceHead.FT.TotCost = TotCost;
                    balanceHead.FT.OwnCost = OwnCost;
                    balanceHead.FT.PayCost = PayCost;
                    balanceHead.FT.PubCost = PubCost;
                    balanceHead.FT.DonateCost = DonateCost;


                    balanceHead.CancelType = FS.HISFC.Models.Base.CancelTypes.Valid;
                    balanceHead.BalanceSaveType = "0";
                    balanceHead.IsMainInvoice = true;

                    //优惠金额
                    if (!IsRebatePrint)
                    {
                        balanceHead.FT.RebateCost = RebateCost;
                    }
                    balanceHead.IsMainInvoice = mainInvoice;
                    if (mainInvoice)
                    {
                        mainInvoice = false;
                        balanceHead.FT.PrepayCost = TotPrepayCost;//预交金额
                        balanceHead.FT.TransferPrepayCost = 0.00M;//欠费金额
                        if (TotBalance == TotPrepayCost)
                        {
                            balanceHead.FT.SupplyCost = 0M;//应收金额
                            balanceHead.FT.ReturnCost = 0M;//应返金额
                        }
                        else if (TotBalance > TotPrepayCost)
                        {
                            balanceHead.FT.SupplyCost = TotBalance - TotPrepayCost;//应收金额
                            balanceHead.FT.ReturnCost = 0M;//应返金额
                        }
                        else
                        {
                            balanceHead.FT.SupplyCost = 0M;//应收金额
                            balanceHead.FT.ReturnCost = TotPrepayCost - TotBalance;//应返金额
                        }

                        balanceHead.FT.SupplyCost = balanceHead.FT.SupplyCost - arrearFeeCost;

                        if (balanceHead.BalanceType.ID.ToString() == FS.HISFC.Models.Fee.EnumBalanceType.Q.ToString())
                        {
                            balanceHead.FT.ArrearCost = arrearFeeCost;
                        }
                    }
                    else
                    {
                        if (balanceHead.BalanceType.ID.ToString() == FS.HISFC.Models.Fee.EnumBalanceType.Q.ToString())
                        {
                            balanceHead.FT.ArrearCost = arrearFeeCost;
                        }
                    }

                    if (patientInfo.Pact.PayKind.ID == "02")
                    {
                        balanceHead.FT.OwnCost = patientInfo.SIMainInfo.OwnCost - balanceHead.FT.DerateCost - balanceHead.FT.RebateCost;
                        balanceHead.FT.PayCost = patientInfo.SIMainInfo.PayCost;
                        balanceHead.FT.PubCost = patientInfo.SIMainInfo.PubCost;
                    }

                    //插入结算头表
                    balanceHead.PrintedInvoiceNO = realInvoiceNO;
                    balanceHead.PackageFlag = packageBalance ? "1" : "0";
                    if (this.inpatientFeeManager.InsertBalance(patientInfo, balanceHead) < 1)
                    {
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        this.RollBack();
                        this.err = this.inpatientFeeManager.Err;
                        return -1;
                    }

                    if (feeIntegrate.InsertInvoiceExtend(invoiceNO, "I", realInvoiceNO, "00") < 1)
                    {
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        this.RollBack();
                        this.err = this.inpatientFeeManager.Err;
                        return -1;
                    }

                    if (feeIntegrate.UseInvoiceNO("I", 1, ref invoiceNO, ref realInvoiceNO, ref this.err) != 1)
                    {
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        this.RollBack();
                        return -1;
                    }

                    listBalance.Add(balanceHead);
                }

                #endregion

                #region 手工发票

                if (alBalanceListMannal.Count > 0)
                {
                    if (feeIntegrate.GetInvoiceNO("I", ref invoiceNO, ref realInvoiceNO, ref errText) < 1 || string.IsNullOrEmpty(invoiceNO) || string.IsNullOrEmpty(realInvoiceNO))
                    {
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        this.RollBack();
                        this.err = "获取发票失败，" + feeIntegrate.Err;
                        return -1;
                    }

                    decimal TotCost = 0m;
                    decimal OwnCost = 0m;
                    decimal PayCost = 0m;
                    decimal PubCost = 0m;
                    decimal DonateCost = 0m;
                    decimal RebateCost = 0M;
                    foreach (FS.HISFC.Models.Fee.Inpatient.BalanceList balance in alBalanceListMannal)
                    {
                        TotCost += balance.BalanceBase.FT.TotCost;
                        OwnCost += balance.BalanceBase.FT.OwnCost;
                        PayCost += balance.BalanceBase.FT.PayCost;
                        PubCost += balance.BalanceBase.FT.PubCost;
                        RebateCost += balance.BalanceBase.FT.RebateCost;
                        DonateCost += balance.BalanceBase.FT.DonateCost;

                        if (balanceType == FS.HISFC.Models.Base.EBlanceType.OweMid)
                        {
                            balance.BalanceBase.BalanceType.ID = FS.HISFC.Models.Fee.EnumBalanceType.O;
                        }

                        balance.BalanceBase.Invoice.ID = invoiceNO;
                        if (this.inpatientFeeManager.InsertBalanceList(patientInfo, balance) < 1)
                        {
                            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                            this.RollBack();
                            this.err = this.inpatientFeeManager.Err;
                            return -1;
                        }

                    }

                    FS.HISFC.Models.Fee.Inpatient.Balance balanceHead = ((FS.HISFC.Models.Fee.Inpatient.Balance)alBalanceListMannal[0].BalanceBase).Clone();

                    balanceHead.Invoice.ID = invoiceNO;
                    balanceHead.BeginTime = patientInfo.PVisit.InTime;//患者入院时间
                    balanceHead.EndTime = sysdate;//结算时间
                    balanceHead.FT.TotCost = TotCost;
                    balanceHead.FT.OwnCost = OwnCost;
                    balanceHead.FT.PayCost = PayCost;
                    balanceHead.FT.PubCost = PubCost;
                    balanceHead.FT.DonateCost = DonateCost;
                    balanceHead.CancelType = FS.HISFC.Models.Base.CancelTypes.Valid;
                    balanceHead.IsMainInvoice = false;
                    balanceHead.BalanceSaveType = "0";

                    //优惠金额
                    if (!IsRebatePrint)
                    {
                        balanceHead.FT.RebateCost = RebateCost;
                    }

                    balanceHead.IsMainInvoice = mainInvoice;
                    if (mainInvoice)
                    {
                        mainInvoice = false;
                        balanceHead.FT.PrepayCost = TotPrepayCost;//预交金额
                        balanceHead.FT.TransferPrepayCost = 0.00M;//欠费金额
                        if (TotBalance == TotPrepayCost)
                        {
                            balanceHead.FT.SupplyCost = 0M;//应收金额
                            balanceHead.FT.ReturnCost = 0M;//应返金额
                        }
                        else if (TotBalance > TotPrepayCost)
                        {
                            balanceHead.FT.SupplyCost = TotBalance - TotPrepayCost;//应收金额
                            balanceHead.FT.ReturnCost = 0M;//应返金额
                        }
                        else
                        {
                            balanceHead.FT.SupplyCost = 0M;//应收金额
                            balanceHead.FT.ReturnCost = TotPrepayCost - TotBalance;//应返金额
                        }

                        balanceHead.FT.SupplyCost = balanceHead.FT.SupplyCost - arrearFeeCost;

                        if (balanceHead.BalanceType.ID.ToString() == FS.HISFC.Models.Fee.EnumBalanceType.Q.ToString())
                        {
                            balanceHead.FT.ArrearCost = arrearFeeCost;
                        }
                    }
                    else
                    {
                        if (balanceHead.BalanceType.ID.ToString() == FS.HISFC.Models.Fee.EnumBalanceType.Q.ToString())
                        {
                            balanceHead.FT.ArrearCost = arrearFeeCost;
                        }
                    }
                    //医保处理 2012年7月24日18:10:28
                    if (patientInfo.Pact.PayKind.ID == "02")
                    {
                        balanceHead.FT.OwnCost = patientInfo.SIMainInfo.OwnCost - balanceHead.FT.DerateCost - balanceHead.FT.RebateCost;
                        balanceHead.FT.PayCost = patientInfo.SIMainInfo.PayCost;
                        balanceHead.FT.PubCost = patientInfo.SIMainInfo.PubCost;
                    }
                    //插入结算头表
                    balanceHead.PrintedInvoiceNO = realInvoiceNO;
                    if (this.inpatientFeeManager.InsertBalance(patientInfo, balanceHead) < 1)
                    {
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        this.RollBack();
                        this.err = this.inpatientFeeManager.Err;
                        return -1;
                    }

                    if (feeIntegrate.InsertInvoiceExtend(invoiceNO, "I", realInvoiceNO, "00") < 1)
                    {
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        this.RollBack();
                        this.err = this.inpatientFeeManager.Err;
                        return -1;
                    }

                    if (feeIntegrate.UseInvoiceNO("I", 1, ref invoiceNO, ref realInvoiceNO, ref this.err) != 1)
                    {
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        this.RollBack();
                        return -1;
                    }

                    listBalance.Add(balanceHead);
                }
                #endregion

                #region 优惠发票

                if (alBalanceListRebate.Count > 0)
                {
                    if (feeIntegrate.GetInvoiceNO("I", ref invoiceNO, ref realInvoiceNO, ref errText) < 1 || string.IsNullOrEmpty(invoiceNO) || string.IsNullOrEmpty(realInvoiceNO))
                    {
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        this.RollBack();
                        this.err = "获取发票失败，" + feeIntegrate.Err;
                        return -1;
                    }
                    decimal TotCost = 0m;
                    decimal OwnCost = 0m;
                    decimal PayCost = 0m;
                    decimal PubCost = 0m;
                    decimal DonateCost = 0m;
                    decimal RebateCost = 0M;
                    foreach (FS.HISFC.Models.Fee.Inpatient.BalanceList balance in alBalanceListRebate)
                    {
                        TotCost += balance.BalanceBase.FT.TotCost;
                        OwnCost += balance.BalanceBase.FT.OwnCost;
                        PayCost += balance.BalanceBase.FT.PayCost;
                        PubCost += balance.BalanceBase.FT.PubCost;
                        DonateCost += balance.BalanceBase.FT.DonateCost;
                        RebateCost += balance.BalanceBase.FT.RebateCost;
                        balance.BalanceBase.Invoice.ID = invoiceNO;
                        if (this.inpatientFeeManager.InsertBalanceList(patientInfo, balance) < 1)
                        {
                            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                            this.RollBack();
                            this.err = this.inpatientFeeManager.Err;
                            return -1;
                        }
                    }
                    //头表实体赋值
                    FS.HISFC.Models.Fee.Inpatient.Balance BalanceRebate = (FS.HISFC.Models.Fee.Inpatient.Balance)alBalanceListRebate[0].BalanceBase.Clone();
                    BalanceRebate.FT.TotCost = TotCost;
                    BalanceRebate.FT.OwnCost = OwnCost;
                    BalanceRebate.FT.PayCost = PayCost;
                    BalanceRebate.FT.PubCost = PubCost;
                    BalanceRebate.FT.DonateCost = DonateCost;
                    BalanceRebate.FT.RebateCost = RebateCost;
                    BalanceRebate.BeginTime = patientInfo.PVisit.InTime;
                    BalanceRebate.EndTime = sysdate;
                    BalanceRebate.CancelType = FS.HISFC.Models.Base.CancelTypes.Valid;
                    BalanceRebate.IsMainInvoice = false;
                    BalanceRebate.BalanceSaveType = "0";
                    //插入结算头表
                    BalanceRebate.PrintedInvoiceNO = realInvoiceNO;
                    if (this.inpatientFeeManager.InsertBalance(patientInfo, BalanceRebate) < 1)
                    {
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        this.RollBack();
                        this.err = this.inpatientFeeManager.Err;
                        return -1;
                    }

                    if (feeIntegrate.InsertInvoiceExtend(invoiceNO, "I", realInvoiceNO, "00") < 1)
                    {
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        this.RollBack();
                        this.err = feeIntegrate.Err;
                        return -1;
                    }

                    if (feeIntegrate.UseInvoiceNO("I", 1, ref invoiceNO, ref realInvoiceNO, ref this.err) != 1)
                    {
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        this.RollBack();
                        return -1;
                    }
                    listBalance.Add(BalanceRebate);
                }

                #endregion

                #region 欠费发票

                if (alArrearBalanceList.Count > 0)
                {
                    if (feeIntegrate.GetInvoiceNO(this.inpatientFeeManager.Operator as FS.HISFC.Models.Base.Employee, "I", true, ref invoiceNO, ref realInvoiceNO, ref this.err) != 1)
                    {
                        this.RollBack();
                        return -1;
                    }

                    decimal TotCost = 0m;
                    decimal OwnCost = 0m;
                    decimal PayCost = 0m;
                    decimal PubCost = 0m;
                    decimal DonateCost = 0m;
                    decimal RebateCost = 0M;
                    foreach (FS.HISFC.Models.Fee.Inpatient.BalanceList balance in alArrearBalanceList)
                    {
                        TotCost += balance.BalanceBase.FT.TotCost;
                        OwnCost += balance.BalanceBase.FT.OwnCost;
                        PayCost += balance.BalanceBase.FT.PayCost;
                        PubCost += balance.BalanceBase.FT.PubCost;
                        DonateCost += balance.BalanceBase.FT.DonateCost;             ////////////////{18AD984D-78C6-4c64-8066-6F31E3BDF7DA}
                        RebateCost += balance.BalanceBase.FT.RebateCost;
                        ((FS.HISFC.Models.Fee.Inpatient.Balance)balance.BalanceBase).IsTempInvoice = true;
                        balance.BalanceBase.Invoice.ID = invoiceNO;
                        if (this.inpatientFeeManager.InsertBalanceList(patientInfo, balance) < 1)
                        {
                            this.RollBack();
                            this.err = this.inpatientFeeManager.Err;
                            return -1;
                        }

                    }

                    FS.HISFC.Models.Fee.Inpatient.Balance balanceHead = ((FS.HISFC.Models.Fee.Inpatient.Balance)alArrearBalanceList[0].BalanceBase).Clone();

                    balanceHead.Invoice.ID = invoiceNO;
                    balanceHead.BeginTime = patientInfo.PVisit.InTime;//患者入院时间
                    balanceHead.EndTime = sysdate;//结算时间
                    balanceHead.FT.TotCost = TotCost;
                    balanceHead.FT.OwnCost = OwnCost;
                    balanceHead.FT.PayCost = PayCost;
                    balanceHead.FT.PubCost = PubCost;
                    balanceHead.FT.DonateCost = DonateCost;            //////////////////{18AD984D-78C6-4c64-8066-6F31E3BDF7DA}
                    balanceHead.IsTempInvoice = true;
                    balanceHead.CancelType = FS.HISFC.Models.Base.CancelTypes.Valid;
                    balanceHead.BalanceSaveType = "0";

                    //优惠金额
                    balanceHead.FT.RebateCost = RebateCost;
                    balanceHead.IsMainInvoice = mainInvoice;
                    if (mainInvoice)
                    {
                        mainInvoice = false;
                        balanceHead.FT.PrepayCost = TotPrepayCost;//预交金额
                        balanceHead.FT.TransferPrepayCost = 0.00M;//欠费金额
                        if (TotBalance == TotPrepayCost)
                        {
                            balanceHead.FT.SupplyCost = 0M;//应收金额
                            balanceHead.FT.ReturnCost = 0M;//应返金额
                        }
                        else if (TotBalance > TotPrepayCost)
                        {
                            balanceHead.FT.SupplyCost = TotBalance - TotPrepayCost;//应收金额
                            balanceHead.FT.ReturnCost = 0M;//应返金额
                        }
                        else
                        {
                            balanceHead.FT.SupplyCost = 0M;//应收金额
                            balanceHead.FT.ReturnCost = TotPrepayCost - TotBalance;//应返金额
                        }


                        balanceHead.FT.SupplyCost = balanceHead.FT.SupplyCost - arrearFeeCost;

                        if (balanceHead.BalanceType.ID.ToString() == FS.HISFC.Models.Fee.EnumBalanceType.Q.ToString())
                        {
                            balanceHead.FT.ArrearCost = arrearFeeCost;
                        }
                    }
                    else
                    {
                        if (balanceHead.BalanceType.ID.ToString() == FS.HISFC.Models.Fee.EnumBalanceType.Q.ToString())
                        {
                            balanceHead.FT.ArrearCost = arrearFeeCost;
                        }
                    }

                    //插入结算头表
                    balanceHead.PrintedInvoiceNO = realInvoiceNO;
                    if (this.inpatientFeeManager.InsertBalance(patientInfo, balanceHead) < 1)
                    {
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        this.RollBack();
                        this.err = this.inpatientFeeManager.Err;
                        return -1;
                    }
                }

                #endregion

                #endregion

                #region 患者费用处理

                patientInfo.SIMainInfo.BalNo = balNo;
                patientInfo.SIMainInfo.InvoiceNo = invoiceNO;

                if (balanceType == FS.HISFC.Models.Base.EBlanceType.Out)
                {
                    //{14CCBD16-9A45-42f8-896C-5A2CB00DAB1B}
                    patientInfo.PVisit.InState.ID = FS.HISFC.Models.Base.EnumInState.O;//出院结算
                }

                FS.HISFC.Models.Fee.Inpatient.FeeInfo feeInfo = new FS.HISFC.Models.Fee.Inpatient.FeeInfo();
                feeInfo.FT.PubCost = TotPub;
                feeInfo.FT.OwnCost = TotOwn;
                feeInfo.FT.PayCost = TotPay;
                feeInfo.FT.RebateCost = TotRebateCost;
                feeInfo.FT.TotCost = TotBalance;
                feeInfo.FT.DonateCost = TotDonateCost;
                feeInfo.FT.PrepayCost = TotPrepayCost;

                //医保记账不处理按项目结算，先屏蔽
                //if (patientInfo.Pact.PayKind.ID == "02")
                //{
                //    feeInfo.FT.PubCost = patientInfo.SIMainInfo.PubCost;
                //    feeInfo.FT.OwnCost = patientInfo.SIMainInfo.OwnCost;
                //    feeInfo.FT.PayCost = patientInfo.SIMainInfo.PayCost;
                //    //feeInfo.FT.RebateCost = TotRebateCost;
                //    //feeInfo.FT.TotCost = TotBalanceCost;
                //    //feeInfo.FT.PrepayCost = TotPrepayCost;
                //}

                if (feeInfo.FT.TotCost != feeInfo.FT.OwnCost + feeInfo.FT.PayCost + feeInfo.FT.PubCost)
                {
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    this.RollBack();
                    this.err = "总费用与分支费用不相等，请检查！";
                    return -1;
                }

                if (this.inpatientFeeManager.UpdateInMainInfoBalanced(patientInfo, sysdate, balanceNO, feeInfo.FT) <= 0)
                {
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    this.RollBack();
                    this.err = this.inpatientFeeManager.Err;
                    return -1;
                }

                //开账
                if (this.inpatientFeeManager.OpenAccount(patientInfo.ID) < 0)
                {
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    this.RollBack();
                    this.err = this.inpatientFeeManager.Err;
                    return -1;
                }

                string motherPayAllFee = controlParamMgr.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.SysConst.Use_Mother_PayAllFee, false, "0");
                if (motherPayAllFee == "1")//婴儿的费用收在妈妈的身上 
                {
                    ArrayList babyList = radtManager.QueryBabies(patientInfo.ID);
                    if (babyList != null && babyList.Count > 0)
                    {
                        foreach (FS.HISFC.Models.RADT.PatientInfo baby in babyList)
                        {
                            FS.HISFC.Models.RADT.PatientInfo pTemp = radtManager.GetPatientInfo(baby.ID);
                            if (pTemp != null && !string.IsNullOrEmpty(pTemp.ID))
                            {
                                pTemp.PVisit = patientInfo.PVisit.Clone();

                                if (this.inpatientFeeManager.UpdateInMainInfoBalanced(pTemp, sysdate, balanceNO, new FS.HISFC.Models.Base.FT()) < 0)
                                {
                                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                                    this.RollBack();
                                    this.err = this.inpatientFeeManager.Err;
                                    return -1;
                                }
                            }
                        }
                    }
                }

                #endregion

                #region 结算变更记录

                FS.FrameWork.Models.NeuObject oldObj = new FS.FrameWork.Models.NeuObject();
                FS.FrameWork.Models.NeuObject newObj = new FS.FrameWork.Models.NeuObject();
                newObj.ID = patientInfo.SIMainInfo.BalNo;
                newObj.Name = "结算序号";
                if (radtManager.SaveShiftData(patientInfo.ID, FS.HISFC.Models.Base.EnumShiftType.BA, "项目结算", oldObj, newObj) == -1)
                {
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    this.RollBack();
                    this.err = radtManager.Err;
                    return -1;
                }

                #endregion

                #region 更新托收单
                if (patientInfo.Pact.PayKind.ID == "03")
                {   //更新结算发票号
                    if (this.Updategfhz(patientInfo, invoiceNO, "0") == -1)
                    {
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        this.RollBack();
                        this.err = "更新托收单出错!" + this.inpatientFeeManager.Err;
                        return -1;

                    }
                }
                #endregion

                //发送消息
                if (InterfaceManager.GetIADT() != null)
                {
                    if (InterfaceManager.GetIADT().Balance(patientInfo, true) < 0)
                    {
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        this.RollBack();
                        this.err = InterfaceManager.GetIADT().Err;
                        return -1;
                    }
                }

                #region 将web相关的积分集中在此处理，方便事务回滚
                //{333D2AD8-DC4A-4c30-A14E-D6815AC858F9}

                //积分模块是否启用
                bool IsCouponModuleInUse = false;

                IsCouponModuleInUse = this.controlParamIntegrate.GetControlParam<bool>("CP0001", false, false);

                if (IsCouponModuleInUse)
                {
                    //处理消耗的积分
                    string resultCode = "0";
                    string errorMsg = "";
                    int reqRtn = -1;

                    if (couponCostAmount != 0)
                    {
                        reqRtn = FS.HISFC.BizProcess.Integrate.WSHelper.CostCoupon(patientInfo.PID.CardNO, patientInfo.Name, patientInfo.PID.CardNO, patientInfo.Name, "ZYSF", invoiceNO, couponCostAmount, 0.0m, out resultCode, out errorMsg);
                        if (reqRtn < 0)
                        {
                            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                            this.RollBack();
                            this.err = "处理会员积分出错!" + errorMsg;
                            return -1;
                        }
                    }

                    //计算本单积分
                    if (operateCouponAmount != 0)
                    {
                        reqRtn = FS.HISFC.BizProcess.Integrate.WSHelper.OperateCoupon(patientInfo.PID.CardNO, patientInfo.Name, "ZYSF", invoiceNO, operateCouponAmount, 0.0m, out resultCode, out errorMsg);
                        if (reqRtn < 0)
                        {
                            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                            this.RollBack();
                            this.err = "处理会员积分出错!" + errorMsg;

                            if (couponCostAmount != 0)
                            {
                                reqRtn = FS.HISFC.BizProcess.Integrate.WSHelper.CostCoupon(patientInfo.PID.CardNO, patientInfo.Name, patientInfo.PID.CardNO, patientInfo.Name, "ZYTF", invoiceNO, -couponCostAmount, 0.0m, out resultCode, out errorMsg);

                                if (reqRtn < 0)
                                {
                                    this.err += "回滚会员积分出错，请联系信息科处理，错误详情:";
                                    this.err += errorMsg;
                                }
                            }

                            return -1;
                        }
                    }
                }

                #endregion

                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }
            catch (Exception ex)
            {
                this.RollBack();
                this.err = "结算过程出现错误：" + ex.Message;
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                return -1;
            }
            this.Commit();
            return 1;
        }
        #endregion

        #region 爱博恩医保结算列表查询

        /// <summary>
        /// {5A1EFA76-6758-40ae-9870-E1BAEAA4BA72}
        /// 查询需要上传的患者列表
        /// </summary>
        /// <param name="dsRes"></param>
        /// <returns></returns>
        public int QueryNeedUpLoadFeePatients(ref System.Data.DataSet dsRes)
        {
            int rtn = this.inpatientFeeManager.QueryNeedUpLoadFeePatients(ref dsRes);
            this.err = this.inpatientFeeManager.Err;
            return rtn;
        }

        #endregion

        #endregion

        #region 在院结算

        /// <summary>
        /// 在院结算
        /// </summary>
        /// <param name="patientInfo">患者信息</param>
        /// <param name="feeInfoList">待结算费用</param>
        /// <param name="oldFeeInfoList">全部费用</param>
        /// <param name="prepayList">预交金信息</param>
        /// <param name="balancePayList">支付方式</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="splitHighInvoice">分发票信息</param>
        /// <param name="listBalance">返回的结算信息</param>
        /// <returns></returns>
        public int InBalance(FS.HISFC.Models.RADT.PatientInfo patientInfo,
            List<FS.HISFC.Models.Fee.Inpatient.FeeInfo> feeInfoList,
            List<FS.HISFC.Models.Fee.Inpatient.FeeInfo> oldFeeInfoList,
            List<FS.HISFC.Models.Fee.Inpatient.Prepay> prepayList,
            List<FS.HISFC.Models.Fee.Inpatient.BalancePay> balancePayList,
            DateTime beginTime, DateTime endTime,
            bool splitHighInvoice,
            ref List<FS.HISFC.Models.Fee.Inpatient.Balance> listBalance)
        {
            if (patientInfo == null || string.IsNullOrEmpty(patientInfo.ID))
            {
                this.err = "患者信息为空！";
                return -1;
            }
            this.BeginTransaction();
            RADT radtManager = new RADT();
            FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();
            FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParamMgr = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
            FS.HISFC.BizLogic.Fee.FeeCodeStat feeCodeStat = new FS.HISFC.BizLogic.Fee.FeeCodeStat();
            radtManager.SetTrans(this.Trans);
            feeIntegrate.SetTrans(this.Trans);
            inpatientFeeManager.SetTrans(this.Trans);
            controlParamMgr.SetTrans(this.Trans);
            feeCodeStat.SetTrans(this.Trans);

            //获取时间
            DateTime sysdate = inpatientFeeManager.GetDateTimeFromSysDateTime();

            #region 患者信息验证
            //验证患者信息
            FS.HISFC.Models.RADT.PatientInfo patientInfoTemp = radtManager.GetPatientInfo(patientInfo.ID);
            if (patientInfoTemp == null || string.IsNullOrEmpty(patientInfoTemp.ID))
            {
                this.RollBack();
                this.err = "患者信息为空！原因：" + radtManager.Err;
                return -1;
            }

            //已经出院的返回
            if (patientInfo.PVisit.InState.ID.ToString().Equals(FS.HISFC.Models.Base.EnumInState.O))
            {
                this.RollBack();
                this.err = "患者已经出院结算";
                return -1;
            }

            #endregion

            #region 发票号和结算号

            string invoiceNO = string.Empty;
            string realInvoiceNO = string.Empty;
            string errText = "";

            if (feeIntegrate.GetInvoiceNO("I", ref invoiceNO, ref realInvoiceNO, ref errText) < 1 || string.IsNullOrEmpty(invoiceNO) || string.IsNullOrEmpty(realInvoiceNO))
            {
                this.RollBack();
                this.err = "获取发票失败，" + feeIntegrate.Err;
                return -1;
            }

            //调业务层获取结算次数
            int balanceNO = 0;
            string balNo = inpatientFeeManager.GetNewBalanceNO(patientInfo.ID);
            if (balNo == null || balNo.Length == 0)
            {
                this.RollBack();
                this.err = "获取结算序号出错，" + feeIntegrate.Err;
                return -1;
            }
            else
            {
                balanceNO = int.Parse(balNo);
            }

            #endregion

            #region 处理预交金信息

            decimal TotPrepayCost = 0.00M;
            decimal TotTransPrepryCost = 0.00M;
            if (prepayList != null && prepayList.Count > 0)
            {
                foreach (FS.HISFC.Models.Fee.Inpatient.Prepay prepay in prepayList)
                {
                    //转押金
                    if (prepay.BalanceState.Equals("1") == false)
                    {
                        TotTransPrepryCost += prepay.FT.PrepayCost;//转押金金额

                        prepay.TransPrepayOper.ID = inpatientFeeManager.Operator.ID;
                        prepay.TransPrepayOper.Name = inpatientFeeManager.Operator.Name;
                        prepay.TransferPrepayTime = sysdate;
                        prepay.PrepayOper.ID = inpatientFeeManager.Operator.ID;
                        prepay.PrepayOper.OperTime = sysdate;
                        prepay.TransferPrepayBalanceNO = balanceNO;
                        prepay.TransferPrepayState = "1";
                        prepay.BalanceNO = 0;
                        prepay.BalanceState = "0";
                        prepay.PrepayState = "0";
                        prepay.RecipeNO = "转押金";
                        prepay.PayType.ID = "CA";
                        FS.FrameWork.Models.NeuObject obj = this.inpatientFeeManager.GetFinGroupInfoByOperCode(this.inpatientFeeManager.Operator.ID);
                        if (obj == null)
                        {
                            this.RollBack();
                            this.err = "获取人员财务组失败，" + this.inpatientFeeManager.Err;
                            return -1;
                        }
                        prepay.FinGroup.ID = obj.ID;
                        //更新预交发票为结算状态
                        if (inpatientFeeManager.InsertPrepay(patientInfo, prepay) <= 0)
                        {
                            this.RollBack();
                            this.err = "结算失败，原因：" + inpatientFeeManager.Err;
                            return -1;
                        }
                    }
                    else
                    {
                        prepay.BalanceNO = balanceNO;
                        prepay.Invoice.ID = invoiceNO;
                        prepay.BalanceOper.ID = this.inpatientFeeManager.Operator.ID;
                        prepay.BalanceOper.OperTime = sysdate;

                        TotPrepayCost += prepay.FT.PrepayCost;//预交金金额

                        //转入预交金处理（目前缺失更新单条的）
                        if (prepay.Name == "转入预交金")
                        {
                            if (inpatientFeeManager.UpdateChangePrepayBalanced(patientInfo.ID, balanceNO) == -1)
                            {
                                this.RollBack();
                                this.err = "结算失败，原因：" + inpatientFeeManager.Err;
                                return -1;
                            }
                        }
                        else
                        {
                            //更新预交发票为结算状态
                            if (inpatientFeeManager.UpdatePrepayBalanced(patientInfo, prepay) <= 0)
                            {
                                this.RollBack();
                                this.err = "结算失败，原因：" + inpatientFeeManager.Err;
                                return -1;
                            }
                        }
                    }
                }
            }

            #endregion

            #region 处理费用信息

            decimal TotOwnCost = 0.00M;
            decimal TotPubCost = 0.00M;
            decimal TotPayCost = 0.00M;
            decimal TotBalanceCost = 0.00M;
            decimal TotRebateCost = 0.00M;
            decimal TotShouldPay = 0.00M;

            if (feeInfoList != null && feeInfoList.Count > 0)
            {
                foreach (FS.HISFC.Models.Fee.Inpatient.FeeInfo f in feeInfoList)
                {
                    TotBalanceCost += f.FT.BalancedCost;
                    TotOwnCost += f.FT.OwnCost;
                    TotPubCost += f.FT.PubCost;
                    TotPayCost += f.FT.PayCost;
                    TotRebateCost += f.FT.RebateCost;

                    if (patientInfo.Pact.PayKind.ID == "03")
                    {
                        TotShouldPay += f.FT.OwnCost + f.FT.PayCost;
                    }
                    else
                    {
                        TotShouldPay += f.FT.OwnCost;
                    }

                    if (patientInfo.Pact.PayKind.ID == "02")
                    {
                        TotShouldPay = patientInfo.SIMainInfo.OwnCost + patientInfo.SIMainInfo.PayCost;
                    }

                    if (f.SplitFeeFlag == false)
                    {
                        int Result = inpatientFeeManager.UpdateFeeInfoBalanced(patientInfo.ID, balanceNO, sysdate, invoiceNO, beginTime, endTime, f.Item.MinFee.ID, false);
                        if (Result == 0)
                        {
                            this.RollBack();
                            this.err = "存在并发操作导致更新费用失败！";
                            return -1;
                        }
                        if (Result < 1)
                        {
                            this.RollBack();
                            this.err = inpatientFeeManager.Err;
                            return -1;
                        }

                        if (inpatientFeeManager.UpdateFeeItemListBalanced(patientInfo.ID, balanceNO, invoiceNO, beginTime, endTime, f.Item.MinFee.ID, false) == -1)
                        {
                            this.RollBack();
                            this.err = inpatientFeeManager.Err;
                            return -1;
                        }
                        if (inpatientFeeManager.UpdateMedItemListBalanced(patientInfo.ID, balanceNO, invoiceNO, beginTime, endTime, f.Item.MinFee.ID) == -1)
                        {
                            this.RollBack();
                            this.err = inpatientFeeManager.Err;
                            return -1;
                        }
                    }


                    //补中结差额
                    if (f.FT.BalancedCost < f.FT.TotCost)
                    {
                        //判断并发
                        #region 中结差额
                        //查找原始相等的记录
                        foreach (FS.HISFC.Models.Fee.Inpatient.FeeInfo finfo in oldFeeInfoList)
                        {
                            if (f.Item.MinFee.ID == finfo.Item.MinFee.ID)
                            {
                                //break;
                                //实体赋值--形成未结算正记录
                                FS.HISFC.Models.Fee.Inpatient.FeeItemList itemList = new FS.HISFC.Models.Fee.Inpatient.FeeItemList();
                                itemList.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(f.FT.TotCost - f.FT.BalancedCost, 2);
                                itemList.FT.OwnCost = finfo.FT.OwnCost - f.FT.OwnCost;
                                itemList.FT.PayCost = finfo.FT.PayCost - f.FT.PayCost;
                                itemList.FT.PubCost = finfo.FT.PubCost - f.FT.PubCost;
                                itemList.FT.RebateCost = finfo.FT.RebateCost - f.FT.RebateCost;
                                if ((itemList.FT.TotCost) != (itemList.FT.OwnCost + itemList.FT.PubCost + itemList.FT.PayCost + itemList.FT.RebateCost))
                                {
                                    this.RollBack();
                                    this.err = "中结差额款拆分处方金额相加不等于总金额";
                                    return -1;
                                }

                                itemList.RecipeNO = inpatientFeeManager.GetUndrugRecipeNO();
                                itemList.SequenceNO = 1;
                                itemList.TransType = FS.HISFC.Models.Base.TransTypes.Positive;//-------
                                itemList.RecipeOper.ID = "中结";
                                itemList.StockOper.Dept.ID = patientInfo.PVisit.PatientLocation.Dept.ID;
                                itemList.ExecOper.Dept.ID = patientInfo.PVisit.PatientLocation.Dept.ID;
                                itemList.RecipeOper.Dept.ID = patientInfo.PVisit.PatientLocation.Dept.ID;
                                ((FS.HISFC.Models.RADT.PatientInfo)itemList.Patient).PVisit.PatientLocation.NurseCell.ID = patientInfo.PVisit.PatientLocation.NurseCell.ID;
                                itemList.Item.ID = "faaaaaaaaaaa";
                                itemList.Item.Name = "中结差额款";
                                itemList.Item.MinFee.ID = f.Item.MinFee.ID;
                                //单价等于总额数量为1
                                itemList.Item.Price = itemList.FT.TotCost;
                                itemList.Item.Qty = 1;
                                itemList.Item.PriceUnit = "次";
                                itemList.Item.ItemType = FS.HISFC.Models.Base.EnumItemType.UnDrug;
                                itemList.PayType = FS.HISFC.Models.Base.PayTypes.Balanced;
                                itemList.BalanceNO = 0;
                                itemList.ChargeOper.ID = inpatientFeeManager.Operator.ID;
                                itemList.ChargeOper.OperTime = sysdate;
                                //整处方婴儿标记为false
                                itemList.IsBaby = false;
                                itemList.FeeOper.ID = inpatientFeeManager.Operator.ID;
                                itemList.FeeOper.OperTime = sysdate;
                                itemList.ExecOper.ID = inpatientFeeManager.Operator.ID;
                                itemList.ExecOper.OperTime = sysdate;
                                itemList.BalanceState = "0";
                                //插入正记录未结算实体
                                if (inpatientFeeManager.InsertFeeItemList(patientInfo, itemList) <= 0)
                                {
                                    this.RollBack();
                                    this.err = "结算失败，原因：" + inpatientFeeManager.Err;
                                    return -1;
                                }
                                if (inpatientFeeManager.InsertFeeInfo(patientInfo, itemList) <= 0)
                                {
                                    this.RollBack();
                                    this.err = "结算失败，原因：" + inpatientFeeManager.Err;
                                    return -1;
                                }

                                //赋值形成负记录结算实体
                                itemList.RecipeNO = inpatientFeeManager.GetUndrugRecipeNO();
                                itemList.Item.Qty = -itemList.Item.Qty;
                                itemList.FT.TotCost = -itemList.FT.TotCost;
                                itemList.FT.OwnCost = -itemList.FT.OwnCost;
                                itemList.FT.PayCost = -itemList.FT.PayCost;
                                itemList.FT.PubCost = -itemList.FT.PubCost;
                                itemList.FT.RebateCost = -itemList.FT.RebateCost;
                                itemList.TransType = FS.HISFC.Models.Base.TransTypes.Negative;
                                itemList.Invoice.ID = invoiceNO;
                                itemList.BalanceOper.ID = inpatientFeeManager.Operator.ID;
                                itemList.BalanceOper.OperTime = sysdate;
                                itemList.BalanceNO = balanceNO;
                                itemList.BalanceState = "1";
                                //插入负记录结算记录
                                if (inpatientFeeManager.InsertFeeItemList(patientInfo, itemList) <= 0)
                                {
                                    this.RollBack();
                                    this.err = "结算失败，原因：" + inpatientFeeManager.Err;
                                    return -1;
                                }
                                if (inpatientFeeManager.InsertFeeInfo(patientInfo, itemList) <= 0)
                                {
                                    this.RollBack();
                                    this.err = "结算失败，原因：" + inpatientFeeManager.Err;
                                    return -1;
                                }

                                //需要减掉f中的金额
                                f.FT.TotCost += itemList.FT.TotCost;

                                break;
                            }
                        }

                        #endregion
                    }
                }

            }

            List<FS.HISFC.Models.Fee.Inpatient.BalanceList> listSplitFeeBalanceList = new List<FS.HISFC.Models.Fee.Inpatient.BalanceList>();
            ArrayList alSplitFeeBalanceList = new ArrayList();
            if (splitHighInvoice)
            {
                for (int i = 0; i < feeInfoList.Count; )
                {
                    FS.HISFC.Models.Fee.Inpatient.FeeInfo f = feeInfoList[i];
                    if (f.SplitFeeFlag)
                    {
                        feeInfoList.RemoveAt(i);
                        alSplitFeeBalanceList.Add(f);
                        continue;
                    }
                    i++;
                }
            }

            #endregion

            #region 处理支付方式

            if (balancePayList != null && balancePayList.Count > 0)
            {
                foreach (FS.HISFC.Models.Fee.Inpatient.BalancePay balancePay in balancePayList)
                {
                    balancePay.Invoice.ID = invoiceNO;
                    balancePay.BalanceNO = balanceNO;
                    balancePay.BalanceOper.ID = inpatientFeeManager.Operator.ID;
                    balancePay.BalanceOper.OperTime = sysdate;
                    //添加记录
                    if (inpatientFeeManager.InsertBalancePay(balancePay) == -1)
                    {
                        this.RollBack();
                        this.err = inpatientFeeManager.Err;
                        return -1;
                    }
                }
            }

            #endregion

            #region 处理发票信息

            string invoiceType = "ZY01";

            ArrayList alFeeState = feeCodeStat.QueryFeeCodeStatByReportCode(invoiceType);
            if (alFeeState == null)
            {
                this.RollBack();
                this.err = "结算失败，原因：" + feeCodeStat.Err;
                return -1;
            }

            bool IsRebatePrint = controlParamMgr.GetControlParam<bool>("100007");


            #region 主发票

            List<FS.HISFC.Models.Fee.Inpatient.BalanceList> balanceList = new List<FS.HISFC.Models.Fee.Inpatient.BalanceList>();
            if (feeInfoList.Count > 0)
            {
                //处理主发票
                if (this.FeeInfoTransFeeStat(patientInfo, FS.HISFC.Models.Base.EBlanceType.Mid, new ArrayList(feeInfoList), balanceList, sysdate, balanceNO, invoiceNO, alFeeState) < 0)
                {
                    this.RollBack();
                    return -1;
                }
            }

            if (balanceList.Count > 0)
            {
                decimal TotCost = 0m;
                decimal OwnCost = 0m;
                decimal PayCost = 0m;
                decimal PubCost = 0m;
                decimal RebateCost = 0M;
                foreach (FS.HISFC.Models.Fee.Inpatient.BalanceList balance in balanceList)
                {
                    TotCost += balance.BalanceBase.FT.TotCost;
                    OwnCost += balance.BalanceBase.FT.OwnCost;
                    PayCost += balance.BalanceBase.FT.PayCost;
                    PubCost += balance.BalanceBase.FT.PubCost;
                    RebateCost += balance.BalanceBase.FT.RebateCost;

                    balance.BalanceBase.Invoice.ID = invoiceNO;
                    if (this.inpatientFeeManager.InsertBalanceList(patientInfo, balance) < 1)
                    {
                        this.RollBack();
                        this.err = this.inpatientFeeManager.Err;
                        return -1;
                    }

                }

                FS.HISFC.Models.Fee.Inpatient.Balance balanceHead = ((FS.HISFC.Models.Fee.Inpatient.Balance)balanceList[0].BalanceBase).Clone();

                balanceHead.Invoice.ID = invoiceNO;
                balanceHead.BeginTime = beginTime;
                balanceHead.EndTime = endTime;
                balanceHead.FT.TotCost = TotCost;
                balanceHead.FT.OwnCost = OwnCost;
                balanceHead.FT.PayCost = PayCost;
                balanceHead.FT.PubCost = PubCost;

                balanceHead.CancelType = FS.HISFC.Models.Base.CancelTypes.Valid;
                balanceHead.IsMainInvoice = true;
                balanceHead.BalanceSaveType = "0";

                //优惠金额
                if (!IsRebatePrint)
                {
                    balanceHead.FT.RebateCost = RebateCost;
                }

                balanceHead.FT.PrepayCost = TotPrepayCost;//预交金额
                balanceHead.FT.TransferPrepayCost = TotTransPrepryCost;//转押金金额
                if (TotTransPrepryCost > 0 || TotShouldPay == TotPrepayCost)
                {
                    balanceHead.FT.SupplyCost = 0M;//应收金额
                    balanceHead.FT.ReturnCost = 0M;//应返金额
                }
                else if (TotShouldPay > TotPrepayCost)
                {
                    balanceHead.FT.SupplyCost = TotShouldPay - TotPrepayCost;//应收金额
                    balanceHead.FT.ReturnCost = 0M;//应返金额
                }
                else
                {
                    balanceHead.FT.SupplyCost = 0M;//应收金额
                    balanceHead.FT.ReturnCost = TotPrepayCost - TotShouldPay;//应返金额
                }

                if (patientInfo.Pact.PayKind.ID == "02")
                {
                    balanceHead.FT.OwnCost = patientInfo.SIMainInfo.OwnCost - balanceHead.FT.DerateCost - balanceHead.FT.RebateCost;
                    balanceHead.FT.PayCost = patientInfo.SIMainInfo.PayCost;
                    balanceHead.FT.PubCost = patientInfo.SIMainInfo.PubCost;
                }

                //插入结算头表
                balanceHead.PrintedInvoiceNO = realInvoiceNO;
                if (this.inpatientFeeManager.InsertBalance(patientInfo, balanceHead) < 1)
                {
                    this.RollBack();
                    this.err = this.inpatientFeeManager.Err;
                    return -1;
                }

                if (feeIntegrate.InsertInvoiceExtend(invoiceNO, "I", realInvoiceNO, "00") < 1)
                {
                    this.RollBack();
                    this.err = this.inpatientFeeManager.Err;
                    return -1;
                }

                if (feeIntegrate.UseInvoiceNO("I", 1, ref invoiceNO, ref realInvoiceNO, ref this.err) != 1)
                {
                    this.RollBack();
                    return -1;
                }

                listBalance.Add(balanceHead);
            }
            #endregion


            #region 高收费发票

            //处理高收费发票明细大类
            if (alSplitFeeBalanceList.Count > 0)
            {
                if (this.FeeInfoTransFeeStat(patientInfo, FS.HISFC.Models.Base.EBlanceType.Mid, alSplitFeeBalanceList, listSplitFeeBalanceList, sysdate, balanceNO, "", alFeeState) == -1)
                {
                    this.RollBack();
                    return -1;
                }
            }

            if (listSplitFeeBalanceList.Count > 0)
            {
                if (feeIntegrate.GetInvoiceNO("I", ref invoiceNO, ref realInvoiceNO, ref errText) < 1 || string.IsNullOrEmpty(invoiceNO) || string.IsNullOrEmpty(realInvoiceNO))
                {
                    this.RollBack();
                    this.err = "获取发票失败，" + feeIntegrate.Err;
                    return -1;
                }

                decimal TotCost = 0m;
                decimal OwnCost = 0m;
                decimal PayCost = 0m;
                decimal PubCost = 0m;
                decimal RebateCost = 0M;
                foreach (FS.HISFC.Models.Fee.Inpatient.BalanceList balance in listSplitFeeBalanceList)
                {
                    TotCost += balance.BalanceBase.FT.TotCost;
                    OwnCost += balance.BalanceBase.FT.TotCost;
                    PayCost += 0;
                    PubCost += 0;
                    RebateCost += balance.BalanceBase.FT.RebateCost;

                    balance.BalanceBase.Invoice.ID = invoiceNO;
                    ((FS.HISFC.Models.Fee.Inpatient.Balance)balance.BalanceBase).IsTempInvoice = false;//暂时代替高收费发票数据
                    if (this.inpatientFeeManager.InsertBalanceList(patientInfo, balance) < 1)
                    {
                        this.RollBack();
                        this.err = this.inpatientFeeManager.Err;
                        return -1;
                    }

                }

                FS.HISFC.Models.Fee.Inpatient.Balance balanceHead = ((FS.HISFC.Models.Fee.Inpatient.Balance)listSplitFeeBalanceList[0].BalanceBase).Clone();
                balanceHead.Invoice.ID = invoiceNO;
                balanceHead.BeginTime = beginTime;//患者入院时间
                balanceHead.EndTime = endTime;//结算时间
                balanceHead.FT.TotCost = TotCost;
                balanceHead.FT.OwnCost = OwnCost;
                balanceHead.FT.PayCost = PayCost;
                balanceHead.FT.PubCost = PubCost;

                balanceHead.CancelType = FS.HISFC.Models.Base.CancelTypes.Valid;
                balanceHead.IsMainInvoice = false;
                balanceHead.BalanceSaveType = "0";

                //优惠金额
                if (!IsRebatePrint)
                {
                    balanceHead.FT.RebateCost = RebateCost;
                }

                foreach (FS.HISFC.Models.Fee.Inpatient.FeeInfo f in alSplitFeeBalanceList)
                {
                    int Result = inpatientFeeManager.UpdateFeeInfoBalanced(patientInfo.ID, balanceNO, sysdate, invoiceNO, beginTime, endTime, f.Item.MinFee.ID, true);
                    if (Result == 0)
                    {
                        this.RollBack();
                        this.err = "存在并发操作导致更新费用失败！";
                        return -1;
                    }
                    if (Result < 1)
                    {
                        this.RollBack();
                        this.err = inpatientFeeManager.Err;
                        return -1;
                    }

                    if (inpatientFeeManager.UpdateFeeItemListBalanced(patientInfo.ID, balanceNO, invoiceNO, beginTime, endTime, f.Item.MinFee.ID, true) == -1)
                    {
                        this.RollBack();
                        this.err = inpatientFeeManager.Err;
                        return -1;
                    }
                }


                //插入结算头表
                balanceHead.PrintedInvoiceNO = realInvoiceNO;
                FS.HISFC.Models.Base.PactInfo pact = patientInfo.Pact.Clone();
                patientInfo.Pact.ID = "1";
                patientInfo.Pact.Name = "自费";
                patientInfo.Pact.PayKind.ID = "01";
                patientInfo.Pact.PayKind.Name = "自费";
                if (this.inpatientFeeManager.InsertBalance(patientInfo, balanceHead) < 1)
                {
                    this.RollBack();
                    this.err = this.inpatientFeeManager.Err;
                    patientInfo.Pact = pact;
                    return -1;
                }
                patientInfo.Pact = pact;

                if (feeIntegrate.InsertInvoiceExtend(invoiceNO, "I", realInvoiceNO, "00") < 1)
                {
                    this.RollBack();
                    this.err = this.inpatientFeeManager.Err;
                    return -1;
                }

                if (feeIntegrate.UseInvoiceNO("I", 1, ref invoiceNO, ref realInvoiceNO, ref this.err) != 1)
                {
                    this.RollBack();
                    return -1;
                }

                listBalance.Add(balanceHead);
            }
            else
            {
                foreach (FS.HISFC.Models.Fee.Inpatient.FeeInfo f in feeInfoList)
                {
                    if (f.SplitFeeFlag)
                    {
                        int Result = inpatientFeeManager.UpdateFeeInfoBalanced(patientInfo.ID, balanceNO, sysdate, invoiceNO, beginTime, endTime, f.Item.MinFee.ID, true);
                        if (Result == 0)
                        {
                            this.RollBack();
                            this.err = "存在并发操作导致更新费用失败！";
                            return -1;
                        }
                        if (Result < 1)
                        {
                            this.RollBack();
                            this.err = inpatientFeeManager.Err;
                            return -1;
                        }

                        if (inpatientFeeManager.UpdateFeeItemListBalanced(patientInfo.ID, balanceNO, invoiceNO, beginTime, endTime, f.Item.MinFee.ID, true) == -1)
                        {
                            this.RollBack();
                            this.err = inpatientFeeManager.Err;
                            return -1;
                        }
                    }
                }
            }

            #endregion

            #region 优惠发票

            List<FS.HISFC.Models.Fee.Inpatient.BalanceList> alBalanceListRebate = new List<FS.HISFC.Models.Fee.Inpatient.BalanceList>();
            //判断优惠是否单独打印发票
            if (IsRebatePrint)
            {
                ArrayList alInvoiceRebateFeeInfo = new ArrayList();
                for (int i = 0; i < feeInfoList.Count; i++)
                {
                    FS.HISFC.Models.Fee.Inpatient.FeeInfo f = feeInfoList[i];
                    if (f.FT.RebateCost > 0)
                    {
                        FS.HISFC.Models.Fee.Inpatient.FeeInfo FeeInfoRebate = new FS.HISFC.Models.Fee.Inpatient.FeeInfo();
                        FeeInfoRebate.Item.MinFee.ID = f.Item.MinFee.ID;
                        FeeInfoRebate.FT.RebateCost = f.FT.RebateCost;
                        FeeInfoRebate.FT.TotCost = f.FT.TotCost;
                        alInvoiceRebateFeeInfo.Add(FeeInfoRebate);
                    }
                }

                if (this.FeeInfoTransFeeStat(patientInfo, FS.HISFC.Models.Base.EBlanceType.Mid, alInvoiceRebateFeeInfo, alBalanceListRebate, sysdate, balanceNO, invoiceNO, alFeeState) == -1)
                {
                    this.RollBack();
                    return -1;
                }
            }

            if (alBalanceListRebate.Count > 0)
            {
                if (feeIntegrate.GetInvoiceNO("I", ref invoiceNO, ref realInvoiceNO, ref errText) < 1 || string.IsNullOrEmpty(invoiceNO) || string.IsNullOrEmpty(realInvoiceNO))
                {
                    this.RollBack();
                    this.err = "获取发票失败，" + feeIntegrate.Err;
                    return -1;
                }
                decimal TotCost = 0m;
                decimal OwnCost = 0m;
                decimal PayCost = 0m;
                decimal PubCost = 0m;
                decimal RebateCost = 0M;
                foreach (FS.HISFC.Models.Fee.Inpatient.BalanceList balance in alBalanceListRebate)
                {
                    TotCost += balance.BalanceBase.FT.TotCost;
                    OwnCost += balance.BalanceBase.FT.OwnCost;
                    PayCost += balance.BalanceBase.FT.PayCost;
                    PubCost += balance.BalanceBase.FT.PubCost;
                    RebateCost += balance.BalanceBase.FT.RebateCost;
                    balance.BalanceBase.Invoice.ID = invoiceNO;
                    if (this.inpatientFeeManager.InsertBalanceList(patientInfo, balance) < 1)
                    {
                        this.RollBack();
                        this.err = this.inpatientFeeManager.Err;
                        return -1;
                    }
                }
                //头表实体赋值
                FS.HISFC.Models.Fee.Inpatient.Balance BalanceRebate = (FS.HISFC.Models.Fee.Inpatient.Balance)alBalanceListRebate[0].BalanceBase.Clone();
                BalanceRebate.FT.TotCost = TotCost;
                BalanceRebate.FT.OwnCost = OwnCost;
                BalanceRebate.FT.PayCost = PayCost;
                BalanceRebate.FT.PubCost = PubCost;
                BalanceRebate.FT.RebateCost = RebateCost;
                BalanceRebate.BeginTime = beginTime;
                BalanceRebate.EndTime = endTime;
                BalanceRebate.CancelType = FS.HISFC.Models.Base.CancelTypes.Valid;
                BalanceRebate.IsMainInvoice = false;
                BalanceRebate.BalanceSaveType = "0";
                //插入结算头表
                BalanceRebate.PrintedInvoiceNO = realInvoiceNO;
                if (this.inpatientFeeManager.InsertBalance(patientInfo, BalanceRebate) < 1)
                {
                    this.RollBack();
                    this.err = this.inpatientFeeManager.Err;
                    return -1;
                }

                if (feeIntegrate.InsertInvoiceExtend(invoiceNO, "I", realInvoiceNO, "00") < 1)
                {
                    this.RollBack();
                    this.err = feeIntegrate.Err;
                    return -1;
                }

                if (feeIntegrate.UseInvoiceNO("I", 1, ref invoiceNO, ref realInvoiceNO, ref this.err) != 1)
                {
                    this.RollBack();
                    return -1;
                }
                listBalance.Add(BalanceRebate);
            }
            #endregion

            //处理其他发票（先不写）
            //婴儿发票无用，如果是单独结算
            //膳食发票先不写，等需要时在写
            //减免发票，目前只支持出院结算
            //分发票（出院结算才允许分发票）


            #endregion

            #region 患者费用处理

            patientInfo.SIMainInfo.BalNo = balNo;
            patientInfo.SIMainInfo.InvoiceNo = invoiceNO;
            FS.HISFC.Models.Fee.Inpatient.FeeInfo feeInfo = new FS.HISFC.Models.Fee.Inpatient.FeeInfo();
            feeInfo.FT.PubCost = TotPubCost;
            feeInfo.FT.OwnCost = TotOwnCost;
            feeInfo.FT.PayCost = TotPayCost;
            feeInfo.FT.RebateCost = TotRebateCost;
            feeInfo.FT.TotCost = TotBalanceCost;
            feeInfo.FT.PrepayCost = TotPrepayCost;

            if (feeInfo.FT.TotCost != feeInfo.FT.OwnCost + feeInfo.FT.PayCost + feeInfo.FT.PubCost + feeInfo.FT.RebateCost)
            {
                this.RollBack();
                this.err = "总费用与分支费用不相等，请检查！";
                return -1;
            }

            if (this.inpatientFeeManager.UpdateInMainInfoBalanced(patientInfo, sysdate, balanceNO, feeInfo.FT) <= 0)
            {
                this.RollBack();
                this.err = this.inpatientFeeManager.Err;
                return -1;
            }

            //开账
            if (this.inpatientFeeManager.OpenAccount(patientInfo.ID) < 0)
            {
                this.RollBack();
                this.err = this.inpatientFeeManager.Err;
                return -1;
            }

            string motherPayAllFee = controlParamMgr.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.SysConst.Use_Mother_PayAllFee, false, "0");
            if (motherPayAllFee == "1")//婴儿的费用收在妈妈的身上 
            {
                ArrayList babyList = radtManager.QueryBabies(patientInfo.ID);
                if (babyList != null && babyList.Count > 0)
                {
                    foreach (FS.HISFC.Models.RADT.PatientInfo baby in babyList)
                    {
                        FS.HISFC.Models.RADT.PatientInfo pTemp = radtManager.GetPatientInfo(baby.ID);
                        if (pTemp != null && !string.IsNullOrEmpty(pTemp.ID))
                        {
                            pTemp.PVisit = patientInfo.PVisit.Clone();

                            if (this.inpatientFeeManager.UpdateInMainInfoBalanced(pTemp, sysdate, balanceNO, new FS.HISFC.Models.Base.FT()) < 0)
                            {
                                this.RollBack();
                                this.err = this.inpatientFeeManager.Err;
                                return -1;
                            }
                        }
                    }
                }
            }

            #endregion

            #region 结算变更记录

            FS.FrameWork.Models.NeuObject oldObj = new FS.FrameWork.Models.NeuObject();
            FS.FrameWork.Models.NeuObject newObj = new FS.FrameWork.Models.NeuObject();
            newObj.ID = patientInfo.SIMainInfo.BalNo;
            newObj.Name = "结算序号";
            if (radtManager.SaveShiftData(patientInfo.ID, FS.HISFC.Models.Base.EnumShiftType.MB, "中途结算", oldObj, newObj) == -1)
            {
                this.RollBack();
                this.err = radtManager.Err;
                return -1;
            }

            #endregion

            #region 医保结算处理

            FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy medcareInterfaceProxy = new FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy();
            long returnValue = medcareInterfaceProxy.SetPactCode(patientInfo.Pact.ID);
            if (returnValue != 1)
            {
                this.RollBack();
                this.err = medcareInterfaceProxy.ErrMsg;
                return -1;
            }
            ////待遇接口处理－－－－－－－－－－－－－－－－－－－－－－－－－－－－
            medcareInterfaceProxy.SetTrans(this.Trans);
            medcareInterfaceProxy.Connect();
            returnValue = medcareInterfaceProxy.GetRegInfoInpatient(patientInfo);
            if (returnValue != 1)
            {
                this.RollBack();
                this.err = medcareInterfaceProxy.ErrMsg;
                return -1;
            }

            ArrayList alFeeItemList = new ArrayList(feeInfoList);
            returnValue = medcareInterfaceProxy.MidBalanceInpatient(patientInfo, ref alFeeItemList);
            if (returnValue != 1)
            {
                this.RollBack();
                this.err = medcareInterfaceProxy.ErrMsg;
                return -1;
            }

            returnValue = medcareInterfaceProxy.Disconnect();
            if (returnValue != 1)
            {
                this.RollBack();
                this.err = medcareInterfaceProxy.ErrMsg;
                return -1;
            }


            #endregion

            #region 更新托收单
            if (patientInfo.Pact.PayKind.ID == "03")
            {   //更新结算发票号
                if (this.Updategfhz(patientInfo, invoiceNO, "1", beginTime, endTime) == -1)
                {
                    this.RollBack();
                    this.err = "更新托收单出错!" + this.inpatientFeeManager.Err;
                    return -1;

                }
            }
            #endregion


            this.Commit();

            return 1;
        }

        #endregion

        #region 结算召回

        /// <summary>
        /// 取消结算
        /// </summary>
        /// <returns></returns>
        public int CancelBalance()
        {
            return 1;
        }

        #endregion

        #region 重新结算

        /// <summary>
        /// 重新结算（发票重打）
        /// </summary>
        /// <returns></returns>
        public int ReBalance(FS.HISFC.Models.RADT.PatientInfo patientInfo, List<FS.HISFC.Models.Fee.Inpatient.Balance> balanceList)
        {
            this.BeginTransaction();
            if (this.reBalance(patientInfo, balanceList, false, FS.HISFC.Models.Fee.EnumBalanceType.O) < 0)
            {
                this.RollBack();
                return -1;
            }

            this.Commit();

            return 1;
        }

        private int reBalance(FS.HISFC.Models.RADT.PatientInfo patientInfo, List<FS.HISFC.Models.Fee.Inpatient.Balance> balanceList, bool isChangeBalanceType, FS.HISFC.Models.Fee.EnumBalanceType enumBalanceType)
        {
            if (patientInfo == null || string.IsNullOrEmpty(patientInfo.ID))
            {
                this.err = "患者信息为空";
                return -1;
            }

            if (balanceList == null || balanceList.Count == 0)
            {
                this.err = "结算信息为空";
                return -1;
            }

            inpatientFeeManager.SetTrans(this.Trans);
            FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();
            feeIntegrate.SetTrans(this.Trans);

            //获取时间
            DateTime sysdate = inpatientFeeManager.GetDateTimeFromSysDateTime();

            //获取新的结算序号
            string balNO = "";
            //balNO = inpatientFeeManager.GetNewBalanceNO(patientInfo.ID);
            //if (balNO == "" || balNO == null)
            //{
            //    this.err = "获取新结算序号失败，" + inpatientFeeManager.Err;
            //    return -1;
            //}

            foreach (FS.HISFC.Models.Fee.Inpatient.Balance balance in balanceList)
            {
                balNO = balance.ID;
                //领取发票         
                string newInvoiceNO = string.Empty;
                string printInvoiceNo = string.Empty;
                if (feeIntegrate.GetInvoiceNO("I", ref newInvoiceNO, ref printInvoiceNo, ref this.err) != 1)
                {
                    this.err = "获取发票失败，" + feeIntegrate.Err;
                    return -1;
                }
                if (string.IsNullOrEmpty(newInvoiceNO))
                {
                    this.err = "请领取住院发票！";
                    return -1;
                }

                //更新feeinfo的结算发票号、结算序号：
                if (this.inpatientFeeManager.UpdateFeeInfoInvoiceNO(patientInfo.ID,
                     balance.Invoice.ID, balance.ID, newInvoiceNO, balNO) < 0)
                {
                    this.err = "更新feeinfo表出错!" + this.inpatientFeeManager.Err;
                    return -1;
                }

                //更新itemlist的结算发票号、结算序号：
                if (this.inpatientFeeManager.UpdateFeeItemListInvoiceNO(patientInfo.ID,
                    balance.Invoice.ID, int.Parse(balance.ID), newInvoiceNO, int.Parse(balNO)) < 0)
                {
                    this.err = "更新itemlist表出错!" + inpatientFeeManager.Err;
                    return -1;
                }

                //更新medicinelist的结算发票号、结算序号；
                if (this.inpatientFeeManager.UpdateFeeMedListInvoiceNO(patientInfo.ID,
                    balance.Invoice.ID, balance.ID, newInvoiceNO, balNO) < 0)
                {
                    this.err = "更新medicinelist表出错!" + inpatientFeeManager.Err;
                    return -1;
                }

                //更新预交金表的发票号、发票流水号；
                if (this.inpatientFeeManager.UpdateFeeInPrepayInvoiceNO(patientInfo.ID,
                    balance.Invoice.ID, balance.ID, newInvoiceNO, balNO) < 0)
                {
                    this.err = "更新inprepay表出错!" + inpatientFeeManager.Err;
                    return -1;
                }

                //更新住院主表--结算序号
                if (this.inpatientFeeManager.UpdateInMainInfoBalanceNO(patientInfo.ID, int.Parse(balNO)) < 0)
                {
                    this.err = "更新inmaininfo表出错!" + inpatientFeeManager.Err;
                    return -1;
                }

                //更新医保主表的结算序号、结算发票号；
                FS.HISFC.BizLogic.Fee.Interface SiMgr = new FS.HISFC.BizLogic.Fee.Interface();
                SiMgr.SetTrans(this.Trans);
                string oldBalanceNo = SiMgr.GetBalNo(patientInfo.ID);
                if (this.inpatientFeeManager.UpdateFeeSIinmaininfoInvoiceNO(patientInfo.ID,
                    balance.Invoice.ID, oldBalanceNo, newInvoiceNO, balNO) < 0)
                {
                    this.err = "更新siinmaininfo表出错!" + inpatientFeeManager.Err;
                    return -1;
                }

                #region 处理结算明细

                ArrayList alBalanceList = this.inpatientFeeManager.QueryBalanceListsByInpatientNOAndBalanceNO(patientInfo.ID, balance.Invoice.ID, int.Parse(balance.ID));

                if (alBalanceList == null)
                {
                    this.err = "提取结算明细数组出错!" + this.inpatientFeeManager.Err;
                    return -1;
                }
                //更新结算明细表中数据
                for (int i = 0; i < alBalanceList.Count; i++)
                {
                    FS.HISFC.Models.Fee.Inpatient.BalanceList Blist = alBalanceList[i] as FS.HISFC.Models.Fee.Inpatient.BalanceList;
                    //形成负记录

                    Blist.BalanceBase.TransType = FS.HISFC.Models.Base.TransTypes.Negative;

                    Blist.BalanceBase.FT.TotCost = -Blist.BalanceBase.FT.TotCost;
                    Blist.BalanceBase.FT.OwnCost = -Blist.BalanceBase.FT.OwnCost;
                    Blist.BalanceBase.FT.PayCost = -Blist.BalanceBase.FT.PayCost;
                    Blist.BalanceBase.FT.PubCost = -Blist.BalanceBase.FT.PubCost;
                    Blist.BalanceBase.FT.RebateCost = -Blist.BalanceBase.FT.RebateCost;
                    Blist.BalanceBase.FT.ArrearCost = -Blist.BalanceBase.FT.ArrearCost;
                    Blist.BalanceBase.BalanceOper.ID = this.inpatientFeeManager.Operator.ID;
                    Blist.BalanceBase.BalanceOper.OperTime = sysdate;
                    if (this.inpatientFeeManager.InsertBalanceList(patientInfo, Blist) == -1)
                    {
                        this.err = "插入结算明细负记录失败，" + this.inpatientFeeManager.Err;
                        return -1;
                    }
                    //形成正记录
                    Blist.BalanceBase.TransType = FS.HISFC.Models.Base.TransTypes.Positive;
                    Blist.BalanceBase.Invoice.ID = newInvoiceNO;
                    Blist.ID = balNO;
                    Blist.BalanceBase.FT.TotCost = -Blist.BalanceBase.FT.TotCost;
                    Blist.BalanceBase.FT.OwnCost = -Blist.BalanceBase.FT.OwnCost;
                    Blist.BalanceBase.FT.PayCost = -Blist.BalanceBase.FT.PayCost;
                    Blist.BalanceBase.FT.PubCost = -Blist.BalanceBase.FT.PubCost;
                    Blist.BalanceBase.FT.RebateCost = -Blist.BalanceBase.FT.RebateCost;
                    Blist.BalanceBase.FT.ArrearCost = -Blist.BalanceBase.FT.ArrearCost;
                    Blist.BalanceBase.BalanceOper.ID = this.inpatientFeeManager.Operator.ID;
                    Blist.BalanceBase.BalanceOper.OperTime = sysdate;
                    Blist.BalanceBase.ID = balNO;
                    #region add liuww
                    if (isChangeBalanceType)
                    {
                        Blist.BalanceBase.BalanceType.ID = FS.HISFC.Models.Fee.EnumBalanceType.J;
                    }
                    #endregion
                    if (this.inpatientFeeManager.InsertBalanceList(patientInfo, Blist) == -1)
                    {
                        this.err = "插入结算明细正记录失败，" + this.inpatientFeeManager.Err;
                        return -1;
                    }

                }


                #endregion

                #region 处理结算支付方式

                ArrayList alBalancePay = this.inpatientFeeManager.QueryBalancePaysByInvoiceNOAndBalanceNO(balance.Invoice.ID, int.Parse(balance.ID));
                if (alBalancePay == null)
                {
                    this.err = "提取结算明细数组出错，" + this.inpatientFeeManager.Err;
                    return -1;
                }

                foreach (FS.HISFC.Models.Fee.Inpatient.BalancePay bPay in alBalancePay)
                {
                    //负记录赋值
                    bPay.TransType = FS.HISFC.Models.Base.TransTypes.Negative;
                    bPay.FT.TotCost = -bPay.FT.TotCost;
                    bPay.Qty = -bPay.Qty;
                    bPay.BalanceOper.ID = this.inpatientFeeManager.Operator.ID;
                    bPay.BalanceOper.OperTime = sysdate;
                    bPay.BalanceNO = int.Parse(balance.ID);
                    if (this.inpatientFeeManager.InsertBalancePay(bPay) == -1)
                    {
                        this.RollBack();
                        this.err = "插入结算支付方式负记录失败，" + this.inpatientFeeManager.Err;
                        return -1;
                    }
                    //正记录赋值

                    bPay.TransType = FS.HISFC.Models.Base.TransTypes.Positive;
                    bPay.FT.TotCost = -bPay.FT.TotCost;
                    bPay.Qty = -bPay.Qty;
                    bPay.BalanceOper.ID = this.inpatientFeeManager.Operator.ID;
                    bPay.BalanceOper.OperTime = sysdate;
                    bPay.Invoice.ID = newInvoiceNO;
                    bPay.BalanceNO = int.Parse(balNO);
                    if (this.inpatientFeeManager.InsertBalancePay(bPay) == -1)
                    {
                        this.err = "插入结算支付方式正记录失败，" + this.inpatientFeeManager.Err;
                        return -1;
                    }
                }

                #endregion

                #region 处理结算信息

                FS.HISFC.Models.Base.CancelTypes cancelTypes = FS.HISFC.Models.Base.CancelTypes.Canceled;

                if (this.inpatientFeeManager.Operator.ID.Equals(balance.BalanceOper.ID) == false || balance.IsDayBalanced)
                {
                    cancelTypes = FS.HISFC.Models.Base.CancelTypes.Reprint;
                }

                //更新作废标记
                if (this.inpatientFeeManager.UpdateBalanceHeadWasteFlag(patientInfo.ID, int.Parse(balance.ID), ((int)cancelTypes).ToString(), sysdate, balance.Invoice.ID) <= 0) //并发
                {
                    this.err = "更新结算信息作废标记失败，" + this.inpatientFeeManager.Err;
                    return -1;
                }


                //负记录赋值

                decimal ReturnCost = balance.FT.ReturnCost;
                decimal SupplyCost = balance.FT.SupplyCost;
                balance.FT.TotCost = -balance.FT.TotCost;
                balance.FT.OwnCost = -balance.FT.OwnCost;
                balance.FT.PayCost = -balance.FT.PayCost;
                balance.FT.PubCost = -balance.FT.PubCost;
                balance.FT.RebateCost = -balance.FT.RebateCost;
                balance.FT.DerateCost = -balance.FT.DerateCost;
                balance.FT.TransferPrepayCost = -balance.FT.TransferPrepayCost;
                balance.FT.TransferTotCost = -balance.FT.TransferTotCost;
                balance.FT.BalancedPrepayCost = -balance.FT.BalancedPrepayCost;
                balance.FT.PrepayCost = -balance.FT.PrepayCost;
                balance.FT.SupplyCost = ReturnCost;
                balance.FT.ReturnCost = SupplyCost;
                balance.FT.ArrearCost = -balance.FT.ArrearCost;

                balance.TransType = FS.HISFC.Models.Base.TransTypes.Negative;
                balance.BalanceOper.ID = this.inpatientFeeManager.Operator.ID;
                balance.Oper.OperTime = sysdate;
                balance.BalanceOper.OperTime = sysdate; //add by liuww 2013 09 23
                balance.CancelType = cancelTypes;
                balance.CancelOper.ID = this.inpatientFeeManager.Operator.ID;
                //插入结算头表
                if (this.inpatientFeeManager.InsertBalance(patientInfo, balance) < 1)
                {
                    this.err = "插入结算信息负记录失败，" + this.inpatientFeeManager.Err;
                    return -1;
                }


                //正记录赋值
                balance.ID = balNO;
                balance.FT.TotCost = -balance.FT.TotCost;
                balance.FT.OwnCost = -balance.FT.OwnCost;
                balance.FT.PayCost = -balance.FT.PayCost;
                balance.FT.PubCost = -balance.FT.PubCost;
                balance.FT.RebateCost = -balance.FT.RebateCost;
                balance.FT.DerateCost = -balance.FT.DerateCost;
                balance.FT.TransferPrepayCost = -balance.FT.TransferPrepayCost;
                balance.FT.TransferTotCost = -balance.FT.TransferTotCost;
                balance.FT.BalancedPrepayCost = -balance.FT.BalancedPrepayCost;
                balance.FT.PrepayCost = -balance.FT.PrepayCost;
                balance.FT.SupplyCost = SupplyCost;
                balance.FT.ReturnCost = ReturnCost;
                balance.FT.ArrearCost = -balance.FT.ArrearCost;
                balance.TransType = FS.HISFC.Models.Base.TransTypes.Positive;
                balance.BalanceOper.ID = this.inpatientFeeManager.Operator.ID;
                balance.Oper.OperTime = sysdate;
                balance.BalanceOper.OperTime = sysdate; //add by liuww 2013 09 23
                balance.CancelType = FS.HISFC.Models.Base.CancelTypes.Valid;
                balance.Invoice.ID = newInvoiceNO;
                balance.PrintedInvoiceNO = printInvoiceNo;
                if (balance.FT.ArrearCost > 0)
                {
                    balance.FT.SupplyCost += balance.FT.ArrearCost;
                }
                balance.IsTempInvoice = false;//已经是真实发票号

                #region add liuww
                if (isChangeBalanceType)
                {
                    balance.BalanceType.ID = FS.HISFC.Models.Fee.EnumBalanceType.J;
                }
                #endregion

                //插入正记录

                //插入结算头表
                if (this.inpatientFeeManager.InsertBalance(patientInfo, balance) < 1)
                {
                    this.err = "插入结算信息正记录失败，" + this.inpatientFeeManager.Err;
                    return -1;
                }
                #endregion

                //发票走号
                if (feeIntegrate.UseInvoiceNO("I", 1, ref newInvoiceNO, ref printInvoiceNo, ref this.err) != 1)
                {
                    this.err = "发票走号出错!" + feeIntegrate.Err;
                    return -1;
                }

            }


            #region 处理变更记录

            FS.HISFC.Models.Fee.Inpatient.Balance balanceMain = balanceList[0];
            FS.FrameWork.Models.NeuObject oldObj = new FS.FrameWork.Models.NeuObject();
            FS.FrameWork.Models.NeuObject newObj = new FS.FrameWork.Models.NeuObject();
            oldObj.ID = balanceMain.ID;
            oldObj.Name = "原结算序号";
            newObj.ID = balNO;
            newObj.Name = "新结算序号";
            RADT radtManager = new RADT();
            radtManager.SetTrans(this.Trans);

            //添加记录
            if (radtManager.SaveShiftData(patientInfo.ID, FS.HISFC.Models.Base.EnumShiftType.BB, "发票重打", oldObj, newObj) == -1)
            {
                this.err = "保存变更记录失败，" + radtManager.Err;
                return -1;
            }

            #endregion

            return 1;
        }

        #endregion

        #region 欠费清账

        /// <summary>
        /// 欠费平帐结算
        /// </summary>
        /// <returns></returns>
        public int ArrearBalance(FS.HISFC.Models.RADT.PatientInfo patientInfo, List<FS.HISFC.Models.Fee.Inpatient.Balance> balanceList, List<FS.HISFC.Models.Fee.Inpatient.BalancePay> balancePayList, FS.HISFC.Models.Base.TransTypes transType)
        {
            if (patientInfo == null || patientInfo.ID == null || patientInfo.ID.Length == 0)
            {
                this.err = "患者信息为空！";
                return -1;
            }
            if (balanceList == null || balanceList.Count == 0)
            {
                this.err = "结算记录为空！";
                return -1;
            }

            this.BeginTransaction();
            this.inpatientFeeManager.SetTrans(this.Trans);
            DateTime sysdate = this.inpatientFeeManager.GetDateTimeFromSysDateTime();

            List<FS.HISFC.Models.Fee.Inpatient.Balance> alTempBalance = new List<FS.HISFC.Models.Fee.Inpatient.Balance>();

            foreach (FS.HISFC.Models.Fee.Inpatient.Balance balance in balanceList)
            {
                //平帐
                if (transType == FS.HISFC.Models.Base.TransTypes.Positive)
                {
                    //如果是临时发票号，重新结算
                    if (balance.IsTempInvoice)
                    {
                        alTempBalance.Add(balance);
                    }
                    else//处理支付方式
                    {
                        #region 处理结算支付方式

                        ArrayList alBalancePay = this.inpatientFeeManager.QueryBalancePaysByInvoiceNOAndBalanceNO(balance.Invoice.ID, int.Parse(balance.ID));
                        if (alBalancePay == null)
                        {
                            this.RollBack();
                            this.err = "提取结算明细数组出错，" + this.inpatientFeeManager.Err;
                            return -1;
                        }

                        foreach (FS.HISFC.Models.Fee.Inpatient.BalancePay bPay in alBalancePay)
                        {
                            if (bPay.PayType.ID.Equals("QF") == false)
                            {
                                continue;
                            }

                            //负记录赋值
                            bPay.TransType = FS.HISFC.Models.Base.TransTypes.Negative;
                            bPay.FT.TotCost = -bPay.FT.TotCost;
                            bPay.Qty = -bPay.Qty;
                            bPay.BalanceOper.ID = this.inpatientFeeManager.Operator.ID;
                            bPay.BalanceOper.OperTime = sysdate;
                            bPay.BalanceNO = int.Parse(balance.ID);
                            if (this.inpatientFeeManager.InsertBalancePay(bPay) == -1)
                            {
                                this.RollBack();
                                this.err = "插入结算支付方式负记录失败，" + this.inpatientFeeManager.Err;
                                return -1;
                            }
                        }
                        decimal arraerCost = balance.FT.ArrearCost;
                        int count = balancePayList.Count;
                        while (arraerCost > 0)
                        {
                            FS.HISFC.Models.Fee.Inpatient.BalancePay balancePayClone = balancePayList[0].Clone();
                            balancePayClone.Invoice.ID = balance.Invoice.ID;
                            balancePayClone.BalanceNO = int.Parse(balance.ID);
                            balancePayClone.BalanceOper.ID = inpatientFeeManager.Operator.ID;
                            balancePayClone.BalanceOper.OperTime = sysdate;
                            if (balancePayClone.FT.TotCost >= arraerCost)
                            {
                                balancePayClone.FT.TotCost = arraerCost;
                                balancePayList[0].FT.TotCost -= arraerCost;
                            }

                            if (this.inpatientFeeManager.InsertBalancePay(balancePayClone) == -1)
                            {
                                this.RollBack();
                                this.err = "插入结算支付方式负记录失败，" + this.inpatientFeeManager.Err;
                                return -1;
                            }

                            arraerCost = arraerCost - balancePayClone.FT.TotCost;

                            if (balancePayList[0].FT.TotCost <= 0)
                            {
                                balancePayList.RemoveAt(0);
                            }
                        }

                        #endregion
                    }
                }
            }

            if (alTempBalance.Count > 0)
            {
                if (this.reBalance(patientInfo, alTempBalance, true, FS.HISFC.Models.Fee.EnumBalanceType.J) < 0)
                {
                    this.RollBack();
                    return -1;
                }
                foreach (FS.HISFC.Models.Fee.Inpatient.Balance balance in alTempBalance)
                {
                    //处理支付方式
                    decimal arraerCost = balance.FT.ArrearCost;
                    int count = balancePayList.Count;
                    while (arraerCost > 0)
                    {
                        FS.HISFC.Models.Fee.Inpatient.BalancePay balancePayClone = balancePayList[0].Clone();
                        balancePayClone.Invoice.ID = balance.Invoice.ID;
                        balancePayClone.BalanceNO = int.Parse(balance.ID);
                        balancePayClone.BalanceOper.ID = inpatientFeeManager.Operator.ID;
                        balancePayClone.BalanceOper.OperTime = sysdate;
                        if (balancePayClone.FT.TotCost >= arraerCost)
                        {
                            balancePayClone.FT.TotCost = arraerCost;
                            balancePayList[0].FT.TotCost -= arraerCost;
                        }

                        if (this.inpatientFeeManager.InsertBalancePay(balancePayClone) == -1)
                        {
                            this.RollBack();
                            this.err = "插入结算支付方式负记录失败，" + this.inpatientFeeManager.Err;
                            return -1;
                        }

                        arraerCost = arraerCost - balancePayClone.FT.TotCost;

                        if (balancePayList[0].FT.TotCost <= 0)
                        {
                            balancePayList.RemoveAt(0);
                        }
                    }
                }
            }

            foreach (FS.HISFC.Models.Fee.Inpatient.Balance balance in balanceList)
            {
                if (this.inpatientFeeManager.UpdateBalanceSaveInfo(balance, transType == FS.HISFC.Models.Base.TransTypes.Positive ? "1" : "0") <= 0)
                {
                    this.RollBack();
                    this.err = "欠费平帐失败，原因：" + this.inpatientFeeManager.Err;
                    return -1;
                }
            }

            this.Commit();

            return 1;
        }

        #endregion

        #region 发票打印
        /// <summary>
        /// 打印发票
        /// </summary>
        /// <param name="balance"></param>
        /// <param name="alBalanceListHead"></param>
        /// <param name="alBalancePay"></param>
        private int PrintInvoice(FS.HISFC.Models.RADT.PatientInfo patientInfo, List<FS.HISFC.Models.Fee.Inpatient.Balance> list, ref string errText)
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
                ArrayList alBalanceListHead = inpatientFeeManager.QueryBalanceListsByInpatientNOAndBalanceNO(balance.Patient.ID, balance.Invoice.ID, FS.FrameWork.Function.NConvert.ToInt32(balance.ID));
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

        /// <summary>
        /// 更新托收单
        /// </summary>
        /// <returns></returns>
        private int Updategfhz(FS.HISFC.Models.RADT.PatientInfo patient, string invoiceNo, string strInState)
        {
            //strInState：0出院结算，1在院结算
            //托收单单号
            string strId = "";
            string begindate = this.inpatientFeeManager.GetLastMidBalanceDate(patient);
            if (begindate == "-1")
            {
                this.err = this.inpatientFeeManager.Err;
                return -1;
            }

            strId = this.inpatientFeeManager.GetgfzhID(patient.ID, strInState,
                FS.FrameWork.Function.NConvert.ToDateTime(begindate).AddSeconds(1).Date.ToString(),
                DateTime.Parse(this.inpatientFeeManager.GetDateTimeFromSysDateTime().Date.ToString("d") + " 23:59:59").ToString());

            if (strId == null || strId == "-1")
            {
                this.err = "未找到托收单!";
                return -1;
            }


            if (this.inpatientFeeManager.UpdatePubReport(strId, invoiceNo) == -1) return -1;

            return 0;

        }


        /// <summary>
        /// 更新托收单
        /// </summary>
        /// <returns></returns>
        private int Updategfhz(FS.HISFC.Models.RADT.PatientInfo patient, string invoiceNo, string strInState, DateTime beginTime, DateTime endTime)
        {
            //strInState：0出院结算，1在院结算
            //托收单单号
            string strId = this.inpatientFeeManager.GetgfzhID(patient.ID, strInState,
                beginTime.ToString("yyyy-MM-dd HH:mm:ss"),
                endTime.ToString("yyyy-MM-dd HH:mm:ss"));

            if (strId == null || strId == "-1")
            {
                this.err = "未找到托收单!";
                return -1;
            }


            if (this.inpatientFeeManager.UpdatePubReport(strId, invoiceNo) == -1) return -1;

            return 0;

        }

        private string GetTansType(FS.HISFC.Models.Base.TransTypes TransTypes)
        {
            switch (TransTypes)
            {
                case FS.HISFC.Models.Base.TransTypes.Positive:
                    return "1";
                case FS.HISFC.Models.Base.TransTypes.Negative:
                    return "2";
            }
            return "";
        }


        //{18AD984D-78C6-4c64-8066-6F31E3BDF7DA}
        /// <summary>
        /// 把一条费用记录分成两条
        /// </summary>
        /// <param name="SplitItem">被拆分的收费记录</param>
        /// <param name="SplitNum">分出来的记录中的itemList.Item.Qty</param>
        /// <returns>1---成功，-1---失败</returns>
        public int SplitUndrugItem(FS.HISFC.Models.Fee.Inpatient.FeeItemList SplitItem, decimal SplitNum, ref FS.HISFC.Models.Fee.Inpatient.FeeItemList newItem, ref string ErrInfo)
        {
            try
            {

                if (SplitNum == 0 || SplitItem.NoBackQty == 0)
                {
                    ErrInfo = "拆分数量或者原费用可退数量为0";
                    return -1;
                }


                if (SplitNum > SplitItem.NoBackQty || SplitNum > SplitItem.Item.Qty)
                {
                    ErrInfo = "可拆分数量不足";
                    return -1;
                }

                //查询出来的SplitItem的DefTotCost是未赋值的状态，需要计算
                SplitItem.FT.DefTotCost = SplitItem.Item.DefPrice * SplitItem.Item.Qty;

                FS.HISFC.Models.Fee.Inpatient.FeeItemList itemList = new FS.HISFC.Models.Fee.Inpatient.FeeItemList();

                itemList.RecipeNO = SplitItem.RecipeNO;//0 处方号
                itemList.SequenceNO = SplitItem.SequenceNO;//1处方内项目流水号
                itemList.TransType = SplitItem.TransType;//2交易类型,1正交易，2反交易
                itemList.ID = SplitItem.ID;//3住院流水号
                itemList.Patient.ID = SplitItem.Patient.ID;//3住院流水号
                itemList.Name = SplitItem.Name;//4姓名
                itemList.Patient.Name = SplitItem.Patient.Name;//4姓名
                itemList.Patient.Pact.PayKind.ID = SplitItem.Patient.Pact.PayKind.ID;//5结算类别
                itemList.Patient.Pact.ID = SplitItem.Patient.Pact.ID;//6合同单位
                itemList.UpdateSequence = SplitItem.UpdateSequence;//7更新库存的流水号(物资)
                ((FS.HISFC.Models.RADT.PatientInfo)itemList.Patient).PVisit.PatientLocation.Dept.ID = ((FS.HISFC.Models.RADT.PatientInfo)SplitItem.Patient).PVisit.PatientLocation.Dept.ID;//8在院科室代码
                ((FS.HISFC.Models.RADT.PatientInfo)itemList.Patient).PVisit.PatientLocation.NurseCell.ID = ((FS.HISFC.Models.RADT.PatientInfo)SplitItem.Patient).PVisit.PatientLocation.NurseCell.ID;//9护士站代码
                itemList.Order.Patient.PVisit.PatientLocation.Dept.ID = SplitItem.Order.Patient.PVisit.PatientLocation.Dept.ID;
                itemList.Order.Patient.PVisit.PatientLocation.NurseCell.ID = SplitItem.Order.Patient.PVisit.PatientLocation.NurseCell.ID;

                itemList.RecipeOper.Dept.ID = SplitItem.RecipeOper.Dept.ID;//10开立科室代码
                itemList.ExecOper.Dept.ID = SplitItem.ExecOper.Dept.ID;//11执行科室代码
                itemList.StockOper.Dept.ID = SplitItem.StockOper.Dept.ID;//12扣库科室代码
                itemList.RecipeOper.ID = SplitItem.RecipeOper.ID;//13开立医师代码
                itemList.Item.ID = SplitItem.Item.ID;//14项目代码
                itemList.Item.MinFee.ID = SplitItem.Item.MinFee.ID;//15最小费用代码
                itemList.Compare.CenterItem.ID = SplitItem.Compare.CenterItem.ID;//16中心代码
                itemList.Item.Name = SplitItem.Item.Name;//17项目名称
                itemList.Item.Price = SplitItem.Item.Price;//18单价1
                itemList.Item.Qty = SplitNum;//9数量   
                itemList.Item.PriceUnit = SplitItem.Item.PriceUnit;//20当前单位
                itemList.UndrugComb.ID = SplitItem.UndrugComb.ID;//21组套代码
                itemList.UndrugComb.Name = SplitItem.UndrugComb.Name;//22组套名称
                itemList.FT.TotCost = Math.Round((SplitItem.FT.TotCost * itemList.Item.Qty) / SplitItem.Item.Qty, 2, MidpointRounding.AwayFromZero);//23费用金额  
                itemList.FT.OwnCost = Math.Round((SplitItem.FT.OwnCost * itemList.Item.Qty) / SplitItem.Item.Qty, 2, MidpointRounding.AwayFromZero);//24自费金额   
                itemList.FT.PayCost = Math.Round((SplitItem.FT.PayCost * itemList.Item.Qty) / SplitItem.Item.Qty, 2, MidpointRounding.AwayFromZero);//25自付金额 
                itemList.FT.PubCost = Math.Round((SplitItem.FT.PubCost * itemList.Item.Qty) / SplitItem.Item.Qty, 2, MidpointRounding.AwayFromZero);//26公费金额  
                itemList.FT.DonateCost = Math.Round((SplitItem.FT.DonateCost * itemList.Item.Qty) / SplitItem.Item.Qty, 2, MidpointRounding.AwayFromZero);//26公费金额  
                itemList.FT.RebateCost = Math.Round((SplitItem.FT.RebateCost * itemList.Item.Qty) / SplitItem.Item.Qty, 2, MidpointRounding.AwayFromZero);//27优惠金额  
                itemList.FT.DefTotCost = Math.Round((SplitItem.FT.DefTotCost * itemList.Item.Qty) / SplitItem.Item.Qty, 2, MidpointRounding.AwayFromZero);//27优惠金额  
                itemList.SendSequence = SplitItem.SendSequence;//28出库单序列号
                itemList.PayType = SplitItem.PayType;//29收费状态
                itemList.IsBaby = SplitItem.IsBaby;//30是否婴儿用
                ((FS.HISFC.Models.Order.Inpatient.Order)itemList.Order).OrderType.ID = ((FS.HISFC.Models.Order.Inpatient.Order)SplitItem.Order).OrderType.ID;//32出院带疗标记
                itemList.Invoice.ID = SplitItem.Invoice.ID;//33结算发票号
                itemList.BalanceNO = SplitItem.BalanceNO;//34结算序号
                itemList.ChargeOper.ID = SplitItem.ChargeOper.ID;//36划价人
                itemList.ChargeOper.OperTime = SplitItem.ChargeOper.OperTime;//37划价日期

                decimal newConfirmQty = 0.0m;
                if (SplitItem.ConfirmedQty > SplitNum)
                {
                    newConfirmQty = SplitNum;
                }
                itemList.ConfirmedQty = newConfirmQty;//38确认数量
                itemList.MachineNO = SplitItem.MachineNO;//39设备号
                itemList.ExecOper.ID = SplitItem.ExecOper.ID;//40执行人代码
                itemList.ExecOper.OperTime = SplitItem.ExecOper.OperTime;//41执行日期
                itemList.FeeOper.ID = SplitItem.FeeOper.ID;//42计费人
                itemList.FeeOper.OperTime = SplitItem.FeeOper.OperTime;//43计费日期
                itemList.AuditingNO = SplitItem.AuditingNO;//45审核序号
                itemList.Order.ID = SplitItem.Order.ID;//46医嘱流水号
                itemList.ExecOrder.ID = SplitItem.ExecOrder.ID;//47医嘱执行单流水号
                itemList.NoBackQty = itemList.Item.Qty;
                itemList.BalanceState = SplitItem.BalanceState;//51结算状态
                itemList.FTRate.ItemRate = SplitItem.FTRate.ItemRate;//52收费比例
                itemList.FTRate.OwnRate = SplitItem.FTRate.OwnRate;
                itemList.ExtCode = SplitItem.ExtCode;//52退费原记录的处方号
                itemList.FTSource = SplitItem.FTSource;
                itemList.Item.ItemType = SplitItem.Item.ItemType;
                itemList.MedicalTeam.ID = SplitItem.MedicalTeam.ID; //增加医疗组处理
                itemList.OperationNO = SplitItem.OperationNO; // 手术编码
                itemList.User03 = SplitItem.User03;  //医保上传标记
                itemList.Item.SpecialFlag4 = SplitItem.Item.SpecialFlag4; //特批标记
                itemList.Item.DefPrice = SplitItem.Item.DefPrice;
                itemList.UndrugComb.Qty = SplitItem.UndrugComb.Qty;
                itemList.SplitID = SplitItem.SplitID;
                itemList.SplitFlag = SplitItem.SplitFlag;
                itemList.SplitFeeFlag = SplitItem.SplitFeeFlag;
                itemList.ExecOrder.DateUse = SplitItem.ExecOrder.DateUse;

                //旧记录费用更新
                SplitItem.Item.Qty -= itemList.Item.Qty;
                SplitItem.NoBackQty -= itemList.Item.Qty;
                SplitItem.ConfirmedQty -= newConfirmQty;

                SplitItem.FT.TotCost -= itemList.FT.TotCost;
                SplitItem.FT.OwnCost -= itemList.FT.OwnCost;
                SplitItem.FT.PayCost -= itemList.FT.PayCost;
                SplitItem.FT.PubCost -= itemList.FT.PubCost;
                SplitItem.FT.DonateCost -= itemList.FT.DonateCost;
                SplitItem.FT.RebateCost -= itemList.FT.RebateCost;
                SplitItem.FT.DefTotCost -= itemList.FT.DefTotCost;
                newItem = itemList;
            }
            catch (Exception ex)
            {
                ErrInfo = ex.Message;
                return -1;
            }


            return 1;
        }

        public int SplitMedicineItem(FS.HISFC.Models.Fee.Inpatient.FeeItemList SplitItem, decimal SplitNum, ref FS.HISFC.Models.Fee.Inpatient.FeeItemList newItem, ref string ErrInfo)
        {
            try
            {
                //可退数量为零时不可拆分
                if (SplitNum == 0 || SplitItem.NoBackQty == 0)
                {
                    ErrInfo = "拆分数量或者原费用可退数量为0";
                    return -1;
                }

                if (SplitNum > SplitItem.NoBackQty || SplitNum > SplitItem.Item.Qty)
                {
                    ErrInfo = "可拆分数量不足！";
                    return -1;
                }

                FS.HISFC.Models.Fee.Inpatient.FeeItemList itemList = new FS.HISFC.Models.Fee.Inpatient.FeeItemList();
                FS.HISFC.Models.Pharmacy.Item pharmacyItem = new FS.HISFC.Models.Pharmacy.Item();
                itemList.Item = pharmacyItem;

                itemList.RecipeNO = SplitItem.RecipeNO;//0 处方号
                itemList.SequenceNO = SplitItem.SequenceNO;//1处方内项目流水号
                itemList.TransType = SplitItem.TransType;//2交易类型,1正交易，2反交易
                itemList.ID = SplitItem.ID;//3住院流水号
                itemList.Patient.ID = SplitItem.Patient.ID;//3住院流水号
                itemList.Name = SplitItem.Name;//4姓名
                itemList.Patient.Name = SplitItem.Patient.Name;//4姓名
                itemList.Patient.Pact.PayKind.ID = SplitItem.Patient.Pact.PayKind.ID;//5结算类别
                itemList.Patient.Pact.ID = SplitItem.Patient.Pact.ID;//6合同单位
                itemList.UpdateSequence = SplitItem.UpdateSequence;//7更新库存的流水号(物资)
                ((FS.HISFC.Models.RADT.PatientInfo)itemList.Patient).PVisit.PatientLocation.Dept.ID = ((FS.HISFC.Models.RADT.PatientInfo)SplitItem.Patient).PVisit.PatientLocation.Dept.ID;//8在院科室代码
                ((FS.HISFC.Models.RADT.PatientInfo)itemList.Patient).PVisit.PatientLocation.NurseCell.ID = ((FS.HISFC.Models.RADT.PatientInfo)SplitItem.Patient).PVisit.PatientLocation.NurseCell.ID;//9护士站代码
                itemList.Order.Patient.PVisit.PatientLocation.Dept.ID = SplitItem.Order.Patient.PVisit.PatientLocation.Dept.ID;
                itemList.Order.Patient.PVisit.PatientLocation.NurseCell.ID = SplitItem.Order.Patient.PVisit.PatientLocation.NurseCell.ID;

                itemList.RecipeOper.Dept.ID = SplitItem.RecipeOper.Dept.ID;//10开立科室代码
                itemList.ExecOper.Dept.ID = SplitItem.ExecOper.Dept.ID;//11执行科室代码
                itemList.StockOper.Dept.ID = SplitItem.StockOper.Dept.ID;//12扣库科室代码
                itemList.RecipeOper.ID = SplitItem.RecipeOper.ID;//13开立医师代码
                itemList.Item.ID = SplitItem.Item.ID;//14项目代码
                itemList.Item.MinFee.ID = SplitItem.Item.MinFee.ID;//15最小费用代码
                itemList.Compare.CenterItem.ID = SplitItem.Compare.CenterItem.ID;//16中心代码
                itemList.Item.Name = SplitItem.Item.Name;//17项目名称
                itemList.Item.Price = SplitItem.Item.Price;//18单价1
                itemList.Item.Qty = SplitNum;//9数量
                itemList.Item.PriceUnit = SplitItem.Item.PriceUnit;//20当前单位
                itemList.Item.PackQty = SplitItem.Item.PackQty;//21包装数量
                itemList.Days = SplitItem.Days;//22付数
                itemList.FT.TotCost = Math.Round((SplitItem.FT.TotCost * itemList.Item.Qty) / SplitItem.Item.Qty, 2, MidpointRounding.AwayFromZero);//23费用金额
                itemList.FT.OwnCost = Math.Round((SplitItem.FT.OwnCost * itemList.Item.Qty) / SplitItem.Item.Qty, 2, MidpointRounding.AwayFromZero);//24自费金额
                itemList.FT.PayCost = Math.Round((SplitItem.FT.PayCost * itemList.Item.Qty) / SplitItem.Item.Qty, 2, MidpointRounding.AwayFromZero);//25自付金额
                itemList.FT.PubCost = Math.Round((SplitItem.FT.PubCost * itemList.Item.Qty) / SplitItem.Item.Qty, 2, MidpointRounding.AwayFromZero);//26公费金额
                itemList.FT.DonateCost = Math.Round((SplitItem.FT.DonateCost * itemList.Item.Qty) / SplitItem.Item.Qty, 2, MidpointRounding.AwayFromZero);//26公费金额
                itemList.FT.RebateCost = (SplitItem.FT.RebateCost * itemList.Item.Qty) / SplitItem.Item.Qty;//27优惠金额
                itemList.SendSequence = SplitItem.SendSequence;//28出库单序列号
                itemList.PayType = SplitItem.PayType;//29收费状态
                itemList.IsBaby = SplitItem.IsBaby;//30是否婴儿用
                ((FS.HISFC.Models.Order.Inpatient.Order)itemList.Order).OrderType.ID = ((FS.HISFC.Models.Order.Inpatient.Order)SplitItem.Order).OrderType.ID;//32出院带疗标记
                itemList.Invoice.ID = SplitItem.Invoice.ID;//33结算发票号
                itemList.BalanceNO = SplitItem.BalanceNO;//34结算序号
                itemList.ChargeOper.ID = SplitItem.ChargeOper.ID;//36划价人
                itemList.ChargeOper.OperTime = SplitItem.ChargeOper.OperTime;//37划价日期
                pharmacyItem.Product.IsSelfMade = ((FS.HISFC.Models.Pharmacy.Item)SplitItem.Item).Product.IsSelfMade;//38自制标识
                pharmacyItem.Quality.ID = ((FS.HISFC.Models.Pharmacy.Item)SplitItem.Item).Quality.ID;//39药品性质
                itemList.ExecOper.ID = SplitItem.ExecOper.ID;//40发药人代码
                itemList.ExecOper.OperTime = SplitItem.ExecOper.OperTime;//41发药日期
                itemList.FeeOper.ID = SplitItem.FeeOper.ID;//42计费人
                itemList.FeeOper.OperTime = SplitItem.FeeOper.OperTime;//43计费日期
                itemList.AuditingNO = SplitItem.AuditingNO;//45审核序号
                itemList.Order.ID = SplitItem.Order.ID;//46医嘱流水号
                itemList.ExecOrder.ID = SplitItem.ExecOrder.ID;//47医嘱执行单流水号
                pharmacyItem.Specs = ((FS.HISFC.Models.Pharmacy.Item)SplitItem.Item).Specs;//规格
                pharmacyItem.Type.ID = ((FS.HISFC.Models.Pharmacy.Item)SplitItem.Item).Type.ID;//49药品类别
                pharmacyItem.ItemType = ((FS.HISFC.Models.Pharmacy.Item)SplitItem.Item).ItemType;
                itemList.NoBackQty = itemList.Item.Qty;//50可退数量
                itemList.BalanceState = SplitItem.BalanceState;//51结算状态
                itemList.FTRate.ItemRate = SplitItem.FTRate.ItemRate;//52收费比例
                itemList.FTRate.OwnRate = SplitItem.FTRate.OwnRate;
                itemList.FeeOper.Dept.ID = SplitItem.FeeOper.Dept.ID;//53收费员科室
                itemList.Item.ItemType = SplitItem.Item.ItemType;
                itemList.FTSource = SplitItem.FTSource.Clone();
                itemList.MedicalTeam.ID = SplitItem.MedicalTeam.ID;//增加医疗组处理
                itemList.OperationNO = SplitItem.OperationNO;// 手术编码
                itemList.Item.DefPrice = SplitItem.Item.DefPrice; // 默认价格总金额
                itemList.FT.DefTotCost = Math.Round((SplitItem.FT.DefTotCost * itemList.Item.Qty) / SplitItem.Item.Qty, 2, MidpointRounding.AwayFromZero);
                itemList.User03 = SplitItem.User03; //医保上传标记
                itemList.ExecOrder.DateUse = SplitItem.ExecOrder.DateUse;
                itemList.Order.DoseOnce = SplitItem.Order.DoseOnce;
                itemList.Order.Frequency.ID = SplitItem.Order.Frequency.ID;
                itemList.Order.Usage.ID = SplitItem.Order.Usage.ID;
                itemList.Order.Combo.ID = SplitItem.Order.Combo.ID;
                itemList.Order.OrderType.ID = SplitItem.Order.OrderType.ID;

                //旧记录费用更新
                SplitItem.Item.Qty -= itemList.Item.Qty;
                SplitItem.NoBackQty -= itemList.Item.Qty;

                SplitItem.FT.TotCost -= itemList.FT.TotCost;
                SplitItem.FT.OwnCost -= itemList.FT.OwnCost;
                SplitItem.FT.PayCost -= itemList.FT.PayCost;
                SplitItem.FT.PubCost -= itemList.FT.PubCost;
                SplitItem.FT.DonateCost -= itemList.FT.DonateCost;
                SplitItem.FT.RebateCost -= itemList.FT.RebateCost;
                SplitItem.FT.DefTotCost -= itemList.FT.DefTotCost;
                newItem = itemList;
            }
            catch (Exception ex)
            {
                ErrInfo = ex.Message;
                return -1;
            }

            return 1;
        }

    }
}
