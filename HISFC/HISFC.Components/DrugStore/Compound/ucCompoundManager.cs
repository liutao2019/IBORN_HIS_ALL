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

namespace FS.HISFC.Components.DrugStore.Compound
{
    /// <summary>
    /// <br></br>
    /// [功能描述: 配置管理主程序]<br></br>
    /// [创 建 者: 梁俊泽]<br></br>
    /// [创建时间: 2007-08]<br></br>
    /// <说明>
    ///     1、移植天津配液中心
    ///     2、实现打印标签、确认发药分离
    ///     3、配液批次算法接口实现
    ///     4. 增加作废标签打印，更改批次号，单独药品检索、临时长期过滤、配液标签卡片显示、标签分组等功能
    /// </说明>
    /// <修改说明>
    ///   修改实现人：梁俊泽、孙久海、冯超{432F8D1A-80F9-45e1-9FE6-7332C49487BA}
    /// </修改说明>
    /// </summary>
    public partial class ucCompoundManager : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.HISFC.BizProcess.Interface.Pharmacy.ICompoundGroup
    {
        public ucCompoundManager()
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
        private FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();

        /// <summary>
        /// 药品管理整合业务类
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Pharmacy pharmacyIntegrate = new FS.HISFC.BizProcess.Integrate.Pharmacy();
        /// <summary>
        /// 医嘱管理整合业务类
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Order orderIntegrate = new FS.HISFC.BizProcess.Integrate.Order();

        /// <summary>
        /// 组件患者选择树
        /// </summary>
        private tvCompoundList tvCompound = null;

        /// <summary>
        /// 医嘱批次信息
        /// </summary>
        private string groupCode = "U";
        /// <summary>
        /// 药品信息
        /// </summary>
        private string drugCode = "ALL";
        /// <summary>
        /// 是否需配置确认
        /// </summary>
        private bool isNeedConfirm = true;

        /// <summary>
        /// 医嘱类型集合
        /// </summary>
        private System.Collections.Hashtable hsOrderType = new Hashtable();
        /// <summary>
        /// 频次数组
        /// </summary>
        ArrayList alFrequency = null;
        /// <summary>
        /// 药理作用数组
        /// </summary>
        ArrayList alPharmcyFunction = null;
        /// <summary>
        /// 存储数据
        /// </summary>
        Hashtable hsTable = null;

        /// <summary>
        /// 是否显示卡片样式
        /// </summary>
        private bool isShowCardStyle = false;

        /// <summary>
        /// 是否显示列表样式
        /// </summary>
        private bool isShowListStyle = false;

        /// <summary>
        /// 管理层
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();
        /// <summary>
        /// 用于存储单药品检索时返回所有病区数据
        /// </summary>
        ArrayList alThisParList = new ArrayList();

        /// <summary>
        /// 欠费提示类型
        /// </summary>
        private FS.HISFC.Models.Base.MessType messType = FS.HISFC.Models.Base.MessType.Y;

        /// <summary>
        /// 当前窗口功能项
        /// </summary>
        private WinFun currentWinFun = WinFun.Print;

        /// <summary>
        /// 是否操作作废数据 True 操作 False 不操作
        /// </summary>
        private bool isManagerUnvalidData = false;
        /// <summary>
        /// 是否作废标签打印
        /// </summary>
        private bool cancelLablePrint = false;

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
        /// 所选择的药品
        /// </summary>
        private string DrugCode
        {
            get
            {
                if (this.cmbParmacy.Text == "" || this.cmbParmacy.Tag == null)
                {
                    this.drugCode = "ALL";
                }
                else
                {
                    this.drugCode = this.cmbParmacy.Tag.ToString();
                }

                return this.drugCode;
            }
        }

        /// <summary>
        /// 本次检索最大时间
        /// </summary>
        private DateTime MaxDate
        {
            get
            {
                return NConvert.ToDateTime(this.dtMaxDate.Text);
            }
        }

        /// <summary>
        /// 本次检索最小时间
        /// </summary>
        private DateTime MinDate
        {
            get
            {
                return NConvert.ToDateTime(this.dtMinDate.Text);
            }
        }

        /// <summary>
        /// 是否显示卡片样式
        /// </summary>
        [Description("是否显示卡片样式"), Category("设置"), DefaultValue(false)]
        public bool IsShowCardStyle
        {
            get
            {
                return isShowCardStyle;
            }
            set
            {
                isShowCardStyle = value;
            }
        }

        /// <summary>
        /// 是否显示列表样式
        /// </summary>
        [Description("是否显示列表样式"), Category("设置"), DefaultValue(false)]
        public bool IsShowListStyle
        {
            get
            {
                return isShowListStyle;
            }
            set
            {
                isShowListStyle = value;
            }
        }

        /// <summary>
        /// 是否判断欠费，欠费是否提示
        /// </summary>
        [Description("设置欠费提示类型 Y 只提示欠费 不可以收费 M 提示欠费 但还可以收费 N 不判断欠费"), Category("设置")]
        public FS.HISFC.Models.Base.MessType MessageType
        {
            set
            {
                messType = value;
            }
            get
            {
                return messType;
            }
        }

        /// <summary>
        /// 当前窗口功能项 Print 当前窗口项为单据打印  Save 当前窗口项为保存。
        /// </summary>
        [Description("当前窗口功能项 Print 当前窗口项为单据打印  Save 当前窗口项为保存"), Category("设置")]
        public WinFun CurrentWinFun
        {
            get
            {
                return this.currentWinFun;
            }
            set
            {
                this.currentWinFun = value;
            }
        }

