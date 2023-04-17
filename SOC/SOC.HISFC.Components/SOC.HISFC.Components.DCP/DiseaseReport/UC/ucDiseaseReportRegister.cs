using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;



namespace Neusoft.UFC.DCP.DiseaseReport
{

    /// <summary>
    /// ucDiseaseReportRegister<br></br>
    /// [��������: ����uc]<br></br>
    /// [�� �� ��: ]<br></br>
    /// [����ʱ��: 2008-8-20]<br></br>
    /// <ҵ��˵�� 
    ///		1���������ĸ�����Ʋ�����Ϊȷ�ﲡ��ʱ��ԭ���Ŀ����ж������û������ж��Ƿ�������updateԭ����copyԭ�������û��޸ĺ�insert
    ///                      �Զ���������չ��Ϣ2��¼ԭ����ţ�ԭ������չ��Ϣ3��¼��������ţ�Ҳ�ɲ���¼��
    ///  />
    /// </summary>
    public partial class ucDiseaseReportRegister : Neusoft.FrameWork.WinForms.Controls.ucBaseControl,Neusoft.FrameWork.WinForms.Classes.IPreArrange
    {

        #region ������

        /// <summary>
        /// ���Ұ�����
        /// </summary>
        private Neusoft.FrameWork.Public.ObjectHelper deptHelper = new Neusoft.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// Ա��
        /// </summary>
        private Neusoft.FrameWork.Public.ObjectHelper employHelper = new Neusoft.FrameWork.Public.ObjectHelper();

        #endregion

        #region ����
      
        /// <summary>
        /// ���߿���
        /// </summary>
        public Neusoft.FrameWork.Models.NeuObject PatientDept = new Neusoft.FrameWork.Models.NeuObject();       

        /// <summary>
        /// סԺ���߹�����
        /// </summary>
        private Neusoft.HISFC.DCP.BizProcess.Patient patientProcess = new Neusoft.HISFC.DCP.BizProcess.Patient();

        /// <summary>
        /// ��Ⱦ��������
        /// </summary>
        private Neusoft.HISFC.DCP.BizLogic.DiseaseReport diseaseMgr = new Neusoft.HISFC.DCP.BizLogic.DiseaseReport();        

        /// <summary>
        /// ��ʼʱ��
        /// </summary>
        private string BeginTime = "";

        /// <summary>
        /// ����ʱ��
        /// </summary>
        private string EndTime = "";

        /// <summary>
        /// ��Աʵ��
        /// </summary>
        private Neusoft.HISFC.Models.Base.Employee User = new Neusoft.HISFC.Models.Base.Employee();        

        /// <summary>
        /// ���ߵ�סԺ��
        /// </summary>
        //private string InpatientNO = "";
       
        /// <summary>
        /// ���漯��
        /// </summary>
        private ArrayList alReport = new ArrayList();        

        /// <summary>
        /// ����������
        /// </summary>
        private Neusoft.HISFC.DCP.BizProcess.Common commonProcess = new Neusoft.HISFC.DCP.BizProcess.Common();

        /// <summary>
        /// ToolBar������
        /// </summary>
        private Neusoft.FrameWork.WinForms.Forms.ToolBarService toolBarService = new Neusoft.FrameWork.WinForms.Forms.ToolBarService();

        /// <summary>
        /// ��������
        /// </summary>
        private OperType operType;

        /// <summary>
        /// ���д�Ⱦ�������ڵ�������
        /// </summary>
        private ArrayList alInfectItem = new ArrayList();

        /// <summary>
        /// ���д�Ⱦ������������
        /// </summary>
        private ArrayList alinfection = new ArrayList();

        /// <summary>
        /// ��Ҫ�����ļ���
        /// </summary>
        private System.Collections.Hashtable hshNeedAdd;

        /// <summary>
        /// ��Ⱦ��������[���ұ���]�����ѡ��Ⱦ��ʱ�Ƿ�ѡ��������
        /// </summary>
        private System.Collections.Hashtable hshInfectClass;

        /// <summary>
        /// ��Ҫ�����Բ����ļ���
        /// </summary>
        private System.Collections.Hashtable hshNeedSexReport;

        /// <summary>
        /// ��Ҫ��Ѫ�ͼ�
        /// </summary>
        private System.Collections.Hashtable hshNeedCheckedBlood;

        /// <summary>
        /// ��Ҫ������������
        /// </summary>
        private System.Collections.Hashtable hshNeedCaseTwo;

        /// <summary>
        /// ��Ҫ�绰����ļ���
        /// </summary>
        private System.Collections.Hashtable hshNeedTelInfect;

        /// <summary>
        /// ��Ҫ��˲�ת�ﵥ�ļ���
        /// </summary>
        private System.Collections.Hashtable hshNeedBill;

        /// <summary>
        /// ��Ҫ��ע�ļ���
        /// </summary>
        private System.Collections.Hashtable hshNeedMemo;

        /// <summary>
        /// ���������˷�
        /// </summary>
        private System.Collections.Hashtable hshLitteChild;

        /// <summary>
        /// ����ְҵΪѧ��[Ӧ��ʾ��дѧУ����֮��]
        /// </summary>
        private System.Collections.Hashtable hshStudent;

        /// <summary>
        /// ��Ҫ�������Ƶ��Բ�
        /// </summary>
        private System.Collections.Hashtable hshSexNeedGradeTwo;

        /// <summary>
        /// ��Ҫ��������Ⱥ����
        /// </summary>
        private System.Collections.Hashtable hshPatientTyepNeedDesc;

        /// <summary>
        /// Ȩ�޿���
        /// </summary>
        private Neusoft.FrameWork.Models.NeuObject myPriDept = new Neusoft.FrameWork.Models.NeuObject();        

        /// <summary>
        /// ��ַ��Ϣ�Ƿ��סԺ�����м���
        /// </summary>
        bool isAddressLoad = false;              

        /// <summary>
        /// �����ӿ�
        /// </summary>
        private Neusoft.HISFC.DCP.Interface.IAddition iAdditionReport;
        
        /// <summary>
        /// ���ߴ�ѡ���б���ʾ
        /// </summary>
        ListBox dataShowList = null;

        /// <summary>
        /// �Ƿ�Ԥ��Ԥ������Ȩ��
        /// </summary>
        private bool isCDCPriv = false;

        #region Ȩ����ر���

        /// <summary>
        /// ����Ȩ����ؿ���
        /// </summary>
        List<Neusoft.FrameWork.Models.NeuObject> cdcPrivDeptList = new List<Neusoft.FrameWork.Models.NeuObject>();

        #endregion

        private string userPriv = "0";

        #endregion

        #region ����

        /// <summary>
        /// ���濨[�ڱ��������ʱ����show֮ǰ��ֵ]
        /// </summary>
        public ArrayList AlReport
        {
            get
            {
                return alReport;
            }
            set
            {
                alReport = value;
            }
        }

        /// <summary>
        /// ��¼��Ա����״̬
        /// </summary>
        [Description("��¼��Ա����״̬"), Category("����"), DefaultValue("0")]
        public string UserPriv
        {
            get
            {
                return this.userPriv;
            }
            set
            {
                this.userPriv = value;
            }
        }


        /// <summary>
        /// ��ѯ��������
        /// </summary>
        private Neusoft.HISFC.DCP.Enum.PatientType llbPatientType;

        /// <summary>
        /// ��ѯ��������
        /// </summary>
        public Neusoft.HISFC.DCP.Enum.PatientType LlbPatientType
        {
            get
            {
                return llbPatientType;
            }
            set
            {
                llbPatientType = value;
            }
        }

        /// <summary>
        /// ����ʵ��
        /// </summary>
        private Neusoft.HISFC.Models.RADT.Patient patient = new Neusoft.HISFC.Models.RADT.Patient();

        /// <summary>
        /// ������Ϣ
        /// </summary>
        public Neusoft.HISFC.Models.RADT.Patient Patient
        {
            get
            {
                return patient;
            }
            set
            {
                this.ClearAll(true);
                patient = value;
            }
        }

        ///// <summary>
        ///// ���߲�����
        ///// </summary>
        //private string clinicNo = "";       

        ///// <summary>
        ///// ����Patient.ID��
        ///// </summary>
        //public string ClinicNo
        //{
        //    get
        //    {
        //        return clinicNo;
        //    }
        //    set
        //    {
        //        if (value != null && value != "")
        //        {
        //            clinicNo = value;
        //        }
        //    }
        //}

        /// <summary>
        /// ����
        /// </summary>
        private Neusoft.HISFC.DCP.Enum.PatientType type = Neusoft.HISFC.DCP.Enum.PatientType.O;

        /// <summary>
        /// ����[IסԺ C���� O����]
        /// </summary>
        public Neusoft.HISFC.DCP.Enum.PatientType PatientType
        {
            get
            {
                return type;
            }
            set
            {
                type = value;

                if (value == Neusoft.HISFC.DCP.Enum.PatientType.O)
                {
                    this.txtClinicNO.ReadOnly = true;
                }
                else
                {
                    this.txtClinicNO.ReadOnly = false;
                }
            }
        }

        /// <summary>
        /// ״̬ �����б�ΪA �����б�ΪB ѡ���˱�������Ϊ����״̬
        /// </summary>
        public enumTreeViewType treeViewType;

        /// <summary>
        /// ״̬ �����б�ΪA �����б�ΪB ѡ���˱�������Ϊ����״̬
        /// </summary>
        public enumTreeViewType TreeViewType
        {
            get
            {
                return treeViewType;
            }
            set
            {
                treeViewType = value;
                //this.ucReportRegister1.State = value;
            }
        }

        /// <summary>
        /// ��Ⱦ����� ������ϴ���
        /// </summary>
        private bool isRenew = false;
        /// <summary>
        /// �Ƿ��� ����Ƕ�������copyԭ���Ļ�����insert�¿�
        /// ������޸�����ԭ��������update
        /// </summary>
        public bool IsRenew
        {
            get
            {
                return isRenew;
            }
            set
            {
                isRenew = value;
            }
        }

        /// <summary>
        /// �Ƿ��и߼�Ȩ�ޣ������ƶ���
        /// </summary>
        private bool isAdvance = false;

        /// <summary>
        /// �Ƿ��и߼�Ȩ�ޣ������ƶ���
        /// </summary>
        public bool IsAdvance
        {
            get
            {
                return isAdvance;
            }
            set
            {
                this.isAdvance = value;
            }
        }

        /// <summary>
        /// �Ƿ���Ҫ����
        /// </summary>
        private bool isNeedAdd;

        /// <summary>
        /// �Ƿ���Ҫ����
        /// </summary>
        public bool IsNeedAdd
        {
            get
            {
                return isNeedAdd;
            }
            set
            {
                isNeedAdd = value;
            }
        }

        /// <summary>
        /// �Ƿ���ʾ��ߵĲ�ѯ��
        /// </summary>
        private bool isDisplayPanelLeft;

        /// <summary>
        /// �Ƿ���ʾ��ߵĲ�ѯ��
        /// </summary>
        public bool IsDisplayPanelLeft
        {
            get
            {
                return isDisplayPanelLeft;
            }
            set
            {
                isDisplayPanelLeft = value;
            }
        }

        /// <summary>
        /// �Ƿ���Ҫ������ʾ
        /// </summary>
        private bool isNeedAdditionMeg = true;

        /// <summary>
        /// �Ƿ���Ҫ������ʾ
        /// </summary>
        public bool IsNeedAdditionMeg
        {
            get
            {
                return isNeedAdditionMeg;
            }
            set
            {
                isNeedAdditionMeg = value;
            }
        }

        private string infectCode = ""; 

        /// <summary>
        /// ָ����������
        /// </summary>
        public string InfectCode
        {
            get { return infectCode; }
            set { infectCode = value; }
        }

        private Neusoft.HISFC.DCP.Enum.ReportOperResult reportOperResult = Neusoft.HISFC.DCP.Enum.ReportOperResult.Other;

        /// <summary>
        /// �����������
        /// </summary>
        public Neusoft.HISFC.DCP.Enum.ReportOperResult ReportOperResult
        {
            get
            {
                return this.reportOperResult;
            }
            set
            {
                this.reportOperResult = value;
            }
        }
        #endregion

        #region ö��

        /// <summary>
        /// ״̬
        /// </summary>
        public enum enumTreeViewType
        {
            PatientInfo,
            ReportInfo,
            FeedBackInfo
        }

        /// <summary>
        /// 
        /// </summary>
        public enum enumResultType
        {
            lisResult,
            feedBack,
            other
        }

        /// <summary>
        /// ����������
        /// </summary>
        public enum OperType
        {
            ����,
            ����,
            �ϸ�,
            ���ϸ�,
            ����,
            ����,
            �ָ�,
            ɾ��,
            ��ѯ
        }

        #endregion

        #region ���캯��

        /// <summary>
        /// ���캯��
        /// </summary>
        public ucDiseaseReportRegister()
            : this(Neusoft.HISFC.DCP.Enum.PatientType.O)
        {
        }

        /// <summary>
        /// ���ع��캯��
        /// </summary>
        /// <param name="patientType"></param>
        public ucDiseaseReportRegister(Neusoft.HISFC.DCP.Enum.PatientType parmPatientType)
        {
            InitializeComponent();

            this.tvPatientInfo.ImageList = this.tvPatientInfo.groupImageList;

            this.PatientType = parmPatientType;
            this.Load += new EventHandler(ucInfectionReport_Load);
        }

        #endregion

        #region ��ʼ��

        /// <summary>
        /// ��ʼ�����ؽ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucInfectionReport_Load(object sender, System.EventArgs e)
        {
            this.Init();

            this.InitEvent();

            this.InitQueryContent();

             //����uc���õĲ�����ʼ��,ֻ�÷���load������          
            this.SetOperType( OperType.����);

            this.ShowInfo();

            this.InitPrivInformation();

            //this.InitQueryContent();

            this.TreeViewAddReportsIgnorState(this.alReport);
  
        }

        /// <summary>
        /// ��ʼ��
        /// </summary>
        public void Init()
        {
            Neusoft.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڳ�ʼ������,���Ժ�...");
            Application.DoEvents();
            try
            {
                this.tvPatientInfo.Nodes.Clear();

                this.User = Neusoft.FrameWork.Management.Connection.Operator as Neusoft.HISFC.Models.Base.Employee;
                //������
                this.Initcmb();

                //ʱ��
                this.Initdtp();

                this.cmbDoctorDept.Tag = this.User.Dept.ID;//��������
                this.cmbReportDoctor.Tag = this.User.ID;//����ҽ��
                this.cmbReportDoctor.Enabled = true;
                

            }
            catch (Exception ex)
            {
                this.MyMessageBox("��ʼ��ʧ�ܣ�" + ex.Message, "err");
            }
            finally
            {
                Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }
        }

     
        #region ������

        /// <summary>
        /// ��ʼ��������
        /// </summary>
        private void Initcmb()
        {
            //�Ա�
            ArrayList alSex = Neusoft.HISFC.Models.Base.SexEnumService.List();
            this.cmbSex.AddItems(alSex);
            //�������
            ArrayList alDept = commonProcess.QueryDeptAllValidAndUnvalid();
            this.cmbDoctorDept.AddItems(alDept);
            //����ҽ��
            ArrayList alDoctor = this.commonProcess.QueryEmployeeAllValidAndUnvalid();
            this.cmbReportDoctor.AddItems(alDoctor);
            
            //��������
            InitInfections();
            //��������
            InitCaseClass();
            //ְҵ
            InitProfession();
        }

        /// <summary>
        /// ��ʼ������
        /// </summary>
        private void InitInfections()
        {

            //��Ⱦ��������
            ArrayList alInfectClass = new ArrayList();

            alInfectClass.AddRange(commonProcess.QueryConstantList("INFECTCLASS"));
            if (alInfectClass.Count < 1)
            {
                return;
            }

            //��Ҫ�����Ĵ�Ⱦ��
            this.hshNeedAdd = new Hashtable();
            //����
            this.hshInfectClass = new Hashtable();
            //��Ҫ���Բ����ļ���
            this.hshNeedSexReport = new Hashtable();
            //��Ҫ��Ѫ�ͼ�ļ���

            this.hshNeedCheckedBlood = new Hashtable();
            //��Ҫ���������ļ���
            this.hshNeedCaseTwo = new Hashtable();
            //��Ҫ�绰����ļ���
            this.hshNeedTelInfect = new Hashtable();
            //��Ҫ��д��˲�ת�ﵥ�ļ���
            this.hshNeedBill = new Hashtable();
            //���������˷�
            this.hshLitteChild = new Hashtable();
            //��Ҫ�������Ƶ��Բ�
            this.hshSexNeedGradeTwo = new Hashtable();
            //��Ҫ��Ⱥ��������
            this.hshPatientTyepNeedDesc = new Hashtable();
            //��Ҫ��ע
            this.hshNeedMemo = new Hashtable();

            //�������ͻ�ȡ��Ⱦ��

            int index = 1;
            foreach (Neusoft.HISFC.Models.Base.Const infectclass in alInfectClass)
            {
                ArrayList al = new ArrayList();
                ArrayList alItem = new ArrayList();


                infectclass.Name = "--" + infectclass.Name + "--";
                infectclass.Name = infectclass.Name.PadLeft(13, ' ');
                al.Add(infectclass);
                if (index == 1)
                {
                    Neusoft.HISFC.Models.Base.Const o = new Neusoft.HISFC.Models.Base.Const();
                    o.ID = "####";
                    o.Name = "--��ѡ��--";
                    al.Insert(0, o);
                    index++;
                }
                alItem = commonProcess.QueryConstantList(infectclass.ID);

                al.AddRange(alItem);
                alInfectItem.AddRange(alItem);

                hshInfectClass.Add(infectclass.ID, null);
                foreach (Neusoft.HISFC.Models.Base.Const infect in al)
                {
                    //���ƹ�����ά���ڱ�ע��ڴ˽���
                    if (infect.Name.IndexOf("��ע", 0) != -1)
                    {
                        infect.Name = infect.Memo;
                        infect.Memo = "";
                    }
                    if (infect.Memo.IndexOf("�踽��", 0) != -1)
                    {
                        hshNeedAdd.Add(infect.ID, null);
                    }
                    if (infect.Memo.IndexOf("���Բ�����", 0) != -1)
                    {
                        hshNeedSexReport.Add(infect.ID, null);
                    }
                    if (infect.Memo.IndexOf("�豸ע") != -1)
                    {
                        hshNeedMemo.Add(infect.ID, null);
                    }
                    //�Բ���������
                    if (infect.Memo.IndexOf("��������", 0) != -1)
                    {
                        hshSexNeedGradeTwo.Add(infect.ID, null);
                    }
                    if (infect.Memo.IndexOf("���Ѫ�ͼ�", 0) != -1)
                    {
                        hshNeedCheckedBlood.Add(infect.ID, null);
                    }
                    //������������
                    if (infect.Memo.IndexOf("��������", 0) != -1)
                    {
                        hshNeedCaseTwo.Add(infect.ID, null);
                    }
                    //�绰֪ͨ
                    if (infect.Memo.IndexOf("�绰֪ͨ", 0) != -1)
                    {
                        hshNeedTelInfect.Add(infect.ID, null);
                    }
                    //��˲�ת�ﵥ
                    if (infect.Memo.IndexOf("��ת�ﵥ", 0) != -1)
                    {
                        hshNeedBill.Add(infect.ID, null);
                    }
                    if (infect.Memo.IndexOf("���������˷�", 0) != -1 || infect.Name.IndexOf("���������˷�", 0) != -1)
                    {
                        hshLitteChild.Add(infect.ID, null);
                    }

                }
                alinfection.AddRange(al);
                Neusoft.FrameWork.Models.NeuObject ob = new Neusoft.FrameWork.Models.NeuObject();
                ob.ID = "####";
                ob.Name = "    ";
                alinfection.Add(ob);
            }
            this.cmbInfectionClass.AddItems(alinfection);
            this.cmbInfectionClass.DataSource = alinfection;
            this.cmbInfectionClass.DisplayMember = "Name";
            this.cmbInfectionClass.ValueMember = "ID";             
        }

