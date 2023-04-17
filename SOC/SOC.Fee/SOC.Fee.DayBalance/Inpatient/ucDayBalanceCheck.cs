using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SOC.Fee.DayBalance.Inpatient
{
    public partial class ucDayBalanceCheck : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucDayBalanceCheck()
        {
            InitializeComponent();
        }

        private FS.FrameWork.WinForms.Forms.ToolBarService toolbarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
        FS.HISFC.BizLogic.Manager.Constant conMgr = new FS.HISFC.BizLogic.Manager.Constant();
        FS.HISFC.BizProcess.Integrate.Manager manager = new FS.HISFC.BizProcess.Integrate.Manager();
        FS.HISFC.BizLogic.Fee.InvoiceServiceNoEnum invoiceMgr = new FS.HISFC.BizLogic.Fee.InvoiceServiceNoEnum();
        FS.HISFC.Models.Base.EnumEmployeeType emplType = FS.HISFC.Models.Base.EnumEmployeeType.F;
        DateTime dtBegein = DateTime.MinValue;
        DateTime dtEnd = DateTime.MinValue;
        DateTime dtCheck = DateTime.MinValue;
        ArrayList alPersonconstantList = null;
        private string personCode = string.Empty;
        private string personName = string.Empty;
        FS.FrameWork.Public.ObjectHelper employeeHelp= new FS.FrameWork.Public.ObjectHelper();
        /// <summary>
        /// 日结业务层
        /// </summary>
        SOC.Fee.DayBalance.Manager.InpatientDayBalanceManage inpatientDayBalanceManage = new SOC.Fee.DayBalance.Manager.InpatientDayBalanceManage();

        /// <summary>
        /// 是否显示审核数据
        /// </summary>
        private string isShow = "0";//‘0’表示显示未审核，‘1’表示显示已审核，‘2’表示显示全部

        /// <summary>
        /// 是否显示已审核
        /// </summary>
        [Description("是否显示已审核，‘0’表示显示未审核，‘1’表示显示已审核，‘2’表示显示全部"), Category("设置")]
        public string IsShow
        {
            get
            {
                return this.isShow;
            }
            set
            {
                this.isShow = value;
            }
        }

        private void Init()
        {
            this.neuTabControl1.TabPages.Remove(tabPage2);

            employeeHelp.ArrayObject = manager.QueryEmployeeAll();
            this.neuLblHosName.Text = FS.FrameWork.Management.Connection.Hospital.Name;
            dtEnd = conMgr.GetDateTimeFromSysDateTime();
            //开始时间为上线时间
            dtBegein = FS.FrameWork.Function.NConvert.ToDateTime("2010-03-10 00:00:00");
            //dtBegein = dtEnd.AddDays(-1);
            this.dtpBegin.Value = dtBegein;
            this.dtpEnd.Value = dtEnd;
            this.dtpCheck.Value = dtEnd;
            //填充数据
            alPersonconstantList = manager.QueryEmployee(emplType);
            FS.HISFC.Models.Base.Employee allPerson = new FS.HISFC.Models.Base.Employee();
            allPerson.ID = "%%";
            allPerson.Name = "全部";
            allPerson.SpellCode = "QB";
            alPersonconstantList.Insert(0, allPerson);
            this.cboPersonCode.AddItems(alPersonconstantList);
            cboPersonCode.SelectedIndex = 0;
        }

        /// <summary>
        /// 查询
        /// </summary>
        private void Query()
        {
            if (this.dtpEnd.Value < this.dtpBegin.Value)
            {
                MessageBox.Show("开始时间大于结束时间。请调整！");
                return;
            }
            this.neuSpread1_Sheet1.RowCount = 0;

            DataTable dtDayBalance = null;
            int iRes = this.inpatientDayBalanceManage.QueryDayBalanceList(personCode, this.dtpBegin.Value.ToString(), this.dtpEnd.Value.ToString(), isShow, out dtDayBalance);
            if (iRes <= 0)
            {
                MessageBox.Show(inpatientDayBalanceManage.Err);
                return;
            }
            if (dtDayBalance == null || dtDayBalance.Rows.Count <= 0)
            {
                MessageBox.Show("当前没有数据可以查询");
                return;
            }

            DataRow drTemp = null;
            int iRowCount = dtDayBalance.Rows.Count;
            this.neuSpread1_Sheet1.RowCount = iRowCount;
            decimal decTemp = 0;
            decimal decCA = 0;
            decimal decPos = 0;

            decimal decTotalAll = 0;
            decimal decTotalPub = 0;
            decimal decTotalBack = 0;
            decimal decTotalCA = 0;
            decimal decTotalPos = 0;
            decimal decTotalMoney = 0;
            decimal decTotalPerMoney = 0;

            for (int idx = 0; idx < iRowCount; idx++)
            {
                drTemp = dtDayBalance.Rows[idx];

                this.neuSpread1_Sheet1.SetValue(idx, 0, drTemp["balance_no"].ToString().Trim(), false); // 结算序号
                this.neuSpread1_Sheet1.Cells[idx, 1].Tag = drTemp["oper_code"].ToString().Trim();       // 工号
                this.neuSpread1_Sheet1.SetValue(idx, 1, drTemp["opername"].ToString().Trim(), false);   // 姓名
                this.neuSpread1_Sheet1.SetValue(idx, 2, drTemp["begin_date"].ToString().Trim(), false);
                this.neuSpread1_Sheet1.SetValue(idx, 3, drTemp["end_date"].ToString().Trim(), false);

                this.neuSpread1_Sheet1.SetValue(idx, 4, drTemp["begin_invoice"].ToString().Trim(), false);
                this.neuSpread1_Sheet1.SetValue(idx, 5, drTemp["end_invoice"].ToString().Trim(), false);

                // 总收入
                decTemp = FS.FrameWork.Function.NConvert.ToDecimal(drTemp["act_tcost"]);
                decTotalAll += decTemp;
                this.neuSpread1_Sheet1.SetValue(idx, 6, decTemp, false);

                // 记账
                decTemp = FS.FrameWork.Function.NConvert.ToDecimal(drTemp["act_pcost02"]);
                decTemp += FS.FrameWork.Function.NConvert.ToDecimal(drTemp["act_pcost03"]);
                decTotalPub += decTemp;
                this.neuSpread1_Sheet1.SetValue(idx, 7, decTemp, false);

                // 退款
                decTemp = FS.FrameWork.Function.NConvert.ToDecimal(drTemp["qt_tcost"]);
                decTotalBack += decTemp;
                this.neuSpread1_Sheet1.SetValue(idx, 8, decTemp, false);
                this.neuSpread1_Sheet1.Columns[8].Visible = false;

                // 现金
                decCA = FS.FrameWork.Function.NConvert.ToDecimal(drTemp["ca_cost"]);
                decTotalCA += decCA;
                this.neuSpread1_Sheet1.SetValue(idx, 11, decCA, false);

                // POS
                decPos = FS.FrameWork.Function.NConvert.ToDecimal(drTemp["pos_cost"]);
                decPos += FS.FrameWork.Function.NConvert.ToDecimal(drTemp["ch_cost"]);
                decTotalPos += decPos;
                this.neuSpread1_Sheet1.SetValue(idx, 12, decPos, false);

                // 上交现金
                decTemp = decCA + decPos;
                decTotalMoney += decTemp;
                this.neuSpread1_Sheet1.SetValue(idx, 10, decTemp, false);

                // 预收款
                decTemp = FS.FrameWork.Function.NConvert.ToDecimal(drTemp["act_tcost"]) - FS.FrameWork.Function.NConvert.ToDecimal(drTemp["act_pcost02"]) - FS.FrameWork.Function.NConvert.ToDecimal(drTemp["act_pcost03"]) - decCA - decPos;
                decTotalPerMoney += decTemp;
                this.neuSpread1_Sheet1.SetValue(idx, 9, decTemp, false);

                if (drTemp["check_flag"].ToString().Trim() == "1")
                {
                    this.neuSpread1_Sheet1.Cells[idx, 13].Tag = "1";
                    this.neuSpread1_Sheet1.SetValue(idx, 13, "已审核", false);

                    this.neuSpread1_Sheet1.Cells[idx, 14].Tag = drTemp["check_opcd"].ToString().Trim();
                    this.neuSpread1_Sheet1.SetValue(idx, 14, drTemp["checkname"].ToString().Trim(), false);

                    this.neuSpread1_Sheet1.SetValue(idx, 15, drTemp["check_date"].ToString().Trim(), false);
                }
                else
                {
                    this.neuSpread1_Sheet1.Cells[idx, 13].Tag = "0";
                    this.neuSpread1_Sheet1.SetValue(idx, 13, "未审核", false);

                    this.neuSpread1_Sheet1.Cells[idx, 14].Tag = "";
                    this.neuSpread1_Sheet1.SetValue(idx, 14, "", false);

                    this.neuSpread1_Sheet1.SetValue(idx, 15, "", false);
                }
            }

            this.neuSpread1_Sheet1.RowCount += 1;

            iRowCount = this.neuSpread1_Sheet1.RowCount - 1;

            this.neuSpread1_Sheet1.SetValue(iRowCount, 5, "合计：", false);

            // 合计
            this.neuSpread1_Sheet1.SetValue(iRowCount, 6, decTotalAll, false);
            this.neuSpread1_Sheet1.SetValue(iRowCount, 7, decTotalPub, false);
            this.neuSpread1_Sheet1.SetValue(iRowCount, 8, decTotalBack, false);
            this.neuSpread1_Sheet1.Columns[8].Visible = false;

            this.neuSpread1_Sheet1.SetValue(iRowCount, 11, decTotalCA, false);
            this.neuSpread1_Sheet1.SetValue(iRowCount, 12, decTotalPos, false);
            this.neuSpread1_Sheet1.SetValue(iRowCount, 10, decTotalMoney, false);
            this.neuSpread1_Sheet1.SetValue(iRowCount, 9, decTotalPerMoney, false);

            this.neuSpread1_Sheet1.Cells[iRowCount, 13].Tag = "1";
            this.neuSpread1_Sheet1.SetValue(iRowCount, 13, "", false);

        }


        /// <summary>
        /// 审核单条记录
        /// </summary>
        private void checkSingleRecord()
        {
            dtCheck = this.dtpCheck.Value;
            int rowCount = this.neuSpread1.ActiveSheet.ActiveRow.Index;
            string balanceNO = this.neuSpread1_Sheet1.Cells[rowCount, 0].Text;
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            int iReturn = 0;
            iReturn = this.inpatientDayBalanceManage.UpdateDayBalanceCheck(balanceNO, conMgr.Operator.ID, dtCheck.Date.ToString(), "1");
            if(iReturn==-1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("审核出错");
                return;
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show("审核成功");
            //this.Query();

            this.neuSpread1_Sheet1.Cells[rowCount, 13].Tag = "1";
            this.neuSpread1_Sheet1.SetValue(rowCount, 13, "已审核", false);

            this.neuSpread1_Sheet1.Cells[rowCount, 14].Tag = conMgr.Operator.ID;
            this.neuSpread1_Sheet1.SetValue(rowCount, 14, conMgr.Operator.Name, false);

            this.neuSpread1_Sheet1.SetValue(rowCount, 15, dtCheck.Date.ToString(), false);

            this.neuSpread1.ActiveSheet.ActiveRowIndex = rowCount - 1;
        }

        /// <summary>
        /// 取消审核单条记录
        /// </summary>
        private void cancelCheckSingleRecord()
        {
            int rowCount = this.neuSpread1.ActiveSheet.ActiveRow.Index;
            string balanceNO = this.neuSpread1_Sheet1.Cells[rowCount, 0].Text;
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            int iReturn = 0;
            iReturn = this.inpatientDayBalanceManage.UpdateDayBalanceCheck(balanceNO, conMgr.Operator.ID, dtCheck.Date.ToString(), "0");
            if (iReturn == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("取消审核出错");
                return;
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show("取消审核成功");
            //this.Query();

            this.neuSpread1_Sheet1.Cells[rowCount, 13].Tag = "0";
            this.neuSpread1_Sheet1.SetValue(rowCount, 13, "未审核", false);

            this.neuSpread1_Sheet1.Cells[rowCount, 14].Tag = "";
            this.neuSpread1_Sheet1.SetValue(rowCount, 14, "", false);

            this.neuSpread1_Sheet1.SetValue(rowCount, 15, "", false);

            this.neuSpread1.ActiveSheet.ActiveRowIndex = rowCount - 1;
        }

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolbarService.AddToolButton("审核", "审核当条记录", FS.FrameWork.WinForms.Classes.EnumImageList.B保存, true, false, null);
            toolbarService.AddToolButton("取消审核", "取消审核当条记录", FS.FrameWork.WinForms.Classes.EnumImageList.Z作废, true, false, null);
            return toolbarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "审核":
                    {
                        int rowCount = this.neuSpread1.ActiveSheet.ActiveRow.Index;
                        string strCheck = this.neuSpread1_Sheet1.Cells[rowCount, 13].Tag.ToString();
                        if (strCheck == "1")
                        {
                            MessageBox.Show("该条记录已审核过，请选择其他记录审核");
                            return;
                        }
                        else
                        {
                            checkSingleRecord();
                            break;
                        }
                    }
                case "取消审核":
                    {
                        int rowCount = this.neuSpread1.ActiveSheet.ActiveRow.Index;
                        string strCheck = this.neuSpread1_Sheet1.Cells[rowCount, 13].Tag.ToString();
                        if (strCheck != "1")
                        {
                            MessageBox.Show("该条记录还未审核，请选择其他取消审核");
                            return;
                        }
                        else
                        {
                            cancelCheckSingleRecord();
                            break;
                        }
                    }
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        protected override void OnLoad(EventArgs e)
        {
            this.Init();
            base.OnLoad(e);
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            this.Query();
            return 1;
        }

        private void cboPersonCode_KeyDown(object sender, KeyEventArgs e)
        {
            this.Query();
        }

        private void cboPersonCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboPersonCode.SelectedIndex >= 0)
            {
                personCode = ((FS.HISFC.Models.Base.Employee)alPersonconstantList[this.cboPersonCode.SelectedIndex]).ID.ToString();
                personName = ((FS.HISFC.Models.Base.Employee)alPersonconstantList[this.cboPersonCode.SelectedIndex]).Name.ToString();
            }
        }

        









    }
}
