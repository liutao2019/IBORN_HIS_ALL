using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.Speciment;
using FS.HISFC.BizLogic.Speciment;
using System.Collections;

namespace FS.HISFC.Components.Speciment
{
    public partial class ucPatientInfo : UserControl
    {
        private SpecPatient specPatient;
        private PatientManage patientManage;
        public FS.HISFC.Components.Common.Controls.ucQueryInpatientNo ucQueryInPatientNo;
        public bool IsExisted;
        private FS.HISFC.BizProcess.Integrate.RADT radtIntegrate;

        public ucPatientInfo()
        {
            InitializeComponent();
            specPatient = new SpecPatient();
            patientManage = new PatientManage();
            IsExisted = false;
            ucQueryInPatientNo = new FS.HISFC.Components.Common.Controls.ucQueryInpatientNo();
            radtIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();
        }

        public SpecPatient SpecPatient
        {
            get
            {
                return specPatient;
            }
            set
            {
                specPatient = value;
            }
        }

        /// <summary>
        /// ��ҳ���϶�ȡ�걾�ⲡ����Ϣ
        /// </summary>
        public SpecPatient SetPatient()
        {            
            specPatient.PatientName = txtPatName.Text.TrimStart().TrimEnd();          
            string birthday = cmbYear.Text.Trim() + "-" + cmbMonth.Text.Trim() + "-" + cmbDate.Text.Trim();
            DateTime dtBirthday = Convert.ToDateTime(birthday);
            specPatient.Birthday = dtBirthday;
            specPatient.ContactNum = txtContactNum.Text.Trim();
            specPatient.HomePhoneNum = txtPhoneNum.Text.Trim();
            if (txtNationality.Tag != null)
                specPatient.Nationality = txtNationality.Tag.ToString();
            if (txtNation.Tag != null)
                specPatient.Nation = txtNation.Tag.ToString();
            specPatient.IcCardNo = txtIcCardNO.Text.Trim();
            if (rbtFemale.Checked)
                specPatient.Gender = 'F';
            else
                specPatient.Gender = 'M';
            if (txtBloodType.Tag != null)
                specPatient.BloodType = txtBloodType.Tag.ToString();
            specPatient.Home = txtHome.Text;
            specPatient.Address = txtAddress.Text;
            if (txtMaritalStatus.Tag != null)
                specPatient.IsMarried = txtMaritalStatus.Tag.ToString();
            specPatient.Comment = txtComment.Text;
            specPatient.IdCardNo = txtIDNum.Text;
            specPatient.InHosNum = txtInHosNum.Text;
            specPatient.CardNo = txtCardNo.Text.Trim();            
            return specPatient;
        }

        public void ClearControlContent()
        {
            foreach (Control c in this.Controls)
            {
                if (c.Name.Contains("txt"))
                {
                    c.Text = "";
                }
            }
            ucQueryInPatientNo.txtInputCode.Text = "";
            cmbYear.Text = "1911";
            cmbMonth.Text = "1";
            cmbDate.Text = "2";
            nudOld.Value = 100;
        }

        /// <summary>
        /// �����HIS������Ϣ��ʼ��
        /// </summary>
        public SpecPatient GetPatient()
        {
            txtPatName.Text = specPatient.PatientName;
            if (txtPatName.Text == "")
                txtPatName.Text = specPatient.Name;
            if (specPatient.Birthday != null)
            {
                cmbYear.Text = specPatient.Birthday.Year.ToString();
                cmbMonth.Text = specPatient.Birthday.Month.ToString();
                cmbDate.Text = specPatient.Birthday.Day.ToString();
            }
            txtContactNum.Text = specPatient.ContactNum;
            txtPhoneNum.Text = specPatient.HomePhoneNum;
            if(specPatient.Nationality!=null) txtNationality.Tag = specPatient.Nationality;
            if(specPatient.Nation!=null) txtNation.Tag = specPatient.Nation; 
            if (specPatient.Gender == 'F')
                rbtFemale.Checked = true;
            else
                rbtMale.Checked = true;
            if(specPatient.BloodType!=null) txtBloodType.Tag = specPatient.BloodType;
            txtHome.Text = specPatient.Home;
            txtAddress.Text = specPatient.Address;
            if(specPatient.IsMarried!=null) txtMaritalStatus.Tag = specPatient.IsMarried;
            if (specPatient.BloodType!=null) txtBloodType.Tag = specPatient.BloodType;
            txtIcCardNO.Text = specPatient.IcCardNo;
            txtCardNo.Text = specPatient.CardNo;
            txtComment.Text = specPatient.Comment;
            txtIDNum.Text = specPatient.IdCardNo;
            txtInHosNum.Text = specPatient.InHosNum;
            return specPatient;
        }

