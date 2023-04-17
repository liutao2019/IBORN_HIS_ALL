namespace Neusoft.SOC.Local.EMR.ZDLY.Forms
{
    partial class frmRecordLock
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
            DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem1 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem1 = new DevExpress.Utils.ToolTipItem();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnUnlock = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.emrFindText1 = new Neusoft.SOC.Local.EMR.ZDLY.Controls.emrFindText(this.components);
            this.gcLock = new DevExpress.XtraGrid.GridControl();
            this.gvLock = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.defaultLookAndFeel1 = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcLock)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvLock)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnUnlock);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.emrFindText1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(658, 68);
            this.panelControl1.TabIndex = 0;
            // 
            // btnUnlock
            // 
            this.btnUnlock.Appearance.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnUnlock.Appearance.Options.UseFont = true;
            this.btnUnlock.Location = new System.Drawing.Point(239, 21);
            this.btnUnlock.Name = "btnUnlock";
            this.btnUnlock.Size = new System.Drawing.Size(97, 29);
            toolTipTitleItem1.Text = "嘿，你知道吗？";
            toolTipItem1.LeftIndent = 6;
            toolTipItem1.Text = "按CTL 可以选择多行进行解锁";
            superToolTip1.Items.Add(toolTipTitleItem1);
            superToolTip1.Items.Add(toolTipItem1);
            this.btnUnlock.SuperTip = superToolTip1;
            this.btnUnlock.TabIndex = 2;
            this.btnUnlock.Text = "解锁";
            this.btnUnlock.ToolTip = "按CTL 可以选择多行进行解锁";
            this.btnUnlock.ToolTipTitle = "张总告诉你";
            this.btnUnlock.Click += new System.EventHandler(this.btnUnlock_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelControl1.Location = new System.Drawing.Point(10, 24);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(42, 14);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "查找：";
            // 
            // emrFindText1
            // 
            this.emrFindText1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.emrFindText1.HintStr = "住院号|姓名";
            this.emrFindText1.Location = new System.Drawing.Point(58, 21);
            this.emrFindText1.Name = "emrFindText1";
            this.emrFindText1.Size = new System.Drawing.Size(128, 23);
            this.emrFindText1.TabIndex = 0;
            this.emrFindText1.TextChanged += new System.EventHandler(this.emrFindText1_TextChanged);
            // 
            // gcLock
            // 
            this.gcLock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcLock.Location = new System.Drawing.Point(0, 68);
            this.gcLock.MainView = this.gvLock;
            this.gcLock.Name = "gcLock";
            this.gcLock.Size = new System.Drawing.Size(658, 370);
            this.gcLock.TabIndex = 1;
            this.gcLock.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvLock});
            // 
            // gvLock
            // 
            this.gvLock.Appearance.FocusedRow.ForeColor = System.Drawing.Color.Blue;
            this.gvLock.Appearance.FocusedRow.Options.UseForeColor = true;
            this.gvLock.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gvLock.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvLock.Appearance.OddRow.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gvLock.Appearance.OddRow.Options.UseFont = true;
            this.gvLock.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gvLock.Appearance.Row.Options.UseFont = true;
            this.gvLock.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn2,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn10,
            this.gridColumn3,
            this.gridColumn1,
            this.gridColumn8,
            this.gridColumn9});
            this.gvLock.GridControl = this.gcLock;
            this.gvLock.Name = "gvLock";
            this.gvLock.OptionsBehavior.Editable = false;
            this.gvLock.OptionsCustomization.AllowColumnMoving = false;
            this.gvLock.OptionsCustomization.AllowColumnResizing = false;
            this.gvLock.OptionsCustomization.AllowFilter = false;
            this.gvLock.OptionsSelection.MultiSelect = true;
            this.gvLock.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "住院号";
            this.gridColumn2.FieldName = "PATIENT_NO";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 0;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "患者姓名";
            this.gridColumn4.FieldName = "PATIENTNAME";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 1;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "病历";
            this.gridColumn5.FieldName = "NAME";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 3;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "科室";
            this.gridColumn6.FieldName = "DEPT_NAME";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 2;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "加锁人员";
            this.gridColumn7.FieldName = "EMPL_NAME";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 4;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "加锁日期";
            this.gridColumn3.FieldName = "OPER_TIME";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 6;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "gridColumn1";
            this.gridColumn1.FieldName = "ID";
            this.gridColumn1.Name = "gridColumn1";
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "gridColumn8";
            this.gridColumn8.FieldName = "INPTIENT_ID";
            this.gridColumn8.Name = "gridColumn8";
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "gridColumn9";
            this.gridColumn9.FieldName = "IN_RECORD_ID";
            this.gridColumn9.Name = "gridColumn9";
            // 
            // defaultLookAndFeel1
            // 
            this.defaultLookAndFeel1.LookAndFeel.SkinName = "Blue";
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "加锁工号";
            this.gridColumn10.FieldName = "EMPL_CODE";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 5;
            // 
            // frmRecordLock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(658, 438);
            this.Controls.Add(this.gcLock);
            this.Controls.Add(this.panelControl1);
            this.Name = "frmRecordLock";
            this.Text = "病历解锁";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmRecordLock_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcLock)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvLock)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private Neusoft.SOC.Local.EMR.ZDLY.Controls.emrFindText emrFindText1;
        private DevExpress.XtraEditors.SimpleButton btnUnlock;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraGrid.GridControl gcLock;
        private DevExpress.XtraGrid.Views.Grid.GridView gvLock;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
    }
}