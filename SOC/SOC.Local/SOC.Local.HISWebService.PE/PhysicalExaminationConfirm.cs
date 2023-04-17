using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections;
using System.Collections.Generic;
using FS.HISFC.Models.Account;
using FS.HISFC.Models.Fee.Outpatient;
using FS.HISFC.Models.Base;

namespace SOC.Local.HISWebService.PE
{
    public class PhysicalExaminationConfirm
    {
        private static Hashtable HSTranNo = new Hashtable();

        private static Hashtable HSIDNo = new Hashtable();

        public string feelistds = "";
        public string drugwindow = "";

        #region 账户管理对象
        //private AccCardRecMgr accCardRecMgr = new AccCardRecMgr();
        //private AccountCardMgr accCardMgr = new AccountCardMgr();
        //private AccountMgr accMgr = new AccountMgr();
        //private AccRecordMgr accRecMgr = new AccRecordMgr();
        //private AccPrepayMgr prepayMgr = new AccPrepayMgr();

        FS.FrameWork.Management.Transaction ttt = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
        #endregion

        /// <summary>
        /// 费用业务层
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();

        /// <summary>
        /// 药品业务层
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Pharmacy pharmacyIntegrate = new FS.HISFC.BizProcess.Integrate.Pharmacy();

        /// <summary>
        /// 非药品业务层
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.Item undrugManager = new FS.HISFC.BizLogic.Fee.Item();

        /// <summary>
        /// 门诊费用业务层
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.Outpatient outpatientManager = new FS.HISFC.BizLogic.Fee.Outpatient();

        /// <summary>
        /// 管理业务层
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// 控制参数业务层
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        /// <summary>
        /// 药品常数业务层
        /// </summary>
        FS.HISFC.BizLogic.Pharmacy.Constant phaConsMgr = new FS.HISFC.BizLogic.Pharmacy.Constant();

        /// <summary>
        /// 医疗组业务层
        /// </summary>
        //FS.HISFC.Management.Manager.EmpConManager empComMgr = new FS.HISFC.Management.Manager.EmpConManager();

        /// <summary>
        /// 非药品组合项目业务层
        /// </summary>
        private FS.HISFC.BizLogic.Fee.UndrugPackAge undrugPackAgeManager = new FS.HISFC.BizLogic.Fee.UndrugPackAge();

        /// <summary>
        /// 医嘱业务层
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Order orderIntegrate = new FS.HISFC.BizProcess.Integrate.Order();

        /// <summary>
        /// 合同单位比例管理业务层
        /// </summary>
        private FS.HISFC.BizLogic.Fee.PactUnitItemRate pactUnitItemRateManager = new FS.HISFC.BizLogic.Fee.PactUnitItemRate();
        /// 排班管理类
        /// </summary>
        private FS.HISFC.BizLogic.Registration.Schema schMgr = new FS.HISFC.BizLogic.Registration.Schema();

        #region 管理类
        /// <summary>
        /// 常数管理类
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager conMgr = new FS.HISFC.BizProcess.Integrate.Manager();
        /// <summary>
        /// 挂号员权限类
        /// </summary>
        private FS.HISFC.BizLogic.Registration.Permission permissMgr = new FS.HISFC.BizLogic.Registration.Permission();
        /// <summary>
        /// 参数控制类
        /// </summary>
        private FS.FrameWork.Management.ControlParam ctlMgr = new FS.FrameWork.Management.ControlParam();
        /// <summary>
        /// 排班管理类
        /// </summary>
        private FS.HISFC.BizLogic.Registration.Schema SchemaMgr = new FS.HISFC.BizLogic.Registration.Schema();
        /// <summary>
        /// 患者管理类
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.RADT patientMgr = new FS.HISFC.BizProcess.Integrate.RADT();
        /// <summary>
        /// 挂号管理类
        /// </summary>
        private FS.SOC.HISFC.PE.BizLogic.Register regMgr = new FS.SOC.HISFC.PE.BizLogic.Register();
        /// <summary>
        /// 合同单位管理类
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Fee feeMgr = new FS.HISFC.BizProcess.Integrate.Fee();
        /// <summary>
        /// 挂号级别管理类
        /// </summary>
        private FS.HISFC.BizLogic.Registration.RegLevel RegLevelMgr = new FS.HISFC.BizLogic.Registration.RegLevel();
        /// <summary>
        /// 预约管理类
        /// </summary>
        private FS.HISFC.BizLogic.Registration.Booking bookingMgr = new FS.HISFC.BizLogic.Registration.Booking();
        /// <summary>
        /// 挂号费管理类
        /// </summary>
        private FS.HISFC.BizLogic.Registration.RegLvlFee regFeeMgr = new FS.HISFC.BizLogic.Registration.RegLvlFee();

        /// <summary>
        /// Acount业务层
        /// </summary>
        private FS.SOC.Local.HISWebService.PE.PEChargeService_Db accountManager = new FS.SOC.Local.HISWebService.PE.PEChargeService_Db();
        /// <summary>
        /// 病历信息
        /// </summary>
        FS.HISFC.BizLogic.HealthRecord.Case.CaseInfo caseInfoMgr = new FS.HISFC.BizLogic.HealthRecord.Case.CaseInfo();
        FS.FrameWork.Public.ObjectHelper docHelper = new FS.FrameWork.Public.ObjectHelper();
        ArrayList tempDoc = new ArrayList();

        /// <summary>
        /// 挂号实体
        /// </summary>
        private FS.HISFC.Models.Registration.Register regObj;
        private FS.HISFC.Models.Registration.Schema schema;
        #endregion

        #region 账户
        private AccountCard accCard = new AccountCard();
        private FS.HISFC.Models.Account.Account acc = new FS.HISFC.Models.Account.Account();
        private AccountCardRecord accCardRecord = new AccountCardRecord();
        private FS.HISFC.Models.Account.PrePay p = new PrePay();
        private AccountRecord accRec = new AccountRecord();
        //收费请求是否来自终端科室
        private bool isFromExecDept = false;
        private FS.HISFC.Models.RADT.PatientInfo patientInfo = new PatientAccount();

        public FS.HISFC.Models.RADT.PatientInfo PatientInfo
        {
            get
            {
                return patientInfo;
            }
            set
            {
                patientInfo = value;
            }
        }
        #endregion

        #region 接口属性
        private string icCardNo = string.Empty;
        private string patientName = string.Empty;
        private string gender = string.Empty;
        private DateTime birthday = DateTime.MinValue;
        private string age = "0";
        private string idNo = string.Empty;
        private string operId = string.Empty;
        private DateTime operTime = DateTime.Now;
        private string address = string.Empty;
        private string secNo = string.Empty;
        private string payMode = "CA";
        private string patientId = "";
        private string telNo = "";
        private string mobilePhone = "";
        private string bankNo = "";
        private string firstPwd = "";
     

        /// <summary>
        /// 防重流水号
        /// </summary>
        private string tranno = "";

        /// <summary>
        /// 防重流水号
        /// </summary>
        public string TranNo
        {
            set
            {
                tranno = value;
            }
        }

        //交易产生的发票号
        private string invo = "";

        /// <summary>
        /// 卡号
        /// </summary>
        public string IcCardNo
        {
            set
            {
                icCardNo = value;
            }
        }

        /// <summary>
        /// 病人姓名
        /// </summary>
        public string PatName
        {
            set
            {
                patientName = value;
            }
        }

        /// <summary>
        /// 性别
        /// </summary>
        public string Gender
        {
            set
            {
                gender = value;
            }
        }

        /// <summary>
        /// 生日
        /// </summary>
        public DateTime Birthday
        {
            set
            {
                birthday = value;
            }
        }

        /// <summary>
        /// 年龄
        /// </summary>
        public string Age
        {
            set
            {
                age = value;
            }
        }

        /// <summary>
        /// 身份证号
        /// </summary>
        public string IdNo
        {
            set
            {
                idNo = value;
            }
        }

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime OperTime
        {
            set
            {
                operTime = value;
            }
        }

        /// <summary>
        /// 户口地址
        /// </summary>
        public string Adress
        {
            set
            {
                address = value;
            }
        }

        /// <summary>
        /// 操作员ID
        /// </summary>
        public string OperID
        {
            set
            {
                operId = value;
            }
        }

        /// <summary>
        /// 安全号
        /// </summary>
        public string SecNo
        {
            get
            {
                return secNo;
            }
            set
            {
                secNo = value;
            }
        }

        /// <summary>
        /// 支付方式
        /// </summary>
        public string PayMode
        {
            get
            {
                return payMode;
            }
            set
            {
                payMode = value;
            }
        }

        /// <summary>
        /// 发票号
        /// </summary>
        public string Invo
        {
            get
            {
                return invo;
            }
        }

        /// <summary>
        /// 病人唯一主索引
        /// </summary>
        public string PatientId
        {
            get
            {
                return patientId;
            }
            set
            {
                patientId = value;
            }
        }

        /// <summary>
        /// 电话号码
        /// </summary>
        public string Tel
        {
            get
            {
                return telNo;
            }
            set
            {
                telNo = value;
            }

        }

        /// <summary>
        /// 电话号码
        /// </summary>
        public string MobilePhone
        {
            get
            {
                return mobilePhone;
            }
            set
            {
                mobilePhone = value;
            }
        }

        /// <summary>
        /// 是否来源于终端科室
        /// </summary>
        public bool FromExecDept
        {
            set
            {
                isFromExecDept = value;
            }
        }

        /// <summary>
        /// ATM充值银行卡号
        /// </summary>
        public string BankNo
        {
            get
            {
                return bankNo;
            }
            set
            {
                bankNo = value;
            }
        }

        #endregion

        private string errMsg = "";

        private string regSeq = "";
        //如果挂号是预约的专科专家门诊必须指定号
        public string RegSeq
        {
            set
            {
                regSeq = value;
            }
        }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrMsg
        {
            get
            {
                return errMsg;
            }
        }

        public FS.HISFC.Models.Registration.Register RegObj
        {
            get
            {
                return regObj;
            }
        }

        public PhysicalExaminationConfirm()
        {
        }

        public void SetSql()
        {

        }

        /// <summary>
        /// 根据卡号获取账户信息 对应接口AccSearch
        /// </summary>
        /// <param name="icCardno"></param>
        /// <returns></returns>
        public List<FS.HISFC.Models.Account.Account> GetAccByIDNo(string icCardno)
        {
            return null;// accMgr.GetAccByIcNo(icCardno);
        }

