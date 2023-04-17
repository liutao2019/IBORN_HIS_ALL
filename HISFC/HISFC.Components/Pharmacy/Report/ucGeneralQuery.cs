using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Management;
using FS.FrameWork.Function;

namespace FS.HISFC.Components.Pharmacy.Report
{
    /// <summary>
    /// [��������: �ۺϲ�ѯ]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2006-12]<br></br>
    /// </summary>
    public partial class ucGeneralQuery : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucGeneralQuery()
        {
            InitializeComponent();

            this.GetSetting();
        }

        #region �����

        /// <summary>
        /// ���ݱ�
        /// </summary>
        private DataTable dt = new DataTable();

        /// <summary>
        /// ��ϸ���ݱ�
        /// </summary>
        private DataTable dtDetail = new DataTable();

        /// <summary>
        /// ��ǰ������
        /// </summary>
        private FS.FrameWork.Models.NeuObject privOper = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// ��ǰ��������
        /// </summary>
        private FS.FrameWork.Models.NeuObject operDept = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// ��ǰ����Ȩ������
        /// </summary>
        private FS.HISFC.Models.Admin.PowerLevelClass3 operPriv = new FS.HISFC.Models.Admin.PowerLevelClass3();

        /// <summary>
        /// ��ǰ���������� 
        /// </summary>
        private AssortType assortType = AssortType.Drug;

        /// <summary>
        /// �������
        /// </summary>
        private FS.FrameWork.Management.DataBaseManger dataManager = new FS.FrameWork.Management.DataBaseManger();

        /// <summary>
        /// ���������
        /// </summary>
        private FS.HISFC.BizLogic.Pharmacy.Report reportManager = new FS.HISFC.BizLogic.Pharmacy.Report();

        /// <summary>
        /// ҩ��/ҩ��ͨ�ò�ѯ����
        /// </summary>
        private string[] pList = { "���|In", "����|Out", "�̵�|Check", "����|Adjust" };

        /// <summary>
        /// ҩ���ѯ����
        /// </summary>
        private string[] piList = { "���ƻ�|InPlan","�ɹ��ƻ�|Stock", "̨��|Record" };

        /// <summary>
        /// ҩ��/ҩ��ͨ�ò�ѯ����
        /// </summary>
        private string pParam = "���|In,����|Out,�̵�|Check,����|Adjust";

        /// <summary>
        /// ҩ���ѯ����
        /// </summary>
        private string piParam = "���ƻ�|InPlan,�ɹ�|Stock,̨��|Record";

        /// <summary>
        /// Ȩ�޹�����
        /// </summary>
        private FS.HISFC.BizLogic.Manager.UserPowerDetailManager powerDetailManager = new FS.HISFC.BizLogic.Manager.UserPowerDetailManager();

        /// <summary>
        /// ��ʼʱ��
        /// </summary>
        private DateTime BeginTime = System.DateTime.MaxValue;

        /// <summary>
        /// ��ֹʱ��
        /// </summary>
        private DateTime EndTime = System.DateTime.MaxValue;

        /// <summary>
        /// ������˾������
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper companyHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// �������Ұ�����
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper producHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// ��ֵCellType
        /// </summary>
        private FarPoint.Win.Spread.CellType.NumberCellType numFpCellType = new FarPoint.Win.Spread.CellType.NumberCellType();

        /// <summary>
        /// ��ǰִ�е�Sql��������
        /// </summary>
        private string exeSqlIndex = "";

        /// <summary>
        /// �Ƿ�ʹ��Sql�����ļ���ʽ
        /// </summary>
        private bool isUseSqlConfig = true;

        /// <summary>
        /// ��ϸ��Ϣ����ʱʹ�õ�Fp����(������0��ʼ)
        /// </summary>
        private int detailIndexNum = 3;

        /// <summary>
        /// ��ǰ��ѯ����
        /// </summary>
        private string privIndex = "";

        #endregion        

        #region ����

        /// <summary>
        /// �Ƿ���ʾ�������ڵ��б�
        /// </summary>
        public bool IsShowDeptList
        {
            get
            {
                return !this.splitContainer2.Panel2Collapsed;
            }
            set
            {
                this.splitContainer2.Panel2Collapsed = !value;
            }
        }

        /// <summary>
        /// �Ƿ���ʾ����Ȩ���б�
        /// </summary>
        public bool IsShowPrivList
        {
            get
            {
                return !this.splitContainer2.Panel1Collapsed;
            }
            set
            {
                this.splitContainer2.Panel1Collapsed = !value;
            }
        }

        /// <summary>
        /// �Ƿ���ʾ����/Ȩ���б�
        /// </summary>
        public bool IsShowList
        {
            get
            {
                return !this.splitContainer1.Panel1Collapsed;
            }
            set
            {
                this.splitContainer1.Panel1Collapsed = !value;
            }
        }

        /// <summary>
        /// ҩ��/ҩ��ͨ�ò�ѯ����
        /// </summary>
        [Description("ҩ��/ҩ��ͨ�ò�ѯ���� ��'|' �ָ���������� ����ⲻ���޸� ��������ʱδ����"),Category("����")]
        public string[] PList
        {
            get
            {
                return this.pList;
            }
            set
            {
                bool isPirvIn = false;
                bool isPrivOut = false;
                foreach (string str in value)
                {
                    if (str.IndexOf("���|In") != -1)
                        isPirvIn = true;
                    if (str.IndexOf("����|Out") != -1)
                        isPrivOut = true;
                }
                if (!isPrivOut || !isPirvIn)
                {
                    MessageBox.Show(Language.Msg("��Ч����ֵ ��������� ���|In ����|Out"));
                }

                this.pList = value;                
            }
        }

