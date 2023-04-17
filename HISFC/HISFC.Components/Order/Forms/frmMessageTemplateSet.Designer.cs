namespace FS.HISFC.Components.Order.Forms
{
    partial class frmMessageTemplateSet
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.neuLabel3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cbtype = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.neuLabel5 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txttitle = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.txtsortid = new FS.FrameWork.WinForms.Controls.NeuTextBox();
            this.neuLabel1 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.neuLabel2 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.cbstate = new FS.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.neuLabel4 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.txtcontent = new FS.FrameWork.WinForms.Controls.NeuRichTextBox();
            this.btnOK = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.btncancel = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.SuspendLayout();
            // 
            // neuLabel3
            // 
            this.neuLabel3.AutoSize = true;
            this.neuLabel3.Location = new System.Drawing.Point(25, 26);
            this.neuLabel3.Name = "neuLabel3";
            this.neuLabel3.Size = new System.Drawing.Size(65, 12);
            this.neuLabel3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel3.TabIndex = 9;
            this.neuLabel3.Text = "模板类别：";
            // 
            // cbtype
            // 
            this.cbtype.ArrowBackColor = System.Drawing.Color.Silver;
            this.cbtype.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cbtype.FormattingEnabled = true;
            this.cbtype.IsEnter2Tab = false;
            this.cbtype.IsFlat = false;
            this.cbtype.IsLike = true;
            this.cbtype.IsListOnly = false;
            this.cbtype.IsPopForm = true;
            this.cbtype.IsShowCustomerList = false;
            this.cbtype.IsShowID = false;
            this.cbtype.IsShowIDAndName = false;
            this.cbtype.Location = new System.Drawing.Point(93, 23);
            this.cbtype.Name = "cbtype";
            this.cbtype.ShowCustomerList = false;
            this.cbtype.ShowID = false;
            this.cbtype.Size = new System.Drawing.Size(262, 20);
            this.cbtype.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cbtype.TabIndex = 8;
            this.cbtype.Tag = "";
            this.cbtype.ToolBarUse = false;
            // 
            // neuLabel5
            // 
            this.neuLabel5.AutoSize = true;
            this.neuLabel5.Location = new System.Drawing.Point(25, 54);
            this.neuLabel5.Name = "neuLabel5";
            this.neuLabel5.Size = new System.Drawing.Size(65, 12);
            this.neuLabel5.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel5.TabIndex = 11;
            this.neuLabel5.Text = "模板标题：";
            // 
            // txttitle
            // 
            this.txttitle.IsEnter2Tab = false;
            this.txttitle.Location = new System.Drawing.Point(93, 50);
            this.txttitle.Name = "txttitle";
            this.txttitle.Size = new System.Drawing.Size(262, 21);
            this.txttitle.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txttitle.TabIndex = 12;
            // 
            // txtsortid
            // 
            this.txtsortid.IsEnter2Tab = false;
            this.txtsortid.Location = new System.Drawing.Point(93, 79);
            this.txtsortid.Name = "txtsortid";
            this.txtsortid.Size = new System.Drawing.Size(100, 21);
            this.txtsortid.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtsortid.TabIndex = 14;
            // 
            // neuLabel1
            // 
            this.neuLabel1.AutoSize = true;
            this.neuLabel1.Location = new System.Drawing.Point(25, 83);
            this.neuLabel1.Name = "neuLabel1";
            this.neuLabel1.Size = new System.Drawing.Size(65, 12);
            this.neuLabel1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel1.TabIndex = 13;
            this.neuLabel1.Text = "排 序 码：";
            // 
            // neuLabel2
            // 
            this.neuLabel2.AutoSize = true;
            this.neuLabel2.Location = new System.Drawing.Point(204, 84);
            this.neuLabel2.Name = "neuLabel2";
            this.neuLabel2.Size = new System.Drawing.Size(65, 12);
            this.neuLabel2.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel2.TabIndex = 15;
            this.neuLabel2.Text = "模板状态：";
            // 
            // cbstate
            // 
            this.cbstate.ArrowBackColor = System.Drawing.Color.Silver;
            this.cbstate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cbstate.FormattingEnabled = true;
            this.cbstate.IsEnter2Tab = false;
            this.cbstate.IsFlat = false;
            this.cbstate.IsLike = true;
            this.cbstate.IsListOnly = false;
            this.cbstate.IsPopForm = true;
            this.cbstate.IsShowCustomerList = false;
            this.cbstate.IsShowID = false;
            this.cbstate.IsShowIDAndName = false;
            this.cbstate.Items.AddRange(new object[] {
            "启用",
            "作废"});
            this.cbstate.Location = new System.Drawing.Point(272, 80);
            this.cbstate.Name = "cbstate";
            this.cbstate.ShowCustomerList = false;
            this.cbstate.ShowID = false;
            this.cbstate.Size = new System.Drawing.Size(83, 20);
            this.cbstate.Style = FS.FrameWork.WinForms.Controls.StyleType.Flat;
            this.cbstate.TabIndex = 16;
            this.cbstate.Tag = "";
            this.cbstate.ToolBarUse = false;
            // 
            // neuLabel4
            // 
            this.neuLabel4.AutoSize = true;
            this.neuLabel4.Location = new System.Drawing.Point(25, 110);
            this.neuLabel4.Name = "neuLabel4";
            this.neuLabel4.Size = new System.Drawing.Size(65, 12);
            this.neuLabel4.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.neuLabel4.TabIndex = 18;
            this.neuLabel4.Text = "模板内容：";
            // 
            // txtcontent
            // 
            this.txtcontent.Location = new System.Drawing.Point(27, 134);
            this.txtcontent.MaxLength = 200;
            this.txtcontent.Name = "txtcontent";
            this.txtcontent.Size = new System.Drawing.Size(328, 184);
            this.txtcontent.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.txtcontent.TabIndex = 17;
            this.txtcontent.Text = "";
            // 
            // btnOK
            // 
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOK.Location = new System.Drawing.Point(93, 324);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(69, 30);
            this.btnOK.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnOK.TabIndex = 19;
            this.btnOK.Text = "保 存";
            this.btnOK.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btncancel
            // 
            this.btncancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btncancel.Location = new System.Drawing.Point(238, 324);
            this.btncancel.Name = "btncancel";
            this.btncancel.Size = new System.Drawing.Size(69, 30);
            this.btncancel.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btncancel.TabIndex = 20;
            this.btncancel.Text = "取 消";
            this.btncancel.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btncancel.UseVisualStyleBackColor = true;
            this.btncancel.Click += new System.EventHandler(this.btncancel_Click);
            // 
            // frmMessageTemplateSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(383, 365);
            this.Controls.Add(this.btncancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.neuLabel4);
            this.Controls.Add(this.txtcontent);
            this.Controls.Add(this.cbstate);
            this.Controls.Add(this.neuLabel2);
            this.Controls.Add(this.txtsortid);
            this.Controls.Add(this.neuLabel1);
            this.Controls.Add(this.txttitle);
            this.Controls.Add(this.neuLabel5);
            this.Controls.Add(this.neuLabel3);
            this.Controls.Add(this.cbtype);
            this.Name = "frmMessageTemplateSet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "短信模板";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel3;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cbtype;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel5;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txttitle;
        private FS.FrameWork.WinForms.Controls.NeuTextBox txtsortid;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel1;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel2;
        private FS.FrameWork.WinForms.Controls.NeuComboBox cbstate;
        private FS.FrameWork.WinForms.Controls.NeuLabel neuLabel4;
        private FS.FrameWork.WinForms.Controls.NeuRichTextBox txtcontent;
        private FS.FrameWork.WinForms.Controls.NeuButton btnOK;
        private FS.FrameWork.WinForms.Controls.NeuButton btncancel;
    }
}