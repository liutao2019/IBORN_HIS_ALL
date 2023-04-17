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
    public partial class ucInputStaticByInvoice : Base.BaseReport
    {
        public ucInputStaticByInvoice()
        {
           
            InitializeComponent();

            this.PriveClassTwos = "0310";
            this.MainTitle = "药品入库统计表";
            this.RightAdditionTitle = "";
            this.SQLIndexs = "SOC.Pharmacy.Report.Input.StaticByInvoice";

            this.IsNeedDetailData = true;
            this.QueryConditionColIndexs = "0";
        }


    }
}
