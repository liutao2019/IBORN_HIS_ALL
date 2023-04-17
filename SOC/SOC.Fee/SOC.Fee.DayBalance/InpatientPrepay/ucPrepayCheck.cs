using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SOC.Fee.DayBalance.InptientPrepay
{
    public partial class ucPrepayCheck : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucPrepayCheck()
        {
            InitializeComponent();
        }


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

        private FS.FrameWork.WinForms.Forms.ToolBarService toolbarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
        Manager.PrepayDayBalance PrepayMgr = new SOC.Fee.DayBalance.Manager.PrepayDayBalance();
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


        private void Init()
        {
            employeeHelp.ArrayObject = manager.QueryEmployeeAll();
            this.neuLblHosName.Text = FS.FrameWork.Management.Connection.Hospital.Name;
            dtEnd = conMgr.GetDateTimeFromSysDateTime();
            //开始时间为上线时间
            dtBegein = FS.FrameWork.Function.NConvert.ToDateTime("2010-03-10 00:00:00");
            //dtBegein = dtEnd.AddDays(-1);
            this.neuDateTimePicker1.Value = dtBegein;
            this.neuDateTimePicker2.Value = dtEnd;
            this.neuDateTimePicker3.Value = dtEnd;
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
            if (this.neuDateTimePicker2.Value < this.neuDateTimePicker1.Value)
            {
                MessageBox.Show("开始时间大于结束时间。请调整！");
                return;
            }
            if (this.neuSpread1_Sheet1.RowCount > 0)
                this.neuSpread1_Sheet1.Rows.Remove(0, this.neuSpread1_Sheet1.RowCount);
            Object.PrepayDayBalance prepay;
            ArrayList al = new ArrayList();
            al = PrepayMgr.GetPrepayDayBalanceByTime(personCode, this.neuDateTimePicker1.Value.ToString(), this.neuDateTimePicker2.Value.ToString(), isShow);
            if (al == null)
            {
                MessageBox.Show(PrepayMgr.Err);
                return;
            }
            else if (al.Count == 0)
            {
                MessageBox.Show("当前没有数据可以查询");
                return;
            }

            decimal decTotalAll = 0;
            decimal decTotalBack = 0;
            decimal decTotalMoney = 0;
            decimal decTotalCA = 0;
            decimal decTotalPos = 0;

            int i = 0;
            int row = 0;
            for (i = 0; i < al.Count; i++)
            {
                prepay = al[i] as Object.PrepayDayBalance;
                this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 1);
                row = this.neuSpread1_Sheet1.RowCount - 1;
                this.neuSpread1_Sheet1.Rows[row].Tag = prepay.ID;//结算序号
                this.neuSpread1_Sheet1.Cells[row, 0].Tag = prepay.BalancOper.ID;//工号
                this.neuSpread1_Sheet1.SetValue(row, 0, employeeHelp.GetName(prepay.BalancOper.ID).ToString(), false);//姓名
                this.neuSpread1_Sheet1.SetValue(row, 1, prepay.BeginDate.ToString(), false);//日结开始时间
                this.neuSpread1_Sheet1.SetValue(row, 2, prepay.EndDate.ToString(), false);//日结结束时间
                this.neuSpread1_Sheet1.SetValue(row, 3, prepay.BeginInvoice, false);//发票起始号
                this.neuSpread1_Sheet1.SetValue(row, 4, prepay.EndInvoice, false);//发票终止号
                this.neuSpread1_Sheet1.SetValue(row, 5, prepay.TotCost, false);//总金额
                this.neuSpread1_Sheet1.SetValue(row, 6, prepay.QuitCost, false);//退款
                this.neuSpread1_Sheet1.SetValue(row, 7, prepay.RealCost, false);//上交金额
                this.neuSpread1_Sheet1.SetValue(row, 8, prepay.CACost, false);//CA
                this.neuSpread1_Sheet1.SetValue(row, 9, prepay.POSCost, false);//POS
                if (prepay.CheckFlag == "1")
                {
                    this.neuSpread1_Sheet1.Cells[row, 10].Tag = prepay.CheckFlag;
                    this.neuSpread1_Sheet1.SetValue(row, 10, "已审核", false);//审核状态
                    this.neuSpread1_Sheet1.Cells[row, 11].Tag = prepay.CheckOper.ID;//审核人工号
                    this.neuSpread1_Sheet1.SetValue(row, 11, employeeHelp.GetName(prepay.CheckOper.ID).ToString(), false);//审核人姓名
                    this.neuSpread1_Sheet1.SetValue(row, 12, prepay.CheckOper.OperTime.ToString(), false);//审核时间
                }
                else
                {
                    this.neuSpread1_Sheet1.Cells[row, 10].Tag = prepay.CheckFlag;
                    this.neuSpread1_Sheet1.SetValue(row, 10, "未审核", false);//审核状态
                    this.neuSpread1_Sheet1.SetValue(row, 11, "", false);//审核人
                    this.neuSpread1_Sheet1.SetValue(row, 12, "", false);//审核时间
                }

                decTotalAll += prepay.TotCost;
                decTotalBack += prepay.QuitCost;
                decTotalMoney += prepay.RealCost;
                decTotalCA += prepay.CACost;
                decTotalPos += prepay.POSCost;
            }

            this.neuSpread1_Sheet1.RowCount += 1;
            row = this.neuSpread1_Sheet1.RowCount - 1;

            this.neuSpread1_Sheet1.SetValue(row, 4, "合计：", false);//发票终止号
            this.neuSpread1_Sheet1.SetValue(row, 5, decTotalAll, false);//总金额
            this.neuSpread1_Sheet1.SetValue(row, 6, decTotalBack, false);//退款
            this.neuSpread1_Sheet1.SetValue(row, 7, decTotalMoney, false);//上交金额
            this.neuSpread1_Sheet1.SetValue(row, 8, decTotalCA, false);//CA
            this.neuSpread1_Sheet1.SetValue(row, 9, decTotalPos, false);//POS

            this.neuSpread1_Sheet1.Cells[row, 10].Tag = "1";

        }


        /// <summary>
        /// 审核单条记录
        /// </summary>
        private void checkSingleRecord()
        {
            dtCheck = this.neuDateTimePicker3.Value;
            int rowCount = this.neuSpread1.ActiveSheet.ActiveRow.Index;
            string balanceNO = this.neuSpread1_Sheet1.Rows[rowCount].Tag.ToString();
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            int iReturn = 0;
            iReturn = PrepayMgr.updatePrepayCheckFlag(conMgr.Operator.ID, balanceNO, dtCheck.Date.ToString());
            if(iReturn==-1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("审核出错");
                return;
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show("审核成功");
            this.Query();
            this.neuSpread1.ActiveSheet.ActiveRowIndex = rowCount - 1;
        }

        /// <summary>
        /// 取消审核单条记录
        /// </summary>
        private void cancelCheckSingleRecord()
        {
            int rowCount = this.neuSpread1.ActiveSheet.ActiveRow.Index;
            string balanceNO = this.neuSpread1_Sheet1.Rows[rowCount].Tag.ToString();
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            int iReturn = 0;
            iReturn = PrepayMgr.updateCancelPrepayCheckFlag(balanceNO);
            if (iReturn == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("取消审核出错");
                return;
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show("取消审核成功");
            this.Query();
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
                        string strCheck = this.neuSpread1_Sheet1.Cells[rowCount, 10].Tag.ToString();
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
                        string strCheck = this.neuSpread1_Sheet1.Cells[rowCount, 10].Tag.ToString();
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
