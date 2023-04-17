using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Neusoft.FrameWork.Management;
using Neusoft.HISFC.BizLogic.Material.BizLogic;

namespace Neusoft.SOC.Local.Material.Report
{
    public partial class ucGeneralQuery : Neusoft.FrameWork.WinForms.Controls.ucBaseControl, Neusoft.FrameWork.WinForms.Classes.IPreArrange
    {
        #region 构造函数
        public ucGeneralQuery()
        {
            InitializeComponent();
        }
        #endregion

        #region 变量

        /// <summary>
        /// 当前操作员
        /// </summary>
        private Neusoft.FrameWork.Models.NeuObject currentOper = new Neusoft.FrameWork.Models.NeuObject();

        /// <summary>
        /// 当前操作科室
        /// </summary>
        private Neusoft.FrameWork.Models.NeuObject currentDept = new Neusoft.FrameWork.Models.NeuObject();

        /// <summary>
        /// 综合管理类
        /// </summary>
        private Neusoft.HISFC.Interface.Material.InterfaceProxy.ManagerProxy managerProxy = new Neusoft.HISFC.Interface.Material.InterfaceProxy.ManagerProxy();

        /// <summary>
        /// 综合查询管理类
        /// </summary>
        private Neusoft.HISFC.BizProcess.Material.Report.ReportProcess reportProcess = new Neusoft.HISFC.BizProcess.Material.Report.ReportProcess();

        /// <summary>
        /// 操作科室列表
        /// </summary>
        private List<Neusoft.FrameWork.Models.NeuObject> alPrivDeptList = new List<Neusoft.FrameWork.Models.NeuObject>();

        /// <summary>
        /// 汇总表
        /// </summary>
        private DataTable dtCollect = new DataTable();

        /// <summary>
        /// 明细表
        /// </summary>
        private DataTable dtDetail = new DataTable();

        /// <summary>
        /// 小数点后保留位数
        /// {73E76169-EC00-47fa-8D16-009BA10C6ED9}
        /// </summary>
        private int decimalPlaces = 2;

        //{73E76169-EC00-47fa-8D16-009BA10C6ED9}
        private TreeNode selectedTreeNode = new TreeNode();

        #endregion

        #region 属性
        /// <summary>
        /// 小数点后保留位数
        /// {73E76169-EC00-47fa-8D16-009BA10C6ED9}
        /// </summary>
        public int DecimalPlaces
        {
            get { return decimalPlaces; }
            set { decimalPlaces = value; }
        }

        #endregion

        #region 方法
        #region 私有方法

        /// <summary>
        /// 初始化函数
        /// </summary>
        /// <returns></returns>
        private int Init()
        {
            this.SpreadDetail_Sheet1.ColumnCount = 0;
            this.SpreadDetail_Sheet1.RowCount = 0;
            this.SpreadDetail_Sheet1.DataAutoCellTypes = false;
            this.SpreadDetail_Sheet1.DataAutoSizeColumns = false;
            this.SpreadDetail_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;

            this.SpreadCollect_Sheet1.ColumnCount = 0;
            this.SpreadCollect_Sheet1.RowCount = 0;
            this.SpreadCollect_Sheet1.DataAutoCellTypes = false;
            this.SpreadCollect_Sheet1.DataAutoSizeColumns = false;
            this.SpreadCollect_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
            //初始化操作科室
            this.cmbDept.AddItems(alPrivDeptList);
            this.cmbDept.SelectedIndex = 0;
            this.rbtByDeptOrCompany.Checked = true;

            return 1;
        }

