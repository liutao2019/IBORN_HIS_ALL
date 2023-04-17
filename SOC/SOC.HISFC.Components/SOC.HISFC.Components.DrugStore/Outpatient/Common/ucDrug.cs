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
    /// <summary>
    /// [功能描述: 门诊药房配药]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2010-12]<br></br>
    /// 说明：
    /// </summary>
    public partial class ucDrug : ucDrugBase, FS.FrameWork.WinForms.Classes.IPreArrange
    {
        public ucDrug()
        {
            InitializeComponent();
        }

        #region 变量及属性，不包括自动刷新相关的变量及属性
        /// <summary>
        /// 时间间隔
        /// 保留加载非当天处方的功能
        /// </summary>
        private uint daySpan = 0;

        /// <summary>
        /// 刷新开始间隔与今天的间隔天数
        /// 保留加载非当天处方的功能
        /// </summary>
        [Description("刷新开始间隔与今天的间隔天数"), Category("设置"), Browsable(true)]
        public uint DaySpan
        {
            get
            {
                return daySpan;
            }
            set
            {
                daySpan = value;
            }
        }

        /// <summary>
        /// 是否使用终端自动打印注册功能
        /// </summary>
        private bool isRegisterAutoPrint = true;

        /// <summary>
        /// 是否使用终端自动打印注册功能
        /// </summary>
        [Description("是否使用终端自动打印注册功能"), Category("设置"), Browsable(true)]
        public bool IsRegisterAutoPrint
        {
            get { return isRegisterAutoPrint; }
            set { isRegisterAutoPrint = value; }
        }


        FS.SOC.HISFC.BizLogic.Pharmacy.DrugStore drugStoreMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.DrugStore();
        //FS.SOC.HISFC.BizLogic.Pharmacy.Item itemMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.Item();

        #endregion

        #region IPreArrange 成员 权限，终端选择等

        public int PreArrange()
        {
            if (this.DesignMode)
            {
                return 0;
            }

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
                    this.ngbAdd.Text = "附加信息： 特别提醒您正在配发【" + this.priveDept.Name + "】的处方，保存时会扣减【" + this.StockDept.Name + "】的库存";
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

                if (this.IsOtherDeptDrug)
                {
                    int param = Function.ChoosePrivDept(prives[0], prives[1], ref this.priveDept);
                    if (param == 0 || param == -1)
                    {
                        return -1;
                    }
                    this.ngbAdd.Text = "附加信息： 特别提醒您正在调配【" + this.priveDept.Name + "】的处方";
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

            this.DrugTerminal = Function.TerminalSelect(this.PriveDept.ID, FS.HISFC.Models.Pharmacy.EnumTerminalType.配药台, true);

            if (this.DrugTerminal == null)
            {
                //必须返回，否则没有数据查询依据
                return -1;
            }
            this.nlbTermialInfo.Text = "您选择的终端是：" + this.DrugTerminal.Name + "，目前已" + (this.DrugTerminal.IsClose ? "关闭" : "开放");

            //记录是否自动打印
            this.isAutoPrint = this.DrugTerminal.IsAutoPrint;

            //刷新间隔
            this.refreshInterval = (uint)this.DrugTerminal.RefreshInterval1;

            //自动打印生效要看本机IP地址是否和终端备注维护相同，这样可以避免多台电脑打开同一窗口
            if (this.isRegisterAutoPrint)
            {
                this.ncbAutoPrintRegister.Checked = Function.CheckAutoPrintPrive(this.DrugTerminal);
            }


            //刷新间隔
            if (this.refreshInterval == 0)
            {
                this.refreshInterval = 10;
            }

            #endregion

            #region 工作量
            string workLoadInfo = "";
            if (this.IsSetWorkLoad)
            {
                if (this.IOutpatientWorkLoad == null)
                {
                    this.ShowMessage("没有实现工作量接口：FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IOutpatientWorkLoad\n可以发药，但是无法计入本地化的工作量", MessageBoxIcon.Error);
                    workLoadInfo = "程序设置错误，无法计入本地化的工作量";
                }
                else
                {
                    workLoadInfo = this.IOutpatientWorkLoad.Init(this.StockDept, "1", this.DrugTerminal);
                }
            }
            else
            {
                workLoadInfo = "配药人：" + this.drugStoreMgr.Operator.Name + "工号：" + this.drugStoreMgr.ID; ;
                if (workLoadInfo == "-1")
                {
                    return -1;
                }
            }

            this.nlbWorkLoadInfo.Text = workLoadInfo;
            #endregion

            return 1;
        }

        #endregion

        #region 自动刷新，包括属性变量都在此，改动前三思

        /// <summary>
        /// 回溯时间-自动刷新间隔倍数
        /// 自动刷新的开始时间是上一次获取的处方信息中的最大收费时间
        /// 回溯的目的在于避免收费时间与插入数据库数据时的误差
        /// 15表示回溯15个刷新时间作为查询时间开始点，即刷新间隔10，则回溯150秒分钟
        /// </summary>
        private uint preTime = 15;

        /// <summary>
        /// 回溯时间-自动刷新间隔倍数
        /// </summary>
        [Description("回溯时间-自动刷新间隔倍数"), Category("设置"), Browsable(true)]
        public uint PreTime
        {
            get
            {
                return preTime;
            }
            set
            {
                preTime = value;
            }
        }

        /// <summary>
        /// 自动刷新启动延迟时间-秒
        /// </summary>
        private uint dueTime = 3;

        /// <summary>
        /// 自动刷新启动延迟时间-秒
        /// </summary>
        [Description("自动刷新启动延迟时间-秒"), Category("设置"), Browsable(true)]
        public uint DueTime
        {
            get { return dueTime; }
            set { dueTime = value; }
        }

        /// <summary>
        /// 刷新的开始时间
        /// 在界面打开和以后的手工刷新中用到
        /// </summary>
        private DateTime manualRefreshBeginTime = DateTime.Now;

        /// <summary>
        /// 自动刷新开始时间
        /// </summary>
        private DateTime autoRefreshBeginTime = DateTime.Now;

        /// <summary>
        /// 终端设置是否自动打印，不自动打印则程序自定更新处方调剂信息为打印状态
        /// </summary>
        private bool isAutoPrint = false;

        /// <summary>
        /// 终端设置的刷新间隔
        /// </summary>
        private uint refreshInterval = 10;

        private System.Threading.Timer autoRefreshTimer = null;
        private System.Threading.TimerCallback autoRefreshCallBack = null;
        private delegate void autoRefreshHandler();
        private autoRefreshHandler autoRefreshEven;

        private void BeginAutoRefresh()
        {
            if (this.autoRefreshCallBack == null)
            {
                this.autoRefreshCallBack = new System.Threading.TimerCallback(this.AutoRefreshTimerCallback);
            }
            this.autoRefreshTimer = new System.Threading.Timer(this.autoRefreshCallBack, null, dueTime * 1000, this.refreshInterval * 1000);
        }

        /// <summary>
        /// 刷新处方列表
        /// </summary>
        /// <param name="param">参数（没有使用）</param>
        /// <returns></returns>
        private void AutoRefreshTimerCallback(object param)
        {
            if (this.ncbPauseRefresh.Checked)
            {
                return;
            }

            if (this.autoRefreshEven == null)
            {
                autoRefreshEven = new autoRefreshHandler(this.AutoRefresh);
            }
            if (this.ParentForm != null)
            {
                this.ParentForm.Invoke(this.autoRefreshEven);
            }

        }

        #endregion

        #region 方法

        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            try
            {
                //从零点开始刷新
                this.manualRefreshBeginTime = drugStoreMgr.GetDateTimeFromSysDateTime().Date.AddDays(-this.DaySpan);
            }
            catch (Exception ex)
            {
                this.ShowMessage("初始化失败刷新开始时间失败：" + ex.Message, MessageBoxIcon.Error);
                this.manualRefreshBeginTime = DateTime.Now.Date;
            }

            //首次刷新时间，以后会根据每次刷新时获取的处方调剂信息中的收费时间重新计算
            this.autoRefreshBeginTime = this.manualRefreshBeginTime;

            //设置tabpage的标题
            this.ucDrugTree1.TabPage1Name = "未打印处方列表F11";
            this.ucDrugTree1.TabPage2Name = "已打印处方列表F12";
            this.ucDrugTree1.SetTreeState("1", "0");
        }

        /// <summary>
        /// 界面打开时加载列表
        /// 不加载未打印的，未打印的在以后的自动刷新或者手工刷新时加载
        /// </summary>
        /// <returns></returns>
        private int QueryOnLoad()
        {
            if (!this.IsShowOperList)
            {
                return 0;
            }

            if (this.EnumOutpatintDrugOperType != Function.EnumOutpatintDrugOperType.空闲)
            {
                this.ShowBalloonTip("正在" + this.EnumOutpatintDrugOperType.ToString() + ",请稍候再试...");
                return 0;
            }
            this.EnumOutpatintDrugOperType = Function.EnumOutpatintDrugOperType.查询;

            this.ucDrugTree1.Clear();
            this.IOutpatientShow.Clear();
            this.ucRecipeDetail1.Clear();

            //指定查询类别，0是发药窗口，1是配药台                
            string type = "1";
            //指定查询终端
            string terminalNO = this.DrugTerminal.ID;
           

            //1、未打印不加载，避免打开界面处理打印任务变得很慢，在以后的自动刷新或者手工刷新中处理

            //2、已经打印但是没有配药的
            ArrayList al1 = drugStoreMgr.QueryList(this.PriveDept.ID, terminalNO, type, "1", manualRefreshBeginTime);
            if (al1 != null)
            {
                this.ucDrugTree1.ShowRecipeList(al1, true, "0");
            }

            //3、已经配药，不加载，属于发药窗

            //4、已经发药的，不加载，属于发药窗

            this.EnumOutpatintDrugOperType = Function.EnumOutpatintDrugOperType.空闲;

            return 1;
        }

        /// <summary>
        /// 手工刷新处方列表
        /// </summary>
        /// <returns></returns>
        private int ManulRefresh()
        {
            if (!this.IsShowOperList)
            {
                return 0;
            }

            if (this.EnumOutpatintDrugOperType != Function.EnumOutpatintDrugOperType.空闲)
            {
                this.ShowBalloonTip("正在" + this.EnumOutpatintDrugOperType.ToString() + ",请稍候再试...");
                return 0;
            }

            //将刷新数据和打印封装在一个原子操作里面
            this.EnumOutpatintDrugOperType = Function.EnumOutpatintDrugOperType.手工刷新;

            this.ucDrugTree1.Clear();
            this.IOutpatientShow.Clear();
            this.ucRecipeDetail1.Clear();

            //指定查询类别，0发药窗口/1配药台/2还药/3直接发药            
            string type = "1";
            //指定查询终端
            string terminalNO =  this.DrugTerminal.ID;

            //4、已经发药的，不加载，属于发药窗

            //3、已经配药，不加载，属于发药窗

            //2、已经打印但是没有配药的
            ArrayList al1 = drugStoreMgr.QueryList(this.PriveDept.ID, terminalNO, type, "1", manualRefreshBeginTime);
            if (al1 != null)
            {
                this.ucDrugTree1.ShowRecipeList(al1, true, "0");
            }
            
            //1、未打印的
            ArrayList al0 = drugStoreMgr.QueryList(this.PriveDept.ID, terminalNO, type, "0", manualRefreshBeginTime);
            if (al0 != null)
            {
                this.SetAutoRefreshBeginTime(al0);
                this.ucDrugTree1.ShowRecipeList(al0, true, "0");

                //使用终端注册功能
                if (this.isRegisterAutoPrint)
                {
                    //没有注册显示数据后返回，不更新处方状态避免正常注册的终端打印不到
                    if (!this.ncbAutoPrintRegister.Checked)
                    {
                        this.EnumOutpatintDrugOperType = Function.EnumOutpatintDrugOperType.空闲;
                        return 0;
                    }
                }

                //是否自动打印维护在终端属性中
                //在终端选择时记录了是否自动打印
                //此处不判断打印是否成功，对打印失败的数据也更新状态才能正常发药保存，事后手工打印即可
                if (this.isAutoPrint)
                {
                    this.Print(al0);
                }

                foreach (FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe in al0)
                {
                    //更改打印状态，不处理事务，数据库自动提交
                    int param = this.drugStoreMgr.UpdateDrugRecipeState(this.PriveDept.ID, drugRecipe.RecipeNO, "M1", "0", "1");
                    if (param == -1)
                    {
                        this.EnumOutpatintDrugOperType = Function.EnumOutpatintDrugOperType.空闲;
                        this.ShowMessage("更新处方调剂信息为已打印状态发生错误：" + drugStoreMgr.Err);
                        return -1;
                    }
                    drugRecipe.RecipeState = "1";
                }
            }
            this.EnumOutpatintDrugOperType = Function.EnumOutpatintDrugOperType.空闲;

            return 1;
        }

        /// <summary>
        /// 自动刷新，改动前三思
        /// </summary>
        private void AutoRefresh()
        {
            if (this.EnumOutpatintDrugOperType != Function.EnumOutpatintDrugOperType.空闲)
            {
                this.ShowStatusBarTip("正在" + this.EnumOutpatintDrugOperType.ToString() + ",本次自动刷新已取消");
                return;
            }

            //将刷新数据和打印封装在一个原子操作里面
            this.EnumOutpatintDrugOperType = Function.EnumOutpatintDrugOperType.自动刷新;

            //指定查询类别，0发药窗口/1配药台/2还药/3直接发药             
            string type = "1";
            //指定查询终端
            string terminalNO = this.DrugTerminal.ID;

            //未打印的
            ArrayList al0 = drugStoreMgr.QueryList(this.PriveDept.ID, terminalNO, type, "0", autoRefreshBeginTime);
            if (al0 != null)
            {
                this.SetAutoRefreshBeginTime(al0);
                if (this.IsShowOperList)
                {
                    this.ucDrugTree1.ShowRecipeList(al0, true, "0");
                }

                //使用终端注册功能
                if (this.isRegisterAutoPrint)
                {
                    //没有注册显示数据后返回，不更新处方状态避免正常注册的终端打印不到
                    if (!this.ncbAutoPrintRegister.Checked)
                    {
                        this.EnumOutpatintDrugOperType = Function.EnumOutpatintDrugOperType.空闲;
                        return;
                    }
                }

                //是否自动打印维护在终端属性中
                //在终端选择时记录了是否自动打印
                //此处不判断打印是否成功，对打印失败的数据也更新状态才能正常发药保存，事后手工打印即可
                if (this.isAutoPrint)
                {
                    this.Print(al0);
                }

                foreach (FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe in al0)
                {
                    //更改打印状态，不处理事务，数据库自动提交
                    int param = this.drugStoreMgr.UpdateDrugRecipeState(this.PriveDept.ID, drugRecipe.RecipeNO, "M1", "0", "1");
                    if (param == -1)
                    {
                        this.EnumOutpatintDrugOperType = Function.EnumOutpatintDrugOperType.空闲;
                        this.ShowMessage("更新处方调剂信息为已打印状态发生错误：" + drugStoreMgr.Err);
                        return;
                    }
                    drugRecipe.RecipeState = "1";
                }
            }

            this.EnumOutpatintDrugOperType = Function.EnumOutpatintDrugOperType.空闲;

        }

        /// <summary>
        /// 显示选中的处方信息
        /// </summary>
        private void ShowInfo()
        {
            if (this.ucDrugTree1.DrugRecipe == null)
            {
                return;
            }
            //防止接口程序修改实体
            FS.HISFC.Models.Pharmacy.DrugRecipe dr = this.ucDrugTree1.DrugRecipe.Clone();
            //显示患者信息
            this.IOutpatientShow.ShowInfo(dr);
            //显示处方明细
            string state = "0";
            if (this.ucDrugTree1.DrugRecipe.RecipeState == "2")
            {
                state = "1";
            }
            else if (this.ucDrugTree1.DrugRecipe.RecipeState == "3")
            {
                state = "2";
            }
            ArrayList alRecipeDetail = this.drugStoreMgr.QueryApplyOutListForClinic(this.PriveDept.ID, "M1", state, this.ucDrugTree1.DrugRecipe.RecipeNO);
            if (alRecipeDetail == null)
            {
                this.ShowMessage("查询处方明细出错：" + drugStoreMgr.Err, MessageBoxIcon.Error);
                return;
            }
            if (alRecipeDetail.Count == 0)
            {
                FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe = this.drugStoreMgr.GetDrugRecipe(this.PriveDept.ID, this.ucDrugTree1.DrugRecipe.RecipeNO);
                if (alRecipeDetail == null)
                {
                    this.ShowMessage("查询处方" + this.ucDrugTree1.DrugRecipe.RecipeNO + "调剂信息出错：" + drugStoreMgr.Err, MessageBoxIcon.Error);
                    return;
                }
                if (drugRecipe.RecipeState != this.ucDrugTree1.DrugRecipe.RecipeState)
                {
                    if (drugRecipe.RecipeState == "2")
                    {
                        this.ShowBalloonTip("该处方已经在其它窗口配药" + (string.IsNullOrEmpty(drugRecipe.SendOper.ID) ? "" : "\n配药人：" + drugRecipe.DrugedOper.ID) + "\n建议您手工【刷新】一下系统");
                    }
                    else if (drugRecipe.RecipeState == "3")
                    {
                        this.ShowBalloonTip("该处方已经在其它窗口发药" + (string.IsNullOrEmpty(drugRecipe.SendOper.ID) ? "" : "\n发药人：" + drugRecipe.SendOper.ID) + "\n建议您手工【刷新】一下系统");
                    }
                }
            }
            this.ucRecipeDetail1.ShowInfo(alRecipeDetail);
        }

        /// <summary>
        /// 保存处方
        /// </summary>
        /// <returns></returns>
        private int Save()
        {
            if (this.ucDrugTree1.DrugRecipe == null)
            {
                return 0;
            }
            if (this.PriveDept == null || this.PriveDept.ID == "")
            {
                this.ShowMessage("请设置操作科室", MessageBoxIcon.Error);
                return -1;
            }

            if (this.EnumOutpatintDrugOperType != Function.EnumOutpatintDrugOperType.空闲)
            {
                this.ShowMessage("正在" + this.EnumOutpatintDrugOperType.ToString() + ",请稍后再试...");
                return 0;
            }

            this.EnumOutpatintDrugOperType = Function.EnumOutpatintDrugOperType.保存;

            FS.HISFC.Models.Pharmacy.DrugRecipe dr = this.ucDrugTree1.DrugRecipe.Clone();
            List<FS.HISFC.Models.Pharmacy.ApplyOut> alData = this.ucRecipeDetail1.GetSelectInfo();
            if (alData == null || alData.Count <= 0)
            {
                this.EnumOutpatintDrugOperType = Function.EnumOutpatintDrugOperType.空闲;
                return -1;
            }

            int parm = 1;
            //判断是否已进行打印
            if (dr.RecipeState == "0")
            {
                if ((this.IsRegisterAutoPrint && this.ncbAutoPrintRegister.Checked) || !this.IsRegisterAutoPrint)
                {
                    FS.FrameWork.Management.PublicTrans.BeginTransaction();
                    int param = this.drugStoreMgr.UpdateDrugRecipeState(this.PriveDept.ID, dr.RecipeNO, "M1", "0", "1");
                    if (param == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        //this.ShowMessage("更新处方调剂信息为已打印状态发生错误：" + drugStoreMgr.Err);
                        //return -1;
                    }
                    FS.FrameWork.Management.PublicTrans.Commit();
                    dr.RecipeState = "1";
                }
            }
            if (alData != null && alData.Count > 0)
            {
                FS.HISFC.Models.Pharmacy.ApplyOut applyOut = alData[0] as FS.HISFC.Models.Pharmacy.ApplyOut;
                if (applyOut.State == "1")
                {
                    this.EnumOutpatintDrugOperType = Function.EnumOutpatintDrugOperType.空闲;
                    this.ShowMessage("该药品已配药 不需保存");
                    return -1;
                }
                else if (applyOut.State == "2")//应该没有这样的数据
                {
                    this.EnumOutpatintDrugOperType = Function.EnumOutpatintDrugOperType.空闲;
                    this.ShowMessage("该药品已发药 不需保存");
                    return -1;
                }
            }

            //配药保存：更新调剂数
            parm = Function.OutpatientDrug(alData, DrugTerminal, this.StockDept, this.drugStoreMgr.Operator, true);

            if (parm == 1)
            {
                //本来想在此清除数据显示的，移动到函数尾
            }
            else
            {
                this.EnumOutpatintDrugOperType = Function.EnumOutpatintDrugOperType.空闲;
                return parm;
            }

            if (this.IsAfterSave)
            {
                if (this.IOutpatientPrint == null)
                {
                    this.EnumOutpatintDrugOperType = Function.EnumOutpatintDrugOperType.空闲;
                    this.ShowMessage("没有实现打印接口：FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IOutpatientDrug", MessageBoxIcon.Error);
                }
                else
                {
                    System.Collections.ArrayList al = new System.Collections.ArrayList(alData);
                    dr.RecipeState = "2";
                    dr.DrugedOper.ID = this.drugStoreMgr.Operator.ID;
                    dr.DrugedOper.Name = this.drugStoreMgr.Operator.Name;
                    this.IOutpatientPrint.AfterSave(al, dr, "1", this.DrugTerminal);
                }
            }

            if (this.IsSetWorkLoad)
            {
                if (this.IOutpatientWorkLoad == null)
                {
                    //MessageBox.Show("没有实现工作量接口：FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IOutpatientWorkLoad");
                }
                else
                {
                    dr.RecipeState = "2";
                    dr.DrugedOper.ID = this.drugStoreMgr.Operator.ID;
                    dr.DrugedOper.Name = this.drugStoreMgr.Operator.Name;
                    this.IOutpatientWorkLoad.Set(this.StockDept, dr, "1", this.DrugTerminal);
                }
            }
            this.EnumOutpatintDrugOperType = Function.EnumOutpatintDrugOperType.空闲;

            //清除显示的数据
            if (parm == 1)
            {
                this.ucRecipeDetail1.Clear();
                this.IOutpatientShow.Clear();
                parm = this.ucDrugTree1.ReMoveNode(0);
            }

            return parm;
        }

        private void AutoSave(int param)
        {
            if (!this.IsAutoSaveAfterQuery)
            {
                return;
            }
            //没有找到处方或在数据找到不可以配药状态的处方都不自动保存
            if (param == -1 || param == 2)
            {
                return;
            }
            this.EnumOutpatintDrugOperType = Function.EnumOutpatintDrugOperType.空闲;
            this.Save();

            this.ucDrugTree1.FocusBillNO();
        }

        /// <summary>
        /// 提供给打印按钮的方法，不更改打印状态
        /// </summary>
        /// <returns></returns>
        private int Print()
        {
            if (this.EnumOutpatintDrugOperType != Function.EnumOutpatintDrugOperType.空闲)
            {
                this.ShowMessage("正在" + this.EnumOutpatintDrugOperType.ToString() + ",请稍后再试...");
                return 0;
            }
            if (this.ucDrugTree1.DrugRecipe==null)
            {
                return 0; 
            }
            FS.HISFC.Models.Pharmacy.DrugRecipe dr = this.ucDrugTree1.DrugRecipe.Clone();
            List<FS.HISFC.Models.Pharmacy.ApplyOut> alData = this.ucRecipeDetail1.GetSelectInfo();
            ArrayList alPrintData = new ArrayList(alData);

            this.EnumOutpatintDrugOperType = Function.EnumOutpatintDrugOperType.打印;
            int param = this.Print(alPrintData, dr);

            //判断是否已进行打印
            if (dr.RecipeState == "0")
            {
                if ((this.IsRegisterAutoPrint && this.ncbAutoPrintRegister.Checked) || !this.IsRegisterAutoPrint)
                {
                    FS.FrameWork.Management.PublicTrans.BeginTransaction();
                    param = this.drugStoreMgr.UpdateDrugRecipeState(this.PriveDept.ID, dr.RecipeNO, "M1", "0", "1");
                    if (param == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        //this.ShowMessage("更新处方调剂信息为已打印状态发生错误：" + drugStoreMgr.Err);
                        //return -1;
                    }
                    FS.FrameWork.Management.PublicTrans.Commit();
                    dr.RecipeState = "1";
                }
            }
            
            this.EnumOutpatintDrugOperType = Function.EnumOutpatintDrugOperType.空闲;
            return param;
        }

        /// <summary>
        /// 打印
        /// 上层调用函数必须保证这个打印过程不可以被刷新、查询等中断或多线程同步执行
        /// </summary>
        /// <param name="alDrugRecipe">必须是状态为0的处方调剂信息数组</param>
        /// <returns>-1失败</returns>
        private int Print(ArrayList alDrugRecipe)
        {
            foreach (FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe in alDrugRecipe)
            {
                //显示处方明细
                string state = "0";
                if (drugRecipe.RecipeState == "2")
                {
                    state = "1";
                }
                else if (drugRecipe.RecipeState == "3")
                {
                    state = "2";
                }
                ArrayList alRecipeDetail = this.drugStoreMgr.QueryApplyOutListForClinic(this.PriveDept.ID, "M1", state, drugRecipe.RecipeNO);
                if (alRecipeDetail == null)
                {
                    this.ShowMessage("查询处方明细出错：" + drugStoreMgr.Err, MessageBoxIcon.Error);
                    return -1;
                }
                int param = this.Print(alRecipeDetail, drugRecipe);
            }
            return 1;
        }

        /// <summary>
        /// 打印
        /// 上层调用函数必须保证这个打印过程不可以被刷新、查询等中断或多线程同步执行
        /// </summary>
        /// <param name="alPrintData">同一处方的出库申请明细数据</param>
        /// <param name="drugRecipe">处方调剂信息</param>
        /// <returns>-1失败</returns>
        private int Print(ArrayList alPrintData, FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe)
        {
            if (this.IOutpatientPrint == null)
            {
                this.ShowMessage("没有实现打印接口：FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IOutpatientDrug", MessageBoxIcon.Error);
                return -1;
            }
            else
            {
                return this.IOutpatientPrint.OnAutoPrint(alPrintData, drugRecipe, "1", this.DrugTerminal);
            }
        }

        /// <summary>
        /// 根据处方调剂信息设置自动刷新的开始时间
        /// 获取本次信息中收费时间最大的作为开始时间然后回溯
        /// </summary>
        /// <param name="alDrugRecipe"></param>
        private void SetAutoRefreshBeginTime(ArrayList alDrugRecipe)
        {
            //获取本次信息中收费时间最大的作为开始时间
            foreach (FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe in alDrugRecipe)
            {
                if (this.autoRefreshBeginTime < drugRecipe.FeeOper.OperTime)
                {
                    this.autoRefreshBeginTime = drugRecipe.FeeOper.OperTime;
                }
            }

            //开始时间回溯：
            /*收费时间是门诊收费时赋值的时间，处方调剂信息数据插入数据库事务提交的顺序和收费时间大小可能是不一致的
             *比如：一号收费窗口9:00:01收费，二号9:00:02收费，但是由于一号窗口需要处理很多业务——连接医保等等，最后9:00:03二号先提交事务
             *自动刷新9:00:04开始，获取了二号窗口的信息，将最大收费时间赋值为9:00:02，然后一号窗口9:00:05提交事务，这样自动刷新下次刷新从
             *9:00:02开始，就再也查询不到一号窗口9:00:01的处方了
             *回溯的目的在于解决这个问题：难点在回溯的时间过大，数据查询处理重复，过小解决不了这个问题
             */
            if (this.autoRefreshBeginTime.AddSeconds(-this.preTime * this.refreshInterval) > this.autoRefreshBeginTime.Date.AddDays(-this.DaySpan))
            {
                this.autoRefreshBeginTime.AddSeconds(-this.preTime * this.refreshInterval);
            }
        }

        #endregion

        #region 事件、工具栏
        protected override int OnSave(object sender, object neuObject)
        {
            this.ucDrugTree1.FocusBillNO();
            return this.Save();
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            return this.ManulRefresh();
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            return this.Print();
        }

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.ToolBarService.AddToolButton("刷新", "刷新处方列表", FS.FrameWork.WinForms.Classes.EnumImageList.S刷新, true, false, null);
            base.OnInit(sender, neuObject, param);
            return this.ToolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "刷新")
            {
                this.ManulRefresh();
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        protected override void OnLoad(EventArgs e)
        {
            if (this.DesignMode)
            {
                return;
            }

            this.Init();

            //查询数据后开始刷新
            this.QueryOnLoad();

            this.ucDrugTree1.RecipeAfterSelectEven += new ucRecipeTree.RecipeAfterSelectHandler(this.ShowInfo);
            this.ucDrugTree1.RecipeQueryAfterEven += new ucRecipeTree.RecipeQueryAfterHandler(this.AutoSave);
            this.nlbWorkLoadInfo.DoubleClick += new EventHandler(nlbWorkLoadInfo_DoubleClick);
            this.nlbTermialInfo.DoubleClick += new EventHandler(nlbTermialInfo_DoubleClick);
            this.ncbAutoPrintRegister.CheckedChanged += new EventHandler(ncbAutoPrintRegister_CheckedChanged);
            base.OnLoad(e);

            //启动自动刷新
            this.BeginAutoRefresh();
        }

        void ncbAutoPrintRegister_CheckedChanged(object sender, EventArgs e)
        {
            this.ncbAutoPrintRegister.CheckedChanged -= new EventHandler(ncbAutoPrintRegister_CheckedChanged);
            this.ncbAutoPrintRegister.Checked = !this.ncbAutoPrintRegister.Checked;
            if (!this.isRegisterAutoPrint)
            {
                this.ShowBalloonTip("程序没有设置自动打印注册功能！\n请保证同一终端在同一台电脑上操作！");
                this.ncbAutoPrintRegister.CheckedChanged += new EventHandler(ncbAutoPrintRegister_CheckedChanged);
                return;
            }
            if (!this.ncbAutoPrintRegister.Checked)
            {
                //注册打印
                bool successful = this.isAutoPrint;

                successful = Function.RegisterDrugTermial(this.DrugTerminal);

                if (successful)
                {
                    this.ManulRefresh();
                }
                this.ncbAutoPrintRegister.Checked = successful;
            }
            else
            {
                this.ShowMessage("取消自动打印注册请在门诊终端维护中操作，如果您没有权限请与药房负责人联系！");
                this.ncbAutoPrintRegister.Checked = true;

            }
            this.ncbAutoPrintRegister.CheckedChanged += new EventHandler(ncbAutoPrintRegister_CheckedChanged);
        }

        void nlbTermialInfo_DoubleClick(object sender, EventArgs e)
        {
            if (this.DrugTerminal != null && !string.IsNullOrEmpty(this.DrugTerminal.ID))
            {
                if (!this.DrugTerminal.IsClose)
                {
                    if (MessageBox.Show(this, "确认关闭终端吗？", "提示>>", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        return;
                    }
                }
                if (Function.TerminalSwith(this.PriveDept.ID, this.DrugTerminal) == 1)
                {
                    this.DrugTerminal.IsClose = !this.DrugTerminal.IsClose;
                    this.nlbTermialInfo.Text = "您选择的终端是：" + this.DrugTerminal.Name + "，目前已" + (this.DrugTerminal.IsClose ? "关闭" : "开放");
                }
            }
        }

        void nlbWorkLoadInfo_DoubleClick(object sender, EventArgs e)
        {
            if (this.IsSetWorkLoad && this.IOutpatientWorkLoad != null)
            {
                this.nlbWorkLoadInfo.Text = this.IOutpatientWorkLoad.Reassigned(this.PriveDept, "1", this.DrugTerminal);
            }
        }

        #endregion
    }
}
