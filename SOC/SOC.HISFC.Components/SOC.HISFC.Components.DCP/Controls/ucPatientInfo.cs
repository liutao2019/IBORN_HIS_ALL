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
    /// [��������: ������Ϣuc]<br></br>
    /// [�� �� ��: zj]<br></br>
    /// [����ʱ��: 2008-09-17]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='chengym' 
    ///		�޸�ʱ��='2012-10-23' 
    ///		�޸�Ŀ��='���Ի���ϸ��ַ��д�ķ�ʽ����ǿ�����ƺ�'
    ///		�޸�����='������ַ�洢��com_address ��ֻ�ܵ��룬��ά������'
    ///  />
    /// </summary>
    public partial class ucPatientInfo : ucBaseMainReport
    {
        #region ������

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

        #region �����

        /// <summary>
        /// ������Ϣʵ��
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
        // {2671947C-3F17-4eee-A72F-1479665EEB16}����Ĭ�����

        private string myTown = "";
        
        //��Ҫ��λ����
        private System.Collections.Hashtable hsNeedWorkName = new Hashtable();
        #endregion

        #region ����

        /// <summary>
        /// ������Ϣʵ��
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
        /// �Ƿ�����޸Ļ�����Ϣ
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

        #region ����

        #region �ⲿ����

        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <returns></returns>
        public override int Init(DateTime sysdate)
        {
            base.Init(sysdate);//�ȳ�ʼ������ķ�����

            ArrayList alSex = FS.HISFC.Models.Base.SexEnumService.List();
            if (alSex == null || alSex.Count == 0)
            {
                MessageBox.Show(Language.Msg("��ȡ�Ա���Ϣ����"));
                return -1;
            }
            this.cmbSex.AddItems(alSex);

            FS.SOC.HISFC.BizProcess.DCP.Common commonProcess = new FS.SOC.HISFC.BizProcess.DCP.Common();
            //���⣬����ϵͳһ��
            ArrayList alProfession = commonProcess.QueryConstantList("PATIENTJOB");
            if (alProfession == null || alProfession.Count == 0)
            {
                MessageBox.Show(Language.Msg("��ȡְҵ��Ϣ����"));
                return -1;
            }
            this.cmbProfession.AddItems(alProfession);
            hsNeedWorkName = new Hashtable();
            foreach (FS.HISFC.Models.Base.Const jobInfo in alProfession)
            {
                if (jobInfo.Memo.IndexOf("�蹤����λ", 0) != -1)
                {
                    hsNeedWorkName.Add(jobInfo.ID, null);
                }
            }
            //��ȡҽԺ���ڵ�ʡ����  2012-10-23 chengym
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


                // {2671947C-3F17-4eee-A72F-1479665EEB16}����Ĭ�����
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
        /// �жϻ�����Ϣ�Ƿ�����
        /// </summary>
        /// <returns>-1 ������ 0 ����</returns>
        public int Valid(ref string msg,ref Control control)
        {
            if (string.IsNullOrEmpty(this.txtCardNO.Text))
            {
                msg = Language.Msg("�����벡���ţ�");
                control = this.txtCardNO;
                return -1;
            }
            if (string.IsNullOrEmpty(this.txtPatientName.Text))
            {
                msg = Language.Msg("����д������");
                control = this.txtPatientName;
                return -1;
            }
            if (this.cmbSex.Tag == null || string.IsNullOrEmpty(this.cmbSex.Text))
            {
                msg = Language.Msg("��ѡ���Ա�");
                control = this.cmbSex;
                return -1;
            }
            if (this.AuthenticationID(this.txtPatientID.Text)==-1)
            {
                msg = Language.Msg("���֤�������");
                control = this.txtPatientID;
                return -1;
            }
            if (this.dtpBirthDay.Value == null || this.dtpBirthDay.Value > this.sysdate)
            {
                msg = Language.Msg("�������ڴ������飡");
                control = this.dtpBirthDay;
                return -1;
            }
            if (this.sysdate.Year - this.dtpBirthDay.Value.Year < 15 && string.IsNullOrEmpty(this.txtPatientParents.Text))
            {
                msg = Language.Msg("14���꣨��14���꣩���µ�����д�ҳ�������");
                control = this.txtPatientParents;
                return -1;
            }
            if (string.IsNullOrEmpty(this.txtSpecialAddress.Text))
            {
                msg = Language.Msg("����д��ͥ��ϸ��ַ��");
                control = this.txtSpecialAddress;
                return -1;
            }
            if (string.IsNullOrEmpty(this.txtTelephone.Text))
            {
                msg = Language.Msg("����д��ϵ�绰��");
                control = this.txtTelephone;
                return -1;
            }
            if (string.IsNullOrEmpty(this.cmbProfession.Text) && (this.cmbProfession.Tag == null || string.IsNullOrEmpty(this.cmbProfession.Tag.ToString())))
            {
                msg = Language.Msg("��ѡ��ְҵ��");
                control = this.cmbProfession;
                return -1;
            }

            //{2671947C-3F17-4eee-A72F-1479665EEB16}�����ж����ְҵѡ����������������д����ְҵ
            if (string.IsNullOrEmpty(this.txtqtzy.Text) && (this.cmbProfession.Text== "����"))
            {
                msg = Language.Msg("����д����ְҵ��");
                control = this.txtqtzy;
                return -1;
            }


            //{2671947C-3F17-4eee-A72F-1479665EEB16}�����ж����ѡ������Ҫ������λ��ְҵ��������д������λ
            if (hsNeedWorkName.Contains(this.cmbProfession.Tag.ToString()) && string.IsNullOrEmpty(this.txtWorkPlace.Text))
            {
                msg = Language.Msg("ְҵΪ��" + this.cmbProfession.Text + "������д������λ��");
                control = this.txtWorkPlace;
                return -1;
            }
            if (string.IsNullOrEmpty(this.HomeArea))
            {
                msg = Language.Msg("��ѡ������������");
                control = this.rb1;
                return -1;
            }
            if (this.rb1.Checked|| this.rb2.Checked || this.rb3.Checked || this.rb4.Checked)//����
            {
                if (this.cmbProvince.Tag == null || string.IsNullOrEmpty(this.cmbProvince.Tag.ToString()))
                {
                    msg = Language.Msg("��ѡ��������ʡ��");
                    control = this.cmbProvince;
                    return -1;
                }
                // {2671947C-3F17-4eee-A72F-1479665EEB16}�����ж�������Ϊ������
                if (this.cmbCity.Tag == null || string.IsNullOrEmpty(this.cmbCity.Tag.ToString()))
                {
                    msg = Language.Msg("��ѡ���������У�");
                    control = this.cmbCity;
                    return -1;
                }
                if (this.cmbCountry.Tag == null || string.IsNullOrEmpty(this.cmbCountry.Tag.ToString()))
                {
                    msg = Language.Msg("��ѡ����������(��)��");
                    control = this.cmbCountry;
                    return -1;
                }
                if (this.cmbTown.Tag == null || string.IsNullOrEmpty(this.cmbTown.Tag.ToString()))
                {
                    msg = Language.Msg("��ѡ����������(�򡢽ֵ�)��");
                    control = this.cmbTown;
                    return -1;
                }
            }
            return 0;
        }

        /// <summary>
        /// ȡ������Ϣ
        /// </summary>
        /// <param name="patient">����Ļ�����Ϣʵ��</param>
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
                report.PatientParents = this.txtPatientParents.Text.Trim();//�ҳ�����
                report.HomeArea=this.HomeArea;
                //ȡ����
                int year = 0;
                int month = 0;
                int day = 0;
                this.GetAgeNumber(this.txtAge.Text, ref year, ref month, ref day);

                if (year > 0)
                {
                    report.Patient.Age = year.ToString();
                    report.AgeUnit = "��";
                }
                else if (month > 0)
                {
                    report.Patient.Age = month.ToString();
                    report.AgeUnit = "��";
                }
                else if (day > 0)
                {
                    report.Patient.Age = day.ToString();
                    report.AgeUnit = "��";
                }
                else
                {
                    MessageBox.Show("���䲻��С����");
                    this.txtAge.Select();
                    return -1;
                }
                #region ʡ���С��� 2012-10-23 chengym
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
                //��ַ
                report.Patient.AddressHome = this.txtSpecialAddress.Text;
                report.Patient.PhoneHome = this.txtTelephone.Text;
                report.Patient.Profession.ID = this.cmbProfession.Tag == null ? "" : this.cmbProfession.Tag.ToString();
                report.Patient.Profession.Name = this.cmbProfession.Text;
                report.Patient.CompanyName = this.txtWorkPlace.Text;
                //report.ExtendInfo1 = this.txtSpecialAddress.Text;//ǰ��report.Patient.AddressHome����Ϊ�������ƺ�
                report.ExtendInfo1 = this.txtVillage.Text;
                report.Patient.PID.CardNO = this.txtCardNO.Text;
                // {2671947C-3F17-4eee-A72F-1479665EEB16}������������ְҵ�������ݿ�
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
        /// ��������Ϣ
        /// </summary>
        /// <param name="patient">������Ϣʵ��</param>
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

                //{F476F41B-D040-44f1-908D-022DCC942A55}�����жϣ�������֤��Ϊ����ֱ��ȡ���֤�ϵ����գ�������֤Ϊ����ȡ�Һ�ʱ������
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
                MessageBox.Show(Language.Msg("������Ϣ��ֵ����"+e.Message));
                return -1;
            }
        }

        public override int SetValue(FS.HISFC.DCP.Object.CommonReport report)
        {
            report.Patient.MatherName = report.PatientParents;//�ҳ�����
            this.HomeArea = report.HomeArea;
            #region ʡ���С��� 2012-10-23 chengym
            this.cmbProvince.Tag = report.HomeProvince.ID;
            this.cmbCity.Tag = report.HomeCity.ID;
            this.cmbCountry.Tag = report.HomeCouty.ID;
            this.cmbTown.Tag = report.HomeTown.ID;
            this.txtVillage.Text = report.ExtendInfo1;

            // {2671947C-3F17-4eee-A72F-1479665EEB16}���ӽ�������ְҵ�ĸ�ֵ
            this.txtqtzy.Text = report.ExtendInfo2;
            #endregion
            return this.SetValue(report.Patient, FS.SOC.HISFC.DCP.Enum.PatientType.C);
        }

        /// <summary>
        /// ��ջ�����Ϣ
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
            //this.txtSpecialAddress.Text = "�㶫ʡ�麣����������������";
            this.PatientInfo = null;
            this.rb1.Checked = true;
            // {2671947C-3F17-4eee-A72F-1479665EEB16}�����������ְҵ
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

        #region �ڲ�ʹ��

        /// <summary>
        /// MessageBox
        /// </summary>
        /// <param name="message">��ʾ��Ϣ</param>
        /// <param name="type">err���� ����������</param>
        private void MyMessageBox(string message, string type)
        {
            switch (type)
            {
                case "err":
                    MessageBox.Show(message, "��ʾ", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                    break;
                default:
                    MessageBox.Show(message, type, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    break;
            }
        }

        /// <summary>
        /// ��֤���֤����
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

        #region ��������������

        /// <summary>
        /// ��������������
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
                MessageBox.Show("��������������������룡");
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
            return string.Format("{0}��{1}��{2}��", year <= 0 ? "___" : year.ToString().PadLeft(3, '_'), year <= 0 && month <= 0 ? "__" : month.ToString().PadLeft(2, '_'), day.ToString().PadLeft(2, '_'));
        }

        public void GetAgeNumber(string age, ref int year, ref int month, ref int day)
        {
            year = 0;
            month = 0;
            day = 0;
            age = age.Replace("_", "");
            int ageIndex = age.IndexOf("��");
            int monthIndex = age.IndexOf("��");
            int dayIndex = age.IndexOf("��");

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
            //�ײ㺯��������
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
                        iMonth = dt.AddMonths(-1).Month + iMonth;//�õ�ǰ���·ݼ�
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

        #region �¼�

        /// <summary>
        /// �س����һ��߻�����Ϣ
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
                    MessageBox.Show(Language.Msg("�����벡���ţ�"));
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
                    MessageBox.Show(Language.Msg("�޴��˵���Ϣ"));
                    this.Clear();
                    return;
                }

            }
        }

        /// <summary>
        /// �س��˶����֤�����ϣ���һ���߼����󣬾�������س�ʱ��ֱ����ProcessDialogKey�������������ȥ�ж����֤��
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
        #region ʡ���У���comboxѡ��
        /// <summary>
        /// ��ʼ��ʡ���У���
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
            //this.cmbProvince.Text = "�㶫ʡ";
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
            //this.cmbCity.Text = "�麣��";

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
            //this.cmbCountry.Text = "������";

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
            //this.cmbTown.Text = "������";
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
            if (this.myProvince != "")//��ά��ʡ�г���
            {
                if (this.cmbProvince.Tag.ToString() != this.myProvince && !this.rb4.Checked)//��ʡ
                {
                    this.rb4.Checked = true;
                }
            }
        }

        private void cmbCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbProvince.Tag == null || this.cmbProvince.Tag.ToString() == "")
            {
                //MessageBox.Show ("��ѡ��ʡ");
                return;
            }
            this.cmbTown.Items.Clear();
            this.cmbCountry.Items.Clear();

            this.alCountry.Clear();
            this.alCountry = patObj.InitCityTownZhen("3", this.cmbCity.Tag.ToString());
            this.cmbCountry.AddItems(alCountry);

            this.txtSpecialAddress.Text = this.SetSpecialAddr();
            if (this.myProvince != "")//��ά��ʡ�г���
            {
                if (this.cmbCity.Tag.ToString() != this.myCity && !this.rb3.Checked)//��ʡ��������
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
                //		MessageBox.Show ("��ѡ����");
                return;
            }

            this.alTown.Clear();
            this.alTown = patObj.InitCityTownZhen("4", this.cmbCountry.Tag.ToString());
            this.cmbTown.AddItems(alTown);

            this.txtSpecialAddress.Text = this.SetSpecialAddr();
            if (this.myProvince != "")//��ά��ʡ�г���
            {
                if (this.cmbCountry.Tag.ToString() != this.myCounty && !this.rb2.Checked)//����������
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
                //MessageBox.Show("��ѡ���ء���");
                return;
            }
            this.txtSpecialAddress.Text = this.SetSpecialAddr();
        }

        private void txtVillage_TextChanged(object sender, EventArgs e)
        {
            string addrStr = this.SetSpecialAddr() + this.txtVillage.Text;
            if (addrStr != "" && addrStr != null)
            {
                this.txtSpecialAddress.Text = this.SetSpecialAddr() + this.txtVillage.Text;// +"��(���ƺ�)";
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
        //                MessageBox.Show("ְҵΪ��" + this.cmbProfession.Text + "������д������λ��");
        //            }
        //        }
        //    }
        //}
        // {2671947C-3F17-4eee-A72F-1479665EEB16}����Ĭ��ְҵ��ѡ���¼������ѡ����������������ְҵΪ����
        private void cmbProfession_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbProfession.Tag != null)
            {
                if (this.cmbProfession.Text == "����")
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
        //// {F476F41B-D040-44f1-908D-022DCC942A55}�������֤�ı������뿪�¼�������뿪ʱ�ж����֤�Ƿ���ȷ�������պ�����������֤�仯

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
