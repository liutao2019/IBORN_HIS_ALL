using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;


namespace FS.WinForms.Report.Pharmacy
{
	/// <summary>
	/// ucPhaOuput ��ժҪ˵����
	/// </summary>
    public class ucPhaOutputBill : System.Windows.Forms.UserControl, FS.HISFC.BizProcess.Interface.Pharmacy.IBillPrint
	{
		/// <summary> 
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;

        public ucPhaOutputBill()
		{
			// �õ����� Windows.Forms ���������������ġ�
			InitializeComponent();

			// TODO: �� InitializeComponent ���ú�����κγ�ʼ��

			//this.sheetView1.Rows.Default.Height = 50F;

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
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType3 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType1 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType2 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType4 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType3 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType4 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType5 = new FarPoint.Win.Spread.CellType.TextCellType();
            this.panel1 = new System.Windows.Forms.Panel();
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
            // lbl0
            // 
            this.lbl0.Font = new System.Drawing.Font("����", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl0.Location = new System.Drawing.Point(344, 5);
            this.lbl0.Name = "lbl0";
            this.lbl0.Size = new System.Drawing.Size(240, 23);
            this.lbl0.TabIndex = 54;
            this.lbl0.Text = "ҩƷ���ⱨ�浥";
            // 
            // lb11
            // 
            this.lb11.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb11.Location = new System.Drawing.Point(3, 47);
            this.lb11.Name = "lb11";
            this.lb11.Size = new System.Drawing.Size(153, 16);
            this.lb11.TabIndex = 0;
            this.lb11.Text = "���ò���";
            // 
            // lb36
            // 
            this.lb36.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb36.Location = new System.Drawing.Point(657, 42);
            this.lb36.Name = "lb36";
            this.lb36.Size = new System.Drawing.Size(112, 21);
            this.lb36.TabIndex = 53;
            this.lb36.Text = "ҳ��";
            // 
            // lb12
            // 
            this.lb12.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb12.Location = new System.Drawing.Point(280, 47);
            this.lb12.Name = "lb12";
            this.lb12.Size = new System.Drawing.Size(232, 16);
            this.lb12.TabIndex = 1;
            this.lb12.Text = "��������";
            // 
            // fpSpread1
            // 
            this.fpSpread1.About = "2.5.2007.2005";
            this.fpSpread1.AccessibleDescription = "fpSpread1, Sheet1";
            this.fpSpread1.BackColor = System.Drawing.Color.Transparent;
            this.fpSpread1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fpSpread1.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never;
            this.fpSpread1.Location = new System.Drawing.Point(5, 83);
            this.fpSpread1.Name = "fpSpread1";
            this.fpSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.sheetView1});
            this.fpSpread1.Size = new System.Drawing.Size(764, 191);
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
            this.sheetView1.ColumnCount = 12;
            this.sheetView1.RowCount = 0;
            this.sheetView1.RowHeader.ColumnCount = 0;
            this.sheetView1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.SystemColors.WindowText, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, true, false, true, true, true);
            this.sheetView1.ColumnHeader.Cells.Get(0, 0).Value = "ҩƷ��";
            this.sheetView1.ColumnHeader.Cells.Get(0, 1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.sheetView1.ColumnHeader.Cells.Get(0, 1).Value = "ҩƷ����";
            this.sheetView1.ColumnHeader.Cells.Get(0, 2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.sheetView1.ColumnHeader.Cells.Get(0, 2).Value = "���";
            this.sheetView1.ColumnHeader.Cells.Get(0, 3).Value = "��װ����";
            this.sheetView1.ColumnHeader.Cells.Get(0, 4).Value = "��λ";
            this.sheetView1.ColumnHeader.Cells.Get(0, 5).Value = "ҩ����";
            this.sheetView1.ColumnHeader.Cells.Get(0, 6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.sheetView1.ColumnHeader.Cells.Get(0, 6).Value = "ҩ������";
            this.sheetView1.ColumnHeader.Cells.Get(0, 7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.sheetView1.ColumnHeader.Cells.Get(0, 7).Value = "��λ";
            this.sheetView1.ColumnHeader.Cells.Get(0, 8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.sheetView1.ColumnHeader.Cells.Get(0, 8).Value = "���ۼ�";
            this.sheetView1.ColumnHeader.Cells.Get(0, 9).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.sheetView1.ColumnHeader.Cells.Get(0, 9).Value = "�����ܶ�";
            this.sheetView1.ColumnHeader.Cells.Get(0, 10).Value = "����";
            this.sheetView1.ColumnHeader.Cells.Get(0, 11).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.sheetView1.ColumnHeader.Cells.Get(0, 11).Value = "ʧЧ��";
            this.sheetView1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.sheetView1.ColumnHeader.DefaultStyle.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Bold);
            this.sheetView1.ColumnHeader.DefaultStyle.Locked = false;
            this.sheetView1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.sheetView1.ColumnHeader.Rows.Get(0).Height = 26F;
            this.sheetView1.Columns.Get(0).CellType = textCellType1;
            this.sheetView1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.sheetView1.Columns.Get(0).Label = "ҩƷ��";
            this.sheetView1.Columns.Get(0).Width = 45F;
            this.sheetView1.Columns.Get(1).BackColor = System.Drawing.Color.White;
            this.sheetView1.Columns.Get(1).CellType = textCellType2;
            this.sheetView1.Columns.Get(1).ForeColor = System.Drawing.Color.Black;
            this.sheetView1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.sheetView1.Columns.Get(1).Label = "ҩƷ����";
            this.sheetView1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.sheetView1.Columns.Get(1).Width = 150F;
            this.sheetView1.Columns.Get(2).CellType = textCellType3;
            this.sheetView1.Columns.Get(2).ForeColor = System.Drawing.Color.Black;
            this.sheetView1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.sheetView1.Columns.Get(2).Label = "���";
            this.sheetView1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.sheetView1.Columns.Get(2).Width = 51F;
            this.sheetView1.Columns.Get(3).Label = "��װ����";
            this.sheetView1.Columns.Get(3).Width = 58F;
            this.sheetView1.Columns.Get(4).Label = "��λ";
            this.sheetView1.Columns.Get(4).Width = 34F;
            this.sheetView1.Columns.Get(5).CellType = numberCellType1;
            this.sheetView1.Columns.Get(5).Label = "ҩ����";
            this.sheetView1.Columns.Get(5).Width = 55F;
            numberCellType2.DecimalPlaces = 3;
            numberCellType2.MaximumValue = 9999999.9;
            numberCellType2.MinimumValue = -9999999.9;
            this.sheetView1.Columns.Get(6).CellType = numberCellType2;
            this.sheetView1.Columns.Get(6).ForeColor = System.Drawing.Color.Black;
            this.sheetView1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.sheetView1.Columns.Get(6).Label = "ҩ������";
            this.sheetView1.Columns.Get(6).Width = 66F;
            this.sheetView1.Columns.Get(7).CellType = textCellType4;
            this.sheetView1.Columns.Get(7).ForeColor = System.Drawing.Color.Black;
            this.sheetView1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.sheetView1.Columns.Get(7).Label = "��λ";
            this.sheetView1.Columns.Get(7).Width = 35F;
            this.sheetView1.Columns.Get(8).CellType = numberCellType3;
            this.sheetView1.Columns.Get(8).ForeColor = System.Drawing.Color.Black;
            this.sheetView1.Columns.Get(8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.sheetView1.Columns.Get(8).Label = "���ۼ�";
            this.sheetView1.Columns.Get(8).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.sheetView1.Columns.Get(8).Width = 55F;
            this.sheetView1.Columns.Get(9).CellType = numberCellType4;
            this.sheetView1.Columns.Get(9).ForeColor = System.Drawing.Color.Black;
            this.sheetView1.Columns.Get(9).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.sheetView1.Columns.Get(9).Label = "�����ܶ�";
            this.sheetView1.Columns.Get(9).Width = 86F;
            this.sheetView1.Columns.Get(10).Label = "����";
            this.sheetView1.Columns.Get(10).Width = 50F;
            this.sheetView1.Columns.Get(11).CellType = textCellType5;
            this.sheetView1.Columns.Get(11).ForeColor = System.Drawing.Color.Black;
            this.sheetView1.Columns.Get(11).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.sheetView1.Columns.Get(11).Label = "ʧЧ��";
            this.sheetView1.Columns.Get(11).Width = 70F;
            this.sheetView1.RowHeader.Columns.Default.Resizable = false;
            this.sheetView1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.sheetView1.RowHeader.DefaultStyle.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Bold);
            this.sheetView1.RowHeader.DefaultStyle.Locked = false;
            this.sheetView1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.sheetView1.SheetCornerStyle.BackColor = System.Drawing.Color.White;
            this.sheetView1.SheetCornerStyle.Locked = false;
            this.sheetView1.SheetCornerStyle.Parent = "HeaderDefault";
            this.sheetView1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.fpSpread1.SetActiveViewport(1, 0);
            // 
            // ucPhaOutputBill
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panel1);
            this.Name = "ucPhaOutputBill";
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
		public void SetDataForInput(ArrayList al ,int pageno,string operCode,string kind)
		{
			this.panel1.Width = this.Width;
			try
			{
				ArrayList alPrint = new ArrayList();
                int icount = FS.FrameWork.Function.NConvert.ToInt32(Math.Ceiling(Convert.ToDouble(al.Count) / MaxRowNo));

				for(int i = 1 ; i <= icount ; i++ )
				{
					if(i != icount )
					{
						alPrint = al.GetRange( MaxRowNo*(i-1),MaxRowNo );
						this.Print( alPrint , i , icount ,operCode,kind);
					}
					else
					{
						int num = al.Count%MaxRowNo;
						if(al.Count%MaxRowNo == 0)
						{
							num = MaxRowNo;
						}
						alPrint = al.GetRange( MaxRowNo*(i-1),num );
						this.Print(alPrint , i , icount,operCode,kind);
					}
				}
			}
			catch(Exception e)
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
		private void Print(ArrayList al, int inow,int icount,string operCode,string kind)
		{
               
           
           this.PrintOutput(al , inow , icount , operCode);
            
		}
		#endregion

		#region ���ⵥ��ӡ
		/// <summary>
		/// ��ӡ����
		/// </summary>
		/// <param name="al">��ӡ����</param>
		/// <param name="i">�ڼ�ҳ</param>
		/// <param name="count">��ҳ��</param>
		/// <param name="operCode">�Ƶ���</param>
		private void PrintOutput(ArrayList al, int inow,int icount,string operCode)
		{
			if( al.Count <= 0 )
			{
				MessageBox.Show("û�д�ӡ������!");
				return;
			}
            FS.HISFC.BizLogic.Pharmacy.Constant constant = new FS.HISFC.BizLogic.Pharmacy.Constant();
            FS.HISFC.Models.Pharmacy.Output info = (FS.HISFC.Models.Pharmacy.Output)al[0];
			
			#region label��ֵ
            //this.lbTitle.Text = this.deptMgr.GetDeptmentById(info.Dept.ID) + "ҩƷ���ⵥ" ;
			if(this.IsReprint)
			{
                this.lbl0.Text = this.lbl0.Text + "(����)"; 
			}
			string strCompany = "";
			try
			{
				strCompany = this.deptMgr.GetDeptmentById(info.TargetDept.ID).Name;
			}
			catch{}

            //���ò�����ʾΪ�������� {67D1E3D6-A457-4FC4-AE45-B71CA8C1DA88}
            //this.lb11.Text = "���ò���:" + "(" + info.TargetDept.ID + ")" + strCompany;
            this.lb11.Text = "���ò���:" + "(" + this.deptMgr.GetDeptmentById(info.TargetDept.ID) + ")" + strCompany;

			this.lb12.Text = "��������:" + info.Operation.ExamOper.OperTime.ToString("yyyy-MM-dd");
			//this.lb21.Text = "�����ֿ�:" + this.deptMgr.GetDeptmentById(info.Dept.ID); 
			this.lb36.Text = "��" + inow.ToString() + "ҳ/��" + icount.ToString() + "ҳ" ;
			#endregion

			#region farpoint��ֵ
            decimal sumNum6 = 0;
            decimal sumNum8 = 0;
            this.sheetView1.RowCount = 0;
            for (int i = 0; i < al.Count; i++)
            {
                this.sheetView1.AddRows(i, 1);
                FS.HISFC.Models.Pharmacy.Output output = al[i] as FS.HISFC.Models.Pharmacy.Output;
                this.sheetView1.Cells[i, 0].Text = this.itemMgr.GetItem(output.Item.ID).NameCollection.UserCode;//ҩƷ�Զ�����
                this.sheetView1.Cells[i, 1].Text = output.Item.Name;//ҩƷ����
                this.sheetView1.Cells[i, 2].Text = output.Item.Specs;//���

                if (output.Item.PackQty == 0) output.Item.PackQty = 1;
                decimal count = 0, count2 = 0;
                //count = Math.Round(output.Operation.ExamQty / output.Item.PackQty, 4);
                count = output.Operation.ExamQty / output.Item.PackQty;
                //count2 = System.Math.Round((output.Item.PriceCollection.RetailPrice) * count, decimals);
                count2 = (output.Item.PriceCollection.RetailPrice) * count;
                this.sheetView1.Cells[i, 3].Text = output.Item.PackQty.ToString();//��װ����

                this.sheetView1.Cells[i, 4].Text = output.Item.MinUnit;//��С��λ
                this.sheetView1.Cells[i, 5].Text = (output.Item.PriceCollection.RetailPrice / output.Item.PackQty).ToString();//ҩ����


                this.sheetView1.Cells[i, 6].Text = count.ToString();//ʵ������   
                this.sheetView1.Cells[i, 7].Text = output.Item.PackUnit;//��λ
                this.sheetView1.Cells[i, 8].Text = (output.Item.PriceCollection.RetailPrice).ToString();//���ۼ�
                //this.sheetView1.Cells[i, 7].Text = System.Math.Round(output.Item.PriceCollection.RetailPrice, decimals).ToString();//���ۼ�
                this.sheetView1.Cells[i, 9].Text = ((output.Item.PriceCollection.RetailPrice) * count).ToString();//�����ܽ��
                //this.sheetView1.Cells[i, 8].Text = (System.Math.Round(output.Item.PriceCollection.RetailPrice, decimals) * count2).ToString();//�����ܽ��
                this.sheetView1.Cells[i,11].Text = output.ValidTime.ToString("yyyy-MM-dd") + "  ";
                this.sheetView1.Cells[i, 10].Text = output.BatchNO + "  ";
                sumNum6 = sumNum6 + count;
                sumNum8 = sumNum8 + count2;

            }
            this.sheetView1.RowCount = al.Count + 1;
            this.sheetView1.Cells[al.Count, 0].Text = "�ϼ�";
            this.sheetView1.Cells[al.Count, 1].Text = "��" + al.Count + "��";//����;
            this.sheetView1.Cells[al.Count, 6].Text = sumNum6.ToString();
            this.sheetView1.Cells[al.Count, 9].Text = sumNum8.ToString();

            //���
            //this.panel4.Width = this.Width - 3;
            this.fpSpread1.Width = this.panel1.Width - 10;
            this.fpSpread1.Height = (int)this.sheetView1.RowHeader.Rows[0].Height +
                (int)(this.sheetView1.Rows[0].Height * (al.Count + 1)) + 10;
            #endregion

       
     
            #endregion

            #region ��ӡ����
            //FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
            ////p.IsDataAutoExtend = false;//p.ShowPageSetup();
            //FS.HISFC.Models.Base.PageSize page = new FS.HISFC.Models.Base.PageSize();
            ////page.Height = 532;
            ////page.Width = 798;
            //page.Name = "PhaOutput";
            //p.SetPageSize(page);
            ////p.PrintPage(5,1,this);
            //p.PrintPreview(5, 1, this);


            FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();

            FS.HISFC.Components.Common.Classes.Function.GetPageSize("PhaOutput", ref p);       

            p.PrintPage(5, 0, this.panel1);

            #endregion
		}

       

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
            return 1;
        }

        public int SetData(ArrayList alPrintData, string privType)
        {
            this.maxrowno = 12;
            //this.panel1.Width = this.Width;
           

            ArrayList a0 = new ArrayList();
            ArrayList a1 = new ArrayList();
            ArrayList a2 = new ArrayList();
            ArrayList a3 = new ArrayList();
            ArrayList a4 = new ArrayList();
            ArrayList a5 = new ArrayList();

            #region �����Զ��������з���

            //foreach (FS.HISFC.Models.Pharmacy.Output output in alPrintData)
            //{
            //    //string code = (this.itemMgr.GetItem(output.Item.ID).NameCollection.UserCode);
            // try{
            //     string code = this.itemMgr.GetItem(output.Item.ID).NameCollection.UserCode;
            //    //string code = output.Item.NameCollection.UserCode;
            //    //string code1 = this.itemMgr.GetItem(output.Item.ID).UserCode;
            //    string firstLetter = code.Substring(0, 1);
            //    if (firstLetter == "1" || firstLetter == "2" || firstLetter == "3")
            //    {
            //        a0.Add(output);
            //    }
            //    else if (firstLetter == "0")
            //    {
            //        a1.Add(output);
            //    }
            //    else if (firstLetter == "4")
            //    {
            //        a2.Add(output);
            //    }
            //    else if (firstLetter == "5")
            //    {
            //        a3.Add(output);
            //    }
            //    else if (firstLetter == "8")
            //    {
            //        a4.Add(output);
            //    }
            //    else if (firstLetter == "9")
            //    {
            //        a5.Add(output);
            //    }
            // }                  
            //catch (Exception e)
            //{
            //    MessageBox.Show("����!" + e.Message);
            //}
            //}

            #endregion

            #region ���ô�ӡ���� ��ɴ�ӡ����
            int inow = 0, icount = 0; string operCode = ",";
            try
            {
                //�޸ĳ��ⵥ��ӡ��ʽ��ȡ�����Զ���������� by Sunjh 2010-9-19 {5422B85D-AB24-49f9-B5E8-DD25BFEBE82E}
                if (alPrintData.Count != 0)
                {
                    FS.HISFC.Models.Pharmacy.Output info = (FS.HISFC.Models.Pharmacy.Output)alPrintData[0];
                    this.lbl0.Text = this.deptMgr.GetDeptmentById(info.StockDept.ID) + "ҩƷ���ⵥ";
                    this.PrintGroupData(alPrintData);
                }

                //if (a0.Count != 0)
                //{
                //    this.lbl0.Text = "��ҩ����ⱨ�浥";
                //    this.PrintGroupData(a0);                           

                //}
                //if (a1.Count != 0)
                //{
                //    this.lbl0.Text = "Һ�����ⱨ�浥"; 
                //    this.PrintGroupData(a1);         
                //}
                //if (a2.Count != 0)
                //{
                //    this.lbl0.Text = "����ҩ����ⱨ�浥"; 
                //    this.PrintGroupData(a2);         
                //}
                //if (a3.Count != 0)
                //{
                //    this.lbl0.Text = "����ҩ����ⱨ�浥";
                //    this.PrintGroupData(a3);              
                //}
                //if (a4.Count != 0)
                //{
                //    this.lbl0.Text = "��ҩ����ⱨ�浥";
                //    this.PrintGroupData(a4);            
                //}
                //if (a5.Count != 0)
                //{
                //    this.lbl0.Text = "�г�ҩҩ����ⱨ�浥"; 
                //    this.PrintGroupData(a5); 
                //}
                return 1;
            }
            catch (Exception e)
            {
                MessageBox.Show("����!" + e.Message);
                return -1;
            }

            #endregion

           // return PrintGroupData(alPrintData);
            //try
            //{
            //    // this.Print(alPrintData, 1, 1, "", "");
             
            //}
            //catch (Exception e)
            //{
            //    MessageBox.Show("��ӡ����!" + e.Message);
               
            //}
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
                        this.Print(alPrint, i, icount, "","");
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

    }
}
        #endregion