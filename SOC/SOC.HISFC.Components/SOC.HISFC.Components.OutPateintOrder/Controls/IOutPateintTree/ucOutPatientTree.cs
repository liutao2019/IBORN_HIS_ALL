using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.HISFC.Components.OutPatientOrder.Controls.IOutPateintTree
{
    /// <summary>
    /// 门诊医生左侧患者列表
    /// </summary>
    public partial class ucOutPatientTree : UserControl, FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPateintTree
    {
        public ucOutPatientTree()
        {
            InitializeComponent();

            txtQuery.Enter += new EventHandler(txtQuery_Enter);

            txtQuery.KeyDown += new KeyEventHandler(txtQuery_KeyDown);
        }

        #region 变量

        #region 补挂号相关

        /// <summary>
        /// 隔日是否提示补收挂号费 0 不提示，不补收；1 提示是否补收；2 不提示，补收(HNMZ21)
        /// </summary>
        private int isAddRegFee_OtherDay = 1;

        /// <summary>
        /// 换医生是否提示补收挂号费 0 不提示，不补收；1 提示是否补收；2 不提示，补收 (HNMZ22)
        /// </summary>
        private int isAddRegFee_OtherDoct = 1;

        /// <summary>
        /// 是否允许用cardNo直接看诊(避免门诊账户情况下，部分医生在患者不在的情况下 自己开立扣费）
        /// </summary>
        private bool isAllowUserCardNoAdded = true;

        /// <summary>
        /// 不挂号是否允许看诊
        /// </summary>
        private bool isAllowNoRegSee = false;
        
        /// <summary>
        /// 是否允许补挂号
        /// </summary>
        private bool isAllowAddNewReg = false;

        /// <summary>
        /// 是否是免挂号费科室，免挂号费科室补挂号时，不提示是否收取挂号费
        /// </summary>
        private bool isNoSupplyRegDept = false;
        
        /// <summary>
        /// 是否普诊科室，普诊科室挂号级别始终是普诊
        /// </summary>
        bool isOrdinaryRegDept = false;

        #endregion

        /// <summary>
        /// 是否启用一个患者多重身份选择
        /// </summary>
        private bool isUserOnePersonMorePact = false;
        
        /// <summary>
        /// 是否启用一个患者多重身份选择
        /// </summary>
        public bool IsUserOnePersonMorePact
        {
            get
            {
                return isUserOnePersonMorePact;
            }
            set
            {
                isUserOnePersonMorePact = value;
            }
        }

        /// <summary>
        /// 挂号、退号有效天数
        /// 负数表示只查询当天挂号患者
        /// </summary>
        private decimal RegValidDays = 1;

        /// <summary>
        /// 当前患者
        /// </summary>
        private FS.HISFC.Models.Registration.Register myRegister = null;

        ///// <summary>
        ///// 查询挂号列表后操作接口
        ///// </summary>
        //private FS.HISFC.BizProcess.Interface.Order.IAfterQueryRegList IAfterQueryRegList = null;


        /// <summary>
        /// 登陆人员信息
        /// </summary>
        private FS.HISFC.Models.Base.Employee loadOper = (FS.HISFC.Models.Base.Employee)CacheManager.ConManager.Operator;

        #endregion

        #region 方法

        protected override void OnLoad(EventArgs e)
        {
            //开立医嘱允许的挂号有效天数
            this.RegValidDays = CacheManager.ContrlManager.GetControlParam<Decimal>("200022", false, 1);
            this.isAllowNoRegSee = CacheManager.ContrlManager.GetControlParam<bool>("200060", false, false);
            isAllowUserCardNoAdded = CacheManager.ContrlManager.GetControlParam<bool>("HNMZ20", false, true);
            isAddRegFee_OtherDay = CacheManager.ContrlManager.GetControlParam<int>("HNMZ21", false, 1);
            isAddRegFee_OtherDoct = CacheManager.ContrlManager.GetControlParam<int>("HNMZ22", false, 1);

            #region 是否允许补挂号

            isAllowAddNewReg = CacheManager.ContrlManager.GetControlParam<bool>("200030", false, false);

            if (isAllowAddNewReg)
            {
                //允许挂号的科室列表 同时和isAllowAddNewReg有关
                //如果科室列表为空，则所有科室都可以补挂号
                ArrayList alAllowAddRegDept = CacheManager.GetConList("AllowAddRegDept");
                if (alAllowAddRegDept != null && alAllowAddRegDept.Count > 0)
                {
                    isAllowAddNewReg = false;
                    foreach (FS.HISFC.Models.Base.Const conObj in alAllowAddRegDept)
                    {
                        if (conObj.ID.Trim() == this.GetReciptDept().ID)
                        {
                            isAllowAddNewReg = true;
                            break;
                        }
                    }
                }
            }

            #endregion

            #region 免挂号费科室

            ArrayList alNoSupplyRegDept = CacheManager.GetConList("NoSupplyRegDept");
            if (alNoSupplyRegDept == null)
            {
                MessageBox.Show("获取免挂号科室列表出错！\r\n" + CacheManager.ConManager.Err);
            }
            foreach (FS.HISFC.Models.Base.Const obj in alNoSupplyRegDept)
            {
                if (obj.IsValid && obj.ID == loadOper.Dept.ID)
                {
                    isNoSupplyRegDept = true;
                }
            }
            #endregion

            #region 普诊挂号科室

            ArrayList alOrdinaryRegDept = CacheManager.GetConList("OrdinaryRegLevlDept");
            if (alOrdinaryRegDept == null)
            {
                MessageBox.Show("获取普诊挂号科室失败！\r\n" + CacheManager.ConManager.Err);
                //return null;
            }

            foreach (FS.HISFC.Models.Base.Const constObj in alOrdinaryRegDept)
            {
                if (constObj.IsValid && constObj.ID.Trim() == this.GetReciptDept().ID)
                {
                    isOrdinaryRegDept = true;
                    break;
                }
            }

            #endregion

            ////考虑到妇幼特殊需求，此处增加了接口处理
            //if (IAfterQueryRegList == null)
            //{
            //    IAfterQueryRegList = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Order.IAfterQueryRegList)) as FS.HISFC.BizProcess.Interface.Order.IAfterQueryRegList;
            //}

            base.OnLoad(e);
        }

        /// <summary>
        /// 清空信息
        /// </summary>
        private void Clear()
        {
            this.txtQuery.Text = "";
            this.txtQuery.Focus();
            myRegister = null;
        }

        /// <summary>
        /// 查询患者挂号信息
        /// </summary>
        /// <returns>1:查询到有效挂号记录 0:没有有效挂号记录 -1:出错</returns>
        protected int Query(string cardNo)
        {
            DateTime dtQueryBegin = CacheManager.ConManager.GetDateTimeFromSysDateTime();
            if (RegValidDays <= 0)
            {
                dtQueryBegin = dtQueryBegin.Date;
            }
            else
            {
                dtQueryBegin = dtQueryBegin.AddDays(0 - (double)this.RegValidDays);
            }

            try
            {
                #region 根据患者主索引接口获取门诊病历号
                FS.HISFC.Models.Account.AccountCard accountCardObj = new FS.HISFC.Models.Account.AccountCard();
                if (CacheManager.FeeIntegrate.ValidMarkNO(cardNo, ref accountCardObj) <= 0)
                {
                    MessageBox.Show(CacheManager.FeeIntegrate.Err);
                    return -1;
                }

                if (!isAllowUserCardNoAdded)
                {
                    if (cardNo.PadLeft(10, '0') == accountCardObj.Patient.PID.CardNO)
                    {
                        MessageBox.Show("只能通过刷卡开立！\r\n如有疑问请联系信息科！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return 1;
                    }
                }

                this.txtQuery.Text = accountCardObj.Patient.PID.CardNO;

                #endregion

                #region 查询有效的挂号记录

                //全部的挂号记录
                ArrayList alRegAll = new ArrayList();
                //有效的挂号记录
                Dictionary<string, FS.HISFC.Models.Registration.Register> dicRegValid = new Dictionary<string, FS.HISFC.Models.Registration.Register>();

                //新挂号
                if (this.cbxNewReg.Checked)
                {
                }
                else
                {
                    //查询有效看诊时间段内所有有效的挂号记录
                    alRegAll = CacheManager.RegInterMgr.Query(accountCardObj.Patient.PID.CardNO, dtQueryBegin);
                }

                ////考虑到妇幼特殊需求，此处增加了接口处理
                //if (IAfterQueryRegList != null)
                //{
                //    if (IAfterQueryRegList.OnAfterQueryRegList(alReg, this.reciptDept) == -1)
                //    {
                //        MessageBox.Show(IAfterQueryRegList.ErrInfo, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //        return -1;
                //    }
                //}

                if (alRegAll == null)
                {
                    MessageBox.Show("查询挂号信息失败！\r\n" + CacheManager.RegInterMgr.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return -1;
                }

                #region 未查到有效的挂号记录

                else if (alRegAll.Count == 0)
                {
                }
                #endregion
                else
                {
                    //1、挂号有效期内，未看诊
                    //2、挂号有效期内，已看诊，看诊医生是当前医生


                    FS.HISFC.Models.Registration.Register regObj = null;
                    for (int i = 0; i < alRegAll.Count; i++)
                    {
                        regObj = alRegAll[i] as FS.HISFC.Models.Registration.Register;

                        //判断是否本科室留观的患者
                        if (regObj.PVisit.InState.ID.ToString() != "N")
                        {
                            if (regObj.SeeDoct.Dept.ID == this.GetReciptDept().ID)
                            {
                                MessageBox.Show("该患者已在本科室留观！");
                                return 1;
                            }
                            else
                            {
                                continue;
                            }
                        }

                        #region 门诊医生要判断看诊科室和看诊医生

                        //已看诊，看诊医生不一致
                        if (regObj.IsSee
                            && regObj.SeeDoct.ID != loadOper.ID)
                        {
                            if (this.isAddRegFee_OtherDoct == 0)
                            {
                                continue;
                            }
                        }
                        //已看诊，看诊日期不一致
                        else if (regObj.IsSee
                            && (regObj.SeeDoct.ID == loadOper.ID)
                            && regObj.DoctorInfo.SeeDate.Date != CacheManager.ConManager.GetDateTimeFromSysDateTime().Date)
                        {
                            if (isAddRegFee_OtherDay == 0)
                            {
                                continue;
                            }
                        }
                        #endregion

                        dicRegValid.Add(regObj.ID, regObj);
                    }
                }

                //未找到有效挂号记录，系统补挂号
                if (dicRegValid.Count <= 0)
                {
                    #region 查不到挂号记录时，可以自动挂号

                    if (this.isAllowAddNewReg
                        && isAllowNoRegSee)
                    {
                        FS.HISFC.Models.Registration.Register regObj = this.GetRegInfoFromPatientInfo(accountCardObj.Patient.PID.CardNO);
                        if (regObj == null)
                        {
                            return -1;
                        }
                        this.myRegister = regObj;
                        //return 1;
                    }
                    else
                    {
                        MessageBox.Show("没有查找到该患者在有效时间内的挂号信息!", "警告");
                        this.txtQuery.Focus();
                        txtQuery.SelectAll();
                        return -1;
                    }
                    #endregion
                }
                else if (dicRegValid.Count == 1)
                {
                    this.myRegister = dicRegValid.Values.First<FS.HISFC.Models.Registration.Register>();
                }
                else
                {
                    this.myRegister = this.SelectPatient(dicRegValid);
                }
                #endregion

                if (myRegister == null)
                {
                    return -1;
                }
                if (this.CheckRegInfo(ref myRegister) == -1)
                {
                    return -1;
                }

                this.AddNewRegToTree();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Clear();
                return -1;
            }

            return 1;
        }

        #region 获取开立科室

        /// <summary>
        /// 开立科室
        /// </summary>
        private FS.FrameWork.Models.NeuObject reciptDept = null;

        /// <summary>
        /// 获取开方科室
        /// </summary>
        /// <returns></returns>
        public FS.FrameWork.Models.NeuObject GetReciptDept()
        {
            try
            {
                if (this.reciptDept == null)
                {
                    //FS.HISFC.Models.Registration.Schema schema = BizManager.RegInterMgr.GetSchema(loadOper.ID, BizManager.ConManager.GetDateTimeFromSysDateTime());
                    //if (schema != null && schema.Templet.Dept.ID != "")
                    //{
                    //    this.reciptDept = schema.Templet.Dept.Clone();
                    //}
                    ////没有排版取登陆科室作为开立科室
                    //else
                    //{
                    this.reciptDept = loadOper.Dept.Clone();
                    //}
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            return this.reciptDept;
        }

        #endregion

        #region 补挂号

        /// <summary>
        /// 有多个挂号记录时，选择患者
        /// </summary>
        private FS.HISFC.Models.Registration.Register SelectPatient(Dictionary<string, FS.HISFC.Models.Registration.Register> dicRegList)
        {

            try
            {
                ArrayList alPatientList = new ArrayList();

                foreach (string key in dicRegList.Keys)
                {
                    FS.HISFC.Models.Registration.Register regObj = dicRegList[key];

                    FS.HISFC.Models.Base.Spell patientObj = new FS.HISFC.Models.Base.Spell();
                    patientObj.ID = regObj.ID;
                    patientObj.Name = regObj.Name;
                    patientObj.Memo = regObj.Pact.Name;

                    if (!string.IsNullOrEmpty(regObj.DoctorInfo.Templet.Dept.ID))
                    {
                        patientObj.SpellCode = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(regObj.DoctorInfo.Templet.Dept.ID);
                    }
                    if (!string.IsNullOrEmpty(regObj.DoctorInfo.Templet.Doct.ID))
                    {
                        patientObj.WBCode = SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(regObj.DoctorInfo.Templet.Doct.ID);
                    }

                    patientObj.UserCode = regObj.DoctorInfo.SeeDate.ToString();
                    alPatientList.Add(patientObj);
                }

                FS.FrameWork.Models.NeuObject selectPatient = null;
                if (alPatientList.Count > 0)
                {
                    if (FrameWork.WinForms.Classes.Function.ChooseItem(alPatientList,
                        new string[] { "门诊流水号", "姓名", "合同单位", "挂号科室", "挂号医生", "挂号时间" },
                        new bool[] { false, true, true, true, true, true },
                        new int[] { 50, 50, 70, 100, 70, 160 },
                        ref selectPatient) != 1)
                    {
                        return null;
                    }
                }

                FS.HISFC.Models.Registration.Register reg = dicRegList[selectPatient.ID];

                return reg;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return null;
            }
        }

        /// <summary>
        /// 根据基本信息获取挂号信息
        /// </summary>
        /// <param name="cardNO">患者卡号</param>
        /// <returns>挂号实体</returns>
        private FS.HISFC.Models.Registration.Register GetRegInfoFromPatientInfo(string cardNO)
        {
            FS.HISFC.Models.RADT.PatientInfo patientInfo = CacheManager.InterMgr.QueryComPatientInfo(cardNO);
            if (patientInfo == null)
            {
                MessageBox.Show("查询患者基本信息出错！如果患者是初诊，请到挂号处办卡挂号！\r\n" + CacheManager.InterMgr.Err, "警告", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }

            FS.HISFC.Models.Registration.Register regObj = new FS.HISFC.Models.Registration.Register();

            FS.HISFC.Models.Base.Employee oper = CacheManager.InterMgr.GetEmployeeInfo(loadOper.ID);

            DateTime dtNow = CacheManager.ConManager.GetDateTimeFromSysDateTime();
            try
            {
                //系统补挂号患者，流水号为新号
                //根据regObj.IsFee判断是否是补挂号
                regObj.ID = CacheManager.ConManager.GetSequence("Registration.Register.ClinicID");
                regObj.TranType = FS.HISFC.Models.Base.TransTypes.Positive;//正交易
                regObj.PID = patientInfo.PID;

                //根据时间段判断是否急诊
                //regObj.DoctorInfo.Templet.RegLevel.IsEmergency = (this.cmbRegLevel.SelectedItem as FS.HISFC.Models.Registration.RegLevel).IsEmergency;

                regObj.DoctorInfo.Templet.Dept.ID = loadOper.Dept.ID;
                regObj.DoctorInfo.Templet.Dept.Name = loadOper.Dept.Name;
                regObj.DoctorInfo.Templet.Doct.ID = loadOper.ID;
                regObj.DoctorInfo.Templet.Doct.Name = loadOper.Name;

                regObj.Name = patientInfo.Name;//患者姓名
                regObj.Sex = patientInfo.Sex;//性别
                regObj.Birthday = patientInfo.Birthday;//出生日期			

                regObj.RegType = FS.HISFC.Models.Base.EnumRegType.Reg;

                regObj.InputOper.ID = loadOper.ID;
                regObj.InputOper.OperTime = dtNow;
                regObj.DoctorInfo.SeeDate = dtNow;
                regObj.SeeDoct.ID = loadOper.ID;
                regObj.SeeDoct.Dept.ID = loadOper.Dept.ID;
                regObj.DoctorInfo.Templet.Begin = dtNow;
                regObj.DoctorInfo.Templet.End = dtNow;

                #region 午别
                if (regObj.DoctorInfo.SeeDate.Hour < 12 && regObj.DoctorInfo.SeeDate.Hour > 6)
                {
                    //上午
                    regObj.DoctorInfo.Templet.Noon.ID = "1";
                }
                else if (regObj.DoctorInfo.SeeDate.Hour > 12 && regObj.DoctorInfo.SeeDate.Hour < 18)
                {
                    //下午
                    regObj.DoctorInfo.Templet.Noon.ID = "2";
                }
                else
                {
                    //晚上
                    regObj.DoctorInfo.Templet.Noon.ID = "3";
                }
                #endregion

                //对于专家扣限额 先不处理


                //合同单位根据办卡记录获取，具体待提取方法
                regObj.Pact = patientInfo.Pact;
                if (string.IsNullOrEmpty(regObj.Pact.ID))
                {
                    regObj.Pact.ID = "1";
                    regObj.Pact.Name = "普通";
                    regObj.Pact.PayKind.ID = "01";
                    regObj.Pact.PayKind.Name = "自费";
                }

                #region 全天自费处理

                ArrayList alOwnFeeRegDept = CacheManager.ConManager.GetList("OwnFeeRegDept");
                if (alOwnFeeRegDept == null)
                {
                    MessageBox.Show("获取自费挂号科室失败！\r\n" + CacheManager.ConManager.Err);
                    return null;
                }

                foreach (FS.HISFC.Models.Base.Const constObj in alOwnFeeRegDept)
                {
                    if (constObj.IsValid && constObj.ID.Trim() == this.GetReciptDept().ID)
                    {
                        ArrayList alOwnFeeRegLevl = CacheManager.ConManager.GetList("OwnFeeRegLevl");
                        if (alOwnFeeRegLevl == null 
                            || alOwnFeeRegLevl.Count == 0)
                        {
                            MessageBox.Show("获取自费挂号级别失败！\r\n" + CacheManager.ConManager.Err);
                            return null;
                        }

                        foreach (FS.HISFC.Models.Base.Const obj in alOwnFeeRegLevl)
                        {
                            if (obj.IsValid)
                            {
                                regObj.Pact.ID = obj.ID;
                                regObj.Pact.Name = "普通";
                                regObj.Pact.PayKind.ID = "01";
                                regObj.Pact.PayKind.Name = "自费";
                                break;
                            }
                        }

                        break;
                    }
                }
                #endregion

                #region 挂号级别

                string regLevl = "";

                isOrdinaryRegDept = false;

                #region 普诊挂号科室
                ArrayList alOrdinaryRegDept = CacheManager.ConManager.GetList("OrdinaryRegLevlDept");
                if (alOrdinaryRegDept == null)
                {
                    MessageBox.Show("获取普诊挂号科室失败！\r\n" + CacheManager.ConManager.Err);
                    return null;
                }

                foreach (FS.HISFC.Models.Base.Const constObj in alOrdinaryRegDept)
                {
                    if (constObj.IsValid && constObj.ID.Trim() == this.GetReciptDept().ID)
                    {
                        isOrdinaryRegDept = true;
                        break;
                    }
                }

                #endregion

                //普诊
                if (isOrdinaryRegDept)
                {
                    ArrayList alOrdinaryLevl = CacheManager.ConManager.GetList("OrdinaryRegLevel");
                    if (alOrdinaryLevl == null || alOrdinaryLevl.Count == 0)
                    {
                        MessageBox.Show("获取普通门诊对应的挂号级别错误！\r\n" + CacheManager.ConManager.Err);
                        return null;
                    }

                    foreach (FS.HISFC.Models.Base.Const constObj in alOrdinaryLevl)
                    {
                        if (constObj.IsValid)
                        {
                            regLevl = constObj.ID.Trim();
                            break;
                        }
                    }
                }
                else
                {
                    //是否急诊
                    bool isEmerg = CacheManager.RegInterMgr.IsEmergency(this.GetReciptDept().ID);

                    string diagItemCode = "";
                    if (isEmerg)
                    {
                        regObj.DoctorInfo.Templet.RegLevel.IsEmergency = true;

                        regLevl = SOC.HISFC.BizProcess.Cache.Fee.GetEmergRegLevl() == null ? "" : SOC.HISFC.BizProcess.Cache.Fee.GetEmergRegLevl().ID;

                        if (string.IsNullOrEmpty(regLevl))
                        {
                            MessageBox.Show("获取挂号级别错误！\r\n原因:急诊挂号级别没有维护！\r\n如有问题请联系信息科！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        //MessageBox.Show("非急诊科室必须先挂号！");
                        //return null;
                        ///*
                        if (CacheManager.RegInterMgr.GetSupplyRegInfo(oper.ID, oper.Level.ID.ToString(), regObj.DoctorInfo.Templet.Dept.ID, ref regLevl, ref diagItemCode) == -1)
                        {
                            MessageBox.Show(CacheManager.RegInterMgr.Err);
                            return null;
                        }
                        //*/ 
                    }
                }

                FS.HISFC.Models.Registration.RegLevel regLevlObj = null;
                regLevlObj = SOC.HISFC.BizProcess.Cache.Fee.GetRegLevl(regLevl) as FS.HISFC.Models.Registration.RegLevel;

                if (regLevlObj == null)
                {
                    regLevlObj = CacheManager.RegInterMgr.QueryRegLevelByCode(regLevl);
                    if (regLevlObj == null)
                    {
                        MessageBox.Show("查询挂号级别错误，编码[" + regLevl + "]！请联系信息科重新维护" + CacheManager.RegInterMgr.Err);
                        return null;
                    }
                }

                regObj.DoctorInfo.Templet.RegLevel = regLevlObj;
                #endregion

                regObj.SSN = patientInfo.SSN;//医疗证号

                regObj.PhoneHome = patientInfo.PhoneHome;//联系电话
                regObj.AddressHome = patientInfo.AddressHome;//联系地址
                regObj.CardType = patientInfo.IDCardType; //证件类型

                regObj.IsFee = false;
                regObj.Status = FS.HISFC.Models.Base.EnumRegisterStatus.Valid;
                //之前为什么改为true呢？？
                regObj.IsSee = false;
                regObj.CancelOper.ID = "";
                regObj.CancelOper.OperTime = DateTime.MinValue;
                regObj.IDCard = patientInfo.IDCard;

                regObj.PVisit.InState.ID = "N";
                regObj.DoctorInfo.SeeNO = -1;

                //加密处理
                if (patientInfo.IsEncrypt)
                {
                    regObj.IsEncrypt = true;
                    regObj.NormalName = FS.FrameWork.WinForms.Classes.Function.Encrypt3DES(patientInfo.Name);
                    regObj.Name = "******";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }

            if (isUserOnePersonMorePact)
            {
                if (CacheManager.AccountMgr.GetPatientPactInfo(regObj) == -1)
                {
                    MessageBox.Show("获取患者合同单位信息失败：" + CacheManager.AccountMgr.Err);
                    return null;
                }

                if (regObj.MutiPactInfo.Count > 1)
                {
                    FS.FrameWork.Models.NeuObject pactObj = new FS.FrameWork.Models.NeuObject();
                    if (regObj.MutiPactInfo.Count > 0)
                    {
                        if (FrameWork.WinForms.Classes.Function.ChooseItem(new ArrayList(regObj.MutiPactInfo), new string[] { "合同单位编码", "合同单位", "有效性" }, new bool[] { false, true, true, false, false, false }, new int[] { 50, 100, 70 }, ref pactObj) != 1)
                        {
                            return null;
                        }
                    }

                    if (pactObj != null && !string.IsNullOrEmpty(pactObj.ID))
                    {
                        regObj.Pact = pactObj as FS.HISFC.Models.Base.PactInfo;
                    }
                }

                if (this.cbxNewReg.Checked)
                {
                    if (MessageBox.Show("该患者将以[" + regObj.Pact.Name + "]的身份重新补挂号，是否重新补收挂号费？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
                    {
                        regObj.Memo = "不补收";
                    }
                }

                this.cbxNewReg.Checked = false;
            }

            return regObj;
        }

        private void AddNewRegToTree()
        {
            //if (this.Register == null)
            //{
            //    MessageBox.Show("不能查询到患者在有效时间内的有效信息！");
            //    return;
            //}

            ////if (IOutPateintTree != null)
            ////{
            ////    if (IOutPateintTree.BeforeAddToTree(ucQuerySeeNoByCardNo1.Register) <= 0)
            ////    {
            ////        if (!string.IsNullOrEmpty(IOutPateintTree.Err))
            ////        {
            ////            MessageBox.Show(IOutPateintTree.Err);
            ////        }
            ////        return;
            ////    }
            ////}

            //FS.HISFC.Models.Registration.Register regObj = new FS.HISFC.Models.Registration.Register();
            //regObj = this.Register;
            //FS.HISFC.Models.Registration.Register regtmp = null;

            ////已看诊，看诊医生一致
            ////对于允许一次挂号多次看诊的，加载到未看诊列表 houwb
            //if (regObj.IsSee && regObj.SeeDoct.ID == this.myQueue.Operator.ID)
            //{
            //    if (this.linkLabel1.Text == "已诊")
            //    {
            //        PatientStateConvert();
            //    }

            //    //是否在已诊列表找到此患者
            //    bool isFind = false;

            //    foreach (TreeNode node in this.neuTreeView2.Nodes[0].Nodes)
            //    {
            //        if (node.Tag != null)
            //        {
            //            regtmp = node.Tag as FS.HISFC.Models.Registration.Register;

            //            //判断是否是补挂号患者，补挂号患者只判断病历号相同
            //            if (regtmp.IsFee)
            //            {
            //                if (regObj.ID == regtmp.ID)
            //                {
            //                    this.neuTreeView2.SelectedNode = node;
            //                    isFind = true;
            //                    return;
            //                }
            //            }
            //            //补挂号患者
            //            else
            //            {
            //                if (regObj.PID.CardNO == regtmp.PID.CardNO)
            //                {
            //                    this.neuTreeView2.SelectedNode = node;
            //                    isFind = true;
            //                    //MessageBox.Show("该患者已经在列表中！");
            //                    return;
            //                }
            //            }
            //        }
            //    }

            //    //保证患者挂号有效期内，即使不在已诊列表也能加载患者看诊
            //    if (!isFind)
            //    {
            //        if (this.linkLabel1.Text == "待诊")
            //        {
            //            PatientStateConvert();
            //        }
            //    }
            //}
            //else
            //{
            //    if (this.linkLabel1.Text == "待诊")
            //    {
            //        PatientStateConvert();
            //    }

            //    foreach (TreeNode node in this.neuTreeView1.Nodes[0].Nodes)
            //    {
            //        if (node.Tag != null)
            //        {
            //            regtmp = node.Tag as FS.HISFC.Models.Registration.Register;

            //            //判断是否是补挂号患者，补挂号患者只判断病历号相同
            //            if (regtmp.IsFee)
            //            {
            //                if (regObj.ID == regtmp.ID)
            //                {
            //                    ///MessageBox.Show("该患者已经在列表中！");
            //                    this.neuTreeView1.SelectedNode = node;
            //                    return;
            //                }
            //            }
            //            //补挂号患者
            //            else
            //            {
            //                if (regObj.PID.CardNO == regtmp.PID.CardNO)
            //                {
            //                    this.neuTreeView1.SelectedNode = node;
            //                    this.neuTreeView1.SelectedNode.Tag = regObj;
            //                    if (neuTreeView1.SelectedNode.Level != 2)
            //                    {
            //                        regObj.DoctorInfo.SeeNO = -1;
            //                        neuTreeView1.SelectedNode.Tag = regObj;
            //                    }
            //                    DoTreeDoubleClick(false);
            //                    return;
            //                }
            //            }
            //        }
            //    }
            //    //留观判断
            //    if (this.neuTreeView1.Nodes.Count > 1)
            //    {
            //        foreach (TreeNode node in this.neuTreeView1.Nodes[1].Nodes)
            //        {
            //            if (node.Tag != null)
            //            {
            //                regtmp = node.Tag as FS.HISFC.Models.Registration.Register;

            //                if (regObj.PID.CardNO == regtmp.PID.CardNO)
            //                {
            //                    this.neuTreeView1.SelectedNode = node;
            //                    return;
            //                }
            //            }
            //        }
            //    }
            //}

            //if (this.isAccountMode)
            //{
            //    #region 判断账户余额
            //    decimal vacancy = 0;
            //    FS.HISFC.BizLogic.Fee.Account accountMgr = new FS.HISFC.BizLogic.Fee.Account();
            //    int rev = accountMgr.GetVacancy(regObj.PID.CardNO, ref vacancy);
            //    if (rev == -1)
            //    {
            //        MessageBox.Show("获取账户余额出错：" + accountMgr.Err);
            //        return;
            //    }
            //    //没有账户或账户停用
            //    else if (rev == 0)
            //    {
            //        if (MessageBox.Show("该患者不存在账户，是否继续看诊？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            //        {
            //            return;
            //        }
            //    }
            //    else
            //    {
            //        if (vacancy < 4 && regObj.Memo != "不补收")
            //        {
            //            if (MessageBox.Show("当前账户余额为" + vacancy.ToString() + "元，不足4元，是否继续看诊？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            //            {
            //                return;
            //            }
            //        }
            //    }
            //    #endregion
            //}

            //AddPatientToTree(regObj);
        }

        /// <summary>
        /// 是否补收诊金
        /// </summary>
        /// <param name="index">0 不提示，不补收(无效挂号）,不会补挂号；1 提示是否补收；2 不提示，补挂号</param>
        /// <param name="regObj"></param>
        /// <returns>-1 错误;0 正常返回（未补收挂号费）;1 正常返回，需补收挂号费</returns>
        private int CheckRegInfo(ref FS.HISFC.Models.Registration.Register regObj)
        {
            if (!regObj.IsSee)
            {
                return 0;
            }

            //是否补挂号
            bool isAddReg = false;

            //是否需要补收挂号费
            bool isSupplyRegFee = true;

            #region 看诊医生不同

            //科室、医生不一样 提示是否补收挂号费
            if (!regObj.IsSee
                && (regObj.SeeDoct.ID != this.loadOper.ID
                || regObj.SeeDoct.Dept.ID != loadOper.Dept.ID)
                )
            {
                if (isAddRegFee_OtherDoct == 0)
                {
                    isAddReg = false;
                    return -1;
                }
                else if (isAddRegFee_OtherDoct == 1)
                {
                    if (MessageBox.Show("该患者已经看诊，看诊科室为[" + SOC.HISFC.BizProcess.Cache.Common.GetDeptName(regObj.SeeDoct.Dept.ID) + "],\r\n\r\n看诊医生为[" + SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(regObj.SeeDoct.ID) + "]\r\n\r\n看诊时间为：" + regObj.DoctorInfo.SeeDate.ToString("yyyy-MM-dd HH:mm:ss") + "\r\n\r\n是否重新挂号？(不再收取挂号费！)", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        isAddReg = true;
                    }
                    else
                    {
                        isAddReg = false; 
                    }
                }
                else
                {
                    isAddReg = true;
                }
            }
            #endregion

            #region 看诊日期不同

            if (!isAddReg)
            {
                //看诊日期不一样提示是否补收挂号费
                if ((regObj.SeeDoct.ID == this.loadOper.ID)
                    && regObj.DoctorInfo.SeeDate.Date != CacheManager.ConManager.GetDateTimeFromSysDateTime().Date)
                {
                    if (this.isAddRegFee_OtherDay == 0)
                    {
                        isAddReg = false;
                        return -1;
                    }
                    else if (isAddRegFee_OtherDay == 1)
                    {
                        if (MessageBox.Show("该患者已经看诊，上次看诊医生为[" + SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(regObj.SeeDoct.ID) + "],\r\n\r\n看诊时间为[" + regObj.DoctorInfo.SeeDate.ToString("yyyy-MM-dd HH:mm:ss") + "]\n\r\n是否重新挂号？(不再收取挂号费！)", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            isAddReg = true;
                        }
                        else
                        {
                            isAddReg = false;
                            isSupplyRegFee = false;
                        }
                    }
                    else
                    {
                        isAddReg = true;
                    }
                }
            }
            #endregion

            if (isAddReg)
            {
                //如果补收挂号费的话，需要修改患者状态等信息
                regObj = this.GetRegInfoFromPatientInfo(regObj.PID.CardNO);
                if (regObj == null)
                {
                    return -1;
                }
            }

            if (isSupplyRegFee
                && isNoSupplyRegDept)
            {
                isSupplyRegFee = false;
            }

            return 1;
        }

        #endregion

        #endregion

        #region 事件

        void txtQuery_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //先把全角转换为半角,避免把符号误认为汉字
                this.txtQuery.Text = FS.FrameWork.Function.NConvert.ToDBC(this.txtQuery.Text.Trim());

                string cardNO = this.txtQuery.Text.Trim();

                #region 补挂号

                if (isAllowAddNewReg &&
                    (cardNO.StartsWith("/") || cardNO.StartsWith("+")))
                {
                    #region 补挂号

                    DialogResult reult = MessageBox.Show("是否开始快速补挂号流程？\r\n\r\n提示：未经过挂号处的补挂号可能会导致漏收挂号费！", "询问", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,MessageBoxDefaultButton.Button1);

                    if (reult == DialogResult.Yes)
                    {
                        string name = cardNO.Remove(0, 1);
                        frmRegistrationByDoctor frmDoctRegistration = new frmRegistrationByDoctor(name);
                        frmDoctRegistration.IsOrdinaryRegDept = this.isOrdinaryRegDept;

                        frmDoctRegistration.ShowDialog();
                        if (frmDoctRegistration.DialogResult == DialogResult.Cancel)
                        {
                            return;
                        }

                        this.myRegister = frmDoctRegistration.Register;

                        if (string.IsNullOrEmpty(this.myRegister.ID)
                            || this.myRegister.ID == null)
                        {
                            this.Clear();
                        }
                        else
                        {
                            this.txtQuery.Text = myRegister.PID.CardNO;

                            //存在一个患者多种身份的时候，弹出界面让医生选择患者的身份
                            if (isUserOnePersonMorePact)
                            {
                                if (CacheManager.AccountMgr.GetPatientPactInfo(myRegister) == -1)
                                {
                                    MessageBox.Show("获取患者合同单位信息失败！\r\n" + CacheManager.AccountMgr.Err);
                                    return;
                                }

                                FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                                if (myRegister.MutiPactInfo.Count > 0)
                                {
                                    if (FrameWork.WinForms.Classes.Function.ChooseItem(
                                        new ArrayList(myRegister.MutiPactInfo), 
                                        new string[] { "合同单位编码", "合同单位", "有效性" }, 
                                        new bool[] { }, new int[] { 0, 100, 70 }, ref obj) != 1)
                                    {
                                        return;
                                    }
                                }
                                myRegister.Pact = obj as FS.HISFC.Models.Base.PactInfo;
                            }
                            this.AddNewRegToTree();
                        }
                    }
                    else
                    {
                        this.Clear();
                    }
                    #endregion
                }
                #endregion
                else
                {
                    #region 汉字开头默认是查询患者

                    if (this.txtQuery.Text.Length >= 2 
                        && System.Text.Encoding.Default.GetBytes(this.txtQuery.Text.Substring(0, 1)).Length > 1)
                    {


                        ArrayList alPatient = CacheManager.InPatientMgr.QueryPatientByName(this.txtQuery.Text.Trim());
                        if (alPatient == null)
                        {
                            MessageBox.Show("查找姓名为[" + txtQuery.Text + "]的患者失败！\r\n" + CacheManager.InPatientMgr.Err, "错误", MessageBoxButtons.OK);
                            return;
                        }
                        else if (alPatient.Count <= 0)
                        {
                            MessageBox.Show("没有找到姓名为[" + this.txtQuery.Text + "]的患者!", "提示", MessageBoxButtons.OK);
                            return;
                        }

                        ArrayList alPatientList = new ArrayList();
                        foreach (FS.HISFC.Models.RADT.PatientInfo patientObj in alPatient)
                        {
                            FS.HISFC.Models.Base.Spell obj = new FS.HISFC.Models.Base.Spell();
                            
                            obj.ID = patientObj.PID.CardNO;
                            obj.Name = patientObj.Name;
                            obj.Memo = patientObj.Sex.Name;
                            obj.SpellCode = patientObj.IDCard;
                            obj.WBCode = patientObj.PhoneHome;
                            obj.UserCode = patientObj.AddressHome;

                            alPatientList.Add(patientObj);
                        }

                        FS.FrameWork.Models.NeuObject selectPatient = null;
                        if (alPatientList.Count > 0)
                        {
                            if (FrameWork.WinForms.Classes.Function.ChooseItem(alPatientList,
                                new string[] { "门诊卡号", "姓名", "性别", "身份证号", "电话", "地址" },
                                new bool[] { false, true, true, true, true, true },
                                new int[] { 50, 50, 30, 80, 80, 200 },
                                ref selectPatient) != 1)
                            {
                            }
                            else
                            {
                                this.txtQuery.Text = selectPatient.ID;
                            }
                        }
                    }
                    #endregion

                    cardNO = this.txtQuery.Text.Trim();
                    this.Query(cardNO);
                }
            }
        }

        void txtQuery_Enter(object sender, EventArgs e)
        {
            try
            {
                foreach (InputLanguage input in InputLanguage.InstalledInputLanguages)
                {
                    if (input.LayoutName == "美式键盘" 
                        || input.LayoutName == "中文(简体) - 美式键盘")
                    {
                        InputLanguage.CurrentInputLanguage = input;
                    }
                }

                if (this.txtQuery.Text.Length >= 2 
                    && System.Text.Encoding.Default.GetBytes(this.txtQuery.Text.Substring(0, 1)).Length > 1)
                {
                    this.txtQuery.Text = "";
                }
            }
            catch(Exception ex)
            {

            }
        }

        #endregion

        #region IOutPateintTree 成员

        public int Call()
        {
            throw new NotImplementedException();
        }

        public int CancelTriage()
        {
            throw new NotImplementedException();
        }

        public int DelayCall()
        {
            throw new NotImplementedException();
        }

        public int Init()
        {
            throw new NotImplementedException();
        }

        public new int Refresh()
        {
            throw new NotImplementedException();
        }

        public FS.HISFC.Models.Registration.Register Register
        {
            get
            {
                return myRegister;
            }
            set
            {
                myRegister = value;
            }
        }

        public int Triage()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IOutPateintTree 成员

        public int AfterAddToTree(FS.HISFC.Models.Registration.Register regObj)
        {
            throw new NotImplementedException();
        }

        public int BeforeAddToTree(FS.HISFC.Models.Registration.Register regObj)
        {
            throw new NotImplementedException();
        }

        public string Err
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion
    }
}
