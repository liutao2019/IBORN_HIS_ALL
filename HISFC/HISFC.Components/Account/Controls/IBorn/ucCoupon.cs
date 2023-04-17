using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data; 
using System.Windows.Forms;
using System.Collections;
using FS.HISFC.Models.Account;
using FS.HISFC.BizProcess.Interface.Account;
using FS.HISFC.Components.Account.Forms;
using FS.HISFC.BizProcess.Interface.Fee;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Components.Account.Controls.IBorn
{
    /// <summary>
    /// 卡卷管理
    /// </summary>
    public partial class ucCoupon : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        /// <summary>
        /// 
        /// </summary>
        public ucCoupon()
        {
            InitializeComponent();
        }

        #region 变量

        /// <summary>
        /// 账户业务层
        /// </summary>
        private FS.HISFC.BizLogic.Fee.Account accountManager = new FS.HISFC.BizLogic.Fee.Account();

        /// <summary>
        /// Manager业务层
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();
       
        /// <summary>
        /// 常数管理类
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager conMgr = new FS.HISFC.BizProcess.Integrate.Manager();
        /// <summary>
        /// 管理类
        /// </summary>
        private FS.HISFC.BizLogic.RADT.InPatient inPatientMgr = new FS.HISFC.BizLogic.RADT.InPatient();
        
        /// <summary>
        /// 工具栏
        /// </summary>
        private FS.FrameWork.WinForms.Forms.ToolBarService toolbarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        /// <summary>
        /// 患者信息
        /// </summary>
        private FS.HISFC.Models.RADT.PatientInfo patientInfo = new FS.HISFC.Models.RADT.PatientInfo();

        /// <summary>
        /// 门诊卡号
        /// </summary>
        HISFC.Models.Account.AccountCard accountCard = null;
        /// <summary>
        /// 费用管理类
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Fee feeManage = new FS.HISFC.BizProcess.Integrate.Fee();
        
        /// <summary>
        /// 所有收费员
        /// </summary>
        private ArrayList allOper = new ArrayList();
        /// <summary>
        /// 所有兑换物品
        /// </summary>
        private ArrayList allGoods = new ArrayList();
        /// <summary>
        /// 积分方式
        /// </summary>
        private ArrayList couponTypeList = new ArrayList();

        /// <summary>
        /// 积分记录
        /// </summary>
        private ArrayList alCardCouponRecord = new ArrayList();
        #endregion
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            if (this.Init() < 0)
            {
                MessageBox.Show("初始化失败！");
                return;
            }
            this.ckSelect.CheckedChanged += new EventHandler(ckSelect_CheckedChanged);
            base.OnLoad(e);
        }
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolbarService.AddToolButton("兑换", "兑换", FS.FrameWork.WinForms.Classes.EnumImageList.B保存, true, false, null);
            toolbarService.AddToolButton("查询记录", "查询", FS.FrameWork.WinForms.Classes.EnumImageList.C查询, true, false, null);
            toolbarService.AddToolButton("清屏", "清屏", FS.FrameWork.WinForms.Classes.EnumImageList.Q清空, true, false, null);
            toolbarService.AddToolButton("积分规则", "积分规则", FS.FrameWork.WinForms.Classes.EnumImageList.M明细, true, false, null);
            toolbarService.AddToolButton("刷卡", "刷卡", FS.FrameWork.WinForms.Classes.EnumImageList.B报警, true, false, null);
           
            return toolbarService;
        }
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "兑换":
                    {
                        Save();
                        break;
                    }
                case "查询记录":
                    {
                        Query();
                        break;
                    }
                case "清屏":
                    {
                        this.Clear(true);
                        break;
                    }

                case "积分规则":
                    {
                        // {9EE79BEB-608C-4bc1-991E-7F5E197A326C}
                        Forms.frmCouponRule frmCouponRule = new frmCouponRule();
                        frmCouponRule.ShowDialog();
                        if (frmCouponRule.ReturnValue == 1)
                        {
                            MessageBox.Show("更新成功！");
                            frmCouponRule.Dispose();
                        }
                        break;
                    }

                case "刷卡":
                    {
                        // {DC68D3DF-1CB9-4d58-93E0-4F85B58E1647}
                        this.txtCardNo.Focus();
                        string mCardNo = string.Empty;
                        string error = "";
                        if (Function.OperMCard(ref mCardNo, ref error) < 0)
                        {
                            MessageBox.Show("读卡失败，请确认是否正确放置诊疗卡！\n" + error);
                            return;
                        }
                        this.txtCardNo.Text = ";"+mCardNo;
                        this.QueryPatientInfo();
                        break;
                    }
                    base.ToolStrip_ItemClicked(sender, e);
            }
        }


        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                ExecCmdKey();
            }
            return base.ProcessDialogKey(keyData);
        }
        /// <summary>
        /// 回车处理
        /// </summary>
        protected virtual void ExecCmdKey()
        {
            if (this.txtCardNo.Focused)
            {
                this.Clear(false);
                this.QueryPatientInfo();
            }
        }
        private int Init()
        {
            this.allOper = this.conMgr.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.F);
            this.cmbOper.AddItems(allOper);
            allGoods = this.conMgr.GetConstantList("CouponGoods");
            this.cmbExchangeGoods.ClearItems();
            this.cmbExchangeGoods.AddItems(allGoods);
            this.Clear(true);
            this.dtBegTime.Value = FS.FrameWork.Function.NConvert.ToDateTime(this.dtBegTime.Value.AddDays(-30).ToLongDateString() + " 00:00:00");
            this.dtEndTime.Value = FS.FrameWork.Function.NConvert.ToDateTime(this.dtEndTime.Value.ToLongDateString() + " 23:59:59");
            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = "0";
            obj.Name = "退费";
            couponTypeList.Add(obj);
            obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = "1";
            obj.Name = "消费";
            couponTypeList.Add(obj);
            obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = "2";
            obj.Name = "兑换";
            couponTypeList.Add(obj);
            this.cmbCouponType.AddItems(couponTypeList);
            return 1;
        }
        /// <summary>
        /// 兑换
        /// </summary>
        public void Save()
        {
            if (this.Valid() <= 0)
            {
                return;
            }
            decimal coupon = FS.FrameWork.Function.NConvert.ToDecimal(this.txtCoupon.Text);
            if (this.accountManager.UpdateCoupon(patientInfo.PID.CardNO, coupon, "", this.cmbExchangeGoods.Text, this.txtMark.Text, CounponOperTypes.Exc) <= 0)
            {
                MessageBox.Show("兑换失败！" + accountManager.Err);
                return;
            }
            alCardCouponRecord = this.accountManager.QueryCardCouponRecord(patientInfo.PID.CardNO, "", "", this.dtBegTime.Value.ToString(), this.dtEndTime.Value.ToString(), "", "","");
            this.SetFP();

            FS.HISFC.Models.Account.CardCoupon cardCoupon = new CardCoupon();
            cardCoupon = this.accountManager.QueryCardCouponByCardNo(patientInfo.PID.CardNO);
            if (cardCoupon == null)
            {
                MessageBox.Show("获取患者积分账户信息失败！" + this.accountManager.Err);
                return;
            }
            this.lblCoupon.Text = cardCoupon.CouponVacancy.ToString("F2");
            this.lblCouponAccumulate.Text = cardCoupon.CouponAccumulate.ToString("F2");
            
            MessageBox.Show("兑换成功！");

        }
        /// <summary>
        /// 验证
        /// </summary>
        /// <returns></returns>
        private int Valid()
        {

            if (this.patientInfo == null)
            {
                MessageBox.Show("请录入需要兑换的患者信息！");
                return -1;
            }
            decimal coupon = FS.FrameWork.Function.NConvert.ToDecimal(this.txtCoupon.Text);
            if (coupon == 0)
            {
                if (MessageBox.Show("积分为0，是否继续？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                {
                    return -1;
                }

            }
            if (string.IsNullOrEmpty(this.cmbExchangeGoods.Text.Trim()))
            {
                MessageBox.Show("请录入需要兑换的物品！");
                return -1;
            }
            return 1;
        }
        /// <summary>
        /// 清屏
        /// </summary>
        private void Clear(bool isClearCardNo)
        {
            if (isClearCardNo)
            {
                this.txtCardNo.Text = string.Empty;
                patientInfo = new FS.HISFC.Models.RADT.PatientInfo();
            }
            if (string.IsNullOrEmpty(this.txtCardNo.Text))
            {
                patientInfo = new FS.HISFC.Models.RADT.PatientInfo();
            }
            this.txtInvoiceNo.Text = string.Empty;
            this.lblPatientInfo.Text = string.Empty;
            this.lblPhone.Text = string.Empty;
            this.lblAddress.Text = string.Empty;
            this.lblCoupon.Text = "0.00";
            this.lblCouponAccumulate.Text = "0.00";
            this.txtMark.Text = string.Empty;
            this.txtCoupon.Text = "0.00";
            this.cmbExchangeGoods.Tag = "";
            this.cmbExchangeGoods.Text = "";
            this.cmbOper.Text = "";
            this.cmbOper.Tag = "";
            this.neuSpread1_Sheet1.RowCount = 0;


        }
        private void Clear()
        {

            if (string.IsNullOrEmpty(this.txtCardNo.Text))
            {
                patientInfo = new FS.HISFC.Models.RADT.PatientInfo();
            }
            this.lblPatientInfo.Text = string.Empty;
            this.lblPhone.Text = string.Empty;
            this.lblAddress.Text = string.Empty;
            this.lblCoupon.Text = "0.00";
            this.lblCouponAccumulate.Text = "0.00";
            this.neuSpread1_Sheet1.RowCount = 0;
        }
        /// <summary>
        /// 查询卡卷信息
        /// </summary>
        private void Query()
        {
            this.Clear();
            if (this.dtBegTime.Value > this.dtEndTime.Value)
            {
                MessageBox.Show("开始时间不能大于结束时间");
                return;
            }
            string invoiceNo = this.txtInvoiceNo.Text.Trim();
            string operCode = this.cmbOper.Tag.ToString();
            string exchangeGoods = this.cmbExchangeGoods.Text.Trim();
            string memo = this.txtMark.Text.Trim();
            string cardNO = "";
            if (!string.IsNullOrEmpty(this.txtCardNo.Text.Trim()))
            {
                cardNO = this.txtCardNo.Text.Trim().PadLeft(10, '0');
                patientInfo = new FS.HISFC.Models.RADT.PatientInfo();
                patientInfo = managerIntegrate.QueryComPatientInfo(cardNO);
                if (patientInfo == null)
                {
                    MessageBox.Show("获取患者信息失败！" + this.managerIntegrate.Err);
                    return;
                }
                cardNO = patientInfo.PID.CardNO;
                this.lblPatientInfo.Text = patientInfo.Name + "  " + patientInfo.Sex.Name + "  " + inPatientMgr.GetAge(patientInfo.Birthday);
                this.lblPhone.Text = "电话：" + (string.IsNullOrEmpty(patientInfo.PhoneHome) ? patientInfo.Mobile : patientInfo.PhoneHome);
                this.lblAddress.Text = "住址：" + (string.IsNullOrEmpty(patientInfo.User01) ? patientInfo.AddressHome : patientInfo.User01);
                FS.HISFC.Models.Account.CardCoupon cardCoupon = new CardCoupon();
                cardCoupon = this.accountManager.QueryCardCouponByCardNo(patientInfo.PID.CardNO);
                if (cardCoupon == null)
                {
                    MessageBox.Show("获取患者积分账户信息失败！" + this.accountManager.Err);
                    return;
                }
                this.lblCoupon.Text = cardCoupon.CouponVacancy.ToString("F2");
                this.lblCouponAccumulate.Text = cardCoupon.CouponAccumulate.ToString("F2");
            }
            alCardCouponRecord = this.accountManager.QueryCardCouponRecord(cardNO, exchangeGoods, memo, this.dtBegTime.Value.ToString(), this.dtEndTime.Value.ToString(),operCode, invoiceNo,this.cmbCouponType.Tag.ToString());
            if (alCardCouponRecord == null)
            {
                MessageBox.Show("获取患者积分账户信息失败！" + this.accountManager.Err);
                return;
            }
            this.SetFP();

        }

        /// <summary>
        /// 根据患者卡号查询积分记录
        /// </summary>
        private void QueryPatientInfo()
        {
            if (string.IsNullOrEmpty(this.txtCardNo.Text))
            {
                MessageBox.Show("请输入卡号");
                return;
            }

            string medicalCardNo = this.txtCardNo.Text.Trim();
            accountCard = new FS.HISFC.Models.Account.AccountCard();
            int resultValue = feeManage.ValidMarkNO(medicalCardNo, ref accountCard);


            if (resultValue <= 0 || accountCard == null)
            {
                MessageBox.Show("没有查询到患者信息！" + feeManage.Err);
                this.txtCardNo.Focus();
                this.txtCardNo.SelectAll();
                return;
            }
            this.txtCardNo.Text = accountCard.Patient.PID.CardNO;
            string cardNO = this.txtCardNo.Text.Trim().PadLeft(10,'0');
            patientInfo = new FS.HISFC.Models.RADT.PatientInfo();
            patientInfo = managerIntegrate.QueryComPatientInfo(cardNO);
            if (patientInfo == null)
            {
                MessageBox.Show("获取患者信息失败！" + this.managerIntegrate.Err);
                return;
            }
            this.lblPatientInfo.Text = patientInfo.Name + "  " + patientInfo.Sex.Name + "  " + inPatientMgr.GetAge(patientInfo.Birthday);
            this.lblPhone.Text = "电话：" + (string.IsNullOrEmpty(patientInfo.PhoneHome) ? patientInfo.Mobile : patientInfo.PhoneHome);
            this.lblAddress.Text = "住址："+ (string.IsNullOrEmpty(patientInfo.User01) ? patientInfo.AddressHome : patientInfo.User01);
            FS.HISFC.Models.Account.CardCoupon cardCoupon = new CardCoupon();
            cardCoupon = this.accountManager.QueryCardCouponByCardNo(patientInfo.PID.CardNO);
            if (cardCoupon == null)
            {
                MessageBox.Show("获取患者积分账户信息失败！" + this.accountManager.Err);
                return;
            }
            this.lblCoupon.Text = cardCoupon.CouponVacancy.ToString("F2");
            this.lblCouponAccumulate.Text = cardCoupon.CouponAccumulate.ToString("F2");
            alCardCouponRecord = this.accountManager.QueryCardCouponRecord(patientInfo.PID.CardNO, "", "", this.dtBegTime.Value.ToString(), this.dtEndTime.Value.ToString(), "", "","");
            this.SetFP();

        }
        /// <summary>
        /// 设置表格
        /// </summary>
        private void SetFP()
        {
            if (alCardCouponRecord == null)
            {
                return;
            }
            this.neuSpread1_Sheet1.RowCount = 0;
            this.neuSpread1_Sheet1.RowCount = alCardCouponRecord.Count;
            int count = 0;
            foreach (CardCouponRecord cardCouponRecord in alCardCouponRecord)
            {
                this.neuSpread1_Sheet1.Cells[count, 0].Text = cardCouponRecord.CardNo;
                this.neuSpread1_Sheet1.Cells[count, 1].Text = cardCouponRecord.Name;
                this.neuSpread1_Sheet1.Cells[count, 2].Text = cardCouponRecord.Money.ToString("F2");
                this.neuSpread1_Sheet1.Cells[count, 3].Text = cardCouponRecord.Coupon.ToString("F2");
                this.neuSpread1_Sheet1.Cells[count, 4].Text = cardCouponRecord.CouponVacancy.ToString("F2");
                this.neuSpread1_Sheet1.Cells[count, 5].Text = cardCouponRecord.InvoiceNo;

                string operType = "";
                if (cardCouponRecord.OperType == "0")
                {
                    operType = "退费";
                }
                else if (cardCouponRecord.OperType == "1")
                {
                    operType = "消费";
                }
                else if (cardCouponRecord.OperType == "2")
                {
                    operType = "兑换";
                }
                this.neuSpread1_Sheet1.Cells[count, 6].Text = operType;
                this.neuSpread1_Sheet1.Cells[count, 7].Text = cardCouponRecord.ExchangeGoods;
                this.neuSpread1_Sheet1.Cells[count, 8].Text = cardCouponRecord.Memo;
                this.neuSpread1_Sheet1.Cells[count, 9].Text = cardCouponRecord.OperEnvironment.Name;
                this.neuSpread1_Sheet1.Cells[count, 10].Text = cardCouponRecord.OperEnvironment.OperTime.ToString();
                this.neuSpread1_Sheet1.Rows[count].Tag = cardCouponRecord;
                count++;
            }

        }

        private void ckSelect_CheckedChanged(object sender, EventArgs e)
        {
            this.gbQueryCon.Visible = this.ckSelect.Checked;

        }

        //private void cmbExchangeGoods_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //}

        private void cmbExchangeGoods_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.cmbExchangeGoods.Tag.ToString()))
            {
                foreach (FS.FrameWork.Models.NeuObject obj in allGoods)
                {
                    if (this.cmbExchangeGoods.Tag.ToString() == obj.ID)
                    {
                        this.txtCoupon.Text = obj.Memo;
                        break;
                    }
                }
            }
            else
            {
                this.txtCoupon.Text = "0.00";
            }

        }

        private void cmbExchangeGoods_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(this.cmbExchangeGoods.Tag.ToString()))
            {
                foreach (FS.FrameWork.Models.NeuObject obj in allGoods)
                {
                    if (this.cmbExchangeGoods.Tag.ToString() == obj.ID)
                    {
                        this.txtCoupon.Text = obj.Memo;
                        break;
                    }
                }
            }
            else
            {
                this.txtCoupon.Text = "0.00";
            }
        }



    }
}
