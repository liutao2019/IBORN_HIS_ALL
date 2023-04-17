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
    /// <br></br>
    /// [功能描述: 配置标签补打]<br></br>
    /// [创 建 者: 梁俊泽]<br></br>
    /// [创建时间: 2007-08]<br></br>
    /// <说明>
    ///     1、
    /// </说明>
    /// </summary>
    public partial class ucCompoundRePrint : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucCompoundRePrint()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 核准科室
        /// </summary>
        private FS.FrameWork.Models.NeuObject approveDept = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// 组件患者选择树
        /// </summary>
        private tvCompoundList tvCompound = null;

        /// <summary>
        /// 医嘱批次信息
        /// </summary>
        private string groupCode = "U";

        /// <summary>
        /// 医嘱类型集合
        /// </summary>
        private System.Collections.Hashtable hsOrderType = new Hashtable();

        /// <summary>
        /// 用于存储所有化疗临瞩
        /// </summary>
        Hashtable hsAllRadiotherapyLZ = new Hashtable();

        /// <summary>
        /// 用于存储所有化疗用法
        /// </summary>
        Hashtable hsRadiotherapyUsage = new Hashtable();

        /// <summary>
        /// 配置中心管理类
        /// </summary>
        private FS.SOC.HISFC.BizLogic.Pharmacy.Compound itemManager = new FS.SOC.HISFC.BizLogic.Pharmacy.Compound();

        /// <summary>
        /// 常数管理类
        /// </summary>
        private FS.HISFC.BizLogic.Manager.Constant consMgr = new FS.HISFC.BizLogic.Manager.Constant();

        /// <summary>
        /// 配置中心单据打印接口
        /// </summary>
        private FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.ICompoundPrint iCompoundPrintObj;

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

        /// <summary>
        /// 所选择的医嘱批次
        /// </summary>
        private string GroupCode
        {
            get
            {
                if (this.cmbOrderGroup.Text == "" || this.cmbOrderGroup.Text == null || this.cmbOrderGroup.Text == "全部")
                {
                    this.groupCode = "U";
                }
                else
                {
                    this.groupCode = this.cmbOrderGroup.Text;
                }

                return this.groupCode;
            }
        }

        /// <summary>
        /// 本次检索最大时间
        /// </summary>
        private DateTime MaxDate
        {
            get
            {
                return NConvert.ToDateTime(this.dtMaxValue.Text);
            }
        }

        /// <summary>
        /// 本次检索最小时间
        /// </summary>
        private DateTime MinDate
        {
            get
            {
                return NConvert.ToDateTime(this.dtMinValue.Text);
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
            this.toolBarService.AddToolButton("补打标签", "补打配置标签", FS.FrameWork.WinForms.Classes.EnumImageList.Y预览, true, false, null);
            this.toolBarService.AddToolButton("补打执行", "补打执行明细单", FS.FrameWork.WinForms.Classes.EnumImageList.D打印, true, false, null);
            this.toolBarService.AddToolButton("刷新", "刷新患者列表显示", FS.FrameWork.WinForms.Classes.EnumImageList.S刷新, true, false, null);
            this.toolBarService.AddToolButton("普通临瞩", "过滤普通临时医嘱", FS.FrameWork.WinForms.Classes.EnumImageList.Y医嘱, true, false, null);
            return this.toolBarService;
        }

        /// <summary>
        /// 保存事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnSave(object sender, object neuObject)
        {
            return base.OnSave(sender, neuObject);
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
                case "补打标签":
                    this.Print(true, false);
                    break;
                case "补打执行":
                    this.Print(false, true);
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

        protected override int OnPrint(object sender, object neuObject)
        {
            return base.OnPrint(sender, neuObject);
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            this.QueryCompound(this.txtCompoundGroup.Text);

            return base.OnQuery(sender, neuObject);
        }

        #endregion

        private void Print(bool isPrintLabel, bool isPrintDetail)
        {
            ArrayList alCheck = this.GetCheckData();
            if (iCompoundPrintObj == null)
            {
                return;
            }
            if (isPrintLabel)
            {
                iCompoundPrintObj.PrintCompoundLabel(alCheck);
            }
            if (isPrintDetail)
            {
                iCompoundPrintObj.PrintCompoundList(alCheck);
            }
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
        /// 过滤
        /// </summary>
        /// <returns></returns>
        private int Filter(string tag)
        {
            ArrayList list;
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
        /// 初始化所有接口
        /// </summary>
        private void InitInterface()
        {
            iCompoundPrintObj = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.ICompoundPrint)) as FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.ICompoundPrint;
        }

        /// <summary>
        /// 初始化医嘱批次信息
        /// </summary>
        /// <returns></returns>
        private int InitOrderGroup()
        {
            FS.SOC.HISFC.BizLogic.Pharmacy.Constant consManager = new FS.SOC.HISFC.BizLogic.Pharmacy.Constant();

            List<FS.HISFC.Models.Pharmacy.OrderGroup> orderGroupList = consManager.QueryOrderGroup();
            if (orderGroupList == null)
            {
                MessageBox.Show(Language.Msg("获取医嘱批次信息发生错误"));
                return -1;
            }

            string[] strOrderGroup = new string[orderGroupList.Count + 1];
            strOrderGroup[0] = "全部";
            int i = 1;
            foreach (FS.HISFC.Models.Pharmacy.OrderGroup info in orderGroupList)
            {
                strOrderGroup[i] = info.ID;
                i++;
            }

            this.cmbOrderGroup.Items.AddRange(strOrderGroup);
            this.cmbOrderGroup.SelectedIndex = 0;
            string orderGroup = consManager.GetOrderGroup(consManager.GetDateTimeFromSysDateTime());
            if (orderGroup != "")
            {
                this.cmbOrderGroup.Text = orderGroup;
            }

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

            //根据库存药房/批次获取列表
            FS.SOC.HISFC.BizLogic.Pharmacy.Compound itemManager = new FS.SOC.HISFC.BizLogic.Pharmacy.Compound();
            List<FS.HISFC.Models.Pharmacy.ApplyOut> alList = itemManager.QueryCompoundListByUseTime(this.ApproveDept.ID, groupCode, "2", "0", true, this.MinDate, this.MaxDate);
            if (alList == null)
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show(Language.Msg("获取配置单列表发生错误") + itemManager.Err);
                return -1;
            }

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
        /// 数据清屏
        /// </summary>
        protected void Clear()
        {
            this.fpApply_Sheet1.Rows.Count = 0;
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
                this.fpApply_Sheet1.Cells[i, (int)ColumnSet.ColSelect].Value = isCheck;
            }

            return 1;
        }

        /// <summary>
        /// 根据批次流水号检索
        /// </summary>
        /// <param name="compoundGroup">批次流水号</param>
        protected void QueryCompound(string compoundGroup)
        {
            if (compoundGroup == null || compoundGroup == "")
            {
                return;
            }

            FS.SOC.HISFC.BizLogic.Pharmacy.Compound itemManager = new FS.SOC.HISFC.BizLogic.Pharmacy.Compound();
            ArrayList alList = itemManager.QueryCompoundApplyOut(compoundGroup);
            if (alList == null)
            {
                MessageBox.Show(Language.Msg("根据批次流水号获取配置数据发生错误") + itemManager.Err);
                return;
            }

            this.AddDataToFp(alList);
        }

        /// <summary>
        /// 向Fp内加入数据
        /// </summary>
        /// <param name="alApply">摆药申请信息</param>
        /// <returns></returns>
        protected int AddDataToFp(ArrayList alApply)
        {
            this.fpApply_Sheet1.Rows.Count = 0;

            int i = 0;

            FS.HISFC.BizProcess.Integrate.Order orderIntegrate = new FS.HISFC.BizProcess.Integrate.Order();

            foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in alApply)
            {
                this.fpApply_Sheet1.Rows.Add(i, 1);

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

                this.fpApply_Sheet1.Cells[i, (int)ColumnSet.ColSelect].Value = true;
                this.fpApply_Sheet1.Cells[i, (int)ColumnSet.ColTradeNameSpecs].Text = info.Item.Name + "[" + info.Item.Specs + "]";
                this.fpApply_Sheet1.Cells[i, (int)ColumnSet.ColRetailPrice].Text = info.Item.PriceCollection.RetailPrice.ToString();
                this.fpApply_Sheet1.Cells[i, (int)ColumnSet.ColDoseOnce].Text = info.DoseOnce.ToString();
                this.fpApply_Sheet1.Cells[i, (int)ColumnSet.ColDoseUnit].Text = info.Item.DoseUnit;
                this.fpApply_Sheet1.Cells[i, (int)ColumnSet.ColQty].Text = (info.Operation.ApplyQty * info.Days).ToString();
                this.fpApply_Sheet1.Cells[i, (int)ColumnSet.ColUnit].Text = info.Item.MinUnit;
                this.fpApply_Sheet1.Cells[i, (int)ColumnSet.ColFrequency].Text = info.Frequency.ID;
                this.fpApply_Sheet1.Cells[i, (int)ColumnSet.ColUsage].Text = info.Usage.Name;

                this.fpApply_Sheet1.Cells[i, (int)ColumnSet.ColDoctor].Text = info.RecipeInfo.ID;
                this.fpApply_Sheet1.Cells[i, (int)ColumnSet.ColApplyTime].Text = info.Operation.ApplyOper.OperTime.ToString();
                this.fpApply_Sheet1.Cells[i, (int)ColumnSet.ColCompoundGroup].Text = info.CompoundGroup;
                this.fpApply_Sheet1.Cells[i, (int)ColumnSet.ColCombNo].Text = info.CombNO + info.UseTime;

                if (this.hsOrderType.ContainsKey(info.OrderType.ID))
                {
                    this.fpApply_Sheet1.Cells[i, (int)ColumnSet.ColOrderType].Text = this.hsOrderType[info.OrderType.ID].ToString();
                }
                else
                {
                    this.fpApply_Sheet1.Cells[i, (int)ColumnSet.ColOrderType].Text = info.OrderType.ID;
                }

                this.fpApply_Sheet1.Rows[i].Tag = info;
            }
            FS.SOC.HISFC.Components.Common.Function.DrawCombo(this.fpApply_Sheet1, (int)ColumnSet.ColCombNo, (int)ColumnSet.ColMoCombo); 
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
                    al.Add(this.fpApply_Sheet1.Rows[i].Tag as FS.HISFC.Models.Pharmacy.ApplyOut);
                }
            }

            return al;
        }

        protected override void OnLoad(EventArgs e)
        {
            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToUpper() != "DEVENV")
            {
                if (this.Init() == -1)
                {
                    MessageBox.Show(Language.Msg("初始化执行发生错误"));
                    return;
                }
            }

            base.OnLoad(e);
        }

        private void txtCompoundGroup_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.Clear();

                this.QueryCompound(this.txtCompoundGroup.Text);
            }
        }

        /// <summary>
        /// 数据初始化
        /// </summary>
        /// <returns></returns>
        protected virtual int Init()
        {
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(Language.Msg("正在加载基础数据.请稍候..."));
            Application.DoEvents();

            if (this.tv != null)
            {
                this.tvCompound = this.tv as FS.SOC.HISFC.Components.DrugStore.Compound.tvCompoundList;

                this.tvCompound.Init();

                this.tvCompound.State = "2";
            }

            DateTime date = this.itemManager.GetDateTimeFromSysDateTime();
            string str1 = date.Date.AddDays(0).ToShortDateString() + " " + "00:00:00";
            string str2 = date.Date.AddDays(1).ToShortDateString() + " " + "23:59:59";
            this.dtMinValue.Value = FS.FrameWork.Function.NConvert.ToDateTime(str1);
            this.dtMaxValue.Value = FS.FrameWork.Function.NConvert.ToDateTime(str2);

            FS.FrameWork.Management.DataBaseManger dataManager = new DataBaseManger();

            this.approveDept = ((FS.HISFC.Models.Base.Employee)dataManager.Operator).Dept;

            //存储所有化疗用法
            ArrayList alRadiotherapyUsage = consMgr.GetAllList("RadiotherapyUsage");

            if (alRadiotherapyUsage != null && alRadiotherapyUsage.Count > 0)
            {
                foreach (FS.FrameWork.Models.NeuObject usageObj in alRadiotherapyUsage)
                {
                    hsRadiotherapyUsage.Add(usageObj.ID, usageObj);
                }
            }

            //取医嘱类型，用于将编码转换成名称
            FS.HISFC.BizLogic.Manager.OrderType orderManager = new FS.HISFC.BizLogic.Manager.OrderType();
            ArrayList alOrderType = orderManager.GetList();
            foreach (FS.FrameWork.Models.NeuObject infoOrderType in alOrderType)
            {
                this.hsOrderType.Add(infoOrderType.ID, infoOrderType.Name);
            }

            this.InitOrderGroup();

            this.InitInterface();

            this.tvCompound.SelectDataEvent += new tvCompoundList.SelectDataHandler(tvCompound_SelectDataEvent);
            this.tvCompound.GetSelectApplyDataEvent +=new tvCompoundList.GetSelectApplyDataHandler(tvCompound_GetSelectApplyDataEvent); 

            //this.ShowList();

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            return 1;
        }

        private void tvCompound_SelectDataEvent(ArrayList alData)
        {
            this.AddDataToFp(alData);
            this.SplitSecialLZ(alData);
        }

        private void tvCompound_GetSelectApplyDataEvent(FS.HISFC.Models.Pharmacy.ApplyOut info,string patientNO, string groupCode, string state, bool isExec, ref ArrayList al)
        {
            al = this.itemManager.QueryCompoundApplyOutByUseTime(info.StockDept.ID, info.ApplyDept.ID, groupCode, patientNO, state, true,this.MinDate,this.MaxDate);
        }

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

        private void cmbOrderGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.tvCompound.GroupCode = this.GroupCode;

            this.ShowList();
        }

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
            /// 开方医生
            /// </summary>
            ColDoctor,
            /// <summary>
            /// 申请时间
            /// </summary>
            ColApplyTime,
            /// <summary>
            /// 批次号
            /// </summary>
            ColCompoundGroup,
            /// <summary>
            /// 组合号
            /// </summary>
            ColCombNo,
            /// <summary>
            /// 医嘱类型
            /// </summary>
            ColOrderType
        }

        
    }
}
