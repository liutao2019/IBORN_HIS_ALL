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
    public partial class ucOutputStaticByTarget : Base.BaseReport
    {
        public ucOutputStaticByTarget()
        {
            InitializeComponent();

            this.PriveClassTwos = "0320";
            this.MainTitle = "药品出库统计表";
            this.RightAdditionTitle = "";

            this.SQLIndexs = "SOC.Pharmacy.Report.Output.StaticByTarget";

            this.IsNeedDetailData = true;
            this.DetailDataQueryType = EnumQueryType.汇总条件活动行;
            this.DetailSQLIndexs = "SOC.Pharmacy.Report.Output.StaticByTargetAndBill";
            this.QueryConditionColIndexs = "0";
        }
    }
}
