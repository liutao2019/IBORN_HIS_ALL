using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.Account;
using System.Collections;

namespace FS.HISFC.Components.Account.Controls
{
    /// <summary>
    /// �ʻ���Ȩ
    /// </summary>
    public partial class ucEmpower : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucEmpower()
        {
            InitializeComponent();
        }

        #region ����
        /// <summary>
        /// ��Ȩ���￨��Ϣ
        /// </summary>
        AccountCard accountCard = null;

        /// <summary>
        /// ����Ȩ���￨��Ϣ
        /// </summary>
        AccountCard empowerAcccountcard = null;

        /// <summary>
        /// �����ʻ�ҵ���
        /// </summary>
        HISFC.BizLogic.Fee.Account accountManager = new FS.HISFC.BizLogic.Fee.Account();

        /// <summary>
        /// �ۺϹ���ҵ���
        /// </summary>
        HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        FS.FrameWork.Public.ObjectHelper IdtypeHelp = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// �ʻ�ʵ��
        /// </summary>
        HISFC.Models.Account.Account account = null;

        /// <summary>
        /// ������
        /// </summary>
        private FS.FrameWork.WinForms.Forms.ToolBarService toolbarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        /// <summary>
        /// ����ҵ���
        /// </summary>
        HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();
        #endregion

