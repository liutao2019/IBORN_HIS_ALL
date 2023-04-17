namespace FoShanYDSI.Controls
{
    partial class frmYDInBalanceInfoPrint
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnEsc = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.ucYDInBalanceInfo1 = new FoShanYDSI.Controls.ucYDInBalanceInfo();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.ucYDInBalanceInfo1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btnEsc);
            this.splitContainer1.Panel2.Controls.Add(this.btnPrint);
            this.splitContainer1.Size = new System.Drawing.Size(778, 517);
            this.splitContainer1.SplitterDistance = 475;
            this.splitContainer1.TabIndex = 0;
            // 
            // btnEsc
            // 
            this.btnEsc.Location = new System.Drawing.Point(643, 8);
            this.btnEsc.Name = "btnEsc";
            this.btnEsc.Size = new System.Drawing.Size(75, 23);
            this.btnEsc.TabIndex = 1;
            this.btnEsc.Text = "退出";
            this.btnEsc.UseVisualStyleBackColor = true;
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(529, 7);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 23);
            this.btnPrint.TabIndex = 0;
            this.btnPrint.Text = "打印";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // ucYDInBalanceInfo1
            // 
            this.ucYDInBalanceInfo1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(213)))));
            this.ucYDInBalanceInfo1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucYDInBalanceInfo1.IsFullConvertToHalf = true;
            this.ucYDInBalanceInfo1.IsPrint = false;
            this.ucYDInBalanceInfo1.Location = new System.Drawing.Point(0, 0);
            this.ucYDInBalanceInfo1.Name = "ucYDInBalanceInfo1";
            //this.ucYDInBalanceInfo1.ParentFormToolBar = null;
            this.ucYDInBalanceInfo1.Size = new System.Drawing.Size(778, 475);
            this.ucYDInBalanceInfo1.TabIndex = 0;
            // 
            // frmYDInBalanceInfoPrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(778, 517);
            this.Controls.Add(this.splitContainer1);
            this.Name = "frmYDInBalanceInfoPrint";
            this.Text = "异地医保住院结算单";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btnEsc;
        private System.Windows.Forms.Button btnPrint;
        private ucYDInBalanceInfo ucYDInBalanceInfo1;
    }
}