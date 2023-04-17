using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Management;

namespace FS.SOC.Local.Order.OutPatientOrder.GYZL.IOrderExtendModule
{

    public partial class ucCardInfo : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        #region ������

        public ucCardInfo()
        {
            InitializeComponent();
            this.txtAge.Leave += new EventHandler(txtAge_Leave);
            this.txtAge.TextChanged += new EventHandler(txtAge_TextChanged);
            this.dtpBirthDay.ValueChanged += new EventHandler(dtpBirthDay_ValueChanged);
            this.patientInfo = null;
        }

        public ucCardInfo(FS.HISFC.Models.RADT.Patient patient)
        {
            InitializeComponent();
            this.txtAge.Leave += new EventHandler(txtAge_Leave);
            this.txtAge.TextChanged += new EventHandler(txtAge_TextChanged);
            this.dtpBirthDay.ValueChanged += new EventHandler(dtpBirthDay_ValueChanged);
            this.patientInfo = patient;
            this.Init(DateTime.Now);
        }

        #endregion

        #region �����

        /// <summary>
        /// ������Ϣʵ��
        /// </summary>
        private FS.HISFC.Models.RADT.Patient patientInfo = null;

        private DataBaseManger dbManager = new DataBaseManger();

        private FS.HISFC.BizProcess.Integrate.RADT radtMgr = new FS.HISFC.BizProcess.Integrate.RADT();
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
        public int Init(DateTime sysdate)
        {
            ArrayList alSex = FS.HISFC.Models.Base.SexEnumService.List();
            if (alSex == null || alSex.Count == 0)
            {
                MessageBox.Show(Language.Msg("��ȡ�Ա���Ϣ����"));
                return -1;
            }
            this.cmbSex.AddItems(alSex);
            this.SetValue(this.patientInfo);
            return 1;
        }

        /// <summary>
        /// �жϻ�����Ϣ�Ƿ�����
        /// </summary>
        /// <returns>-1 ������ 0 ����</returns>
        public int Valid(ref string msg, ref Control control)
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
            if (this.AuthenticationID(this.txtPatientID.Text) == -1)
            {
                msg = Language.Msg("���֤�������");
                control = this.txtPatientID;
                return -1;
            }
            if (this.dtpBirthDay.Value == null || this.dtpBirthDay.Value > DateTime.Now)
            {
                msg = Language.Msg("�������ڴ������飡");
                control = this.dtpBirthDay;
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
            return 0;
        }

