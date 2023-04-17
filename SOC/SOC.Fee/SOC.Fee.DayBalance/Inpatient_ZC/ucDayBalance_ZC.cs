using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SOC.Fee.DayBalance.Manager;
using FS.FrameWork.Models;

namespace SOC.Fee.DayBalance.Inpatient_ZC
{
    public partial class ucDayBalance_ZC : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucDayBalance_ZC()
        {
            InitializeComponent();
        }

        #region 
        /// <summary>
        /// 日结业务
        /// </summary>
        InpatientDayBalanceManage_ZC dayBalanceManage = new InpatientDayBalanceManage_ZC();
        private FS.FrameWork.WinForms.Forms.ToolBarService toolbarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
        FS.HISFC.BizLogic.Manager.PageSize pageSizeMgr = new FS.HISFC.BizLogic.Manager.PageSize();
        FS.HISFC.Models.Base.PageSize pageSize;
        /// <summary>
        /// 当前操作员
        /// </summary>
        FS.HISFC.Models.Base.Employee currentOperator = null;
        /// <summary>
        /// 本次日结记录
        /// </summary>
        List<string> lstBalanceData = null;

        string strLastBeginDate = null;

        #endregion


        private void ctlBalanceDate_Load(object sender, EventArgs e)
        {
            currentOperator = this.dayBalanceManage.Operator as FS.HISFC.Models.Base.Employee;
            
            string strCurrentDate = null;
            int iRes = dayBalanceManage.GetLastDayBalanceDate(currentOperator.ID, out strLastBeginDate, out strCurrentDate);
            if (iRes <= 0)
            {
                MessageBox.Show("查询最近一次日结时间失败！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(this.strLastBeginDate))
            {
                this.strLastBeginDate = DateTime.MinValue.ToString("yyyy-MM-dd HH:mm:ss");
            }

            this.ctlBalanceDate.DtLastBalance = Convert.ToDateTime(this.strLastBeginDate);

            lstBalanceData = new List<string>();

        }


        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolbarService.AddToolButton("历史查询", "查询指定月份的日结记录！", (int)FS.FrameWork.WinForms.Classes.EnumImageList.C查询历史, true, false, null);
            toolbarService.AddToolButton("日结", "统计本次日结信息", (int)FS.FrameWork.WinForms.Classes.EnumImageList.C查询, true, false, null);
            toolbarService.AddToolButton("清空", "清空日结数据", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q清空, true, false, null);

            return this.toolbarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "历史查询":
                    lstBalanceData.Clear();
                    QueryHistoryDayBalanceInfo();
                    break;

                case "日结":
                    lstBalanceData.Clear();
                    QueryDayBalance();

                    break;

                case "清空":
                    lstBalanceData.Clear();
                    this.ClearBalance();
                    break;
            }

            base.ToolStrip_ItemClicked(sender, e);
        }

        public override int Save(object sender, object neuObject)
        {
            SaveDayBalanceData();
            return 1;
        }

        public override int Print(object sender, object neuObject)
        {
            PrintInfo();
            return base.Print(sender, neuObject);
        }

