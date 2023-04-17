using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Management;

namespace FS.SOC.HISFC.Components.DCP.Controls
{
    /// <summary>
    /// ucPatientInfo<br></br>
    /// [功能描述: 患者信息uc]<br></br>
    /// [创 建 者: zj]<br></br>
    /// [创建时间: 2008-09-17]<br></br>
    /// <修改记录 
    ///		修改人='chengym' 
    ///		修改时间='2012-10-23' 
    ///		修改目的='人性化详细地址填写的方式并且强调门牌号'
    ///		修改描述='行政地址存储在com_address 暂只能导入，无维护界面'
    ///  />
    /// </summary>
    public partial class ucPatientInfo : ucBaseMainReport
    {
        #region 构造器

        public ucPatientInfo()
            : this(null)
        {
        }

        public ucPatientInfo(FS.HISFC.Models.RADT.Patient patient)
        {
            InitializeComponent();
            this.patientInfo = patient;

            this.txtAge.Leave+=new EventHandler(txtAge_Leave);
            this.txtAge.TextChanged+=new EventHandler(txtAge_TextChanged);
            this.dtpBirthDay.ValueChanged+=new EventHandler(dtpBirthDay_ValueChanged);
        }

        #endregion

        #region 域变量

        /// <summary>
        /// 患者信息实体
        /// </summary>
        private FS.HISFC.Models.RADT.Patient patientInfo = null;

        private DataBaseManger dbManager = new DataBaseManger();


        private FS.SOC.HISFC.BizLogic.DCP.PatientAddress patObj = new FS.SOC.HISFC.BizLogic.DCP.PatientAddress();
        private ArrayList alProvince = new ArrayList();
        private ArrayList alCity = new ArrayList();
        private ArrayList alCountry = new ArrayList();
        private ArrayList alTown = new ArrayList();

        private string myProvince = "";
        private string myCity = "";
        private string myCounty = "";
        // {2671947C-3F17-4eee-A72F-1479665EEB16}增加默认乡村

        private string myTown = "";
        
        //需要单位名称
        private System.Collections.Hashtable hsNeedWorkName = new Hashtable();
        #endregion

        #region 属性

        /// <summary>
        /// 患者信息实体
        /// </summary>
        private FS.HISFC.Models.RADT.Patient PatientInfo
        {
            get
            {
                return this.patientInfo;
            }
            set
            {
                this.patientInfo = value;
            }
        }

        /// <summary>
        /// 是否可以修改患者信息
        /// </summary>
        private bool IsEditPatient
        {
            get
            {
                return this.gbPatientInfo.Enabled;
            }
            set
            {
                this.gbPatientInfo.Enabled = value;
            }
        }

        #endregion

        #region 方法

        #region 外部调用

        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        public override int Init(DateTime sysdate)
        {
            base.Init(sysdate);//先初始化基类的方法。

            ArrayList alSex = FS.HISFC.Models.Base.SexEnumService.List();
            if (alSex == null || alSex.Count == 0)
            {
                MessageBox.Show(Language.Msg("获取性别信息出错！"));
                return -1;
            }
            this.cmbSex.AddItems(alSex);

            FS.SOC.HISFC.BizProcess.DCP.Common commonProcess = new FS.SOC.HISFC.BizProcess.DCP.Common();
            //特殊，不和系统一致
            ArrayList alProfession = commonProcess.QueryConstantList("PATIENTJOB");
            if (alProfession == null || alProfession.Count == 0)
            {
                MessageBox.Show(Language.Msg("获取职业信息出错！"));
                return -1;
            }
            this.cmbProfession.AddItems(alProfession);
            hsNeedWorkName = new Hashtable();
            foreach (FS.HISFC.Models.Base.Const jobInfo in alProfession)
            {
                if (jobInfo.Memo.IndexOf("需工作单位", 0) != -1)
                {
                    hsNeedWorkName.Add(jobInfo.ID, null);
                }
            }
            //获取医院所在地省市区  2012-10-23 chengym
            FS.FrameWork.Models.NeuObject addrHosp = commonProcess.GetConstantByTypeAndID("DCPHOSPADRR", "1");
            if (addrHosp != null && addrHosp.ID != "")
            {
                string[] addrstr = addrHosp.Memo.Split('|');
                //if (addrstr.Length == 3)
                //{
                //    this.myProvince = addrstr[0].ToString();
                //    this.myCity = addrstr[1].ToString();
                //    this.myCounty = addrstr[2].ToString();
                //}


                // {2671947C-3F17-4eee-A72F-1479665EEB16}增加默认乡村
                if (addrstr.Length == 4)
                {
                    this.myProvince = addrstr[0].ToString();
                    this.myCity = addrstr[1].ToString();
                    this.myCounty = addrstr[2].ToString();
                    this.myTown = addrstr[3].ToString();
                }
            }
            this.InitIgnoreInfo();
            this.Clear();
            return 1;
        }