        /// <summary>
        /// ҩ��/ҩ��ͨ�ò�ѯ����
        /// </summary>
        [Description("ҩ��/ҩ��ͨ�ò�ѯ���� ��'|' �ָ���������� ����ⲻ���޸� ��ͬ����ͨ��','�ָ�"), Category("����")]
        public string PParam
        {
            get
            {
                return this.pParam;
            }
            set
            {
                this.pParam = value;

                this.PList = pParam.Split(',');
            }
        }
        
        /// <summary>
        /// ҩ���ѯ����
        /// </summary>
        [Description("ҩ���ѯ���� ��'|' �ָ����������"), Category("����")]
        public string[] PIList
        {
            get
            {
                return this.piList;
            }
            set
            {
                this.piList = value;
            }
        }

        /// <summary>
        /// ҩ��ͨ�ò�ѯ����
        /// </summary>
        [Description("ҩ��ͨ�ò�ѯ���� ��'|' �ָ���������� ����ⲻ���޸� ��ͬ����ͨ��','�ָ�"), Category("����")]
        public string PIParam
        {
            get
            {
                return this.piParam;
            }
            set
            {
                this.piParam = value;

                this.PIList = piParam.Split(',');
            }
        }

        /// <summary>
        /// �Ƿ�ʹ��Sql�����ļ���ʽ
        /// </summary>
        [Description("�Ƿ�ʹ��Sql�����ļ���ʽ"), Category("����"),DefaultValue(true)]
        public bool IsUseSqlConfig
        {
            get
            {
                return this.isUseSqlConfig;
            }
            set
            {
                this.isUseSqlConfig = value;
            }
        }

        /// <summary>
        /// ��ϸ��Ϣ����ʱʹ�õ�Fp����(������0��ʼ)
        /// </summary>
        [Description("��ϸ��Ϣ����ʱʹ�õ�Fp����(������0��ʼ)"), Category("����"), DefaultValue(3)]
        public int DetailIndexNum
        {
            get
            {
                return detailIndexNum;
            }
            set
            {
                detailIndexNum = value;
            }
        }

        #endregion

        #region ������

        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("��  ��", "���ü�������", FS.FrameWork.WinForms.Classes.EnumImageList.C��ѯ��ʷ, true, false, null);
            toolBarService.AddToolButton("������", "�����Ż��ܲ�ѯ", FS.FrameWork.WinForms.Classes.EnumImageList.Zת��, true, false, null);
            toolBarService.AddToolButton("��ҩƷ", "��ҩƷ���ܲ�ѯ", FS.FrameWork.WinForms.Classes.EnumImageList.YҩƷ, true, false, null);
            toolBarService.AddToolButton("������", "�����ݻ��ܲ�ѯ", FS.FrameWork.WinForms.Classes.EnumImageList.B��ҩ��, true, false, null);
            toolBarService.AddToolButton("��������", "���������ļ�", FS.FrameWork.WinForms.Classes.EnumImageList.S����, true, false, null);

