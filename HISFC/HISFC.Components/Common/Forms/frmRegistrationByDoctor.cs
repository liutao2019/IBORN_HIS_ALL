using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.HISFC.Components.Common.Classes;

namespace FS.HISFC.Components.Common.Forms
{
    public partial class frmRegistrationByDoctor : FS.FrameWork.WinForms.Forms.BaseForm
    {
        public frmRegistrationByDoctor(string patientName) 
        {
            InitializeComponent();
            this.txtName.Text = patientName;
        }

        #region 变量

        /// <summary>
        /// 自动生成的卡号
        /// </summary>
        protected string autoCardNO = string.Empty;

        /// <summary>
        /// 自助挂号相关接口
        /// </summary>
        public FS.HISFC.BizProcess.Interface.Order.IAfterQueryRegList IAfterQueryRegList = null;

        /// <summary>
        /// 门诊流水号
        /// </summary>
        protected string clinicNO = string.Empty;

        /// <summary>
        /// 没有挂号患者,卡号第一位标志,默认以9开头
        /// </summary>
        protected string noRegFlagChar = "9";

        /// <summary>
        /// 挂号信息实体
        /// </summary>
        protected FS.HISFC.Models.Registration.Register register = new FS.HISFC.Models.Registration.Register();

        /// <summary>
        /// 患者基本信息
        /// </summary>
        private FS.HISFC.Models.RADT.PatientInfo patientInfo = null;

        /// <summary>
        /// 门诊医嘱业务层
        /// </summary>
        protected FS.HISFC.BizLogic.Order.OutPatient.Order orderManagement = new FS.HISFC.BizLogic.Order.OutPatient.Order();
        /// <summary>
        /// 合同单位业务层
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Manager interMgr = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// 是否普诊科室，普诊科室挂号级别始终是普诊
        /// </summary>
        bool isOrdinaryRegDept = false;

        /// <summary>
        /// 费用业务层
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Fee feeManagement = new FS.HISFC.BizProcess.Integrate.Fee();

        /// <summary>
        /// 挂号业务层
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Registration.Registration regManagement = new FS.HISFC.BizProcess.Integrate.Registration.Registration();

        /// <summary>
        /// 控制参数业务层
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        /// <summary>
        /// 常数管理业务层
        /// </summary>
        private FS.HISFC.BizLogic.Manager.Constant conManager = new FS.HISFC.BizLogic.Manager.Constant();

        /// <summary>
        /// 操作员
        /// </summary>
        protected FS.HISFC.Models.Base.Employee employee = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;

        private FS.HISFC.BizLogic.RADT.InPatient radtMgr = new FS.HISFC.BizLogic.RADT.InPatient();

        /// <summary>
        /// 开立医生
        /// </summary>
        private FS.HISFC.Models.Base.Employee doct = null;

        /// <summary>
        /// 不允许自动挂号的合同单位
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper noAutoRegPactHelper = null;

        /// <summary>
        /// 挂号管理类
        /// </summary>
        private FS.HISFC.BizLogic.Registration.Register regMgr = new FS.HISFC.BizLogic.Registration.Register();
       

        #endregion

        #region 属性

        //{5D855B3C-5A4F-42c9-931D-333D0A01D809}
        /// <summary>
        /// 病种常数
        /// </summary>
        private const string CLASS1DESEASE = "CLASS1DESEASE";
        /// <summary>
        /// 一级病种
        /// </summary>
        private ArrayList Class1Desease = new ArrayList();

        /// <summary>
        /// 急诊挂号级别
        /// </summary>
        private string emergencyLevlCode;

        /// <summary>
        /// 急诊挂号级别
        /// </summary>
        public string EmergencyLevlCode
        {
            get
            {
                return emergencyLevlCode;
            }
            set
            {
                emergencyLevlCode = value;
            }
        }

        /// <summary>
        /// 患者挂号信息
        /// </summary>
        public FS.HISFC.Models.Registration.Register PatientInfo
        {
            get
            {
                return this.register;
            }
        }

        /// <summary>
        /// 挂号级别帮助类
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper regLevlHelper = null;

        /// <summary>
        /// 登陆人员
        /// </summary>
        private  FS.HISFC.Models.Base.Employee logEmpl = null;