        /// <summary>
        /// 初始化操作权限树
        /// </summary>
        /// <returns></returns>
        private int InitOperTypeTree()
        {
            this.tvOperType.ImageList = this.tvOperType.groupImageList;
            this.tvOperType.Nodes.Clear();

            #region 加载入库权限

            int privIndex = FrameWork.Function.NConvert.ToInt32(HISFC.BizLogic.Material.BizLogic.EnumClass2Priv.入库);
            List<Neusoft.FrameWork.Models.NeuObject> alInPriv = this.managerProxy.QueryUserPrivCollection(this.currentOper.ID, privIndex.ToString(), this.currentDept.ID);
            if (alInPriv == null)
            {
                MessageBox.Show(Language.Msg("读取操作员入库权限集合时出错！\n") + this.managerProxy.Err);
                return -1;
            }
            if (alInPriv.Count != 0)
            {
                TreeNode inParentNode = new TreeNode(Language.Msg("入库"), 0, 1);
                this.tvOperType.Nodes.Add(inParentNode);
                foreach (Neusoft.FrameWork.Models.NeuObject inPriv in alInPriv)
                {
                    TreeNode privNode = new TreeNode();
                    privNode.ImageIndex = 0;
                    privNode.SelectedImageIndex = 1;
                    privNode.Text = inPriv.Name;
                   
                    inParentNode.Nodes.Add(privNode);
                }
                //取三级权限含义码
                ArrayList alClass3Priv = this.managerProxy.LoadLevel3ByLevel2(privIndex.ToString());
                TreeNode tmpNode = null;
                foreach (Neusoft.HISFC.Models.Admin.PowerLevelClass3 levelClass3 in alClass3Priv)
                {
                    tmpNode = this.FindNode(inParentNode, levelClass3.Class3Name);
                    if (tmpNode != null)
                    {
                        tmpNode.Tag = levelClass3;
                    }
                }
            }
            #endregion

            #region 加载出库权限
            privIndex = FrameWork.Function.NConvert.ToInt32(HISFC.BizLogic.Material.BizLogic.EnumClass2Priv.出库);
            List<Neusoft.FrameWork.Models.NeuObject> alOutPriv = this.managerProxy.QueryUserPrivCollection(this.currentOper.ID, privIndex.ToString(), this.currentDept.ID);
            if (alOutPriv == null)
            {
                MessageBox.Show(Language.Msg("读取操作员出库权限集合时出错！\n") + this.managerProxy.Err);
                return -1;
            }
            if (alOutPriv.Count != 0)
            {
                TreeNode outParentNode = new TreeNode(Language.Msg("出库"), 0, 1);
                this.tvOperType.Nodes.Add(outParentNode);
                foreach (Neusoft.FrameWork.Models.NeuObject outPriv in alOutPriv)
                {
                    TreeNode privNode = new TreeNode();
                    privNode.ImageIndex = 0;
                    privNode.SelectedImageIndex = 1;
                    privNode.Text = outPriv.Name;

                    outParentNode.Nodes.Add(privNode);
                }
                //取三级权限含义码
                ArrayList alClass3Priv = this.managerProxy.LoadLevel3ByLevel2(privIndex.ToString());
                TreeNode tmpNode = null;
                foreach (Neusoft.HISFC.Models.Admin.PowerLevelClass3 levelClass3 in alClass3Priv)
                {
                    tmpNode = this.FindNode(outParentNode, levelClass3.Class3Name);
                    if (tmpNode != null)
                    {
                        tmpNode.Tag = levelClass3;
                    }
                }
            }
            #endregion

            #region 加载盘点权限
            privIndex = FrameWork.Function.NConvert.ToInt32(HISFC.BizLogic.Material.BizLogic.EnumClass2Priv.盘点);
            List<Neusoft.FrameWork.Models.NeuObject> alCheckPriv = this.managerProxy.QueryUserPrivCollection(this.currentOper.ID, privIndex.ToString(), this.currentDept.ID);
            if (alCheckPriv == null)
            {
                MessageBox.Show(Language.Msg("读取操作员盘点权限集合时出错！\n") + this.managerProxy.Err);
                return -1;
            }
            if (alCheckPriv.Count != 0)
            {
                TreeNode checkParentNode = new TreeNode(Language.Msg("盘点"), 0, 1);
                this.tvOperType.Nodes.Add(checkParentNode);
                foreach (Neusoft.FrameWork.Models.NeuObject checkPriv in alCheckPriv)
                {
                    TreeNode privNode = new TreeNode();
                    privNode.ImageIndex = 0;
                    privNode.SelectedImageIndex = 1;
                    privNode.Text = checkPriv.Name;

                    checkParentNode.Nodes.Add(privNode);
                }
                //取三级权限含义码
                ArrayList alClass3Priv = this.managerProxy.LoadLevel3ByLevel2(privIndex.ToString());
                TreeNode tmpNode = null;
                foreach (Neusoft.HISFC.Models.Admin.PowerLevelClass3 levelClass3 in alClass3Priv)
                {
                    tmpNode = this.FindNode(checkParentNode, levelClass3.Class3Name);
                    if (tmpNode != null)
                    {
                        tmpNode.Tag = levelClass3;
                    }
                }
            }
            #endregion

            #region 加载月结权限
            privIndex = FrameWork.Function.NConvert.ToInt32(HISFC.BizLogic.Material.BizLogic.EnumClass2Priv.月结);
            List<Neusoft.FrameWork.Models.NeuObject> alMonthStorePriv = this.managerProxy.QueryUserPrivCollection(this.currentOper.ID, privIndex.ToString(), this.currentDept.ID);
            if (alMonthStorePriv == null)
            {
                MessageBox.Show(Language.Msg("读取操作员月结权限集合时出错！\n") + this.managerProxy.Err);
                return -1;
            }
            if (alMonthStorePriv.Count != 0)
            {
                TreeNode monthStoreParentNode = new TreeNode(Language.Msg("月结"), 0, 1);
                this.tvOperType.Nodes.Add(monthStoreParentNode);
                foreach (Neusoft.FrameWork.Models.NeuObject monthStorePriv in alMonthStorePriv)
                {
                    TreeNode privNode = new TreeNode();
                    privNode.ImageIndex = 0;
                    privNode.SelectedImageIndex = 1;
                    privNode.Text = monthStorePriv.Name;

                    monthStoreParentNode.Nodes.Add(privNode);
                }
                //取三级权限含义码
                ArrayList alClass3Priv = this.managerProxy.LoadLevel3ByLevel2(privIndex.ToString());
                TreeNode tmpNode = null;
                foreach (Neusoft.HISFC.Models.Admin.PowerLevelClass3 levelClass3 in alClass3Priv)
                {
                    tmpNode = this.FindNode(monthStoreParentNode, levelClass3.Class3Name);
                    if (tmpNode != null)
                    {
                        tmpNode.Tag = levelClass3;
                    }
                }
            }
            #endregion

            this.tvOperType.ExpandAll();
            if (this.tvOperType.Nodes.Count > 0)
            {
                this.tvOperType.SelectedNode = this.tvOperType.Nodes[0];
            }
            return 1;
        }