        /// <summary>
        /// 判断患者信息是否完整
        /// </summary>
        /// <returns>-1 不完整 0 完整</returns>
        public int Valid(ref string msg,ref Control control)
        {
            if (string.IsNullOrEmpty(this.txtCardNO.Text))
            {
                msg = Language.Msg("请输入病历号！");
                control = this.txtCardNO;
                return -1;
            }
            if (string.IsNullOrEmpty(this.txtPatientName.Text))
            {
                msg = Language.Msg("请填写姓名！");
                control = this.txtPatientName;
                return -1;
            }
            if (this.cmbSex.Tag == null || string.IsNullOrEmpty(this.cmbSex.Text))
            {
                msg = Language.Msg("请选择性别！");
                control = this.cmbSex;
                return -1;
            }
            if (this.AuthenticationID(this.txtPatientID.Text)==-1)
            {
                msg = Language.Msg("身份证号码错误");
                control = this.txtPatientID;
                return -1;
            }
            if (this.dtpBirthDay.Value == null || this.dtpBirthDay.Value > this.sysdate)
            {
                msg = Language.Msg("出生日期错误，请检查！");
                control = this.dtpBirthDay;
                return -1;
            }
            if (this.sysdate.Year - this.dtpBirthDay.Value.Year < 15 && string.IsNullOrEmpty(this.txtPatientParents.Text))
            {
                msg = Language.Msg("14周岁（含14周岁）以下的请填写家长姓名！");
                control = this.txtPatientParents;
                return -1;
            }
            if (string.IsNullOrEmpty(this.txtSpecialAddress.Text))
            {
                msg = Language.Msg("请填写家庭详细地址！");
                control = this.txtSpecialAddress;
                return -1;
            }
            if (string.IsNullOrEmpty(this.txtTelephone.Text))
            {
                msg = Language.Msg("请填写联系电话！");
                control = this.txtTelephone;
                return -1;
            }
            if (string.IsNullOrEmpty(this.cmbProfession.Text) && (this.cmbProfession.Tag == null || string.IsNullOrEmpty(this.cmbProfession.Tag.ToString())))
            {
                msg = Language.Msg("请选择职业！");
                control = this.cmbProfession;
                return -1;
            }

            //{2671947C-3F17-4eee-A72F-1479665EEB16}增加判断如果职业选择了其他，必须填写其他职业
            if (string.IsNullOrEmpty(this.txtqtzy.Text) && (this.cmbProfession.Text== "其他"))
            {
                msg = Language.Msg("请填写其他职业！");
                control = this.txtqtzy;
                return -1;
            }


            //{2671947C-3F17-4eee-A72F-1479665EEB16}增加判断如果选择了需要工作单位的职业，必须填写工作单位
            if (hsNeedWorkName.Contains(this.cmbProfession.Tag.ToString()) && string.IsNullOrEmpty(this.txtWorkPlace.Text))
            {
                msg = Language.Msg("职业为“" + this.cmbProfession.Text + "”需填写工作单位！");
                control = this.txtWorkPlace;
                return -1;
            }
            if (string.IsNullOrEmpty(this.HomeArea))
            {
                msg = Language.Msg("请选择病人属于区域！");
                control = this.rb1;
                return -1;
            }
            if (this.rb1.Checked|| this.rb2.Checked || this.rb3.Checked || this.rb4.Checked)//国内
            {
                if (this.cmbProvince.Tag == null || string.IsNullOrEmpty(this.cmbProvince.Tag.ToString()))
                {
                    msg = Language.Msg("请选择病人所在省！");
                    control = this.cmbProvince;
                    return -1;
                }
                // {2671947C-3F17-4eee-A72F-1479665EEB16}增加判断市县区为必填项
                if (this.cmbCity.Tag == null || string.IsNullOrEmpty(this.cmbCity.Tag.ToString()))
                {
                    msg = Language.Msg("请选择病人所在市！");
                    control = this.cmbCity;
                    return -1;
                }
                if (this.cmbCountry.Tag == null || string.IsNullOrEmpty(this.cmbCountry.Tag.ToString()))
                {
                    msg = Language.Msg("请选择病人所在县(区)！");
                    control = this.cmbCountry;
                    return -1;
                }
                if (this.cmbTown.Tag == null || string.IsNullOrEmpty(this.cmbTown.Tag.ToString()))
                {
                    msg = Language.Msg("请选择病人所在乡(镇、街道)！");
                    control = this.cmbTown;
                    return -1;
                }
            }
            return 0;
        }