        /// <summary>
        /// 登陆人员
        /// </summary>
        public  FS.HISFC.Models.Base.Employee LogEmpl
        {
            get
            {
                if (logEmpl == null)
                {
                    logEmpl = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator);
                }
                return logEmpl;
            }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 初始化
        /// </summary>
        private void InitControl()
        {
            //初始化控制参数 

            //{3F495C28-F952-43fb-8B19-C63DDAB2F749}
            if(ctlMgr.QueryControlerInfo("DZ0001")=="1")
            {
              IsdiseaseMust=true;
            }
            else
            {
            IsdiseaseMust=false;
            }
            //初始化合同单位
            ArrayList pactList = this.interMgr.QueryPactUnitOutPatient();
            if (pactList == null)
            {
                MessageBox.Show("初始化合同单位出错!" + this.interMgr.Err);

                return;
            }
            this.cmbPact.AddItems(pactList);

            //初始化性别
            this.cmbSex.AddItems(FS.HISFC.Models.Base.SexEnumService.List());

            //获得卡号前补位规则
            this.noRegFlagChar = this.controlParamIntegrate.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.Const.NO_REG_CARD_RULES, false, "9");

            this.autoCardNO = this.feeManagement.GetAutoCardNO();
            if (autoCardNO == string.Empty || autoCardNO == "" || autoCardNO == null)
            {
                MessageBox.Show("获得门诊卡号出错!" + this.feeManagement.Err);

                return;
            }
            //autoCardNO = this.noRegFlagChar + autoCardNO;
            autoCardNO = this.autoCardNO.PadLeft(10, '0');
            //this.txtCardNo.Text = this.autoCardNO;

            this.clinicNO = this.orderManagement.GetSequence("Registration.Register.ClinicID");
            if (clinicNO == string.Empty || clinicNO == "" || clinicNO == null)
            {
                MessageBox.Show("获得门诊就诊号出错!" + this.orderManagement.Err);

                return;
            }

            this.cmbSex.Tag = "M";

            this.cmbPact.Tag = "1";

            this.doct = this.interMgr.GetEmployeeInfo(this.employee.ID);
            if (this.doct == null)
            {
                MessageBox.Show(this.interMgr.Err);
            }

            this.lblTip.Text = "";

            if (noAutoRegPactHelper == null)
            {
                noAutoRegPactHelper = new FS.FrameWork.Public.ObjectHelper();
                noAutoRegPactHelper.ArrayObject = this.interMgr.GetConstantList("NoAutoRegPact");
            }

            #region 获取所有挂号级别
            if (regLevlHelper == null)
            {
                regLevlHelper = new FS.FrameWork.Public.ObjectHelper();

                //获取所有的挂号级别
                ArrayList al = regManagement.QueryAllRegLevel();

                regLevlHelper.ArrayObject = al;

                //有效的挂号级别
                ArrayList alValidReglevl = new ArrayList();

                if (al == null || al.Count == 0)
                {
                    MessageBox.Show("查询所有挂号级别错误！会导致补收挂号费错误!\r\n请联系信息科重新维护" + regManagement.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    bool isValidEmergency = true;
                    foreach (FS.HISFC.Models.Registration.RegLevel regLevl in al)
                    {
                        if (regLevl.IsValid)
                        {
                            alValidReglevl.Add(regLevl);

                            if (regLevl.IsEmergency)
                            {
                                emergencyLevlCode = regLevl.ID;
                                //break;
                            }
                        }
                        else if (regLevl.IsEmergency)
                        {
                            isValidEmergency = false;
                        }
                    }

                    this.cmbRegLevl.AddItems(alValidReglevl);
                }
            }
            #endregion

            this.SetEnabled(false);

            #region 设置是否允许首诊挂号

            btAutoCardNo.Visible = false;
            if (FrameWork.WinForms.Classes.Function.IsManager()
                //||这里要做控制参数设置了,交给后来人吧~
                )
            {
                btAutoCardNo.Visible = true;
            }

            #endregion

            //{5D855B3C-5A4F-42c9-931D-333D0A01D809}
            this.Class1Desease = this.interMgr.QueryConstantList(CLASS1DESEASE);

            if (Class1Desease != null)
            {
                ArrayList deptDesease = new ArrayList();
                HISFC.Models.Base.Employee oper = FrameWork.Management.Connection.Operator as HISFC.Models.Base.Employee;
                HISFC.Models.Base.Department dept = oper.Dept as HISFC.Models.Base.Department;
                foreach (FS.FrameWork.Models.NeuObject obj in this.Class1Desease)
                {
                    if (obj.Memo.Contains(dept.ID) || obj.Memo.Contains("ALL"))
                    {
                        deptDesease.Add(obj);
                    }
                }

                if (deptDesease.Count > 0)
                {
                    this.cmbClass1Desease.AddItems(deptDesease);
                }
            }

            this.cmbClass1Desease.SelectedIndexChanged += new EventHandler(cmbClass1Desease_SelectedIndexChanged);
        }

