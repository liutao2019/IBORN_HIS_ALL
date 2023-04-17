using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace GZSI.Controls
{
    /// <summary>
    /// 取消医保结算
    /// </summary>
    public partial class ucCancelBalanceN : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucCancelBalanceN()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            this.fpSpread1_Sheet1.RowCount = 0;
            this.fpSpread2.Sheets[0].RowCount = 0;
            this.lblDetailTitle.Text = string.Empty;
            this.lblPInfo.Text = string.Empty;

            base.OnLoad(e);
        }

        private Management.SILocalManager silocalMgr = new Management.SILocalManager();

        private void txtCardNO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
            {
                return;
            }
            string strCard = txtCardNO.Text.Trim();
            if (string.IsNullOrEmpty(strCard))
            {
                return;
            }
            strCard = strCard.PadLeft(10, '0');

            #region 查询医保结算记录

            ArrayList alBalanceList = silocalMgr.GetBalanceList(strCard, dtBegin.Value.Date, dtEnd.Value.Date.AddDays(1));
            if (alBalanceList == null)
            {
                MessageBox.Show("查询结算记录失败！" + silocalMgr.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            foreach (FS.HISFC.Models.Registration.Register reg in alBalanceList)
            {
                int row = this.fpSpread1_Sheet1.RowCount;
                this.fpSpread1_Sheet1.Rows.Add(row, 1);

                this.fpSpread1_Sheet1.Cells[row, 0].Text = reg.Name;
                this.fpSpread1_Sheet1.Cells[row, 1].Text = reg.DoctorInfo.Templet.Dept.Name;
                this.fpSpread1_Sheet1.Cells[row, 2].Text = reg.SIMainInfo.TotCost.ToString();
                this.fpSpread1_Sheet1.Cells[row, 3].Text = reg.SIMainInfo.PubCost.ToString();
                this.fpSpread1_Sheet1.Cells[row, 4].Text = reg.SIMainInfo.OwnCost.ToString();
                this.fpSpread1_Sheet1.Cells[row, 5].Text = reg.SIMainInfo.BalanceDate.ToString();
                this.fpSpread1_Sheet1.Cells[row, 6].Text = reg.SIMainInfo.RegNo;
                this.fpSpread1_Sheet1.Cells[row, 7].Text = reg.SIMainInfo.FeeTimes.ToString();
                this.fpSpread1_Sheet1.Rows[row].Tag = reg;
            }
            this.fpSpread1_Sheet1.ActiveRowIndex = 0;

            #endregion
        }

        private void CancelBalance()
        {
            int row = fpSpread1_Sheet1.ActiveRowIndex;
            if (this.fpSpread1_Sheet1.ActiveRowIndex == -1)
            {
                MessageBox.Show("请选中需要取消结算的记录");
                return;
            }

            FS.HISFC.Models.Registration.Register reg = this.fpSpread1_Sheet1.Rows[row].Tag as FS.HISFC.Models.Registration.Register;
            if (reg != null)
            {
                Management.SILocalManager siLocalMgr = new Management.SILocalManager();
                siLocalMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                //删除结算数据
                siLocalMgr.DeleteBalanceInfo(reg);

                //删除明细数据

            }
        }

        /// <summary>
        /// 工具条
        /// </summary>
        protected FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        /// <summary>
        /// 设置toolBar按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("取消医保结算", "取消广州医保结算", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Z作废, true, false, null);
            toolBarService.AddToolButton("打印医保清单", "打印医保清单", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D打印清单, true, false, null);
            return this.toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "取消医保结算":

                    break;
                case "打印医保清单":

                    break;
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                FS.HISFC.Components.Common.Forms.frmQueryPatientByConditions frmQuery = new FS.HISFC.Components.Common.Forms.frmQueryPatientByConditions();

                string QueryStr = this.txtName.Text;

                if (string.IsNullOrEmpty(QueryStr))
                {
                    return;
                }

                frmQuery.QueryByName(QueryStr);
                frmQuery.ShowDialog();

                if (frmQuery.DialogResult == DialogResult.OK)
                {

                    this.txtCardNO.Text = frmQuery.PatientInfo.PID.CardNO;
                    this.txtCardNO.Focus();
                    txtCardNO_KeyDown(new object(), new KeyEventArgs(Keys.Enter));
                }
            }
        }

        /// <summary>
        /// 显示费用明细
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpSpread1_SelectionChanged(object sender, FarPoint.Win.Spread.SelectionChangedEventArgs e)
        {
            this.fpSpread2.ActiveSheet.RowCount = 0;
            FS.HISFC.Models.Registration.Register reg = this.fpSpread1_Sheet1.Rows[this.fpSpread2.ActiveSheet.ActiveRowIndex].Tag as FS.HISFC.Models.Registration.Register;
            if (reg != null)
            {
                ArrayList alFee = silocalMgr.GetBalanceDetailList(reg);
                if (alFee == null)
                {
                    MessageBox.Show("查询医保结算明细失败！" + silocalMgr.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList fItem in alFee)
                {
                    int row = this.fpSpread2.ActiveSheet.RowCount;
                    this.fpSpread2.ActiveSheet.Rows.Add(row, 1);

                    this.fpSpread2.ActiveSheet.Cells[row, 0].Text = fItem.Order.ID;
                    this.fpSpread2.ActiveSheet.Cells[row, 1].Text = fItem.Item.UserCode;
                    this.fpSpread2.ActiveSheet.Cells[row, 2].Text = fItem.Item.Name;
                    this.fpSpread2.ActiveSheet.Cells[row, 3].Text = fItem.Item.Specs;
                    this.fpSpread2.ActiveSheet.Cells[row, 4].Text = fItem.Item.Price.ToString();
                    this.fpSpread2.ActiveSheet.Cells[row, 5].Text = fItem.Item.Qty.ToString();
                    this.fpSpread2.ActiveSheet.Cells[row, 6].Text = fItem.FT.TotCost.ToString();
                }
            }
        }
    }
}
