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
    public partial class ucPharmacyOutputNoStorageForApply : FS.SOC.Local.Pharmacy.Base.BaseReport
    {
        #region SQL
 //select fun_get_dept_name(tt.dept_code) 申请科室,
 //      tt.apply_billcode 申请单号,
 //      cc.custom_code 药品代码,
 //      cc.trade_name 药品名称,
 //      cc.specs 药品规格,
 //      round(tt.apply_num / tt.pack_qty, 2) 药房申请数量,
 //      cc.pack_unit 单位
 // from pha_com_applyout tt, pha_com_baseinfo cc, pha_com_stockinfo bb
 //where tt.drug_dept_code = '{0}'
 //  and tt.drug_code = bb.drug_code
 //  and cc.drug_code = bb.drug_code
 //  and tt.class3_meaning_code not in ('M1','M2','Z1','Z2')
 //  and bb.store_sum = 0
 //  and tt.drug_dept_code = bb.drug_dept_code
 //  and tt.apply_date >=
 //      to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')
 //  and tt.apply_date <
 //      to_date('{2}', 'yyyy-mm-dd hh24:mi:ss')
 //  and tt.apply_state = '1'
        #endregion
        public ucPharmacyOutputNoStorageForApply()
        {
            InitializeComponent();
            this.SQLIndexs = "Pharmacy.NewReport.InOut.PharmacyOutputNoStorageForApply";
            //this.PriveClassTwos = "0320";
            this.MainTitle = "中山大学附属第五医院已申领未出库报表";
            //this.DeptType = "P";
            this.PriveClassTwos = "0320";
            this.RightAdditionTitle = string.Empty;
            this.SortColIndexs = "0,1,2,3,4,5,6";
        }
    }
}
