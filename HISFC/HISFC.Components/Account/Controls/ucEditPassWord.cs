using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.BizProcess.Interface.Account;

namespace FS.HISFC.Components.Account.Controls
{
    /// <summary>
    /// �ʻ�����
    /// </summary>
    public partial class ucEditPassWord : UserControl, FS.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        public ucEditPassWord(bool isedit)
        {
            this.isEdit = isedit;
            InitializeComponent();
        }

        public ucEditPassWord()
        {
            InitializeComponent();
        }��

        #region ����
        /// <summary>
        /// �Ƿ��޸�����
        /// </summary>
        private bool isEdit=false;

        
        /// <summary>
        /// �Ƿ�����֤����
        /// </summary>
        private bool isVerifyPSW = false;
        
        /// <summary>
        /// ����
        /// </summary>
        private string pwstr = string.Empty;

        /// <summary>
        /// ���￨��
        /// </summary>
        private HISFC.Models.Account.Account account = new FS.HISFC.Models.Account.Account();

        /// <summary>
        /// �ʻ�ҵ������
        /// </summary>
        HISFC.BizLogic.Fee.Account accountManager = new FS.HISFC.BizLogic.Fee.Account();
        
        /// <summary>
        /// �������ת����ҵ���
        /// </summary>
        HISFC.BizProcess.Integrate.RADT radtIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();

        private bool isValidoldPwd = true;

        /// <summary>
        /// ���˻�ʱ�Ƿ��Զ�Ĭ������
        /// {9AE6E29C-1CAB-40b8-8F81-0F8379FEB8C9}
        /// </summary>
        protected bool IsDefaultPassword = true; 
        #endregion

