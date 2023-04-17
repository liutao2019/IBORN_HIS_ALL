﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FS.SOC.HISFC.BizProcess.CommonInterface;
using System.Collections;

namespace FS.SOC.HISFC.InpatientFee.Components.Balance
{
    /// <summary>
    /// 支付方式列表
    /// </summary>
    enum ColumnPay
    {
        PayWay,
        PayCost,
        PayBank,
        PayAccountNO,
        PayCorporation,
        PayTranscationNO
    }

    /// <summary>
    /// [功能描述: 结算支付方式选择]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2012-4]<br></br>
    /// </summary>
    public partial class frmBalancePay : FS.FrameWork.WinForms.Forms.BaseForm
    {
        public frmBalancePay()
        {
            InitializeComponent();

            this.txtPay.KeyDown += new KeyEventHandler(txtPay_KeyDown);
            this.txtPay.Leave+=new EventHandler(txtPay_Leave);
            this.txtCharge.KeyDown += new KeyEventHandler(txtCharge_KeyDown);
            this.btnCancel.Click += new EventHandler(btnCancel_Click);
            this.btnOK.Click += new EventHandler(btnOK_Click);
            this.fpPayType_Sheet1.CellChanged += new FarPoint.Win.Spread.SheetViewEventHandler(fpPayType_Sheet1_CellChanged);
            this.btnReCompute.Click += new EventHandler(btnReCompute_Click);
        }
        
        #region 变量

        private FS.FrameWork.Public.ObjectHelper payWayHelper = new FS.FrameWork.Public.ObjectHelper();
        private FS.FrameWork.Public.ObjectHelper bankHelper = new FS.FrameWork.Public.ObjectHelper();
        #endregion

        #region 属性
        private decimal shouldPay = 0M;
        /// <summary>
        /// 应收金额
        /// </summary>
        public decimal ShouldPay
        {
            set
            {
                this.shouldPay = value;
            }
        }

        private decimal arrearBalance = 0M;
        /// <summary>
        /// 是否欠费结算
        /// </summary>
        public decimal ArrearBalance
        {
            set
            {
                arrearBalance = value;
            }
        }

        private decimal returnPay = 0M;
        /// <summary>
        /// 应返金额
        /// </summary>
        public decimal ReturnPay
        {
            set
            {
                returnPay = value;
            }
        }

        private bool isOK = false;
        public bool IsOK
        {
            get
            {
                return isOK;
            }
        }

        private List<FS.HISFC.Models.Fee.Inpatient.BalancePay> listBalancePay = new List<FS.HISFC.Models.Fee.Inpatient.BalancePay>();
        /// <summary>
        /// 支付方式信息
        /// </summary>
        public List<FS.HISFC.Models.Fee.Inpatient.BalancePay> ListBalancePay
        {
            get
            {
                return listBalancePay;
            }
        }

        /// <summary>
        /// 自付金额 对应 中山医保IC卡
        /// </summary>
        private decimal balancePayCost = 0m;

        /// <summary>
        /// 自付金额 对应 中山医保IC卡
        /// </summary>
        public decimal BalancePayCost
        {
            get { return balancePayCost; }
            set { balancePayCost = value; }
        }

        /// <summary>
        /// 民政补助金额
        /// </summary>
        private decimal balanceMZCost = 0m;

        /// <summary>
        /// 民政补助金额
        /// </summary>
        public decimal BalanceMZCost
        {
            get { return balanceMZCost; }
            set { balanceMZCost = value; }
        }



        #endregion

        #region 方法

