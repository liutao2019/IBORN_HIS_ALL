using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.Local.Pharmacy.Report.SunnyDrug
{
    public partial class ucTopTenRecipes : Base.BaseReport
    {
        public ucTopTenRecipes()
        {

            InitializeComponent();

            this.PriveClassTwos = "0310";
            this.MainTitle = "金额排序前10名处方";
            this.RightAdditionTitle = "";

            this.SQLIndexs = "SOC.Pharmacy.Report.SunnyDrug.TopTenRecipe.Summry";

            this.IsNeedDetailData = true;
            this.DetailDataQueryType = EnumQueryType.汇总条件活动行;
            this.DetailSQLIndexs = "SOC.Pharmacy.Report.SunnyDrug.TopTenRecipe.Detail";
            this.QueryConditionColIndexs = "0";
            this.QueryDataWhenInit = false;
        }
    }
}
