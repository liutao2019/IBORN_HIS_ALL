using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.Local.RADT.ZhuHai.ZDWY.IMoneyAlert
{
    /// <summary>
    /// 中大五院催款单打印
    /// </summary>
    public partial class ucMoneyAlert : UserControl, FS.HISFC.BizProcess.Interface.Fee.IMoneyAlert
    {
        public ucMoneyAlert()
        {
            InitializeComponent();
        }

        FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();

        FS.HISFC.BizLogic.Manager.DataBase dbMgr = new FS.HISFC.BizLogic.Manager.DataBase();

        private void Print(FS.HISFC.Models.RADT.PatientInfo pInfo, decimal payMoney)
        {
            this.Clear();

            lblNurse.Text = pInfo.PVisit.PatientLocation.NurseCell.Name;
            lblPatientNO.Text = "住院号：" + pInfo.PID.PatientNO;
            this.lblBedNO.Text = "床位：" + pInfo.PVisit.PatientLocation.Bed.ID.Substring(4);
            this.lblName.Text = "姓名：" + pInfo.Name;
            this.lblSex.Text = "性别：" + pInfo.Sex.Name;
            this.lblPactName.Text = "结算方式：" + pInfo.Pact.Name;
            this.lblDateBegin.Text = "起日：" + pInfo.PVisit.InTime.ToString("yyyy-MM-dd");
            this.lblDateEnd.Text = "止日：" + dbMgr.GetDateTimeFromSysDateTime().ToString("yyyy-MM-dd");
            this.lblOwnCost.Text ="合计：" +(pInfo.FT.OwnCost + pInfo.FT.BalancedCost).ToString();
            this.lblNopay.Text = "未清金额：" + pInfo.FT.OwnCost.ToString();
            this.lblPreCost.Text = "预交款：" + pInfo.FT.PrepayCost.ToString();
            this.lblFreeCost.Text = "余额：" + pInfo.FT.LeftCost.ToString();
            this.lblPrintDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            decimal money = payMoney - pInfo.FT.LeftCost;

            this.lblPayInfo.Text = "您入院时的预交款已不足，为了您快速治愈出院，请于今日内交款 " + money.ToString() + " 元。多谢合作！";

            string strSQL = @"select t.fee_stat_cate 费用类别编码,
                               t.fee_stat_name 费用类别名称,
                               t.print_order 打印序号,
                               sum(f.own_cost) 金额
                          from fin_ipb_feeinfo f, fin_com_feecodestat t
                         where f.fee_code = t.fee_code
                           and t.report_code = 'ZY01'
                           and t.valid_state = '1'
                           and f.inpatient_no = '{0}'
                         group by t.fee_stat_cate, t.fee_stat_name, t.print_order
                         having sum(f.own_cost)>0
                         order by t.print_order";

            strSQL = string.Format(strSQL, pInfo.ID);
            DataSet dsFee = null;
            if (dbMgr.ExecQuery(strSQL, ref dsFee) == -1)
            {
                MessageBox.Show("查询患者费用汇总信息出错！\r\n" + dbMgr.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (dsFee != null
                && dsFee.Tables.Count > 0)
            {
                int index = 1;
                foreach (DataRow dRow in dsFee.Tables[0].Rows)
                {
                    GetFeeCostLable(index).Text = dRow["费用类别名称"].ToString() + "：" + dRow["金额"].ToString();
                    index += 1;
                }
            }

            FS.HISFC.BizLogic.Manager.PageSize pgMgr = new FS.HISFC.BizLogic.Manager.PageSize();
            FS.HISFC.Models.Base.PageSize pSize = pgMgr.GetPageSize("催款单");
            print.SetPageSize(pSize);
            print.PrintPreview(0, 0, this);
        }

        private void Clear()
        {
            lblNurse.Text = string.Empty;
            lblPatientNO.Text = string.Empty;
            this.lblBedNO.Text = string.Empty;
            this.lblName.Text = string.Empty;
            this.lblSex.Text = string.Empty;
            this.lblPactName.Text = string.Empty;
            this.lblDateBegin.Text = string.Empty;
            this.lblDateEnd.Text = string.Empty;
            this.lblOwnCost.Text = string.Empty;
            this.lblNopay.Text = string.Empty;
            this.lblPreCost.Text = string.Empty;
            this.lblFreeCost.Text = string.Empty;
            this.lblPayInfo.Text = string.Empty;
            this.lblPrintDate.Text = string.Empty;
            foreach (Control c in this.Controls)
            {
                if (c.Name.Contains("lblFeeCost"))
                {
                    c.Text = string.Empty;
                }
            }
        }

        /// <summary>
        /// 获得费用金额输入框
        /// </summary>
        /// <param name="i">序号</param>
        /// <returns>费用金额输入框控件</returns>
        private Control GetFeeCostLable(int i)
        {
            foreach (Control c in this.Controls)
            {
                if (c.Name == "lblFeeCost" + i.ToString())
                {
                    return c;
                }
            }
            return null;
        }

        #region IMoneyAlert 成员

        public int Print(System.Collections.ArrayList alPatientInfo, decimal payMoney)
        {
            foreach (FS.HISFC.Models.RADT.PatientInfo pInfo in alPatientInfo)
            {
                Print(pInfo, payMoney);
            }

            return 1;
        }

        #endregion
    }
}