        /// <summary>
        /// 根据卡号获取卡信息
        /// </summary>
        /// <param name="icCardno"></param>
        /// <returns></returns>
        public List<FS.HISFC.Models.Account.AccountCard> GetRecByIcState(string icCardno)
        {
            return null; //accCardMgr.GetRecByIcState(icCardno,"1");
        }

        public void RemoveHSTranNo()
        {
            try
            {
                HSTranNo.Remove(tranno);
            }
            catch
            { }
        }

        /// <summary>
        /// ATM办卡首先获取PatientId
        /// </summary>
        /// <returns></returns>
        public int GetPatientId()
        {
            //获取患者唯一号
            string printID = accountManager.GetPrintCardID();
            if (printID == "" || printID == null)
            {
                return -1;
                errMsg = "获取唯一主索引失败！" + accountManager.Err;
            }
            patientId = printID;
            return 1;
        }

        /// <summary>
        /// 根据身份证号查询是否存在病人的病历号
        /// </summary>
        /// <param name="idNo">身份证号</param>
        /// <returns>-2 数据库异常, -1 不存在, null 不存在, 其它存在</returns>
        public string GetPatInfoByIdno(string idNo)
        {
            //try
            //{
            //    string r = accMgr.GetCardNoByIdNo(idNo);
            //    return r;
            //}
            //catch
            //{
            //    return "-2";
            //}
            return "";
        }

        /// <summary>
        /// 根据身份证号查询是否存在病人的病历号
        /// </summary>
        /// <param name="idNo">身份证号</param>
        /// <returns>-2 数据库异常, -1 不存在, null 不存在, 其它存在</returns>
        public string GetPatInfoByIdno(string idNo, ref string patientid)
        {
            //try
            //{
            //    string r = accMgr.GetCardNoByIdNo(idNo);

            //    FS.HISFC.BizLogic.RADT.InPatient patMgr = new FS.HISFC.BizLogic.RADT.InPatient();
            //    FS.HISFC.Models.RADT.PatientInfo tempinfo = patMgr.QueryComPatientInfobyIDNO(idNo);

            //    if (tempinfo == null )
            //    {
            //        return "-1";
            //    }
            //    else
            //    {
            //        patientid = tempinfo.PatientId;
            //    }

            //    return r;
            //}
            //catch
            //{
            //    return "-2";
            //}
            return "-1";
        }

        /// <summary>
        /// 数据库测试连接
        /// </summary>
        /// <returns>1成功，-1 失败</returns>
        public int ConnectTest()
        {
            //try
            //{
            //    string s = accMgr.ExecSqlReturnOne(" select '1'from DUAL");
            //    if (s == "1")
            //    {
            //        return 1;
            //    }
            //    else
            //    {
            //        errMsg = accMgr.Err;
            //        return -1;
            //    }
            //}
            //catch
            //{
            //    errMsg = accMgr.Err;
            //    return -1;
            //}
            return -1;
        }

        /// <summary>
        /// 根据卡号获取病人信息
        /// </summary>
        public FS.HISFC.Models.RADT.PatientInfo GetPatInfo(string icCardNo)
        {
            //FS.HISFC.BizLogic.RADT.InPatient patMgr = new FS.HISFC.BizLogic.RADT.InPatient();
            FS.HISFC.Models.RADT.PatientInfo patInfo = new FS.HISFC.Models.RADT.PatientInfo();
            //patInfo = patMgr.QueryComPatientInfoByIcCardNo(icCardNo.ToUpper());
            return patInfo;
        }

        /// <summary>
        /// 获取病人挂号信息
        /// </summary>
        /// <param name="icCardNo"></param>
        /// <param name="dsRecipe"></param>
        public void GetRegInfo(string icCardNo, ref DataSet dsRecipe)
        {
            try
            {
                //获得当前系统时间

                this.patientInfo = this.GetPatInfo(icCardNo);

                int validDays = 0;
                FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

                validDays = controlParamIntegrate.GetControlParam<int>(FS.HISFC.BizProcess.Integrate.Const.VALID_REG_DAYS, false, 10000);

                if (validDays == 0)
                {
                    validDays = 10000;//如果没有维护，那么默认挂号一直有效;
                }
                DateTime nowTime = this.outpatientManager.GetDateTimeFromSysDateTime();

                string tmpsql = @"
                  select distinct
                  CLINIC_CODE, --  门诊号
                  CARD_NO, --  病历卡号
                  REG_DATE, --  挂号日期
                  d1.DEPT_NAME  REG_DPCD, --  挂号科室
                  e.EMPL_NAME DOCT_CODE, --  开方医师
                  d.DEPT_NAME DOCT_DEPT, --  开方医师所在科室
                  PAY_FLAG, --  收费标志，1未收费，2收费
                  CANCEL_FLAG, --  作废标志,1未作废,2作废  
                  RECIPE_SEQ  --处方号 
                  from fin_opb_feedetail m left join COM_DEPARTMENT d on m.DOCT_DEPT= d.DEPT_CODE
                  left join COM_DEPARTMENT d1 on m.REG_DPCD=d1.DEPT_CODE
                  left join COM_EMPLOYEE e on e.EMPL_CODE = m.DOCT_CODE
                  where    pay_flag = '0'
                  AND CLINIC_CODE IN
	                  (
              	         SELECT clinic_code
                         FROM fin_opr_register  
                         WHERE card_no='{0}'
                         AND reg_date>=to_date('{1}','yyyy-mm-dd HH24:mi:ss')
                         AND valid_flag='1'
              	    )     
                  and CANCEL_FLAG='1'                 
              ";
                string cardNo = patientInfo.PID.CardNO;
                DateTime dt = outpatientManager.GetDateTimeFromSysDateTime().AddDays(-3);
                tmpsql = string.Format(tmpsql, cardNo, dt.ToString());
                outpatientManager.ExecQuery(tmpsql, ref dsRecipe);
            }
            catch
            {
                errMsg = outpatientManager.Err;
            }
        }

        public void GetRecipeList(string icCardNo,string clinic_no ,ref DataSet dsRecipe)
        {
            try
            {
                //获得当前系统时间

                this.patientInfo = this.GetPatInfo(icCardNo);

                DateTime nowTime = this.outpatientManager.GetDateTimeFromSysDateTime();

                string tmpsql = @"
                 select 
                  
                     sum(m.OWN_COST+m.PAY_COST+m.PUB_COST)  SumFee,
                                
                  CARD_NO, --  病历卡号
                  REG_DATE, --  挂号日期
                  d1.DEPT_NAME  REG_DPCD, --  挂号科室
                  e.EMPL_NAME DOCT_CODE, --  开方医师
                  d.DEPT_NAME DOCT_DEPT, --  开方医师所在科室
                 m.OPER_CODE,
                 m.OPER_DATE,
                m.RECIPE_SEQ,
                   CLINIC_CODE --  门诊号
                  from fin_opb_feedetail m left join COM_DEPARTMENT d on m.DOCT_DEPT= d.DEPT_CODE
                  left join COM_DEPARTMENT d1 on m.REG_DPCD=d1.DEPT_CODE
                  left join COM_EMPLOYEE e on e.EMPL_CODE = m.DOCT_CODE
                  where    pay_flag = '0'
                  and CANCEL_FLAG='1'
                  and CLINIC_CODE='{0}'
                 group by RECIPE_SEQ,CARD_NO,
                  REG_DATE, --  挂号日期
                  d1.DEPT_NAME , --  挂号科室
                  e.EMPL_NAME , --  开方医师
                  d.DEPT_NAME , --  开方医师所在科室
                 m.OPER_CODE,
                 m.OPER_DATE,
                   CLINIC_CODE
                                 
              ";
                string cardNo = patientInfo.PID.CardNO;

                tmpsql = string.Format(tmpsql, clinic_no);
                outpatientManager.ExecQuery(tmpsql, ref dsRecipe);
            }
            catch
            {
                errMsg = outpatientManager.Err;
            }
        }

        /// <summary>
        /// 给缴费用
        /// </summary>
        /// <param name="icCardNo"></param>
        /// <param name="dsRecipe"></param>
        public void GetRegInfo2(string icCardNo, ref DataSet dsRecipe)
        {
            try
            { 
                this.patientInfo = this.GetPatInfo(icCardNo);

                FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

                //FS.FrameWork.Management.Transaction trans = new FS.FrameWork.Management.Transaction(this.outpatientManager.Connection);
                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                this.outpatientManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                controlParamIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                //获得当前系统时间
                int validDays = 0;

                validDays = controlParamIntegrate.GetControlParam<int>(FS.HISFC.BizProcess.Integrate.Const.VALID_REG_DAYS, false, 10000);

                if (validDays == 0)
                {
                    validDays = 10000;//如果没有维护，那么默认挂号一直有效;
                }
                DateTime nowTime = this.outpatientManager.GetDateTimeFromSysDateTime();

                string tmpsql = @"
                   select  
                 CLINIC_CODE, --  门诊号
                  CARD_NO, --  病历卡号
                  REG_DATE, --  挂号日期
                  d1.DEPT_NAME  REG_DPCD, --  挂号科室
                  e.EMPL_NAME DOCT_CODE, --  开方医师
                  d.DEPT_NAME DOCT_DEPT, --  开方医师所在科室
                  PAY_FLAG, --  收费标志，1未收费，2收费
                  CANCEL_FLAG,--  作废标志,1未作废,2作废  
                   sum(m.OWN_COST+m.PAY_COST+m.PUB_COST) Total_Cost, -- 总金额
                   count(distinct m.RECIPE_SEQ) RECIPE_Count -- 处方数量
                  from fin_opb_feedetail m left join COM_DEPARTMENT d on m.DOCT_DEPT= d.DEPT_CODE
                  left join COM_DEPARTMENT d1 on m.REG_DPCD=d1.DEPT_CODE
                  left join COM_EMPLOYEE e on e.EMPL_CODE = m.DOCT_CODE
                  where    pay_flag = '0'
                  AND CLINIC_CODE IN
	                  (
              	         SELECT clinic_code
                         FROM fin_opr_register  
                         WHERE card_no='{0}'
                         AND reg_date>=to_date('{1}','yyyy-mm-dd HH24:mi:ss')
                         AND valid_flag='1'
              	    )     
                  and CANCEL_FLAG='1'
                  group by  CLINIC_CODE,
                  CARD_NO,
                  REG_DATE, 
                  d1.DEPT_NAME , 
                  e.EMPL_NAME , 
                  d.DEPT_NAME , 
                  PAY_FLAG, 
                  CANCEL_FLAG 
              ";
                string cardNo = patientInfo.PID.CardNO;
                DateTime dt = outpatientManager.GetDateTimeFromSysDateTime().AddDays(-3);
                tmpsql = string.Format(tmpsql, cardNo, dt.ToString());
                outpatientManager.ExecQuery(tmpsql, ref dsRecipe);
                FS.FrameWork.Management.PublicTrans.RollBack();
            }
            catch
            {
                errMsg = outpatientManager.Err;
            }
        }

