using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace API.GZSI.UI
{
    public partial class frmDiagnoseInput : Form
    {
        #region 属性

        /// <summary>
        /// 患者实体
        /// </summary>
        public FS.HISFC.Models.RADT.PatientInfo PatientInfo { get; set; }

        public ArrayList DiagnoseList;

        public string ID; 

        #endregion 属性

        public frmDiagnoseInput(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            InitializeComponent();

            this.PatientInfo = patientInfo;
        }

        private void frmInpatientInput_Load(object sender, EventArgs e)
        {
            if (DiagnoseList==null)
            {
                FS.HISFC.BizLogic.Manager.Constant constMgr = new FS.HISFC.BizLogic.Manager.Constant();
                // 加载诊断
                //this.cmbDiagnose.AddItems(constMgr.GetAllList(Common.Constants.GDSZSI_CODE_PREFIX + "bizh200401"));
                this.cmbDiagnose.AddItems(constMgr.GetAllList("GDSZSI_bizh200401"));
            }
            else
            {
                this.cmbDiagnose.AddItems(DiagnoseList);
            }
           

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.cmbDiagnose.SelectedItem == null)
            {
                FS.FrameWork.WinForms.Classes.Function.Msg("请选择诊断！",111);
                return;
            }

            if (DiagnoseList == null)
            {
                FS.HISFC.Models.Base.Const diag = this.cmbDiagnose.SelectedItem as FS.HISFC.Models.Base.Const;
                if (diag == null)
                {
                    FS.FrameWork.WinForms.Classes.Function.Msg("请选择正确诊断！", 111);
                    return;
                }
                this.PatientInfo.Diagnoses = new System.Collections.ArrayList();
                FS.HISFC.Models.RADT.Diagnose diagTemp = new FS.HISFC.Models.RADT.Diagnose();
                diagTemp.ID = diag.ID;
                diagTemp.Name = diag.Name;
                diagTemp.ICD10.ID = diag.ID;
                diagTemp.ICD10.Name = diag.Name;

                this.PatientInfo.Diagnoses.Add(diagTemp); 
            }
            else
            {
                ID = this.cmbDiagnose.SelectedItem.ID;
            }
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

            FS.FrameWork.WinForms.Classes.Function.Msg("未选择诊断！", 111);
            
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
