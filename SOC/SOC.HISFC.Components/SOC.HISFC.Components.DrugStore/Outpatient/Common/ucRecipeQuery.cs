using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.HISFC.Components.DrugStore.Outpatient.Common
{
    public partial class ucRecipeQuery : Base.ucDrugBaseComponent, FS.FrameWork.WinForms.Classes.IPreArrange
    {
        public ucRecipeQuery()
        {
            InitializeComponent();
            this.ntvRecipeTree.AfterSelect += new TreeViewEventHandler(ntvRecipeTree_AfterSelect);
            this.ntxtRecipeNO.KeyPress += new KeyPressEventHandler(ntxtRecipeNO_KeyPress);
            this.ntxtInvoiceNO.KeyPress += new KeyPressEventHandler(ntxtInvoiceNO_KeyPress);
            this.ntxtCardNO.KeyPress += new KeyPressEventHandler(ntxtCardNO_KeyPress);
            this.ntxtPatientName.KeyPress += new KeyPressEventHandler(ntxtPatientName_KeyPress);
        }

        ucRecipeDetail ucRecipeDetail = new ucRecipeDetail();
        ArrayList curDepts = new ArrayList();

        FS.SOC.HISFC.BizLogic.Pharmacy.DrugStore drugStoreMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.DrugStore();

        Hashtable hsTerminal = new Hashtable();

        #region 变量属性


        private bool isCheckPrivePower = true;

        /// <summary>
        /// 是否判断发药权限
        /// </summary>
        [Description("是否判断发药权限"), Category("设置"), Browsable(true)]
        public bool IsCheckPrivePower
        {
            get { return isCheckPrivePower; }
            set { isCheckPrivePower = value; }
        }

        private bool isChooseDeptWhenCheckPrive = true;

        /// <summary>
        /// 权限检测时是否需要用户选择科室
        /// </summary>
        [Description("权限检测时是否需要用户选择科室"), Category("设置"), Browsable(true)]
        public bool IsChooseDeptWhenCheckPrive
        {
            get { return isChooseDeptWhenCheckPrive; }
            set { isChooseDeptWhenCheckPrive = value; }
        }

        private string privePowerString = "0320+M1";

        [Description("使用窗口需要的权限,如：0320+M1"), Category("设置"), Browsable(true)]
        public string PrivePowerString
        {
            get { return privePowerString; }
            set { privePowerString = value; }
        }

        protected FS.FrameWork.Models.NeuObject priveDept;

        /// <summary>
        /// 权限科室，当前获取、显示数据的科室
        /// </summary>
        [Description("权限科室"), Category("非设置"), Browsable(false)]
        public FS.FrameWork.Models.NeuObject PriveDept
        {
            get { return priveDept; }
            set { priveDept = value;}
        }


        private FS.FrameWork.Models.NeuObject stockDept;

        /// <summary>
        /// 当前操作数据保存、扣库等动作的科室
        /// </summary>
        [Description("扣库科室"), Category("非设置"), Browsable(false)]
        public FS.FrameWork.Models.NeuObject StockDept
        {
            get { return stockDept; }
            set { stockDept = value; }
        }


        private FS.HISFC.Models.Pharmacy.DrugTerminal drugTerminal;

        /// <summary>
        /// 配药台
        /// </summary>
        [Description("配药台"), Category("非设置"), Browsable(false)]
        public FS.HISFC.Models.Pharmacy.DrugTerminal DrugTerminal
        {
            get { return drugTerminal; }
            set { drugTerminal = value; }
        }

        private FS.HISFC.Models.Pharmacy.DrugTerminal sendTerminal;

        /// <summary>
        /// 配药台
        /// </summary>
        [Description("发药窗"), Category("非设置"), Browsable(false)]
        public FS.HISFC.Models.Pharmacy.DrugTerminal SendTerminal
        {
            get { return sendTerminal; }
            set { sendTerminal = value; }
        }

        private Function.EnumOutpatintDrugOperType enumOutpatintDrugOperType = Function.EnumOutpatintDrugOperType.空闲;

        /// <summary>
        /// 操作状态
        /// 对于空闲状态才可以进行下一步操作
        /// </summary>
        [Description("操作状态"), Category("非设置"), Browsable(false)]
        public Function.EnumOutpatintDrugOperType EnumOutpatintDrugOperType
        {
            get
            {
                //需要检测处方树也是否在查询操作
                return enumOutpatintDrugOperType;
            }
            set
            {
                enumOutpatintDrugOperType = value;
            }
        }


        private int decimals = 2;

        [Description("金额小数位数"), Category("设置"), Browsable(true)]
        public int Decimals
        {
            get
            {
                return decimals;
            }
            set
            {
                decimals = value;
            }
        }

        /// <summary>
        /// 单位显示方式
        /// </summary>
        [Description("单位显示方式"), Category("设置"), Browsable(false)]
        public Function.EnumQtyShowType EnumQtyShowType
        {
            get { return this.ucRecipeDetail.EnumQtyShowType; }
            set { this.ucRecipeDetail.EnumQtyShowType = value; }
        }


        /// <summary>
        /// 是否需要选择配药台
        /// 对于多个配药台对应一个发药窗口的情况有可能用到此功能
        /// </summary>
        private bool isNeedChooseDrugTerminal = false;

        /// <summary>
        /// 是否需要选择配药台
        /// 对于多个配药台对应一个发药窗口的情况有可能用到此功能
        /// </summary>
        [Description("是否需要选择配药台"), Category("设置"), Browsable(true)]
        public bool IsNeedChooseDrugTerminal
        {
            get { return isNeedChooseDrugTerminal; }
            set { isNeedChooseDrugTerminal = value; }
        }       

        #endregion

        #region 接口

        FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IOutpatientShow myIOutpatientShow;
        /// <summary>
        /// 患者信息接口
        /// </summary>
        public FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IOutpatientShow IOutpatientShow
        {
            get
            {
                if (this.DesignMode)
                {
                    return null;
                }
                if (myIOutpatientShow == null)
                {
                    try
                    {
                        myIOutpatientShow = (InterfaceManager.GetOutpatientInfoLocalComponent() as FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IOutpatientShow);
                    }
                    catch
                    {
                        myIOutpatientShow = null;
                    }
                    if (myIOutpatientShow == null)
                    {
                        myIOutpatientShow = new ucPatientInfo();
                    }
                }
                if (myIOutpatientShow != null && this.ngbPatientInfo.Controls.Count == 0)
                {
                    Control c = myIOutpatientShow as Control;
                    if (c != null)
                    {
                        c.Dock = DockStyle.Fill;
                    }
                    this.ngbPatientInfo.Controls.Add(c);
                }
                return myIOutpatientShow;
            }
        }


        FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IOutpatientDrug myIOutpatientPrint;
        bool isCreate = true;

        /// <summary>
        /// 打印接口
        /// </summary>
        public FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IOutpatientDrug IOutpatientPrint
        {
            get
            {
                if (this.DesignMode)
                {
                    return null;
                }
                if (myIOutpatientPrint == null)
                {
                    try
                    {
                        myIOutpatientPrint = (InterfaceManager.GetOutpatientPrint() as FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IOutpatientDrug);
                    }
                    catch
                    {
                        myIOutpatientPrint = null;
                    }
                }

                return myIOutpatientPrint;
            }
        }

        FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IRecipeQueryConvert myIRecipeQueryConvert;


        #endregion

        #region IPreArrange 成员 权限，终端选择等

        public int PreArrange()
        {
            #region 权限科室
            FS.HISFC.BizLogic.Manager.UserPowerDetailManager userPowerDetailManager = new FS.HISFC.BizLogic.Manager.UserPowerDetailManager();
            this.PriveDept = ((FS.HISFC.Models.Base.Employee)userPowerDetailManager.Operator).Dept;
            this.StockDept = PriveDept.Clone();

            if (this.IsCheckPrivePower)
            {
                if (string.IsNullOrEmpty(PrivePowerString))
                {
                    PrivePowerString = "0320+M1";
                }
                if (PrivePowerString.Split('+').Length < 2)
                {
                    PrivePowerString = PrivePowerString + "+M1";
                }
                string[] prives = PrivePowerString.Split('+');

                if (this.IsChooseDeptWhenCheckPrive)
                {
                    int param = Function.ChoosePrivDept(prives[0], prives[1], ref this.priveDept);
                    if (param == 0 || param == -1)
                    {
                        return -1;
                    }
                    this.StockDept = priveDept.Clone();
                    this.ngbAdd.Text = "附加信息： 特别提醒您正在【" + this.priveDept.Name + "】操作处方查询，保存时会扣减【" + this.StockDept.Name + "】的库存";
                }
                else
                {
                    List<FS.FrameWork.Models.NeuObject> alDept = userPowerDetailManager.QueryUserPriv(userPowerDetailManager.Operator.ID, prives[0], prives[1]);
                    if (alDept == null || alDept.Count == 0)
                    {
                        this.ShowMessage("您没有权限！");
                        return -1;
                    }
                    foreach (FS.FrameWork.Models.NeuObject dept in alDept)
                    {
                        if (dept.ID == ((FS.HISFC.Models.Base.Employee)userPowerDetailManager.Operator).Dept.ID)
                        {
                            this.PriveDept = dept;
                            this.StockDept = PriveDept.Clone();
                            break;
                        }
                    }
                }

                if (this.PriveDept == null)
                {
                    this.ShowMessage("您没有权限！请重新登录！");
                    return -1;
                }
            }
            else
            {
                this.PriveDept = ((FS.HISFC.Models.Base.Employee)userPowerDetailManager.Operator).Dept;
                this.StockDept = PriveDept.Clone();
            }

            #endregion

            #region 终端信息
            this.SendTerminal = Function.TerminalSelect(this.PriveDept.ID, FS.HISFC.Models.Pharmacy.EnumTerminalType.发药窗口, true);

            if (this.SendTerminal == null)
            {
                return -1;
            }

            this.nlbTermialInfo.Text = "您选择的终端是：" + this.SendTerminal.Name;


            //使用配药台情况下，除了调用接口外，程序将以配药台设置为基础
            if (this.IsNeedChooseDrugTerminal)
            {
                this.DrugTerminal = Function.TerminalSelect(this.PriveDept.ID, SendTerminal.ID, true);

                if (this.DrugTerminal == null)
                {
                    return 0;
                }
                this.nlbTermialInfo.Text = "您选择的终端是：" + this.SendTerminal.Name + "； " + this.DrugTerminal.Name + "，目前已" + (this.DrugTerminal.IsClose ? "关闭" : "开放");

            }
           

            #endregion        

            return 1;
        }

        #endregion

        #region 配发药公用的工具栏

        FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        public FS.FrameWork.WinForms.Forms.ToolBarService ToolBarService
        {
            get { return toolBarService; }
            set { toolBarService = value; }
        }

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("打印处方", "手工打印或补打处方", FS.FrameWork.WinForms.Classes.EnumImageList.D打印, true, false, null);
            toolBarService.AddToolButton("打印标签", "手工打印或补打标签", FS.FrameWork.WinForms.Classes.EnumImageList.D打印, true, false, null);
            toolBarService.AddToolButton("打印清单", "手工打印或补打清单", FS.FrameWork.WinForms.Classes.EnumImageList.D打印, true, false, null);
            toolBarService.AddToolButton("打印药袋", "手工打印或补打药袋", FS.FrameWork.WinForms.Classes.EnumImageList.D打印, true, false, null);
            return toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "打印处方")
            {
                this.Print("Recipe");
            }
            else if (e.ClickedItem.Text == "打印标签")
            {
                this.Print("DrugLabel");

            }
            else if (e.ClickedItem.Text == "打印清单")
            {
                this.Print("DrugBill");
            }
            else if (e.ClickedItem.Text == "打印药袋")
            {
                this.Print("DrugBag");
            }

            base.ToolStrip_ItemClicked(sender, e);
        }

        #endregion

        #region 打印或补打

        /// <summary>
        /// 打印，此函数和调用接口中不更新打印状态
        /// 可能存在RecipeState=0的打印后也不更新状态情况，这个在具体的配发药界面控制
        /// </summary>
        /// <param name="type"></param>
        private void Print(string type)
        {
           if(this.ntvRecipeTree.SelectedNode.Tag is FS.HISFC.Models.Pharmacy.DrugRecipe)
           {
                if (this.IOutpatientPrint == null)
                {
                    this.ShowMessage("没有实现打印接口：FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IOutpatientDrug", MessageBoxIcon.Error);
                }
                else
                {
                    List<FS.HISFC.Models.Pharmacy.ApplyOut> listData = this.ucRecipeDetail.GetSelectInfo();
                    System.Collections.ArrayList alData = new System.Collections.ArrayList(listData);

                    FS.HISFC.Models.Pharmacy.DrugRecipe dr = this.ntvRecipeTree.SelectedNode.Tag as FS.HISFC.Models.Pharmacy.DrugRecipe;

                    FS.HISFC.Models.Pharmacy.DrugTerminal drugTerminal = this.SendTerminal;
                    if (drugTerminal == null)
                    {
                        drugTerminal = this.DrugTerminal;
                    }

                    if (type == "DrugBill")
                    {
                        this.IOutpatientPrint.PrintDrugBill(alData, dr, drugTerminal);
                    }
                    else if (type == "DrugLabel")
                    {
                        this.IOutpatientPrint.PrintDrugLabel(alData, dr, drugTerminal);
                    }
                    else if (type == "Recipe")
                    {
                        this.IOutpatientPrint.PrintRecipe(alData, dr, drugTerminal);
                    }
                    else if (type == "DrugBag")
                    {
                        this.IOutpatientPrint.PrintDrugBag(alData, dr, drugTerminal);
                    }
                    else
                    {
                        this.IOutpatientPrint.OnAutoPrint(alData,dr,"",drugTerminal);                            
                    }

                }
            }
        }

        ///// <summary>
        ///// 提供给打印按钮的方法，不更改打印状态
        ///// </summary>
        ///// <returns></returns>
        //private int PrintData()
        //{
        //    if (this.ntvRecipeTree.SelectedNode.Tag is FS.HISFC.Models.Pharmacy.DrugRecipe)
        //    {
        //        FS.HISFC.Models.Pharmacy.DrugRecipe dr = (FS.HISFC.Models.Pharmacy.DrugRecipe)this.ntvRecipeTree.SelectedNode.Tag;
        //        List<FS.HISFC.Models.Pharmacy.ApplyOut> alData = this.ucRecipeDetail.GetSelectInfo();
        //        ArrayList alPrintData = new ArrayList(alData);

        //        int param = this.Print(alPrintData, dr);
        //        return param;
        //    }

        //    return 0;
        //}

        #endregion

        /// <summary>
        /// 转换查询录入的发票号等
        /// </summary>
        public int InitIRecipeQueryConvert()
        {
            if (this.myIRecipeQueryConvert == null && !isCreate)
            {
                try
                {
                    isCreate = true;
                    myIRecipeQueryConvert = (FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IRecipeQueryConvert)InterfaceManager.GetRecipeQueryConvert();
                }
                catch (Exception ex)
                {
                    this.ShowMessage("接口FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IRecipeQueryConvert转换出错" + ex.Message, MessageBoxIcon.Error);
                    return -1;
                }
            }
            return 1;
        }

        protected int Init()
        {
            //初始化查询时间
            this.ndtpBeginTime.Value = DateTime.Now.Date.AddDays(-3);
            this.ndtpEndTime.Value = DateTime.Now.Date.AddDays(1);

            //初始化处方明细显示控件
            ucRecipeDetail.Dock = DockStyle.Fill;
            this.ngbRecipeDetail.Controls.Add(ucRecipeDetail);

            int param = this.InitRecipeDrugStockDept();
            try
            {
                this.IOutpatientShow.Clear();
                this.InitIRecipeQueryConvert();
            }
            catch { }
            return param;

        }

        protected int InitRecipeDrugStockDept()
        {
            //只可以查看操作员登录的科室
            this.nlbDeptInfo.Text = "你查询处方的范围是";
            Hashtable hsDept = new Hashtable();
            
            FS.HISFC.BizLogic.Manager.DepartmentStatManager deptStatMgr = new FS.HISFC.BizLogic.Manager.DepartmentStatManager();
            ArrayList alLogionDept = deptStatMgr.GetMultiDeptNew(deptStatMgr.Operator.ID);
            if (alLogionDept == null)
            {
                alLogionDept = new ArrayList();
            }
            foreach (FS.HISFC.Models.Base.DepartmentStat info in alLogionDept)
            {
                if (hsDept.Contains(info.DeptCode))
                {
                    continue;
                }
                hsDept.Add(info.DeptCode, new FS.FrameWork.Models.NeuObject(info.DeptCode, info.DeptName, ""));
            }
           
            FS.HISFC.BizLogic.Manager.UserPowerDetailManager priPowerMgr = new FS.HISFC.BizLogic.Manager.UserPowerDetailManager();
            List<FS.FrameWork.Models.NeuObject> alPriveDept = priPowerMgr.QueryUserPriv(priPowerMgr.Operator.ID, "0320", "M1");
            if (alPriveDept == null)
            {
                alPriveDept=new List<FS.FrameWork.Models.NeuObject>();
            }
            foreach (FS.FrameWork.Models.NeuObject info in alPriveDept)
            {
                if (hsDept.Contains(info.ID))
                {
                    continue;
                }
                hsDept.Add(info.ID, info);
            }


            foreach (FS.FrameWork.Models.NeuObject dept in hsDept.Values)
            {
                try
                {
                    string deptType = SOC.HISFC.BizProcess.Cache.Common.GetDept(dept.ID).DeptType.ID.ToString();
                    if (deptType == "P")
                    {
                        this.nlbDeptInfo.Text += "【" + dept.Name + "】";
                        curDepts.Add(dept);
                    }
                }
                catch { }
            }

            return 1;
        }

        protected int QueryList()
        {
            string recipeNO = "All";
            bool isInput = false;
            if (this.ntxtRecipeNO.Text != "")
            {
                recipeNO = this.ntxtRecipeNO.Text;
                isInput = true;
            }

            string invoiceNO = "All";
            if (this.ntxtInvoiceNO.Text != "")
            {
                invoiceNO = this.ntxtInvoiceNO.Text;
                if (this.myIRecipeQueryConvert != null)
                {
                    invoiceNO = this.myIRecipeQueryConvert.ConvertInvoiceNO(invoiceNO);
                }
                isInput = true;
            }

            string cardNO = "All";
            if (this.ntxtCardNO.Text != "")
            {
                cardNO = this.ntxtCardNO.Text;
                if (this.myIRecipeQueryConvert != null)
                {
                    cardNO = this.myIRecipeQueryConvert.ConvertCardNO(cardNO);
                }
                else
                {
                    cardNO = cardNO.PadLeft(10, '0');
                }
                isInput = true;
            }

            string patientName = "All";
            if (this.ntxtPatientName.Text != "")
            {
                patientName = this.ntxtPatientName.Text;
                isInput = true;
            }
            if (!isInput)
            {
                this.ShowMessage("请录入查询条件");
                return 0;
            }
            string depts = "";
            foreach (FS.FrameWork.Models.NeuObject info in this.curDepts)
            {
                depts += "'" + info.ID + "',";
            }
            depts = depts.TrimEnd(',');
            ArrayList alRecipe = drugStoreMgr.QueryRecipeList(depts, recipeNO, invoiceNO, cardNO, patientName, this.ndtpBeginTime.Value, this.ndtpEndTime.Value);
            if (alRecipe == null)
            {
                this.ShowMessage("请向系统管理员联系并报告发生的错误：" + drugStoreMgr.Err, MessageBoxIcon.Error);
                return -1;
            }
            if (alRecipe.Count == 0)
            {
                this.ShowMessage("没有查找到任何处方信息，请您确认\n1、录入的查询条件是否正确\n2、是否在选择时间范围内收费");
                return 0; 
            }
            int param = this.ShowList(alRecipe);
            return param;
        }

        protected int ShowList(ArrayList alRecipe)
        {
            if (alRecipe == null)
            {
                return -1;
            }
            Hashtable hsPatient = new Hashtable();
            Hashtable hsPatientNode = new Hashtable();
            foreach (FS.HISFC.Models.Pharmacy.DrugRecipe recipe in alRecipe)
            {
                TreeNode nodePatient = new TreeNode();
                if (hsPatient.Contains(recipe.CardNO))
                {
                    (hsPatient[recipe.CardNO] as ArrayList).Add(recipe);
                    nodePatient = hsPatientNode[recipe.CardNO] as TreeNode;
                }
                else
                {
                    ArrayList al = new ArrayList();
                    al.Add(recipe);
                   
                    hsPatientNode.Add(recipe.CardNO, nodePatient);

                    hsPatient.Add(recipe.CardNO, al);

                    nodePatient.Text = recipe.PatientName + "【" + recipe.CardNO + "】";
                    this.ntvRecipeTree.Nodes.Add(nodePatient);

                }

              
                TreeNode nodeRecipe = new TreeNode();
                string state = "未发药";
                string stockDept = "";
                string sendDept = "";
                string terminal = "";
                if (recipe.RecipeState == "3")
                {
                    state = "已发药";
                    sendDept = "-发药药房【" + SOC.HISFC.BizProcess.Cache.Common.GetDeptName(recipe.SendOper.Dept.ID) + "】";
                }
                else if (recipe.RecipeState == "4")
                {

                }
                else
                {
                }
                stockDept = "" + SOC.HISFC.BizProcess.Cache.Common.GetDeptName(recipe.StockDept.ID);
                if (hsTerminal.Contains(recipe.SendTerminal.ID))
                {
                    terminal = hsTerminal[recipe.SendTerminal.ID].ToString();
                }
                else
                {
                    try
                    {
                        terminal = this.drugStoreMgr.GetDrugTerminalById(recipe.SendTerminal.ID).Name;
                        hsTerminal.Add(recipe.SendTerminal.ID, terminal);
                    }
                    catch { }
                }
                nodeRecipe.Text = "处方号【" + recipe.RecipeNO + "】" + state + "-" + stockDept + "-" + terminal + sendDept;
                nodeRecipe.Tag = recipe;
                nodePatient.Nodes.Add(nodeRecipe);

            }
            this.ntvRecipeTree.ExpandAll();

            return 1;
        }

        /// <summary>
        /// 保存处方
        /// </summary>
        /// <returns></returns>
        private int Save()
        {
            if (!(this.ntvRecipeTree.SelectedNode.Tag is FS.HISFC.Models.Pharmacy.DrugRecipe))
            {
                return 0;
            }
            if (this.PriveDept == null || this.PriveDept.ID == "")
            {
                this.ShowMessage("请设置操作科室", MessageBoxIcon.Error);
                return -1;
            }

            int param = 1;
            bool unDruged = true;

            FS.HISFC.Models.Pharmacy.DrugRecipe dr = this.ntvRecipeTree.SelectedNode.Tag as FS.HISFC.Models.Pharmacy.DrugRecipe;
            if (dr.RecipeState == "3")
            {
                this.ShowMessage("该处方已经发药");
                return 0;
            }

          
            List<FS.HISFC.Models.Pharmacy.ApplyOut> alData = this.ucRecipeDetail.GetSelectInfo();
            if (alData == null || alData.Count <= 0)
            {
                return -1;
            }

            //判断是否已进行过发药处理
            if (alData != null && alData.Count > 0)
            {
                FS.HISFC.Models.Pharmacy.ApplyOut applyOut = alData[0] as FS.HISFC.Models.Pharmacy.ApplyOut;
                if (applyOut.State == "2")
                {
                    this.ShowMessage("该药品已发药 不需保存");
                    return -1;
                }
                if (applyOut.State == "1")
                {
                    unDruged = false;
                }
            }


            string info = "您正在【" + this.priveDept.Name + "】操作处方查询，保存时会扣减【" + this.StockDept.Name + "】的库存\n是否继续？";
            DialogResult d = MessageBox.Show(this, info, "特别提醒>>", MessageBoxButtons.YesNo);
            if (d == DialogResult.No)
            {
                return 0;
            }


            if (dr.RecipeState == "0")
            {
                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                param = this.drugStoreMgr.UpdateDrugRecipeState(dr.StockDept.ID, dr.RecipeNO, "M1", "0", "1");
                if (param == 1)
                {
                    FS.FrameWork.Management.PublicTrans.Commit();
                }
                else
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.ShowMessage("保存失败，数据可能已经变化，请重新查询");
                    return -1;
                }
            }

            //直接发药更新调剂数
            FS.HISFC.Models.Pharmacy.DrugTerminal terminal = this.SendTerminal;
            if (this.DrugTerminal != null && !string.IsNullOrEmpty(this.DrugTerminal.ID))
            {
                terminal = this.DrugTerminal;
            }

            param = Function.OutpatientSend(alData, terminal, this.StockDept.Clone(), this.drugStoreMgr.Operator, unDruged, true);

            if (param == 1)
            {
                this.ucRecipeDetail.Clear();
                this.IOutpatientShow.Clear();
                dr.RecipeState = "3";

                string state = "已发药";
                string stockDept = "";
                string sendDept = "";
                stockDept = "" + SOC.HISFC.BizProcess.Cache.Common.GetDeptName(dr.StockDept.ID);
                sendDept = "-发药药房【" + SOC.HISFC.BizProcess.Cache.Common.GetDeptName(StockDept.ID) + "】";

                this.ntvRecipeTree.SelectedNode.Text = "处方号" + dr.RecipeNO + "【" + state + "】-" + stockDept + sendDept;
            }
            else
            {
                return param;
            }
           
            return param;
        }

        protected override int OnSave(object sender, object neuObject)
        {
            return this.Save();
        }

        protected override void OnLoad(EventArgs e)
        {
            this.Init();
            base.OnLoad(e);
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            this.ntvRecipeTree.Nodes.Clear();
            this.QueryList();
            return base.OnQuery(sender, neuObject);
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            this.Print("");
            return 1;
        }

        void ntxtPatientName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (this.ntxtPatientName.Text != "")
                {
                    this.ntvRecipeTree.Nodes.Clear();
                    this.QueryList();
                }
            }
        }
        void ntxtCardNO_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (this.ntxtCardNO.Text != "")
                {
                    this.ntvRecipeTree.Nodes.Clear();
                    this.QueryList();
                }
            }
        }

        void ntxtInvoiceNO_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (this.ntxtInvoiceNO.Text != "")
                {
                    this.ntvRecipeTree.Nodes.Clear();
                    this.QueryList();
                }
            }
        }

        void ntxtRecipeNO_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (this.ntxtRecipeNO.Text != "")
                {
                    this.ntvRecipeTree.Nodes.Clear();
                    this.QueryList();
                }
            }
        }

        void ntvRecipeTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.ucRecipeDetail.Clear();
            this.IOutpatientShow.Clear();
            if (e.Node.Tag is FS.HISFC.Models.Pharmacy.DrugRecipe)
            {
                FS.HISFC.Models.Pharmacy.DrugRecipe r = (FS.HISFC.Models.Pharmacy.DrugRecipe)e.Node.Tag;
                this.IOutpatientShow.ShowInfo(r);
                string state = "0";
                if (r.RecipeState == "2")
                {
                    state = "1";
                }
                else if (r.RecipeState == "3")
                {
                    state = "2";
                }
                ArrayList alApplyOut = this.drugStoreMgr.QueryApplyOutListForClinic(r.StockDept.ID, "M1", state, r.RecipeNO);
                if (alApplyOut == null)
                {
                    this.ShowMessage("请与系统管理员联系并报告错误："+this.drugStoreMgr.Err, MessageBoxIcon.Error);
                    return;
                }
                this.ucRecipeDetail.ShowInfo(alApplyOut);

            }
        }

        #region 提示信息

        /// <summary>
        /// 提供在状态栏第一panel显示信息的功能
        /// </summary>
        /// <param name="text">显示信息的文本</param>
        protected void ShowStatusBarTip(string text)
        {
            if (this.ParentForm != null)
            {
                if (this.ParentForm is FS.FrameWork.WinForms.Forms.frmBaseForm)
                {
                    ((FS.FrameWork.WinForms.Forms.frmBaseForm)this.ParentForm).statusBar1.Panels[1].Text = "       " + text + "       ";
                }
            }
        }

        /// <summary>
        /// MessageBox统一形式
        /// </summary>
        /// <param name="text"></param>
        protected void ShowMessage(string text)
        {
            ShowMessage(text, MessageBoxIcon.Information);
        }

        /// <summary>
        /// MessageBox统一形式
        /// </summary>
        /// <param name="text"></param>
        protected void ShowMessage(string text, MessageBoxIcon messageBoxIcon)
        {
            string caption = "";
            switch (messageBoxIcon)
            {
                case MessageBoxIcon.Warning:
                    caption = "警告>>";
                    break;
                case MessageBoxIcon.Error:
                    caption = "错误>>";
                    break;
                default:
                    caption = "提示>>";
                    break;
            }

            MessageBox.Show(this, text, caption, MessageBoxButtons.OK, messageBoxIcon);
        }

        #endregion


    }
}
