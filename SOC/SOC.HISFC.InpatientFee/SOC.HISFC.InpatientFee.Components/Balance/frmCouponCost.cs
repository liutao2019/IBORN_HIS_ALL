using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FarPoint.Win.Spread;

namespace FS.SOC.HISFC.InpatientFee.Components.Balance
{

    public partial class frmCouponCost : Form
    {
        #region 属性

        /// <summary>
        /// 费用管理
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Fee feeMgr = new FS.HISFC.BizProcess.Integrate.Fee();
        /// <summary>
        /// 当前患者
        /// </summary>
        private FS.HISFC.Models.RADT.PatientInfo patientInfo = new FS.HISFC.Models.RADT.PatientInfo();
        /// <summary>
        /// 控制参数管理
        /// </summary>
        FS.FrameWork.Management.ControlParam contrlManager = new FS.FrameWork.Management.ControlParam();
        /// <summary>
        /// 患者管理类
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.RADT radtIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();

        /// <summary>
        /// 当前患者
        /// </summary>
        public FS.HISFC.Models.RADT.PatientInfo PatientInfo
        {
            get { return this.patientInfo; }
            set
            {
                this.patientInfo = value;
                this.SetPatientInfo();
                this.RefreshCoupon();
            }
        }

        /// <summary>
        /// 能够分配的金额
        /// </summary>
        private decimal deliverableCost = 0.0m;

        /// <summary>
        /// 能够分配的金额
        /// </summary>
        public decimal DeliverableCost
        {
            set
            {
                deliverableCost = value;
                this.tbMoeny.Text = deliverableCost.ToString("F2");
            }
        }

        /// <summary>
        /// 设置支付方式
        /// </summary>
        public event DelegateHashtableSet SetPayModeRes;

        private bool isEmpower = false;

        /// <summary>
        /// 是否是代付
        /// </summary>
        public bool IsEmpower
        {
            get { return isEmpower; }
            set 
            { 
                isEmpower = value;
                this.tbMedcineNO.ReadOnly = !isEmpower;
                this.tbName.ReadOnly = (!isEmpower) && isFindForName;
                this.btnCard.Visible = isEmpower;
            }
        }

        /// <summary>
        /// 是否可以根据名字搜索
        /// </summary>
        private bool isFindForName = false;

        #endregion

        /// <summary>
        /// 账户支付分配界面
        /// </summary>
        public frmCouponCost()
        {
            InitializeComponent();
            isFindForName = this.contrlManager.QueryControlerInfo("DF0001") == "1";
            this.addEvents();
        }

        /// <summary>
        /// 添加事件
        /// </summary>
        private void addEvents()
        {
            this.btnOK.Click += new EventHandler(btnOK_Click);
            this.btnCancel.Click += new EventHandler(btnCancel_Click);
            this.tbMedcineNO.KeyDown += new KeyEventHandler(tbMedcineNO_KeyDown);
            this.btnCard.Click += new EventHandler(btnCard_Click);
            this.tbCostMoney.TextChanged += new EventHandler(tbCostMoney_TextChanged);
        }

        void tbMedcineNO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                string cardNO = string.Empty;
                string queryStr = this.tbMedcineNO.Text.Trim();
                if (string.IsNullOrEmpty(queryStr))
                {
                    MessageBox.Show("请输入卡号、病历号、预约号进行检索！");
                    return;
                }

