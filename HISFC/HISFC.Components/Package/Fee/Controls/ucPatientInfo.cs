using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.Fee.Outpatient;
using FS.FrameWork.Management;
using FS.HISFC.Models.Registration;
using FarPoint.Win.Spread;
using System.Xml;

namespace HISFC.Components.Package.Fee.Controls
{
    /// <summary>
    /// ������
    /// </summary>
    /// <returns></returns>
    public delegate int DelegateVoidSet();
    public delegate int DelegateHashtableSet(Hashtable hsTable);
    public delegate int DelegateArrayListSet(ArrayList al);
    public delegate int DelegateStringSet(string str);
    public delegate int DelegateTripleStringSet(string str, string str1, string str2);
    public delegate int DelegateBoolPointSet(bool flag, Point point);
    public delegate int DelegateKeysSet(Keys keyData);
    public delegate int DelegateBoolSet(bool bo);

    public delegate ArrayList DelegateArrayListGetString(string str);
    public delegate bool DelegateBoolGet();
    public delegate Hashtable DelegateHashtableGet();
    public delegate ArrayList DelegatArraListeGet();
    public delegate string DelegateStringGet();


    public partial class ucPatientInfo : UserControl//, FS.HISFC.BizProcess.Interface.MedicalPackage.IPatientInfomation
    {
        #region ���Ʋ���

        /// <summary>
        /// �ײ��շѶദ���Ƿ�Ĭ��ȫѡ��1��0��
        /// </summary>
        private bool isSelectedAllRecipe = false;

        #endregion

        #region ҵ����
        /// <summary>
        /// ���Ʋ���ҵ���
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        /// <summary>
        /// ����ҵ���
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// �ײͷ���ҵ����
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.MedicalPackage.Fee.Package feePackageIntegrate = new FS.HISFC.BizProcess.Integrate.MedicalPackage.Fee.Package();

        /// <summary>
        /// ��ͬ��λҵ���
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.PactUnitInfo pactManager = new FS.HISFC.BizLogic.Fee.PactUnitInfo();

        /// <summary>
        /// ����������
        /// </summary>
        FS.HISFC.BizLogic.Manager.Constant constantMgr = new FS.HISFC.BizLogic.Manager.Constant();

        /// <summary>
        /// �˻�������
        /// </summary>
        FS.HISFC.BizLogic.Fee.Account accountMgr = new FS.HISFC.BizLogic.Fee.Account();

        #endregion

        #region ����
        /// <summary>
        /// ���߻�����Ϣ
        /// </summary>
        protected FS.HISFC.Models.RADT.PatientInfo patientInfo = null;

        /// <summary>
        /// ���߻�����Ϣ
        /// </summary>
        public FS.HISFC.Models.RADT.PatientInfo PatientInfo
        {
            get { return this.patientInfo; }
            set
            {
                this.patientInfo = value;
                this.SetPatientInfo();
                this.PatientInfoChange();
                this.SetRecipeInfo();
            }
        }

        public void SetDiscountInfo(decimal levelDiscount, string levelID, string levelName)
        {

            string accountInfo = "��ǰ����Ϊ��{0}���û�,��Ա�ۿ�Ϊ{1}��";

            //{5B7CD01E-2DDB-499d-9F49-DA8A2F7E0AAC}
            if (levelDiscount.Equals(1))
            {
                accountInfo = "��ǰ����Ϊ��{0}���û�,�޻�Ա�ۿ�";
                accountInfo = string.Format(accountInfo, levelName);
            }
            else
            {
                accountInfo = string.Format(accountInfo, levelName, (levelDiscount * 10).ToString().Trim());
            }
            this.lbAccountInfo.Text = accountInfo;
        }


        /// <summary>
        /// ��ע// {F53BD032-1D92-4447-8E20-6C38033AA607}
        /// </summary>
        public string Memo
        {
            get { return this.tbMemo.Text; }
            set
            {
                this.tbMemo.Text = value;
            }
        }

        //{0904C52F-CD4D-48ae-963C-ACD592A173AE}
        /// <summary>
        /// �Ƿ�¼�ײ�
        /// </summary>
        public bool IsBLPackage
        {
            get { return this.chkBL.Checked; }
        }