        /// <summary>
        /// 查找某树中的某个节点
        /// </summary>
        /// <param name="parent">根节点</param>
        /// <param name="strValue">节点名</param>
        /// <returns></returns>
        private TreeNode FindNode(TreeNode parent, string strValue)
        {
            if (parent == null)
            {
                return null;
            }
            if (parent.Text == strValue)
            {
                return parent;
            }
            TreeNode ret = null;
            foreach (TreeNode tn in parent.Nodes)
            {
                ret = FindNode(tn, strValue);
                if (ret != null)
                {
                    break;
                }
            }
            return ret;
        }

        /// <summary>
        /// 清空过滤框
        /// </summary>
        private void ClearFilter()
        {
            this.txtCompany.Text = string.Empty;
            this.txtMatKind.Text = string.Empty;
            this.txtMatName.Text = string.Empty;
            this.txtMatSpecs.Text = string.Empty;
            //{73E76169-EC00-47fa-8D16-009BA10C6ED9}
            this.txtDeptName.Text = string.Empty;
        }

        /// <summary>
        /// 获取过滤条件
        /// </summary>
        /// <returns></returns>
        private string GetFilter()
        {
            string filter = " 1=1 ";
            string filterMat = string.Empty;
            string filterKind = string.Empty;
            string filterSpecs = string.Empty;
            string filterCompany = string.Empty;
            //{73E76169-EC00-47fa-8D16-009BA10C6ED9}
            string filterDept = string.Empty;
            if (!this.txtMatName.ReadOnly && !string.IsNullOrEmpty(this.txtMatName.Text.Trim()))
            {
                string text = this.txtMatName.Text.Trim();
                filterMat = string.Format(" and ( 物品名称 like '%{0}%' or 物品拼音码 like '%{0}%' or 物品五笔码 like '%{0}%' or 物品自定义码 like '%{0}%')", text);
            } if (!this.txtMatKind.ReadOnly && !string.IsNullOrEmpty(this.txtMatKind.Text.Trim()))
            {
                string text = this.txtMatKind.Text.Trim();
                filterKind = string.Format(" and (物品类别名称 like '%{0}%' or 物品类别拼音码 like '%{0}%' or 物品类别五笔码 like '%{0}%' or 物品类别自定义码 like '%{0}%')", text);
            }
            if (!this.txtMatSpecs.ReadOnly && !string.IsNullOrEmpty(this.txtMatSpecs.Text.Trim()))
            {
                string text = this.txtMatSpecs.Text.Trim();
                filterSpecs = string.Format(" and (规格 like '%{0}%')", text);
            }
            if (!this.txtCompany.ReadOnly && !string.IsNullOrEmpty(this.txtCompany.Text.Trim()))
            {
                string text = this.txtCompany.Text.Trim();
                filterCompany = string.Format(" and (供货公司名称 like '%{0}%' or 供货公司拼音码 like '%{0}%' or 供货公司五笔码 like '%{0}%' or 供货公司自定义码 like '%{0}%')", text);
            }
            //{73E76169-EC00-47fa-8D16-009BA10C6ED9}
            if (!this.txtDeptName.ReadOnly && !string.IsNullOrEmpty(this.txtDeptName.Text.Trim()))
            {
                string text = this.txtDeptName.Text.Trim();
                filterDept = string.Format("and(领用科室 like '%{0}%' or 领用科室拼音码 like '%{0}%' or 领用科室五笔码 like '%{0}%')", text);
            }
            filter += filterMat + filterKind + filterSpecs + filterCompany + filterDept;
            return filter;
        }

