using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Pharmacy.Report
{
    /// <summary>
    /// [控件描述: 药品入出库通用综合查询]
    /// [创 建 人: 孙久海]
    /// [创建时间: 2010-9-2]
    /// [{DAA7351F-09C0-4af6-B244-45B637EB8109}]
    /// <版本说明>
    ///    1.用于替代版本原来的药品综合查询功能ucGeneralQuery
    ///    2.采用可自定义方式对入库、出库业务综合统计
    ///    3.SQl语句规则Pharmacy.CommonQuery.(Input/Output).(MinUnit/PackUnit).(Total/Detail)
    /// </版本说明>
    /// <修改记录>
    ///    1.exceptFilterColumns变量及属性统一变更为allFilterColumns，含义变更为全字段过滤包含字段 by Sunjh 2010-9-19 {D95280DD-9CEF-4025-B650-32CC625E1326}
    ///    2.增加报表主副标题的自定义位置拖拽和数据列顺序move拖拽 by Sunjh 2010-10-27
    ///    3.调整查询结果和报表打印的界面样式 by Sunjh 2010-10-28
    ///    4.升级药品入出库通用综合查询:1增加查询记录数显示 2增加自定义列合计功能
    /// </修改记录>
    /// <升级记录> 
    ///    {D37E2665-FEEF-4c7d-ACA6-70B14092797C}
    ///    1.增加报表主副标题的自定义位置拖拽和数据列顺序move拖拽 by Sunjh 2010-10-27
    ///    2.调整查询结果和报表打印的界面样式 by Sunjh 2010-10-28
    ///    3.增加查询记录数显示 by Sunjh 2010-11-1
    ///    4.增加自定义列合计功能 by Sunjh 2010-11-1
    /// </升级记录>
    /// </summary>
    public partial class ucCommonPharmacyQuery : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucCommonPharmacyQuery()
        {
            InitializeComponent();
        }        

        #region 变量

        private FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();

        private FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        private FS.HISFC.BizLogic.Manager.PowerLevelManager powerManager = new FS.HISFC.BizLogic.Manager.PowerLevelManager();

        private System.Collections.Hashtable hsDeptCollect = new Hashtable();

        private DataSet ds = new DataSet();

        private DataTable dt = new DataTable();

        private DataView dv = new DataView();

        private Hashtable hsColumnStyle = new Hashtable();

        /// <summary>
        /// 是否允许查询其他库存科室
        /// </summary>
        private bool isQueryOthersDept = false;

        /// <summary>
        /// 查询结果是否只读
        /// </summary>
        private bool isDataReadOnly = false;

        /// <summary>
        /// 查询结果计数是否统计过滤掉的数据
        /// </summary>
        private bool isResultCountIncludeFilter = true;

        /// <summary>
        /// 全字段过滤包含字段
        /// </summary>
        private string allFilterColumns = "";

        /// <summary>
        /// 是否显示标题
        /// </summary>
        private bool isShowTitles = false;

        /// <summary>
        /// 是否显示副表头
        /// </summary>
        private bool isShowSecHeader = false;

        /// <summary>
        /// 打印标题前缀
        /// </summary>
        private string titleStartString = "";

        /// <summary>
        /// 打印标题后缀
        /// </summary>
        private string titleEndString = "查询报表";

        /// <summary>
        /// 打印标题是否显示库存科室名称
        /// </summary>
        private bool isTitleShowDeptName = false;

        /// <summary>
        /// 控件拖拽控制
        /// </summary>
        private bool isMove = false;

        /// <summary>
        /// 控件拖拽控制X位移
        /// </summary>
        private int xx = 0;

        /// <summary>
        /// 控件拖拽控制Y位移
        /// </summary>
        private int yy = 0;

        #endregion

        #region 属性

        /// <summary>
        /// 是否允许查询其他库存科室
        /// </summary>
        [Description("是否允许查询其他库存科室"), Category("设置"), DefaultValue(false)]
        public bool IsQueryOthersDept
        {
            get 
            { 
                return isQueryOthersDept; 
            }
            set 
            { 
                isQueryOthersDept = value; 
            }
        }

        /// <summary>
        /// 查询结果是否只读
        /// </summary>
        [Description("查询结果是否只读"), Category("设置"), DefaultValue(false)]
        public bool IsDataReadOnly
        {
            get 
            {
                return isDataReadOnly; 
            }
            set 
            { 
                isDataReadOnly = value; 
            }
        }

        /// <summary>
        /// 查询结果计数是否统计过滤掉的数据
        /// </summary>
        [Description("查询结果计数是否统计过滤掉的数据"), Category("设置"), DefaultValue(true)]
        public bool IsResultCountIncludeFilter
        {
            get 
            { 
                return isResultCountIncludeFilter; 
            }
            set 
            {
                isResultCountIncludeFilter = value;
            }
        }

        /// <summary>
        /// 全字段过滤包含字段
        /// </summary>
        [Description("全字段过滤包含字段，多字段用空格间隔"), Category("设置"), DefaultValue("零售价 金额 发生数量")]
        public string AllFilterColumns
        {
            get 
            {
                return allFilterColumns;
            }
            set 
            {
                allFilterColumns = value; 
            }
        }

        /// <summary>
        /// 是否显示标题
        /// </summary>
        [Description("是否显示标题"), Category("打印设置"), DefaultValue(false)]
        public bool IsShowTitles
        {
            get 
            { 
                return isShowTitles; 
            }
            set 
            { 
                isShowTitles = value; 
            }
        }

        /// <summary>
        /// 是否显示副表头
        /// </summary>
        [Description("是否显示副表头"), Category("打印设置"), DefaultValue(false)]
        public bool IsShowSecHeader
        {
            get 
            { 
                return isShowSecHeader; 
            }
            set
            { 
                isShowSecHeader = value; 
            }
        }

        /// <summary>
        /// 打印标题前缀
        /// </summary>
        [Description("打印标题前缀"), Category("打印设置"), DefaultValue("")]
        public string TitleStartString
        {
            get 
            { 
                return titleStartString;
            }
            set 
            { 
                titleStartString = value; 
            }
        }

        /// <summary>
        /// 打印标题后缀
        /// </summary>
        [Description("打印标题后缀"), Category("打印设置"), DefaultValue("查询报表")]
        public string TitleEndString
        {
            get 
            {
                return titleEndString; 
            }
            set 
            {
                titleEndString = value; 
            }
        }

        /// <summary>
        /// 打印标题是否显示库存科室名称
        /// </summary>
        [Description("打印标题是否显示库存科室名称"), Category("打印设置"), DefaultValue(false)]
        public bool IsTitleShowDeptName
        {
            get 
            { 
                return isTitleShowDeptName; 
            }
            set 
            { 
                isTitleShowDeptName = value; 
            }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 控件初始化
        /// </summary>
        protected virtual void InitControls()
        {
            #region 初始化全部科室哈希表

            ArrayList alTempDept = managerIntegrate.GetDeptmentAllValid();            
            foreach (FS.HISFC.Models.Base.Department info in alTempDept)
            {
                hsDeptCollect.Add(info.ID, info);
            }

            #endregion

            #region 设置默认全部查询条件

            FS.FrameWork.Models.NeuObject tempObj = new FS.FrameWork.Models.NeuObject();
            tempObj.ID = "ALL";
            tempObj.Name = "全部";

            #endregion

            #region 加载库存科室

            ArrayList alStockDept = this.managerIntegrate.GetDepartment(FS.HISFC.Models.Base.EnumDepartmentType.PI);
            ArrayList alStockDeptAdd = this.managerIntegrate.GetDepartment(FS.HISFC.Models.Base.EnumDepartmentType.P);
            alStockDept.AddRange(alStockDeptAdd);
            alStockDept.Insert(0, tempObj);
            this.cbbStockDept.AddItems(alStockDept);
            this.cbbStockDept.Text = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.Name;
            if (this.isQueryOthersDept)
            {
                if (this.cbbStockDept.SelectedIndex < 0)
                {
                    this.cbbStockDept.SelectedIndex = 0;
                }
            }
            else
            {
                this.cbbStockDept.Enabled = false;
            }
            

            #endregion

            #region 加载查询业务

            ArrayList alQueryType = new ArrayList();
            FS.FrameWork.Models.NeuObject tempType = new FS.FrameWork.Models.NeuObject();
            tempType.ID = "0310";
            tempType.Name = "入库";
            alQueryType.Add(tempType);
            tempType = new FS.FrameWork.Models.NeuObject();
            tempType.ID = "0320";
            tempType.Name = "出库";
            alQueryType.Add(tempType);
            this.cbbQueryType.AddItems(alQueryType);
            this.cbbQueryType.SelectedIndex = 0;

            #endregion

            #region 加载业务子类

            ArrayList alQueryKinds = new ArrayList();
            ArrayList alTempKinds = new ArrayList();
            alQueryKinds = this.powerManager.LoadLevel3ByLevel2(this.cbbQueryType.SelectedItem.ID);
            for (int i = 0; i < alQueryKinds.Count; i++)
            {
                FS.HISFC.Models.Admin.PowerLevelClass3 pr = alQueryKinds[i] as FS.HISFC.Models.Admin.PowerLevelClass3;
                FS.FrameWork.Models.NeuObject tempLevel = new FS.FrameWork.Models.NeuObject();
                tempLevel.ID = pr.Class3MeaningCode;
                tempLevel.Name = pr.Class3MeaningName;
                alTempKinds.Add(tempLevel);
            }
            alTempKinds.Insert(0, tempObj);
            this.cbbQueryKinds.AddItems(alTempKinds);
            this.cbbQueryKinds.SelectedIndex = 0;

            #endregion

            #region 加载目标科室

            ArrayList alDept = new ArrayList();
            if (this.cbbStockDept.SelectedIndex >= 0 && this.cbbStockDept.SelectedItem.ID != "ALL" && this.cbbQueryType.SelectedIndex >= 0)
            {                                
                ArrayList alPrivInOut = new ArrayList();
                alPrivInOut = this.managerIntegrate.GetPrivInOutDeptList(this.cbbStockDept.SelectedItem.ID, this.cbbQueryType.SelectedItem.ID);

                FS.FrameWork.Models.NeuObject tempDept = new FS.FrameWork.Models.NeuObject();
                for (int i = 0; i < alPrivInOut.Count; i++)
                {
                    FS.HISFC.Models.Base.PrivInOutDept privInOutDept = alPrivInOut[i] as FS.HISFC.Models.Base.PrivInOutDept;
                    if (hsDeptCollect.ContainsKey(privInOutDept.Dept.ID))
                    {
                        tempDept = hsDeptCollect[privInOutDept.Dept.ID] as FS.HISFC.Models.Base.Department;
                        tempDept.Memo = privInOutDept.Memo;
                    }

                    alDept.Add(tempDept.Clone());
                }

                alDept.Insert(0, tempObj);
                this.cbbDept.AddItems(alDept);
                this.cbbDept.SelectedIndex = 0;
            }
            else
            {
                alDept.Insert(0, tempObj);
                this.cbbDept.AddItems(alDept);
                this.cbbDept.SelectedIndex = 0;
            }

            #endregion

            #region 设置时间控件

            this.dtpBegin.Value = DateTime.Now.Date;
            this.dtpEnd.Value = DateTime.Now.Date.AddDays(1).AddSeconds(-1);            

            #endregion

            #region 加载列类型

            this.hsColumnStyle.Clear();
            ArrayList alColumnStyle = this.managerIntegrate.GetConstantList("PHAQUERYCOLUMN");            
            for (int i = 0; i < alColumnStyle.Count; i++)
            {
                FS.HISFC.Models.Base.Const tempStyle = alColumnStyle[i] as FS.HISFC.Models.Base.Const;
                tempStyle.User01 = tempStyle.UserCode;
                tempStyle.User02 = tempStyle.WBCode;
                this.hsColumnStyle.Add(tempStyle.ID, tempStyle);
            }            

            #endregion

            #region 列表打印属性设置

            if (this.isShowTitles)
            {
                this.pnlPrintTitle.Visible = true;
            }
            else
            {
                this.pnlPrintTitle.Visible = false;
            }

            if (this.isShowSecHeader)
            {
                this.pnlReportHeader.Visible = true;
            }
            else
            {
                this.pnlReportHeader.Visible = false;
            }

            if (this.isTitleShowDeptName)
            {
                this.txtTitle.Text = this.titleStartString + this.cbbQueryType.Text + this.titleEndString + "（" + this.cbbStockDept.Text + "）";
            }
            else
            {
                this.txtTitle.Text = this.titleStartString + this.cbbQueryType.Text + this.titleEndString;
            }
            
            this.lblTimeShow.Text = this.dtpBegin.Value.ToString() + " 至 " + this.dtpEnd.Value.ToString();
            this.lblStockDept.Text = this.cbbStockDept.Text;
            this.lblShowQueryType.Text = this.cbbQueryType.Text + "类型: " + this.cbbQueryKinds.Text;            

            #endregion

            #region 界面权限控制

            if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
            {
                this.neuLabel12.Visible = true;
            }
            else
            {
                this.neuLabel12.Visible = false;
            }            

            #endregion

        }

        /// <summary>
        /// 主查询方法
        /// </summary>
        protected virtual void QueryData()
        {
            #region 生成SQL索引
            #region 查询语句集合
            //'Pharmacy.CommonQuery.Input.MinUnit.Detail',
            //'Pharmacy.CommonQuery.Input.MinUnit.Total',
            //'Pharmacy.CommonQuery.Input.PackUnit.Detail',
            //'Pharmacy.CommonQuery.Input.PackUnit.Total',
            //'Pharmacy.CommonQuery.Output.MinUnit.Detail',
            //'Pharmacy.CommonQuery.Output.MinUnit.Total',
            //'Pharmacy.CommonQuery.Output.PackUnit.Detail',
            //'Pharmacy.CommonQuery.Output.PackUnit.Total'
            #endregion
            string sqlStr = "Pharmacy.CommonQuery";

            //业务类型
            if (this.cbbQueryType.Text == "入库")
            {
                sqlStr = sqlStr + ".Input";
            }
            else if (this.cbbQueryType.Text == "出库")
            {
                sqlStr = sqlStr + ".Output";                
            }

            //显示数量单位
            if (this.rbtPackUnit.Checked)
            {
                sqlStr = sqlStr + ".PackUnit";
            }
            else
            {
                sqlStr = sqlStr + ".MinUnit";
            }

            //显示汇总or明细
            if (this.rbtTotal.Checked)
            {
                sqlStr = sqlStr + ".Total";
            }
            else
            {
                sqlStr = sqlStr + ".Detail";
            }
            #endregion

            #region 查询执行并返回结果集
            if (this.itemManager.Sql.GetSql(sqlStr, ref sqlStr) == -1)
            {
                MessageBox.Show("没有找到" + sqlStr + "字段!");
                return;
            }
            try
            {
                sqlStr = string.Format(sqlStr, this.cbbStockDept.SelectedItem.ID, this.cbbQueryKinds.SelectedItem.ID,
                    this.cbbDept.SelectedItem.ID, this.dtpBegin.Value.ToString(), this.dtpEnd.Value.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("格式化SQL语句时出错" + sqlStr + " " + ex.Message);
                return;
            }

            if (this.itemManager.ExecQuery(sqlStr, ref this.ds) == -1)
            {
                MessageBox.Show("执行查询时出错");
                return;
            }

            #endregion

            #region 输出到界面显示

            if (this.ds == null)
            {
                MessageBox.Show("查询结果为空!");
                return;
            }

            this.dt = this.ds.Tables[0];
            this.dv = new DataView(this.dt);
            this.fpDataView.DataSource = this.dv;
            this.Filter();            
            this.SetColumnStyle();            

            #endregion            
        }

        /// <summary>
        /// 表格数据过滤
        /// </summary>
        protected virtual void Filter()
        {            
            string filterStr = "(1=2 ";
            //格式化全字段过滤条件
            for (int i = 0; i < this.fpDataView.ColumnCount; i++)
            {
                if (this.allFilterColumns.Contains(this.fpDataView.Columns[i].Label))
                {
                    filterStr = filterStr + " or " + this.fpDataView.Columns[i].Label + " like '%" + this.txtFilterAllColumns.Text + "%'";
                }                
            }
            //格式化药品过滤条件
            filterStr = filterStr + ") and (药品名称 like '%" + this.txtFilterDrug.Text + "%' or 拼音码 like '%" + this.txtFilterDrug.Text + "%')";
            
            this.dv.RowFilter = filterStr;

            if (!this.isResultCountIncludeFilter)
            {
                this.ShowResultInfo(false);
            }
            else
            {
                this.ShowResultInfo(true);
            }
        }

        /// <summary>
        ///  表格列属性设置
        /// </summary>
        protected virtual void SetColumnStyle()
        {
            Hashtable hsTotalColumns = new Hashtable();
            int totalColumnsCount = 0;
            for (int i = 0; i < this.fpDataView.ColumnCount; i++)
            {
                if (this.hsColumnStyle.ContainsKey(this.fpDataView.Columns[i].Label))
                {
                    FS.FrameWork.Models.NeuObject tempStyle = this.hsColumnStyle[this.fpDataView.Columns[i].Label] as FS.FrameWork.Models.NeuObject;
                    this.fpDataView.Columns[i].Width = Convert.ToInt32(tempStyle.Name);
                    this.fpDataView.Columns[i].Visible = tempStyle.Memo == "hide" ? false : true;
                    if (tempStyle.User01 == "Text")
                    {
                        FarPoint.Win.Spread.CellType.TextCellType txtType = new FarPoint.Win.Spread.CellType.TextCellType();
                        this.fpDataView.Columns[i].CellType = txtType;
                    }
                    if (tempStyle.User01 == "Number")
                    {
                        FarPoint.Win.Spread.CellType.NumberCellType numType = new FarPoint.Win.Spread.CellType.NumberCellType();
                        this.fpDataView.Columns[i].CellType = numType;
                    }
                    if (tempStyle.User01 == "CheckBox")
                    {
                        FarPoint.Win.Spread.CellType.CheckBoxCellType chkType = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
                        this.fpDataView.Columns[i].CellType = chkType;
                    }
                    if (tempStyle.User02 == "True")
                    {
                        hsTotalColumns.Add(tempStyle.Name, i.ToString());
                    }                    
                }
                
                this.fpDataView.Columns[i].Locked = this.isDataReadOnly;
            }

            if (hsTotalColumns != null)
            {
                this.fpDataView.RowCount += 1;
                foreach (string columnIndex in hsTotalColumns.Values)
                {
                    decimal tempSum = 0;
                    for (int i = 0; i < this.fpDataView.RowCount - 1; i++)
                    {
                        tempSum += Convert.ToDecimal(this.fpDataView.Cells[i, Convert.ToInt32(columnIndex)].Text);
                    }
                    this.fpDataView.Cells[this.fpDataView.RowCount - 1, Convert.ToInt32(columnIndex)].Text = tempSum.ToString("0.00");
                }
                
            }            
        }

        /// <summary>
        /// 显示查询结果信息
        /// </summary>
        /// <param name="isIncludeFilter">是否包含过滤的数据（预留参数）</param>
        protected virtual void ShowResultInfo(bool isIncludeFilter)
        {
            int iCount = 0;
            //iCount = this.fpDataView.RowCount - 1;
            iCount = this.dt.Rows.Count;
            
            this.lblResult.Text = "本次检索数据: " + iCount.ToString() + " 条";
        }

        #endregion

        #region 工具栏

        protected override void OnLoad(EventArgs e)
        {
            this.InitControls();
            base.OnLoad(e);
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            this.QueryData();
            return base.OnQuery(sender, neuObject);
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            FS.FrameWork.WinForms.Classes.Print pp = new FS.FrameWork.WinForms.Classes.Print();
            pp.PrintPreview(5, 5, this.pnlMain);
            return base.OnPrint(sender, neuObject);
        }

        public override int Export(object sender, object neuObject)
        {
            this.neuSpread1.Export();
            return base.Export(sender, neuObject);
        }

        #endregion

        #region 事件

        private void chkShowPrintTitle_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkShowPrintTitle.Checked)
            {
                this.pnlPrintTitle.Visible = true;
                this.pnlReportHeader.Visible = true;
            }
            else
            {
                this.pnlPrintTitle.Visible = false;
                this.pnlReportHeader.Visible = false;
            }
        }

        private void cbbStockDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cbbStockDept.SelectedIndex >= 0 && this.cbbStockDept.SelectedItem.ID != "ALL" && this.cbbQueryType.SelectedIndex >= 0)
            {
                //设置默认全部查询条件
                FS.FrameWork.Models.NeuObject tempObj = new FS.FrameWork.Models.NeuObject();
                tempObj.ID = "ALL";
                tempObj.Name = "全部";
                //加载目标科室
                ArrayList alDept = new ArrayList();
                ArrayList alPrivInOut = new ArrayList();
                alPrivInOut = this.managerIntegrate.GetPrivInOutDeptList(this.cbbStockDept.SelectedItem.ID, this.cbbQueryType.SelectedItem.ID);

                FS.FrameWork.Models.NeuObject tempDept = new FS.FrameWork.Models.NeuObject();
                for (int i = 0; i < alPrivInOut.Count; i++)
                {
                    FS.HISFC.Models.Base.PrivInOutDept privInOutDept = alPrivInOut[i] as FS.HISFC.Models.Base.PrivInOutDept;
                    if (hsDeptCollect.ContainsKey(privInOutDept.Dept.ID))
                    {
                        tempDept = hsDeptCollect[privInOutDept.Dept.ID] as FS.HISFC.Models.Base.Department;
                        tempDept.Memo = privInOutDept.Memo;
                    }

                    alDept.Add(tempDept.Clone());
                }

                alDept.Insert(0, tempObj);
                this.cbbDept.AddItems(alDept);
                this.cbbDept.SelectedIndex = 0;
            }
            this.lblStockDept.Text = this.cbbStockDept.Text;
            if (this.isTitleShowDeptName)
            {
                this.txtTitle.Text = this.titleStartString + this.cbbQueryType.Text + this.titleEndString + "（" + this.cbbStockDept.Text + "）";
            }
            else
            {
                this.txtTitle.Text = this.titleStartString + this.cbbQueryType.Text + this.titleEndString;
            }
        }

        private void cbbQueryType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //设置默认全部查询条件
            FS.FrameWork.Models.NeuObject tempObj = new FS.FrameWork.Models.NeuObject();
            tempObj.ID = "ALL";
            tempObj.Name = "全部";
            //加载业务子类
            ArrayList alQueryKinds = new ArrayList();
            ArrayList alTempKinds = new ArrayList();
            alQueryKinds = this.powerManager.LoadLevel3ByLevel2(this.cbbQueryType.SelectedItem.ID);
            for (int i = 0; i < alQueryKinds.Count; i++)
            {
                FS.HISFC.Models.Admin.PowerLevelClass3 pr = alQueryKinds[i] as FS.HISFC.Models.Admin.PowerLevelClass3;
                FS.FrameWork.Models.NeuObject tempLevel = new FS.FrameWork.Models.NeuObject();
                tempLevel.ID = pr.Class3MeaningCode;
                tempLevel.Name = pr.Class3MeaningName;
                alTempKinds.Add(tempLevel);
            }
            alTempKinds.Insert(0, tempObj);
            this.cbbQueryKinds.AddItems(alTempKinds);
            this.cbbQueryKinds.SelectedIndex = 0;

            if (this.cbbStockDept.SelectedIndex >= 0 && this.cbbStockDept.SelectedItem.ID != "ALL" && this.cbbQueryType.SelectedIndex >= 0)
            {
                //加载目标科室
                ArrayList alDept = new ArrayList();
                ArrayList alPrivInOut = new ArrayList();
                alPrivInOut = this.managerIntegrate.GetPrivInOutDeptList(this.cbbStockDept.SelectedItem.ID, this.cbbQueryType.SelectedItem.ID);

                FS.FrameWork.Models.NeuObject tempDept = new FS.FrameWork.Models.NeuObject();
                for (int i = 0; i < alPrivInOut.Count; i++)
                {
                    FS.HISFC.Models.Base.PrivInOutDept privInOutDept = alPrivInOut[i] as FS.HISFC.Models.Base.PrivInOutDept;
                    if (hsDeptCollect.ContainsKey(privInOutDept.Dept.ID))
                    {
                        tempDept = hsDeptCollect[privInOutDept.Dept.ID] as FS.HISFC.Models.Base.Department;
                        tempDept.Memo = privInOutDept.Memo;
                    }

                    alDept.Add(tempDept.Clone());
                }

                alDept.Insert(0, tempObj);
                this.cbbDept.AddItems(alDept);
                this.cbbDept.SelectedIndex = 0;
            }

            if (this.isTitleShowDeptName)
            {
                this.txtTitle.Text = this.titleStartString + this.cbbQueryType.Text + this.titleEndString + "（" + this.cbbStockDept.Text + "）";
            }
            else
            {
                this.txtTitle.Text = this.titleStartString + this.cbbQueryType.Text + this.titleEndString;
            }
            this.lblShowQueryType.Text = this.cbbQueryType.Text + "类型: " + this.cbbQueryKinds.Text;
        }

        private void 排序ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.fpDataView.ActiveColumnIndex >= 0)
            {
                this.fpDataView.ActiveColumn.AllowAutoSort = !this.fpDataView.ActiveColumn.AllowAutoSort;
            }
        }

        private void 过滤ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.fpDataView.ActiveColumnIndex >= 0)
            {
                this.fpDataView.ActiveColumn.AllowAutoFilter = !this.fpDataView.ActiveColumn.AllowAutoFilter;
            }
        }

        private void neuSpread1_AutoFilteredColumn(object sender, FarPoint.Win.Spread.AutoFilteredColumnEventArgs e)
        {
            if (!this.isResultCountIncludeFilter)
            {
                int iCount = 0;
                for (int i = 0; i < this.fpDataView.RowCount; i++)
                {
                    if (!this.fpDataView.RowFilter.IsRowFilteredOut(i))
                    {
                        iCount += 1;
                    }
                }

                if (iCount > this.dt.Rows.Count)
                {
                    iCount = this.dt.Rows.Count;
                }
                
                this.lblResult.Text = "本次检索数据: " + iCount.ToString() + " 条";
            }
        }

        private void txtFilterDrug_TextChanged(object sender, EventArgs e)
        {
            this.Filter();
        }

        private void txtFilterAllColumns_TextChanged(object sender, EventArgs e)
        {
            this.Filter();
        }

        private void dtpBegin_ValueChanged(object sender, EventArgs e)
        {
            this.lblTimeShow.Text = this.dtpBegin.Value.ToString() + " 至 " + this.dtpEnd.Value.ToString();
        }

        private void dtpEnd_ValueChanged(object sender, EventArgs e)
        {
            this.lblTimeShow.Text = this.dtpBegin.Value.ToString() + " 至 " + this.dtpEnd.Value.ToString();
        }

        private void cbbQueryKinds_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lblShowQueryType.Text = this.cbbQueryType.Text + "类型: " + this.cbbQueryKinds.Text;
        }

        private void neuLabel12_Click(object sender, EventArgs e)
        {
            ucCommonQueryColumnSet ucColumnSet = new ucCommonQueryColumnSet();
            FS.FrameWork.WinForms.Classes.Function.PopShowControl(ucColumnSet);
            this.hsColumnStyle.Clear();
            ArrayList alColumnStyle = this.managerIntegrate.GetConstantList("PHAQUERYCOLUMN");
            for (int i = 0; i < alColumnStyle.Count; i++)
            {
                FS.HISFC.Models.Base.Const tempStyle = alColumnStyle[i] as FS.HISFC.Models.Base.Const;
                tempStyle.User01 = tempStyle.UserCode;
                this.hsColumnStyle.Add(tempStyle.ID, tempStyle);
            }
            this.SetColumnStyle();
        }

        private void txtTitle_TextChanged(object sender, EventArgs e)
        {
            this.lblTitle.Text = this.txtTitle.Text;
        }

        #region 控件拖拽事件

        private void lblTitle_MouseDown(object sender, MouseEventArgs e)
        {
            this.isMove = true;
            xx = e.X;
            yy = e.Y;
        }

        private void lblTitle_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.isMove)
            {
                this.lblTitle.Left += e.X - xx;
                //this.lblTitle.Top += e.Y - yy;
            }
        }

        private void lblTitle_MouseUp(object sender, MouseEventArgs e)
        {
            this.isMove = false;
        }

        private void neuLabel10_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.isMove)
            {
                this.neuLabel10.Left += e.X - xx;
                this.lblTimeShow.Left += e.X - xx;
                //this.lblTitle.Top += e.Y - yy;
            }
        }

        private void lblTimeShow_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.isMove)
            {
                this.neuLabel10.Left += e.X - xx;
                this.lblTimeShow.Left += e.X - xx;
                //this.lblTitle.Top += e.Y - yy;
            }
        }

        private void neuLabel11_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.isMove)
            {
                this.neuLabel11.Left += e.X - xx;
                this.lblStockDept.Left += e.X - xx;
            }
        }

        private void lblStockDept_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.isMove)
            {
                this.neuLabel11.Left += e.X - xx;
                this.lblStockDept.Left += e.X - xx;
                //this.lblTitle.Top += e.Y - yy;
            }
        }

        private void lblShowQueryType_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.isMove)
            {
                this.lblShowQueryType.Left += e.X - xx;
                //this.lblTitle.Top += e.Y - yy;
            }
        }

        #endregion

        #endregion

    }
}
