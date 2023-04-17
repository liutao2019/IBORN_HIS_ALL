using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using FS.HISFC.Models.Fee.Outpatient;

namespace FS.HISFC.BizProcess.Integrate.AccountFee
{
    /// <summary>
    /// 预交金流程，终端扣费管理类
    /// {42CDFA33-9FE5-42b0-BBC5-533922960DE8}
    /// </summary>
    public class OutPatientFeeManage : IntegrateBase
    {
        #region 变量

        /// <summary>
        /// 预交金流程，终端扣费发票管理类
        /// </summary>
        private OutPatientInvoiceManage invoiceManager = null;

        /// <summary>
        /// 发票管理类
        /// </summary>
        public OutPatientInvoiceManage InvoiceManager
        {
            get
            {
                if (invoiceManager == null)
                {
                    invoiceManager = new OutPatientInvoiceManage();
                }
                return invoiceManager;
            }
        }

        /// <summary>
        /// 费用管理类
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Fee feeManager = null;

        /// <summary>
        /// 费用管理类
        /// </summary>
        public FS.HISFC.BizProcess.Integrate.Fee FeeManager
        {
            get
            {
                if (feeManager == null)
                {
                    feeManager = new Fee();
                }
                return feeManager;
            }
        }

        /// <summary>
        /// 本地医疗待遇接口
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy medcareProxy = null;

        /// <summary>
        /// 本地医疗待遇接口
        /// </summary>
        public FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy MedcareProxy
        {
            get
            {
                if (medcareProxy == null)
                {
                    medcareProxy = new FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy();
                }
                return medcareProxy;
            }
        }

        /// <summary>
        /// 账户管理
        /// </summary>
        private FS.HISFC.BizLogic.Fee.Account accountMgr = null;

        /// <summary>
        /// 账户管理
        /// </summary>
        public FS.HISFC.BizLogic.Fee.Account AccountMgr
        {
            get
            {
                if (accountMgr == null)
                {
                    accountMgr = new FS.HISFC.BizLogic.Fee.Account();
                }
                return accountMgr;
            }
        }

        /// <summary>
        /// 入出转管理
        /// </summary>
        private HISFC.BizProcess.Integrate.RADT radtMgr = null;

        /// <summary>
        /// 入出转管理
        /// </summary>
        public HISFC.BizProcess.Integrate.RADT RadtMgr
        {
            get
            {
                if (radtMgr == null)
                {
                    radtMgr = new RADT();
                }
                return radtMgr;
            }
        }
        #endregion

