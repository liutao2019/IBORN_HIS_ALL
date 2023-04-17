using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SOC.Fee.DayBalance.Object;
using SOC.Fee.DayBalance.Manager;
using System.Collections;

namespace SOC.Fee.DayBalance.Outpatient
{
    public partial class ucBalanceByMath : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucBalanceByMath()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 统计大类业务类
        /// </summary>
        FS.HISFC.BizLogic.Fee.FeeCodeStat feecodeStat = new FS.HISFC.BizLogic.Fee.FeeCodeStat();

        /// <summary>
        /// 管理类
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager managerInteger = new FS.HISFC.BizProcess.Integrate.Manager();
        /// <summary>
        /// 打印纸张设置类
        /// </summary>
        FS.HISFC.BizLogic.Manager.PageSize psManager = new FS.HISFC.BizLogic.Manager.PageSize();

        /// <summary>
        /// 未退药品的列设置路径
        /// </summary>
        protected string filePathBalanceDetialByBank = FS.FrameWork.WinForms.Classes.Function.SettingPath + @".\BalanceDetialByBankByDay.xml";

        /// <summary>
        /// 明细列表列表
        /// </summary>
        protected DataTable dtBalanceDetial = new DataTable();

        /// <summary>
        /// 明细DV
        /// </summary>
        protected DataView dvBalanceDetial = new DataView();

        OutPatientDayBalance opDayBalance = new OutPatientDayBalance();

