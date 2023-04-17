using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.HISFC.Models.Base;

namespace FS.SOC.Local.ZDLY.PubReport.Components
{
    public partial class frmMidBalanceBill : FS.FrameWork.WinForms.Forms.BaseStatusBar
    {
        public frmMidBalanceBill()
        {
            this.ProgressRun(true);
            InitializeComponent();
        }

        FS.HISFC.Models.RADT.PatientInfo p = null;
        FS.HISFC.BizLogic.Fee.InPatient myInpatient = new FS.HISFC.BizLogic.Fee.InPatient();
        string errText;

        bool IsBalance = false;

        private void ucQueryInpatientNo1_myEvent()
        {
            if (this.ucQueryInpatientNo1.InpatientNo == null || this.ucQueryInpatientNo1.InpatientNo.Trim() == "")
            {
                MessageBox.Show("住院号错误，没有找到该患者");
                this.ucBalanceBill1.Clear();
                this.ucQueryInpatientNo1.Focus();
                return;
            }
            //Add by 王宇，当患者开始打结算清单时修改在院状态为C,其他地方不允许录入费用
            //2005-12-26
            FS.HISFC.BizLogic.Fee.InPatient myInpatient = new FS.HISFC.BizLogic.Fee.InPatient();
            FS.HISFC.BizLogic.RADT.InPatient myRADT = new FS.HISFC.BizLogic.RADT.InPatient();
            p = myRADT.QueryPatientInfoByInpatientNO(this.ucQueryInpatientNo1.InpatientNo);
            string dtBegin = "", dtEnd = this.dTEnd.Value.ToString();
            if (this.IsBalance)
            {
                this.ucBalanceBill1.IsBalance = true;
                this.ucBalanceBill1.DisplayPatient(this.ucQueryInpatientNo1.InpatientNo, ref dtBegin, dtEnd);
                this.dTBegin.Value = FS.FrameWork.Function.NConvert.ToDateTime(dtBegin);
                //this.ucBalanceBill1.SetPatientFee(p);
            }
            else
            {

                if (p == null || p.ID == null)
                {
                    MessageBox.Show("获得患者基本信息失败!");
                    return;
                }
                if (p.PVisit.InState.ID.ToString() == "O")
                {
                    MessageBox.Show("该患者已作出院清账！");
                    return;
                }
                this.ucBalanceBill1.DisplayPatient(this.ucQueryInpatientNo1.InpatientNo, ref dtBegin, dtEnd);
                this.dTBegin.Value = FS.FrameWork.Function.NConvert.ToDateTime(dtBegin);

                //FS.FrameWork.Management.PublicTrans.BeginTransaction();
                //myInpatient.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                //if (p.PVisit.InState.ID.ToString() == "B")
                //{
                //    int iReturn = myInpatient.UpdateCloseAccount(this.ucQueryInpatientNo1.InpatientNo, "C");
                //    if (iReturn <= 0)
                //    {
                //        FS.FrameWork.Management.PublicTrans.RollBack();
                //        MessageBox.Show("更新患者在院状态出错!" + myInpatient.Err);
                //        return;
                //    }
                //}
                //FS.FrameWork.Management.PublicTrans.Commit();

                string errText = "";
                this.QueryFeeInfo(ref errText);
            }



            //Add End

            this.btnPrint.Focus();
        }

        private void toolBar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
        {
            //封账
            if (e.Button == this.tbPrint)
            {
                this.ucBalanceBill1.Print();
                return;
            }

            //开账
            if (e.Button == this.tbbOpenAccount)
            {
                this.btnOpenAccount_Click(this.btnPrint, null);
                return;
            }


            //清屏
            if (e.Button == this.tbFresh)
            {
                this.ucQueryInpatientNo1.Text = "";
                //this.ucQueryInpatientNo1.c = "";
                this.ucBalanceBill1.Clear();
                return;
            }

            //退出
            if (e.Button == this.tbQuit)
            {
                this.Close();
                return;
            }
            if (e.Button == this.tbCDFee)
            {
                //Fee.frmConfirmOrder frmCDFee = new frmConfirmOrder(this.var);
                //frmCDFee.ISOutPatient = true;
                //frmCDFee.ShowDialog();

                return;
            }
        }

