using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.Function;
using FS.FrameWork.Management;
using System.Collections;
using FS.FrameWork.WinForms.Classes;
using System.Text.RegularExpressions;

namespace FS.HISFC.Components.InpatientFee.Maintenance
{
    /// <summary>
    /// �������á����տؼ�
    /// �����ߣ�
    /// </summary>
    public partial class ucCardPanel : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucCardPanel()
        {
            InitializeComponent();
        }

        #region ����

        /// <summary>
        /// ������
        /// </summary>
        private FS.FrameWork.Models.NeuObject cardKind = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// ��Աҵ���
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// ����ҵ���
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.DiscountCardLogic discountCardLogic = new FS.HISFC.BizLogic.Fee.DiscountCardLogic();

        /// <summary>
        /// ��Ա������ҵ���
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.EmployeeFinanceGroup employeeFinanceGroupManager = new FS.HISFC.BizLogic.Fee.EmployeeFinanceGroup();

        /// <summary>
        /// ��Ա����
        /// </summary>
        ArrayList personList = new ArrayList();

        /// <summary>
        /// ��Ա������
        /// </summary>
        ArrayList finaceGroupList = new ArrayList();

        /// <summary>
        /// toolBarService
        /// </summary>
        protected FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        private string currentStartCode = string.Empty;
        private long currentCodeNums = 0;
        private bool isMouseCheck = true;

        /// <summary>
        /// ��ǰ��¼����Ա
        /// </summary>
        private FS.HISFC.Models.Base.Employee curOper;

        #endregion

        #region ����

        private FS.FrameWork.Models.NeuObject CurrCardKind
        {
            get
            {
                if (this.cmbCardKind.Tag.ToString() == string.Empty)
                {
                    this.cmbCardKind.SelectedIndex = 0;
                }
                cardKind.ID = this.cmbCardKind.Tag.ToString();
                cardKind.Name = this.cmbCardKind.Text.Trim();

                return cardKind;
            }
        }

        /// <summary>
        /// ȡ�õ�ǰ��Ա����
        /// </summary>
        /// <param name="personCode"></param>
        /// <returns></returns>
        protected string GetPersonName(string personCode)
        {
            string PersonName = string.Empty;
            if (personList != null)
            {
                foreach (FS.HISFC.Models.Base.Employee p in personList)
                {
                    if (p.ID == personCode)
                    {
                        PersonName = p.Name;
                        break;
                    }
                }
            }
            return PersonName;
        }

        /// <summary>
        /// ȡ�õ�ǰ��Ա����
        /// </summary>
        /// <param name="personName"></param>
        /// <returns></returns>
        private string GetPersonCode(string personName)
        {
            string PersonCode = string.Empty;
            if (personList != null)
            {
                foreach (FS.HISFC.Models.Base.Employee p in personList)
                {
                    if (p.Name == personName)
                    {
                        PersonCode = p.ID;
                        break;
                    }
                }
            }
            return PersonCode;
        }

        /// <summary>
        /// ȡ�õ�ǰ��Ա���ͣ������жϲ�������Ա�����Ƿ��ں�̨�б仯
        /// </summary>
        /// <param name="personCode"></param>
        /// <returns></returns>
        protected string GetPersonType(string personCode)
        {
            FS.HISFC.Models.Base.Employee employeeInfo = managerIntegrate.GetEmployeeInfo(personCode);

            return employeeInfo.EmployeeType.ID.ToString();
        }

        //{BA6D9596-064F-4dd7-B76A-A98FFD58EA65}
        [Category("�ؼ�����"), Description("�Ƿ������޸�������ʼ��,true:�������޸ģ�false:�����޸�"), DefaultValue(true)]
        public bool IsModifyBegionNo
        {
            set
            {
                this.txtStart.ReadOnly = value;
            }
            get
            {
                return this.txtStart.ReadOnly;
            }
        }

        private bool isShowAll = false;