        /// <summary>
        /// ��������
        /// </summary>
        private void InitCaseClass()
        {
            Neusoft.HISFC.Models.Base.Const obj = new Neusoft.HISFC.Models.Base.Const();
            obj.ID = "####";
            obj.Name = "--��ѡ��--";
            ArrayList al = new ArrayList();
            al.Add(obj);
            al.AddRange(commonProcess.QueryConstantList("CASECLASS"));
            this.cmbCaseClassOne.AddItems(al);
            this.cmbCaseClassOne.DataSource = al;
            this.cmbCaseClassOne.ValueMember = "ID";
            this.cmbCaseClassOne.DisplayMember = "Name";

            ArrayList altwo = new ArrayList();
            Neusoft.HISFC.Models.Base.Const obone = new Neusoft.HISFC.Models.Base.Const();

            //altwo.Add(obj);
            Neusoft.HISFC.Models.Base.Const obthree = new Neusoft.HISFC.Models.Base.Const();
            obthree.ID = "2";
            obthree.Name = "δ����";
            altwo.Add(obthree);

            obone.ID = "0";
            obone.Name = "����";
            altwo.Add(obone);

            Neusoft.HISFC.Models.Base.Const obtwo = new Neusoft.HISFC.Models.Base.Const();
            obtwo.ID = "1";
            obtwo.Name = "����";
            altwo.Add(obtwo);

            this.cmbCaseClaseTwo.DataSource = altwo;
            this.cmbCaseClaseTwo.ValueMember = "ID";
            this.cmbCaseClaseTwo.DisplayMember = "Name";
        }

        /// <summary>
        /// ��ʼ��ְҵ
        /// </summary>
        private void InitProfession()
        {
            Neusoft.HISFC.Models.Base.Const obj = new Neusoft.HISFC.Models.Base.Const();
            ArrayList alpro = new ArrayList();
            obj.ID = "####";
            obj.Name = "--��ѡ��--";
            //ְҵ
            alpro.Add(obj);
            alpro.AddRange(commonProcess.QueryConstantList(Neusoft.HISFC.Models.Base.EnumConstant.PROFESSION));
            this.hshStudent = new Hashtable();

            foreach (Neusoft.HISFC.Models.Base.Const con in alpro)
            {
                if (con.Name.IndexOf("����", 0) != -1 || con.Memo.IndexOf("��ͯ", 0) != -1 || con.Name.IndexOf("��ͯ", 0) != -1)
                {
                    hshStudent.Add(con.ID, "���׻������༶����");
                }
                if (con.Name.IndexOf("ѧ��", 0) != -1 || con.Memo.IndexOf("ѧ��", 0) != -1)
                {
                    hshStudent.Add(con.ID, "ѧУ���༶����");
                }
            }
            this.cmbProfession.AddItems(alpro);
            this.cmbProfession.DataSource = alpro;
            this.cmbProfession.ValueMember = "ID";
            this.cmbProfession.DisplayMember = "Name";
            this.cmbProfession.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        /// <summary>
        /// ��ѯ���ݳ�ʼ��
        /// </summary>
        private void InitQueryContent()
        {
            //��ʼ����ѯ����
            ArrayList al = new ArrayList();
            Neusoft.FrameWork.Models.NeuObject obj = new Neusoft.FrameWork.Models.NeuObject();
            if (this.PatientType == Neusoft.HISFC.DCP.Enum.PatientType.O)
            {
                obj.ID = "ReportInfo";
                obj.Name = "ȫԺ������Ϣ��ѯ";
                al.Add(obj);
            }

            obj = new Neusoft.FrameWork.Models.NeuObject();
            obj.ID = "PatientInfo";
            obj.Name = "���߲�ѯ";
            al.Add(obj);

            obj = new Neusoft.FrameWork.Models.NeuObject();
            obj.ID = "DeptReport";
            obj.Name = "�����ұ�����Ϣ��ѯ";
            al.Add(obj);

            obj = new Neusoft.FrameWork.Models.NeuObject();
            obj.ID = "DeptUnReport";
            obj.Name = "�����Ҳ��ϸ񱨿���ѯ";
            al.Add(obj);

            this.cmbQueryContent.DataSource = al;
            this.cmbQueryContent.DisplayMember = "Name";
            this.cmbQueryContent.ValueMember = "ID";
            this.cmbQueryContent.SelectedIndex = 0;
            this.cmbQueryContent.DropDownStyle = ComboBoxStyle.DropDownList;

            //�ڴ˼����¼��������һ�γ�ʼ���¼�ʱ����12�ε����¼������������Ӱ���һ�μ��ؽ�������ܣ�  --���ף��޸��� 20:15 2010-12-26��
            //this.cmbQueryContent.SelectedValueChanged += new EventHandler(cmbQueryContent_SelectedValueChanged);
        }

        #endregion

        #region ʱ��

        /// <summary>
        /// ��ʼ��ʱ��
        /// </summary>
        private void Initdtp()
        {
            //����
            this.dtBirthDay.Value = diseaseMgr.GetDateTimeFromSysDateTime();
            //���ʱ��
            this.dtDiaDate.Value = diseaseMgr.GetDateTimeFromSysDateTime();
            //���ʱ��
            this.lbReportTime.Text = diseaseMgr.GetSysDateTime("yyyy��MM��dd�� HH:mm:ss");
            //���� �� ����ʱ��

            this.SetEnablellb(this.PatientType);

            //��ʼ����ѯʱ��
            System.DateTime dt = this.diseaseMgr.GetDateTimeFromSysDateTime();
            dt = dt.AddDays(1);
            this.dtEnd.Value = new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0);
            this.dtBegin.Value = this.dtEnd.Value.AddDays(-30);
        }
        
        #endregion

        #region �¼�ί��

        /// <summary>
        /// ��ʼ���¼�ί��
        /// </summary>
        private void InitEvent()
        {            
            this.tvPatientInfo.AfterSelect += new TreeViewEventHandler(tvPatientInfo_AfterSelect);
            this.cmbQueryContent.SelectedValueChanged += new EventHandler(cmbQueryContent_SelectedValueChanged);
            //this.cmbInfectionClass.Click += new EventHandler(cmbInfectionClass_Enter);
        }

        #endregion

        #endregion

        #region ������

        /// <summary>
        /// �������ĳ�ʼ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override Neusoft.FrameWork.WinForms.Forms.ToolBarService OnInit (object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton( "�½�", "", (int)Neusoft.FrameWork.WinForms.Classes.EnumImageList.X�½�, true, false, null );

            toolBarService.AddToolButton( "�ϸ�", "Eligible", (int)Neusoft.FrameWork.WinForms.Classes.EnumImageList.Zִ��, true, false, null );
            toolBarService.AddToolButton( "���ϸ�", "UnEligible", (int)Neusoft.FrameWork.WinForms.Classes.EnumImageList.Zע��, true, false, null );

            toolBarService.AddToolButton( "����", "", (int)Neusoft.FrameWork.WinForms.Classes.EnumImageList.X��һ��, true, false, null );
            toolBarService.AddToolButton( "�ָ�", "", (int)Neusoft.FrameWork.WinForms.Classes.EnumImageList.MĬ��, true, false, null );
            toolBarService.AddToolButton( "����", "Cancel", (int)Neusoft.FrameWork.WinForms.Classes.EnumImageList.Qȡ��, true, false, null );

            toolBarService.AddToolButton( "ɾ��", "", (int)Neusoft.FrameWork.WinForms.Classes.EnumImageList.Sɾ��, true, false, null );

            return toolBarService;
        }

        #region ��������ť

        /// <summary>
        /// ��������ť�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ToolStrip_ItemClicked (object sender, ToolStripItemClickedEventArgs e)
        {
            this.ToolStrip_ItemClicked( e.ClickedItem.Text );
            base.ToolStrip_ItemClicked( sender, e );
        }

