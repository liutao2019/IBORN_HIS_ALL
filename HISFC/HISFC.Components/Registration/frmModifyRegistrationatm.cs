using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Registration
{
    /// <summary>
    /// 挂号发票补打
    /// </summary>
    public partial class frmModifyRegistrationatm : Form
    {
        public frmModifyRegistrationatm()
        {
            InitializeComponent();

            this.Init();
        }

        public frmModifyRegistrationatm(FS.HISFC.Models.Registration.Register reg)
        {
            InitializeComponent();

            obj = reg;
            Init();
        }

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
        /// 初始化
        /// </summary>
        private void Init()
        {
            this.Load += new EventHandler(frmModifyRegistration_Load);
            this.txtCardNo.KeyDown += new KeyEventHandler(txtCardNo_KeyDown);
            this.txtName.KeyDown += new KeyEventHandler(txtName_KeyDown);
            this.cmbSex.KeyDown += new KeyEventHandler(cmbSex_KeyDown);
            this.dtBirthday.KeyDown += new KeyEventHandler(dtBirthday_KeyDown);
            this.txtPhone.KeyDown += new KeyEventHandler(txtPhone_KeyDown);
            this.txtAdress.KeyDown += new KeyEventHandler(txtAdress_KeyDown);

            this.button1.Click += new EventHandler(button1_Click);
            this.button2.Click += new EventHandler(button2_Click);

            this.button1.Focus();
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

            this.txtRecipeNo.Text = reg.InvoiceNO;
            this.txtSeeNo.Text = reg.OrderNO.ToString();
            this.txtName.Text = reg.Name;
            this.cmbSex.Tag = reg.Sex.ID;
            this.dtBirthday.Value = reg.Birthday;
            this.txtPhone.Text = reg.PhoneHome;
            this.txtAdress.Text = reg.AddressHome;
        }


        private void frmModifyRegistration_Load(object sender, EventArgs e)
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
                    MessageBox.Show( FS.FrameWork.Management.Language.Msg( "请输入病历号" ), "提示" );
                    this.txtCardNo.Focus();
                    return;
                }

                //cardNo = cardNo.PadLeft(10, '0');
                FS.HISFC.Models.Account.AccountCard accountCardObj = new FS.HISFC.Models.Account.AccountCard();
                int rev = this.feeIntegrate.ValidMarkNO(cardNo, ref accountCardObj);
                if (rev <= 0)
                {
                    MessageBox.Show(this.feeIntegrate.Err);
                    return;
                }
                cardNo = accountCardObj.Patient.PID.CardNO;

                this.txtCardNo.Text = cardNo;

                DateTime permitDate = this.regMgr.GetDateTimeFromSysDateTime().Date;
                permitDate = permitDate.AddDays(-30);
                //检索患者有效号
                ArrayList tal = this.regMgr.Query(cardNo, permitDate);
                if (tal == null)
                {
                    MessageBox.Show("检索患者挂号信息时出错!" + this.regMgr.Err, "提示");
                    return;
                }

                if (tal.Count == 0)
                {
                    MessageBox.Show( FS.FrameWork.Management.Language.Msg( "没有符合条件的挂号信息" ), "提示" );
                    return;
                }

                ArrayList al = new ArrayList();
                foreach (FS.HISFC.Models.Registration.Register tr in tal)
                {
                    if (tr.InvoiceNO.StartsWith("R"))
                    {
                        al.Add(tr);
                    }
 
                }
                if (al.Count == 1)
                {
                    this.obj = (FS.HISFC.Models.Registration.Register)al[0];
                }
                else
                {
                    if (this.getSelectRegInfo(al) == -1)
                    {
                        this.txtCardNo.Focus();
                        return;
                    }
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

        private int getSelectRegInfo(ArrayList alRegs)
        {
            frmSelectRegister f = new frmSelectRegister();

            f.SetRegInfo(alRegs);

            DialogResult dr = f.ShowDialog();

            if (dr == DialogResult.OK)
            {
                this.obj = f.Reg;
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

            //
            this.obj.Name = this.txtName.Text.Trim();
            this.obj.Sex.ID = this.cmbSex.Tag.ToString();
            this.obj.Birthday = this.dtBirthday.Value;
            this.obj.PhoneHome = this.txtPhone.Text.Trim();
            this.obj.AddressHome = this.txtAdress.Text.Trim();

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
            if (this.obj == null || this.obj.ID == "")
            {
                MessageBox.Show( FS.FrameWork.Management.Language.Msg( "请选择挂号信息" ), "提示" );
                this.txtCardNo.Focus();
                return -1;
            }

            if (this.txtName.Text.Trim() == "")
            {
                MessageBox.Show( FS.FrameWork.Management.Language.Msg( "请输入患者姓名" ), "提示" );
                this.txtName.Focus();
                return -1;
            }


            if (FS.FrameWork.Public.String.ValidMaxLengh(this.txtName.Text.Trim(), 40) == false)
            {
                MessageBox.Show( FS.FrameWork.Management.Language.Msg( "患者名称最多可录入20个汉字" ), "提示" );
                this.txtName.Focus();
                return -1;
            }
            if (this.cmbSex.Tag == null || this.cmbSex.Tag.ToString() == "")
            {
                MessageBox.Show( FS.FrameWork.Management.Language.Msg( "请选择患者性别" ), "提示" );
                this.cmbSex.Focus();
                return -1;
            }


            if (FS.FrameWork.Public.String.ValidMaxLengh(this.txtPhone.Text.Trim(), 20) == false)
            {
                MessageBox.Show( FS.FrameWork.Management.Language.Msg( "联系电话最多可录入20位数字" ), "提示" );
                this.txtPhone.Focus();
                return -1;
            }
            if (FS.FrameWork.Public.String.ValidMaxLengh(this.txtAdress.Text.Trim(), 60) == false)
            {
                MessageBox.Show( FS.FrameWork.Management.Language.Msg( "联系人地址最多可录入30个汉字" ), "提示" );
                this.txtAdress.Focus();
                return -1;
            }

            if ((this.txtPhone.Text == null || this.txtPhone.Text.Trim() == "") &&
                (this.txtAdress.Text == null || this.txtAdress.Text.Trim() == ""))
            {
                MessageBox.Show( FS.FrameWork.Management.Language.Msg( "联系电话和地址不能同时为空,必须输入一个" ), "提示" );
                this.txtPhone.Focus();
                return -1;
            }
            //if(this.dtBirthday.Value

            return 0;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //this.DialogResult = DialogResult.No ;
            this.Close();
        }

        private void neuButton1_Click(object sender, EventArgs e)
        {
        
            string cardNo = "";
            string error = "";
            if (Function.OperMCard(ref cardNo, ref error) == -1)
            {
                MessageBox.Show(error);
                return;
            }

            txtCardNo.Text = cardNo;
            txtCardNo.Focus();
            this.txtCardNo_KeyDown(null, new KeyEventArgs(Keys.Enter));

        }
  }		
}