        protected virtual void Init()
        {
            //清空
            this.fpPayType_Sheet1.RowCount = 5;

            #region 支付方式
            FarPoint.Win.Spread.CellType.ComboBoxCellType comboType = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
            ArrayList al = CommonController.CreateInstance().QueryConstant(FS.HISFC.Models.Base.EnumConstant.PAYMODES);
            if (al == null)
            {
                CommonController.CreateInstance().MessageBox("获取支付方式失败", MessageBoxIcon.Warning);
                return;
            }
            payWayHelper.ArrayObject = al;
            comboType.Items = new string[al.Count];
            for (int i = 0; i < al.Count; i++)
            {
                FS.FrameWork.Models.NeuObject obj = al[i] as FS.FrameWork.Models.NeuObject;
                comboType.Items[i] = obj.Name;
            }

            this.fpPayType_Sheet1.Columns[(int)ColumnPay.PayWay].CellType = comboType;
            #endregion

            #region 银行
            FarPoint.Win.Spread.CellType.ComboBoxCellType banktype = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
            al = CommonController.CreateInstance().QueryConstant(FS.HISFC.Models.Base.EnumConstant.BANK);
            if (al == null)
            {
                CommonController.CreateInstance().MessageBox("获取开户银行失败", MessageBoxIcon.Warning);
                return;
            }
            bankHelper.ArrayObject = al;
            banktype.Items = new string[al.Count];
            for (int i = 0; i < al.Count; i++)
            {
                FS.FrameWork.Models.NeuObject obj = al[i] as FS.FrameWork.Models.NeuObject;
                banktype.Items[i] = obj.Name;
            }
            this.fpPayType_Sheet1.Columns[(int)ColumnPay.PayBank].CellType = banktype;
            #endregion

            this.ComputeCost();
        }

