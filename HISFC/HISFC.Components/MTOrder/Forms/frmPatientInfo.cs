using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.MTOrder.Forms
{
    public partial class frmPatientInfo : Form
    {
        public frmPatientInfo()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 已选择的预约信息
        /// </summary>
        public Models.MedicalTechnology.Appointment Appoint { get; set; }

        private void frmPatientInfo_Load(object sender, EventArgs e)
        {
            if (Appoint == null)
            {
                MessageBox.Show("载入病人信息出错", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            #region 赋值
            this.txtAppID.Text = Appoint.ID;
            this.txtAppID.Tag = Appoint;
            this.txtStatues.Text = Appoint.ApplyStatus.ToString();
            this.txtOrderNo.Text = Appoint.OrderNo;
            this.txtOrderType.Text = ApplyTypeConverter(Appoint.OrderType);
            this.txtMT.Text = Appoint.MTName;
            this.txtMTType.Text = Appoint.TypeName;
            this.txtItemName.Text = Appoint.ItemName;
            this.txtDoctor.Text = Appoint.DoctName;
            this.txtDeptName.Text = Appoint.DeptName;
            this.txtExecDoctor.Text = Appoint.ExecDoctName;
            this.txtExecDept.Text = Appoint.ExecDeptName;
            this.txtApplyTime.Text = Appoint.ApplyDate.ToString("yyyy-MM-dd HH:mm:ss");
            this.txtArrangeDate.Text = Appoint.ArrangeDate.ToString("yyyy-MM-dd");
            this.txtArrangeTime.Text = Appoint.ArrangeTime;
            this.txtCardNo.Text = Appoint.CardNo;
            this.txtClinicNo.Text = Appoint.ClinicCode;
            this.txtName.Text = Appoint.Name;
            this.txtSex.Text = Appoint.Sex;
            this.txtTelephone.Text = Appoint.Telephone;
            this.txtDiagnosis.Text = Appoint.Diagnosis;
            this.txtMedicalHistory.Text = Appoint.MedicalHistory;
            #endregion
        }
        /// <summary>
        /// 将OrderType转成中文
        /// </summary>
        /// <param name="ApplyType"></param>
        /// <returns></returns>
        private string ApplyTypeConverter(FS.HISFC.Models.MedicalTechnology.EnumApplyType ApplyType)
        {
            switch (ApplyType)
            {
                case Models.MedicalTechnology.EnumApplyType.Clinic: return "门诊";
                case Models.MedicalTechnology.EnumApplyType.Hospital: return "住院";
            }
            return string.Empty;
        }
    }
}
