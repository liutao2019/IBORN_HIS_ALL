using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.Pharmacy.ZhuHai.Report
{
    public partial class ucPharmacyStockSummary:FS.SOC.Local.Pharmacy.Base.BaseReport
    {
        #region SQL
         // select fun_get_company_name(b.fac_code) 单位名称,
         //sum(t.purchase_cost) 买入金额,
         //sum(t.purchase_cost) 批发金额,
         //sum(t.wholesale_cost) 零售金额,
         //sum(t.wholesale_cost) - sum(t.purchase_cost) 购零差
         // from pha_com_input t, pha_com_company b
         //where b.company_type = '1'
         //  and t.drug_dept_code = '{0}'
         //  and t.company_code = b.fac_code
         //  and t.in_date >= to_date('{1}','yyyy-mm-dd hh24:mi:ss')
         //  and t.in_date <  to_date('{2}','yyyy-mm-dd hh24:mi:ss')
         //group by b.fac_code
         //order by b.fac_code
        #endregion
        public ucPharmacyStockSummary()
        {
            InitializeComponent();
            this.SQLIndexs = "Pharmacy.NewReport.StockInfo.PharmacyStockSummary";
            //this.PriveClassTwos = "0320";
            this.MainTitle = "中山大学附属第五医院库存汇总情况（全院）";
            this.DeptType = "P,PI";
            this.IsTimeAsCondition = false;
            this.IsAllowAllDept = true;
            this.SumColIndexs = "7";
            this.SortColIndexs = "0,1,2,3,4,5,6,7,8";
            this.RightAdditionTitle = string.Empty;
            this.DetailSortColIndexs = "0,1,2,3,4,5,6,7,8";
            this.SumDetailColIndexs = "5,7,8";
            this.IsUseCustomType = true;
            this.IsNeedDetailData = true;
            this.DetailDataQueryType = EnumQueryType.同条件同步查询;
            this.DetailSQLIndexs = "Pharmacy.NewReport.StockInfo.PharmacyStockSummaryByDept";
            this.CustomTypeShowType = "药品：";
            this.CustomTypeSQL = @"select c.drug_code id,c.trade_name name,c.custom_code,fun_get_querycode(c.trade_name, 1),fun_get_querycode(c.trade_name, 0),'1' input_code from pha_com_baseinfo c order by input_code
";
        }
    }
}
