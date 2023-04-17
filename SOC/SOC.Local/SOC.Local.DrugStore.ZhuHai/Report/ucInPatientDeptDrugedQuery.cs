using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.DrugStore.ZhuHai.Report
{
    public partial class ucInPatientDeptDrugedQuery : FS.SOC.Local.Pharmacy.Base.BaseReport
    {
        /// <summary>
        /// 肾内科配药统计
        /// </summary>
        public ucInPatientDeptDrugedQuery()
        {
            #region SQL
 //           select tt.drug_storage_code,
 //      bb.custom_code,
 //      bb.trade_name,
 //      bb.specs,
 //      round(sum(tt.out_num / bb.pack_qty), 2),
 //      bb.pack_unit,
 //      sum(tt.sale_cost)
 // from pha_com_output tt, pha_com_baseinfo bb
 //where tt.class3_meaning_code in ('Z1', 'Z2')
 //  and tt.drug_storage_code = '{0}'
 //  and tt.drug_code = bb.drug_code
 //  and (bb.phy_function1 = '{3}'
 //   or '{3}' = 'ALL')
 //  and tt.out_date >=
 //      to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')
 //  and tt.out_date <
 //      to_date('{2}', 'yyyy-mm-dd hh24:mi:ss')
 //group by tt.drug_storage_code,
 //         bb.custom_code,
 //         bb.trade_name,
 //         bb.specs,
 //         bb.pack_unit,
 //         bb.pack_qty
 //order by bb.custom_code
            #endregion

            InitializeComponent();
            this.SQLIndexs = "Pharmacy.NewReport.InPatientDrugStore.DeptDrugedQuery";
            this.DetailSQLIndexs = "Pharmacy.NewReport.InPatientDrugStore.DeptDrugedQuery";
            this.MainTitle = string.Empty;
            this.PriveClassTwos = "0320";
            this.RightAdditionTitle = "";
            this.IsTimeAsCondition = true;
            this.DeptType = "N,T";
            this.SumColIndexs = "5";
            this.CustomTypeSQL = "";
            this.CustomTypeShowType = "药性：";
            this.CustomTypeSQL = "select 'ALL' id, '全部' name, '00' memo, 'ALL' spell_code, 'ALL', '' from dual union select tt.node_code id,tt.node_name name,tt.node_code memo,fun_get_querycode(tt.node_name,1) spell_code,fun_get_querycode(tt.node_name,0) wb_code,'' from pha_com_function tt where tt.grade_code = '2' order by memo ";
            this.IsUseCustomType = true;
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            base.OnQuery(sender, neuObject);
            this.lbMainTitle.Text = this.cmbDept.Text + "配药统计" + "(" + this.cmbCustomType.Text + ")";
            return 1;
        }
    }
}
