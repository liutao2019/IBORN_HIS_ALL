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
    /// ע���ʻ�����
    /// </summary>
    public partial class ucFindAccountPassWord :FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucFindAccountPassWord()
        {
            InitializeComponent();
        }

        #region ����
        /// <summary>
        /// �ʻ�ʵ��
        /// </summary>
        HISFC.Models.Account.Account account = null;

        FS.HISFC.Components.Account.Controls.ucRegPatientInfo ucRegPatientInfo1=new ucRegPatientInfo();
        /// <summary>
        /// �ۺϹ���ҵ���
        /// </summary>
        HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// �ʻ�����ҵ���
        /// </summary>
        FS.HISFC.BizLogic.Fee.Account accountManager = new FS.HISFC.BizLogic.Fee.Account();

        /// <summary>
        /// ������
        /// </summary>
        private FS.FrameWork.WinForms.Forms.ToolBarService toolbarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
        #endregion

        #region ����
        /// <summary>
        /// ��������
        /// </summary>
        public  void QueryAccountPwd()
        {
            string error=string.Empty;
            if (this.cmbidNOType.Tag == null || this.cmbidNOType.Tag.ToString() == string.Empty)
            {
                MessageBox.Show("������֤�����ͣ�");
                this.cmbidNOType.Focus();
                return;
            }
            //�������֤���Ƿ���ȷ
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
                MessageBox.Show("�����ʻ���Ϣʧ�ܣ�" + accountManager.Err);
                return;
            }
            if (accountList.Count > 1)
            {
                MessageBox.Show("��֤���Ŷ�Ӧ���ʻ���Ϣ��Ψһ���������Ա��ϵ��");
                return;
            }
            if (accountList.Count == 0)
            {
                MessageBox.Show("��֤����������Ч���ʻ���Ϣ�����������룡");
                this.txtidenno.SelectAll();
                this.txtidenno.Focus();
                return;
            }

            account = accountList[0] as HISFC.Models.Account.Account;
            this.ucRegPatientInfo1.CardNO = account.CardNO;
            this.txtPassWord.Text = account.PassWord;
        }

        /// <summary>
        /// �޸�����
        /// </summary>
        public void EditPassWord()
        {
            if (account == null)
            {
                MessageBox.Show("��س�ȷ��֤���ţ�");
                return;
            }
            ucEditPassWord uc = new ucEditPassWord(true);
            uc.Account = account;
            //���ж�ԭ������
            uc.IsValidoldPwd = false;
            FS.FrameWork.WinForms.Classes.Function.ShowControl(uc);
            if (uc.FindForm().DialogResult == DialogResult.OK)
            {
                MessageBox.Show("�޸�����ɹ���");
                this.Clear();
                return;
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        public void Clear()
        {
            this.ucRegPatientInfo1.Clear(true);
            if (this.cmbidNOType.Items.Count > 0)
            {
                //01�����֤
                this.cmbidNOType.Tag = "01";
            }
            this.txtidenno.Text = string.Empty;
            this.txtPassWord.Text = string.Empty;
            account = null;
        }
        #endregion

        #region �¼�

        private void ucFindAccountPassWord_Load(object sender, EventArgs e)
        {
            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToLower() == "devenv") return;
            this.ActiveControl = this.txtidenno;
            //֤������
            System.Collections.ArrayList al = managerIntegrate.QueryConstantList("IDCard");
            if (al == null) return;
            this.cmbidNOType.AddItems(al);
            if (al.Count > 0)
            {
                cmbidNOType.Tag = "01";//���֤
            }

        }

        private void txtidenno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                this.QueryAccountPwd();
        }

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolbarService.AddToolButton("�޸�����", "�޸�����", FS.FrameWork.WinForms.Classes.EnumImageList.X�޸�, true, false, null);
            toolbarService.AddToolButton("���֤", "���֤", FS.FrameWork.WinForms.Classes.EnumImageList.Mģ��, true, false, null);
            return toolbarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "�޸�����":
                    {
                        EditPassWord();
                        break;
                    }
                case "���֤":
                    {
                        if (ReadIDCard() == 1)
                            txtidenno_KeyDown(sender, new KeyEventArgs(Keys.Enter));
                        break; 
                    }
            }
            base.ToolStrip_ItemClicked(sender, e);
        }
        /// <summary>
        /// ��ȡ���֤
        /// </summary>
        /// <returns></returns>
        public int ReadIDCard()
        {
            FS.HISFC.BizProcess.Interface.Account.IReadIDCard readIDCard = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Account.IReadIDCard)) as FS.HISFC.BizProcess.Interface.Account.IReadIDCard;
            if (readIDCard == null)
            {
                MessageBox.Show("�����֤�ӿ�û��ʵ��");
                return -1;
            }
            string code = "", name = "", sex = "", nation = "", agent = "", add = "", message = "";
            DateTime birth = DateTime.MinValue, bigen = DateTime.MinValue, end = DateTime.MinValue;
            string photoFileName = "";
            int rtn = readIDCard.GetIDInfo(ref code, ref name, ref sex, ref birth, ref nation, ref add, ref agent, ref bigen, ref end, ref photoFileName, ref message);
            if (rtn == -1)
            {
                MessageBox.Show("��ȡ���֤����" + message);
                return -1;
            }

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(code))
            {
                MessageBox.Show("��ȡ���֤ʧ�ܣ���ȷ�Ϸź�λ�ã�" + message);
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