        [Category("�ؼ�����"), Description("�Ƿ���ʾȫ����Ա,Ĭ��false"), DefaultValue(false)]
        public bool IsShowAll
        {
            set
            {
                this.isShowAll = value;
            }
            get
            {
                return this.isShowAll;
            }
        }

        #endregion

        #region ˽�з���

        /// <summary>
        /// ��ʼ��
        /// </summary>
        protected virtual void Init()
        {
            //��ǰ����Ա
            this.curOper = (FS.HISFC.Models.Base.Employee)this.discountCardLogic.Operator;

            this.cmbCardKind.AddItems((new FS.HISFC.BizLogic.Manager.Constant().GetList("GetCardKind")));

            if (this.cmbCardKind.alItems == null || this.cmbCardKind.alItems.Count == 0)
            {
                MessageBox.Show("���ڳ���ά����ά���վݵ����");
                return;
            }
            this.CleanUpListView();

            this.personList = this.managerIntegrate.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.F);

            finaceGroupList = this.employeeFinanceGroupManager.QueryFinaceGroupIDAndNameAll();

            this.LoadPersons(this.lstvPerson);

            this.cmbCardKind.SelectedIndex = 0;
            this.cmbCardState.SelectedIndex = 0;
            this.neuTabControl1.SelectedIndex = 0;

        }

        /// <summary>
        /// �����Ա�б�ListView
        /// </summary>
        protected virtual void CleanUpListView()
        {
            this.lstvPerson.Items.Clear();
            if (this.isShowAll || this.curOper.IsManager)
            {
                this.lstvPerson.Items.Add(new ListViewItem(new string[] { "ȫ��" }));
                this.lstvPerson.Items[0].Selected = true;
            }
        }

        /// <summary>
        /// ������Ա
        /// </summary>
        /// <param name="listview"></param>
        protected virtual void LoadPersons(ListView listview)
        {

            if (personList == null || personList.Count <= 0)
            {
                return;
            }
            foreach (FS.HISFC.Models.Base.Employee p in personList)
            {
                //������ʾ����
                if (this.isShowAll || curOper.IsManager || p.ID.Equals(curOper.ID))
                {
                    ListViewItem item = new ListViewItem(new string[] { p.ID, p.Name });
                    listview.Items.Add(item);
                }
            }
        }

        /// <summary>
        /// ���ز�������Ϣ
        /// </summary>
        /// <param name="listview"></param>
        protected virtual void LoadGroups(ListView listview)
        {
            if (finaceGroupList == null)
            {
                return;
            }
            if (finaceGroupList.Count <= 0)
                return;
            foreach (FS.HISFC.Models.Fee.FinanceGroup group in finaceGroupList)
            {
                ListViewItem item = new ListViewItem(new string[] { group.ID, group.Name });
                listview.Items.Add(item);
            }
        }

        /// <summary>
        /// ������Ա��������һ��ʵ��
        /// </summary>
        /// <param name="personID"></param>
        /// <param name="personName"></param>
        /// <returns></returns>
        private FS.HISFC.Models.Fee.DiscountCard CreateDiscountCardItem(string personID, string personName)
        {
            FS.HISFC.Models.Fee.DiscountCard discountCard = new FS.HISFC.Models.Fee.DiscountCard();

            discountCard.GetDate = System.DateTime.Now;
            discountCard.GetPersonCode = personID;
            discountCard.CardKind = CurrCardKind.ID;
            discountCard.CardName = CurrCardKind.Name;
            discountCard.StartNo = currentStartCode.ToString();
            discountCard.EndNo = FS.FrameWork.Public.String.AddNumber(currentStartCode, currentCodeNums - 1);
            discountCard.UsedNo = FS.FrameWork.Public.String.AddNumber(currentStartCode, -1);
            discountCard.UsedState = "0";
            discountCard.IsPub = "0";
            discountCard.OperCode = this.curOper.ID;
            discountCard.OperDate = System.DateTime.Now;
            discountCard.QTY = currentCodeNums.ToString();

            currentStartCode = FS.FrameWork.Public.String.AddNumber(currentStartCode, currentCodeNums);
            return discountCard;
        }

        /// <summary>
        /// ��֤��ʼ�š���ֹ�š���������Ч�ԡ�
        /// </summary>
        /// <returns></returns>
        protected virtual bool ValidateNumValid()
        {
            long startcode = 0;
            long endcode = 0;
            long num = 0;
            if (this.ckbStartNO.Checked)
            {
                if (this.txtStart.Text.Trim() == "")
                {
                    MessageBox.Show("��������ʼ�ţ�", "��ʾ", MessageBoxButtons.OK);
                    return false;
                }

                if (this.txtEndNO.Text.Trim() == "")
                {
                    MessageBox.Show("��������ֹ�ţ�", "��ʾ", MessageBoxButtons.OK);
                    return false;
                }
                if (this.txtQty.Text.Trim() == "")
                {
                    MessageBox.Show("��������ȡ��������", "��ʾ", MessageBoxButtons.OK);
                    return false;
                }

                #region ���

                try
                {
                    startcode = FS.FrameWork.Public.String.GetNumber(this.txtStart.Text.Trim());// Convert.ToInt64(this.txtStart.Text.Trim());
                }
                catch (FormatException formatException)
                {
                    MessageBox.Show("��ʼ�ű����Ǵ���0������!" + formatException.Message, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                catch (OverflowException overflowException)
                {
                    MessageBox.Show("��ʼ�����Ϊ12λ!" + overflowException.Message, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("������������ʼ��!" + ex.Message, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }

                try
                {
                    endcode = FS.FrameWork.Public.String.GetNumber(this.txtEndNO.Text.Trim());//  Convert.ToInt64(this.txtEndNO.Text.Trim());
                }
                catch (FormatException formatException)
                {
                    MessageBox.Show("��ֹ�ű����Ǵ���0������!" + formatException.Message, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                catch (OverflowException overflowException)
                {
                    MessageBox.Show("��ֹ�����Ϊ12λ!" + overflowException.Message, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("������������ֹ��!" + ex.Message, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }

                try
                {
                    num = Convert.ToInt64(this.txtQty.Text.Trim());
                }
                catch (FormatException formatException)
                {
                    MessageBox.Show("��ȡ���������Ǵ���0������!" + formatException.Message, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                catch (OverflowException overflowException)
                {
                    MessageBox.Show("��ȡ�������Ϊ12λ!" + overflowException.Message, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("������������ȡ����!" + ex.Message, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }

                #endregion

                if (endcode < startcode)
                {
                    MessageBox.Show("��ֹ��Ӧ���ڻ������ʼ�ţ�", "��ʾ", MessageBoxButtons.OK);

                    return false;
                }

                if (this.txtEndNO.Text.Trim().Length > 12)
                {
                    MessageBox.Show("��Ʊ�����������������룡", "��ʾ");
                    txtQty.Focus();
                    txtQty.SelectAll();
                    return false;
                }


                if (startcode + num != endcode + 1)
                {

                    this.txtQty.Text = ((long)(endcode - startcode + 1)).ToString();
                }

            }

            if (discountCardLogic.DiscountCardIsValid(this.txtStart.Text.Trim(), this.txtEndNO.Text.Trim(), this.CurrCardKind.ID) == false)
            {
                MessageBox.Show("����Ŀ������󣬿����ѱ���ȡ�����������룡", "��ʾ", MessageBoxButtons.OK);
                return false;
            }


            return true;
        }

        /// <summary>
        /// ���lstvPerson�����е�ѡ��
        /// </summary>
        protected virtual void PersonViewClear()
        {
            foreach (ListViewItem item in this.lstvPerson.Items)
            {
                if (item.Checked)
                    item.Checked = false;
            }
        }

        /// <summary>
        /// ɾ��lstvGetPerson�����Ա��Ϣ
        /// </summary>
        /// <param name="id"></param>
        protected virtual void DeletePersonList(string id)
        {
            if (this.lstvGetPerson.Items.Count <= 0)
                return;
            foreach (ListViewItem item in this.lstvGetPerson.Items)
            {
                if (item.SubItems[1].Text == id)
                {
                    this.lstvGetPerson.Items.Remove(item);
                    return;
                }
            }
        }

        /// <summary>
        /// ��lstvGetPerson�����lstvPersonѡ�е���Ա��Ϣ
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        protected virtual void AddPersonList(string id, string name)
        {
            this.lstvGetPerson.Items.Add(new ListViewItem(new string[] { name, id }));
        }

        /// <summary>
        /// ���ÿ�
        /// </summary>
        private void GetDiscountCard()
        {
            this.neuTabControl1.SelectedIndex = 0;
            if (ValidateNumValid() == false)
                return;
            if (this.lstvGetPerson.Items.Count <= 0)
            {
                MessageBox.Show("��ѡ��Ҫ���俨�ŵ���Ա��", "��ʾ", MessageBoxButtons.OK);

                return;
            }

            GenerateDiscountCard();
        }

        /// <summary>
        /// ����PersonListBox�����Ա�����ɣ������档������ϡ�
        /// </summary>
        protected virtual void GenerateDiscountCard()
        {
            string cardKind = string.Empty;

            cardKind = this.CurrCardKind.ID;

            ArrayList discountCards = new ArrayList();
            //��ʼ����
            currentStartCode = this.txtStart.Text.Trim();
            //��ֹ����
            currentCodeNums = Convert.ToInt64(this.txtQty.Text.Trim());
            //��Ա����б䶯��Ա��
            string changedPersons = string.Empty;

            foreach (ListViewItem item in this.lstvGetPerson.Items)
            {
                FS.HISFC.Models.Fee.DiscountCard discountCard = CreateDiscountCardItem(item.SubItems[1].Text, item.Text);
                discountCards.Add(discountCard);
            }

            if (changedPersons != string.Empty)
            {
                MessageBox.Show("ϵͳ��������Ա�Ѿ������շ�Ա�������µ�½���ý���:" + changedPersons);
                return;
            }
 
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.discountCardLogic.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            int result = 1;
            foreach (FS.HISFC.Models.Fee.DiscountCard info in discountCards)
            {

                if (this.discountCardLogic.InsertDiscountCard(info) == -1)
                {
                    result = -1;
                    break;
                }
            }

            if (result == 1)
            {
                FS.FrameWork.Management.PublicTrans.Commit();
                AddDataTofpSpread2(discountCards);
                MessageBox.Show("����ɹ���", "��ʾ", MessageBoxButtons.OK);

                PersonViewClear();

                return;
            }
            else
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("����ʧ�ܣ�" + this.discountCardLogic.Err);
            }

            MessageBox.Show("����ʧ�ܣ�", "��ʾ", MessageBoxButtons.OK);
            return;

        }

        /// <summary>
        /// ��ѯ
        /// </summary>
        protected virtual void SearchDiscountCardBy()
        {
            try
            {
                if (this.neuTabControl1.SelectedIndex != 1)
                    this.neuTabControl1.SelectedIndex = 1;

                string type = string.Empty;
                type = this.CurrCardKind.ID;
                
                if (this.txtpersonCode.Text != "")
                {
                    if (GetPersonName(this.txtpersonCode.Text.Trim()) == "")
                    {
                        MessageBox.Show("�������Ա���Ų�����", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;

                    }
                    ArrayList List = this.discountCardLogic.QueryDiscountCard(this.txtpersonCode.Text.Trim(), type);
                    SetDataToGrid(List);

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        /// <summary>
        /// ���б�����ʾ
        /// </summary>
        /// <param name="list"></param>
        protected virtual void SetDataToGrid(ArrayList list)
        {
            if (fpSpread1_Sheet1.Rows.Count > 0)
                fpSpread1_Sheet1.Rows.Count = 0;//Clear();
            int i = 0;
            try
            {
                string user = string.Empty;
                string state = string.Empty;

                int viewState = 2;
                if (this.cmbCardState.SelectedIndex == 0)
                {
                    viewState = 2;
                }
                else if (this.cmbCardState.SelectedIndex == 1)
                {
                    viewState = 1;
                }
                else
                    if (this.cmbCardState.SelectedIndex == 2)
                    {
                        viewState = 0;
                    }
                    else
                        viewState = -1;

                foreach (FS.HISFC.Models.Fee.DiscountCard info in list)
                {
                    if (viewState != 2 && Convert.ToInt32(info.UsedState) != viewState)
                    {
                        continue;
                    }

                    fpSpread1_Sheet1.AddRows(i, 1);
                    fpSpread1_Sheet1.SetValue(i, 0, info.StartNo.ToString().PadLeft(12, '0')); //��ʼ��
                    fpSpread1_Sheet1.SetValue(i, 1, info.EndNo.ToString().PadLeft(12, '0'));   //��ֹ��

                    if (Convert.ToInt32(info.UsedState) == 0)
                    {
                        state = "δ��";
                        fpSpread1_Sheet1.SetValue(i, 2, "");
                    }
                    else if (Convert.ToInt32(info.UsedState) == 1)
                    {
                        state = "����";
                        fpSpread1_Sheet1.SetValue(i, 2, info.UsedNo.ToString().PadLeft(12, '0'));
                    }
                    else
                    {
                        state = "����";
                        fpSpread1_Sheet1.SetValue(i, 2, info.UsedNo.ToString().PadLeft(12, '0'));
                    }


                   
                    fpSpread1_Sheet1.SetValue(i, 3, state);
                    fpSpread1_Sheet1.SetValue(i, 4, info.GetDate.ToString());
                    if (user == null || user == string.Empty)
                    {
                        user = info.GetPersonCode;

                    }

                    fpSpread1_Sheet1.SetValue(i, 5, user);

                    fpSpread1_Sheet1.SetValue(i, 5, info.GetPersonCode);
                    fpSpread1_Sheet1.SetValue(i, 6, this.cmbCardKind.Text);

                    info.CardKind = this.CurrCardKind.ID;

                    fpSpread1_Sheet1.SetValue(i, 7, this.GetPersonName(info.GetPersonCode));
                    
                    fpSpread1_Sheet1.Rows[i].Tag = info;

                    int lastNo = 0;
                    try
                    {
                        if (!string.IsNullOrEmpty(info.UsedNo) && !string.IsNullOrEmpty(info.EndNo))
                        {
                            int useNo = Convert.ToInt32(info.UsedNo.Substring(3, info.UsedNo.Length - 3));
                            int endNo = Convert.ToInt32(info.EndNo.Substring(3, info.EndNo.Length - 3));
                            lastNo = endNo - useNo;
                        }

                        fpSpread1_Sheet1.SetValue(i, 8, lastNo);
                    }
                    catch (Exception)
                    {
                        
                        //throw;
                    }
                    ++i;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        /// <summary>
        /// ���б���ʾ�ɹ�����ķ�Ʊ
        /// </summary>
        /// <param name="List"></param>
        private void AddDataTofpSpread2(ArrayList List)
        {
            try
            {
                foreach (FS.HISFC.Models.Fee.DiscountCard info in List)
                {
                    fpSpread2_Sheet1.Rows.Add(0, 1);
                    fpSpread2_Sheet1.Cells[0, 0].Text = this.GetPersonName(info.GetPersonCode);
                    fpSpread2_Sheet1.Cells[0, 1].Text = info.CardName;
                    fpSpread2_Sheet1.Cells[0, 2].Text = info.StartNo;
                    fpSpread2_Sheet1.Cells[0, 3].Text = info.EndNo;
                    fpSpread2_Sheet1.Cells[0, 4].Text = info.GetDate.ToString();
                }
            }

            catch (Exception ee)
            {
                string Error = ee.Message;
            }
        }

        /// <summary>
        /// ���յ�ʵ��
        /// </summary>
        /// <returns></returns>
        private FS.HISFC.Models.Fee.DiscountCard GetReturnBackDiscountCard()
        {
            return fpSpread1_Sheet1.Rows[fpSpread1_Sheet1.ActiveRowIndex].Tag as FS.HISFC.Models.Fee.DiscountCard;
        }

        /// <summary>
        /// �жϱ���е��Ƿ���Ի���
        /// </summary>
        /// <returns></returns>
        protected virtual bool CanReturnBack()
        {
            FS.HISFC.Models.Fee.DiscountCard info = fpSpread1_Sheet1.Rows[fpSpread1_Sheet1.ActiveRowIndex].Tag as FS.HISFC.Models.Fee.DiscountCard;
            if (info.UsedState == "-1")
                return false;

            return true;
        }

        /// <summary>
        /// ����
        /// </summary>
        public void ReturnBack()
        {
            if (this.neuTabControl1.SelectedIndex != 1)
                this.neuTabControl1.SelectedIndex = 1;
            if (this.fpSpread1_Sheet1.Rows.Count <= 0)
            {
                MessageBox.Show("û�п��Ի��յĿ���", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return;
            }
            if (CanReturnBack() == false)
            {
                MessageBox.Show("���Ѿ���ʹ�ã����ܻ��գ�", "��ʾ", MessageBoxButtons.OK);

                return;
            }

            FS.HISFC.Models.Fee.DiscountCard discountCardReturn = GetReturnBackDiscountCard();

            FS.HISFC.Models.Fee.DiscountCard clone = (FS.HISFC.Models.Fee.DiscountCard)discountCardReturn.Clone();
            frmDiscountCardReturn frmDiscountCardReturn = new InpatientFee.Maintenance.frmDiscountCardReturn(clone);
            DialogResult result = frmDiscountCardReturn.ShowDialog();

            if (result == DialogResult.OK)
            {
                string start = clone.StartNo;
                string discountCardStart = discountCardReturn.StartNo;
                string end = clone.EndNo;
                string discountCardEnd = discountCardReturn.EndNo;

                bool saved = true;
                if (start == discountCardStart)
                {
                    if (this.discountCardLogic.Delete(clone) == -1)
                        saved = false; ;
                }
                else
                {
                    discountCardReturn.EndNo = FS.FrameWork.Public.String.AddNumber(start, -1);
                    if (discountCardReturn.EndNo == discountCardReturn.UsedNo)
                    {
                        discountCardReturn.UsedState = "-1";
                    }
                    discountCardReturn.QTY = (Convert.ToInt32(discountCardReturn.QTY) - Convert.ToInt32(clone.QTY)).ToString();

                    if (discountCardLogic.UpdateDiscountCardUsedStateByPerson(discountCardReturn) == -1)
                        saved = false;

                }
                if (saved)
                {
                    MessageBox.Show("���ճɹ���", "��ʾ", MessageBoxButtons.OK);
                    SearchDiscountCardBy();
                }
                else
                    MessageBox.Show("����ʧ�ܣ�", "��ʾ", MessageBoxButtons.OK);


            }

        }

        /// <summary>
        /// ���toolbar��ť
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("����", "����������Ϣ", FS.FrameWork.WinForms.Classes.EnumImageList.X�½�, true, false, null);
            toolBarService.AddToolButton("����", "���տ���Ϣ", FS.FrameWork.WinForms.Classes.EnumImageList.Q���, true, false, null);

            return this.toolBarService;
        }

        /// <summary>
        /// ����toolbarservice��onprint
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnPrint(object sender, object neuObject)
        {
            this.PrintData();
            return base.OnPrint(sender, neuObject);
        }

        /// <summary>
        /// ����toolbarservice��OnPrintPreview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnPrintPreview(object sender, object neuObject)
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            //print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.Border;
            if (this.neuTabControl1.SelectedIndex == 1)
            {
                print.PrintPreview(this.neuPanel8);
            }
            else if (this.neuTabControl1.SelectedIndex == 0)
            {
                print.PrintPreview(this.neuPanel5);
            }
            return base.OnPrintPreview(sender, neuObject);
        }

        /// <summary>
        /// ����toolbarservice��export
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        public override int Export(object sender, object neuObject)
        {
            this.Exportinfo();
            return base.Export(sender, neuObject);
        }

        /// <summary>
        /// ����toolbarservice��onquery
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnQuery(object sender, object neuObject)
        {
            this.SearchDiscountCardBy();
            return base.OnQuery(sender, neuObject);
        }

        /// <summary>
        /// �������ݳ�excel
        /// </summary>
        protected virtual void Exportinfo()
        {
            try
            {
                bool ret = false;
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "Excel |.xls";
                saveFileDialog1.Title = "��������";
                saveFileDialog1.FileName = "��Ʊ";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if (saveFileDialog1.FileName != "")
                    {
                        if (this.neuTabControl1.SelectedIndex == 1)
                        {
                            //��Excel ����ʽ��������
                            ret = fpSpread1.SaveExcel(saveFileDialog1.FileName, FarPoint.Win.Spread.Model.IncludeHeaders.ColumnHeadersCustomOnly);

                        }
                        else
                        {
                            ret = fpSpread2.SaveExcel(saveFileDialog1.FileName, FarPoint.Win.Spread.Model.IncludeHeaders.ColumnHeadersCustomOnly);
                        }
                    }
                    if (ret)
                    {
                        MessageBox.Show("�ɹ���������");
                    }
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        /// <summary>
        /// ��ӡ�����ҳ��ı��
        /// </summary>
        /// <returns></returns>
        protected virtual int PrintData()
        {
            try
            {
                FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
                //print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.Border;
                if (this.neuTabControl1.SelectedIndex == 1)
                {
                    print.PrintPage(0, 0, this.neuPanel8);
                }
                else if (this.neuTabControl1.SelectedIndex == 0)
                {
                    print.PrintPage(0, 0, this.neuPanel5);
                }
                else
                {
                    MessageBox.Show("��ѡ��Ҫ��ӡ��ҳ��");
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
            return 0;
        }

        #endregion

        #region ���з���

        /// <summary>
        /// ����toolbar��ťclick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text.ToString())
            {
                case "����":
                    this.GetDiscountCard();
                    break;
                case "����":
                    this.ReturnBack();
                    break;
            }

            base.ToolStrip_ItemClicked(sender, e);
        }

        #endregion

        #region �¼�

        private void ucCardPanel_Load(object sender, EventArgs e)
        {
            this.ckbStartNO.Checked = true;
            Init();
        }

        private void cmbCardKind_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtEndNO.Text = string.Empty;
            txtQty.Text = string.Empty;
        }

        private void lstvPerson_Click(object sender, EventArgs e)
        {
            if (this.neuTabControl1.SelectedIndex == 1)
            {
                if (this.lstvPerson.Items[0].Selected && this.lstvPerson.SelectedItems[0].SubItems[0].Text == "ȫ��")
                    this.txtpersonCode.Text = "";
                else
                    this.txtpersonCode.Text = this.lstvPerson.SelectedItems[0].SubItems[0].Text;

                SearchDiscountCardBy();
            }
        }

        private void lstvPerson_DoubleClick(object sender, EventArgs e)
        {
            if (this.neuTabControl1.SelectedIndex == 1)
            {
                if (this.lstvPerson.Items[0].Selected && this.lstvPerson.SelectedItems[0].SubItems[0].Text == "ȫ��")
                    this.txtpersonCode.Text = "";
                else
                    this.txtpersonCode.Text = this.lstvPerson.SelectedItems[0].SubItems[0].Text;

                SearchDiscountCardBy();
            }
        }

        /// <summary>
        /// ��lstvPerson��ѡ��Ҫ�������Ա
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstvPerson_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (isMouseCheck == false)
                return;
            if (e.Index == 0 && (this.isShowAll || curOper.IsManager))
            {
                bool itemChecked;
                if (e.NewValue == CheckState.Checked)
                    itemChecked = true;
                else
                    itemChecked = false;

                isMouseCheck = false;
                for (int i = 1; i < this.lstvPerson.Items.Count; ++i)
                {
                    if (this.lstvPerson.Items[i].Checked != itemChecked)
                    {
                        this.lstvPerson.Items[i].Checked = itemChecked;
                    }
                }

                this.lstvGetPerson.Items.Clear();

                if (itemChecked)
                {
                    for (int i = 1; i < this.lstvPerson.Items.Count; ++i)
                    {
                        AddPersonList(this.lstvPerson.Items[i].SubItems[0].Text, this.lstvPerson.Items[i].SubItems[1].Text);
                    }
                }

                isMouseCheck = true;

            }
            else
            {
                string id = this.lstvPerson.Items[e.Index].SubItems[0].Text;

                isMouseCheck = false;
                int itemCheckedNum = 1;
                for (int i = 1; i < this.lstvPerson.Items.Count; ++i)
                {
                    if (this.lstvPerson.Items[i].Checked == true)
                        ++itemCheckedNum;

                }
                if (e.NewValue == CheckState.Checked)
                {
                    ++itemCheckedNum;
                    AddPersonList(id, this.lstvPerson.Items[e.Index].SubItems[1].Text);
                }
                else
                {
                    --itemCheckedNum;
                    DeletePersonList(id);
                }
                if (itemCheckedNum == this.lstvPerson.Items.Count)
                {
                    if (this.lstvPerson.Items[0].Checked == false)
                        this.lstvPerson.Items[0].Checked = true;
                }
                else
                {
                    if (this.lstvPerson.Items[0].Checked == true)
                        this.lstvPerson.Items[0].Checked = false;
                }

                isMouseCheck = true;
            }
        }

        private void ckbStartNO_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ckbStartNO.Checked)
            {
                this.txtStart.Enabled = true;
                this.txtEndNO.Enabled = true;
            }
            else
            {
                this.txtStart.Enabled = false;
                this.txtEndNO.Enabled = false;

                this.txtEndNO.Text = "";
            }
        }

        private void txtQty_Enter(object sender, EventArgs e)
        {
            try
            {
                if (this.txtStart.Text.Trim() != "" && this.txtQty.Text.Trim() != "")
                {
                    this.txtEndNO.Text = FS.FrameWork.Public.String.AddNumber(this.txtStart.Text.Trim(), Convert.ToInt64(this.txtQty.Text.Trim()) - 1);
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void txtQty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.txtStart.Text.Trim() != "" && this.txtQty.Text.Trim() != "")
                {

                    string strStart = txtStart.Text.Trim();
                    int index = 0;
                    for (int i = strStart.Length - 1; i >= 0; i--)
                    {
                        if (!Regex.IsMatch(strStart[i].ToString(), "^[0-9]*$"))
                        {
                            index = i;
                            break;
                        }
                    }

                    string strStr = txtStart.Text.Trim().Substring(0, strStart.Length - 1 - index);

                    this.txtEndNO.Text = strStr + FS.FrameWork.Public.String.AddNumber(this.txtStart.Text.Trim().Substring(strStart.Length - 1 - index), Convert.ToInt64(this.txtQty.Text.Trim()) - 1);
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void neuTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.neuTabControl1.SelectedIndex == 0)
            {
                this.lstvPerson.CheckBoxes = true;
            }
            else
            {
                this.lstvPerson.CheckBoxes = false;

            }
        }

        private void fpSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            ReturnBack();
        }

        private void cmbDiscountCardState_SelectedIndexChanged(object sender, EventArgs e)
        {
            SearchDiscountCardBy();
        }
        #endregion


    }
}
