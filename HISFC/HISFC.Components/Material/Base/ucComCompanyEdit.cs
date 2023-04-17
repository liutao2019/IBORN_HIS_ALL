using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Function;
using FarPoint.Win.Spread;
using FS.FrameWork.Management;

namespace FS.HISFC.Components.Material.Base
{
    /// <summary>		
    /// ucComCompany��ժҪ˵����<br></br>
    /// [��������: ������˾ά��]<br></br>
    /// [�� �� ��: ��־��]<br></br>
    /// [����ʱ��: 2007-11-28<br></br>
    /// 
    /// </summary>
    public partial class ucComCompanyEdit : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucComCompanyEdit()
        {
            InitializeComponent();
            //this.Init();
        }

        #region �����

        /// <summary>
        /// ƴ����
        /// </summary>
        private FS.HISFC.BizLogic.Manager.Spell mySpell = new FS.HISFC.BizLogic.Manager.Spell();

        /// <summary>
        /// ��Ӧ����Ϣ��
        /// </summary>
        private FS.HISFC.BizLogic.Material.ComCompany comCompany = new FS.HISFC.BizLogic.Material.ComCompany();

        /// <summary>
        /// ���ݱ�
        /// </summary>
        private DataTable dt = new DataTable();

        /// <summary>
        /// ά���Ĺ�˾��� 
        /// </summary>
        private CompanyKind kind = CompanyKind.���ʳ���ʹ��;

        /// <summary>
        /// ά���Ĺ�˾���� 
        /// </summary>
        private ucComCompany.CompanyType type = ucComCompany.CompanyType.������˾;

        public ucComCompany.CompanyType Type
        {
            get { return type; }
            set { type = value; }
        }

        /// <summary>
        /// ά���Ĺ�˾��� 
        /// </summary>
        public string companyID;

        /// <summary>
        /// ������ʵ��
        /// </summary>
        public FS.HISFC.Models.Material.MaterialCompany company;

        /// <summary>
        /// ��������
        /// </summary>
        private string inputType = "N";

        public delegate void SaveInput(FS.HISFC.Models.Material.MaterialCompany company);

        public event SaveInput MyInput;

        #endregion

        #region ����

        /// <summary>
        /// �������� Update/Insert
        /// </summary>
        public string InputType
        {
            get
            {
                return this.inputType;
            }
            set
            {
                this.inputType = value;
            }
        }


        /// <summary>
        /// �Ƿ���ֻ��״̬ �������޸�
        /// </summary>
        public bool ReadOnly
        {
            get
            {
                return this.btnSave.Visible;
            }
            set
            {
                this.btnSave.Visible = !value;
            }
        }

        /// <summary>
        /// �ؼ��ڲ�������Ʒʵ��
        /// </summary>
        public FS.HISFC.Models.Material.MaterialCompany Company
        {
            get
            {
                this.GetCompany();
                return this.company;
            }
            set
            {
                if (value == null)
                {
                    this.company = new FS.HISFC.Models.Material.MaterialCompany();
                }
                else
                {
                    this.company = value;
                }

                this.SetCompany();
            }
        }

        #endregion

        #region ����

        /// <summary>
        /// ���������Ч��
        /// </summary>
        /// <returns></returns>
        private bool IsValid()
        {
            if (this.txtName.TextLength == 0)
            {
                MessageBox.Show("��˾���Ʋ���Ϊ��!");
                this.txtName.Focus();
                return false;
            }
            if (this.txtCoporation.TextLength == 0)
            {
                MessageBox.Show("��˾���˲���Ϊ��!");
                this.txtCoporation.Focus();
                return false;
            }
            if (this.txtAddress.TextLength == 0)
            {
                MessageBox.Show("��˾��ַ����Ϊ��!");
                this.txtAddress.Focus();
                return false;
            }
            if (this.txtSpellCode.TextLength == 0)
            {
                MessageBox.Show("ƴ������Ϊ��!");
                this.txtSpellCode.Focus();
                return false;
            }
            if (this.txtUserCode.Text.Length == 0)
            {
                MessageBox.Show("�Զ�����벻��Ϊ��!");
                this.txtUserCode.Focus();
                return false;
            }
            if (this.txtWbCode.Text.Length == 0)
            {
                MessageBox.Show("����벻��Ϊ��!");
                this.txtWbCode.Focus();
                return false;
            }
            if (NConvert.ToDecimal(this.txtActualRate.Text) >= NConvert.ToDecimal("1.0000"))
            {
                MessageBox.Show("��������Ӧ��С��1����0!");
                this.txtActualRate.Focus();
                return false;
            }
            else
                return true;

        }

