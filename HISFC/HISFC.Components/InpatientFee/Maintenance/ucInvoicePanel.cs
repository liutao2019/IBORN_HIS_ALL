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
    /// 发票领用、回收控件
    /// 创建者：孙盟
    /// </summary>
    public partial class ucInvoicePanel : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucInvoicePanel()
        {
            InitializeComponent();
        }

        #region 变量

        /// <summary>
        /// 发票类型
        /// </summary>
        //{B461213F-80EB-4338-9EF4-DB3E61F9C6DF}
        //FS.HISFC.Models.Fee.InvoiceTypeEnumService minvoiceType = new FS.HISFC.Models.Fee.InvoiceTypeEnumService();
        private FS.FrameWork.Models.NeuObject minvoiceType = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// 人员业务层
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// 发票业务层
        /// </summary>
        //{B461213F-80EB-4338-9EF4-DB3E61F9C6DF}
        //protected FS.HISFC.BizLogic.Fee.InvoiceService invoiceServiceManager = new FS.HISFC.BizLogic.Fee.InvoiceService();
        protected FS.HISFC.BizLogic.Fee.InvoiceServiceNoEnum invoiceServiceManager = new FS.HISFC.BizLogic.Fee.InvoiceServiceNoEnum();

        /// <summary>
        /// 人员财务组业务层
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.EmployeeFinanceGroup employeeFinanceGroupManager = new FS.HISFC.BizLogic.Fee.EmployeeFinanceGroup();

        /// <summary>
        /// 人员数组
        /// </summary>
        ArrayList personList = new ArrayList();

        /// <summary>
        /// 人员财务组
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
        /// 当前登录操作员
        /// </summary>
        private FS.HISFC.Models.Base.Employee curOper;

        #endregion

        #region 属性

        /// <summary>
        /// 当前发票类型
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
        /// 取得当前人员姓名
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
        /// 取得当前人员编码
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
        /// 取得当前人员类型，用于判断操作的人员类型是否在后台有变化
        /// </summary>
        /// <param name="personCode"></param>
        /// <returns></returns>
        protected string GetPersonType(string personCode)
        {
            FS.HISFC.Models.Base.Employee employeeInfo = managerIntegrate.GetEmployeeInfo(personCode);

            return employeeInfo.EmployeeType.ID.ToString();
        }

        //{BA6D9596-064F-4dd7-B76A-A98FFD58EA65}
        [Category("控件设置"), Description("是否允许修改领用起始发票号,true:不允许修改，false:允许修改"), DefaultValue(true)]
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

        [Category("控件设置"), Description("是否显示全部人员,默认false"), DefaultValue(false)]
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

        #region 私有方法

        /// <summary>
        /// 初始化
        /// </summary>
        protected virtual void Init()
        {
            //当前操作员
            this.curOper = (FS.HISFC.Models.Base.Employee)this.invoiceServiceManager.Operator;

            //{B461213F-80EB-4338-9EF4-DB3E61F9C6DF}
            //this.cmbInvoiceType.AddItems(FS.HISFC.Models.Fee.InvoiceTypeEnumService.List());
            this.cmbInvoiceType.AddItems((new FS.HISFC.BizLogic.Manager.Constant().GetList("GetInvoiceType")));

            if (this.cmbInvoiceType.alItems == null || this.cmbInvoiceType.alItems.Count == 0)
            {
                MessageBox.Show("请在常数维护中维护收据的类别");
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
        /// 清除人员列表ListView
        /// </summary>
        protected virtual void CleanUpListView()
        {
            this.lstvPerson.Items.Clear();
            if (this.isShowAll || this.curOper.IsManager)
            {
                this.lstvPerson.Items.Add(new ListViewItem(new string[] { "全部" }));
                this.lstvPerson.Items[0].Selected = true;
            }
        }

        /// <summary>
        /// 加载人员
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
                //增加显示控制
                if (this.isShowAll || curOper.IsManager || p.ID.Equals(curOper.ID))
                {
                    ListViewItem item = new ListViewItem(new string[] { p.ID, p.Name });
                    listview.Items.Add(item);
                }
            }
        }

        /// <summary>
        /// 加载财务组信息
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
        /// 根据人员编码生成一个ｉｎｖｏｉｃｅ实体
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
        /// 验证起始号、终止号、数量的有效性。
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
                    MessageBox.Show("请输入发票起始号！", "提示", MessageBoxButtons.OK);
                    return false;
                }

                if (this.txtEndNO.Text.Trim() == "")
                {
                    MessageBox.Show("请输入发票终止号！", "提示", MessageBoxButtons.OK);
                    return false;
                }
                if (this.txtQty.Text.Trim() == "")
                {
                    MessageBox.Show("请输入领取发票的数量！", "提示", MessageBoxButtons.OK);
                    return false;
                }

                #region 检察

                try
                {
                    startcode = FS.FrameWork.Public.String.GetNumber(this.txtStart.Text.Trim());// Convert.ToInt64(this.txtStart.Text.Trim());
                }
                catch (FormatException formatException)
                {
                    MessageBox.Show("起始号必须是大于0的数字!" + formatException.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                catch (OverflowException overflowException)
                {
                    MessageBox.Show("起始号最大为12位!" + overflowException.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("请重新输入起始号!" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }

                try
                {
                    endcode = FS.FrameWork.Public.String.GetNumber(this.txtEndNO.Text.Trim());//  Convert.ToInt64(this.txtEndNO.Text.Trim());
                }
                catch (FormatException formatException)
                {
                    MessageBox.Show("终止号必须是大于0的数字!" + formatException.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                catch (OverflowException overflowException)
                {
                    MessageBox.Show("终止号最大为12位!" + overflowException.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("请重新输入终止号!" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }

                try
                {
                    num = Convert.ToInt64(this.txtQty.Text.Trim());
                }
                catch (FormatException formatException)
                {
                    MessageBox.Show("领取数量必须是大于0的数字!" + formatException.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                catch (OverflowException overflowException)
                {
                    MessageBox.Show("领取数量最大为12位!" + overflowException.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("请重新输入领取数量!" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }

                #endregion

                if (endcode < startcode)
                {
                    MessageBox.Show("终止号应大于或等于起始号！", "提示", MessageBoxButtons.OK);

                    return false;
                }

                //if (Convert.ToInt64(txtEndNO.Text.Trim()).ToString().Length > 12)
                //{
                //    MessageBox.Show("发票数量过大，请重新输入！", "提示");
                //    txtQty.Focus();
                //    txtQty.SelectAll();
                //    return false;
                //}

                if (this.txtEndNO.Text.Trim().Length > 12)
                {
                    MessageBox.Show("发票数量过大，请重新输入！", "提示");
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
                MessageBox.Show("输入的发票号有误，可能已被领取，请重新输入！", "提示", MessageBoxButtons.OK);
                return false;
            }


            return true;
        }

        /// <summary>
        /// 清除lstvPerson中所有的选中
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
        /// 删除lstvGetPerson里的人员信息
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
        /// 在lstvGetPerson里添加lstvPerson选中的人员信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        protected virtual void AddPersonList(string id, string name)
        {
            this.lstvGetPerson.Items.Add(new ListViewItem(new string[] { name, id }));
        }

        /// <summary>
        /// 领用发票
        /// </summary>
        private void GetInvoice()
        {
            this.neuTabControl1.SelectedIndex = 0;
            if (ValidateNumValid() == false)
                return;
            if (this.lstvGetPerson.Items.Count <= 0)
            {
                MessageBox.Show("请选择要分配发票号的人员！", "提示", MessageBoxButtons.OK);

                return;
            }

            GenerateInvoices();
            //this.txtStart.Text = invoiceServiceManager.GetDefaultStartCode(CurrInvoiceType.ID);
        }

        /// <summary>
        /// 根据PersonListBox里的人员，生成Invoice，并保存。领票完毕。
        /// </summary>
        protected virtual void GenerateInvoices()
        {
            //{B461213F-80EB-4338-9EF4-DB3E61F9C6DF}
            //FS.HISFC.Models.Fee.InvoiceTypeEnumService invoicetype = new FS.HISFC.Models.Fee.InvoiceTypeEnumService();
            string invoicetype = string.Empty;
            //invoicetype.ID = CurrInvoiceType.ID;
            invoicetype = CurrInvoiceType.ID;

            ArrayList invoices = new ArrayList();
            //发票的起始号码
            currentStartCode = this.txtStart.Text.Trim();
            //发票的终止号码
            currentCodeNums = Convert.ToInt64(this.txtQty.Text.Trim());
            //人员类别有变动的员工
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
                MessageBox.Show("系统中以下人员已经不是收费员，请重新登陆发票领用界面:" + changedPersons);
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
                MessageBox.Show("发票分配成功！", "提示", MessageBoxButtons.OK);

                PersonViewClear();

                return;
            }
            else
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("发票分配失败！" + this.invoiceServiceManager.Err);
            }

            MessageBox.Show("发票分配失败！", "提示", MessageBoxButtons.OK);
            return;

        }

        /// <summary>
        /// 发票查询
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
                            MessageBox.Show("输入的人员工号不存在", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        /// 在发票列表中显示发票
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
                    fpSpread1_Sheet1.SetValue(i, 0, info.BeginNO.ToString().PadLeft(12, '0')); //启始号
                    fpSpread1_Sheet1.SetValue(i, 1, info.EndNO.ToString().PadLeft(12, '0'));   //终止号

                    if (Convert.ToInt32(info.ValidState) == 0)
                    {
                        state = "未用";
                        fpSpread1_Sheet1.SetValue(i, 2, "");
                    }
                    else if (Convert.ToInt32(info.ValidState) == 1)
                    {
                        state = "在用";
                        fpSpread1_Sheet1.SetValue(i, 2, info.UsedNO.ToString().PadLeft(12, '0'));
                    }
                    else
                    {
                        state = "已用";
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
                    //A87AAC77-567E-4c94-8BCA-1BD9D1EF74E7 计算剩余发票张数
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
        /// 在列表显示成功分配的发票
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
        /// 发票回收的实体
        /// </summary>
        /// <returns></returns>
        private FS.HISFC.Models.Fee.Invoice GetReturnBackInvoice()
        {
            return fpSpread1_Sheet1.Rows[fpSpread1_Sheet1.ActiveRowIndex].Tag as FS.HISFC.Models.Fee.Invoice;
        }

        /// <summary>
        /// 判断表格中的发票是否可以回收
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
        /// 回收发票
        /// </summary>
        public void ReturnBack()
        {
            if (this.neuTabControl1.SelectedIndex != 1)
                this.neuTabControl1.SelectedIndex = 1;
            if (this.fpSpread1_Sheet1.Rows.Count <= 0)
            {
                MessageBox.Show("没有可以回收的发票！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return;
            }
            if (CanReturnBack() == false)
            {
                MessageBox.Show("发票已经被使用，不能回收！", "提示", MessageBoxButtons.OK);

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
                    MessageBox.Show("发票回收成功！", "提示", MessageBoxButtons.OK);
                    SearchInvoiceBy();
                }
                else
                    MessageBox.Show("发票回收失败！", "提示", MessageBoxButtons.OK);


            }

        }

        /// <summary>
        /// 添加toolbar按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("领用", "保存发票领用信息", FS.FrameWork.WinForms.Classes.EnumImageList.X新建, true, false, null);
            toolBarService.AddToolButton("回收", "回收发票信息", FS.FrameWork.WinForms.Classes.EnumImageList.Q清空, true, false, null);

            return this.toolBarService;
        }

        /// <summary>
        /// 重载toolbarservice的onprint
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
        /// 重载toolbarservice的OnPrintPreview
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
        /// 重载toolbarservice的export
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
        /// 重载toolbarservice的onquery
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
        /// 导出数据成excel
        /// </summary>
        protected virtual void Exportinfo()
        {
            try
            {
                bool ret = false;
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "Excel |.xls";
                saveFileDialog1.Title = "导出数据";
                saveFileDialog1.FileName = "发票";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if (saveFileDialog1.FileName != "")
                    {
                        if (this.neuTabControl1.SelectedIndex == 1)
                        {
                            //以Excel 的形式导出数据
                            ret = fpSpread1.SaveExcel(saveFileDialog1.FileName, FarPoint.Win.Spread.Model.IncludeHeaders.ColumnHeadersCustomOnly);

                        }
                        else
                        {
                            ret = fpSpread2.SaveExcel(saveFileDialog1.FileName, FarPoint.Win.Spread.Model.IncludeHeaders.ColumnHeadersCustomOnly);
                        }
                    }
                    if (ret)
                    {
                        MessageBox.Show("成功导出数据");
                    }
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        /// <summary>
        /// 打印ｔａｂ页面的表格
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
                    MessageBox.Show("请选择要打印的页面");
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
            return 0;
        }

        #endregion

        #region 共有方法

        /// <summary>
        /// 定义toolbar按钮click
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
                case "领用":
                    this.GetInvoice();
                    break;
                case "回收":
                    this.ReturnBack();
                    break;
            }

            base.ToolStrip_ItemClicked(sender, e);
        }

        #endregion

        #region 事件

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
                //目前财务组不维护，所以不能按照组领取
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
                if (this.lstvPerson.Items[0].Selected && this.lstvPerson.SelectedItems[0].SubItems[0].Text == "全部")
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
                if (this.lstvPerson.Items[0].Selected && this.lstvPerson.SelectedItems[0].SubItems[0].Text == "全部")
                    this.txtpersonCode.Text = "";
                else
                    this.txtpersonCode.Text = this.lstvPerson.SelectedItems[0].SubItems[0].Text;

                SearchInvoiceBy();
            }
        }

        /// <summary>
        /// 在lstvPerson中选择要分配的人员
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

                    //考虑发票号包含字母的情况
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