        //{5D855B3C-5A4F-42c9-931D-333D0A01D809}
        private void cmbClass1Desease_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.cmbClass2Desease.Items.Clear();
            string class1desease = this.cmbClass1Desease.Tag.ToString();
            if (string.IsNullOrEmpty(class1desease))
            {
                return;
            }
            string queryCode = CLASS1DESEASE + class1desease;

            ArrayList class2desease = this.interMgr.QueryConstantList(queryCode);
            if (class2desease == null)
            {
                return;
            }

            this.cmbClass2Desease.AddItems(class2desease);
        }

        /// <summary>
        /// 自动生成卡号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btAutoCardNo_Click(object sender, EventArgs e)
        {
            FS.HISFC.BizLogic.Registration.Register regMgr = new FS.HISFC.BizLogic.Registration.Register();

            this.autoCardNO = regMgr.AutoGetCardNO().ToString(); //this.feeManagement.GetAutoCardNO();
            if (autoCardNO == string.Empty || autoCardNO == "" || autoCardNO == null)
            {
                MessageBox.Show("获得门诊卡号出错!" + this.feeManagement.Err);
                return;
            }
            //autoCardNO = this.noRegFlagChar + autoCardNO;
            autoCardNO = this.autoCardNO.PadLeft(10, '0');
            this.txtCardNo.Text = this.autoCardNO;

            this.SetEnabled(true);
            this.txtName.Focus();
        }

