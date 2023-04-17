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
    public partial class ucPharmacyOutputSummaryByOutListCode : FS.SOC.Local.Pharmacy.Base.BaseReport
    {
        #region SQL
 //       select t.drug_storage_code 科室编码,
 //      fun_get_dept_name(t.drug_storage_code) 科室名称,
 //      t.out_list_code 出库单号,
 //      t.out_date 出库日期,
 //      sum(t.approve_cost) 购入金额,
 //      sum(t.trade_cost) 加成金额,
 //      sum(t.sale_cost) 零售金额,
 //      t.oper_code 操作员,
 //      t.get_person 领药人
 // from pha_com_output t, pha_com_baseinfo bb
 //where bb.drug_code = t.drug_code
 //  and t.class3_meaning_code not in ('M1', 'M2', 'Z1', 'Z2')
 //  and t.drug_dept_code = '{0}'
 //  and t.out_date >= to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')
 //  and t.out_date < to_date('{2}', 'yyyy-mm-dd hh24:mi:ss')
 //group by t.out_list_code,
 //         t.out_date,
 //         t.drug_storage_code,
 //         t.oper_code,
 //         t.get_person
 //order by t.out_list_code
        #endregion
        public ucPharmacyOutputSummaryByOutListCode()
        {
            InitializeComponent();
            this.SQLIndexs = "Pharmacy.NewReport.InOut.PharmacyOutputSummaryByOutListCode";
            //this.PriveClassTwos = "0320";
            this.MainTitle = "中山大学附属第五医院药品出库汇总(按出库单)";
            //this.DeptType = "P";
            this.PriveClassTwos = "0320";
            this.RightAdditionTitle = string.Empty;
            this.IsNeedDetailData = true;
            this.DetailSQLIndexs = "Pharmacy.NewReport.InOut.PharmacyOutputSummaryByOutListCode.Detail";
            this.DetailDataQueryType = EnumQueryType.活动行取参数;
            this.QueryConditionColIndexs = "2";
        }
    }
}
