using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Nurse
{
    /// <summary>
    /// 门诊治疗单查询并打印（补打）
    /// 2011.6.21 佛山南庄 赵景
    /// 右下角设置控制系统类别
    /// 执行科室进行查询
    /// 右下角设置控制挂号记录的有效性
    /// 右下角控制发票号查询还是卡号查询
    /// </summary>
    public partial class ucTreatmentQuery : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucTreatmentQuery()
        {
            InitializeComponent();
        }

        #region 变量

        /// <summary>
        /// 挂号业务层
        /// </summary>
        private FS.HISFC.BizLogic.Registration.Register registerManager = new FS.HISFC.BizLogic.Registration.Register();
        /// <summary>
        /// 综合费用层
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Fee feeManage = new FS.HISFC.BizProcess.Integrate.Fee();

        /// <summary>
        /// 门诊费用业务层
        /// </summary>
        private
            FS.HISFC.BizLogic.Fee.Outpatient outpatientFeeManager = new FS.HISFC.BizLogic.Fee.Outpatient();
        /// <summary>
        /// 常数维护
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager constantMgr = new FS.HISFC.BizProcess.Integrate.Manager();
        private FS.FrameWork.Public.ObjectHelper usageHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 科室信息
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper deptHelper = new FS.FrameWork.Public.ObjectHelper();
        /// <summary>
        /// 医生信息
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper doctHelper = new FS.FrameWork.Public.ObjectHelper();

        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        private FS.HISFC.BizProcess.Interface.Nurse.ITreatmentPrint ucPrint = null;
        #endregion

        #region 属性

        /// <summary>
        /// 挂号记录/发票查询有效天数
        /// </summary>
        private int validDays = 1;
        [Description("显示挂号记录有效天数，默认值为1天"), Category("参数设置")]
        public int ValidDays
        {
            get
            {
                return this.validDays;
            }
            set
            {
                if (value <= 0)
                {
                    this.validDays = 1;
                }
                else
                {
                    this.validDays = value;
                }
            }
        }

        /// <summary>
        /// 是否只显示本科室的项目
        /// </summary>
        private bool isShowOnlyCurrentDept = true;
        [Description("是否只显示本科室项目，true显示当前登录科室的项目，false显示所有科室的项目"), Category("参数设置")]
        public bool IsShowOnlyCurrentDept
        {
            get
            {
                return this.isShowOnlyCurrentDept;
            }
            set
            {
                this.isShowOnlyCurrentDept = value;
            }
        }

        /// <summary>
        /// 系统类别（默认治疗）
        /// </summary>
        private FS.HISFC.Models.Base.EnumSysClass sysClass = FS.HISFC.Models.Base.EnumSysClass.UZ;
        [Description("显示当前系统类别的项目"), Category("参数设置")]
        public FS.HISFC.Models.Base.EnumSysClass SysClass
        {
            get
            {
                return this.sysClass;
            }
            set
            {
                this.sysClass = value;
            }
        }

        /// <summary>
        /// 是否显示所有项目
        /// </summary>
        private bool isShowAllItem = false;
        [Description("是否显示所有项目；如果是，则系统类别SysClass参数无用"), Category("参数设置")]
        public bool IsShowAllItem
        {
            get
            {
                return this.isShowAllItem;
            }
            set
            {
                this.isShowAllItem = value;
            }
        }

        /// <summary>
        /// 是否显示所有用法的项目
        /// </summary>
        private bool isShowAllUsage = false;
        [Description("是否显示所有用法的项目；如果是，则常数维护里面的MZZLDUSAGE无效"), Category("参数设置")]
        public bool IsShowAllUsage
        {
            get
            {
                return this.isShowAllUsage;
            }
            set
            {
                this.isShowAllUsage = value;
            }
        }

        /// <summary>
        /// 是否显示发票信息
        /// </summary>
        private QueryType queryType = QueryType.RegInfo;
        [Description("查询方式，RegInfo：查询挂号信息；InvocieInfo：查询发票信息"), Category("参数设置")]
        public QueryType FeeQueryType
        {
            get
            {
                return this.queryType;
            }
            set
            {
                if (value == QueryType.RegInfo)
                {
                    this.neuTabControl1.TabPages.Remove(this.tpInvoice);
                    this.txtInvoiceNO.Visible = false;
                    this.lbInvoice.Visible = false;
                }
                else
                {
                    this.neuTabControl1.TabPages.Remove(this.tpRegInfo);
                }
                this.queryType = value;
            }
        }

        /// <summary>
        /// 是否自动打印
        /// </summary>
        private bool isAutoPrint = true;
        [Description("是否自动打印治理单"), Category("参数设置")]
        public bool IsAutoPrint
        {
            get
            {
                return isAutoPrint;
            }
            set
            {
                this.isAutoPrint = value;
            }
        }

        /// <summary>
        /// 是否自动打印
        /// </summary>
        private bool isShowNoFee = true;
        [Description("是否显示未收费"), Category("参数设置")]
        public bool IsShowNoFee
        {
            get
            {
                return isShowNoFee;
            }
            set
            {
                this.isShowNoFee = value;
            }
        }

        /// <summary>
        /// 是否自动打印
        /// </summary>
        private ItemType showItemType = ItemType.Undrug;
        [Description("是否显示药品"), Category("参数设置")]
        public ItemType ShowItemType
        {
            get
            {
                return showItemType;
            }
            set
            {
                this.showItemType = value;
            }
        }

        #endregion

        #region 工具栏

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.toolBarService.AddToolButton("全选", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q全选, true, false, null);
            this.toolBarService.AddToolButton("全不选", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q取消, true, false, null);
            return this.toolBarService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text.Trim())
            {
                case "全选":
                    this.SelectAll(true);
                    break;
                case "全不选":
                    this.SelectAll(false);
                    break;

                default:
                    break;
            }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        private int Init()
        {
            //默认天数
            DateTime dtNow = this.registerManager.GetDateTimeFromSysDateTime();
            this.dtpStart.Value = dtNow.AddDays(-this.validDays);
            this.dtpEnd.Value = dtNow;

            //获取治疗单用法
            if (this.isShowAllUsage == false)
            {
                usageHelper.ArrayObject = constantMgr.GetConstantList("MZZLDUSAGE");
            }

            //加载门诊科室
            deptHelper.ArrayObject = this.constantMgr.GetDepartment(FS.HISFC.Models.Base.EnumDepartmentType.C);
            //加载医生列表
            doctHelper.ArrayObject = this.constantMgr.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.D);

            //设置格式
            FarPoint.Win.Spread.CellType.CheckBoxCellType ckCellType = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            this.neuSpread1_Sheet1.Columns[(int)ColFeeItem.Selected].CellType = ckCellType;
            FarPoint.Win.Spread.CellType.NumberCellType numCellType = new FarPoint.Win.Spread.CellType.NumberCellType();
            this.neuSpread1_Sheet1.Columns[(int)ColFeeItem.Days].CellType = numCellType;
            this.neuSpread1_Sheet1.Columns[(int)ColFeeItem.Num].CellType = numCellType;


            //加载打印预览
            //获取接口
            ucPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Nurse.ITreatmentPrint)) as FS.HISFC.BizProcess.Interface.Nurse.ITreatmentPrint;

            if (ucPrint == null)
            {
                ucPrint = new FS.HISFC.Components.Nurse.Print.ucPrintTreatment();
            }

            if (this.ucPrint != null)
            {
                ucPrint.Init();

                if (ucPrint is Control)
                {
                    ((Control)ucPrint).Dock = DockStyle.Fill;
                    this.panelPrint.Controls.Add((Control)ucPrint);
                }
            }

            this.neuSpread1.ButtonClicked += new FarPoint.Win.Spread.EditorNotifyEventHandler(neuSpread1_ButtonClicked);

            this.FeeQueryType = this.queryType;

            this.txtCardNo.Select();
            this.txtCardNo.SelectAll();
            this.txtCardNo.Focus();
            return 1;
        }

        private void Clear()
        {
            this.neuSpread1_Sheet1.RowCount = 0;
            this.tvRegInfo.Nodes.Clear();
            if (this.ucPrint != null)
            {
                this.ucPrint.Init();
            }
        }

        private void Query()
        {
            this.Clear();
            HISFC.Models.Account.AccountCard objCard = new FS.HISFC.Models.Account.AccountCard();
            if (this.txtCardNo.Text.Trim() == "")
            {
                MessageBox.Show("请输入病历号!", "提示");
                this.txtCardNo.Focus();
                this.txtCardNo.SelectAll();
                return;
            }
            string strCardNO = this.txtCardNo.Text.Trim();
            int iTemp = feeManage.ValidMarkNO(strCardNO, ref objCard);
            if (iTemp <= 0 || objCard == null)
            {
                MessageBox.Show("无效卡号，请联系管理员！");
                this.txtCardNo.Focus();
                this.txtCardNo.SelectAll();
                return;
            }
            string cardNo = objCard.Patient.PID.CardNO;

            if (this.queryType == QueryType.RegInfo)
            {
                this.QueryRegInfo(cardNo, this.dtpStart.Value);
            }
            else
            {
                this.QueryInvoiceInfoByCardNo(cardNo, this.dtpStart.Value, this.dtpEnd.Value);
            }


        }

        private void QueryRegInfo(string cardNo, DateTime beginDate)
        {
            this.tvRegInfo.Nodes.Clear();
            ArrayList alRegs = this.registerManager.Query(cardNo, beginDate.Date);
            if (alRegs == null || alRegs.Count == 0)
            {
                MessageBox.Show("没有病历号为:" + cardNo + "的患者挂号信息!", "提示");
                this.txtCardNo.Focus();
                this.txtCardNo.SelectAll();
                return;
            }

            foreach (FS.HISFC.Models.Registration.Register reg in alRegs)
            {
                TreeNode treeNode = new TreeNode();
                string doctName = this.doctHelper.GetName(reg.SeeDoct.ID);
                if (string.IsNullOrEmpty(doctName))
                {
                    FS.FrameWork.Models.NeuObject obj = this.constantMgr.GetEmployeeInfo(reg.SeeDoct.ID);
                    if (obj != null)
                    {
                        doctName = obj.Name;
                    }
                }
                treeNode.Text = reg.Name + "【" + reg.SeeDoct.OperTime.ToString("yyyy-MM-dd") + "】" + "【" + doctName + "】";
                treeNode.Name = reg.ID;
                treeNode.Tag = reg;

                this.tvRegInfo.Nodes.Add(treeNode);
            }

            this.txtCardNo.Text = cardNo;
            this.txtCardNo.Focus();

            if (this.tvRegInfo.Nodes.Count > 0)
            {
                this.tvRegInfo.SelectedNode = this.tvRegInfo.Nodes[0];
                //自动打印
                if (this.isAutoPrint)
                {
                    this.Print();
                }
            }
        }

        private void QueryInvoiceInfoByCardNo(string cardNo, DateTime beginDate, DateTime endDate)
        {
            this.tvInvoice.Nodes.Clear();
            List<FS.HISFC.Models.Fee.Outpatient.Balance> balanceList = new List<FS.HISFC.Models.Fee.Outpatient.Balance>();
            int i = outpatientFeeManager.QueryInvoiceInfoByCardNo(cardNo, beginDate.Date, endDate.AddDays(1).Date, out balanceList);
            if (i <= 0 || balanceList == null || balanceList.Count == 0)
            {
                MessageBox.Show("没有病历号为:" + cardNo + "的患者发票信息!", "提示");
                this.txtCardNo.Focus();
                this.txtCardNo.SelectAll();
                return;
            }

            foreach (FS.HISFC.Models.Fee.Outpatient.Balance balance in balanceList)
            {
                TreeNode treeNode = new TreeNode();
                treeNode.Text = balance.Invoice.ID + "【" + balance.BalanceOper.OperTime.ToString("yyyy-MM-dd") + "】" + "【" + this.GetInvoiceTypeName(balance.CancelType) + "】";
                treeNode.Name = balance.Invoice.ID;
                treeNode.Tag = balance;

                this.tvInvoice.Nodes.Add(treeNode);
            }
            this.txtCardNo.Text = cardNo;
            this.txtCardNo.Focus();
            if (this.tvInvoice.Nodes.Count > 0)
            {
                this.tvInvoice.SelectedNode = this.tvInvoice.Nodes[0];
                //自动打印
                if (this.isAutoPrint)
                {
                    this.Print();
                }
            }

        }

        private void QueryInvoiceInfoByInvoice(string invoiceNo)
        {
            this.tvInvoice.Nodes.Clear();
            ArrayList balanceList = outpatientFeeManager.QueryBalancesByInvoiceNO(invoiceNo);
            if (balanceList == null || balanceList.Count == 0)
            {
                MessageBox.Show("没有发票号号为:" + invoiceNo + "的患者发票信息!", "提示");
                this.txtInvoiceNO.Focus();
                this.txtInvoiceNO.SelectAll();
                return;
            }

            foreach (FS.HISFC.Models.Fee.Outpatient.Balance balance in balanceList)
            {
                TreeNode treeNode = new TreeNode();

                treeNode.Text = balance.Invoice.ID + "【" + balance.BalanceOper.OperTime.ToString("yyyy-MM-dd") + "】" + "【" + this.GetInvoiceTypeName(balance.CancelType) + "】";
                treeNode.Name = balance.Invoice.ID;
                treeNode.Tag = balance;

                this.tvInvoice.Nodes.Add(treeNode);
            }
            this.txtInvoiceNO.Focus();
            if (this.tvInvoice.Nodes.Count > 0)
            {
                this.tvInvoice.SelectedNode = this.tvInvoice.Nodes[0];
                //自动打印
                if (this.isAutoPrint)
                {
                    this.Print();
                }
            }


        }

        private string GetInvoiceTypeName(FS.HISFC.Models.Base.CancelTypes cancelTypes)
        {
            switch (cancelTypes)
            {
                case FS.HISFC.Models.Base.CancelTypes.Canceled:
                    return "退费";
                case FS.HISFC.Models.Base.CancelTypes.LogOut:
                    return "作废";
                case FS.HISFC.Models.Base.CancelTypes.Reprint:
                    return "重打";
                case FS.HISFC.Models.Base.CancelTypes.Valid:
                    return "有效";
                default:
                    return "";
            }
        }

        /// <summary>
        /// 添加单条明细
        /// </summary>
        /// <param name="row"></param>
        /// <param name="feeItem"></param>
        private void AddDetail(int row, FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItem)
        {
            this.neuSpread1_Sheet1.Rows[row].Tag = feeItem;
            string doctName = this.doctHelper.GetName(feeItem.RecipeOper.ID);
            if (string.IsNullOrEmpty(doctName))
            {
                FS.FrameWork.Models.NeuObject obj = this.constantMgr.GetEmployeeInfo(feeItem.RecipeOper.ID);
                if (obj != null)
                {
                    doctName = obj.Name;
                }
            }
            this.neuSpread1_Sheet1.Cells[row, (int)ColFeeItem.SeeDoct].Text = doctName;
            string deptName = this.deptHelper.GetName(feeItem.RecipeOper.Dept.ID);
            if (string.IsNullOrEmpty(deptName))
            {
                FS.FrameWork.Models.NeuObject obj = this.constantMgr.GetDepartment(feeItem.RecipeOper.Dept.ID);
                if (obj != null)
                {
                    deptName = obj.Name;
                }
            }
            string execdeptName = this.deptHelper.GetName(feeItem.ExecOper.Dept.ID);
            if (string.IsNullOrEmpty(execdeptName))
            {
                FS.FrameWork.Models.NeuObject obj = this.constantMgr.GetDepartment(feeItem.ExecOper.Dept.ID);
                if (obj != null)
                {
                    execdeptName = obj.Name;
                }
            }
            if (string.IsNullOrEmpty(feeItem.Order.Memo))
            {
                FS.HISFC.BizProcess.Integrate.Order orderMgr = new FS.HISFC.BizProcess.Integrate.Order();
                FS.HISFC.Models.Order.OutPatient.Order order = orderMgr.GetOneOrder(feeItem.Patient.ID, feeItem.Order.ID);
                if (order != null)
                {
                    feeItem.Order.Memo = order.Memo;
                }
            }

            this.neuSpread1_Sheet1.Cells[row, (int)ColFeeItem.SeeDept].Text = deptName;
            this.neuSpread1_Sheet1.Cells[row, (int)ColFeeItem.ItemName].Text = feeItem.Item.Name;
            this.neuSpread1_Sheet1.Cells[row, (int)ColFeeItem.ComboCode].Text = feeItem.Order.Combo.ID;
            this.neuSpread1_Sheet1.Cells[row, (int)ColFeeItem.Num].Text = feeItem.Item.Qty.ToString();
            this.neuSpread1_Sheet1.Cells[row, (int)ColFeeItem.Frequency].Text = feeItem.Order.Frequency.Name;
            this.neuSpread1_Sheet1.Cells[row, (int)ColFeeItem.Usage].Text = feeItem.Order.Usage.Name;
            this.neuSpread1_Sheet1.Cells[row, (int)ColFeeItem.Days].Text = feeItem.Days.ToString();
            this.neuSpread1_Sheet1.Cells[row, (int)ColFeeItem.OrderMemo].Text = feeItem.Order.Memo;
            this.neuSpread1_Sheet1.Cells[row, (int)ColFeeItem.ExecDept].Text = execdeptName;
        }

        private void SelectAll(bool isSelect)
        {
            if (this.neuSpread1_Sheet1.RowCount > 0)
            {
                for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
                {
                    this.neuSpread1_Sheet1.Cells[i, (int)ColFeeItem.Selected].Value = isSelect;
                }

            }
            this.SetPrintInfo();
        }

        private void SelectedComb(bool isSelect)
        {
            int row = this.neuSpread1_Sheet1.ActiveRowIndex;
            string combID = this.neuSpread1_Sheet1.Cells[row, (int)ColFeeItem.ComboCode].Value.ToString();
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                if (this.neuSpread1_Sheet1.Cells[i, (int)ColFeeItem.ComboCode].Value.ToString() == combID)
                {
                    this.neuSpread1_Sheet1.Cells[i, (int)ColFeeItem.Selected].Value = isSelect;
                }
            }

        }

        private void SetPrintInfo()
        {
            //获取所有的打印信息进行设置
            ArrayList alPrint = new ArrayList();
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                if (FS.FrameWork.Function.NConvert.ToBoolean(this.neuSpread1_Sheet1.Cells[i, (int)ColFeeItem.Selected].Value))
                {
                    if (this.neuSpread1_Sheet1.Rows[i].Tag != null && this.neuSpread1_Sheet1.Rows[i].Tag is FS.HISFC.Models.Fee.Outpatient.FeeItemList)
                    {
                        alPrint.Add(this.neuSpread1_Sheet1.Rows[i].Tag);
                    }
                }
            }

            //设置
            if (ucPrint != null)
            {
                this.ucPrint.SetData(alPrint);
            }
        }

        private void SetFeeInfo(ArrayList alFee)
        {
            //清空表格
            this.neuSpread1_Sheet1.RowCount = 0;
            string currentDept = ((FS.HISFC.Models.Base.Employee)this.registerManager.Operator).Dept.ID;
            //没有任何费用信息
            foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItem in alFee)
            {
                //显示有效信息
                if (feeItem.CancelType != FS.HISFC.Models.Base.CancelTypes.Valid)
                {
                    continue;
                }
                //药品不显示
                if (showItemType == ItemType.Undrug)
                {
                    if (feeItem.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        continue;
                    }
                }
                else if (showItemType == ItemType.Drug)
                {
                    if (feeItem.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.UnDrug)
                    {
                        continue;
                    }
                }

                //判断执行科室是否本科室
                if (this.isShowOnlyCurrentDept)
                {
                    if (feeItem.ExecOper.Dept.ID.Equals(currentDept) == false)
                    {
                        continue;
                    }
                }
                //判断是否显示所有费用，不是则按照系统类别进行过滤
                if (this.isShowAllItem == false)
                {
                    if (feeItem.Item.SysClass.ID.ToString().Equals(this.sysClass.ToString()) == false)
                    {
                        continue;
                    }
                }

                //根据用法显示
                if (this.isShowAllUsage == false)
                {
                    FS.HISFC.Models.Base.Const constObject = usageHelper.GetObjectFromID(feeItem.Order.Usage.ID) as FS.HISFC.Models.Base.Const;
                    if (constObject == null || string.IsNullOrEmpty(constObject.ID))
                    {
                        continue;
                    }
                }

                this.neuSpread1_Sheet1.RowCount++;
                this.AddDetail(this.neuSpread1_Sheet1.RowCount - 1, feeItem);
            }

            //刷新组合号
            if (this.neuSpread1_Sheet1.RowCount > 0)
            {
                FS.HISFC.Components.Common.Classes.Function.DrawCombo(this.neuSpread1_Sheet1, (int)ColFeeItem.ComboCode, (int)ColFeeItem.Combo);
            }
        }

        /// <summary>
        /// 设置病人信息
        /// </summary>
        /// <param name="reg"></param>
        private void SetPatient(FS.HISFC.Models.RADT.Patient reg)
        {
            if (reg == null || reg.ID == "")
            {
                return;
            }
            else
            {
                this.txtName.Text = reg.Name;
                this.txtSex.Text = reg.Sex.Name;
                this.txtBirthday.Text = reg.Birthday.ToString("yyyy-MM-dd");
                this.txtAge.Text = this.outpatientFeeManager.GetAge(reg.Birthday);
                this.txtCardNo.Text = reg.PID.CardNO;
            }
        }

        /// <summary>
        /// 打印函数
        /// </summary>
        private void Print()
        {
            //重新设置
            ArrayList alPrint = new ArrayList();
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                if (FS.FrameWork.Function.NConvert.ToBoolean(this.neuSpread1_Sheet1.Cells[i, (int)ColFeeItem.Selected].Value))
                {
                    alPrint.Add(this.neuSpread1_Sheet1.Rows[i].Tag);
                }
            }
            if (alPrint.Count > 0)
            {
                //设置
                if (ucPrint != null)
                {
                    this.ucPrint.SetData(alPrint);
                }

                if (this.ucPrint != null)
                {
                    this.ucPrint.PrintBill();
                }
            }
        }
        #endregion

        #region 事件

        protected override void OnLoad(EventArgs e)
        {
            this.Init();
        }

        private void txtCardNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.Query();
            }
        }

        private void txtCardNo_Enter(object sender, EventArgs e)
        {
            this.txtCardNo.Select(0, this.txtCardNo.Text.Length);
        }

        private void txtCardNo_MouseClick(object sender, MouseEventArgs e)
        {
            this.txtCardNo.Select(0, this.txtCardNo.Text.Length);
        }

        private void txtInvoiceNO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.QueryInvoiceInfoByInvoice(this.txtInvoiceNO.Text.Trim());
            }
        }

        private void txtInvoiceNO_MouseClick(object sender, MouseEventArgs e)
        {
            this.txtInvoiceNO.Select(0, this.txtInvoiceNO.Text.Length);
        }

        private void txtInvoiceNO_Enter(object sender, EventArgs e)
        {
            this.txtInvoiceNO.Select(0, this.txtInvoiceNO.Text.Length);
        }

        private void tvRegInfo_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node != null && e.Node.Tag is FS.HISFC.Models.Registration.Register)
            {
                //查询所有的费用信息
                FS.HISFC.Models.Registration.Register reg = e.Node.Tag as FS.HISFC.Models.Registration.Register;
                if (reg == null || string.IsNullOrEmpty(reg.ID))
                {
                    return;
                }
                this.SetPatient(reg);

                ArrayList alFee = this.feeManage.QueryFeeItemListsByClinicNO(reg.ID);
                if (this.isShowNoFee)
                {
                    ArrayList alNoFee = this.feeManage.QueryChargedFeeItemListsByClinicNO(reg.ID);
                    if (alFee == null)
                    {
                        alFee = new ArrayList();
                    }
                    alFee.AddRange(alNoFee);

                }

                if (alFee != null)
                {
                    this.SetFeeInfo(alFee);
                }

                this.SelectAll(true);
            }
        }

        private void tvInvoice_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node != null && e.Node.Tag is FS.HISFC.Models.Fee.Outpatient.Balance)
            {
                //查询所有的费用信息
                FS.HISFC.Models.Fee.Outpatient.Balance balance = e.Node.Tag as FS.HISFC.Models.Fee.Outpatient.Balance;
                if (balance == null || string.IsNullOrEmpty(balance.Invoice.ID))
                {
                    return;
                }
                FS.HISFC.Models.Registration.Register reg = this.registerManager.GetByClinic(balance.Patient.ID);
                if (reg == null || string.IsNullOrEmpty(reg.ID))
                {
                    return;
                }

                this.SetPatient(reg);
                ArrayList alFee = this.outpatientFeeManager.QueryFeeItemListsByInvoiceNO(balance.Invoice.ID);
                if (alFee != null)
                {
                    this.SetFeeInfo(alFee);
                }

                this.SelectAll(true);

            }
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            this.Print();
            return base.OnPrint(sender, neuObject);
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            if (this.queryType == QueryType.RegInfo
                || !string.IsNullOrEmpty(this.txtCardNo.Text))
            {
                this.Query();
            }
            else
            {
                this.QueryInvoiceInfoByInvoice(this.txtInvoiceNO.Text);
            }
            return base.OnQuery(sender, neuObject);
        }

        private void neuSpread1_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (e.Column == (int)ColFeeItem.Selected && e.Row >= 0)
            {
                this.SelectedComb(FS.FrameWork.Function.NConvert.ToBoolean(this.neuSpread1_Sheet1.Cells[e.Row, e.Column].Value));
                this.SetPrintInfo();
            }
        }

        #endregion

        enum ColFeeItem
        {
            Selected = 0,
            SeeDoct,
            SeeDept,
            ExecDept,
            ItemName,
            ComboCode,
            Combo,
            Num,
            Frequency,
            Usage,
            Days,
            OrderMemo
        }

        /// <summary>
        /// 查询类别
        /// </summary>
        public enum QueryType
        {
            RegInfo,
            InvoiceInfo
        }

        public enum ItemType
        {
            Drug,
            Undrug,
            All
        }

        private void ucTreatmentQuery_Enter(object sender, EventArgs e)
        {
            this.Focus();
            this.txtCardNo.Select();
            this.txtCardNo.SelectAll();

        }

    }
}
