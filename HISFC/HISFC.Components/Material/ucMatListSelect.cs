using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Material
{
    public partial class ucMatListSelect : FS.HISFC.Components.Common.Controls.ucIMAListSelecct
    {
        public ucMatListSelect()
        {
            InitializeComponent();
        }

        #region 域成员变量

        /// <summary>
        /// 入库类型帮助类
        /// </summary>		
        private FS.FrameWork.Public.ObjectHelper inTypeHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 出库类型帮助类
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper outTypeHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 科室帮助类
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper deptHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 物资项目ID
        /// </summary>
        private string itemID = "all";

        /// <summary>
        /// 本身科室
        /// </summary>
        private string deptCode = "all";        

        #endregion

        #region 属性

        /// <summary>
        /// 入库类型帮助类
        /// </summary>
        public FS.FrameWork.Public.ObjectHelper InTypeHelper
        {
            get
            {
                return inTypeHelper;
            }
            set
            {
                inTypeHelper = value;
            }
        }

        /// <summary>
        /// 出库类型帮助类
        /// </summary>
        public FS.FrameWork.Public.ObjectHelper OutTypeHelper
        {
            get
            {
                return outTypeHelper;
            }
            set
            {
                outTypeHelper = value;
            }
        }

        /// <summary>
        /// 科室帮助类
        /// </summary>
        public FS.FrameWork.Public.ObjectHelper DeptHelper
        {
            get
            {
                return deptHelper;
            }
            set
            {
                deptHelper = value;
            }
        }

        /// <summary>
        /// 物资ID
        /// </summary>
        public string ItemID
        {
            get 
            { 
                return itemID; 
            }
            set 
            { 
                itemID = value; 
            }
        }

        /// <summary>
        /// 本身科室
        /// </summary>
        public string DeptCode
        {
            get
            {
                return deptCode;
            }
            set
            {
                deptCode = value;
            }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 初始化
        /// </summary>
        public override void Init()
        {
            base.Init();

            FarPoint.Win.Spread.CellType.TextCellType txtType = new FarPoint.Win.Spread.CellType.TextCellType();
            base.neuSpread1_Sheet1.Columns[0, neuSpread1_Sheet1.ColumnCount - 1].CellType = txtType;
            #region 获取入库权限

            FS.HISFC.BizLogic.Manager.PowerLevelManager myManager = new FS.HISFC.BizLogic.Manager.PowerLevelManager();
            ArrayList inPriv = myManager.LoadLevel3ByLevel2("0510");

            ArrayList alPriv = new ArrayList();
            FS.FrameWork.Models.NeuObject tempInfo = new FS.FrameWork.Models.NeuObject();
            foreach (FS.HISFC.Models.Admin.PowerLevelClass3 info in inPriv)
            {
                tempInfo = new FS.FrameWork.Models.NeuObject();
                tempInfo.ID = info.Class3Code;
                tempInfo.Name = info.Class3Name;

                alPriv.Add(tempInfo);
            }
            this.inTypeHelper = new FS.FrameWork.Public.ObjectHelper(alPriv);

            #endregion

            #region 获取出库权限

            ArrayList outPriv = myManager.LoadLevel3ByLevel2("0520");

            ArrayList alOutPriv = new ArrayList();
            FS.FrameWork.Models.NeuObject tempOutfo = new FS.FrameWork.Models.NeuObject();
            foreach (FS.HISFC.Models.Admin.PowerLevelClass3 info in outPriv)
            {
                tempOutfo = new FS.FrameWork.Models.NeuObject();
                tempOutfo.ID = info.Class3Code;
                tempOutfo.Name = info.Class3Name;

                alOutPriv.Add(tempOutfo);
            }
            this.outTypeHelper = new FS.FrameWork.Public.ObjectHelper(alOutPriv);

            #endregion

            #region 获取科室

            FS.HISFC.BizLogic.Manager.Department deptManager = new FS.HISFC.BizLogic.Manager.Department();
            ArrayList alDept = deptManager.GetDeptmentAll();
            if (alDept != null)
                this.deptHelper = new FS.FrameWork.Public.ObjectHelper(alDept);

            #endregion

            #region 获取物资列表

            this.GetItemList();

            #endregion
        }

        /// <summary>
        /// 获取物资列表
        /// </summary>
        private void GetItemList()
        {
            this.ucMaterialItemList1.ShowFpRowHeader = false;

            this.ucMaterialItemList1.ShowMaterialList("all");            
        }

        /// <summary>
        /// 入库单据查询
        /// </summary>
        protected override void QueryIn()
        {
            FS.HISFC.BizLogic.Material.Store itemManager = new FS.HISFC.BizLogic.Material.Store();

            //重新写了查找单据的方法  增加了通过物资项目查询
            //ArrayList alList = itemManager.QueryInputList(this.DeptInfo.ID, "AA", this.State, this.BeginDate, this.EndDate);
            ArrayList alList = itemManager.QueryInputList(this.DeptInfo.ID, "AA", this.State, this.BeginDate, this.EndDate, this.itemID);
            if (alList == null)
            {
                MessageBox.Show("查询单据列表发生错误" + itemManager.Err);
            }

            this.neuSpread1_Sheet1.Rows.Count = 0;

            foreach (FS.HISFC.Models.Material.Input info in alList)
            {
                if (this.MarkPrivType != null)
                {
                    if (this.MarkPrivType.ContainsKey(info.StoreBase.PrivType))       //对于过滤的权限不显示
                    {
                        continue;
                    }
                }

                this.neuSpread1_Sheet1.Rows.Add(0, 1);

                this.neuSpread1_Sheet1.Cells[0, 0].Text = info.InListNO;
                this.neuSpread1_Sheet1.Cells[0, 2].Text = this.inTypeHelper.GetName(info.StoreBase.PrivType);

                if (this.DeptInfo.Memo == "L")			//仓库 获取供货公司
                {
                    #region 获取供货公司

                    FS.HISFC.Models.Material.MaterialCompany company = new FS.HISFC.Models.Material.MaterialCompany();

                    if (info.StoreBase.Company.ID != "None")
                    {
                        FS.HISFC.BizLogic.Material.ComCompany constant = new FS.HISFC.BizLogic.Material.ComCompany();
                        company = constant.QueryCompanyByCompanyID(info.StoreBase.Company.ID, "A", "1");
                        if (company == null)
                        {
                            MessageBox.Show(constant.Err);
                            return;
                        }
                    }
                    else
                    {
                        company.ID = "None";
                        company.Name = "无供货公司";
                    }

                    this.neuSpread1_Sheet1.Cells[0, 3].Text = company.Name;
                    this.neuSpread1_Sheet1.Cells[0, 4].Text = company.ID;

                    #endregion
                }
            }
        }

        /// <summary>
        /// 出库单据查询
        /// </summary>
        protected override void QueryOut()
        {
            this.ShowSelectData();
            FS.HISFC.BizLogic.Material.Store itemManager = new FS.HISFC.BizLogic.Material.Store();
            //重新写了查找单据的方法  增加了通过物资项目查询
            //List<FS.HISFC.Models.Material.Output> alList = itemManager.QueryOutputList(this.DeptInfo.ID, "AA", this.State, this.BeginDate, this.EndDate);
            List<FS.HISFC.Models.Material.Output> alList = itemManager.QueryOutputList(this.DeptInfo.ID, "AA", this.State, this.BeginDate, this.EndDate, itemID, this.deptCode);
            if (alList == null)
            {
                MessageBox.Show("查询单据列表发生错误" + itemManager.Err);
            }

            this.neuSpread1_Sheet1.Rows.Count = 0;

            foreach (FS.HISFC.Models.Material.Output info in alList)
            {
                this.neuSpread1_Sheet1.Rows.Add(0, 1);

                this.neuSpread1_Sheet1.Cells[0, (int)ColumnSet.ColList].Text = info.OutListNO;

                this.neuSpread1_Sheet1.Cells[0, 1].Text = info.StoreBase.StockDept.Name;

                this.neuSpread1_Sheet1.Cells[0, 2].Text = info.StoreBase.StockDept.ID;

                this.neuSpread1_Sheet1.Cells[0, (int)ColumnSet.ColTargetName].Text = info.StoreBase.TargetDept.Name;

                this.neuSpread1_Sheet1.Cells[0, (int)ColumnSet.ColTargetID].Text = info.StoreBase.TargetDept.ID;
            }
        }

        /// <summary>
        /// 采购单据查询
        /// </summary>
        protected override void QueryStock()
        {
            this.ShowStockData();

            FS.HISFC.BizLogic.Material.Plan planManager = new FS.HISFC.BizLogic.Material.Plan();

            ArrayList al = planManager.QueryStockPLanCompanayList(this.DeptInfo.ID, "2");
            if (al == null)
            {
                MessageBox.Show("获取采购单列表发生错误!" + planManager.Err);
                return;
            }

            this.neuSpread1_Sheet1.Rows.Count = 0;

            foreach (FS.FrameWork.Models.NeuObject info in al)
            {
                this.neuSpread1_Sheet1.Rows.Add(0, 1);

                this.neuSpread1_Sheet1.Cells[0, (int)ColumnSet.ColList].Text = info.ID;						//采购单号
                this.neuSpread1_Sheet1.Cells[0, (int)ColumnSet.ColTargetName].Text = info.User01;			//供货公司名称
                this.neuSpread1_Sheet1.Cells[0, (int)ColumnSet.ColTargetID].Text = info.Name;				//供货公司编码
            }
        }

        private int ShowStockData()
        {
            string[] filterStr = new string[1] { "采购单号", };
            string[] label = new string[] { "采购单号", "送货单号", "类型", "供货单位", "供货单位编码" };
            float[] width = new float[] { 120, 100, 100, 100, 100 };
            bool[] visible = new bool[] { true, false, false, true, true };

            this.InitFp(label, visible, width);

            return 1;
        }

        private int ShowSelectData()
        {
            string[] filterStr = new string[1] { "出库单号", };
            string[] label = new string[] { "出库单号", "出库科室", "出库科室代码", "目标科室", "目标科室代码" };
            float[] width = new float[] { 120, 100, 60, 100, 60 };
            bool[] visible = new bool[] { true, true, false, true, false };

            this.InitFp(label, visible, width);

            return 1;
        }

        #endregion

        #region 事件

        /// <summary>
        /// 获取列表中当前活动行的物资项目ID
        /// </summary>
        /// <param name="sv"></param>
        /// <param name="activeRow"></param>
        private void ucMaterialItemList1_ChooseDataEvent(FarPoint.Win.Spread.SheetView sv, int activeRow)
        {
            if (chkItem.Checked)
            {
                if (sv != null && activeRow >= 0)
                {
                    this.itemID = sv.Cells[activeRow, 10].Text;
                    this.chkItem.Text = sv.Cells[activeRow, 1].Text;
                } 
            }
        }        
        
        /// <summary>
        /// 判断是否通过物资过滤  和  当前选择的物资名
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkItem_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkItem.Checked)
            {
                this.itemID = "all";
                this.chkItem.Text = "未选择项目";
            }
        }

        #endregion
    }
}