        /// <summary>
        /// 取患者信息
        /// </summary>
        /// <param name="patient">传入的患者信息实体</param>
        /// <returns></returns>
        public override int GetValue(ref FS.HISFC.DCP.Object.CommonReport report)
        {
            if (report.Patient == null)
            {
                return -1;
            }
            string msg = "";
            Control c = null;
            if (this.Valid(ref msg,ref c)== -1)
            {
                MessageBox.Show(Language.Msg(msg));
                if (c != null)
                {
                    c.Select();
                }
                return -1;
            }

            try
            {
                report.Patient.Name = this.txtPatientName.Text;
                report.Patient.Sex.ID = this.cmbSex.Tag.ToString();
                report.Patient.IDCard = this.txtPatientID.Text;
                report.Patient.Birthday = this.dtpBirthDay.Value;
                report.Patient.MatherName = this.txtPatientParents.Text;
                report.PatientParents = this.txtPatientParents.Text.Trim();//家长姓名
                report.HomeArea=this.HomeArea;
                //取年龄
                int year = 0;
                int month = 0;
                int day = 0;
                this.GetAgeNumber(this.txtAge.Text, ref year, ref month, ref day);

                if (year > 0)
                {
                    report.Patient.Age = year.ToString();
                    report.AgeUnit = "岁";
                }
                else if (month > 0)
                {
                    report.Patient.Age = month.ToString();
                    report.AgeUnit = "月";
                }
                else if (day > 0)
                {
                    report.Patient.Age = day.ToString();
                    report.AgeUnit = "天";
                }
                else
                {
                    MessageBox.Show("年龄不能小于零");
                    this.txtAge.Select();
                    return -1;
                }
                #region 省、市、区 2012-10-23 chengym
                if (this.cmbProvince.Tag != null)
                {
                    report.HomeProvince.ID = this.cmbProvince.Tag.ToString();
                }
                else
                {
                    report.HomeProvince.ID = "";
                }
                if (this.cmbCity.Tag != null)
                {
                    report.HomeCity.ID = this.cmbCity.Tag.ToString();
                }
                else
                {
                    report.HomeCity.ID = "";
                }
                if (this.cmbCountry.Tag != null)
                {
                    report.HomeCouty.ID = this.cmbCountry.Tag.ToString();
                }
                else
                {
                    report.HomeCouty.ID = "";
                }
                if (this.cmbTown.Tag != null)
                {
                    report.HomeTown.ID = this.cmbTown.Tag.ToString();
                }
                else
                {
                    report.HomeTown.ID = "";
                }
                #endregion
                //地址
                report.Patient.AddressHome = this.txtSpecialAddress.Text;
                report.Patient.PhoneHome = this.txtTelephone.Text;
                report.Patient.Profession.ID = this.cmbProfession.Tag == null ? "" : this.cmbProfession.Tag.ToString();
                report.Patient.Profession.Name = this.cmbProfession.Text;
                report.Patient.CompanyName = this.txtWorkPlace.Text;
                //report.ExtendInfo1 = this.txtSpecialAddress.Text;//前面report.Patient.AddressHome，改为存村加门牌号
                report.ExtendInfo1 = this.txtVillage.Text;
                report.Patient.PID.CardNO = this.txtCardNO.Text;
                // {2671947C-3F17-4eee-A72F-1479665EEB16}将界面中其他职业填入数据库
                report.ExtendInfo2 = this.txtqtzy.Text;

                return 0;
            }
            catch (Exception e)
            {
                MessageBox.Show(Language.Msg(e.Message));
                return -1;
            }
        }

