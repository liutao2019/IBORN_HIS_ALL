using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.Local.Pharmacy.ZhuHai.Report
{
    public partial class ucCheckReportTot : FS.SOC.Local.Pharmacy.Base.ucPrivePowerReport
    {
        public ucCheckReportTot()
        {
            InitializeComponent();
            this.SQLIndexs = "Pharmacy.NewReport.Check.Tot";
            this.PriveClassTwos = "0305";
            this.MainTitle = "盘点汇总查询";
            this.RightAdditionTitle = "";
            this.ShowTypeName = myDeptType.其他;
        }

        //private void ucCheckReportTot_Load(object sender, EventArgs e)
        //{
        //    this.SQLIndexs = "Pharmacy.NewReport.Check.Tot";
        //    this.PriveClassTwos = "0305";
        //    this.MainTitle = "盘点汇总查询";
        //    this.RightAdditionTitle = "";
        //    this.ShowTypeName = myDeptType.其他;

        //    this.Init();
        //}
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
            string strSql = @" select nvl(分类,'总合计：') 类别,
       decode(金额类型, '1', '购入', '2', '零售', '3', '购零差') 类别1,
       账本金额,
       实存金额,
       增减金额
  from (select (select d.name
                  from com_dictionary d
                 where d.type = 'ITEMTYPE'
                   and d.code = b.drug_type
                   and rownum = 1) 分类,
               '1' 金额类型,
               sum(round(cd.fstore_num * cd.purchase_price / cd.pack_qty, 2)) 账本金额,
               sum(round(cd.adjust_num * cd.purchase_price / cd.pack_qty, 2)) 实存金额,
               sum(round(cd.adjust_num * cd.purchase_price / cd.pack_qty, 2)) -
               sum(round(cd.fstore_num * cd.purchase_price / cd.pack_qty, 2)) 增减金额
          from pha_com_checkstatic cs,
               pha_com_checkdetail cd,
               pha_com_baseinfo    b
         where cs.drug_dept_code = cd.drug_dept_code
           and cs.check_code = cd.check_code
           and cd.drug_code = b.drug_code
           and cs.drug_dept_code = '{0}'
           and (cs.foper_time >
               to_date('{1}', 'yyyy-mm-dd hh24:mi:ss'))
           and (cs.foper_time <=
               to_date('{2}', 'yyyy-mm-dd hh24:mi:ss'))
           and (cd.drug_type = '{3}' or '{3}' = 'AAAA')
           AND cs.CHECK_STATE = 1
         group by rollup(cd.drug_dept_code, b.drug_type)
        union
        select (select d.name
                  from com_dictionary d
                 where d.type = 'ITEMTYPE'
                   and d.code = b.drug_type
                   and rownum = 1) 分类,
               '2' 金额类型,
               sum(round(cd.fstore_num * cd.wholesale_price / cd.pack_qty, 2)) 账本金额,
               sum(round(cd.adjust_num * cd.wholesale_price / cd.pack_qty, 2)) 实存金额,
               sum(round(cd.adjust_num * cd.wholesale_price / cd.pack_qty, 2)) -
               sum(round(cd.fstore_num * cd.wholesale_price / cd.pack_qty, 2)) 增减金额
          from pha_com_checkstatic cs,
               pha_com_checkdetail cd,
               pha_com_baseinfo    b
         where cs.drug_dept_code = cd.drug_dept_code
           and cs.check_code = cd.check_code
           and cd.drug_code = b.drug_code
           and cs.drug_dept_code = '{0}'
           and (cs.foper_time >
               to_date('{1}', 'yyyy-mm-dd hh24:mi:ss'))
           and (cs.foper_time <=
               to_date('{2}', 'yyyy-mm-dd hh24:mi:ss'))
           and (cd.drug_type = '{3}' or '{3}' = 'AAAA')
           AND cs.CHECK_STATE = 1
         group by rollup(cd.drug_dept_code, b.drug_type)
        union
        select (select d.name
                  from com_dictionary d
                 where d.type = 'ITEMTYPE'
                   and d.code = b.drug_type
                   and rownum = 1) 分类,
               '3' 金额类型,
               sum(round(cd.fstore_num *
                         (cd.wholesale_price - cd.purchase_price) /
                         cd.pack_qty,
                         2)) 账本金额,
               sum(round(cd.adjust_num *
                         (cd.wholesale_price - cd.purchase_price) /
                         cd.pack_qty,
                         2)) 实存金额,
               sum(round(cd.adjust_num *
                         (cd.wholesale_price - cd.purchase_price) /
                         cd.pack_qty,
                         2)) - sum(round(cd.fstore_num * (cd.wholesale_price -
                                         cd.purchase_price) / cd.pack_qty,
                                         2)) 增减金额
          from pha_com_checkstatic cs,
               pha_com_checkdetail cd,
               pha_com_baseinfo    b
         where cs.drug_dept_code = cd.drug_dept_code
           and cs.check_code = cd.check_code
           and cd.drug_code = b.drug_code
           and cs.drug_dept_code = '{0}'
           and (cs.foper_time >
               to_date('{1}', 'yyyy-mm-dd hh24:mi:ss'))
           and (cs.foper_time <=
               to_date('{2}', 'yyyy-mm-dd hh24:mi:ss'))
           and (cd.drug_type = '{3}' or '{3}' = 'AAAA')
           AND cs.CHECK_STATE = 1
         group by rollup(cd.drug_dept_code, b.drug_type))
 order by (select dd.sort_id
             from com_dictionary dd
            where dd.type = 'ITEMTYPE'
              and dd.name = 分类),
          金额类型";
            DataSet ds = new DataSet();
            strSql = string.Format(strSql, this.cmbDept.Tag, this.dtStart.Value, this.dtEnd.Value, string.IsNullOrEmpty(this.ncmbDrugType.Tag.ToString())?"AAAA":this.ncmbDrugType.Tag);
            int param = checkMgr.ExecQuery(strSql, ref ds);
            DataTable dt = ds.Tables[0] as DataTable;
            if (dt == null || dt.Rows.Count == 0)
            {
                MessageBox.Show("没有要打印的信息");
                return 0;
            }

            //if (SOC.HISFC.BizProcess.Cache.Common.GetDeptInfo(this.cmbDept.Tag.ToString()).DeptType.ID == "PI")
            //{
                FS.SOC.Local.Pharmacy.ZhuHai.Print.ZDWY.Report.ReportPrint.ucCheckReportTotPrint ucCheckReportTotPrint = new FS.SOC.Local.Pharmacy.ZhuHai.Print.ZDWY.Report.ReportPrint.ucCheckReportTotPrint();
                FS.SOC.Local.Pharmacy.ZhuHai.Base.PrintBill printBill = new FS.SOC.Local.Pharmacy.ZhuHai.Base.PrintBill();
                printBill.PageSize.Printer = this.DefaultPrinter;
                printBill.PageSize.Height = 1100;
                printBill.PageSize.Width = 870;
                printBill.PageSize.ID = "Letter";
                printBill.RowCount = 20;
                ucCheckReportTotPrint.printBill = printBill;
                ucCheckReportTotPrint.SetPrintData(ds.Tables[0], this.dtStart.Value, this.dtEnd.Value, this.cmbDept.Tag.ToString());
            //}
            //else
            //{
            //    FS.SOC.Local.Pharmacy.ZhuHai.Print.ZDWY.Report.ReportPrint.ucDrugStoreCheckReportTotPrint ucCheckReportTotPrint = new FS.SOC.Local.Pharmacy.ZhuHai.Print.ZDWY.Report.ReportPrint.ucDrugStoreCheckReportTotPrint();
            //    FS.SOC.Local.Pharmacy.ZhuHai.Base.PrintBill printBill = new FS.SOC.Local.Pharmacy.ZhuHai.Base.PrintBill();
            //    printBill.PageSize.Printer = this.DefaultPrinter;
            //    printBill.PageSize.Height = 1100;
            //    printBill.PageSize.Width = 870;
            //    printBill.PageSize.ID = "Letter";
            //    printBill.RowCount = 20;
            //    ucCheckReportTotPrint.printBill = printBill;
            //    ucCheckReportTotPrint.SetPrintData(ds.Tables[0], this.dtStart.Value, this.dtEnd.Value, this.cmbDept.Tag.ToString());
            //}


            return 1;
        }
    }
}
