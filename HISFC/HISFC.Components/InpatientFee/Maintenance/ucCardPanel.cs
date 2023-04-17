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
    /// 卡卷领用、回收控件
    /// 创建者：
    /// </summary>
    public partial class ucCardPanel : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucCardPanel()
        {
            InitializeComponent();
        }

        #region 变量

        /// <summary>
        /// 卡类型
        /// </summary>
        private FS.FrameWork.Models.NeuObject cardKind = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// 人员业务层
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// 卡卷业务层
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.DiscountCardLogic discountCardLogic = new FS.HISFC.BizLogic.Fee.DiscountCardLogic();

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
        [Category("控件设置"), Description("是否允许修改领用起始号,true:不允许修改，false:允许修改"), DefaultValue(true)]
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
            this.curOper = (FS.HISFC.Models.Base.Employee)this.discountCardLogic.Operator;

            this.cmbCardKind.AddItems((new FS.HISFC.BizLogic.Manager.Constant().GetList("GetCardKind")));

            if (this.cmbCardKind.alItems == null || this.cmbCardKind.alItems.Count == 0)
            {
                MessageBox.Show("请在常数维护中维护收据的类别");
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
        /// 根据人员编码生成一个实体
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
                    MessageBox.Show("请输入起始号！", "提示", MessageBoxButtons.OK);
                    return false;
                }

                if (this.txtEndNO.Text.Trim() == "")
                {
                    MessageBox.Show("请输入终止号！", "提示", MessageBoxButtons.OK);
                    return false;
                }
                if (this.txtQty.Text.Trim() == "")
                {
                    MessageBox.Show("请输入领取的数量！", "提示", MessageBoxButtons.OK);
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

            if (discountCardLogic.DiscountCardIsValid(this.txtStart.Text.Trim(), this.txtEndNO.Text.Trim(), this.CurrCardKind.ID) == false)
            {
                MessageBox.Show("输入的卡号有误，可能已被领取，请重新输入！", "提示", MessageBoxButtons.OK);
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
        /// 领用卡
        /// </summary>
        private void GetDiscountCard()
        {
            this.neuTabControl1.SelectedIndex = 0;
            if (ValidateNumValid() == false)
                return;
            if (this.lstvGetPerson.Items.Count <= 0)
            {
                MessageBox.Show("请选择要分配卡号的人员！", "提示", MessageBoxButtons.OK);

                return;
            }

            GenerateDiscountCard();
        }

        /// <summary>
        /// 根据PersonListBox里的人员，生成，并保存。领用完毕。
        /// </summary>
        protected virtual void GenerateDiscountCard()
        {
            string cardKind = string.Empty;

            cardKind = this.CurrCardKind.ID;

            ArrayList discountCards = new ArrayList();
            //起始号码
            currentStartCode = this.txtStart.Text.Trim();
            //终止号码
            currentCodeNums = Convert.ToInt64(this.txtQty.Text.Trim());
            //人员类别有变动的员工
            string changedPersons = string.Empty;

            foreach (ListViewItem item in this.lstvGetPerson.Items)
            {
                FS.HISFC.Models.Fee.DiscountCard discountCard = CreateDiscountCardItem(item.SubItems[1].Text, item.Text);
                discountCards.Add(discountCard);
            }

            if (changedPersons != string.Empty)
            {
                MessageBox.Show("系统中以下人员已经不是收费员，请重新登陆领用界面:" + changedPersons);
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
                MessageBox.Show("分配成功！", "提示", MessageBoxButtons.OK);

                PersonViewClear();

                return;
            }
            else
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("分配失败！" + this.discountCardLogic.Err);
            }

            MessageBox.Show("分配失败！", "提示", MessageBoxButtons.OK);
            return;

        }

        /// <summary>
        /// 查询
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
                        MessageBox.Show("输入的人员工号不存在", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        /// 在列表中显示
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
                    fpSpread1_Sheet1.SetValue(i, 0, info.StartNo.ToString().PadLeft(12, '0')); //启始号
                    fpSpread1_Sheet1.SetValue(i, 1, info.EndNo.ToString().PadLeft(12, '0'));   //终止号

                    if (Convert.ToInt32(info.UsedState) == 0)
                    {
                        state = "未用";
                        fpSpread1_Sheet1.SetValue(i, 2, "");
                    }
                    else if (Convert.ToInt32(info.UsedState) == 1)
                    {
                        state = "在用";
                        fpSpread1_Sheet1.SetValue(i, 2, info.UsedNo.ToString().PadLeft(12, '0'));
                    }
                    else
                    {
                        state = "已用";
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
        /// 在列表显示成功分配的发票
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
        /// 回收的实体
        /// </summary>
        /// <returns></returns>
        private FS.HISFC.Models.Fee.DiscountCard GetReturnBackDiscountCard()
        {
            return fpSpread1_Sheet1.Rows[fpSpread1_Sheet1.ActiveRowIndex].Tag as FS.HISFC.Models.Fee.DiscountCard;
        }

        /// <summary>
        /// 判断表格中的是否可以回收
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
        /// 回收
        /// </summary>
        public void ReturnBack()
        {
            if (this.neuTabControl1.SelectedIndex != 1)
                this.neuTabControl1.SelectedIndex = 1;
            if (this.fpSpread1_Sheet1.Rows.Count <= 0)
            {
                MessageBox.Show("没有可以回收的卡！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return;
            }
            if (CanReturnBack() == false)
            {
                MessageBox.Show("卡已经被使用，不能回收！", "提示", MessageBoxButtons.OK);

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
                    MessageBox.Show("回收成功！", "提示", MessageBoxButtons.OK);
                    SearchDiscountCardBy();
                }
                else
                    MessageBox.Show("回收失败！", "提示", MessageBoxButtons.OK);


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
            toolBarService.AddToolButton("领用", "保存领用信息", FS.FrameWork.WinForms.Classes.EnumImageList.X新建, true, false, null);
            toolBarService.AddToolButton("回收", "回收卡信息", FS.FrameWork.WinForms.Classes.EnumImageList.Q清空, true, false, null);

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
            this.SearchDiscountCardBy();
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
            switch (e.ClickedItem.Text.ToString())
            {
                case "领用":
                    this.GetDiscountCard();
                    break;
                case "回收":
                    this.ReturnBack();
                    break;
            }

            base.ToolStrip_ItemClicked(sender, e);
        }

        #endregion

        #region 事件

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
                if (this.lstvPerson.Items[0].Selected && this.lstvPerson.SelectedItems[0].SubItems[0].Text == "全部")
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
                if (this.lstvPerson.Items[0].Selected && this.lstvPerson.SelectedItems[0].SubItems[0].Text == "全部")
                    this.txtpersonCode.Text = "";
                else
                    this.txtpersonCode.Text = this.lstvPerson.SelectedItems[0].SubItems[0].Text;

                SearchDiscountCardBy();
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
