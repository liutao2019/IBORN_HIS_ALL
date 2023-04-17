using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Function;
using FS.FrameWork.Management;

namespace FS.HISFC.Components.Pharmacy.Plan
{
    /// <summary>
    /// [��������: ҩƷ���ƻ�]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2006-12]<br></br>
    /// <�޸ļ�¼>
    ///    1.ҩƷ��ӡ�ӿڵ��ó���Bug�������ͬʱ�������ƻ��Ͳɹ��ƻ������й���ӡ������£�
    ///      �л�������ӡ�ͻ������һ����ӡ�ӿ�ʵ��by Sunjh 2010-8-26 {D78A574D-59BE-491b-808C-38DCD26BA5EA}
    ///    2.�޸���ʾ���� by Sunjh 2010-9-6 {1C29C7AC-D178-4caf-915C-B1E824014B78}
    ///    3.���ƻ�����ҩƷ�����ѯ���� by Sunjh 2010-11-4 {03133950-80B6-4df1-8087-2550F8A0DC69}
    /// </�޸ļ�¼>
    /// </summary>
    public partial class ucInPlan : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.FrameWork.WinForms.Forms.IInterfaceContainer,
                                        FS.FrameWork.WinForms.Classes.IPreArrange
    {
        public ucInPlan()
        {
            InitializeComponent();
        }

        #region �����

        /// <summary>
        /// Ȩ�޿���
        /// </summary>
        private FS.FrameWork.Models.NeuObject privDept = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// ���ݱ�
        /// </summary>
        private DataTable dt = new DataTable();

        private FarPoint.Win.Spread.CellType.NumberCellType numCellType = new FarPoint.Win.Spread.CellType.NumberCellType();

        /// <summary>
        /// ���ڼ����վ���������������
        /// </summary>
        private int outday = 30;       

        /// <summary>
        /// ���Ұ�����
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper deptHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// ��Ա������
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper personHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// �������Ұ�����
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper produceHelpter = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// ҩƷ������
        /// </summary>
        private FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();

        /// <summary>
        /// �洢�ƻ�����
        /// </summary>
        private System.Collections.Hashtable hsPlanData = new Hashtable();

        /// <summary>
        /// �Ƿ�Լƻ�����Ϊ0������Ч���ж� 
        /// </summary>
        private bool isJudgeValid = true;

        /// <summary>
        /// ��ǰ��������
        /// </summary>
        private string nowBillNO = "";

        /// <summary>
        /// �ƻ����� ���ƻ������� 0 �ֹ��ƻ� 1 ������ 2 ���� 3 ʱ�� 4 ������ 5 ģ��
        /// </summary>
        private string planType = "0";

        /// <summary>
        /// �Ƿ�Լƻ���Ϊ������ж�
        /// </summary>
        private bool isCheckNumZero = true;

        /// <summary>
        /// �Ƿ���ʾ�Զ�����
        /// </summary>
        private bool isShowCustomCode = false;

        /// <summary>
        /// �Ƿ���ʾ���ƿ��
        /// </summary>
        private bool isShowOwnStock = false;

        /// <summary>
        /// �Ƿ���ʾȫԺ���
        /// </summary>
        private bool isShowAllStock = false;

        /// <summary>
        /// �Ƿ�ʹ���ֵ���Ϣ��Ĭ�ϵĹ�����˾/�����
        /// </summary>
        private bool isUseDefaultStockData = false;

        /// <summary>
        /// ��Ա�����ֵ���Ϣ
        /// </summary>
        private Dictionary<string, string> personNameDictionary = new Dictionary<string, string>();

        #endregion

        #region ����

        /// <summary>
        /// ���ڼ����վ��������������� ͳ������
        /// </summary>
        [Description("���ڼ����վ��������������� ͳ������"), Category("����"),Browsable(false)]
        public int Outday
        {
            get
            {
                return this.outday;
            }
            set
            {
                this.outday = value;
            }
        }

        /// <summary>
        /// �������
        /// </summary>
        [Description("������� ���ݲ�ͬҽԺ��������"), Category("����"), DefaultValue("���ƻ���")]
        public string Title
        {
            get
            {
                return this.lbTitle.Text;
            }
            set
            {
                this.lbTitle.Text = value;
            }
        }

        /// <summary>
        /// �Ƿ�Լƻ�����Ϊ0������Ч���ж�
        /// </summary>
        [Description("�Ƿ������Ч���ж�"), Category("����"), DefaultValue(true)]
        public bool IsJudgeValid
        {
            get
            {
                return this.isJudgeValid;
            }
            set
            {
                this.isJudgeValid = value;
            }
        }

        /// <summary>
        /// �Ƿ���ʾ�б���
        /// </summary>
        [Description("�б�ѡ��ؼ��Ƿ���ʾ�б���"), Category("����"), DefaultValue(true), Browsable(false)]
        public bool IsShowRowHeader
        {
            get
            {
                return this.ucDrugList1.ShowFpRowHeader;
            }
            set
            {
                this.ucDrugList1.ShowFpRowHeader = value;
            }
        }

        /// <summary>
        /// �Ƿ�����ͨ��������ȷ��ѡ������
        /// </summary>
        [Description("�б�ѡ��ؼ��Ƿ�����ͨ��������ȷ��ѡ������"), Category("����"), DefaultValue(false), Browsable(false)]
        public bool IsSelectByNumber
        {
            get
            {
                return this.ucDrugList1.IsUseNumChooseData;
            }
            set
            {
                this.ucDrugList1.IsUseNumChooseData = value;
            }
        }

        /// <summary>
        /// �Ƿ�Լƻ����Ƿ�Ϊ������ж�
        /// </summary>
        [Description("�Ƿ�Լƻ����Ƿ�Ϊ������ж�"), Category("����"), DefaultValue(false), Browsable(false)]
        public bool IsCheckNumZero
        {
            get
            {
                return this.isCheckNumZero;
            }
            set
            {
                this.isCheckNumZero = value;
            }
        }

        /// <summary>
        /// �Ƿ�ʹ���ֵ���Ϣ��Ĭ�ϵĹ�����˾/�����
        /// </summary>
        [Browsable(false)]
        public bool UseDefaultStockData
        {
            get
            {
                return this.isUseDefaultStockData;
            }
            set
            {
                this.isUseDefaultStockData = value;
            }
        }

        /// <summary>
        /// �Ƿ���ʾ�ƻ����б�
        /// </summary>
        [Browsable(false)]
        public bool IsShowList
        {
            get
            {
                return this.ucDrugList1.ShowTreeView;
            }
            set
            {
                this.ucDrugList1.ShowTreeView = value;

                this.SetToolButton(value);
            }
        }

        #endregion

        #region ������

        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("��    ��", "�½��ƻ���", FS.FrameWork.WinForms.Classes.EnumImageList.X�½�, true, false, null);
            toolBarService.AddToolButton("�ƻ�ģ��", "����ģ�����ɼƻ���", FS.FrameWork.WinForms.Classes.EnumImageList.Z����, true, false, null);
            toolBarService.AddToolButton("�� �� ��", "����ģ�����ɼƻ���", FS.FrameWork.WinForms.Classes.EnumImageList.B����, true, false, null);
            toolBarService.AddToolButton("�� �� ��", "����ģ�����ɼƻ���", FS.FrameWork.WinForms.Classes.EnumImageList.R������, true, false, null);
            toolBarService.AddToolButton("����ɾ��", "ɾ�������ƻ���", FS.FrameWork.WinForms.Classes.EnumImageList.Qȡ��, true, false, null);
            toolBarService.AddToolButton("�� �� ��", "�ƻ����б�", FS.FrameWork.WinForms.Classes.EnumImageList.X��Ϣ, true, false, null);
            
            //toolBarService.AddToolButton("��    ��", "���üƻ�����������", FS.FrameWork.WinForms.Classes.EnumImageList.D��ѯ��ʷ, true, false, null);
            
            toolBarService.AddToolButton("ɾ    ��", "ɾ����ǰѡ��ļƻ�ҩƷ", FS.FrameWork.WinForms.Classes.EnumImageList.Sɾ��, true, false, null);
            toolBarService.AddToolButton("��    ��", "�Լƻ�����ҩƷ��Ϣ��������", FS.FrameWork.WinForms.Classes.EnumImageList.YԤԼ, true, false, null);
            toolBarService.AddToolButton("�� �� ��", "���û��ܿ������뵥", FS.FrameWork.WinForms.Classes.EnumImageList.C����, true, false, null);

            return toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "ɾ    ��")
            {
                this.DeleteData();
            }
            if (e.ClickedItem.Text == "����ɾ��")
            {
                this.DeleteDataByBill(this.privDept.ID, this.nowBillNO);
            }
            if (e.ClickedItem.Text == "�� �� ��")
            {
                this.tvList.ShowInPlanList(this.privDept, "0");

                this.IsShowList = true;
            }
            if (e.ClickedItem.Text == "��    ��")
            {
                this.New();
            }
            if (e.ClickedItem.Text == "�ƻ�ģ��")
            {
                this.planType = "5";
                this.AddStencilData();
            }
            if (e.ClickedItem.Text == "�� �� ��")
            {
                this.planType = "1";
                this.AddAlterData("0");
            }
            if (e.ClickedItem.Text == "�� �� ��")
            {
                this.planType = "2";
                this.AddAlterData("1");
            }
            if (e.ClickedItem.Text == "��    ��")
            {
                this.Sort();
            }
            if (e.ClickedItem.Text == "�� �� ��")
            {
                this.AddTotalApplyData();
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        protected override int OnSave(object sender, object neuObject)
        {
            if (this.Save() == 1)
            {
                this.IsShowList = true;
            }
            return 1;
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
            ArrayList alPlan = new ArrayList();

            foreach (DataRow dr in this.dt.Rows)
            {
                FS.HISFC.Models.Pharmacy.InPlan inPlan = this.GetDataFromRow(dr);

                alPlan.Add(inPlan);
            }

            this.Print(alPlan,false);

            return 1;
        }

        public override int SetPrint(object sender, object neuObject)
        {
            return 1;
        }

        /// <summary>
        /// ���ù�������ť״̬
        /// </summary>
        /// <param name="isShowList">�Ƿ���ʾ�̵㵥�б�</param>
        protected void SetToolButton(bool isShowList)
        {
            this.toolBarService.SetToolButtonEnabled("�� �� ��", !isShowList);
            this.toolBarService.SetToolButtonEnabled("��    ��", isShowList);
            this.toolBarService.SetToolButtonEnabled("����ɾ��", isShowList);
            this.toolBarService.SetToolButtonEnabled("�ƻ�ģ��", !isShowList);
            this.toolBarService.SetToolButtonEnabled("�� �� ��", !isShowList);
            this.toolBarService.SetToolButtonEnabled("�� �� ��", !isShowList);
        }

        #endregion

        #region ���ݱ��ʼ��

        /// <summary>
        /// ���Ʋ�����ʼ��
        /// </summary>
        private void InitControlParam()
        {
            FS.HISFC.BizProcess.Integrate.Common.ControlParam ctrlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

            this.Outday = ctrlParamIntegrate.GetControlParam<int>(FS.HISFC.BizProcess.Integrate.PharmacyConstant.Plan_Expand_Days, true, 30);
            this.IsShowRowHeader = ctrlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.PharmacyConstant.Plan_Show_RowHeader, true, true);
            this.IsSelectByNumber = ctrlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.PharmacyConstant.Plan_Num_SelectRow, true, false);
            this.IsCheckNumZero = ctrlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.PharmacyConstant.Plan_NumZero_Valid, true, true);
            this.UseDefaultStockData = ctrlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.PharmacyConstant.Stock_Use_DefaultData, true, true);
            //minihisȡ��config�����ļ�������
            //this.InitConfig();
        }

        /// <summary>
        /// ���ò�����ȡ
        /// </summary>
        private void InitConfig()
        {
            HISFC.Components.Pharmacy.Function fun = new Function();
            System.Xml.XmlDocument doc = fun.GetConfig();

            if (doc != null)
            {
                System.Xml.XmlNode valueNode = doc.SelectSingleNode("/Setting/Group[@ID='Pharmacy']/Fun[@ID='InPlan']");
                if (valueNode != null)
                {
                    this.isShowCustomCode = NConvert.ToBoolean(valueNode.Attributes["IsShowCustomCode"].Value);
                    this.isShowOwnStock = NConvert.ToBoolean(valueNode.Attributes["IsShowOwnStock"].Value);
                    this.isShowAllStock = NConvert.ToBoolean(valueNode.Attributes["IsShowAllStock"].Value);
                }

                this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColUserCode].Visible = this.isShowCustomCode;
                this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColOwnStockNum].Visible = this.isShowOwnStock;
                this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColAllStockNum].Visible = this.isShowAllStock;
            }
        }

        /// <summary>
        /// ���ݳ�ʼ��
        /// </summary>
        private void InitData()
        {
            FarPoint.Win.Spread.InputMap im;
            im = this.neuSpread1.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused);
            im.Put(new FarPoint.Win.Spread.Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            #region �������ݻ�ȡ

            //��ÿ�������
            FS.HISFC.BizLogic.Manager.Department deptManager = new FS.HISFC.BizLogic.Manager.Department();
            ArrayList deptAll = deptManager.GetDeptmentAll();
            if (deptAll == null)
            {
                MessageBox.Show(Language.Msg("������ÿ����б����" + deptManager.Err));
                return;
            }
            this.deptHelper.ArrayObject = deptAll;
            //��ò���Ա����
            FS.HISFC.BizLogic.Manager.Person personManager = new FS.HISFC.BizLogic.Manager.Person();
            ArrayList personAl = personManager.GetEmployeeAll();
            if (personAl == null)
            {
                MessageBox.Show(Language.Msg("��ȡȫ����Ա�б����!" + personManager.Err));
                return;
            }
            this.personHelper.ArrayObject = personAl;
            //��ȡ��������
            FS.HISFC.BizLogic.Pharmacy.Constant phaConsManager = new FS.HISFC.BizLogic.Pharmacy.Constant();
            ArrayList produceAl = phaConsManager.QueryCompany("0");
            if (produceAl == null)
            {
                MessageBox.Show(Language.Msg("��ȡ���������б����!" + phaConsManager.Err));
                return;
            }
            this.produceHelpter.ArrayObject = produceAl;

            #endregion
            //{E215BCFB-9D4B-418c-9C12-AC6E0242FB7F}
            this.ucDrugList1.DeptCode = this.privDept.ID;

            this.ucDrugList1.ShowPharmacyList();
        }

        /// <summary>
        /// ���ݱ��ʼ��
        /// </summary>
        private void InitDataTable()
        {
            //��������
            System.Type dtStr = System.Type.GetType("System.String");
            System.Type dtDec = System.Type.GetType("System.Decimal");
            System.Type dtBol = System.Type.GetType("System.Boolean");

            //��myDataTable�������
            this.dt.Columns.AddRange(new DataColumn[] {
                                                                        new DataColumn("�Զ�����",    dtStr),
                                                                        new DataColumn("��Ʒ����",	  dtStr),
                                                                        new DataColumn("���",        dtStr),
                                                                        new DataColumn("��װ����",    dtDec),
                                                                        new DataColumn("�ƻ������",  dtDec),
                                                                        new DataColumn("�ƻ�����",	  dtDec),
                                                                        new DataColumn("��λ",        dtStr),
                                                                        new DataColumn("�ƻ����",	  dtDec),
                                                                        new DataColumn("���ƿ��",	  dtDec),
                                                                        new DataColumn("ȫԺ���",	  dtDec),
                                                                        new DataColumn("��������",	  dtDec),
                                                                        new DataColumn("�վ�����",	  dtDec),
                                                                        new DataColumn("������˾",    dtStr),
                                                                        new DataColumn("��������",    dtStr),
                                                                        new DataColumn("��ע",        dtStr),
                                                                        new DataColumn("ҩƷ����",	  dtStr),
                                                                        new DataColumn("ƴ����",      dtStr),
                                                                        new DataColumn("�����",      dtStr)                                                                        
                                                                    });

            this.dt.DefaultView.AllowNew = true;
            this.dt.DefaultView.AllowEdit = true;
            this.dt.DefaultView.AllowDelete = true;

            //�趨���ڶ�DataView�����ظ��м���������
            DataColumn[] keys = new DataColumn[1];
            keys[0] = this.dt.Columns["ҩƷ����"];
            this.dt.PrimaryKey = keys;

            this.neuSpread1_Sheet1.DataSource = this.dt.DefaultView;

            this.SetFormat();
        }

        /// <summary>
        /// Fp��ʽ��
        /// </summary>
        private void SetFormat()
        {
            this.numCellType.DecimalPlaces = 4;

            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColPurchasePrice].CellType = this.numCellType;

            this.neuSpread1_Sheet1.DefaultStyle.Locked = true;

            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColTradeName].Width = 120F;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColSpecs].Width = 80F;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColPurchasePrice].Width = 100F;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColPlanNum].Width = 80F;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColPlanCost].Width = 100F;

            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColDrugNO].Visible = false;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColOutDay].Visible = false;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColAllStockNum].Visible = false;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColOutTotal].Visible = false;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColOwnStockNum].Visible = false;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColSpellCode].Visible = false;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColWBCode].Visible = false;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColUserCode].Visible = false;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColUserCode].CellType = new FarPoint.Win.Spread.CellType.TextCellType();

            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColPackQty].Visible = false;

            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColPlanNum].Locked = false;
        }

        #endregion   

        #region �б��ʼ��

        /// <summary>
        /// �̵㵥�б������
        /// </summary>
        private tvPlanList tvList = null;

        /// <summary>
        /// �̵㵥�б��ʼ��
        /// </summary>
        protected void InitPlanList()
        {
            this.tvList = new tvPlanList();
            this.ucDrugList1.TreeView = this.tvList;

            this.tvList.AfterSelect -= new TreeViewEventHandler(tvList_AfterSelect);
            this.tvList.AfterSelect += new TreeViewEventHandler(tvList_AfterSelect);

            this.tvList.DoubleClick -= new EventHandler(tvList_DoubleClick);
            this.tvList.DoubleClick += new EventHandler(tvList_DoubleClick);

            this.ucDrugList1.Caption = "�ƻ����б�";

            this.ShowPlanList();

            this.ucDrugList1.ShowTreeView = true;
        }      

        /// <summary>
        /// �̵㵥�б���ʾ
        /// </summary>
        private void ShowPlanList()
        {
            this.tvList.ShowInPlanList(this.privDept, "0");
        }

        #endregion

        #region ����

        /// <summary>
        /// �����ݱ��ڼ�������
        /// </summary>
        /// <param name="inPlan"></param>
        private int AddDataToTable(FS.HISFC.Models.Pharmacy.InPlan inPlan)
        {
            try
            {
                if (inPlan.Item.PriceCollection.PurchasePrice == 0)
                    inPlan.Item.PriceCollection.PurchasePrice = inPlan.Item.PriceCollection.RetailPrice;

                decimal planCost = inPlan.PlanQty / inPlan.Item.PackQty * inPlan.Item.PriceCollection.PurchasePrice;
                if (this.produceHelpter != null)
                    inPlan.Item.Product.Producer.Name = this.produceHelpter.GetName(inPlan.Item.Product.Producer.ID);               

                this.dt.Rows.Add(new object[] { 
                                                inPlan.Item.NameCollection.UserCode,        //�Զ�����
                                                inPlan.Item.Name,                           //��Ʒ����
                                                inPlan.Item.Specs,                          //���
                                                inPlan.Item.PackQty,                        //��װ����
                                                inPlan.Item.PriceCollection.PurchasePrice,  //�ƻ������
                                                inPlan.PlanQty / inPlan.Item.PackQty,       //�ƻ�����
                                                inPlan.Item.PackUnit,                       //��λ
                                                planCost,                                   //�ƻ�����  
                                                inPlan.StoreQty / inPlan.Item.PackQty,      //���ƿ��
                                                inPlan.StoreTotQty / inPlan.Item.PackQty,   //ȫԺ���
                                                inPlan.OutputQty / inPlan.Item.PackQty,     //��������
                                                0,                                          //�վ�����
                                                inPlan.Item.Product.Company.Name,           //������˾
                                                inPlan.Item.Product.Producer.Name,          //��������
                                                inPlan.Memo,                                //��ע
                                                inPlan.Item.ID,                             //ҩƷ����
                                                inPlan.Item.NameCollection.SpellCode,       //ƴ����
                                                inPlan.Item.NameCollection.WBCode          //�����                                                
                                           });
                this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColPurchasePrice].CellType = this.numCellType;//{1EC17564-2FAD-4a77-97AC-4C57076888B2}
            }
            catch (System.Data.DataException e)
            {
                System.Windows.Forms.MessageBox.Show(FS.FrameWork.Management.Language.Msg("DataTable�ڸ�ֵ��������" + e.Message));

                return -1;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(FS.FrameWork.Management.Language.Msg("DataTable�ڸ�ֵ��������" + ex.Message));

                return -1;
            }

            return 1;
        }

        /// <summary>
        /// ���������ڻ�ȡ���ƻ�����
        /// </summary>
        /// <param name="dr">������</param>
        /// <returns>�ɹ�����1 ʧ�ܷ��أ�1</returns>
        private FS.HISFC.Models.Pharmacy.InPlan GetDataFromRow(DataRow dr)
        {
            FS.HISFC.Models.Pharmacy.InPlan inPlan = this.hsPlanData[dr["ҩƷ����"].ToString()] as FS.HISFC.Models.Pharmacy.InPlan;

            inPlan.BillNO = this.nowBillNO;                     //�ƻ�����
            inPlan.State = "0";                                 //����״̬
            inPlan.PlanType = this.planType;                    //�ɹ�����

            inPlan.PlanQty = NConvert.ToDecimal(dr["�ƻ�����"]) * inPlan.Item.PackQty;//�ƻ�����

            inPlan.PlanOper.ID = this.itemManager.Operator.ID;

            inPlan.Oper = inPlan.PlanOper;
            inPlan.Memo = dr["��ע"].ToString();                //��ע
            
            inPlan.Dept = this.privDept;//{AB8CE85E-DED7-4f12-A6B7-BFD76A70C6F9} �������ƻ���ʱ�����������¸�ֵ

            return inPlan;
        }

        /// <summary>
        /// ������ƻ�ҩƷ��Ϣ
        /// </summary>
        /// <returns>�ɹ���ӷ���1 ʧ�ܷ��أ�1</returns>
        public void Clear()
        {
            this.dt.Rows.Clear();
            this.dt.AcceptChanges();

            this.hsPlanData.Clear();

            this.lbPlanBill.Text = "���ݺ�:";
            this.lbPlanInfo.Text = "�ƻ����� �ƻ���";

            this.txtFilter.Text = "";
        }

        /// <summary>
        /// ��Ч���ж�
        /// </summary>
        /// <returns> </returns>
        private bool IsValid()
        {
            if (this.isJudgeValid)
            {
                this.dt.AcceptChanges();
                foreach(DataRow dr in this.dt.Rows)
                {
                    if (NConvert.ToDecimal(dr["�ƻ�����"]) < 0)
                    {
                        MessageBox.Show("������ " + dr["��Ʒ����"].ToString() + " �ƻ�����");
                        return false;
                    }
                    if (this.isCheckNumZero && (NConvert.ToDecimal(dr["�ƻ�����"]) == 0))
                    {
                        MessageBox.Show("������ " + dr["��Ʒ����"].ToString() + " �ƻ����� �ƻ�������Ϊ��");
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="isFpFocus"></param>
        public void SetFocus(bool isFpFocus)
        {
            if (isFpFocus)
            {
                this.neuSpread1.Select();                
                this.neuSpread1_Sheet1.ActiveColumnIndex = (int)ColumnSet.ColPlanNum;
            }
            else
            {
                this.ucDrugList1.Select();
                this.ucDrugList1.SetFocusSelect();
            }
        }

        /// <summary>
        /// ������һ�����ƻ���
        /// </summary>
        public void New()
        {
            //�������б��в����½ڵ�
            TreeNode node = new TreeNode();
            node.Text = "�½����ƻ���";
            node.ImageIndex = 4;
            node.SelectedImageIndex = 4;
            node.Tag = new FS.HISFC.Models.Pharmacy.InPlan();

            this.tvList.Nodes[0].Nodes.Insert(0, node);

            //ѡ�д��½ڵ�
            this.tvList.SelectedNode = node;//this.ucChooseList.tvList.Nodes[0].Nodes[0];

            //�л���ҩƷ�����б�
            this.IsShowList = false;

            this.ucDrugList1.SetFocusSelect();
        }

        /// <summary>
        /// ��ҩƷʵ�����
        /// </summary>
        /// <param name="item">ҩƷʵ��</param>
        /// <param name="totOutQty">��������</param>
        /// <param name="averageOutQty">�վ�����</param>
        /// <param name="planQty">���ݾ������Զ����ɵļƻ�������</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        public int AddDrugData(FS.HISFC.Models.Pharmacy.Item item,decimal totOutQty,decimal averageOutQty,decimal planQty)
        {
            if (this.hsPlanData.ContainsKey(item.ID))
            {
                MessageBox.Show(Language.Msg("��ҩƷ����ӵ��ƻ����� ͬһƷ��ҩ�����ظ����"));
                return 0;
            }

            //��ȡȫԺ�����
            decimal itemSum = 0, itemTotSum = 0;

            if (this.itemManager.FindSum(this.privDept.ID, item.ID, ref itemSum, ref itemTotSum) == -1)
            {
                MessageBox.Show(Language.Msg("��ȡ��" + item.Name + "���������ʱ��������" + this.itemManager.Err));
                return -1;
            }

            FS.HISFC.Models.Pharmacy.InPlan inPlan = new FS.HISFC.Models.Pharmacy.InPlan();

            inPlan.Item = item;
            inPlan.StoreTotQty = itemTotSum;
            inPlan.StoreQty = itemSum;
            inPlan.PlanQty = planQty;

            inPlan.Dept = this.privDept;

            #region ��ȡ��ʷ�ɹ���Ϣ

            if (!this.isUseDefaultStockData)        //��ʾ��һ�εĹ�����Ϣ
            {
                ArrayList alHistory = this.itemManager.QueryHistoryStockPlan(this.privDept.ID, item.ID);
                if (alHistory == null)
                {
                    Function.ShowMsg("��ȡ��ʷ�ɹ���Ϣ����" + this.itemManager.Err);
                    return -1;
                }

                if (alHistory.Count > 0)
                {
                    FS.HISFC.Models.Pharmacy.StockPlan stockTemp = alHistory[0] as FS.HISFC.Models.Pharmacy.StockPlan;

                    inPlan.Item.Product.Company = stockTemp.Company;
                    inPlan.Item.Product.Producer = stockTemp.Item.Product.Producer;
                    inPlan.Item.PriceCollection.PurchasePrice = stockTemp.StockPrice;
                }
            }

            #endregion

            if (this.AddDataToTable(inPlan) == 1)
            {
                this.hsPlanData.Add(inPlan.Item.ID, inPlan);
            }

            this.SetSum();

            return 1;
        }

        /// <summary>
        /// ��ҩƷʵ�����
        /// </summary>
        /// <param name="item">ҩƷʵ��</param>
        /// <param name="planQty">���������Զ����ɵļƻ�������</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        public int AddDrugData(FS.HISFC.Models.Pharmacy.Item item,decimal planQty)
        {
            return this.AddDrugData(item, 0, 0, planQty);
        }

        /// <summary>
        /// ��ҩƷʵ�����
        /// </summary>
        /// <param name="item">ҩƷʵ��</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        public int AddDrugData(FS.HISFC.Models.Pharmacy.Item item)
        {
            return this.AddDrugData(item, 0, 0, 0);
        }

        #region ���վ�����/������ ��������

        ///<summary>
        ///����ҩƷ�����߼�������
        ///</summary>
        ///<param name="alterFlag">���ɷ�ʽ 0 ������ 1 ������</param>
        ///<returns>�ɹ�����0��ʧ�ܷ��أ�1</returns>
        public void AddAlterData(string alterFlag)
        {
            DialogResult result = DialogResult.Yes;
            if (this.dt.Rows.Count > 0)
            {
                //�޸���ʾ���� by Sunjh 2010-9-6 {1C29C7AC-D178-4caf-915C-B1E824014B78}
                result = MessageBox.Show(Language.Msg("�Ƿ������ǰ���ƻ������ݣ�"), "", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.RightAlign);

                if (result == DialogResult.Cancel)
                {
                    return;
                }
            }

            if (result == DialogResult.Yes)
            {
                //�������
                this.Clear();
            }

            try
            {
                ArrayList alDetail = new ArrayList();

                if (alterFlag == "1")
                {
                    #region �������� ���������Ĳ��� ������������Ϣ
                    using (ucPhaAlter uc = new ucPhaAlter())
                    {
                        uc.DeptCode = this.privDept.ID;
                        uc.SetData();
                        uc.Focus();
                       FS.FrameWork.WinForms.Classes.Function.PopShowControl(uc);
                       if (uc.rs == DialogResult.Cancel)
                       {
                           return;
                       }
                        if (uc.ApplyInfo != null)
                        {
                            alDetail = uc.ApplyInfo;
                        }
                    }
                    #endregion
                }
                else
                {
                    #region �����߼���

                    ////{F4D82F23-CCDC-45a6-86A1-95D41EF856B8} ���ĵ��ú���
                    alDetail = this.itemManager.QueryDrugListByNumAlter(this.privDept.ID);
                    if (alDetail == null)
                    {
                        MessageBox.Show(Language.Msg("��������������ִ����Ϣ����δ��ȷִ��\n" + this.itemManager.Err));
                        return;
                    }

                    #endregion
                }

                if (alDetail.Count == 0)
                {
                    MessageBox.Show(Language.Msg("������������ҩƷ�ƻ���Ϣ"));
                    return;
                }

                if (result == DialogResult.Yes)
                {
                    #region ���ԭ���ݣ�ʹ�þ�����/����������

                    FS.HISFC.Models.Pharmacy.Item item = new FS.HISFC.Models.Pharmacy.Item();

                    foreach (FS.FrameWork.Models.NeuObject temp in alDetail)
                    {
                        item = this.itemManager.GetItem(temp.ID);
                        if (item == null)
                        {
                            MessageBox.Show(Language.Msg("��ȡҩƷ������Ϣʧ�ܣ�[" + temp.Name + "]ҩƷ������! \n ����ҩѧ����ϵ"));
                            continue;
                        }

                        if (alterFlag == "1")
                            this.AddDrugData(item, NConvert.ToDecimal(temp.User01), NConvert.ToDecimal(temp.User02), NConvert.ToDecimal(temp.User03));
                        else
                            this.AddDrugData(item, NConvert.ToDecimal(temp.User03));
                    }

                    #endregion
                }

                if (result == DialogResult.No)
                {
                    #region ��ԭ���ݻ����ϣ�ʹ�þ���������

                    System.Collections.Hashtable hsAlterList = new Hashtable();

                    foreach (FS.FrameWork.Models.NeuObject temp in alDetail)
                    {
                        hsAlterList.Add(temp.ID, temp);
                    }

                    this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColPlanNum].Locked = true;

                    foreach (DataRow dr in this.dt.Rows)
                    {
                        if (hsAlterList.ContainsKey(dr["ҩƷ����"].ToString()))
                        {
                            FS.FrameWork.Models.NeuObject info = hsAlterList[dr["ҩƷ����"].ToString()] as FS.FrameWork.Models.NeuObject;

                            dr["�ƻ�����"] = System.Math.Ceiling(NConvert.ToDecimal(info.User03) / NConvert.ToDecimal(dr["��װ����"]));                           
                        }
                    }

                    this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColPlanNum].Locked = false;

                    #endregion
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region ���ܿ�����������

        /// <summary>
        /// ��������ѡ��ؼ�
        /// </summary>
        ucPhaApplyList ucApply = null;

        /// <summary>
        /// ���ܿ�����������
        /// </summary>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        public int AddTotalApplyData()
        {
            if (this.ucApply == null)
            {
                this.ucApply = new ucPhaApplyList();
                this.ucApply.Init();
            }

            string class3MeaningCode = FS.HISFC.Models.Base.EnumIMAInTypeService.GetNameFromEnum(FS.HISFC.Models.Base.EnumIMAInType.InnerApply);

            this.ucApply.QueryApplyListByTarget(this.privDept, class3MeaningCode, "0");
            FS.FrameWork.WinForms.Classes.Function.PopForm.Text = "����������Ϣ����";
            FS.FrameWork.WinForms.Classes.Function.PopShowControl(this.ucApply);

            if (this.ucApply.Result == DialogResult.OK)
            {
                System.Collections.Generic.Dictionary<string, FS.HISFC.Models.Pharmacy.Item> hsApplyData = new Dictionary<string, FS.HISFC.Models.Pharmacy.Item>();

                foreach (FS.FrameWork.Models.NeuObject info in this.ucApply.ApplyListCollection)
                {
                    ArrayList alTempData = this.itemManager.QueryApplyOutInfoByListCode(info.Memo, info.ID, "0");
                    if (alTempData == null)
                    {
                        MessageBox.Show("���ؿ���������Ϣ��������" + this.itemManager.Err);
                        return -1;
                    }
                    //��ȥ�ѷ���׼��   {78801D5B-DE2D-4e59-9181-EB09AE5F0118}
                    foreach (FS.HISFC.Models.Pharmacy.ApplyOut applyData in alTempData)
                    {
                        if (hsApplyData.ContainsKey(applyData.Item.ID))
                        {
                            decimal totApplyQty = FS.FrameWork.Function.NConvert.ToDecimal(hsApplyData[applyData.Item.ID].User01);

                            hsApplyData[applyData.Item.ID].User01 = (totApplyQty + (applyData.Operation.ApplyQty - applyData.Operation.ApproveQty)).ToString();
                        }
                        else
                        {
                            applyData.Item.User01 = (applyData.Operation.ApplyQty - applyData.Operation.ApproveQty).ToString();
                            hsApplyData.Add(applyData.Item.ID, applyData.Item.Clone());
                        }
                    }

                    this.Clear();

                    foreach (string key in hsApplyData.Keys)
                    {
                        FS.HISFC.Models.Pharmacy.Item tempItem = hsApplyData[key];

                        this.AddDrugData(tempItem, FS.FrameWork.Function.NConvert.ToDecimal(tempItem.User01));
                    }
                }
            }

            return 1;
        }

        #endregion

        /// <summary>
        /// ģ��������ʾ
        /// </summary>
        public void AddStencilData()
        {
            DialogResult rs = MessageBox.Show(Language.Msg("����ģ�����ɼƻ���Ϣ�������ǰ��ʾ������ �Ƿ����?"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (rs == DialogResult.No)
                return;

            this.Clear();

            ArrayList alOpenDetail = Function.ChooseDrugStencil(this.privDept.ID,FS.HISFC.Models.Pharmacy.EnumDrugStencil.Plan);

            if (alOpenDetail != null && alOpenDetail.Count > 0)
            {             
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(Language.Msg("���ڸ�����ѡģ�����ɼƻ���Ϣ..."));
                Application.DoEvents();
                //�ȼ��ؿ����Ϣ��Hs ��֤ģ�����˳��
                System.Collections.Hashtable hsStoreDrug = new Hashtable();

                List<FS.HISFC.Models.Pharmacy.Item> alItem = this.itemManager.QueryItemAvailableList(false);
                foreach (FS.HISFC.Models.Pharmacy.Item item in alItem)
                {
                    hsStoreDrug.Add(item.ID, item);
                }

                int i = 0;
                foreach (FS.HISFC.Models.Pharmacy.DrugStencil info in alOpenDetail)
                {
                    FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(i, alOpenDetail.Count);
                    Application.DoEvents();

                    if (hsStoreDrug.Contains(info.Item.ID))
                    {
                        this.AddDrugData(hsStoreDrug[info.Item.ID] as FS.HISFC.Models.Pharmacy.Item);
                    }

                    i++;
                }

                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

                this.SetFocus(true);
            }
        }

        /// <summary>
        /// �������ƻ����� ��ȡ���ƻ�����
        /// </summary>
        /// <param name="privDept">Ȩ�޿���</param>
        /// <param name="billNO">���ݺ�</param>
        public int ShowPlanData(string privDept,string billNO)
        {
            //������ݡ�
            this.Clear();

            //ȡ���ƻ��е�����
            List<FS.HISFC.Models.Pharmacy.InPlan> alDetail = this.itemManager.QueryInPlanDetail(privDept, billNO);
            if (alDetail == null)
            {
                MessageBox.Show(Language.Msg(this.itemManager.Err));
                return -1;
            }

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(Language.Msg("������ʾ�ƻ���ϸ ���Ժ�..."));
            Application.DoEvents();

            foreach (FS.HISFC.Models.Pharmacy.InPlan info in alDetail)
            {
                //��������ɹ��ƻ������ݲ���ʾ 
                if (info.State != "0")
                    continue;

                info.Item = this.itemManager.GetItem(info.Item.ID);
                if (info.Item == null)
                {
                    Function.ShowMsg("��ȡ��ҩƷ��Ϣ�������� "); //+ info.Item.ID);
                    return -1;
                }

                this.SetPlanInfo(info);

                if (this.AddDataToTable(info) == 1)
                {
                    this.hsPlanData.Add(info.Item.ID,info);
                }
                else
                {
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    return -1;
                }
            }

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            
            this.SetSum();

            return 1;
        }

        /// <summary>
        /// ���üƻ���Ϣ��ʾ
        /// </summary>
        /// <param name="inPlan"></param>
        private void SetPlanInfo(FS.HISFC.Models.Pharmacy.InPlan inPlan)
        {
            this.lbPlanBill.Text = "���ݺ�:" + inPlan.BillNO;

            //{B5B12199-4F8C-4a70-B55F-795E13261EAF}  ������ʾ��Ա����
            if (this.personNameDictionary.ContainsKey(inPlan.PlanOper.ID) == false) //�������ƻ�����Ϣ
            {
                FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

                FS.HISFC.Models.Base.Employee planOper = managerIntegrate.GetEmployeeInfo(inPlan.PlanOper.ID);
                if (planOper != null)
                {
                    this.personNameDictionary.Add(planOper.ID, planOper.Name);
                }
            }

            if (this.personNameDictionary.ContainsKey(inPlan.PlanOper.ID))
            {
                this.lbPlanInfo.Text = "�ƻ�����: " + this.privDept.Name + " �ƻ���: " + this.personNameDictionary[inPlan.PlanOper.ID];
            }
            else
            {               
                this.lbPlanInfo.Text = "�ƻ�����: " + this.privDept.Name + " �ƻ���: " + inPlan.PlanOper.ID;
            }            
        }

        /// <summary>
        /// �ƻ��ܽ�����
        /// </summary>
        private void SetSum()
        {
            decimal totCost = 0;

            for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
            {
                totCost += NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.ColPlanCost].Text);
            }

            this.lbCost.Text = "�ƻ��ܽ��:" + totCost.ToString("N");
        }

        /// <summary>
        /// ɾ��һ������
        /// </summary>
        public void DeleteData()
        {
            if (this.neuSpread1_Sheet1.Rows.Count == 1)
            {
                MessageBox.Show(Language.Msg("�ƻ�����ֻ��һ��ҩƷ��¼ ��ѡ������ɾ����ʽ���в���"));
                return;
            }
            if (this.neuSpread1_Sheet1.Rows.Count == 0) 
                return;

            if (MessageBox.Show("ȷ��ɾ��ѡ���������?", "��ʾ", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }

            this.neuSpread1.StopCellEditing();

            string drugNO = this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, (int)ColumnSet.ColDrugNO].Text;
            if (this.hsPlanData.ContainsKey(drugNO))
            {
                this.hsPlanData.Remove(drugNO);
            }

            this.neuSpread1_Sheet1.Rows.Remove(this.neuSpread1_Sheet1.ActiveRowIndex, 1);

            this.neuSpread1.StartCellEditing(null, false);
        }

        /// <summary>
        /// �����ƻ�����������ɾ��
        /// </summary>
        /// <param name="deptCode">�ⷿ����</param>
        /// <param name="billCode">���ƻ�����</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        public int DeleteDataByBill(string deptCode, string billCode)
        {
            if (this.nowBillNO == "")
                return 0;

            DialogResult result;
            //��ʾ�û��Ƿ�ȷ��ɾ��
            result = MessageBox.Show(Language.Msg("ȷ��ɾ����" + this.nowBillNO + "���ƻ�����\n �˲����޷�����"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (result == DialogResult.No)
            {
                return 0;
            }

            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
            //t.BeginTransaction();
            this.itemManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            try
            {
                int parm = this.itemManager.DeleteInPlan(deptCode, billCode,"0");
                if (parm == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(this.itemManager.Err);
                    return -1;
                }
                else
                    if (parm != this.dt.Rows.Count)
                    { //������
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(Language.Msg("���ݷ����䶯����ˢ�´���"));
                        return -1;
                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            this.tvList.ShowInPlanList(this.privDept, "0");

            return 1;
        }

        /// <summary>
        /// �������ƻ���
        /// </summary>
        /// <param name="billCode">���ƻ�����</param>
        /// <param name="deptCode">���ұ���</param>
        /// <param name="state">�ƻ���״̬</param>
        /// <param name="plantype">�ƻ�����</param>
        /// <returns>�ɹ�����1 ʧ�ܷ��أ�1</returns>
        public int Save()
        {
            if (this.dt.Rows.Count <= 0)
                return -1;
            if (!this.IsValid())
                return -1;

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڽ��б��� ���Ժ�...");
            Application.DoEvents();

            //ϵͳʱ��
            DateTime sysTime = this.itemManager.GetDateTimeFromSysDateTime();

            //�������ݿ⴦������
            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
            //t.BeginTransaction();

            this.itemManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            #region ������޸ĵ����ƻ���������ɾ��ԭ���ƻ�������

            if (this.nowBillNO != null && this.nowBillNO != "")
            {
                List<FS.HISFC.Models.Pharmacy.InPlan> alCount = this.itemManager.QueryInPlanDetail(this.privDept.ID, this.nowBillNO);
                    
                //ɾ��δ�ɹ���˵ļƻ���Ϣ
                int parm = this.itemManager.DeleteInPlan(this.privDept.ID, this.nowBillNO,"0");
                if (parm == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    Function.ShowMsg(this.itemManager.Err);
                    return -1;
                }
                else if (parm < alCount.Count)
                { //������
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    Function.ShowMsg("�ƻ���������ͨ���ɹ���ˣ���ˢ�´���");
                    return -1;
                }
            }
            else
            {
                //����������ӵ����ƻ�������ȡ���ƻ�����
                this.nowBillNO = this.itemManager.GetPlanBillNO(this.privDept.ID);
                //���ƻ����ŵĲ���
                if (this.nowBillNO == null)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    Function.ShowMsg(Language.Msg("��ȡ�¼ƻ����ų���" + this.itemManager.Err));
                    return -1;
                }
            }

            #endregion

            int iCount = 1;

            ArrayList printData = new ArrayList();

            foreach (DataRow dr in this.dt.Rows)
            {
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(iCount, this.dt.Rows.Count);
                Application.DoEvents();
             
                #region ���ƻ���ֵ ����

                //�Լƻ�����Ϊ0�Ĳ����д���
                if (NConvert.ToDecimal(dr["�ƻ�����"]) == 0)
                    continue;

                FS.HISFC.Models.Pharmacy.InPlan inPlan = this.hsPlanData[dr["ҩƷ����"].ToString()] as FS.HISFC.Models.Pharmacy.InPlan;

                inPlan.BillNO = this.nowBillNO;                     //�ƻ�����
                inPlan.State = "0";                                 //����״̬
                inPlan.PlanType = this.planType;                    //�ɹ�����

                inPlan.PlanQty = NConvert.ToDecimal(dr["�ƻ�����"]) * inPlan.Item.PackQty;//�ƻ�����

                inPlan.PlanOper.ID = this.itemManager.Operator.ID;
                inPlan.PlanOper.OperTime = sysTime;                 //������Ϣ

                inPlan.Oper = inPlan.PlanOper;
                inPlan.Memo = dr["��ע"].ToString();                //��ע

                if (this.itemManager.InsertInPlan(inPlan) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    Function.ShowMsg(inPlan.Item.Name + "����ʧ�� " + this.itemManager.Err);
                    return -1;
                }

                #endregion

                printData.Add(inPlan);

                iCount++;
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            Function.ShowMsg("����ɹ�");

            this.Print(printData,true);

            //�������
            this.Clear();

            this.tvList.ShowInPlanList(this.privDept, "0");

            return 1;
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public void Filter()
        {
            if (this.dt.DefaultView == null)
                return;

            //��ù�������
            string queryCode = "%" + this.txtFilter.Text.Trim() + "%";

            try
            {
                this.dt.DefaultView.RowFilter = Function.GetFilterStr(this.dt.DefaultView, queryCode);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// ����ǰ��ѯ���ݰ�Excel��ʽ����
        /// </summary>
        public void Export()
        {
            try
            {
                if (this.neuSpread1_Sheet1.Rows.Count <= 0)
                    return;

                if (this.neuSpread1.Export() == 1)
                {
                    MessageBox.Show(Language.Msg("�����ɹ�"));
                }
            }
            catch (Exception ex)
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show(ex.Message);
            }
            finally
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }
        }

        /// <summary>
        /// ��ӡ
        /// </summary>
        /// <param name="alData">����ӡ����</param>
        /// <param name="isCue">�Ƿ������ʾ</param>
        public void Print(ArrayList alData,bool isCue)
        {
            foreach (FS.HISFC.Models.Pharmacy.InPlan info in alData)
            {
                //{B5B12199-4F8C-4a70-B55F-795E13261EAF}  ������ʾ��Ա����
                if (this.personNameDictionary.ContainsKey(info.PlanOper.ID) == false) //�������ƻ�����Ϣ
                {
                    FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

                    FS.HISFC.Models.Base.Employee planOper = managerIntegrate.GetEmployeeInfo(info.PlanOper.ID);
                    if (planOper != null)
                    {
                        this.personNameDictionary.Add(planOper.ID, planOper.Name);
                    }
                }

                if (this.personNameDictionary.ContainsKey(info.PlanOper.ID))
                {
                    info.PlanOper.Name = this.personNameDictionary[info.PlanOper.ID];
                }   
            }

            //ҩƷ��ӡ�ӿڵ��ó���Bug�������ͬʱ�������ƻ��Ͳɹ��ƻ������й���ӡ������£��л�������ӡ�ͻ������һ����ӡ�ӿ�ʵ��by Sunjh 2010-8-26 {D78A574D-59BE-491b-808C-38DCD26BA5EA}
            Function.IPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Pharmacy.IBillPrint)) as FS.HISFC.BizProcess.Interface.Pharmacy.IBillPrint;
            //if (Function.IPrint == null)
            //{
            //    Function.IPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Pharmacy.IBillPrint)) as FS.HISFC.BizProcess.Interface.Pharmacy.IBillPrint;
            //}

            if (Function.IPrint != null)
            {
                if (isCue)
                {
                    DialogResult rs = MessageBox.Show(Language.Msg("�Ƿ��ӡ�ƻ���"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (rs == DialogResult.No)
                    {
                        return;
                    }
                }
                Function.IPrint.SetData(alData, FS.HISFC.BizProcess.Interface.Pharmacy.BillType.InPlan);

                Function.IPrint.Print();
            }
        }

        /// <summary>
        /// ���ƻ�����ҩƷ�����ѯ���� by Sunjh 2010-11-4 {03133950-80B6-4df1-8087-2550F8A0DC69}
        /// </summary>
        public void ShowDrugStatus()
        {
            #region SQL���
            string tempSql = @"select 'ҩƷ����' as ������,b.trade_name as ���ֵ 
                    from pha_com_baseinfo b where b.drug_code='{1}'
                    union all
                    select '��������' as ������,(select fac_name from pha_com_company where fac_code=b.producer_code and company_type='0') as ���ֵ 
                    from pha_com_baseinfo b where b.drug_code='{1}'
                    union all
                    select '������˾' as ������,(select fac_name from pha_com_company where fac_code=b.company_code and company_type='1') as ���ֵ 
                    from pha_com_baseinfo b where b.drug_code='{1}'
                    union all
                    select '�б�����' as ������,decode(b.tender_flag,'1','�б�ҩƷ','���б�ҩƷ') as ���ֵ 
                    from pha_com_baseinfo b where b.drug_code='{1}'
                    union all
                    select '�����' as ������,nvl(sum(ti.in_num),0)||' '||max(ti.min_unit) as ���ֵ from pha_com_input ti 
                    where ti.drug_dept_code='{0}' and ti.drug_code='{1}' and ti.oper_date>=to_date('{2}','yyyy-MM-dd hh24:mi:ss')
                     and ti.oper_date<to_date('{3}','yyyy-MM-dd hh24:mi:ss')
                    union all
                    select '������' as ������,nvl(sum(t.out_num),0)||' '||max(t.min_unit) as ���ֵ from pha_com_output t 
                    where t.drug_dept_code='{0}' and t.drug_code='{1}' and t.oper_date>=to_date('{2}','yyyy-MM-dd hh24:mi:ss')
                     and t.oper_date<to_date('{3}','yyyy-MM-dd hh24:mi:ss')
                    union all
                    select '��ǰ�����' as ������,nvl(sum(ts.store_sum),0)||' '||max(ts.min_unit) as ���ֵ from pha_com_stockinfo ts 
                    where ts.drug_dept_code='{0}' and ts.drug_code='{1}'";
            #endregion

            string[] argColumns = new string[2] { "100", "260" };
            FS.HISFC.Components.Pharmacy.Base.ucCommonPopQuery ucPopList = new FS.HISFC.Components.Pharmacy.Base.ucCommonPopQuery();
            ucPopList.TopInfo = "ҩƷ�������";
            ucPopList.BottomInfo = "Ĭ�ϲ�ѯʱ�䷶ΧΪ���һ���£���������ѡ��ʱ���ѯ����ʱ��Ρ�";
            ucPopList.Width = 470;
            ucPopList.Height = 300;
            ucPopList.IsShowConditionPanel = true;
            ucPopList.dtpBegin.Value = itemManager.GetDateTimeFromSysDateTime().Date.AddMonths(-1).AddDays(1);
            ucPopList.dtpEnd.Value = itemManager.GetDateTimeFromSysDateTime().Date.AddDays(1);
            tempSql = string.Format(tempSql, this.privDept.ID, this.ucDrugList1.CurrDrugCode, ucPopList.dtpBegin.Value.ToString(), ucPopList.dtpEnd.Value.ToString());
            ucPopList.SqlStr = tempSql;
            ucPopList.ArgColumnWith = argColumns;
            ucPopList.ShowData();
            FS.FrameWork.WinForms.Classes.Function.PopShowControl(ucPopList);
        }

        #endregion

        #region IInterfaceContainer ��Ա

        public Type[] InterfaceTypes
        {
            get 
            {
                Type[] printType = new Type[1];
                printType[0] = typeof(FS.HISFC.BizProcess.Interface.Pharmacy.IBillPrint);

                return printType;
            }
        }

        #endregion

        /// <summary>
        /// ����
        /// </summary>
        /// <returns>�ɹ�����1 ʧ�ܷ��أ�1</returns>
        public int Sort()
        {
            if (this.nowBillNO == null || this.nowBillNO == "")
            {
                MessageBox.Show(Language.Msg("���Ƚ��б���������ٽ����������"), "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return 1;
            }
            //��ȡ��ǰ����������
            List<FS.HISFC.Models.Pharmacy.InPlan> inPlanList = new List<FS.HISFC.Models.Pharmacy.InPlan>();
            foreach (DataRow dr in this.dt.Rows)
            {
                FS.HISFC.Models.Pharmacy.InPlan inPlan = this.GetDataFromRow(dr);
                inPlanList.Add(inPlan);
            }
            if (inPlanList.Count == 0)
            {
                return 1;
            }

            using (ucSortManager uc = new ucSortManager())
            {
                uc.SetFarPoint("�Զ�����", "��Ʒ����", "���","������˾","��������");
                FS.FrameWork.WinForms.Classes.Function.PopShowControl(uc);

                //�������������
                List<FS.HISFC.Models.Pharmacy.InPlan> sortList = new List<FS.HISFC.Models.Pharmacy.InPlan>();

                MultiSortBase<FS.HISFC.Models.Pharmacy.InPlan> sortManager = new MultiSortBase<FS.HISFC.Models.Pharmacy.InPlan>();
                InPlanSortDelegateInstance delegateInstance = new InPlanSortDelegateInstance();
                sortManager.GetCompareInstance = delegateInstance.GetCompare;

                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(Language.Msg("���ڽ����������� ���Ժ�..."));
                Application.DoEvents();
                //��������
                sortManager.MultiStort(inPlanList, ref sortList,uc.SortLevel,uc.Direction);
                //������������������ʾ            
                if (sortList != null && sortList.Count > 0)
                {
                    this.Clear();

                    foreach (FS.HISFC.Models.Pharmacy.InPlan info in sortList)
                    {
                        this.SetPlanInfo(info);

                        if (this.AddDataToTable(info) == 1)
                        {
                            this.hsPlanData.Add(info.Item.ID, info);
                        }
                        else
                        {
                            return -1;
                        }
                    }
                }
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }
            return 1;

        }

        #region �¼�

        private void ucInPlan_Load(object sender, EventArgs e)
        {
            this.InitDataTable();

            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToUpper() != "DEVENV")
            {
                //FS.FrameWork.Models.NeuObject testPrivDept = new FS.FrameWork.Models.NeuObject();
                //int parma = FS.HISFC.Components.Common.Classes.Function.ChoosePivDept("0311", ref testPrivDept);

                //if (parma == -1)            //��Ȩ��
                //{
                //    MessageBox.Show(Language.Msg("���޴˴��ڲ���Ȩ��"));
                //    return;
                //}
                //else if (parma == 0)       //�û�ѡ��ȡ��
                //{
                //    return;
                //}

                //this.privDept = testPrivDept;

                //base.OnStatusBarInfo(null, "�������ң� " + testPrivDept.Name);          

                //{52402239-DB82-41c8-A8A7-2411B9EF64F1}  ��ʼ����ӡ�ӿ�
                Function.IPrint = null;

                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڼ������� ���Ժ�...");
                Application.DoEvents();

                this.InitData();

                this.InitPlanList();

                this.SetToolButton(true);

                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

                if (this.ParentForm != null)
                {
                    this.ParentForm.FormClosing -= new FormClosingEventHandler(ParentForm_FormClosing);
                    this.ParentForm.FormClosing += new FormClosingEventHandler(ParentForm_FormClosing);
                }

                this.InitControlParam();
            }
        }

        private void ParentForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!this.IsShowList)
            {
                DialogResult rs = MessageBox.Show(Language.Msg("�ƻ�����δ���� ȷ���˳���?"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (rs == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }

        private void tvList_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.Clear();

            if (e.Node != null && e.Node.Parent != null)
            {
                FS.FrameWork.Models.NeuObject inPlanObj = e.Node.Tag as FS.FrameWork.Models.NeuObject;

                this.nowBillNO = inPlanObj.ID;

                this.ShowPlanData(this.privDept.ID, inPlanObj.ID);
            }
        }

        private void tvList_DoubleClick(object sender, EventArgs e)
        {
            if (this.tvList.SelectedNode != null && this.tvList.SelectedNode.Parent != null)
            {
                FS.FrameWork.Models.NeuObject inPlanObj = this.tvList.SelectedNode.Tag as FS.FrameWork.Models.NeuObject;

                this.nowBillNO = inPlanObj.ID;

                if (inPlanObj.Memo == "0")
                {
                    this.IsShowList = false;
                }
            }
        }

        private void fpStockPlan_EditModeOff(object sender, EventArgs e)
        {
            //���¼������ڶ�dr���в��� ����ɼƻ��������ڽ�����
            //if (this.neuSpread1_Sheet1.ActiveColumnIndex == (int)ColumnSet.ColPlanNum)
            //{
            //    string[] keys = new string[] { this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, (int)ColumnSet.ColDrugNO].Text};
            //    DataRow dr = this.dt.Rows.Find(keys);
            //    if (dr != null)
            //    {
            //        dr["�ƻ����"] = NConvert.ToDecimal(dr["�ƻ�����"]) * NConvert.ToDecimal(dr["�ƻ������"]);

            //        dr.EndEdit();

            //        this.SetSum();
            //    }
            //}            
        }

        private void neuTextBox1_TextChanged(object sender, EventArgs e)
        {
            this.Filter();
        }

        private void neuTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.SetFocus(true);
            }
        }        

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (this.neuSpread1.ContainsFocus && keyData == Keys.Enter)
            {
                if (this.neuSpread1_Sheet1.ActiveColumnIndex == (int)ColumnSet.ColPlanNum)
                {
                    decimal planQty = NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, (int)ColumnSet.ColPlanNum].Text);
                    decimal planPrice = NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, (int)ColumnSet.ColPurchasePrice].Text);

                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex,(int)ColumnSet.ColPlanCost].Value = planQty * planPrice;

                    this.SetSum();
                }          

                if (this.neuSpread1_Sheet1.ActiveColumnIndex == (int)ColumnSet.ColPlanNum)
                {
                    if (this.neuSpread1_Sheet1.ActiveRowIndex < this.neuSpread1_Sheet1.Rows.Count - 1)
                    {
                        this.neuSpread1_Sheet1.ActiveRowIndex++;
                    }
                    else
                    {
                        if (this.IsShowList)
                        {
                            this.txtFilter.Select();
                            this.txtFilter.SelectAll();
                        }
                        else
                        {
                            this.SetFocus(false);
                        }
                    }
                }
            }

            //���ƻ�����ҩƷ�����ѯ���� by Sunjh 2010-11-4 {03133950-80B6-4df1-8087-2550F8A0DC69}
            if (keyData == Keys.F9)
            {
                this.ShowDrugStatus();
            }

            return base.ProcessDialogKey(keyData);
        }

        private void ucDrugList1_ChooseDataEvent(FarPoint.Win.Spread.SheetView sv, int activeRow)
        {
            if (activeRow < 0)
                return;

            string drugCode = sv.Cells[activeRow, 0].Text;

            FS.HISFC.Models.Pharmacy.Item item = this.itemManager.GetItem(drugCode);
            if (item == null)
            {
                MessageBox.Show(Language.Msg(this.itemManager.Err));
            }

            if (this.AddDrugData(item) == 1)
            {
                this.neuSpread1_Sheet1.ActiveRowIndex = this.neuSpread1_Sheet1.Rows.Count - 1;
                this.SetFocus(true);
            }
        }

        private void lblShowDrugDetail_Click(object sender, EventArgs e)
        {
            //���ƻ�����ҩƷ�����ѯ���� by Sunjh 2010-11-4 {03133950-80B6-4df1-8087-2550F8A0DC69}
            this.ShowDrugStatus();
        }        

        #endregion

        /// <summary>
        /// ������
        /// </summary>
        private enum ColumnSet
        {          
            /// <summary>
            /// �Զ�����
            /// </summary>
            ColUserCode,
            /// <summary>
            /// ҩƷ��Ʒ��  
            /// </summary>
            ColTradeName,
            /// <summary>
            /// ���  
            /// </summary>
            ColSpecs,
            /// <summary>
            /// ��װ����
            /// </summary>
            ColPackQty,
            /// <summary>
            /// �ƻ������  
            /// </summary>
            ColPurchasePrice,
            /// <summary>
            /// �ƻ�����  
            /// </summary>
            ColPlanNum,
            /// <summary>
            /// ��λ  
            /// </summary>
            ColUnit,
            /// <summary>
            /// �ƻ����  
            /// </summary>
            ColPlanCost,
            /// <summary>
            /// ���ƿ��  
            /// </summary>
            ColOwnStockNum,
            /// <summary>
            /// ȫԺ���  
            /// </summary>
            ColAllStockNum,
            /// <summary>
            /// ��������
            /// </summary>
            ColOutTotal,
            /// <summary>
            /// �վ�����
            /// </summary>
            ColOutDay,
            /// <summary>
            /// ������˾
            /// </summary>
            ColCompany,
            /// <summary>
            /// ��������
            /// </summary>
            ColProduce,
            /// <summary>
            /// ��ע
            /// </summary>
            ColMemo,
            /// <summary>
            /// ҩƷ���� 
            /// </summary>
            ColDrugNO,
            /// <summary>
            /// ƴ����
            /// </summary>
            ColSpellCode,
            /// <summary>
            /// �����
            /// </summary>
            ColWBCode
        }

        #region IPreArrange ��Ա

        public int PreArrange()
        {
            FS.FrameWork.Models.NeuObject testPrivDept = new FS.FrameWork.Models.NeuObject();
            int parma = FS.HISFC.Components.Common.Classes.Function.ChoosePivDept("0311", ref testPrivDept);

            if (parma == -1)            //��Ȩ��
            {
                MessageBox.Show(Language.Msg("���޴˴��ڲ���Ȩ��"));
                return -1;
            }
            else if (parma == 0)       //�û�ѡ��ȡ��
            {
                return -1;
            }

            this.privDept = testPrivDept;

            base.OnStatusBarInfo(null, "�������ң� " + testPrivDept.Name);

            return 1;
        }

        #endregion        
        
    }

    internal class InPlanSortDelegateInstance
    {
        public IComparer<FS.HISFC.Models.Pharmacy.InPlan> GetCompare(List<string> sortColumnLevel, SortDirection direction)
        {
            CompareInPlan c = new CompareInPlan();

            c.sortColumnLevel = sortColumnLevel;
            c.sortDirection = direction;

            return c;
        }

        public class CompareInPlan : IComparer<FS.HISFC.Models.Pharmacy.InPlan>
        {
            /// <summary>
            /// ������
            /// </summary>
            public List<string> sortColumnLevel = new List<string>();

            /// <summary>
            /// ����ʽ
            /// </summary>
            public SortDirection sortDirection = SortDirection.Ascend;

            #region IComparer<FS.HISFC.Models.Pharmacy.InPlan> ��Ա

            public int Compare(FS.HISFC.Models.Pharmacy.InPlan x, FS.HISFC.Models.Pharmacy.InPlan y)
            {
                string oX = null;
                string oY = null;
                int nComp;

                foreach (string sortColumn in sortColumnLevel)
                {
                    switch (sortColumn)
                    {
                        case "�Զ�����":
                            oX += x.Item.NameCollection.UserCode;
                            oY += y.Item.NameCollection.UserCode;
                            break;
                        case "��Ʒ����":
                            oX += x.Item.Name;
                            oY += y.Item.Name;
                            break;
                        case "���":
                            oX += "S" + x.Item.Specs;
                            oY += "S" + y.Item.Specs;
                            break;
                        case "������˾":
                            oX += "C" + x.Item.Product.Company.ID;
                            oY += "C" + y.Item.Product.Company.ID;
                            break;
                        case "��������":
                            oX += "P" + x.Item.Product.Producer.ID;
                            oY += "P" + y.Item.Product.Producer.ID;
                            break;
                    }

                }

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
                    nComp = string.Compare(oX.ToString(), oY.ToString());
                }

                return this.sortDirection == SortDirection.Ascend ? nComp : -nComp;
            }

            #endregion
        }
    }
}