        /// <summary>
        /// ��ǰ��Ա����Ϣ
        /// </summary>
        private FS.HISFC.Models.Account.AccountCard accountCardInfo = null;

        /// <summary>
        /// ��Ա����Ϣ
        /// </summary>
        public FS.HISFC.Models.Account.AccountCard AccountCardInfo
        {
            get { return this.accountCardInfo; }
            set { this.accountCardInfo = value; }
        }
        #endregion

        #region ί��

        /// <summary>
        /// ���û�����Ϣ
        /// </summary>
        public event DelegateVoidSet PatientInfoChange;

        /// <summary>
        /// ��ȡ���۵��б�
        /// </summary>
        public event DelegatArraListeGet GetPatientRecipeList;

        /// <summary>
        /// ����һ�Ż��۵�
        /// </summary>
        public event DelegateStringGet NewPatientRecipe;

        /// <summary>
        /// ɾ��һ�Ż��۵�
        /// </summary>
        public event DelegateStringSet DelPatientRecipe;

        /// <summary>
        /// ѡ��Ļ��۵������˸ı�
        /// </summary>
        public event DelegateVoidSet CurrentRecipeChange;

        /// <summary>
        /// {FAE56BB8-F958-411f-9663-CC359D6D494B}
        /// �Ƿ�ʹ�û�Ա�ۿ�
        /// </summary>
        public event DelegateBoolSet UseLevelDiscount;

        #endregion

        /// <summary>
        /// �ײ��շѽ���
        /// </summary>
        public ucPatientInfo()
        {
            InitializeComponent();
        }


        #region �ڲ�����

        /// <summary>
        /// ��ʼ������
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        private void InitControlParams()
        {
            this.isSelectedAllRecipe = this.controlParamIntegrate.GetControlParam<bool>("PM0001", false, false);
        }

