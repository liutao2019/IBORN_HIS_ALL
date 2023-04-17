using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.BizProcess.Integrate.Terminal;
using FS.HISFC.Models.Account;
using FS.HISFC.Models.Base;
using FS.HISFC.Models.Pharmacy;
using FS.HISFC.Models.RADT;
using FS.HISFC.Models.Registration;
using FS.HISFC.Models.Terminal;
using FS.FrameWork.WinForms.Forms;
using FS.FrameWork.Management;
using FS.HISFC.Components.Terminal.Confirm;
using FS.HISFC.FSLocal.OutPatientFee;
using FS.HISFC.Models.Fee.Outpatient;
using FS.HISFC.BizLogic.Fee;
namespace FS.SOC.Local.OutpatientFee.FoSi
{
    /// <summary>
    /// 终端扣费
    /// 创建时间 2010-12-10
    /// 
    /// 按执行科室进行扣费，
    /// 通过菜单维护配置执行科室；
    /// 多个执行科室按 '|' 号隔开；
    /// 不维护表示不分执行科室
    /// {D4E43582-44C5-4f84-B049-4E8902E927AB}
    /// </summary>
    public partial class ucQueryOutPatientFee : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucQueryOutPatientFee()
        {
            InitializeComponent();

            if (!DesignMode)
            {
                this.ucPatientTree.TreeNodeKeyDown += new ucPatientTree.delegateKeyDown(ucPatientTree_TreeNodeKeyDown);
                this.ucPatientTree1.TreeNodeKeyDown += new ucPatientTree.delegateKeyDown(ucPatientTree_TreeNodeKeyDown);
                this.ucPatientTree1.ClickTreeNode += new ucPatientTree.delegatePatientTree(ucPatientTree_ClickTreeNode);
            }
        }

        #region 变量

        /// <summary>
        /// 窗口类型：3-门诊预交金;
        /// </summary>
        string windowType = "3";
        /// <summary>
        /// 患者实体
        /// </summary>
        FS.HISFC.Models.Registration.Register register = new Register();

        FS.HISFC.BizLogic.Registration.Register registerMgr = new FS.HISFC.BizLogic.Registration.Register();

        /// <summary>
        /// 执行科室链表,通过参数读取
        /// </summary>
        List<string> lstExcuDept = null;

        private DataSet dsItem = new DataSet();

        /// <summary>
        /// 终端确认扣费
        /// </summary>
        protected event System.EventHandler ConfirmEvent;
        /// <summary>
        /// 显示全部事件
        /// </summary>
        protected event System.EventHandler ShowAllEvent;
        /// <summary>
        /// 查询历史扣费记录
        /// </summary>
        protected event System.EventHandler QueryHistory;
        /// <summary>
        /// 是否显示所有，包含未扣费的
        /// </summary>
        protected bool isShowAll = true;

        /// <summary>
        /// 是否显示全部最小费用
        /// </summary>
        protected bool isShowAllMinFee = false;

        /// <summary>
        /// 是否显示为复合项目
        /// </summary>
        protected bool isShowCombItem = true;

        /// <summary>
        /// 是否按执行科室过滤患者列表，否则按最小费用
        /// </summary>
        protected bool isShowPatientByExcu = true;

        /// <summary>
        /// 显示哪些最小费用
        /// </summary>
        protected string showMinFee = "";

        /// <summary>
        /// 是否只显示执行科室内容
        /// </summary>
        protected bool IsShowExcuOnly = true;

        /// <summary>
        /// 收费保存后调用接口
        /// </summary>
        FS.HISFC.BizProcess.Interface.Fee.IOutpatientAfterFee iOutpatientAfterFee = null;

        #endregion

        #region 属性
        /// <summary>
        /// 患者实体
        /// </summary>
        public FS.HISFC.Models.Registration.Register Register
        {
            get
            {
                return this.register;
            }
            set
            {
                this.register = value;
            }
        }
        /// <summary>
        ///  是否显示所有，包含未扣费的
        /// </summary>
        [Category("控件设置"), Description(" 是否显示所有，包含未扣费的")]
        public bool IsShowAll
        {
            get { return isShowAll; }
            set
            {
                isShowAll = value;
            }
        }