        /// <summary>
        ///  ������������ť�ǵ���,д������,�Ա㱻�ⲿ����
        /// </summary>
        /// <param name="clickedItemText"></param>
        public void ToolStrip_ItemClicked (string clickedItemText)
        {
            switch (clickedItemText)
            {
                case "�½�":
                    if (this.SetOperType( OperType.���� ) == 1)
                    {
                        //this.ClinicNo = "";
                        this.Patient = null;
                        this.TreeViewShowDeptPatient();
                        //this.ClearAll( true );
                    }
                    break;
                case "�ϸ�":
                    this.SetOperType( OperType.�ϸ� );
                    break;
                case "���ϸ�":
                    this.SetOperType( OperType.���ϸ� );
                    break;
                case "����":
                    if (this.SetOperType( OperType.���� ) == 1)
                    {
                        this.OnCorrect();
                    }
                    break;
                case "�ָ�":
                    this.SetOperType( OperType.�ָ� );
                    break;
                case "����":
                    this.SetOperType( OperType.���� );
                    break;
                case "ɾ��":
                    if (this.SetOperType( OperType.ɾ�� ) == 1)
                    {
                        this.DeleteReport();
                    }
                    break;
                case "��ѯ":
                    this.Query();
                    break;
                case "����":
                    if (this.SetOperType( OperType.���� ) == 1)
                    {
                        this.OnSave( new object(), new object() );
                    }
                    break;
                case "�˳�":
                    this.reportOperResult = Neusoft.HISFC.DCP.Enum.ReportOperResult.Cancel;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public void OnCorrect ()
        {
            if (this.SaveCorrectReport() == 0)
            {
                this.ClearAll();

                if (this.AlReport != null && this.AlReport.Count > 0)
                {
                    this.ReflashTreeView(this.AlReport);
                }
                else
                {
                    this.QueryOldReport();
                }
                this.SetOperType(OperType.��ѯ);

                // ����Ϻ��Ƿ���д�˱��濨��������ɹ���������
                this.Text += "--����ɹ�";
            }
        }

        /// <summary>
        /// ���水ť
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnSave (object sender, object neuObject)
        {
            if (this.lblState.Text == "1")
            {
                MessageBox.Show("�����ϵı��濨�������޸ģ�");
                return -1;
            }

            if (this.SaveRepot() == 0)
            {
                this.ClearAll();
                if (this.AlReport != null && this.AlReport.Count > 0)
                {
                    this.ReflashTreeView(this.AlReport);
                }
                else
                {
                    this.QueryOldReport();
                }
                this.SetOperType(OperType.��ѯ);
                // ����Ϻ��Ƿ���д�˱��濨��������ɹ���������
                this.Text += "--����ɹ�";
            }
            return 1;
        }

        /// <summary>
        /// ��ѯ��ť
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnQuery (object sender, object neuObject)
        {
            this.Query();
            return 0;
        }

        /// <summary>
        /// ���ò���Ա�Ĳ�������
        /// </summary>
        /// <param name="operType"></param>
        /// <returns>�������ͽ�� 1 �ɹ� -1 ʧ��</returns>
        private int SetOperType (OperType operType)
        {
            if (PreArrange() == -1)
            {
                return -1;
            }

            this.operType = operType;

            Neusoft.HISFC.DCP.Object.CommonReport report = this.GetSelectedReport();
            if (report == null || string.IsNullOrEmpty(report.ID)== true)
            {
                if (this.operType == OperType.����)     //�����½��ĵ�������
                {
                    return 1;
                }
                return -1;
            }
            if (IsAllowOper( report, operType ) == false)
            {
                return -1;
            }
            switch (this.operType)
            {
                case OperType.�ϸ�:
                    this.UpdateReportState( report, Neusoft.HISFC.DCP.Enum.ReportState.Eligible );
                    break;
                case OperType.���ϸ�:
                    //2011-3-8 ���ϸ�Ļ������˿�ԭ��
                    if (string.IsNullOrEmpty(this.txtCase.Text))
                    {
                        MessageBox.Show("����д���ϸ�ԭ��������Ϊ���ϸ�");
                        this.txtCase.Focus();
                        return -1;
                    }
                    report.OperCase = this.txtCase.Text;
                    this.UpdateReportState( report, Neusoft.HISFC.DCP.Enum.ReportState.UnEligible );
                    break;
                case OperType.����:
                    if (this.User.ID == report.Oper.ID)
                    {
                        this.UpdateReportState( report, Neusoft.HISFC.DCP.Enum.ReportState.OwnCancel );
                    }
                    else
                    {
                        this.UpdateReportState( report, Neusoft.HISFC.DCP.Enum.ReportState.Cancel );
                    }
                    break;
                case OperType.�ָ�:
                    if (this.User.ID == report.Oper.ID)
                    {
                        this.UpdateReportState( report, Neusoft.HISFC.DCP.Enum.ReportState.New );
                    }
                    else
                    {
                        this.UpdateReportState( report, Neusoft.HISFC.DCP.Enum.ReportState.Cancel );
                    }
                    break;
            }

            return 1;
        }

        #endregion

        #endregion

        #region Ȩ����ز���

        /// <summary>
        /// ��ʼȨ�޿���
        /// </summary>
        protected void InitPrivInformation ()
        {
            Neusoft.HISFC.DCP.BizProcess.Permission permissionProcess = new Neusoft.HISFC.DCP.BizProcess.Permission();
            this.cdcPrivDeptList = permissionProcess.QueryUserPriv( Neusoft.FrameWork.Management.Connection.Operator.ID, "8001" );

            if (this.cdcPrivDeptList == null)
            {
                MessageBox.Show( "��ȡCDCȨ�޿��ҷ�������" + permissionProcess.Err, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information );
                return;
            }
            /*
             * ҽ�����Խ��еĲ���
             *  1���½�����ѯ
             *  2�����桢����
             *  3�����ϡ�ɾ��
             *  4���޸ı��˽��Ŀ�
             * 
             * CDCȨ�����н��еĲ���
             *  1����ˣ��ϸ񡢲��ϸ�
             *  2���޸�
             *  3���ָ�
            */

            //����Ȩ������
            toolBarService.SetToolButtonEnabled( "�ϸ�", this.isCDCPriv );
            toolBarService.SetToolButtonEnabled( "���ϸ�", this.isCDCPriv );
            this.cmbDoctorDept.Enabled = this.isCDCPriv;
        }

        /// <summary>
        /// �ж��Ƿ�ΪCDCȨ�޿���
        /// </summary>
        /// <param name="deptCode">���ұ���</param>
        /// <returns>�ɹ�����True ʧ�ܷ���False</returns>
        protected bool IsCDCDept (string deptCode)
        {
            if (this.cdcPrivDeptList != null)
            {
                foreach (Neusoft.FrameWork.Models.NeuObject info in this.cdcPrivDeptList)
                {
                    if (info.ID == deptCode)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// ����Ȩ�޵��ж�
        /// </summary>
        /// <param name="report">����ʵ��</param>
        /// <param name="operType">������ʽ</param>
        /// <returns>true�в���Ȩ�� false�޲���Ȩ��</returns>
        private bool IsAllowOper (Neusoft.HISFC.DCP.Object.CommonReport report, OperType operType)
        {
            if (report == null)
            {
                return true;
            }

            bool isAllow = false;

            switch (operType)
            {
                case OperType.����:

                    #region Modify

                    switch (Int32.Parse( report.State ))
                    {
                        case (int)Neusoft.HISFC.DCP.Enum.ReportState.New:               //�½���
                        case (int)Neusoft.HISFC.DCP.Enum.ReportState.UnEligible:        //���ϸ�

                            if (this.User.ID == report.ReportDoctor.ID)                //����뵱ǰ����Աһ��
                            {
                                isAllow = true;
                            }
                            else
                            {
                                if (this.IsCDCDept( this.User.Dept.ID ) == true)              //�ж��Ƿ���CDCȨ�ޣ�����Ȩ�������޸�
                                {
                                    isAllow = true;
                                }
                            }
                            if (isAllow == false)
                            {
                                MessageBox.Show("��ʾ�������޸�������д�ı���", "��ʾ>>" ,MessageBoxButtons.OK,MessageBoxIcon.Information);
                            }
                            break;
                        case (int)Neusoft.HISFC.DCP.Enum.ReportState.Eligible:
                            MessageBox.Show( "��ʾ�������Ѿ��ϸ�", "��ʾ>>", MessageBoxButtons.OK, MessageBoxIcon.Information );
                            break;
                        case (int)Neusoft.HISFC.DCP.Enum.ReportState.OwnCancel:
                            MessageBox.Show( "��ʾ�������Ѿ����� �������޸�", "��ʾ>>", MessageBoxButtons.OK, MessageBoxIcon.Information );
                            break;
                        case (int)Neusoft.HISFC.DCP.Enum.ReportState.Cancel:
                            MessageBox.Show( "��ʾ���������ʱ�Ѿ����� �����޸�", "��ʾ>>", MessageBoxButtons.OK, MessageBoxIcon.Information );
                            break;
                    }

                    #endregion

                    break;
                case OperType.����:

                    #region Cancel

                    switch (Int32.Parse( report.State ))
                    {
                        case (int)Neusoft.HISFC.DCP.Enum.ReportState.New:
                        case (int)Neusoft.HISFC.DCP.Enum.ReportState.UnEligible:
                            if (this.User.ID == report.ReportDoctor.ID)
                            {
                                isAllow = true;
                            }
                            else
                            {
                                if (this.IsCDCDept( this.User.Dept.ID ) == true)              //�ж��Ƿ���CDCȨ�ޣ�����Ȩ�������޸�
                                {
                                    isAllow = true;
                                }
                            }
                            if (isAllow == false)
                            {
                                MessageBox.Show( this, "��ʾ�������޸�������д�ı���", "��ʾ>>" );
                            }
                            else
                            {
                                if (MessageBox.Show( this, "ȷʵҪ���ϱ�����", "��ʾ>>", System.Windows.Forms.MessageBoxButtons.YesNo,
                                    System.Windows.Forms.MessageBoxIcon.Information, System.Windows.Forms.MessageBoxDefaultButton.Button2 )
                                    == System.Windows.Forms.DialogResult.No)
                                {
                                    isAllow = false;
                                }
                            }
                            break;
                        case (int)Neusoft.HISFC.DCP.Enum.ReportState.Eligible:
                            MessageBox.Show( this, "��ʾ�������Ѿ��ϸ� ��������", "��ʾ>>" );
                            break;
                        case (int)Neusoft.HISFC.DCP.Enum.ReportState.OwnCancel:
                            MessageBox.Show( this, "��ʾ�������Ѿ�������������", "��ʾ>>" );
                            break;
                        case (int)Neusoft.HISFC.DCP.Enum.ReportState.Cancel:
                            MessageBox.Show( this, "��ʾ���������ʱ�Ѿ�����", "��ʾ>>" );
                            break;
                    }
                    #endregion

                    break;
                case OperType.ɾ��:

                    #region ɾ��

                    if (Neusoft.FrameWork.Function.NConvert.ToInt32( report.State ) != (int)Neusoft.HISFC.DCP.Enum.ReportState.New)
                    {
                        MessageBox.Show( "���½��������ܽ���ɾ������", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information );
                    }
                    else
                    {
                        if (this.User.ID != report.ReportDoctor.ID)
                        {
                            MessageBox.Show( this, "��ʾ������ɾ��������д�ı���", "��ʾ>>" );
                        }
                        else
                        {
                            return true;
                        }
                    }

                    #endregion

                    break;

                case OperType.�ϸ�:

                    #region �ϸ�

                    if (Int32.Parse( report.State ) == (int)Neusoft.HISFC.DCP.Enum.ReportState.New)
                    {
                        isAllow = true;
                    }
                    else if (Int32.Parse( report.State ) == (int)Neusoft.HISFC.DCP.Enum.ReportState.Eligible)
                    {
                        MessageBox.Show( this, "��ʾ����������˺ϸ�", "��ʾ>>" );
                    }
                    else if (Int32.Parse( report.State ) == (int)Neusoft.HISFC.DCP.Enum.ReportState.UnEligible)
                    {
                        if (MessageBox.Show( this, "���濨����ˣ��Ƿ�����", "��ʾ>>", System.Windows.Forms.MessageBoxButtons.YesNo,
                            System.Windows.Forms.MessageBoxIcon.Information, System.Windows.Forms.MessageBoxDefaultButton.Button2 )
                            == System.Windows.Forms.DialogResult.Yes)
                            isAllow = true;
                    }
                    else if (Int32.Parse( report.State ) == (int)Neusoft.HISFC.DCP.Enum.ReportState.OwnCancel)
                    {
                        MessageBox.Show( this, "��ʾ�������Ѿ�����������", "��ʾ>>" );
                    }
                    else if (Int32.Parse( report.State ) == (int)Neusoft.HISFC.DCP.Enum.ReportState.Cancel)
                    {
                        MessageBox.Show( this, "��ʾ���������ʱ�Ѿ�����", "��ʾ>>" );
                    }
                    #endregion

                    break;
                case OperType.���ϸ�:
                    #region
                    if (Int32.Parse( report.State ) == (int)Neusoft.HISFC.DCP.Enum.ReportState.New)
                    {
                        isAllow = true;
                    }
                    else if (Int32.Parse( report.State ) == (int)Neusoft.HISFC.DCP.Enum.ReportState.UnEligible)
                    {
                        MessageBox.Show( this, "��ʾ����������˲��ϸ�", "��ʾ>>" );
                    }
                    else if (Int32.Parse( report.State ) == (int)Neusoft.HISFC.DCP.Enum.ReportState.Eligible)
                    {
                        if (MessageBox.Show( this, "���濨����ˣ��Ƿ�����", "��ʾ>>", System.Windows.Forms.MessageBoxButtons.YesNo,
                            System.Windows.Forms.MessageBoxIcon.Information, System.Windows.Forms.MessageBoxDefaultButton.Button2 )
                            == System.Windows.Forms.DialogResult.Yes)
                            isAllow = true;
                    }
                    else if (Int32.Parse( report.State ) == (int)Neusoft.HISFC.DCP.Enum.ReportState.OwnCancel)
                    {
                        MessageBox.Show( this, "��ʾ�������Ѿ�����������", "��ʾ>>" );
                    }
                    else if (Int32.Parse( report.State ) == (int)Neusoft.HISFC.DCP.Enum.ReportState.Cancel)
                    {
                        MessageBox.Show( this, "��ʾ���������ʱ�Ѿ�����", "��ʾ>>" );
                    }
                    break;
                    #endregion
                case OperType.�ָ�:
                    #region �ָ�
                    if (this.IsCDCDept( this.User.Dept.ID ) == false)
                    {
                        MessageBox.Show( this, "��ʾ���ָ����濨���ڼ���Ԥ������ϵ", "��ʾ>>" );
                    }
                    else
                    {
                        if (Int32.Parse( report.State ) != (int)Neusoft.HISFC.DCP.Enum.ReportState.OwnCancel && Int32.Parse( report.State ) != (int)Neusoft.HISFC.DCP.Enum.ReportState.Cancel)
                        {
                            MessageBox.Show( this, "��ʾ��û�����ϵı��治����ָ�", "��ʾ>>" );
                            break;
                        }
                        isAllow = true;
                        if (MessageBox.Show( this, "ȷʵҪ�ָ���", "��ʾ>>", System.Windows.Forms.MessageBoxButtons.YesNo,
                            System.Windows.Forms.MessageBoxIcon.Information, System.Windows.Forms.MessageBoxDefaultButton.Button2 )
                            == System.Windows.Forms.DialogResult.No)
                        {
                            isAllow = false;
                        }
                    }
                    break;
                    #endregion
                case OperType.����:
                    if (this.User.ID == report.ReportDoctor.ID)
                    {
                        isAllow = true;

                        string state = this.diseaseMgr.GetCommonReportByID( report.ID ).State;
                        if (state == ((int)Neusoft.HISFC.DCP.Enum.ReportState.Eligible).ToString())
                        {
                            MessageBox.Show( this, "���濨�Ѿ����ͨ�������ܽ��ж�������", "����>>" );
                            isAllow = false;
                        }
                        else if (state == ((int)Neusoft.HISFC.DCP.Enum.ReportState.Cancel).ToString() || state == ((int)Neusoft.HISFC.DCP.Enum.ReportState.OwnCancel).ToString())
                        {
                            MessageBox.Show( this, "���濨�Ѿ����ϣ����ܽ��ж�������", "����>>" );
                            isAllow = false;
                        }
                    }
                    else
                    {
                        MessageBox.Show( this, "��ʾ�����ɶ�������д�ı�����ж�������", "��ʾ>>" );
                    }
                    break;

            }
            return isAllow;
        }
        #endregion       

        #region ��ʾ��Ϣ

        /// <summary>
        /// ��ʾ��Ϣ
        /// </summary>
        public void ShowInfo()
        {
            if (this.PatientType != Neusoft.HISFC.DCP.Enum.PatientType.O)
            {
                if (this.Patient != null)
                {
                    this.ShowPatienInfo(this.Patient);

                    ////��ʾ������Ϣ
                    //if (this.Type == Neusoft.HISFC.DCP.Enum.PatientType.C)              //����
                    //{
                    //    //this.ClinicNo = this.Patient.ID;
                    //}
                    //else if (this.Type == Neusoft.HISFC.DCP.Enum.PatientType.I)         //סԺ
                    //{
                    //    this.InpatientNO = this.Patient.ID;
                    //}

                    this.TreeViewShowDeptPatient();
                }
            }
            else
            {
                //��ʾ��ѯ����Ϣ
            }
        }

        #endregion

        #region ����ʾ����

        #region ������״̬��ʾ������Ϣ

        /// <summary>
        /// ��ѯȫԺ��Ⱦ������
        /// </summary>
        private void QueryHospitalReport()
        {
            ArrayList al = new ArrayList();
            if (this.PatientType == Neusoft.HISFC.DCP.Enum.PatientType.O)
            {
                al = this.diseaseMgr.GetCommonReportListByReportTime(this.dtBegin.Value, this.dtEnd.Value);
            }
            if (al.Count > 0)
            {
                //��ʾ����
                this.TreeViewAddReports( al );
            }
        }

        /// <summary>
        ///  ��ʾ���濨[ͬ״̬�ı���]
        /// </summary>
        /// <param name="al">ͬ״̬�ı���</param>
        private void TreeViewAddReports(ArrayList al)
        {
            if (al.Count < 1 || al == null)
            {
                return;
            }

            this.tvPatientInfo.Nodes.Clear();

            CompareState compare = new CompareState();

            //al.Sort( compare );

            ArrayList alTempSortData = new ArrayList();
            string privState = string.Empty;

            foreach (Neusoft.HISFC.DCP.Object.CommonReport info in al)
            {
                if (info.State != privState)
                {
                    if (alTempSortData.Count > 0)
                    {
                        this.TreeViewAddReports( alTempSortData, (Neusoft.HISFC.DCP.Enum.ReportState)Neusoft.FrameWork.Function.NConvert.ToInt32( privState ) );
                    }
                 
                    alTempSortData = new ArrayList();

                    alTempSortData.Add( info );

                    privState = info.State;
                }
                else
                {
                    alTempSortData.Add( info );
                }
            }

            if (alTempSortData.Count > 0)
            {
                this.TreeViewAddReports( alTempSortData, (Neusoft.HISFC.DCP.Enum.ReportState)Neusoft.FrameWork.Function.NConvert.ToInt32( privState ) );
            }
        }

        /// <summary>
        /// ��ʾ�����б�[��״̬���б������ӷּ��ڵ�]
        /// </summary>
        private void TreeViewAddReports(ArrayList al, Neusoft.HISFC.DCP.Enum.ReportState reportState)
        {           
            try
            {
                this.TreeViewType = enumTreeViewType.ReportInfo;
                System.Windows.Forms.TreeNode node = new TreeNode();
                int imagindex = 4;

                //���ڵ����� ��ʾ����״̬

                switch (reportState)
                {
                    case Neusoft.HISFC.DCP.Enum.ReportState.New:
                        node.Text = "����";
                        break;
                    case Neusoft.HISFC.DCP.Enum.ReportState.Eligible:
                        node.Text = "�ϸ�";
                        imagindex = 4;
                        break;
                    case Neusoft.HISFC.DCP.Enum.ReportState.UnEligible://���
                        node.Text = "���ϸ����޸ı�����";
                        node.ForeColor = System.Drawing.Color.Red;
                        imagindex = 3;
                        break;
                    case Neusoft.HISFC.DCP.Enum.ReportState.OwnCancel:
                        node.Text = "��������";
                        node.ForeColor = System.Drawing.Color.Red;
                        imagindex = 3;
                        break;
                    case Neusoft.HISFC.DCP.Enum.ReportState.Cancel://����
                        node.Text = "����������";//����������
                        imagindex = 3;
                        break;
                    default:
                        break;
                }

                node.Tag = "root";
                node.ImageIndex = 0;
                this.tvPatientInfo.Nodes.Add(node);
                //�ӽڵ���� ��ʾ�������� ������
                string msg = "";
                foreach (Neusoft.HISFC.DCP.Object.CommonReport report in al)
                {
                    System.Windows.Forms.TreeNode reportnode = new TreeNode();
                    reportnode.Tag = report;
                    if (report.Patient.Name == null || report.Patient.Name == "")
                    {
                        reportnode.Text = report.PatientParents + "[" + report.ReportNO + "]" + report.ExtendInfo3;
                    }
                    else
                    {
                        reportnode.Text = report.Patient.Name + "[" + report.ReportNO + "]" + report.ExtendInfo3; ;
                    }
                    reportnode.ImageIndex = imagindex;
                    reportnode.SelectedImageIndex = 2;
                    this.tvPatientInfo.Nodes[this.tvPatientInfo.Nodes.Count - 1].Nodes.Add(reportnode);

                    
                }
                if (msg != "")
                {
                    MessageBox.Show(this, "����д��" + msg + "���濨���ϸ���鿴[�˿�ԭ��]��������Ӧ�޸�", "�˿�ԭ��");
                }
                this.tvPatientInfo.ExpandAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "������ʷ������Ϣʧ��" + ex.Message, "����>>");
            }
        }

        /// <summary>
        /// ���б�ˢ��
        /// </summary>
        /// <param name="alAllState">�б��е����б���</param>
        private void ReflashTreeView(ArrayList alAllState)
        {
            //���������ʱˢ�±����б� ���Ƶ��㷨

            //���ڳ�ʼ����ǰ���濨�����Ѿ���ֵ������������״̬�ı䡣���԰�״̬���·�����ʾ

            try
            {
                ArrayList alNew = new ArrayList();//�¼�
                ArrayList alGood = new ArrayList();//�ϸ�
                ArrayList alBad = new ArrayList();//���ϸ�
                ArrayList alCancel = new ArrayList();//����������

                //ArrayList alfive = new ArrayList();//����������


                foreach (ArrayList alonestate in alAllState)
                {
                    foreach (Neusoft.HISFC.DCP.Object.CommonReport report in alonestate)
                    {
                        Neusoft.HISFC.DCP.Object.CommonReport tempreport = new Neusoft.HISFC.DCP.Object.CommonReport();
                        tempreport = this.diseaseMgr.GetCommonReportByID(report.ID);
                        switch (Int32.Parse(tempreport.State))
                        {
                            case (int)Neusoft.HISFC.DCP.Enum.ReportState.New:
                                alNew.Add(tempreport);
                                break;
                            case (int)Neusoft.HISFC.DCP.Enum.ReportState.Eligible:
                                alGood.Add(tempreport);
                                break;
                            case (int)Neusoft.HISFC.DCP.Enum.ReportState.UnEligible:
                                alBad.Add(tempreport);
                                break;
                            case (int)Neusoft.HISFC.DCP.Enum.ReportState.OwnCancel:
                            case (int)Neusoft.HISFC.DCP.Enum.ReportState.Cancel:
                                alCancel.Add(tempreport);
                                break;
                        }
                    }
                }
                this.tvPatientInfo.Nodes.Clear();
                this.TreeViewAddReports(alNew,Neusoft.HISFC.DCP.Enum.ReportState.New);
                this.TreeViewAddReports(alGood,Neusoft.HISFC.DCP.Enum.ReportState.Eligible);
                this.TreeViewAddReports(alBad,Neusoft.HISFC.DCP.Enum.ReportState.UnEligible);
                this.TreeViewAddReports(alCancel,Neusoft.HISFC.DCP.Enum.ReportState.Cancel);

            }
            catch (Exception ex)
            {
                MessageBox.Show("ˢ���б����>>" + ex.Message);
            }
        }

        #endregion

        #region ��ʼ������ʾ������Ϣ

        /// <summary>
        /// ���ڵ���ʾ���һ���
        /// </summary>
        private void TreeViewShowDeptPatient()
        {
            //������ӱ��濨ʱ�������ﻼ�߻��߲�������
            try
            {
                if (this.type == Neusoft.HISFC.DCP.Enum.PatientType.C)//����
                {
                    #region ���ﴦ��

                    ArrayList alpatient = new ArrayList();
                    //if (this.ClinicNo != "")
                    if (this.Patient!=null&&string.IsNullOrEmpty(this.Patient.ID)==false)
                    {
                        //������ҽ��վ�����ʱ�ᴫ�뿴��ţ���ʱֻ��ʾһ������
                        Neusoft.HISFC.Models.Registration.Register r = new Neusoft.HISFC.Models.Registration.Register();

                        r = this.patientProcess.GetByClinic(this.Patient.ID);
                        if (r != null && string.IsNullOrEmpty(r.ID) == false)
                        {
                            alpatient.Add(r);
                        }
                        else
                        {
                            alpatient.Add(this.Patient);
                        }
                    }
                    else
                    {
                        //��ʾ��Dr���в��ϸ�ı���
                        this.tvPatientInfo.Nodes.Clear();
                        ArrayList al = new ArrayList();
                        al = this.diseaseMgr.GetReportListByStateAndDoctor(Function.ConvertState(Neusoft.HISFC.DCP.Enum.ReportState.UnEligible), this.User.ID);
                        if (al != null && al.Count > 0)
                        {
                            this.TreeViewAddReports(al);
                        }
                        return;
                    }
                    this.TreeViewAddClincPatients(alpatient);
                    try
                    {
                        if (this.tvPatientInfo.Nodes.Count>0)
                        {
                            this.tvPatientInfo.SelectedNode = this.tvPatientInfo.Nodes[0].FirstNode;
                        }
                    }
                    catch(Exception e)
                    {
                        MessageBox.Show(e.Message);
                        return;
                    }

                    #endregion
                }
                else if (this.type == Neusoft.HISFC.DCP.Enum.PatientType.I)//סԺ
                {
                    if (this.Patient != null && string.IsNullOrEmpty(this.Patient.ID) == false)
                    {
                        ArrayList alpatient = new ArrayList();
                        Neusoft.HISFC.Models.RADT.PatientInfo patientTemp = this.patientProcess.GetPatientInfomation( this.Patient.ID );
                        if (patientTemp == null)
                        {
                            MessageBox.Show( "����סԺ�ż��ػ�����Ϣ��������" + this.patientProcess.ErrorMsg );
                            return;
                        }

                        Neusoft.HISFC.DCP.Object.CommonReport newInpatientReport = new Neusoft.HISFC.DCP.Object.CommonReport();

                        newInpatientReport.Patient = patientTemp;
                        newInpatientReport.Patient.PID.CardNO = newInpatientReport.Patient.PID.PatientNO;

                        newInpatientReport.ReportDoctor = this.User;
                        newInpatientReport.DoctorDept = this.User.Dept;

                        ArrayList alTemp = new ArrayList();

                        alTemp.Add( newInpatientReport );

                        this.TreeViewAddReports( alTemp );
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("��ȡ������Ϣʧ��>>" + ex.Message);
            }
        }

        /// <summary>
        /// ��ʾ���ﻼ���б�
        /// </summary>
        /// <param name="al"></param>
        private void TreeViewAddClincPatients(ArrayList al)
        {
            this.TreeViewAddClincPatients(al, false, false);
        }

        /// <summary>
        /// ��ʾ���ﻼ���б�
        /// </summary>
        /// <param name="al"></param>
        /// <param name="isDeptLimited">�Ƿ����������</param>
        /// <param name="isTimeLimited">�Ƿ���ʱ��������</param>
        private void TreeViewAddClincPatients(ArrayList al, bool isDeptLimited, bool isTimeLimited)
        {
            this.tvPatientInfo.Nodes.Clear();
            //��Ӹ�

            System.Windows.Forms.TreeNode node = new TreeNode();
            node.Text = "�����б�--����[����ʱ��]�Һſ���";
            node.Tag = "root";
            node.ImageIndex = 1;
            this.tvPatientInfo.Nodes.Add(node);

            //״̬

            this.TreeViewType = enumTreeViewType.PatientInfo;

            //��ӻ����ӽ��
            foreach (Neusoft.HISFC.Models.Registration.Register reg in al)
            {
                if (isDeptLimited && reg.DoctorInfo.Templet.Dept.ID != this.User.Dept.ID)
                {
                    continue;
                }
                if (isTimeLimited && (reg.DoctorInfo.SeeDate.CompareTo(this.dtBegin.Value) < 0 || reg.DoctorInfo.SeeDate.CompareTo(this.dtEnd.Value) > 0))
                {
                    continue;
                }
                System.Windows.Forms.TreeNode patientnode = new TreeNode();
                Neusoft.HISFC.Models.RADT.Patient patient = patient = reg as Neusoft.HISFC.Models.RADT.Patient;
                patient.User01 = reg.DoctorInfo.Templet.Dept.ID;

                patientnode.Tag = patient;
                patientnode.Text = ((Neusoft.HISFC.Models.RADT.Patient)reg).Name + "[" + reg.DoctorInfo.SeeDate.ToShortDateString() + "]" + reg.DoctorInfo.Templet.Dept.Name;
                patientnode.ImageIndex = 4;
                patientnode.SelectedImageIndex = 2;
                this.tvPatientInfo.Nodes[0].Nodes.Add(patientnode);
            }
            this.tvPatientInfo.ExpandAll();
        }

        /// <summary>
        /// ��ʾסԺ�����б�
        /// </summary>
        /// <param name="al"></param>
        private void TreeViewAddInpatients(ArrayList al)
        {
            this.AddTreeViewInpatients(al, false, false);
        }

        /// <summary>
        /// ��ʾסԺ�����б�
        /// </summary>
        /// <param name="al"></param>
        /// <param name="isDeptLimited">�Ƿ����������</param>
        /// <param name="isTimeLimited">�Ƿ���ʱ��������</param>
        private void AddTreeViewInpatients(ArrayList al, bool isDeptLimited, bool isTimeLimited)
        {
            this.tvPatientInfo.Nodes.Clear();
            //��Ӹ�

            System.Windows.Forms.TreeNode node = new TreeNode();
            node.Text = "�����б�--����[��Ժ����]��Ժ����";
            node.Tag = "root";
            node.ImageIndex = 1;
            this.tvPatientInfo.Nodes.Add(node);

            //״̬

            this.TreeViewType = enumTreeViewType.PatientInfo;
            foreach (Neusoft.HISFC.Models.RADT.PatientInfo info in al)
            {
                if (isDeptLimited && info.PVisit.PatientLocation.Dept.ID != this.User.Dept.ID)
                {
                    continue;
                }
                if (isTimeLimited && (info.PVisit.InTime.CompareTo(this.dtBegin.Value) < 0 || info.PVisit.InTime.CompareTo(this.dtEnd.Value) > 0))
                {
                    continue;
                }
                System.Windows.Forms.TreeNode patientnode = new TreeNode();

                info.User01 = info.PVisit.PatientLocation.Dept.ID;
                if (info.User01 == null || info.User01 == "")
                {
                    info.User01 = this.User.Dept.ID;
                }
                patientnode.Tag = info;
                string id = "";
                if (info.ID != null && info.ID.Length > 8)
                {
                    id = info.ID.Remove(0, 4);
                }
                patientnode.Text = info.Name + "[" + info.PVisit.InTime.ToShortDateString() + "]" + info.PVisit.PatientLocation.Dept.Name;
                patientnode.ImageIndex = 4;
                patientnode.SelectedImageIndex = 2;
                this.tvPatientInfo.Nodes[0].Nodes.Add(patientnode);
            }
            this.tvPatientInfo.ExpandAll();
        }

        #endregion

        #region ��ʾ��������Ϣ
        
        /// <summary>
        /// ��������Ϣ
        /// </summary>
        /// <param name="al"></param>
        private void TreeViewAddFeedBack(ArrayList al)
        {
            System.Windows.Forms.TreeNode node = new TreeNode();
            this.treeViewType = enumTreeViewType.FeedBackInfo;
            int imagindex = 1;

            node.Text = "������";
            node.Tag = "root";
            node.ImageIndex = 0;
            this.tvPatientInfo.Nodes.Add(node);
            this.tvPatientInfo.ExpandAll();

        }

        #endregion
        
        #endregion

        #region �����¼�

        #region �������Ų�ѯ������Ϣ-txtClinicNO

        /// <summary>
        /// ��������Ĳ����Ż�û�����Ϣ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtClinicNO_KeyDown (object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.dataShowList == null)
                {
                    this.dataShowList = new ListBox();
                    this.dataShowList.Dock = DockStyle.Fill;
                }

                this.txtClinicNO.Text = this.txtClinicNO.Text.Trim().PadLeft( 10, '0' );

                if (this.PatientType == Neusoft.HISFC.DCP.Enum.PatientType.I)          //סԺ����
                {                 
                    ArrayList alAllPatient = this.patientProcess.GetPatientInfoByPatientNOAll( this.txtClinicNO.Text );
                    if (alAllPatient == null)
                    {
                        MessageBox.Show( "����סԺ�Ż�ȡסԺ������Ϣ��������" + this.patientProcess.ErrorMsg );
                        return;
                    }

                    foreach (Neusoft.HISFC.Models.RADT.PatientInfo info in alAllPatient)
                    {
                        if (info.PVisit.InState.ID.ToString() == "I")           //��Ժ״̬
                        {
                            ArrayList alTemp = new ArrayList();
                            alTemp.Add( info );

                            this.Patient = info;

                            this.ShowPatienInfo( info );
                            this.cbxDeadDate.Checked = true;

                            this.TreeViewAddInpatients( alTemp );
                            break;
                        }
                    }
                }
                else if (this.PatientType == Neusoft.HISFC.DCP.Enum.PatientType.C) //���ﻼ��
                {

                }
                else
                {
                    MessageBox.Show( "�޴��˵���Ϣ" );
                    this.ClearAll();
                    return;
                }
            }
        }

        #endregion

        #region ������-tvPatientInfo

        /// <summary>
        /// ����������-ѡ�����ڵ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvPatientInfo_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //���ڵ�tagΪroot
            if (this.tvPatientInfo.SelectedNode.Tag.ToString() == "root")
            {
                this.ClearAll(true);
                return;
            }

            //״̬ΪAʱ���ص��ǻ����б� Bʱ���ص��Ǳ����б�
            this.cmbProfession.SelectedValueChanged -= new System.EventHandler(cmbProfession_SelectedValueChanged);

            if (this.TreeViewType == enumTreeViewType.PatientInfo)
            {
                this.Patient = this.tvPatientInfo.SelectedNode.Tag as Neusoft.HISFC.Models.RADT.Patient;
                this.cbxDeadDate.Checked = true;

                ShowPatienInfo(this.Patient);
            }
            else if (this.treeViewType == enumTreeViewType.ReportInfo)
            {
                Neusoft.HISFC.DCP.Object.CommonReport report = this.tvPatientInfo.SelectedNode.Tag as Neusoft.HISFC.DCP.Object.CommonReport;
                //��ʾ����
                this.IsNeedAdditionMeg = false;
                this.ShowReportData(report);
                this.IsNeedAdditionMeg = true;
            }

            this.cmbProfession.SelectedValueChanged += new EventHandler(cmbProfession_SelectedValueChanged);
        }

        #endregion        

        #region ���濨��ѯ

        #region ��ѯ���͸ı�-cmbQueryContent

        /// <summary>
        /// ��ѯ���ͷ����ı�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbQueryContent_SelectedValueChanged(object sender, EventArgs e)
        {
            //this.ucReportRegister1.InfectCode = "";

            if (this.cmbQueryContent.SelectedValue.ToString() == "PatientInfo" )
            {
                //���߻�����Ϣ��ѯ

                this.dtBegin.Enabled = false;
                this.dtEnd.Enabled = false;
                this.groupBox3.Enabled = true;
                this.tvPatientInfo.Nodes.Clear();
                this.txtReportNo.Enabled = false;
                this.txtDoctor.Enabled = false;
                this.txtName.Enabled = true;
                this.txtInPatienNo.Enabled = true;
                this.SetEnablellb(this.PatientType);
            }
            else if (this.cmbQueryContent.SelectedValue.ToString() == "ReportInfo")
            {
                //���߱�����Ϣ��ѯ

                this.dtBegin.Enabled = true;
                this.dtEnd.Enabled = true;
                this.groupBox3.Enabled = true;
                this.tvPatientInfo.Nodes.Clear();
                this.txtReportNo.Enabled = true;
                this.txtDoctor.Enabled = true;
                this.txtInPatienNo.Enabled = true;
                this.txtName.Enabled = true;
                this.llbPatienNO.Enabled = false;
            }
            else if (this.cmbQueryContent.SelectedValue.ToString() == "DeptReport"
                || this.cmbQueryContent.SelectedValue.ToString() == "DeptUnReport")
            {
                //���ұ�����Ϣ��ѯ
                this.dtBegin.Enabled = true;
                this.dtEnd.Enabled = true;
                this.groupBox3.Enabled = true;
                this.txtDoctor.Enabled = false;
                this.txtInPatienNo.Enabled = false;
                this.txtName.Enabled = false;
                this.txtReportNo.Enabled = false;
                this.tvPatientInfo.Nodes.Clear();
                //this.mySearchOldReport();
            }
            else if (this.cmbQueryContent.SelectedValue.ToString() == "choose")
            {
                this.groupBox3.Enabled = true;
                this.txtDoctor.Enabled = false;
                this.txtInPatienNo.Enabled = false;
                this.txtName.Enabled = false;
                this.txtReportNo.Enabled = false;
                this.dtBegin.Enabled = false;
                this.dtEnd.Enabled = false;
                this.tvPatientInfo.Nodes.Clear();
                //this.mySearchOldReport();
            }
            else if (this.cmbQueryContent.SelectedValue.ToString() == "deptLisResult")
            {
                this.groupBox3.Enabled = true;
                this.txtDoctor.Enabled = false;
                this.txtInPatienNo.Enabled = false;
                this.txtName.Enabled = false;
                this.txtReportNo.Enabled = false;
                this.dtBegin.Enabled = false;
                this.dtEnd.Enabled = false;
                this.tvPatientInfo.Nodes.Clear();
            }
        }

        #endregion

        #region �������Ų�ѯ���濨-txtReportNo

        /// <summary>
        /// ��������������Ų�ѯ���濨
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtReportNo_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                //���濨�����Ų�ѯ

                //this.ucReportRegister1.InfectCode = "";

                this.QueryByReportNO();
            }
        }

        /// <summary>
        /// ���濨�����Ų�ѯ
        /// </summary>
        private void QueryByReportNO()
        {
            //���濨�����Ų�ѯ

            this.tvPatientInfo.Nodes.Clear();
            ArrayList al = new ArrayList();
            Neusoft.HISFC.DCP.Object.CommonReport report = new Neusoft.HISFC.DCP.Object.CommonReport();
            report = this.diseaseMgr.GetCommonReportByNO(this.txtReportNo.Text);
            if (report != null)
            {
                if (report.ReportTime > this.dtBegin.Value && report.ReportTime < this.dtEnd.Value)
                {
                    al.Add( report );
                }

                this.TreeViewAddReports( al );
            }
        }

        #endregion

        #region �����ߺŲ�ѯ-txtInPatienNo

        /// <summary>
        /// ���ߺŲ�ѯ
        /// </summary>
        private void QueryByPatientNO()
        {
            //���ߺŲ�ѯ
            ArrayList al = new ArrayList();
            if (this.txtInPatienNo.Text.Trim() == "")
            {
                return;
            }
            string patientno = this.txtInPatienNo.Text.Trim().PadLeft(10, '0');

            al = this.diseaseMgr.GetCommonReportListByPatientNO( patientno );
            if (al == null)
            {
                MessageBox.Show( "���ݻ���ID��ѯ���߱�����Ϣʱ��������" + this.diseaseMgr.Err );
                return;
            }
            else if (al.Count > 0)
            {
                this.TreeViewAddReports( al );
            }
            else             //�ޱ�����Ϣ ��ʾ������Ϣ
            {
                #region �ޱ�����Ϣʱ��ʾ������Ϣ

                //סԺ����
                if (this.LlbPatientType == Neusoft.HISFC.DCP.Enum.PatientType.I)
                {
                    al = this.patientProcess.GetPatientInfoByPatientNOAll( patientno );
                }
                //���ﻼ��
                else if (this.LlbPatientType == Neusoft.HISFC.DCP.Enum.PatientType.C)
                {
                    al = this.patientProcess.Query( patientno, DateTime.Now.AddYears( -1000 ) );
                }

                Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                if (al == null || al.Count == 0)
                {
                    MessageBox.Show( this, "û�л�����Ϣ��", "��ʾ>>" );
                    this.tvPatientInfo.Nodes.Clear();
                    return;
                }
                //סԺ����
                if (this.LlbPatientType == Neusoft.HISFC.DCP.Enum.PatientType.I)
                {
                    this.TreeViewAddInpatients( al );
                }
                //���ﻼ��
                else if (this.LlbPatientType == Neusoft.HISFC.DCP.Enum.PatientType.C)
                {
                    this.TreeViewAddClincPatients( al );
                }

                #endregion
            }            
        }

        #endregion

        #region ��������ת��-llbPatientNO

        /// <summary>
        /// ��������ת��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void llbPatienNO_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.LlbPatientType == Neusoft.HISFC.DCP.Enum.PatientType.I)
            {
                this.llbPatienNO.Text = "�� �� ��";
                this.lbPatientName.Text = "���ﻼ��";
                this.LlbPatientType = Neusoft.HISFC.DCP.Enum.PatientType.C;
            }
            else
            {
                this.llbPatienNO.Text = "ס Ժ ��";
                this.lbPatientName.Text = "סԺ����";
                this.LlbPatientType = Neusoft.HISFC.DCP.Enum.PatientType.I;
            }
        }