        /// <summary>
        /// 判断最后收费项目是否停用等
        /// </summary>
        /// <param name="feeItemLists">要判断的费用明细</param>
        /// <returns>成功 true 失败 false</returns>
        private bool IsItemValid(ArrayList feeItemLists)
        {

            try
            {
                bool isJudgeValid = this.controlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.Const.STOP_ITEM_WARNNING, false, false);
                string ownDiagFeeCode = controlParamIntegrate.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.Const.AUTO_PUB_FEE_DIAG_FEE_CODE, true, string.Empty);


                //已经收取诊金时是否继续 
                //if (!isJudgeValid) //如果不需要判断，默认都没有停用
                //{
                //    return true;
                //}

                for (int i = 0; i < feeItemLists.Count; i++)
                {

                    FeeItemList f = feeItemLists[0] as FeeItemList;
                    decimal g = f.Item.PackQty;
                    if (f.Item.ItemType==EnumItemType.Drug)
                    {
                        FS.HISFC.Models.Pharmacy.Item drugItem = this.pharmacyIntegrate.GetItem(f.Item.ID);
                        if (drugItem == null)
                        {
                            //MessageBox.Show(Language.Msg("查询药品项目出错!") + pharmacyIntegrate.Err);
                            errMsg = "查询药品项目出错!" + pharmacyIntegrate.Err;
                            return false;
                        }
                        if (drugItem.IsStop)
                        {
                            errMsg = "[" + drugItem.Name + "]已经停用!请验证再收费!";
                            return false;
                        }
                        f.Item.Price = drugItem.Price;
                        f.Item.IsNeedConfirm = drugItem.IsNeedConfirm;
                    }
                    else
                    {
                        FS.HISFC.Models.Fee.Item.Undrug undrugItem = this.undrugManager.GetUndrugByCode(f.Item.ID);
                        if (undrugItem == null)
                        {
                            errMsg = "查询非药品项目出错!" + undrugManager.Err;
                            return false;
                        }
                        if (undrugItem.ValidState != "1")//停用
                        {
                            errMsg = "[" + undrugItem.Name + "]已经停用或废弃，请验证再收费!";
                            return false;
                        }
                        f.Item.Price = undrugItem.Price;
                        f.Item.IsNeedConfirm = undrugItem.IsNeedConfirm;
                    }
                }
            }
            catch (Exception ex)
            {
                this.errMsg = ex.Message;
                return false;
            }


