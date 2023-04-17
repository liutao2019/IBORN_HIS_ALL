using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Registration.Forms
{
    /// <summary>
    /// 挂号找零窗口[新]：针对fin_opr_Register 和 fin_opb_accountcardfee挂号和费用分离
    /// </summary>
    public partial class frmReturnCostNew : Form
    {
        /// <summary>
        /// 挂号找零窗口[新]
        /// </summary>
        public frmReturnCostNew()
        {
            InitializeComponent();
        }

        #region 变量

        /// <summary>
        /// 挂号实体
        /// </summary>
        private FS.HISFC.Models.Registration.Register regObj;

        #endregion

        #region 属性

        /// <summary>
        /// 挂号实体
        /// </summary>
        public FS.HISFC.Models.Registration.Register RegObj
        {
            get
            {
                return regObj;
            }
            set
            {
                regObj = value;
                this.SetCost();
            }
        }

        #endregion

        #region 方法
        /// <summary>
        /// 校验数
        /// </summary>
        /// <returns></returns>
        protected virtual int ValidValue()
        {
            //应收金额
            decimal willCost = FS.FrameWork.Function.NConvert.ToDecimal(this.ntxtWillCost.Text);

            //实收金额
            decimal realCost = FS.FrameWork.Function.NConvert.ToDecimal(this.ntxtRealCost.Text);

            //找零金额
            decimal changeCost = FS.FrameWork.Function.NConvert.ToDecimal(this.ntxtChangeCost.Text);

            //实收金额小于应收金额
            if (realCost < willCost)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("实收金额不能小于应收金额"));
                return -1;

            }

            return 1;


        }

        /// <summary>
        /// 赋值
        /// </summary>
        private void SetCost()
        {
            if (!this.ntxtRealCost.Focused)
            {
                this.ntxtRealCost.Focus();
            }

            decimal totCost = 0m;
            foreach (FS.HISFC.Models.Account.AccountCardFee cardFee in this.regObj.LstCardFee)
            {
                totCost += cardFee.Tot_cost;
            }

            this.ntxtWillCost.Text = totCost.ToString("F2");


        }
        #endregion

        #region 事件
        private void btOK_Click(object sender, EventArgs e)
        {
            //校验数据
            int retureValue = this.ValidValue();

            if (retureValue < 0)
            {
                return;
            }

            this.DialogResult = DialogResult.OK;


            this.Close();
        }

        private void ntxtRealCost_TextChanged(object sender, EventArgs e)
        {
            try
            {

                decimal willCost = FS.FrameWork.Function.NConvert.ToDecimal(this.ntxtWillCost.Text);
                decimal realCost = FS.FrameWork.Function.NConvert.ToDecimal(this.ntxtRealCost.Text);
                this.ntxtChangeCost.Text = (realCost - willCost).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void ntxtRealCost_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.btOK_Click(sender, new EventArgs());
            }
        }
        #endregion

    }
}