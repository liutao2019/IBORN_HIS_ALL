using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FS.SOC.HISFC.Assign.Interface.Components;
using FS.SOC.HISFC.BizProcess.CommonInterface;

namespace FS.SOC.HISFC.Assign.Components.Common.Patient
{
    /// <summary>
    /// [功能描述: 门诊分诊修改患者挂号信息]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2011-12]<br></br>
    /// </summary>
    public partial class frmRegPatientInfo : FS.FrameWork.WinForms.Forms.BaseForm,FS.SOC.HISFC.Assign.Interface.Components.IMaintenance<FS.HISFC.Models.Registration.Register>
    {
        public frmRegPatientInfo()
        {
            InitializeComponent();
        }

        #region  域变量

        private FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<FS.HISFC.Models.Registration.Register> saveRegister = null;

        private FS.HISFC.Models.Registration.Register register = null;

        #endregion

        #region 初始化

        private int initEvents()
        {
            this.btnSave.Click -= new EventHandler(btnSave_Click);
            this.btnSave.Click += new EventHandler(btnSave_Click);

            this.btnCancel.Click -= new EventHandler(btnCancel_Click);
            this.btnCancel.Click += new EventHandler(btnCancel_Click);

            this.cmbUnit.SelectedIndexChanged -= new EventHandler(cmbUnit_SelectedIndexChanged);
            this.cmbUnit.SelectedIndexChanged+=new EventHandler(cmbUnit_SelectedIndexChanged);

            this.dtBirthday.ValueChanged -= new EventHandler(dtBirthday_ValueChanged);
            this.dtBirthday.ValueChanged+=new EventHandler(dtBirthday_ValueChanged);

            return 1;
        }

        private int initDatas()
        {
            //科室列表
            this.cmbDept.AddItems(CommonController.CreateInstance().QueryDepartment(FS.HISFC.Models.Base.EnumDepartmentType.C));

            //挂号级别
            this.cmbRegLevel.AddItems(FS.SOC.HISFC.BizProcess.CommonInterface.CommonController.CreateInstance().QueryRegLevel());

            //看诊医生
            this.cmbDoctor.AddItems(CommonController.CreateInstance().QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.D));

            //合同单位
            this.cmbPayKind.AddItems(CommonController.CreateInstance().QueryPactInfo());

            //性别
            this.cmbSex.AddItems(FS.HISFC.Models.Base.SexEnumService.List());

            //证件类别
            this.cmbCardType.AddItems(CommonController.CreateInstance().QueryConstant("IDCard"));

            return 1;
        }

        private int initInterface()
        {

            return 1;
        }

        #endregion

        #region 方法

        /// <summary>
        /// 界面赋值
        /// </summary>
        /// <param name="register"></param>
        private void setPatientInfo(FS.HISFC.Models.Registration.Register regObj)
        {
            this.cmbRegLevel.Tag = regObj.DoctorInfo.Templet.RegLevel.ID;
            this.cmbDept.Tag = regObj.DoctorInfo.Templet.Dept.ID;
            this.cmbDoctor.Tag = regObj.DoctorInfo.Templet.Doct.ID;

            this.dtRegDate.Value = regObj.DoctorInfo.SeeDate;
            this.txtCardNo.Text = regObj.PID.PatientNO;
            this.txtName.Text = regObj.Name;
            this.cmbSex.Tag = regObj.Sex.ID;
            this.cmbPayKind.Tag = regObj.Pact.ID;
            this.txtMcardNo.Text = regObj.SSN;
            this.txtPhone.Text = regObj.PhoneHome;
            this.txtAddress.Text = regObj.AddressHome;

            this.txtIdNO.Text = regObj.IDCard;
            if (regObj.Birthday != DateTime.MinValue)
                this.dtBirthday.Value = regObj.Birthday;

            this.cmbCardType.Tag = regObj.IDCardType.ID;

            this.ckEmergency.Checked = regObj.DoctorInfo.Templet.RegLevel.IsEmergency;
            this.txtTemperature.Text = regObj.Temperature;

            this.setAge(regObj.Birthday);
        }

