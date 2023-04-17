namespace Neusoft.SOC.Local.EMR.ZDLY.Forms
{
    partial class frmPatientList
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
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.grcPatientList = new DevExpress.XtraGrid.GridControl();
            this.gvPatientList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.pnlPatientList = new DevExpress.XtraEditors.PanelControl();
            this.emrFindText1 = new Neusoft.SOC.Local.EMR.ZDLY.Controls.emrFindText(this.components);
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.defaultLookAndFeel1 = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            this.pnlMain = new DevExpress.XtraEditors.PanelControl();
            this.pnllist = new DevExpress.XtraEditors.PanelControl();
            this.chkduty = new DevExpress.XtraEditors.CheckEdit();
            this.pnloutdate = new DevExpress.XtraEditors.PanelControl();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.dteendate = new DevExpress.XtraEditors.DateEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.dtebegindate = new DevExpress.XtraEditors.DateEdit();
            this.rgpInstate = new DevExpress.XtraEditors.RadioGroup();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.lueDeptList = new DevExpress.XtraEditors.LookUpEdit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grcPatientList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPatientList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlPatientList)).BeginInit();
            this.pnlPatientList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnllist)).BeginInit();
            this.pnllist.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkduty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnloutdate)).BeginInit();
            this.pnloutdate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dteendate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteendate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtebegindate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtebegindate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgpInstate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueDeptList.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(2, 49);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
            this.xtraTabControl1.Size = new System.Drawing.Size(1026, 566);
            this.xtraTabControl1.TabIndex = 0;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Appearance.Header.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xtraTabPage1.Appearance.Header.Options.UseFont = true;
            this.xtraTabPage1.Controls.Add(this.grcPatientList);
            this.xtraTabPage1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(1021, 537);
            this.xtraTabPage1.Text = "患者列表";
            // 
            // grcPatientList
            // 
            this.grcPatientList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grcPatientList.Location = new System.Drawing.Point(0, 0);
            this.grcPatientList.MainView = this.gvPatientList;
            this.grcPatientList.Name = "grcPatientList";
            this.grcPatientList.Size = new System.Drawing.Size(1021, 537);
            this.grcPatientList.TabIndex = 0;
            this.grcPatientList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvPatientList});
            // 
            // gvPatientList
            // 
            this.gvPatientList.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gvPatientList.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvPatientList.Appearance.OddRow.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gvPatientList.Appearance.OddRow.Options.UseFont = true;
            this.gvPatientList.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gvPatientList.Appearance.Row.Options.UseFont = true;
            this.gvPatientList.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3});
            this.gvPatientList.GridControl = this.grcPatientList;
            this.gvPatientList.IndicatorWidth = 40;
            this.gvPatientList.Name = "gvPatientList";
            this.gvPatientList.OptionsBehavior.Editable = false;
            this.gvPatientList.OptionsCustomization.AllowColumnMoving = false;
            this.gvPatientList.OptionsView.ColumnAutoWidth = false;
            this.gvPatientList.OptionsView.ShowGroupPanel = false;
            this.gvPatientList.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gvPatientList_CustomDrawRowIndicator);
            this.gvPatientList.DoubleClick += new System.EventHandler(this.gvPatientList_DoubleClick);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "gridColumn1";
            this.gridColumn1.FieldName = "PATIENT_NO";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "gridColumn2";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "gridColumn3";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.PageVisible = false;
            this.xtraTabPage2.Size = new System.Drawing.Size(1021, 537);
            this.xtraTabPage2.Text = "xtraTabPage2";
            // 
            // pnlPatientList
            // 
            this.pnlPatientList.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.pnlPatientList.Controls.Add(this.emrFindText1);
            this.pnlPatientList.Controls.Add(this.labelControl1);
            this.pnlPatientList.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlPatientList.Location = new System.Drawing.Point(0, 0);
            this.pnlPatientList.Name = "pnlPatientList";
            this.pnlPatientList.Size = new System.Drawing.Size(1030, 66);
            this.pnlPatientList.TabIndex = 1;
            // 
            // emrFindText1
            // 
            this.emrFindText1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.emrFindText1.HintStr = "住院号|姓名|床号";
            this.emrFindText1.Location = new System.Drawing.Point(54, 34);
            this.emrFindText1.Name = "emrFindText1";
            this.emrFindText1.Size = new System.Drawing.Size(215, 26);
            this.emrFindText1.TabIndex = 1;
            this.emrFindText1.TextChanged += new System.EventHandler(this.emrFindText1_TextChanged);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Location = new System.Drawing.Point(10, 35);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(38, 19);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "查找:";
            // 
            // defaultLookAndFeel1
            // 
            this.defaultLookAndFeel1.LookAndFeel.SkinName = "Blue";
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.xtraTabControl1);
            this.pnlMain.Controls.Add(this.pnllist);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 66);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(1030, 617);
            this.pnlMain.TabIndex = 2;
            // 
            // pnllist
            // 
            this.pnllist.Controls.Add(this.chkduty);
            this.pnllist.Controls.Add(this.pnloutdate);
            this.pnllist.Controls.Add(this.rgpInstate);
            this.pnllist.Controls.Add(this.labelControl2);
            this.pnllist.Controls.Add(this.lueDeptList);
            this.pnllist.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnllist.Location = new System.Drawing.Point(2, 2);
            this.pnllist.Name = "pnllist";
            this.pnllist.Size = new System.Drawing.Size(1026, 47);
            this.pnllist.TabIndex = 5;
            // 
            // chkduty
            // 
            this.chkduty.Location = new System.Drawing.Point(426, 10);
            this.chkduty.Name = "chkduty";
            this.chkduty.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkduty.Properties.Appearance.Options.UseFont = true;
            this.chkduty.Properties.Caption = "分管";
            this.chkduty.Size = new System.Drawing.Size(64, 24);
            this.chkduty.TabIndex = 8;
            this.chkduty.Visible = false;
            this.chkduty.CheckedChanged += new System.EventHandler(this.chkduty_CheckedChanged);
            // 
            // pnloutdate
            // 
            this.pnloutdate.Controls.Add(this.btnSearch);
            this.pnloutdate.Controls.Add(this.dteendate);
            this.pnloutdate.Controls.Add(this.labelControl3);
            this.pnloutdate.Controls.Add(this.dtebegindate);
            this.pnloutdate.Location = new System.Drawing.Point(512, 6);
            this.pnloutdate.Name = "pnloutdate";
            this.pnloutdate.Size = new System.Drawing.Size(432, 31);
            this.pnloutdate.TabIndex = 7;
            this.pnloutdate.Visible = false;
            // 
            // btnSearch
            // 
            this.btnSearch.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.Appearance.Options.UseFont = true;
            this.btnSearch.Location = new System.Drawing.Point(248, 0);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 28);
            this.btnSearch.TabIndex = 3;
            this.btnSearch.Text = "查询";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // dteendate
            // 
            this.dteendate.EditValue = null;
            this.dteendate.Location = new System.Drawing.Point(131, 3);
            this.dteendate.Name = "dteendate";
            this.dteendate.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dteendate.Properties.Appearance.Options.UseFont = true;
            this.dteendate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteendate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteendate.Size = new System.Drawing.Size(100, 23);
            this.dteendate.TabIndex = 2;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Location = new System.Drawing.Point(113, 6);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(14, 17);
            this.labelControl3.TabIndex = 1;
            this.labelControl3.Text = "至";
            // 
            // dtebegindate
            // 
            this.dtebegindate.EditValue = null;
            this.dtebegindate.Location = new System.Drawing.Point(5, 3);
            this.dtebegindate.Name = "dtebegindate";
            this.dtebegindate.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtebegindate.Properties.Appearance.Options.UseFont = true;
            this.dtebegindate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtebegindate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dtebegindate.Size = new System.Drawing.Size(100, 23);
            this.dtebegindate.TabIndex = 0;
            // 
            // rgpInstate
            // 
            this.rgpInstate.Location = new System.Drawing.Point(275, 5);
            this.rgpInstate.Name = "rgpInstate";
            this.rgpInstate.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rgpInstate.Properties.Appearance.Options.UseFont = true;
            this.rgpInstate.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "在院"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "出院")});
            this.rgpInstate.Size = new System.Drawing.Size(145, 32);
            this.rgpInstate.TabIndex = 6;
            this.rgpInstate.SelectedIndexChanged += new System.EventHandler(this.rgpInstate_SelectedIndexChanged);
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Location = new System.Drawing.Point(6, 13);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(40, 16);
            this.labelControl2.TabIndex = 5;
            this.labelControl2.Text = "科室:";
            // 
            // lueDeptList
            // 
            this.lueDeptList.EditValue = "";
            this.lueDeptList.Location = new System.Drawing.Point(52, 10);
            this.lueDeptList.Name = "lueDeptList";
            this.lueDeptList.Properties.Appearance.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lueDeptList.Properties.Appearance.Options.UseFont = true;
            this.lueDeptList.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueDeptList.Properties.NullText = "";
            this.lueDeptList.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lueDeptList.Size = new System.Drawing.Size(203, 25);
            this.lueDeptList.TabIndex = 1;
            this.lueDeptList.EditValueChanged += new System.EventHandler(this.lueDeptList_EditValueChanged);
            // 
            // frmPatientList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1030, 683);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlPatientList);
            this.Name = "frmPatientList";
            this.Text = "frmPatientList";
            this.Load += new System.EventHandler(this.frmPatientList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grcPatientList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPatientList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlPatientList)).EndInit();
            this.pnlPatientList.ResumeLayout(false);
            this.pnlPatientList.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnllist)).EndInit();
            this.pnllist.ResumeLayout(false);
            this.pnllist.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkduty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnloutdate)).EndInit();
            this.pnloutdate.ResumeLayout(false);
            this.pnloutdate.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dteendate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteendate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtebegindate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtebegindate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgpInstate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueDeptList.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private DevExpress.XtraEditors.PanelControl pnlPatientList;
        private DevExpress.XtraGrid.GridControl grcPatientList;
        private DevExpress.XtraGrid.Views.Grid.GridView gvPatientList;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private Neusoft.SOC.Local.EMR.ZDLY.Controls.emrFindText emrFindText1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel1;
        private DevExpress.XtraEditors.PanelControl pnlMain;
        private DevExpress.XtraEditors.PanelControl pnllist;
        private DevExpress.XtraEditors.LookUpEdit lueDeptList;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.RadioGroup rgpInstate;
        private DevExpress.XtraEditors.PanelControl pnloutdate;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraEditors.DateEdit dteendate;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.DateEdit dtebegindate;
        private DevExpress.XtraEditors.CheckEdit chkduty;
    }
}