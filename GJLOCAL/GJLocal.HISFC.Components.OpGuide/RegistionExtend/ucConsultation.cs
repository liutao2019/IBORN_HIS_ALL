using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GJLocal.HISFC.Components.OpGuide.RegistionExtend
{
    public partial class ucConsultation : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucConsultation()
        {
            InitializeComponent();
        }

        #region 属性&变量

        /// <summary>
        /// 门诊流水号
        /// </summary>
        private string clinc_code = "";

        /// <summary>
        /// 门诊流水号
        /// </summary>
        public string Clinc_code
        {
            get { return clinc_code; }
            set { clinc_code = value; }
        }

        #region 业务变量
        /// <summary>
        /// 入出转
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.RADT radtManager = new FS.HISFC.BizProcess.Integrate.RADT();

        private FS.HISFC.BizLogic.Registration.Register regMgr = new FS.HISFC.BizLogic.Registration.Register();

        private FS.HISFC.BizLogic.Fee.Account accountMgr = new FS.HISFC.BizLogic.Fee.Account();
        #endregion
        #endregion

        #region 方法
        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        public int Init()
        {
            return 1;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        private int Query()
        {
            return 1;
        }

        public void Clean()
        {
            this.lbLastName.Text = "";
            this.lbFirstName.Text = "";
            this.lbMiddle.Text = "";
            this.lbAddress.Text = "";
            this.lbCity.Text = "";
            this.lbState.Text = "";
            this.lbPhone.Text = "";
            this.lbMobile.Text = "";
            this.lbBirthdate.Text = "";
            this.lbAge.Text = "";
            this.lbSex.Text = "";
            //this.lblSingle.BackColor = Color.White;
            //this.lblMairried.BackColor = Color.White;
            //this.lblDivorced.BackColor = Color.White;
            //this.lblSeparated.BackColor = Color.White;
            //this.lblWidowed.BackColor = Color.White;
            this.lbChildren.Text = "";
            this.lbPassport.Text = "";
            this.lbNationality.Text = "";
            this.lbBusiness.Text = "";
            this.lbAddress2.Text = "";
            this.lbBusinessPhone.Text = "";
            this.lbTypeOfWork.Text = "";
            this.lbEmergencyLocal.Text = "";
            this.lbPhone1.Text = "";
            this.lbEmergencyHome.Text = "";
            this.lbPhone2.Text = "";
            this.lbReferred.Text = "";
            this.lbEmail.Text = "";
            this.lbInsurance.Text = "";
            this.lblPayModle.Text = "";
            this.lbMarred.Text = "";
        }

        /// <summary>
        /// 界面赋值
        /// </summary>
        /// <returns></returns>
        public int SetValue()
        {
            System.Collections.ArrayList alpatient = this.regMgr.QueryPatient(this.clinc_code);
            FS.HISFC.Models.RADT.PatientInfo patientInfo = new FS.HISFC.Models.RADT.PatientInfo();
            FS.HISFC.Models.Registration.Register reg = alpatient[0] as FS.HISFC.Models.Registration.Register;
            FS.HISFC.Models.Account.AccountCard acTemp = accountMgr.GetAccountCard(reg.Card.ID, "Card_No");
            if (!string.IsNullOrEmpty(reg.PID.CardNO))
            {
                patientInfo = radtManager.QueryComPatientInfo(reg.PID.CardNO);
            }
            else if (acTemp == null)
            {
                patientInfo = radtManager.QueryComPatientInfo(reg.Card.ID);
            }
            else
            {
                patientInfo = radtManager.QueryComPatientInfo(acTemp.Patient.PID.CardNO);
            }
            this.lbLastName.Text = patientInfo.Last_Name;
            this.lbFirstName.Text = patientInfo.First_Name;
            this.lbMiddle.Text = patientInfo.Middle_Name;
            this.lbAddress.Text = patientInfo.AddressHome;
            this.lbCity.Text = patientInfo.City;
            this.lbState.Text = patientInfo.State;
            this.lbPhone.Text = patientInfo.PhoneHome;     //家庭电话
            this.lbMobile.Text = patientInfo.Mobile;//手机
            this.lbBirthdate.Text = patientInfo.Birthday.ToShortDateString();
            this.lbAge.Text=this.regMgr.GetAge(patientInfo.Birthday);
            //this.ConvertBirthdayByAge(patientInfo.Birthday);
            this.lbSex.Text = patientInfo.Sex.Name;            //性别
            this.lbMarred.Text = patientInfo.MaritalStatus.Name;//婚姻情况
            //switch (patientInfo.MaritalStatus.Name)
            //{
            //    case  "未婚":
            //        this.lblSingle.BackColor=Color.LightYellow;
            //        break;
            //    case "已婚":
            //        this.lblMairried.BackColor = Color.LightYellow;
            //        break;
            //    case "离异":
            //        this.lblDivorced.BackColor=Color.LightYellow;
            //         break;
            //    case "再婚":

            //         break;
            //    case "分居":
            //        this.lblSeparated.BackColor=Color.LightYellow;
            //         break;
            //    case "丧偶":
            //        this.lblWidowed.BackColor=Color.LightYellow;
            //         break;
            //}

            this.lbChildren.Text=patientInfo.NoOfChildren;//邮编-- 有几个孩子
            this.lbPassport.Text=patientInfo.IDCard;             //身份证号-- 护照号码
            //this.lbNationality.Text=patientInfo.Country.ID;       //国籍
            FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();
            System.Collections.ArrayList al1 = managerIntegrate.GetConstantList(FS.HISFC.Models.Base.EnumConstant.COUNTRY);
            foreach (FS.HISFC.Models.Base.Const con in al1)// {C40B6BF8-0D2A-4fa8-8486-53538CE503B6} lfhm
            {
                if (con.ID == patientInfo.Country.ID)
                {
                    this.lbNationality.Text = con.Name;
                }
            }
            this.lbBusiness.Text=patientInfo.CompanyName; //工作单位
            this.lbAddress2.Text=patientInfo.WorkAddress;            //工作地址)
            this.lbBusinessPhone.Text=patientInfo.PhoneBusiness; //单位电话
            this.lbTypeOfWork.Text=patientInfo.Profession.ID; //职业
            this.lbEmergencyLocal.Text = patientInfo.Kin.Name;        //联系人 
            this.lbPhone1.Text = patientInfo.Kin.RelationPhone;//联系人电话
            this.lbEmergencyHome.Text = patientInfo.LinkManHome;//紧急联系人（本国）
            this.lbPhone2.Text = patientInfo.LinkPhoneHome;//紧急联系人（本国）电话
            this.lbReferred.Text = patientInfo.KnowWay;
            this.lbEmail.Text = patientInfo.Email;//电子邮件
            this.lbInsurance.Text = patientInfo.Insurance.Name;
            this.lblPayModle.Text = patientInfo.Pact.Name;
            return 1;
        }

        /// <summary>
        /// 根据年龄算生日
        /// </summary>
        /// <param name="age"></param>
        /// <param name="ageUnit"></param>
        /// <returns></returns>
        private void ConvertBirthdayByAge(DateTime birthDay)
        {
            //DateTime birthDay = this.accountManager.GetDateTimeFromSysDateTime();
            if (birthDay == null || birthDay < new DateTime(1700, 1, 1))
            {
                return;
            }
            string ageStr = this.lbAge.Text.Trim();
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
            else
            {
                DateTime dt = new DateTime(year, m, 1);
                if (day > dt.AddMonths(1).AddDays(-1).Day)
                {
                    day = dt.AddMonths(1).AddDays(-1).Day;
                }
            }

            birthDay = new DateTime(year, m, day);

            this.lbAge.Text = this.GetAge(iyear, iMonth, iDay);
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
            if (ageIndex < 0 && monthIndex > 0 && monthIndex > ageIndex)
            {
                month = FS.FrameWork.Function.NConvert.ToInt32(age.Substring(0, monthIndex));//只有月
            }
            if (monthIndex >= 0 && dayIndex > 0 && dayIndex > monthIndex)
            {
                day = FS.FrameWork.Function.NConvert.ToInt32(age.Substring(monthIndex + 1, dayIndex - monthIndex - 1));
            }
            if (ageIndex < 0 && monthIndex < 0 && dayIndex > 0 && dayIndex > monthIndex)
            {
                day = FS.FrameWork.Function.NConvert.ToInt32(age.Substring(0, dayIndex));//只有日
            }
            if (ageIndex >= 0 && monthIndex < 0 && dayIndex > 0 && dayIndex > monthIndex)
            {
                day = FS.FrameWork.Function.NConvert.ToInt32(age.Substring(ageIndex + 1, dayIndex - ageIndex - 1));//只有年，日
            }
        }

        #endregion
    }
}