        /// <summary>
        /// 查询方法
        /// </summary>
        /// <returns></returns>
        private int QueryCollectInfo()
        {
            DateTime dtBegin = this.dtpBeginDate.Value.Date;
            DateTime dtEnd = this.dtpEndDate.Value.Date.AddDays(1).AddSeconds(-1);

            #region 获取查询编码
            Neusoft.HISFC.Models.Admin.PowerLevelClass3 priv3 = this.tvOperType.SelectedNode.Tag as Neusoft.HISFC.Models.Admin.PowerLevelClass3;
            string class2Code = priv3.Class2Code;
            string class3Code = priv3.Class3Code;
            string class3MeaningCode = priv3.Class3MeaningCode;
            #endregion

            #region 获取查询类别
            Neusoft.HISFC.BizLogic.Material.BizLogic.AssortType queryType = Neusoft.HISFC.BizLogic.Material.BizLogic.AssortType.Mat;
            if (this.rbtByDeptOrCompany.Checked)
            {
                queryType = Neusoft.HISFC.BizLogic.Material.BizLogic.AssortType.MatCompany;
            }
            else if (this.rbtByBill.Checked)
            {
                queryType = Neusoft.HISFC.BizLogic.Material.BizLogic.AssortType.MatBill;
            }
            else if(this.rbtByMat.Checked)
            {
                queryType = Neusoft.HISFC.BizLogic.Material.BizLogic.AssortType.Mat;
            }

            //{5C52C6F0-71C1-443f-AA35-9149564BA556}
            else if (this.rbtByType.Checked)
            {
                queryType=Neusoft.HISFC.BizLogic.Material.BizLogic.AssortType.MatType;
            }
            #endregion

            #region 获取查询时间

            this.lblCollectDate.Text = dtBegin.ToShortDateString() + " 至 " + dtEnd.ToShortDateString();
            this.lblDetailDate.Text = dtBegin.ToShortDateString() + " 至 " + dtEnd.ToShortDateString();

            #endregion

            this.SpreadCollect_Sheet1.RowCount = 0;
            this.SpreadDetail_Sheet1.RowCount = 0;
            this.SpreadCollect_Sheet1.ColumnCount = 0;
            this.SpreadDetail_Sheet1.ColumnCount = 0;
            string searchKey = string.Empty;
            DataSet result = new DataSet();
            if (this.reportProcess.QueryGeneralInfo(this.currentDept.ID, class2Code, class3Code, class3MeaningCode, dtBegin, dtEnd, searchKey, queryType, true, ref result) < 0)
            {
                MessageBox.Show(Language.Msg("查询汇总信息出错:") + this.reportProcess.Err);
                return -1;
            }
            if(result ==null)
            {
                return -1;
            }
            if (result.Tables.Count > 0)
            {
                this.dtCollect = result.Tables[0];
                this.SpreadCollect_Sheet1.DataSource = this.dtCollect.DefaultView;
                this.SetCollectInfoFpStyle();
                this.SetFilterVisible(this.SpreadCollect_Sheet1);
                this.neuTabControl1.SelectedTab = this.tabPage1;
            }
            //{73E76169-EC00-47fa-8D16-009BA10C6ED9}
            if (this.tvOperType.SelectedNode.Parent.Text == "入库")
            {
                if (this.tvOperType.SelectedNode.Text == "内部入库申请" || this.tvOperType.SelectedNode.Text == "内部入库核准")
                {
                    this.lbInfo.Text = this.GetSumOfString(EnumMatColumnSumType.申请总金额.ToString(), new List<DataRow>());
                }
                this.lbInfo.Text = this.GetSumOfString(EnumMatColumnSumType.入库总金额.ToString(), new List<DataRow>());
            }
            else if (this.tvOperType.SelectedNode.Parent.Text == "出库")
            {
                this.lbInfo.Text = this.GetSumOfString(EnumMatColumnSumType.出库金额.ToString(), new List<DataRow>());
            }
            else if (this.tvOperType.SelectedNode.Parent.Text == "盘点")
            {
                this.lbInfo.Visible = false;
            }
            else if (this.tvOperType.SelectedNode.Parent.Text == "月结")
            {
                this.lbInfo.Visible = false;
            }
            this.ClearFilter();
            return 1;
        }