        private void ComputeCost()
        {
            this.Clear();
            if (arrearBalance > 0)
            {
                this.lblShouldPay.Text = "欠费金额";
                this.txtShouldPay.Text = (shouldPay + arrearBalance).ToString("F2");
                this.lblShouldPay.ForeColor = Color.Red;
                this.txtShouldPay.ForeColor = Color.Red;

                this.fpPayType_Sheet1.RowHeader.Rows[0].Label = "欠";
                this.fpPayType_Sheet1.RowHeader.Rows[0].BackColor = Color.Red;
                this.fpPayType_Sheet1.Cells[0, (int)ColumnPay.PayCost].Value = shouldPay;             

                this.txtPay.Text = shouldPay.ToString("F2");
            }
            else if(shouldPay>0)
            {
                this.lblShouldPay.Text = "应收(自费)";
                this.txtShouldPay.Text = (shouldPay + arrearBalance).ToString("F2");
                this.lblShouldPay.ForeColor = Color.Red;
                this.txtShouldPay.ForeColor = Color.Red;

                this.fpPayType_Sheet1.RowHeader.Rows[0].Label = "收";
                this.fpPayType_Sheet1.RowHeader.Rows[0].BackColor = Color.Red;
                this.fpPayType_Sheet1.Cells[0, (int)ColumnPay.PayCost].Value = shouldPay;
                
                this.txtPay.Text = shouldPay.ToString("F2");
            }
            else if (returnPay > 0)
            {
                this.lblShouldPay.Text = "应退金额";
                this.txtShouldPay.Text = (returnPay).ToString("F2");
                this.lblShouldPay.ForeColor = Color.Blue;
                this.txtShouldPay.ForeColor = Color.Blue;

                this.fpPayType_Sheet1.RowHeader.Rows[0].Label = "退";
                this.fpPayType_Sheet1.RowHeader.Rows[0].BackColor = Color.Blue;
                this.fpPayType_Sheet1.Cells[0, (int)ColumnPay.PayCost].Value = returnPay;

                this.txtPay.Text = returnPay.ToString("F2");
            }
            this.fpPayType_Sheet1.Cells[0, (int)ColumnPay.PayWay].Text = "现金";

            if (this.BalancePayCost > 0 && this.BalanceMZCost <= 0)
            {
                #region 中山医保的PAY_COST的特殊处理(都是收取)

                this.Clear();
                if (arrearBalance > 0)
                {
                    this.lblShouldPay.Text = "欠费金额";
                    this.txtShouldPay.Text = (shouldPay + arrearBalance).ToString("F2");
                    this.lblShouldPay.ForeColor = Color.Red;
                    this.txtShouldPay.ForeColor = Color.Red;

                    this.fpPayType_Sheet1.RowHeader.Rows[0].Label = "收";
                    this.fpPayType_Sheet1.RowHeader.Rows[0].BackColor = Color.Red;
                    this.fpPayType_Sheet1.Cells[0, (int)ColumnPay.PayWay].Text = "社会保障卡(中山)";
                    this.fpPayType_Sheet1.Cells[0, (int)ColumnPay.PayCost].Value = this.BalancePayCost;

                    this.txtPay.Text = shouldPay.ToString("F2");
                }
                else if (shouldPay > 0)
                {
                    this.lblShouldPay.Text = "应收(自费)";
                    this.txtShouldPay.Text = (shouldPay + arrearBalance).ToString("F2");
                    this.lblShouldPay.ForeColor = Color.Red;
                    this.txtShouldPay.ForeColor = Color.Red;

                    this.fpPayType_Sheet1.RowHeader.Rows[0].Label = "收";
                    this.fpPayType_Sheet1.RowHeader.Rows[0].BackColor = Color.Red;
                    this.fpPayType_Sheet1.Cells[0, (int)ColumnPay.PayWay].Text = "社会保障卡(中山)";
                    this.fpPayType_Sheet1.Cells[0, (int)ColumnPay.PayCost].Value = this.BalancePayCost;

                    this.txtPay.Text = shouldPay.ToString("F2");
                }
                else if (returnPay > 0)
                {
                    this.lblShouldPay.Text = "应退金额";
                    this.txtShouldPay.Text = (returnPay).ToString("F2");
                    this.lblShouldPay.ForeColor = Color.Blue;
                    this.txtShouldPay.ForeColor = Color.Blue;

                    this.fpPayType_Sheet1.RowHeader.Rows[0].Label = "收";
                    this.fpPayType_Sheet1.RowHeader.Rows[0].BackColor = Color.Red;
                    this.fpPayType_Sheet1.Cells[0, (int)ColumnPay.PayWay].Text = "社会保障卡(中山)";
                    this.fpPayType_Sheet1.Cells[0, (int)ColumnPay.PayCost].Value = this.BalancePayCost;

                    this.txtPay.Text = returnPay.ToString("F2");
                }

                #endregion
            }

            if (this.BalancePayCost <= 0 && this.BalanceMZCost > 0)
            {
                #region 民政补助金额的特殊处理(都是收取)

                this.Clear();
                if (arrearBalance > 0)
                {
                    this.lblShouldPay.Text = "欠费金额";
                    this.txtShouldPay.Text = (shouldPay + arrearBalance).ToString("F2");
                    this.lblShouldPay.ForeColor = Color.Red;
                    this.txtShouldPay.ForeColor = Color.Red;

                    this.fpPayType_Sheet1.RowHeader.Rows[0].Label = "收";
                    this.fpPayType_Sheet1.RowHeader.Rows[0].BackColor = Color.Red;
                    this.fpPayType_Sheet1.Cells[0, (int)ColumnPay.PayWay].Text = "优惠";
                    this.fpPayType_Sheet1.Cells[0, (int)ColumnPay.PayCost].Value = this.BalanceMZCost;

                    this.txtPay.Text = shouldPay.ToString("F2");
                }
                else if (shouldPay > 0)
                {
                    this.lblShouldPay.Text = "应收(自费)";
                    this.txtShouldPay.Text = (shouldPay + arrearBalance).ToString("F2");
                    this.lblShouldPay.ForeColor = Color.Red;
                    this.txtShouldPay.ForeColor = Color.Red;

                    this.fpPayType_Sheet1.RowHeader.Rows[0].Label = "收";
                    this.fpPayType_Sheet1.RowHeader.Rows[0].BackColor = Color.Red;
                    this.fpPayType_Sheet1.Cells[0, (int)ColumnPay.PayWay].Text = "优惠";
                    this.fpPayType_Sheet1.Cells[0, (int)ColumnPay.PayCost].Value = this.BalanceMZCost;

                    this.txtPay.Text = shouldPay.ToString("F2");
                }
                else if (returnPay > 0)
                {
                    this.lblShouldPay.Text = "应退金额";
                    this.txtShouldPay.Text = (returnPay).ToString("F2");
                    this.lblShouldPay.ForeColor = Color.Blue;
                    this.txtShouldPay.ForeColor = Color.Blue;

                    this.fpPayType_Sheet1.RowHeader.Rows[0].Label = "收";
                    this.fpPayType_Sheet1.RowHeader.Rows[0].BackColor = Color.Red;
                    this.fpPayType_Sheet1.Cells[0, (int)ColumnPay.PayWay].Text = "优惠";
                    this.fpPayType_Sheet1.Cells[0, (int)ColumnPay.PayCost].Value = this.BalanceMZCost;

                    this.txtPay.Text = returnPay.ToString("F2");
                }

                #endregion
            }

            if (this.BalancePayCost > 0 && this.BalanceMZCost > 0)
            {
                #region 中山医保的PAY_COST的特殊处理 与 民政补助金额的特殊处理(都是收取)

                this.Clear();
                if (arrearBalance > 0)
                {
                    this.lblShouldPay.Text = "欠费金额";
                    this.txtShouldPay.Text = (shouldPay + arrearBalance).ToString("F2");
                    this.lblShouldPay.ForeColor = Color.Red;
                    this.txtShouldPay.ForeColor = Color.Red;

                    this.fpPayType_Sheet1.CellChanged -= new FarPoint.Win.Spread.SheetViewEventHandler(fpPayType_Sheet1_CellChanged);
                    this.fpPayType_Sheet1.RowHeader.Rows[0].Label = "收";
                    this.fpPayType_Sheet1.RowHeader.Rows[0].BackColor = Color.Red;
                    this.fpPayType_Sheet1.Cells[0, (int)ColumnPay.PayWay].Text = "社会保障卡(中山)";
                    this.fpPayType_Sheet1.Cells[0, (int)ColumnPay.PayCost].Value = this.BalancePayCost;
                    this.fpPayType_Sheet1.CellChanged += new FarPoint.Win.Spread.SheetViewEventHandler(fpPayType_Sheet1_CellChanged);

                    this.fpPayType_Sheet1.RowHeader.Rows[1].Label = "收";
                    this.fpPayType_Sheet1.RowHeader.Rows[1].BackColor = Color.Red;
                    this.fpPayType_Sheet1.Cells[1, (int)ColumnPay.PayWay].Text = "优惠";
                    this.fpPayType_Sheet1.Cells[1, (int)ColumnPay.PayCost].Value = this.BalanceMZCost;

                    this.txtPay.Text = shouldPay.ToString("F2");
                }
                else if (shouldPay > 0)
                {
                    this.lblShouldPay.Text = "应收(自费)";
                    this.txtShouldPay.Text = (shouldPay + arrearBalance).ToString("F2");
                    this.lblShouldPay.ForeColor = Color.Red;
                    this.txtShouldPay.ForeColor = Color.Red;

                    this.fpPayType_Sheet1.CellChanged -= new FarPoint.Win.Spread.SheetViewEventHandler(fpPayType_Sheet1_CellChanged);
                    this.fpPayType_Sheet1.RowHeader.Rows[0].Label = "收";
                    this.fpPayType_Sheet1.RowHeader.Rows[0].BackColor = Color.Red;
                    this.fpPayType_Sheet1.Cells[0, (int)ColumnPay.PayWay].Text = "社会保障卡(中山)";
                    this.fpPayType_Sheet1.Cells[0, (int)ColumnPay.PayCost].Value = this.BalancePayCost;
                    this.fpPayType_Sheet1.CellChanged += new FarPoint.Win.Spread.SheetViewEventHandler(fpPayType_Sheet1_CellChanged);

                    this.fpPayType_Sheet1.RowHeader.Rows[1].Label = "收";
                    this.fpPayType_Sheet1.RowHeader.Rows[1].BackColor = Color.Red;
                    this.fpPayType_Sheet1.Cells[1, (int)ColumnPay.PayWay].Text = "优惠";
                    this.fpPayType_Sheet1.Cells[1, (int)ColumnPay.PayCost].Value = this.BalanceMZCost;

                    this.txtPay.Text = shouldPay.ToString("F2");
                }
                else if (returnPay > 0)
                {
                    this.lblShouldPay.Text = "应退金额";
                    this.txtShouldPay.Text = (returnPay).ToString("F2");
                    this.lblShouldPay.ForeColor = Color.Blue;
                    this.txtShouldPay.ForeColor = Color.Blue;

                    this.fpPayType_Sheet1.CellChanged -= new FarPoint.Win.Spread.SheetViewEventHandler(fpPayType_Sheet1_CellChanged);
                    this.fpPayType_Sheet1.RowHeader.Rows[0].Label = "收";
                    this.fpPayType_Sheet1.RowHeader.Rows[0].BackColor = Color.Red;
                    this.fpPayType_Sheet1.Cells[0, (int)ColumnPay.PayWay].Text = "社会保障卡(中山)";
                    this.fpPayType_Sheet1.Cells[0, (int)ColumnPay.PayCost].Value = this.BalancePayCost;
                    this.fpPayType_Sheet1.CellChanged += new FarPoint.Win.Spread.SheetViewEventHandler(fpPayType_Sheet1_CellChanged);

                    this.fpPayType_Sheet1.RowHeader.Rows[1].Label = "收";
                    this.fpPayType_Sheet1.RowHeader.Rows[1].BackColor = Color.Red;
                    this.fpPayType_Sheet1.Cells[1, (int)ColumnPay.PayWay].Text = "优惠";
                    this.fpPayType_Sheet1.Cells[1, (int)ColumnPay.PayCost].Value = this.BalanceMZCost;

                    this.txtPay.Text = returnPay.ToString("F2");
                }

                #endregion
            }

            this.txtPay.Focus();
            this.txtPay.SelectAll();
        }