            return toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "��  ��")
            {
                //ѡ��ʱ��Σ����û��ѡ��ͷ���
                if (FS.FrameWork.WinForms.Classes.Function.ChooseDate(ref this.BeginTime, ref this.EndTime) == 0)
                    return;

                this.ShowData();
            }
            if (e.ClickedItem.Text == "������")
            {
                if (this.assortType == AssortType.Company)
                    return;

                this.assortType = AssortType.Company;

                this.ShowData();
            }
            if (e.ClickedItem.Text == "��ҩƷ")
            {
                if (this.assortType == AssortType.Drug)
                    return;

                this.assortType = AssortType.Drug;

                this.ShowData();
            }
            if (e.ClickedItem.Text == "������")
            {
                if (this.assortType == AssortType.Bill)
                    return;

                this.assortType = AssortType.Bill;

                this.ShowData();
            }
            if (e.ClickedItem.Text == "��������")
            {
                frmSetConfig frm = new frmSetConfig();
                frm.Show();
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            this.ShowData();

            return base.OnQuery(sender, neuObject);
        }

        public override int Export(object sender, object neuObject)
        {
            if (this.neuSpread1.Export() == 1)
            {
                MessageBox.Show(Language.Msg("�����ɹ�"));
            }
            return 1;
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            return 1;
        }

        /// <summary>
        /// ��ť��Ч������
        /// </summary>
        protected void SetToolButton()
        {
            switch (this.operPriv.Name)
            {
                case "���":
                case "����":
                case "�ɹ�":
                case "��Ʊ":               
                    this.toolBarService.SetToolButtonEnabled("������", true);
                    this.toolBarService.SetToolButtonEnabled("��ҩƷ", true);
                    this.toolBarService.SetToolButtonEnabled("������", true);
                    break;
                case "סԺ":
                    this.toolBarService.SetToolButtonEnabled("������", false);
                    this.toolBarService.SetToolButtonEnabled("��ҩƷ", true);
                    this.toolBarService.SetToolButtonEnabled("������", true);
                    break;
                case "����":            //������ʱ�������Ų� �����Ų�������
                case "����":
                    this.toolBarService.SetToolButtonEnabled("������", true);
                    this.toolBarService.SetToolButtonEnabled("��ҩƷ", true);
                    this.toolBarService.SetToolButtonEnabled("������", false);
                    break;
                case "�̵�":
                    this.toolBarService.SetToolButtonEnabled("������", true);
                    this.toolBarService.SetToolButtonEnabled("��ҩƷ", true);
                    this.toolBarService.SetToolButtonEnabled("������", false);
                    break;
                case "̨��":
                    this.toolBarService.SetToolButtonEnabled("������", false);
                    this.toolBarService.SetToolButtonEnabled("��ҩƷ", true);
                    this.toolBarService.SetToolButtonEnabled("������", false);
                    break;
                case "��ҩ":
                    this.toolBarService.SetToolButtonEnabled("������", true);
                    this.toolBarService.SetToolButtonEnabled("��ҩƷ", false);
                    this.toolBarService.SetToolButtonEnabled("������", false);
                    break;
            }
        }

        /// <summary>
        /// ��ť��Ч������ ������Sql���İ�ť ��ť������Ч
        /// </summary>
        /// <param name="indexPrifx">Sql���ǰ׺</param>
        protected void SetToolButton(string indexPrifx)
        {
            string index = "";

            index = indexPrifx + AssortType.Drug.ToString();
            if (this.hsSqlConfig.Contains(index))
            {
                this.toolBarService.SetToolButtonEnabled("��ҩƷ", true);
            }
            else
            {
                this.toolBarService.SetToolButtonEnabled("��ҩƷ", false);
            }

            index = indexPrifx + AssortType.Bill.ToString();
            if (this.hsSqlConfig.Contains(index))
            {
                this.toolBarService.SetToolButtonEnabled("������", true);
            }
            else
            {
                this.toolBarService.SetToolButtonEnabled("������", false);
            }

            index = indexPrifx + AssortType.Company.ToString();
            if (this.hsSqlConfig.Contains(index))
            {
                this.toolBarService.SetToolButtonEnabled("������", true);
            }
            else
            {
                this.toolBarService.SetToolButtonEnabled("������", false);
            }
        }

        #endregion

        #region ��ʼ��

        /// <summary>
        /// ���Ʋ�����ȡ 
        /// </summary>
        private void InitControlParam()
        {
            FS.HISFC.BizProcess.Integrate.Common.ControlParam ctrlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

            this.PParam = ctrlParamIntegrate.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.PharmacyConstant.Query_Commo_Type, false, "���|In,����|Out,�̵�|Check,����|Adjust");

            this.PIParam = ctrlParamIntegrate.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.PharmacyConstant.Query_PI_Type, false, "���ƻ�|InPlan,�ɹ�|Stock,̨��|Record");
        }

        /// <summary>
        /// ��ʼ������
        /// </summary>
        private void InitData()
        {
            this.privOper = this.dataManager.Operator;

            List<FS.FrameWork.Models.NeuObject> alPriv = FS.HISFC.Components.Common.Classes.Function.QueryPrivList("0330", true);
            if (alPriv != null && alPriv.Count != 0)
            {
                this.BeginTime = this.dataManager.GetDateTimeFromSysDateTime().Date.AddYears(-1);
                this.EndTime = this.dataManager.GetDateTimeFromSysDateTime();
            }

            #region ��ȡ������

            FS.HISFC.BizLogic.Pharmacy.Constant phaCons = new FS.HISFC.BizLogic.Pharmacy.Constant();
            ArrayList alCompany = phaCons.QueryCompany("1");
            if (alCompany != null)
            {
                this.companyHelper = new FS.FrameWork.Public.ObjectHelper(alCompany);
            }
            ArrayList alProduce = phaCons.QueryCompany("0");
            if (alProduce != null)
            {
                this.producHelper = new FS.FrameWork.Public.ObjectHelper(alProduce);
            }

            #endregion

            this.numFpCellType.DecimalPlaces = 4;

            this.BeginTime = this.dataManager.GetDateTimeFromSysDateTime().Date.AddDays(-7);
            this.EndTime = this.dataManager.GetDateTimeFromSysDateTime().Date.AddDays(1);

        }

        /// <summary>
        /// Ȩ�޿��ҳ�ʼ��
        /// </summary>
        protected int InitPrivDept()
        {
            this.tvDept.ImageList = this.tvDept.deptImageList;

            this.tvDept.Nodes.Clear();            

            List<FS.FrameWork.Models.NeuObject> alPrivDept = this.powerDetailManager.QueryUserPriv(this.privOper.ID,"0330");
            if (alPrivDept == null)
            {
                MessageBox.Show(Language.Msg("��ȡ��ԱȨ�޷�������" + this.powerDetailManager.Err));
                return -1;
            }
            if (alPrivDept.Count == 0)
            {
                MessageBox.Show(Language.Msg("����δ�����ѯ����"));
                return -1;
            }
            foreach (FS.FrameWork.Models.NeuObject info in alPrivDept)
            {
                //��ȡ�в�ѯȨ�޵Ŀ���
                TreeNode node = new TreeNode();

                node.Text = info.Name;

                node.ImageIndex = 0;
                node.SelectedImageIndex = 0;

                //�洢���Ҵ���+�������ͣ����ڿ��Ʋ����б����ʾ
                node.Tag = info;

                this.tvDept.Nodes.Add(node);
            }
            this.tvDept.SelectedNode = this.tvDept.Nodes[0];

            return 1;
        }

        /// <summary>
        /// Ȩ������ʼ��
        /// </summary>
        protected void InitPrivOper()
        {
            this.tvOper.ImageList = this.tvOper.groupImageList;

            this.tvOper.Nodes.Clear();

            FS.HISFC.Models.Admin.PowerLevelClass3 privClass3;             //��ѯȨ��
            foreach (string tempStr in this.PList)
            {
                #region ����ͨ������

                privClass3 = new FS.HISFC.Models.Admin.PowerLevelClass3();

                privClass3.ID = tempStr.Substring(tempStr.IndexOf("|") + 1);        //��ѯ���
                privClass3.Name = tempStr.Substring(0, tempStr.IndexOf("|"));       //��ѯ����
                privClass3.Memo = "AAAA";                                           //Ȩ����Ϣ

                TreeNode parentNode = new TreeNode();
                parentNode.Text = privClass3.Name;

                parentNode.ImageIndex = 0;
                parentNode.SelectedImageIndex = 1;

                parentNode.Tag = privClass3;

                this.tvOper.Nodes.Add(parentNode);

                if (privClass3.ID == "In")           //��ȡ���Ȩ������
                {
                    #region ����������Ȩ��

                    List<FS.FrameWork.Models.NeuObject> alInPriv = this.powerDetailManager.QueryUserPrivCollection(this.privOper.ID, "0310", this.operDept.ID);
                    if (alInPriv == null)
                    {
                        MessageBox.Show(Language.Msg("��ȡ����Ա���Ȩ�޼���ʱ����\n" + this.powerDetailManager.Err));
                        return;
                    }

                    foreach (FS.FrameWork.Models.NeuObject inInfo in alInPriv)
                    {
                        if (this.operDept.Memo == "PI")
                        {
                            if (inInfo.ID == "M1" || inInfo.ID == "M2" || inInfo.ID == "Z1" || inInfo.ID == "Z2")
                                continue;
                        }

                        privClass3 = new FS.HISFC.Models.Admin.PowerLevelClass3();

                        privClass3.ID = "In";
                        privClass3.Name = inInfo.Name;
                        privClass3.Memo = inInfo.ID;

                        TreeNode privNode = new TreeNode();
                        privNode.Text = privClass3.Name;		//����Ȩ������

                        privNode.ImageIndex = 2;
                        privNode.SelectedImageIndex = 4;

                        privNode.Tag = privClass3;				//����Ȩ�ޱ���

                        parentNode.Nodes.Add(privNode);
                    }

                    #endregion
                }

                if (privClass3.ID == "Out")         //��ȡ����Ȩ������
                {
                    #region ��ӳ�������Ȩ��

                    List<FS.FrameWork.Models.NeuObject> alOutPriv = this.powerDetailManager.QueryUserPrivCollection(this.privOper.ID, "0320", this.operDept.ID);
                    if (alOutPriv == null)
                    {
                        MessageBox.Show(Language.Msg("��ȡ����Ա����Ȩ�޼���ʱ����\n" + this.powerDetailManager.Err));
                        return;
                    }

                    foreach (FS.FrameWork.Models.NeuObject outInfo in alOutPriv)
                    {
                        privClass3 = new FS.HISFC.Models.Admin.PowerLevelClass3();

                        privClass3.ID = "Out";
                        privClass3.Name = outInfo.Name;
                        privClass3.Memo = outInfo.ID;

                        TreeNode privNode = new TreeNode();
                        privNode.Text = privClass3.Name;		//����Ȩ������

                        privNode.ImageIndex = 2;
                        privNode.SelectedImageIndex = 4;

                        privNode.Tag = privClass3;				//����Ȩ�ޱ���

                        parentNode.Nodes.Add(privNode);
                    }

                    #endregion
                }

                #endregion
            }

            if (this.operDept.Memo == "PI")
            {
                #region ����ҩ��ͨ������

                foreach (string tempStr in this.PIList)
                {
                    privClass3 = new FS.HISFC.Models.Admin.PowerLevelClass3();

                    privClass3.ID = tempStr.Substring(tempStr.IndexOf("|") + 1);	//��ѯ���
                    privClass3.Name = tempStr.Substring(0, tempStr.IndexOf("|"));	//��ѯ����

                    switch (privClass3.Name)
                    {
                        case "סԺ":
                            privClass3.Memo = "Z";
                            break;
                        case "����":
                            privClass3.Memo = "M";
                            break;
                        default:
                            privClass3.Memo = "AAAA";
                            break;
                    }

                    TreeNode node = new TreeNode();
                    node.Text = privClass3.Name;

                    node.ImageIndex = 0;
                    node.SelectedImageIndex = 1;

                    node.Tag = privClass3;

                    this.tvOper.Nodes.Add(node);
                }

                #endregion
            }

            if (this.tvOper.Nodes.Count > 0)
                this.tvOper.SelectedNode = this.tvOper.Nodes[0];
        }        

        #endregion

        #region ����

        /// <summary>
        /// ���ù�������
        /// </summary>
        private void Filter()
        {
            if (this.dt == null || this.dt.DefaultView == null)
                return;

            string queryCode = "%" + this.txtFilter.Text.Trim() + "%";
            string filter = "";

            //��ȡ��������
            switch (this.assortType)
            {
                case AssortType.Drug:	    //��ҩƷ
                case AssortType.Company:    //��������ʽ
                    filter = Function.GetFilterStr(this.dt.DefaultView, queryCode);
                    break;
                case AssortType.Bill:       //������
                    if (this.dt.Columns.Contains("���ݺ�"))
                        filter = "���ݺ� LIKE '" + queryCode + "'";
                    break;
            }
            try
            {
                this.dt.DefaultView.RowFilter = filter;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Language.Msg(ex.Message));
            }
        }

        /// <summary>
        /// ��ȡִ�в�ѯ��Sql����
        /// </summary>
        /// <returns></returns>
        private string GetSqlIndex(bool isDetail)
        {
            //string sqlIndex = "Pharmacy.Report.";
            string sqlIndex = "";
            if (isDetail)
            {
                //��ȡӦ��ѯ��sql
                if (this.operPriv.ID == "Out" && this.assortType.ToString() == "Bill")
                    sqlIndex = this.operPriv.ID + "By" + "DetailForBill";
                else
                    sqlIndex = this.operPriv.ID + "By" + "Detail";

                //�����ҩ��������ҩ
                if (this.operPriv.Memo == "M1" || this.operPriv.Memo == "M2")
                    sqlIndex = this.operPriv.ID + "ByClinicDetail";
                //סԺ��ҩ��סԺ��ҩ
                if (this.operPriv.Memo == "Z1" || this.operPriv.Memo == "Z2")
                    sqlIndex = this.operPriv.ID + "ByOutpatientDetail";
                //ҩ�������ϸ��ѯ
                if (this.operDept.Memo == "P" && this.operPriv.ID == "In")
                {
                    sqlIndex = this.operPriv.ID + "ByPDetail";
                }
            }
            else
            {
                //sqlIndex = this.operDept.Memo + this.operPriv.ID + "By" + this.assortType.ToString();
                sqlIndex = this.operPriv.ID + "By" + this.assortType.ToString();
            }

            return sqlIndex;
        }

        /// <summary>
        /// ִ��Sql����ȡDataTable
        /// </summary>
        /// <returns></returns>
        private int GetDataTable(string sqlIndex,bool isDetail)
        {
            if (isDetail)
            {
                DataSet ds = new DataSet();
                if (this.operPriv.ID == "Record")	//��̨����ϸ��Ϣ�ļ���ͨ��ҩƷ������У�ͨ��privcode��������
                {
                    string drugCode = this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, 0].Text;
                    ds = this.reportManager.PharmacyReportQueryBase(this.operDept.ID, this.BeginTime, this.EndTime, drugCode, this.exeSqlIndex);
                }
                else
                {
                    ds = this.reportManager.PharmacyReportQueryBase(this.operDept.ID, this.BeginTime, this.EndTime, this.operPriv.Memo, this.exeSqlIndex);
                }
                if (ds == null || ds.Tables.Count <= 0)
                {
                    MessageBox.Show(Language.Msg(this.reportManager.Err));
                    return -1;
                }

                this.dtDetail = ds.Tables[0];
            }
            else
            {
                DataSet ds = this.reportManager.PharmacyReportQueryBase(this.operDept.ID, this.BeginTime, this.EndTime, this.operPriv.Memo, sqlIndex);               

                if (ds == null || ds.Tables.Count <= 0)
                {
                    MessageBox.Show(Language.Msg(this.reportManager.Err));
                    return -1;
                }

                this.dt = ds.Tables[0];
            }
            return 1;
        }

        /// <summary>
        /// ���������ļ���ȡSql�����Ϣ
        /// </summary>
        /// <param name="isDetail">�Ƿ���ϸ��Ϣ��ʾ</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        private int GetDataByConfig(bool isDetail)
        {
            DataSet ds = new DataSet();
            if (isDetail)
            {
                #region ��ϸ����

                string index = this.operPriv.ID + "By" + this.operDept.Memo + "Detail";

                if (!this.hsSqlConfig.ContainsKey(index))
                {
                    if (this.hsSqlConfig.ContainsKey(index + "For" + this.assortType.ToString()))
                    {
                        index = index + "For" + this.assortType.ToString();
                    }
                    else
                    {
                        Function.ShowMsg(Language.Msg("δ�ҵ�" + index + "Sql���"));
                        return -1;
                    }
                }

                if (this.detailIndexNum >= this.neuSpread1_Sheet1.Columns.Count)                
                {
                    this.detailIndexNum = this.neuSpread1_Sheet1.Columns.Count;
                }

                string[] detailData = new string[this.detailIndexNum];

                for (int i = 0; i < this.detailIndexNum; i++)
                {
                    detailData[i] = this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, i].Text;
                }

                if (!this.hsSqlConfig.ContainsKey(index))
                {
                    Function.ShowMsg("δ�ҵ�" + index + "��ӦSql���");
                    return -1;
                }

                ds = this.reportManager.PharmacyReport(this.hsSqlConfig[index] as string, this.operDept.ID, this.BeginTime.ToString(),
                  this.EndTime.ToString(), this.operPriv.Memo, this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, 0].Text,
                  this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, 1].Text,
                  this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, 2].Text);

                if (ds != null && ds.Tables.Count > 0)
                {
                    this.dtDetail = ds.Tables[0];

                    this.privIndex = index;
                }
                else
                {
                    Function.ShowMsg(this.reportManager.Err);
                    return -1;
                }

                #endregion
            }
            else
            {
                #region ���ܼ���

                string index = this.operPriv.ID + "By" + this.assortType.ToString();                

                if (!this.hsSqlConfig.ContainsKey(index))
                {
                    if (!this.hsSqlConfig.ContainsKey(this.operPriv.ID + "By" + this.operDept.Memo + this.assortType.ToString()))
                    {
                        //���δ�����ʾ ����δ�ҵ�Sql����� ����Fp��ʾΪ��
                        //Function.ShowMsg("δ�ҵ�" + index + "��ӦSql���");
                        this.neuSpread1_Sheet1.Rows.Count = 0;

                        return -1;
                    }
                }

                this.SetToolButton(this.operPriv.ID + "By");

                ds = this.reportManager.PharmacyReport(this.hsSqlConfig[index] as string, this.operDept.ID, this.BeginTime.ToString(),
                   this.EndTime.ToString(), this.operPriv.Memo);

                if (ds != null && ds.Tables.Count > 0)
                {
                    this.dt = ds.Tables[0];

                    this.privIndex = index;

                    if (this.hsSqlFpPath.ContainsKey(index))
                    {
                        this.neuSpread1_Sheet1.DataAutoHeadings = false;
                        this.neuSpread1_Sheet1.DataAutoSizeColumns = false;
                    }
                    else
                    {
                        this.neuSpread1_Sheet1.DataAutoHeadings = true;
                        this.neuSpread1_Sheet1.DataAutoSizeColumns = true;
                    }
                }

                #endregion
            }

            return 1;
        }

        /// <summary>
        /// ��ѯ
        /// </summary>
        protected void ShowData()
        {
            this.lbSubTitle.Text = string.Format("����: {0} - {1}", this.BeginTime.ToString(), this.EndTime.ToString());

            switch (this.assortType)
            {
                case AssortType.Drug:
                    this.tpTotal.Text = "��ҩƷ ���ܲ�ѯ";
                    this.lbTitle.Text = "��ҩƷ ���ܲ�ѯ";
                    break;
                case AssortType.Company:
                    this.tpTotal.Text = "������ ���ܲ�ѯ";
                    this.lbTitle.Text = "������ ���ܲ�ѯ";
                    break;
                case AssortType.Bill:
                    this.tpTotal.Text = "������ ���ܲ�ѯ";
                    this.lbTitle.Text = "������ ���ܲ�ѯ";
                    break;
            }

            if (this.isUseSqlConfig)
            {
                if (this.GetDataByConfig(false) == -1)
                {
                    return;
                }
            }
            else
            {
                this.exeSqlIndex = this.GetSqlIndex(false);
                if (this.exeSqlIndex == null)
                {
                    this.exeSqlIndex = "";
                    return;
                }

                if (this.GetDataTable(this.exeSqlIndex, false) != 1)
                {
                    return;
                }
            }

            foreach (DataRow dr in this.dt.Rows)
            {
                if (this.dt.Columns.Contains("������˾"))
                    dr["������˾"] = this.companyHelper.GetName(dr["������˾"].ToString());
                if (this.dt.Columns.Contains("��������"))
                    dr["��������"] = this.producHelper.GetName(dr["��������"].ToString());
            }

            this.neuSpread1_Sheet1.DataSource = this.dt.DefaultView;

            this.Sum();

            this.SetFormat(false);

            this.Filter();

            if (this.neuTabControl1.SelectedTab == this.tpDetail)
            {
                this.neuTabControl1.SelectedTab = this.tpTotal;
            }

            if (this.neuTabControl1.TabPages.Contains(this.tpDetail) == true)
            {
                this.neuTabControl1.TabPages.Remove(this.tpDetail);
            }
        }

        /// <summary>
        /// ��ϸ��ѯ
        /// </summary>
        protected void ShowDetail()
        {
            try
            {
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڼ�����ϸ��Ϣ...���Ժ�");
                Application.DoEvents();

                if (this.isUseSqlConfig)
                {
                    if (this.GetDataByConfig(true) == -1)
                    {
                        return;
                    }
                }
                else
                {
                    this.exeSqlIndex = this.GetSqlIndex(true);

                    if (this.GetDataTable(this.exeSqlIndex, true) != 1)
                    {
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        return;
                    }
                }

                this.neuSpread2_Sheet1.DataSource = this.dtDetail.DefaultView;                

                this.SetFormat(true);

                if (!this.neuTabControl1.TabPages.Contains(this.tpDetail))
                {
                    this.neuTabControl1.TabPages.Add(this.tpDetail);
                }

                this.neuTabControl1.SelectedTab = this.tpDetail;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }
        }

        /// <summary>
        /// ��ʽ��
        /// </summary>
        protected void SetFormat(bool isDetail)  
        {
            this.GetFpSetting(isDetail);
        }

        /// <summary>
        /// �ϼƼ���
        /// </summary>
        protected void Sum()
        {
            //���б��ڼ���ϼ���
            DataRow row = this.dt.NewRow();

            #region ��Ӻϼ���
            switch (this.exeSqlIndex)
            {
                case "OutByDrug":	//��סԺ��ҩƷ��ѯ
                    row[1] = "�ϼ�:";
                    row[8] = this.dt.Compute("sum(���)", "");
                    row[11] = this.dt.Compute("sum(�����)", "");
                    row["ƴ����"] = "%";		//��ֹ�ڵ��ù��˺���ʱ���б����˵�
                    this.dt.Rows.Add(row);
                    break;
                case "OutByCompany":
                    row[1] = "�ϼ�:";
                    row[2] = this.dt.Compute("sum(���)", "");
                    row["ƴ����"] = "%";
                    this.dt.Rows.Add(row);
                    break;
                case "StockByBill":
                    row[0] = "�ϼ�:";
                    row[2] = this.dt.Compute("sum(�����ܽ��)", "");
                    this.dt.Rows.Add(row);
                    break;
                case "StockByDrug":
                    row[1] = "�ϼ�:";
                    row[7] = this.dt.Compute("sum(�ɹ��ƻ����)", "");
                    row[8] = this.dt.Compute("sum(��˽��)", "");
                    row[9] = this.dt.Compute("sum(���۽��)", "");
                    row["ƴ����"] = "%";	//��ֹ�ڵ��ù��˺���ʱ���б����˵�
                    this.dt.Rows.Add(row);
                    break;
                case "StockByCompany":
                    row[1] = "�ϼ�:";
                    row[6] = this.dt.Compute("sum(�ɹ��ƻ����)", "");
                    row[7] = this.dt.Compute("sum(���۽��)", "");
                    row["ƴ����"] = "%";
                    this.dt.Rows.Add(row);
                    break;
                case "CheckByBill":
                    row[0] = "�ϼ�:";
                    row[6] = this.dt.Compute("sum(��ӯ���)", "");
                    row[7] = this.dt.Compute("sum(�̿����)", "");
                    row[8] = this.dt.Compute("sum(ӯ���ϼ�)", "");
                    this.dt.Rows.Add(row);
                    break;
                case "CheckByDrug":
                    row[1] = "�ϼ�:";
                    row[6] = this.dt.Compute("sum(���ʿ��)", "");
                    row[7] = this.dt.Compute("sum(�̵���)", "");
                    row[8] = this.dt.Compute("sum(ӯ������)", "");
                    row[9] = this.dt.Compute("sum(ӯ�����)", "");
                    row["ƴ����"] = "%";
                    this.dt.Rows.Add(row);
                    break;

                case "InByDrug":
                    row[1] = "�ϼ�";
                    row[10] = this.dt.Compute("sum(�����ܽ��)", "");
                    row[12] = this.dt.Compute("sum(����ܽ��)", "");
                    row[13] = this.dt.Compute("sum(�����)", "");
                    row["ƴ����"] = "%";
                    this.dt.Rows.Add(row);
                    break;
                case "InByBill":
                    row[0] = "�ϼ�";
                    row[3] = this.dt.Compute("sum(�����ܽ��)", "");
                    row[4] = this.dt.Compute("sum(����ܽ��)", "");
                    this.dt.Rows.Add(row);
                    break;
                case "InByCompany":
                    row[1] = "�ϼ�";
                    row[4] = this.dt.Compute("sum(���۽��)", "");
                    row[5] = this.dt.Compute("sum(�����)", "");
                    row[6] = this.dt.Compute("sum(�����)", "");
                    row["ƴ����"] = "%";
                    this.dt.Rows.Add(row);
                    break;
                case "SendByCompany":
                    row[1] = "�ϼ�";
                    row[3] = this.dt.Compute("sum(���)", "");
                    this.dt.Rows.Add(row);
                    break;
                case "SendByDrug":
                    row[1] = "�ϼ�";
                    row[6] = this.dt.Compute("sum(���)", "");
                    this.dt.Rows.Add(row);
                    break;
                case "SendByBill":
                    row[0] = "�ϼ�";
                    row[6] = this.dt.Compute("sum(���)", "");
                    this.dt.Rows.Add(row);
                    break;
                case "AdjustByCompany":    //2006-4-5 by zlw  ���ղ���ͳ�Ƶ���
                    row[0] = "�ϼ�:";
                    row[13] = this.dt.Compute("sum(ӯ�����)", "");
                    row["ƴ����"] = "%";
                    this.dt.Rows.Add(row);

                    break;
            }
            #endregion
        }
      
        #endregion
       
        #region �����ļ���ȡ

        private System.Collections.Hashtable hsFpPathConfig = new Hashtable();

        /// <summary>
        /// Sql���洢
        /// </summary>
        private System.Collections.Hashtable hsSqlConfig = new Hashtable();

        /// <summary>
        /// Fp��ʽ�ļ��洢
        /// </summary>
        private System.Collections.Hashtable hsSqlFpPath = new Hashtable();

        /// <summary>
        /// ������·��
        /// </summary>
        private string serverPath = "";

        /// <summary>
        /// �û�����·��
        /// </summary>
        private string configPath = "";

        /// <summary>
        /// ��ȡSql������Ϣ
        /// </summary>
        /// <returns></returns>
        private int GetSetting()
        {
            #region ��ȡ�����ļ�·��

            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            doc.Load(Application.StartupPath + "\\url.xml");

            System.Xml.XmlNode node = doc.SelectSingleNode("//dir");
            if (node == null)
            {
                MessageBox.Show(Language.Msg("url����dir������"));
                return -1;
            }

            this.serverPath = node.InnerText;
            this.configPath = "//Report_Setting.xml"; //Զ�������ļ��� 

            #endregion

            try
            {
                doc.Load(serverPath + configPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(Language.Msg("װ��HisProfile.xmlʧ�ܣ�\n" + ex.Message));
                return -1;
            }

            System.Xml.XmlNode nodeCollection = doc.SelectSingleNode("/Setting/Fun[@ID='Pha']");

            FS.FrameWork.Management.DataBaseManger dataManager = new DataBaseManger();

            foreach (System.Xml.XmlNode reportConfigNode in nodeCollection.ChildNodes)
            {
                System.Xml.XmlNode sqlNode = reportConfigNode.ChildNodes[0];
                if (reportConfigNode.Attributes["sqlLocation"].Value == "1")        //Sqlλ��Xml��
                {                   
                    this.hsSqlConfig.Add(reportConfigNode.Attributes["ID"].Value, sqlNode.InnerText);
                }
                else
                {
                    string sql = "";
                    if (dataManager.Sql.GetSql(sqlNode.Attributes["index"].Value, ref sql) == -1)
                    {
                        MessageBox.Show(Language.Msg("��������δ��ȡSql���!") + sqlNode.Attributes["index"].Value);
                    }
                    this.hsSqlConfig.Add(reportConfigNode.Attributes["ID"].Value, sql);
                }

                System.Xml.XmlNode fpPathNode = reportConfigNode.ChildNodes[1];
                if (fpPathNode.Attributes["fileName"].Value.ToString() != "")
                {
                    this.hsSqlFpPath.Add(reportConfigNode.Attributes["ID"].Value, fpPathNode.Attributes["fileName"].Value);
                }
            }

            return 1;
        }

        /// <summary>
        /// Fp��ʽ����
        /// </summary>
        /// <returns></returns>
        private int GetFpSetting(bool isDetail)
        {
            if (this.hsFpPathConfig.ContainsKey(this.privIndex))
            {
                System.Xml.XmlDocument hsDoc = this.hsFpPathConfig[this.privIndex] as System.Xml.XmlDocument;

                if (!isDetail)
                {
                    frmSetConfig.SetFpByConfig(this.neuSpread1_Sheet1, hsDoc);
                }
                else
                {
                    frmSetConfig.SetFpByConfig(this.neuSpread2_Sheet1, hsDoc);
                }

                return 1;
            }

            string pathName = "";
            if (this.hsSqlFpPath.ContainsKey(this.privIndex))
            {
                pathName = this.hsSqlFpPath[this.privIndex] as string;

                if (pathName.IndexOf(".xml") == -1)
                {
                    pathName = pathName + ".xml";
                }
            }
            else
            {
                return -1;
            }

            #region ��ȡ�����ļ�·��

            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            doc.Load(Application.StartupPath + "\\url.xml");

            System.Xml.XmlNode node = doc.SelectSingleNode("//dir");
            if (node == null)
            {
                MessageBox.Show(Language.Msg("url����dir������"));
                return -1;
            }

            this.serverPath = node.InnerText;
            
            this.configPath = "FpSetting/" + pathName; //Զ�������ļ��� 

            #endregion

            try
            {
                doc.Load(serverPath + configPath);

                if (!isDetail)
                {
                    frmSetConfig.SetFpByConfig(this.neuSpread1_Sheet1, doc);
                }
                else
                {
                    frmSetConfig.SetFpByConfig(this.neuSpread2_Sheet1, doc);
                }

                this.hsFpPathConfig.Add(this.privIndex, doc);
            }
            catch (Exception ex)
            {
                MessageBox.Show(Language.Msg("װ��HisProfile.xmlʧ�ܣ�\n" + ex.Message));
                return -1;
            }

            return 1;
        }

        #endregion

        #region �¼�

        private void ucGeneralQuery_Load(object sender, EventArgs e)
        {
            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToUpper() != "DEVENV")
            {
                this.InitData();

                if (this.InitPrivDept() == 1)
                {
                    this.operDept = this.tvDept.SelectedNode.Tag as FS.FrameWork.Models.NeuObject;

                    this.InitPrivOper();
                }
            }
        }

        private void tvDept_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //����Աѡ��Ŀ��Ҵ���
            if (e.Node != null && e.Node.Tag != null)
            {
                this.operDept = e.Node.Tag as FS.FrameWork.Models.NeuObject;

                this.InitPrivOper();                
            }
        }

        private void tvOper_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //ȡ�ڵ�����
            if (e.Node.Tag != null)
            {
                this.operPriv = e.Node.Tag as FS.HISFC.Models.Admin.PowerLevelClass3;

                this.SetToolButton();

                this.ShowData();
            }
        }

        private void txtFilter_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Down)
            {
                this.neuSpread1_Sheet1.ActiveRowIndex++;
                return;
            }

            if (e.KeyData == Keys.Up)
            {
                this.neuSpread1_Sheet1.ActiveRowIndex--;
                return;
            }
        }

        private void txtFilter_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (this.neuSpread1_Sheet1.RowCount == 0) 
                    return;

                if (this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, 0].Text == "�ϼ�:" || this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, 1].Text == "�ϼ�:")
                    return;

                //��ʾ��ϸ��Ϣ
                this.ShowDetail();
            }
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            this.Filter();
        }

        private void neuTabControl1_SelectionChanged(object sender, EventArgs e)
        {
            if (this.neuTabControl1.SelectedIndex == 0)
            {
                if (this.neuTabControl1.Contains(this.tpDetail))
                    this.neuTabControl1.TabPages.Remove(this.tpDetail);
            }
        }

        private void fpSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.exeSqlIndex == "AdjustByCompany")
                return;

            if (this.operPriv.ID != "In")
            {
                return;
            }

            if (e.ColumnHeader)
                return;

            if (this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, 0].Text == "�ϼ�:" || this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, 1].Text == "�ϼ�:") 
                return;

            //��ʾ��ϸ��Ϣ
            this.ShowDetail();
        }


        #endregion
          
        /// <summary>
        /// ���������� 
        /// </summary>
        private enum AssortType
        {
            /// <summary>
            /// ��ҩƷ
            /// </summary>
            Drug,
            /// <summary>
            /// ��������λ
            /// </summary>
            Company,
            /// <summary>
            /// ������
            /// </summary>
            Bill
        }
    }
}