            return true;
        }

        private string GetFeeDetSql()
        {
            return @" select               
              CARD_NO, --  病历号
              REG_DATE, --  挂号日期
              REG_DPCD, --  挂号科室
              (select EMPL_NAME from COM_EMPLOYEE e where e.EMPL_CODE = DOCT_CODE fetch first 1 rows only) as DOCT_CODE, --  开方医师
              DOCT_DEPT, --  开方医师所在科室
              ITEM_CODE, --  项目代码
              ITEM_NAME, --  项目名称
              DRUG_FLAG, --  1药品/2非要
              SPECS,  --  规格
              SELF_MADE, --  自制药标志
              DRUG_QUALITY, --  药品性质，麻药，普药
              DOSE_MODEL_CODE,--  剂型
              d.name, --  最小费用代码
              UNIT_PRICE, --  单价
              QTY,  --  数量
              DAYS,  --  草药的付数，其他药品为1
              FREQUENCY_CODE, --  频次代码
              USAGE_CODE, --  用法代码
              USE_NAME, --  用法名称
              INJECT_NUMBER, --  院内注射次数
              EMC_FLAG, --  加急标记：1普通/2加急
              LAB_TYPE, --  样本类型
              DOSE_ONCE, --  每次用量
              DOSE_UNIT, --  每次用量单位
              BASE_DOSE, --  基本剂量
              PACK_QTY, --  包装数量
              PRICE_UNIT, --  计价单位
              PUB_COST, --  可报效金额
              PAY_COST, --  自付金额
              OWN_COST, --  现金金额
              EXEC_DPCD, --  执行科室代码
              EXEC_DPNM, --  执行科室名称
              ITEM_GRADE, --  项目等级，1甲类，2乙类，3丙类
              MAIN_DRUG, --  主药标志
              f.OPER_CODE, --  划价人
              f.OPER_DATE, --  划价时间
              PAY_FLAG, --  收费标志，1未收费，2收费
              CANCEL_FLAG, --  作废标志,1未作废,2作废
              FEE_CPCD, --  收费员代码
              FEE_DATE, --  收费日期
              INVOICE_NO, --  票据号
              INVO_CODE, --  发票科目代码
              INVO_SEQUENCE, --  发票内流水号
              CONFIRM_FLAG, --  1未确认/2确认
              CONFIRM_CODE, --  确认人
              CONFIRM_DEPT, --  确认科室
              CONFIRM_DATE, --  确认时间
              INVOICE_SEQ,
              NOBACK_NUM,--      NUMBER(7,2)   Y                可退数量                               
              CONFIRM_NUM ,
              RECIPE_SEQ,	-- 处方号,
              f.INVOICE_NO, --发票号
              f.PACKAGE_NAME,-- 组套名称
              f.RECIPE_NO
             from fin_opb_feedetail f left join com_dictionary d on f.FEE_CODE = d.code ";
        }

        private string GetBillSql()
        {
            return @" select               
              CARD_NO, --  病历号
              REG_DATE, --  挂号日期
              REG_DPCD, --  挂号科室
              (select EMPL_NAME from COM_EMPLOYEE e where e.EMPL_CODE = DOCT_CODE fetch first 1 rows only) as DOCT_CODE, --  开方医师
              DOCT_DEPT, --  开方医师所在科室
              ITEM_CODE, --  项目代码
              ITEM_NAME, --  项目名称
              DRUG_FLAG, --  1药品/2非要
              SPECS,  --  规格
              SELF_MADE, --  自制药标志
              DRUG_QUALITY, --  药品性质，麻药，普药
              DOSE_MODEL_CODE,--  剂型
              '' as NAME, --  最小费用代码
              UNIT_PRICE, --  单价
              QTY,  --  数量
              DAYS,  --  草药的付数，其他药品为1
              FREQUENCY_CODE, --  频次代码
              USAGE_CODE, --  用法代码
              USE_NAME, --  用法名称
              INJECT_NUMBER, --  院内注射次数
              EMC_FLAG, --  加急标记：1普通/2加急
              LAB_TYPE, --  样本类型
              DOSE_ONCE, --  每次用量
              DOSE_UNIT, --  每次用量单位
              BASE_DOSE, --  基本剂量
              PACK_QTY, --  包装数量
              PRICE_UNIT, --  计价单位
              PUB_COST, --  可报效金额
              PAY_COST, --  自付金额
              OWN_COST, --  现金金额
              EXEC_DPCD, --  执行科室代码
              EXEC_DPNM, --  执行科室名称
              ITEM_GRADE, --  项目等级，1甲类，2乙类，3丙类
              MAIN_DRUG, --  主药标志
              f.OPER_CODE, --  划价人
              f.OPER_DATE, --  划价时间
              PAY_FLAG, --  收费标志，1未收费，2收费
              CANCEL_FLAG, --  作废标志,1未作废,2作废
              FEE_CPCD, --  收费员代码
              FEE_DATE, --  收费日期
              INVOICE_NO, --  票据号
              INVO_CODE, --  发票科目代码
              INVO_SEQUENCE, --  发票内流水号
              CONFIRM_FLAG, --  1未确认/2确认
              CONFIRM_CODE, --  确认人
              CONFIRM_DEPT, --  确认科室
              CONFIRM_DATE, --  确认时间
              INVOICE_SEQ,
              NOBACK_NUM,--      NUMBER(7,2)   Y                可退数量                               
              CONFIRM_NUM ,
              RECIPE_SEQ,	-- 处方号,            
              f.INVOICE_NO, --发票号
              f.PACKAGE_NAME, -- 组套名称,
                
              (
              --取划价科室,如果
               select a.ext2 from FIN_COM_ADDITEM a
               where a.EXT1='AC' and a.ISVALID = '1' and a.ITEMCODE = f.ITEM_CODE
               and f.OPER_CODE not in
               (
                select value(e.EMPL_CODE,'') from FIN_COM_ADDITEM a
              left join COM_EMPLOYEE e on e.DEPT_CODE = a.DEPT_CODE
              where a.EXT1='AC' and a.ISVALID = '1' and a.ITEMCODE = f.ITEM_CODE
               )
              ) ChargeDept,
               f.RECIPE_NO

             from fin_opb_feedetail f";
        }

        /// <summary>
        /// 根据处方号获取明细信息
        /// </summary>
        /// <param name="cardNo">病历号</param>
        /// <param name="recNo">处方号</param>
        /// <param name="payFlag">缴费标识</param>
        /// <returns></returns>
        public void GetBill(string cardNo, string clinic_code, ref DataSet ds)
        {
            string tmpSql = this.GetBillSql() + " where CARD_NO='{0}' and PAY_FLAG='0' and  CLINIC_CODE='{1}' and  f.CANCEL_FLAG='1' and f.TRANS_TYPE='1'";

            tmpSql = string.Format(tmpSql, cardNo, clinic_code);
            //accMgr.ExecQuery(tmpSql, ref ds);
        }

        /// <summary>
        /// 根据处方号获取明细信息
        /// </summary>
        /// <param name="cardNo">病历号</param>
        /// <param name="recNo">处方号</param>
        /// <param name="payFlag">缴费标识</param>
        /// <returns></returns>
        public void GetBill(string cardNo, string recNo, string clinic_code, ref DataSet ds)
        {
            string tmpSql = this.GetBillSql() + " where CARD_NO='{0}' and PAY_FLAG='0' and RECIPE_SEQ in ('{1}')  and  CLINIC_CODE='{2}' and  f.CANCEL_FLAG='1' and f.TRANS_TYPE='1'";

            tmpSql = string.Format(tmpSql, cardNo, recNo, clinic_code);
            //accMgr.ExecQuery(tmpSql, ref ds);
        }

        /// <summary>
        /// 根据处方号 获取已经交费了的明细
        /// </summary>
        /// <param name="cardNo">病历号</param>
        /// <param name="recNo">处方号</param>
        /// <param name="ds"></param>
        public void GetChargedBill(string cardNo, string recNo, ref DataSet ds)
        {
            string tmpSql = this.GetFeeDetSql() + " where CARD_NO='{0}' and RECIPE_SEQ in ('{1}') and PAY_FLAG='{2}' and d.type = 'MINFEE'";

            tmpSql = string.Format(tmpSql, cardNo, recNo, "1");
            //accMgr.ExecQuery(tmpSql, ref ds);

        }

        public void TimeRang(ref DataSet ds)
        {
            string sql = @"select NOON_CODE,NOON_NAME,BEGIN_TIME,END_TIME from FIN_OPR_NOON";
            //accMgr.ExecQuery(sql, ref ds);
        }

        public void DocList(ref DataSet ds, string noonCode)
        {
            string sql = @"select ch.* , 
             (select r.REG_FEE + r.CHCK_FEE + r.DIAG_FEE + r.OTH_FEE
             from FIN_OPR_REGFEEONPACT r
             where r.PACT_CODE = '1' and r.REGLEVL_CODE = ch.REGLEVL_CODE
             fetch first 1 rows only) diagFee
             from FIN_OPR_SCHEMA ch 
             where  ch.NOON_CODE = '{0}' and ONLINE_LMT>ch.ONLINE_REGED and ch.VALID_FLAG = '1' and date(ch.BEGIN_TIME) >= date(current timestamp)";
            sql = string.Format(sql, noonCode);
            //accMgr.ExecQuery(sql, ref ds);
        }

        /// <summary>
        /// 获得患者应交金额、报销金额
        /// </summary>
        /// <param name="regFee"></param>
        /// <param name="chkFee"></param>
        /// <param name="digFee"></param>
        /// <param name="othFee"></param>
        /// <param name="digPub"></param>
        /// <param name="ownCost"></param>
        /// <param name="pubCost"></param>
        /// <param name="cardNo"></param>		
        private void getCost(decimal regFee, decimal chkFee, decimal OwnDigFee, decimal PubDigFee, ref decimal othFee,
            ref decimal ownCost, ref decimal payCost, ref decimal pubCost, string cardNo)
        {
            ownCost = regFee + chkFee + othFee + OwnDigFee;//挂号费自费
            payCost = 0;//诊金记帐
            pubCost = 0;
        }

        /// <summary>
        /// 将应缴金额转为挂号实体,
        /// 属性不能作为ref参数传递 TNND
        /// </summary>
        /// <param name="obj"></param>
        private void ConvertCostToObject(FS.HISFC.Models.Registration.Register obj)
        {
            decimal othFee = 0, ownCost = 0, payCost = 0, pubCost = 0;
            othFee = obj.RegLvlFee.OthFee; //add by niux
            this.getCost(obj.RegLvlFee.RegFee, obj.RegLvlFee.ChkFee, obj.RegLvlFee.OwnDigFee, obj.RegLvlFee.PubDigFee,
                    ref othFee, ref ownCost, ref payCost, ref pubCost, this.regObj.PID.CardNO);

            obj.RegLvlFee.OthFee = othFee;
            obj.OwnCost = ownCost;
            obj.PayCost = payCost;
            obj.PubCost = pubCost;
        }

        /// <summary>
        /// 获取挂号信息
        /// </summary>
        /// <returns></returns>
        private int getValue() 
        {
            //门诊号
            this.regObj.ID = this.regMgr.GetSequence("Registration.Register.ClinicID");
            this.regObj.TranType = FS.HISFC.Models.Base.TransTypes.Positive;//正交易

            if (schema.Templet.RegLevel.ID == "")
            {
                schema.Templet.RegLevel.ID = "1";
            }
            this.regObj.DoctorInfo.Templet.RegLevel.ID = schema.Templet.RegLevel.ID;//this.cmbRegLevel.Tag.ToString();
            if (schema.Templet.RegLevel.Name == "")
            {
                this.schema.Templet.RegLevel.Name = "普通";// "";// this.cmbRegLevel.Text;
            }
            this.regObj.DoctorInfo.Templet.RegLevel.Name = this.schema.Templet.RegLevel.Name;

            this.regObj.DoctorInfo.Templet.Dept.ID = schema.Templet.Dept.ID;// "";// this.cmbDept.Tag.ToString();
            this.regObj.DoctorInfo.Templet.Dept.Name = schema.Templet.Dept.Name;// "";// this.cmbDept.Text;
            #region 处理普通号没有医生 2008-1-5

            //0 科室挂号，1医生挂号
            if (Convert.ToInt32(schema.Templet.EnumSchemaType) == 0)// this.cmbDoctor.Tag.ToString().Equals("1"))
            {
                this.regObj.DoctorInfo.Templet.Doct.ID = "";
                this.regObj.DoctorInfo.Templet.Doct.Name = "";
            }
            else
            {
                this.regObj.DoctorInfo.Templet.Doct.ID = schema.Templet.Doct.ID;// this.cmbDoctor.Tag.ToString();
                this.regObj.DoctorInfo.Templet.Doct.Name = schema.Templet.Doct.Name;// this.cmbDoctor.Text;
            }
            #endregion


            this.regObj.Name = this.patientInfo.Name;// this.txtName.Text.Trim();//患者姓名
            this.regObj.Sex.ID = this.patientInfo.Sex.ID;// this.cmbSex.Tag.ToString();//性别

            this.regObj.Birthday = this.patientInfo.Birthday;//出生日期			

            FS.HISFC.Models.Registration.RegLevel level = null;// (FS.HISFC.Object.Registration.RegLevel)this.cmbRegLevel.SelectedItem;
            level = RegLevelMgr.Query(schema.Templet.RegLevel.ID);
            if (level == null)
            {
                errMsg = "获取挂号级别出错";
                return -1;
            }
            this.regObj.RegType = FS.HISFC.Models.Base.EnumRegType.Reg;

            //如果是今天以后的挂号时间说明是预约挂号
            if (this.schema.SeeDate.Date > this.SchemaMgr.GetDateTimeFromSysDateTime().Date)
            {
                this.regObj.RegType = FS.HISFC.Models.Base.EnumRegType.Pre;
            }

            //add by zengft
            this.regObj.DoctorInfo.Templet.RegQuota = schema.Templet.RegQuota;
            this.regObj.DoctorInfo.Templet.TelQuota = schema.Templet.TelQuota;
            this.regObj.DoctorInfo.Templet.SpeQuota = schema.Templet.SpeQuota;
            //this.regObj.DoctorInfo.Templet.RegLevel.IsExpert = level.IsExpert;
            //this.regObj.DoctorInfo.Templet.AutQuota = schema.Templet.AutQuota;

            #region 结算类别
            this.regObj.Pact.ID = "1"; //this.cmbPayKind.Tag.ToString();//合同单位
            this.regObj.Pact.Name = "自费";// this.cmbPayKind.Text;

            this.regObj.Pact.PayKind.Name = "自费";
            this.regObj.Pact.PayKind.ID = "01";
            this.regObj.SSN = ""; //this.txtMcardNo.Text.Trim();//医疗证号


            //人员黑名单判断

            #endregion

            this.regObj.PhoneHome = this.patientInfo.PhoneHome;//联系电话
            this.regObj.AddressHome = this.patientInfo.AddressHome;//联系地址
            this.regObj.CardType.ID = "";

            #region 预约时间段
            //if (this.regObj.RegType == FS.HISFC.Object.Base.EnumRegType.Pre)//预约号扣排班限额
            //{
            string idno = patientInfo.IDCard.Trim().Replace(".", "").Replace("'", "");
            if (idno.Length > 18)
            {
                this.regObj.IDCard = idno.Substring(0, 18);
            }
            else
            {
                this.regObj.IDCard = idno;
            }
            // (this.txtOrder.Tag as FS.HISFC.Object.Registration.Booking).IDCard;
            this.regObj.DoctorInfo.Templet.Noon.ID = schema.Templet.Noon.ID;// "";// (this.txtOrder.Tag as FS.HISFC.Object.Registration.Booking).DoctorInfo.Templet.Noon.ID;
            this.regObj.DoctorInfo.Templet.IsAppend = schema.Templet.IsAppend;// (this.txtOrder.Tag as FS.HISFC.Object.Registration.Booking).DoctorInfo.Templet.IsAppend;
            this.regObj.DoctorInfo.SeeDate = schema.Templet.Begin;// DateTime.Parse(this.dtBookingDate.Value.ToString("yyyy-MM-dd") + " " +
            this.regObj.DoctorInfo.Templet.Begin = schema.Templet.Begin;
            this.regObj.DoctorInfo.Templet.End = schema.Templet.End;// DateTime.Now; //结束时间
            this.regObj.DoctorInfo.Templet.ID = schema.Templet.ID;//(this.txtOrder.Tag as FS.HISFC.Object.Registration.Booking).DoctorInfo.Templet.ID;
            //{

            //    this.regObj.DoctorInfo.Templet.Noon.ID = this.getNoon(this.regObj.DoctorInfo.SeeDate);


            //    if (this.regObj.DoctorInfo.Templet.Noon.ID == "")
            //    {
            //        errMsg = "未维护午别信息,请先维护!";
            //        return -1;
            //    }
            //}
            #endregion


            #region 挂号费
            //int rtn = ConvertRegFeeToObject(regObj);
            //if (rtn == -1)
            //{
            //    errMsg = "获取挂号费出错";
            //    return -1;
            //}
            //if (rtn == 1)
            //{
            //    errMsg = "该挂号级别未维护挂号费,请先维护挂号费!";
            //    return -1;
            //}

            //获得患者应收、报销
            ConvertCostToObject(regObj);

            #endregion

            //处方号  使用处方号作为发票号
            //this.regObj.InvoiceNO = ""; //this.txtRecipeNo.Text.Trim();
            //this.regObj.RecipeNO = "";// this.txtRecipeNo.Text.Trim();


            this.regObj.IsFee = false;
            this.regObj.Status = FS.HISFC.Models.Base.EnumRegisterStatus.Valid;
            this.regObj.IsSee = false;
            this.regObj.InputOper.ID = this.operId;
            this.regObj.InputOper.OperTime = this.regMgr.GetDateTimeFromSysDateTime();
            this.regObj.DoctorInfo.SeeDate = schema.Templet.Begin;



            FS.HISFC.BizLogic.Registration.Noon noonMgr = new FS.HISFC.BizLogic.Registration.Noon();

            this.regObj.DoctorInfo.Templet.Noon.Name = noonMgr.Query(this.regObj.DoctorInfo.Templet.Noon.ID);
            this.regObj.CancelOper.ID = "";
            this.regObj.CancelOper.OperTime = DateTime.MinValue;
            //挂号为上午号还是为下午号
            this.regObj.PID.User03 = this.regObj.DoctorInfo.Templet.Noon.ID;
            this.regObj.PID.CardNO = this.patientInfo.PID.CardNO;


            //如果是初诊病历，不提取病历
            if (this.regObj.PID.CardNO.IndexOf('M') >= 0)
            {
                this.regObj.CaseState = "9";
            }
            else if (this.regObj.PID.CardNO == "BLB" || this.regObj.PID.CardNO == "BKF")
            {
                this.regObj.CaseState = "9";
            }
            else
            {
                //挂号级别为急诊的不发送到病案室
                if (level.ID == "10")
                {
                    this.regObj.CaseState = "9";
                }
                else
                {
                    this.regObj.CaseState = "0";
                }
            }
            //导管门诊、职工保健不发送病历
            if (this.regObj.DoctorInfo.Templet.Dept.ID == "41C1" ||
                this.regObj.DoctorInfo.Templet.Dept.ID == "42C1" ||
                this.regObj.DoctorInfo.Templet.Dept.ID == "13C2" ||
                this.regObj.DoctorInfo.Templet.Dept.ID == "20C3" ||
                this.regObj.DoctorInfo.Templet.Dept.ID == "43C1")
            {
                this.regObj.CaseState = "9";
            }

            try
            {
                if (this.regObj.DoctorInfo.Templet.Doct.ID == "" &&
                    this.regObj.DoctorInfo.Templet.Dept.ID == "20C2")
                {
                    this.regObj.CaseState = "9";
                }
            }
            catch
            {
            }


            return 0;
        }

        /// <summary>
        /// 不允许使用直接收费生成的号再进行挂号
        /// </summary>
        /// <param name="CardNO"></param>
        /// <returns></returns>
        private int ValidCardNO(string CardNO)
        {
           // FS.HISFC.Integrate.Common.ControlParam controlParams = new FS.HISFC.Integrate.Common.ControlParam();

            string cardRule = controlParamIntegrate.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.Const.NO_REG_CARD_RULES, false, "9");
            if (CardNO != "" && CardNO != string.Empty)
            {
                if (CardNO.Substring(0, 1) == cardRule)
                {
                    errMsg = "此号段为直接收费使用，请选择其它号段";
                    return -1;
                }
            }
            return 1;
        }

        /// <summary>
        /// 更新全院看诊序号
        /// </summary>
        /// <param name="rMgr"></param>
        /// <param name="current"></param>
        /// <param name="seeNo"></param>
        /// <param name="Err"></param>
        /// <returns></returns>
        private int Update(FS.SOC.HISFC.PE.BizLogic.Register rMgr, DateTime current, ref int seeNo,
            ref string Err)
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
      
        /// <summary>
        /// 保存处方记录
        /// </summary>
        /// <returns></returns>
        private int SaveRecipeNo()
        {
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.regMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            try
            {
                this.conMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                FS.HISFC.Models.Base.Const con = new FS.HISFC.Models.Base.Const();
                con.ID = this.operId;//操作员
                con.Name = this.regObj.InvoiceNO;//处方号
                con.IsValid = true;

                int rtn = this.conMgr.UpdateConstant("RegRecipeNo", con);
                if (rtn == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.errMsg = this.conMgr.Err;
                    //  MessageBox.Show(this.conMgr.Err, "提示");
                    return -1;
                }
                if (rtn == 0)//更新没有数据、插入
                {
                    if (this.conMgr.InsertConstant("RegRecipeNo", con) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        this.errMsg = this.conMgr.Err;
                        // MessageBox.Show(this.conMgr.Err, "提示");
                        return -1;
                    }
                }
                FS.FrameWork.Management.PublicTrans.Commit();
            }
            catch (Exception e)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                this.errMsg = e.Message;
                // MessageBox.Show(e.Message, "提示");
                return -1;
            }

            return 0;
        }
       
        /// <summary>
        /// 获取门诊科室列表
        /// </summary>
        /// <returns></returns>
        public string GetOutDeptList()
        {
            string sql = @"select d.DEPT_CODE as ""DepRowid"",d.DEPT_NAME as ""DepDesc""
                            from COM_DEPARTMENT d
                            where d.REGDEPT_FLAG= '1' and d.DEPT_CODE  in ( 
                            select distinct code
                            from COM_DICTIONARY
                            where type = 'ATMDEPT' and valid_state='1')
                            and length(d.DEPT_NAME )>4
                            and d.VALID_STATE='1'
                            and d.DEPT_CODE<>'41T6'
                            order by int(simple_name) ";
            DataSet ds = new DataSet();
            try
            {
                outpatientManager.ExecQuery(sql, ref ds);

                return ds.GetXml();
            }
            catch
            {
                errMsg = outpatientManager.Err;
            }
            return "";
        }
       
        /// <summary>
        /// 根据SchemaId 获取医生的余号信息
        /// </summary>
        /// <param name="schemaId"></param>
        /// <returns></returns>
        public string RegRemainList(string schemaId)
        {
            string head = "<Response><ResultCode>";
            string res = "";
            string sql = @"select distinct s.DOCT_NAME as ""DoctName"", s.ID ""SchemaId"", s.BEGIN_TIME ""RegisterDate"",
                          r.SEENO ""RegisterID"",r.STATE as ""RegisterStatus""
                          from FIN_OPR_SCHEMA s 
                          join FIN_OPR_REGNO r on s.ID=r.SCHEMAID
                          where s.ID='{0}'";
            DataSet dsNew = new DataSet();
            try
            {
                sql = string.Format(sql, schemaId);
                outpatientManager.ExecQuery(sql, ref dsNew);

                if (dsNew == null || dsNew.Tables[0].Rows.Count == 0)
                {
                    this.errMsg = "没有余号";
                    head += "1</ResultCode><ErrorMsg>" + errMsg + "</ErrorMsg>";
                }
                else
                {
                    DataSet ds = new DataSet();
                    DataTable dtParent = ds.Tables.Add("RbInfo");
                    DataTable dtSub = ds.Tables.Add("RegisterRemain");


                    string parSeg = @"<DOCRBList><SubDisDec></SubDisDec><DoctName></DoctName><SchemaId ></SchemaId><RegisterDate></RegisterDate></DOCRBList>";
                    string subSeg = @"<RegisterRemain><RegisterID></RegisterID><RegisterStatus></RegisterStatus></RegisterRemain>";

                    dtParent.Columns.AddRange(new DataColumn[] 
                                                    {
                                                         new DataColumn("SubDisDec",typeof(string)),
                                                         new DataColumn("SchemaId",typeof(string)),
                                                         new DataColumn("DoctName",typeof(string)),
                                                         new DataColumn("RegisterDate",typeof(string))
                                                    });
                    //dtParent.Rows[0].GetChildRows();
                    //DataRelation drl = new DataRelation();
                    //dtParent.ChildRelations
                    dtSub.Columns.AddRange(new DataColumn[] 
                                                    {
                                                         new DataColumn("SchemaId",typeof(string)),
                                                         new DataColumn("RegisterID",typeof(string)),
                                                         new DataColumn("RegisterStatus",typeof(string))
                                                    });

                    //当前医生的列表
                    List<string> schemaList = new List<string>();
                    foreach (DataRow dr in dsNew.Tables[0].Rows)
                    {
                        //             select '{3}' as ""SubDisDec""  s.ID as ""SchemaId"", s.BEGIN_TIME as ""RegisterDate"",s.DOCT_NAME as ""DoctName"",
                        //s.AUT_LMT - s.AUT_REGED as ""Remain"", s.DOCT_CODE as ""DoctId""
                        string subDec = "";
                        string tmpId = dr["SchemaId"].ToString();
                        string registerID = dr["RegisterID"].ToString();
                        string doctName = dr["DoctName"].ToString();
                        string registerStatus = dr["RegisterStatus"].ToString();
                        string regDate = dr["RegisterDate"].ToString();

                        if (!schemaList.Contains(tmpId))
                        {
                            dtParent.Rows.Add(new string[] { subDec, schemaId, doctName, regDate });
                        }

                        schemaList.Add(schemaId);
                        dtSub.Rows.Add(new string[] { schemaId, registerID, registerStatus });
                    }

                    ds.Relations.Add("SeqList", dtParent.Columns["SchemaId"], dtSub.Columns["SchemaId"]);
                    System.Xml.XmlDocument xmlPar;//= new System.Xml.XmlDocument();
                    System.Xml.XmlDocument xmlSub;//= new System.Xml.XmlDocument();

                    List<string> seg = new List<string>();

                    foreach (DataRow dr in dtParent.Rows)
                    {
                        string curSeg = "";
                        xmlPar = new System.Xml.XmlDocument();
                        xmlPar.LoadXml(parSeg);
                        string subDec = dr["SubDisDec"].ToString();
                        string doctName = dr["DoctName"].ToString();
                        schemaId = dr["SchemaId"].ToString();
                        string registerDate = dr["RegisterDate"].ToString();

                        xmlPar.SelectSingleNode("/DOCRBList/SubDisDec").InnerText = subDec;
                        xmlPar.SelectSingleNode("/DOCRBList/DoctName").InnerText = doctName;
                        xmlPar.SelectSingleNode("/DOCRBList/SchemaId").InnerText = schemaId;
                        xmlPar.SelectSingleNode("/DOCRBList/RegisterDate").InnerText = registerDate;

                        curSeg += xmlPar.InnerXml.ToString();

                        string curSubSeg = "";
                        DataRow[] drChilds = dr.GetChildRows("SeqList");
                        foreach (DataRow child in drChilds)
                        {
                            string registerID = child["RegisterID"].ToString();
                            string registerStatus = child["RegisterStatus"].ToString();


                            xmlSub = new System.Xml.XmlDocument();
                            xmlSub.LoadXml(subSeg);
                            xmlSub.SelectSingleNode("/RegisterRemain/RegisterID").InnerText = registerID;
                            xmlSub.SelectSingleNode("/RegisterRemain/RegisterStatus").InnerText = registerStatus;
                            curSubSeg += xmlSub.InnerXml.ToString();
                        }

                        int index = curSeg.IndexOf("</DOCRBList>");
                        curSeg = curSeg.Insert(index, curSubSeg);
                        seg.Add(curSeg);
                    }


                    foreach (string s in seg)
                    {
                        res += s;
                    }

                    head += "0</ResultCode><ErrorMsg></ErrorMsg>";
                }
            }
            catch
            {
                errMsg = outpatientManager.Err;
                head += "1</ResultCode><ErrorMsg>" + errMsg + "</ErrorMsg>";
            }
            return head + res + "</Response>";
        }
       
        /// <summary>
        /// 把组套拆分成明细
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        private ArrayList ConvertGroupToDetail(FS.HISFC.Models.Fee.Outpatient.FeeItemList f, FS.HISFC.Models.Registration.Register rgs)
        {
            ArrayList undrugCombList = this.undrugPackAgeManager.QueryUndrugPackagesBypackageCode(f.Item.ID);
            ArrayList alTemp = new ArrayList();
            if (undrugCombList == null)
            {
                this.errMsg = "获得组套明细出错!" + undrugPackAgeManager.Err;

                return null;
            }
            decimal price = 0;
            decimal count = 0;
            string feeCode = string.Empty;
            string itemType = string.Empty;
            decimal totCost = 0;
            FeeItemList feeDetail = null;
            if (f.Order.ID == null || f.Order.ID == string.Empty)
            {
                f.Order.ID = this.orderIntegrate.GetNewOrderID();
                if (f.Order.ID == null || f.Order.ID == string.Empty)
                {
                    this.errMsg = "获得医嘱流水号出错!";

                    return null;
                }
            }
            foreach (FS.HISFC.Models.Fee.Item.UndrugComb undrugCombo in undrugCombList)
            {

                FS.HISFC.Models.Fee.Item.Undrug undrugItem = new FS.HISFC.Models.Fee.Item.Undrug();
                undrugItem = this.feeIntegrate.GetItem(undrugCombo.ID);
                feeDetail = new FeeItemList();

                feeCode = undrugItem.MinFee.ID.ToString();//rowFindZT["FEE_CODE"].ToString();
                try
                {
                    DateTime nowTime = this.outpatientManager.GetDateTimeFromSysDateTime();
                    int age = (int)((new TimeSpan(nowTime.Ticks - rgs.Birthday.Ticks)).TotalDays / 365);

                    FS.FrameWork.Models.NeuObject priceObj = new FS.FrameWork.Models.NeuObject();
                    priceObj.ID = rgs.Pact.PriceForm;
                    priceObj.Name = age.ToString();

                    priceObj.User01 = undrugItem.Price.ToString();
                    priceObj.User02 = undrugItem.SpecialPrice.ToString();
                    priceObj.User03 = undrugItem.ChildPrice.ToString();
                    //price = this.GetPrice(priceObj);
                }
                catch (Exception e)
                {
                    this.errMsg = e.Message;

                    return null;
                }

                count = FS.FrameWork.Function.NConvert.ToDecimal(f.Item.Qty) * undrugCombo.Qty;
                totCost = price * count;

                feeDetail.Patient = f.Patient.Clone();
                feeDetail.Item.ID = undrugItem.ID.ToString();//rowFindZT["ITEM_CODE"].ToString();
                feeDetail.Item.Name = undrugItem.Name.ToString();//rowFindZT["ITEM_NAME"].ToString();
                feeDetail.Name = feeDetail.Item.Name;
                feeDetail.ID = feeDetail.Item.ID;

                if (undrugItem.UnitFlag.ToString() == "0")
                {
                    feeDetail.IsGroup = false;
                }
                else
                {
                    feeDetail.IsGroup = true;
                }
                feeDetail.Item.ItemType = undrugItem.ItemType;

                feeDetail.RecipeOper = f.RecipeOper.Clone();
                feeDetail.Item.Price = price;
                feeDetail.Item.Specs = undrugItem.Specs;//rowFindZT["SPECS"].ToString();
                feeDetail.Item.SysClass.ID = undrugItem.SysClass.ID;//rowFindZT["SYS_CLASS"].ToString();
                feeDetail.Item.MinFee.ID = feeCode;
                feeDetail.Item.PackQty = undrugItem.PackQty;//NConvert.ToDecimal(rowFindZT["PACK_QTY"].ToString());
                feeDetail.Item.Qty = count;
                feeDetail.Days = FS.FrameWork.Function.NConvert.ToDecimal(f.Days);
                feeDetail.FT.TotCost = totCost;
                //自费如此，如果加上公费需要重新计算!!!
                feeDetail.FT.OwnCost = totCost;
                feeDetail.ExecOper = f.ExecOper.Clone();
                //feeDetail.Item.PriceUnit = rowFindZT["MIN_UNIT"].ToString() == string.Empty ? "次" : rowFindZT["MIN_UNIT"].ToString();
                feeDetail.Item.PriceUnit = undrugItem.PriceUnit;
                //if (rowFindZT["CONFIRM_FLAG"].ToString() == "2" || rowFindZT["CONFIRM_FLAG"].ToString() == "3" || rowFindZT["CONFIRM_FLAG"].ToString() == "1")
                //{
                //    feeDetail.Item.IsNeedConfirm = true;
                //}
                //else
                //{
                //    feeDetail.Item.IsNeedConfirm = false;
                //}
                feeDetail.Item.IsNeedConfirm = undrugItem.IsNeedConfirm;
                //feeDetail.Item.IsNeedBespeak = NConvert.ToBoolean(rowFindZT["NEEDBESPEAK"].ToString());
                feeDetail.Item.IsNeedBespeak = undrugItem.IsNeedBespeak;

                feeDetail.Order.ID = f.Order.ID;
                feeDetail.UndrugComb.ID = f.Item.ID;
                feeDetail.UndrugComb.Name = f.Item.Name;
                feeDetail.Order.Combo.ID = f.Order.Combo.ID;
                feeDetail.Item.IsMaterial = f.Item.IsMaterial;
                feeDetail.RecipeSequence = f.RecipeSequence;
                feeDetail.FTSource = f.FTSource;
                feeDetail.FeePack = f.FeePack;
                if (rgs.Pact.PayKind.ID == "03")
                {
                    FS.HISFC.Models.Base.PactItemRate pactRate = null;

                    if (pactRate == null)
                    {
                        pactRate = this.pactUnitItemRateManager.GetOnepPactUnitItemRateByItem(rgs.Pact.ID, feeDetail.Item.ID);

                        if (pactRate == null)
                        {
                            pactRate = this.pactUnitItemRateManager.GetOnePaceUnitItemRateByFeeCode(rgs.Pact.ID, f.Item.MinFee.ID);
                        }
                    }
                    if (pactRate != null)
                    {
                        if (f.ItemRateFlag != "3")
                        {
                            if (pactRate.Rate.PayRate != rgs.Pact.Rate.PayRate)
                            {
                                if (pactRate.Rate.PayRate == 1)//自费
                                {
                                    feeDetail.ItemRateFlag = "1";
                                }
                                else
                                {
                                    feeDetail.ItemRateFlag = "3";
                                }
                            }
                            else
                            {
                                feeDetail.ItemRateFlag = "2";

                            }
                            feeDetail.OrgItemRate = rgs.Pact.Rate.PayRate;
                            feeDetail.NewItemRate = pactRate.Rate.PayRate;
                        }
                        if (f.ItemRateFlag == "3")
                        {
                            feeDetail.OrgItemRate = f.OrgItemRate;
                            feeDetail.NewItemRate = f.NewItemRate;
                            feeDetail.ItemRateFlag = "2";
                        }
                    }
                    else
                    {
                        if (f.ItemRateFlag == "3")
                        {
                            if (undrugItem.SpecialFlag2.ToString() != "1")
                            //if (rowFindZT["ZF"].ToString() != "1")
                            {
                                feeDetail.OrgItemRate = f.OrgItemRate;
                                feeDetail.NewItemRate = f.NewItemRate;
                                feeDetail.ItemRateFlag = "2";
                            }
                        }
                        else
                        {
                            feeDetail.OrgItemRate = f.OrgItemRate;
                            feeDetail.NewItemRate = f.NewItemRate;
                            feeDetail.ItemRateFlag = f.ItemRateFlag;
                        }
                    }
                }

                alTemp.Add(feeDetail);
            }
            if (alTemp.Count > 0)
            {
                if (f.FT.RebateCost > 0)//有减免
                {
                    if (rgs.Pact.PayKind.ID != "01")
                    {
                        //MessageBox.Show("暂时不允许非自费患者减免!");
                        this.errMsg = "暂时不允许非自费患者减免!";
                        return null;
                    }
                    decimal rebateRate =
                        FS.FrameWork.Public.String.FormatNumber(
                        f.FT.RebateCost / (f.FT.OwnCost + f.FT.RebateCost), 2);
                    decimal tempFix = 0;
                    decimal tempRebateCost = 0;
                    foreach (FeeItemList feeTemp in alTemp)
                    {
                        feeTemp.FT.RebateCost = (feeTemp.FT.OwnCost + feeTemp.FT.RebateCost) * rebateRate;
                        tempRebateCost += feeTemp.FT.RebateCost;
                        feeTemp.FT.OwnCost = feeTemp.FT.OwnCost - feeTemp.FT.RebateCost;
                        feeTemp.FT.TotCost = feeTemp.FT.TotCost - feeTemp.FT.RebateCost;
                    }
                    tempFix = f.FT.RebateCost - tempRebateCost;
                    FeeItemList fFix = alTemp[0] as FeeItemList;
                    fFix.FT.RebateCost = fFix.FT.RebateCost + tempFix;
                    fFix.FT.OwnCost = fFix.FT.OwnCost - tempFix;
                    fFix.FT.TotCost = fFix.FT.TotCost - tempFix;
                }
            }
            if (alTemp.Count > 0)
            {
                //原来根据自费单价进行判断
                //if (f.SpecialPrice > 0)//有特殊自费
                if (f.FT.User03 != string.Empty)//有特殊自费
                {
                    //原来根据自费单价进行计算
                    #region

                    //decimal tempPrice = 0m;
                    //string id = string.Empty;
                    //foreach (FeeItemList feeTemp in alTemp)
                    //{
                    //    if (feeTemp.Item.Price > tempPrice)
                    //    {
                    //        id = feeTemp.Item.ID;
                    //        tempPrice = feeTemp.Item.Price;
                    //    }
                    //}

                    //foreach (FeeItemList fee in alTemp)
                    //{
                    //    if (fee.Item.ID == id)
                    //    {
                    //        fee.SpecialPrice = f.SpecialPrice;

                    //        break;
                    //    }
                    //}

                    #endregion
                    //现在根据总金额进行计算
                    string temptotCost = "";
                    string id = string.Empty;
                    foreach (FeeItemList feeTemp in alTemp)
                    {
                        if (FS.FrameWork.Function.NConvert.ToDecimal(feeTemp.FT.TotCost)> FS.FrameWork.Function.NConvert.ToDecimal(temptotCost))
                        {
                            id = feeTemp.Item.ID;
                            temptotCost = feeTemp.FT.TotCost.ToString();
                        }
                    }

                    foreach (FeeItemList fee in alTemp)
                    {
                        if (FS.FrameWork.Function.NConvert.ToDecimal(f.FT.User03) > 0)
                        {
                            if (fee.Item.ID == id)
                            {
                                fee.FT.User03 = f.FT.User03;

                                break;
                            }
                        }
                        else
                        {
                            fee.FT.User03 = f.FT.User03;
                        }
                    }
                }
            }

            return alTemp;
        }
       
        #region rabit add

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int CancelRegist(string CardNo, string CardSerNo, string RegSerNo, string UserId, ref string InvoiceNO, ref string SumFee)
        {

            //预约挂号直接调用webservice接口 
            string errorMSG = "";


            //Service onlineweb = new Service();
           
           FS.HISFC.Models.Registration.Register reg = regMgr.GetByClinic(RegSerNo);
           //获取ATM机器编号
           FS.HISFC.BizLogic.Manager.Constant conMgr = new FS.HISFC.BizLogic.Manager.Constant();
           ArrayList arrATMList = conMgr.GetList("ATMMACHINE");


           System.Collections.Generic.List<string> atmList = new System.Collections.Generic.List<string>();

           foreach (FS.HISFC.Models.Base.Const c in arrATMList)
           {
               atmList.Add(c.Name);
           }

           //FS.HISFC.Object.Registration.RegLevel regLevel = new FS.HISFC.Object.Registration.RegLevel();

           FS.HISFC.BizLogic.Registration.RegLevel reglevelManager = new FS.HISFC.BizLogic.Registration.RegLevel();

           if (reg == null || reg.ID == "")
           {
               this.errMsg = "获取挂号信息错误！";
               return -1;
           }

           if (reg.Pact.ID != "1")
           {
               this.errMsg = "只能退合同单位为自费的挂号！";
               return -1;
           }



           if (reg.DoctorInfo.SeeDate.Date <= regMgr.GetDateTimeFromSysDateTime().Date)
           {
               this.errMsg = "不能退当天，以及之前的挂号信息，请去前台处理！";
               return -1;
           }

           //int regLevel = reglevelManager.IsExpert(reg);
           //bool chargeFromAcc = this.regMgr.IsCheckFlag(reg.InvoiceNO, reg.PID.CardNO);

           //允许退ATM机的号
           if (!atmList.Contains(reg.InputOper.ID))
           {
               this.errMsg = "只能退ATM机上的预约挂号数据，其他退号请去前台！";
               return -1;
           }
           SumFee = reg.OwnCost.ToString();
           InvoiceNO = reg.InvoiceNO;

          // if (onlineweb.CancleRegister(RegSerNo, reg.DoctorInfo.Templet.ID, "06226600", "", "自助退号", UserId, ref errorMSG) != 1)
          //  {
          //      this.errMsg = errorMSG;
          //          return -1;
          //  }
          //else
          //{
          //   return 0;
          //}  
           return -1;
        }
        #endregion 


        #region 查询

        /// <summary>
        /// 查询住院记录
        /// </summary>
        /// <param name="CardNo"></param>
        /// <returns></returns>
        public string InHospitalExpenseRecord(string CardNo)
        {
            string sql = @"select i.NAME,i.INPATIENT_NO as InHospitalNo ,i.IN_DATE as InHospitalDate,
                          i.BALANCE_COST as SumFee,
                          case  i.IN_STATE when 'I' then '正在住院' when 'O' then '出院结算' when 'R' then '住院登记' 
                          when 'B' then '出院登记' when 'P' then '预约出院' when 'N' then '无费退院' end   as Flag 
                          from FIN_IPR_INMAININFO i 
                          where i.CARD_NO =(select p.CARD_NO from COM_PATIENTINFO  p where p.IC_CARDNO='{0}')
                          and i.IN_DATE between current timestamp -3 month  and  current timestamp
                            ";
            DataSet ds = new DataSet();
            try
            {
                sql = string.Format(sql, CardNo);
                outpatientManager.ExecQuery(sql, ref ds);
                ds.DataSetName = "RecordList";
                ds.Tables[0].TableName = "Record";
                return ds.GetXml();
            }
            catch
            {
                return "error";
            }
        }

        #endregion

        #region 登记
        /// <summary>
        /// 登记信息
        /// </summary>
        /// <param name="regObj"></param>
        /// <returns></returns>
        public string _intAddRegisterInf(FS.HISFC.Models.Registration.Register regObj)
        {
            string strTmp = "-1";
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.regMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.patientMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.accountManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            string Err = string.Empty;
            //更新患者基本信息
            if (this.UpdatePatientinfo(regObj, this.patientMgr, this.regMgr, ref Err) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                return strTmp="-1";
            }
            if (regObj.User01 == "1")
            {
                if (this.InsertAccountPE(regObj, this.accountManager) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    return strTmp = "-1";
                }
                if (this.InsertAccountCardPE(regObj, this.accountManager) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    return strTmp = "-1";
                }
                string strCaseID = this._strGetSequence("select seq_cm_caseid.nextval from dual");

               if (this.InsertMetCASCase(regObj, this.accountManager,strCaseID) == -1)
               {
                   FS.FrameWork.Management.PublicTrans.RollBack();
                   return strTmp = "-1";
               }

               //if (this.InsertMetCasCaseRecord(regObj, this.accountManager, strCaseID) == -1)
               //{
               //    FS.FrameWork.Management.PublicTrans.RollBack();
               //    return strTmp = "-1";
               //}
            }
            //登记挂号信息
            if (regObj.User02 == "1")//需要登记
            {
                if (this.accountManager.InsertPERegister(regObj) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.errMsg = this.accountManager.Err;
                    return strTmp = "-1";
                } 
            }
            else
            {
                if (this.accountManager.UpdatePERegister(regObj) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.errMsg = this.accountManager.Err;
                    return strTmp = "-1";
                }
            }

            FS.FrameWork.Management.PublicTrans.Commit();
            return strTmp = "1";
        }
        #endregion

        #region 更新患者基本信息
        /// <summary>
        /// 更新患者基本信息
        /// </summary>
        /// <param name="regInfo"></param>
        /// <param name="patMgr"></param>
        /// <param name="registerMgr"></param>
        /// <param name="Err"></param>
        /// <returns></returns>
        private int UpdatePatientinfo(FS.HISFC.Models.Registration.Register regInfo,
            FS.HISFC.BizProcess.Integrate.RADT patMgr, FS.SOC.HISFC.PE.BizLogic.Register registerMgr,
            ref string Err)
        {
            int rtn = -1;
            if (regInfo == null)
            {
                return rtn;
            }
            if (regInfo.User01 == "2")
            {
                rtn = registerMgr.Update(FS.SOC.HISFC.PE.BizLogic.EnumUpdateStatus.PatientInfo, regInfo);
                if (rtn == -1)
                {
                    Err = registerMgr.Err;
                    return -1;
                }
            }
            if (regInfo.User01 == "1")//没有更新到患者信息，插入
            {
                FS.HISFC.Models.RADT.PatientInfo p = new FS.HISFC.Models.RADT.PatientInfo();

                p.PID.CardNO = regInfo.PID.CardNO;
                p.Name = regInfo.Name;
                p.Sex.ID = regInfo.Sex.ID;
                p.Birthday = regInfo.Birthday;
                p.Pact = regInfo.Pact;
                p.Pact.PayKind.ID = regInfo.Pact.PayKind.ID;
                p.SSN = regInfo.SSN;
                p.PhoneHome = regInfo.PhoneHome;
                p.AddressHome = regInfo.AddressHome;
                p.IDCard = regInfo.IDCard;
                p.Memo = regInfo.CardType.ID;
                p.NormalName = regInfo.NormalName;
                p.IsEncrypt = regInfo.IsEncrypt;
               // p.PatientId = p.PID.CardNO;
                p.ID = this._strGetSequence("select  seq_fin_clinicno.nextval from dual").PadLeft(8, '0');
                // modify by majq
                p.Card.ICCard.ID = p.PID.CardNO;//.Remove(0,2);
                rtn = patientMgr.RegisterComPatient(p);
                if (rtn <= 0)
                {
                    Err = patientMgr.Err;
                }         
            }
            return rtn;
        }

        public string _strGetSequence(string p_strSqlID)
        {
            string strTmp = string.Empty;
            if (string.IsNullOrEmpty(p_strSqlID))
            {
                return strTmp;
            }
            try
            {
                //strTmp = this.regMgr.GetSequence(p_strSqlID);
                //strTmp = accountManager.GetSequence(p_strSqlID);
                DataSet objDs = new DataSet();
                int intI = accountManager._dsExcuteQuery(p_strSqlID, ref objDs);
                if (intI < 0 || objDs.Tables[0].Rows.Count == 0)
                {
                    return strTmp;
                }
                strTmp = objDs.Tables[0].Rows[0][0].ToString();
            }
            catch 
            {
                strTmp = string.Empty;
            }
            return strTmp;
        }
        #endregion

        #region 插入收费信息
        public bool SetChargeInfo(FS.HISFC.Models.Registration.Register r, ArrayList feeItemLists, DateTime chargeTime, ref string errText)
        {
            bool strTmp = false;
            if (r == null || feeItemLists == null)
            {
                return strTmp;
            }
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.feeIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            try
            {
                strTmp = this.feeIntegrate.SetChargeInfo(r, feeItemLists, chargeTime, ref errText);
            }
            catch
            {
                strTmp = false;
                FS.FrameWork.Management.PublicTrans.RollBack();
            }
            if (strTmp)
            {
                FS.FrameWork.Management.PublicTrans.Commit();
            }
            else
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
            }
            return strTmp;
        }
        #endregion

        #region 删除未收费信息
        /// <summary>
        /// 删除未收费信息
        /// </summary>
        /// <param name="strPatientID">体检人ID</param>
        /// <param name="strRegisterNo">体检人登记流水号</param>
        /// <param name="errText">出错信息</param>
        /// <returns>是否成功，true-成功，false-失败</returns>
        public int _DeleteUnChargeInfo(string strPatientID, string strRegisterNo, ref string errText)
        {
            int intI  = 0;
            errText = string.Empty;
            if (strPatientID == null || strRegisterNo == null)
            {
                errText = "门诊卡号或者门诊号为空";
                return intI;
            }
            string strSql = @"delete from fin_opb_feedetail a where a.card_no='{0}' and a.clinic_code='{1}'  and a.pay_flag='0' ";
            strSql = string.Format(strSql, strPatientID, strRegisterNo);
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.outpatientManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            try
            {
                intI = accountManager._ExcuteSQL(strSql);
            }
            catch(Exception objEx)
            {
                errText = objEx.Message;
                FS.FrameWork.Management.PublicTrans.RollBack();
            }
            if (intI >= 0)
            {
                intI = 1;
                FS.FrameWork.Management.PublicTrans.Commit();
            }
            else
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
            }
            return intI;
        }
        #endregion

        #region 查询全部的有效的科室编码、名称信息。
        /// <summary>
        /// 查询全部的有效的科室编码、名称信息。
        /// </summary>
        /// <returns></returns>
        public int _QueryDeptInf(ref DataSet objDs)
        {
            int intI = 0;
            objDs = null;
            string strSql = @"select a.dept_code,a.dept_name from com_department a where a.valid_state= '1'";

            try
            {
                intI = accountManager._dsExcuteQuery(strSql, ref objDs);
            }
            catch 
            {
            }
            return intI;
        }
        #endregion

        #region 用于查询全部的有效的操作人员编码、姓名，性别
        /// <summary>
        /// 用于查询全部的有效的操作人员编码、姓名，性别
        /// </summary>
        /// <returns></returns>
        public int _QueryEmpInf(ref DataSet objDs)
        {
            int intI = 0;
            objDs = null;
            string strSql = @"select a.empl_code,a.empl_name,a.SEX_CODE from com_employee a where a.valid_state='1'";

            try
            {
                intI = accountManager._dsExcuteQuery(strSql, ref objDs);
            }
            catch
            {
            }
            return intI;
        }
        #endregion

        #region 用于查询全部的收费组套项目ID、名称、价格
        /// <summary>
        /// 用于查询全部的收费组套项目ID、名称、价格
        /// </summary>
        /// <returns></returns>
        public int _QueryItemInf(ref DataSet objDs)
        {
            int intI = 0;
            string strSql = @"select b.item_code as item_code,b.item_name as item_name,b.stock_unit as stock_unit,b.unit_price as unit_price,b.unit_price2 as unit_price2,1 as PACK_QTY 
from fin_com_undruginfo b where b.valid_state='1' union all 
select i.DRUG_CODE as item_code,i.TRADE_NAME as item_name,i.PACK_UNIT as stock_unit,i.RETAIL_PRICE as unit_price,i.RETAIL_PRICE as unit_price2,i.PACK_QTY as PACK_QTY
from PHA_COM_BASEINFO i where i.VALID_STATE = '1'";
            objDs = new DataSet();
            try
            {
                intI = accountManager._dsExcuteQuery(strSql, ref objDs);
            }
            catch
            {
            }
            return intI;
        }
        #endregion

        #region 用于查询项目的最小费用ID和分类ID
        /// <summary>
        /// 用于查询项目的最小费用ID
        /// </summary>
        /// <returns></returns>
        public int _QueryItemFeeIDInf(string p_strItemCode, ref string strItemFeeCode, ref string strFeeClass, ref bool isPharmacy)
        {
            int intI = -1;
            string strSql = @"select d.fee_code,d.sys_class from fin_com_undruginfo d where d.item_code='{0}'";
            string strSql1 = @"select d.FEE_CODE,d.CLASS_CODE as sys_class from pha_com_baseinfo d where d.DRUG_CODE='{0}'";
            strSql = string.Format(strSql,p_strItemCode);
            strSql1 = string.Format(strSql1, p_strItemCode);
            DataSet objDs = new DataSet();
            try
            {
                intI = accountManager._dsExcuteQuery(strSql, ref objDs);
                if (objDs == null || objDs.Tables[0].Rows.Count == 0)
                {
                    intI = accountManager._dsExcuteQuery(strSql1, ref objDs);
                    if (objDs == null || objDs.Tables[0].Rows.Count == 0)
                    {
                        return -1;
                    }
                    isPharmacy = true;
                }
                strItemFeeCode = objDs.Tables[0].Rows[0]["fee_code"].ToString();
                strFeeClass = objDs.Tables[0].Rows[0]["sys_class"].ToString();
            }
            catch
            {
            }
            return intI;
        }
        #endregion

        #region 插入AccountPE
        /// <summary>
        /// 插入AccountPE信息
        /// </summary>
        /// <param name="regObj"></param>
        /// <returns>是否成功，true-成功，false-失败</returns>
        public int InsertAccountPE(FS.HISFC.Models.Registration.Register regObj, FS.SOC.Local.HISWebService.PE.PEChargeService_Db acManager)
        {
            int intI = 0;
            string strAccount = this._strGetSequence("select seq_fin_opb_accountno.nextval from dual");
            string strSql = @"insert into fin_opb_account (account_no, card_no, state, vacancy, password, daylimit, is_empower, idno, ee, account_type)
values('{0}','{1}','1',0.00,'888888',0.00,'','{2}','noHICAEwogc=','1')";
//            string strSql = @"insert into fin_opb_account (account_no, card_no, state, vacancy, password, daylimit, is_empower)
//values('{0}','{1}','1',0.00,'888888',0.00,'')";
            strSql = string.Format(strSql, strAccount, regObj.PID.CardNO, regObj.IDCard);
            try
            {
                intI = acManager._ExcuteSQL(strSql);
            }
            catch (Exception objEx)
            {
                errMsg = objEx.Message;
                intI = -1;
                
            }
            if (intI < 0)
            {
                this.errMsg = "插入表FIN_OPB_ACCOUNT失败，请确认是否要插入";
            }
            return intI;
           
                
        }
        #endregion

        #region 插入AccountRecordPE
        /// <summary>
        /// 插入AccountRecordPE信息
        /// </summary>
        /// <param name="regObj"></param>
        /// <returns>是否成功，true-成功，false-失败</returns>
        public int InsertAccountCardPE(FS.HISFC.Models.Registration.Register regObj, FS.SOC.Local.HISWebService.PE.PEChargeService_Db acManager)
        {
            int intI = 0;
            string strSql = @"insert into fin_opb_accountcard
                            (card_no, markno, type, state, securitycode, bankno) 
                            values( '{0}','{1}','2','1','','')";
//            string strSql = @"insert into fin_opb_accountcard
//                            (card_no, markno, type, state, securitycode) 
//                            values( '{0}','{1}','2','1','')";
            strSql = string.Format(strSql, regObj.PID.CardNO, regObj.PID.CardNO.Remove(0, 2));
            try
            {
                intI = acManager._ExcuteSQL(strSql);
            }
            catch (Exception objEx)
            {
                errMsg = objEx.Message;
                intI = -1;
                //t.RollBack();
            }
            if (intI < 0)
            {
                this.errMsg = "插入表FIN_OPB_ACCOUNTCARD失败，请确认是否要插入";
            }
            return intI;
        }
        #endregion
        
        #region 插入MET_CAS_CASE 表
        /// <summary>
        /// 插入MET_CAS_CASE信息
        /// </summary>
        /// <param name="regObj"></param>
        /// <returns>是否成功，true-成功，false-失败</returns>
        public int InsertMetCASCase(FS.HISFC.Models.Registration.Register regObj, FS.SOC.Local.HISWebService.PE.PEChargeService_Db acManager, string strCaseID)
        {
            int intI = 0;
            string strSql = @"insert into met_cas_case
                              (case_id, card_no, first_code, second_code, 
                              third_code, cabinet_code, cabinet_gridno, 
                              case_state, lose_type, in_type, in_employee,
                              in_dept, empi_id, createsec_time, createthird_time,
                              extend1, extend2, extend3) 
                              values (
                              '{0}', 
                              '{1}', 
                              '{2}', 
                              '', 
                              '', 
                              '',  
                              '',  
                              '', 
                              '',  
                              '{3}', 
                              '', 
                              '', 
                              '{4}', 
                               to_date('{5}','yyyy-mm-dd hh24:mi:ss'),
                               to_date( '{6}','yyyy-mm-dd hh24:mi:ss'),
                                 '',
                               '', 
                                '')";
            strSql = string.Format(strSql, strCaseID, regObj.PID.CardNO, regObj.PID.CardNO, "0", regObj.PID.CardNO.Remove(0, 2), acManager.GetSysDateTime(), acManager.GetSysDateTime());
//            string strSql = @"insert into met_cas_case
//                              (case_id, card_no, first_code, second_code, 
//                              third_code, cabinet_code, cabinet_gridno, 
//                              case_state, lose_type, in_type, in_employee,
//                              in_dept, empi_id) 
//                              values (
//                              '{0}', 
//                              '{1}', 
//                              '{2}', 
//                              '', 
//                              '', 
//                              '',  
//                              '',  
//                              '', 
//                              '',  
//                              '{3}', 
//                              '', 
//                              '', 
//                              '{4}')";
            //strSql = string.Format(strSql, strCaseID, regObj.PID.CardNO, regObj.PID.CardNO, "0", regObj.PID.CardNO.Remove(0, 2));
            try
            {
                intI = acManager._ExcuteSQL(strSql);
            }
            catch (Exception objEx)
            {
                errMsg = objEx.Message;
                intI = -1;
                //t.RollBack();
            }
            if (intI < 0)
            {
                this.errMsg = "插入表MET_CAS_CASE失败，请确认是否要插入";
            }
            return intI;
        }
        #endregion

        #region 插入MET_CAS_CASE_RECORD 表
        /// <summary>
        /// 插入MET_CAS_CASE_RECORD信息
        /// </summary>
        /// <param name="regObj"></param>
        /// <returns>是否成功，true-成功，false-失败</returns>
        public int InsertMetCasCaseRecord(FS.HISFC.Models.Registration.Register regObj, FS.SOC.Local.HISWebService.PE.PEChargeService_Db acManager, string strCaseID)
        {
            int intI = 0;
            string strSql = @"insert into met_cas_case_record
                             (case_id, card_no, empi_id, oper_code, oper_date) 
                             values ('{0}', '{1}', '{2}', '{3}', '{4}');";
            strSql = string.Format(strSql, strCaseID, regObj.PID.CardNO, regObj.PID.CardNO, acManager.Operator.ID, acManager.GetSysDateTime());
            try
            {
                intI = acManager._ExcuteSQL(strSql);
            }
            catch (Exception objEx)
            {
                errMsg = objEx.Message;
                intI = -1;
                //t.RollBack();
            }
            if (intI < 0)
            {
                this.errMsg = "插入表MET_CAS_CASE_RECORD失败，请确认是否要插入";
            }
            return intI;
        }
        #endregion
    }
}