        /// <summary>
        /// 赋患者信息
        /// </summary>
        /// <param name="patient">患者信息实体</param>
        public override int SetValue(FS.HISFC.Models.RADT.Patient patient,FS.SOC.HISFC.DCP.Enum.PatientType patientType)
        {
            if (patient == null)
            {
                return -1;
            }

            this.PatientInfo = patient;

            try
            {
                if (patientType == FS.SOC.HISFC.DCP.Enum.PatientType.I)
                {
                    this.txtCardNO.Text = this.PatientInfo.PID.PatientNO;
                }
                else if (patientType == FS.SOC.HISFC.DCP.Enum.PatientType.C)
                {
                    this.txtCardNO.Text = this.PatientInfo.PID.CardNO;
                }
                else
                {
                    this.txtCardNO.Text = this.PatientInfo.PID.CardNO;
                }
                this.txtPatientName.Text = this.PatientInfo.Name;
              
                this.txtPatientID.Text = this.PatientInfo.IDCard;
                this.txtPatientParents.Text = this.PatientInfo.MatherName;
                this.cmbSex.Tag = this.PatientInfo.Sex.ID;
                this.cmbSex.Text = this.PatientInfo.Sex.Name;

                //{F476F41B-D040-44f1-908D-022DCC942A55}增加判断，如果身份证不为空则直接取身份证上的生日，如果身份证为空则取挂号时的生日
                if (string.IsNullOrEmpty(this.PatientInfo.IDCard.ToString()))
                {
                    this.dtpBirthDay.Value = this.PatientInfo.Birthday;
                    this.txtAge.Enabled = true;
                    this.dtpBirthDay.Enabled = true;
                    
                }
                else 
                {
                   // if (this.PatientInfo.Birthday > DateTime.MinValue)
                    //{
                        //this.dtpBirthDay.Value = this.PatientInfo.Birthday;
                    //}

                    this.AuthenticationID(this.PatientInfo.IDCard);
                    this.txtAge.Enabled = false;
                    this.dtpBirthDay.Enabled = false;
                    
                }

                
                if (string.IsNullOrEmpty(this.PatientInfo.PhoneHome))
                {
                    this.txtTelephone.Text = this.PatientInfo.PhoneBusiness;
                }
                else
                {
                    this.txtTelephone.Text = this.PatientInfo.PhoneHome;
                }
                this.txtWorkPlace.Text = this.PatientInfo.CompanyName;
                this.cmbProfession.Tag = this.PatientInfo.Profession.ID;
                if (string.IsNullOrEmpty(this.PatientInfo.Profession.ID))
                {
                    this.cmbProfession.Text = this.PatientInfo.Profession.Name;
                }
                //this.txtWorkPlace.Text = this.PatientInfo.CompanyName;
                //this.txtSpecialAddress.Text = this.PatientInfo.AddressHome;
               
                return 1;
            }
            catch (Exception e)
            {
                MessageBox.Show(Language.Msg("患者信息赋值出错！"+e.Message));
                return -1;
            }
        }

        public override int SetValue(FS.HISFC.DCP.Object.CommonReport report)
        {
            report.Patient.MatherName = report.PatientParents;//家长姓名
            this.HomeArea = report.HomeArea;
            #region 省、市、区 2012-10-23 chengym
            this.cmbProvince.Tag = report.HomeProvince.ID;
            this.cmbCity.Tag = report.HomeCity.ID;
            this.cmbCountry.Tag = report.HomeCouty.ID;
            this.cmbTown.Tag = report.HomeTown.ID;
            this.txtVillage.Text = report.ExtendInfo1;

            // {2671947C-3F17-4eee-A72F-1479665EEB16}增加界面其他职业的赋值
            this.txtqtzy.Text = report.ExtendInfo2;
            #endregion
            return this.SetValue(report.Patient, FS.SOC.HISFC.DCP.Enum.PatientType.C);
        }

        /// <summary>
        /// 清空患者信息
        /// </summary>
        public override void Clear()
        {
            this.txtPatientName.Text = "";
            this.txtPatientID.Text = "";
            this.txtPatientParents.Text = "";
            this.dtpBirthDay.Value = this.sysdate;
            this.txtTelephone.Text = "";
            this.txtWorkPlace.Text = "";
            this.cmbProfession.Tag = "";
            this.txtCardNO.Text = "";
            //this.txtSpecialAddress.Text = "广东省珠海市香洲区不详乡镇";
            this.PatientInfo = null;
            this.rb1.Checked = true;
            // {2671947C-3F17-4eee-A72F-1479665EEB16}增加清空其他职业
            this.txtqtzy.Text = "";
            this.dtpBirthDay.Enabled = true;
            this.txtAge.Enabled = true;
            this.cmbProvince.Tag = "44000000";

            this.cmbCity.Tag = "44040000";
            this.cmbCountry.Tag = "44040200";
            this.cmbTown.Tag = "44040299";
            
            //this.cmbCity.SelectedIndex = 1;
            //this.cmbCountry.SelectedIndex = 1;
            //this.cmbTown.SelectedIndex = 1;
            this.txtVillage.Text = "";
        }

        #endregion

        #region 内部使用

