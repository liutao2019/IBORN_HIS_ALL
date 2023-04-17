using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.HISFC.Components.Pharmacy.Maintenance
{
    public partial class ucPreOutputQuery : FS.FrameWork.WinForms.Controls.ucBaseControl
    {

        public ucPreOutputQuery()
        {
            InitializeComponent();
            this.btnExport.Click +=new EventHandler(btnExport_Click);
        }

        private DataTable dtDetail = null;

        private FS.HISFC.BizLogic.Manager.Constant consMgr = new FS.HISFC.BizLogic.Manager.Constant();

        public int Init(string deptCode, string drugCode)
        {
            this.Clear();
            string strSQL = @"select r.card_no 病人号,
       r.name 姓名,
       t.apply_number 申请单号,
       fun_get_dept_name(d.dept_code) 科室,
       decode(t.class3_meaning_code, 'M1', '门诊', '住院') 类型,
       t.drug_code 药品编码,
       t.trade_name 药品名称,
       t.specs 规格,
       t.apply_num 预扣量,
       d.min_unit 单位,
       d.apply_date 预扣日期
  from pha_sto_preoutstore t, pha_com_applyout d, fin_opr_register r
 where t.class3_meaning_code = 'M1'
   and t.class3_meaning_code = d.class3_meaning_code
   and t.patient_id = r.clinic_code
   and t.apply_number = d.apply_number
   and t.drug_dept_code = '{0}'
   and t.drug_code = '{1}'
 union 
select c.patient_no 病人号,
       c.name 姓名,
       t.apply_number 申请单号,
       fun_get_dept_name(d.dept_code) 科室,
       decode(t.class3_meaning_code, 'M1', '门诊', '住院') 类型,
       t.drug_code 药品编码,
       t.trade_name 药品名称,
       t.specs 规格,
       t.apply_num 预扣量,
       d.min_unit 单位,
       d.apply_date 预扣日期
  from pha_sto_preoutstore t, pha_com_applyout d, fin_ipr_inmaininfo c
 where t.class3_meaning_code = 'Z1'
   and t.class3_meaning_code = d.class3_meaning_code
   and t.patient_id = c.inpatient_no
   and t.apply_number = d.apply_number
   and t.drug_dept_code = '{0}'
   and t.drug_code = '{1}'
   order by 预扣日期 desc";

            strSQL = string.Format(strSQL,deptCode, drugCode);

            DataSet ds = new DataSet();

            consMgr.ExecQuery(strSQL,ref ds);

            if(ds.Tables[0]!= null && ds.Tables[0].Rows.Count >0)
            {
                dtDetail = ds.Tables[0];
                this.neuSpreadDetail_Sheet1.DataSource = dtDetail.DefaultView;
                this.SetFarpointWith();
                decimal totCount = 0m;
                string minUnit = string.Empty;
                foreach (DataRow dr in dtDetail.Rows)
                {
                    totCount += FS.FrameWork.Function.NConvert.ToDecimal(dr["预扣量"].ToString());
                    minUnit = dr["单位"].ToString();
                }
                this.nlbTitle.Text = "总预扣量：" + totCount + minUnit;
            }
            return 1;
        }

        private void SetFarpointWith()
        {
            this.neuSpreadDetail_Sheet1.Columns.Get(0).Width = 73F;
            this.neuSpreadDetail_Sheet1.Columns.Get(1).Width = 76F;
            this.neuSpreadDetail_Sheet1.Columns.Get(2).Width = 73F;
            this.neuSpreadDetail_Sheet1.Columns.Get(3).Width = 166F;
            this.neuSpreadDetail_Sheet1.Columns.Get(4).Width = 52F;
            this.neuSpreadDetail_Sheet1.Columns.Get(5).Width = 86F;
            this.neuSpreadDetail_Sheet1.Columns.Get(6).Width = 167F;
            this.neuSpreadDetail_Sheet1.Columns.Get(7).Width = 102F;
            this.neuSpreadDetail_Sheet1.Columns.Get(8).Width = 80F;
            this.neuSpreadDetail_Sheet1.Columns.Get(9).Width = 46F;
            this.neuSpreadDetail_Sheet1.Columns.Get(10).Width = 74F;

        }


        private void Clear()
        {
            this.dtDetail = null;
            this.neuSpreadDetail_Sheet1.RowCount = 0;
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExport_Click(object sender, EventArgs e)
        {
            this.neuSpreadDetail.Export();
        }

    }
}
