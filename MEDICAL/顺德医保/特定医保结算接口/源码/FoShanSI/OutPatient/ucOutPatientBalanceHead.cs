using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Function;


namespace FoShanSI.OutPatient
{
    /// <summary>
    /// 门诊结算选择框
    /// 张琦
    /// 2010-7月
    /// </summary>
    public partial class ucOutPatientBalanceHead : UserControl
    {
        public ucOutPatientBalanceHead()
        {
            InitializeComponent();
        }

        
        //public delegate void WhenClosingForm(ucOutPatientBalanceHead ucBalance);
        //public event WhenClosingForm closingFormDealEvent = null;


        #region 变量

        /// <summary>
        /// 是否正常结算
        /// </summary>
        bool isSuccessBalance = false;
        /// <summary>
        /// 是否正常结算
        /// </summary>
        public bool IsSuccessBalance
        {
            get { return isSuccessBalance; }
        }

        /// <summary>
        /// 医保业务层
        /// </summary>
        Management.SIBizProcess siDealBusiness = null;

        public Management.SIBizProcess SiDealBusiness
        {
            get { return siDealBusiness; }
            set { siDealBusiness = value; }
        }

        /// <summary>
        /// 挂号实体
        /// </summary>
        FS.HISFC.Models.Registration.Register register = new FS.HISFC.Models.Registration.Register();
        /// <summary>
        /// 挂号实体
        /// </summary>
        public FS.HISFC.Models.Registration.Register Register
        {
            get { return register; }
            set 
            { 
                register = value;
                if (register != null)
                {
                    this.ClearDisplay();
                    this.txtName.Text = register.Name;
                    this.txtIC.Text = register.IDCard;
                    this.txtRegNo.Text = register.SIMainInfo.RegNo;
                    this.txtDiagnose.Text = this.GetDiagnose(register.ID);
                }
            }
        }

        /// <summary>
        /// 挂号诊查费用(挂号) {6E8955EE-09C2-40b5-89B7-B31326EDD753} 佛山居民医保二次改造
        /// </summary>
        private List<FS.HISFC.Models.Account.AccountCardFee> accFeeList = null;

        /// <summary>
        /// 挂号诊查费用(挂号) {6E8955EE-09C2-40b5-89B7-B31326EDD753} 佛山居民医保二次改造
        /// </summary>
        public List<FS.HISFC.Models.Account.AccountCardFee> AccFeeList
        {
            get
            {
                return this.accFeeList;
            }
            set
            {
                this.accFeeList = value;
            }
        }

        #endregion

        /// <summary>
        /// 刷新新数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (RefreshBalance() == -1)
            {
                return;
            }
            //检索完项目光标自动跳转到确定按钮
            this.btnOk.Enabled = true;
            this.btnOk.Focus();
        }

        /// <summary>
        /// 确认结算
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click(object sender, EventArgs e)
        {
            isSuccessBalance = true;
            this.FindForm().Tag = "正常结算";
            this.FindForm().Close();
        }

