using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace FS.HISFC.Components.Account.Forms
{
    public partial class frmCouponRule : Form
    {
        public frmCouponRule()
        {

            InitializeComponent();
            this.txtMoney.Text = "";
            this.txtCoupon.Text = "";
            string ratio = controlParam.GetControlParam<string>("JFGZ01");
            lblCouponRatio.Text = "积分比：" + ratio;
        }
        private int returnValue = 0;


        /// <summary>
        /// 控制参数业务层
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParam = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
        
        /// <summary>
        /// 
        /// </summary>
        private FS.HISFC.BizLogic.Fee.Account accountMgr = new FS.HISFC.BizLogic.Fee.Account();
        /// <summary>
        /// 返回
        /// </summary>
        public int ReturnValue
        {
            set { returnValue = value; }
            get { return returnValue; }
        }
        
        private void button1_Click(object sender, EventArgs e)
        {

            decimal money = 0m;
            decimal coupon = 0m;
            decimal couponRatio = 0m;
            if (!Regex.IsMatch(this.txtMoney.Text.Trim(), @"^[+-]?\d*[.]?\d*$") || string.IsNullOrEmpty(this.txtMoney.Text.Trim()))
            {
                MessageBox.Show("请输入正确的数字！");
                this.txtMoney.Focus();
                this.txtMoney.SelectAll();
                returnValue = -1;
                return;
            }

            if (!Regex.IsMatch(this.txtCoupon.Text.Trim(), @"^[+-]?\d*[.]?\d*$") || string.IsNullOrEmpty(this.txtCoupon.Text.Trim()))
            {
                MessageBox.Show("请输入正确的数字！");
                this.txtCoupon.Focus();
                this.txtCoupon.SelectAll();
                returnValue = -1;
                return;
            }
            money = decimal.Parse(this.txtMoney.Text.Trim());
            coupon = decimal.Parse(this.txtCoupon.Text.Trim());
            string error = "";

            couponRatio = coupon / money;

            string sql = @"update com_controlargument
                            set control_value = '{0}'
                            where control_code = 'JFGZ01'";
            sql = string.Format(sql, couponRatio);

            if (this.accountMgr.ExecNoQuery(sql) < 0)
            {
                MessageBox.Show("更新积分规则失败！");
                returnValue = - 1;
                return;
            }


            returnValue = 1;
        }

    }
}
