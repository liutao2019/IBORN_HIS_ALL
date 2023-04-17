using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.HISFC.Models.Account;

namespace FS.HISFC.BizProcess.Integrate.Registration
{
    //{0F383ECD-2CB9-40a7-8712-341C44BA56F0}
    public class SelfRegister : FS.HISFC.BizProcess.Interface.Registration.ISelfRegister
    {
        #region 业务管理类

        /// <summary>
        /// 参数控制类
        /// </summary>
        private FS.FrameWork.Management.ControlParam ctlMgr = null;

        public FS.FrameWork.Management.ControlParam CtlMgr
        {
            get
            {
                if (ctlMgr == null)
                {
                    ctlMgr = new FS.FrameWork.Management.ControlParam();
                }
                return ctlMgr;
            }
        }
        
        /// <summary>
        /// 挂号管理类
        /// </summary>
        private FS.HISFC.BizLogic.Registration.Register regMgr = new FS.HISFC.BizLogic.Registration.Register();

        private FS.HISFC.BizLogic.Registration.Booking bookingMgr = null;

        public FS.HISFC.BizLogic.Registration.Booking BookingMgr
        {
            get
            {
                if (bookingMgr == null)
                {
                    bookingMgr = new FS.HISFC.BizLogic.Registration.Booking();
                }
                return bookingMgr;
            }
        }

        /// <summary>
        /// 常数管理类
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager conMgr = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// 患者信息业务类
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.RADT radtProcess = new FS.HISFC.BizProcess.Integrate.RADT();

        /// <summary>
        /// 挂号级别管理类
        /// </summary>
        private FS.HISFC.BizLogic.Registration.RegLevel RegLevelMgr = new FS.HISFC.BizLogic.Registration.RegLevel();

        /// <summary>
        /// 午别管理类
        /// </summary>
        private FS.HISFC.BizLogic.Registration.Noon noonMgr = new FS.HISFC.BizLogic.Registration.Noon();

        /// <summary>
        /// 科室管理类
        /// </summary>
        private FS.HISFC.BizLogic.Manager.Department deptMgr = new FS.HISFC.BizLogic.Manager.Department();

        /// <summary>
        /// 人员管理类
        /// </summary>
        private FS.HISFC.BizLogic.Manager.Person employeeMgr = new FS.HISFC.BizLogic.Manager.Person();
        
        /// <summary>
        /// 排班管理类
        /// </summary>
        private FS.HISFC.BizLogic.Registration.Schema SchemaMgr = new FS.HISFC.BizLogic.Registration.Schema();
        
        /// <summary>
        /// 挂号费管理类
        /// </summary>
        private FS.HISFC.BizLogic.Registration.RegLvlFee regFeeMgr = null;

        public FS.HISFC.BizLogic.Registration.RegLvlFee RegFeeMgr
        {
            get
            {
                if (regFeeMgr == null)
                {
                    regFeeMgr = new FS.HISFC.BizLogic.Registration.RegLvlFee();
                }
                return regFeeMgr;
            }
        }
        
        /// <summary>
        /// 合同单位管理类
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Fee feeMgr = null;

        public FS.HISFC.BizProcess.Integrate.Fee FeeMgr
        {
            get
            {
                if (feeMgr == null)
                {
                    feeMgr = new Fee();
                }
                return feeMgr;
            }
        }
        
        /// <summary>
        /// 账户管理类
        /// </summary>
        private FS.HISFC.BizLogic.Fee.Account accountMgr = new FS.HISFC.BizLogic.Fee.Account();
        
        /// <summary>
        /// 账户管理
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Account.AccountPay accountPay = new FS.HISFC.BizProcess.Integrate.Account.AccountPay();
        
        #endregion

        #region 控制参数

        /// <summary>
        /// 为0是否打印
        /// </summary>
        private bool IsPrintIfZero = true;

        /// <summary>
        /// 获取处方号的方式
        /// </summary>
        private int GetRecipeType = 2;

        private bool isFilterDoc = true;
        /// <summary>
        /// 是否根据挂号级别过滤医生
        /// </summary>
        public bool IsFilterDoc
        {
            set { this.isFilterDoc = value; }
            get { return this.isFilterDoc; }
        }


        /// <summary>
        /// 电话预约是否在现场号前面
        /// </summary>
        private bool IsPreFirst = false;

        /// <summary>
        /// 挂号是否允许超出排班限额
        /// </summary>
        private bool IsAllowOverrun = true;

        #endregion 

        #region 属性

        /// <summary>
        /// 挂号实体
        /// </summary>
        //private FS.HISFC.Models.Registration.Register regObj = null;

        /// <summary>
        /// 费用详情
        /// </summary>
        private System.Collections.ArrayList RegFeeList = new System.Collections.ArrayList();

        /// <summary>
        /// 存储优惠和支付类型的哈希表
        /// </summary>
        private Hashtable hsPayCost = new Hashtable();

        /// <summary>
        /// 支付方式
        /// </summary>
        private ArrayList paymodeList = new ArrayList();


        #endregion 

        #region 接口函数

        /// <summary>
        /// 保存开始时处理
        /// </summary>
        /// <param name="regObj"></param>
        /// <param name="errText"></param>
        /// <returns></returns>
        public int SaveBegin(ref string errText)
        {
            return 1;
        }

        /// <summary>
        /// 保存结束时处理
        /// </summary>
        /// <param name="regObj"></param>
        /// <param name="errText"></param>
        /// <returns></returns>
        public int SaveEnd(ref string errText)
        {
            return 1;
        }

