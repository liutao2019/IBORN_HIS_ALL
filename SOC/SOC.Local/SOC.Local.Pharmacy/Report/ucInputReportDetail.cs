using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.Local.Pharmacy.Report
{
    public partial class ucInputReportDetail : FS.SOC.Local.Pharmacy.Base.ucPrivePowerReport
    {
        public ucInputReportDetail()
        {
            InitializeComponent();
            this.ncbInDate.CheckedChanged += new EventHandler(ncbInDate_CheckedChanged);
            this.ncbInvoiceDate.CheckedChanged += new EventHandler(ncbInvoiceDate_CheckedChanged);
            this.SQLIndexs = "Pharmacy.NewReport.In.Detail";
            this.PriveClassTwos = "0310";
            this.MainTitle = "入库明细查询";
            this.RightAdditionTitle = "";
            this.ShowTypeName = myDeptType.单位;
            this.IsUseCustomType = true;
            this.CustomTypeSQL = @"select c.class3_code,c.class3_name,c.class3_meaning_code,c.class3_code,'','' from com_priv_class3 c where c.class2_code = '0320'";
            this.SumColIndexs = "12,13,14";
            this.SortColIndexs = "0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20";
        }

        void ncbInvoiceDate_CheckedChanged(object sender, EventArgs e)
        {
            this.ncbInDate.Checked = !this.ncbInvoiceDate.Checked;
        }

        void ncbInDate_CheckedChanged(object sender, EventArgs e)
        {
            this.ncbInvoiceDate.Checked = !this.ncbInDate.Checked;
        }

        //private void ucInputReport_Load(object sender, EventArgs e)
        //{
        //    this.SQLIndexs = "Pharmacy.NewReport.In.Detail";
        //    this.PriveClassTwos = "0310";
        //    this.MainTitle = "入库明细查询";
        //    this.RightAdditionTitle = "";
        //    this.ShowTypeName = myDeptType.单位;

        //    this.Init();
        //}
        protected override int OnQuery(object sender, object neuObject)
        {
            if (this.ncbInDate.Checked)
            {
                this.SQLIndexs = "Pharmacy.NewReport.In.Detail";
            }
            else
            {
                this.SQLIndexs = "Pharmacy.NewReport.In.DetailByInvoiceDate";
            }
            return base.OnQuery(sender, neuObject);
        }
    }
}
