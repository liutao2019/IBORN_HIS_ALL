using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;


namespace FS.WinForms.Report.Pharmacy
{
	/// <summary>
	/// ucPhaInput ��ժҪ˵����
	/// </summary>
	public class ucPhaInputBill : System.Windows.Forms.UserControl,FS.HISFC.BizProcess.Interface.Pharmacy.IBillPrint
	{
		/// <summary> 
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;
        private Label label1;
        private Label label2;
        private HISFC.BizLogic.Manager.PowerLevelManager levelManager = new FS.HISFC.BizLogic.Manager.PowerLevelManager();
		public ucPhaInputBill()
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
            FarPoint.Win.Spread.TipAppearance tipAppearance5 = new FarPoint.Win.Spread.TipAppearance();
            FarPoint.Win.Spread.CellType.TextCellType textCellType17 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType18 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType19 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType25 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType20 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType26 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType27 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType28 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType29 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType30 = new FarPoint.Win.Spread.CellType.NumberCellType();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.lb34 = new System.Windows.Forms.Label();
            this.lb33 = new System.Windows.Forms.Label();
            this.lb32 = new System.Windows.Forms.Label();
            this.lb31 = new System.Windows.Forms.Label();
            this.lb13 = new System.Windows.Forms.Label();
            this.lb11 = new System.Windows.Forms.Label();
            this.fpSpread1 = new FarPoint.Win.Spread.FpSpread();
            this.sheetView1 = new FarPoint.Win.Spread.SheetView();
            this.lbTitle = new System.Windows.Forms.Label();
            this.lb35 = new System.Windows.Forms.Label();
            this.lb21 = new System.Windows.Forms.Label();
            this.lb36 = new System.Windows.Forms.Label();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sheetView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.lb34);
            this.panel3.Controls.Add(this.lb33);
            this.panel3.Controls.Add(this.lb32);
            this.panel3.Controls.Add(this.lb31);
            this.panel3.Controls.Add(this.lb13);
            this.panel3.Controls.Add(this.lb11);
            this.panel3.Controls.Add(this.fpSpread1);
            this.panel3.Controls.Add(this.lbTitle);
            this.panel3.Controls.Add(this.lb35);
            this.panel3.Controls.Add(this.lb21);
            this.panel3.Controls.Add(this.lb36);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(879, 416);
            this.panel3.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(3, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(149, 16);
            this.label2.TabIndex = 60;
            this.label2.Text = "��ԭʼƾ֤���ݣ�:1";
            // 
            // lb34
            // 
            this.lb34.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb34.Location = new System.Drawing.Point(330, 77);
            this.lb34.Name = "lb34";
            this.lb34.Size = new System.Drawing.Size(146, 16);
            this.lb34.TabIndex = 59;
            this.lb34.Text = "ҩƷ���:";
            // 
            // lb33
            // 
            this.lb33.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb33.Location = new System.Drawing.Point(176, 77);
            this.lb33.Name = "lb33";
            this.lb33.Size = new System.Drawing.Size(123, 16);
            this.lb33.TabIndex = 58;
            this.lb33.Text = "�ɹ�Ա:";
            // 
            // lb32
            // 
            this.lb32.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb32.Location = new System.Drawing.Point(3, 77);
            this.lb32.Name = "lb32";
            this.lb32.Size = new System.Drawing.Size(107, 16);
            this.lb32.TabIndex = 57;
            this.lb32.Text = "ҩ�����Ա:";
            // 
            // lb31
            // 
            this.lb31.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb31.Location = new System.Drawing.Point(425, 23);
            this.lb31.Name = "lb31";
            this.lb31.Size = new System.Drawing.Size(62, 16);
            this.lb31.TabIndex = 56;
            this.lb31.Text = "�Ƶ��ˣ�";
            this.lb31.Visible = false;
            // 
            // lb13
            // 
            this.lb13.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb13.Location = new System.Drawing.Point(330, 56);
            this.lb13.Name = "lb13";
            this.lb13.Size = new System.Drawing.Size(126, 16);
            this.lb13.TabIndex = 55;
            this.lb13.Text = "����:";
            // 
            // lb11
            // 
            this.lb11.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb11.Location = new System.Drawing.Point(477, 77);
            this.lb11.Name = "lb11";
            this.lb11.Size = new System.Drawing.Size(299, 16);
            this.lb11.TabIndex = 54;
            this.lb11.Text = "������λ:";
            // 
            // fpSpread1
            // 
            this.fpSpread1.About = "3.0.2004.2005";
            this.fpSpread1.AccessibleDescription = "fpSpread1, Sheet1, Row 0, Column 0, ";
            this.fpSpread1.BackColor = System.Drawing.Color.Transparent;
            this.fpSpread1.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fpSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never;
            this.fpSpread1.Location = new System.Drawing.Point(7, 96);
            this.fpSpread1.Name = "fpSpread1";
            this.fpSpread1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fpSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.sheetView1});
            this.fpSpread1.Size = new System.Drawing.Size(785, 151);
            this.fpSpread1.TabIndex = 49;
            this.fpSpread1.TabStripPolicy = FarPoint.Win.Spread.TabStripPolicy.Never;
            tipAppearance5.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance5.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance5.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpSpread1.TextTipAppearance = tipAppearance5;
            this.fpSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never;
            // 
            // sheetView1
            // 
            this.sheetView1.Reset();
            this.sheetView1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.sheetView1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.sheetView1.ColumnCount = 14;
            this.sheetView1.RowCount = 14;
            this.sheetView1.RowHeader.ColumnCount = 0;
            this.sheetView1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.SystemColors.WindowText, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, true, false, true, true, true);
            this.sheetView1.Cells.Get(0, 2).ForeColor = System.Drawing.Color.Black;
            this.sheetView1.Cells.Get(0, 8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.sheetView1.Cells.Get(0, 12).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.sheetView1.Cells.Get(13, 13).Value = "    ";
            this.sheetView1.ColumnHeader.Cells.Get(0, 0).Value = "�嵥��";
            this.sheetView1.ColumnHeader.Cells.Get(0, 1).Value = "����";
            this.sheetView1.ColumnHeader.Cells.Get(0, 2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.sheetView1.ColumnHeader.Cells.Get(0, 2).Value = "ҩƷ����";
            this.sheetView1.ColumnHeader.Cells.Get(0, 3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.sheetView1.ColumnHeader.Cells.Get(0, 3).Value = "���";
            this.sheetView1.ColumnHeader.Cells.Get(0, 4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.sheetView1.ColumnHeader.Cells.Get(0, 4).Value = "����";
            this.sheetView1.ColumnHeader.Cells.Get(0, 5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.sheetView1.ColumnHeader.Cells.Get(0, 5).Value = "��λ";
            this.sheetView1.ColumnHeader.Cells.Get(0, 6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.sheetView1.ColumnHeader.Cells.Get(0, 6).Value = "���ۼ�";
            this.sheetView1.ColumnHeader.Cells.Get(0, 7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.sheetView1.ColumnHeader.Cells.Get(0, 7).Value = "����";
            this.sheetView1.ColumnHeader.Cells.Get(0, 8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.sheetView1.ColumnHeader.Cells.Get(0, 8).Value = "�����ܶ�";
            this.sheetView1.ColumnHeader.Cells.Get(0, 9).Value = "�����ܶ�";
            this.sheetView1.ColumnHeader.Cells.Get(0, 10).Value = "���";
            this.sheetView1.ColumnHeader.Cells.Get(0, 11).Value = "����";
            this.sheetView1.ColumnHeader.Cells.Get(0, 12).Value = "����";
            this.sheetView1.ColumnHeader.Cells.Get(0, 13).Value = "��ע";
            this.sheetView1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.sheetView1.ColumnHeader.DefaultStyle.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Bold);
            this.sheetView1.ColumnHeader.DefaultStyle.Locked = false;
            this.sheetView1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.sheetView1.ColumnHeader.Rows.Get(0).Height = 26F;
            this.sheetView1.Columns.Get(0).Label = "�嵥��";
            this.sheetView1.Columns.Get(0).Width = 45F;
            this.sheetView1.Columns.Get(1).CellType = textCellType17;
            this.sheetView1.Columns.Get(1).Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.sheetView1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.sheetView1.Columns.Get(1).Label = "����";
            this.sheetView1.Columns.Get(1).Width = 46F;
            this.sheetView1.Columns.Get(2).BackColor = System.Drawing.Color.White;
            this.sheetView1.Columns.Get(2).CellType = textCellType18;
            this.sheetView1.Columns.Get(2).Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.sheetView1.Columns.Get(2).ForeColor = System.Drawing.Color.Black;
            this.sheetView1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.sheetView1.Columns.Get(2).Label = "ҩƷ����";
            this.sheetView1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.sheetView1.Columns.Get(2).Width = 120F;
            this.sheetView1.Columns.Get(3).CellType = textCellType19;
            this.sheetView1.Columns.Get(3).Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.sheetView1.Columns.Get(3).ForeColor = System.Drawing.Color.Black;
            this.sheetView1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.sheetView1.Columns.Get(3).Label = "���";
            this.sheetView1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.sheetView1.Columns.Get(3).Width = 53F;
            numberCellType25.MaximumValue = 9999999.9;
            numberCellType25.MinimumValue = -9999999.9;
            this.sheetView1.Columns.Get(4).CellType = numberCellType25;
            this.sheetView1.Columns.Get(4).Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.sheetView1.Columns.Get(4).ForeColor = System.Drawing.Color.Black;
            this.sheetView1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.sheetView1.Columns.Get(4).Label = "����";
            this.sheetView1.Columns.Get(4).Width = 48F;
            this.sheetView1.Columns.Get(5).CellType = textCellType20;
            this.sheetView1.Columns.Get(5).Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.sheetView1.Columns.Get(5).ForeColor = System.Drawing.Color.Black;
            this.sheetView1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.sheetView1.Columns.Get(5).Label = "��λ";
            this.sheetView1.Columns.Get(5).Width = 30F;
            this.sheetView1.Columns.Get(6).CellType = numberCellType26;
            this.sheetView1.Columns.Get(6).Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.sheetView1.Columns.Get(6).ForeColor = System.Drawing.Color.Black;
            this.sheetView1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.sheetView1.Columns.Get(6).Label = "���ۼ�";
            this.sheetView1.Columns.Get(6).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.sheetView1.Columns.Get(6).Width = 51F;
            this.sheetView1.Columns.Get(7).CellType = numberCellType27;
            this.sheetView1.Columns.Get(7).Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.sheetView1.Columns.Get(7).ForeColor = System.Drawing.Color.Black;
            this.sheetView1.Columns.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.sheetView1.Columns.Get(7).Label = "����";
            this.sheetView1.Columns.Get(7).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.sheetView1.Columns.Get(7).Width = 51F;
            this.sheetView1.Columns.Get(8).CellType = numberCellType28;
            this.sheetView1.Columns.Get(8).Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.sheetView1.Columns.Get(8).ForeColor = System.Drawing.Color.Black;
            this.sheetView1.Columns.Get(8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.sheetView1.Columns.Get(8).Label = "�����ܶ�";
            this.sheetView1.Columns.Get(8).Width = 59F;
            this.sheetView1.Columns.Get(9).CellType = numberCellType29;
            this.sheetView1.Columns.Get(9).Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.sheetView1.Columns.Get(9).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.sheetView1.Columns.Get(9).Label = "�����ܶ�";
            this.sheetView1.Columns.Get(9).Width = 59F;
            this.sheetView1.Columns.Get(10).CellType = numberCellType30;
            this.sheetView1.Columns.Get(10).Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.sheetView1.Columns.Get(10).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.sheetView1.Columns.Get(10).Label = "���";
            this.sheetView1.Columns.Get(10).Width = 57F;
            this.sheetView1.Columns.Get(11).Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.sheetView1.Columns.Get(11).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.sheetView1.Columns.Get(11).Label = "����";
            this.sheetView1.Columns.Get(11).Width = 90F;
            this.sheetView1.Columns.Get(12).Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.sheetView1.Columns.Get(12).Label = "����";
            this.sheetView1.Columns.Get(12).Width = 43F;
            this.sheetView1.Columns.Get(13).Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.sheetView1.Columns.Get(13).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.sheetView1.Columns.Get(13).Label = "��ע";
            this.sheetView1.Columns.Get(13).Width = 35F;
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
            // 
            // lbTitle
            // 
            this.lbTitle.Font = new System.Drawing.Font("����", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbTitle.Location = new System.Drawing.Point(216, 18);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(391, 24);
            this.lbTitle.TabIndex = 6;
            this.lbTitle.Text = "ҩƷ��ⱨ�浥";
            // 
            // lb35
            // 
            this.lb35.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb35.Location = new System.Drawing.Point(477, 56);
            this.lb35.Name = "lb35";
            this.lb35.Size = new System.Drawing.Size(163, 16);
            this.lb35.TabIndex = 53;
            this.lb35.Text = "��ӡ����";
            // 
            // lb21
            // 
            this.lb21.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb21.Location = new System.Drawing.Point(179, 56);
            this.lb21.Name = "lb21";
            this.lb21.Size = new System.Drawing.Size(153, 16);
            this.lb21.TabIndex = 3;
            this.lb21.Text = "�Ʊ�:";
            // 
            // lb36
            // 
            this.lb36.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb36.Location = new System.Drawing.Point(656, 56);
            this.lb36.Name = "lb36";
            this.lb36.Size = new System.Drawing.Size(112, 16);
            this.lb36.TabIndex = 53;
            this.lb36.Text = "ҳ��";
            // 
            // ucPhaInputBill
            // 
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Controls.Add(this.panel3);
            this.Name = "ucPhaInputBill";
            this.Size = new System.Drawing.Size(879, 416);
            this.panel3.ResumeLayout(false);
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

        //private System.Windows.Forms.Label lb11;
        //private System.Windows.Forms.Label lb12;
        //private System.Windows.Forms.Label lb13;
        //private System.Windows.Forms.Label lb22;
        //private System.Windows.Forms.Label lb23;
		#endregion

		/// <summary>
		/// ҩƷ����
		/// </summary>
        private FS.HISFC.BizLogic.Pharmacy.Item itemMgr = new FS.HISFC.BizLogic.Pharmacy.Item();

        private FS.HISFC.BizLogic.Pharmacy.Constant conMgr = new FS.HISFC.BizLogic.Pharmacy.Constant();
        //private System.Windows.Forms.Label lb34;
        //private System.Windows.Forms.Label lb33;
        //private System.Windows.Forms.Label lb32;
        //private System.Windows.Forms.Label lb31;
		/// <summary>
		/// ��Ա��
		/// </summary>
        private FS.HISFC.BizLogic.Manager.Person psMgr = new FS.HISFC.BizLogic.Manager.Person();
        private Panel panel3;
        private FarPoint.Win.Spread.FpSpread fpSpread1;
        private FarPoint.Win.Spread.SheetView sheetView1;
        private Label lbTitle;
        private Label lb35;
        private Label lb21;
        private Label lb36;
        private Label lb34;
        private Label lb33;
        private Label lb32;
        private Label lb31;
        private Label lb13;
        private Label lb11;

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
            //this.panel1.Width = this.Width;
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
		/// <param name="kind">��ӡ���� 0.���ƻ���  1.�ɹ���   2.��ⵥ   3.���ⵥ    5 ������뵥.</param>
        private void Print(ArrayList al, int inow, int icount, string operCode, string kind)
        {
            this.PrintInput( al, inow, icount, operCode );//�ƻ���
        }
		#endregion

		#region ��ⵥ��ӡ
		/// <summary>
		/// ��ӡ����
		/// </summary>
		/// <param name="al">��ӡ����</param>
		/// <param name="i">�ڼ�ҳ</param>
		/// <param name="count">��ҳ��</param>
		/// <param name="operCode">�Ƶ���</param>
		private void PrintInput(ArrayList al, int inow,int icount,string operCode)
		{
			if( al.Count <= 0 )
			{
				MessageBox.Show("û�д�ӡ������!");
				return;
			}
            FS.HISFC.BizLogic.Pharmacy.Constant constant = new FS.HISFC.BizLogic.Pharmacy.Constant();
            FS.HISFC.Models.Pharmacy.Input info = (FS.HISFC.Models.Pharmacy.Input)al[0];
			
			#region label��ֵ
			//this.lbTitle.Text = this.deptMgr.GetDeptmentById(info.StockDept.ID)+"ҩƷ��ⱨ�浥";

            FS.HISFC.Models.Admin.PowerLevelClass3 class3 = levelManager.LoadLevel3ByPrimaryKey(info.Class2Type, info.PrivType);
            this.lbTitle.Text = constantMgr.GetHospitalName() + this.deptMgr.GetDeptmentById(info.StockDept.ID) + "ҩƷ" + class3.Class3Name + "��";

			if(this.IsReprint)
			{
				this.lbTitle.Text = this.lbTitle.Text + "(����)"; 
			}
			string strCompany = "";
			try
			{
				strCompany = this.conMgr.QueryCompanyByCompanyID(info.Company.ID).Name;
			}
			catch{}
            this.lb11.Text = "������λ:" + strCompany;
			//this.lb12.Text = "�ջ�����:" + info.User02;
            this.lb13.Text = "����:" + info.InListNO;

            this.lb21.Text = "�Ʊ�:" + this.deptMgr.GetDeptmentById(info.StockDept.ID);//"������λ:" + this.deptMgr.GetDeptmentById(info.StockDept.ID);
			//this.lb22.Text = "��Ʊ����:" + info.InvoiceNO;
			//this.lb23.Text = "�ڲ�����:" + info.User01;
           // this.lb31.Text = "�Ƶ�:" + this.psMgr.GetPersonByID(operCode);
            //this.lb32.Text = "����:";
            //this.lb33.Text = "������:";
            //this.lb34.Text = "������:"; 
            this.lb32.Text = "ҩ�����Ա:" + this.psMgr.GetPersonByID(operCode); ;
            this.lb33.Text = "�ɹ�Ա:";
            this.lb34.Text = "ҩƷ���:";

            this.lb35.Text = "��ӡ����:" + this.psMgr.GetSysDateTime("yyyy.MM.dd");
			this.lb36.Text = "��" + inow.ToString() + "ҳ/��" + icount.ToString() + "ҳ" ;
			#endregion

			#region farpoint��ֵ
            string strMemo = string.Empty;
			decimal sumNum2 = 0;
			decimal sumNum7 = 0;
			decimal sumNum8 = 0;
            //decimal sumNum9 = 0;
			decimal sumNum10 = 0;
			this.sheetView1.RowCount = 0;
            if (info.SystemType == "17" || info.SystemType == "18" || info.SystemType == "19")
            {
                this.sheetView1.Columns[13].Label = "��ע";
                this.sheetView1.Columns[13].Visible = true;
            }
            else
            {  //��ʱ����Ч���ڲ��ɼ�
                this.sheetView1.Columns[13].Label = "��Ч����";
                this.sheetView1.Columns[13].Visible = false;
                
            }

            for (int i = 0; i < al.Count; i++)
            {
                this.sheetView1.AddRows(i, 1);
                FS.HISFC.Models.Pharmacy.Input input = al[i] as FS.HISFC.Models.Pharmacy.Input;
                this.sheetView1.Cells[i, 1].Text = input.Item.ID; //ҩƷ����  //this.itemMgr.GetItem(input.Item.ID).UserCode; //input.Item.UserCode+"??";//ҩƷ�Զ�����
                this.sheetView1.Cells[i, 2].Text = input.Item.Name;//ҩƷ����
                this.sheetView1.Cells[i, 3].Text = input.Item.Specs;//���		
                this.sheetView1.Cells[i, 5].Text = input.Item.PackUnit;//��λ
                if (input.Item.PackQty == 0) input.Item.PackQty = 1;
                decimal count = 0;
                count = input.Quantity;
                this.sheetView1.Cells[i, 4].Text = (count / input.Item.PackQty).ToString();//����				
                this.sheetView1.Cells[i, 7].Text = (input.Item.PriceCollection.PurchasePrice).ToString();//������              
                this.sheetView1.Cells[i, 8].Text = ((input.Item.PriceCollection.RetailPrice) * count / input.Item.PackQty).ToString();//�����ܽ��
                this.sheetView1.Cells[i, 6].Text = (input.Item.PriceCollection.RetailPrice).ToString();//���ۼ�
                this.sheetView1.Cells[i, 9].Text = ((input.Item.PriceCollection.PurchasePrice) * count / input.Item.PackQty).ToString();//�����ܽ�� 
                this.sheetView1.Cells[i, 10].Text = ((input.Item.PriceCollection.RetailPrice) * count / input.Item.PackQty
                    - (input.Item.PriceCollection.PurchasePrice) * count / input.Item.PackQty).ToString();//�������
                this.sheetView1.Cells[i, 12].Text = input.BatchNO.ToString();
                //�˿��ʱ����ʾ��ע�������������ʾ��Ч��
               // this.sheetView1.Cells[i, 10].Text = input.ValidTime.ToString();
                strMemo = input.Memo;
                if (string.IsNullOrEmpty(strMemo))
                {
                    strMemo = "";
                }
                if (input.SystemType == "17" || input.SystemType == "18" || input.SystemType == "19")
                {


                    this.sheetView1.Cells[i, 13].Text = strMemo;
                }
                else
                {

                    
                    this.sheetView1.Cells[i, 13].Text = input.ValidTime.ToString();
                    
                   
                }

                this.sheetView1.Cells[i, 11].Text = input.Item.Product.Producer.ToString();
                if ((input.Item.Product.Producer.ToString()) != "")
                    this.sheetView1.Cells[i, 11].Text = input.Item.Product.Producer.ToString();
                else
                    this.sheetView1.Cells[i, 11].Text = "δ¼��";

                sumNum2 = sumNum2 + count / input.Item.PackQty;
                sumNum7 = sumNum7 + (input.Item.PriceCollection.RetailPrice) * count / input.Item.PackQty;
                sumNum8 = sumNum8 + (input.Item.PriceCollection.PurchasePrice) * count / input.Item.PackQty;
                sumNum10 = sumNum10 + (input.Item.PriceCollection.RetailPrice) * count / input.Item.PackQty
                    - (input.Item.PriceCollection.PurchasePrice) * count / input.Item.PackQty;
            }

          
           

			this.sheetView1.RowCount = al.Count + 1;			
			this.sheetView1.Cells[al.Count,0].Text = "�ϼ�";
            this.sheetView1.Cells[al.Count,1].Text = "��" + al.Count + "��";//����
			this.sheetView1.Cells[al.Count,4].Text = sumNum2.ToString();			
			this.sheetView1.Cells[al.Count,9].Text = sumNum8.ToString();
            this.sheetView1.Cells[al.Count,8].Text = sumNum7.ToString();
			//this.sheetView1.Cells[al.Count,9].Text = sumNum9.ToString();
			this.sheetView1.Cells[al.Count,10].Text = sumNum10.ToString();
			#endregion 
			
			#region �������
			
			//���
            //this.panel4.Width = this.Width - 3;
            //this.fpSpread1.Width = this.panel4.Width - 10;
			this.fpSpread1.Height = (int)this.sheetView1.RowHeader.Rows[0].Height +
				(int)(this.sheetView1.Rows[0].Height*(al.Count+1)) + 10;
			
			//�߶�����
            //this.panel4.Height = this.fpSpread1.Height +3;
            //this.lb31.Location = new Point(this.lb31.Location.X,this.fpSpread1.Location.Y + this.fpSpread1.Height + 1);
            //this.lb32.Location = new Point(this.lb32.Location.X,this.lb31.Location.Y );
            //this.lb33.Location = new Point(this.lb33.Location.X,this.lb31.Location.Y );
            //this.lb34.Location = new Point(this.lb34.Location.X,this.lb31.Location.Y );
            this.lb35.Location = new Point(this.lb35.Location.X, this.lb21.Location.Y);
            this.lb36.Location = new Point(this.lb36.Location.X, this.lb21.Location.Y);
			
			#endregion

			#region ��ӡ����
            //FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
            //p.IsDataAutoExtend = false;//p.ShowPageSetup();
            //FS.HISFC.Models.Base.PageSize page = new FS.HISFC.Models.Base.PageSize();
            ////page.Height = 342;
            ////page.Width = 342;
            ////page.Name = "PhaInput";
            //FS.HISFC.BizLogic.Manager.PageSize pManager = new FS.HISFC.BizLogic.Manager.PageSize();
            //p.SetPageSize(pManager.GetPageSize("PhaInput"));
            //p.PrintPage(5, 0, this);

            FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
            p.IsDataAutoExtend = false;

            FS.HISFC.Components.Common.Classes.Function.GetPageSize("PhaInput", ref p);

            p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.Border;
            p.IsHaveGrid = true;

            p.PrintPage(5, 0, this.panel3);



            //FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();

            //HISFC.Components.Common.Classes.Function.GetPageSize("PhaInput", p);

            //FS.HISFC.BizLogic.Manager.PageSize pageManger = new FS.HISFC.BizLogic.Manager.PageSize();

            //FS.HISFC.Models.Base.PageSize ps = new FS.HISFC.Models.Base.PageSize();


            //p.SetPageSize(pageManger.GetPageSize("PhaInput"));


            //p.PrintPage(5, 0, this);

            //FS.FrameWork.WinForms.Classes.Print p = null;

            //try
            //{
            //    p = new FS.FrameWork.WinForms.Classes.Print();
            //}
            //catch (Exception e)
            //{
            //    MessageBox.Show("��ʼ����ӡ������");
            //}

            //FS.HISFC.BizLogic.Manager.PageSize pageManger = new FS.HISFC.BizLogic.Manager.PageSize();
            
            //p.SetPageSize(pageManger.GetPageSize("PhaInput"));

            //p.PrintPreview(this);
			#endregion
 
		}

		#endregion

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
            if (billType == FS.HISFC.BizProcess.Interface.Pharmacy.BillType.InnerApplyIn)
            {
                return this.SetDataPrintApp(alPrintData,billType);
            }

            return 1;
        }

        public int SetData(ArrayList alPrintData, string privType)
        {
            this.maxrowno = 12;
            try
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
            catch (Exception e)
            {
                MessageBox.Show("��ӡ����!" + e.Message);
                return -1;
            }
        }

        public int SetData(string billNO)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion

        /// <summary>
        /// ���ô�ӡҳ��(����˿�����)
        /// 
        ///             //{0084F0DF-44E5-4fe9-9DBC-E92CFCDC0636} ʵ���ڲ�������뵥��ӡ
        /// </summary>
        /// <param name="alPrintData"></param>
        public int SetDataPrintApp(ArrayList alPrintData,FS.HISFC.BizProcess.Interface.Pharmacy.BillType billType)
        {
            this.maxrowno = 13;

            try
            {
                ArrayList alPrint = new ArrayList();
                int icount = FS.FrameWork.Function.NConvert.ToInt32(Math.Ceiling(Convert.ToDouble(alPrintData.Count) / MaxRowNo));

                FS.HISFC.Models.Base.Employee operEmp = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;

                for (int i = 1; i <= icount; i++)
                {
                    if (i != icount)
                    {
                        alPrint = alPrintData.GetRange(MaxRowNo * (i - 1), MaxRowNo);
                        this.InputPrintApp( alPrint, i, icount, operEmp.ID, billType );
                    }
                    else
                    {
                        int num = alPrintData.Count % MaxRowNo;
                        if (alPrintData.Count % MaxRowNo == 0)
                        {
                            num = MaxRowNo;
                        }
                        alPrint = alPrintData.GetRange(MaxRowNo * (i - 1), num);

                        this.InputPrintApp( alPrint, i, icount, operEmp.ID, billType );
                    }
                }
                return 1;
            }
            catch (Exception e)
            {
                MessageBox.Show("��ӡ����!" + e.Message);
                return -1;
            }
        }

        /// <summary>
        /// ��ӡ����(����˿�����)
        /// 
        ///             //{0084F0DF-44E5-4fe9-9DBC-E92CFCDC0636} ʵ���ڲ�������뵥��ӡ
        /// </summary>
        /// <param name="al">��ӡ����</param>
        /// <param name="irowno">�ڼ�ҳ</param>
        /// <param name="icount">��ҳ��</param>
        /// <param name="operCode">�Ƶ���</param>
        public void InputPrintApp(ArrayList al, int irowno, int icount, string operCode,FS.HISFC.BizProcess.Interface.Pharmacy.BillType billType)
        {
            if (al.Count <= 0)
            {
                MessageBox.Show("�޿ɴ�ӡ������");
                return;
            }

            FS.HISFC.BizLogic.Pharmacy.Constant constant = new FS.HISFC.BizLogic.Pharmacy.Constant();
            FS.HISFC.Models.Pharmacy.ApplyOut info = (FS.HISFC.Models.Pharmacy.ApplyOut)al[0];

            #region label��ֵ
            if (info.SystemType == "13")        //�ڲ��������
            {
                this.lbTitle.Text = this.deptMgr.GetDeptmentById( info.StockDept.ID ) + " ������뵥";
            }
            else
            {
                this.lbTitle.Text = this.deptMgr.GetDeptmentById( info.StockDept.ID ) + " �˿����뵥";
            }
            if (this.IsReprint)
            {
                this.lbTitle.Text = this.lbTitle.Text + "(����)";
            }

            this.lb21.Text = "�������:" + this.deptMgr.GetDeptmentById(info.ApplyDept.ID);

            this.lb35.Text = "��ӡ����:" + this.psMgr.GetSysDateTime("yyyy.MM.dd");
            this.lb36.Text = "��" + irowno.ToString() + "ҳ/��" + icount.ToString() + "ҳ";
            #endregion

            #region farpoint��ֵ

            decimal sumNum2 = 0;
            decimal sumNum7 = 0;
            decimal sumNum8 = 0;

            decimal sumNum10 = 0;

            this.sheetView1.RowCount = 0;
            for (int i = 0; i < al.Count; i++)
            {
                this.sheetView1.AddRows(i, 1);

                FS.HISFC.Models.Pharmacy.ApplyOut applyout = al[i] as FS.HISFC.Models.Pharmacy.ApplyOut;

                this.sheetView1.Cells[i, 1].Text = applyout.Item.ID;//this.itemMgr.GetItem(applyout.Item.ID).UserCode; 
                this.sheetView1.Cells[i, 2].Text = applyout.Item.Name;//ҩƷ����
                this.sheetView1.Cells[i, 3].Text = applyout.Item.Specs;//���		
                this.sheetView1.Cells[i, 5].Text = applyout.Item.PackUnit;//��λ
                if (applyout.Item.PackQty == 0)
                {
                    applyout.Item.PackQty = 1;
                }

                decimal count = 0;
                count = applyout.Operation.ApplyQty;

                if (applyout.SystemType == "13")        //�ڲ��������
                {
                    
                    
                    this.sheetView1.Cells[i, 4].Text = (count / applyout.Item.PackQty).ToString();//����	
                    this.sheetView1.Cells[i, 6].Text = (applyout.Item.PriceCollection.RetailPrice).ToString();//���ۼ�
                    this.sheetView1.Cells[i, 7].Text = (applyout.Item.PriceCollection.PurchasePrice).ToString();//���� 
                    this.sheetView1.Cells[i, 8].Text = ((applyout.Item.PriceCollection.RetailPrice) * count / applyout.Item.PackQty).ToString();//�����ܽ��
                    this.sheetView1.Cells[i, 9].Text = ((applyout.Item.PriceCollection.PurchasePrice) * count / applyout.Item.PackQty).ToString(); //�����ܶ�
                                 
                    
                    
                    //this.sheetView1.Cells[i, 8].Text = ((applyout.Item.PriceCollection.PurchasePrice) * count / applyout.Item.PackQty).ToString();//�����ܽ�� 
                    this.sheetView1.Cells[i, 10].Text = ((applyout.Item.PriceCollection.RetailPrice) * count / applyout.Item.PackQty
                        - (applyout.Item.PriceCollection.PurchasePrice) * count / applyout.Item.PackQty).ToString();//�������
                }
                else                                   //�ڲ�����˿�����
                {
                    this.sheetView1.Cells[i, 4].Text = (-(count / applyout.Item.PackQty)).ToString();//����	
                    this.sheetView1.Cells[i, 7].Text = (applyout.Item.PriceCollection.PurchasePrice).ToString();//������              
                    this.sheetView1.Cells[i, 8].Text = ((-applyout.Item.PriceCollection.RetailPrice) * count / applyout.Item.PackQty).ToString();//�����ܽ��
                    this.sheetView1.Cells[i, 6].Text = (applyout.Item.PriceCollection.RetailPrice).ToString();//���ۼ�
                    this.sheetView1.Cells[i, 9].Text = ((-applyout.Item.PriceCollection.PurchasePrice) * count / applyout.Item.PackQty).ToString();//�����ܽ�� 
                    this.sheetView1.Cells[i, 10].Text = ((applyout.Item.PriceCollection.RetailPrice) * count / applyout.Item.PackQty
                        - (applyout.Item.PriceCollection.PurchasePrice) * count / applyout.Item.PackQty).ToString();//�������
                }
                
                
                this.sheetView1.Cells[i, 12].Text = applyout.BatchNO.ToString();
                //����
                //this.sheetView1.Columns[10].Visible = true;
                

                if ((applyout.Item.Product.Producer.ToString()) != "")
                    this.sheetView1.Cells[i, 11].Text = applyout.Item.Product.Producer.ToString();
                else
                    this.sheetView1.Cells[i, 11].Text = "δ¼��";
                //��Ч��
               

                //if ((applyout.Item.Product.Producer.ToString()) != "")
                //    this.sheetView1.Cells[i, 12].Text = applyout.Item.Product.Producer.ToString();
                //else
                //    this.sheetView1.Cells[i, 12].Text = "δ¼��";

                sumNum2 = sumNum2 + count / applyout.Item.PackQty;
                sumNum7 = sumNum7 + (applyout.Item.PriceCollection.RetailPrice) * count / applyout.Item.PackQty;
                sumNum8 = sumNum8 + (applyout.Item.PriceCollection.PurchasePrice) * count / applyout.Item.PackQty;

                sumNum10 = sumNum10 + (applyout.Item.PriceCollection.RetailPrice) * count / applyout.Item.PackQty
                    - (applyout.Item.PriceCollection.PurchasePrice) * count / applyout.Item.PackQty;
            }
            this.sheetView1.RowCount = al.Count + 1;
            this.sheetView1.Cells[al.Count, 0].Text = "�ϼ�";
            this.sheetView1.Cells[al.Count, 1].Text = "��" + al.Count + "��";//����

            this.sheetView1.Cells[al.Count, 4].Text = sumNum2.ToString();
            this.sheetView1.Cells[al.Count, 9].Text = sumNum8.ToString();
            this.sheetView1.Cells[al.Count, 8].Text = sumNum7.ToString();

            this.sheetView1.Cells[al.Count, 10].Text = sumNum10.ToString();
            #endregion

            #region �������

            this.fpSpread1.Height = (int)this.sheetView1.RowHeader.Rows[0].Height +
                (int)(this.sheetView1.Rows[0].Height * (al.Count + 1)) + 10;

            this.lb35.Location = new Point(this.lb35.Location.X, this.lb21.Location.Y);
            this.lb36.Location = new Point(this.lb36.Location.X, this.lb21.Location.Y);

            #endregion

            #region ��ӡ����

            FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
            p.IsDataAutoExtend = false;

            FS.HISFC.Components.Common.Classes.Function.GetPageSize("PhaInput", ref p);

            p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.Border;
            p.IsHaveGrid = true;

            p.PrintPage(5, 0, this.panel3);

            #endregion
        }
      
    }
}