        /// <summary>
        /// ��companyʵ����ȡ����,����ؼ�
        /// </summary>
        private void SetCompany()
        {
            FS.FrameWork.Management.DataBaseManger data = new FS.FrameWork.Management.DataBaseManger();
            if (this.company == null)
            {
                this.company = new FS.HISFC.Models.Material.MaterialCompany();
            }

            this.txtId.Text = this.company.ID;

            if (this.inputType == "I")
            {
                try
                {
                    this.company.ID = comCompany.GetMaxCompanyID(((int)kind).ToString());
                    this.txtId.Text = this.company.ID;
                }
                catch { }
            }
            this.txtName.Text = company.Name;                 //��˾����
            this.txtCoporation.Text = company.Coporation;     //��˾����
            this.txtAddress.Text = company.Address;           //��˾��ַ
            this.txtTelCode.Text = company.TelCode;           //��˾�绰
            this.txtFaxCode.Text = company.FaxCode;          //��˾����
            this.txtNetAddress.Text = company.NetAddress;     //��˾��ַ
            this.txtEMail.Text = company.EMail;               //��˾����
            this.txtLinkMan.Text = company.LinkMan;           //��ϵ��
            this.txtLinkMail.Text = company.LinkMail;         //��ϵ������
            this.txtLinkTel.Text = company.LinkTel;          //��ϵ�˵绰
            this.txtGMPInfo.Text = company.GMPInfo;          //GMP��Ϣ
            this.txtGSPInfo.Text = company.GSPInfo;          //GSP��Ϣ
            this.txtISOInfo.Text = company.ISOInfo;          //ISO��Ϣ
            this.txtSpellCode.Text = company.SpellCode;       //ƴ����
            this.txtWbCode.Text = company.WBCode;          //�����
            this.txtUserCode.Text = company.UserCode;        //�Զ�����
            this.txtOpenBank.Text = company.OpenBank;       //��������
            this.txtOpenAccounts.Text = company.OpenAccounts; //�����ʺ�
            this.txtActualRate.Text = company.ActualRate.ToString();     //�Ӽ���
            this.txtMemo.Text = company.Memo;               //��ע		

            if (company.BusinessDate == DateTime.MinValue)
                this.dtBusinessDate.Value = DateTime.Now;
            else
                this.dtBusinessDate.Value = company.BusinessDate;
            if (company.ManageDate == DateTime.MinValue)
                this.dtManageDate.Value = DateTime.Now;
            else
                this.dtManageDate.Value = company.ManageDate;
            if (company.DutyDate == DateTime.MinValue)
                this.dtDutyDate.Value = DateTime.Now;
            else
                this.dtDutyDate.Value = company.DutyDate;
            if (company.OrgDate == DateTime.MinValue)
                this.dtOrgDate.Value = DateTime.Now;
            else
                this.dtOrgDate.Value = company.OrgDate;

            if (company.IsValid)
                this.chbIsValid.Checked = true;
            else
                this.chbIsValid.Checked = false;

        }