        /// <summary>
        /// Set Age
        /// </summary>
        /// <param name="birthday"></param>
        private void setAge(DateTime birthday)
        {
            this.txtAge.Text = "";

            if (birthday == DateTime.MinValue)
            {
                return;
            }

            DateTime current;
            int year, month, day;

            current = CommonController.CreateInstance().GetSystemTime();
            year = current.Year - birthday.Year;
            month = current.Month - birthday.Month;
            day = current.Day - birthday.Day;

            if (year > 1)
            {
                this.txtAge.Text = year.ToString();
                this.cmbUnit.SelectedIndex = 0;
            }
            else if (year == 1)
            {
                if (month >= 0)//一岁
                {
                    this.txtAge.Text = year.ToString();
                    this.cmbUnit.SelectedIndex = 0;
                }
                else
                {
                    this.txtAge.Text = Convert.ToString(12 + month);
                    this.cmbUnit.SelectedIndex = 1;
                }
            }
            else if (month > 0)
            {
                this.txtAge.Text = month.ToString();
                this.cmbUnit.SelectedIndex = 1;
            }
            else if (day > 0)
            {
                this.txtAge.Text = day.ToString();
                this.cmbUnit.SelectedIndex = 2;
            }

        }

        /// <summary>
        /// 获取待更新数据
        /// </summary>
        private void getPatientInfo()
        {
            register.Name = this.txtName.Text.Trim();
            register.Birthday = this.dtBirthday.Value;
            register.Sex.ID = this.cmbSex.Tag.ToString();
            register.AddressHome = this.txtAddress.Text.Trim();
            register.PhoneHome = this.txtPhone.Text.Trim();
            register.IDCardType.ID = this.cmbCardType.Tag.ToString();
            register.Pact.ID = this.cmbPayKind.Tag.ToString();
            register.Pact.Name = this.cmbPayKind.Text.Trim();
            register.SSN = this.txtMcardNo.Text.Trim();
            register.IDCard = this.txtIdNO.Text.Trim();
            if (this.cmbDept.Tag != null)
            {
                register.DoctorInfo.Templet.Dept.Memo = register.DoctorInfo.Templet.Dept.ID;//修改之前科室ID
                register.DoctorInfo.Templet.Dept.ID = this.cmbDept.Tag.ToString();
                register.DoctorInfo.Templet.Dept.Name = this.cmbDept.SelectedItem.Name;
            }
            register.DoctorInfo.Templet.Doct.ID = this.cmbDoctor.Tag.ToString();
            register.DoctorInfo.Templet.RegLevel.IsEmergency = this.ckEmergency.Checked;
            register.Temperature = this.txtTemperature.Text;

        }

        /// <summary>
        /// 患者信息有效性判断
        /// </summary>
        /// <returns></returns>
        private bool validInformation()
        {
            #region 姓名
            if (this.txtName.Text.Trim() == "")
            {
                CommonController.CreateInstance().MessageBox(this, FS.FrameWork.Management.Language.Msg("请输入患者姓名"), MessageBoxIcon.Error);
                this.txtName.Select();
                this.txtName.Focus();
                return false;
            }
            #endregion

            #region 性别

            if (this.cmbSex.Tag == null || this.cmbSex.Tag.ToString() == "")
            {
                CommonController.CreateInstance().MessageBox(this,FS.FrameWork.Management.Language.Msg("请选择患者性别"), MessageBoxIcon.Error);
                this.cmbSex.Select();
                this.cmbSex.Focus();
                return false;
            }

            #endregion

            #region 出生日期
            DateTime current = CommonController.CreateInstance().GetSystemTime();

            if (this.dtBirthday.Value.Date > current)
            {
                CommonController.CreateInstance().MessageBox(this,FS.FrameWork.Management.Language.Msg("出生日期不能大于当前时间"), MessageBoxIcon.Error);
                this.dtBirthday.Select();
                this.dtBirthday.Focus();
                return false;
            }
            #endregion

            #region 身份证
            if (!string.IsNullOrEmpty(cmbCardType.Tag.ToString()) && cmbCardType.Tag.ToString() == "01" && !string.IsNullOrEmpty(this.txtIdNO.Text.Trim()))
            {
                int returnValue = this.processIDENNO(this.txtIdNO.Text.Trim(), false);
                if (returnValue < 0)
                {
                    return false;
                }
            }
            #endregion

            #region 温度判断
            if (!string.IsNullOrEmpty(this.txtTemperature.Text))
            {
                char c = this.txtTemperature.Text[0];
                if (c >= 48 && c <= 57)
                {
                }
                else
                {
                    MessageBox.Show("录入温度中不是数字字符！");
                    return false;
                }
            }
            #endregion
            return true;
        }