        /// <summary>
        /// �����������뵥�ϵ�סԺ��ˮ�Ų�ѯ������Ϣ
        /// </summary>
        /// <param name="inHosNum">סԺ��ˮ��</param>
        public void GetPatient(string inHosNum)
        {
            ucQueryInPatientNo.txtInputCode.Text = inHosNum.Substring(4);
            //ucQueryInPatientNo.KeyDown(this,new KeyEventArgs(Keys.Enter));
            PatientInfoMyEvent();
        }

        public void GetPatientByInHosNum(string inHosNum)
        {
            if (!string.IsNullOrEmpty(inHosNum))
            {
                ucQueryInPatientNo.txtInputCode.Text = inHosNum.Substring(4);
                txtCardNo.Text = inHosNum.Substring(4);
                CheckPatientExsited();
            }
        }

        /// <summary>
        /// ��ѯ������Ϣ�Ƿ����,���ݿ���,סԺ��
        /// </summary>
        private void CheckPatientExsited()
        {
            string icCardNo = txtIcCardNO.Text.Trim();
            if (icCardNo != "")
            {
                ArrayList arrSpec = patientManage.GetPatientInfo("select * from SPEC_PATIENT where IC_CARDNO = '" + icCardNo + "'");
                //����걾���д��ڸò��˵���Ϣ
                if (arrSpec != null && arrSpec.Count > 0)
                {
                    specPatient = arrSpec[0] as SpecPatient;
                    GetPatient();
                    IsExisted = true;
                    return;
                }
                //��������ڴ�His�л�ȡ
                else
                {
                    specPatient = patientManage.GetPatientInfoIcCardNo(icCardNo);
                    if (specPatient != null)
                    {
                        GetPatient();
                    }
                }
            }

            string cardNo = txtCardNo.Text.Trim();
            if (cardNo != "")
            {
                ArrayList arrSpec = patientManage.GetPatientInfo("select * from SPEC_PATIENT where CARD_NO = '" + cardNo + "'");
                //����걾���д��ڸò��˵���Ϣ
                if (arrSpec != null && arrSpec.Count > 0)
                {
                    specPatient = arrSpec[0] as SpecPatient;
                    GetPatient();
                    IsExisted = true;
                    return;
                }
                //��������ڴ�His�л�ȡ
                else
                {
                    specPatient = patientManage.GetPatientInfoCardNo(cardNo);
                    if (specPatient != null)
                    {
                        GetPatient();
                    }                   
                }
            }
        }

        /// <summary>
        /// HIS��Patient��Ϣת��Ϊ�걾��Patient��Ϣ
        /// </summary>
        /// <param name="patientInfo"></param>
        private void ConvertToSpecPatient(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            specPatient.Address = patientInfo.AddressHome;
            
            specPatient.Birthday = patientInfo.Birthday;
            specPatient.BloodType = patientInfo.BloodType.ID.ToString();
            specPatient.CardNo = patientInfo.PID.PatientNO;
            //specPatient.IcCardNo = patientInfo.PatientId;
            specPatient.ContactNum = patientInfo.PhoneBusiness;
            specPatient.Gender = Convert.ToChar(patientInfo.Sex.ID.ToString());
            specPatient.Home = patientInfo.DIST;
            specPatient.HomePhoneNum = patientInfo.PhoneHome;
            specPatient.IdCardNo = patientInfo.IDCard;
            //סԺ��ˮ��
            specPatient.InHosNum = patientInfo.ID;
            specPatient.IsMarried = patientInfo.MaritalStatus.ID.ToString();
            switch (patientInfo.MaritalStatus.ID.ToString())
            {
                case "W":
                    specPatient.IsMarried = "4";
                    break;
                case "A":
                    specPatient.IsMarried = "5";
                    break;
                case "R":
                    specPatient.IsMarried = "5";
                    break;
                case "D":
                    specPatient.IsMarried = "3";
                    break;
                case "M":
                    specPatient.IsMarried = "2";
                    break;
                case "S":
                    specPatient.IsMarried = "1";
                    break;         
            }
            specPatient.Nation = patientInfo.Nationality.ID;
            specPatient.Nationality = patientInfo.Country.ID;
            specPatient.PatientName = patientInfo.Name;
            GetPatient();
        }        