        /// <summary>
        /// 清屏
        /// </summary>
        public void Clear()
        {
            this.fpPayType_Sheet1.CellChanged -= new FarPoint.Win.Spread.SheetViewEventHandler(fpPayType_Sheet1_CellChanged);

            this.listBalancePay.Clear();
            for (int i = 0; i < this.fpPayType_Sheet1.RowCount; i++)
            {
                this.fpPayType_Sheet1.Cells[i, (int)ColumnPay.PayWay].Text = string.Empty;
                this.fpPayType_Sheet1.Cells[i, (int)ColumnPay.PayCost].Value = 0M;
                this.fpPayType_Sheet1.Cells[i, (int)ColumnPay.PayBank].Text = string.Empty;
                this.fpPayType_Sheet1.Cells[i, (int)ColumnPay.PayCorporation].Text = string.Empty;
                this.fpPayType_Sheet1.Cells[i, (int)ColumnPay.PayAccountNO].Text = string.Empty;
                this.fpPayType_Sheet1.Cells[i, (int)ColumnPay.PayTranscationNO].Text = string.Empty;

                this.fpPayType_Sheet1.RowHeader.Rows[i].Label = (i + 1).ToString();  //行号
                this.fpPayType_Sheet1.RowHeader.Rows[i].BackColor = Color.White;
            }
            this.lblShouldPay.Text = "应收(自费)";
            this.txtShouldPay.Text = "0.00";
            this.lblShouldPay.ForeColor = Color.Red;
            this.txtShouldPay.ForeColor = Color.Red;
            this.txtPay.Text = "0.00";
            this.txtCharge.Text = "0.00";
            this.txtCharge.ReadOnly = true;
            this.txtCharge.AllowNegative = true;
            this.isOK = false;

            this.fpPayType_Sheet1.CellChanged += new FarPoint.Win.Spread.SheetViewEventHandler(fpPayType_Sheet1_CellChanged);

        }

