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
    public partial class ucInPatientUnSendDrugQuery : FS.SOC.Local.Pharmacy.Base.BaseReport
    {
        /// <summary>
        /// 住院未发药品查询
        /// </summary>
        public ucInPatientUnSendDrugQuery()
        {
            #region SQL
            //            select rownum 序号,cc.oper_code 员工号,cc.oper_name 姓名,cc.类别,cc.发药数量 from 
            //(
            //select bb.oper_code oper_code, fun_get_employee_name(bb.oper_code) oper_name, '发药' 类别, count(*) 发药数量
            //  from pha_com_applyout aa, pha_soc_drugstore_workload bb
            // where aa.drug_dept_code = bb.drug_dept_code
            //   and aa.druged_bill = bb.bill_no
            //   and bb.drug_dept_code = '{0}'
            //   and bb.work_oper_date >=
            //       to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')
            //   and bb.work_oper_date <
            //       to_date('{2}', 'yyyy-mm-dd hh24:mi:ss')
            //   and aa.class3_meaning_code = 'Z1'
            // group by bb.oper_code
            // union all
            // select bb.oper_code, fun_get_employee_name(bb.oper_code) oper_name, '退药' 类别, count(*) 发药数量
            //  from pha_com_applyout aa, pha_soc_drugstore_workload bb
            // where aa.drug_dept_code = bb.drug_dept_code
            //   and aa.druged_bill = bb.bill_no
            //   and bb.drug_dept_code = '{0}'
            //   and bb.work_oper_date >=
            //       to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')
            //   and bb.work_oper_date <
            //       to_date('{2}', 'yyyy-mm-dd hh24:mi:ss')
            //   and aa.class3_meaning_code = 'Z2'
            // group by bb.oper_code
            // ) cc order by oper_code


 //           select ccc.billclass_name 类别,fun_get_dept_name(aa.dept_code) 科室名称,aa.druged_date 操作时间,count(*) 发药数量
 // from pha_com_applyout           aa,
 //      pha_soc_drugstore_workload bb,
 //      pha_sto_billclass          ccc
 //where aa.drug_dept_code = bb.drug_dept_code
 //  and aa.druged_bill = bb.bill_no
 //  and aa.billclass_code = ccc.billclass_code
 //  and bb.drug_dept_code = '{0}'
 //  and bb.work_oper_date >=
 //      to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')
 //  and bb.work_oper_date <
 //      to_date('{2}', 'yyyy-mm-dd hh24:mi:ss')
 //  and bb.oper_code = '{3}'
 //group by ccc.billclass_name, aa.dept_code, aa.druged_date
 //order by ccc.billclass_name,aa.dept_code
            #endregion

            InitializeComponent();
            this.SQLIndexs = "Pharmacy.NewReport.InPatientDrugStore.UnSendDrugQuery";
            this.IsNeedDetailData = true;
            this.DetailSQLIndexs = "Pharmacy.NewReport.InPatientDrugStore.UnSendDrugQueryDetail";
            this.DetailDataQueryType = EnumQueryType.汇总条件活动行;
            this.QueryConditionColIndexs = "1";
            //this.PriveClassTwos = "0320";
            this.MainTitle = "住院未发药查询";
            //this.DeptType = "P";
            this.PriveClassTwos = "0320";
            this.SumColIndexs = "9";
            this.RightAdditionTitle = "";
            this.CustomTypeSQL = "select 'ALL' id, '全部' name, '全部' memo, 'ALL' spell_code, 'ALL', '' from dual union select tt.node_code id,tt.node_name name,tt.node_name memo,fun_get_querycode(tt.node_name,1) spell_code,fun_get_querycode(tt.node_name,0) wb_code,'' from pha_com_function tt where tt.grade_code = '1'";
            this.IsUseCustomType = true;
            this.CustomTypeShowType = "药品：";
        }
    }
}