        /// <summary>
        /// 设置患者信息
        /// </summary>
        /// <returns></returns>
        private int SetRegister()
        {
            DateTime now = this.orderManagement.GetDateTimeFromSysDateTime();
            this.register.ID = clinicNO;
            this.register.Name = this.txtName.Text.Trim();
            //this.register.Card.ID = autoCardNO;
            //this.register.PID.CardNO = autoCardNO;
            this.register.Card.ID = this.txtCardNo.Text;
            this.register.PID.CardNO = this.txtCardNo.Text;
            this.register.IDCard = this.txtIDCard.Text;

            if (this.register.PID.CardNO.Length < 10)
            {
                this.register.PID.CardNO.PadLeft(10, '0');
            }

            this.register.PhoneHome = this.txtPhone.Text;
            this.register.AddressHome = this.txtAddress.Text;

            #region 合同单位

            if (this.cmbPact.Tag == null || string.IsNullOrEmpty(this.cmbPact.Tag.ToString()))
            {
                MessageBox.Show("请选择合同单位！");
                return -1;
            }

            FS.HISFC.Models.Base.PactInfo pactObj = interMgr.GetPactUnitInfoByPactCode(this.cmbPact.Tag.ToString());
            if (pactObj == null)
            {
                MessageBox.Show("获取合同单位信息出错：" + interMgr.Err);
                return -1;
            }
            this.register.Pact = pactObj;
            #endregion


            this.register.Sex.ID = this.cmbSex.Tag.ToString();
            this.register.Birthday = this.dtPickerBirth.Value;
            this.register.DoctorInfo.SeeDate = now; 
            this.register.DoctorInfo.SeeNO = -1;
            this.register.DoctorInfo.Templet.Dept = this.employee.Dept;

            this.register.InputOper.ID = this.employee.ID;
            this.register.InputOper.OperTime = this.orderManagement.GetDateTimeFromSysDateTime();
            this.register.DoctorInfo.SeeDate = this.orderManagement.GetDateTimeFromSysDateTime();
            this.register.DoctorInfo.Templet.Begin = this.orderManagement.GetDateTimeFromSysDateTime();
            this.register.DoctorInfo.Templet.End = this.orderManagement.GetDateTimeFromSysDateTime();
            this.register.RegType = FS.HISFC.Models.Base.EnumRegType.Reg;

            #region 午别
            if (this.register.DoctorInfo.SeeDate.Hour < 12 && this.register.DoctorInfo.SeeDate.Hour > 6)
            {
                //上午
                this.register.DoctorInfo.Templet.Noon.ID = "1";
            }
            else if (this.register.DoctorInfo.SeeDate.Hour > 12 && this.register.DoctorInfo.SeeDate.Hour < 18)
            {
                //下午
                this.register.DoctorInfo.Templet.Noon.ID = "2";
            }
            else
            {
                //晚上
                this.register.DoctorInfo.Templet.Noon.ID = "3";
            }
            #endregion

            #region 挂号级别

            this.register.DoctorInfo.Templet.RegLevel = this.cmbRegLevl.SelectedItem as FS.HISFC.Models.Registration.RegLevel;

            #endregion

            this.register.IsFee = false;
            this.register.Status = FS.HISFC.Models.Base.EnumRegisterStatus.Valid;
            this.register.IsSee = false;
            this.register.PVisit.InState.ID = "N";

            //{5D855B3C-5A4F-42c9-931D-333D0A01D809}
            this.register.Class1Desease = this.cmbClass1Desease.Tag.ToString();
            this.register.Class2Desease = this.cmbClass2Desease.Tag.ToString();

            //{75ADC0C9-77FC-45ee-8E74-8CDDE328FA33}
            //院级初诊，大科级初诊，科级出诊，医生级初诊设置
            int isHospitalFirst = this.regMgr.IsFirstRegister("1", this.register.PID.CardNO, this.register.DoctorInfo.Templet.Dept.ID, this.register.DoctorInfo.Templet.Doct.ID, this.regMgr.GetDateTimeFromSysDateTime().AddYears(-1).Date);
            int isRootDeptFirst = this.regMgr.IsFirstRegister("2", this.register.PID.CardNO, this.register.DoctorInfo.Templet.Dept.ID, this.register.DoctorInfo.Templet.Doct.ID, this.regMgr.GetDateTimeFromSysDateTime().AddYears(-1).Date);
            int isDeptFirst = this.regMgr.IsFirstRegister("3", this.register.PID.CardNO, this.register.DoctorInfo.Templet.Dept.ID, this.register.DoctorInfo.Templet.Doct.ID, this.regMgr.GetDateTimeFromSysDateTime().AddYears(-1).Date);
            int isDoctFirst = this.regMgr.IsFirstRegister("4", this.register.PID.CardNO, this.register.DoctorInfo.Templet.Dept.ID, FS.FrameWork.Management.Connection.Operator.ID, this.regMgr.GetDateTimeFromSysDateTime().AddYears(-1).Date);

            this.register.HospitalFirstVisit = isHospitalFirst > 0 ? "0" : "1";
            this.register.RootDeptFirstVisit = isRootDeptFirst > 0 ? "0" : "1";
            this.register.IsFirst = isDeptFirst > 0 ? false : true;
            this.register.DoctFirstVist = isDoctFirst > 0 ? "0" : "1";

            register.DoctorInfo.Templet.Doct = employee;

            //return this.register;
            return 1;
        }

