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
    /// [功能描述: 静态发药]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2010-9]<br></br>
    /// 说明：
    /// 1、不添加自动刷新、自动打印等自动功能的发药
    /// 2、暂时用于发非当天收费的门诊处方
    /// </summary>
    public partial class ucStaticSend : ucDrugBase, FS.FrameWork.WinForms.Classes.IPreArrange
    {
        public ucStaticSend()
        {
            InitializeComponent();
        }


        private string beginLimitedTime = "00:00:00";

        /// <summary>
        /// 限制时间点
        /// </summary>
        [Description("限制时间点"), Category("设置"), Browsable(true)]
        public string BeginLimitedTime
        {
            get { return beginLimitedTime; }
            set { beginLimitedTime = value; }
        }

        private string endLimitedTime = "00:00:00";

        /// <summary>
        /// 限制时间点
        /// </summary>
        [Description("限制时间点"), Category("设置"), Browsable(true)]
        public string EndLimitedTime
        {
            get { return endLimitedTime; }
            set { endLimitedTime = value; }
        }

        private uint daySpan = 30;

        /// <summary>
        /// 时间间隔天数
        /// </summary>
        [Description("时间间隔天数"), Category("设置"), Browsable(true)]
        public uint DaySpan
        {
            get { return daySpan; }
            set { daySpan = value; }
        }

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

        /// <summary>
        /// 是否需要当天数据
        /// </summary>
        private bool isNeedToDayData = false;

        /// <summary>
        /// 是否需要当天数据（结束时间向后加一天）
        /// </summary>
        [Description("是否需要当天数据（结束时间向后加一天）"), Category("设置"), Browsable(true)]
        public bool IsNeedToDayData
        {
            get { return isNeedToDayData; }
            set { isNeedToDayData = value; }
        }

        /// <summary>
        /// 是否超过七天
        /// </summary>
        private bool IsExpire = false;


        FS.SOC.HISFC.BizLogic.Pharmacy.DrugStore drugStoreMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.DrugStore();

        FS.HISFC.BizLogic.Manager.Department deptMgr = new FS.HISFC.BizLogic.Manager.Department();

        /// <summary>
        /// 常数控制类
        /// </summary>
        private FS.HISFC.BizLogic.Manager.Constant conMgr = new FS.HISFC.BizLogic.Manager.Constant();
        //FS.SOC.HISFC.BizLogic.Pharmacy.Item drugStoreMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.Item();

        #region IPreArrange 成员

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
                    this.ngbAdd.Text = "附加信息： 特别提醒您正在配发【" + this.priveDept.Name + "】的处方，保存时会扣减【" + this.StockDept.Name + "】的库存";
                }

                if (this.PriveDept == null)
                {
                    this.ShowMessage("您没有权限！请重新登录！");
                    return -1;
                }
            }

            #endregion

            this.SendTerminal = Function.TerminalSelect(this.PriveDept.ID, FS.HISFC.Models.Pharmacy.EnumTerminalType.发药窗口, true);

            if (this.SendTerminal == null)
            {
                return -1;
            }

            this.nlbTermialInfo.Text = "您选择的终端是：" + this.SendTerminal.Name;

            if (this.IsNeedChooseDrugTerminal)
            {
                this.DrugTerminal = Function.TerminalSelect(this.PriveDept.ID, SendTerminal.ID, true);

                if (this.DrugTerminal == null)
                {
                    return -1;
                }
                this.nlbTermialInfo.Text = "您选择的终端是：" + this.SendTerminal.Name + "； " + this.DrugTerminal.Name + "，目前已" + (this.DrugTerminal.IsClose ? "关闭" : "开放");

            }

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
                    workLoadInfo = this.IOutpatientWorkLoad.Init(this.StockDept, "0", this.SendTerminal);
                    if (workLoadInfo == "-1")
                    {
                        return -1;
                    }
                }
            }
            else
            {
                workLoadInfo = "发药人：" + this.drugStoreMgr.Operator.Name + "工号：" + this.drugStoreMgr.Operator.ID; ;
            }

            this.nlbWorkLoadInfo.Text = workLoadInfo;
            return 1;
        }

        #endregion

        private void Init()
        {
            //设置开始和结束时间
            this.ndtpEnd.Value = this.ndtpEnd.Value.Date.AddSeconds(-1);
            this.ndtpBegin.Value = this.ndtpEnd.Value.Date.AddDays(-this.DaySpan);

            if (!string.IsNullOrEmpty(BeginLimitedTime))
            {
                DateTime time = this.ndtpBegin.Value;
                if (!DateTime.TryParse(time.ToShortDateString() + " " + this.BeginLimitedTime, out time))
                {
                    time = this.ndtpBegin.Value;
                }
                this.ndtpBegin.Value = time;
                this.ndtpBegin.Enabled = false;
            }

            if (!string.IsNullOrEmpty(EndLimitedTime))
            {
                DateTime time = this.ndtpEnd.Value;
                if (!DateTime.TryParse(time.ToShortDateString() + " " + this.EndLimitedTime, out time))
                {
                    time = this.ndtpEnd.Value;
                }
                this.ndtpEnd.Value = time;

            }
            if (this.IsNeedToDayData)
            {
                this.ndtpEnd.Value = this.ndtpEnd.Value.AddDays(1);
            }

            //管理员可以修改时间
            if (((FS.HISFC.Models.Base.Employee)this.drugStoreMgr.Operator).IsManager)
            {
                this.ndtpBegin.Enabled = true;
                this.ndtpEnd.Enabled = true;
            }
            else
            {
                this.ndtpBegin.Enabled = true;
                this.ndtpEnd.Enabled = false;
            }

            //设置tabpage的标题
            this.ucDrugTree1.TabPage1Name = "未发药处方列表F11";
            this.ucDrugTree1.TabPage2Name = "已发药处方列表F12";
            this.ucDrugTree1.SetTreeState("1", "3");
        }

        /// <summary>
        /// 查询处方列表
        /// </summary>
        /// <returns></returns>
        private int Query()
        {
            try
            {
                //仅仅清除未发药处方列表
                this.ucDrugTree1.Clear(0);
                this.IOutpatientShow.Clear();
                this.ucRecipeDetail1.Clear();

                //指定查询类别，0发药窗口/1配药台/2还药/3直接发药             
                string type = "3";
                //指定查询终端
                string terminalNO = this.SendTerminal.ID;
                if (this.DrugTerminal != null && !string.IsNullOrEmpty(this.DrugTerminal.ID))
                {
                    type = "1";
                    terminalNO = this.DrugTerminal.ID;
                }
                //未打印的，几乎没有数据
                ArrayList al0 = drugStoreMgr.QueryList(this.PriveDept.ID, terminalNO, type, "0", this.ndtpBegin.Value);
                this.ucDrugTree1.ShowRecipeList(this.FilterOverTimeData(al0), true, "0");
                //已经打印但是没有配药的
                ArrayList al1 = drugStoreMgr.QueryList(this.PriveDept.ID, terminalNO, type, "1", this.ndtpBegin.Value);
                this.ucDrugTree1.ShowRecipeList(this.FilterOverTimeData(al1), true, "0");
                //已经配药但是没有发药的
                ArrayList al2 = drugStoreMgr.QueryList(this.PriveDept.ID, terminalNO, type, "2", this.ndtpBegin.Value);
                this.ucDrugTree1.ShowRecipeList(this.FilterOverTimeData(al2), true, "0");
                //已经发药的
                //{6D58131A-E702-42e7-852B-9BE7872F2F7D}
                ArrayList al3 = drugStoreMgr.QueryList(this.PriveDept.ID, terminalNO, type, "3", this.ndtpBegin.Value);
                if (al3 != null)
                {
                    this.ucDrugTree1.ShowRecipeList(al3, false, "1");
                }
            }
            catch (Exception ex)
            {
                this.ShowMessage("查询发生错误：" + ex.Message, MessageBoxIcon.Error);
                return -1;
            }
            return 1;
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


            IsExpire = false;
            //防止接口程序修改实体
            FS.HISFC.Models.Pharmacy.DrugRecipe dr = this.ucDrugTree1.DrugRecipe.Clone();
            this.IOutpatientShow.ShowInfo(dr);
            //显示处方明细
            //{7F07EFC1-0C36-4cf0-860E-DECB8529CEDD}
            string state = this.ucDrugTree1.DrugRecipe.RecipeState;
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

            string opertime = GetDrugOperByRecipeNO(this.ucDrugTree1.DrugRecipe.RecipeNO);

            DateTime opertimes = Convert.ToDateTime(opertime);
            DateTime now = DateTime.Now;

            if (opertimes.Date < now.Date && now.AddDays(-7) > opertimes)
            {
                if (alRecipeDetail.Count == 1)
                {
                    FS.HISFC.Models.Pharmacy.ApplyOut app = alRecipeDetail[0] as FS.HISFC.Models.Pharmacy.ApplyOut;

                    FrameWork.Models.NeuObject cst = conMgr.GetConstant("LIMITMEDICINE", app == null ? "9999999" : app.Item.ID);

                    if (cst == null || string.IsNullOrEmpty(cst.ID))
                    {

                        IsExpire = true;
                        this.ShowMessage("处方" + this.ucDrugTree1.DrugRecipe.RecipeNO + "的开立时间" + opertimes + "已超过7天，不可发药！！！");
                        this.ucRecipeDetail1.ShowInfo(alRecipeDetail);

                        //this.IOutpatientShow.ShowInfo(null);
                        return;
                    }

                }
                else
                {
                    IsExpire = true;
                    this.ShowMessage("处方" + this.ucDrugTree1.DrugRecipe.RecipeNO + "的开立时间" + opertimes + "已超过7天，不可发药！！！");
                    this.ucRecipeDetail1.ShowInfo(alRecipeDetail);
                    //this.IOutpatientShow.ShowInfo(null);
                    return;
                }
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
                    this.ShowBalloonTip("该处方已经在其它窗口发药\n" + (string.IsNullOrEmpty(drugRecipe.SendOper.ID) ? "" : "发药人：" + drugRecipe.SendOper.ID) + "\n建议您手工【刷新】一下系统");
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
            if (IsExpire)
            {
                 this.ShowMessage("改处方的开立时间已超过7天，不可发药！！！");
                 return 0;
            }

            //[2011-8-31]zhao.zf unDruged表示该处方状态是否还没配药，还没配药的话调用直接发药流程，否则调用正常发药流程
            bool unDruged = true;

            if (this.ucDrugTree1.DrugRecipe == null)
            {
                return 0;
            }
            if (this.PriveDept == null || this.PriveDept.ID == "")
            {
                this.ShowMessage("请设置操作科室", MessageBoxIcon.Error);
                return -1;
            }
            FS.HISFC.Models.Pharmacy.DrugRecipe dr = this.ucDrugTree1.DrugRecipe.Clone();
            List<FS.HISFC.Models.Pharmacy.ApplyOut> alData = this.ucRecipeDetail1.GetSelectInfo();
            if (alData == null || alData.Count <= 0)
            {
                return -1;
            }

            int param = 1;
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
                    //{637F23E0-4018-4302-8483-8A567535E941}
                    //unDruged = false;
                }
            }
            if (dr.RecipeState == "0")
            {
                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                param = this.drugStoreMgr.UpdateDrugRecipeState(this.PriveDept.ID, dr.RecipeNO, "M1", "0", "1");
                if (param == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.ShowMessage("更新处方调剂信息为已打印状态发生错误：" + drugStoreMgr.Err);
                    return -1;
                }
                FS.FrameWork.Management.PublicTrans.Commit();
                dr.RecipeState = "1";
            }

            string recipeState = "3";
            //判断部分发药，并且未全部选择界面药品
            if (Function.JudgePartSend() == 1 && !this.ucRecipeDetail1.CheckAll)
            {
                recipeState = "1";
                param = Function.OutpatientPartSend(alData, this.SendTerminal, this.StockDept.Clone(), this.drugStoreMgr.Operator, unDruged, false);
            }
            else if (Function.JudgePartSend() == 1 && this.ucRecipeDetail1.CheckAll)
            {
                param = Function.OutpatientPartSend(alData, this.SendTerminal, this.StockDept.Clone(), this.drugStoreMgr.Operator, unDruged, true);
            }
            else
            {
                //静态发药不更新调剂数
                //[2011-8-31]zhao.zf unDruged表示该处方状态是否还没配药，还没配药的话调用直接发药流程，否则调用正常发药流程
                param = Function.OutpatientSend(alData, this.SendTerminal, this.StockDept, this.drugStoreMgr.Operator, unDruged, false);
            }
            if (param == 1)
            {
                //清除数据，移动到函数结尾
            }
            else
            {
                return param;
            }

            if (this.IsAfterSave)
            {
                if (this.IOutpatientPrint == null)
                {
                    this.ShowMessage("没有实现打印接口：FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IOutpatientDrug", MessageBoxIcon.Error);
                }
                else
                {
                    System.Collections.ArrayList al = new System.Collections.ArrayList(alData);
                    dr.RecipeState = recipeState;
                    dr.DrugedOper.ID = this.drugStoreMgr.Operator.ID;
                    dr.DrugedOper.Name = this.drugStoreMgr.Operator.Name;
                    dr.SendOper.ID = this.drugStoreMgr.Operator.ID;
                    dr.SendOper.Name = this.drugStoreMgr.Operator.Name;
                    this.IOutpatientPrint.AfterSave(al, dr, "0", this.SendTerminal);
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
                    dr.RecipeState = recipeState;
                    dr.DrugedOper.ID = this.drugStoreMgr.Operator.ID;
                    dr.DrugedOper.Name = this.drugStoreMgr.Operator.Name;
                    dr.SendOper.ID = this.drugStoreMgr.Operator.ID;
                    dr.SendOper.Name = this.drugStoreMgr.Operator.Name;
                    this.IOutpatientWorkLoad.Set(this.StockDept, dr, "0", this.SendTerminal);
                }
            }
            if (param == 1)
            {
                this.ucRecipeDetail1.Clear();
                this.IOutpatientShow.Clear();
                param = this.ucDrugTree1.MoveNode();
            }
            return param;
        }

        /// <summary>
        /// 过滤掉超过结束时间的处方调剂信息
        /// </summary>
        /// <param name="alRecipe">需要过滤处方调剂信息实体数组</param>
        /// <returns>过滤后的处方调剂信息实体数组</returns>
        private ArrayList FilterOverTimeData(ArrayList alRecipe)
        {
            if (alRecipe == null)
            {
                return new ArrayList();
            }
            ArrayList al = new ArrayList();

            foreach (FS.HISFC.Models.Pharmacy.DrugRecipe dr in alRecipe)
            {
                if (dr.FeeOper.OperTime <= this.ndtpEnd.Value)
                {
                    al.Add(dr);
                }
            }
            return al;
        }

        protected override int OnSave(object sender, object neuObject)
        {
            return this.Save();
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            return this.Query();
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            base.ToolStrip_ItemClicked(sender, e);
        }

        protected override void OnLoad(EventArgs e)
        {
            this.Init();
            this.ucDrugTree1.RecipeAfterSelectEven += new ucRecipeTree.RecipeAfterSelectHandler(this.ShowInfo);
            this.nlbWorkLoadInfo.DoubleClick += new EventHandler(nlbWorkLoadInfo_DoubleClick);
            //静态发药不允许关闭终端
            //this.nlbTermialInfo.DoubleClick += new EventHandler(nlbTermialInfo_DoubleClick);

            base.OnLoad(e);
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
                    this.nlbTermialInfo.Text = "您选择的终端是：" + this.SendTerminal.Name + "； "
                        + this.DrugTerminal.Name + "，目前已" + (this.DrugTerminal.IsClose ? "关闭" : "开放");
                }
            }
            else if (this.SendTerminal != null && !string.IsNullOrEmpty(this.SendTerminal.ID))
            {
                FS.HISFC.Models.Pharmacy.DrugTerminal t = Function.TerminalSelect(this.PriveDept.ID, this.SendTerminal.ID, false);
                if (t == null)
                {
                    return;
                }
                if (!t.IsClose)
                {
                    if (MessageBox.Show(this, "确认关闭终端吗？", "提示>>", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        return;
                    }
                }
                if (Function.TerminalSwith(this.PriveDept.ID, t) == 1)
                {
                    this.nlbTermialInfo.Text = "您选择的终端是：" + this.SendTerminal.Name + "； "
                        + t.Name + "，目前已" + (t.IsClose ? "开放" : "关闭");
                }
            }
        }

        void nlbWorkLoadInfo_DoubleClick(object sender, EventArgs e)
        {
            if (this.IsSetWorkLoad && this.IOutpatientWorkLoad != null)
            {
                this.IOutpatientWorkLoad.Reassigned(this.PriveDept, "0", this.SendTerminal);
            }
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            return this.Print();
        }
        private int Print()
        {
            if (this.EnumOutpatintDrugOperType != Function.EnumOutpatintDrugOperType.空闲)
            {
                this.ShowMessage("正在" + this.EnumOutpatintDrugOperType.ToString() + ",请稍后再试...");
                return 0;
            }

            FS.HISFC.Models.Pharmacy.DrugRecipe dr = this.ucDrugTree1.DrugRecipe.Clone();
            List<FS.HISFC.Models.Pharmacy.ApplyOut> alData = this.ucRecipeDetail1.GetSelectInfo();
            ArrayList alPrintData = new ArrayList(alData);

            this.EnumOutpatintDrugOperType = Function.EnumOutpatintDrugOperType.打印;
            int param = this.Print(alPrintData, dr);
            this.EnumOutpatintDrugOperType = Function.EnumOutpatintDrugOperType.空闲;
            return param;
        }

        /// <summary>
        /// 根据处方号获取处方开立时间
        /// </summary>
        /// <returns></returns>
        private string GetDrugOperByRecipeNO(string recipeNO)
        {
            string strSql = string.Empty;
            if (this.deptMgr.Sql.GetSql("Pharmacy.DrugStore.GetDrugedOperTime", ref strSql) == -1)
            {
                return string.Empty;
            }
            strSql = string.Format(strSql, recipeNO);
            return this.deptMgr.ExecSqlReturnOne(strSql, "");
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
                return this.IOutpatientPrint.OnAutoPrint(alPrintData, drugRecipe, "0", this.SendTerminal);
            }
        }
    }
}
