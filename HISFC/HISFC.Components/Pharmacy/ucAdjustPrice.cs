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

namespace FS.HISFC.Components.Pharmacy
{
    /// <summary>
    /// [��������: ҩƷ���۹���]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2006-12]<br></br>
    /// <�޸ļ�¼>
    ///    1��({4E0793B1-9BCF-44c0-BB71-7AEB89F0F5EE})
    ///         ���۴洢���̽����޸ģ����ݲ���Nostrum_Manage_Store (P00513) �����á���������� ����ϸҩƷ����ʱ��������ϸ��Э���������д���
    ///         ����������� ����ϸҩƷ����ʱ�԰�������ϸ��Э���������д���
    ///    2������ѡ�񲿷�ҩƷ���ٵ��������۳��ֵ�BUG by Sunjh 2010-8-23 {DC7BD11C-3A3D-4485-9D25-DE1FE49B4687}
    ///    3������ɾ��δ��Ч�ĵ��ۼ�¼ by Sunjh 2010-8-31 {B56F6FDF-E7D0-4afd-953A-3006AFE257C1}
    ///    4����������ʱ�Զ����������� by Sunjh 2010-9-1 {B978D218-127A-4511-BB47-0E132AB1860A}
    /// </�޸ļ�¼>
    /// </summary>
    public partial class ucAdjustPrice : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.FrameWork.WinForms.Forms.IInterfaceContainer, FS.FrameWork.WinForms.Classes.IPreArrange
    {
        public ucAdjustPrice()
        {
            InitializeComponent();
        }

        #region �����

        /// <summary>
        /// ���ݱ�
        /// </summary>
        private DataTable dt = null;

        /// <summary>
        /// Fp��Ԫ��
        /// </summary>
        private FarPoint.Win.Spread.CellType.NumberCellType numCellType = new FarPoint.Win.Spread.CellType.NumberCellType();

        /// <summary>
        /// �۸���ʾλ��
        /// </summary>
        private int decimalPlaces = 4;

        /// <summary>
        /// ���ۼ��������۵ı���ϵ��
        /// </summary>
        private decimal scale = (decimal)1.5;

        /// <summary>
        /// ������˾������
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper companyHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// �������Ұ�����
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper produceHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// ��ʼʱ��
        /// </summary>
        private DateTime dtBegin = System.DateTime.MaxValue;

        /// <summary>
        /// ��ֹʱ��
        /// </summary>
        private DateTime dtEnd = System.DateTime.MaxValue;

        /// <summary>
        /// Ȩ�޿���
        /// </summary>
        private FS.FrameWork.Models.NeuObject privDept = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// ҩƷҵ�������
        /// </summary>
        private FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();

        /// <summary>
        /// �Զ����ɵ��ۺ����ۼ�
        /// </summary>
        private bool isAutoNewPrice = false;

        /// <summary>
        /// ���۵���
        /// </summary>
        private string adjustNO = "";

        /// <summary>
        /// ��ǰ�����ĵ��ۼ�¼
        /// </summary>
        private FS.HISFC.Models.Pharmacy.AdjustPrice tempOperAdjustPrice = new FS.HISFC.Models.Pharmacy.AdjustPrice();

        /// <summary>
        /// ��������
        /// </summary>
        private frmGroupAdjust frmGroup = null;

        /// <summary>
        /// �Ƿ�ս����걣������
        /// </summary>
        private bool isExeEditModeOffEvent = false;

        private EnumAdjustType adjustType = EnumAdjustType.ȫԺ����;

        /// <summary>
        /// ����ԭ��CellType
        /// </summary>
        FarPoint.Win.Spread.CellType.ComboBoxCellType cmbCellType = new FarPoint.Win.Spread.CellType.ComboBoxCellType();

        #endregion

        #region ����

        /// <summary>
        /// �������
        /// </summary>
        [Description("������� ���ݲ�ͬҽԺ��������"), Category("����"), DefaultValue("���۵�")]
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
        /// ���ۼ��������۵ı���       
        /// </summary>
        [Description("���ۼ��������۵ı���"), Category("����"), DefaultValue(1.5)]
        public decimal Scale
        {
            get
            {
                return this.scale;
            }
            set
            {
                scale = value;
            }
        }

        /// <summary>
        /// �۸���ʾλ��
        /// </summary>
        [Description("�۸���ʾλ��"), Category("����"), DefaultValue(4)]
        public int PricePlaces
        {
            get
            {
                return this.decimalPlaces;
            }
            set
            {
                this.decimalPlaces = value;
            }
        }

        /// <summary>
        /// ���۷�Χ
        /// </summary>
        [Description("���۷�Χ"), Category("����")]
        public EnumAdjustType AdjustType
        {
            get
            {
                return this.adjustType;
            }
            set
            {
                this.adjustType = value;

                if (value == EnumAdjustType.ȫԺ����)
                {
                    this.chkValid.Visible = true;
                }
                else
                {
                    this.chkValid.Visible = false;
                }
            }
        }

        /// <summary>
        /// �Ƿ���Ա༭
        /// </summary>
        public bool IsCanEdit
        {
            set
            {
                this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColRetailPrice].Locked = !value;
                this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColTradePrice].Locked = !value;
                this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColMemo].Locked = !value;

