using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.Registration;
using System.Collections;
using FS.HISFC.Models.Account;
using FS.HISFC.Components.Registration;

namespace API.GZSI.UI
{
    public partial class ucBalanceQuery : FS.FrameWork.WinForms.Controls.ucBaseControl//System.Windows.Forms.UserControl//
    {

        /// <summary>
        /// 挂号管理类
        /// </summary>
        private FS.HISFC.BizLogic.Registration.Register regMgr = new FS.HISFC.BizLogic.Registration.Register();
        /// <summary>
        /// 费用
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Fee feeMgr = new FS.HISFC.BizProcess.Integrate.Fee();
        /// <summary>
        /// 帐户管理
        /// </summary>
        private FS.HISFC.BizLogic.Fee.Account accMgr = new FS.HISFC.BizLogic.Fee.Account();

        /// <summary>
        /// 患者入出转业务层
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.RADT radtIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();

        private Register reg = new Register();

        public ucBalanceQuery()
        {
            InitializeComponent();
        }

        ucBalanceInfomation print = null;//new ucInBalanceInfo();

        #region 事件

        private void ucInBalanceInfoPrintForClinic_Load(object sender, EventArgs e)
        {
            this.initEvents();
            print = new ucBalanceInfomation();
            this.neuPanel3.Controls.Clear();
            this.neuPanel3.Controls.Add(print);
        }

        #endregion

        #region 方法

        /// <summary>
        /// 回车事件
        /// </summary>
        public void initEvents()
        {
            this.txtCardNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCardNo_KeyDown);
        }

        /// <summary>
        /// 根据病历号检索患者挂号信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCardNo_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.rbOut.Checked)
                {
                    string cardNo = this.txtCardNo.Text.Trim();

                    if (string.IsNullOrEmpty(cardNo))
                    {
                        this.txtCardNo.SelectAll();
                        this.txtCardNo.Focus();
                        return;
                    }

                    cardNo = FS.FrameWork.Public.String.TakeOffSpecialChar(this.txtCardNo.Text.Trim(), "'", "[", "]");
                    if (string.IsNullOrEmpty(cardNo))
                    {
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("就诊卡号不能为空"), "提示");
                        this.txtCardNo.SelectAll();
                        this.txtCardNo.Focus();
                        return;
                    }

                    FS.HISFC.Models.Account.AccountCard accountObj = new FS.HISFC.Models.Account.AccountCard();
                    if (this.feeMgr.ValidMarkNO(cardNo, ref accountObj) == -1)
                    {
                        MessageBox.Show(feeMgr.Err);
                        this.txtCardNo.SelectAll();
                        this.txtCardNo.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(accountObj.Patient.PID.CardNO))
                    {
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("您输入的就诊卡号不存在"), "提示");
                        this.txtCardNo.SelectAll();
                        this.txtCardNo.Focus();
                        return;
                    }

                    DateTime permitDate = this.regMgr.GetDateTimeFromSysDateTime().AddDays(-365);
                    cardNo = accountObj.Patient.PID.CardNO;

                    //检索患者有效号
                    ArrayList tarlRegInfo = this.regMgr.Query(cardNo, permitDate);

                    if (tarlRegInfo == null || tarlRegInfo.Count == 0)
                    {
                        MessageBox.Show("未检索到相关患者挂号信息" + this.regMgr.Err, "提示");
                        return;
                    }

                    if (tarlRegInfo != null && tarlRegInfo.Count > 0)
                    {
                        //多条挂号记录，让收费员自己去选择
                        ucShowRegisterInfo ucShow = new ucShowRegisterInfo();
                        ucShow.SelectedRegister += new ucShowRegisterInfo.GetRegister(ucShow_SelectedRegister);
                        ucShow.SetRegisterInfo(tarlRegInfo);
                        Form fpShow = new Form();
                        fpShow.Width = 600;
                        fpShow.Height = 300;
                        fpShow.Controls.Add(ucShow);
                        fpShow.ShowDialog();
                    }
                }
            }
        }

        /// <summary>
        /// 挂号信息选择事件
        /// </summary>
        /// <param name="reg"></param>
        public void ucShow_SelectedRegister(FS.HISFC.Models.Registration.Register reg)
        {
            print = new ucBalanceInfomation();
            this.neuPanel3.Controls.Clear();
            this.neuPanel3.Controls.Add(print);
            print.Query(reg,"1");
        }

        public void ucQueryInpatientNo_myEvent()
        {
            if (this.ucQueryPatientInfo.InpatientNo == null || this.ucQueryPatientInfo.InpatientNo.Length == 0)
            {
                MessageBox.Show("住院患者不存在或者已出院");
                return;
            }

            FS.HISFC.Models.RADT.PatientInfo pInfo = this.radtIntegrate.GetPatientInfomation(this.ucQueryPatientInfo.InpatientNo);

            if (pInfo == null || pInfo.ID == null || pInfo.ID == string.Empty)
            {
                MessageBox.Show("该患者不存在!请验证后输入");
                return;
            }
            FS.HISFC.Models.Registration.Register reg = new Register();
            reg.ID = pInfo.ID;
            reg.PID = pInfo.PID;
            reg.Pact = pInfo.Pact;
            reg.Name = pInfo.Name;
            reg.PVisit = pInfo.PVisit;
            reg.Birthday = pInfo.Birthday;
            reg.IDCard = pInfo.IDCard;
            reg.IDCardType = pInfo.IDCardType;
            reg.Sex = pInfo.Sex;
            reg.CompanyName = pInfo.CompanyName;
            reg.PhoneHome = pInfo.PhoneHome;
            reg.AddressHome = pInfo.AddressHome;

            print = new ucBalanceInfomation();
            this.neuPanel3.Controls.Clear();
            this.neuPanel3.Controls.Add(print);
            print.Query(reg,"2");
        }

        #endregion

        private void neuButton1_Click(object sender, EventArgs e)
        {
            print.PrintInBalanceInfo(5,5);
        }

        private void rbOut_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbOut.Checked)
            {
                this.lblCardType.Text = "门诊号：";
                this.lblCardType.Visible = true;
                this.txtCardNo.Visible = true;
                this.ucQueryPatientInfo.Visible = false;
            }
            else if (this.rbIn.Checked)
            {
                this.lblCardType.Visible = false;
                this.txtCardNo.Visible = false;
                this.ucQueryPatientInfo.Visible = true;
            }
        }

        private void rbIn_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbOut.Checked)
            {
                this.lblCardType.Text = "门诊号：";
                this.lblCardType.Visible = true;
                this.txtCardNo.Visible = true;
                this.ucQueryPatientInfo.Visible = false;
            }
            else if (this.rbIn.Checked)
            {
                this.lblCardType.Visible = false;
                this.txtCardNo.Visible = false;
                this.ucQueryPatientInfo.Visible = true;
            }

        }
    }
}
