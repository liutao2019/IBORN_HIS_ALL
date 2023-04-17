namespace Neusoft.HISFC.Components.Manager.Controls
{
    partial class ucPactCompare
	{
		/// <summary> 
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// 清理所有正在使用的资源。
		/// </summary>
		/// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region 组件设计器生成的代码

		/// <summary> 
		/// 设计器支持所需的方法 - 不要
		/// 使用代码编辑器修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            FarPoint.Win.Spread.TipAppearance tipAppearance3 = new FarPoint.Win.Spread.TipAppearance();
            this.fpCompany = new Neusoft.FrameWork.WinForms.Controls.NeuFpEnter(this.components);
            this.fpCompany_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.txtQueryCode = new System.Windows.Forms.TextBox();
            this.chbMisty = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.fpCompany)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpCompany_Sheet1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // fpCompany
            // 
            this.fpCompany.About = "3.0.2004.2005";
            this.fpCompany.AccessibleDescription = "";
            this.fpCompany.BackColor = System.Drawing.Color.Azure;
            this.fpCompany.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpCompany.EditModePermanent = true;
            this.fpCompany.EditModeReplace = true;
            this.fpCompany.Location = new System.Drawing.Point(0, 0);
            this.fpCompany.Name = "fpCompany";
            this.fpCompany.SelectNone = false;
            this.fpCompany.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpCompany_Sheet1});
            this.fpCompany.ShowListWhenOfFocus = false;
            this.fpCompany.Size = new System.Drawing.Size(800, 560);
            this.fpCompany.TabIndex = 0;
            tipAppearance3.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            tipAppearance3.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpCompany.TextTipAppearance = tipAppearance3;
            // 
            // fpCompany_Sheet1
            // 
            this.fpCompany_Sheet1.Reset();
            this.fpCompany_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpCompany_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpCompany_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.SystemColors.Control, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, false, false, false, true, true);
            this.fpCompany_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.fpCompany_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpCompany_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.fpCompany_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
            this.fpCompany_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
            this.fpCompany_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.White;
            this.fpCompany_Sheet1.SheetCornerStyle.Parent = "HeaderDefault";
            this.fpCompany_Sheet1.VisualStyles = FarPoint.Win.VisualStyles.Off;
            this.fpCompany_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.txtQueryCode);
            this.panel1.Controls.Add(this.chbMisty);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 40);
            this.panel1.TabIndex = 1;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(16, 8);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(47, 12);
            this.label11.TabIndex = 16;
            this.label11.Text = "查询码:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtQueryCode
            // 
            this.txtQueryCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtQueryCode.Location = new System.Drawing.Point(64, 8);
            this.txtQueryCode.Name = "txtQueryCode";
            this.txtQueryCode.Size = new System.Drawing.Size(175, 21);
            this.txtQueryCode.TabIndex = 15;
            this.txtQueryCode.TextChanged += new System.EventHandler(this.txtQueryCode_TextChanged);
            // 
            // chbMisty
            // 
            this.chbMisty.ForeColor = System.Drawing.Color.Black;
            this.chbMisty.Location = new System.Drawing.Point(248, 8);
            this.chbMisty.Name = "chbMisty";
            this.chbMisty.Size = new System.Drawing.Size(73, 21);
            this.chbMisty.TabIndex = 14;
            this.chbMisty.Text = "模糊查询";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.fpCompany);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 40);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(800, 560);
            this.panel2.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(337, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(401, 12);
            this.label1.TabIndex = 16;
            this.label1.Text = "提示：修改完后光标不要放在当前修改行上。删除行然后点保存后才有效！";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ucPactStatRelation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.BackColor = System.Drawing.Color.Azure;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "ucPactStatRelation";
            this.Size = new System.Drawing.Size(800, 600);
            this.Load += new System.EventHandler(this.ucCompanyManager_Load);
            ((System.ComponentModel.ISupportInitialize)(this.fpCompany)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpCompany_Sheet1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtQueryCode;
        private System.Windows.Forms.CheckBox chbMisty;
        private System.Windows.Forms.Panel panel2;
        private Neusoft.FrameWork.WinForms.Controls.NeuFpEnter fpCompany;
        private FarPoint.Win.Spread.SheetView fpCompany_Sheet1;
        private System.Windows.Forms.Label label1;
	}
}