        /// <summary>
        ///  是否显示为复合项目
        /// </summary>
        [Category("控件设置"), Description(" 是否显示为复合项目")]
        public bool IsShowCombItem
        {
            get { return isShowCombItem; }
            set
            {
                isShowCombItem = value;
            }
        }

        /// <summary>
        ///  是否按执行科室过滤患者列表，否则按最小费用
        /// </summary>
        [Category("控件设置"), Description(" 是否按执行科室过滤患者列表，否则按最小费用")]
        public bool IsShowPatientByExcu
        {
            get { return isShowPatientByExcu; }
            set
            {
                isShowPatientByExcu = value;
            }
        }

        /// <summary>
        /// 是否显示全部最小费用
        /// </summary>
        [Category("控件设置"), Description(" 是否显示全部最小费用")]
        public bool IsShowAllMinFee
        {
            get { return isShowAllMinFee; }
            set
            {
                isShowAllMinFee = value;
            }
        }

        /// <summary>
        /// 显示哪些最小费用
        /// </summary>
        [Category("控件设置"), Description(" 显示哪些最小费用")]
        public string ShowMinFee
        {
            get { return showMinFee; }
            set
            {
                showMinFee = value;
            }
        }
        public bool IsUnFee
        {
            get
            {
                return this.neuTabControl1.SelectedTab == this.tpUnFee;
            }
            set
            {
                this.neuTabControl1.SelectedTab = value ? this.tpUnFee : this.tpFeed;
            }
        }

        #endregion

        #region 事件

        #region 初始化
        /// <summary>
        /// 初始化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override ToolBarService OnInit(object sender, object neuObject, object param)
        {
            if (this.DesignMode)
            {
                return base.OnInit(sender, neuObject, param);
            }
            // 按钮对应事件
            this.ShowAllEvent += new EventHandler(ucTerminalConfirm_ShowAllEvent);
            this.QueryHistory += new EventHandler(ucAccountTerminalForDoc_QueryHistory);
            patientCaseEventHandler += new EventHandler(ucTerminalConfirm_PatientCaseEvent);
            // 显示按钮
            this.toolbarService.AddToolButton("显示全部", "显示全部项目(F5)", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q全选, true, false, this.ShowAllEvent);
            this.toolbarService.AddToolButton("门诊病历", "查看当前患者的门诊病历信息(F11)", (int)FS.FrameWork.WinForms.Classes.EnumImageList.B病历, true, false, patientCaseEventHandler);
            this.toolbarService.AddToolButton("查询扣费", "查询历史扣费", (int)FS.FrameWork.WinForms.Classes.EnumImageList.C查询历史, true, false, this.QueryHistory);
            this.toolbarService.AddToolButton("清屏", "清屏", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q清空, true, false, null);
            return this.toolbarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "清屏":
                    {
                        this.ClearArr();
                        break;
                    }
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        

        /// <summary>
        /// 加载窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucAccountTerminalForDoc_Load(object sender, EventArgs e)
        {

            lstExcuDept = new List<string>();

            string strTemp = this.Tag.ToString();
            if (!string.IsNullOrEmpty(strTemp))
            {
                if (strTemp.ToLower() == "all")
                {
                    lstExcuDept.Clear();
                }
                else
                {
                    lstExcuDept.AddRange(strTemp.Split(new char[] { '|' }));
                }
            }
            else
            {
                FS.HISFC.Models.Base.Employee employee = this.outPateientFeeManage.Operator as FS.HISFC.Models.Base.Employee;
                lstExcuDept.Add(employee.Dept.ID);

            }

            this.ucPatientTree.WindowType = "3";

            this.InitUC();
            this.ucPatientInformation1.Enabled = true;
            this.txtCardNO.Focus();
            this.txtCardNO.Focus();

            this.SetFocus();
        }

        #endregion

        private void txtCardNO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;

            this.ClearArr();

            string strCard = this.txtCardNO.Text.Trim().PadLeft(10, '0');
            this.txtCardNO.Text = "";

            if (string.IsNullOrEmpty(strCard))
            {
                this.txtCardNO.SelectAll();
                return;
            }
            this.txtCardNO.Clear();

            int iReturn = 0;

            iReturn=this.QueryInfo(strCard, true);
            if (iReturn == -1)
            {
                FS.HISFC.Models.Account.AccountCard objCard = new FS.HISFC.Models.Account.AccountCard();
                int iTemp = feeManage.ValidMarkNO(strCard, ref objCard);
                if (iTemp <= 0 || objCard == null)
                {
                    MessageBox.Show("无效卡号，请联系管理员！");
                    return ;
                }
                this.txtCardNO.Text = objCard.Patient.PID.CardNO.ToString();
            }

        }

