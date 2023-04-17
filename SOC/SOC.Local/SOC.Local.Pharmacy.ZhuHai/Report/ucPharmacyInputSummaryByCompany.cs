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
    public partial class ucPharmacyInputSummaryByCompany:FS.SOC.Local.Pharmacy.Base.BaseReport
    {
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
   
        public ucPharmacyInputSummaryByCompany()
        {
            InitializeComponent();
            this.SQLIndexs = "Pharmacy.NewReport.InOut.PharmacyInputSummaryByDrugType";
            //this.PriveClassTwos = "0320";
            this.MainTitle = "中山大学附属第五医院药品入库汇总（药品分类）(含外退)";
            //this.DeptType = "P";
            this.PriveClassTwos = "0310";
            this.SumColIndexs = "1,2,3,4";
            this.SortColIndexs = "0,1,2,3,4";
            this.RightAdditionTitle = string.Empty;
        }

        public override int Print(object sender, object neuObject)
        {
            FS.SOC.HISFC.BizLogic.Pharmacy.Check checkMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.Check();
            string strSql = @" select  (select c.name from com_dictionary c where c.type = 'ITEMTYPE' and c.code = b.drug_type) 药品分类,
         round(sum(t.purchase_cost),2) 买入金额,
         round(sum(t.purchase_cost),2) 批发金额,
         round(sum(t.wholesale_cost),2) 零售金额,
         round(sum(t.wholesale_cost) - sum(t.purchase_cost),2) 购零差
          from pha_com_input t,pha_com_baseinfo b
         where  t.drug_dept_code = '{0}'
           and t.drug_code = b.drug_code
           and t.in_date >= to_date('{1}','yyyy-mm-dd hh24:mi:ss')
           and t.in_date <  to_date('{2}','yyyy-mm-dd hh24:mi:ss')         
           and t.in_type in (select pp.class3_code from com_priv_class3 pp where pp.class2_code = '0310'
           and pp.class3_meaning_code <> '1C')
         group by b.drug_type
          order by (select c.sort_id from com_dictionary c where c.type = 'ITEMTYPE' and c.code =  b.drug_type)";
            DataSet ds = new DataSet();
            strSql = string.Format(strSql, this.cmbDept.Tag, this.dtStart.Value, this.dtEnd.Value, this.cmbCustomType.Tag);
            int param = checkMgr.ExecQuery(strSql, ref ds);
            DataTable dt = ds.Tables[0] as DataTable;
            if (dt == null || dt.Rows.Count == 0)
            {
                MessageBox.Show("没有要打印的信息");
                return 0;
            }

            FS.SOC.Local.Pharmacy.ZhuHai.Print.ZDWY.Report.ReportPrint.ucPharmacyInputSummaryByCompanyPrint ucPharmacyInputSummaryByCompanyPrint = new FS.SOC.Local.Pharmacy.ZhuHai.Print.ZDWY.Report.ReportPrint.ucPharmacyInputSummaryByCompanyPrint();
            FS.SOC.Local.Pharmacy.ZhuHai.Base.PrintBill printBill = new FS.SOC.Local.Pharmacy.ZhuHai.Base.PrintBill();
            printBill.PageSize.Printer = this.DefaultPrinter;
            printBill.PageSize.Height = 1100;
            printBill.PageSize.Width = 870;
            printBill.PageSize.ID = "Letter";
            printBill.RowCount = 20;
            ucPharmacyInputSummaryByCompanyPrint.printBill = printBill;
            ucPharmacyInputSummaryByCompanyPrint.SetPrintData(ds.Tables[0],this.dtStart.Value,this.dtEnd.Value);

            return 1;
        }
    }
}