                FS.HISFC.Models.Account.AccountCard accountCard = new FS.HISFC.Models.Account.AccountCard();
                int rev = this.feeMgr.ValidMarkNO(queryStr, ref accountCard);
                if (rev > 0)
                {

                    FS.HISFC.Models.RADT.PatientInfo patient = radtIntegrate.QueryComPatientInfo(accountCard.Patient.PID.CardNO);
                    if (patient == null || string.IsNullOrEmpty(patient.PID.CardNO))
                    {
                        MessageBox.Show("未找到患者信息！");
                        return;
                    }

                    if (this.IsEmpower)
                    {
                        if (string.IsNullOrEmpty(OriginalCardNO))
                        {
                            MessageBox.Show("被代付患者卡号获取失败！");
                            return;
                        }
                        else
                        {
                            if (OriginalCardNO == patient.PID.CardNO)
                            {
                                MessageBox.Show("不能自己给自己代付，请用其他账户！");
                                return;
                            }
                        }
                    }

                    this.PatientInfo = patient;
                }
                else
                {
                    MessageBox.Show("未找到患者信息！");
                    return;
                }
            }
        }

        /// <summary>
        /// 删除事件
        /// </summary>
        private void delEvents()
        {
            this.tbCostMoney.TextChanged -= new EventHandler(tbCostMoney_TextChanged);
            this.btnOK.Click -= new EventHandler(btnOK_Click);
            this.btnCancel.Click -= new EventHandler(btnCancel_Click);
            this.tbMedcineNO.KeyDown -= new KeyEventHandler(tbMedcineNO_KeyDown);
            this.btnCard.Click -= new EventHandler(btnCard_Click);
        }

        private string originalCardNO = string.Empty;

        /// <summary>
        /// {4E4E36FF-EFBB-42ea-90EB-13FADAA4623A}
        /// 代付时原患者卡号
        /// </summary>
        public string OriginalCardNO
        {
            get { return originalCardNO; }
            set
            {
                originalCardNO = value;
            }
        }

        /// <summary>
        /// {4E4E36FF-EFBB-42ea-90EB-13FADAA4623A}
        /// 刷卡
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCard_Click(object sender, EventArgs e)
        {
            //string cardNo = "";
            //string error = "";
            //if (FS.HISFC.Components.Registration.Function.OperMCard(ref cardNo, ref error) == -1)
            //{
            //    MessageBox.Show("读卡失败：" + error, "提示");
            //    return;
            //}
            //cardNo = ";" + cardNo;
            //this.tbMedcineNO.Text = cardNo;
            //this.tbMedcineNO_KeyDown(this.tbMedcineNO, new KeyEventArgs(Keys.Enter));
        }

        /// <summary>
        /// 记录修改前的值
        /// </summary>
        private decimal previousTot = 0.0m;
        private decimal previousAccount = 0.0m;
        private decimal previousGift = 0.0m;



        private void tbCostMoney_TextChanged(object sender, EventArgs e)
        {
            decimal totCost = 0.0m;
            decimal avaliableCost = 0.0m;
            totCost = decimal.Parse(this.tbCostMoney.Text);
            avaliableCost = decimal.Parse(this.lbCouponMoney.Text);

            if (totCost > avaliableCost)
            {
                MessageBox.Show("输入金额大于可抵用积分");
                this.tbCostMoney.Text = "0";
                this.tbCostMoney.SelectAll();
                return;
            }

            if (this.CountCostInfo() < 0)
            {
                MessageBox.Show("输入金额大于应缴纳金额");
                this.tbCostMoney.Text = "0";
                this.tbCostMoney.SelectAll();
            }
        }

        /// <summary>
        /// 计算已分配总金额　{dd227cc2-3277-40c4-6872-a60abfa4e957}
        /// </summary>
        /// <returns></returns>
        private decimal CountCostInfo()
        {
            decimal totCost = 0.0m;

            totCost = decimal.Parse(this.tbCostMoney.Text);

            if (this.deliverableCost >= totCost)
            {
                this.lbCost.Text = "￥" + totCost.ToString("F2");
            }

            return this.deliverableCost - totCost;
        }

        /// <summary>
        /// 设置患者信息
        /// </summary>
        private void SetPatientInfo()
        {
            if (this.patientInfo == null)
            {
                return;
            }

            this.tbMedcineNO.Text = this.patientInfo.PID.CardNO;
            this.tbName.Text = this.patientInfo.Name;
        }

        /// <summary>
        /// 设置积分信息
        /// </summary>
        private void RefreshCoupon()
        {
            decimal couponAmount = 0.0m;

            string resultCode = "0";
            string errorMsg = "";
            Dictionary<string, string> dic = FS.HISFC.BizProcess.Integrate.WSHelper.QueryAccountInfo(patientInfo.PID.CardNO, out resultCode, out errorMsg);

            if (dic == null)
            {
                MessageBox.Show("查询账户出错:" + errorMsg);
                return;
            }

            if (dic.ContainsKey("couponvacancy"))
            {
                couponAmount = decimal.Parse(dic["couponvacancy"].ToString());
            }
            else
            {
                MessageBox.Show("查询账户余额出错！" );
                return;
            }

            this.lbCouponAmount.Text = couponAmount.ToString("F2");
            this.lbCouponMoney.Text = (Math.Floor(couponAmount) / 100).ToString("F2");
        }

        /// <summary>
        /// 获取支付信息
        /// </summary>
        /// <returns></returns>
        public Hashtable GetPayModeInfo()
        {
            List<FS.HISFC.Models.Fee.Inpatient.BalancePay> COmodeList = new List<FS.HISFC.Models.Fee.Inpatient.BalancePay>();

            FS.HISFC.Models.Fee.Inpatient.BalancePay balancePay = new FS.HISFC.Models.Fee.Inpatient.BalancePay();

            balancePay.PayType.ID = "CO";
            balancePay.PayType.Name = "积分支付";

            balancePay.AccountNo = "";
            balancePay.AccountTypeCode = "";
            balancePay.IsEmpPay = this.isEmpower;

            balancePay.FT.TotCost = decimal.Parse((this.tbCostMoney.Text));
            balancePay.FT.RealCost = balancePay.FT.TotCost;

            COmodeList.Add(balancePay);

            Hashtable hsPayMode = new Hashtable();
            hsPayMode.Add("CO", COmodeList);

            return hsPayMode;
        }

        /// <summary>
        /// 确认按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.SetPayModeRes != null)
            {
                if (SetPayModeRes(this.GetPayModeInfo(),this.deliverableCost) < 0)
                {
                    MessageBox.Show("获取支付信息处错！");
                    return;
                }
            }

            this.Close();
        }

        /// <summary>
        /// 取消按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        private void tbName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                FS.HISFC.Components.Common.Forms.frmQueryPatientByConditions frmQuery = new FS.HISFC.Components.Common.Forms.frmQueryPatientByConditions();

                string QueryStr = this.tbName.Text;

                if (string.IsNullOrEmpty(QueryStr))
                {
                    return;
                }

                frmQuery.QueryByName(QueryStr);
                frmQuery.ShowDialog();

                if (frmQuery.DialogResult == DialogResult.OK)
                {

                    this.tbMedcineNO.Text = frmQuery.PatientInfo.PID.CardNO;
                    this.tbMedcineNO.Focus();
                    tbMedcineNO_KeyDown(new object(), new KeyEventArgs(Keys.Enter));
                }
            }
        }
    }
}
