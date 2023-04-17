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
    public partial class ucPharmacyAdjustTot:FS.SOC.Local.Pharmacy.Base.BaseReport
    {
        #region SQL
    //    select cc.drug_code 药品编码,
    //      cc.trade_name 药品名称,
    //      bb.custom_code 自定义码,
    //      bb.specs 规格,
    //      cc.pre_retail_price 原价,
    //      sum(round(cc.store_sum * cc.pre_retail_price / bb.pack_qty,2)) 原总金额,
    //      cc.retail_price 现零售价,
    //      sum(round(cc.store_sum * cc.retail_price / bb.pack_qty,2)) 现总金额,
    //      sum(round(cc.store_sum * cc.retail_price / bb.pack_qty,2))-sum(round(cc.store_sum * cc.pre_retail_price / bb.pack_qty,2)) 差额,
    //      cc.inure_time 调价发生时间
    // from pha_com_adjustpricedetail cc, pha_com_baseinfo bb
    //where cc.current_state = '1'
    //  and cc.inure_time >=
    //      to_date('{0}', 'yyyy-mm-dd hh24:mi:ss')
    //  and cc.inure_time <
    //      to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')
    //  and cc.drug_code = bb.drug_code
    //group by cc.drug_code,
    //         bb.custom_code,
    //         cc.trade_name,
    //         bb.specs,
    //         cc.pre_retail_price,
    //         cc.retail_price
        #endregion
        public ucPharmacyAdjustTot()
        {
            InitializeComponent();
            this.SQLIndexs = "Pharmacy.NewReport.Adjust.PharmacyAdjustTot";
            //this.PriveClassTwos = "0320";
            this.MainTitle = "药房药品调价损益统计汇总表";
            //this.DeptType = "P";
            this.PriveClassTwos = "0304";
            this.SumColIndexs = "7,8,10,11,12";
            this.SortColIndexs = "0,1,2,3,4,5,6,7,8,9,10,11,12,13,14";
            this.IsDeptAsCondition = false;
            this.IsNeedDetailData = false;
            this.DetailSQLIndexs = "Pharmacy.NewReport.Adjust.PharmacyAdjustTot.Detail";
            this.DetailDataQueryType = EnumQueryType.活动行取参数;
            this.QueryConditionColIndexs = "0,2";
            this.RightAdditionTitle = string.Empty;
            this.SumDetailColIndexs = "5,7,8";
            this.IsUseCustomType = true;
            this.CustomTypeShowType = "类型";
            this.CustomTypeSQL = @"select 'All' id,'全部' name,'','','','0' sortid from dual union select '1' id,'零售价调价' name, '', '', '', '1' sortid from dual union select '2' id,'批次调价' name, '', '', '', '2' sortid from dual order by sortid
";
        }
    }
}