        #endregion

        #region ������ģ����ѯ

        /// <summary>
        /// ����������ѯ
        /// </summary>
        private void QueryByPatientName()
        {
            ArrayList al = new ArrayList();
            if (this.txtName.Text.Trim() == "")
            {
                return;
            }
            string patientName = this.txtName.Text.Trim();

            al = this.diseaseMgr.GetReportListByPatientName( patientName );
            if (al == null)
            {
                MessageBox.Show( "���ݻ��������������濨��Ϣʱ��������" + this.diseaseMgr.Err );
                return;
            }
            else if (al.Count > 0)
            {
                this.TreeViewAddReports( al );
            }
            else            //�ޱ��濨ʱ��ʾ������Ϣ
            {
                //סԺ����
                if (this.LlbPatientType == Neusoft.HISFC.DCP.Enum.PatientType.I)
                {
                    al = this.patientProcess.GetPatientInfoByPatientName( patientName );
                }
                //���ﻼ��
                else if (this.LlbPatientType == Neusoft.HISFC.DCP.Enum.PatientType.C)
                {
                    al = this.patientProcess.QueryValidPatientsByName( patientName );
                }

                if (al == null || al.Count == 0)
                {
                    MessageBox.Show( this, "û�л�����Ϣ��", "��ʾ>>" );
                    this.tvPatientInfo.Nodes.Clear();
                    return;
                }
                if (this.LlbPatientType == Neusoft.HISFC.DCP.Enum.PatientType.I)
                {
                    this.TreeViewAddInpatients( al );
                }
                //���ﻼ��
                else if (this.LlbPatientType == Neusoft.HISFC.DCP.Enum.PatientType.C)
                {
                    TreeViewAddClincPatients( al );
                }
            }
        }
        /// <summary>
        /// ����סԺ���Ҳ��һ���
        /// </summary>
        private void QueryPatientByDeptIN()
        {
            ArrayList al = new ArrayList();
            Neusoft.HISFC.Models.RADT.InStateEnumService instate = new Neusoft.HISFC.Models.RADT.InStateEnumService();
            instate.ID = "I";
            //סԺ����
            if (this.LlbPatientType == Neusoft.HISFC.DCP.Enum.PatientType.I)
            {
                al = this.patientProcess.QueryPatientByDeptCode(((Neusoft.HISFC.Models.Base.Employee)Neusoft.FrameWork.Management.Connection.Operator).Dept.ID, instate);
            }

            if (al == null || al.Count == 0)
            {
                MessageBox.Show(this, "û�л�����Ϣ��", "��ʾ>>");
                this.tvPatientInfo.Nodes.Clear();
                return;
            }

            if (this.LlbPatientType == Neusoft.HISFC.DCP.Enum.PatientType.I)
            {
                this.TreeViewAddInpatients(al);
            }
        }
        /// <summary>
        /// ����ҽ����ѯ���ﲡ�� --�޸�
        /// </summary>
        private void QueryPatientByDco()
        {
            ArrayList al = new ArrayList();
           
            if (this.LlbPatientType == Neusoft.HISFC.DCP.Enum.PatientType.C)
            {
                al = this.patientProcess.QueryBySeeDoc(((Neusoft.HISFC.Models.Base.Employee)Neusoft.FrameWork.Management.Connection.Operator).ID, Neusoft.FrameWork.Function.NConvert.ToDateTime(this.diseaseMgr.GetSysDateTime()).AddDays(-3), Neusoft.FrameWork.Function.NConvert.ToDateTime(this.diseaseMgr.GetSysDateTime()).Date.AddDays(1),true);
            }

            if (al == null || al.Count == 0)
            {
                MessageBox.Show(this, "û�л�����Ϣ��", "��ʾ>>");
                this.tvPatientInfo.Nodes.Clear();
                return;
            }
            if (this.LlbPatientType == Neusoft.HISFC.DCP.Enum.PatientType.C)
            {
                #region {6949963F-0A35-405a-9A44-4D5CBB53FFA2} ���� �޸����� 2011-5-25
               
                //this.TreeViewAddInpatients(al);

                //����ҽ��վ �����߲�ѯ��ʱ�� Ӧ����ʾ���ﻼ��
                this.TreeViewAddClincPatients(al);

                #endregion
            }
        }

        #endregion

        #region ����ҽ�����Ų�ѯ��ҽ������д����-txtDoctor

        /// <summary>
        /// ���ݱ���ҽ����ѯ���濨
        /// </summary>
        private void QueryByDoctorNO()
        {
            this.tvPatientInfo.Nodes.Clear();
            if (this.txtDoctor.Text.Trim() == "")
            {
                return;
            }
            ArrayList al = new ArrayList();
            foreach (Neusoft.HISFC.DCP.Enum.ReportState s in Enum.GetValues(typeof(Neusoft.HISFC.DCP.Enum.ReportState)))
            {
                al.AddRange(this.diseaseMgr.GetReportListByStateAndDoctor(Function.ConvertState(s), this.txtDoctor.Text.Trim().PadLeft(6, '0')));
            }

            this.TreeViewAddReports( al );
        }

        #endregion

        #endregion

        #region ���濨��д����

        #region ��������-dtBirthDay

