using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.Fee.Outpatient;
using FS.FrameWork.Function;

namespace FS.SOC.Local.OutpatientFee.ZhuHai.Zdwy.IOutpatientItemInputAndDisplay
{
    public partial class frmDiscount : Form
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public frmDiscount()
        {
            InitializeComponent();
            this.addEvent();

            //事件
            this.btnRate.Click += new EventHandler(btnRate_Click);
        }

        //{3DDB8F6F-C32B-4d09-A7E1-CB0011A82ED0}
        private void addEvent()
        {
            this.tbRate1.TextChanged += new System.EventHandler(this.tbRate1_TextChanged);
            this.tbIncome1.TextChanged += new EventHandler(tbIncome1_TextChanged);
            this.tbEtc1.TextChanged += new EventHandler(tbEtc1_TextChanged);
        }

        private void deleteEvent()
        {
            this.tbEtc1.TextChanged -= new EventHandler(tbEtc1_TextChanged);
            this.tbIncome1.TextChanged -= new EventHandler(tbIncome1_TextChanged);
            this.tbRate1.TextChanged -= new System.EventHandler(this.tbRate1_TextChanged);
        }

        #region 变量和属性

        /// <summary>
        /// 费用明细
        /// </summary>
        private ArrayList alFeeItemList = new ArrayList();

        /// <summary>
        /// 费用明细
        /// </summary>
        public ArrayList AlFeeItemList
        {
            get
            {
                return this.alFeeItemList;
            }
            set
            {
                this.alFeeItemList = value;
            }
        }

        /// <summary>
        /// 折扣类型：0单条项目打折；1整张处方打折；
        /// </summary>
        private int rateType = 0;

        /// <summary>
        /// 折扣类型：0单条项目打折；1整张处方打折；
        /// </summary>
        public int RateType
        {
            get
            {
                return this.rateType;
            }
            set
            {
                this.rateType = value;
                if (value == 0)
                {
                    this.lblRateType.Text = "单项目折扣";
                }
                else
                {
                    this.lblRateType.Text = "处方项目折扣";

                }
            }
        }

        /// <summary>
        /// 原价
        /// </summary>
        private decimal totCost = 0;

        #endregion

        #region 方法和事件

        /// <summary>
        /// 初始化方法
        /// </summary>
        /// <returns></returns>
        public bool Init()
        {
            try
            {
                //判定
                if (this.alFeeItemList == null || this.alFeeItemList.Count <= 0)
                {
                    return false;
                }

                this.totCost = 0; //清空

                foreach (FeeItemList fItem in this.alFeeItemList)
                {
                    this.totCost += (fItem.FT.OwnCost + fItem.FT.PubCost + fItem.FT.PayCost);
                }
                this.lblTotCost.Text = this.totCost.ToString();
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 折扣确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.tbIncome1.Text) || string.IsNullOrEmpty(this.tbEtc1.Text))
            {
                MessageBox.Show("请输入比例进行折算！");
                return;
            }

            this.SetPriceInRecipes(NConvert.ToDecimal(this.tbIncome1.Text), NConvert.ToDecimal(this.tbEtc1.Text));

            this.Close();
        }

        /// <summary>
        /// 设置费用明细
        /// </summary>
        /// <param name="realCost"></param>
        /// <param name="ecoCost"></param>
        private void SetPriceInRecipes(decimal realCost, decimal ecoCost)
        {
            try
            {
                FeeItemList lastFeeItem = null;
                decimal leftEcoCost = ecoCost;
                foreach (FeeItemList feeItem in this.alFeeItemList)
                {
                    if (feeItem == null)
                    {
                        continue;
                    }

                    feeItem.FT.RebateCost = Math.Round(((feeItem.FT.OwnCost + feeItem.FT.PubCost + feeItem.FT.PayCost) / this.totCost) * ecoCost, 2);

                    leftEcoCost -= feeItem.FT.RebateCost;

                    lastFeeItem = feeItem;
                }

                if (lastFeeItem != null)
                {
                    lastFeeItem.FT.RebateCost += leftEcoCost;
                }
            }
            catch (Exception ex) { }
        }


        private void tbRate1_TextChanged(object sender, EventArgs e)
        {
            //{3DDB8F6F-C32B-4d09-A7E1-CB0011A82ED0}
            this.deleteEvent();

            decimal rate = 100;

            try
            {

                if (string.IsNullOrEmpty(this.tbRate1.Text))
                {
                    this.tbRate1.Text = "0";
                }

                rate = NConvert.ToDecimal(this.tbRate1.Text);
                if (rate < 0 || rate > 100)
                {
                    this.tbRate1.Text = this.tbRate1.Text.Substring(0, this.tbRate1.Text.Length - 1);
                    this.tbRate1.SelectAll();
                    throw new Exception("请输入0~100之间的数字!");
                }

                this.tbIncome1.Text = (this.totCost * rate / 100).ToString("F2");
                this.tbEtc1.Text = (this.totCost - NConvert.ToDecimal(tbIncome1.Text)).ToString("F2");
            }
            catch
            {
                MessageBox.Show("请输入正确的数字!");
                this.tbRate1.Text = this.tbRate1.Text.Substring(0, this.tbRate1.Text.Length - 1);
                this.tbRate1.SelectAll();
            }

            this.addEvent();

        }

        void tbEtc1_TextChanged(object sender, EventArgs e)
        {
            this.deleteEvent();
            decimal etc = 0;
            try
            {
                if (string.IsNullOrEmpty(this.tbEtc1.Text))
                {
                    this.tbEtc1.Text = "0";
                }

                etc = Decimal.Parse(this.tbEtc1.Text);
                if (etc < 0 || etc > this.totCost)
                {
                    this.tbEtc1.Text = this.tbEtc1.Text.Substring(0, this.tbEtc1.Text.Length - 1);
                    this.tbEtc1.SelectAll();
                   throw new Exception("请输入正确的金额!");
                }

                this.tbRate1.Text = (((totCost - etc) / this.totCost) * 100).ToString("F2");
                this.tbIncome1.Text = (totCost - etc).ToString("F2");
            }
            catch
            {
                MessageBox.Show("请输入正确的数字!");
                this.tbEtc1.Text = this.tbEtc1.Text.Substring(0, this.tbEtc1.Text.Length - 1);
                this.tbEtc1.SelectAll();
            }

            this.addEvent();
        }

        void tbIncome1_TextChanged(object sender, EventArgs e)
        {
            this.deleteEvent();
            decimal income = 0;
            try
            {
                if (string.IsNullOrEmpty(this.tbIncome1.Text))
                {
                    this.tbIncome1.Text = "0";
                }
                income = Decimal.Parse(this.tbIncome1.Text);
                if (income < 0 || income > this.totCost)
                {
                    this.tbIncome1.Text = this.tbEtc1.Text.Substring(0, this.tbIncome1.Text.Length - 1);
                    this.tbIncome1.SelectAll();
                    throw new Exception("请输入正确的金额!");
                }

                this.tbRate1.Text = ((income / this.totCost) * 100).ToString("F2");
                this.tbEtc1.Text = (totCost - income).ToString("F2");
            }
            catch
            {
                MessageBox.Show("请输入正确的数字!");
                this.tbIncome1.Text = this.tbEtc1.Text.Substring(0, this.tbIncome1.Text.Length - 1);
                this.tbIncome1.SelectAll();
            }
            this.addEvent();
        }



        #endregion
    }
}