        private bool ValidBalancePay(ref string errText)
        {
            decimal payTypeCost = 0m;
            string payType = "";
            decimal payAllCost = 0m;

            for (int i = 0; i < this.fpPayType_Sheet1.Rows.Count; i++)
            {
                payType = this.fpPayType_Sheet1.Cells[i, (int)ColumnPay.PayWay].Text;
                if (this.fpPayType_Sheet1.Cells[i, (int)ColumnPay.PayCost].Value == null)
                {
                    payTypeCost = 0;
                }
                else
                {
                    int sign = 1;
                    if (arrearBalance > 0 && this.fpPayType_Sheet1.RowHeader.Rows[i].Label == "欠")
                    {
                        sign = 1;
                    }
                    else if (shouldPay > 0 && this.fpPayType_Sheet1.RowHeader.Rows[i].Label == "收")
                    {
                        sign = 1;
                    }
                    else if (returnPay > 0 && this.fpPayType_Sheet1.RowHeader.Rows[i].Label == "退")
                    {
                        sign = 1;
                    }
                    else
                    {
                        sign = -1;
                    }

                    payTypeCost = decimal.Parse(this.fpPayType_Sheet1.Cells[i, (int)ColumnPay.PayCost].Value.ToString()) * sign;
                }

                #region 屏蔽 by ma_d
                //if (payType.Trim() != "" && payTypeCost > 0)
                //{
                //    if (payType.Trim() == "支票" || payType.Trim() == "汇票")
                //    {
                //        if (this.fpPayType_Sheet1.Cells[i, (int)ColumnPay.PayBank].Text.Trim() == "" ||
                //            this.fpPayType_Sheet1.Cells[i, (int)ColumnPay.PayAccountNO].Text.Trim() == "" ||
                //            this.fpPayType_Sheet1.Cells[i, (int)ColumnPay.PayCorporation].Text.Trim() == "" ||
                //            this.fpPayType_Sheet1.Cells[i, (int)ColumnPay.PayTranscationNO].Text.Trim() == "")
                //        {
                //            errText = "支票汇票必须将完整的开户银行,帐户,开据单位,票号补充完整!";
                //            return false;
                //        }
                //    }
                //    if (payType.Trim() == "借记卡" || payType.Trim() == "信用卡")
                //    {
                //        if (this.fpPayType_Sheet1.Cells[i, (int)ColumnPay.PayBank].Text.Trim() == "" ||
                //            this.fpPayType_Sheet1.Cells[i, (int)ColumnPay.PayAccountNO].Text.Trim() == "" ||
                //            this.fpPayType_Sheet1.Cells[i, (int)ColumnPay.PayTranscationNO].Text.Trim() == "")
                //        {
                //            errText = "支票汇票必须将完整的开户,银行帐户,交易号补充完整!";
                //            return false;
                //        }
                //    }
                //}
                #endregion

                payAllCost += payTypeCost;
            }
            //控制不能同时使用两种相同的支付方式
            for (int i = 0; i < this.fpPayType_Sheet1.RowCount; i++)
            {
                string paytype = this.fpPayType_Sheet1.Cells[i, (int)ColumnPay.PayWay].Text.Trim();
                if (paytype != "")
                {
                    for (int j = i + 1; j < this.fpPayType_Sheet1.RowCount; j++)
                    {
                        if (paytype == this.fpPayType_Sheet1.Cells[j, (int)ColumnPay.PayWay].Text.Trim())
                        {
                            errText = "不能同时使用两种相同的支付方式";
                            return false;
                        }
                    }
                }
                else
                {
                    if (FS.FrameWork.Function.NConvert.ToDecimal(this.fpPayType_Sheet1.Cells[i, (int)ColumnPay.PayCost].Value)>0)
                    {
                        errText = "支付方式不能为空";
                        return false;
                    }
                }
            }

            //如果是欠费
            if (arrearBalance>0)
            {
                if (this.shouldPay + arrearBalance < payAllCost)
                {
                    errText = "实收费用之和" + payAllCost.ToString() + "大于应收款项总额" + this.shouldPay.ToString() + "!";
                    return false;
                }
            }
            else
            {
                //判断是否金额相等
                if (returnPay > 0)
                {
                    if (this.returnPay != payAllCost)
                    {
                        errText = "应退款项总额" + this.returnPay.ToString() + "与分项费用之和" + payAllCost.ToString() + "不符合!";
                        return false;
                    }
                }
                else
                {
                    if (this.shouldPay != payAllCost)
                    {
                        errText = "应收款项总额" + this.shouldPay.ToString() + "与分项费用之和" + payAllCost.ToString() + "不符合!";
                        return false;
                    }
                }
            }

            return true;
        }