        /// <summary>
        /// 挂号函数
        /// </summary>
        /// <param name="regObj"></param>
        /// <param name="ErrMsg"></param>
        /// <returns></returns>
        public int SaveRegister(string CardNO, string Name, bool isPre, string bookID, string DeptCode, string DoctCode, DateTime begin, DateTime end, string reglevel, string payCode, decimal amount, string operCode, ref FS.HISFC.Models.Registration.Register regObj, ref string errText)
        {
            try
            {
                ///是否预约号看诊序号排在现场号前面别
                string rtn = this.CtlMgr.QueryControlerInfo("400026");
                if (rtn == null || rtn == "-1" || rtn == "") rtn = "0";
                this.IsPreFirst = FS.FrameWork.Function.NConvert.ToBoolean(rtn);

                //挂号是否允许超出排班限额
                rtn = this.CtlMgr.QueryControlerInfo("400015");
                if (rtn == null || rtn == "-1" || rtn == "") rtn = "1";
                this.IsAllowOverrun = FS.FrameWork.Function.NConvert.ToBoolean(rtn);  
                
                FS.FrameWork.Management.PublicTrans.BeginTransaction();

                string errMessage = string.Empty;

                regObj = this.GetValue(CardNO, Name, DeptCode, DoctCode, begin, end, reglevel, payCode, amount,operCode, ref errMessage);

                if (regObj == null)
                {
                    this.Clear();
                    throw new Exception(errMessage);
                }

                //事务开始
                DateTime current = this.regMgr.GetDateTimeFromSysDateTime();

                #region 取发票号

                string strInvioceNO = "";
                string strRealInvoiceNO = "";
                string strErrText = "";
                int iRes = 0;
                string strInvoiceType = "R";

                FS.HISFC.Models.Base.Employee employee = this.employeeMgr.GetPersonByID(operCode);
                if (employee == null || string.IsNullOrEmpty(employee.ID) || string.IsNullOrEmpty(employee.Name))
                {
                    throw new Exception("未找到操作员信息！");
                }

                //有费用信息的时候才打发票
                if (this.RegFeeList.Count > 0)
                {
                    if (this.GetRecipeType == 1)
                    {
                        strInvioceNO = regObj.RecipeNO.ToString().PadLeft(12, '0');
                        strRealInvoiceNO = "";
                    }
                    else
                    {
                        if (this.GetRecipeType == 2)
                        {
                            strInvoiceType = "R";
                        }
                        else if (this.GetRecipeType == 3)
                        {
                            // 取门诊收据
                            strInvoiceType = "C";
                        }
                        iRes = this.FeeMgr.GetInvoiceNO(employee, strInvoiceType,true, ref strInvioceNO, ref strRealInvoiceNO, ref strErrText);
                        if (iRes == -1)
                        {
                            throw new Exception(strErrText);
                        }
                    }
                }

                regObj.InvoiceNO = strInvioceNO;

                #endregion

                #region 处理费用明细信息

                //有费用信息的时候才处理
                if (RegFeeList.Count > 0)
                {
                    FS.HISFC.Models.Account.AccountCard accountCard = new FS.HISFC.Models.Account.AccountCard();
                    int rev = this.FeeMgr.ValidMarkNO(regObj.PID.CardNO, ref accountCard);

                    decimal totCost = (decimal)this.hsPayCost["TOT"];
                    decimal actuCost = (decimal)this.hsPayCost["ACTU"];
                    decimal giftCost = (decimal)this.hsPayCost["GIFT"];
                    decimal etcCost = (decimal)this.hsPayCost["ETC"];

                    decimal countactu = actuCost;
                    decimal countgift = giftCost;
                    decimal countetc = etcCost;

                    for (int i = 0; i < this.RegFeeList.Count; i++)
                    {
                        HISFC.Models.Registration.RegisterFeeDetail regFeeDetail = this.RegFeeList[i] as HISFC.Models.Registration.RegisterFeeDetail;
                        regFeeDetail.InvoiceNo = strInvioceNO;
                        regFeeDetail.Print_InvoiceNo = strRealInvoiceNO;
                        regFeeDetail.ClinicNO = regObj.ID;
                        regFeeDetail.MarkType = accountCard.MarkType;
                        regFeeDetail.MarkNO = accountCard.MarkNO;
                        regFeeDetail.SequenceNO = (i + 1).ToString();

                        regFeeDetail.Patient.PID.CardNO = regObj.PID.CardNO;
                        regFeeDetail.Patient.Name = regObj.Name;
                        regFeeDetail.CancelFlag = 1;

                        regFeeDetail.Oper.ID = employee.ID;
                        regFeeDetail.Oper.Name = employee.Name;
                        regFeeDetail.Oper.OperTime = current;

                        regFeeDetail.IsBalance = false;
                        regFeeDetail.BalanceNo = "";
                        regFeeDetail.Qty = 1;

                        decimal real_cost = 0.0m;
                        decimal gift_cost = 0.0m;
                        decimal etc_cost = 0.0m;

                        //最后一个项目的价格由总价格减去前面所有项目的价格
                        if (i == this.RegFeeList.Count - 1)
                        {
                            real_cost = countactu;
                            gift_cost = countgift;
                            etc_cost = countetc;
                        }
                        else
                        {
                            //总是直接舍去小数点后两位以后的数值，以防止最后一个项目的价格出现负数
                            real_cost = Math.Floor((regFeeDetail.Tot_cost * actuCost * 100) / totCost) / 100;
                            gift_cost = Math.Floor((regFeeDetail.Tot_cost * giftCost * 100) / totCost) / 100;
                            etc_cost = Math.Floor((regFeeDetail.Tot_cost * etcCost * 100) / totCost) / 100;
                        }

                        countactu -= real_cost;
                        countgift -= gift_cost;
                        countetc -= etc_cost;

                        if (real_cost < 0 || gift_cost < 0)
                        {
                            throw new Exception("分配优惠金额，赠送金额至挂号明细时出现错误！");
                        }

                        //当总价不等于优惠金额+实收金额+赠送金额的时候
                        //此处不可能出现regFeeDetail.Tot_cost < real_cost + gift_cost + etc_cost
                        //的情况，因为上面都是直接舍去小数点后两位以后的数值
                        if (regFeeDetail.Tot_cost > real_cost + gift_cost + etc_cost)
                        {
                            decimal diff = regFeeDetail.Tot_cost - real_cost - gift_cost - etc_cost;

                            if (countactu + countgift + countetc >= diff)
                            {
                                if (countactu >= diff)
                                {
                                    real_cost += diff;
                                    countactu -= diff;
                                }
                                else
                                {
                                    real_cost += countactu;
                                    countactu = 0;
                                    diff -= countactu;

                                    if (countgift >= diff)
                                    {
                                        countgift -= diff;
                                        gift_cost += diff;
                                    }
                                    else
                                    {
                                        if (countetc >= diff)
                                        {
                                            countetc -= diff;
                                            etc_cost += diff;
                                        }
                                        else
                                        {
                                            throw new Exception("分配优惠金额，赠送金额至挂号明细时出现错误！");
                                        }
                                    }
                                }
                            }
                        }

                        regFeeDetail.Real_cost = real_cost;
                        regFeeDetail.Gift_cost = gift_cost;
                        regFeeDetail.Etc_cost = etc_cost;
                    }
                }

                #endregion

                #region
                int payinfoSequence = 1;
                foreach (FS.HISFC.Models.Registration.RegisterPayMode pay in this.paymodeList)
                {
                    pay.InvoiceNo = strInvioceNO;
                    pay.TransType = FS.HISFC.Models.Base.TransTypes.Positive;
                    pay.SequenceNO = payinfoSequence.ToString();
                    pay.Oper.ID = FS.FrameWork.Management.Connection.Operator.ID;
                    pay.Oper.OperTime = this.regMgr.GetDateTimeFromSysDateTime();

                    //账户扣费
                    if (pay.Mode_Code == "YS" || pay.Mode_Code == "DC")
                    {
                        FS.HISFC.Models.RADT.PatientInfo powerpatient = null;
                        List<AccountDetail> tmp = accountMgr.GetAccountDetail(pay.AccountID, pay.AccountType, "1");
                        if (tmp != null && tmp.Count > 0)
                        {
                            AccountDetail tmpAc = tmp[0];
                            powerpatient = accountMgr.GetPatientInfoByCardNO(tmpAc.CardNO);
                        }

                        if (powerpatient == null)
                        {
                            throw new Exception("查找账户授权人失败！");
                        }

                        FS.HISFC.Models.RADT.PatientInfo patient = accountMgr.GetPatientInfoByCardNO(regObj.PID.CardNO);

                        if (patient == null)
                        {
                            throw new Exception("查找当前患者信息失败！");
                        }

                        if (accountPay.OutpatientPay(patient,
                                                    pay.AccountID,
                                                    pay.AccountType,
                                                    pay.AccountFlag == "0" ? -pay.Tot_cost : 0,
                                                    pay.AccountFlag == "0" ? 0 : -pay.Tot_cost,
                                                    strInvioceNO, powerpatient,
                                                    FS.HISFC.Models.Account.PayWayTypes.R,
                                                    1) < 1)
                        {
                            throw new Exception("账户扣费失败！");
                        }

                    }

                    payinfoSequence++;
                }
                #endregion

                #region 更新预约信息

                //预约支付，更新预约信息
                if (isPre)
                {
                    //预约号更新已看诊标志
                    #region book

                    //确认预约信息
                    int rev = this.BookingMgr.Update(bookID, true, operCode, current);
                    if (rev == -1)
                    {
                        throw new Exception(BookingMgr.Err);
                    }
                    if (rev == 0)
                    {
                        throw new Exception("未找到预约记录！" + BookingMgr.Err);
                    }

                    rev = this.BookingMgr.UpdateBookReg(bookID, regObj.ID);
                    if (rev == -1)
                    {
                        throw new Exception(BookingMgr.Err);
                    }
                    if (rev == 0)
                    {
                        throw new Exception("未找到预约记录！" + BookingMgr.Err);
                    }
                    #endregion
                }
                #endregion

                #region 更新看诊序号
                int orderNo = 0;

                //2看诊序号	
                string Err = string.Empty;
                if (this.UpdateSeeID(regObj.DoctorInfo.Templet.Dept.ID, regObj.DoctorInfo.Templet.Doct.ID,
                    regObj.DoctorInfo.Templet.Noon.ID, regObj.DoctorInfo.SeeDate, ref orderNo,
                    ref Err) == -1)
                {
                    throw new Exception(Err);
                }

                regObj.DoctorInfo.SeeNO = orderNo;

                //专家、专科、特诊、预约号更新排班限额
                #region schema
                if (this.UpdateSchema(this.SchemaMgr, regObj, regObj.RegType, ref orderNo, ref Err) == -1)
                {
                    throw new Exception(Err);
                }

                #endregion

                //1全院流水号			
                if (this.Update(this.regMgr, regObj.DoctorInfo.SeeDate, ref orderNo, ref Err) == -1)
                {
                    throw new Exception(Err);
                }

                regObj.OrderNO = orderNo;

                #endregion

                #region 平费用

                decimal totcost = regObj.RegLvlFee.RegFee + regObj.RegLvlFee.ChkFee + regObj.RegLvlFee.OwnDigFee + regObj.RegLvlFee.OthFee;
                if (totcost - regObj.PubCost - regObj.PayCost - regObj.OwnCost != 0)
                {
                    regObj.OwnCost = totcost - regObj.PubCost - regObj.PayCost;
                }

                #endregion

                //登记挂号信息
                if (this.regMgr.Insert(regObj) == -1)
                {
                    throw new Exception(this.regMgr.Err);
                }

                #region 保存费用明细信息

                if (this.RegFeeList != null && this.RegFeeList.Count > 0)
                {
                    if (this.FeeMgr.SaveRegFeeList(this.RegFeeList) == -1)
                    {
                        throw new Exception(this.FeeMgr.Err);
                    }
                }

                if (this.paymodeList != null && this.paymodeList.Count > 0)
                {
                    //{9F2C17EC-0AE6-4df8-B959-F4D99314E227}
                    if (this.FeeMgr.SaveRegPayModeList(regObj, this.paymodeList) == -1)
                    {
                        throw new Exception(this.FeeMgr.Err);
                    }
                }

                #endregion

                //挂号自动分诊接口
                //if (InterfaceManager.GetIADT() != null)
                //{
                //    //挂号没有选择医生时，这里可能会根据排班队列和候诊人数自动分配一个医生，所以提前到前面
                //    if (InterfaceManager.GetIADT().Register(regObj, true) < 0)
                //    {
                //        FS.FrameWork.Management.PublicTrans.RollBack();
                //        MessageBox.Show(this, "挂号失败，请向系统管理员报告错误信息：" + InterfaceManager.GetIADT().Err, "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                //        return -1;
                //    }
                //}
            }
            catch (Exception ex)
            {
                errText = "挂号过程出现错误：" + ex.Message;
                FS.FrameWork.Management.PublicTrans.RollBack();
                return -1;
            }

            FS.FrameWork.Management.PublicTrans.Commit();
            this.Clear();
            return 1;
        }

