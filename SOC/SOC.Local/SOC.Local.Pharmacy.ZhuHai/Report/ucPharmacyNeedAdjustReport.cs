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
    public partial class ucPharmacyNeedAdjustReport:FS.SOC.Local.Pharmacy.Base.BaseReport
    {
        #region SQL
 //        select bb.custom_code 药品代码,
 //      bb.trade_name 项目中文,
 //      bb.specs 规格,
 //      cc.store_sum 调价数量,
 //      cc.pre_retail_price 原价,
 //      round(cc.store_sum * cc.pre_retail_price / bb.pack_qty,2) 原总金额,
 //      cc.retail_price 现零售价,
 //      round(cc.store_sum * cc.retail_price / bb.pack_qty,2) 现总金额,
 //      round(cc.store_sum * cc.retail_price / bb.pack_qty,2) -
 //      round(cc.store_sum * cc.pre_retail_price / bb.pack_qty,2) 差额
 // from pha_com_adjustpricedetail cc, pha_com_baseinfo bb
 //where cc.drug_code = bb.drug_code
 //  and cc.current_state = '1'
 //  and cc.inure_time >=
 //      to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')
 //  and cc.inure_time <
 //      to_date('{2}', 'yyyy-mm-dd hh24:mi:ss')
 //  and cc.drug_dept_code = '{0}'
        #endregion
        public ucPharmacyNeedAdjustReport()
        {
            InitializeComponent();
            this.SQLIndexs = "Pharmacy.NewReport.Adjust.PharmacyNeedAdjustReport";
            //this.PriveClassTwos = "0320";
            this.MainTitle = "药品调价监控表";
            //this.DeptType = "P";
            this.PriveClassTwos = "0304";
            this.SortColIndexs = "0,1,2,3,4,5,6,7,8,9,10";
            this.RightAdditionTitle = string.Empty;
            this.IsDeptAsCondition = false;
            this.IsUseCustomType = true;
            this.CustomTypeSQL = @"select 0.1 id,0.1 name,0.1 memo,0.1 spell_code,0.1,0.1 input_code from dual";
            this.cmbCustomType.Text = "0.1";
            this.cmbCustomType.Tag = "0.1";
        }
    }
}
