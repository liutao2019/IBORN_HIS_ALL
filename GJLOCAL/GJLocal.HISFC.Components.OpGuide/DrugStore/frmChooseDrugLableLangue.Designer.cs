namespace GJLocal.HISFC.Components.OpGuide.DrugStore
{
    partial class frmChooseDrugLableLangue
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
            this.btEnglish = new System.Windows.Forms.Button();
            this.btJapaness = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblPatientInfo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btEnglish
            // 
            this.btEnglish.Location = new System.Drawing.Point(22, 73);
            this.btEnglish.Name = "btEnglish";
            this.btEnglish.Size = new System.Drawing.Size(286, 40);
            this.btEnglish.TabIndex = 0;
            this.btEnglish.Text = "English";
            this.btEnglish.UseVisualStyleBackColor = true;
            this.btEnglish.Click += new System.EventHandler(this.btEnglish_Click);
            // 
            // btJapaness
            // 
            this.btJapaness.Location = new System.Drawing.Point(22, 131);
            this.btJapaness.Name = "btJapaness";
            this.btJapaness.Size = new System.Drawing.Size(286, 40);
            this.btJapaness.TabIndex = 1;
            this.btJapaness.Text = "日本语";
            this.btJapaness.UseVisualStyleBackColor = true;
            this.btJapaness.Click += new System.EventHandler(this.btJapaness_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(19, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "当前打印患者：";
            // 
            // lblPatientInfo
            // 
            this.lblPatientInfo.AutoSize = true;
            this.lblPatientInfo.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPatientInfo.ForeColor = System.Drawing.Color.Red;
            this.lblPatientInfo.Location = new System.Drawing.Point(19, 35);
            this.lblPatientInfo.Name = "lblPatientInfo";
            this.lblPatientInfo.Size = new System.Drawing.Size(218, 16);
            this.lblPatientInfo.TabIndex = 3;
            this.lblPatientInfo.Text = "0000001234，测试患者[男]";
            // 
            // frmChooseDrugLableLangue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(332, 188);
            this.Controls.Add(this.lblPatientInfo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btJapaness);
            this.Controls.Add(this.btEnglish);
            this.Name = "frmChooseDrugLableLangue";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ChooseDrugLableLangue";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btEnglish;
        private System.Windows.Forms.Button btJapaness;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblPatientInfo;
    }
}