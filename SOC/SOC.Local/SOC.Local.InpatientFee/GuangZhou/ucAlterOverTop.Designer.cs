namespace FS.SOC.Local.InpatientFee.GuangZhou
{
    partial class ucAlterOverTop
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
            components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;

            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnChange = new System.Windows.Forms.Button();
            this.btnQuery = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtLimitTot = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtPubCost = new System.Windows.Forms.TextBox();
            this.dtEnd = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.dtBegin = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ucQueryInpatientNo1 = new FS.HISFC.Components.Common.Controls.ucQueryInpatientNo();
            this.label8 = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.lblPatientNO = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox1.Controls.Add(this.lblPatientNO);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.lblName);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.btnChange);
            this.groupBox1.Controls.Add(this.btnQuery);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtLimitTot);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtPubCost);
            this.groupBox1.Controls.Add(this.dtEnd);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.dtBegin);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.ucQueryInpatientNo1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(600, 232);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // btnChange
            // 
            this.btnChange.Location = new System.Drawing.Point(416, 142);
            this.btnChange.Name = "btnChange";
            this.btnChange.Size = new System.Drawing.Size(152, 23);
            this.btnChange.TabIndex = 19;
            this.btnChange.Text = "单击更改药品记帐金额";
            this.btnChange.Click += new System.EventHandler(this.btnChange_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(416, 72);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(152, 23);
            this.btnQuery.TabIndex = 18;
            this.btnQuery.Text = "单击查询药品记账金额";
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
            this.label7.Location = new System.Drawing.Point(381, 144);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(15, 23);
            this.label7.TabIndex = 17;
            this.label7.Text = "元";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
            this.label6.Location = new System.Drawing.Point(381, 112);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(15, 23);
            this.label6.TabIndex = 16;
            this.label6.Text = "元";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtLimitTot
            // 
            this.txtLimitTot.ForeColor = System.Drawing.Color.Red;
            this.txtLimitTot.Location = new System.Drawing.Point(237, 144);
            this.txtLimitTot.Name = "txtLimitTot";
            this.txtLimitTot.Size = new System.Drawing.Size(137, 21);
            this.txtLimitTot.TabIndex = 15;
            this.txtLimitTot.Text = "";
            this.txtLimitTot.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
            this.label5.Location = new System.Drawing.Point(8, 144);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(218, 23);
            this.label5.TabIndex = 14;
            this.label5.Text = "此段时间内患者药品应该记帐金额为";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPubCost
            // 
            this.txtPubCost.BackColor = System.Drawing.Color.White;
            this.txtPubCost.ForeColor = System.Drawing.Color.Red;
            this.txtPubCost.Location = new System.Drawing.Point(237, 112);
            this.txtPubCost.Name = "txtPubCost";
            this.txtPubCost.ReadOnly = true;
            this.txtPubCost.Size = new System.Drawing.Size(137, 21);
            this.txtPubCost.TabIndex = 13;
            this.txtPubCost.Text = "";
            this.txtPubCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // dtEnd
            // 
            this.dtEnd.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtEnd.Location = new System.Drawing.Point(236, 74);
            this.dtEnd.Name = "dtEnd";
            this.dtEnd.Size = new System.Drawing.Size(141, 21);
            this.dtEnd.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(212, 73);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(16, 23);
            this.label4.TabIndex = 11;
            this.label4.Text = "从";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtBegin
            // 
            this.dtBegin.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtBegin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtBegin.Location = new System.Drawing.Point(62, 74);
            this.dtBegin.Name = "dtBegin";
            this.dtBegin.Size = new System.Drawing.Size(140, 21);
            this.dtBegin.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(32, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(16, 23);
            this.label3.TabIndex = 9;
            this.label3.Text = "从";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
            this.label2.Location = new System.Drawing.Point(8, 112);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(218, 23);
            this.label2.TabIndex = 8;
            this.label2.Text = "此段时间内患者药品已经记帐金额为";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(31, 184);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(545, 32);
            this.label1.TabIndex = 7;
            this.label1.Text = "输入住院号后回车,选定事件后单击查询按钮查询患者指定时间内药品记账费用,输入该患应该记帐金额,单击更改限额来调整限额。";
            // 
            // ucQueryInpatientNo1
            // 
            this.ucQueryInpatientNo1.InputType = 0;
            this.ucQueryInpatientNo1.Location = new System.Drawing.Point(31, 28);
            this.ucQueryInpatientNo1.Name = "ucQueryInpatientNo1";
            this.ucQueryInpatientNo1.ShowState = FS.HISFC.Components.Common.Controls.enuShowState.All;
            this.ucQueryInpatientNo1.Size = new System.Drawing.Size(190, 32);
            this.ucQueryInpatientNo1.TabIndex = 6;
            this.ucQueryInpatientNo1.myEvent += new FS.HISFC.Components.Common.Controls.myEventDelegate(this.ucQueryInpatientNo1_myEvent);
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(243, 35);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(39, 11);
            this.label8.TabIndex = 20;
            this.label8.Text = "姓名:";
            // 
            // lblName
            // 
            this.lblName.Location = new System.Drawing.Point(289, 34);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(91, 15);
            this.lblName.TabIndex = 21;
            // 
            // lblPatientNO
            // 
            this.lblPatientNO.Location = new System.Drawing.Point(443, 35);
            this.lblPatientNO.Name = "lblPatientNO";
            this.lblPatientNO.Size = new System.Drawing.Size(91, 15);
            this.lblPatientNO.TabIndex = 23;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(390, 39);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(43, 11);
            this.label11.TabIndex = 22;
            this.label11.Text = "住院号:";
            // 
            // ucAlterOverTop
            // 
            this.Controls.Add(this.groupBox1);
            this.Name = "ucAlterOverTop";
            this.Size = new System.Drawing.Size(600, 232);
            this.Load += new System.EventHandler(this.ucAlterOverTop_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion


        private System.Windows.Forms.GroupBox groupBox1;
        //public FS.Common.Controls.ucQueryInpatientNo ucQueryInpatientNo1;
        public FS.HISFC.Components.Common.Controls.ucQueryInpatientNo ucQueryInpatientNo1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtBegin;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtPubCost;
        private System.Windows.Forms.TextBox txtLimitTot;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.Button btnChange;
        private System.Windows.Forms.DateTimePicker dtEnd;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblPatientNO;
        private System.Windows.Forms.Label label11;
    }
}