        private int Save()
        {
            string errText="";

            if (!this.ValidBalancePay(ref errText))
            {
                CommonController.CreateInstance().MessageBox(errText, MessageBoxIcon.Warning);
                return -1;
            }
            string payType = "";
            decimal payTypeCost = 0m;
            string bankName = "";
            for (int i = 0; i < this.fpPayType_Sheet1.Rows.Count; i++)
            {
                payType = this.fpPayType_Sheet1.Cells[i, (int)ColumnPay.PayWay].Text;
                bankName = this.fpPayType_Sheet1.Cells[i, (int)ColumnPay.PayBank].Text;
                if (this.fpPayType_Sheet1.Cells[i, (int)ColumnPay.PayCost].Value == null)
                {
                    payTypeCost = 0;
                }
                else
                {
                    payTypeCost = FS.FrameWork.Function.NConvert.ToDecimal(this.fpPayType_Sheet1.Cells[i, (int)ColumnPay.PayCost].Value.ToString());
                }

                if (payType.Trim() != "" && payTypeCost > 0)
                {
                    FS.HISFC.Models.Fee.Inpatient.BalancePay balancePay = new FS.HISFC.Models.Fee.Inpatient.BalancePay();
                    balancePay.TransKind.ID = "1";
                    balancePay.TransType = FS.HISFC.Models.Base.TransTypes.Positive;
                    balancePay.PayType.ID = payWayHelper.GetID(payType);
                    balancePay.PayType.Name = payType;
                    balancePay.FT.TotCost = payTypeCost;
                    balancePay.Qty = 1;
                    balancePay.Bank.ID = bankHelper.GetID(bankName);
                    balancePay.Bank.Name = bankName;
                    balancePay.Bank.Account = this.fpPayType_Sheet1.Cells[i, (int)ColumnPay.PayAccountNO].Text;
                    balancePay.Bank.WorkName = this.fpPayType_Sheet1.Cells[i, (int)ColumnPay.PayCorporation].Text;
                    balancePay.Bank.InvoiceNO = this.fpPayType_Sheet1.Cells[i, (int)ColumnPay.PayTranscationNO].Text;

                    //1补收；2返还
                    //balancePay.RetrunOrSupplyFlag = returnPay > 0 ? "2" : "1";
                    if (this.fpPayType_Sheet1.RowHeader.Rows[i].Label == "退")
                    {
                        balancePay.RetrunOrSupplyFlag = "2";
                    }
                    else
                    {
                        balancePay.RetrunOrSupplyFlag = "1";
                    }

                    this.listBalancePay.Add(balancePay.Clone());
                }
            }
            return 1;
        }

