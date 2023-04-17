using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Management;
using FS.FrameWork.Function;

namespace FS.SOC.HISFC.Components.DrugStore.Compound
{
    /// <summary>
    /// 静配中心审批
    /// </summary>
    public partial class ucCompoundCheck : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucCompoundCheck()
        {
            InitializeComponent();
        }

        #region 域变量

        /// <summary>
        /// 核准人员
        /// </summary>
        private FS.FrameWork.Models.NeuObject approveOper = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// 核准科室
        /// </summary>
        private FS.FrameWork.Models.NeuObject approveDept = new FS.FrameWork.Models.NeuObject();
        /// <summary>
        /// 药品业务管理层
        /// </summary>
        private FS.SOC.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.SOC.HISFC.BizLogic.Pharmacy.Item();
        /// <summary>
        /// 配置中心管理类
        /// </summary>
        private FS.SOC.HISFC.BizLogic.Pharmacy.Compound compoundMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.Compound();

        /// <summary>
        /// 药品管理整合业务类
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Pharmacy pharmacyIntegrate = new FS.HISFC.BizProcess.Integrate.Pharmacy();
        /// <summary>
        /// 医嘱管理整合业务类
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Order orderIntegrate = new FS.HISFC.BizProcess.Integrate.Order();

        /// <summary>
        /// 常数管理类
        /// </summary>
        private FS.HISFC.BizLogic.Manager.Constant consMgr = new FS.HISFC.BizLogic.Manager.Constant();

        /// <summary>
        /// 组件患者选择树
        /// </summary>
        private tvCompoundList tvCompound = null;

        /// <summary>
        /// 医嘱类型集合
        /// </summary>
        private System.Collections.Hashtable hsOrderType = new Hashtable();

        /// <summary>
        /// 管理层
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();
       
        /// <summary>
        /// 用于存储单药品检索时返回所有病区数据
        /// </summary>
        ArrayList alThisParList = new ArrayList();

        /// <summary>
        /// 用于存储所有化疗用法
        /// </summary>
        Hashtable hsRadiotherapyUsage = new Hashtable();

        /// <summary>
        /// 用于存储所有化疗临瞩
        /// </summary>
        Hashtable hsAllRadiotherapyLZ = new Hashtable();


        #endregion

        #region 属性

        /// <summary>
        /// 核准科室
        /// </summary>
        public FS.FrameWork.Models.NeuObject ApproveDept
        {
            get
            {
                return this.approveDept;
            }
            set
            {
                this.approveDept = value;
            }
        }

        #endregion

        #region 工具栏信息

        /// <summary>
        /// 定义工具栏服务
        /// </summary>
        protected FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        /// <summary>
        /// 初始化工具栏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="NeuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object NeuObject, object param)
        {
            //增加工具栏
            this.toolBarService.AddToolButton("全选", "选择全部申请", FS.FrameWork.WinForms.Classes.EnumImageList.Q全选, true, false, null);

            this.toolBarService.AddToolButton("全不选", "取消全部申请选择", FS.FrameWork.WinForms.Classes.EnumImageList.Q取消, true, false, null);

            this.toolBarService.AddToolButton("刷新", "刷新患者列表显示", FS.FrameWork.WinForms.Classes.EnumImageList.S刷新, true, false, null);

            this.toolBarService.AddToolButton("查看合理用药信息", "调用合理用药，查看合理用药信息", FS.FrameWork.WinForms.Classes.EnumImageList.B病历, true, false, null);

            this.toolBarService.AddToolButton("普通临瞩", "过滤临时医嘱", FS.FrameWork.WinForms.Classes.EnumImageList.Y医嘱, true, false, null);

            return this.toolBarService;
        }

