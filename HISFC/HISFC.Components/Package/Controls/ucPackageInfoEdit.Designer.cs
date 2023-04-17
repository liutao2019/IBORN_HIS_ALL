namespace HISFC.Components.Package.Controls
{
    partial class ucPackageInfoEdit
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
            this.plEdit = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.lblMoney = new System.Windows.Forms.Label();
            this.lbTip2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.chkSelfChoose = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.lbTip1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.chkMainFlag = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
            this.cmbValid = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.label6 = new System.Windows.Forms.Label();
            this.rbMemo = new System.Windows.Forms.RichTextBox();
            this.tbUserCode = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.tbName = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.cmbPackageRange = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.cmbPackageType = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.plSelector = new FS.FrameWork.WinForms.Controls.NeuPanel();
            this.ucPackageItemSelect1 = new HISFC.Components.Package.Controls.ucPackageItemSelect();
            this.plEdit.SuspendLayout();
            this.plSelector.SuspendLayout();
            this.SuspendLayout();
            // 
            // plEdit
            // 
            this.plEdit.Controls.Add(this.lblMoney);
            this.plEdit.Controls.Add(this.lbTip2);
            this.plEdit.Controls.Add(this.chkSelfChoose);
            this.plEdit.Controls.Add(this.lbTip1);
            this.plEdit.Controls.Add(this.chkMainFlag);
            this.plEdit.Controls.Add(this.cmbValid);
            this.plEdit.Controls.Add(this.label6);
            this.plEdit.Controls.Add(this.rbMemo);
            this.plEdit.Controls.Add(this.tbUserCode);
            this.plEdit.Controls.Add(this.tbName);
            this.plEdit.Controls.Add(this.cmbPackageRange);
            this.plEdit.Controls.Add(this.label2);
            this.plEdit.Controls.Add(this.cmbPackageType);
            this.plEdit.Controls.Add(this.label5);
            this.plEdit.Controls.Add(this.label4);
            this.plEdit.Controls.Add(this.label3);
            this.plEdit.Controls.Add(this.label1);
            this.plEdit.Dock = System.Windows.Forms.DockStyle.Top;
            this.plEdit.Location = new System.Drawing.Point(0, 0);
            this.plEdit.Name = "plEdit";
            this.plEdit.Size = new System.Drawing.Size(1187, 130);
            this.plEdit.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.plEdit.TabIndex = 1;
            // 
            // lblMoney
            // 
            this.lblMoney.AutoSize = true;
            this.lblMoney.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblMoney.ForeColor = System.Drawing.Color.Red;
            this.lblMoney.Location = new System.Drawing.Point(657, 87);
            this.lblMoney.Name = "lblMoney";
            this.lblMoney.Size = new System.Drawing.Size(193, 22);
            this.lblMoney.TabIndex = 60;
            this.lblMoney.Text = "项目包总金额：￥500.00";
            // 
            // lbTip2
            // 
            this.lbTip2.AutoSize = true;
            this.lbTip2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbTip2.ForeColor = System.Drawing.Color.Red;
            this.lbTip2.Location = new System.Drawing.Point(204, 100);
            this.lbTip2.Name = "lbTip2";
            this.lbTip2.Size = new System.Drawing.Size(320, 17);
            this.lbTip2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbTip2.TabIndex = 59;
            this.lbTip2.Text = "自选包无需维护项目，在购买套餐时可以自由选择收费项目";
            // 
            // chkSelfChoose
            // 
            this.chkSelfChoose.AutoSize = true;
            this.chkSelfChoose.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.chkSelfChoose.ForeColor = System.Drawing.Color.Black;
            this.chkSelfChoose.Location = new System.Drawing.Point(105, 97);
            this.chkSelfChoose.Name = "chkSelfChoose";
            this.chkSelfChoose.Size = new System.Drawing.Size(67, 23);
            this.chkSelfChoose.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.chkSelfChoose.TabIndex = 58;
            this.chkSelfChoose.Text = "自选包";
            this.chkSelfChoose.UseVisualStyleBackColor = true;
            // 
            // lbTip1
            // 
            this.lbTip1.AutoSize = true;
            this.lbTip1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbTip1.ForeColor = System.Drawing.Color.Red;
            this.lbTip1.Location = new System.Drawing.Point(204, 76);
            this.lbTip1.Name = "lbTip1";
            this.lbTip1.Size = new System.Drawing.Size(320, 17);
            this.lbTip1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbTip1.TabIndex = 57;
            this.lbTip1.Text = "默认项目包在购买套餐时会自动带出，非默认项目包则不会";
            // 
            // chkMainFlag
            // 
            this.chkMainFlag.AutoSize = true;
            this.chkMainFlag.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.chkMainFlag.ForeColor = System.Drawing.Color.Black;
            this.chkMainFlag.Location = new System.Drawing.Point(105, 73);
            this.chkMainFlag.Name = "chkMainFlag";
            this.chkMainFlag.Size = new System.Drawing.Size(93, 23);
            this.chkMainFlag.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.chkMainFlag.TabIndex = 56;
            this.chkMainFlag.Text = "默认项目包";
            this.chkMainFlag.UseVisualStyleBackColor = true;
            // 
            // cmbValid
            // 
            this.cmbValid.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.cmbValid.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbValid.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbValid.FormattingEnabled = true;
            this.cmbValid.IsEnter2Tab = false;
            this.cmbValid.IsFlat = false;
            this.cmbValid.IsLike = true;
            this.cmbValid.IsListOnly = false;
            this.cmbValid.IsPopForm = true;
            this.cmbValid.IsShowCustomerList = false;
            this.cmbValid.IsShowID = false;
            this.cmbValid.IsShowIDAndName = false;
            this.cmbValid.Location = new System.Drawing.Point(549, 5);
            this.cmbValid.Name = "cmbValid";
            this.cmbValid.ShowCustomerList = false;
            this.cmbValid.ShowID = false;
            this.cmbValid.Size = new System.Drawing.Size(54, 27);
            this.cmbValid.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbValid.TabIndex = 55;
            this.cmbValid.Tag = "";
            this.cmbValid.ToolBarUse = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.ForeColor = System.Drawing.Color.Blue;
            this.label6.Location = new System.Drawing.Point(505, 10);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 19);
            this.label6.TabIndex = 54;
            this.label6.Text = "状态:";
            // 
            // rbMemo
            // 
            this.rbMemo.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.rbMemo.Location = new System.Drawing.Point(661, 7);
            this.rbMemo.Name = "rbMemo";
            this.rbMemo.Size = new System.Drawing.Size(297, 59);
            this.rbMemo.TabIndex = 53;
            this.rbMemo.Text = "";
            // 
            // tbUserCode
            // 
            this.tbUserCode.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.tbUserCode.IsEnter2Tab = false;
            this.tbUserCode.Location = new System.Drawing.Point(408, 7);
            this.tbUserCode.Name = "tbUserCode";
            this.tbUserCode.Size = new System.Drawing.Size(91, 25);
            this.tbUserCode.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbUserCode.TabIndex = 52;
            // 
            // tbName
            // 
            this.tbName.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.tbName.IsEnter2Tab = false;
            this.tbName.Location = new System.Drawing.Point(101, 6);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(210, 25);
            this.tbName.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tbName.TabIndex = 51;
            // 
            // cmbPackageRange
            // 
            this.cmbPackageRange.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.cmbPackageRange.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbPackageRange.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbPackageRange.FormattingEnabled = true;
            this.cmbPackageRange.IsEnter2Tab = false;
            this.cmbPackageRange.IsFlat = false;
            this.cmbPackageRange.IsLike = true;
            this.cmbPackageRange.IsListOnly = false;
            this.cmbPackageRange.IsPopForm = true;
            this.cmbPackageRange.IsShowCustomerList = false;
            this.cmbPackageRange.IsShowID = false;
            this.cmbPackageRange.IsShowIDAndName = false;
            this.cmbPackageRange.Location = new System.Drawing.Point(408, 39);
            this.cmbPackageRange.Name = "cmbPackageRange";
            this.cmbPackageRange.ShowCustomerList = false;
            this.cmbPackageRange.ShowID = false;
            this.cmbPackageRange.Size = new System.Drawing.Size(195, 27);
            this.cmbPackageRange.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbPackageRange.TabIndex = 50;
            this.cmbPackageRange.Tag = "";
            this.cmbPackageRange.ToolBarUse = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(315, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 19);
            this.label2.TabIndex = 49;
            this.label2.Text = "项目包范围：";
            // 
            // cmbPackageType
            // 
            this.cmbPackageType.ArrowBackColor = System.Drawing.SystemColors.Control;
            this.cmbPackageType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbPackageType.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbPackageType.FormattingEnabled = true;
            this.cmbPackageType.IsEnter2Tab = false;
            this.cmbPackageType.IsFlat = false;
            this.cmbPackageType.IsLike = true;
            this.cmbPackageType.IsListOnly = false;
            this.cmbPackageType.IsPopForm = true;
            this.cmbPackageType.IsShowCustomerList = false;
            this.cmbPackageType.IsShowID = false;
            this.cmbPackageType.IsShowIDAndName = false;
            this.cmbPackageType.Location = new System.Drawing.Point(101, 39);
            this.cmbPackageType.Name = "cmbPackageType";
            this.cmbPackageType.ShowCustomerList = false;
            this.cmbPackageType.ShowID = false;
            this.cmbPackageType.Size = new System.Drawing.Size(210, 27);
            this.cmbPackageType.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cmbPackageType.TabIndex = 48;
            this.cmbPackageType.Tag = "";
            this.cmbPackageType.ToolBarUse = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(610, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 19);
            this.label5.TabIndex = 47;
            this.label5.Text = "备注：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(328, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 19);
            this.label4.TabIndex = 46;
            this.label4.Text = "自定义码：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(8, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 19);
            this.label3.TabIndex = 45;
            this.label3.Text = "项目包类型：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(8, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 19);
            this.label1.TabIndex = 44;
            this.label1.Text = "项目包名称：";
            // 
            // plSelector
            // 
            this.plSelector.Controls.Add(this.ucPackageItemSelect1);
            this.plSelector.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plSelector.Location = new System.Drawing.Point(0, 130);
            this.plSelector.Name = "plSelector";
            this.plSelector.Size = new System.Drawing.Size(1187, 39);
            this.plSelector.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.plSelector.TabIndex = 2;
            // 
            // ucPackageItemSelect1
            // 
            this.ucPackageItemSelect1.BackColor = System.Drawing.Color.White;
            this.ucPackageItemSelect1.CurrentChildPackage = null;
            this.ucPackageItemSelect1.CurrentDetail = null;
            this.ucPackageItemSelect1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucPackageItemSelect1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ucPackageItemSelect1.Location = new System.Drawing.Point(0, 0);
            this.ucPackageItemSelect1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ucPackageItemSelect1.Name = "ucPackageItemSelect1";
            this.ucPackageItemSelect1.Size = new System.Drawing.Size(1187, 39);
            this.ucPackageItemSelect1.TabIndex = 3;
            // 
            // ucPackageInfoEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.plSelector);
            this.Controls.Add(this.plEdit);
            this.Name = "ucPackageInfoEdit";
            this.Size = new System.Drawing.Size(1187, 169);
            this.plEdit.ResumeLayout(false);
            this.plEdit.PerformLayout();
            this.plSelector.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuPanel plEdit;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbTip1;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox chkMainFlag;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbValid;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RichTextBox rbMemo;
        private FS.FrameWork.WinForms.Controls.NeuTextBox tbUserCode;
        private FS.FrameWork.WinForms.Controls.NeuTextBox tbName;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbPackageRange;
        private System.Windows.Forms.Label label2;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cmbPackageType;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private FS.FrameWork.WinForms.Controls.NeuPanel plSelector;
        private ucPackageItemSelect ucPackageItemSelect1;
        private FS.FrameWork.WinForms.Controls.NeuLabel lbTip2;
        private FS.FrameWork.WinForms.Controls.NeuCheckBox chkSelfChoose;
        private System.Windows.Forms.Label lblMoney;

    }
}