        /// <summary>
        /// ��ʼ���ؼ�
        /// </summary>
        /// <returns></returns>
        private int InitControls()
        {
            try
            {
                this.cmbSex.IsListOnly = true;
                this.cmbConsultant.IsListOnly = true;
                this.cmbDept.IsListOnly = true;

                //��ʼ������
                ArrayList deptList = this.managerIntegrate.GetDepartment(FS.HISFC.Models.Base.EnumDepartmentType.C);
                if (deptList == null)
                {
                    MessageBox.Show("��ʼ�������б����!" + this.managerIntegrate.Err);

                    return -1;
                }
                ArrayList alDeptInPatient = this.managerIntegrate.GetDepartment(FS.HISFC.Models.Base.EnumDepartmentType.I);
                if (deptList == null)
                {
                    MessageBox.Show("��ʼ�������б����!" + this.managerIntegrate.Err);

                    return -1;
                }
                deptList.AddRange(alDeptInPatient);
                this.cmbDept.AddItems(deptList);

                //��ʼ���Ա�
                ArrayList sexListTemp = new ArrayList();
                ArrayList sexList = new ArrayList();
                sexListTemp = FS.HISFC.Models.Base.SexEnumService.List();
                FS.HISFC.Models.Base.Spell spell = null;
                foreach (FS.FrameWork.Models.NeuObject neuObj in sexListTemp)
                {
                    spell = new FS.HISFC.Models.Base.Spell();
                    if (neuObj.ID == "M")
                    {
                        spell.ID = neuObj.ID;
                        spell.Name = neuObj.Name;
                        spell.Memo = neuObj.Memo; 
                        spell.UserCode = "1";
                    }
                    else if (neuObj.ID == "F")
                    {
                        spell.ID = neuObj.ID;
                        spell.Name = neuObj.Name;
                        spell.Memo = neuObj.Memo;
                        spell.UserCode = "2";
                    }
                    else if (neuObj.ID == "O")
                    {
                        spell.ID = neuObj.ID;
                        spell.Name = neuObj.Name;
                        spell.Memo = neuObj.Memo;
                        spell.UserCode = "3";
                    }
                    else
                    {
                        spell.ID = neuObj.ID;
                        spell.Name = neuObj.Name;
                        spell.Memo = neuObj.Memo;
                    }
                    sexList.Add(spell);
                }
                this.cmbSex.AddItems(sexList);

                //��ʼ��ҽ���б�����һ���޹���ҽ��
                ArrayList consultantList = new ArrayList();
                //consultantList = this.managerIntegrate.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.D);
                consultantList = constantMgr.GetList("ConsultantList");// {3B757263-9BE5-4e5a-9AD2-0815E9A210C7}
                if (consultantList == null)
                {
                    MessageBox.Show("��ʼ���ͷ��б����!" + this.managerIntegrate.Err);

                    return -1;
                }
                this.cmbConsultant.AddItems(consultantList);
            }
            catch (Exception ex)
            {
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// ����¼�
        /// </summary>
        /// <returns></returns>
        private void AddEvents()
        {
            this.tbCardNO.KeyDown += new KeyEventHandler(tbQuery_KeyDown);
            //{8659FDB2-4200-475c-83B6-37092AD86D7D}
            this.tbName.KeyDown += new KeyEventHandler(tbQuery_KeyDown);
            this.tbPhone.KeyDown += new KeyEventHandler(tbQuery_KeyDown);
            this.fpRecipe.ButtonClicked += new EditorNotifyEventHandler(fpRecipe_ButtonClicked);
            this.fpRecipe.CellClick += new CellClickEventHandler(fpRecipe_CellClick);
            this.addRecipe.Click += new EventHandler(addRecipe_Click);
            this.delRecipe.Click += new EventHandler(delRecipe_Click);

            this.cmbConsultant.SelectedValueChanged += new EventHandler(Info_SelectedValueChanged);
            this.cmbDept.SelectedValueChanged += new EventHandler(Info_SelectedValueChanged);
            //{FAE56BB8-F958-411f-9663-CC359D6D494B}
            this.chkLevelDiscount.CheckedChanged += new EventHandler(chkLevelDiscount_CheckedChanged);
        }

        

        /// <summary>
        /// ɾ���¼�
        /// </summary>
        /// <returns></returns>
        private void DelEvents()
        {

            //{FAE56BB8-F958-411f-9663-CC359D6D494B}
            this.chkLevelDiscount.CheckedChanged -= new EventHandler(chkLevelDiscount_CheckedChanged);
            //{8659FDB2-4200-475c-83B6-37092AD86D7D}
            this.tbName.KeyDown -= new KeyEventHandler(tbQuery_KeyDown);
            this.tbPhone.KeyDown -= new KeyEventHandler(tbQuery_KeyDown);
            this.tbCardNO.KeyDown -= new KeyEventHandler(tbQuery_KeyDown);
            this.fpRecipe.ButtonClicked -= new EditorNotifyEventHandler(fpRecipe_ButtonClicked);
            this.fpRecipe.CellClick -= new CellClickEventHandler(fpRecipe_CellClick);
            this.addRecipe.Click -= new EventHandler(addRecipe_Click);
            this.delRecipe.Click -= new EventHandler(delRecipe_Click);

            this.cmbConsultant.SelectedValueChanged -= new EventHandler(Info_SelectedValueChanged);
            this.cmbDept.SelectedValueChanged -= new EventHandler(Info_SelectedValueChanged);
        }

        /// <summary>
        /// ���û�����Ϣ
        /// </summary>
        private bool SetPatientInfo()
        {
            try
            {
                this.DelEvents();
                if (this.patientInfo == null || this.patientInfo.PID.CardNO == "")
                {
                    this.tbCardNO.SelectAll();
                    this.tbCardNO.Focus();
                    this.tbCardNO.Text = string.Empty;
                    this.tbName.Text = string.Empty;
                    this.cmbSex.Tag = string.Empty;
                    this.tbAge.Text = string.Empty;
                    this.tbPhone.Text = string.Empty;
                    this.cmbConsultant.Tag = string.Empty;
                    this.cmbDept.Tag = string.Empty;
                    this.tbMemo.Text = string.Empty;
                    this.lbAccountInfo.Text = string.Empty;

                    this.chkBL.CheckState = CheckState.Unchecked;
                    this.tbCardNO.ReadOnly = false;
                    this.tbName.ReadOnly = false;
                    this.tbPhone.ReadOnly = false;
                    throw new Exception();
                }
                //{C22E94C1-78A0-493c-8FFB-5BB0BF51D6AE����˻���� �������
                FS.HISFC.BizLogic.Fee.Account accountMgr = new FS.HISFC.BizLogic.Fee.Account();
                FS.HISFC.Models.Account.Account account = accountMgr.GetAccountByCardNoEX(patientInfo.PID.CardNO);
                string CKDonateAmout = accountMgr.GetAccountDetailCK(patientInfo.PID.CardNO);
                string PTVacancy = accountMgr.GetAccountDetailPTYE(patientInfo.PID.CardNO);
                string PTDonateAmout = accountMgr.GetAccountDetailPT(patientInfo.PID.CardNO);
                if (string.IsNullOrEmpty(CKDonateAmout))
                {
                    CKDonateAmout = "0";
                }
                string vacancyInfo = "��ͨ�˻����:{0},��ͨ�������:{1},�����������:{2}" ;
                if (account != null)
                {
                    //this.txtVacancy.Text = string.Format(vacancyInfo, account.BaseVacancy.ToString("F2"), account.DonateVacancy.ToString("F2"), CKDonateAmout);
                    this.txtVacancy.Text = string.Format(vacancyInfo, PTVacancy, PTDonateAmout, CKDonateAmout);
                }
                else
                {

                    this.txtVacancy.Text = string.Format(vacancyInfo, "0", "0", CKDonateAmout);
                }

                this.tbCardNO.Text = this.accountCardInfo.Patient.PID.CardNO;
                this.tbName.Text = patientInfo.Name;
                this.cmbSex.Tag = patientInfo.Sex.ID;
                this.tbAge.Text = this.accountMgr.GetAge(patientInfo.Birthday);
                this.tbPhone.Text = this.patientInfo.PhoneHome;
                this.patientInfo.Pact.User01 = this.accountCardInfo.AccountLevel.ID;
                //{816EBD83-A112-479c-82E6-98D4EA5321F2}
                this.patientMemo.Text = this.patientInfo.Memo;
            }
            catch
            {
            }
            this.AddEvents();

            return true;
        }

        /// <summary>
        /// ���û��۵���Ϣ
        /// </summary>
        private void SetRecipeInfo()
        {
            this.fpRecipe_Sheet1.RowCount = 0;

            if (this.patientInfo == null)
                return;
            //ͨ�������ȡ���۵��б�
            ArrayList recipeList = this.GetPatientRecipeList();
            if (recipeList == null)
                return;

            for (int i = 0; i < recipeList.Count; i++)
            {
                FS.FrameWork.Models.NeuObject recipe = recipeList[i] as FS.FrameWork.Models.NeuObject;
                this.fpRecipe_Sheet1.AddRows(this.fpRecipe_Sheet1.RowCount, 1);
                this.fpRecipe_Sheet1.Cells[this.fpRecipe_Sheet1.RowCount - 1, 0].Value = (i == 0);
                this.fpRecipe_Sheet1.Cells[this.fpRecipe_Sheet1.RowCount - 1, 1].Value = recipe.ID;
                this.fpRecipe_Sheet1.Cells[this.fpRecipe_Sheet1.RowCount - 1, 2].Value = recipe.Name;
                this.fpRecipe_Sheet1.Rows[this.fpRecipe_Sheet1.RowCount - 1].Tag = recipe;

                if (i == 0)
                {
                    this.cmbConsultant.Tag = recipe.User02;
                    this.cmbDept.Tag = recipe.User03;
                }
            }

            this.CurrentRecipeChange();
        }

        #endregion

        #region �¼�

        public void setCardNO(string cardNO)
        {
            if (string.IsNullOrEmpty(cardNO))
            {
                return;
            }
            this.tbCardNO.Text = cardNO;
            KeyEventArgs tmp = new KeyEventArgs(Keys.Enter);
            this.tbQuery_KeyDown(this.tbCardNO, tmp);
        }
        /// <summary>
        /// ���￨�Żس�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbQuery_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TextBox queryControl = sender as TextBox;
                string QueryStr = queryControl.Text.Trim();

                this.Clear();

                //������Ĭ�Ͻ��в�ȫ
                if (queryControl.Name == "tbMedicalNO")
                {
                    QueryStr = QueryStr.PadLeft(10, '0');
                    this.tbCardNO.ReadOnly = true;
                    this.tbName.ReadOnly = true;
                    this.tbPhone.ReadOnly = true;
                }
                else if (queryControl.Name == "tbCardNO") //���Ų�ѯ
                {
                    this.tbName.ReadOnly = true;
                    this.tbPhone.ReadOnly = true;
                }
                else if (queryControl.Name == "tbName")
                {
                    this.tbCardNO.ReadOnly = true; 
                    this.tbPhone.ReadOnly = true;
                }
                else if (queryControl.Name == "tbPhone")
                {
                    this.tbCardNO.ReadOnly = true;
                    this.tbName.ReadOnly = true;
                }

                if (string.IsNullOrEmpty(QueryStr))
                {
                    this.tbCardNO.ReadOnly = false;
                    this.tbName.ReadOnly = false;
                    this.tbPhone.ReadOnly = false;
                    return;
                }

                if (queryControl.Name == "tbName" || queryControl.Name == "tbPhone")
                {
                    FS.HISFC.Components.Common.Forms.frmQueryPatientByConditions frm = new FS.HISFC.Components.Common.Forms.frmQueryPatientByConditions();
                    //{51C02BB2-BBC3-45c2-B985-5E14ECEB0943}
                    frm.IsFliterUnValid = true;
                    if (queryControl.Name == "tbName")
                    {
                        frm.QueryByName(QueryStr);
                    }
                    else
                    {
                        frm.QueryByPhone(QueryStr);
                    }
                    frm.ShowDialog();

                    if (frm.DialogResult == DialogResult.OK)
                    {
                        QueryStr = frm.PatientInfo.PID.CardNO;
                    }
                    else
                    {
                        this.tbCardNO.ReadOnly = false;
                        this.tbName.ReadOnly = false;
                        this.tbPhone.ReadOnly = false;
                        return;
                    }
                }

                //�����Ų�ѯ
                if (queryControl.Name == "tbMedicalNO" ||
                    queryControl.Name == "tbName" ||
                    queryControl.Name == "tbPhone")
                {
                    //{0214F8DB-402F-4b19-A854-A7ECF225630B}
                    System.Collections.Generic.List<FS.HISFC.Models.Account.AccountCard> cardList = accountMgr.GetMarkList(QueryStr, "ALL", "1");
                    if (cardList == null || cardList.Count < 1)
                    {
                        MessageBox.Show("δ��ѯ�����ߣ�");
                        this.tbCardNO.ReadOnly = false;
                        this.tbName.ReadOnly = false;
                        this.tbPhone.ReadOnly = false;
                        return;
                    }
                    this.AccountCardInfo = cardList[cardList.Count - 1];
                }
                else if ((sender as TextBox).Name == "tbCardNO")//���Ų�ѯ
                {
                    this.AccountCardInfo = accountMgr.GetAccountCard(QueryStr, "Account_CARD");
                    if (this.AccountCardInfo == null || (this.AccountCardInfo.MarkStatus != FS.HISFC.Models.Account.MarkOperateTypes.Begin))
                    {
                        MessageBox.Show("���Ų����ڻ����Ѿ����ϣ�");
                        this.tbCardNO.ReadOnly = false;
                        this.tbName.ReadOnly = false;
                        this.tbPhone.ReadOnly = false;
                        return;
                    }
                }

                this.PatientInfo = accountMgr.GetPatientInfoByCardNO(this.AccountCardInfo.Patient.PID.CardNO);
                this.patientInfo.Pact.User01 = this.accountCardInfo.AccountLevel.ID;

                if (this.PatientInfo == null || string.IsNullOrEmpty(this.PatientInfo.PID.CardNO))
                {
                    MessageBox.Show("δ��ѯ���ڻ��ߣ�");
                    this.tbCardNO.ReadOnly = false;
                    this.tbName.ReadOnly = false;
                    this.tbPhone.ReadOnly = false;
                    return;
                }
                
                //{51C02BB2-BBC3-45c2-B985-5E14ECEB0943}
                if (this.PatientInfo.Memo == "���ϻ���" || this.patientInfo.State == "2")
                {
                    this.Clear();
                    MessageBox.Show("�û�����Ϣ�Ѿ����ϣ��볢��ͨ��������绰���м�����");
                }
            }
        }