        /// <summary>
        /// 预交金流程收费操作
        /// </summary>
        /// <param name="r">病人挂号信息</param>
        /// <param name="feeItemList">费用明细信息</param>
        /// <param name="strMsg">提示信息</param>
        /// <returns> -1 失败，0 账户余额不足，1 成功扣费 </returns>
        public int ChargeFee(FS.HISFC.Models.Registration.Register r, ArrayList feeItemList, out string strMsg)
        {
            strMsg = "";
            int lngRes = 1;
            if (r == null || string.IsNullOrEmpty(r.ID) || string.IsNullOrEmpty(r.Pact.ID))
            {
                strMsg = "患者信息为空！";
                return -1;
            }

            // {9635BF11-D633-409e-8880-2DB29CB830F7}
            if (FS.HISFC.BizProcess.Integrate.AccountFee.Function.LstUnTerminalPactCode.Contains(r.Pact.ID))
            {
                lngRes = -1;
                strMsg = r.Pact.Name + " 身份病人，请到收费处收费！";
                return lngRes;
            }

            if (feeItemList == null || feeItemList.Count <= 0)
            {
                strMsg = "患者费用信息为空！";
                return 1;
            }

            FS.HISFC.Models.Base.Employee employee = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
            // 医生站自助扣费
            // 指定一个固定终端员工
            if (!employee.ID.StartsWith("T"))
            {
                // 系统必须定义一个 T00001 的员工 为医生站扣费时分配发票用
                employee = new FS.HISFC.Models.Base.Employee();
                employee.ID = "T00001"; // 终端全院
                employee.Name = "T-全院";
                employee.UserCode = "99";
            }

            if (this.trans == null)
            {
                FS.FrameWork.Management.PublicTrans.BeginTransaction();
            }

            InvoiceManager.SetTrans(this.trans);
            FeeManager.SetTrans(this.trans);

            MedcareProxy.BeginTranscation();
            MedcareProxy.SetPactCode(r.Pact.ID);
            MedcareProxy.IsLocalProcess = true;

            if (!MedcareProxy.IsInBlackList(r))
            {
                lngRes = MedcareProxy.LocalBalanceOutpatient(r, ref feeItemList, null);
                if (lngRes <= 0)
                {
                    strMsg = MedcareProxy.ErrMsg;
                    //FS.FrameWork.Management.PublicTrans.RollBack();
                    return lngRes;
                }
            }
            else
            {
                // 特殊处理
                // {9832026E-02FE-4118-A3F5-51C20E79742B}
                if (Function.HospitalCode == "A-19")
                {
                    // 南庄医院特殊处理 -- 老年减免医保-6 第二次报销时按 老年减免自费-7 减免
                    switch (r.Pact.ID)
                    {
                        case "6":
                            r.Pact.ID = "7";
                            MedcareProxy.SetPactCode(r.Pact.ID);
                            MedcareProxy.IsLocalProcess = true;
                            lngRes = MedcareProxy.LocalBalanceOutpatient(r, ref feeItemList, null);
                            if (lngRes <= 0)
                            {
                                strMsg = MedcareProxy.ErrMsg;
                                //FS.FrameWork.Management.PublicTrans.RollBack();
                                return lngRes;
                            }

                            r.Pact.ID = "6";
                            break;

                        default:
                            break;
                    }
                }
            }


            // 生成发票信息
            Balance invoiceInfo = null;
            List<BalanceList> lstInvoiceDetial = null;

            List<FeeItemList> lstFeeItem = new List<FeeItemList>();
            lstFeeItem.AddRange((FeeItemList[])feeItemList.ToArray(typeof(FeeItemList)));

            lngRes = InvoiceManager.BuildInvoiceInfo(employee, r, lstFeeItem, out invoiceInfo, out lstInvoiceDetial, out strMsg);
            if (lngRes <= 0 || invoiceInfo == null || lstInvoiceDetial == null || lstInvoiceDetial.Count <= 0)
            {                
                //FS.FrameWork.Management.PublicTrans.RollBack();
                return -1;
            }

            // 生成支付方式信息
            List<BalancePay> lstPayModes = InvoiceManager.MakeInvoicePayModes(invoiceInfo, ref strMsg);
            if (lstPayModes == null || lstPayModes.Count <= 0)
            {
                //FS.FrameWork.Management.PublicTrans.RollBack();
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
            blnRes = FeeManager.ClinicFee(FS.HISFC.Models.Base.ChargeTypes.Fee, "C", true, r, arlInvoices, arlInvoiceDetial, arlFeeItem, new ArrayList(), arlPayModes, ref strMsg, employee);

            this.Err = strMsg;
            if (!blnRes)
            {
                return -1;
            }
            else
            {
                return 1;
            }
        }

        /// <summary>
        /// 移动端充值服务
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="heathNo"></param>
        /// <param name="amount">充值金额（分）</param>
        /// <param name="payModeCode">支付方式编码</param>
        /// <param name="payModeName">支付方式</param>
        /// <param name="oper"></param>
        /// <param name="orderID">外部支付订单号</param>
        /// <returns></returns>
        public int RechargeAccountForMobile(string cardNo, string heathNo, decimal amount, string payModeCode, string payModeName, FS.HISFC.Models.Base.Employee oper, string orderID, ref FS.HISFC.Models.Account.PrePay prePay)
        {
            //移动端目前只支持充值给基本账户
            //基本账户：编码：1 名称：普通账户
            string accountTypeCode = "1";
            string accountTypeName = "普通账户";
            DateTime dtNow = AccountMgr.GetDateTimeFromSysDateTime(); 

            //1、查询患者信息
            FS.HISFC.Models.Account.AccountCard accountCard = new FS.HISFC.Models.Account.AccountCard();
            if (string.IsNullOrEmpty(heathNo))
            {
                heathNo = cardNo;
            }
            int resultValue = AccountMgr.GetCardByRule(heathNo, ref accountCard);
            if (resultValue <= 0)
            {
                this.Err = "查询患者信息失败!" + this.AccountMgr.Err;
                return -1;
            }

            FS.HISFC.Models.RADT.PatientInfo patientInfo = null;
            if (!string.IsNullOrEmpty(accountCard.Patient.PID.CardNO))
            {
                cardNo = accountCard.Patient.PID.CardNO;
                patientInfo = RadtMgr.QueryComPatientInfo(cardNo);
                if (patientInfo == null)
                {
                    this.Err = "查询患者信息出错！" + RadtMgr.Err;
                    return -1;
                }
            }

            //2、判断是否已存在账户
            bool isNewAccount = false;//是否新账户
            FS.HISFC.Models.Account.Account account = AccountMgr.GetAccountByCardNoEX(cardNo);
            FS.HISFC.Models.Account.AccountDetail accountDetail = null;
            if (account == null)
            {
                if (!string.IsNullOrEmpty(AccountMgr.Err))
                {
                    this.Err = "查询患者基本账户信息失败!" + this.AccountMgr.Err;
                    return -1;
                }
                else
                {
                    isNewAccount = true;
                }
            }
            else if (string.IsNullOrEmpty(account.ID))
            {
                isNewAccount = true;
            }
            else
            {
                //查询基本账户信息1
                List<FS.HISFC.Models.Account.AccountDetail> accountDetailList = this.AccountMgr.GetAccountDetail(account.ID, accountTypeCode, "1");
                if (accountDetailList == null)
                {
                    this.Err = "查询患者基本账户信息失败!" + this.AccountMgr.Err;
                    return -1;
                }
                else if (accountDetailList.Count == 0)
                {
                    //没有账户信息则创建账户
                    isNewAccount = true;
                }
                accountDetail = accountDetailList[0] as FS.HISFC.Models.Account.AccountDetail;
            }
            #region 新账户则创建
            //FS.HISFC.Models.Account.AccountRecord accountRecord = null;
            if (isNewAccount)
            {
                #region 创建账户
                //账户信息
                account = new FS.HISFC.Models.Account.Account();
                account.ID = AccountMgr.GetAccountNO();
                account.AccountCard = accountCard;
                //设置默认密码
                account.PassWord = "000000";

                //是否可用
                account.ValidState = FS.HISFC.Models.Base.EnumValidState.Valid;
                account.AccountLevel.ID = "1";//会员等级:1 普通会员卡；2 黄金会员卡；3白金会员卡；4钻石会员卡；5至尊会员卡
                account.CreateEnvironment.ID = oper.ID;
                account.CreateEnvironment.OperTime = dtNow;
                account.OperEnvironment.ID = oper.ID;
                account.OperEnvironment.OperTime = dtNow;
                #endregion

                #region 账户明细
                accountDetail = new FS.HISFC.Models.Account.AccountDetail();
                accountDetail.ID = account.ID;
                accountDetail.AccountType.ID = accountTypeCode;
                accountDetail.CardNO = accountCard.Patient.PID.CardNO;
                accountDetail.ValidState = FS.HISFC.Models.Base.EnumValidState.Valid;
                accountDetail.CreateEnvironment.ID = oper.ID;
                accountDetail.CreateEnvironment.OperTime = dtNow;
                accountDetail.OperEnvironment.ID = oper.ID;
                accountDetail.OperEnvironment.OperTime = dtNow;
                #endregion

                #region 交易信息
                //交易信息
                //accountRecord = new FS.HISFC.Models.Account.AccountRecord();
                //accountRecord.AccountNO = account.ID;//帐号
                //accountRecord.Patient = accountCard.Patient;//门诊卡号
                //accountRecord.FeeDept.ID = string.Empty;//科室编码
                //accountRecord.Oper.ID = oper.ID;//操作员
                //accountRecord.OperTime = dtNow;//操作时间
                //accountRecord.IsValid = true;//是否有效
                //accountRecord.OperType.ID = "1";
                //accountRecord.AccountType.ID = accountTypeCode;
                //accountRecord.PayInvoiceNo = orderID;//外部消费发票号
                #endregion
            }
            #endregion

            //3、获取优惠金额
            //赠送金额按照规则由充值金额获取，目前暂不处理
            decimal donate = 0;

            //4、获取发票
            string strInvioceNO = string.Empty;
            string strRealInvoiceNO = string.Empty;
            string errInfo = string.Empty;
            long returnValue = this.FeeManager.GetInvoiceNO(oper, "A", true, ref strInvioceNO, ref strRealInvoiceNO, ref errInfo);
            if (returnValue == -1)
            {
                this.Err = "获得发票号出错!" + this.FeeManager.Err;
                return -1;
            }

            //5、充值的预交金
            prePay = new FS.HISFC.Models.Account.PrePay();
            prePay.Patient = patientInfo;//患者基本信息
            prePay.InvoiceNO = strInvioceNO;
            prePay.PayType.ID = payModeCode; //支付方式
            prePay.PayType.Name = payModeName;

            //prePay.Bank = this.cmbPayType.bank.Clone();//开户银行
            //if (prePay.PayType.ID != "CA" && !string.IsNullOrEmpty(BankPayType))
            //{
            //    prePay.PayType.ID = BankPayType;//支付方式
            //    prePay.PayType.Name = "银行卡";
            //}

            prePay.PrePayOper.ID = oper.ID;//操作员编号
            prePay.PrePayOper.Name = oper.Name;//操作员姓名
            prePay.ValidState = FS.HISFC.Models.Base.EnumValidState.Valid;
            prePay.BaseCost = amount;//充值金额
            prePay.PrePayOper.OperTime = dtNow;//系统时间
            prePay.AccountNO = account.ID; //帐号
            prePay.IsHostory = false; //是否历史数据
            if (accountDetail == null)
            {
                prePay.BaseVacancy = amount;
            }
            else
            {
                prePay.BaseVacancy = accountDetail.BaseVacancy + amount;
            }
            prePay.Bank.InvoiceNO = orderID;//pos交易流水号或支票号或汇票号 -- 存储外部订单号
            prePay.DonateCost = donate;
            if (accountDetail == null)
            {
                prePay.DonateVacancy = donate;
            }
            else
            {
                prePay.DonateVacancy = accountDetail.DonateVacancy + donate;
            }
            prePay.AccountType.ID = accountTypeCode;//账户类型编码
            prePay.AccountType.Name = accountTypeName;//账户类型

            //6.提交业务
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            AccountMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            #region 创建账户
            if (isNewAccount)//已经存在账户不需重新建立,以免新建账户类型重复插入
            {
                if (AccountMgr.InsertAccount(account) < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.Err = "建立账户失败！" + AccountMgr.Err;
                    return -1;
                }

                if (AccountMgr.InsertAccountDetail(accountDetail) < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.Err = "建立账户失败！确定是否存在相同账户!" + AccountMgr.Err;
                    return -1;
                }
                //生成账户流水信息
                //if (AccountMgr.InsertAccountRecordEX(accountRecord) < 0)
                //{
                //    FS.FrameWork.Management.PublicTrans.RollBack();
                //    this.Err = "建立账户失败！" + AccountMgr.Err;
                //    return -1;
                //}
            }

            #endregion

            #region 充值
            if (!AccountMgr.AccountPrePayManagerEX(prePay, 1))
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                this.Err = "预交金充值出现错误！" + AccountMgr.Err;
                return -1;
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            #endregion
            return 1;
        }
    }
}
