using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Function;

namespace FS.HISFC.Components.HealthRecord.CaseFirstPage
{
    public partial class ucCaseMainInfo : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        /// <summary>
        /// ucCaseMainInfo<br></br>
        /// [��������: ����������Ϣ¼��]<br></br>
        /// [�� �� ��: �ſ���]<br></br>
        /// [����ʱ��: 2007-04-20]<br></br>
        /// <�޸ļ�¼ 
        ///		�޸���='���' 
        ///		�޸�ʱ��='2009-09-16' 
        ///		�޸�Ŀ��='��Ϊ���¼��Ļ�����Ϣ��ȡҳ�����
        ///     {E9F858A6-BDBC-4052-BA57-68755055FB80}
        ///              '
        ///		�޸�����='
        ///         �������ԣ���ʶ�����μ��صĴ����Ƿ�Ϊ���¼�봰��
        ///             '
        ///  />
        /// </summary>
        public ucCaseMainInfo()
        {
            InitializeComponent();
            #region {E9F858A6-BDBC-4052-BA57-68755055FB80}
            //�������ռ����ʱ����ǰ�ڵ�ǰ�����Load�¼�

            #region  ����
            this.ucFeeInfo1.InitInfo();
            #endregion

            #region  ��Ӥ
            ucBabyCardInput1.InitInfo();
            #endregion

            #region ����
            this.ucOperation1.InitInfo();
            ucOperation1.InitICDList();
            //thread = new System.Threading.Thread(ucOperation1.InitICDList);
            //thread.Start();
            #endregion

            #region ����
            //thread = new System.Threading.Thread(this.ucTumourCard2.InitInfo);
            //thread.Start();
            this.ucTumourCard2.InitInfo();
            #endregion

            #region  ת��
            //thread = new System.Threading.Thread(this.ucChangeDept1.InitInfo);
            //thread.Start(); 
            this.ucChangeDept1.InitInfo();
            #endregion

            #region  �����Ϣ
            //thread = new System.Threading.Thread(this.ucDiagNoseInput1.InitInfo);
            //thread.Start();  
            this.ucDiagNoseInput1.InitInfo();
            #endregion

            #endregion

        }

        #region {E9F858A6-BDBC-4052-BA57-68755055FB80}

        #region ����

        private bool isVisitInput=false;

        /// <summary>
        /// �Ƿ�Ϊ������봰��
        /// </summary>
        public bool IsVisitInput
        {
            get { return isVisitInput; }
            set { isVisitInput = value; }
        }

        /// <summary>
        /// �Ƿ�����  true ���� false ������ chengym
        /// �÷������� ֱ��¼�����������  �����ٱ༭ icd10  ������ ֱ��¼�� ICD10
        /// </summary>
        [Category("��������"), Description("��ϱ༭����,True������������������� False:ֱ��¼��ICD10")]
        public bool IsDoubt
        {
            get { return this.ucDiagNoseInput1.IsDoubt; }
            set { this.ucDiagNoseInput1.IsDoubt = value; }
        }

        // =======================����������������:��������в����ڸû���,��ʹ�ô��崫����ֵ����Ϊ��ѯ������Ϣ������
        private string patientNo;
        /// <summary>
        /// סԺ��
        /// </summary>
        public string PatientNo
        {
            get { return patientNo; }
            set { patientNo = value; }
        }

        private string cardNo;
        /// <summary>
        /// ������
        /// </summary>
        public string CardNo
        {
            get { return cardNo; }
            set { cardNo = value; }
        }
        //============================
        /// <summary>
        /// �Ƿ��д��ҳ��Ϣ������
        /// </summary>
        private bool isUpdataFinIprInmaininfo = true;
        /// <summary>
        /// �Ƿ��д��ҳ��Ϣ������
        /// </summary>
        [Category("�Ƿ��д��ҳ��Ϣ������"), Description("��д�������,True����д False:����д")]
        public bool IsUpdataFinIprInmaininfo
        {
            get { return this.isUpdataFinIprInmaininfo; }
            set { this.isUpdataFinIprInmaininfo = value; }
        }
        #endregion

        #endregion


        #region  ȫ�ֱ���
        //��־ ��ʶ��ҽ��վ�û��ǲ�������
        private FS.HISFC.Models.HealthRecord.EnumServer.frmTypes frmType = FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC;
        //�ݴ浱ǰ�޸��˵Ĳ���������Ϣ
        private FS.HISFC.Models.HealthRecord.Base CaseBase = new FS.HISFC.Models.HealthRecord.Base();
        //����������Ϣ������
        private FS.HISFC.BizLogic.HealthRecord.Base baseDml = new FS.HISFC.BizLogic.HealthRecord.Base();

        #region {E9F858A6-BDBC-4052-BA57-68755055FB80}

        //��û�����Ϣ������
        FS.HISFC.BizLogic.HealthRecord.Visit.LinkWay linkWayManager
            = new FS.HISFC.BizLogic.HealthRecord.Visit.LinkWay();

        /// <summary>
        /// ���ҵ�������

        /// </summary>
        FS.HISFC.BizProcess.Integrate.HealthRecord.Visit.Visit visitIntergrate
            = new FS.HISFC.BizProcess.Integrate.HealthRecord.Visit.Visit();

        /// <summary>
        /// �����ϸҵ����
        /// </summary>
        FS.HISFC.BizLogic.HealthRecord.Visit.VisitRecord visitRecordManager
            = new FS.HISFC.BizLogic.HealthRecord.Visit.VisitRecord();


        //�绰״̬�б�
        FarPoint.Win.Spread.CellType.ComboBoxCellType telStateBox;

        //�뻼�߹�ϵ
        FarPoint.Win.Spread.CellType.ComboBoxCellType relationBox;

        //add chengym ��ʱסԺ��ˮ�ţ�LoadInfo�������ж��Ƿ�ͬһ�����ߣ���ͬ�������»�ȡ����
        private string TempInpatient = string.Empty;
        private bool isNeedLoadInfo = false;
        private static ArrayList icdList = new ArrayList();

        private string in_State = string.Empty;//��¼����Ŀǰ״̬ loadinfoʱ������ֵ
        private DateTime dt_out = System.DateTime.Now.AddYears(-1);//��Ժ����
        private string bedNo = string.Empty;//��Ժ��λ
        private string dept_out = string.Empty;//סԺ�����Ժ���ұ���

        //end add chengym
        #endregion


        //�������
        FS.HISFC.BizLogic.Manager.Constant con = new FS.HISFC.BizLogic.Manager.Constant();
        //������� 
        private FS.HISFC.Models.HealthRecord.Diagnose clinicDiag = null;
        //��Ժ��� 
        private FS.HISFC.Models.HealthRecord.Diagnose InDiag = null;
        //��ͬ��λ
        private FS.HISFC.BizLogic.Fee.PactUnitInfo pactManager = new FS.HISFC.BizLogic.Fee.PactUnitInfo();
        //ת����Ϣ
        ArrayList changeDept = new ArrayList();
        //��һ��ת��
        private FS.HISFC.Models.RADT.Location firDept = null;
        //�ڶ���ת����Ϣ
        private FS.HISFC.Models.RADT.Location secDept = null;
        //������ת����Ϣ
        private FS.HISFC.Models.RADT.Location thirDept = null;
        FS.HISFC.BizLogic.HealthRecord.DeptShift deptChange = new FS.HISFC.BizLogic.HealthRecord.DeptShift();
        FS.HISFC.BizLogic.HealthRecord.Fee healthRecordFee = new FS.HISFC.BizLogic.HealthRecord.Fee();
        HealthRecord.Search.ucPatientList ucPatient = new HealthRecord.Search.ucPatientList();
        //��ʶ�ֹ������״̬�ǲ��뻹�Ǹ���  0Ĭ��״̬  1  ���� 2����  
        private int HandCraft = 0;

        //		//��Ժ��ϵı�־λ  0 Ĭ�ϣ� 1 �޸� ��2 ���룬 3 ɾ�� 
        //		public int RuDiag = 0;
        //		//������ϵı�־λ  0 Ĭ�ϣ� 1 �޸� ��2 ���룬 3 ɾ�� 
        //		public int menDiag = 0;
        //���没����״̬
        private int CaseFlag = 0;
        //��ʾ����
        ucDiagNoseCheck frm = null;
        private FS.FrameWork.Models.NeuObject localObj = new FS.FrameWork.Models.NeuObject();
        private FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterface healthRecordPrint = null;//��ӡ�ӿ�
        //{DC8452B8-FF77-4639-9522-A2CCED4B8A5C}
        private FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterfaceBack healthRecordPrintBack = null;//��ӡ�ӿ� ����


        //{B71C3094-BDC8-4fe8-A6F1-7CEB2AEC55DD}
        private FS.HISFC.BizProcess.Integrate.Manager manageIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        ////{FB6490C7-4A01-443c-8EF4-CC7281379979}
        /// <summary>
        /// �Ƿ�ȫԺ
        /// </summary>
        private bool isAllDept = false;

        private FS.FrameWork.Public.ObjectHelper caseStusHelper = null;

        /// <summary>
        /// ת�Ʋ������� 1 �Զ�����ϵͳת����Ϣ 0 �ֹ�¼��
        /// </summary>
        private int changeDeptType = 0;

        /// <summary>
        /// ������������ 1 �Զ�����ϵͳ������Ϣ 0 �ֹ�¼��
        /// </summary>
        private int operationType = 0;

        //{701EECD4-5ADF-4323-9FC4-73881FB1632D}add20120517
        /// <summary>
        /// ��Ժʱ�䣬ȷ��ʱ�䣬ת��ʱ���Ƿ�ȡ����ʱ�䣨Ĭ��false��
        /// </summary>
        private bool isSetInDate = false;
        private FS.HISFC.BizProcess.Integrate.RADT radtIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();
        private DateTime dtArrive = System.DateTime.Now;

        #endregion

        #region ��������
        ////{FB6490C7-4A01-443c-8EF4-CC7281379979}
        /// <summary>
        /// �Ƿ�ȫԺ����
        /// </summary>
        [Category("�ؼ�����"), Description("�Ƿ�ȫԺ����,True��ȫԺ False:��ǰ����")]
        public bool IsAllDept
        {
            get
            {
                return isAllDept;
            }
            set
            {
                isAllDept = value;
            }
        }
        /// <summary>
        /// ҽ��or����¼�����
        /// </summary>
        [Category("�ؼ�����"), Description("ҽ��or����¼�����")]
        public FS.HISFC.Models.HealthRecord.EnumServer.frmTypes FrmTypes
        {
            get
            {
                return frmType;
            }
            set
            {
                frmType = value;
            }
        }
        //���ز�����¼���
        private bool isHideInputControl = true;
        /// <summary>
        /// �Ƿ����ز�����¼���
        /// </summary>
        [Category("�Ƿ����ز�����¼���"), Description("�Ƿ����ز�����¼��򣺡�ҽ��վ����")]
        public bool IsHideInputControl
        {
            get
            {
                return this.isHideInputControl;
            }
            set
            {
                this.isHideInputControl = value;
                if (value)
                {
                    this.panel2.Width = 0;
                }
            }
        }
        #endregion

        #region ��ʼ��
        //System.Threading.Thread thread = null;
        /// <summary>
        /// ��ʼ���ؼ�
        /// </summary>
        /// <returns></returns>
        public int InitCaseMainInfo()
        {
            InitCountryList();//����

            #region {E9F858A6-BDBC-4052-BA57-68755055FB80}

            if (!IsVisitInput)//�жϴ�������(����¼��/���¼��)
            {
                this.tab1.TabPages.Remove(tabPage8);
            }
            else
            {
                this.panel2.Visible = false;
            }
            #endregion

            #region ����ѡ���
            this.Controls.Add(this.ucPatient);
            this.ucPatient.BringToFront();
            this.ucPatient.Visible = false;
            this.ucPatient.SelectItem += new HealthRecord.Search.ucPatientList.ListShowdelegate(ucPatient_SelectItem);
            #endregion
            //{701EECD4-5ADF-4323-9FC4-73881FB1632D}��ʼ������add20120517
            FS.HISFC.BizProcess.Integrate.Common.ControlParam ctrlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
            this.isSetInDate =ctrlParamIntegrate .GetControlParam <bool>("CASE03",true ,false );
            //this.InitControlParam();
            return 1;
        }
        /// <summary>
        /// ��ʼ�����Ʋ���
        /// </summary>
        private void InitControlParam()
        {
            FS.HISFC.BizProcess.Integrate.Common.ControlParam ctrlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
            changeDeptType = ctrlParamIntegrate.GetControlParam<int>("CASE01", true, 1);
            operationType = ctrlParamIntegrate.GetControlParam<int>("CASE02", true, 1);
        }
        private void ucCaseMainInfo_Load(object sender, EventArgs e)
        {
            InitCaseMainInfo();
            if (!this.isHideInputControl)
            {
                InitTreeView();
            }
            else
            {
                this.panel2.Width = 0;
            }
            this.txtCaseNum.Leave += new EventHandler(txtCaseNum_Leave);
            this.txtPatientNOSearch.Focus();
        }
        #endregion

        #region ��������Ϣ

        /// <summary>
        /// ���幤��������
        /// </summary>
        protected FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        #region ��ʼ��������
        /// <summary>
        /// ��ʼ��������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("ɾ��(&D)", "ɾ��(&D)", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Sɾ��, true, false, null);
            //{DC8452B8-FF77-4639-9522-A2CCED4B8A5C}
            toolBarService.AddToolButton("��ӡ�ڶ�ҳ", "��ӡ�ڶ�ҳ", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D��ӡ, true, false, null);

            toolBarService.AddToolButton("�ύ", "�ύ", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Zִ��, true, false, null);