        /// <summary>
        /// 设置汇总界面格式
        /// </summary>
        private void SetCollectInfoFpStyle()
        {
            FarPoint.Win.Spread.CellType.NumberCellType numCellType = new FarPoint.Win.Spread.CellType.NumberCellType();
            numCellType.DecimalPlaces = Base.Function.NumDecimalPlaces;
            FarPoint.Win.Spread.CellType.NumberCellType moneyCellType = new FarPoint.Win.Spread.CellType.NumberCellType();
            moneyCellType.DecimalPlaces = Base.Function.MoneyDecimalPlaces;
            for (int i = 0; i < this.SpreadCollect_Sheet1.ColumnCount; i++)
            {
                FarPoint.Win.Spread.Column column = this.SpreadCollect_Sheet1.Columns[i];
                if (column.Label.Contains("拼音码") || column.Label.Contains("五笔码") || column.Label.Contains("自定义码"))
                {
                    this.SpreadCollect_Sheet1.Columns[i].Visible = false;
                }
                this.SpreadCollect_Sheet1.Columns[i].Locked = true;

                if (column.Label.Contains("金额") || column.Label.Contains("单价") || column.Label.Contains("价"))
                {
                    this.SpreadCollect_Sheet1.Columns[i].CellType = moneyCellType;
                }
                else if (column.Label.Contains("数量") && !column.Label.Contains("包装数量"))
                {
                    this.SpreadCollect_Sheet1.Columns[i].CellType = numCellType;
                }
                if (this.SpreadCollect_Sheet1.Columns[i].Visible)
                {
                    this.SpreadCollect_Sheet1.Columns[i].Width = this.SpreadCollect_Sheet1.Columns[i].GetPreferredWidth() + 4;
                }
            }
        }

