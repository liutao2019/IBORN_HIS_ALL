using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
namespace FS.HISFC.Components.Nurse.Controls
{
    /// <summary>
    /// [��������: ������Ϣ]<br></br>
    /// [�� �� ��: wolf]<br></br>
    /// [����ʱ��: 2006-11-30]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucPatientInfo : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucPatientInfo()
        {
            InitializeComponent();
            this.AutoScroll = true;
        }

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.InitControl();
            return base.OnInit(sender, neuObject, param);
        }
        protected override int OnSetValue(object neuObject, TreeNode e)
        {
            this.ClearPatintInfo();
            this.SetPatientInfo(e.Tag as FS.HISFC.Models.Registration.Register);
            return base.OnSetValue(neuObject, e);
        }
        protected FS.HISFC.BizProcess.Integrate.Manager manager = new FS.HISFC.BizProcess.Integrate.Manager();

        protected FS.HISFC.BizProcess.Integrate.Registration.Registration Outpatient = new FS.HISFC.BizProcess.Integrate.Registration.Registration();

        protected FS.HISFC.Models.Registration.Register register = null;
        protected FS.HISFC.BizProcess.Integrate.RADT radtManager = new FS.HISFC.BizProcess.Integrate.RADT();


        #region ����
        /// <summary>
        /// �Ա��Ƿ������޸�
        /// </summary>
        [Category("���߻�����Ϣ"), Description("�Ա��Ƿ������޸�")]
        public bool SexReadOnly
        {
            get
            {
                return cmbSex.Enabled;
            }
            set
            {
                cmbSex.Enabled = value;
            }
        }
        /// <summary>
        /// �����Ƿ������޸�
        /// </summary>
        [Category("���߻�����Ϣ"), Description("�����Ƿ������޸�")]
        public bool BirthdayReadOnly
        {
            get
            {
                return dtBirthday.Enabled;
            }
            set
            {
                dtBirthday.Enabled = value;
            }
        }
       
        /// <summary>
        /// ���֤���Ƿ������޸�
        /// </summary>
        [Category("���߻�����Ϣ"), Description("���֤���Ƿ������޸�")]
        public bool IDReadOnly
        {
            get
            {
                return txtID.Enabled;
            }
            set
            {
                txtID.Enabled = value;
            }
        }
        /// <summary>
        /// ְҵ�Ƿ������޸�
        /// </summary>
        [Category("���߻�����Ϣ"), Description("ְҵ�Ƿ������޸�")]
        public bool ProfessionReadOnly
        {
            get
            {
                return cmbProfession.Enabled;
            }
            set
            {
                cmbProfession.Enabled = value;
            }
        }
        /// <summary>
        /// ְҵ�Ƿ������޸�
        /// </summary>
        [Category("���߻�����Ϣ"), Description("�����Ƿ������޸�")]
        public bool MarryReadOnly
        {
            get
            {
                return cmbMarry.Enabled;
            }
            set
            {
                cmbMarry.Enabled = value;
            }
        }
        /// <summary>
        /// ��ͥסַ�Ƿ������޸�
        /// </summary>
        [Category("���߻�����Ϣ"), Description("��ͥסַ�Ƿ������޸�")]
        public bool HomeAddrReadOnly
        {
            get
            {
                return cmbHomeAddr.Enabled;
            }
            set
            {
                cmbHomeAddr.Enabled = value;
            }
        }
        /// <summary>
        /// ��ͥ�绰�Ƿ������޸�
        /// </summary>
        [Category("���߻�����Ϣ"), Description("��ͥ�绰�Ƿ������޸�")]
        public bool HomeTelReadOnly
        {
            get
            {
                return txtHomeTel.Enabled;
            }
            set
            {
                txtHomeTel.Enabled = value;
            }
        }
        /// <summary>
        /// ������λ�Ƿ������޸�
        /// </summary>
        [Category("���߻�����Ϣ"), Description("������λ�Ƿ������޸�")]
        public bool WorkReadOnly
        {
            get
            {
                return txtWork.Enabled;
            }
            set
            {
                txtWork.Enabled = value;
            }
        }
        /// <summary>
        /// ��ϵ���Ƿ������޸�
        /// </summary>
        [Category("���߻�����Ϣ"), Description("��ϵ���Ƿ������޸�")]
        public bool LinkManReadOnly
        {
            get
            {
                return txtLinkMan.Enabled;
            }
            set
            {
                txtLinkMan.Enabled = value;
            }
        }
        /// <summary>
        /// ��ϵ�˵�ַ�Ƿ������޸�
        /// </summary>
        [Category("���߻�����Ϣ"), Description("��ϵ�˵�ַ�Ƿ������޸�")]
        public bool KinAddressReadOnly
        {
            get
            {
                return cmbKinAddress.Enabled;
            }
            set
            {
                cmbKinAddress.Enabled = value;
            }
        }
        /// <summary>
        /// ��ϵ�˵绰�Ƿ������޸�
        /// </summary>
        [Category("���߻�����Ϣ"), Description("��ϵ�˵绰�Ƿ������޸�")]
        public bool LinkTelReadOnly
        {
            get
            {
                return txtLinkTel.Enabled;
            }
            set
            {
                txtLinkTel.Enabled = value;
            }
        }
        /// <summary>
        /// ��ϵ�˹�ϵ�Ƿ������޸�
        /// </summary>
       
        
        #endregion 

        /// <summary>
        /// ��ʼ���ؼ�
        /// </summary>
        protected void InitControl()
        {
            try
            {
               
                this.cmbSex.AddItems(FS.HISFC.Models.Base.SexEnumService.List());
                this.cmbSex.IsListOnly = true;
                this.cmbMarry.AddItems(FS.HISFC.Models.RADT.MaritalStatusEnumService.List());
                this.cmbMarry.IsListOnly = true;
                this.cmbProfession.AddItems(manager.GetConstantList(FS.HISFC.Models.Base.EnumConstant.PROFESSION));
                this.cmbProfession.IsListOnly = true;
                this.cmbKinAddress.AddItems(manager.GetConstantList(FS.HISFC.Models.Base.EnumConstant.AREA));
                this.cmbHomeAddr.AddItems(manager.GetConstantList(FS.HISFC.Models.Base.EnumConstant.AREA));
                this.txtWork.AddItems(manager.GetConstantList(FS.HISFC.Models.Base.EnumConstant.AREA));

                //��ͬ��λ{B71C3094-BDC8-4fe8-A6F1-7CEB2AEC55DD}
                //this.cmbPact.AddItems(manager.GetConstantList(FS.HISFC.Models.Base.EnumConstant.PACTUNIT));
                this.cmbPact.AddItems(manager.QueryPactUnitInPatient());
                this.cmbPact.IsListOnly = true;

       
            }
            catch { }

        }


        /// <summary>
        /// ���û�����Ϣ���ؼ�
        /// </summary>
        /// <param name="patientInfo"></param>
        protected void SetPatientInfo(FS.HISFC.Models.Registration.Register register)
        {
            this.register = register;
            FS.HISFC.Models.Registration.Register Register = register;
            this.txtPatientNo.Text = Register.ID;				//���ۺ�
            this.txtCard.Text = Register.PID.CardNO;				//���￨��
            this.txtName.Text = Register.Name;					//��������
            this.txtWork.Text = Register.CompanyName;			//��λ����		
            this.txtHomeTel.Text = Register.PhoneHome;			//��ͥ�绰
            this.cmbHomeAddr.Text = Register.AddressHome;		//��ͥסַ
            this.cmbMarry.Tag = Register.MaritalStatus.ID;		//���
            this.cmbSex.Tag = Register.Sex.ID;					//�Ա�
            this.cmbProfession.Tag = Register.Profession.ID;    //ְҵ
            this.dtBirthday.Value = Register.Birthday;			//��������
            this.txtDeptName.Text = Register.DoctorInfo.Templet.Dept.Name;//��������
            this.dtIndate.Text = Register.PVisit.InTime.ToString("yyyy��MM��dd��");				//��Ժ����
            this.txtID.Text = Register.SSN;						//���֤��
            this.cmbPact.Text = Register.Pact.Name; 			//��ͬ��λ����
            this.cmbPact.Tag = Register.Pact.ID;					//��ͬ��λ����
           
        }


        /// <summary>
        /// ��տؼ�����
        /// </summary>
        public void ClearPatintInfo()
        {
            this.txtPatientNo.Text = "";	//סԺ��
            this.txtName.Text = "";			//��������
            this.txtWork.Text = "";			//������λ
            this.txtHomeTel.Text = "";		//��ͥ�绰
            this.cmbHomeAddr.Text = "";
            this.cmbMarry.Tag = "";
            this.cmbSex.Tag = "";
            this.cmbProfession.Text = "";
            this.dtBirthday.Value = System.DateTime.Now;
            //�󲹳�----wangrc
            this.cmbPact.Text = "";          //��ͬ��λ
            this.cmbPact.Tag = null;
            this.txtLinkMan.Text = "";      //��ϵ��
            this.txtLinkTel.Text = "";      //��ϵ�˵绰
            this.cmbKinAddress.Text = "";   //��ϵ�˵�ַ
            this.txtID.Text = "";           //���֤��
           
            this.txtCard.Text = "";         //���￨��
            this.txtDeptName.Text = "";	//�������ڿ���
            
        }


        /// <summary>
        /// ��û��߻�����Ϣ�ӿؼ���PatientInfo
        /// </summary>
        /// <param name="PatientInfo"></param>
        protected bool GetPatientInfo(FS.HISFC.Models.Registration.Register Register)
        {
            Register.ID = this.txtPatientNo.Text;				//���ۺ�
            Register.Card.ID = this.txtCard.Text;				//���￨��
            Register.Name = this.txtName.Text;					//��������
            Register.CompanyName = this.txtWork.Text;			//��λ����		
            Register.PhoneHome = this.txtHomeTel.Text;			//��ͥ�绰
            Register.AddressHome = this.cmbHomeAddr.Text;		//��ͥסַ
            Register.MaritalStatus.ID = this.cmbMarry.Tag;		//���
            Register.Sex.ID = this.cmbSex.Tag;					//�Ա�
            Register.Profession.ID = this.cmbProfession.Tag.ToString();    //ְҵ
            Register.Birthday = this.dtBirthday.Value;			//��������
            Register.DoctorInfo.Templet.Dept.Name = this.txtDeptName.Text;//��������
            Register.PVisit.InTime = DateTime.Parse(this.dtIndate.Text);		//��Ժ����
            Register.SSN = this.txtID.Text;						//���֤��
            Register.Pact.Name = this.cmbPact.Text; 			//��ͬ��λ����
            Register.Pact.ID = this.cmbPact.Tag.ToString();			    //��ͬ��λ����

            if (this.CheckIDInfo(this.txtID.Text.Trim()) == -1)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// ������֤�ŵ���Ч�ԣ����������֤��У���������
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        protected int CheckIDInfo(string ID)
        {
            if (ID != "")
            {
                int result = 0;
                string errText = string.Empty;
                result = FS.FrameWork.WinForms.Classes.Function.CheckIDInfo(ID, ref errText);
                if (result == -1)
                {
                    MessageBox.Show(errText);
                    return -1;
                }
                int idlength = ID.Length;
                int year = 0;
                int month = 0;
                int day = 0;
                DateTime dtBirth = System.DateTime.Now;
                if (idlength == 15)
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
                
                if (this.dtBirthday.Value.CompareTo(dtBirth) != 0)
                {
                    this.dtBirthday.Value = dtBirth;
                    MessageBox.Show("ϵͳ��������������֤�������˳������ڣ���ȷ��������ٽ��б��棡");
                    return -1;
                }
                return 1;
            }
            else
            {
                return 1;
            }
        }

        /// <summary>
        /// ˢ�»�����Ϣ
        /// </summary>
        /// <param name="patient"></param>
        public void RefreshList(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            this.RefreshList(patient.ID);
        }


        /// <summary>
        /// ����������ˮ��ˢ�»�����Ϣ
        /// </summary>
        /// <param name="inpatientNo"></param>
        public void RefreshList(string clinicNO)
        {
            FS.HISFC.Models.Registration.Register obj = null;
            obj = this.Outpatient.GetByClinic(clinicNO);
            if (obj == null)
            {
                MessageBox.Show(this.Outpatient.Err);
                return;
            }
           

            //ֻ�г�Ժ�ǼǵĻ��߲���ʾ"��Ժ֪ͨ��"��ť
            if (obj.PVisit.InState.ID.ToString() == "B")
            {
                
                this.btnOutBill.Visible = true;
            }
            else
            {
                this.btnOutBill.Visible = false;
              
            }
            try
            {
                this.SetPatientInfo(obj);
            }
            catch { }
        }


        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public int Save()
        {
            //ȡ�ؼ����޸ĺ�Ļ�����Ϣ
            if (!GetPatientInfo(register)) return -1;

            FS.HISFC.Models.RADT.Patient obj = (FS.HISFC.Models.RADT.Patient)register;

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
            //t.BeginTransaction();

            radtManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            if (this.radtManager.UpdatePatient((FS.HISFC.Models.RADT.PatientInfo)obj) == 1)
            {
                FS.FrameWork.Management.PublicTrans.Commit();
                MessageBox.Show("����ɹ���", "��ʾ");
                base.OnRefreshTree();
            }
            else
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("����ʧ�ܣ�" + this.radtManager.Err, "��ʾ");
                return -1;
            }
            return 0;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.Save();
        }
  
    }
}
