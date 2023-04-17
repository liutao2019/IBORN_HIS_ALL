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
    public partial class ucPharmacyAdjustDetail:FS.SOC.Local.Pharmacy.Base.BaseReport
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
        public ucPharmacyAdjustDetail()
        {
            InitializeComponent();
            this.SQLIndexs = "Pharmacy.NewReport.Adjust.PharmacyAdjustDetail";
            //this.PriveClassTwos = "0320";
            this.MainTitle = "药房药品调价损益统计表";
            //this.DeptType = "P";
            this.PriveClassTwos = "0304";
            this.SumColIndexs = "9,11,12";
            this.SortColIndexs = "0,1,2,3,4,5,6,7,8,9,10,11,12";
            this.RightAdditionTitle = string.Empty;
        }
    }
}