        /// <summary>
        /// 设置明细界面格式
        /// </summary>
        private void SetDetailInfoFpStyle()
        {
            FarPoint.Win.Spread.CellType.NumberCellType numCellType = new FarPoint.Win.Spread.CellType.NumberCellType();
            numCellType.DecimalPlaces = Base.Function.NumDecimalPlaces;
            FarPoint.Win.Spread.CellType.NumberCellType moneyCellType = new FarPoint.Win.Spread.CellType.NumberCellType();
            moneyCellType.DecimalPlaces = Base.Function.MoneyDecimalPlaces;
            for (int i = 0; i < this.SpreadDetail_Sheet1.ColumnCount; i++)
            {
                FarPoint.Win.Spread.Column column = this.SpreadDetail_Sheet1.Columns[i];
                if (column.Label.Contains("拼音码") || column.Label.Contains("五笔码") || column.Label.Contains("自定义码"))
                {
                    this.SpreadDetail_Sheet1.Columns[i].Visible = false;
                }
                this.SpreadDetail_Sheet1.Columns[i].Locked = true;

                if (column.Label.Contains("金额") || column.Label.Contains("单价") || column.Label.Contains("价"))
                {
                    this.SpreadDetail_Sheet1.Columns[i].CellType = moneyCellType;
                }
                else if (column.Label.Contains("数量") && !column.Label.Contains("包装数量"))
                {
                    this.SpreadDetail_Sheet1.Columns[i].CellType = numCellType;
                }
                if (this.SpreadDetail_Sheet1.Columns[i].Visible)
                {
                    this.SpreadDetail_Sheet1.Columns[i].Width = this.SpreadDetail_Sheet1.Columns[i].GetPreferredWidth() + 4;
                }
            }
        }

        private void SetFilterVisible(FarPoint.Win.Spread.SheetView sv)
        {
            int columnIndex = Base.Function.GetColIndex("供货公司拼音码", sv);
            if (columnIndex < 0)
            {
                this.txtCompany.ReadOnly = true;
                this.txtCompany.BackColor = Color.Honeydew;
            }
            else
            {
                this.txtCompany.ReadOnly = false;
                this.txtCompany.BackColor = Color.SeaShell;
            }
            columnIndex = Base.Function.GetColIndex("物品拼音码", sv);
            if (columnIndex < 0)
            {
                this.txtMatName.ReadOnly = true;
                this.txtMatName.BackColor = Color.Honeydew;
            }
            else
            {
                this.txtMatName.ReadOnly = false;
                this.txtMatName.BackColor = Color.SeaShell;
            }
            columnIndex = Base.Function.GetColIndex("规格", sv);
            if (columnIndex < 0)
            {
                this.txtMatSpecs.ReadOnly = true;
                this.txtMatSpecs.BackColor = Color.Honeydew;
            }
            else
            {
                this.txtMatSpecs.ReadOnly = false;
                this.txtMatSpecs.BackColor = Color.SeaShell;
            }
            columnIndex = Base.Function.GetColIndex("物品类别拼音码", sv);
            if (columnIndex < 0)
            {
                this.txtMatKind.ReadOnly = true;
                this.txtMatKind.BackColor = Color.Honeydew;
            }
            else
            {
                this.txtMatKind.ReadOnly = false;
                this.txtMatKind.BackColor = Color.SeaShell;
            }
            //{73E76169-EC00-47fa-8D16-009BA10C6ED9}
            columnIndex = Base.Function.GetColIndex("领用科室拼音码", sv);
            if (columnIndex < 0)
            {
                this.txtDeptName.ReadOnly = true;
                this.txtDeptName.BackColor = Color.Honeydew;
            }
            else
            {
                this.txtDeptName.ReadOnly = false;
                this.txtDeptName.BackColor = Color.SeaShell;
            }
        }

        /// <summary>
        /// 导出方法
        /// </summary>
        /// <returns></returns>
        private int Export()
        {
            if (this.neuTabControl1.SelectedTab == this.tabPage1)
            {
                this.SpreadCollect.Export();
            }
            else if (this.neuTabControl1.SelectedTab == this.tabPage2)
            {
                this.SpreadDetail.Export();
            }
            return 1;
        }

        /// <summary>
        /// 打印方法
        /// </summary>
        /// <returns></returns>
        private int Print()
        {
            return 1;
        }
        #endregion

        #region 保护方法