        /// <summary>
        /// 取消结算
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            //if (this.CloseWindow() == -1)
            //{
            //    return;//关闭出现问题则返回
            //}
            //closingFormDealEvent(this);
            this.FindForm().Tag = "取消结算";
            this.FindForm().Close();
        }

        /// <summary>
        /// 非法关闭窗口
        /// </summary>
        /// <returns></returns>
        public int CloseWindow()
        {
            DialogResult result = MessageBox.Show("关闭此窗口将取消已经上传得明细!是否确定?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (result == DialogResult.Yes)
            {
                try
                {
                    siDealBusiness.BeginTranscation();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return -1;
                }

                int balanceCount = 0;
                balanceCount = siDealBusiness.GetOutPatientBalanceCount(register.ID);
                if (balanceCount < 0)
                {
                    MessageBox.Show("获取医保结算次数失败！" + siDealBusiness.Err);
                    siDealBusiness.RollBack();
                    return -1;
                }
                else if (balanceCount > 0)//说明已经结算过，先取消结算
                {
                    MessageBox.Show("门诊号:" + register.ID + "患者在医保终端已经结算，请先取消结算，才能删除明细重新结算！");
                    siDealBusiness.RollBack();
                    return -1;
                }

                int iReturn = -1;

                iReturn=siDealBusiness.DeleteOutPaientFeeItemDetail(register.SIMainInfo.RegNo);

                if (iReturn < 0)
                {
                    siDealBusiness.RollBack();
                    MessageBox.Show("删除明细失败!" + siDealBusiness.Err);
                    return -1;
                }
                siDealBusiness.Commit();
                return 0;
            }
            else
            {
                isSuccessBalance = false;
                return -1;
            }
        }

        /// <summary>
        /// 刷新结算信息.
        /// </summary>
        /// <returns>-1失败 0 成功</returns>
        public int RefreshBalance()
        {
            ///结算结果
            string balanceResult = string.Empty;

            balanceResult = siDealBusiness.GetOutPatientBalanceHead(register);

            if (balanceResult == Function.Function.errorText)
            {
                MessageBox.Show("获得医保结算信息出错!" + siDealBusiness.Err);
                return -1;
            }
            if (string.IsNullOrEmpty(balanceResult))
            {
                MessageBox.Show("患者还没有在佛山医保端结算,请稍候刷新!");
                return -1;
            }
            this.ClearDisplay();
            this.DispayBalance(balanceResult);

            this.btnOk.Enabled = true;
            this.btnCancel.Enabled = false;
            return 0;
        }

        /// <summary>
        /// 显示返回的结算结果
        /// </summary>
        private void DispayBalance(string displayResult)
        {
            //设置ListView显示格式
            ListDetail.View = View.List;

            //计算自付部分累计金额
            decimal payAddUpCost = 0m;

            string[] dispalyResultStr = Function.Function.splitStr(displayResult, '|');
            decimal decTemp = 0;

            for (int cursor = 0; cursor < dispalyResultStr.Length; cursor++)
            {
                string columnName = Function.Function.GetColumnNameByIndex(Function.Function.HsOutPatientFeeItemTable, cursor);
                //列名不为空，并且该列需要显示
                if (!string.IsNullOrEmpty(columnName) && columnName.StartsWith("%"))
                {
                    ListViewItem itemView = new ListViewItem();
                    itemView.Name = columnName.Substring(1, columnName.Length - 1);

                    decimal.TryParse(dispalyResultStr[cursor], out decTemp);
                    payAddUpCost += decTemp;

                    itemView.Text = itemView.Name + ":" + dispalyResultStr[cursor];
                    ListDetail.Items.Add(itemView);
                }
                else if (!string.IsNullOrEmpty(columnName) && columnName.StartsWith("T"))
                {
                    //总金额
                    lblTotCost.Text = dispalyResultStr[cursor];

                }
                else if (!string.IsNullOrEmpty(columnName) && columnName.StartsWith("P"))
                {
                    //统筹支付金额
                    lblPubCost.Text = (NConvert.ToDecimal(dispalyResultStr[cursor]) + NConvert.ToDecimal(lblPubCost.Text)).ToString();

                }
                else if (!string.IsNullOrEmpty(columnName) && columnName.StartsWith("N"))
                {
                    //txtName.Text = dispalyResultStr[cursor];
                }
            }

            lblPayCost.Text = (FS.FrameWork.Function.NConvert.ToDecimal(lblTotCost.Text.Trim()) -
                FS.FrameWork.Function.NConvert.ToDecimal(lblPubCost.Text.Trim())).ToString();

            if (payAddUpCost != FS.FrameWork.Function.NConvert.ToDecimal(lblPayCost.Text.Trim()))
            {
                //MessageBox.Show("自付部分金额不等同于明细累加金额，请核对！");
            }


            decimal decTotCost = FS.FrameWork.Function.NConvert.ToDecimal(lblTotCost.Text.Trim());   //总金额
            decimal decPubCost = FS.FrameWork.Function.NConvert.ToDecimal(lblPubCost.Text.Trim());   //统筹金额
            decimal decOwnCost = FS.FrameWork.Function.NConvert.ToDecimal(lblPayCost.Text.Trim());   //自费金额

            #region 挂号诊查费除外

            // {6E8955EE-09C2-40b5-89B7-B31326EDD753} 佛山居民医保二次改造

            if (this.accFeeList != null && this.accFeeList.Count > 0)
            {
                foreach (FS.HISFC.Models.Account.AccountCardFee cardFee in this.accFeeList)
                {
                    decTotCost -= (cardFee.Own_cost + cardFee.Pub_cost + cardFee.Pay_cost);
                    decPubCost -= (cardFee.Pub_cost);
                    decOwnCost -= (cardFee.Own_cost);
                }
            }

            #endregion

            lblTotCost.Text = decTotCost.ToString("F2");
            lblPubCost.Text = decPubCost.ToString("F2");
            lblPayCost.Text = decOwnCost.ToString("F2");


            register.SIMainInfo.TotCost = FS.FrameWork.Function.NConvert.ToDecimal(lblTotCost.Text.Trim());
            register.SIMainInfo.PubCost = FS.FrameWork.Function.NConvert.ToDecimal(lblPubCost.Text.Trim());
            register.SIMainInfo.OwnCost = FS.FrameWork.Function.NConvert.ToDecimal(lblPayCost.Text.Trim());
            register.SIMainInfo.Memo = displayResult;//保存医保返回的结算串信息
        }

        /// <summary>
        /// 清空显示
        /// </summary>
        private void ClearDisplay()
        {
            try
            {
                this.txtName.Text = "";
                this.txtIC.Text = "";
                this.txtRegNo.Text = "";
                this.lblPayCost.Text = "";
                this.lblPubCost.Text = "";
                this.lblTotCost.Text = "";
                this.txtDiagnose.Text = string.Empty;
                ListDetail.Items.Clear();
            }
            catch
            {
            }
        }

        /// <summary>
        /// 显示诊断信息
        /// </summary>
        private string GetDiagnose(string clinicNO)
        {
            FS.HISFC.BizLogic.HealthRecord.Diagnose diagnoseMgr = new FS.HISFC.BizLogic.HealthRecord.Diagnose();
            ArrayList al = diagnoseMgr.QueryMainDiagnose(clinicNO, true, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC);
            if (al == null)
            {
                return "";
            }
            if (al.Count == 0)
            {
                al = diagnoseMgr.QueryCaseDiagnoseForClinicByState(clinicNO, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC, true);
                if (al == null)
                {
                    return "";
                }
                if (al.Count == 0)
                {
                    return "";
                }
                else
                {
                    FS.HISFC.Models.HealthRecord.Diagnose diagnose = al[0] as FS.HISFC.Models.HealthRecord.Diagnose;
                    return diagnose.DiagInfo.ICD10.Name;
                }
            }
            else
            {
                FS.HISFC.Models.HealthRecord.Diagnose diagnose = al[0] as FS.HISFC.Models.HealthRecord.Diagnose;
                return diagnose.DiagInfo.ICD10.Name;
            }
        }
    }
}
