using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.HISFC.Components.OutPatientOrder.Controls.IOutPateintTree
{

    public partial class frmRegistrationByDoctor : FS.FrameWork.WinForms.Forms.BaseForm
    {
        public frmRegistrationByDoctor(string patientName)
        {
            InitializeComponent();
            this.txtName.Text = patientName;
        }

        #region 变量

        ///// <summary>
        ///// 自助挂号相关接口
        ///// </summary>
        //public FS.HISFC.BizProcess.Interface.Order.IAfterQueryRegList IAfterQueryRegList = null;

        /// <summary>
        /// 开立医生
        /// </summary>
        private FS.HISFC.Models.Base.Employee recipeDoct = null;

        /// <summary>
        /// 登陆人员信息
        /// </summary>
        private FS.HISFC.Models.Base.Employee loadOper = (FS.HISFC.Models.Base.Employee)CacheManager.ConManager.Operator;

        /// <summary>
        /// 不允许自动挂号的合同单位
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper noAutoRegPactHelper = null;

        /// <summary>
        /// 患者挂号信息
        /// </summary>
        FS.HISFC.Models.Registration.Register register = null;

        /// <summary>
        /// 患者挂号信息
        /// </summary>
        public FS.HISFC.Models.Registration.Register Register
        {
            get
            {
                return this.register;
            }
        }
        
        FS.HISFC.Models.RADT.PatientInfo patientInfo = new FS.HISFC.Models.RADT.PatientInfo();

        /// <summary>
        /// 是否普诊科室，普诊科室挂号级别始终是普诊
        /// </summary>
        private bool isOrdinaryRegDept = false;

        /// <summary>
        /// 是否普诊科室，普诊科室挂号级别始终是普诊
        /// </summary>
        public bool IsOrdinaryRegDept
        {
            get
            {
                return isOrdinaryRegDept;
            }
            set
            {
                isOrdinaryRegDept = value;
            }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 初始化
        /// </summary>
        private void InitControl()
        {
            //初始化合同单位
            ArrayList pactList = CacheManager.InterMgr.QueryPactUnitOutPatient();
            if (pactList == null)
            {
                MessageBox.Show("初始化合同单位出错!" + CacheManager.InterMgr.Err);

                return;
            }
            this.cmbPact.AddItems(pactList);

            //初始化性别
            this.cmbSex.AddItems(FS.HISFC.Models.Base.SexEnumService.List());

            string autoCardNO = string.Empty;
            autoCardNO = CacheManager.FeeIntegrate.GetAutoCardNO();
            if (autoCardNO == string.Empty || autoCardNO == "" || autoCardNO == null)
            {
                MessageBox.Show("获得门诊卡号出错!" + CacheManager.FeeIntegrate.Err);

                return;
            }
            autoCardNO = autoCardNO.PadLeft(10, '0');

            this.cmbSex.Tag = "M";

            this.cmbPact.Tag = "1";

            this.recipeDoct = CacheManager.InterMgr.GetEmployeeInfo(this.loadOper.ID);
            if (this.recipeDoct == null)
            {
                MessageBox.Show(CacheManager.InterMgr.Err);
            }

            this.lblTip.Text = "";

            if (noAutoRegPactHelper == null)
            {
                noAutoRegPactHelper = new FS.FrameWork.Public.ObjectHelper();
                noAutoRegPactHelper.ArrayObject = CacheManager.InterMgr.GetConstantList("NoAutoRegPact");
            }

            cmbRegLevl.AddItems(SOC.HISFC.BizProcess.Cache.Fee.GetAllRegLevl());

            this.SetEnabled(false);
        }

        /// <summary>
        /// 自动生成卡号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btAutoCardNo_Click(object sender, EventArgs e)
        {
            string autoCardNO = string.Empty;
            autoCardNO = CacheManager.RegMgr.AutoGetCardNO().ToString(); //CacheManager.FeeIntegrate.GetAutoCardNO();
            if (autoCardNO == string.Empty || autoCardNO == "" || autoCardNO == null)
            {
                MessageBox.Show("获得门诊卡号出错!" + CacheManager.RegMgr.Err);
                return;
            }
            autoCardNO = autoCardNO.PadLeft(10, '0');
            this.txtCardNo.Text = autoCardNO;

            this.SetEnabled(true);
            this.txtName.Focus();
        }

        /// <summary>
        /// 设置患者信息
        /// </summary>
        /// <returns></returns>
        private int SetRegister()
        {
            DateTime dtNow = CacheManager.OrderMgr.GetDateTimeFromSysDateTime();



            string clinicNO = CacheManager.OrderMgr.GetSequence("Registration.Register.ClinicID");
            if (string.IsNullOrEmpty(clinicNO))
            {
                MessageBox.Show("获得门诊就诊流水号出错!" + CacheManager.OrderMgr.Err);
                return -1;
            }
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

            FS.HISFC.Models.Base.PactInfo pactObj = CacheManager.InterMgr.GetPactUnitInfoByPactCode(this.cmbPact.Tag.ToString());
            if (pactObj == null)
            {
                MessageBox.Show("获取合同单位信息出错：" + CacheManager.InterMgr.Err);
                return -1;
            }
            this.register.Pact = pactObj;
            #endregion


            this.register.Sex.ID = this.cmbSex.Tag.ToString();
            this.register.Birthday = this.dtPickerBirth.Value;
            this.register.DoctorInfo.SeeDate = dtNow;
            this.register.DoctorInfo.SeeNO = -1;
            this.register.DoctorInfo.Templet.Dept = this.loadOper.Dept;

            this.register.InputOper.ID = this.loadOper.ID;
            this.register.InputOper.OperTime = dtNow;
            this.register.DoctorInfo.SeeDate = dtNow;
            this.register.DoctorInfo.Templet.Begin = dtNow;
            this.register.DoctorInfo.Templet.End = dtNow;
            this.register.RegType = FS.HISFC.Models.Base.EnumRegType.Reg;

            #region 午别
            if (this.register.DoctorInfo.SeeDate.Hour < 12 
                && this.register.DoctorInfo.SeeDate.Hour > 6)
            {
                //上午
                this.register.DoctorInfo.Templet.Noon.ID = "1";
            }
            else if (this.register.DoctorInfo.SeeDate.Hour > 12
                && this.register.DoctorInfo.SeeDate.Hour < 18)
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

            register.DoctorInfo.Templet.Doct = loadOper;

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

            //if (IAfterQueryRegList != null)
            //{
            //    if (IAfterQueryRegList.OnConfirmRegInfo(this.register) == -1)
            //    {
            //        MessageBox.Show(IAfterQueryRegList.ErrInfo, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        return false;
            //    }
            //}

            return true;
        }

        private int InsertRegInfo(FS.HISFC.Models.Registration.Register reg)
        {
            CacheManager.RegInterMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            int iReturn = -1;
            reg.InputOper.ID = this.loadOper.ID;
            reg.InputOper.Name = this.loadOper.Name;
            //reg.InputOper.OperTime = reg.DoctorInfo.SeeDate;
            iReturn = CacheManager.RegInterMgr.Insert(reg);
            if (iReturn == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                if (CacheManager.RegInterMgr.DBErrCode != 1)//不是主键重复
                {
                    MessageBox.Show("插入挂号信息出错!" + CacheManager.RegInterMgr.Err);

                    return -1;
                }
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            return iReturn;
        }

        #endregion

        private void btnOK_Click(object sender, EventArgs e)
        {
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
                FS.HISFC.Models.Base.PactInfo pactObj = CacheManager.InterMgr.GetPactUnitInfoByPactCode(this.cmbPact.Tag.ToString());
                if (pactObj == null)
                {
                    MessageBox.Show("获取合同单位信息出错：" + CacheManager.InterMgr.Err);
                    return;
                }
                this.patientInfo.Pact = pactObj;
                #endregion

                this.patientInfo.Sex.ID = this.cmbSex.Tag.ToString();
                this.patientInfo.Birthday = this.dtPickerBirth.Value;

                //增加判断，避免医生人为修改卡号，导致挂号的信息和患者实际分配的卡号不一致
                FS.HISFC.Models.RADT.Patient patientCommonInfo = CacheManager.InterMgr.QueryComPatientInfo(patientInfo.PID.CardNO);
                if (!string.IsNullOrEmpty(patientCommonInfo.PID.CardNO)
                    && patientCommonInfo.Name != patientInfo.Name)
                {
                    MessageBox.Show("请在卡号处回车确认！\r\n\r\n原因：卡号【" + patientInfo.PID.CardNO + "】对应的姓名【" + patientCommonInfo.Name + "】和显示的姓名【" + patientInfo.Name + "】不一致！\r\n如需修改患者信息，请患者到门诊收费处修改！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            #endregion


            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            CacheManager.RegInterMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            this.register.InputOper.ID = this.loadOper.ID;
            register.InputOper.Name = this.loadOper.Name;
            int iReturn = CacheManager.RegInterMgr.Insert(register);
            if (iReturn == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                if (CacheManager.RegInterMgr.DBErrCode != 1)//不是主键重复
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("插入挂号信息出错!" + CacheManager.RegInterMgr.Err);
                    return;
                }
            }

            CacheManager.InPatientMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            if (CacheManager.InPatientMgr.UpdatePatientInfo(this.patientInfo) <= 0)
            {
                if (CacheManager.InPatientMgr.InsertPatientInfo(this.patientInfo) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("插入患者基本信息出错：" + CacheManager.InPatientMgr.Err);
                    return;
                }
            }

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

        /// <summary>
        /// 设置患者基本信息
        /// </summary>
        /// <param name="patientObj"></param>
        public void SetPatientInfo(FS.HISFC.Models.RADT.Patient patientObj)
        {
            if (patientObj != null)
            {
                this.SetEnabled(false);

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

                ArrayList alOwnFeeRegDept = CacheManager.ConManager.GetList("OwnFeeRegDept");
                if (alOwnFeeRegDept == null)
                {
                    MessageBox.Show("获取自费看诊科室失败！" + CacheManager.ConManager.Err);
                }

                foreach (FS.HISFC.Models.Base.Const constObj in alOwnFeeRegDept)
                {
                    if (constObj.IsValid && constObj.ID.Trim() == this.loadOper.Dept.ID)
                    {
                        ArrayList alOwnFeeRegLevl = CacheManager.ConManager.GetList("OwnFeeRegLevl");
                        if (alOwnFeeRegLevl == null || alOwnFeeRegLevl.Count == 0)
                        {
                            MessageBox.Show("获取自费挂号级别失败！" + CacheManager.ConManager.Err);
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

                //普诊
                if (isOrdinaryRegDept)
                {
                    ArrayList alOrdinaryLevl = CacheManager.ConManager.GetList("OrdinaryRegLevel");
                    if (alOrdinaryLevl == null || alOrdinaryLevl.Count == 0)
                    {
                        MessageBox.Show("获取普通门诊对应的挂号级别错误：" + CacheManager.ConManager.Err);
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
                    bool isEmerg = CacheManager.RegInterMgr.IsEmergency(this.loadOper.Dept.ID);

                    string diagItemCode = "";
                    if (isEmerg)
                    {
                        regLevl = SOC.HISFC.BizProcess.Cache.Fee.GetEmergRegLevl() == null ? "" : SOC.HISFC.BizProcess.Cache.Fee.GetEmergRegLevl().ID;
                    }
                    else
                    {
                        if (CacheManager.RegInterMgr.GetSupplyRegInfo(loadOper.ID, this.recipeDoct.Level.ID.ToString(), loadOper.Dept.ID, ref regLevl, ref diagItemCode) == -1)
                        {
                            MessageBox.Show(CacheManager.RegInterMgr.Err);
                            return;
                        }
                    }
                }

                FS.HISFC.Models.Registration.RegLevel regLevlObj = SOC.HISFC.BizProcess.Cache.Fee.GetRegLevl(regLevl);
                if (regLevlObj == null)
                {
                    MessageBox.Show("查询挂号级别错误，编码[" + regLevl + "]！请联系信息科重新维护!");
                    return;
                }

                this.cmbRegLevl.Tag = regLevlObj.ID;

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
                if (!string.IsNullOrEmpty(this.txtCardNo.Text))
                {
                    this.txtCardNo.Text = this.txtCardNo.Text.PadLeft(10, '0');
                }

                if (!string.IsNullOrEmpty(txtCardNo.Text.Trim()))
                {
                    this.patientInfo = CacheManager.InPatientMgr.QueryComPatientInfo(this.txtCardNo.Text);
                    if (patientInfo != null && !string.IsNullOrEmpty(patientInfo.PID.CardNO))
                    {
                        this.SetPatientInfo(this.patientInfo);
                    }
                }

                this.btnOK.Focus();
            }
        }
    }
}
