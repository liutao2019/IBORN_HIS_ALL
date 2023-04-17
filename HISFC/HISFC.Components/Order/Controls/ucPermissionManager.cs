using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Order.Controls
{
    /// <summary>
    /// 医嘱授权管理
    /// </summary>
    public partial class ucPermissionManager : UserControl, FS.FrameWork.WinForms.Forms.IMaintenanceControlable
    {
        public ucPermissionManager()
        {
            InitializeComponent();
        }

        #region 变量

        private FarPoint.Win.Spread.CellType.ComboBoxCellType cmbDept = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
        private FarPoint.Win.Spread.CellType.ComboBoxCellType cmbDoc = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
        private FS.HISFC.BizLogic.Order.Permission manager = new FS.HISFC.BizLogic.Order.Permission();

        private string inpatientno = "";

        /// <summary>
        /// 住院流水号
        /// </summary>
        /// <returns></returns>
        public string InpatientNo
        {
            set
            {
                if (value == null || value == "")
                {
                    inpatientno = "";
                    return;
                }
                inpatientno = value;
                this.ucPatient1.PatientInfo = CacheManager.RadtIntegrate.GetPatientInfomation(value);
                this.Retrieve();
            }
        }
        #endregion

        private void neuSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            try
            {
                ucPermissionInput u = new ucPermissionInput();
                u.Permission = this.neuSpread1.Sheets[0].ActiveRow.Tag as FS.HISFC.Models.Order.Consultation;

                this.ucPatient1.PatientInfo = CacheManager.RadtIntegrate.GetPatientInfomation(u.Permission.InpatientNo);
                FS.FrameWork.WinForms.Classes.Function.PopForm.Text = "授权";
                FS.FrameWork.WinForms.Classes.Function.PopForm.MaximizeBox = false;
                FS.FrameWork.WinForms.Classes.Function.PopShowControl(u);

                this.Retrieve();

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public int Retrieve()
        {
            ArrayList al = new ArrayList();
            if (string.IsNullOrEmpty(this.ucQueryInpatientNo1.Text))
            {
                al = manager.QueryPermissionByDoct(this.manager.Operator.ID);
                this.ucPatient1.PatientInfo = null;
            }
            else
            {
                this.inpatientno = "";

                if (ucPatient1.PatientInfo != null)
                {
                    inpatientno = this.ucPatient1.PatientInfo.ID;
                }
                al = manager.QueryPermission(this.inpatientno);
            }

            if (al == null)
            {
                MessageBox.Show(manager.Err);
                return -1;
            }
            
            this.neuSpread1_Sheet1.RowCount = al.Count;
            for (int i = 0; i < al.Count; i++)
            {
                FS.HISFC.Models.Order.Consultation permission = al[i] as FS.HISFC.Models.Order.Consultation;
                if (permission == null)
                {
                    MessageBox.Show("错误！");
                    return -1;
                }

                this.neuSpread1.Sheets[0].Cells[i, 0].Value = CacheManager.RadtIntegrate.GetPatientInfomation(permission.InpatientNo).Name;
                this.neuSpread1.Sheets[0].Cells[i, 1].Value = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(permission.DeptConsultation.ID);
                this.neuSpread1.Sheets[0].Cells[i, 2].Value = permission.DoctorConsultation.Name;
                this.neuSpread1.Sheets[0].Cells[i, 3].Value = permission.BeginTime;
                this.neuSpread1.Sheets[0].Cells[i, 4].Value = permission.EndTime;
                this.neuSpread1.Sheets[0].Cells[i, 5].Value = permission.Name;
                this.neuSpread1.Sheets[0].Cells[i, 6].Value = permission.User01;
                this.neuSpread1.Sheets[0].Cells[i, 7].Value = permission.User02;
                this.neuSpread1.Sheets[0].Rows[i].Tag = permission;
            }
            return 0;
        }

        private void ucPermissionManager_Load(object sender, EventArgs e)
        {
            //
            // 屏蔽不用的患者信息
            //
            this.ucPatient1.S5_Birthday = false;
            this.ucPatient1.S6_Cautioner = false;
            this.ucPatient1.S7_PayKind = false;
            this.ucPatient1.S8_MoneyAlert = false;
            this.ucPatient1.Sa_CautionMoney = false;
            this.ucPatient1.Sb_Bill = false;
            this.ucPatient1.Sc_Available = false;
            this.ucPatient1.Sd_AttendingDoctor = false;
            this.ucPatient1.Se_AdmittingDoctor = false;
            this.ucPatient1.Sf_AdmittingNurse = false;
            this.ucPatient1.Sg_ThisBill = false;

            //
            // 屏蔽不用的按钮
            //
            this.queryForm.ShowExportButton = false;
            this.queryForm.ShowImportButton = false;
            this.queryForm.ShowSaveButton = false;
            this.queryForm.ShowPrintPreviewButton = false;
            this.queryForm.ShowPrintButton = false;
        }

        void ucQueryInpatientNo1_myEvent()
        {
            if (this.ucQueryInpatientNo1.Text == "" )
            {
                this.InpatientNo = "";
                this.ucPatient1.PatientInfo = null;
                MessageBox.Show("没有查到患者！");
            }
            else
            {
                this.InpatientNo = this.ucQueryInpatientNo1.InpatientNo;
            }
        }

        #region IMaintenanceControlable 成员

        int FS.FrameWork.WinForms.Forms.IMaintenanceControlable.Add()
        {
            try
            {
                if (/*this.inpatientno == null ||*/ this.ucQueryInpatientNo1.Text == "")
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("请输入住院号"));
                    return -1;
                }

                this.inpatientno = this.ucPatient1.PatientInfo.ID;
                ucPermissionInput u = new ucPermissionInput();
                FS.HISFC.Models.Order.Consultation permission = new FS.HISFC.Models.Order.Consultation();
                permission.PatientNo = ucPatient1.PatientInfo.PID.PatientNO;
                permission.InpatientNo = this.inpatientno;
                if (this.ucPatient1.PatientInfo.PVisit.PatientLocation.Dept.ID != ((FS.HISFC.Models.Base.Employee)this.manager.Operator).Dept.ID)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("非本科患者不能授权"));
                    return -1;
                }
                permission.BeginTime = manager.GetDateTimeFromSysDateTime();
                permission.EndTime = permission.BeginTime;
                u.Permission = permission;
                FS.FrameWork.WinForms.Classes.Function.PopForm.Text = "授权";
                FS.FrameWork.WinForms.Classes.Function.PopForm.MaximizeBox = false;
                FS.FrameWork.WinForms.Classes.Function.PopShowControl(u);
                this.ucQueryInpatientNo1.Text = "";
                this.Retrieve();
            }
            catch { }
            return 0;
        }

        int FS.FrameWork.WinForms.Forms.IMaintenanceControlable.Copy()
        {
            return 0;
        }

        int FS.FrameWork.WinForms.Forms.IMaintenanceControlable.Cut()
        {
            return 0;
        }

        int FS.FrameWork.WinForms.Forms.IMaintenanceControlable.Delete()
        {
            try
            {
                if (this.neuSpread1.Sheets[0].ActiveRow == null)
                {
                    return -1;
                }

                FS.HISFC.Models.Order.Consultation permission = this.neuSpread1.Sheets[0].ActiveRow.Tag as FS.HISFC.Models.Order.Consultation;
                if (permission == null)
                {
                    return -1;
                }
                if (MessageBox.Show("确实要删除该授权吗?\n该操作不能撤销!", "警告", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                {
                    return 0;
                }

                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                manager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                if (this.manager.DeletePermission(permission.ID) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();;
                    MessageBox.Show(manager.Err);
                    return -1;
                }
                FS.FrameWork.Management.PublicTrans.Commit();
                this.Retrieve();
            }
            catch { }
            return 0;
        }

        int FS.FrameWork.WinForms.Forms.IMaintenanceControlable.Export()
        {
            return 0;
        }

        int FS.FrameWork.WinForms.Forms.IMaintenanceControlable.Import()
        {
            return 0;
        }

        int FS.FrameWork.WinForms.Forms.IMaintenanceControlable.Init()
        {
            return 0;
        }

        bool FS.FrameWork.WinForms.Forms.IMaintenanceControlable.IsDirty
        {
            get
            {
                return false;
            }
            set
            {
            }
        }

        int FS.FrameWork.WinForms.Forms.IMaintenanceControlable.Modify()
        {
            return 0;
        }

        int FS.FrameWork.WinForms.Forms.IMaintenanceControlable.NextRow()
        {
            return 0;
        }

        int FS.FrameWork.WinForms.Forms.IMaintenanceControlable.Paste()
        {
            return 0;
        }

        int FS.FrameWork.WinForms.Forms.IMaintenanceControlable.PreRow()
        {
            return 0;
        }

        int FS.FrameWork.WinForms.Forms.IMaintenanceControlable.Print()
        {
            return 0;
        }

        int FS.FrameWork.WinForms.Forms.IMaintenanceControlable.PrintConfig()
        {
            return 0;
        }

        int FS.FrameWork.WinForms.Forms.IMaintenanceControlable.PrintPreview()
        {
            return 0;
        }

        int FS.FrameWork.WinForms.Forms.IMaintenanceControlable.Query()
        {
            return this.Retrieve();
        }

        FS.FrameWork.WinForms.Forms.IMaintenanceForm queryForm;
        FS.FrameWork.WinForms.Forms.IMaintenanceForm FS.FrameWork.WinForms.Forms.IMaintenanceControlable.QueryForm
        {
            get
            {
                return this.queryForm;
            }
            set
            {
                this.queryForm = value;
            }
        }

        int FS.FrameWork.WinForms.Forms.IMaintenanceControlable.Save()
        {
            return 0;
        }

        #endregion
    }
}