        /// <summary>
        /// 有效性校验
        /// </summary>
        /// <param name="reg"></param>
        /// <returns></returns>
        private bool CheckRegister(FS.HISFC.Models.Registration.Register reg)
        {
            if (reg.ID.Trim() == "" || reg.ID == null)
            {
                MessageBox.Show("门诊就诊号不可为空！");
                return false;
            }
            if (reg.Name.Trim() == "" || reg.Name == null)
            {
                MessageBox.Show("姓名不可为空！");
                return false;
            }
            if (reg.PID.CardNO.Trim() == "" || reg.PID.CardNO == null)
            {
                MessageBox.Show("门诊卡号不可为空！");
                return false;
            }
            if (reg.Sex.ID.ToString().Trim() == "" || reg.Sex.ID == null)
            {
                MessageBox.Show("性别不可为空！");
                return false;
            }

            FS.HISFC.Models.Base.Const conObj = noAutoRegPactHelper.GetObjectFromID(cmbPact.Tag.ToString()) as FS.HISFC.Models.Base.Const;

            if (this.cmbPact.Tag != null && !string.IsNullOrEmpty(this.cmbPact.Tag.ToString()) && conObj != null)
            {
                MessageBox.Show("合同单位【" + cmbPact.Text + "】" + conObj.Memo);
                return false;
            }

            if (IAfterQueryRegList != null)
            {
                if (IAfterQueryRegList.OnConfirmRegInfo(this.register) == -1)
                {
                    MessageBox.Show(IAfterQueryRegList.ErrInfo, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            //{3F495C28-F952-43fb-8B19-C63DDAB2F749}
            if (cmbClass1Desease.Text.ToString() == "" || cmbClass2Desease.Text.ToString() == "")
            {
                #region 儿科门诊判断一、二级病种是否已填写
                string deptCode = this.LogEmpl.Dept.ID;
                bool IsEkDept = false;
                if (deptCode == "5021")
                {
                   IsEkDept = true;
                }
                #endregion
                if (IsdiseaseMust == true || IsEkDept == true)
                {
                    MessageBox.Show("一级二级病种不能为空");
                    return false;
                }
                else
                {
                    return true;
                }
            }


            return true;
        }

        private int InsertRegInfo(FS.HISFC.Models.Registration.Register reg)
        {
            this.regManagement.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            int iReturn = -1;
            reg.InputOper.ID = this.employee.ID;
            reg.InputOper.Name = this.employee.Name;
            //reg.InputOper.OperTime = reg.DoctorInfo.SeeDate;
            iReturn = this.regManagement.Insert(reg);
            if (iReturn == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                if (regManagement.DBErrCode != 1)//不是主键重复
                {
                    MessageBox.Show("插入挂号信息出错!" + regManagement.Err);

                    return -1;
                }
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            return iReturn;
        }

        #endregion

        //{3F495C28-F952-43fb-8B19-C63DDAB2F749}
        #region 参数控制
        /// <summary>
        /// 参数控制类
        /// </summary>
        private static FS.FrameWork.Management.ControlParam ctlMgr = new FS.FrameWork.Management.ControlParam();
        bool IsdiseaseMust = false;
        #endregion
       
        private void btnOK_Click(object sender, EventArgs e)
        {
            //这里要判断一下，调取患者信息后是不是又修改过卡号
            if (patientInfo != null
                && !string.IsNullOrEmpty(patientInfo.PID.CardNO)
                && patientInfo.PID.CardNO != txtCardNo.Text)
            {
                MessageBox.Show("修改门诊号后，请回车确认！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                this.txtCardNo.Focus();
                return;
            }

            if (patientInfo != null && string.IsNullOrEmpty(this.cmbRegLevl.Text))
            {
                //{7F00D216-EFE7-48be-A8B3-CAE08FB347E0}
                MessageBox.Show("请选择挂号级别！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (this.SetRegister() == -1)
            {
                return;
            }

            #region 判断挂号信息
            if (!this.CheckRegister(this.register))
            {
                return;
            }
            #endregion           

            #region 保存患者基本信息

            #region 判断患者信息

            if (string.IsNullOrEmpty(this.patientInfo.Card.ID))
            {
                this.patientInfo.Name = this.txtName.Text.Trim();
                this.patientInfo.Card.ID = this.txtCardNo.Text;
                this.patientInfo.PID.CardNO = this.txtCardNo.Text;

                this.patientInfo.PhoneHome = this.txtPhone.Text;
                this.patientInfo.AddressHome = this.txtAddress.Text;
                this.patientInfo.IDCard = this.txtIDCard.Text;

                #region 合同单位

                if (this.cmbPact.Tag == null || string.IsNullOrEmpty(this.cmbPact.Tag.ToString()))
                {
                    MessageBox.Show("请选择合同单位！");
                    return;
                }
                FS.HISFC.Models.Base.PactInfo pactObj = interMgr.GetPactUnitInfoByPactCode(this.cmbPact.Tag.ToString());
                if (pactObj == null)
                {
                    MessageBox.Show("获取合同单位信息出错：" + interMgr.Err);
                    return;
                }
                this.patientInfo.Pact = pactObj;
                #endregion

                this.patientInfo.Sex.ID = this.cmbSex.Tag.ToString();
                this.patientInfo.Birthday = this.dtPickerBirth.Value;

                //增加判断，避免医生人为修改卡号，导致挂号的信息和患者实际分配的卡号不一致
                FS.HISFC.Models.RADT.Patient patientCommonInfo = this.interMgr.QueryComPatientInfo(patientInfo.PID.CardNO);
                if (!string.IsNullOrEmpty(patientCommonInfo.PID.CardNO)  
                    && patientCommonInfo.Name != patientInfo.Name)
                {
                    MessageBox.Show("请在卡号处回车确认！\r\n\r\n原因：卡号【" + patientInfo.PID.CardNO + "】对应的姓名【" + patientCommonInfo.Name + "】和显示的姓名【" + patientInfo.Name + "】不一致！\r\n如需修改患者信息，请患者到门诊收费处修改！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            #endregion

            //{2888444F-50BA-4956-A5F7-D71F0C6448BB}
            #region 判断是否当天已存在这个医生的挂号
            DateTime now = regManagement.GetDateTimeFromSysDateTime();

            int tmp = regManagement.QueryRegisterByCardNODoctTime(register.PID.CardNO, register.DoctorInfo.Templet.Dept.ID,employee.ID, now.Date);

            if (tmp > 0)
            {
                MessageBox.Show("该患者今天已经挂过您的号，请在已诊或待诊列表中查找！");
                return;
            }
            #endregion


            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            this.regManagement.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            this.register.InputOper.ID = this.employee.ID;
            register.InputOper.Name = this.employee.Name;
            int iReturn = this.regManagement.Insert(register);
            if (iReturn == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                if (regManagement.DBErrCode != 1)//不是主键重复
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("插入挂号信息出错!" + regManagement.Err);
                    return;
                }
            }

            /*
             * 2020-02-12 huyungui
             * {067831BF-DDA5-4ac3-958A-4DD0BE5B085F}
             * 因为生育保险和广州医保新需要区分，不能够被医生补挂号选择的合同单位，而影响到患者基本信息的合同单位，所以不做update
            this.radtMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            if (this.radtMgr.UpdatePatientInfo(this.patientInfo) <= 0)
            {
                if (this.radtMgr.InsertPatientInfo(this.patientInfo) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("插入患者基本信息出错：" + radtMgr.Err);
                    return;
                }
            }
             * */

            FS.FrameWork.Management.PublicTrans.Commit();

            #endregion
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void frmRegistrationByDoctor_Load(object sender, EventArgs e)
        {
            this.InitControl();
        }

        private void btnCaecel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void SetEnabled(bool val)
        {
            //this.cmbPact.Enabled = val;
            this.cmbSex.Enabled = val;
            this.txtName.Enabled = val;
            this.txtIDCard.Enabled = val;
            //this.dtPickerBirth.Enabled = val;
            this.txtCardNo.Enabled = !val;
            this.txtPhone.Enabled = val; //新增电话与地址
            this.txtAddress.Enabled = val;
        }

        private void Clear()
        {
            this.txtName.Text = "";

            this.txtIDCard.Text = "";

            this.txtPhone.Text = "";

            this.txtAddress.Text = "";
            cmbPact.Tag = null;
            this.lblTip.Text = "";

            this.cmbRegLevl.Tag = null;

            this.cmbSex.Tag = null;
            this.dtPickerBirth.Value = DateTime.Now;

        }

        /// <summary>
        /// 设置患者基本信息
        /// </summary>
        /// <param name="patientObj"></param>
        public void SetPatientInfo(FS.HISFC.Models.RADT.Patient patientObj)
        {
            if (patientObj != null)
            {
                this.SetEnabled(false);

                this.txtCardNo.Text = patientObj.PID.CardNO;

                this.txtName.Text = patientObj.Name;

                this.txtIDCard.Text = patientObj.IDCard;

                this.txtPhone.Text = patientObj.PhoneHome;  //新增加的联系电话与家庭住址 by zhy 

                this.txtAddress.Text = patientObj.AddressHome;

                #region 合同单位

                //this.cmbPact.Enabled = true;
                this.cmbPact.Tag = patientObj.Pact.ID;

                if (this.cmbPact.Tag == null || string.IsNullOrEmpty(this.cmbPact.Tag.ToString()))
                {
                    this.cmbPact.Tag = "1";
                }

                this.lblTip.Text = "";

                #region 合同单位全天自费处理

                ArrayList alOwnFeeRegDept = this.conManager.GetList("OwnFeeRegDept");
                if (alOwnFeeRegDept == null)
                {
                    MessageBox.Show("获取自费看诊科室失败！" + conManager.Err);
                }

                foreach (FS.HISFC.Models.Base.Const constObj in alOwnFeeRegDept)
                {
                    if (constObj.IsValid && constObj.ID.Trim() == this.employee.Dept.ID)
                    {
                        ArrayList alOwnFeeRegLevl = this.conManager.GetList("OwnFeeRegLevl");
                        if (alOwnFeeRegLevl == null || alOwnFeeRegLevl.Count == 0)
                        {
                            MessageBox.Show("获取自费挂号级别失败！" + conManager.Err);
                        }

                        foreach (FS.HISFC.Models.Base.Const obj in alOwnFeeRegLevl)
                        {
                            if (obj.IsValid)
                            {
                                this.cmbPact.Tag = obj.ID;
                                this.lblTip.Text = "提示：系统设置本科室只能挂号【" + cmbPact.Text + "】合同单位！";
                                //this.cmbPact.Enabled = false;
                                break;
                            }
                        }

                        break;
                    }
                }
                #endregion

                #endregion

                #region 挂号级别

                string regLevl = "";

                isOrdinaryRegDept = false;

                #region 普诊挂号科室
                ArrayList alOrdinaryRegDept = this.conManager.GetList("OrdinaryRegLevlDept");
                if (alOrdinaryRegDept == null)
                {
                    MessageBox.Show("获取普诊挂号科室失败！" + conManager.Err);
                    return;
                }

                foreach (FS.HISFC.Models.Base.Const constObj in alOrdinaryRegDept)
                {
                    if (constObj.IsValid && constObj.ID.Trim() == this.employee.Dept.ID)
                    {
                        isOrdinaryRegDept = true;
                        break;
                    }
                }

                #endregion

                //普诊
                if (isOrdinaryRegDept)
                {
                    ArrayList alOrdinaryLevl = this.conManager.GetList("OrdinaryRegLevel");
                    if (alOrdinaryLevl == null || alOrdinaryLevl.Count == 0)
                    {
                        MessageBox.Show("获取普通门诊对应的挂号级别错误：" + conManager.Err);
                        return;
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
                    bool isEmerg = this.regManagement.IsEmergency(this.employee.Dept.ID);

                    string diagItemCode = "";
                    if (isEmerg && !string.IsNullOrEmpty(emergencyLevlCode))
                    {
                        regLevl = this.emergencyLevlCode;
                    }
                    else
                    {
                        //非急诊挂号统一为普通挂号级别
                        if (this.regManagement.GetSupplyRegInfo(employee.ID, this.doct.Level.ID.ToString(), employee.Dept.ID, ref regLevl, ref diagItemCode) == -1)
                        {
                            MessageBox.Show(regManagement.Err);
                            return;
                        }

                        //{4DE128D5-7CDD-4c4c-8B7E-3A887FD5E6BA}
                        //早上8点到晚上6点的挂号统一为普通级别
                        DateTime now = this.feeManagement.GetDateTimeFromSysDateTime();
                        if (now.Hour > 8 && now.Hour < 18)
                        {
                            regLevl = "1";
                        }
                    }
                }

                FS.HISFC.Models.Registration.RegLevel regLevlObj = this.regLevlHelper.GetObjectFromID(regLevl) as FS.HISFC.Models.Registration.RegLevel;
                if (regLevlObj == null)
                {
                    //{7F00D216-EFE7-48be-A8B3-CAE08FB347E0}
                    regLevlObj = new FS.HISFC.Models.Registration.RegLevel();
                    regLevlObj.ID = "1";
                    //MessageBox.Show("查询挂号级别错误，编码[" + regLevl + "]！请联系信息科重新维护!");
                    //return;
                }
               // {78F152F3-0A93-4502-A47D-802314849489}
               // MessageBox.Show(this.employee.Name + this.employee.Level.ID);
                if (this.employee.Level.ID.Trim() == "09") //主任医师
                {
                    cmbRegLevl.Tag = 4;
                }
                else if (this.employee.Level.ID.Trim() == "10") //副主任医师
                {
                    cmbRegLevl.Tag = 3;
                }
                else if (this.employee.Level.ID.Trim() == "11") //主治医师
                {
                    cmbRegLevl.Tag = 2;
                }
                else
                    this.cmbRegLevl.Tag = 1;

                //判断急诊科室{A40886D6-8636-410c-9718-B879A74B09D0}
                HISFC.Models.Base.Employee oper = FrameWork.Management.Connection.Operator as HISFC.Models.Base.Employee;
                HISFC.Models.Base.Department dept = oper.Dept as HISFC.Models.Base.Department;
                if (dept.Name.Contains("急诊"))
                {
                    if (MessageBox.Show(this, "当前科室是急诊科室，是否挂急诊号？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        cmbRegLevl.Tag = 5;
                    }
                }
                #endregion

                this.cmbSex.Tag = patientObj.Sex.ID;
                if (patientObj.Birthday > new DateTime(1800, 1, 1))
                {
                    this.dtPickerBirth.Value = patientObj.Birthday;
                }
            }
        }

        private void txtCardNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int a = 0;
                //输入的是数字，则认为是卡号查询，否则是名称查询
                if (int.TryParse(txtCardNo.Text.Trim().Substring(1), out a))// {5D579726-0CDC-4f7d-BF02-EC6673B6BF41}
                {
                    FS.HISFC.Models.Account.AccountCard accountCard = new FS.HISFC.Models.Account.AccountCard();
                    FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();
                    string cardNO = this.txtCardNo.Text;
                    int flag = feeIntegrate.ValidMarkNO(cardNO, ref accountCard);

                    if (flag > 0)
                    {
                        cardNO = accountCard.Patient.PID.CardNO;
                    }
                    //返回错误了
                    else
                    {
                        MessageBox.Show(feeIntegrate.Err, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    this.txtCardNo.Text = cardNO;

                    if (!string.IsNullOrEmpty(this.txtCardNo.Text))
                    {
                        this.txtCardNo.Text = this.txtCardNo.Text.PadLeft(10, '0');
                    }

                    if (!string.IsNullOrEmpty(txtCardNo.Text.Trim()))
                    {
                        Clear();

                        this.patientInfo = this.radtMgr.QueryComPatientInfo(this.txtCardNo.Text);
                        if (patientInfo != null && !string.IsNullOrEmpty(patientInfo.PID.CardNO))
                        {
                            if (patientInfo.Memo == "作废患者")
                            {
                                MessageBox.Show("此患者已经作废！");
                                return;
                            }
                            this.SetPatientInfo(this.patientInfo);
                        }
                    }
                }
                else
                {
                    frmQueryPatientByName frmQuery = new frmQueryPatientByName();
                    //{51C02BB2-BBC3-45c2-B985-5E14ECEB0943}
                    frmQuery.IsFliterUnValid = true;
                    frmQuery.QueryByName(txtCardNo.Text.Trim());
                    frmQuery.SelectedPatient += new frmQueryPatientByName.GetPatient(frmQuery_SelectedPatient);
                    frmQuery.ShowDialog(this);
                }

                if (this.patientInfo != null
                    && !string.IsNullOrEmpty(txtName.Text))
                {
                    this.btnOK.Focus();
                }
            }
        }

        void frmQuery_SelectedPatient(FS.HISFC.Models.RADT.PatientInfo pInfo)
        {
            this.patientInfo = pInfo;
            if (patientInfo != null && !string.IsNullOrEmpty(patientInfo.PID.CardNO))
            {
                this.SetPatientInfo(this.patientInfo);
            }
        }

        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                frmQueryPatientByName frmQuery = new frmQueryPatientByName();
                //{51C02BB2-BBC3-45c2-B985-5E14ECEB0943}
                frmQuery.IsFliterUnValid = true;
                frmQuery.QueryByName(txtCardNo.Text.Trim());
                frmQuery.SelectedPatient += new frmQueryPatientByName.GetPatient(frmQuery_SelectedPatient);
                frmQuery.ShowDialog(this);
            }
        }
        /// <summary>
        /// 刷卡// {5D579726-0CDC-4f7d-BF02-EC6673B6BF41}
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btShuaKa_Click(object sender, EventArgs e)
        {
            string mCardNo = "";
            string error = "";
            if (Function.OperMCard(ref mCardNo, ref error) == -1)
            {
                MessageBox.Show(error);
                return;
            }
            this.txtCardNo.Text = ";" + mCardNo;
            txtCardNo_KeyDown(null, new KeyEventArgs(Keys.Enter));
        }
    }
}