        /// <summary>
        /// MessageBox
        /// </summary>
        /// <param name="message">提示信息</param>
        /// <param name="type">err错误 其它作标题</param>
        private void MyMessageBox(string message, string type)
        {
            switch (type)
            {
                case "err":
                    MessageBox.Show(message, "提示", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                    break;
                default:
                    MessageBox.Show(message, type, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    break;
            }
        }

        /// <summary>
        /// 验证身份证号码
        /// </summary>
        /// <param name="cardID"></param>
        private int AuthenticationID(string cardID)
        {
            if (!string.IsNullOrEmpty(cardID))
            {
                string err = "";
                if (FS.FrameWork.WinForms.Classes.Function.CheckIDInfo(cardID, ref err) == -1)
                {
                    MessageBox.Show(Language.Msg(err));
                    this.txtPatientID.Select();
                    this.txtPatientID.Focus();
                    return -1;
                }
                else
                {
                    string ID = cardID;
                    int year = 0;
                    int month = 0;
                    int day = 0;
                    DateTime dtBirth = System.DateTime.Now;
                    if (ID.Length == 15)
                    {
                        year = Convert.ToInt32("19" + ID.Substring(6, 2));
                        month = Convert.ToInt32(ID.Substring(8, 2));
                        day = Convert.ToInt32(ID.Substring(10, 2));
                    }
                    else if(ID.Length==18)
                    {
                        year = Convert.ToInt32(ID.Substring(6, 4));
                        month = Convert.ToInt32(ID.Substring(10, 2));
                        day = Convert.ToInt32(ID.Substring(12, 2));
                    }
                    dtBirth = new DateTime(year, month, day);

                    this.dtpBirthDay.Value = dtBirth;
                }
            }

            return 1;
        }

        #region 出生日期与生日

        /// <summary>
        /// 根据年龄算生日
        /// </summary>
        /// <param name="age"></param>
        /// <param name="ageUnit"></param>
        /// <returns></returns>
        private void ConvertBirthdayByAge(bool isUpdateAgeText)
        {
            DateTime birthDay = sysdate;
            string ageStr = this.txtAge.Text.Trim();
            int iyear = 0;
            int iMonth = 0;
            int iDay = 0;
            this.GetAgeNumber(ageStr, ref iyear, ref iMonth, ref iDay);

            //birthDay = birthDay.AddDays(-iDay).AddMonths(-iMonth).AddYears(-iyear);

            int year = birthDay.Year - iyear;
            int m = birthDay.Month - iMonth;
            if (m <= 0)
            {
                if (year > 0)
                {
                    year = year - 1;
                    DateTime dt = new DateTime(year, 1, 1);
                    m = dt.AddYears(1).AddDays(-1).Month + m;
                }
            }

            int day = birthDay.Day - iDay;
            if (day <= 0)
            {
                if (m > 0)
                {
                    m = m - 1;
                    DateTime dt = new DateTime(year, m + 1, 1).AddMonths(-1);
                    day = dt.AddMonths(1).AddDays(-1).Day + day;
                }
                else if (year > 0)
                {
                    year = year - 1;
                    DateTime dt = new DateTime(year, 1, 1);
                    m = dt.AddYears(1).AddDays(-1).Month - 1;
                    dt = new DateTime(year, m + 1, 1).AddMonths(-1);
                    day = dt.AddMonths(1).AddDays(-1).Day + day;
                }

                if (m <= 0)
                {
                    if (year > 0)
                    {
                        year = year - 1;
                        DateTime dt = new DateTime(year, 1, 1);
                        m = dt.AddYears(1).AddDays(-1).Month + m;
                    }
                }
            }

            birthDay = new DateTime(year, m, day);

            if (birthDay < dtpBirthDay.MinDate || birthDay > dtpBirthDay.MaxDate)
            {
                MessageBox.Show("年龄输入过大请重新输入！");
                this.txtAge.Text = this.GetAge(0, 0, 0);
                this.txtAge.Select(1, 1);
                return;
            }
            if (isUpdateAgeText)
            {
                this.txtAge.TextChanged -= new EventHandler(txtAge_TextChanged);
                this.txtAge.Text = this.GetAge(iyear, iMonth, iDay);
                this.dtpBirthDay.Value = birthDay;
                this.txtAge.TextChanged += new EventHandler(txtAge_TextChanged);
            }
            else
            {
                this.dtpBirthDay.ValueChanged -= new EventHandler(dtpBirthDay_ValueChanged);
                this.dtpBirthDay.Value = birthDay;
                this.dtpBirthDay.ValueChanged += new EventHandler(dtpBirthDay_ValueChanged);
            }
        }

        public string GetAge(int year, int month, int day)
        {
            return string.Format("{0}岁{1}月{2}天", year <= 0 ? "___" : year.ToString().PadLeft(3, '_'), year <= 0 && month <= 0 ? "__" : month.ToString().PadLeft(2, '_'), day.ToString().PadLeft(2, '_'));
        }

        public void GetAgeNumber(string age, ref int year, ref int month, ref int day)
        {
            year = 0;
            month = 0;
            day = 0;
            age = age.Replace("_", "");
            int ageIndex = age.IndexOf("岁");
            int monthIndex = age.IndexOf("月");
            int dayIndex = age.IndexOf("天");

            if (ageIndex > 0)
            {
                year = FS.FrameWork.Function.NConvert.ToInt32(age.Substring(0, ageIndex));
            }
            if (ageIndex >= 0 && monthIndex > 0 && monthIndex > ageIndex)
            {
                month = FS.FrameWork.Function.NConvert.ToInt32(age.Substring(ageIndex + 1, monthIndex - ageIndex - 1));
            }

            if (monthIndex >= 0 && dayIndex > 0 && dayIndex > monthIndex)
            {
                day = FS.FrameWork.Function.NConvert.ToInt32(age.Substring(monthIndex + 1, dayIndex - monthIndex - 1));
            }
        }

        private void txtAge_TextChanged(object sender, EventArgs e)
        {
            this.ConvertBirthdayByAge(false);
        }

        private void txtAge_Leave(object sender, EventArgs e)
        {
            this.ConvertBirthdayByAge(true);
        }

        private void dtpBirthDay_ValueChanged(object sender, EventArgs e)
        {
            //底层函数有问题
            //string age = this.dbManager.GetAge(this.dtpBirthDay.Value, true);

            int iyear = 0;
            int iMonth = 0;
            int iDay = 0;

            if (sysdate > this.dtpBirthDay.Value)
            {
                iyear = sysdate.Year - this.dtpBirthDay.Value.Year;
                if (iyear < 0)
                {
                    iyear = 0;
                }
                iMonth = sysdate.Month - this.dtpBirthDay.Value.Month;
                if (iMonth < 0)
                {
                    if (iyear > 0)
                    {
                        iyear = iyear - 1;
                        DateTime dt = new DateTime(this.dtpBirthDay.Value.Year + 1, 1, 1);
                        iMonth = dt.AddMonths(-1).Month + iMonth;//用当前的月份减
                    }

                    if (iMonth < 0)
                    {
                        iMonth = 0;
                    }
                }
                iDay = sysdate.Day - this.dtpBirthDay.Value.Day;
                if (iDay < 0)
                {
                    if (iMonth > 0)
                    {
                        iMonth = iMonth - 1;
                        DateTime dt = new DateTime(this.dtpBirthDay.Value.Year, this.dtpBirthDay.Value.Month, 1).AddMonths(1);
                        iDay = dt.AddDays(-1).Day + iDay;
                    }
                    else if (iyear > 0)
                    {
                        iyear = iyear - 1;
                        DateTime dt = new DateTime(this.dtpBirthDay.Value.Year + 1, 1, 1);
                        iMonth = dt.AddMonths(-1).Month - 1;
                        dt = new DateTime(this.dtpBirthDay.Value.Year, this.dtpBirthDay.Value.Month, 1).AddMonths(1);
                        iDay = dt.AddDays(-1).Day + iDay;
                    }
                    else
                    {
                        iDay = 0;
                    }
                }
            }

            //this.GetAgeNumber(age, ref iyear, ref iMonth, ref iDay);
            this.txtAge.TextChanged -= new EventHandler(txtAge_TextChanged);
            this.txtAge.Text = this.GetAge(iyear, iMonth, iDay);
            this.txtAge.TextChanged += new EventHandler(txtAge_TextChanged);

        }

        public string HomeArea
        {
            get
            {
                return this.rb1.Checked ? this.rb1.Text : this.rb2.Checked ? this.rb2.Text : this.rb3.Checked ? this.rb3.Text : this.rb4.Checked ? this.rb4.Text : this.rb5.Checked ? this.rb5.Text : this.rb6.Checked ? this.rb6.Text : "";
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    return;
                }

                if (this.rb1.Text == value)
                {
                    this.rb1.Checked = true;
                }
                else if (this.rb2.Text == value)
                {
                    this.rb2.Checked = true;
                }
                else if (this.rb3.Text == value)
                {
                    this.rb3.Checked = true;
                }
                else if (this.rb4.Text == value)
                {
                    this.rb4.Checked = true;
                }
                else if (this.rb5.Text == value)
                {
                    this.rb5.Checked = true;
                }
                else if (this.rb6.Text == value)
                {
                    this.rb6.Checked = true;
                }
            }
        }

        #endregion

        #endregion

        #endregion

        #region 事件

        /// <summary>
        /// 回车查找患者基本信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtClinicNO_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                this.txtCardNO.Text=this.txtCardNO.Text.PadLeft(10, '0');
                if (string.IsNullOrEmpty(this.txtCardNO.Text))
                {
                    MessageBox.Show(Language.Msg("请输入病历号！"));
                    return;
                }

                FS.SOC.HISFC.BizProcess.DCP.Patient patientProcess = new FS.SOC.HISFC.BizProcess.DCP.Patient();
                this.PatientInfo = patientProcess.GetPatientInfomation(this.txtCardNO.Text);

                if (this.PatientInfo != null)
                {
                    if (this.SetValue(this.PatientInfo,null) == -1)
                    {
                        return;
                    }
                }
                else
                {
                    MessageBox.Show(Language.Msg("无此人的信息"));
                    this.Clear();
                    return;
                }

            }
        }