        #endregion

        #region 事件

        /// <summary>
        /// 支付farpoint CellChange事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void fpPayType_Sheet1_CellChanged(object sender, FarPoint.Win.Spread.SheetViewEventArgs e)
        {
            //只响应金额的变化
            if (e.Column != (int)ColumnPay.PayCost) return;
            //金额判断
            decimal PayTypeCost = 0m;
            for (int i = 0; i < this.fpPayType_Sheet1.Rows.Count; i++)
            {
                if (this.fpPayType_Sheet1.Cells[i, (int)ColumnPay.PayCost].Value == null)
                {
                    continue;
                }
                else
                {
                    int sign = 1;
                    if (arrearBalance > 0 && this.fpPayType_Sheet1.RowHeader.Rows[i].Label == "欠")
                    {
                        sign = 1;
                    }
                    else if (shouldPay > 0 && this.fpPayType_Sheet1.RowHeader.Rows[i].Label == "收")
                    {
                        sign = 1;
                    }
                    else if (returnPay > 0 && this.fpPayType_Sheet1.RowHeader.Rows[i].Label == "退")
                    {
                        sign = 1;
                    }
                    else
                    {
                        sign = -1;
                    }

                    PayTypeCost += decimal.Parse(this.fpPayType_Sheet1.Cells[i, (int)ColumnPay.PayCost].Value.ToString()) * sign;
                }
            }
            decimal Spay = decimal.Parse(this.txtShouldPay.Text);
            if (PayTypeCost == Spay)
            {
                return;
            }
            if (PayTypeCost < Spay)
            {
                decimal Rest = Spay - PayTypeCost;
                for (int i = 0; i < this.fpPayType_Sheet1.Rows.Count; i++)
                {
                    if (this.fpPayType_Sheet1.Cells[i, (int)ColumnPay.PayWay].Value == null)
                    {
                        if (arrearBalance > 0)
                        {
                            this.fpPayType_Sheet1.RowHeader.Rows[i].Label = "欠";
                            this.fpPayType_Sheet1.RowHeader.Rows[i].BackColor = Color.Red;
                        }
                        else if (shouldPay > 0)
                        {
                            this.fpPayType_Sheet1.RowHeader.Rows[i].Label = "收";
                            this.fpPayType_Sheet1.RowHeader.Rows[i].BackColor = Color.Red;
                        }
                        else if (returnPay > 0)
                        {
                            this.fpPayType_Sheet1.RowHeader.Rows[i].Label = "退";
                            this.fpPayType_Sheet1.RowHeader.Rows[i].BackColor = Color.Blue;
                        }

                        this.fpPayType_Sheet1.Cells[i, (int)ColumnPay.PayWay].Value = "现金";
                        this.fpPayType_Sheet1.Cells[i,  (int)ColumnPay.PayCost].Value = Rest;
                        this.fpPayType.Focus();
                        this.fpPayType_Sheet1.SetActiveCell(i, (int)ColumnPay.PayWay);
                        break;
                    }
                }
                return;
            }
            if (PayTypeCost > Spay)
            {
                bool found = false;

                for (int i = 0; i < this.fpPayType_Sheet1.Rows.Count; i++)
                {
                    if (false && this.fpPayType_Sheet1.Cells[i, (int)ColumnPay.PayWay].Text.Trim() == "现金")
                    {
                        #region 废弃

                        decimal Cash = 0m;//现金
                        decimal Rest = 0m;//其他支付方式
                        Cash = decimal.Parse(this.fpPayType_Sheet1.Cells[i, (int)ColumnPay.PayCost].Value.ToString());
                        Rest = PayTypeCost - Cash;
                        if (Rest > Spay)
                        {
                            FS.FrameWork.WinForms.Classes.Function.Msg("现金为负值，如果不允许，请更改！", 111);
                        }

                        if (decimal.Parse(this.txtPay.Text) > Cash)
                        {
                            //找零
                            decimal RealPayCost = decimal.Parse(this.txtPay.Text);
                            decimal charge = 0m;
                            charge = RealPayCost - Cash;
                            this.txtCharge.Text = charge.ToString();
                        }

                        //现金为其他支付方式与应收应付之间的差额
                        this.fpPayType_Sheet1.SetValue(i, (int)ColumnPay.PayCost, Spay - Rest);
                        found = true;
                        break;

                        #endregion
                    }
                    else if (this.fpPayType_Sheet1.Cells[i, (int)ColumnPay.PayWay].Value == null)
                    {
                        found = true;
                        decimal Rest = Spay - PayTypeCost;  //差异金额

                        if (arrearBalance > 0)
                        {
                            this.fpPayType_Sheet1.RowHeader.Rows[i].Label = "退";
                            this.fpPayType_Sheet1.RowHeader.Rows[i].BackColor = Color.Blue;
                        }
                        else if (shouldPay > 0)
                        {
                            this.fpPayType_Sheet1.RowHeader.Rows[i].Label = "退";
                            this.fpPayType_Sheet1.RowHeader.Rows[i].BackColor = Color.Blue;
                        }
                        else if (returnPay > 0)
                        {
                            this.fpPayType_Sheet1.RowHeader.Rows[i].Label = "收";
                            this.fpPayType_Sheet1.RowHeader.Rows[i].BackColor = Color.Red;
                        }

                        this.fpPayType_Sheet1.Cells[i, (int)ColumnPay.PayWay].Value = "现金";
                        this.fpPayType_Sheet1.Cells[i, (int)ColumnPay.PayCost].Value = Math.Abs(Rest);
                        this.fpPayType.Focus();
                        this.fpPayType_Sheet1.SetActiveCell(i, (int)ColumnPay.PayWay);
                        break;
                    }
                }

                if (!found)
                {
                    FS.FrameWork.WinForms.Classes.Function.Msg("除现金外，其它支付方式不能大于应收应付金额", 111);
                    return;
                }
            }
        }

