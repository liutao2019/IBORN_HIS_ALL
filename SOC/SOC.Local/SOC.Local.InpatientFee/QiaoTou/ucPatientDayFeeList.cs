using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.Local.InpatientFee.QiaoTou
{
    public partial class ucPatientDayFeeList : FS.FrameWork.WinForms.Controls.ucBaseControl 
    {
        public ucPatientDayFeeList()
        {
            InitializeComponent();
        }

        #region 变量

        FS.HISFC.BizLogic.Fee.InPatient inpatientMgr = new FS.HISFC.BizLogic.Fee.InPatient();

        /// <summary>
        /// 结束时间
        /// </summary>
        DateTime endTime = new DateTime();

        #endregion

        #region 属性

        public DateTime EndTime
        {
            get
            {
                return this.endTime;
            }
        }

        #endregion

        /// <summary>
        /// 快捷键处理
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                if (this.FindForm() != null)
                {
                    this.FindForm().Close();
                }
            }
            return base.ProcessDialogKey(keyData);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="dsResult"></param>
        public void Init(DataSet dsResult)
        {

            this.neuSpread1_Sheet1.Rows.Count = 0;

            if (dsResult == null || dsResult.Tables.Count <= 0)
            {
                return;
            }

            if (this.SetData(dsResult.Tables[0]) == -1)
            {
                return;
            }

            this.endTime = this.inpatientMgr.GetDateTimeFromSysDateTime();

        }

        /// <summary>
        /// 设置FP值
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private int SetData(DataTable dt)
        {
            if (dt == null || dt.Rows.Count <= 0)
            {
                return 0;
            }

            decimal dayTotCat = 0m;
            this.neuSpread1_Sheet1.Rows.Count = 0;
            this.neuSpread1_Sheet1.Rows.Add(0, dt.Rows.Count);

            try
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    string dateStr = dr[0].ToString();
                    decimal dayCost = FS.FrameWork.Function.NConvert.ToDecimal(dr[2].ToString());
                    dayTotCat += dayCost;

                    this.neuSpread1_Sheet1.Cells[i, 0].Text = dateStr;
                    this.neuSpread1_Sheet1.Cells[i, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                    this.neuSpread1_Sheet1.Cells[i, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

                    this.neuSpread1_Sheet1.Cells[i, 1].Text = dr[1].ToString();
                    this.neuSpread1_Sheet1.Cells[i, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                    this.neuSpread1_Sheet1.Cells[i, 1].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

                    this.neuSpread1_Sheet1.Cells[i, 2].Text = dayCost.ToString();
                    this.neuSpread1_Sheet1.Cells[i, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                    this.neuSpread1_Sheet1.Cells[i, 2].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

                    this.neuSpread1_Sheet1.Cells[i, 3].Text = dayTotCat.ToString();
                    this.neuSpread1_Sheet1.Cells[i, 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                    this.neuSpread1_Sheet1.Cells[i, 3].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }

            return 1;

        }

        /// <summary>
        /// 双击换取endDate
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {

            if (e.Row < 0)
            {
                MessageBox.Show("没有选中行!");
                return;
            }
            else
            {
                try
                {
                    this.endTime = FS.FrameWork.Function.NConvert.ToDateTime(this.neuSpread1_Sheet1.Cells[e.Row, 0].Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }

                if (this.FindForm() != null)
                {
                    this.FindForm().Close();
                }
            }

        }



    }
}
