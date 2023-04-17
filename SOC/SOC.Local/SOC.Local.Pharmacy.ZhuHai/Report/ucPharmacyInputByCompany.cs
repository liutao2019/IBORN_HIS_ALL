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
    public partial class ucPharmacyInputByCompany:FS.SOC.Local.Pharmacy.Base.BaseReport
    {
        #region SQL
         // select fun_get_company_name(b.fac_code) 单位名称,
         //sum(t.purchase_cost) 买入金额,
         //sum(t.purchase_cost) 批发金额,
         //sum(t.wholesale_cost) 零售金额,
         //sum(t.wholesale_cost) - sum(t.purchase_cost) 购零差
         // from pha_com_input t, pha_com_company b
         //where b.company_type = '1'
         //  and t.drug_dept_code = '{0}'
         //  and t.company_code = b.fac_code
         //  and t.in_date >= to_date('{1}','yyyy-mm-dd hh24:mi:ss')
         //  and t.in_date <  to_date('{2}','yyyy-mm-dd hh24:mi:ss')
         //group by b.fac_code
         //order by b.fac_code
        #endregion
        public ucPharmacyInputByCompany()
        {
            InitializeComponent();
            this.SQLIndexs = "Pharmacy.NewReport.Fin.PharmacyInputByCompany";
            //this.PriveClassTwos = "0320";
            this.MainTitle = "中山大学附属第五医院药品入库汇总（含外退）";
            //this.DeptType = "P";
            this.PriveClassTwos = "0310";
            this.SumColIndexs = "1,2,3,4";
            this.SortColIndexs = "0,1,2,3,4";
            this.RightAdditionTitle = string.Empty;
            this.IsUseCustomType = true;
            this.CustomTypeShowType = "公司：";
            this.CustomTypeSQL = @"select 'All' id,'全部' name,'All' memo,'All' spell_code,'All','0' input_code from dual union select c.fac_code id, c.fac_name name, '', fun_get_querycode(c.fac_name,1), fun_get_querycode(c.fac_name,0), '1'input_code from pha_com_company c where c.company_type = '1' order by input_code
";
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
            string strSql = @"select nvl(fun_get_company_name(tt.company_code),fun_get_dept_name(tt.company_code)) 单位名称,
           sum(tt.purchase_cost) 买入金额,
           sum(tt.purchase_cost) 批发金额,
           sum(tt.wholesale_cost) 零售金额,
           sum(tt.wholesale_cost) -  sum(tt.purchase_cost) 购零差
      from pha_com_input tt
     where tt.in_date >=
           to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')
       and tt.in_date <
           to_date('{2}', 'yyyy-mm-dd hh24:mi:ss')
       and tt.drug_dept_code = '{0}'
       and (tt.company_code = '{3}' or 'All' = '{3}')
       and tt.in_type in (select pp.class3_code from com_priv_class3 pp where pp.class2_code = '0310' and pp.class3_meaning_code <> '1C')
     group by tt.company_code
     order by tt.company_code";
            DataSet ds = new DataSet();
            strSql = string.Format(strSql, this.cmbDept.Tag, this.dtStart.Value, this.dtEnd.Value, this.cmbCustomType.Tag);
            int param = checkMgr.ExecQuery(strSql, ref ds);
            DataTable dt = ds.Tables[0] as DataTable;
            if (dt == null || dt.Rows.Count == 0)
            {
                MessageBox.Show("没有要打印的信息");
                return 0;
            }

            FS.SOC.Local.Pharmacy.ZhuHai.Print.ZDWY.Report.ReportPrint.ucPharmacyInputByCompanyPrint ucPharmacyInputByCompanyPrint = new FS.SOC.Local.Pharmacy.ZhuHai.Print.ZDWY.Report.ReportPrint.ucPharmacyInputByCompanyPrint();
            FS.SOC.Local.Pharmacy.ZhuHai.Base.PrintBill printBill = new FS.SOC.Local.Pharmacy.ZhuHai.Base.PrintBill();
            printBill.PageSize.Printer = this.DefaultPrinter;
            printBill.PageSize.Height = 1100;
            printBill.PageSize.Width = 870;
            printBill.PageSize.ID = "Letter";
            printBill.RowCount = 27;
            ucPharmacyInputByCompanyPrint.printBill = printBill;
            ucPharmacyInputByCompanyPrint.SetPrintData(ds.Tables[0], this.dtStart.Value, this.dtEnd.Value);

            return 1;
        }
    }
}
