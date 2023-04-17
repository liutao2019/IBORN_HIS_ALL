using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Order.OutPatient.Forms
{
    public partial class frmInputInjectNum : FS.FrameWork.WinForms.Forms.BaseForm
    {
        public frmInputInjectNum()
        {
            InitializeComponent();
            //this.txtInJectNum.ReadOnly = true;
        }

        #region 变量
        private bool isHaveSetted = false;
        private bool isSpring = false;
        /// <summary>
        /// 设置需要更改的医嘱
        /// </summary>
        protected FS.HISFC.Models.Order.OutPatient.Order order = null;
        #endregion

        #region 属性
        /// <summary>
        /// 获得或设置院注次数
        /// </summary>
        public int InjectNum
        {
            get
            {
                return (int)this.txtInJectNum.Value;
            }
            set
            {
                if ((decimal)value >= 100)
                {
                    MessageBox.Show("院注次数过大，请重新输入！");
                }
                else
                {
                    this.txtInJectNum.Value = (decimal)value;
                }
            }
        }


        /// <summary>
        /// 当前医嘱
        /// </summary>
        public FS.HISFC.Models.Order.OutPatient.Order Order
        {
            get
            {
                return this.order;
            }
            set
            {
                order = value;
            }
        }
        #endregion

        /// <summary>
        /// load
        /// </summary>xz
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmInputInjectNum_Load(object sender, System.EventArgs e)
        {
            if (this.order.ExtendFlag1 == null)
            {
                this.txtTimes.Text = "1";//默认
            }
            else
            {
                this.txtTimes.TextChanged -= new EventHandler(txtTimes_TextChanged);
                this.txtTimes.Text = "1";
                this.txtTimes.TextChanged += new EventHandler(txtTimes_TextChanged);

                if (this.order.ExtendFlag1.Length >= 1)
                {
                    decimal times = 1;

                    try
                    {
                        times = System.Convert.ToDecimal(this.order.ExtendFlag1.Substring(0, 1));
                        this.isHaveSetted = true;
                    }
                    catch
                    {
                        this.isHaveSetted = false;
                    }

                    this.txtTimes.Text = times.ToString();
                }
            }
            this.txtInJectNum.Focus();
            this.txtInJectNum.Select(0, this.txtInJectNum.Text.ToString().Length);
            this.lblDoseOnce.Text = "每次 " + order.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + order.DoseUnit;
            //{27DBE032-6896-4b8f-9CBC-EDC47F499B50} 开立医嘱时显示院注天数
            this.SetInjectDays();
        }

        /// <summary>
        /// {27DBE032-6896-4b8f-9CBC-EDC47F499B50} 开立医嘱时显示院注天数
        /// </summary>
        private void SetInjectDays()
        {
            //开立数量
            decimal qty = this.order.Item.Qty;
            //院注次数
            decimal injectTimes = this.txtInJectNum.Value;
            //基本剂量
            decimal baseDose = ((FS.HISFC.Models.Pharmacy.Item)this.order.Item).BaseDose;
            //每次剂量
            decimal doseOnce = this.order.DoseOnce;
            //每日频次
            int frequencyDayCount = (this.order.Frequency.Time.Split('-')).Length;
            //计算得出院注天数
            int injectDays = (int)Math.Ceiling(injectTimes / frequencyDayCount);
            //最多院注天数
            if (doseOnce != 0)
            {
                int maxDays = (int)((qty * baseDose) / (doseOnce * frequencyDayCount));
            }

            this.lblInjectDays.Text = "院注天数：" + injectDays + "天";
        }

        /// <summary>
        /// 保存
        /// </summary>
        private void Save()
        {
            try
            {
                if (this.InjectNum < 0)
                {
                    MessageBox.Show("院注必须大等于零！");
                    this.txtInJectNum.Select(0, this.txtInJectNum.Text.Length);
                    this.txtInJectNum.Focus();
                    return;
                }
                if (this.InjectNum > 98)
                {
                    MessageBox.Show("院注次数过大！");
                    this.txtInJectNum.Select(0, this.txtInJectNum.Text.Length);
                    this.txtInJectNum.Focus();
                    return;
                }
                order.InjectCount = this.InjectNum;
                order.NurseStation.User02 = "C";//修改过
                if (this.isHaveSetted)
                {
                    order.ExtendFlag1 = order.ExtendFlag1.Remove(0, 1);
                    order.ExtendFlag1 = this.txtTimes.Text + order.ExtendFlag1;
                }
                else
                {
                    if (this.txtTimes.Text.Trim() == "")
                    {
                        MessageBox.Show("注射瓶数不能为空!");
                        this.txtTimes.Focus();
                        return;
                    }
                    int price = 0;
                    try
                    {
                        price = FS.FrameWork.Function.NConvert.ToInt32(this.txtTimes.Text.Trim());
                    }
                    catch
                    {
                        MessageBox.Show("输入注射瓶数的格式不正确，请重新输入！");
                        this.txtTimes.Focus();
                        return;
                    }
                    if (price <= 0)
                    {
                        MessageBox.Show("输入的注射瓶数不能小于或等于0！");
                        this.txtTimes.Focus();
                        return;
                    }
                    if (price > 9)
                    {
                        MessageBox.Show("输入的注射瓶数不能大于9！");
                        this.txtTimes.Focus();
                        return;
                    }
                    this.order.ExtendFlag1 = this.txtTimes.Text.Trim() + this.order.ExtendFlag1;
                }
            }
            catch
            {
                MessageBox.Show("添加院注次数出错！");
                this.Close();
            }
            this.Close();
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, System.EventArgs e)
        {
            this.Save();
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                this.Save();   
            }
            else if (keyData == Keys.Escape)
            {
                this.Close();
            }
            //return true;
            return base.ProcessDialogKey(keyData);
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuNumericUpDown1_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.isSpring == false)
                {
                    this.txtInJectNum.Focus();
                    this.isSpring = true;
                }
                else
                {
                    this.btnOK_Click(null, null);
                }
            }
        }

        /// <summary>
        /// 输入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtTimes_TextChanged(object sender, System.EventArgs e)
        {
            decimal times = 1;

            if (this.txtTimes.Text.Trim() == "")
            {
                return;
            }

            try
            {
                times = System.Convert.ToDecimal(this.txtTimes.Text.Trim());
            }
            catch
            {
                MessageBox.Show("您输入的的数字格式不正确，请重新输入！");
                this.txtTimes.Focus();
                return;
            }
            if (times > 9)
            {
                MessageBox.Show("您输入的的数字过大，请重新输入！");
                this.txtTimes.Focus();
                return;
            }
        }

        /// <summary>
        /// {27DBE032-6896-4b8f-9CBC-EDC47F499B50} 开立医嘱时显示院注天数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuNumericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            this.SetInjectDays();
        }

        private void txtInJectNum_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.btnOK.Focus();
            }
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