        //{73E76169-EC00-47fa-8D16-009BA10C6ED9}
        protected string GetSumOfString(string lableName, List<DataRow> dataList)
        {
            lableName = Language.Msg(lableName);
            string message = string.Empty;
            decimal sum = 0.00M;
            if (this.SpreadCollect_Sheet1.RowCount != 0)
            {
                foreach (DataRow dr in this.dtCollect.Rows)
                {
                    if (dataList.Contains(dr) || dr[lableName].ToString() == string.Empty)
                    {
                        continue;
                    }
                    sum += Neusoft.FrameWork.Function.NConvert.ToDecimal(dr[lableName].ToString());
                }
            }
            else if (this.SpreadDetail_Sheet1.RowCount != 0)
            {
                foreach (DataRow dr in this.dtDetail.Rows)
                {
                    if (dataList.Contains(dr) || dr[lableName].ToString() == string.Empty)
                    {
                        continue;
                    }
                    sum += Neusoft.FrameWork.Function.NConvert.ToDecimal(dr[lableName].ToString());
                }
            }
            message = string.Format("{0}合计:{1}", lableName, decimal.Round(sum, this.decimalPlaces, MidpointRounding.AwayFromZero));
            return message;
        }

        #endregion

        #endregion

        #region 事件

        /// <summary>
        /// 加载事件
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            if (!this.DesignMode)
            {
                if (this.Init() < 0)
                {
                    return;
                }
            }
        }

        /// <summary>
        /// 查询事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnQuery(object sender, object neuObject)
        {
            if (this.tvOperType.SelectedNode.Level != 1)
            {
                return 1;
            }
            return this.QueryCollectInfo();
        }

        /// <summary>
        /// 导出事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        public override int Export(object sender, object neuObject)
        {
            return this.Export();
        }

        /// <summary>
        /// 打印事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnPrint(object sender, object neuObject)
        {
            return this.Print();
        }

        /// <summary>
        /// 操作权限选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvOperType_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Level != 1)
            {
                return;
            }
            this.rbtByDeptOrCompany.Enabled = true;
            this.rbtByDeptOrCompany.Checked = true;
            this.rbtByMat.Enabled = true;
            //{5C52C6F0-71C1-443f-AA35-9149564BA556}
            this.rbtByType.Enabled = true;
            this.dtpBeginDate.Enabled = true;
            this.dtpEndDate.Enabled = true;

            if (e.Node.Parent.Text == Language.Msg("月结") || e.Node.Parent.Text == Language.Msg("盘点"))
            {
                this.rbtByDeptOrCompany.Enabled = false;
                this.rbtByMat.Enabled = false;
                //{5C52C6F0-71C1-443f-AA35-9149564BA556}
                this.rbtByType.Enabled = false;
                this.rbtByBill.Checked = true;
            }
            Neusoft.HISFC.Models.Admin.PowerLevelClass3 level3 = e.Node.Tag as Neusoft.HISFC.Models.Admin.PowerLevelClass3;
            if ( level3.Class2Code == ((int)Neusoft.HISFC.BizLogic.Material.BizLogic.EnumClass2Priv.入库).ToString()
                && (level3.Class3MeaningCode == ((int)Neusoft.HISFC.BizLogic.Material.BizLogic.EnumInputSysPriv.自增加入库).ToString()
                || level3.Class3MeaningCode == ((int)Neusoft.HISFC.BizLogic.Material.BizLogic.EnumInputSysPriv.自增加入库核准).ToString()))
                
