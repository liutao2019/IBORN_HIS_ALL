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
    public partial class ucPharmacyInputByCompanyType:FS.SOC.Local.Pharmacy.Base.BaseReport
    {
        #region SQL
 //select aa.药品分类,
 //      aa.药品名称,
 //      aa.药品代码,
 //      aa.规格,
 //      aa.单位,
 //      aa.发票号,
 //      aa.品种数,
 //      aa.入库总数量,
 //      aa.买入总金额,
 //      round(aa.买入总金额 / bb.总金额, 4) * 100 || '%' 占总金额百分比
 // from (select (select c.name
 //                 from com_dictionary c
 //                where c.type = 'PAYMENTLEVEL'
 //                  and c.code = bb.extend3) 药品分类,
 //              bb.trade_name 药品名称,
 //              bb.custom_code 药品代码,
 //              bb.specs 规格,
 //              bb.pack_unit 单位,
 //              t.invoice_no 发票号,
 //              count(*) 品种数,
 //              round(sum(t.in_num / t.pack_qty), 2) 入库总数量,
 //              sum(t.purchase_cost) 买入总金额
 //         from pha_com_input t, pha_com_baseinfo bb
 //        where t.in_date >=
 //              to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')
 //          and t.in_date <
 //              to_date('{2}', 'yyyy-mm-dd hh24:mi:ss')
 //          and t.drug_dept_code = '{0}'
 //          and (t.company_code = '{3}' or 'All' = '{3}')
 //          and t.drug_code = bb.drug_code
 //          and t.invoice_no is not null
 //        group by grouping sets((bb.extend3, bb.drug_code, t.invoice_no, bb.trade_name, bb.custom_code, bb.specs, bb.pack_unit), bb.extend3,null)
 //        order by bb.extend3) aa,
 //      (select sum(t.purchase_cost) 总金额
 //         from pha_com_input t, pha_com_baseinfo bb
 //        where t.in_date >=
 //              to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')
 //          and t.in_date <
 //              to_date('{2}', 'yyyy-mm-dd hh24:mi:ss')
 //          and t.drug_dept_code = '{0}'
 //          and (t.company_code = '{3}' or 'All' = '{3}')
 //          and t.drug_code = bb.drug_code
 //          and t.invoice_no is not null) bb
        #endregion
        public ucPharmacyInputByCompanyType()
        {
            InitializeComponent();
            this.SQLIndexs = "Pharmacy.NewReport.Fin.ucPharmacyInputByCompanyType";
            //this.PriveClassTwos = "0320";
            this.MainTitle = "供货单位药品分类表";
            //this.DeptType = "P";
            this.PriveClassTwos = "0310";
            this.RightAdditionTitle = string.Empty;
            this.IsUseCustomType = true;
            this.CustomTypeShowType = "公司：";
            this.CustomTypeSQL = @"select 'All' id,'全部' name,'All' memo,'All' spell_code,'All','0' input_code from dual union select c.fac_code id, c.fac_name name, '', fun_get_querycode(c.fac_name,1), fun_get_querycode(c.fac_name,0), '1'input_code from pha_com_company c where c.company_type = '1' order by input_code
";
        }
    }
}
