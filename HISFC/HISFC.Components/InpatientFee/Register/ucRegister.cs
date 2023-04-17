using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.Management;
using FS.HISFC.Models.RADT;
using FS.HISFC.BizLogic.Fee;
using FS.HISFC.Models.Fee.Inpatient;
using FS.HISFC.Models.Fee;
using FS.HISFC.Models.Base;
using FS.FrameWork.Function;
using FS.FrameWork.WinForms.Forms;
using System.Xml;


namespace FS.HISFC.Components.InpatientFee.Register
{
    /// <summary>
    /// ucRegister<br></br>
    /// [��������: סԺ�Ǽ�UC]<br></br>
    /// [�� �� ��: ����]<br></br>
    /// [����ʱ��: 2006-11-06]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucRegister : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.FrameWork.WinForms.Forms.IInterfaceContainer,FS.HISFC.BizProcess.Interface.FeeInterface.ISIReadCard
    {
        /// <summary>
        /// 
        /// </summary>
        public ucRegister()
        {
            InitializeComponent();
        }

        #region ����

        private string hl7 = "A01";

        /// <summary>
        /// ��ǰ������Ϣ
        /// </summary>
        private string errText;

        /// <summary>
        /// �Ƿ�����޸�סԺ����
        /// </summary>
        private bool isCanModifyInTime;

        /// <summary>
        /// �Ƿ�Ϊ�޸�״̬
        /// </summary>
        private bool isModify = false;

        /// <summary>
        /// ������������ı���ʾ��ɫ
        /// </summary>
        private Color mustInputColor = Color.Blue;

        /// <summary>
        /// �����������Ƿ��������
        /// </summary>
        private bool rdoClinicNOIsMustInput = false;

        /// <summary>
        /// ��������Ŀؼ��б�
        /// </summary>
        private Hashtable mustInputHashTable = new Hashtable();

        /// <summary>
        /// סԺ���߻�����Ϣʵ��
        /// </summary>
        private PatientInfo patientInfomation = new PatientInfo();
        private bool homeAddressChangeLanguage = false; 
        private bool linkManAddressChangeLanguage = false;
        private bool isCreateMoneyAlert = false;
        private bool workAddressChangeLanguage = false;
        //��������
        private SuretyTypeEnumService suretyType = new SuretyTypeEnumService();

        /// <summary>
        /// ��չ��Ϣ�ӿ�
        /// </summary>
        private FS.HISFC.BizProcess.Interface.FeeInterface.IRegisterExtend registerExtend;


        /// <summary>
        /// toolBarService
        /// </summary>
        protected FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        /// <summary>
        /// �Ƿ���TabIndex,�س��л�����
        /// </summary>
        private bool isTabIndexFocused = true;

        /// <summary>
        /// �Ƿ��Զ�����סԺ��
        /// </summary>
        private bool isAutoInpatientNO = true;

        /// <summary>
        /// סԺ����ʱ���ɲ���
        /// </summary>
        private string autoPatientParms = string.Empty;

        /// <summary>
        /// �Ƿ�ֱ��ѡ�񲡴�
        /// </summary>
        private bool isSelectBed = false;
        /// <summary>
        /// �Ƿ����
        /// </summary>
        private bool isReadCard = false;

        /// <summary>
        /// �洢���»�����Ϣʱ��סԺ��(liu.xq��)
        /// </summary>
        private string tempUpdatePatientID;

        /// <summary>
        /// �Ƿ����޸�ԤԼ�Ǽǵ���Ϣ  {E9EC275C-F044-40f1-BDDA-0F17410983EB}
        /// </summary>
        private bool isModifyPreRegInfo = true;

        /// <summary>
        /// �Ƿ��µǼǻ��ߣ�����ǲ�ѯ�����Ķ����վɻ��ߴ���
        /// </summary>
        private bool isNewRegPerson = true;
        
        /// <summary>
        /// ��ӡ����
        /// </summary>
        FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterface healthPrint = null;

        #region �ؼ������������

        /// <summary>
        /// סԺ���Ƿ��������
        /// </summary>
        private bool rdoInpatientNOIsMustInput = false;

        /// <summary>
        /// ҽ��֤�������Ƿ��������
        /// </summary>
        private bool txtMCardIsMustInput = false;

        /// <summary>
        /// ��Ժ�����Ƿ��������
        /// </summary>
        private bool dtpInDateMustInput = false;

        /// <summary>
        /// ���㷽ʽ�Ƿ��������
        /// </summary>
        private bool cmbPactIsMustInput = false;

        /// <summary>
        /// �����Ƿ��������
        /// </summary>
        private bool txtNameIsMustInput = false;

        /// <summary>
        /// �Ա��Ƿ�������� Ĭ�ϱ�������
        /// </summary>
        private bool cmbSexIsMustInput = true;

        /// <summary>
        /// �����Ƿ�������� Ĭ�ϱ�������
        /// </summary>
        private bool cmbDeptIsMustInput = true;

        /// <summary>
        /// �����Ƿ�������� Ĭ�ϱ�������
        /// </summary>
        private bool cmbNurseCellIsMustInput = true;

        /// <summary>
        /// ���������Ƿ�������� Ĭ�ϱ�������
        /// </summary>
        private bool dtpBirthDayMustInput = true;

        ///// <summary>
        ///// ������λ�Ƿ�������� Ĭ�ϱ�������
        ///// </summary>
        //private bool cmbWorkAddressMustInput = true;

        /// <summary>
        /// ��סҽʦ�Ƿ�������� Ĭ�ϱ�������
        /// </summary>
        private bool cmbDoctorMustInput = true;

        /// <summary>
        /// Ԥ������Ƿ�������� Ĭ�ϱ�������
        /// </summary>
        private bool mTxtPrepayMustInput = true;

        //{0975AB99-8BEC-4561-A371-77993B255948}
        /// <summary>
        /// ��ϵ���Ƿ��������
        /// </summary>
        private bool linkPersonMustInput = true;

        //{0975AB99-8BEC-4561-A371-77993B255948}
        /// <summary>
        /// ��ͥסַ�Ƿ��������
        /// </summary>
        private bool homeAdressMustInput = true;

        /// <summary>
        /// ������ϱ�������
        /// </summary>
        private bool clinicDiagnoseMustInput = false;

        /// <summary>
        /// Ԥ�����ӡ�ӿ�
        /// </summary>
        private FS.HISFC.BizProcess.Interface.FeeInterface.IPrepayPrint prepayPrint = null;
        
        /// <summary>
        /// �Ƿ������������
        /// </summary>
        private bool IsContainsInstate=false;

        /// <summary>
        /// ADT�ӿ�
        /// </summary>
        private FS.HISFC.BizProcess.Interface.IHE.IADT adt = null;

        ////{0374EA05-782E-4609-9CDC-03236AB97906}
        private FS.HISFC.BizProcess.Interface.FeeInterface.IPrintSurety iPrintSureType = null;

        #endregion

        #region ҵ������

        /// <summary>
        /// סԺ����ҵ���
        /// </summary>
        private InPatient inpatientManager = new InPatient();

        private FS.HISFC.BizProcess.Integrate.RADT radtIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();

        /// <summary>
        /// ���ù��ýӿ�ҵ���
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();

        /// <summary>
        /// Managerҵ���
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// ��ͬ��λҵ���
        /// </summary>
        private PactUnitInfo pactManager = new PactUnitInfo();

        /// <summary>
        /// ����������
        /// </summary>
        private FS.FrameWork.Management.ControlParam ctlMgr = new FS.FrameWork.Management.ControlParam();
        /// <summary>
        /// ���ô�����
        /// </summary>
        FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy medcareInterfaceProxy = new FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy();

        FS.HISFC.BizProcess.Interface.IQueryGFPatient queryGFPatient = null;
        #endregion

        /// <summary>
        /// �Ƿ��ӡ����
        /// </summary>
        //{F862D2BC-57DB-4868-9A4D-32A47A8B4588}
        private bool isHealthPrint = true;
        #endregion

        #region IInterfaceContainer ��Ա

        Type[] FS.FrameWork.WinForms.Forms.IInterfaceContainer.InterfaceTypes
        {
            get
            {
                Type[] type = new Type[5];
                type[0] = typeof(FS.HISFC.BizProcess.Interface.FeeInterface.IPrepayPrint);
                type[1] = typeof(FS.HISFC.BizProcess.Interface.IHE.IADT);
                //{0374EA05-782E-4609-9CDC-03236AB97906}
                type[2] = typeof(FS.HISFC.BizProcess.Interface.FeeInterface.IPrintSurety);

                //{1336CBD1-EF5A-430c-9965-B9BC72823593}
                type[3] = typeof(FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterface);
                type[4] = typeof(FS.HISFC.BizProcess.Interface.IQueryGFPatient);
                return type;
            }
        }

          #endregion

        #region �ؼ���������

        /// <summary>
        /// ���������Ƿ��������
        /// </summary>
        [Category("�ؼ�����"), Description("���������Ƿ��������,�������ΪTrue��ô�ؼ�����ʾϵͳ�������ɫ")]
        public bool ����������������
        {
            set
            {
                this.txtNameIsMustInput = value;

                if (this.txtNameIsMustInput)
                {
                    this.lblName.ForeColor = mustInputColor;
                }

                this.AddOrRemoveUnitAtMustInputLists(this.lblName, this.txtName, this.txtNameIsMustInput);
            }
            //get
            //{
            //    return this.txtNameIsMustInput;
            //}
        }

        /// <summary>
        /// ���������Ƿ��������
        /// </summary>
        [Category("�ؼ�����"), Description("�����Ա��Ƿ��������,�������ΪTrue��ô�ؼ�����ʾϵͳ�������ɫ")]
        public bool �����Ա��������
        {
            set
            {
                this.cmbSexIsMustInput = value;

                if (this.cmbSexIsMustInput)
                {
                    this.lblSex.ForeColor = mustInputColor;
                }

                this.AddOrRemoveUnitAtMustInputLists(this.lblSex, this.cmbSex, this.cmbSexIsMustInput);
            }
           
        }

        /// <summary>
        /// ���߿����Ƿ��������
        /// </summary>
        [Category("�ؼ�����"), Description("���߿����Ƿ��������,�������ΪTrue��ô�ؼ�����ʾϵͳ�������ɫ")]
        public bool �Ǽǿ��ұ�������
        {
            set
            {
                this.cmbDeptIsMustInput = value;

                if (this.cmbDeptIsMustInput)
                {
                    this.lblDept.ForeColor = mustInputColor;
                }
                
                this.AddOrRemoveUnitAtMustInputLists(this.lblDept, this.cmbDept, this.cmbDeptIsMustInput);
            }
        }

        /// <summary>
        /// ���߲����Ƿ��������//{F0BF027A-9C8A-4bb7-AA23-26A5F3539586}
        /// </summary>
        [Category("�ؼ�����"), Description("���߲����Ƿ��������,�������ΪTrue��ô�ؼ�����ʾϵͳ�������ɫ")]
        public bool �Ǽǲ�����������
        {
            set
            {
                this.cmbNurseCellIsMustInput = value;

                if (this.cmbNurseCellIsMustInput)
                {
                    this.lblNurseCell.ForeColor = mustInputColor;
                }

                this.AddOrRemoveUnitAtMustInputLists(this.lblNurseCell, this.cmbNurseCell, this.cmbNurseCellIsMustInput);
            }
        }
        /// <summary>
        /// ���㷽ʽ�Ƿ��������
        /// </summary>
        [Category("�ؼ�����"), Description("���㷽ʽ�Ƿ��������,�������ΪTrue��ô�ؼ�����ʾϵͳ�������ɫ")]
        public bool ���㷽ʽ��������
        {
            set
            {
                this.cmbPactIsMustInput = value;

                if (this.cmbPactIsMustInput)
                {
                    this.lblPact.ForeColor = mustInputColor;
                }

                this.AddOrRemoveUnitAtMustInputLists(this.lblPact, this.cmbPact, this.cmbPactIsMustInput);
            }
            get
            {
                return this.cmbPactIsMustInput;
            }
        }

        /// <summary>
        /// ��Ժ�����Ƿ��������
        /// </summary>
        [Category("�ؼ�����"), Description("��Ժ�����Ƿ��������,�������ΪTrue��ô�ؼ�����ʾϵͳ�������ɫ")]
        public bool סԺ���ڱ�������
        {
            set
            {
                this.dtpInDateMustInput = value;

                if (this.dtpInDateMustInput)
                {
                    this.lblInTime.ForeColor = mustInputColor;
                }

                this.AddOrRemoveUnitAtMustInputLists(this.lblInTime, this.dtpInTime, this.dtpInDateMustInput);
            }
            get
            {
                return this.dtpInDateMustInput;
            }
        }

        /// <summary>
        /// �����ҽ��֤���Ƿ��������
        /// </summary>
        [Category("�ؼ�����"), Description("ҽ��֤������ؼ��Ƿ��������,�������ΪTrue��ô�ؼ�����ʾϵͳ�������ɫ")]
        public bool ҽ��֤�ű�������
        {
            set
            {
                this.txtMCardIsMustInput = value;

                if (this.txtMCardIsMustInput)
                {
                    this.lblMCardNO.ForeColor = mustInputColor;
                }

                this.AddOrRemoveUnitAtMustInputLists(this.lblMCardNO, this.txtMCardNO, this.txtMCardIsMustInput);
            }
            get
            {
                return this.txtMCardIsMustInput;
            }
        }
        
        /// <summary>
        /// �����סԺ���Ƿ��������
        /// </summary>
        [Category("�ؼ�����"), Description("סԺ������ؼ��Ƿ��������,�������ΪTrue��ô�ؼ�����ʾϵͳ�������ɫ")]
        public bool סԺ�ű�������
        {
            set
            {
                this.rdoInpatientNOIsMustInput = value;

                if (this.rdoInpatientNOIsMustInput)
                {
                    this.rdoInpatientNO.ForeColor = mustInputColor;
                }

                this.AddOrRemoveUnitAtMustInputLists(this.rdoInpatientNO, this.txtInpatientNO, this.rdoInpatientNOIsMustInput);
            }
            get
            {
                return this.rdoInpatientNOIsMustInput;
            }
        }

        /// <summary>
        /// ���������Ƿ��������
        /// </summary>
        [Category("�ؼ�����"), Description("������������ؼ��Ƿ��������,�������ΪTrue��ô�ؼ�����ʾϵͳ�������ɫ")]
        public bool �������ڱ�������
        {
            set
            {
                this.dtpBirthDayMustInput = value;
                if (this.dtpBirthDayMustInput)
                {
                    this.lblBirthday.ForeColor = mustInputColor;
                }
                this.AddOrRemoveUnitAtMustInputLists(this.lblBirthday, this.dtpBirthDay, this.dtpBirthDayMustInput);
            }
            get
            {
                return this.dtpBirthDayMustInput;
            }
        }

        ///// <summary>
        ///// ������λ�Ƿ��������
        ///// </summary>
        //[Category("�ؼ�����"), Description("������λ����ؼ��Ƿ��������,�������ΪTrue��ô�ؼ�����ʾϵͳ�������ɫ")]
        //public bool ������λ��������
        //{
        //    set
        //    {
        //        this.cmbWorkAddressMustInput = value;
        //        if (this.cmbWorkAddressMustInput)
        //        {
        //            this.lblWorkAddress.ForeColor = mustInputColor;
        //        }
        //        this.AddOrRemoveUnitAtMustInputLists(this.lblWorkAddress, this.cmbWorkAddress, this.cmbWorkAddressMustInput);
        //    }
        //    get
        //    {
        //        return this.cmbWorkAddressMustInput;
        //    }
        //}

        /// <summary>
        /// ��סҽʦ�Ƿ��������
        /// </summary>
        [Category("�ؼ�����"), Description("��סҽʦ����ؼ��Ƿ��������,�������ΪTrue��ô�ؼ�����ʾϵͳ�������ɫ")]
        public bool ��סҽʦ��������
        {
            set
            {
                this.cmbDoctorMustInput = value;
                if (this.cmbDoctorMustInput)
                {
                    this.lblDoctor.ForeColor = mustInputColor;
                }
                this.AddOrRemoveUnitAtMustInputLists(this.lblDoctor, this.cmbDoctor, this.cmbDoctorMustInput);
            }
            get
            {
                return this.cmbDoctorMustInput;
            }
        }

        //{0975AB99-8BEC-4561-A371-77993B255948}
        /// <summary>
        /// ��סҽʦ�Ƿ��������
        /// </summary>
        [Category("�ؼ�����"), Description("��סҽʦ����ؼ��Ƿ��������,�������ΪTrue��ô�ؼ�����ʾϵͳ�������ɫ")]
        public bool Ԥ������������
        {
            set
            {
                this.mTxtPrepayMustInput = value;
                if (this.mTxtPrepayMustInput)
                {
                    this.lblPrepay.ForeColor = mustInputColor;
                }
                this.AddOrRemoveUnitAtMustInputLists(this.lblPrepay, this.mTxtPrepay, this.mTxtPrepayMustInput);
            }
            get
            {
                return this.mTxtPrepayMustInput;
            }
        }
        //{0975AB99-8BEC-4561-A371-77993B255948}
        // <summary>
        /// ��ϵ���Ƿ��������
        /// </summary>
        [Category("�ؼ�����"), Description("��ϵ������ؼ��Ƿ��������,�������ΪTrue��ô�ؼ�����ʾϵͳ�������ɫ")]
        public bool ��ϵ�˱�������
        {
            set
            {
                this.linkPersonMustInput = value;
                if (this.linkPersonMustInput)
                {
                    this.lblLinkMan.ForeColor = mustInputColor;
                }
                this.AddOrRemoveUnitAtMustInputLists(this.lblLinkMan, this.txtLinkMan, this.linkPersonMustInput);
            }
            get 
            {
                return linkPersonMustInput;
            }
        }

        /// <summary>
        /// ��ͥ��ַ�Ƿ��������
        /// </summary>
        [Category("�ؼ�����"), Description("��ͥ��ַ����ؼ��Ƿ��������,�������ΪTrue��ô�ؼ�����ʾϵͳ�������ɫ")]
        public bool ��ͥ��ַ��������
        {
            set
            {
                this.homeAdressMustInput = value;
                if (this.homeAdressMustInput)
                {
                    this.lblHomeAddress.ForeColor = mustInputColor;
                }
                this.AddOrRemoveUnitAtMustInputLists(this.lblHomeAddress, this.cmbHomeAddress, this.homeAdressMustInput);
            }
            get
            {
                return homeAdressMustInput;
            }
        }

        /// <summary>
        /// ��������Ƿ��������
        /// </summary>
        [Category("�ؼ�����"), Description("�����������ؼ��Ƿ��������,�������ΪTrue��ô�ؼ�����ʾϵͳ�������ɫ")]
        public bool ������ϱ�������
        {
            set
            {
                this.clinicDiagnoseMustInput = value;
                if (this.clinicDiagnoseMustInput)
                {
                    this.lblDiagnose.ForeColor = mustInputColor;
                }
                this.AddOrRemoveUnitAtMustInputLists(this.lblDiagnose, this.cmbClinicDiagnose, this.clinicDiagnoseMustInput);
            }
            get
            {
                return clinicDiagnoseMustInput;
            }
        }
        #endregion

        /// <summary>
        /// Ԥ�����ӡ�ӿ�
        /// </summary>
        public FS.HISFC.BizProcess.Interface.FeeInterface.IPrepayPrint PrepayPrint 
        {
            set 
            {
                this.prepayPrint = value;
            }
            get 
            {
                return this.prepayPrint;
            }
        }

        /// <summary>
        /// �Ƿ�Ϊ�޸�״̬
        /// </summary>
        public bool IsModify
        {
            get 
            {
                return this.isModify;
            }
        }
        
        /// <summary>
        /// ��չ��Ϣ�ӿ�
        /// </summary>
        public FS.HISFC.BizProcess.Interface.FeeInterface.IRegisterExtend RegisterExtend 
        {
            set 
            {
                this.registerExtend = value;
            }
        }

        /// <summary>
        /// ��ǰ������Ϣ
        /// </summary>
        public string ErrText 
        {
            get 
            {
                return this.errText;
            }
        }

        /// <summary>
        /// ��ǰ�ؼ��ĸ�����
        /// </summary>
        public Form FatherForm 
        {
            get 
            {
                try
                {
                    Form f = this.FindForm();

                    return f;
                }
                catch (Exception e) 
                {
                    this.errText = e.Message;

                    return null;
                }
            }
        }

        /// <summary>
        /// ������������ı���ʾ��ɫ
        /// </summary>
        [Category("�ؼ�����"), Description("��������ؼ�����ɫ")]
        public Color MustInputColor 
        {
            set 
            {
                this.mustInputColor = value;
            }
            get 
            {
                return this.mustInputColor;
            }
        }

        /// <summary>
        /// סԺ���߻�����Ϣʵ��
        /// </summary>
        public PatientInfo PatientInfomation 
        {
            get 
            {
                return this.patientInfomation;
            }
            set 
            {
                this.patientInfomation = value;
            }
        }

        /// <summary>
        /// �Ƿ���TabIndex,�س��л�����
        /// </summary>
        [Category("�ؼ�����"), Description("�Ƿ���TabIndex�س��л�����,���ѡ��False���ձ�������ÿؼ��л�����")]
        public bool IsTabIndexFocused
        {
            set 
            {
                this.isTabIndexFocused = value;
            }
            get 
            {
                return this.isTabIndexFocused;
            }
        }
        
        /// <summary>
        /// �����������Ƿ��������
        /// </summary>
        [Category("�ؼ�����"), Description("����ſؼ��Ƿ��������,�������ΪTrue��ô�ؼ�����ʾϵͳ�������ɫ")]
        public bool RdoClinicNOIsMustInput
        {
            set 
            {
                this.rdoClinicNOIsMustInput = value;

                if (this.rdoClinicNOIsMustInput) 
                {
                    this.rdoClinicNO.ForeColor = mustInputColor;
                }

                this.AddOrRemoveUnitAtMustInputLists(this.rdoClinicNO, this.txtClinicNO, this.rdoClinicNOIsMustInput);
            }
            get 
            {
                return this.rdoClinicNOIsMustInput;
            }
        }

        /// <summary>
        /// �Ƿ�����޸�סԺ����
        /// </summary>
        [Category("�ؼ�����"), Description("True�����޸�סԺ���� False�������޸� Ĭ��Ϊ��ǰʱ��")]
        public bool IsCanModifyInTime 
        {
            get 
            {
                return this.isCanModifyInTime;
            }
            set 
            {
                this.isCanModifyInTime = value;

                this.dtpInTime.Enabled = this.isCanModifyInTime;
            }
        }

        /// <summary>
        /// �Ƿ��Զ�����סԺ��
        /// </summary>
        [Category("�ؼ�����"), Description("True�Զ�����סԺ�� False�ֹ�����סԺ�� Ĭ��Ϊ�ֹ�����סԺ��")]
        public bool IsAutoInpatientNO 
        {
            get 
            {
                return this.isAutoInpatientNO;
            }
            set 
            {
                this.isAutoInpatientNO = value;

                this.btnAutoInpatientNO.Enabled = this.isAutoInpatientNO;

                toolBarService.SetToolButtonEnabled("��ʱ��", this.isAutoInpatientNO);
            }
        }
        
        /// <summary> 
        /// �Ƿ�ֱ��ѡ�񲡴�
        /// </summary>
        [Category("�ؼ�����"), Description("��ͥ��ַ�Ƿ��Զ��л����뷨")]
        public bool HomeAddressChangeLanguage
        {
            get
            {
                return this.homeAddressChangeLanguage;
            }
            set
            {
                this.homeAddressChangeLanguage = value; 
            }
        }
        /// <summary> 
        /// �Ƿ�ֱ��ѡ�񲡴�
        /// </summary>
        [Category("�ؼ�����"), Description("��ͥ��ַ�Ƿ��Զ��л����뷨")]
        public bool WorkAddressChangeLanguage
        {
            get
            {
                return this.workAddressChangeLanguage;
            }
            set
            {
                this.workAddressChangeLanguage = value;
            }
        }
        /// <summary>
        /// �Ƿ�ֱ��ѡ�񲡴�
        /// </summary>
        [Category("�ؼ�����"), Description("��ͥ��ַ�Ƿ��Զ��л����뷨")]
        public bool LinkManAddressChangeLanguage
        {
            get
            {
                return this.linkManAddressChangeLanguage;
            }
            set
            {
                this.linkManAddressChangeLanguage = value; 
            }
        }
        /// <summary>
        /// �Ƿ�ֱ��ѡ�񲡴�
        /// </summary>
        [Category("�ؼ�����"), Description("Trueֱ��ѡ�񲡴�,���Զ����� False��ʿվ����,���ﲻ���䴲 Ĭ�ϻ�ʿվ����,���ﲻ���䴲")]
        public bool IsSelectBed 
        {
            get 
            {
                return this.isSelectBed;
            }
            set 
            {
                this.isSelectBed = value;

                this.cmbBedNO.Enabled = this.isSelectBed;
            }
        }

        /// <summary>
        /// �Ƿ�ֱ��ѡ�񲡴�
        /// </summary>
        [Category("�ؼ�����"), Description("�Ǽ�ʱ�Ƿ񰴺�ͬ��λ�Զ�����Ĭ�Ͼ�����,True:Ĭ�ϵľ�����ȡ����ά���к�ͬ��λ�ı�ע���뽫��ע����Ϊ���֣�")]
        public bool IsCreateMoneyAlert
        {
            get
            {
                return this.isCreateMoneyAlert;
            }
            set
            {
                this.isCreateMoneyAlert = value; 
            }
        }
        
        [Category("�ؼ�����"),Description("סԺ�Ǽ�ʱ�Ƿ��ӡ����,True:��ӡ:False:����ӡ")]
        //{F862D2BC-57DB-4868-9A4D-32A47A8B4588}
        public bool IsHealthPrint
        {
            get
            {
                return this.isHealthPrint;
            }
            set
            {
                this.isHealthPrint = value;
            }
        }

        /// <summary>
        /// �Ƿ����޸�ԤԼ�Ǽǵ���Ϣ  {E9EC275C-F044-40f1-BDDA-0F17410983EB}
        /// </summary>
        [Category("�ؼ�����"),Description("�Ƿ����޸�ԤԼ�Ǽǵ���Ϣ" )]
        public bool IsModifyPreRegInfo
        {
            get
            {
                return isModifyPreRegInfo;
            }
            set
            {
                isModifyPreRegInfo = value;
            }
        }

        //{0374EA05-782E-4609-9CDC-03236AB97906}
        private void InitInterface()
        {
            this.iPrintSureType = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.FeeInterface.IPrintSurety)) as FS.HISFC.BizProcess.Interface.FeeInterface.IPrintSurety;
   
        }

       // #endregion

        #region ö��

        /// <summary>
        /// �Ѿ��Ǽǻ����б�,��˳��ö��
        /// </summary>
        protected enum PatientLists 
        {
            /// <summary>
            /// סԺ��
            /// </summary>
            PatientNO = 0,

            /// <summary>
            /// ����
            /// </summary>
            Name,

            /// <summary>
            /// �Ա�
            /// </summary>
            Sex,

            /// <summary>
            /// סԺ����
            /// </summary>
            Dept,

            /// <summary>
            /// ���֤��
            /// </summary>
            IDNO,

            /// <summary>
            /// ��ͥסַ
            /// </summary>
            HomeAddress,

            /// <summary>
            /// �Ǽ�ʱ��
            /// </summary>
            InDate,

            /// <summary>
            /// ״̬
            /// </summary>
            InState
        }

        #endregion

        #region ˽�з���

        /// <summary>
        /// ���ݿؼ�����,�ж��Ƿ��ڱ�������ؼ��б��м������ɾ���ÿؼ�
        /// </summary>
        /// <param name="nameControl">���ƿؼ�</param>
        /// <param name="inputControl">����ؼ�</param>
        /// <param name="isMustInput">�Ƿ��������</param>
        private void AddOrRemoveUnitAtMustInputLists(Control nameControl, Control inputControl, bool isMustInput) 
        {
            if (isMustInput)
            {
                if (!mustInputHashTable.ContainsKey(nameControl))
                {
                    this.mustInputHashTable.Add(nameControl, inputControl);
                }
            }
            else 
            {
                if (mustInputHashTable.ContainsKey(nameControl))
                {
                    this.mustInputHashTable.Remove(nameControl);
                }
            }
        }

        private int GetAutoPatientNO()
        {
            this.isAutoInpatientNO = true;
            PatientInfo patient = new PatientInfo();
            this.dtpInTime.Value = this.inpatientManager.GetDateTimeFromSysDateTime(); //��Ժ����
            patient.PatientNOType = EnumPatientNOType.First;
            string patientNO = string.Empty;
            bool isRecycle = false;
            if (this.radtIntegrate.GetAutoPatientNO(ref patientNO,ref isRecycle) == -1)
            {
                MessageBox.Show(Language.Msg("����Զ�����סԺ�ų���!") + this.radtIntegrate.Err);
                return -1;
            }
            //Ĭ�ϵ�һ����Ժ
            patient.PID.PatientNO = patientNO;
            patient.ID = "T001";
            patient.InTimes = 1;
            this.txtInpatientNO.Text = patient.PID.PatientNO;
            this.txtInpatientNO.Tag = "T001";
            string cardNO = this.txtClinicNO.Text.Trim();          
            if (string.IsNullOrEmpty(cardNO))
            {
                this.txtClinicNO.Text = "T" + patientNO.Substring(1, 9); 
            }
            else
            {
                PatientInfo tempP = radtIntegrate.QueryComPatientInfo(cardNO);
                if (tempP == null)
                {
                    MessageBox.Show("��ѯ������Ϣ����" + radtIntegrate.Err);
                    return -1;
                }
                if (string.IsNullOrEmpty(tempP.PID.CardNO))
                {
                    this.txtClinicNO.Text = patient.PID.CardNO;
                }
                else if (tempP.Name != this.txtName.Text || tempP.Sex.ID.ToString() != this.cmbSex.Tag.ToString())
                {
                    txtClinicNO.Text = patient.PID.CardNO;
                }
            }
            patient.InTimes = 1;//�����һ��������ֵΪ1
            this.mTxtIntimes.Text = patient.InTimes.ToString();         
            return 1;
        }

        private int GetInputPatientNO(string patientNO)
        {
            this.isAutoInpatientNO = false;
            PatientInfo patient = new PatientInfo();

            //���סԺ������ʷסԺ��Ϣ����ȡ������Ϣ��סԺ����
            if (this.radtIntegrate.GetInputPatientNO(patientNO, ref patient) < 1)
            {
                MessageBox.Show(this.radtIntegrate.Err);
                return -1;
            }
            if (patient.PatientNOType == EnumPatientNOType.Second)
            {
                //�����������޸�
                this.txtName.Enabled = false;
                //�����渳ֵ
                if (!isReadCard)
                {
                    patient .PVisit.InTime= this.inpatientManager.GetDateTimeFromSysDateTime(); //��Ժ����
                    this.SetPatientInfomation(patient);
                }
            }
            else
            {
                this.txtInpatientNO.Text = patient.PID.PatientNO;
                this.txtInpatientNO.Tag = patient.ID;
                this.txtClinicNO.Text = patient.PID.CardNO;
            }
           

            //this.mTxtIntimes.Text = patient.InTimes.ToString();
            this.dtpInTime.Value = this.inpatientManager.GetDateTimeFromSysDateTime();//סԺ����
            this.cmbDoctor.Text = string.Empty;//��סҽʦ
            this.cmbDept.Focus();
           
            return 1;
        }
        /// <summary>
        /// �Զ����ɻ��ߵǼ�סԺ��,��������ʱ��
        /// </summary>
        /// <param name="patientNO">��ǰסԺ��</param>
        /// <returns>�ɹ� : 1 ʧ�� : -1</returns>
        private int GetAutoInpatientNO(string patientNO) 
        {
            PatientInfo patient = new PatientInfo();
            //û������סԺ��,˵��Ϊ��һ����Ժ.
            if (patientNO == string.Empty)
            {
            }
            else
            {
                //���߷ǵ�һ����Ժ
                patient.PatientNOType = EnumPatientNOType.Second;

                if (this.radtIntegrate.CreateAutoInpatientNO(patientNO, ref patient) == -1)
                {
                    MessageBox.Show(Language.Msg("���סԺ�ų���!") + this.radtIntegrate.Err);

                    return -1;
                }
                //��ǰס��Ժ
                if (patient.PatientNOType == EnumPatientNOType.Second)
                {
                    //�ж���Ժ״̬
                    if (patient.PVisit.InState.ID.ToString() == "R" || patient.PVisit.InState.ID.ToString() == "I" || patient.PVisit.InState.ID.ToString() == "P" || patient.PVisit.InState.ID.ToString() == "B")
                    {
                        MessageBox.Show(Language.Msg("�˻�����Ժ����!"));
                        this.patientInfomation = new PatientInfo();
                        this.txtInpatientNO.Text = string.Empty;
                        this.txtInpatientNO.Tag = string.Empty;
                        this.txtClinicNO.Text = string.Empty;

                        return -1;
                    }
                    else//��ǰס��ԺĿǰ����Ժ	
                    {
                        FS.FrameWork.WinForms.Classes.Function.Msg("��סԺ�����ϴε�סԺ��Ϣ��", 111);
                        //��մ���
                        patient.PVisit.PatientLocation.Bed.ID = string.Empty;
                        //�����������޸�
                        this.txtName.Enabled = false;
                        //�����渳ֵ
                        if (!isReadCard)
                        {
                            this.SetPatientInfomation(patient);
                        }
                        else
                        {
                            this.txtInpatientNO.Text = patient.PID.PatientNO;
                            this.txtInpatientNO.Tag = patient.ID;
                            this.txtClinicNO.Text = patient.PID.CardNO;
                            this.mTxtIntimes.Text = (patient.InTimes + 1).ToString();
                            this.patientInfomation.User03 = patient.User03;
                        }

                        //this.mTxtIntimes.Text =patient.InTimes.ToString();
                        this.dtpInTime.Value = this.inpatientManager.GetDateTimeFromSysDateTime();//סԺ����
                        this.cmbDoctor.Text = string.Empty;//��סҽʦ
                        this.cmbDept.Focus();

                        return -1;
                    }
                }
            }

            this.txtInpatientNO.Text = patient.PID.PatientNO;
            this.txtInpatientNO.Tag = patient.ID;

            string cardNO = this.txtClinicNO.Text.Trim();
            //{61D727CB-A325-4bb4-AE74-1116440FC7C6}
            if (string.IsNullOrEmpty(cardNO))
            {
                this.txtClinicNO.Text = patient.PID.CardNO;
            }
            else
            {
                PatientInfo tempP = radtIntegrate.QueryComPatientInfo(cardNO);
                if (tempP == null)
                {
                    MessageBox.Show("��ѯ������Ϣ����" + radtIntegrate.Err);
                    return -1;
                }
                if (string.IsNullOrEmpty(tempP.PID.CardNO))
                {
                    this.txtClinicNO.Text = patient.PID.CardNO;
                }
                else if (tempP.Name != this.txtName.Text || tempP.Sex.ID.ToString() != this.cmbSex.Tag.ToString())
                {
                    txtClinicNO.Text = patient.PID.CardNO;
                }
            }

            if (patient.InTimes == 0) patient.InTimes = 1;//�����һ��������ֵΪ1
            this.mTxtIntimes.Text = patient.InTimes.ToString();
            this.patientInfomation.User03 = patient.User03;

            return 1;
        }

        /// <summary>
        /// ��ʼ���ؼ�,����Ϣ
        /// </summary>
        /// <returns>�ɹ� 1 ʧ��: -1</returns>
        protected virtual int Init() 
        {

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(Language.Msg("���ڳ�ʼ�����ڣ����Ժ�^^"));
            Application.DoEvents();

            try
            {
                #region ���� luzhp@FS.com
                this.cmbSuretyType.AddItems(FS.HISFC.Models.Fee.SuretyTypeEnumService.List());
                this.cmbSuretyPerson.AddItems(managerIntegrate.QueryEmployeeAll());
                #endregion
                //��ʼ�������б�
                this.cmbDept.AddItems(managerIntegrate.QueryDeptmentsInHos(true));

                //�Ա��б�
                this.cmbSex.AddItems(FS.HISFC.Models.Base.SexEnumService.List());
                //this.cmbSex.Text = "��";

                //����
                this.cmbNation.AddItems(managerIntegrate.GetConstantList(EnumConstant.NATION));
                this.cmbNation.Text = "����";

                //����״̬
                this.cmbMarry.AddItems(FS.HISFC.Models.RADT.MaritalStatusEnumService.List());

                //����
                this.cmbCountry.AddItems(managerIntegrate.GetConstantList(EnumConstant.COUNTRY));
                this.cmbCountry.Text = "�й�";

                //ְҵ��Ϣ
                this.cmbProfession.AddItems(managerIntegrate.GetConstantList(EnumConstant.PROFESSION));

                //��ϵ����Ϣ
                this.cmbRelation.AddItems(managerIntegrate.GetConstantList(EnumConstant.RELATIVE));

                //��ϵ�˵�ַ��Ϣ
                this.cmbLinkAddress.AddItems(managerIntegrate.GetConstantList(EnumConstant.AREA));

                //��ͥסַ��Ϣ
                this.cmbHomeAddress.AddItems(managerIntegrate.GetConstantList(EnumConstant.AREA));

                //������λ
                this.cmbWorkAddress.AddItems(managerIntegrate.GetConstantList(EnumConstant.AREA));

                //��������Ϣ
                this.cmbBirthArea.AddItems(managerIntegrate.GetConstantList(EnumConstant.DIST));

                //������Դ��Ϣ
                this.cmbInSource.AddItems(managerIntegrate.GetConstantList(EnumConstant.INSOURCE));
                this.cmbInSource.SelectedIndex = 0;

                //��Ժ;����Ϣ
                this.cmbApproach.AddItems(managerIntegrate.GetConstantList(EnumConstant.INAVENUE));
                this.cmbApproach.SelectedIndex = 0;

                //��Ժ�����Ϣ
                this.cmbCircs.AddItems(managerIntegrate.GetConstantList(EnumConstant.INCIRCS));
                this.cmbCircs.SelectedIndex = 0;

                //ҽ����Ϣ
                this.cmbDoctor.AddItems(managerIntegrate.QueryEmployee(EnumEmployeeType.D));

                //סԺ����
                this.mTxtIntimes.Text = "1";

                //֧����ʽ
                this.cmbPayMode.Tag = "CA";

                //{0374EA05-782E-4609-9CDC-03236AB97906}
                this.cmbTransType1.Tag = "CA";

                //��λ���
                this.txtBedInterval.Text = "1";

                //��Ժ����
                this.dtpInTime.Value = this.inpatientManager.GetDateTimeFromSysDateTime(); //��Ժ����

                //����
                this.dtpBirthDay.Value = this.inpatientManager.GetDateTimeFromSysDateTime();//��������

                //����
                this.cmbArea.AddItems(managerIntegrate.GetConstantList(EnumConstant.AREA));

                //������ϼ���ICD10��
                FS.HISFC.BizLogic.HealthRecord.ICD icdManager = new FS.HISFC.BizLogic.HealthRecord.ICD();
                this.cmbClinicDiagnose.AddItems(icdManager.Query(FS.HISFC.Models.HealthRecord.EnumServer.ICDTypes.ICD10, FS.HISFC.Models.HealthRecord.EnumServer.QueryTypes.Valid));

                //��ͬ��λ
                this.cmbPact.AddItems(this.pactManager.QueryPactUnitInPatient());
                this.cmbPact.Tag = "1";
                this.cmbUnit.SelectedIndex = 0;

                //{F0BF027A-9C8A-4bb7-AA23-26A5F3539586}
                this.cmbNurseCell.AddItems(this.managerIntegrate.GetDeptmentIn(EnumDepartmentType.N));
                //��֤��չ��Ч���жϽӿ�
                if (this.registerExtend != null)
                {
                    if (this.registerExtend.InitExtendInfomation(ref this.errText) == -1)
                    {
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        MessageBox.Show(Language.Msg(this.errText));

                        return -1;
                    }
                }
                //�ж��Ƿ������������
                //string instateStr = this.ctlMgr.QueryControlerInfo("100017");
                //if (instateStr != string.Empty)
                //{
                this.IsContainsInstate = this.isSelectBed;
                if (this.IsContainsInstate)
                {
                    this.lblBedInterval.ForeColor = this.MustInputColor;
                }
                //}

                foreach (Control c in this.plInfomation.Controls) 
                {
                    if (c is FS.FrameWork.WinForms.Controls.NeuComboBox)
                    {
                        ((FS.FrameWork.WinForms.Controls.NeuComboBox)c).Enter += new EventHandler(ucRegister_Enter);
                    }
                    else 
                    {
                        c.Enter += new EventHandler(c_Enter);
                    }
                }
            }
            catch (Exception e) 
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show(e.Message);
                
                return -1;
            }
            #region ������ӡ {1336CBD1-EF5A-430c-9965-B9BC72823593}���ýӿ�����
            //object[] o = new object[] { };

            //try
            //{

            //    FS.HISFC.BizProcess.Integrate.Common.ControlParam ctrlIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

            //    System.Runtime.Remoting.ObjectHandle objHande = System.Activator.CreateInstance("HISFC.Components.HealthRecord", "FS.HISFC.Components.HealthRecord.ucLCCasePrint", false, System.Reflection.BindingFlags.CreateInstance, null, o, null, null, null);

            //    object oLabel = objHande.Unwrap();

            //    this.healthPrint = oLabel as FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterface;
            //}
            //catch (System.TypeLoadException ex)
            //{
            //    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            //    MessageBox.Show(Language.Msg("�����ռ���Ч\n" + ex.Message));
            //    return -1;
            //}

            this.healthPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterface)) as FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterface;
            #endregion

            this.RefreshPatientLists();

            this.cmbUnit.IsFlat = true;

            //{0374EA05-782E-4609-9CDC-03236AB97906}
            this.InitInterface();

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            return 1;
        }

        void c_Enter(object sender, EventArgs e)
        {
            this.spPatientInfo.Visible = true;
            this.spConst.Visible = false;
            this.spPatientInfo.BringToFront();
            //������������ϵ����Ҫ�л����������뷨
            if (sender == this.txtName || sender == txtLinkMan || sender == this.cmbClinicDiagnose || sender == cmbWorkAddress 
                || sender == cmbHomeAddress || sender == cmbLinkAddress)
            {
                InputLanguage.CurrentInputLanguage = this.CHInput;
            }
            else
            {
                InputLanguage.CurrentInputLanguage = InputLanguage.DefaultInputLanguage;
            }
            if (sender == this.mTxtPrepay)
            {
                this.mTxtPrepay.SelectAll();
            }
        }

        void ucRegister_Enter(object sender, EventArgs e)
        {
            if (sender == null) 
            {
                return;
            }

            this.spPatientInfo.Visible = false;
            this.spConst.Visible = true;
            this.spConst.BringToFront();

            ArrayList constantList = ((FS.FrameWork.WinForms.Controls.NeuComboBox)sender).alItems;

            this.DealConstantList(constantList);
            InputLanguage.CurrentInputLanguage = InputLanguage.DefaultInputLanguage;

            if (sender == this.cmbHomeAddress&&this.homeAddressChangeLanguage)
            {
                InputLanguage.CurrentInputLanguage = this.CHInput;
            }
         
            if (sender == cmbLinkAddress&&this.linkManAddressChangeLanguage)
            {
                InputLanguage.CurrentInputLanguage = this.CHInput;
            }
    
            if (workAddressChangeLanguage && sender == this.cmbWorkAddress)
            {
                InputLanguage.CurrentInputLanguage = this.CHInput;
            } 
 
        }

        private void DealConstantList(ArrayList consList) 
        {
            if (consList == null || consList.Count <= 0) 
            {
                return;
            }

            this.spConst_Sheet1.RowCount = 0;
            this.spConst_Sheet1.RowCount = (consList.Count / 3) + (consList.Count % 3 == 0 ? 0 : 1);

            int row = 0;
            int col = 0;

            foreach (FS.FrameWork.Models.NeuObject obj in consList)
            {
                if (col >= 5)
                {
                    col = 0;
                    row++;
                }

                this.spConst_Sheet1.SetValue(row, col, obj.ID);
                this.spConst_Sheet1.SetValue(row, col + 1, obj.Name);

                col = col + 2;
            }
            
            this.spPatientInfo.Visible = false;
            this.spConst.Visible = true;


        }

        /// <summary>
        /// ͨ����ͬ��λ����,��ý������ʵ��
        /// </summary>
        /// <param name="pactID">��ͬ��λ����</param>
        /// <returns>�ɹ�: �������ʵ�� ʧ��: null</returns>
        private PayKind GetPayKindFromPactID(string pactID) 
        {
            FS.HISFC.Models.Base.PactInfo pact = this.pactManager.GetPactUnitInfoByPactCode(pactID);
            if (pact == null) 
            {
                MessageBox.Show(Language.Msg("��ú�ͬ��λ��ϸ����!"));

                return null;
            }

            return pact.PayKind;
        }

        /// <summary>
        /// ��֤�������Ϣ�Ƿ�Ϸ�
        /// </summary>
        /// <returns>�ɹ�: true ʧ��: null</returns>
        private bool IsInputValid() 
        {
            //�жϱ�������Ŀؼ��Ƿ��Ѿ�������Ϣ
            foreach (DictionaryEntry d in this.mustInputHashTable) 
            {
                if (d.Value is FS.FrameWork.WinForms.Controls.NeuComboBox)
                {
                    //{0975AB99-8BEC-4561-A371-77993B255948}
                    //if (((Control)d.Value).Tag == null || ((Control)d.Value).Tag.ToString() == string.Empty || ((Control)d.Value).Text.Trim() == string.Empty)
                    if(((Control)d.Value).Text.Trim() == string.Empty)
                    {
                        MessageBox.Show(((Control)d.Key).Text.Replace(':', ' ') + Language.Msg("����������Ϣ!"));
                        ((Control)d.Value).Focus();

                        return false;
                    }
                }
                else
                {
                    if (((Control)d.Value).Text == string.Empty)
                    {
                        MessageBox.Show(((Control)d.Key).Text.Replace(':', ' ') + Language.Msg("����������Ϣ!"));
                        ((Control)d.Value).Focus();

                        return false;
                    }
                }
            }

            if (this.txtInpatientNO.Tag == null || this.txtInpatientNO.Tag.ToString().Trim() == "") 
            {
                MessageBox.Show(Language.Msg("��س�ȷ��סԺ��"));
                this.txtInpatientNO.Focus();

                return false;
            }

            //����ų���
            if (!FS.FrameWork.Public.String.ValidMaxLengh(this.txtClinicNO.Text, 10))
            {
                MessageBox.Show(Language.Msg("����Ź���,����������!"));
                this.txtClinicNO.Focus();

                return false;
            }
            //���Ժ�
            if (!FS.FrameWork.Public.String.ValidMaxLengh(this.txtComputerNO.Text, 20))
            {
                MessageBox.Show(Language.Msg("���ԺŹ���,����������!"));
                this.txtComputerNO.Focus();

                return false;
            }

            

            //ҽ��֤�ų���
            if (!FS.FrameWork.Public.String.ValidMaxLengh(this.txtMCardNO.Text, 18))
            {
                MessageBox.Show(Language.Msg("ҽ��֤�Ź���,����������!"));
                this.txtClinicNO.Focus();

                return false;
            }

            try
            {
                Int64 inpatientNO = Convert.ToInt64(this.txtInpatientNO.Text);

                if (inpatientNO > 9000000000)
                {
                    MessageBox.Show(Language.Msg("�������סԺ�Ź���"));
                    this.txtInpatientNO.Focus();

                    return false;
                }
            }
            catch (Exception e) 
            {
                MessageBox.Show(Language.Msg("�������סԺ���к��з������ַ��������") + e.Message);
                this.txtInpatientNO.Focus();

                return false;
            }

            if (this.txtClinicNO.Text.Substring(0, 1) == "T" && this.txtClinicNO.Text != "T" + this.txtInpatientNO.Text.Substring(1, 9))
            {
                MessageBox.Show(Language.Msg("�Զ����ɵĿ��Ų������޸�,����������!"));
                this.txtClinicNO.Focus();

                return false;
            }

            //����
            if (this.cmbDept.Tag == null || this.cmbDept.Text.Trim() == string.Empty)
            {
                MessageBox.Show(Language.Msg("���Ҳ���Ϊ�գ����������룡"));
                return false;
            }

            //���ҳ���
            if (!FS.FrameWork.Public.String.ValidMaxLengh(this.cmbDept.Text, 16))
            {
                MessageBox.Show(Language.Msg("�����������,����������!"));
                this.cmbDept.Focus();

                return false;
            }

            if(this.IsContainsInstate)
            {
                if (this.cmbBedNO.Tag.ToString() == string.Empty)
                {
                    MessageBox.Show(Language.Msg("��������Ϊ�գ���ѡ�񲡴���"));
                    this.cmbBedNO.Focus();
                    return false;
                }
            }

            //��Ժ��Դ
            if (this.cmbInSource.Tag.ToString() == string.Empty)
            {
                MessageBox.Show("��Ժ��Դ����Ϊ�գ�����������");
                return false;
            }

            //���㷽ʽ
            if (this.cmbPact.Tag.ToString() == null||this.cmbPact.Tag.ToString() == string.Empty)
            {
                MessageBox.Show("���㷽ʽ����Ϊ�գ�����������");
                return false;
            }

            if (this.dtpInTime.Value > this.inpatientManager.GetDateTimeFromSysDateTime()) 
            {
                MessageBox.Show(Language.Msg("��Ժ���ڴ��ڵ�ǰ����,����������!"));
                this.dtpInTime.Focus();

                return false;
            }

            if (this.dtpBirthDay.Value > this.inpatientManager.GetDateTimeFromSysDateTime())
            {
                MessageBox.Show(Language.Msg("�������ڴ��ڵ�ǰ����,����������!"));
                this.dtpBirthDay.Focus();

                return false;
            }

            //�ж��ַ���������
            if (!FS.FrameWork.Public.String.ValidMaxLengh(this.txtName.Text, 20))
            {
                MessageBox.Show(Language.Msg("����¼�볬����"));
                this.txtName.Focus();
                return false;
            }

            //�ж��ַ���������
            if (!FS.FrameWork.Public.String.ValidMaxLengh(this.cmbBirthArea.Text, 50))
            {
                MessageBox.Show(Language.Msg("����¼�볬����"));
                this.cmbBirthArea.Focus();
                return false;
            }

            //�ж��ַ�������ϵ��
            if (!FS.FrameWork.Public.String.ValidMaxLengh(this.txtLinkMan.Text, 12))
            {
                MessageBox.Show(Language.Msg("��ϵ��¼�볬����"));
                this.txtLinkMan.Focus();
                return false;
            }

            //�ж��ַ�����������λ
            if (!FS.FrameWork.Public.String.ValidMaxLengh(this.cmbWorkAddress.Text, 50))
            {
                MessageBox.Show(Language.Msg("������λ¼�볬����"));
                this.cmbWorkAddress.Focus();
                return false;
            }

            //�ж��ַ�������ϵ�˵绰
            if (!FS.FrameWork.Public.String.ValidMaxLengh(this.txtLinkPhone.Text, 30))
            {
                MessageBox.Show(Language.Msg("��ϵ�˵绰¼�볬����"));
                this.txtLinkPhone.Focus();

                return false;
            }
            //��ͥ�绰
            if (!FS.FrameWork.Public.String.ValidMaxLengh(this.txtHomePhone.Text, 30))
            {
                MessageBox.Show(Language.Msg("��ͥ�绰¼�볬����"));
                this.txtHomePhone.Focus();

                return false;
            }
            //��λ�绰
            if (!FS.FrameWork.Public.String.ValidMaxLengh(this.txtWorkPhone.Text, 30))
            {
                MessageBox.Show(Language.Msg("��λ�绰¼�볬����"));
                this.txtWorkPhone.Focus();

                return false;
            }

            //���
            if (!FS.FrameWork.Public.String.ValidMaxLengh(this.cmbClinicDiagnose.Text, 50))
            {
                MessageBox.Show(Language.Msg("�������¼�볬����"));
                this.cmbClinicDiagnose.Focus();

                return false;
            }

            //���֤
            if (this.txtIDNO.Text.Trim() != string.Empty)
            {
                string errText = string.Empty;
                if (FS.FrameWork.WinForms.Classes.Function.CheckIDInfo(this.txtIDNO.Text, ref errText) == -1)
                {
                    MessageBox.Show(errText);
                    return false;
                }
            }
            if (Convert.ToInt32(this.txtBedInterval.Text) == 0)
            {
                MessageBox.Show(Language.Msg("���Ѽ�������Ǵ��������,����������"));
                txtBedInterval.Focus();
                txtBedInterval.SelectAll();
                return false;
            }
            if (this.cmbSex.Text.Trim() == string.Empty) 
            {
                MessageBox.Show("�����뻼���Ա�!");
                this.cmbSex.Focus();

                return false;
            }
           

            //if (Convert.ToInt32(this.mTxtIntimes.Text) == 0)
            //{
            //    MessageBox.Show(Language.Msg("סԺ���������Ǵ�������������������룡"));
            //    this.mTxtIntimes.Focus();
            //    this.mTxtIntimes.SelectAll();
            //    return false;
            //}
            //if (!FS.FrameWork.Public.String.ValidMaxLengh(this.txtIDNO.Text, 18))
            //{
            //    MessageBox.Show(Language.Msg("���֤����¼�볬��18λ��"));
            //    this.txtIDNO.Focus();

            //    return false;
            //}

            //��֤��չ��Ч���жϽӿ�
            if (this.registerExtend != null) 
            {
                Control errControl = new Control();

                if (!this.registerExtend.IsInputValid(errControl, ref this.errText)) 
                {
                    MessageBox.Show(Language.Msg(this.errText));
                    errControl.Focus();

                    return false;
                }
            }

            //У�����֤�ź����� {9B24289B-D017-4356-8A25-B0F76EB79D15}
           
            int returnValue = this.ProcessIDENNO(this.txtIDNO.Text.Trim(), EnumCheckIDNOType.Saveing);
            
            if (returnValue < 0)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// ��ÿؼ��������Ϣ,�ϳɻ��߻�����Ϣʵ��
        /// </summary>
        /// <param name="patient">���߻�����Ϣʵ��</param>
        /// <returns> �ɹ�: 1 ʧ�� : -1</returns>
        private int GetPatientInfomation(PatientInfo patient) 
        {
            if (patient == null) 
            {
                MessageBox.Show(Language.Msg("������Ϣʵ��Ϊ��!"));

                return -1;
            }

            patient.PID.PatientNO = this.txtInpatientNO.Text; //סԺ��
            patient.PID.CardNO = this.txtClinicNO.Text;//���￨��
            patient.ID = this.txtInpatientNO.Tag.ToString();//סԺ��ˮ��
            patient.SSN = this.txtMCardNO.Text;//ҽ����
            patient.ProCreateNO = this.txtComputerNO.Text;//�������յ��Ժ�
            patient.HomeZip = this.txtHomeZip.Text;//��ͥ��������
            if (this.isCanModifyInTime == true)
            {
                patient.PVisit.InTime = this.dtpInTime.Value;//��Ժ����
            }
            else
            {
                patient.PVisit.InTime = this.inpatientManager.GetDateTimeFromSysDateTime(); //��Ժ����
            }
            patient.Pact.ID = this.cmbPact.Tag.ToString();//��ͬ��λ����
            patient.Pact.Name = this.cmbPact.Text;//��ͬ��λ����
            patient.Pact.PayKind = this.GetPayKindFromPactID(patient.Pact.ID);//�������
            //��ʱ���ε� ����ʱ�����λ
            //patient.PVisit.PatientLocation.Bed.ID = this.txtBedNo.Text;//����
            patient.Name = this.txtName.Text;//����
            patient.Sex.ID = this.cmbSex.Tag.ToString();//�Ա�
            patient.Nationality.ID = this.cmbNation.Tag.ToString();//����
            patient.Birthday = this.dtpBirthDay.Value;//����
            patient.PVisit.PatientLocation.Dept.ID = this.cmbDept.Tag.ToString();//���ұ���
            try
            {
                patient.PVisit.PatientLocation.Dept.Name = ((Department)this.cmbDept.SelectedItem).Memo;//��������
            }
            catch (Exception ex)
            {
                MessageBox.Show(Language.Msg("סԺ�������벻��ȷ�����������룡")+ex.Message);
                return -1;
            }
            patient.CompanyName = this.cmbWorkAddress.Text;//������λ
            patient.MaritalStatus.ID = this.cmbMarry.Tag.ToString();//����״��
            patient.DIST = this.cmbBirthArea.Text;//����
            patient.Country.ID = this.cmbCountry.Tag.ToString();//����ID
            patient.Country.Name = this.cmbCountry.Text;//����
            patient.Profession.ID = this.cmbProfession.Tag.ToString();//ְλID
            patient.Profession.Name = this.cmbProfession.Text;//ְλ����
            patient.Kin.Name = this.txtLinkMan.Text;//��ϵ������
            patient.Kin.RelationPhone = this.txtLinkPhone.Text;//��ϵ�˱�ע-�绰
            patient.Kin.Relation.ID = this.cmbRelation.Tag.ToString();//�뻼�߹�ϵ����
            patient.Kin.Relation.Name = this.cmbRelation.Text;//�뻼�߹�ϵ
            patient.Kin.RelationAddress = this.cmbLinkAddress.Text;//��ϵ�˵�ַ
            patient.AddressHome = this.cmbHomeAddress.Text;//��ͥ��ַ
            patient.PhoneHome = this.txtHomePhone.Text;//���ߵ绰
            #region {9FF4736C-5D48-48a3-95A0-0E864346DB9D}
            patient.PhoneBusiness = this.txtWorkPhone.Text;//��λ�绰 
            #endregion
            patient.IDCard = this.txtIDNO.Text;//���֤
            patient.PVisit.AdmitSource.ID = this.cmbApproach.Tag.ToString();//��Ժ;��
            patient.PVisit.AdmitSource.Name = this.cmbApproach.Text;//��Ժ;��
            patient.PVisit.InSource.ID = this.cmbInSource.Tag.ToString();//��Ժ��Դ
            patient.PVisit.InSource.Name = this.cmbInSource.Text;//��Ժ��Դ
            patient.PVisit.Circs.ID = this.cmbCircs.Tag.ToString();//��Ժ���
            patient.PVisit.Circs.Name = this.cmbCircs.Text;//��Ժ���
            patient.DoctorReceiver.ID = this.cmbDoctor.Tag.ToString();//��סҽʦ

            #region addby xuewj 2010-3-15 {010BAFC3-96E2-4acc-AAD4-55320B9C2229}
            //addby zl 2010-8-27{28D36E56-E893-4482-B71C-BAB884E3591C} �˴������ˣ�����ʱ�Ͷ��������ˣ��ȴ򿪡�
            //hl7�� ��ʱ���� ����
            //patient.PVisit.AdmittingDoctor.ID = this.cmbDoctor.Tag.ToString();
            //patient.PVisit.AdmittingDoctor.Name = this.cmbDoctor.Text;
            //patient.PVisit.AttendingDoctor.ID = this.cmbDoctor.Tag.ToString();
            //patient.PVisit.AttendingDoctor.Name = this.cmbDoctor.Text;
            patient.DoctorReceiver.ID = this.cmbDoctor.Tag.ToString();//��סҽʦ

            #endregion

            try
            {
                patient.FT.FixFeeInterval = FS.FrameWork.Function.NConvert.ToInt32(this.txtBedInterval.Text);//���Ѽ��
            }
            catch
            {
                MessageBox.Show("���Ѽ���������벻��ȷ������������");
                return -1;
            }
            patient.ClinicDiagnose = this.cmbClinicDiagnose.Text;//�������
            patient.AreaCode = this.cmbArea.Tag.ToString();//����
            
            patient.FT.BloodLateFeeCost = FS.FrameWork.Function.NConvert.ToDecimal(this.mtxtBloodFee.Text);//Ѫ���ɽ�
            //·־�� �޸�סԺ���� Ŀ�ģ�����סԺ�Ǽǵ�סԺ����Ӧ������һ��סԺ������1
            patient.InTimes = NConvert.ToInt32(this.mTxtIntimes.Text);//סԺ���������ξ�������
            patient.FT.LeftCost = NConvert.ToDecimal(this.mTxtPrepay.Text);//Ԥ����
            patient.FT.PrepayCost = NConvert.ToDecimal(this.mTxtPrepay.Text);//Ԥ����

            #region ����
            patient.Surety.Mark = this.txtMark.Text;//������ע
            //������
            patient.Surety.SuretyPerson.ID = this.cmbSuretyPerson.Tag.ToString();
            patient.Surety.SuretyPerson.Name = this.cmbSuretyPerson.Text.ToString();
            //�������
            patient.Surety.SuretyCost = NConvert.ToDecimal(this.txtSuretyCost.Text);
            //��������
            patient.Surety.SuretyType.ID = this.cmbSuretyType.Tag.ToString();
            patient.Surety.SuretyType.Name = this.cmbSuretyType.Text.ToString();
            //{0374EA05-782E-4609-9CDC-03236AB97906}
            patient.Surety.Bank = this.cmbTransType1.bank.Clone();
            patient.Surety.InvoiceNO = this.inpatientManager.GetSequence("Fee.Inpatient.GetSeq.InvoiceNO");
            patient.Surety.State = "1";

            //����Ա
            patient.Surety.Oper.ID = inpatientManager.Operator.ID;
            #endregion

            //�ɼ���չ¼�����Ϣ
            if (this.registerExtend != null) 
            {
                if (this.registerExtend.GetExtentPatientInfomation(patient, ref this.errText) == -1) 
                {
                    MessageBox.Show(Language.Msg(this.errText));

                    return -1;
                }
            }
            //����
            patient.IsEncrypt = this.chbencrypt.Checked;

            if (patient.IsEncrypt)
            {
            
                patient.NormalName = FS.FrameWork.WinForms.Classes.Function.Encrypt3DES(patient.Name);
                patient.Name = "******";
            }

            //[2011-5-24] zhaozf �ж��Ƿ�¼���˻�ʿվ
            if (string.IsNullOrEmpty(this.cmbNurseCell.Tag.ToString())||string.IsNullOrEmpty(this.cmbNurseCell.Text))
            {
                MessageBox.Show("������סԺ����!");
                this.cmbNurseCell.Focus();
                return -1;
            }

            patient.PVisit.PatientLocation.NurseCell.ID = this.cmbNurseCell.Tag.ToString();//���ұ���
            try
            {
                patient.PVisit.PatientLocation.NurseCell.Name = ((Department)this.cmbNurseCell.SelectedItem).Memo;//��������
            }
            catch (Exception ex)
            {
                MessageBox.Show(Language.Msg("סԺ�������벻��ȷ�����������룡") + ex.Message);
                return -1;
            }
            #region ������������
            if (this.IsContainsInstate)
            {
                FS.HISFC.Models.Base.Bed bedObj = this.cmbBedNO.SelectedItem as FS.HISFC.Models.Base.Bed;
                patient.PVisit.PatientLocation.NurseCell = bedObj.NurseStation;
                //FS.HISFC.Models.Base.Department deptObj= managerIntegrate.GetDepartment(bedObj.NurseStation.ID);
                //if (deptObj != null)
                //{
                //    patient.PVisit.PatientLocation.NurseCell.Name = deptObj.Name;
                //}
                patient.PVisit.PatientLocation.Bed = bedObj;
                patient.PVisit.InState.ID = FS.HISFC.Models.Base.EnumInState.I;
            }
            else
            {
                patient.PVisit.InState.ID = FS.HISFC.Models.Base.EnumInState.R;
            }
            #endregion
            return 1;
        }

        /// <summary>
        /// ���û�����Ϣ���ؼ�
        /// </summary>
         /// <param name="patient">���߻�����Ϣʵ��</param>
        private void SetPatientInfomation(PatientInfo patient)
        {
            this.txtInpatientNO.Text = patient.PID.PatientNO;//סԺ�š�
            this.txtClinicNO.Text = patient.PID.CardNO;//���￨��
            this.txtInpatientNO.Tag = patient.ID;//סԺ��ˮ��
            this.txtMCardNO.Text = patient.SSN;//ҽ����
            this.cmbBedNO.Text = patient.PVisit.PatientLocation.Bed.ID;//����
            this.chbencrypt.Checked = patient.IsEncrypt;
            if (patient.IsEncrypt)
            {
                patient.Name = FS.FrameWork.WinForms.Classes.Function.Decrypt3DES(patient.NormalName);
            }
            this.txtName.Text = patient.Name;//��������
            
            this.cmbWorkAddress.Text = patient.CompanyName;//��˾����
            this.txtHomePhone.Text = patient.PhoneHome;//���ߵ绰
            this.txtWorkPhone.Text = patient.PhoneBusiness;//��λ�绰
            //��Ժ;��
            if (patient.PVisit.AdmitSource.ID == string.Empty)
            {
                if (this.cmbApproach.Items.Count > 0)
                {
                    this.cmbApproach.SelectedIndex = 0;
                }
            }
            else
            {
                this.cmbApproach.Tag = patient.PVisit.AdmitSource.ID;
            }
            //��Ժ��Դ
            if (patient.PVisit.InSource.ID == string.Empty)
            {
                this.cmbInSource.Tag = "1";
            }
            else
            {
                this.cmbInSource.Tag = patient.PVisit.InSource.ID;
            }
            //��Ժ���
            if (patient.PVisit.Circs.ID == "")
            {
                if (this.cmbCircs.Items.Count > 0)
                {
                    this.cmbCircs.SelectedIndex = 0;
                }
            }
            else
            {
                this.cmbCircs.Tag = patient.PVisit.Circs.ID;
            }
            this.cmbCountry.Tag = patient.Country.ID;//����
            this.cmbCountry.Focus();
            this.cmbHomeAddress.Text = patient.AddressHome;//��ͥ��ַ
            this.cmbPact.Tag = null;
            this.cmbPact.Text = string.Empty;
            this.cmbPact.Tag = patient.Pact.ID;//��ͬ��λ
            this.cmbBirthArea.Tag = patient.DIST;//�����أ�����
            this.cmbMarry.Tag = patient.MaritalStatus.ID;//����״��
            this.cmbSex.Tag = patient.Sex.ID;//�Ա�  
            if (patient.Nationality.ID != string.Empty && patient.Nationality.ID != null)
            {
                this.cmbNation.Tag = patient.Nationality.ID;
            }
            this.cmbProfession.Text = patient.Profession.Name;//ְλ
            this.cmbProfession.Tag = patient.Profession.ID;//ְλ
            #region {9FF4736C-5D48-48a3-95A0-0E864346DB9D}
            this.mtxtBloodFee.Text = patient.FT.BloodLateFeeCost.ToString(); 
            #endregion
            if (patient.Birthday == DateTime.MinValue)
            {
                this.dtpBirthDay.Value = this.inpatientManager.GetDateTimeFromSysDateTime().Date;
            }
            else
            {
                this.dtpBirthDay.Value = patient.Birthday;//Edit By Maokb
            }
            //this.txtAge.Text =this.inpatientManager.GetAge(this.dtpBirthDay.Value);//����  Edit By maokb
            this.setAge(this.dtpBirthDay.Value);
            if (patient.PVisit.InTime == DateTime.MinValue)
            {
                this.dtpInTime.Value = this.inpatientManager.GetDateTimeFromSysDateTime().Date;
            }
            else
            {
                this.dtpInTime.Value = patient.PVisit.InTime;//this.inpatientManager.GetDateTimeFromSysDateTime();//סԺ����
            }
            /*
             *�޸ı�ʶ��2AF24B84-500B-4D24-9890-2CD83D10F64F
             *�޸��ˣ�songrabit
             *�޸�ԭ�򣺸�ֵʱ��ϵ�˸�ֵ����
             *�޸ķ���������add -endadd����                 
             */
            this.txtLinkMan.Text = patient.Kin.Name;

            this.txtLinkPhone.Text = patient.Kin.RelationPhone;//��ϵ�˱�ע-�绰����ַ
            this.cmbRelation.Text = patient.Kin.Relation.Name; //�뻼�߹�ϵ
            this.cmbRelation.Tag = patient.Kin.Relation.ID;//�뻼�߹�ϵ
            this.cmbDept.Tag = patient.PVisit.PatientLocation.Dept.ID;//����
            this.mTxtPrepay.Text = patient.FT.PrepayCost.ToString();//Ԥ����
            this.txtIDNO.Text = patient.IDCard;//���֤����

            if (patient.InTimes == 0)
            {
                patient.InTimes = 1;
            }
            else
            {
                //ȥ����סԺ������+1
                int times = this.radtIntegrate.GetMaxInTimes(patient.PID.PatientNO);
                if (times <= 0)
                {
                    patient.InTimes += 1;
                }
                else
                {
                    patient.InTimes = times + 1;
                }
            }

            this.mTxtIntimes.Text = patient.InTimes.ToString();// סԺ����
            this.cmbLinkAddress.Text = patient.Kin.RelationAddress; //��ϵ�˵�ַ
            this.cmbClinicDiagnose.Text = patient.ClinicDiagnose; //�������
            this.cmbArea.Tag = patient.AreaCode; //����
            
            this.cmbDoctor.Tag = patient.PVisit.AdmittingDoctor.ID;
            try
            {
                FS.HISFC.Models.Base.Employee myEmpInfo = this.managerIntegrate.GetEmployeeInfo(patient.PVisit.AdmittingDoctor.ID);
                this.cmbDoctor.Text = myEmpInfo.Name; //��סҽʦ
            }
            catch { }
            //this.cmbMarry.Text = patient.MaritalStatus.ID.ToString(); //����״��
            this.txtComputerNO.Text = patient.ProCreateNO;//�������յ��Ժ�
            //{F0BF027A-9C8A-4bb7-AA23-26A5F3539586}
            this.cmbNurseCell.Tag = patient.PVisit.PatientLocation.NurseCell.ID;
            this.txtHomeZip.Text = patient.HomeZip;//��ͥ�ʱ�
        }

        /// <summary>
        /// ���û�����Ϣ���ؼ�
        /// </summary>
        /// <param name="patient">���߻�����Ϣʵ��</param>
        /// <param name="isAll">�Ƿ���ʾȫ����Ϣ��Addʱֻ��ʾ������Ϣ</param>
        protected virtual void SetPatientInfomation(PatientInfo patient, bool isAll)
        {
            #region ��ʾ��Ҫ��Ϣ

            this.txtInpatientNO.Text = patient.PID.PatientNO.PadLeft(10, '0');//סԺ�š�
            if (string.IsNullOrEmpty(this.patientInfomation.PID.CardNO))
            {
                this.txtClinicNO.Text = "T" + this.txtInpatientNO.Text.Substring(1);
            }
            else
            {
                this.txtClinicNO.Text = patient.PID.CardNO.PadLeft(10, '0');//���￨��
            }

            this.txtInpatientNO.Tag = patient.ID;//סԺ��ˮ��

            //�Ƿ����
            this.chbencrypt.Checked = patient.IsEncrypt;
            if (patient.IsEncrypt)
            {
                patient.Name = FS.FrameWork.WinForms.Classes.Function.Decrypt3DES(patient.NormalName);
            }
            this.txtName.Text = patient.Name;//��������

            //this.txtWorkAddress.Text = patient.CompanyName;//��˾����
            //this.txtBirthArea.Text = patient.AreaCode; //������
            this.txtHomePhone.Text = patient.PhoneHome;//���ߵ绰
            this.txtWorkPhone.Text = patient.PhoneBusiness;//��λ�绰    
            this.txtHomeZip.Text = patient.HomeZip;//��ͥ�ʱ�

            this.cmbCountry.Tag = patient.Country.ID;//����
            if (!string.IsNullOrEmpty(patient.Country.Name))
            {
                this.cmbCountry.Text = patient.Country.Name;
            }
            //this.txtHomeAddress.Text = patient.AddressHome;//��ͥ��ַ

            if (!string.IsNullOrEmpty(patient.DIST))
            {
                this.cmbBirthArea.Text = patient.DIST;//�����أ�����
            }
            //����״��
            this.cmbMarry.Tag = patient.MaritalStatus.ID;
            if (!string.IsNullOrEmpty(patient.MaritalStatus.Name))
            {
                this.cmbMarry.Text = patient.MaritalStatus.Name.ToString();
            }
            this.cmbSex.Tag = patient.Sex.ID;//�Ա�
            //����
            if (patient.Nationality.ID != string.Empty && patient.Nationality.ID != null)
            {
                this.cmbNation.Tag = patient.Nationality.ID;
            }
            this.cmbProfession.Tag = patient.Profession.ID;//ְҵID
            if (!string.IsNullOrEmpty(patient.Profession.Name))
            {
                this.cmbProfession.Text = patient.Profession.Name;//ְҵ����
            }

            //��������
            if (patient.Birthday == DateTime.MinValue)
            {
                this.dtpBirthDay.Value = this.inpatientManager.GetDateTimeFromSysDateTime().Date;
            }
            else
            {
                this.dtpBirthDay.Value = patient.Birthday;//Edit By Maokb
            }
            this.setAge(this.dtpBirthDay.Value);
            //this.txtAge.Text = this.inpatientManager.GetAge(feeIntegrate.ToDateTime(this.dtpBirthDay.Text));//����  Edit By maokb
            //���޸�
            //this.txtAge.Text = this.feeIntegrate.GetAgeStr(feeIntegrate.ToDateTime(this.dtpBirthDay.Text)); //����

            this.txtLinkMan.Text = patient.Kin.Name;//��ϵ������
            this.txtLinkPhone.Text = patient.Kin.RelationPhone;//��ϵ�˱�ע-�绰����ַ

            //�뻼�߹�ϵ
            if (!string.IsNullOrEmpty(patient.Kin.Relation.ID))
            {
                this.cmbRelation.Tag = patient.Kin.Relation.ID;
                //this.cmbRelation.Text = helperRelation.GetObjectFromID(patient.Kin.Relation.ID).Name;
            }
            else
            {
                this.cmbRelation.Text = patient.Kin.Relation.Name;
            }

            this.txtIDNO.Text = patient.IDCard;//���֤����

            if (patient.InTimes == 0)
            {
                patient.InTimes = 1;
            }
            else
            {
                //ȥ����סԺ������+1
                int times = this.radtIntegrate.GetMaxInTimes(patient.PID.PatientNO);
                if (times <= 0)
                {
                    patient.InTimes += 1;
                }
                else
                {
                    patient.InTimes = times + 1;
                }
            }
            this.mTxtIntimes.Text = patient.InTimes.ToString();// סԺ����
            //this.txtLinkAddr.Text = patient.Kin.RelationAddress; //��ϵ�˵�ַ

            //��ݸ��ҵ������Ϣ
            //���޸�
            //this.dgPatientInfo = inpaitentManager.GetDgInmainInfoByID(patient.ID);
            //if (dgPatientInfo != null)
            //{
            //this.cmbComInsure.Tag = this.dgPatientInfo.ComInsurance.ID;
            //this.cmbComInsure.Text = this.dgPatientInfo.ComInsurance.Name;
            //this.cbIsSelfDeal.Checked = FS.FrameWork.Function.NConvert.ToBoolean(this.dgPatientInfo.IsSelfDeal);
            //this.cmbOldDept.Tag = this.dgPatientInfo.ExtendFlag;
            //    this.txtAddressNow.Text = dgPatientInfo.ExtendFlag1;
            //}
            //if (string.IsNullOrEmpty(this.txtAddressNow.Text))
            //{
            //    this.txtAddressNow.Text = this.txtHomeAddress.Text;
            //}

            #endregion

            #region ��Щ���Բ���ʾ

            if (isAll)
            {
                this.txtMCardNO.Text = patient.SSN;//ҽ����
                this.cmbPact.Text = patient.Pact.Name;
                this.cmbPact.Tag = patient.Pact.ID;//��ͬ��λ
                this.cmbDept.Tag = patient.PVisit.PatientLocation.Dept.ID;//����
                //��סҽʦ
                if (patient.DoctorReceiver != null && !string.IsNullOrEmpty(patient.DoctorReceiver.ID.ToString()))
                {
                    try
                    {
                        this.cmbDoctor.Tag = patient.DoctorReceiver.ID;
                        this.cmbDoctor.Text = this.managerIntegrate.GetEmployeeInfo(patient.DoctorReceiver.ID).Name;
                    }
                    catch
                    {
                    }
                }
                this.cmbClinicDiagnose.Text = patient.ClinicDiagnose; //�������
                this.txtComputerNO.Text = patient.ProCreateNO;//�������յ��Ժ�

                //��ݸ��ҵ������Ϣ
                //���޸�
                //if (dgPatientInfo != null)
                //{
                //    this.cmbComInsure.Tag = this.dgPatientInfo.ComInsurance.ID;
                //    this.cmbComInsure.Text = this.dgPatientInfo.ComInsurance.Name;
                //    this.cbIsSelfDeal.Checked = FS.FrameWork.Function.NConvert.ToBoolean(this.dgPatientInfo.IsSelfDeal);
                //    this.cmbOldDept.Tag = this.dgPatientInfo.ExtendFlag;
                //}

                //����
                this.cmbBedNO.Text = patient.PVisit.PatientLocation.Bed.ID;

                this.mTxtPrepay.Text = patient.FT.PrepayCost.ToString();//Ԥ����
                this.cmbArea.Tag = patient.AreaCode; //����

                //סԺ����
                if (patient.PVisit.InTime == DateTime.MinValue)
                {
                    this.dtpInTime.Value = this.inpatientManager.GetDateTimeFromSysDateTime().Date;
                }
                else
                {
                    if (patient.PVisit.InTime > new DateTime(1975, 1, 1))
                    {
                        this.dtpInTime.Value = patient.PVisit.InTime;//this.inpatientManager.GetDateTimeFromSysDateTime();//סԺ����
                    }
                }

                //��Ժ;��
                if (patient.PVisit.AdmitSource.ID == string.Empty)
                {//���޸�
                    //if (this.cmbApproach.Items.Count > 0)
                    //{
                    //    this.cmbApproach.SelectedIndex = 0;
                    //}
                }
                {
                    this.cmbApproach.Tag = patient.PVisit.AdmitSource.ID;
                }

                //��Ժ��Դ
                if (patient.PVisit.InSource.ID == string.Empty)
                {
                    this.cmbInSource.Tag = "1";
                }
                else
                {
                    this.cmbInSource.Tag = patient.PVisit.InSource.ID;

                    //if (this.cmbInSource.Text.Trim() == "ת��")// || this.cmbInSource.Tag.ToString() == "3")
                    //{
                    //    this.cmbOldDept.Visible = true;
                    //    this.lblOldDept.Visible = true;
                    //}
                    //else
                    //{
                    //    this.cmbOldDept.Visible = false;
                    //    this.lblOldDept.Visible = false;
                    //    this.cmbOldDept.Text = "";
                    //}
                }

                //���޸�
                //ת��ɿ���
                //if (dgPatientInfo != null)
                //{
                //    this.cmbOldDept.Tag = this.dgPatientInfo.ExtendFlag;
                //}

                ////��Ժ���
                //if (patient.PVisit.Circs.ID == "")
                //{
                //    if (this.cmbCircs.Items.Count > 0)
                //    {
                //        this.cmbCircs.SelectedIndex = 0;
                //    }
                //}
                //else
                //{
                //    this.cmbCircs.Tag = patient.PVisit.Circs.ID;
                //}
            }
            #endregion
        }

        /// <summary>
        /// ����Ԥ����
        /// </summary>
        /// <param name="prepay">Ԥ����ʵ��</param>
        /// <returns>�ɹ� 1 ʧ��: -1</returns>
        private int DealPreapy(FS.HISFC.Models.Fee.Inpatient.Prepay prepay)
        {
            string invoiceNO = null;//��Ʊ��

            //���û������Ԥ����,��ôֱ�ӷ���
            if (this.patientInfomation.FT.PrepayCost <= 0)
            {
                return 1;
            }

            prepay.FT.PrepayCost = this.patientInfomation.FT.PrepayCost;
            prepay.PayType.ID = this.cmbPayMode.Tag.ToString();
            prepay.PayType.Name = this.cmbPayMode.Text;
            prepay.PrepayState = "0";
            prepay.BalanceState = "0";
            prepay.BalanceNO = 0;
            prepay.TransferPrepayState = "0";
            prepay.PrepayOper.OperTime = this.inpatientManager.GetDateTimeFromSysDateTime();
            prepay.PrepayOper.ID = this.inpatientManager.Operator.ID;
            //{021A7D2F-2C91-4144-B7A9-E26AE53FA985}
            //invoiceNO = this.feeIntegrate.GetNewInvoiceNO(EnumInvoiceType.P);
            invoiceNO = this.feeIntegrate.GetNewInvoiceNO("P");

            if (invoiceNO == null || invoiceNO == string.Empty)
            {
                MessageBox.Show("���Ԥ����Ʊ�ݺ�ʧ��!" + this.feeIntegrate.Err);

                return -1;
            }

            prepay.RecipeNO = invoiceNO;

            if (this.inpatientManager.PrepayManager(this.patientInfomation, prepay) == -1)
            {
                MessageBox.Show("����Ԥ����ʧ��" + this.inpatientManager.Err);

                return -1;
            }

            return 1;
        }

        //��ӡԤ����
        protected virtual void PrintPrepayInvoice(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.HISFC.Models.Fee.Inpatient.Prepay prepay, bool isReturn)
        {
            if (patientInfo.IsEncrypt)
            {
                patientInfo.Name = FS.FrameWork.WinForms.Classes.Function.Decrypt3DES(patientInfo.NormalName);
            }
            this.prepayPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.FeeInterface.IPrepayPrint)) as FS.HISFC.BizProcess.Interface.FeeInterface.IPrepayPrint;
            //regprint.SetPrintValue(regObj,regmr);
            //this.prepayPrint = new ucPrepayPrint();
            if (this.prepayPrint == null)
            {
                //this.prepayPrint = new ucPrepayPrint();
            }


            this.prepayPrint.SetValue(patientInfo, prepay);
            this.prepayPrint.Print();


        }

        /// <summary>
        /// ����סԺ������Ϣ
        /// </summary>
        /// <param name="t">���ݿ�����</param>
        /// <returns>�ɹ� 1 ʧ��: -1</returns>
        private int InsertMainInfo()
        {
            #region addby xuewj {010BAFC3-96E2-4acc-AAD4-55320B9C2229} adt-Patient Registration

            if (this.adt == null)
            {
                this.adt = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.IHE.IADT)) as FS.HISFC.BizProcess.Interface.IHE.IADT;
            }
            if (this.adt != null)
            {
                if (this.hl7 == "A01")
                    adt.RegInpatient(this.patientInfomation.Clone());
                else
                    adt.OutpatientToInpatient(this.patientInfomation.Clone());
            }
            #endregion

            int returnValue = 0;//���뺯���ķ���ֵ

            returnValue = this.radtIntegrate.RegisterPatient(this.patientInfomation);

            if (returnValue == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(this.radtIntegrate.Err);

                return -1;
                #region delete
                ////�����ظ�
                ////�жϲ���--��ֹסԺ���ظ����������ظ�
                //if (this.radtIntegrate.DBErrCode == 1)
                //{
                //    if (this.isAutoInpatientNO)
                //    {

                //        int tryCount = 0;//���Ի��סԺ�ŵĴ���
                //    SetNewNo:

                //        tryCount++;

                //        //���Դ�������10��,��ô������εǼ�
                //        if (tryCount >= 10)
                //        {
                //            FS.FrameWork.Management.PublicTrans.RollBack();
                //            MessageBox.Show(Language.Msg("��ȡסԺ�ų�������ϵ��Ϣ�ƣ�") + this.patientInfomation.PID.PatientNO);

                //            return -1;
                //        }

                //        if (this.patientInfomation.PatientNOType == EnumPatientNOType.Temp)
                //        {
                //            //this.AutoTempPatientNo();
                //        }
                //        else
                //        {
                //            this.GetAutoInpatientNO(string.Empty);
                //            //{74CD8A32-2A99-4b5f-BD6D-4FFAC604284C} ��ʾסԺ�ű仯
                //            MessageBox.Show("ѡ����סԺ��: " + this.patientInfomation.PID.PatientNO + "�Ѿ�ռ��,�·����סԺ��Ϊ: " + this.txtInpatientNO.Text.Trim());
                //            //{74CD8A32-2A99-4b5f-BD6D-4FFAC604284C} ����
                //        }

                //        //���¸�ֵסԺ��
                //        ResetPatientNO();

                //        if (this.radtIntegrate.RegisterPatient(this.patientInfomation) <= 0)
                //        {
                //            if (this.radtIntegrate.DBErrCode == 1)
                //            {
                //                goto SetNewNo;
                //            }
                //            else
                //            {
                //                FS.FrameWork.Management.PublicTrans.RollBack();
                //                MessageBox.Show(this.radtIntegrate.Err);

                //                return -1;
                //            }
                //        }

                //    }
                //}
                //else
                //{
                //    FS.FrameWork.Management.PublicTrans.RollBack();
                //    MessageBox.Show(this.radtIntegrate.Err);

                //    return -1;
                //}
                #endregion
            }

            return 1;
        }

        /// <summary>
        /// ���»��סԺ��
        /// </summary>
        private int ReGetPatientNO()
        {
            string patientNO = string.Empty;
            bool isRecycle = false;
            if (this.radtIntegrate.GetAutoPatientNO(ref patientNO, ref isRecycle) == -1)
            {
                MessageBox.Show(Language.Msg("����Զ�����סԺ�ų���!") + this.radtIntegrate.Err);
                return -1;
            }
            this.patientInfomation.PID.PatientNO = patientNO;
            this.patientInfomation.ID = "T001";
            this.patientInfomation.PID.CardNO = "T" + patientNO.Substring(1, 9);
            return 1;
        }

        /// <summary>
        /// ˢ����ʾ�����б�
        /// </summary>
        private void RefreshPatientLists()
        {
            DateTime beginTime = inpatientManager.GetDateTimeFromSysDateTime();
            
            //ȡ����ʱ���0��
            beginTime = new DateTime(beginTime.Year, beginTime.Month, beginTime.Day);

            ArrayList patients = radtIntegrate.QueryPatientsByDateTime(beginTime, beginTime.AddDays(1));

            if (patients == null) 
            {
                MessageBox.Show(Language.Msg("ˢ���ѵǼǻ�����Ϣ����!") + radtIntegrate.Err);

                return;
            }

            this.spPatientInfo_Sheet1.RowCount = 0;
            this.spPatientInfo_Sheet1.RowCount = patients.Count;

            PatientInfo patient = null;//��ǰ������Ϣ

            for (int i = 0; i < patients.Count; i++)
            {
                patient = patients[i] as PatientInfo;

                this.spPatientInfo_Sheet1.SetValue(i, (int)PatientLists.PatientNO, patient.PID.PatientNO);//סԺ��
                this.spPatientInfo_Sheet1.SetValue(i, (int)PatientLists.Name, patient.Name);//����
                this.spPatientInfo_Sheet1.SetValue(i, (int)PatientLists.Sex, patient.Sex.Name);//�Ա�
                this.spPatientInfo_Sheet1.SetValue(i, (int)PatientLists.Dept, patient.PVisit.PatientLocation.Dept.Name);
                this.spPatientInfo_Sheet1.SetValue(i, (int)PatientLists.IDNO, patient.IDCard);//���֤��
                this.spPatientInfo_Sheet1.SetValue(i, (int)PatientLists.HomeAddress, patient.AddressHome);//��ͥסַ
                this.spPatientInfo_Sheet1.SetValue(i, (int)PatientLists.InDate, patient.PVisit.InTime);//��Ժʱ��
                this.spPatientInfo_Sheet1.SetValue(i, (int)PatientLists.InState, patient.PVisit.InState.Name);//״̬

                this.spPatientInfo_Sheet1.Rows[i].Tag = patient;
            }
        }

        /// <summary>
        /// ��õ�ǰ����ؼ�
        /// </summary>
        /// <returns>�ɹ�: ��õ�ǰ����ÿؼ�, ʧ��: null</returns>
        private Control GetFocusedControl() 
        {
            Control focusedControl = null;
            
            foreach (Control c in this.plInfomation.Controls) 
            {
                if (c.Focused) 
                {
                    focusedControl = c;
                    break;
                }
            }

            return focusedControl;
        }

        /// <summary>
        /// ��һ����������ÿؼ���ý���
        /// </summary>
        /// <param name="nowControl">��ǰ��ý���ÿؼ�</param>
        private void SetNextControlFocus(Control nowControl) 
        {
            if (nowControl == null) 
            {
                return;
            }

            Control focusedControl = this.GetNextControl(nowControl, false);

            if (focusedControl == null) 
            {
                return;
            }

            if (!this.mustInputHashTable.ContainsValue(nowControl)) 
            {
                this.SetNextControlFocus(focusedControl);
            }
            else
            {
                focusedControl.Focus();
            }
        }

        /// <summary>
        /// ��һ����������ÿؼ���ý���
        /// </summary>
        private void SetNextControlFocus() 
        {
            this.SetNextControlFocus(GetFocusedControl());
        }

        /// <summary>
        /// ��������סԺ��
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        private int DealInputInpatientNO() 
        {
            //���û��¼��סԺ�ţ���ȡ��סԺ��   

            if (this.txtInpatientNO.Text.Trim() == string.Empty)
            {
                this.Clear();
                //GetAutoInpatientNO(string.Empty);
                this.GetAutoPatientNO();
                 
            }
            else
            {
                string inpatientNO = FS.FrameWork.Public.String.FillString(this.txtInpatientNO.Text);
                this.Clear();

                this.txtInpatientNO.Text = inpatientNO;

                //if (this.GetAutoInpatientNO(this.txtInpatientNO.Text) == -1)
                //{
                //    return -1;
                //}
                if (this.GetInputPatientNO(inpatientNO) == -1)
                {
                    return -1;
                }
            }

            return 1;
        }

        /// <summary>
        /// ��������סԺ��
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        private int DealInputInpatientNO(string patientNo)
        {
            //this.txtName.Enabled = true;

            this.dtpInTime.Value = this.inpatientManager.GetDateTimeFromSysDateTime(); //��Ժ����
            //���û��¼����Ϣ,��ôֹͣ
            //if (this.txtInpatientNO.Text.Trim() == string.Empty)
            if (patientNo.Trim() == string.Empty)
            {
                return -1;
            }

            //this.txtInpatientNO.Text = FS.FrameWork.Public.String.FillString(this.txtInpatientNO.Text);
            patientNo = FS.FrameWork.Public.String.FillString(patientNo);

            if (this.GetAutoInpatientNO(patientNo) == -1)
            {
                this.cmbSex.Focus();
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// ���þ�ϵͳ������Ϣ
        /// </summary>
        /// <param name="oldPatientInfo"></param>
        private int SetOldPatientInfo(PatientInfo oldPatientInfo)
        {
            PatientInfo patient = new PatientInfo();
            this.dtpInTime.Value = this.inpatientManager.GetDateTimeFromSysDateTime(); //��Ժ����
            //û������סԺ��,˵��Ϊ��һ����Ժ.
            if (oldPatientInfo.PID.PatientNO == string.Empty)
            {
                MessageBox.Show(Language.Msg("����סԺ��Ϊ��!"));
                return -1;
            }
            else
            {
                if (this.radtIntegrate.CreateAutoInpatientNO(oldPatientInfo.PID.PatientNO, ref patient) == -1)
                {
                    MessageBox.Show(Language.Msg("���סԺ�ų���!") + this.radtIntegrate.Err);

                    return -1;
                }  //��ǰס��Ժ
                if (patient.User03 == "SECOND")
                {
                    //�ж���Ժ״̬
                    if (patient.PVisit.InState.ID.ToString() == "R" || patient.PVisit.InState.ID.ToString() == "I" || patient.PVisit.InState.ID.ToString() == "P")// || patient.PVisit.InState.ID.ToString() == "B")
                    {
                        MessageBox.Show(Language.Msg("�˻�����Ժ����!"));
                        this.patientInfomation = new PatientInfo();
                        this.txtInpatientNO.Text = string.Empty;
                        this.txtInpatientNO.Tag = string.Empty;
                        // this.txtClinicNO.Text = string.Empty;
                        this.Clear();
                        return -1;
                    }
                    else//��ǰס��ԺĿǰ����Ժ	
                    {
                        FS.FrameWork.WinForms.Classes.Function.Msg("��סԺ�����ϴε�סԺ��Ϣ��", 111);
                        //��մ���
                        patient.PVisit.PatientLocation.Bed.ID = string.Empty;
                        //�����������޸�
                        this.txtName.Enabled = false;
                        //�����渳ֵ
                        if (!isReadCard)
                        {
                            this.SetPatientInfomation(patient, false);
                        }
                        else
                        {
                            this.txtInpatientNO.Text = patient.PID.PatientNO;
                            this.txtInpatientNO.Tag = patient.ID;
                            //this.txtClinicNO.Text = patient.PID.CardNO;
                            //���޸�
                            //patient.InTimes = this.inpatientRadt.GetMaxInTimes(patient.PID.PatientNO);
                            this.mTxtIntimes.Text = (patient.InTimes + 1).ToString();
                            this.patientInfomation.User03 = patient.User03;
                        }

                        //this.mTxtIntimes.Text = (patient.InTimes + 1).ToString();
                        this.dtpInTime.Value = this.inpatientManager.GetDateTimeFromSysDateTime();//סԺ����
                        this.cmbDoctor.Text = string.Empty;//��סҽʦ
                        this.cmbDept.Focus();

                        return -1;
                    }
                }
                else
                {
                    oldPatientInfo.ID = patient.ID;
                    //��ϵͳ����סԺ����+1
                    //oldPatientInfo.InTimes = oldPatientInfo.InTimes + 1;
                    this.SetPatientInfomation(oldPatientInfo, false);
                }
            }
            return 1;
        }

        /// <summary>
        /// ����ؼ��س��¼�
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        private int DealControlEnterEvents() 
        {
            if (this.txtInpatientNO.Focused) 
            {
                return this.DealInputInpatientNO();
            }

            if (this.txtClinicNO.Focused) 
            {
                //{61D727CB-A325-4bb4-AE74-1116440FC7C6}
                string cardNO = txtClinicNO.Text.Trim();
                FS.HISFC.Models.Account.AccountCard accountCard = new FS.HISFC.Models.Account.AccountCard();
                int resultValue = feeIntegrate.ValidMarkNO(cardNO,ref accountCard);
                if (resultValue <= 0)
                {
                    cardNO = FS.FrameWork.Public.String.FillString(cardNO);
                }
                else
                {
                    cardNO = accountCard.Patient.PID.CardNO;
                }
                
                if (cardNO == string.Empty) 
                {
                    return -1;
                }

                return this.GetPatientFromClinic(cardNO);
            }

            //���ܻ������� ���ڸ���ʹ�ð�
            //if (this.ctlMgr.Hospital.ID == "YLV21053")
            //{
                if (this.cmbSex.Focused && this.cmbSex.Tag != null && !string.IsNullOrEmpty(this.cmbSex.Text))
                {
                    if (string.IsNullOrEmpty(this.txtName.Text))
                    {
                        return -1;
                    }
                    string numberCheck = "0123456789";
                    if (!numberCheck.Contains(txtName.Text.Substring(0, 1)))
                    {
                        frmQueryPatientInfo ucQueryPatientNo = new frmQueryPatientInfo();
                        FS.HISFC.Models.RADT.PatientInfo patient = new PatientInfo();

                        patient.Name = this.txtName.Text;
                        patient.Sex.ID = this.cmbSex.Tag.ToString();
                        patient.Sex.Name = this.cmbSex.Text;

                        ucQueryPatientNo.PatientInfo = patient;
                        if (ucQueryPatientNo.Query(enumQueryType.NameSex) != -1)
                        {
                            ucQueryPatientNo.ShowDialog(this);
                        }

                        if (!ucQueryPatientNo.IsSelect)
                        {
                            this.isNewRegPerson = true;
                            return 1;
                        }
                        this.isNewRegPerson = false;

                        if (ucQueryPatientNo.IsNewPerson)
                        {
                            this.DealInputInpatientNO(ucQueryPatientNo.PatientInfo.PID.PatientNO);
                        }
                        else
                        {
                            this.SetOldPatientInfo(ucQueryPatientNo.PatientInfo);
                        }
                    }
                //}
            }


            //{538F0253-AB89-4ce3-8C2A-7E8F5C6EDBF5}�����������ջ���
            if (this.dtpBirthDay.Focused) 
            {
                DateTime current = this.inpatientManager.GetDateTimeFromSysDateTime().Date;

                if (this.dtpBirthDay.Value.Date > current)
                {
                    MessageBox.Show("�������ڲ��ܴ��ڵ�ǰʱ��!", "��ʾ");
                    this.dtpBirthDay.Focus();
                    return -1;
                }

                //��������
                //if (this.dtpBirthDay.Value.Date != current)
                //{
                //    this.setAge(this.dtpBirthDay.Value);
                //}
                // ���ý����ж�
                this.setAge(this.dtpBirthDay.Value);

                this.cmbWorkAddress.Focus();

                return -1;
            }
            if (this.txtAge.Focused) 
            {
                 this.getBirthday();

                 this.cmbUnit.Focus();
               
                return -1;
            }
            if (this.cmbUnit.Focused) 
            {
                this.cmbWorkAddress.Focus();

                return -1;
            }

            if (this.cmbDept.Focused)
            {
                //{1BFFE533-C177-46bf-93C6-093DD7344DAF} wbo 2010-08-19
                //this.cmbNurseCell.Focus();
                //this.cmbBedNO.Focus();

                //[2011-5-24] zhaozf ��ѡ��ʿվ�ֲ��ñ��棬Ϊʲô��Ҫ�����أ�
                this.cmbNurseCell.Focus();                
                return -1;
            }

            if (this.cmbNurseCell.Focused)
            {
                this.cmbBedNO.Focus();
                return -1;
            }
            //{4B86EBA7-5616-4787-A9F2-CF7EEAA85D92}
            if (this.txtMCardNO.Focused)
            {
                if (this.GetGFPatientInfo() > 0)
                {
                    if (txtInpatientNO.Text.Trim().Length > 0)
                    {
                        return 1;
                    }
                    else
                    {
                        this.txtInpatientNO.Focus();
                        this.txtMCardNO.Enabled = false;
                    }
                    return -1;
                }
            }


            //{538F0253-AB89-4ce3-8C2A-7E8F5C6EDBF5}�����������ջ��� ����

            return 1;
        }

        /// <summary>
        /// �����ʱסԺ��ˮ��
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        private int AutoTempPatientNO()
		{
            //this.txtInpatientNO.Enabled = false;
            //this.btnAutoInpatientNO.Enabled = false;
			string pNO = string.Empty;

            PatientInfo patient = new PatientInfo();
            if (this.radtIntegrate.CreateAutoInpatientNO(patient) == -1)
            {
                MessageBox.Show(Language.Msg("����Զ�����סԺ�ų���!") + this.radtIntegrate.Err);

                return -1;
            }
            //pNO = this.inpatientManager.GetTempPatientNO(this.autoPatientParms);
            //if(pNO == "000000") 
            //{
            //    pNO = this.autoPatientParms + "000000";				
            //}

            //pNO = (NConvert.ToInt32(pNO) + 1).ToString().PadLeft(10, '0');

            this.txtInpatientNO.Text = patient.PID.PatientNO;
            this.txtInpatientNO.Tag = patient.ID;//"ZY01" + pNO;
            this.txtClinicNO.Text = FS.FrameWork.Public.String.FillString(txtClinicNO.Text.Trim()); 
			this.mTxtIntimes.Text = "1";
			this.patientInfomation.User03 = "TEMP";
			
			return 0; 
		}

        /// <summary>
        /// ͨ�����￨�Ż��סԺ�ŵ���Ϣ
        /// </summary>
        /// <param name="cardNO">���￨��</param>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        private int GetPatientFromClinic(string cardNO) 
        {
            //��ȡ���סԺ��---�������һ��סԺ��סԺ��ˮ��
	            string inpatientNO = string.Empty;

            inpatientNO = this.radtIntegrate.GetMaxPatientNOByCardNO(cardNO);

            //���ݿ����!
            if (inpatientNO == null)
            {
                return -2;
            }

            if (inpatientNO.Trim() == string.Empty)
            {
                //û��סԺ��Ϣ���黼�߻�����Ϣ��
                FS.HISFC.Models.RADT.PatientInfo pInfo = this.radtIntegrate.QueryComPatientInfo(cardNO);
                if (pInfo == null)
                {
                    MessageBox.Show(Language.Msg("��û��߻�����Ϣ����!") + this.radtIntegrate.Err);

                    return -1;
                }

                if (this.isAutoInpatientNO)
                {
                    this.SetPatientInfomation(pInfo);
                    this.AutoTempPatientNO();
                    
                }
                else
                {
                    this.SetPatientInfomation(pInfo);
                    txtClinicNO.Focus();
                }
            }
            else//�ҵ����ϴ�סԺ��Ϣ 
            {
                FS.HISFC.Models.RADT.PatientInfo pInfo = this.radtIntegrate.GetPatientInfomation(inpatientNO);
                //if (pInfo.PVisit.InState.ID.ToString() == "R" || pInfo.PVisit.InState.ID.ToString() == "B" || pInfo.PVisit.InState.ID.ToString() == "P") 
                if (pInfo.PVisit.InState.ID.ToString() == "R" || pInfo.PVisit.InState.ID.ToString() == "B" || pInfo.PVisit.InState.ID.ToString() == "P" || pInfo.PVisit.InState.ID.ToString() == "I") //{EFD5F36A-4179-4413-A4AA-DBB199B2AC49}
                {
                    MessageBox.Show(Language.Msg("�˻�����Ժ����"));
                    this.txtClinicNO.SelectAll();
                    this.txtClinicNO.Focus();

                    return -1;
                }

                pInfo.InTimes++;
                //pInfo.ID = "ZY" + pInfo.InTimes.ToString().PadLeft(2, '0') + pInfo.PID.PatientNO;

                pInfo.ID = radtIntegrate.GetNewInpatientNO();

                this.txtInpatientNO.Text = pInfo.PID.PatientNO;
                this.txtInpatientNO.Tag = pInfo.ID;
                this.txtClinicNO.Text = pInfo.PID.CardNO;
                pInfo.PVisit.PatientLocation.Bed.ID = string.Empty;
                pInfo.PVisit.InTime = this.inpatientManager.GetDateTimeFromSysDateTime();
                this.SetPatientInfomation(pInfo);
                this.cmbPact.Focus();
            }

            return 1;
        }

        /// <summary>
        /// {9B24289B-D017-4356-8A25-B0F76EB79D15}
        /// </summary>
        /// <param name="idNO"></param>
        /// <param name="enumType"></param>
        /// <returns></returns>
        private int ProcessIDENNO(string idNO, EnumCheckIDNOType enumType)
        {
            string errText = string.Empty;

            if (string.IsNullOrEmpty(idNO)) //Ϊ�յĲ�����
            {
                return 1;
            }

            //У�����֤��


            //{99BDECD8-A6FC-44fc-9AAA-7F0B166BB752}

            //string idNOTmp = FS.FrameWork.WinForms.Classes.Function.TransIDFrom15To18(idNO);
            string idNOTmp = string.Empty;
            if (idNO.Length == 15)
            {
                idNOTmp = FS.FrameWork.WinForms.Classes.Function.TransIDFrom15To18(idNO);
            }
            else
            {
                idNOTmp = idNO;
            }

            //У�����֤��
            int returnValue = FS.FrameWork.WinForms.Classes.Function.CheckIDInfo(idNOTmp, ref errText);



            if (returnValue < 0)
            {
                MessageBox.Show(errText);
                this.txtIDNO.Focus();
                return -1;
            }
            string[] reurnString = errText.Split(',');
            if (enumType == EnumCheckIDNOType.BeforeSave)
            {
                this.dtpBirthDay.Text = reurnString[1];
                this.cmbSex.Text = reurnString[2];
                this.setAge(this.dtpBirthDay.Value);
                //this.cmbPayKind.Focus();
            }
            else
            {
                if (this.dtpBirthDay.Text != reurnString[1])
                {
                    MessageBox.Show("������������������֤�кŵ����ղ���");
                    this.dtpBirthDay.Focus();
                    return -1;
                }

                if (this.cmbSex.Text != reurnString[2])
                {
                    MessageBox.Show("������Ա������֤�кŵ��Ա𲻷�");
                    this.cmbSex.Focus();
                    return -1;
                }
            }
            return 1;
        }

        #endregion

        #region ���з���

        /// <summary>
        /// ������չ¼����Ϣ�ӿ�
        /// </summary>
        /// <param name="inputObject">��չ¼����Ϣ����</param>
        public void SetRegisterExtendInterface(FS.HISFC.BizProcess.Interface.FeeInterface.IRegisterExtend inputObject) 
        {
            this.registerExtend = inputObject;
        }

        /// <summary>
        /// ���»�����Ϣ(Ŀǰֻ�ܸ��¿���)
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        protected virtual int UpdatePatientInfomation() 
        {
            //��֤���ݺϷ���
            if (!this.IsInputValid())
            {
                return -1;
            }
            
            //���»�����ݿ�洢�û��߻�����Ϣ
            //this.patientInfomation.ID = this.txtInpatientNO.Tag.ToString();
            //���»���סԺ����ʱ������ı���סԺ��text�ؼ��ᱨ���Ӵ
            this.patientInfomation.ID = this.tempUpdatePatientID;
            this.patientInfomation = this.radtIntegrate.GetPatientInfomation(this.patientInfomation.ID);

            if (this.patientInfomation == null)
            {
                MessageBox.Show(Language.Msg("��û��߻�����Ϣʧ��!") + this.radtIntegrate.Err);

                return -1;
            }

            //������߲��ǵǼ�״̬,����������κ���Ϣ
            if (this.patientInfomation.PVisit.InState.ID.ToString() != FS.HISFC.Models.Base.EnumInState.R.ToString())
            {
                MessageBox.Show(Language.Msg("�û�����Ժ״̬�Ѿ��ı�, ���ܽ����޸�!"));

                return -1;
            }

            //���浱ǰ�û��߿�����Ϣ,����Ҫ��������¼
            FS.FrameWork.Models.NeuObject oldDept = new FS.FrameWork.Models.NeuObject();
            oldDept.ID = this.patientInfomation.PVisit.PatientLocation.Dept.ID;
            oldDept.Name = this.patientInfomation.PVisit.PatientLocation.Dept.Name;

            //����޸ĺ�û��߻�����Ϣ
            if (this.GetPatientInfomation(this.patientInfomation) == -1)
            {
                return -1;
            }

            //�����ڽ�����ȡ����סԺ��������ԭ����סԺ�Ÿ�����
            this.patientInfomation.ID = this.tempUpdatePatientID;

            //��ʼ���ݿ�����
            //Transaction t = new Transaction(this.inpatientManager.Connection);
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.radtIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            //���¿���
            if (this.radtIntegrate.UpdatePatientDept(this.patientInfomation) <= 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(Language.Msg("���»�����Ժ����ʧ��!") + this.radtIntegrate.Err);

                return -1;
            }


            //��ӱ����¼��	
            if (this.radtIntegrate.InsertShiftData(this.patientInfomation.ID, FS.HISFC.Models.Base.EnumShiftType.CD, "�޸Ŀ���", oldDept,
                this.patientInfomation.PVisit.PatientLocation.Dept) < 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(Language.Msg("��ӱ����¼ʧ��!") + this.radtIntegrate.Err);

                return -1;
            }

            //���浱ǰ�û��߿�����Ϣ,����Ҫ��������¼
            FS.FrameWork.Models.NeuObject oldNurseCell = new FS.FrameWork.Models.NeuObject();
            oldNurseCell.ID = this.patientInfomation.PVisit.PatientLocation.NurseCell.ID;
            oldNurseCell.Name = this.patientInfomation.PVisit.PatientLocation.NurseCell.Name;

            //���»��߻�ʿվ{F0BF027A-9C8A-4bb7-AA23-26A5F3539586}
            if (this.radtIntegrate.UpdatePatientNurse(this.patientInfomation) <= 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(Language.Msg("���»�����Ժ����ʧ��!") + this.radtIntegrate.Err);

                return -1;
            }

            //��ӱ�ת��������¼��	F0BF027A-9C8A-4bb7-AA23-26A5F3539586}
            if (this.radtIntegrate.InsertShiftData(this.patientInfomation.ID, FS.HISFC.Models.Base.EnumShiftType.CN, "�޸Ŀ���", oldNurseCell,
                this.patientInfomation.PVisit.PatientLocation.NurseCell) < 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(Language.Msg("��ӱ����¼ʧ��!") + this.radtIntegrate.Err);

                return -1;
            }


            //�ύ���ݿ�
            FS.FrameWork.Management.PublicTrans.Commit();

            MessageBox.Show(Language.Msg("���³ɹ�!"));

            //ˢ����ʾ�����б�
            this.RefreshPatientLists();

            //����
            this.Clear();
            this.isModify = false;
            return 1;
        }
        
        /// <summary>
        /// ���뻼�ߵǼ���Ϣ
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        protected virtual int InsertPatientInfomation() 
        {
            //�����û������סԺ��,��ô�Զ�����סԺ��
            if (this.txtInpatientNO.Text == string.Empty)// || this.patientInfomation.PID.PatientNO == null || this.patientInfomation.PID.PatientNO == string.Empty)
            {
                if (this.isAutoInpatientNO)
                {
                    //����Զ�����סԺ��ʧ��,��ô��ֹ����
                    if (this.GetAutoPatientNO()== -1)
                    {
                        return -1;
                    }
                }
                else 
                {
                    MessageBox.Show(Language.Msg("û������סԺ��!"));
                    
                    return -1;
                }
            }

            //��֤��Ч��,����в�����¼��,��ô��ֹ����
                if (!this.IsInputValid()) 
            {
                return -1;
            }

            if (this.GetPatientInfomation(this.patientInfomation) == -1)
            {
                return -1;
            }

            FS.HISFC.BizProcess.Integrate.Manager managerMgr = new FS.HISFC.BizProcess.Integrate.Manager();
            FS.HISFC.BizProcess.Integrate.RADT radtIntegrade = new FS.HISFC.BizProcess.Integrate.RADT();
            //�������ݿ�����
            //FS.FrameWork.Management.Transaction t = new Transaction(this.inpatientManager.Connection);
            //��ʼ���ݿ�����
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.radtIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.inpatientManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.feeIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            radtIntegrade.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            managerMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

           //�жϲ������������
            PatientInfo patient = new PatientInfo();
            if (this.radtIntegrate.GetInputPatientNO(this.patientInfomation.PID.PatientNO, ref patient) == -1)
            {
                //������Զ���ȡסԺ�ţ��������»�ȡ�����򣬱���
                if (this.isAutoInpatientNO)
                {
                    //���·���סԺ��
                    if (this.ReGetPatientNO() == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();

                        return -1;
                    }
                }
                else
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(radtIntegrate.Err);
                    return -1;
                }
            }
            //��ȡ�µ�סԺ��ˮ�ţ�
            this.patientInfomation.ID = this.radtIntegrate.GetNewInpatientNO();
            //����סԺ����
            if (this.InsertMainInfo() == -1) 
            {
                return -1;
            }
            if (isCreateMoneyAlert)
            {
                FS.FrameWork.Models.NeuObject conObj = managerIntegrate.GetConstant("PACTUNIT", cmbPact.Tag.ToString());
                if (conObj == null)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("��ȡ��ͬ��λʧ��" + cmbPact.Text);
                    return -1;
                }
                if (FS.FrameWork.Public.String.IsNumeric(conObj.Memo))
                {
                    patientInfomation.PVisit.MoneyAlert = FS.FrameWork.Function.NConvert.ToDecimal(conObj.Memo);
                }
                else
                {
                    patientInfomation.PVisit.MoneyAlert = 0m;
                }
                if (radtIntegrade.UpdatePatientAlert(this.patientInfomation.ID, patientInfomation.PVisit.MoneyAlert,FS.HISFC.Models.Base.EnumAlertType.M.ToString(),DateTime.MinValue,DateTime.MinValue) <= 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("���¾�����ʧ��" + radtIntegrade.Err);
                    return -1;
                }
            }
            # region ��������������̣����´���ʹ��״̬
            //��������������̣����´���ʹ��״̬
            if (this.IsContainsInstate)
            {

                //{4A0E8D9F-2FF5-4fc5-A050-8AA719E4D302}
                FS.HISFC.Models.Base.Bed bedObjTemp = this.cmbBedNO.SelectedItem as FS.HISFC.Models.Base.Bed;
                FS.HISFC.Models.Base.Bed bedObj = bedObjTemp.Clone();
                bedObj.Status.User03 = bedObjTemp.Status.ID.ToString();

                bedObj.Status.ID = FS.HISFC.Models.Base.EnumBedStatus.O;
                bedObj.InpatientNO = this.patientInfomation.ID;
                //if (managerIntegrate.SetBed(bedObj) == -1)
                if (radtIntegrate.ArrivePatientForReg(patientInfomation, bedObj) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(Language.Msg("���´�λ״̬ʧ�ܣ�"));
                    return -1;
                }
            }
            #endregion

            //���ȡ���ǷϺŸ���סԺ�ű�־
            if (this.radtIntegrate.UpdatePatientNOState(this.patientInfomation.PID.PatientNO) < 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(Language.Msg("����סԺ��״̬����") + this.radtIntegrate.Err);

                return -1;
            }

            //������չ��Ϣ
            if (this.registerExtend != null) 
            {
                if (this.registerExtend.UpdateOtherInfomation(this.patientInfomation, ref this.errText) == -1) 
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(Language.Msg(this.errText));

                    return -1;
                }
            }

            

            //���뻼�߻�����Ϣ
            //if (this.radtIntegrate.RegisterComPatient(this.patientInfomation) == -1) 
            //{
            //    FS.FrameWork.Management.PublicTrans.RollBack();
            //    MessageBox.Show(Language.Msg("���뻼�߻�����Ϣ����!") + this.radtIntegrate.Err);

            //    return -1;
            //}

            
            //���������
            FS.HISFC.BizLogic.RADT.InPatient inpatientMgr = new FS.HISFC.BizLogic.RADT.InPatient();
            inpatientMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            if (inpatientMgr.InsertPatientInfo(this.patientInfomation) < 0)
            {
                //�Ȳ�ѯ
                if (inpatientMgr.UpdatePatientInfoForInpatient(this.patientInfomation) <= 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("���뻼�߻�����Ϣ����!" + inpatientMgr.Err);
                    return -1;
                }
            }
            
            //��������Ϣ
            if (this.radtIntegrate.InsertShiftData(this.patientInfomation) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(Language.Msg("��������Ϣ����!") + this.radtIntegrate.Err);

                    return -1;
                }
            #region �������ı����Ϣ{FA3B8CE6-0414-423a-A92D-33678E5FF193}
            if (this.IsContainsInstate)
            {
                if (this.radtIntegrate.InsertRecievePatientShiftData(this.patientInfomation) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(Language.Msg("�����������Ϣ����!") + this.radtIntegrate.Err);

                    return -1;
                }
            }
            #endregion
            //���뵣����Ϣ
            if (this.radtIntegrate.InsertSurty(this.patientInfomation) == -1) 
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(Language.Msg("���뵣����Ϣ����!") + this.radtIntegrate.Err);

                return -1;
            }

            //Ԥ����ʵ��
            FS.HISFC.Models.Fee.Inpatient.Prepay prepay = new FS.HISFC.Models.Fee.Inpatient.Prepay();

            //����Ԥ����,�������Ԥ����Ʊ,���ﲻ������ӡ
            if (this.DealPreapy(prepay) == -1) 
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                return -1;
            }

            //����ԤԼ�Ǽ�״̬
            //User02��ԤԼ�Ǽ�״̬ 0ԤԼ 1���� 2ԤԼתסԺ

            if (this.PatientInfomation.User02 == "0" && this.PatientInfomation.User02!="")
            {
                string CardNo = this.patientInfomation.PID.CardNO;
                string HappenNo = this.patientInfomation.User01;
                managerIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                if (this.managerIntegrate.UpdatePreInPatientState(CardNo,"2",HappenNo) < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    return -1;
                }
                if (this.adt != null)
                {
                    if (this.hl7 == "A01")
                        adt.PreRegInpatient(this.patientInfomation.Clone());

                }
            }

            long  returnValue = 0;
            this.medcareInterfaceProxy.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            returnValue = this.medcareInterfaceProxy.SetPactCode(this.patientInfomation.Pact.ID);
            this.medcareInterfaceProxy.Trans = FS.FrameWork.Management.PublicTrans.Trans;
            if (returnValue != 1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                this.medcareInterfaceProxy.Rollback();
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("�����ӿڻ�ú�ͬ��λʧ��") + this.medcareInterfaceProxy.ErrMsg);
                return -1;
            }
            returnValue = this.medcareInterfaceProxy.Connect();
            if (returnValue != 1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                this.medcareInterfaceProxy.Rollback();
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("�����ӿڳ�ʼ��ʧ��") + this.medcareInterfaceProxy.ErrMsg);
                return -1;
            }
            returnValue = this.medcareInterfaceProxy.UploadRegInfoInpatient(this.patientInfomation);
            if (returnValue != 1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                this.medcareInterfaceProxy.Rollback();
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("�����ӿ�סԺ�Ǽ�ʧ��") + this.medcareInterfaceProxy.ErrMsg);
                return -1;
            }
         
            this.medcareInterfaceProxy.Commit();
            

          
            returnValue = this.medcareInterfaceProxy.Disconnect();

            FS.FrameWork.Management.PublicTrans.Commit();

            # region ��ӡ����
            //{F862D2BC-57DB-4868-9A4D-32A47A8B4588}
            if (this.isHealthPrint)
            {
                if (this.healthPrint != null)
                {
                    DialogResult rs = MessageBox.Show(Language.Msg("�Ƿ��ӡ����?"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (rs == DialogResult.Yes)
                    {
                        FS.HISFC.Models.HealthRecord.Base parmPatientinfo = new FS.HISFC.Models.HealthRecord.Base();
                        parmPatientinfo.PatientInfo = this.patientInfomation;

                        this.healthPrint.ControlValue(parmPatientinfo);
                        //this.healthPrint.PrintPreview();
                        this.healthPrint.Print();
                    }
                }
                else
                {
                    MessageBox.Show("�����ô�ӡ�����ӿڣ�");
                }
            }
            # endregion

            #region Ԥ�����ӡ
            if (this.patientInfomation.FT.PrepayCost > 0)
            {
                this.prepayPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.FeeInterface.IPrepayPrint)) as FS.HISFC.BizProcess.Interface.FeeInterface.IPrepayPrint;

                if (this.prepayPrint == null)
                {

                }
                else
                {
                    if (this.patientInfomation.IsEncrypt)
                    {
                        this.patientInfomation.Name = FS.FrameWork.WinForms.Classes.Function.Decrypt3DES(this.patientInfomation.NormalName);
                    }
                    this.prepayPrint.Clear();
                    this.prepayPrint.SetValue(this.patientInfomation, prepay);
                    this.prepayPrint.Print();
                }
            }
            #endregion

            #region ��ӡ������
            if (this.iPrintSureType != null && patientInfomation.Surety.SuretyCost > 0)
            {
                this.iPrintSureType.SetValue(patientInfomation);
                this.iPrintSureType.Print();
            }
            #endregion

            this.RefreshPatientLists();

            MessageBox.Show(Language.Msg("�Ǽǳɹ�!סԺ���ǣ�" + this.patientInfomation.PID.PatientNO));


            this.Clear();

            
            return 1;
        }

        /// <summary>
        /// ���
        /// </summary>
        protected virtual void Clear() 
        {
            this.txtClinicNO.SelectAll();
            this.txtInpatientNO.SelectAll();
            this.txtName.Text = string.Empty;
            this.cmbDept.Text = string.Empty;
            //{F0BF027A-9C8A-4bb7-AA23-26A5F3539586}
            this.cmbNurseCell.Text = string.Empty;
            this.cmbBedNO.Text = string.Empty;
            this.dtpInTime.Value = DateTime.Today;
            this.txtIDNO.Text = string.Empty;
            this.cmbMarry.Text = string.Empty;
            this.cmbPact.Text = string.Empty;
            this.cmbSex.Text = string.Empty;

            //--------
            this.cmbPact.Tag = "1";
            //this.cmbPact.Text = "�ֽ�";
            this.cmbInSource.Tag = "1";
            this.cmbInSource.Text = "����";
            //--------
            txtHomePhone.Text = "";
            this.cmbBirthArea.Text = string.Empty;
            this.cmbCountry.Text = string.Empty;
            this.cmbProfession.Text = string.Empty;
            this.txtLinkMan.Text = string.Empty;
            this.cmbRelation.Text = string.Empty;
            this.cmbLinkAddress.Text = string.Empty;
            this.txtHomePhone.Text = string.Empty;
            this.cmbInSource.Text = string.Empty;
            this.cmbDoctor.Text = string.Empty;
            this.mTxtPrepay.Text = "0.00";
            this.cmbInSource.SelectedIndex = 0;
            this.cmbApproach.SelectedIndex = 0;
            this.cmbCircs.SelectedIndex = 0;
            this.mTxtIntimes.Text = "1";
            this.cmbPayMode.Tag = "CA";
            this.cmbPayMode.Text = "�ֽ�";
            //{0374EA05-782E-4609-9CDC-03236AB97906}
            this.cmbTransType1.Tag = "CA";
            this.cmbTransType1.Text = "�ֽ�";

            this.cmbUnit.SelectedIndex = 0;

            this.txtBedInterval.Text = "1";
            //this.Text = "��";
            //this.cmbCase.Tag = "1";
            //����---
            this.txtMCardNO.Text = string.Empty;
            this.txtComputerNO.Text = string.Empty;
            this.cmbClinicDiagnose.Text = string.Empty;
            this.mtxtBloodFee.Text = "0.00";
            this.cmbArea.Text = string.Empty;
            this.txtMCardNO.Enabled = true;
            this.txtName.Enabled = true;
            this.txtIDNO.Enabled = true;
            this.cmbHomeAddress.Text = string.Empty;
            this.cmbWorkAddress.Text = string.Empty;
            this.txtInpatientNO.Text = string.Empty;
            this.txtClinicNO.Text = string.Empty;
           
            this.dtpInTime.Value = this.inpatientManager.GetDateTimeFromSysDateTime();
            this.dtpBirthDay.Value = this.inpatientManager.GetDateTimeFromSysDateTime();
            this.txtAge.Text = string.Empty;
            this.txtHomePhone.Text = string.Empty;
            this.txtLinkPhone.Text = string.Empty;
            this.isModify = false;
            //�����չ��Ϣ
            if (this.registerExtend != null) 
            {
                this.registerExtend.ClearOtherInfomation();
            }
            this.chbencrypt.Checked = false;
            if(this.cmbBedNO.Items.Count>0)
                this.cmbBedNO.Items.Clear();
            this.isReadCard = false;
            //{FF419F26-D52C-404b-84BF-47A509BF5782}
            this.patientInfomation = new PatientInfo();
            this.RefreshPatientLists();

            //����������Ϣ
            this.cmbNation.Text = "����";
            this.txtWorkPhone.Text = string.Empty;
            this.cmbSuretyType.Tag = string.Empty;
            this.cmbSuretyPerson.Tag = string.Empty;
            this.txtSuretyCost.Text = "0.00";
            this.txtMark.Text = string.Empty;
            //{8D5C8D10-0E22-4229-A7C5-C133E666F567}
            this.SetEnableConrol(true);
            if (this.rdoClinicNO.Checked)
            {
                this.txtClinicNO.Focus();
            }
            else
            {
                this.txtInpatientNO.Focus();
            }

            this.cmbTransType1.bank = new FS.HISFC.Models.Base.Bank();
            txtMCardNO.Enabled = true;

        }
        /// <summary>
        /// ԤԼ����
        /// </summary>
        protected virtual void PrepayPatient()
        {
            try
            {
                //�ж�סԺ��
                if (this.txtInpatientNO.Text == null || this.txtInpatientNO.Text.Trim() == "")
                {
                    MessageBox.Show("������סԺ��!", "��ʾ");
                    this.txtInpatientNO.Focus();
                    return;
                }
                //�ж�סԺ��ˮ��
                if (this.txtInpatientNO.Tag == null)
                {
                    MessageBox.Show("��س�ȷ��סԺ��", "��ʾ");
                    this.txtInpatientNO.Focus();
                    return;
                }
                ucPrepayInQuery control = new ucPrepayInQuery();
                control.PrepayinState = "0";
                DialogResult result = FS.FrameWork.WinForms.Classes.Function.PopShowControl(control);
                if (result == DialogResult.OK)
                {
                    if (control.PatientInfo != null)
                    {
                        this.PatientInfomation = control.PatientInfo;
                        this.PatientInfomation.PID.PatientNO = this.txtInpatientNO.Text;
                        this.PatientInfomation.ID = this.txtInpatientNO.Tag.ToString() ;
                        PatientInfomation.PVisit.InTime = this.inpatientManager.GetDateTimeFromSysDateTime();
                        this.SetPatientInfomation(PatientInfomation);
                        this.txtName.Focus();

                        //{E9EC275C-F044-40f1-BDDA-0F17410983EB}
                        if (!isModifyPreRegInfo)
                        {
                            if (!string.IsNullOrEmpty(this.cmbBedNO.Text))
                            {
                                this.cmbBedNO.Enabled = false;
                            }

                            if (!string.IsNullOrEmpty(this.cmbDept.Text))
                            {
                                this.cmbDept.Enabled = false;
                            }

                            if (!string.IsNullOrEmpty(this.cmbClinicDiagnose.Text))
                            {
                                this.cmbClinicDiagnose.Enabled = false;
                            }

                            if (!string.IsNullOrEmpty(this.cmbDoctor.Text))
                            {
                                this.cmbDoctor.Enabled = false;
                            }

                            if (!string.IsNullOrEmpty(this.cmbNurseCell.Text))
                            {
                                this.cmbNurseCell.Enabled = false;
                            }                         
                        }
                    }
                }
            }
            catch { }
            
        }

        /// <summary>
        /// ��ѯ����
        /// </summary>
        protected virtual void SearchPatient()
        {
            FS.HISFC.Components.Common.Forms.frmSearchPatient frm = new FS.HISFC.Components.Common.Forms.frmSearchPatient();
            frm.SaveInfo+=new FS.HISFC.Components.Common.Forms.frmSearchPatient.SaveHandel(frm_SaveInfo);
            frm.Show();
        }
        private void frm_SaveInfo(PatientInfo patient)
        {
            patient.PVisit.InTime = this.inpatientManager.GetDateTimeFromSysDateTime();
            this.SetPatientInfomation(patient);
        }
        #endregion

        #region �ؼ�����

        /// <summary>
        /// ����ToolBar�ؼ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("ȷ�ϱ���", "����¼�����Ϣ", (int)FS.FrameWork.WinForms.Classes.EnumImageList.B����, true, false, null);
            toolBarService.AddToolButton("����", "���¼�����Ϣ", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q���, true, false, null);
            toolBarService.AddToolButton("��ʱ��", "������ʱסԺ��", (int)FS.FrameWork.WinForms.Classes.EnumImageList.L��ʱ��, true, false, null);
            toolBarService.AddToolButton("ԤԼ����", "��ԤԼ���߽���", (int)FS.FrameWork.WinForms.Classes.EnumImageList.YԤԼ, true, false, null);
            toolBarService.AddToolButton("���߲�ѯ", "�򿪻��߲�ѯ����", (int)FS.FrameWork.WinForms.Classes.EnumImageList.C��ѯ, true, false, null);
            toolBarService.AddToolButton("����", "�򿪰����ļ�", (int)FS.FrameWork.WinForms.Classes.EnumImageList.B����, true, false, null);

            return this.toolBarService;
        }

        /// <summary>
        /// �Զ��尴ť���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            //string tempText = string.Empty;
            
            //try
            //{
            //    tempText = this.hsToolBar[e.ClickedItem.Text].ToString();
            //}
            //catch (Exception ex) 
            //{
            //    return;
            //}

            ButtonClicked(e.ClickedItem.Text);
            
            base.ToolStrip_ItemClicked(sender, e);
        }
        /// <summary>
        /// ��Ӧ���̡�����¼�
        /// </summary>
        /// <param name="tempText">��������ť����</param>
        private void ButtonClicked(string tempText)
        {
            switch (tempText)
            {
                case "ȷ�ϱ���":
                    this.hl7 = "A01";
                    if (this.isModify == true)
                    {
                        this.UpdatePatientInfomation();
                    }
                    else
                    {
                        this.InsertPatientInfomation();
                    }
                    //this.InsertPatientInfomation();
                    break;
                case "����":
                    this.Clear();
                    break;
                case "��ʱ��":
                    if (this.isAutoInpatientNO)
                    {  //��ʱ����ʱȡ��aaaaaaaaa
                        //this.txtInpatientNO.Text = string.Empty;

                        //this.GetAutoInpatientNO(string.Empty);

                        //this.txtMCardNO.Focus();
                    }
                    break;
                case "ԤԼ����":
                    {
                        PrepayPatient();
                        break;
                    }
                case "���߲�ѯ":
                    {
                        SearchPatient();
                        break;
                    }
                case "�˳�":
                    {
                        this.FindForm().Close();
                        break;
                    }
                case "����":
                    {
                        break;
                    }
            }
        }

        /// <summary>
        /// �����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnSave(object sender, object neuObject)
        {
            //this.hl7 = "A01";

            if (this.isModify == true)
            {
                return this.UpdatePatientInfomation();
            }
            else
            {
                return this.InsertPatientInfomation();
            }
        }

        #endregion 

        #region �¼�
        /// <summary>
        /// ��ʼ���Ǽ���Ϣ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucRegister_Load(object sender, EventArgs e)
        {
            //if (!this.DesignMode)
            //{E4002949-0D84-4eac-BE03-B2196A1AEC0D}
            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToUpper() != "DEVENV")
            {
                this.neuTabControl1.Focus();
                this.tabPage1.Focus();
                this.plInfomation.Focus();
                this.txtInpatientNO.Focus();
                this.rdoInpatientNO.Checked = true;

                this.�Ǽǿ��ұ������� = true;
                this.�����Ա�������� = true;
                this.���������������� = true;
                this.�������ڱ������� = true;
                this.���㷽ʽ�������� = true;
                this.��סҽʦ�������� = mTxtPrepayMustInput;
                this.Ԥ������������ = true;
                this.סԺ�ű������� = true;
                this.סԺ���ڱ������� = true;
                //[2011-5-24] zhaozf
                this.�Ǽǲ����������� = true;
                
                //this.cmbPact.Text = "�ֽ�";
                this.lblInSource.ForeColor = this.mustInputColor;
                this.lblPact.ForeColor = this.mustInputColor;


                //���³�ʼ��������
                //try
                //{
                //    Function.RefreshToolBar(this.hsToolBar, ((FS.FrameWork.WinForms.Forms.frmBaseForm)this.ParentForm).toolBar1, "סԺ�Ǽ�");
                //}
                //catch (Exception ex)
                //{ }


                //�������뷨
                try
                {
                    this.SetInputMenu( );
                    //���ô���ؼ������뷨״̬Ϊ���
                    FS.HISFC.Components.Common.Classes.Function.SetIme(this);
              
                }
                catch 
                {
                }

                this.Init();

                /*
                 *�޸ı�ʶ��2AF24B84-500B-4D24-9890-2CD83D10F64F
                 *�޸��ˣ�songrabit
                 *�޸�ԭ�򣺰����¼�ѡ����Һ�  ���س� ���㲻����  ��Ϊ ��λ����򲻿�д
                 *�޸ķ���������add -endadd����                 
                 */

                //add
                this.Clear();
                //end add

                this.txtInpatientNO.Focus();

            }
        }

        /// <summary>
        /// �Զ�����סԺ���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAutoInpatientNO_Click(object sender, EventArgs e)
        {
            this.txtInpatientNO.Text = string.Empty;

          //  this.GetAutoInpatientNO(string.Empty);
            this.GetAutoPatientNO();

            this.txtMCardNO.Focus();

        }

        /// <summary>
        /// �Ѿ��Ǽǻ�����ʾ��Ϣ�б��˫���¼�
        /// ��Ҫ�ж��Ƿ�����޸Ļ�����Ϣ,
        /// ��������Ǹ���Ժ״̬,������޸�,����״̬�������޸�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void spPatientInfo_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.spPatientInfo_Sheet1.RowCount <= 0)
            {
                return;
            }

            if (this.spPatientInfo_Sheet1.Rows[e.Row].Tag == null)
            {
                return;
            }
            FS.HISFC.Models.RADT.PatientInfo pInfo = new FS.HISFC.Models.RADT.PatientInfo();
            pInfo = (FS.HISFC.Models.RADT.PatientInfo)this.spPatientInfo.Sheets[0].Rows[e.Row].Tag;
            if (pInfo.PVisit.InState.ID.ToString() != FS.HISFC.Models.Base.EnumInState.R.ToString())
            {
                MessageBox.Show(Language.Msg("�û��߲���סԺ�Ǽ�״̬, ���ܽ����޸�!"));
                this.Clear();
                return;
            }
            this.SetPatientInfomation(pInfo);
            this.tempUpdatePatientID = pInfo.ID;
            this.isModify = true;
            //{8D5C8D10-0E22-4229-A7C5-C133E666F567}
            this.cmbDept.Focus();
            this.SetEnableConrol(false);
        }

        /// <summary>
        /// �����뻼�ߵ����䷢���仯ʱ���Զ���������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtpBirthDay_ValueChanged(object sender, EventArgs e)
        {
            //DateTime nowTime = this.inpatientManager.GetDateTimeFromSysDateTime();



            //if (this.dtpBirthDay.Value > nowTime)
            //{

            //    //MessageBox.Show(Language.Msg("���ߵ����ղ��ܴ��ڵ�ǰϵͳʱ��!"));

            //    this.dtpBirthDay.Value = nowTime;

            //    return;
            //}

            //this.txtAge.Text = this.inpatientManager.GetAge(this.dtpBirthDay.Value);

            DateTime current = this.inpatientManager.GetDateTimeFromSysDateTime().Date;

            if (this.dtpBirthDay.Value.Date > current)
            {
                MessageBox.Show("�������ڲ��ܴ��ڵ�ǰʱ��!", "��ʾ");
                this.dtpBirthDay.Focus();
                return;
            }

            //��������
            if (this.dtpBirthDay.Value.Date != current)
            {
                this.setAge(this.dtpBirthDay.Value);
            }

            this.cmbWorkAddress.Focus();
        }


        /// <summary>
        /// ���֤ {9B24289B-D017-4356-8A25-B0F76EB79D15}
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtIDNO_Leave(object sender, System.EventArgs e)
        {
            if (this.txtIDNO.Text != "")
            {
                //string errMessage = string.Empty;
                //if (FS.FrameWork.WinForms.Classes.Function.CheckIDInfo(this.txtIDNO.Text, ref errMessage) != 0)
                //{
                //    MessageBox.Show(FS.FrameWork.Management.Language.Msg(errMessage));
                //}

               int returnValue = this.ProcessIDENNO(this.txtIDNO.Text.Trim(), EnumCheckIDNOType.BeforeSave);

               if (returnValue < 0)
               {
                   return;
               }
            }

            

            //System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^\d{15,18}$");
            //System.Text.RegularExpressions.Match match = regex.Match(this.txtIDNO.Text);
            //if ( !match.Success )
            //{
            //    MessageBox.Show(FS.FrameWork.Management.Language.Msg("���֤��Ϊ 15 �� 18 λ����"));
            //    this.txtIDNO.Focus();
            //}
        }

        #endregion

        private void cmbDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            //{1BFFE533-C177-46bf-93C6-093DD7344DAF} wbo 2010-08-19
            this.cmbNurseCell.Tag = null;
            
            FS.FrameWork.Models.NeuObject deptObj = this.cmbDept.SelectedItem;

            int returnValue = GetNurseCellByDept(deptObj);


            //ArrayList alBed = new ArrayList();

            //if (deptObj == null) return;
            
            //alBed = this.GetBedByDeptCode(deptObj);
            //if (alBed == null)
            //{
            //    MessageBox.Show("���Ҵ�λʧ�ܣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}
            //if (alBed.Count == 0)
            //{
            //    MessageBox.Show("���ң�"+deptObj.Name + "Ŀǰû�д�λ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}
            
            ////�������������
            //if (IsContainsInstate)
            //{
            //    cmbBedNO.Items.Clear();
            //    this.cmbBedNO.AddItems(alBed);
            //    if (this.cmbBedNO.Items.Count > 0)
            //    {
            //        this.cmbBedNO.SelectedIndex = 0;
            //    }
            //}

            

        }

        protected ArrayList GetBedByDeptCode(FS.FrameWork.Models.NeuObject deptObj)
        {
            ArrayList alNurseCell = managerIntegrate.QueryNurseStationByDept(deptObj);
            ArrayList alBed = new ArrayList();
            try
            {
                foreach (FS.FrameWork.Models.NeuObject obj in alNurseCell)
                {
                    ArrayList temp = managerIntegrate.QueryUnoccupiedBed(obj.ID);
                    if (temp != null && temp.Count > 0)
                        alBed.AddRange(temp);
                }
                return alBed;
            }
            catch { return null;}
        }

        /// <summary>
        /// ���ݿ��Ҳ�ѯ����{F0BF027A-9C8A-4bb7-AA23-26A5F3539586}
        /// </summary>
        /// <param name="deptObj"></param>
        /// <returns></returns>
        protected int GetNurseCellByDept(FS.FrameWork.Models.NeuObject deptObj)
        {
            ArrayList alNurseCell = managerIntegrate.QueryNurseStationByDept(deptObj);
            if (alNurseCell == null)
            {
                MessageBox.Show("���ݿ��Ҳ�ѯ��������" + managerIntegrate.Err);
                return -1;
            }
            else if (alNurseCell.Count == 0)
            {
                alNurseCell = managerIntegrate.QueryDepartment(deptObj.ID);
            }

            FS.FrameWork.Models.NeuObject nurseObj = new FS.FrameWork.Models.NeuObject();
          
            //[2011-5-24] zhaozf
            if (alNurseCell.Count>0)
            {
                nurseObj = alNurseCell[0] as FS.FrameWork.Models.NeuObject;
            }

            this.cmbNurseCell.Tag = nurseObj.ID;
            return 1;
        }


        #region ISIReadCard ��Ա

        public int ReadCard(string pactCode)
        {
            //{FF419F26-D52C-404b-84BF-47A509BF5782} ����ǰ�����һ��
            this.Clear();

            long returnValue = 0;
            returnValue =  this.medcareInterfaceProxy.SetPactCode(pactCode);
            if (returnValue != 1)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("��ú�ͬ��λʧ��")+this.medcareInterfaceProxy.ErrMsg);
                return -1;
            }
            returnValue = this.medcareInterfaceProxy.Connect();
            if (returnValue != 1)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("�����ӿڳ�ʼ��ʧ��") + this.medcareInterfaceProxy.ErrMsg);
                return -1;
            }
            returnValue = this.medcareInterfaceProxy.GetRegInfoInpatient(this.patientInfomation);
            if (returnValue != 1)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("�����ӿڻ�û�����Ϣʧ��") + this.medcareInterfaceProxy.ErrMsg);
                return -1;
            }

            //returnValue = this.medcareInterfaceProxy.UploadRegInfoInpatient(this.patientInfomation);
            //this.medcareInterfaceProxy.Commit();

            //this.patientInfomation.SIMainInfo.TransType = 0;
            //this.patientInfomation.SIMainInfo.ReimbFlag = "";
            this.medcareInterfaceProxy.Disconnect();
            this.isReadCard = true;


            this.SetSIPatientInfo();
            return 1;

        }

        public int SetSIPatientInfo()
        {
            ArrayList alPatientInfo = new ArrayList();
            this.txtMCardNO.Text = this.patientInfomation.SSN;
            this.cmbPact.Tag = this.patientInfomation.Pact.ID;
            this.cmbSex.Tag = this.patientInfomation.Sex.ID;
            this.cmbNation.Tag = this.patientInfomation.Nationality.ID;
          
            FS.HISFC.BizProcess.Integrate.RADT radt = new FS.HISFC.BizProcess.Integrate.RADT();
            FS.HISFC.Models.RADT.PatientInfo p = null;

            alPatientInfo = radt.PatientQueryByMcardNO(this.patientInfomation.SSN);
            if (alPatientInfo.Count != 0 && alPatientInfo!= null)
            {
                p = (FS.HISFC.Models.RADT.PatientInfo)alPatientInfo[0];
                this.patientInfomation.PID.CardNO = p.PID.CardNO;
                this.patientInfomation.PhoneHome = p.PhoneHome;
                this.patientInfomation.AddressHome = p.AddressHome;
                this.patientInfomation.PID.PatientNO = p.PID.PatientNO;
                FS.HISFC.Models.RADT.PatientInfo tempPatientInfo = new PatientInfo();
                //this.radtIntegrate.CreateAutoInpatientNO(tempPatientInfo);
                this.patientInfomation.ID = tempPatientInfo.ID;
                this.patientInfomation.Birthday = p.Birthday;
            }

            this.SetPatientInfomation(this.patientInfomation);


            return 1 ;
        }

        #endregion

        #region ��ݼ�

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="keyData">��ǰ����</param>
        /// <returns>����ִ��True False ��ǰ�������</returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                if (DealControlEnterEvents() == -1)
                {
                    return true;
                }

                SendKeys.Send("{TAB}"); 
                return true;
            }
            if (keyData == Keys.Space)
            {
                if (txtClinicNO.Focused)
                {
                    PatientInfo tempPatient = new PatientInfo();
                    int resultValue =FS.HISFC.Components.Common.Classes.Function.QueryComPatientInfo(ref tempPatient);
                    if (resultValue == 1)
                    {
                        this.txtClinicNO.Text = tempPatient.PID.CardNO;
                        DealControlEnterEvents();
                        this.txtInpatientNO.Focus();
                        return true;
                    }
                }
            }
            ////�ж�ִ�п�ݼ�
            //this.ExecuteShotCut(keyData);

            return base.ProcessDialogKey(keyData);
        }



        #endregion

        #region ���뷨����

        /// <summary>
        /// Ĭ�ϵ��������뷨
        /// </summary>
        private InputLanguage CHInput = null;

        /// <summary>
        /// ��ʼ�����뷨�˵�
        /// </summary>
        private void SetInputMenu(  )
        {
   
            for (int i = 0; i < InputLanguage.InstalledInputLanguages.Count; i++)
            {
                InputLanguage t = InputLanguage.InstalledInputLanguages[i];
                System.Windows.Forms.ToolStripMenuItem m = new ToolStripMenuItem( );
                m.Text = t.LayoutName;
                m.Click += new EventHandler(m_Click);
                neuContextMenuStrip1.Items.Add(m);
            }

            this.ReadInputLanguage( );
        }

        /// <summary>
        /// ��ȡ��ǰĬ�����뷨
        /// </summary>
        private  void ReadInputLanguage( )
        {
            if (!System.IO.File.Exists(FS.FrameWork.WinForms.Classes.Function.SettingPath + "/feeSetting.xml"))
            {
                FS.HISFC.Components.Common.Classes.Function.CreateFeeSetting( );

            }
            try
            {
                XmlDocument doc = new XmlDocument( );
                doc.Load(FS.FrameWork.WinForms.Classes.Function.SettingPath + "/feeSetting.xml");
                XmlNode node = doc.SelectSingleNode("//IME");

                CHInput = GetInputLanguage(node.Attributes["currentmodel"].Value);

                if (CHInput != null)
                {
                    foreach (ToolStripMenuItem m in neuContextMenuStrip1.Items)
                    {
                        if (m.Text == CHInput.LayoutName)
                        {
                            m.Checked = true;
                        }
                    }
                }

                //��ӵ�������

            }
            catch (Exception e)
            {
                MessageBox.Show("��ȡĬ���������뷨����!" + e.Message);
                return;
            }
        }

        /// <summary>
        /// �������뷨���ƻ�ȡ���뷨
        /// </summary>
        /// <param name="LanName"></param>
        /// <returns></returns>
        private InputLanguage GetInputLanguage( string LanName )
        {
            foreach (InputLanguage input in InputLanguage.InstalledInputLanguages)
            {
                if (input.LayoutName == LanName)
                {
                    return input;
                }
            }
            return null;
        }

        /// <summary>
        /// ���浱ǰ���뷨
        /// </summary>
        private void SaveInputLanguage( )
        {
            if (!System.IO.File.Exists(FS.FrameWork.WinForms.Classes.Function.SettingPath + "/feeSetting.xml"))
            {
                FS.HISFC.Components.Common.Classes.Function.CreateFeeSetting( );
            }
            if (CHInput == null)
                return;

            try
            {
                XmlDocument doc = new XmlDocument( );
                doc.Load(FS.FrameWork.WinForms.Classes.Function.SettingPath + "/feeSetting.xml");
                XmlNode node = doc.SelectSingleNode("//IME");

                node.Attributes["currentmodel"].Value = CHInput.LayoutName;

                doc.Save(FS.FrameWork.WinForms.Classes.Function.SettingPath + "/feeSetting.xml");
            }
            catch (Exception e)
            {
                MessageBox.Show("����Ĭ���������뷨����!" + e.Message);
                return;
            }
        }

        private void m_Click( object sender, EventArgs e )
        {
            foreach (ToolStripMenuItem m in this.neuContextMenuStrip1.Items)
            {
                if (sender == m)
                {
                    m.Checked = true;
                    this.CHInput = this.GetInputLanguage(m.Text);
                    //�������뷨
                    this.SaveInputLanguage( );
                }
                else
                {
                    m.Checked = false;
                }
            }
        }

        #endregion

        #region ////{538F0253-AB89-4ce3-8C2A-7E8F5C6EDBF5}�����������ջ���
        private void txtAge_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DateTime current = this.inpatientManager.GetDateTimeFromSysDateTime().Date;

                if (this.dtpBirthDay.Value.Date > current)
                {
                    MessageBox.Show("�������ڲ��ܴ��ڵ�ǰʱ��!", "��ʾ");
                    this.dtpBirthDay.Focus();

                    return;
                }

                //��������
                if (this.dtpBirthDay.Value.Date != current)
                {
                    this.setAge(this.dtpBirthDay.Value);
                }

                this.cmbWorkAddress.Focus();
            }
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
            int year=0, month=0, day=0;

            current = this.inpatientManager.GetDateTimeFromSysDateTime();
            //year = current.Year - birthday.Year;
            //month = current.Month - birthday.Month;
            //day = current.Day - birthday.Day;
            this.inpatientManager.GetAge(birthday, current, ref year, ref month, ref day);
            if (year > 1)
            {
                this.txtAge.Text = year.ToString();
                this.cmbUnit.SelectedIndex = 0;
            }
            else if (year == 1)
            {
                if (month >= 0)//һ��
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

                this.cmbUnit.SelectedIndexChanged -= this.cmbUnit_SelectedIndexChanged;
                this.cmbUnit.SelectedIndex = 1;
                this.cmbUnit.SelectedIndexChanged += this.cmbUnit_SelectedIndexChanged;
            }
            else if (day > 0)
            {
                this.txtAge.Text = day.ToString();
                this.cmbUnit.SelectedIndexChanged -= this.cmbUnit_SelectedIndexChanged;
                this.cmbUnit.SelectedIndex = 2;
                this.cmbUnit.SelectedIndexChanged += this.cmbUnit_SelectedIndexChanged;
            }
            else if (day == 0)
            {
                this.txtAge.Text = "1";
                this.cmbUnit.SelectedIndexChanged -= this.cmbUnit_SelectedIndexChanged;
                this.cmbUnit.SelectedIndex = 2;
                this.cmbUnit.SelectedIndexChanged += this.cmbUnit_SelectedIndexChanged;
            }

        }

        /// <summary>
        /// ��ȡ��������
        /// </summary>
        private void getBirthday()
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
                MessageBox.Show("�������䲻��ȷ,����������!", "��ʾ");
                this.txtAge.Focus();
                return;
            }

            ///
            ///

            DateTime birthday = DateTime.MinValue;

            this.getBirthday(i, this.cmbUnit.Text, ref birthday);

            if (birthday < this.dtpBirthDay.MinDate)
            {
                MessageBox.Show("���䲻�ܹ���!", "��ʾ");
                this.txtAge.Focus();
                return;
            }

            //this.dtBirthday.Value = birthday ;

            if (this.cmbUnit.Text == "��")
            {

                //���ݿ��д���ǳ�������,������䵥λ����,��������ĳ������ں����ݿ��г������������ͬ
                //�Ͳ��������¸�ֵ,��Ϊ����ĳ�����������Ϊ����,���������ݿ���Ϊ׼

                if (this.dtpBirthDay.Value.Year != birthday.Year)
                {
                    this.dtpBirthDay.Value = birthday;
                }
            }
            else
            {
                this.dtpBirthDay.Value = birthday;
            }
        }

        /// <summary>
        /// ��������õ���������
        /// </summary>
        /// <param name="age"></param>
        /// <param name="ageUnit"></param>
        /// <param name="birthday"></param>
        private void getBirthday(int age, string ageUnit, ref DateTime birthday)
        {
            DateTime current = this.inpatientManager.GetDateTimeFromSysDateTime();

            ageUnit = ageUnit.Trim();
            if (ageUnit == "��")
            {
                birthday = current.AddYears(-age);
            }
            else if (ageUnit == "��")
            {
                birthday = current.AddMonths(-age);
            }
            else if (ageUnit == "��")
            {
                birthday = current.AddDays(-age);
            }
            else if (ageUnit == "ʱ")
            {
                birthday = current.AddHours(-age);
            }
            else if (ageUnit == "��")
            {
                birthday = current.AddMinutes(-age);
            }
        }

        private void cmbUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.getBirthday();
        }

        private void cmbUnit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.cmbWorkAddress.Focus();
            }
          
        }

        private void dtpBirthDay_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DateTime current = this.inpatientManager.GetDateTimeFromSysDateTime().Date;

                if (this.dtpBirthDay.Value.Date > current)
                {
                    MessageBox.Show("�������ڲ��ܴ��ڵ�ǰʱ��!", "��ʾ");
                    this.dtpBirthDay.Focus();
                    return;
                }

                //��������
                if (this.dtpBirthDay.Value.Date != current)
                {
                    this.setAge(this.dtpBirthDay.Value);
                }

                this.cmbWorkAddress.Focus();
            }
        }
        ////{538F0253-AB89-4ce3-8C2A-7E8F5C6EDBF5}�����������ջ��� ����
        #endregion

        #region ���֤У��ö��
        /// <summary>
        /// �ж����֤{9B24289B-D017-4356-8A25-B0F76EB79D15}
        /// </summary>
        private enum EnumCheckIDNOType
        {
            /// <summary>
            /// ����֮ǰУ��
            /// </summary>
            BeforeSave = 0,

            /// <summary>
            /// ����ʱУ��
            /// </summary>
            Saveing
        }
        #endregion

        /// <summary>//{8D5C8D10-0E22-4229-A7C5-C133E666F567}
        /// ���ÿռ䲻����
        /// </summary>
        /// <param name="isEnable"></param>
        private void SetEnableConrol(bool isEnable)
        {
            foreach (Control var in this.plInfomation.Controls)
            {
                if (var.GetType().ToString() != "FS.FrameWork.WinForms.Controls.NeuLabel" && var.Name != "cmbDept"&&var.Name != "cmbNurseCell")
                {
                    var.Enabled = isEnable;
                }
            }

            this.btnAutoInpatientNO.Enabled = this.isAutoInpatientNO;
        }
        
        //{F0BF027A-9C8A-4bb7-AA23-26A5F3539586}
        private void cmbNurseCell_SelectedIndexChanged(object sender, EventArgs e)
        {
            FS.FrameWork.Models.NeuObject nurseObj = this.cmbNurseCell.SelectedItem;
            //ArrayList alBed = new ArrayList();

            if (nurseObj == null) return;

            ArrayList alBed = managerIntegrate.QueryUnoccupiedBed(nurseObj.ID);

            // alBed = this.GetBedByDeptCode(deptObj);
            if (alBed == null)
            {
                MessageBox.Show("���Ҵ�λʧ�ܣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (alBed.Count == 0)
            {
                MessageBox.Show("������" + nurseObj.Name + "Ŀǰû�д�λ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //�������������
            if (IsContainsInstate)
            {
                cmbBedNO.Items.Clear();
                this.cmbBedNO.AddItems(alBed);
                if (this.cmbBedNO.Items.Count > 0)
                {
                    this.cmbBedNO.SelectedIndex = 0;
                }
            }


        }

        //{4B86EBA7-5616-4787-A9F2-CF7EEAA85D92}
        /// <summary>
        /// ��ȡ���ѻ�����Ϣ
        /// </summary>
        /// <returns></returns>
        private int GetGFPatientInfo()
        {
            string mCardNO = this.txtMCardNO.Text.Trim();
            if (string.IsNullOrEmpty(mCardNO))
            {
                return -1;
            }
            string pactCode = this.cmbPact.Tag.ToString();
            if (queryGFPatient == null)
            {
                queryGFPatient = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.IQueryGFPatient)) as FS.HISFC.BizProcess.Interface.IQueryGFPatient;
            }
            if (queryGFPatient == null)
            {
                return -1;
            }
            PatientInfo p = new PatientInfo();
            string errText = string.Empty;
            int resultValue = queryGFPatient.QueryGFPatient(p, ref errText, pactCode, mCardNO);
            if (resultValue <= 0)
            {
                MessageBox.Show(errText);
                return -1;
            }
            this.txtName.Text = p.Name;
            this.cmbSex.Text = p.Sex.Name;
            this.txtIDNO.Text = p.IDCard;
            this.cmbWorkAddress.Text = p.CompanyName;
            this.txtHomePhone.Text = p.PhoneHome;
            this.txtInpatientNO.Focus();
            return 1;
        }
    } 
}