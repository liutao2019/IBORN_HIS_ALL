using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Order.Forms
{
    public partial class frmDCTreatmentType : Form
    {

        /// <summary>
        /// 当前开立患者 {d88ca0f0-6235-4a5d-b04e-4eac0f7a78e7}
        /// </summary>
        protected FS.HISFC.Models.RADT.PatientInfo myPatientInfo = null;

        /// <summary>
        /// 当前开立患者
        /// </summary>
        public FS.HISFC.Models.RADT.PatientInfo Patient
        {
            get { return this.myPatientInfo; }
            set { this.myPatientInfo = value; }
        }

        public string HealthCareObject
        {
            get
            {
                return this.cbxHealthCareType.Tag.ToString();
            }
        }




        public frmDCTreatmentType()
        {
            InitializeComponent();
        }

       
        public void Init()
        {

            System.Collections.ArrayList alHealthCareType = CacheManager.GetConList("MEDICALINSURANCEITEM");
            alHealthCareType.Add(new FS.FrameWork.Models.NeuObject());
            if (alHealthCareType == null)
            {
                MessageBox.Show("查询医保待遇类型出错！" + CacheManager.InterMgr.Err);
                return;
            }
            else if (alHealthCareType.Count == 0)
            {
                //cbxHealthCareType.Enabled = false;
            }
            else
            {
                this.cbxHealthCareType.AddItems(alHealthCareType);
            }

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            HISFC.Models.Base.Employee empl2 = FS.FrameWork.Management.Connection.Operator as HISFC.Models.Base.Employee;

            HISFC.Models.Base.Department dept2 = empl2.Dept as HISFC.Models.Base.Department;
            if (dept2.ID == "1001")
            {
                //如果登录的是妇产科就一定要检测医保待遇填写情况
                //校验医保类型
                if (this.cbxHealthCareType.Text == string.Empty)
                {
                    MessageBox.Show("医保待遇类型不能为空！");
                    return;
                }
            }
           
            this.DialogResult = DialogResult.OK;
            this.Hide();
           
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Hide();
        }
    }
}