        /// <summary>
        /// ȡ������Ϣ
        /// </summary>
        /// <param name="patient">����Ļ�����Ϣʵ��</param>
        /// <returns></returns>
        public int GetValue(ref FS.HISFC.Models.RADT.PatientInfo patient)
        {
            if (patient == null)
            {
                return -1;
            }
            string msg = "";
            Control c = null;
            if (this.Valid(ref msg, ref c) == -1)
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
                patient.Name = this.txtPatientName.Text;
                patient.Sex.ID = this.cmbSex.Tag.ToString();
                patient.IDCard = this.txtPatientID.Text;
                patient.Birthday = this.dtpBirthDay.Value;

                //ȡ����
                int year = 0;
                int month = 0;
                int day = 0;
                this.GetAgeNumber(this.txtAge.Text, ref year, ref month, ref day);

                if (year > 0)
                {
                    patient.Age = year.ToString();
                }
                else if (month > 0)
                {
                    patient.Age = month.ToString();
                }
                else if (day > 0)
                {
                    patient.Age = day.ToString();
                }
                else
                {
                    MessageBox.Show("���䲻��С����");
                    this.txtAge.Select();
                    return -1;
                }
                if (string.IsNullOrEmpty(patient.IDCard))
                {
                    MessageBox.Show("���֤����Ϊ�գ�");
                    this.txtPatientID.SelectAll();
                    return -1;
                }

                //��ַ
                patient.AddressHome = this.txtSpecialAddress.Text;
                patient.PhoneHome = this.txtTelephone.Text;
                patient.PID.CardNO = this.txtCardNO.Text;
                if (!string.IsNullOrEmpty(this.tbKinName.Text))
                {
                    patient.Kin.Name = this.tbKinName.Text;
                }
                if (!string.IsNullOrEmpty(this.tbKinIdeno.Text))
                {
                    patient.Kin.Memo = this.tbKinName.Text;
                }
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
        public int SetValue(FS.HISFC.Models.RADT.Patient patient)
        {
            if (patient == null)
            {
                return -1;
            }

            this.PatientInfo = patient;

            try
            {
                this.txtCardNO.Text = this.PatientInfo.PID.CardNO;
                this.txtPatientName.Text = this.PatientInfo.Name;
                this.txtPatientID.Text = this.PatientInfo.IDCard;
                this.cmbSex.Tag = this.PatientInfo.Sex.ID;
                this.cmbSex.Text = this.PatientInfo.Sex.Name;
                if (this.PatientInfo.Birthday > DateTime.MinValue)
                {
                    this.dtpBirthDay.Value = this.PatientInfo.Birthday;
                }
                if (string.IsNullOrEmpty(this.PatientInfo.PhoneHome))
                {
                    this.txtTelephone.Text = this.PatientInfo.PhoneBusiness;
                }
                else
                {
                    this.txtTelephone.Text = this.PatientInfo.PhoneHome;
                }
                this.txtSpecialAddress.Text = this.PatientInfo.AddressHome;
                FS.HISFC.Models.RADT.PatientInfo tmpInfo = new FS.HISFC.Models.RADT.PatientInfo();
                if (!string.IsNullOrEmpty(this.PatientInfo.PID.CardNO))
                {
                    tmpInfo = this.radtMgr.QueryComPatientInfo(this.PatientInfo.PID.CardNO);
                }
                if (tmpInfo != null)
                {
                    this.txtPatientID.Text = tmpInfo.IDCard;
                    this.txtTelephone.Text = tmpInfo.PhoneHome;
                    this.txtSpecialAddress.Text = tmpInfo.AddressHome;
                    this.tbKinName.Text = tmpInfo.Kin.Name;//������
                    this.tbKinIdeno.Text = tmpInfo.Kin.Memo;//���������֤
                }
                return 1;
            }
            catch (Exception e)
            {
                MessageBox.Show(Language.Msg("������Ϣ��ֵ����" + e.Message));
                return -1;
            }
        }

        /// <summary>
        /// ��ջ�����Ϣ
        /// </summary>
        public void Clear()
        {
            this.txtPatientName.Text = "";
            this.txtPatientID.Text = "";
            this.dtpBirthDay.Value = DateTime.Now;
            this.txtTelephone.Text = "";
            this.txtCardNO.Text = "";
            this.txtSpecialAddress.Text = "";
            this.txtAnesthesiaCardNo.Text = "";
            this.txtCreateCardDate.Text = DateTime.Now.ToShortDateString();
            this.PatientInfo = null;
        }

        public int SavePatientInfo()
        {
            try
            {
                FS.HISFC.Models.RADT.PatientInfo curPatient = new FS.HISFC.Models.RADT.PatientInfo();
                if (this.GetValue(ref curPatient) >= 0)
                {
                    FS.FrameWork.Management.PublicTrans.BeginTransaction();
                    this.radtMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                    if (this.radtMgr.RegisterComPatient(curPatient) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(Language.Msg("���»�����Ϣ����" + radtMgr.Err));
                        return -1;
                    }
                    FS.FrameWork.Management.PublicTrans.Commit();
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(Language.Msg("���»�����Ϣ����" + e.Message));
                return -1;
            }
            return 1;
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
            string err = "";
            string idNO = "";
            string idNOTmp = string.Empty;
            idNO = cardID;
            if (idNO.Length == 15)
            {
                idNOTmp = FS.FrameWork.WinForms.Classes.Function.TransIDFrom15To18(idNO);
            }
            else if (idNO.Length == 18)
            {
                idNOTmp = idNO;
            }
            else if (idNO.Length == 10 || idNO.Length == 11)
            {
                if (CheckHKID(idNO) == false)
                {
                    MessageBox.Show("��������Ч����۾������֤");
                    this.tbKinIdeno.Focus();
                    return -1;
                }
                else
                {
                    return 1;
                }
            }
            if (!string.IsNullOrEmpty(cardID))
            {
                if (FS.FrameWork.WinForms.Classes.Function.CheckIDInfo(idNOTmp, ref err) == -1)
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
                    else
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
            DateTime birthDay = DateTime.Now;
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

            if (DateTime.Now > this.dtpBirthDay.Value)
            {
                iyear = DateTime.Now.Year - this.dtpBirthDay.Value.Year;
                if (iyear < 0)
                {
                    iyear = 0;
                }
                iMonth = DateTime.Now.Month - this.dtpBirthDay.Value.Month;
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
                iDay = DateTime.Now.Day - this.dtpBirthDay.Value.Day;
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
                this.txtCardNO.Text = this.txtCardNO.Text.PadLeft(10, '0');
                if (string.IsNullOrEmpty(this.txtCardNO.Text))
                {
                    MessageBox.Show(Language.Msg("�����벡���ţ�"));
                    return;
                }

                //FS.SOC.HISFC.BizProcess.DCP.Patient patientProcess = new FS.SOC.HISFC.BizProcess.DCP.Patient();
                //this.PatientInfo = patientProcess.GetPatientInfomation(this.txtCardNO.Text);
                //δ���

                if (this.PatientInfo != null)
                {
                    if (this.SetValue(this.PatientInfo, null) == -1)
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

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                if (this.txtPatientID.Focus())
                {
                    this.AuthenticationID(this.txtPatientID.Text);
                }
                if (this.tbKinIdeno.Focus())
                {
                    this.AuthenticationID(this.tbKinIdeno.Text);
                }
                SendKeys.Send("{Tab}");
                
                return true;
            }

            return base.ProcessDialogKey(keyData);
        }

        #endregion

        #region ������֤

        /// <summary>
        /// ��ȡ�ַ�������
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private int GetAppNumber(char str)
        {
            /*
             * 
             *        �ո� 58 I 18 R 27
                        A 10 J 19 S 28
                        B 11 K 20 T 29
                        C 12 L 21 U 30
                        D 13 M 22 V 31
                        E 14 N 23 W 32
                        F 15 O 24 X 33
                        G 16 P 25 Y 34
                        H 17 Q 26 Z 35

             **/
            switch (str)
            {
                case '@':
                case ' ':
                    return 58;
                case 'A':
                case 'a':
                    return 10;
                case 'B':
                case 'b':
                    return 11;
                case 'C':
                case 'c':
                    return 12;
                case 'D':
                case 'd':
                    return 13;
                case 'E':
                case 'e':
                    return 14;
                case 'F':
                case 'f':
                    return 15;
                case 'G':
                case 'g':
                    return 16;
                case 'H':
                case 'h':
                    return 17;
                case 'I':
                case 'i':
                    return 18;
                case 'J':
                case 'j':
                    return 19;
                case 'K':
                case 'k':
                    return 20;
                case 'L':
                case 'l':
                    return 21;
                case 'M':
                case 'm':
                    return 22;
                case 'N':
                case 'n':
                    return 23;
                case 'O':
                case 'o':
                    return 24;
                case 'P':
                case 'p':
                    return 25;
                case 'Q':
                case 'q':
                    return 26;
                case 'R':
                case 'r':
                    return 27;
                case 'S':
                case 's':
                    return 28;
                case 'T':
                case 't':
                    return 29;
                case 'U':
                case 'u':
                    return 30;
                case 'V':
                case 'v':
                    return 31;
                case 'W':
                case 'w':
                    return 32;
                case 'X':
                case 'x':
                    return 33;
                case 'Y':
                case 'y':
                    return 34;
                case 'Z':
                case 'z':
                    return 35;
                default:
                    return 58;

            }
        }

        /// <summary>
        /// ��֤������֤
        /// </summary>
        /// <param name="CardNumber"></param>
        /// <returns></returns>
        private bool CheckHKID(string CardNumber)
        {
            if (string.IsNullOrEmpty(CardNumber))
            {
                return false;
            }

            string regex = @"^[a-zA-Z]{1,2}\d{6}\([0-9a-zAZ-Z]\)$";
            if (System.Text.RegularExpressions.Regex.IsMatch(CardNumber, regex))
            {
                if (CardNumber.Length == 10)
                {
                    int sum = 9 * GetAppNumber('@') +
                                    8 * GetAppNumber(CardNumber[0]) +
                                    7 * int.Parse(CardNumber[1].ToString()) +
                                    6 * int.Parse(CardNumber[2].ToString()) +
                                    5 * int.Parse(CardNumber[3].ToString()) +
                                    4 * int.Parse(CardNumber[4].ToString()) +
                                    3 * int.Parse(CardNumber[5].ToString()) +
                                    2 * int.Parse(CardNumber[6].ToString());

                    int checkdigit = 11 - sum % 11;
                    char checkdigitstr = ' ';
                    if (checkdigit == 10)
                    {
                        checkdigitstr = 'A';
                    }
                    if (checkdigit == 11)
                    {
                        checkdigitstr = '0';
                    }

                    if (checkdigitstr == CardNumber[8])
                    {
                        return true;
                    }
                }
                else if (CardNumber.Length == 11)
                {
                    int sum = 9 * GetAppNumber(CardNumber[0]) +
                                    8 * GetAppNumber(CardNumber[1]) +
                                    7 * int.Parse(CardNumber[2].ToString()) +
                                    6 * int.Parse(CardNumber[3].ToString()) +
                                    5 * int.Parse(CardNumber[4].ToString()) +
                                    4 * int.Parse(CardNumber[5].ToString()) +
                                    3 * int.Parse(CardNumber[6].ToString()) +
                                    2 * int.Parse(CardNumber[7].ToString());

                    int checkdigit = 11 - sum % 11;
                    char checkdigitstr = ' ';
                    if (checkdigit == 10)
                    {
                        checkdigitstr = 'A';
                    }
                    if (checkdigit == 11)
                    {
                        checkdigitstr = '0';
                    }

                    if (checkdigitstr == CardNumber[9])
                    {
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }

            return false;
        }

        #endregion
    }
}