        /// <summary>
        /// 单击树结点设置当前患者
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ucPatientTree_ClickTreeNode(object sender, TreeNodeMouseClickEventArgs e)
        {
            ClearArr();

            if (e.Node.Tag == null)
                return;

            Register objRegister = e.Node.Tag as Register;
            if (objRegister == null)
                return;
            string errInfo = "";
            if (this.QueryAndShowFeeItemLists(objRegister, ref errInfo) != 1)
            {
                Function.ShowMsg("系统提示", errInfo);
                this.SetFocus();
                return;
            }
            this.SetFocus();
        }

        /// <summary>
        /// 树节点按键事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ucPatientTree_TreeNodeKeyDown(object sender, KeyEventArgs e)
        {

            TreeNode currentNode = this.ucPatientTree.GetCurrentNode();
            if (currentNode == null || currentNode.Tag == null)
            {
                this.SetFocus();
                return;
            }

            ClearArr();

            Register objRegister = currentNode.Tag as Register;
            if (objRegister == null)
                return;
            string errInfo = "";
            if (this.QueryAndShowFeeItemLists(objRegister, ref errInfo) != 1)
            {
                Function.ShowMsg("系统提示", errInfo);
            }

            this.SetFocus();
        }

        /// <summary>
        /// 查询历史扣费记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ucAccountTerminalForDoc_QueryHistory(object sender, EventArgs e)
        {
            string beginDate = this.dtBeginDate.Value.ToString("yyyy-MM-dd 00:00:00");
            string endDate = this.dtEndTime.Value.ToString("yyyy-MM-dd 23:59:59");
            ArrayList arlRegister = null;

            FS.HISFC.Models.Base.Employee employee = this.outPateientFeeManage.Operator as FS.HISFC.Models.Base.Employee;
            string strDeptID = employee.Dept.ID;
            string minFee = "";
            if (string.IsNullOrEmpty(this.txtCardNO.Text))
            {
                if (this.isShowPatientByExcu)
                {
                    arlRegister = registerMgr.QueryRegisterByFeeExcuDeptOrderByRegDate(strDeptID, beginDate, endDate);
                }
                else
                {
                    if (string.IsNullOrEmpty(this.showMinFee))
                    {
                        if (this.isShowAllMinFee)
                        {
                            minFee = "All";
                        }
                        else
                        {
                            MessageBox.Show("请维护要查询的最小费用,或设置查询全部最小费用");
                            return;
                        }
                    }
                    else
                    {
                        minFee = this.showMinFee;
                    }
                    arlRegister = registerMgr.QueryRegisterByMinFeeOrderByRegDate(minFee, beginDate, endDate);
                }
            }
            else
            {
                string cardNo=this.txtCardNO.Text.Trim().PadLeft(10, '0');
                //arlRegister = registerMgr.QueryRegisterByFeeExcuDeptAndCardNoOrderByRegDate(strDeptID, beginDate, endDate, cardNo);
                this.QueryInfo(cardNo, true);
                return;
            }
            if (arlRegister == null || arlRegister.Count <= 0)
            {
                Function.ShowMsg("系统提示", "无历史扣费记录!");
                return;
            }
            this.neuTabControl1.SelectedTab = this.tpFeed;
            this.ucPatientTree.ClearPatient();
            this.ucPatientTree.AddNodes(arlRegister);
        }