        private void txtPay_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtCharge.Focus();
            }
        }

        protected void txtPay_Leave(object sender, EventArgs e)
        {
            if (this.txtPay.Text == "") this.txtPay.Text = "0.00";

            if (decimal.Parse(this.txtPay.Text) == 0)
            {
                this.txtCharge.Text = "0.00";
                return;
            }
            if (decimal.Parse(this.txtPay.Text) < 0)
            {
                FS.FrameWork.WinForms.Classes.Function.Msg("实付金额应大于零!", 111);
                return;
            }
            //实收
            decimal RealPayCost = 0m;
            //ShouldPay = decimal.Parse(this.txtShouldPay.Text);
            //应收只计算现金部分，其他的不计算
            decimal Cash = 0m;//现金
            for (int i = 0; i < this.fpPayType_Sheet1.Rows.Count; i++)
            {
                if (this.fpPayType_Sheet1.Cells[i, (int)ColumnPay.PayWay].Text.Trim() == "现金")
                {
                    Cash = decimal.Parse(this.fpPayType_Sheet1.Cells[i, (int)ColumnPay.PayCost].Value.ToString());
                    break;
                }
            }
            RealPayCost = decimal.Parse(this.txtPay.Text);
            //找零
            decimal charge = 0m;
            charge = RealPayCost - Cash;
            this.txtCharge.Text = charge.ToString();
        }

        void btnCancel_Click(object sender, EventArgs e)
        {
            this.isOK = false;
            this.Close();
        }

        void btnOK_Click(object sender, EventArgs e)
        {
            if (this.Save() > 0)
            {
                this.isOK = true;
                this.Close();
            }
        }

        void btnReCompute_Click(object sender, EventArgs e)
        {
            this.ComputeCost();
        }

        void txtCharge_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.btnOK.Focus();
            }
        }

        #endregion

        #region 重载

        protected override void OnLoad(EventArgs e)
        {
            this.Init();
            base.OnLoad(e);
        }

        #endregion
    }
}
