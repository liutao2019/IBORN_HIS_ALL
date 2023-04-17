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
    public partial class ucInputStaticByCompany : Base.BaseReport
    {
        public ucInputStaticByCompany()
        {
           
            InitializeComponent();

            this.PriveClassTwos = "0310";
            this.MainTitle = "药品入库统计表";
            this.RightAdditionTitle = "";

            this.SQLIndexs = "SOC.Pharmacy.Report.Input.StaticByCompany";

            this.IsNeedDetailData = true;
            this.DetailDataQueryType = EnumQueryType.汇总条件活动行;
            this.DetailSQLIndexs = "SOC.Pharmacy.Report.Input.StaticByCompanyAndBill";
            this.QueryConditionColIndexs = "0";
        }          
    }
}
