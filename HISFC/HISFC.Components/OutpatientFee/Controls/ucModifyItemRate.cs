using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using FarPoint.Win.Spread;

namespace FS.HISFC.Components.OutpatientFee.Controls
{
	/// <summary>
	/// ucModifyItemRate ��ժҪ˵����
	/// </summary>
	public class ucModifyItemRate : System.Windows.Forms.UserControl
    {
        private FS.FrameWork.WinForms.Controls.NeuSpread fpSpread1;
        private FarPoint.Win.Spread.SheetView fpSpread1_Sheet1;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblCost;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblRate;
        private FS.FrameWork.WinForms.Controls.NeuNumericTextBox tbCost;
        private FS.FrameWork.WinForms.Controls.NeuNumericTextBox tbPayRate;
        private FS.FrameWork.WinForms.Controls.NeuPanel neuPanel1;
        private FS.FrameWork.WinForms.Controls.NeuContextMenuStrip contextMenu1 = null;
        private FS.FrameWork.Public.ObjectHelper objHelp = new FS.FrameWork.Public.ObjectHelper();
  
        /// <summary>
        /// �Ƿ���ʾ�Ҽ��˵�
        /// </summary>
        protected bool bIsShowPopMenu = true;
        private Button btnOk;
        private Button tbCancel;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Panel panel1;
        private IContainer components;

        /// <summary>
        /// �Ƿ���ʾ�Ҽ��˵�
        /// </summary>
        public bool IsShowPopMenu
        {
            get
            {
                return this.bIsShowPopMenu;
            }
            set
            {
                this.bIsShowPopMenu = value;
            }
        }

		public ucModifyItemRate()
		{
			// �õ����� Windows.Forms ���������������ġ�
			InitializeComponent();

			// TODO: �� InitializeComponent ���ú�����κγ�ʼ��
            this.tbPayRate.Focus();
            this.tbPayRate.KeyDown += new KeyEventHandler( tbPayRate_KeyDown );
            this.tbCost.KeyDown += new KeyEventHandler( tbCost_KeyDown );

            this.fpSpread1.MouseDown += new MouseEventHandler(fpSpread1_MouseDown);
            this.fpSpread1.MouseUp += new MouseEventHandler(fpSpread1_MouseUp);
            this.contextMenu1 = new FS.FrameWork.WinForms.Controls.NeuContextMenuStrip();
		}


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void tbCost_KeyDown( object sender, KeyEventArgs e )
        {
            if (e.KeyData == Keys.Enter)
            {
                if (this.fpSpread1_Sheet1.RowCount > 0)
                {
                    for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
                    {
                        this.fpSpread1_Sheet1.SetValue( i, 3, this.tbCost.Text );
                    }
                }
                if (this.fpSpread1_Sheet1.RowCount > 0)
                {
                    this.fpSpread1.Focus();
                    int row = this.fpSpread1_Sheet1.RowCount - 1;
                    this.fpSpread1_Sheet1.SetActiveCell( row, 1, false );
                }
            }
        }

        void tbPayRate_KeyDown( object sender, KeyEventArgs e )
        {
            if (e.KeyData == Keys.Enter)
            {
                if (this.fpSpread1_Sheet1.RowCount > 0)
                {
                    for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
                    {
                        if(FS.FrameWork.Function.NConvert.ToBoolean(this.fpSpread1_Sheet1.Cells[i,5].Value) == true)
                        this.fpSpread1_Sheet1.SetValue( i, 1, this.tbPayRate.Text );
                    }
                }
                this.tbCost.Focus();
                this.tbCost.SelectAll();
            }
        }

