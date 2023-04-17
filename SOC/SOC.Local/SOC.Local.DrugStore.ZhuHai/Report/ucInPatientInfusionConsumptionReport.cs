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
    public partial class ucInPatientInfusionConsumptionReport : FS.SOC.Local.Pharmacy.Base.BaseReport
    {
        /// <summary>
        /// 住院大输液消耗报表
        /// </summary>
        public ucInPatientInfusionConsumptionReport()
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
            this.SQLIndexs = "Pharmacy.NewReport.InPatientDrugStore.InfusionConsumptionReport";
            this.IsNeedDetailData = true;
            this.DetailSQLIndexs = "Pharmacy.NewReport.InPatientDrugStore.InfusionConsumptionReport";
            this.QueryConditionColIndexs = "1";
            this.MainTitle = "住院大输液消耗报表";
            this.PriveClassTwos = "0320";
            this.RightAdditionTitle = "";
            this.IsTimeAsCondition = true;
            this.CustomTypeShowType = "病房：";
            this.IsUseCustomType = true;
            this.CustomTypeSQL = @"select 'ALL' id, '全部' name,'ALL' memo,'ALL' spell_code,'ALL','0' input_code from dual union select d.dept_code,d.dept_name,'',d.spell_code,d.wb_code,d.dept_code input_code from com_department d where d.dept_type = 'N' order by input_code ";
            this.fpSpread1_Sheet1.Rows.Default.Height = 30F;
            this.fpSpread1_Sheet1.Rows.Default.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Bold);
            this.SumColIndexs = "6";
            this.RightAdditionTitle = "执行配药师：          核对护士：";

        }

        protected override int OnQuery(object sender, object neuObject)
        {
            base.OnQuery(sender, neuObject);
            this.lbMainTitle.Text = this.cmbCustomType.Text + "住院大输液消耗报表";
            return 1;
        }

        public override int Print(object sender, object neuObject)
        {
            FS.SOC.HISFC.BizLogic.Pharmacy.Check checkMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.Check();
            string strSql = @"select --fun_get_dept_name(t.drug_storage_code) 科室名称,
       b.custom_code 自定义码,
       b.trade_name 药品名称,
       b.specs 规格,
       round(sum(t.out_num) / b.pack_qty, 2) 数量,
       B.PACK_UNIT 单位,
       B.RETAIL_PRICE 单价,
       SUM(T.SALE_COST) 金额
  from pha_com_output t, pha_com_baseinfo b
 where t.drug_dept_code = '{0}'
   and b.drug_code = t.drug_code
   and t.out_date >=
       to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')
   and t.out_date < to_date('{2}', 'yyyy-mm-dd hh24:mi:ss')
   and (t.drug_storage_code = '{3}' or 'ALL' = '{3}')
   and t.class3_meaning_code in ('Z1', 'Z2')
   and t.drug_quality = 'T'
 group by t.drug_storage_code,
          t.drug_code,
          t.drug_dept_code,
          b.custom_code,
          b.trade_name,
          B.RETAIL_PRICE,
          b.specs,
          b.pack_qty,
          B.PACK_UNIT
          order by b.specs";
            DataSet ds = new DataSet();
            strSql = string.Format(strSql, this.cmbDept.Tag, this.dtStart.Value, this.dtEnd.Value, this.cmbCustomType.Tag);
            int param = checkMgr.ExecQuery(strSql, ref ds);
            DataTable dt = ds.Tables[0] as DataTable;
            if (dt == null || dt.Rows.Count == 0)
            {
                MessageBox.Show("没有要打印的信息");
                return 0;
            }

            FS.SOC.Local.DrugStore.ZhuHai.Report.ReportPrint.ucInPatientInfusionConsumptionReportPrint ucInPatientInfusionConsumptionReportPrint = new FS.SOC.Local.DrugStore.ZhuHai.Report.ReportPrint.ucInPatientInfusionConsumptionReportPrint();
            FS.SOC.Local.Pharmacy.Base.PrintBill printBill = new FS.SOC.Local.Pharmacy.Base.PrintBill();
            printBill.PageSize.Height = 550;
            printBill.PageSize.Width = 870;
            printBill.PageSize.ID = "Letter";
            printBill.RowCount = 13;
            ucInPatientInfusionConsumptionReportPrint.printBill = printBill;
            ucInPatientInfusionConsumptionReportPrint.SetPrintData(ds.Tables[0], this.cmbDept.Tag.ToString(),this.dtStart.Value, this.dtEnd.Value,this.cmbCustomType.Tag.ToString());

            return 1;
        }
    }
}