        protected void init()
        {
            DateTime dt = this.opDayBalance.GetDateTimeFromSysDateTime();
            this.dtpStart.Value = new DateTime(this.dtpStart.Value.Year, this.dtpStart.Value.Month, this.dtpStart.Value.Day, 0, 0, 0);
            this.dtpEnd.Value = new DateTime(this.dtpEnd.Value.Year, this.dtpEnd.Value.Month, this.dtpEnd.Value.Day, 23, 59, 59);
            ArrayList al = managerInteger.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.F);
            this.cbsky.AddItems(al);
        }
        
        protected override int OnQuery(object sender, object neuObject)
        {
            DateTime starttime = new DateTime(this.dtpStart.Value.Year, this.dtpStart.Value.Month, this.dtpStart.Value.Day, 0, 0, 0);
            DateTime endtime = new DateTime(this.dtpEnd.Value.Year, this.dtpEnd.Value.Month, this.dtpEnd.Value.Day, 23, 59, 59);
            //汇总使用的发票
            string sql = @"SELECT  
                           DISTINCT print_invoiceno
                           FROM fin_opb_invoiceinfo --发票信息表(结算信息表)
                           WHERE (oper_code = '{0}' or 'ALL' = '{0}')
                           and oper_date >= to_date('{1}', 'yyyy-MM-dd hh24:mi:ss')
                           and oper_date <= to_date('{2}', 'yyyy-MM-dd hh24:mi:ss')
                           and (balance_flag='1'or balance_flag is null)
                           order by print_invoiceno
                           ";
            try
            {
                sql = string.Format(sql,this.cbsky.Tag.ToString(), starttime, endtime);
            }
            catch (Exception ex)
            {
                //this.Err = ex.Message;
                return -1;
            }
            DataSet dsResult = null;
            if (this.opDayBalance.ExecQuery(sql, ref dsResult) == -1)
            {
                //this.Err = "执行SQL语句失败！";
                return -1;
            }
            this.neuSpread1_Sheet1.RowCount = 0;
            DataTable dtResult = dsResult.Tables[0];
            long  invoiceno1 = 0;
            long  invoiceno2 = 0;
            int invoicenoCount = 1;
            int i = 0;
            foreach (DataRow dr in dtResult.Rows)
            {
                if (invoiceno1 == 0)
                {
                    invoiceno1 = long.Parse(dr["print_invoiceno"].ToString());
                    this.neuSpread1_Sheet1.RowCount =2;
                    this.neuSpread1_Sheet1.Cells[i, 4].Text = invoiceno1.ToString();
                }
                invoiceno2 = long.Parse(dr["print_invoiceno"].ToString());
                if (invoiceno2 - invoiceno1 > 1 || dtResult.Rows.Count == invoicenoCount)
                {
                    this.neuSpread1_Sheet1.Cells[i, 5].Text = invoiceno1.ToString();
                    if (dtResult.Rows.Count != invoicenoCount)
                    {
                        i++;
                        this.neuSpread1_Sheet1.RowCount++;
                        this.neuSpread1_Sheet1.Cells[i, 4].Text = invoiceno2.ToString();
                    }
                }
                invoiceno1 = invoiceno2;
                if (dtResult.Rows.Count == invoicenoCount)
                {
                    this.neuSpread1_Sheet1.Cells[i, 5].Text = invoiceno2.ToString();
                }
                invoicenoCount++;
            }
            //统计总金额
            string sql1 = @"SELECT  
                           sum(own_cost) as sumown
                           FROM fin_opb_invoiceinfo --发票信息表(结算信息表)
                           WHERE (oper_code = '{0}' or 'ALL' = '{0}')
                           and oper_date >= to_date('{1}', 'yyyy-MM-dd hh24:mi:ss')
                           and oper_date <= to_date('{2}', 'yyyy-MM-dd hh24:mi:ss')
                           and (balance_flag='1'or balance_flag is null)
                           ";
            try
            {
                sql1 = string.Format(sql1, this.cbsky.Tag.ToString(), starttime, endtime);
            }
            catch (Exception ex)
            {
                //this.Err = ex.Message;
                return -1;
            }
            DataSet dsResult1 = null;
            if (this.opDayBalance.ExecQuery(sql1, ref dsResult1) == -1)
            {
                //this.Err = "执行SQL语句失败！";
                return -1;
            }
            DataTable dtResult1 = dsResult1.Tables[0];
            foreach (DataRow dr in dtResult1.Rows)
            {
                i++;
                this.neuSpread1_Sheet1.RowCount++;
                this.neuSpread1_Sheet1.Cells[i, 0].Text ="现金";
                this.neuSpread1_Sheet1.Cells[i, 1].Text = dr["sumown"].ToString();
                this.neuSpread1_Sheet1.Cells[i, 3].Text = (invoicenoCount -1).ToString();
            }
            //汇总退费的发票
            string sql2 = @"SELECT  
                           DISTINCT print_invoiceno
                           FROM fin_opb_invoiceinfo --发票信息表(结算信息表)
                           WHERE (oper_code = '{0}' or 'ALL' = '{0}')
                           and oper_date >= to_date('{1}', 'yyyy-MM-dd hh24:mi:ss')
                           and oper_date <= to_date('{2}', 'yyyy-MM-dd hh24:mi:ss')
                           and (balance_flag='1'or balance_flag is null)
                           and trans_type ='2'
                           order by print_invoiceno
                           ";
            try
            {
                sql2 = string.Format(sql2, this.cbsky.Tag.ToString(), starttime, endtime);
            }
            catch (Exception ex)
            {
                //this.Err = ex.Message;
                return -1;
            }
            DataSet dsResult2 = null;
            if (this.opDayBalance.ExecQuery(sql2, ref dsResult2) == -1)
            {
                //this.Err = "执行SQL语句失败！";
                return -1;
            }
            DataTable dtResult2 = dsResult2.Tables[0];
            int sumcost = 0;
            foreach (DataRow dr in dtResult2.Rows)
            {
                i++;
                this.neuSpread1_Sheet1.RowCount++;
                this.neuSpread1_Sheet1.Cells[i, 4].Text = dr["print_invoiceno"].ToString();
                this.neuSpread1_Sheet1.Cells[i, 5].Text = dr["print_invoiceno"].ToString();
                sumcost++;
            }
            //退费总金额
            string sql3 = @"SELECT  
                           sum(own_cost) as sumown
                           FROM fin_opb_invoiceinfo --发票信息表(结算信息表)
                           WHERE (oper_code = '{0}' or 'ALL' = '{0}')
                           and oper_date >= to_date('{1}', 'yyyy-MM-dd hh24:mi:ss')
                           and oper_date <= to_date('{2}', 'yyyy-MM-dd hh24:mi:ss')
                           and (balance_flag='1'or balance_flag is null)
                           and trans_type ='2'
                           ";
            try
            {
                sql3 = string.Format(sql3, this.cbsky.Tag.ToString(), starttime, endtime);
            }
            catch (Exception ex)
            {
                //this.Err = ex.Message;
                return -1;
            }
            DataSet dsResult3 = null;
            if (this.opDayBalance.ExecQuery(sql3, ref dsResult3) == -1)
            {
                //this.Err = "执行SQL语句失败！";
                return -1;
            }
            DataTable dtResult3 = dsResult3.Tables[0];
            foreach (DataRow dr in dtResult3.Rows)
            {
                i++;
                this.neuSpread1_Sheet1.RowCount++;
                this.neuSpread1_Sheet1.Cells[i, 0].Text = "退费";
                this.neuSpread1_Sheet1.Cells[i, 1].Text = dr["sumown"].ToString();
                this.neuSpread1_Sheet1.Cells[i, 3].Text = sumcost.ToString();
            }

            return 1;
        }
        private void ucBalanceByMath_Load(object sender, EventArgs e)
        {
            this.init();
        }
    }
    
}
