using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.Report.Finance.FinIpb
{
    public partial class ucFinIpbDeptIncome : FSDataWindow.Controls.ucQueryBaseForDataWindow 
    {
        public ucFinIpbDeptIncome()
        {
            InitializeComponent();
        }
        private string reportCode = string.Empty;
        private string reportName = string.Empty;
        protected override void  OnLoad()
        {
            this.isAcross = true;
            this.isSort = false;
            this.Init();

 	        base.OnLoad();
            //设置时间范围
            DateTime now = DateTime.Now;
            DateTime dt = new DateTime(DateTime.Now.Year, 1, 1);
            this.dtpBeginTime.Value = dt;

            //填充数据
            FS.HISFC.BizProcess.Integrate.Manager manager = new FS.HISFC.BizProcess.Integrate.Manager();
            System.Collections.ArrayList constantList = manager.GetConstantList("FEECODESTAT");
            foreach (FS.HISFC.Models.Base.Const con in constantList)
            {
                cboReportCode.Items.Add(con);
            }
            if (cboReportCode.Items.Count > 0)
            {
                cboReportCode.SelectedIndex = 0;
                reportCode = ((FS.HISFC.Models.Base.Const)cboReportCode.Items[0]).ID;
                reportName = ((FS.HISFC.Models.Base.Const)cboReportCode.Items[0]).Name;
            }

        }

        /// <summary>
        /// 检索数据
        /// </summary>
        /// <returns></returns>
        protected override int OnRetrieve(params object[] objects)
        {
            
            if (base.GetQueryTime() == -1)
            {
                return -1;
            }
            //return base.OnRetrieve(this.dtpBeginTime.Value, this.dtpEndTime.Value, reportCode, reportName);
            dwMain.Retrieve(this.dtpBeginTime.Value, this.dtpEndTime.Value, reportCode, reportName);
            dwMain.Modify("t_date.text = '时间范围：" + this.dtpBeginTime.Value.ToString() + "－" + this.dtpEndTime.Value.ToString() + "   统计分类：" + reportName + "'");
                        
            return 1;

        }

        private void cboReportCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboReportCode.SelectedIndex >= 0)
            {
                reportCode = ((FS.HISFC.Models.Base.Const)cboReportCode.Items[this.cboReportCode.SelectedIndex]).ID;
                reportName = ((FS.HISFC.Models.Base.Const)cboReportCode.Items[this.cboReportCode.SelectedIndex]).Name;
            }
        }             
    }
}