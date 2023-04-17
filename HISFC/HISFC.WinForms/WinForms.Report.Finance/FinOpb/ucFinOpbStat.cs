using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.Report.Finance.FinOpb
{
    public partial class ucFinOpbStatClinicDetail : FSDataWindow.Controls.ucQueryBaseForDataWindow 
    {
        public ucFinOpbStatClinicDetail()
        {
            InitializeComponent();
        }

        private string reportCode = string.Empty;
        private string reportName = string.Empty;

        protected override void OnLoad()
        {
            this.isAcross = true;
            this.isSort = false;

            base.OnLoad();

            // 统计大类下拉列表
            //FS.HISFC.BizProcess.Integrate.Manager manager = new FS.HISFC.BizProcess.Integrate.Manager();
            //System.Collections.ArrayList constantList = manager.GetConstantList("FEECODESTAT");

            //FS.HISFC.Models.Base.Const topReport = new FS.HISFC.Models.Base.Const();
            //topReport.ID = "ALL";
            //topReport.Name = "全部";
            //topReport.SpellCode = "QB";

            //this.cboReportCode.Items.Add(topReport);

            //foreach (FS.HISFC.Models.Base.Const con in constantList)
            //{
            //    cboReportCode.Items.Add(con);
            //}

            //this.cboReportCode.alItems.Add(topReport);
            //this.cboReportCode.alItems.AddRange(constantList);

            //if (cboReportCode.Items.Count > 0)
            //{
            //    cboReportCode.SelectedIndex = 0;
            //    reportCode = ((FS.HISFC.Models.Base.Const)cboReportCode.Items[0]).ID;
            //    reportName = ((FS.HISFC.Models.Base.Const)cboReportCode.Items[0]).Name;
            //}

            // 医师下拉列表
            //System.Collections.ArrayList docList = manager.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.D);
            //FS.HISFC.Models.Base.Employee topDoc = new FS.HISFC.Models.Base.Employee();
            //topDoc.ID = "ALL";
            //topDoc.Name = "全部";
            //topDoc.SpellCode = "QB";
            //docList.Insert(0, topDoc);

            //foreach (FS.HISFC.Models.Base.Employee var in docList)
            //{

            //}
        }

        /// <summary>
        /// 检索数据
        /// </summary>
        /// <returns></returns>
        protected override int OnRetrieve(params object[] objects)
        {
            //InitializeComponent();
            //dwMain.Size = plRightTop.Size;
            //OnLoad();

            if (base.GetQueryTime() == -1)
            {
                return -1;
            }
            return base.OnRetrieve(this.dtpBeginTime.Value, this.dtpEndTime.Value);
           
        }

      
    }
}
