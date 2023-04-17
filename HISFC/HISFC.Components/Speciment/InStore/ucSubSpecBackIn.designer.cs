namespace FS.HISFC.Components.Speciment.InStore
{
    partial class ucSubSpecBackIn
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
            this.ucBaseControl1 = new FS.FrameWork.WinForms.Controls.ucBaseControl();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSpecBarCode = new System.Windows.Forms.TextBox();
            this.grpSubInfo = new System.Windows.Forms.GroupBox();
            this.txtDisType = new System.Windows.Forms.TextBox();
            this.cmbSpecType = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.label10 = new System.Windows.Forms.Label();
            this.txtOutCount = new System.Windows.Forms.TextBox();
            this.nudSpecCount = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.nudSpecCap = new System.Windows.Forms.NumericUpDown();
            this.label14 = new System.Windows.Forms.Label();
            this.txtTumorPro = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtLastReturn = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtComment = new System.Windows.Forms.TextBox();
            this.txtTumorType = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.grpLocateInfo = new System.Windows.Forms.GroupBox();
            this.lvNewLocate = new System.Windows.Forms.ListBox();
            this.label8 = new System.Windows.Forms.Label();
            this.lvOldLacate = new System.Windows.Forms.ListBox();
            this.btnIn = new System.Windows.Forms.Button();
            this.grpSubInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSpecCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSpecCap)).BeginInit();
            this.grpLocateInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // ucBaseControl1
            // 
            this.ucBaseControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucBaseControl1.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ucBaseControl1.IsPrint = false;
            this.ucBaseControl1.Location = new System.Drawing.Point(0, 0);
            this.ucBaseControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ucBaseControl1.Name = "ucBaseControl1";
            this.ucBaseControl1.Size = new System.Drawing.Size(960, 589);
            this.ucBaseControl1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 12);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "标本条形码：";
            // 
            // txtSpecBarCode
            // 
            this.txtSpecBarCode.Location = new System.Drawing.Point(130, 8);
            this.txtSpecBarCode.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtSpecBarCode.Name = "txtSpecBarCode";
            this.txtSpecBarCode.Size = new System.Drawing.Size(182, 26);
            this.txtSpecBarCode.TabIndex = 2;
            this.txtSpecBarCode.TextChanged += new System.EventHandler(this.txtSpecBarCode_TextChanged);
            this.txtSpecBarCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSpecBarCode_KeyDown);
            // 
            // grpSubInfo
            // 
            this.grpSubInfo.Controls.Add(this.txtDisType);
            this.grpSubInfo.Controls.Add(this.cmbSpecType);
            this.grpSubInfo.Controls.Add(this.label10);
            this.grpSubInfo.Controls.Add(this.txtOutCount);
            this.grpSubInfo.Controls.Add(this.nudSpecCount);
            this.grpSubInfo.Controls.Add(this.label9);
            this.grpSubInfo.Controls.Add(this.label11);
            this.grpSubInfo.Controls.Add(this.nudSpecCap);
            this.grpSubInfo.Controls.Add(this.label14);
            this.grpSubInfo.Controls.Add(this.txtTumorPro);
            this.grpSubInfo.Controls.Add(this.label7);
            this.grpSubInfo.Controls.Add(this.txtLastReturn);
            this.grpSubInfo.Controls.Add(this.label6);
            this.grpSubInfo.Controls.Add(this.txtComment);
            this.grpSubInfo.Controls.Add(this.txtTumorType);
            this.grpSubInfo.Controls.Add(this.label4);
            this.grpSubInfo.Controls.Add(this.label3);
            this.grpSubInfo.Controls.Add(this.label2);
            this.grpSubInfo.Location = new System.Drawing.Point(22, 44);
            this.grpSubInfo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpSubInfo.Name = "grpSubInfo";
            this.grpSubInfo.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpSubInfo.Size = new System.Drawing.Size(880, 235);
            this.grpSubInfo.TabIndex = 3;
            this.grpSubInfo.TabStop = false;
            this.grpSubInfo.Text = "标本信息";
            // 
            // txtDisType
            // 
            this.txtDisType.Location = new System.Drawing.Point(338, 34);
            this.txtDisType.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtDisType.Name = "txtDisType";
            this.txtDisType.Size = new System.Drawing.Size(98, 26);
            this.txtDisType.TabIndex = 48;
            // 
            // cmbSpecType
            // 
            //this.cmbSpecType.A = false;
            this.cmbSpecType.ArrowBackColor = System.Drawing.Color.Silver;
            this.cmbSpecType.FormattingEnabled = true;
            this.cmbSpecType.IsFlat = true;
            this.cmbSpecType.IsLike = true;
            this.cmbSpecType.Location = new System.Drawing.Point(86, 34);
            this.cmbSpecType.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbSpecType.Name = "cmbSpecType";
            this.cmbSpecType.PopForm = null;
            this.cmbSpecType.ShowCustomerList = false;
            this.cmbSpecType.ShowID = false;
            this.cmbSpecType.Size = new System.Drawing.Size(140, 24);
            this.cmbSpecType.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.cmbSpecType.TabIndex = 47;
            this.cmbSpecType.Tag = "";
            this.cmbSpecType.ToolBarUse = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(712, 91);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(88, 16);
            this.label10.TabIndex = 34;
            this.label10.Text = "借出次数：";
            // 
            // txtOutCount
            // 
            this.txtOutCount.Location = new System.Drawing.Point(806, 84);
            this.txtOutCount.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtOutCount.Name = "txtOutCount";
            this.txtOutCount.Size = new System.Drawing.Size(50, 26);
            this.txtOutCount.TabIndex = 33;
            // 
            // nudSpecCount
            // 
            this.nudSpecCount.Location = new System.Drawing.Point(774, 35);
            this.nudSpecCount.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.nudSpecCount.Name = "nudSpecCount";
            this.nudSpecCount.Size = new System.Drawing.Size(82, 26);
            this.nudSpecCount.TabIndex = 32;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(712, 40);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(56, 16);
            this.label9.TabIndex = 31;
            this.label9.Text = "数量：";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(8, 158);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(56, 16);
            this.label11.TabIndex = 30;
            this.label11.Text = "备注：";
            // 
            // nudSpecCap
            // 
            this.nudSpecCap.Location = new System.Drawing.Point(600, 35);
            this.nudSpecCap.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.nudSpecCap.Name = "nudSpecCap";
            this.nudSpecCap.Size = new System.Drawing.Size(82, 26);
            this.nudSpecCap.TabIndex = 29;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(242, 37);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(88, 16);
            this.label14.TabIndex = 27;
            this.label14.Text = "标本病种：";
            // 
            // txtTumorPro
            // 
            this.txtTumorPro.Location = new System.Drawing.Point(282, 85);
            this.txtTumorPro.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtTumorPro.Name = "txtTumorPro";
            this.txtTumorPro.Size = new System.Drawing.Size(98, 26);
            this.txtTumorPro.TabIndex = 14;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(390, 91);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(120, 16);
            this.label7.TabIndex = 13;
            this.label7.Text = "上次返还时间：";
            // 
            // txtLastReturn
            // 
            this.txtLastReturn.Location = new System.Drawing.Point(526, 84);
            this.txtLastReturn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtLastReturn.Name = "txtLastReturn";
            this.txtLastReturn.Size = new System.Drawing.Size(154, 26);
            this.txtLastReturn.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(220, 91);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 16);
            this.label6.TabIndex = 11;
            this.label6.Text = "癌性质：";
            // 
            // txtComment
            // 
            this.txtComment.Location = new System.Drawing.Point(86, 133);
            this.txtComment.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtComment.Multiline = true;
            this.txtComment.Name = "txtComment";
            this.txtComment.Size = new System.Drawing.Size(472, 65);
            this.txtComment.TabIndex = 18;
            // 
            // txtTumorType
            // 
            this.txtTumorType.Location = new System.Drawing.Point(86, 84);
            this.txtTumorType.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtTumorType.Name = "txtTumorType";
            this.txtTumorType.Size = new System.Drawing.Size(104, 26);
            this.txtTumorType.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 91);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 16);
            this.label4.TabIndex = 7;
            this.label4.Text = "肿物类型：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(490, 40);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "标本容量：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 40);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "标本类型：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 84);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 16);
            this.label5.TabIndex = 9;
            this.label5.Text = "原占位置：";
            // 
            // grpLocateInfo
            // 
            this.grpLocateInfo.Controls.Add(this.lvNewLocate);
            this.grpLocateInfo.Controls.Add(this.label8);
            this.grpLocateInfo.Controls.Add(this.lvOldLacate);
            this.grpLocateInfo.Controls.Add(this.label5);
            this.grpLocateInfo.Location = new System.Drawing.Point(22, 287);
            this.grpLocateInfo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpLocateInfo.Name = "grpLocateInfo";
            this.grpLocateInfo.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpLocateInfo.Size = new System.Drawing.Size(880, 282);
            this.grpLocateInfo.TabIndex = 4;
            this.grpLocateInfo.TabStop = false;
            this.grpLocateInfo.Text = "位置信息";
            // 
            // lvNewLocate
            // 
            this.lvNewLocate.FormattingEnabled = true;
            this.lvNewLocate.ItemHeight = 16;
            this.lvNewLocate.Location = new System.Drawing.Point(86, 162);
            this.lvNewLocate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lvNewLocate.Name = "lvNewLocate";
            this.lvNewLocate.Size = new System.Drawing.Size(442, 100);
            this.lvNewLocate.TabIndex = 29;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 204);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(88, 16);
            this.label8.TabIndex = 28;
            this.label8.Text = "现占位置：";
            // 
            // lvOldLacate
            // 
            this.lvOldLacate.FormattingEnabled = true;
            this.lvOldLacate.ItemHeight = 16;
            this.lvOldLacate.Location = new System.Drawing.Point(86, 41);
            this.lvOldLacate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lvOldLacate.Name = "lvOldLacate";
            this.lvOldLacate.Size = new System.Drawing.Size(442, 100);
            this.lvOldLacate.TabIndex = 27;
            // 
            // btnIn
            // 
            this.btnIn.Location = new System.Drawing.Point(394, 8);
            this.btnIn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnIn.Name = "btnIn";
            this.btnIn.Size = new System.Drawing.Size(100, 30);
            this.btnIn.TabIndex = 5;
            this.btnIn.Text = "入库";
            this.btnIn.UseVisualStyleBackColor = true;
            this.btnIn.Click += new System.EventHandler(this.btnIn_Click);
            // 
            // ucSubSpecBackIn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnIn);
            this.Controls.Add(this.grpLocateInfo);
            this.Controls.Add(this.grpSubInfo);
            this.Controls.Add(this.txtSpecBarCode);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ucBaseControl1);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "ucSubSpecBackIn";
            this.Size = new System.Drawing.Size(960, 589);
            this.Load += new System.EventHandler(this.ucSubSpecBackIn_Load);
            this.grpSubInfo.ResumeLayout(false);
            this.grpSubInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSpecCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSpecCap)).EndInit();
            this.grpLocateInfo.ResumeLayout(false);
            this.grpLocateInfo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.ucBaseControl ucBaseControl1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSpecBarCode;
        private System.Windows.Forms.GroupBox grpSubInfo;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtTumorPro;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtLastReturn;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtTumorType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudSpecCap;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtComment;
        private System.Windows.Forms.GroupBox grpLocateInfo;
        private System.Windows.Forms.ListBox lvOldLacate;
        private System.Windows.Forms.ListBox lvNewLocate;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown nudSpecCount;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtOutCount;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbSpecType;
        private System.Windows.Forms.TextBox txtDisType;
        private System.Windows.Forms.Button btnIn;
    }
}