        /// <summary>
        /// �ӿؼ���ȡ����,����companyʵ��
        /// </summary>
        private void GetCompany()
        {
            FS.FrameWork.Management.DataBaseManger data = new FS.FrameWork.Management.DataBaseManger();
            string operCode = ((FS.HISFC.Models.Base.Employee)data.Operator).ID;
            if (this.company == null)
            {
                this.company = new FS.HISFC.Models.Material.MaterialCompany();
            }

            this.company.ID = this.txtId.Text;

            if (this.inputType == "I")
            {
                try
                {
                    this.company.ID = comCompany.GetMaxCompanyID(((int)kind).ToString());
                    this.txtId.Text = this.company.ID;
                }
                catch { }
            }
            company.Kind = ((int)this.kind).ToString();
            company.Name = this.txtName.Text;                 //��˾����
            company.Coporation = this.txtCoporation.Text;   //��˾����
            company.Address = this.txtAddress.Text;           //��˾��ַ
            company.TelCode = this.txtTelCode.Text;           //��˾�绰
            company.FaxCode = this.txtFaxCode.Text;          //��˾����
            company.NetAddress = this.txtNetAddress.Text;     //��˾��ַ
            company.EMail = this.txtEMail.Text;               //��˾����
            company.LinkMan = this.txtLinkMan.Text;           //��ϵ��
            company.LinkMail = this.txtLinkMail.Text;         //��ϵ������
            company.LinkTel = this.txtLinkTel.Text;          //��ϵ�˵绰
            company.GMPInfo = this.txtGMPInfo.Text;          //GMP��Ϣ
            company.GSPInfo = this.txtGSPInfo.Text;          //GSP��Ϣ
            company.ISOInfo = this.txtISOInfo.Text;          //ISO��Ϣ
            company.SpellCode = this.txtSpellCode.Text;       //ƴ����
            company.WBCode = this.txtWbCode.Text;          //�����
            company.UserCode = this.txtUserCode.Text;        //�Զ�����
            company.Type = ((int)this.type).ToString();  //��˾����
            company.OpenBank = this.txtOpenBank.Text;       //��������
            company.OpenAccounts = this.txtOpenAccounts.Text; //�����ʺ�
            company.ActualRate = Convert.ToDecimal(this.txtActualRate.Text);     //�Ӽ���
            if (this.chbIsValid.Checked)
                company.IsValid = true; //��Ч
            else
                company.IsValid = false;
            company.Memo = this.txtMemo.Text;               //��ע		
            company.Oper.ID = operCode;
            company.OperTime = comCompany.GetDateTimeFromSysDateTime();
            //{9E7FB328-89B3-4f43-A417-2EC3ACFC7093}
            //�����ж��Ƿ�����С��
            //if (company.BusinessDate == DateTime.MinValue)
            //{
            //    company.BusinessDate = DateTime.Now;
            //}
            //else
            //{
                company.BusinessDate = NConvert.ToDateTime(this.dtBusinessDate.Value);
            //}
            //if (company.ManageDate == DateTime.MinValue)
            //{
            //    company.ManageDate = DateTime.Now;
            //}
            //else
            //{
                company.ManageDate = NConvert.ToDateTime(this.dtManageDate.Value);
            //}
            //if (company.DutyDate == DateTime.MinValue)
            //{
            //    company.DutyDate = DateTime.Now;
            //}
            //else
            //{
                company.DutyDate = NConvert.ToDateTime(this.dtDutyDate.Value);
            //}
            //if (company.OrgDate == DateTime.MinValue)
            //{
            //    company.OrgDate = DateTime.Now;
            //}
            //else
            //{
                company.OrgDate = NConvert.ToDateTime(this.dtOrgDate.Value);
            //}

        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <returns>�ɹ�: ����1,ʧ��: ���� -1</returns>
        public int Save()
        {
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            comCompany.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            int parm = 1;

            FS.HISFC.Models.Material.MaterialCompany matCompany = new FS.HISFC.Models.Material.MaterialCompany();

            matCompany = this.Company;
            switch (this.InputType)
            {
                case "U":
                    parm = comCompany.UpdateCompany(matCompany);
                    break;
                case "I":
                    parm = comCompany.InsertCompany(matCompany);
                    break;
                case "N":
                    return -1;
            }

            if (parm == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(this.comCompany.Err);
                return -1;
            }
            else
            {

                FS.FrameWork.Management.PublicTrans.Commit();

                if (this.inputType == "I")
                {
                    this.MyInput(matCompany);
                }

                MessageBox.Show("����ɹ���", "��ʾ");

                return 1;
            }
        }

        /// <summary>
        /// ��տؼ�
        /// </summary>
        public void Reset()
        {
            foreach (System.Windows.Forms.Control c in this.Controls)
            {
                if (c.GetType() == typeof(System.Windows.Forms.GroupBox))
                {
                    foreach (System.Windows.Forms.Control crl in c.Controls)
                    {
                        if (crl.GetType() == typeof(FS.FrameWork.WinForms.Controls.NeuComboBox))
                            continue;
                        if (crl.GetType() != typeof(System.Windows.Forms.Label) && crl.GetType() != typeof(System.Windows.Forms.CheckBox))
                        {
                            crl.Tag = "";
                            crl.Text = "";
                        }
                    }
                }
            }
        }

        #endregion

        #region �¼�

        /// <summary>
        /// �س��Զ�����ƴ���롢�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }

            return base.ProcessDialogKey(keyData);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //���������Ч��
            if (!this.IsValid()) return;

            //����
            if (this.Save() == -1) return;


            switch (this.InputType)
            {
                case "U":
                    this.InputType = "N";
                    this.FindForm().Close();
                    break;
                case "I":
                    this.InputType = "N";
                    this.FindForm().Close();
                    break;
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }

        private void txtName_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                FS.HISFC.Models.Base.Spell spCode = new FS.HISFC.Models.Base.Spell();

                spCode = (FS.HISFC.Models.Base.Spell)mySpell.Get(this.txtName.Text.Trim());
                if (spCode.SpellCode.Length > 8)
                {
                    this.txtSpellCode.Text = spCode.SpellCode.Substring(0, 8);
                }
                else
                {
                    this.txtSpellCode.Text = spCode.SpellCode;
                }
                if (spCode.WBCode.Length > 8)
                {
                    this.txtWbCode.Text = spCode.WBCode.Substring(0, 8);
                }
                else
                {
                    this.txtWbCode.Text = spCode.WBCode;
                }
            }

        }
        #endregion

        #region ö����
        /// <summary>
        /// ά����˾���
        /// </summary>
        public enum CompanyKind
        {
            ҩ��ʹ��,
            ���ʳ���ʹ��
        }

        ///// <summary>
        ///// ά����˾����
        ///// </summary>
        //public enum CompanyType
        //{
        //    ��������,
        //    ������˾
        //}


        #endregion

    }
}
