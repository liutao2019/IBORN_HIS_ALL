using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FS.Report.MET.MetCas
{
    public partial class ucMetCasIpbDynamic : FSDataWindow.Controls.ucQueryBaseForDataWindow
    {
        public ucMetCasIpbDynamic()
        {
            InitializeComponent();
        }
        private string reportCode = string.Empty;
        private string reportName = string.Empty;
        protected override void OnLoad()
        {
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
            if (cboReportCode.Items.Count >= 0)
            {
                cboReportCode.SelectedIndex = 0;
            reportCode = ((FS.HISFC.Models.Base.Const)cboReportCode.Items[0]).ID;
            reportName = ((FS.HISFC.Models.Base.Const)cboReportCode.Items[0]).Name ;
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

            return base.OnRetrieve(this.dtpBeginTime.Value, this.dtpEndTime.Value, reportCode, reportName);

        }

        private void cboReportCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboReportCode.SelectedIndex >-1)
            {
                reportCode = ((FS.HISFC.Models.Base.Const)cboReportCode.Items[this.cboReportCode.SelectedIndex]).ID;
                reportName = ((FS.HISFC.Models.Base.Const)cboReportCode.Items[this.cboReportCode.SelectedIndex]).Name ;
            }
        }
    }
}

