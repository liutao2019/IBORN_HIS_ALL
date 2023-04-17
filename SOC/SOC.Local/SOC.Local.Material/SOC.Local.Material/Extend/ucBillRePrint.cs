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


namespace FS.SOC.Local.Material.Extend
{
    public partial class ucBillRePrint : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucBillRePrint()
        {
            InitializeComponent();
        }

        private FS.FrameWork.Models.NeuObject privDept = new FS.FrameWork.Models.NeuObject();

        private string billtype = "NO";

        private string billstate = "A";

        private bool isprint = false;

        private int decimals = 4;

        FS.HISFC.BizLogic.Material.BizLogic.Base.BaseLogic baseLogic = new FS.HISFC.BizLogic.Material.BizLogic.Base.BaseLogic();
        private FS.HISFC.BizProcess.Material.Store.StockProcess stockProcess = new FS.HISFC.BizProcess.Material.Store.StockProcess();
        private FS.HISFC.Interface.Material.InterfaceProxy.ManagerProxy managerProxy = new FS.HISFC.Interface.Material.InterfaceProxy.ManagerProxy();
        private FS.HISFC.Interface.Material.InterfaceProxy.BillPrintProxy printProxy = new FS.HISFC.Interface.Material.InterfaceProxy.BillPrintProxy();

        private Hashtable hsInputPriv = new Hashtable();
        private Hashtable hsOutputPriv = new Hashtable();

        private DataView dvInput = null;

        private DataView dvOutput = null;

        protected object inputInstance;

        protected object outputInstance;

        private bool isShowAsPrint = true;

        [Browsable(false)]
        public string BillType
        {
            get
            {
                return this.billtype;
            }
            set
            {
                if (value != this.billtype)
                {
                    this.billtype = value;
                }
            }
        }

        [Browsable(false)]
        public string BillState
        {
            get
            {
                if (this.billstate == null || this.billstate == "")
                {

                    return "A";
                }
                else
                {

                    return this.billstate;
                }
            }
            set
            {
                this.billstate = value;
            }
        }

        [Browsable(false)]
        public int ActiveSheet
        {
            get
            {
                return this.neuSpread1.ActiveSheetIndex;
            }
            set
            {
                this.neuSpread1.ActiveSheetIndex = value;
            }
        }


        [Browsable(false)]
        public new bool IsPrint
        {
            get
            {
                return this.isprint;
            }
            set
            {
                this.isprint = value;
            }
        }

        [Description("�����������ʾ����"), Category("����"), DefaultValue(4)]
        public int Decimals
        {
            get
            {
                return decimals;
            }
            set
            {
                decimals = value;
            }
        }

        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("ȫ��ѡ", "���ѡ����", FS.FrameWork.WinForms.Classes.EnumImageList.Qȫ��ѡ, true, false, null);

