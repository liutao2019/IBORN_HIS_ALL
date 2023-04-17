using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;


namespace FS.HISFC.Components.Account.Controls
{
    /// <summary>
    /// 注销帐户密码
    /// </summary>
    public partial class ucFindAccountPassWord :FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucFindAccountPassWord()
        {
            InitializeComponent();
        }

        #region 变量
        /// <summary>
        /// 帐户实体
        /// </summary>
        HISFC.Models.Account.Account account = null;

        FS.HISFC.Components.Account.Controls.ucRegPatientInfo ucRegPatientInfo1=new ucRegPatientInfo();
        /// <summary>
        /// 综合管理业务层
        /// </summary>
        HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// 帐户管理业务层
        /// </summary>
        FS.HISFC.BizLogic.Fee.Account accountManager = new FS.HISFC.BizLogic.Fee.Account();

        /// <summary>
        /// 工具栏
        /// </summary>
        private FS.FrameWork.WinForms.Forms.ToolBarService toolbarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
        #endregion

        #region 方法
        /// <summary>
        /// 查找密码
        /// </summary>
        public  void QueryAccountPwd()
        {
            string error=string.Empty;
            if (this.cmbidNOType.Tag == null || this.cmbidNOType.Tag.ToString() == string.Empty)
            {
                MessageBox.Show("请输入证件类型！");
                this.cmbidNOType.Focus();
                return;
            }
            //检验身份证号是否正确
            string idennostr = this.txtidenno.Text.Trim();
            string idennoType = this.cmbidNOType.Tag.ToString();
            if (idennoType == "01")
            {
                int resultValue = FS.FrameWork.WinForms.Classes.Function.CheckIDInfo(idennostr, ref error);
                if (resultValue < 0)
                {
                    MessageBox.Show(error);
                    this.txtidenno.Focus();
                    this.txtidenno.SelectAll();
                    return;
                }
            }
            ArrayList accountList = accountManager.GetAccountByIdNO(this.txtidenno.Text, this.cmbidNOType.Tag.ToString());
            if (accountList == null)
            {
                MessageBox.Show("查找帐户信息失败！" + accountManager.Err);
                return;
            }
            if (accountList.Count > 1)
            {
                MessageBox.Show("该证件号对应的帐户信息不唯一，请与管理员联系！");
                return;
            }
            if (accountList.Count == 0)
            {
                MessageBox.Show("该证件不存在有效的帐户信息，请重新输入！");
                this.txtidenno.SelectAll();
                this.txtidenno.Focus();
                return;
            }

            account = accountList[0] as HISFC.Models.Account.Account;
            this.ucRegPatientInfo1.CardNO = account.CardNO;
            this.txtPassWord.Text = account.PassWord;
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        public void EditPassWord()
        {
            if (account == null)
            {
                MessageBox.Show("请回车确认证件号！");
                return;
            }
            ucEditPassWord uc = new ucEditPassWord(true);
            uc.Account = account;
            //不判断原来密码
            uc.IsValidoldPwd = false;
            FS.FrameWork.WinForms.Classes.Function.ShowControl(uc);
            if (uc.FindForm().DialogResult == DialogResult.OK)
            {
                MessageBox.Show("修改密码成功！");
                this.Clear();
                return;
            }
        }

        /// <summary>
        /// 清屏
        /// </summary>
        public void Clear()
        {
            this.ucRegPatientInfo1.Clear(true);
            if (this.cmbidNOType.Items.Count > 0)
            {
                //01是身份证
                this.cmbidNOType.Tag = "01";
            }
            this.txtidenno.Text = string.Empty;
            this.txtPassWord.Text = string.Empty;
            account = null;
        }
        #endregion

        #region 事件

        private void ucFindAccountPassWord_Load(object sender, EventArgs e)
        {
            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToLower() == "devenv") return;
            this.ActiveControl = this.txtidenno;
            //证件类型
            System.Collections.ArrayList al = managerIntegrate.QueryConstantList("IDCard");
            if (al == null) return;
            this.cmbidNOType.AddItems(al);
            if (al.Count > 0)
            {
                cmbidNOType.Tag = "01";//身份证
            }

        }

        private void txtidenno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                this.QueryAccountPwd();
        }

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolbarService.AddToolButton("修改密码", "修改密码", FS.FrameWork.WinForms.Classes.EnumImageList.X修改, true, false, null);
            toolbarService.AddToolButton("身份证", "身份证", FS.FrameWork.WinForms.Classes.EnumImageList.M模版, true, false, null);
            return toolbarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "修改密码":
                    {
                        EditPassWord();
                        break;
                    }
                case "身份证":
                    {
                        if (ReadIDCard() == 1)
                            txtidenno_KeyDown(sender, new KeyEventArgs(Keys.Enter));
                        break; 
                    }
            }
            base.ToolStrip_ItemClicked(sender, e);
        }
        /// <summary>
        /// 读取身份证
        /// </summary>
        /// <returns></returns>
        public int ReadIDCard()
        {
            FS.HISFC.BizProcess.Interface.Account.IReadIDCard readIDCard = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Account.IReadIDCard)) as FS.HISFC.BizProcess.Interface.Account.IReadIDCard;
            if (readIDCard == null)
            {
                MessageBox.Show("读身份证接口没有实现");
                return -1;
            }
            string code = "", name = "", sex = "", nation = "", agent = "", add = "", message = "";
            DateTime birth = DateTime.MinValue, bigen = DateTime.MinValue, end = DateTime.MinValue;
            string photoFileName = "";
            int rtn = readIDCard.GetIDInfo(ref code, ref name, ref sex, ref birth, ref nation, ref add, ref agent, ref bigen, ref end, ref photoFileName, ref message);
            if (rtn == -1)
            {
                MessageBox.Show("读取身份证出错！" + message);
                return -1;
            }

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(code))
            {
                MessageBox.Show("读取身份证失败，请确认放好位置！" + message);
                return -1;
            }

            if (!string.IsNullOrEmpty(message))
            {
                MessageBox.Show(message);
                return -1;
            }
            this.txtidenno.Text = code;
            return 1;
        }
        #endregion
    }
}
