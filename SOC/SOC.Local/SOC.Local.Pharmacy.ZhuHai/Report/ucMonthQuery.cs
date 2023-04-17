using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.Local.Pharmacy.ZhuHai.Report
{
    public partial class ucMonthQuery : FS.SOC.Local.Pharmacy.Base.BaseReport
    {
        public ucMonthQuery()
        {
            InitializeComponent();

            this.PriveClassTwos = "0310,0320";
            this.MainTitle = "药品月结汇总表";
            this.RightAdditionTitle = "";
            this.SQLIndexs = "SOC.Pharmacy.Report.Month.Static";
            this.SumColIndexs = "2,3,4,5,6,7,8,9,10,11,12,13,14,15";
            this.IsNeedDetailData = true;
            this.SumDetailColIndexs = "2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26";
            this.DetailSQLIndexs = "SOC.Pharmacy.Report.Month.StaticDetail";
            this.DetailDataQueryType = EnumQueryType.汇总条件活动行;
            this.QueryConditionColIndexs = "0";
        }
    }
}
