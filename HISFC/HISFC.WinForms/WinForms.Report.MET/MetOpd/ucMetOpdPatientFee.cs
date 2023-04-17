using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.Report.MET.MetOpd
{
    public partial class ucMetOpdPatientFee : FSDataWindow.Controls.ucQueryBaseForDataWindow
    {
        public ucMetOpdPatientFee()
        {
            InitializeComponent();
        }

        /// <summary>
        /// סԺ�շ�ҵ���
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.InPatient inpatientManager = new FS.HISFC.BizLogic.Fee.InPatient();

        protected override int OnRetrieve(params object[] objects)
        {
            if (base.GetQueryTime() == -1)
            {
                return -1;
            }

            return base.OnRetrieve(this.dtpBeginTime.Value, endTime, this.employee.Dept.ID.ToString());
        }

        private void ucMetOpdPatientFee_Load(object sender, EventArgs e)
        {
            DateTime nowTime = this.inpatientManager.GetDateTimeFromSysDateTime();
            this.dtpBeginTime.Value = new DateTime(nowTime.Year, nowTime.Month, nowTime.Day, 0, 0, 0);
            
        }
    }
}
