using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.Base;
using System.Collections;

namespace FS.HISFC.Components.Registration.SelfReg
{
    /// <summary>
    /// [��������: �����ҺŻ�����Ϣ��ʾ�ؼ�]<br></br>
    /// [�� �� ��: ţ��Ԫ]<br></br>
    /// [����ʱ��: 2009-9]<br></br>
    /// <˵��
    ///		��۱��ػ�
    ///  />
    /// </summary>
    public partial class ucSelfHelpPatientInfo : UserControl
    {
        public ucSelfHelpPatientInfo()
        {
            InitializeComponent();
        }

        #region ����

        /// <summary>
        /// סԺ������Ϣʵ��
        /// </summary>
        private FS.HISFC.Models.RADT.PatientInfo patientInfo = null;

        /// <summary>
        /// �ۺϹ���ҵ���
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = null;//new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// ����������
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper nationHelper = new FS.FrameWork.Public.ObjectHelper();


        /// <summary>
        /// ������λ
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper workHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// home
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper homeHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// cardType
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper cardTypeHelper = new FS.FrameWork.Public.ObjectHelper();



        #endregion

        #region ����
        /// <summary>
        /// סԺ������Ϣʵ��
        /// </summary>
        public FS.HISFC.Models.RADT.PatientInfo PatientInfo
        {
            set
            {
                this.patientInfo = value;
                //���߻��߽�����Ϣ
                this.SetPatientInfo();
            }
        }
        #endregion

        #region ����

        /// <summary>
        /// ��ʾ���߻�����Ϣ
        /// </summary>
        /// 
        private void SetPatientInfo()
        {
            //modify by sung 2009-2-24 {DCAA485E-753C-41ed-ABCF-ECE46CD41B33}
            if (this.patientInfo.IsEncrypt)
            {
                patientInfo.Name = FS.FrameWork.WinForms.Classes.Function.Decrypt3DES(patientInfo.NormalName);
            }
            this.txtName.Text = patientInfo.Name;//��������

            //this.txtName.Text = this.patientInfo.Name;               //����
            //if (this.patientInfo.IsEncrypt)
            //{

            //    this.txtName.Tag = this.patientInfo.DecryptName;         //��ʵ����                  
            //}
            //else
            //{
            //    this.txtName.Tag = null;
            //}
            this.cmbSex.Text = this.patientInfo.Sex.Name;            //�Ա�
            this.cmbSex.Tag = this.patientInfo.Sex.ID;               //�Ա�
            this.cmbPact.Text = this.patientInfo.Pact.Name;          //��ͬ��λ����
            this.cmbPact.Tag = this.patientInfo.Pact.ID;             //��ͬ��λID
            //this.cmbArea.Tag = this.patientInfo.AreaCode;            //����
            this.cmbCountry.Tag = this.patientInfo.Country.ID;       //����
            this.cmbCountry.Text = this.nationHelper.GetName(this.patientInfo.Country.ID);
            //this.cmbNation.Tag = this.patientInfo.Nationality.ID;    //����
            this.dtpBirthDay.Text = this.patientInfo.Birthday.ToString("yyyy-MM-dd");      //��������
            //this.txtAge.Text = this.accountManager.GetAge(this.patientInfo.Birthday);//����
            //this.cmbDistrict.Text = this.patientInfo.DIST;            //����
            //this.cmbProfession.Tag = this.patientInfo.Profession.ID; //ְҵ
            this.txtIDNO.Text = this.patientInfo.IDCard;             //���֤��
            this.cmbWorkAddress.Text = this.patientInfo.CompanyName; //������λ
            //this.txtWorkPhone.Text = this.patientInfo.PhoneBusiness; //��λ�绰
            //this.cmbMarry.Tag = this.patientInfo.MaritalStatus.ID.ToString();//����״��
            this.cmbHomeAddress.Text = this.patientInfo.AddressHome;  //��ͥסַ
            //this.txtHomePhone.Text = this.patientInfo.PhoneHome;     //��ͥ�绰
            //this.txtLinkMan.Text = this.patientInfo.Kin.Name;        //��ϵ�� 
            //this.cmbRelation.Tag = this.patientInfo.Kin.Relation.ID; //��ϵ�˹�ϵ
            //this.cmbLinkAddress.Text = this.patientInfo.Kin.RelationAddress;//��ϵ�˵�ַ
            //this.txtLinkPhone.Text = this.patientInfo.Kin.RelationPhone;//��ϵ�˵绰
            //this.ckEncrypt.Checked = this.patientInfo.IsEncrypt; //�Ƿ��������
            //this.ckVip.Checked = this.patientInfo.VipFlag;//�Ƿ�vip
            //this.txtMatherName.Text = this.patientInfo.MatherName;//ĸ������
            this.cmbCardType.Tag = this.patientInfo.IDCardType.ID; //֤������
            this.cmbCardType.Text = this.patientInfo.IDCardType.Name;//֤������
            //this.txtSiNO.Text = this.patientInfo.SSN;//��ᱣ�պ�

        }

        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <returns></returns>
        private int InitInfo()
        {
            this.managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

            //�Ա��б�
            //this.cmbSex.AddItems(FS.HISFC.Models.Base.SexEnumService.List());
            this.cmbSex.Text = "";

            //����
            //this.cmbCountry.AddItems(managerIntegrate.GetConstantList(EnumConstant.COUNTRY));
            ArrayList alNation = managerIntegrate.GetConstantList(EnumConstant.COUNTRY);
            this.nationHelper.ArrayObject = alNation;

            //������λ
            //this.cmbWorkAddress.AddItems(managerIntegrate.GetConstantList(EnumConstant.WORKNAME));

            ArrayList alWork = this.managerIntegrate.GetConstantList(EnumConstant.WORKNAME);
            this.workHelper.ArrayObject = alWork;

            //��ͥסַ��Ϣ
            //this.cmbHomeAddress.AddItems(managerIntegrate.GetConstantList(EnumConstant.AREA));
            ArrayList alHome = this.managerIntegrate.GetConstantList(EnumConstant.AREA);
            this.homeHelper.ArrayObject = alHome;


            //��ͬ��λ{B71C3094-BDC8-4fe8-A6F1-7CEB2AEC55DD}
            //this.cmbPact.AddItems(managerIntegrate.QueryPactUnitAll());

            //֤������
            //this.cmbCardType.AddItems(managerIntegrate.QueryConstantList("IDCard"));
            ArrayList alCardType = managerIntegrate.QueryConstantList("IDCard");
            cardTypeHelper.ArrayObject = alCardType;

            //foreach (Control var in this.neuGroupBox1.Controls)
            //{
            //    if (var.GetType() != typeof(FS.FrameWork.WinForms.Controls.NeuLabel))
            //    {
            //        var.Enabled = false;
            //    }
            //}

            return 1;
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public int Clear()
        {
            foreach (Control var in this.neuGroupBox1.Controls)
            {
                if (var.GetType() == typeof(FS.FrameWork.WinForms.Controls.NeuComboBox))
                {
                    var.Tag = "";
                    var.Text = "";
                }
                if (var.GetType() == typeof(FS.FrameWork.WinForms.Controls.NeuTextBox))
                {
                    var.Text = "";
                }
            }
            return 1;
        }

        /// <summary>
        /// ��ʼ����Ϣ
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            //if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToLower() != "devenv.exe")
            //{
            if (!this.DesignMode)
            {
                this.InitInfo();
            }
            base.OnLoad(e);
        }
        #endregion

    }
}