        #region 方法
        /// <summary>
        /// 查询历史日结信息
        /// </summary>
        private void QueryHistoryDayBalanceInfo()
        {
            sheetView1.RowCount = 0;

            DateTime dtHistoryMonth = Convert.ToDateTime(dtpHistory.Value.ToString("yyyy-MM-01 00:00:00"));
            DataTable dtHistoryInfo = null;

            int iRes = this.dayBalanceManage.QueryHistoryBalanceData(currentOperator.ID, dtHistoryMonth.ToString("yyyy-MM-dd HH:mm:ss"), dtHistoryMonth.AddMonths(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss"), out dtHistoryInfo);

            if (iRes <= 0)
            {
                MessageBox.Show("查询历史日结记录失败！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (dtHistoryInfo == null || dtHistoryInfo.Rows.Count <= 0)
            {
                MessageBox.Show("指定查询月份无有效日结记录！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


            DataRow dr = null;
            sheetView1.RowCount = dtHistoryInfo.Rows.Count;
            for (int idx = 0; idx < dtHistoryInfo.Rows.Count; idx++)
            {
                dr = dtHistoryInfo.Rows[idx];

                sheetView1.Cells[idx, 0].Tag = dr["balance_no"].ToString().Trim();
                sheetView1.Cells[idx, 0].Text = dr["begin_date"].ToString() + " - " + dr["end_date"].ToString();

            }

        }
        /// <summary>
        /// 统计本次日结
        /// </summary>
        private void QueryDayBalance()
        {
            ClearBalance();

            string beginDate = this.strLastBeginDate;
            string endDate = "";

            int iRes = this.ctlBalanceDate.GetBalanceDate(ref endDate);
            if (iRes <= 0)
            {
                return;
            }

            DataTable dtBillMoney = null;
            iRes = this.dayBalanceManage.QueryDayBalanceBillMoney(this.currentOperator.ID, beginDate, endDate, out dtBillMoney);
            if (iRes <= 0)
            {
                MessageBox.Show("查询日结费用信息失败！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DataTable dtPrepayMoney = null;
            iRes = this.dayBalanceManage.QueryDayBalancePrepayMoney(this.currentOperator.ID, beginDate, endDate, out dtPrepayMoney);
            if (iRes <= 0)
            {
                MessageBox.Show("查询日结预收款信息失败！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DataTable dtBalancePaymode = null;
            iRes = this.dayBalanceManage.QueryDayBalanceBalancePayMode(this.currentOperator.ID, beginDate, endDate, out dtBalancePaymode);
            if (iRes <= 0)
            {
                MessageBox.Show("查询日结结算支付信息失败！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            SetBalanceData(beginDate, endDate, dtBillMoney, dtPrepayMoney, dtBalancePaymode);
        }
        /// <summary>
        /// 设置日结信息
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="dtBillMoney"></param>
        /// <param name="dtPrepayMoney"></param>
        /// <param name="dtBalancePaymode"></param>
        private void SetBalanceData(string beginDate, string endDate, DataTable dtBillMoney, DataTable dtPrepayMoney, DataTable dtBalancePaymode)
        {
            this.neuSpread1_Sheet1.Cells[1, 2].Text = this.currentOperator.Name + "(" + this.currentOperator.ID + ")";
            this.neuSpread1_Sheet1.Cells[1, 7].Text = beginDate + " -- " + endDate;

            lstBalanceData.Add(beginDate);
            lstBalanceData.Add(endDate);
            lstBalanceData.Add(this.currentOperator.ID);
            lstBalanceData.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            lstBalanceData.Add(this.currentOperator.Dept.ID);


            // 发票合计
            decimal decInvoiceTotal = 0;
            decInvoiceTotal = FS.FrameWork.Function.NConvert.ToDecimal(dtBillMoney.Compute("Sum(tot_cost)", ""));
            this.neuSpread1_Sheet1.Cells[3, 1].Text = decInvoiceTotal.ToString();

            lstBalanceData.Add(decInvoiceTotal.ToString());
            lstBalanceData.Add("0");
            lstBalanceData.Add("0");
            lstBalanceData.Add("0");

            // 冲销预收款
            decimal decPayperpayTotal = 0;
            decPayperpayTotal = FS.FrameWork.Function.NConvert.ToDecimal(dtBillMoney.Compute("Sum(prepay_cost)", ""));
            this.neuSpread1_Sheet1.Cells[4, 1].Text = decPayperpayTotal.ToString();

            lstBalanceData.Add(decPayperpayTotal.ToString());

            // 预收款
            decimal decPrepayTotal = 0;
            decPrepayTotal = FS.FrameWork.Function.NConvert.ToDecimal(dtPrepayMoney.Compute("Sum(prepay_cost)", ""));
            this.neuSpread1_Sheet1.Cells[4, 3].Text = decPrepayTotal.ToString();

            lstBalanceData.Add(decPrepayTotal.ToString());

            // 预收款 -- 现金
            decimal decPrepayCA = 0;
            decPrepayCA = FS.FrameWork.Function.NConvert.ToDecimal(dtPrepayMoney.Compute("Sum(prepay_cost)", "pay_way = 'CA'"));
            this.neuSpread1_Sheet1.Cells[4, 5].Text = decPrepayCA.ToString();

            lstBalanceData.Add(decPrepayCA.ToString());


            // 预收款 -- 信用卡
            decimal decPrepayCD = 0;
            decPrepayCD = FS.FrameWork.Function.NConvert.ToDecimal(dtPrepayMoney.Compute("Sum(prepay_cost)", "pay_way = 'CD'"));
            this.neuSpread1_Sheet1.Cells[4, 7].Text = decPrepayCD.ToString();

            lstBalanceData.Add(decPrepayCD.ToString());

            // 预收款 -- 其他
            decimal decPrepayOther = 0;
            decPrepayOther = FS.FrameWork.Function.NConvert.ToDecimal(dtPrepayMoney.Compute("Sum(prepay_cost)", "pay_way <> 'CA' and pay_way <> 'CD'"));
            this.neuSpread1_Sheet1.Cells[4, 9].Text = decPrepayOther.ToString();

            lstBalanceData.Add(decPrepayOther.ToString());

            // 总现金
            decimal decBalanceCA = 0;
            decBalanceCA = FS.FrameWork.Function.NConvert.ToDecimal(dtBalancePaymode.Compute("Sum(cost)", "pay_way = 'CA' and reutrnorsupply_flag = '1'"));
            decBalanceCA -= FS.FrameWork.Function.NConvert.ToDecimal(dtBalancePaymode.Compute("Sum(cost)", "pay_way = 'CA' and reutrnorsupply_flag = '2'"));
            this.neuSpread1_Sheet1.Cells[5, 1].Text = (decBalanceCA + decPrepayCA).ToString();

            lstBalanceData.Add(decBalanceCA.ToString());

            // 总信用卡
            decimal decBalanceCD = 0;
            decBalanceCD = FS.FrameWork.Function.NConvert.ToDecimal(dtBalancePaymode.Compute("Sum(cost)", "pay_way = 'CD' and reutrnorsupply_flag = '1'"));
            decBalanceCD -= FS.FrameWork.Function.NConvert.ToDecimal(dtBalancePaymode.Compute("Sum(cost)", "pay_way = 'CD' and reutrnorsupply_flag = '2'"));
            this.neuSpread1_Sheet1.Cells[5, 5].Text = (decBalanceCD + decPrepayCD).ToString();

            lstBalanceData.Add(decBalanceCD.ToString());

            // 总支票收
            decimal decBalanceOther = 0;
            decBalanceOther = FS.FrameWork.Function.NConvert.ToDecimal(dtBalancePaymode.Compute("Sum(cost)", "pay_way <> 'CA' and pay_way <> 'CD' and reutrnorsupply_flag = '1'"));
            this.neuSpread1_Sheet1.Cells[5, 3].Text = (decBalanceOther + decPrepayOther).ToString();

            lstBalanceData.Add(decBalanceOther.ToString());

            // 总支票退
            decimal decBalanceOtherReturn = 0;
            decBalanceOtherReturn = FS.FrameWork.Function.NConvert.ToDecimal(dtBalancePaymode.Compute("Sum(cost)", "pay_way <> 'CA' and pay_way <> 'CD' and reutrnorsupply_flag = '2'"));
            this.neuSpread1_Sheet1.Cells[5, 7].Text = decBalanceOtherReturn.ToString();

            lstBalanceData.Add(decBalanceOtherReturn.ToString());

            // 优惠
            decimal decChargeJZ = 0;
            decChargeJZ = FS.FrameWork.Function.NConvert.ToDecimal(dtBillMoney.Compute("Sum(der_cost)", ""));
            decChargeJZ += FS.FrameWork.Function.NConvert.ToDecimal(dtBillMoney.Compute("Sum(eco_cost)", ""));
            this.neuSpread1_Sheet1.Cells[6, 1].Text = decChargeJZ.ToString();

            lstBalanceData.Add(decChargeJZ.ToString());
            lstBalanceData.Add("0");
            lstBalanceData.Add("0");
            
            // 医保金额
            decimal decPubCost = 0;
            decPubCost = FS.FrameWork.Function.NConvert.ToDecimal(dtBillMoney.Compute("Sum(pub_cost)", ""));
            this.neuSpread1_Sheet1.Cells[6, 11].Text = decPubCost.ToString();

            lstBalanceData.Add(decPubCost.ToString());

            // 收入合计
            this.neuSpread1_Sheet1.Cells[3, 11].Text = (decInvoiceTotal - decPubCost).ToString();

            // 支出合计
            this.neuSpread1_Sheet1.Cells[4, 11].Text = decPayperpayTotal.ToString();

            // 应交合计
            this.neuSpread1_Sheet1.Cells[5, 11].Text = (decBalanceCA + decPrepayCA + decBalanceCD + decPrepayCD + decBalanceOther + decPrepayOther + decBalanceOtherReturn).ToString();

            // 扣除医保金额实交
            this.neuSpread1_Sheet1.Cells[7, 2].Text = (decInvoiceTotal - decPubCost).ToString();

            this.neuSpread1_Sheet1.Cells[7, 3].Text = "(现金：" + (decInvoiceTotal - decPubCost - decBalanceCD - (decBalanceOther - decBalanceOtherReturn) - decChargeJZ).ToString()
                                                      + "  信用卡：" + decBalanceCD.ToString()
                                                      + "  支票：" + (decBalanceOther - decBalanceOtherReturn).ToString()
                                                      + "  其他：" + decChargeJZ.ToString() + ")";

            string strTemp = "";
            string strFlag = "";

            List<string> lstInvoice = new List<string>();
            List<string> lstPrintInvoice = new List<string>();
            List<string> lstReturnInvoice = new List<string>();

            foreach (DataRow dr in dtBillMoney.Rows)
            {
                strTemp = dr["invoice_no"].ToString().Trim();
                strFlag = dr["trans_type"].ToString().Trim();
                if (!lstInvoice.Contains(strTemp))
                {
                    lstInvoice.Add(strTemp);
                }
                if (strFlag == "2")
                {
                    if (!lstReturnInvoice.Contains(strTemp))
                    {
                        lstReturnInvoice.Add(strTemp);
                    }
                }

                strTemp = dr["print_invoiceno"].ToString().Trim();
                if (!lstPrintInvoice.Contains(strTemp))
                {
                    lstPrintInvoice.Add(strTemp);
                }
            }
            if (lstInvoice.Count > 0)
            {
                lstInvoice.Sort();
                strTemp = lstInvoice[0] + "  至  " + lstInvoice[lstInvoice.Count - 1];
                this.neuSpread1_Sheet1.Cells[9, 1].Text = strTemp + "  共 ";
                this.neuSpread1_Sheet1.Cells[9, 5].Text = lstInvoice.Count.ToString();

                lstBalanceData.Add(strTemp);
                lstBalanceData.Add(lstInvoice.Count.ToString());
            }
            else
            {
                lstBalanceData.Add("");
                lstBalanceData.Add("0");
            }
            if (lstPrintInvoice.Count > 0)
            {
                lstPrintInvoice.Sort();
                strTemp = lstPrintInvoice[0] + "  至  " + lstPrintInvoice[lstInvoice.Count - 1];
                this.neuSpread1_Sheet1.Cells[9, 8].Text = strTemp;

                lstBalanceData.Add(strTemp);
            }
            else
            {
                lstBalanceData.Add("");
            }

            lstBalanceData.Add("");
            lstBalanceData.Add("0");

            // 退费数
            if (lstReturnInvoice.Count > 0)
            {
                this.neuSpread1_Sheet1.Cells[13, 1].Text = lstReturnInvoice.Count.ToString() + " 张";

                strTemp = "";
                foreach (string str in lstReturnInvoice)
                {
                    strTemp += str + "、";
                }
                strTemp = strTemp.Trim(new char[] { '、' });

                this.neuSpread1_Sheet1.Cells[13, 3].Text = strTemp;

                lstBalanceData.Add(strTemp);
                lstBalanceData.Add(lstReturnInvoice.Count.ToString());
            }
            else
            {
                lstBalanceData.Add("");
                lstBalanceData.Add("0");
            }

        }
        /// <summary>
        /// 设置日结信息
        /// </summary>
        /// <param name="dtDayBalanceData"></param>
        private void SetBalanceData(DataTable dtDayBalanceData)
        {
            if (dtDayBalanceData == null || dtDayBalanceData.Rows.Count <= 0)
                return;

            DataRow dr = dtDayBalanceData.Rows[0];

            this.neuSpread1_Sheet1.Cells[1, 2].Text = this.currentOperator.Name + "(" + this.currentOperator.ID + ")";
            this.neuSpread1_Sheet1.Cells[1, 7].Text = dr["begin_date"].ToString() + " -- " + dr["end_date"].ToString();

            // 发票合计
            decimal decInvoiceTotal = 0;
            decInvoiceTotal = FS.FrameWork.Function.NConvert.ToDecimal(dr["INVICE_TCOST"].ToString());
            this.neuSpread1_Sheet1.Cells[3, 1].Text = decInvoiceTotal.ToString();

            // 冲销预收款
            decimal decPayperpayTotal = 0;
            decPayperpayTotal = FS.FrameWork.Function.NConvert.ToDecimal(dr["PAYPREPAY_TCOST"].ToString());
            this.neuSpread1_Sheet1.Cells[4, 1].Text = decPayperpayTotal.ToString();

            // 预收款
            decimal decPrepayTotal = 0;
            decPrepayTotal = FS.FrameWork.Function.NConvert.ToDecimal(dr["PREPAY_TCOST"].ToString());
            this.neuSpread1_Sheet1.Cells[4, 3].Text = decPrepayTotal.ToString();

            // 预收款 -- 现金
            decimal decPrepayCA = 0;
            decPrepayCA = FS.FrameWork.Function.NConvert.ToDecimal(dr["PREPAYCA_COST"].ToString());
            this.neuSpread1_Sheet1.Cells[4, 5].Text = decPrepayCA.ToString();

            // 预收款 -- 信用卡
            decimal decPrepayCD = 0;
            decPrepayCD = FS.FrameWork.Function.NConvert.ToDecimal(dr["PREPAYCD_COST"].ToString());
            this.neuSpread1_Sheet1.Cells[4, 7].Text = decPrepayCD.ToString();

            // 预收款 -- 其他
            decimal decPrepayOther = 0;
            decPrepayOther = FS.FrameWork.Function.NConvert.ToDecimal(dr["PREPAYOTHER_COST"].ToString());
            this.neuSpread1_Sheet1.Cells[4, 7].Text = decPrepayOther.ToString();

            // 总现金
            decimal decBalanceCA = 0;
            decBalanceCA = FS.FrameWork.Function.NConvert.ToDecimal(dr["CHARGEPAYCA_COST"].ToString());
            this.neuSpread1_Sheet1.Cells[5, 1].Text = (decBalanceCA + decPrepayCA).ToString();

            // 总信用卡
            decimal decBalanceCD = 0;
            decBalanceCD = FS.FrameWork.Function.NConvert.ToDecimal(dr["CHARGEPAYCD_COST"].ToString());
            this.neuSpread1_Sheet1.Cells[5, 5].Text = (decBalanceCD + decPrepayCD).ToString();

            // 总支票收
            decimal decBalanceOther = 0;
            decBalanceOther = FS.FrameWork.Function.NConvert.ToDecimal(dr["CHARGEPAYOTHER_COST"].ToString());
            this.neuSpread1_Sheet1.Cells[5, 3].Text = (decBalanceOther + decPrepayOther).ToString();
            // 总支票退
            decimal decBalanceOtherReturn = 0;
            decBalanceOtherReturn = FS.FrameWork.Function.NConvert.ToDecimal(dr["CHARGEPAYOTHERRETURN_COST"].ToString());
            this.neuSpread1_Sheet1.Cells[5, 7].Text = decBalanceOtherReturn.ToString();
            // 优惠
            decimal decChargeJZ = 0;
            decChargeJZ = FS.FrameWork.Function.NConvert.ToDecimal(dr["CHARGEJZ_COST"].ToString());
            this.neuSpread1_Sheet1.Cells[6, 1].Text = decChargeJZ.ToString();

            // 医保金额
            decimal decPubCost = 0;
            decPubCost = FS.FrameWork.Function.NConvert.ToDecimal(dr["PUB_COST"].ToString());
            this.neuSpread1_Sheet1.Cells[6, 11].Text = decPubCost.ToString();

            // 收入合计
            this.neuSpread1_Sheet1.Cells[3, 11].Text = (decInvoiceTotal - decPubCost).ToString();

            // 支出合计
            this.neuSpread1_Sheet1.Cells[4, 11].Text = decPayperpayTotal.ToString();

            // 应交合计
            this.neuSpread1_Sheet1.Cells[5, 11].Text = (decBalanceCA + decBalanceCD + decBalanceOther + decBalanceOtherReturn + decPrepayTotal).ToString();

            // 扣除医保金额实交
            this.neuSpread1_Sheet1.Cells[7, 2].Text = (decInvoiceTotal - decPubCost).ToString();

            this.neuSpread1_Sheet1.Cells[7, 3].Text = "(现金：" + (decInvoiceTotal - decPubCost - decBalanceCD - (decBalanceOther - decBalanceOtherReturn) - decChargeJZ).ToString()
                                                      + "  信用卡：" + decBalanceCD.ToString()
                                                      + "  支票：" + (decBalanceOther - decBalanceOtherReturn).ToString()
                                                      + "  其他：" + decChargeJZ.ToString();

            this.neuSpread1_Sheet1.Cells[9, 1].Text = dr["INVOICENO"].ToString();
            this.neuSpread1_Sheet1.Cells[9, 5].Text = dr["INVOICECOUNT"].ToString();
            this.neuSpread1_Sheet1.Cells[9, 8].Text = dr["PRINTINVOICENO"].ToString();

            this.neuSpread1_Sheet1.Cells[13, 1].Text = dr["QUITINVOICECOUNT"].ToString();
            this.neuSpread1_Sheet1.Cells[13, 3].Text = dr["QUITINVOICENO"].ToString();

        }

        /// <summary>
        /// 清空日结信息
        /// </summary>
        private void ClearBalance()
        {
            this.neuSpread1_Sheet1.Tag = null;
            if (lstBalanceData != null)
            {
                lstBalanceData.Clear();
            }

            for (int iRow = 0; iRow < this.neuSpread1_Sheet1.RowCount; iRow++)
            {
                for (int iCol = 0; iCol < this.neuSpread1_Sheet1.ColumnCount; iCol++)
                {
                    if (this.neuSpread1_Sheet1.Cells[iRow, iCol].Tag != null && !string.IsNullOrEmpty(this.neuSpread1_Sheet1.Cells[iRow, iCol].Tag.ToString()))
                    {
                        this.neuSpread1_Sheet1.Cells[iRow, iCol].Text = "";
                    }
                }
            }
        }
        /// <summary>
        /// 保存日结数据
        /// </summary>
        private void SaveDayBalanceData()
        {
            if (lstBalanceData == null || lstBalanceData.Count <= 0)
                return;
            if (lstBalanceData.Count != 29)
            {
                MessageBox.Show("日结记录数据不对！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            string strBalanceNO = "";
            int iRes = this.dayBalanceManage.GetBalanceNO(out strBalanceNO);
            if (iRes <= 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("获取日结号失败！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            lstBalanceData.Insert(0, strBalanceNO);

            iRes = this.dayBalanceManage.SaveDayBalanceRecord(lstBalanceData.ToArray());
            if (iRes <= 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("保存日结记录失败！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            iRes = this.dayBalanceManage.UpdateDayBalanceFlag(strBalanceNO, this.currentOperator.ID, this.currentOperator.ID, lstBalanceData[1], lstBalanceData[2]);
            if (iRes <= 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("更新日结状态失败！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 写收入表
            iRes = this.dayBalanceManage.BuildFeeIncome(strBalanceNO, this.currentOperator.ID, lstBalanceData[1], lstBalanceData[2]);
            if (iRes <= 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("生成住院实收信息失败！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            PrintInfo();



            MessageBox.Show("日结成功！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);



        }

        private void PrintInfo()
        {
            //FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            //print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            //print.PrintPage(10, 20, pnlReport);
            try
            {
                FS.FrameWork.WinForms.Classes.Print print = null;
                try
                {
                    print = new FS.FrameWork.WinForms.Classes.Print();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("初始化打印机失败!" + ex.Message);
                }
                //获得打印机名
                //string printer = this.controlIntegrate.GetControlParam<string>("ZYYJFP", true, "");
                //if (!string.IsNullOrEmpty(printer))
                //{
                //    print.PrintDocument.PrinterSettings.PrinterName = printer;
                //}
                //FS.HISFC.Models.Base.PageSize ps = new FS.HISFC.Models.Base.PageSize("", 866, 366);
                //print.SetPageSize(ps);
                //print.PrintPage(0, 0, this.neuPanel1);
                if (pageSize == null)
                {
                    pageSize = pageSizeMgr.GetPageSize("ZYYJFP");
                    if (pageSize != null && pageSize.Printer.ToLower() == "default")
                    {
                        pageSize.Printer = "";
                    }
                    if (pageSize == null)
                    {
                        pageSize = new FS.HISFC.Models.Base.PageSize("ZYYJFP", 840, 367);
                    }
                }
                print.SetPageSize(pageSize);
                print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
                print.IsDataAutoExtend = false;

                //try
                //{
                //    //普济分院4号窗口自动打印总是暂停：打印机针头或纸张太薄或太厚都可能引起暂停
                //    FS.FrameWork.WinForms.Classes.Print.ResumePrintJob();
                //}
                //catch { }
                if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
                {
                    print.PrintPreview(10, 20, pnlReport);
                }
                else
                {
                    print.PrintPage(10, 20, pnlReport);
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        #endregion

        private void neuSpread2_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            int iRow = e.Row;
            if (iRow < 0)
                return;

            this.ClearBalance();

            string strBalanceNO = this.sheetView1.Cells[iRow, 0].Tag.ToString();

            DataTable dtBalanceData = null;
            int iRes = this.dayBalanceManage.QueryDayBalanceDataByBalanceNO(strBalanceNO, out dtBalanceData);
            if (iRes <= 0 || dtBalanceData == null || dtBalanceData.Rows.Count <= 0)
            {
                MessageBox.Show("获取日结数据失败！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            this.SetBalanceData(dtBalanceData);
        }

    }
}