        /// <summary>
        /// ����һ�Ŵ�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addRecipe_Click(object sender, EventArgs e)
        {
            if (this.PatientInfo == null)
            {
                MessageBox.Show("��������ߣ�");
                return;
            }

            foreach (Row row in this.fpRecipe_Sheet1.Rows)
            {
                this.fpRecipe_Sheet1.Cells[row.Index, 0].Value = false;
            }

            string newRecipe = this.NewPatientRecipe();

            if (string.IsNullOrEmpty(newRecipe))
            {
                MessageBox.Show("��ȡ���۵��Ŵ���");
            }

            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = newRecipe;
            obj.Name = "0.0";
            obj.User02 = this.cmbConsultant.Tag.ToString();
            obj.User03 = this.cmbDept.Tag.ToString();

            //��������
            this.fpRecipe_Sheet1.Rows.Add(this.fpRecipe_Sheet1.RowCount, 1);
            this.fpRecipe_Sheet1.Rows[this.fpRecipe_Sheet1.RowCount - 1].Tag = obj;
            this.fpRecipe_Sheet1.Cells[this.fpRecipe_Sheet1.RowCount - 1, 0].Value = true;
            this.fpRecipe_Sheet1.Cells[this.fpRecipe_Sheet1.RowCount - 1, 1].Value = newRecipe;
            this.fpRecipe_Sheet1.Cells[this.fpRecipe_Sheet1.RowCount - 1, 2].Value = "0.00";
            this.fpRecipe_Sheet1.SetActiveCell(this.fpRecipe_Sheet1.RowCount - 1, 0, false);

            this.CurrentRecipeChange();
        }