        #endregion 

        private void Clear()
        {
            this.RegFeeList.Clear();
            this.hsPayCost.Clear();
            this.paymodeList.Clear();
        }

        /// <summary>
        /// 获取挂号实体
        /// </summary>
        /// <param name="CardNO"></param>
        /// <param name="Name"></param>
        /// <param name="DeptCode"></param>
        /// <param name="DoctCode"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <param name="reglevel"></param>
        /// <param name="errText"></param>
        /// <returns></returns>
        private FS.HISFC.Models.Registration.Register GetValue(string CardNO, string Name, string DeptCode, string DoctCode, DateTime begin, DateTime end, string reglevel,string payCode,decimal amount,string operCode, ref string errText)
        {
            FS.HISFC.Models.Registration.Register regObj = new FS.HISFC.Models.Registration.Register();

            try
            {
                if (string.IsNullOrEmpty(CardNO))
                {
                    throw new Exception("卡号为空！");
                }

                if (string.IsNullOrEmpty(Name))
                {
                    throw new Exception("姓名为空！");
                }

                if (string.IsNullOrEmpty(DeptCode))
                {
                    throw new Exception("科室编码为空！");
                }

                if (string.IsNullOrEmpty(DoctCode))
                {
                    throw new Exception("医生编码为空！");
                }

                if (begin > end)
                {
                    throw new Exception("开始时间不能大于结束时间");
                }

                if (begin < this.regMgr.GetDateTimeFromSysDateTime() && end < this.regMgr.GetDateTimeFromSysDateTime())
                {
                    throw new Exception("开始时间和结束时间不能同时小于当前时间");
                }

                if (string.IsNullOrEmpty(reglevel))
                {
                    throw new Exception("挂号级别为空");
                }

                if (string.IsNullOrEmpty(payCode))
                {
                    throw new Exception("支付方式不能为空");
                }

                if (amount < 0)
                {
                    throw new Exception("支付金额不能小于零");
                }

                if (string.IsNullOrEmpty(operCode))
                {
                    throw new Exception("操作人员编码不能为空");
                }

                //先检索患者基本信息表,看是否存在该患者信息
                FS.HISFC.Models.RADT.PatientInfo p = this.radtProcess.QueryComPatientInfo(CardNO);

                FS.HISFC.Models.Base.Employee employee = this.employeeMgr.GetPersonByID(operCode);
                if (employee == null || string.IsNullOrEmpty(employee.ID) || string.IsNullOrEmpty(employee.Name))
                {
                    throw new Exception("未找到操作员信息！");
                }

                if (p != null && !string.IsNullOrEmpty(p.Name))
                {
                    #region 存在患者基本信息,取基本信息

                    if (Name != p.Name)
                    {
                        throw new Exception("患者姓名与系统中信息不符合！");
                    }
                    regObj.PID.CardNO = CardNO;
                    regObj.Name = p.Name;
                    regObj.Sex.ID = p.Sex.ID;
                    regObj.Birthday = p.Birthday;
                    regObj.Pact.ID = p.Pact.ID;
                    regObj.Pact.PayKind.ID = p.Pact.PayKind.ID;
                    FS.HISFC.Models.Base.PactInfo pInfo = SOC.HISFC.BizProcess.Cache.Fee.GetPactUnitInfo(p.Pact.ID);
                    if (pInfo != null)
                    {
                        regObj.Pact = pInfo;
                    }

                    regObj.SSN = p.SSN;
                    regObj.PhoneHome = p.PhoneHome;
                    regObj.AddressHome = p.AddressHome;
                    regObj.IDCard = p.IDCard;
                    regObj.NormalName = p.NormalName;
                    regObj.IsEncrypt = p.IsEncrypt;
                    regObj.IDCard = p.IDCard;

                    //给预约调用的，先不用判断证件号
                    //if (this.validCardType(p.IDCardType.ID))
                    //{
                    //    if (this.validCardType(p.IDCardType.ID))
                    //    {
                    //        regObj.CardType.ID = p.IDCardType.ID;
                    //    }
                    //}
                    //else
                    //{
                    //    throw new Exception("患者基本信息证件类型非法！");
                    //}

                    #endregion

                    #region 挂号信息设置

                    regObj.DoctorInfo.Templet.RegLevel = this.RegLevelMgr.Query(reglevel);
                    if (regObj.DoctorInfo.Templet.RegLevel == null || string.IsNullOrEmpty(regObj.DoctorInfo.Templet.RegLevel.ID))
                    {
                        throw new Exception("挂号级别编码非法");
                    }

                    regObj.DoctorInfo.Templet.Dept = this.deptMgr.GetDeptmentById(DeptCode);
                    if (regObj.DoctorInfo.Templet.Dept == null || string.IsNullOrEmpty(regObj.DoctorInfo.Templet.Dept.ID))
                    {
                        throw new Exception("挂号科室编码非法");
                    }

                    regObj.DoctorInfo.Templet.Doct = this.employeeMgr.GetPersonByID(DoctCode);
                    if (regObj.DoctorInfo.Templet.Doct == null || string.IsNullOrEmpty(regObj.DoctorInfo.Templet.Doct.ID))
                    {
                        throw new Exception("挂号医生编码非法");
                    }

                    System.Collections.ArrayList al = this.SchemaMgr.QueryByDoct(begin.Date, regObj.DoctorInfo.Templet.Doct.ID);
                    foreach (FS.HISFC.Models.Registration.Schema schema in al)
                    {
                        if (this.IsValid(schema, SchemaMgr.GetDateTimeFromSysDateTime()))
                        {
                            if (schema.Templet.Dept.ID == regObj.DoctorInfo.Templet.Dept.ID
                                && schema.Templet.Begin <= begin
                                && schema.Templet.End >= end)
                            {
                                regObj.DoctorInfo = schema;
                                break;
                            }
                        }
                    }

                    //排班里面的挂号级别没有 isExpert属性
                    regObj.DoctorInfo.Templet.RegLevel = this.RegLevelMgr.Query(regObj.DoctorInfo.Templet.RegLevel.ID);

                    if (string.IsNullOrEmpty(regObj.DoctorInfo.Templet.ID))
                    {
                        throw new Exception("不存在有效的排班！");
                    }

                    string RecipeNO = string.Empty;
                    string errmsg = string.Empty;
                    if (GetRecipeNo(this.GetRecipeType, operCode, ref RecipeNO, ref errmsg) < 0)
                    {
                        throw new Exception("获取处方号出错！");
                    }

                    int regCount = regMgr.QueryRegisterByCardNOTimeDept(regObj.PID.CardNO,
                                          regObj.DoctorInfo.Templet.Dept.ID,
                                          regMgr.GetDateTimeFromSysDateTime().AddYears(-1).Date);
                    regObj.IsFirst = regCount > 0 ? false : true;

                    regObj.ID = this.regMgr.GetSequence("Registration.Register.ClinicID");
                    regObj.RecipeNO = RecipeNO;
                    regObj.Status = FS.HISFC.Models.Base.EnumRegisterStatus.Valid;
                    regObj.IsSee = false;
                    regObj.InputOper.ID = employee.ID;
                    regObj.InputOper.OperTime = this.regMgr.GetDateTimeFromSysDateTime();
                    regObj.DoctorInfo.Templet.Noon.Name = this.noonMgr.Query(regObj.DoctorInfo.Templet.Noon.ID);
                    regObj.CancelOper.ID = "";
                    regObj.CancelOper.OperTime = DateTime.MinValue;
                    regObj.IsFee = true;

                    #endregion

                    #region 费用信息

                    FS.HISFC.Models.Registration.RegisterPayMode payMode = new FS.HISFC.Models.Registration.RegisterPayMode();
                    payMode.Mode_Code = payCode;
                    payMode.CancelFlag = 1;
                    payMode.TransType = FS.HISFC.Models.Base.TransTypes.Positive;
                    payMode.Tot_cost = amount;
                    payMode.Oper.ID = operCode;

                    this.paymodeList.Add(payMode);

                    this.getCost(ref regObj);

                    if (!CompareCost())
                    {
                        throw new Exception("支付金额与消费金额不等！");
                    }

                    #endregion
                }
                else
                {
                    throw new Exception("患者信息不存在！");
                }
            }
            catch(Exception ex)
            {
                errText = ex.Message;
                return null;
            }

            return regObj;
        }

