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
    public partial class frmPayModeForRefee : Form
    {

        /// <summary>
        /// 支付,押金信息
        /// </summary>
        private Controls.ucPayInfo ucpayInfo = null;


        ArrayList payModeInfo;
        /// <summary>
        /// 支付方式信息
        /// </summary>
        public ArrayList PayModeInfo
        {
            get { return payModeInfo; }
            set { payModeInfo = value; }
        }

        /// <summary>
        /// 存储优惠和支付类型的哈希表
        /// </summary>
        private Hashtable hsPayCost = new Hashtable();

        /// <summary>
        /// 存储优惠和支付类型的哈希表
        /// </summary>
        public Hashtable HsPayCost
        {
            get { return this.hsPayCost; }
            set
            {
                this.hsPayCost = value;
            }
        }

        /// <summary>
        /// 押金信息
        /// </summary>
        ArrayList depositInfo = new ArrayList();
        /// <summary>
        /// 押金信息
        /// </summary>
        public ArrayList DepositInfo
        {
            get { return depositInfo; }
            set { depositInfo = value; }
        }

        private decimal giftPay = 0.0m;
        /// <summary>
        /// 赠送支付金额限定
        /// </summary>
        public decimal GiftPay
        {
            get { return giftPay; }
            set { giftPay = value; }
        }

        /// <summary>
        /// 患者基本信息
        /// </summary>
        protected FS.HISFC.Models.RADT.PatientInfo patientInfo = null;

        /// <summary>
        /// 患者基本信息
        /// </summary>
        public FS.HISFC.Models.RADT.PatientInfo PatientInfo
        {
            get { return this.patientInfo; }
            set
            {
                this.patientInfo = value;
            }
        }

        private ArrayList packageList = new ArrayList();
        /// <summary>
        /// 套餐列表
        /// </summary>
        public ArrayList PackageList
        {
            get { return packageList; }
            set { packageList = value; }
        }

        public frmPayModeForRefee()
        {
            InitializeComponent();
            this.Load += new EventHandler(frmPayModeForRefee_Load);
        }

        private void frmPayModeForRefee_Load(object sender, EventArgs e)
        {
            this.init();
            this.bindEvent();
        }

        private void init()
        {
            ///支付信息
            if (this.ucpayInfo == null)
            {
                this.ucpayInfo = new Controls.ucPayInfo();
                this.ucpayInfo.Dock = DockStyle.Fill;
                this.plPayInfo.Height = ucpayInfo.Height;
                this.plPayInfo.Controls.Add(this.ucpayInfo);
            }

            this.ucpayInfo.InitInvoice(false);
            this.ucpayInfo.PatientInfo = this.PatientInfo;
            this.ucpayInfo.PackageLists = this.packageList;
            this.ucpayInfo.HsPayCost = this.hsPayCost;

            this.lbGift.Text = this.giftPay.ToString();
        }

        private void bindEvent()
        {
            this.btnOK.Click += new EventHandler(btnOK_Click);
            this.btnCancel.Click += new EventHandler(btnCancel_Click);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.hsPayCost = this.ucpayInfo.HsPayCost;
            this.depositInfo = this.ucpayInfo.GetDepositInfo();
            this.payModeInfo = this.ucpayInfo.GetPayModeInfo();

            decimal giftPayMode = decimal.Parse(hsPayCost["GIFT"].ToString()) + decimal.Parse(hsPayCost["COU"].ToString());

            if (this.giftPay != giftPayMode)
            {
                MessageBox.Show("赠送支付金额必须与原消耗金额一致,赠送金额 为 账户赠送金额与积分支付之和！");
                return;
            }

            if (this.payModeInfo == null)
            {
                MessageBox.Show("获取支付方式失败");
                return;
                
            }

            this.DialogResult = DialogResult.OK;
            this.Close();

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
