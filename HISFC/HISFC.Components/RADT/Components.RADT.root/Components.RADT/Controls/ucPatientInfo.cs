using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
namespace Neusoft.HISFC.Components.RADT.Controls
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
    public partial class ucPatientInfo : Neusoft.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucPatientInfo()
        {
            InitializeComponent();
            this.AutoScroll = true;
        }

        protected override Neusoft.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.InitControl();
            return base.OnInit(sender, neuObject, param);
        }
        protected override int OnSetValue(object neuObject, TreeNode e)
        {
            this.ClearPatintInfo();
            this.SetPatientInfo(neuObject as Neusoft.HISFC.Models.RADT.PatientInfo);
            return base.OnSetValue(neuObject, e);
        }
        protected Neusoft.HISFC.BizProcess.Integrate.Manager manager = new Neusoft.HISFC.BizProcess.Integrate.Manager();
        protected Neusoft.HISFC.BizLogic.RADT.InPatient Inpatient = new Neusoft.HISFC.BizLogic.RADT.InPatient();
        protected Neusoft.HISFC.Models.RADT.PatientInfo patientInfo = null;

        //{B71C3094-BDC8-4fe8-A6F1-7CEB2AEC55DD}
        //Neusoft.HISFC.BizProcess.Integrate.Manager = new Neusoft.HISFC.BizProcess.Integrate.Fee();
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
        /// ����Ƿ������޸�
        /// </summary>
        [Category("���߻�����Ϣ"), Description("����Ƿ������޸�")]
        public bool HeightReadOnly
        {
            get
            {
                return txtHeight.Enabled;
            }
            set
            {
                txtHeight.Enabled = value;
            }
        }
        /// <summary>
        /// �����Ƿ������޸�
        /// </summary>
        [Category("���߻�����Ϣ"), Description("�����Ƿ������޸�")]
        public bool WeightReadOnly
        {
            get
            {
                return txtWeight.Enabled;
            }
            set
            {
                txtWeight.Enabled = value;
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
        [Category("���߻�����Ϣ"), Description("��ϵ�˹�ϵ�Ƿ������޸�")]
        public bool RelationReadOnly
        {
            get
            {
                return cmbRelation.Enabled;
            }
            set
            {
                cmbRelation.Enabled = value;
            }
        }
        /// <summary>
        /// ��ע�����Ƿ������޸�
        /// </summary>
        [Category("���߻�����Ϣ"), Description("��ע�����Ƿ������޸�")]
        public bool MemoReadOnly
        {
            get
            {
                return cmbMemo.Enabled;
            }
            set
            {
                cmbMemo.Enabled = value;
            }
        }
        #endregion 

        /// <summary>
        /// ��ʼ���ؼ�
        /// </summary>
        protected void InitControl()
        {
            try
            {
               
                this.cmbSex.AddItems(Neusoft.HISFC.Models.Base.SexEnumService.List());
                this.cmbSex.IsListOnly = true;
                this.cmbMarry.AddItems(Neusoft.HISFC.Models.RADT.MaritalStatusEnumService.List());
                this.cmbMarry.IsListOnly = true;
                this.cmbProfession.AddItems(manager.GetConstantList(Neusoft.HISFC.Models.Base.EnumConstant.PROFESSION));
                this.cmbProfession.IsListOnly = true;
                this.cmbRelation.AddItems(manager.GetConstantList(Neusoft.HISFC.Models.Base.EnumConstant.RELATIVE));
                this.cmbRelation.IsListOnly = true;
                this.cmbKinAddress.AddItems(manager.GetConstantList(Neusoft.HISFC.Models.Base.EnumConstant.AREA));
                this.cmbHomeAddr.AddItems(manager.GetConstantList(Neusoft.HISFC.Models.Base.EnumConstant.AREA));
                this.txtWork.AddItems(manager.GetConstantList(Neusoft.HISFC.Models.Base.EnumConstant.AREA));
                this.cmbMemo.AddItems(manager.GetConstantList(Neusoft.HISFC.Models.Base.EnumConstant.REMARK));
                this.cmbMemo.IsListOnly = true;
                //��ͬ��λ
                //{B71C3094-BDC8-4fe8-A6F1-7CEB2AEC55DD}
                //this.cmbPact.AddItems(manager.GetConstantList(Neusoft.HISFC.Models.Base.EnumConstant.PACTUNIT));

                this.cmbPact.AddItems(manager.QueryPactUnitInPatient());

                this.cmbPact.IsListOnly = true;

       
            }
            catch { }

        }


        /// <summary>
        /// ���û�����Ϣ���ؼ�
        /// </summary>
        /// <param name="patientInfo"></param>
        protected void SetPatientInfo(Neusoft.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            this.patientInfo = patientInfo;
            Neusoft.HISFC.Models.RADT.Patient Patient = patientInfo;
       
            this.txtPatientNo.Text = Patient.PID.PatientNO;				//סԺ��
            this.txtCard.Text = patientInfo.PID.CardNO;				//���￨��
            this.txtName.Text = Patient.Name;								//��������
            this.txtWork.Text = Patient.CompanyName;						//��λ����		
            this.txtHomeTel.Text = Patient.PhoneHome;						//��ͥ�绰
            this.cmbHomeAddr.Text = Patient.AddressHome;					//��ͥסַ
            this.cmbMarry.Tag = Patient.MaritalStatus.ID;					//���
            this.cmbSex.Tag = Patient.Sex.ID;								//�Ա�
            this.cmbProfession.Tag = Patient.Profession.ID;					//ְҵ
            this.dtBirthday.Value = Patient.Birthday;						//��������
            this.txtDeptName.Text = patientInfo.PVisit.PatientLocation.Dept.Name;//��������
            this.dtIndate.Text = patientInfo.PVisit.InTime.ToString("yyyy��MM��dd��");				//��Ժ����
            this.cmbRelation.Tag = patientInfo.Kin.Relation.ID;				//�벡�˹�ϵ����
            this.cmbKinAddress.Text = patientInfo.Kin.RelationAddress;				//��ϵ�˵�ַ
            this.txtLinkTel.Text = patientInfo.Kin.RelationPhone;					//��ϵ�˵绰
            this.txtLinkMan.Text = patientInfo.Kin.Name;					//��ϵ������
            this.txtID.Text = patientInfo.IDCard;						//���֤��
            this.txtHeight.Text = patientInfo.Height;				//���
            this.txtWeight.Text = patientInfo.Weight;				//����
            if (patientInfo.Memo == "")
                this.cmbMemo.Tag = "";
            else
                this.cmbMemo.Text = patientInfo.Memo;						//��ע
            this.cmbPact.Text = patientInfo.Pact.Name;				//��ͬ��λ����
            this.cmbPact.Tag = patientInfo.Pact.ID;					//��ͬ��λ����
           
            this.txtDeptName.Text = patientInfo.PVisit.PatientLocation.Dept.Name;

          
           
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
            this.txtHeight.Text  = "";       //���
            this.txtWeight.Text = "";       //����
            this.txtCard.Text = "";         //���￨��
            this.cmbRelation.Text = "";    //��ϵ�˹�ϵ
            this.cmbMemo.Text = "";        //��ע
            this.Enabled = true;
            this.txtDeptName.Text = "";	//�������ڿ���
            
        }


        /// <summary>
        /// ��û��߻�����Ϣ�ӿؼ���PatientInfo
        /// </summary>
        /// <param name="PatientInfo"></param>
        protected bool GetPatientInfo(Neusoft.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            patientInfo.PID.PatientNO = this.txtPatientNo.Text;//סԺ��
            patientInfo.PID.CardNO = this.txtCard.Text;//���￨��
            patientInfo.Name = this.txtName.Text;//����
            patientInfo.Sex.ID = this.cmbSex.Tag;//�Ա�
            patientInfo.Birthday = this.dtBirthday.Value;//��������
            
            patientInfo.IDCard = this.txtID.Text;//���֤��
            

            patientInfo.Profession.ID = this.cmbProfession.Tag.ToString();
            patientInfo.MaritalStatus.ID = this.cmbMarry.Tag.ToString();
            patientInfo.AddressHome = cmbHomeAddr.Text;
            patientInfo.PhoneHome = this.txtHomeTel.Text;
            patientInfo.CompanyName = this.txtWork.Text;
            patientInfo.Kin.Relation.ID = this.cmbRelation.Tag.ToString();
            patientInfo.Kin.RelationLink = this.cmbRelation.Text;
            patientInfo.Kin.RelationAddress = this.cmbKinAddress.Text;
            patientInfo.Kin.RelationPhone = this.txtLinkTel.Text;
            patientInfo.Kin.Name = this.txtLinkMan.Text;
            //�ж�����Ƿ�Ϊ����
            string strHeight = string.Empty;
            if (this.txtHeight.Text.IndexOf(".") < 0)
            {
                strHeight = this.txtHeight.Text;
                if (strHeight.Length > 3)
                {
                    MessageBox.Show("�������������������룡");
                    this.txtHeight.Focus();
                    return false;
                }
            }
            else
            {
                if (this.txtHeight.Text.IndexOf(".") > 3)
                {
                    MessageBox.Show("�������������������룡");
                    this.txtHeight.Focus();
                    return false;
                }
                strHeight = this.txtHeight.Text.Remove(this.txtHeight.Text.IndexOf("."), 1);
            }
            for (int i = 0, j = strHeight.Length; i < j; i++)
            {
                if (!char.IsDigit(strHeight, i))
                {
                    //����˵���ǵڼ����ַ�������
                    MessageBox.Show("��߱���������", "��ʾ", MessageBoxButtons.OK);
                    return false;
                }
            }
            patientInfo.Height = this.txtHeight.Text;
            //�ж������Ƿ�Ϊ����
            string strWeight = string.Empty;
            if (this.txtWeight.Text.IndexOf(".") < 0)
            {
                strWeight = this.txtWeight.Text;
                if (strWeight.Length > 3)
                {
                    MessageBox.Show("��������������������룡");
                    this.txtWeight.Focus();
                    return false;
                }
            }
            else
            {
                if (this.txtWeight.Text.IndexOf(".") > 3)
                {
                    MessageBox.Show("��������������������룡");
                    this.txtWeight.Focus();
                    return false;
                }
                strWeight = this.txtWeight.Text.Remove(this.txtWeight.Text.IndexOf("."), 1);
            }
            for (int i = 0, j = strWeight.Length; i < j; i++)
            {
                if (!char.IsDigit(strWeight, i))
                {
                    //����˵���ǵڼ����ַ�������
                    MessageBox.Show("���ر���������", "��ʾ", MessageBoxButtons.OK);
                    return false;
                }
            }
            patientInfo.Weight = this.txtWeight.Text;
            if (!Neusoft.FrameWork.Public.String.ValidMaxLengh(this.cmbMemo.Text, 200))
            {
                MessageBox.Show("��ע������", "��ʾ", MessageBoxButtons.OK);
                return false;
            }
            else
            {
                patientInfo.Memo = this.cmbMemo.Text;
            }

            patientInfo.Pact.Name = this.cmbPact.Text;
            patientInfo.Pact.ID = this.cmbPact.Tag.ToString();

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
                result = Neusoft.FrameWork.WinForms.Classes.Function.CheckIDInfo(ID, ref errText);
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
        public void RefreshList(Neusoft.HISFC.Models.RADT.PatientInfo patient)
        {
            this.RefreshList(patient.ID);
        }


        /// <summary>
        /// ����������ˮ��ˢ�»�����Ϣ
        /// </summary>
        /// <param name="inpatientNo"></param>
        public void RefreshList(string inpatientNo)
        {
            Neusoft.HISFC.Models.RADT.PatientInfo PatientInfo = null;
            PatientInfo = this.Inpatient.QueryPatientInfoByInpatientNO(inpatientNo);
            if (PatientInfo == null)
            {
                MessageBox.Show(this.Inpatient.Err);
                return;
            }
           

            //ֻ�г�Ժ�ǼǵĻ��߲���ʾ"��Ժ֪ͨ��"��ť
            if (PatientInfo.PVisit.InState.ID.ToString() == "B")
            {
                
                this.btnOutBill.Visible = true;
            }
            else
            {
                this.btnOutBill.Visible = false;
              
            }
            try
            {
                this.SetPatientInfo(PatientInfo);
            }
            catch { }
        }


        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public int Save()
        {
            if (patientInfo == null) return 0;
            Neusoft.HISFC.Models.RADT.PatientInfo PatientInfo = null;
            try
            {
                PatientInfo = this.Inpatient.QueryPatientInfoByInpatientNO(patientInfo.ID);
                if (PatientInfo == null)
                {
                    MessageBox.Show(this.Inpatient.Err);
                    return -1;
                }

                //��������Ѳ��ڱ���,���������---������ת�ƺ�,���������û�йر�,����ִ������
                if (PatientInfo.PVisit.PatientLocation.NurseCell.ID != ((Neusoft.HISFC.Models.Base.Employee)Neusoft.FrameWork.Management.Connection.Operator).Nurse.ID)
                {
                    MessageBox.Show("���߲����ڱ�����,�����޸Ĵ˻�����Ϣ", "��ʾ");
                    return -1;
                }
            }
            catch { }

            //ȡ�ؼ����޸ĺ�Ļ�����Ϣ
            if (!GetPatientInfo(PatientInfo)) return -1;

            if (this.Inpatient.UpdatePatient(PatientInfo) == 1)
            {
                MessageBox.Show("����ɹ���", "��ʾ");
                base.OnRefreshTree();

            }
            else
            {
                MessageBox.Show("����ʧ�ܣ�" + this.Inpatient.Err, "��ʾ");
                return -1;
            }
            return 1;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.Save();
        }

        private void btnOutBill_Click(object sender, EventArgs e)
        {
            ///���´��������
            //if (patientInfo == null)
            //{
            //    return;
            //}
            //ucOutPrint print = new ucOutPrint();
            //print.SetPatientInfo(patientInfo);
            //print.PrintPreview();
            ////print.Print();
        } 
  
    }
}
