using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FS.SOC.HISFC.BizProcess.CommonInterface;
using FS.HISFC.Models.Base;
using System.Collections;

namespace FS.SOC.HISFC.InpatientFee.Components.Balance
{
    /// <summary>
    /// 拆分费用信息
    /// </summary>
    public partial class frmSplitFeeInfo : FS.FrameWork.WinForms.Forms.BaseForm
    {
        public frmSplitFeeInfo()
        {
            InitializeComponent();

            this.ckCombine.CheckedChanged += new EventHandler(ckInvoice_CheckedChanged);
            this.ckMinFee.CheckedChanged += new EventHandler(ckInvoice_CheckedChanged);
            this.ckNoSplit.CheckedChanged += new EventHandler(ckInvoice_CheckedChanged);

            this.btnConfrim.Click += new EventHandler(btnConfrim_Click);
            this.btnCancel.Click += new EventHandler(btnCancel_Click);


        }
        private bool isConfirm = false;

        public bool IsConfirm
        {
            get
            {
                return isConfirm;
            }
        }

        /// <summary>
        /// 是否按照最小费用拆分
        /// </summary>
        public bool IsSplit
        {
            get
            {
               return  this.ckMinFee.Checked;
            }
        }

        /// <summary>
        /// 显示患者信息
        /// </summary>
        /// <param name="patientInfo"></param>
        public void SetPatientInfo(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            this.txtPatientNo.Text = patientInfo.PID.PatientNO;
            this.txtName.Text = patientInfo.Name;

            if (patientInfo.Pact.PayKind.ID == "01")
            {
                this.ckNoSplit.Enabled = true;
                this.ckNoSplit.Checked = true;

                this.ckCombine.Enabled = true;

            }
            else
            {
                this.ckMinFee.Checked = true;
                this.ckCombine.Enabled = false;
                this.ckNoSplit.Enabled = false;
            }
        }

        /// <summary>
        /// 显示患者的费用汇总信息
        /// </summary>
        /// <param name="listFeeInfo"></param>
        public void SetPatientFeeInfo(ArrayList alFeeInfo)
        {
            decimal TotalCost = 0M;
            decimal RebateCost = 0M;
            decimal TotalOrgCost = 0M;
            this.fpCost_Sheet1.RowCount = 0;

            List<FS.HISFC.Models.Fee.Inpatient.FeeInfo> listFeeInfo = new List<FS.HISFC.Models.Fee.Inpatient.FeeInfo>();
            foreach (FS.HISFC.Models.Fee.Inpatient.FeeInfo feeInfo in alFeeInfo)
            {
                //获取最小费用名称
                feeInfo.Item.MinFee.Name = CommonController.Instance.GetConstantName(EnumConstant.MINFEE, feeInfo.Item.MinFee.ID);
                TotalCost += feeInfo.FT.TotCost;
                RebateCost += feeInfo.FT.RebateCost;
                TotalOrgCost += feeInfo.FT.DefTotCost;

                this.fpCost_Sheet1.RowCount++;
                int i = this.fpCost_Sheet1.RowCount - 1;
                this.fpCost_Sheet1.Cells[i, (int)ColumnCost.Check].Value = true;
                this.fpCost_Sheet1.Cells[i, (int)ColumnCost.Check].Locked = true;
                this.fpCost_Sheet1.Cells[i, (int)ColumnCost.MinFee].Value = feeInfo.Item.MinFee.Name + (feeInfo.SplitFeeFlag ? "(加收)" : "");
                this.fpCost_Sheet1.Cells[i, (int)ColumnCost.OrgCost].Value = feeInfo.FT.DefTotCost;
                this.fpCost_Sheet1.Cells[i, (int)ColumnCost.Cost].Value = feeInfo.FT.TotCost;
                this.fpCost_Sheet1.Cells[i, (int)ColumnCost.PubCost].Value = feeInfo.FT.PubCost;
                this.fpCost_Sheet1.Cells[i, (int)ColumnCost.OwnCost].Value = feeInfo.FT.OwnCost;
                this.fpCost_Sheet1.Cells[i, (int)ColumnCost.Check].Tag = feeInfo;
                this.fpCost_Sheet1.Rows[i].Tag = feeInfo;
                if (feeInfo.SplitFeeFlag)
                {
                    this.fpCost_Sheet1.Rows[i].ForeColor = Color.Red;
                }

                listFeeInfo.Add(feeInfo);

            }
            this.AddFeeInfoSumCost(TotalOrgCost, TotalCost, this.fpCost_Sheet1);
        }

