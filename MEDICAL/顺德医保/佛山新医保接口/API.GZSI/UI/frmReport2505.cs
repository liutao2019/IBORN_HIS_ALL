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
    public partial class frmReport2505 : Form
    {
        private FS.HISFC.Models.RADT.PatientInfo patientInfo;

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
                    this.tbAddr.Text = this.patientInfo.AddressHome;
                }
            }
        }

        public string fixmedins_code = "";
        public string fixmedins_name = "";
        public string psn_no = "";
        public string tel = "";
        public string addr = "";
        public string begndate = "";
        public string enddate = "";
        public string fix_srt_no = "";
        public string memo = "";

        public frmReport2505()
        {
            InitializeComponent();
            this.btnOK.Click += new EventHandler(btnOK_Click);
            this.btnCancel.Click += new EventHandler(btnCancel_Click);
            this.dtpBegnDate.Value = DateTime.Now.Date;
            this.dtpEndDate.Value = new DateTime(2099, 12, 31);
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

            if (string.IsNullOrEmpty(this.tbFixSrtNO.Text))
            {
                MessageBox.Show("排序号不能为空");
                return -1;
            }

            if (this.dtpBegnDate.Value.Date < DateTime.Now.Date)
            {
                MessageBox.Show("定点日期不能小于当前日期");
                return -1;
            }

            if (this.dtpEndDate.Value.Date <= this.dtpBegnDate.Value.Date)
            {
                MessageBox.Show("定点结束时间必须大于开始时间");
                return -1;
            }

            this.fixmedins_code = this.tbFixmedinsCode.Text;
            this.fixmedins_name = this.tbFixmedinsName.Text;
            this.psn_no = this.tbPsnNO.Text;
            this.tel = this.tbTel.Text;
            this.addr = this.tbAddr.Text;
            this.begndate = this.dtpBegnDate.Value.ToShortDateString();
            this.enddate = this.dtpEndDate.Value.ToShortDateString();
            this.fix_srt_no = this.tbFixSrtNO.Text;
            this.memo = this.tbMemo.Text;

            return 1;
        }
    }
}
