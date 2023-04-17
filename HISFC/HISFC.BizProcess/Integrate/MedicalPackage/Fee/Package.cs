using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.HISFC.BizProcess.Integrate.MedicalPackage.Fee
{
    /// <summary>
    /// 购买套餐业务层
    /// </summary>
    public class Package : IntegrateBase
    {
        #region 逻辑管理类


        /// <summary>
        /// 套餐业务主数据
        /// </summary>
        private FS.HISFC.BizLogic.MedicalPackage.Package basePackageMgr = new FS.HISFC.BizLogic.MedicalPackage.Package();

        /// <summary>
        /// 套餐业务主数据
        /// </summary>
        private FS.HISFC.BizLogic.MedicalPackage.Fee.Package packageMgr = new FS.HISFC.BizLogic.MedicalPackage.Fee.Package();

        /// <summary>
        /// 套餐业务明细数据
        /// </summary>
        private FS.HISFC.BizLogic.MedicalPackage.Fee.PackageDetail packageDetailMgr = new FS.HISFC.BizLogic.MedicalPackage.Fee.PackageDetail();

        /// <summary>
        /// 押金业务数据类
        /// </summary>
        private FS.HISFC.BizLogic.MedicalPackage.Fee.Deposit depositMgr = new FS.HISFC.BizLogic.MedicalPackage.Fee.Deposit();

        /// <summary>
        /// 发票管理
        /// </summary>
        private FS.HISFC.BizLogic.MedicalPackage.Fee.Invoice invoiceMgr = new FS.HISFC.BizLogic.MedicalPackage.Fee.Invoice();

        /// <summary>
        /// 支付方式
        /// </summary>
        private FS.HISFC.BizLogic.MedicalPackage.Fee.PayMode paymodeMgr = new FS.HISFC.BizLogic.MedicalPackage.Fee.PayMode();
        /// <summary>
        /// 消费记录管理类
        /// </summary>
        private FS.HISFC.BizLogic.MedicalPackage.Fee.PackageCost packageCostMgr = new FS.HISFC.BizLogic.MedicalPackage.Fee.PackageCost();
        /// <summary>
        /// 账户管理
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Account.AccountPay accountPay = new FS.HISFC.BizProcess.Integrate.Account.AccountPay();

        /// <summary>
        /// 费用业务层
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();

        /// <summary>
        /// 套餐管理类
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.MedicalPackage.Package packageProcess = new FS.HISFC.BizProcess.Integrate.MedicalPackage.Package();
        /// <summary>
        /// 非药品
        /// </summary>
        FS.HISFC.BizLogic.Fee.Item itemMgr = new FS.HISFC.BizLogic.Fee.Item();

        /// <summary>
        /// 药品
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Pharmacy itemIntegrate = new FS.HISFC.BizProcess.Integrate.Pharmacy();

        /// <summary>
        /// 费用类业务层
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.InPatient inpatientManager = new FS.HISFC.BizLogic.Fee.InPatient();

        /// <summary>
        /// 控制类业务层
        /// </summary>
        protected FS.FrameWork.Management.ControlParam controlManager = new FS.FrameWork.Management.ControlParam();

        /// <summary>
        /// 管理业务层
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new Manager();

        /// <summary>
        /// 门诊业务层
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.Outpatient outpatientManager = new FS.HISFC.BizLogic.Fee.Outpatient();

        /// <summary>
        /// /// 发票业务层
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.InvoiceServiceNoEnum invoiceServiceManager = new FS.HISFC.BizLogic.Fee.InvoiceServiceNoEnum();
        
        #endregion

        /// <summary>
        /// 获取新的划价单号
        /// </summary>
        /// <returns></returns>
        public string GetNewRecipeNO()
        {
            return this.packageMgr.GetNewRecipeNO();
        }

        /// <summary>
        /// 根据患者查询划价单
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        public ArrayList GetRecipesByPatient(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            return this.packageMgr.GetRecipesByPatient(patient,"1");
        }

        /// <summary>
        /// 根据划价单号查询押金记录单(暂未调用)
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        public ArrayList GetDepositsByRecipe(string RecipeNO)
        {
            //FS.HISFC.Models.MedicalPackage.Fee.Deposit
            return new ArrayList();
        }

        /// <summary>
        /// 根据病人获取押金记录
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        public ArrayList GetDepositsByPatient(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            return depositMgr.QueryByCardNO(patientInfo.PID.CardNO,"0");
        }

        /// <summary>
        /// 根据病人获取押金消费记录
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        public ArrayList GetCostDepositsByPatient(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            return depositMgr.QueryByCardNO(patientInfo.PID.CardNO, "1");
        }

        /// <summary>
        /// 根据病人和时间获取押金记录
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        public ArrayList GetDepositsByPatientAndDate(FS.HISFC.Models.RADT.PatientInfo patientInfo, DateTime begin, DateTime end)
        {
            return depositMgr.QueryByCardNOAndDate(patientInfo.PID.CardNO, "0",begin,end);
        }

        /// <summary>
        /// 根据病人和时间获取押金消费记录
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        public ArrayList GetCostDepositsByPatientAndDate(FS.HISFC.Models.RADT.PatientInfo patientInfo, DateTime begin, DateTime end)
        {
            return depositMgr.QueryByCardNOAndDate(patientInfo.PID.CardNO, "1", begin, end);
        }

        /// <summary>
        /// 根据划价单查询套餐列表
        /// </summary>
        /// <param name="RecipeNO"></param>
        /// <returns></returns>
        public ArrayList GetFeeInfoByRecipe(string RecipeNO)
        {
            ArrayList feeInfoList =  this.packageMgr.QueryByRecipeNO(RecipeNO, "1");
            if (feeInfoList != null)
            {
                foreach (FS.HISFC.Models.MedicalPackage.Fee.Package package in feeInfoList)
                {
                    package.PackageInfo = this.basePackageMgr.QueryPackageByID(package.PackageInfo.ID);
                }
            }

            return feeInfoList;
        }

        /// <summary>
        /// 根据套餐查询明细
        /// </summary>
        /// <param name="RecipeNO"></param>
        /// <returns></returns>
        public ArrayList GetDetailByPackage(FS.HISFC.Models.MedicalPackage.Fee.Package package)
        {
            return this.packageProcess.GetPackageItemByPackageID(package.PackageInfo.ID);
        }

        /// <summary>
        /// 套餐收费
        /// </summary>
        /// <returns></returns>
        public int SaveFee(FS.HISFC.Models.RADT.PatientInfo PatientInfo, ArrayList checkedFee, 
                           ArrayList payInfo,ArrayList depositInfo,Hashtable costInfo,Hashtable hsDetals,ref ArrayList depositPayInfo, ref string ErrInfo, ref string FeeInvoiceNO)
        {

            try
            {
                //用来存储所有的支付方式
                ArrayList PackageInfoList = new ArrayList();
                ArrayList PackageDetailInfoList = new ArrayList();
                ArrayList PayInfoList = new ArrayList();
                PackageInfoList.AddRange(checkedFee);
                PayInfoList.AddRange(payInfo);


                //获取发票号
                string invoiceNO = string.Empty;
                string realInvoiceNO = string.Empty;
                string errText = string.Empty;
                FS.HISFC.Models.Base.Employee oper = this.invoiceMgr.Operator as FS.HISFC.Models.Base.Employee;



                if (this.feeIntegrate.GetInvoiceNO(oper, "M", ref invoiceNO, ref realInvoiceNO, ref errText) == -1)
                {
                    ErrInfo = errText;
                    return -1;
                }

                //获取所有的费用类别信息
                decimal totCost = (decimal)costInfo["TOT"];           //套餐原价
                decimal actuCost = (decimal)costInfo["ACTU"];         //实收金额
                //{333D2AD8-DC4A-4c30-A14E-D6815AC858F9}
                decimal giftCost = (decimal)costInfo["GIFT"] + (decimal)costInfo["COU"];         //赠送金额与积分金额
                decimal depoCost = (decimal)costInfo["DEPO"];         //押金金额
                decimal etcCost = (decimal)costInfo["ETC"];           //优惠金额
                decimal roundCost = (decimal)costInfo["ROUND"];       //四舍五入

                //正常情况下不会存在支付总额大于应付总额的情况,但如果患者缴纳
                //的单笔押金额大于套餐的总金额时，需要进行处理
                if (actuCost + giftCost + depoCost + etcCost > totCost)
                {
                    if (actuCost > 0 || giftCost > 0)
                    {
                        ErrInfo = "缴纳的金额多余应缴金额，请调整金额！";
                        return -1;
                    }
                }

                if (actuCost + giftCost + depoCost + etcCost < totCost)
                {
                    ErrInfo = "缴纳的金额不足，请调整金额！";
                    return -1;
                }

                #region 押金处理

                //实际应该缴纳的押金额
                decimal shouldDepo = totCost - giftCost - actuCost - etcCost;
                //将押金记录转化为支付记录并且计算出真正的 实付金额，赠送金额
                foreach (FS.HISFC.Models.MedicalPackage.Fee.Deposit deposit in depositInfo)
                {
                    if (shouldDepo == 0)
                        break;

                    FS.HISFC.Models.MedicalPackage.Fee.PayMode depositpay = new FS.HISFC.Models.MedicalPackage.Fee.PayMode();
                    depositpay.Mode_Code = "DE";
                    depositpay.Related_ID = deposit.ID;               //押金记录号
                    depositpay.Related_ModeCode = deposit.Mode_Code;  //押金缴纳方式
                    depositpay.Account = deposit.Account;             //押金的缴纳账号
                    depositpay.AccountFlag = deposit.AccountFlag;     //押金的缴纳账户标识
                    depositpay.AccountType = deposit.AccountType;     //押金的缴纳账户类型
                    depositpay.Cancel_Flag = "0";
                    depositpay.Trans_Type = "1";
                    depositpay.Oper = FS.FrameWork.Management.Connection.Operator.ID;
                    depositpay.OperTime = this.packageMgr.GetDateTimeFromSysDateTime();

                    //部分支付
                    if (shouldDepo <= deposit.Amount) 
                    {
                        depositpay.Tot_cost = depositpay.Real_Cost = shouldDepo;
                        shouldDepo = 0;
                    }
                    else  //整条押金支付
                    {
                        depositpay.Tot_cost = depositpay.Real_Cost = deposit.Amount;
                        shouldDepo -= deposit.Amount;
                    }

                    if (depositpay.Related_ModeCode == "DC" && depositpay.AccountFlag == "1")
                    {
                        giftCost += depositpay.Tot_cost;
                    }
                    else
                    {
                        actuCost += depositpay.Tot_cost;
                    }

                    PayInfoList.Add(depositpay);

                    depositPayInfo.Add(depositpay);

                    //消费押金的函数
                    if (this.depositMgr.DepositCost(deposit, depositpay.Tot_cost) < 0)
                    {
                        ErrInfo = this.depositMgr.Err;
                        return -1;
                    }
                }

                if (totCost != actuCost + giftCost + etcCost)
                {
                    ErrInfo = "费用类别计算错误！";
                    return -1;
                }

                #endregion

                #region 支付方式处理

                //{F166B18B-62E3-4835-A729-4CA384F9ADEE}
                FS.HISFC.BizLogic.Manager.Constant constManager = new FS.HISFC.BizLogic.Manager.Constant();

                //FS.FrameWork.Models.NeuObject cashCouponPayMode = constManager.GetConstant("XJLZFFS", "1");

                //decimal cashCouponAmount = 0.0m;

                //支付方式seq
                int payinfoSequence = 1;
                 //  {D59EF243-868D-41a0-9827-5E2E608522CA}
                 HISFC.Models.Base. Employee employee = FS.FrameWork.Management.Connection.Operator as HISFC.Models.Base. Employee ;
                 HISFC.Models.Base.Department dept= employee.Dept as HISFC.Models.Base.Department ;

                foreach (FS.HISFC.Models.MedicalPackage.Fee.PayMode pay in PayInfoList)
                {
                        

                    pay.InvoiceNO = invoiceNO;
                    pay.Trans_Type = "1";
                    pay.SequenceNO = payinfoSequence.ToString();
                    pay.Oper = FS.FrameWork.Management.Connection.Operator.ID;
                    pay.OperTime = this.packageMgr.GetDateTimeFromSysDateTime();
                    //  {D59EF243-868D-41a0-9827-5E2E608522CA}
                    pay.Hospital_ID = dept.HospitalID;
                    pay.HospitalName = dept.HospitalName;
                    //账户扣费
                    if (pay.Mode_Code == "YS")
                    {
                        if (accountPay.OutpatientPay(PatientInfo,
                                                    pay.Account,
                                                    pay.AccountType,
                                                    -pay.Tot_cost,
                                                    0,
                                                    invoiceNO, PatientInfo,
                                                    FS.HISFC.Models.Account.PayWayTypes.P,
                                                    1) < 1)
                        {
                            ErrInfo = "账户扣费失败！";
                            return -1;
                        }

                    }

                    if (pay.Mode_Code == "DC")
                    {
                        if (accountPay.OutpatientPay(PatientInfo,
                                                    pay.Account,
                                                    pay.AccountType,
                                                    0,
                                                    -pay.Tot_cost,
                                                    invoiceNO, PatientInfo,
                                                    FS.HISFC.Models.Account.PayWayTypes.P,
                                                    1) < 1)
                        {
                            ErrInfo = "账户扣费失败！";
                            return -1;
                        }

                    }

                    //插入支付表
                    if (this.paymodeMgr.Insert(pay) < 0)
                    {
                        ErrInfo = "插入支付方式失败！";
                        return -1;
                    }


                    //if (cashCouponPayMode.Name.Contains(pay.Mode_Code.ToString()) || (pay.Mode_Code == "DE" && cashCouponPayMode.Name.Contains(pay.Related_ModeCode.ToString())))
                    //{
                    //    cashCouponAmount += pay.Tot_cost;
                    //}

                    payinfoSequence++;
                }

                //{F166B18B-62E3-4835-A729-4CA384F9ADEE}
                //if (cashCouponAmount > 0 || cashCouponAmount < 0)
                //{
                //    HISFC.BizProcess.Integrate.Account.CashCoupon cashCouponPrc = new FS.HISFC.BizProcess.Integrate.Account.CashCoupon();
                //    if (cashCouponPrc.CashCouponSave("TCSF", PatientInfo.PID.CardNO, invoiceNO, cashCouponAmount, ref errText) <= 0)
                //    {
                //        this.Err = "计算现金流积分出错!" + errText;
                //        return -1;
                //    }

                //}

                if (roundCost != 0)
                {
                    FS.HISFC.Models.MedicalPackage.Fee.PayMode pay = new FS.HISFC.Models.MedicalPackage.Fee.PayMode();
                    pay.InvoiceNO = invoiceNO;
                    pay.Trans_Type = "1";
                    pay.SequenceNO = payinfoSequence.ToString();
                    pay.Mode_Code = "SW";
                    pay.Oper = FS.FrameWork.Management.Connection.Operator.ID;
                    pay.OperTime = this.packageMgr.GetDateTimeFromSysDateTime();
                    pay.Tot_cost = pay.Real_Cost = roundCost;
                    pay.Cancel_Flag = "0";

                    
                    //插入支付表
                    if (this.paymodeMgr.Insert(pay) < 0)
                    {
                        ErrInfo = "插入四舍五入位失败！";
                        return -1;
                    }
                }

                #endregion

                #region 发票信息处理

                //  {D59EF243-868D-41a0-9827-5E2E608522CA}
              
                FS.HISFC.Models.MedicalPackage.Fee.Invoice invoice = new FS.HISFC.Models.MedicalPackage.Fee.Invoice();
                invoice.InvoiceNO = invoiceNO;
                invoice.Trans_Type = "1";
                invoice.Paykindcode = PatientInfo.Pact.ID;
                invoice.Card_Level = PatientInfo.Pact.User01;
                invoice.Package_Cost = totCost;
                invoice.Real_Cost = actuCost;
                invoice.Gift_cost = giftCost;
                invoice.Etc_cost = etcCost;
                invoice.InvoiceSeq = this.invoiceMgr.GetNewInvoiceSeq();  //
                invoice.PrintInvoiceNO = realInvoiceNO;
                invoice.Memo = PatientInfo.Memo;// {F53BD032-1D92-4447-8E20-6C38033AA607}
                invoice.Oper = FS.FrameWork.Management.Connection.Operator.ID;
                invoice.OperTime = this.invoiceMgr.GetDateTimeFromSysDateTime();
                invoice.Cancel_Flag = "0";
                //  {D59EF243-868D-41a0-9827-5E2E608522CA}
                invoice.Hospital_ID = dept.HospitalID;
                invoice.HospitalName = dept.HospitalName;
                if (this.invoiceMgr.Insert(invoice) < 0)
                {
                    ErrInfo = "发票信息插入失败！";
                    return -1;
                }

                #endregion

                #region 套餐与明细处理

                decimal PackageReal = 0.0m;
                for (int i = 0; i < PackageInfoList.Count; i++)
                {
                    FS.HISFC.Models.MedicalPackage.Fee.Package package = PackageInfoList[i] as FS.HISFC.Models.MedicalPackage.Fee.Package;
                    PackageReal += package.Real_Cost;
                }

                decimal countactu = actuCost;
                decimal countgift = giftCost;
                decimal countetc = etcCost;

                //套餐序号
                Hashtable parentSequence = new Hashtable();
                Hashtable recipeIndex = new Hashtable();

                //计算费用套餐的实收，赠送和优惠金额
                //【优惠金额实体中已自带，只需要根据赠送金额去计算 实收和赠送即可】
                for (int i = 0; i < PackageInfoList.Count; i++)
                {
                    FS.HISFC.Models.MedicalPackage.Fee.Package package = PackageInfoList[i] as FS.HISFC.Models.MedicalPackage.Fee.Package;

                    decimal real_cost = 0.0m;
                    decimal gift_cost = 0.0m;
                    //{2694417D-715F-4ef6-A664-1F92399DC325}
                    //decimal etc_cost = 0.0m;

                    //最后一个项目的价格由总价格减去前面所有项目的价格
                    if (i == PackageInfoList.Count - 1)
                    {
                        #region 旧的算法
                        //real_cost = countactu;
                        //gift_cost = countgift;
                        //etc_cost = countetc;
                        #endregion

                        #region 新的算法
                        //{2694417D-715F-4ef6-A664-1F92399DC325}
                        gift_cost = countgift;
                        real_cost = package.Real_Cost;
                        real_cost -= gift_cost;
                        #endregion
                    }
                    else
                    {
                        #region 新的算法
                        //{2694417D-715F-4ef6-A664-1F92399DC325}
                        //根据实收的比例来分配赠送金额
                        gift_cost = Math.Ceiling((package.Real_Cost * giftCost * 100) / (PackageReal == 0 ? 1 : PackageReal)) / 100;//{CE65A8ED-B2E8-4066-8364-A2BDF4CDDD88}
                        //{C586129A-D831-4c92-BFD9-D777B6B91C5E}
                        //最后一个项目实收金额为0时，逻辑有漏洞
                        if (gift_cost > countgift)
                        {
                            gift_cost = countgift;
                        }
                        real_cost = package.Real_Cost;
                        real_cost -= gift_cost;
                        countgift -= gift_cost;
                        #endregion
                    }

                    package.Real_Cost = real_cost;
                    package.Gift_cost = gift_cost;
                    //{2694417D-715F-4ef6-A664-1F92399DC325}
                    //package.Etc_cost = etc_cost;

                    //用来验证
                    actuCost -= package.Real_Cost;

                    package.InvoiceNO = invoiceNO;
                    //package.Memo = PatientInfo.Memo;
                    package.Invoiceseq = invoice.InvoiceSeq;
                    package.SequenceNO = (i + 1).ToString();
                    
                    if (parentSequence.Contains(package.RecipeNO + package.PackageSequenceNO))
                    {
                        package.PackageSequenceNO = parentSequence[package.RecipeNO + package.PackageSequenceNO].ToString();
                    }
                    else
                    {
                        int index = 1;
                        if (recipeIndex.Contains(package.RecipeNO))
                        {
                            index = Int32.Parse(recipeIndex[package.RecipeNO].ToString()) + 1;
                        }
                        else
                        {
                            recipeIndex.Add(package.RecipeNO, index);
                        }

                        string tmpParentSequece = package.PackageSequenceNO;
                        package.PackageSequenceNO = index.ToString();
                        parentSequence.Add(package.RecipeNO + tmpParentSequece, package.PackageSequenceNO);
                        recipeIndex[package.RecipeNO] = index;
                    }

                    package.Patient = PatientInfo;
                    if (this.savePackage(PatientInfo,package, invoiceNO,hsDetals, ref ErrInfo) < 0)
                    {
                        return -1;
                    }

                }

                //{2694417D-715F-4ef6-A664-1F92399DC325}
                if (actuCost != 0)
                {
                    ErrInfo = "分配赠送金额发生错误！";
                    return -1;
                }

                #endregion

                #region 返回发票号
                //{F31B0DE2-C48A-4a86-A917-43930C602D52}
                FeeInvoiceNO = invoiceNO;
                #endregion

                #region 发票走号
                if (this.UseInvoiceNO(oper, "1", "M", 1, ref invoiceNO, ref realInvoiceNO, ref ErrInfo) < 0)
                {
                    return -1;
                }

                if(this.InsertInvoiceExtend(invoiceNO,"M",realInvoiceNO,"00") < 0)
                {
                    ErrInfo = this.Err;
                    return -1;
                }
                #endregion


            }
            catch (Exception ex)
            {
                ErrInfo = ex.Message;
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// {7C8E0BBA-04CB-4457-9399-39A25804EA12}
        /// {56809DCA-CD5A-435e-86F0-93DE99227DF4}
        /// 根据套餐ID更改套餐特殊折扣标记
        /// </summary>
        /// <param name="RecipeNO"></param>
        /// <param name="PayFlag"></param>
        /// <returns></returns>
        public int UpdateSpecialFlagByID(ArrayList packageList)
        {
            foreach (FS.HISFC.Models.MedicalPackage.Fee.Package package in packageList)
            {
                int rtn = this.packageMgr.UpdateSpecialFlagByID(package.ID, package.SpecialFlag);
                if (rtn <= 0)
                {
                    return -1;
                }
            }

            return 1;
        }

        /// <summary>
        /// 根据套餐信息计算明细项目价格信息并插入数据库
        /// </summary>
        /// <param name="package"></param>
        /// <returns></returns>
        private int savePackage(FS.HISFC.Models.RADT.PatientInfo PatientInfo, FS.HISFC.Models.MedicalPackage.Fee.Package package, string InvoiceNO,Hashtable hsDetails, ref string ErrInfo)
        {
            try
            {
                //{01C202AF-5D8A-4d89-9BA5-1910B5AA7607} 
                FS.HISFC.Models.Base.Employee employee = FS.FrameWork.Management.Connection.Operator as HISFC.Models.Base.Employee;
                FS.HISFC.Models.Base.Department dept = employee.Dept as HISFC.Models.Base.Department;
                //{01C202AF-5D8A-4d89-9BA5-1910B5AA7607} 
                package.HospitalID = dept.HospitalID;
                package.HospitalName = dept.HospitalName;
                if (string.IsNullOrEmpty(package.ID))
                {
                    package.ID = this.packageMgr.GetNewClinicNO();
                    package.Pay_Flag = "1";
                    package.Cancel_Flag = "0";
                    package.Oper = FS.FrameWork.Management.Connection.Operator.ID;
                    package.OperTime = this.invoiceMgr.GetDateTimeFromSysDateTime();
                    package.DelimitOper = FS.FrameWork.Management.Connection.Operator.ID;
                    package.DelimitTime = this.invoiceMgr.GetDateTimeFromSysDateTime();
                   

                    if (this.packageMgr.Insert(package) < 0)
                    {
                        ErrInfo = "插入套餐失败!";
                        package.ID = "";
                        return -1;
                    }
                }
                else
                {
                    FS.HISFC.Models.MedicalPackage.Fee.Package pac = this.packageMgr.QueryByID(package.ID);
                    if (pac != null && pac.Pay_Flag != "0")
                    {
                        ErrInfo = "部分划价费用已收费，请刷新界面重试！";
                        return -1;
                    }
                    package.Pay_Flag = "1";
                    package.Oper = FS.FrameWork.Management.Connection.Operator.ID;
                    package.OperTime = this.invoiceMgr.GetDateTimeFromSysDateTime();
                    if (this.packageMgr.Update(package) < 0)
                    {
                        ErrInfo = "更新套餐失败!";
                        return -1;
                    }
                }


                decimal totCost = package.Package_Cost;
                decimal actuCost = package.Real_Cost;
                decimal giftCost = package.Gift_cost;
                decimal etcCost = package.Etc_cost;

                ArrayList detailList = new ArrayList();

                if (package.PackageInfo.SpecialFlag == "1")
                {
                    if (hsDetails.ContainsKey(package.User01))
                    {
                        detailList = hsDetails[package.User01] as ArrayList;
                    }
                }
                else
                {
                    detailList = this.GetDetailByPackage(package);
                }

                if (detailList == null || detailList.Count == 0)
                {
                    ErrInfo = "获取【" + package.ID + "】明细失败!";
                    return -1;
                }

                decimal countactu = actuCost;
                decimal countgift = giftCost;
                decimal countetc = etcCost;
                int sequenceNO = 1;
                for (int i = 0; i < detailList.Count; i++)
                {
                    FS.HISFC.Models.MedicalPackage.PackageDetail detail = detailList[i] as FS.HISFC.Models.MedicalPackage.PackageDetail;
                    decimal tmpQTY = detail.Item.Qty;
                    if (detail.Item.SysClass.ID.ToString().Equals("P") ||
                        detail.Item.SysClass.ID.ToString().Equals("PCC") ||
                        detail.Item.SysClass.ID.ToString().Equals("PCZ"))
                    {
                        detail.Item = itemIntegrate.GetItem(detail.Item.ID);
                    }
                    else
                    {
                        detail.Item = itemMgr.GetUndrugByCode(detail.Item.ID);
                    }

                    detail.Item.Qty = tmpQTY;

                    FS.HISFC.Models.MedicalPackage.Fee.PackageDetail feeDetail = new FS.HISFC.Models.MedicalPackage.Fee.PackageDetail();
                    feeDetail.ID = package.ID;
                    feeDetail.SequenceNO = sequenceNO.ToString();
                    feeDetail.Trans_Type = "1";
                    feeDetail.PayFlag = "1";
                    feeDetail.CardNO = PatientInfo.PID.CardNO;
                    feeDetail.Item = detail.Item;
                    feeDetail.RtnQTY = detail.Item.Qty;
                    feeDetail.ConfirmQTY = 0;
                    feeDetail.Unit = detail.Unit;
                    feeDetail.UnitFlag = detail.UnitFlag;
                    feeDetail.InvoiceNO = InvoiceNO;
                    feeDetail.Cancel_Flag = "0";
                    feeDetail.Oper = FS.FrameWork.Management.Connection.Operator.ID;
                    feeDetail.OperTime = this.invoiceMgr.GetDateTimeFromSysDateTime();

                    //{01C202AF-5D8A-4d89-9BA5-1910B5AA7607} 
                    feeDetail.HospitalID = dept.HospitalID;
                    feeDetail.HospitalName = dept.HospitalName;
                    if (detail.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        if (detail.UnitFlag.Equals("0"))
                        {
                            feeDetail.Detail_Cost = Math.Round((detail.Item.Price / detail.Item.PackQty) * detail.Item.Qty, 2);
                        }
                        else
                        {
                            feeDetail.Detail_Cost = detail.Item.Price * detail.Item.Qty;
                        }
                    }
                    else
                    {
                        feeDetail.Detail_Cost += detail.Item.Price * detail.Item.Qty;
                    }

                    decimal real_cost = 0.0m;
                    decimal gift_cost = 0.0m;
                    decimal etc_cost = 0.0m;

                    //最后一个项目的价格由总价格减去前面所有项目的价格
                    if (i == detailList.Count - 1)
                    {
                        real_cost = countactu;
                        gift_cost = countgift;
                        etc_cost = countetc;
                    }
                    else
                    {
                        //总是直接舍去小数点后两位以后的数值，以防止最后一个项目的价格出现负数
                        real_cost = Math.Floor((feeDetail.Detail_Cost * actuCost * 100) / totCost) / 100;
                        gift_cost = Math.Floor((feeDetail.Detail_Cost * giftCost * 100) / totCost) / 100;
                        etc_cost = Math.Floor((feeDetail.Detail_Cost * etcCost * 100) / totCost) / 100;
                    }

                    if (countactu >= real_cost)
                    {
                        countactu -= real_cost;
                    }
                    else
                    {
                        real_cost = countactu;
                        countactu = 0;
                    }

                    if (countgift > gift_cost)
                    {
                        countgift -= gift_cost;
                    }
                    else
                    {
                        gift_cost = countgift;
                        countgift = 0;
                    }

                    if (countetc >= etc_cost)
                    {
                        countetc -= etc_cost;
                    }
                    else
                    {
                        etc_cost = countetc;
                        countetc = 0;
                    }

                    if (real_cost < 0 || gift_cost < 0)
                    {
                        ErrInfo = "计算套餐费用类别时出现错误！";
                        return -1;
                    }

                    //当总价不等于优惠金额+实收金额+赠送金额的时候
                    //{E9D5059F-E0F6-43a8-9844-5640FAB88DAF}
                    if (feeDetail.Detail_Cost > real_cost + gift_cost + etc_cost)
                    {
                        decimal diff = feeDetail.Detail_Cost - real_cost - gift_cost - etc_cost;

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
                                diff -= countactu;
                                countactu = 0;

                                if (countgift >= diff)
                                {
                                    countgift -= diff;
                                    gift_cost += diff;
                                }
                                else
                                {
                                    gift_cost += countgift;
                                    diff -= countgift;
                                    countgift = 0;

                                    if (countetc >= diff)
                                    {
                                        countetc -= diff;
                                        etc_cost += diff;
                                    }
                                    else
                                    {
                                        ErrInfo = "计算套餐费用类别时出现错误！";
                                        return -1;
                                    }
                                }
                            }
                        }
                    }

                    feeDetail.Real_Cost = real_cost;
                    feeDetail.Gift_cost = gift_cost;
                    feeDetail.Etc_cost = etc_cost;

                    if (this.packageDetailMgr.Insert(feeDetail) < 0)
                    {
                        ErrInfo = "插入套餐明细失败:" + this.packageDetailMgr.Err;
                        return -1;
                    }

                    sequenceNO++;

                }
            }
            catch (Exception ex)
            {
                ErrInfo = ex.Message;
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 套餐退费
        /// </summary>
        /// <param name="PatientInfo">患者信息</param>
        /// <param name="QuitFee">退费套餐</param>
        /// <param name="HSQuitDetail">退费明细</param>
        /// <param name="QuitPayModeList">退费支付方式</param>
        /// <param name="invoiceInfo">发票信息</param>
        /// <param name="QuitWay">退费途径</param>
        /// <param name="QuitWay">费用类别</param>
        /// <param name="ErrInfo">错误信息</param>
        /// <returns></returns>
        public int SaveQuit(FS.HISFC.Models.RADT.PatientInfo PatientInfo,    
                            ArrayList QuitFee,           
                            Hashtable HSQuitDetail,       
                            ArrayList QuitPayModeList,    
                            FS.HISFC.Models.MedicalPackage.Fee.Invoice invoiceInfo,
                            string QuitWay,
                            ArrayList ReFeePackage,
                            Hashtable HSReFeeDetail,
                            Hashtable HSCostInfo,
                            Hashtable HSPayInfo,
                            ref string ErrInfo,
                            ref decimal quitCostCouponAmount,
                            ref decimal quitOperateCouponAmount,
                            ref decimal refeeCostCouponAmount,
                            ref decimal refeeOperateCouponAmount,
                            ref string newInvoiceNO)
        {
            quitCostCouponAmount = 0.0m;
            quitOperateCouponAmount = 0.0m;
            refeeCostCouponAmount = 0.0m;
            refeeOperateCouponAmount = 0.0m;

            #region 处理套餐主表和明细表

            foreach(FS.HISFC.Models.MedicalPackage.Fee.Package package in QuitFee)
            {

                FS.HISFC.Models.MedicalPackage.Fee.Package tmppackage = this.packageMgr.QueryByID(package.ID);
                if (tmppackage == null || tmppackage.Cancel_Flag != "0")
                {
                    ErrInfo = "费用已发生改变，请刷新后重试！";
                    return -1;
                }

                tmppackage.Cancel_Flag = "1";
                tmppackage.CancelOper = FS.FrameWork.Management.Connection.Operator.ID;
                tmppackage.CancelTime = this.packageMgr.GetDateTimeFromSysDateTime();

                //更新旧记录
                if (this.packageMgr.Update(tmppackage) < 1)
                {
                    ErrInfo = this.packageMgr.Err;
                    return -1;
                }

                //插入负记录
                FS.HISFC.Models.MedicalPackage.Fee.Package newPackage = tmppackage.Clone();
                newPackage.Trans_Type = "2";
                newPackage.Package_Cost = -package.Package_Cost;
                newPackage.Real_Cost = -package.Real_Cost;
                newPackage.Gift_cost = -package.Gift_cost;
                newPackage.Etc_cost = -package.Etc_cost;
                newPackage.Cancel_Flag = "1";
                newPackage.CancelOper = FS.FrameWork.Management.Connection.Operator.ID;
                newPackage.CancelTime = this.packageMgr.GetDateTimeFromSysDateTime();
                //{030CEF69-9D8D-4a12-8158-1AB14920B7D5}
                newPackage.Oper = FS.FrameWork.Management.Connection.Operator.ID;
                newPackage.OperTime = this.packageMgr.GetDateTimeFromSysDateTime();

                if (this.packageMgr.Insert(newPackage) < 1)
                {
                    ErrInfo = this.packageMgr.Err;
                    return -1;
                }
 

                ArrayList detailList = HSQuitDetail[package.ID] as ArrayList;

                if(detailList == null)
                {
                    ErrInfo = "套餐明细为空！";
                    return -1;
                }

                foreach (FS.HISFC.Models.MedicalPackage.Fee.PackageDetail detail in detailList)
                {

                    FS.HISFC.Models.MedicalPackage.Fee.PackageDetail tmpdetail = this.packageDetailMgr.QueryDetailByClinicSeq(detail.ID,"1",detail.SequenceNO);
                    if (tmpdetail == null || tmpdetail.Cancel_Flag != "0")
                    {
                        ErrInfo = "费用已发生改变，请刷新后重试！";
                        return -1;
                    }

                    tmpdetail.Cancel_Flag = "1";
                    tmpdetail.CancelOper = FS.FrameWork.Management.Connection.Operator.ID;
                    tmpdetail.CancelTime = this.packageMgr.GetDateTimeFromSysDateTime();

                    //更新旧记录
                    if (this.packageDetailMgr.Update(tmpdetail) < 1)
                    {
                        ErrInfo = this.packageDetailMgr.Err;
                        return -1;
                    }

                    FS.HISFC.Models.MedicalPackage.Fee.PackageDetail newPackageDetail = tmpdetail.Clone();
                    newPackageDetail.Trans_Type = "2";
                    newPackageDetail.Detail_Cost = -detail.Detail_Cost;
                    newPackageDetail.Real_Cost = -detail.Real_Cost;
                    newPackageDetail.Gift_cost = -detail.Gift_cost;
                    newPackageDetail.Etc_cost = -detail.Etc_cost;
                    newPackageDetail.Item.Qty = -detail.Item.Qty;
                    newPackageDetail.Cancel_Flag = "1";
                    newPackageDetail.CancelOper = FS.FrameWork.Management.Connection.Operator.ID;
                    newPackageDetail.CancelTime = this.packageMgr.GetDateTimeFromSysDateTime();
                    //{030CEF69-9D8D-4a12-8158-1AB14920B7D5}
                    newPackageDetail.Oper = FS.FrameWork.Management.Connection.Operator.ID;
                    newPackageDetail.OperTime = this.packageMgr.GetDateTimeFromSysDateTime();
                    //插入负记录
                    if (this.packageDetailMgr.Insert(newPackageDetail) < 1)
                    {
                        ErrInfo = this.packageDetailMgr.Err;
                        return -1;
                    }
                }

            }

            #endregion

            #region 处理退费发票

            FS.HISFC.Models.MedicalPackage.Fee.Invoice tmpInvoice = this.invoiceMgr.QueryByInvoiceNO(invoiceInfo.InvoiceNO, "1");
            if (tmpInvoice == null || tmpInvoice.Cancel_Flag != "0")
            {
                ErrInfo = "费用已发生改变，请刷新后重试！";
                return -1;
            }

            tmpInvoice.Cancel_Flag = "1";
            tmpInvoice.CancelOper = FS.FrameWork.Management.Connection.Operator.ID;
            tmpInvoice.CancelTime = this.packageMgr.GetDateTimeFromSysDateTime();

            //更新旧记录
            if (this.invoiceMgr.Update(tmpInvoice) < 1)
            {
                ErrInfo = this.invoiceMgr.Err;
                return -1;
            }

            FS.HISFC.Models.MedicalPackage.Fee.Invoice newInvoice = tmpInvoice.Clone(false);
            newInvoice.Trans_Type = "2";
            newInvoice.Package_Cost = -invoiceInfo.Package_Cost;
            newInvoice.Real_Cost = -invoiceInfo.Real_Cost;
            newInvoice.Gift_cost = -invoiceInfo.Gift_cost;
            newInvoice.Etc_cost = -invoiceInfo.Etc_cost;
            newInvoice.Cancel_Flag = "1";
            newInvoice.CancelOper = FS.FrameWork.Management.Connection.Operator.ID;
            newInvoice.CancelTime = this.packageMgr.GetDateTimeFromSysDateTime();
            //{030CEF69-9D8D-4a12-8158-1AB14920B7D5}
            newInvoice.Oper = FS.FrameWork.Management.Connection.Operator.ID;
            newInvoice.OperTime = this.packageMgr.GetDateTimeFromSysDateTime();

            //插入负记录
            if (this.invoiceMgr.Insert(newInvoice) < 1)
            {
                ErrInfo = this.invoiceMgr.Err;
                return -1;
            }

            #endregion

            #region 处理支付方式
            
            //{F166B18B-62E3-4835-A729-4CA384F9ADEE}
            FS.HISFC.BizLogic.Manager.Constant constManager = new FS.HISFC.BizLogic.Manager.Constant();

            FS.FrameWork.Models.NeuObject cashCouponPayMode = constManager.GetConstant("XJLZFFS", "1");

            //decimal cashCouponAmount = 0.0m;

            FS.HISFC.BizProcess.Integrate.Account.AccountPay accountPay = new FS.HISFC.BizProcess.Integrate.Account.AccountPay();
           
            foreach (FS.HISFC.Models.MedicalPackage.Fee.PayMode pay in QuitPayModeList)
            {
                FS.HISFC.Models.MedicalPackage.Fee.PayMode tmppay = new FS.HISFC.Models.MedicalPackage.Fee.PayMode();
                tmppay = this.paymodeMgr.QueryByInvoiceSeq(pay.InvoiceNO, pay.Trans_Type, pay.SequenceNO, pay.Cancel_Flag);
                if (tmppay == null || tmppay.Cancel_Flag != "0")
                {
                    ErrInfo = "费用已发生改变，请刷新后重试！";
                    return -1;
                }

                //账户退费
                if (pay.Mode_Code == "YS" || (pay.Mode_Code == "DE" && pay.Related_ModeCode == "YS"))
                {
                    if (accountPay.OutpatientPay(PatientInfo,
                                                 pay.Account,
                                                 pay.AccountType,
                                                 pay.Tot_cost,
                                                 0,
                                                 pay.InvoiceNO, PatientInfo,
                                                 FS.HISFC.Models.Account.PayWayTypes.M,
                                                 0) < 1)
                    {
                        //{2694417D-715F-4ef6-A664-1F92399DC325}
                        ErrInfo = "账户退费失败！" + accountPay.Err;
                        return -1;
                    }
                }

                //账户退费
                if (pay.Mode_Code == "DC" || (pay.Mode_Code == "DE" && pay.Related_ModeCode == "DC"))
                {
                    if (accountPay.OutpatientPay(PatientInfo,
                                                 pay.Account,
                                                 pay.AccountType,
                                                 0,
                                                 pay.Tot_cost,
                                                 pay.InvoiceNO, PatientInfo,
                                                 FS.HISFC.Models.Account.PayWayTypes.M,
                                                 0) < 1)
                    {
                        //{2694417D-715F-4ef6-A664-1F92399DC325}
                        ErrInfo = "账户退费失败！" + accountPay.Err;
                        return -1;
                    }
                }

                //{333D2AD8-DC4A-4c30-A14E-D6815AC858F9}
                if (pay.Mode_Code == "CO")
                {
                    quitCostCouponAmount -= pay.Tot_cost;
                }

                if (cashCouponPayMode.Name.Contains(pay.Mode_Code.ToString()) || (pay.Mode_Code == "DE" && cashCouponPayMode.Name.Contains(pay.Related_ModeCode.ToString())))
                {
                    //cashCouponAmount -= pay.Tot_cost;
                    quitOperateCouponAmount -= pay.Tot_cost;
                }

                tmppay.CancelOper = FS.FrameWork.Management.Connection.Operator.ID;
                tmppay.CancelTime = this.packageMgr.GetDateTimeFromSysDateTime();
                tmppay.Cancel_Flag = "1";

                //更新旧记录
                if (this.paymodeMgr.Update(tmppay) < 1)
                {
                    ErrInfo = this.invoiceMgr.Err;
                    return -1;
                }

                FS.HISFC.Models.MedicalPackage.Fee.PayMode newpaymode = tmppay.Clone(false);
                newpaymode.Trans_Type = "2";
                newpaymode.Real_Cost = -pay.Real_Cost;
                newpaymode.Tot_cost = -pay.Tot_cost;
                newpaymode.CancelOper = FS.FrameWork.Management.Connection.Operator.ID;
                newpaymode.CancelTime = this.packageMgr.GetDateTimeFromSysDateTime();
                newpaymode.Cancel_Flag = "1";            
                //{030CEF69-9D8D-4a12-8158-1AB14920B7D5}
                newpaymode.Oper = FS.FrameWork.Management.Connection.Operator.ID;
                newpaymode.OperTime = this.packageMgr.GetDateTimeFromSysDateTime();

                //押金转化为充值方式退费
                if (newpaymode.Mode_Code == "DE")
                {
                    newpaymode.Mode_Code = pay.Related_ModeCode;
                }

                //账户,优惠金额以及四舍五入以外的支付方式选择用指定退费方式进行退费
                if (newpaymode.Mode_Code != "RC" && 
                    newpaymode.Mode_Code != "YS" && 
                    newpaymode.Mode_Code != "DC" && 
                    QuitWay != "ADYYSFH" &&
                    newpaymode.Mode_Code != "SW")
                {
                    newpaymode.Mode_Code = QuitWay;
                }

                //插入负记录
                if (this.paymodeMgr.Insert(newpaymode) < 1)
                {
                    ErrInfo = this.invoiceMgr.Err;
                    return -1;
                }
            }

            //{F166B18B-62E3-4835-A729-4CA384F9ADEE}
            //if (cashCouponAmount > 0 || cashCouponAmount < 0)
            //{
            //    HISFC.BizProcess.Integrate.Account.CashCoupon cashCouponPrc = new FS.HISFC.BizProcess.Integrate.Account.CashCoupon();
            //    string errText1 = string.Empty;
            //    if (cashCouponPrc.CashCouponSave("TCTF", PatientInfo.PID.CardNO, invoiceInfo.InvoiceNO, cashCouponAmount, ref errText1) <= 0)
            //    {
            //        this.Err = "计算现金流积分出错!" + errText1;
            //        return -1;
            //    }

            //}

            #endregion

            //不存在重新收费的项目时 直接返回
            if (ReFeePackage == null || ReFeePackage.Count == 0)
            {
                return 1;
            }

            //获取发票号
            string invoiceNO = string.Empty;
            string realInvoiceNO = string.Empty;
            string errText = string.Empty;
            FS.HISFC.Models.Base.Employee oper = this.invoiceMgr.Operator as FS.HISFC.Models.Base.Employee;
            if (this.feeIntegrate.GetInvoiceNO(oper, "M", ref invoiceNO, ref realInvoiceNO, ref errText) == -1)
            {
                ErrInfo = errText;
                return -1;
            }
            newInvoiceNO = invoiceNO;
            string InvoiceSeq = this.invoiceMgr.GetNewInvoiceSeq();

            decimal reTot = (decimal)HSCostInfo["RETOT"];
            decimal reReal = (decimal)HSCostInfo["REREAL"];
            decimal reGift = (decimal)HSCostInfo["REGIFT"];
            decimal reEtc = (decimal)HSCostInfo["REETC"];


            #region 处理新的套餐主表和明细表

            int packageSeq = 1;
            foreach(FS.HISFC.Models.MedicalPackage.Fee.Package repackage in ReFeePackage)
            {
                //划价人，划价日期，收费人,收费日期,发票号，发票序列号
                FS.HISFC.Models.MedicalPackage.Fee.Package newpackage = repackage.Clone();
                newpackage.ID = this.packageMgr.GetNewClinicNO();
                newpackage.InvoiceNO = invoiceNO;
                newpackage.Invoiceseq = InvoiceSeq;
                newpackage.DelimitOper = oper.ID;
                newpackage.DelimitTime = this.packageMgr.GetDateTimeFromSysDateTime();
                newpackage.Oper = oper.ID;
                newpackage.OperTime = this.packageMgr.GetDateTimeFromSysDateTime();
                newpackage.Pay_Flag = "1";
                newpackage.Cancel_Flag = "0";
                newpackage.SequenceNO = packageSeq.ToString();

                if (this.packageMgr.Insert(newpackage) < 1)
                {
                    ErrInfo = packageMgr.Err;
                    return -1;
                }

                ArrayList al = HSReFeeDetail[repackage.ID] as ArrayList;
                if(al == null || al.Count == 0)
                {
                    ErrInfo = "查找明细项目失败！";
                    return -1;
                }

                int detailSeq = 1;
                foreach (FS.HISFC.Models.MedicalPackage.Fee.PackageDetail redetail in al)
                {
                    FS.HISFC.Models.MedicalPackage.Fee.PackageDetail newdetail = redetail.Clone();
                    newdetail.ID = newpackage.ID;
                    newdetail.Trans_Type = "1";
                    newdetail.PayFlag = "1";
                    newdetail.Cancel_Flag = "0";
                    newdetail.Oper = oper.ID;
                    newdetail.OperTime = this.packageMgr.GetDateTimeFromSysDateTime();
                    newdetail.InvoiceNO = invoiceNO;
                    newdetail.SequenceNO = detailSeq.ToString();

                    if (this.packageDetailMgr.Insert(newdetail) < 1)
                    {
                        ErrInfo = packageMgr.Err;
                        return -1;
                    }

                    detailSeq++;
                }
                packageSeq++;
            }

            #endregion

            #region 处理新的发票

            FS.HISFC.Models.MedicalPackage.Fee.Invoice invoice = new FS.HISFC.Models.MedicalPackage.Fee.Invoice();
            invoice.InvoiceNO = invoiceNO;
            invoice.Trans_Type = "1";
            invoice.Paykindcode = invoiceInfo.Paykindcode;
            invoice.Card_Level = invoiceInfo.Card_Level;
            invoice.Package_Cost = reTot;
            invoice.Real_Cost = reReal;
            invoice.Gift_cost = reGift;
            invoice.Etc_cost = reEtc;
            invoice.InvoiceSeq = InvoiceSeq;  
            invoice.PrintInvoiceNO = realInvoiceNO;
            invoice.Oper = FS.FrameWork.Management.Connection.Operator.ID;
            invoice.OperTime = this.invoiceMgr.GetDateTimeFromSysDateTime();
            invoice.Cancel_Flag = "0";

            if (this.invoiceMgr.Insert(invoice) < 1)
            {
                ErrInfo = this.invoiceMgr.Err;
                return -1;
            }

            #endregion

            #region 处理新的支付方式


            //{F166B18B-62E3-4835-A729-4CA384F9ADEE}
            //cashCouponAmount = 0.0m;

            //第一次，消耗账户和赠送账户
            int paySeq = 1;
            foreach (FS.HISFC.Models.MedicalPackage.Fee.PayMode pay in QuitPayModeList)
            {
                //全部扣费完毕，无需再扣
                if (reGift == 0 && reReal == 0)
                {
                    break;
                }

                if (pay.Mode_Code == "YS" || pay.Related_ModeCode == "YS" || pay.Mode_Code == "DC" || pay.Related_ModeCode == "DC")
                {
                    if (pay.AccountFlag == "0" && reReal > 0)
                    {
                        FS.HISFC.Models.MedicalPackage.Fee.PayMode newpay = new FS.HISFC.Models.MedicalPackage.Fee.PayMode();
                        newpay.InvoiceNO = invoiceNO;
                        newpay.Mode_Code = "YS";
                        newpay.Trans_Type = "1";
                        newpay.Cancel_Flag = "0";
                        newpay.SequenceNO = paySeq.ToString();
                        newpay.Account = pay.Account;
                        newpay.AccountFlag = pay.AccountFlag;
                        newpay.AccountType = pay.AccountType;
                        newpay.Tot_cost = newpay.Real_Cost = reReal > pay.Real_Cost ? pay.Real_Cost : reReal;
                        newpay.Oper = FS.FrameWork.Management.Connection.Operator.ID;
                        newpay.OperTime = this.packageMgr.GetDateTimeFromSysDateTime();

                        if (reReal > pay.Real_Cost)
                        {
                            reReal -= pay.Real_Cost;
                        }
                        else
                        {
                            reReal = 0;
                        }

                        
                        //用于返回提示
                        HSPayInfo["YS"] = (decimal)HSPayInfo["YS"] - newpay.Tot_cost;
                        if (accountPay.OutpatientPay(PatientInfo,
                                                     newpay.Account,
                                                     newpay.AccountType,
                                                     -newpay.Tot_cost,
                                                     0,
                                                     invoiceNO, PatientInfo,
                                                     FS.HISFC.Models.Account.PayWayTypes.M,
                                                     1) < 1)
                        {
                            //{2694417D-715F-4ef6-A664-1F92399DC325}
                            ErrInfo = "账户消费失败！" + accountPay.Err;
                            return -1;
                        }

                        if (this.paymodeMgr.Insert(newpay) < 1)
                        {
                            ErrInfo = this.paymodeMgr.Err;
                            return -1;
                        }



                        //{333D2AD8-DC4A-4c30-A14E-D6815AC858F9}
                        if (newpay.Mode_Code == "CO")
                        {
                            refeeCostCouponAmount += pay.Tot_cost;
                        }

                        if (cashCouponPayMode.Name.Contains(newpay.Mode_Code.ToString()) || (newpay.Mode_Code == "DE" && cashCouponPayMode.Name.Contains(newpay.Related_ModeCode.ToString())))
                        {
                            //cashCouponAmount += newpay.Tot_cost;
                            refeeOperateCouponAmount += pay.Tot_cost;
                        }

                        paySeq++;
                    }

                    if(pay.AccountFlag == "1" &&  reGift > 0)
                    {
                        FS.HISFC.Models.MedicalPackage.Fee.PayMode newpay = new FS.HISFC.Models.MedicalPackage.Fee.PayMode();
                        newpay.InvoiceNO = invoiceNO;
                        newpay.Mode_Code = "DC";
                        newpay.Trans_Type = "1";
                        newpay.Cancel_Flag = "0";
                        newpay.SequenceNO = paySeq.ToString();
                        newpay.Account = pay.Account;
                        newpay.AccountFlag = pay.AccountFlag;
                        newpay.AccountType = pay.AccountType;
                        newpay.Tot_cost = newpay.Real_Cost = reGift > pay.Real_Cost ? pay.Real_Cost : reGift;
                        newpay.Oper = FS.FrameWork.Management.Connection.Operator.ID;
                        newpay.OperTime = this.packageMgr.GetDateTimeFromSysDateTime();
                        if (reGift > pay.Real_Cost)
                        {
                            reGift -= pay.Real_Cost;
                        }
                        else
                        {
                            reGift = 0;
                        }


                        //用于返回提示
                        HSPayInfo["DC"] = (decimal)HSPayInfo["DC"] - newpay.Tot_cost;
                        if (accountPay.OutpatientPay(PatientInfo,
                                                     newpay.Account,
                                                     newpay.AccountType,
                                                     0,
                                                     -newpay.Tot_cost,
                                                     newpay.InvoiceNO, PatientInfo,
                                                     FS.HISFC.Models.Account.PayWayTypes.M,
                                                     1) < 1)
                        {
                            //{2694417D-715F-4ef6-A664-1F92399DC325}
                            ErrInfo = "账户消费失败！" + accountPay.Err;
                            return -1;
                        }

                        if (this.paymodeMgr.Insert(newpay) < 1)
                        {
                            ErrInfo = this.paymodeMgr.Err;
                            return -1;
                        }

                        //{333D2AD8-DC4A-4c30-A14E-D6815AC858F9}
                        if (newpay.Mode_Code == "CO")
                        {
                            refeeCostCouponAmount += pay.Tot_cost;
                        }

                        if (cashCouponPayMode.Name.Contains(newpay.Mode_Code.ToString()) || (newpay.Mode_Code == "DE" && cashCouponPayMode.Name.Contains(newpay.Related_ModeCode.ToString())))
                        {
                            //cashCouponAmount += newpay.Tot_cost;
                            refeeOperateCouponAmount += newpay.Tot_cost;
                        }

                        paySeq++;
                    }
                }
            }

            //到此步，重收的赠送账户应该全部收完，如果没有，则产生了错误
            if(reGift > 0)
            {
                ErrInfo = "费用扣除失败！";
                return -1;
            }

            //消耗其他支付
            foreach (FS.HISFC.Models.MedicalPackage.Fee.PayMode pay in QuitPayModeList)
            {
                if(reReal ==0)
                {
                    break;
                }

                if (pay.Mode_Code == "YS" || pay.Related_ModeCode == "YS" || pay.Mode_Code == "DC" || pay.Related_ModeCode == "DC")
                {
                    continue;
                }

                FS.HISFC.Models.MedicalPackage.Fee.PayMode newpay = new FS.HISFC.Models.MedicalPackage.Fee.PayMode();
                newpay.InvoiceNO = invoiceNO;
                newpay.Mode_Code = pay.Mode_Code;
                newpay.Trans_Type = "1";
                newpay.Cancel_Flag = "0";
                newpay.SequenceNO = paySeq.ToString();
                newpay.Account = pay.Account;
                newpay.AccountFlag = pay.AccountFlag;
                newpay.AccountType = pay.AccountType;
                newpay.InvoiceNO = invoiceNO;
                newpay.Tot_cost = newpay.Real_Cost = reReal > pay.Real_Cost ? pay.Real_Cost : reReal;
                newpay.Oper = FS.FrameWork.Management.Connection.Operator.ID;
                newpay.Related_ID = pay.Related_ID;
                newpay.Related_ModeCode = pay.Related_ModeCode;
                newpay.OperTime = this.packageMgr.GetDateTimeFromSysDateTime();
                if (newpay.Mode_Code == "RC")//{791F1D67-AE8A-4bc8-AC1F-2FB13204A862} 如果是套餐优惠则不扣除实付金额
                {
                    newpay.Tot_cost = newpay.Real_Cost = reEtc;
                }
                else
                {
                    if (reReal > pay.Real_Cost)
                    {
                        reReal -= pay.Real_Cost;
                    }
                    else
                    {
                        reReal = 0;
                    }
                }
                //用于返回提示
                if (newpay.Mode_Code == "DE")
                {
                    HSPayInfo[newpay.Related_ModeCode] = (decimal)HSPayInfo[newpay.Related_ModeCode] - newpay.Tot_cost;
                }
                else
                {
                    HSPayInfo[newpay.Mode_Code] = (decimal)HSPayInfo[newpay.Mode_Code] - newpay.Tot_cost;
                }

                if (this.paymodeMgr.Insert(newpay) < 1)
                {
                    ErrInfo = this.paymodeMgr.Err;
                    return -1;
                }

                //{333D2AD8-DC4A-4c30-A14E-D6815AC858F9}
                if (newpay.Mode_Code == "CO")
                {
                    refeeCostCouponAmount += pay.Tot_cost;
                }

                if (cashCouponPayMode.Name.Contains(newpay.Mode_Code.ToString()) || (newpay.Mode_Code == "DE" && cashCouponPayMode.Name.Contains(newpay.Related_ModeCode.ToString())))
                {
                    //cashCouponAmount += newpay.Tot_cost;
                    refeeOperateCouponAmount += newpay.Tot_cost;
                }

                //{146FEAC3-5B89-4966-AAA2-C8D86FACB35F}交易流水号bug修复
                paySeq++;
            }


            //{F166B18B-62E3-4835-A729-4CA384F9ADEE}
            //if (cashCouponAmount > 0 || cashCouponAmount < 0)
            //{
            //    HISFC.BizProcess.Integrate.Account.CashCoupon cashCouponPrc = new FS.HISFC.BizProcess.Integrate.Account.CashCoupon();
            //    string errText2 = string.Empty;
            //    if (cashCouponPrc.CashCouponSave("TCSF", PatientInfo.PID.CardNO, invoiceNO, cashCouponAmount, ref errText2) <= 0)
            //    {
            //        this.Err = "计算现金流积分出错!" + errText2;
            //        return -1;
            //    }

            //}

            #endregion

            #region 发票走号

            if (this.UseInvoiceNO(oper, "1", "M", 1, ref invoiceNO, ref realInvoiceNO, ref ErrInfo) < 0)
            {
                return -1;
            }

            if (this.InsertInvoiceExtend(invoiceNO, "M", realInvoiceNO, "00") < 0)
            {
                ErrInfo = this.Err;
                return -1;
            }

            #endregion

            return 1;

        }

        /// <summary>
        /// 重新收费
        /// </summary>
        /// <param name="PatientInfo">患者信息</param>
        /// <param name="ReeFee">收费明细</param>
        /// <param name="HSReFeeDetail">退费细项</param>
        /// <param name="PayInfoList">支付方式</param>
        /// <param name="ErrInfo">错误信息</param>
        /// <returns></returns>
        public int SaveRefee(FS.HISFC.Models.RADT.PatientInfo PatientInfo,
                             ArrayList ReeFee,           
                             Hashtable HSReFeeDetail,
                             ArrayList PayInfoList,
                             ref string ErrInfo)
        {
            return 1;
        }

        /// <summary>
        /// 发票号走号
        /// </summary>
        /// <param name="oper">收费员</param>
        /// <param name="invoiceStytle">取发票方式</param>
        /// <param name="invoiceType">发票类型R:挂号收据 C:门诊收据 P:预交收据 I:住院收据 A:门诊账户 M:套餐收据</param>
        /// <param name="invoiceCount"></param>
        /// <param name="invoiceNO"></param>
        /// <param name="realInvoiceNO"></param>
        /// <param name="errText"></param>
        /// <returns></returns>
        public int UseInvoiceNO(FS.HISFC.Models.Base.Employee oper, string invoiceStytle, string invoiceType, int invoiceCount, ref string invoiceNO, ref string realInvoiceNO, ref string errText)
        {
            string lastRealInvoice = "";
            int returnValue = 0;
            FS.HISFC.Models.Base.Const con = new FS.HISFC.Models.Base.Const();

            switch (invoiceStytle)
            {
                case "1":
                    //实际发票号码
                    for (int i = 0; i < invoiceCount; i++)
                    {
                        lastRealInvoice = this.GetNewInvoiceNO(invoiceType, oper);
                    }

                    if (string.IsNullOrEmpty(lastRealInvoice))
                    {
                        errText = inpatientManager.Err;
                        return -1;
                    }

                    if (lastRealInvoice != realInvoiceNO.PadLeft(lastRealInvoice.Length, '0'))
                    {
                        errText = "实际发票号有误!";
                        return -1;
                    }
                    //发票流水号
                    //更新发票流水号    
                    int len = invoiceNO.Length;
                    //获得发票除了后四位的字符串,代表发票的日期和收款员,格式为yymmddxx(年,月,日,操作员2位工号)
                    string orgInvoice = invoiceNO.Substring(0, len - 4);
                    //获得后四位发票序列号
                    string addInvoice = invoiceNO.Substring(len - 4, 4);

                    //获得下一张发票号
                    string nextInvoiceNO = orgInvoice + (FS.FrameWork.Function.NConvert.ToInt32(addInvoice) + 1).ToString().PadLeft(4, '0');

                    con.ID = oper.ID; // this.outpatientManager.Operator.ID;
                    con.Name = FS.FrameWork.Public.String.AddNumber(realInvoiceNO, 1);// (NConvert.ToInt32(realInvoiceNO) + 1).ToString();
                    con.Memo = nextInvoiceNO;

                    con.IsValid = true;
                    con.OperEnvironment.ID = oper.ID; // outpatientManager.Operator.ID;
                    con.OperEnvironment.OperTime = outpatientManager.GetDateTimeFromSysDateTime();
                    returnValue = managerIntegrate.UpdateConstant("INVOICE-" + invoiceType, con);
                    if (returnValue <= 0)
                    {
                        errText = "更新操作员初试发票失败!" + managerIntegrate.Err;

                        return -1;
                    }
                    break;
                case "2":
                    //实际发票号
                    for (int i = 0; i < invoiceCount; i++)
                    {
                        lastRealInvoice = this.GetNewInvoiceNO(invoiceType, oper);
                    }
                    con.ID = oper.ID;
                    //更新发票流水号                    
                    con.Name = FS.FrameWork.Public.String.AddNumber(realInvoiceNO, 1);//  (NConvert.ToInt32(realInvoiceNO) + 1).ToString();
                    con.Memo = FS.FrameWork.Public.String.AddNumber(invoiceNO, 1);//(NConvert.ToInt32(invoiceNO) + 1).ToString();
                    con.IsValid = true;
                    con.OperEnvironment.ID = oper.ID; // outpatientManager.Operator.ID;
                    con.OperEnvironment.OperTime = outpatientManager.GetDateTimeFromSysDateTime();
                    returnValue = managerIntegrate.UpdateConstant("INVOICE-" + invoiceType, con);
                    if (returnValue < 0)
                    {

                        errText = "更新操作员初试发票失败!" + managerIntegrate.Err;

                        return -1;
                    }
                    else if (returnValue == 0)
                    {
                        returnValue = managerIntegrate.InsertConstant("INVOICE-" + invoiceType, con);
                        if (returnValue < 0)
                        {
                            errText = "插入操作员初试发票失败!" + managerIntegrate.Err;
                            return -1;
                        }
                    }
                    break;
                case "3":
                case "4":
                default:
                    //实际发票号
                    //更新发票流水号   
                    con.ID = oper.ID;
                    con.Name = FS.FrameWork.Public.String.AddNumber(realInvoiceNO, 1);//  (NConvert.ToInt32(realInvoiceNO) + 1).ToString();
                    con.Memo = FS.FrameWork.Public.String.AddNumber(invoiceNO, 1);// (NConvert.ToInt32(invoiceNO) + 1).ToString();
                    con.IsValid = true;
                    con.OperEnvironment.ID = oper.ID; // outpatientManager.Operator.ID;
                    con.OperEnvironment.OperTime = outpatientManager.GetDateTimeFromSysDateTime();
                    returnValue = managerIntegrate.UpdateConstant("INVOICE-" + invoiceType, con);
                    if (returnValue < 0)
                    {

                        errText = "更新操作员初试发票失败!" + managerIntegrate.Err;

                        return -1;
                    }
                    else if (returnValue == 0)
                    {
                        returnValue = managerIntegrate.InsertConstant("INVOICE-" + invoiceType, con);
                        if (returnValue < 0)
                        {
                            errText = "插入操作员初试发票失败!" + managerIntegrate.Err;
                            return -1;
                        }
                    }
                    break;
            }

            return 1;
        }

        /// <summary>
        /// 取实际发票号--  结算时用，自动发票号+1
        /// </summary>
        /// <param name="invoiceType">发票类型R:挂号收据 C:门诊收据 P:预交收据 I:住院收据 A:门诊账户 M:套餐收据</param>
        /// <returns></returns>
        public string GetNewInvoiceNO(string invoiceType, FS.HISFC.Models.Base.Employee oper)
        {
            int leftQty = 0;//发票剩余数目
            int alarmQty = 0;//发票预警数目
            string finGroupID = string.Empty;//发票组代码
            string newInvoiceNO = string.Empty;//获得的发票号

            alarmQty = FS.FrameWork.Function.NConvert.ToInt32(controlManager.QueryControlerInfo("100002"));

            finGroupID = inpatientManager.GetFinGroupInfoByOperCode(oper.ID).ID;

            if (finGroupID == null || finGroupID == string.Empty)
            {
                finGroupID = " ";
            }

            FS.HISFC.BizLogic.Fee.FeeCodeStat feeCodeState = new FS.HISFC.BizLogic.Fee.FeeCodeStat();

            newInvoiceNO = inpatientManager.GetNewInvoiceNO(oper.ID, invoiceType, alarmQty, ref leftQty, finGroupID);

            if (newInvoiceNO == null || newInvoiceNO == string.Empty)
            {
                return null;
            }

            return newInvoiceNO;
        }

        /// <summary>
        /// 保存发票扩展表
        /// </summary>
        /// <param name="invoiceNO"></param>
        /// <param name="invoiceType"></param>
        /// <param name="realInvoiceNO"></param>
        /// <param name="invoiceHead"></param>
        /// <returns></returns>
        public int InsertInvoiceExtend(string invoiceNO, string invoiceType, string realInvoiceNO, string invoiceHead )
        {
            FS.HISFC.Models.Fee.InvoiceExtend invoiceExtend = new FS.HISFC.Models.Fee.InvoiceExtend();
            invoiceExtend.ID = invoiceNO;
            invoiceExtend.InvoiceType = invoiceType;
            if (realInvoiceNO.Length < 8)
            {
                invoiceExtend.RealInvoiceNo = realInvoiceNO;
            }
            else
            {
                invoiceExtend.RealInvoiceNo = realInvoiceNO.Substring(realInvoiceNO.Length - 8);//保存后8位
            }
            invoiceExtend.InvvoiceHead = invoiceHead;
            invoiceExtend.InvoiceState = "1";//有效
            int i = this.invoiceServiceManager.InsertInvoiceExtend(invoiceExtend);
            if (i <= 0)
            {
                this.Err = this.invoiceServiceManager.Err;
            }
            return i;
        }

        /// <summary>
        /// 划价保存
        /// </summary>
        /// <param name="PatientInfo">患者信息</param>
        /// <param name="checkedFee">需要或插入的条目</param>
        /// <param name="uncheckFee">需要删除的条目</param>
        /// <param name="ErrMessage">错误信息</param>
        /// <returns></returns>
        public int SavePrice(FS.HISFC.Models.RADT.PatientInfo PatientInfo, ArrayList checkedFee, ArrayList uncheckFee, ref string ErrMessage)
        {
            try
            {
                //用来存储划价列表
                Hashtable recipes = new Hashtable();

                foreach (FS.HISFC.Models.MedicalPackage.Fee.Package feepackage in checkedFee)
                {
                    if (recipes.Contains(feepackage.RecipeNO))
                    {
                        ArrayList al = recipes[feepackage.RecipeNO] as ArrayList;
                        feepackage.SequenceNO = (al.Count + 1).ToString() ;
                        al.Add(feepackage);
                    }
                    else
                    {
                        ArrayList al = new ArrayList();
                        al.Add(feepackage);
                        recipes.Add(feepackage.RecipeNO, al);
                        feepackage.SequenceNO = "1";
                    }

                    feepackage.Patient = PatientInfo;
                    feepackage.Trans_Type = "1";  
                    feepackage.Pay_Flag = "0"; //订单状态(0-未付款，1-已付款)
                    feepackage.Cancel_Flag = "0";  //有效标志0-有效，1-退款，2-半退
                    feepackage.Card_Level = PatientInfo.Pact.User01;
                    feepackage.DelimitOper = FS.FrameWork.Management.Connection.Operator.ID;
                    feepackage.DelimitTime = this.packageMgr.GetDateTimeFromSysDateTime();

                    //{2694417D-715F-4ef6-A664-1F92399DC325}
                    FS.HISFC.Models.MedicalPackage.Fee.Package oldpackage = this.packageMgr.QueryByID(feepackage.ID);

                    if (oldpackage == null || string.IsNullOrEmpty(oldpackage.ID))
                    {
                        feepackage.ID = this.packageMgr.GetNewClinicNO();
                        if (this.packageMgr.Insert(feepackage) < 0)
                        {
                            throw new Exception("插入单号为" + feepackage.RecipeNO + "的时候出现错误！");
                        }
                    }
                    else
                    {
                        if (oldpackage.Pay_Flag == "1")
                        {
                            throw new Exception("单号为" + feepackage.RecipeNO + "的划价单存在已经收费，请刷新！");
                        }

                        if (this.packageMgr.Update(feepackage) < 0)
                        {
                            throw new Exception("更新单号为" + feepackage.RecipeNO + "的时候出现错误！");
                        }
                    }
                }

                foreach (FS.HISFC.Models.MedicalPackage.Fee.Package delpackage in uncheckFee)
                {
                    if (!string.IsNullOrEmpty(delpackage.ID))
                    {
                        FS.HISFC.Models.MedicalPackage.Fee.Package oldpackage = this.packageMgr.QueryByID(delpackage.ID);
                        if (oldpackage.Pay_Flag == "1")
                        {
                            throw new Exception("单号为" + delpackage.RecipeNO + "的划价单存在已经收费的项目，请刷新！");
                        }
                        this.packageMgr.Delete(delpackage);
                    }
                }
            }
            catch(Exception ex)
            {
                ErrMessage = ex.Message;
                return -1;
            }
               
            return 1;
        }

        /// <summary>
        /// 缴纳押金
        /// </summary>
        /// <param name="PatientInfo"></param>
        /// <param name="Deposit"></param>
        /// <returns></returns>
        public int SaveDeposit(FS.HISFC.Models.RADT.PatientInfo PatientInfo, ArrayList DepositList,ref string ErrInfo)
        {
           

            try
            {

                if (PatientInfo == null || string.IsNullOrEmpty(PatientInfo.PID.CardNO))
                {
                    throw new Exception("患者信息为空!");
                }

                if (DepositList == null)
                {
                    throw new Exception("押金信息为空!");
                }

                foreach (FS.HISFC.Models.MedicalPackage.Fee.Deposit deposit in DepositList)
                {
                    deposit.Oper = FS.FrameWork.Management.Connection.Operator.ID;
                    deposit.OperTime = this.depositMgr.GetDateTimeFromSysDateTime();
                    deposit.Trans_Type = "1";
                    deposit.Cancel_Flag = "0";
                    deposit.DepositType = FS.HISFC.Models.MedicalPackage.Fee.DepositType.JYJ;
                    deposit.CardNO = PatientInfo.PID.CardNO;
                 
                    if(string.IsNullOrEmpty(deposit.ID))
                    {
                        deposit.ID = this.depositMgr.GetNewDepositNO();
                    }

                    if (deposit.Mode_Code == "YS")
                    {
                        if (accountPay.OutpatientPay(PatientInfo,
                                                     deposit.Account,
                                                     deposit.AccountType,
                                                     -deposit.Amount,
                                                     0,
                                                     deposit.ID, PatientInfo,
                                                     FS.HISFC.Models.Account.PayWayTypes.M,
                                                     1) < 1)
                        {
                            ErrInfo = "账户扣费失败！";
                            return -1;
                        }

                    }

                    if (deposit.Mode_Code == "DC")
                    {
                        if (accountPay.OutpatientPay(PatientInfo,
                                                     deposit.Account,
                                                     deposit.AccountType,
                                                     0,
                                                     -deposit.Amount,
                                                     deposit.ID, PatientInfo,
                                                     FS.HISFC.Models.Account.PayWayTypes.M,
                                                     1) < 1)
                        {
                            ErrInfo = "账户扣费失败！";
                            return -1;
                        }

                    }

                    if (this.depositMgr.Insert(deposit) <= 0)
                    {
                     
                       throw new Exception("保存失败：插入信息失败！");
                    }

                }
            }
            catch (Exception ex)
            {
                ErrInfo = ex.Message;
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// {2E41B9BF-6B67-4b56-BD54-A836CE09F52B}
        /// 查询套餐中可用的挂号费数目
        /// </summary>
        /// <param name="CardNO"></param>
        /// <returns></returns>
        public int GetPackageRegisterCount(string CardNO)
        {
            return this.packageMgr.GetPackageRegisterCount(CardNO);
        }

        //{56809DCA-CD5A-435e-86F0-93DE99227DF4}
        //{A777B7DF-AB62-4603-A0F6-B3643AD442F0}
        //public int QueryIsExistSIPackage(string CardNO)
        //{
        //    int rtn =  this.packageMgr.QueryIsExistSIPackage(CardNO);

        //    if (rtn < 0)
        //    {
        //        this.Err = this.packageMgr.Err;
        //    }

        //    return rtn;
        //}
    }
}