        private void frmMidBalanceBill_Load(object sender, System.EventArgs e)
        {
            this.ucQueryInpatientNo1.TextBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dTBegin.Enabled = false;
            this.dTEnd.Value = this.dTEnd.Value.AddDays(-1);
            if (this.Tag.ToString() == "O")
            {
                this.IsBalance = true;
            }
            else
            {
                this.IsBalance = false;
            }
            this.WindowState = FormWindowState.Maximized;
            this.ucQueryInpatientNo1.Focus();
            this.ucQueryInpatientNo1.Select();

        }

        private void btnPrint_Click(object sender, System.EventArgs e)
        {

            FS.HISFC.BizLogic.Fee.InPatient myInpatient = new FS.HISFC.BizLogic.Fee.InPatient();
            DateTime now = myInpatient.GetDateTimeFromSysDateTime();
            if (this.dTEnd.Value.Date >= now.Date)
            {
                MessageBox.Show("未防止费用计算错误，中途结算只能结算到当前日期之前的日期。");
                return;
            }
            if (this.dTEnd.Value.Date < this.dTBegin.Value.Date)
            {
                MessageBox.Show("结算开始日期大于结算结束日期！");
                return;

            }
  
            if (p != null)
            {
                string beginDate = this.dTBegin.Value.ToString("yyyy-MM-dd HH:mm:ss");
                string endDate = this.dTEnd.Value.ToString("yyyy-MM-dd HH:mm:ss");
                this.ucBalanceBill1.QueryFee(p, this.dTBegin.Value.ToString(),
                    new DateTime(this.dTEnd.Value.Year, this.dTEnd.Value.Month, this.dTEnd.Value.Day, 23, 59, 59).ToString());
                this.ucBalanceBill1.DisplayPatient(this.ucQueryInpatientNo1.InpatientNo, ref beginDate, endDate);
                string errText = "";
                this.QueryFeeInfo(ref errText);
            }
        }