        /// <summary>
        /// 判断支付金额与挂号金额是否相等
        /// </summary>
        /// <returns></returns>
        private bool CompareCost()
        {
            decimal regCost = (decimal)hsPayCost["TOT"];
            decimal payCost = 0.0m;

            if (paymodeList == null)
            {
                return false;
            }
            else
            {
                foreach (FS.HISFC.Models.Registration.RegisterPayMode paymode in this.paymodeList)
                {
                    payCost += paymode.Tot_cost;
                }
            }

            if (regCost != payCost)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 验证证件类别是否有效
        /// </summary>
        /// <param name="cardType">卡号</param>
        /// <returns></returns>
        private bool validCardType(string cardType)
        {
            bool found = false;
            System.Collections.ArrayList cardTypeList = this.conMgr.QueryConstantList("IDCard");
            if (cardType == null)
            {
                throw new Exception("获取证件类型时出错!" + this.conMgr.Err);
                return false;
            }
            foreach (FS.FrameWork.Models.NeuObject obj in cardTypeList)
            {
                if (obj.ID == cardType)
                {
                    found = true;
                    break;
                }
            }
            return found;
        }

        /// <summary>
        /// 取得最有效的一条出诊记录
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="current"></param>
        /// <param name="regType"></param>
        /// <returns></returns>
        private bool IsValid(FS.HISFC.Models.Registration.Schema obj, DateTime current)
        {
            if (!this.IsMaybeValid(obj, current)) return false;

            //判断是否超限额
            if (!obj.Templet.IsAppend)
            {
                if (obj.Templet.RegQuota <= obj.RegedQTY && obj.Templet.TelQuota <= obj.TelingQTY)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 判断一条出诊信息是否有效(超出限额的为判断,所以用了Maybe, HaHa ～～ :))
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="current"></param>
        /// <param name="regType"></param>
        /// <returns></returns>
        private bool IsMaybeValid(FS.HISFC.Models.Registration.Schema obj, DateTime current)
        {
            //无效
            if (obj.Templet.IsValid == false) return false;	

            //
            //只有日期相同,才判断时间是否超时,否则就是预约到以后日期,时间不用判断,(出诊时间一定是>=当前时间)
            //
            if (current.Date == obj.SeeDate.Date)
            {
                if (obj.Templet.End.TimeOfDay < current.TimeOfDay) return false;//时间小于当前时间
            }

            return true;
        }

        /// <summary>
        /// 获取当前处方号
        /// </summary>
        /// <param name="OperID"></param>		
        private int GetRecipeNo(int GetRecipeType,string OperID,ref string recipeNO,ref string ErrMsg)
        {
            if (GetRecipeType == 1)
            {
                recipeNO = string.Empty;
                return 1;
            }
            else if (GetRecipeType == 2)
            {
                FS.FrameWork.Models.NeuObject obj = this.conMgr.GetConstansObj("RegRecipeNo", OperID);
                if (obj == null)
                {
                    ErrMsg = this.conMgr.Err;
                    return -1;
                }

                if (obj.Name == "")
                {
                    recipeNO = "0";
                }
                else
                {
                    recipeNO = obj.Name;
                }
            }
            else
            {
                FS.FrameWork.Models.NeuObject obj = this.conMgr.GetConstansObj("RegRecipeNo", OperID);
                if (obj == null)
                {
                    ErrMsg = this.conMgr.Err;
                    return -1;
                }
                if (obj.Name == "")
                {
                    recipeNO = "0";
                }
                else
                {
                    recipeNO = obj.Name;
                }
            }

            return 1;
        }

        /// <summary>
        /// 得到患者应付
        /// </summary>		
        /// <returns></returns>
        private int getCost(ref FS.HISFC.Models.Registration.Register regObj)
        {

            string regLvlID, pactID;
            decimal regfee = 0, chkfee = 0, digfee = 0, othfee = 0, owncost = 0, etccost = 0;

            regLvlID = regObj.DoctorInfo.Templet.RegLevel.ID;
            pactID = regObj.Pact.ID;

            int rtn = this.GetRegFee(pactID, regLvlID, ref regfee, ref chkfee, ref digfee, ref othfee);

            if (rtn == -1) return 0;

            //获得患者应收、报销
            if (regObj == null || regObj.PID.CardNO == "")
            {
                this.RegFeeList = this.getCost(regfee, chkfee, digfee, ref othfee, ref owncost, ref etccost, "");
            }
            else
            {
                this.RegFeeList = this.getCost(regfee, chkfee, digfee, ref othfee, ref owncost, ref etccost, regObj.PID.CardNO);
            }

            regObj.RegLvlFee.RegFee = regfee;
            regObj.RegLvlFee.ChkFee = chkfee;
            regObj.RegLvlFee.OwnDigFee = digfee;
            regObj.RegLvlFee.OthFee = othfee;

            this.setPayInfo();
            return 0;
        }

        /// <summary>
        /// 设置支付信息
        /// </summary>
        /// <returns></returns>
        private int setPayInfo()
        {

            this.hsPayCost.Clear();
            //套餐金额
            hsPayCost.Add("TOT", 0.0m);
            //实际金额
            hsPayCost.Add("REAL", 0.0m);
            //赠送金额
            hsPayCost.Add("GIFT", 0.0m);
            //实际的收入
            hsPayCost.Add("ACTU", 0.0m);
            //优惠金额
            hsPayCost.Add("ETC", 0.0m);
            //四舍五入位
            hsPayCost.Add("ROUND", 0.0m);

            //此处调用时，GIFT_COST应该是为零的，费用发生变化的时候支付方式清零，折扣清零
            foreach (HISFC.Models.Registration.RegisterFeeDetail detail in this.RegFeeList)
            {
                hsPayCost["TOT"] = (decimal)hsPayCost["TOT"] + detail.Tot_cost;
                hsPayCost["REAL"] = (decimal)hsPayCost["REAL"] + detail.Real_cost;
                hsPayCost["ETC"] = (decimal)hsPayCost["ETC"] + detail.Etc_cost;
            }
            return 1;
        }

        /// <summary>
        /// 获取挂号费
        /// </summary>
        /// <param name="pactID"></param>
        /// <param name="regLvlID"></param>
        /// <param name="regFee"></param>
        /// <param name="chkFee"></param>
        /// <param name="digFee"></param>
        /// <param name="othFee"></param>
        /// <returns></returns>
        private int GetRegFee(string pactID, string regLvlID, ref decimal regFee, ref decimal chkFee, ref decimal digFee, ref decimal othFee)
        {
            FS.HISFC.Models.Registration.RegLvlFee p = this.RegFeeMgr.Get(pactID, regLvlID);
            if (p == null)//找不到就默认自费
            {
                p = this.RegFeeMgr.Get("1", regLvlID);
            }
            if (p.ID == null || p.ID == "")
            {
                return 1;
            }

            regFee = p.RegFee;
            chkFee = p.ChkFee;
            digFee = p.OwnDigFee;
            othFee = p.OthFee;

            return 0;
        }

        /// <summary>
        /// 获得患者应交金额、报销金额
        /// </summary>
        /// <param name="regFee"></param>
        /// <param name="chkFee"></param>
        /// <param name="digFee"></param>
        /// <param name="othFee"></param>
        /// <param name="ownCost"></param>
        /// <param name="etcCost"></param>
        /// <param name="cardNo"></param>		
        private ArrayList getCost(decimal regFee, decimal chkFee, decimal digFee, ref decimal othFee, ref decimal ownCost, ref decimal etcCost, string cardNo)
        {
            ArrayList lstRegFee = new ArrayList();
            HISFC.Models.Registration.RegisterFeeDetail regFeeDetail = null;

            if (regFee > 0)
            {
                regFeeDetail = BuildRegFeeInfo(AccCardFeeType.RegFee, regFee, 0);
                lstRegFee.Add(regFeeDetail);
            }
            else if (regFee == 0 && this.IsPrintIfZero)
            {
                regFeeDetail = BuildRegFeeInfo(AccCardFeeType.RegFee, regFee, 0);
                lstRegFee.Add(regFeeDetail);
            }

            if (chkFee > 0)
            {
                regFeeDetail = BuildRegFeeInfo(AccCardFeeType.ChkFee, chkFee, 0);
                lstRegFee.Add(regFeeDetail);
            }

            if (digFee > 0)
            {
                regFeeDetail = BuildRegFeeInfo(AccCardFeeType.DiaFee, digFee, 0);
                lstRegFee.Add(regFeeDetail);
            }

            // 其他费用
            if (othFee > 0)
            {
                regFeeDetail = BuildRegFeeInfo(AccCardFeeType.OthFee, othFee, 0);
                lstRegFee.Add(regFeeDetail);
            }

            ownCost = regFee + chkFee + digFee + othFee;
            etcCost = 0;
            return lstRegFee;
        }

        /// <summary>
        /// 创建收费条目
        /// </summary>
        /// <returns></returns>
        private HISFC.Models.Registration.RegisterFeeDetail BuildRegFeeInfo(AccCardFeeType feeType, decimal ownCost, decimal etcCost)
        {
            HISFC.Models.Registration.RegisterFeeDetail regFeeDetail = new HISFC.Models.Registration.RegisterFeeDetail();
            regFeeDetail.Memo = feeType.ToString();
            regFeeDetail.TransType = FS.HISFC.Models.Base.TransTypes.Positive;
            regFeeDetail.Real_cost = ownCost;
            regFeeDetail.Gift_cost = 0.0m;
            regFeeDetail.Etc_cost = etcCost;
            regFeeDetail.Tot_cost = ownCost + etcCost;

            if (regFeeDetail.FeeItem == null)
            {
                regFeeDetail.FeeItem = new FS.HISFC.Models.Base.Item();
            }

            switch (feeType)
            {
                case AccCardFeeType.RegFee:
                    regFeeDetail.FeeItem.Name = "挂号费";
                    break;
                case AccCardFeeType.DiaFee:
                    regFeeDetail.FeeItem.Name = "诊金";
                    break;
                case AccCardFeeType.CardFee:
                    regFeeDetail.FeeItem.Name = "卡费";
                    break;
                case AccCardFeeType.CaseFee:
                    regFeeDetail.FeeItem.Name = "病历本费";
                    break;
                case AccCardFeeType.ChkFee:
                    regFeeDetail.FeeItem.Name = "检查费";
                    break;
                case AccCardFeeType.AirConFee:
                    regFeeDetail.FeeItem.Name = "空调费";
                    break;
                case AccCardFeeType.OthFee:
                    regFeeDetail.FeeItem.Name = "其他费";
                    break;
                default:
                    break;
            }

            return regFeeDetail;
        }

        /// <summary>
        /// 更新医生或科室的看诊序号
        /// </summary>
        /// <param name="deptID"></param>
        /// <param name="doctID"></param>
        /// <param name="noonID"></param>
        /// <param name="regDate"></param>
        /// <param name="seeNo"></param>
        /// <param name="Err"></param>
        /// <returns></returns>
        private int UpdateSeeID(string deptID, string doctID, string noonID, DateTime regDate,
            ref int seeNo, ref string Err)
        {
            string Type = "", Subject = "";

            if (doctID != null && doctID != "")
            {
                Type = "1";//医生
                Subject = doctID;
            }
            else
            {
                Type = "2";//科室
                Subject = deptID;
            }

            //更新看诊序号
            if (this.regMgr.UpdateSeeNo(Type, regDate, Subject, noonID) == -1)
            {
                Err = this.regMgr.Err;
                return -1;
            }

            //获取看诊序号		
            if (this.regMgr.GetSeeNo(Type, regDate, Subject, noonID, ref seeNo) == -1)
            {
                Err = this.regMgr.Err;
                return -1;
            }

            return 0;
        }

        /// <summary>
        /// 更新看诊限额
        /// </summary>
        /// <param name="SchMgr"></param>
        /// <param name="regType"></param>
        /// <param name="seeNo"></param>
        /// <param name="Err"></param>
        /// <returns></returns>
        private int UpdateSchema(FS.HISFC.BizLogic.Registration.Schema SchMgr, FS.HISFC.Models.Registration.Register regObj, FS.HISFC.Models.Base.EnumRegType regType, ref int seeNo, ref string Err)
        {
            int rtn = 1;
            //挂号级别
            FS.HISFC.Models.Registration.RegLevel level = regObj.DoctorInfo.Templet.RegLevel;

            if (level.IsFaculty || level.IsExpert)//专家、专科,扣挂号限额
            {
                if (regObj.DoctorInfo.Templet.RegQuota - regObj.DoctorInfo.RegedQTY > 0)
                {
                    rtn = SchMgr.Increase(regObj.DoctorInfo.Templet.ID, true, false, false, false);
                }
                else//减预约限额
                {
                    rtn = SchMgr.Increase(regObj.DoctorInfo.Templet.ID,false, true, true, false);
                }

                //判断限额是否允许挂号
                if (this.IsPermitOverrun(SchMgr, regType, regObj.DoctorInfo.Templet.ID,level, ref seeNo, ref Err) == -1)
                {
                    return -1;
                }
            }
            else if (level.IsSpecial && !isFilterDoc)//特诊扣特诊限额
            {
                rtn = SchMgr.Increase(regObj.DoctorInfo.Templet.ID,false, false, false, true);

                //判断限额是否允许挂号

                if (this.IsPermitOverrun(SchMgr, regType, regObj.DoctorInfo.Templet.ID,level, ref seeNo, ref Err) == -1)
                {
                    return -1;
                }
            }

            if (rtn == -1)
            {
                Err = "更新排班看诊限额时出错!" + SchMgr.Err;
                return -1;
            }

            if (rtn == 0)
            {
                Err = FS.FrameWork.Management.Language.Msg("医生排班信息已经改变,请重新选择看诊时段");
                return -1;
            }

            return 0;
        }

        /// <summary>
        /// 判断超出挂号限额是否允许挂号
        /// </summary>
        /// <param name="schMgr"></param>
        /// <param name="regType"></param>
        /// <param name="schemaID"></param>
        /// <param name="level"></param>
        /// <param name="seeNo"></param>
        /// <param name="Err"></param>
        /// <returns></returns>
        private int IsPermitOverrun(FS.HISFC.BizLogic.Registration.Schema schMgr,
                    FS.HISFC.Models.Base.EnumRegType regType,
                    string schemaID, FS.HISFC.Models.Registration.RegLevel level,
                    ref int seeNo, ref string Err)
        {
            bool isOverrun = false;//是否超额

            FS.HISFC.Models.Registration.Schema schema = schMgr.GetByID(schemaID);
            if (schema == null || schema.Templet.ID == "")
            {
                Err = "查询排班信息出错!" + schMgr.Err;
                return -1;
            }


            if (regType == FS.HISFC.Models.Base.EnumRegType.Pre)//预约号,不用判断限额,因为预约时已经判断
            {
                if (this.IsPreFirst)
                {
                    seeNo = int.Parse(schema.TeledQTY.ToString());
                }
                else
                {
                    seeNo = schema.SeeNO;
                }
            }
            else if (level.IsExpert || level.IsFaculty)//专家、专科判断限额是否大于已挂号
            {
                if (schema.Templet.RegQuota - schema.RegedQTY < 0)
                {
                    if (schema.Templet.TelQuota - schema.TelingQTY < 0)
                    {
                        isOverrun = true;
                    }
                }

                if (this.IsPreFirst)
                {
                    //seeNo = int.Parse(Convert.ToString(schema.RegedQty + schema.TelReging + schema.SpeReged)) ;//获得当前时段已看诊数,作为看诊序列号
                    seeNo = int.Parse(Convert.ToString(schema.SeeNO + schema.TelingQTY - schema.TeledQTY));
                }
                else
                {
                    //seeNo = int.Parse(Convert.ToString(schema.RegedQty + schema.TelReged + schema.SpeReged)) ;//获得当前时段已看诊数,作为看诊序列号
                    seeNo = schema.SeeNO;
                }
            }
            else if (level.IsSpecial)//特诊判断特诊限额是否超表
            {
                if (schema.Templet.SpeQuota - schema.SpedQTY < 0)
                {
                    isOverrun = true;
                }

                if (this.IsPreFirst)
                {
                    //seeNo = int.Parse(Convert.ToString(schema.RegedQty + schema.TelReging + schema.SpeReged)) ;//获得当前时段已看诊数,作为看诊序列号
                    seeNo = int.Parse(Convert.ToString(schema.SeeNO + schema.TelingQTY - schema.TeledQTY));
                }
                else
                {
                    //seeNo = int.Parse(Convert.ToString(schema.RegedQty + schema.TelReged + schema.SpeReged)) ;//获得当前时段已看诊数,作为看诊序列号
                    seeNo = schema.SeeNO;
                }
            }

            if (isOverrun)
            {
                //加号不用提示
                if (schema.Templet.IsAppend) return 0;

                //if (!this.IsAllowOverrun)
                //{
                    Err = "已经超出出诊排班限额,不能挂号!";
                    return -1;
                //}
            }

            return 0;
        }

        /// <summary>
        /// 更新全院看诊序号
        /// </summary>
        /// <param name="rMgr"></param>
        /// <param name="current"></param>
        /// <param name="seeNo"></param>
        /// <param name="Err"></param>
        /// <returns></returns>
        private int Update(FS.HISFC.BizLogic.Registration.Register rMgr, DateTime current, ref int seeNo, ref string Err)
        {
            //更新看诊序号
            //全院是全天大排序，所以午别不生效，默认 1
            if (rMgr.UpdateSeeNo("4", current, "ALL", "1") == -1)
            {
                Err = rMgr.Err;
                return -1;
            }

            //获取全院看诊序号
            if (rMgr.GetSeeNo("4", current, "ALL", "1", ref seeNo) == -1)
            {
                Err = rMgr.Err;
                return -1;
            }

            return 0;
        }
    }
}