        /// <summary>
        /// 拆分费用
        /// </summary>
        /// <param name="listFeeInfo"></param>
        public void SplitPatientFeeInfo(List<FS.HISFC.Models.Fee.Inpatient.FeeInfo> listFeeInfo)
        {
            this.fpSplitCost_Sheet1.RowCount = 0;

            List<FS.HISFC.Models.Fee.Inpatient.FeeInfo> newListFeeInfo = Function.GetFeeBizProcess().ProcessSplitFeeInfo(listFeeInfo);

            decimal TotalCost = 0M;
            decimal RebateCost = 0M;

            foreach (FS.HISFC.Models.Fee.Inpatient.FeeInfo feeInfo in newListFeeInfo)
            {
                //获取最小费用名称
                feeInfo.Item.MinFee.Name = CommonController.Instance.GetConstantName(EnumConstant.MINFEE, feeInfo.Item.MinFee.ID);
                TotalCost += feeInfo.FT.TotCost;
                RebateCost += feeInfo.FT.RebateCost;

                this.fpSplitCost_Sheet1.RowCount++;
                int i = this.fpSplitCost_Sheet1.RowCount - 1;
                this.fpSplitCost_Sheet1.Cells[i, (int)ColumnCost.Check].Value = true;
                this.fpSplitCost_Sheet1.Cells[i, (int)ColumnCost.Check].Locked = true;
                this.fpSplitCost_Sheet1.Cells[i, (int)ColumnCost.MinFee].Value = feeInfo.Item.MinFee.Name + (feeInfo.SplitFeeFlag ? "(加收)" : "");
                this.fpSplitCost_Sheet1.Cells[i, (int)ColumnCost.OrgCost].Value = feeInfo.FT.DefTotCost;
                this.fpSplitCost_Sheet1.Cells[i, (int)ColumnCost.Cost].Value = feeInfo.FT.TotCost;
                this.fpSplitCost_Sheet1.Cells[i, (int)ColumnCost.PubCost].Value = feeInfo.FT.PubCost;
                this.fpSplitCost_Sheet1.Cells[i, (int)ColumnCost.OwnCost].Value = feeInfo.FT.OwnCost;
                this.fpSplitCost_Sheet1.Cells[i, (int)ColumnCost.Check].Tag = feeInfo.Clone();
                if (feeInfo.SplitFeeFlag)
                {
                    this.fpSplitCost_Sheet1.Rows[i].ForeColor = Color.Red;
                }
                this.fpSplitCost_Sheet1.Rows[i].Tag = feeInfo;
            }

            this.AddFeeInfoSumCost(TotalCost, TotalCost, this.fpSplitCost_Sheet1);
        }

        /// <summary>
        /// 合并费用
        /// </summary>
        /// <param name="listFeeInfo"></param>
        public void CombinePatientFeeInfo(List<FS.HISFC.Models.Fee.Inpatient.FeeInfo> listFeeInfo)
        {
            this.fpSplitCost_Sheet1.RowCount = 0;

            List<FS.HISFC.Models.Fee.Inpatient.FeeInfo> newListFeeInfo = Function.GetFeeBizProcess().ProcessCombineFeeInfo(listFeeInfo);

            decimal TotalCost = 0M;
            decimal RebateCost = 0M;

            foreach (FS.HISFC.Models.Fee.Inpatient.FeeInfo feeInfo in newListFeeInfo)
            {
                //获取最小费用名称
                feeInfo.Item.MinFee.Name = CommonController.Instance.GetConstantName(EnumConstant.MINFEE, feeInfo.Item.MinFee.ID);
                TotalCost += feeInfo.FT.TotCost;
                RebateCost += feeInfo.FT.RebateCost;

                this.fpSplitCost_Sheet1.RowCount++;
                int i = this.fpSplitCost_Sheet1.RowCount - 1;
                this.fpSplitCost_Sheet1.Cells[i, (int)ColumnCost.Check].Value = true;
                this.fpSplitCost_Sheet1.Cells[i, (int)ColumnCost.Check].Locked = true;
                this.fpSplitCost_Sheet1.Cells[i, (int)ColumnCost.MinFee].Value = feeInfo.Item.MinFee.Name + (feeInfo.SplitFeeFlag ? "(加收)" : "");
                this.fpSplitCost_Sheet1.Cells[i, (int)ColumnCost.OrgCost].Value = feeInfo.FT.DefTotCost;
                this.fpSplitCost_Sheet1.Cells[i, (int)ColumnCost.Cost].Value = feeInfo.FT.TotCost;
                this.fpSplitCost_Sheet1.Cells[i, (int)ColumnCost.PubCost].Value = feeInfo.FT.PubCost;
                this.fpSplitCost_Sheet1.Cells[i, (int)ColumnCost.OwnCost].Value = feeInfo.FT.OwnCost;
                this.fpSplitCost_Sheet1.Cells[i, (int)ColumnCost.Check].Tag = feeInfo.Clone();
                if (feeInfo.SplitFeeFlag)
                {
                    this.fpSplitCost_Sheet1.Rows[i].ForeColor = Color.Red;
                }
                this.fpSplitCost_Sheet1.Rows[i].Tag = feeInfo;
            }

            this.AddFeeInfoSumCost(TotalCost, TotalCost, this.fpSplitCost_Sheet1);
        }