        /// <summary>
        /// 身份证有效性判断
        /// </summary>
        /// <param name="idNO"></param>
        /// <param name="enumType"></param>
        /// <returns></returns>
        private int processIDENNO(string idNO, bool beforeSave)
        {
            string errText = string.Empty;

            //校验身份证号

            string idNOTmp = string.Empty;
            if (idNO.Length == 15)
            {
                idNOTmp = FS.FrameWork.WinForms.Classes.Function.TransIDFrom15To18(idNO);
            }
            else
            {
                idNOTmp = idNO;
            }

            //校验身份证号
            int returnValue = FS.FrameWork.WinForms.Classes.Function.CheckIDInfo(idNOTmp, ref errText);

            if (returnValue < 0)
            {
                CommonController.CreateInstance().MessageBox(this,errText, MessageBoxIcon.Error);
                this.txtIdNO.Focus();
                return -1;
            }
            string[] reurnString = errText.Split(',');
            if (beforeSave)
            {
                this.dtBirthday.Text = reurnString[1];
                this.cmbSex.Text = reurnString[2];
                this.setAge(this.dtBirthday.Value);
            }
            else
            {
                if (this.dtBirthday.Text != reurnString[1])
                {
                    CommonController.CreateInstance().MessageBox(this, FS.FrameWork.Management.Language.Msg("输入的生日日期与身份证号码中的生日不符"), MessageBoxIcon.Error);
                    this.dtBirthday.Focus();
                    return -1;
                }

                if (this.cmbSex.Text != reurnString[2])
                {
                    CommonController.CreateInstance().MessageBox(this, FS.FrameWork.Management.Language.Msg("输入的性别与身份证中号的性别不符"), MessageBoxIcon.Error);
                    this.cmbSex.Focus();
                    return -1;
                }
            }
            return 1;
        }

        /// <summary>
        /// 更新患者基本信息
        /// </summary>
        private int updateRegInfo()
        {
            if (this.register == null || this.register.ID == "")
            {
                return -1;
            }
            FS.SOC.HISFC.Assign.BizProcess.OutPatientInfo outPatientInfoBiz=new FS.SOC.HISFC.Assign.BizProcess.OutPatientInfo();
            string error = "";
            if (outPatientInfoBiz.Update(register, ref error) <= 0)
            {
                CommonController.CreateInstance().MessageBox(this, error, MessageBoxIcon.Question);
            }
            else
            {
                CommonController.CreateInstance().MessageBox(this, "保存成功", MessageBoxIcon.Question);
            }
            return 1;
        }

        /// <summary>
        /// 获取出生日期
        /// </summary>
        private int getBirthday()
        {
            string age = this.txtAge.Text.Trim();
            int i = 0;

            if (age == "") age = "0";

            try
            {
                i = int.Parse(age);
            }
            catch (Exception e)
            {
                string error = e.Message;
                CommonController.CreateInstance().MessageBox(this, FS.FrameWork.Management.Language.Msg("输入年龄不正确,请重新输入"), MessageBoxIcon.Error);
                this.txtAge.Focus();
                return -1;
            }

            DateTime birthday = DateTime.MinValue;

            this.getBirthday(i, this.cmbUnit.Text, ref birthday);

            if (birthday < this.dtBirthday.MinDate)
            {
                CommonController.CreateInstance().MessageBox(this, "年龄不能过大!", MessageBoxIcon.Error);
                this.txtAge.Focus();
                return -1;
            }

            if (this.cmbUnit.Text == "岁")
            {

                //数据库中存的是出生日期,如果年龄单位是岁,并且算出的出生日期和数据库中出生日期年份相同
                //就不进行重新赋值,因为算出的出生日期生日为当天,所以以数据库中为准

                if (this.dtBirthday.Value.Year != birthday.Year)
                {
                    this.dtBirthday.Value = birthday;
                }
            }
            else
            {
                this.dtBirthday.Value = birthday;
            }
            return 1;
        }

