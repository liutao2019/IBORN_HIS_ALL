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
    public partial class ucInPatientUnReduceStockQuery : FS.SOC.Local.Pharmacy.Base.BaseReport
    {
        /// <summary>
        /// 未减库存情况查询报表
        /// </summary>
        public ucInPatientUnReduceStockQuery()
        {
            #region SQL
   //          select (select pb.custom_code
   //         from pha_com_baseinfo pb
   //        where pb.drug_code = aa.drug_code) 药品代码,
   //      aa.trade_name 药品名称,
   //      (select pb.specs
   //         from pha_com_baseinfo pb
   //        where pb.drug_code = aa.drug_code) 规格,
   //      aa.applyNum 未减库存数量,
   //      b.min_unit 单位,
   //      round(aa.applyNum * b.retail_price / b.pack_qty, 2) 金额,
   //      b.store_sum 当前库存,
   //      (select pb.custom_code
   //         from pha_com_baseinfo pb
   //        where pb.drug_code = aa.drug_code) 药品内部代码
   // from (select cc.drug_dept_code,
   //              cc.drug_code,
   //              cc.trade_name,
   //              sum(decode(cc.class3_meaning_code,
   //                         'Z1',
   //                         cc.apply_num,
   //                         -cc.apply_num)) applyNum
   //         from pha_com_applyout cc
   //        where cc.apply_date > trunc(sysdate)
   //          and cc.valid_state = '1'
   //          and cc.apply_state in ('0', '1')
   //          and cc.class3_meaning_code in ('Z1', 'Z2')
   //          and cc.drug_dept_code = '{0}'
   //        group by cc.drug_code, cc.trade_name, drug_dept_code) aa,
   //      pha_com_stockinfo b
   //where aa.drug_dept_code = b.drug_dept_code
   //  and aa.drug_code = b.drug_code
   //  and abs(aa.applyNum) > b.store_sum
            #endregion

            InitializeComponent();
            this.SQLIndexs = "Pharmacy.NewReport.InPatientDrugStore.UnReduceStockQuery";
            this.IsNeedDetailData = true;
            this.DetailSQLIndexs = "Pharmacy.NewReport.InPatientDrugStore.UnReduceStockQuery";
            this.QueryConditionColIndexs = "1";
            this.MainTitle = "住院未减库存情况查询";
            this.PriveClassTwos = "0320";
            this.SumColIndexs = "5";
            this.RightAdditionTitle = "";
            this.IsTimeAsCondition = false;
        }
    }
}
