using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace FS.HISFC.Components.EPR
{ 
	/// <summary>
	/// frmSealEMR ��ժҪ˵����
	/// </summary>
	public class frmSealEMR : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ToolBar toolBar1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Splitter splitter1;
		private FarPoint.Win.Spread.FpSpread fpSpread1;
		private FarPoint.Win.Spread.SheetView fpSpread1_Sheet1;
		private System.Windows.Forms.ToolBarButton toolBarButton1;
		private System.Windows.Forms.ToolBarButton toolBarButton2;
		private System.Windows.Forms.ToolBarButton tbSeal;
		private System.Windows.Forms.ToolBarButton tbUnSeal;
		private System.Windows.Forms.ToolBarButton tbExit;
		private System.Windows.Forms.ToolBarButton toolBarButton6;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.DateTimePicker dtOutBegin;
		private System.Windows.Forms.DateTimePicker dtOutEnd;
		private FS.FrameWork.WinForms.Controls.NeuComboBox cmbDept1;
		private Common.Controls.ucQueryInpatientNo ucQueryInpatientNo1;
		private System.Windows.Forms.Button btnQuery;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown numericUpDown1;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.RadioButton chk1;
		private System.Windows.Forms.RadioButton chk2;
		private System.ComponentModel.IContainer components;

		public frmSealEMR()
		{
			//
			// Windows ���������֧���������
			//
			InitializeComponent();

			//
			// TODO: �� InitializeComponent ���ú�����κι��캯������
			//
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

		#region Windows ������������ɵĴ���
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSealEMR));
            FarPoint.Win.Spread.TipAppearance tipAppearance1 = new FarPoint.Win.Spread.TipAppearance();
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            this.toolBar1 = new System.Windows.Forms.ToolBar();
            this.toolBarButton1 = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton2 = new System.Windows.Forms.ToolBarButton();
            this.tbSeal = new System.Windows.Forms.ToolBarButton();
            this.tbUnSeal = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton6 = new System.Windows.Forms.ToolBarButton();
            this.tbExit = new System.Windows.Forms.ToolBarButton();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.ucQueryInpatientNo1 = new Common.Controls.ucQueryInpatientNo();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dtOutBegin = new System.Windows.Forms.DateTimePicker();
            this.dtOutEnd = new System.Windows.Forms.DateTimePicker();
            this.cmbDept1 = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.chk1 = new System.Windows.Forms.RadioButton();
            this.chk2 = new System.Windows.Forms.RadioButton();
            this.btnQuery = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.fpSpread1 = new FarPoint.Win.Spread.FpSpread();
            this.fpSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // toolBar1
            // 
            this.toolBar1.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
            this.toolBar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.toolBarButton1,
            this.toolBarButton2,
            this.tbSeal,
            this.tbUnSeal,
            this.toolBarButton6,
            this.tbExit});
            this.toolBar1.DropDownArrows = true;
            this.toolBar1.ImageList = this.imageList1;
            this.toolBar1.Location = new System.Drawing.Point(0, 0);
            this.toolBar1.Name = "toolBar1";
            this.toolBar1.ShowToolTips = true;
            this.toolBar1.Size = new System.Drawing.Size(752, 41);
            this.toolBar1.TabIndex = 0;
            this.toolBar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar1_ButtonClick);
            // 
            // toolBarButton1
            // 
            this.toolBarButton1.Name = "toolBarButton1";
            this.toolBarButton1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // toolBarButton2
            // 
            this.toolBarButton2.Name = "toolBarButton2";
            this.toolBarButton2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // tbSeal
            // 
            this.tbSeal.ImageIndex = 0;
            this.tbSeal.Name = "tbSeal";
            this.tbSeal.Text = "���";
            // 
            // tbUnSeal
            // 
            this.tbUnSeal.ImageIndex = 1;
            this.tbUnSeal.Name = "tbUnSeal";
            this.tbUnSeal.Text = "���";
            // 
            // toolBarButton6
            // 
            this.toolBarButton6.Name = "toolBarButton6";
            this.toolBarButton6.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // tbExit
            // 
            this.tbExit.ImageIndex = 2;
            this.tbExit.Name = "tbExit";
            this.tbExit.Text = "�˳�";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "");
            this.imageList1.Images.SetKeyName(1, "");
            this.imageList1.Images.SetKeyName(2, "");
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Window;
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 41);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(216, 605);
            this.panel1.TabIndex = 1;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(216, 605);
            this.tabControl1.TabIndex = 31;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.White;
            this.tabPage1.Controls.Add(this.ucQueryInpatientNo1);
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Controls.Add(this.chk1);
            this.tabPage1.Controls.Add(this.chk2);
            this.tabPage1.Controls.Add(this.btnQuery);
            this.tabPage1.Location = new System.Drawing.Point(4, 21);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(208, 580);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "����";
            // 
            // ucQueryInpatientNo1
            // 
            this.ucQueryInpatientNo1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ucQueryInpatientNo1.InputType = 0;
            this.ucQueryInpatientNo1.Location = new System.Drawing.Point(16, 28);
            this.ucQueryInpatientNo1.Name = "ucQueryInpatientNo1";
            this.ucQueryInpatientNo1.ShowState = Common.Controls.enuShowState.All;
            this.ucQueryInpatientNo1.Size = new System.Drawing.Size(184, 27);
            this.ucQueryInpatientNo1.TabIndex = 25;
            this.ucQueryInpatientNo1.myEvent += new Common.Controls.myEventDelegate(this.ucQueryInpatientNo1_myEvent);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.dtOutBegin);
            this.groupBox3.Controls.Add(this.dtOutEnd);
            this.groupBox3.Controls.Add(this.cmbDept1);
            this.groupBox3.Enabled = false;
            this.groupBox3.Location = new System.Drawing.Point(16, 88);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(184, 96);
            this.groupBox3.TabIndex = 28;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "����Ժʱ��";
            // 
            // dtOutBegin
            // 
            this.dtOutBegin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dtOutBegin.CustomFormat = "yyyy-MM-dd HH:mm";
            this.dtOutBegin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtOutBegin.Location = new System.Drawing.Point(8, 24);
            this.dtOutBegin.Name = "dtOutBegin";
            this.dtOutBegin.Size = new System.Drawing.Size(163, 21);
            this.dtOutBegin.TabIndex = 7;
            // 
            // dtOutEnd
            // 
            this.dtOutEnd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dtOutEnd.CustomFormat = "yyyy-MM-dd HH:mm";
            this.dtOutEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtOutEnd.Location = new System.Drawing.Point(8, 56);
            this.dtOutEnd.Name = "dtOutEnd";
            this.dtOutEnd.Size = new System.Drawing.Size(163, 21);
            this.dtOutEnd.TabIndex = 17;
            // 
            // cmbDept1
            // 
            this.cmbDept1.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbDept1.IsFlat = true;
            this.cmbDept1.IsLike = true;
            this.cmbDept1.Location = new System.Drawing.Point(8, 24);
            this.cmbDept1.Name = "cmbDept1";
            this.cmbDept1.PopForm = null;
            this.cmbDept1.ShowCustomerList = false;
            this.cmbDept1.ShowID = false;
            this.cmbDept1.Size = new System.Drawing.Size(127, 20);
            this.cmbDept1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.cmbDept1.TabIndex = 25;
            this.cmbDept1.Tag = "";
            this.cmbDept1.ToolBarUse = false;
            this.cmbDept1.Visible = false;
            // 
            // chk1
            // 
            this.chk1.Checked = true;
            this.chk1.Location = new System.Drawing.Point(0, 32);
            this.chk1.Name = "chk1";
            this.chk1.Size = new System.Drawing.Size(20, 24);
            this.chk1.TabIndex = 26;
            this.chk1.TabStop = true;
            // 
            // chk2
            // 
            this.chk2.Location = new System.Drawing.Point(0, 96);
            this.chk2.Name = "chk2";
            this.chk2.Size = new System.Drawing.Size(20, 24);
            this.chk2.TabIndex = 29;
            this.chk2.CheckedChanged += new System.EventHandler(this.chk2_CheckedChanged);
            // 
            // btnQuery
            // 
            this.btnQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQuery.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnQuery.Location = new System.Drawing.Point(120, 272);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 23);
            this.btnQuery.TabIndex = 30;
            this.btnQuery.Text = "��ѯ";
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.White;
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.btnSave);
            this.tabPage2.Controls.Add(this.numericUpDown1);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Location = new System.Drawing.Point(4, 21);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(208, 580);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "�Զ��������";
            // 
            // label2
            // 
            this.label2.ForeColor = System.Drawing.Color.Coral;
            this.label2.Location = new System.Drawing.Point(8, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(176, 40);
            this.label2.TabIndex = 3;
            this.label2.Text = "���ó�Ժ�������ϵͳ�Զ���没��";
            // 
            // btnSave
            // 
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSave.Location = new System.Drawing.Point(112, 160);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "����";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(96, 48);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(80, 21);
            this.numericUpDown1.TabIndex = 1;
            this.numericUpDown1.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "��Ժ������";
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(216, 41);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 605);
            this.splitter1.TabIndex = 2;
            this.splitter1.TabStop = false;
            // 
            // fpSpread1
            // 
            this.fpSpread1.About = "2.5.2007.2005";
            this.fpSpread1.AccessibleDescription = "fpSpread1, Sheet1, Row 0, Column 0, ";
            this.fpSpread1.BackColor = System.Drawing.Color.White;
            this.fpSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpSpread1.Location = new System.Drawing.Point(219, 41);
            this.fpSpread1.Name = "fpSpread1";
            this.fpSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpSpread1_Sheet1});
            this.fpSpread1.Size = new System.Drawing.Size(533, 605);
            this.fpSpread1.TabIndex = 3;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpSpread1.TextTipAppearance = tipAppearance1;
            this.fpSpread1.SelectionChanged += new FarPoint.Win.Spread.SelectionChangedEventHandler(this.fpSpread1_SelectionChanged);
            // 
            // fpSpread1_Sheet1
            // 
            this.fpSpread1_Sheet1.Reset();
            this.fpSpread1_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpSpread1_Sheet1.ColumnCount = 9;
            this.fpSpread1_Sheet1.RowCount = 0;
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "סԺ��ˮ��";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "סԺ��";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "����";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "�Ա�";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "����";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "����";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "��Ժ����";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "��Ժ����";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "״̬";
            this.fpSpread1_Sheet1.Columns.Get(0).Label = "סԺ��ˮ��";
            this.fpSpread1_Sheet1.Columns.Get(0).Width = 82F;
            this.fpSpread1_Sheet1.Columns.Get(1).CellType = textCellType1;
            this.fpSpread1_Sheet1.Columns.Get(1).Label = "סԺ��";
            this.fpSpread1_Sheet1.Columns.Get(1).Width = 78F;
            this.fpSpread1_Sheet1.Columns.Get(2).Label = "����";
            this.fpSpread1_Sheet1.Columns.Get(2).Width = 80F;
            this.fpSpread1_Sheet1.Columns.Get(5).Label = "����";
            this.fpSpread1_Sheet1.Columns.Get(5).Width = 88F;
            this.fpSpread1_Sheet1.Columns.Get(6).Label = "��Ժ����";
            this.fpSpread1_Sheet1.Columns.Get(6).Width = 91F;
            this.fpSpread1_Sheet1.Columns.Get(7).Label = "��Ժ����";
            this.fpSpread1_Sheet1.Columns.Get(7).Width = 79F;
            this.fpSpread1_Sheet1.Columns.Get(8).Label = "״̬";
            this.fpSpread1_Sheet1.Columns.Get(8).Width = 81F;
            this.fpSpread1_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
            this.fpSpread1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.ExtendedSelect;
            this.fpSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.fpSpread1_Sheet1.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.MultiRange;
            this.fpSpread1_Sheet1.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.fpSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.fpSpread1.SetActiveViewport(1, 0);
            // 
            // frmSealEMR
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(752, 646);
            this.Controls.Add(this.fpSpread1);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolBar1);
            this.Name = "frmSealEMR";
            this.Text = "��������ⴰ��";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		#region ��ʼ��
		/// <summary>
		/// ��ʼ��
		/// </summary>
		public void Init()
		{
			//FS.HISFC.Management.Manager.Department managerDept = new FS.HISFC.Management.Manager.Department();
			//ArrayList alDepts = managerDept.GetDeptment(FS.HISFC.Models.Base.DepartmentType.enuDepartmentType.I);	
			//this.cmbDept1.AddItems(alDepts);
			this.fpSpread1_Sheet1.DataAutoCellTypes = false;
			this.fpSpread1_Sheet1.Columns[0].Visible = false;
		}

		#endregion

		#region ����
		
		private FS.HISFC.Models.RADT.PatientInfo currentPatient = null;
		/// <summary>
		/// ѡ��
		/// </summary>
		private void ucQueryInpatientNo1_myEvent()
		{
			currentPatient = FS.HISFC.BizProcess.Factory.Function.IntegrateRADT.QueryPatientInfoByInpatientNO(this.ucQueryInpatientNo1.InpatientNo);
			this.Query();
		}

		/// <summary>
		/// ��ѯ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnQuery_Click(object sender, System.EventArgs e)
		{
			if(this.chk1.Checked)
			{
				this.Query();
			}
			else
			{
				if(this.dtOutBegin.Value >= this.dtOutEnd.Value)
				{
					MessageBox.Show("��Ժ��ʼʱ�䲻�ܴ��ڳ�Ժ����ʱ�䣡");
					return;
				}
				this.QueryOutDept(this.dtOutBegin.Value,this.dtOutEnd.Value);
			}
		}
		
		private void Query()
		{
			if(currentPatient == null)
				return;
			this.fpSpread1_Sheet1.RowCount =0;
			int i=0;
			this.fpSpread1_Sheet1.Rows.Add(i,1);
			this.fpSpread1_Sheet1.Cells[i,0].Text = currentPatient.ID;//סԺ��ˮ��
			this.fpSpread1_Sheet1.Cells[i,1].Text = currentPatient.PID.PatientNO;//סԺ��
			this.fpSpread1_Sheet1.Cells[i,2].Text = currentPatient.Name;//����
			this.fpSpread1_Sheet1.Cells[i,3].Text = currentPatient.Sex.Name;//�Ա�
			this.fpSpread1_Sheet1.Cells[i,4].Text = currentPatient.Age.ToString();//����
			this.fpSpread1_Sheet1.Cells[i,5].Text = currentPatient.PVisit.PatientLocation.Dept.Name;//���Ŀ���
			this.fpSpread1_Sheet1.Cells[i,6].Text = currentPatient.PVisit.InTime.ToString();//��Ժʱ��
			this.fpSpread1_Sheet1.Cells[i,7].Text = currentPatient.PVisit.OutTime.ToString();//��Ժʱ��
			//��ѯ����״̬
			if(FS.HISFC.BizProcess.Factory.Function.IntegrateEPR.IsSeal(currentPatient.ID))
			{
				this.fpSpread1_Sheet1.Rows[i].BackColor = Color.LightGray;
				this.fpSpread1_Sheet1.Cells[i,8].Text = "�ѷ��";
			}
			else
			{
				this.fpSpread1_Sheet1.Cells[i,8].Text = "δ���";
			}
		}

		/// <summary>
		/// ��ѯ��Ժ������Ϣ
		/// </summary>
		/// <param name="beginTime"></param>
		/// <param name="endTime"></param>
		private void QueryOutDept(DateTime beginTime,DateTime endTime)
		{
            ArrayList alPatients = FS.HISFC.BizProcess.Factory.Function.IntegrateRADT.QuereyPatientByDateAndState(beginTime, endTime, FS.HISFC.Models.Base.EnumInState.O);
            if (alPatients == null)
            {
                MessageBox.Show(FS.HISFC.BizProcess.Factory.Function.IntegrateRADT.Err);
                return;
            }
            this.myQuery(alPatients);
	
		}

		/// <summary>
		/// ��ӵ�farpoint
		/// </summary>
		/// <param name="al"></param>
		protected void myQuery(ArrayList al)
		{
			this.fpSpread1_Sheet1.RowCount = 0;
			int i=0;
			FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڲ�ѯ�����Ժ�.");
			foreach(FS.HISFC.Models.RADT.PatientInfo p in al)
			{
				this.fpSpread1_Sheet1.Rows.Add(i,1);
				this.fpSpread1_Sheet1.Cells[i,0].Text = p.ID;//סԺ��ˮ��
				this.fpSpread1_Sheet1.Cells[i,1].Text = p.PID.PatientNO;//סԺ��
				this.fpSpread1_Sheet1.Cells[i,2].Text = p.Name;//����
				this.fpSpread1_Sheet1.Cells[i,3].Text = p.Sex.Name;//�Ա�
				this.fpSpread1_Sheet1.Cells[i,4].Text = p.Age.ToString();//����
				this.fpSpread1_Sheet1.Cells[i,5].Text = p.PVisit.PatientLocation.Dept.Name;//���Ŀ���
				this.fpSpread1_Sheet1.Cells[i,6].Text = p.PVisit.InTime.ToString();//��Ժʱ��
				this.fpSpread1_Sheet1.Cells[i,7].Text = p.PVisit.OutTime.ToString();//��Ժʱ��
				//��ѯ����״̬
				if(FS.HISFC.BizProcess.Factory.Function.IntegrateEPR.IsSeal(p.ID))
				{
					this.fpSpread1_Sheet1.Rows[i].BackColor = Color.LightGray;
					this.fpSpread1_Sheet1.Cells[i,8].Text = "�ѷ��";
				}
				else
				{
					this.fpSpread1_Sheet1.Rows[i].BackColor = Color.White;
					this.fpSpread1_Sheet1.Cells[i,8].Text = "δ���";
				}
				i++;
				FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(i,al.Count );
			}
			FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
		}
		#endregion

		private void chk2_CheckedChanged(object sender, System.EventArgs e)
		{
			this.groupBox3.Enabled = this.chk2.Checked;
		}

		private void toolBar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
			if(e.Button == this.tbExit)
			{
				this.Close();
			}
			else if(e.Button == this.tbSeal)
			{
				if(this.fpSpread1_Sheet1.SelectionCount >1)
				{
					if(MessageBox.Show("ȷʵҪ���������","��ʾ",MessageBoxButtons.YesNo)==DialogResult.No)
					{
						return;
					}
				}
				else if(this.fpSpread1_Sheet1.SelectionCount == 1)
				{
					if(this.fpSpread1_Sheet1.ActiveRow.BackColor == Color.LightGray)
					{
						MessageBox.Show("�û��߲����Ѿ���棡");
						return;
					}
					if(MessageBox.Show("ȷʵҪ���"+this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex,2].Text+"�Ĳ�����?","��ʾ",MessageBoxButtons.YesNo)==DialogResult.No)
					{
						return;
					}
				}
				this.Seal();

			}
			else if(e.Button == this.tbUnSeal)
			{
				if(this.fpSpread1_Sheet1.SelectionCount >1)
				{
					MessageBox.Show("����ֻ��ѡ��һ��!");
					return;
				}
				else if(this.fpSpread1_Sheet1.SelectionCount == 1)
				{
					if(this.fpSpread1_Sheet1.ActiveRow.BackColor == Color.LightGray)
					{
						if(MessageBox.Show("ȷʵҪ���"+this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex,2].Text+"�Ĳ�����?","��ʾ",MessageBoxButtons.YesNo)==DialogResult.Yes)
						{
							this.UnSeal();
							return;
						}
						
					}
					else
					{
						MessageBox.Show("�û��߲���������������");
						return;
					}
				}
			}
		}

		/// <summary>
		/// ��没��
		/// </summary>
		private void Seal()
		{
			FS.HISFC.BizProcess.Factory.Function.BeginTransaction();
			
			
			for(int i=0;i<this.fpSpread1_Sheet1.RowCount;i++)
			{
				if(this.fpSpread1_Sheet1.IsSelected(i,0))
				{
					if(this.fpSpread1_Sheet1.Rows[i].BackColor == Color.LightGray)
					{

					}
					else
					{
						int r = FS.HISFC.BizProcess.Factory.Function.IntegrateEPR.Seal(this.fpSpread1_Sheet1.Cells[i,0].Text );
						if(r == -1 )
						{
                            FS.HISFC.BizProcess.Factory.Function.RollBack();
							MessageBox.Show(FS.HISFC.BizProcess.Factory.Function.IntegrateEPR.Err);
							return;
						}
						else if(r == 0)
						{
							this.fpSpread1_Sheet1.Rows[i].BackColor = Color.LightGreen;
							this.fpSpread1_Sheet1.Cells[i,8].Text = "�޲���";
						}
						else 
						{
							this.fpSpread1_Sheet1.Rows[i].BackColor = Color.LightGray;
							this.fpSpread1_Sheet1.Cells[i,8].Text = "�ѷ��";
						}
					}
				}
			}

            FS.HISFC.BizProcess.Factory.Function.Commit();
		}
		private void UnSeal()
		{
			FS.HISFC.BizProcess.Factory.Function.BeginTransaction();
			
			
			for(int i=0;i<this.fpSpread1_Sheet1.RowCount;i++)
			{
				if(this.fpSpread1_Sheet1.IsSelected(i,0))
				{
					if(this.fpSpread1_Sheet1.Rows[i].BackColor == Color.LightGray)
					{
						int r = FS.HISFC.BizProcess.Factory.Function.IntegrateEPR.UnSeal(this.fpSpread1_Sheet1.Cells[i,0].Text );
						if(r == -1 )
						{
                            FS.HISFC.BizProcess.Factory.Function.RollBack();
							MessageBox.Show(FS.HISFC.BizProcess.Factory.Function.IntegrateEPR.Err);
							return;
						}
						else if(r == 0)
						{
							this.fpSpread1_Sheet1.Rows[i].BackColor = Color.LightGreen;
							this.fpSpread1_Sheet1.Cells[i,8].Text = "�޲���";
						}
						else 
						{
							this.fpSpread1_Sheet1.Rows[i].BackColor = Color.White;
							this.fpSpread1_Sheet1.Cells[i,8].Text = "δ���";
						}
					}
					else
					{
						
					}
				}
			}

            FS.HISFC.BizProcess.Factory.Function.Commit();
		}
		private void fpSpread1_SelectionChanged(object sender, FarPoint.Win.Spread.SelectionChangedEventArgs e)
		{
			if(this.fpSpread1_Sheet1.SelectionCount == 1)
			{
                if (this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, 8].Text == "�ѷ��")
				{
					this.tbSeal.Enabled = false;
					this.tbUnSeal.Enabled = true;	
				}
                else if (this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, 8].Text == "δ���")
				{
					this.tbSeal.Enabled = true;
					this.tbUnSeal.Enabled = false;
				}
				else
				{
					this.tbSeal.Enabled = false;
					this.tbUnSeal.Enabled = false;
				}
			}
			else
			{
				this.tbSeal.Enabled = true;
				this.tbUnSeal.Enabled = true;
			}
		}

		

	}
}
