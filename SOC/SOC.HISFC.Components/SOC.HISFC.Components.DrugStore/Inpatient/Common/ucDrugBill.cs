using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.HISFC.Components.DrugStore.Inpatient.Common
{
    /// <summary>
    /// [功能描述: 住院药房单据]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2010-12]<br></br>
    /// 说明：
    /// 1、去掉药柜管理功能
    /// </summary>
    public partial class ucDrugBill : ucDrugBase, FS.FrameWork.WinForms.Classes.IPreArrange
    {
        public ucDrugBill()
        {
            InitializeComponent();
        }

        private bool isQueryOnLoad = true;

        /// <summary>
        /// 界面打开时是否查询列表
        /// </summary>
        [Description("界面打开时是否查询列表"), Category("设置"), Browsable(true)]
        public bool IsQueryOnLoad
        {
            get { return isQueryOnLoad; }
            set { isQueryOnLoad = value; }
        }

        private bool isExpand = true;

        /// <summary>
        /// 列表查询后是否展开树节点
        /// </summary>
        [Description("列表查询后是否展开树节点"), Category("设置"), Browsable(true)]
        public bool IsExpand
        {
            get { return isExpand; }
            set { isExpand = value; }
        }

        private bool billNOWithDate = false;
        /// <summary>
        /// 查询时是否在单号前面增加日期，日期格式yyyyMMdd
        /// </summary>
        [Description("按单号查询时是否加上日期前缀"), Category("设置"), Browsable(true)]
        public bool BillNOWithDate
        {
            get { return this.billNOWithDate; }
            set { this.billNOWithDate = value; }
        }

        //{0FB7D47B-BF3A-4b7d-BCBE-C6473D376B7E}
        private bool isAlwaysPrintAll = false;
        /// <summary>
        /// 打印明细时是否总是打印全部条目
        /// </summary>
        [Description("打印明细时是否总是打印全部条目"), Category("设置"), Browsable(true)]
        public bool IsAlwaysPrintAll
        {
            get { return this.isAlwaysPrintAll; }
            set { this.isAlwaysPrintAll = value; }
        }

        FS.SOC.HISFC.BizLogic.Pharmacy.DrugStore drugStoreMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.DrugStore();
        //FS.SOC.HISFC.BizLogic.Pharmacy.Item drugStoreMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.Item();

        FS.HISFC.Models.Pharmacy.DrugBillClass curDrugBillClass;

        /// <summary>
        /// 汇总打印接口
        /// </summary>
        private FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IInPatientDrugTotalPrint inPatientDrugTotalImp = null;
        
        #region IPreArrange 成员

        public int PreArrange()
        {
            #region 权限科室
            FS.HISFC.BizLogic.Manager.UserPowerDetailManager userPowerDetailManager = new FS.HISFC.BizLogic.Manager.UserPowerDetailManager();
            if (this.IsCheckPrivePower)
            {
                if (string.IsNullOrEmpty(PrivePowerString))
                {
                    PrivePowerString = "0320+Z1";
                }
                if (PrivePowerString.Split('+').Length < 2)
                {
                    PrivePowerString = PrivePowerString + "+Z1";
                }
                string[] prives = PrivePowerString.Split('+');
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

                if (this.IsOtherDeptDrug)
                {
                    int param = Function.ChoosePriveDept(prives[0], prives[1], ref this.priveDept);
                    if (param == 0 || param == -1)
                    {
                        return -1;
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

            this.nlbInfo.Text = "您选择的科室是：" + this.priveDept.Name + "，特别提醒：根据摆药单号查找数据时可以不选择时间";

            #endregion

            return 1;
        }

        #endregion

        #region 方法

        /// <summary>
        /// 初始化接口
        /// </summary>
        private void InitInterface()
        {
            if (inPatientDrugTotalImp == null)
            {
                inPatientDrugTotalImp = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(FS.SOC.HISFC.Components.DrugStore.Inpatient.Common.ucDrugBill), typeof(FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IInPatientDrugTotalPrint)) as FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IInPatientDrugTotalPrint;
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        private int Query()
        {
            this.ucDrugDetail1.Clear();
            this.tvMessageBaseTree1.Clear();
            
            if (string.IsNullOrEmpty(this.ntxtDrugedBillNO.Text))
            {
                this.QueryByDate();
            }
            else
            {
                this.QueryByDrugBillNO();
            }

            return 1;
        }

        /// <summary>
        /// 根据摆药日期查找摆药单列表
        /// </summary>
        /// <returns></returns>
        private int QueryByDate()
        {
            ArrayList alDrugBillClass = this.drugStoreMgr.QueryDrugBillByDay(this.PriveDept.ID, this.ndtpDrugedDate.Value.Date);

            if (alDrugBillClass == null)
            {
                this.ShowMessage("查询科室配药单发生错误：" + this.drugStoreMgr.Err, MessageBoxIcon.Error);
                return -1;
            }
            this.tvMessageBaseTree1.ShowDrugBillClass(alDrugBillClass, this.isExpand);
            return 1;
        }

        /// <summary>
        /// 根据摆药单号查找摆药信息
        /// </summary>
        /// <returns></returns>
        private int QueryByDrugBillNO()
        {
            ArrayList alDrugBillClass = new ArrayList();
            ArrayList alDetail = new ArrayList();
            
            string billNO = this.ntxtDrugedBillNO.Text;
            if (this.billNOWithDate)
            {
                string deptLogo = drugStoreMgr.GetDeptDrugBillNOLogo(this.PriveDept.ID);
                billNO = deptLogo + this.ndtpDrugedDate.Value.Date.ToString("yyyyMMdd") + "-" + this.ntxtDrugedBillNO.Text;
            }
                
            alDetail = this.drugStoreMgr.QueryApplyOutListByBill(billNO,"'1','2'");
            
            
            if (alDetail == null)
            {
                this.ShowMessage("查询科室配药单发生错误：" + this.drugStoreMgr.Err, MessageBoxIcon.Error);
                return -1;
            }
            if (alDetail.Count == 0)
            {
                this.ShowMessage("您输入单号不正确！");
                return -1;
            }
            FS.HISFC.Models.Pharmacy.ApplyOut applyOut = alDetail[0] as FS.HISFC.Models.Pharmacy.ApplyOut;

            FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass = this.drugStoreMgr.GetDrugBillClass(applyOut.BillClassNO);
            drugBillClass.DrugBillNO = applyOut.DrugNO;
            drugBillClass.ApplyDept.ID = applyOut.ApplyDept.ID;
            drugBillClass.ApplyDept.Name = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(applyOut.ApplyDept.ID);


            alDrugBillClass.Add(drugBillClass);

            this.tvMessageBaseTree1.ShowDrugBillClass(alDrugBillClass, this.isExpand);

            this.ucDrugDetail1.ShowDetail(alDetail, this.IsAutoSelected, this.EnumQtyShowType, false, Function.EnumInpatintDrugApplyType.临时发送, false);

            this.ShowDrugBill(alDetail, drugBillClass);

            //避免重复查询数据，删除事件后选择节点
            this.tvMessageBaseTree1.AfterSelect -= new TreeViewEventHandler(tvMessageBaseTree1_AfterSelect);
            this.tvMessageBaseTree1.SelectedNode = this.tvMessageBaseTree1.Nodes[0].Nodes[0];

            //需要再添加事件
            this.tvMessageBaseTree1.AfterSelect += new TreeViewEventHandler(tvMessageBaseTree1_AfterSelect);

            return 1;
        }

        /// <summary>
        /// 显示摆药单明细数据
        /// </summary>
        /// <param name="drugBillClass">摆药单分类</param>
        /// <returns></returns>
        private int ShowDrugBillClassDetail(FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass)
        {
            this.ucDrugDetail1.Clear();

            //底层的sql是in，支持多单据数据一起查询
            ArrayList al = new ArrayList();

            al = this.drugStoreMgr.QueryApplyOutListByBill("'" + drugBillClass.DrugBillNO + "'", "'1','2'");

            this.ucDrugDetail1.ShowDetail(al, this.IsAutoSelected, this.EnumQtyShowType, false, Function.EnumInpatintDrugApplyType.临时发送, false);

            this.ShowDrugBill(al, drugBillClass);

            return 1;
        }

        /// <summary>
        /// 显示摆药单信息
        /// </summary>
        /// <param name="alData">applyout数组</param>
        /// <param name="drugBillClass">摆药单分类</param>
        private void ShowDrugBill(ArrayList alData, FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass)
        {
            //本地接口返回的控件可能不止一个，返回明细则显示明细摆药单，返回汇总则显示汇总摆药单，返回两个则两个都显示
            if (this.IInpatientDrug == null)
            {
                this.ShowMessage("没有实现单据打印接口：FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IInpatientDrug", MessageBoxIcon.Error);
                return;
            }
            List<FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IInpatientBill> listInpatientBill = this.IInpatientDrug.ShowDrugBill(alData, drugBillClass, this.StockDept);
            
            //明细显示
            this.ucDrugDetail1.ShowBill(listInpatientBill);
            this.tvMessageBaseTree1.Select();
            this.tvMessageBaseTree1.Focus();
        }

        /// <summary>
        /// 打印单据
        /// </summary>
        /// <param name="billType">单据类型</param>
        private void PrintBill(FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.InpatientBillType billType)
        {
            if (billType == FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.InpatientBillType.标签)
            {
                ArrayList alData = this.ucDrugDetail1.GetSelectData();
                this.IInpatientDrug.PrintDrugLabel(alData, curDrugBillClass, this.StockDept);
                return;
            }

            if (billType == FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.InpatientBillType.药袋)
            {
                ArrayList alData = this.ucDrugDetail1.GetSelectData();
                this.IInpatientDrug.PrintDrugBag(alData, curDrugBillClass, this.StockDept);
                return;
            }

            this.ucDrugDetail1.PrintBill(billType);
        }

        private void SelectAllDetailData(bool isSelectAll)
        {
            this.ucDrugDetail1.SelectAllData(isSelectAll);
        }

        #endregion

        #region 事件
        protected override int OnQuery(object sender, object neuObject)
        {
            return this.Query();
        }

        protected override void OnLoad(EventArgs e)
        {
            this.ucDrugDetail1.SetTabPageVisible(false, true, true, true, true, true);
            this.tvMessageBaseTree1.AfterSelect += new TreeViewEventHandler(tvMessageBaseTree1_AfterSelect);
            this.nbtDateOK.Click += new EventHandler(nbtDateOK_Click);
            this.nbtBillNOOK.Click += new EventHandler(nbtBillNOOK_Click);
            this.nbtBillNOOK.KeyUp += new KeyEventHandler(nbtBillNOOK_KeyUp);

            if (this.IsQueryOnLoad)
            {
                this.Query();
            }
            this.InitInterface();
            base.OnLoad(e);
        }

        void nbtBillNOOK_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.ucDrugDetail1.Clear();
                this.tvMessageBaseTree1.Clear();
                this.QueryByDrugBillNO();
            }
        }

        void nbtBillNOOK_Click(object sender, EventArgs e)
        {
            this.ucDrugDetail1.Clear();
            this.tvMessageBaseTree1.Clear();
            this.QueryByDrugBillNO();
        }

        void nbtDateOK_Click(object sender, EventArgs e)
        {
            this.ucDrugDetail1.Clear();
            this.tvMessageBaseTree1.Clear();
            this.QueryByDate();
        }

        void tvMessageBaseTree1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Level == 0)
            {
                return;
            }
            else if (e.Node.Level == 1)
            {
                if (e.Node.Tag == null)
                {
                    return;
                }
                if (e.Node.Tag is FS.HISFC.Models.Pharmacy.DrugBillClass)
                {
                    FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass = e.Node.Tag as FS.HISFC.Models.Pharmacy.DrugBillClass;
                    curDrugBillClass = drugBillClass;
                    this.ShowDrugBillClassDetail(drugBillClass);
                }
            }
        }

        #endregion

        #region 工具栏

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            ToolBarService.AddToolButton("打印处方", "手工打印或补打处方", FS.FrameWork.WinForms.Classes.EnumImageList.D打印, true, false, null);
            ToolBarService.AddToolButton("打印标签", "手工打印或补打标签", FS.FrameWork.WinForms.Classes.EnumImageList.D打印, true, false, null);
            ToolBarService.AddToolButton("打印汇总", "手工打印或补打汇总单", FS.FrameWork.WinForms.Classes.EnumImageList.D打印, true, false, null);
            ToolBarService.AddToolButton("打印明细", "手工打印或补打明细单", FS.FrameWork.WinForms.Classes.EnumImageList.D打印, true, false, null);
            ToolBarService.AddToolButton("打印药袋", "手工打印或补打药袋", FS.FrameWork.WinForms.Classes.EnumImageList.D打印, true, false, null);
            ToolBarService.AddToolButton("全    选", "选择所有数据", FS.FrameWork.WinForms.Classes.EnumImageList.D打印, true, false, null);
            ToolBarService.AddToolButton("全 不 选", "取消所有数据选择", FS.FrameWork.WinForms.Classes.EnumImageList.D打印, true, false, null);
            
            base.OnInit(sender, neuObject, param);
            return this.ToolBarService;
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            this.ucDrugDetail1.Print();
            return base.OnPrint(sender, neuObject);
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "打印处方")
            {
                this.PrintBill(FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.InpatientBillType.出院带药处方);
            }
            else if (e.ClickedItem.Text == "打印标签")
            {
                this.PrintBill(FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.InpatientBillType.标签);

            }
            else if (e.ClickedItem.Text == "打印汇总")
            {
                if (this.inPatientDrugTotalImp != null)
                {
                    //{3D84EF54-CE7A-40b1-8417-CF8FBED4A70D}
                    ArrayList alData = this.ucDrugDetail1.GetSelectData(true);
                    //口服药摆药单按照医嘱类型分单
                    if (curDrugBillClass.ID == "DL")
                    {
                        ArrayList alLZData = new ArrayList();
                        ArrayList alCZData = new ArrayList();
                        foreach (FS.HISFC.Models.Pharmacy.ApplyOut applyInfo in alData)
                        {
                            FS.HISFC.Models.Pharmacy.ApplyOut info = this.drugStoreMgr.GetApplyOutByID(applyInfo.ID);
                            if (info != null && !string.IsNullOrEmpty(info.ID))
                            {
                                if (SOC.HISFC.BizProcess.Cache.Pharmacy.isValueableItem(info.StockDept.ID, info.Item.ID) || info.OrderType.ID.ToString() != "CZ")
                                {
                                    alLZData.Add(info);
                                }
                                else
                                {
                                    alCZData.Add(info);
                                }
                            }
                        }
                        if (alLZData != null && alLZData.Count > 0)
                        {
                            if((DialogResult)(MessageBox.Show("是否补打口服临嘱摆药单？","提示",MessageBoxButtons.YesNo)) == DialogResult.Yes)
                            {
                            this.inPatientDrugTotalImp.PrintDrugBillTotal(alLZData, curDrugBillClass, this.StockDept);
                            }
                        }
                        if (alCZData != null && alCZData.Count > 0)
                        {
                            if ((DialogResult)(MessageBox.Show("是否补打口服长嘱摆药单？", "提示", MessageBoxButtons.YesNo)) == DialogResult.Yes)
                            {
                                this.inPatientDrugTotalImp.PrintDrugBillTotal(alCZData, curDrugBillClass, this.StockDept);
                            }
                        }
                    }
                    else
                    {
                        ArrayList alRealData = new ArrayList();
                        foreach (FS.HISFC.Models.Pharmacy.ApplyOut applyInfo in alData)
                        {
                            FS.HISFC.Models.Pharmacy.ApplyOut info = this.drugStoreMgr.GetApplyOutByID(applyInfo.ID);
                            if (info != null && !string.IsNullOrEmpty(info.ID))
                            {
                                alRealData.Add(info);
                            }
                        }

                        this.inPatientDrugTotalImp.PrintDrugBillTotal(alRealData, curDrugBillClass, this.StockDept);
                    }
                }
                return;
                //this.PrintBill(FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.InpatientBillType.汇总);

            }
            else if (e.ClickedItem.Text == "打印明细")
            {
                if (this.inPatientDrugTotalImp != null)
                {
                    //{3D84EF54-CE7A-40b1-8417-CF8FBED4A70D}
                    //{0FB7D47B-BF3A-4b7d-BCBE-C6473D376B7E}
                    ArrayList alData = this.ucDrugDetail1.GetSelectData(isAlwaysPrintAll);
                    ArrayList alRealData = new ArrayList();

                    ArrayList allNormal = new ArrayList();
                    ArrayList allAnes = new ArrayList();
                    ArrayList allHerbal = new ArrayList();
                    foreach (FS.HISFC.Models.Pharmacy.ApplyOut applyInfo in alData)
                    {
                        FS.HISFC.Models.Pharmacy.ApplyOut info = this.drugStoreMgr.GetApplyOutByID(applyInfo.ID);
                        if (info != null && !string.IsNullOrEmpty(info.ID))
                        {
                            info.BedNO = applyInfo.BedNO;
                            info.PatientName = applyInfo.PatientName;
                            alRealData.Add(info);
                        }
                        if (FS.SOC.HISFC.BizProcess.Cache.Common.GetDrugQualitySystem(applyInfo.Item.Quality.ID) == "S"
                           || FS.SOC.HISFC.BizProcess.Cache.Common.GetDrugQualitySystem(applyInfo.Item.Quality.ID) == "P")
                        {
                            allAnes.Add(applyInfo);
                        }
                        else if (FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(applyInfo.Item.ID).Type.ID == "C")
                        {
                            allHerbal.Add(applyInfo);
                        }
                        else
                        {
                            allNormal.Add(applyInfo);
                        }
                    }

                    FS.HISFC.Models.Pharmacy.DrugMessage drugMsg = new FS.HISFC.Models.Pharmacy.DrugMessage();
                    drugMsg.DrugBillClass = curDrugBillClass.Clone();
                    this.StockDept.User01 = "打印明细";
                    if (allNormal.Count > 0)// {7DBB85BF-547C-4230-8598-55A2A4AD83F4}
                    {
                        this.inPatientDrugTotalImp.PrintDrugBillDetail(allNormal, curDrugBillClass, this.StockDept);
                    }
                    if (allAnes.Count > 0)// {7DBB85BF-547C-4230-8598-55A2A4AD83F4}
                    {

                        GJLocal.HISFC.Components.OpGuide.InDrugStore.InPatient.IBORN.ucAnestheticDrugBill ucAnestheticDrugBill = new GJLocal.HISFC.Components.OpGuide.InDrugStore.InPatient.IBORN.ucAnestheticDrugBill();
                        ucAnestheticDrugBill.Init();
                        ucAnestheticDrugBill.PrintData(allAnes, curDrugBillClass, this.StockDept);
                    }

                    if (allHerbal.Count > 0)
                    {

                        GJLocal.HISFC.Components.OpGuide.InDrugStore.InPatient.IBORN.ucHerbalDrugBillIBORN ucHerbalDrugBill = new GJLocal.HISFC.Components.OpGuide.InDrugStore.InPatient.IBORN.ucHerbalDrugBillIBORN();
                        ucHerbalDrugBill.Init();
                        ucHerbalDrugBill.PrintData(allHerbal, curDrugBillClass, this.StockDept);
                        //ucHerbalDrugBill.PrintPage();
                    }
                }
                return;
                //this.PrintBill(FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.InpatientBillType.明细);

            }
            else if (e.ClickedItem.Text == "打印药袋")
            {
                if (this.inPatientDrugTotalImp != null)
                {
                    ArrayList alData = this.ucDrugDetail1.GetSelectData();
                    ArrayList alRealData = new ArrayList();
                    foreach (FS.HISFC.Models.Pharmacy.ApplyOut applyInfo in alData)
                    {
                        FS.HISFC.Models.Pharmacy.ApplyOut info = this.drugStoreMgr.GetApplyOutByID(applyInfo.ID);
                        if (info != null && !string.IsNullOrEmpty(info.ID))
                        {
                            info.BedNO = applyInfo.BedNO;
                            info.PatientName = applyInfo.PatientName;
                            alRealData.Add(info);
                        }
                    }

                    FS.HISFC.Models.Pharmacy.DrugMessage drugMsg = new FS.HISFC.Models.Pharmacy.DrugMessage();
                    drugMsg.DrugBillClass = curDrugBillClass.Clone();
                    this.StockDept.User01 = "打印药袋";//打印药袋接口没有实现，为了不改变原接口，暂用USER01
                    this.inPatientDrugTotalImp.PrintDrugBillDetail(alRealData, curDrugBillClass, this.StockDept);
                }
                return;
                //this.PrintBill(FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.InpatientBillType.明细);


            }
            else if (e.ClickedItem.Text == "全    选")
            {
                this.SelectAllDetailData(true);
            }
            else if (e.ClickedItem.Text == "全 不 选")
            {
                this.SelectAllDetailData(false);
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        #endregion

    }
}
