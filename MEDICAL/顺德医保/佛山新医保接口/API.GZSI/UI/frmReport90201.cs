using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace API.GZSI.UI
{
    public partial class frmReport90201 : Form
    {
        /// <summary>
        /// 常数管理类
        /// </summary>
        private FS.HISFC.BizLogic.Manager.Constant constMgr = new FS.HISFC.BizLogic.Manager.Constant();

        /// <summary>
        /// 定义最小日期
        /// </summary>
        private DateTime MinDate = new DateTime(1900, 01, 01); 

        private FS.HISFC.Models.RADT.PatientInfo patientInfo;
        /// <summary>
        /// 当前患者信息
        /// </summary>
        public FS.HISFC.Models.RADT.PatientInfo PatientInfo
        {
            get { return patientInfo; }
            set 
            { 
                patientInfo = value;
                if (patientInfo != null)
                {
                    this.tbPsnNO.Text = this.patientInfo.SIMainInfo.Psn_no;
                    this.tbTel.Text = this.patientInfo.PhoneHome;
                }
            }
        }

        public string fixmedins_code = "";
        public string fixmedins_name = "";
        public string psn_no = "";
        public string tel = "";
        public string geso_val = "";
        public string fetts = "";
        public string matn_type = "";
        public string matn_trt_dclaer_type = "";
        public string fpsc_no = "";
        public string last_mena_date = "";
        public string plan_matn_date = "";
        public string begndate = "";
        public string enddate = "";

        public frmReport90201()
        {
            InitializeComponent();

            this.btnOK.Click += new EventHandler(btnOK_Click);
            this.btnCancel.Click += new EventHandler(btnCancel_Click);
            this.dtpLastMenaDate.Value = MinDate;
            this.dtpPlanMatnDate.Value = MinDate;
            this.dtpBegnDate.Value = DateTime.Now.Date;
            this.dtpEndDate.Value = DateTime.Now.Date.AddMonths(6);
            this.dtpPlanMatnDate.ValueChanged += new EventHandler(dtpPlanMatnDate_ValueChanged);

            this.cmbMatnType.AddItems(constMgr.GetAllList(Common.Constants.GZSI_CODE_PREFIX + "matn_type"));
            this.cmbMatnTrtDclaerType.AddItems(constMgr.GetAllList(Common.Constants.GZSI_CODE_PREFIX + "matn_trt_dclaer_type"));
            
        }


        private void dtpPlanMatnDate_ValueChanged(object sender, EventArgs e)
        {
            if( this.dtpPlanMatnDate.Value != MinDate)
            {
                this.dtpEndDate.Value = this.dtpPlanMatnDate.Value.AddDays(30);
            }
        }

        public void btnOK_Click(object sender, EventArgs e)
        {
            if (this.GetValue() < 0)
            {
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        public void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        public int GetValue()
        {
            if (string.IsNullOrEmpty(this.tbFixmedinsCode.Text))
            {
                MessageBox.Show("定点医疗机构编码不能为空");
                return -1;
            }

            if (string.IsNullOrEmpty(this.tbFixmedinsName.Text))
            {
                MessageBox.Show("定点医疗机构名称不能为空");
                return -1;
            }

            if (string.IsNullOrEmpty(this.tbPsnNO.Text))
            {
                MessageBox.Show("人员编码不能为空");
                return -1;
            }

            if (string.IsNullOrEmpty(this.tbTel.Text))
            {
                MessageBox.Show("电话不能为空");
                return -1;
            }

            if (this.cmbMatnType.Tag == null || string.IsNullOrEmpty(this.cmbMatnType.Tag.ToString()))
            {
                MessageBox.Show("生育类别不能为空");
                return -1;
            }

            // {18ABBA5F-5755-4AEE-9C41-B8114453CE5F}
            //if (this.dtpBegnDate.Value.Date < DateTime.Now.Date)
            //{
            //    MessageBox.Show("登记日期不能小于当前日期");
            //    return -1;
            //}

            if (this.dtpEndDate.Value.Date <= this.dtpBegnDate.Value.Date)
            {
                MessageBox.Show("登记结束时间必须大于开始时间");
                return -1;
            }

            this.fixmedins_code = this.tbFixmedinsCode.Text;
            this.fixmedins_name = this.tbFixmedinsName.Text;
            this.psn_no = this.tbPsnNO.Text;
            this.tel = this.tbTel.Text;
            this.geso_val = this.tbGesoVal.Text;
            this.fetts = this.tbFetts.Text;
            this.matn_type = this.cmbMatnType.Tag.ToString();
            this.matn_trt_dclaer_type = this.cmbMatnTrtDclaerType.Tag.ToString();
            this.fpsc_no = this.tbFpscNO.Text;
            this.last_mena_date = (this.dtpLastMenaDate.Value <= MinDate ? "" : this.dtpLastMenaDate.Value.ToShortDateString());
            this.plan_matn_date = (this.dtpPlanMatnDate.Value <= MinDate ? "" : this.dtpPlanMatnDate.Value.ToShortDateString());
            this.begndate = this.dtpBegnDate.Value.ToShortDateString();
            this.enddate = this.dtpEndDate.Value.ToShortDateString();

            return 1;
        }
    }
}