        /// <summary>
        /// 回车核对身份证号作废，有一个逻辑错误，就是输入回车时会直接走ProcessDialogKey这个函数，不会去判断身份证。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void txtPatientID_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (e.KeyChar != (char)Keys.Enter)
        //    {
        //     return;
        //    }

        //    this.AuthenticationID(this.txtPatientID.Text);
         
        //}

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
              
                SendKeys.Send("{Tab}");

                return true;
            }

            return base.ProcessDialogKey(keyData);
        }

        public override void PrePrint()
        {
            this.gbPatientInfo.BackColor = Color.White;
            this.BackColor = Color.White;
            this.txtCardNO.BackColor = Color.White;
            //this.cl1.Visible = false;
            //this.cl2.Visible = false;
            //this.cl3.Visible = false;
            //this.cl4.Visible = false;
            //this.cl5.Visible = false;
            //this.cl6.Visible = false;
            //this.cl7.Visible = false;
            //this.cl8.Visible = false;
            //this.cl9.Visible = false;
            //this.cl10.Visible = false;
            //this.cl11.Visible = false;
            base.PrePrint();
        }

        public override void Printed()
        {
            this.gbPatientInfo.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            this.BackColor = System.Drawing.Color.FromArgb(158, 177, 201);
            //this.cl1.Visible = true;
            //this.cl2.Visible = true;
            //this.cl3.Visible = true;
            //this.cl4.Visible = true;
            //this.cl5.Visible = true;
            //this.cl6.Visible = true;
            //this.cl7.Visible = true;
            //this.cl8.Visible = true;
            //this.cl9.Visible = true;
            //this.cl10.Visible = true;
            //this.cl11.Visible = true;
            base.Printed();
        }

        #endregion
        #region 省，市，区combox选择
        /// <summary>
        /// 初始化省，市，区
        /// </summary>
        private void InitIgnoreInfo()
        {
            InitProvince();
            InitCity();
            InitCountry();
            InitZhen();
        }

        private int InitProvince()
        {
            this.alProvince = patObj.InitProvince("1");
            this.cmbProvince.AddItems(alProvince);
            this.cmbProvince.Tag = this.myProvince;
            //this.cmbProvince.Tag = "44000000";
            //this.cmbProvince.Text = "广东省";
            return 0;
        }

        private int InitCity()
        {

            this.alCity.Clear();
            if (this.cmbProvince.Tag == null || this.cmbProvince.Text == "") return -1;
            this.alCity = patObj.InitCityTownZhen("2", this.cmbProvince.Tag.ToString());
            this.cmbCity.AddItems(alCity);
            this.cmbCity.Tag = this.myCity;
            //this.cmbCity.Tag = "44040000";
            //this.cmbCity.Text = "珠海市";

            return 0;

        }
        private int InitCountry()
        {

            this.alCountry.Clear();
            if (this.cmbCity.Tag == null || this.cmbCity.Text == "") return -1;
            this.alCountry = patObj.InitCityTownZhen("3", this.cmbCity.Tag.ToString());
            this.cmbCountry.AddItems(alCountry);
            this.cmbCountry.Tag = this.myCounty;
            //this.cmbCountry.Tag = "44040200";
            //this.cmbCountry.Text = "香洲区";

            return 0;

        }

        private int InitZhen()
        {
            this.alTown.Clear();
            if (this.cmbCountry.Tag == null || this.cmbCountry.Text == "") return -1;
            this.alTown = patObj.InitCityTownZhen("4", this.cmbCountry.Tag.ToString());
            this.cmbTown.AddItems(alTown);
            this.cmbTown.Tag = this.myTown;
            //this.cmbTown.Tag = "44040299";
            //this.cmbTown.Text = "香洲区";
            return 0;

        } 

        private void cmbProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.cmbCity.Items.Clear();
            this.cmbCountry.Items.Clear();
            this.cmbTown.Items.Clear();
            this.alCity.Clear();
            this.alCity = patObj.InitCityTownZhen("2", this.cmbProvince.Tag.ToString());
            this.cmbCity.AddItems(alCity);
            this.txtSpecialAddress.Text = this.SetSpecialAddr();
            if (this.myProvince != "")//有维护省市常数
            {
                if (this.cmbProvince.Tag.ToString() != this.myProvince && !this.rb4.Checked)//外省
                {
                    this.rb4.Checked = true;
                }
            }
        }

        private void cmbCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbProvince.Tag == null || this.cmbProvince.Tag.ToString() == "")
            {
                //MessageBox.Show ("请选择省");
                return;
            }
            this.cmbTown.Items.Clear();
            this.cmbCountry.Items.Clear();

            this.alCountry.Clear();
            this.alCountry = patObj.InitCityTownZhen("3", this.cmbCity.Tag.ToString());
            this.cmbCountry.AddItems(alCountry);

            this.txtSpecialAddress.Text = this.SetSpecialAddr();
            if (this.myProvince != "")//有维护省市常数
            {
                if (this.cmbCity.Tag.ToString() != this.myCity && !this.rb3.Checked)//本省其他市县
                {
                    this.rb3.Checked = true;
                }
            }
        }

        private void cmbCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.cmbTown.Items.Clear();
            if (this.cmbCity.Tag == null || this.cmbCity.Tag.ToString() == "")
            {
                //		MessageBox.Show ("请选择市");
                return;
            }

            this.alTown.Clear();
            this.alTown = patObj.InitCityTownZhen("4", this.cmbCountry.Tag.ToString());
            this.cmbTown.AddItems(alTown);

            this.txtSpecialAddress.Text = this.SetSpecialAddr();
            if (this.myProvince != "")//有维护省市常数
            {
                if (this.cmbCountry.Tag.ToString() != this.myCounty && !this.rb2.Checked)//本市其他区
                {
                    this.rb2.Checked = true;
                }
                else if (this.cmbCountry.Tag.ToString() == this.myCounty && !this.rb1.Checked)
                {
                    this.rb1.Checked = true;
                }
            }

        }

        private void cmbTown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbTown.Tag == null || this.cmbTown.Tag.ToString() == "")
            {
                //MessageBox.Show("请选择县、区");
                return;
            }
            this.txtSpecialAddress.Text = this.SetSpecialAddr();
        }

        private void txtVillage_TextChanged(object sender, EventArgs e)
        {
            string addrStr = this.SetSpecialAddr() + this.txtVillage.Text;
            if (addrStr != "" && addrStr != null)
            {
                this.txtSpecialAddress.Text = this.SetSpecialAddr() + this.txtVillage.Text;// +"村(门牌号)";
            }
        }

        private string SetSpecialAddr()
        {
            string strSpeAddr = "";
            if (this.cmbProvince.Text != "")
                strSpeAddr += this.cmbProvince.Text.Trim();
            else
            {
               

            }
            if (this.cmbCity.Text != "")
                strSpeAddr += this.cmbCity.Text.Trim();
            else
            {
                
            }
            if (this.cmbCountry.Text != "")
                strSpeAddr += this.cmbCountry.Text.Trim();
            else
            {
               
            }
            if (this.cmbTown.Text != "")
                strSpeAddr += this.cmbTown.Text.Trim();
            

            return strSpeAddr;

        }
        #endregion

        //private void cmbProfession_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (this.cmbProfession.Tag != null)
        //    {
        //        if (this.hsNeedWorkName.Contains(this.cmbProfession.Tag.ToString()))
        //        {
        //            if (this.txtWorkPlace.Text.Trim() == "" || this.txtWorkPlace.Text.Trim() == "-")
        //            {
        //                this.txtWorkPlace.Focus();
        //                MessageBox.Show("职业为“" + this.cmbProfession.Text + "”需填写工作单位！");
        //            }
        //        }
        //    }
        //}
        // {2671947C-3F17-4eee-A72F-1479665EEB16}增加默认职业的选择事件，如果选择了其他，则其他职业为可填
        private void cmbProfession_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbProfession.Tag != null)
            {
                if (this.cmbProfession.Text == "其他")
                {
                    this.txtqtzy.Enabled = true;
                }
                else
                {
                    this.txtqtzy.Text = "";
                    this.txtqtzy.Enabled = false;
                }
            }

        }
        //// {F476F41B-D040-44f1-908D-022DCC942A55}增加身份证文本框光标离开事件，光标离开时判断身份证是否正确，且生日和年龄跟着身份证变化

        private void txtPatientID_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtPatientID.Text))
            {
                
                this.txtAge.Enabled = true;
                this.dtpBirthDay.Enabled = true;
            }
            else
            {
                this.AuthenticationID(this.txtPatientID.Text);
                this.txtAge.Enabled = false;
                this.dtpBirthDay.Enabled = false;
            }
        }



       
    }
}