        /// <summary>
        /// ɾ��һ�Ŵ�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void delRecipe_Click(object sender, EventArgs e)
        {
            if (this.fpRecipe_Sheet1.ActiveRow == null)
            {
                MessageBox.Show("��ѡ��һ�Ż��۵���");
                return;
            }

            int row = this.fpRecipe_Sheet1.ActiveRowIndex;
            string recipeNO = this.fpRecipe_Sheet1.Cells[row, 1].Value.ToString();
            if (MessageBox.Show("�Ƿ�ɾ��[" + recipeNO + "]���۵���Ϣ?", "��ʾ!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (this.DelPatientRecipe(recipeNO) >= 0)
                {
                    this.fpRecipe_Sheet1.Rows.Remove(row, 1);
                }

                if (this.fpRecipe_Sheet1.RowCount > 0)
                {
                    this.fpRecipe_Sheet1.Cells[this.fpRecipe_Sheet1.ActiveRowIndex, 0].Value = true;
                }
                this.CurrentRecipeChange();
            }
        }

        /// <summary>
        /// ������۵��ؼ�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpRecipe_ButtonClicked(object sender, EditorNotifyEventArgs e)
        {
            if (this.fpRecipe_Sheet1.RowCount == 0)
            {
                return;
            }
            this.fpRecipe_Sheet1.ActiveRowIndex = e.Row;

            if (e.Column == 0)//���ѡ���
            {
                FS.FrameWork.Models.NeuObject tmp = this.fpRecipe_Sheet1.Rows[e.Row].Tag as FS.FrameWork.Models.NeuObject;
                this.cmbConsultant.Tag = tmp.User02;
                this.cmbDept.Tag = tmp.User03;

                this.CurrentRecipeChange();
            }
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void fpRecipe_CellClick(object sender, CellClickEventArgs e)
        {
            if (e.Column == 0)
                return;
            this.fpRecipe_Sheet1.ActiveRowIndex = e.Row;
            foreach (Row row in this.fpRecipe_Sheet1.Rows)
            {
                if (row.Index != e.Row)
                {
                    this.fpRecipe_Sheet1.Cells[row.Index, 0].Value = false;
                }
                else
                {
                    this.fpRecipe_Sheet1.Cells[row.Index, 0].Value = true;
                    FS.FrameWork.Models.NeuObject tmp = row.Tag as FS.FrameWork.Models.NeuObject;

                    this.cmbConsultant.Tag = tmp.User02;
                    this.cmbDept.Tag = tmp.User03;
                }
            }
            this.CurrentRecipeChange();
        }

        private void Info_SelectedValueChanged(object sender, EventArgs e)
        {
            if (this.patientInfo != null)
            {
                this.patientInfo.Pact.User02 = this.cmbConsultant.Tag.ToString();
                this.patientInfo.Pact.User03 = this.cmbDept.Tag.ToString();
            }
        }

        /// <summary>
        /// {FAE56BB8-F958-411f-9663-CC359D6D494B}
        /// �Ƿ����ܻ�Ա�ۿ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void chkLevelDiscount_CheckedChanged(object sender, EventArgs e)
        {
            this.UseLevelDiscount(this.chkLevelDiscount.Checked);
        }

        #endregion

        #region �ⲿ����
        /// <summary>
        /// ��ʼ��
        /// </summary>
        public int Init()
        {
            this.InitControlParams();
            this.AddEvents();
            this.InitControls();
            return 1;
        }

        /// <summary>
        /// ���
        /// </summary>
        public void Clear()
        {
            this.PatientInfo = null;
            this.AccountCardInfo = null;
        }

        /// <summary>
        /// ��ȡ���ڲ����Ļ��۵�
        /// </summary>
        /// <returns></returns>
        public ArrayList GetCurrentRecipeList()
        {
            ArrayList feeRecipesList = new ArrayList();
            foreach (Row row in this.fpRecipe_Sheet1.Rows)
            {
                if ((bool)this.fpRecipe_Sheet1.Cells[row.Index, 0].Value)
                {
                    FS.FrameWork.Models.NeuObject obj = row.Tag as FS.FrameWork.Models.NeuObject;
                    feeRecipesList.Add(obj);
                }
            }
            return feeRecipesList;
        }

        /// <summary>
        /// ��ȡ��ǰ���ۺ�
        /// </summary>
        /// <returns></returns>
        public string GetCurrentRecipe()
        {
            if (this.patientInfo == null)
            {
                return string.Empty;
            }

            if (this.fpRecipe_Sheet1.RowCount == 0)
            {
                this.addRecipe_Click(null, null);
            }

            int row = this.fpRecipe_Sheet1.ActiveRow.Index;

            if ((bool)this.fpRecipe_Sheet1.Cells[row, 0].Value == false)
            {
                return string.Empty;
            }

            return this.fpRecipe_Sheet1.Cells[row, 1].Value.ToString();
        }

        /// <summary>
        /// ��ȡ�ֳ���ѯ��Ϣ{07ECD432-CA49-42f8-AFCB-596D3B670CB6}
        /// </summary>
        /// <returns></returns>
        public string GetConsultant()
        {
            string consultant = cmbConsultant.Tag.ToString ();
            return consultant;
        }

        /// <summary>
        /// ���»��۵��۸���Ϣ
        /// </summary>
        /// <param name="hsRecipes"></param>
        public void SetRecipeCost(Hashtable hsRecipes)
        {
            Hashtable tmp = new Hashtable();

            foreach (string key in hsRecipes.Keys)
            {
                double totcost = 0;
                ArrayList packages = hsRecipes[key] as ArrayList;
                foreach (FS.HISFC.Models.MedicalPackage.Fee.Package curpackage in packages)
                {
                    if (curpackage.PackageInfo.PackageClass == "1")
                    {
                        continue;
                    }
                    totcost += Double.Parse(curpackage.Package_Cost.ToString());
                }
                tmp.Add(key, totcost);
            }

            foreach (Row row in this.fpRecipe_Sheet1.Rows)
            {
                try
                {
                    this.fpRecipe_Sheet1.Cells[row.Index, 2].Value = tmp[this.fpRecipe_Sheet1.Cells[row.Index, 1].Value.ToString()].ToString();
                }
                catch
                { }
            }
        }
        #endregion
    }
}
