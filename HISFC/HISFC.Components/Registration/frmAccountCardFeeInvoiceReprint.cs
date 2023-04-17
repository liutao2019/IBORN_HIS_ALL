using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;
using FS.HISFC.Models.Account;

namespace FS.HISFC.Components.Registration
{
    /// <summary>
    /// 挂号发票补打[新]
    /// </summary>
    public partial class frmAccountCardFeeInvoiceReprint : Form
    {
        public frmAccountCardFeeInvoiceReprint()
        {
            InitializeComponent();

            this.Init();
        }

        public frmAccountCardFeeInvoiceReprint(FS.HISFC.Models.Registration.Register reg)
        {
            InitializeComponent();

            obj = reg;
            Init();
        }

        #region 变量

        /// <summary>
        /// 控制管理类
        /// </summary>
        private FS.FrameWork.Management.ControlParam ctlMgr = new FS.FrameWork.Management.ControlParam();

        /// <summary>
        /// 帐户管理
        /// </summary>
        private FS.HISFC.BizLogic.Fee.Account accMgr = new FS.HISFC.BizLogic.Fee.Account();

        FS.HISFC.BizProcess.Integrate.RADT radt = new FS.HISFC.BizProcess.Integrate.RADT();

        /// <summary>
        /// 当前挂号实体
        /// </summary>
        private FS.HISFC.Models.Registration.Register obj;

        /// <summary>
        /// 挂号实体
        /// </summary>
        public FS.HISFC.Models.Registration.Register Register
        {
            get
            {
                return this.obj;
            }
        }

        /// <summary>
        /// 挂号管理类
        /// </summary>
        private FS.HISFC.BizLogic.Registration.Register regMgr = new FS.HISFC.BizLogic.Registration.Register();

        private FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();

        /// <summary>
        /// 可退号天数
        /// </summary>
        private int PermitDays = 1;

        #endregion

        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            this.Load += new EventHandler(frmAccountCardFeeInvoiceReprint_Load);
            this.txtCardNo.KeyDown += new KeyEventHandler(txtCardNo_KeyDown);
            this.txtName.KeyDown += new KeyEventHandler(txtName_KeyDown);
            this.cmbSex.KeyDown += new KeyEventHandler(cmbSex_KeyDown);
            this.dtBirthday.KeyDown += new KeyEventHandler(dtBirthday_KeyDown);
            this.txtPhone.KeyDown += new KeyEventHandler(txtPhone_KeyDown);
            this.txtAdress.KeyDown += new KeyEventHandler(txtAdress_KeyDown);

            this.button1.Click += new EventHandler(button1_Click);
            this.button2.Click += new EventHandler(button2_Click);
            this.neuButton1.Click += new EventHandler(btnPrintBarCode_Click);

            //门诊退号－允许退号天数
            string Days = this.ctlMgr.QueryControlerInfo("400006");

            if (Days == null || Days == "" || Days == "-1")
            {
                this.PermitDays = 1;
            }
            else
            {
                this.PermitDays = int.Parse(Days);
            }

            //焦点所在
            this.txtCardNo.Select();
            this.txtCardNo.Focus();
        }

        /// <summary>
        /// 界面赋值
        /// </summary>
        /// <param name="reg"></param>
        private void SetValue(FS.HISFC.Models.Registration.Register reg)
        {
            if (reg == null)
            {
                reg = new FS.HISFC.Models.Registration.Register();
            }

            //{E4CB6714-9AA1-4228-AE94-ACAF32EDFACA}
            //if (reg.LstCardFee.Count > 0)
            //{
            //    this.txtRecipeNo.Text = (reg.LstCardFee[0] as FS.HISFC.Models.Account.AccountCardFee).InvoiceNo;
            //}
            //else
            //{
            //    this.txtRecipeNo.Text = reg.RecipeNO;
            //}

            this.txtRecipeNo.Text = reg.InvoiceNO;
            this.txtSeeNo.Text = reg.OrderNO.ToString();
            this.txtName.Text = reg.Name;
            this.cmbSex.Tag = reg.Sex.ID;
            this.dtBirthday.Value = reg.Birthday;
            this.txtPhone.Text = reg.PhoneHome;
            this.txtAdress.Text = reg.AddressHome;
        }


