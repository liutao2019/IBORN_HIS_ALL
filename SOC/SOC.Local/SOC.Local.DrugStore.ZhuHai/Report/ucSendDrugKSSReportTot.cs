using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.Local.DrugStore.ZhuHai.Report
{
    public partial class ucSendDrugKSSReportTot : FS.SOC.Local.Pharmacy.Base.ucPrivePowerReport
    {
        #region SQL
// select   b.custom_code 自定义码,
//              (select d.name from com_dictionary d where d.type = 'ITEMTYPE' and d.code = o.drug_type and rownum = 1) 药品类型,
//                            (select ee.node_name from pha_com_function ee where ee.node_code = b.phy_function2)
//                            b.trade_name 药品名称,
//                            b.SPECS 规格,
//                            sum(o.out_num) 数量,
//                            b.min_unit 单位,
//                            o.purchase_price 购入价,
//                            sum(round(o.purchase_price*o.out_num/o.pack_qty,4)) 购入金额,
//                            o.retail_price 零售价, 
//                            sum(round(o.retail_price*o.out_num/o.pack_qty,2)) 零售金额
//from    pha_com_output o,pha_com_baseinfo b
//where o.DRUG_CODE = b.DRUG_CODE 
//and      o.drug_dept_code = '{0}' 
//and      (o.out_date >= to_date('{1}','yyyy-mm-dd hh24:mi:ss') or 'AAAA'<>'{4}')
//and      (o.out_date <   to_date('{2}','yyyy-mm-dd hh24:mi:ss') or 'AAAA'<>'{4}')
//and      (b.phy_function1 = '{3}' or 'AAAA'='{3}')
//and      (o.recipe_no = '{4}' or 'AAAA'='{4}')
//and      (o.drug_code = '{5}' or 'AAAA'='{5}')
//and      (o.drug_storage_code = '{6}' or 'AAAA' = '{6}')
//and      (o.drug_type = '{7}' or 'AAAA' = '{7}')
//and      o.out_type in ( 'M1','M2','Z1','Z2') 
//group  by o.drug_type,b.custom_code,b.trade_name,b.specs,b.min_unit,o.purchase_price,o.retail_price,b.phy_function2
        #endregion
        public ucSendDrugKSSReportTot()
        {
            InitializeComponent();
            this.SQLIndexs = "Pharmacy.NewReport.Out.SendDrugKSSRepot.Tot";
            this.PriveClassTwos = "0320";
            this.MainTitle = "药房抗菌药消耗报表";
            this.CustomTypeShowType = "药理：";
            this.CustomTypeSQL = "select cc.node_code id,cc.node_name name,cc.spell_code,cc.wb_code,'','' from pha_com_function cc where cc.grade_code = '2'";
            this.RightAdditionTitle = "";
            this.IsUseCustomType = true;
            this.SumColIndexs = "9,10";
            this.SortColIndexs = "0,1,2,3,4,5,6,7,8,9,10";
        }

        //private void ucSendDrugReport_Load(object sender, EventArgs e)
        //{
        //    this.SQLIndexs = "Pharmacy.NewReport.Out.SendDrugRepot.Detail";
        //    this.PriveClassTwos = "0320";
        //    this.MainTitle = "药房消耗明细查询";
        //    this.RightAdditionTitle = "";
        //    this.Init();
        //}
    }
}
