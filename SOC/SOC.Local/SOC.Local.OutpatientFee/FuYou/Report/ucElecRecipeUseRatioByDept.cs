using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.Local.OutpatientFee.FuYou.Report
{
    public partial class ucElecRecipeUseRatioByDept : FS.WinForms.Report.Common.ucQueryBaseForDataWindow
    {
        public ucElecRecipeUseRatioByDept()
        {
            InitializeComponent();
        }

        FS.FrameWork.Management.DataBaseManger db = new FS.FrameWork.Management.DataBaseManger(); 
        protected override int OnRetrieve(params object[] objects)
        {
            if (base.GetQueryTime() == -1)
            {
                MessageBox.Show("输入查询时间有误！");
                return -1;
            }
            return base.OnRetrieve(this.dtpBeginTime.Value, this.dtpEndTime.Value);
        }

        private void ucElecRecipeUseRatioByDept_Load(object sender, EventArgs e)
        {
            DateTime nowTime = this.db.GetDateTimeFromSysDateTime();

            this.dtpEndTime.Value = new DateTime(nowTime.Year, nowTime.Month, nowTime.Day, 23, 59, 59);
            this.dtpBeginTime.Value = new DateTime(nowTime.Year, nowTime.Month, nowTime.Day, 00, 00, 01);
        }
    }
}
