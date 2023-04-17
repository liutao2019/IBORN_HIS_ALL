using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SOC.Fee.DayBalance.InpatientPrepay
{
    public partial class ucPrepayHistory : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucPrepayHistory()
        {
            InitializeComponent();
        }

        Manager.PrepayDayBalance PrepayMgr = new SOC.Fee.DayBalance.Manager.PrepayDayBalance();
        ucPrepayCompare PrepayCompare = new ucPrepayCompare();

        /// <summary>
        /// 历史日结数据双击事件
        /// </summary>
        public event SetValue evnSetValue;

        public void Init()
        {
            DateTime nowDate = new DateTime();
            nowDate= PrepayMgr.GetDateTimeFromSysDateTime();
            this.neuDateTimePicker1.Value =new DateTime(nowDate.Year,nowDate.Month,1,0,0,0);
        }

        public void Query()
        {
            //左边显示最近一个月日结记录
            DateTime dtBegein = this.neuDateTimePicker1.Value;//取当月的开始时间
            DateTime dtEnd = dtBegein.AddMonths(1);//取当月的结束时间
            int intReturn = 0;
            DataSet ds = new DataSet();
            intReturn = PrepayMgr.GetPrepayBalanceHistory(this.PrepayMgr.Operator.ID, dtBegein.ToString(), dtEnd.ToString(), ref ds);
            if (intReturn == -1)
            {
                MessageBox.Show("查找数据失败");
                return;
            }
            else
            {
                if (this.neuSpread1_Sheet1.RowCount > 0)
                    this.neuSpread1_Sheet1.Rows.Remove(0, this.neuSpread1_Sheet1.RowCount);
                DataTable dt = ds.Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 1);
                    int row = this.neuSpread1_Sheet1.RowCount - 1;
                    this.neuSpread1_Sheet1.Rows[row].Tag = dr[0].ToString();//结算序号
                    this.neuSpread1_Sheet1.SetValue(row, 0, dr[1].ToString(), false);//结算结束时间
                }
            }
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

        /// <summary>
        /// 双击左边日结时间显示当时日结数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuSpread1_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            int rowCount = this.neuSpread1.ActiveSheet.ActiveRow.Index;
            if (rowCount < 0)
            {
                return;
            }
            string balanceNO = this.neuSpread1_Sheet1.Rows[rowCount].Tag.ToString();

            SOC.Fee.DayBalance.Object.PrepayDayBalance prepay = this.PrepayMgr.GetPrepayDayBalance(balanceNO);
            if (prepay == null)
            {
                MessageBox.Show("查询历史日结记录出错");
                return;
            }
            this.evnSetValue(prepay);
        }

        public delegate void SetValue(SOC.Fee.DayBalance.Object.PrepayDayBalance prepay);

    }
}
