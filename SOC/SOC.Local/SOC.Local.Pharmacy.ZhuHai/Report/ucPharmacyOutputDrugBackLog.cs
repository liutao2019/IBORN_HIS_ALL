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
    public partial class ucPharmacyOutputDrugBackLog : FS.SOC.Local.Pharmacy.Base.BaseReport
    {
        #region SQL
 //select fun_get_dept_name(t.dept_code) 申请药房,
 //      t.apply_billcode 申请单号,
 //      b.trade_name 药品名称,
 //      b.specs 药品规格,
 //      round(t.apply_num / t.pack_qty, 2) 药房申领数量,
 //      b.pack_unit 单位,
 //      a.store_sum / a.pack_qty || a.pack_unit 库存量
 // from pha_com_stockinfo a, pha_com_applyout t, pha_com_baseinfo b
 //where a.drug_dept_code = '{0}'
 //  and t.drug_dept_code = a.drug_dept_code
 //  and a.drug_code = b.drug_code
 //  and t.apply_date >=
 //      to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')
 //  and t.apply_date <
 //      to_date('{2}', 'yyyy-mm-dd hh24:mi:ss')
 //  and t.apply_state = '2'
 //  and t.drug_code = a.drug_code
 //  and t.valid_state = '1'
 //  and a.store_sum = 0
 //order by t.apply_billcode, b.custom_code
        #endregion
        public ucPharmacyOutputDrugBackLog()
        {
            InitializeComponent();
            this.SQLIndexs = "Pharmacy.NewReport.InOut.PharmacyOutputDrugBackLog";
            //this.PriveClassTwos = "0320";
            this.MainTitle = "中山大学附属第五医院积压药品报表";
            this.IsTimeAsCondition = false;
            //this.DeptType = "P";
            this.PriveClassTwos = "0320";
            this.SortColIndexs = "0,1,2,3,4,5,6";
            this.RightAdditionTitle = string.Empty;
            this.IsUseCustomType = true;
            this.CustomTypeShowType = "天数：";
            this.cmbCustomType.Text = "90";
            this.CustomTypeSQL = string.Empty;
        }
    }
}