            return toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "ȫ��ѡ")
            {
                //this.SelectAll(false);
            }

            base.ToolStrip_ItemClicked(sender, e);
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            //this.neuSpread1_Sheet1.Reset();
            this.neuSpread1_Sheet1.RowCount = 0;
            this.neuSpread1_Sheet1.ColumnCount = 7;
            this.Query();

            return base.OnQuery(sender, neuObject);
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            this.Print();

            return 1;
        }

        private void Init()
        {
            //this.InitFp();
            this.neuSpread1.ActiveSheetIndex = 0;

            DateTime dt = this.baseLogic.GetDateTimeFromSysDateTime();
            this.dtEnd.Value = dt;
            dt = dt.AddDays(-3);
            this.dtBegin.Value = new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0);
        }

        private void InitClass3Helper()
        {
            int inputPriv = FrameWork.Function.NConvert.ToInt32("5510");
            int outputPriv = FrameWork.Function.NConvert.ToInt32("5520");
            ArrayList alInputPriv = this.managerProxy.LoadLevel3ByLevel2(inputPriv.ToString());
            if (alInputPriv == null || alInputPriv.Count == 0)
            {
                MessageBox.Show("��ȡ���Ȩ�ް��������!");
                return;
            }
            ArrayList alOutputPriv = this.managerProxy.LoadLevel3ByLevel2(outputPriv.ToString());
            if (alOutputPriv == null || alOutputPriv.Count == 0)
            {
                MessageBox.Show("��ȡ����Ȩ�ް��������!");
                return;
            }

            foreach (FS.HISFC.Models.Admin.PowerLevelClass3 class3 in alInputPriv)
            {
                if (this.hsInputPriv.ContainsKey(class3.Class3Code) == false)
                {
                    this.hsInputPriv.Add(class3.Class3Code, class3.Class3Name);
                }
            }
            foreach (FS.HISFC.Models.Admin.PowerLevelClass3 class3Output in alOutputPriv)
            {
                if (this.hsOutputPriv.ContainsKey(class3Output.Class3Code) == false)
                {
                    this.hsOutputPriv.Add(class3Output.Class3Code, class3Output.Class3Name);
                }
            }
        }

        private void InitFp()
        {
            #region ����
            this.neuSpread1_Sheet1.Reset();
            this.neuSpread1_Sheet1.ColumnCount = 2;
            this.neuSpread1_Sheet1.RowCount = 30;
            this.neuSpread1_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.SystemColors.AppWorkspace, System.Drawing.Color.White, System.Drawing.Color.Black, System.Drawing.Color.White, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.White, System.Drawing.Color.White, System.Drawing.Color.White, System.Drawing.Color.White, System.Drawing.Color.White, System.Drawing.Color.White, true, true, true, true, true);
            this.neuSpread1_Sheet1.Cells.Get(0, 1).Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Bold);
            this.neuSpread1_Sheet1.Cells.Get(0, 1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Cells.Get(0, 1).Text = "����˵��";
            this.neuSpread1_Sheet1.Cells.Get(1, 1).Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Bold);
            this.neuSpread1_Sheet1.Cells.Get(1, 1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuSpread1_Sheet1.Cells.Get(1, 1).Text = "һ���Ķ�˵��";
            this.neuSpread1_Sheet1.Cells.Get(2, 1).Text = "    1������������˵��ʱ����δ�����κβ��������������ȷ��β������������Ķ�";
            this.neuSpread1_Sheet1.Cells.Get(3, 1).Text = "    2�����Ķ�ʱ�����һ�μ�ס��˵�����ݣ������޷�����һ���Ķ�һ�߲���";
            this.neuSpread1_Sheet1.Cells.Get(4, 1).Text = "    3����������Ҫ�鿴��˵��ʱ��ѡ��[����]��ť";
            this.neuSpread1_Sheet1.Cells.Get(5, 1).Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Bold);
            this.neuSpread1_Sheet1.Cells.Get(5, 1).Text = "��������ѡ��";
            this.neuSpread1_Sheet1.Cells.Get(6, 1).Text = "    1������൥�����������б���ѡ��õ������͡����ң���ɫ��������Ч[���������е���]";
            this.neuSpread1_Sheet1.Cells.Get(7, 1).Font = new System.Drawing.Font("����", 9F);
            this.neuSpread1_Sheet1.Cells.Get(7, 1).Text = "    2��ѡ��ʼ�ͽ���ʱ�䣬����Ĭ��Ϊ4��";
            this.neuSpread1_Sheet1.Cells.Get(8, 1).Text = "    3���������ⵥ��ѡ��״̬������Ĭ��Ϊȫ��";
            this.neuSpread1_Sheet1.Cells.Get(9, 1).Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Bold);
            this.neuSpread1_Sheet1.Cells.Get(9, 1).Text = "������β�ѯ";
            this.neuSpread1_Sheet1.Cells.Get(10, 1).Text = "    1��ȷ���ڶ�����ɺ���[��ѯ]��ť����ʾѡ�������ڵ����б�";
            this.neuSpread1_Sheet1.Cells.Get(11, 1).Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Bold);
            this.neuSpread1_Sheet1.Cells.Get(11, 1).Text = "�ġ�Ϊ��ˢ��";
            this.neuSpread1_Sheet1.Cells.Get(12, 1).Text = "    1�������������ʱ������󣬿��������������������ݱ䶯����ʱ��[ˢ��]";
            this.neuSpread1_Sheet1.Cells.Get(13, 1).Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Bold);
            this.neuSpread1_Sheet1.Cells.Get(13, 1).Text = "�塢�鿴��ϸ";
            this.neuSpread1_Sheet1.Cells.Get(14, 1).Text = "    1����ѯ��ɺ�˫���б�[���ܱ�]��Ŀ����ʾ��ϸ";
            this.neuSpread1_Sheet1.Cells.Get(15, 1).Text = "    2����ⵥӦ��ѡ��[ѡ��]�����򹳺���Ч[����ѡ�л�ȡ��]������Ĭ������������";
            this.neuSpread1_Sheet1.Cells.Get(16, 1).Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Bold);
            this.neuSpread1_Sheet1.Cells.Get(16, 1).Text = "������δ�ӡ";
            this.neuSpread1_Sheet1.Cells.Get(17, 1).Text = "    1���ڻ��ܱ��е���ѡ��Ҫ��ӡ�ĵ���";
            this.neuSpread1_Sheet1.Cells.Get(18, 1).Text = "    2��ѡ��[��ӡ]��ť���ɴ�ӡ";
            this.neuSpread1_Sheet1.Cells.Get(19, 1).Text = "    3����ⵥ���԰���Ʊ��ӡ��Ӧ��ѡ��[ѡ��]�����򹳺���Ч[����ѡ�л�ȡ��]������Ĭ������������";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Text = "����˵��";
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Bold);
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.ForeColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.Columns.Get(1).ForeColor = System.Drawing.Color.Black;
            this.neuSpread1_Sheet1.Columns.Get(1).Label = "����˵��";
            this.neuSpread1_Sheet1.Columns.Get(1).Width = 732F;
            this.neuSpread1_Sheet1.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.DefaultStyle.ForeColor = System.Drawing.Color.Black;
            this.neuSpread1_Sheet1.DefaultStyle.Parent = "DataAreaDefault";
            this.neuSpread1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.ReadOnly;
            this.neuSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.neuSpread1_Sheet1.RowHeader.Columns.Get(0).Width = 0F;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Bold);
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.ForeColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.Single;
            this.neuSpread1_Sheet1.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.neuSpread1_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.SheetCornerStyle.ForeColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet1.SheetCornerStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet1.SheetName = "����";
            #endregion

            #region ��ϸ
            this.neuSpread1_Sheet2.Reset();
            this.neuSpread1_Sheet2.ColumnCount = 15;
            this.neuSpread1_Sheet2.RowCount = 50;
            this.neuSpread1_Sheet2.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.SystemColors.AppWorkspace, System.Drawing.Color.White, System.Drawing.Color.Black, System.Drawing.Color.White, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.White, System.Drawing.Color.White, System.Drawing.Color.FromArgb(((System.Byte)(206)), ((System.Byte)(93)), ((System.Byte)(90))), System.Drawing.Color.White, System.Drawing.Color.White, System.Drawing.Color.White, true, true, true, true, true);
            this.neuSpread1_Sheet2.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet2.ColumnHeader.DefaultStyle.ForeColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet2.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet2.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet2.DefaultStyle.ForeColor = System.Drawing.Color.Black;
            this.neuSpread1_Sheet2.DefaultStyle.Parent = "DataAreaDefault";
            this.neuSpread1_Sheet2.RowHeader.Columns.Default.Resizable = false;
            this.neuSpread1_Sheet2.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet2.RowHeader.DefaultStyle.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Bold);
            this.neuSpread1_Sheet2.RowHeader.DefaultStyle.ForeColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet2.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet2.SheetCornerStyle.BackColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet2.SheetCornerStyle.ForeColor = System.Drawing.Color.White;
            this.neuSpread1_Sheet2.SheetCornerStyle.Parent = "HeaderDefault";
            this.neuSpread1_Sheet2.SheetName = "��ϸ";
            #endregion
        }

        public void Query()
        {
            this.neuSpread1.ActiveSheetIndex = 0;

            if (this.BillType == "NO")
            {
                MessageBox.Show(this, Language.Msg("��ѡ�񵥾�����"), "��ʾ", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                return;
            }
            else if (BillType == "I")
            {
                this.QueryInputData();
            }
            else if (BillType == "O")
            {
                this.QueryOutputData();
            }

            else
            {
                MessageBox.Show(this, Language.Msg("�޷�ʶ��ĵ�������") + this.BillType, "����>>", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

        }

        public void Print()
        {
            if (this.neuSpread1_Sheet1.RowCount == 0)
            {
                MessageBox.Show(Language.Msg("û�пɴ�ӡ������"));
                return;
            }
            if (this.neuSpread1_Sheet1.ActiveRowIndex < 0)
            {
                MessageBox.Show(Language.Msg("û��ѡ��Ҫ��ӡ�ĵ���"));
                return;
            }
            //this.txtFilter.Text = "";
            this.neuSpread1_Sheet2.RowCount = 0;
            this.isprint = true;
            //���
            if (this.BillType == "I")
            {
                this.PrintInputBill(this.neuSpread1_Sheet1.ActiveRowIndex);
            }
            //����
            if (this.BillType == "O")
            {
                this.PrintOutputBill(this.neuSpread1_Sheet1.ActiveRowIndex);
            }

            this.neuSpread1.ActiveSheetIndex = 1;
        }

        public void QueryInputData()
        {
            List<FS.HISFC.BizLogic.Material.Object.InputHead> inputHeadList = this.stockProcess.QueryInputInfoByCondition(this.privDept.ID, this.dtBegin.Value.Date, this.dtEnd.Value.Date.AddDays(1).AddSeconds(-1), "ALL", "ALL");
            if (inputHeadList == null || inputHeadList.Count == 0)
            {
                MessageBox.Show(Language.Msg(this.stockProcess.Err + "\nδ�ܲ�ѯ����������������¼"), Language.Msg("��ʾ"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            foreach (FS.HISFC.BizLogic.Material.Object.InputHead head in inputHeadList)
            {
                this.AddInputHeadData(head);
            }
        }

        private void PrintInputBill(int activeRowIndex)
        {
            string class3MeaningCode = string.Empty;
            FS.HISFC.BizLogic.Material.Object.InputHead headInfo = this.neuSpread1_Sheet1.ActiveRow.Tag as FS.HISFC.BizLogic.Material.Object.InputHead;
            if (headInfo == null)
            {
                MessageBox.Show(Language.Msg("��ȡѡ����������Ϣ����"), Language.Msg("��ʾ"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (headInfo.InputList == null || headInfo.InputList.Count <= 0)
            {
                MessageBox.Show(Language.Msg("δ����ѡ�л�����Ϣ�������ϸ��Ϣ�б�"), Language.Msg("��ʾ"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //��ȡ����
            ArrayList alInputDetail = new ArrayList(headInfo.InputList.ToArray());

            //��ʾ����
            this.AddInputDataToFp(alInputDetail);

            class3MeaningCode = headInfo.InClass3Mean;

            List<FS.FrameWork.Models.NeuObject> tmpInputList = new List<FS.FrameWork.Models.NeuObject>();
            foreach (FS.HISFC.BizLogic.Material.Object.Input ip in headInfo.InputList)
            {
                tmpInputList.Add(ip as FS.FrameWork.Models.NeuObject);
            }
            //��ӡ
            if (this.IsPrint)
            {
                this.SetPrint("5510", class3MeaningCode, tmpInputList);
                this.IsPrint = false;
            }
        }

        private void AddInputDataToFp(ArrayList al)
        {
            foreach (FS.HISFC.BizLogic.Material.Object.Input detail in al)
            {
                this.neuSpread1_Sheet2.Rows.Add(this.neuSpread1_Sheet2.RowCount, 1);
                //���ʵ����Ϣ
                this.neuSpread1_Sheet2.Cells[this.neuSpread1_Sheet2.RowCount - 1, (int)DetailColumns.ColListNo].Value = detail.InListCode;
                this.neuSpread1_Sheet2.Cells[this.neuSpread1_Sheet2.RowCount - 1, (int)DetailColumns.ColInvoiceNo].Value = detail.InvoiceNo;
                this.neuSpread1_Sheet2.Cells[this.neuSpread1_Sheet2.RowCount - 1, (int)DetailColumns.ColItemName].Value =
                    detail.StockInfo.BaseInfo.Name + "[" + detail.StockInfo.StoreSpecs + "]";
                this.neuSpread1_Sheet2.Cells[this.neuSpread1_Sheet2.RowCount - 1, (int)DetailColumns.ColNum].Value = detail.InNum;
                this.neuSpread1_Sheet2.Cells[this.neuSpread1_Sheet2.RowCount - 1, (int)DetailColumns.ColMinUnit].Value = detail.StockInfo.MinUnit;
                this.neuSpread1_Sheet2.Cells[this.neuSpread1_Sheet2.RowCount - 1, (int)DetailColumns.ColPackQty].Value = detail.StockInfo.PackQty;
                this.neuSpread1_Sheet2.Cells[this.neuSpread1_Sheet2.RowCount - 1, (int)DetailColumns.ColSalePrice].Value = detail.InSalePrice;
                this.neuSpread1_Sheet2.Cells[this.neuSpread1_Sheet2.RowCount - 1, (int)DetailColumns.ColSaleCost].Value = detail.InSaleCost;
                this.neuSpread1_Sheet2.Cells[this.neuSpread1_Sheet2.RowCount - 1, (int)DetailColumns.ColInPrice].Value = detail.InPrice;
                this.neuSpread1_Sheet2.Cells[this.neuSpread1_Sheet2.RowCount - 1, (int)DetailColumns.ColInCost].Value = detail.InCost;
                this.neuSpread1_Sheet2.Cells[this.neuSpread1_Sheet2.RowCount - 1, (int)DetailColumns.ColValidDate].Value = detail.StockInfo.ValidDate.ToShortDateString();
                this.neuSpread1_Sheet2.Cells[this.neuSpread1_Sheet2.RowCount - 1, (int)DetailColumns.ColBatchNo].Value = detail.StockInfo.BatchNo;
                this.neuSpread1_Sheet2.Cells[this.neuSpread1_Sheet2.RowCount - 1, (int)DetailColumns.ColFactory].Value =
                       FS.HISFC.BizProcess.Material.Manager.MatHelper.GetCompanyNameByID(detail.StockInfo.Factory.ID);
                if (detail.StockInfo.IsHighValue)
                {
                    string highValueBarCode = detail.StockInfo.HighValueBarCode;
                    this.neuSpread1_Sheet2.Cells[this.neuSpread1_Sheet2.RowCount - 1, (int)DetailColumns.ColHighValueBarCode].Text = highValueBarCode;
                }
                this.neuSpread1_Sheet2.Rows[this.neuSpread1_Sheet2.RowCount - 1].Tag = detail;
            }
        }

        public void QueryOutputData()
        {
            List<FS.HISFC.BizLogic.Material.Object.OutputHead> outputHeadList = this.stockProcess.QueryOutputInfoByCondition(this.privDept.ID, this.dtBegin.Value.Date, this.dtEnd.Value.Date.AddDays(1).AddSeconds(-1), "ALL", "ALL");
            if (outputHeadList == null || outputHeadList.Count == 0)
            {
                MessageBox.Show(Language.Msg(this.stockProcess.Err + "\nδ�ܲ�ѯ�����������ĳ����¼"), Language.Msg("��ʾ"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            foreach (FS.HISFC.BizLogic.Material.Object.OutputHead head in outputHeadList)
            {
                this.AddOutputHeadData(head);
            }
        }

        private void PrintOutputBill(int activerow)
        {
            string class3MeaningCode = string.Empty;
            FS.HISFC.BizLogic.Material.Object.OutputHead outputHeadInfo = this.neuSpread1_Sheet1.ActiveRow.Tag as FS.HISFC.BizLogic.Material.Object.OutputHead;
            if (outputHeadInfo == null)
            {
                MessageBox.Show(Language.Msg("��ȡѡ�г��������Ϣ����"), Language.Msg("��ʾ"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (outputHeadInfo.OutputList == null || outputHeadInfo.OutputList.Count <= 0)
            {
                MessageBox.Show(Language.Msg("δ����ѡ�л�����Ϣ�������ϸ��Ϣ�б�"), Language.Msg("��ʾ"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            ArrayList al = new ArrayList(outputHeadInfo.OutputList.ToArray());
            this.AddOutputDataToFp(al);

            class3MeaningCode = outputHeadInfo.OutClass3Mean;

            List<FS.FrameWork.Models.NeuObject> tmpOutputList = new List<FS.FrameWork.Models.NeuObject>();
            foreach (FS.HISFC.BizLogic.Material.Object.Output op in outputHeadInfo.OutputList)
            {
                tmpOutputList.Add(op as FS.FrameWork.Models.NeuObject);
            }
            //��ӡ
            if (this.IsPrint)
            {
                this.SetPrint("5520", class3MeaningCode, tmpOutputList);
                this.IsPrint = false;
            }
        }

        private void AddOutputDataToFp(ArrayList al)
        {
            foreach (FS.HISFC.BizLogic.Material.Object.Output detail in al)
            {
                this.neuSpread1_Sheet2.Rows.Add(this.neuSpread1_Sheet2.RowCount, 1);

                this.neuSpread1_Sheet2.Cells[this.neuSpread1_Sheet2.RowCount - 1, (int)DetailColumns.ColListNo].Value = detail.OutListCode;
                this.neuSpread1_Sheet2.Cells[this.neuSpread1_Sheet2.RowCount - 1, (int)DetailColumns.ColItemName].Value =
                    detail.BaseInfo.Name + "[" + detail.StockSpecs + "]";
                this.neuSpread1_Sheet2.Cells[this.neuSpread1_Sheet2.RowCount - 1, (int)DetailColumns.ColNum].Value = detail.OutNum;
                this.neuSpread1_Sheet2.Cells[this.neuSpread1_Sheet2.RowCount - 1, (int)DetailColumns.ColMinUnit].Value = detail.MinUnit;
                this.neuSpread1_Sheet2.Cells[this.neuSpread1_Sheet2.RowCount - 1, (int)DetailColumns.ColPackQty].Value = detail.StorePackQty;
                this.neuSpread1_Sheet2.Cells[this.neuSpread1_Sheet2.RowCount - 1, (int)DetailColumns.ColSalePrice].Value = detail.OutSalePrice;
                this.neuSpread1_Sheet2.Cells[this.neuSpread1_Sheet2.RowCount - 1, (int)DetailColumns.ColSaleCost].Value = detail.OutSaleCost;
                this.neuSpread1_Sheet2.Cells[this.neuSpread1_Sheet2.RowCount - 1, (int)DetailColumns.ColInPrice].Value = detail.OutPrice;
                this.neuSpread1_Sheet2.Cells[this.neuSpread1_Sheet2.RowCount - 1, (int)DetailColumns.ColInCost].Value = detail.OutCost;
                this.neuSpread1_Sheet2.Cells[this.neuSpread1_Sheet2.RowCount - 1, (int)DetailColumns.ColValidDate].Value = detail.StockValidDate.Date;
                this.neuSpread1_Sheet2.Cells[this.neuSpread1_Sheet2.RowCount - 1, (int)DetailColumns.ColBatchNo].Value = detail.StockBatchNo;
                //{7B622589-9043-4166-9E36-EA97D1420D25}
                if (detail.IsHighValue)
                {
                    string highValueBarcode = detail.HighValueBarCode;
                    this.neuSpread1_Sheet2.Cells[this.neuSpread1_Sheet2.RowCount - 1, (int)DetailColumns.ColHighValueBarCode].Text = highValueBarcode;
                }
                this.neuSpread1_Sheet2.Rows[this.neuSpread1_Sheet2.RowCount - 1].Tag = detail;
            }
        }

        private void AddInputHeadData(FS.HISFC.BizLogic.Material.Object.InputHead inputHead)
        {
            int rowCount = this.neuSpread1_Sheet1.Rows.Count;

            this.neuSpread1_Sheet1.Rows.Add(rowCount, 1);

            this.neuSpread1_Sheet1.Cells[rowCount, (int)HeadColumns.ColListNo].Value = inputHead.ID;
            this.neuSpread1_Sheet1.Cells[rowCount, (int)HeadColumns.ColClass3Name].Value = this.hsInputPriv[inputHead.InClass3].ToString();
            this.neuSpread1_Sheet1.Cells[rowCount, (int)HeadColumns.ColTargetDept].Value = inputHead.Company.Name;
            this.neuSpread1_Sheet1.Cells[rowCount, (int)HeadColumns.ColCost].Value = inputHead.InCost;
            this.neuSpread1_Sheet1.Cells[rowCount, (int)HeadColumns.ColOperName].Value =
                    FS.HISFC.BizProcess.Material.Manager.MatHelper.GetEmployeeNameByID(inputHead.LastOper.ID);
            this.neuSpread1_Sheet1.Cells[rowCount, (int)HeadColumns.ColOperDate].Value = inputHead.LastOper.OperTime;

            this.neuSpread1_Sheet1.Cells[rowCount, (int)HeadColumns.ColOutDate].Value = inputHead.InDate;


            this.neuSpread1_Sheet1.Rows[rowCount].Tag = inputHead;
        }

        private void AddOutputHeadData(FS.HISFC.BizLogic.Material.Object.OutputHead outputHead)
        {
            int rowCount = this.neuSpread1_Sheet1.Rows.Count;

            this.neuSpread1_Sheet1.Rows.Add(rowCount, 1);

            this.neuSpread1_Sheet1.Cells[rowCount, (int)HeadColumns.ColListNo].Value = outputHead.ID;
            this.neuSpread1_Sheet1.Cells[rowCount, (int)HeadColumns.ColClass3Name].Value = this.hsOutputPriv[outputHead.OutClass3].ToString();
            this.neuSpread1_Sheet1.Cells[rowCount, (int)HeadColumns.ColTargetDept].Value =
                    FS.HISFC.BizProcess.Material.Manager.MatHelper.GetDeptNameByID(outputHead.TargetDept.ID);
            this.neuSpread1_Sheet1.Cells[rowCount, (int)HeadColumns.ColCost].Value = outputHead.OutCost;
            this.neuSpread1_Sheet1.Cells[rowCount, (int)HeadColumns.ColOperName].Value =
                    FS.HISFC.BizProcess.Material.Manager.MatHelper.GetEmployeeNameByID(outputHead.LastOper.ID);
            this.neuSpread1_Sheet1.Cells[rowCount, (int)HeadColumns.ColOperDate].Value = outputHead.LastOper.OperTime;

            this.neuSpread1_Sheet1.Cells[rowCount, (int)HeadColumns.ColOutDate].Value = outputHead.OutDate;

            this.neuSpread1_Sheet1.Rows[rowCount].Tag = outputHead;
        }

        protected override int OnSetValue(object neuObject, TreeNode e)
        {
            //�������ң��ӽ�㵥��
            if (e.Tag != null && e.Parent != null && e.Parent.Tag != null)
            {
                this.BillType = (e.Tag as FS.FrameWork.Models.NeuObject).ID;
                this.privDept = e.Parent.Tag as FS.FrameWork.Models.NeuObject;
                if (this.neuSpread1.ActiveSheetIndex != 0)
                {
                    this.neuSpread1.ActiveSheetIndex = 0;
                }
            }
            return base.OnSetValue(neuObject, e);
        }

        protected override void OnLoad(EventArgs e)
        {
            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToUpper() != "DEVENV")
            {
                this.Init();

                this.InitClass3Helper();
                
                tvPrivTree tvPriv = this.tv as tvPrivTree;

            }
            base.OnLoad(e);
        }

        private void fpSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.neuSpread1_Sheet1.RowCount == 0 || this.neuSpread1.ActiveSheetIndex == 1)
            {
                return;
            }
            this.IsPrint = false;
            this.neuSpread1_Sheet2.RowCount = 0;
            this.neuSpread1_Sheet1.ActiveRowIndex = e.Row;
            //���
            if (this.BillType == "I")
            {
                this.PrintInputBill(e.Row);
            }
            //����
            if (this.BillType == "O")
            {
                this.PrintOutputBill(e.Row);
            }
            if (this.BillType != "NO")
            {
                this.neuSpread1.ActiveSheetIndex = 1;
            }

        }

        private enum HeadColumns
        {
            /// <summary>
            ///����
            /// </summary>
            ColListNo,
            /// <summary>
            /// ��������
            /// </summary>
            ColClass3Name,
            /// <summary>
            /// Ŀ�굥λ
            /// </summary>
            ColTargetDept,
            /// <summary>
            ///  ���
            /// </summary>
            ColCost,
            /// <summary>
            /// ����Ա
            /// </summary>
            ColOperName,
            /// <summary>
            /// ����ʱ��
            /// </summary>
            ColOperDate,
            /// <summary>
            /// ����ʱ��
            /// </summary>
            ColOutDate
        }

        private enum DetailColumns
        {
            /// <summary>
            /// ����
            /// </summary>
            ColListNo,
            /// <summary>
            /// ��Ʊ��
            /// </summary>
            ColInvoiceNo,
            /// <summary>
            ///�������ƺ͹�� 
            /// </summary>
            ColItemName,
            /// <summary>
            /// ����
            /// </summary>
            ColNum,
            /// <summary>
            /// ��С��λ
            /// </summary>
            ColMinUnit,
            /// <summary>
            /// ��װ����
            /// </summary>
            ColPackQty,
            /// <summary>
            /// ���۵���
            /// </summary>
            ColSalePrice,
            /// <summary>
            /// �����ܶ�
            /// </summary>
            ColSaleCost,
            /// <summary>
            /// ����
            /// </summary>
            ColInPrice,
            /// <summary>
            /// �����ܶ�
            /// </summary>
            ColInCost,
            /// <summary>
            /// ��Ч��
            /// </summary>
            ColValidDate,
            /// <summary>
            /// ����
            /// </summary>
            ColBatchNo,
            /// <summary>
            /// ��������
            /// </summary>
            ColFactory,
            /// <summary>
            /// ��ֵ�Ĳ�������
            /// </summary>
            /// {7B622589-9043-4166-9E36-EA97D1420D25}
            ColHighValueBarCode
        }

        private void SetPrint(string class2Code,string class3MeaningCode,List<FS.FrameWork.Models.NeuObject> printList)
        {
            if (this.printProxy.SetControl(class2Code, class3MeaningCode) < 0)
            {
                MessageBox.Show(Language.Msg("��ӡʧ�ܣ�" + this.printProxy.Err), Language.Msg("��ʾ"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.printProxy.IsRePrint = true;
            int printRowCount = this.printProxy.PrintRowCount;
            if (printRowCount <= 0)
            {
                return;
            }
            this.printProxy.SetPrintDataTotal(printList);

            int totPageCount = (int)Math.Ceiling((decimal)printList.Count / (decimal)printRowCount);
            for (int i = 1; i <= totPageCount; i++)
            {
                List<FrameWork.Models.NeuObject> tmpList;
                if (i == totPageCount)
                {
                    tmpList = printList;
                }
                else
                {
                    tmpList = printList.GetRange(0, printRowCount);
                    printList.RemoveRange(0, printRowCount);
                }
                this.printProxy.SetPrintData(tmpList, i, totPageCount);
                this.printProxy.Print();
            }
        }
    }

    internal class Sort : System.Collections.IComparer
    {
        #region IComparer ��Ա

        public int Compare(object x, object y)
        {
            FS.HISFC.Models.Pharmacy.Output o1 = x as FS.HISFC.Models.Pharmacy.Output;

            FS.HISFC.Models.Pharmacy.Output o2 = y as FS.HISFC.Models.Pharmacy.Output;

            return NConvert.ToInt32(o1.ID) - NConvert.ToInt32(o2.ID);
        }

        #endregion
    }
    /// <summary>
    /// �̵㵥��ҩƷ�������
    /// </summary>
    internal class SortCheck : System.Collections.IComparer
    {
        #region IComparer ��Ա

        public int Compare(object x, object y)
        {
            FS.HISFC.Models.Pharmacy.Check ch1 = x as FS.HISFC.Models.Pharmacy.Check;

            FS.HISFC.Models.Pharmacy.Check ch2 = y as FS.HISFC.Models.Pharmacy.Check;
            string drugType1 = GetDrugCode(ch1.Item.Type.ID) + ch1.PlaceNO;
            string drugType2 = GetDrugCode(ch2.Item.Type.ID) + ch2.PlaceNO;
            return string.Compare(drugType1, drugType2);
        }
        private string GetDrugCode(string drugtype)
        {
            string drugType = drugtype;
            if (drugtype == "Z" || drugtype == "P" || drugtype == "C")
            {
                return "L";
            }
            return drugType;
        }
        #endregion
    }

}