        /// <summary>
        /// 显示全部事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ucTerminalConfirm_ShowAllEvent(object sender, EventArgs e)
        {
            isShowAll = !isShowAll;
            if (isShowAll)
            {
                ((System.Windows.Forms.ToolStripItem)(sender)).Text = "显示全部";
            }
            else
            {
                ((System.Windows.Forms.ToolStripItem)(sender)).Text = "取消显示全部";
            }
        }
        /// <summary>
        /// 按键事件
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.F7)
            {

                this.SetFocus();

                return true;
            }
            else if (keyData == Keys.F5)
            {
                return true;
            }
            else if (keyData.GetHashCode() == Keys.Alt.GetHashCode() + Keys.P.GetHashCode())
            {
                // 切换焦点到患者列表
                this.SetFocus();

                return true;
            }
            else if (keyData.GetHashCode() == Keys.Alt.GetHashCode() + Keys.A.GetHashCode())
            {
                // 切换焦点到患者检索文本框
                this.ucPatientInformation1.SetFocus();

                return true;
            }
            else if (keyData == Keys.F1)
            {
                txtCardNO.Focus();
                return true;
            }
            else if (keyData == Keys.F11)
            {
                this.patientCaseEventHandler(null, null);
                return true;
            }

            return base.ProcessDialogKey(keyData);
        }

        #region 医技终端增加门诊病历内容查询功能，供医技人员参考用

        void ucTerminalConfirm_PatientCaseEvent(object sender, EventArgs e)
        {
            if (this.IOutpatientCaseInstance == null)
            {
                this.IOutpatientCaseInstance = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Terminal.IOutpatientCase)) as FS.HISFC.BizProcess.Interface.Terminal.IOutpatientCase;
            }
            if (this.IOutpatientCaseInstance != null)
            {
                this.IOutpatientCaseInstance.IsBrowse = true;
                this.IOutpatientCaseInstance.InitUC();
            }
            else
            {
                MessageBox.Show("门诊病历数据显示接口未维护", "提示");
            }
            if (this.ucPatientTree.GetCurrentNode() == null)
            {
                return;
            }
            TreeNode currentNode = this.ucPatientTree.GetCurrentNode();
            if (currentNode == null)
            {
                return;
            }
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在进行门诊病历数据加载 请稍候...");
            Application.DoEvents();
            // 获取单击的树结点的Tag属性,里面承载着患者基本信息
            try
            {
                FS.HISFC.Models.Registration.Register registerInfo = currentNode.Tag as FS.HISFC.Models.Registration.Register;
                if (registerInfo == null)
                {
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    return;
                }
                this.IOutpatientCaseInstance.Register = registerInfo;
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                this.IOutpatientCaseInstance.Show();
            }
            catch (Exception ex)
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #endregion

        #region 设置焦点
        /// <summary>
        /// 设置焦点
        /// </summary>
        public void SetFocus()
        {
            txtCardNO.Focus();
            txtCardNO.SelectAll();
        }
        #endregion

        #region 内部方法

        /// <summary>
        /// 初始化UC
        /// </summary>
        private void InitUC()
        {
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("请等待，正在进行初始化...");
            Application.DoEvents();
            // 设置窗口类型：1-门诊用；2-住院用

            this.ucPatientInformation1.WindowType = this.windowType;
            this.ucPatientTree.WindowType = this.windowType;

            // 初始化各子UC
            this.ucPatientTree.isLoad = true;
            this.ucPatientTree.Init();

            this.ucPatientTree1.WindowType = this.windowType;

            // 初始化各子UC
            this.ucPatientTree1.isLoad = true;
            this.ucPatientTree1.Init();

            // 患者树事件代理
            this.ucPatientTree.ClickTreeNode += new ucPatientTree.delegatePatientTree(ucPatientTree_ClickTreeNode);

            // 患者检索事件

            this.ucPatientInformation1.WindowType = this.windowType;
            this.ucPatientInformation1.Clear();

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
        }

        /// <summary>
        /// 清空信息
        /// </summary>
        private void ClearArr()
        {
            this.ucPatientInformation1.Clear();
            this.ucChargeItem.ClearFP();

        }

        private int QueryInfo(string cardNO, bool isTip)
        {
         
            string beginDate = this.dtBeginDate.Value.ToString("yyyy-MM-dd 00:00:00");
            string endDate = this.dtEndTime.Value.ToString("yyyy-MM-dd 23:59:59");
            ArrayList arlRegister = null;
            FS.HISFC.Models.Registration.Register objRegister=new Register();

            FS.HISFC.Models.Base.Employee employee = this.outPateientFeeManage.Operator as FS.HISFC.Models.Base.Employee;
            string strDeptID = employee.Dept.ID;
            string minFee = "";

            if (this.isShowPatientByExcu)
            {
                arlRegister = registerMgr.QueryRegisterByFeeExcuDeptAndCardNoOrderByRegDate(strDeptID, beginDate, endDate, cardNO);
            }
            else
            {
                if (string.IsNullOrEmpty(this.showMinFee))
                {
                    if (this.isShowAllMinFee)
                    {
                        minFee = "All";
                    }
                    else
                    {
                        MessageBox.Show("请维护要查询的最小费用,或设置查询全部最小费用");
                        return -1;
                    }
                }
                else
                {
                    minFee = this.showMinFee;
                }
                arlRegister = registerMgr.QueryRegisterByMinFeeAndCardNoOrderByRegDate(minFee, beginDate, endDate, cardNO);
            }
            if (arlRegister == null || arlRegister.Count <= 0)
            {
                Function.ShowMsg("系统提示", "无历史扣费记录!");
                return -1;
            }

            objRegister = (FS.HISFC.Models.Registration.Register)arlRegister[0];
            this.ucPatientTree1.ClearPatient();
            this.ucPatientTree1.AddNoDeptNodes(objRegister, arlRegister);
            this.neuTabControl1.SelectedTab = this.tpUnFee;
            string errInfo = "";
            if (this.QueryAndShowFeeItemLists(objRegister, ref errInfo) != 1)
            {
                Function.ShowMsg("系统提示", errInfo);
                this.SetFocus();
                return -1;
            }
            this.SetFocus();

            return 1;
        }

        /// <summary>
        /// 获取收费信息，指定执行科室
        /// </summary>
        /// <param name="clinicNO"></param>
        /// <param name="allFeeItemList">所有收费项目</param>
        /// <param name="excuFeeItemList">指定执行科室项目</param>
        /// <returns></returns>
        private int QueryChargedFeeItemLists(string clinicNO, out ArrayList allFeeItemList, out ArrayList excuFeeItemList)
        {
            allFeeItemList = null;
            excuFeeItemList = null;

            allFeeItemList = outPateientFeeManage.QueryChargedFeeItemListsByClinicNO(clinicNO);
            if (isShowAll)
            {
                ArrayList arlHasFeeItem = outPateientFeeManage.QueryFeeItemListsByClinicNOAndValid(clinicNO);
                if (arlHasFeeItem != null && arlHasFeeItem.Count > 0)
                {
                    allFeeItemList.AddRange(arlHasFeeItem.ToArray());
                }
            }

            GetExcuFeeItemLists(allFeeItemList, out excuFeeItemList);

            if (allFeeItemList == null || allFeeItemList.Count == 0)
            {
                return 0;
            }
            return 1;
        }
        /// <summary>
        /// 获取指定执行科室的费用信息
        /// </summary>
        /// <param name="allFeeItemList"></param>
        /// <param name="excuFeeItemList"></param>
        private void GetExcuFeeItemLists(ArrayList allFeeItemList, out ArrayList excuFeeItemList)
        {
            excuFeeItemList = null;
            if (allFeeItemList != null && allFeeItemList.Count > 0)
            {
                if (lstExcuDept == null || lstExcuDept.Count <= 0)
                {
                    // 执行科室为空，收取所有费用，否则只收取指定执行科室费用
                    excuFeeItemList = new ArrayList();
                    foreach (FeeItemList feeItem in allFeeItemList)
                    {
                        if (feeItem.IsAccounted)
                        {
                            continue;
                        }

                        excuFeeItemList.Add(feeItem);
                    }
                }
                else
                {
                    excuFeeItemList = new ArrayList();
                    foreach (FeeItemList feeItem in allFeeItemList)
                    {
                        if (lstExcuDept.Contains(feeItem.ExecOper.Dept.ID) && !feeItem.IsAccounted)
                        {
                            excuFeeItemList.Add(feeItem);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 查询和显示费用信息
        /// </summary>
        /// <param name="regInfo"></param>
        private int QueryAndShowFeeItemLists(Register regInfo, ref string errInfo)
        {
            ArrayList allFeeItemList = null;
            ArrayList excuFeeItemList = null;
            int rev = this.QueryChargedFeeItemLists(regInfo.ID, out allFeeItemList, out excuFeeItemList);
            if (rev == 0)
            {
                errInfo = "无相关未扣费用信息！";
                return 0;
            }

            return ShowFeeItemLists(regInfo, allFeeItemList, excuFeeItemList, ref errInfo);
        }
        /// <summary>
        /// 查询和显示费用信息
        /// </summary>
        /// <param name="regInfo"></param>
        /// <param name="allFeeItemList"></param>
        /// <param name="excuFeeItemList"></param>
        /// <returns></returns>
        private int ShowFeeItemLists(Register regInfo, ArrayList allFeeItemList, ArrayList excuFeeItemList, ref string errInfo)
        {

            this.ucPatientInformation1.Register = regInfo;

            //Hashtable 用来判断是否组套项目
            Hashtable hsPackageItem = new Hashtable();
            // 显示收费信息
            try
            {
                //处理只显示某些最小费用
                //是否显示全部最小费用
                if (!isShowAllMinFee)
                {
                    ArrayList allFeeItemListTemp = new ArrayList();
                    foreach (FeeItemList feeItemList in allFeeItemList)
                    {
                        //维护的最小费用是否包含当前项目的最小费用
                        if (this.showMinFee.Contains(feeItemList.Item.MinFee.ID))
                        {
                            allFeeItemListTemp.Add(feeItemList);
                        }
                    }
                    allFeeItemList = allFeeItemListTemp;
                }
                //把拆分的组套变为复合项目
                if (this.isShowCombItem)
                {
                    ArrayList allFeeItemListTemp1 = new ArrayList();
                    foreach (FeeItemList feeItemList in allFeeItemList)
                    {
                        if (feeItemList.Item.ItemType != EnumItemType.UnDrug)
                        {
                            allFeeItemListTemp1.Add(feeItemList);
                            continue;
                        }
                        if (feeItemList.PayType != FS.HISFC.Models.Base.PayTypes.Balanced)
                        {
                            allFeeItemListTemp1.Add(feeItemList);
                            continue;
                        }
                        if (string.IsNullOrEmpty(feeItemList.UndrugComb.ID) == false)
                        {
                            if (hsPackageItem.ContainsKey(feeItemList.Order.Combo.ID + feeItemList.TransType.ToString() + "|" + feeItemList.UndrugComb.ID))
                            {
                                ((ArrayList)hsPackageItem[feeItemList.Order.Combo.ID + feeItemList.TransType.ToString() + "|" + feeItemList.UndrugComb.ID]).Add(feeItemList);
                            }
                            else
                            {
                                //新建
                                ArrayList al = new ArrayList();
                                al.Add(feeItemList);
                                hsPackageItem[feeItemList.Order.Combo.ID + feeItemList.TransType.ToString() + "|" + feeItemList.UndrugComb.ID] = al;
                            }
                            continue;
                        }
                        allFeeItemListTemp1.Add(feeItemList);
                    }

                    if (hsPackageItem.Count > 0)
                    {
                        foreach (DictionaryEntry de in hsPackageItem)
                        {
                            string packagecode = de.Key.ToString().Split('|')[1];
                            ArrayList al = de.Value as ArrayList;
                            if (al != null && al.Count > 0)
                            {
                                FeeItemList feeItemListFirst = al[0] as FeeItemList;

                                decimal qty = feeItemListFirst.Item.Qty;
                                decimal sumCost = 0.00M;
                                decimal sumPubCost = 0.00M;
                                decimal sumPayCost = 0.00M;
                                decimal sumOwnCost = 0.00M;
                                foreach (FeeItemList feeItemList in al)
                                {
                                    if (feeItemList.Item.PackQty == 0)
                                    {
                                        feeItemList.Item.PackQty = 1;
                                    }
                                    sumPubCost += feeItemList.FT.PubCost;
                                    sumPayCost += feeItemList.FT.PayCost;
                                    sumOwnCost += feeItemList.FT.OwnCost;
                                    sumCost += feeItemList.FT.PubCost + feeItemList.FT.PayCost + feeItemList.FT.OwnCost;
                                }
                                //查找复合项目
                                FS.HISFC.Models.Fee.Item.Undrug undrug = this.feeManage.GetUndrugByCode(packagecode);

                                feeItemListFirst.Item = undrug;
                                if (feeItemListFirst.Item.PackQty == 0)
                                {
                                    feeItemListFirst.Item.PackQty = 1;
                                }
                                feeItemListFirst.Item.Qty = qty;
                                feeItemListFirst.FT.PubCost = sumPubCost;
                                feeItemListFirst.FT.PayCost = sumPayCost;
                                feeItemListFirst.FT.OwnCost = feeItemListFirst.Item.Price;
                                allFeeItemListTemp1.Add(feeItemListFirst);
                            }
                        }
                    }
                    allFeeItemList = allFeeItemListTemp1;
                }
                this.ucChargeItem.IsShowDoseOnce = false;
                this.ucChargeItem.IsShowSpecs = false;
                this.ucChargeItem.IsShowUsage = false;
                if (this.ucChargeItem.ShowFeeItemList(allFeeItemList) <= 0)
                {
                    return 0;
                }

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
            return 1;
        }


        /// <summary>
        /// 获取执行科室名称
        /// </summary>
        /// <param name="allFeeItemList"></param>
        /// <returns></returns>
        private string GetExcueDeptName(ArrayList allFeeItemList)
        {
            if (lstExcuDept == null)
            {
                lstExcuDept = new List<string>();
            }
            if (allFeeItemList == null)
            {
                return "";
            }

            string strExcuDeptName = "";
            foreach (FeeItemList feeItem in allFeeItemList)
            {
                if (!lstExcuDept.Contains(feeItem.ExecOper.Dept.ID))
                {
                    if (!strExcuDeptName.Contains(feeItem.ExecOper.Dept.Name))
                    {
                        strExcuDeptName += feeItem.ExecOper.Dept.Name + "、";
                    }
                }
            }
            if (!string.IsNullOrEmpty(strExcuDeptName))
            {
                strExcuDeptName = strExcuDeptName.Trim(new char[] { '、' });
            }
            return strExcuDeptName;
        }
        /// <summary>
        /// 收费传入的数组，
        /// </summary>
        /// <param name="alFeeItemList"></param>
        /// <returns></returns>
        private ArrayList ConvertGroupToDetail(FS.HISFC.Models.Registration.Register r, FS.HISFC.Models.Fee.Outpatient.FeeItemList f)
        {
            ArrayList alItem = new ArrayList();
            return alItem;
        }
        #endregion

        #region 业务管理
        /// <summary>
        /// 工具栏服务
        /// </summary>
        FS.FrameWork.WinForms.Forms.ToolBarService toolbarService = new ToolBarService();
        /// <summary>
        /// 业务管理类
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Manager conl = new FS.HISFC.BizProcess.Integrate.Manager();
        /// <summary>
        /// 体检管理类
        /// </summary>
        FS.HISFC.BizProcess.Integrate.PhysicalExamination.ExamiManager examMgr = new FS.HISFC.BizProcess.Integrate.PhysicalExamination.ExamiManager();
        /// <summary>
        /// 挂号管理类
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Registration.Registration registerIntegrate = new FS.HISFC.BizProcess.Integrate.Registration.Registration();
        /// <summary>
        /// 预交金扣费管理
        /// </summary>
        FS.HISFC.FSLocal.OutPatientFee.OutPatientFeeManage accountPatientFeeManage = new OutPatientFeeManage();
        /// <summary>
        /// 门诊费用业务类
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.Outpatient outPateientFeeManage = new FS.HISFC.BizLogic.Fee.Outpatient();
        /// <summary>
        /// 控制业务层
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
        /// <summary>
        /// 门诊病历接口显示
        /// </summary>
        private FS.HISFC.BizProcess.Interface.Terminal.IOutpatientCase IOutpatientCaseInstance = null;
        /// <summary>
        /// 费用业务层
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Fee feeManage = new FS.HISFC.BizProcess.Integrate.Fee();
        protected event System.EventHandler patientCaseEventHandler;
        /// <summary>
        /// 帐户管理
        /// </summary>
        FS.HISFC.BizLogic.Fee.Account accountManage = new FS.HISFC.BizLogic.Fee.Account();
        FS.HISFC.BizLogic.Fee.Item itemManager = new FS.HISFC.BizLogic.Fee.Item();

        #endregion

        private void neuTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }

    public enum EnumShowItemType
    {
        /// <summary>
        /// 药品
        /// </summary>
        Pharmacy,
        /// <summary>
        /// 非药品
        /// </summary>
        Undrug,
        /// <summary>
        /// 全部
        /// </summary>
        All,
        /// <summary>
        /// 科常用
        /// </summary>
        DeptItem
    }
}