                this.chkValid.Enabled = value;
                this.dtpDateTime.Enabled = value;
            }
        }

        /// <summary>
        /// �����Ƿ�ʱ��Ч
        /// </summary>
        private bool IsInitInsure
        {
            set
            {
                this.chkValid.Checked = value;
                this.dtpDateTime.Enabled = !value;
            }
        }

        /// <summary>
        /// �Ƿ���ʾ���۵��б�ڵ�
        /// </summary>
        private bool IsShowList
        {
            get
            {
                return this.ucChooseList1.IsShowTree;
            }
            set
            {
                this.ucChooseList1.IsShowTree = value;

                this.SetToolButton(value);
            }
        }

        #endregion

        #region ������

        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("���۵�", "��ʾ���۵��б�", FS.FrameWork.WinForms.Classes.EnumImageList.X��Ϣ, true, false, null);
            toolBarService.AddToolButton("��  ��", "�½����۵�", FS.FrameWork.WinForms.Classes.EnumImageList.X�½�, true, false, null);
            toolBarService.AddToolButton("��  ��", "���õ��۵���������", FS.FrameWork.WinForms.Classes.EnumImageList.YԤԼ, true, false, null);
            toolBarService.AddToolButton("ɾ  ��", "ɾ����ǰѡ��ĵ���ҩƷ ɾ��������Ч", FS.FrameWork.WinForms.Classes.EnumImageList.Sɾ��, true, false, null);
            toolBarService.AddToolButton("��������", "���յ��۹�ʽ���е���", FS.FrameWork.WinForms.Classes.EnumImageList.Z����, true, false, null);

            return toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "���۵�")
            {
                this.ShowList();
            }
            if (e.ClickedItem.Text == "��  ��")
            {
                this.New();
            }
            if (e.ClickedItem.Text == "��  ��")
            {
                this.ChooseTime();
            }
            if (e.ClickedItem.Text == "ɾ  ��")
            {
                this.DelData();
            }
            if (e.ClickedItem.Text == "��������")
            {
                this.GroupAdjust();
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        protected override int OnSave(object sender, object neuObject)
        {
            if (this.adjustType == EnumAdjustType.ȫԺ����)
            {
                this.Save();
            }
            else
            {
                this.SaveDeptAdjust();
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

            return 1;
        }

        public override int SetPrint(object sender, object neuObject)
        {
            return 1;
        }

        /// <summary>
        /// ���ù�������ť״̬
        /// </summary>
        /// <param name="isShowList">�Ƿ��ڽڵ�༭״̬</param>
        protected void SetToolButton(bool isShowList)
        {
            this.toolBarService.SetToolButtonEnabled("���۵�", !isShowList);
            this.toolBarService.SetToolButtonEnabled("��  ��", isShowList);
            this.toolBarService.SetToolButtonEnabled("��  ��", isShowList);
            //����ɾ��δ��Ч�ĵ��ۼ�¼ by Sunjh 2010-8-31 {B56F6FDF-E7D0-4afd-953A-3006AFE257C1}
            //this.toolBarService.SetToolButtonEnabled("ɾ  ��", !isShowList);
            this.toolBarService.SetToolButtonEnabled("��������", !isShowList);
        }

        #endregion

        #region ��ʼ����Fp����

        /// <summary>
        /// ���ݱ��ʼ��
        /// </summary>
        private void InitDataTable()
        {
            System.Type dtBol = System.Type.GetType("System.Boolean");
            System.Type dtStr = System.Type.GetType("System.String");
            System.Type dtDec = System.Type.GetType("System.Decimal");
            System.Type dtDate = System.Type.GetType("System.DateTime");

            this.dt = new DataTable();
            //{8CF898CC-847E-42dd-8BA9-47BCBF312F2E}��ɽ��
            this.dt.Columns.AddRange(
                                    new System.Data.DataColumn[] {
                                                                    new DataColumn("��Ʒ����[���]",  dtStr),
                                                                    new DataColumn("������˾",      dtStr),
                                                                    new DataColumn("��������",      dtStr),
                                                                    new DataColumn("ԭ���ۼ�",      dtDec),
                                                                    new DataColumn("ԭ������",      dtDec),
                                                                    new DataColumn("�����ۼ�",      dtDec),
                                                                    new DataColumn("��������",      dtDec),
                                                                    new DataColumn("���",      dtDec),
                                                                    new DataColumn("�����۲��",    dtDec),
                                                                    new DataColumn("����ԭ��",          dtStr),                                            
                                                                    new DataColumn("ҩƷ����",      dtStr),
                                                                    new DataColumn("ƴ����",        dtStr),
                                                                    new DataColumn("�����",        dtStr),
                                                                    new DataColumn("�Զ�����",      dtStr)
                                                                   }
                                  );

            DataColumn[] keys = new DataColumn[1];

            keys[0] = this.dt.Columns["ҩƷ����"];

            this.dt.PrimaryKey = keys;

            this.dt.DefaultView.AllowDelete = true;
            this.dt.DefaultView.AllowEdit = true;
            this.dt.DefaultView.AllowNew = true;

            //this.neuSpread1_Sheet1.DataSource = this.dt;

            this.neuSpread1_Sheet1.DataSource = this.dt.DefaultView;
            this.SetFormat();
        }


        /// <summary>
        /// Fp��ʾ��ʽ��
        /// </summary>
        private void SetFormat()
        {

            FarPoint.Win.Spread.InputMap im;

            im = this.neuSpread1.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused);
            im.Put(new FarPoint.Win.Spread.Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.None);
            //im.Put(new FarPoint.Win.Spread.Keystroke(Keys.MButton, Keys.None), FarPoint.Win.Spread.SpreadActions.None);
            this.neuSpread1_Sheet1.DefaultStyle.Locked = true;

            this.numCellType.DecimalPlaces = this.decimalPlaces;
            this.numCellType.SubEditor = null;

            //{8CF898CC-847E-42dd-8BA9-47BCBF312F2E}��ɽ��
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColPreRetailPrice].CellType = this.numCellType;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColPreTradePrice].CellType = this.numCellType;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColRetailPrice].CellType = this.numCellType;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColTradePrice].CellType = this.numCellType;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColBalancePrice].CellType = this.numCellType;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColBalanceTradePrice].CellType = this.numCellType;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColTradeName].Width = 200F;       //��Ʒ��

            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColMemo].Width = 130F;            //����ԭ��

            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColPreRetailPrice].Width = 100F;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColRetailPrice].Width = 100F;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColBalancePrice].Width = 100F;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColBalanceTradePrice].Width = 120F;

            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColCompany].Visible = false;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColProduce].Visible = false;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColDrugNO].Visible = false;

            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColSpellCode].Visible = false;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColWBCode].Visible = false;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColUserCode].Visible = false;

            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColRetailPrice].Locked = false;
            //{4E42F6D5-1F3C-4b12-BCB7-895239AB9D9A}��������
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColTradePrice].Locked = false;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColMemo].Locked = false;

            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColRetailPrice].BackColor = System.Drawing.Color.SeaShell;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColTradePrice].BackColor = System.Drawing.Color.SeaShell;
            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColMemo].BackColor = System.Drawing.Color.SeaShell;

            this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColBalancePrice].Font = new Font("����", 9F, FontStyle.Bold);

            //{8A8F8D41-843A-4b0a-9BA3-0FFBB1B84F10}  ����ԭ�����
            FS.HISFC.BizProcess.Integrate.Manager manageIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();
            ArrayList alList = manageIntegrate.QueryConstantList("AdjustCausation");
            if (alList == null)
            {
                MessageBox.Show(Language.Msg("���ص���ԭ���б�������"));
                return;
            }
            if (alList.Count > 0)
            {
                string[] strAdjustCausation = new string[alList.Count];
                int iIndex = 0;
                foreach (FS.FrameWork.Models.NeuObject info in alList)
                {
                    strAdjustCausation[iIndex] = info.Name;
                    iIndex++;
                }
                this.cmbCellType.Items = strAdjustCausation;

                this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColMemo].CellType = cmbCellType;
             
            }
        }

        /// <summary>
        /// ���ݳ�ʼ��
        /// </summary>
        private void InitData()
        {
            FS.HISFC.BizLogic.Pharmacy.Constant phaCons = new FS.HISFC.BizLogic.Pharmacy.Constant();
            ArrayList alCompany = phaCons.QueryCompany("1");
            if (alCompany == null)
            {
                MessageBox.Show(Language.Msg("���ع�����˾�б�������"));
                return;
            }
            this.companyHelper = new FS.FrameWork.Public.ObjectHelper(alCompany);
            ArrayList alProduce = phaCons.QueryCompany("0");
            if (alProduce == null)
            {
                MessageBox.Show(Language.Msg("�������������б�������"));
                return;
            }
            this.produceHelper = new FS.FrameWork.Public.ObjectHelper(alProduce);

            //����ҩƷ�б�
            this.ucChooseList1.ShowPharmacyList();            
            //int iDistiance = 140;
            //this.ucChooseList1.GetColumnWidth(2,ref iDistiance);
            //this.splitContainer1.SplitterDistance = iDistiance;
            //����ʱ��
            this.dtEnd = phaCons.GetDateTimeFromSysDateTime().AddDays(7);
            this.dtBegin = this.dtEnd.AddDays(-10);
            //Ȩ�޿��� ��ʱʹ�ò���Ա���Ҵ���
            this.privDept = ((FS.HISFC.Models.Base.Employee)phaCons.Operator).Dept;

            if (this.adjustType == EnumAdjustType.���Ƶ���)
            {
                FS.HISFC.Models.Base.Department dept = new FS.HISFC.Models.Base.Department();
                FS.HISFC.BizLogic.Manager.Department deptManager = new FS.HISFC.BizLogic.Manager.Department();
                dept = deptManager.GetDeptmentById(this.privDept.ID);
                if (dept.DeptType.ID.ToString() != "PI")
                {
                    MessageBox.Show(Language.Msg("ֻ��ҩ��������е��Ƶ��۲���"), "", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    this.toolBarService.SetToolButtonEnabled("��  ��", false);
                    this.toolBarService.SetToolButtonEnabled("����", false);
                }
                else
                {
                    this.toolBarService.SetToolButtonEnabled("��  ��", true);
                }
                return;
            }

            //����������������
            FS.HISFC.BizLogic.Pharmacy.Item itemMgr = new FS.HISFC.BizLogic.Pharmacy.Item();
            List<FS.HISFC.Models.Pharmacy.Item> alItem = this.itemManager.QueryItemAvailableList(false);
            if (alItem != null)
            {
                if (this.frmGroup == null)
                {
                    this.frmGroup = new frmGroupAdjust();
                }

                List<FS.HISFC.Models.Pharmacy.Item> itemListCollection = new List<FS.HISFC.Models.Pharmacy.Item>();
                foreach (FS.HISFC.Models.Pharmacy.Item info in alItem)
                {
                    //{122BCCB2-A7B5-4644-9550-5AB1000CB663}  Э���������������
                    if (info.IsNostrum == true)             //Э���������������
                    {
                        continue;
                    }

                    itemListCollection.Add( info );
                }

                this.frmGroup.AllItem = itemListCollection;
            }
        }

        /// <summary>
        /// �¼��������
        /// </summary>
        private void InitEvent()
        {
            this.ucChooseList1.TvList.AfterSelect += new TreeViewEventHandler(TvList_AfterSelect);

            this.ucChooseList1.TvList.DoubleClick += new EventHandler(TvList_DoubleClick);

        }

        /// <summary>
        /// ����Fpӯ����ʽ��ʾ
        /// </summary>
        private void SetProfitFlag()
        {
            try
            {
                for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
                {
                    if (NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.ColBalancePrice].Text) > 0)//��Ӯ
                        this.neuSpread1_Sheet1.Rows[i].ForeColor = System.Drawing.Color.Blue;
                    else if (NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.ColBalancePrice].Text) < 0)//����
                        this.neuSpread1_Sheet1.Rows[i].ForeColor = System.Drawing.Color.Red;
                    else
                        this.neuSpread1_Sheet1.Rows[i].ForeColor = System.Drawing.Color.Black;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Language.Msg(ex.Message));
            }
        }

        #endregion

        #region ����

        /// <summary>
        /// ��DataSet�ڼ�������
        /// </summary>
        /// <param name="adjust"></param>
        /// <returns></returns>
        private int AddDataToTable(FS.HISFC.Models.Pharmacy.AdjustPrice adjust)
        {            
            try
            {
                //{8CF898CC-847E-42dd-8BA9-47BCBF312F2E}��ɽ��
                this.dt.Rows.Add(new object[] { 
                                            adjust.Item.Name + "[" + adjust.Item.Specs + "]",       //ҩƷ���ƹ��
                                            adjust.Item.Product.Company.Name,                       //������˾
                                            adjust.Item.Product.Producer.Name,                      //��������
                                            adjust.Item.PriceCollection.RetailPrice.ToString(),                //ԭ���ۼ�
                                            adjust.Item.PriceCollection.WholeSalePrice.ToString(),             //ԭ������
                                            adjust.AfterRetailPrice,                                //���ۺ����ۼ�
                                            adjust.AfterWholesalePrice,//���ۺ�������
                                            adjust.AfterRetailPrice - adjust.Item.PriceCollection.RetailPrice,//���
                                            adjust.AfterWholesalePrice- adjust.Item.PriceCollection.WholeSalePrice,//�����۲��
                                            adjust.Memo,                                            //����ԭ��
                                            adjust.Item.ID,                                         //ҩƷ����
                                            adjust.Item.NameCollection.SpellCode,                   //ƴ����
                                            adjust.Item.NameCollection.WBCode,                      //�����
                                            adjust.Item.NameCollection.UserCode,                    //�Զ�����
                                           });
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
            //neuSpread1.EditModePermanent = true;
            //neuSpread1.EditMode = true;

            return 1;
        }

        /// <summary>
        /// ���������ʾ
        /// </summary>
        protected void Clear()
        {
            try
            {
                this.neuSpread1_Sheet1.DataSource = null;
                this.isExeEditModeOffEvent = true;
                this.dt.Clear();
                this.dt.AcceptChanges();
                this.neuSpread1_Sheet1.RowCount = 0;
                this.neuSpread1_Sheet1.ColumnCount = 0;
                InitDataTable();
                this.neuSpread1_Sheet1.DataSource = this.dt.DefaultView;
                this.SetFormat();
                this.lbOperInfo.Text = "";
                this.lblAdjustNumber.Text = "";
            }
            catch  (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <returns></returns>
        protected int DelData()
        {
            try
            {
                if (this.neuSpread1_Sheet1.Rows.Count > 0)
                {
                    DialogResult rs = MessageBox.Show(Language.Msg("ȷ��ɾ������������?"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (rs == DialogResult.No)
                        return 0;

                    //����ɾ��δ��Ч�ĵ��ۼ�¼ by Sunjh 2010-8-31 {B56F6FDF-E7D0-4afd-953A-3006AFE257C1}
                    if (this.itemManager.DeleteAdjustPriceInfo(this.adjustNO, this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, (int)ColumnSet.ColDrugNO].Text) == -1)
                    {
                        MessageBox.Show("ɾ��������Ϣʧ��!" + this.itemManager.Err);

                        return -1;
                    }

                    this.neuSpread1.StopCellEditing();

                    string[] keys = new string[]{
                                                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, (int)ColumnSet.ColDrugNO].Text
                                            };
                    DataRow dr = this.dt.Rows.Find(keys);
                    if (dr != null)
                    {                       
                        this.dt.Rows.Remove(dr);
                    }

                    this.neuSpread1.StartCellEditing(null, false);

                    this.SetProfitFlag();
                    
                }
            }
            catch (System.Data.DataException e)
            {
                System.Windows.Forms.MessageBox.Show(FS.FrameWork.Management.Language.Msg("�����ݱ�ִ��ɾ��������������" + e.Message));
                return -1;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(FS.FrameWork.Management.Language.Msg("�����ݱ�ִ��ɾ��������������" + ex.Message));
                return -1;
            }

            MessageBox.Show("ɾ��������Ϣ�ɹ�!");

            return 1;
        }

        /// <summary>
        /// ��ȡ�Զ��������ۼ�
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected decimal GetNewPrice(FS.HISFC.Models.Pharmacy.Item item)
        {
            if (this.frmGroup != null)
            {
                return this.frmGroup.GetNewPrice(item);
            }
            else
            {
                return item.PriceCollection.RetailPrice;
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="isFpFocus">�Ƿ�����Fp����</param>
        protected void SetFocus(bool isFpFocus)
        {
            if (isFpFocus)
            {
                this.neuSpread1.Select();
                this.neuSpread1_Sheet1.ActiveRowIndex = this.neuSpread1_Sheet1.Rows.Count - 1;
                this.neuSpread1_Sheet1.ActiveColumnIndex = (int)ColumnSet.ColRetailPrice;
            }
            else
            {
                if (this.neuSpread1_Sheet1.Rows.Count > 0)
                {
                    //���һ��������EditMode_Off�¼�
                    string[] keys = new string[] { this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, (int)ColumnSet.ColDrugNO].Text };
                    DataRow dr = this.dt.Rows.Find(keys);
                    if (dr != null)
                    {
                        if (this.neuSpread1_Sheet1.ActiveColumnIndex == (int)ColumnSet.ColTradePrice)
                        {
                            dr["�����۲��"] = NConvert.ToDecimal(dr["��������"]) - NConvert.ToDecimal(dr["ԭ������"]);
                            this.neuSpread1.Refresh();
                        }
                        else
                        {
                            //{8CF898CC-847E-42dd-8BA9-47BCBF312F2E}��ɽ��
                            dr["��������"] = System.Math.Round((decimal)dr["�����ۼ�"] / (decimal)Scale, 4);
                            dr["���"] = NConvert.ToDecimal(dr["�����ۼ�"]) - NConvert.ToDecimal(dr["ԭ���ۼ�"]);
                            dr["�����۲��"] = NConvert.ToDecimal(dr["��������"]) - NConvert.ToDecimal(dr["ԭ������"]);
                        }
                    }
                }

                this.ucChooseList1.SetFoucs();
            }
        }

        /// <summary>
        /// ���ҩƷ�Ƿ���δ��Ч�ĵ��õ��д��ڡ�
        /// �����Գ���������������ε��۵���ʼ�۲�������
        /// </summary>
        protected bool SearchItem(string drugNO)
        {
            string find = this.itemManager.SearchAdjustPriceByItem(drugNO);
            return (find != "0");
        }

        /// <summary>
        /// ����ѡ��
        /// </summary>
        protected void ChooseTime()
        {
            //ѡ��ʱ��Σ����û��ѡ��ͷ���
            if (FS.FrameWork.WinForms.Classes.Function.ChooseDate(ref this.dtBegin, ref this.dtEnd) == 0) 
                return;

            //����ʱ�䣬ˢ�µ��۵��б�
            this.ShowList();
        }

        /// <summary>
        /// ��ʾһ���ڵ��۵��б�
        /// </summary>
        protected void ShowList()
        {
            this.ucChooseList1.TreeClear();

            ArrayList alList = null;
            if (this.adjustType == EnumAdjustType.ȫԺ����)
            {
                alList = this.itemManager.QueryAdjustPriceBillList(this.privDept.ID, this.dtBegin, this.dtEnd);
            }
            else
            {
                alList = this.itemManager.QueryAdjustPriceBillList(this.privDept.ID, this.dtBegin, this.dtEnd,true);
            }
            if (alList == null)
            {
                MessageBox.Show(Language.Msg("��ȡ���۵��б�������") + this.itemManager.Err);
                return;
            }
            if (alList.Count == 0)
            {
                TreeNode node = new TreeNode("û�е��۵�", 0, 0);
                this.ucChooseList1.TvList.Nodes.Add(node);
                return;
            }
            else
            {
                TreeNode node = new TreeNode("���۵��б�", 0, 0);
                this.ucChooseList1.TvList.Nodes.Add(node);
            }

            foreach (FS.HISFC.Models.Pharmacy.AdjustPrice adjustPrice in alList)
            {
                TreeNode node = new TreeNode();
                switch (adjustPrice.State)
                {
                    case "0":           //δִ��
                        node.Text = adjustPrice.ID + "[δִ��]";
                        node.ForeColor = System.Drawing.Color.Blue;     //��ɫ������ʾδִ�е�
                        node.ImageIndex = 4;
                        node.SelectedImageIndex = 5;
                        break;
                    case "1":           //��ִ��
                        node.Text = adjustPrice.ID + "[��ִ��]";
                        node.ImageIndex = 4;
                        node.SelectedImageIndex = 5;
                        break;
                    case "2":           //����
                        node.Text = adjustPrice.ID + "[����]";
                        node.ForeColor = System.Drawing.Color.Red;      //��ɫ������ʾ���ϵ�
                        node.ImageIndex = 4;
                        node.SelectedImageIndex = 5;
                        break;
                }
                node.Tag = adjustPrice;
                this.ucChooseList1.TvList.Nodes[0].Nodes.Add(node);
            }

            this.ucChooseList1.TvList.Nodes[0].ExpandAll();
            this.ucChooseList1.TvList.SelectedNode = this.ucChooseList1.TvList.Nodes[0];

            this.IsShowList = true;
        }

        /// <summary>
        /// ������ʾ
        /// </summary>
        protected void ShowData(FS.HISFC.Models.Pharmacy.AdjustPrice adjustPrice)
        {
            this.Clear();

            this.tempOperAdjustPrice = adjustPrice;
            this.adjustNO = adjustPrice.ID;
            //�������۵�
            if (adjustPrice.ID == "")
            {
                this.IsCanEdit = true;
                this.IsInitInsure = true;
                return;
            }

            if (adjustPrice.State == "0")       //�޸ĵĵ��۵�       ����༭
            {
                this.IsCanEdit = true;
                this.IsInitInsure = false;
            }
            else                                //��ִ�л����ϵ��۵� ������༭
            {
                this.IsCanEdit = false;
            }

            ArrayList alDetail = this.itemManager.QueryAdjustPriceInfoList(adjustPrice.ID);
            if (alDetail == null)
            {
                MessageBox.Show(Language.Msg(this.itemManager.Err));
                return;
            }
            bool isInit = false;            
            foreach (FS.HISFC.Models.Pharmacy.AdjustPrice info in alDetail)
            {
                if (!isInit)
                {
                    this.lblAdjustNumber.Text = "���۵���" + info.ID;                //���۵���
                    this.dtpDateTime.Value = info.InureTime;            //��Чʱ��
                    this.lbOperInfo.Text = string.Format("������ {0} ����ʱ�� {1}", info.Operation.Oper.Name, info.Operation.Oper.OperTime.ToString());
                    isInit = true;
                }

                //Ӧ��Ϊ��Sql����ڹ�����ȡ �˴���ʱ���»�ȡ��Ŀʵ������ֵ
                FS.HISFC.Models.Pharmacy.Item tempItem = this.itemManager.GetItem(info.Item.ID);

                info.Item.NameCollection.SpellCode = tempItem.NameCollection.SpellCode;
                info.Item.NameCollection.WBCode = tempItem.NameCollection.WBCode;
                info.Item.NameCollection.UserCode = tempItem.NameCollection.UserCode;

                this.AddDataToTable(info);
            }

            this.SetProfitFlag();            
        }

        /// <summary>
        /// �����µ�����Ŀ
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected int AddData(FS.HISFC.Models.Pharmacy.Item item)
        {
            FS.HISFC.Models.Pharmacy.AdjustPrice adjustPrice = new FS.HISFC.Models.Pharmacy.AdjustPrice();

            adjustPrice.Item = item;
            adjustPrice.Item.Product.Company.Name = this.companyHelper.GetName(adjustPrice.Item.Product.Company.ID);
            adjustPrice.Item.Product.Producer.Name = this.produceHelper.GetName(adjustPrice.Item.Product.Producer.ID);
            adjustPrice.AfterWholesalePrice = adjustPrice.Item.PriceCollection.WholeSalePrice;//{DC10D9C4-062C-4a92-8F16-08B5491E817E}
            if (this.isAutoNewPrice)
            {
                #region �Զ������¼۸�

                adjustPrice.AfterRetailPrice = this.GetNewPrice(adjustPrice.Item);

                #endregion
            }
            else
            {
                #region �ֹ������¼۸�

                adjustPrice.AfterRetailPrice = adjustPrice.Item.PriceCollection.RetailPrice;

                #endregion
            }

            return this.AddDataToTable(adjustPrice);
        }

        /// <summary>
        /// ��Ч��
        /// </summary>
        /// <returns></returns>
        protected bool Valid()
        {
            if (this.tempOperAdjustPrice == null || this.tempOperAdjustPrice.State == "1" || this.tempOperAdjustPrice.State == "2")
            {
                MessageBox.Show(Language.Msg("��ִ�л����ϵĵ��۵������ڽ��б����޸�"));
                return false;
            }

            foreach (DataRow dr in this.dt.Rows)
            {
                if (NConvert.ToDecimal(dr["�����ۼ�"]) < 0)
                {
                    MessageBox.Show(Language.Msg("���ۺ����ۼ۲���С��0"),"��ʾ",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    return false;
                }
                if (NConvert.ToDecimal(dr["��������"]) < 0)
                {
                    MessageBox.Show(Language.Msg("���ۺ������۲���С��0"), "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                if (NConvert.ToDecimal(dr["ԭ���ۼ�"]) == NConvert.ToDecimal(dr["�����ۼ�"]) && NConvert.ToDecimal(dr["ԭ������"]) == NConvert.ToDecimal(dr["��������"]))
                {
                    MessageBox.Show(Language.Msg("����ǰ�����ۼۺ������۲��ܶ���ͬ"), "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// �½����۵�
        /// </summary>
        /// <returns></returns>
        protected int New()
        {
            TreeNode node = new TreeNode();
            node.Text = "�½����۵�";
            node.ImageIndex = 4;
            node.SelectedImageIndex = 4;
            node.Tag = new FS.HISFC.Models.Pharmacy.AdjustPrice();

            this.ucChooseList1.TvList.Nodes[0].Nodes.Insert(0, node);

            //ѡ�д��½ڵ�
            this.ucChooseList1.TvList.SelectedNode = node;

            this.IsShowList = false;

            

            this.ucChooseList1.SetFoucs();

            return 1;
        }

        /// <summary>
        /// ����
        /// </summary>
        protected int Save()
        {
            #region ��Ч���ж�

            this.neuSpread1.StopCellEditing();

            if (!this.Valid())
            {
                return 0;
            }

            #endregion

            ////ϵͳʱ��
            DateTime sysTime = this.itemManager.GetDateTimeFromSysDateTime();

            #region ��Чʱ���ж�

            //��Чʱ��
            DateTime insureTime;
            if (this.chkValid.Checked)
                insureTime = sysTime;
            else
                insureTime = this.dtpDateTime.Value;

            //�ж���Чʱ���Ƿ���ڵ�ǰʱ��
            if (insureTime < sysTime)
            {
                MessageBox.Show(Language.Msg("������Чʱ�������ڵ�ǰʱ�䡣"), "��Чʱ����ʾ");
                return 0;
            }

            #endregion

            for (int i = 0; i < this.dt.Rows.Count; i++)
            {
                this.dt.Rows[i].EndEdit();
            }

            #region ������

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
            //t.BeginTransaction();

            this.itemManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            #endregion

            #region ���޸ĵ��۵�ɾ��ԭ���۵���Ϣ ���µ��۵���ȡ���۵���

            if (this.adjustNO != "")
            {
                if (this.itemManager.DeleteAdjustPriceInfo(this.adjustNO) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(Language.Msg("����ǰɾ��ԭ���۵���Ϣ��������" + this.itemManager.Err));
                    return -1;
                }
            }
            else
            {
                adjustNO = this.itemManager.GetSequence("Pharmacy.Item.GetNewAdjustPriceID");
                if (adjustNO == null)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(Language.Msg("��ȡ�µ��۵��ų���" + this.itemManager.Err));
                    return -1;
                }
            }

            #endregion

            int serialNO = 0;
            ArrayList alAdjustData = new ArrayList();

            ///{E49F9CEA-2E6D-4b2e-919F-99145BEE3E68}     ������۵�Э����ҩƷ ����1��2��3 ...��ŵĵط�Ϊ�����
            Dictionary<string, FS.HISFC.Models.Pharmacy.AdjustPrice> adjustNostrumList = new Dictionary<string, FS.HISFC.Models.Pharmacy.AdjustPrice>();
            //���۴�����ϸ
            Dictionary<string, List<FS.HISFC.Models.Pharmacy.Nostrum>> nostrumDetailList = new Dictionary<string, List<FS.HISFC.Models.Pharmacy.Nostrum>>();

            foreach (DataRow dr in this.dt.Rows)
            {
                #region ������Ϣ����

                FS.HISFC.Models.Pharmacy.AdjustPrice info = new FS.HISFC.Models.Pharmacy.AdjustPrice();
                string drugNO = dr["ҩƷ����"].ToString();
                if (this.SearchItem(drugNO))
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(Language.Msg(dr["��Ʒ����[���]"].ToString() + "�Ѿ���δ��Ч�ĵ��۵��д��ڣ������ڴ���ӡ�"));
                    return -1;
                }

                info.Item = this.itemManager.GetItem(drugNO);

                info.ID = this.adjustNO;                        //���۵���
                info.SerialNO = serialNO;                       //���۵������
                info.StockDept = this.privDept;                 //���ۿ���
                info.State = "0";                               //���۵�״̬��0��δ���ۣ�1���ѵ��ۣ�2����Ч
                info.Operation.Oper.ID = this.itemManager.Operator.ID;       //����Ա
                info.Operation.Oper.Name = this.itemManager.Operator.Name;  //����Ա����
                info.Operation.Oper.OperTime = sysTime;                     //����ʱ��

                info.InureTime = insureTime;                    //��Чʱ��

                info.AfterRetailPrice = NConvert.ToDecimal(dr["�����ۼ�"]); //���ۺ����ۼ�
                //{8CF898CC-847E-42dd-8BA9-47BCBF312F2E}��ɽ��
                info.AfterWholesalePrice = NConvert.ToDecimal(dr["��������"]); //���ۺ�������
                info.Memo = dr["����ԭ��"].ToString();

                if (info.Item.PriceCollection.RetailPrice > info.AfterRetailPrice)
                    info.ProfitFlag = "0";
                else
                    info.ProfitFlag = "1";

                if (this.itemManager.InsertAdjustPriceInfo(info) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(Language.Msg("���淢������" + this.itemManager.Err));
                    return -1;
                }

                #endregion

                alAdjustData.Add(info);

                #region {E49F9CEA-2E6D-4b2e-919F-99145BEE3E68}  Э�������۴���
                // 1. ���ݵ�ǰ����ҩƷID����ȡ������ҩƷ��Э��������Ϣ
                List<FS.HISFC.Models.Pharmacy.Nostrum> nostrumList = this.itemManager.QueryNostrumListByDetail( info.Item.ID );
                if (nostrumList == null)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show( "������ϸ��Ŀ��ȡЭ�����б���Ϣ��������   " + this.itemManager.Err );
                    return -1;
                }
                //2. �����а�����ҩƷ��Э������ ѭ�����е�����Ϣ��ֵ����
                foreach (FS.HISFC.Models.Pharmacy.Nostrum tempNostrum in nostrumList)
                {
                    //Э����������Ϣ
                    FS.HISFC.Models.Pharmacy.AdjustPrice tempNostrumAdjust = new FS.HISFC.Models.Pharmacy.AdjustPrice();

                    if (adjustNostrumList.ContainsKey( tempNostrum.ID ) == true)            //֮ǰ�����ɵ�����Ϣ���Լ۸����¸�ֵ
                    {
                        #region 2.1 ֮ǰ���γɹ�������Ϣ
                        //Э����������Ϣ
                        tempNostrumAdjust = adjustNostrumList[tempNostrum.ID];

                        tempNostrumAdjust.AfterRetailPrice = 0;
                        tempNostrumAdjust.AfterWholesalePrice = 0;

                        //2.1.1 ������ϸ���������ۼ�
                        List<FS.HISFC.Models.Pharmacy.Nostrum> tempNostrumDetail = new List<FS.HISFC.Models.Pharmacy.Nostrum>();
                        if (nostrumDetailList.ContainsKey( tempNostrum.ID ) == true)          //�ѻ�ȡ��������ϸ��Ϣ
                        {
                            tempNostrumDetail = nostrumDetailList[tempNostrum.ID];
                        }
                        else
                        {
                            tempNostrumDetail = this.itemManager.QueryNostrumDetail( tempNostrum.ID );
                            if (tempNostrumDetail == null)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show( "������ϸ��������Э������������Ϣʱ��������" + this.itemManager.Err );
                                return -1;
                            }
                            nostrumDetailList.Add( tempNostrum.ID, tempNostrumDetail );
                        }
                        foreach (FS.HISFC.Models.Pharmacy.Nostrum computeNostrum in tempNostrumDetail)
                        {
                            if (computeNostrum.Item.ID == info.Item.ID)                 //��ϸ��ID = ��ǰ����ID
                            {
                                computeNostrum.Item.PriceCollection.RetailPrice = info.AfterRetailPrice;
                                computeNostrum.Item.PriceCollection.WholeSalePrice = info.AfterWholesalePrice;
                            }

                            tempNostrumAdjust.AfterRetailPrice += computeNostrum.Qty  / computeNostrum.Item.PackQty * computeNostrum.Item.PriceCollection.RetailPrice;
                            tempNostrumAdjust.AfterWholesalePrice += computeNostrum.Qty  / computeNostrum.Item.PackQty * computeNostrum.Item.PriceCollection.WholeSalePrice;
                        }

                        //2.1.2  �γɵ��ۼ�¼ ���״̬���¸�ֵ
                        tempNostrumAdjust.AfterRetailPrice = Math.Round( tempNostrumAdjust.AfterRetailPrice, 2 );
                        tempNostrumAdjust.AfterWholesalePrice = Math.Round( tempNostrumAdjust.AfterWholesalePrice, 2 );

                        if (tempNostrumAdjust.Item.PriceCollection.RetailPrice > tempNostrumAdjust.AfterRetailPrice)
                        {
                            tempNostrumAdjust.ProfitFlag = "0";
                        }
                        else
                        {
                            tempNostrumAdjust.ProfitFlag = "1";
                        }

                        #endregion
                    }
                    else
                    {
                        #region 2.2 ֮ǰ�޵�����Ϣ ��������

                        serialNO++;                                                         //��������¼���к�����

                        //2.2.1 ��ȡЭ������ҩƷ�ֵ���Ϣ
                        tempNostrumAdjust.Item = this.itemManager.GetItem( tempNostrum.ID );

                        //2.2.2 Э������������Ϣ��ֵ
                        tempNostrumAdjust.ID = this.adjustNO;
                        tempNostrumAdjust.SerialNO = serialNO;                       //���۵������
                        tempNostrumAdjust.StockDept = this.privDept;                 //���ۿ���
                        tempNostrumAdjust.State = "0";                               //���۵�״̬��0��δ���ۣ�1���ѵ��ۣ�2����Ч
                        tempNostrumAdjust.Operation.Oper.ID = this.itemManager.Operator.ID;       //����Ա
                        tempNostrumAdjust.Operation.Oper.Name = this.itemManager.Operator.Name;  //����Ա����
                        tempNostrumAdjust.Operation.Oper.OperTime = sysTime;                     //����ʱ��

                        tempNostrumAdjust.InureTime = insureTime;                    //��Чʱ��

                        tempNostrumAdjust.Memo = info.Item.ID + "  ��ϸ��������ӦЭ����������";

                        //2.2.2 ������ϸ���������ۼ�
                        tempNostrumAdjust.AfterRetailPrice = 0;
                        tempNostrumAdjust.AfterWholesalePrice = 0;

                        List<FS.HISFC.Models.Pharmacy.Nostrum> tempNostrumDetail = new List<FS.HISFC.Models.Pharmacy.Nostrum>();
                        if (nostrumDetailList.ContainsKey( tempNostrum.ID ) == true)          //�ѻ�ȡ��������ϸ��Ϣ
                        {
                            tempNostrumDetail = nostrumDetailList[tempNostrum.ID];
                        }
                        else
                        {
                            tempNostrumDetail = this.itemManager.QueryNostrumDetail( tempNostrum.ID );
                            if (tempNostrumDetail == null)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show( "������ϸ��������Э������������Ϣʱ��������" + this.itemManager.Err );
                                return -1;
                            }
                            nostrumDetailList.Add( tempNostrum.ID, tempNostrumDetail );
                        }
                        foreach (FS.HISFC.Models.Pharmacy.Nostrum computeNostrum in tempNostrumDetail)
                        {
                            if (computeNostrum.Item.ID == info.Item.ID)                 //��ϸ��ID = ��ǰ����ID
                            {
                                computeNostrum.Item.PriceCollection.RetailPrice = info.AfterRetailPrice;
                                computeNostrum.Item.PriceCollection.WholeSalePrice = info.AfterWholesalePrice;
                            }
                            //Э�����۸���㣺������һ��װ��λЭ����������ϸ�ܶЭ��������
                            tempNostrumAdjust.AfterRetailPrice += computeNostrum.Qty / computeNostrum.Item.PackQty * computeNostrum.Item.PriceCollection.RetailPrice;
                            tempNostrumAdjust.AfterWholesalePrice += computeNostrum.Qty / computeNostrum.Item.PackQty * computeNostrum.Item.PriceCollection.WholeSalePrice;
                        }

                        //2.2.3  �γɵ��ۼ�¼ ���״̬���¸�ֵ
                        tempNostrumAdjust.AfterRetailPrice = Math.Round( tempNostrumAdjust.AfterRetailPrice, 2 );
                        tempNostrumAdjust.AfterWholesalePrice = Math.Round( tempNostrumAdjust.AfterWholesalePrice, 2 );

                        if (tempNostrumAdjust.Item.PriceCollection.RetailPrice > tempNostrumAdjust.AfterRetailPrice)
                        {
                            tempNostrumAdjust.ProfitFlag = "0";
                        }
                        else
                        {
                            tempNostrumAdjust.ProfitFlag = "1";
                        }

                        adjustNostrumList.Add( tempNostrum.ID, tempNostrumAdjust );

                        #endregion
                    }                   
                }

                #endregion

                serialNO++;
            }

            #region 3. ��Э������ҩƷ�γɵ��ۼ�¼  {E49F9CEA-2E6D-4b2e-919F-99145BEE3E68}

            foreach (FS.HISFC.Models.Pharmacy.AdjustPrice tempAdjust in adjustNostrumList.Values)
            {
                if (this.itemManager.InsertAdjustPriceInfo( tempAdjust ) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show( Language.Msg( "���淢������" + this.itemManager.Err ) );
                    return -1;
                }
            }

            #endregion

            FS.FrameWork.Management.PublicTrans.Commit();

            this.Print(alAdjustData);

            #region ��������Ч��ִ�д洢����

            if (this.chkValid.Checked)
            {
                if (this.itemManager.ExecProcedureChangPrice() == -1)
                {
                    MessageBox.Show(Language.Msg("ִ�е��۴洢���̷�������" + this.itemManager.Err));
                    return -1;
                }
            }

            #endregion

            MessageBox.Show(Language.Msg(" ����ɹ� "));

            this.Clear();

            this.ShowList();

            return 1;
        }

        /// <summary>
        /// ���浥���ҵ�����Ϣ
        /// </summary>
        /// <returns></returns>
        protected int SaveDeptAdjust()
        {
            #region ��Ч���ж�

            if (!this.Valid())
            {
                return 0;
            }

            #endregion

            ////ϵͳʱ��
            DateTime sysTime = this.itemManager.GetDateTimeFromSysDateTime();

            #region ��Чʱ���ж�

            //��Чʱ��
            DateTime insureTime;
            if (this.chkValid.Checked)
                insureTime = sysTime;
            else
                insureTime = this.dtpDateTime.Value;

            //�ж���Чʱ���Ƿ���ڵ�ǰʱ��
            if (insureTime < sysTime)
            {
                MessageBox.Show(Language.Msg("������Чʱ�������ڵ�ǰʱ�䡣"), "��Чʱ����ʾ");
                return 0;
            }

            #endregion

            for (int i = 0; i < this.dt.Rows.Count; i++)
            {
                this.dt.Rows[i].EndEdit();
            }

            #region ������
            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
            //t.BeginTransaction();

            this.itemManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            #endregion

            #region ���޸ĵ��۵�ɾ��ԭ���۵���Ϣ ���µ��۵���ȡ���۵���

            if (this.adjustNO != "")
            {
                if (this.itemManager.DeleteAdjustPriceInfo(this.adjustNO) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(Language.Msg("����ǰɾ��ԭ���۵���Ϣ��������" + this.itemManager.Err));
                    return -1;
                }
            }
            else
            {
                adjustNO = this.itemManager.GetSequence("Pharmacy.Item.GetNewAdjustPriceID");
                if (adjustNO == null)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(Language.Msg("��ȡ�µ��۵��ų���" + this.itemManager.Err));
                    return -1;
                }
            }

            #endregion

            int serialNO = 0;
            foreach (DataRow dr in this.dt.Rows)
            {
                #region ������Ϣ����

                FS.HISFC.Models.Pharmacy.AdjustPrice info = new FS.HISFC.Models.Pharmacy.AdjustPrice();
                string drugNO = dr["ҩƷ����"].ToString();

                #region ������ж�

                decimal storageNum = 1;
                if (this.itemManager.GetStorageNum(this.privDept.ID, drugNO, out storageNum) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(Language.Msg("��ȡ���ҵ�ǰ���ʱ��������") + this.itemManager.Err);
                    return -1;
                }
                if (storageNum > 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(Language.Msg(dr["��Ʒ����[���]"].ToString() + "��ǰ�Դ��ڿ���������ܽ��е��Ƶ���"));
                    return -1;
                }

                #endregion

                if (this.SearchItem(drugNO))
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(Language.Msg(dr["��Ʒ����[���]"].ToString() + "�Ѿ���δ��Ч�ĵ��۵��д��ڣ������ڴ���ӡ�"));
                    return -1;
                }

                info.Item = this.itemManager.GetItem(drugNO);               

                info.ID = this.adjustNO;                        //���۵���
                info.SerialNO = serialNO;                       //���۵������
                info.StockDept = this.privDept;                 //���ۿ���
                info.State = "1";                               //���۵�״̬��0��δ���ۣ�1���ѵ��ۣ�2����Ч
                info.Operation.Oper.ID = this.itemManager.Operator.ID;       //����Ա
                info.Operation.Oper.Name = this.itemManager.Operator.Name;  //����Ա����
                info.Operation.Oper.OperTime = sysTime;                     //����ʱ��

                info.InureTime = insureTime;                    //��Чʱ��

                info.AfterRetailPrice = NConvert.ToDecimal(dr["�����ۼ�"]); //���ۺ����ۼ�
                info.Memo = dr["����ԭ��"].ToString() + " - ���Ƶ���";

                info.IsDDAdjust = true;

                if (info.Item.PriceCollection.RetailPrice > info.AfterRetailPrice)
                    info.ProfitFlag = "0";
                else
                    info.ProfitFlag = "1";

                if (this.itemManager.InsertAdjustPriceInfo(info) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(Language.Msg("���Ƶ��۱�����ۻ��ܷ�������" + this.itemManager.Err));
                    return -1;
                }

                if (this.itemManager.InsertAdjustPriceDetail(info) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(Language.Msg("���Ƶ��۱��������ϸ��������" + this.itemManager.Err));
                    return -1;
                }

                int param = this.itemManager.UpdateStoragePrice(info.StockDept.ID, info.Item.ID, info.AfterRetailPrice);
                if (param == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(Language.Msg("���µ��Ƽ۸���Ϣ��������") + this.itemManager.Err);
                    return -1;
                }
                else if (param == 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(dr["��Ʒ����[���]"].ToString() + Language.Msg("���Ƶ���ҩƷ������������������ҩƷ����ǰҩƷ�ڱ����޿��"));
                    return -1;
                }

                serialNO++;

                #endregion
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            MessageBox.Show(Language.Msg(" ����ɹ� "));

            this.Clear();

            this.ShowList();

            return 1;
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        protected int GroupAdjust()
        {
            if (this.frmGroup == null)
            {
                this.frmGroup = new frmGroupAdjust();
            }

            DialogResult rsT = this.frmGroup.ShowDialog();
            if (rsT == System.Windows.Forms.DialogResult.OK)
            {
                //{D5CD15B5-617B-4a06-9EBC-7BC589CBD7D9} ����ҩƷ����������� ������ckOnlyPrice.Visible = true
                if (this.frmGroup.PriceException == "")
                {
                    this.isAutoNewPrice = false;
                }
                else
                {
                    this.isAutoNewPrice = true;
                }

                if (this.frmGroup.AdjustItems == null)
                    return 0;

                if (this.frmGroup.AdjustItems.Count > 0)
                {
                    System.Windows.Forms.DialogResult rs;
                    rs = MessageBox.Show(Language.Msg("ʹ���������۽������ǰ���� �Ƿ����"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (rs == System.Windows.Forms.DialogResult.No)
                        return -1;

                    this.Clear();

                    FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("������������ҩƷ���� ���Ժ�..");
                    Application.DoEvents();

                    int i = 1;
                    foreach (FS.HISFC.Models.Pharmacy.Item info in this.frmGroup.AdjustItems)
                    {
                        FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(i, this.frmGroup.AdjustItems.Count);
                        Application.DoEvents();

                        i++;
                        
                        //this.AddData(info);
                        if (this.AddData(info) == -1)
                        {
                            i--;
                        }
                        //��������ʱ�Զ����������� by Sunjh 2010-9-1 {B978D218-127A-4511-BB47-0E132AB1860A}
                        if (this.neuSpread1_Sheet1.ActiveRowIndex >= 0)
                        {
                            string[] keys = new string[] { this.neuSpread1_Sheet1.Cells[i - 2, (int)ColumnSet.ColDrugNO].Text };
                            DataRow dr = this.dt.Rows.Find(keys);
                            if (dr != null)
                            {
                                //{8CF898CC-847E-42dd-8BA9-47BCBF312F2E}��ɽ��
                                dr["��������"] = System.Math.Round((decimal)dr["�����ۼ�"] / (decimal)Scale, 4);
                                dr["���"] = NConvert.ToDecimal(dr["�����ۼ�"]) - NConvert.ToDecimal(dr["ԭ���ۼ�"]);
                                dr["�����۲��"] = NConvert.ToDecimal(dr["��������"]) - NConvert.ToDecimal(dr["ԭ������"]);

                                this.SetProfitFlag();
                            }
                        }

                    }
                    this.SetProfitFlag();//{DC10D9C4-062C-4a92-8F16-08B5491E817E}
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                }
                this.isAutoNewPrice = true;
            }

            return 1;
        }

        protected int Print(ArrayList alAdjustData)
        {
            FS.HISFC.BizProcess.Interface.Pharmacy.IBillPrint billPrint = null;

            billPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Pharmacy.IBillPrint)) as FS.HISFC.BizProcess.Interface.Pharmacy.IBillPrint;

            if (billPrint != null)
            {
                DialogResult rs = MessageBox.Show("�Ƿ��ӡ���۵�", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (rs == DialogResult.Yes)
                {
                    billPrint.SetData(alAdjustData, FS.HISFC.BizProcess.Interface.Pharmacy.BillType.Adjust);
                    billPrint.Print();
                }
            }

            return 1;
        }

        /// <summary>
        /// �����ҵ���м�����У����ʾ
        /// </summary>
        /// <param name="itemCode">ҩƷ����</param>
        /// <returns>�ɹ�����True ʧ�ܷ���False</returns>
        protected bool InterimDataVerify(string itemCode)
        {
            return true;
        }

        #endregion

        private void ucAdjustPrice_Load(object sender, EventArgs e)
        {
            this.ucChooseList1.TvList.ImageList = this.ucChooseList1.TvList.deptImageList;

            this.InitDataTable();

            this.SetFormat();

            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToUpper() != "DEVENV")
            {
                this.InitData();

                this.InitEvent();

                this.ShowList();
            }
            
        }

        private void ucChooseList1_ChooseDataEvent(FarPoint.Win.Spread.SheetView sv, int activeRowIndex)
        {
            if (activeRowIndex < 0) 
                return;

            string drugNO = sv.Cells[activeRowIndex, 0].Value.ToString();

            string[] keys = new string[]{drugNO};
            DataRow dr = this.dt.Rows.Find(keys);
            if (dr != null)
            {
                MessageBox.Show(Language.Msg("��ҩƷ�����"));
                return;
            }
            if (this.SearchItem(drugNO))
            {
                MessageBox.Show(Language.Msg("��ҩƷ���ϴε��ۻ�δ��Ч�������ظ����ۡ�"), "");
                return;
            }

            //����ҩƷ���룬ȡҩƷ��Ϣ
            FS.HISFC.Models.Pharmacy.Item item = this.itemManager.GetItem(drugNO);
            if (item == null)
            {
                MessageBox.Show(Language.Msg(this.itemManager.Err));
                return;
            }

            #region Э������������ʾ

            List<FS.HISFC.Models.Pharmacy.Nostrum> nostrumList = this.itemManager.QueryNostrumListByDetail( item.ID );
            if (nostrumList == null)
            {
                MessageBox.Show( Language.Msg( this.itemManager.Err ) );
                return;
            }

            if (nostrumList.Count > 0)
            {
                MessageBox.Show( "��ҩƷ���ڶ��Э����������ɳɷ��ڣ��Ը�ҩƷ���۽����������Э�������ĵ���","��ʾ",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }

            #endregion

            if (this.AddData(item) == 1)
            {
                this.SetFocus(true);
            }
            else
            {
                this.SetFocus(false);
            }           
        }

        private void TvList_DoubleClick(object sender, EventArgs e)
        {
            if (this.ucChooseList1.TvList.SelectedNode.Tag != null)
            {
                if (this.tempOperAdjustPrice.ID != "" && this.tempOperAdjustPrice.State == "0")
                    this.IsShowList = false;
            }
        }

        private void TvList_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag == null)
            {
                this.Clear();
            }
            else
            {
                this.ShowData(e.Node.Tag as FS.HISFC.Models.Pharmacy.AdjustPrice);
            }
        }

        private void chkValid_CheckedChanged(object sender, EventArgs e)
        {
            this.dtpDateTime.Enabled = !this.chkValid.Checked;
        }
        
        private void neuSpread1_EditModeOff(object sender, EventArgs e)
        {
            //if (this.isExeEditModeOffEvent) 
            //{
            //    this.isExeEditModeOffEvent = false;
            //}
            if((int)ColumnSet.ColRetailPrice == this.neuSpread1_Sheet1.ActiveColumnIndex&&this.neuSpread1_Sheet1.RowCount>0)
            {
                if (this.neuSpread1_Sheet1.ActiveColumnIndex == (int)ColumnSet.ColRetailPrice && this.neuSpread1_Sheet1.ActiveRowIndex >= 0)
                {
                    string[] keys = new string[] { this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, (int)ColumnSet.ColDrugNO].Text };
                    DataRow dr = this.dt.Rows.Find(keys);
                    if (dr != null)
                    {
                        //{8CF898CC-847E-42dd-8BA9-47BCBF312F2E}��ɽ��
                        dr["��������"] = System.Math.Round((decimal)dr["�����ۼ�"] / (decimal)Scale, 4);
                        dr["���"] = NConvert.ToDecimal(dr["�����ۼ�"]) - NConvert.ToDecimal(dr["ԭ���ۼ�"]);
                        dr["�����۲��"] = NConvert.ToDecimal(dr["��������"]) - NConvert.ToDecimal(dr["ԭ������"]);

                        this.SetProfitFlag();
                    }
                }
            }
        }

        

        private void neuSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            
        }


        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (this.neuSpread1.ContainsFocus)
            {
                //if (keyData == Keys.A)
                //{
                //    return true;
                //}
                if (keyData == Keys.Enter)
                {
                    this.neuSpread1.StopCellEditing();
                    this.SetFocus(false);
                    //if (this.neuSpread1_Sheet1.ActiveRowIndex == this.neuSpread1_Sheet1.Rows.Count - 1)
                    //{
                    //    this.SetFocus(false);
                    //}
                    //else
                    //{
                    //    this.neuSpread1_Sheet1.ActiveRowIndex++;
                    //}
                }
            }
            if (keyData == Keys.F5)
            {
                this.SetFocus(false);
            }

            

            return base.ProcessDialogKey(keyData);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.textBox1.Text != "")
                    this.dt.DefaultView.RowFilter = Function.GetFilterStr(this.dt.DefaultView, "%" + this.textBox1.Text + "%");
                else
                    this.dt.DefaultView.RowFilter = "1=1";

                this.SetProfitFlag();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Language.Msg(ex.Message));
            }
        }  

        private enum ColumnSet
        {
            /// <summary>
            /// ��Ʒ���ƹ��
            /// </summary>
            ColTradeName,
            /// <summary>
            /// ������˾
            /// </summary>
            ColCompany,
            /// <summary>
            /// ��������
            /// </summary>
            ColProduce,
            /// <summary>
            /// ԭ���ۼ�		
            /// </summary>
            ColPreRetailPrice,
            /// <summary>
            /// ԭ������  {8CF898CC-847E-42dd-8BA9-47BCBF312F2E}��ɽ��
            /// </summary>
            ColPreTradePrice,
            /// <summary>
            /// �����ۼ�
            /// </summary>
            ColRetailPrice,
            /// <summary>
            /// ��������  {8CF898CC-847E-42dd-8BA9-47BCBF312F2E}��ɽ��
            /// </summary>
            ColTradePrice,
            /// <summary>
            /// ��� 
            /// </summary>
            ColBalancePrice,
            /// <summary>
            /// �����۲��  {8CF898CC-847E-42dd-8BA9-47BCBF312F2E}��ɽ��
            /// </summary>
            ColBalanceTradePrice,
            /// <summary>
            /// ����ԭ��
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
            ColWBCode,
            /// <summary>
            /// �Զ�����
            /// </summary>
            ColUserCode
        }

        /// <summary>
        /// ���۷�Χ
        /// </summary>
        public enum EnumAdjustType
        {
            ȫԺ����,
            ���Ƶ���
        }

        #region IInterfaceContainer ��Ա

        public Type[] InterfaceTypes
        {
            get
            {
                return new Type[] { 
                    typeof(FS.HISFC.BizProcess.Interface.Pharmacy.IBillPrint)
                };
            }
        }

        #endregion

        #region IPreArrange ��Ա  {543F5224-6BCB-4645-8D86-4E7EA8BDF80E}  ���ӶԵ��Ƶ����õ�Ԥ����ʾ���ж�

        public int PreArrange()
        {
            if (this.adjustType == EnumAdjustType.���Ƶ���)
            {
                FS.HISFC.BizLogic.Manager.Department deptManager = new FS.HISFC.BizLogic.Manager.Department();

                //Ȩ�޿��� ��ʱʹ�ò���Ա���Ҵ���
                this.privDept = ((FS.HISFC.Models.Base.Employee)deptManager.Operator).Dept;

                FS.HISFC.Models.Base.Department dept = new FS.HISFC.Models.Base.Department();

                dept = deptManager.GetDeptmentById(this.privDept.ID);
                if (dept.DeptType.ID.ToString() != "PI")
                {
                    MessageBox.Show(Language.Msg("ֻ��ҩ��������е��Ƶ��۲���"), "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    
                    return -1;
                }
                else
                {
                    MessageBox.Show(Language.Msg("��ע�� ��ѡ����ǵ��Ƶ��۹���"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                }
                return 1;
            }

            return 1;
        }

        #endregion
    }
}