        /// <summary>
        /// 添加合计
        /// </summary>
        private void AddFeeInfoSumCost(decimal OrgTotalCost, decimal TotalCost, FarPoint.Win.Spread.SheetView sheetView) 
        {
            int row = sheetView.RowCount;
            if (row > 0)
            {
                if (sheetView.Rows[row - 1].Tag is FS.HISFC.Models.Fee.Inpatient.FeeInfo)
                {
                    sheetView.RowCount++;
                }
                else
                {
                    row = row - 1;
                }
                sheetView.Cells[row, (int)ColumnCost.Check].Locked = true;
                sheetView.Cells[row, (int)ColumnCost.Check].Value = false;
                sheetView.Cells[row, (int)ColumnCost.OrgCost].Value = OrgTotalCost;
                sheetView.Cells[row, (int)ColumnCost.MinFee].Value = "合计：";
                sheetView.Cells[row, (int)ColumnCost.Cost].Value = TotalCost;
                sheetView.Cells[row, (int)ColumnCost.BalanceCost].Value = TotalCost;
                sheetView.Rows[row].Tag = null;
            }
        }

        /// <summary>
        /// 选择拆分属性
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ckInvoice_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                this.ckMinFee.CheckedChanged -= new EventHandler(ckInvoice_CheckedChanged);
                this.ckCombine.CheckedChanged -= new EventHandler(ckInvoice_CheckedChanged);
                this.ckNoSplit.CheckedChanged -= new EventHandler(ckInvoice_CheckedChanged);

                if(sender== this.ckMinFee)
                {
                    this.ckCombine.Checked = false;
                    this.ckMinFee.Checked = true;
                    this.ckNoSplit.Checked = false;
                }
                else if (sender == this.ckCombine)
                {
                    this.ckMinFee.Checked = false;
                    this.ckCombine.Checked = true;
                    this.ckNoSplit.Checked = false;
                }
                else if (sender == this.ckNoSplit)
                {
                    this.ckMinFee.Checked = false;
                    this.ckCombine.Checked = false;
                    this.ckNoSplit.Checked = true;

                }

                List<FS.HISFC.Models.Fee.Inpatient.FeeInfo> listFeeInfo = new List<FS.HISFC.Models.Fee.Inpatient.FeeInfo>();
                for (int i = 0; i <= this.fpCost_Sheet1.RowCount - 1; i++)
                {
                    if (this.fpCost_Sheet1.Cells[i, (int)ColumnCost.Check].Tag is FS.HISFC.Models.Fee.Inpatient.FeeInfo)
                    {
                        listFeeInfo.Add(this.fpCost_Sheet1.Cells[i, (int)ColumnCost.Check].Tag as FS.HISFC.Models.Fee.Inpatient.FeeInfo);
                    }
                }

                if (this.ckCombine.Checked)
                {
                    this.CombinePatientFeeInfo(listFeeInfo);
                }
                else if(this.ckMinFee.Checked)
                {
                    this.SplitPatientFeeInfo(listFeeInfo);
                }
                else
                {
                    this.SplitPatientFeeInfo(new List<FS.HISFC.Models.Fee.Inpatient.FeeInfo>());
                }
            }
            finally
            {
                this.ckMinFee.CheckedChanged += new EventHandler(ckInvoice_CheckedChanged);
                this.ckCombine.CheckedChanged += new EventHandler(ckInvoice_CheckedChanged);
                this.ckNoSplit.CheckedChanged += new EventHandler(ckInvoice_CheckedChanged);
            }
        }

        void btnConfrim_Click(object sender, EventArgs e)
        {
            if (this.ckCombine.Checked)
            {
                if (CommonController.Instance.MessageBox(this, "确认合并费用？", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    this.isConfirm = true;
                    this.Close();
                }
                else
                {
                    this.isConfirm = false;
                }
            }
            else if (this.ckMinFee.Checked)
            {
                if (CommonController.Instance.MessageBox(this, "确认拆分费用？", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    this.isConfirm = true;
                    this.Close();
                }
                else
                {
                    this.isConfirm = false;
                }
            }
            else
            {
                this.isConfirm = false;
                this.Close();
            }
        }

        void btnCancel_Click(object sender, EventArgs e)
        {
            this.isConfirm = false;
            this.Close();
        }
    }
}