        /// <summary>
        /// 根据年龄得到出生日期
        /// </summary>
        /// <param name="age"></param>
        /// <param name="ageUnit"></param>
        /// <param name="birthday"></param>
        private void getBirthday(int age, string ageUnit, ref DateTime birthday)
        {
            DateTime current = CommonController.CreateInstance().GetSystemTime();

            if (ageUnit == "岁")
            {
                birthday = current.AddYears(-age);
            }
            else if (ageUnit == "月")
            {
                birthday = current.AddMonths(-age);
            }
            else if (ageUnit == "天")
            {
                birthday = current.AddDays(-age);
            }
        }

        #endregion

        #region 事件

        /// <summary>
        /// 按键事件
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                if (this.dtBirthday.Focused)
                {
                    DateTime current = CommonController.CreateInstance().GetSystemTime().Date;

                    if (this.dtBirthday.Value.Date > current)
                    {
                        CommonController.CreateInstance().MessageBox(this, FS.FrameWork.Management.Language.Msg("出生日期不能大于当前时间"), MessageBoxIcon.Error);
                        this.dtBirthday.Focus();
                        return false;
                    }

                    //计算年龄
                    if (this.dtBirthday.Value.Date != current)
                    {
                        this.setAge(this.dtBirthday.Value);
                    }
                }
                else if (this.txtAge.Focused)
                {
                    if (this.getBirthday() == -1)
                    {
                        return base.ProcessDialogKey(keyData);
                    }
                }
                else if (this.txtIdNO.Focused)
                {
                    if (!string.IsNullOrEmpty(cmbCardType.Tag.ToString()) && cmbCardType.Tag.ToString() == "01" && !string.IsNullOrEmpty(this.txtIdNO.Text.Trim()))
                    {
                        int returnValue = this.processIDENNO(this.txtIdNO.Text.Trim(), true);
                        if (returnValue < 0)
                        {
                            return base.ProcessDialogKey(keyData);
                        }
                    }
                }
                SendKeys.Send("{Tab}");
            }
            return base.ProcessDialogKey(keyData);
        }

        void btnSave_Click(object sender, EventArgs e)
        {
            //有效性判断
            if (this.validInformation() == false)
            {
                return;
            }

            //取患者信息
            this.getPatientInfo();

            //更新患者基本信息
            if (this.updateRegInfo() == -1)
            {
                return;
            }

            this.Close();

            if (this.saveRegister != null)
            {
                this.saveRegister(register);
            }
        }

        void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 年龄单位选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.getBirthday();
        }

        /// <summary>
        /// 出生日期修改事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtBirthday_ValueChanged(object sender, EventArgs e)
        {
            this.setAge(dtBirthday.Value);
        }

        #endregion

        #region IInitialisable 成员

        public int Init()
        {
            this.initDatas();
            this.initInterface();
            this.initEvents();
            return 1;
        }

        #endregion

        #region IClearable 成员

        public int Clear()
        {
            this.txtCardNo.Text = string.Empty;
            this.txtAddress.Text = string.Empty;
            this.txtAge.Text = string.Empty;
            this.txtIdNO.Text = string.Empty;
            this.txtMcardNo.Text = string.Empty;
            this.txtName.Text = string.Empty;
            this.txtPhone.Text = string.Empty;
            this.cmbCardType.Tag = null;
            this.cmbDept.Tag = null;
            this.cmbDoctor.Tag = null;
            this.cmbPayKind.Tag = null;
            this.cmbRegLevel.Tag = null;
            this.cmbSex.Tag = null;
            this.cmbUnit.Tag = null;
            this.dtBirthday.Value = CommonController.CreateInstance().GetSystemTime();
            this.dtRegDate.Value = this.dtBirthday.Value;
            this.ckEmergency.Checked = false;
            this.txtTemperature.Text = string.Empty;
            register = null;

            return 1;
        }

        #endregion

        #region ILoadable 成员

        public new int Load()
        {
            this.ShowDialog();
            return 1;
        }

        #endregion

        #region IMaintenance<Register> 成员

        public int Init(System.Collections.ArrayList al)
        {
            return 1;
        }

        public int Add(FS.HISFC.Models.Registration.Register t)
        {
            return 1;
        }

        public int Modify(FS.HISFC.Models.Registration.Register t)
        {
            if (t == null)
            {
                return -1;
            }
            register = t;
            this.setPatientInfo(t);
            return 1;
        }

        public FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<FS.HISFC.Models.Registration.Register> SaveInfo
        {
            get
            {
                return saveRegister;
            }
            set
            {
                saveRegister = value;
            }
        }

        #endregion
    }
}