		/// <summary> 
		/// ������������ʹ�õ���Դ��
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region �����������ɵĴ���
		/// <summary> 
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭�� 
		/// �޸Ĵ˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{
            FarPoint.Win.Spread.TipAppearance tipAppearance1 = new FarPoint.Win.Spread.TipAppearance();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType1 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType2 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType1 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            this.neuPanel1 = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.lblRate = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.tbCost = new FS.FrameWork.WinForms.Controls.NeuNumericTextBox();
            this.lblCost = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.tbPayRate = new FS.FrameWork.WinForms.Controls.NeuNumericTextBox();
            this.fpSpread1 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            this.fpSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.btnOk = new System.Windows.Forms.Button();
            this.tbCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.neuPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // neuPanel1
            // 
            this.neuPanel1.Controls.Add(this.lblRate);
            this.neuPanel1.Controls.Add(this.tbCost);
            this.neuPanel1.Controls.Add(this.lblCost);
            this.neuPanel1.Controls.Add(this.tbPayRate);
            this.neuPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.neuPanel1.Location = new System.Drawing.Point(3, 253);
            this.neuPanel1.Name = "neuPanel1";
            this.neuPanel1.Size = new System.Drawing.Size(638, 46);
            this.neuPanel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuPanel1.TabIndex = 0;
            // 
            // lblRate
            // 
            this.lblRate.AutoSize = true;
            this.lblRate.Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblRate.Location = new System.Drawing.Point(9, 13);
            this.lblRate.Name = "lblRate";
            this.lblRate.Size = new System.Drawing.Size(172, 16);
            this.lblRate.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblRate.TabIndex = 18;
            this.lblRate.Text = "��ʼ���Ը�����(F1):";
            // 
            // tbCost
            // 
            this.tbCost.AllowNegative = false;
            this.tbCost.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbCost.ForeColor = System.Drawing.Color.Blue;
            this.tbCost.IsAutoRemoveDecimalZero = false;
            this.tbCost.IsEnter2Tab = false;
            this.tbCost.Location = new System.Drawing.Point(469, 13);
            this.tbCost.Name = "tbCost";
            this.tbCost.NumericPrecision = 5;
            this.tbCost.NumericScaleOnFocus = 2;
            this.tbCost.NumericScaleOnLostFocus = 2;
            this.tbCost.NumericValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.tbCost.SetRange = new System.Drawing.Size(-1, -1);
            this.tbCost.Size = new System.Drawing.Size(140, 21);
            this.tbCost.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbCost.TabIndex = 20;
            this.tbCost.Text = "0.00";
            this.tbCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbCost.UseGroupSeperator = true;
            this.tbCost.ZeroIsValid = true;
            // 
            // lblCost
            // 
            this.lblCost.AutoSize = true;
            this.lblCost.Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCost.Location = new System.Drawing.Point(329, 14);
            this.lblCost.Name = "lblCost";
            this.lblCost.Size = new System.Drawing.Size(136, 16);
            this.lblCost.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblCost.TabIndex = 19;
            this.lblCost.Text = "��ʼ���Էѽ��:";
            // 
            // tbPayRate
            // 
            this.tbPayRate.AllowNegative = false;
            this.tbPayRate.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbPayRate.ForeColor = System.Drawing.Color.Blue;
            this.tbPayRate.IsAutoRemoveDecimalZero = false;
            this.tbPayRate.IsEnter2Tab = false;
            this.tbPayRate.Location = new System.Drawing.Point(184, 12);
            this.tbPayRate.Name = "tbPayRate";
            this.tbPayRate.NumericPrecision = 5;
            this.tbPayRate.NumericScaleOnFocus = 2;
            this.tbPayRate.NumericScaleOnLostFocus = 2;
            this.tbPayRate.NumericValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.tbPayRate.SetRange = new System.Drawing.Size(-1, -1);
            this.tbPayRate.Size = new System.Drawing.Size(140, 21);
            this.tbPayRate.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbPayRate.TabIndex = 0;
            this.tbPayRate.Text = "0.00";
            this.tbPayRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbPayRate.UseGroupSeperator = true;
            this.tbPayRate.ZeroIsValid = true;
            // 
            // fpSpread1
            // 
            this.fpSpread1.About = "3.0.2004.2005";
            this.fpSpread1.AccessibleDescription = "fpSpread1, Sheet1, Row 0, Column 0, ";
            this.fpSpread1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.fpSpread1.Dock = System.Windows.Forms.DockStyle.Top;
            this.fpSpread1.EditModeReplace = true;
            this.fpSpread1.FileName = "";
            this.fpSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpSpread1.IsAutoSaveGridStatus = false;
            this.fpSpread1.IsCanCustomConfigColumn = false;
            this.fpSpread1.Location = new System.Drawing.Point(3, 3);
            this.fpSpread1.Name = "fpSpread1";
            this.fpSpread1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpSpread1_Sheet1});
            this.fpSpread1.Size = new System.Drawing.Size(638, 250);
            this.fpSpread1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.fpSpread1.TabIndex = 10;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpSpread1.TextTipAppearance = tipAppearance1;
            this.fpSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpSpread1.ButtonClicked += new FarPoint.Win.Spread.EditorNotifyEventHandler(this.fpSpread1_ButtonClicked);
            this.fpSpread1.Leave += new System.EventHandler(this.fpSpread1_Leave);
            this.fpSpread1.EditChange += new FarPoint.Win.Spread.EditorNotifyEventHandler(this.fpSpread1_EditChange);
            this.fpSpread1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.fpSpread1_KeyDown);
            this.fpSpread1.LeaveCell += new FarPoint.Win.Spread.LeaveCellEventHandler(this.fpSpread1_LeaveCell);
            // 
            // fpSpread1_Sheet1
            // 
            this.fpSpread1_Sheet1.Reset();
            this.fpSpread1_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpSpread1_Sheet1.ColumnCount = 6;
            this.fpSpread1_Sheet1.RowCount = 10;
            this.fpSpread1_Sheet1.Cells.Get(4, 5).Value = false;
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "�շ�����(���)";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "�Ը�����";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "��������";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "�Էѽ��";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "���ʽ";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "����";
            this.fpSpread1_Sheet1.Columns.Get(0).Font = new System.Drawing.Font("����", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpSpread1_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.General;
            this.fpSpread1_Sheet1.Columns.Get(0).Label = "�շ�����(���)";
            this.fpSpread1_Sheet1.Columns.Get(0).Locked = true;
            this.fpSpread1_Sheet1.Columns.Get(0).Width = 246F;
            this.fpSpread1_Sheet1.Columns.Get(1).CellType = numberCellType1;
            this.fpSpread1_Sheet1.Columns.Get(1).Font = new System.Drawing.Font("����", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpSpread1_Sheet1.Columns.Get(1).Label = "�Ը�����";
            this.fpSpread1_Sheet1.Columns.Get(1).Width = 84F;
            this.fpSpread1_Sheet1.Columns.Get(2).CellType = textCellType1;
            this.fpSpread1_Sheet1.Columns.Get(2).Font = new System.Drawing.Font("����", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpSpread1_Sheet1.Columns.Get(2).Label = "��������";
            this.fpSpread1_Sheet1.Columns.Get(2).Locked = true;
            this.fpSpread1_Sheet1.Columns.Get(2).Width = 79F;
            this.fpSpread1_Sheet1.Columns.Get(3).CellType = numberCellType2;
            this.fpSpread1_Sheet1.Columns.Get(3).Font = new System.Drawing.Font("����", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpSpread1_Sheet1.Columns.Get(3).Label = "�Էѽ��";
            this.fpSpread1_Sheet1.Columns.Get(3).Width = 70F;
            this.fpSpread1_Sheet1.Columns.Get(4).Font = new System.Drawing.Font("����", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpSpread1_Sheet1.Columns.Get(4).Label = "���ʽ";
            this.fpSpread1_Sheet1.Columns.Get(4).Locked = true;
            this.fpSpread1_Sheet1.Columns.Get(4).Width = 82F;
            this.fpSpread1_Sheet1.Columns.Get(5).CellType = checkBoxCellType1;
            this.fpSpread1_Sheet1.Columns.Get(5).Label = "����";
            this.fpSpread1_Sheet1.Columns.Get(5).Width = 34F;
            this.fpSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.fpSpread1_Sheet1.Rows.Default.Height = 22F;
            this.fpSpread1_Sheet1.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.fpSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // btnOk
            // 
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnOk.Location = new System.Drawing.Point(472, 11);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "ȷ��(&O)";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // tbCancel
            // 
            this.tbCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.tbCancel.Location = new System.Drawing.Point(553, 11);
            this.tbCancel.Name = "tbCancel";
            this.tbCancel.Size = new System.Drawing.Size(75, 23);
            this.tbCancel.TabIndex = 1;
            this.tbCancel.Text = "ȡ��(&C)";
            this.tbCancel.Click += new System.EventHandler(this.tbCancel_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(18, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "F4";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(50, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "ȷ���޸�";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.Blue;
            this.label3.Location = new System.Drawing.Point(127, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "ESC";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(166, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 20);
            this.label4.TabIndex = 5;
            this.label4.Text = "ȡ���޸�";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.tbCancel);
            this.panel1.Controls.Add(this.btnOk);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(3, 301);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(638, 48);
            this.panel1.TabIndex = 1;
            // 
            // ucModifyItemRate
            // 
            this.Controls.Add(this.neuPanel1);
            this.Controls.Add(this.fpSpread1);
            this.Controls.Add(this.panel1);
            this.Name = "ucModifyItemRate";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Size = new System.Drawing.Size(644, 352);
            this.Load += new System.EventHandler(this.ucModifyItemRate_Load);
            this.neuPanel1.ResumeLayout(false);
            this.neuPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		#region ����

		private ArrayList feeDetails = new ArrayList();//������Ϣ
		private bool isConfirm = false;//�Ƿ��Ѿ��޸ı���
		private ArrayList relations = new ArrayList();
        private FS.HISFC.Models.Registration.Register register = new FS.HISFC.Models.Registration.Register();
        FS.FrameWork.Public.ObjectHelper apprItemHelper = new FS.FrameWork.Public.ObjectHelper();

		#endregion

		#region ����
		/// <summary>
		/// feeDetails
		/// </summary>
		public ArrayList FeeDetails
		{
			get
			{
				return feeDetails;
			}
			set
			{
				feeDetails = value;
			}
		}

        public FS.HISFC.Models.Registration.Register Register
        {
            get
            {
                return register;
            }
            set
            {
                register = value;
            }
        }
		/// <summary>
		/// �Ƿ��Ѿ��޸ı���
		/// </summary>
		public bool IsConfirm
		{
			get
			{
				return isConfirm;
			}
		}

		#endregion

		#region ����
		/// <summary>
		/// ��ʼ�������б�
		/// </summary>
		/// <param name="feeDetails">������ϸ����</param>
		public void InitFeeDetails(ArrayList feeDetails)
		{
			this.feeDetails = feeDetails;
			this.InitFeeDetails();
		}
		/// <summary>
		/// ��ʼ�������б�
		/// </summary>
		public void InitFeeDetails()
		{
            FS.HISFC.BizLogic.Manager.Constant myCons = new FS.HISFC.BizLogic.Manager.Constant();

            ArrayList alMenu = myCons.GetAllList("GFSPTYPES");
            if (alMenu == null || alMenu.Count == 0)
            {
                objHelp.ArrayObject = new ArrayList();
                //MessageBox.Show("��ά�������������ࡾTYPEΪGFSPTYPES��!");
            }
            else
            {
                objHelp.ArrayObject = alMenu;
            }
			this.fpSpread1_Sheet1.RowCount = this.feeDetails.Count;
			FS.HISFC.Models.Fee.Outpatient.FeeItemList feeDetail = new FS.HISFC.Models.Fee.Outpatient.FeeItemList();
			for(int i = 0; i < this.feeDetails.Count; i++)
			{
				feeDetail = feeDetails[i] as FS.HISFC.Models.Fee.Outpatient.FeeItemList;

                if (feeDetail.OrgItemRate == 0 && feeDetail.ItemRateFlag != "3")
                {
                    feeDetail.OrgItemRate = 1;
                }
                this.fpSpread1_Sheet1.Rows[i].Tag = feeDetail;
                this.fpSpread1_Sheet1.Cells[i, 0].Text = feeDetail.Item.Name + "[" + feeDetail.Item.Specs + "]";
                this.fpSpread1_Sheet1.Cells[i, 1].Text = (feeDetail.NewItemRate * 100).ToString();

                //��ô�޸�
                if (feeDetail.FT.User03 != string.Empty)
                {
                    this.fpSpread1_Sheet1.Cells[i, 3].Text = feeDetail.FT.User03;
                }
                else
                {
                    //this.fpSpread1_Sheet1.Cells[i, 3].Text = feeDetail.FT.TotCost.ToString();
                    this.fpSpread1_Sheet1.Cells[i, 3].Text = "0";
                }
				string tmpFlag = null;
				switch(feeDetail.ItemRateFlag)
				{
					case "1":
                        tmpFlag = "�Է�";
						break;
					case "2":
						tmpFlag = "����";
						break;
					case "3":
						tmpFlag = "����";
						break;
				}

                string tmpGF = objHelp.GetName(feeDetail.FT.FTRate.User03);
               
				this.fpSpread1_Sheet1.Cells[i,4].Text = tmpFlag;
                this.fpSpread1_Sheet1.Cells[i, 2].Text = tmpGF;


				if(feeDetail.ItemRateFlag == "3")
				{
					this.fpSpread1_Sheet1.Cells[i,5].Value = true;
				}
				else
				{
					this.fpSpread1_Sheet1.Cells[i,5].Value = false;
				}

                if (feeDetail.NewItemRate == 0)
                {
                    this.fpSpread1_Sheet1.Cells[i, 4].Text = "�Է�";
                    this.fpSpread1_Sheet1.Cells[i, 1].Text = "100";
                }
			}

		}
		/// <summary>
		/// ��ʼ����ʾ�б�
		/// </summary>
		private void InitFp()
		{
			InputMap im;
			im = this.fpSpread1.GetInputMap(InputMapMode.WhenAncestorOfFocused);
			im.Put(new Keystroke(Keys.Enter,Keys.None),FarPoint.Win.Spread.SpreadActions.None);
		
			im = fpSpread1.GetInputMap(InputMapMode.WhenAncestorOfFocused );
			im.Put(new Keystroke(Keys.Down,Keys.None),FarPoint.Win.Spread.SpreadActions.None);

			im = fpSpread1.GetInputMap(InputMapMode.WhenAncestorOfFocused );
			im.Put(new Keystroke(Keys.Up,Keys.None),FarPoint.Win.Spread.SpreadActions.None);

			im = fpSpread1.GetInputMap(InputMapMode.WhenAncestorOfFocused );
			im.Put(new Keystroke(Keys.Escape,Keys.None),FarPoint.Win.Spread.SpreadActions.None);

			im = fpSpread1.GetInputMap(InputMapMode.WhenAncestorOfFocused );
			im.Put(new Keystroke(Keys.F4,Keys.None),FarPoint.Win.Spread.SpreadActions.None);
		}
		/// <summary>
		/// ����޸ĺ����Ŀ��Ϣ
		/// </summary>
        private int GetItemList()
        {
            this.feeDetails = new ArrayList();
            FS.HISFC.Models.Fee.Outpatient.FeeItemList f = null;
            this.fpSpread1.StopCellEditing();

            #region
            /*
            for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i ++)
			{
				
				decimal newItemRate = FS.FrameWork.Function.NConvert.ToDecimal(this.fpSpread1_Sheet1.Cells[i,1].Text);
				if(newItemRate > 100 || newItemRate < 0)
				{
					MessageBox.Show("�������벻�Ϸ�!");
                    this.fpSpread1.Focus();
					this.fpSpread1_Sheet1.SetActiveCell(i, 1, false);
					return -1;
				}
				if(newItemRate == 100)
				{
					this.fpSpread1_Sheet1.Cells[i,4].Text = "�Է�";
					((FS.HISFC.Models.Fee.Outpatient.FeeItemList)this.fpSpread1_Sheet1.Rows[i].Tag).ItemRateFlag = "1";
				}
                //else if(newItemRate != ((FS.HISFC.Models.Fee.Outpatient.FeeItemList)this.fpSpread1_Sheet1.Rows[i].Tag).OrgItemRate * 100)
                //{
                //    this.fpSpread1_Sheet1.Cells[i,4].Text = "����";
                //    ((FS.HISFC.Models.Fee.Outpatient.FeeItemList)this.fpSpread1_Sheet1.Rows[i].Tag).ItemRateFlag = "3";
                //}
                else if (newItemRate != ((FS.HISFC.Models.Fee.Outpatient.FeeItemList)this.fpSpread1_Sheet1.Rows[i].Tag).OrgItemRate * 100 ||
                    FS.FrameWork.Function.NConvert.ToBoolean(this.fpSpread1_Sheet1.Cells[i, 5].Value) == true)
                {
                    this.fpSpread1_Sheet1.Cells[i, 4].Text = "����";
                    ((FS.HISFC.Models.Fee.Outpatient.FeeItemList)this.fpSpread1_Sheet1.Rows[i].Tag).ItemRateFlag = "3";
                    this.fpSpread1_Sheet1.Cells[i, 5].Value = true;
                }
				else
				{
					this.fpSpread1_Sheet1.Cells[i,4].Text = "����";
					((FS.HISFC.Models.Fee.Outpatient.FeeItemList)this.fpSpread1_Sheet1.Rows[i].Tag).ItemRateFlag = "2";
				}
				((FS.HISFC.Models.Fee.Outpatient.FeeItemList)this.fpSpread1_Sheet1.Rows[i].Tag).NewItemRate = FS.FrameWork.Public.String.FormatNumber(newItemRate / 100, 2);
				
                //if(((FS.HISFC.Models.Fee.Outpatient.FeeItemList)this.fpSpread1_Sheet1.Rows[i].Tag).ItemRateFlag != "3" && 
                //  FS.FrameWork.Function.NConvert.ToBoolean(this.fpSpread1_Sheet1.Cells[i,5].Value) == true)
                if (FS.FrameWork.Function.NConvert.ToBoolean(this.fpSpread1_Sheet1.Cells[i, 5].Value) == true)
				{
					DialogResult _Reu;

					_Reu = MessageBox.Show("��Ŀ:"+((FS.HISFC.Models.Fee.Outpatient.FeeItemList)this.fpSpread1_Sheet1.Rows[i].Tag).Name+"ȷ��Ϊ������Ŀ��?","��ʾ",MessageBoxButtons.OKCancel,MessageBoxIcon.Warning,MessageBoxDefaultButton.Button2);

					if(_Reu == DialogResult.Cancel)
					{
						this.fpSpread1_Sheet1.SetActiveCell(i, 5, false);
						return -1;
					}
					else
					{
						this.fpSpread1_Sheet1.Cells[i,4].Text = "����";
						((FS.HISFC.Models.Fee.Outpatient.FeeItemList)this.fpSpread1_Sheet1.Rows[i].Tag).ItemRateFlag = "3";
					}
				}

				decimal ownPrice = FS.FrameWork.Function.NConvert.ToDecimal(this.fpSpread1_Sheet1.Cells[i,3].Text);

                //��ô�޸� 2008-4-20
                //decimal truePrice = ((FS.HISFC.Models.Fee.Outpatient.FeeItemList)this.fpSpread1_Sheet1.Rows[i].Tag ).Item.Price;
                decimal truePrice = ( (FS.HISFC.Models.Fee.Outpatient.FeeItemList)this.fpSpread1_Sheet1.Rows[i].Tag ).FT.TotCost;

				if(truePrice < ownPrice)
				{
					//MessageBox.Show("�����Էѵ��۲��ܴ��ڷ��õ���!");
                    MessageBox.Show( "�����Էѽ��ܴ��ڷ����ܼ�!" );
					this.fpSpread1_Sheet1.SetActiveCell(i, 3, false);
					return -1;
				}
				//��ô�޸� 2008-4-20
                //( (FS.HISFC.Models.Fee.Outpatient.FeeItemList)this.fpSpread1_Sheet1.Rows[i].Tag ).SpecialPrice = ownPrice;	
                ( (FS.HISFC.Models.Fee.Outpatient.FeeItemList)this.fpSpread1_Sheet1.Rows[i].Tag ).SpecialPrice = 0m;																			 
                ( (FS.HISFC.Models.Fee.Outpatient.FeeItemList)this.fpSpread1_Sheet1.Rows[i].Tag ).FT.User03 = ownPrice.ToString();	

				f = this.fpSpread1_Sheet1.Rows[i].Tag as FS.HISFC.Models.Fee.Outpatient.FeeItemList;
				feeDetails.Add(f);

            }*/
            #endregion

            for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
            {

                decimal newItemRate = FS.FrameWork.Function.NConvert.ToDecimal(this.fpSpread1_Sheet1.Cells[i, 1].Text);
                if (newItemRate > 100 || newItemRate < 0)
                {
                    MessageBox.Show("�������벻�Ϸ�!");
                    this.fpSpread1.Focus();
                    this.fpSpread1_Sheet1.SetActiveCell(i, 1, false);
                    return -1;
                }
                decimal ownPrice = FS.FrameWork.Function.NConvert.ToDecimal(this.fpSpread1_Sheet1.Cells[i, 3].Text);
                decimal truePrice = ((FS.HISFC.Models.Fee.Outpatient.FeeItemList)this.fpSpread1_Sheet1.Rows[i].Tag).FT.TotCost;

                if (truePrice < ownPrice)
                {
                    MessageBox.Show("�����Էѽ��ܴ��ڷ����ܼ�!");
                    this.fpSpread1_Sheet1.SetActiveCell(i, 3, false);
                    return -1;
                }

                if (FS.FrameWork.Function.NConvert.ToBoolean(this.fpSpread1_Sheet1.Cells[i, 5].Value) == true)
                {
                    DialogResult _Reu;

                    _Reu = MessageBox.Show("��Ŀ:" + ((FS.HISFC.Models.Fee.Outpatient.FeeItemList)this.fpSpread1_Sheet1.Rows[i].Tag).Name + "ȷ��Ϊ������Ŀ��?", "��ʾ", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

                    if (_Reu == DialogResult.Cancel)
                    {
                        //this.fpSpread1_Sheet1.SetActiveCell(i, 5, false);
                        this.fpSpread1_Sheet1.Cells[i, 5].Value = false;
                        this.fpSpread1_Sheet1.Cells[i, 2].Text = "";
                        this.fpSpread1_Sheet1.Cells[i, 2].Tag = "";
                        this.fpSpread1_Sheet1.Cells[i, 3].Text = "0";

                        this.fpSpread1_Sheet1.Cells[i, 1].Text = (((FS.HISFC.Models.Fee.Outpatient.FeeItemList)this.fpSpread1_Sheet1.Rows[i].Tag).OrgItemRate * 100).ToString();
                        if (((FS.HISFC.Models.Fee.Outpatient.FeeItemList)this.fpSpread1_Sheet1.Rows[i].Tag).OrgItemRate == 1)
                        {
                            this.fpSpread1_Sheet1.Cells[i, 4].Text = "�Է�";
                            this.fpSpread1_Sheet1.Cells[i, 1].Text = "100";
                        }
                        else
                        {
                            this.fpSpread1_Sheet1.Cells[i, 4].Text = "����";
                        }
                        return -1;
                    }
                    else
                    {
                        this.fpSpread1_Sheet1.Cells[i, 4].Text = "����";
                        //((FS.HISFC.Models.Fee.Outpatient.FeeItemList)this.fpSpread1_Sheet1.Rows[i].Tag).ItemRateFlag = "3";
                    }
                }
            }
            for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
            {
                decimal newItemRate = FS.FrameWork.Function.NConvert.ToDecimal(this.fpSpread1_Sheet1.Cells[i, 1].Text);
                decimal ownPrice = FS.FrameWork.Function.NConvert.ToDecimal(this.fpSpread1_Sheet1.Cells[i, 3].Text);
                decimal truePrice = ((FS.HISFC.Models.Fee.Outpatient.FeeItemList)this.fpSpread1_Sheet1.Rows[i].Tag).FT.TotCost;

                if (FS.FrameWork.Function.NConvert.ToBoolean(this.fpSpread1_Sheet1.Cells[i, 5].Value) == true)
                {

                    ((FS.HISFC.Models.Fee.Outpatient.FeeItemList)this.fpSpread1_Sheet1.Rows[i].Tag).ItemRateFlag = "3";

                }
                else
                {
                    if (((FS.HISFC.Models.Fee.Outpatient.FeeItemList)this.fpSpread1_Sheet1.Rows[i].Tag).OrgItemRate == 1)
                    {
                        ((FS.HISFC.Models.Fee.Outpatient.FeeItemList)this.fpSpread1_Sheet1.Rows[i].Tag).ItemRateFlag = "1";
                    }
                    else
                    {
                        ((FS.HISFC.Models.Fee.Outpatient.FeeItemList)this.fpSpread1_Sheet1.Rows[i].Tag).ItemRateFlag = "2";
                    }
                    ((FS.HISFC.Models.Fee.Outpatient.FeeItemList)this.fpSpread1_Sheet1.Rows[i].Tag).FT.FTRate.User03 = "";
                }
                ((FS.HISFC.Models.Fee.Outpatient.FeeItemList)this.fpSpread1_Sheet1.Rows[i].Tag).NewItemRate = FS.FrameWork.Public.String.FormatNumber(newItemRate / 100, 2);

                if (this.fpSpread1_Sheet1.Cells[i, 2].Tag != null)
                {
                    ((FS.HISFC.Models.Fee.Outpatient.FeeItemList)this.fpSpread1.ActiveSheet.Rows[i].Tag).FT.FTRate.User03 = this.fpSpread1_Sheet1.Cells[i, 2].Tag.ToString();
                }
                //��ô�޸� 2008-4-20
                ((FS.HISFC.Models.Fee.Outpatient.FeeItemList)this.fpSpread1_Sheet1.Rows[i].Tag).SpecialPrice = 0m;
                ((FS.HISFC.Models.Fee.Outpatient.FeeItemList)this.fpSpread1_Sheet1.Rows[i].Tag).FT.User03 = ownPrice.ToString();

                f = this.fpSpread1_Sheet1.Rows[i].Tag as FS.HISFC.Models.Fee.Outpatient.FeeItemList;
                feeDetails.Add(f);

            }

            return 0;
        }

		#endregion

		#region �¼�

		private void tbCancel_Click(object sender, System.EventArgs e)
		{
			isConfirm = false;
			this.FindForm().Close();
		}
		private void btnOk_Click(object sender, System.EventArgs e)
		{
			isConfirm = true;
			if(GetItemList() == -1)
			{
				this.isConfirm = false;
				return;
			}
            //if(this.GetRelation() == -1)
            //{
            //    this.isConfirm = false;
            //    return;
            //}
			this.FindForm().Close();
			
		}
		private void ucModifyItemRate_Load(object sender, System.EventArgs e)
		{
			try
			{
				this.InitFp();
                //this.fpSpread1.Focus();
                //if(this.fpSpread1_Sheet1.RowCount > 0)
                //{
                //    int row = this.fpSpread1_Sheet1.RowCount -1;
                //    this.fpSpread1_Sheet1.SetActiveCell(row, 1, false);
                //}

                FS.HISFC.BizLogic.Manager.Constant myCons = new FS.HISFC.BizLogic.Manager.Constant();
				ArrayList alApprItem = myCons.GetAllList("ApprItem");
				if(alApprItem != null)
				{
					this.apprItemHelper.ArrayObject = alApprItem;
				}
                //�����ת����ʼ���Ը����
                this.neuPanel1.Focus();
                this.tbPayRate.Focus();
               // SendKeys.Send( "{F1}" );
                this.tbPayRate.SelectAll();
			}
			catch{}
		}
		protected override bool ProcessDialogKey(Keys keyData)
		{
			if(this.fpSpread1.ContainsFocus)
			{
				if(keyData == Keys.Enter)
				{
					int row = this.fpSpread1_Sheet1.ActiveRowIndex;
					int col = this.fpSpread1_Sheet1.ActiveColumnIndex;
					if(col == 1)
					{
						this.fpSpread1.StopCellEditing();
						decimal newItemRate = FS.FrameWork.Function.NConvert.ToDecimal(this.fpSpread1_Sheet1.Cells[row,1].Text);
						if(newItemRate > 100 || newItemRate < 0)
						{
							MessageBox.Show("�������벻�Ϸ�!");
                            this.fpSpread1.Focus();
							this.fpSpread1_Sheet1.SetActiveCell(row, 1, false);
							return false;
						}
                        this.fpSpread1_Sheet1.SetActiveCell(row,3,false);
					}
					if(col == 3)
					{
						col = 1;
						if(row == this.fpSpread1_Sheet1.RowCount -1)
						{
							row = 0;
						}
						else
						{
							row = row + 1;
						}
                        this.fpSpread1_Sheet1.SetActiveCell(row,col,false);
					}
                    //else
                    //{
                    //    col = col + 1;
                    //}
                    //this.fpSpread1_Sheet1.SetActiveCell(row, col, false);
				}
				if(keyData == Keys.Up)
				{
					int currRow = this.fpSpread1_Sheet1.ActiveRowIndex;
					if(currRow > 0)
					{
						this.fpSpread1_Sheet1.ActiveRowIndex = currRow -1;
						this.fpSpread1_Sheet1.SetActiveCell(currRow -1, this.fpSpread1_Sheet1.ActiveColumnIndex);
					}
				}
				if(keyData == Keys.Down)
				{
					int currRow = this.fpSpread1_Sheet1.ActiveRowIndex;
					
					this.fpSpread1_Sheet1.ActiveRowIndex = currRow + 1;
					this.fpSpread1_Sheet1.SetActiveCell(currRow + 1, this.fpSpread1_Sheet1.ActiveColumnIndex);

				}
			}
			if(keyData == Keys.F4)
			{
				this.isConfirm = true;
				if(this.GetItemList() == -1)
				{
					this.isConfirm = false;
					return false;
				}
				
                //if(this.GetRelation() == -1)
                //{
                //    this.isConfirm = false;
                //    return false;
                //}
				this.FindForm().Close();
			}
            if (keyData == Keys.F1)
            {
                this.tbPayRate.Focus();
                this.tbPayRate.SelectAll();
            }
            //if(keyData == Keys.F5)
            //{
            //    foreach(Control c in this.gb1.Controls)
            //    {
            //        if(c.Name.Substring(0,1) == "t" && c.Visible == true)
            //        {
            //            c.Focus();
            //            ((System.Windows.Forms.TextBox)c).SelectAll();
            //            break;
            //        }
            //    }
            //}
			if(keyData == Keys.Escape)
			{
				this.isConfirm = false;
				
				this.FindForm().Close();
				
			}

			
			return base.ProcessDialogKey (keyData);
		}
		private void fpSpread1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			
		}
		private void fpSpread1_Leave(object sender, System.EventArgs e)
		{
			this.fpSpread1.StopCellEditing();
		}
		private void fpSpread1_LeaveCell(object sender, FarPoint.Win.Spread.LeaveCellEventArgs e)
		{
		
		}
        private void fpSpread1_EditChange(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            int row = this.fpSpread1_Sheet1.ActiveRowIndex;
            int col = this.fpSpread1_Sheet1.ActiveColumnIndex;
            if (col == 1)
            {

                decimal newItemRate = FS.FrameWork.Function.NConvert.ToDecimal(this.fpSpread1_Sheet1.Cells[row, 1].Text);
                if (FS.FrameWork.Function.NConvert.ToBoolean(this.fpSpread1_Sheet1.Cells[row, 5].Value) == true)
                {
                    this.fpSpread1_Sheet1.Cells[row, 4].Text = "����";
                }
                else
                {
                    if (newItemRate != ((FS.HISFC.Models.Fee.Outpatient.FeeItemList)this.fpSpread1_Sheet1.Rows[row].Tag).OrgItemRate * 100)
                    {
                        if (newItemRate == 100)
                        {
                            this.fpSpread1_Sheet1.Cells[row, 4].Text = "�Է�(����)";
                        }
                        else
                        {
                            this.fpSpread1_Sheet1.Cells[row, 4].Text = "����";
                        }
                        this.fpSpread1_Sheet1.Cells[row, 5].Value = true;
                    }
                    else
                    {
                        this.fpSpread1_Sheet1.Cells[row, 4].Text = "����";
                    }
                }
            }
        }
		private void gb1_Enter(object sender, System.EventArgs e)
		{
		
		}
        /// <summary>
        /// ����Ҽ��˵�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpSpread1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            try
            {
                this.contextMenu1.Items.Clear();
            }
            catch { }
        }
        /// <summary>
        /// Ϊ�Ҽ���Ӳ˵�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpSpread1_MouseUp(object sender, MouseEventArgs e)
        {
            if (this.bIsShowPopMenu && e.Button == MouseButtons.Right)
            {
                try
                {
                    this.contextMenu1.Items.Clear();
                }
                catch { }


                #region
                FS.HISFC.BizLogic.Manager.Constant myCons = new FS.HISFC.BizLogic.Manager.Constant();

                ArrayList alMenu = myCons.GetAllList("GFSPTYPES");
                if (alMenu == null || alMenu.Count==0)
                {
                    return;
                    //MessageBox.Show("��ά�������������ࡾTYPEΪGFSPTYPES��!");
                }
                ToolStripMenuItem mnuTP = new ToolStripMenuItem("�������ൽ:");
                for (int i = 0; i < alMenu.Count; i++)
                {
                    FS.HISFC.Models.Base.Const cs = (FS.HISFC.Models.Base.Const)alMenu[i];
                    ToolStripMenuItem tpType = new ToolStripMenuItem(cs.Name);
                    tpType.Tag = cs.ID;
                    mnuTP.DropDownItems.Add(tpType);
                    tpType.Click += new EventHandler(mnuTP_Click);
                }

                this.contextMenu1.Items.Add(mnuTP);
                this.contextMenu1.Show(this.fpSpread1, new Point(e.X, e.Y));
                #endregion

                if (this.fpSpread1.ActiveSheet.RowCount <= 0)
                {
                    return;
                }

                #region ��¼��ѡ����

                string rows = "";

                for (int i = 0; i < this.fpSpread1.ActiveSheet.Rows.Count; i++)
                {
                    if (this.fpSpread1.ActiveSheet.IsSelected(i, 0))
                    {
                        rows += "$" + i.ToString() + "|";
                    }
                }
                #endregion

            }
        }
        private void mnuTP_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.fpSpread1.ActiveSheet.Rows.Count; i++)
            {
                if (this.fpSpread1.ActiveSheet.IsSelected(i, 0))
                {
                    //((FS.HISFC.Models.Fee.Outpatient.FeeItemList)this.fpSpread1.ActiveSheet.Rows[i].Tag).FT.FTRate.User03 = ((ToolStripMenuItem)sender).Tag.ToString();
                    this.fpSpread1_Sheet1.Cells[i, 2].Tag = ((ToolStripMenuItem)sender).Tag.ToString();
                    this.fpSpread1_Sheet1.Cells[i, 2].Text = ((ToolStripMenuItem)sender).Text;
                    this.fpSpread1_Sheet1.Cells[i, 4].Text = "����";
                    this.fpSpread1_Sheet1.Cells[i, 5].Value = true;
                }
            }
        }
        private void fpSpread1_ButtonClicked(object sender, EditorNotifyEventArgs e)
        {
            if (e.Column == 5)
            {
                if (FS.FrameWork.Function.NConvert.ToBoolean(this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRow.Index, 5].Value) == false)
                {
                    int i = 0;
                    i = this.fpSpread1_Sheet1.ActiveRowIndex;
                    this.fpSpread1_Sheet1.Cells[i, 5].Value = false;
                    this.fpSpread1_Sheet1.Cells[i, 3].Text = "0";

                    this.fpSpread1_Sheet1.Cells[i, 1].Text = (((FS.HISFC.Models.Fee.Outpatient.FeeItemList)this.fpSpread1_Sheet1.Rows[i].Tag).OrgItemRate * 100).ToString();
                    if (((FS.HISFC.Models.Fee.Outpatient.FeeItemList)this.fpSpread1_Sheet1.Rows[i].Tag).OrgItemRate == 1)
                    {
                        this.fpSpread1_Sheet1.Cells[i, 4].Text = "�Է�";
                    }
                    else
                    {
                        this.fpSpread1_Sheet1.Cells[i, 4].Text = "����";
                    }

                    if (((FS.HISFC.Models.Fee.Outpatient.FeeItemList)this.fpSpread1_Sheet1.Rows[i].Tag).NewItemRate == 0)
                    {
                        this.fpSpread1_Sheet1.Cells[i, 4].Text = "�Է�";
                        this.fpSpread1_Sheet1.Cells[i, 1].Text = "100";
                    }

                    this.fpSpread1_Sheet1.Cells[i, 2].Tag = "";
                    this.fpSpread1_Sheet1.Cells[i, 2].Text = "";
                }
            }
        }

        #endregion
		
	}
}
