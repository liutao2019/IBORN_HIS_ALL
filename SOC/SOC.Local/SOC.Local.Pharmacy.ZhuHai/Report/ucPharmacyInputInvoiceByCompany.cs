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
    public partial class ucPharmacyInputInvoiceByCompany:FS.SOC.Local.Pharmacy.Base.BaseReport
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
  
        public ucPharmacyInputInvoiceByCompany()
        {
            InitializeComponent();
            this.SQLIndexs = "Pharmacy.NewReport.InOut.PharmacyInputInvoiceByCompany";
            //this.PriveClassTwos = "0320";
            this.MainTitle = "中山大学附属第五医院药品入库发票汇总(按供货公司)";
            //this.DeptType = "P";
            this.PriveClassTwos = "0310";
            this.IsUseCustomType = true;
            this.CustomTypeShowType = "公司";
            this.CustomTypeSQL = @"select aa.fac_code,aa.fac_name,aa.fac_code,aa.spell_code,aa.wb_code,'' from pha_com_company aa where aa.company_type = '1' order by aa.fac_code";
            this.SumColIndexs = "2";
            this.RightAdditionTitle = string.Empty;
        }

        public override int Print(object sender, object neuObject)
        {
             FS.SOC.HISFC.BizLogic.Pharmacy.Check checkMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.Check();
             string strSql = @" select  fun_get_company_name(t.company_code) 公司名称,
         t.invoice_no 发票号,
         sum(t.purchase_cost) 发票金额,
         trunc(t.invoice_date) 发票日期
          from pha_com_input t,pha_com_baseinfo b
         where  t.drug_dept_code = '{0}'
           and t.drug_code = b.drug_code
           and t.in_date >= to_date('{1}','yyyy-mm-dd hh24:mi:ss')
           and t.in_date <  to_date('{2}','yyyy-mm-dd hh24:mi:ss')         
           and t.in_type in (select pp.class3_code from com_priv_class3 pp where pp.class2_code = '0310'
           and pp.class3_meaning_code <> '1C')
           and t.company_code = '{3}'
         group by t.company_code,t.invoice_no,t.invoice_date
         order by 发票号";
             DataSet ds = new DataSet();
             strSql = string.Format(strSql,this.cmbDept.Tag,this.dtStart.Value,this.dtEnd.Value,this.cmbCustomType.Tag);
             int param = checkMgr.ExecQuery(strSql, ref ds);
             DataTable dt = ds.Tables[0] as DataTable;
             if (dt == null ||dt.Rows.Count == 0)
             {
                MessageBox.Show("没有要打印的信息");
                return 0;
             }

             FS.SOC.Local.Pharmacy.ZhuHai.Print.ZDWY.Report.ReportPrint.ucPharmacyInputInvoiceByCompanyPrint ucPharmacyInputInvoceByCompanyPrint = new FS.SOC.Local.Pharmacy.ZhuHai.Print.ZDWY.Report.ReportPrint.ucPharmacyInputInvoiceByCompanyPrint();
             FS.SOC.Local.Pharmacy.ZhuHai.Base.PrintBill printBill = new FS.SOC.Local.Pharmacy.ZhuHai.Base.PrintBill();
             printBill.PageSize.Printer = this.DefaultPrinter;
             printBill.PageSize.Height = 1150;
             printBill.PageSize.Width = 870;
             printBill.PageSize.ID = "A4";
             printBill.RowCount = 37;
             ucPharmacyInputInvoceByCompanyPrint.printBill = printBill;
             ucPharmacyInputInvoceByCompanyPrint.SetPrintData(ds.Tables[0]);

             return 1;
        }
    }
}