        private void frmAccountCardFeeInvoiceReprint_Load(object sender, EventArgs e)
        {
            this.cmbSex.AddItems(FS.HISFC.Models.Base.SexEnumService.List());

            if (this.obj != null)
            {
                this.SetValue(obj);
                this.button1.Focus();
            }
        }

        private void txtCardNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string cardNo = this.txtCardNo.Text.Trim();
                this.Clear();

                if (cardNo == "")
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("请输入病历号"), "提示");
                    this.txtCardNo.Focus();
                    return;
                }

                FS.HISFC.Models.Account.AccountCard accountCardObj = new FS.HISFC.Models.Account.AccountCard();
                int rev = this.feeIntegrate.ValidMarkNO(cardNo, ref accountCardObj);
                if (rev <= 0)
                {
                    MessageBox.Show(this.feeIntegrate.Err);
                    return;
                }
                cardNo = accountCardObj.Patient.PID.CardNO;

                this.txtCardNo.Text = cardNo;

                DateTime permitDate = this.regMgr.GetDateTimeFromSysDateTime().AddDays(-this.PermitDays);

                //ArrayList al= this.regMgr.QueryRegList(cardNo, permitDate, this.regMgr.GetDateTimeFromSysDateTime(),"1");
                ArrayList al = this.regMgr.Query(cardNo, permitDate);
                if (al == null||al.Count==0)
                {
                    MessageBox.Show("未检索到患者的相关信息" + this.accMgr.Err);
                    return;
                }

                if (al.Count== 1)
                {

                    string clinicNO = (al[0] as FS.HISFC.Models.Registration.Register).ID;

                    if (string.IsNullOrEmpty(clinicNO))
                    {
                        //先检索患者基本信息表,看是否存在该患者信息
                        FS.HISFC.Models.RADT.PatientInfo p = radt.QueryComPatientInfo(cardNo);

                        if (p == null || p.Name == "")
                        {
                            //不存在患者基本信息
                            MessageBox.Show("不常在该患者的信息", "提示");
                            return;
                        }

                        this.obj = new FS.HISFC.Models.Registration.Register();

                        //存在患者基本信息,取基本信息
                        this.obj.PID.CardNO = cardNo;
                        this.obj.Name = p.Name;
                        this.obj.Sex.ID = p.Sex.ID;
                        this.obj.Birthday = p.Birthday;
                        this.obj.Pact.ID = p.Pact.ID;
                        this.obj.Pact.PayKind.ID = p.Pact.PayKind.ID;
                        this.obj.SSN = p.SSN;
                        this.obj.PhoneHome = p.PhoneHome;
                        this.obj.AddressHome = p.AddressHome;
                        this.obj.IDCard = p.IDCard;
                        this.obj.NormalName = p.NormalName;
                        this.obj.IsEncrypt = p.IsEncrypt;
                        this.obj.IDCard = p.IDCard;

                        if (p.IsEncrypt == true)
                        {
                            this.obj.Name = FS.FrameWork.WinForms.Classes.Function.Decrypt3DES(p.NormalName);
                        }

                    }
                    else
                    {
                        this.obj = this.regMgr.GetByClinic(clinicNO);
                        if (this.obj == null || this.obj.Name == "")
                        {
                            MessageBox.Show("检索患者的挂号信息出错!", "提示");
                            return;
                        }
                    }

                    //{E4CB6714-9AA1-4228-AE94-ACAF32EDFACA}
                    //string invoiceNO = (al[0] as FS.HISFC.Models.Registration.Register).InvoiceNO;
                    //List<AccountCardFee> lstCardFee = null;

                    //int iRes = this.accMgr.QueryAccountCardFeeByInvoiceNO(invoiceNO, out lstCardFee);

                    //if (lstCardFee != null && lstCardFee.Count > 0)
                    //{


                    //    this.obj.LstCardFee = lstCardFee;
                    //    if (string.IsNullOrEmpty(this.obj.InvoiceNO))
                    //    {
                    //        this.obj.InvoiceNO = (lstCardFee[0] as FS.HISFC.Models.Account.AccountCardFee).InvoiceNo;
                    //    }
                    //}


                }
                else if (al.Count > 1)
                {
                    string clinicNO = string.Empty;
                    string invoiceNO = string.Empty;

                    if (this.getSelectRegInfo(al, ref clinicNO, ref invoiceNO) == -1)
                    {
                        this.txtCardNo.Select();
                        this.txtCardNo.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(clinicNO))
                    {
                        //先检索患者基本信息表,看是否存在该患者信息
                        FS.HISFC.Models.RADT.PatientInfo p = radt.QueryComPatientInfo(cardNo);

                        if (p == null || p.Name == "")
                        {
                            //不存在患者基本信息
                            MessageBox.Show("不存在该患者的信息", "提示");
                            return;
                        }

                        this.obj = new FS.HISFC.Models.Registration.Register();

                        //存在患者基本信息,取基本信息
                        this.obj.PID.CardNO = cardNo;
                        this.obj.Name = p.Name;
                        this.obj.Sex.ID = p.Sex.ID;
                        this.obj.Birthday = p.Birthday;
                        this.obj.Pact.ID = p.Pact.ID;
                        this.obj.Pact.PayKind.ID = p.Pact.PayKind.ID;
                        this.obj.SSN = p.SSN;
                        this.obj.PhoneHome = p.PhoneHome;
                        this.obj.AddressHome = p.AddressHome;
                        this.obj.IDCard = p.IDCard;
                        this.obj.NormalName = p.NormalName;
                        this.obj.IsEncrypt = p.IsEncrypt;
                        this.obj.IDCard = p.IDCard;

                        if (p.IsEncrypt == true)
                        {
                            this.obj.Name = FS.FrameWork.WinForms.Classes.Function.Decrypt3DES(p.NormalName);
                        }

                    }
                    else
                    {
                        this.obj = this.regMgr.GetByClinic(clinicNO);
                        if (this.obj == null || this.obj.Name == "")
                        {
                            MessageBox.Show("检索患者的挂号信息出错!", "提示");
                            return;
                        }
                    }

                    //{E4CB6714-9AA1-4228-AE94-ACAF32EDFACA}
                    //List<AccountCardFee> lstCardFee = null;

                    //int iRes = this.accMgr.QueryAccountCardFeeByInvoiceNO(invoiceNO, out lstCardFee);

                    //if (lstCardFee != null && lstCardFee.Count > 0)
                    //{


                    //    this.obj.LstCardFee = lstCardFee;

                    //    if (string.IsNullOrEmpty(this.obj.InvoiceNO))
                    //    {
                    //        this.obj.InvoiceNO = (lstCardFee[0] as FS.HISFC.Models.Account.AccountCardFee).InvoiceNo;
                    //    }
                    //}

                }

                this.SetValue(this.obj);

                this.txtName.Focus();
            }
        }

        private void Clear()
        {
            this.obj = null;

            this.txtCardNo.Text = "";
            this.txtRecipeNo.Text = "";
            this.txtSeeNo.Text = "";
            this.txtName.Text = "";
            this.cmbSex.Tag = "";
            this.txtPhone.Text = "";
            this.txtAdress.Text = "";
        }

        private int getSelectRegInfo(ArrayList dsResult, ref string ClinicNO, ref string invoiceNO)
        {

            frmSelectInvoice f = new frmSelectInvoice();

            f.SetRegInfo(dsResult);

            DialogResult dr = f.ShowDialog();

            if (dr == DialogResult.OK)
            {
                ClinicNO = f.ClinicNO;
                invoiceNO = f.InvoiceNO;
                f.Dispose();
            }
            else
            {
                f.Dispose();
                return -1;
            }

            return 0;
        }
        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.cmbSex.Focus();
            }
        }

        private void cmbSex_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.dtBirthday.Focus();
            }
        }

        private void dtBirthday_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtPhone.Focus();
            }
        }

        private void txtPhone_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtAdress.Focus();
            }
        }

        private void txtAdress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.button1.Focus();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.valid() == -1) return;

            //{E4CB6714-9AA1-4228-AE94-ACAF32EDFACA}
            //DataSet dsResult = new DataSet();
            //DateTime permitDate = this.regMgr.GetDateTimeFromSysDateTime().AddDays(-this.PermitDays);

            //int res = this.accMgr.QueryAccountCardFeeByCardNOAndDate(this.obj.PID.CardNO, permitDate.ToString(), this.regMgr.GetDateTimeFromSysDateTime().ToString(), ref dsResult);

            //if (dsResult == null || dsResult.Tables[0].Rows.Count <= 0)
            //{
            //    MessageBox.Show("未检索到相关患者的费用信息" + this.accMgr.Err);
            //    return;
            //}

            //
            this.obj.Name = this.txtName.Text.Trim();
            this.obj.Sex.ID = this.cmbSex.Tag.ToString();
            this.obj.Birthday = this.dtBirthday.Value;
            this.obj.PhoneHome = this.txtPhone.Text.Trim();
            this.obj.AddressHome = this.txtAdress.Text.Trim();
            this.obj.PrintInvoiceCnt = 1;//补打发票
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        void btnPrintBarCode_Click(object sender, EventArgs e)
        {
            if (this.valid() == -1) return;
            //
            this.obj.Name = this.txtName.Text.Trim();
            this.obj.Sex.ID = this.cmbSex.Tag.ToString();
            this.obj.Birthday = this.dtBirthday.Value;
            this.obj.PhoneHome = this.txtPhone.Text.Trim();
            this.obj.AddressHome = this.txtAdress.Text.Trim();
            this.obj.PrintInvoiceCnt = 2;//补打条码
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.Close();
            }
            return base.ProcessDialogKey(keyData);
        }

        /// <summary>
        /// 验证
        /// </summary>
        /// <returns></returns>
        private int valid()
        {
            if (this.obj == null)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("请选择挂号信息"), "提示");
                this.txtCardNo.Focus();
                return -1;
            }

            if (this.txtName.Text.Trim() == "")
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("请输入患者姓名"), "提示");
                this.txtName.Focus();
                return -1;
            }


            if (FS.FrameWork.Public.String.ValidMaxLengh(this.txtName.Text.Trim(), 40) == false)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("患者名称最多可录入20个汉字"), "提示");
                this.txtName.Focus();
                return -1;
            }
            if (this.cmbSex.Tag == null || this.cmbSex.Tag.ToString() == "")
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("请选择患者性别"), "提示");
                this.cmbSex.Focus();
                return -1;
            }


            if (FS.FrameWork.Public.String.ValidMaxLengh(this.txtPhone.Text.Trim(), 20) == false)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("联系电话最多可录入20位数字"), "提示");
                this.txtPhone.Focus();
                return -1;
            }
            if (FS.FrameWork.Public.String.ValidMaxLengh(this.txtAdress.Text.Trim(), 60) == false)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("联系人地址最多可录入30个汉字"), "提示");
                this.txtAdress.Focus();
                return -1;
            }

            //if ((this.txtPhone.Text == null || this.txtPhone.Text.Trim() == "") &&
            //    (this.txtAdress.Text == null || this.txtAdress.Text.Trim() == ""))
            //{
            //    MessageBox.Show(FS.FrameWork.Management.Language.Msg("联系电话和地址不能同时为空,必须输入一个"), "提示");
            //    this.txtPhone.Focus();
            //    return -1;
            //}

            return 0;

        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            //this.DialogResult = DialogResult.No ;
            this.Close();
        }
    }
}