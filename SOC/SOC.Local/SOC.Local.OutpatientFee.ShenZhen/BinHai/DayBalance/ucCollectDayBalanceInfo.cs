using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.WinForms.Report.OutpatientFee.Function;
using FS.WinForms.Report.OutpatientFee.Class;
using System.Collections;


namespace FS.SOC.Local.OutpatientFee.ShenZhen.BinHai.DayBalance
{
    public partial class ucCollectDayBalanceInfo : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucCollectDayBalanceInfo()
        {
            InitializeComponent();
        }
        #region 变量
        /// <summary>
        /// 数据库操作类
        /// </summary>
        private FS.FrameWork.Management.DataBaseManger dbMgr = new FS.FrameWork.Management.DataBaseManger();
        /// <summary>
        /// 日结方法类
        /// </summary>
        FS.WinForms.Report.OutpatientFee.Function.ClinicDayBalance clinicDayBalance = new FS.WinForms.Report.OutpatientFee.Function.ClinicDayBalance();
        /// <summary>
        /// 日结序号
        /// </summary>
        private string balanceNO = "' '";
        
        #endregion
        /// <summary>
        /// 日结序号
        /// </summary>
        public string BalaceNO
        {
            get
            {
                return balanceNO;
            }
        }
        #region 属性
        
        #endregion
        private void btSelectAll_Click(object sender, EventArgs e)
        {
            if (this.neuSpread1_Sheet1.Rows.Count == 0) return;
            for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
            {
                this.neuSpread1_Sheet1.Cells[i, 0].Text = "True";
            }
        }

        private void ucCollectDayBalanceInfo_Load(object sender, EventArgs e)
        {
            this.neuSpread1_Sheet1.Rows.Count = 0;
        }

        private void btQuery_Click(object sender, EventArgs e)
        {
            DateTime begin = this.dtBeginDate.Value;
            DateTime end = this.dtEndDate.Value;
            int resultValue = 0;
            List<ClinicDayBalanceNew> list = new List<ClinicDayBalanceNew>();
            #region {A233C411-4B52-4831-AF89-8D7C2CE8D09E} 日结汇总加补打功能
            if (this.ckRePrint.Checked == true)
            {
                resultValue = this.LocalGetCheckedCollectDayBalanceInfo(begin.ToString(), end.ToString(), ref list);
            }
            else
            {
                resultValue = this.LocalGetCheckedCollectDayBalanceInfo(begin.ToString(), end.ToString(), ref list);

            } 
            #endregion
            if (resultValue == -1) return;
            this.neuSpread1_Sheet1.Rows.Count = list.Count;
            for (int i = 0; i < list.Count; i++)
            {
                this.neuSpread1_Sheet1.Cells[i, 0].Text = "False";
                this.neuSpread1_Sheet1.Cells[i, 1].Text = list[i].Oper.Name;
                this.neuSpread1_Sheet1.Cells[i, 2].Text = list[i].BeginTime.ToString();
                this.neuSpread1_Sheet1.Cells[i, 3].Text = list[i].EndTime.ToString();
                this.neuSpread1_Sheet1.Cells[i, 4].Text = list[i].BegionInvoiceNO.ToString();
                this.neuSpread1_Sheet1.Cells[i, 5].Text = list[i].EndInvoiceNo.ToString();
                #region {A233C411-4B52-4831-AF89-8D7C2CE8D09E} 日结汇总加补打功能
                this.neuSpread1_Sheet1.Cells[i, 6].Text = list[i].Memo.ToString(); 
                #endregion
                this.neuSpread1_Sheet1.Rows[i].Tag = list[i];
            }
        }

        private void btOk_Click(object sender, EventArgs e)
        {
            balanceNO = string.Empty;
            ClinicDayBalanceNew obj = null;
            for (int i = 0; i < neuSpread1_Sheet1.Rows.Count; i++)
            {
                if (this.neuSpread1_Sheet1.Cells[i, 0].Text != "True") continue;
                if (this.neuSpread1_Sheet1.Rows[i].Tag == null) continue;
                obj = this.neuSpread1_Sheet1.Rows[i].Tag as ClinicDayBalanceNew;
                balanceNO += "'"+obj.BlanceNO+ "'"+ ",";
            }

            if (balanceNO == string.Empty)
            {
               // MessageBox.Show("请选择要汇总的数据！");
                balanceNO = "' '";
            }
            else
            {
                balanceNO = balanceNO.Substring(0, balanceNO.LastIndexOf(","));
            }
            this.FindForm().DialogResult = DialogResult.OK;
            this.FindForm().Close();
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }

        #region {A233C411-4B52-4831-AF89-8D7C2CE8D09E} 日结汇总加补打功能
        private void neuSpread1_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.ckRePrint.Checked == true)
            {
                if (this.neuSpread1_Sheet1.Cells[e.Row, e.Column].Text == "False")
                {
                    this.neuSpread1.Sheets[0].Cells[0, 0, this.neuSpread1.Sheets[0].RowCount - 1, 0].Text = "False";
                    ClinicDayBalanceNew cdbn;
                    cdbn = this.neuSpread1_Sheet1.Rows[e.Row].Tag as ClinicDayBalanceNew;
                    for (int i = 0; i < this.neuSpread1.Sheets[0].RowCount; i++)
                    {
                       ClinicDayBalanceNew cdbnCurrent;
                        cdbnCurrent = this.neuSpread1_Sheet1.Rows[i].Tag as ClinicDayBalanceNew;
                        if (cdbn.Memo == cdbnCurrent.Memo)
                        {
                            this.neuSpread1_Sheet1.Cells[i, 0].Text = "True";
                        }
                    }
                }
                else
                {
                    this.neuSpread1.Sheets[0].Cells[0, 0, this.neuSpread1.Sheets[0].RowCount - 1, 0].Text = "False";
                }
                e.Cancel = true;
            }
        } 
        #endregion
        #region 数据库操作
        public  int LocalGetCheckedCollectDayBalanceInfo(string dateBegin, string dateEnd,ref List<ClinicDayBalanceNew> list)
        {
            string sql = @"select balance_no,
       (select aa.empl_name
          from com_employee aa
         where aa.empl_type = 'F'
           and aa.valid_state = '1'
           and aa.empl_code = t.oper_code)||'(收费)' oper_code,
       begin_date,
       end_date,
       oper_date
  from fin_opb_dayreport t
 WHERE t.begin_date >= to_date('{0}', 'yyyy-mm-dd hh24:mi:ss')
   AND t.begin_date <= to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')
union all 
select balance_no,
       (select aa.empl_name
          from com_employee aa
         where aa.empl_type = 'F'
           and aa.valid_state = '1'
           and aa.empl_code = t.oper_code)||'(挂号)' oper_code,
       begin_date,
       end_date,
       oper_date
  from fin_opr_daybalance t
 WHERE t.begin_date >= to_date('{0}', 'yyyy-mm-dd hh24:mi:ss')
   AND t.begin_date <= to_date('{1}', 'yyyy-mm-dd hh24:mi:ss') 
     order by oper_code";
            DataSet dsItem = new DataSet();
            sql = string.Format(sql, dateBegin, dateEnd);
            dbMgr.ExecQuery(sql, ref dsItem);
            ClinicDayBalanceNew obj = null;
            if (dsItem.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dsItem.Tables[0].Rows.Count; i++)
                {
                    try
                    {
                    obj = new ClinicDayBalanceNew();
                    obj.BlanceNO = dsItem.Tables[0].Rows[i][0].ToString();
                    obj.Oper.Name = dsItem.Tables[0].Rows[i][1].ToString();
                    obj.BeginTime =Convert.ToDateTime(dsItem.Tables[0].Rows[i][2].ToString());
                    obj.EndTime = Convert.ToDateTime(dsItem.Tables[0].Rows[i][3].ToString());
                    obj.BegionInvoiceNO = "";
                    obj.EndInvoiceNo = "";
                    obj.Memo =  dsItem.Tables[0].Rows[i][4].ToString();
                    list.Add(obj);
                   
                    }
                    catch
                    {

                    }
                }
               
        }
            return 1;
       }
        #endregion

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            string strbool = "False";
            if (this.chkSelectAll.Checked)
            {
                strbool = "True";
            }
            else
            {
                strbool = "False";
            }
            for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
            {
                this.neuSpread1_Sheet1.Cells[i, 0].Text = strbool;
            }
        }

    }
}