        private void PatientInfoMyEvent()
        {
            IsExisted = false;
            ArrayList arrPatient = new ArrayList();
            //ucQueryInPatientNo.query();
            arrPatient = ucQueryInPatientNo.InpatientNos;
            FS.HISFC.Models.RADT.PatientInfo patientInfo = new FS.HISFC.Models.RADT.PatientInfo();
            patientInfo = radtIntegrate.GetPatientInfomation(ucQueryInPatientNo.InpatientNo);
            if (patientInfo == null)
            {
                return;
            }
            if (patientInfo != null)
            {
                ConvertToSpecPatient(patientInfo);
            }
            string sql = "select distinct * from SPEC_PATIENT where PATIENTID>0 and (";          
            string icCardNo = txtIcCardNO.Text.Trim();
            string cardNo = txtCardNo.Text.Trim();
            if (icCardNo != "")
            {
                sql += " IC_CARDNO = '" + icCardNo + "' Or";
            }
            if (cardNo != "")
            {
                sql += " CARD_NO = '" + cardNo + "'";
            }
            sql += ")";
            ArrayList arrSpec = patientManage.GetPatientInfo(sql);
            //����걾���д��ڸò��˵���Ϣ
            if (arrSpec != null && arrSpec.Count > 0)
            {
                IsExisted = true;
                this.SpecPatient = arrSpec[0] as SpecPatient;
            }
            else
            {
                this.SpecPatient = new SpecPatient();
            }
        }

        private void ucPatientInfo_Load(object sender, EventArgs e)
        {
            FS.HISFC.BizLogic.Manager.Constant con = new FS.HISFC.BizLogic.Manager.Constant();
            //��ѯ�����б�
            ArrayList countryList = new ArrayList();
            countryList = con.GetList(FS.HISFC.Models.Base.EnumConstant.COUNTRY);
            this.txtNationality.AddItems(countryList);

            //��ѯ�����б�
            ArrayList Nationallist = con.GetList(FS.HISFC.Models.Base.EnumConstant.NATION);
            this.txtNation.AddItems(Nationallist);

            //Ѫ���б�
            ArrayList BloodTypeList = con.GetList(FS.HISFC.Models.Base.EnumConstant.BLOODTYPE);// baseDml.GetBloodType();
            this.txtBloodType.AddItems(BloodTypeList);

            //�����б�
            ArrayList MaritalStatusList = con.GetList("MaritalStatus");
            this.txtMaritalStatus.AddItems(MaritalStatusList);

            DateTime dtNow = DateTime.Now;
            int year = dtNow.Year;
            int month = dtNow.Month;
            
            for (int i = year-100; i <= year; i++)
            {
                cmbYear.Items.Add(i);                         
            }
            for (int i = 1; i <= 12; i++)
            {
                cmbMonth.Items.Add(i);              
            }
            cmbYear.SelectedIndex = 0;
            cmbMonth.SelectedIndex = 0;
            ucQueryInPatientNo.myEvent += new FS.HISFC.Components.Common.Controls.myEventDelegate(this.PatientInfoMyEvent);
            ucQueryInPatientNo.Name = "QueryPatientNo";
            Size size = new Size();
            size.Height = 70;
            size.Width = 200;
            ucQueryInPatientNo.Size = size;
            flpQueryInpatietnNo.Controls.Add(ucQueryInPatientNo);

            flpQueryInpatietnNo.Visible = true;
        }

        private void txtIcCardNO_KeyDown(object sender, KeyEventArgs e)
        {
            string icCardNo = txtIcCardNO.Text.Trim();
            if (e.KeyValue == 13 && icCardNo!="")
            {
                CheckPatientExsited();
            }            
        }

        private void txtCardNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                string cardNo = txtCardNo.Text.Trim().PadLeft(10, '0').ToUpper();
                txtCardNo.Text = cardNo;
                CheckPatientExsited();
            }
        }

        private void txtInHosNum_KeyDown(object sender, KeyEventArgs e)
        {
            string inHosNum = txtInHosNum.Text.Trim();
            if (e.KeyValue == 13 && inHosNum!="")
            {
                CheckPatientExsited();
            }
        }

        private void cmbMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            int month = Convert.ToInt32(cmbMonth.Text.Trim());
            int year = Convert.ToInt32(cmbYear.Text.Trim());
            if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)
            {
                for (int i = 1; i <= 31; i++)
                    cmbDate.Items.Add(i);
                cmbDate.SelectedIndex = 1;
                return;
            }
            if (month == 4 || month == 6 || month == 9 || month == 11)
            {
                for (int i = 1; i <= 30; i++)
                    cmbDate.Items.Add(i);
                cmbDate.SelectedIndex = 1;
                return;
            }
            if (year % 4 == 0 && month == 2)
            {
                for (int i = 1; i <= 29; i++)
                {
                    cmbDate.Items.Add(i);
                }
                cmbDate.SelectedIndex = 1;
                return;
            }
            else
            {
                for (int i = 1; i <= 28; i++)
                {
                    cmbDate.Items.Add(i);
                }
                cmbDate.SelectedIndex = 1;
                return;
            }
        }

        private void cmbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            nudOld.Value = DateTime.Now.Year - Convert.ToInt32(cmbYear.Text.Trim());
           // nudOld.Value =
        }

        private void nudOld_ValueChanged(object sender, EventArgs e)
        {
            cmbYear.Text = (DateTime.Now.Year - Convert.ToInt32(nudOld.Value)).ToString();
        }
 

    }
}