            {
                this.rbtByDeptOrCompany.Enabled = false;
                this.rbtByBill.Enabled = true;
            }
            if (level3.Class2Code == ((int)Neusoft.HISFC.BizLogic.Material.BizLogic.EnumClass2Priv.出库).ToString() &&
                (level3.Class3MeaningCode == ((int)Neusoft.HISFC.BizLogic.Material.BizLogic.EnumOutputSysPriv.自减少出库).ToString()
                || level3.Class3MeaningCode == ((int)Neusoft.HISFC.BizLogic.Material.BizLogic.EnumOutputSysPriv.自减少出库核准).ToString()))
            {
                this.rbtByDeptOrCompany.Enabled = false;
                this.rbtByBill.Checked = true;
                this.rbtByBill.Enabled = true;
            }
        }

        /// <summary>
        /// 日期控件调整事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtp_ValueChanged(object sender, EventArgs e)
        {
            DateTime dtBegin = this.dtpBeginDate.Value.Date;
            DateTime dtEnd = this.dtpEndDate.Value.Date;
            if (dtBegin > dtEnd)
            {
                this.dtpBeginDate.Value = dtEnd;
            }
        }

        private void txt_TextChanged(object sender, EventArgs e)
        {
            string filter = this.GetFilter();
            if (this.neuTabControl1.SelectedTab == this.tabPage1)
            {
                this.dtCollect.DefaultView.RowFilter = filter;
            }
            else if (this.neuTabControl1.SelectedTab == this.tabPage2)
            {
                this.dtDetail.DefaultView.RowFilter = filter;
            }
        }

        /// <summary>
        /// 操作科室选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ClearFilter();
            this.currentDept.ID = this.cmbDept.Tag as string;
            this.InitOperTypeTree();
        }

        /// <summary>
        /// 双击汇总数据显示明细，第一列为控制列
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SpreadCollect_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            //查询条件
            string searchKey = string.Empty;
            searchKey = this.SpreadCollect_Sheet1.Cells[e.Row, 0].Text;
            if (string.IsNullOrEmpty(searchKey))
            {
                return;
            }
            DateTime dtBegin = this.dtpBeginDate.Value.Date;
            DateTime dtEnd = this.dtpEndDate.Value.Date.AddDays(1).AddSeconds(-1);

            #region 获取查询编码
            Neusoft.HISFC.Models.Admin.PowerLevelClass3 priv3 = this.tvOperType.SelectedNode.Tag as Neusoft.HISFC.Models.Admin.PowerLevelClass3;
            string class2Code = priv3.Class2Code;
            string class3Code = priv3.Class3Code;
            string class3MeaningCode = priv3.Class3MeaningCode;
            #endregion

            #region 获取查询类别
            Neusoft.HISFC.BizLogic.Material.BizLogic.AssortType queryType = Neusoft.HISFC.BizLogic.Material.BizLogic.AssortType.Mat;
            if (this.rbtByDeptOrCompany.Checked)
            {
                queryType = Neusoft.HISFC.BizLogic.Material.BizLogic.AssortType.MatCompany;
            }
            else if (this.rbtByBill.Checked)
            {
                queryType = Neusoft.HISFC.BizLogic.Material.BizLogic.AssortType.MatBill;
            }
            else if (this.rbtByMat.Checked)
            {
                queryType = Neusoft.HISFC.BizLogic.Material.BizLogic.AssortType.Mat;
            }
            //{5C52C6F0-71C1-443f-AA35-9149564BA556}
            else if (this.rbtByType.Checked)
            {
                queryType = Neusoft.HISFC.BizLogic.Material.BizLogic.AssortType.MatType;
            }
            #endregion

            DataSet detailResult = new DataSet();

            if (this.reportProcess.QueryGeneralInfo(currentDept.ID, class2Code, class3Code, class3MeaningCode, dtBegin, dtEnd, searchKey, queryType, false, ref detailResult) < 0)
            {
                MessageBox.Show(Language.Msg("查询明细信息失败") + this.reportProcess.Err);
                return ;
            }
            if (detailResult != null && detailResult.Tables.Count > 0)
            {
                this.dtDetail = detailResult.Tables[0];
                this.SpreadDetail_Sheet1.DataSource = this.dtDetail.DefaultView;
                this.SetDetailInfoFpStyle();
                this.SetFilterVisible(this.SpreadDetail_Sheet1);
                this.neuTabControl1.SelectedTab = this.tabPage2;
            }

        }

        #endregion

        #region 接口实现

        public int PreArrange()
        {
            this.currentOper = Neusoft.FrameWork.Management.Connection.Operator;
            this.alPrivDeptList = this.managerProxy.QueryUserPriv(this.currentOper.ID, ((int)Neusoft.HISFC.BizLogic.Material.BizLogic.EnumClass2Priv.查询).ToString());
            if (this.alPrivDeptList == null)
            {
                System.Windows.Forms.MessageBox.Show(Language.Msg("获取查询权限发生错误" + this.managerProxy.Err), Language.Msg("提示"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
            if (this.alPrivDeptList.Count == 0)
            {
                System.Windows.Forms.MessageBox.Show(Language.Msg("您没有查询权限"), Language.Msg("提示"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return -1;
            }
            return 1;
        }

        #endregion


    }
}
