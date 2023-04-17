using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

using FS.FrameWork.WinForms.Forms;
using FS.FrameWork.Models;

namespace FS.WinForms.Report
{
    public partial class ucInvoiceCheck : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucInvoiceCheck()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 管理类
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager managerInteger = new FS.HISFC.BizProcess.Integrate.Manager();
        private FS.HISFC.BizLogic.Manager.Constant conManger = new FS.HISFC.BizLogic.Manager.Constant();
        private FS.FrameWork.Management.DataBaseManger dbm = new FS.FrameWork.Management.DataBaseManger();
        /// <summary>
        /// toolBarService
        /// </summary>
        ToolBarService toolBarService = new ToolBarService();
        /// <summary>
        /// 增加ToolBar控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("核销", "核销", (int)FS.FrameWork.WinForms.Classes.EnumImageList.G过滤删除, true, false, null);
            //toolBarService.AddToolButton("预览", "预览发票信息", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Y预览, true, false, null);
            //toolBarService.AddToolButton("帮助", "打开帮助文件", (int)FS.FrameWork.WinForms.Classes.EnumImageList.B帮助, true, false, null);


            return this.toolBarService;
        }
        protected override int OnQuery(object sender, object neuObject)
        {
            this.QueryReceipt();
            return base.OnQuery(sender, neuObject);
        }
        /// <summary>
        /// 定义toolbar按钮click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "核销":

                    if (this.Check() > 0)
                    {
                        MessageBox.Show("核销成功！");
                        this.QueryReceipt();
                    }
                    break;
                //case "":

           
                //    break;

                //case "":
                    break;

            }

            base.ToolStrip_ItemClicked(sender, e);
        }
        private void ucInvoiceCheck_Load(object sender, EventArgs e)
        {
            init();
        }
        protected virtual int init()
        {
            //1) 按开始结束时间+票据类型（挂号票、门诊发票、住院预交金发票、住院结算发票）+核销状态（已核销、未核销、全部）
            this.dtpBegin.Value = this.dtpBegin.MinDate;
            this.dtpBegin.Value = DateTime.Now.Date ;
            //this.dtpBegin.Text = this.dtpBegin.Value.ToString(this.dtpBegin.CustomFormat);
            this.dtpEnd.Value = this.dtpBegin.Value.AddDays(1).AddSeconds(-1);
            //this.dtpEnd.Text = this.dtpEnd.Value.ToString(this.dtpEnd.CustomFormat);
            //2) 按收款员姓名+票据类型+核销状态
            //初始化人员信息
            this.cmbName.ClearItems();
            ArrayList al = managerInteger.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.F);
            if (al != null)
            {
                NeuObject obj = new NeuObject();
                obj.ID = "ALL";
                obj.Name = "全部";
                al.Insert(0, obj);
                this.cmbName.AddItems(al);
                this.cmbName.Tag = "ALL";
            }
            //3) 按日结报表编号+票据类型+核销状态
            ArrayList alrt = conManger.GetList("GetInvoiceType");
            this.cmbReceiptType.AddItems(alrt);

            if (this.cmbReceiptType.alItems == null || this.cmbReceiptType.alItems.Count == 0)
            {
                MessageBox.Show("请在常数维护中维护收据的类别");
                return -1 ;
            }
            this.neuSpread1_Sheet1.RowCount = 0;
            this.cmbReceiptType.SelectedIndex = 0;
            return 0;
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            QueryReceipt();
        }
        protected virtual int QueryReceipt()
        {
           
            string sql = string.Empty;
            string sqlWhere = "@where@";
            string sqlUpdate = string.Empty;
            #region 字段
            string c0;
            string c1;
            string c2;
            string c3;
            string c4;
            string c5;

            #endregion
            switch (this.cmbReceiptType.SelectedItem.ID)
            {
                //P	预交收据
                 case "P":
                    c0 = "BEGIN_DATE";
                    c1 = "END_DATE";
                    c2 = "OPER_CODE";
                    c3 = "STAT_NO";
                    c4 = "INVOICE1_CHECK_FLAG";
                    c5 = "";
                    sqlUpdate = @"
update fin_ipb_dayreport
   set INVOICE1_CHECK_FLAG = '1'
,invoice1_check_date = sysdate
,invoice1_check_opcd = '" + dbm.Operator.ID +@"'
 where INVOICE1_CHECK_FLAG = '0'
   and STAT_NO = '@index@'";
                    sql = @"
select '',
       t.stat_no,
       t.oper_code,
(select distinct a.empl_name from com_employee a where a.empl_code = t.oper_code) name,
       t.begin_date,
       t.end_date,
       t.prepayinv_zone zone,
       t.prepayinv_num qty,
       sum(t.prepay_cost - t.balance_prepaycost) tot_cost,
       sum(t.prepay_cost - t.balance_prepaycost) own_cost,
       invoice1_check_date,
       decode(t.invoice1_check_flag , '0','False' ,'1','True'),
       t.invoice1_check_opcd,
(select distinct a.empl_name from com_employee a where a.empl_code = t.invoice1_check_opcd) check_opname
  from fin_ipb_dayreport t
" + sqlWhere+@" 
 group by t.stat_no,
          t.oper_code,
          t.begin_date,
          t.end_date,
          t.prepayinv_zone,
          t.prepayinv_num,        
          t.invoice1_check_date,
          t.invoice1_check_flag,
          t.invoice1_check_opcd
 order by t.stat_no desc";
                    break;
                           //I	住院收据               
             case "I":
                    c0 = "BEGIN_DATE";
                    c1 = "END_DATE";
                    c2 = "OPER_CODE";
                    c3 = "STAT_NO";
                    c4 = "INVOICE_CHECK_FLAG";
                    c5 = "";
                    sqlUpdate = @"
update fin_ipb_dayreport
   set INVOICE_CHECK_FLAG = '1'
,invoice_check_date = sysdate 
,invoice_check_opcd = '" + dbm.Operator.ID + @"'
 where INVOICE_CHECK_FLAG = '0'
   and STAT_NO = '@index@'";
                    sql = @"
select '',
       t.stat_no,
       t.oper_code,

(select distinct a.empl_name from com_employee a where a.empl_code = t.oper_code) name,
       t.begin_date,
       t.end_date,
       t.balanceinv_zone zone,
       t.balanceinv_num qty,
       sum(t.balance_cost) tot_cost,
       sum(t.balance_cost) own_cost,
       t.invoice_check_date,
         decode(t.invoice_check_flag ,'0','False' ,'1','True'),
       t.invoice_check_opcd,
(select distinct a.empl_name from com_employee a where a.empl_code = t.invoice_check_opcd) check_opname
  from fin_ipb_dayreport t
" + sqlWhere + @"
 group by t.stat_no,
          t.oper_code,
          t.begin_date,
          t.end_date,
          t.balanceinv_zone,
          t.balanceinv_num,
          t.invoice_check_date,
          t.invoice_check_flag,
          t.invoice_check_opcd
 order by t.stat_no desc";
                    break;
                    //C	门诊收据
              case "C":
                    c0 = "BEGIN_DATE";
                    c1 = "END_DATE";
                    c2 = "OPER_CODE";
                    c3 = "BLANCE_NO";
                    c4 = "INVOICE_CHECK_FLAG";
                    c5 = "";
                    sqlUpdate = @"
update fin_opb_daybalance
   set INVOICE_CHECK_FLAG = '1'
,invoice_check_date = sysdate 
,invoice_check_opcd = '" + dbm.Operator.ID + @"'
 where INVOICE_CHECK_FLAG = '0'
   and BLANCE_NO = '@index@'";
                    sql = @"
select '',
       t.BLANCE_NO,
       t.oper_code,

(select distinct a.empl_name from com_employee a where a.empl_code = t.oper_code) name,
       t.begin_date,
       t.end_date,
       (select a.extent_field2
          from fin_opb_daybalance a
         where a.blance_no = t.blance_no
           and a.invoice_no in ('A001')) zone,
       (select a.tot_cost
          from fin_opb_daybalance a
         where a.blance_no = t.blance_no
           and a.invoice_no in ('A002')) qty,
       (select sum(a.tot_cost)
          from fin_opb_daybalance a
         where a.blance_no = t.blance_no
           and a.invoice_no in ('A023', 'A024', 'A025')) tot_cost,
       (select sum(a.tot_cost)
          from fin_opb_daybalance a
         where a.blance_no = t.blance_no
           and a.invoice_no in ('A023')) own_cost,
       t.invoice_check_date,
         decode(t.invoice_check_flag , '0','False' ,'1','True'),
       t.invoice_check_opcd,
(select distinct a.empl_name from com_employee a where a.empl_code = t.invoice_check_opcd) check_opname
  from fin_opb_daybalance t
" + sqlWhere + @"
 group by BLANCE_NO,
          oper_code,
          begin_date,
          end_date,
          t.invoice_check_date,
          t.invoice_check_flag,
          t.invoice_check_opcd
 order by t.BLANCE_NO desc";
                    break;
                     //R	挂号收据
               case "R":
                    c0 = "BEGIN_DATE";
                    c1 = "END_DATE";
                    c2 = "OPER_CODE";
                    c3 = "BALANCE_NO";
                    c4 = "INVOICE_CHECK_FLAG";
                    c5 = "";
                    sqlUpdate = @"
update fin_opr_daybalance
   set INVOICE_CHECK_FLAG = '1'
,invoice_check_date = sysdate 
,invoice_check_opcd = '" + dbm.Operator.ID + @"'
 where INVOICE_CHECK_FLAG = '0'
   and BALANCE_NO = '@index@'";
                    sql = @"
select '',
       t.balance_no,
       t.oper_code,

(select distinct a.empl_name from com_employee a where a.empl_code = t.oper_code) name,
       t.begin_date,
       t.end_date,
       t.INVOICE_ZONE zone,
       t.tot_qty,
       sum(t.tot_own + t.tot_pay + t.tot_pub) tot_cost,
       t.tot_own own_cost,
       t.invoice_check_date,
         decode(t.invoice_check_flag , '0','False' ,'1','True'),
       t.invoice_check_opcd,
(select distinct a.empl_name from com_employee a where a.empl_code = t.invoice_check_opcd) check_opname
  from fin_opr_daybalance t
" + sqlWhere + @" 
 group by t.balance_no,
       t.oper_code,
       t.begin_date,
       t.end_date,
       t.INVOICE_ZONE ,
t.tot_qty,
 t.tot_own,
         t.invoice_check_date,
         t.invoice_check_flag,
       t.invoice_check_opcd
 order by t.balance_no desc";
                    break;
               //A	帐户收据
               case "A":
                    MessageBox.Show("当前票据类型不在核销范围内！");
                    return -1;
            
                default:
                    MessageBox.Show("当前票据类型不在核销范围内！");
                    return -1;
                //break;
            }
            string checkWhere = string.Empty;
            if (this.rbCheckAll.Checked==true)
            {
                checkWhere = " (" + c4 + " = '0' or " + c4 + " = '1' ) and ";
            }
            else if (this.rbCheckFalse.Checked == true)
            {
                checkWhere = " "+c4 + " = '0' and ";
            }
            else if (this.rbCheckTrue.Checked == true)
            {
                checkWhere = " " + c4 + " = '1' and ";
            }
            if (this.rbSelect1.Checked == true)
            {
                if (this.dtpBegin.Value > this.dtpEnd.Value)
                {
                    MessageBox.Show("开始时间不能小于结束时间！");
                    return -1;
                }
                sql = sql.Replace(sqlWhere, " where  " + checkWhere + c0 + " >= to_date('" + this.dtpBegin.Value.ToString() + "','yyyy-mm-dd HH24:mi:ss')   and " + c1 + " < to_date('" + this.dtpEnd.Value.ToString() + "','yyyy-mm-dd HH24:mi:ss')");
            }
            else if (this.rbSelect2.Checked == true)
            {

                sql = sql.Replace(sqlWhere, " where " + checkWhere + "(" + c2 + " = '" + cmbName.SelectedItem.ID + "'  or 'ALL'='" + cmbName.SelectedItem.ID + "')");
            }
            else if (this.rbSelect3.Checked == true)
            {
                sql = sql.Replace(sqlWhere, " where " + checkWhere + c3 + " like '%" + this.tbNo.Text.Trim().ToString() + "%'");
            }

            DataSet  ds = new DataSet();
            if (this.dbm.ExecQuery(sql,ref ds)<0)
            {
                MessageBox.Show("查询日结数据出错！"+ this.dbm.Err);
                return -1;
            }
            DataTable dt;
            dt = ds.Tables[0];
            Display(dt);
            this.neuSpread1_Sheet1.Tag = sqlUpdate;
            return 0;
        }
        protected virtual int Display(DataTable dt)
        {
            this.neuSpread1_Sheet1.RowCount = 0;
            this.neuSpread1_Sheet1.DataSource = dt;
            
            return 0;
        }
        protected virtual int Check()
        {
            int retVal = 0;
            if (this.neuSpread1_Sheet1.Tag == null)
            {
                MessageBox.Show("无核销数据！");
                return -1;
            }
            if (string.IsNullOrEmpty(this.neuSpread1_Sheet1.Tag.ToString())==true)
            {
                MessageBox.Show("无核销数据！");
                return -1;
            }
            
            string sql = this.neuSpread1_Sheet1.Tag.ToString();
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            try
            {
                for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
                {
                    if (this.neuSpread1_Sheet1.Cells[i, 0].Text == "True")
                    {
                        sql = sql.Replace("@index@", this.neuSpread1_Sheet1.Cells[i, 1].Text);
                        if (dbm.ExecNoQuery(sql) < 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("核销出错！");
                            return -1;
                        }
                        retVal++;
                    }
                }
                FS.FrameWork.Management.PublicTrans.Commit();
            }
            catch (Exception ex)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("核销出错！");
                return -1;
            }
            return retVal;
        }
    }
}
