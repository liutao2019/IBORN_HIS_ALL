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
    public partial class ucPharmacyInputSummaryByDrug:FS.SOC.Local.Pharmacy.Base.BaseReport
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
        public ucPharmacyInputSummaryByDrug()
        {
            InitializeComponent();
            this.CustomTypeSQL = @"select bb.drug_code,bb.trade_name,bb.custom_code,bb.spell_code,bb.wb_code,'' from pha_com_baseinfo bb";
            //this.PriveClassTwos = "0320";
            this.MainTitle = "中山大学附属第五医院药品入库汇总（按药品）(含外退)";
            //this.DeptType = "P";
            this.PriveClassTwos = "0310";
            this.SumColIndexs = "4,6,7,8";
            this.SortColIndexs = "0,1,2,3,4,5,6,7,8,9";
            this.RightAdditionTitle = string.Empty;
            this.IsUseCustomType = true;
            this.CustomTypeShowType = "药品";
            this.SQLIndexs = "Pharmacy.NewReport.InOut.PharmacyInputSummaryByDrug";

        }
    }
}
