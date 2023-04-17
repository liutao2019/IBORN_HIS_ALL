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
    public partial class ucPharmacyOutputSummaryByHospital : FS.SOC.Local.Pharmacy.Base.ucPrivePowerReport
    {
        #region SQL
 //        select (select c.name
 //         from com_dictionary c
 //        where c.type = 'ITEMTYPE'
 //          and c.code = bb.drug_type),
 //      sum(t.approve_cost),
 //      sum(t.sale_cost)
 // from pha_com_output t, pha_com_baseinfo bb
 //where bb.drug_code = t.drug_code
 //  and t.class3_meaning_code not in ('M1', 'M2', 'Z1', 'Z2')
 //  and t.drug_dept_code = '{0}'
 //  and t.out_date >=
 //      to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')
 //  and t.out_date < to_date('{2}', 'yyyy-mm-dd hh24:mi:ss')
 //group by bb.drug_type
        #endregion
        public ucPharmacyOutputSummaryByHospital()
        {
            InitializeComponent();
            this.SQLIndexs = "Pharmacy.NewReport.InOut.PharmacyOutputSummaryByHospital";
            //this.PriveClassTwos = "0320";
            this.MainTitle = "中山大学附属第五医院全院药品消耗汇总";
            this.DetailDataQueryType = EnumQueryType.同条件同步查询;
            //this.DeptType = "P";
            this.PriveClassTwos = "0320";
            this.IsNeedDetailData = false;
            this.DetailSQLIndexs = "Pharmacy.NewReport.InOut.PharmacyOutputSummaryByDept";
            this.SumColIndexs = "1,2,3";
            this.SortColIndexs = "0,1,2,3,4,5,6,7,8,9,10";
            this.RightAdditionTitle = string.Empty;
            this.MidAdditionTitle = string.Empty;
            this.IsUseCustomType = true;
            this.SumColIndexs = "5,8,10";
            this.SumDetailColIndexs = "8,10";
            this.CustomTypeSQL = @"select 'All' id,'全部' name,'All' memo,'All' spell_code,'All','0' input_code from dual";
        }
    }
}
