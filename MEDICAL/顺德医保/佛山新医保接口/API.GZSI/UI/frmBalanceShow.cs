using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace API.GZSI.UI
{
    public partial class frmBalanceShow : Form
    {

        #region 变量

        /// <summary>
        /// 当前挂号信息
        /// </summary>
        private FS.HISFC.Models.RADT.Patient patientInfo = new FS.HISFC.Models.RADT.Patient();
        
        /// <summary>
        /// 是否正式结算
        /// </summary>
        private bool isBalance = false;

        /// <summary>
        /// 窗体标题
        /// </summary>
        private string dialogTitle = string.Empty;

        #endregion

        #region 属性

        /// <summary>
        /// 挂号信息
        /// </summary>
        public FS.HISFC.Models.RADT.Patient PatientInfo
        {
            set
            {
                this.patientInfo = value;
            }
            get
            {
                return this.patientInfo;
            }
        }

        /// <summary>
        /// 是否正式结算
        /// </summary>
        public bool IsBalance
        {
            get
            {
                return isBalance;
            }
        }

        public string DialogTitle
        {
            get { return dialogTitle; }
            set { dialogTitle = value; }
        }

        #endregion

        public frmBalanceShow()
        {
            InitializeComponent();
            this.Load += new EventHandler(frmBalance_Load);
            this.btnOk.Click += new EventHandler(btnOk_Click);
            this.btnCancel.Click += new EventHandler(btnCancel_Click);
        }

        /// <summary>
        /// 加载窗体事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmBalance_Load(object sender, EventArgs e)
        {
            this.Text = this.dialogTitle;
            if (this.patientInfo is FS.HISFC.Models.Registration.Register)
            {
                FS.HISFC.Models.Registration.Register register = this.patientInfo as FS.HISFC.Models.Registration.Register;
                this.setBalanceInfo(register);
            }
            else if (this.patientInfo is FS.HISFC.Models.RADT.PatientInfo)
            {
                FS.HISFC.Models.RADT.PatientInfo patient = this.patientInfo as FS.HISFC.Models.RADT.PatientInfo;
                this.setBalanceInfo(patient);
            }
        }

        /// <summary>
        /// 确定事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// 取消事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// 设置门诊结算信息
        /// </summary>
        /// <param name="obj"></param>
        private void setBalanceInfo(FS.HISFC.Models.Registration.Register obj)
        {
            //统筹信息
            this.tbName.Text = obj.Name;
            this.tbIDCard.Text = obj.IDCard;
            this.tbMedfeeSumamt.Text = obj.SIMainInfo.Medfee_sumamt.ToString();  //医疗费总额
            this.tbActPayDedc.Text = obj.SIMainInfo.Act_pay_dedc.ToString(); //实际支付起付线
            this.tbInscpScpAmt.Text = obj.SIMainInfo.Inscp_scp_amt.ToString();
            this.tbFundPaySumat.Text = obj.SIMainInfo.Fund_pay_sumamt.ToString();
            this.tbPsnPartAm.Text = obj.SIMainInfo.Psn_part_am.ToString();
            this.tbHospPartAmt.Text = obj.SIMainInfo.Hosp_part_amt.ToString();
            //统筹基金明细
            this.tbHifpPay.Text = obj.SIMainInfo.Hifp_pay.ToString();
            this.tbCvlservPay.Text = obj.SIMainInfo.Cvlserv_pay.ToString();
            this.tbHifesPay.Text = obj.SIMainInfo.Hifes_pay.ToString();
            this.tbHifmiPay.Text = obj.SIMainInfo.Hifmi_pay.ToString();
            this.tbHifobPay.Text = obj.SIMainInfo.Hifob_pay.ToString();
            this.tbMafPay.Text = obj.SIMainInfo.Maf_pay.ToString();
            //自负明细
            this.tbFulamtOwnpayAmt.Text = obj.SIMainInfo.Ownpay_amt.ToString();
            this.tbOverlmtSelfpay.Text = obj.SIMainInfo.Overlmt_selfpay.ToString();
            this.tbPreselfpayAmt.Text = obj.SIMainInfo.Preselfpay_amt.ToString();
            this.tbAcctPay.Text = obj.SIMainInfo.Acct_pay.ToString();
            this.tbAcctMulaidPay.Text = obj.SIMainInfo.Acct_mulaid_pay.ToString();
            this.tbBalc.Text = obj.SIMainInfo.Balc.ToString();
            this.tbPsnCashPay.Text = obj.SIMainInfo.Cash_payamt.ToString();
            //备注
            this.tbMemo.Text = obj.SIMainInfo.Memo;
            
        }


        /// <summary>
        /// 设置主院结算信息
        /// </summary>
        private void setBalanceInfo(FS.HISFC.Models.RADT.PatientInfo obj)
        {
            //统筹信息
            this.tbName.Text = obj.Name;
            this.tbIDCard.Text = obj.IDCard;
            this.tbMedfeeSumamt.Text = obj.SIMainInfo.Medfee_sumamt.ToString();  //医疗费总额
            this.tbActPayDedc.Text = obj.SIMainInfo.Act_pay_dedc.ToString(); //实际支付起付线
            this.tbInscpScpAmt.Text = obj.SIMainInfo.Inscp_scp_amt.ToString();
            this.tbFundPaySumat.Text = obj.SIMainInfo.Fund_pay_sumamt.ToString();
            this.tbPsnPartAm.Text = obj.SIMainInfo.Psn_part_am.ToString();
            this.tbHospPartAmt.Text = obj.SIMainInfo.Hosp_part_amt.ToString();
            //统筹基金明细
            this.tbHifpPay.Text = obj.SIMainInfo.Hifp_pay.ToString();
            this.tbCvlservPay.Text = obj.SIMainInfo.Cvlserv_pay.ToString();
            this.tbHifesPay.Text = obj.SIMainInfo.Hifes_pay.ToString();
            this.tbHifmiPay.Text = obj.SIMainInfo.Hifmi_pay.ToString();
            this.tbHifobPay.Text = obj.SIMainInfo.Hifob_pay.ToString();
            this.tbMafPay.Text = obj.SIMainInfo.Maf_pay.ToString();
            //自负明细
            this.tbFulamtOwnpayAmt.Text = obj.SIMainInfo.Ownpay_amt.ToString();
            this.tbOverlmtSelfpay.Text = obj.SIMainInfo.Overlmt_selfpay.ToString();
            this.tbPreselfpayAmt.Text = obj.SIMainInfo.Preselfpay_amt.ToString();
            this.tbAcctPay.Text = obj.SIMainInfo.Acct_pay.ToString();
            this.tbAcctMulaidPay.Text = obj.SIMainInfo.Acct_mulaid_pay.ToString();
            this.tbBalc.Text = obj.SIMainInfo.Balc.ToString();
            //备注
            this.tbMemo.Text = obj.SIMainInfo.Memo;
        }

    }
}
