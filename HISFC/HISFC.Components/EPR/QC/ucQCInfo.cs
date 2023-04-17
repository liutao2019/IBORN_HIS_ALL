using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace FS.HISFC.Components.EPR.QC
{
	/// <summary>
	/// ucQCInfo ��ժҪ˵����
	/// </summary>
	public class ucQCInfo : FS.FrameWork.WinForms.Controls.ucBaseControl,FS.FrameWork.EPRControl.IUserControlable
	{
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private FarPoint.Win.Spread.FpSpread fpSpread1;
		private FarPoint.Win.Spread.SheetView fpSpread1_Sheet1;
		private System.Windows.Forms.ToolBar toolBar1;
		private System.Windows.Forms.ToolBarButton toolBarButton1;
		private System.Windows.Forms.ToolBarButton toolBarButton2;
		private System.Windows.Forms.ToolBarButton tbAdd;
		private System.Windows.Forms.ToolBarButton tbDel;
		private System.Windows.Forms.ToolBarButton tbSave;
		private System.Windows.Forms.ImageList imageList32;
		private System.ComponentModel.IContainer components;

		public ucQCInfo()
		{
			// �õ����� Windows.Forms ���������������ġ�
			InitializeComponent();

			// TODO: �� InitializeComponent ���ú�����κγ�ʼ��

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
            this.components = new System.ComponentModel.Container();
            FarPoint.Win.Spread.TipAppearance tipAppearance1 = new FarPoint.Win.Spread.TipAppearance();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucQCInfo));
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.fpSpread1 = new FarPoint.Win.Spread.FpSpread();
            this.fpSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.toolBar1 = new System.Windows.Forms.ToolBar();
            this.toolBarButton1 = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton2 = new System.Windows.Forms.ToolBarButton();
            this.tbAdd = new System.Windows.Forms.ToolBarButton();
            this.tbDel = new System.Windows.Forms.ToolBarButton();
            this.tbSave = new System.Windows.Forms.ToolBarButton();
            this.imageList32 = new System.Windows.Forms.ImageList(this.components);
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.Color.Honeydew;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(480, 383);
            this.panel1.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(488, 408);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 21);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(480, 383);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "��Ϣ";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.fpSpread1);
            this.tabPage2.Controls.Add(this.toolBar1);
            this.tabPage2.Location = new System.Drawing.Point(4, 21);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(480, 383);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "�¼�";
            // 
            // fpSpread1
            // 
            this.fpSpread1.About = "2.5.2007.2005";
            this.fpSpread1.AccessibleDescription = "";
            this.fpSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpSpread1.EditModePermanent = true;
            this.fpSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never;
            this.fpSpread1.Location = new System.Drawing.Point(0, 36);
            this.fpSpread1.Name = "fpSpread1";
            this.fpSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpSpread1_Sheet1});
            this.fpSpread1.Size = new System.Drawing.Size(480, 347);
            this.fpSpread1.TabIndex = 0;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpSpread1.TextTipAppearance = tipAppearance1;
            this.fpSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // fpSpread1_Sheet1
            // 
            this.fpSpread1_Sheet1.Reset();
            this.fpSpread1_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpSpread1_Sheet1.ColumnCount = 3;
            this.fpSpread1_Sheet1.RowCount = 5;
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "����";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "��ֵ";
            this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "��ע";
            this.fpSpread1_Sheet1.Columns.Get(0).Label = "����";
            this.fpSpread1_Sheet1.Columns.Get(0).Width = 135F;
            this.fpSpread1_Sheet1.Columns.Get(1).Label = "��ֵ";
            this.fpSpread1_Sheet1.Columns.Get(1).Width = 135F;
            this.fpSpread1_Sheet1.Columns.Get(2).Label = "��ע";
            this.fpSpread1_Sheet1.Columns.Get(2).Width = 135F;
            this.fpSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.fpSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // toolBar1
            // 
            this.toolBar1.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
            this.toolBar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.toolBarButton1,
            this.toolBarButton2,
            this.tbAdd,
            this.tbDel,
            this.tbSave});
            this.toolBar1.ButtonSize = new System.Drawing.Size(16, 16);
            this.toolBar1.DropDownArrows = true;
            this.toolBar1.ImageList = this.imageList32;
            this.toolBar1.Location = new System.Drawing.Point(0, 0);
            this.toolBar1.Name = "toolBar1";
            this.toolBar1.ShowToolTips = true;
            this.toolBar1.Size = new System.Drawing.Size(480, 36);
            this.toolBar1.TabIndex = 1;
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
            // tbAdd
            // 
            this.tbAdd.ImageIndex = 0;
            this.tbAdd.Name = "tbAdd";
            this.tbAdd.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.tbAdd.ToolTipText = "���";
            this.tbAdd.Visible = false;
            // 
            // tbDel
            // 
            this.tbDel.ImageIndex = 1;
            this.tbDel.Name = "tbDel";
            this.tbDel.ToolTipText = "ɾ��";
            this.tbDel.Visible = false;
            // 
            // tbSave
            // 
            this.tbSave.ImageIndex = 5;
            this.tbSave.Name = "tbSave";
            this.tbSave.ToolTipText = "����";
            // 
            // imageList32
            // 
            this.imageList32.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList32.ImageStream")));
            this.imageList32.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList32.Images.SetKeyName(0, "");
            this.imageList32.Images.SetKeyName(1, "");
            this.imageList32.Images.SetKeyName(2, "");
            this.imageList32.Images.SetKeyName(3, "");
            this.imageList32.Images.SetKeyName(4, "");
            this.imageList32.Images.SetKeyName(5, "");
            this.imageList32.Images.SetKeyName(6, "");
            this.imageList32.Images.SetKeyName(7, "");
            this.imageList32.Images.SetKeyName(8, "");
            this.imageList32.Images.SetKeyName(9, "");
            this.imageList32.Images.SetKeyName(10, "");
            this.imageList32.Images.SetKeyName(11, "");
            this.imageList32.Images.SetKeyName(12, "");
            this.imageList32.Images.SetKeyName(13, "");
            this.imageList32.Images.SetKeyName(14, "");
            this.imageList32.Images.SetKeyName(15, "");
            this.imageList32.Images.SetKeyName(16, "");
            this.imageList32.Images.SetKeyName(17, "");
            this.imageList32.Images.SetKeyName(18, "");
            this.imageList32.Images.SetKeyName(19, "");
            this.imageList32.Images.SetKeyName(20, "");
            this.imageList32.Images.SetKeyName(21, "");
            this.imageList32.Images.SetKeyName(22, "");
            this.imageList32.Images.SetKeyName(23, "");
            this.imageList32.Images.SetKeyName(24, "");
            this.imageList32.Images.SetKeyName(25, "");
            this.imageList32.Images.SetKeyName(26, "");
            this.imageList32.Images.SetKeyName(27, "");
            this.imageList32.Images.SetKeyName(28, "");
            this.imageList32.Images.SetKeyName(29, "");
            this.imageList32.Images.SetKeyName(30, "");
            this.imageList32.Images.SetKeyName(31, "");
            this.imageList32.Images.SetKeyName(32, "");
            this.imageList32.Images.SetKeyName(33, "");
            this.imageList32.Images.SetKeyName(34, "");
            this.imageList32.Images.SetKeyName(35, "");
            this.imageList32.Images.SetKeyName(36, "");
            this.imageList32.Images.SetKeyName(37, "");
            // 
            // ucQCInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Controls.Add(this.tabControl1);
            this.Name = "ucQCInfo";
            this.Size = new System.Drawing.Size(488, 408);
            this.Load += new System.EventHandler(this.ucQCInfo_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		private void ucQCInfo_Load(object sender, System.EventArgs e)
		{
			t.ShowAlways = true;
			t.InitialDelay =0;
			t.ReshowDelay =0;
		}

		ToolTip t = new ToolTip();
		/// <summary>
		/// ��ʾ�ʿ�����
		/// </summary>
		/// <param name="al"></param>
		public void ShowQCData(string inpatientNo)
		{
			this.panel1.Controls.Clear();
            ArrayList alConditions = FS.HISFC.BizProcess.Factory.Function.IntegrateEPR.GetQCConditionList();
			int i=0;
            ArrayList alMessage = new ArrayList();

			foreach(FS.HISFC.Models.EPR.QCConditions condition  in alConditions)
			{
                bool b = FS.HISFC.BizProcess.Factory.Function.IntegrateEPR.ExecQCInfo(inpatientNo, Common.Classes.Function.ISql, condition);
				LinkLabel label = new LinkLabel();
				label.Click+=new EventHandler(label_Click);
				label.AutoSize = true;
				label.Location = new Point(20,i*25+(i+1)*15);
				label.Visible = true;
				label.Tag = condition;
				label.Text = condition.Name;
				if(b)
				{
					label.ForeColor = Color.Red;
					label.LinkVisited = true;
                    alMessage.AddRange(condition.Acion.AlMessage);

				}
				else
				{
					label.ForeColor = Color.Blue;
					label.LinkVisited = false;
				}

				t.SetToolTip(label,condition.Memo);
				this.panel1.Controls.Add(label);
				i++;
			}

            this.ShowMessage(alMessage);
            this.ShowInput(inpatientNo);
			this.inpatient_no = inpatientNo;
        }

       
        #region  �ڲ�����
        private void ShowInput(string inpatientNo)
		{
            ArrayList al = FS.HISFC.BizProcess.Factory.Function.IntegrateEPR.GetQCInputCondition();
			this.fpSpread1_Sheet1.RowCount = 0;
			this.fpSpread1_Sheet1.RowCount  = al.Count;
			int i = 0;
			foreach(FS.FrameWork.Models.NeuObject obj in al)
			{
				this.fpSpread1_Sheet1.Cells[i,0].Text = obj.Name;
				this.fpSpread1_Sheet1.Cells[i,0].Locked = true;
				if(obj.Memo == "0") //�ı�
				{
				}
				else if(obj.Memo == "1")//��ֵ
				{
					this.fpSpread1_Sheet1.Cells[i,1].CellType = new FarPoint.Win.Spread.CellType.NumberCellType();
				}
				else if(obj.Memo == "2")//boolean
				{
					this.fpSpread1_Sheet1.Cells[i,1].CellType = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
				}
				else if(obj.Memo == "3")//datetime
				{
					this.fpSpread1_Sheet1.Cells[i,1].CellType = new FarPoint.Win.Spread.CellType.DateTimeCellType();
				}
				this.fpSpread1_Sheet1.Cells[i,1].Text = obj.User01;
				this.fpSpread1_Sheet1.Cells[i,2].Text = obj.User03;//Memo
				i++;	
			}

			al = FS.HISFC.BizProcess.Factory.Function.IntegrateEPR.GetQCInputCondition(inpatientNo);
			for(i=0;i<this.fpSpread1_Sheet1.RowCount;i++)
			{
				foreach(FS.FrameWork.Models.NeuObject obj in al)
				{
					if(obj.Name == this.fpSpread1_Sheet1.Cells[i,0].Text)
					{
						this.fpSpread1_Sheet1.Cells[i,1].Text = obj.User01;
					}
				}
			}

		}
		private string inpatient_no = "";
		private void label_Click(object sender, EventArgs e)
		{
			QC.ucQCInfoDetail uc = new ucQCInfoDetail();
			uc.QCConditions = ((Control)sender).Tag as FS.HISFC.Models.EPR.QCConditions;
			FS.FrameWork.WinForms.Classes.Function.ShowControl(uc);
		}

		#region IUserControlable ��Ա

		public bool IsPrint
		{
			get
			{
				// TODO:  ��� ucQCInfo.IsPrint getter ʵ��
				return false;
			}
			set
			{
				// TODO:  ��� ucQCInfo.IsPrint setter ʵ��
				this.Visible = !value;
			}
		}

		
		

		public void RefreshUC(object sender, string[] @params)
		{
			// TODO:  ��� ucQCInfo.RefreshUC ʵ��
            try
            {
                this.ShowQCData(@params[0]);
            }
            catch { }

		}

		

		#endregion

		private void toolBar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
			if(e.Button == this.tbSave)
			{
				this.Save();
			}
		}
		
		/// <summary>
		/// ����
		/// </summary>
		private void Save()
		{
			ArrayList al = new ArrayList();
			for(int i=0;i<this.fpSpread1_Sheet1.RowCount;i++)
			{
				FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();

				obj.ID = this.inpatient_no;
				obj.Name = this.fpSpread1_Sheet1.Cells[i,0].Text;
				obj.User01 = this.fpSpread1_Sheet1.Cells[i,1].Text;
				obj.User03 = this.fpSpread1_Sheet1.Cells[i,2].Text;
				al.Add(obj);
			}

			FS.HISFC.BizProcess.Factory.Function.BeginTransaction();

            if (FS.HISFC.BizProcess.Factory.Function.IntegrateEPR.SaveQCInputCondition(al) == -1)
			{
				FS.HISFC.BizProcess.Factory.Function.RollBack();
                MessageBox.Show(FS.HISFC.BizProcess.Factory.Function.IntegrateEPR.Err);
			}
            FS.HISFC.BizProcess.Factory.Function.Commit();
		}

        #region IUserControlable ��Ա

        public void Init(object sender, string[] @params)
        {
            // TODO:  ��� ucQCInfo.LoadUC ʵ��
            this.RefreshUC(sender, @params);
        }

        public int Save(object sender)
        {
            this.Save();
            return 0;
        }

       

        #endregion

        #region IUserControlable ��Ա

        public Control FocusedControl
        {
            get { return this.tabPage1; }
        }

        #endregion

        #region IUserControlable ��Ա


        public int Valid(object sender)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion

        #endregion

        protected override int OnSetValue(object neuObject, TreeNode e)
        {
            
            this.ShowQCData(((FS.FrameWork.Models.NeuObject)neuObject).ID);
            return 0;
        }

        private ArrayList myReadOnlyEMR = new ArrayList();
        public ArrayList ReadOnlyEMR
        {
            get
            {
                return myReadOnlyEMR;
            }
        }
        private bool bShowMessage = true;

        [Description("�Ƿ���ʾ�ʿ�������Ϣ")]
        public bool IsShowMessage
        {
            get
            {
                return this.bShowMessage;
            }
            set
            {
                this.bShowMessage = value;
            }
        }
        private void ShowMessage(ArrayList message)
        {
            //�ж��ʿ���Ϣ
            ArrayList myReadOnlyEMR = new ArrayList();
            if (this.bShowMessage == false) return;


            string strMessage = "";
            foreach (FS.FrameWork.Models.NeuObject obj in message)
            {
                if (obj.ID == "0")//��ʾ��Ϣ
                {
                    strMessage = strMessage + "\n" + obj.Name;
                }
                else if (obj.ID == "1")//����ֻ��
                {
                    myReadOnlyEMR.Add(obj.Name);
                }
            }
            if (strMessage.Trim() != "")
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show(strMessage);
            }
        }
        
    }
}