        /// <summary>
        /// 工具栏按钮单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "全选":
                    this.Check(true);
                    break;
                case "全不选":
                    this.Check(false);
                    break;
                case "刷新":
                    this.ShowList();
                    break;
                case "查看合理用药信息":
                    this.DrugUse();
                    break;
                case "普通临瞩":
                    this.Filter("short");
                    break;
                case "化疗临瞩":
                    this.Filter("shortSpecial");
                    break;
                case "长期":
                    this.Filter("long");
                    break;
                case "全部":
                    this.Filter("all");
                    break;
            }

        }

        /// <summary>
        /// 过滤
        /// </summary>
        /// <returns></returns>
        private int Filter(string tag)
        {
            if (tag == "short")
            {
                    for (int i = 0; i < this.fpApply_Sheet1.Rows.Count; i++)
                    {
                        if ((this.fpApply_Sheet1.Rows[i].Tag as FS.HISFC.Models.Pharmacy.ApplyOut).OrderType.ID.ToString() == "LZ")
                        {
                            if (this.hsAllRadiotherapyLZ.Contains((this.fpApply_Sheet1.Rows[i].Tag as FS.HISFC.Models.Pharmacy.ApplyOut).CombNO))
                            {
                                this.fpApply_Sheet1.Rows[i].Visible = false;
                            }
                            else
                            {
                                this.fpApply_Sheet1.Rows[i].Visible = true;
                            }
                        }
                        else
                        {
                            this.fpApply_Sheet1.Rows[i].Visible = false;
                        }
                    }

                this.toolBarService.GetToolButton("普通临瞩").Text = "化疗临瞩";
            }
            else if (tag == "shortSpecial")
            {
                    for (int i = 0; i < this.fpApply_Sheet1.Rows.Count; i++)
                    {
                        if ((this.fpApply_Sheet1.Rows[i].Tag as FS.HISFC.Models.Pharmacy.ApplyOut).OrderType.ID.ToString() == "LZ")
                        {
                            if (this.hsAllRadiotherapyLZ.Contains((this.fpApply_Sheet1.Rows[i].Tag as FS.HISFC.Models.Pharmacy.ApplyOut).CombNO))
                            {
                                this.fpApply_Sheet1.Rows[i].Visible = true;
                            }
                            else
                            {
                                this.fpApply_Sheet1.Rows[i].Visible = false;
                            }
                        }
                        else
                        {
                            this.fpApply_Sheet1.Rows[i].Visible = false;
                        }
                    }
                this.toolBarService.GetToolButton("化疗临瞩").Text = "长期";
            }
            else if (tag == "long")
            {
                for (int i = 0; i < this.fpApply_Sheet1.Rows.Count; i++)
                {
                    if ((this.fpApply_Sheet1.Rows[i].Tag as FS.HISFC.Models.Pharmacy.ApplyOut).OrderType.ID.ToString() == "CZ")
                    {
                        this.fpApply_Sheet1.Rows[i].Visible = true;
                    }
                    else
                    {
                        this.fpApply_Sheet1.Rows[i].Visible = false;
                    }
                }
                
                this.toolBarService.GetToolButton("长期").Text = "全部";
            }
            else
            {
                for (int i = 0; i < this.fpApply_Sheet1.Rows.Count; i++)
                {
                    this.fpApply_Sheet1.Rows[i].Visible = true;
                }

                this.toolBarService.GetToolButton("全部").Text = "普通临瞩";
            }
            return 1;
        }

        /// <summary>
        /// 选中/不选中
        /// </summary>
        /// <param name="isCheck"></param>
        /// <returns></returns>
        public int Check(bool isCheck)
        {
            for (int i = 0; i < this.fpApply_Sheet1.Rows.Count; i++)
            {
                if (this.fpApply_Sheet1.Rows[i].Visible)  //只选择可见的行
                {
                    this.fpApply_Sheet1.Cells[i, (int)ColumnSet.ColSelect].Value = isCheck;
                }
            }

            return 1;
        }

        /// <summary>
        /// 查看合理用药结果
        /// </summary>
        /// <returns></returns>
        private void DrugUse()
        {
            ArrayList alApply = this.GetCheckData();
            if (alApply.Count == 0)
            {
                return ;
            }

            FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IRationalDrugUseForCompound obj = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IRationalDrugUseForCompound)) as FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IRationalDrugUseForCompound;
            if (obj == null)
            {
                MessageBox.Show("无合理用药");
                return;
            }
            else
            {
                obj.CheckDrugUse(alApply);
            }

        }

        #endregion

        #region 方法

        /// <summary>
        /// 数据初始化
        /// </summary>
        /// <returns></returns>
        protected virtual int Init()
        {
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(Language.Msg("正在加载基础数据.请稍候..."));
            Application.DoEvents();

            this.tvCompound = this.tv as tvCompoundList;

            this.tvCompound.Init();

            this.tvCompound.State = "C";

            FS.FrameWork.Management.DataBaseManger dataManager = new DataBaseManger();

            this.approveOper = dataManager.Operator;

            this.approveDept = ((FS.HISFC.Models.Base.Employee)dataManager.Operator).Dept;

            this.tvCompound.SelectDataEvent += new tvCompoundList.SelectDataHandler(tvCompound_SelectDataEvent);

            //取医嘱类型，用于将编码转换成名称
            FS.HISFC.BizLogic.Manager.OrderType orderManager = new FS.HISFC.BizLogic.Manager.OrderType();

            ArrayList alOrderType = orderManager.GetList();

            //存储所有化疗用法
            ArrayList alRadiotherapyUsage = consMgr.GetAllList("RadiotherapyUsage");

            if (alRadiotherapyUsage != null && alRadiotherapyUsage.Count > 0)
            {
                foreach (FS.FrameWork.Models.NeuObject usageObj in alRadiotherapyUsage)
                {
                    hsRadiotherapyUsage.Add(usageObj.ID, usageObj);
                }
            }

            foreach (FS.FrameWork.Models.NeuObject infoOrderType in alOrderType)
            {
                this.hsOrderType.Add(infoOrderType.ID, infoOrderType.Name);
            }

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            return 1;
        }

        /// <summary>
        /// 获取所有当前选中的数据
        /// </summary>
        /// <returns></returns>
        protected ArrayList GetCheckData()
        {
            ArrayList al = new ArrayList();

            for (int i = 0; i < this.fpApply_Sheet1.Rows.Count; i++)
            {
                if (NConvert.ToBoolean(this.fpApply_Sheet1.Cells[i, (int)ColumnSet.ColSelect].Value))
                {
                    FS.HISFC.Models.Pharmacy.ApplyOut appTemp = this.fpApply_Sheet1.Rows[i].Tag as FS.HISFC.Models.Pharmacy.ApplyOut;
                    appTemp.Item = this.itemManager.GetItem(appTemp.Item.ID);
                    appTemp.CompoundGroup = this.fpApply_Sheet1.Cells[i, (int)ColumnSet.ColCompoundGroup].Text;
                    al.Add(appTemp);
                }
            }

            return al;
        }

        /// <summary>
        /// 清屏
        /// </summary>
        /// <returns></returns>
        protected int Clear()
        {
            this.fpApply_Sheet1.Rows.Count = 0;

            return 1;
        }

        /// <summary>
        /// 列表显示
        /// </summary>
        /// <returns>成功返回1 失败返回-1</returns>
        protected virtual int ShowList()
        {
            this.Clear();

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(Language.Msg("正在加载配置数据,请稍候..."));
            Application.DoEvents();

            //检索所有申请信息
            //this.tvCompound.ShowListByParmacy(this.ApproveDept.ID, "C", ref this.alThisParList);

            //根据库存药房/批次获取列表

            FS.SOC.HISFC.BizLogic.Pharmacy.Compound itemManager = new FS.SOC.HISFC.BizLogic.Pharmacy.Compound();

            List<FS.HISFC.Models.Pharmacy.ApplyOut> alList = new List<FS.HISFC.Models.Pharmacy.ApplyOut>();

            alList = itemManager.QueryCompoundList(this.ApproveDept.ID, "U", "C", "ALL", true);

            this.tvCompound.ShowList(alList);

            //重置过滤按钮默认显示全部
            this.ReSetToolBarFilterToDefault();

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            return 1;

        }

        /// <summary>
        /// 重置按钮默认为查询全部
        /// </summary>
        private void ReSetToolBarFilterToDefault()
        {
            if (this.toolBarService.GetToolButton("普通临瞩") != null)
            {
                this.toolBarService.GetToolButton("普通临瞩").Text = "普通临瞩";
            }
            if (this.toolBarService.GetToolButton("化疗临瞩") != null)
            {
                this.toolBarService.GetToolButton("化疗临瞩").Text = "普通临瞩";
            }
            if (this.toolBarService.GetToolButton("长期") != null)
            {
                this.toolBarService.GetToolButton("长期").Text = "普通临瞩";
            }
            if (this.toolBarService.GetToolButton("全部") != null)
            {
                this.toolBarService.GetToolButton("全部").Text = "普通临瞩";
            }
        }

        /// <summary>
        /// 向Fp内加入数据
        /// </summary>
        /// <param name="alApply">摆药申请信息</param>
        /// <returns></returns>
        protected int AddDataToFp(ArrayList alData)
        {
            ArrayList alApply = new ArrayList();
            //按照组合排序
            ComboSort sort = new ComboSort();
            alData.Sort(sort);           

            this.fpApply_Sheet1.Rows.Count = 0;

            int i = 0;

            FS.HISFC.BizProcess.Integrate.Order orderIntegrate = new FS.HISFC.BizProcess.Integrate.Order();
            foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in alData)
            {

                this.fpApply_Sheet1.Rows.Add(i, 1);
                

                if (info.PrintState == "1")     //已打印项目
                {
                    this.fpApply_Sheet1.Rows[i].ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    this.fpApply_Sheet1.Rows[i].ForeColor = System.Drawing.Color.Black;
                }

                //对于作废数据 背景色显示红色
                if (info.ValidState != FS.HISFC.Models.Base.EnumValidState.Valid)
                {
                    this.fpApply_Sheet1.Rows[i].BackColor = System.Drawing.Color.Silver;
                }
                else
                {
                    this.fpApply_Sheet1.Rows[i].BackColor = System.Drawing.Color.White;
                }

                if (info.UseTime != System.DateTime.MinValue)
                {
                    this.fpApply_Sheet1.Cells[i, (int)ColumnSet.ColUseTime].Text = info.UseTime.ToString();
                }

                if (info.User01.Length > 4)
                {
                    this.fpApply_Sheet1.Cells[i, (int)ColumnSet.ColBedName].Text = "[" + info.User01.Substring(4) + "]" + info.User02;
                }
                else
                {
                    this.fpApply_Sheet1.Cells[i, (int)ColumnSet.ColBedName].Text = "[" + info.User01 + "]" + info.User02;
                }

                this.fpApply_Sheet1.Cells[i, (int)ColumnSet.ColSelect].Value = false;
                this.fpApply_Sheet1.Cells[i, (int)ColumnSet.ColTradeNameSpecs].Text = info.Item.Name + "[" + info.Item.Specs + "]";
                this.fpApply_Sheet1.Cells[i, (int)ColumnSet.ColRetailPrice].Text = info.Item.PriceCollection.RetailPrice.ToString();
                this.fpApply_Sheet1.Cells[i, (int)ColumnSet.ColDoseOnce].Text = info.DoseOnce.ToString();
                this.fpApply_Sheet1.Cells[i, (int)ColumnSet.ColDoseUnit].Text = info.Item.DoseUnit;
                this.fpApply_Sheet1.Cells[i, (int)ColumnSet.ColQty].Text = (info.Operation.ApplyQty * info.Days).ToString();
                this.fpApply_Sheet1.Cells[i, (int)ColumnSet.ColUnit].Text = info.Item.MinUnit;
                this.fpApply_Sheet1.Cells[i, (int)ColumnSet.ColFrequency].Text = info.Frequency.ID;
                this.fpApply_Sheet1.Cells[i, (int)ColumnSet.ColUsage].Text = info.Usage.Name;
                this.fpApply_Sheet1.Cells[i, (int)ColumnSet.ColCombNo].Text = info.CombNO + info.UseTime.ToString();
                if (info.OrderType == null || info.OrderType.ID == "")
                {
                    this.fpApply_Sheet1.Cells[i, (int)ColumnSet.ColOrderType].Text = "直接收费";
                }
                else
                {
                    if (this.hsOrderType.ContainsKey(info.OrderType.ID))
                    {
                        this.fpApply_Sheet1.Cells[i, (int)ColumnSet.ColOrderType].Text = this.hsOrderType[info.OrderType.ID].ToString();
                    }
                    else
                    {
                        this.fpApply_Sheet1.Cells[i, (int)ColumnSet.ColOrderType].Text = info.OrderType.ID;
                    }
                }

                this.fpApply_Sheet1.Cells[i, (int)ColumnSet.ColDoctor].Text = info.RecipeInfo.ID;
                this.fpApply_Sheet1.Cells[i, (int)ColumnSet.ColApplyTime].Text = info.Operation.ApplyOper.OperTime.ToString();
                this.fpApply_Sheet1.Cells[i, (int)ColumnSet.ColCompoundGroup].Text = info.CompoundGroup;
                this.fpApply_Sheet1.Rows[i].Tag = info;
            }
            //画组合
            SOC.HISFC.Components.Common.Function.DrawCombo(this.fpApply_Sheet1, (int)ColumnSet.ColCombNo, (int)ColumnSet.ColMoCombo);
            return 1;
        }
        
        /// <summary>
        /// 按照医嘱流水号获取该项目的初审复审信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public  string GetCheckCompounmdState(FS.HISFC.Models.Pharmacy.ApplyOut info)
        {

            string state = string.Empty;

            if (info == null || string.IsNullOrEmpty(info.OrderNO))
            {
                return state;
            }

            state = this.compoundMgr.GetCompoundState(info);

            return state;

        }

        /// <summary>
        /// 获取临瞩放疗项目
        /// </summary>
        /// <param name="allApplyData"></param>
        /// <returns></returns>
        private Hashtable GetRadiotherapyLZ(ArrayList allApplyData)
        {
            Hashtable hsCombo = new Hashtable();

            if (allApplyData == null || allApplyData.Count == 0)
            {
                return hsCombo;
            }

            foreach (FS.HISFC.Models.Pharmacy.ApplyOut applyInfo in allApplyData)
            {
                if (string.IsNullOrEmpty(applyInfo.CombNO))
                {
                    continue;
                }

                if (applyInfo.OrderType.ID != "LZ")
                {
                    continue;
                }
                
                if (hsCombo.Contains(applyInfo.CombNO))
                {
                    continue;
                }

                FS.HISFC.Models.Pharmacy.Item itemTmp = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(applyInfo.Item.ID);

                if (FS.FrameWork.Function.NConvert.ToBoolean(itemTmp.SpecialFlag))
                {
                    hsCombo.Add(applyInfo.CombNO, applyInfo.CombNO);

                    continue;
                }

                if (hsRadiotherapyUsage != null && hsRadiotherapyUsage.Count > 0)
                {
                    if (hsRadiotherapyUsage.Contains(applyInfo.Usage.ID))
                    {
                        ArrayList allDataTmp = new ArrayList();

                        allDataTmp.Add(applyInfo);

                        hsCombo.Add(applyInfo.CombNO, allDataTmp);

                        continue;

                    }
                }
            }
            return hsCombo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private int Save()
        {
            ArrayList alOriginalData = this.GetCheckData();
            if (alOriginalData.Count == 0)
            {
                return 0;
            }

            int parm = 0;
            System.Collections.Hashtable hscheck = new Hashtable();

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(Language.Msg("正在保存,请稍候..."));
            Application.DoEvents();
            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            FS.SOC.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.SOC.HISFC.BizLogic.Pharmacy.Item();

            DateTime sysTime = itemManager.GetDateTimeFromSysDateTime();

            for (int i = 0; i < alOriginalData.Count; i++)
            {
                FS.HISFC.Models.Pharmacy.ApplyOut info = alOriginalData[i] as FS.HISFC.Models.Pharmacy.ApplyOut;

                ///更新发药申请标识为待发药
                parm = this.compoundMgr.UpdateApplyOutState(NConvert.ToDecimal(info.ID), info.State, "0");

                if (parm == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(Language.Msg("更新药品申请信息状态出错"), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return -1;
                }

                ///只有长嘱才需要存审核信息，以便校验每次分解后是否通过，临时医嘱等则不需要，需要每次都审核
                if (info.OrderType.ID == "CZ")
                {
                    if (!hscheck.ContainsKey(info.OrderNO))
                    {
                        hscheck.Add(info.OrderNO, info);
                        ///向医嘱审核表中插入数据，相同mo_order只存一条数据即可
                        parm = this.compoundMgr.InsertCheckApplyOut(info);
                        if (parm == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(Language.Msg("插入医嘱审核数据出错"), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return -1;
                        }

                    }
                }
            }

            FS.FrameWork.Management.PublicTrans.Commit();
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            MessageBox.Show(Language.Msg("保存成功"), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.ShowList();

            return 1;
        }

        #endregion

        #region 事件

        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToUpper() != "DEVENV")
            {
                if (this.Init() == -1)
                {
                    MessageBox.Show(Language.Msg("初始化执行发生错误"));
                    return;
                }

                base.OnStatusBarInfo(null, "   字体红色代表已打印,背景黑色代表已作废");

            }
        }


        /// <summary>
        /// 保存事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnSave(object sender, object neuObject)
        {
            return this.Save();
        }

        /// <summary>
        /// 左侧树选择事件
        /// </summary>
        /// <param name="alData"></param>
        private void tvCompound_SelectDataEvent(ArrayList alData)
        {
            this.AddDataToFp(alData);
            this.SplitSecialLZ(alData);
            //this.SeCompoundCheckState();
        }

        /// <summary>
        /// 获取所有临瞩化疗用法
        /// </summary>
        /// <param name="alData"></param>
        private void SplitSecialLZ(ArrayList alData)
        {
            if (alData == null || alData.Count == 0)
            {
                return;
            }
            hsAllRadiotherapyLZ = this.GetRadiotherapyLZ(alData);
        }

        /// <summary>
        /// 设置初/复审情况
        /// </summary>
        public void SeCompoundCheckState()
        {
            if (this.fpApply_Sheet1.Rows.Count == 0)
            {
                return;
            }
            for(int index = 0;index < this.fpApply_Sheet1.Rows.Count;index++)
            {
                FS.HISFC.Models.Pharmacy.ApplyOut info = this.fpApply_Sheet1.Rows[index].Tag as FS.HISFC.Models.Pharmacy.ApplyOut;

                if(!string.IsNullOrEmpty(this.fpApply_Sheet1.Cells[index, (int)ColumnSet.ColCombNo].Text))
                {
                    string state = this.GetCheckCompounmdState(info);

                    if (state == "0")
                    {
                        this.fpApply_Sheet1.RowHeader.Rows[index].Label = "初审(新)";
                    }
                }
            }
        }

        /// <summary>
        /// 同组的选择一个则都选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpApply_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            FS.HISFC.Models.Pharmacy.ApplyOut apply = this.fpApply_Sheet1.ActiveRow.Tag as FS.HISFC.Models.Pharmacy.ApplyOut;
            if (apply == null)
            {
                return;
            }
            string combo = apply.CombNO;
            DateTime dateUse = apply.UseTime;
            if (e.Column == 1)
            {
                for (int i = 0; i < this.fpApply_Sheet1.RowCount; i++)
                {
                    FS.HISFC.Models.Pharmacy.ApplyOut info = this.fpApply_Sheet1.Rows[i].Tag as FS.HISFC.Models.Pharmacy.ApplyOut;
                    if (info == null)
                    {
                        return;
                    }
                    if (FS.FrameWork.Function.NConvert.ToBoolean(this.fpApply_Sheet1.Cells[this.fpApply_Sheet1.ActiveRowIndex, 1].Value))
                    {
                        if (info.UseTime == dateUse && info.CombNO == combo)
                        {
                            this.fpApply_Sheet1.Cells[i, 1].Value = true;
                        }
                    }
                    else
                    {
                        if (info.UseTime == dateUse && info.CombNO == combo)
                        {
                            this.fpApply_Sheet1.Cells[i, 1].Value = false;
                        }
                    }
                }
            }
        }

        int curRow = 0;
        FarPoint.Win.Spread.CellClickEventArgs cellClickEvent = null;
        private void fpApply_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.Row < 0)
                return;
            if (e.ColumnHeader == true)
                return;
            if (e.Column > 0)
            {
                try
                {
                    int active = this.fpApply.ActiveSheetIndex;
                    if (e.View.Sheets.Count <= active) active = 0;
                    curRow = active;
                    if (e.View.Sheets[active].Columns[3].Label == "组") //组合号
                    {
                        if (e.Button == MouseButtons.Left) //左键
                        {
                        }
                        else if(e.Button == MouseButtons.Right)
                        {
                            cellClickEvent = e;
                            ContextMenu menu = new ContextMenu();
                            MenuItem mnuTip = new MenuItem("不合格");//取消审核
                            mnuTip.Click += new EventHandler(mnuTip_Click);
                            menu.MenuItems.Add(mnuTip);
                            this.fpApply.ContextMenu = menu;

                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }
        }

        #region 取消审核
        /// <summary>
        /// 取消审核
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuTip_Click(object sender, EventArgs e)
        {
            if (FS.FrameWork.Function.NConvert.ToBoolean(this.fpApply_Sheet1.Cells[this.fpApply_Sheet1.ActiveRowIndex, 1].Value))
            {
                FS.HISFC.Models.Pharmacy.ApplyOut applyInfo = this.fpApply_Sheet1.ActiveRow.Tag as FS.HISFC.Models.Pharmacy.ApplyOut;
                ucResultInput ucRInput = new ucResultInput();
                if(applyInfo != null)
                {
                    //审核结果表的审核状态
                    string state = "1";
                    ucRInput.Result = this.compoundMgr.GetCompoundNode(applyInfo.PatientNO, applyInfo.OrderNO,state);
                }
                ucRInput.OKEvent += new myTipEvent(ucRInput_OKEvent);
                FS.FrameWork.WinForms.Classes.Function.PopShowControl(ucRInput);
            }
        }

        void ucRInput_OKEvent(string result)
        {
            if(string.IsNullOrEmpty(result.Trim()))
            {
                MessageBox.Show("未录入审核原因，取消审核失败！","提示",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }
            //获取同组的所有项目
            ArrayList alComboOrder = new ArrayList();
            ArrayList allCombos = new ArrayList();
            FS.HISFC.Models.Pharmacy.ApplyOut applyInfo = this.fpApply_Sheet1.ActiveRow.Tag as FS.HISFC.Models.Pharmacy.ApplyOut;
            string comboNo = applyInfo.CombNO;
            string state = "1";
            string operCode = (this.itemManager.Operator as FS.HISFC.Models.Base.Employee).ID;
            DateTime operTime = this.itemManager.GetDateTimeFromSysDateTime();
            DateTime dateuse = applyInfo.UseTime;
            for (int i = 0; i < this.fpApply_Sheet1.RowCount; i++)
            {
                FS.HISFC.Models.Pharmacy.ApplyOut info = this.fpApply_Sheet1.Rows

[i].Tag as FS.HISFC.Models.Pharmacy.ApplyOut;
                if (info == null)
                {
                    return;
                }
                if (info.CombNO == comboNo)
                {
                    allCombos.Add(info);
                }
                if (info.UseTime == dateuse && info.CombNO == comboNo)
                {
                    alComboOrder.Add(info);
                }
            }
            if (alComboOrder != null && alComboOrder.Count != 0)
            {
                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                foreach (FS.HISFC.Models.Pharmacy.ApplyOut applyOut in alComboOrder)
                {
                    int param2 = this.compoundMgr.InsertCompoundNode(applyOut.PatientNO, applyOut.OrderNO, result, state, operCode, operTime);
                    if (param2 == -1)
                    {
                        int param3 = this.compoundMgr.UpdateCompoundNode(applyOut.PatientNO, applyOut.OrderNO, state, result);
                        if (param3 == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("后台插入配置原因出错，请联系信息科！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                }
            }
            if (allCombos != null && allCombos.Count != 0)
            {
                foreach (FS.HISFC.Models.Pharmacy.ApplyOut applyOut in allCombos)
                {
                    int parm1 = this.compoundMgr.UpdateApplyOutState(NConvert.ToDecimal(applyOut.ID), applyOut.State, "U");

                    if (parm1 == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(Language.Msg("更新药品申请信息状态出错"), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return ;
                    }
                }
            }
                
                FS.FrameWork.Management.PublicTrans.Commit();
                this.SetCheckState(comboNo);
        }

        private void SetCheckState(string comboNo)
        {
            if (this.fpApply_Sheet1.RowCount == 0)
            {
                return;
            }
            for (int i = 0; i < this.fpApply_Sheet1.RowCount; i++)
            {
                FS.HISFC.Models.Pharmacy.ApplyOut info = this.fpApply_Sheet1.Rows[i].Tag as FS.HISFC.Models.Pharmacy.ApplyOut;
                if (info.CombNO == comboNo)
                {
                    this.fpApply_Sheet1.RowHeader.Rows[i].BackColor = System.Drawing.Color.Red;
                    this.fpApply_Sheet1.Cells[i, (int)ColumnSet.ColSelect].Value = true;
                }
            }

        }
        #endregion

        #endregion

        #region 枚举
        /// <summary>
        /// 列设置
        /// </summary>
        private enum ColumnSet
        {
            /// <summary>
            /// 床号 姓名
            /// </summary>
            ColBedName,
            /// <summary>
            /// 选中
            /// </summary>
            ColSelect,
            /// <summary>
            /// 批次号
            /// </summary>
            ColCompoundGroup,
            /// <summary>
            /// 画组合
            /// </summary>
            ColMoCombo,
            /// <summary>
            /// 药品名称 规格
            /// </summary>
            ColTradeNameSpecs,
            /// <summary>
            /// 零售价
            /// </summary>
            ColRetailPrice,
            /// <summary>
            /// 用量
            /// </summary>
            ColDoseOnce,
            /// <summary>
            /// 剂量单位
            /// </summary>
            ColDoseUnit,
            /// <summary>
            /// 总量
            /// </summary>
            ColQty,
            /// <summary>
            /// 单位
            /// </summary>
            ColUnit,
            /// <summary>
            /// 频次
            /// </summary>
            ColFrequency,
            /// <summary>
            /// 用法
            /// </summary>
            ColUsage,
            /// <summary>
            /// 用药时间
            /// </summary>
            ColUseTime,
            /// <summary>
            /// 医嘱类型
            /// </summary>
            ColOrderType,
            /// <summary>
            /// 开方医生
            /// </summary>
            ColDoctor,
            /// <summary>
            /// 申请时间
            /// </summary>
            ColApplyTime,
            /// <summary>
            /// 组合号
            /// </summary>
            ColCombNo

        }

        #endregion

        #region 排序
        /// <summary>
        /// 按照组合排序
        /// </summary>
        class ComboSort : System.Collections.IComparer
        {
            public int Compare(object x, object y)
            {
                FS.HISFC.Models.Pharmacy.ApplyOut o1 = x as FS.HISFC.Models.Pharmacy.ApplyOut;
                FS.HISFC.Models.Pharmacy.ApplyOut o2 = y as FS.HISFC.Models.Pharmacy.ApplyOut;
                string str1 = o1.CombNO + o1.UseTime.ToString();
                string str2 = o2.CombNO + o2.UseTime.ToString();
                return string.Compare(str1, str2);
            }
        }
        /// <summary>
        /// 按照批次号排序
        /// </summary>
        class CompandGroupSort : System.Collections.IComparer
        {
            public int Compare(object x, object y)
            {
                FS.HISFC.Models.Pharmacy.ApplyOut o1 = x as FS.HISFC.Models.Pharmacy.ApplyOut;
                FS.HISFC.Models.Pharmacy.ApplyOut o2 = y as FS.HISFC.Models.Pharmacy.ApplyOut;
                string str1 = o1.CompoundGroup + o1.Item.ID;
                string str2 = o2.CompoundGroup + o2.Item.ID;

                return string.Compare(str1, str2);
            }
        }


        /// <summary>
        /// 按照组合排序
        /// </summary>
        class ComboOrderSort : System.Collections.IComparer
        {
            public int Compare(object x, object y)
            {
                FS.HISFC.Models.Order.ExecOrder o1 = x as FS.HISFC.Models.Order.ExecOrder;
                FS.HISFC.Models.Order.ExecOrder o2 = y as FS.HISFC.Models.Order.ExecOrder;
                string str1 = o1.Order.Combo.ID + o1.DateUse.ToString();
                string str2 = o2.Order.Combo.ID + o2.DateUse.ToString();
                return string.Compare(str1, str2);
            }
        }

        #endregion

       
    }
}
