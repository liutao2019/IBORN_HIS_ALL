using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Registration.Forms
{
    public partial class frmDiscount : Form
    {
        /// <summary>
        /// 挂号费用明细
        /// </summary>
        public ArrayList RegFeeInfo = new ArrayList();

        /// <summary>
        /// 原价
        /// </summary>
        private double totCost = 0.0;

        public frmDiscount()
        {
            InitializeComponent();
            this.btnRate.Click += new EventHandler(btnRate_Click);
            this.btnIncome.Click += new EventHandler(BtnIncome_Click);
            this.btnEtc.Click += new EventHandler(btnEtc_Click);
        }

        private void btnEtc_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.tbIncome3.Text) || string.IsNullOrEmpty(this.tbEtc3.Text))
            {
                MessageBox.Show("请输入优惠金额进行折算！");
                return;
            }
            SetPriceInRecipes(Double.Parse(this.tbIncome3.Text), Double.Parse(this.tbEtc3.Text));
            this.Close();
        }

        private void BtnIncome_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.tbIncome2.Text) || string.IsNullOrEmpty(this.tbEtc2.Text))
            {
                MessageBox.Show("请输入目标金额进行折算！");
                return;
            }
            SetPriceInRecipes(Double.Parse(this.tbIncome2.Text), Double.Parse(this.tbEtc2.Text));
            this.Close();
        }

        private void btnRate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.tbIncome1.Text) || string.IsNullOrEmpty(this.tbEtc1.Text))
            {
                MessageBox.Show("请输入比例进行折算！");
                return;
            }
            SetPriceInRecipes(Double.Parse(this.tbIncome1.Text), Double.Parse(this.tbEtc1.Text));
            this.Close();
        }

        private void SetPriceInRecipes(double incomes,double etc)
        {
            try
            {
                double RealCost= incomes;
                double EtcCost = etc;

                FS.HISFC.Models.Registration.RegisterFeeDetail lastfee = null;

                foreach (FS.HISFC.Models.Registration.RegisterFeeDetail obj in RegFeeInfo)
                {
                    if (obj == null)
                    {
                        break;
                    }

                    double tmpReal = (Double.Parse(obj.Tot_cost.ToString()) / this.totCost) * incomes;
                    double TmpEtc = (Double.Parse(obj.Tot_cost.ToString()) / this.totCost) * etc;
                    obj.Real_cost = Decimal.Parse(tmpReal.ToString("F2"));
                    obj.Etc_cost = Decimal.Parse(TmpEtc.ToString("F2"));
                    RealCost -= Double.Parse(obj.Real_cost.ToString("F2"));
                    EtcCost -= Double.Parse(obj.Etc_cost.ToString("F2"));
                    lastfee = obj;
                }

                if (lastfee != null)
                {
                    lastfee.Real_cost += Decimal.Parse(RealCost.ToString("F2"));
                    lastfee.Etc_cost += Decimal.Parse(EtcCost.ToString("F2"));
                }
            }
            catch(Exception ex)
            {
            }

        }

        public bool Init()
        {
            try
            {
                foreach (FS.HISFC.Models.Registration.RegisterFeeDetail obj in RegFeeInfo)
                {
                    totCost += Double.Parse(obj.Tot_cost.ToString());
                }

                this.lblTotCost.Text = this.totCost.ToString();
            }
            catch
            {
                return false;
            }

            return true;
        }

        private void tbRate1_TextChanged(object sender, EventArgs e)
        {
            double rate = 100;

            if (string.IsNullOrEmpty(this.tbRate1.Text))
            {
                this.tbIncome1.Text = "";
                this.tbEtc1.Text = "";
                return;
            }

            try
            {
                rate = Double.Parse(this.tbRate1.Text);
                if (rate < 0 || rate > 100)
                {
                    MessageBox.Show("请输入0~100之间的数字!");
                    this.tbRate1.Text = this.tbRate1.Text.Substring(0, this.tbRate1.Text.Length - 1);
                    this.tbRate1.SelectAll();
                    return;
                }
            }
            catch
            {
                MessageBox.Show("请输入正确的数字!");
                this.tbRate1.Text = this.tbRate1.Text.Substring(0,this.tbRate1.Text .Length -1);
                this.tbRate1.SelectAll();
                return;
            }

            this.tbIncome1.Text = (this.totCost*rate/100).ToString("F2");
            this.tbEtc1.Text = (this.totCost - Double.Parse(tbIncome1.Text)).ToString("F2");
            return;
        }

        private void tbIncome2_TextChanged(object sender, EventArgs e)
        {
            double income = this.totCost;
            if (string.IsNullOrEmpty(this.tbIncome2.Text))
            {
                this.tbRate2.Text = "";
                this.tbEtc2.Text = "";
                return;
            }

            try
            {
                income = Double.Parse(this.tbIncome2.Text);
                if (income < 0 || income > this.totCost)
                {
                    MessageBox.Show("请输入正确的金额!");
                    this.tbIncome2.Text = this.tbIncome2.Text.Substring(0, this.tbIncome2.Text.Length - 1);
                    this.tbIncome2.SelectAll();
                    return;
                }
            }
            catch
            {
                MessageBox.Show("请输入正确的数字!");
                this.tbIncome2.Text = this.tbIncome2.Text.Substring(0, this.tbIncome2.Text.Length - 1);
                this.tbIncome2.SelectAll();
                return;
            }

            this.tbRate2.Text = (income / this.totCost).ToString("F2");
            this.tbEtc2.Text = (this.totCost - income).ToString("F2");
            return;
        }

        private void tbEtc3_TextChanged(object sender, EventArgs e)
        {
            double Etc = this.totCost;
            if (string.IsNullOrEmpty(this.tbEtc3.Text))
            {
                this.tbRate3.Text = "";
                this.tbIncome3.Text = "";
                return;
            }

            try
            {
                Etc = Double.Parse(this.tbEtc3.Text);
                if (Etc < 0 || Etc > this.totCost)
                {
                    MessageBox.Show("请输入正确的金额!");
                    this.tbEtc3.Text = this.tbEtc3.Text.Substring(0, this.tbEtc3.Text.Length - 1);
                    this.tbEtc3.SelectAll();
                    return;
                }
            }
            catch
            {
                MessageBox.Show("请输入正确的数字!");
                this.tbEtc3.Text = this.tbEtc3.Text.Substring(0, this.tbEtc3.Text.Length - 1);
                this.tbEtc3.SelectAll();
                return;
            }

            this.tbRate3.Text = ((this.totCost - Etc) / this.totCost).ToString("F2");
            this.tbIncome3.Text = (this.totCost - Etc).ToString("F2");
            return;
        }
    }
}
