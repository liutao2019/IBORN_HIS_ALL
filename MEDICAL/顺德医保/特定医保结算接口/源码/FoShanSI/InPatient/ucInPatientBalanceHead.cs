using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;


namespace FoShanSI.InPatient
{
    /// <summary>
    /// 门诊结算选择框
    /// 张琦
    /// 2010-7月
    /// </summary>
    public partial class ucInPatientBalanceHead : UserControl
    {
        public ucInPatientBalanceHead()
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

        ///// <summary>
        ///// 挂号实体
        ///// </summary>
        //FS.HISFC.Models.Registration.Register register = new FS.HISFC.Models.Registration.Register();
        FS.HISFC.Models.RADT.PatientInfo patientinfo = new FS.HISFC.Models.RADT.PatientInfo();
        /// <summary>
        /// 挂号实体
        /// </summary>
        public FS.HISFC.Models.RADT.PatientInfo Patientinfo
        {
            get { return patientinfo; }
            set 
            {
                patientinfo = value;
                if (patientinfo != null)
                {
                    this.ClearDisplay();
                    this.txtName.Text = patientinfo.Name;
                    this.txtIC.Text = patientinfo.IDCard;
                    this.txtRegNo.Text = patientinfo.PID.PatientNO + "-" + patientinfo.InTimes.ToString();
                    this.txtDiagnose.Text = this.GetDiagnose(patientinfo.ID);
                }
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
            return 1;
            DialogResult result = MessageBox.Show("关闭此窗口将取消已经上传得明细!是否确定?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (result == DialogResult.Yes)
            {
                
                try
                {
                    siDealBusiness.Open();
                    siDealBusiness.BeginTranscation();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return -1;
                }

                int balanceCount = 0;
                balanceCount = siDealBusiness.QueryIsBalance(patientinfo.PID.PatientNO, patientinfo.InTimes);
                if (balanceCount < 0)
                {
                    siDealBusiness.RollBack();
                    MessageBox.Show("判断医保结算状态失败！" + siDealBusiness.Err);
                    return -1;
                }
                else if (balanceCount > 0)//说明已经结算过，先取消结算
                {
                    siDealBusiness.RollBack();
                    MessageBox.Show("住院号:" + this.patientinfo.ID + "患者在医保终端已经结算，请先取消结算，才能删除明细重新结算！");
                    
                    return -1;
                }

                int iReturn = -1;

                iReturn = siDealBusiness.DeleteInPaientFeeItemDetail(patientinfo.PID.PatientNO, patientinfo.InTimes);

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
            //string balanceResult = string.Empty;
            string balanceResult = string.Empty;

            //balanceResult = siDealBusiness.GetOutPatientBalanceHead(register);
            balanceResult = siDealBusiness.GetInPatientBalanceHead1(patientinfo);

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

            this.txtName.Text = patientinfo.Name;
            this.txtIC.Text = patientinfo.IDCard;
            this.txtRegNo.Text = patientinfo.PID.PatientNO + "-" + patientinfo.InTimes.ToString();

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
            Function.LogManager.Write("gmz-" + displayResult);

            //设置ListView显示格式
            ListDetail.View = View.List;

            //计算自付部分累计金额
            decimal payAddUpCost = 0m;

            string[] dispalyResultStr = Function.Function.splitStr(displayResult, '|');
            decimal decTemp = 0;

            for (int cursor = 0; cursor < dispalyResultStr.Length; cursor++)
            {
                string columnName = Function.Function.GetColumnNameByIndex(Function.Function.HsInPatientFeeItemTable, cursor);
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
                    lblTotCost.Text = dispalyResultStr[cursor];
                }
                else if (!string.IsNullOrEmpty(columnName) && columnName.StartsWith("P"))
                {
                    decimal.TryParse(dispalyResultStr[cursor], out decTemp);
                    decimal temp = 0;
                    decimal.TryParse(lblPubCost.Text, out temp);

                    temp = decTemp + temp;

                    lblPubCost.Text = temp.ToString();
                }
                else if (!string.IsNullOrEmpty(columnName) && columnName.StartsWith("N"))
                {
                    //txtName.Text = dispalyResultStr[cursor];
                }
            }
            lblPayCost.Text = (FS.FrameWork.Function.NConvert.ToDecimal(lblTotCost.Text) -
                FS.FrameWork.Function.NConvert.ToDecimal(lblPubCost.Text)).ToString();
            if (payAddUpCost != FS.FrameWork.Function.NConvert.ToDecimal(lblPayCost.Text))
            {
                //MessageBox.Show("自付部分金额不等同于明细累加金额，请核对！");
            }

            patientinfo.SIMainInfo.TotCost = FS.FrameWork.Function.NConvert.ToDecimal(lblTotCost.Text);
            patientinfo.SIMainInfo.PubCost = FS.FrameWork.Function.NConvert.ToDecimal(lblPubCost.Text);
            patientinfo.SIMainInfo.OwnCost = FS.FrameWork.Function.NConvert.ToDecimal(lblPayCost.Text);
            patientinfo.SIMainInfo.Memo = displayResult;//保存医保返回的结算串信息

            lblTotCost.Text = patientinfo.SIMainInfo.TotCost.ToString();
            lblPubCost.Text = patientinfo.SIMainInfo.PubCost.ToString();
            lblPayCost.Text = patientinfo.SIMainInfo.OwnCost.ToString();
        }

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
            catch { }
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
