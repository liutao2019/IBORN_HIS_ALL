using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.Pharmacy.ShenZhen.Report
{
    public partial class ucPrivePowerReport : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucPrivePowerReport()
        {
            InitializeComponent();
        }

        #region 变量

        private FS.HISFC.BizLogic.Manager.UserPowerDetailManager priPowerMgr = new FS.HISFC.BizLogic.Manager.UserPowerDetailManager();
        public FS.FrameWork.WinForms.Classes.Print PrintObject = new FS.FrameWork.WinForms.Classes.Print();
        private FS.HISFC.BizLogic.Pharmacy.Report pharmacyReport = new FS.HISFC.BizLogic.Pharmacy.Report();

        public delegate void DelegateQueryEnd();
        public DelegateQueryEnd QueryEndHandler;

        public delegate void DelegateOperationStart(string type);
        public DelegateOperationStart OperationStartHandler;

        public delegate void DelegateOperateionEnd(string type);
        public DelegateOperateionEnd OperationEndHandler;

        public delegate void DelegateDoubleClickEnd();
        public DelegateDoubleClickEnd DetailDoubleClickEnd;

        public int maxWidth = 820;
        public bool IsNeedDataMerge = false;

        /// <summary>
        /// 是否需要合计[Tot汇总求和，Det明细求和，Both明细都需要求和，Null都不求和]
        /// </summary>
        private string sumType = "Null";

        /// <summary>
        /// 科室类别[Cls指定二级权限，Dpt指定科室类别，All全部科室]
        /// 默认0，1指定后0无效，2指定后0、1无效
        /// </summary>
        public string DeptType = "Cls";

        /// <summary>
        /// 科室不可见时是否还需要权限
        /// 二级权限赋值后自动值为true;
        /// </summary>
        private bool isJugdePriPower = false;

        /// <summary>
        /// 二级权限初始化后根据操作员更改是否可以查询数据
        /// </summary>
        private bool isHavePriPower = true;

        #endregion

        #region 属性及相关变量

        private System.Data.DataView dataView;
        private System.Data.DataView detailDataView;

        /// <summary>
        /// 汇总查询的数据
        /// </summary>
        public System.Data.DataView DataView
        {
            set
            {
                dataView = value;
            }
            get
            {
                //if (this.fpSpread1_Sheet1.DataSource.GetType() == typeof(System.Data.DataSet))
                //{
                //    return new System.Data.DataView(((System.Data.DataSet)this.fpSpread1_Sheet1.DataSource).Tables[0]);
                //}
                return this.dataView;
            }
        }

        /// <summary>
        /// 明细查询的数据
        /// </summary>
        public System.Data.DataView DetailDataView
        {
            set
            {
                detailDataView = value;
            }
            get
            {
                //if (this.fpSpread1_Sheet2.DataSource.GetType() == typeof(System.Data.DataSet))
                //{
                //    return new System.Data.DataView(((System.Data.DataSet)this.fpSpread1_Sheet2.DataSource).Tables[0]);
                //}
                return detailDataView;
            }
        }

        /// <summary>
        /// 汇总过滤
        /// </summary>
        private string[] filters;

        /// <summary>
        /// 汇总过滤
        /// </summary>
        public string[] Filters
        {
            get
            {
                return filters;
            }
            set
            {
                filters = value;
            }
        }

        /// <summary>
        /// 明细过滤
        /// </summary>
        private string[] detailFilters;

        /// <summary>
        /// 明细过滤
        /// </summary>
        public string[] DetailFilters
        {
            get
            {
                return detailFilters;
            }
            set
            {
                detailFilters = value;
            }
        }

        /// <summary>
        /// 二级权限数组
        /// </summary>
        private string[] priClassTwos;

        /// <summary>
        /// 二级权限数组[只写]
        /// </summary>
        public string[] PriClassTwos
        {
            set
            {
                this.priClassTwos = value;
                this.isJugdePriPower = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private string[] titles;

        /// <summary>
        /// 标题
        /// 如果没有附加标题，传入空
        /// </summary>
        public string[] Titles
        {
            set
            {
                this.titles = value;
            }
        }

        /// <summary>
        /// 是否需要附加标题[只写]
        /// </summary>
        public bool IsNeedAdditionTitle
        {
            set
            {
                this.panelAdditionTitle.Visible = value;
                if (!value)
                {
                    this.panelTitle.Height = this.lbMainTitle.Height;
                }
                else
                {
                    this.panelTitle.Height = this.lbMainTitle.Height + this.lbAdditionTitleMid.Height + 1;
                }
            }
        }

        /// <summary>
        /// 时间间隔[天数]
        /// </summary>
        private int daysSpan = 0;

        /// <summary>
        /// 时间间隔[只写][天数]
        /// </summary>
        public int DaySpan
        {
            set
            {
                this.daysSpan = value;
            }
        }

        /// <summary>
        /// 是否需要将时间设置为整点
        /// </summary>
        private bool isNeedIntedDays = true;

        /// <summary>
        /// 是否需要将时间设置为整点[只写]
        /// </summary>
        public bool IsNeedIntedDays
        {
            set
            {
                this.isNeedIntedDays = value;
            }
        }

        /// <summary>
        /// 是否将科室作为查询条件
        /// </summary>
        public bool IsDeptAsCondition
        {
            set
            {
                this.panelDept.Visible = value;
            }
        }

        /// <summary>
        /// 是否将时间作为查询条件
        /// </summary>
        public bool IsTimeAsCondition
        {
            set
            {
                this.panelTime.Visible = value;
            }
        }
        /// <summary>
        /// 科室中是否可以用“全部”查询所有权限科室数据
        /// </summary>
        private bool isAllowAllDept = false;

        /// <summary>
        /// 科室中是否可以用“全部”查询所有权限科室数据[只写]
        /// </summary>
        public bool IsAllowAllDept
        {
            set
            {
                this.isAllowAllDept = value;
            }
        }

        /// <summary>
        /// 科室类别
        /// </summary>
        private FS.HISFC.Models.Base.EnumDepartmentType[] deptTypes;

        /// <summary>
        /// 科室类别
        /// 指定类别后二级权限无效
        /// </summary>
        public FS.HISFC.Models.Base.EnumDepartmentType[] DeptTypes
        {
            set
            {
                this.deptTypes = value;
                if (DeptType == "Cls")
                {
                    this.DeptType = "Dpt";
                }
            }

        }

        /// <summary>
        /// 初始化时是否用默认条件查询
        /// </summary>
        private bool queryDataWhenInit = true;

        /// <summary>
        /// 初始化时是否用默认条件查询[只写]
        /// </summary>
        public bool QueryDataWhenInit
        {
            set
            {
                this.queryDataWhenInit = value;
            }
        }

        /// <summary>
        /// SQL数组
        /// 如果有明细，这个将是汇总查询sql
        /// </summary>
        private string[] sqlIndexs;

        /// <summary>
        /// SQL数组
        /// 如果有明细，这个将是汇总查询sql
        /// </summary>
        public string[] SQLIndexs
        {
            set
            {
                this.sqlIndexs = value;
            }
        }

        /// <summary>
        /// SQLStr字符串
        /// </summary>
        private string sqlString;

        /// <summary>
        /// SQLStr字符串
        /// </summary>
        public string SQLString
        {
            set
            {
                this.sqlString = value;
            }
        }

        /// <summary>
        /// SQLOne字符串
        /// </summary>
        private string sqlStrOne;

        /// <summary>
        /// SQLOne字符串
        /// </summary>
        public string SQLStrOne
        {
            set
            {
                this.sqlStrOne = value;
            }
        }

        /// <summary>
        /// SQLTwo字符串
        /// </summary>
        private string sqlStrTwo;

        /// <summary>
        /// SQLTwo字符串
        /// </summary>
        public string SQLStrTwo
        {
            set
            {
                this.sqlStrTwo = value;
            }
        }

        /// <summary>
        /// 是否需要查询明细
        /// </summary>
        private bool isNeedDetailData = false;

        /// <summary>
        /// 是否需要查询明细
        /// </summary>
        public bool IsNeedDetailData
        {
            set
            {
                this.isNeedDetailData = value;
                if (!value)
                {
                    this.fpSpread1.Sheets.Remove(this.fpSpread1_Sheet2);
                }
            }
        }

        /// <summary>
        /// SQL数组
        /// 明细查询sql
        /// </summary>
        private string[] detailSQLIndexs;

        /// <summary>
        /// SQL数组
        /// 明细查询sql
        /// </summary>
        public string[] DetailSQLIndexs
        {
            set
            {
                this.detailSQLIndexs = value;
            }
        }

        /// <summary>
        /// 是否需要附加条件
        /// </summary>
        private bool isNeedAdditionConditions = false;

        /// <summary>
        /// 是否需要附加条件[只写]
        /// </summary>
        public bool IsNeedAdditionConditions
        {
            set
            {
                this.isNeedAdditionConditions = value;
            }
        }

        /// <summary>
        /// 查询条件[科室、起始时间][只读]
        /// </summary>
        public string[] QueryConditions
        {
            get
            {
                return this.GetQueryConditions();
            }
        }

        /// <summary>
        /// 附加查询条件
        /// </summary>
        private string[] queryAdditionConditions;

        /// <summary>
        /// 附加查询条件
        /// 需要加入科室、时间
        /// </summary>
        public string[] QueryAdditionConditions
        {
            set
            {
                this.queryAdditionConditions = value;
            }
        }

        /// <summary>
        /// 明细数据查询方式[C固定值 R从FarPoint中的活动行取，这两个用于双击 O和汇总同步查询]
        /// </summary>
        private string detailDataQueryType = "R";

        /// <summary>
        /// 明细数据查询方式[C固定值 R从FarPoint中的活动行取]
        /// </summary>
        public string DetailDataQueryType
        {
            set
            {
                detailDataQueryType = value;
            }
        }

        /// <summary>
        /// 明细查询时作为条件的FarPint列索引
        /// </summary>
        private int[] queryConditionColIndexs;

        /// <summary>
        /// 明细查询时作为条件的FarPint列索引
        /// </summary>
        public int[] QueryConditionColIndexs
        {
            set
            {
                queryConditionColIndexs = value;
                this.detailDataQueryType = "R";
            }
        }

        /// <summary>
        /// 附加查询条件
        /// </summary>
        private string[] detailConditions;

        /// <summary>
        /// 附加查询条件
        /// 需要加入科室、时间
        /// </summary>
        public string[] DetailConditions
        {
            set
            {
                this.detailConditions = value;
            }
        }

        /// <summary>
        /// 打印是否需要预览
        /// </summary>
        private bool isNeedPreView = true;

        /// <summary>
        /// 打印是否需要预览[只写][默认需要]
        /// </summary>
        public bool IsNeedPreView
        {
            set
            {
                isNeedPreView = value;
            }
        }

        /// <summary>
        /// 合计列的索引
        /// </summary>
        private int[] sumColIndexs;

        /// <summary>
        /// 合计列的索引
        /// </summary>
        public int[] SumColIndexs
        {
            set
            {
                this.sumColIndexs = value;
                if (this.sumType == "Det")
                {
                    this.sumType = "Both";
                }
                else if (this.sumType == "Null")
                {
                    this.sumType = "Tot";
                }
            }
        }

        /// <summary>
        /// 合计列的索引
        /// </summary>
        private int[] sumDetailColIndexs;

        /// <summary>
        /// 合计列的索引
        /// </summary>
        public int[] SumDetailColIndexs
        {
            set
            {
                this.sumDetailColIndexs = value;
                if (this.sumType == "Tot")
                {
                    this.sumType = "Both";
                }
                else if (this.sumType == "Null")
                {
                    this.sumType = "Det";
                }
            }
        }

        /// <summary>
        /// 合并数据时参考sheet的合并依据
        /// 即其他sheet根据是否与此列的值相同交叉对应
        /// </summary>
        private int mergeDataColIndex = 0;

        /// <summary>
        /// 合并数据时参考sheet的合并依据[只读，默认0]
        /// eg：参考sheet[0]的0列[MergeDataColIndex=0]存放科室代码，
        /// 那么sheet[1]中也应该有科室代码列，而且代码相同行的合并到farPoint中的同一行
        /// </summary>
        public int MergeDataColIndex
        {
            set
            {
                mergeDataColIndex = value;
            }
        }

        /// <summary>
        /// 过滤方式[Tot过滤汇总 Det过滤明细 Both两者过滤 Null不过滤]
        /// </summary>
        string filerType = "Null";

        /// <summary>
        /// 过滤方式[Tot过滤汇总 Det过滤明细 Both两者过滤 Null不过滤]
        /// </summary>
        public string FilerType
        {
            set
            {
                filerType = value;
                if (value == "Null")
                {
                    this.panelFilter.Visible = false;
                }
                else
                {
                    this.panelFilter.Visible = true;
                }
            }
        }

        /// <summary>
        /// 是否自动纸张
        /// </summary>
        private bool isAutoPaper = true;

        /// <summary>
        /// 是否自动纸张
        /// </summary>
        public bool IsAutoPaper
        {
            get
            {
                return isAutoPaper;
            }
            set
            {
                isAutoPaper = value;
            }
        }

        #endregion

        #region 方法

        #region 私有

        /// <summary>
        /// 根据二级权限初始化查寻科室
        /// </summary>
        /// <returns>-1失败 0成功</returns>
        private int initDeptByPriPower()
        {
            //科室不可见时表示不用
            if (!this.panelDept.Visible && !isJugdePriPower)
            {
                return 0;
            }
            if (this.priClassTwos == null)
            {
                MessageBox.Show("二级权限数组没有赋值\n请使用属性PriClassTwos[字符串]");
                return -1;
            }

            FS.HISFC.BizLogic.Manager.Department deptMgr = new FS.HISFC.BizLogic.Manager.Department();

            //哈希表用于避免科室重复添加
            System.Collections.Hashtable hsDept = new Hashtable();

            //所有科室，当有多个二级权限时，可能是重复的
            ArrayList alDept = new ArrayList();

            if (this.DeptType == "All")
            {
                alDept = deptMgr.GetDeptmentAll();
            }
            else if (this.DeptType == "Dpt")
            {
                foreach (FS.HISFC.Models.Base.EnumDepartmentType deptType in this.deptTypes)
                {
                    alDept.AddRange(deptMgr.GetDeptment(deptType));
                }
            }
            else
            {
                //每一二级权限对应科室
                System.Collections.Generic.List<FS.FrameWork.Models.NeuObject> alTmpDept;

                //根据二级权限获取科室信息
                foreach (string classTwoCode in priClassTwos)
                {
                    alTmpDept = new System.Collections.Generic.List<FS.FrameWork.Models.NeuObject>();
                    alTmpDept = this.priPowerMgr.QueryUserPriv(priPowerMgr.Operator.ID, classTwoCode);
                    if (alTmpDept == null || alTmpDept.Count == 0)
                    {
                        continue;
                    }
                    alDept.AddRange(alTmpDept);
                }
                //移除重复的科室
                foreach (FS.FrameWork.Models.NeuObject dept in alDept)
                {
                    if (!hsDept.Contains(dept.ID))
                    {
                        hsDept.Add(dept.ID, null);
                    }
                    else
                    {
                        alDept.Remove(dept);
                    }
                }
            }

            FS.FrameWork.Models.NeuObject deptTmp;

            //添加“全部”
            if (alDept.Count > 0)
            {
                if (this.isAllowAllDept)
                {
                    deptTmp = new FS.FrameWork.Models.NeuObject();
                    deptTmp.ID = "All";
                    deptTmp.Name = "全部";
                    alDept.Insert(0, deptTmp);
                }
            }

            //如果没有权限
            else
            {
                deptTmp = new FS.FrameWork.Models.NeuObject();
                deptTmp.ID = "Null";
                deptTmp.Name = "您没有可查询的科室";
                alDept.Insert(0, deptTmp);
                this.isHavePriPower = false;
            }

            //邦定
            this.cmbDept.DataSource = alDept;
            this.cmbDept.ValueMember = "ID";
            this.cmbDept.DisplayMember = "Name";

            return 0;
        }

        /// <summary>
        /// 初始化查询时间
        /// </summary>
        /// <returns>-1失败 0成功</returns>
        private int initTime()
        {
            //时间不可见，不处理
            if (!this.panelTime.Visible)
            {
                return 0;
            }
            System.DateTime dt = this.priPowerMgr.GetDateTimeFromSysDateTime();
            if (this.isNeedIntedDays)
            {
                this.dtEnd.Value = new DateTime(dt.Year, dt.Month, dt.Day, 23, 59, 59);
                this.dtStart.Value = this.dtEnd.Value.AddSeconds(1);
            }
            else
            {
                this.dtEnd.Value = dt;
            }
            this.dtStart.Value = this.dtStart.Value.AddDays(-(this.daysSpan + 1));
            return 0;
        }

        /// <summary>
        /// 标题初始化
        /// </summary>
        /// <returns>-1失败 0成功</returns>
        private int initTitle()
        {
            try
            {
                //标题不可见不处理
                if (!this.panelTitle.Visible)
                {
                    return 0;
                }
                this.lbMainTitle.Text = titles[0];
                this.lbAdditionTitleLeft.Text = titles[1];
                this.lbAdditionTitleMid.Text = titles[2];
                this.lbAdditionTitleRight.Text = titles[3];
            }
            catch (Exception ex)
            {
                MessageBox.Show("标题赋值不正确\n请传入1个主标题，3个附加标题[可以为空]>>" + ex.Message);
                return -1;
            }
            return 0;
        }

        /// <summary>
        /// 查询数据
        /// 如果有明细查询，这个作为汇总查询
        /// 明细通过双击fp实现
        /// </summary>
        /// <returns>-1失败 0成功</returns>
        private int queryData()
        {
            if (this.sqlIndexs == null || this.sqlIndexs.Length == 0)
            {
                MessageBox.Show("查询的SQL语句索引不正确\n请将SqlIndexs属性赋值");
                return -1;
            }
            System.Data.DataSet ds = new DataSet();
            if (!isHavePriPower)
            {
                MessageBox.Show("您没有权限，您可以将二级权限代码：" + this.priClassTwos[0] + "报给信息科以便协助解决！");
                return 0;
            }

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在查询，请稍候...");
            Application.DoEvents();
            if (this.IsNeedDataMerge)
            {

                if (this.mergeQuery() == -1)
                {
                    return -1;
                }
            }
            else
            {
                if (this.isNeedAdditionConditions)
                {
                    if (this.priPowerMgr.ExecQuery(this.sqlIndexs, ref ds, this.queryAdditionConditions) == -1)
                    {
                        MessageBox.Show("执行sql发生错误>>" + this.priPowerMgr.Err);
                        return -1;
                    }
                }
                else if (this.priPowerMgr.ExecQuery(this.sqlIndexs, ref ds, this.QueryConditions) == -1)
                {
                    MessageBox.Show("执行sql发生错误>>" + this.priPowerMgr.Err);
                    return -1;
                }
            }
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            if (ds == null || ds.Tables.Count == 0)
            {
                return -1;
            }

            this.fpSpread1_Sheet1.DataSource = ds;
            this.sumTot();
            ds.AcceptChanges();
            this.DataView = new System.Data.DataView(ds.Tables[0]);
            this.fpSpread1_Sheet1.DataSource = this.DataView;

            if (this.detailDataQueryType == "O")
            {
                this.queryDetailData();
            }
            return 0;
        }

        /// <summary>
        /// 复合条件查询数据
        /// </summary>
        /// <returns>-1失败 0成功</returns>
        private int queryCompoundData()
        {
            if (this.sqlIndexs == null || this.sqlIndexs.Length == 0)
            {
                MessageBox.Show("查询的SQL语句索引不正确\n请将SqlIndexs属性赋值");
                return -1;
            }
            System.Data.DataSet ds = new DataSet();
            if (!isHavePriPower)
            {
                MessageBox.Show("您没有权限，您可以将二级权限代码：" + this.priClassTwos[0] + "报给信息科以便协助解决！");
                return 0;
            }

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在查询，请稍候...");
            Application.DoEvents();
            ds = this.pharmacyReport.PharmacyReportQueryBaseOne(this.sqlString, this.sqlStrOne, this.sqlStrTwo, this.queryAdditionConditions);
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            if (ds == null || ds.Tables.Count == 0)
            {
                return -1;
            }
            this.DataView = new DataView(ds.Tables[0]);
            this.fpSpread1_Sheet1.DataSource = this.DataView;
            this.sumTot();
            if (this.detailDataQueryType == "O")
            {
                this.queryDetailData();
            }
            return 0;
        }

        /// <summary>
        /// 查询所有需要合并的数据
        /// </summary>
        /// <returns></returns>
        private int mergeQuery()
        {
            FarPoint.Win.Spread.SheetView sv;
            System.Data.DataSet dsTmp;
            System.Collections.Generic.List<FarPoint.Win.Spread.SheetView> sheets = new System.Collections.Generic.List<FarPoint.Win.Spread.SheetView>();
            foreach (string sql in this.sqlIndexs)
            {
                dsTmp = new DataSet();
                sv = new FarPoint.Win.Spread.SheetView();
                if (this.isNeedAdditionConditions)
                {
                    if (this.priPowerMgr.ExecQuery(sql, ref dsTmp, this.queryAdditionConditions) == -1)
                    {
                        MessageBox.Show("执行sql发生错误>>" + this.priPowerMgr.Err);
                        return -1;
                    }
                }
                else if (this.priPowerMgr.ExecQuery(sql, ref dsTmp, this.QueryConditions) == -1)
                {
                    MessageBox.Show("执行sql发生错误>>" + this.priPowerMgr.Err);
                    return -1;
                }
                sv.DataSource = dsTmp;
                sheets.Add(sv);
            }
            return this.mergeSheet(sheets);
        }

        /// <summary>
        /// 合并数据[完成交叉报表的功能]
        /// </summary>
        /// <param name="sheets"></param>
        /// <returns></returns>
        private int mergeSheet(System.Collections.Generic.List<FarPoint.Win.Spread.SheetView> sheets)
        {
            if (sheets == null || sheets.Count == 0)
            {
                return -1;
            }

            //第一个sheet作为参考，其他sheet的行数据应该与第一sheet一致
            System.Collections.Hashtable hsKey = new Hashtable();

            //第一个sheet作为参考
            int maxRowNo = sheets[0].RowCount;
            this.fpSpread1_Sheet1.RowCount = maxRowNo;
            this.fpSpread1_Sheet1.ColumnCount = 500;
            int maxColNo = 0;

            foreach (FarPoint.Win.Spread.SheetView sheet in sheets)
            {
                //列循环
                for (int colIndex = 0; colIndex < sheet.ColumnCount; colIndex++)
                {
                    //不是第一个sheet，关键值列0跳过
                    if (sheet != sheets[0] && colIndex == 0)
                    {
                        continue;
                    }
                    //动态增加一列
                    //this.fpSpread1_Sheet1.AddColumns(this.fpSpread1_Sheet1.ColumnCount, 1);
                    //行循环，行数不能多于参考sheet行数
                    for (int rowIndex = 0; rowIndex < sheet.RowCount && rowIndex < maxRowNo; rowIndex++)
                    {
                        //取得关键值
                        if (colIndex == mergeDataColIndex)
                        {
                            if (sheet == sheets[0])
                            {
                                if (!hsKey.Contains(sheet.Cells[rowIndex, colIndex].Value.ToString()))
                                {
                                    //记录了关键值对应的行
                                    hsKey.Add(sheet.Cells[rowIndex, colIndex].Value.ToString(), rowIndex);
                                }
                                //第一sheet的关键值加入farPoint
                                this.fpSpread1_Sheet1.Cells[rowIndex, maxColNo].Value = sheet.Cells[rowIndex, colIndex].Value;
                            }
                            continue;
                        }
                        //不是关键值列，加入farPoint
                        //关键是找到对应的行，这个在哈希表中
                        if (hsKey.Contains(sheet.Cells[rowIndex, 0].Value.ToString()))
                        {
                            int row = (int)hsKey[sheet.Cells[rowIndex, 0].Value.ToString()];
                            this.fpSpread1_Sheet1.Cells[row, maxColNo].Value = sheet.Cells[rowIndex, colIndex].Value;
                        }
                        else if (sheet == sheets[0])
                        {
                            //没有找到行
                            this.fpSpread1_Sheet1.Cells[rowIndex, maxColNo].Value = sheet.Cells[rowIndex, colIndex].Value;
                        }
                    }
                    maxColNo++;
                    if (this.fpSpread1_Sheet1.ColumnCount < maxColNo)
                    {
                        this.fpSpread1_Sheet1.AddColumns(this.fpSpread1_Sheet1.ColumnCount - 1, 1);
                    }
                }
            }
            this.fpSpread1_Sheet1.ColumnCount = maxColNo;
            return 0;
        }

        private string[] getDetailParm()
        {
            if (this.detailDataQueryType == "R")
            {

                int activeRowIndex = this.fpSpread1_Sheet1.ActiveRow.Index;
                string[] parm;
                int totLenth = this.QueryConditions.Length;
                parm = new string[totLenth + queryConditionColIndexs.Length];
                int index = 0;
                foreach (string totParm in this.QueryConditions)
                {
                    parm[index] = totParm;
                    index++;
                }
                foreach (int colIndex in this.queryConditionColIndexs)
                {
                    string curColValue = this.fpSpread1_Sheet1.Cells[activeRowIndex, colIndex].Text.Trim();
                    parm[index] = curColValue;
                    index++;
                }
                return parm;
            }
            if (this.detailDataQueryType == "O")
            {
                return this.QueryConditions;
            }
            return this.detailConditions;
        }
        /// <summary>
        /// 查询数据
        /// 如果有明细查询，这个作为汇总查询
        /// 明细通过双击fp实现
        /// </summary>
        /// <returns>-1失败 0成功</returns>
        private int queryDetailData()
        {
            if (!this.isNeedDetailData)
            {
                return 0;
            }
            if (this.detailSQLIndexs == null || this.detailSQLIndexs.Length == 0)
            {
                MessageBox.Show("查询的SQL语句索引不正确\n请将DetailSQLIndexs属性赋值");
                return -1;
            }
            System.Data.DataSet ds = new DataSet();
            string[] parm = this.getDetailParm();
            if (this.priPowerMgr.ExecQuery(this.detailSQLIndexs, ref ds, parm) == -1)
            {
                return -1;
            }
            if (ds == null || ds.Tables.Count == 0)
            {
                return -1;
            }
            if (this.fpSpread1.Sheets.Count == 1)
            {
                this.fpSpread1.Sheets.Add(this.fpSpread1_Sheet2);
            }

            this.fpSpread1_Sheet2.DataSource = ds;
            this.sumDetail();
            ds.AcceptChanges();
            this.DetailDataView = new System.Data.DataView(ds.Tables[0]);
            this.fpSpread1_Sheet2.DataSource = this.DetailDataView;
            return 0;
        }

        /// <summary>
        /// 数据导出
        /// </summary>
        private int exportData()
        {
            try
            {
                string fileName = "";
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.DefaultExt = ".xls";
                dlg.Filter = "Microsoft Excel 工作薄 (*.xls)|*.*";
                DialogResult result = dlg.ShowDialog();
                if (result == DialogResult.OK)
                {
                    fileName = dlg.FileName;
                    this.fpSpread1.SaveExcel(fileName, FarPoint.Win.Spread.Model.IncludeHeaders.ColumnHeadersCustomOnly);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("导出数据发生错误>>" + ex.Message);
                return -1;
            }
            return 0;
        }

        /// <summary>
        /// 打印数据
        /// </summary>
        /// <returns></returns>
        private int printData()
        {
            int width = this.Width;
            this.Dock = System.Windows.Forms.DockStyle.None;
            if (this.isAutoPaper)
            {
                PrintObject.SetPageSize(this.getPaperSize());
            }
            if (this.isNeedPreView)
            {
                PrintObject.PrintPreview(resetReportLocation(), 20, this.panelPrint);
            }
            else
            {
                PrintObject.PrintPage(resetReportLocation(), 20, this.panelPrint);
            }
            this.Width = width;
            this.Dock = System.Windows.Forms.DockStyle.Fill;

            return 0;
        }

        /// <summary>
        /// 打印数据
        /// </summary>
        /// <param name="paperName">纸张名称</param>
        /// <param name="paperWidth">宽度</param>
        /// <param name="paperHeight">高度</param>
        /// <returns></returns>
        private int printData(string paperName, int paperWidth, int paperHeight)
        {
            int width = this.Width;
            this.Dock = System.Windows.Forms.DockStyle.None; PrintObject.IsResetPage = true;
            PrintObject.SetPageSize(new System.Drawing.Printing.PaperSize(paperName, paperWidth, paperHeight));
            PrintObject.PrintPreview(resetReportLocation(), 20, this.panelPrint);
            this.Width = width;
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            return 0;
        }
        /// <summary>
        /// 出库单的纸张高度设置
        /// 默认情况下是三行出库数据的高度
        /// </summary>
        private System.Drawing.Printing.PaperSize getPaperSize()
        {
            System.Drawing.Printing.PaperSize paperSize = new System.Drawing.Printing.PaperSize();
            paperSize.PaperName = "ckd";
            try
            {
                int width = 800;
                int curHeight = this.Height;
                int addHeight = 0;
                int additionAddHeight = 0;
                if (this.fpSpread1.ActiveSheet.RowCount > 0)
                {
                    addHeight = (this.fpSpread1.ActiveSheet.RowCount - 1) *
                        (int)this.fpSpread1.ActiveSheet.Rows[0].Height;

                    //Epson 300K+ 额外增加高度 = 三行farPoint高度
                    additionAddHeight = 3 * (int)this.fpSpread1.ActiveSheet.Rows[0].Height;
                }

                paperSize.Width = width;
                paperSize.Height = (addHeight + curHeight + additionAddHeight);
            }
            catch (Exception ex)
            {
                MessageBox.Show("设置纸张出错>>" + ex.Message);
            }
            return paperSize;
        }
        /// <summary>
        /// 如果FarPoint的宽度小于uc控件，返回页边距，使其居中
        /// 否则，返回页边距20，这需要调整FarPoint
        /// </summary>
        /// <returns></returns>
        private int resetReportLocation()
        {
            this.Width = this.maxWidth;
            float sheetWidth = 0;
            FarPoint.Win.Spread.SheetView sv = this.fpSpread1.ActiveSheet;
            for (int index = 0; index < sv.ColumnCount; index++)
            {
                sheetWidth += sv.Columns[index].Width;
            }
            if ((int)sheetWidth < this.Width)
            {
            }
            return 20;

        }

        /// <summary>
        /// 汇总求和
        /// </summary>
        /// <returns></returns>
        private int sumTot()
        {
            try
            {
                if (this.sumColIndexs == null || this.sumColIndexs.Length == 0 || this.fpSpread1_Sheet1.RowCount == 0)
                {
                    return 0;
                }
                if (this.fpSpread1_Sheet1.RowCount == 1 && this.fpSpread1_Sheet1.Cells[0, 0].Value.ToString() == "合计：")
                {
                    foreach (int colIndex in this.sumColIndexs)
                    {
                        if (colIndex < this.fpSpread1_Sheet1.ColumnCount)
                        {
                            this.fpSpread1_Sheet1.Cells[0, colIndex].Formula = null;
                            this.fpSpread1_Sheet1.Cells[0, colIndex].Value = 0;
                        }
                    }
                    return 0;
                }

                if (this.sumType == "Tot" || this.sumType == "Both")
                {
                    if (this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.RowCount - 1, 0].Value.ToString() != "合计：")
                    {
                        this.fpSpread1_Sheet1.AddRows(this.fpSpread1_Sheet1.RowCount, 1);
                    }
                    foreach (int colIndex in this.sumColIndexs)
                    {
                        if (colIndex > this.fpSpread1_Sheet1.ColumnCount - 1 || this.fpSpread1_Sheet1.RowCount == 1)
                        {
                            break;
                        }
                        string colLetter = ((char)(colIndex + 65)).ToString();
                        string formul = "SUM(" + colLetter + "1:" + colLetter + (this.fpSpread1_Sheet1.RowCount - 1).ToString() + ")";

                        this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.RowCount - 1, colIndex].Formula = formul;
                    }
                }
                else
                {
                    return 0;
                }
                if (this.sumColIndexs[0] != 0)
                {
                    this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.RowCount - 1, 0].Value = "合计：";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("汇总求和错误>>" + ex.Message);
                return -1;
            }
            return 0;

        }

        /// <summary>
        /// 明细求和
        /// </summary>
        /// <returns></returns>
        private int sumDetail()
        {
            try
            {
                if (this.sumDetailColIndexs == null || this.sumDetailColIndexs.Length == 0 || this.fpSpread1_Sheet2.RowCount == 0)
                {
                    return 0;
                }
                if (this.fpSpread1_Sheet2.RowCount == 1 && this.fpSpread1_Sheet2.Cells[0, 0].Value.ToString() == "合计：")
                {
                    foreach (int colIndex in this.sumDetailColIndexs)
                    {
                        if (colIndex < this.fpSpread1_Sheet2.ColumnCount)
                        {
                            this.fpSpread1_Sheet2.Cells[0, colIndex].Formula = null;
                            this.fpSpread1_Sheet2.Cells[0, colIndex].Value = 0;
                        }
                    }
                    return 0;
                }
                if (this.sumType == "Det" || this.sumType == "Both")
                {
                    this.fpSpread1_Sheet2.AddRows(this.fpSpread1_Sheet2.RowCount, 1);
                    foreach (int colIndex in this.sumDetailColIndexs)
                    {
                        if (colIndex > this.fpSpread1_Sheet2.ColumnCount - 1 || this.fpSpread1_Sheet2.RowCount == 1)
                        {
                            break;
                        }
                        string colLetter = ((char)(colIndex + 65)).ToString();
                        this.fpSpread1_Sheet2.Cells[this.fpSpread1_Sheet2.RowCount - 1, colIndex].Formula
                        = "SUM(" + colLetter + "1:" + colLetter + (this.fpSpread1_Sheet2.RowCount - 1).ToString() + ")";
                    }
                }
                else
                {
                    return 0;
                }
                if (this.sumDetailColIndexs[0] != 0)
                {
                    this.fpSpread1_Sheet2.Cells[this.fpSpread1_Sheet2.RowCount - 1, 0].Value = "合计：";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("明细求和错误>>" + ex.Message);
                return -1;
            }
            return 0;

        }
        #endregion

        #region 公有

        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        public int Init()
        {
            this.panelPrint.BackColor = System.Drawing.Color.White;

            //科室
            if (this.initDeptByPriPower() == -1)
            {
                return -1;
            }

            //查询时间
            if (this.initTime() == -1)
            {
                return -1;
            }

            //标题
            if (this.initTitle() == -1)
            {
                return -1;
            }

            this.fpSpread1.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(fpSpread1_CellDoubleClick);
            this.txtFilter.TextChanged += new EventHandler(txtFilter_TextChanged);

            //默认条件查询
            if (this.queryDataWhenInit)
            {
                this.queryData();
            }

            return 0;


        }

        /// <summary>
        /// 获取查询条件
        /// 附加条件在外部实现
        /// </summary>
        /// <returns></returns>
        public string[] GetQueryConditions()
        {
            if (this.panelDept.Visible && this.panelTime.Visible)
            {
                string[] parm = { "", "", "" };
                if (this.cmbDept.SelectedValue != null && this.cmbDept.SelectedValue.ToString() != "")
                {
                    parm[0] = this.cmbDept.SelectedValue.ToString();
                }
                parm[1] = this.dtStart.Value.ToString();
                parm[2] = this.dtEnd.Value.ToString();

                return parm;
            }
            if (!this.panelDept.Visible && this.panelTime.Visible)
            {
                string[] parm = { "", "" };
                parm[0] = this.dtStart.Value.ToString();
                parm[1] = this.dtEnd.Value.ToString();

                return parm;
            }
            if (this.panelDept.Visible && !this.panelTime.Visible)
            {
                string[] parm = { "" };
                parm[0] = this.cmbDept.SelectedValue.ToString();

                return parm;
            }
            string[] parmNull = { "Null", "Null", "Null" };
            return parmNull;
        }

        /// <summary>
        /// 默认设置
        /// 1、没有附加标题
        /// 2、不需要明细数据
        /// 3、farPoint锁定
        /// 4、查询条件panel一行控件
        /// </summary>
        public void SetDefaultSetting()
        {
            this.IsNeedAdditionTitle = false;
            this.IsNeedDetailData = false;
            this.fpSpread1_Sheet1.DefaultStyle.Locked = true;
            this.fpSpread1_Sheet2.DefaultStyle.Locked = true;
            this.FilerType = "Null";
            this.fpSpread1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.fpSpread1_Sheet2.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.fpSpread1_Sheet1.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            this.fpSpread1_Sheet2.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;

            this.panelQueryConditions.Height = 40;
        }

        /// <summary>
        /// 数据查询
        /// </summary>
        /// <returns></returns>
        public int QueryData()
        {
            if (this.OperationStartHandler != null)
            {
                this.OperationStartHandler("query");
            }
            int parm = this.queryData();
            if (this.OperationEndHandler != null)
            {
                this.OperationEndHandler("query");
            }
            return parm;
        }

        /// <summary>
        /// 数据查询
        /// </summary>
        /// <returns></returns>
        public int QueryCompoundData()
        {
            if (this.OperationStartHandler != null)
            {
                this.OperationStartHandler("query");
            }
            int parm = this.queryCompoundData();
            if (this.OperationEndHandler != null)
            {
                this.OperationEndHandler("query");
            }
            return parm;
        }

        /// <summary>
        /// 数据导出
        /// </summary>
        /// <returns></returns>
        public int ExportData()
        {
            if (this.OperationStartHandler != null)
            {
                this.OperationStartHandler("export");
            }
            int parm = this.exportData();
            if (this.OperationEndHandler != null)
            {
                this.OperationEndHandler("export");
            }
            return parm;
        }

        /// <summary>
        /// 打印数据
        /// </summary>
        /// <returns></returns>
        public int PrintData()
        {
            if (this.OperationStartHandler != null)
            {
                this.OperationStartHandler("print");
            }
            int parm = this.printData();
            if (this.OperationEndHandler != null)
            {
                this.OperationEndHandler("print");
            }
            return parm;
        }

        /// <summary>
        /// 打印数据
        /// </summary>
        /// <param name="paperName">纸张名称</param>
        /// <param name="paperWidth">宽度</param>
        /// <param name="paperHeight">高度</param>
        /// <returns></returns>
        public int PrintData(string paperName, int paperWidth, int paperHeight)
        {
            int parm = this.printData(paperName, paperWidth, paperHeight);
            if (this.OperationEndHandler != null)
            {
                this.OperationEndHandler("print");
            }
            return parm;
        }
        #endregion

        #endregion

        #region 事件

        private void fpSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.detailDataQueryType != "O")
            {
                this.queryDetailData();
            }
            if (this.DetailDoubleClickEnd != null)
            {
                DetailDoubleClickEnd();
            }
        }

        void txtFilter_TextChanged(object sender, EventArgs e)
        {
            if (this.OperationStartHandler != null)
            {
                this.OperationStartHandler("filter");
            }

            if (this.filerType == "Null")
            {
                return;
            }
            if (this.filerType == "Tot" || this.filerType == "Both")
            {
                if (this.filters == null || this.filters.Length == 0)
                {
                    MessageBox.Show("程序没有设置过滤字段");
                    this.txtFilter.Text = "程序没有设置过滤字段";
                    this.txtFilter.ReadOnly = true;
                    this.txtFilter.BackColor = System.Drawing.Color.Yellow;
                    return;
                }
                string filter = "1";
                foreach (string field in filters)
                {
                    if (filter == "1")
                    {
                        filter = "(" + field + " LIKE '%" + this.txtFilter.Text + "%') ";
                    }
                    else
                    {
                        filter += " OR (" + field + " LIKE '%" + this.txtFilter.Text + "%') ";
                    }
                }
                try
                {
                    filter += " OR (" + this.fpSpread1_Sheet1.Columns[0].Label + " LIKE '合计%') ";
                    this.DataView.RowFilter = filter;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(filter + ex.Message);
                }
            }
            if (this.filerType == "Det" || this.filerType == "Both")
            {
                if (this.detailFilters == null || this.detailFilters.Length == 0)
                {
                    MessageBox.Show("程序没有设置过滤字段");
                    this.txtFilter.Text = "程序没有设置过滤字段";
                    this.txtFilter.ReadOnly = true;
                    this.txtFilter.BackColor = System.Drawing.Color.Yellow;
                    return;
                }
                string filter = "1";
                foreach (string field in detailFilters)
                {
                    if (filter == "1")
                    {
                        filter = "(" + field + " LIKE '%" + this.txtFilter.Text + "%') ";
                    }
                    else
                    {
                        filter += " OR (" + field + " LIKE '%" + this.txtFilter.Text + "%') ";
                    }
                }
                try
                {
                    filter += " OR (" + this.fpSpread1_Sheet2.Columns[0].Label + " LIKE '合计%') ";
                    this.DetailDataView.RowFilter = filter;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(filter + ex.Message);
                }
            }
            this.sumTot();
            this.sumDetail();
            if (this.OperationEndHandler != null)
            {
                this.OperationEndHandler("filter");
            }
        }

        #endregion

        #region 工具栏信息

        protected override int OnQuery(object sender, object neuObject)
        {
            this.queryData();
            if (QueryEndHandler != null)
            {
                QueryEndHandler();
            }
            return base.OnQuery(sender, neuObject);
        }

        public override int Export(object sender, object neuObject)
        {
            this.exportData();
            return base.Export(sender, neuObject);
        }

        public override int Print(object sender, object neuObject)
        {
            this.printData();
            return base.Print(sender, neuObject);
        }
        #endregion
    }
}
