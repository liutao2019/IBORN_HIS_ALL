using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.Local.InpatientFee.ZhangCha
{
    public partial class ucDailyReportCancelPrepay : FS.SOC.Local.InpatientFee.Base.ucDeptTimeBaseReport
    {
        public ucDailyReportCancelPrepay()
        {
            InitializeComponent();
            //this.LeftAdditionTitle = "";
            this.OperationStartHandler += new DelegateOperationStart(ucDailyReportCancelPrepay_DelegateOperateionEnd);
            this.numericUpDown1.ValueChanged += new EventHandler(numericUpDown1_ValueChanged);
            this.ncmbTime.SelectedIndexChanged += new EventHandler(ncmbTime_SelectedIndexChanged);
            this.Load += new EventHandler(ucDailyReportCancelPrepay_Load);
        }

        void ncmbTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strTemp = this.ncmbTime.Text;
            string[] strArr = strTemp.Split(new string[] { "到" }, StringSplitOptions.RemoveEmptyEntries);
            if (strArr == null || strArr.Length != 2)
            {
                return;
            }

            try
            {
                dtStart.Value = FS.FrameWork.Function.NConvert.ToDateTime(strArr[0]);
            }
            catch
            {
                dtStart.Value = DateTime.Now.AddYears(-10);
            }
            dtEnd.Value = FS.FrameWork.Function.NConvert.ToDateTime(strArr[1]);

        }

        private void SetTime()
        {
            DataSet ds = new DataSet();
            FS.FrameWork.Management.DataBaseManger dataBaseManger = new FS.FrameWork.Management.DataBaseManger();
            if (dataBaseManger.ExecQuery(string.Format(this.GetStaticTimeSQL, FS.FrameWork.Management.Connection.Operator.ID, this.numericUpDown1.Value.ToString()), ref ds) == -1)
            {
                this.ShowBalloonTip(10, "错误", "获取结算时间段发生错误！");
                this.ncmbTime.Visible = false;
            }
            if (ds != null && ds.Tables.Count > 0)
            {
                System.Collections.ArrayList al = new System.Collections.ArrayList();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    FS.FrameWork.Models.NeuObject o = new FS.FrameWork.Models.NeuObject();
                    o.ID = row[0].ToString();
                    o.Name = row[0].ToString();
                    o.Memo = row[0].ToString();
                    al.Add(o);
                }
                this.ncmbTime.AddItems(al);
                if (al.Count > 0)
                {
                    this.ncmbTime.SelectedIndex = 0;

                }
            }
          
        }

        void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            this.SetTime();
           
        }

        void ucDailyReportCancelPrepay_Load(object sender, EventArgs e)
        {
            if (this.DesignMode)
            {
                return;
            }
            this.SetTime();
            this.LeftAdditionTitle = "";
            this.RightAdditionTitle = "";
            this.MainTitle = "收款员注销预收明细";
            this.MidAdditionTitle = "";
            this.IsDeptAsCondition = false;
            this.SQLIndexs = "SOC.Fee.Inpatient.DailyReport.CancelPrepay";
            this.RowHeaderVisible = false;
            this.CHHGridLineColor = System.Drawing.Color.Black;
            this.ColumnHeaderHeight = 24f;
            this.fpSpread1_Sheet1.ColumnHeader.Rows[0].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
            this.QueryDataWhenInit = false;
            this.Init();
            this.QueryData();
        }
       
        private void ucDailyReportCancelPrepay_DelegateOperateionEnd(string operType)
        {
            if (operType == "query")
            {
                FS.HISFC.BizLogic.Fee.InPatient feeMgr = new FS.HISFC.BizLogic.Fee.InPatient();
                string sqlTotIndex = "SOC.Fee.Inpatient.DailyReport.CancelPrepayForTot";
                DataSet dsResult = new DataSet();
                string strTot = "";

                int returnValue = feeMgr.ExecQuery(sqlTotIndex, ref dsResult, this.dtStart.Value.ToString(), this.dtEnd.Value.ToString(), FS.FrameWork.Management.Connection.Operator.ID);
                if (returnValue == -1)
                {
                    MessageBox.Show("查询发票汇总信息失败!" + feeMgr.Err);
                }
                else
                {
                    if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
                    {
                        DataRow dr = dsResult.Tables[0].Rows[0];
                        string totCost = FS.FrameWork.Function.NConvert.ToDecimal(dr[0].ToString()).ToString("F2");
                        string totNum = dr[1].ToString();
                        strTot = "【总金额是：" + totCost + " 元" + "；注销发票张数：" + totNum + " 张】";
                    }
                }

                this.panelAdditionTitle.Size = new System.Drawing.Size(775, 30);
                this.lbAdditionTitleMid.Text = "收款员：" + FS.FrameWork.Management.Connection.Operator.Name + "(" + FS.FrameWork.Management.Connection.Operator.ID + ")   报表时间："
                    + this.dtStart.Value.ToString()
                    + " 到 "
                    + this.dtEnd.Value.ToString() + "\n" + strTot;
            }
        }
    }
}
