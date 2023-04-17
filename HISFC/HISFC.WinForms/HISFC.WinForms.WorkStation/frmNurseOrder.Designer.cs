namespace FS.HISFC.WinForms.WorkStation
{
    partial class frmNurseOrder
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。

        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tvNurseCellPatientList1 = new FS.HISFC.Components.RADT.Controls.tvNurseCellPatientList(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Size = new System.Drawing.Size(621, 343);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tvNurseCellPatientList1);
            this.panel2.Size = new System.Drawing.Size(168, 343);
            this.panel2.Controls.SetChildIndex(this.neuTextBox1, 0);
            this.panel2.Controls.SetChildIndex(this.panelTree, 0);
            this.panel2.Controls.SetChildIndex(this.btnClose, 0);
            this.panel2.Controls.SetChildIndex(this.tvNurseCellPatientList1, 0);
            this.panel2.Controls.SetChildIndex(this.btnShow, 0);
            // 
            // panelMain
            // 
            this.panelMain.Location = new System.Drawing.Point(171, 0);
            this.panelMain.Size = new System.Drawing.Size(450, 343);
            // 
            // tabControl1
            // 
            this.tabControl1.Size = new System.Drawing.Size(450, 343);
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // panelTree
            // 
            this.panelTree.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.panelTree.Size = new System.Drawing.Size(168, 322);
            // 
            // neuTextBox1
            // 
            this.neuTextBox1.BackColor = System.Drawing.Color.White;
            this.neuTextBox1.Size = new System.Drawing.Size(168, 21);
            this.neuTextBox1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.neuTextBox1_MouseDoubleClick);
            // 
            // lblSet
            // 
            this.lblSet.Location = new System.Drawing.Point(560, 431);
            // 
            // btnShow
            // 
            this.btnShow.Size = new System.Drawing.Size(13, 0);
            // 
            // panelToolBar
            // 
            this.panelToolBar.Size = new System.Drawing.Size(621, 57);
            // 
            // statusBar1
            // 
            this.statusBar1.Location = new System.Drawing.Point(0, 425);
            this.statusBar1.Size = new System.Drawing.Size(621, 24);
            // 
            // tvNurseCellPatientList1
            // 
            this.tvNurseCellPatientList1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tvNurseCellPatientList1.Checked = FS.HISFC.Components.Common.Controls.tvPatientList.enuChecked.None;
            this.tvNurseCellPatientList1.Direction = FS.HISFC.Components.Common.Controls.tvPatientList.enuShowDirection.Ahead;
            this.tvNurseCellPatientList1.Font = new System.Drawing.Font("Arial", 9F);
            this.tvNurseCellPatientList1.HideSelection = false;
            this.tvNurseCellPatientList1.ImageIndex = 0;
            this.tvNurseCellPatientList1.IsShowContextMenu = true;
            this.tvNurseCellPatientList1.IsShowCount = true;
            this.tvNurseCellPatientList1.IsShowNewPatient = true;
            this.tvNurseCellPatientList1.IsShowPatientNo = true;
            this.tvNurseCellPatientList1.Location = new System.Drawing.Point(5, 32);
            this.tvNurseCellPatientList1.Name = "tvNurseCellPatientList1";
            this.tvNurseCellPatientList1.PateintType = "0";
            this.tvNurseCellPatientList1.SelectedImageIndex = 0;
            this.tvNurseCellPatientList1.ShowType = FS.HISFC.Components.Common.Controls.tvPatientList.enuShowType.Bed;
            this.tvNurseCellPatientList1.Size = new System.Drawing.Size(161, 302);
            this.tvNurseCellPatientList1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.tvNurseCellPatientList1.TabIndex = 0;
            // 
            // timer1
            // 
            //this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // frmNurseOrder
            // 
            this.ClientSize = new System.Drawing.Size(621, 449);
            this.Name = "frmNurseOrder";
            this.Text = "护士站医嘱管理";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FS.HISFC.Components.RADT.Controls.tvNurseCellPatientList tvNurseCellPatientList1;
        private System.Windows.Forms.Timer timer1;
    }
}