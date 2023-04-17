using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;


namespace FS.WinForms.Report.Pharmacy
{
    /// <summary>
    /// ucPhaInPlan ��ժҪ˵����
    /// 
    /// {71DE54FE-82C4-438f-A4C1-761459692CDB}
    /// </summary>
    public class ucPhaStockplanBill : System.Windows.Forms.UserControl, FS.HISFC.BizProcess.Interface.Pharmacy.IBillPrint
    {
        /// <summary> 
        /// ����������������
        /// </summary>
        private System.ComponentModel.Container components = null;

        public ucPhaStockplanBill()
        {
            // �õ����� Windows.Forms ���������������ġ�
            InitializeComponent();

            // TODO: �� InitializeComponent ���ú�����κγ�ʼ��

            //this.sheetView1.Rows.Default.Height = 50F;

        }

        /// <summary> 
        /// ������������ʹ�õ���Դ��
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region �����������ɵĴ���
        /// <summary> 
        /// �����֧������ķ��� - ��Ҫʹ�ô���༭�� 
        /// �޸Ĵ˷��������ݡ�
        /// </summary>
        private void InitializeComponent()
        {
            FarPoint.Win.Spread.TipAppearance tipAppearance1 = new FarPoint.Win.Spread.TipAppearance();
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType3 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType4 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType1 = new FarPoint.Win.Spread.CellType.NumberCellType();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl0 = new System.Windows.Forms.Label();
            this.lb11 = new System.Windows.Forms.Label();
            this.lb36 = new System.Windows.Forms.Label();
            this.lb12 = new System.Windows.Forms.Label();
            this.fpSpread1 = new FarPoint.Win.Spread.FpSpread();
            this.sheetView1 = new FarPoint.Win.Spread.SheetView();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sheetView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.lbl0);
            this.panel1.Controls.Add(this.lb11);
            this.panel1.Controls.Add(this.lb36);
            this.panel1.Controls.Add(this.lb12);
            this.panel1.Controls.Add(this.fpSpread1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(879, 416);
            this.panel1.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(508, 45);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(169, 23);
            this.label6.TabIndex = 60;
            this.label6.Text = "������";
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(207, 45);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(140, 23);
            this.label5.TabIndex = 59;
            this.label5.Text = "Ժ��";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(359, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(169, 23);
            this.label4.TabIndex = 58;
            this.label4.Text = "�ɹ�Ա";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(508, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(190, 14);
            this.label2.TabIndex = 57;
            this.label2.Text = "�ɹ�����";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(4, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 14);
            this.label3.TabIndex = 56;
            this.label3.Text = "����";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(3, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(211, 23);
            this.label1.TabIndex = 55;
            this.label1.Text = "���ݺ�";
            // 
            // lbl0
            // 
            this.lbl0.Font = new System.Drawing.Font("����", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl0.Location = new System.Drawing.Point(262, 9);
            this.lbl0.Name = "lbl0";
            this.lbl0.Size = new System.Drawing.Size(339, 23);
            this.lbl0.TabIndex = 54;
            this.lbl0.Text = "ҩƷ�ɹ��ƻ���";
            // 
            // lb11
            // 
            this.lb11.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb11.Location = new System.Drawing.Point(691, 72);
            this.lb11.Name = "lb11";
            this.lb11.Size = new System.Drawing.Size(141, 14);
            this.lb11.TabIndex = 0;
            this.lb11.Text = "�ƻ���";
            this.lb11.Visible = false;
            this.lb11.Click += new System.EventHandler(this.lb11_Click);
            // 
            // lb36
            // 
            this.lb36.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb36.Location = new System.Drawing.Point(677, 45);
            this.lb36.Name = "lb36";
            this.lb36.Size = new System.Drawing.Size(112, 13);
            this.lb36.TabIndex = 53;
            this.lb36.Text = "ҳ��";
            // 
            // lb12
            // 
            this.lb12.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb12.Location = new System.Drawing.Point(207, 72);
            this.lb12.Name = "lb12";
            this.lb12.Size = new System.Drawing.Size(190, 14);
            this.lb12.TabIndex = 1;
            this.lb12.Text = "�ƻ�����";
            // 
            // fpSpread1
            // 
            this.fpSpread1.About = "3.0.2004.2005";
            this.fpSpread1.AccessibleDescription = "fpSpread1, Sheet1";
            this.fpSpread1.BackColor = System.Drawing.Color.Transparent;
            this.fpSpread1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fpSpread1.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never;
            this.fpSpread1.Location = new System.Drawing.Point(4, 91);
            this.fpSpread1.Name = "fpSpread1";
            this.fpSpread1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.sheetView1});
            this.fpSpread1.Size = new System.Drawing.Size(799, 191);
            this.fpSpread1.TabIndex = 49;
            this.fpSpread1.TabStripPolicy = FarPoint.Win.Spread.TabStripPolicy.Never;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpSpread1.TextTipAppearance = tipAppearance1;
            this.fpSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never;
            // 
            // sheetView1
            // 
            this.sheetView1.Reset();
            this.sheetView1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.sheetView1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.sheetView1.ColumnCount = 11;
            this.sheetView1.RowCount = 0;
            this.sheetView1.RowHeader.ColumnCount = 0;
            this.sheetView1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.SystemColors.WindowText, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, true, false, true, true, true);
            this.sheetView1.ColumnHeader.Cells.Get(0, 0).Value = "ҩƷ��";
            this.sheetView1.ColumnHeader.Cells.Get(0, 1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.sheetView1.ColumnHeader.Cells.Get(0, 1).Value = "ҩƷ����";
            this.sheetView1.ColumnHeader.Cells.Get(0, 2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.sheetView1.ColumnHeader.Cells.Get(0, 2).Value = "���";
            this.sheetView1.ColumnHeader.Cells.Get(0, 3).Value = "��λ";
            this.sheetView1.ColumnHeader.Cells.Get(0, 4).Value = "����";
            this.sheetView1.ColumnHeader.Cells.Get(0, 5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.sheetView1.ColumnHeader.Cells.Get(0, 5).Value = "��������";
            this.sheetView1.ColumnHeader.Cells.Get(0, 6).Value = "���뵥��";
            this.sheetView1.ColumnHeader.Cells.Get(0, 7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.sheetView1.ColumnHeader.Cells.Get(0, 7).Value = "���ۼ�";
            this.sheetView1.ColumnHeader.Cells.Get(0, 8).Value = "�������";
            this.sheetView1.ColumnHeader.Cells.Get(0, 9).Value = "���۽��";
            this.sheetView1.ColumnHeader.Cells.Get(0, 10).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.sheetView1.ColumnHeader.Cells.Get(0, 10).Value = "������˾";
            this.sheetView1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.sheetView1.ColumnHeader.DefaultStyle.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Bold);
            this.sheetView1.ColumnHeader.DefaultStyle.Locked = false;
            this.sheetView1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.sheetView1.ColumnHeader.Rows.Get(0).Height = 26F;
            this.sheetView1.Columns.Get(0).CellType = textCellType1;
            this.sheetView1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.sheetView1.Columns.Get(0).Label = "ҩƷ��";
            this.sheetView1.Columns.Get(0).Width = 45F;
            this.sheetView1.Columns.Get(1).BackColor = System.Drawing.Color.White;
            this.sheetView1.Columns.Get(1).CellType = textCellType2;
            this.sheetView1.Columns.Get(1).ForeColor = System.Drawing.Color.Black;
            this.sheetView1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.sheetView1.Columns.Get(1).Label = "ҩƷ����";
            this.sheetView1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.sheetView1.Columns.Get(1).Width = 160F;
            this.sheetView1.Columns.Get(2).CellType = textCellType3;
            this.sheetView1.Columns.Get(2).ForeColor = System.Drawing.Color.Black;
            this.sheetView1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.sheetView1.Columns.Get(2).Label = "���";
            this.sheetView1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.sheetView1.Columns.Get(2).Width = 68F;
            this.sheetView1.Columns.Get(5).CellType = textCellType4;
            this.sheetView1.Columns.Get(5).ForeColor = System.Drawing.Color.Black;
            this.sheetView1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.sheetView1.Columns.Get(5).Label = "��������";
            this.sheetView1.Columns.Get(5).Width = 63F;
            this.sheetView1.Columns.Get(7).CellType = numberCellType1;
            this.sheetView1.Columns.Get(7).ForeColor = System.Drawing.Color.Black;
            this.sheetView1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.sheetView1.Columns.Get(7).Label = "���ۼ�";
            this.sheetView1.Columns.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.sheetView1.Columns.Get(7).Width = 59F;
            this.sheetView1.Columns.Get(8).Label = "�������";
            this.sheetView1.Columns.Get(8).Width = 62F;
            this.sheetView1.Columns.Get(10).ForeColor = System.Drawing.Color.Black;
            this.sheetView1.Columns.Get(10).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.sheetView1.Columns.Get(10).Label = "������˾";
            this.sheetView1.Columns.Get(10).Width = 110F;
            this.sheetView1.RowHeader.Columns.Default.Resizable = false;
            this.sheetView1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.sheetView1.RowHeader.DefaultStyle.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Bold);
            this.sheetView1.RowHeader.DefaultStyle.Locked = false;
            this.sheetView1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.sheetView1.SheetCornerStyle.BackColor = System.Drawing.Color.White;
            this.sheetView1.SheetCornerStyle.Locked = false;
            this.sheetView1.SheetCornerStyle.Parent = "HeaderDefault";
            this.sheetView1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.sheetView1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.fpSpread1.SetActiveViewport(0, 1, 0);
            // 
            // ucPhaStockplanBill
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panel1);
            this.Name = "ucPhaStockplanBill";
            this.Size = new System.Drawing.Size(879, 416);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sheetView1)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        #region ����
        /// <summary>
        /// �Ƿ񲹴�
        /// </summary>
        public bool IsReprint = false;
        /// <summary>
        /// 
        /// </summary>
        private int decimals;
        /// <summary>
        /// ���ȣ�ע��farpointС��λ����
        /// </summary>
        public int Decimals
        {
            get
            {
                return this.decimals;
            }
            set
            {
                this.decimals = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private int maxrowno;
        /// <summary>
        /// ����������޸�ʱ��ע��ʵ�ʵ��ݳ��ȣ�
        /// </summary>
        public int MaxRowNo
        {
            get
            {
                return this.maxrowno;
            }
            set
            {
                this.maxrowno = value;
            }
        }
        
  
        /// <summary>
        /// ������
        /// </summary>
        private FS.HISFC.BizLogic.Manager.Department deptMgr = new FS.HISFC.BizLogic.Manager.Department();

        #region ��������ɵ�
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lb11;
        private System.Windows.Forms.Label lb12;
        #endregion

        /// <summary>
        /// ҩƷ����
        /// </summary>
        private FS.HISFC.BizLogic.Pharmacy.Item itemMgr = new FS.HISFC.BizLogic.Pharmacy.Item();

        private FS.HISFC.BizLogic.Pharmacy.Constant conMgr = new FS.HISFC.BizLogic.Pharmacy.Constant();
        private FarPoint.Win.Spread.FpSpread fpSpread1;
        private FarPoint.Win.Spread.SheetView sheetView1;
        private System.Windows.Forms.Label lb36;
        /// <summary>
        /// ��Ա��
        /// </summary>
        private FS.HISFC.BizLogic.Manager.Person psMgr = new FS.HISFC.BizLogic.Manager.Person();
        private Label lbl0;
        private Label label1;
        private Label label3;
        private Label label2;
        private Label label6;
        private Label label5;
        private Label label4;

        private FS.HISFC.BizLogic.Manager.Constant constantMgr = new FS.HISFC.BizLogic.Manager.Constant();
        #endregion

        #region ����

        #region ���ô�ӡ��ʽ

        /// <summary>
        /// ���ô�ӡ����
        /// </summary>
        /// <param name="al">����</param>
        /// <param name="pageno">ҳ��</param>
        /// <param name="operCode">����Ա</param>
        /// <param name="Kind">��ӡ���� 0.���ƻ���  1.�ɹ���   2.��ⵥ   3.���ⵥ    4.</param>
        public void SetDataForInput(ArrayList al, int pageno, string operCode, string kind)
        {
            this.panel1.Width = this.Width;
            try
            {
                ArrayList alPrint = new ArrayList();
                int icount = FS.FrameWork.Function.NConvert.ToInt32(Math.Ceiling(Convert.ToDouble(al.Count) / MaxRowNo));

                for (int i = 1; i <= icount; i++)
                {
                    if (i != icount)
                    {
                        alPrint = al.GetRange(MaxRowNo * (i - 1), MaxRowNo);
                        this.Print(alPrint, i, icount, operCode, kind);
                    }
                    else
                    {
                        int num = al.Count % MaxRowNo;
                        if (al.Count % MaxRowNo == 0)
                        {
                            num = MaxRowNo;
                        }
                        alPrint = al.GetRange(MaxRowNo * (i - 1), num);
                        this.Print(alPrint, i, icount, operCode, kind);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("��ӡ����!" + e.Message);
                return;
            }
        }

        #endregion

        #region ��ӡ������

        /// <summary>
        /// ��ӡ����
        /// </summary>
        /// <param name="al">��ӡ����</param>
        /// <param name="i">�ڼ�ҳ</param>
        /// <param name="count">��ҳ��</param>
        /// <param name="operCode">�Ƶ���</param>
        /// <param name="kind">��ӡ���� 0.���ƻ���  1.�ɹ���   2.��ⵥ   3.���ⵥ    4.</param>
        private void Print(ArrayList al, int inow, int icount, string operCode, string kind)
        {
            this.PrintStockplan(al, inow, icount, operCode);
        }
        #endregion

        #region �ɹ��ƻ�����ӡ

        /// <summary>
        /// ��ӡ����
        /// </summary>
        /// <param name="al">��ӡ����</param>
        /// <param name="i">�ڼ�ҳ</param>
        /// <param name="count">��ҳ��</param>
        /// <param name="operCode">�Ƶ���</param>
        private void PrintStockplan(ArrayList al, int inow, int icount, string operCode)
        {
            if (al.Count <= 0)
            {
                MessageBox.Show("û�д�ӡ������!");
                return;
            }
            FS.HISFC.BizLogic.Pharmacy.Constant constant = new FS.HISFC.BizLogic.Pharmacy.Constant();
            FS.HISFC.Models.Pharmacy.StockPlan stockPlan = (FS.HISFC.Models.Pharmacy.StockPlan)al[0];
            FS.HISFC.BizLogic.Manager.Constant constantMgr= new FS.HISFC.BizLogic.Manager.Constant();
            FS.HISFC.BizLogic.Manager.Department deptMgr = new FS.HISFC.BizLogic.Manager.Department();
            FS.HISFC.BizProcess.Integrate.Pharmacy itemMgr = new FS.HISFC.BizProcess.Integrate.Pharmacy();

            string strHos = constantMgr.GetHospitalName();
            string dosageID = string.Empty; //����
           

            if (stockPlan.State == "2")
            {
                this.lbl0.Text = strHos + "ҩƷ�ɹ�������";
            }
            else
            {
                this.lbl0.Text = strHos + "ҩƷ�ɹ��ƻ���";
                //label2.Visible = false;

            }

            #region label��ֵ
     
            if (this.IsReprint)
            {
                this.lbl0.Text = this.lbl0.Text + "(����)";
            }

            //DateTime sysTime = this.itemMgr.GetDateTimeFromSysDateTime();
            //this.lb11.Text = "�ƻ�����  " + inPlan.Dept+this.deptMgr.get + " �ƻ���  " + inPlan.PlanOper.ID;
            this.lb11.Text =  " �ƻ��ˣ�  " + stockPlan.Oper.Name;
            this.lb12.Text = "�ƻ����ڣ�" + this.itemMgr.GetDateTimeFromSysDateTime().ToString("yyyy-MM-dd");
            this.label2.Text = "�ɹ����ڣ�" + stockPlan.StockOper.OperTime.ToString("yyyy-MM-dd");
            this.lb36.Text = "��" + inow.ToString() + "ҳ/��" + icount.ToString() + "ҳ";
            this.label1.Text = "���ݺţ�" + stockPlan.BillNO;
            this.label3.Text = "���ң�" + deptMgr.GetDeptmentById(stockPlan.Dept.ID);
     
            #endregion

            #region farpoint��ֵ

            decimal sumNum5 = 0;
            decimal sumNum8 = 0;
            decimal sumNum9 = 0;
            this.sheetView1.RowCount = 0;

            #region{1EC17564-2FAD-4a77-97AC-4C57076888B2}
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType1 = new FarPoint.Win.Spread.CellType.NumberCellType();
            numberCellType1.DecimalPlaces = 4;
            this.sheetView1.Columns[6].CellType = numberCellType1;
            this.sheetView1.Columns[7].CellType = numberCellType1;
            this.sheetView1.Columns[8].CellType = numberCellType1;
            #endregion
            for (int i = 0; i < al.Count; i++)
             {
                this.sheetView1.AddRows(i, 1);
                FS.HISFC.Models.Pharmacy.StockPlan info = al[i] as FS.HISFC.Models.Pharmacy.StockPlan;


                this.sheetView1.Cells[i, 0].Text = info.Item.ID;// info.Item.NameCollection.UserCode;//ҩƷ�Զ�����
                this.sheetView1.Cells[i, 1].Text = info.Item.Name;//ҩƷ����
                this.sheetView1.Cells[i, 2].Text = info.Item.Specs;//���
                if (info.Item.PackQty == 0) info.Item.PackQty = 1;

                decimal count = 0, count2 = 0,count3 = 0;
                count = info.StockApproveQty / info.Item.PackQty;
                count2 = (info.StockApproveQty / info.Item.PackQty) * (info.Item.PriceCollection.PurchasePrice);
                count3 = (info.StockApproveQty / info.Item.PackQty) * (info.Item.PriceCollection.RetailPrice);
                this.sheetView1.Cells[i, 3].Text = info.Item.PackUnit;//��λ

               // FS.HISFC.BizProcess.Integrate.Pharmacy itemMgr = new FS.HISFC.BizProcess.Integrate.Pharmacy();

                

                dosageID = ((FS.HISFC.Models.Pharmacy.Item)itemMgr.GetItem(info.Item.ID)).DosageForm.ID;
               
                
                this.sheetView1.Cells[i, 4].Text = ((FS.FrameWork.Models.NeuObject)constantMgr.GetConstant("DOSAGEFORM", dosageID)).Name; //info.Item.DosageForm.Name; //����
                this.sheetView1.Cells[i, 5].Text = (info.StockApproveQty / info.Item.PackQty).ToString();     //�ƻ�����

                this.sheetView1.Cells[i, 6].Text = info.StockPrice.ToString();  //�ƻ������      // info.Item.PackUnit;//        
                this.sheetView1.Cells[i, 7].Text = info.Item.Price.ToString(); //�ο����ۼ�

               
                this.sheetView1.Cells[i, 8].Text = ((info.StockApproveQty / info.Item.PackQty) * (info.StockPrice)).ToString();//�ƻ����      
                this.sheetView1.Cells[i, 9].Text = ((info.StockApproveQty / info.Item.PackQty) * (info.Item.PriceCollection.RetailPrice)).ToString();//���۽��      
       

                //if (info.Item.Product.Company.Name != "")
                //    this.sheetView1.Cells[i, 7].Text = info.Item.Product.Company.Name;         //������˾;   
                //else
                //    this.sheetView1.Cells[i, 7].Text = "δѡ��";

                if (info.Company.Name!= "")
                    this.sheetView1.Cells[i, 10].Text = info.Company.Name;         //������˾;   
                else
                    this.sheetView1.Cells[i, 10].Text = "δѡ��";

                //if (info.Item.Product.Producer.Name != "")
                //    this.sheetView1.Cells[i, 8].Text = info.Item.Product.Producer.Name;        //��������  
                //else
                //    this.sheetView1.Cells[i, 8].Text = "δѡ��";
   
                sumNum5 = sumNum5 + count;
                sumNum8 = sumNum8 + count2;
                sumNum9 = sumNum9 + count3;

            }
            this.sheetView1.RowCount = al.Count + 1;
            this.sheetView1.Cells[al.Count, 0].Text = "�ϼ�";
            this.sheetView1.Cells[al.Count, 1].Text = "��" + al.Count + "��";//����;
            this.sheetView1.Cells[al.Count, 5].Text = sumNum5.ToString(); //���������ϼ�
            this.sheetView1.Cells[al.Count, 8].Text = sumNum8.ToString(); //������ϼ�
            this.sheetView1.Cells[al.Count, 9].Text = sumNum9.ToString(); //���۽��ϼ�

            //���
            //this.panel4.Width = this.Width - 3;
            this.fpSpread1.Width = this.panel1.Width - 10;
            this.fpSpread1.Height = (int)this.sheetView1.RowHeader.Rows[0].Height +
                (int)(this.sheetView1.Rows[0].Height * (al.Count + 1)) + 10;

            #endregion

            #region ��ӡ����

            FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();

            FS.HISFC.Components.Common.Classes.Function.GetPageSize("PhaInplan", ref p);

            p.PrintPage(5, 0, this.panel1);

            #endregion
        }

        #endregion

        #region IBillPrint ��Ա

        public int Prieview()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int Print()
        {
            return 1;
        }

        public int SetData(ArrayList alPrintData, FS.HISFC.BizProcess.Interface.Pharmacy.BillType billType)
        {
            this.maxrowno = 12;
            this.panel1.Width = this.Width;

            this.PrintGroupData(alPrintData);

            return 1;
        }

        public int SetData(ArrayList alPrintData, string privType)
        {

            return 1;
        }

        private int PrintGroupData(ArrayList alPrintData)
        {
            ArrayList alPrint = new ArrayList();
            int icount = FS.FrameWork.Function.NConvert.ToInt32(Math.Ceiling(Convert.ToDouble(alPrintData.Count) / MaxRowNo));

            for (int i = 1; i <= icount; i++)
            {

                if (i != icount)
                {
                    alPrint = alPrintData.GetRange(MaxRowNo * (i - 1), MaxRowNo);
                    this.Print(alPrint, i, icount, "", "");
                }
                else
                {
                    int num = alPrintData.Count % MaxRowNo;
                    if (alPrintData.Count % MaxRowNo == 0)
                    {
                        num = MaxRowNo;
                    }
                    alPrint = alPrintData.GetRange(MaxRowNo * (i - 1), num);
                    this.Print(alPrint, i, icount, "", "");
                }

            }
            return 1;
        }

        public int SetData(string billNO)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion

        private void lb11_Click(object sender, EventArgs e)
        {

        }

    }
}
        #endregion 