        #region ����
        /// <summary>
        /// �Ƿ��޸�����
        /// </summary>
        public bool IsEdit
        {
            get
            {
                return isEdit;
            }
            set
            {
                isEdit = value;
            }
        }
        /// <summary>
        /// �Ƿ�����֤����
        /// </summary>
        public bool IsVerifyPSW
        {
            get
            {
                return isVerifyPSW;
            }
            set
            {
                isVerifyPSW = value;
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string PwStr
        {
            get
            {
                return pwstr;
            }
            set
            {
                pwstr = value;
            }
        }


        /// <summary>
        /// ���޸������ʱ���Ƿ��ж�ԭ�����Ƿ���ȷ
        /// </summary>
        public bool IsValidoldPwd
        {
            get
            {
                return isValidoldPwd;
            }
            set
            {
                isValidoldPwd = value;
            }
        }

        /// <summary>
        /// �ʺ�
        /// </summary>
        public HISFC.Models.Account.Account Account
        {
            get
            {
                return account;
            }
            set
            {
                account = value;
            }
        }
        #endregion

        #region ����

        private bool ValidPwd()
        {
            string newPwdStr = this.txtnewpwd.Text.Trim();
            string confirmPwdStr = this.txtconfirmpwd.Text.Trim();
            if (newPwdStr != confirmPwdStr)
            {
                MessageBox.Show("����������ȷ�����벻��������������룡");
                this.txtnewpwd.Text = string.Empty;
                this.txtconfirmpwd.Text = string.Empty;
                this.txtoldpwd.Text = string.Empty;
                this.txtnewpwd.Focus();
                return false;
            }
            if (IsEdit)
            {
                string oldPwdStr = this.txtoldpwd.Text.Trim();
                //�ڲ����ʻ����룬�޸������ʱ���ж�ԭ����
                if (this.isValidoldPwd)
                {
                    if (oldPwdStr == string.Empty)
                    {
                        MessageBox.Show("�������ʻ�ԭ���룡");
                        this.txtoldpwd.Focus();
                        return false;
                    }

                    if (account.PassWord != oldPwdStr)
                    {
                        MessageBox.Show("ԭ��������������������룡");
                        this.txtnewpwd.Text = string.Empty;
                        this.txtconfirmpwd.Text = string.Empty;
                        this.txtoldpwd.Text = string.Empty;
                        this.txtoldpwd.Focus();
                        return false;
                    }
                }

                if (!IsNumber(oldPwdStr))
                {
                    MessageBox.Show("�������Ϊ0-9�����֣�", "��ʾ");
                    this.txtnewpwd.Text = string.Empty;
                    this.txtconfirmpwd.Text = string.Empty;
                    this.txtoldpwd.Text = string.Empty;
                    this.txtoldpwd.Focus();
                    return false;
                }

                
            }
            if (!IsNumber(newPwdStr))
            {
                MessageBox.Show("�������Ϊ0-9�����֣�", "��ʾ");
                this.txtnewpwd.Text = string.Empty;
                this.txtconfirmpwd.Text = string.Empty;
                this.txtoldpwd.Text = string.Empty;
                this.txtnewpwd.Focus();
                return false;
            }
            if(newPwdStr.Length!= 6)
            {
                MessageBox.Show("�������Ϊ6λ��Ч���֣����������룡", "��ʾ");
                this.txtnewpwd.Text = string.Empty;
                this.txtconfirmpwd.Text = string.Empty;
                this.txtoldpwd.Text = string.Empty;
                this.txtnewpwd.Focus();
                return false;
            }

            return true;
        }


        /// <summary>
        /// ����
        /// </summary>
        private void Confirm()
        {
            if (this.isVerifyPSW)
            {
                string psw = this.txtoldpwd.Text.Trim();
                if (account.PassWord != psw)
                {
                    MessageBox.Show("���벻��ȷ�����������룡");
                }
                else
                {
                    this.FindForm().DialogResult = DialogResult.OK;

                }
                return;

            }
            if (!ValidPwd()) return;
            if (!IsEdit)
            {
                this.PwStr = this.txtnewpwd.Text;
                this.FindForm().DialogResult = DialogResult.OK;
            }
            else
            {
                if (UpdatePWd(true) == 1)
                {
                    this.FindForm().DialogResult = DialogResult.OK;
                }
            }
        }

        /// <summary>
        /// �޸��ʻ�����
        /// </summary>
        /// <returns>1�ɹ� -1 ʧ��</returns>
        protected virtual int UpdatePWd(bool isEditPWD)
        {
            //�����ʻ�������Ϣ
            HISFC.Models.RADT.PatientInfo patient = radtIntegrate.QueryComPatientInfo(account.CardNO);
            if (patient == null)
            {
                MessageBox.Show("���һ�����Ϣʧ�ܣ�");
                return -1;
            }
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            //�޸��ʻ�����
            int resultValue = accountManager.UpdatePassWordByCardNO(account.ID, this.txtnewpwd.Text.Trim());
            if (resultValue < 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("�޸��ʻ�����ʧ�ܣ�"+ accountManager.Err, "����");
                return -1;
            }
            #region �ʻ�������¼
            HISFC.Models.Account.AccountRecord accountRecord = new FS.HISFC.Models.Account.AccountRecord();
            //������Ϣ
            accountRecord.AccountNO = this.account.ID;//�ʺ�
            accountRecord.Patient = patient;//���￨��
            accountRecord.FeeDept.ID = (accountManager.Operator as FS.HISFC.Models.Base.Employee).Dept.ID;//���ұ���
            accountRecord.Oper.ID = accountManager.Operator.ID;//����Ա
            accountRecord.OperTime = accountManager.GetDateTimeFromSysDateTime();//����ʱ��
            accountRecord.IsValid = true;//�Ƿ���Ч
            accountRecord.OperType.ID = (int)HISFC.Models.Account.OperTypes.EditPassWord;
            accountRecord.AccountType.ID = "ALL";// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}
            //�����ʻ�������¼
            resultValue = accountManager.InsertAccountRecordEX(accountRecord);
            if (resultValue < 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("�����ʻ�������¼ʧ�ܣ�"+accountManager.Err, "����");
                return -1;
            }
           
            #endregion
            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show("�޸�����ɹ���");
            if (isEditPWD)// {74621CF6-D26C-44aa-87DC-C68D0867BAC5}
            {
                //PrintOperRecipe(accountRecord);
            }
            return 1;
        }

        /// <summary>
        /// ��ӡע������ƾ֤
        /// </summary>
        private void PrintOperRecipe(HISFC.Models.Account.AccountRecord tempAccountRecord)
        {
            IPrintOperRecipe Iprint = FS.FrameWork.WinForms.Classes.
            UtilInterface.CreateObject(this.GetType(), typeof(IPrintOperRecipe)) as IPrintOperRecipe;
            if (Iprint == null)
            {
                MessageBox.Show("��ά����ӡƱ�ݣ����Ҵ�ӡƱ��ʧ�ܣ�");
                return;
            }
            Iprint.SetValue(tempAccountRecord);
            Iprint.Print();
        }

        /// <summary>
        /// ��֤�ַ����Ƿ�Ϊ����
        /// </summary>
        /// <param name="checkStr">Ч����ַ���</param>
        /// <returns></returns>
        private bool IsNumber(string checkStr)
        {
            bool bl = true;
            foreach (char c in checkStr.ToCharArray())
            {
                if (!char.IsNumber(c))
                {
                    bl = false;
                    break;
                }
            }
            return bl;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
            if (keyData == Keys.Escape)
            {
                this.FindForm().Close();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        #endregion

        #region �¼�
        private void ucEditPassWord_Load(object sender, EventArgs e)
        {
            this.FindForm().Text = "�ʻ������޸�";
            if (isEdit)
            {
                if (isValidoldPwd)
                {
                    this.txtoldpwd.Enabled = true;
                    this.ActiveControl = this.txtoldpwd;
                }
                else
                {
                    this.txtoldpwd.Enabled = false;
                    if (account != null)
                    {
                        this.txtoldpwd.Text = account.PassWord;
                    }
                    this.ActiveControl = this.txtnewpwd;
                }
            }
            else
            {
                this.txtoldpwd.Enabled = false;
                this.ActiveControl = this.txtnewpwd;
            }
            if (this.isVerifyPSW)
            {
                this.FindForm().Text = "�ʻ�������֤";
                this.btDefaultPsw.Visible = false;
                this.neuLabel4.Text = "ע�����벻��Ϊ��,����Ϊ0-9��6λ��Ч����\n�������������Ϊ��000000";
                this.txtoldpwd.Enabled = true;
                this.txtnewpwd.Enabled = false;
                this.txtconfirmpwd.Enabled = false;
                this.btDefaultPsw.Visible = true;
                this.neuLabel1.Text = "�������룺";
            }

            this.txtoldpwd.Focus();
        }

        private void btcancel_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }

        private void btok_Click(object sender, EventArgs e)
        {
            Confirm();
        }

        private void txtconfirmpwd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                this.Confirm();
        }
        #endregion

        #region IInterfaceContainer ��Ա

        public Type[] InterfaceTypes
        {
            get 
            {
                return new Type[] { typeof(HISFC.BizProcess.Interface.Account.IPrintOperRecipe)};
            }
        }

        #endregion

        private void btDefaultPsw_Click(object sender, EventArgs e)
        {// {74621CF6-D26C-44aa-87DC-C68D0867BAC5}
            this.txtnewpwd.Text = "000000";
            if (UpdatePWd(false) < 0)
            {
                MessageBox.Show("��������ʧ�ܣ�");
                this.txtnewpwd.Text = "";
                return;
            }
            this.txtnewpwd.Text = "";
            this.account.PassWord = "000000";
        }

        private void txtoldpwd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)// {5B3B503C-8CF5-415b-89EB-C11A4FEE8A19}
            {
                if (this.isVerifyPSW)
                {
                    this.btok_Click(null, null);
                }
                else
                {
                    this.txtnewpwd.Focus();
                }
            }
        }
    }
}