        #region ����

        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <returns></returns>
        private int Init()
        {
            try
            {
                this.ActiveControl = this.txtMarkNO;
                //������
                ArrayList al = managerIntegrate.GetConstantList("MarkType");
                this.cmbepMarkType.AddItems(al);
                this.cmbMarkType.AddItems(al);
                //֤������
                IdtypeHelp.ArrayObject = managerIntegrate.QueryConstantList("IDCard");
                return 1;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        /// <summary>
        /// ��Ȩ��ϢУ��
        /// </summary>
        /// <returns></returns>
        private bool Valid()
        {
            if (accountCard == null)
            {
                MessageBox.Show("��������Ȩ�û��ľ��￨�ţ�");
                return false;
            }
            if (accountCard.Patient == null)
            {
                MessageBox.Show("�ÿ���û�б����Ų�����Ȩ��������������Ȩ���ţ�");
                return false;
            }
            account = this.accountManager.GetAccountByMarkNo(accountCard.MarkNO);
            if (account == null)
            {
                MessageBox.Show("�ÿ��������ʻ�������Ȩ,������������Ȩ���ţ�");
                return false;
            }
            if (account.ValidState == FS.HISFC.Models.Base.EnumValidState.Invalid)
            {
                MessageBox.Show("�ÿ����ʻ��ѱ�ͣ�ò�����Ȩ��������������Ȩ���ţ�");
                return false;
            }
            return true;
        }

        /// <summary>
        /// ����Ȩ��ϢУ��
        /// </summary>
        /// <returns></returns>
        private bool EmpowerValid()
        {
            if (empowerAcccountcard == null)
            {
                MessageBox.Show("�����뱻��Ȩ�û��ľ��￨�ţ�");
                return false;
            }
            if (empowerAcccountcard.Patient == null)
            {
                MessageBox.Show("�ÿ���û�б����ţ����ܱ���Ȩ��");
                return false;
            }
            HISFC.Models.Account.Account obj = this.accountManager.GetAccountByMarkNo(empowerAcccountcard.MarkNO);
            if (obj != null)
            {
                MessageBox.Show("�ÿ��Ѵ����ʻ������ܱ���Ȩ��");

                return false;
            }
            AccountEmpower accountEmpwoer = new AccountEmpower();
            int resultValue = accountManager.QueryAccountEmpowerByEmpwoerCardNO(empowerAcccountcard.Patient.PID.CardNO, ref accountEmpwoer);
            if (resultValue < 0)
            {
                MessageBox.Show(this.accountManager.Err);
                return false;
            }
            if (resultValue > 0)
            {
                MessageBox.Show("���ʻ�����Ȩ���ܱ��ٴ���Ȩ��");
                return false;
            }
            return true;
        }

        /// <summary>
        /// ����
        /// </summary>
        protected virtual void save()
        {
            //��Ȩ��ϢУ��
            if (!Valid())
            {
                this.txtMarkNO.Text = string.Empty;
                this.txtMarkNO.Focus();
                return;
            }
            //����Ȩ��ϢУ��
            if (!EmpowerValid())
            {
                this.txtepMarkNO.Text = string.Empty;
                this.txtepMarkNO.Focus();
                return;
            }

            //��֤��Ȩ���ʻ�����
            if (!feeIntegrate.CheckAccountPassWord(accountCard.Patient)) return;

            AccountEmpower accountEmpower = new AccountEmpower();
            //����ucEmpowerInfo��������Ȩ��Ϣ
            ucEmpowerInfo uc = new ucEmpowerInfo(accountEmpower, false);
            FS.FrameWork.WinForms.Classes.Function.ShowControl(uc);
            if (uc.FindForm().DialogResult != DialogResult.OK) return;
            //������Ȩʵ��
            accountEmpower.AccountCard = accountCard;
            accountEmpower.Vacancy = accountEmpower.EmpowerLimit;
            accountEmpower.EmpowerCard = empowerAcccountcard;
            accountEmpower.ValidState = FS.HISFC.Models.Base.EnumValidState.Valid;
            accountEmpower.AccountNO = account.ID;
            accountEmpower.Oper.ID = accountManager.Operator.ID;
            accountEmpower.Oper.OperTime = accountManager.GetDateTimeFromSysDateTime();
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            accountManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            //������Ȩ��Ϣ
            if (accountManager.InsertEmpower(accountEmpower) < 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("������Ȩ�����" + accountManager.Err);
                return;
            }
            //�����ʻ���Ȩ���
            int resultValue = accountManager.UpdateAccountEmpowerFlag(accountEmpower.AccountNO);
            if (resultValue == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("�����ʻ���Ȩ��ʶ����" + accountManager.Err);
                return;
            }
            if (resultValue == 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("��Ȩ�ʻ���Ϣ�����仯��");
                return;
            }
            //������ˮ��Ϣ
            resultValue = this.InsertAccountRecord(OperTypes.Empower, accountEmpower);
            if (resultValue < 0)
            {
                MessageBox.Show("���뽻�ױ����" + accountManager.Err);
                FS.FrameWork.Management.PublicTrans.RollBack();
                return ;
            }

            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show("��Ȩ�ɹ���");
            this.ClearEmpower();
            SetEmpowerToFp(accountEmpower);
        }

        ///// <summary>
        ///// ���뽻������
        ///// </summary>
        ///// <param name="operType"></param>
        //private int InsertAccountRecord(HISFC.Models.Account.OperTypes operType,HISFC.Models.RADT.PatientInfo empowerPatient)
        //{
        //    AccountRecord accountRecord = new AccountRecord();
        //    accountRecord.AccountNO = this.account.ID;//�ʺ�
        //    accountRecord.Patient = accountCard.Patient;//���￨��
        //    accountRecord.DeptCode = (accountManager.Operator as FS.HISFC.Models.Base.Employee).Dept.ID;//���ұ���
        //    accountRecord.Oper = accountManager.Operator.ID;//����Ա
        //    accountRecord.OperTime = accountManager.GetDateTimeFromSysDateTime();//����ʱ��
        //    accountRecord.IsValid = true;//�Ƿ���Ч
        //    accountRecord.EmpowerPatient = empowerPatient;//����Ȩ���￨��
        //    accountRecord.OperType.ID = (int)operType;
        //    return accountManager.InsertAccountRecord(accountRecord);
        //}

        /// <summary>
        /// ���뽻������
        /// </summary>
        /// <param name="operType">��������</param>
        /// <param name="empowerPatient">��Ȩ��Ϣ</param>
        /// <returns>1�ɹ� -1ʧ��</returns>
        private int InsertAccountRecord(HISFC.Models.Account.OperTypes operType, HISFC.Models.Account.AccountEmpower empowerObj)
        {
            AccountRecord accountRecord = new AccountRecord();
            accountRecord.AccountNO = this.account.ID;//�ʺ�
            accountRecord.Patient = accountCard.Patient;//���￨��
            accountRecord.FeeDept.ID = (accountManager.Operator as FS.HISFC.Models.Base.Employee).Dept.ID;//���ұ���
            accountRecord.Oper.ID = accountManager.Operator.ID;//����Ա
            accountRecord.OperTime = accountManager.GetDateTimeFromSysDateTime();//����ʱ��
            accountRecord.IsValid = true;//�Ƿ���Ч
            accountRecord.EmpowerPatient = empowerObj.EmpowerCard.Patient;//empowerPatient;//����Ȩ���￨��
            accountRecord.OperType.ID = (int)operType;
            accountRecord.EmpowerCost = empowerObj.EmpowerLimit;
            return accountManager.InsertAccountRecord(accountRecord);
        }

        /// <summary>
        /// �����Ȩ��Ϣ
        /// </summary>
        private void ClearEmpower()
        {
            this.txtepMarkNO.Text = string.Empty;
            this.cmbepMarkType.Tag = string.Empty;
            this.txtepName.Text = string.Empty;
            this.txtepSex.Text = string.Empty;
            this.txtepAge.Text = string.Empty;
            this.txtIdCardNO.Text = string.Empty;
            this.txtIdCardType.Text = string.Empty;
            this.txtEpNation.Text = string.Empty;
            this.txtCountry.Text = string.Empty;
            this.txtsiNo.Text = string.Empty;
            this.empowerAcccountcard = null;
            this.txtepMarkNO.Focus();
            
        }

        /// <summary>
        /// ��ѯ��Ȩ��Ϣ
        /// </summary>
        /// <param name="accountNO">�ʺ�</param>
        protected virtual void GetEmpowerList(string accountNO)
        {
            if (this.spEmpower.Rows.Count > 0)
            {
                this.spEmpower.Rows.Remove(0, this.spEmpower.Rows.Count);
            }
            List<AccountEmpower> list = accountManager.QueryAllEmpowerByAccountNO(accountNO);
            if (list == null)
            {
                MessageBox.Show("��ѯ��Ȩ��Ϣ����" + accountManager.Err);
                return;
            }
            foreach (AccountEmpower obj in list)
            {
                SetEmpowerToFp(obj);
            }
        }

        /// <summary>
        /// ��ʾ�ʻ���Ȩ��Ϣ
        /// </summary>
        /// <param name="tempEmpwoer">��Ȩʵ��</param>
        private void SetEmpowerToFp(AccountEmpower tempEmpwoer)
        {
            int rowindex = 0;
            this.spEmpower.Rows.Add(this.spEmpower.Rows.Count, 1);
            rowindex = this.spEmpower.Rows.Count - 1;
            this.spEmpower.Cells[rowindex, 0].Text = tempEmpwoer.AccountCard.Patient.Name; //����
            this.spEmpower.Cells[rowindex, 1].Text = tempEmpwoer.EmpowerCard.Patient.Name;//����Ȩ�û�����
            this.spEmpower.Cells[rowindex, 2].Text = tempEmpwoer.EmpowerLimit.ToString(); //��Ȩ�޶�
            this.spEmpower.Cells[rowindex, 3].Text = this.GetText(tempEmpwoer.ValidState); //״̬
            if (tempEmpwoer.ValidState == FS.HISFC.Models.Base.EnumValidState.Invalid) //�Ƿ����
            {
                this.spEmpower.Rows[rowindex].BackColor = Color.Red;
            }
            //��ѯ����ԭ��Ϣ
            HISFC.Models.Base.Employee employee = managerIntegrate.GetEmployeeInfo(tempEmpwoer.Oper.ID);
            if (employee != null)
            {
                this.spEmpower.Cells[rowindex, 4].Text = employee.Name;//����ԭ����
            }
            this.spEmpower.Cells[rowindex, 5].Text = tempEmpwoer.Oper.OperTime.ToString();//����ʱ��
            this.spEmpower.Rows[rowindex].Tag = tempEmpwoer;
        }

        /// <summary>
        /// ��ʾ״̬����
        /// </summary>
        /// <param name="validState">״̬����</param>
        /// <returns>״̬����</returns>
        private string GetText(FS.HISFC.Models.Base.EnumValidState validState)
        {
            string txtStr = string.Empty;
            switch (validState)
            {
                case FS.HISFC.Models.Base.EnumValidState.Valid:
                    {
                        txtStr = "����";
                        break;
                    }
                case FS.HISFC.Models.Base.EnumValidState.Invalid:
                    {
                        txtStr  ="ȡ����Ȩ";
                        break;
                    }
                case FS.HISFC.Models.Base.EnumValidState.Ignore:
                    {
                        txtStr = "ͣ��";
                        break;
                    }
                case FS.HISFC.Models.Base.EnumValidState.Extend:
                    {
                        txtStr = "ע��";
                        break;
                    }
            }
            return txtStr;
        }

        /// <summary>
        /// ��Ȩ������ȡ����Ȩ����Ȩ��
        /// </summary>
        /// <param name="isValid">�Ƿ���Ȩ</param>
        private int EmpowerManager(AccountEmpower accountEmpower, FS.HISFC.Models.Base.EnumValidState validState)
        {
            accountEmpower.ValidState = validState;
            accountEmpower.Oper.ID = accountManager.Operator.ID;
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            accountManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            //������Ȩ��
            if (this.accountManager.UpdateEmpower(accountEmpower) < 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("������Ȩ�����");
                return -1;
            }
            //�����ʻ���Ȩ״̬
            int resultValue = accountManager.UpdateAccountEmpowerFlag(accountEmpower.AccountNO);
            if (resultValue == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("�����ʻ���Ȩ��ʶ����" + accountManager.Err);
                return -1;
            }
            if (resultValue == 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("��Ȩ�ʻ���Ϣ�����仯��");
                return -1;
            }
            //�����ʻ���ˮ��
            if ( validState == FS.HISFC.Models.Base.EnumValidState.Valid)
            {
                resultValue = this.InsertAccountRecord(OperTypes.RevertEmpower, accountEmpower);
            }
            else
            {
                resultValue = this.InsertAccountRecord(OperTypes.CancelEmpower, accountEmpower);
            }
            if (resultValue < 0)
            {
                MessageBox.Show("���뽻�ױ����"+accountManager.Err);
                FS.FrameWork.Management.PublicTrans.RollBack();
                return -1;
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            return 1;
        }

        /// <summary>
        /// ȡ����Ȩ
        /// </summary>
        protected virtual void CancelEmpower()
        {
            if (this.spEmpower.Rows.Count == 0) return;
            if (this.spEmpower.ActiveRow.Tag == null) return;
            int rowIndex = this.spEmpower.ActiveRowIndex;

            if (MessageBox.Show("�Ƿ�Ҫȡ�����û�����Ȩ��", "��ʾ", MessageBoxButtons.OKCancel) == DialogResult.Cancel) return;

            //��֤��Ȩ���ʻ�����
            if (!feeIntegrate.CheckAccountPassWord(accountCard.Patient)) return;

            AccountEmpower accountEmpower = this.spEmpower.ActiveRow.Tag as AccountEmpower;
            if (accountEmpower.ValidState == FS.HISFC.Models.Base.EnumValidState.Invalid)
            {
                MessageBox.Show("���û���ȡ����Ȩ��", "��ʾ");
                return;
            }
            if (EmpowerManager(accountEmpower, FS.HISFC.Models.Base.EnumValidState.Invalid) == 1)
            {
                MessageBox.Show("ȡ����Ȩ�ɹ���", "��ʾ");
            }
            this.spEmpower.Cells[rowIndex, 3].Text = this.GetText(accountEmpower.ValidState);
            this.spEmpower.Rows[rowIndex].Tag = accountEmpower;
            this.spEmpower.Rows[rowIndex].BackColor = Color.Red ;

        }

        /// <summary>
        /// ��Ȩ
        /// </summary>
        protected virtual void Empower()
        {
            if (this.spEmpower.Rows.Count == 0) return;
            if (this.spEmpower.ActiveRow.Tag == null) return;
            int rowIndex = this.spEmpower.ActiveRowIndex;
            if (MessageBox.Show("�Ƿ�Ҫ�Ը��û�������Ȩ��", "��ʾ", MessageBoxButtons.OKCancel) == DialogResult.Cancel) return;

            //��֤��Ȩ���ʻ�����
            if (!feeIntegrate.CheckAccountPassWord(accountCard.Patient)) return;


            AccountEmpower accountEmpower = this.spEmpower.ActiveRow.Tag as AccountEmpower;
            //�жϱ���Ȩ�û��Ƿ����ʻ�
            HISFC.Models.Account.Account obj = this.accountManager.GetAccountByMarkNo(accountEmpower.EmpowerCard.MarkNO);
            if (obj != null)
            {
                MessageBox.Show("���û��Ѵ����ʻ������ܱ���Ȩ");
                return ;
            }

            if (accountEmpower.ValidState == FS.HISFC.Models.Base.EnumValidState.Valid)
            {
                MessageBox.Show("���û�����Ȩ�����ܱ��ٴ���Ȩ", "��ʾ");
                return;
            }

            if (EmpowerManager(accountEmpower, FS.HISFC.Models.Base.EnumValidState.Valid) == 1)
            {
                MessageBox.Show("��Ȩ�ɹ���", "��ʾ");
            }
            this.spEmpower.Cells[rowIndex, 3].Text = this.GetText(accountEmpower.ValidState);
            this.spEmpower.Rows[rowIndex].Tag = accountEmpower;
            this.spEmpower.Rows[rowIndex].BackColor = Color.White;

        }

        /// <summary>
        /// �޸���Ȩ��Ϣ
        /// </summary>
        protected virtual void EditEmpowerInfo()
        {
            if (this.spEmpower.Rows.Count == 0) return;
            if (this.spEmpower.ActiveRow.Tag == null) return;
            int rowIndex = this.spEmpower.ActiveRowIndex;
            AccountEmpower accountEmpower = this.spEmpower.ActiveRow.Tag as AccountEmpower;
            if (accountEmpower == null) return;
            if (accountEmpower.ValidState == FS.HISFC.Models.Base.EnumValidState.Invalid)
            {
                MessageBox.Show("���û���ȡ����Ȩ�����ܱ༭����Ȩ��Ϣ");
                return;
            }

            //��֤����
            if (!feeIntegrate.CheckAccountPassWord(accountCard.Patient)) return;

            ucEmpowerInfo uc = new ucEmpowerInfo(accountEmpower, true);
            FS.FrameWork.WinForms.Classes.Function.ShowControl(uc);
            if (uc.FindForm().DialogResult == DialogResult.OK)
            {
                this.GetEmpowerList(account.ID);
            }
        }


        #endregion

        #region �¼�
        private void ucEmpower_Load(object sender, EventArgs e)
        {
            if (Init() < 0)
            {
                MessageBox.Show("��ʼ����Ϣʧ�ܣ�");
                return;
            }
        }

        private void txtMarkNO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                //��ȡ���￨��Ϣ
                accountCard = new FS.HISFC.Models.Account.AccountCard();
                int resultValue = accountManager.GetCardByRule(this.txtMarkNO.Text.Trim(), ref accountCard);
                if (resultValue<= 0)
                {
                    MessageBox.Show(accountManager.Err);
                    accountCard = null;
                    this.txtMarkNO.Text = string.Empty;
                    this.txtMarkNO.Focus();
                    return;
                }
                //У��
                if (!Valid())
                {
                    this.txtMarkNO.Text = string.Empty;
                    this.txtMarkNO.Focus();
                    accountCard = null;
                    return; 
                }
                
                this.txtMarkNO.Text = accountCard.MarkNO; //���￨��
                this.cmbMarkType.Tag = accountCard.MarkType.ID;//������
                this.txtName.Text = accountCard.Patient.Name; //��������
                this.txtSex.Text = accountCard.Patient.Sex.Name; //�Ա�
                this.txtAge.Text = accountManager.GetAge(accountCard.Patient.Birthday);//����
                this.txtIdCardNO.Text = accountCard.Patient.IDCard;//֤����
                
                this.txtIdCardType.Text =IdtypeHelp.GetName(accountCard.Patient.IDCardType.ID);//֤������
                FS.FrameWork.Models.NeuObject tempObj = null;
                tempObj = managerIntegrate.GetConstansObj("NATION", accountCard.Patient.Nationality.ID);
                if (tempObj != null)
                {
                    this.txtNation.Text = tempObj.Name;//����
                }
                tempObj = managerIntegrate.GetConstansObj("COUNTRY", accountCard.Patient.Country.ID);
                if (tempObj != null)
                {
                    this.txtCountry.Text = tempObj.Name;//����
                }
                tempObj = null;
                this.txtsiNo.Text = accountCard.Patient.SSN;
                this.txtepMarkNO.Focus();
                GetEmpowerList(account.ID);

            }
        }

        private void txtepMarkNO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                empowerAcccountcard = new FS.HISFC.Models.Account.AccountCard();
                int resultValue = accountManager.GetCardByRule(this.txtepMarkNO.Text.Trim(), ref empowerAcccountcard);
                if (resultValue<= 0)
                {
                    MessageBox.Show(accountManager.Err);
                    empowerAcccountcard = null;
                    this.txtepMarkNO.Text = string.Empty;
                    this.txtepMarkNO.Focus();
                    return;
                }
                if (!EmpowerValid())
                {
                    this.txtepMarkNO.Text = string.Empty;
                    this.txtepMarkNO.Focus();
                    empowerAcccountcard = null;
                    return;
                }
                this.txtepMarkNO.Text = empowerAcccountcard.MarkNO; //���￨��
                this.cmbepMarkType.Tag = empowerAcccountcard.MarkType.ID;//������
                this.txtepName.Text = empowerAcccountcard.Patient.Name; //��������
                this.txtepSex.Text = empowerAcccountcard.Patient.Sex.Name; //�Ա�
                this.txtepAge.Text = accountManager.GetAge(empowerAcccountcard.Patient.Birthday);//����
                this.txtepIdNO.Text = empowerAcccountcard.Patient.IDCard;//֤����
                this.txtedIdType.Text = IdtypeHelp.GetName(empowerAcccountcard.Patient.IDCardType.ID);//֤������
                FS.FrameWork.Models.NeuObject tempObj = null;
                tempObj = managerIntegrate.GetConstansObj("NATION", empowerAcccountcard.Patient.Nationality.ID);
                if (tempObj != null)
                {
                    this.txtEpNation.Text = tempObj.Name;//����
                }

                tempObj = managerIntegrate.GetConstansObj("COUNTRY", empowerAcccountcard.Patient.Country.ID);
                if (tempObj != null)
                {

                    this.txtepCountry.Text = tempObj.Name;//����
                }
                tempObj = null;
                this.txtepMarkNO.Text = empowerAcccountcard.MarkNO;
                this.cmbepMarkType.Tag = empowerAcccountcard.MarkType.ID;
                this.txtepsiNo.Text = empowerAcccountcard.Patient.SSN;
                
                
            }
        }

        protected override int OnSave(object sender, object neuObject)
        {
           
            return base.OnSave(sender, neuObject);
        }

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolbarService.AddToolButton("ȡ����Ȩ", "ȡ����Ȩ", FS.FrameWork.WinForms.Classes.EnumImageList.Qȡ��, true, false, null);
            toolbarService.AddToolButton("�ָ���Ȩ", "�ָ���Ȩ", FS.FrameWork.WinForms.Classes.EnumImageList.J��ɫ���, true, false, null);
            
            toolbarService.AddToolButton("��Ȩ", "��Ȩ", FS.FrameWork.WinForms.Classes.EnumImageList.QȨ��, true, false, null);
            toolbarService.AddToolButton("�޸���Ȩ��Ϣ", "�޸���Ȩ��Ϣ", FS.FrameWork.WinForms.Classes.EnumImageList.X�޸�, true, false, null);


            return toolbarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "ȡ����Ȩ":
                    {
                        this.CancelEmpower();
                        break;
                    }
                case "�ָ���Ȩ":
                    {
                        Empower();
                        break;
                    }
                case "�޸���Ȩ��Ϣ":
                    {
                        EditEmpowerInfo();
                        break;
                    }
                case "��Ȩ":
                    {
                        this.save();
                        break;
                    }
            }
            base.ToolStrip_ItemClicked(sender, e);
        }
        #endregion
    }
}
