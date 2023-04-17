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
    public partial class ucPharmacyOutputSummaryByType : FS.SOC.Local.Pharmacy.Base.BaseReport
    {
        #region SQL
 //        select (select c.name
 //         from com_dictionary c
 //        where c.type = 'ITEMTYPE'
 //          and c.code = bb.drug_type),
 //      sum(t.approve_cost),
 //      sum(t.sale_cost)
 // from pha_com_output t, pha_com_baseinfo bb
 //where bb.drug_code = t.drug_code
 //  and t.class3_meaning_code not in ('M1', 'M2', 'Z1', 'Z2')
 //  and t.drug_dept_code = '{0}'
 //  and t.out_date >=
 //      to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')
 //  and t.out_date < to_date('{2}', 'yyyy-mm-dd hh24:mi:ss')
 //group by bb.drug_type
        #endregion
        public ucPharmacyOutputSummaryByType()
        {
            InitializeComponent();
            this.SQLIndexs = "Pharmacy.NewReport.InOut.PharmacyOutputSummaryByType";
            //this.PriveClassTwos = "0320";
            this.MainTitle = "中山大学附属第五医院药品出库汇总";
            //this.DeptType = "P";
            this.PriveClassTwos = "0320";
            this.SumColIndexs = "1,2,3";
            this.SortColIndexs = "0,1,2,3";
            this.RightAdditionTitle = string.Empty;
        }

        private string defaultPrinter = string.Empty;

        /// <summary>
        /// 是否启用本地化打印
        /// </summary>
        [Description("默认打印机名称"), Category("Print打印设置"), Browsable(true), DefaultValue(true)]
        public string DefaultPrinter
        {
            get { return defaultPrinter; }
            set { defaultPrinter = value; }
        }

        public override int Print(object sender, object neuObject)
        {
            FS.SOC.HISFC.BizLogic.Pharmacy.Check checkMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.Check();
            string strSql = @"select (select c.name
          from com_dictionary c
         where c.type = 'ITEMTYPE'
           and c.code = bb.drug_type) 药品分类,
       sum(t.approve_cost) 买入金额,
       sum(t.sale_cost) 零售金额,
       sum(t.sale_cost) - sum(t.approve_cost) 进销差额
  from pha_com_output t, pha_com_baseinfo bb
 where bb.drug_code = t.drug_code
   and t.class3_meaning_code not in ('M1', 'M2', 'Z1', 'Z2')
   and t.out_type in (select pp.class3_code from com_priv_class3 pp where pp.class2_code = '0320' and pp.class3_meaning_code <> '26')
   and t.drug_dept_code = '{0}'
   and t.out_date >=
       to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')
   and t.out_date < to_date('{2}', 'yyyy-mm-dd hh24:mi:ss')
 group by bb.drug_type";
            DataSet ds = new DataSet();
            strSql = string.Format(strSql, this.cmbDept.Tag, this.dtStart.Value, this.dtEnd.Value, this.cmbCustomType.Tag);
            int param = checkMgr.ExecQuery(strSql, ref ds);
            DataTable dt = ds.Tables[0] as DataTable;
            if (dt == null || dt.Rows.Count == 0)
            {
                MessageBox.Show("没有要打印的信息");
                return 0;
            }

            FS.SOC.Local.Pharmacy.ZhuHai.Print.ZDWY.Report.ReportPrint.ucPharmacyOutputSummaryByTypePrint ucPharmacyOutputSummaryByTypePrint = new FS.SOC.Local.Pharmacy.ZhuHai.Print.ZDWY.Report.ReportPrint.ucPharmacyOutputSummaryByTypePrint();
            FS.SOC.Local.Pharmacy.ZhuHai.Base.PrintBill printBill = new FS.SOC.Local.Pharmacy.ZhuHai.Base.PrintBill();
            printBill.PageSize.Printer = this.DefaultPrinter;
            printBill.PageSize.Height = 1100;
            printBill.PageSize.Width = 870;
            printBill.PageSize.ID = "Letter";
            printBill.RowCount = 20;
            ucPharmacyOutputSummaryByTypePrint.printBill = printBill;
            ucPharmacyOutputSummaryByTypePrint.SetPrintData(ds.Tables[0], this.dtStart.Value, this.dtEnd.Value);

            return 1;
        }
    }
}
