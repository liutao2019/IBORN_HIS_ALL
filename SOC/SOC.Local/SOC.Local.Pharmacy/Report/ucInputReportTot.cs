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
    public partial class ucInputReportTot : FS.SOC.Local.Pharmacy.Base.ucPrivePowerReport
    {
        public ucInputReportTot()
        {
            InitializeComponent();
            this.SQLIndexs = "Pharmacy.NewReport.In.Tot";
            this.PriveClassTwos = "0310";
            this.MainTitle = "入库汇总查询";
            this.RightAdditionTitle = "";
            this.ShowTypeName = myDeptType.单位;
            this.IsUseCustomType = true;
            this.CustomTypeSQL = "select c.class3_code,c.class3_name,c.class3_meaning_code,c.class3_code,'','' from com_priv_class3 c where c.class2_code = '0310'";
            this.SumColIndexs = "3,4";
            this.IsNeedDetailData = true;
            this.DetailSQLIndexs = "Pharmacy.NewReport.In.Tot.Detail";
            this.DetailDataQueryType = EnumQueryType.活动行取参数;
            this.QueryConditionColIndexs = "1,5";
            this.SumDetailColIndexs = "12,13,14";
            this.SortColIndexs = "0,1,2,3,4,5,6,7,8,9,10,11,12,13";
        }

        //private void ucInputReportTot_Load(object sender, EventArgs e)
        //{
        //    this.SQLIndexs = "Pharmacy.NewReport.In.Tot";
        //    this.PriveClassTwos = "0310";
        //    this.MainTitle = "入库汇总查询";
        //    this.RightAdditionTitle = "";
        //    this.ShowTypeName = myDeptType.单位;

        //    this.Init();
        //}
    }
}
