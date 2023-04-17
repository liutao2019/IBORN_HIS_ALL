using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SOC.Fee.DayBalance.Control
{
    public partial class ucPopDayReportQuery : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucPopDayReportQuery()
        {
            InitializeComponent();
        }

        #region 变量

        SOC.Fee.DayBalance.Manager.InpatientDayReport FeeInaptientDayReport = new SOC.Fee.DayBalance.Manager.InpatientDayReport();
        /// <summary>
        /// 设置一个delegate
        /// </summary>
        /// <param name="dayReport"></param>
        public delegate void mydelegateEndChoose(SOC.Fee.DayBalance.Object.DayReport dayReport);
        public event mydelegateEndChoose OnEndChoose;

    
        #endregion

        #region 属性

        /// <summary>
        /// 开始时间

        /// </summary>
        public DateTime BeginDate
        {
            get
            {
                return this.dtpBeginDate.Value;
            }
            set
            {
                this.dtpBeginDate.Value = value;
            }
        }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndDate
        {
            get
            {
                return this.dtpEndDate.Value;
            }

            set
            {
                this.dtpEndDate.Value = value;
            }
        }        
     
        #endregion

        #region 方法
        /// <summary>
        /// 查找当前操作员已结算列表
        /// </summary>
        protected virtual void GetReportDet()
        {
            ArrayList alFeeDayReport = new ArrayList();

            alFeeDayReport = this.FeeInaptientDayReport.GetDayReportInfosForOper(this.FeeInaptientDayReport.Operator.ID, BeginDate,EndDate);

            SOC.Fee.DayBalance.Object.DayReport dayReport = new SOC.Fee.DayBalance.Object.DayReport();

            if (alFeeDayReport == null) return;

            for (int i = 0; i < alFeeDayReport.Count; i++)
            {
                dayReport = (SOC.Fee.DayBalance.Object.DayReport)alFeeDayReport[i];
                this.fpQuery_Sheet1.Cells[i, 0].Value = dayReport.StatNO;
                this.fpQuery_Sheet1.Cells[i, 1].Value = dayReport.BeginDate;
                this.fpQuery_Sheet1.Cells[i, 2].Value = dayReport.EndDate;
                this.fpQuery_Sheet1.Cells[i,3].Value = dayReport.Oper.OperTime;

                this.fpQuery_Sheet1.Rows[i].Tag = dayReport;
                
            }
            
        }
        /// <summary>
        /// 组件初始化

        /// </summary>
        private void initControl()
        {            
            dtpBeginDate.Value = FeeInaptientDayReport.GetDateTimeFromSysDateTime().Date;
            dtpEndDate.Value = FeeInaptientDayReport.GetDateTimeFromSysDateTime();
        }
        
        #endregion

        #region 事件

        /// <summary>
        /// 双击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpQuery_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            SOC.Fee.DayBalance.Object.DayReport dayReport = new SOC.Fee.DayBalance.Object.DayReport();

            if (fpQuery_Sheet1.RowCount == 0)
            {
                return;
            }

            dayReport = (SOC.Fee.DayBalance.Object.DayReport)this.fpQuery_Sheet1.Rows[fpQuery_Sheet1.ActiveRowIndex].Tag;
            try
            {
                this.OnEndChoose(dayReport);
            }
            catch { }

            this.FindForm().Close();
                       
        }
        /// <summary>
        /// 初始化

        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucPopDayReportQuery_Load(object sender, EventArgs e)
        {
            initControl();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            this.BeginDate = this.dtpBeginDate.Value;
            this.EndDate = this.dtpEndDate.Value;
            if (this.EndDate < this.BeginDate)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("起始时间不能大于截至时间"));
                return;
            }
            GetReportDet();
        }
        #endregion

        private void btnExit_Click(object sender, EventArgs e)
        {
            try
            {
                this.FindForm().Close();
            }
            catch { }
        }



       
    }
}
