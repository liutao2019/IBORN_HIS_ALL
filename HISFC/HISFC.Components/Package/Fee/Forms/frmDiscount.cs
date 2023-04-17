using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HISFC.Components.Package.Fee.Forms
{
    public partial class frmDiscount : Form
    {
        /// <summary>
        /// 单据哈希表
        /// </summary>
        public Hashtable hsRecipe = new Hashtable();

        /// <summary>
        /// 单号列表
        /// </summary>
        public ArrayList RecipeNO = new ArrayList();

        /// <summary>
        /// 需要折扣的套餐
        /// </summary>
        public ArrayList packageList = new ArrayList();

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
            this.DialogResult = DialogResult.OK;
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
            this.DialogResult = DialogResult.OK;
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
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void SetPriceInRecipes(double incomes, double etc)
        {
            try
            {

                decimal StaticRealCost = decimal.Parse(incomes.ToString("F2"));
                decimal StaticEtcCost = decimal.Parse(etc.ToString("F2"));
                decimal RealCost = decimal.Parse(incomes.ToString("F2"));
                decimal EtcCost = decimal.Parse(etc.ToString("F2"));

                //FS.HISFC.Models.MedicalPackage.Fee.Package lastfee = null;

                //foreach (FS.HISFC.Models.MedicalPackage.Fee.Package package in packageList)
                //{
                //    double tmpReal = (Double.Parse(package.Package_Cost.ToString()) / this.totCost) * incomes;
                //    double TmpEtc = (Double.Parse(package.Package_Cost.ToString()) / this.totCost) * etc;
                //    package.Real_Cost = Decimal.Parse(tmpReal.ToString("F2"));
                //    package.Etc_cost = Decimal.Parse(TmpEtc.ToString("F2"));
                //    RealCost -= Double.Parse(package.Real_Cost.ToString("F2"));
                //    EtcCost -= Double.Parse(package.Etc_cost.ToString("F2"));
                //    lastfee = package;
                //}


                //if (lastfee != null)
                //{
                //    lastfee.Real_Cost += Decimal.Parse(RealCost.ToString("F2"));
                //    lastfee.Etc_cost += Decimal.Parse(EtcCost.ToString("F2"));
                //}

                for (int i = 0; i < packageList.Count; i++)
                {
                    FS.HISFC.Models.MedicalPackage.Fee.Package package = packageList[i] as FS.HISFC.Models.MedicalPackage.Fee.Package;
                    decimal tmpReal = 0.0m;
                    decimal tmpEtc = 0.0m;

                    if (i != packageList.Count - 1)
                    {
                        tmpReal = Math.Floor(StaticRealCost * package.Package_Cost * 100 / decimal.Parse(this.totCost.ToString("F2"))) / 100;
                        tmpEtc = Math.Floor(StaticEtcCost * package.Package_Cost * 100 / decimal.Parse(this.totCost.ToString("F2"))) / 100;
                    }
                    else
                    {
                        tmpReal = RealCost;
                        tmpEtc = EtcCost;
                    }
                    decimal diffs = 0.1M;
                    decimal tds = 0.00M;
                    if (tmpReal - RealCost == diffs)
                    {
                        tds = 0.1M;
                    }

                    if (RealCost + tds >= tmpReal)
                    {
                        RealCost -= tmpReal;
                    }
                    else
                    {
                        RealCost = 0;
                        tmpReal = RealCost;
                    }

                    if (EtcCost >= tmpEtc)
                    {
                        EtcCost -= tmpEtc;
                    }
                    else
                    {
                        tmpEtc = EtcCost;
                        EtcCost = 0;
                    }

                    if (package.Package_Cost > tmpReal + tmpEtc)
                    {
                        decimal diff = package.Package_Cost - tmpReal - tmpEtc;

                        if (RealCost + EtcCost >= diff)
                        {
                            if (RealCost >= diff)
                            {
                                tmpReal += diff;
                                RealCost -= diff;
                            }
                            else
                            {
                                tmpReal += RealCost;
                                RealCost = 0;
                                diff -= RealCost;

                                if (EtcCost >= diff)
                                {
                                    EtcCost -= diff;
                                    tmpEtc += diff;
                                }
                                else
                                {
                                    MessageBox.Show("折扣失败！分配费用失败！");

                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("折扣失败！分配费用失败！");
                        }
                    }

                    package.Real_Cost = tmpReal;
                    package.Etc_cost = tmpEtc;
                }
            }
            catch (Exception ex)
            {
            }

        }

        public bool Init()
        {
            try
            {
                //foreach (FS.FrameWork.Models.NeuObject obj in RecipeNO)
                //{
                //    ArrayList Packages = hsRecipe[obj.ID] as ArrayList;

                //    if (Packages == null || Packages.Count == 0)
                //    {
                //        continue;
                //    }

                //    this.lbRecipeNO.Text += obj.ID;
                //    this.lbRecipeNO.Text += ",";

                //    foreach (FS.HISFC.Models.MedicalPackage.Fee.Package package in Packages)
                //    {
                //        totCost += Double.Parse(package.Package_Cost.ToString());
                //    }
                //}

                //this.lbRecipeNO.Text = this.lbRecipeNO.Text.Substring(0, this.lbRecipeNO.Text.Length - 1);

                foreach (FS.HISFC.Models.MedicalPackage.Fee.Package package in packageList)
                {
                    totCost += Double.Parse(package.Package_Cost.ToString());
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
                this.tbRate1.Text = this.tbRate1.Text.Substring(0, this.tbRate1.Text.Length - 1);
                this.tbRate1.SelectAll();
                return;
            }

            this.tbIncome1.Text = (this.totCost * rate / 100).ToString("F2");
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
