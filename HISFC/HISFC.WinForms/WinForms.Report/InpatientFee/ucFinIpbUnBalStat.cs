using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FS.WinForms.Report.InpatientFee
{
    public partial class ucFinIpbUnBalStat : Report.Common.ucQueryBaseForDataWindow
    {
        public ucFinIpbUnBalStat()
        {
            InitializeComponent();
        }
        private string reportCode = string.Empty;
        private string reportName = string.Empty;
        protected override void OnLoad()
        {
            this.Init();

            base.OnLoad();
            //����ʱ�䷶Χ
            DateTime now = DateTime.Now;
            DateTime dt = new DateTime(DateTime.Now.Year, 1, 1);
            this.dtpBeginTime.Value = dt;

            //�������
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
        /// ��������
        /// </summary>
        /// <returns></returns>
        protected override int OnRetrieve(params object[] objects)
        {

            //InitializeComponent();
            //OnLoad();

            //  dwMain.Dock = System.Windows.Forms.DockStyle.Fill;
            if (base.GetQueryTime() == -1)
            {
                return -1;
            }
            return base.OnRetrieve(this.dtpBeginTime.Value, this.dtpEndTime.Value, reportCode, reportName);
            //dwMain.Retrieve(this.dtpBeginTime.Value, this.dtpEndTime.Value, reportCode, reportName);
            //dwMain.Modify("t_date.text = 'ʱ�䷶Χ��" + this.dtpBeginTime.Value.ToString() + "��" + this.dtpEndTime.Value.ToString() + "   ͳ�Ʒ��ࣺ" + reportName + "'");
            //return 1;

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