            toolBarService.AddToolButton("���", "��ص�ҽ��վ����״̬", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Z�ٻ�, true, false, null);
            return toolBarService;
        }
        #endregion

        #region ���������Ӱ�ť�����¼�
        /// <summary>
        /// ���������Ӱ�ť�����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "ɾ��(&D)":
                    DeleteActiveRow();
                    break;
                //{DC8452B8-FF77-4639-9522-A2CCED4B8A5C}
                case "��ӡ�ڶ�ҳ":
                    PrintBack(this.CaseBase);
                    break;
                case "�ύ":
                    this.Save(1);
                    break;
                case "���":
                    this.Save(2);
                    break;
                default:
                    break;
            }
        }
        #endregion

        #endregion

        #region  ���е������б�
        private int InitCountryList()
        {
            //��ȡ�б�
            ArrayList list = FS.HISFC.Models.Base.SexEnumService.List();
            //�����б�
            this.txtPatientSex.AddItems(list);
            //g��ѯ�����б�
            ArrayList list1 = con.GetList(FS.HISFC.Models.Base.EnumConstant.COUNTRY);
            this.txtCountry.AddItems(list1);

            //��ѯ�����б�
            ArrayList Nationallist1 = con.GetList(FS.HISFC.Models.Base.EnumConstant.NATION);
            this.txtNationality.AddItems(Nationallist1);

            //��ѯְҵ�б�
            ArrayList Professionlist = con.GetList(FS.HISFC.Models.Base.EnumConstant.PROFESSION);
            this.txtProfession.AddItems(Professionlist);
            //Ѫ���б�
            ArrayList BloodTypeList = con.GetList("CASEBLOODTYPE");// con.GetList(FS.HISFC.Models.Base.EnumConstant.BLOODTYPE);// baseDml.GetBloodType();
            this.txtBloodType.AddItems(BloodTypeList);
            //�����б�
            ArrayList MaritalStatusList = FS.HISFC.Models.RADT.MaritalStatusEnumService.List();
            this.txtMaritalStatus.AddItems(MaritalStatusList);
            //�������{B71C3094-BDC8-4fe8-A6F1-7CEB2AEC55DD}
            //ArrayList pactKindlist = con.GetList(FS.HISFC.Models.Base.EnumConstant.PACTUNIT);// baseDml.GetPayKindCode(); //GetList(FS.HISFC.Models.Base.EnumConstant.PACTUNIT);
            ArrayList pactKindlist = this.manageIntegrate.QueryPactUnitAll();
            this.txtPactKind.AddItems(pactKindlist);
            //����ϵ�˹�ϵ
            ArrayList RelationList = con.GetList(FS.HISFC.Models.Base.EnumConstant.RELATIVE);
            this.txtRelation.AddItems(RelationList);

            FS.HISFC.BizLogic.Manager.Person person = new FS.HISFC.BizLogic.Manager.Person();
            //��ȡҽ���б�
            ArrayList DoctorList = person.GetEmployeeAll();//.GetEmployee(FS.HISFC.Models.RADT.PersonType.enuPersonType.D);
            this.txtInputDoc.AddItems(DoctorList);
            this.txtCoordinate.AddItems(DoctorList);
            this.txtOperationCode.AddItems(DoctorList);
            this.txtAdmittingDoctor.AddItems(DoctorList);
            this.txtAttendingDoctor.AddItems(DoctorList);
            this.txtConsultingDoctor.AddItems(DoctorList);
            this.txtRefresherDocd.AddItems(DoctorList);
            txtClinicDocd.AddItems(DoctorList);
            txtGraDocCode.AddItems(DoctorList);
            txtQcDocd.AddItems(DoctorList);
            txtQcNucd.AddItems(DoctorList);
            txtCodingCode.AddItems(DoctorList);
            this.txtPraDocCode.AddItems(DoctorList);
            this.txtDeptChiefDoc.AddItems(DoctorList);
            //��ȡ������Դ
            //			ArrayList InAvenuelist = baseDml.GetPatientSource();
            //ArrayList InAvenuelist = con.GetAllList(FS.HISFC.Models.Base.EnumConstant.INAVENUE);
            //this.txtInAvenue.AddItems(InAvenuelist);
            ArrayList Insourcelist = con.GetAllList("PATIENTINSOURCE");
            this.txtInAvenue.AddItems(Insourcelist);
            //��Ժ���
            ArrayList CircsList = con.GetList(FS.HISFC.Models.Base.EnumConstant.INCIRCS);
            this.txtCircs.AddItems(CircsList);

            //ҩ�����
            ArrayList arraylist = con.GetList(FS.HISFC.Models.Base.EnumConstant.PHARMACYALLERGIC);// baseDml.GetHbsagList();
            this.txtHbsag.AddItems(arraylist);

            ////��Ϸ������
            //ArrayList diagAccord = con.GetList(FS.HISFC.Models.Base.EnumConstant.DIAGNOSEACCORD);// baseDml.GetDiagAccord();
            //this.CePi.AddItems(diagAccord);

            //��������
            ArrayList qcList = con.GetList(FS.HISFC.Models.Base.EnumConstant.CASEQUALITY);
            txtMrQual.AddItems(qcList);

            //RH���� 
            ArrayList RHList = con.GetList(FS.HISFC.Models.Base.EnumConstant.RHSTATE); //baseDml.GetRHType();
            txtRhBlood.AddItems(RHList);

            //ArrayList listAccord = con.GetList(FS.HISFC.Models.Base.EnumConstant.ACCORDSTAT);
            //txtHbsag.AddItems(listAccord);
            //txtHcvAb.AddItems(listAccord);
            //txtHivAb.AddItems(listAccord);
            //txtPiPo.AddItems(listAccord);
            //txtOpbOpa.AddItems(listAccord);
            //txtClPa.AddItems(listAccord);
            //txtFsBl.AddItems(listAccord);
            //txtCePi.AddItems(listAccord);
            //HBsAg HCV-Ab HIV-Ab
            ArrayList listAssayType = con.GetList("ASSAYTYPE");
            txtHbsag.AddItems(listAssayType);
            txtHcvAb.AddItems(listAssayType);
            txtHivAb.AddItems(listAssayType);

            //��Ϸ������
            ArrayList listAccord = con.GetList("ACCORDSTATNEW");
            txtPiPo.AddItems(listAccord);
            txtOpbOpa.AddItems(listAccord);
            txtClPa.AddItems(listAccord);
            txtFsBl.AddItems(listAccord);
            txtCePi.AddItems(listAccord);
            //���������б�
            FS.HISFC.BizLogic.Manager.Department dept = new FS.HISFC.BizLogic.Manager.Department();
            ArrayList deptList = dept.GetDeptmentAll();
            txtFirstDept.AddItems(deptList);
            txtDeptSecond.AddItems(deptList);
            txtDeptInHospital.AddItems(deptList);
            txtDeptThird.AddItems(deptList);
            txtDeptOut.AddItems(deptList);

            //InitList(DeptListBox, deptList);
            //ѪҺ��Ӧ

            ArrayList ReactionBloodList = con.GetList(FS.HISFC.Models.Base.EnumConstant.BLOODREACTION);// baseDml.GetReactionBlood();
            txtReactionBlood.AddItems(ReactionBloodList);
            txtReactionTransfuse.AddItems(ReactionBloodList);//��Һ��Ӧ

            //��Ⱦ��λ
            //ArrayList InfectionPosition = con.GetList("INFECTPOSITION");
            //����ҩ��
            ArrayList PharmacyAllergic = con.GetList("PHARMACYALLERGIC");
            this.txtPharmacyAllergic1.AddItems(PharmacyAllergic);
            this.txtPharmacyAllergic2.AddItems(PharmacyAllergic);

            ArrayList ReportedList = con.GetList("Reported");
            this.txtFourDiseasesReport.AddItems(ReportedList);
            this.txtInfectionDiseasesReport.AddItems(ReportedList);
            #region {E9F858A6-BDBC-4052-BA57-68755055FB80}


            //��÷�ʽ
            this.cmbLinkType.AddItems(con.GetList("CASE06"));
            //һ�����
            cmbCircs.AddItems(con.GetList("CASE07"));
            //֢״����
            cmbSymptom.AddItems(con.GetList("CASE10"));
            //����֢
            neuComboBoxSequela.AddItems(con.GetList("CASE09"));
            //����ԭ��
            neuComboBoxDeadReason.AddItems(con.GetList("CASE08"));
            //ת�Ʋ�λ
            neuComboBoxTransferPosition.AddItems(con.GetList("CASE11"));

            //��ý��
            cmbResult.AddItems(con.GetList("CASE14"));


            //���ص绰״̬
            InitTelStateList();

            //���ع�ϵ
            IninRelation();

            #endregion

            FS.HISFC.BizLogic.HealthRecord.ICD icd = new FS.HISFC.BizLogic.HealthRecord.ICD();
            ArrayList listIcd = icd.Query(FS.HISFC.Models.HealthRecord.EnumServer.ICDTypes.ICD10, FS.HISFC.Models.HealthRecord.EnumServer.QueryTypes.Valid);
            foreach (FS.HISFC.Models.HealthRecord.ICD info in listIcd)
            {
                if (info.ID.IndexOf('U') == 0 || info.ID.IndexOf('V') == 0 || info.ID.IndexOf('W') == 0 || info.ID.IndexOf('X') == 0 || info.ID.IndexOf('Y') == 0 || info.ID.IndexOf('Z') == 0)
                {
                    icdList.Add(info);
                }
            }
            FS.HISFC.Models.HealthRecord.ICD icdInfo = new FS.HISFC.Models.HealthRecord.ICD();
            icdInfo.ID = "MS999";
            icdInfo.Name = "/";
            icdInfo.SpellCode = "WFX";
            icdInfo.WBCode = "FNG";
            icdList.Add(icdInfo);
            this.txtInjuryOrPoisoningCause.AddItems(icdList);

            this.caseStusHelper = new FS.FrameWork.Public.ObjectHelper(con.GetList("CASESTUS"));
            return 1;
        }

        #endregion

        #region ��ѯ������Ϣ

        protected override int OnQuery(object sender, object neuObject)
        {
            FS.HISFC.Components.Common.Forms.frmSearchPatient frm = new FS.HISFC.Components.Common.Forms.frmSearchPatient();
            frm.SelectItem += new FS.HISFC.Components.Common.Forms.frmSearchPatient.ListShowdelegate(frm_SelectItem);
            frm.Show();
            
            return base.OnQuery(sender, neuObject);
        }

        void frm_SelectItem(FS.HISFC.Models.HealthRecord.Base obj)
        {
            LoadInfo(obj.PatientInfo.ID, this.frmType);
        }

        /// <summary>
        /// ��ʼ��TreeView
        /// </summary>
        public void InitTreeView()
        {
            ArrayList al = new ArrayList();
            TreeNode tnParent;
            this.treeView1.HideSelection = false;
            //Neuosft.FS.HISFC.BizProcess.Integrate.RADT pQuery = new FS.HISFC.BizProcess.Integrate.RADT(); //t.RADT.InPatient();
            this.treeView1.BeginUpdate();
            this.treeView1.Nodes.Clear();
            //����ͷ
            tnParent = new TreeNode();
            tnParent.Text = "�����Ժ����";
            tnParent.Tag = "%";
            try
            {
                tnParent.ImageIndex = 0;
                tnParent.SelectedImageIndex = 1;
            }
            catch { }
            this.treeView1.Nodes.Add(tnParent);
            DateTime dt = this.baseDml.GetDateTimeFromSysDateTime();
            DateTime dt2 = dt.AddDays(-3);
            ////{FB6490C7-4A01-443c-8EF4-CC7281379979}
            string strBegin = dt.Year.ToString() + "-" + dt.Month.ToString() + "-" + dt.Day.ToString() + " 23:59:59";
            string strEnd = dt2.Year.ToString() + "-" + dt2.Month.ToString() + "-" + dt2.Day.ToString() + " 00:00:00";
            FS.HISFC.Models.Base.Employee personObj = (FS.HISFC.Models.Base.Employee)baseDml.Operator;

            //��������Ժ���㻼����Ϣ
            if (isAllDept)
            {
                al = this.baseDml.QueryPatientOutHospital(strEnd, strBegin, "ALL");
            }
            else
            {
                al = this.baseDml.QueryPatientOutHospital(strEnd, strBegin, personObj.Dept.ID);
            }
            if (al == null)
            {
                MessageBox.Show("��ѯ��Ժ������Ϣʧ��");
                return;
            }

            foreach (FS.HISFC.Models.RADT.PatientInfo pInfo in al)
            {
                TreeNode tnPatient = new TreeNode();

                tnPatient.Text = pInfo.Name + "[" + pInfo.PID.PatientNO + "]";
                tnPatient.Tag = pInfo;
                try
                {
                    tnPatient.ImageIndex = 2;
                    tnPatient.SelectedImageIndex = 3;
                }
                catch { }
                tnParent.Nodes.Add(tnPatient);
            }

            tnParent.Expand();
            this.treeView1.EndUpdate();
        }

        private void patientTreeView_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            if (e.Node.Tag.GetType() == typeof(FS.HISFC.Models.RADT.PatientInfo))
            {
                //				this.Reset();
                //				caseBase.PatientInfo = ((FS.HISFC.Models.RADT.PatientInfo)e.Node.Tag).Clone();
                //				this.ucCaseFirstPage1.Item = caseBase.Clone();
                //				ArrayList alOrg = new ArrayList();
                //				ArrayList alNew = new ArrayList();
                //				alOrg = myBaseDML.GetInhosDiagInfo( caseBase.PatientInfo.ID, "%");
                //				FS.HISFC.Models.HealthRecord.Diagnose dg;
                //				for(int i = 0; i < alOrg.Count; i++)
                //				{
                //					dg = new FS.HISFC.Models.HealthRecord.Diagnose();
                //					dg.DiagInfo = ((FS.HISFC.Models.Case.DiagnoseBase)alOrg[i]).Clone();
                //					alNew.Add( dg );
                //				}
                //				this.ucCaseFirstPage1.AlDiag = alNew;
            }
        }


        #endregion

        #region �¼�

        #region �Ա�

        private void PatientSex_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                dtPatientBirthday.Focus();
            }
        }
        #endregion
        #region �������
        private void ClinicDiag_Enter(object sender, System.EventArgs e)
        {
            //			//���浫ǰ��ؼ�
            //			if(ClinicDiag.ReadOnly)
            //			{
            //				return ;
            //			}
            //			contralActive = this.ClinicDiag;
            //			listBoxActive = ICDListBox;
            //			ListBoxActiveVisible(true);
        }

        private void ClinicDiag_TextChanged(object sender, System.EventArgs e)
        {
            //			ListFilter();
        }
        private void ClinicDiag_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                txtClinicDocd.Focus();
            }
            //			else if(e.KeyData == Keys.Up)
            //			{
            //				ICDListBox.PriorRow();
            //			}
            //			else if(e.KeyData == Keys.Down)
            //			{
            //				ICDListBox.NextRow();
            //			}
        }
        #endregion
        #region ����
        private void Country_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                txtDIST.Focus();
            }
        }
        #endregion
        #region  ����
        private void Nationality_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtCountry.Focus();
            }
        }
        #endregion
        #region  Ѫ��
        private void BloodType_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtRhBlood.Focus();
            }
        }
        #endregion
        #region ����
        private void MaritalStatus_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtProfession.Focus();
            }
        }
        #endregion
        #region ְҵ
        private void Profession_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                txtAreaCode.Focus();
            }
        }
        #endregion
        #region ��ϵ�˹�ϵ
        private void Relation_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                txtLinkmanTel.Focus();
            }
        }
        #endregion
        #region  ��Ժ���


        private void Circs_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtInAvenue.Focus();
            }
        }

        #endregion
        #region ����ҽ��
        private void ClinicDocd_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtPiDays.Focus();
            }
        }
        #endregion
        #region ������Դ
        private void InAvenue_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtDateOut.Focus();
            }
        }
        #endregion
        #region ҩ�����
        private void Hbsag_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                txtHcvAb.Focus();
            }
        }
        private void HcvAb_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                txtHivAb.Focus();
            }
        }
        private void HivAb_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtCePi.Focus();
            }
        }
        #endregion
        #region ��Ϸ���

        private void CePi_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                txtPiPo.Focus();
            }
        }
        private void PiPo_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                txtOpbOpa.Focus();
            }
        }

        private void OpbOpa_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtClPa.Focus();
            }
        }
        private void ClPa_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                txtFsBl.Focus();
            }
        }

        private void FsBl_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                txtSalvTimes.Focus();
            }
        }
        #endregion
        #region  סԺҽ��
        private void AdmittingDoctor_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                txtRefresherDocd.Focus();
            }
        }
        #endregion
        #region ����ҽʦ
        private void RefresherDocd_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtPraDocCode.Focus();
            }
        }
        #endregion
        #region �о���ʵϰҽʦ
        private void GraDocCode_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtComeFrom.Focus();
            }
        }
        #endregion
        #region ʵϰҽ��
        private void PraDocCode_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtGraDocCode.Focus();
            }
        }
        #endregion
        #region  ����ҽʦ
        private void AttendingDoctor_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                txtAdmittingDoctor.Focus();
            }
        }
        #endregion
        #region ����ҽʦ
        private void ConsultingDoctor_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                txtAttendingDoctor.Focus();
            }
        }
        #endregion
        #region  �ʿػ�ʿ
        private void QcNucd_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtCodingCode.Focus();
            }
        }

        #endregion
        #region �ʿ�ҽ��
        private void QcDocd_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                txtQcNucd.Focus();
            }
        }
        #endregion
        #region ����Ա
        private void CodingCode_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                txtCoordinate.Focus();
            }
        }
        #endregion
        #region ����Ա
        private void textBox33_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                txtOperationCode.Focus();
            }
        }
        #endregion
        #region ��������
        private void MrQual_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                txtQcDocd.Focus();
            }
        }
        #endregion
        #region  ��Ѫ��ӳ
        private void ReactionBlood_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                //if (ReactionBlood.Tag != null)
                //{
                //    //if (ReactionBlood.Tag.ToString() != "2")
                //    //{
                //    //    BloodRed.Focus();
                //    //}
                //    //else
                //    //{
                //        //Ժ�ʻ������
                //        InconNum.Focus();
                //    //}
                //}
                //else
                //{
                txtBloodRed.Focus();
                //}
            }
        }

        #endregion
        #region ����Ա
        private void InputDoc_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                //�����ж� ��������ϰ�
                this.tab1.SelectedIndex = 1;
            }
        }
        #endregion
        #region  ��Ժ���


        private void RuyuanDiagNose_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                //�����ж� ���������
                this.txtConsultingDoctor.Focus();
            }
            //else if (e.KeyData == Keys.Up)
            //{
            //    listBoxActive.PriorRow();
            //}
            //else if (e.KeyData == Keys.Down)
            //{
            //    listBoxActive.NextRow();
            //}
        }


        #endregion
        #region  ��Ժ����
        private void DeptInHospital_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtCircs.Focus();
            }
        }
        #endregion
        #region  RH��Ӧ
        private void RhBlood_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtReactionBlood.Focus();
            }
        }
        #endregion
        #region  ������
        private void AreaCode_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtNationality.Focus();
            }
        }
        #endregion
        #region ת��1
        private void firstDept_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.dtSecond.Focus();
            }
        }
        #endregion
        private void deptSecond_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.dtThird.Focus();
            }
        }
        #endregion

        #region ɾ����ǰ��
        /// <summary>
        ///ɾ����ǰ��
        /// </summary>
        /// <returns></returns>
        public int DeleteActiveRow()
        {
            switch (this.tab1.SelectedTab.Name)
            {
                // case "������Ϣ":
                case "tabPage6":
                    if (operationType == 0)
                    {
                        this.ucOperation1.DeleteActiveRow();
                    }
                    break;
                // case "�����Ϣ":
                case "tabPage5":
                    this.ucDiagNoseInput1.DeleteActiveRow();
                    break;
                // case "ת����Ϣ":
                case "tabPage3":
                    this.ucChangeDept1.DeleteActiveRow();
                    break;
                // case "������Ϣ":
                case "tabPage7":
                    this.ucTumourCard2.DeleteActiveRow();
                    break;
                // case "��Ӥ��Ϣ":
                case "tabPage2":
                    this.ucBabyCardInput1.DeleteActiveRow();
                    break;
                //case "������Ϣ":
                case "tabPage1":
                    MessageBox.Show("������Ϣ������ɾ��");
                    break;
            }
            return 1;
        }
        #endregion

        #region ��������
        /// <summary>
        /// ��д���溯��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        public override int Save(object sender, object neuObject)
        {
            return this.Save(0);
        }

        /// <summary>
        /// ���溯��
        /// </summary>
        /// <param name="type">�������� 0 �ݴ� 1 �ύ 2 ���˳�ʼ״̬</param>
        /// <returns></returns>
        private int Save(int type)
        {
            if (CaseBase == null || CaseBase.PatientInfo.ID == null || CaseBase.PatientInfo.ID == "")
            {
                MessageBox.Show("������סԺ��ˮ�Ż�ѡ����");
                return -1;
            }
            #region �жϻ����Ƿ���Ժ
            FS.HISFC.BizLogic.RADT.InPatient radtMana = new FS.HISFC.BizLogic.RADT.InPatient();
            FS.HISFC.Models.RADT.PatientInfo patientInfoForUpdate = radtMana.QueryPatientInfoByInpatientNO(CaseBase.PatientInfo.ID);

            if (patientInfoForUpdate != null)
            {
                if (patientInfoForUpdate.PVisit.InState.ID != null)
                {
                    if (patientInfoForUpdate.PVisit.InState.ID.ToString() == "R" || patientInfoForUpdate.PVisit.InState.ID.ToString() == "I")
                    {
                        if (MessageBox.Show("�û�������סԺ���Ƿ񱣴没����Ϣ��", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
                        {
                            return 0;
                        }
                    }
                }
            }
            #endregion
            #region  �ж�����Ƿ����Լ��  ��ʱ�����ж� ��ȥ�ٿ������������2012-3-30 chengym
            FS.HISFC.BizLogic.HealthRecord.Diagnose diagNose = new FS.HISFC.BizLogic.HealthRecord.Diagnose();
            //if (this.frmType == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC) //ҽ��վ��ʾ �����Ҳ���Ҫ��ʾ
            //{
            //    if (DiagValueState(diagNose) != 1)
            //    {
            //        return -1;
            //    }
            //}

            System.DateTime dt = diagNose.GetDateTimeFromSysDateTime(); //��ȡϵͳʱ��
            #endregion
            #region  �ж�סԺ�ź�סԺ�����Ƿ��Ѿ�����
            int intI = baseDml.ExistCase(this.CaseBase.PatientInfo.ID, txtCaseNum.Text, txtInTimes.Text);
            if (intI == -1)
            {
                MessageBox.Show("��ѯ����ʧ��");
                return -1;
            }
            if (intI == 2)
            {
                MessageBox.Show(txtCaseNum.Text + " ��" + "�� " + txtInTimes.Text + " ����Ժ�Ѿ�����,�������Ժ����");
                return -1;
            }
            #endregion
            //��������

            //FS.FrameWork.Management.Transaction trans = new FS.FrameWork.Management.Transaction(baseDml.Connection);
            try
            {

                if (CaseBase == null)
                {
                    return -2;
                }
                if (CaseBase.PatientInfo.ID == "")
                {
                    MessageBox.Show("��ָ��Ҫ���没���Ĳ���");
                    return -2;
                }
                if (CaseBase.PatientInfo.CaseState == "0")
                {
                    MessageBox.Show("���˲������в���");
                    return 0;
                }
                if (this.frmType == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC && FS.FrameWork.Function.NConvert.ToInt32(CaseBase.PatientInfo.CaseState) > 2) //ҽ��վ�ύ֮��״̬
                {
                    MessageBox.Show("����״̬Ϊ��" + this.caseStusHelper.GetName(CaseBase.PatientInfo.CaseState) + "�����������޸�");
                    return -3;
                }
                if (this.frmType == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC && (HandCraft == 1 || HandCraft == 2))
                {
                    MessageBox.Show("�������Ѿ��浵�������޸�");
                    return -3;
                }
                if (HandCraft == 1 || HandCraft == 2)  //�ֹ�¼�� ����
                {
                    CaseBase.PatientInfo.CaseState = "3";
                    CaseBase.IsHandCraft = "1";
                }

                FS.FrameWork.Management.PublicTrans.BeginTransaction();

                //trans.BeginTransaction();

                baseDml.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                diagNose.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                deptChange.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                healthRecordFee.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                #region ����������Ϣ
                FS.HISFC.Models.HealthRecord.Base info = new FS.HISFC.Models.HealthRecord.Base();
                int i = this.GetInfoFromPanel(info);
                if (ValidState(info) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    return -1;
                }
                if (type == 1)
                {
                    info.PatientInfo.CaseState = (FS.FrameWork.Function.NConvert.ToInt32(info.PatientInfo.CaseState) + 1).ToString();
                    if (MessageBox.Show("ȷ���ύ����" + this.caseStusHelper.GetName(info.PatientInfo.CaseState) + "��״̬��", "��ʾ", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        return -1;
                    }
                }
                else if (type == 2)
                {
                    frmRemark frmRemark = new frmRemark();
                    frmRemark.CaseBase = info;
                    frmRemark.ShowDialog();
                    if (frmRemark.DialogResult== DialogResult.No)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        return -1;
                    }
                    info.PatientInfo.CaseState = "2";
                }
                //��ִ�и��²��� 
                if (baseDml.UpdateBaseInfo(info) < 1)
                {
                    //����ʧ�� ��ִ�в������ 
                    if (baseDml.InsertBaseInfo(info) < 1)
                    {
                        //����
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("���没�˻�����Ϣʧ�� :" + baseDml.Err);
                        return -1;
                    }
                }
                this.CaseBase = info;//add by chengym 2011-9-22 ���»�ȡ��Ϣ
                this.ucChangeDept1.Patient = info.PatientInfo;
                this.ucDiagNoseInput1.Patient = info.PatientInfo;
                this.ucOperation1.Patient = info.PatientInfo;
                this.ucBabyCardInput1.Patient = info.PatientInfo;
                this.ucTumourCard2.Patient = info.PatientInfo;
                this.ucFeeInfo1.Patient = info.PatientInfo;
                #region  �ʼ�����,���� סԺ����� ������־ �ж�ʱ���»��ǲ������  �������ÿ��� ����ɾ����
                //				if(CaseBase.PatientInfo.CaseState == "1") 
                //				{
                //					//�����в��� ����û�б�����Ĳ���
                //					if(baseDml.InsertBaseInfo(info) < 1)
                //					{
                //						//����
                //						FS.FrameWork.Management.PublicTrans.RollBack();
                //						MessageBox.Show("���没�˻�����Ϣʧ�� :" +baseDml.Err );
                //						return -1;
                //					}
                //					#region �������
                //					//					if(clinicDiag != null)
                //					//					{
                //					//						if(diagNose.InsertDiagnose(clinicDiag) < 1)
                //					//						{
                //					//							FS.FrameWork.Management.PublicTrans.RollBack();
                //					//							MessageBox.Show("�����������ʧ�� :" + diagNose.Err);
                //					//						}
                //					//					}
                //					#endregion 
                //					#region  ��Ժ��� 
                //					//					if(InDiag != null)
                //					//					{
                //					//						if(diagNose.InsertDiagnose(InDiag) < 1)
                //					//						{
                //					//							FS.FrameWork.Management.PublicTrans.RollBack();
                //					//							MessageBox.Show("����ʧ�� :" + diagNose.Err);
                //					//						}
                //					//					}
                //					#endregion 
                //				}
                //				else if(CaseBase.PatientInfo.CaseState == "2" ||CaseBase.PatientInfo.CaseState == "3")
                //				{
                //					//�Ѿ������������ 
                //					if(baseDml.UpdateBaseInfo(info)< 1)
                //					{
                //						FS.FrameWork.Management.PublicTrans.RollBack();
                //						MessageBox.Show("���没�˻�����Ϣʧ�� :" +baseDml.Err );
                //						return -1;
                //					}
                //
                //					#region  ������� 
                ////					if(clinicDiag != null)
                ////					{
                ////						if(diagNose.UpdateDiagnose(clinicDiag) < 1)
                ////						{
                ////							if(diagNose.InsertDiagnose(clinicDiag) < 1)
                ////							{
                ////								FS.FrameWork.Management.PublicTrans.RollBack();
                ////								MessageBox.Show("�����������ʧ�� :" + diagNose.Err);
                ////							}
                ////						}
                ////					}
                //					#endregion 
                //
                //					#region  ��Ժ��� 
                ////					if(InDiag != null)
                ////					{
                ////						if(diagNose.UpdateDiagnose(InDiag) < 1)
                ////						{
                ////							if(diagNose.InsertDiagnose(InDiag) < 1)
                ////							{
                ////								FS.FrameWork.Management.PublicTrans.RollBack();
                ////								MessageBox.Show("������Ժ���ʧ�� :" + diagNose.Err);
                ////							}
                ////						}
                ////					}
                //					#endregion 
                //				}
                #endregion
                #endregion
                #region ת����Ϣ
                if (this.changeDeptType == 0)
                {
                    //���˼·,��ɾ��,Ȼ��ͬһ����.
                    //�������ϵ� 
                    ArrayList deptMain = new ArrayList();
                    //���ӵ� 
                    ArrayList deptAdd = new ArrayList();
                    //�޸Ĺ��� 
                    ArrayList deptMod = new ArrayList();
                    #region ������Ϣ�����ϵ�ת����Ϣ
                    #region ��һ��ת����Ϣ
                    if (txtFirstDept.Tag != null && txtFirstDept.Text.Trim() != string.Empty)
                    {
                        FS.HISFC.Models.RADT.Location deptObj = new FS.HISFC.Models.RADT.Location();
                        deptObj.User02 = CaseBase.PatientInfo.ID;
                        deptObj.Dept.Name = txtFirstDept.Text;
                        deptObj.Dept.ID = txtFirstDept.Tag.ToString();
                        deptObj.Dept.Memo = this.dtFirstTime.Value.ToString();

                        deptMain.Add(deptObj);
                    }
                    #endregion
                    #region  �ڶ���ת����Ϣ
                    if (txtDeptSecond.Tag != null && txtDeptSecond.Text.Trim() != string.Empty)
                    {
                        FS.HISFC.Models.RADT.Location deptObj = new FS.HISFC.Models.RADT.Location();
                        deptObj.User02 = CaseBase.PatientInfo.ID;
                        deptObj.Dept.Name = txtDeptSecond.Text;
                        deptObj.Dept.ID = txtDeptSecond.Tag.ToString();
                        deptObj.Dept.Memo = this.dtSecond.Value.ToString();
                        deptMain.Add(deptObj);
                    }
                    #endregion
                    #region ������ת��
                    if (txtDeptThird.Tag != null && txtDeptThird.Text.Trim() != string.Empty)
                    {
                        FS.HISFC.Models.RADT.Location deptObj = new FS.HISFC.Models.RADT.Location();
                        deptObj.User02 = CaseBase.PatientInfo.ID;
                        deptObj.Dept.Name = txtDeptThird.Text;
                        deptObj.Dept.ID = txtDeptThird.Tag.ToString();
                        deptObj.Dept.Memo = this.dtThird.Value.ToString();
                        deptMain.Add(deptObj);
                    }
                    #endregion
                    #endregion
                    //ɾ���հ���
                    this.ucChangeDept1.deleteRow();
                    //this.ucChangeDept1.GetList("D", deptDel);
                    this.ucChangeDept1.GetList("A", deptAdd);
                    this.ucChangeDept1.GetList("M", deptMod);

                    if (this.ucChangeDept1.ValueState(deptAdd) == -1 || this.ucChangeDept1.ValueState(deptMod) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        return -3;
                    }
                    else
                    {
                        if (deptChange.DeleteChangeDept(info.PatientInfo.ID) < 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("ת����Ϣ����ʧ��" + baseDml.Err);
                            return -3;
                        }
                    }
                    //if (deptDel != null)
                    //{
                    //    foreach (FS.HISFC.Models.RADT.Location obj in deptDel)
                    //    {
                    //        if (deptChange.DeleteChangeDept(obj) < 1)
                    //        {
                    //            FS.FrameWork.Management.PublicTrans.RollBack();
                    //            MessageBox.Show("ת����Ϣ����ʧ��" + baseDml.Err);
                    //            return -1;
                    //        }
                    //    }
                    //}
                    if (deptAdd != null)
                    {
                        foreach (FS.HISFC.Models.RADT.Location obj in deptAdd)
                        {
                            if (deptChange.InsertChangeDept(obj) < 1)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("ת����Ϣ����ʧ��" + baseDml.Err);
                                return -1;
                            }
                        }
                    }
                    if (deptMain != null)
                    {
                        foreach (FS.HISFC.Models.RADT.Location obj in deptMain)
                        {
                            if (deptChange.InsertChangeDept(obj) < 1)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("ת����Ϣ����ʧ��" + baseDml.Err);
                                return -1;
                            }
                        }
                    }
                    if (deptMod != null)
                    {
                        foreach (FS.HISFC.Models.RADT.Location obj in deptMod)
                        {
                            if (deptChange.InsertChangeDept(obj) < 1)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("ת����Ϣ����ʧ��" + baseDml.Err);
                                return -1;
                            }
                        }
                    }
                }
                //��ѯ���������Ϣ
                ArrayList tempChangeDept = deptChange.QueryChangeDeptFromShiftApply(CaseBase.PatientInfo.ID, "2");
                #endregion
                #region  �������

                ////ɾ����
                //ArrayList diagDel = new ArrayList();
                ////���ӵ� 
                //ArrayList diagAdd = new ArrayList();
                ////�޸Ĺ��� 
                //ArrayList diagMod = new ArrayList();
                //#region  �������
                ////				//0 Ĭ�ϣ� 1 �޸� ��2 ���룬 3 ɾ�� 
                ////				if(RuDiag == 1)
                ////				{
                ////					diagMod.Add(clinicDiag);
                ////				}
                ////				else if(RuDiag == 2)
                ////				{
                ////					diagAdd.Add(clinicDiag);
                ////				}
                ////				else if(RuDiag == 3)
                ////				{
                ////					diagDel.Add(clinicDiag);
                ////				}
                //#endregion
                //#region  ��Ժ���
                ////				if(menDiag == 1)
                ////				{
                ////					diagMod.Add(InDiag);
                ////				}
                ////				else if(menDiag == 2)
                ////				{
                ////					diagAdd.Add(InDiag);
                ////				}
                ////				else if(menDiag == 3)
                ////				{
                ////					diagDel.Add(InDiag);
                ////				}
                //#endregion
                ////ɾ���հ���
                //this.ucDiagNoseInput1.deleteRow();
                //this.ucDiagNoseInput1.GetList("A", diagAdd);
                //this.ucDiagNoseInput1.GetList("M", diagMod);
                //this.ucDiagNoseInput1.GetList("D", diagDel);
                //if (this.ucDiagNoseInput1.ValueState(diagAdd) == -1 || this.ucDiagNoseInput1.ValueState(diagMod) == -1 || this.ucDiagNoseInput1.ValueState(diagDel) == -1)
                //{
                //    FS.FrameWork.Management.PublicTrans.RollBack(); //����У��ʧ��
                //    return -3;
                //}
                //if (diagDel != null)
                //{
                //    foreach (FS.HISFC.Models.HealthRecord.Diagnose obj in diagDel)
                //    {
                //        if (diagNose.DeleteDiagnoseSingle(obj.DiagInfo.Patient.ID, obj.DiagInfo.HappenNo, frmType) < 1)
                //        {
                //            FS.FrameWork.Management.PublicTrans.RollBack();
                //            MessageBox.Show("���������Ϣʧ��" + diagNose.Err);
                //            return -1;
                //        }
                //    }
                //}
                //if (diagMod != null)
                //{
                //    foreach (FS.HISFC.Models.HealthRecord.Diagnose obj in diagMod)
                //    {
                //        if (diagNose.UpdateDiagnose(obj) < 1)
                //        {
                //            if (diagNose.InsertDiagnose(obj) < 1)
                //            {
                //                FS.FrameWork.Management.PublicTrans.RollBack();
                //                MessageBox.Show("���������Ϣʧ��" + diagNose.Err);
                //                return -1;
                //            }
                //        }

                //    }
                //}
                //if (diagAdd != null)
                //{
                //    foreach (FS.HISFC.Models.HealthRecord.Diagnose obj in diagAdd)
                //    {
                //        if (diagNose.InsertDiagnose(obj) < 1)
                //        {
                //            FS.FrameWork.Management.PublicTrans.RollBack();
                //            MessageBox.Show("���������Ϣʧ��" + diagNose.Err);
                //            return -1;
                //        }

                //    }
                //}
                ////��ʱ���������޸Ĺ�������
                //ArrayList tempDiag = diagNose.QueryCaseDiagnose(CaseBase.PatientInfo.ID, "%", frmType);

                //modify chengym 2011-9-27 
                List<FS.HISFC.Models.HealthRecord.Diagnose> diagList = new List<FS.HISFC.Models.HealthRecord.Diagnose>();
                this.ucDiagNoseInput1.deleteRow();

                this.ucDiagNoseInput1.GetDiagnosInfo(diagList);
                if (this.ucDiagNoseInput1.ValueStateNew(diagList) == -1)
                {
                    this.tab1.SelectedIndex = 1;
                    FS.FrameWork.Management.PublicTrans.RollBack(); //����У��ʧ��
                    return -3;
                }
                if (diagList != null)
                {
                    diagNose.DeleteDiagnoseAll(CaseBase.PatientInfo.ID, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC,FS.HISFC.Models.Base.ServiceTypes.I);
                    foreach (FS.HISFC.Models.HealthRecord.Diagnose obj in diagList)
                    {
                        if (diagNose.InsertDiagnose(obj) < 1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("���������Ϣʧ��" + diagNose.Err);
                            return -1;
                        }
                    }
                }
                //��ʱ���������޸Ĺ�������
                ArrayList tempDiag = diagNose.QueryCaseDiagnose(CaseBase.PatientInfo.ID, "%", frmType,FS.HISFC.Models.Base.ServiceTypes.I);
                #endregion
                #region  ������Ϣ
                //FS.HISFC.BizLogic.HealthRecord.Operation operation = new FS.HISFC.BizLogic.HealthRecord.Operation();
                //operation.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                ////ɾ����
                //ArrayList operDel = new ArrayList();
                ////���ӵ� 
                //ArrayList operAdd = new ArrayList();
                ////�޸Ĺ��� 
                //ArrayList operMod = new ArrayList();
                ////ɾ���հ���
                //this.ucOperation1.deleteRow();
                //this.ucOperation1.GetList("D", operDel);
                //this.ucOperation1.GetList("A", operAdd);
                //this.ucOperation1.GetList("M", operMod);

                //if (this.ucOperation1.ValueState(operDel) == -1 || this.ucOperation1.ValueState(operAdd) == -1 || this.ucOperation1.ValueState(operMod) == -1)
                //{
                //    FS.FrameWork.Management.PublicTrans.RollBack(); //����У��ʧ��
                //    return -3;
                //}
                //if (operDel != null)
                //{
                //    foreach (FS.HISFC.Models.HealthRecord.OperationDetail obj in operDel)
                //    {
                //        if (operation.delete(frmType, obj) < 1)
                //        {
                //            FS.FrameWork.Management.PublicTrans.RollBack();
                //            MessageBox.Show("�������������Ϣʧ��" + operation.Err);
                //            return -1;
                //        }
                //    }
                //}
                //if (operAdd != null)
                //{
                //    foreach (FS.HISFC.Models.HealthRecord.OperationDetail obj in operAdd)
                //    {
                //        obj.OperDate = dt;
                //        if (operation.Insert(frmType, obj) < 1)
                //        {
                //            FS.FrameWork.Management.PublicTrans.RollBack();
                //            MessageBox.Show("�������������Ϣʧ��" + operation.Err);
                //            return -1;
                //        }
                //    }
                //}
                //if (operMod != null)
                //{
                //    foreach (FS.HISFC.Models.HealthRecord.OperationDetail obj in operMod)
                //    {
                //        if (operation.Update(frmType, obj) < 1)
                //        {
                //            FS.FrameWork.Management.PublicTrans.RollBack();
                //            MessageBox.Show("�������������Ϣʧ��" + operation.Err);
                //            return -1;
                //        }
                //    }
                //}
                //ArrayList tempOperation = operation.QueryOperation(this.frmType, CaseBase.PatientInfo.ID);

                FS.HISFC.BizLogic.HealthRecord.Operation operation = new FS.HISFC.BizLogic.HealthRecord.Operation();
                operation.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                ArrayList operationList = new ArrayList();
                this.ucOperation1.deleteRow();
                ucOperation1.GetOperationList(operationList);
                if (this.ucOperation1.ValueState(operationList) == -1)
                {
                    this.tab1.SelectedIndex = 2;
                    FS.FrameWork.Management.PublicTrans.RollBack(); //����У��ʧ��
                    return -3;
                }
                if (operationList != null)
                {
                    operation.deleteAll(CaseBase.PatientInfo.ID);

                    foreach (FS.HISFC.Models.HealthRecord.OperationDetail obj in operationList)
                    {
                        if (operation.Insert(frmType, obj) < 1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("�������������Ϣʧ��" + operation.Err);
                            return -1;
                        }
                    }
                }
                ArrayList tempOperation = operation.QueryOperation(this.frmType, CaseBase.PatientInfo.ID);

                #endregion
                #region ��Ӥ��Ϣ
                FS.HISFC.BizLogic.HealthRecord.Baby baby = new FS.HISFC.BizLogic.HealthRecord.Baby();
                baby.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                //ɾ����
                ArrayList babyDel = new ArrayList();
                //���ӵ� 
                ArrayList babyAdd = new ArrayList();
                //�޸Ĺ��� 
                ArrayList babyMod = new ArrayList();
                //ɾ���հ���
                this.ucBabyCardInput1.deleteRow();
                this.ucBabyCardInput1.GetList("D", babyDel);
                this.ucBabyCardInput1.GetList("A", babyAdd);
                this.ucBabyCardInput1.GetList("M", babyMod);
                if (this.ucBabyCardInput1.ValueState(babyDel) == -1 || this.ucBabyCardInput1.ValueState(babyAdd) == -1 || this.ucBabyCardInput1.ValueState(babyMod) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack(); //����У��ʧ��
                    return -3;
                }
                if (babyDel != null)
                {
                    foreach (FS.HISFC.Models.HealthRecord.Baby obj in babyDel)
                    {
                        if (baby.Delete(obj) < 1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("���渾Ӥ��Ϣʧ��" + baby.Err);
                            return -1;
                        }
                    }
                }
                if (babyAdd != null)
                {
                    foreach (FS.HISFC.Models.HealthRecord.Baby obj in babyAdd)
                    {
                        if (baby.Insert(obj) < 1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("���渾Ӥ��Ϣʧ��" + baby.Err);
                            return -1;
                        }
                    }

                }
                if (babyMod != null)
                {
                    foreach (FS.HISFC.Models.HealthRecord.Baby obj in babyMod)
                    {
                        if (baby.Update(obj) < 1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("���渾Ӥ��Ϣʧ��" + baby.Err);
                            return -1;
                        }
                    }
                }
                //��ʱ�洢���������Ϣ
                ArrayList tempBaby = baby.QueryBabyByInpatientNo(CaseBase.PatientInfo.ID);
                #endregion
                #region  ������Ϣ
                FS.HISFC.BizLogic.HealthRecord.Tumour tumour = new FS.HISFC.BizLogic.HealthRecord.Tumour();
                tumour.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                FS.HISFC.Models.HealthRecord.Tumour TumInfo = this.ucTumourCard2.GetTumourInfo();
                int m = this.ucTumourCard2.ValueTumourSate(TumInfo);
                if (m == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    return -3;
                }
                else if (m == 2) //��������Ҫ���� 
                {
                    if (tumour.UpdateTumour(TumInfo) < 1)
                    {
                        if (tumour.InsertTumour(TumInfo) < 1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(tumour.Err);
                            return -3;
                        }
                    }
                }
                //ɾ����
                ArrayList tumDel = new ArrayList();
                //���ӵ� 
                ArrayList tumAdd = new ArrayList();
                //�޸Ĺ��� 
                ArrayList tumMod = new ArrayList();
                //ɾ���հ���
                this.ucTumourCard2.deleteRow();
                this.ucTumourCard2.GetList("D", tumDel);
                this.ucTumourCard2.GetList("A", tumAdd);
                this.ucTumourCard2.GetList("M", tumMod);
                if (this.ucTumourCard2.ValueState(tumDel) == -1 || this.ucTumourCard2.ValueState(tumAdd) == -1 || this.ucTumourCard2.ValueState(tumMod) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();//����
                    return -3;
                }
                if (tumDel != null)
                {
                    foreach (FS.HISFC.Models.HealthRecord.TumourDetail obj in tumDel)
                    {
                        if (tumour.DeleteTumourDetail(obj) < 1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("����������Ϣʧ��" + tumour.Err);
                            return -1;
                        }
                    }
                }
                if (tumAdd != null)
                {
                    foreach (FS.HISFC.Models.HealthRecord.TumourDetail obj in tumAdd)
                    {
                        if (tumour.InsertTumourDetail(obj) < 1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("����������Ϣʧ��" + tumour.Err);
                            return -1;
                        }
                    }
                }
                if (tumMod != null)
                {
                    foreach (FS.HISFC.Models.HealthRecord.TumourDetail obj in tumMod)
                    {
                        if (tumour.UpdateTumourDetail(obj) < 1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("����������Ϣʧ��" + tumour.Err);
                            return -1;
                        }
                    }
                }
                //��ѯ�������Ϣ
                ArrayList tempTumour = tumour.QueryTumourDetail(CaseBase.PatientInfo.ID);

                #endregion
                #region  ������Ϣ
                ArrayList feeList = this.ucFeeInfo1.GetFeeInfoList();
                if (this.ucFeeInfo1.ValueState(feeList) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();//����
                    return -3;
                }
                if (feeList != null)
                {
                    foreach (FS.HISFC.Models.RADT.Patient obj in feeList)
                    {
                        obj.ID = this.CaseBase.PatientInfo.ID; //סԺ��ˮ��
                        obj.User01 = this.CaseBase.PatientInfo.PVisit.OutTime.ToString(); //��Ժ����
                        if (healthRecordFee.UpdateFeeInfo(obj) < 1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("���������Ϣʧ��" + baseDml.Err);
                            return -1;
                        }
                    }
                }
                #endregion

                #region  ����ɹ�

                //����Ŀǰ������־ �޸�סԺ����Ĳ�����Ϣ
                if (this.frmType == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC)
                {
                    //ҽ��վ¼�벡��
                    if (baseDml.UpdateMainInfoCaseFlag(CaseBase.PatientInfo.ID, CaseBase.PatientInfo.CaseState) < 1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("��������ʧ��" + baseDml.Err);
                        return -1;
                    }
                    //CaseBase.PatientInfo.CaseState = "2";
                }
                else if (this.frmType == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.CAS && CaseBase.IsHandCraft != "1") //������¼�벡��
                {
                    if (baseDml.UpdateMainInfoCaseFlag(CaseBase.PatientInfo.ID, CaseBase.PatientInfo.CaseState) < 1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("�������� case_flag ʧ��" + baseDml.Err);
                        return -1;
                    }
                    if (baseDml.UpdateMainInfoCaseSendFlag(CaseBase.PatientInfo.ID, CaseBase.PatientInfo.CaseState) < 1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("��������casesend_flag ʧ��" + baseDml.Err);
                        return -1;
                    }
                    //CaseBase.PatientInfo.CaseState = "3";
                }
                //����fpPoint�ĸ��ġ�
                this.ucBabyCardInput1.fpEnterSaveChanges(tempBaby);
                this.ucChangeDept1.fpEnterSaveChanges(tempChangeDept);
                //LoadChangeDept(tempChangeDept);
                this.ucDiagNoseInput1.fpEnterSaveChanges(tempDiag);
                this.ucOperation1.fpEnterSaveChanges(tempOperation);
                this.ucTumourCard2.fpEnterSaveChanges(tempTumour);
                //				RuDiag = 0;  //������ϱ�־
                //				menDiag = 0; //��Ժ��ϱ�־
                //������Ϣ
                //trans.Commit();

                #region ��������
                //���²����������� ������ϣ���Ժ��ϣ���Ժ��� ������ ����һ��� ��������
                //if (baseDml.UpdateBaseDiagAndOperation(CaseBase.PatientInfo.ID, frmType) == -1)
                //{
                //    FS.FrameWork.Management.PublicTrans.RollBack();
                //    MessageBox.Show("���²����������������Ϣʧ��.");
                //    return -1;
                //}
                if (baseDml.UpdateBaseDiagAndOperationNew(CaseBase.PatientInfo.ID, CaseBase.ClinicDiag.ID, CaseBase.ClinicDiag.Name, CaseBase.InHospitalDiag.Name, frmType) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("���²����������������Ϣʧ��.");
                    return -1;
                }
                //ֱ����������Ϣ����ʱ��ֵ��chengym
                //localObj.User01 = CaseBase.PatientInfo.PVisit.OutTime.ToString(); //��Ժ����
                //localObj.User02 = CaseBase.PatientInfo.PVisit.PatientLocation.ID; //��Ժ���� 
                //if (baseDml.DiagnoseAndOperation(localObj, CaseBase.PatientInfo.ID) == -1)
                //{
                //    FS.FrameWork.Management.PublicTrans.RollBack();
                //    MessageBox.Show("���²����������������Ϣʧ��.");
                //    return -1;
                //}

                #region  ������������ chengym 2011-9-28

                if (isUpdataFinIprInmaininfo)
                {
                    //radtMana.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                    Function fun=new Function();
                    if (patientInfoForUpdate != null)
                    {
                        if (fun.ReturnStringValue(CaseBase.PatientInfo.Name, false) != string.Empty)
                        {
                            patientInfoForUpdate.Name = CaseBase.PatientInfo.Name;//����
                        }
                        if (CaseBase.PatientInfo.Sex.ID != null && fun.ReturnStringValue(CaseBase.PatientInfo.Sex.ID.ToString(), false) != string.Empty)//�Ա�
                        {
                            patientInfoForUpdate.Sex.ID = CaseBase.PatientInfo.Sex.ID;
                        }
                        if (fun.ReturnStringValue(CaseBase.PatientInfo.IDCard, false) != string.Empty)
                        {
                            patientInfoForUpdate.IDCard = CaseBase.PatientInfo.IDCard; //���֤��
                        }

                        patientInfoForUpdate.Birthday = CaseBase.PatientInfo.Birthday; // ����
                        if (fun.ReturnStringValue(CaseBase.PatientInfo.Profession.ID, false) != string.Empty)
                        {
                            patientInfoForUpdate.Profession.ID = CaseBase.PatientInfo.Profession.ID; //ְҵ
                        }
                        if (fun.ReturnStringValue(CaseBase.PatientInfo.AddressBusiness, false) != string.Empty)
                        {
                            patientInfoForUpdate.CompanyName = CaseBase.PatientInfo.AddressBusiness; //������λ
                        }
                        if (fun.ReturnStringValue(CaseBase.PatientInfo.PhoneBusiness, false) != string.Empty)
                        {
                            patientInfoForUpdate.PhoneBusiness = CaseBase.PatientInfo.PhoneBusiness; //��λ�绰
                        }
                        if (fun.ReturnStringValue(CaseBase.PatientInfo.BusinessZip, false) != string.Empty)
                        {
                            patientInfoForUpdate.BusinessZip = CaseBase.PatientInfo.BusinessZip; //��λ�ʱ�
                        }
                        if (fun.ReturnStringValue(CaseBase.PatientInfo.AddressHome, false) != string.Empty)
                        {
                            patientInfoForUpdate.AddressHome = CaseBase.PatientInfo.AddressHome; //���ڻ��ͥ����
                        }
                        if (fun.ReturnStringValue(CaseBase.PatientInfo.PhoneHome, false) != string.Empty)
                        {
                            patientInfoForUpdate.PhoneHome = CaseBase.PatientInfo.PhoneHome; //��ͥ�绰
                        }
                        if (fun.ReturnStringValue(CaseBase.PatientInfo.HomeZip, false) != string.Empty)
                        {
                            patientInfoForUpdate.HomeZip = CaseBase.PatientInfo.HomeZip;//���ڻ��ͥ��������
                        }
                        if (fun.ReturnStringValue(CaseBase.PatientInfo.DIST, false) != string.Empty)
                        {
                            patientInfoForUpdate.DIST = CaseBase.PatientInfo.DIST; //����
                        }
                        if (fun.ReturnStringValue(CaseBase.PatientInfo.Nationality.ID, false) != string.Empty)
                        {
                            patientInfoForUpdate.Nationality.ID = CaseBase.PatientInfo.Nationality.ID; //����
                        }
                        if (fun.ReturnStringValue(CaseBase.PatientInfo.Kin.Name, false) != string.Empty)
                        {
                            patientInfoForUpdate.Kin.Name = CaseBase.PatientInfo.Kin.Name; //��ϵ������
                        }
                        if (fun.ReturnStringValue(CaseBase.PatientInfo.Kin.RelationPhone, false) != string.Empty)
                        {
                            patientInfoForUpdate.Kin.RelationPhone = CaseBase.PatientInfo.Kin.RelationPhone; //��ϵ�˵绰
                        }
                        if (fun.ReturnStringValue(CaseBase.PatientInfo.Kin.RelationAddress, false) != string.Empty)
                        {
                            patientInfoForUpdate.Kin.RelationAddress = CaseBase.PatientInfo.Kin.RelationAddress; //��ϵ��סַ
                        }
                        if (fun.ReturnStringValue(CaseBase.PatientInfo.Kin.RelationLink, false) != string.Empty)
                        {
                            patientInfoForUpdate.Kin.Relation.ID = CaseBase.PatientInfo.Kin.RelationLink; //��ϵ�˹�ϵ
                        }
                        if (CaseBase.PatientInfo.MaritalStatus.ID !=null && fun.ReturnStringValue(CaseBase.PatientInfo.MaritalStatus.ID.ToString(), false) != string.Empty)
                        {
                            patientInfoForUpdate.MaritalStatus.ID = CaseBase.PatientInfo.MaritalStatus.ID.ToString(); //����״��
                        }
                        if (fun.ReturnStringValue(CaseBase.PatientInfo.Country.ID, false) != string.Empty)
                        {
                            patientInfoForUpdate.Country.ID = CaseBase.PatientInfo.Country.ID; //����
                        }
                        if (CaseBase.PatientInfo.BloodType.ID!=null && fun.ReturnStringValue(CaseBase.PatientInfo.BloodType.ID.ToString(), false) != string.Empty)
                        {
                            patientInfoForUpdate.BloodType.ID = CaseBase.PatientInfo.BloodType.ID.ToString(); //Ѫ��
                        }
                        if (CaseBase.FirstAnaphyPharmacy.Name == "")
                        {
                            patientInfoForUpdate.Disease.IsAlleray = false; //ҩ�����
                        }
                        else
                        {
                            patientInfoForUpdate.Disease.IsAlleray = true; //ҩ�����
                        }

                        if (baseDml.UpdatePatientInfo(patientInfoForUpdate) < 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();

                            MessageBox.Show("����com_patientinfo����!" );

                            return -1;
                        }

                        if (baseDml.UpdatePatient(patientInfoForUpdate) < 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();

                            MessageBox.Show("����fin_ipr_inmaininfo����!" );

                            return -1;
                        }
                    }
                }
                #endregion
                FS.FrameWork.Management.PublicTrans.Commit();
                this.tab1.SelectedIndex = 0;
                #endregion
                //�ֹ�¼�벡����־�ó�Ĭ�ϱ�־ 
                this.HandCraft = 0;
                #endregion
                MessageBox.Show("����ɹ�");
                //this.ClearInfo();
            }
            catch (Exception ex)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(ex.Message);
                return -1;
            }
            return 1;
        }

        #endregion

        #region ѡ��TABҳ
        private void tab1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            switch (this.tab1.SelectedTab.Name)
            {
                //case "������Ϣ":
                case "tabPage7":
                    //���С���� ������һ��
                    if (this.ucTumourCard2.GetfpSpreadRowCount() == 0)
                    {
                        this.ucTumourCard2.AddRow();
                        this.ucTumourCard2.SetActiveCells();
                    }
                    break;
                // case "������Ϣ":
                case "tabPage6":
                    //���С���� ������һ��
                    if (this.ucOperation1.GetfpSpread1RowCount() == 0)
                    {
                        this.ucOperation1.AddRow();
                        this.ucOperation1.SetActiveCells();
                    }
                    break;
                // case "������Ϣ":
                case "tabPage4":
                    break;
                // case "�����Ϣ":
                case "tabPage5":
                    if (this.ucDiagNoseInput1.GetfpSpreadRowCount() == 0)
                    {
                        this.ucDiagNoseInput1.AddRow();
                        this.ucDiagNoseInput1.SetActiveCells();
                    }
                    break;
                //case "��Ӥ��Ϣ":
                case "tabPage2":
                    if (this.ucBabyCardInput1.GetfpSpreadRowCount() == 0)
                    {
                        this.ucBabyCardInput1.AddRow();
                        this.ucBabyCardInput1.SetActiveCells();
                    }
                    break;
                // case "ת����Ϣ":
                case "tabPage3":
                    if (this.ucChangeDept1.GetfpSpreadRowCount() == 0)
                    {
                        this.ucChangeDept1.AddRow();
                        this.ucChangeDept1.SetActiveCells();
                    }
                    break;
            }
        }
        #endregion

        #region  ��������ʾ��������

        #region ���ػ�����Ϣ
        /// <summary>
        /// ��������ʾ��������
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private int ConvertInfoToPanel(FS.HISFC.Models.HealthRecord.Base info)
        {
            try
            {
                #region  ��Ժ���ң���Ժ����
                if (CaseBase.PatientInfo.CaseState == "1")
                {
                    FS.HISFC.Models.RADT.Location indept = baseDml.GetDeptIn(CaseBase.PatientInfo.ID);
                    //FS.HISFC.Models.RADT.Location outdept = baseDml.GetDeptOut(CaseBase.PatientInfo.ID);
                    if (indept != null) //��Ժ���� 
                    {
                        CaseBase.InDept.ID = indept.Dept.ID;
                        CaseBase.InDept.Name = indept.Dept.Name;
                        //��Ժ���Ҵ���
                        txtDeptInHospital.Tag = indept.Dept.ID;
                        //��Ժ��������
                        //txtDeptInHospital.Text = indept.Dept.Name;

                    }
                    else
                    {
                        //��Ժ���Ҵ���
                        txtDeptInHospital.Tag = info.PatientInfo.PVisit.PatientLocation.Dept.ID;
                    }
                    //��Ժ����
                    CaseBase.OutDept.ID = info.PatientInfo.PVisit.PatientLocation.Dept.ID;
                    CaseBase.OutDept.Name = info.PatientInfo.PVisit.PatientLocation.Dept.Name;
                    ////��Ժ���Ҵ���
                    //txtDeptOut.Tag = info.PatientInfo.PVisit.PatientLocation.Dept.ID;
                }
                else
                {
                    //��Ժ���Ҵ���
                    txtDeptInHospital.Tag = info.InDept.ID;
                    ////��Ժ���Ҵ���
                    //txtDeptOut.Tag = info.OutDept.ID;
                }
                //��Ժ���Ҵ���
                txtDeptOut.Tag = this.dept_out;
                #endregion

                //סԺ��  ������
                if (info.CaseNO == "" || info.CaseNO == null)
                {
                    txtCaseNum.Text = info.PatientInfo.PID.PatientNO;
                }
                else
                {
                    txtCaseNum.Text = info.CaseNO;
                }
                //���￨��  �����
                txtClinicNo.Text = info.PatientInfo.PID.CardNO;
                //����
                this.txtPatientName.Text = info.PatientInfo.Name;
                //������
                txtNomen.Text = info.Nomen;
                //�Ա�
                if (info.PatientInfo.Sex.ID != null)
                {
                    txtPatientSex.Tag = info.PatientInfo.Sex.ID.ToString();
                }
                //����
                if (info.PatientInfo.Birthday != System.DateTime.MinValue)
                {
                    dtPatientBirthday.Value = info.PatientInfo.Birthday;
                }
                else
                {
                    dtPatientBirthday.Value = System.DateTime.Now;
                }
                //Ϊ��֤��������Ӳ���һ�£�ֱ��ʹ�õ��Ӳ�����������㺯��fun_get_age_new
                this.txtPatientAge.Text = this.baseDml.GetAgeByFun(dtPatientBirthday.Value.Date, info.PatientInfo.PVisit.InTime.Date);
                //���� ����
                txtCountry.Tag = info.PatientInfo.Country.ID;
                //���� 
                txtNationality.Tag = info.PatientInfo.Nationality.ID;
                //ְҵ
                txtProfession.Tag = info.PatientInfo.Profession.ID;
                //Ѫ�ͱ���
                if (info.PatientInfo.BloodType.ID != null)
                {
                    txtBloodType.Tag = info.PatientInfo.BloodType.ID.ToString();
                }
                //����
                if (info.PatientInfo.MaritalStatus.ID != null)
                {
                    txtMaritalStatus.Tag = info.PatientInfo.MaritalStatus.ID;
                }
                //���֤��
                txtIDNo.Text = info.PatientInfo.IDCard;
                //��������
                txtPactKind.Tag = info.PatientInfo.Pact.ID;
                //ҽ�����Ѻ�
                txtSSN.Text = info.PatientInfo.SSN;
                //����
                txtDIST.Text = info.PatientInfo.DIST;
                //������
                txtAreaCode.Tag = info.PatientInfo.AreaCode;
                txtAreaCode.Text = info.PatientInfo.AreaCode;
                //if (txtAreaCode.Text == "")
                //{
                //    txtAreaCode.Text = info.PatientInfo.AreaCode;
                //}
                //��ͥסַ
                txtAddressHome.Text = info.PatientInfo.AddressHome;
                //��ͥ�绰
                txtPhoneHome.Text = info.PatientInfo.PhoneHome;
                //סַ�ʱ�
                if (info.PatientInfo.CaseState == "1")
                {
                    txtHomeZip.Text = info.PatientInfo.User02;
                }
                else
                {
                    txtHomeZip.Text = info.PatientInfo.HomeZip;
                }
                //��λ��ַ
                if (info.PatientInfo.CaseState == "1")
                {
                    txtAddressBusiness.Text = info.PatientInfo.CompanyName;
                }
                else
                {
                    txtAddressBusiness.Text = info.PatientInfo.AddressBusiness;
                }
                //��λ�绰
                txtPhoneBusiness.Text = info.PatientInfo.PhoneBusiness;
                //��λ�ʱ�
                if (info.PatientInfo.CaseState == "1")
                {
                    txtBusinessZip.Text = info.PatientInfo.User01;
                }
                else
                {
                    txtBusinessZip.Text = info.PatientInfo.BusinessZip;
                }
                //��ϵ������
                txtKin.Text = info.PatientInfo.Kin.Name;
                txtKin.Tag = info.PatientInfo.Kin.ID;
                //�뻼�߹�ϵ
                txtRelation.Tag = info.PatientInfo.Kin.RelationLink;
                //��ϵ�绰
                if (info.PatientInfo.CaseState == "1")
                {
                    txtLinkmanTel.Text = info.PatientInfo.Kin.RelationPhone;
                }
                else
                {
                    txtLinkmanTel.Text = info.PatientInfo.Kin.RelationPhone;
                }
                //��ϵ��ַ
                
                txtLinkmanAdd.Text = info.PatientInfo.Kin.RelationAddress;

                //�������ҽ�� ID
                txtClinicDocd.Tag = info.ClinicDoc.ID;
                //�������ҽ������
                //ClinicDocd.Text = info.ClinicDoc.Name;
                //ת��ҽԺ
                txtComeFrom.Text = info.ComeFrom;
                //��Ժ����
                //{701EECD4-5ADF-4323-9FC4-73881FB1632D}
                if (this.isSetInDate)
                {
                    this.dtArrive = radtIntegrate.GetArriveDate(info.PatientInfo.ID);//��ȡ����ʱ��
                    if (this.dtArrive == System.DateTime.MinValue)
                    {                        
                        if (info.PatientInfo.PVisit.InTime != System.DateTime.MinValue)
                        {
                            dtDateIn.Value = info.PatientInfo.PVisit.InTime;
                            this.dtArrive = info.PatientInfo.PVisit.InTime;//��Ժʱ�䲻����Сֵ������ʱ�������Ժʱ��
                        }
                        else
                        {
                            dtDateIn.Value = System.DateTime.Now;
                            this.dtArrive = System.DateTime.Now;//��Ժʱ������Сֵ������ʱ����ڵ�ǰʱ��
                        }
                    }
                    else
                    {
                        dtDateIn.Value = this.dtArrive;
                    }
                }
                else
                {
                    if (info.PatientInfo.PVisit.InTime != System.DateTime.MinValue)
                    {
                        dtDateIn.Value = info.PatientInfo.PVisit.InTime;
                    }
                    else
                    {
                        dtDateIn.Value = System.DateTime.Now;
                    }
                }

                //---------------------or-------------------------
                //FS.HISFC.BizProcess.Integrate.Common.ControlParam ctrlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
                
                //int DtInDept = ctrlParamIntegrate.GetControlParam<int>("CASE03",true,0);
                //if (DtInDept == 1)
                //{
                //    FS.HISFC.BizProcess.Integrate.RADT radtIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();
                //    DateTime dtArrive = radtIntegrate.GetArriveDate(info.PatientInfo.ID);
                //    if (dtArrive == System.DateTime.MinValue)
                //    {
                //        dtDateIn.Value = info.PatientInfo.PVisit.InTime;
                //    }
                //    else
                //    {
                //        dtDateIn.Value = dtArrive;
                //    }
                //}
                //else
                //{
                //    if (info.PatientInfo.PVisit.InTime != System.DateTime.MinValue)
                //    {
                //        dtDateIn.Value = info.PatientInfo.PVisit.InTime;
                //    }
                //    else
                //    {
                //        dtDateIn.Value = System.DateTime.Now;
                //    }
                //}
                if (info.PatientInfo.CaseState == "1")
                {
                    //Ժ�д��� 
                    txtInfectNum.Text = Convert.ToString(this.ucDiagNoseInput1.GetInfectionNum());
                }
                else
                {
                    //Ժ�д��� 
                    txtInfectNum.Text = info.InfectionNum.ToString();
                }
                //סԺ����
                txtInTimes.Text = info.PatientInfo.InTimes.ToString();
                //��Ժ��Դ

                txtInAvenue.Tag = info.PatientInfo.PVisit.InSource.ID;

                //��Ժ״̬                  
                txtCircs.Tag = info.PatientInfo.PVisit.Circs.ID;
                //ȷ������
                if (this.isSetInDate)
                {
                    //{701EECD4-5ADF-4323-9FC4-73881FB1632D}
                    txtDiagDate.Value = this.dtArrive;
                }
                else
                {
                    if (info.DiagDate != System.DateTime.MinValue)
                    {
                        txtDiagDate.Value = info.DiagDate;
                    }
                    else
                    {
                        txtDiagDate.Value = info.PatientInfo.PVisit.InTime;
                    }
                }
                //��������
                //			info.OperationDate 
                //��Ժ����
                if (info.PatientInfo.PVisit.OutTime != System.DateTime.MinValue)
                {
                    txtDateOut.Value = info.PatientInfo.PVisit.OutTime;
                }
                else
                {
                    txtDateOut.Value = System.DateTime.Now;
                }

                if (this.in_State != "I")//��Ժ�༭ʱ��ȡһ�γ�Ժ���ڱ�֤����
                {
                    this.txtDateOut.Value = this.dt_out;
                }
                //ת�����
                //			info.PatientInfo.PVisit.Zg.ID 
                //ȷ������
                //			info.DiagDays
                //סԺ����
                txtPiDays.Text = info.InHospitalDays.ToString();
                if (this.in_State != "I")
                {
                    System.TimeSpan tt = this.dt_out.Date - this.dtDateIn.Value.Date;
                    if (tt.Days == 0)
                    {
                        this.txtPiDays.Text = "1";
                    }
                    else
                    {
                        this.txtPiDays.Text = Convert.ToString(tt.Days);
                    }
                }
                //��������
                //			info.DeadDate = 
                //����ԭ��
                //			info.DeadReason
                //ʬ��
                if (info.CadaverCheck == "1")
                {
                    cbBodyCheck.Checked = true;
                }
                //��������
                //			info.DeadKind 
                //ʬ����ʺ�
                //			info.BodyAnotomize
                //�Ҹα��濹ԭ
                txtHbsag.Tag = info.Hbsag;
                //���β�������
                txtHcvAb.Tag = info.HcvAb;
                //�������������ȱ�ݲ�������
                txtHivAb.Tag = info.HivAb;
                //�ż�_��Ժ����
                txtCePi.Tag = info.CePi;
                //���_Ժ����
                txtPiPo.Tag = info.PiPo;
                //��ǰ_�����
                txtOpbOpa.Tag = info.OpbOpa;
                //�ٴ�_CT����
                //�ٴ�_MRI����
                //�ٴ�_�������
                txtClPa.Tag = info.ClPa;
                //����_�������
                txtFsBl.Tag = info.FsBl;

                //���ȴ���
                txtSalvTimes.Text = info.SalvTimes.ToString();
                //�ɹ�����
                txtSuccTimes.Text = info.SuccTimes.ToString();
                //ʾ�̿���
                if (info.TechSerc == "1")
                {
                    cbTechSerc.Checked = true;
                }
                //�Ƿ�����
                if (info.VisiStat == "1")
                {
                    cbVisiStat.Checked = true;
                }
                //������� ��
                if (info.VisiPeriodWeek == "")
                {
                    txtVisiPeriWeek.Text = "0";
                }
                else
                {
                    txtVisiPeriWeek.Text = info.VisiPeriodWeek;
                }
                //������� ��
                if (info.VisiPeriodMonth == "")
                {
                    txtVisiPeriMonth.Text = "0";
                }
                else
                {
                    txtVisiPeriMonth.Text = info.VisiPeriodMonth;
                }
                //������� ��
                if (info.VisiPeriodYear == "")
                {
                    txtVisiPeriYear.Text = "0";
                }
                else
                {
                    txtVisiPeriYear.Text = info.VisiPeriodYear;
                }
                //Ժ�ʻ������
                txtInconNum.Text = info.InconNum.ToString();
                //Զ�̻���
                txtOutconNum.Text = info.OutconNum.ToString();
                //ҩ�����
                //			info.AnaphyFlag 
                //����ҩ��1
                //this.txtPharmacyAllergic1.Tag = info.FirstAnaphyPharmacy.ID;
                if (info.FirstAnaphyPharmacy.ID != null && info.FirstAnaphyPharmacy.ID.ToString() != "")
                {
                    this.neuTxtPharmacyAllergic1.Text = info.FirstAnaphyPharmacy.ID;
                }
                else
                {
                    this.PharmacyAllergicLoadInfo(info.PatientInfo.ID, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC);
                }
                ////����ҩ��2
                //this.neuTxtPharmacyAllergic2.Text = info.SecondAnaphyPharmacy.ID;
                //this.txtPharmacyAllergic2.Tag = info.SecondAnaphyPharmacy.ID;
                //��Ⱦ��λ
                this.txtInfectionPositionNew.Text = info.InfectionPosition.Name;
                //���ĺ��Ժ����
                //			info.CoutDate
                //סԺҽʦ����
                txtAdmittingDoctor.Tag = info.PatientInfo.PVisit.AdmittingDoctor.ID;
                //סԺҽʦ����
                //AdmittingDoctor.Text = info.PatientInfo.PVisit.AdmittingDoctor.Name;
                //����ҽʦ����
                txtAttendingDoctor.Tag = info.PatientInfo.PVisit.AttendingDoctor.ID;
                //AttendingDoctor.Text = info.PatientInfo.PVisit.AttendingDoctor.Name;
                //����ҽʦ����
                txtConsultingDoctor.Tag = info.PatientInfo.PVisit.ConsultingDoctor.ID;
                //ConsultingDoctor.Text = info.PatientInfo.PVisit.ConsultingDoctor.Name;
                //�����δ���
                //			info.PatientInfo.PVisit.ReferringDoctor.ID
                //�����δ���
                txtDeptChiefDoc.Tag = info.PatientInfo.PVisit.ReferringDoctor.ID;

                //����ҽʦ����
                txtRefresherDocd.Tag = info.RefresherDoc.ID;
                //RefresherDocd.Text = info.RefresherDoc.Name;
                //�о���ʵϰҽʦ����
                txtGraDocCode.Tag = info.GraduateDoc.ID;
                //GraDocCode.Text = info.GraduateDoc.Name;
                //ʵϰҽʦ����
                txtPraDocCode.Tag = info.PatientInfo.PVisit.TempDoctor.ID;
                //PraDocCode.Text = info.GraduateDoc.Name;
                //����Ա
                txtCodingCode.Tag = info.CodingOper.ID;
                //CodingCode.Text = info.CodingName;
                //��������
                txtMrQual.Tag = info.MrQuality;
                //MrQual.Text = CaseQCHelper.GetName(info.MrQual);
                //�ϸ񲡰�
                //			info.MrElig
                //�ʿ�ҽʦ����
                txtQcDocd.Tag = info.QcDoc.ID;
                //QcDocd.Text = info.QcDonm;
                //�ʿػ�ʿ����
                txtQcNucd.Tag = info.QcNurse.ID;
                //QcNucd.Text = info.QcNunm;
                //���ʱ��
                if (info.CheckDate != System.DateTime.MinValue)
                {
                    txtCheckDate.Value = info.CheckDate;
                }
                else
                {
                    txtCheckDate.Value = System.DateTime.Now;
                }

                if (this.in_State != "I" && info.CheckDate.Date<info.PatientInfo.PVisit.OutTime.Date)
                {
                    txtCheckDate.Value = info.PatientInfo.PVisit.OutTime.AddDays(3);//Ĭ�ϳ�Ժ����+3��
                }
                //�����������Ƽ�����Ϊ��Ժ��һ����Ŀ
                if (info.YnFirst == "1")
                {
                    cbYnFirst.Checked = true;
                }
                //RhѪ��(����)
                txtRhBlood.Tag = info.RhBlood;
                //��Ѫ��Ӧ�����ޣ�
                txtReactionBlood.Tag = info.ReactionBlood;
                //��Ⱦ������
                txtInfectionDiseasesReport.Tag = info.InfectionDiseasesReport;
                //�Ĳ�����
                txtFourDiseasesReport.Tag = info.FourDiseasesReport;
                //��ϸ����
                if (info.BloodRed == "" || info.BloodRed == null)
                {
                    txtBloodRed.Text = "0";
                }
                else
                {
                    txtBloodRed.Text = info.BloodRed;
                }
                //ѪС����
                if (info.BloodPlatelet == "" || info.BloodPlatelet == null)
                {
                    txtBloodPlatelet.Text = "0";
                }
                else
                {
                    txtBloodPlatelet.Text = info.BloodPlatelet;
                }
                //Ѫ����
                if (info.BloodPlasma == "" || info.BloodPlasma == null)
                {
                    txtBodyAnotomize.Text = "0";
                }
                else
                {
                    txtBodyAnotomize.Text = info.BloodPlasma;
                }
                //ȫѪ��
                if (info.BloodWhole == "" || info.BloodWhole == null)
                {
                    txtBloodWhole.Text = "0";
                }
                else
                {
                    txtBloodWhole.Text = info.BloodWhole;
                }
                //������Ѫ��
                if (info.BloodOther == "" || info.BloodOther == null)
                {
                    txtBloodOther.Text = "0";
                }
                else
                {
                    txtBloodOther.Text = info.BloodOther;
                }
                //X���
                txtXNumb.Text = info.XNum;
                //CT��
                txtCtNumb.Text = info.CtNum;
                //MRI��
                txtMriNumb.Text = info.MriNum;
                //�����
                txtPathNumb.Text = info.PathNum;
                //B����
                txtBC.Text = info.DsaNum;
                //ECT��
                txtECTNumb.Text = info.EctNum;
                //PET��
                txtPETNumb.Text = info.PetNum;

                //DSA��
                //			info.DsaNumb
                //PET��
                //			info.PetNumb
                //ECT��
                //			info.EctNumb
                //X�ߴ���
                //			info.XTimes
                //CT����
                //			info.CtTimes
                //MR����
                //			info.MrTimes;
                //DSA����
                //			info.DsaTimes
                //PET����
                //			info.PetTimes
                //ECT����
                //			info.EctTimes
                //˵��
                //			info.Memo
                //�鵵�����
                //			info.BarCode
                //��������״̬(O��� I�ڼ�)
                //			info.LendStus
                //����״̬1�����ʼ�2�ǼǱ���3����4�������ʼ�5��Ч
                //			info.CaseStus 
                //�ؼ�����ʱ��
                txtSuperNus.Text = info.SuperNus.ToString();
                //I������ʱ��
                txtINus.Text = info.INus.ToString();
                //II������ʱ��
                txtIINus.Text = info.IINus.ToString();
                //III������ʱ��
                txtIIINus.Text = info.IIINus.ToString();
                //��֢�໤ʱ��
                txtStrictNuss.Text = info.StrictNuss.ToString();
                //���⻤��
                txtSPecalNus.Text = info.SpecalNus.ToString();
                //����Ա
                txtInputDoc.Tag = info.OperInfo.ID;
                //InputDoc.Text = DoctorHelper.GetName(info.OperCode);
                //����Ա 
                txtCoordinate.Tag = info.PackupMan.ID;
                //textBox33.Text = DoctorHelper.GetName(info.PackupMan.ID);
                //��������Ա 
                this.txtOperationCode.Tag = info.OperationCoding.ID;
                txtBC.Text = info.DsaNum;
                //������ 
                cbDisease30.Checked = FS.FrameWork.Function.NConvert.ToBoolean(info.Disease30);

                if (this.caseStusHelper == null)
                {
                    this.caseStusHelper = new FS.FrameWork.Public.ObjectHelper(con.GetList("CASESTUS"));
                }

                //����״̬
                txtCaseStus.Text = this.caseStusHelper.GetName(info.PatientInfo.CaseState);

                //�����ж�ԭ��
                if (info.InjuryOrPoisoningCauseCode != null && info.InjuryOrPoisoningCauseCode!="")
                {
                    txtInjuryOrPoisoningCause.Tag = info.InjuryOrPoisoningCauseCode;
                }
                if (info.InjuryOrPoisoningCause != null)
                {
                    txtInjuryOrPoisoningCause.Text = CaseFirstPage.Funtion.ReplaceSingleQuotationMarks(info.InjuryOrPoisoningCause,false);
                }
                else
                {
                    txtInjuryOrPoisoningCause.Text = info.InjuryOrPoisoningCause;
                }
                //�������
                if (info.PathologicalDiagName != null)
                {
                    this.neuTxtPharmacyAllergic2.Tag = info.PathologicalDiagCode;
                    this.neuTxtPharmacyAllergic2.Text = CaseFirstPage.Funtion.ReplaceSingleQuotationMarks(info.PathologicalDiagName,false);
                }
                else
                {
                    this.neuTxtPharmacyAllergic2.Tag = info.PathologicalDiagCode;
                    this.neuTxtPharmacyAllergic2.Text = info.PathologicalDiagName;
                }
                //��ע˵��
                txtRemark.Text = info.PatientInfo.Memo;
                //��Һ��Ӧ
                this.txtReactionTransfuse.Tag = info.ReactionLiquid;
                //ת����ҽԺ
                this.txtComeTo.Text = info.OutDept.Memo;

                if (info.ClinicDiag.Name != null)
                {
                    this.txtClinicDiag.Text = CaseFirstPage.Funtion.ReplaceSingleQuotationMarks(info.ClinicDiag.Name,false);
                }
                else
                {
                    this.txtClinicDiag.Text = info.ClinicDiag.Name;
                }
                if (info.InHospitalDiag.Name != null)
                {
                    this.txtRuyuanDiagNose.Text = CaseFirstPage.Funtion.ReplaceSingleQuotationMarks(info.InHospitalDiag.Name,false);
                }
                else
                {
                    this.txtRuyuanDiagNose.Text = info.InHospitalDiag.Name;
                }

                //��ȡ��������
                if (info.PatientInfo.CaseState == "1")
                {
                    this.GetNursingNum(info.PatientInfo.ID);
                }
                else
                {
                    if (this.in_State=="I" || ( info.PatientInfo.CaseState=="2" && info.PatientInfo.PVisit.OutTime.Date < this.dt_out.Date))
                    {
                        this.GetNursingNum(info.PatientInfo.ID);
                    }
                    else if (info.SuperNus.ToString() == "0" && info.INus.ToString() == "0" && info.IIINus.ToString() == "0" && info.SpecalNus.ToString() == "0" && info.StrictNuss.ToString() == "0")
                    {
                        this.GetNursingNum(info.PatientInfo.ID);
                    }
                    else
                    {
                        this.txtSuperNus.Text = info.SuperNus.ToString();//�ؼ�����
                        this.txtINus.Text = info.INus.ToString();//һ������
                        this.txtIINus.Text = info.IINus.ToString();//��������
                        this.txtIIINus.Text = info.IIINus.ToString();//��������
                        this.txtSPecalNus.Text = info.SpecalNus.ToString();//���⻤��
                        this.txtStrictNuss.Text = info.StrictNuss.ToString();//��֢����
                    }
                }

                if (this.txtFirstDept.Text == "")//ת��ʱ��
                {
                    //{701EECD4-5ADF-4323-9FC4-73881FB1632D}
                    if (this.isSetInDate)
                    {                       
                            this.dtFirstTime.Value = this.dtArrive;
                            this.dtSecond.Value = this.dtArrive;
                            this.dtThird.Value = this.dtArrive;
                    }
                    else
                    {
                        if (info.PatientInfo.PVisit.InTime != System.DateTime.MinValue)
                        {
                            this.dtFirstTime.Value = info.PatientInfo.PVisit.InTime;
                            this.dtSecond.Value = info.PatientInfo.PVisit.InTime;
                            this.dtThird.Value = info.PatientInfo.PVisit.InTime;
                        }
                        else
                        {
                            this.dtFirstTime.Value = System.DateTime.Now;
                            this.dtSecond.Value = System.DateTime.Now;
                            this.dtThird.Value = System.DateTime.Now;
                        }
                    }
                }
                if (this.txtFirstDept.Text != "" && this.txtDeptSecond.Text == "")
                {
                    this.dtSecond.Value = this.dtFirstTime.Value;
                    this.dtThird.Value = this.dtFirstTime.Value;
                }
                if (this.txtDeptSecond.Text != "" && this.txtDeptThird.Text == "")
                {
                    this.dtThird.Value = this.dtSecond.Value;
                }
                FS.HISFC.BizLogic.HealthRecord.DeptShift deptMger = new FS.HISFC.BizLogic.HealthRecord.DeptShift();
                string changeBed = deptMger.QueryWardNoBedNOByInpatienNO(info.PatientInfo.ID, "1");
                string WardNoBedNO = deptMger.QueryWardNoBedNOByInpatienNO(changeBed, "2");
                this.txtInRoom.Text = WardNoBedNO;//��Ժ����

                this.txtOutRoom.Text = deptMger.QueryWardNoBedNOByInpatienNO(this.bedNo, "2"); //��Ժ����

                return 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }
        }
        #endregion

        #region ����ǰ����ת����Ϣ
        /// <summary>
        /// ����ǰ����ת����Ϣ
        /// </summary>
        /// <param name="list"></param>
        private void LoadChangeDept(ArrayList list)
        {
            if (list == null)
            {
                return;
            }

            #region ת����Ϣ��ǰ�����ڽ�������ʾ
            if (list.Count > 0) //��ת����Ϣ
            {
                //ת����Ϣ��ǰ�����ڽ�������ʾ
                int j = 0;
                if (list.Count >= 3)
                {
                    j = 3;
                }
                else
                {
                    j = list.Count;
                }
                for (int i = 0; i < j; i++)
                {
                    switch (i)
                    {
                        case 0:
                            firDept = (FS.HISFC.Models.RADT.Location)list[0];
                            break;
                        case 1:
                            secDept = (FS.HISFC.Models.RADT.Location)list[1];
                            break;
                        case 2:
                            thirDept = (FS.HISFC.Models.RADT.Location)list[2];
                            break;
                    }
                }
            }
            #endregion

            #region ת����Ϣ
            if (this.firDept != null)
            {
                //firstDept.Text = firDept.Dept.Name;
                txtFirstDept.Tag = firDept.Dept.ID;
                this.dtFirstTime.Value = FS.FrameWork.Function.NConvert.ToDateTime(firDept.Dept.Memo);
            }
            if (this.secDept != null)
            {
                //deptSecond.Text = this.secDept.Dept.Name;
                txtDeptSecond.Tag = this.secDept.Dept.ID;
                this.dtSecond.Value = FS.FrameWork.Function.NConvert.ToDateTime(secDept.Dept.Memo);
            }
            if (this.thirDept != null)
            {
                //deptThird.Text = this.thirDept.Dept.Name;
                txtDeptThird.Tag = this.thirDept.Dept.ID;
                this.dtThird.Value = FS.FrameWork.Function.NConvert.ToDateTime(thirDept.Dept.Memo);
            }
            #endregion
        }
        #endregion

        #endregion

        #region �ӿ�������ϻ�ȡ����
        /// <summary>
        /// �ӿ�������ϻ�ȡ����
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private int GetInfoFromPanel(FS.HISFC.Models.HealthRecord.Base info)
        {
            //System.TimeSpan tt = this.txtDateOut.Value.Date - this.dtDateIn.Value.Date;
            //this.txtPiDays.Text = Convert.ToString(tt.Days);
            //סԺ��ˮ��
            info.PatientInfo.ID = CaseBase.PatientInfo.ID;
            info.IsHandCraft = CaseBase.IsHandCraft;
            //סԺ��  ������
            info.CaseNO = txtCaseNum.Text;
            info.CaseNO = info.CaseNO.PadLeft(10, '0');
            //סԺ��
            info.PatientInfo.PID.PatientNO = CaseBase.PatientInfo.PID.PatientNO;
            //���￨��  �����
            info.PatientInfo.PID.CardNO = txtClinicNo.Text;
            //����
            info.PatientInfo.Name = txtPatientName.Text;
            //������
            info.Nomen = txtNomen.Text;
            //�Ա�
            if (txtPatientSex.Tag != null)
            {
                info.PatientInfo.Sex.ID = txtPatientSex.Tag;
            }
            else
            {
                info.PatientInfo.Sex.ID = CaseBase.PatientInfo.Sex.ID;
            }
            if (info.PatientInfo.Sex.ID == null)
            {
                info.PatientInfo.Sex.ID = "";
            }
            //����
            info.PatientInfo.Birthday = dtPatientBirthday.Value;
            //����
            if (txtCountry.Tag != null)
            {
                info.PatientInfo.Country.ID = txtCountry.Tag.ToString();
            }
            else
            {
                info.PatientInfo.Country.ID = "";
            }
            //���� 
            if (txtNationality.Tag != null)
            {
                info.PatientInfo.Nationality.ID = txtNationality.Tag.ToString();
            }
            else
            {
                info.PatientInfo.Nationality.ID = "";
            }
            //ְҵ
            if (txtProfession.Tag != null)
            {
                info.PatientInfo.Profession.ID = txtProfession.Tag.ToString();
            }
            else
            {
                info.PatientInfo.Profession.ID = "";
            }
            //Ѫ�ͱ���
            info.PatientInfo.BloodType.ID = txtBloodType.Tag;
            //����
            if (txtMaritalStatus.Tag != null)
            {
                info.PatientInfo.MaritalStatus.ID = txtMaritalStatus.Tag;
            }
            else
            {
                info.PatientInfo.MaritalStatus.ID = "";
            }
            //���䵥λ����������͵�λ
            //info.AgeUnit = cmbUnit.Text;
            //����
            //info.PatientInfo.Age = txtPatientAge.Text;
            //if (info.PatientInfo.Age == "")
            //{
            //    info.PatientInfo.Age = "0";
            //}
            info.AgeUnit = txtPatientAge.Text;
            info.PatientInfo.Age = "0";

            //���֤��
            info.PatientInfo.IDCard = txtIDNo.Text;
            //��Ժ;��
            //			if( InSource.Tag != null)
            //			{
            //				info.PatientInfo.PVisit.InSource.ID =  InSource.Tag.ToString();
            //			}
            //��������
            if (txtPactKind.Tag != null)
            {
                FS.HISFC.Models.Base.PactInfo pactInfo = this.pactManager.GetPactUnitInfoByPactCode(this.txtPactKind.Tag.ToString());
                if (pactInfo != null)
                {
                    info.PatientInfo.Pact.PayKind.ID = pactInfo.PayKind.ID;
                }
                else
                {
                    info.PatientInfo.Pact.PayKind.ID = txtPactKind.Tag.ToString();
                }
                info.PatientInfo.Pact.ID = txtPactKind.Tag.ToString();
            }
            else
            {
                info.PatientInfo.Pact.PayKind.ID = "";
                info.PatientInfo.Pact.ID = "";
            }
            //if (txtPactKind.Tag != null)
            //{
            //    info.PatientInfo.Pact.PayKind.ID = txtPactKind.Tag.ToString();
            //    info.PatientInfo.Pact.ID = txtPactKind.Tag.ToString();
            //}
            //else
            //{
            //    info.PatientInfo.Pact.PayKind.ID = "";
            //    info.PatientInfo.Pact.ID = "";
            //}
            //ҽ�����Ѻ�
            info.PatientInfo.SSN = txtSSN.Text;
            //����
            info.PatientInfo.DIST = txtDIST.Text;
            //������
            info.PatientInfo.AreaCode = txtAreaCode.Text;
            //��ͥסַ
            info.PatientInfo.AddressHome = txtAddressHome.Text;
            //��ͥ�绰
            info.PatientInfo.PhoneHome = txtPhoneHome.Text;
            //סַ�ʱ�
            info.PatientInfo.HomeZip = txtHomeZip.Text;
            //��λ��ַ
            info.PatientInfo.AddressBusiness = txtAddressBusiness.Text;
            //��λ�绰
            info.PatientInfo.PhoneBusiness = txtPhoneBusiness.Text;
            //��λ�ʱ�
            info.PatientInfo.BusinessZip = txtBusinessZip.Text;
            //��ϵ������
            info.PatientInfo.Kin.Name = txtKin.Text;
            //�뻼�߹�ϵ
            if (txtRelation.Tag != null)
            {
                info.PatientInfo.Kin.RelationLink = txtRelation.Tag.ToString();
            }
            else
            {
                info.PatientInfo.Kin.RelationLink = "";
            }
            //��ϵ�绰
            info.PatientInfo.Kin.RelationPhone = txtLinkmanTel.Text;
            //��ϵ��ַ
            info.PatientInfo.Kin.RelationAddress = txtLinkmanAdd.Text;
            //�������ҽ�� ID
            if (txtClinicDocd.Tag != null)
            {
                info.ClinicDoc.ID = txtClinicDocd.Tag.ToString();
            }
            else
            {
                info.ClinicDoc.ID = "";
            }
            //�������ҽ������
            info.ClinicDoc.Name = txtClinicDocd.Text;
            //ת��ҽԺ
            info.ComeFrom = txtComeFrom.Text;
            //��Ժ����
            info.PatientInfo.PVisit.InTime = dtDateIn.Value;
            //סԺ����
            info.PatientInfo.InTimes = FS.FrameWork.Function.NConvert.ToInt32(txtInTimes.Text);
            //��Ժ���Ҵ���
            if (txtDeptInHospital.Tag != null)
            {
                info.InDept.ID = txtDeptInHospital.Tag.ToString();
            }
            else
            {
                info.InDept.ID = "";
            }
            //��Ժ��������
            info.InDept.Name = txtDeptInHospital.Text;
            //��Ժ��Դ
            if (txtInAvenue.Tag != null)
            {
                info.PatientInfo.PVisit.InSource.ID = txtInAvenue.Tag.ToString();
                info.PatientInfo.PVisit.InSource.Name = txtInAvenue.Text;
            }
            else
            {
                info.PatientInfo.PVisit.InSource.ID = "";
                info.PatientInfo.PVisit.InSource.Name = "";
            }
            //��Ժ״̬
            if (txtCircs.Tag != null)
            {
                info.PatientInfo.PVisit.Circs.ID = txtCircs.Tag.ToString();
            }
            else
            {
                info.PatientInfo.PVisit.Circs.ID = "";
            }
            //ȷ������
            info.DiagDate = txtDiagDate.Value;

            this.ucDiagNoseInput1.DiagDate = info.DiagDate;//chengym ��ϻ�ȡʱ��
            //��������
            //			info.OperationDate 
            //��Ժ����
            info.PatientInfo.PVisit.OutTime = txtDateOut.Value;
            //��Ժ���Ҵ���
            if (txtDeptOut.Tag != null)
            {
                info.OutDept.ID = txtDeptOut.Tag.ToString();
            }
            else
            {
                info.OutDept.ID = "";
            }
            //��Ժ��������
            info.OutDept.Name = txtDeptOut.Text;
            //ת�����
            //			info.PatientInfo.PVisit.Zg.ID 
            //ȷ������
            //			info.DiagDays
            //סԺ����
            info.InHospitalDays = FS.FrameWork.Function.NConvert.ToInt32(txtPiDays.Text);
            //��������
            //			info.DeadDate = 
            //����ԭ��
            //			info.DeadReason
            //ʬ��
            if (cbBodyCheck.Checked)
            {
                info.CadaverCheck = "1";
            }
            else
            {
                info.CadaverCheck = "0";
            }
            //��������
            //			info.DeadKind 
            //ʬ����ʺ�
            //			info.BodyAnotomize
            //�Ҹα��濹ԭ
            if (txtHbsag.Tag != null)
            {
                info.Hbsag = txtHbsag.Tag.ToString();
            }
            else
            {
                info.Hbsag = "";
            }
            //���β�������
            if (txtHcvAb.Tag != null)
            {
                info.HcvAb = txtHcvAb.Tag.ToString();
            }
            else
            {
                info.HcvAb = "";
            }
            //�������������ȱ�ݲ�������
            if (txtHivAb.Tag != null)
            {
                info.HivAb = txtHivAb.Tag.ToString();
            }
            else
            {
                info.HivAb = "";
            }
            //�ż�_��Ժ����
            if (txtCePi.Tag != null)
            {
                info.CePi = txtCePi.Tag.ToString();
            }
            else
            {
                info.CePi = "";
            }
            //���_Ժ����
            if (txtPiPo.Tag != null)
            {
                info.PiPo = txtPiPo.Tag.ToString();
            }
            else
            {
                info.PiPo = "";
            }
            //��ǰ_�����
            if (txtOpbOpa.Tag != null)
            {
                info.OpbOpa = txtOpbOpa.Tag.ToString();
            }
            else
            {
                info.OpbOpa = "";
            }
            //�ٴ�_�������

            //�ٴ�_CT����
            //�ٴ�_MRI����
            //�ٴ�_�������
            if (txtClPa.Tag != null)
            {
                info.ClPa = txtClPa.Tag.ToString();
            }
            else
            {
                info.ClPa = "";
            }
            //����_�������
            if (txtFsBl.Tag != null)
            {
                info.FsBl = txtFsBl.Tag.ToString();
            }
            else
            {
                info.FsBl = "";
            }
            //���ȴ���
            info.SalvTimes = FS.FrameWork.Function.NConvert.ToInt32(txtSalvTimes.Text.Trim());
            //�ɹ�����
            info.SuccTimes = FS.FrameWork.Function.NConvert.ToInt32(txtSuccTimes.Text.Trim());
            //ʾ�̿���
            if (cbTechSerc.Checked)
            {
                info.TechSerc = "1";
            }
            else
            {
                info.TechSerc = "0";
            }
            //�Ƿ�����
            if (cbVisiStat.Checked)
            {
                info.VisiStat = "1";
            }
            else
            {
                info.VisiStat = "0";
            }
            //������� ��
            info.VisiPeriodWeek = txtVisiPeriWeek.Text;
            //������� ��
            info.VisiPeriodMonth = txtVisiPeriMonth.Text;
            //������� ��
            info.VisiPeriodYear = txtVisiPeriYear.Text;
            //Ժ�ʻ������
            info.InconNum = FS.FrameWork.Function.NConvert.ToInt32(txtInconNum.Text.Trim());
            //Զ�̻���
            info.OutconNum = FS.FrameWork.Function.NConvert.ToInt32(txtOutconNum.Text.Trim());
            //סԺҽʦ����
            if (txtAdmittingDoctor.Tag != null)
            {
                info.PatientInfo.PVisit.AdmittingDoctor.ID = txtAdmittingDoctor.Tag.ToString();
                //סԺҽʦ����
                info.PatientInfo.PVisit.AdmittingDoctor.Name = txtAdmittingDoctor.Text;
            }
            else
            {
                info.PatientInfo.PVisit.AdmittingDoctor.ID = "";
                //סԺҽʦ����
                info.PatientInfo.PVisit.AdmittingDoctor.Name = "";
            }
            //����ҽʦ����
            if (txtAttendingDoctor.Tag != null)
            {
                info.PatientInfo.PVisit.AttendingDoctor.ID = txtAttendingDoctor.Tag.ToString();
                info.PatientInfo.PVisit.AttendingDoctor.Name = txtAttendingDoctor.Text;
            }
            else
            {
                info.PatientInfo.PVisit.AttendingDoctor.ID = "";
                info.PatientInfo.PVisit.AttendingDoctor.Name = "";
            }
            //����ҽʦ����
            if (txtConsultingDoctor.Tag != null)
            {
                info.PatientInfo.PVisit.ConsultingDoctor.ID = txtConsultingDoctor.Tag.ToString();
                info.PatientInfo.PVisit.ConsultingDoctor.Name = txtConsultingDoctor.Text;
            }
            else
            {
                info.PatientInfo.PVisit.ConsultingDoctor.ID = "";
                info.PatientInfo.PVisit.ConsultingDoctor.Name = "";
            }
            //�����δ���
            if (txtDeptChiefDoc.Tag != null)
            {
                info.PatientInfo.PVisit.ReferringDoctor.ID = txtDeptChiefDoc.Tag.ToString();
                info.PatientInfo.PVisit.ReferringDoctor.Name = txtDeptChiefDoc.Text;
            }
            else
            {
                info.PatientInfo.PVisit.ReferringDoctor.ID = "";
                info.PatientInfo.PVisit.ReferringDoctor.Name = "";
            }
            //����ҽʦ����
            if (txtRefresherDocd.Tag != null)
            {
                info.RefresherDoc.ID = txtRefresherDocd.Tag.ToString();
                info.RefresherDoc.Name = txtRefresherDocd.Text;
            }
            else
            {
                info.RefresherDoc.ID = "";
                info.RefresherDoc.Name = "";
            }
            //�о���ʵϰҽʦ����
            if (txtGraDocCode.Tag != null)
            {
                info.GraduateDoc.ID = txtGraDocCode.Tag.ToString();
                info.GraduateDoc.Name = txtGraDocCode.Text.Trim();
            }
            else
            {
                info.GraduateDoc.ID = "";
                info.GraduateDoc.Name = "";
            }
            //ʵϰҽʦ����
            if (txtPraDocCode.Tag != null)
            {
                info.PatientInfo.PVisit.TempDoctor.ID = txtPraDocCode.Tag.ToString();
                info.PatientInfo.PVisit.TempDoctor.Name = txtPraDocCode.Text.Trim();
            }
            else
            {
                info.PatientInfo.PVisit.TempDoctor.ID = "";
                info.PatientInfo.PVisit.TempDoctor.Name = "";
            }
            //����Ա
            if (txtCodingCode.Tag != null)
            {
                info.CodingOper.ID = txtCodingCode.Tag.ToString();
                info.CodingOper.Name = txtCodingCode.Text.Trim();
            }
            else
            {
                info.CodingOper.ID = "";
                info.CodingOper.Name = "";
            }
            //��������
            if (txtMrQual.Tag != null)
            {
                info.MrQuality = txtMrQual.Tag.ToString();
            }
            else
            {
                info.MrQuality = "";
            }
            //�ϸ񲡰�
            //			info.MrElig
            //�ʿ�ҽʦ����
            if (txtQcDocd.Tag != null)
            {
                info.QcDoc.ID = txtQcDocd.Tag.ToString();
                info.QcDoc.Name = txtQcDocd.Text.Trim();
            }
            else
            {
                info.QcDoc.ID = "";
                info.QcDoc.Name = "";
            }
            //�ʿػ�ʿ����
            if (txtQcNucd.Tag != null)
            {
                info.QcNurse.ID = txtQcNucd.Tag.ToString();
                info.QcNurse.Name = txtQcNucd.Text.Trim();
            }
            else
            {
                info.QcNurse.ID = "";
                info.QcNurse.Name = "";
            }
            //���ʱ��
            info.CheckDate = txtCheckDate.Value;
            //�����������Ƽ�����Ϊ��Ժ��һ����Ŀ
            if (cbYnFirst.Checked)
            {
                info.YnFirst = "1";
            }
            else
            {
                info.YnFirst = "0";
            }
            //RhѪ��(����)
            if (txtRhBlood.Tag != null)
            {
                info.RhBlood = txtRhBlood.Tag.ToString();
            }
            else
            {
                info.RhBlood = "";
            }
            //��Ѫ��Ӧ�����ޣ�
            if (txtReactionBlood.Tag != null)
            {
                info.ReactionBlood = txtReactionBlood.Tag.ToString();
            }
            else
            {
                info.ReactionBlood = "";
            }
            //��Ⱦ������
            if (txtInfectionDiseasesReport.Tag != null)
            {
                info.InfectionDiseasesReport = txtInfectionDiseasesReport.Tag.ToString();
            }
            else
            {
                info.InfectionDiseasesReport = "";
            }
            //�Ĳ�����
            if (txtFourDiseasesReport.Tag != null)
            {
                info.FourDiseasesReport = txtFourDiseasesReport.Tag.ToString();
            }
            else
            {
                info.FourDiseasesReport = "";
            }

            //��ϸ����
            info.BloodRed = txtBloodRed.Text;
            //ѪС����
            info.BloodPlatelet = txtBloodPlatelet.Text;
            //Ѫ����
            info.BloodPlasma = txtBodyAnotomize.Text;
            //ȫѪ��
            info.BloodWhole = txtBloodWhole.Text;
            //������Ѫ��
            info.BloodOther = txtBloodOther.Text;
            //X���
            info.XNum = txtXNumb.Text;
            //CT��
            info.CtNum = txtCtNumb.Text;
            //MRI��
            info.MriNum = txtMriNumb.Text;
            // ����� 
            info.PathNum = txtPathNumb.Text;
            //B����
            info.DsaNum = txtBC.Text;
            //PET ��
            info.PetNum = txtPETNumb.Text;
            //ECT��
            info.EctNum = txtECTNumb.Text;
            //�ؼ�����ʱ��
            info.SuperNus = FS.FrameWork.Function.NConvert.ToInt32(txtSuperNus.Text);
            //I������ʱ��
            info.INus = FS.FrameWork.Function.NConvert.ToInt32(txtINus.Text);
            //II������ʱ��
            info.IINus = FS.FrameWork.Function.NConvert.ToInt32(txtIINus.Text);
            //III������ʱ��
            info.IIINus = FS.FrameWork.Function.NConvert.ToInt32(txtIIINus.Text);
            //��֢�໤ʱ��
            info.StrictNuss = FS.FrameWork.Function.NConvert.ToInt32(txtStrictNuss.Text);
            //���⻤��
            info.SpecalNus = FS.FrameWork.Function.NConvert.ToInt32(txtSPecalNus.Text);
            if (txtInputDoc.Tag != null)
            {
                info.OperInfo.ID = txtInputDoc.Tag.ToString();
            }
            else
            {
                info.OperInfo.ID = "";
            }
            //����Ա 
            if (txtCoordinate.Tag != null)
            {
                info.PackupMan.ID = txtCoordinate.Tag.ToString();
            }
            else
            {
                info.PackupMan.ID = "";
            }
            if (this.txtOperationCode.Tag != null)
            {
                info.OperationCoding.ID = this.txtOperationCode.Tag.ToString();
            }
            else
            {
                info.OperationCoding.ID = "";
            }
            //������ 
            if (cbDisease30.Checked)
            {
                info.Disease30 = "1";
            }
            else
            {
                info.Disease30 = "0";
            }
            info.LendStat = "1"; //��������״̬ 0 Ϊ��� 1Ϊ�ڼ� 
            if (this.frmType == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC && (this.CaseBase.PatientInfo.CaseState == "1" || string.IsNullOrEmpty(this.CaseBase.PatientInfo.CaseState)))
            {
                info.PatientInfo.CaseState = "2";
            }
            else if (this.frmType == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.CAS && (this.CaseBase.PatientInfo.CaseState == "1" || string.IsNullOrEmpty(this.CaseBase.PatientInfo.CaseState))) //������¼�벡��
            {
                info.PatientInfo.CaseState = "3";
            }
            else
            {
                info.PatientInfo.CaseState = this.CaseBase.PatientInfo.CaseState;
            }
            //�Ƿ��в���֢
            info.SyndromeFlag = this.ucDiagNoseInput1.GetSyndromeFlag();
            info.InfectionNum = FS.FrameWork.Function.NConvert.ToInt32(txtInfectNum.Text);  //��Ⱦ����
            if (this.CaseBase.LendStat == null || this.CaseBase.LendStat == "") //��������״̬ 
            {
                info.LendStat = "I";
            }
            else
            {
                info.LendStat = this.CaseBase.LendStat;
            }

            //����ҩ��1
            //if (this.txtPharmacyAllergic1.Tag != null)
            //{
            //    info.FirstAnaphyPharmacy.ID = this.txtPharmacyAllergic1.Tag.ToString();
            //    info.FirstAnaphyPharmacy.Name = this.txtPharmacyAllergic1.Text;
            //}
            //else
            //{
            //    info.FirstAnaphyPharmacy.ID = "";
            //    info.FirstAnaphyPharmacy.Name = "";
            //}
            if (this.neuTxtPharmacyAllergic1.Text != "")
            {
                info.FirstAnaphyPharmacy.ID = this.neuTxtPharmacyAllergic1.Text;
            }
            else
            {
                info.FirstAnaphyPharmacy.ID = "";
            }

            //����ҩ��2
            //if (this.txtPharmacyAllergic2.Tag != null)
            //{
            //    info.SecondAnaphyPharmacy.ID = this.txtPharmacyAllergic2.Tag.ToString();
            //    info.SecondAnaphyPharmacy.Name = this.txtPharmacyAllergic2.Text;
            //}
            //else
            //{
            //    info.SecondAnaphyPharmacy.ID = "";
            //    info.SecondAnaphyPharmacy.Name = "";
            //}
            //if (this.neuTxtPharmacyAllergic2.Text != "")
            //{
            //    info.SecondAnaphyPharmacy.ID = this.neuTxtPharmacyAllergic2.Text;
            //}
            //else
            //{
            //    info.SecondAnaphyPharmacy.ID = "";
            //}
            //��Ⱦ��λ
            info.InfectionPosition.Name = this.txtInfectionPositionNew.Text;

            //�����ж�ԭ��
            if (this.txtInjuryOrPoisoningCause.Tag != null)
            {
                info.InjuryOrPoisoningCauseCode = this.txtInjuryOrPoisoningCause.Tag.ToString();
            }
            info.InjuryOrPoisoningCause = CaseFirstPage.Funtion.ReplaceSingleQuotationMarks(this.txtInjuryOrPoisoningCause.Text,true);

            //�������
            if (this.neuTxtPharmacyAllergic2.Tag != null)
            {
                info.PathologicalDiagCode = this.neuTxtPharmacyAllergic2.Tag.ToString();
            }
            info.PathologicalDiagName = CaseFirstPage.Funtion.ReplaceSingleQuotationMarks(this.neuTxtPharmacyAllergic2.Text,true);
            //��Һ��Ӧ
            if (this.txtReactionTransfuse.Tag != null)
            {
                info.ReactionLiquid = this.txtReactionTransfuse.Tag.ToString();
            }
            else
            {
                info.ReactionLiquid = "";
            }
            //ת����ҽԺ
            info.OutDept.Memo = this.txtComeTo.Text;
            //�������
            info.ClinicDiag.Name = CaseFirstPage.Funtion.ReplaceSingleQuotationMarks(this.txtClinicDiag.Text,true);
            //��Ժ���
            info.InHospitalDiag.Name = CaseFirstPage.Funtion.ReplaceSingleQuotationMarks(this.txtRuyuanDiagNose.Text,true);
            #region �������°汾�ֶ�
            info.BabyAge=CaseBase.BabyAge;
            info.BabyBirthWeight = CaseBase.BabyBirthWeight;
            info.BabyInWeight = CaseBase.BabyInWeight;
            info.CurrentAddr = CaseBase.CurrentAddr;
            info.CurrentPhone = CaseBase.CurrentPhone;
            info.CurrentZip = CaseBase.CurrentZip;
            info.InPath = CaseBase.InPath;
            info.InRoom = CaseBase.InRoom;
            info.OutRoom = CaseBase.OutRoom;
            info.ClinicDiag.ID = CaseBase.ClinicDiag.ID;
            info.ExampleType = CaseBase.ExampleType;
            info.ClinicPath = CaseBase.ClinicPath;
            info.InjuryOrPoisoningCauseCode = CaseBase.InjuryOrPoisoningCauseCode;
            info.PathologicalDiagCode = CaseBase.PathologicalDiagCode;
            info.PathNum = CaseBase.PathNum;
            info.AnaphyFlag = CaseBase.AnaphyFlag;
            info.DutyNurse.ID = CaseBase.DutyNurse.ID;
            info.DutyNurse.Name = CaseBase.DutyNurse.Name;
            //��Ժ��ʽ
            info.Out_Type=CaseBase.Out_Type;
            //ҽ��תԺ����ҽ�ƻ���
            info.HighReceiveHopital=CaseBase.HighReceiveHopital;
            //ҽ��ת����
            info.LowerReceiveHopital=CaseBase.LowerReceiveHopital;
            //��Ժ31������סԺ�ƻ�
            info.ComeBackInMonth = CaseBase.ComeBackInMonth;
            //��Ժ31����סԺĿ��
            info.ComeBackPurpose=CaseBase.ComeBackPurpose;
            //­�����˻��߻���ʱ�� -��Ժǰ ��
            info.OutComeDay=CaseBase.OutComeDay;
            //­�����˻��߻���ʱ�� -��Ժǰ Сʱ
            info.OutComeHour=CaseBase.OutComeHour;
            //­�����˻��߻���ʱ�� -��Ժǰ ����
            info.OutComeMin=CaseBase.OutComeMin;
            //­�����˻��߻���ʱ�� -��Ժ�� ��
            info.InComeDay=CaseBase.InComeDay;
            //­�����˻��߻���ʱ�� -��Ժ�� Сʱ
            info.InComeHour=CaseBase.InComeHour;
            //­�����˻��߻���ʱ�� -��Ժ�� ����
            info.InComeMin = CaseBase.InComeMin;
            #endregion
            #region �������
            //			if( ClinicDiag.Tag != null)
            //			{
            //
            //				if( clinicDiag == null)
            //				{
            //					#region �¼ӵ��������
            //					clinicDiag = new FS.HISFC.Models.HealthRecord.Diagnose();
            //					clinicDiag.DiagInfo.ICD10.ID = ClinicDiag.Tag.ToString();
            //					clinicDiag.DiagInfo.ICD10.Name = ClinicDiag.Text;
            //					clinicDiag.DiagInfo.Patient.ID = CaseBase.PatientInfo.ID;
            //					if(ClinicDocd.Tag != null||CaseBase.PatientInfo.CaseState == "1")
            //					{
            //						clinicDiag.DiagInfo.Doctor.ID = ClinicDocd.Tag.ToString();
            //						clinicDiag.DiagInfo.Doctor.Name = ClinicDocd.Text;
            //					}
            //					else
            //					{
            //						clinicDiag.DiagInfo.Doctor.ID = baseDml.Operator.ID;
            //						clinicDiag.DiagInfo.Doctor.Name = baseDml.Operator.Name;
            //					}
            //					clinicDiag.Pvisit.Date_In = CaseBase.PatientInfo.PVisit.InTime;
            //					clinicDiag.DiagInfo.DiagType.ID = "14";
            //					clinicDiag.DiagInfo.DiagDate = System.DateTime.Now;
            //					//��Ժ��ϵı�־λ  0 Ĭ�ϣ� 1 �޸� ��2 ���룬 3 ɾ�� 
            //					RuDiag = 2 ;
            //					#endregion 
            //				}
            //				else
            //				{
            //					#region �޸ĵ��������
            //					clinicDiag.DiagInfo.ICD10.ID = ClinicDiag.Tag.ToString();
            //					clinicDiag.DiagInfo.ICD10.Name = ClinicDiag.Text;
            //					clinicDiag.DiagInfo.DiagType.ID = "14";
            //					if(clinicDiag.DiagInfo.Patient.ID == null || clinicDiag.DiagInfo.Patient.ID == "")
            //					{
            //						clinicDiag.DiagInfo.Patient.ID = CaseBase.PatientInfo.ID;
            //					}
            //					if(ClinicDocd.Tag != null)
            //					{
            //						clinicDiag.DiagInfo.Doctor.ID = ClinicDocd.Tag.ToString();
            //						clinicDiag.DiagInfo.Doctor.Name = ClinicDocd.Text;
            //					}
            //					else
            //					{
            //						clinicDiag.DiagInfo.Doctor.ID = baseDml.Operator.ID;
            //						clinicDiag.DiagInfo.Doctor.Name = baseDml.Operator.Name;
            //					}
            //					if(clinicDiag.Pvisit.Date_In == System.DateTime.MinValue )
            //					{
            //						clinicDiag.Pvisit.Date_In = CaseBase.PatientInfo.PVisit.InTime;
            //					}
            //					if(clinicDiag.DiagInfo.DiagDate == System.DateTime.MinValue)
            //					{
            //						clinicDiag.DiagInfo.DiagDate = System.DateTime.Now;
            //					}
            //					//��Ժ��ϵı�־λ  0 Ĭ�ϣ� 1 �޸� ��2 ���룬 3 ɾ�� 
            //					RuDiag = 1 ;
            //					#endregion 
            //				}
            //				if(this.frmType == "DOC")
            //				{
            //					clinicDiag.OperType = "1";
            //				}
            //				else if(this.frmType == "CAS")
            //				{
            //					clinicDiag.OperType = "2";
            //				}
            //			}
            //			else  if(ClinicDiag.Tag == null && clinicDiag != null) 
            //			{
            //				RuDiag = 3 ;
            //			}
            //			else
            //			{
            //				RuDiag = 0 ;
            //			}
            #endregion
            #region ��Ժ���
            //			if(RuyuanDiagNose.Tag != null) //����Ժ���
            //			{
            //				if(InDiag == null||CaseBase.PatientInfo.CaseState =="1")
            //				{
            //					#region �¼ӵ���Ժ���
            //					InDiag = new FS.HISFC.Models.HealthRecord.Diagnose();
            //					InDiag.DiagInfo.ICD10.ID = RuyuanDiagNose.Tag.ToString();
            //					InDiag.DiagInfo.ICD10.Name = RuyuanDiagNose.Text;
            //					InDiag.DiagInfo.Patient.ID = CaseBase.PatientInfo.ID;
            //					InDiag.DiagInfo.Doctor.ID = baseDml.Operator.ID;
            //					InDiag.DiagInfo.Doctor.Name = baseDml.Operator.Name;
            //					InDiag.Pvisit.Date_In = CaseBase.PatientInfo.PVisit.InTime;
            //					InDiag.DiagInfo.DiagType.ID = "1";
            //					InDiag.DiagInfo.DiagDate = System.DateTime.Now;
            //
            //					menDiag = 2;
            //					#endregion 
            //				}
            //				else
            //				{
            //					#region �޸ĵ���Ժ���
            //					InDiag.DiagInfo.ICD10.ID = RuyuanDiagNose.Tag.ToString();
            //					InDiag.DiagInfo.ICD10.Name = RuyuanDiagNose.Text.ToString();
            //					InDiag.DiagInfo.DiagType.ID = "1";
            //					if( InDiag.DiagInfo.Patient.ID == null ||InDiag.DiagInfo.Patient.ID == "")
            //					{
            //						InDiag.DiagInfo.Patient.ID = CaseBase.PatientInfo.ID;
            //					}
            //					if(InDiag.DiagInfo.Doctor.ID == null || InDiag.DiagInfo.Doctor.ID == "")
            //					{
            //						InDiag.DiagInfo.Doctor.ID = baseDml.Operator.ID;
            //					}
            //					if(InDiag.DiagInfo.Doctor.Name == null || InDiag.DiagInfo.Doctor.Name == "")
            //					{
            //						InDiag.DiagInfo.Doctor.Name = baseDml.Operator.Name;
            //					}
            //					if(InDiag.Pvisit.Date_In  == System.DateTime.MinValue)
            //					{
            //						InDiag.Pvisit.Date_In = CaseBase.PatientInfo.PVisit.InTime;
            //					}
            //					if(InDiag.DiagInfo.DiagDate == System.DateTime.MinValue)
            //					{
            //						InDiag.DiagInfo.DiagDate = System.DateTime.Now;
            //					}
            //
            //					menDiag = 1;
            //					#endregion 
            //				}
            //				if(this.frmType == "DOC")
            //				{
            //					InDiag.OperType = "1";
            //				}
            //				else if(this.frmType == "CAS")
            //				{
            //					InDiag.OperType = "2";
            //				}
            //			}
            //			else  if(RuyuanDiagNose.Tag == null && InDiag != null) 
            //			{
            //				menDiag = 3;
            //			}
            //			else
            //			{
            //				menDiag = 0;
            //			}
            #endregion
            return 0;
        }
        #endregion

        #region ����סԺ��ˮ�� ���ز�����Ϣ
        /// <summary>
        /// ���ݴ���Ĳ�����Ϣ�Ĳ���״̬,���ز�����Ϣ 
        /// </summary>
        /// <param name="InpatientNo">����סԺ��ˮ��</param>
        /// <param name="Type">����</param>
        /// <returns>-1 �������,����Ĳ�����ϢΪ�� 0 ���˲������в��� 1 �ֹ�¼����Ϣ </returns>
        public int LoadInfo(string InpatientNo, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes Type)
        {
            try
            {
                //add chengym 2011-9-22�л�ǰ�л����������ͬһ������ �������»�ȡ���� save�������¶�CaseBase��ֵ��
                if (TempInpatient == InpatientNo && !this.isNeedLoadInfo)
                {
                    return 1;
                }

                TempInpatient = InpatientNo;
                this.ClearInfo();
                //end add
                if (InpatientNo == null || InpatientNo == "")
                {
                    MessageBox.Show("�����סԺ��ˮ��Ϊ��");
                    return -1;
                }
                FS.HISFC.BizProcess.Integrate.RADT pa = new FS.HISFC.BizProcess.Integrate.RADT();
                FS.HISFC.Models.RADT.PatientInfo patientInfo = new FS.HISFC.Models.RADT.PatientInfo();
                //add by chengym  ��ʵ�г�����ҽ����д��ҳ�������������������ȴ������ȡ����״̬��Ϣ��
                patientInfo = pa.QueryPatientInfoByInpatientNO(InpatientNo);
                this.in_State = patientInfo.PVisit.InState.ID.ToString();
                this.dt_out = patientInfo.PVisit.OutTime;
                this.bedNo = patientInfo.PVisit.PatientLocation.Bed.ID;
                this.dept_out = patientInfo.PVisit.PatientLocation.Dept.ID;
                patientInfo = new FS.HISFC.Models.RADT.PatientInfo();
                //end add chengym
                ArrayList DrgsTimesList = this.con.GetList("CASEDRGSUSEDTIME");
                if (DrgsTimesList != null && DrgsTimesList.Count > 0)
                {
                    DateTime dt = this.con.GetDateTimeFromSysDateTime().Date.AddDays(10);
                    FS.FrameWork.Models.NeuObject usedTime = DrgsTimesList[0] as FS.FrameWork.Models.NeuObject;
                    try
                    {
                        if (usedTime.Memo != "")
                        {
                            dt = FS.FrameWork.Function.NConvert.ToDateTime(usedTime.Memo);
                        }
                    }
                    catch
                    {
                    }
                    if (this.in_State == "I")
                    {
                        if (dt.Date <= this.baseDml.GetDateTimeFromSysDateTime().Date)
                        {
                            MessageBox.Show("�û���Ӧ����д�²�����ҳ��", "��ʾ");
                            return -1;
                        }
                    }
                    else
                    {
                        if (dt.Date <= this.dt_out.Date)
                        {
                            MessageBox.Show("�û���Ӧ����д�²�����ҳ��", "��ʾ");
                            return -1;
                        }
                    }
                }

                CaseBase = baseDml.GetCaseBaseInfo(InpatientNo);

                if (CaseBase == null)
                {
                    CaseBase = new FS.HISFC.Models.HealthRecord.Base();
                    CaseBase.PatientInfo.ID = InpatientNo;
                    CaseBase.PatientInfo.PID.PatientNO = this.PatientNo;
                    CaseBase.PatientInfo.PID.CardNO = this.CardNo;
                }
                #region {E9F858A6-BDBC-4052-BA57-68755055FB80}

                if (string.IsNullOrEmpty(CaseBase.PatientInfo.PID.PatientNO))
                {
                    CaseBase.PatientInfo.PID.PatientNO = this.PatientNo;
                }

                if (string.IsNullOrEmpty(CaseBase.PatientInfo.PID.CardNO))
                {
                    CaseBase.PatientInfo.PID.CardNO = this.CardNo;
                }

                #endregion


                //1. �����������û����Ϣ ��ȥסԺ����ȥ��ѯ
                //2. ��� סԺ�������м�¼����ȡ��Ϣ д��������. 
                if (CaseBase.PatientInfo.ID == "" || CaseBase.OperInfo.OperTime == DateTime.MinValue)//����������û�м�¼
                {
                    #region ������û�м�¼
                    patientInfo = pa.QueryPatientInfoByInpatientNO(InpatientNo);
                    if (patientInfo.ID == "") //סԺ������Ҳû����ػ�����Ϣ
                    {
                        MessageBox.Show("û�в鵽��صĲ�����Ϣ");
                        return 1;
                    }
                    else
                    {
                        CaseBase.PatientInfo = patientInfo;
                    }
                    #endregion
                }
                //������ֹ�¼�벡�� ���ܲ�ѯ��������Ϣ��Ϊ�� ֻ�д����InpatientNo ��Ϊ��
                this.frmType = Type;
                if (CaseBase.PatientInfo.CaseState == "0")
                {
                    MessageBox.Show("�ò��˲������в���");
                    return 0;
                }
                //���没����״̬
                CaseFlag = FS.FrameWork.Function.NConvert.ToInt32(CaseBase.PatientInfo.CaseState);

                #region  ת����Ϣ
                //����ת����Ϣ���б�
                ArrayList changeDept = new ArrayList();
                //��ȡת����Ϣ
                changeDept = deptChange.QueryChangeDeptFromShiftApply(CaseBase.PatientInfo.ID, "2");
                firDept = null;
                secDept = null;
                thirDept = null;
                if (changeDept != null)
                {
                    if (changeDept.Count == 0)
                    {
                        changeDept = deptChange.QueryChangeDeptFromShiftApply(CaseBase.PatientInfo.ID, "1");
                    }
                    //���� 
                    LoadChangeDept(changeDept);
                }
                #endregion
                if (frmType == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC) // ҽ��վ¼�벡��
                {
                    #region  ҽ��վ¼�벡��

                    //Ŀǰ�����в��� ����Ŀǰû��¼�벡��  ���߱�־λλ�գ�Ĭ��������¼�벡���� 
                    if (CaseBase.PatientInfo.CaseState == "1" || CaseBase.PatientInfo.CaseState == null)
                    {
                        //��סԺ�����л�ȡ��Ϣ ����ʾ�ڽ����� 
                        ConvertInfoToPanel(CaseBase);
                        SetReadOnly(false);
                    }
                    // ҽ��վ¼������� 
                    else if (CaseBase.PatientInfo.CaseState == "2")
                    {
                        //�Ӳ����������л�ȡ��Ϣ ����ʾ�ڽ����� 
                        CaseBase = baseDml.GetCaseBaseInfo(CaseBase.PatientInfo.ID);
                        CaseBase.PatientInfo.CaseState = CaseFlag.ToString();
                        if (CaseBase == null)
                        {
                            MessageBox.Show("��ѯ����ʧ��" + baseDml.Err);
                            return -1;
                        }
                        //������� 
                        ConvertInfoToPanel(CaseBase);
                        SetReadOnly(false);
                    }
                    else
                    {
                        // �����Ѿ�����Ѿ�������ҽ���޸�
                        ConvertInfoToPanel(CaseBase);
                        SetReadOnly(true);
                    }

                    #endregion
                }
                else if (frmType == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.CAS)//������¼�벡��
                {
                    #region ������¼�벡��
                    //Ŀǰ�����в��� ����Ŀǰû��¼�벡��  ���߱�־λλ�գ�Ĭ��������¼�벡���� 
                    if (CaseBase.PatientInfo.CaseState == "1" || CaseBase.PatientInfo.CaseState == null)
                    {
                        //��סԺ�����л�ȡ��Ϣ ����ʾ�ڽ����� 
                        ConvertInfoToPanel(CaseBase);
                        SetReadOnly(true);
                    }
                    else if (CaseBase.PatientInfo.CaseState == "2" || CaseBase.PatientInfo.CaseState == "3")
                    {
                        //					//ҽ��վ�Ѿ�¼�벡��
                        ////					list = diag.QueryCaseDiagnose(patientInfo.ID,"%","1");
                        //				}
                        //				else if( patientInfo.Patient.CaseState == "3")
                        //				{
                        //�Ӳ����������л�ȡ��Ϣ ����ʾ�ڽ����� 
                        CaseBase = baseDml.GetCaseBaseInfo(CaseBase.PatientInfo.ID);
                        CaseBase.PatientInfo.CaseState = CaseFlag.ToString();
                        if (CaseBase == null)
                        {
                            MessageBox.Show("��ѯ����ʧ��" + baseDml.Err);
                            return -1;
                        }
                        //������� 
                        ConvertInfoToPanel(CaseBase);
                        SetReadOnly(true);
                    }
                    else if ((CaseBase.PatientInfo.CaseState == "" || CaseBase.PatientInfo.CaseState == null) && CaseBase.IsHandCraft == "1")
                    {
                        //�������
                        ConvertInfoToPanel(CaseBase);
                        SetReadOnly(true);
                    }
                    else
                    {
                        //�����Ѿ���� �������޸ġ�
                        //					MessageBox.Show("�����Ѿ����,�������޸�");
                        ConvertInfoToPanel(CaseBase);
                        this.SetReadOnly(true); //��Ϊֻ�� 
                    }
                    this.ucDiagNoseInput1.SetReadOnly(false);
                    #endregion
                }
                else
                {
                    //û�д������ �����κδ���
                }
                #region ���
                this.ucDiagNoseInput1.LoadInfo(CaseBase.PatientInfo, frmType);
                //LoadDiag(this.ucDiagNoseInput1.diagList);
                #endregion
                #region  ��Ӥ��
                this.ucBabyCardInput1.LoadInfo(CaseBase.PatientInfo);
                #endregion
                #region ����
                this.ucOperation1.LoadInfo(CaseBase.PatientInfo, frmType);
                #endregion
                #region  ����
                this.ucTumourCard2.LoadInfo(CaseBase.PatientInfo, frmType);
                #endregion
                #region ת��
                this.ucChangeDept1.LoadInfo(CaseBase.PatientInfo, changeDept);
                #endregion
                #region  ����
                if (CaseBase.IsHandCraft == "1") //�ֹ�¼�벡��
                {
                    //�������޸�
                    this.ucFeeInfo1.BoolType = true;
                }
                else
                {
                    //�������޸�
                    this.ucFeeInfo1.BoolType = false;
                }
                this.ucFeeInfo1.LoadInfo(CaseBase.PatientInfo);
                #endregion

                #region ������ϵ���б�

                #region {E9F858A6-BDBC-4052-BA57-68755055FB80}


                InitLinkWay(CaseBase.PatientInfo.PID.PatientNO, CaseBase.PatientInfo.PID.CardNO);

                #endregion

                #endregion

                //��ʾ������Ϣ
                this.tab1.SelectedIndex = 0;
                ////סԺ��
                //this.txtPatientNOSearch.Focus();
                return 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }
        }

        void ucPatient_SelectItem(FS.HISFC.Models.HealthRecord.Base HealthRecord)
        {
            LoadInfo(HealthRecord.PatientInfo.ID, this.frmType);
            this.isNeedLoadInfo = true;//add chengym 2011-9-23
        }
        #endregion

        #region ˽���¼�
        private void txtCaseNum_Leave(object sender, EventArgs e)
        {
            if (txtCaseNum.Text.Trim() == "")
            {
                return;
            }
            this.txtCaseNum.Text = txtCaseNum.Text.Trim().PadLeft(10, '0');
        }
        private void deptThird_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtInfectionPositionNew.Focus();
            }
        }
        private void pactKind_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                txtSSN.Focus();
            }
        }
        private void deptOut_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtClinicDiag.Focus();
            }
        }
        private void patientBirthday_ValueChanged(object sender, System.EventArgs e)
        {
            //if (dtPatientBirthday.Value > System.DateTime.Now)
            //{
            //    dtPatientBirthday.Value = System.DateTime.Now;
            //}
            //if (dtPatientBirthday.Value.Year == System.DateTime.Now.Year)
            //{
            //    if (dtPatientBirthday.Value.Month == System.DateTime.Now.Month)
            //    {
            //        System.TimeSpan span = System.DateTime.Now - dtPatientBirthday.Value;
            //        if (span.Days != 0) //��
            //        {
            //            txtPatientAge.Text = span.Days.ToString();
            //            cmbUnit.Text = "��";
            //        }
            //        else
            //        {
            //            txtPatientAge.Text = span.Hours.ToString();
            //            cmbUnit.Text = "Сʱ";
            //        }
            //    }
            //    else //��
            //    {
            //        txtPatientAge.Text = Convert.ToString(System.DateTime.Now.Month - dtPatientBirthday.Value.Month);
            //        cmbUnit.Text = "��";
            //    }
            //}
            //else //��
            //{
            //    txtPatientAge.Text = Convert.ToString(System.DateTime.Now.Year - dtPatientBirthday.Value.Year);
            //    cmbUnit.Text = "��";
            //}
        }
        private void txtPatientNOSearch_Enter(object sender, EventArgs e)
        {
            this.ucPatient.Location = new Point(this.txtPatientNOSearch.Location.X, this.txtPatientNOSearch.Location.Y + this.txtPatientNOSearch.Height + 2);
            this.ucPatient.BringToFront();
            this.ucPatient.Visible = false;
        }
        private void txtCaseNOSearch_Enter(object sender, EventArgs e)
        {
            this.ucPatient.Location = new Point(this.txtCaseNOSearch.Location.X, this.txtCaseNOSearch.Location.Y + this.txtCaseNOSearch.Height + 2);
            this.ucPatient.BringToFront();
            this.ucPatient.Visible = false;
        }
        private void Date_Out_ValueChanged(object sender, System.EventArgs e)
        {
            if (txtDateOut.Value < this.dtDateIn.Value)
            {
                txtDateOut.Value = dtDateIn.Value;
            }
            System.TimeSpan tt = this.txtDateOut.Value.Date - this.dtDateIn.Value.Date;
            this.txtPiDays.Text = Convert.ToString(tt.Days);
        }

        #endregion

        #region �س��¼�
        private void txtBC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtECTNumb.Focus();
            }
        }
        private void cmbUnit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                txtMaritalStatus.Focus();
            }
        }


        private void label1_Click(object sender, EventArgs e)
        {
            if (this.label1.Text == "�� �� ��")
            {
                label1.Text = "ס Ժ ��";
            }
            else if (this.label1.Text == "ס Ժ ��")
            {
                label1.Text = "�� �� ��";
            }
        }
        private void caseNum_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                txtInTimes.Focus();
            }
            else if (e.KeyData == Keys.Divide)
            {
                return;
            }
        }


        private void SSN_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtClinicNo.Focus();
            }
        }

        private void PatientName_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtPatientSex.Focus();
            }
        }

        private void clinicNo_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtPatientName.Focus();
            }
        }

        private void patientBirthday_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            //string age= FS.FrameWork.Public.String.GetAge(this.dtPatientBirthday.Value, baseDml.GetDateTimeFromSysDateTime());
            //this.txtPatientAge.Text = age.Substring(0, age.Length - 1);
            //this.cmbUnit.Text = age.Substring(age.Length - 1);
            if (e.KeyData == Keys.Enter)
            {
                this.txtMaritalStatus.Focus();
            }
        }

        private void PatientAge_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.cmbUnit.Focus();
            }
        }

        private void DIST_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtIDNo.Focus();
            }
        }

        private void IDNo_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtAddressBusiness.Focus();
            }
        }
        private void BusinessZip_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtPhoneBusiness.Focus();
            }
        }

        private void PhoneBusiness_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtAddressHome.Focus();
            }
        }

        private void AddressHome_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtHomeZip.Focus();
            }
        }
        private void HomeZip_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtPhoneHome.Focus();
            }
        }
        private void AddressBusiness_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtBusinessZip.Focus();
            }
        }

        private void PhoneHome_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtKin.Focus();
            }
        }

        private void Kin_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtRelation.Focus();
            }
        }

        private void LinkmanTel_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtLinkmanAdd.Focus();
            }
        }

        private void LinkmanAdd_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtNomen.Focus();

            }
        }
        private void txtNomen_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.dtDateIn.Focus();
            }
        }
        private void Date_In_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                //				System.TimeSpan tt = this.Date_Out.Value - this.Date_In.Value;
                //				this.PiDays.Text = Convert.ToString(tt.Days+1);
                this.txtDeptInHospital.Focus();
            }
        }
        private void dateTimePicker3_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtFirstDept.Focus();
            }
        }

        private void dateTimePicker4_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtDeptSecond.Focus();
            }
        }

        private void dtThird_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtDeptThird.Focus();
            }
        }

        private void Date_Out_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                System.TimeSpan tt = this.txtDateOut.Value.Date - this.dtDateIn.Value.Date;
                this.txtPiDays.Text = Convert.ToString(tt.Days);
                this.txtDeptOut.Focus();
            }
        }

        private void DiagDate_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtRuyuanDiagNose.Focus();
            }
        }

        private void PiDays_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtDiagDate.Focus();
            }
        }

        private void ComeFrom_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.dtFirstTime.Focus();
            }
        }

        private void Nomen_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtInAvenue.Focus();
            }
        }

        private void infectNum_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtPharmacyAllergic1.Focus();
            }
        }

        private void CheckDate_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtMrQual.Focus();
            }
        }

        private void YnFirst_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.NumPad1)
            {
                cbYnFirst.Checked = !cbYnFirst.Checked;
                this.cbVisiStat.Focus();
            }
            else if (e.KeyData == Keys.Enter)
            {
                this.cbVisiStat.Focus();
            }
        }

        private void VisiStat_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.NumPad1)
            {
                cbVisiStat.Checked = !cbVisiStat.Checked;
                this.txtVisiPeriWeek.Focus();
            }
            else if (e.KeyData == Keys.Enter)
            {
                this.txtVisiPeriWeek.Focus();
            }
        }

        private void VisiPeriWeek_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtVisiPeriMonth.Focus();
            }
        }

        private void VisiPeriMonth_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtVisiPeriYear.Focus();
            }
        }

        private void VisiPeriYear_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.cbTechSerc.Focus();
            }
        }

        private void TechSerc_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.NumPad1)
            {
                cbTechSerc.Checked = !cbTechSerc.Checked;
                this.cbDisease30.Focus();
            }
            else if (e.KeyData == Keys.Enter)
            {
                this.cbDisease30.Focus();
            }
        }

        private void BloodRed_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                txtBloodPlatelet.Focus();
            }
        }

        private void BloodPlatelet_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                txtBodyAnotomize.Focus();
            }
        }

        private void BodyAnotomize_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtBloodWhole.Focus();
            }
        }

        private void BloodWhole_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtBloodOther.Focus();
            }
        }

        private void BloodOther_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtInconNum.Focus();
            }
        }

        private void InconNum_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtOutconNum.Focus();
            }
        }

        private void outOutconNum_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtSuperNus.Focus();
            }
        }

        private void SuperNus_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtINus.Focus();
            }
        }

        private void INus_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtIINus.Focus();
            }
        }

        private void IINus_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtIIINus.Focus();
            }
        }

        private void IIINus_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtStrictNuss.Focus();
            }
        }

        private void StrictNuss_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtSPecalNus.Focus();
            }
        }

        private void SPecalNus_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtInfectionDiseasesReport.Focus();
            }
        }

        private void CtNumb_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                txtPathNumb.Focus();
            }
        }

        private void textBox54_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtMriNumb.Focus();
            }
        }

        private void MriNumb_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtXNumb.Focus();
            }
        }

        private void XNumb_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtBC.Focus();
            }
        }

        private void checkBox9_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtInputDoc.Focus();
            }
        }

        private void SalvTimes_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtSuccTimes.Focus();
            }
        }

        private void SuccTimes_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtCtNumb.Focus();
            }
        }

        private void BodyCheck_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.cbYnFirst.Focus();
            }
            else if (e.KeyData == Keys.NumPad1)
            {
                this.cbBodyCheck.Checked = !this.cbBodyCheck.Checked;
                this.cbYnFirst.Focus();
            }
        }

        private void checkBox8_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtBloodType.Focus();
            }
            else if (e.KeyData == Keys.NumPad1)
            {
                this.cbBodyCheck.Checked = !this.cbBodyCheck.Checked;
                this.txtBloodType.Focus();
            }
        }

        private void Date_In_Leave(object sender, System.EventArgs e)
        {
            System.TimeSpan tt = this.txtDateOut.Value.Date - this.dtDateIn.Value.Date;
            this.txtPiDays.Text = Convert.ToString(tt.Days);
        }

        private void Date_Out_Leave(object sender, System.EventArgs e)
        {
            System.TimeSpan tt = this.txtDateOut.Value.Date - this.dtDateIn.Value.Date;
            this.txtPiDays.Text = Convert.ToString(tt.Days);
        }
        private void OperationCOde_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtInputDoc.Focus();
            }
        }

        private void txtInfectionPosition_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtInjuryOrPoisoningCause.Focus();
            }
        }

        private void txtPharmacyAllergic1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtPharmacyAllergic2.Focus();
            }

        }

        private void txtPharmacyAllergic2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtHbsag.Focus();
            }

        }

        private void txtECTNumb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtPETNumb.Focus();
            }
        }

        private void txtPETNumb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.cbBodyCheck.Focus();
            }
        }

        #endregion

        #region ���� �������
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Divide)
            {
                if (this.tab1.SelectedIndex != 6)
                {
                    this.tab1.SelectedIndex++;
                }
                else
                {
                    this.tab1.SelectedIndex = 0;
                }
            }
            else if (keyData == Keys.Escape)
            {
                this.ucPatient.Visible = false;
            }
            return base.ProcessDialogKey(keyData);
        }
        #endregion

        #region ���� ��Ժ��Ϻ��������
        /// <summary>
        /// ���� ��Ժ��Ϻ��������
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private int LoadDiag(ArrayList list)
        {
            if (list == null)
            {
                return -1;
            }
            clinicDiag = null;
            InDiag = null;
            #region ��Ĭ������һ�����������
            foreach (FS.HISFC.Models.HealthRecord.Diagnose obj in list)
            {
                if (obj.DiagInfo.DiagType.ID == "10" && obj.DiagInfo.IsMain)
                {	//������� 
                    this.txtClinicDiag.Tag = obj.DiagInfo.ICD10.ID;
                    this.txtClinicDiag.Text = obj.DiagInfo.ICD10.Name;
                    this.txtClinicDocd.Tag = obj.DiagInfo.Doctor.ID;
                    this.txtClinicDocd.Text = obj.DiagInfo.Doctor.Name;
                    clinicDiag = obj;
                }
                else if (obj.DiagInfo.DiagType.ID == "11" && obj.DiagInfo.IsMain)
                {	//��Ժ���
                    txtRuyuanDiagNose.Tag = obj.DiagInfo.ICD10.ID;
                    txtRuyuanDiagNose.Text = obj.DiagInfo.ICD10.Name;
                    InDiag = obj;
                }
            }
            #endregion

            #region ���û������� ���������������
            foreach (FS.HISFC.Models.HealthRecord.Diagnose obj in list)
            {
                if (obj.DiagInfo.DiagType.ID == "10")
                {	//������� 
                    if (this.txtClinicDiag.Tag == null)
                    {
                        this.txtClinicDiag.Tag = obj.DiagInfo.ICD10.ID;
                        this.txtClinicDiag.Text = obj.DiagInfo.ICD10.Name;
                        this.txtClinicDocd.Tag = obj.DiagInfo.Doctor.ID;
                        this.txtClinicDocd.Text = obj.DiagInfo.Doctor.Name;
                        clinicDiag = obj;
                    }
                }
                else if (obj.DiagInfo.DiagType.ID == "11")
                {	//��Ժ���
                    if (txtRuyuanDiagNose.Tag == null)
                    {
                        txtRuyuanDiagNose.Tag = obj.DiagInfo.ICD10.ID;
                        txtRuyuanDiagNose.Text = obj.DiagInfo.ICD10.Name;
                        InDiag = obj;
                    }
                }
            }
            #endregion
            return 0;
        }
        #endregion

        #region �������ݵĺϷ���
        /// <summary>
        /// �������ݵĺϷ���
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private int ValidState(FS.HISFC.Models.HealthRecord.Base info)
        {
            #region  У��
            if (txtDeptInHospital.Text == "" && txtDeptOut.Text != "")
            {
                MessageBox.Show("������д��Ժ������Ϣ");
                txtDeptOut.Focus();
                return -1;
            }
            if (txtDeptInHospital.Text == "" && txtFirstDept.Text != "")
            {
                MessageBox.Show("������д��Ժ������Ϣ");
                txtDeptInHospital.Focus();
                return -1;
            }
            if (txtFirstDept.Text == "" && txtDeptSecond.Text != "")
            {
                MessageBox.Show("������д��һ��ת����Ϣ");
                txtFirstDept.Focus();
                return -1;
            }
            if (txtDeptSecond.Text == "" && txtDeptThird.Text != "")
            {
                MessageBox.Show("������д�ڶ���ת����Ϣ");
                txtDeptSecond.Focus();
                return -1;
            }
            if (dtFirstTime.Value.Date < this.dtDateIn.Value.Date)
            {
                MessageBox.Show("��һ��ת��ʱ�䲻��С����Ժʱ��");
                dtFirstTime.Focus();
                return -1;
            }
            if (dtFirstTime.Value.Date > this.txtDateOut.Value.Date)
            {
                MessageBox.Show("��һ��ת��ʱ�䲻�ܴ����ڳ�Ժʱ��");
                dtFirstTime.Focus();
                return -1;
            }
            if ((dtFirstTime.Value.Date > dtSecond.Value.Date) && txtDeptSecond.Text.Trim() != string.Empty)
            {
                MessageBox.Show("��һ��ת��ʱ�䲻�ܴ��ڵڶ���ת��ʱ��");
                dtFirstTime.Focus();
                return -1;
            }
            if ((dtSecond.Value.Date > dtThird.Value.Date) && txtDeptThird.Text.Trim() != string.Empty)
            {
                MessageBox.Show("�ڶ���ת��ʱ�䲻�ܴ��ڵ�����ת��ʱ��");
                dtSecond.Focus();
                return -1;
            }
            #endregion
            if (info.PatientInfo.Pact.ID == null)
            {
                this.txtPactKind.Focus();
                MessageBox.Show("��ѡ��������");
                return -1;
            }
            if (info.PatientInfo.Pact.ID == "")
            {
                this.txtPactKind.Focus();
                MessageBox.Show("��ѡ��������");
                return -1;
            }
            if (!ValidMaxLengh(info.PatientInfo.ID, 14))
            {
                MessageBox.Show("סԺ��ˮ�Ź���");
                return -1;
            }
            //if (!ValidMaxLengh(info.PatientInfo.PID.PatientNO, 10))
            //{
            //    txtCaseNum.Focus();
            //    MessageBox.Show("סԺ�Ź���");
            //    return -1;
            //}
            if (!ValidMaxLengh(info.CaseNO, 10))
            {
                txtCaseNum.Focus();
                MessageBox.Show("�����Ź���");
                return -1;
            }
            if (!ValidMaxLengh(info.PatientInfo.PID.CardNO, 10))
            {
                txtClinicNo.Focus();
                MessageBox.Show("���￨�Ź���");
                return -1;
            }
            if (!ValidMaxLengh(info.PatientInfo.SSN, 18))
            {
                txtSSN.Focus();
                MessageBox.Show("ҽ���Ź���");
                return -1;
            }
            if (!ValidMaxLengh(info.PatientInfo.PID.CardNO, 10))
            {
                txtSSN.Focus();
                MessageBox.Show("���Ź���");
                return -1;
            }
            if (!ValidMaxLengh(info.PatientInfo.Name, 20))
            {
                txtPatientName.Focus();
                MessageBox.Show("��������");
                return -1;
            }
            if (!ValidMaxLengh(info.Nomen, 20))
            {
                txtNomen.Focus();
                MessageBox.Show("����������");
                return -1;
            }
            if (info.PatientInfo.Sex.ID != null)
            {
                if (!ValidMaxLengh(info.PatientInfo.Sex.ID.ToString(), 20))
                {
                    txtPatientSex.Focus();
                    MessageBox.Show("�Ա�������");
                    return -1;
                }
            }
            if (!ValidMaxLengh(info.PatientInfo.Country.ID, 20))
            {
                txtCountry.Focus();
                MessageBox.Show("�����������");
                return -1;
            }

            if (!ValidMaxLengh(info.PatientInfo.Nationality.ID, 20))
            {
                txtNationality.Focus();
                MessageBox.Show("����������");
                return -1;
            }
            if (!ValidMaxLengh(info.PatientInfo.Profession.ID, 20))
            {
                txtProfession.Focus();
                MessageBox.Show("ְҵ�������");
                return -1;
            }
            if (info.PatientInfo.BloodType.ID != null)
            {
                if (!ValidMaxLengh(info.PatientInfo.BloodType.ID.ToString(), 20))
                {
                    txtBloodType.Focus();
                    MessageBox.Show("Ѫ�ͱ������");
                    return -1;
                }
            }
            if (info.PatientInfo.MaritalStatus.ID != null)
            {
                if (!ValidMaxLengh(info.PatientInfo.MaritalStatus.ID.ToString(), 10))
                {
                    txtMaritalStatus.Focus();
                    MessageBox.Show("�����������");
                    return -1;
                }
            }
            if (info.AgeUnit != null)
            {
                if (!ValidMaxLengh(info.AgeUnit, 10))
                {
                    txtPatientAge.Focus();
                    MessageBox.Show("���䵥λ����");
                    return -1;
                }
            }

            if (!ValidMaxLengh(info.PatientInfo.Age, 3))
            {
                txtPatientAge.Focus();
                MessageBox.Show("�������");
                return -1;
            }
            if (!ValidMaxLengh(info.PatientInfo.IDCard, 18))
            {
                txtIDNo.Focus();
                MessageBox.Show("���֤����");
                return -1;
            }
            //			if(info.PatientInfo.PVisit.InSource.ID.Length  > 1 )
            //			{
            //				In.Focus();
            //				MessageBox.Show("������Դ�������");
            //				return -1;
            //			} 
            if (!ValidMaxLengh(info.PatientInfo.Pact.PayKind.ID, 20))
            {
                txtPactKind.Focus();
                MessageBox.Show("�������������");
                return -1;
            }

            if (!ValidMaxLengh(info.PatientInfo.Pact.ID, 20))
            {
                txtPactKind.Focus();
                MessageBox.Show("��ͬ��λ�������");
                return -1;
            }
            if (info.PatientInfo.Pact.ID == null || info.PatientInfo.Pact.ID == "")
            {
                txtPactKind.Focus();
                MessageBox.Show("��ͬ��λ���벻��Ϊ��");
                return -1;
            }
            if (!ValidMaxLengh(info.PatientInfo.SSN, 18))
            {
                txtSSN.Focus();
                MessageBox.Show("ҽ�����ѺŹ���");
                return -1;
            }

            if (!ValidMaxLengh(info.PatientInfo.DIST, 50))
            {
                txtDIST.Focus();
                MessageBox.Show("�������");
                return -1;
            }

            if (!ValidMaxLengh(info.PatientInfo.AddressHome, 50))
            {
                txtAddressHome.Focus();
                MessageBox.Show("��ͥסַ����");
                return -1;
            }

            if (!ValidMaxLengh(info.PatientInfo.PhoneHome, 25))
            {
                txtPhoneHome.Focus();
                MessageBox.Show("��ͥ�绰����");
                return -1;
            }

            if (!ValidMaxLengh(info.PatientInfo.HomeZip, 25))
            {
                txtHomeZip.Focus();
                MessageBox.Show("סַ�ʱ����");
                return -1;
            }

            if (!ValidMaxLengh(info.PatientInfo.AddressBusiness, 50))
            {
                txtAddressBusiness.Focus();
                MessageBox.Show("��λ��ַ����");
                return -1;
            }
            if (!ValidMaxLengh(info.PatientInfo.PhoneBusiness, 25))
            {
                txtPhoneBusiness.Focus();
                MessageBox.Show("��λ�绰����");
                return -1;
            }
            if (!ValidMaxLengh(info.PatientInfo.BusinessZip, 6))
            {
                txtBusinessZip.Focus();
                MessageBox.Show("��λ�ʱ����");
                return -1;
            }
            if (!ValidMaxLengh(info.PatientInfo.Kin.Name, 10))
            {
                txtKin.Focus();
                MessageBox.Show("��ϵ�˹���");
                return -1;
            }
            if (!ValidMaxLengh(info.PatientInfo.Kin.RelationLink, 20))
            {
                txtRelation.Focus();
                MessageBox.Show("��ϵ�˹�ϵ����");
                return -1;
            }
            if (!ValidMaxLengh(info.PatientInfo.Kin.RelationAddress, 50))
            {
                txtLinkmanAdd.Focus();
                MessageBox.Show("��ϵ��ַ����");
                return -1;
            }
            if (!ValidMaxLengh(info.PatientInfo.Kin.RelationPhone, 25))
            {
                txtLinkmanTel.Focus();
                MessageBox.Show("��ϵ�绰����");
                return -1;
            }
            if (!ValidMaxLengh(info.ComeFrom, 100))
            {
                txtComeFrom.Focus();
                MessageBox.Show("ת��ҽԺ");
                return -1;
            }
            if (info.PatientInfo.InTimes > 99)
            {
                txtComeFrom.Focus();
                MessageBox.Show("��Ժ��������");
                return -1;
            }
            if (info.SalvTimes > 99)
            {
                txtSalvTimes.Focus();
                MessageBox.Show("���ȴ�������");
                return -1;
            }
            if (info.InfectionNum > 99)
            {
                txtInfectNum.Focus();
                MessageBox.Show("�ɹ���������");
                return -1;
            }
            if (!ValidMaxLengh(info.VisiPeriodWeek, 6))
            {
                txtVisiPeriWeek.Focus();
                MessageBox.Show("���������������");
                return -1;
            }
            if (!ValidMaxLengh(info.VisiPeriodMonth, 6))
            {
                txtVisiPeriMonth.Focus();
                MessageBox.Show("���������������");
                return -1;
            }
            if (!ValidMaxLengh(info.VisiPeriodYear, 6))
            {
                txtVisiPeriYear.Focus();
                MessageBox.Show("���������������");
                return -1;
            }
            if (!ValidMaxLengh(info.BloodRed, 10))
            {
                txtBloodRed.Focus();
                MessageBox.Show("��ϸ����������");
                return -1;
            }
            if (!ValidMaxLengh(info.BloodPlatelet, 10))
            {
                txtBloodPlatelet.Focus();
                MessageBox.Show("ѪС����������");
                return -1;
            }
            if (!ValidMaxLengh(info.BloodPlasma, 10))
            {
                txtBodyAnotomize.Focus();
                MessageBox.Show("Ѫ����������");
                return -1;
            }
            if (!ValidMaxLengh(info.BloodWhole, 10))
            {
                txtBloodWhole.Focus();
                MessageBox.Show("ȫѪ��������");
                return -1;
            }
            if (!ValidMaxLengh(info.BloodOther, 10))
            {
                txtBloodOther.Focus();
                MessageBox.Show("������Ѫ��������");
                return -1;
            }
            if (info.InconNum > 99)
            {
                txtInconNum.Focus();
                MessageBox.Show("Ժ�ʻ����������");
                return -1;
            }
            if (info.OutconNum > 99)
            {
                txtOutconNum.Focus();
                MessageBox.Show("Զ�̴�����������");
                return -1;
            }
            if (info.SpecalNus > 9999)
            {
                txtSuperNus.Focus();
                MessageBox.Show("���⻤����������");
                return -1;
            }
            if (info.INus > 9999)
            {
                txtINus.Focus();
                MessageBox.Show("I������ʱ����������");
                return -1;
            }
            if (info.IINus > 9999)
            {
                txtIINus.Focus();
                MessageBox.Show("II������ʱ����������");
                return -1;
            }
            if (info.IIINus > 9999)
            {
                txtIIINus.Focus();
                MessageBox.Show("III������ʱ����������");
                return -1;
            }
            if (info.StrictNuss > 9999)
            {
                txtStrictNuss.Focus();
                MessageBox.Show("��֢�໤ʱ����������");
                return -1;
            }
            if (info.SuperNus > 9999)
            {
                txtSuperNus.Focus();
                MessageBox.Show("�ؼ�����ʱ����������");
                return -1;
            }
            if (!ValidMaxLengh(info.CtNum, 10))
            {
                txtCtNumb.Focus();
                MessageBox.Show("CT�Ź���");
                return -1;
            }
            if (!ValidMaxLengh(info.XNum, 10))
            {
                txtXNumb.Focus();
                MessageBox.Show("X��Ź���");
                return -1;
            }
            if (!ValidMaxLengh(info.MriNum, 10))
            {
                txtMriNumb.Focus();
                MessageBox.Show("M R �Ź���");
                return -1;
            }
            if (!ValidMaxLengh(info.PathNum, 10))
            {
                txtPathNumb.Focus();
                MessageBox.Show("UFCT �Ź���");
                return -1;
            }
            //add by 2011-9-20 chengym
            if (info.SalvTimes > 0 && info.SalvTimes < info.SuccTimes)
            {
                MessageBox.Show("���ȳɹ�������Ӧ�������ȴ���", "��ʾ");
                txtSuccTimes.Focus();
                return -1;
            }
            if (info.PatientInfo.PVisit.ReferringDoctor.ID == "" && info.PatientInfo.PVisit.ConsultingDoctor.ID == "" && info.PatientInfo.PVisit.AttendingDoctor.ID == "")
            {
                MessageBox.Show("�����Ρ�����ҽʦ������ҽ��������Ϊ��");
                return -1;
            }
            //if (info.CePi == "" || info.PiPo == "")
            //{
            //    MessageBox.Show("�������Ժ����Ժ���Ժ����Ϸ����������Ϊ��");
            //    txtCePi.Focus();
            //    return -1;
            //}
            if (info.PatientInfo.PVisit.OutTime.Date < info.PatientInfo.PVisit.InTime.Date)
            {
                MessageBox.Show("����Ժ���ڡ�����С�ڡ���Ժʱ�䡱");
                return -1;
            }
            if (txtCheckDate.Value.Date > info.PatientInfo.PVisit.OutTime.Date.AddDays(15))
            {
                this.txtCheckDate.Focus();
                MessageBox.Show("���ʿ����ڡ����ܳ�����Ժʱ��15��");
                return -1;
            }
            if (info.DiagDate.Date < info.PatientInfo.PVisit.InTime.Date)
            {
                MessageBox.Show("��ȷ�����ڡ�����С����Ժ����");
                this.txtDiagDate.Focus();
                return -1;
            }
            if (info.DiagDate.Date > info.PatientInfo.PVisit.OutTime.Date)
            {
                MessageBox.Show("��ȷ�����ڡ����ܴ��ڳ�Ժ����");
                this.txtDiagDate.Focus();
                return -1;
            }
            return 1;
        }
        #region ��ȡ���ֵ
        /// <summary>
        /// ��ȡ���ֵ
        /// </summary>
        /// <param name="str"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        private bool ValidMaxLengh(string str, int length)
        {
            return FS.FrameWork.Public.String.ValidMaxLengh(str, length);
        }
        #endregion
        #endregion

        #region ����Ϊֻ��
        /// <summary>
        /// ����Ϊֻ��
        /// </summary>
        /// <param name="type"></param> 
        public void SetReadOnly(bool type)
        {
            this.ucDiagNoseInput1.SetReadOnly(type);
            this.ucOperation1.SetReadOnly(type);
            this.ucTumourCard2.SetReadOnly(type);
            if (this.changeDeptType == 1) //�Զ�����ת����Ϣ
            {
                this.ucChangeDept1.SetReadOnly(true);
            }
            else
            {
                this.ucChangeDept1.SetReadOnly(type);
            }
            this.ucBabyCardInput1.SetReadOnly(type);
            //������ 
            txtCaseNum.ReadOnly = type;
            txtCaseNum.BackColor = System.Drawing.Color.White;
            //סԺ����
            txtInTimes.ReadOnly = type;
            txtInTimes.BackColor = System.Drawing.Color.White;
            //�������
            txtPactKind.ReadOnly = type;
            txtPactKind.EnterVisiable = !type;
            txtPactKind.BackColor = System.Drawing.Color.White;
            //ҽ����
            txtSSN.ReadOnly = type;
            txtSSN.BackColor = System.Drawing.Color.White;
            //�����
            txtClinicNo.ReadOnly = type;
            txtClinicNo.BackColor = System.Drawing.Color.White;
            //����
            txtPatientName.ReadOnly = type;
            txtPatientName.BackColor = System.Drawing.Color.White;
            //�Ա�
            txtPatientSex.ReadOnly = type;
            txtPatientSex.EnterVisiable = !type;
            txtPatientSex.BackColor = System.Drawing.Color.White;
            //����
            dtPatientBirthday.Enabled = !type;
            //����
            txtPatientAge.ReadOnly = true;
            txtPatientAge.BackColor = System.Drawing.Color.White;
            //����
            txtMaritalStatus.ReadOnly = type;
            txtMaritalStatus.EnterVisiable = !type;
            txtMaritalStatus.BackColor = System.Drawing.Color.White;
            //ְҵ
            txtProfession.ReadOnly = type;
            txtProfession.EnterVisiable = !type;
            txtProfession.BackColor = System.Drawing.Color.White;
            //������
            txtAreaCode.ReadOnly = type;
            txtAreaCode.BackColor = System.Drawing.Color.White;
            //����
            txtNationality.ReadOnly = type;
            txtNationality.EnterVisiable = !type;
            txtNationality.BackColor = System.Drawing.Color.White;
            //����
            txtCountry.ReadOnly = type;
            txtCountry.EnterVisiable = !type;
            txtCountry.BackColor = System.Drawing.Color.White;
            //����
            txtDIST.ReadOnly = type;
            txtDIST.BackColor = System.Drawing.Color.White;
            //���֤
            txtIDNo.ReadOnly = type;
            txtIDNo.BackColor = System.Drawing.Color.White;
            //������λ
            txtAddressBusiness.ReadOnly = type;
            txtAddressBusiness.BackColor = System.Drawing.Color.White;
            //��λ�ʱ�
            txtBusinessZip.ReadOnly = type;
            txtBusinessZip.BackColor = System.Drawing.Color.White;
            //��λ�绰
            txtPhoneBusiness.ReadOnly = type;
            txtPhoneBusiness.BackColor = System.Drawing.Color.White;
            //���ڵ�ַ
            txtAddressHome.ReadOnly = type;
            txtAddressHome.BackColor = System.Drawing.Color.White;
            //�����ʱ�
            txtHomeZip.ReadOnly = type;
            txtHomeZip.BackColor = System.Drawing.Color.White;
            //��ͥ�绰
            txtPhoneHome.ReadOnly = type;
            txtPhoneHome.BackColor = System.Drawing.Color.White;
            //��ϵ�� 
            txtKin.ReadOnly = type;
            txtKin.BackColor = System.Drawing.Color.White;
            //��ϵ
            txtRelation.ReadOnly = type;
            txtRelation.EnterVisiable = !type;
            txtRelation.BackColor = System.Drawing.Color.White;
            //��ϵ�绰
            txtLinkmanTel.ReadOnly = type;
            txtLinkmanTel.BackColor = System.Drawing.Color.White;
            //l��ϵ�˵�ַ
            txtLinkmanAdd.ReadOnly = type;
            txtLinkmanAdd.BackColor = System.Drawing.Color.White;
            //��Ժ����
            txtDeptInHospital.ReadOnly = type;
            txtDeptInHospital.EnterVisiable = !type;
            txtDeptInHospital.BackColor = System.Drawing.Color.White;
            //��Ժʱ�� 
            dtDateIn.Enabled = !type;
            //��Ժ���
            txtCircs.ReadOnly = type;
            txtCircs.EnterVisiable = !type;
            txtCircs.BackColor = System.Drawing.Color.White;
            //ת�����
            txtFirstDept.ReadOnly = type;
            txtFirstDept.EnterVisiable = !type;
            txtFirstDept.BackColor = System.Drawing.Color.White;
            //ת��ʱ��
            dtFirstTime.Enabled = !type;
            dtFirstTime.BackColor = System.Drawing.Color.White;
            //ת�����
            txtDeptSecond.ReadOnly = type;
            txtDeptSecond.EnterVisiable = !type;
            txtDeptSecond.BackColor = System.Drawing.Color.White;
            //ת��ʱ��
            dtSecond.Enabled = !type;
            //ת�����
            txtDeptThird.ReadOnly = type;
            txtDeptThird.EnterVisiable = !type;
            txtDeptThird.BackColor = System.Drawing.Color.White;
            //ת��ʱ��
            dtThird.Enabled = !type;
            //��Ժ����
            txtDeptOut.ReadOnly = type;
            txtDeptOut.EnterVisiable = !type;
            txtDeptOut.BackColor = System.Drawing.Color.White;
            //��Ժʱ��
            txtDateOut.Enabled = !type;
            //�������
            //			ClinicDiag.ReadOnly = type;
            txtClinicDiag.BackColor = System.Drawing.Color.Gainsboro;
            //���ҽ��
            txtClinicDocd.ReadOnly = type;
            txtClinicDocd.EnterVisiable = !type;
            txtClinicDocd.BackColor = System.Drawing.Color.White;
            //סԺ����
            txtPiDays.ReadOnly = type;
            txtPiDays.BackColor = System.Drawing.Color.White;
            //ȷ֤ʱ��
            txtDiagDate.Enabled = !type;
            //��Ժ���
            //			RuyuanDiagNose.ReadOnly = type;
            txtRuyuanDiagNose.BackColor = System.Drawing.Color.Gainsboro;
            //�ɺ�ҽԺת��
            txtComeFrom.ReadOnly = type;
            txtComeFrom.BackColor = System.Drawing.Color.White;
            //������
            txtNomen.ReadOnly = type;
            txtNomen.BackColor = System.Drawing.Color.White;
            //������Դ
            txtInAvenue.ReadOnly = type;
            txtInAvenue.EnterVisiable = !type;
            txtInAvenue.BackColor = System.Drawing.Color.White;
            //Ժ�д���
            txtInfectNum.ReadOnly = type;
            txtInfectNum.BackColor = System.Drawing.Color.White;
            //hbsag
            txtHbsag.ReadOnly = type;
            txtHbsag.EnterVisiable = !type;
            txtHbsag.BackColor = System.Drawing.Color.White;
            txtHcvAb.ReadOnly = type;
            txtHcvAb.EnterVisiable = !type;
            txtHcvAb.BackColor = System.Drawing.Color.White;
            //�������Ժ
            txtCePi.ReadOnly = type;
            txtCePi.EnterVisiable = !type;
            txtCePi.BackColor = System.Drawing.Color.White;
            //��Ժ���Ժ 
            txtPiPo.ReadOnly = type;
            txtPiPo.EnterVisiable = !type;
            txtPiPo.BackColor = System.Drawing.Color.White;
            //��ǰ������
            txtOpbOpa.ReadOnly = type;
            txtOpbOpa.EnterVisiable = !type;
            txtOpbOpa.BackColor = System.Drawing.Color.White;
            //�ٴ��벡��
            txtClPa.ReadOnly = type;
            txtClPa.EnterVisiable = !type;
            txtClPa.BackColor = System.Drawing.Color.White;
            //�����벡��
            txtFsBl.ReadOnly = type;
            txtFsBl.EnterVisiable = !type;
            txtFsBl.BackColor = System.Drawing.Color.White;
            //���ȴ���
            txtSalvTimes.ReadOnly = type;
            txtSalvTimes.BackColor = System.Drawing.Color.White;
            //�ɹ�����
            txtSuccTimes.ReadOnly = type;
            txtSuccTimes.BackColor = System.Drawing.Color.White;
            //��������
            txtMrQual.ReadOnly = type;
            txtMrQual.EnterVisiable = !type;
            txtMrQual.BackColor = System.Drawing.Color.White;
            //�ʿ�ҽʦ
            txtQcDocd.ReadOnly = type;
            txtQcDocd.EnterVisiable = !type;
            txtQcDocd.BackColor = System.Drawing.Color.White;
            //�ʿػ�ʿ
            txtQcNucd.ReadOnly = type;
            txtQcNucd.EnterVisiable = !type;
            txtQcNucd.BackColor = System.Drawing.Color.White;
            //����ҽʦ
            txtConsultingDoctor.ReadOnly = type;
            txtConsultingDoctor.EnterVisiable = !type;
            txtConsultingDoctor.BackColor = System.Drawing.Color.White;
            //����ҽʦ
            txtAttendingDoctor.ReadOnly = type;
            txtAttendingDoctor.EnterVisiable = !type;
            txtAttendingDoctor.BackColor = System.Drawing.Color.White;
            //סԺҽʦ
            txtAdmittingDoctor.ReadOnly = type;
            txtAdmittingDoctor.EnterVisiable = !type;
            txtAdmittingDoctor.BackColor = System.Drawing.Color.White;
            //����ҽʦ
            txtRefresherDocd.ReadOnly = type;
            txtRefresherDocd.EnterVisiable = !type;
            txtRefresherDocd.BackColor = System.Drawing.Color.White;
            //�о���ʵϰҽʦ
            txtGraDocCode.ReadOnly = type;
            txtGraDocCode.EnterVisiable = !type;
            txtGraDocCode.BackColor = System.Drawing.Color.White;
            //�ʿ�ʱ��
            txtCheckDate.Enabled = !type;
            //ʵϰҽ��
            txtPraDocCode.ReadOnly = type;
            txtPraDocCode.EnterVisiable = !type;
            txtPraDocCode.BackColor = System.Drawing.Color.White;
            //����Ա
            txtCodingCode.ReadOnly = type;
            txtCodingCode.EnterVisiable = !type;
            txtCodingCode.BackColor = System.Drawing.Color.White;
            //����Ա 
            txtCoordinate.ReadOnly = type;
            txtCoordinate.EnterVisiable = !type;
            txtCoordinate.BackColor = System.Drawing.Color.White;
            this.txtOperationCode.ReadOnly = type;
            txtOperationCode.EnterVisiable = !type;
            this.txtOperationCode.BackColor = System.Drawing.Color.White;
            //ʬ�
            cbBodyCheck.Enabled = !type;
            cmbUnit.Enabled = !type;
            //���������ơ���顢��ϡ��Ƿ�Ժ����
            cbYnFirst.Enabled = !type;
            //����
            cbVisiStat.Enabled = !type;
            //��������
            txtVisiPeriWeek.ReadOnly = type;
            txtVisiPeriWeek.BackColor = System.Drawing.Color.White;
            txtVisiPeriMonth.ReadOnly = type;
            txtVisiPeriMonth.BackColor = System.Drawing.Color.White;
            txtVisiPeriYear.ReadOnly = type;
            txtVisiPeriYear.BackColor = System.Drawing.Color.White;
            //ʾ�̲���
            cbTechSerc.Enabled = !type;
            //������
            cbDisease30.Enabled = !type;
            //Ѫ��
            txtBloodType.ReadOnly = type;
            txtBloodType.EnterVisiable = !type;
            txtBloodType.BackColor = System.Drawing.Color.White;
            txtRhBlood.ReadOnly = type;
            txtRhBlood.EnterVisiable = !type;
            txtRhBlood.BackColor = System.Drawing.Color.White;
            //��Ѫ��Ӧ
            txtReactionBlood.ReadOnly = type;
            txtReactionBlood.EnterVisiable = !type;
            txtReactionBlood.BackColor = System.Drawing.Color.White;
            //��ϸ��
            txtBloodRed.ReadOnly = type;
            txtBloodRed.BackColor = System.Drawing.Color.White;
            //ѪС��
            txtBloodPlatelet.ReadOnly = type;
            txtBloodPlatelet.BackColor = System.Drawing.Color.White;
            //Ѫ��
            txtBodyAnotomize.ReadOnly = type;
            txtBodyAnotomize.BackColor = System.Drawing.Color.White;
            //ȫѪ
            txtBloodWhole.ReadOnly = type;
            txtBloodWhole.BackColor = System.Drawing.Color.White;
            //����
            txtBloodOther.ReadOnly = type;
            txtBloodOther.BackColor = System.Drawing.Color.White;
            //Ժ�ʻ���
            txtInconNum.ReadOnly = type;
            txtInconNum.BackColor = System.Drawing.Color.White;
            //Զ�̻���
            txtOutconNum.ReadOnly = type;
            txtOutconNum.BackColor = System.Drawing.Color.White;
            //SuperNus �ؼ�����
            txtSuperNus.ReadOnly = type;
            txtSuperNus.BackColor = System.Drawing.Color.White;
            //һ������
            txtINus.ReadOnly = type;
            txtINus.BackColor = System.Drawing.Color.White;
            //��������
            txtIINus.ReadOnly = type;
            txtIINus.BackColor = System.Drawing.Color.White;
            //��������
            txtIIINus.ReadOnly = type;
            txtIIINus.BackColor = System.Drawing.Color.White;
            //��֢�໤
            txtStrictNuss.ReadOnly = type;
            txtStrictNuss.BackColor = System.Drawing.Color.White;
            //���⻤��
            txtSPecalNus.ReadOnly = type;
            txtSPecalNus.BackColor = System.Drawing.Color.White;
            //ct
            txtCtNumb.ReadOnly = type;
            txtCtNumb.BackColor = System.Drawing.Color.White;
            //UCFT
            txtPathNumb.ReadOnly = type;
            txtPathNumb.BackColor = System.Drawing.Color.White;
            //MR
            txtMriNumb.ReadOnly = type;
            txtMriNumb.BackColor = System.Drawing.Color.White;
            //X��
            txtXNumb.ReadOnly = type;
            txtXNumb.BackColor = System.Drawing.Color.White;
            //B��
            txtBC.Enabled = !type;
            //����Ա
            txtInputDoc.ReadOnly = type;
            txtInputDoc.EnterVisiable = !type;
            txtInputDoc.BackColor = System.Drawing.Color.White;

            txtInfectionPositionNew.ReadOnly = type;
            txtInjuryOrPoisoningCause.ReadOnly = type;
            txtInfectionDiseasesReport.ReadOnly = type;
            txtFourDiseasesReport.ReadOnly = type;
            #region {DB330B9D-7C52-40cc-AC23-73A81CA2AF73}
            txtPharmacyAllergic1.ReadOnly = type;
            txtPharmacyAllergic1.EnterVisiable = !type;
            txtPharmacyAllergic1.BackColor = Color.White;

            txtPharmacyAllergic2.ReadOnly = type;
            txtPharmacyAllergic2.EnterVisiable = !type;
            txtPharmacyAllergic2.BackColor = Color.White;

            txtHivAb.ReadOnly = type;
            txtHivAb.EnterVisiable = !type;
            txtHivAb.BackColor = Color.White;

            txtECTNumb.ReadOnly = type;
            txtECTNumb.BackColor = Color.White;

            txtPETNumb.ReadOnly = type;
            txtPETNumb.BackColor = Color.White;
            #endregion
        }
        #endregion

        #region �����������
        /// <summary>
        /// �����������
        /// </summary>
        public void ClearInfo()
        {
            try
            {
                this.ucDiagNoseInput1.ClearInfo();
                this.ucOperation1.ClearInfo();
                this.ucTumourCard2.ClearInfo();
                this.ucChangeDept1.ClearInfo();
                this.ucBabyCardInput1.ClearInfo();
                this.ucFeeInfo1.ClearInfo();
                //������ 
                txtCaseNum.Text = "";
                //סԺ����
                txtInTimes.Text = "";
                //�������
                txtPactKind.Tag = null;
                //ҽ����
                txtSSN.Text = "";
                //�����
                txtClinicNo.Text = "";
                //����
                txtPatientName.Text = "";
                //�Ա�
                txtPatientSex.Tag = null;
                //����
                //			patientBirthday.Enabled = !type;
                //����
                txtPatientAge.Text = "";
                //����
                txtMaritalStatus.Tag = null;
                //ְҵ
                txtProfession.Tag = null;
                //������
                txtAreaCode.Text = "";
                //����
                txtNationality.Tag = null;
                //����
                txtCountry.Tag = null;
                //����
                txtDIST.Text = "";
                //���֤
                txtIDNo.Text = "";
                //������λ
                txtAddressBusiness.Text = "";
                //��λ�ʱ�
                txtBusinessZip.Text = "";
                //��λ�绰
                txtPhoneBusiness.Text = "";
                //���ڵ�ַ
                txtAddressHome.Text = "";
                //�����ʱ�
                txtHomeZip.Text = "";
                //��ͥ�绰
                txtPhoneHome.Text = "";
                //��ϵ�� 
                txtKin.Text = "";
                //��ϵ
                txtRelation.Tag = null;
                //��ϵ�绰
                txtLinkmanTel.Text = "";
                //l��ϵ�˵�ַ
                txtLinkmanAdd.Text = "";
                //��Ժ����
                txtDeptInHospital.Tag = null;
                //��Ժʱ�� 
                //			Date_In.Enabled = !type;
                //��Ժ���
                txtCircs.Tag = null;
                //ת�����
                txtFirstDept.Tag = null;
                //ת��ʱ��
                dtFirstTime.Value = System.DateTime.Now;
                //ת�����
                txtDeptSecond.Tag = null;
                //ת��ʱ��
                dtSecond.Value = System.DateTime.Now;
                //ת�����
                txtDeptThird.Tag = null;
                //ת��ʱ��
                dtThird.Value = System.DateTime.Now;
                //��Ժ����
                txtDeptOut.Tag = null;
                //��Ժʱ��
                txtDateOut.Value = System.DateTime.Now;
                //�������
                txtClinicDiag.Text = "";
                //���ҽ��
                txtClinicDocd.Tag = null;
                //סԺ����
                txtPiDays.Text = "";
                //ȷ֤ʱ��
                txtDiagDate.Value = System.DateTime.Now;
                //��Ժ���
                txtRuyuanDiagNose.Text = "";
                //�ɺ�ҽԺת��
                txtComeFrom.Text = "";
                //ת����ҽԺ
                txtComeTo.Text = "";
                //������
                txtNomen.Text = "";
                //������Դ
                txtInAvenue.Tag = null;
                //Ժ�д���
                txtInfectNum.Text = "";
                //hbsag
                txtHbsag.Tag = null;
                txtHcvAb.Tag = null;
                txtHivAb.Tag = null;
                //�������Ժ
                txtCePi.Tag = null;
                //��Ժ���Ժ 
                txtPiPo.Tag = null;
                //��ǰ������
                txtOpbOpa.Tag = null;
                //�ٴ��벡��
                txtClPa.Tag = null;
                //�����벡��
                txtFsBl.Tag = null;
                //���ȴ���
                txtSalvTimes.Text = "";
                //�ɹ�����
                txtSuccTimes.Text = "";
                //��������
                txtMrQual.Tag = null;
                //�ʿ�ҽʦ
                txtQcDocd.Tag = null;
                //�ʿػ�ʿ
                txtQcNucd.Tag = null;
                //������
                txtDeptChiefDoc.Tag = null;
                //����ҽʦ
                txtConsultingDoctor.Tag = null;
                //����ҽʦ
                txtAttendingDoctor.Tag = null;
                //סԺҽʦ
                txtAdmittingDoctor.Tag = null;
                //����ҽʦ
                txtRefresherDocd.Tag = null;
                //�о���ʵϰҽʦ
                txtGraDocCode.Tag = null;
                //�ʿ�ʱ��
                txtCheckDate.Value = System.DateTime.Now;
                //ʵϰҽ��
                txtPraDocCode.Tag = null;
                //����Ա
                txtCodingCode.Tag = null;
                //����Ա 
                txtCoordinate.Tag = null;
                this.txtOperationCode.Tag = null;
                //ʬ�
                cbBodyCheck.Checked = false;
                //���������ơ���顢��ϡ��Ƿ�Ժ����
                cbYnFirst.Checked = false;
                //����
                cbVisiStat.Checked = false;
                //��������
                txtVisiPeriWeek.Text = "";
                txtVisiPeriMonth.Text = "";
                txtVisiPeriYear.Text = "";
                //ʾ�̲���
                cbTechSerc.Checked = false;
                //������
                cbDisease30.Checked = false;
                //Ѫ��
                txtBloodType.Tag = null;
                txtRhBlood.Tag = null;
                //��Һ��Ӧ
                txtReactionTransfuse.Tag = null;
                //��Ѫ��Ӧ
                txtReactionBlood.Tag = null;
                //��ϸ��
                txtBloodRed.Text = "";
                //ѪС��
                txtBloodPlatelet.Text = "";
                //Ѫ��
                txtBodyAnotomize.Text = "";
                //ȫѪ
                txtBloodWhole.Text = "";
                //����
                txtBloodOther.Text = "";
                //Ժ�ʻ���
                txtInconNum.Text = "";
                //Զ�̻���
                txtOutconNum.Text = "";
                //SuperNus �ؼ�����
                txtSuperNus.Text = "";
                //һ������
                txtINus.Text = "";
                //��������
                txtIINus.Text = "";
                //��������
                txtIIINus.Text = "";
                //��֢�໤
                txtStrictNuss.Text = "";
                //���⻤��
                txtSPecalNus.Text = "";
                //ct
                txtCtNumb.Text = "";
                //UCFT
                txtPathNumb.Text = "";
                //MR
                txtMriNumb.Text = "";
                //X��
                txtXNumb.Text = "";
                //B��
                txtBC.Text = "";
                //����Ա
                txtInputDoc.Tag = null;
                //��Ⱦ��λ
                this.txtInfectionPositionNew.Text = "";
                //����ҩ��1
                this.txtPharmacyAllergic1.Tag = null;
                //����ҩ��2
                this.txtPharmacyAllergic2.Tag = null;
                //pet��
                this.txtPETNumb.Text = "";
                //ect��
                this.txtECTNumb.Text = "";
                //����״̬
                this.txtCaseStus.Text = "";
                //�����ж�ԭ��
                this.txtInjuryOrPoisoningCause.Text = "";
                this.txtInfectionDiseasesReport.Tag = "";
                this.txtFourDiseasesReport.Tag = null;
                this.txtRemark.Text = "";
                //����ҩ��
                neuTxtPharmacyAllergic1.Text = "";
                neuTxtPharmacyAllergic2.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region ��ӡ������ҳ
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int PrintInterface()
        {
            frmPrintCase frmPrint= new frmPrintCase(this);
            frmPrint.ShowDialog();
            return 0;
        }

        /// <summary>
        /// ��ӡ
        /// </summary>
        /// <param name="Type"></param>
        /// <returns></returns>
        public  int PrintCase(string Type)
        {
            string tips = string.Empty;
            if (this.ContrastInfo(ref tips) == -1)
            {
                MessageBox.Show(tips+ "�����仯���뱣���ٴ�ӡ��", "��ʾ");
                return -1;
            }

            switch (Type)
            {
                case "Print":
                    this.Print(null, null);
                    break;
                case "PrintBack":
                    this.PrintBack(null);
                    break;
                case "PrintPreview":
                    this.PrintPreview(null, null);
                    break;
                case "PrintBackPreview":
                    this.PrintBackPreview(null, null);
                    break;
            }
            return 0;
        }
        /// <summary>
        /// ��ӡ��һҳ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        public override int Print(object sender, object neuObject)
        {
            if (this.healthRecordPrint == null)
            {
                this.healthRecordPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterface)) as FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterface;
                if (this.healthRecordPrint == null)
                {
                    MessageBox.Show("��ýӿ�IExamPrint����\n������û��ά����صĴ�ӡ�ؼ����ӡ�ؼ�û��ʵ�ֽӿ�IExamPrint\n����ϵͳ����Ա��ϵ��");
                    return -1;
                }
            }
            this.healthRecordPrint.ControlValue(this.CaseBase);
            this.healthRecordPrint.Print();
            return 1;

        }

        /// <summary>
        /// ��ӡ����
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int PrintBack(FS.HISFC.Models.HealthRecord.Base obj)
        {
            //���渳ֵ

            if (this.healthRecordPrintBack == null)
            {
                this.healthRecordPrintBack = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(ucCaseMainInfo), typeof(FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterfaceBack)) as FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterfaceBack;
                if (this.healthRecordPrintBack == null)
                {
                    MessageBox.Show("��ýӿ�IExamPrint����\n������û��ά����صĴ�ӡ�ؼ����ӡ�ؼ�û��ʵ�ֽӿ�IExamPrint\n����ϵͳ����Ա��ϵ��");
                    return -1;
                }
            }
            if (CaseBase == null)
            {
                return -1;
            }

            this.healthRecordPrintBack.ControlValue(this.CaseBase);
            this.healthRecordPrintBack.Print();
            return 1;
        }
        /// <summary>
        /// ��ӡԤ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        public override int PrintPreview(object sender, object neuObject)
        {
            if (this.healthRecordPrint == null)
            {
                this.healthRecordPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterface)) as FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterface;
                if (this.healthRecordPrint == null)
                {
                    MessageBox.Show("��ýӿ�IExamPrint����\n������û��ά����صĴ�ӡ�ؼ����ӡ�ؼ�û��ʵ�ֽӿ�IExamPrint\n����ϵͳ����Ա��ϵ��");
                    return -1;
                }
            }

            if (this.CaseBase == null)
            {
                return -1;
            }
            this.healthRecordPrint.ControlValue(this.CaseBase);
            this.healthRecordPrint.PrintPreview();
            return 0;
        }
        /// <summary>
        /// ��ӡ����Ԥ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        public int PrintBackPreview(object sender, object neuObject)
        {
            if (this.healthRecordPrintBack == null)
            {
                this.healthRecordPrintBack = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(ucCaseMainInfo), typeof(FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterfaceBack)) as FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterfaceBack;               
                if (this.healthRecordPrintBack == null)
                {
                    MessageBox.Show("��ýӿ�IExamPrint����\n������û��ά����صĴ�ӡ�ؼ����ӡ�ؼ�û��ʵ�ֽӿ�IExamPrint\n����ϵͳ����Ա��ϵ��");
                    return -1;
                }
            }

            if (this.CaseBase == null)
            {
                return -1;
            }

            this.healthRecordPrintBack.ControlValue(this.CaseBase);
            this.healthRecordPrintBack.PrintPreview();
            return 0;
        }
        /// <summary>
        /// �ԱȽ��������ݿ��Ƿ�һ��
        /// </summary>
        /// <returns></returns>
        private int ContrastInfo(ref string Tips)
        {
            FS.HISFC.Models.HealthRecord.Base dataBaseInfo = baseDml.GetCaseBaseInfo(this.CaseBase.PatientInfo.ID);
            FS.HISFC.Models.HealthRecord.Base info = new FS.HISFC.Models.HealthRecord.Base();
            int i = this.GetInfoFromPanel(info);
            int ret = 0;
            if (dataBaseInfo.PatientInfo.ID != info.PatientInfo.ID)//סԺ��ˮ��
            {
                Tips = "סԺ��ˮ�š�";
            }

            if(dataBaseInfo.PatientInfo.PID.PatientNO!=info.PatientInfo.PID.PatientNO)//סԺ������
            {
                Tips += "סԺ�����š�";
            }
            if(dataBaseInfo.PatientInfo.PID.CardNO!=info.PatientInfo.PID.CardNO)//����
            {
                Tips += "���š�";
            }
            if(dataBaseInfo.PatientInfo.Name!=info.PatientInfo.Name)//����
            {
                Tips += "������";
            }

            if(dataBaseInfo.PatientInfo.Sex.ID.ToString()!=info.PatientInfo.Sex.ID.ToString())//�Ա�
            {
                Tips += "�Ա�";
            }
            if (dataBaseInfo.PatientInfo.Birthday.Date != info.PatientInfo.Birthday.Date)//��������
            {
                Tips += "�������ڡ�";
            }
            if (dataBaseInfo.PatientInfo.Country.ID != info.PatientInfo.Country.ID)//����
            {
                Tips += "���ҡ�";
            }
            if (dataBaseInfo.PatientInfo.Nationality.ID != info.PatientInfo.Nationality.ID)//����
            {
                Tips += "���塢";
            }
            if (dataBaseInfo.PatientInfo.Profession.ID != info.PatientInfo.Profession.ID)//ְҵ
            {
                Tips += "ְҵ��";
            }
            if (dataBaseInfo.PatientInfo.BloodType.ID.ToString().Trim() != info.PatientInfo.BloodType.ID.ToString().Trim())//Ѫ�ͱ���
            {
                Tips += "Ѫ�͡�";
            }
            if (dataBaseInfo.PatientInfo.MaritalStatus.ID.ToString().Trim() != info.PatientInfo.MaritalStatus.ID.ToString().Trim())//���
            {
                Tips += "������";
            }
            if (dataBaseInfo.PatientInfo.IDCard.ToString().Trim() != info.PatientInfo.IDCard.ToString().Trim())//���֤��
            {
                Tips += "���֤�š�";
            }
            if (dataBaseInfo.PatientInfo.PVisit.InSource.ID != info.PatientInfo.PVisit.InSource.ID)//������Դ
            {
                Tips += "������Դ��";
            }
            if (dataBaseInfo.PatientInfo.Pact.ID != info.PatientInfo.Pact.ID)//��ͬ����
            {
                Tips += "���ʽ��";
            }

            if (dataBaseInfo.PatientInfo.SSN != info.PatientInfo.SSN)//ҽ�����Ѻ�
            {
                Tips += "ҽ���š�";
            }
            if (dataBaseInfo.PatientInfo.DIST != info.PatientInfo.DIST)//����
            {
                Tips += "���ᡢ";
            }
            if (dataBaseInfo.PatientInfo.AreaCode != info.PatientInfo.AreaCode)//������
            {
                Tips += "�����ء�";
            }
            if (dataBaseInfo.PatientInfo.AddressHome != info.PatientInfo.AddressHome)//��ͥסַ
            {
                Tips += "��ͥסַ��";
            }
            if (dataBaseInfo.PatientInfo.PhoneHome != info.PatientInfo.PhoneHome)//��ͥ�绰
            {
                Tips += "��ͥ�绰��";
            }
            if (dataBaseInfo.PatientInfo.HomeZip != info.PatientInfo.HomeZip)//סַ�ʱ�
            {
                Tips += "סַ�ʱࡢ";
            }
            if (dataBaseInfo.PatientInfo.AddressBusiness != info.PatientInfo.AddressBusiness)//��λ��ַ
            {
                Tips += "��λ��ַ��";
            }
            if (dataBaseInfo.PatientInfo.PhoneBusiness != info.PatientInfo.PhoneBusiness)//��λ�绰
            {
                Tips += "��λ�绰��";
            }
            if (dataBaseInfo.PatientInfo.BusinessZip != info.PatientInfo.BusinessZip)//��λ�ʱ�
            {
                Tips += "��λ�ʱࡢ";
            }
            if (dataBaseInfo.PatientInfo.Kin.Name != info.PatientInfo.Kin.Name)//��ϵ��
            {
                Tips += "��ϵ�ˡ�";
            }
            if (dataBaseInfo.PatientInfo.Kin.RelationLink != info.PatientInfo.Kin.RelationLink)//�뻼�߹�ϵ
            {
                Tips += "�뻼�߹�ϵ��";
            }
            if (dataBaseInfo.PatientInfo.Kin.RelationPhone != info.PatientInfo.Kin.RelationPhone)//��ϵ�绰
            {
                Tips += "��ϵ�绰��";
            }
            if (dataBaseInfo.PatientInfo.Kin.RelationAddress != info.PatientInfo.Kin.RelationAddress)//��ϵ��ַ
            {
                Tips += "��ϵ��ַ��";
            }
            if (dataBaseInfo.ClinicDoc.ID != info.ClinicDoc.ID)//�������ҽ��
            {
                Tips += "�������ҽ����";
            }

            if (dataBaseInfo.ComeFrom != info.ComeFrom)//ת��ҽԺ
            {
                Tips += "ת��ҽԺ��";
            }
            if (dataBaseInfo.PatientInfo.PVisit.InTime.Date != info.PatientInfo.PVisit.InTime.Date)//��Ժ����
            {
                Tips += "��Ժ���ڡ�";
            }
            if (dataBaseInfo.PatientInfo.InTimes != info.PatientInfo.InTimes)//סԺ����
            {
                Tips += "סԺ������";
            }
            if (dataBaseInfo.InDept.ID != info.InDept.ID)//��Ժ���Ҵ���
            {
                Tips += "��Ժ���ҡ�";
            }

            if (dataBaseInfo.PatientInfo.PVisit.InSource.ID != info.PatientInfo.PVisit.InSource.ID)//��Ժ��Դ
            {
                Tips += "��Ժ��Դ��";
            }
            if (dataBaseInfo.PatientInfo.PVisit.Circs.ID != info.PatientInfo.PVisit.Circs.ID)//��Ժ״̬
            {
                Tips += "��Ժ״̬��";
            }
            if (dataBaseInfo.DiagDate.Date != info.DiagDate.Date)//ȷ������
            {
                Tips += "ȷ�����ڡ�";
            }

            if (dataBaseInfo.PatientInfo.PVisit.OutTime.Date != info.PatientInfo.PVisit.OutTime.Date)//��Ժ����
            {
                Tips += "��Ժ���ڡ�";
            }
            if (dataBaseInfo.OutDept.ID != info.OutDept.ID)//��Ժ���Ҵ���
            {
                Tips += "��Ժ���ҡ�";
            }

            if (dataBaseInfo.DiagDays != info.DiagDays)//ȷ������
            {
                Tips += "ȷ��������";
            }
            if (dataBaseInfo.InHospitalDays != info.InHospitalDays)//סԺ����
            {
                Tips += "סԺ������";
            }


            if (dataBaseInfo.CadaverCheck != info.CadaverCheck)//ʬ��
            {
                Tips += "ʬ�졢";
            }
            if (dataBaseInfo.Hbsag != info.Hbsag)//�Ҹα��濹ԭ
            {
                Tips += "Hbsag��";
            }
            if (dataBaseInfo.HcvAb != info.HcvAb)//���β�������
            {
                Tips += "HcvAb��";
            }
            if (dataBaseInfo.HivAb != info.HivAb)//�������������ȱ�ݲ�������
            {
                Tips += "HivAb��";
            }
            if (dataBaseInfo.CePi != info.CePi)//�ż�_��Ժ����
            {
                Tips += "��������Ժ���ϡ�";
            }
            if (dataBaseInfo.PiPo != info.PiPo)//���_Ժ����
            {
                Tips += "��Ժ���Ժ���ϡ�";
            }
            if (dataBaseInfo.OpbOpa != info.OpbOpa)//��ǰ_�����
            {
                Tips += "��ǰ��������ϡ�";
            }

            if (dataBaseInfo.ClPa != info.ClPa)//�ٴ�_�������
            {
                Tips += "�ٴ��벡����ϡ�";
            }
            if (dataBaseInfo.FsBl != info.FsBl)//����_�������
            {
                Tips += "�����벡����ϡ�";
            }
            if (dataBaseInfo.SalvTimes != info.SalvTimes)//���ȴ���
            {
                Tips += "���ȴ�����";
            }
            if (dataBaseInfo.SuccTimes != info.SuccTimes)//�ɹ�����
            {
                Tips += "�ɹ�������";
            }
            if (dataBaseInfo.TechSerc != info.TechSerc)//ʾ�̿���
            {
                Tips += "ʾ�̿��С�";
            }
            if (dataBaseInfo.VisiStat != info.VisiStat)//�Ƿ�����
            {
                Tips += "���";
            }

            if (dataBaseInfo.InconNum != info.InconNum)//Ժ�ʻ������ 70 Զ�̻������
            {
                Tips += "Ժ�ʻ��������";
            }
            if (dataBaseInfo.OutconNum != info.OutconNum)//Ժ�ʻ������ 70 Զ�̻������
            {
                Tips += "Զ�̻��������";
            }

            if (dataBaseInfo.FirstAnaphyPharmacy.ID != info.FirstAnaphyPharmacy.ID)//����ҩ������
            {
                Tips += "����ҩ�����ơ�";
            }
            if (dataBaseInfo.PatientInfo.PVisit.AdmittingDoctor.ID != info.PatientInfo.PVisit.AdmittingDoctor.ID)//סԺҽʦ����
            {
                Tips += "סԺҽʦ�";
            }


            if (dataBaseInfo.PatientInfo.PVisit.AttendingDoctor.ID != info.PatientInfo.PVisit.AttendingDoctor.ID)//����ҽʦ����
            {
                Tips += "����ҽʦ�";
            }


            if (dataBaseInfo.PatientInfo.PVisit.ConsultingDoctor.ID != info.PatientInfo.PVisit.ConsultingDoctor.ID)//����ҽʦ����
            {
                Tips += "����ҽʦ�";
            }


            if (dataBaseInfo.PatientInfo.PVisit.ReferringDoctor.ID != info.PatientInfo.PVisit.ReferringDoctor.ID)//�����δ���
            {
                Tips += "�������";
            }

            if (dataBaseInfo.RefresherDoc.ID != info.RefresherDoc.ID)//����ҽʦ����
            {
                Tips += "����ҽʦ�";
            }

            if (dataBaseInfo.GraduateDoc.ID != info.GraduateDoc.ID)//�о���ʵϰҽʦ����
            {
                Tips += "�о���ʵϰҽʦ�";
            }
            if (dataBaseInfo.PatientInfo.PVisit.TempDoctor.ID != info.PatientInfo.PVisit.TempDoctor.ID)//ʵϰҽʦ����
            {
                Tips += "ʵϰҽʦ�";
            }

            if (dataBaseInfo.MrQuality != info.MrQuality)//��������
            {
                Tips += "����������";
            }

            if (dataBaseInfo.QcDoc.ID != info.QcDoc.ID)//�ʿ�ҽʦ����
            {
                Tips += "�ʿ�ҽʦ�";
            }

            if (dataBaseInfo.QcNurse.ID != info.QcNurse.ID)//�ʿػ�ʿ����
            {
                Tips += "�ʿػ�ʿ�";
            }

            if (dataBaseInfo.CheckDate.Date != info.CheckDate.Date)//���ʱ��
            {
                Tips += "�ʿ������";
            }
            if (dataBaseInfo.YnFirst != info.YnFirst)//�����������Ƽ�����Ϊ��Ժ��һ����Ŀ
            {
                Tips += "�����������Ƽ�����Ϊ��Ժ��һ����Ŀ��";
            }
            if (dataBaseInfo.RhBlood != info.RhBlood)//RhѪ��(����)
            {
                Tips += "RhѪ�͡�";
            }
            if (dataBaseInfo.ReactionBlood != info.ReactionBlood)//��Ѫ��Ӧ�����ޣ�
            {
                Tips += "��Ѫ��Ӧ��";
            }
            if (dataBaseInfo.BloodRed != info.BloodRed)//��ϸ����
            {
                Tips += "��ϸ������";
            }
            if (dataBaseInfo.BloodPlatelet != info.BloodPlatelet)//ѪС����
            {
                Tips += "ѪС������";
            }
            if (dataBaseInfo.BloodPlasma != info.BloodPlasma)//Ѫ����
            {
                Tips += "Ѫ������";
            }
            if (dataBaseInfo.BloodWhole != info.BloodWhole)//ȫѪ��
            {
                Tips += "ȫѪ����";
            }
            if (dataBaseInfo.BloodOther != info.BloodOther)//������Ѫ��
            {
                Tips += "������Ѫ����";
            }

            if (dataBaseInfo.VisiPeriodWeek != info.VisiPeriodWeek) //������� ��
            {
                Tips += "�������-�ܡ�";
            }
            if (dataBaseInfo.VisiPeriodMonth != info.VisiPeriodMonth) //������� ��
            {
                Tips += "�������-�¡�";
            }
            if (dataBaseInfo.VisiPeriodYear != info.VisiPeriodYear)//������� ��
            {
                Tips += "�������-�ꡢ";
            }
            if (dataBaseInfo.SpecalNus != info.SpecalNus)  // ���⻤��(��)  
            {
                Tips += "���⻤��";
            }
            if (dataBaseInfo.INus != info.INus) //I������ʱ��(��)  
            {
                Tips += "I������";
            }
            if (dataBaseInfo.IINus != info.IINus) //II������ʱ��(��)   
            {
                Tips += "II������";
            }
            if (dataBaseInfo.IIINus != info.IIINus) //III������ʱ��(��)
            {
                Tips += "III������";
            }
            if (dataBaseInfo.StrictNuss != info.StrictNuss) //��֢�໤ʱ��( Сʱ)  
            {
                Tips += "��֢�໤��";
            }
            if (dataBaseInfo.SuperNus != info.SuperNus) //�ؼ�����ʱ��(Сʱ)   
            {
                Tips += "�ؼ�����";
            }
            if (dataBaseInfo.ClinicDiag.Name != info.ClinicDiag.Name)
            {
                Tips += "������ϡ�";
            }

            if (dataBaseInfo.InHospitalDiag.Name != info.InHospitalDiag.Name)
            {
                Tips += "��Ժ��ϡ�";
            }

            if (dataBaseInfo.InfectionPosition.Name != info.InfectionPosition.Name) //Ժ�ڸ�Ⱦ��λ����
            {
                Tips += "Ժ�ڸ�Ⱦ��";
            }
            if (dataBaseInfo.InfectionPosition.Memo != info.InfectionPosition.Memo)//�����ж����ⲿ���ر���-��ҽ2010-2-2
            {
                Tips += "�����ж����ⲿ����";
            }

            if (dataBaseInfo.OutDept.Memo != info.OutDept.Memo)//ת����ҽԺ
            {
                Tips += "ת����ҽԺ";
            }
            if (Tips != "")
            {
                ret = -1;
            }
            else
            {
                ret = 1;
            }
            return ret;
        }
        /// <summary>
        /// �˳��Ƿ񱣴�
        /// </summary>
        public void IsNeedSaveCheck()
        {
            string tips = string.Empty;
            if (this.ContrastInfo(ref tips) == -1)
            {
                if (MessageBox.Show(tips + "�����仯���뱣���ٴ�ӡ��", "����", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    this.Save(0);
                }
            }
        }
        #endregion

        #region У�����Լ��
        /// <summary>
        /// 
        /// </summary>
        /// <param name="diag"></param>
        /// <returns></returns>
        public int DiagValueState(FS.HISFC.BizLogic.HealthRecord.Diagnose diag)
        {
            ArrayList allList = new ArrayList();
            this.ucDiagNoseInput1.GetAllDiagnose(allList);
            if (allList == null)
            {
                return -1;
            }
            if (allList.Count == 0)
            {
                return 1;
            }
            FS.HISFC.Models.Base.EnumSex sex;
            if (CaseBase.PatientInfo.Sex.ID.ToString() == "F")
            {
                sex = FS.HISFC.Models.Base.EnumSex.F;
            }
            else if (CaseBase.PatientInfo.Sex.ID.ToString() == "M")
            {
                sex = FS.HISFC.Models.Base.EnumSex.M;
            }
            else
            {
                sex = FS.HISFC.Models.Base.EnumSex.U;
            }
            //����
            ArrayList diagCheckList = diag.QueryDiagnoseValueState(allList, sex);
            ucDiagnoseCheck ucdia = new ucDiagnoseCheck();
            if (diagCheckList == null)
            {
                MessageBox.Show("��ȡԼ������");
                return -1;
            }
            if (diagCheckList.Count == 0)
            {
                return 1;
            }
            try
            {
                if (frm != null)
                {
                    frm.Close();
                }
            }
            catch { }

            frm = new ucDiagNoseCheck();
            frm.initDiangNoseCheck(diagCheckList);
            if (frmType == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC)
            {
                frm.Show();
                if (frm.GetRedALarm())
                {
                    return -1;
                }
            }
            //			else if(frmType == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.CAS)
            //			{
            //				frm.ShowDialog();
            //				if(frm.GetRedALarm() )
            //				{
            //					return -1;
            //				}
            //			}
            return 1;
        }
        #endregion

        #region ��ȡ��������
        private void cmbUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.dtPatientBirthday.ValueChanged -= new EventHandler(txBirth_ValueChanged);
            //this.getBirthday();
            //this.dtPatientBirthday.ValueChanged += new EventHandler(txBirth_ValueChanged);
        }

        #region У���¼��Ƿ����
        private void txBirth_ValueChanged(object sender, System.EventArgs e)
        {
            //DateTime dtNow = this.baseDml.GetDateTimeFromSysDateTime();

            //DateTime dtBirth = this.dtPatientBirthday.Value;

            //if (dtBirth > dtNow)
            //{
            //    dtBirth = dtNow;
            //    //MessageBox.Show("�������ڲ��ܴ���ϵͳ���ڣ�");
            //    return;
            //}

            //int years = 0;

            //System.TimeSpan span = new TimeSpan(dtNow.Ticks - dtBirth.Ticks);

            //years = span.Days / 365;

            //if (years <= 0)
            //{
            //    int month = span.Days / 30;

            //    if (month <= 0)
            //    {
            //        this.txtPatientAge.Text = span.Days.ToString();
            //        this.cmbUnit.SelectedIndex = 2;
            //    }
            //    else
            //    {
            //        this.txtPatientAge.Text = month.ToString();
            //        this.cmbUnit.SelectedIndex = 1;
            //    }
            //}
            //else
            //{
            //    this.txtPatientAge.Text = years.ToString();
            //    this.cmbUnit.SelectedIndex = 0;
            //}
        }
        #endregion
        /// <summary>
        /// ��ȡ��������
        /// </summary>
        private void getBirthday()
        {
            //string age = this.txtPatientAge.Text.Trim();
            //int i = 0;

            //if (age == "") age = "0";

            //try
            //{
            //    i = int.Parse(age);
            //}
            //catch (Exception e)
            //{
            //    string error = e.Message;
            //    MessageBox.Show("�������䲻��ȷ,����������!", "��ʾ");
            //    this.txtPatientAge.Focus();
            //    return;
            //}
            //DateTime birthday = DateTime.MinValue;

            //this.getBirthday(i, this.cmbUnit.Text, ref birthday);

            //if (birthday <= this.dtPatientBirthday.MinDate)
            //{
            //    this.txtPatientAge.Focus();
            //    return;
            //}

            ////this.dtBirthday.Value = birthday ;

            //if (this.cmbUnit.Text == "��")
            //{

            //    //���ݿ��д���ǳ�������,������䵥λ����,��������ĳ������ں����ݿ��г������������ͬ
            //    //�Ͳ��������¸�ֵ,��Ϊ����ĳ�����������Ϊ����,���������ݿ���Ϊ׼

            //    if (this.dtPatientBirthday.Value.Year != birthday.Year)
            //    {
            //        this.dtPatientBirthday.Value = birthday;
            //    }
            //}
            //else
            //{
            //    this.dtPatientBirthday.Value = birthday;
            //}
        }
        #region ��������õ���������
        /// <summary>
        /// ��������õ���������
        /// </summary>
        /// <param name="age"></param>
        /// <param name="ageUnit"></param>
        /// <param name="birthday"></param>
        private void getBirthday(int age, string ageUnit, ref System.DateTime birthday)
        {
            DateTime current = this.baseDml.GetDateTimeFromSysDateTime();

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
        }
        #endregion
        #endregion

        #region �����ֹ�¼��
        private void SetHandcraft()
        {
            this.CaseBase = new FS.HISFC.Models.HealthRecord.Base();
            string strCaseNO = this.baseDml.GetCaseInpatientNO();
            if (strCaseNO == null || strCaseNO == "")
            {
                MessageBox.Show("��ȡסԺ��ˮ��ʧ��" + baseDml.Err);
                CaseBase = new FS.HISFC.Models.HealthRecord.Base();
                return;
            }
            CaseBase.PatientInfo.ID = strCaseNO;
            CaseBase.IsHandCraft = "1";
            ucFeeInfo1.BoolType = true;
            ucFeeInfo1.LoadInfo(CaseBase.PatientInfo);
            HandCraft = 1;
        }
        #endregion

        #region  �������Ų�ѯ
        private void txtCaseNOSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                try
                {
                    if (txtCaseNOSearch.Text == "")
                    {
                        MessageBox.Show("�����벡����");
                        return;
                    }
                    else
                    {
                        txtCaseNOSearch.Text = txtCaseNOSearch.Text.Trim().PadLeft(10, '0');
                    }

                    #region ���
                    HandCraft = 0;
                    this.CaseBase = null;
                    ClearInfo();
                    #endregion
                    if (!this.ucPatient.Visible)
                    {
                        #region ��ѯ
                        ArrayList list = null;
                        list = ucPatient.Init(txtCaseNOSearch.Text, "1");
                        if (list == null)
                        {
                            MessageBox.Show("��ѯʧ��" + ucPatient.strErr);
                            return;
                        }
                        if (list.Count == 0)
                        {
                            if (frmType == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.CAS)
                            {
                                #region �������Լ��ֹ�¼�벡��
                                if (MessageBox.Show("û�в鵽��ز�����Ϣ,�Ƿ��ֹ�¼�벡��", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    txtCaseNum.Text = txtCaseNOSearch.Text;
                                    txtCaseNum.Focus();
                                    SetHandcraft();
                                }
                                else
                                {
                                    return;
                                }
                                #endregion
                            }
                            else
                            {
                                MessageBox.Show("û�в鵽��ز�����Ϣ");
                                return;
                            }
                        }
                        else if (list.Count == 1) //ֻ��һ��������Ϣ
                        {
                            ucPatient.Visible = false;
                            FS.HISFC.Models.HealthRecord.Base obj = this.ucPatient.GetCaseInfo();
                            if (obj != null)
                            {
                                LoadInfo(obj.PatientInfo.ID, this.frmType);
                            }
                        }
                        else
                        {
                            ucPatient.Visible = true;
                        }
                        #endregion
                    }
                    else
                    {
                        #region  ѡ��
                        FS.HISFC.Models.HealthRecord.Base obj = this.ucPatient.GetCaseInfo();
                        if (obj != null)
                        {
                            LoadInfo(obj.PatientInfo.ID, this.frmType);
                        }
                        this.ucPatient.Visible = false;
                        #endregion
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (e.KeyCode == Keys.Up)
            {
                ucPatient.PriorRow();
            }
            else if (e.KeyCode == Keys.Down)
            {
                ucPatient.NextRow();
            }
        }
        #endregion

        #region ��סԺ�Ų�ѯ
        private void txtPatientNOSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                try
                {
                    if (txtPatientNOSearch.Text == "")
                    {
                        MessageBox.Show("������סԺ��");
                        return;
                    }
                    else
                    {
                        txtPatientNOSearch.Text = txtPatientNOSearch.Text.Trim().PadLeft(10, '0');
                    }
                    #region ���
                    HandCraft = 0;
                    this.CaseBase = null;
                    ClearInfo();
                    #endregion
                    if (!this.ucPatient.Visible)
                    {
                        #region ��ѯ
                        ArrayList list = null;
                        list = ucPatient.Init(txtPatientNOSearch.Text, "2");
                        if (list == null)
                        {
                            MessageBox.Show("��ѯʧ��" + ucPatient.strErr);
                            return;
                        }
                        if (list.Count == 0)
                        {
                            if (frmType == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.CAS)
                            {
                                #region �������Լ��ֹ�¼�벡��
                                if (MessageBox.Show("û�в鵽��ز�����Ϣ,�Ƿ��ֹ�¼�벡��", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    SetHandcraft();
                                }
                                else
                                {
                                    return;
                                }
                                #endregion
                            }
                            else
                            {
                                MessageBox.Show("û�в鵽��ز�����Ϣ");
                                return;
                            }
                        }
                        else if (list.Count == 1) //ֻ��һ����Ϣ
                        {
                            ucPatient.Visible = false;
                            FS.HISFC.Models.HealthRecord.Base obj = this.ucPatient.GetCaseInfo();
                            if (obj != null)
                            {
                                LoadInfo(obj.PatientInfo.ID, this.frmType);
                                this.txtCaseNum.Focus();
                            }
                        }
                        else //������Ϣ 
                        {
                            ucPatient.Visible = true;
                        }
                        #endregion
                    }
                    else
                    {
                        #region  ѡ��
                        FS.HISFC.Models.HealthRecord.Base obj = this.ucPatient.GetCaseInfo();
                        if (obj != null)
                        {
                            LoadInfo(obj.PatientInfo.ID, this.frmType);
                            this.txtCaseNum.Focus();
                        }
                        this.ucPatient.Visible = false;
                        #endregion
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (e.KeyCode == Keys.Up)
            {
                ucPatient.PriorRow();
            }
            else if (e.KeyCode == Keys.Down)
            {
                ucPatient.NextRow();
            }
        }
        #endregion ��סԺ�Ų�ѯ

        #region ˫�����Ľڵ�
        private void treeView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.treeView1.SelectedNode.Level == 0)
            {
                return;
            }

            try
            {
                if (this.treeView1.SelectedNode.Text == "�����Ժ����")
                {
                    return;
                }
                FS.HISFC.Models.RADT.PatientInfo pa = (FS.HISFC.Models.RADT.PatientInfo)treeView1.SelectedNode.Tag;
                LoadInfo(pa.ID, this.frmType);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region ��ѯ������ϵ����Ϣ

        #region {E9F858A6-BDBC-4052-BA57-68755055FB80}

        /// <summary>
        /// ����סԺ�Ż����Ų�ѯ������ϵ���б�
        /// </summary>
        /// <param name="patientNo">סԺ��</param>
        /// <param name="cardNo">������</param>
        private void InitLinkWay(string patientNo, string cardNo)
        {
            if (string.IsNullOrEmpty(patientNo))
            {
                patientNo = this.PatientNo;
            }

            if (string.IsNullOrEmpty(cardNo))
            {
                cardNo = this.CardNo;
            }

            ArrayList list = new ArrayList();

            list = linkWayManager.QueryLinkWay(patientNo, cardNo);
            if (list == null)
            {
                return;
            }
            neuSpread1_Sheet1.Rows.Count = list.Count;
            for (int i = 0; i < list.Count; i++)
            {
                FS.HISFC.Models.HealthRecord.Visit.LinkWay linkWayObj
                    = list[i] as FS.HISFC.Models.HealthRecord.Visit.LinkWay;


                if (linkWayObj != null)
                {
                    this.neuSpread1_Sheet1.Cells[i, 1].Text = linkWayObj.Name;//��ϵ��
                    this.neuSpread1_Sheet1.Cells[i, 2].Text = linkWayObj.Memo;//�뻼�߹�ϵ
                    this.neuSpread1_Sheet1.Cells[i, 3].Text = linkWayObj.Phone;//��ϵ�绰
                    this.neuSpread1_Sheet1.Cells[i, 4].Text = linkWayObj.User01;//�绰״̬
                    this.neuSpread1_Sheet1.Cells[i, 5].Text = linkWayObj.Address;//��ϵ��ַ
                    this.neuSpread1_Sheet1.Cells[i, 6].Text = linkWayObj.Mail;//�����ʼ�

                    this.neuSpread1_Sheet1.Rows[i].Tag = linkWayObj;

                }
            }

        }

        private void cmsMain_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "����":
                    AddNewLinkRow();
                    break;
                case "����":
                    SaveLinkRow();
                    break;
                case "ɾ��":
                    DeleteLinkRow();
                    break;
            }
        }

        /// <summary>
        /// �����ϵ�˷���
        /// </summary>
        /// <returns>�ɹ����� 0;ʧ�ܷ��� -1</returns>
        private int AddNewLinkRow()
        {
            try
            {
                int RowCount = this.neuSpread1_Sheet1.Rows.Count;
                this.neuSpread1_Sheet1.Rows.Add(RowCount, 1);
                this.neuSpread1_Sheet1.ActiveRowIndex = this.neuSpread1_Sheet1.Rows.Count;
                neuSpread1.ShowActiveCell(FarPoint.Win.Spread.VerticalPosition.Center, FarPoint.Win.Spread.HorizontalPosition.Center);
                this.neuSpread1_Sheet1.SetActiveCell(RowCount, 0);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }
            return 0;
        }

        /// <summary>
        /// ������ϵ���б�
        /// </summary>
        /// <returns></returns>
        private int SaveLinkRow()
        {

            linkWayManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            for (int i = 0; i < neuSpread1_Sheet1.Rows.Count; i++)
            {
                FS.HISFC.Models.HealthRecord.Visit.LinkWay linkWayObj1 =
                    neuSpread1_Sheet1.Rows[i].Tag as FS.HISFC.Models.HealthRecord.Visit.LinkWay;//Tag�еĶ���

                FS.HISFC.Models.HealthRecord.Visit.LinkWay linkWayObj2 = GetLinkWayObj(i); //�����е�ֵ���ɵĶ���

                if (linkWayObj1 == null)
                {
                    //������ϵ��
                    if (linkWayManager.InsertLinkWay(linkWayObj2) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        //t.RollBack();
                        MessageBox.Show("��ϵ���б��淢������:" + linkWayManager.Err);
                        return -1;
                    }
                }
                else if (linkWayObj1 != linkWayObj2)
                {
                    //������ϵ��
                    linkWayObj2.ID = linkWayObj1.ID;
                    if (linkWayManager.UpdateInsertLinkWay(linkWayObj2) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        //t.RollBack();
                        MessageBox.Show("��ϵ���б��淢������:" + linkWayManager.Err);
                        return -1;
                    }
                }

            }

            FS.FrameWork.Management.PublicTrans.Commit();

            if (CaseBase != null)
            {
                InitLinkWay(CaseBase.PatientInfo.PID.PatientNO, CaseBase.PatientInfo.PID.CardNO);
            }

            return 0;
        }

        /// <summary>
        /// ɾ����ϵ����Ϣ
        /// </summary>
        /// <returns></returns>
        private int DeleteLinkRow()
        {
            if (MessageBox.Show("�Ƿ�ɾ��ѡ�����ϵ����Ϣ", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                == DialogResult.Yes)
            {

                linkWayManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                for (int i = 0; i < neuSpread1_Sheet1.Rows.Count; i++)
                {
                    if (this.neuSpread1_Sheet1.Cells[i, 0].Text == true.ToString())
                    {
                        FS.HISFC.Models.HealthRecord.Visit.LinkWay linkWayObj1 =
                        neuSpread1_Sheet1.Rows[i].Tag as FS.HISFC.Models.HealthRecord.Visit.LinkWay;//Tag�еĶ���
                        if (linkWayObj1 != null)
                        {
                            if (linkWayManager.DelLinkWay(linkWayObj1) == -1)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                //t.RollBack();
                                MessageBox.Show("ɾ����ϵ�˷�������:" + linkWayManager.Err);
                                return -1;
                            }
                        }
                    }
                }

                FS.FrameWork.Management.PublicTrans.Commit();

                //���¼�����ϵ���б�
                InitLinkWay(CaseBase.PatientInfo.PID.PatientNO, CaseBase.PatientInfo.PID.CardNO);
            }

            return 0;
        }

        /// <summary>
        /// ������������ȡ����
        /// </summary>
        /// <param name="index">FarPoint������</param>
        /// <returns></returns>
        private FS.HISFC.Models.HealthRecord.Visit.LinkWay GetLinkWayObj(int index)
        {
            FS.HISFC.Models.HealthRecord.Visit.LinkWay linkWayObj
            = new FS.HISFC.Models.HealthRecord.Visit.LinkWay();
            linkWayObj.Name = this.neuSpread1_Sheet1.Cells[index, 1].Text;//��ϵ��
            linkWayObj.Memo = this.neuSpread1_Sheet1.Cells[index, 2].Text;  //�뻼�߹�ϵ
            linkWayObj.Phone = this.neuSpread1_Sheet1.Cells[index, 3].Text;//��ϵ�绰
            linkWayObj.User01 = this.neuSpread1_Sheet1.Cells[index, 4].Text;//�绰״̬
            linkWayObj.Address = this.neuSpread1_Sheet1.Cells[index, 5].Text;//��ϵ��ַ
            linkWayObj.Mail = this.neuSpread1_Sheet1.Cells[index, 6].Text;//�����ʼ�

            linkWayObj.Patient = CaseBase.PatientInfo;

            if (string.IsNullOrEmpty(linkWayObj.Patient.PID.CardNO))
            {
                linkWayObj.Patient.PID.CardNO = this.CardNo;
            }

            if (string.IsNullOrEmpty(linkWayObj.Patient.PID.PatientNO))
            {
                linkWayObj.Patient.PID.PatientNO = this.PatientNo;
            }


            return linkWayObj;
        }




        #endregion

        #endregion

        #region {E9F858A6-BDBC-4052-BA57-68755055FB80}

        private void neuCheckBoxIsDead_CheckedChanged(object sender, EventArgs e)
        {
            this.neuDateTimePickerDeadTime.Enabled = this.neuCheckBoxIsDead.Checked;
            this.neuComboBoxDeadReason.Enabled = this.neuCheckBoxIsDead.Checked;
            if (this.neuCheckBoxIsDead.Checked)
            {
                this.neuDateTimePickerDeadTime.Value = visitIntergrate.GetCurrentDateTime();
            }
            else
            {
                this.neuDateTimePickerDeadTime.Value = this.neuDateTimePickerDeadTime.MinDate;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (SaveLinkRow() == -1)
            {
                MessageBox.Show("��ϵ����Ϣ����ʧ��");
                return;
            }

            if (cmbLinkType.SelectedIndex == -1)
            {
                MessageBox.Show("��ѡ����÷�ʽ");
                return;
            }

            if (cmbResult.SelectedIndex == -1)
            {
                MessageBox.Show("��ѡ����ý��");
                return;
            }

            //�����ϸ��¼ʵ��
            FS.HISFC.Models.HealthRecord.Visit.VisitRecord visitRecord
                = new FS.HISFC.Models.HealthRecord.Visit.VisitRecord();

            //�������¼ʵ��
            FS.HISFC.Models.HealthRecord.Visit.Visit visit = new FS.HISFC.Models.HealthRecord.Visit.Visit();

            string seqNo = visitRecordManager.GetVisitRecordSequ();

            if (seqNo == null)
            {
                MessageBox.Show(visitRecordManager.Err);
                return;
            }
            if (GetVisitRecordObj(ref visitRecord, ref visit) == -1)
            {
                return;
            }
            visitIntergrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            if (visitIntergrate.InsertAndUpdateVisit(visitRecord, seqNo, visit) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(visitIntergrate.Err);
                return;
            }
            FS.FrameWork.Management.PublicTrans.Commit();

            MessageBox.Show("�����Ϣ����ɹ�");
            this.FindForm().Close();



        }

        /// <summary>
        /// ��ȡ�����ϸʵ�����
        /// </summary>
        /// <param name="visitRecordObj">ref�����ϸʵ��</param>
        /// <param name="visitObj">ref�������¼ʵ��</param>
        /// <returns>�ɹ����� 0; ʧ�ܷ��� -1;</returns>
        private int GetVisitRecordObj(ref FS.HISFC.Models.HealthRecord.Visit.VisitRecord visitRecordObj,
          ref FS.HISFC.Models.HealthRecord.Visit.Visit visitObj)
        {


            int checkCount = 0;//��ѡ�е���ϵ������

            FS.HISFC.Models.HealthRecord.Visit.LinkWay linkWayObj;

            for (int i = 0; i < neuSpread1_Sheet1.Rows.Count; i++)
            {
                if (this.neuSpread1_Sheet1.Cells[i, 0].Text == true.ToString())
                {
                    linkWayObj = neuSpread1_Sheet1.Rows[i].Tag as FS.HISFC.Models.HealthRecord.Visit.LinkWay;//Tag�еĶ���
                    checkCount += 1;
                }
            }

            if (checkCount != 1)
            {
                MessageBox.Show("������ֻ��ѡ��һ����ϵ��");
                return -1;
            }

            visitRecordObj.Patient.PID.CardNO = CaseBase.PatientInfo.PID.CardNO;
            visitRecordObj.Circs.ID = ((FS.FrameWork.Models.NeuObject)cmbCircs.SelectedItem)
                == null ? "" : ((FS.FrameWork.Models.NeuObject)cmbCircs.SelectedItem).ID;
            visitRecordObj.DeadReason.ID = ((FS.FrameWork.Models.NeuObject)neuComboBoxDeadReason.SelectedItem)
                == null ? "" : ((FS.FrameWork.Models.NeuObject)neuComboBoxDeadReason.SelectedItem).ID;
            if (neuCheckBoxIsDead.Checked)
            {
                visitRecordObj.DeadTime = neuDateTimePickerDeadTime.Value;
            }
            visitRecordObj.IsDead = neuCheckBoxIsDead.Checked;
            visitRecordObj.IsRecrudesce = neuCheckBoxIsRecrudesce.Checked;
            visitRecordObj.IsSequela = neuCheckBoxIsSequela.Checked;
            visitRecordObj.VisitResult.ID = ((FS.FrameWork.Models.NeuObject)cmbResult.SelectedItem)
                == null ? "" : ((FS.FrameWork.Models.NeuObject)cmbResult.SelectedItem).ID;
            visitRecordObj.IsTransfer = neuCheckBoxIsTtransfer.Checked;

            if (neuCheckBoxIsRecrudesce.Checked)
            {
                visitRecordObj.RecrudesceTime = neuDateTimePickerRecrudesceTime.Value;
            }
            visitRecordObj.ResultOper.OperTime = System.DateTime.Now;
            visitRecordObj.VisitOper.OperTime = System.DateTime.Now;
            visitRecordObj.ResultOper.ID = FS.FrameWork.Management.Connection.Operator.ID;
            visitRecordObj.VisitOper.ID = FS.FrameWork.Management.Connection.Operator.ID;

            visitRecordObj.Sequela.ID = ((FS.FrameWork.Models.NeuObject)neuComboBoxSequela.SelectedItem)
                == null ? "" : ((FS.FrameWork.Models.NeuObject)neuComboBoxSequela.SelectedItem).ID;
            visitRecordObj.Symptom.ID = ((FS.FrameWork.Models.NeuObject)cmbSymptom.SelectedItem)
                == null ? "" : ((FS.FrameWork.Models.NeuObject)cmbSymptom.SelectedItem).ID;
            visitRecordObj.TransferPosition.ID = ((FS.FrameWork.Models.NeuObject)neuComboBoxTransferPosition.SelectedItem)
                == null ? "" : ((FS.FrameWork.Models.NeuObject)neuComboBoxTransferPosition.SelectedItem).ID;
            visitRecordObj.VisitType.ID = ((FS.FrameWork.Models.NeuObject)cmbLinkType.SelectedItem)
                == null ? "" : ((FS.FrameWork.Models.NeuObject)cmbLinkType.SelectedItem).ID;

            visitRecordObj.WriteBackPerson = txtWritebackPerson.Text;

            visitRecordObj.User01 = txtContent.Text.Trim();//�����ϸ����

            for (int i = 0; i < neuSpread1_Sheet1.Rows.Count; i++)
            {
                if (this.neuSpread1_Sheet1.Cells[i, 0].Text == true.ToString())
                {
                    //�����ϸ��ϵ��ʽ
                    visitRecordObj.LinkWay.LinkMan.Name = this.neuSpread1_Sheet1.Cells[i, 1].Text;//��ϵ������
                    visitRecordObj.LinkWay.Relation.ID = this.neuSpread1_Sheet1.Cells[i, 2].Text;
                    visitRecordObj.LinkWay.Phone = this.neuSpread1_Sheet1.Cells[i, 3].Text;
                    visitRecordObj.LinkWay.OtherLinkway = this.neuSpread1_Sheet1.Cells[i, 4].Text;//�绰״̬
                    visitRecordObj.LinkWay.Mail = this.neuSpread1_Sheet1.Cells[i, 6].Text;
                    visitRecordObj.LinkWay.Address = this.neuSpread1_Sheet1.Cells[i, 5].Text;


                    //ĩ�������ϵ��ʽ
                    visitObj.Patient.PID.CardNO = CaseBase.PatientInfo.PID.CardNO;
                    visitObj.Linkway.Address = this.neuSpread1_Sheet1.Cells[i, 5].Text;
                    visitObj.Linkway.Mail = this.neuSpread1_Sheet1.Cells[i, 6].Text;
                    visitObj.Linkway.Phone = this.neuSpread1_Sheet1.Cells[i, 3].Text;
                    visitObj.LastVisitTime = System.DateTime.Now;
                    visitObj.Linkway.LinkWayType.ID = ((FS.FrameWork.Models.NeuObject)cmbLinkType.SelectedItem)
                == null ? "" : ((FS.FrameWork.Models.NeuObject)cmbLinkType.SelectedItem).ID;


                    visitObj.Linkway.LinkMan.Name = this.neuSpread1_Sheet1.Cells[i, 1].Text;
                    visitObj.Linkway.Relation.ID = this.neuSpread1_Sheet1.Cells[i, 2].Text;

                    if (rdbNormal.Checked)
                    {
                        visitObj.VisitState = FS.HISFC.Models.HealthRecord.Visit.EnumVisitState.Normal;
                    }
                    if (rdbStop.Checked)
                    {
                        visitObj.VisitState = FS.HISFC.Models.HealthRecord.Visit.EnumVisitState.Stop;
                    }


                }
            }

            return 0;
        }

        private void neuCheckBoxIsRecrudesce_CheckedChanged(object sender, EventArgs e)
        {
            this.neuDateTimePickerRecrudesceTime.Enabled = this.neuCheckBoxIsRecrudesce.Checked;
            if (this.neuCheckBoxIsRecrudesce.Checked)
            {
                this.neuDateTimePickerRecrudesceTime.Value = visitIntergrate.GetCurrentDateTime();
            }
            else
            {
                this.neuDateTimePickerRecrudesceTime.Value = this.neuDateTimePickerRecrudesceTime.MinDate;
            }
        }

        private void neuCheckBoxIsSequela_CheckedChanged(object sender, EventArgs e)
        {
            this.neuComboBoxSequela.Enabled = this.neuCheckBoxIsSequela.Checked;
        }

        private void neuCheckBoxIsTtransfer_CheckedChanged(object sender, EventArgs e)
        {
            this.neuComboBoxTransferPosition.Enabled = this.neuCheckBoxIsTtransfer.Checked;
        }

        /// <summary>
        /// ���ص绰״̬�б�
        /// </summary>
        private void InitTelStateList()
        {
            telStateBox = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
            ArrayList telStateList = new ArrayList();
            telStateList = con.GetList("TELSTATE");

            string[] s = new string[telStateList.Count];
            for (int i = 0; i < telStateList.Count; i++)
            {
                s[i] = ((FS.FrameWork.Models.NeuObject)telStateList[i]).Name.ToString();
            }

            telStateBox.Items = s;
            telStateBox.Editable = true;
            this.neuSpread1_Sheet1.Columns[4].CellType = telStateBox;
        }

        /// <summary>
        /// �����뻼�߹�ϵ�б�
        /// </summary>
        private void IninRelation()
        {
            ArrayList RelationList = con.GetList(FS.HISFC.Models.Base.EnumConstant.RELATIVE);

            relationBox = new FarPoint.Win.Spread.CellType.ComboBoxCellType();


            string[] s = new string[RelationList.Count];
            for (int i = 0; i < RelationList.Count; i++)
            {
                s[i] = ((FS.FrameWork.Models.NeuObject)RelationList[i]).Name.ToString();
            }

            relationBox.Items = s;
            this.neuSpread1_Sheet1.Columns[2].CellType = relationBox;

        }




        #endregion

        public override int Exit(object sender, object neuObject)
        {
            return base.Exit(sender, neuObject);

        }


        #region IInterfaceContainer ��Ա

        //{DC8452B8-FF77-4639-9522-A2CCED4B8A5C}
        Type[] FS.FrameWork.WinForms.Forms.IInterfaceContainer.InterfaceTypes
        {
            get
            {
                //return new Type[] { typeof(FS.HISFC.BizProcess.Integrate.HealthRecord.HealthRecordInterface) }; ; 
                Type[] t = new Type[2];
                t[0] = typeof(FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterface);
                t[1] = typeof(FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterfaceBack);//ת������
                return t;
            }
        }
        #endregion

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {

            //if (keyData == Keys.Enter)
            //{
            //    SendKeys.Send("{Tab}");
            //}
            //������Ϣ
            if (keyData == Keys.F2)
            {
                this.tab1.SelectedTab = this.tabPage1;
            }
            //�����Ϣ
            if (keyData == Keys.F3)
            {
                this.tab1.SelectedTab = this.tabPage5;
            }
            //������Ϣ
            if (keyData == Keys.F4)
            {
                this.tab1.SelectedTab = this.tabPage6;
            }
            //��Ӥ��Ϣ
            if (keyData == Keys.F5)
            {
                this.tab1.SelectedTab = this.tabPage2;
            }
            //ת����Ϣ
            if (keyData == Keys.F6)
            {
                this.tab1.SelectedTab = this.tabPage3;
            }
            //������Ϣ
            if (keyData == Keys.F7)
            {
                this.tab1.SelectedTab = this.tabPage7;
            }
            //������Ϣ
            if (keyData == Keys.F8)
            {
                this.tab1.SelectedTab = this.tabPage4;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }


        private void txtNameSearch_Enter(object sender, EventArgs e)
        {
            this.ucPatient.Location = new Point(this.txtNameSearch.Location.X, this.txtNameSearch.Location.Y + this.txtNameSearch.Height + 2);
            this.ucPatient.BringToFront();
            this.ucPatient.Visible = false;
        }

        private void txtNameSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                try
                {
                    if (txtNameSearch.Text == "")
                    {
                        MessageBox.Show("�����뻼������");
                        return;
                    }
                    #region ���
                    HandCraft = 0;
                    this.CaseBase = null;
                    ClearInfo();
                    #endregion
                    if (!this.ucPatient.Visible)
                    {
                        #region ��ѯ
                        ArrayList list = null;
                        list = ucPatient.Init(txtNameSearch.Text.Trim(), "3");
                        if (list == null)
                        {
                            MessageBox.Show("��ѯʧ��" + ucPatient.strErr);
                            return;
                        }
                        if (list.Count == 0)
                        {
                            if (frmType == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.CAS)
                            {
                                #region �������Լ��ֹ�¼�벡��
                                if (MessageBox.Show("û�в鵽��ز�����Ϣ,�Ƿ��ֹ�¼�벡��", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    txtPatientName.Text = txtNameSearch.Text;
                                    txtCaseNum.Focus();
                                    SetHandcraft();
                                }
                                else
                                {
                                    return;
                                }
                                #endregion
                            }
                            else
                            {
                                MessageBox.Show("û�в鵽��ز�����Ϣ");
                                return;
                            }
                        }
                        else if (list.Count == 1) //ֻ��һ����Ϣ
                        {
                            ucPatient.Visible = false;
                            FS.HISFC.Models.HealthRecord.Base obj = this.ucPatient.GetCaseInfo();
                            if (obj != null)
                            {
                                LoadInfo(obj.PatientInfo.ID, this.frmType);
                                this.txtCaseNum.Focus();
                            }
                        }
                        else //������Ϣ 
                        {
                            ucPatient.Visible = true;
                        }
                        #endregion
                    }
                    else
                    {
                        #region  ѡ��
                        FS.HISFC.Models.HealthRecord.Base obj = this.ucPatient.GetCaseInfo();
                        if (obj != null)
                        {
                            LoadInfo(obj.PatientInfo.ID, this.frmType);
                            this.txtCaseNum.Focus();
                        }
                        this.ucPatient.Visible = false;
                        #endregion
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (e.KeyCode == Keys.Up)
            {
                ucPatient.PriorRow();
            }
            else if (e.KeyCode == Keys.Down)
            {
                ucPatient.NextRow();
            }
        }

        private void txtPhoneHome_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtInTimes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                txtPactKind.Focus();
            }
        }

        private void txtDateOut_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtInjuryOrPoisoningCause_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtInfectNum.Focus();
            }
        }

        private void txtInfectionDiseasesReport_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtFourDiseasesReport.Focus();
            }
        }

        private void txtFourDiseasesReport_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtCheckDate.Focus();
            }
        }

        private void dtPatientBirthday_ValueChanged(object sender, EventArgs e)
        {
            //string age = FS.FrameWork.Public.String.GetAge(this.dtPatientBirthday.Value, baseDml.GetDateTimeFromSysDateTime());
            //this.txtPatientAge.Text = age.Substring(0, age.Length - 1);
            //this.cmbUnit.Text = age.Substring(age.Length - 1);
            this.txtPatientAge.Text = this.baseDml.GetAgeByFun(dtPatientBirthday.Value.Date, this.CaseBase.PatientInfo.PVisit.InTime.Date);
        }

        private void txtDateOut_ValueChanged(object sender, EventArgs e)
        {
            System.TimeSpan tt = this.txtDateOut.Value.Date - this.dtDateIn.Value.Date;
            if (tt.Days == 0)
            {
                this.txtPiDays.Text = "1";
            }
            else
            {
                this.txtPiDays.Text = Convert.ToString(tt.Days);
            }
            this.txtDeptOut.Focus();
        }
        
        /// <summary>
        /// ��ȡ�ؼ�����һ����������������������
        /// </summary>
        /// <param name="inPatient"></param>
        private void GetNursingNum(string inPatient)
        {
            int tempT = this.baseDml.GetNursingNum(inPatient, "TJHL");//�ؼ�����
            int tempY = this.baseDml.GetNursingNum(inPatient, "YJHL");//һ������
            int tempE = this.baseDml.GetNursingNum(inPatient, "EJHL");//��������
            int tempS = this.baseDml.GetNursingNum(inPatient, "SJHL");//��������
            int tempTS = this.baseDml.GetNursingNum(inPatient, "TSHL");//���⻤��
            int tempZZ = this.baseDml.GetNursingNum(inPatient, "ZZJH");//��֢�໤

            this.txtSuperNus.Text = tempT.ToString();
            this.txtINus.Text = tempY.ToString();
            this.txtIINus.Text = tempE.ToString();
            this.txtIIINus.Text = tempS.ToString();
            this.txtSPecalNus.Text = tempTS.ToString();
            this.txtStrictNuss.Text = tempZZ.ToString();

        }

        /// <summary>
        /// ��ȡҩ�����ʷ
        /// </summary>
        /// <param name="inPatient"></param>
        /// <param name="frmType"></param>
        private void PharmacyAllergicLoadInfo(string inPatient, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes frmType)
        {
            FS.HISFC.BizLogic.HealthRecord.Diagnose diagInfo = new FS.HISFC.BizLogic.HealthRecord.Diagnose();
            List<FS.FrameWork.Models.NeuObject> inDiagnose = new List<FS.FrameWork.Models.NeuObject>();

            if (frmType == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC)
            {
                inDiagnose = diagInfo.QueryPharmacyAllergicFromInCaseByInpatient(inPatient);
                if (inDiagnose != null)
                {
                    if (inDiagnose.Count == 0)
                    {
                        this.neuTxtPharmacyAllergic1.Text = "δ����";
                    }
                    else
                    {
                        foreach (FS.FrameWork.Models.NeuObject info in inDiagnose)
                        {
                            this.neuTxtPharmacyAllergic1.Text = info.Memo.ToString();
                        }
                    }
                }
            }
        }
    }


}
