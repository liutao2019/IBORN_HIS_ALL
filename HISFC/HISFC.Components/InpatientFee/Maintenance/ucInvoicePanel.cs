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
    /// ��Ʊ���á����տؼ�
    /// �����ߣ�����
    /// </summary>
    public partial class ucInvoicePanel : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucInvoicePanel()
        {
            InitializeComponent();
        }

        #region ����

        /// <summary>
        /// ��Ʊ����
        /// </summary>
        //{B461213F-80EB-4338-9EF4-DB3E61F9C6DF}
        //FS.HISFC.Models.Fee.InvoiceTypeEnumService minvoiceType = new FS.HISFC.Models.Fee.InvoiceTypeEnumService();
        private FS.FrameWork.Models.NeuObject minvoiceType = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// ��Աҵ���
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// ��Ʊҵ���
        /// </summary>
        //{B461213F-80EB-4338-9EF4-DB3E61F9C6DF}
        //protected FS.HISFC.BizLogic.Fee.InvoiceService invoiceServiceManager = new FS.HISFC.BizLogic.Fee.InvoiceService();
        protected FS.HISFC.BizLogic.Fee.InvoiceServiceNoEnum invoiceServiceManager = new FS.HISFC.BizLogic.Fee.InvoiceServiceNoEnum();

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

        /// <summary>
        /// ��ǰ��Ʊ����
        /// </summary>
        /// 
        //{B461213F-80EB-4338-9EF4-DB3E61F9C6DF}
        //private FS.HISFC.Models.Fee.InvoiceTypeEnumService CurrInvoiceType
        //{
        //    get
        //    {
        //        if (this.cmbInvoiceType.Tag.ToString() == string.Empty)
        //        {
        //            this.cmbInvoiceType.SelectedIndex = 0;
        //        }
        //        minvoiceType.ID = this.cmbInvoiceType.Tag.ToString();

        //        return minvoiceType;
        //    }
        //}

        private FS.FrameWork.Models.NeuObject CurrInvoiceType
        {
            get
            {
                if (this.cmbInvoiceType.Tag.ToString() == string.Empty)
                {
                    this.cmbInvoiceType.SelectedIndex = 0;
                }
                minvoiceType.ID = this.cmbInvoiceType.Tag.ToString();
                minvoiceType.Name = this.cmbInvoiceType.Text.Trim();

                return minvoiceType;
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
        [Category("�ؼ�����"), Description("�Ƿ������޸�������ʼ��Ʊ��,true:�������޸ģ�false:�����޸�"), DefaultValue(true)]
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
            this.curOper = (FS.HISFC.Models.Base.Employee)this.invoiceServiceManager.Operator;

            //{B461213F-80EB-4338-9EF4-DB3E61F9C6DF}
            //this.cmbInvoiceType.AddItems(FS.HISFC.Models.Fee.InvoiceTypeEnumService.List());
            this.cmbInvoiceType.AddItems((new FS.HISFC.BizLogic.Manager.Constant().GetList("GetInvoiceType")));

            if (this.cmbInvoiceType.alItems == null || this.cmbInvoiceType.alItems.Count == 0)
            {
                MessageBox.Show("���ڳ���ά����ά���վݵ����");
                return;
            }
            this.CleanUpListView();
            //{B461213F-80EB-4338-9EF4-DB3E61F9C6DF}
            //txtStart.Text = invoiceServiceManager.GetDefaultStartCode(this.CurrInvoiceType);
            //txtStart.Text = invoiceServiceManager.GetDefaultStartCode(this.CurrInvoiceType.ID);

            this.personList = this.managerIntegrate.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.F);

            finaceGroupList = this.employeeFinanceGroupManager.QueryFinaceGroupIDAndNameAll();

            this.cmbGetType.SelectedIndex = 0;
            this.cmbInvoiceType.SelectedIndex = 0;
            this.cmbInvoiceState.SelectedIndex = 0;
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
        /// ������Ա��������һ�����������ʵ��
        /// </summary>
        /// <param name="personID"></param>
        /// <param name="personName"></param>
        /// <returns></returns>
        private FS.HISFC.Models.Fee.Invoice CreateInvoiceItem(string personID, string personName)
        {
            FS.HISFC.Models.Fee.Invoice invoice = new FS.HISFC.Models.Fee.Invoice();

            invoice.AcceptOper.ID = personID;
            invoice.AcceptOper.Name = personName;
            invoice.BeginNO = currentStartCode.ToString();
            invoice.EndNO = FS.FrameWork.Public.String.AddNumber(currentStartCode, currentCodeNums - 1);
            invoice.UsedNO = FS.FrameWork.Public.String.AddNumber(currentStartCode, -1);
            invoice.AcceptTime = invoiceServiceManager.GetDateTimeFromSysDateTime();


            invoice.ValidState = "0";

            if (this.cmbGetType.SelectedIndex == 0)
            {
                invoice.IsPublic = false;
            }
            else
            {
                invoice.IsPublic = true;

            }

            invoice.Type.ID = CurrInvoiceType.ID;
            invoice.Type.Name = CurrInvoiceType.Name;



            currentStartCode = FS.FrameWork.Public.String.AddNumber(currentStartCode, currentCodeNums);
            return invoice;
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
                    MessageBox.Show("�����뷢Ʊ��ʼ�ţ�", "��ʾ", MessageBoxButtons.OK);
                    return false;
                }

                if (this.txtEndNO.Text.Trim() == "")
                {
                    MessageBox.Show("�����뷢Ʊ��ֹ�ţ�", "��ʾ", MessageBoxButtons.OK);
                    return false;
                }
                if (this.txtQty.Text.Trim() == "")
                {
                    MessageBox.Show("��������ȡ��Ʊ��������", "��ʾ", MessageBoxButtons.OK);
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

                //if (Convert.ToInt64(txtEndNO.Text.Trim()).ToString().Length > 12)
                //{
                //    MessageBox.Show("��Ʊ�����������������룡", "��ʾ");
                //    txtQty.Focus();
                //    txtQty.SelectAll();
                //    return false;
                //}

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

            if (invoiceServiceManager.InvoicesIsValid(this.txtStart.Text.Trim(), this.txtEndNO.Text.Trim(), CurrInvoiceType.ID) == false)
            {
                MessageBox.Show("����ķ�Ʊ�����󣬿����ѱ���ȡ�����������룡", "��ʾ", MessageBoxButtons.OK);
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
        /// ���÷�Ʊ
        /// </summary>
        private void GetInvoice()
        {
            this.neuTabControl1.SelectedIndex = 0;
            if (ValidateNumValid() == false)
                return;
            if (this.lstvGetPerson.Items.Count <= 0)
            {
                MessageBox.Show("��ѡ��Ҫ���䷢Ʊ�ŵ���Ա��", "��ʾ", MessageBoxButtons.OK);

                return;
            }

            GenerateInvoices();
            //this.txtStart.Text = invoiceServiceManager.GetDefaultStartCode(CurrInvoiceType.ID);
        }

        /// <summary>
        /// ����PersonListBox�����Ա������Invoice�������档��Ʊ��ϡ�
        /// </summary>
        protected virtual void GenerateInvoices()
        {
            //{B461213F-80EB-4338-9EF4-DB3E61F9C6DF}
            //FS.HISFC.Models.Fee.InvoiceTypeEnumService invoicetype = new FS.HISFC.Models.Fee.InvoiceTypeEnumService();
            string invoicetype = string.Empty;
            //invoicetype.ID = CurrInvoiceType.ID;
            invoicetype = CurrInvoiceType.ID;

            ArrayList invoices = new ArrayList();
            //��Ʊ����ʼ����
            currentStartCode = this.txtStart.Text.Trim();
            //��Ʊ����ֹ����
            currentCodeNums = Convert.ToInt64(this.txtQty.Text.Trim());
            //��Ա����б䶯��Ա��
            string changedPersons = string.Empty;

            foreach (ListViewItem item in this.lstvGetPerson.Items)
            {
                if (this.cmbGetType.SelectedIndex == 0)
                {
                    if (GetPersonType(item.SubItems[1].Text) == "F")
                    {
                        FS.HISFC.Models.Fee.Invoice invoice = CreateInvoiceItem(item.SubItems[1].Text, item.Text);
                        invoices.Add(invoice);
                    }
                    else
                    {
                        changedPersons = changedPersons + item.SubItems[1].Text + "(" + item.Text + "),";
                    }
                }
                else
                {
                    FS.HISFC.Models.Fee.Invoice invoice = CreateInvoiceItem(item.SubItems[1].Text, item.Text);
                    invoices.Add(invoice);
                }
            }

            if (changedPersons != string.Empty)
            {
                MessageBox.Show("ϵͳ��������Ա�Ѿ������շ�Ա�������µ�½��Ʊ���ý���:" + changedPersons);
                return;
            }
            //{B461213F-80EB-4338-9EF4-DB3E61F9C6DF}
            //FS.FrameWork.Management.Transaction trans = new FS.FrameWork.Management.Transaction(this.invoiceServiceManager.Connection);

            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.invoiceServiceManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            int result = 1;
            foreach (FS.HISFC.Models.Fee.Invoice info in invoices)
            {

                if (this.invoiceServiceManager.InsertInvoice(info) == -1)
                {
                    result = -1;
                    break;
                }
            }

            if (result == 1)
            {
                FS.FrameWork.Management.PublicTrans.Commit();
                AddDataTofpSpread2(invoices);
                MessageBox.Show("��Ʊ����ɹ���", "��ʾ", MessageBoxButtons.OK);

                PersonViewClear();

                return;
            }
            else
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("��Ʊ����ʧ�ܣ�" + this.invoiceServiceManager.Err);
            }

            MessageBox.Show("��Ʊ����ʧ�ܣ�", "��ʾ", MessageBoxButtons.OK);
            return;

        }

        /// <summary>
        /// ��Ʊ��ѯ
        /// </summary>
        protected virtual void SearchInvoiceBy()
        {
            try
            {
                if (this.neuTabControl1.SelectedIndex != 1)
                    this.neuTabControl1.SelectedIndex = 1;
                bool isGroup;
                if (this.cmbGetType.SelectedIndex == 0)
                    isGroup = false;
                else
                    isGroup = true;
                //{B461213F-80EB-4338-9EF4-DB3E61F9C6DF}
                //FS.HISFC.Models.Fee.InvoiceTypeEnumService type = new FS.HISFC.Models.Fee.InvoiceTypeEnumService();
                string type = string.Empty;
                //type.ID = CurrInvoiceType.ID;
                type = CurrInvoiceType.ID;
                
                if (this.txtpersonCode.Text != "")
                {
                    if (this.cmbGetType.SelectedIndex == 0)
                    {
                        if (GetPersonName(this.txtpersonCode.Text.Trim()) == "")
                        {
                            MessageBox.Show("�������Ա���Ų�����", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;

                        }
                    }
                    ArrayList List = invoiceServiceManager.QueryInvoices(this.txtpersonCode.Text.Trim(), type, isGroup);
                    SetDataToGrid(List);

                }
                else if(this.isShowAll || this.curOper.IsManager)
                {

                    ArrayList allList = invoiceServiceManager.QueryInvoices(type, isGroup);
                    SetDataToGrid(allList);
                }

            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        /// <summary>
        /// �ڷ�Ʊ�б�����ʾ��Ʊ
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
                if (this.cmbInvoiceState.SelectedIndex == 0)
                {
                    viewState = 2;
                }
                else if (this.cmbInvoiceState.SelectedIndex == 1)
                {
                    viewState = 1;
                }
                else
                    if (this.cmbInvoiceState.SelectedIndex == 2)
                    {
                        viewState = 0;
                    }
                    else
                        viewState = -1;

                foreach (FS.HISFC.Models.Fee.Invoice info in list)
                {
                    if (viewState != 2 && Convert.ToInt32(info.ValidState) != viewState)
                    {
                        continue;
                    }

                    fpSpread1_Sheet1.AddRows(i, 1);
                    fpSpread1_Sheet1.SetValue(i, 0, info.BeginNO.ToString().PadLeft(12, '0')); //��ʼ��
                    fpSpread1_Sheet1.SetValue(i, 1, info.EndNO.ToString().PadLeft(12, '0'));   //��ֹ��

                    if (Convert.ToInt32(info.ValidState) == 0)
                    {
                        state = "δ��";
                        fpSpread1_Sheet1.SetValue(i, 2, "");
                    }
                    else if (Convert.ToInt32(info.ValidState) == 1)
                    {
                        state = "����";
                        fpSpread1_Sheet1.SetValue(i, 2, info.UsedNO.ToString().PadLeft(12, '0'));
                    }
                    else
                    {
                        state = "����";
                        fpSpread1_Sheet1.SetValue(i, 2, info.UsedNO.ToString().PadLeft(12, '0'));
                    }


                   
                    fpSpread1_Sheet1.SetValue(i, 3, state);
                    fpSpread1_Sheet1.SetValue(i, 4, info.AcceptTime);
                    if (user == null || user == string.Empty)
                    {
                        user = info.AcceptOper.ID;

                    }

                    fpSpread1_Sheet1.SetValue(i, 5, user);

                    fpSpread1_Sheet1.SetValue(i, 5, info.AcceptOper.ID);
                    fpSpread1_Sheet1.SetValue(i, 6, this.cmbInvoiceType.Text);

                    info.Type.ID = CurrInvoiceType.ID;
                    if (this.cmbGetType.SelectedIndex == 0)
                    {
                        user = info.AcceptOper.Name;
                    }
                    else
                    {
                        user = info.AcceptOper.Name;
                    }
                    fpSpread1_Sheet1.SetValue(i, 7, user);
                    
                    fpSpread1_Sheet1.Rows[i].Tag = info;
                    //A87AAC77-567E-4c94-8BCA-1BD9D1EF74E7 ����ʣ�෢Ʊ����
                    int lastNo = 0;
                    try
                    {
                        if (!string.IsNullOrEmpty(info.UsedNO) && !string.IsNullOrEmpty(info.EndNO))
                        {
                            int useNo = Convert.ToInt32(info.UsedNO.Substring(3, info.UsedNO.Length - 3));
                            int endNo = Convert.ToInt32(info.EndNO.Substring(3, info.EndNO.Length - 3));
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
                foreach (FS.HISFC.Models.Fee.Invoice info in List)
                {
                    fpSpread2_Sheet1.Rows.Add(0, 1);
                    fpSpread2_Sheet1.Cells[0, 0].Text = info.AcceptOper.Name;
                    fpSpread2_Sheet1.Cells[0, 1].Text = info.Type.Name;
                    fpSpread2_Sheet1.Cells[0, 2].Text = info.BeginNO;
                    fpSpread2_Sheet1.Cells[0, 3].Text = info.EndNO;
                    fpSpread2_Sheet1.Cells[0, 4].Text = info.AcceptTime.ToString();


                }
            }

            catch (Exception ee)
            {
                string Error = ee.Message;
            }
        }

        /// <summary>
        /// ��Ʊ���յ�ʵ��
        /// </summary>
        /// <returns></returns>
        private FS.HISFC.Models.Fee.Invoice GetReturnBackInvoice()
        {
            return fpSpread1_Sheet1.Rows[fpSpread1_Sheet1.ActiveRowIndex].Tag as FS.HISFC.Models.Fee.Invoice;
        }

        /// <summary>
        /// �жϱ���еķ�Ʊ�Ƿ���Ի���
        /// </summary>
        /// <returns></returns>
        protected virtual bool CanReturnBack()
        {
            FS.HISFC.Models.Fee.Invoice info = fpSpread1_Sheet1.Rows[fpSpread1_Sheet1.ActiveRowIndex].Tag as FS.HISFC.Models.Fee.Invoice;
            if (info.ValidState == "-1")
                return false;

            return true;
        }

        /// <summary>
        /// ���շ�Ʊ
        /// </summary>
        public void ReturnBack()
        {
            if (this.neuTabControl1.SelectedIndex != 1)
                this.neuTabControl1.SelectedIndex = 1;
            if (this.fpSpread1_Sheet1.Rows.Count <= 0)
            {
                MessageBox.Show("û�п��Ի��յķ�Ʊ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return;
            }
            if (CanReturnBack() == false)
            {
                MessageBox.Show("��Ʊ�Ѿ���ʹ�ã����ܻ��գ�", "��ʾ", MessageBoxButtons.OK);

                return;
            }

            FS.HISFC.Models.Fee.Invoice invoiceReturn = GetReturnBackInvoice();

            FS.HISFC.Models.Fee.Invoice clone = (FS.HISFC.Models.Fee.Invoice)invoiceReturn.Clone();
            frmInvoiceReturn frmInvoiceReturn = new InpatientFee.Maintenance.frmInvoiceReturn(clone);
            DialogResult result = frmInvoiceReturn.ShowDialog();

            if (result == DialogResult.OK)
            {
                string start = clone.BeginNO;
                string invoiceStart = invoiceReturn.BeginNO;
                string end = clone.EndNO;
                string invoiceEnd = invoiceReturn.EndNO;

                bool saved = true;
                if (start == invoiceStart)
                {
                    if (invoiceServiceManager.Delete(clone) == -1)
                        saved = false; ;
                }
                else
                {
                    invoiceReturn.EndNO = FS.FrameWork.Public.String.AddNumber(start, -1);
                    if (invoiceReturn.EndNO == invoiceReturn.UsedNO)
                    {
                        invoiceReturn.ValidState = "-1";
                    }
                    invoiceReturn.Qty = invoiceReturn.Qty - clone.Qty;



                    if (invoiceServiceManager.UpdateInvoice(invoiceReturn) == -1)
                        saved = false;

                }
                if (saved)
                {
                    MessageBox.Show("��Ʊ���ճɹ���", "��ʾ", MessageBoxButtons.OK);
                    SearchInvoiceBy();
                }
                else
                    MessageBox.Show("��Ʊ����ʧ�ܣ�", "��ʾ", MessageBoxButtons.OK);


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
            toolBarService.AddToolButton("����", "���淢Ʊ������Ϣ", FS.FrameWork.WinForms.Classes.EnumImageList.X�½�, true, false, null);
            toolBarService.AddToolButton("����", "���շ�Ʊ��Ϣ", FS.FrameWork.WinForms.Classes.EnumImageList.Q���, true, false, null);

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
            this.SearchInvoiceBy();
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
            //string text = e.ClickedItem.Text.ToString().Trim();
            //int index = text.IndexOf('(');
            //if (index>0)
            //{
            //    text = text.Substring(0, index);
            //}
            switch (e.ClickedItem.Text.ToString())
            {
                case "����":
                    this.GetInvoice();
                    break;
                case "����":
                    this.ReturnBack();
                    break;
            }

            base.ToolStrip_ItemClicked(sender, e);
        }

        #endregion

        #region �¼�

        private void ucInvoicePanel_Load(object sender, EventArgs e)
        {
            this.ckbStartNO.Checked = true;
            Init();
        }

        private void cmbGetType_SelectedIndexChanged(object sender, EventArgs e)
        {
            CleanUpListView();

            if (this.cmbGetType.SelectedIndex == 0)
            {
                LoadPersons(this.lstvPerson);
            }
            else
            {
                //Ŀǰ�����鲻ά�������Բ��ܰ�������ȡ
                LoadGroups(this.lstvPerson);
            }

            this.lstvGetPerson.Items.Clear();
        }

        private void cmbInvoiceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.txtStart.Text = invoiceServiceManager.GetDefaultStartCode(CurrInvoiceType.ID);
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

                SearchInvoiceBy();
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

                SearchInvoiceBy();
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
                    //long StartNum = Convert.ToInt64(this.txtStart.Text) - 1;
                    //this.txtEndNO.Text = "";
                    //long endNum = StartNum + Convert.ToInt64(this.txtQty.Text.Trim());
                    //this.txtEndNO.Text =  Convert.ToString(endNum).PadLeft(14, '0');
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
                    //long StartNum = Convert.ToInt64(this.txtStart.Text) - 1;
                    //this.txtEndNO.Text = "";
                    //long endNum = StartNum + Convert.ToInt64(this.txtQty.Text.Trim());
                    //this.txtEndNO.Text = Convert.ToString(endNum).PadLeft(14, '0');

                    //2014-9-24
                    //this.txtEndNO.Text = FS.FrameWork.Public.String.AddNumber(this.txtStart.Text.Trim(), Convert.ToInt64(this.txtQty.Text.Trim()) - 1);

                    //���Ƿ�Ʊ�Ű�����ĸ�����
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

        private void cmbInvoiceState_SelectedIndexChanged(object sender, EventArgs e)
        {
            SearchInvoiceBy();
        }
        #endregion


    }
}
