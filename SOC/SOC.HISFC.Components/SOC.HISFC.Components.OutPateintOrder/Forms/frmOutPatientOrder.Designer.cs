namespace FS.SOC.HISFC.Components.OutPatientOrder.Forms
{
    partial class frmOutPatientOrder
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
            this.tvOutOrderPatientList1 = new FS.SOC.HISFC.Components.OutPatientOrder.Controls.IOutPateintTree.tvOutOrderPatientList();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.panelTree.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).BeginInit();
            this.SuspendLayout();
            // 
            // panelTree
            // 
            this.panelTree.Controls.Add(this.tvOutOrderPatientList1);
            // 
            // neuTextBox1
            // 
            this.neuTextBox1.BackColor = System.Drawing.Color.White;
            // 
            // tvOutOrderPatientList1
            // 
            //{014680EC-6381-408b-98FB-A549DAA49B82}
            //this.tvOutOrderPatientList1.Checked = FS.SOC.HISFC.Components.OutPatientOrder.Controls.IOutPateintTree.tvOutOrderPatientList.enuChecked.None;
            //this.tvOutOrderPatientList1.Direction = FS.SOC.HISFC.Components.OutPatientOrder.Controls.IOutPateintTree.tvOutOrderPatientList.enuShowDirection.Ahead;
            this.tvOutOrderPatientList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvOutOrderPatientList1.HideSelection = false;
            //this.tvOutOrderPatientList1.IsShowContextMenu = true;
            //this.tvOutOrderPatientList1.IsShowCount = true;
            //this.tvOutOrderPatientList1.IsShowNewPatient = true;
            //this.tvOutOrderPatientList1.IsShowPatientNo = true;
            this.tvOutOrderPatientList1.Location = new System.Drawing.Point(0, 0);
            this.tvOutOrderPatientList1.Name = "tvOutOrderPatientList1";
            //this.tvOutOrderPatientList1.ShowType = FS.SOC.HISFC.Components.OutPatientOrder.Controls.IOutPateintTree.tvOutOrderPatientList.enuShowType.Bed;
            this.tvOutOrderPatientList1.Size = new System.Drawing.Size(206, 359);
            this.tvOutOrderPatientList1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tvOutOrderPatientList1.TabIndex = 0;
            // 
            // frmOutPatientOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(603, 486);
            this.Name = "frmOutPatientOrder";
            this.Text = "frmOutPatientOrder";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panelMain.ResumeLayout(false);
            this.panelTree.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FS.SOC.HISFC.Components.OutPatientOrder.Controls.IOutPateintTree.tvOutOrderPatientList tvOutOrderPatientList1;

    }
}