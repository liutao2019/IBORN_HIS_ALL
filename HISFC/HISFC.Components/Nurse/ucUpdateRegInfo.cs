using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.HISFC.Components.Common.Controls;

namespace FS.HISFC.Components.Nurse
{
    /// <summary>
    /// 挂号患者信息修改
    /// create by xuewj 2010-11-4 {FCEC42B4-DF78-45c2-8D1A-EDAB94AA56DD} 
    /// </summary>
    public partial class ucUpdateRegInfo : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public ucUpdateRegInfo()
        {
            InitializeComponent();
        }

        #endregion

        #region 变量

        /// <summary>
        /// 显示患者信息
        /// </summary>
        protected ucShowPatients ucShow = new ucShowPatients();

        /// <summary>
        /// 多患者弹出窗口
        /// </summary>
        protected Form fPopWin = new Form();        
        
        /// <summary>
        /// 患者挂号基本信息
        /// </summary>
        protected FS.HISFC.Models.Registration.Register patientInfo = null;

        #endregion

        #region 属性
        /// <summary>
        /// 患者信息
        /// </summary>
        public FS.HISFC.Models.Registration.Register PatientInfo
        {
            set
            {
                this.patientInfo = value;
                this.txtCardNo.Enabled = false;
            }
        }
        #endregion

        #region 业务层变量

        /// <summary>
        /// 控制参数业务层
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        /// <summary>
        /// 挂号业务层
        /// </summary>
        protected FS.HISFC.BizLogic.Registration.Register regMgr = new FS.HISFC.BizLogic.Registration.Register();

        /// <summary>
        /// 挂号级别管理类
        /// </summary>
        protected FS.HISFC.BizLogic.Registration.RegLevel regLevelMgr = new FS.HISFC.BizLogic.Registration.RegLevel();

        /// <summary>
        /// 管理业务层
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// 费用综合业务层
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();

        #endregion

        #region 枚举

        /// <summary>
        /// 判断身份证信息
        /// </summary>
        private enum EnumCheckIDNOType
        {
            /// <summary>
            /// 保存之前校验
            /// </summary>
            BeforeSave = 0,

            /// <summary>
            /// 保存时校验
            /// </summary>
            Saving
        }

        #endregion

        #region 方法

        /// <summary>
        /// 基本验证输入的卡号是否合法
        /// </summary>
        /// <param name="cardNO">输入的卡号</param>
        /// <returns>合法返回true 不合法返回false</returns>
        private bool IsInputCardNOValid(string cardNO)
        {
            //如果输入的卡号是一个或者多个空格,那么认为没有输入.
            if (cardNO.Trim() == string.Empty)
            {
                this.txtCardNo.SelectAll();
                this.txtCardNo.Focus();

                return false;
            }

            return true;
        }

        /// <summary>
        /// 清空方法
        /// </summary>
        public void Clear()
        {

            DateTime current = this.regMgr.GetDateTimeFromSysDateTime();

            txtIdNO.Text = string.Empty;
            txtPhone.Text = string.Empty;
            txtName.Text = string.Empty;
            txtAge.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtMcardNo.Text = string.Empty;
            txtCardNo.Text = string.Empty;

            dtBirthday.Value = current;
            dtRegDate.Value = current;

            cmbUnit.Tag = string.Empty;
            cmbPayKind.Tag = string.Empty;
            cmbSex.Tag = string.Empty;
            cmbCardType.Tag = string.Empty;
            cmbDept.Tag = string.Empty;
            cmbDoctor.Tag = string.Empty;
            cmbRegLevel.Tag = string.Empty;

            txtCardNo.Focus();

        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            if (this.InitList() == -1)
            {
                return;
            }
            this.InitPopShowPatient();
            if (this.patientInfo != null)
            {
                this.SetPatientInfo(this.patientInfo);
            }
            base.OnLoad(e);
        }

        /// <summary>
        /// 初始化多患者弹出窗口
        /// </summary>
        protected virtual void InitPopShowPatient()
        {
            fPopWin.Width = ucShow.Width + 10;
            fPopWin.MinimizeBox = false;
            fPopWin.MaximizeBox = false;
            fPopWin.Controls.Add(ucShow);
            ucShow.Dock = DockStyle.Fill;
            fPopWin.Height = 200;
            fPopWin.Visible = false;
            fPopWin.KeyDown += new KeyEventHandler(fPopWin_KeyDown);
            this.ucShow.SelectedPatient += new ucShowPatients.GetPatient(ucShow_SelectedPatient);
        }