        /// <summary>
        /// 是否操作作废数据 True 操作 False 不操作
        /// </summary>
        [Description("是否操作作废数据 True 操作 False 不操作 该属性仅当CurrentWinFun设置为Save时有效"), Category("设置")]
        public bool IsManagerUnValidData
        {
            get
            {
                return this.isManagerUnvalidData;
            }
            set
            {
                this.isManagerUnvalidData = value;
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

            this.toolBarService.AddToolButton("作废申请", "作废当前选择的整组药品申请信息", FS.FrameWork.WinForms.Classes.EnumImageList.M模版删除, true, false, null);

            this.toolBarService.AddToolButton("作废标签打印", "打印已作废的标签并更新标志", FS.FrameWork.WinForms.Classes.EnumImageList.D打印, true, false, null);

            this.toolBarService.AddToolButton("作废标签补打", "打印已打印过的作废标签", FS.FrameWork.WinForms.Classes.EnumImageList.D打印, true, false, null);

            this.toolBarService.AddToolButton("更改批号", "保存新的批号", FS.FrameWork.WinForms.Classes.EnumImageList.D打印, true, false, null);

            this.toolBarService.AddToolButton("临时", "过滤长期医嘱", FS.FrameWork.WinForms.Classes.EnumImageList.Y医嘱, true, false, null);


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
            //配液中心兼容卡片界面样式by Sunjh 2010-11-16 {5998231C-4049-4b99-89B1-62BF1A639F4B}
            if (this.IsShowCardStyle)
            {
                this.SynCheckedStates();
            }

            this.SaveApply();
            this.pnlCardCollections.Controls.Clear();

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
                case "作废申请":
                    this.CancelApplyout();
                    break;
                case "作废标签打印":
                    this.PrintCancelDate();
                    break;
                case "更改批号":
                    this.ModifyCompandGroup();
                    break;
                case "作废标签补打":
                    this.CancelLabelRePrint();
                    break;
                case "临时":
                    this.Filter("short");
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
            ArrayList list;
            if (tag == "short")
            {
                for (int i = 0; i < this.fpApply_Sheet1.Rows.Count; i++)
                {
                    if ((this.fpApply_Sheet1.Rows[i].Tag as FS.HISFC.Models.Pharmacy.ApplyOut).OrderType.ID.ToString() == "LZ")
                    {
                        this.fpApply_Sheet1.Rows[i].Visible = true;
                    }
                    else
                    {
                        this.fpApply_Sheet1.Rows[i].Visible = false;
                    }
                }
                if (this.IsShowCardStyle)
                {
                    list = new ArrayList();
                    for (int i = 0; i < this.fpApply_Sheet1.RowCount; i++)
                    {
                        if (this.fpApply_Sheet1.Rows[i].Visible)
                        {
                            list.Add(this.fpApply_Sheet1.Rows[i].Tag as FS.HISFC.Models.Pharmacy.ApplyOut);
                        }
                    }
                    this.SetCardData(list);
                }
                this.toolBarService.GetToolButton("临时").Text = "长期";
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
                if (this.IsShowCardStyle)
                {
                    list = new ArrayList();
                    for (int i = 0; i < this.fpApply_Sheet1.RowCount; i++)
                    {
                        if (this.fpApply_Sheet1.Rows[i].Visible)
                        {
                            list.Add(this.fpApply_Sheet1.Rows[i].Tag as FS.HISFC.Models.Pharmacy.ApplyOut);
                        }
                    }
                    this.SetCardData(list);
                }
                this.toolBarService.GetToolButton("长期").Text = "全部";
            }
            else
            {
                for (int i = 0; i < this.fpApply_Sheet1.Rows.Count; i++)
                {
                    this.fpApply_Sheet1.Rows[i].Visible = true;
                }
                if (this.IsShowCardStyle)
                {
                    list = new ArrayList();
                    for (int i = 0; i < this.fpApply_Sheet1.RowCount; i++)
                    {
                        if (this.fpApply_Sheet1.Rows[i].Visible)
                        {
                            list.Add(this.fpApply_Sheet1.Rows[i].Tag as FS.HISFC.Models.Pharmacy.ApplyOut);
                        }
                    }
                    this.SetCardData(list);
                }
                this.toolBarService.GetToolButton("全部").Text = "临时";
            }
            return 1;
        }
        /// <summary>
        /// 作废标签补打
        /// </summary>
        /// <returns></returns>
        private int CancelLabelRePrint()
        {
            if (!this.rbCancel.Checked || this.fpApply_Sheet1.Rows.Count <= 0)
            {
                return -1;
            }
            //获取选中数据
            ArrayList alCheck = this.GetCheckData();
            this.neuTabControl1.SelectedTab = this.tpCardStyle;
            if (this.neuTabControl1.SelectedTab == this.tpCardStyle)
            {
                for (int i = 0; i < this.pnlCardCollections.Controls.Count; i++)
                {
                    if (i % 2 != 0)
                    {
                        CheckBox ckk = this.pnlCardCollections.Controls[i] as CheckBox;
                        if (ckk.Checked)
                        {
                            ucCompoundLabel ucc = this.pnlCardCollections.Controls[Convert.ToInt32(ckk.Tag.ToString()) * 2] as ucCompoundLabel;
                            ucc.IsUnvalidTitle = true;          //作废标题显示
                            if (ucc.Tag is ArrayList)
                            {
                                alCheck.AddRange(ucc.Tag as ArrayList);
                            }
                            ucc.Print();
                        }
                    }
                }
            }
            this.ShowList();
            return 1;
        }
        /// <summary>
        /// 保存新的批次号
        /// </summary>
        /// <returns></returns>
        private int ModifyCompandGroup()
        {
            ArrayList alOriginalData = this.GetCheckData();
            if (alOriginalData.Count == 0)
            {
                return 0;
            }
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            //因为可能手工更改过批次号，所以保存的时候重新更新一下批次信息
            FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();
            foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in alOriginalData)
            {
                if (itemManager.UpdateCompoundGroup(info.ID, info.CompoundGroup) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(Language.Msg("更新批次出错") + itemManager.Err);
                    return -1;
                }
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show("更新批次号成功!");
            this.tvCompound.RefreshData();
            return 1;
        }
        /// <summary>
        /// 打印作废标签并且确认标识
        /// </summary>
        /// <returns></returns>
        private int PrintCancelDate()
        {
            ArrayList al = new ArrayList();
            //只加载无效数据
            for (int i = 0; i < this.fpApply_Sheet1.Rows.Count; i++)
            {
                FS.HISFC.Models.Pharmacy.ApplyOut appTemp = this.fpApply_Sheet1.Rows[i].Tag as FS.HISFC.Models.Pharmacy.ApplyOut;
                appTemp.Item = this.itemManager.GetItem(appTemp.Item.ID);
                appTemp.CompoundGroup = this.fpApply_Sheet1.Cells[i, (int)ColumnSet.ColCompoundGroup].Text;
                if (appTemp.ValidState != FS.HISFC.Models.Base.EnumValidState.Valid && appTemp.ExtFlag != "1"
                    && (bool)this.fpApply_Sheet1.Cells[i, (int)ColumnSet.ColSelect].Value)
                {
                    al.Add(appTemp);
                }
            }
            if (al.Count == 0)
            {
                MessageBox.Show("无作废数据！");
                return 0;
            }
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            //以下处理无效数据 通过设置扩展标记为“1” 标识不需要进行处理 在再次加载时不进行数据显示
            foreach (FS.HISFC.Models.Pharmacy.ApplyOut unvalidData in al)
            {
                unvalidData.ExtFlag = "1";
                string str = "";  //因为核心更新取消日期的时候用的是User03 而程序中User03被组号暂用，所以交换一下
                str = unvalidData.User03;
                unvalidData.User03 = unvalidData.UseTime.ToString();
                if (itemManager.UpdateApplyOut(unvalidData) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(Language.Msg("对作废数据进行确认标识时发生错误") + itemManager.Err);
                    return -1;
                }
                unvalidData.User03 = str;
            }

            FS.FrameWork.Management.PublicTrans.Commit();
            this.cancelLablePrint = true;//作废标签打印的时候不打印汇总明细，打印模式调用标签页打印方法
            this.Print(al);
            return 1;
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            if (this.neuTabControl1.SelectedTab == this.tpCardStyle)
            {
                if (this.IsShowCardStyle)
                {
                    for (int i = 0; i < this.pnlCardCollections.Controls.Count; i++)
                    {
                        if (i % 2 != 0)
                        {
                            CheckBox ckk = this.pnlCardCollections.Controls[i] as CheckBox;
                            if (ckk.Checked)
                            {
                                //ucCompoundLabelC ucc = this.pnlCardCollections.Controls[Convert.ToInt32(ckk.Tag.ToString()) * 2] as ucCompoundLabelC;
                                ucCompoundLabel ucc = this.pnlCardCollections.Controls[Convert.ToInt32(ckk.Tag.ToString()) * 2] as ucCompoundLabel;
                                if (ucc.GroupNO != "")
                                {
                                    for (int j = 0; j < this.fpApply_Sheet1.Rows.Count; j++)
                                    {
                                        if (this.fpApply_Sheet1.Cells[j, (int)ColumnSet.ColCompoundGroup].Text == ucc.GroupNO)
                                        {
                                            this.fpApply_Sheet1.Cells[j, (int)ColumnSet.ColSelect].Value = true;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                //ucCompoundLabelC ucc = this.pnlCardCollections.Controls[Convert.ToInt32(ckk.Tag.ToString()) * 2] as ucCompoundLabelC;
                                ucCompoundLabel ucc = this.pnlCardCollections.Controls[Convert.ToInt32(ckk.Tag.ToString()) * 2] as ucCompoundLabel;
                                if (ucc.GroupNO != "")
                                {
                                    for (int j = 0; j < this.fpApply_Sheet1.Rows.Count; j++)
                                    {
                                        if (this.fpApply_Sheet1.Cells[j, (int)ColumnSet.ColCompoundGroup].Text == ucc.GroupNO)
                                        {
                                            this.fpApply_Sheet1.Cells[j, (int)ColumnSet.ColSelect].Value = false;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            ArrayList alCheck = this.GetCheckData();
            if (this.currentWinFun == WinFun.Print)         //仅当打印状态下，才进行打印标记更新
            {
                FS.FrameWork.Management.PublicTrans.BeginTransaction();

                //string drugBillNO = this.itemManager.GetNewDrugBillNO();
                string drugBillNO = string.Empty;

                foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in alCheck)
                {
                    if (this.itemManager.UpdateApplyOutPrintState(info.ID, true) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("更新标签打印标记失败" + this.itemManager.Err);
                        return -1;
                    }

                    if (string.IsNullOrEmpty(info.DrugNO) == true)
                    {
                        info.DrugNO = drugBillNO;
                        if (this.itemManager.ExamApplyOut(info) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("更新摆药单号失败" + this.itemManager.Err);
                            return -1;
                        }
                    }
                }

                FS.FrameWork.Management.PublicTrans.Commit();
            }
            this.Print(alCheck);

            return base.OnPrint(sender, neuObject);
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            this.tvCompound.RefreshData();

            return base.OnQuery(sender, neuObject);
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

            FS.FrameWork.Management.DataBaseManger dataManager = new DataBaseManger();

            this.approveOper = dataManager.Operator;

            this.approveDept = ((FS.HISFC.Models.Base.Employee)dataManager.Operator).Dept;

            //this.InitOrderGroup();
            this.cmbOrderGroup.SelectedIndex = 0;
            DateTime date = dataManager.GetDateTimeFromSysDateTime();
            string str1 = date.Date.AddDays(1).ToShortDateString() + " " + "00:00:00";
            string str2 = date.Date.AddDays(1).ToShortDateString() + " " + "23:59:59";
            //this.dtMaxDate.Value = dataManager.GetDateTimeFromSysDateTime().Date.AddDays(1);
            this.dtMaxDate.Value = FS.FrameWork.Function.NConvert.ToDateTime(str2);
            this.dtMinDate.Value = FS.FrameWork.Function.NConvert.ToDateTime(str1);
            this.tvCompound.SelectDataEvent += new tvCompoundList.SelectDataHandler(tvCompound_SelectDataEvent);

            //取医嘱类型，用于将编码转换成名称
            FS.HISFC.BizLogic.Manager.OrderType orderManager = new FS.HISFC.BizLogic.Manager.OrderType();
            ArrayList alOrderType = orderManager.GetList();
            foreach (FS.FrameWork.Models.NeuObject infoOrderType in alOrderType)
            {
                this.hsOrderType.Add(infoOrderType.ID, infoOrderType.Name);
            }
            //药品列表
            List<FS.HISFC.Models.Pharmacy.Item> drugList = new List<FS.HISFC.Models.Pharmacy.Item>();
            drugList = itemManager.QueryItemList(false);
            if (drugList != null)
            {
                ArrayList list = new ArrayList();
                foreach (FS.HISFC.Models.Pharmacy.Item itemObj in drugList)
                {
                    list.Add(itemObj);
                }
                this.cmbParmacy.AddItems(list);
            }

            //配液中心兼容卡片界面样式by Sunjh 2010-11-16 {5998231C-4049-4b99-89B1-62BF1A639F4B}
            if (!this.IsShowCardStyle)
            {
                this.tpCardStyle.Hide();

                this.neuTabControl1.TabPages.Remove(this.tpCardStyle);
            }

            //临时处理 屏蔽列表数据显示方式
            if (this.isShowListStyle == false)
            {
                this.neuTabControl1.TabPages.Remove(this.tpListStyle);
            }

            this.fpApply_Sheet1.Columns[(int)ColumnSet.ColCompoundGroup].Locked = false;
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            return 1;
        }

        /// <summary>
        /// 初始化医嘱批次信息
        /// </summary>
        /// <returns></returns>
        private int InitOrderGroup()
        {
            FS.HISFC.BizLogic.Pharmacy.Constant consManager = new FS.HISFC.BizLogic.Pharmacy.Constant();

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

            string orderGroup = consManager.GetOrderGroup(consManager.GetDateTimeFromSysDateTime());
            if (orderGroup != "")
            {
                this.cmbOrderGroup.Text = orderGroup;
            }

            return 1;
        }

        /// <summary>
        /// 清屏
        /// </summary>
        /// <returns></returns>
        protected int Clear()
        {
            this.fpApply_Sheet1.Rows.Count = 0;

            this.pnlCardCollections.Controls.Clear();

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
            //如果单独药品检索则不分病区，检索所有病区的所有批次
            if (this.DrugCode != "ALL")
            {
                this.tvCompound.ShowListByParmacy(this.ApproveDept.ID, ref this.alThisParList);
            }
            else
            {
                //根据库存药房/批次获取列表
                FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();

                List<FS.HISFC.Models.Pharmacy.ApplyOut> alList = new List<FS.HISFC.Models.Pharmacy.ApplyOut>();

                if (this.currentWinFun == WinFun.Print)     //打印功能
                {
                    alList = itemManager.QueryCompoundList(this.ApproveDept.ID, this.GroupCode, "0", "ALL", true);
                }
                else
                {
                    //此时可包含无效数据 以打印负单据
                    alList = itemManager.QueryCompoundList(this.ApproveDept.ID, this.GroupCode, "0", "ALL", false);
                }
                if (alList == null)
                {
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    MessageBox.Show(Language.Msg("获取配置单列表发生错误") + itemManager.Err);
                    return -1;
                }

                this.tvCompound.ShowList(alList);
            }
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            return 1;
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

            foreach (FS.HISFC.Models.Pharmacy.ApplyOut tempApplyOut in alData)
            {
                if (this.rbCancel.Checked && this.currentWinFun == WinFun.Save)
                {
                    if (tempApplyOut.PrintState == "1" && tempApplyOut.ExtFlag == "1" && tempApplyOut.ValidState != FS.HISFC.Models.Base.EnumValidState.Valid)
                    {
                        alApply.Add(tempApplyOut);
                    }
                    continue;
                }
                if (tempApplyOut.ExtFlag == "1" && tempApplyOut.ValidState != FS.HISFC.Models.Base.EnumValidState.Valid)            //此标识表式已作废单据但完成了确认操作的数据 不进行显示
                {
                    continue;
                }

                if (this.isManagerUnvalidData == false)      //对于作废数据不进行操作
                {
                    if (tempApplyOut.ValidState != FS.HISFC.Models.Base.EnumValidState.Valid)  //无效数据
                    {
                        continue;
                    }
                }
                else                                         //可对作废数据进行操作
                {
                    if (this.currentWinFun == WinFun.Print) //即使上一个判断条件满足 当前窗口功能为打印时 不对作废数据进行操作
                    {
                        continue;
                    }
                }

                if (this.rbPrinting.Checked == true)        //检索未打印数据
                {
                    if (tempApplyOut.PrintState == "0" || string.IsNullOrEmpty(tempApplyOut.PrintState) == true)
                    {
                        alApply.Add(tempApplyOut);
                    }
                }
                else if (this.rbPrinted.Checked == true)    //检索已打印数据
                {
                    if (tempApplyOut.PrintState == "1")
                    {
                        alApply.Add(tempApplyOut);
                    }
                }
                else                                       //检索全部数据
                {
                    alApply.Add(tempApplyOut);
                }
            }
            //设置配液中心 卡片格式显示
            this.SetCardData(alApply);

            this.fpApply_Sheet1.Rows.Count = 0;

            int i = 0;

            FS.HISFC.BizProcess.Integrate.Order orderIntegrate = new FS.HISFC.BizProcess.Integrate.Order();
            foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in alApply)
            {
                if (info.UseTime > this.MaxDate || info.UseTime < this.MinDate)
                {
                    continue;
                }
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
                    this.fpApply_Sheet1.Rows[i].BackColor = System.Drawing.Color.Gray;
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
            FS.HISFC.Components.Common.Classes.Function.DrawCombo(this.fpApply_Sheet1, (int)ColumnSet.ColCombNo, (int)ColumnSet.ColMoCombo);
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
                    //al.Add(this.fpApply_Sheet1.Rows[i].Tag as FS.HISFC.Models.Pharmacy.ApplyOut);                    
                }
            }

            return al;
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

            //配液中心兼容卡片界面样式 by Sunjh 2010-11-16 {5998231C-4049-4b99-89B1-62BF1A639F4B}
            if (this.IsShowCardStyle)
            {
                for (int i = 0; i < this.pnlCardCollections.Controls.Count; i++)
                {
                    if (i % 2 != 0)
                    {
                        CheckBox ckk = this.pnlCardCollections.Controls[i] as CheckBox;
                        ckk.Checked = isCheck;
                    }
                }
            }

            return 1;
        }

        /// <summary>
        /// 保存申请
        /// </summary>
        /// <returns></returns>
        public virtual int SaveApply()
        {
            ArrayList alOriginalData = this.GetCheckData();
            if (alOriginalData.Count == 0)
            {
                return 0;
            }

            ArrayList alUnValidData = new ArrayList();      //无效数据

            ArrayList alCheck = new ArrayList();            //有效数据
            foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in alOriginalData)
            {
                if (info.ValidState == FS.HISFC.Models.Base.EnumValidState.Valid)
                {
                    alCheck.Add(info);
                }
                else
                {
                    alUnValidData.Add(info);
                }
            }
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(Language.Msg("正在保存,请稍候..."));
            Application.DoEvents();
            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();

            DateTime sysTime = itemManager.GetDateTimeFromSysDateTime();

            //FS.FrameWork.Management.Transaction t = new Transaction(FS.FrameWork.Management.Connection.Instance);
            //t.BeginTransaction();
            //itemManager.SetTrans(t.Trans);

            //暂时不处理药柜问题

            if (Function.DrugConfirm(alCheck, null, null, this.approveDept.Clone(), FS.FrameWork.Management.PublicTrans.Trans) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                return -1;
            }

            //if (Function.DrugApprove(alCheck, this.approveOper.ID, this.approveDept.ID, FS.FrameWork.Management.PublicTrans.Trans) == -1)
            //{
            //    FS.FrameWork.Management.PublicTrans.RollBack();
            //    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            //    return -1;
            //}

            //如不需配置确认则在此直接进行确认
            if (!this.isNeedConfirm)
            {
                FS.HISFC.Models.Base.OperEnvironment compoundOper = new FS.HISFC.Models.Base.OperEnvironment();
                compoundOper.OperTime = sysTime;
                compoundOper.ID = this.approveOper.ID;

                foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in alCheck)
                {
                    if (itemManager.UpdateCompoundApplyOut(info, compoundOper, true) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        MessageBox.Show(Language.Msg("更新配置确认信息发生错误") + itemManager.Err);
                        return -1;
                    }
                }
            }

            #region 附材计费 （屏蔽掉）
            //if (Function.CompoundFeeByUsage(alCheck, this.approveDept, FS.FrameWork.Management.PublicTrans.Trans) == -1)
            //{
            //    FS.FrameWork.Management.PublicTrans.RollBack();
            //    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            //    return -1;
            //} 
            #endregion

            //以下处理无效数据 通过设置扩展标记为“1” 标识不需要进行处理 在再次加载时不进行数据显示
            foreach (FS.HISFC.Models.Pharmacy.ApplyOut unvalidData in alUnValidData)
            {
                unvalidData.ExtFlag = "1";
                string str = "";  //因为核心更新取消日期的时候用的是User03 而程序中User03被组号暂用，所以交换一下
                str = unvalidData.User03;
                unvalidData.User03 = unvalidData.UseTime.ToString();
                if (itemManager.UpdateApplyOut(unvalidData) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(Language.Msg("对作废数据进行确认标识时发生错误") + itemManager.Err);
                    return -1;
                }
                unvalidData.User03 = str;
            }

            FS.FrameWork.Management.PublicTrans.Commit();
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            MessageBox.Show(Language.Msg("保存成功"), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //Function.PrintCompound(alCheck,true,true);

            if (alUnValidData.Count > 0)
            {
                this.cancelLablePrint = true;
                this.Print(alUnValidData);  //保存的时候只打印作废数据
            }
            this.ShowList();

            return 1;
        }

        /// <summary>
        /// 根据医嘱组合号作废发药申请和执行档 by Sunjh 2010-12-8 {68126BF1-DCE3-4383-9F25-046E2E74C717}
        /// </summary>
        /// <returns></returns>
        public void CancelApplyout()
        {
            //1 获取医嘱组合号
            ArrayList alCancel = this.GetCheckData();
            //2 作废发药申请和执行档
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            foreach (FS.HISFC.Models.Pharmacy.ApplyOut appCancel in alCancel)
            {
                //作废发药申请
                if (this.pharmacyIntegrate.CancelApplyOut(appCancel.ExecNO) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("作废药品申请时出错!");
                    return;
                }
                //作废执行档
                if (this.orderIntegrate.DcExecImmediateUnNormal(appCancel.ExecNO, true, FS.FrameWork.Management.Connection.Operator) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("作废医嘱执行档时出错!");
                    return;
                }
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show("作废申请信息成功!");
            //3 刷新界面列表
            this.ShowList();
        }

        /// <summary>
        /// 打印执行
        /// </summary>
        /// <param name="alPrintData"></param>
        public void Print(ArrayList alPrintData)
        {
            ArrayList alData = new ArrayList();

            alData = alPrintData;
            CompandGroupSort sort = new CompandGroupSort();
            alData.Sort(sort);  //先按照批次号排序在传入

            this.neuTabControl1.SelectedTab = this.tpCardStyle;//列表和卡片都调用一个打印函数

            if (this.neuTabControl1.SelectedTab == this.tpCardStyle)
            {
                for (int i = 0; i < this.pnlCardCollections.Controls.Count; i++)
                {
                    if (i % 2 != 0)
                    {
                        CheckBox ckk = this.pnlCardCollections.Controls[i] as CheckBox;
                        if (ckk.Checked)
                        {
                            //count1++;
                            if (this.currentWinFun == WinFun.Save)      //当前为保存状态 仅对作废数据进行打印
                            {
                                if (ckk.Text.IndexOf("[已作废]") == -1)   //当前为有效标签，不进行打印
                                {
                                    continue;
                                }
                                else   //对作废标签进行打印
                                {
                                    ucCompoundLabel ucc = this.pnlCardCollections.Controls[Convert.ToInt32(ckk.Tag.ToString()) * 2] as ucCompoundLabel;
                                    ucc.IsUnvalidTitle = true;          //作废标题显示
                                    if (ucc.Tag is ArrayList)
                                    {
                                        alData.AddRange(ucc.Tag as ArrayList);
                                    }
                                    ucc.Print();
                                }
                            }
                            else
                            {
                                ucCompoundLabel ucc = this.pnlCardCollections.Controls[Convert.ToInt32(ckk.Tag.ToString()) * 2] as ucCompoundLabel;
                                if (rbPrinted.Checked)
                                {
                                    ucc.IsPrintedTitle = true;
                                }
                                ucc.Print();

                            }
                        }
                    }
                }
            }
            else
            {
                Function.PrintCompound(alData, true, true);
            }

            if (alData.Count > 0 && !this.cancelLablePrint)
            {
                this.PrintDrugBills(alData);
            }

            this.ShowList();

        }



        private int SetCardData(ArrayList alData)
        {
            this.pnlCardCollections.Controls.Clear();
            ArrayList alDataClone = new ArrayList();
            foreach (FS.HISFC.Models.Pharmacy.ApplyOut apply in alData)
            {
                alDataClone.Add(apply.Clone());
            }
            CompandGroupSort sort = new CompandGroupSort();
            alDataClone.Sort(sort);  //先按照批次号排序在传入
            ArrayList alGroupApplyOut = new ArrayList();
            ArrayList alCombo = new ArrayList();
            string privUseTime = "";
            string privCombo = "-1";

            #region 标签分组 根据批次流水号

            foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in alDataClone)
            {
                if (info.UseTime > this.MaxDate || info.UseTime < this.MinDate)
                {
                    continue;
                }
                //处方号相同并且使用时间相同
                if ((privCombo == "-1" || (privCombo == info.CombNO && info.CombNO != ""))
                    && (privUseTime == "" || (privUseTime == info.UseTime.ToString() && info.UseTime.ToString() != "")))
                {
                    alCombo.Add(info.Clone());
                    privCombo = info.CombNO;
                    privUseTime = info.UseTime.ToString();
                    continue;
                }
                else			//不同处方号
                {
                    alGroupApplyOut.Add(alCombo);

                    privCombo = info.CombNO;
                    privUseTime = info.UseTime.ToString();
                    alCombo = new ArrayList();
                    alCombo.Add(info.Clone());
                }
            }

            if (alCombo.Count > 0)
            {
                alGroupApplyOut.Add(alCombo);
            }

            #endregion

            bool val = false;
            int i = 0;
            int j = 0;
            foreach (ArrayList alTemp in alGroupApplyOut)
            {
                //{D8B142A6-A344-45a4-930E-30561148E056}feng.ch
                if (this.DrugCode != "ALL") //单独药品检索
                {
                    #region 单药品处理

                    //如果该组中包含检索药品val为true
                    foreach (FS.HISFC.Models.Pharmacy.ApplyOut f in alTemp)
                    {
                        if (f.Item.ID == this.DrugCode)
                        {
                            val = true;
                            break;
                        }
                    }
                    //如果改组液体中不含检索药品则不继续进行
                    if (!val)
                    {
                        val = false;
                        continue;
                    }

                    #endregion
                }

                if (i >= 3)
                {
                    i = 0;
                    j += 1;
                }

                //大于4个的时候分页显示
                ucCompoundLabel ucclc = new ucCompoundLabel();
                if (alTemp.Count > 4)
                {
                    #region 分页处理

                    int count = 1;
                    int iCount = 1;
                    int pCount = 0;
                    ArrayList listSmall = new ArrayList();
                    ArrayList listBig = new ArrayList();
                    foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in alTemp)
                    {
                        if (count <= 4 * iCount)
                        {
                            count++;
                            listSmall.Add(info);
                            continue;    //超过4条时候向大数组中添加这4条记录
                        }
                        else
                        {
                            listBig.Add(listSmall);
                            listSmall = new ArrayList();
                            listSmall.Add(info);
                            iCount++;
                            count++;
                        }
                    }
                    if (listSmall.Count > 0)
                    {
                        listBig.Add(listSmall);
                    }

                    #endregion

                    foreach (ArrayList list in listBig)
                    {
                        if (i >= 3)
                        {
                            i = 0;
                            j += 1;
                        }
                        bool isHaveUnValid = false;

                        #region 如果该list里边存在作废数据 则题目处予以标识

                        foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in list)
                        {
                            if (info.ValidState != FS.HISFC.Models.Base.EnumValidState.Valid)
                            {
                                isHaveUnValid = true;
                                break;
                            }
                        }

                        #endregion

                        #region 标签页赋值、打印

                        pCount++;
                        ucclc = new ucCompoundLabel();
                        this.pnlCardCollections.Controls.Add(ucclc);
                        ucclc.Left = i * 5 + i * 235;
                        ucclc.Top = j * 20 + j * 307 + 20;
                        ucclc.Clear();
                        ucclc.LabelTotNum = Convert.ToDecimal(alGroupApplyOut.Count);
                        ucclc.ICount = i + j * 3 + 1;
                        ucclc.IPage = pCount + "/" + iCount;
                        ucclc.Tag = list;           //保存本标签的数据
                        ucclc.AddComboNonePrint(list);

                        CheckBox ckTemp = new CheckBox();
                        ckTemp.Left = ucclc.Left;
                        ckTemp.Top = ucclc.Top - 20;
                        ckTemp.Tag = Convert.ToString(i + j * 3);
                        ckTemp.Width = 235;

                        if (isHaveUnValid == true)      //包含作废数据
                        {
                            ckTemp.Text = "点击此处选择当前标签[已作废]                      ";
                            //ckTemp.ForeColor = System.Drawing.Color.Red;                            
                        }
                        else
                        {
                            ckTemp.Text = "点击此处选择当前标签                              ";
                            //ckTemp.ForeColor = System.Drawing.Color.Black;
                        }

                        FS.HISFC.Models.Pharmacy.ApplyOut tempApplyState = list[0] as FS.HISFC.Models.Pharmacy.ApplyOut;
                        if (tempApplyState.PrintState == "0" || string.IsNullOrEmpty(tempApplyState.PrintState) == true)
                        {
                            ckTemp.ForeColor = System.Drawing.Color.Black;
                            // ckTemp.Text = "点击此处选择当前标签                              ";//ckTemp.Tag.ToString();
                        }
                        else if (tempApplyState.PrintState == "1")
                        {
                            ckTemp.ForeColor = System.Drawing.Color.Red;
                            //ckTemp.Text = "点击此处选择当前标签[已打印]                      ";//ckTemp.Tag.ToString();
                        }

                        ckTemp.Visible = true;
                        this.pnlCardCollections.Controls.Add(ckTemp);

                        #endregion

                        i++;
                    }
                }
                else
                {
                    bool isHaveUnValid = false;

                    #region 如果该list里边存在作废数据 则题目处予以标识

                    foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in alTemp)
                    {
                        if (info.ValidState != FS.HISFC.Models.Base.EnumValidState.Valid)
                        {
                            isHaveUnValid = true;
                            break;
                        }
                    }

                    #endregion

                    #region 标签页赋值、打印

                    //ucCompoundLabelC ucclc = new ucCompoundLabelC();     
                    ucclc = new ucCompoundLabel();

                    this.pnlCardCollections.Controls.Add(ucclc);
                    ucclc.Left = i * 5 + i * 235;
                    ucclc.Top = j * 20 + j * 307 + 20;
                    ucclc.Clear();
                    ucclc.LabelTotNum = Convert.ToDecimal(alGroupApplyOut.Count);
                    ucclc.ICount = i + j * 3 + 1;
                    ucclc.Tag = alTemp;           //保存本标签的数据
                    ucclc.AddComboNonePrint(alTemp);


                    CheckBox ckTemp = new CheckBox();
                    ckTemp.Left = ucclc.Left;
                    ckTemp.Top = ucclc.Top - 20;
                    ckTemp.Tag = Convert.ToString(i + j * 3);
                    ckTemp.Width = 235;

                    if (isHaveUnValid == true)      //包含作废数据
                    {
                        //ckTemp.ForeColor = System.Drawing.Color.Red;
                        ckTemp.Text = "点击此处选择当前标签[已作废]                      ";
                    }
                    else
                    {
                        //ckTemp.ForeColor = System.Drawing.Color.Black;
                        ckTemp.Text = "点击此处选择当前标签                              ";
                    }


                    FS.HISFC.Models.Pharmacy.ApplyOut tempApplyState = alTemp[0] as FS.HISFC.Models.Pharmacy.ApplyOut;
                    if (tempApplyState.PrintState == "0" || string.IsNullOrEmpty(tempApplyState.PrintState) == true)
                    {
                        ckTemp.ForeColor = System.Drawing.Color.Black;
                        //ckTemp.Text = "点击此处选择当前标签                              ";//ckTemp.Tag.ToString();
                    }
                    else if (tempApplyState.PrintState == "1")
                    {
                        //ckTemp.Text = "点击此处选择当前标签[已打印]                      ";//ckTemp.Tag.ToString();
                        ckTemp.ForeColor = System.Drawing.Color.Red;
                    }

                    ckTemp.Visible = true;
                    this.pnlCardCollections.Controls.Add(ckTemp);

                    #endregion

                    i++;
                }

            }

            return 1;
        }



        /// <summary>
        /// 通过选择卡片同步选择明细列表相应批次号数据
        /// </summary>
        public void SynCheckedStates()
        {
            //配液中心兼容卡片界面样式 by Sunjh 2010-11-16 {5998231C-4049-4b99-89B1-62BF1A639F4B}
            if (this.neuTabControl1.SelectedTab == this.tpCardStyle)
            {
                for (int i = 0; i < this.pnlCardCollections.Controls.Count; i++)
                {
                    if (i % 2 != 0)
                    {
                        CheckBox ckk = this.pnlCardCollections.Controls[i] as CheckBox;
                        if (ckk.Checked)
                        {
                            //ucCompoundLabelC ucc = this.pnlCardCollections.Controls[Convert.ToInt32(ckk.Tag.ToString()) * 2] as ucCompoundLabelC;
                            ucCompoundLabel ucc = this.pnlCardCollections.Controls[Convert.ToInt32(ckk.Tag.ToString()) * 2] as ucCompoundLabel;
                            if (ucc.GroupNO != "")
                            {
                                for (int j = 0; j < this.fpApply_Sheet1.Rows.Count; j++)
                                {
                                    if (this.fpApply_Sheet1.Cells[j, (int)ColumnSet.ColCompoundGroup].Text == ucc.GroupNO)
                                    {
                                        this.fpApply_Sheet1.Cells[j, (int)ColumnSet.ColSelect].Value = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 打印明细与汇总摆药单
        /// </summary>
        /// <param name="alApplyout"></param>
        public void PrintDrugBills(ArrayList alApplyout)
        {
            Function.PrintCompound(alApplyout, true, false);
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

                if (this.currentWinFun == WinFun.Print)     //检索打印
                {
                    this.rbPrinting.Checked = true;
                    this.rbCancel.Visible = false;

                }
                else if (this.currentWinFun == WinFun.Save) //确认操作
                {
                    this.rbPrinted.Checked = true;
                    this.rbCancel.Visible = true;
                    this.rbAll.Visible = false;
                    this.rbPrinting.Visible = false;
                }
            }
        }
        /// <summary>
        /// TAB页切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.neuTabControl1.SelectedTab == this.tpCardStyle)
            {
                //this.chkPhamrcy.Visible = false;
            }
            else
            {
                //配液中心兼容卡片界面样式by Sunjh 2010-11-16 {5998231C-4049-4b99-89B1-62BF1A639F4B}
                if (this.IsShowCardStyle)
                {
                    for (int i = 0; i < this.pnlCardCollections.Controls.Count; i++)
                    {
                        if (i % 2 != 0)
                        {
                            CheckBox ckk = this.pnlCardCollections.Controls[i] as CheckBox;
                            if (ckk.Checked)
                            {
                                //ucCompoundLabelC ucc = this.pnlCardCollections.Controls[Convert.ToInt32(ckk.Tag.ToString()) * 2] as ucCompoundLabelC;
                                ucCompoundLabel ucc = this.pnlCardCollections.Controls[Convert.ToInt32(ckk.Tag.ToString()) * 2] as ucCompoundLabel;
                                if (ucc.GroupNO != "")
                                {
                                    for (int j = 0; j < this.fpApply_Sheet1.Rows.Count; j++)
                                    {
                                        if (this.fpApply_Sheet1.Cells[j, (int)ColumnSet.ColCompoundGroup].Text == ucc.GroupNO)
                                        {
                                            this.fpApply_Sheet1.Cells[j, (int)ColumnSet.ColSelect].Value = true;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                //ucCompoundLabelC ucc = this.pnlCardCollections.Controls[Convert.ToInt32(ckk.Tag.ToString()) * 2] as ucCompoundLabelC;
                                ucCompoundLabel ucc = this.pnlCardCollections.Controls[Convert.ToInt32(ckk.Tag.ToString()) * 2] as ucCompoundLabel;
                                if (ucc.GroupNO != "")
                                {
                                    for (int j = 0; j < this.fpApply_Sheet1.Rows.Count; j++)
                                    {
                                        if (this.fpApply_Sheet1.Cells[j, (int)ColumnSet.ColCompoundGroup].Text == ucc.GroupNO)
                                        {
                                            this.fpApply_Sheet1.Cells[j, (int)ColumnSet.ColSelect].Value = false;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                this.chkPhamrcy.Visible = true;
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
                for (int z = 0; z < this.fpApply_Sheet1.Rows.Count; z++)
                {
                    for (int i = 0; i < this.pnlCardCollections.Controls.Count; i++)
                    {
                        if (i % 2 != 0)
                        {
                            CheckBox ckk = this.pnlCardCollections.Controls[i] as CheckBox;

                            ucCompoundLabel ucc = this.pnlCardCollections.Controls[Convert.ToInt32(ckk.Tag.ToString()) * 2] as ucCompoundLabel;
                            if (ucc.GroupNO != "")
                            {

                                if (this.fpApply_Sheet1.Cells[z, (int)ColumnSet.ColCompoundGroup].Text == ucc.GroupNO)
                                {
                                    ckk.Checked = (bool)this.fpApply_Sheet1.Cells[z, 1].Value;
                                }

                            }

                        }
                    }
                }
            }
        }
        /// <summary>
        /// 更改批次号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpApply_EditChange(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            FS.HISFC.Models.Pharmacy.ApplyOut apply = this.fpApply_Sheet1.ActiveRow.Tag as FS.HISFC.Models.Pharmacy.ApplyOut;
            if (apply == null)
            {
                return;
            }
            string combo = apply.CombNO;
            DateTime dateUse = apply.UseTime;
            string compoundGroup = this.fpApply_Sheet1.Cells[this.fpApply_Sheet1.ActiveRowIndex, (int)ColumnSet.ColCompoundGroup].Text;
            if (e.Column == (int)ColumnSet.ColCompoundGroup)
            {
                for (int i = 0; i < this.fpApply_Sheet1.RowCount; i++)
                {
                    FS.HISFC.Models.Pharmacy.ApplyOut info = this.fpApply_Sheet1.Rows[i].Tag as FS.HISFC.Models.Pharmacy.ApplyOut;
                    if (info == null)
                    {
                        return;
                    }
                    if (info.UseTime == dateUse && info.CombNO == combo)
                    {
                        this.fpApply_Sheet1.Cells[i, (int)ColumnSet.ColCompoundGroup].Text = compoundGroup;
                    }
                }
            }
        }
        /// <summary>
        /// 左侧树选择事件
        /// </summary>
        /// <param name="alData"></param>
        private void tvCompound_SelectDataEvent(ArrayList alData)
        {
            if (this.tvCompound.SelectedNode.Tag == "allDept")
            {
                return;
            }
            if (this.tvCompound.SelectedNode.Parent == null)
            {
                this.lbInfo.Text = string.Format("当前科室:{0} 患者总计:{1} ", this.tvCompound.SelectedNode.Text, this.tvCompound.SelectedNode.Nodes.Count);
            }
            else
            {
                this.lbInfo.Text = string.Format("当前患者:{0}", this.tvCompound.SelectedNode.Text);
            }

            this.AddDataToFp(alData);
        }
        /// <summary>
        /// 批次号选择时候触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbOrderGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.tvCompound.GroupCode = this.GroupCode;

            this.ShowList();
        }
        /// <summary>
        /// 药品列表选择框触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkPhamrcy_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkPhamrcy.Checked)
            {
                this.cmbParmacy.Visible = true;
                this.cmbOrderGroup.SelectedIndex = 0;
                this.cmbOrderGroup.Enabled = false;
            }
            else
            {
                this.cmbParmacy.Visible = false;
                this.cmbOrderGroup.Enabled = true;
                this.cmbParmacy.Text = "";
                this.ShowList();
            }
        }
        /// <summary>
        /// 药品列表选择触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbParmacy_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool val = false;
            ArrayList applyList = new ArrayList();
            this.ShowList();
            this.lbInfo.Text = "所有病区中包含该药品信息...";
            if (this.alThisParList != null && this.alThisParList.Count > 0)
            {
                ArrayList alGroupApplyOut = new ArrayList();
                ArrayList alCombo = new ArrayList();
                string privCombo = "-1";
                foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in this.alThisParList)
                {
                    if (privCombo == "-1" || (privCombo == info.CompoundGroup && info.CompoundGroup != ""))
                    {
                        alCombo.Add(info.Clone());
                        privCombo = info.CompoundGroup;
                        continue;
                    }
                    else
                    {
                        alGroupApplyOut.Add(alCombo);
                        privCombo = info.CompoundGroup;
                        alCombo = new ArrayList();
                        alCombo.Add(info.Clone());
                    }
                }
                if (alCombo.Count > 0)
                {
                    alGroupApplyOut.Add(alCombo);
                }
                foreach (ArrayList list in alGroupApplyOut)
                {
                    val = false;
                    //如果该组中包含检索药品val为true
                    foreach (FS.HISFC.Models.Pharmacy.ApplyOut f in list)
                    {
                        if (f.Item.ID == this.DrugCode)
                        {
                            val = true;
                            break;
                        }
                    }
                    //如果改组液体中不含检索药品则不打印
                    if (!val)
                    {
                        continue;
                    }
                    foreach (FS.HISFC.Models.Pharmacy.ApplyOut f in list)
                    {
                        applyList.Add(f);
                    }
                }
                this.AddDataToFp(applyList);
            }
        }
        /// <summary>
        /// 设置打印快捷键
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.F12)
            {
                ArrayList alCheck = this.GetCheckData();

                this.PrintDrugBills(alCheck);
            }
            return base.ProcessDialogKey(keyData);
        }
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

        /// <summary>
        /// 当前窗口功能设置
        /// </summary>
        public enum WinFun
        {
            Print,
            Save
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

        #endregion

        #region ICompoundGroup 成员
        /* 设计思路：首先将医嘱分解时候传递过来的医嘱信息数组进行处理，只检索出发送到配液科室（在常数中维护）*/
        /*的医嘱信息存储在一个新数组，然后按照批次算法先将一些特殊批次：A、B、C、D、E、4、5 计算出来，*/
        /*之后循环一下将同组的其他液体都赋值成同一批次，到此特殊批次都已经赋值；把剩余的医嘱信息存储成一个 */
        /*数组，然后按照分批次的算法计算批次：将每组液体药理作用最大那个药品取出来形成一个数组，将数组 */
        /*按照频次排序，然后取出每组的最大液体量进行累加赋批次 ，之后循环一下将同组的其他液体都赋值同一批次*/
        public string GetCompoundGroup(FS.HISFC.Models.Order.ExecOrder order)
        {
            string compoundGroup = "";
            return compoundGroup;
        }

        public int GetCompoundGroup(ArrayList List, ref string error)
        {
            //组合排序
            ComboSort comboSort = new ComboSort();

            //处理后的医嘱信息数组
            ArrayList execOrderList = new ArrayList();
            //获取配液科室--begin
            ArrayList compoundDeptList = this.managerIntegrate.GetConstantList("CompoundDept");
            if (compoundDeptList == null)
            {
                error = "获取配液科室出错！";
                return -1;
            }
            if (compoundDeptList.Count == 0)
            {
                error = "配液中心还没有维护配液科室，请通知配液中心在常数维护中维护配液科室！";
                return -1;
            }
            //--end

            //获取用法---begin---
            ArrayList usageList = this.managerIntegrate.GetConstantList("USAGE");
            if (usageList == null)
            {
                error = "获取用法出错！";
                return -1;
            }
            //----end-----

            //只加载取药科室为配液科室的医嘱信息形成数组--begin
            foreach (FS.FrameWork.Models.NeuObject obj in compoundDeptList)
            {
                foreach (FS.HISFC.Models.Order.ExecOrder order in List)
                {
                    if (obj.ID == order.Order.StockDept.ID)
                    {
                        execOrderList.Add(order);
                    }
                }
            }
            if (execOrderList.Count == 0)
            {
                return 0;
            }
            //--end

            //获取频次--begin
            FS.HISFC.BizProcess.Integrate.Manager manger = new FS.HISFC.BizProcess.Integrate.Manager();
            alFrequency = new ArrayList();
            alFrequency = manger.QuereyFrequencyList();
            if (alFrequency == null)
            {
                error = "获取频次信息出错！";
                return -1;
            }
            //--end


            //按组合排序
            execOrderList.Sort(comboSort);


            //---------------------begin----------------------------------------
            #region 特殊批次算法

            //----begin-变量---
            string combo1 = "";
            string combo2 = "";
            string compoundGroupNo1 = "";
            string compoundGroupNo2 = "";
            string sqNo = "";   //取序列设置批次流水号
            //----end---------

            foreach (FS.HISFC.Models.Order.ExecOrder order in execOrderList)
            {
                #region 前期赋值
                //药理作用排序号赋值到User01上，用于后面取药理作用最大的药品
                for (int i = 0; i < this.alPharmcyFunction.Count; i++)
                {
                    FS.HISFC.Models.Pharmacy.PhaFunction phFun = this.alPharmcyFunction[i] as FS.HISFC.Models.Pharmacy.PhaFunction;
                    if (order.Order.Item.ID == phFun.ID)
                    {
                        order.Order.Item.User01 = phFun.SortID.ToString();
                    }
                }
                //频次排序号赋值 用于频次排序
                foreach (FS.HISFC.Models.Order.Frequency fr in this.alFrequency)
                {
                    if (order.Order.Frequency.ID == fr.ID)
                    {
                        order.Order.Frequency.SortID = fr.SortID;
                    }
                }
                #endregion



                combo2 = order.Order.Combo.ID + order.DateUse.ToString();
                //同组的直接赋值，不在循环              
                if (combo1 == combo2)
                {
                    order.Order.Item.User02 = compoundGroupNo1;
                    continue;
                }

                //判断同组液体中的没有单位为ml的，放到E批次-----begin----
                string str = "";
                int drugCount = 0; //一组中的药品数量，如果为1就一个药品则放到B批次
                foreach (FS.HISFC.Models.Order.ExecOrder o in execOrderList)
                {
                    if (order.Order.Combo.ID + order.DateUse.ToString() == o.Order.Combo.ID + o.DateUse.ToString())
                    {
                        str += o.Order.DoseUnit.ToString() + "|";
                        drugCount++;
                    }
                }
                //如果同组中不包含单位为ml的
                if (!str.Contains("ml|"))
                {
                    sqNo = itemManager.GetNewCompoundGroup();//获取新的批次流水号
                    order.Order.Item.User02 = "E_" + sqNo;
                    compoundGroupNo2 = order.Order.Item.User02;
                    combo1 = combo2;
                    compoundGroupNo1 = compoundGroupNo2;
                    continue;
                }
                //--------end-----


                //------临时医嘱,批次A -- begin--
                if (order.Order.OrderType.ID.ToString() == "LZ")
                {
                    sqNo = itemManager.GetNewCompoundGroup();
                    order.Order.Item.User02 = "A_" + sqNo;
                    compoundGroupNo2 = order.Order.Item.User02;
                    combo1 = combo2;
                    compoundGroupNo1 = compoundGroupNo2;
                    continue;
                }
                //----end----------


                //------------将打包药品分批次：B-----begin---
                for (int i = 0; i < this.alPharmcyFunction.Count; i++)
                {
                    FS.HISFC.Models.Pharmacy.PhaFunction phFun = this.alPharmcyFunction[i] as FS.HISFC.Models.Pharmacy.PhaFunction;
                    if (order.Order.Item.ID == phFun.ID)
                    {
                        //打包药品或者就一个药品
                        if (phFun.Memo == "1" || drugCount == 1)
                        {
                            sqNo = itemManager.GetNewCompoundGroup();
                            order.Order.Item.User02 = "B_" + sqNo;
                            compoundGroupNo2 = order.Order.Item.User02;
                        }
                        break;
                    }
                }
                //如果已经赋值批次则进行下一条医嘱
                if (order.Order.Item.User02 != "")
                {
                    combo1 = combo2;
                    compoundGroupNo1 = compoundGroupNo2;
                    continue;
                }
                //--------end----


                //----特殊用法,静脉营养:C,另几种：D-----begin---- 
                for (int i = 0; i < usageList.Count; i++)
                {
                    FS.FrameWork.Models.NeuObject useObj = usageList[i] as FS.FrameWork.Models.NeuObject;
                    foreach (FS.HISFC.Models.Order.ExecOrder o in execOrderList)
                    {
                        if ((order.Order.Combo.ID + order.DateUse.ToString() == o.Order.Combo.ID + o.DateUse.ToString()) &&
                            o.Order.Usage.ID == useObj.ID)
                        {
                            switch (useObj.Memo)
                            {
                                case "静脉营养":
                                    sqNo = itemManager.GetNewCompoundGroup();
                                    order.Order.Item.User02 = "C_" + sqNo;
                                    compoundGroupNo2 = order.Order.Item.User02;
                                    break;
                                case "莫非氏管":
                                    sqNo = itemManager.GetNewCompoundGroup();
                                    order.Order.Item.User02 = "D_" + sqNo;
                                    compoundGroupNo2 = order.Order.Item.User02;
                                    break;
                                case "静脉慢推":
                                    sqNo = itemManager.GetNewCompoundGroup();
                                    order.Order.Item.User02 = "D_" + sqNo;
                                    compoundGroupNo2 = order.Order.Item.User02;
                                    break;
                                case "静脉注射":
                                    sqNo = itemManager.GetNewCompoundGroup();
                                    order.Order.Item.User02 = "D_" + sqNo;
                                    compoundGroupNo2 = order.Order.Item.User02;
                                    break;
                            }
                            if (order.Order.Item.User02 != "")
                            {
                                break;
                            }
                        }
                    }
                    if (order.Order.Item.User02 != "")
                    {
                        break;
                    }
                }
                //如果已经赋值批次则进行下一条医嘱
                if (order.Order.Item.User02 != "")
                {
                    combo1 = combo2;
                    compoundGroupNo1 = compoundGroupNo2;
                    continue;
                }
                //-------end-----------


                //执行时间点在12:00和16:00之间的批次为：4,大于16:00的批次为：5  ------begin------

                //执行日期
                string dateStr = order.DateUse.Year.ToString() + "-" + order.DateUse.Month.ToString()
                    + "-" + order.DateUse.Day.ToString();
                foreach (FS.HISFC.Models.Order.Frequency fr in this.alFrequency)
                {
                    if (order.Order.Frequency.ID == fr.ID)
                    {
                        if (DateTime.Compare(order.DateUse, FS.FrameWork.Function.NConvert.ToDateTime(dateStr + " 12:00")) >= 0
                            && DateTime.Compare(order.DateUse, FS.FrameWork.Function.NConvert.ToDateTime(dateStr + " 16:00")) <= 0)
                        {
                            sqNo = itemManager.GetNewCompoundGroup();
                            order.Order.Item.User02 = "4_" + sqNo;
                            compoundGroupNo2 = order.Order.Item.User02;
                            break;
                        }
                        if (DateTime.Compare(order.DateUse, FS.FrameWork.Function.NConvert.ToDateTime(dateStr + " 16:00")) > 0)
                        {
                            sqNo = itemManager.GetNewCompoundGroup();
                            order.Order.Item.User02 = "5_" + sqNo;
                            compoundGroupNo2 = order.Order.Item.User02;
                            break;
                        }
                    }
                }
                //如果已经赋值批次则进行下一条医嘱
                if (order.Order.Item.User02 != "")
                {
                    combo1 = combo2;
                    compoundGroupNo1 = compoundGroupNo2;
                    continue;
                }
                //------end-------

                //如果没有批次赋值为空则赋值一下组合号，并且将compoundGroupNo1清空
                if (order.Order.Item.User02 == "")
                {
                    combo1 = combo2;
                    compoundGroupNo1 = "";
                    continue;
                }

            }
            #endregion
            //---------------------end------------------------------------------


            //到此特殊批次已经赋值完毕，现在将剩余批次为空的提出组成一个新数组进行1,2,3批次的
            //赋值，按照最大液体量累加算法


            //-------------------------begin-------------------------------------   

            #region 1,2,3批次算法

            //----取出剩余的医嘱信息组成新数组---begin----
            ArrayList otherList = new ArrayList();//存储剩余医嘱信息
            foreach (FS.HISFC.Models.Order.ExecOrder order in execOrderList)
            {
                if (order.Order.Item.User02 == "")
                {
                    otherList.Add(order);
                }
            }
            if (otherList.Count == 0)
            {
                return 0;
            }
            //------end-------

            otherList.Sort(comboSort);//组合排序

            //-----begin----
            #region 取每组中的药理作用最大那个写入一个新数组
            ArrayList alCouts = new ArrayList();
            string bill1 = "";
            string bill2 = "";
            int sortId = 0;
            ArrayList alList = new ArrayList();
            int count = 0;
            foreach (FS.HISFC.Models.Order.ExecOrder o in otherList)
            {
                bill2 = o.Order.Combo.ID + o.DateUse.ToString();
                if (bill2 != bill1)
                {
                    sortId = FS.FrameWork.Function.NConvert.ToInt32(o.Order.Item.User01);
                    if (sortId == 0 || sortId.ToString() == "")
                    {
                        sortId = 99;
                    }
                    alList = new ArrayList();
                    alList.Add(o);
                    count++;
                    alCouts.Add(alList);
                    bill1 = bill2;
                }
                else
                {
                    if (sortId > FS.FrameWork.Function.NConvert.ToInt32(o.Order.Item.User01))
                    {
                        sortId = FS.FrameWork.Function.NConvert.ToInt32(o.Order.Item.User01);
                        alList = new ArrayList();
                        alList.Add(o);
                        alCouts[count - 1] = alList;
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            #endregion
            //-------end-----

            //------begin------
            #region 按照最大液体量累加分批次
            string combo = "";
            decimal doseOnce = 0;
            string compGroup = "";
            bool val1 = true;
            bool val2 = true;
            bool val3 = true;
            for (int i = 0; i < alCouts.Count; i++)
            {
                ArrayList al = alCouts[i] as ArrayList;
                decimal maxDoseOnce = 0;
                FS.HISFC.Models.Order.ExecOrder execOrder = al[0] as FS.HISFC.Models.Order.ExecOrder;
                if (execOrder.Order.DoseUnit == "ml") //如果单位为毫升则赋值最大液体量不是则为0
                {
                    maxDoseOnce = execOrder.Order.DoseOnce;
                }
                else
                {
                    maxDoseOnce = 0;
                }
                combo = execOrder.Order.Combo.ID + execOrder.DateUse.ToString();
                //取每组液中的最大液体量---begin---
                foreach (FS.HISFC.Models.Order.ExecOrder order in otherList)
                {
                    if (order.Order.Combo.ID + order.DateUse.ToString() == combo)
                    {
                        if (order.Order.DoseOnce > maxDoseOnce && order.Order.DoseUnit == "ml")
                        {
                            maxDoseOnce = order.Order.DoseOnce;
                        }
                    }
                }
                //----end-------
                //第一批次累计，若超过200则下次循环不在走这部分
                if (val1)
                {
                    if ((maxDoseOnce + doseOnce) >= 200)
                    {
                        sqNo = itemManager.GetNewCompoundGroup();
                        execOrder.Order.Item.User02 = "1_" + sqNo;
                        doseOnce = 0;
                        val1 = false;
                        continue;
                    }
                    else
                    {
                        sqNo = itemManager.GetNewCompoundGroup();
                        execOrder.Order.Item.User02 = "1_" + sqNo;
                        doseOnce += maxDoseOnce;
                        val1 = true;
                        continue;
                    }
                }
                //第二批次累计，若超过250则下次循环不在走这部分
                if (val2)
                {
                    if ((maxDoseOnce + doseOnce) >= 250)
                    {
                        sqNo = itemManager.GetNewCompoundGroup();
                        execOrder.Order.Item.User02 = "2_" + sqNo;
                        doseOnce = 0;
                        val2 = false;
                        continue;
                    }
                    else
                    {
                        sqNo = itemManager.GetNewCompoundGroup();
                        execOrder.Order.Item.User02 = "2_" + sqNo;
                        doseOnce += maxDoseOnce;
                        val2 = true;
                        continue;
                    }
                }
                //第三批次累计，若超过750则下次循环不在走这部分
                if (val3)
                {
                    if ((maxDoseOnce + doseOnce) >= 750)
                    {
                        sqNo = itemManager.GetNewCompoundGroup();
                        execOrder.Order.Item.User02 = "3_" + sqNo;
                        doseOnce = 0;
                        val3 = false;
                        continue;
                    }
                    else
                    {
                        sqNo = itemManager.GetNewCompoundGroup();
                        execOrder.Order.Item.User02 = "3_" + sqNo;
                        doseOnce += maxDoseOnce;
                        val3 = true;
                        continue;
                    }
                }
                //如果还有药则都放进第四批次
                if (!val1 && !val2 && !val3)
                {
                    sqNo = itemManager.GetNewCompoundGroup();
                    execOrder.Order.Item.User02 = "4_" + sqNo;
                    continue;
                }
            }
            #endregion
            //------end--------

            //-----同组的药品批次号赋值---begin-----
            for (int i = 0; i < alCouts.Count; i++)
            {
                ArrayList al = alCouts[i] as ArrayList;
                FS.HISFC.Models.Order.ExecOrder execOrder = al[0] as FS.HISFC.Models.Order.ExecOrder;
                foreach (FS.HISFC.Models.Order.ExecOrder o in otherList)
                {
                    if (o.Order.Combo.ID + o.DateUse.ToString() == execOrder.Order.Combo.ID + execOrder.DateUse.ToString())
                    {
                        o.Order.Item.User02 = execOrder.Order.Item.User02;
                    }
                }
            }
            //------end--------

            #endregion

            //--------------------------end---------------------------------------

            return 1;
        }

        #endregion






        #region ICompoundGroup 成员

        public int GetCompoundGroup(ArrayList List)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