        private int Deal(string state)
        {
            FS.HISFC.BizLogic.Fee.InPatient myInpatient = new FS.HISFC.BizLogic.Fee.InPatient();
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            myInpatient.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            //int iReturn = myInpatient.UpdateCloseAccount(this.ucQueryInpatientNo1.InpatientNo, state);
            int iReturn = myInpatient.CloseAccount(this.ucQueryInpatientNo1.InpatientNo);
            if (iReturn <= 0)
            {
                errText = myInpatient.Err;
                FS.FrameWork.Management.PublicTrans.RollBack();
                return iReturn;
            }
            else
            {
                errText = "";
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            return 1;
        }

        private void btnOpenAccount_Click(object sender, System.EventArgs e)
        {
            FS.HISFC.BizLogic.RADT.InPatient myRADT = new FS.HISFC.BizLogic.RADT.InPatient();
            p = myRADT.QueryPatientInfoByInpatientNO(this.ucQueryInpatientNo1.InpatientNo);

            if (p == null || p.ID == null)
            {
                MessageBox.Show("获得患者基本信息失败!");
                return;
            }
            //if (p.PVisit.InState.ID.ToString() != "C")
            //{
            //    MessageBox.Show("该患者不是封账状态,不能进行开帐处理!");
            //    return;
            //}
            if (p.IsStopAcount == true)
            {
                MessageBox.Show("该患者不是封账状态,不能进行开帐处理!");
                return;
            }


            if (Deal("B") != 1)
            {
                MessageBox.Show("开账失败!" + errText);
            }
            else
            {
                MessageBox.Show("开账成功!" + errText);
            }
        }


        /// <summary>
        /// 检索患者费用汇总信息
        /// </summary>
        /// <param name="dtFeeBegin">开始时间</param>
        /// <param name="dtFeeEnd">结束时间</param>
        /// <returns>1成功 －1失败</returns>
        protected int QueryFeeInfo(ref string errText)
        {
            //ArrayList al = this.myInpatient.QueryFeeInfosAndChangeCostGroupByMinFeeByInpatientNO(p.ID, "0");

            ArrayList al = this.myInpatient.QueryFeeInfosGroupByMinFeeByInpatientNO(p.ID, 
                FS.FrameWork.Function.NConvert.ToDateTime(this.dTBegin.Value),
                FS.FrameWork.Function.NConvert.ToDateTime(this.dTEnd.Value.Date.ToShortDateString()+" 23:59:59"), "0");

            if (al == null)
            {
                errText = myInpatient.Err;
                return -1;
            }

            if (SOC.HISFC.InpatientFee.Components.Function.SplitFeeItem(p, al, ref errText))
            {
                //al = this.myInpatient.QueryFeeInfosAndChangeCostGroupByMinFeeByInpatientNO(p.ID, "0");

                al = this.myInpatient.QueryFeeInfosGroupByMinFeeByInpatientNO(p.ID,
                FS.FrameWork.Function.NConvert.ToDateTime(this.dTBegin.Value),
                FS.FrameWork.Function.NConvert.ToDateTime(this.dTEnd.Value.Date.ToShortDateString() + " 23:59:59"), "0");
            }
            else
            {
                return -1;
            }


            decimal TotalCost = 0m;
            decimal TotalPubPay = 0m;
            decimal TotalOwnCost = 0m;



            this.fpCost_Sheet1.RowCount = 0;
            for (int i = 0; i < al.Count; i++)
            {
                FS.HISFC.Models.Fee.Inpatient.FeeInfo feeInfo = new FS.HISFC.Models.Fee.Inpatient.FeeInfo();
                FS.HISFC.Models.Fee.Inpatient.FeeInfo feeInfoClone = new FS.HISFC.Models.Fee.Inpatient.FeeInfo();
                feeInfo = (FS.HISFC.Models.Fee.Inpatient.FeeInfo)al[i];

                //获取最小费用名称
                feeInfo.Item.MinFee.Name = FS.SOC.HISFC.BizProcess.CommonInterface.CommonController.CreateInstance().GetConstantName(EnumConstant.MINFEE, feeInfo.Item.MinFee.ID);
                TotalCost += feeInfo.FT.TotCost;
                TotalPubPay += feeInfo.FT.PubCost + feeInfo.FT.PayCost;
                TotalOwnCost += feeInfo.FT.OwnCost;

                this.fpCost_Sheet1.RowCount++;
                this.fpCost_Sheet1.Cells[i, (int)ColumnCost.Check].Value = true;
                this.fpCost_Sheet1.Cells[i, (int)ColumnCost.Check].Locked = true;
                this.fpCost_Sheet1.Cells[i, (int)ColumnCost.MinFee].Value = feeInfo.Item.MinFee.Name;
                this.fpCost_Sheet1.Cells[i, (int)ColumnCost.Cost].Value = feeInfo.FT.TotCost;
                this.fpCost_Sheet1.Cells[i, (int)ColumnCost.PubCost].Value = feeInfo.FT.PubCost + feeInfo.FT.PayCost;
                this.fpCost_Sheet1.Cells[i, (int)ColumnCost.OwnCost].Value = feeInfo.FT.OwnCost;
                this.fpCost_Sheet1.Cells[i, (int)ColumnCost.Check].Tag = feeInfo.Clone();
                this.fpCost_Sheet1.Rows[i].Tag = feeInfo;
            }
            this.fpCost_Sheet1.RowCount++;
            this.fpCost_Sheet1.Cells[this.fpCost_Sheet1.RowCount - 1, (int)ColumnCost.Check].Value = true;
            this.fpCost_Sheet1.Cells[this.fpCost_Sheet1.RowCount - 1, (int)ColumnCost.Check].Locked = true;
            this.fpCost_Sheet1.Cells[this.fpCost_Sheet1.RowCount - 1, (int)ColumnCost.MinFee].Value = "总费用";
            this.fpCost_Sheet1.Cells[this.fpCost_Sheet1.RowCount - 1, (int)ColumnCost.Cost].Value = TotalCost;
            this.fpCost_Sheet1.Cells[this.fpCost_Sheet1.RowCount - 1, (int)ColumnCost.PubCost].Value = TotalPubPay;
            this.fpCost_Sheet1.Cells[this.fpCost_Sheet1.RowCount - 1, (int)ColumnCost.OwnCost].Value = TotalOwnCost;


            return 1;
        }
    }
}
