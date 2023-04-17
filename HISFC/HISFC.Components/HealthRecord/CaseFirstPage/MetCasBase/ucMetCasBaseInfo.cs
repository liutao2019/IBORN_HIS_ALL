using System;
using System.Linq;
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
    public partial class ucMetCasBaseInfo : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.FrameWork.WinForms.Forms.IInterfaceContainer
    {

        /// <summary>
        /// /// ucMetCasBaseInfo<br></br>
        /// [��������: ����������Ϣ¼��]<br></br>
        /// [�� �� ��: �ſ���]<br></br>
        /// [����ʱ��: 2007-04-20]<br></br>
        /// </summary>
        public ucMetCasBaseInfo()
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
            this.ucTumourCard1.InitInfo();
            #endregion

            #region  ת��
            //thread = new System.Threading.Thread(this.ucChangeDept1.InitInfo);
            //thread.Start(); 
            //this.ucChangeDept1.InitInfo();
            #endregion

            #region  �����Ϣ
            //thread = new System.Threading.Thread(this.ucDiagNoseInput1.InitInfo);
            //thread.Start();  
            this.ucDiagNoseInput1.InitInfo();
            #endregion

            #endregion

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
        [Category("�����Ժ����"), Description("����Ҫ��ѯ��ʱ��Σ�Ĭ��Ϊ3��")]
        /// <summary>
        /// ����  ������ҳ�����ʾ��Ժ���˵�ʱ���
        /// �÷���ֱ����������
        /// </summary>
        private int days = 3;
        public int DAYs
        {
            get { return days; }
            set { days = value; }
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


        #region  ȫ�ֱ���
        //��־ ��ʶ��ҽ��վ�û��ǲ�������
        private FS.HISFC.Models.HealthRecord.EnumServer.frmTypes frmType = FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC;
        //�ݴ浱ǰ�޸��˵Ĳ���������Ϣ
        private FS.HISFC.Models.HealthRecord.Base CaseBase = new FS.HISFC.Models.HealthRecord.Base();
        //����������Ϣ������
        private FS.HISFC.BizLogic.HealthRecord.Base baseDml = new FS.HISFC.BizLogic.HealthRecord.Base();
        //add ren
        private FS.FrameWork.WinForms.Controls.PopUpListBox Drr = new FS.FrameWork.WinForms.Controls.PopUpListBox();
        private FS.FrameWork.Public.ObjectHelper DrrHelper = new FS.FrameWork.Public.ObjectHelper();
        //add chengym ��ʱסԺ��ˮ�ţ�LoadInfo�������ж��Ƿ�ͬһ�����ߣ���ͬ�������»�ȡ����
        private string TempInpatient = string.Empty;
        private bool isNeedLoadInfo = false;
        private string in_State = string.Empty;//��¼����Ŀǰ״̬ loadinfoʱ������ֵ
        private DateTime dt_out = System.DateTime.Now.AddYears(-1);//��Ժ����
        private string bedNo = string.Empty;//��Ժ��λ
        private string dept_out = string.Empty;//סԺ�����Ժ����
        private string dutyNurse = string.Empty;//���λ�ʿ
        private string patient_no = string.Empty;//סԺ�� ����ĺŵ����2013-1-17
        //end add chengym
        //�������
        FS.HISFC.BizLogic.Manager.Constant con = new FS.HISFC.BizLogic.Manager.Constant();
        //������� 
        private FS.HISFC.Models.HealthRecord.Diagnose clinicDiag = null;
        //��Ժ��� 
        private FS.HISFC.Models.HealthRecord.Diagnose InDiag = null;
        //��ͬ��λ
        private FS.HISFC.BizLogic.Fee.PactUnitInfo pactManager = new FS.HISFC.BizLogic.Fee.PactUnitInfo();
        //ת����Ϣ
        ArrayList changeDeptList = new ArrayList();
        FS.HISFC.BizLogic.HealthRecord.DeptShift deptChange = new FS.HISFC.BizLogic.HealthRecord.DeptShift();
        FS.HISFC.BizLogic.HealthRecord.Fee healthRecordFee = new FS.HISFC.BizLogic.HealthRecord.Fee();
        HealthRecord.Search.ucPatientList ucPatient = new HealthRecord.Search.ucPatientList();
        //��ʶ�ֹ������״̬�ǲ��뻹�Ǹ���  0Ĭ��״̬  1  ���� 2����  
        private int HandCraft = 0;
        //���没����״̬
        private int CaseFlag = 0;
        //��ʾ����
        ucDiagNoseCheck frm = null;
        private FS.FrameWork.Models.NeuObject localObj = new FS.FrameWork.Models.NeuObject();
        //��ӡ�ӿ� 
        private FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterface healthRecordPrint = null;//��ӡ����
        private FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterfaceBack healthRecordPrintBack = null;//���� 
        private FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterfaceAdditional healthRecordInterfaceAdditional = null;//��ӡ����ҳ
        //{B71C3094-BDC8-4fe8-A6F1-7CEB2AEC55DD}
        private FS.HISFC.BizProcess.Integrate.Manager manageIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();
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
        /// <summary>
        /// ʹ�����µ��Ӳ�����ȡ
        /// ��ʾ������ʱcmb�ؼ�textδ��ʾ����
        /// </summary>
        private bool isNewEMR = false;
        /// <summary>
        /// ��ӡ��ҳ��
        /// Ĭ�ϰ��㶫ʡ��׼��ӡ3�� 
        /// �����ó�����CASEPRINTTOWP����ӡ��ҳ��ȡ����ӡ���ã�
        /// </summary>
        private int PrintPageNum = 3;
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
            #region ����ѡ���
            this.Controls.Add(this.ucPatient);
            this.ucPatient.BringToFront();
            this.ucPatient.Visible = false;
            this.ucPatient.SelectItem += new HealthRecord.Search.ucPatientList.ListShowdelegate(ucPatient_SelectItem);
            #endregion

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
            this.neuGroupBox3.Visible = false;//drgsǰ�Ĳ�����ҳ�ֶβ���ʾ
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
            //{DC8452B8-FF77-4639-9522-A2CCED4B8A5C}
            toolBarService.AddToolButton("��ӡ�ڶ�ҳ", "��ӡ�ڶ�ҳ", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D��ӡ, true, false, null);

            toolBarService.AddToolButton("�ύ", "�ύ", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Zִ��, true, false, null);
            this.toolBarService.AddToolButton("ˢ��", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Sˢ��, true, false, null);
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
                case "ˢ��":
                    //this.Save(2);
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
            //ҽ�Ƹ��ѷ�ʽ 
            ArrayList pactKindlist = con.GetList("CASEPACT");
            this.cmbPactKind.AddItems(pactKindlist);
            //��ȡ�Ա��б�
            //ArrayList sexList = con.GetList("CASESEX");
            ArrayList sexList = FS.HISFC.Models.Base.SexEnumService.List();
            this.cmbPatientSex.AddItems(sexList);
            //��ѯ�����б�
            ArrayList countryList = con.GetList(FS.HISFC.Models.Base.EnumConstant.COUNTRY);
            this.cmbCountry.AddItems(countryList);
            //��ѯ������ַ�б�
            ArrayList AddrList = con.GetList("CASEADDR");
            //�޸ļ��ᡢ������
            ArrayList addrlist = con.GetList("GBXZQYHF");
            this.cmbBirthAddr.AddItems(addrlist);//������
            this.cmbDist.AddItems(addrlist);//����
            this.cmbCurrentAdrr.AddItems(addrlist);//��סַ
            this.cmbHomeAdrr.AddItems(addrlist);//��ͥסַ

            //��ѯ�����б�
            ArrayList Nationallist1 = con.GetList(FS.HISFC.Models.Base.EnumConstant.NATION);
            this.cmbNationality.AddItems(Nationallist1);

            //��ѯְҵ�б�
            //ArrayList Professionlist = con.GetList(FS.HISFC.Models.Base.EnumConstant.PROFESSION);
            ArrayList Professionlist = con.GetList("CASEPROFESSION");
            this.cmbProfession.AddItems(Professionlist);
            //�����б�
            ArrayList MaritalStatusList = new ArrayList();
            MaritalStatusList = con.GetList("CASEMARITAL");
            if (MaritalStatusList == null || MaritalStatusList.Count == 0)
            {
                MaritalStatusList = FS.HISFC.Models.RADT.MaritalStatusEnumService.List();//con.GetList("CASEMARITAL"); 
            }
            this.cmbMaritalStatus.AddItems(MaritalStatusList);
            //����ϵ�˹�ϵ
            ArrayList RelationList = con.GetList(FS.HISFC.Models.Base.EnumConstant.RELATIVE);
            this.cmbRelation.AddItems(RelationList);
            //��ȡ��Ժ;���б�
            ArrayList inAvenueList = con.GetAllList("INSOURCE");
            this.cmbInPath.AddItems(inAvenueList);

            //���������б�
            FS.HISFC.BizLogic.Manager.Department dept = new FS.HISFC.BizLogic.Manager.Department();
            ArrayList deptList = dept.GetDeptmentAll();
            cmbDeptInHospital.AddItems(deptList);
            cmbDeptOutHospital.AddItems(deptList);
            txtFirstDept.AddItems(deptList);
            txtDeptSecond.AddItems(deptList);
            txtDeptThird.AddItems(deptList);

            //��ȡҽ���б�
            FS.HISFC.BizLogic.Manager.Person person = new FS.HISFC.BizLogic.Manager.Person();
            ArrayList DoctorList = person.GetEmployeeAll();//.GetEmployee(FS.HISFC.Models.RADT.PersonType.enuPersonType.D);
            FS.HISFC.Models.Base.Employee tempEmpl = new FS.HISFC.Models.Base.Employee();
            //��ҽ��Ժǿ��Ҫ�� ��Э��
            tempEmpl.ID = "-";
            tempEmpl.Name = "-";
            tempEmpl.SpellCode = "��";
            tempEmpl.WBCode = "-";
            DoctorList.Add(tempEmpl);
            this.cmbAdmittingDoctor.AddItems(DoctorList);
            this.cmbAttendingDoctor.AddItems(DoctorList);
            this.cmbConsultingDoctor.AddItems(DoctorList);
            this.cmbRefresherDocd.AddItems(DoctorList);
            //this.cmbRefresherDocd.Text = "-";
            cmbDutyNurse.AddItems(DoctorList);
            cmbQcDocd.AddItems(DoctorList);
            cmbQcNucd.AddItems(DoctorList);
            txtCodingCode.AddItems(DoctorList);
            this.cmbPraDocCode.AddItems(DoctorList);
            //this.cmbPraDocCode.Text = "-";
            this.cmbDeptChiefDoc.AddItems(DoctorList);
            this.txtClinicDocd.AddItems(DoctorList);//����ҽ��
            //��������
            ArrayList ExampleTypeList = con.GetList("CASEEXAMPLETYPE");
            this.cmbExampleType.AddItems(ExampleTypeList);
            //Ѫ���б�
            ArrayList BloodTypeList = con.GetList("CASEBLOODTYPE");//con.GetList(FS.HISFC.Models.Base.EnumConstant.BLOODTYPE);//baseDml.GetBloodType();
            this.cmbBloodType.AddItems(BloodTypeList);
            //RH���� 
            ArrayList RHList = con.GetList(FS.HISFC.Models.Base.EnumConstant.RHSTATE); //baseDml.GetRHType();
            cmbRhBlood.AddItems(RHList);


            //��������
            ArrayList qcList = con.GetList(FS.HISFC.Models.Base.EnumConstant.CASEQUALITY);
            cmbMrQual.AddItems(qcList);

            //�����ж��б�
            FS.HISFC.BizLogic.HealthRecord.ICD icd = new FS.HISFC.BizLogic.HealthRecord.ICD();
            //ArrayList listIcd = icd.Query(FS.HISFC.Models.HealthRecord.EnumServer.ICDTypes.ICD10, FS.HISFC.Models.HealthRecord.EnumServer.QueryTypes.Valid);
            //�����ж� ��Χ�ǣ�V01-Y98
            ArrayList listIcd = icd.QueryDrgs(FS.HISFC.Models.HealthRecord.EnumServer.ICDTypes.ICD10);
            ArrayList injuryOrPoisoningCauseList = new ArrayList();
            ArrayList otherIcdList = new ArrayList();
            if (listIcd != null)
            {
                foreach (FS.HISFC.Models.HealthRecord.ICD info in listIcd)
                {
                    //info.ID.IndexOf('U') == 0 || || info.ID.IndexOf('Z') == 0ȡ�� 2012-10-22 chengym
                    if (info.ID.IndexOf('V') == 0 || info.ID.IndexOf('W') == 0 || info.ID.IndexOf('X') == 0 || info.ID.IndexOf('Y') == 0)
                    {
                        injuryOrPoisoningCauseList.Add(info);
                    }
                    else
                    {
                        if (info.ID.IndexOf("U") != 0)
                        {
                            otherIcdList.Add(info);
                        }
                    }
                }
            }
            FS.HISFC.Models.HealthRecord.ICD icdInfo = new FS.HISFC.Models.HealthRecord.ICD();
            icdInfo.ID = "-";
            icdInfo.Name = "δ����";
            icdInfo.SpellCode = "WFX";
            icdInfo.WBCode = "FNG";
            injuryOrPoisoningCauseList.Add(icdInfo);
            otherIcdList.Add(icdInfo);
            icdInfo = new FS.HISFC.Models.HealthRecord.ICD();
            icdInfo.ID = "��";
            icdInfo.Name = "��";
            icdInfo.SpellCode = "-";
            icdInfo.WBCode = "��";
            injuryOrPoisoningCauseList.Add(icdInfo);
            otherIcdList.Add(icdInfo);
            this.txtInjuryOrPoisoningCause.AddItems(injuryOrPoisoningCauseList);//�����ж�
            this.cmbClinicDiagName.AddItems(otherIcdList);//�������
            this.cmbPathologicalDiagName.AddItems(otherIcdList);//�������
            //����״̬
            this.caseStusHelper = new FS.FrameWork.Public.ObjectHelper(con.GetList("CASESTUS"));

            //1 �� 2 �� �����б�
            ArrayList notOrHavedList = con.GetList("CASENOTORHAVED");
            this.cmbIsDrugAllergy.AddItems(notOrHavedList);//ҩ�����
            this.cmbComeBackInMonth.AddItems(notOrHavedList);//�Ƿ��Ժ31������סԺ

            //1�� 2�� �����б�
            ArrayList yseOrNoList = con.GetList("CASEYSEORNO");
            this.cmbClinicPath.AddItems(yseOrNoList);//�ٴ�·������
            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = "-";
            obj.Name = "-";
            yseOrNoList.Add(obj);
            this.cmbDeathPatientBobyCheck.AddItems(yseOrNoList);//��������ʬ��

            //���ݾɰ汾
            this.cmbYnFirst.AddItems(yseOrNoList);//�Ƿ��ײ���
            this.cmbVisiStat.AddItems(yseOrNoList);//�Ƿ����
            this.cmbTechSerc.AddItems(yseOrNoList);//�Ƿ�ʾ�̲���
            this.cmbDisease30.AddItems(yseOrNoList);//�Ƿ񵥲���
            //��ȡ������Դ
            ArrayList Insourcelist = con.GetAllList("PATIENTINSOURCE");
            this.txtInAvenue.AddItems(Insourcelist);
            //��Ժ���
            ArrayList CircsList = con.GetList(FS.HISFC.Models.Base.EnumConstant.INCIRCS);
            this.txtCircs.AddItems(CircsList);
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
            //ѪҺ��Ӧ
            ArrayList ReactionBloodList = con.GetList(FS.HISFC.Models.Base.EnumConstant.BLOODREACTION);// baseDml.GetReactionBlood();
            txtReactionBlood.AddItems(ReactionBloodList);
            txtReactionTransfuse.AddItems(ReactionBloodList);//��Һ��Ӧ
            //��Ⱦ���ϱ�
            ArrayList ReportedList = con.GetList("Reported");
            this.txtFourDiseasesReport.AddItems(ReportedList);
            this.txtInfectionDiseasesReport.AddItems(ReportedList);
            //��ӡҳ��
            ArrayList PrintPageNumList = con.GetList("CASEPRINTTOWP");
            if (PrintPageNumList != null && PrintPageNumList.Count > 0)
            {
                this.PrintPageNum = 2;
            }
            ArrayList alTemp = new ArrayList();
            //�ʱ�
            ArrayList alDist = con.GetList("CASEDIST");
            //ArrayList alDist = con.GetList("ZIP");
            foreach (FS.HISFC.Models.Base.Spell s in alDist)
            {
                FS.HISFC.Models.Base.Spell sClone = s.Clone();

                sClone.ID = sClone.Name;
                sClone.Name = sClone.UserCode;

                alTemp.Add(sClone);
            }
            this.txtCurrentZip.AddItems(alTemp);
            this.txtBusinessZip.AddItems(alTemp);
            this.txtHomeZip.AddItems(alTemp);
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
            //TreeNode tnTJParent;//���ύ�����б�
            this.treeView1.HideSelection = false;
            //Neuosft.FS.HISFC.BizProcess.Integrate.RADT pQuery = new FS.HISFC.BizProcess.Integrate.RADT(); //t.RADT.InPatient();
            this.treeView1.BeginUpdate();
            this.treeView1.Nodes.Clear();
            //����ͷ
            tnParent = new TreeNode();
            tnParent.Text = "���ҽ���ύ��Ժ����";
            tnParent.Tag = "%";
            try
            {
                tnParent.ImageIndex = 0;
                tnParent.SelectedImageIndex = 1;
            }
            catch { }
            this.treeView1.Nodes.Add(tnParent);
            ////��tnTJParent��ͷ
            //tnTJParent = new TreeNode();
            //tnTJParent.Text = "���ҽ���ύ�Ļ���";
            //tnTJParent.Tag = "%";
            //try
            //{
            //    tnTJParent.ImageIndex = 4;
            //    tnTJParent.SelectedImageIndex = 5;
            //}
            //catch { }
            //this.treeView1.Nodes.Add(tnTJParent);//�����ύ�����б�
            DateTime dt = this.baseDml.GetDateTimeFromSysDateTime();
            DateTime dt2 = dt.AddDays(-days);
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
                //TreeNode tnTJPatient = new TreeNode();

                tnPatient.Text = pInfo.Name + "[" + pInfo.PID.PatientNO + "]";
                tnPatient.Tag = pInfo;
                //ArrayList CaseBase = baseDml.GetCaseBaseInfo(dt,dt2);//����������Ϣ��met_cas_base��
                //foreach (FS.HISFC.Models.HealthRecord.Base Casebase in CaseBase)
                //{
                //    if (FS.FrameWork.Function.NConvert.ToInt32(Casebase.PatientInfo.CaseState) == 2)
                //    {
                //        tnTJPatient.Text = pInfo.Name + "[" + pInfo.PID.PatientNO + "]";//
                //        tnTJPatient.Tag = pInfo;//
                //    }
                //    else
                //    {
                //        this.treeView1.EndUpdate();
                //    }
                //}
                try
                {
                    tnPatient.ImageIndex = 2;
                    tnPatient.SelectedImageIndex = 3;
                    //    tnTJPatient.ImageIndex = 6;///
                    //    tnTJPatient.SelectedImageIndex = 7;//
                }
                catch { }
                tnParent.Nodes.Add(tnPatient);
                //tnTJParent.Nodes.Add(tnTJPatient);//
            }

            tnParent.Expand();
            //tnTJParent.Expand();//
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
        /// ��д���溯��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        public int EmrSave(object sender, object neuObject)
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
            //if (type == 1)
            //{//�ύʱ������
            //    if (this.PrintFristCheck() == -1)
            //    {
            //        return -1;
            //    }
            //}
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
            #region  �ж�����Ƿ����Լ��
            FS.HISFC.BizLogic.Manager.Constant con = new FS.HISFC.BizLogic.Manager.Constant();
            //�������ϼ�������û�о����о�������Ҫ�������ƣ�������ЩҽԺ���±������⣬���������Ρ�
            ArrayList alDiagCheck = con.GetList("CASEDIAGCHECK");
            FS.HISFC.BizLogic.HealthRecord.Diagnose diagNose = new FS.HISFC.BizLogic.HealthRecord.Diagnose();
            if (alDiagCheck != null && alDiagCheck.Count != 0)
            {
                if (this.frmType == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC) //ҽ��վ��ʾ �����Ҳ���Ҫ��ʾ
                {
                    if (DiagValueState(diagNose) != 1)
                    {
                        return -1;
                    }
                }
            }
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

                baseDml.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                diagNose.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                healthRecordFee.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                deptChange.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

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
                    if (frmRemark.DialogResult == DialogResult.No)
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
                this.ucDiagNoseInput1.Patient = info.PatientInfo;
                this.ucOperation1.Patient = info.PatientInfo;
                this.ucBabyCardInput1.Patient = info.PatientInfo;
                this.ucTumourCard1.Patient = info.PatientInfo;
                this.ucFeeInfo1.Patient = info.PatientInfo;

                #endregion
                #region ת�ƿƱ�
                if (changeDeptList != null)
                {

                    if (deptChange.DeleteChangeDept(info.PatientInfo.ID) < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("ת����Ϣ����ʧ��" + baseDml.Err);
                        return -1;
                    }
                    foreach (FS.HISFC.Models.RADT.Location locationInfo in changeDeptList)
                    {
                        if (deptChange.InsertOrUpdate(locationInfo) < 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("ת����Ϣ����ʧ��" + baseDml.Err);
                            return -1;
                        }
                    }
                }
                #endregion
                #region  �������
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
                if (diagList != null && diagList.Count > 0)
                {
                    diagNose.DeleteDiagnoseAll(CaseBase.PatientInfo.ID, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC, FS.HISFC.Models.Base.ServiceTypes.I);
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
                ////��ʱ���������޸Ĺ������� ���ǲ���table changed��ģʽ����Ҫ�ݴ���
                //ArrayList tempDiag = diagNose.QueryCaseDiagnose(CaseBase.PatientInfo.ID, "%", frmType,FS.HISFC.Models.Base.ServiceTypes.I);
                #endregion
                #region  ������Ϣ
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
                operation.deleteAll(CaseBase.PatientInfo.ID);
                if (operationList != null && operationList.Count > 0)
                {
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
                //ArrayList tempOperation = operation.QueryOperation(this.frmType, CaseBase.PatientInfo.ID);

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
                FS.HISFC.Models.HealthRecord.Tumour TumInfo = this.ucTumourCard1.GetTumourInfo();
                int m = this.ucTumourCard1.ValueTumourSate(TumInfo);
                tumour.DeleteTumour(TumInfo.InpatientNo);
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
                this.ucTumourCard1.deleteRow();
                this.ucTumourCard1.GetList("D", tumDel);
                this.ucTumourCard1.GetList("A", tumAdd);
                this.ucTumourCard1.GetList("M", tumMod);
                if (this.ucTumourCard1.ValueState(tumDel) == -1 || this.ucTumourCard1.ValueState(tumAdd) == -1 || this.ucTumourCard1.ValueState(tumMod) == -1)
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
                        if (obj.DrugInfo.Name != null && obj.DrugInfo.Name != "")
                        {
                            if (tumour.InsertTumourDetail(obj) < 1)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("����������Ϣʧ��" + tumour.Err);
                                return -1;
                            }
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
                #region  ������Ϣ ����ҳ���ٴ没��������Ϣ
                //ArrayList feeList = this.ucFeeInfo1.GetFeeInfoList();
                //if (this.ucFeeInfo1.ValueState(feeList) == -1)
                //{
                //    FS.FrameWork.Management.PublicTrans.RollBack();//����
                //    return -3;
                //}
                //if (feeList != null)
                //{
                //    foreach (FS.HISFC.Models.RADT.Patient obj in feeList)
                //    {
                //        obj.ID = this.CaseBase.PatientInfo.ID; //סԺ��ˮ��
                //        obj.User01 = this.CaseBase.PatientInfo.PVisit.OutTime.ToString(); //��Ժ����
                //        if (healthRecordFee.UpdateFeeInfo(obj) < 1)
                //        {
                //            FS.FrameWork.Management.PublicTrans.RollBack();
                //            MessageBox.Show("���������Ϣʧ��" + baseDml.Err);
                //            return -1;
                //        }
                //    }
                //}
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
                //this.ucChangeDept1.fpEnterSaveChanges(tempChangeDept);
                //this.ucDiagNoseInput1.fpEnterSaveChanges(tempDiag);
                //this.ucOperation1.fpEnterSaveChanges(tempOperation);
                this.ucTumourCard1.fpEnterSaveChanges(tempTumour);

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
                    Function fun = new Function();
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
                        if (CaseBase.PatientInfo.MaritalStatus.ID != null && fun.ReturnStringValue(CaseBase.PatientInfo.MaritalStatus.ID.ToString(), false) != string.Empty)
                        {
                            switch (CaseBase.PatientInfo.MaritalStatus.ID.ToString())
                            {
                                case "1":// δ��
                                    patientInfoForUpdate.MaritalStatus.ID = "S";//δ��
                                    break;
                                case "2"://�ѻ�
                                    patientInfoForUpdate.MaritalStatus.ID = "M";//�ѻ�
                                    break;
                                case "3"://ɥż
                                    patientInfoForUpdate.MaritalStatus.ID = "W";//ɥż
                                    break;
                                case "4"://���
                                    patientInfoForUpdate.MaritalStatus.ID = "D";//ʧ��
                                    break;
                                case "9":
                                    patientInfoForUpdate.MaritalStatus.ID = "";//     R�ٻ�
                                    break;
                                default:
                                    patientInfoForUpdate.MaritalStatus.ID = info.PatientInfo.MaritalStatus.ID.ToString();
                                    break;
                            }
                            //patientInfoForUpdate.MaritalStatus.ID = CaseBase.PatientInfo.MaritalStatus.ID.ToString(); //����״��
                        }
                        if (fun.ReturnStringValue(CaseBase.PatientInfo.Country.ID, false) != string.Empty)
                        {
                            patientInfoForUpdate.Country.ID = CaseBase.PatientInfo.Country.ID; //����
                        }
                        if (CaseBase.PatientInfo.BloodType.ID != null && fun.ReturnStringValue(CaseBase.PatientInfo.BloodType.ID.ToString(), false) != string.Empty)
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

                            MessageBox.Show("����com_patientinfo����!");

                            return -1;
                        }

                        if (baseDml.UpdatePatient(patientInfoForUpdate) < 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();

                            MessageBox.Show("����fin_ipr_inmaininfo����!");

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
                //MessageBox.Show("��ҳ����ɹ�");
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
                //������Ϣ
                case "tabPage3":
                    //���С���� ������һ��
                    if (this.ucOperation1.GetfpSpread1RowCount() == 0)
                    {
                        this.ucOperation1.AddRow();
                        this.ucOperation1.SetActiveCells();
                    }
                    break;
                // case "�����Ϣ":
                case "tabPage2":
                    if (this.ucDiagNoseInput1.GetfpSpreadRowCount() == 0)
                    {
                        this.ucDiagNoseInput1.AddRow();
                        this.ucDiagNoseInput1.SetActiveCells();
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
            #region
            try
            {
                //ҽ�Ƹ��ѷ�ʽ fin_ipr_inmaininfo��ȡ���ݽ���ͬ��λת������
                if (info.PatientInfo.CaseState == "1")
                {
                    FS.FrameWork.Models.NeuObject pactInfo = con.GetConstant("CASEPACTCHANGE", info.PatientInfo.Pact.ID);
                    if (pactInfo != null && pactInfo.Memo != "")
                    {
                        cmbPactKind.Tag = pactInfo.Memo;
                    }
                }
                else
                {
                    if (info.PatientInfo.Pact.PayKind.ID == "DRGS")
                    {
                        cmbPactKind.Tag = info.PatientInfo.Pact.ID;
                    }
                    else
                    {
                        FS.FrameWork.Models.NeuObject pactInfo = con.GetConstant("CASEPACTCHANGE", info.PatientInfo.Pact.ID);
                        if (pactInfo != null && pactInfo.Memo != "")
                        {
                            cmbPactKind.Tag = pactInfo.Memo;
                        }
                    }
                }
                //��������
                txtHealthyCard.Text = info.PatientInfo.SSN;
                //סԺ����
                if (info.PatientInfo.InTimes == 0)
                {
                    txtInTimes.Text = "1";
                }
                else
                {
                    txtInTimes.Text = info.PatientInfo.InTimes.ToString();
                }
                //������
                //if (info.CaseNO == "" || info.CaseNO == null)
                //{
                //    txtCaseNum.Text = info.PatientInfo.PID.PatientNO;
                //}
                //else
                //{
                //    txtCaseNum.Text = info.CaseNO;
                //}
                this.txtCaseNum.Text = this.patient_no;
                //����
                this.txtPatientName.Text = info.PatientInfo.Name;

                //�Ա�
                if (info.PatientInfo.Sex.ID != null)
                {
                    cmbPatientSex.Tag = info.PatientInfo.Sex.ID.ToString();
                }
                //��������
                if (info.PatientInfo.Birthday != System.DateTime.MinValue)
                {
                    dtPatientBirthday.Value = info.PatientInfo.Birthday;
                }
                else
                {
                    dtPatientBirthday.Value = System.DateTime.Now;
                }
                #region ����
                //���� �� ���䲻��һ����
                //Ϊ��֤��������Ӳ���һ�£�ֱ��ʹ�õ��Ӳ�����������㺯��fun_get_age_new
                if (info.PatientInfo.CaseState == "1")
                {

                    string strAge = string.Empty;
                    int age = 0;
                    if (this.isNewEMR)
                    {
                        strAge = this.baseDml.EmrGetAge(dtPatientBirthday.Value.Date, info.PatientInfo.PVisit.InTime.Date, ref age);
                    }
                    else
                    {
                        strAge = this.baseDml.GetAgeByFun(dtPatientBirthday.Value.Date, info.PatientInfo.PVisit.InTime.Date);
                    }
                    this.txtPatientAge.Text = strAge;
                    this.txtBabyAge.Text = strAge;
                }
                else
                {
                    if (info.AgeUnit == "0��")
                    {
                        string strAge1 = string.Empty;
                        int age1 = 0;
                        if (this.isNewEMR)
                        {
                            strAge1 = this.baseDml.EmrGetAge(dtPatientBirthday.Value.Date, info.PatientInfo.PVisit.InTime.Date, ref age1);
                        }
                        else
                        {
                            strAge1 = this.baseDml.GetAgeByFun(dtPatientBirthday.Value.Date, info.PatientInfo.PVisit.InTime.Date);
                        }
                        this.txtPatientAge.Text = strAge1;
                        this.txtBabyAge.Text = strAge1;
                    }
                    else
                    {
                        this.txtPatientAge.Text = info.AgeUnit;
                        this.txtBabyAge.Text = info.BabyAge;
                    }
                }
                //ת����ʾ��ʽ
                if (this.txtPatientAge.Text != "" && this.txtPatientAge.Text != "0")
                {
                    if (this.txtPatientAge.Text.IndexOf("��") > 0 && this.txtPatientAge.Text.IndexOf("��") < 0) //����
                    {
                        this.txtPatientAge.Text = "Y" + this.txtPatientAge.Text.Replace("��", "");
                    }
                    else if (this.txtPatientAge.Text.IndexOf("��") < 0 && this.txtPatientAge.Text.IndexOf("��") > 0 && this.txtPatientAge.Text.IndexOf("��") < 0)//����
                    {
                        this.txtPatientAge.Text = "M" + this.txtPatientAge.Text.Replace("��", "");
                    }
                    else if (this.txtPatientAge.Text.IndexOf("��") < 0 && this.txtPatientAge.Text.IndexOf("��") < 0 && this.txtPatientAge.Text.IndexOf("��") < 0 && this.txtPatientAge.Text.IndexOf("��") > 0)//����
                    {
                        this.txtPatientAge.Text = "D" + this.txtPatientAge.Text.Replace("��", "");
                    }
                    else if (this.txtPatientAge.Text.IndexOf("��") > 0 && this.txtPatientAge.Text.IndexOf("��") > 0 && this.txtPatientAge.Text.IndexOf("��") < 0)//N��N��
                    {
                        string[] PAge = this.txtPatientAge.Text.Split('��');
                        this.txtPatientAge.Text = "Y" + PAge[0] + "M" + PAge[1].Replace("��", "").Replace("��", "");
                    }
                    else if (this.txtPatientAge.Text.IndexOf("��") < 0 && this.txtPatientAge.Text.IndexOf("��") > 0 && this.txtPatientAge.Text.IndexOf("��") > 0)//N��N��
                    {
                        string[] PAge = this.txtPatientAge.Text.Split('��');
                        this.txtPatientAge.Text = "M" + PAge[0] + "D" + PAge[1].Replace("��", "").Replace("��", "");
                    }
                    else if (this.txtPatientAge.Text.IndexOf("��") > 0 && this.txtPatientAge.Text.IndexOf("��") > 0 && this.txtPatientAge.Text.IndexOf("��") > 0)//N��N��N��
                    {
                        string[] PAge = this.txtPatientAge.Text.Split('��');

                        string[] PAge1 = PAge[1].Split('��');
                        this.txtPatientAge.Text = "Y" + PAge[0] + "M" + PAge1[0] + "D" + PAge1[1].Replace("��", "").Replace("��", "");
                    }
                    else if (this.txtPatientAge.Text.IndexOf("��") < 0 && this.txtPatientAge.Text.IndexOf("��") < 0 && this.txtPatientAge.Text.IndexOf("��") > 0 && this.txtPatientAge.Text.IndexOf("��") > 0)//N��N��
                    {
                        string[] PAge = this.txtPatientAge.Text.Split('��');
                        this.txtPatientAge.Text = "W" + PAge[0] + "D" + PAge[1].Replace("��", "").Replace("��", "");
                    }
                }
                #endregion
                //���� ����
                cmbCountry.Tag = info.PatientInfo.Country.ID;
                //��������������
                if (info.BabyBirthWeight == "0")
                {
                    this.txtBabyBirthWeight.Text = "-";
                }
                else
                {
                    this.txtBabyBirthWeight.Text = info.BabyBirthWeight;
                }
                //��������Ժ����
                if (info.BabyInWeight == "0")
                {
                    this.txtBabyInWeight.Text = "-";
                }
                else
                {
                    this.txtBabyInWeight.Text = info.BabyInWeight;
                }
                //������
                if (info.PatientInfo.CaseState == "1")
                {
                    try
                    {
                        FS.FrameWork.Models.NeuObject area = this.con.GetConstant("AREA", info.PatientInfo.AreaCode);
                        if (area != null && area.Name != "")
                        {
                            this.cmbBirthAddr.Text = area.Name;
                        }
                    }
                    catch
                    {
                        this.cmbBirthAddr.Text = info.PatientInfo.AreaCode;
                    }
                }
                else
                {
                    this.cmbBirthAddr.Text = info.PatientInfo.AreaCode;
                }
                //����
                this.cmbDist.Text = info.PatientInfo.DIST;
                //���� 
                cmbNationality.Tag = info.PatientInfo.Nationality.ID;
                //���֤��
                txtIDNo.Text = info.PatientInfo.IDCard;
                //ְҵ
                cmbProfession.Tag = info.PatientInfo.Profession.ID;
                //����
                if (info.PatientInfo.MaritalStatus.ID != null)
                {
                    switch (info.PatientInfo.MaritalStatus.ID.ToString())
                    {
                        case "S"://δ��
                            this.cmbMaritalStatus.Tag = "1";//δ��
                            break;
                        case "M"://�ѻ�
                            this.cmbMaritalStatus.Tag = "2";//�ѻ�
                            break;
                        case "W"://ɥż
                            this.cmbMaritalStatus.Tag = "3";//ɥż
                            break;
                        case "A"://�־�
                            this.cmbMaritalStatus.Tag = "2";//�ѻ�
                            break;
                        case "D"://ʧ��
                            this.cmbMaritalStatus.Tag = "4";//���
                            break;
                        case "R"://�ٻ�
                            this.cmbMaritalStatus.Tag = "2";//�ѻ�
                            break;
                        default:
                            this.cmbMaritalStatus.Tag = info.PatientInfo.MaritalStatus.ID.ToString();
                            break;
                    }
                }
                if (info.PatientInfo.CaseState == "1")
                {
                    //��סַ 
                    cmbCurrentAdrr.Text = info.PatientInfo.AddressHome;
                    //��סַ�绰
                    txtCurrentPhone.Text = info.PatientInfo.PhoneHome;
                    //��סַ�ʱ�
                    txtCurrentZip.Text = info.PatientInfo.User02;
                }
                else
                {
                    //��סַ
                    cmbCurrentAdrr.Text = info.CurrentAddr;
                    //��סַ�绰
                    txtCurrentPhone.Text = info.CurrentPhone;
                    //��סַ�ʱ�
                    txtCurrentZip.Text = info.CurrentZip;
                }
                //���ڵ�ַ
                cmbHomeAdrr.Text = info.PatientInfo.AddressHome;
                //����סַ�ʱ�
                if (info.PatientInfo.CaseState == "1")
                {
                    txtHomeZip.Text = info.PatientInfo.User02;
                }
                else
                {
                    txtHomeZip.Text = info.PatientInfo.HomeZip;
                }
                //������λ����ַ
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
                //�뻼�߹�ϵ
                cmbRelation.Tag = info.PatientInfo.Kin.RelationLink;
                //��ϵ�˹�ϵ��ע
                txtRelationMemo.Text = info.PatientInfo.Kin.Memo;
                //��ϵ��ַ
                txtLinkmanAdd.Text = info.PatientInfo.Kin.RelationAddress;

                //��ϵ�绰
                txtLinkmanTel.Text = info.PatientInfo.Kin.RelationPhone;

                //��Ժ;��
                cmbInPath.Tag = info.InPath;
                //������Դ
                txtInAvenue.Text = info.InAvenue;
                //��Ժ����
                FS.HISFC.BizProcess.Integrate.Common.ControlParam ctrlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

                int DtInDept = ctrlParamIntegrate.GetControlParam<int>("CASE03", true, 0);
                if (DtInDept == 1)
                {
                    FS.HISFC.BizProcess.Integrate.RADT radtIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();
                    DateTime dtArrive = radtIntegrate.GetArriveDate(info.PatientInfo.ID);
                    if (dtArrive == System.DateTime.MinValue)
                    {
                        dtDateIn.Value = info.PatientInfo.PVisit.InTime;
                        //����ʱ��
                        //dtDateIn.Value = dtArrive;
                    }
                    else
                    {
                        dtDateIn.Value = dtArrive;
                    }
                }
                else
                {
                    FS.HISFC.BizProcess.Integrate.RADT radtIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();
                    DateTime dtArrive = radtIntegrate.GetArriveDate(info.PatientInfo.ID);
                    if (info.PatientInfo.PVisit.InTime != System.DateTime.MinValue)
                    {
                        //dtDateIn.Value = info.PatientInfo.PVisit.InTime;
                        //����ʱ��
                        dtDateIn.Value = dtArrive;
                    }
                    else
                    {
                        dtDateIn.Value = System.DateTime.Now;
                    }
                }
                #region  ��Ժ���ң���Ժ����
                if (info.PatientInfo.CaseState == "1")//��fin_ipr_inmaininfo �л�ȡ������
                {
                    FS.HISFC.Models.RADT.Location indept = baseDml.GetDeptIn(info.PatientInfo.ID);
                    //ȡ������� ren.jch
                    //FS.HISFC.Models.RADT.Location indept = baseDml.GetDeptIn1(info.PatientInfo.ID);
                    if (indept != null) //��Ժ���� 
                    {
                        CaseBase.InDept.ID = indept.Dept.ID;
                        CaseBase.InDept.Name = indept.Dept.Name;
                        //��Ժ���Ҵ���
                        cmbDeptInHospital.Tag = indept.Dept.ID;
                    }
                    else
                    {
                        //��Ժ���Ҵ���
                        cmbDeptInHospital.Tag = info.PatientInfo.PVisit.PatientLocation.Dept.ID;
                    }
                    //��Ժ����
                    CaseBase.OutDept.ID = info.PatientInfo.PVisit.PatientLocation.Dept.ID;
                    CaseBase.OutDept.Name = info.PatientInfo.PVisit.PatientLocation.Dept.Name;
                   
                    ////��Ժ���Ҵ���
                    //cmbDeptOutHospital.Tag = info.PatientInfo.PVisit.PatientLocation.Dept.ID;
                }
                else
                {
                    //��Ժ���Ҵ���
                    cmbDeptInHospital.Tag = info.InDept.ID;
                    ////��Ժ���Ҵ���
                    //cmbDeptOutHospital.Tag = info.OutDept.ID;
                }
                //��Ժ���Ҵ���
                cmbDeptOutHospital.Tag = this.dept_out;
                #endregion
                if (info.InRoom == null || info.InRoom == "")
                {
                    string changeBed = this.deptChange.QueryWardNoBedNOByInpatienNO(info.PatientInfo.ID, "1");
                    string WardNoBedNO = this.deptChange.QueryWardNoBedNOByInpatienNO(changeBed, "2");
                    //��Ժ����
                    this.txtInRoom.Text = WardNoBedNO;
                }
                else
                {
                    this.txtInRoom.Text = info.InRoom;
                    //this.txtInRoom.Text = this.bedNo.Substring(4);
                }
                #region ת�ƿƱ�

                //����ת����Ϣ���б�
                ArrayList changeDept = new ArrayList();
                //��ȡת����Ϣ
                changeDept = this.deptChange.QueryChangeDeptFromShiftApply(info.PatientInfo.ID, "2");
                if (changeDept != null && changeDept.Count > 0)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        if (i >= changeDept.Count)
                        {
                            break;
                        }
                        if (i == 0)
                        {
                            FS.HISFC.Models.RADT.Location locaInfo = changeDept[0] as FS.HISFC.Models.RADT.Location;
                            this.dtFirstTime.Value = FS.FrameWork.Function.NConvert.ToDateTime(locaInfo.Dept.Memo);
                            this.txtFirstDept.Tag = locaInfo.Dept.ID;
                        }
                        else if (i == 1)
                        {
                            FS.HISFC.Models.RADT.Location locaInfo = changeDept[1] as FS.HISFC.Models.RADT.Location;
                            this.dtSecondTime.Value = FS.FrameWork.Function.NConvert.ToDateTime(locaInfo.Dept.Memo);
                            this.txtDeptSecond.Tag = locaInfo.Dept.ID;
                        }
                        else if (i == 2)
                        {
                            FS.HISFC.Models.RADT.Location locaInfo = changeDept[2] as FS.HISFC.Models.RADT.Location;
                            this.dtThirdTime.Value = FS.FrameWork.Function.NConvert.ToDateTime(locaInfo.Dept.Memo);
                            this.txtDeptThird.Tag = locaInfo.Dept.ID;
                        }

                    }
                }
                else
                {
                    changeDept = new ArrayList();
                    //��ȡת����Ϣ
                    changeDept = this.deptChange.QueryChangeDeptFromShiftApply(info.PatientInfo.ID, "1");
                    if (changeDept != null)
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            if (i >= changeDept.Count)
                            {
                                break;
                            }
                            if (i == 0)
                            {
                                FS.HISFC.Models.RADT.Location locaInfo = changeDept[0] as FS.HISFC.Models.RADT.Location;
                                this.dtFirstTime.Value = FS.FrameWork.Function.NConvert.ToDateTime(locaInfo.Dept.Memo);
                                this.txtFirstDept.Tag = locaInfo.Dept.ID;
                            }
                            else if (i == 1)
                            {
                                FS.HISFC.Models.RADT.Location locaInfo = changeDept[1] as FS.HISFC.Models.RADT.Location;
                                this.dtSecondTime.Value = FS.FrameWork.Function.NConvert.ToDateTime(locaInfo.Dept.Memo);
                                this.txtDeptSecond.Tag = locaInfo.Dept.ID;
                            }
                            else if (i == 2)
                            {
                                FS.HISFC.Models.RADT.Location locaInfo = changeDept[2] as FS.HISFC.Models.RADT.Location;
                                this.dtThirdTime.Value = FS.FrameWork.Function.NConvert.ToDateTime(locaInfo.Dept.Memo);
                                this.txtDeptThird.Tag = locaInfo.Dept.ID;
                            }

                        }
                    }
                }
                #endregion
               
                //��Ժ����
                if (info.OutRoom == null || info.OutRoom == "")
                {
                    this.txtOutRoom.Text = deptChange.QueryWardNoBedNOByInpatienNO(this.bedNo, "2");
                }
                else
                {
                    this.txtOutRoom.Text = info.OutRoom;
                    //this.txtOutRoom.Text = this.bedNo.Substring(4);
                }
                //��Ժ����
                if (info.PatientInfo.PVisit.OutTime != System.DateTime.MinValue && this.in_State != "I")
                {
                    //�Ƿ�Ĭ�ϻ�ȡ��ʿ�Ǽǳ�Ժʱ�� 
                    ArrayList outTimelist = con.GetList("CASEOUTTIME");
                    //��Ժ�༭ʱ��ȡһ�γ�Ժ���ڱ�֤����  �������������п��ܰѳ�Ժʱ����ǰ�ģ���ҽԺȡ���2013-1-15
                    if (outTimelist != null && outTimelist.Count > 0)
                    {
                        this.txtDateOut.Value = this.dt_out;
                    }
                    else
                    {
                        //txtDateOut.Value = info.PatientInfo.PVisit.OutTime;
                        this.txtDateOut.Value = this.dt_out;
                    }
                }
                else
                {
                    txtDateOut.Value = System.DateTime.Now;
                }

                //ʵ��סԺ����

                if (this.in_State == "I")
                {
                    System.TimeSpan tt = this.baseDml.GetDateTimeFromSysDateTime().Date - this.dtDateIn.Value.Date;
                    if (tt.Days == 0)
                    {
                        this.txtPiDays.Text = "1";
                    }
                    else
                    {
                        this.txtPiDays.Text = Convert.ToString(tt.Days);
                    }
                }
                else
                {
                    System.TimeSpan ts = this.txtDateOut.Value.Date - this.dtDateIn.Value.Date;
                    if (ts.Days == 0)
                    {
                        this.txtPiDays.Text = "1";
                    }
                    else
                    {
                        this.txtPiDays.Text = Convert.ToString(ts.Days);
                    }
                }

                //�����������
                if (info.ClinicDiag.Name != null)
                {
                    this.cmbClinicDiagName.Text = Funtion.ReplaceSingleQuotationMarks(info.ClinicDiag.Name, false);
                }
                else
                {
                    this.cmbClinicDiagName.Text = "";
                }
                //������ϱ���
                this.txtClinicDiagCode.Text = info.ClinicDiag.ID;
                //�������ҽ�� ID
                txtClinicDocd.Tag = info.ClinicDoc.ID;
                //��������
                cmbExampleType.Tag = info.ExampleType;
                //�ٴ�·������
                cmbClinicPath.Tag = info.ClinicPath;
                //���ȴ���
                txtSalvTimes.Text = info.SalvTimes.ToString();
                //�ɹ�����
                txtSuccTimes.Text = info.SuccTimes.ToString();
                //�ڶ�ҳ����
                //�����ж�ԭ��
                if (info.InjuryOrPoisoningCauseCode != null && info.InjuryOrPoisoningCauseCode != "")
                {
                    txtInjuryOrPoisoningCauseCode.Text = info.InjuryOrPoisoningCauseCode;
                }

                if (info.InjuryOrPoisoningCause != null)
                {
                    txtInjuryOrPoisoningCause.Text = Funtion.ReplaceSingleQuotationMarks(info.InjuryOrPoisoningCause, false);
                }
                else
                {
                    txtInjuryOrPoisoningCause.Text = "";
                }

                //�������
                if (info.PathologicalDiagName != null)
                {
                    this.cmbPathologicalDiagName.Text = Funtion.ReplaceSingleQuotationMarks(info.PathologicalDiagName, false);
                }
                else
                {
                    this.cmbPathologicalDiagName.Text = "";
                }
                this.txtPathologicalDiagCode.Text = info.PathologicalDiagCode;

                this.txtPathologicalDiagNum.Text = info.PathNum;
                //ҩ�����
                cmbIsDrugAllergy.Tag = info.AnaphyFlag;
                //����ҩ��
                if (info.FirstAnaphyPharmacy.ID != null && info.FirstAnaphyPharmacy.ID.ToString() != "")
                {
                    this.txtDrugAllergy.Text = Funtion.ReplaceSingleQuotationMarks(info.FirstAnaphyPharmacy.ID, false);
                }
                else
                {
                    this.PharmacyAllergicLoadInfo(info.PatientInfo.ID, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC);
                }
                //��������ʬ��
                cmbDeathPatientBobyCheck.Tag = info.CadaverCheck;
                //Ѫ�ͱ���
                if (info.PatientInfo.BloodType.ID != null)
                {
                    cmbBloodType.Tag = info.PatientInfo.BloodType.ID.ToString();
                }
                //RhѪ��(����)
                cmbRhBlood.Tag = info.RhBlood;
                //�����δ���
                cmbDeptChiefDoc.Tag = info.PatientInfo.PVisit.ReferringDoctor.ID;
                //����ҽʦ����
                cmbConsultingDoctor.Tag = info.PatientInfo.PVisit.ConsultingDoctor.ID;
                //����ҽʦ����
                cmbAttendingDoctor.Tag = info.PatientInfo.PVisit.AttendingDoctor.ID;
                //סԺҽʦ����
                cmbAdmittingDoctor.Tag = info.PatientInfo.PVisit.AdmittingDoctor.ID;
                //���λ�ʿ
                if (info.DutyNurse.ID == "")
                {
                    cmbDutyNurse.Tag = this.dutyNurse;
                }
                else
                {
                    cmbDutyNurse.Tag = info.DutyNurse.ID;
                }
                //����ҽʦ����
                cmbRefresherDocd.Tag = info.RefresherDoc.ID;
                //ʵϰҽʦ����
                cmbPraDocCode.Tag = info.PatientInfo.PVisit.TempDoctor.ID;
                //����Ա
                txtCodingCode.Tag = info.CodingOper.ID;
                //��������
                cmbMrQual.Tag = info.MrQuality;
                //�ʿ�ҽʦ����
                cmbQcDocd.Tag = info.QcDoc.ID;
                //�ʿػ�ʿ����
                cmbQcNucd.Tag = info.QcNurse.ID;
                //���ʱ��
                if (info.CheckDate != System.DateTime.MinValue)
                {
                    txtCheckDate.Value = info.CheckDate;
                }
                else
                {
                    txtCheckDate.Value = System.DateTime.Now;
                }
                if (info.PatientInfo.CaseState != "I" && info.CheckDate.Date < info.PatientInfo.PVisit.OutTime.Date)
                {
                    //txtCheckDate.Value = info.PatientInfo.PVisit.OutTime.AddDays(3);//�ѳ�Ժ���ʼ�����С�ڳ�Ժ���� Ĭ�ϳ�Ժ����+3��
                    txtCheckDate.Value = info.PatientInfo.PVisit.OutTime;//ԭ���ǳ�Ժ���ڼ�3�죬�������ΪĬ���ǳ�Ժ���� by zhy
                }
                //��Ժ��ʽ
                txtLeaveHopitalType.Text = info.Out_Type;
                //ҽ��תԺ����ҽ�ƻ���
                txtHighReceiveHopital.Text = info.HighReceiveHopital;
                //ҽ��ת����
                txtLowerReceiveHopital.Text = info.LowerReceiveHopital;
                //��Ժ31������סԺ�ƻ�
                cmbComeBackInMonth.Tag = info.ComeBackInMonth;
                //��Ժ31����סԺĿ��
                cmbComeBackPurpose.Text = info.ComeBackPurpose;
                //­�����˻��߻���ʱ�� -��Ժǰ ��
                txtOutComeDay.Text = info.OutComeDay.ToString();
                //­�����˻��߻���ʱ�� -��Ժǰ Сʱ
                txtOutComeHour.Text = info.OutComeHour.ToString();
                //­�����˻��߻���ʱ�� -��Ժǰ ����
                txtOutComeMin.Text = info.OutComeMin.ToString();
                //­�����˻��߻���ʱ�� -��Ժ�� ��
                txtInComeDay.Text = info.InComeDay.ToString();
                //­�����˻��߻���ʱ�� -��Ժ�� Сʱ
                txtInComeHour.Text = info.InComeHour.ToString();
                //­�����˻��߻���ʱ�� -��Ժ�� ����
                txtInComeMin.Text = info.InComeMin.ToString();

                //�������� 2012-9-19
                if (info.Ever_Sickintodeath == null)
                {
                    info.Ever_Sickintodeath = string.Empty;
                }
                this.ucOperation1.IsHavedOps = info.Ever_Sickintodeath;
                //���޸�Ӥ��Ϣ
                if (info.Ever_Firstaid == null)
                {
                    info.Ever_Firstaid = string.Empty;
                }
                this.ucBabyCardInput1.IsHavedBaby = info.Ever_Firstaid;
                //������������Ϣ
                if (info.Ever_Difficulty == null)
                {
                    info.Ever_Difficulty = string.Empty;
                }
                this.ucTumourCard1.IsHavedTum = info.Ever_Difficulty;
                #region ���ݾɰ汾
                //��Ժ��Դ
                txtInAvenue.Tag = info.PatientInfo.PVisit.InSource.ID;
                //��Ժ״̬                  
                txtCircs.Tag = info.PatientInfo.PVisit.Circs.ID;
                //ȷ������
                if (info.DiagDate != System.DateTime.MinValue)
                {
                    txtDiagDate.Value = info.DiagDate;
                }
                else
                {
                    txtDiagDate.Value = info.PatientInfo.PVisit.InTime;
                }
                //��Ժ���
                txtRuyuanDiagNose.Text = Funtion.ReplaceSingleQuotationMarks(info.InHospitalDiag.Name, false);
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
                //�ٴ�_�������
                txtClPa.Tag = info.ClPa;
                //����_�������
                txtFsBl.Tag = info.FsBl;
                //�״β���
                this.cmbYnFirst.Tag = info.YnFirst;
                //ʾ�̿���
                this.cmbTechSerc.Tag = info.TechSerc;
                //�Ƿ�����
                this.cmbVisiStat.Tag = info.VisiStat;
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
                //������
                cmbDisease30.Tag = info.Disease30;
                //Ժ�ʻ������
                txtInconNum.Text = info.InconNum.ToString();
                //Զ�̻���
                txtOutconNum.Text = info.OutconNum.ToString();
                //��Ѫ��Ӧ�����ޣ�
                txtReactionBlood.Tag = info.ReactionBlood;
                //��Һ��Ӧ
                this.txtReactionTransfuse.Tag = info.ReactionLiquid;
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
                if (info.BodyAnotomize == "" || info.BodyAnotomize == null)
                {
                    txtBodyAnotomize.Text = "0";
                }
                else
                {
                    txtBodyAnotomize.Text = info.BodyAnotomize;
                }
                //ȫѪ��
                if (info.BloodWhole == "" || info.BodyAnotomize == null)
                {
                    txtBloodWhole.Text = "0";
                }
                else
                {
                    txtBloodWhole.Text = info.BloodWhole;
                }
                //������Ѫ��
                if (info.BloodOther == "" || info.BodyAnotomize == null)
                {
                    txtBloodOther.Text = "0";
                }
                else
                {
                    txtBloodOther.Text = info.BloodOther;
                }
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

                //��ȡ��������
                if (info.PatientInfo.CaseState == "1")
                {
                    this.GetNursingNum(info.PatientInfo.ID);
                }
                else
                {
                    if (this.in_State == "I" || (info.PatientInfo.CaseState == "2" && info.PatientInfo.PVisit.OutTime.Date < this.dt_out.Date))
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
                #endregion
                return 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
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

            //סԺ��ˮ��
            info.PatientInfo.ID = CaseBase.PatientInfo.ID;
            info.IsHandCraft = CaseBase.IsHandCraft;

            //ҽ�Ƹ��ѷ�ʽ
            if (cmbPactKind.Tag != null)
            {
                info.PatientInfo.Pact.ID = cmbPactKind.Tag.ToString();
            }
            else
            {
                info.PatientInfo.Pact.ID = "";
            }
            info.PatientInfo.Pact.PayKind.ID = "DRGS";
            //ҽ�����Ѻ�
            info.PatientInfo.SSN = txtHealthyCard.Text;
            //סԺ����
            info.PatientInfo.InTimes = FS.FrameWork.Function.NConvert.ToInt32(txtInTimes.Text);
            //������
            info.CaseNO = txtCaseNum.Text;
            info.CaseNO = info.CaseNO.PadLeft(10, '0');
            //סԺ��
            if (patient_no != "")
            {
                info.PatientInfo.PID.PatientNO = this.patient_no;//CaseBase.PatientInfo.PID.PatientNO;
            }
            else
            {
                info.PatientInfo.PID.PatientNO = CaseBase.PatientInfo.PID.PatientNO;
            }

            //����
            info.PatientInfo.Name = txtPatientName.Text;

            //�Ա�
            if (cmbPatientSex.Tag != null)
            {
                info.PatientInfo.Sex.ID = cmbPatientSex.Tag;
            }
            else
            {
                info.PatientInfo.Sex.ID = CaseBase.PatientInfo.Sex.ID;
            }
            if (info.PatientInfo.Sex.ID == null)
            {
                info.PatientInfo.Sex.ID = "";
            }

            //��������
            info.PatientInfo.Birthday = dtPatientBirthday.Value;
            //���䵥λ����������͵�λ
            info.AgeUnit = this.txtPatientAge.Text;
            info.PatientInfo.Age = "0";
            info.BabyAge = this.txtBabyAge.Text;

            //����
            if (cmbCountry.Tag != null)
            {
                info.PatientInfo.Country.ID = cmbCountry.Tag.ToString();
            }
            else
            {
                info.PatientInfo.Country.ID = "";
            }
            //��������������
            info.BabyBirthWeight = this.txtBabyBirthWeight.Text;
            //��������Ժ����
            info.BabyInWeight = this.txtBabyInWeight.Text;
            //������
            info.PatientInfo.AreaCode = this.cmbBirthAddr.Text;
            //����
            info.PatientInfo.DIST = this.cmbDist.Text;

            //���� 
            if (cmbNationality.Tag != null)
            {
                info.PatientInfo.Nationality.ID = cmbNationality.Tag.ToString();
            }
            else
            {
                info.PatientInfo.Nationality.ID = "";
            }
            //���֤��
            info.PatientInfo.IDCard = txtIDNo.Text;
            //ְҵ
            if (cmbProfession.Tag != null)
            {
                info.PatientInfo.Profession.ID = cmbProfession.Tag.ToString();
            }
            else
            {
                info.PatientInfo.Profession.ID = "";
            }
            //����
            if (cmbMaritalStatus.Tag != null)
            {
                info.PatientInfo.MaritalStatus.ID = cmbMaritalStatus.Tag;
            }
            else
            {
                info.PatientInfo.MaritalStatus.ID = "";
            }
            // ��סַ
            info.CurrentAddr = cmbCurrentAdrr.Text;
            //��סַ�绰
            info.CurrentPhone = txtCurrentPhone.Text;
            //��סַ�ʱ�
            info.CurrentZip = txtCurrentZip.Text;
            //����סַ
            info.PatientInfo.AddressHome = cmbHomeAdrr.Text;
            //����סַ�ʱ�
            info.PatientInfo.HomeZip = txtHomeZip.Text;
            //������λ����ַ
            info.PatientInfo.AddressBusiness = txtAddressBusiness.Text;
            //��λ�绰
            info.PatientInfo.PhoneBusiness = txtPhoneBusiness.Text;
            //��λ�ʱ�
            info.PatientInfo.BusinessZip = txtBusinessZip.Text;
            //��ϵ������
            info.PatientInfo.Kin.Name = txtKin.Text;
            //�뻼�߹�ϵ
            if (cmbRelation.Tag != null)
            {
                info.PatientInfo.Kin.RelationLink = cmbRelation.Tag.ToString();
            }
            else
            {
                info.PatientInfo.Kin.RelationLink = "";
            }
            //��ϵ�˹�ϵ��ע
            info.PatientInfo.Kin.Memo = txtRelationMemo.Text;
            //��ϵ��ַ
            info.PatientInfo.Kin.RelationAddress = txtLinkmanAdd.Text;
            //��ϵ�绰
            info.PatientInfo.Kin.RelationPhone = txtLinkmanTel.Text;
            //��Ժ;��
            if (cmbInPath.Tag != null)
            {
                info.InPath = cmbInPath.Tag.ToString();
            }
            else
            {
                info.InPath = "";
            }
            //��Ժ����
            info.PatientInfo.PVisit.InTime = dtDateIn.Value;
            //��Ժ���Ҵ���
            if (cmbDeptInHospital.Tag != null)
            {
                info.InDept.ID = cmbDeptInHospital.Tag.ToString();
            }
            else
            {
                info.InDept.ID = "";
            }
            //��Ժ��������
            info.InDept.Name = cmbDeptInHospital.Text;
            //��Ժ����
            info.InRoom = this.txtInRoom.Text;
            //ת�ƿƱ�
            changeDeptList = new ArrayList();
            FS.HISFC.Models.RADT.Location locationInfo = new FS.HISFC.Models.RADT.Location();
            if (this.txtFirstDept.Text != "")
            {
                locationInfo.User02 = info.PatientInfo.ID;
                locationInfo.User03 = "1";
                locationInfo.Dept.Memo = this.dtFirstTime.Value.ToString();
                if (this.txtFirstDept.Tag != null)
                {
                    locationInfo.Dept.ID = this.txtFirstDept.Tag.ToString();
                    locationInfo.Dept.Name = this.txtFirstDept.Text;
                }
                this.changeDeptList.Add(locationInfo);

                if (this.txtDeptSecond.Text != "")
                {
                    locationInfo = new FS.HISFC.Models.RADT.Location();
                    locationInfo.User02 = info.PatientInfo.ID;
                    locationInfo.User03 = "2";
                    locationInfo.Dept.Memo = this.dtSecondTime.Value.ToString();
                    if (this.txtDeptSecond.Tag != null)
                    {
                        locationInfo.Dept.ID = this.txtDeptSecond.Tag.ToString();
                        locationInfo.Dept.Name = this.txtDeptSecond.Text;
                    }
                    this.changeDeptList.Add(locationInfo);
                    if (this.txtDeptThird.Text != "")
                    {
                        locationInfo = new FS.HISFC.Models.RADT.Location();
                        locationInfo.User02 = info.PatientInfo.ID;
                        locationInfo.User03 = "3";
                        locationInfo.Dept.Memo = this.dtThirdTime.Value.ToString();
                        if (this.txtDeptThird.Tag != null)
                        {
                            locationInfo.Dept.ID = this.txtDeptThird.Tag.ToString();
                            locationInfo.Dept.Name = this.txtDeptThird.Text;
                        }
                        this.changeDeptList.Add(locationInfo);
                    }
                }
            }

            //��Ժ����
            info.PatientInfo.PVisit.OutTime = txtDateOut.Value;
            //��Ժ���Ҵ���
            if (cmbDeptOutHospital.Tag != null)
            {
                info.OutDept.ID = cmbDeptOutHospital.Tag.ToString();
            }
            else
            {
                info.OutDept.ID = "";
            }
            //��Ժ��������
            info.OutDept.Name = cmbDeptOutHospital.Text;
            //��Ժ����
            info.OutRoom = this.txtOutRoom.Text;
            //ʵ��סԺ����
            info.InHospitalDays = FS.FrameWork.Function.NConvert.ToInt32(txtPiDays.Text);
            //�������
            info.ClinicDiag.Name = Funtion.ReplaceSingleQuotationMarks(this.cmbClinicDiagName.Text, true);
            //������ϱ���
            info.ClinicDiag.ID = this.txtClinicDiagCode.Text;
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

            //��������
            if (cmbExampleType.Tag != null)
            {
                info.ExampleType = cmbExampleType.Tag.ToString();
            }
            else
            {
                info.ExampleType = "";
            }
            //�ٴ�·������
            if (cmbClinicPath.Tag != null)
            {
                info.ClinicPath = cmbClinicPath.Tag.ToString();
            }
            else
            {
                info.ClinicPath = "2";
            }
            //���ȴ���
            info.SalvTimes = FS.FrameWork.Function.NConvert.ToInt32(txtSalvTimes.Text.Trim());
            //�ɹ�����
            info.SuccTimes = FS.FrameWork.Function.NConvert.ToInt32(txtSuccTimes.Text.Trim());

            //�����ж�ԭ��

            info.InjuryOrPoisoningCauseCode = this.txtInjuryOrPoisoningCauseCode.Text;
            info.InjuryOrPoisoningCause = Funtion.ReplaceSingleQuotationMarks(this.txtInjuryOrPoisoningCause.Text, true);


            //�������

            info.PathologicalDiagName = Funtion.ReplaceSingleQuotationMarks(this.cmbPathologicalDiagName.Text, true);

            info.PathologicalDiagCode = this.txtPathologicalDiagCode.Text;

            info.PathNum = this.txtPathologicalDiagNum.Text;
            //ҩ�����
            if (this.cmbIsDrugAllergy.Tag != null)
            {
                info.AnaphyFlag = this.cmbIsDrugAllergy.Tag.ToString();
            }
            else
            {
                info.AnaphyFlag = "";
            }
            //����ҩ��
            info.FirstAnaphyPharmacy.ID = Funtion.ReplaceSingleQuotationMarks(this.txtDrugAllergy.Text, true);

            //��������ʬ��
            if (cmbDeathPatientBobyCheck.Tag != null)
            {
                info.CadaverCheck = cmbDeathPatientBobyCheck.Tag.ToString();
            }
            else
            {
                info.CadaverCheck = "";
            }


            //Ѫ�ͱ���
            info.PatientInfo.BloodType.ID = cmbBloodType.Tag;
            //RhѪ��(����)
            if (cmbRhBlood.Tag != null)
            {
                info.RhBlood = cmbRhBlood.Tag.ToString();
            }
            else
            {
                info.RhBlood = "";
            }
            //�����δ���
            if (cmbDeptChiefDoc.Tag != null)
            {
                info.PatientInfo.PVisit.ReferringDoctor.ID = cmbDeptChiefDoc.Tag.ToString();
                info.PatientInfo.PVisit.ReferringDoctor.Name = cmbDeptChiefDoc.Text;
            }
            else
            {
                info.PatientInfo.PVisit.ReferringDoctor.ID = "";
                info.PatientInfo.PVisit.ReferringDoctor.Name = "";
            }
            //����ҽʦ����
            if (cmbConsultingDoctor.Tag != null)
            {
                info.PatientInfo.PVisit.ConsultingDoctor.ID = cmbConsultingDoctor.Tag.ToString();
                info.PatientInfo.PVisit.ConsultingDoctor.Name = cmbConsultingDoctor.Text;
            }
            else
            {
                info.PatientInfo.PVisit.ConsultingDoctor.ID = "";
                info.PatientInfo.PVisit.ConsultingDoctor.Name = "";
            }
            //����ҽʦ����
            if (cmbAttendingDoctor.Tag != null)
            {
                info.PatientInfo.PVisit.AttendingDoctor.ID = cmbAttendingDoctor.Tag.ToString();
                info.PatientInfo.PVisit.AttendingDoctor.Name = cmbAttendingDoctor.Text;
            }
            else
            {
                info.PatientInfo.PVisit.AttendingDoctor.ID = "";
                info.PatientInfo.PVisit.AttendingDoctor.Name = "";
            }
            //סԺҽʦ����
            if (cmbAdmittingDoctor.Tag != null)
            {
                info.PatientInfo.PVisit.AdmittingDoctor.ID = cmbAdmittingDoctor.Tag.ToString();
                //סԺҽʦ����
                info.PatientInfo.PVisit.AdmittingDoctor.Name = cmbAdmittingDoctor.Text;
            }
            else
            {
                info.PatientInfo.PVisit.AdmittingDoctor.ID = "";
                //סԺҽʦ����
                info.PatientInfo.PVisit.AdmittingDoctor.Name = "";
            }
            //���λ�ʿ
            if (cmbDutyNurse.Tag != null)
            {
                info.DutyNurse.ID = cmbDutyNurse.Tag.ToString();
                info.DutyNurse.Name = cmbDutyNurse.Text.Trim();
            }
            else
            {
                info.DutyNurse.ID = "";
                info.DutyNurse.Name = "";
            }
            //����ҽʦ����
            if (cmbRefresherDocd.Tag != null)
            {
                info.RefresherDoc.ID = cmbRefresherDocd.Tag.ToString();
                info.RefresherDoc.Name = cmbRefresherDocd.Text;
            }
            else
            {
                info.RefresherDoc.ID = "";
                info.RefresherDoc.Name = "";
            }
            //ʵϰҽʦ����
            if (cmbPraDocCode.Tag != null)
            {
                info.PatientInfo.PVisit.TempDoctor.ID = cmbPraDocCode.Tag.ToString();
                info.PatientInfo.PVisit.TempDoctor.Name = cmbPraDocCode.Text.Trim();
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
            if (cmbMrQual.Tag != null)
            {
                info.MrQuality = cmbMrQual.Tag.ToString();
            }
            else
            {
                info.MrQuality = "";
            }
            //�ʿ�ҽʦ����
            if (cmbQcDocd.Tag != null)
            {
                info.QcDoc.ID = cmbQcDocd.Tag.ToString();
                info.QcDoc.Name = cmbQcDocd.Text.Trim();
            }
            else
            {
                info.QcDoc.ID = "";
                info.QcDoc.Name = "";
            }
            //�ʿػ�ʿ����
            if (cmbQcNucd.Tag != null)
            {
                info.QcNurse.ID = cmbQcNucd.Tag.ToString();
                info.QcNurse.Name = cmbQcNucd.Text.Trim();
            }
            else
            {
                info.QcNurse.ID = "";
                info.QcNurse.Name = "";
            }
            //���ʱ��
            info.CheckDate = txtCheckDate.Value;

            //��Ժ��ʽ
            info.Out_Type = txtLeaveHopitalType.Text;
            //ҽ��תԺ����ҽ�ƻ���
            info.HighReceiveHopital = txtHighReceiveHopital.Text;
            //ҽ��ת����
            info.LowerReceiveHopital = txtLowerReceiveHopital.Text;

            //��Ժ31������סԺ�ƻ�
            if (cmbComeBackInMonth.Tag != null)
            {
                info.ComeBackInMonth = cmbComeBackInMonth.Tag.ToString();
            }
            else
            {
                info.ComeBackInMonth = "1";//1�� 2��
            }
            //��Ժ31����סԺĿ��

            info.ComeBackPurpose = cmbComeBackPurpose.Text;

            //­�����˻��߻���ʱ�� -��Ժǰ ��
            info.OutComeDay = FS.FrameWork.Function.NConvert.ToInt32(txtOutComeDay.Text);

            //­�����˻��߻���ʱ�� -��Ժǰ Сʱ
            info.OutComeHour = FS.FrameWork.Function.NConvert.ToInt32(txtOutComeHour.Text);

            //­�����˻��߻���ʱ�� -��Ժǰ ����
            info.OutComeMin = FS.FrameWork.Function.NConvert.ToInt32(txtOutComeMin.Text);

            //­�����˻��߻���ʱ�� -��Ժ�� ��
            info.InComeDay = FS.FrameWork.Function.NConvert.ToInt32(txtInComeDay.Text);
            //­�����˻��߻���ʱ�� -��Ժ�� Сʱ
            info.InComeHour = FS.FrameWork.Function.NConvert.ToInt32(txtInComeHour.Text);

            //­�����˻��߻���ʱ�� -��Ժ�� ����
            info.InComeMin = FS.FrameWork.Function.NConvert.ToInt32(txtInComeMin.Text);

            info.LendStat = "1"; //��������״̬ 0 Ϊ��� 1Ϊ�ڼ� 
            if (this.frmType == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC && (this.CaseBase.PatientInfo.CaseState == "1" || string.IsNullOrEmpty(this.CaseBase.PatientInfo.CaseState)))
            {
                info.PatientInfo.CaseState = "2";
            }
            else if (this.frmType == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.CAS) //������������
            {
                info.PatientInfo.CaseState = "3";
            }
            else
            {
                info.PatientInfo.CaseState = this.CaseBase.PatientInfo.CaseState;
            }
            //�Ƿ��в���֢
            info.SyndromeFlag = this.ucDiagNoseInput1.GetSyndromeFlag();
            if (this.CaseBase.LendStat == null || this.CaseBase.LendStat == "") //��������״̬ 
            {
                info.LendStat = "I";
            }
            else
            {
                info.LendStat = this.CaseBase.LendStat;
            }
            info.UploadStatu = "0";
            info.IsDrgs = "1";

            //�������� 2012-9-19
            info.Ever_Sickintodeath = this.ucOperation1.IsHavedOps;
            //���޸�Ӥ����Ϣ 2012-9-19
            info.Ever_Firstaid = this.ucBabyCardInput1.IsHavedBaby;
            //������������Ϣ 2012-9-19
            info.Ever_Difficulty = this.ucTumourCard1.IsHavedTum;
            #region ���ݾɰ汾����
            //��ͥ�绰
            info.PatientInfo.PhoneHome = this.CaseBase.PatientInfo.PhoneHome;
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
            //��Ժ���
            info.InHospitalDiag.Name = Funtion.ReplaceSingleQuotationMarks(this.txtRuyuanDiagNose.Text, true);
            //�о���ʵϰҽʦ����
            info.GraduateDoc.ID = this.CaseBase.GraduateDoc.ID;
            info.GraduateDoc.Name = this.CaseBase.GraduateDoc.Name;
            //ת��ҽԺ
            info.ComeFrom = this.CaseBase.ComeFrom;
            //ת����ҽԺ
            info.OutDept.Memo = this.CaseBase.OutDept.Memo;
            //��Ⱦ��λ
            info.InfectionPosition.Name = this.CaseBase.InfectionPosition.Name;
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


            //ʾ�̿���
            if (this.cmbTechSerc.Tag != null)
            {
                info.TechSerc = this.cmbTechSerc.Tag.ToString();
            }
            else
            {
                info.TechSerc = "2";
            }
            //�Ƿ�����
            if (this.cmbVisiStat.Tag != null)
            {
                info.VisiStat = this.cmbVisiStat.Tag.ToString();
            }
            else
            {
                info.VisiStat = "2";
            }
            //������� ��
            info.VisiPeriodWeek = this.CaseBase.VisiPeriodWeek;
            //������� ��
            info.VisiPeriodMonth = this.CaseBase.VisiPeriodMonth;
            //������� ��
            info.VisiPeriodYear = this.CaseBase.VisiPeriodYear;

            //�����������Ƽ�����Ϊ��Ժ��һ����Ŀ
            if (this.cmbYnFirst.Tag != null)
            {
                info.YnFirst = this.cmbYnFirst.Tag.ToString();
            }
            else
            {
                info.YnFirst = "2";
            }
            //������ 
            if (this.cmbDisease30.Tag != null)
            {
                info.Disease30 = this.cmbDisease30.Tag.ToString();
            }
            else
            {
                info.Disease30 = "2";
            }
            //Ժ�ʻ������
            info.InconNum = FS.FrameWork.Function.NConvert.ToInt32(txtInconNum.Text.Trim());
            //Զ�̻���
            info.OutconNum = FS.FrameWork.Function.NConvert.ToInt32(txtOutconNum.Text.Trim());
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
            //��Һ��Ӧ
            if (this.txtReactionTransfuse.Tag != null)
            {
                info.ReactionLiquid = this.txtReactionTransfuse.Tag.ToString();
            }
            else
            {
                info.ReactionLiquid = "";
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
            //��ϸ����
            info.BloodRed = txtBloodRed.Text;
            //ѪС����
            info.BloodPlatelet = txtBloodPlatelet.Text;
            //Ѫ����
            info.BodyAnotomize = txtBodyAnotomize.Text;
            //ȫѪ��
            info.BloodWhole = txtBloodWhole.Text;
            //������Ѫ��
            info.BloodOther = txtBloodOther.Text;
            //X���
            info.XNum = this.CaseBase.XNum;
            //CT��
            info.CtNum = this.CaseBase.CtNum;
            //MRI��
            info.MriNum = this.CaseBase.MriNum;
            //B����
            info.DsaNum = this.CaseBase.DsaNum;
            //PET ��
            info.PetNum = this.CaseBase.PetNum;
            //ECT��
            info.EctNum = this.CaseBase.EctNum;
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

            info.OperInfo.ID = this.CaseBase.OperInfo.ID;

            //����Ա 
            info.PackupMan.ID = this.CaseBase.PackupMan.ID;

            info.OperationCoding.ID = this.CaseBase.OperationCoding.ID;

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
                //ʹ�����µ��Ӳ���2012-9-11
                FS.FrameWork.Models.NeuObject NewEMRInfo = con.GetConstant("CASENEWEMR", "1");
                if (NewEMRInfo != null && NewEMRInfo.Memo == "1")
                {
                    this.isNewEMR = true;
                    this.InitCountryList();
                }

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
                this.dutyNurse = patientInfo.PVisit.AdmittingNurse.ID;
                this.patient_no = patientInfo.PID.PatientNO;
                patientInfo = new FS.HISFC.Models.RADT.PatientInfo();
                //end add chengym
                CaseBase = baseDml.GetCaseBaseInfo(InpatientNo);//����������Ϣ��met_cas_base��

                if (CaseBase == null)
                {
                    CaseBase = new FS.HISFC.Models.HealthRecord.Base();
                    CaseBase.PatientInfo.ID = InpatientNo;
                    CaseBase.PatientInfo.PID.PatientNO = this.PatientNo;
                    CaseBase.PatientInfo.PID.CardNO = this.CardNo;
                }
                else
                {
                    ArrayList DrgsTimesList = this.con.GetList("CASEDRGSUSEDTIME");
                    if (DrgsTimesList != null && DrgsTimesList.Count > 0)
                    {
                        DateTime dt = this.con.GetDateTimeFromSysDateTime().Date.AddDays(10);
                        FS.FrameWork.Models.NeuObject usedTime = DrgsTimesList[0] as FS.FrameWork.Models.NeuObject;
                        try
                        {
                            dt = FS.FrameWork.Function.NConvert.ToDateTime(usedTime.Memo);
                        }
                        catch
                        {
                        }
                        if (dt.Date != this.con.GetDateTimeFromSysDateTime().Date.AddDays(10)
                            && dt.Date > this.dt_out.Date && CaseBase.PatientInfo.Pact.PayKind.ID != "DRGS")
                        {
                            MessageBox.Show("�û�����д���Ǿɲ�����ҳ����ʹ�þɲ�����ҳ�ڵ㣨��һ���ڵ㣩�鿴��", "��ʾ");
                            return -1;
                        }
                    }
                }
                //1. �����������û����Ϣ ��ȥסԺ����ȥ��ѯ
                //2. ��� סԺ�������м�¼����ȡ��Ϣ д��������. 
                if (CaseBase.PatientInfo.ID == "" || CaseBase.OperInfo.OperTime == DateTime.MinValue)//����������û�м�¼
                {
                    #region �����У�met_cas_base��û�м�¼
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
                this.frmType = Type;

                //������ֹ�¼�벡�� ���ܲ�ѯ��������Ϣ��Ϊ�� ֻ�д����InpatientNo ��Ϊ��
                if (CaseBase.PatientInfo.CaseState == "0")
                {
                    MessageBox.Show("�ò��˲������в���");
                    return 0;
                }
                //���没����״̬
                CaseFlag = FS.FrameWork.Function.NConvert.ToInt32(CaseBase.PatientInfo.CaseState);

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

                        //CaseBase = baseDml.GetCaseBaseInfo(CaseBase.PatientInfo.ID);
                        //CaseBase.PatientInfo.CaseState = CaseFlag.ToString();
                        //if (CaseBase == null)
                        //{
                        //    MessageBox.Show("��ѯ����ʧ��" + baseDml.Err);
                        //    return -1;
                        //}

                        //������� 
                        ConvertInfoToPanel(CaseBase);
                        //ҽ���ύ�󣬲�����༭
                        if (this.baseDml.GetEmrQcCommit(CaseBase.PatientInfo.ID) > 0)
                        {
                            SetReadOnly(true);
                        }
                        else
                        {
                            SetReadOnly(false);
                        }
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
                        SetReadOnly(false);
                    }
                    else if (CaseBase.PatientInfo.CaseState == "2" || CaseBase.PatientInfo.CaseState == "3")
                    {
                        //�Ӳ����������л�ȡ��Ϣ ����ʾ�ڽ����� 
                        //CaseBase = baseDml.GetCaseBaseInfo(CaseBase.PatientInfo.ID);
                        //CaseBase.PatientInfo.CaseState = CaseFlag.ToString();
                        //if (CaseBase == null)
                        //{
                        //    MessageBox.Show("��ѯ����ʧ��" + baseDml.Err);
                        //    return -1;
                        //}
                        //������� 
                        ConvertInfoToPanel(CaseBase);
                        SetReadOnly(true);
                    }
                    else if ((CaseBase.PatientInfo.CaseState == "" || CaseBase.PatientInfo.CaseState == null) && CaseBase.IsHandCraft == "1")
                    {
                        //�������
                        ConvertInfoToPanel(CaseBase);
                        SetReadOnly(false);
                    }
                    else
                    {
                        //�����Ѿ���� �������޸ġ�
                        ConvertInfoToPanel(CaseBase);
                        this.SetReadOnly(true); //��Ϊֻ�� 
                    }
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
                this.ucTumourCard1.LoadInfo(CaseBase.PatientInfo, frmType);
                #endregion
                #region ת��
                //this.ucChangeDept1.LoadInfo(CaseBase.PatientInfo, changeDept);
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
                this.ucFeeInfo1.LoadInfoDrgs(CaseBase.PatientInfo);
                #endregion

                //��ʾ������Ϣ
                this.tab1.SelectedIndex = 0;
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

        private void pactKind_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                txtHealthyCard.Focus();
            }
        }
        private void txtHealthyCard_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtInTimes.Focus();
            }
        }
        private void txtInTimes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtCaseNum.Focus();
            }
        }

        private void caseNum_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtPatientName.Focus();
            }
            else if (e.KeyData == Keys.Divide)
            {
                return;
            }
        }
        private void PatientName_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.cmbPatientSex.Focus();
            }
        }
        //�Ա�
        private void PatientSex_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                dtPatientBirthday.Focus();
            }
        }
        private void patientBirthday_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.cmbCountry.Focus();
            }
        }

        private void PatientAge_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.cmbCountry.Focus();
            }
        }

        #region ����
        private void Country_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.cmbNationality.Focus();
            }
        }
        #endregion
        private void txtBabyAge_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtBabyBirthWeight.Focus();
            }
        }
        private void txtBabyBirthWeight_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtBabyInWeight.Focus();
            }
        }
        private void txtBabyInWeight_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.cmbBirthAddr.Focus();
            }
        }

        private void cmbBirthAddr_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.cmbDist.Focus();
            }
        }
        private void cmbDist_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtIDNo.Focus();
            }
        }
        #region  ����
        private void Nationality_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtBabyBirthWeight.Focus();
            }
        }
        #endregion
        private void IDNo_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.cmbProfession.Focus();
            }
        }
        #region ְҵ
        private void Profession_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.cmbMaritalStatus.Focus();
            }
        }
        #endregion
        #region ����
        private void MaritalStatus_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.cmbCurrentAdrr.Focus();
            }
        }
        #endregion
        private void cmbCurrentAdrr_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtCurrentPhone.Focus();
            }
        }
        private void txtCurrentPhone_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtCurrentZip.Focus();
            }
        }
        private void txtCurrentZip_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.cmbHomeAdrr.Focus();
            }

        }
        private void cmbHomeAdrr_KeyDown(object sender, KeyEventArgs e)
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
                this.txtAddressBusiness.Focus();
            }
        }
        private void AddressBusiness_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
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
                this.txtBusinessZip.Focus();
            }
        }
        private void BusinessZip_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
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
                this.cmbRelation.Focus();
            }
        }
        #region ��ϵ�˹�ϵ
        private void Relation_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtLinkmanAdd.Focus();
            }
        }
        #endregion
        private void LinkmanAdd_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtLinkmanTel.Focus();

            }
        }

        private void LinkmanTel_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.cmbInPath.Focus();
            }
        }
        #region ������Դ
        private void InAvenue_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtInAvenue.Focus();
            }
        }
        private void txtInAvenue_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.dtDateIn.Focus();
            }
        }
        #endregion
        private void Date_In_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.cmbDeptInHospital.Focus();
            }
        }
        private void txtInRoom_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.dtFirstTime.Focus();
            }
        }
        private void dtFirstTime_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtFirstDept.Focus();
            }
        }
        private void txtFirstDept_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.dtSecondTime.Focus();
            }
        }
        private void dtSecondTime_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtDeptSecond.Focus();
            }
        }
        private void txtDeptSecond_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.dtThirdTime.Focus();
            }
        }
        private void dtThirdTime_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtDeptThird.Focus();
            }
        }
        private void txtDeptThird_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtDateOut.Focus();
            }
        }
        private void txtOutRoom_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtPiDays.Focus();
            }
        }
        #region  ��Ժ����
        private void DeptInHospital_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtInRoom.Focus();
            }
        }
        #endregion
        private void txtChangeDept_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtDateOut.Focus();
            }
        }
        private void Date_Out_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                System.TimeSpan tt = this.txtDateOut.Value.Date - this.dtDateIn.Value.Date;
                this.txtPiDays.Text = Convert.ToString(tt.Days);
                this.cmbDeptOutHospital.Focus();
            }
        }
        private void cmbDeptOutHospital_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtOutRoom.Focus();
            }
        }
        private void txtPiDays_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.cmbClinicDiagName.Focus();
            }
        }
        private void cmbClinicDiagName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtClinicDiagCode.Focus();
            }
        }
        private void txtClinicDiagCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtClinicDocd.Focus();
            }
        }
        private void txtClinicDocd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.cmbExampleType.Focus();
            }
        }
        private void cmbExampleType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.cmbClinicPath.Focus();
            }
        }
        private void cmbClinicPath_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtSalvTimes.Focus();
            }
        }
        private void txtSalvTimes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtSuccTimes.Focus();
            }
        }
        private void txtSuccTimes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtInjuryOrPoisoningCause.Focus();
            }
        }
        private void txtInjuryOrPoisoningCause_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.cmbPathologicalDiagName.Focus();
            }
        }

        private void txtInjuryOrPoisoningCauseCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.cmbPathologicalDiagName.Focus();
            }
        }
        private void cmbPathologicalDiagName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtPathologicalDiagCode.Focus();
            }
        }

        private void txtPathologicalDiagCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtPathologicalDiagNum.Focus();
            }
        }
        private void txtPathologicalDiagNum_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.cmbIsDrugAllergy.Focus();
            }
        }
        private void cmbIsDrugAllergy_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (cmbIsDrugAllergy.Tag != null && cmbIsDrugAllergy.Tag.ToString() == "2")
                {
                    this.txtDrugAllergy.Focus();
                }
                else
                {
                    this.cmbDeathPatientBobyCheck.Focus();
                }
            }
        }

        private void txtDrugAllergy_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.cmbDeathPatientBobyCheck.Focus();
            }
        }

        private void cmbDeathPatientBobyCheck_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.cmbBloodType.Focus();
            }
        }

        #region  Ѫ��
        private void BloodType_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.cmbRhBlood.Focus();
            }
        }
        #endregion
        #region  RH��Ӧ
        private void RhBlood_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtCePi.Focus();
            }
        }
        #endregion
        #region  �������Ժ
        private void txtCePi_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtClPa.Focus();
            }
        }
        #endregion
        #region  ��Ժ�벡��
        private void txtClPa_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.cmbDeptChiefDoc.Focus();
            }
        }
        #endregion
        private void cmbDeptChiefDoc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.cmbConsultingDoctor.Focus();
            }
        }
        #region ����ҽʦ
        private void ConsultingDoctor_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                cmbAttendingDoctor.Focus();
            }
        }
        #endregion
        #region  ����ҽʦ
        private void AttendingDoctor_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                cmbAdmittingDoctor.Focus();
            }
        }
        #endregion
        #region  סԺҽ��
        private void AdmittingDoctor_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.cmbDutyNurse.Focus();
            }
        }
        #endregion
        #region ���λ�ʿ
        private void cmbDutyNurse_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                cmbRefresherDocd.Focus();
            }
        }
        #endregion
        #region ����ҽʦ
        private void RefresherDocd_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.cmbPraDocCode.Focus();
            }
        }
        #endregion

        private void cmbPraDocCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtCodingCode.Focus();
            }
        }
        #region ����Ա
        private void CodingCode_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.cmbMrQual.Focus();
            }
        }
        #endregion
        #region ��������
        private void MrQual_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                cmbQcDocd.Focus();
            }
        }
        #endregion
        #region �ʿ�ҽ��
        private void QcDocd_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                cmbQcNucd.Focus();
            }
        }
        #endregion
        private void cmbQcNucd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtCheckDate.Focus();
            }
        }
        private void CheckDate_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtLeaveHopitalType.Focus();
            }
        }
        private void txtLeaveHopitalType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (this.txtLeaveHopitalType.Text.Trim() == "2")
                {
                    this.txtHighReceiveHopital.Focus();
                }
                else if (this.txtLeaveHopitalType.Text.Trim() == "3")
                {
                    this.txtLowerReceiveHopital.Focus();
                }
                else
                {
                    cmbComeBackInMonth.Focus();
                }
            }
        }
        private void txtHighReceiveHopital_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.cmbComeBackInMonth.Focus();
            }
        }

        private void txtLowerReceiveHopital_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.cmbComeBackInMonth.Focus();
            }
        }
        private void cmbComeBackInMonth_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (cmbComeBackInMonth.Tag != null && cmbComeBackInMonth.Tag.ToString() == "2")
                {
                    this.cmbComeBackPurpose.Focus();
                }
                else
                {
                    txtOutComeDay.Focus();
                }
            }
        }

        private void cmbComeBackPurpose_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtOutComeDay.Focus();
            }
        }

        private void txtOutComeDay_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                txtOutComeHour.Focus();
            }
        }
        private void txtOutComeHour_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                txtOutComeMin.Focus();
            }
        }

        private void txtOutComeMin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtInComeDay.Focus();
            }
        }
        private void txtInComeDay_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtInComeHour.Focus();
            }
        }
        private void txtInComeHour_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtInComeMin.Focus();
            }
        }
        private void txtInComeMin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.tab1.SelectedTab = this.tabPage2;
            }
        }
        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// tabҳ�л�
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            //������Ϣ
            if (keyData == Keys.F1)
            {
                this.tab1.SelectedTab = this.tabPage1;
            }
            //�����Ϣ
            if (keyData == Keys.F2)
            {
                this.tab1.SelectedTab = this.tabPage2;
            }
            //������Ϣ
            if (keyData == Keys.F3)
            {
                this.tab1.SelectedTab = this.tabPage3;
            }
            //��Ӥ��Ϣ
            if (keyData == Keys.F4)
            {
                this.tab1.SelectedTab = this.tabPage4;
            }
            //������Ϣ
            if (keyData == Keys.F5)
            {
                this.tab1.SelectedTab = this.tabPage5;
            }
            //������Ϣ
            if (keyData == Keys.F8)
            {
                this.tab1.SelectedTab = this.tabPage6;
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
            this.cmbDeptOutHospital.Focus();
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
                        this.txtDrugAllergy.Text = "δ����";
                    }
                    else
                    {
                        foreach (FS.FrameWork.Models.NeuObject info in inDiagnose)
                        {
                            this.txtDrugAllergy.Text = info.Memo.ToString();
                        }
                    }
                }
            }
        }
        /// <summary>
        /// ������� code��ֵ�¼�
        /// </summary>
        private void cmbClinicDiagName_SelectItemChangEvent()
        {
            if (cmbClinicDiagName.Tag != null)
            {
                this.txtClinicDiagCode.Text = cmbClinicDiagName.Tag.ToString();
            }
        }
        /// <summary>
        /// �����ж� code��ֵ�¼�
        /// </summary>
        private void txtInjuryOrPoisoningCause_SelectItemChangEvent()
        {
            if (txtInjuryOrPoisoningCause.Tag != null)
            {
                this.txtInjuryOrPoisoningCauseCode.Text = txtInjuryOrPoisoningCause.Tag.ToString();
            }
        }
        /// <summary>
        /// ������� code��ֵ�¼�
        /// </summary>
        private void cmbPathologicalDiagName_SelectItemChangEvent()
        {
            if (cmbPathologicalDiagName.Tag != null)
            {
                this.txtPathologicalDiagCode.Text = cmbPathologicalDiagName.Tag.ToString();
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
        /// ҩ������¼�
        /// </summary>
        private void cmbIsDrugAllergy_SelectItemChangEvent()
        {
            if (cmbIsDrugAllergy.Tag != null)
            {
                if (cmbIsDrugAllergy.Tag.ToString() == "1")
                {
                    this.txtDrugAllergy.Enabled = false;
                    this.txtDrugAllergy.Text = "δ����";
                }
                else if (cmbIsDrugAllergy.Tag.ToString() == "2")
                {
                    this.txtDrugAllergy.Focus();
                    this.txtDrugAllergy.Enabled = true;
                    this.txtDrugAllergy.Text = "";
                }
            }
        }
        private void cmbComeBackInMonth_SelectItemChangEvent()
        {
            if (cmbComeBackInMonth.Tag != null)
            {
                if (cmbComeBackInMonth.Tag.ToString() == "1")
                {
                    this.cmbComeBackPurpose.Enabled = false;
                    this.cmbComeBackPurpose.Text = "��";
                }
                else
                {
                    this.cmbComeBackPurpose.Focus();
                    this.cmbComeBackPurpose.Enabled = true;
                    this.cmbComeBackPurpose.Text = "";
                }
            }
        }
        private void txtLeaveHopitalType_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if ((int)e.KeyChar <= 32)
            {
                return;
            }
            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                return;
            }
        }
        private void txtLeaveHopitalType_Leave(object sender, System.EventArgs e)
        {
            switch (this.txtLeaveHopitalType.Text.Trim())
            {
                case "1":
                    this.txtHighReceiveHopital.Text = "-";
                    this.txtLowerReceiveHopital.Text = "-";
                    break;
                case "2":
                    this.txtLowerReceiveHopital.Text = "-";
                    break;
                case "3":
                    this.txtHighReceiveHopital.Text = "-";
                    break;
                case "4":
                    this.txtHighReceiveHopital.Text = "-";
                    this.txtLowerReceiveHopital.Text = "-";
                    break;
                case "5":
                    this.txtHighReceiveHopital.Text = "-";
                    this.txtLowerReceiveHopital.Text = "-";
                    break;
                case "6":
                    this.txtHighReceiveHopital.Text = "-";
                    this.txtLowerReceiveHopital.Text = "-";
                    break;
                default:
                    break;
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
                    this.txtClinicDiagCode.Tag = obj.DiagInfo.ICD10.ID;
                    this.txtClinicDiagCode.Text = obj.DiagInfo.ICD10.Name;
                    //this.txtClinicDocd.Tag = obj.DiagInfo.Doctor.ID;
                    //this.txtClinicDocd.Text = obj.DiagInfo.Doctor.Name;
                    clinicDiag = obj;
                }
                else if (obj.DiagInfo.DiagType.ID == "11" && obj.DiagInfo.IsMain)
                {	//��Ժ���
                    //txtRuyuanDiagNose.Tag = obj.DiagInfo.ICD10.ID;
                    //txtRuyuanDiagNose.Text = obj.DiagInfo.ICD10.Name;
                    InDiag = obj;
                }
            }
            #endregion

            #region ���û������� ���������������
            foreach (FS.HISFC.Models.HealthRecord.Diagnose obj in list)
            {
                if (obj.DiagInfo.DiagType.ID == "10")
                {	//������� 
                    if (this.txtClinicDiagCode.Tag == null)
                    {
                        this.txtClinicDiagCode.Tag = obj.DiagInfo.ICD10.ID;
                        this.txtClinicDiagCode.Text = obj.DiagInfo.ICD10.Name;
                        clinicDiag = obj;
                    }
                }
                else if (obj.DiagInfo.DiagType.ID == "11")
                {	//��Ժ���
                    //if (txtRuyuanDiagNose.Tag == null)
                    //{
                    //    txtRuyuanDiagNose.Tag = obj.DiagInfo.ICD10.ID;
                    //    txtRuyuanDiagNose.Text = obj.DiagInfo.ICD10.Name;
                    //    InDiag = obj;
                    //}
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
            if (!ValidMaxLengh(info.PatientInfo.ID, 14))
            {
                MessageBox.Show("סԺ��ˮ�Ź���");
                return -1;
            }
            if (!ValidMaxLengh(info.CaseNO, 10))
            {
                txtCaseNum.Focus();
                MessageBox.Show("�����Ź���");
                return -1;
            }
            if (!ValidMaxLengh(info.PatientInfo.PID.CardNO, 10))
            {
                MessageBox.Show("���￨�Ź���");
                return -1;
            }
            if (!ValidMaxLengh(info.PatientInfo.SSN, 20))
            {
                txtHealthyCard.Focus();
                MessageBox.Show("ҽ���Ź���");
                return -1;
            }
            if (!ValidMaxLengh(info.PatientInfo.Name, 20))
            {
                txtPatientName.Focus();
                MessageBox.Show("��������");
                return -1;
            }
            if (!ValidMaxLengh(info.PatientInfo.Country.ID, 3))
            {
                cmbCountry.Focus();
                MessageBox.Show("�����������");
                return -1;
            }

            if (!ValidMaxLengh(info.PatientInfo.Nationality.ID, 2))
            {
                cmbNationality.Focus();
                MessageBox.Show("����������");
                return -1;
            }
            if (!ValidMaxLengh(info.PatientInfo.Profession.ID, 3))
            {
                cmbProfession.Focus();
                MessageBox.Show("ְҵ�������");
                return -1;
            }
            if (info.PatientInfo.BloodType.ID != null)
            {
                if (!ValidMaxLengh(info.PatientInfo.BloodType.ID.ToString(), 2))
                {
                    cmbBloodType.Focus();
                    MessageBox.Show("Ѫ�ͱ������");
                    return -1;
                }
            }
            if (info.PatientInfo.MaritalStatus.ID != null)
            {
                if (!ValidMaxLengh(info.PatientInfo.MaritalStatus.ID.ToString(), 10))
                {
                    cmbMaritalStatus.Focus();
                    MessageBox.Show("�����������");
                    return -1;
                }
            }
            if (info.AgeUnit != null)
            {
                if (!ValidMaxLengh(info.AgeUnit, 15))
                {
                    txtPatientAge.Focus();
                    MessageBox.Show("���䵥λ����");
                    return -1;
                }
            }
            //�ж�û��ת�Ƶ��������Ժ�������Ժ�����Ƿ���ͬ
            if ((txtFirstDept.Text=="") && (info.OutDept.ID.ToString() != info.InDept.ID.ToString()))
            {
                cmbDeptInHospital.Focus();
                MessageBox.Show("��Ժ�������Ժ���Ҳ���,��˲�");
                return -1;
            }
            if (!ValidMaxLengh(info.PatientInfo.Age, 3))
            {
                txtPatientAge.Focus();
                MessageBox.Show("�������");
                return -1;
            }
            //if (!ValidMaxLengh(info.PatientInfo.IDCard, 18))
            if (info.PatientInfo.Country.ID == "156")
            {
                if (this.ProcessIDENNO(info.PatientInfo.IDCard) != 1 && info.PatientInfo.IDCard != "δ�ṩ")
                {
                    txtIDNo.Focus();
                    MessageBox.Show("���֤�Ų��Ϸ����������û�ṩ����д��δ�ṩ��");
                    return -1;
                }
            }
            if (!ValidMaxLengh(info.PatientInfo.Pact.PayKind.ID, 6))
            {
                cmbPactKind.Focus();
                MessageBox.Show("�������������");
                return -1;
            }

            if (!ValidMaxLengh(info.PatientInfo.Pact.ID, 10))
            {
                cmbPactKind.Focus();
                MessageBox.Show("ҽ�Ƹ��ѷ�ʽ����");
                return -1;
            }
            if (!ValidMaxLengh(info.PatientInfo.DIST, 50))
            {

                MessageBox.Show("�������");
                return -1;
            }
            if (!ValidMaxLengh(info.PatientInfo.AddressHome, 100))
            {
                MessageBox.Show("��ͥסַ����");
                return -1;
            }
            if (!ValidMaxLengh(info.PatientInfo.PhoneHome, 25))
            {
                txtCurrentPhone.Focus();
                MessageBox.Show("��ͥ�绰����,��δ�ṩ����д���š�-��");
                return -1;
            }
            if (!ValidMaxLengh(info.PatientInfo.HomeZip, 6))
            {
                txtHomeZip.Focus();
                MessageBox.Show("סַ�ʱ����");
                return -1;
            }
            if (!ValidMaxLengh(info.PatientInfo.AddressBusiness, 100))
            {
                txtAddressBusiness.Focus();
                MessageBox.Show("��λ��ַ����,��û������д���š�-��");
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
                MessageBox.Show("��ϵ�˹���,��δ�ṩ����д���š�-��");
                return -1;
            }
            if (!ValidMaxLengh(info.PatientInfo.Kin.RelationLink, 3))
            {
                cmbRelation.Focus();
                MessageBox.Show("��ϵ�˹�ϵ����,��δ�ṩ����д���š�-��");
                return -1;
            }
            if (!ValidMaxLengh(info.PatientInfo.Kin.RelationAddress, 100))
            {
                txtLinkmanAdd.Focus();
                MessageBox.Show("��ϵ��ַ����,��δ�ṩ����д���š�-��");
                return -1;
            }
            if (!ValidMaxLengh(info.PatientInfo.Kin.RelationPhone, 25))
            {
                txtLinkmanTel.Focus();
                MessageBox.Show("��ϵ�绰����,��δ�ṩ����д���š�-��");
                return -1;
            }
            if (!ValidMaxLengh(info.ComeFrom, 100))
            {
                txtChangeDept.Focus();
                MessageBox.Show("ת��ҽԺ");
                return -1;
            }
            if (info.PatientInfo.InTimes > 99)
            {
                txtChangeDept.Focus();
                MessageBox.Show("��Ժ��������");
                return -1;
            }
            if (!ValidMaxLengh(info.BloodRed, 10))
            {
                txtOutComeDay.Focus();
                MessageBox.Show("��ϸ����������");
                return -1;
            }
            if (!ValidMaxLengh(info.BloodPlatelet, 10))
            {
                txtOutComeHour.Focus();
                MessageBox.Show("ѪС����������");
                return -1;
            }
            if (!ValidMaxLengh(info.BloodPlasma, 10))
            {
                txtOutComeMin.Focus();
                MessageBox.Show("Ѫ����������");
                return -1;
            }
            if (!ValidMaxLengh(info.BloodWhole, 10))
            {
                txtInComeDay.Focus();
                MessageBox.Show("ȫѪ��������");
                return -1;
            }
            if (!ValidMaxLengh(info.BloodOther, 10))
            {
                txtInComeHour.Focus();
                MessageBox.Show("������Ѫ��������");
                return -1;
            }
            if (!ValidMaxLengh(info.InjuryOrPoisoningCause, 200))
            {
                this.txtInjuryOrPoisoningCause.Focus();
                MessageBox.Show("�����ж����ⲿԭ�����ƹ���");
                return -1;
            }
            if (!ValidMaxLengh(info.PathologicalDiagName, 50))
            {
                this.cmbPathologicalDiagName.Focus();
                MessageBox.Show("����������ƹ���");
                return -1;
            }
            if (!ValidMaxLengh(info.HighReceiveHopital, 100))
            {
                this.txtHighReceiveHopital.Focus();
                MessageBox.Show("����ҽ�ƻ������ƹ���");
                return -1;
            }
            if (!ValidMaxLengh(info.LowerReceiveHopital, 100))
            {
                this.txtLowerReceiveHopital.Focus();
                MessageBox.Show("ת��������ҽ�ƻ������ƹ���");
                return -1;
            }
            if (!ValidMaxLengh(info.ComeBackPurpose, 100))
            {
                this.cmbComeBackPurpose.Focus();
                MessageBox.Show("��סԺĿ����������");
                return -1;
            }
            if (!ValidMaxLengh(info.OutComeHour.ToString(), 2))
            {
                this.txtOutComeHour.Focus();
                MessageBox.Show("­�����˻�����Ժǰ����Сʱ������");
                return -1;
            }
            if (!ValidMaxLengh(info.OutComeMin.ToString(), 2))
            {
                this.txtOutComeMin.Focus();
                MessageBox.Show("­�����˻�����Ժǰ���Է���������");
                return -1;
            }
            if (!ValidMaxLengh(info.InComeHour.ToString(), 2))
            {
                this.txtInComeHour.Focus();
                MessageBox.Show("­�����˻�����Ժ�����Сʱ������");
                return -1;
            }
            if (!ValidMaxLengh(info.InComeMin.ToString(), 2))
            {
                this.txtInComeMin.Focus();
                MessageBox.Show("­�����˻�����Ժ����Է���������");
                return -1;
            }
            if (!ValidMaxLengh(info.CurrentAddr, 100))
            {
                this.cmbCurrentAdrr.Focus();
                MessageBox.Show("��סַ����");
                return -1;
            }
            if (!ValidMaxLengh(info.CurrentPhone, 18))
            {
                this.txtCurrentPhone.Focus();
                MessageBox.Show("��סַ�绰����");
                return -1;
            }
            if (!ValidMaxLengh(info.CurrentZip, 6))
            {
                this.txtCurrentZip.Focus();
                MessageBox.Show("��סַ�ʱ����");
                return -1;
            }
            if (!ValidMaxLengh(info.InRoom, 20))
            {
                this.txtInRoom.Focus();
                MessageBox.Show("��Ժ��������");
                return -1;
            }
            if (!ValidMaxLengh(info.OutRoom, 20))
            {
                this.txtOutRoom.Focus();
                MessageBox.Show("��Ժ��������");
                return -1;
            }
            //add by 2011-9-20 chengym �����ӡʱ���жϣ����治������
            //if (info.PatientInfo.Pact.ID == null || info.PatientInfo.Pact.ID == "")
            //{
            //    cmbPactKind.Focus();
            //    MessageBox.Show("��ѡ��ҽ�Ƹ��ѷ�ʽ��");
            //    return -1;
            //}
            //if (info.PatientInfo.Pact.ID == "1" || info.PatientInfo.Pact.ID == "2" || info.PatientInfo.Pact.ID == "3")
            //{
            //    if (this.txtHealthyCard.Text.Trim() == "")
            //    {
            //        this.txtHealthyCard.Focus();
            //        MessageBox.Show("ҽ�����������롰�������ţ�ҽ���ţ�����");
            //        return -1;
            //    }
            //}
            //if (info.CurrentAddr == "" || info.CurrentAddr == "��" || info.CurrentAddr == "����")
            //{
            //    this.cmbCurrentAdrr.Focus();
            //    MessageBox.Show("����д��ϸ�ġ���סַ����");
            //    return -1;
            //}
            //if (info.InPath == "")
            //{
            //    this.cmbInPath.Focus();
            //    MessageBox.Show("����д����Ժ;������");
            //    return -1;
            //}
            //if (info.ExampleType == "")
            //{
            //    this.cmbExampleType.Focus();
            //    MessageBox.Show("����д���������͡���");
            //    return -1;
            //}
            //if (info.ClinicPath == "")
            //{
            //    this.cmbClinicPath.Focus();
            //    MessageBox.Show("����д���ٴ�·����������");
            //    return -1;
            //}
            //if (cmbDeptOutHospital.Text == "")
            //{
            //    MessageBox.Show("����д��Ժ������Ϣ");
            //    cmbDeptOutHospital.Focus();
            //    return -1;
            //}
            //if (cmbDeptInHospital.Text == "")
            //{
            //    MessageBox.Show("����д��Ժ������Ϣ");
            //    cmbDeptInHospital.Focus();
            //    return -1;
            //}
            //if (dtFirstTime.Value.Date < this.dtDateIn.Value.Date)
            //{
            //    MessageBox.Show("��һ��ת��ʱ�䲻��С����Ժʱ��");
            //    dtFirstTime.Focus();
            //    return -1;
            //}
            //if (dtFirstTime.Value.Date > this.txtDateOut.Value.Date)
            //{
            //    MessageBox.Show("��һ��ת��ʱ�䲻�ܴ����ڳ�Ժʱ��");
            //    dtFirstTime.Focus();
            //    return -1;
            //}
            //if ((dtFirstTime.Value.Date > dtSecondTime.Value.Date) && txtDeptSecond.Text.Trim() != string.Empty)
            //{
            //    MessageBox.Show("��һ��ת��ʱ�䲻�ܴ��ڵڶ���ת��ʱ��");
            //    dtFirstTime.Focus();
            //    return -1;
            //}
            //if ((dtSecondTime.Value.Date > dtThirdTime.Value.Date) && txtDeptThird.Text.Trim() != string.Empty)
            //{
            //    MessageBox.Show("�ڶ���ת��ʱ�䲻�ܴ��ڵ�����ת��ʱ��");
            //    dtSecondTime.Focus();
            //    return -1;
            //}
            //if (info.PatientInfo.PVisit.ReferringDoctor.ID == "" && info.PatientInfo.PVisit.ConsultingDoctor.ID == "" && info.PatientInfo.PVisit.AttendingDoctor.ID == "")
            //{
            //    MessageBox.Show("�����Ρ�����ҽʦ������ҽ��������Ϊ��");
            //    return -1;
            //}

            //if (info.PatientInfo.PVisit.OutTime.Date < info.PatientInfo.PVisit.InTime.Date)
            //{
            //    MessageBox.Show("����Ժ���ڡ�����С�ڡ���Ժʱ�䡱");
            //    return -1;
            //}
            //if (txtCheckDate.Value.Date > info.PatientInfo.PVisit.OutTime.Date.AddDays(15))
            //{
            //    this.txtCheckDate.Focus();
            //    MessageBox.Show("���ʿ����ڡ����ܳ�����Ժʱ��15��");
            //    return -1;
            //}
            if (info.SalvTimes < info.SuccTimes)
            {
                this.txtSuccTimes.Focus();
                MessageBox.Show("���ȳɹ��������ܴ������ȴ���");
                return -1;
            }
            if (info.SuccTimes > 0 && info.SalvTimes - info.SuccTimes > 1)
            {
                this.txtSalvTimes.Focus();
                MessageBox.Show("Ӧ��ֻ��һ�����Ȳ��ɹ�����˶ԣ�");
                return -1;
            }
            //if (info.Out_Type == "")
            //{
            //    this.txtLeaveHopitalType.Focus();
            //    MessageBox.Show("����д����Ժ��ʽ����");
            //    return -1;
            //}
            //if (info.Out_Type == "2")
            //{
            //    if (info.HighReceiveHopital == "" || info.HighReceiveHopital == "��" || info.HighReceiveHopital == "����")
            //    {
            //        this.txtHighReceiveHopital.Focus();
            //        MessageBox.Show("����д��ҽ��תԺ�������ҽ�ƻ������ơ���");
            //        return -1;
            //    }
            //}
            //if (info.Out_Type == "3")
            //{
            //    if (info.LowerReceiveHopital == "" || info.LowerReceiveHopital=="��" || info.LowerReceiveHopital=="����")
            //    {
            //        this.txtLeaveHopitalType.Focus();
            //        MessageBox.Show("����д��ҽ�����������������/��������Ժ�������ҽ�ƻ������ơ���");
            //        return -1;
            //    }
            //}
            //if (info.ComeBackInMonth == "2")
            //{
            //    if (info.ComeBackPurpose == "")
            //    {
            //        this.cmbComeBackPurpose.Focus();
            //        MessageBox.Show("�г�Ժ31������סԺ�ļƻ�������д����סԺ��Ŀ�ġ���");
            //        return -1;
            //    }
            //}
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
            this.ucDiagNoseInput1.SetReadOnly(type, this.frmType);
            this.ucOperation1.SetReadOnly(type, this.frmType);
            this.ucBabyCardInput1.SetReadOnly(type);
            this.ucTumourCard1.SetReadOnly(type);
            //�������
            cmbPactKind.ReadOnly = type;
            cmbPactKind.EnterVisiable = !type;
            cmbPactKind.BackColor = System.Drawing.Color.White;
            //��������
            txtHealthyCard.ReadOnly = type;
            txtHealthyCard.BackColor = System.Drawing.Color.White;
            //סԺ����
            txtInTimes.ReadOnly = type;
            txtInTimes.BackColor = System.Drawing.Color.White;
            //������ 
            txtCaseNum.ReadOnly = type;
            txtCaseNum.BackColor = System.Drawing.Color.White;
            //����
            txtPatientName.ReadOnly = type;
            txtPatientName.Enabled = !type;
            txtPatientName.BackColor = System.Drawing.Color.White;
            //�Ա�
            cmbPatientSex.ReadOnly = type;
            cmbPatientSex.EnterVisiable = !type;
            cmbPatientSex.BackColor = System.Drawing.Color.White;
            //����
            dtPatientBirthday.Enabled = !type;
            //����
            txtPatientAge.ReadOnly = true;
            txtPatientAge.BackColor = System.Drawing.Color.White;
            //����
            cmbCountry.ReadOnly = type;
            cmbCountry.EnterVisiable = !type;
            cmbCountry.BackColor = System.Drawing.Color.White;
            //���䲻��һ������
            txtBabyAge.ReadOnly = type;
            txtBabyAge.BackColor = System.Drawing.Color.White;
            //��������������
            txtBabyBirthWeight.ReadOnly = type;
            txtBabyBirthWeight.BackColor = System.Drawing.Color.White;
            //��������Ժ����
            txtBabyInWeight.ReadOnly = type;
            txtBabyInWeight.BackColor = System.Drawing.Color.White;
            //������
            cmbBirthAddr.ReadOnly = type;
            cmbBirthAddr.BackColor = System.Drawing.Color.White;
            //����
            cmbDist.ReadOnly = type;
            cmbDist.BackColor = System.Drawing.Color.White;
            //����
            cmbNationality.ReadOnly = type;
            cmbNationality.EnterVisiable = !type;
            cmbNationality.BackColor = System.Drawing.Color.White;
            //���֤
            txtIDNo.ReadOnly = type;
            txtIDNo.BackColor = System.Drawing.Color.White;
            //ְҵ
            cmbProfession.ReadOnly = type;
            cmbProfession.EnterVisiable = !type;
            cmbProfession.BackColor = System.Drawing.Color.White;
            //����
            cmbMaritalStatus.ReadOnly = type;
            cmbMaritalStatus.EnterVisiable = !type;
            cmbMaritalStatus.BackColor = System.Drawing.Color.White;
            //��סַ
            cmbCurrentAdrr.ReadOnly = type;
            cmbCurrentAdrr.BackColor = System.Drawing.Color.White;
            //�绰
            txtCurrentPhone.ReadOnly = type;
            txtCurrentPhone.BackColor = System.Drawing.Color.White;
            //�ʱ�
            txtCurrentZip.ReadOnly = type;
            txtCurrentZip.BackColor = System.Drawing.Color.White;
            //���ڵ�ַ
            cmbHomeAdrr.ReadOnly = type;
            cmbHomeAdrr.BackColor = System.Drawing.Color.White;
            //�����ʱ�
            txtHomeZip.ReadOnly = type;
            txtHomeZip.BackColor = System.Drawing.Color.White;

            //������λ
            txtAddressBusiness.ReadOnly = type;
            txtAddressBusiness.BackColor = System.Drawing.Color.White;
            //��λ�绰
            txtPhoneBusiness.ReadOnly = type;
            txtPhoneBusiness.BackColor = System.Drawing.Color.White;
            //��λ�ʱ�
            txtBusinessZip.ReadOnly = type;
            txtBusinessZip.BackColor = System.Drawing.Color.White;

            //��ϵ������ 
            txtKin.ReadOnly = type;
            txtKin.BackColor = System.Drawing.Color.White;
            //��ϵ
            cmbRelation.ReadOnly = type;
            cmbRelation.EnterVisiable = !type;
            cmbRelation.BackColor = System.Drawing.Color.White;
            //��ϵ��ע
            txtRelationMemo.ReadOnly = type;
            txtRelationMemo.BackColor = System.Drawing.Color.White;
            //l��ϵ�˵�ַ
            txtLinkmanAdd.ReadOnly = type;
            txtLinkmanAdd.BackColor = System.Drawing.Color.White;
            //��ϵ�绰
            txtLinkmanTel.ReadOnly = type;
            txtLinkmanTel.BackColor = System.Drawing.Color.White;
            //��Ժ;��
            cmbInPath.ReadOnly = type;
            cmbInPath.EnterVisiable = !type;
            cmbInPath.BackColor = System.Drawing.Color.White;

            //��Ժʱ�� 
            dtDateIn.Enabled = !type;
            //��Ժ����
            cmbDeptInHospital.ReadOnly = type;
            cmbDeptInHospital.EnterVisiable = !type;
            cmbDeptInHospital.BackColor = System.Drawing.Color.White;
            //��Ժ����
            txtInRoom.ReadOnly = type;
            txtInRoom.BackColor = System.Drawing.Color.White;
            //ת�ƿƱ�
            dtFirstTime.Enabled = !type;
            txtFirstDept.ReadOnly = type;
            txtFirstDept.BackColor = System.Drawing.Color.White;
            dtSecondTime.Enabled = !type;
            txtDeptSecond.ReadOnly = type;
            txtDeptSecond.BackColor = System.Drawing.Color.White;
            dtThirdTime.Enabled = !type;
            txtDeptThird.ReadOnly = type;
            txtDeptThird.BackColor = System.Drawing.Color.White;
            //��Ժʱ��
            txtDateOut.Enabled = !type;
            //��Ժ����
            cmbDeptOutHospital.ReadOnly = type;
            cmbDeptOutHospital.EnterVisiable = !type;
            cmbDeptOutHospital.BackColor = System.Drawing.Color.White;
            //��Ժ����
            txtOutRoom.ReadOnly = type;
            txtOutRoom.BackColor = System.Drawing.Color.White;
            //ʵ��סԺ����
            txtPiDays.ReadOnly = type;
            txtPiDays.BackColor = System.Drawing.Color.White;
            //�������
            if (this.frmType == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.CAS)
            {
                txtClinicDiagCode.ReadOnly = false;
                txtClinicDiagCode.Enabled = true;
            }
            else
            {
                txtClinicDiagCode.ReadOnly = type;
                txtClinicDiagCode.BackColor = System.Drawing.Color.White;
            }
            txtClinicDocd.ReadOnly = type;
            txtClinicDocd.BackColor = System.Drawing.Color.White;
            cmbClinicDiagName.ReadOnly = type;
            cmbClinicDiagName.BackColor = System.Drawing.Color.White;
            //��������
            cmbExampleType.ReadOnly = type;
            cmbExampleType.BackColor = System.Drawing.Color.White;
            //�ٴ�·������
            cmbClinicPath.ReadOnly = type;
            cmbClinicPath.BackColor = System.Drawing.Color.White;
            //���ȴ���
            txtSalvTimes.ReadOnly = type;
            txtSalvTimes.BackColor = System.Drawing.Color.White;
            //�ɹ����ȴ���
            txtSuccTimes.ReadOnly = type;
            txtSuccTimes.BackColor = System.Drawing.Color.White;
            //�����ж����ⲿԭ��
            txtInjuryOrPoisoningCause.ReadOnly = type;
            txtInjuryOrPoisoningCause.BackColor = System.Drawing.Color.White;
            //��������
            if (this.frmType == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.CAS)
            {
                txtInjuryOrPoisoningCauseCode.ReadOnly = false;
                txtInjuryOrPoisoningCauseCode.Enabled = true;
            }
            else
            {
                txtInjuryOrPoisoningCauseCode.ReadOnly = type;
                txtInjuryOrPoisoningCauseCode.BackColor = System.Drawing.Color.White;
            }
            //�������
            cmbPathologicalDiagName.ReadOnly = type;
            cmbPathologicalDiagName.BackColor = System.Drawing.Color.White;
            //��������
            if (this.frmType == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.CAS)
            {
                txtPathologicalDiagCode.ReadOnly = false;
                txtPathologicalDiagCode.Enabled = true;
            }
            else
            {
                txtPathologicalDiagCode.ReadOnly = type;
                txtPathologicalDiagCode.BackColor = System.Drawing.Color.White;
            }
            //�����
            txtPathologicalDiagNum.ReadOnly = type;
            txtPathologicalDiagNum.BackColor = System.Drawing.Color.White;
            //�Ƿ�ҩ�����
            cmbIsDrugAllergy.ReadOnly = type;
            cmbIsDrugAllergy.BackColor = System.Drawing.Color.White;
            //ҩ�����
            txtDrugAllergy.ReadOnly = type;
            txtDrugAllergy.BackColor = System.Drawing.Color.White;
            //��������ʬ��
            cmbDeathPatientBobyCheck.ReadOnly = type;
            cmbDeathPatientBobyCheck.BackColor = System.Drawing.Color.White;

            //Ѫ��
            cmbBloodType.ReadOnly = type;
            cmbBloodType.EnterVisiable = !type;
            cmbBloodType.BackColor = System.Drawing.Color.White;
            cmbRhBlood.ReadOnly = type;
            cmbRhBlood.EnterVisiable = !type;
            cmbRhBlood.BackColor = System.Drawing.Color.White;
            //������
            cmbDeptChiefDoc.ReadOnly = type;
            cmbDeptChiefDoc.EnterVisiable = !type;
            cmbDeptChiefDoc.BackColor = System.Drawing.Color.White;
            //����ҽʦ
            cmbConsultingDoctor.ReadOnly = type;
            cmbConsultingDoctor.EnterVisiable = !type;
            cmbConsultingDoctor.BackColor = System.Drawing.Color.White;
            //����ҽʦ
            cmbAttendingDoctor.ReadOnly = type;
            cmbAttendingDoctor.EnterVisiable = !type;
            cmbAttendingDoctor.BackColor = System.Drawing.Color.White;
            //סԺҽʦ
            cmbAdmittingDoctor.ReadOnly = type;
            cmbAdmittingDoctor.EnterVisiable = !type;
            cmbAdmittingDoctor.BackColor = System.Drawing.Color.White;
            //���λ�ʿ
            cmbDutyNurse.ReadOnly = type;
            cmbDutyNurse.EnterVisiable = !type;
            cmbDutyNurse.BackColor = System.Drawing.Color.White;
            //����ҽʦ
            cmbRefresherDocd.ReadOnly = type;
            cmbRefresherDocd.EnterVisiable = !type;
            cmbRefresherDocd.BackColor = System.Drawing.Color.White;
            //ʵϰҽ��
            cmbPraDocCode.ReadOnly = type;
            cmbPraDocCode.EnterVisiable = !type;
            cmbPraDocCode.BackColor = System.Drawing.Color.White;
            //����Ա
            txtCodingCode.ReadOnly = type;
            txtCodingCode.EnterVisiable = !type;
            txtCodingCode.BackColor = System.Drawing.Color.White;

            //��������
            cmbMrQual.ReadOnly = type;
            cmbMrQual.EnterVisiable = !type;
            cmbMrQual.BackColor = System.Drawing.Color.White;
            //�ʿ�ҽʦ
            cmbQcDocd.ReadOnly = type;
            cmbQcDocd.EnterVisiable = !type;
            cmbQcDocd.BackColor = System.Drawing.Color.White;
            //�ʿػ�ʿ
            cmbQcNucd.ReadOnly = type;
            cmbQcNucd.EnterVisiable = !type;
            cmbQcNucd.BackColor = System.Drawing.Color.White;
            //�ʿ�ʱ��
            txtCheckDate.Enabled = !type;
            //��Ժ��ʽ
            txtLeaveHopitalType.ReadOnly = type;
            txtLeaveHopitalType.BackColor = System.Drawing.Color.White;

            //����ҽ�ƽṹ
            txtHighReceiveHopital.ReadOnly = type;
            txtHighReceiveHopital.BackColor = System.Drawing.Color.White;
            //����ҽ�ƽṹ
            txtLowerReceiveHopital.ReadOnly = type;
            txtLowerReceiveHopital.BackColor = System.Drawing.Color.White;

            //��Ժ31������סԺ
            cmbComeBackInMonth.ReadOnly = type;
            cmbComeBackInMonth.EnterVisiable = !type;
            cmbComeBackInMonth.BackColor = System.Drawing.Color.White;
            // Ŀ��         
            cmbComeBackPurpose.ReadOnly = type;

            //­�����˻��߻��� ��Ժǰ
            txtOutComeDay.ReadOnly = type;
            txtOutComeDay.BackColor = System.Drawing.Color.White;
            //­�����˻��߻��� ��Ժǰ
            txtOutComeHour.ReadOnly = type;
            txtOutComeHour.BackColor = System.Drawing.Color.White;
            //­�����˻��߻��� ��Ժǰ
            txtOutComeMin.ReadOnly = type;
            txtOutComeMin.BackColor = System.Drawing.Color.White;
            //­�����˻��߻��� ��Ժ��
            txtInComeDay.ReadOnly = type;
            txtInComeDay.BackColor = System.Drawing.Color.White;
            //­�����˻��߻��� ��Ժ��
            txtInComeHour.ReadOnly = type;
            txtInComeHour.BackColor = System.Drawing.Color.White;
            //­�����˻��߻��� ��Ժ��
            txtInComeMin.ReadOnly = type;
            txtInComeMin.BackColor = System.Drawing.Color.White;
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
                this.ucFeeInfo1.ClearInfo();
                //ҽ�Ƹ��ѷ�ʽ
                cmbPactKind.Tag = null;
                //��������
                txtHealthyCard.Text = "";
                //ʵ��סԺ����
                txtInTimes.Text = "";
                //������ 
                txtCaseNum.Text = "";
                //����
                txtPatientName.Text = "";
                //�Ա�
                cmbPatientSex.Tag = null;
                //����

                //����
                txtPatientAge.Text = "";
                //����
                cmbCountry.Tag = null;
                //(���䲻��һ�����)����
                txtBabyAge.Text = "";
                //��������������
                txtBabyBirthWeight.Text = "";
                //��������Ժ����
                txtBabyInWeight.Text = "";
                //������
                cmbBirthAddr.Tag = null;
                //����
                cmbDist.Tag = null;
                //����
                cmbNationality.Tag = null;
                //���֤
                txtIDNo.Text = "";
                //ְҵ
                cmbProfession.Tag = null;
                //����
                cmbMaritalStatus.Tag = null;
                //��סַ
                cmbCurrentAdrr.Tag = null;
                //�绰
                txtCurrentPhone.Text = "";
                //�ʱ�
                txtCurrentZip.Text = "";
                //���ڵ�ַ
                cmbHomeAdrr.Tag = null;
                //�����ʱ�
                txtHomeZip.Text = "";
                //������λ����ַ
                txtAddressBusiness.Text = "";
                //��λ�ʱ�
                txtBusinessZip.Text = "";
                //��λ�绰
                txtPhoneBusiness.Text = "";
                //��ϵ�� 
                txtKin.Text = "";
                //��ϵ
                cmbRelation.Tag = null;
                //��ϵ˵��
                txtRelationMemo.Text = "";
                //l��ϵ�˵�ַ
                txtLinkmanAdd.Text = "";
                //��ϵ�绰
                txtLinkmanTel.Text = "";


                //��Ժ;��
                cmbInPath.Tag = null;
                //��Ժ����
                dtDateIn.Value = System.DateTime.Now;
                //��Ժ����
                cmbDeptInHospital.Tag = null;
                //���� 
                txtInRoom.Text = "";
                //ת�ƿƱ�
                txtChangeDept.Text = "";
                txtFirstDept.Text = "";
                dtFirstTime.Value = System.DateTime.Now;
                txtDeptSecond.Text = "";
                dtSecondTime.Value = System.DateTime.Now;
                txtDeptThird.Text = "";
                dtThirdTime.Value = System.DateTime.Now;

                //��Ժʱ��
                txtDateOut.Value = System.DateTime.Now;
                //��Ժ����
                cmbDeptOutHospital.Tag = null;
                //����
                txtOutRoom.Text = "";
                //ʵ��סԺ����
                txtPiDays.Text = "";
                //�������
                txtClinicDiagCode.Text = "";
                //������ϱ���
                cmbClinicDiagName.Tag = null;

                //�����ж�ԭ��
                this.txtInjuryOrPoisoningCause.Text = "";
                //��������
                this.txtInjuryOrPoisoningCauseCode.Text = "";
                //�������
                cmbPathologicalDiagName.Tag = null;
                //��������
                txtPathologicalDiagCode.Text = "";
                //�����
                this.txtPathologicalDiagNum.Text = "";
                //�Ƿ�ҩ�����
                cmbIsDrugAllergy.Tag = null;
                //����ҩ������
                txtDrugAllergy.Text = "";
                //��������ʬ����
                cmbDeathPatientBobyCheck.Tag = null;
                //Ѫ��
                cmbBloodType.Tag = null;
                //RhѪ��
                cmbRhBlood.Tag = null;
                //������
                cmbDeptChiefDoc.Tag = null;
                //���Σ������Σ�ҽʦ
                cmbConsultingDoctor.Tag = null;
                //����ҽʦ
                cmbAttendingDoctor.Tag = null;
                //סԺҽʦ
                cmbAdmittingDoctor.Tag = null;
                //���λ�ʿ
                cmbDutyNurse.Tag = null;
                //����ҽʦ
                cmbRefresherDocd.Tag = null;
                //ʵϰҽ��
                cmbPraDocCode.Tag = null;
                //����Ա
                txtCodingCode.Tag = null;
                //��������
                cmbMrQual.Tag = null;
                //�ʿ�ҽʦ
                cmbQcDocd.Tag = null;
                //�ʿػ�ʿ
                cmbQcNucd.Tag = null;
                //�ʿ�ʱ��
                txtCheckDate.Value = System.DateTime.Now;
                //��Ժ��ʽ
                txtLeaveHopitalType.Text = "";
                //����ҽ�ƻ�����ƽ����߼���
                txtHighReceiveHopital.Text = "";
                //����ҽ�ƻ���(��������������Ժ)
                txtLowerReceiveHopital.Text = "";
                //��Ժ31������סԺ�ƻ�
                cmbComeBackInMonth.Tag = null;
                //��סԺĿ��
                this.cmbComeBackPurpose.Tag = null;
                //­�����˻��߻���ʱ�䣨��Ժǰ�죩
                txtOutComeDay.Text = "";
                //­�����˻��߻���ʱ�䣨��Ժǰʱ��
                txtOutComeHour.Text = "";
                //­�����˻��߻���ʱ�䣨��Ժǰ�֣�
                txtOutComeMin.Text = "";
                //­�����˻��߻���ʱ�䣨��Ժ���죩
                txtInComeDay.Text = "";
                //­�����˻��߻���ʱ�䣨��Ժ���죩
                txtInComeHour.Text = "";
                //­�����˻��߻���ʱ�䣨��Ժ���죩
                txtInComeMin.Text = "";



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
            frmPrintMetCasBase frmPrint = new frmPrintMetCasBase(this);
            frmPrint.PrintPageNum = this.PrintPageNum;
            frmPrint.ShowDialog();
            return 0;
        }
        protected override int OnPrint(object sender, object neuObject)
        {
            this.PrintInterface();
            return base.OnPrint(sender, neuObject);
        }
        /// <summary>
        /// ��ӡ
        /// </summary>
        /// <param name="Type"></param>
        /// <returns></returns>
        public int PrintCase(string Type)
        {
            string tips = string.Empty;
            if (this.ContrastInfo(ref tips) == -1)
            {
                MessageBox.Show(tips + "�����仯���뱣���ٴ�ӡ��", "��ʾ");
                return -1;
            }

            switch (Type)
            {
                case "Print":
                    this.Print();
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
                case "PrintAdditional":
                    this.PrintAdditional(null);
                    break;
                case "PrintAdditionalPreview":
                    this.PrintAdditionalPreview(null, null);
                    break;
                default:
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
        public int Print()
        {
            if (this.PrintFristCheck() == -1)
            {
                return -1;
            }
            if (this.healthRecordPrint == null)
            {
                //this.healthRecordPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterface)) as FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterface;
                this.healthRecordPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(FS.HISFC.Components.HealthRecord.CaseFirstPage.ucMetCasBaseInfo), typeof(FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterface)) as FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterface;
                if (this.healthRecordPrint == null)
                {
                    MessageBox.Show("��ýӿ�IExamPrint����\n������û��ά����صĴ�ӡ�ؼ����ӡ�ؼ�û��ʵ�ֽӿ�IExamPrint\n����ϵͳ����Ա��ϵ��");
                    return -1;
                }
            }
            this.healthRecordPrint.Reset();
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
            if (this.PrintSecCheck() == -1)
            {
                return -1;
            }
            if (this.healthRecordPrintBack == null)
            {
                //this.healthRecordPrintBack = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterfaceBack)) as FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterfaceBack;
                this.healthRecordPrintBack = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(ucMetCasBaseInfo), typeof(FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterfaceBack)) as FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterfaceBack;
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
            this.healthRecordPrintBack.Reset();
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
            if (this.PrintFristCheck() == -1)
            {
                return -1;
            }
            if (this.healthRecordPrint == null)
            {
                //this.healthRecordPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterface)) as FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterface;
                this.healthRecordPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(ucMetCasBaseInfo), typeof(FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterface)) as FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterface;
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
            this.healthRecordPrint.Reset();

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
            //���渳ֵ
            if (this.PrintSecCheck() == -1)
            {
                return -1;
            }
            if (this.healthRecordPrintBack == null)
            {
                //this.healthRecordPrintBack = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterfaceBack)) as FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterfaceBack;
                this.healthRecordPrintBack = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(ucMetCasBaseInfo), typeof(FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterfaceBack)) as FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterfaceBack;
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
            this.healthRecordPrintBack.Reset();
            this.healthRecordPrintBack.ControlValue(this.CaseBase);
            this.healthRecordPrintBack.PrintPreview();
            return 0;
        }

        /// <summary>
        /// ��ӡ����ҳ
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int PrintAdditional(FS.HISFC.Models.HealthRecord.Base obj)
        {
            //���渳ֵ

            if (this.healthRecordInterfaceAdditional == null)
            {
                //this.healthRecordInterfaceAdditional = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterfaceAdditional)) as FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterfaceAdditional;
                this.healthRecordInterfaceAdditional = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(ucMetCasBaseInfo), typeof(FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterfaceAdditional)) as FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterfaceAdditional;
                if (this.healthRecordInterfaceAdditional == null)
                {
                    MessageBox.Show("��ýӿ�IExamPrint(healthRecordInterfaceAdditional)����\n������û��ά����صĴ�ӡ�ؼ����ӡ�ؼ�û��ʵ�ֽӿ�IExamPrint\n����ϵͳ����Ա��ϵ��");
                    return -1;
                }
            }
            if (CaseBase == null)
            {
                return -1;
            }
            this.healthRecordInterfaceAdditional.Reset();
            this.healthRecordInterfaceAdditional.ControlValue(this.CaseBase);
            this.healthRecordInterfaceAdditional.Print();
            return 1;
        }
        /// <summary>
        /// ��ӡ����ҳԤ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        public int PrintAdditionalPreview(object sender, object neuObject)
        {
            if (this.healthRecordInterfaceAdditional == null)
            {
                //this.healthRecordInterfaceAdditional = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterfaceAdditional)) as FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterfaceAdditional;
                this.healthRecordInterfaceAdditional = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(ucMetCasBaseInfo), typeof(FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterfaceAdditional)) as FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterfaceAdditional;
                if (this.healthRecordInterfaceAdditional == null)
                {
                    MessageBox.Show("��ýӿ�IExamPrint(healthRecordInterfaceAdditional)����\n������û��ά����صĴ�ӡ�ؼ����ӡ�ؼ�û��ʵ�ֽӿ�IExamPrint\n����ϵͳ����Ա��ϵ��");
                    return -1;
                }
            }

            if (this.CaseBase == null)
            {
                return -1;
            }
            this.healthRecordInterfaceAdditional.Reset();
            this.healthRecordInterfaceAdditional.ControlValue(this.CaseBase);
            this.healthRecordInterfaceAdditional.PrintPreview();
            return 0;
        }

        /// <summary>
        /// �ԱȽ��������ݿ��Ƿ�һ��
        /// </summary>
        /// <returns></returns>
        private int ContrastInfo(ref string Tips)
        {
            FS.HISFC.Models.HealthRecord.Base dataBaseInfo = baseDml.GetCaseBaseInfo(this.CaseBase.PatientInfo.ID);
            if (dataBaseInfo == null)
            {
                Tips = "�뱣����ҳ���ٴ�ӡ��";
            }
            this.CaseBase = dataBaseInfo;//��ӡ���ݺ����ݿ�һ��
            FS.HISFC.Models.HealthRecord.Base info = new FS.HISFC.Models.HealthRecord.Base();
            int i = this.GetInfoFromPanel(info);
            int ret = 0;
            if (dataBaseInfo.PatientInfo.ID != info.PatientInfo.ID)//סԺ��ˮ��
            {
                Tips = "סԺ��ˮ�š�";
            }
            if (dataBaseInfo.PatientInfo.Pact.ID != info.PatientInfo.Pact.ID)//ҽ�Ƹ��ѷ�ʽ
            {
                Tips += "���ʽ��";
            }
            if (dataBaseInfo.PatientInfo.SSN != info.PatientInfo.SSN)//��������
            {
                Tips += "ҽ���š�";
            }
            if (dataBaseInfo.PatientInfo.InTimes != info.PatientInfo.InTimes)//סԺ����
            {
                Tips += "סԺ������";
            }
            if (dataBaseInfo.CaseNO != info.CaseNO)
            {
                Tips += "������";
            }
            if (dataBaseInfo.PatientInfo.Name != info.PatientInfo.Name)//����
            {
                Tips += "������";
            }

            if (dataBaseInfo.PatientInfo.Sex.ID.ToString() != info.PatientInfo.Sex.ID.ToString())//�Ա�
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
            //��������������
            try
            {
                int weight = FS.FrameWork.Function.NConvert.ToInt32(dataBaseInfo.BabyBirthWeight);
                if (weight > 0)
                {
                    if (dataBaseInfo.BabyBirthWeight != null && info.BabyBirthWeight != null && dataBaseInfo.BabyBirthWeight.Trim() != info.BabyBirthWeight.Trim())
                    {
                        Tips += "�������������ء�";
                    }
                }
            }
            catch
            {
            }
            //��������Ժ����
            try
            {
                int inWeight = FS.FrameWork.Function.NConvert.ToInt32(dataBaseInfo.BabyBirthWeight);
                if (inWeight > 0)
                {
                    if (dataBaseInfo.BabyInWeight != null && info.BabyInWeight != null && dataBaseInfo.BabyInWeight.Trim() != info.BabyInWeight.Trim())
                    {
                        Tips += "��������Ժ���ء�";
                    }
                }
            }
            catch
            {
            }
            if (dataBaseInfo.PatientInfo.AreaCode != info.PatientInfo.AreaCode)//������
            {
                Tips += "�����ء�";
            }
            if (dataBaseInfo.PatientInfo.DIST != info.PatientInfo.DIST)//����
            {
                Tips += "���ᡢ";
            }
            if (dataBaseInfo.PatientInfo.IDCard.ToString().Trim() != info.PatientInfo.IDCard.ToString().Trim())//���֤��
            {
                Tips += "���֤�š�";
            }

            if (dataBaseInfo.PatientInfo.Profession.ID != info.PatientInfo.Profession.ID)//ְҵ
            {
                Tips += "ְҵ��";
            }
            if (dataBaseInfo.PatientInfo.MaritalStatus.ID.ToString().Trim() != info.PatientInfo.MaritalStatus.ID.ToString().Trim())//���
            {
                Tips += "������";
            }
            // ��סַ
            if (dataBaseInfo.CurrentAddr != info.CurrentAddr)
            {
                Tips += "��סַ��";
            }
            //��סַ�绰
            if (dataBaseInfo.CurrentPhone != info.CurrentPhone)
            {
                Tips += "��סַ�绰��";
            }
            //��סַ�ʱ�
            if (info.CurrentZip != info.CurrentZip)
            {
                Tips += "��סַ�ʱࡢ";
            }
            if (dataBaseInfo.PatientInfo.AddressHome != info.PatientInfo.AddressHome)//��ͥסַ
            {
                Tips += "��ͥסַ��";
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
            if (dataBaseInfo.InPath != info.InPath)
            {
                Tips += "��Ժ;����";
            }

            if (dataBaseInfo.PatientInfo.PVisit.InTime.Date != info.PatientInfo.PVisit.InTime.Date)//��Ժ����
            {
                Tips += "��Ժ���ڡ�";
            }
            if (dataBaseInfo.InDept.ID != info.InDept.ID)//��Ժ���Ҵ���
            {
                Tips += "��Ժ���ҡ�";
            }
            if (dataBaseInfo.PatientInfo.PVisit.OutTime.Date != info.PatientInfo.PVisit.OutTime.Date)//��Ժ����
            {
                Tips += "��Ժ���ڡ�";
            }
            if (dataBaseInfo.OutDept.ID != info.OutDept.ID)//��Ժ���Ҵ���
            {
                Tips += "��Ժ���ҡ�";
            }
            if (dataBaseInfo.InHospitalDays != info.InHospitalDays)//סԺ����
            {
                Tips += "סԺ������";
            }
            //�������
            if (dataBaseInfo.ClinicDiag.Name != info.ClinicDiag.Name)
            {
                Tips += "������ϡ�";
            }
            //������ϱ���
            if (dataBaseInfo.ClinicDiag.ID != info.ClinicDiag.ID)
            {
                Tips += "������ϱ��롢";
            }
            if (dataBaseInfo.ClinicDoc.ID != info.ClinicDoc.ID)//�������ҽ��
            {
                Tips += "�������ҽ����";
            }
            //��������
            if (dataBaseInfo.ExampleType != info.ExampleType)
            {
                Tips += "�������͡�";
            }
            //�ٴ�·������
            if (dataBaseInfo.ClinicPath != info.ClinicPath)
            {
                Tips += "�ٴ�·��������";
            }
            if (dataBaseInfo.SalvTimes != info.SalvTimes)//���ȴ���
            {
                Tips += "���ȴ�����";
            }
            if (dataBaseInfo.SuccTimes != info.SuccTimes)//�ɹ�����
            {
                Tips += "�ɹ�������";
            }
            //�����ж�ԭ��
            if (dataBaseInfo.InjuryOrPoisoningCauseCode != info.InjuryOrPoisoningCauseCode)
            {
                Tips += "�����ж�ԭ����롢";
            }
            if (dataBaseInfo.InjuryOrPoisoningCause != info.InjuryOrPoisoningCause)
            {
                Tips += "�����ж�ԭ��";
            }
            //�������

            if (dataBaseInfo.PathologicalDiagName != info.PathologicalDiagName)
            {
                Tips += "������ϡ�";
            }

            if (dataBaseInfo.PathologicalDiagCode != info.PathologicalDiagCode)
            {
                Tips += "������ϱ��롢";
            }

            if (dataBaseInfo.PathNum != info.PathNum)
            {
                Tips += "����š�";
            }
            //ҩ�����
            if (dataBaseInfo.AnaphyFlag != info.AnaphyFlag)
            {
                Tips += "ҩ�������";
            }
            if (dataBaseInfo.FirstAnaphyPharmacy.ID != info.FirstAnaphyPharmacy.ID)//����ҩ������
            {
                Tips += "����ҩ�����ơ�";
            }
            if (dataBaseInfo.CadaverCheck != info.CadaverCheck)//ʬ��
            {
                Tips += "ʬ�졢";
            }
            if (dataBaseInfo.PatientInfo.BloodType.ID.ToString().Trim() != info.PatientInfo.BloodType.ID.ToString().Trim())//Ѫ�ͱ���
            {
                Tips += "Ѫ�͡�";
            }
            if (dataBaseInfo.RhBlood != info.RhBlood)//RhѪ��(����)
            {
                Tips += "RhѪ�͡�";
            }
            if (dataBaseInfo.PatientInfo.PVisit.ReferringDoctor.ID != info.PatientInfo.PVisit.ReferringDoctor.ID)//�����δ���
            {
                Tips += "�����Ρ�";
            }
            if (dataBaseInfo.PatientInfo.PVisit.ConsultingDoctor.ID != info.PatientInfo.PVisit.ConsultingDoctor.ID)//����ҽʦ����
            {
                Tips += "����ҽʦ��";
            }
            if (dataBaseInfo.PatientInfo.PVisit.AttendingDoctor.ID != info.PatientInfo.PVisit.AttendingDoctor.ID)//����ҽʦ����
            {
                Tips += "����ҽʦ��";
            }
            if (dataBaseInfo.PatientInfo.PVisit.AdmittingDoctor.ID != info.PatientInfo.PVisit.AdmittingDoctor.ID)//סԺҽʦ����
            {
                Tips += "סԺҽʦ��";
            }
            if (dataBaseInfo.DutyNurse.ID != info.DutyNurse.ID)//���λ�ʿ
            {
                Tips += "���λ�ʿ��";
            }
            if (dataBaseInfo.RefresherDoc.ID != info.RefresherDoc.ID)//����ҽʦ����
            {
                Tips += "����ҽʦ��";
            }
            if (dataBaseInfo.PatientInfo.PVisit.TempDoctor.ID != info.PatientInfo.PVisit.TempDoctor.ID)//ʵϰҽʦ����
            {
                Tips += "ʵϰҽʦ�";
            }
            if (dataBaseInfo.CodingOper.ID != info.CodingOper.ID)
            {
                Tips += "����Ա��";
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
            //��Ժ��ʽ
            if (dataBaseInfo.Out_Type != info.Out_Type)
            {
                Tips += "��Ժ��ʽ��";
            }
            //ҽ��תԺ����ҽ�ƻ���
            if (dataBaseInfo.HighReceiveHopital != info.HighReceiveHopital)
            {
                Tips += "ҽ��תԺ����ҽ�ƻ�����";
            }
            //ҽ��ת����
            if (dataBaseInfo.LowerReceiveHopital != info.LowerReceiveHopital)
            {
                Tips += "ҽ��ת������";
            }
            //��Ժ31������סԺ�ƻ�
            if (dataBaseInfo.ComeBackInMonth != info.ComeBackInMonth)
            {
                Tips += "��Ժ31������סԺ�ƻ���";
            }
            //��Ժ31����סԺĿ��

            if (dataBaseInfo.ComeBackPurpose != info.ComeBackPurpose)
            {
                Tips += "��Ժ31����סԺĿ�ġ�";
            }
            //­�����˻��߻���ʱ�� -��Ժǰ ��
            if (dataBaseInfo.OutComeDay != info.OutComeDay)
            {
                Tips += "­�����˻��߻���ʱ����Ժǰ(��)��";
            }
            //­�����˻��߻���ʱ�� -��Ժǰ Сʱ
            if (dataBaseInfo.OutComeHour != info.OutComeHour)
            {
                Tips += "­�����˻��߻���ʱ����Ժǰ(Сʱ)��";
            }
            //­�����˻��߻���ʱ�� -��Ժǰ ����
            if (dataBaseInfo.OutComeMin != info.OutComeMin)
            {
                Tips += "­�����˻��߻���ʱ����Ժǰ(����)��";
            }
            //­�����˻��߻���ʱ�� -��Ժ�� ��
            if (dataBaseInfo.InComeDay != info.InComeDay)
            {
                Tips += "­�����˻��߻���ʱ����Ժ��(��)��";
            }
            //­�����˻��߻���ʱ�� -��Ժ�� Сʱ
            if (dataBaseInfo.InComeHour != info.InComeHour)
            {
                Tips += "­�����˻��߻���ʱ����Ժ��(Сʱ)��";
            }
            //­�����˻��߻���ʱ�� -��Ժ�� ����
            if (dataBaseInfo.InComeMin != info.InComeMin)
            {
                Tips += "­�����˻��߻���ʱ����Ժ��(����)��";
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

        /// <summary>
        /// ��˵������ҳȱ��Ƚ϶࣬���Դ�ӡʱ����Ҫ����ʾ
        /// </summary>
        private int PrintFristCheck()
        {
            if (cmbPactKind.Tag == null || (cmbPactKind.Tag != null && cmbPactKind.Tag.ToString() == ""))
            {
                MessageBox.Show("��ѡ��ҽ�Ƹ��ʽ��", "��ʾ��");
                this.cmbPactKind.Focus();
                return -1;
            }
            //ҽ�����Ѻ�
            if (txtHealthyCard.Text.Trim() == "")
            {
                MessageBox.Show("����д��������/ҽ���ţ�", "��ʾ��");
                this.txtHealthyCard.Focus();
                return -1;
            }
            //סԺ����
            if (txtInTimes.Text.Trim() == "")
            {
                MessageBox.Show("����дסԺ������", "��ʾ��");
                this.txtInTimes.Focus();
                return -1;
            }
            //סԺ��  ������
            if (txtCaseNum.Text.Trim() == "")
            {
                MessageBox.Show("����дסԺ�ţ�", "��ʾ��");
                this.txtCaseNum.Focus();
                return -1;
            }
            //����
            if (txtPatientName.Text.Trim() == "")
            {
                MessageBox.Show("����д������", "��ʾ��");
                this.txtPatientName.Focus();
                return -1;
            }
            //�Ա�
            if (cmbPatientSex.Tag == null || (cmbPatientSex.Tag != null && cmbPatientSex.Tag.ToString() == ""))
            {
                MessageBox.Show("��ѡ�����Ա�", "��ʾ��");
                this.cmbPatientSex.Focus();
                return -1;
            }
            //����
            if (txtPatientAge.Text.Trim() == "")
            {
                MessageBox.Show("��ѡ���߳������ڣ�", "��ʾ��");
                this.dtPatientBirthday.Focus();
                return -1;
            }
            //���� ����
            if (cmbCountry.Tag == null || (cmbCountry.Tag != null && cmbCountry.Tag.ToString() == ""))
            {
                MessageBox.Show("��ѡ���߹�����", "��ʾ��");
                this.cmbCountry.Focus();
                return -1;
            }
            //���� 
            if (cmbCountry.Tag.ToString() == "156")
            {
                if (cmbNationality.Tag == null || (cmbNationality.Tag != null && cmbNationality.Tag.ToString() == ""))
                {
                    MessageBox.Show("��ѡ�������壡", "��ʾ��");
                    this.cmbNationality.Focus();
                    return -1;
                }
            }
            //��������������
            FS.HISFC.BizLogic.HealthRecord.Baby babyMgr = new FS.HISFC.BizLogic.HealthRecord.Baby();
            ArrayList babyAl = babyMgr.QueryBabyByInpatientNo(this.CaseBase.PatientInfo.ID);
            if (babyAl != null && babyAl.Count > 0)
            {
                int weith = 0;
                try
                {
                    weith = FS.FrameWork.Function.NConvert.ToInt32(this.txtBabyBirthWeight.Text);
                }
                catch
                {
                    weith = 0;
                }
                if (weith == 0)
                {
                    if (this.txtBabyBirthWeight.Text.Trim() == "-" || this.txtBabyBirthWeight.Text.Trim() == "")
                    {
                        MessageBox.Show("��������������д��������������,��������Ժ���أ�", "��ʾ��");
                        this.txtBabyBirthWeight.Focus();
                        return -1;
                    }
                }
            }
            //��������Ժ����
            if (this.txtBabyInWeight.Text == "")
            {
                MessageBox.Show("����д��������Ժ���أ�", "��ʾ��");
                this.txtBabyInWeight.Focus();
                return -1;
            }
            //������
            if (cmbCountry.Tag.ToString() == "156")
            {
                ArrayList addrlist = con.GetList("GBXZQYHF");
                bool a = true;

                if ((cmbBirthAddr.Text.Trim().ToString().Length > 3 && cmbBirthAddr.Text.Trim().ToString().Substring(0, 3) == "������") || (cmbBirthAddr.Text.Trim().ToString().Length > 3 && cmbBirthAddr.Text.Trim().ToString().Substring(0, 3) == "�����") || (cmbBirthAddr.Text.Trim().ToString().Length > 3 && cmbBirthAddr.Text.Trim().ToString().Substring(0, 3) == "�Ϻ���") || (cmbBirthAddr.Text.Trim().ToString().Length > 3 && cmbBirthAddr.Text.Trim().ToString().Substring(0, 3) == "������") || (cmbBirthAddr.Text.Trim().ToString().Length > 6 && cmbBirthAddr.Text.Trim().ToString().Substring(0, 7) == "����ر�������") || (cmbBirthAddr.Text.Trim().ToString().Length > 6 && cmbBirthAddr.Text.Trim().ToString().Substring(0, 7) == "�����ر�������") || (cmbBirthAddr.Text.Trim().ToString().Length > 2 && cmbBirthAddr.Text.Trim().ToString().Substring(0, 3) == "̨��ʡ") || (cmbBirthAddr.Text.Trim().ToString().Length > 2 && cmbBirthAddr.Text.Trim().ToString().Substring(0, 2) == "����") || (cmbBirthAddr.Text.Trim().ToString().Length > 2 && cmbBirthAddr.Text.Trim().ToString().Substring(0, 2) == "�½�") || (cmbBirthAddr.Text.Trim().ToString().Length > 7 && cmbBirthAddr.Text.Trim().ToString().Substring(0, 7) == "����׳��������") || (cmbBirthAddr.Text.Trim().ToString().Length > 3 && cmbBirthAddr.Text.Trim().ToString().Substring(0, 3) == "���ɹ�") || (cmbBirthAddr.Text.Trim().ToString().Length > 2 && cmbBirthAddr.Text.Trim().ToString().Substring(0, 2) == "����"))
                {
                    a = false;
                }
                else if (cmbCountry.Tag.ToString() == "156")
                {
                    if (cmbBirthAddr.Text.Trim().IndexOf("ʡ") > 0 && cmbBirthAddr.Text.Trim().IndexOf("��") > 0 && (cmbBirthAddr.Text.Trim().IndexOf("��") > 0 || cmbBirthAddr.Text.Trim().IndexOf("��") > 0))
                    {
                        a = false;
                    }
                    else if (cmbBirthAddr.Text.Trim().IndexOf("ʡ") > 0 && cmbBirthAddr.Text.Trim().IndexOf("��") > 0 && (cmbBirthAddr.Text.Trim().IndexOf("��") > 0 || cmbBirthAddr.Text.Trim().IndexOf("��") > 0))
                    {
                        a = false;
                    }

                    else if (cmbBirthAddr.Text.Trim().IndexOf("ʡ") > 0 && cmbBirthAddr.Text.Trim().IndexOf("������") > 0)
                    {
                        a = false;
                    }
                    else
                    {
                        MessageBox.Show("�밴��ʡ-��-��(��)����д���߳����أ����û�������أ�ֱ���ڵ�ַ����ӡ�������", "��ʾ��");
                        this.cmbBirthAddr.Focus();
                        return -1;
                    }



                }

                if (a)
                {
                    MessageBox.Show("�밴��ʡ-��-��(��)����д���߳����أ����û�������أ�ֱ���ڵ�ַ����ӡ�������", "��ʾ��");
                    this.cmbBirthAddr.Focus();
                    return -1;
                }
            }
            //if (cmbBirthAddr.Text.Trim() == "")
            //{
            //    MessageBox.Show("����д���߳����أ�", "��ʾ��");
            //    this.cmbBirthAddr.Focus();
            //    return -1;
            //}
            //����
            //if (cmbDist.Text.Trim() == "")
            //{
            //    MessageBox.Show("����д���߼��ᣡ", "��ʾ��");
            //    this.cmbDist.Focus();
            //    return -1;
            //}
            if (cmbCountry.Tag.ToString() == "156")
            {
                ArrayList addrlist = con.GetList("GBXZQYHF");
                bool a = true;

                if ((cmbDist.Text.Trim().ToString().Length > 3 && cmbDist.Text.Trim().ToString().Substring(0, 3) == "������") || (cmbDist.Text.Trim().ToString().Length > 3 && cmbDist.Text.Trim().ToString().Substring(0, 3) == "�����") || (cmbDist.Text.Trim().ToString().Length > 3 && cmbDist.Text.Trim().ToString().Substring(0, 3) == "�Ϻ���") || (cmbDist.Text.Trim().ToString().Length > 3 && cmbDist.Text.Trim().ToString().Substring(0, 3) == "������") || (cmbDist.Text.Trim().ToString().Length > 6 && cmbDist.Text.Trim().ToString().Substring(0, 7) == "����ر�������") || (cmbDist.Text.Trim().ToString().Length > 6 && cmbDist.Text.Trim().ToString().Substring(0, 7) == "�����ر�������") || (cmbDist.Text.Trim().ToString().Length > 2 && cmbDist.Text.Trim().ToString().Substring(0, 3) == "̨��ʡ") || (cmbDist.Text.Trim().ToString().Length > 2 && cmbDist.Text.Trim().ToString().Substring(0, 2) == "����") || (cmbDist.Text.Trim().ToString().Length > 2 && cmbDist.Text.Trim().ToString().Substring(0, 2) == "�½�") || (cmbDist.Text.Trim().ToString().Length > 7 && cmbDist.Text.Trim().ToString().Substring(0, 7) == "����׳��������") || (cmbDist.Text.Trim().ToString().Length > 3 && cmbDist.Text.Trim().ToString().Substring(0, 3) == "���ɹ�") || (cmbDist.Text.Trim().ToString().Length > 2 && cmbDist.Text.Trim().ToString().Substring(0, 2) == "����"))
                {
                    a = false;
                }
                else if (cmbCountry.Tag.ToString() == "156")
                {
                    if (cmbDist.Text.Trim().IndexOf("ʡ") > 0 && cmbDist.Text.Trim().IndexOf("��") > 0 && (cmbDist.Text.Trim().IndexOf("��") > 0 || cmbDist.Text.Trim().IndexOf("��") > 0))
                    {
                        a = false;
                    }
                    else if (cmbDist.Text.Trim().IndexOf("ʡ") > 0 && cmbDist.Text.Trim().IndexOf("��") > 0 && (cmbDist.Text.Trim().IndexOf("��") > 0 || cmbDist.Text.Trim().IndexOf("��") > 0))
                    {
                        a = false;
                    }

                    else if (cmbDist.Text.Trim().IndexOf("ʡ") > 0 && cmbDist.Text.Trim().IndexOf("������") > 0)
                    {
                        a = false;
                    }
                    else
                    {
                        MessageBox.Show("�밴��ʡ-��-��(��)����д���߼��ᣡ���û�������أ�ֱ���ڵ�ַ����ӡ�������", "��ʾ��");
                        this.cmbDist.Focus();
                        return -1;
                    }



                }

                if (a)
                {
                    MessageBox.Show("�밴��ʡ-��-��(��)����д���߼��ᣡ���û�������أ�ֱ���ڵ�ַ����ӡ�������", "��ʾ��");
                    this.cmbDist.Focus();
                    return -1;
                }
            }
            //���֤��
            if (txtIDNo.Text.Trim() == "")
            {
                MessageBox.Show("����д�������֤�ţ����û������δ�ṩ��", "��ʾ��");
                this.txtIDNo.Focus();
                return -1;
            }
            //ְҵ
            if (cmbProfession.Tag == null || (cmbProfession.Tag != null && cmbProfession.Tag.ToString() == ""))
            {
                MessageBox.Show("��ѡ����ְҵ��", "��ʾ��");
                this.cmbProfession.Focus();
                return -1;
            }
            //����
            if (cmbMaritalStatus.Tag == null || (cmbMaritalStatus.Tag != null && cmbMaritalStatus.Tag.ToString() == ""))
            {
                MessageBox.Show("��ѡ���߻�����", "��ʾ��");
                this.cmbMaritalStatus.Focus();
                return -1;
            }
            //��סַ  
            if (cmbCountry.Tag.ToString() == "156")
            {
                ArrayList addrlist = con.GetList("GBXZQYHF");
                bool a = true;

                if ((cmbCurrentAdrr.Text.Trim().ToString().Length > 3 && cmbCurrentAdrr.Text.Trim().ToString().Substring(0, 3) == "������") || (cmbCurrentAdrr.Text.Trim().ToString().Length > 3 && cmbCurrentAdrr.Text.Trim().ToString().Substring(0, 3) == "�����") || (cmbCurrentAdrr.Text.Trim().ToString().Length > 3 && cmbCurrentAdrr.Text.Trim().ToString().Substring(0, 3) == "�Ϻ���") || (cmbCurrentAdrr.Text.Trim().ToString().Length > 3 && cmbCurrentAdrr.Text.Trim().ToString().Substring(0, 3) == "������") || (cmbCurrentAdrr.Text.Trim().ToString().Length > 6 && cmbCurrentAdrr.Text.Trim().ToString().Substring(0, 7) == "����ر�������") || (cmbCurrentAdrr.Text.Trim().ToString().Length > 6 && cmbCurrentAdrr.Text.Trim().ToString().Substring(0, 7) == "�����ر�������") || (cmbCurrentAdrr.Text.Trim().ToString().Length > 2 && cmbCurrentAdrr.Text.Trim().ToString().Substring(0, 3) == "̨��ʡ") || (cmbCurrentAdrr.Text.Trim().ToString().Length > 2 && cmbCurrentAdrr.Text.Trim().ToString().Substring(0, 2) == "����") || (cmbCurrentAdrr.Text.Trim().ToString().Length > 2 && cmbCurrentAdrr.Text.Trim().ToString().Substring(0, 2) == "�½�") || (cmbCurrentAdrr.Text.Trim().ToString().Length > 7 && cmbCurrentAdrr.Text.Trim().ToString().Substring(0, 7) == "����׳��������") || (cmbCurrentAdrr.Text.Trim().ToString().Length > 3 && cmbCurrentAdrr.Text.Trim().ToString().Substring(0, 3) == "���ɹ�") || (cmbCurrentAdrr.Text.Trim().ToString().Length > 2 && cmbCurrentAdrr.Text.Trim().ToString().Substring(0, 2) == "����"))
                {
                    a = false;
                }
                else if (cmbCountry.Tag.ToString() == "156")
                {
                    if (cmbCurrentAdrr.Text.Trim().IndexOf("ʡ") > 0 && cmbCurrentAdrr.Text.Trim().IndexOf("��") > 0 && (cmbCurrentAdrr.Text.Trim().IndexOf("��") > 0 || cmbCurrentAdrr.Text.Trim().IndexOf("��") > 0))
                    {
                        a = false;
                    }
                    else if (cmbCurrentAdrr.Text.Trim().IndexOf("ʡ") > 0 && cmbCurrentAdrr.Text.Trim().IndexOf("��") > 0 && (cmbCurrentAdrr.Text.Trim().IndexOf("��") > 0 || cmbCurrentAdrr.Text.Trim().IndexOf("��") > 0))
                    {
                        a = false;
                    }
                    //else if (cmbCurrentAdrr.Text.Trim().IndexOf("��") > 0 && cmbCurrentAdrr.Text.Trim().IndexOf("��") > 0)
                    //{
                    //    a = false;
                    //}
                    else if (cmbCurrentAdrr.Text.Trim().IndexOf("ʡ") > 0 && cmbCurrentAdrr.Text.Trim().IndexOf("������") > 0)
                    {
                        a = false;
                    }
                    else
                    {
                        MessageBox.Show("�밴��ʡ-��-��(��)����д������סַ�����û�������أ�ֱ���ڵ�ַ����ӡ�������", "��ʾ��");
                        this.cmbCurrentAdrr.Focus();
                        return -1;
                    }



                }

                if (a)
                {
                    MessageBox.Show("�밴��ʡ-��-��(��)����д������סַ�����û�������أ�ֱ���ڵ�ַ����ӡ�������", "��ʾ��");
                    this.cmbCurrentAdrr.Focus();
                    return -1;
                }
            }

            //��סַ�绰 //��ͥ�绰
            if (this.cmbCurrentAdrr.Text.Trim() != "-" && this.txtCurrentPhone.Text.Trim() == "")
            {
                MessageBox.Show("����д������סַ�绰��", "��ʾ��");
                this.txtCurrentPhone.Focus();
                return -1;
            }
            //��סַ�ʱ�
            if (this.cmbCurrentAdrr.Text.Trim() != "-" && txtCurrentZip.Text.Trim() == "")
            {
                MessageBox.Show("����д������סַ�ʱ࣡", "��ʾ��");
                this.txtCurrentZip.Focus();
                return -1;
            }
            //��ͥסַ���ڵ�ַ
            //if (cmbHomeAdrr.Text.Trim() == "")
            //{
            //    MessageBox.Show("����д���߼�ͥסַ��", "��ʾ��");
            //    this.cmbHomeAdrr.Focus();
            //    return -1;
            //}
            if (cmbCountry.Tag.ToString() == "156")
            {
                bool b = true;

                if ((cmbHomeAdrr.Text.Trim().ToString().Length > 3 && cmbHomeAdrr.Text.Trim().ToString().Substring(0, 3) == "������") || (cmbHomeAdrr.Text.Trim().ToString().Length > 3 && cmbHomeAdrr.Text.Trim().ToString().Substring(0, 3) == "�����") || (cmbHomeAdrr.Text.Trim().ToString().Length > 3 && cmbHomeAdrr.Text.Trim().ToString().Substring(0, 3) == "�Ϻ���") || (cmbHomeAdrr.Text.Trim().ToString().Length > 3 && cmbHomeAdrr.Text.Trim().ToString().Substring(0, 3) == "������") || (cmbHomeAdrr.Text.Trim().ToString().Length > 6 && cmbHomeAdrr.Text.Trim().ToString().Substring(0, 7) == "����ر�������") || (cmbHomeAdrr.Text.Trim().ToString().Length > 6 && cmbHomeAdrr.Text.Trim().ToString().Substring(0, 7) == "�����ر�������") || (cmbHomeAdrr.Text.Trim().ToString().Length > 2 && cmbHomeAdrr.Text.Trim().ToString().Substring(0, 3) == "̨��ʡ") || (cmbHomeAdrr.Text.Trim().ToString().Length > 2 && cmbHomeAdrr.Text.Trim().ToString().Substring(0, 2) == "����") || (cmbHomeAdrr.Text.Trim().ToString().Length > 2 && cmbHomeAdrr.Text.Trim().ToString().Substring(0, 2) == "�½�") || (cmbHomeAdrr.Text.Trim().ToString().Length > 7 && cmbHomeAdrr.Text.Trim().ToString().Substring(0, 7) == "����׳��������") || (cmbHomeAdrr.Text.Trim().ToString().Length > 3 && cmbHomeAdrr.Text.Trim().ToString().Substring(0, 3) == "���ɹ�") || (cmbHomeAdrr.Text.Trim().ToString().Length > 2 && cmbHomeAdrr.Text.Trim().ToString().Substring(0, 2) == "����"))
                {
                    b = false;
                }
                else if (cmbCountry.Tag.ToString() == "156")
                {
                    if (cmbHomeAdrr.Text.Trim().IndexOf("ʡ") > 0 && cmbHomeAdrr.Text.Trim().IndexOf("��") > 0 && (cmbHomeAdrr.Text.Trim().IndexOf("��") > 0 || cmbHomeAdrr.Text.Trim().IndexOf("��") > 0))
                    {
                        b = false;
                    }
                    else if (cmbHomeAdrr.Text.Trim().IndexOf("ʡ") > 0 && cmbHomeAdrr.Text.Trim().IndexOf("��") > 0 && (cmbHomeAdrr.Text.Trim().IndexOf("��") > 0 || cmbHomeAdrr.Text.Trim().IndexOf("��") > 0))
                    {
                        b = false;
                    }
                    //else if (cmbHomeAdrr.Text.Trim().IndexOf("��") > 0 && cmbHomeAdrr.Text.Trim().IndexOf("��") > 0)
                    //{
                    //    b = false;
                    //}
                    else if (cmbHomeAdrr.Text.Trim().IndexOf("ʡ") > 0 && cmbHomeAdrr.Text.Trim().IndexOf("������") > 0)
                    {
                        b = false;
                    }
                    else
                    {
                        MessageBox.Show("�밴��ʡ-��-��(��)����д���߻��ڵ�ַ�����û�������أ�ֱ���ڵ�ַ����ӡ�������", "��ʾ��");
                        this.cmbCurrentAdrr.Focus();
                        return -1;
                    }

                }

                if (b)
                {
                    MessageBox.Show("�밴��ʡ-��-��(��)����д���߻��ڵ�ַ�����û�������أ�ֱ���ڵ�ַ����ӡ�������", "��ʾ��");
                    this.cmbHomeAdrr.Focus();
                    return -1;
                }
            }

            //סַ�ʱ�
            if (cmbHomeAdrr.Text.Trim() != "-" && txtHomeZip.Text.Trim() == "")
            {
                MessageBox.Show("����д���߼�ͥסַ�ʱ࣡", "��ʾ��");
                this.txtHomeZip.Focus();
                return -1;
            }
            //��λ��ַ
            if (txtAddressBusiness.Text.Trim() == "")
            {
                MessageBox.Show("����д���ߵ�λ��ַ��", "��ʾ��");
                this.txtAddressBusiness.Focus();
                return -1;
            }
            //��λ�绰
            if (txtAddressBusiness.Text.Trim() != "-" && txtPhoneBusiness.Text.Trim() == "")
            {
                MessageBox.Show("����д���ߵ�λ�绰��", "��ʾ��");
                this.txtPhoneBusiness.Focus();
                return -1;
            }
            //��λ�ʱ�
            if (txtAddressBusiness.Text.Trim() != "-" && txtBusinessZip.Text.Trim() == "")
            {
                MessageBox.Show("����д���ߵ�λ�ʱ࣡", "��ʾ��");
                this.txtBusinessZip.Focus();
                return -1;
            }
            //��ϵ������
            if (txtKin.Text.Trim() == "")
            {
                MessageBox.Show("����д��ϵ�����ƣ�", "��ʾ��");
                this.txtKin.Focus();
                return -1;
            }
            //�뻼�߹�ϵ
            if (cmbRelation.Tag == null || (cmbRelation.Tag != null && cmbRelation.Tag.ToString() == ""))
            {
                MessageBox.Show("��ѡ���뻼�߹�ϵ��", "��ʾ��");
                this.cmbRelation.Focus();
                return -1;
            }
            //��ϵ�绰
            if (txtLinkmanTel.Text.Trim() == "")
            {
                MessageBox.Show("����д��ϵ�绰��", "��ʾ��");
                this.txtLinkmanTel.Focus();
                return -1;
            }
            //��ϵ��ַ
            if (txtLinkmanAdd.Text.Trim() == "")
            {
                MessageBox.Show("����д��ϵ��ַ��", "��ʾ��");
                this.txtLinkmanAdd.Focus();
                return -1;
            }
            //��Ժ;��
            if (this.cmbInPath.Tag == null || (cmbInPath.Tag != null && cmbInPath.Tag.ToString() == ""))
            {
                MessageBox.Show("��ѡ����Ժ;����", "��ʾ��");
                this.cmbInPath.Focus();
                return -1;
            }
            //��Ժ����
            if (cmbDeptInHospital.Tag == null || (cmbDeptInHospital.Tag != null && cmbDeptInHospital.Tag.ToString() == ""))
            {
                MessageBox.Show("��ѡ����Ժ���ң�", "��ʾ��");
                this.cmbDeptInHospital.Focus();
                return -1;
            }
            //��Ժ���Ҵ���
            if (cmbDeptOutHospital.Tag == null || (cmbDeptOutHospital.Tag != null && cmbDeptOutHospital.Tag.ToString() == ""))
            {
                MessageBox.Show("��ѡ���Ժ���ң�", "��ʾ��");
                this.cmbDeptOutHospital.Focus();
                return -1;
            }
            //��Ժ����
            if (txtInRoom.Text.Trim() == "")
            {
                MessageBox.Show("����д��Ժ���ң�", "��ʾ��");
                this.txtInRoom.Focus();
                return -1;
            }
            //��Ժ����
            if (txtOutRoom.Text.Trim() == "")
            {
                MessageBox.Show("����д��Ժ���ң�", "��ʾ��");
                this.txtOutRoom.Focus();
                return -1;
            }
            //��Ժ����

            //סԺ����
            if (txtPiDays.Text.Trim() == "")
            {
                MessageBox.Show("����дסԺ������", "��ʾ��");
                this.txtPiDays.Focus();
                return -1;
            }

            if (cmbClinicDiagName.Text.Trim() == "")
            {
                MessageBox.Show("����д������ϣ�", "��ʾ��");
                this.cmbClinicDiagName.Focus();
                return -1;
            }
            //������ϱ���
            //�������ҽ�� ID
            if (cmbClinicDiagName.Text.Trim() != "" && (txtClinicDocd.Tag == null || (txtClinicDocd.Tag != null && txtClinicDocd.Tag.ToString() == "")))
            {
                MessageBox.Show("��ѡ���������ҽ����", "��ʾ��");
                this.txtClinicDocd.Focus();
                return -1;
            }
            if (txtFirstDept.Text.Trim() != string.Empty && dtFirstTime.Value.Date < this.dtDateIn.Value.Date)
            {
                MessageBox.Show("��һ��ת��ʱ�䲻��С����Ժʱ��");
                dtFirstTime.Focus();
                return -1;
            }
            if (txtFirstDept.Text.Trim() != string.Empty && dtFirstTime.Value.Date > this.txtDateOut.Value.Date)
            {
                MessageBox.Show("��һ��ת��ʱ�䲻�ܴ����ڳ�Ժʱ��");
                dtFirstTime.Focus();
                return -1;
            }
            if ((dtFirstTime.Value.Date > dtSecondTime.Value.Date) && txtDeptSecond.Text.Trim() != string.Empty)
            {
                MessageBox.Show("��һ��ת��ʱ�䲻�ܴ��ڵڶ���ת��ʱ��");
                dtFirstTime.Focus();
                return -1;
            }
            if ((dtSecondTime.Value.Date > dtThirdTime.Value.Date) && txtDeptThird.Text.Trim() != string.Empty)
            {
                MessageBox.Show("�ڶ���ת��ʱ�䲻�ܴ��ڵ�����ת��ʱ��");
                dtSecondTime.Focus();
                return -1;
            }
            //��������
            if (cmbExampleType.Tag == null || (cmbExampleType.Tag != null && cmbExampleType.Tag.ToString() == ""))
            {
                MessageBox.Show("��ѡ�������ͣ�", "��ʾ��");
                this.cmbExampleType.Focus();
                return -1;
            }
            //�ٴ�·������
            if (cmbClinicPath.Tag == null || (cmbClinicPath.Tag != null && cmbClinicPath.Tag.ToString() == ""))
            {
                MessageBox.Show("��ѡ���ٴ�·��������", "��ʾ��");
                this.cmbClinicPath.Focus();
                return -1;
            }
            //���ȴ���
            if (txtSalvTimes.Text.Trim() == "")
            {
                MessageBox.Show("����д���ȴ�����", "��ʾ��");
                this.txtSalvTimes.Focus();
                return -1;
            }
            //�ɹ�����
            if (txtSuccTimes.Text.Trim() == "")
            {
                MessageBox.Show("����д�ɹ�������", "��ʾ��");
                this.txtSuccTimes.Focus();
                return -1;
            }
            //�ڶ�ҳ����
            //�����ж�ԭ��
            if (txtInjuryOrPoisoningCause.Text.Trim() == "")
            {
                MessageBox.Show("����д�����ж�ԭ��", "��ʾ��");
                this.txtInjuryOrPoisoningCause.Focus();
                return -1;
            }
            //�������
            if (cmbPathologicalDiagName.Text.Trim() == "")
            {
                MessageBox.Show("����д������ϣ�", "��ʾ��");
                this.cmbPathologicalDiagName.Focus();
                return -1;
            }
            //������ϱ���
            if (txtPathologicalDiagCode.Text.Trim() == "��")
            {
                MessageBox.Show("����д������ϣ����û������д��δ���֡���-����", "��ʾ��");
                this.cmbPathologicalDiagName.Focus();
                return -1;
            }
            //�����
            if (cmbPathologicalDiagName.Text.Trim() != "" && txtPathologicalDiagNum.Text.Trim() == "")
            {
                MessageBox.Show("����д����ţ�", "��ʾ��");
                this.txtPathologicalDiagNum.Focus();
                return -1;
            }
            //ҩ�����
            if (cmbIsDrugAllergy.Tag == null || (cmbIsDrugAllergy.Tag != null && cmbIsDrugAllergy.Tag.ToString() == ""))
            {
                MessageBox.Show("����дҩ�������", "��ʾ��");
                this.cmbIsDrugAllergy.Focus();
                return -1;
            }
            ////����ҩ��1
            if (cmbIsDrugAllergy.Tag.ToString() == "2" && txtDrugAllergy.Text.Trim() == "")
            {
                MessageBox.Show("����д����ҩ�", "��ʾ��");
                this.txtDrugAllergy.Focus();
                return -1;
            }
            //��������ʬ��
            if (txtLeaveHopitalType.Text.Trim() == "5" && (cmbDeathPatientBobyCheck.Tag == null || (cmbDeathPatientBobyCheck.Tag != null && cmbDeathPatientBobyCheck.Tag.ToString() == "")))
            {
                MessageBox.Show("��������,��ѡ����������ʬ�죡", "��ʾ��");
                this.cmbDeathPatientBobyCheck.Focus();
                return -1;
            }
            if (cmbDeathPatientBobyCheck.Tag != null && (cmbDeathPatientBobyCheck.Tag.ToString() == "1" || cmbDeathPatientBobyCheck.Tag.ToString() == "2"))
            {
                if (txtLeaveHopitalType.Text.Trim() != "5")
                {
                    MessageBox.Show("�������˲���ѡ��ʬ�죬������ѡ��", "��ʾ��");
                    this.cmbDeathPatientBobyCheck.Focus();
                    return -1;
                }
            }
            //Ѫ�ͱ���
            if (cmbBloodType.Tag == null || (cmbBloodType.Tag != null && cmbBloodType.Tag.ToString() == ""))
            {
                MessageBox.Show("��ѡ��Ѫ�ͣ�", "��ʾ��");
                this.cmbBloodType.Focus();
                return -1;
            }
            //RhѪ��(����)
            if (cmbRhBlood.Tag == null || (cmbRhBlood.Tag != null && cmbRhBlood.Tag.ToString() == ""))
            {
                MessageBox.Show("��ѡ��RhѪ�ͣ�", "��ʾ��");
                this.cmbRhBlood.Focus();
                return -1;
            }
            ////�����δ���
            //if (txtDeptChiefDoc.Tag == null)
            //{
            //    MessageBox.Show("��ѡ������Σ�", "��ʾ��");
            //    this.txtDeptChiefDoc.Focus();
            //    return;
            //}
            ////����ҽʦ����
            //txtConsultingDoctor.Tag = info.PatientInfo.PVisit.ConsultingDoctor.ID;
            ////ConsultingDoctor.Text = info.PatientInfo.PVisit.ConsultingDoctor.Name;
            ////����ҽʦ����
            //txtAttendingDoctor.Tag = info.PatientInfo.PVisit.AttendingDoctor.ID;
            ////AttendingDoctor.Text = info.PatientInfo.PVisit.AttendingDoctor.Name;
            ////סԺҽʦ����
            //txtAdmittingDoctor.Tag = info.PatientInfo.PVisit.AdmittingDoctor.ID;
            //סԺҽʦ����
            //AdmittingDoctor.Text = info.PatientInfo.PVisit.AdmittingDoctor.Name;
            // ���λ�ʿ
            if (cmbDutyNurse.Tag == null || (cmbDutyNurse.Tag != null && cmbDutyNurse.Tag.ToString() == ""))
            {
                MessageBox.Show("��ѡ�����λ�ʿ��", "��ʾ��");
                this.cmbDutyNurse.Focus();
                return -1;
            }
            ////����ҽʦ����
            //txtRefresherDocd.Tag = info.RefresherDoc.ID;
            ////RefresherDocd.Text = info.RefresherDoc.Name;
            ////ʵϰҽʦ����
            //txtPraDocCode.Tag = info.PatientInfo.PVisit.TempDoctor.ID;
            //PraDocCode.Text = info.GraduateDoc.Name;
            ////����Ա
            //txtCodingCode.Tag = info.CodingOper.ID;
            //CodingCode.Text = info.CodingName;
            //��������
            //ArrayList QualList = con.GetList("CASEQUALFORPRI");
            //if (QualList != null && QualList.Count > 0)//ҽ����ӡʱ����Ҫ�ʿ�
            //{

            //}
            //else
            //{
            //if (cmbMrQual.Tag == null || (cmbMrQual.Tag != null && cmbMrQual.Tag.ToString() == ""))
            //{
            //    MessageBox.Show("��ѡ�񲡰�������", "��ʾ��");
            //    this.cmbMrQual.Focus();
            //    return -1;
            //}
            ////�ʿ�ҽʦ����
            //if (cmbMrQual.Tag != null && (cmbQcDocd.Tag == null || (cmbQcDocd.Tag != null && cmbQcDocd.Tag.ToString() == "")))
            //{
            //    MessageBox.Show("��ѡ���ʿ�ҽʦ���룡", "��ʾ��");
            //    this.cmbQcDocd.Focus();
            //    return -1;
            //}
            ////�ʿػ�ʿ����
            //if (cmbMrQual.Tag != null && (cmbQcNucd.Tag == null || (cmbQcNucd.Tag != null && cmbQcNucd.Tag.ToString() == "")))
            //{
            //    MessageBox.Show("��ѡ���ʿػ�ʿ��", "��ʾ��");
            //    this.cmbQcNucd.Focus();
            //    return -1;
            //    }
            //}
            //��Ժ��ʽ
            if (txtLeaveHopitalType.Text.Trim() == "")
            {
                MessageBox.Show("����д��Ժ��ʽ��", "��ʾ��");
                this.txtLeaveHopitalType.Focus();
                return -1;
            }
            //ҽ��תԺ����ҽ�ƻ���
            if (txtLeaveHopitalType.Text.Trim() == "2" && txtHighReceiveHopital.Text.Trim() == "")
            {
                MessageBox.Show("����дҽ��תԺ����ҽ�ƻ�����", "��ʾ��");
                this.txtHighReceiveHopital.Focus();
                return -1;
            }
            //ҽ��ת����
            if (txtLeaveHopitalType.Text.Trim() == "3" && txtLowerReceiveHopital.Text.Trim() == "")
            {
                MessageBox.Show("����дҽ��ת������", "��ʾ��");
                this.txtLowerReceiveHopital.Focus();
                return -1;
            }
            //����
            int savTime = 0;
            int sunTime = 0;
            try
            {
                savTime = FS.FrameWork.Function.NConvert.ToInt32(this.txtSalvTimes.Text.Trim());
                sunTime = FS.FrameWork.Function.NConvert.ToInt32(this.txtSuccTimes.Text.Trim());
            }
            catch
            {
                savTime = 0;
                sunTime = 0;
            }
            if (txtLeaveHopitalType.Text.Trim() == "5" && savTime - sunTime > 1)
            {
                MessageBox.Show("��������ֻ��һ�����Ȳ��ɹ����ᣬ��˶ԣ�", "��ʾ��");
                this.txtSalvTimes.Focus();
                return -1;
            }
            if (txtLeaveHopitalType.Text.Trim() != "5" && sunTime > 0 && savTime - sunTime != 0)
            {
                MessageBox.Show("��Ժ��ʽ���������ߣ����ȴ���Ӧ�������ȳɹ���������˶ԣ�", "��ʾ��");
                this.txtSalvTimes.Focus();
                return -1;
            }
            //��Ժ31������סԺ�ƻ�
            if (this.cmbComeBackInMonth.Tag == null || (cmbComeBackInMonth.Tag != null && cmbComeBackInMonth.Tag.ToString() == ""))
            {
                MessageBox.Show("����д��Ժ31������סԺ�ƻ���", "��ʾ��");
                this.cmbComeBackInMonth.Focus();
                return -1;
            }
            //��Ժ31����סԺĿ��
            if (this.cmbComeBackInMonth.Tag.ToString() == "2" && cmbComeBackPurpose.Text.Trim() == "")
            {
                MessageBox.Show("����д��Ժ31����סԺĿ�ģ�", "��ʾ��");
                this.cmbComeBackPurpose.Focus();
                return -1;
            }
            //­�����˻��߻���ʱ�� -��Ժǰ ��
            if (txtOutComeDay.Text.Trim() == "")
            {
                MessageBox.Show("����д­�����˻��߻���ʱ�� -��Ժǰ �죡", "��ʾ��");
                this.txtOutComeDay.Focus();
                return -1;
            }
            //­�����˻��߻���ʱ�� -��Ժǰ Сʱ
            if (txtOutComeHour.Text.Trim() == "")
            {
                MessageBox.Show("����д­�����˻��߻���ʱ�� -��Ժǰ Сʱ��", "��ʾ��");
                this.txtOutComeHour.Focus();
                return -1;
            }
            //­�����˻��߻���ʱ�� -��Ժǰ ����
            if (txtOutComeMin.Text.Trim() == "")
            {
                MessageBox.Show("����д­�����˻��߻���ʱ�� -��Ժǰ ���ӣ�", "��ʾ��");
                this.txtOutComeMin.Focus();
                return -1;
            }
            //­�����˻��߻���ʱ�� -��Ժ�� ��
            if (txtInComeDay.Text.Trim() == "")
            {
                MessageBox.Show("����д­�����˻��߻���ʱ�� -��Ժ�� �죡", "��ʾ��");
                this.txtInComeDay.Focus();
                return -1;
            }
            //­�����˻��߻���ʱ�� -��Ժ�� Сʱ
            if (txtInComeHour.Text.Trim() == "")
            {
                MessageBox.Show("����д­�����˻��߻���ʱ�� -��Ժ�� Сʱ��", "��ʾ��");
                this.txtInComeHour.Focus();
                return -1;
            }
            //­�����˻��߻���ʱ�� -��Ժ�� ����
            if (txtInComeMin.Text.Trim() == "")
            {
                MessageBox.Show("����д­�����˻��߻���ʱ�� -��Ժ�� ���ӣ�", "��ʾ��");
                this.txtInComeMin.Focus();
                return -1;
            }
            //������Դ ����
            if (txtInAvenue.Tag == null || (txtInAvenue.Tag != null && txtInAvenue.Tag.ToString() == ""))
            {
                MessageBox.Show("��ѡ������Դ��", "��ʾ��");
                this.txtInAvenue.Focus();
                return -1;
            }
            //�������Ժ���� ����
            if (txtCePi.Tag == null || (txtCePi.Tag != null && txtCePi.Tag.ToString() == ""))
            {
                MessageBox.Show("��ѡ���������Ժ���ϣ�", "��ʾ��");
                this.txtCePi.Focus();
                return -1;
            }
            //�ٴ��벡����� ����
            if (this.cmbPathologicalDiagName.Text.Trim() != "" && this.cmbPathologicalDiagName.Text.Trim() != "-")
            {
                if (txtClPa.Tag == null || (txtClPa.Tag != null && txtClPa.Tag.ToString() == ""))
                {
                    MessageBox.Show("��ѡ���ٴ��벡����ϣ�", "��ʾ��");
                    this.txtClPa.Focus();
                    return -1;
                }
            }
            FS.HISFC.BizLogic.HealthRecord.Diagnose DiaMgr = new FS.HISFC.BizLogic.HealthRecord.Diagnose();
            ArrayList al = new ArrayList();
            //al = DiaMgr.QueryCaseDiagnoseForClinic(this.CaseBase.PatientInfo.ID, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC);
            al = DiaMgr.QueryCaseDiagnose(this.CaseBase.PatientInfo.ID, "%", FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC, FS.HISFC.Models.Base.ServiceTypes.I);
            if (al == null || al.Count < 1)
            {
                MessageBox.Show("����д���߳�Ժ�����Ϣ��", "��ʾ��");
                this.tab1.SelectedTab = this.tabPage2;
                return -1;
            }
            bool isHavedMain = false;
            bool isHavedState = false;
            foreach (FS.HISFC.Models.HealthRecord.Diagnose diagNose in al)
            {

                if (diagNose.DiagInfo.DiagType.ID == "1" && !isHavedMain)
                {
                    isHavedMain = true;
                }
                if (diagNose.DiagOutState == "")
                {
                    isHavedState = true;
                }
            }
            if (!isHavedMain)
            {
                MessageBox.Show("��ѡ��һ����Ժ�����Ϊ��Ҫ��ϣ�", "��ʾ��");
                this.tab1.SelectedTab = this.tabPage2;
                return -1;
            }
            if (isHavedState)
            {
                MessageBox.Show("��ѡ���Ժ��ϵ���Ժ���飡", "��ʾ��");
                this.tab1.SelectedTab = this.tabPage2;
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// �����ύ֮ǰ��������ҳȱ����ƣ����Բ����ύʱ����Ҫ����ʾ
        /// </summary>
        public int CommitCheck(string InpatientNo)
        {

            //ʹ�����µ��Ӳ���2012-9-11
            FS.FrameWork.Models.NeuObject NewEMRInfo = con.GetConstant("CASENEWEMR", "1");
            if (NewEMRInfo != null && NewEMRInfo.Memo == "1")
            {
                this.isNewEMR = true;
                //this.InitCountryList();
            }

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
            this.dutyNurse = patientInfo.PVisit.AdmittingNurse.ID;
            this.patient_no = patientInfo.PID.PatientNO;
            patientInfo = new FS.HISFC.Models.RADT.PatientInfo();
            //end add chengym
            CaseBase = baseDml.GetCaseBaseInfo(InpatientNo);//����������Ϣ��met_cas_base��
            if (CaseBase.PatientInfo.Pact.ID == null || (CaseBase.PatientInfo.Pact.ID != null && CaseBase.PatientInfo.Pact.ID.ToString() == ""))
            {
                MessageBox.Show("��ѡ��סԺ��" + patient_no + "��ҽ�Ƹ��ʽ��", "��ʾ��");
                return -1;
            }
            //ҽ�����Ѻ�
            if (CaseBase.PatientInfo.Pact.ID == "1" || CaseBase.PatientInfo.Pact.ID == "2" || CaseBase.PatientInfo.Pact.ID == "3")
            {

                if (CaseBase.PatientInfo.SSN == "")
                {
                    MessageBox.Show("��ѡ��סԺ��" + patient_no + "��ҽ�����Ѻţ�", "��ʾ��");
                    return -1;
                }
            }
            //סԺ����
            if (CaseBase.PatientInfo.InTimes.ToString() == "")
            {
                MessageBox.Show("��ѡ��סԺ��" + patient_no + "��סԺ������", "��ʾ��");
                return -1;
            }
            ////סԺ��  ������
            //if (CaseBase.PatientInfo.PID.CaseNO.ToString() == "")
            //{
            //    MessageBox.Show("��ѡ��סԺ��" + InpatientNo + "��סԺ�š������ţ�", "��ʾ��");
            //    return -1;
            //}
            //����
            if (CaseBase.PatientInfo.Name == "")
            {
                MessageBox.Show("��ѡ��סԺ��" + patient_no + "��������", "��ʾ��");
                return -1;
            }
            //�Ա�
            if (CaseBase.PatientInfo.Sex == null || (CaseBase.PatientInfo.Sex != null && CaseBase.PatientInfo.Sex.ToString() == ""))
            {
                MessageBox.Show("��ѡ��סԺ��" + patient_no + "���Ա�", "��ʾ��");
                return -1;
            }
            //����
            if (CaseBase.PatientInfo.Birthday.ToString() == "")
            {
                MessageBox.Show("��ѡ��סԺ��" + patient_no + "�����գ�", "��ʾ��");
                return -1;
            }
            //���� ����
            if (CaseBase.PatientInfo.Country.ID.ToString() == "")
            {
                MessageBox.Show("��ѡ��סԺ��" + patient_no + "�Ĺ�����", "��ʾ��");
                return -1;
            }
            //���� 
            if (CaseBase.PatientInfo.Nationality.ID.ToString() == "")
            {
                MessageBox.Show("��ѡ��סԺ��" + patient_no + "�����壡", "��ʾ��");
                return -1;
            }
            //��������������
            FS.HISFC.BizLogic.HealthRecord.Baby babyMgr = new FS.HISFC.BizLogic.HealthRecord.Baby();
            ArrayList babyAl = babyMgr.QueryBabyByInpatientNo(this.CaseBase.PatientInfo.ID);
            if (babyAl != null && babyAl.Count > 0)
            {
                int weith = 0;
                try
                {
                    weith = FS.FrameWork.Function.NConvert.ToInt32(CaseBase.BabyBirthWeight);
                }
                catch
                {
                    weith = 0;
                }
                if (weith == 0)
                {
                    if (CaseBase.BabyBirthWeight == "-" || CaseBase.BabyBirthWeight == "")
                    {
                        MessageBox.Show("��������������д��������������,��������Ժ���أ�", "��ʾ��");
                        return -1;
                    }

                }
            }
            //��������Ժ����
            if (CaseBase.BabyInWeight == "")
            {
                MessageBox.Show("����д��������Ժ���أ�", "��ʾ");
                return -1;
            }
            //������
            if (CaseBase.PatientInfo.AreaCode == "")
            {
                MessageBox.Show("��ѡ��סԺ��" + patient_no + "�ĳ����أ�", "��ʾ��");
                return -1;
            }
            //����
            if (CaseBase.PatientInfo.DIST == "")
            {
                MessageBox.Show("��ѡ��סԺ��" + patient_no + "�ļ��ᣡ", "��ʾ��");
                return -1;
            }
            //���֤��
            if (CaseBase.PatientInfo.IDCard == "")
            {
                MessageBox.Show("��ѡ��סԺ��" + patient_no + "�����֤�ţ�", "��ʾ��");
                return -1;
            }
            //ְҵ
            if (CaseBase.PatientInfo.Profession.ID == "")
            {
                MessageBox.Show("��ѡ��סԺ��" + patient_no + "��ְҵ��", "��ʾ��");
                return -1;
            }
            //���� 
            if (CaseBase.PatientInfo.MaritalStatus.ID.ToString() == "")
            {
                MessageBox.Show("��ѡ��סԺ��" + patient_no + "�Ļ�����", "��ʾ��");
                return -1;
            }
            //��סַ
            //ArrayList addrlist = con.GetList("GBXZQYHF");
            //if (cmbCountry.Tag.ToString() == "156")
            //{
            //    bool aaa1 = true;

            //    if (CaseBase.CurrentAddr.IndexOf("��") > 0)
            //    {

            //        for (int i = 0; i < addrlist.Count; i++)
            //        {
            //            if (addrlist[i].ToString() == CaseBase.CurrentAddr.Substring(0, CaseBase.CurrentAddr.IndexOf("��") + 1))
            //            {
            //                if (addrlist[i].ToString() == "��������Ͻ��" || addrlist[i].ToString() == "�������Ͻ��" || addrlist[i].ToString() == "�Ϻ�����Ͻ��" || addrlist[i].ToString() == "��������Ͻ��")
            //                {
            //                    aaa1 = false;
            //                }
            //                DrrHelper.ArrayObject = addrlist;
            //                if (DrrHelper.GetID(addrlist[i].ToString()).Length != 4)
            //                {
            //                    aaa1 = false;
            //                }
            //            }

            //        }
            //    }
            //    else if (CaseBase.CurrentAddr.IndexOf("��") > 0)
            //    {
            //        for (int i = 0; i < addrlist.Count; i++)
            //        {
            //            if (addrlist[i].ToString() == CaseBase.CurrentAddr.Substring(0, CaseBase.CurrentAddr.IndexOf("��") + 1))
            //            {
            //                DrrHelper.ArrayObject = addrlist;
            //                if (DrrHelper.GetID(addrlist[i].ToString()).Length != 4)
            //                {
            //                    aaa1 = false;
            //                }
            //            }
            //        }
            //    }
            //    else if (CaseBase.CurrentAddr.IndexOf("��") > 0)
            //    {
            //        for (int i = 0; i < addrlist.Count; i++)
            //        {
            //            if (addrlist[i].ToString() == CaseBase.CurrentAddr.Substring(0, CaseBase.CurrentAddr.IndexOf("��") + 1))
            //            {
            //                DrrHelper.ArrayObject = addrlist;
            //                if (DrrHelper.GetID(addrlist[i].ToString()).Length != 4)
            //                {
            //                    aaa1 = false;
            //                }
            //            }
            //        }
            //    }
            //    else if (CaseBase.CurrentAddr.IndexOf("ʡ") > 0 && CaseBase.CurrentAddr.IndexOf("��") > 0)
            //    {
            //        for (int i = 0; i < addrlist.Count; i++)
            //        {
            //            if (addrlist[i].ToString() == CaseBase.CurrentAddr.Substring(0, CaseBase.CurrentAddr.IndexOf("��") + 1))
            //            {
            //                DrrHelper.ArrayObject = addrlist;
            //                if (DrrHelper.GetID(addrlist[i].ToString()).Length != 4)
            //                {
            //                    aaa1 = false;
            //                }
            //            }
            //        }
            //    }


            //    else
            //    {
            //        MessageBox.Show("�밴��ʡ-��-��(��)����д������סַ��", "��ʾ��");
            //        return -1;
            //    }
            //    if (aaa1)
            //    {
            //        MessageBox.Show("�밴��ʡ-��-��(��)����д������סַ��", "��ʾ��");
            //        return -1;
            //    }
            //}
            //if (CaseBase.CurrentAddr == "" || CaseBase.CurrentAddr == "��" || CaseBase.CurrentAddr == "δ�ṩ")
            //{
            //    MessageBox.Show("��ѡ��סԺ��" + InpatientNo + "����ϸ��סַ�����û�ṩ����д���š�-����", "��ʾ��");
            //    return -1;
            //}
            //��סַ�绰 //��ͥ�绰
            if (CaseBase.CurrentAddr.ToString() != "-" && CaseBase.CurrentPhone == "")
            {
                MessageBox.Show("��ѡ��סԺ��" + patient_no + "����סַ�绰�����û�ṩ����д���š�-����", "��ʾ��");
                return -1;
            }
            //��סַ�ʱ�
            if (CaseBase.CurrentAddr.ToString() != "-" && CaseBase.CurrentZip == "")
            {
                MessageBox.Show("��ѡ��סԺ��" + patient_no + "����סַ�ʱ࣬���û�ṩ����д���š�-����", "��ʾ��");
                return -1;
            }
            //��ͥסַ
            //if (CaseBase.PatientInfo.AddressHome == "")
            //{
            //    MessageBox.Show("��ѡ��סԺ��" + InpatientNo + "�ļ�ͥסַ�����û�ṩ����д���š�-����", "��ʾ��");
            //    return -1;
            //}
            //if (cmbCountry.Tag.ToString() == "156")
            //{
            //    bool bbb1 = true;

            //    if (CaseBase.PatientInfo.AddressHome.IndexOf("��") > 0)
            //    {

            //        for (int i = 0; i < addrlist.Count; i++)
            //        {
            //            if (addrlist[i].ToString() == CaseBase.PatientInfo.AddressHome.Substring(0, CaseBase.PatientInfo.AddressHome.IndexOf("��") + 1))
            //            {
            //                if (addrlist[i].ToString() == "��������Ͻ��" || addrlist[i].ToString() == "�������Ͻ��" || addrlist[i].ToString() == "�Ϻ�����Ͻ��" || addrlist[i].ToString() == "��������Ͻ��")
            //                {
            //                    bbb1 = false;
            //                }
            //                DrrHelper.ArrayObject = addrlist;
            //                if (DrrHelper.GetID(addrlist[i].ToString()).Length != 4)
            //                {
            //                    bbb1 = false;
            //                }
            //            }

            //        }
            //    }
            //    else if (CaseBase.PatientInfo.AddressHome.IndexOf("��") > 0)
            //    {
            //        for (int i = 0; i < addrlist.Count; i++)
            //        {
            //            if (addrlist[i].ToString() == CaseBase.PatientInfo.AddressHome.Substring(0, CaseBase.PatientInfo.AddressHome.IndexOf("��") + 1))
            //            {
            //                DrrHelper.ArrayObject = addrlist;
            //                if (DrrHelper.GetID(addrlist[i].ToString()).Length != 4)
            //                {
            //                    bbb1 = false;
            //                }
            //            }
            //        }
            //    }
            //    else if (CaseBase.PatientInfo.AddressHome.IndexOf("��") > 0)
            //    {
            //        for (int i = 0; i < addrlist.Count; i++)
            //        {
            //            if (addrlist[i].ToString() == CaseBase.PatientInfo.AddressHome.Substring(0, CaseBase.PatientInfo.AddressHome.IndexOf("��") + 1))
            //            {
            //                DrrHelper.ArrayObject = addrlist;
            //                if (DrrHelper.GetID(addrlist[i].ToString()).Length != 4)
            //                {
            //                    bbb1 = false;
            //                }
            //            }
            //        }
            //    }
            //    else if (CaseBase.PatientInfo.AddressHome.IndexOf("ʡ") > 0 && CaseBase.PatientInfo.AddressHome.IndexOf("��") > 0)
            //    {
            //        for (int i = 0; i < addrlist.Count; i++)
            //        {
            //            if (addrlist[i].ToString() == CaseBase.PatientInfo.AddressHome.Substring(0, CaseBase.PatientInfo.AddressHome.IndexOf("��") + 1))
            //            {
            //                DrrHelper.ArrayObject = addrlist;
            //                if (DrrHelper.GetID(addrlist[i].ToString()).Length != 4)
            //                {
            //                    bbb1 = false;
            //                }
            //            }
            //        }
            //    }


            //    else
            //    {
            //        MessageBox.Show("�밴��ʡ-��-��(��)����д���߻��ڵ�ַ��", "��ʾ��");
            //        return -1;
            //    }
            //    if (bbb1)
            //    {
            //        MessageBox.Show("�밴��ʡ-��-��(��)����д���߻��ڵ�ַ��", "��ʾ��");
            //        return -1;
            //    }
            //}

            //סַ�ʱ�
            if (CaseBase.PatientInfo.AddressHome != "-" && CaseBase.PatientInfo.HomeZip == "")
            {
                MessageBox.Show("��ѡ��סԺ��" + patient_no + "�ļ�ͥסַ�ʱ࣬���û�ṩ����д���š�-����", "��ʾ��");
                return -1;
            }
            //��λ��ַ
            if (CaseBase.PatientInfo.AddressBusiness == "")
            {
                MessageBox.Show("��ѡ��סԺ��" + patient_no + "�ĵ�λ��ַ�����û�ṩ����д���š�-����", "��ʾ��");
                return -1;
            }
            //��λ�绰
            if (CaseBase.PatientInfo.AddressBusiness != "-" && CaseBase.PatientInfo.PhoneBusiness == "")
            {
                MessageBox.Show("��ѡ��סԺ��" + patient_no + "�ĵ�λ�绰�����û�ṩ����д���š�-����", "��ʾ��");
                return -1;
            }
            //��λ�ʱ�
            if (CaseBase.PatientInfo.AddressBusiness != "-" && CaseBase.PatientInfo.BusinessZip == "")
            {
                MessageBox.Show("��ѡ��סԺ��" + patient_no + "�ĵ�λ�ʱ࣬���û�ṩ����д���š�-����", "��ʾ��");
                return -1;
            }
            //��ϵ������
            if (CaseBase.PatientInfo.Kin.Name == "")
            {
                MessageBox.Show("��ѡ��סԺ��" + patient_no + "����ϵ�����ƣ����û�ṩ����д���š�-����", "��ʾ��");
                return -1;
            }
            //�뻼�߹�ϵ
            if (CaseBase.PatientInfo.Kin.RelationLink.ToString() == "")
            {
                MessageBox.Show("��ѡ��סԺ��" + patient_no + "�Ļ��߹�ϵ��", "��ʾ��");
                return -1;
            }
            //��ϵ�绰
            if (CaseBase.PatientInfo.Kin.RelationPhone == "")
            {
                MessageBox.Show("��ѡ��סԺ��" + patient_no + "����ϵ�绰�����û�ṩ����д���š�-����", "��ʾ��");
                return -1;
            }
            //��ϵ��ַ
            if (CaseBase.PatientInfo.Kin.RelationAddress == "")
            {
                MessageBox.Show("��ѡ��סԺ��" + patient_no + "����ϵ��ַ�����û�ṩ����д���š�-����", "��ʾ��");
                return -1;
            }
            //��Ժ;��
            if (CaseBase.InPath == "")
            {
                MessageBox.Show("��ѡ��סԺ��" + patient_no + "����Ժ;����", "��ʾ��");
                return -1;
            }
            //��Ժ����
            if (CaseBase.InDept.Name == "")
            {
                MessageBox.Show("��ѡ��סԺ��" + patient_no + "����Ժ���ң�", "��ʾ��");
                return -1;
            }
            //��Ժ���Ҵ���
            if (CaseBase.OutDept.Name == "")
            {
                MessageBox.Show("��ѡ��סԺ��" + patient_no + "�ĳ�Ժ���ң�", "��ʾ��");
                return -1;
            }
            //��Ժ����
            if (CaseBase.InRoom == "")
            {
                MessageBox.Show("��ѡ��סԺ��" + patient_no + "����Ժ���ң�", "��ʾ��");
                return -1;
            }
            //��Ժ����
            if (CaseBase.OutRoom == "")
            {
                MessageBox.Show("��ѡ��סԺ��" + patient_no + "�ĳ�Ժ���ң�", "��ʾ��");
                return -1;
            }
            //�������
            if (CaseBase.ClinicDiag.Name == "")
            {
                MessageBox.Show("��ѡ��סԺ��" + patient_no + "��������ϣ�", "��ʾ��");
                return -1;
            }
            //�������ҽ�� ID
            if (CaseBase.ClinicDiag.Name != "" && CaseBase.ClinicDoc.Name == "")
            {
                MessageBox.Show("��ѡ��סԺ��" + patient_no + "���������ҽ����", "��ʾ��");
                return -1;
            }
            //ת��ʱ��û�п���
            //��������
            if (CaseBase.ExampleType == "")
            {
                MessageBox.Show("��ѡ��סԺ��" + patient_no + "�Ĳ������ͣ�", "��ʾ��");
                return -1;
            }
            //�ٴ�·������
            if (CaseBase.ClinicPath == "")
            {
                MessageBox.Show("��ѡ��סԺ��" + patient_no + "���ٴ�·��������", "��ʾ��");
                return -1;
            }
            //���ȴ���
            if (CaseBase.SalvTimes.ToString() == "")
            {
                MessageBox.Show("��ѡ��סԺ��" + patient_no + "�����ȴ�����", "��ʾ��");
                return -1;
            }
            //�ɹ�����
            if (CaseBase.SuccTimes.ToString() == "")
            {
                MessageBox.Show("��ѡ��סԺ��" + patient_no + "�ĳɹ�������", "��ʾ��");
                return -1;
            }
            //�ڶ�ҳ����
            //�����ж�ԭ��
            if (CaseBase.InjuryOrPoisoningCause == "")
            {
                MessageBox.Show("��ѡ��סԺ��" + patient_no + "�������ж�ԭ�����û�ṩ����д���š�-����", "��ʾ��");
                return -1;
            }
            //�������
            if (CaseBase.PathologicalDiagName == "")
            {
                MessageBox.Show("��ѡ��סԺ��" + patient_no + "�Ĳ�����ϣ����û�ṩ����д���š�-����", "��ʾ��");
                return -1;
            }
            //�����
            if (CaseBase.PathologicalDiagName != "" && CaseBase.PathNum == "")
            {
                MessageBox.Show("��ѡ��סԺ��" + patient_no + "�Ĳ���ţ�", "��ʾ��");
                return -1;
            }
            //ҩ�����
            if (CaseBase.AnaphyFlag == "")
            {
                MessageBox.Show("��ѡ��סԺ��" + patient_no + "��ҩ�������", "��ʾ��");
                return -1;
            }
            ////����ҩ��1
            if (CaseBase.AnaphyFlag == "2" && CaseBase.FirstAnaphyPharmacy.ID.ToString() == "")
            {
                MessageBox.Show("��ѡ��סԺ��" + patient_no + "�Ĺ���ҩ�", "��ʾ��");
                return -1;
            }
            //��������ʬ��
            if (CaseBase.Out_Type == "5" && CaseBase.CadaverCheck == "")
            {
                MessageBox.Show("��ѡ��סԺ��" + patient_no + "����������ʬ�죡", "��ʾ��");
                return -1;
            }
            if (CaseBase.CadaverCheck != "" && (CaseBase.CadaverCheck == "1" || CaseBase.CadaverCheck == "2"))
            {
                if (CaseBase.Out_Type != "5")
                {
                    MessageBox.Show("�������˲���ѡ��ʬ�죬������ѡ��", "��ʾ��");
                    return -1;
                }
            }
            //Ѫ�ͱ���
            if (CaseBase.PatientInfo.BloodType.ToString() == "")
            {
                MessageBox.Show("��ѡ��סԺ��" + patient_no + "��Ѫ�ͣ�", "��ʾ��");
                return -1;
            }
            //RhѪ��(����)
            if (CaseBase.RhBlood == "")
            {
                MessageBox.Show("��ѡ��סԺ��" + patient_no + "��RhѪ�ͣ�", "��ʾ��");
                return -1;
            }

            // ���λ�ʿ
            if (CaseBase.DutyNurse.ToString() == "")
            {
                MessageBox.Show("��ѡ��סԺ��" + patient_no + "�����λ�ʿ��", "��ʾ��");
                return -1;
            }
            //��Ժ��ʽ
            if (CaseBase.Out_Type == "")
            {
                MessageBox.Show("��ѡ��סԺ��" + patient_no + "����Ժ��ʽ��", "��ʾ��");
                return -1;
            }
            //ҽ��תԺ����ҽ�ƻ���
            if (CaseBase.Out_Type == "2" && CaseBase.HighReceiveHopital == "")
            {
                MessageBox.Show("��ѡ��סԺ��" + patient_no + "��ҽ��תԺ����ҽ�ƻ�����", "��ʾ��");
                return -1;
            }
            //ҽ��ת����
            if (CaseBase.Out_Type == "3" && CaseBase.LowerReceiveHopital == "")
            {
                MessageBox.Show("��ѡ��סԺ��" + patient_no + "��ҽ��ת������", "��ʾ��");
                return -1;
            }
            //����
            int savTime = 0;
            int sunTime = 0;
            try
            {
                savTime = FS.FrameWork.Function.NConvert.ToInt32(CaseBase.SalvTimes);
                sunTime = FS.FrameWork.Function.NConvert.ToInt32(CaseBase.SuccTimes);
            }
            catch
            {
                savTime = 0;
                sunTime = 0;
            }
            if (CaseBase.Out_Type == "5" && savTime - sunTime > 1)
            {
                MessageBox.Show("��������ֻ��һ�����Ȳ��ɹ����ᣬ��˶ԣ�", "��ʾ��");
                return -1;
            }
            if (CaseBase.Out_Type != "5" && sunTime > 0 && (savTime - sunTime != 0))
            {
                MessageBox.Show("��Ժ��ʽ���������ߣ����ȴ���Ӧ�������ȳɹ���������˶ԣ�", "��ʾ��");
                return -1;
            }
            //��Ժ31������סԺ�ƻ�
            if (CaseBase.ComeBackInMonth == "")
            {
                MessageBox.Show("��ѡ��סԺ��" + patient_no + "�ĳ�Ժ31������סԺ�ƻ���", "��ʾ��");
                return -1;
            }
            //��Ժ31����סԺĿ��
            if (CaseBase.ComeBackInMonth == "2" && CaseBase.ComeBackPurpose == "")
            {
                MessageBox.Show("��ѡ��סԺ��" + patient_no + "�ĳ�Ժ31������סԺĿ�ģ�", "��ʾ��");
                return -1;
            }
            //­�����˻��߻���ʱ�� -��Ժǰ ��
            if (CaseBase.OutComeDay.ToString() == "")
            {
                MessageBox.Show("��ѡ��סԺ��" + patient_no + "��­�����˻��߻���ʱ�� -��Ժǰ �죡", "��ʾ��");
                return -1;
            }
            //­�����˻��߻���ʱ�� -��Ժǰ Сʱ
            if (CaseBase.OutComeHour.ToString() == "")
            {
                MessageBox.Show("��ѡ��סԺ��" + patient_no + "��­�����˻��߻���ʱ�� -��Ժǰ Сʱ��", "��ʾ��");
                return -1;
            }
            //­�����˻��߻���ʱ�� -��Ժǰ ����
            if (CaseBase.OutComeMin.ToString() == "")
            {
                MessageBox.Show("��ѡ��סԺ��" + patient_no + "��­�����˻��߻���ʱ�� -��Ժǰ ���ӣ�", "��ʾ��");
                return -1;
            }
            //������Դ ����
            if (CaseBase.InAvenue == "")
            {
                MessageBox.Show("��ѡ��סԺ��" + patient_no + "�Ĳ�����Դ��", "��ʾ��");
                return -1;
            }
            //�������Ժ���� ����
            if (CaseBase.CePi == "")
            {
                MessageBox.Show("��ѡ��סԺ��" + patient_no + "���������Ժ���ϣ�", "��ʾ��");
                return -1;
            }
            //�ٴ��벡����� ����
            if (CaseBase.PathologicalDiagName != "" && CaseBase.PathologicalDiagName != "-")
            {
                if (CaseBase.ClPa == "")
                {
                    MessageBox.Show("��ѡ��סԺ��" + patient_no + "���ٴ��벡����ϣ�", "��ʾ��");
                    return -1;
                }
            }
            FS.HISFC.BizLogic.HealthRecord.Diagnose DiaMgr = new FS.HISFC.BizLogic.HealthRecord.Diagnose();
            ArrayList al = new ArrayList();
            //al = DiaMgr.QueryCaseDiagnoseForClinic(this.CaseBase.PatientInfo.ID, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC);
            al = DiaMgr.QueryCaseDiagnose(this.CaseBase.PatientInfo.ID, "%", FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC, FS.HISFC.Models.Base.ServiceTypes.I);
            if (al == null || al.Count < 1)
            {
                MessageBox.Show("����дסԺ��" + patient_no + "�Ļ��߳�Ժ�����Ϣ��", "��ʾ��");
                this.tab1.SelectedTab = this.tabPage2;
                return -1;
            }
            bool isHavedMain = false;
            bool isHavedState = false;
            foreach (FS.HISFC.Models.HealthRecord.Diagnose diagNose in al)
            {

                if (diagNose.DiagInfo.DiagType.ID == "1" && !isHavedMain)
                {
                    isHavedMain = true;
                }
                if (diagNose.DiagOutState == "")
                {
                    isHavedState = true;
                }
            }
            if (!isHavedMain)
            {
                MessageBox.Show("��ѡ��סԺ��" + patient_no + "��һ����Ժ�����Ϊ��Ҫ��ϣ�", "��ʾ��");
                return -1;
            }
            if (isHavedState)
            {
                MessageBox.Show("��ѡ��סԺ��" + patient_no + "�ĳ�Ժ��ϵ���Ժ���飡", "��ʾ��");
                return -1;
            }
            return 1;

        }
        /// <summary>
        /// �ڶ�ҳ��ӡǰ����
        /// </summary>
        /// <returns></returns>
        private int PrintSecCheck()
        {
            //�ӳ������л�ȡ�Ƿ���Ҫ��������ѡ��
            ArrayList list = con.GetList("CASEHAVEDOPS");
            if (list != null && list.Count > 0)
            {
                if (this.CaseBase.Ever_Sickintodeath == "")
                {
                    MessageBox.Show("��ѡ���Ƿ����������Ϣ��", "��ʾ��");
                    this.tab1.SelectedTab = this.tabPage3;
                    return -1;
                }
            }
            FS.HISFC.BizLogic.HealthRecord.Operation operationManager = new FS.HISFC.BizLogic.HealthRecord.Operation();
            ArrayList alOpr = operationManager.QueryOperationByInpatientNo(this.CaseBase.PatientInfo.ID);
       
            ArrayList alOPr = operationManager.QueryOperationByInpatientNo1(this.CaseBase.PatientInfo.ID);
            if (alOPr != null)
            {
                foreach (FS.HISFC.Models.HealthRecord.OperationDetail operationInfo in alOpr)
                {
                    
                    //if (operationInfo.MarcKind.ToString() == "")
                    //{
                    //    MessageBox.Show("��ѡ��������Ϣ������ʽ��", "��ʾ��");
                    //    return -1;
                    //}
                    if (operationInfo.NickKind.ToString() == "")
                    {
                        MessageBox.Show("��ѡ��������Ϣ���п����࣬����ǲ���������д01��0��0�����пڣ��ɲ����������࣡", "��ʾ��");
                        return -1;
                    }
                    if (operationInfo.NickKind.ToString() !="01"&&operationInfo.NickKind.ToString() !="02"&&operationInfo.NickKind.ToString() !="03"&&operationInfo.CicaKind.ToString() == "")
                    {
                        MessageBox.Show("��ѡ��������Ϣ���������࣡", "��ʾ��");
                        return -1;
                    }
                    //if (operationInfo.NarcDoctInfo.Name.ToString() == "")
                    //{
                    //    MessageBox.Show("��ѡ��������Ϣ������ҽʦ��", "��ʾ��");
                    //    return -1;
                    //}
                }
            }
          

            bool isBigOutDate = false;
            bool isMinInDate = false;
            if (alOpr != null && alOpr.Count > 0)
            {
                foreach (FS.HISFC.Models.HealthRecord.OperationDetail operationInfo in alOpr)
                {
                    if (this.CaseBase.PatientInfo.PVisit.OutTime.Date < operationInfo.OperationDate.Date)
                    {
                        isBigOutDate = true;
                        break;
                    }
                    if (this.CaseBase.PatientInfo.PVisit.InTime.Date > operationInfo.OperationDate.Date)
                    {
                        isMinInDate = true;
                        break;
                    }
                }
            }
            if (isBigOutDate)
            {
                MessageBox.Show("��������Ӧ��<=��Ժ���ڡ�" + this.CaseBase.PatientInfo.PVisit.OutTime.ToShortDateString() + "����", "��ʾ��");
                this.tab1.SelectedTab = this.tabPage3;
                return -1;
            }
            if (isMinInDate)
            {
                MessageBox.Show("��������Ӧ��>=��Ժ���ڡ�" + this.CaseBase.PatientInfo.PVisit.InTime.ToShortDateString() + "����", "��ʾ��");
                this.tab1.SelectedTab = this.tabPage3;
                return -1;
            }
            FS.HISFC.BizLogic.HealthRecord.Baby baby = new FS.HISFC.BizLogic.HealthRecord.Baby();
            //��ѯ��������������
            ArrayList babyList = baby.QueryBabyByInpatientNo(this.CaseBase.PatientInfo.ID);
            bool isBrithEnd = false;
            if (babyList != null && babyList.Count > 0)
            {
                foreach (FS.HISFC.Models.HealthRecord.Baby babyInfo in babyList)
                {
                    if (babyInfo.BabyState == "3" && babyInfo.BirthEnd != "1")
                    {
                        isBrithEnd = true;
                        break;
                    }
                    if (babyInfo.BabyState == "2" && babyInfo.BirthEnd != "1")
                    {
                        isBrithEnd = true;
                        break;
                    }
                }
            }
            if (isBrithEnd)
            {
                MessageBox.Show("��Ӥ�������������ǡ���Ժ����ת�ơ��ģ������������ֻ��ѡ�񡰻�������������޸ģ�", "��ʾ��");
                this.tab1.SelectedTab = this.tabPage4;
                return -1;
            }
            //�ӳ������л�ȡ�Ƿ���Ҫ�ж����޸�Ӥ����Ϣ
            ArrayList listBaby = con.GetList("CASEHAVEDBABY");
            if (listBaby != null && listBaby.Count > 0)
            {
                if (this.CaseBase.Ever_Firstaid == "")
                {
                    MessageBox.Show("��ѡ���Ƿ���ڸ�Ӥ����Ϣ��", "��ʾ��");
                    this.tab1.SelectedTab = this.tabPage4;
                    return -1;
                }
            }
            //�ӳ������л�ȡ�Ƿ���Ҫ�ж�������������Ϣ
            ArrayList listTum = con.GetList("CASEHAVEDTUM");
            if (listTum != null && listTum.Count > 0)
            {
                if (this.CaseBase.Ever_Difficulty == "")
                {
                    MessageBox.Show("��ѡ���Ƿ������������Ϣ��", "��ʾ��");
                    this.tab1.SelectedTab = this.tabPage5;
                    return -1;
                }
            }
            return 1;
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
            return 1;
        }
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

        //�ж����֤���Ƿ�Ϸ�
        #region �ж����֤��
        private int ProcessIDENNO(string idNO)
        {
            try
            {
                string errText = string.Empty;

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
                    this.txtIDNo.Focus();
                    return -1;
                }
                string[] reurnString = errText.Split(',');

                //if (this.dtPatientBirthday.Text != reurnString[1])
                //{
                //    MessageBox.Show(FS.FrameWork.Management.Language.Msg("������������������֤�����е����ղ���"));
                //    this.dtPatientBirthday.Focus();
                //    return -1;
                //}

                if (this.cmbPatientSex.Text != reurnString[2])
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("������Ա������֤�кŵ��Ա𲻷�"));
                    this.cmbPatientSex.Focus();
                    return -1;
                }

            }
            catch
            {
            }
            return 1;
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


    }


}
