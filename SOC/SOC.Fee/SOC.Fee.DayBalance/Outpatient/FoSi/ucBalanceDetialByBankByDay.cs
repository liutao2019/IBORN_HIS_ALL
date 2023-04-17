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

namespace SOC.Fee.DayBalance.Outpatient.FoSi
{
    public partial class ucBalanceDetialByBankByDay : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        /// <summary>
        /// 统计大类业务类
        /// </summary>
        FS.HISFC.BizLogic.Fee.FeeCodeStat feecodeStat = new FS.HISFC.BizLogic.Fee.FeeCodeStat();
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

        #region 属性
        /// <summary>
        /// 报表标题
        /// </summary>
        [Description("报表标题"), Category("设置")]
        public string ReportTitle
        {
            get
            {
                return reportTitle;
            }
            set
            {
                reportTitle = value;
            }
        }
        /// <summary>
        /// 报表标题
        /// </summary>
        private string reportTitle = "";
        #endregion

        public ucBalanceDetialByBankByDay()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 清空显示
        /// </summary>
        protected void Clear(string startDate, string endDate)
        {
            neuSpread1_Sheet1.RowCount = 0;

            //显示报表日结时间和制表员
            string strSpace = "               ";
            string strInfo = "制表员：" + feecodeStat.Operator.Name + strSpace + "时间段：" + startDate + " --- " + endDate;
            this.lblReportInfo.Text = strInfo;

            FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.neuSpread1_Sheet1, this.filePathBalanceDetialByBank);
        }


        protected override int OnQuery(object sender, object neuObject)
        {
            int iRes = 0;

            string startDate = "";
            string endDate = "";
            if (rdbDayBalance.Checked)
            {
                FS.FrameWork.Models.NeuObject obj = cmdDayBalance.SelectedItem as FS.FrameWork.Models.NeuObject;

                //加个提示
                if(obj==null)
                {
                    MessageBox.Show("未找到你的日结记录！");
                    return -1;
                }
                startDate = obj.User01;
                endDate = obj.User02;
            }
            else
            {
                startDate = this.dtpStart.Value.Date.ToString("yyyy-MM-dd HH:mm:ss");
                endDate = this.dtpEnd.Value.Date.ToString("yyyy-MM-dd 23:59:59");
            }

            Clear(startDate, endDate);

            string payMode = "NH','JH";

            DataTable dtDetial = null;
            if (radioButton1.Checked)
            {
                iRes = opDayBalance.QueryDayBalnaceDetialByNotBalance(feecodeStat.Operator.ID, payMode, out dtDetial);
                this.lblReportInfo.Text = "制表员： " + feecodeStat.Operator.Name;
            }
            else 
            {
                iRes = opDayBalance.QueryDayBalnaceDetialByBankByDay(feecodeStat.Operator.ID, startDate, endDate, payMode, out dtDetial);
            }
            if (iRes <= 0)
            {
                MessageBox.Show(opDayBalance.Err);
                return -1;
            }
            //this.neuSpread1_Sheet1.DataSource = dtDetial;
            this.getdtfp();
            foreach (DataRow rowDetial in dtDetial.Rows)
            {
                DataRow row = this.dtBalanceDetial.NewRow();

                row["操作时间"] = FS.FrameWork.Function.NConvert.ToDateTime(rowDetial[0]).ToString("yyyy-MM-dd");
                row["类型"] = rowDetial[1];
                row["金额"] = rowDetial[2];

                this.dtBalanceDetial.Rows.Add(row);

            }

            return iRes;
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            // 打印纸张设置
            FS.HISFC.Models.Base.PageSize ps = null;
            ps = psManager.GetPageSize("MZYHDZ");

            if (ps == null)
            {
                ps = new FS.HISFC.Models.Base.PageSize("MZYHDZ", 820, 1100);
                ps.Top = 0;
                ps.Left = 0;

            }

            print.SetPageSize(ps);

            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            print.PrintPage(0, 0, pnlSumary);

            return 1;
        }

        private void rdbDate_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbDate.Checked)
            {
                pnlTime.Visible = true;
                pnlTime.Dock = DockStyle.Fill;
                pnlTime.BringToFront();
            }
            else
            {
                pnlDayBalance.Visible = true;
                pnlDayBalance.Dock = DockStyle.Fill;
                pnlDayBalance.BringToFront();
            }
        }

        private void ucBalanceDetialByBankByDay_Load(object sender, EventArgs e)
        {
            string startDate = "";
            string endDate = "";

            DateTime dtNow = opDayBalance.GetDateTimeFromSysDateTime();
            endDate = dtNow.ToString("yyyy-MM-dd HH:mm:ss");
            startDate = dtNow.AddMonths(-1).ToString("yyyy-MM-dd HH:mm:ss");

            ArrayList arlRecords = null;
            int iRes = opDayBalance.QueryDayBalanceRecord(feecodeStat.Operator.ID, startDate, endDate, out arlRecords);
            if (arlRecords != null && arlRecords.Count>0)
            {
                cmdDayBalance.AddItems(arlRecords);
                this.cmdDayBalance.SelectedIndex = 0;
            }

            this.rdbDayBalance.Checked = true;

            ////如果存在本地药品的列配置文件,直接读取配置文件生成DataTable
            //if (System.IO.File.Exists(this.filePathBalanceDetialByBank))
            //{
            //    FS.FrameWork.WinForms.Classes.CustomerFp.CreatColumnByXML(this.filePathBalanceDetialByBank, dtBalanceDetial, ref dvBalanceDetial, this.neuSpread1_Sheet1);

            //    FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.neuSpread1_Sheet1, this.filePathBalanceDetialByBank);

            //}
            //else//如果没有找到配置文件,已默认的列顺序和名称生成DataTable
            //{
            //    this.dtBalanceDetial.Columns.AddRange(new DataColumn[]
            //    {
            //        new DataColumn("操作时间", typeof(string)),
            //        new DataColumn("类型", typeof(string)),
            //        new DataColumn("金额", typeof(decimal)),
            //     });

            //    dvBalanceDetial = new DataView(dtBalanceDetial);

            //    //绑定到对应的Farpoint
            //    this.neuSpread1_Sheet1.DataSource = dvBalanceDetial;

            //    //保存当前的列顺序,名称等信息到XML
            //    FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.neuSpread1_Sheet1, this.filePathBalanceDetialByBank);

            //}
        }
        private void getdtfp()
        {
            this.dtBalanceDetial = new DataTable();
            if (System.IO.File.Exists(this.filePathBalanceDetialByBank))
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.CreatColumnByXML(this.filePathBalanceDetialByBank, dtBalanceDetial, ref dvBalanceDetial, this.neuSpread1_Sheet1);

                FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.neuSpread1_Sheet1, this.filePathBalanceDetialByBank);

            }
            else//如果没有找到配置文件,已默认的列顺序和名称生成DataTable
            {
                this.dtBalanceDetial.Columns.AddRange(new DataColumn[]
                {
                    new DataColumn("操作时间", typeof(string)),
                    new DataColumn("类型", typeof(string)),
                    new DataColumn("金额", typeof(decimal)),
                 });

                dvBalanceDetial = new DataView(dtBalanceDetial);

                //绑定到对应的Farpoint
                this.neuSpread1_Sheet1.DataSource = dvBalanceDetial;

                //保存当前的列顺序,名称等信息到XML
                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.neuSpread1_Sheet1, this.filePathBalanceDetialByBank);

            }
        }
        private void neuSpread1_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.neuSpread1_Sheet1, this.filePathBalanceDetialByBank);
        }

        private void cmdDayBalance_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.OnQuery(sender,e);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                pnlTime.Visible = false;
                pnlDayBalance.Visible = false;
                pnlTime.Dock = DockStyle.Fill;
                pnlTime.BringToFront();
            }
            else
            {
                pnlDayBalance.Visible = true;
                pnlDayBalance.Dock = DockStyle.Fill;
                pnlDayBalance.BringToFront();
            }
        }
    }
}
