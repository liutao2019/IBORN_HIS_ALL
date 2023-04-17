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

namespace API.GZSI.Print
{
    public partial class ucInBalanceInfoPrintForClinic : FS.FrameWork.WinForms.Controls.ucBaseControl
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

        /// <summary>
        /// 医保数据操作
        /// </summary>
        private LocalManager localMgr = new LocalManager();

        private Register reg = new Register();

        public ucInBalanceInfoPrintForClinic()
        {
            InitializeComponent();
        }

        ucInBalanceInfoForClinicNew print = null;//new ucInBalanceInfo();

        #region 事件

        private void ucInBalanceInfoPrintForClinic_Load(object sender, EventArgs e)
        {
            this.initEvents();
            print = new ucInBalanceInfoForClinicNew();
            //print.Dock = System.Windows.Forms.DockStyle.Fill;
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

                    List<FS.HISFC.Models.Base.Const> al = new List<FS.HISFC.Models.Base.Const>();

                    al = this.localMgr.QueryAllFeeReg(accountObj.Patient.PID.CardNO, "1");
                    if (al == null || al.Count <= 0)
                    {
                        MessageBox.Show("未找到【" + accountObj.Patient.PID.CardNO + "】有效的结算单号！");
                        return;
                    }
                    FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                    if (al != null && al.Count > 1)
                    {


                        if (FS.FrameWork.WinForms.Classes.Function.ChooseItem(new ArrayList(al), new string[] { "结算单号", "就诊登记号", "姓名", "就诊日期", "看诊医生" }, new bool[] { true, true, true, true, true, true, true }, new int[] { 100, 100, 100, 200, 100 }, ref obj) != 1)
                        {
                            return;
                        }
                        if (obj == null || string.IsNullOrEmpty(obj.ID))
                        {
                            MessageBox.Show("请选择一位患者！");
                            return;
                        }
                    }
                    else if (al != null && al.Count == 1)
                    {
                        obj = al[0] as FS.FrameWork.Models.NeuObject;
                    }
                    else
                    {
                        MessageBox.Show("未找到【" + accountObj.Patient.PID.CardNO + "】有效的结算单号！");
                        return;
                    }

                    #region // {1978DB39-3AC8-4bdb-A15C-FCD1F5379B49}
                    FS.HISFC.Models.Base.Const con = new FS.HISFC.Models.Base.Const();
                    if (al != null && al.Count == 1)
                    {
                        con = al[0] as FS.HISFC.Models.Base.Const;
                    }
                    else
                    {
                        foreach (FS.HISFC.Models.Base.Const c in al)
                        {
                            if (c.ID == obj.ID)
                            {
                                con = c;
                                break;
                            }
                        }
                    }

                    FS.HISFC.Models.RADT.PatientInfo patientInfo = new FS.HISFC.Models.RADT.PatientInfo();
                    patientInfo.SIMainInfo.Mdtrt_id = con.Name;//obj.Name;
                    patientInfo.SIMainInfo.Setl_id = con.ID;//obj.ID;
                    patientInfo.SIMainInfo.InvoiceNo = con.OperEnvironment.ID;
                    #endregion
                    print = new ucInBalanceInfoForClinicNew();
                    this.neuPanel3.Controls.Clear();
                    this.neuPanel3.Controls.Add(print);
                    print.Query(patientInfo, true);
                    //DateTime permitDate = this.regMgr.GetDateTimeFromSysDateTime().AddDays(-7);
                    //cardNo = accountObj.Patient.PID.CardNO;

                    ////检索患者有效号
                    //ArrayList tarlRegInfo = this.regMgr.Query(cardNo, permitDate);

                    //if (tarlRegInfo == null || tarlRegInfo.Count == 0)
                    //{
                    //    MessageBox.Show("未检索到相关患者挂号信息" + this.regMgr.Err, "提示");
                    //    return;
                    //}

                    //if (tarlRegInfo != null && tarlRegInfo.Count > 0)
                    //{

                    //    //只找到一条挂号记录
                    //    //if (tarlRegInfo.Count == 1)
                    //    //{
                    //    //    //reg = tarlRegInfo as FS.HISFC.Models.Registration.Register;
                    //    //}
                    //    //else
                    //    //{
                    //    //多条挂号记录，让收费员自己去选择
                    //    ucShowRegisterInfo ucShow = new ucShowRegisterInfo();
                    //    ucShow.SelectedRegister += new ucShowRegisterInfo.GetRegister(ucShow_SelectedRegister);
                    //    ucShow.SetRegisterInfo(tarlRegInfo);
                    //    Form fpShow = new Form();
                    //    fpShow.Width = 600;
                    //    fpShow.Height = 300;
                    //    fpShow.Controls.Add(ucShow);
                    //    fpShow.ShowDialog();
                    //    //}
                    //}
                }
                else if (this.rbIn.Checked)
                {
                    if (string.IsNullOrEmpty(this.txtCardNo.Text))
                    {
                        MessageBox.Show("请输入住院号！");
                        return;
                    }
                    string patientNo = this.txtCardNo.Text.Trim().PadLeft(10, '0');

                    List<FS.HISFC.Models.Base.Const> al = new List<FS.HISFC.Models.Base.Const>();
                    al = this.localMgr.QueryAllFeeReg(patientNo, "2");
                    if (al == null || al.Count <= 0)
                    {
                        MessageBox.Show("未找到【" + patientNo + "】有效的结算单号！");
                        return;
                    }
                    FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                    if (al != null && al.Count > 1)
                    {


                        if (FS.FrameWork.WinForms.Classes.Function.ChooseItem(new ArrayList(al), new string[] { "结算单号", "就诊登记号", "姓名", "入院日期", "出院日期" }, new bool[] { true, true, true, true, true, true, true }, new int[] { 100, 100, 100, 200, 200 }, ref obj) != 1)
                        {
                            return;
                        }
                        if (obj == null || string.IsNullOrEmpty(obj.ID))
                        {
                            MessageBox.Show("请选择一位患者！");
                            return;
                        }
                    }
                    else if (al != null && al.Count == 1)
                    {
                        obj = al[0] as FS.FrameWork.Models.NeuObject;
                    }
                    else
                    {
                        MessageBox.Show("未找到【" + patientNo + "】有效的结算单号！");
                        return;
                    }

                    #region // {1978DB39-3AC8-4bdb-A15C-FCD1F5379B49}
                    FS.HISFC.Models.Base.Const con = new FS.HISFC.Models.Base.Const();
                    if (al != null && al.Count == 1)
                    {
                        con = al[0] as FS.HISFC.Models.Base.Const;
                    }
                    else
                    {
                        foreach (FS.HISFC.Models.Base.Const c in al)
                        {
                            if (c.ID == obj.ID)
                            {
                                con = c;
                                break;
                            }
                        }
                    }

                    FS.HISFC.Models.RADT.PatientInfo patientInfo = new FS.HISFC.Models.RADT.PatientInfo();
                    patientInfo.SIMainInfo.Mdtrt_id = con.Name;//obj.Name;
                    patientInfo.SIMainInfo.Setl_id = con.ID;//obj.ID;
                    patientInfo.SIMainInfo.InvoiceNo = con.OperEnvironment.ID;
                    #endregion
                    print = new ucInBalanceInfoForClinicNew();
                    this.neuPanel3.Controls.Clear();
                    this.neuPanel3.Controls.Add(print);
                    print.Query(patientInfo, false);
                }
                else if (this.rbMedNo.Checked)
                {
                    if (string.IsNullOrEmpty(this.txtCardNo.Text))
                    {
                        MessageBox.Show("请输入就诊登记号！");
                        return;
                    }

                    List<FS.HISFC.Models.Base.Const> al = new List<FS.HISFC.Models.Base.Const>();

                    al = this.localMgr.QueryAllFeeReg(this.txtCardNo.Text, "3");
                    if (al == null || al.Count <= 0)
                    {
                        MessageBox.Show("未找到【" + this.txtCardNo.Text + "】有效的结算单号！");
                        return;
                    }
                    FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                    if (al != null && al.Count > 1)
                    {

                        if (FS.FrameWork.WinForms.Classes.Function.ChooseItem(new ArrayList(al), new string[] { "结算单号", "就诊登记号", "姓名", "结算日期", "备注" }, new bool[] { true, true, true, true, true, true, true }, new int[] { 100, 100, 100, 200, 100 }, ref obj) != 1)
                        {
                            return;
                        }
                        if (obj == null || string.IsNullOrEmpty(obj.ID))
                        {
                            MessageBox.Show("请选择一位患者！");
                            return;
                        }
                    }
                    else if (al != null && al.Count == 1)
                    {
                        obj = al[0] as FS.FrameWork.Models.NeuObject;
                    }
                    else
                    {
                        MessageBox.Show("未找到【" + this.txtCardNo.Text + "】有效的结算单号！");
                        return;
                    }

                    #region // {1978DB39-3AC8-4bdb-A15C-FCD1F5379B49}
                    FS.HISFC.Models.Base.Const con = new FS.HISFC.Models.Base.Const();
                    if (al != null && al.Count == 1)
                    {
                        con = al[0] as FS.HISFC.Models.Base.Const;
                    }
                    else
                    {
                        foreach (FS.HISFC.Models.Base.Const c in al)
                        {
                            if (c.ID == obj.ID)
                            {
                                con = c;
                                break;
                            }
                        }
                    }

                    FS.HISFC.Models.RADT.PatientInfo patientInfo = new FS.HISFC.Models.RADT.PatientInfo();
                    patientInfo.SIMainInfo.Mdtrt_id = con.Name;//obj.Name;
                    patientInfo.SIMainInfo.Setl_id = con.ID;//obj.ID;
                    patientInfo.SIMainInfo.InvoiceNo = con.OperEnvironment.ID;
                    #endregion
                    print = new ucInBalanceInfoForClinicNew();
                    this.neuPanel3.Controls.Clear();
                    this.neuPanel3.Controls.Add(print);
                    print.Query(patientInfo, true);
                }
            }
        }

        /// <summary>
        /// 挂号信息选择事件
        /// </summary>
        /// <param name="reg"></param>
        public void ucShow_SelectedRegister(FS.HISFC.Models.Registration.Register reg)
        {
            print = new ucInBalanceInfoForClinicNew();
            //print.Dock = System.Windows.Forms.DockStyle.Fill;
            this.neuPanel3.Controls.Clear();
            this.neuPanel3.Controls.Add(print);
            print.Query(reg, true);
        }
        #endregion

        private void neuButton1_Click(object sender, EventArgs e)
        {
            print.PrintInBalanceInfo(5, 5);
        }

        private void rbOut_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbOut.Checked)
            {
                this.lblCardType.Text = "门诊号：";
                this.lblCardType.Visible = true;
                this.txtCardNo.Visible = true;
            }
            else if (this.rbIn.Checked)
            {
                this.lblCardType.Text = "住院号：";
                this.lblCardType.Visible = true;
                this.txtCardNo.Visible = true;
            }
            else if (this.rbMedNo.Checked)
            {
                this.lblCardType.Text = "就医登记号：";
                this.lblCardType.Visible = true;
                this.txtCardNo.Visible = true;
            }
        }

        private void rbIn_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbOut.Checked)
            {
                this.lblCardType.Text = "门诊号：";
                this.lblCardType.Visible = true;
                this.txtCardNo.Visible = true;
            }
            else if (this.rbIn.Checked)
            {
                this.lblCardType.Text = "住院号：";
                this.lblCardType.Visible = true;
                this.txtCardNo.Visible = true;
            }
            else if (this.rbMedNo.Checked)
            {
                this.lblCardType.Text = "就医登记号：";
                this.lblCardType.Visible = true;
                this.txtCardNo.Visible = true;
            }

        }

        private void rbMedNo_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbOut.Checked)
            {
                this.lblCardType.Text = "门诊号：";
                this.lblCardType.Visible = true;
                this.txtCardNo.Visible = true;
            }
            else if (this.rbIn.Checked)
            {
                this.lblCardType.Text = "住院号：";
                this.lblCardType.Visible = true;
                this.txtCardNo.Visible = true;
            }
            else if (this.rbMedNo.Checked)
            {
                this.lblCardType.Text = "就医登记号：";
                this.lblCardType.Visible = true;
                this.txtCardNo.Visible = true;
            }

        }

    }
}
