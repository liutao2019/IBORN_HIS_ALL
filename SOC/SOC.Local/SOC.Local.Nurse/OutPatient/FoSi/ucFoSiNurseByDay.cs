using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.Local.Nurse.FoSi
{
    /// <summary>
    /// 佛四注射室用药查询
    /// </summary>
    public partial class ucFoSiNurseByDay : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucFoSiNurseByDay()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 注射管理类
        /// </summary>
        private FS.HISFC.BizLogic.Nurse.Inject injectMgr = new FS.HISFC.BizLogic.Nurse.Inject();

        private FS.FrameWork.Models.NeuObject neuObject = null;
        
        //初始化
        private void Init()
        {
            DateTime dt = this.injectMgr.GetDateTimeFromSysDateTime();
            DateTime dt1 = dt.AddDays(-1);
            this.dtpstartime.Value = new DateTime(dt1.Year, dt1.Month, dt1.Day, 0, 0, 0);
            this.dtpendtime.Value = new DateTime(dt1.Year, dt1.Month, dt1.Day, 23, 59, 59);
            this.neuObject = ((FS.HISFC.Models.Base.Employee)this.injectMgr.Operator).Dept;
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            this.Query();

            return base.OnQuery(sender, neuObject);
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            this.print();

            return base.OnPrint(sender, neuObject);
        }

        /// <summary>
        /// 导出数据为Excel格式
        /// </summary>
        private void ExportInfo()
        {
            //try
            //{
            //    string fileName = "";
            //    SaveFileDialog dlg = new SaveFileDialog();
            //    dlg.DefaultExt = ".xls";
            //    dlg.Filter = "Microsoft Excel (*.xls)|*.*";
            //    DialogResult result = dlg.ShowDialog();

            //    if (result == DialogResult.OK)
            //    {
            //        fileName = dlg.FileName;
            //        this.neuSpread1.SaveExcel(fileName, FarPoint.Win.Spread.Model.IncludeHeaders.ColumnHeadersCustomOnly);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

        public override int Export(object sender, object neuObject)
        {
            this.ExportInfo();
            return 1;
        }

        public int print()
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            print.PrintPreview(0, 0, this.neuPanel2);
            //print.PrintPage(12, 2, this.neuPanel2);
            return 1;
        }

        /// <summary>
        /// 按时间查找注射室用药
        /// </summary>
        private void Query()
        {
            //this.dtpstartime.Value = FS.FrameWork.Function.NConvert.ToDateTime(this.dtpstartime.Text);

            DateTime starttime = new DateTime(this.dtpstartime.Value.Year, this.dtpstartime.Value.Month, this.dtpstartime.Value.Day, 0, 0, 0);
            DateTime endtime = new DateTime(this.dtpendtime.Value.Year, this.dtpendtime.Value.Month, this.dtpendtime.Value.Day, 23, 59, 59);

            string sql = @"select
                           tt.trade_name  as 商品名,
                           tt.specs as 规格,
                           sum(t.apply_num ) as 总量,
                           t.min_unit as 单位,
                           round(t.retail_price / decode(t.pack_qty,0,1,t.pack_qty),4) as 零售价,
                           t.retail_price as 包装价格,
                           t.pack_unit as 包装单位,
                           t.pack_qty as 包装数量,
                          sum(round(t.apply_num * t.days / decode(t.pack_qty,0,1,t.pack_qty) * t.retail_price,2)) as 金额
                    from    pha_com_applyout t,pha_com_baseinfo tt
                    where  t.valid_state ='1'
                    and t.drug_code=tt.drug_code
                    and (tt.special_flag4 is  null or tt.special_flag4!='1')
                    and     t.recipe_no in (
                    select  pha_sto_recipe.recipe_no
                    from    pha_sto_recipe
                    where  pha_sto_recipe.drug_dept_code='{2}'
                    and    pha_sto_recipe.fee_date >= to_date('{0}','yyyy-mm-dd hh24:mi:ss')
                    and     pha_sto_recipe.fee_date < to_date('{1}','yyyy-mm-dd hh24:mi:ss')
                    and     (('A' = 'A') or ('A' = '0' and pha_sto_recipe.card_no = 'A')
                            or ('A' = '1' and pha_sto_recipe.invoice_no = 'A')
                            or ('A' = '2' and pha_sto_recipe.patient_name =  'A')
                            or ('A' = '3' and pha_sto_recipe.recipe_no = 'A')
                            or ('A' = '4' and pha_sto_recipe.doct_code = 'A'))
                    )
                    group by   tt.trade_name,tt.specs, t.min_unit ,round(t.retail_price / decode(t.pack_qty,0,1,t.pack_qty),4),t.retail_price, t.pack_unit ,t.pack_qty 
                    order by tt.trade_name
                    ";
            try
            {
                sql = string.Format(sql, starttime, endtime,this.neuObject.ID);
            }
            catch (Exception ex)
            {
                //this.Err = ex.Message;
                return ;
            }

            this.neuLabel1.Text = "统计时间：  " + starttime.ToShortDateString().ToString() + "  至  " + endtime.ToShortDateString().ToString();
            this.neuLabel2.Text = "打印时间：  "+this.injectMgr.GetDateTimeFromSysDateTime().ToString();
            DataSet dsResult = null;
            if (this.injectMgr.ExecQuery(sql, ref dsResult) == -1)
            {
                //this.Err = "执行SQL语句失败！";
                return ;
            }

            this.neuSpread1_Sheet1.RowCount = 0;
            DataTable dtResult = dsResult.Tables[0];
            int i = 0;

            foreach (DataRow dr in dtResult.Rows)
            {
                this.neuSpread1_Sheet1.RowCount++;
                if (dr["单位"].ToString() == "ml")
                {
                    this.neuSpread1_Sheet1.Cells[i, 0].Text = dr["商品名"].ToString();
                    this.neuSpread1_Sheet1.Cells[i, 1].Text = dr["规格"].ToString();
                    this.neuSpread1_Sheet1.Cells[i, 2].Text = dr["包装价格"].ToString();
                    decimal cost = FS.FrameWork.Function.NConvert.ToDecimal(dr["总量"]) / FS.FrameWork.Function.NConvert.ToDecimal(dr["包装数量"]);
                    this.neuSpread1_Sheet1.Cells[i, 3].Text = cost.ToString();
                    this.neuSpread1_Sheet1.Cells[i, 4].Text = dr["包装单位"].ToString();
                    this.neuSpread1_Sheet1.Cells[i, 5].Text = dr["金额"].ToString();
                }
                else
                {
                    this.neuSpread1_Sheet1.Cells[i, 0].Text = dr["商品名"].ToString();
                    this.neuSpread1_Sheet1.Cells[i, 1].Text = dr["规格"].ToString();
                    this.neuSpread1_Sheet1.Cells[i, 2].Text = dr["零售价"].ToString();
                    this.neuSpread1_Sheet1.Cells[i, 3].Text = dr["总量"].ToString();
                    this.neuSpread1_Sheet1.Cells[i, 4].Text = dr["单位"].ToString();
                    this.neuSpread1_Sheet1.Cells[i, 5].Text = dr["金额"].ToString();
                }
                i++;
            }
            return;
        }

        private void ucFoSiNurseByDay_Load(object sender, EventArgs e)
        {
            this.Init();
        }
    }
}