        /// <summary>
        /// ѡ���������ʱ�ı�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtBirthDay_ValueChanged(object sender, System.EventArgs e)
        {
            string age = this.diseaseMgr.GetAge(this.dtBirthDay.Value, this.diseaseMgr.GetDateTimeFromSysDateTime()).Trim();
            try
            {
                int length = age.Length;
                //ɾ�����䵥λ
                this.txtAge.Text = age.Substring(0, length - 1);                
                if (age.Substring(length - 1) == "��")
                {
                    this.rdbYear.Checked = true;
                }
                else if (age.Substring(length - 1) == "��")
                {
                    this.rdbMonth.Checked = true;
                }
                else
                {
                    this.rdbDay.Checked = true;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(Neusoft.FrameWork.Management.Language.Msg(ex.Message));
            }
        }

        #endregion

        #region ְҵѡ��-cmbProfession

        /// <summary>
        /// ְҵѡ����ʾ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbProfession_SelectedValueChanged(object sender, System.EventArgs e)
        {
            //���׶�ͯ��ѧ��
            if (this.hshStudent.Contains(this.cmbProfession.SelectedValue.ToString()))
            {
                this.MyMessageBox("��ע����" + "\"" + "������λ��" + "\"" + "��д" + "\"" + this.hshStudent[this.cmbProfession.SelectedValue.ToString()].ToString() + "\"", "��ʾ");
            }
        }

        #endregion              

        #region ����ѡ��

        private void cbxHomeAearOne_Click(object sender, System.EventArgs e)
        {
            if (this.cbxHomeAearOne.Checked)
                this.SetHomeArea(1);
        }

        private void cbxHomeAearTwo_Click(object sender, System.EventArgs e)
        {
            if (this.cbxHomeAearTwo.Checked)
                this.SetHomeArea(2);
        }

        private void cbxHomeAearThree_Click(object sender, System.EventArgs e)
        {
            if (this.cbxHomeAearThree.Checked)
                this.SetHomeArea(3);
        }

        private void cbxHomeAearFour_Click(object sender, System.EventArgs e)
        {
            if (this.cbxHomeAearFour.Checked)
                this.SetHomeArea(4);
        }

        private void cbxHomeAearFive_Click(object sender, System.EventArgs e)
        {
            if (this.cbxHomeAearFive.Checked)
                this.SetHomeArea(5);
        }

        private void cbxHomeAearSix_Click(object sender, System.EventArgs e)
        {
            if (this.cbxHomeAearSix.Checked)
                this.SetHomeArea(6);
        }

        #endregion

        #region ��ַѡ��

        private void txtHomeAddress_Enter(object sender, System.EventArgs e)
        {
            //this.txtHomeAdd.Text = this.cmbprovince.Text + this.cmbCity.Text + this.cmbCouty.Text + this.cmbTown.Text;
        }

        #endregion

        #region ����ѡ��

        #region ��ʾ��Ϣ

        private void ShowMessageAfterSelect(string diseaseId)
        {
            string msg = "";
            if (diseaseId == "3001")
            {
                msg = @"���������������������Ժר������ﲻ�����Ϊ���������ĵķ��ײ��������ϱ�Ϊ������ԭ����ס���
����ԭ����׶��壺

�ٷ��ȣ�Ҹ�����¡�38�棩
�ھ��з��׵�Ӱ��ѧ����

�۷������ڰ�ϸ���������ͻ����������ܰ�ϸ�������������

�ܾ��淶����ҩ������3~5�죨�����л�ҽѧ�������ѧ�ֻ�䲼��2006�桰��������Է�����Ϻ�����ָ�ϡ����������2�������������Ը��ƻ�ʽ����Լ���

";

            }

            else if (diseaseId == "3003")
            {
                msg = @"�������¼��Գڻ�����ԣ�AFP���������壬����б��档

AFP���壺����15�����³��ּ��Գڻ������֢״�Ĳ��������κ������ٴ����Ϊ���ҵĲ�������Ϊ���Գڻ�����ԣ�AFP��������

AFP���������Ҫ�㣺�����𲡡������������������½����췴���������ʧ��

������AFP�����������¼�����

��1����������ף�

��2�����ְ����ۺ�������Ⱦ�Զ෢���񾭸����ף�GBS����
��3������Լ����ס������ס��Լ����ס������񾭸������ף�
��4�����񾭲���ҩ���Զ��񾭲����ж���������Ķ��񾭲���ԭ�����Զ��񾭲�����

��5���񾭸��ף�
��6�����������ף������μ�ҩ��ע������������ף���
��7�������ף�
��8���񾭴��ף�
��9����������ԣ������ͼ�����ԡ��߼�����ԡ�����������ԣ���

��10������������ȫ������֢���������ж��ԡ�ԭ�����Լ�������

��11�����Զ෢�Լ��ף�
��12���ⶾ�ж���
��13����̱֫����̱�͵�̱��ԭ��������

��14��������֫����ԡ�

";
            }

            if (msg != "")
            {
                MessageBox.Show(msg, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ShowMessageAfterSave(string diseaseId)
        {
            string msg = "";
            if (diseaseId == "3003")
            {
                msg = @"������AFP����Ӧ�ɼ�˫�ݴ��걾���ڲ�������

�ű걾�Ĳɼ�Ҫ���ǣ�����Գ��ֺ�14���ڲɼ������ݱ걾�ɼ�ʱ�����ټ��24Сʱ��ÿ�ݱ걾������5�ˣ�ԼΪ���˵Ĵ�Ĵָĩ�ڴ�С�����걾�ɼ�Ӧ��д�������ͼ쵥����

�Ʋ��������������ͼ쵥�������˵�����Ԥ������ȡ��

�Ǳ걾�ͼ�ص㣺�걾�ɼ����֪ͨԽ��������Ԥ���������ģ�������ǰ����ȡ��

";
            }
            if (msg != "")
            {
                MessageBox.Show(msg, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion

        /// <summary>
        /// ѡ�񼲲�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbInfectionClass_Enter(object sender, EventArgs e)
        {
            if (this.infectCode == null || this.infectCode == "")
            {
                this.cmbInfectionClass.Enabled = true;
                return;
            }
            string[] infectCodes = this.infectCode.Split(',');
            if (infectCodes.Length > 1)
            {
                ArrayList alTmp = new ArrayList();
                this.cmbInfectionClass.Enabled = true;
                foreach (string code in infectCodes)
                {
                    foreach (Neusoft.HISFC.Models.Base.Const disease in this.alInfectItem)
                    {
                        if (code == disease.ID)
                        {
                            alTmp.Add(disease);
                            break;
                        }
                    }
                }
                Neusoft.FrameWork.Models.NeuObject ob = new Neusoft.HISFC.Models.Base.Const();
                Neusoft.FrameWork.WinForms.Classes.Function.ChooseItem(alTmp, ref ob);
                this.cmbInfectionClass.SelectedValue = ob.ID;

                //��������
                this.cmbCaseClaseTwo.Enabled = this.hshNeedCaseTwo.Contains(ob.ID);
                this.cmbInfectionClass.Enabled = true;
            }
        }

        /// <summary>
        /// ѡ�񼲲����ж��Ƿ���Ҫ�������Ƿ���Ҫ��д��������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbInfectionClass_SelectedValueChanged(object sender, System.EventArgs e)
        {   
            //xiwx
            if (this.cmbInfectionClass.Tag == null || this.cmbInfectionClass.Tag.ToString() == "####")
            {
                return;
            }
            
            string strtempid  = this.cmbInfectionClass.Tag.ToString();
            
            if (this.hshNeedMemo.Contains(strtempid))
            {
                this.MyMessageBox("���ڱ�ע����д��������", "��ʾ>>");
            }
            
            //�ж��Ƿ���Ҫ����
            if (this.tcReport.TabPages.ContainsKey("tpAddition"))
            {
                //��ɾ�����ҽ���־��ΪFALSE
                this.tcReport.TabPages.RemoveByKey("tpAddition");
                this.IsNeedAdd = false;
            }
            this.IsNeedAddition(strtempid);

            this.ShowMessageAfterSelect(strtempid);

            //��������
            this.cmbCaseClaseTwo.Enabled = this.hshNeedCaseTwo.Contains(strtempid);
            this.cmbCaseClaseTwo.TabStop = this.cmbCaseClaseTwo.Enabled;
        }

        #endregion

        #region �س���ת

        /// <summary>
        /// �س���ת
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                System.Windows.Forms.SendKeys.Send("{tab}");
                return base.ProcessCmdKey(ref msg, keyData);
            }
            return false;
        }
    
        #endregion

        #region �����л�-tcReport

        ///// <summary>
        ///// ѡ��ѡ�ʱ���������ڸ�����Ϣ��������������л�Tabҳ
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void tcReport_Selecting(object sender, TabControlCancelEventArgs e)
        //{
        //    if (this.tcReport.TabPages.ContainsKey("tpAddition") && this.tcReport.TabPages["tpAddition"].Focus())
        //    {
        //        if (this.tcReport.SelectedIndex != 1)
        //        {
        //            if (this.JudgeAdditionInfo() == -1)
        //            {
        //                return;
        //            }
        //        }
        //    }
        //}

        #endregion

        #endregion

        #endregion

        #region ����

        /// <summary>
        /// ��ȷ��ѯ������Ϣ
        /// </summary>
        /// <param name="patientNO"></param>
        /// <returns></returns>
        private Neusoft.HISFC.Models.RADT.Patient GetPatientInfo(string patientNO)
        {
            Neusoft.HISFC.BizProcess.Integrate.RADT radtItg = new Neusoft.HISFC.BizProcess.Integrate.RADT();

            return radtItg.QueryComPatientInfo(patientNO);
        }

        /// <summary>
        /// ������ʷ����[�������� ɾ�� ���ϱ���������ʷ��ť����ʾ����д�����б�]
        /// </summary>
        private void QueryOldReport()
        {
            if (this.PatientType == Neusoft.HISFC.DCP.Enum.PatientType.O)//������Ⱦ��
            {
                this.QueryHospitalReport();
            }
            else
            {
                this.QueryDeptReport();
            }
        }
 
        /// <summary>
        /// ���ñ�����Ϣ��״̬
        /// </summary>
        /// <param name="report"></param>
        /// <param name="reportState"></param>
        private void UpdateReportState(Neusoft.HISFC.DCP.Object.CommonReport report, Neusoft.HISFC.DCP.Enum.ReportState reportState)
        {
            System.DateTime now = this.diseaseMgr.GetDateTimeFromSysDateTime();

            //������Ϣ
            report.Oper.ID = this.User.ID;
            report.OperDept.ID = this.User.Dept.ID;
            report.OperTime = now;// 

            //״̬�仯�󷵻� �ڸ����ڼ��������˲���

            if (!this.CheckState(report.ID, report.State))
            {
                return;
            }
            //�µ�״̬

            report.State = Function.ConvertState(reportState);

            //�������ݿ�;

            if (this.diseaseMgr.UpdateCommonReport(report) != -1)
            {
                this.MyMessageBox("�����ɹ���", "��ʾ>>");
                this.ClearAll();
            }
            else
            {
                this.MyMessageBox("����ʧ�ܣ�" + this.diseaseMgr.Err, "err");
            }

            #region

            //�����ʱ��ˢ��
            this.QueryOldReport();

            #endregion
        }

        /// <summary>
        /// ѡ����ʾ��������ڷ�Χ
        /// </summary>
        public void ChanageTime()
        {
            //ѡ��ʱ��Σ����û��ѡ��ͷ���
            DateTime dtbegin = this.diseaseMgr.GetDateTimeFromSysDateTime().Date;
            DateTime dtend = dtbegin;
            if (Neusoft.FrameWork.WinForms.Classes.Function.ChooseDate(ref dtbegin, ref dtend) == 0) return;
            this.BeginTime = dtbegin.ToString();
            this.EndTime = dtend.ToString();
        }   

        /// <summary>
        /// ���ҷ�����Ϣ
        /// </summary>
        //private void QueryFeedBackByDept()
        //{
        //    ArrayList al = new ArrayList();//this.diseaseMgr.GetFeedBackByDoctAndDept(this.User.ID,this.User.Dept.ID);
        //    if (al.Count == 0)
        //    {
        //        MessageBox.Show(this, "û�п��ҷ�����Ϣ", "��ʾ>>");
        //        return;
        //    }
        //    Neusoft.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڼ��ر����ҷ�����Ϣ");
        //    Application.DoEvents();
        //    this.TreeViewAddFeedBack(al);
        //    Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
        //}
       
        /// <summary>
        /// ��ʾ����
        /// </summary>
        /// <param name="information"></param>
        /// <returns></returns>
        private bool IsContinue(string information)
        {
            if (MessageBox.Show(this, information, "��ܰ��ʾ>>", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Information,System.Windows.Forms.MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.No)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// ��ȡ���濨-ѡ�������
        /// </summary>
        /// <returns></returns>
        private Neusoft.HISFC.DCP.Object.CommonReport GetSelectedReport()
        {
            Neusoft.HISFC.DCP.Object.CommonReport report = null;
            if (this.tvPatientInfo.SelectedNode != null && this.tvPatientInfo.SelectedNode.Tag != null && this.tvPatientInfo.SelectedNode.Tag is Neusoft.HISFC.DCP.Object.CommonReport)
            {
                report = this.tvPatientInfo.SelectedNode.Tag as Neusoft.HISFC.DCP.Object.CommonReport;
            }
            return report;
        }

        /// <summary>
        /// ���ͷ����Ϣ
        /// </summary>
        private void ClearHeadInfo()
        { 
            txtClinicNO.Text = string.Empty;
            lbID.Text = string.Empty;
            lbState.Text = string.Empty;
        }

        #region ����[�¼ӻ����޸�]

        /// <summary>
        /// ����,����,�޸�
        /// </summary>
        /// <returns>-1ʧ��</returns>
        public int SaveRepot()
        {
            #region ������Ϣ��ȡ����֤

            Neusoft.HISFC.DCP.Object.CommonReport report = this.GetSelectedReport();
            if (report == null || string.IsNullOrEmpty(report.ID))
            {
                report = new Neusoft.HISFC.DCP.Object.CommonReport();
            }

            if (this.AuthenticationInfo(ref report) == -1)
            {
                return -1;
            }
            #endregion

            #region �û�ȷ��

            if (cmbInfectionClass.Enabled == true
                && MessageBox.Show(this, "��ѡ���ˡ�" + report.Disease.Name + "��\n�������ӱ�����ϵͳ�Զ��ϴ�������Ԥ���ơ�\nȷ�ϱ�����", "��ܰ��ʾ>>", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Information)
                == System.Windows.Forms.DialogResult.No)
            {
                return -1;
            }

            #endregion

            #region ���ݱ���

            if (this.hshNeedBill.Contains(report.Disease.ID))
            {
                if (report.Memo != string.Empty)
                {
                    if (report.Memo.IndexOf("��ת��") == -1)
                    {
                        report.Memo = "��ת��\\\\" + report.Memo;
                    }
                }
                else
                {
                    report.Memo = "��ת��\\\\";
                }
            }

            Neusoft.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڱ���,���Ժ�....");
            Application.DoEvents();

            //������Ϊ�� ��Ϊ�¿��������ݿ�
            
            Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();            

            this.diseaseMgr.SetTrans(Neusoft.FrameWork.Management.PublicTrans.Trans);

            if (string.IsNullOrEmpty( report.ID ) == true && string.IsNullOrEmpty(report.ReportNO) == true)
            {
                #region �¿����봦��

                //����Ƕ��� ��Ҫ����ԭ��
                if (this.diseaseMgr.InsertCommonReport(report) == -1)
                {
                    Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    Neusoft.FrameWork.Management.PublicTrans.RollBack();
                    report.ID = string.Empty;
                    report.ReportNO = string.Empty;
                    this.MyMessageBox("���濨����ʧ��>>" + this.diseaseMgr.Err, "err");
                    return -1;
                }

                //��������
                if (IsNeedAdd)
                {
                    if(this.UpdateAdditionInfo(this.operType, report)==-1)
                    {
                        Neusoft.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(Neusoft.FrameWork.Management.Language.Msg("�洢������Ϣʧ��"));
                        return -1;
                    }
                }

                #endregion
            }
            else
            {
                #region �ɿ����´���
                Neusoft.HISFC.DCP.Object.CommonReport mainreport = this.GetSelectedReport();//ԭ��
                report.ID = ((Neusoft.HISFC.DCP.Object.CommonReport)this.tvPatientInfo.SelectedNode.Tag).ID;
                report.ReportNO = ((Neusoft.HISFC.DCP.Object.CommonReport)this.tvPatientInfo.SelectedNode.Tag).ReportNO;
                report.CorrectFlag = mainreport.CorrectFlag;
                report.CorrectReportNO = mainreport.CorrectReportNO;
                report.CorrectedReportNO = mainreport.CorrectedReportNO;
                report.ExtendInfo3 = mainreport.ExtendInfo3;
                if (this.diseaseMgr.UpdateCommonReport(report.Clone()) == -1)
                {
                    Neusoft.FrameWork.Management.PublicTrans.RollBack();
                    Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    this.MyMessageBox("���濨����ʧ��>>" + this.diseaseMgr.Err, "err");
                    return -1;
                }
                //��������
                if (IsNeedAdd)
                {
                    this.UpdateAdditionInfo(this.operType, report.Clone());
                }

                #endregion
            }

            Neusoft.FrameWork.Management.PublicTrans.Commit();
            Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
            if (this.patient != null && this.type != Neusoft.HISFC.DCP.Enum.PatientType.O && this.infectCode != "")
            {
                this.infectCode = "";
                this.reportOperResult = Neusoft.HISFC.DCP.Enum.ReportOperResult.OK;
            }
            #endregion

            #region ������ʾ��Ϣ

            this.GetMessage(report);

            #endregion

            return 0;
        }

        /// <summary>
        /// ������ʾ��Ϣ
        /// </summary>
        /// <param name="report">�������</param>
        /// <returns>������Ϣ</returns>
        private void GetMessage(Neusoft.HISFC.DCP.Object.CommonReport report)
        {
            string message = "���濨�ɹ����沢�ϱ�!\n\n";
            string diseaseID = report.Disease.ID;

            if (this.hshNeedCheckedBlood.Contains(diseaseID))
            {
                message += "��֪ͨ��ʿ��Ѫ���걾��Խ����CDC�������IgM����\n";
            }

            //��ĩ�绰����

            ArrayList altemp = new ArrayList();
            altemp = this.commonProcess.QueryConstantList("SWITCH");
            string strtelephone = "";
            foreach (Neusoft.HISFC.Models.Base.Const conOb in altemp)
            {
                strtelephone += conOb.Memo + "\n";
            }

            //if (strtelephone == "") ȡ���ڼ����տ��ص����ȼ� 2011-3-9
            {
                //�绰֪ͨ
                if (this.hshNeedTelInfect.Contains(diseaseID))
                {
                    ArrayList al = new ArrayList();
                    al = this.commonProcess.QueryConstantList("MESSAGE");
                    foreach (Neusoft.HISFC.Models.Base.Const con in al)
                    {
                        message += con.Memo + "\n";
                    }
                }
            }
            if (message != "" && message != null)
            {
                MessageBox.Show(this, message, "��ʾ>>");
            }

            if (strtelephone != "")
            {
                MessageBox.Show(this, strtelephone, "��ܰ��ʾ>>", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            }
            int diseasecode = Neusoft.FrameWork.Function.NConvert.ToInt32(diseaseID);

            if (diseasecode == 1002 || diseasecode == 1003)
            {

            }
            if (diseasecode >= 1033 && diseasecode <= 1038)
            {
                this.MyMessageBox("�뼰ʱ������ת�����Բ����ġ�����", "�Բ���ڹ�����ʾ>>");
            }
            else if (diseasecode >= 7001 && diseasecode <= 7005)
            {
                this.MyMessageBox("�뼰ʱ������ת�����Բ����ġ�����", "�Բ���ڹ�����ʾ>>");
            }
            this.ShowMessageAfterSave(diseasecode.ToString());
        }


        /// <summary>
        /// ��ʾ����״̬�ı��濨
        /// </summary>
        /// <param name="al"></param>
        private void TreeViewAddReportsIgnorState(ArrayList al)
        {
            this.tvPatientInfo.Nodes.Clear();
            if (al == null || al.Count < 1)
            {
                return;
            }

            Array alState = Enum.GetValues(typeof(Neusoft.HISFC.DCP.Enum.ReportState));
            foreach (Neusoft.HISFC.DCP.Enum.ReportState reportState in alState)
            {
                ArrayList altemp = new ArrayList();
                foreach (Neusoft.HISFC.DCP.Object.CommonReport report in al)
                {
                    if (report.State == Function.ConvertState(reportState))
                    {
                        altemp.Add(report);
                    }
                }
                this.TreeViewAddReports(altemp);
            }
        }

        #endregion

        #region ɾ��

        /// <summary>
        /// ɾ�����濨
        /// </summary>
        /// <param name="ID">���</param>
        public int DeleteReport(string ID)
        {
            System.Windows.Forms.DialogResult dr = new DialogResult();
            dr = MessageBox.Show("ȷ��Ҫɾ�����濨��\nɾ�����ָܻ���", "��ʾ>>", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Warning, System.Windows.Forms.MessageBoxDefaultButton.Button2);

            if (dr == System.Windows.Forms.DialogResult.Yes)
            {
                int param = this.diseaseMgr.DeleteCommonReport( ID );
                if (param == 1)
                {
                    this.MyMessageBox("���濨ɾ���ɹ�!", "��ʾ>>");
                    return -1;                 
                }
                else if (param == 0)
                {
                    this.MyMessageBox( "���濨�Ѿ����޶������ �޷�����ɾ��", "��ʾ" );
                }
                else
                {
                    this.MyMessageBox( "���濨ɾ��ʧ��!" + this.diseaseMgr.Err, "����>>" );
                }

                return -1;
            }
            return 1;
        }

        /// <summary>
        /// ɾ������
        /// </summary>
        private void DeleteReport ()
        {
            if (this.TreeViewType == enumTreeViewType.PatientInfo || this.tvPatientInfo.SelectedNode == null)
            {
                return;
            }
            if (this.tvPatientInfo.SelectedNode.Tag.ToString() == "root")
            {
                return;
            }

            Neusoft.HISFC.DCP.Object.CommonReport report = new Neusoft.HISFC.DCP.Object.CommonReport();
            report = (Neusoft.HISFC.DCP.Object.CommonReport)this.tvPatientInfo.SelectedNode.Tag;
            if (report != null && report.ID != "")
            {
                if (this.DeleteReport( report.ID ) == 0)
                {
                    this.tvPatientInfo.Nodes.Remove( this.tvPatientInfo.SelectedNode );

                    //��ѯ�¼�
                    ArrayList alTempReport = new ArrayList();
                    foreach (ArrayList al in this.AlReport)
                    {
                        ArrayList altemp = new ArrayList();
                        foreach (Neusoft.HISFC.DCP.Object.CommonReport rpt in al)
                        {
                            if (rpt.ID != report.ID)
                            {
                                altemp.Add(rpt);
                            }
                        }
                        if (altemp != null && altemp.Count > 0)
                        {
                            alTempReport.Add(altemp);
                        }
                    }
                    this.AlReport = alTempReport;
                    this.ReflashTreeView(this.AlReport);
                }
            }
        }      

        #endregion

        #region ��ӡ

        /// <summary>
        /// ��ӡ-��Ҫ�ȱ���
        /// </summary>
        protected override int OnPrint(object sender, object neuObject)
        {
            if(string.IsNullOrEmpty(lbID.Text.Trim()))
            {
                return -1;
            }
            ucPrint uc = new ucPrint();
            Neusoft.FrameWork.WinForms.Classes.Print p = new Neusoft.FrameWork.WinForms.Classes.Print();
            uc.lbState.Text = this.lbState.Text;
            uc.lbID.Text = this.lbID.Text;
            uc.lbClinicNO.Text = this.txtClinicNO.Text;

            uc.lbPatientName.Text = this.txtPatientName.Text;
            uc.lbPatientParents.Text = this.txtPatientParents.Text;
            uc.lbPatientID.Text = this.txtPatientID.Text;
            uc.lbSex.Text = this.cmbSex.Text;
            uc.lbBirthday.Text = this.dtBirthDay.Value.ToShortDateString();
            uc.lbAge.Text = this.txtAge.Text;

            uc.rdbDay.Checked = this.rdbDay.Checked;
            uc.rdbMonth.Checked = this.rdbMonth.Checked;
            uc.rdbYear.Checked = this.rdbYear.Checked;

            uc.cbxHomeAearOne.Checked = this.cbxHomeAearOne.Checked;
            uc.cbxHomeAearTwo.Checked = this.cbxHomeAearTwo.Checked;
            uc.cbxHomeAearThree.Checked = this.cbxHomeAearThree.Checked;
            uc.cbxHomeAearFour.Checked = this.cbxHomeAearFour.Checked;
            uc.cbxHomeAearFive.Checked = this.cbxHomeAearFive.Checked;
            uc.cbxHomeAearSix.Checked = this.cbxHomeAearSix.Checked;

            uc.lbSpecificlAddress.Text = this.txtSpecialAddress.Text;
            uc.lbTelephone.Text = this.txtTelephone.Text;
            uc.lbProfession.Text = this.cmbProfession.Text;
            uc.lbWorkPlace.Text = this.txtWorkPlace.Text;

            uc.lbInfectionClass.Text = this.cmbInfectionClass.Text;
            uc.lbCaseClassOne.Text = this.cmbCaseClassOne.Text;
            uc.lbCaseClaseTwo.Text = this.cmbCaseClaseTwo.Text;

            uc.rdbInfectOtherYes.Checked = this.rdbInfectOtherYes.Checked;
            uc.rdbInfectionOtherNo.Checked = this.rdbInfectionOtherNo.Checked;

            uc.lbInfectionDate.Text = this.dtInfectionDate.Value.ToShortDateString();
            uc.lbDiaDate.Text = this.dtDiaDate.Value.ToString();

            //���⴦��
            if (!this.cbxDeadDate.Checked)
            {
                uc.lbDeadDate.Text = new DateTime(this.dtDeadDate.Value.Year, this.dtDeadDate.Value.Month,
                    this.dtDeadDate.Value.Day, 0, 0, 0).ToShortDateString();//��������
            }
            else
            {
                uc.lbDeadDate.Text = "";
            }          

            uc.neuSpread1_Sheet1.Cells[0,0].Text = this.rtxtMemo.Text;
            uc.lbCase.Text = this.txtCase.Text;
            uc.lbReportDoctor.Text = this.cmbReportDoctor.Text;
            uc.lbDoctorDept.Text = this.cmbDoctorDept.Text;
            uc.lbReportTime.Text = this.lbReportTime.Text;

            p.PrintPage(0, 30, uc);
            return base.OnPrint(sender, neuObject);
        } 
       
        #endregion

        #region ��Ϣ����

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
        /// �ж�״̬�Ƿ�仯����ʱ���������
        /// </summary>
        /// <param name="ID">���</param>
        /// <param name="state">״̬</param>
        /// <returns>true ״̬δ��</returns>
        private bool CheckState(string ID, string reportState)
        {
            string tempstate = "";
            try
            {
                Neusoft.HISFC.DCP.Object.CommonReport report = this.diseaseMgr.GetCommonReportByID(ID);
                tempstate = report.State;
            }
            catch (Exception ex)
            {
                this.MyMessageBox("��������ʱת�����濨״̬ʧ�ܣ�" + ex.Message, "err");
                return false;
            }
            if (reportState != tempstate)
            {
                if (reportState == "1" || tempstate == "0")
                {
                    //�޸ĺϸ�
                    return true;
                }
                this.MyMessageBox("����ʧ�ܣ����濨װ̬�ѷ����仯\n��[ȷ��]��ϵͳ�Զ�ˢ��", "��ʾ>>");
                return false;
            }
            return true;

        }

        /// <summary>
        /// ��ַ��ʾ����
        /// </summary>
        /// <param name="isDetail">true��ϸ��ַģʽ</param>
        public void SetAddressInfoVisible(bool isDetail)
        {
            this.label52.Visible = isDetail;
            this.label53.Visible = isDetail;
            this.label54.Visible = isDetail;
            this.txtCounty.Visible = isDetail;
            this.txtCity.Visible = isDetail;
            this.txtProvince.Visible = isDetail;
            this.dtDeadDate.Visible = isDetail;

            this.txtSpecialAddress.Visible = true;
        }   

        /// <summary>
        /// ��ַ��Ϣ�Ƿ��סԺ�����м���
        /// </summary>
        private bool IsAddressLoad
        {
            get 
            {
                return this.isAddressLoad;
            }
            set
            {
                this.isAddressLoad = value;
                if (this.isAddressLoad)
                {
                    this.cbxHomeAearOne.Checked = true;
                }
                else
                {
                    this.cbxHomeAearOne.Checked = false;
                }

            }
        }

        /// <summary>
        /// ��ӻ��߻�����Ϣ
        /// </summary>
        /// <param name="patient">ʵ��Neusoft.HISFC.Models.RADT.Patient</param>
        public void ShowPatienInfo(Neusoft.HISFC.Models.RADT.Patient patient)
        {
            //�ų���ʼ����ʱ�������� --zl
            if (patient.ID == null || patient.ID == "")
            {
                return;
            }

            //���¼ӿ�ʱ���Զ�����������Ϣ
            try
            {
                string patientID = patient.PID.CardNO;

                if (this.PatientType == Neusoft.HISFC.DCP.Enum.PatientType.I)          //סԺ���� patientID ȡ PatientNO
                {
                    patientID = patient.PID.PatientNO;
                }

                if (patientID == null)
                {
                    patientID = patient.ID;

                    if (patient.ID.IndexOf( "ZY" ) >= 0)
                    {
                        patientID = patientID.Remove( 0, 4 );
                    }
                }

                if (patientID.Length > 0) 
                {
                    this.txtClinicNO.Text = patientID.PadLeft(10, '0');
                }
                if (this.PatientType == Neusoft.HISFC.DCP.Enum.PatientType.I)
                {
                    this.txtSpecialAddress.Text = patient.AddressHome;
                    this.IsAddressLoad = true;
                    this.lbPatientNO.Text = "ס Ժ ��";
                }
                else if (this.PatientType == Neusoft.HISFC.DCP.Enum.PatientType.C)
                {
                    this.IsAddressLoad = false;
                    this.lbPatientNO.Text = "�� �� ��";
                }
                this.txtPatientName.Text = patient.Name;
                this.txtPatientID.Text = patient.IDCard;
                this.cmbSex.Tag = patient.Sex.ID;

                //����ְҵ��ֵ û��ȡ��������

                //if (patient.Profession.ID == "" || patient.Profession == null)
                //{
                //    patient.ID;

                    
                //}

                

                try
                {
                    this.dtBirthDay.Value = patient.Birthday;
                }
                catch
                {
                    this.dtBirthDay.Value = this.diseaseMgr.GetDateTimeFromSysDateTime();
                    this.txtAge.Text = "";
                    this.rdbYear.Checked = true;
                }
                this.txtWorkPlace.Text = patient.AddressBusiness;//��λ��ַ
                if (!string.IsNullOrEmpty( patient.Profession.ID ))
                {
                    this.cmbProfession.SelectedValue = patient.Profession.ID;//ְҵ
                }
                this.txtTelephone.Text = patient.PhoneHome;//��ϵ�绰
                this.txtSpecialAddress.Text = patient.AddressHome;//��ͥסַ
                this.cmbReportDoctor.Tag = this.User.ID;
                this.cmbDoctorDept.Tag = this.User.Dept.ID;
                this.operType = OperType.����;
            }
            catch (Exception ex)
            {
                this.MyMessageBox("��ӻ��߻�����Ϣʧ��" + ex.Message, "err");
            }
        }

        /// <summary>
        /// �ж�״̬
        /// </summary>
        /// <param name="report"></param>
        private void CheckState(string state)
        {
            if (state == "3" || state == "4")
            {
                this.lblState.Text = "1";
            }
            else
            {
                this.lblState.Text = "0";
            }
        }

        /// <summary>
        /// ���ݱ��濨��ʾ����Ϣ
        /// </summary>
        /// <param name="report"></param>
        public void ShowReportData(Neusoft.HISFC.DCP.Object.CommonReport report)
        {
            try
            {
                this.SetOperType(OperType.��ѯ);

                //���
                this.lbID.Text = report.ReportNO;

                if (report.CorrectFlag != null && report.CorrectFlag != "" && report.ID != "")
                {
                    this.lbState.Text = "��������";
                    Neusoft.HISFC.DCP.Object.CommonReport tempReport = this.diseaseMgr.GetCommonReportByID(report.CorrectedReportNO);
                    try
                    {
                        this.lbState.Text += "(��������" + tempReport.ExtendInfo1 + ")";
                    }
                    catch
                    { }
                }
                else
                {
                    this.lbState.Text = "���α���";
                    if (report.ExtendInfo3 != "")
                    {
                        Neusoft.HISFC.DCP.Object.CommonReport tempReport = this.diseaseMgr.GetCommonReportByNO(report.ID);
                        try
                        {
                            this.lbState.Text += "(��������" + tempReport.ExtendInfo1 + ")";
                        }
                        catch
                        { }
                    }
                }

                //�㱨�濨״̬����ֵ��ҳ��
                this.CheckState(report.State);              


                //���ߺ�
                this.txtClinicNO.Text = report.Patient.PID.CardNO;
                //��������
                this.txtPatientName.Text = report.Patient.Name;
                //�ҳ�����
                this.txtPatientParents.Text = report.PatientParents;
                //���֤��
                this.txtPatientID.Text = report.Patient.IDCard;
                //�Ա�
                this.cmbSex.Tag = report.Patient.Sex.ID;           
                //��������
                this.dtBirthDay.Value = report.Patient.Birthday;
                //����
                this.txtAge.Text = report.Patient.Age;
                //���䵥λ
                switch (report.AgeUnit)
                {
                    case "0":
                        this.rdbYear.Checked = true;
                        break;
                    case "1":
                        this.rdbMonth.Checked = true;
                        break;
                    default:
                        this.rdbDay.Checked = true;
                        break;
                }
                //����ְҵ
                if (string.IsNullOrEmpty( report.Patient.Profession.ID ) == false)
                {
                    this.cmbProfession.Tag = report.Patient.Profession.ID;
                    this.cmbProfession.SelectedValue = report.Patient.Profession.ID;
                }                
 
                //���߹�����λ
                this.txtWorkPlace.Text = report.Patient.CompanyName;
                //��ϵ�绰
                this.txtTelephone.Text = report.Patient.PhoneHome;
                //������Դ��
                this.SetHomeArea(Neusoft.FrameWork.Function.NConvert.ToInt32(report.HomeArea));
                this.txtSpecialAddress.Visible = true;

                if (string.IsNullOrEmpty( report.Patient.AddressHome ) == false)
                {
                    string[] householdNames = report.Patient.AddressHome.Split( ',' );
                    this.txtSpecialAddress.Text = householdNames[0];
                    this.cmbInfectionClass.SelectedValue = report.Disease.ID;
                }
                //xiwx ��ϸ��ַ
                this.txtSpecialAddress.Text = report.ExtendInfo1;
                //��������
                if (report.InfectDate != DateTime.MinValue)
                {
                    this.dtInfectionDate.Value = report.InfectDate;
                }
                
                //�������
                if (report.DiagnosisTime != DateTime.MinValue)
                {
                    this.dtDiaDate.Value = report.DiagnosisTime;
                }
                //��������
                try
                {
                    //���쳣˵������ʱ����Ч
                    this.dtDeadDate.Value = report.DeadDate;
                    this.cbxDeadDate.Checked = false;
                }
                catch
                {                    
                    this.cbxDeadDate.Checked = true;                     
                }
                //��������
                if (report.Disease != null && string.IsNullOrEmpty(report.Disease.ID) == false)
                {
                    this.cmbInfectionClass.SelectedValue = report.Disease.ID;
                    this.cmbInfectionClass.Text = report.Disease.Name;
                }

                //��������
                if (report.CaseClass1 != null && string.IsNullOrEmpty(report.CaseClass1.ID) == false)
                {
                    this.cmbCaseClassOne.SelectedValue = report.CaseClass1.ID;
                }
                this.cmbCaseClaseTwo.SelectedValue = report.CaseClass2;
                //�Ӵ�����
                this.rdbInfectOtherYes.Checked = Neusoft.FrameWork.Function.NConvert.ToBoolean(report.InfectOtherFlag);
             
                //��ע
                this.rtxtMemo.Text = report.Memo;

                //������ �������

                this.cmbReportDoctor.Tag = report.ReportDoctor.ID;
                this.cmbDoctorDept.Tag = report.DoctorDept.ID;
                //����ʱ��                
                this.lbReportTime.Text = report.ReportTime.ToString();

                //����-�˿�ԭ��
                this.txtCase.Text = report.OperCase;
                
                //����
                if (IsNeedAdd)
                {
                    this.GetAdditionInfo(report);
                }

            }
            catch (Exception ex)
            {
                this.MyMessageBox("����ת��ʧ��,��Ϣ���ܲ���ȫ" + ex.Message, "err");
            }
        }

        /// <summary>
        /// ��ȡ���濨��Ϣ[��������֤]
        /// </summary>
        /// <param name="report">��Ⱦ������</param>
        /// <param name="additionReport">��Ⱦ������</param>
        /// <param name="sexAdditionReport">�Բ�����</param>
        /// <returns>-1 ʧ��</returns>
        private int GetReportData(ref Neusoft.HISFC.DCP.Object.CommonReport report)
        {
            try
            {
                System.DateTime now = new DateTime();
                now = this.diseaseMgr.GetDateTimeFromSysDateTime();
                //�޸ĵı����������š����

                //������¿���¼������
                if (this.operType == OperType.���� || this.operType == OperType.����)
                {
                    report.ReportTime = now;//����ʱ��
                    report.DoctorDept.ID = this.User.Dept.ID;
                    report.ReportDoctor.ID = this.User.ID;//����ҽ��
                }

                report.PatientType = Enum.GetName( typeof( Neusoft.HISFC.DCP.Enum.PatientType ), (int)this.PatientType );//����
                if (this.txtClinicNO.Text.Trim() == null || this.txtClinicNO.Text.Trim() == "")
                {
                    this.MyMessageBox( "����д������", "err" );
                    this.txtPatientName.Select();
                    this.txtPatientName.Focus();
                    return -1;
                }
                else
                {
                    report.Patient.PID.CardNO = this.txtClinicNO.Text;//���ߺ�
                    report.PatientDept.ID = this.User.Dept.ID;
                }

                //����
                if (this.txtPatientName.Text.Trim() == null || this.txtPatientName.Text.Trim() == "")
                {
                    if (this.txtPatientParents.Text.Trim() == null || this.txtPatientParents.Text.Trim() == "")
                    {
                        this.MyMessageBox( "����д����", "err" );
                        this.txtPatientName.Select();
                        this.txtPatientName.Focus();
                        return -1;
                    }
                }
                else
                {
                    report.Patient.Name = this.txtPatientName.Text.Trim();
                }
                report.PatientParents = this.txtPatientParents.Text.Trim();//�ҳ�����
             
                if (!string.IsNullOrEmpty( this.txtPatientID.Text ))
                {
                    string err = "";
                    if (Neusoft.FrameWork.WinForms.Classes.Function.CheckIDInfo( this.txtPatientID.Text, ref err ) == -1)
                    {
                        this.MyMessageBox( err, "err" );
                        this.txtPatientID.Select();
                        this.txtPatientID.Focus();
                        return -1;
                    }
                    else
                    {
                        string ID = this.txtPatientID.Text.Trim();
                        int year = 0;
                        int month = 0;
                        int day = 0;
                        DateTime dtBirth = System.DateTime.Now;
                        if (ID.Length == 15)
                        {
                            year = Convert.ToInt32( "19" + ID.Substring( 6, 2 ) );
                            month = Convert.ToInt32( ID.Substring( 8, 2 ) );
                            day = Convert.ToInt32( ID.Substring( 10, 2 ) );
                        }
                        else
                        {
                            year = Convert.ToInt32( ID.Substring( 6, 4 ) );
                            month = Convert.ToInt32( ID.Substring( 10, 2 ) );
                            day = Convert.ToInt32( ID.Substring( 12, 2 ) );
                        }
                        dtBirth = new DateTime( year, month, day );

                        report.Patient.IDCard = this.txtPatientID.Text.Trim();//���֤��
                    }
                }

                if (this.cmbSex.Tag != null && this.cmbSex.Tag.ToString() != "")
                {
                    report.Patient.Sex.ID = this.cmbSex.Tag;
                }
                else
                {
                    this.MyMessageBox( "��ѡ���Ա�", "err" );
                    this.txtAge.Select();
                    this.txtAge.Focus();
                    return -1;
                }
                report.Patient.Birthday = new DateTime( this.dtBirthDay.Value.Year, this.dtBirthDay.Value.Month,
                    this.dtBirthDay.Value.Day, 0, 0, 0 );//��������

                //����
                string agemessage = "\n��ʾ��������ѡ��������ڣ�ϵͳ���Զ���������";
                if (this.txtAge.Text.Trim() == null || this.txtAge.Text.Trim() == "")
                {
                    this.MyMessageBox( "��ѡ��������ڻ���д����" + agemessage, "err" );
                    this.txtAge.Select();
                    this.txtAge.Focus();
                    return -1;
                }
                else
                {
                    for (int i = 0; i < this.txtAge.Text.Trim().Length; i++)
                    {
                        if (!Char.IsDigit( this.txtAge.Text.Trim(), i ))
                        {
                            this.MyMessageBox( "����Ӧ��Ϊ������" + agemessage, "err" );
                            this.txtAge.Select();
                            this.txtAge.Focus();
                            return -1;
                        }
                    }
                    report.Patient.Age = this.txtAge.Text.Trim();
                }

                //���䵥λ
                report.AgeUnit = this.rdbYear.Checked ? "0" : (this.rdbMonth.Checked ? "1" : "2");
                if (report.AgeUnit == "1" || report.AgeUnit == "2"
                    || (report.AgeUnit == "0" && Neusoft.FrameWork.Function.NConvert.ToInt32( report.Patient.Age ) <= 14))
                {
                    if (this.txtPatientParents.Text.Trim() == null || this.txtPatientParents.Text.Trim() == "")
                    {
                        this.MyMessageBox( "14�����£���14�꣩��ͯ������д�ҳ�����", "err" );
                        this.txtPatientParents.Select();
                        this.txtPatientParents.Focus();
                        return -1;
                    }
                }
                int intage = Neusoft.FrameWork.Function.NConvert.ToInt32( report.Patient.Age );
                if (this.rdbYear.Checked)
                {
                    if (this.diseaseMgr.GetAge( report.Patient.Birthday ) != report.Patient.Age + "��")
                    {
                        report.Patient.Birthday = new DateTime( now.AddYears( -intage ).Year, now.AddYears( -intage ).Month, now.AddYears( -intage ).Day, 0, 0, 0 );
                    }
                }
                if (this.rdbDay.Checked)
                {
                    if (intage == 31 && this.diseaseMgr.GetDateTimeFromSysDateTime().Day != 31)
                    {
                        intage = 100;
                    }
                    if (intage > 31)
                    {
                        this.MyMessageBox( "������������һ���£���ѡ���·�" + agemessage, "err" );
                        this.dtBirthDay.Select();
                        this.dtBirthDay.Focus();
                        return -1;
                    }
                }
                if (this.rdbMonth.Checked)
                {
                    if (intage > 12)
                    {
                        this.MyMessageBox( "�������12���£�����д����" + agemessage, "err" );
                        this.dtBirthDay.Select();
                        this.dtBirthDay.Focus();
                        return -1;
                    }
                    if (this.diseaseMgr.GetAge( report.Patient.Birthday ) != report.Patient.Age + "��")
                    {
                        report.Patient.Birthday = new DateTime( now.AddMonths( -intage ).Year, now.AddMonths( -intage ).Month, now.Day, 0, 0, 0 );
                    }
                }

                //������Դ
                string homearea = this.GetHomeArea();
                if (homearea == "7")
                {
                    this.MyMessageBox( "��ѡ����ס��ַ", "err" );
                    this.cbxHomeAearOne.Select();
                    this.cbxHomeAearOne.Focus();
                    this.cbxHomeAearOne.Checked = false;
                    return -1;
                }
                report.HomeArea = homearea;

                //��ϵ�绰
                if (this.txtTelephone.Text.Trim() == "" || this.txtTelephone.Text.Trim() == null)
                {
                    this.MyMessageBox( "����д��ϵ�绰", "err" );
                    this.txtTelephone.Select();
                    this.txtTelephone.Focus();
                    return -1;
                }
                report.Patient.PhoneHome = this.txtTelephone.Text.Trim();

                //ְҵ����
                if (this.cmbProfession.SelectedValue != null)
                {
                    string profession = this.cmbProfession.SelectedValue.ToString();
                    if (profession == "####")
                    {
                        this.MyMessageBox( "��ѡ����ְҵ", "err" );
                        this.cmbProfession.Select();
                        this.cmbProfession.Focus();
                        return -1;
                    }
                    report.Patient.Profession.ID = profession;
                }
                else
                {
                    this.MyMessageBox( "��ѡ����ְҵ", "err" );
                    this.cmbProfession.Select();
                    this.cmbProfession.Focus();
                    return -1;
                }

                //������λ
                string workplace = "";
                workplace = this.txtWorkPlace.Text.Trim();
                //˭˵������λ����ѽ�� ǰ����дʱ�Ѿ�������д��ʾ�� 2011-3-8
                //if (workplace == "" && this.hshStudent.Contains( report.Patient.Profession.ID ))
                //{
                //    this.MyMessageBox( "����" + "\"" + "������λ��" + "\"" + "��д" + "\"" + this.hshStudent[report.Patient.Profession.ID].ToString() + "\"", "err" );
                //    this.txtWorkPlace.Select();
                //    this.txtWorkPlace.Focus();
                //    return -1;
                //}
                report.Patient.CompanyName = workplace;

                //report.PatientDept.ID = this.PatientDept.ID;
                //��Ⱦ��
                //��ס��ϸ��ַxiwx
                report.ExtendInfo1 = this.txtSpecialAddress.Text;

                Neusoft.FrameWork.Models.NeuObject disease = new Neusoft.FrameWork.Models.NeuObject();
                if (this.GetDisease( ref disease ) == -1)
                {
                    this.MyMessageBox( "��ѡ�񼲲�����", "err" );
                    this.cmbInfectionClass.Select();
                    this.cmbInfectionClass.Focus();
                    return -1;
                }
                report.Disease = disease;
                //���������˷紦��
                if (this.hshLitteChild.Contains( report.Disease.ID ))
                {
                    if (report.AgeUnit != "2" || (Neusoft.FrameWork.Function.NConvert.ToInt32( report.Patient.Age ) > 28
                        && report.AgeUnit == "2"))
                    {
                        this.MyMessageBox( "���������˷�����ӦС��28�죬��˶���Ϻ�����", "err" );
                        this.dtBirthDay.Select();
                        this.dtBirthDay.Focus();
                        return -1;
                    }
                }
                //�������� xiwx2011.2.22
                if (this.cmbCaseClassOne.SelectedValue == null || this.cmbCaseClassOne.SelectedValue.ToString() == "####")
                {
                    this.MyMessageBox("��ѡ��������", "err");
                    this.cmbCaseClassOne.Select();
                    this.cmbCaseClassOne.Focus();
                    return -1;
                }
                string caseclass = this.cmbCaseClassOne.SelectedValue.ToString();                
                report.CaseClass1.ID = caseclass;
                if (this.cmbCaseClaseTwo.Enabled)
                {
                    if (this.cmbCaseClaseTwo.SelectedValue == null)
                    {
                        this.MyMessageBox( "���͸���/Ѫ���没��ѡ��������2", "err" );
                        this.cmbCaseClaseTwo.Select();
                        this.cmbCaseClaseTwo.Focus();
                        return -1;
                    }
                    report.CaseClass2 = this.cmbCaseClaseTwo.SelectedValue.ToString();
                }
                else
                {
                    report.CaseClass2 = "";
                }
                //��������              
                report.InfectDate = new DateTime( this.dtInfectionDate.Value.Year, this.dtInfectionDate.Value.Month,
                        this.dtInfectionDate.Value.Day, 0, 0, 0 );//��������
              
               
                report.DiagnosisTime = new DateTime( this.dtDiaDate.Value.Year, this.dtDiaDate.Value.Month,
                    this.dtDiaDate.Value.Day, this.dtDiaDate.Value.Hour, 0, 0 );//�������
                if (this.diseaseMgr.GetDateTimeFromSysDateTime().CompareTo( report.DiagnosisTime ) < 0)
                {
                    this.MyMessageBox( "������ڳ����˽���", "err" );
                    this.dtInfectionDate.Select();
                    this.dtInfectionDate.Focus();
                    return -1;
                }
                //�������ڸ�ֵ
                if (this.cbxDeadDate.Checked)
                {   
                    //����
                    report.DeadDate = new DateTime(1, 1, 1);                   
                }
                else
                {
                    report.DeadDate = new DateTime(this.dtDeadDate.Value.Year, this.dtDeadDate.Value.Month,
                       this.dtDeadDate.Value.Day, 0, 0, 0);//��������
                }

                if (report.DiagnosisTime.CompareTo( report.InfectDate ) < 0)
                {
                    this.MyMessageBox( "�������Ӧ���ڷ�������", "err" );
                    this.dtInfectionDate.Select();
                    this.dtInfectionDate.Focus();
                    return -1;
                }
                //��������
                if (!this.cbxDeadDate.Checked)
                {
                    if (report.DiagnosisTime.CompareTo( report.DeadDate ) < 0)
                    {
                        this.MyMessageBox( "�������Ӧ������������", "err" );
                        this.dtDeadDate.Select();
                        this.dtDeadDate.Focus();
                        return -1;
                    }
                    if (report.DeadDate.CompareTo( report.InfectDate ) < 0)
                    {
                        this.MyMessageBox( "��������Ӧ���ڷ�������", "err" );
                        this.dtDeadDate.Select();
                        this.dtDeadDate.Focus();
                        return -1;
                    }
                }
                //�����и����ı����޸�Ϊ���������ı���ʱһ��Ҫ���ĸ�����־


                //��Ⱦ����
                report.InfectOtherFlag = this.rdbInfectOtherYes.Checked ? "1" : (this.rdbInfectionOtherNo.Checked ? "0" : "");
                //����
                //repor = this.txtCase.Text.Trim();
                //��ע��������
                if (report.Memo.Length > 100)
                {
                    this.MyMessageBox( "���濨����ʧ��\n\n��ע��" + report.Memo + "\n\n�������࣬�������100����\n", "err" );
                    this.rtxtMemo.Text = report.Memo;
                    return -1;
                }
                //��ע
                report.Memo = this.rtxtMemo.Text.Trim();
                if (this.hshNeedMemo.Contains( report.Disease.ID ) && report.Memo == "")
                {
                    this.MyMessageBox( "���ڱ�ע����д��������", "err" );
                    return -1;
                }

                report.ModifyOper.ID = this.User.ID;
                report.ModifyTime = now;

                //������Ϣ
                report.Oper.ID = this.cmbReportDoctor.Tag.ToString();
                report.OperDept.ID = this.User.Dept.ID;
                report.OperTime = now;//this.myReport.GetDateTimeFromSysDateTime();
                if (this.operType == OperType.����)
                {
                    this.RenewInfo( ref report );
                }
                report.State = Function.ConvertState( Neusoft.HISFC.DCP.Enum.ReportState.New );
            }
            catch (Exception ex)
            {
                this.MyMessageBox( "��ȡ���濨��Ϣʧ��" + ex.Message, "err" );
                return -1;
            }
            return 0;
        }     

        /// <summary>
        /// ������ʱ��Ҫ�����ԭ�����ƹ����Ĳ�����Ϣ
        /// </summary>
        /// <param name="report"></param>
        private void RenewInfo(ref Neusoft.HISFC.DCP.Object.CommonReport report)
        {
            if (report == null)
            {
                return;
            }
            System.DateTime dt = new DateTime(1, 1, 1);
            report.ModifyOper.ID = "";
            report.ModifyTime = dt;
            report.ApproveOper.ID = "";
            report.ApproveTime = dt;
        }

        /// <summary>
        /// ������Դcheck
        /// </summary>
        /// <param name="index"></param>
        private void SetHomeArea(int index)
        {
            try
            {
                //����д�ı����ַ�ϲ���ʾ
                this.cbxHomeAearOne.Checked = (index == 1 ? true : false);
                this.cbxHomeAearTwo.Checked = (index == 2 ? true : false);
                this.cbxHomeAearThree.Checked = (index == 3 ? true : false);
                this.cbxHomeAearFour.Checked = (index == 4 ? true : false);
                this.cbxHomeAearFive.Checked = (index == 5 ? true : false);
                this.cbxHomeAearSix.Checked = (index == 6 ? true : false);
                #region ����
                //if (index == 5 || index == 6 || (this.lbID.Text != null && this.lbID.Text != ""))
                //{
                //    this.txtSpecialAddress.Visible = true;
                //    this.txtProvince.Clear();
                //    this.txtCity.Clear();
                //    this.txtCounty.Clear();
                //}
                //else
                //{
                //    //this.txtSpecialAddress.Visible = false;
                //    if (index == 1)
                //    {
                //        this.txtProvince.Text = this.myProvince;
                //        this.txtCity.Text = this.myCity;
                //        this.txtCounty.Text = this.myCounty;
                //    }
                //    else if (index == 2)
                //    {
                //        this.txtProvince.Text = this.myProvince;
                //        this.txtCity.Text = this.myCity;
                //        this.txtCounty.Clear();
                //    }
                //    else if (index == 3)
                //    {
                //        this.txtProvince.Text = this.myProvince;
                //        this.txtCity.Clear();
                //        this.txtCounty.Clear();
                //    }
                //    else
                //    {
                //        this.txtProvince.Clear();
                //        this.txtCity.Clear();
                //        this.txtCounty.Clear();
                //    }
                //}
                #endregion
            }
            catch
            {
                this.cbxHomeAearOne.Checked = true;
            }
            //			this.cmbprovince.Select();
            //			this.cmbprovince.Focus();
        }

        /// <summary>
        /// ��ȡ������Դ
        /// </summary>
        /// <returns></returns>
        private string GetHomeArea()
        {
            try
            {
                if (this.cbxHomeAearOne.Checked)
                    return "1";
                else if (this.cbxHomeAearTwo.Checked)
                    return "2";
                else if (this.cbxHomeAearThree.Checked)
                    return "3";
                else if (this.cbxHomeAearFour.Checked)
                    return "4";
                else if (this.cbxHomeAearFive.Checked)
                    return "5";
                else if (this.cbxHomeAearSix.Checked)
                    return "6";
                else return "8";//��ѡ�� ����
            }
            catch
            {
                return "7";//����
            }
        }

        /// <summary>
        /// ��ȡ������Ϣ
        /// </summary>
        /// <param name="disease"></param>
        private int GetDisease(ref Neusoft.FrameWork.Models.NeuObject disease)
        {
            if (this.cmbInfectionClass.SelectedValue.ToString() == "####"
                || hshInfectClass.Contains(this.cmbInfectionClass.SelectedValue.ToString()))
            {
                return -1;
            }
            // if (this.rdbInfectionClass.Checked)
            {
                disease.ID = this.cmbInfectionClass.SelectedValue.ToString();
                string diseasename = (string)this.cmbInfectionClass.Text;
                if (diseasename != null && diseasename != "")
                {
                    disease.Name = diseasename;
                }
                else
                {
                    disease.Name = this.cmbInfectionClass.Text;
                }
            }
            disease.Memo = disease.ID.Substring(0, 1);
            return 0;
        }

        /// <summary>
        /// ����llbPatientNO�Ŀ���
        /// </summary>
        /// <param name="patientType"></param>
        public void SetEnablellb(Neusoft.HISFC.DCP.Enum.PatientType patientType)
        {
            if (Neusoft.HISFC.DCP.Enum.PatientType.C == patientType)
            {
                this.llbPatienNO.Enabled = false;
                this.llbPatienNO.Text = "�� �� ��";
                this.lbPatientName.Text = "���ﻼ��";
                this.LlbPatientType = Neusoft.HISFC.DCP.Enum.PatientType.C;
            }
            else if (Neusoft.HISFC.DCP.Enum.PatientType.I == patientType)
            {
                this.llbPatienNO.Enabled = false;
                this.llbPatienNO.Text = "ס Ժ ��";
                this.lbPatientName.Text = "סԺ����";
                this.LlbPatientType = Neusoft.HISFC.DCP.Enum.PatientType.I;
            }
            else
            {
                this.llbPatienNO.Enabled = true;
                this.llbPatienNO.Text = "ס Ժ ��";
                this.lbPatientName.Text = "סԺ����";
                this.LlbPatientType = Neusoft.HISFC.DCP.Enum.PatientType.I;
            }
        }

        #endregion

        #region �½�������

        /// <summary>
        /// ������б�����Ϣ
        /// </summary>
        /// <param name="isClearNo">false�����ߺš�Printpanel.Tag�������š�״̬�ⶼ���</param>
        public void ClearAll(bool isClearNo)
        {
            if (isClearNo)
            {
                this.lbID.Text = "";
                this.lbState.Text = "";
            }
            this.ClearAll();
        }

        /// <summary>
        /// ��������ı���Ϣ
        /// </summary>
        public void ClearAll()
        {
            try
            {   
                //Ĭ�Ϻ�������ʱ��
                cbxDeadDate.Checked = true;
                this.txtClinicNO.Text = "";
                this.lbID.Text = "";
                this.txtPatientName.Text = "";
                this.txtPatientParents.Clear();
                this.txtPatientID.Clear();

                this.txtAge.Clear();
                this.rdbYear.Checked = true;
                this.txtWorkPlace.Clear();
                this.txtTelephone.Clear();
                this.txtProvince.Clear();
                this.txtCity.Clear();
                this.txtCounty.Clear();
                this.cmbProfession.SelectedIndex = 0;
                this.cmbInfectionClass.SelectedIndex = 0;

                this.dtBirthDay.Value = this.diseaseMgr.GetDateTimeFromSysDateTime();
                this.txtAge.Text = "";
                this.rdbYear.Checked = true;

                this.dtInfectionDate.Value = this.dtBirthDay.Value;
                this.dtDiaDate.Value = this.dtBirthDay.Value;
                this.dtDeadDate.Value = this.dtBirthDay.Value;              

                this.cbxDeadDate.Checked = true;                

                this.cmbCaseClassOne.SelectedIndex = 0;
                this.rdbInfectionOtherNo.Checked = true;
                this.rtxtMemo.Clear();
                this.txtCase.Clear();
                this.tcReport.TabPages.RemoveByKey("tpAddition");
                this.IsNeedAdd = false;
                //������ �������
                this.cmbReportDoctor.SelectedText = this.employHelper.GetName(this.User.ID);
                this.cmbDoctorDept.SelectedText = this.deptHelper.GetName(this.User.Dept.ID);
                //����ʱ��
                this.lbReportTime.Text = this.diseaseMgr.GetDateTimeFromSysDateTime().ToString("yyyy-MM-dd HH:MM:ss");
                //xiwx��ϸ��ַ
                this.txtSpecialAddress.Text = string.Empty;
                this.cbxHomeAearOne.Checked = true;
                this.rdbYear.Checked = true;
            }
            catch (Exception e)
            {
                MessageBox.Show(Neusoft.FrameWork.Management.Language.Msg(e.Message));
                return;
            }
        }

        #endregion

        #region ��ѯ

        /// <summary>
        /// ��ѯ
        /// </summary>
        private void Query()
        {
            this.ClearHeadInfo();

            if (this.cmbQueryContent.SelectedValue.ToString() == "ReportInfo")
            {
                #region ȫԺ��ѯ

                if (this.txtReportNo.Text.Trim() != "")
                {
                    this.QueryByReportNO();
                }
                else if (this.txtInPatienNo.Text.Trim() != "")
                {
                    this.QueryByPatientNO();
                }
                else if (this.txtName.Text != "")
                {
                    this.QueryByPatientName();
                }
                else if (this.txtDoctor.Text.Trim() != "")
                {
                    this.QueryByDoctorNO();
                }
                else
                {
                    this.QueryOldReport();
                }

                #endregion
            }
            else if (this.cmbQueryContent.SelectedValue.ToString() == "PatientInfo")
            {
                #region ���߲�ѯ

                if (this.txtInPatienNo.Text.Trim() != "")
                {
                    this.QueryByPatientNO();
                }
                else if (this.txtName.Text != "")
                {
                    this.QueryByPatientName();
                }
                else
                {
                    if (this.LlbPatientType == Neusoft.HISFC.DCP.Enum.PatientType.I)
                    {
                        this.QueryPatientByDeptIN();
                    }
                    else if(this.LlbPatientType == Neusoft.HISFC.DCP.Enum.PatientType.C)    //--�޸�
                    {
                        QueryPatientByDco();                    
                    }
                }

                #endregion
            }
            else if (this.cmbQueryContent.SelectedValue.ToString() == "DeptReport")
            {
                this.QueryDeptReport();
            }
            else if (this.cmbQueryContent.SelectedValue.ToString() == "DeptUnReport")
            {
                this.QueryDeptReportByReportState(Neusoft.HISFC.DCP.Enum.ReportState.UnEligible);
            }
            //else if (this.cmbQueryContent.SelectedValue.ToString() == "FeedBack")
            //{
            //    this.tvPatientInfo.Nodes.Clear();
            //    this.QueryFeedBackByDept();
            //}
        }

        /// <summary>
        /// ���Ҳ�ѯ���濨
        /// </summary>
        private void QueryDeptReport()
        {
            ArrayList al = this.diseaseMgr.GetCommonReportListByMore("report_date", this.dtBegin.Value.ToString(), this.dtEnd.Value.ToString(), "AAA", this.User.Dept.ID);
            this.TreeViewAddReports( al );
        }

        /// <summary>
        /// ���Ҳ�ѯ���濨
        /// </summary>
        private void QueryDeptReportByReportState(Neusoft.HISFC.DCP.Enum.ReportState reportState)
        {
            ArrayList al = this.diseaseMgr.GetCommonReportListByMore("report_date", this.dtBegin.Value.ToString(), this.dtEnd.Value.ToString(), ((int)reportState).ToString(), this.User.Dept.ID);
            this.TreeViewAddReports( al );
        } 

        #endregion

        #region ����

        /// <summary>
        /// ��֤������������Ϣ�Ƿ�����
        /// </summary>
        /// <param name="report">������Ϣ</param>
        /// <returns>-1 ��������1 ����</returns>
        public int AuthenticationInfo(ref Neusoft.HISFC.DCP.Object.CommonReport report)
        {
            if (this.GetReportData(ref report) == -1)
            {
                return -1;
            }

            //�Ƿ��и���
            if (this.IsNeedAdd)
            {
                if (this.JudgeAdditionInfo() <= 0)
                {
                    return -1;
                }
            }
            return 1;
        }

        /// <summary>
        /// ���붩����
        /// </summary>
        /// <param name="report">������Ϣ</param>
        /// <param name="reportState"></param>
        /// <returns></returns>
        public int InsertCorrectReport(ref Neusoft.HISFC.DCP.Object.CommonReport report)
        {
            Neusoft.HISFC.DCP.Object.CommonReport tempReport = report;
            tempReport.CorrectedReportNO = report.ID;
            //��ע�м���ԭ����
            if (report.Memo.IndexOf("//ԭ����[" + tempReport.Disease.Name + "]") == -1)
            {
                report.Memo += "//ԭ����[" + tempReport.Disease.Name + "]";
            }

            if (diseaseMgr.InsertCommonReport(tempReport) == -1)
            {
                this.MyMessageBox("����������ʧ��" + this.diseaseMgr.Err, "err");
                return -1;
            }
            report = tempReport;

            //��������
            if (this.IsNeedAdd)
            {
                if (this.UpdateAdditionInfo(this.operType, report) == -1)
                {
                    MessageBox.Show(Neusoft.FrameWork.Management.Language.Msg("�洢������Ϣʧ��"));
                    return -1;
                }
            }
            return 1;
        }

        /// <summary>
        /// �޸�ԭ��
        /// </summary>
        /// <param name="report"></param>
        /// <returns></returns>
        public int UpdateCorrectedReport(Neusoft.HISFC.DCP.Object.CommonReport mainreport)
        {
            if (this.diseaseMgr.UpdateCommonReport(mainreport) != 1)
            {
                Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                this.MyMessageBox("���濨����ʧ��>>" + this.diseaseMgr.Err, "err");
                return -1;
            }

            //��������
            if (this.IsNeedAdd)
            {
                if (this.UpdateAdditionInfo(this.operType, mainreport) == -1)
                {
                    MessageBox.Show(Neusoft.FrameWork.Management.Language.Msg("�洢������Ϣʧ��"));
                    return -1;
                }
            }

            return 1;
        }

        /// <summary>
        /// ���涩��������ԭ��
        /// </summary>
        /// <returns></returns>
        public int SaveCorrectReport()
        {
            Neusoft.HISFC.DCP.Object.CommonReport mainreport = new Neusoft.HISFC.DCP.Object.CommonReport();//ԭ��
            Neusoft.HISFC.DCP.Object.CommonReport report = new Neusoft.HISFC.DCP.Object.CommonReport();//������

            mainreport = this.GetSelectedReport();
            if (mainreport == null)
            {
                return -1;
            }

            //��֤��Ϣ
            if (this.AuthenticationInfo(ref report) == -1)
            {
                return -1;
            }

            //��ʾ��Ϣ

            //��ȡ��������Ϣ
            if (this.operType == OperType.����)
            {
                if (mainreport.CorrectFlag == "1")
                {
                    if(!this.IsContinue("�˿��Զ��������Ƿ����������"))
                    {
                        return -1;
                    }
                }

                //������               
                report.CorrectedReportNO = mainreport.ID;
                report.ExtendInfo3 = "������ԭ��Ϊ[" + mainreport.ReportNO + "]";
                //��ע�м���ԭ����
                if (report.Memo.IndexOf("//ԭ����[" + mainreport.Disease.Name + "]") == -1)
                {
                    report.Memo += "//ԭ����[" + mainreport.Disease.Name + "]";
                }
                //ԭ��
                mainreport.ExtendInfo3 = "�Ѷ���";
                mainreport.CorrectFlag = "1";
            }

            Neusoft.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڱ���,���Ժ�....");
            Application.DoEvents();
            //���붩����
            if (this.InsertCorrectReport(ref report) == -1)
            {
                return -1;
            }

            //�޸�ԭ��
            mainreport.CorrectReportNO = report.ID;
            if (this.UpdateCorrectedReport(mainreport) == -1)
            {
                return -1;
            }

            Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
            // ������Ϣ
            this.GetMessage(report);

            return 0;
        }

        #endregion

        #region ����
        
        /// <summary>
        /// ���ݼ����������Ƿ���Ҫ��Ӹ���
        /// </summary>
        /// <param name="diseaseCode"></param>
        public void IsNeedAddition(string infectCode)
        {
            string msg = "";
            //���Բ�����
            if (hshNeedSexReport.Contains(infectCode))
            {
                DiseaseReport.UC.ucBaseAddition sexAddition = new UFC.DCP.DiseaseReport.UC.ucSexAddition();
                this.AddAddtion(sexAddition);
                msg += "�Բ�����||";
            }
            //�踽��
            if (hshNeedAdd.Contains(infectCode))
            {
                DiseaseReport.UC.ucBaseAddition otherAddition = new UFC.DCP.DiseaseReport.UC.ucOtherAddition();
                this.AddAddtion(otherAddition);
                msg += "����";
            }

            if (msg != "")
            {
                if (this.IsNeedAdditionMeg)
                {
                    MessageBox.Show(Neusoft.FrameWork.Management.Language.Msg("�˼�����Ҫ") + msg);
                    this.tcReport.SelectedIndex = 1;
                    this.tcReport.TabPages["tpAddition"].Select();
                    this.tcReport.TabPages["tpAddition"].Focus();
                }
                this.tcReport.TabPages["tpAddition"].AutoScroll = true;
                this.IsNeedAdd = true;              
                this.tcReport.TabPages["tpAddition"].BackColor = Color.White;
            }
        }

        /// <summary>
        /// ȡ������Ϣ
        /// </summary>
        public void GetAdditionInfo(Neusoft.HISFC.DCP.Object.CommonReport diseaseReport)
        {
            if (this.tcReport.TabPages.ContainsKey("tpAddition"))
            {
                this.iAdditionReport = new DiseaseReport.UC.ucBaseAddition();
                this.iAdditionReport.SetAdditionInfo(this.iAdditionReport.GetAdditionInfo(diseaseReport.ReportNO),this.tcReport.TabPages["tpAddition"]);
                
            }
        }

        /// <summary>
        /// �޸ĸ�����Ϣ
        /// </summary>
        /// <param name="operType"></param>
        /// <param name="patientNO"></param>
        /// <param name="patientName"></param>
        /// <param name="report"></param>
        /// <returns></returns>
        public int UpdateAdditionInfo(OperType operType,Neusoft.HISFC.DCP.Object.CommonReport report)
        {
            if (this.tcReport.TabPages.ContainsKey("tpAddition"))
            {
                Neusoft.HISFC.DCP.Object.AdditionReport additionReport = new Neusoft.HISFC.DCP.Object.AdditionReport();
                this.iAdditionReport = new DiseaseReport.UC.ucBaseAddition();
                this.iAdditionReport.PatientNO = report.Patient.ID;
                this.iAdditionReport.PatientName = report.Patient.Name;
                this.iAdditionReport.Report = report;
                additionReport = (Neusoft.HISFC.DCP.Object.AdditionReport)this.iAdditionReport.GetAdditionInfo(this.tcReport.TabPages["tpAddition"]);
                additionReport.PatientNO = report.Patient.PID.ID;
                additionReport.PatientName = report.Patient.Name;
                additionReport.Memo = report.Disease.ID;

                int state = 0;
                if (string.IsNullOrEmpty(report.ID) == true)
                {
                    return this.iAdditionReport.InsertAdditionInfo(additionReport);                 
                }
                else if (operType == OperType.����)
                {
                    state = this.iAdditionReport.UpdateAdditionInfo(additionReport);
                    if (state <= 1)
                    {
                        state = this.iAdditionReport.InsertAdditionInfo(additionReport);
                    }
                    return state;
                }
                else
                {
                    return this.iAdditionReport.UpdateAdditionInfo(additionReport);
                }
            }
            return 1;
        }

        /// <summary>
        /// ��֤������Ϣ������
        /// </summary>
        /// <returns>-1,������ 1,����</returns>
        public int JudgeAdditionInfo()
        {
            string msg="";
            int i=0;
            if(this.tcReport.TabPages.ContainsKey("tpAddition"))
            {
                foreach(Control c in this.tcReport.TabPages["tpAddition"].Controls)
                {
                    if (c.GetType() == typeof(DiseaseReport.UC.ucSexAddition))
                    {
                        i = ((DiseaseReport.UC.ucSexAddition)c).IsValid(ref msg);
                    }
                    else
                    {
                        i = ((DiseaseReport.UC.ucBaseAddition)c).IsValid(ref msg);
                    }
                    if (i < 0)
                    {
                        msg = "������Ϣ��" + msg + "������";
                        MessageBox.Show(Neusoft.FrameWork.Management.Language.Msg(msg), "����", MessageBoxButtons.OK);
                        this.tcReport.SelectedIndex = 1;
                        return -1;
                    }
                }
            }
            return i;
        }

        /// <summary>
        /// ��Ӹ�����Ƭ
        /// </summary>
        /// <param name="ucBaseControl">�����û��ؼ�</param>
        public void AddAddtion(DiseaseReport.UC.ucBaseAddition ucBaseAddition)
        {
            if (this.tcReport.TabPages.Count == 1)
            {
                this.tcReport.TabPages.Add("tpAddition", "������Ϣ");
                this.tcReport.TabPages["tpAddition"].Controls.Add(ucBaseAddition);
                ucBaseAddition.Dock = DockStyle.Top;
            }
            else
            {
                this.tcReport.TabPages["tpAddition"].Controls.Add(ucBaseAddition);
                ucBaseAddition.Dock = DockStyle.Top;
            }
            this.IsNeedAdd = true; 
        }

	    #endregion       

        #endregion

        #region ����

        public class CompareState : IComparer
        {

            #region IComparer ��Ա

            public int Compare (object x, object y)
            {
                Neusoft.HISFC.DCP.Object.CommonReport r1 = x as Neusoft.HISFC.DCP.Object.CommonReport;
                Neusoft.HISFC.DCP.Object.CommonReport r2 = y as Neusoft.HISFC.DCP.Object.CommonReport;

                string oX = r1.State + r1.ReportNO;
                string oY = r2.State + r2.ReportNO;

                int nComp = 1;

                if (oX == null)
                {
                    nComp = (oY != null) ? -1 : 0;
                }
                else if (oY == null)
                {
                    nComp = 1;
                }
                else
                {
                    nComp = string.Compare( oX.ToString(), oY );
                }

                return nComp;
            }

            #endregion
        }

        #endregion

        #region IPreArrange ��Ա

        public int PreArrange ()
        {
            this.InitPrivInformation();

            this.isCDCPriv = Function.CheckUserPriv( Neusoft.FrameWork.Management.Connection.Operator.ID, "8001" );

            if (Neusoft.FrameWork.Management.Connection.Operator.ID != "009999")
            {
                if (isCDCPriv == false)
                {
                    MessageBox.Show("����Ԥ�����Ȩ�ޣ��޷�������Ӧ����", "Ȩ�޲���", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return -1;
                }
            }
            //2011-3-8
            this.txtCase.Enabled = true;

            return 1;
        }

        #endregion

        #region ��ӡ

        /// <summary>
        /// ��ӡ-��Ҫ�ȱ���
        /// </summary>
        public void print()
        {
            this.print(true);
        }

        /// <summary>
        /// ��ӡ
        /// </summary>
        /// <param name="needSave">true �������ܴ�ӡ</param>
        public void print(bool needSave)
        {
            if (needSave && this.lbID.Text == "")
            {
                this.MyMessageBox("���ȱ���", "��ʾ>>");
                return;
            }
            //��ע�ı߽�����
            this.rtxtMemo.BorderStyle = System.Windows.Forms.BorderStyle.None;

            //��ϸ��ַ������
            this.SetAddressInfoVisible(!this.txtSpecialAddress.Visible);

            if (this.cbxDeadDate.Checked)
            {
                this.dtDeadDate.Visible = false;
            }

            Neusoft.FrameWork.WinForms.Classes.Print p = new Neusoft.FrameWork.WinForms.Classes.Print();
            System.Drawing.Printing.PaperSize size = new System.Drawing.Printing.PaperSize("Letter", 700, 920);
            p.SetPageSize(size);
            p.ControlBorder = Neusoft.FrameWork.WinForms.Classes.enuControlBorder.None;
            //p.PrintPreview(55, 0, this.Printpanel);
            p.IsDataAutoExtend = true;
            p.IsAutoFont = true;

            //�ָ�
            this.rtxtMemo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.SetAddressInfoVisible(true);
        }

        #endregion

        private void cbxDeadDate_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxDeadDate.Checked)
            {
                dtDeadDate.CustomFormat = " ";
            }
            else
            {
                dtDeadDate.CustomFormat = "yyyy��MM��dd��";
            }
        }

        /// <summary>
        /// �س��¼��Ǹ�  --�޸�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtInPatienNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                this.Query();
            }
        }

        #region ���濨״̬ö��

        // ժҪ:
        //     ���濨״̬
        //public enum ReportState
        //{
            // ժҪ:
            //     ����
            //New = 0,
            ////
            //// ժҪ:
            ////     �ϸ�
            //Eligible = 1,
            ////
            //// ժҪ:
            ////     ���ϸ�
            //UnEligible = 2,
            ////
            //// ժҪ:
            ////     ����������
            //OwnCancel = 3,
            ////
            //// ժҪ:
            ////     ����������
            //Cancel = 4,
        //}

        #endregion
    }
}
