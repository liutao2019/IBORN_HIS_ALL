using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.Pharmacy.ZhuHai.Report.DeptOfMedcine
{
    public partial class OutPatientRecipeQuery : FS.SOC.Local.Pharmacy.Base.BaseReport
    {
        #region SQL
//         select bb.custom_code 药品代码,
//       bb.trade_name 名称,
//       bb.specs 规格,
//       bb.min_unit 小单位,
//       sum(tt.out_num) 总数量,
//       sum(tt.approve_cost) 买入金额,
//       sum(tt.sale_cost) 零售金额,
//       fun_get_dept_name(tt.drug_storage_code) 科室名称
//  from pha_com_output tt, pha_com_baseinfo bb
// where tt.drug_code = bb.drug_code
//   and tt.class3_meaning_code in ('Z1', 'Z2')
//   and tt.out_date >=
//       to_date('{0}', 'yyyy-mm-dd hh24:mi:ss')
//   and tt.out_date <
//       to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')
// group by tt.drug_code,
//          bb.trade_name,
//          bb.specs,
//          bb.min_unit,
//          bb.custom_code,
//          tt.drug_storage_code
//order by bb.custom_code
        #endregion
        public OutPatientRecipeQuery()
        {
            InitializeComponent();
            this.SQLIndexs = "Pharmacy.NewReport.DeptOfMedcine.OutPatientRecipeQuery";
            //this.PriveClassTwos = "0320";
            this.MainTitle = "门诊处方抽查";
            //this.DeptType = "P";
            this.PriveClassTwos = "0310";
            this.IsDeptAsCondition = false;
            this.MidAdditionTitle = string.Empty;
            this.RightAdditionTitle = string.Empty;
            this.SumColIndexs = "21";
            this.SortColIndexs = "0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30";
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            if ((this.dtEnd.Value - this.dtStart.Value).TotalDays > 3)
            {
                MessageBox.Show("查询时间仅限于3天内,请修改时间,谢谢！", "提示");
                return -1;
            }
            this.QueryData();
            return 1;
        }
    }
}
