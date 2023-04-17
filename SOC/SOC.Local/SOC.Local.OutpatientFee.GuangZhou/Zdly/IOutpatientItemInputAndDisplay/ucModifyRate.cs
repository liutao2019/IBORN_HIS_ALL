using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.OutpatientFee.GuangZhou.Zdly.IOutpatientItemInputAndDisplay
{
    public partial class ucModifyRate : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucModifyRate()
        {
            InitializeComponent();

            this.btnCancel.Click += new EventHandler(btnCancel_Click);
            this.btnConfirm.Click += new EventHandler(btnConfirm_Click);
            this.fpSpread1.EditModeOff+=new EventHandler(fpSpread1_EditModeOff);
        }

        void fpSpread1_EditModeOff(object sender, EventArgs e)
        {
            int i = this.fpSpread1_Sheet1.ActiveRowIndex;
            if (i >= 0)
            {
                if (this.fpSpread1_Sheet1.Rows[i].Tag is FS.HISFC.Models.Fee.Outpatient.FeeItemList)
                {
                    #region 设置单个项目的打折比例
                    if (this.fpSpread1_Sheet1.ActiveColumnIndex == (int)EnumColumnName.EcoRate)
                    {
                        #region 打折比例
                        decimal realRate = FS.FrameWork.Function.NConvert.ToDecimal(this.fpSpread1_Sheet1.Cells[i, (int)EnumColumnName.EcoRealRate].Value);
                        decimal rate = FS.FrameWork.Public.String.FormatNumber(FS.FrameWork.Function.NConvert.ToDecimal(this.fpSpread1_Sheet1.Cells[i, (int)EnumColumnName.EcoRate].Value), 2) / 100;

                        if (rate == FS.FrameWork.Public.String.FormatNumber(realRate, 2))//使用实际比例计算
                        {
                            decimal orgCost = FS.FrameWork.Function.NConvert.ToDecimal(this.fpSpread1_Sheet1.Cells[i, (int)EnumColumnName.Cost].Value);
                            decimal cost = FS.FrameWork.Public.String.FormatNumber(orgCost * realRate, 2);
                            this.fpSpread1_Sheet1.Cells[i, (int)EnumColumnName.EcoCost].Value = cost;
                        }
                        else//否则使用新比例计算
                        {
                            decimal orgCost = FS.FrameWork.Function.NConvert.ToDecimal(this.fpSpread1_Sheet1.Cells[i, (int)EnumColumnName.Cost].Value);
                            decimal cost = FS.FrameWork.Public.String.FormatNumber(orgCost * rate, 2);
                            this.fpSpread1_Sheet1.Cells[i, (int)EnumColumnName.EcoCost].Value = cost;
                            this.fpSpread1_Sheet1.Cells[i, (int)EnumColumnName.EcoRealRate].Value = rate;
                        }
                        #endregion
                    }
                    else if (this.fpSpread1_Sheet1.ActiveColumnIndex == (int)EnumColumnName.EcoCost)
                    {
                        #region 优惠金额
                        decimal orgCost = FS.FrameWork.Function.NConvert.ToDecimal(this.fpSpread1_Sheet1.Cells[i, (int)EnumColumnName.Cost].Value);
                        decimal cost = FS.FrameWork.Function.NConvert.ToDecimal(this.fpSpread1_Sheet1.Cells[i, (int)EnumColumnName.EcoCost].Value);
                        if (cost > orgCost)
                        {
                            cost = orgCost;
                            this.fpSpread1_Sheet1.Cells[i, (int)EnumColumnName.EcoCost].Value = cost;
                        }
                        decimal realRate = cost / orgCost;
                        this.fpSpread1_Sheet1.Cells[i, (int)EnumColumnName.EcoRealRate].Value = realRate;
                        decimal rate = FS.FrameWork.Public.String.FormatNumber(realRate, 2);
                        this.fpSpread1_Sheet1.Cells[i, (int)EnumColumnName.EcoRate].Value = rate * 100;
                        #endregion
                    }
                    #endregion
                }
                else
                {
                    #region 全部打折

                    decimal rateAll = FS.FrameWork.Public.String.FormatNumber(FS.FrameWork.Function.NConvert.ToDecimal(this.fpSpread1_Sheet1.Cells[i, (int)EnumColumnName.EcoRate].Value), 2) / 100;

                    for (int row = 0; row < this.fpSpread1_Sheet1.RowCount; row++)
                    {
                        if (this.fpSpread1_Sheet1.Rows[row].Tag is FS.HISFC.Models.Fee.Outpatient.FeeItemList)
                        {
                            decimal orgCost = FS.FrameWork.Function.NConvert.ToDecimal(this.fpSpread1_Sheet1.Cells[row, (int)EnumColumnName.Cost].Value);
                            decimal cost = FS.FrameWork.Public.String.FormatNumber(orgCost * rateAll, 2);
                            this.fpSpread1_Sheet1.Cells[row, (int)EnumColumnName.EcoCost].Value = cost;
                            this.fpSpread1_Sheet1.Cells[row, (int)EnumColumnName.EcoRealRate].Value = rateAll;
                            this.fpSpread1_Sheet1.Cells[row, (int)EnumColumnName.EcoRate].Value = rateAll * 100;
                        }
                    }
                    

                    #endregion
                }

            }

            this.sumCost();
        }

        void btnConfirm_Click(object sender, EventArgs e)
        {
            if (this.save() > 0)
            {
                if (this.ParentForm != null)
                {
                    this.ParentForm.Close();
                }
            }
        }

        void btnCancel_Click(object sender, EventArgs e)
        {
            if (this.ParentForm != null)
            {
                this.ParentForm.Close();
            }
        }

        public void SetFeeDetail(ArrayList al)
        {
            if (al != null)
            {
                this.fpSpread1_Sheet1.RowCount=0;
                int i=0;
                foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItemList in al)
                {
                    this.fpSpread1_Sheet1.RowCount++;
                    i = this.fpSpread1_Sheet1.RowCount - 1;

                    if (feeItemList.Item.PackQty <= 0)
                    {
                        feeItemList.Item.PackQty = 1;
                    }

                    feeItemList.Item.Price = feeItemList.Item.Price / (1 - feeItemList.Patient.Pact.Rate.RebateRate);

                    this.fpSpread1_Sheet1.Cells[i, (int)EnumColumnName.ItemName].Value = feeItemList.Item.Name;
                    if (feeItemList.FeePack == "1")
                    {
                        this.fpSpread1_Sheet1.Cells[i, (int)EnumColumnName.Qty].Value = feeItemList.Item.Qty / feeItemList.Item.PackQty;
                        this.fpSpread1_Sheet1.Cells[i, (int)EnumColumnName.Price].Value = feeItemList.Item.Price;
                    }
                    else
                    {
                        this.fpSpread1_Sheet1.Cells[i, (int)EnumColumnName.Qty].Value = feeItemList.Item.Qty;
                        this.fpSpread1_Sheet1.Cells[i, (int)EnumColumnName.Price].Value = FS.FrameWork.Public.String.FormatNumber(feeItemList.Item.Price / feeItemList.Item.PackQty, 4);
                    }

                    decimal totCost = FS.FrameWork.Public.String.FormatNumber(feeItemList.Item.Price * feeItemList.Item.Qty / feeItemList.Item.PackQty, 2);
                    this.fpSpread1_Sheet1.Cells[i, (int)EnumColumnName.Cost].Value = totCost;
                    //实际比例
                    this.fpSpread1_Sheet1.Cells[i, (int)EnumColumnName.EcoRealRate].Value = feeItemList.Patient.Pact.Rate.RebateRate;
                    //显示比例
                    this.fpSpread1_Sheet1.Cells[i, (int)EnumColumnName.EcoRate].Value = FS.FrameWork.Public.String.FormatNumber(feeItemList.Patient.Pact.Rate.RebateRate, 2) * 100;
                    this.fpSpread1_Sheet1.Cells[i, (int)EnumColumnName.EcoCost].Value = FS.FrameWork.Public.String.FormatNumber(feeItemList.Patient.Pact.Rate.RebateRate * totCost, 2);
                    this.fpSpread1_Sheet1.Rows[i].Tag = feeItemList;

                    this.cmbRebateInfo.Text = feeItemList.Memo;
                }

                //最后添加合计行

                if (al.Count > 0)
                {
                    this.sumCost();
                }
            }
        }

        private int save()
        {
            this.fpSpread1.StopCellEditing();

            if (string.IsNullOrEmpty(this.cmbRebateInfo.Text))
            {
                SOC.HISFC.BizProcess.CommonInterface.CommonController.Instance.MessageBox("请选择打折信息", MessageBoxIcon.Information);
                return -1;
            }

            for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
            {
                //是否费用实体
                if (this.fpSpread1_Sheet1.Rows[i].Tag is FS.HISFC.Models.Fee.Outpatient.FeeItemList)
                {
                    FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItemList = this.fpSpread1_Sheet1.Rows[i].Tag as FS.HISFC.Models.Fee.Outpatient.FeeItemList;
                    decimal realRate = FS.FrameWork.Function.NConvert.ToDecimal(this.fpSpread1_Sheet1.Cells[i, (int)EnumColumnName.EcoRealRate].Value);
                    decimal rateCost = FS.FrameWork.Function.NConvert.ToDecimal(this.fpSpread1_Sheet1.Cells[i, (int)EnumColumnName.EcoCost].Value);
                    if (realRate >= 0)
                    {
                        //feeItemList.Patient.Pact.Rate.RebateRate = realRate;
                        feeItemList.Patient.Pact.Rate.RebateRate = realRate;
                        feeItemList.Memo = this.cmbRebateInfo.Text;
                    }
                }
            }

            return 1;

        }

        private void sumCost()
        {

            decimal orgSumTotCost = 0M;
            decimal sumTotCost = 0M;

            for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
            {
                //是否费用实体
                if (this.fpSpread1_Sheet1.Rows[i].Tag is FS.HISFC.Models.Fee.Outpatient.FeeItemList)
                {
                    orgSumTotCost += FS.FrameWork.Function.NConvert.ToDecimal(this.fpSpread1_Sheet1.Cells[i, (int)EnumColumnName.Cost].Value);
                    sumTotCost += FS.FrameWork.Function.NConvert.ToDecimal(this.fpSpread1_Sheet1.Cells[i, (int)EnumColumnName.EcoCost].Value);
                }
            }

            if (this.fpSpread1_Sheet1.RowCount > 0)
            {
                if (this.fpSpread1_Sheet1.Rows[this.fpSpread1_Sheet1.RowCount - 1].Tag is FS.HISFC.Models.Fee.Outpatient.FeeItemList)
                {
                    this.fpSpread1_Sheet1.RowCount++;
                }
                int row = this.fpSpread1_Sheet1.RowCount - 1;
                this.fpSpread1_Sheet1.Cells[row, (int)EnumColumnName.ItemName].Value = "合计：";
                this.fpSpread1_Sheet1.Cells[row, (int)EnumColumnName.Cost].Value = orgSumTotCost;
                this.fpSpread1_Sheet1.Cells[row, (int)EnumColumnName.EcoCost].Value = sumTotCost;
                this.fpSpread1_Sheet1.Cells[row, (int)EnumColumnName.EcoRate].Value = null;
                this.fpSpread1_Sheet1.Cells[row, (int)EnumColumnName.EcoRealRate].Value = null;
                this.fpSpread1_Sheet1.Rows[row].Locked = true;
                this.fpSpread1_Sheet1.Cells[row, (int)EnumColumnName.EcoRate].Locked = false;
                this.fpSpread1_Sheet1.Cells[row, (int)EnumColumnName.EcoRealRate].Locked = false;

            }
        }

    }

    enum EnumColumnName
    {
        ItemName,
        Qty,
        Price,
        Cost,
        EcoRate,
        EcoRealRate,
        EcoCost
    }
}