        /// <summary>
        /// 初始化列表
        /// </summary>
        protected virtual int InitList()
        {
            //科室列表
            ArrayList alDepts=this.managerIntegrate.GetDepartment(FS.HISFC.Models.Base.EnumDepartmentType.C);
            if (alDepts == null)
            {
                MessageBox.Show("查询科室列表出错!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
            this.cmbDept.AddItems(alDepts);

            //挂号级别
            ArrayList alRegLevels = this.regLevelMgr.Query(true);
            if (alRegLevels == null)
            {
                MessageBox.Show("查询挂号级别列表出错!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
            this.cmbRegLevel.AddItems(alRegLevels);

            //看诊医生
            ArrayList alDocs = this.managerIntegrate.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.D);
            if (alDocs == null)
            {
                MessageBox.Show("查询看诊医生列表出错!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
            this.cmbDoctor.AddItems(alDocs);


            //合同单位
            ArrayList alPayKinds = this.feeIntegrate.QueryPactUnitOutPatient();
            if (alPayKinds == null)
            {
                MessageBox.Show("查询合同单位列表出错!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
            this.cmbPayKind.AddItems(alPayKinds);

            //性别
            this.cmbSex.AddItems(FS.HISFC.Models.Base.SexEnumService.List());

            //证件类别
            ArrayList alCardTypes = this.managerIntegrate.QueryConstantList("IDCard");
            if (alCardTypes == null)
            {
                MessageBox.Show("查询证件类别列表出错!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
            this.cmbCardType.AddItems(alCardTypes);

            return 1;
        }

        /// <summary>
        /// 选择患者事件
        /// </summary>
        /// <param name="register"></param>
        protected virtual void ucShow_SelectedPatient(FS.HISFC.Models.Registration.Register register)
        {
            if (register == null)
            {
                return;
            }

            this.patientInfo = register.Clone();
            this.SetPatientInfo(this.patientInfo);
        }

        /// <summary>
        /// 界面赋值
        /// </summary>
        /// <param name="register"></param>
        public void SetPatientInfo(FS.HISFC.Models.Registration.Register regObj)
        {
            this.cmbRegLevel.Tag = regObj.DoctorInfo.Templet.RegLevel.ID;
            this.cmbDept.Tag = regObj.DoctorInfo.Templet.Dept.ID;
            this.cmbDoctor.Tag = regObj.DoctorInfo.Templet.Doct.ID;

            this.dtRegDate.Value = regObj.DoctorInfo.SeeDate;
            this.txtCardNo.Text = regObj.PID.CardNO;
            this.txtName.Text = regObj.Name;
            this.cmbSex.Tag = regObj.Sex.ID;
            this.cmbPayKind.Tag = regObj.Pact.ID;
            this.txtMcardNo.Text = regObj.SSN;
            this.txtPhone.Text = regObj.PhoneHome;
            this.txtAddress.Text = regObj.AddressHome;

            this.txtIdNO.Text = regObj.IDCard;
            if (regObj.Birthday != DateTime.MinValue)
                this.dtBirthday.Value = regObj.Birthday;

            this.cmbCardType.Tag = regObj.CardType.ID;

            this.SetAge(regObj.Birthday);
        }

        /// <summary>
        /// 切换焦点
        /// </summary>
        public void ChangeFocus()
        {
            foreach (System.Windows.Forms.Control control in this.Controls)
            {
                if (!(control is FS.FrameWork.WinForms.Controls.NeuLabel ||
                    control is FS.FrameWork.WinForms.Controls.NeuPanel))
                {
                    SendKeys.Send("{Tab}");
                    break;
                }
            }
        }

        /// <summary>
        /// Set Age
        /// </summary>
        /// <param name="birthday"></param>
        private void SetAge(DateTime birthday)
        {
            this.txtAge.Text = "";

            if (birthday == DateTime.MinValue)
            {
                return;
            }

            DateTime current;
            int year, month, day;

            current = this.regMgr.GetDateTimeFromSysDateTime();
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
        /// 更新患者基本信息
        /// </summary>
        private int UpdateRegInfo()
        {
            if (this.patientInfo == null || this.patientInfo.ID == "")
            {
                return -1;
            }

            this.regMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //更新患者基本信息
            if (this.regMgr.UpdatePatientForNurse(this.patientInfo) < 1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("更新患者基本信息失败!" + this.regMgr.Err, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }

            //更新挂号主表中的患者基本信息
            if (this.regMgr.UpdateRegInfo(this.patientInfo) < 1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("更新患者挂号信息失败!" + this.regMgr.Err, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }

            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show("保存成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            return 1;
        }

        /// <summary>
        /// 获取待更新数据
        /// </summary>
        private void GetPatientInfo()
        {
            patientInfo.Name = this.txtName.Text.Trim();
            patientInfo.Birthday = this.dtBirthday.Value;
            patientInfo.Sex.ID = this.cmbSex.Tag.ToString();
            patientInfo.AddressHome = this.txtAddress.Text.Trim();
            patientInfo.PhoneHome = this.txtPhone.Text.Trim();
            patientInfo.CardType.ID = this.cmbCardType.Tag.ToString();
            //patientInfo.InSource.ID=
            //patientInfo.Pact.PayKind.ID=
            //patientInfo.Pact.PayKind.Name=;
            patientInfo.Pact.ID = this.cmbPayKind.Tag.ToString();
            patientInfo.Pact.Name = this.cmbPayKind.Text.Trim();
            patientInfo.SSN = this.txtMcardNo.Text.Trim();
            //FS.FrameWork.Function.NConvert.ToInt32(patientInfo.IsEncrypt)=
            //patientInfo.NormalName=
            patientInfo.IDCard = this.txtIdNO.Text.Trim();
            //patientInfo.ID
        }

        /// <summary>
        /// 身份证有效性判断
        /// </summary>
        /// <param name="idNO"></param>
        /// <param name="enumType"></param>
        /// <returns></returns>
        private int ProcessIDENNO(string idNO, EnumCheckIDNOType enumType)
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
                MessageBox.Show(errText);
                this.txtIdNO.Focus();
                return -1;
            }
            string[] reurnString = errText.Split(',');
            if (enumType == EnumCheckIDNOType.BeforeSave) 
            {
                this.dtBirthday.Text = reurnString[1];
                this.cmbSex.Text = reurnString[2];
                this.SetAge(this.dtBirthday.Value);
                //this.cmbPayKind.Focus();
            }
            else
            {
                if (this.dtBirthday.Text != reurnString[1])
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("输入的生日日期与身份证号码中的生日不符"));
                    this.dtBirthday.Focus();
                    return -1;
                }

                if (this.cmbSex.Text != reurnString[2])
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("输入的性别与身份证中号的性别不符"));
                    this.cmbSex.Focus();
                    return -1;
                }
            }
            return 1;
        }

        /// <summary>
        /// 患者信息有效性判断
        /// </summary>
        /// <returns></returns>
        private bool ValidInformation()
        {
            #region 姓名
            if (this.txtName.Text.Trim() == "")
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("请输入患者姓名"), "提示");
                this.txtName.Focus();
                return false;
            }
            #endregion

            #region 性别

            if (this.cmbSex.Tag == null || this.cmbSex.Tag.ToString() == "")
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("请选择患者性别"), "提示");
                this.cmbSex.Focus();
                return false;
            }

            #endregion

            #region 出生日期
            DateTime current = this.regMgr.GetDateTimeFromSysDateTime().Date;

            if (this.dtBirthday.Value.Date > current)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("出生日期不能大于当前时间"), "提示");
                this.dtBirthday.Focus();
                return false;
            }
            #endregion

            #region 身份证
            if (!string.IsNullOrEmpty(cmbCardType.Tag.ToString()) && cmbCardType.Tag.ToString() == "01" && !string.IsNullOrEmpty(this.txtIdNO.Text.Trim()))
            {
                int returnValue = this.ProcessIDENNO(this.txtIdNO.Text.Trim(), EnumCheckIDNOType.Saving);
                if (returnValue < 0)
                {
                    return false;
                }
            }
            #endregion

            return true;
        }

        /// <summary>
        /// 获取出生日期
        /// </summary>
        private int GetBirthday()
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
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("输入年龄不正确,请重新输入"), "提示");
                this.txtAge.Focus();
                return -1;
            }

            DateTime birthday = DateTime.MinValue;

            this.GetBirthday(i, this.cmbUnit.Text, ref birthday);

            if (birthday < this.dtBirthday.MinDate)
            {
                MessageBox.Show("年龄不能过大!", "提示");
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
        private void GetBirthday(int age, string ageUnit, ref DateTime birthday)
        {
            DateTime current = this.regMgr.GetDateTimeFromSysDateTime();

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
                    DateTime current = this.regMgr.GetDateTimeFromSysDateTime().Date;

                    if (this.dtBirthday.Value.Date > current)
                    {
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("出生日期不能大于当前时间"), "提示");
                        this.dtBirthday.Focus();
                        return false;
                    }

                    //计算年龄
                    if (this.dtBirthday.Value.Date != current)
                    {
                        this.SetAge(this.dtBirthday.Value);
                    }
                }
                else if (this.txtAge.Focused)
                {
                    if (this.GetBirthday() == -1)
                    {
                        return base.ProcessDialogKey(keyData);
                    }
                }
                else if (this.txtIdNO.Focused)
                {
                    if (!string.IsNullOrEmpty(cmbCardType.Tag.ToString()) && cmbCardType.Tag.ToString() == "01" && !string.IsNullOrEmpty(this.txtIdNO.Text.Trim()))
                    {
                        int returnValue = this.ProcessIDENNO(this.txtIdNO.Text.Trim(), EnumCheckIDNOType.BeforeSave);
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

        /// <summary>
        /// 卡号回车事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCardNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string cardNO = this.txtCardNo.Text.Trim();

                if (!IsInputCardNOValid(cardNO))
                {
                    return;
                }

                //清空已经录入的信息.其他选择信息,重置.
                this.Clear();

                //处理挂号业务，不通过输入的内容检索信息
                //各个内容等待操作员输入
                //正常输入患者门诊卡号情况
                
                    FS.HISFC.Models.Account.AccountCard accountCard = new FS.HISFC.Models.Account.AccountCard();
                    if (feeIntegrate.ValidMarkNO(cardNO, ref accountCard) > 0)
                    {
                        cardNO = accountCard.Patient.PID.CardNO;
                        decimal vacancy = 0m;
                        if (feeIntegrate.GetAccountVacancy(accountCard.Patient.PID.CardNO, ref vacancy) > 0)
                        {
                            this.cmbCardType.Enabled = false;
                            this.txtIdNO.Enabled = false;
                            //this.tbSIBalanceCost.Text = vacancy.ToString();
                        }
                        else
                        {
                            //this.tbSIBalanceCost.Text = string.Empty;
                        }
                    }

                    bool isValid = false;

                    string tmpOrgCardNo = cardNO;
                    //填充卡号到10位，左补0
                    cardNO = cardNO.PadLeft(10, '0');
                    this.patientInfo = new FS.HISFC.Models.Registration.Register(); //实例化挂号信息实体
                    //触发显示患者信息控件
                    isValid = ShowPatient(cardNO, tmpOrgCardNo, this.txtCardNo.Location, this.txtCardNo.Height);

                    if (isValid) //如果获得的患者基本信息有效，那么跳转焦点到选择姓名
                    {
                        if (this.patientInfo.DoctorInfo.Templet.Doct.ID != null && this.patientInfo.DoctorInfo.Templet.Doct.ID.Length > 0)
                        {
                            this.ChangeFocus();
                        }
                        else
                        {
                            this.cmbCardType.Focus();
                        }
                    }
                    else //如果无效卡号，那么重新输入卡号
                    {
                        this.txtCardNo.SelectAll();
                        this.txtCardNo.Focus();
                    }
                
            }

            if (e.KeyCode == Keys.Space)//{7EEF23C0-631F-4cfa-9DFA-E62453A2307A}
            {
                FS.HISFC.Models.RADT.PatientInfo p = new FS.HISFC.Models.RADT.PatientInfo();
                if (FS.HISFC.Components.Common.Classes.Function.QueryComPatientInfo(ref p) == 1)
                {
                    this.txtCardNo.Text = p.PID.CardNO;
                    this.txtCardNo_KeyDown(null, new KeyEventArgs(Keys.Enter));
                }
            }
        }

        /// <summary>
        /// 显示患者信息
        /// </summary>
        /// <param name="cardNO"></param>
        /// <param name="orgNO"></param>
        /// <param name="cardLocation"></param>
        /// <param name="cardHeight"></param>
        /// <returns></returns>
        private bool ShowPatient(string cardNO, string orgNO, Point cardLocation, int cardHeight)
        {
            ucShow.OrgCardNO = orgNO;
            ucShow.CardNO = cardNO;
            ucShow.operType = "1";//直接输入
            if (ucShow.PersonCount == 0)
            {
                this.Clear();
                MessageBox.Show("该患者没有挂号信息!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                return false;
            }
            if (ucShow.PersonCount > 1)
            {
                fPopWin.Show();
                fPopWin.Hide();
                fPopWin.Location = this.PointToScreen(new Point(cardLocation.X, cardLocation.Y + cardHeight));
                fPopWin.ShowDialog();
            }
            if (this.patientInfo == null)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 打开患者多次挂号UC
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void fPopWin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.fPopWin.Close();
            }
        }

        /// <summary>
        /// 年龄单位选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetBirthday();
        }

        /// <summary>
        /// 出生日期修改事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtBirthday_ValueChanged(object sender, EventArgs e)
        {
            this.SetAge(dtBirthday.Value);
        }

        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            //有效性判断
            if (this.ValidInformation() == false)
            {
                return;
            }

            //取患者信息
            this.GetPatientInfo();
            if (this.patientInfo == null || this.patientInfo.ID == "")
            {
                MessageBox.Show("保存失败!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //更新患者基本信息
            if (this.UpdateRegInfo() == -1)
            {                
                return;
            }

            this.ParentForm.Close();
        }

        /// <summary>
        /// 取消按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.ParentForm.Close();
        }

        #endregion

    }
}
