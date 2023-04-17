using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.InpatientFee.Base
{
    /// <summary>
    /// ucDeptTimeBaseReport<br></br>
    /// [功能描述: ucDeptTimeBaseReport科室时间报表]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2011-04-12]<br></br>
    /// <修改记录 
    ///		修改人='' 
    ///		修改时间='yyyy-mm-dd' 
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    public partial class ucDeptTimeBaseReport : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucDeptTimeBaseReport()
        {
            InitializeComponent();
        }

        #region 变量

        private FS.HISFC.BizLogic.Manager.UserPowerDetailManager priPowerMgr = new FS.HISFC.BizLogic.Manager.UserPowerDetailManager();
        public FS.FrameWork.WinForms.Classes.Print PrintObject = new FS.FrameWork.WinForms.Classes.Print();

        public delegate void DelegateQueryEnd();
        public DelegateQueryEnd QueryEndHandler;

        public delegate void DelegateOperationStart(string type);
        public DelegateOperationStart OperationStartHandler;

        public delegate void DelegateOperateionEnd(string type);
        public DelegateOperateionEnd OperationEndHandler;

        public delegate void DelegateDoubleClickEnd();
        public DelegateDoubleClickEnd DetailDoubleClickEnd;

        /// <summary>
        /// uc宽度
        /// </summary>
        public int MaxWidth = 820;

        /// <summary>
        /// 是否需要合计[Tot汇总求和，Det明细求和，Both明细都需要求和，Null都不求和]
        /// </summary>
        private string sumType = "Null";

        /// <summary>
        /// 是否需要平均数[Tot汇总求和，Det明细求和，Both明细都需要求和，Null都不求和]
        /// </summary>
        private string avgType = "Null";

        private string formulaType = "Null";

        /// <summary>
        /// 科室不可见时是否还需要权限
        /// 二级权限赋值后自动值为true;
        /// </summary>
        private bool isJugdePriPower = false;

        /// <summary>
        /// 二级权限初始化后根据操作员权限是否可以查询数据
        /// </summary>
        private bool isHavePriPower = true;

        /// <summary>
        /// SQL
        /// </summary>
        public string SQL = "";

        /// <summary>
        /// 获取结算时间段的sql
        /// </summary>
        private string getStaticTimeSQL = "";

        #endregion

        #region 属性及相关变量

        #region 过滤
        /// <summary>
        /// 汇总查询的数据
        /// </summary>
        private System.Data.DataView dataView;

        /// <summary>
        /// 明细查询的数据
        /// </summary>
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
                return detailDataView;
            }
        }

        /// <summary>
        /// 汇总过滤
        /// </summary>
        private string filters = "";

        /// <summary>
        /// 汇总过滤
        /// </summary>
        [Description("过滤字符串，如果有明细查询，则是汇总过滤"), Category("Filter过滤"), Browsable(true)]
        public string Filters
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
        private string detailFilters = "";

        /// <summary>
        /// 明细过滤
        /// </summary>
        [Description("过滤字符串，明细过滤"), Category("Filter过滤"), Browsable(true)]
        public string DetailFilters
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
        /// 过滤方式[Tot过滤汇总 Det过滤明细 Both两者过滤 Null不过滤]
        /// </summary>
        EnumFilterType filerType = EnumFilterType.不过滤;

        /// <summary>
        /// 过滤方式[Tot过滤汇总 Det过滤明细 Both两者过滤 Null不过滤]
        /// </summary>
        [Description("过滤方式"), Category("Filter过滤"), Browsable(true), DefaultValue(EnumFilterType.不过滤)]
        public EnumFilterType FilerType
        {
            get
            {
                return filerType;
            }
            set
            {
                filerType = value;
            }
        }

        /// <summary>
        /// 是否回车后启动过滤
        /// </summary>
        bool isFilterAfterEnterKey = false;

        /// <summary>
        /// 是否回车后启动过滤
        /// </summary>
        [Description("是否回车后启动过滤"), Category("Filter过滤"), Browsable(true)]
        public bool FilterAfterEnterKey
        {
            get { return isFilterAfterEnterKey; }
            set { isFilterAfterEnterKey = value; }
        }
        #endregion

        #region 二级权限
        /// <summary>
        /// 二级权限字符串
        /// </summary>
        private string priveClassTwos = "";

        /// <summary>
        /// 二级权限
        /// </summary>
        [Description("二级权限编码，赋值后将检测操作员权限"), Category("Prive二级权限"), Browsable(true)]
        public string PriveClassTwos
        {
            get
            {
                return priveClassTwos;
            }
            set
            {
                priveClassTwos = value;
                if (!string.IsNullOrEmpty(value))
                {
                    isJugdePriPower = true;
                }
            }
        }

        #endregion

        #region 标题设置
        /// <summary>
        /// 标题
        /// </summary>
        private string mainTitle = "主标题";

        /// <summary>
        /// 标题
        /// 如果没有附加标题，传入空
        /// </summary>
        [Description("主标题"), Category("Title标题设置"), Browsable(true)]
        public string MainTitle
        {
            get
            {
                return mainTitle;
            }
            set
            {
                mainTitle = value;
            }
        }

        /// <summary>
        /// 主标题空间高度
        /// </summary>
        private int mainTitleHeight = 47;

        /// <summary>
        /// 主标题空间高度
        /// </summary>
        [Description("主标题空间高度"), Category("Title标题设置"), Browsable(true)]
        public int MainTitleHeight
        {
            get
            {
                return mainTitleHeight;
            }
            set
            {
                mainTitleHeight = value;
            }
        }
        /// <summary>
        /// 主标题字体大小
        /// </summary>
        private float mainTitleFontSize = 14F;

        /// <summary>
        /// 主标题字体大小
        /// </summary>
        [Description("主标题字体大小"), Category("Title标题设置"), Browsable(true)]
        public float MainTitleFontSize
        {
            get
            {
                return mainTitleFontSize;
            }
            set
            {
                mainTitleFontSize = value;
            }
        }

        /// <summary>
        /// 主标题字体类型
        /// </summary>
        private FontStyle mainTitleFontStyle = FontStyle.Bold;

        /// <summary>
        /// 主标题字体类型
        /// </summary>
        [Description("主标题字体类型"), Category("Title标题设置"), Browsable(true)]
        public FontStyle MainTitleFontStyle
        {
            get { return mainTitleFontStyle; }
            set { mainTitleFontStyle = value; }
        }

        /// <summary>
        /// 附加标题空间高度
        /// </summary>
        private int addtionTitleHeight = 20;

        /// <summary>
        /// 附加标题空间高度
        /// </summary>
        [Description("附加标题空间高度"), Category("Title标题设置"), Browsable(true)]
        public int AddtionTitleHeight
        {
            get
            {
                return addtionTitleHeight;
            }
            set
            {
                addtionTitleHeight = value;
            }
        }
        /// <summary>
        /// 附加标题字体大小
        /// </summary>
        private float addtionTitleFontSize = 9F;

        /// <summary>
        /// 附加标题字体大小
        /// </summary>
        [Description("附加标题字体大小"), Category("Title标题设置"), Browsable(true)]
        public float AddtionTitleFontSize
        {
            get
            {
                return addtionTitleFontSize;
            }
            set
            {
                addtionTitleFontSize = value;
            }
        }

        /// <summary>
        /// 主标题字体类型
        /// </summary>
        private FontStyle addtionTitleFontStyle = FontStyle.Bold;

        /// <summary>
        /// 主标题字体类型
        /// </summary>
        [Description("附加标题字体类型"), Category("Title标题设置"), Browsable(true)]
        public FontStyle AddtionTitleFontStyle
        {
            get { return addtionTitleFontStyle; }
            set { addtionTitleFontStyle = value; }
        }

        /// <summary>
        /// 附加标题[左]
        /// </summary>
        private string leftAdditionTitle = "统计时间";

        /// <summary>
        /// 附加标题[左]
        /// </summary>
        [Description("附加标题[左]"), Category("Title标题设置"), Browsable(true)]
        public string LeftAdditionTitle
        {
            get
            {
                return leftAdditionTitle;
            }
            set
            {
                leftAdditionTitle = value;
            }
        }

        /// <summary>
        /// 附加标题[中]
        /// </summary>
        private string midAdditionTitle = "科室";

        /// <summary>
        /// 附加标题[中]
        /// </summary>
        [Description("附加标题[中]"), Category("Title标题设置"), Browsable(true)]
        public string MidAdditionTitle
        {
            get
            {
                return midAdditionTitle;
            }
            set
            {
                midAdditionTitle = value;
            }
        }

        /// <summary>
        /// 附加标题[右]
        /// </summary>
        private string rightAdditionTitle = "附加标题[右]";

        /// <summary>
        /// 附加标题[右]
        /// </summary>
        [Description("附加标题[右]"), Category("Title标题设置"), Browsable(true)]
        public string RightAdditionTitle
        {
            get
            {
                return rightAdditionTitle;
            }
            set
            {
                rightAdditionTitle = value;
            }
        }

        /// <summary>
        /// 是否需要附加标题
        /// </summary>
        private bool isNeedAdditionTitle = true;

        /// <summary>
        /// 是否需要附加标题
        /// </summary>
        [Description("是否需要附加标题"), Category("Title标题设置"), Browsable(true), DefaultValue(true)]
        public bool IsNeedAdditionTitle
        {
            get
            {
                return isNeedAdditionTitle;
            }
            set
            {
                isNeedAdditionTitle = value;
            }
        }
        #endregion

        #region 时间设置
        /// <summary>
        /// 时间间隔天数
        /// </summary>
        private int daysSpan = 0;

        /// <summary>
        /// 时间间隔天数
        /// </summary>
        [Description("开始时间和结束时间间隔天数"), Category("Time时间设置"), Browsable(true), DefaultValue(0)]
        public int DaySpan
        {
            get
            {
                return daysSpan;
            }
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
        /// 是否需要将时间设置为整点
        /// </summary>
        [Description("是否需要将时间设置成整点"), Category("Time时间设置"), Browsable(true), DefaultValue(true)]
        public bool IsNeedIntedDays
        {
            get
            {
                return this.isNeedIntedDays;
            }
            set
            {
                this.isNeedIntedDays = value;
            }
        }

        /// <summary>
        /// 是否将时间作为查询条件
        /// </summary>
        private bool isTimeAsCondition = true;

        /// <summary>
        /// 是否将时间作为查询条件
        /// </summary>
        [Description("是否需要将时间作为查询条件"), Category("Time时间设置"), Browsable(true), DefaultValue(true)]
        public bool IsTimeAsCondition
        {
            get
            {
                return isTimeAsCondition;
            }
            set
            {
                isTimeAsCondition = value;
            }
        }

        #endregion

        #region 科室设置
        /// <summary>
        /// 是否将科室作为查询条件
        /// </summary>
        private bool isDeptAsCondition = true;

        /// <summary>
        /// 是否将科室作为查询条件
        /// </summary>
        [Description("是否需要将科室作为查询条件"), Category("Dept科室设置"), Browsable(true), DefaultValue(false)]
        public bool IsDeptAsCondition
        {
            get
            {
                return isDeptAsCondition;
            }
            set
            {
                isDeptAsCondition = value;
            }
        }

        /// <summary>
        /// 科室中是否可以用“全部”查询所有权限科室数据
        /// </summary>
        private bool isAllowAllDept = false;

        /// <summary>
        /// 科室中是否可以用“全部”查询所有权限科室数据
        /// </summary>
        [Description("科室中是否可以用“全部”查询所有权限科室"), Category("Dept科室设置"), Browsable(true), DefaultValue(false)]
        public bool IsAllowAllDept
        {
            get
            {
                return isAllowAllDept;
            }
            set
            {
                this.isAllowAllDept = value;
            }
        }

        /// <summary>
        /// 科室类别
        /// </summary>
        private FS.HISFC.Models.Base.EnumDepartmentType[] deptTypes = new FS.HISFC.Models.Base.EnumDepartmentType[12];

        /// <summary>
        /// 科室类别
        /// 指定类别后二级权限无效
        /// </summary>
        [Description("科室类别，指定科室类别后二级权限无效"), Category("Dept科室设置"), Browsable(false)]
        public FS.HISFC.Models.Base.EnumDepartmentType[] DeptTypes
        {
            get
            {
                return deptTypes;
            }
            set
            {
                this.deptTypes = value;
                if (DeptType == "PriveDept")
                {
                    this.DeptType = "CommonDept";
                }
            }

        }

        /// <summary>
        /// 科室类别[PriveDept指定二级权限，CommonDept指定科室类别，AllDept全部科室]
        /// 默认0，1指定后0无效，2指定后0、1无效
        /// </summary>
        private string deptType = "PriveDept";

        /// <summary>
        /// 科室类别[PriveDept指定二级权限，CommonDept指定科室类别，AllDept全部科室]
        /// 默认0，1指定后0无效，2指定后0、1无效
        /// </summary>
        [Description("科室类别，指定科室类别后二级权限无效"), Category("Dept科室设置"), Browsable(true)]
        public string DeptType
        {
            get { return deptType; }
            set { deptType = value; }
        }


        #endregion

        #region 查询

        /// <summary>
        /// 初始化时是否用默认条件查询
        /// </summary>
        private bool queryDataWhenInit = true;

        /// <summary>
        /// 初始化时是否用默认条件查询[只写]
        /// </summary>
        [Description("初始化时查询数据"), Category("Query查询设置"), Browsable(true), DefaultValue(true)]
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
        private string sqlIndexs = "";

        /// <summary>
        /// SQL数组
        /// 如果有明细，这个将是汇总查询sql
        /// </summary>
        [Description("汇总SQL的ID"), Category("Query查询设置"), Browsable(true)]
        public string SQLIndexs
        {
            get
            {
                return sqlIndexs;
            }
            set
            {
                this.sqlIndexs = value;
            }
        }

        /// <summary>
        /// 是否需要查询明细
        /// </summary>
        private bool isNeedDetailData = false;

        /// <summary>
        /// 是否需要查询明细
        /// </summary>
        [Description("是否需要查询明细"), Category("Query查询设置"), Browsable(true), DefaultValue(false)]
        public bool IsNeedDetailData
        {
            get
            {
                return this.isNeedDetailData;
            }
            set
            {
                this.isNeedDetailData = value;
            }
        }

        /// <summary>
        /// SQL数组
        /// 明细查询sql
        /// </summary>
        private string detailSQLIndexs = "";

        /// <summary>
        /// SQL数组
        /// 明细查询sql
        /// </summary>
        [Description("明细SQL的ID"), Category("Query查询设置"), Browsable(true)]
        public string DetailSQLIndexs
        {
            get
            {
                return detailSQLIndexs;
            }
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
        [Browsable(false)]
        public string[] QueryAdditionConditions
        {
            set
            {
                this.queryAdditionConditions = value;
            }
        }

        /// <summary>
        /// 明细数据查询方式
        /// </summary>
        EnumQueryType detailDataQueryType;

        /// <summary>
        /// 明细数据查询方式
        /// </summary>
        [Description("明细数据查询方式"), Category("Query查询设置"), Browsable(true), DefaultValue(EnumQueryType.活动行取参数)]
        public EnumQueryType DetailDataQueryType
        {
            get
            {
                return this.detailDataQueryType;
            }
            set
            {
                detailDataQueryType = value;
            }
        }

        /// <summary>
        /// 明细查询时作为条件的FarPint列索引
        /// </summary>
        private string queryConditionColIndexs = "";

        /// <summary>
        /// 明细查询时作为条件的FarPint列索引
        /// </summary>
        [Description("明细查询时作为条件的FarPint列索引"), Category("Query查询设置"), Browsable(true)]
        public string QueryConditionColIndexs
        {
            get
            {
                return this.queryConditionColIndexs;
            }
            set
            {
                queryConditionColIndexs = value;
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

        #endregion

        #region Formula公式

        /// <summary>
        /// 合计列的索引
        /// </summary>
        private string sumColIndexs = "";

        /// <summary>
        /// 合计列的索引
        /// </summary>
        [Description("求和列"), Category("Formula公式"), Browsable(true)]
        public string SumColIndexs
        {
            get
            {
                return this.sumColIndexs;
            }
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
        private string sumDetailColIndexs = "";

        /// <summary>
        /// 合计列的索引
        /// </summary>
        [Description("明细求和列"), Category("Formula公式"), Browsable(true)]
        public string SumDetailColIndexs
        {
            get
            {
                return sumDetailColIndexs;
            }
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
        /// 求平均数列的索引
        /// </summary>
        private string avgColIndexs = "";

        /// <summary>
        /// 求平均数列的索引
        /// </summary>
        [Description("求平均数列的索引"), Category("Formula公式"), Browsable(true)]
        public string AvgColIndexs
        {
            get
            {
                return this.avgColIndexs;
            }
            set
            {
                this.avgColIndexs = value;
                if (this.avgType == "Det")
                {
                    this.avgType = "Both";
                }
                else if (this.avgType == "Null")
                {
                    this.avgType = "Tot";
                }
            }
        }

        /// <summary>
        /// 求平均数列的索引
        /// </summary>
        private string avgDetailColIndexs = "";

        /// <summary>
        /// 求平均数列的索引
        /// </summary>
        [Description("明细求平均数列的索引"), Category("Formula公式"), Browsable(true)]
        public string AvgDetailColIndexs
        {
            get
            {
                return avgDetailColIndexs;
            }
            set
            {
                this.avgDetailColIndexs = value;
                if (this.avgType == "Tot")
                {
                    this.avgType = "Both";
                }
                else if (this.avgType == "Null")
                {
                    this.avgType = "Det";
                }
            }
        }

        /// <summary>
        /// 排序列
        /// </summary>
        private string sortColIndexs = "";

        /// <summary>
        /// 排序列
        /// </summary>
        [Description("排序列"), Category("Formula公式"), Browsable(true)]
        public string SortColIndexs
        {
            get { return sortColIndexs; }
            set { sortColIndexs = value; }
        }

        /// <summary>
        /// 明细排序列
        /// </summary>
        private string detailSortColIndexs = "";

        /// <summary>
        /// 明细排序列
        /// </summary>
        [Description("明细排序列"), Category("Formula公式"), Browsable(true)]
        public string DetailSortColIndexs
        {
            get { return detailSortColIndexs; }
            set { detailSortColIndexs = value; }
        }

        /// <summary>
        /// 求比列的列
        /// </summary>
        private string formulaColIndexs = "";

        /// <summary>
        /// 求比列的列
        /// </summary>
        [Description("求比列的列"), Category("Formula公式"), Browsable(true)]
        public string FormulaColIndexs
        {
            get
            {
                return formulaColIndexs;
            }
            set
            {
                formulaColIndexs = value;
                if (this.formulaType == "Det")
                {
                    this.formulaType = "Both";
                }
                else if (this.formulaType == "Null")
                {
                    this.formulaType = "Tot";
                }

            }
        }

        /// <summary>
        /// 明细求比列的列
        /// </summary>
        private string detailRateColIndexs = "";

        /// <summary>
        /// 明细求比列的列
        /// </summary>
        [Description("求比列的列"), Category("Formula公式"), Browsable(true)]
        public string DetailRateColIndexs
        {
            get
            {
                return detailRateColIndexs;
            }
            set
            {
                detailRateColIndexs = value;
                if (this.formulaType == "Tot")
                {
                    this.formulaType = "Both";
                }
                else if (this.formulaType == "Null")
                {
                    this.formulaType = "Det";
                }
            }
        }

        #endregion

        #region 合并数据

        /// <summary>
        /// 指定合并数据的列
        /// </summary>
        private string mergeDataColIndexs = "";

        /// <summary>
        /// 指定合并数据的列
        /// </summary>
        [Description("指定合并数据的列,仅对Sheet1有效"), Category("Merge合并数据"), Browsable(true)]
        public string MergeDataColIndexs
        {
            get
            {
                return mergeDataColIndexs;
            }
            set
            {
                mergeDataColIndexs = value;
            }
        }

        /// <summary>
        /// 是否需要合并数据
        /// </summary>
        private bool isNeedMergeData = false;

        /// <summary>
        /// 是否需要合并数据
        /// </summary>
        [Description("Sheet1的列是否需要合并数据"), Category("Merge合并数据"), Browsable(true), DefaultValue(false)]
        public bool IsNeedMergeData
        {
            get
            {
                return isNeedMergeData;
            }
            set
            {
                this.isNeedMergeData = value;
            }
        }
        /// <summary>
        /// 指定交叉表的主键列
        /// </summary>
        private int crossDataColIndex = 0;

        /// <summary>
        /// 合并数据时参考sheet的合并依据
        /// eg：参考sheet[0]的0列[MergeDataColIndex=0]存放科室代码，
        /// 那么sheet[1]中也应该有科室代码列，而且代码相同行的合并到farPoint中的同一行
        /// </summary>
        [Description("指定交叉表的主键列，SQLIndexs第一个SQL作为主键的字段序号"), Category("Cross合并数据"), Browsable(true)]
        public int CrossDataColIndex
        {
            get
            {
                return crossDataColIndex;
            }
            set
            {
                crossDataColIndex = value;
            }
        }

        /// <summary>
        /// 是否交叉数据
        /// </summary>
        private bool isNeedCrossData = false;

        /// <summary>
        /// 是否交叉数据
        /// </summary>
        [Description("交叉数据，为True时指定CrossDataColIndexs，第一个外的SQL主键在第一列"), Category("Cross合并数据"), Browsable(true), DefaultValue(false)]
        public bool IsNeedCrossData
        {
            get
            {
                return isNeedCrossData;
            }
            set
            {
                this.isNeedCrossData = value;
            }
        }

        #endregion

        #region 打印设置
        /// <summary>
        /// 打印是否需要预览
        /// </summary>
        private bool isNeedPreView = true;

        /// <summary>
        /// 打印是否需要预览[只写][默认需要]
        /// </summary>
        [Description("打印是否需要预览"), Category("Print打印设置"), Browsable(true), DefaultValue(true)]
        public bool IsNeedPreView
        {
            get
            {
                return isNeedPreView;
            }
            set
            {
                isNeedPreView = value;
            }
        }

        /// <summary>
        /// 打印纸张长度根据内容自动调节
        /// </summary>
        private bool isAutoPaper = true;

        /// <summary>
        /// 打印纸张长度根据内容自动调节
        /// </summary>
        [Description("打印纸张长度根据内容自动调节"), Category("Print打印设置"), Browsable(true), DefaultValue(true)]
        public bool IsAutoPaper
        {
            get { return isAutoPaper; }
            set { isAutoPaper = value; }
        }

        /// <summary>
        /// 打印纸张长度附加高度
        /// </summary>
        private int paperAddHeight = 0;

        /// <summary>
        /// 打印纸张长度附加高度
        /// </summary>
        [Description("打印纸张长度附加高度"), Category("Print打印设置"), Browsable(true)]
        public int PaperAddHeight
        {
            get { return paperAddHeight; }
            set { paperAddHeight = value; }
        }

        /// <summary>
        /// 打印纸张
        /// </summary>
        private string paperName = "Letter";

        /// <summary>
        /// 打印纸张
        /// </summary>
        [Description("打印纸张"), Category("Print打印设置"), Browsable(true)]
        public string PaperName
        {
            get { return paperName; }
            set { paperName = value; }
        }

        /// <summary>
        /// 纸张高度
        /// </summary>
        private int paperWith = 800;

        /// <summary>
        /// 纸张宽度
        /// </summary>
        [Description("纸张像素宽度"), Category("Print打印设置"), Browsable(true)]
        public int PaperWith
        {
            get { return paperWith; }
            set { paperWith = value; }
        }

        /// <summary>
        /// 纸张高度
        /// </summary>
        private int paperHeight = 1100;

        /// <summary>
        /// 纸张高度
        /// </summary>
        [Description("纸张像素高度"), Category("Print打印设置"), Browsable(true)]
        public int PaperHeight
        {
            get { return paperHeight; }
            set { paperHeight = value; }
        }

        /// <summary>
        /// 打印机名称
        /// </summary>
        private string printerName = "";

        /// <summary>
        /// 打印机名称
        /// </summary>
        [Description("打印机名称"), Category("Print打印设置"), Browsable(true)]
        public string PrinterName
        {
            get
            {
                return printerName;
            }
            set
            {
                this.printerName = value;
            }
        }

        #endregion

        #region 配置文件
        /// <summary>
        /// 配置文件
        /// </summary>
        private string settingFilePatch = "";

        /// <summary>
        /// 明细配置文件
        /// </summary>
        private string detailSettingFilePatch = "";

        /// <summary>
        /// 明细配置文件
        /// </summary>
        [Description("明细配置文件"), Category("Set配置文件"), Browsable(true)]
        public string DetailSettingFilePatch
        {
            get
            {
                if (this.detailSettingFilePatch == string.Empty)
                {
                    try
                    {
                        return FS.FrameWork.WinForms.Classes.Function.CurrentPath + @"\Profile\" + this.FindForm().Text + "detail.xml";
                    }
                    catch
                    {
                        return "";
                    }
                }
                return detailSettingFilePatch;
            }
            set
            {
                detailSettingFilePatch = value;
            }
        }

        /// <summary>
        /// 配置文件
        /// </summary>
        [Description("配置文件"), Category("Set配置文件"), Browsable(true)]
        public string SettingFilePatch
        {
            get
            {
                if (this.settingFilePatch == string.Empty)
                {
                    try
                    {
                        return FS.FrameWork.WinForms.Classes.Function.CurrentPath + @"\Profile\" + this.FindForm().Text + "tot.xml";
                    }
                    catch
                    {
                        return "";
                    }
                }
                return settingFilePatch;
            }
            set { settingFilePatch = value; }
        }
        #endregion

        #region FarPoint设置

        /// <summary>
        /// 行头可见性
        /// </summary>
        private bool rowHeaderVisible = true;

        /// <summary>
        /// 行头可见性
        /// </summary>
        [Description("行头可见性"), Category("FarPoint设置"), Browsable(true), DefaultValue(true)]
        public bool RowHeaderVisible
        {
            get
            {
                return rowHeaderVisible;
            }
            set
            {
                rowHeaderVisible = value;
            }
        }

        /// <summary>
        /// 列头高
        /// </summary>
        private float columnHeaderHeight = 20F;

        /// <summary>
        /// 列头高
        /// </summary>
        [Description("列头高"), Category("FarPoint设置"), Browsable(true)]
        public float ColumnHeaderHeight
        {
            get { return columnHeaderHeight; }
            set { columnHeaderHeight = value; }
        }

        /// <summary>
        /// 表名
        /// </summary>
        [Description("表名"), Category("FarPoint设置"), Browsable(true)]
        public string SheetName
        {
            get
            {
                return this.fpSpread1_Sheet1.SheetName;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    this.fpSpread1_Sheet1.SheetName = "汇总";
                }
                else
                {
                    this.fpSpread1_Sheet1.SheetName = value;
                }
            }
        }

        /// <summary>
        /// 表名
        /// </summary>
        [Description("明细表表名"), Category("FarPoint设置"), Browsable(true)]
        public string DetailSheetName
        {
            get
            {
                return this.fpSpread1_Sheet2.SheetName;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    this.fpSpread1_Sheet2.SheetName = "明细";
                }
                else
                {
                    this.fpSpread1_Sheet2.SheetName = value;
                }
            }
        }

        /// <summary>
        /// 表的垂直线颜色
        /// </summary>
        [Description("表的垂直线颜色"), Category("FarPoint设置"), Browsable(true)]
        public Color VerticalGridLineColor
        {
            get
            {
                return this.fpSpread1_Sheet1.VerticalGridLine.Color;
            }
            set
            {
                this.fpSpread1_Sheet1.VerticalGridLine = new FarPoint.Win.Spread.GridLine(this.fpSpread1_Sheet1.HorizontalGridLine.Type, value);
            }
        }

        /// <summary>
        /// 表的水平线颜色
        /// </summary>
        [Description("表的水平线颜色"), Category("FarPoint设置"), Browsable(true)]
        public Color HorizontalGridLineColor
        {
            get
            {
                return this.fpSpread1_Sheet1.HorizontalGridLine.Color;
            }
            set
            {
                this.fpSpread1_Sheet1.HorizontalGridLine = new FarPoint.Win.Spread.GridLine(this.fpSpread1_Sheet1.HorizontalGridLine.Type, value);
            }
        }

        /// <summary>
        /// 表的垂直线颜色
        /// </summary>
        [Description("表列头的垂直线颜色"), Category("FarPoint设置"), Browsable(true)]
        public Color CHVGridLineColor
        {
            get
            {
                return this.fpSpread1_Sheet1.ColumnHeader.VerticalGridLine.Color;
            }
            set
            {
                this.fpSpread1_Sheet1.ColumnHeader.VerticalGridLine = new FarPoint.Win.Spread.GridLine(this.fpSpread1_Sheet1.HorizontalGridLine.Type, value);
            }
        }

        /// <summary>
        /// 表的水平线颜色
        /// </summary>
        [Description("表列头的水平线颜色"), Category("FarPoint设置"), Browsable(true)]
        public Color CHHGridLineColor
        {
            get
            {
                return this.fpSpread1_Sheet1.ColumnHeader.HorizontalGridLine.Color;
            }
            set
            {
                this.fpSpread1_Sheet1.ColumnHeader.HorizontalGridLine = new FarPoint.Win.Spread.GridLine(this.fpSpread1_Sheet1.HorizontalGridLine.Type, value);
            }
        }

        /// <summary>
        /// 表的垂直线颜色
        /// </summary>
        [Description("表行头的垂直线颜色"), Category("FarPoint设置"), Browsable(true)]
        public Color RHVGridLineColor
        {
            get
            {
                return this.fpSpread1_Sheet1.RowHeader.VerticalGridLine.Color;
            }
            set
            {
                this.fpSpread1_Sheet1.RowHeader.VerticalGridLine = new FarPoint.Win.Spread.GridLine(this.fpSpread1_Sheet1.HorizontalGridLine.Type, value);
            }
        }

        /// <summary>
        /// 表的水平线颜色
        /// </summary>
        [Description("表行头的水平线颜色"), Category("FarPoint设置"), Browsable(true)]
        public Color RHHGridLineColor
        {
            get
            {
                return this.fpSpread1_Sheet1.RowHeader.HorizontalGridLine.Color;
            }
            set
            {
                this.fpSpread1_Sheet1.RowHeader.HorizontalGridLine = new FarPoint.Win.Spread.GridLine(this.fpSpread1_Sheet1.HorizontalGridLine.Type, value);
            }
        }


        /// <summary>
        /// 表的垂直线颜色
        /// </summary>
        [Description("明细表的垂直线颜色"), Category("FarPoint设置"), Browsable(true)]
        public Color DetailVerticalGridLineColor
        {
            get
            {
                return this.fpSpread1_Sheet2.VerticalGridLine.Color;
            }
            set
            {
                this.fpSpread1_Sheet2.VerticalGridLine = new FarPoint.Win.Spread.GridLine(this.fpSpread1_Sheet1.HorizontalGridLine.Type, value);
            }
        }

        /// <summary>
        /// 表的水平线颜色
        /// </summary>
        [Description("明表的水平线颜色"), Category("FarPoint设置"), Browsable(true)]
        public Color DetailHorizontalGridLineColor
        {
            get
            {
                return this.fpSpread1_Sheet2.HorizontalGridLine.Color;
            }
            set
            {
                this.fpSpread1_Sheet2.HorizontalGridLine = new FarPoint.Win.Spread.GridLine(this.fpSpread1_Sheet1.HorizontalGridLine.Type, value);
            }
        }

        /// <summary>
        /// 表的垂直线颜色
        /// </summary>
        [Description("明细表头的垂直线颜色"), Category("FarPoint设置"), Browsable(true)]
        public Color DetailCHVGridLineColor
        {
            get
            {
                return this.fpSpread1_Sheet2.ColumnHeader.VerticalGridLine.Color;
            }
            set
            {
                this.fpSpread1_Sheet2.ColumnHeader.VerticalGridLine = new FarPoint.Win.Spread.GridLine(this.fpSpread1_Sheet1.HorizontalGridLine.Type, value);
            }
        }

        /// <summary>
        /// 表的水平线颜色
        /// </summary>
        [Description("明细表头的水平线颜色"), Category("FarPoint设置"), Browsable(true)]
        public Color DetailCHHGridLineColor
        {
            get
            {
                return this.fpSpread1_Sheet2.ColumnHeader.HorizontalGridLine.Color;
            }
            set
            {
                this.fpSpread1_Sheet2.ColumnHeader.HorizontalGridLine = new FarPoint.Win.Spread.GridLine(this.fpSpread1_Sheet1.HorizontalGridLine.Type, value);
            }
        }

        /// <summary>
        /// 明细行头可见性
        /// </summary>
        private bool detailRowHeaderVisible = true;

        /// <summary>
        /// 明细行头可见性
        /// </summary>
        [Description("明细行头可见性"), Category("FarPoint设置"), Browsable(true), DefaultValue(true)]
        public bool DetailRowHeaderVisible
        {
            get
            {
                return detailRowHeaderVisible;
            }
            set
            {
                detailRowHeaderVisible = value;
            }
        }
        /// <summary>
        /// 明细列头高
        /// </summary>
        private float detailColumnHeaderHeight = 20F;

        /// <summary>
        /// 明细列头高
        /// </summary>
        [Description("明细列头高"), Category("FarPoint设置"), Browsable(true)]
        public float DetailColumnHeaderHeight
        {
            get { return detailColumnHeaderHeight; }
            set { detailColumnHeaderHeight = value; }
        }

        /// <summary>
        /// 表的垂直线颜色
        /// </summary>
        [Description("明细表行头的垂直线颜色"), Category("FarPoint设置"), Browsable(true)]
        public Color DetailRHVGridLineColor
        {
            get
            {
                return this.fpSpread1_Sheet2.RowHeader.VerticalGridLine.Color;
            }
            set
            {
                this.fpSpread1_Sheet2.RowHeader.VerticalGridLine = new FarPoint.Win.Spread.GridLine(this.fpSpread1_Sheet1.HorizontalGridLine.Type, value);
            }
        }

        /// <summary>
        /// 表的水平线颜色
        /// </summary>
        [Description("明细表行头的水平线颜色"), Category("FarPoint设置"), Browsable(true)]
        public Color DetailRHHGridLineColor
        {
            get
            {
                return this.fpSpread1_Sheet2.RowHeader.HorizontalGridLine.Color;
            }
            set
            {
                this.fpSpread1_Sheet2.RowHeader.HorizontalGridLine = new FarPoint.Win.Spread.GridLine(this.fpSpread1_Sheet1.HorizontalGridLine.Type, value);
            }
        }


        #endregion


        /// <summary>
        /// 获取月结时间段的sql
        /// </summary>
        [Category("扩展信息"), Description("获取结算时间段的sql"), Browsable(true)]
        public string GetStaticTimeSQL
        {
            get { return getStaticTimeSQL; }
            set { getStaticTimeSQL = value; }
        }

        #endregion

        #region 方法

        #region 私有

        /// <summary>
        /// 根据属性设置控件
        /// </summary>
        /// <returns></returns>
        private int setControlWithPerporty()
        {
            this.nGroupBoxQueryCondition.Visible = this.IsDeptAsCondition | this.IsTimeAsCondition | (this.FilerType != EnumFilterType.不过滤);

            //过滤
            if (this.FilerType == EnumFilterType.不过滤)
            {
                this.panelFilter.Visible = false;
            }
            else
            {
                this.panelFilter.Visible = true;
            }

            //标题
            this.lbMainTitle.Text = this.MainTitle;
            if (this.MainTitle == null || this.MainTitle == string.Empty)
            {
                //this.panelTitle.Visible = false;
            }
            this.lbAdditionTitleLeft.Text = this.LeftAdditionTitle;
            this.lbAdditionTitleMid.Text = this.MidAdditionTitle;
            this.lbAdditionTitleRight.Text = this.RightAdditionTitle;
            if (!this.IsNeedAdditionTitle)
            {
                this.panelAdditionTitle.Height = 0;
            }
            else
            {
                this.panelAdditionTitle.Height = this.AddtionTitleHeight;
            }


            this.panelTitle.Height = this.MainTitleHeight;
            this.lbMainTitle.Font = new Font(this.lbMainTitle.Font.FontFamily, this.MainTitleFontSize, this.MainTitleFontStyle);
            this.lbAdditionTitleLeft.Font = new Font(this.lbAdditionTitleLeft.Font.FontFamily, this.AddtionTitleFontSize, this.AddtionTitleFontStyle);
            this.lbAdditionTitleMid.Font = new Font(this.lbAdditionTitleMid.Font.FontFamily, this.AddtionTitleFontSize, this.AddtionTitleFontStyle);
            this.lbAdditionTitleRight.Font = new Font(this.lbAdditionTitleRight.Font.FontFamily, this.AddtionTitleFontSize, this.AddtionTitleFontStyle);

            this.panelTime.Visible = this.IsTimeAsCondition;
            this.panelDept.Visible = this.IsDeptAsCondition;
            //查询
            if (!this.IsNeedDetailData)
            {
                this.fpSpread1.Sheets.Remove(this.fpSpread1_Sheet2);
            }
            else
            {
                if (!this.fpSpread1.Sheets.Contains(this.fpSpread1_Sheet2))
                {
                    this.fpSpread1.Sheets.Add(this.fpSpread1_Sheet2);
                }
            }

            //FarPiont设置
            if (this.DetailSQLIndexs == null || this.DetailSQLIndexs == string.Empty)
            {
                this.fpSpread1.Sheets.Remove(this.fpSpread1_Sheet2);
            }
            this.fpSpread1_Sheet1.RowHeader.Visible = this.RowHeaderVisible;
            this.fpSpread1_Sheet2.RowHeader.Visible = this.DetailRowHeaderVisible;
            this.fpSpread1_Sheet1.ColumnHeader.Rows[0].Height = this.ColumnHeaderHeight;
            this.fpSpread1_Sheet2.ColumnHeader.Rows[0].Height = this.DetailColumnHeaderHeight;

            return 0;
        }

        /// <summary>
        /// 根据二级权限初始化查寻科室
        /// </summary>
        /// <returns>-1失败 0成功</returns>
        private int initDeptByPriPower()
        {
            this.isHavePriPower = true;

            //科室不可见时表示不用
            if (!this.IsDeptAsCondition && !isJugdePriPower)
            {
                return 0;
            }
            if (this.priveClassTwos == null)
            {
                MessageBox.Show("二级权限数组没有赋值\n请使用属性PriClassTwos[字符串]");
                return -1;
            }

            FS.HISFC.BizLogic.Manager.Department deptMgr = new FS.HISFC.BizLogic.Manager.Department();

            //哈希表用于避免科室重复添加
            System.Collections.Hashtable hsDept = new Hashtable();

            //所有科室，当有多个二级权限时，可能是重复的
            ArrayList alDept = new ArrayList();

            if (this.DeptType == "AllDept")
            {
                alDept = deptMgr.GetDeptmentAll();
            }
            else if (this.DeptType == "PriveDept")
            {
                //每一二级权限对应科室
                System.Collections.Generic.List<FS.FrameWork.Models.NeuObject> alTmpDept;

                //根据二级权限获取科室信息
                string[] prives = priveClassTwos.Split(',', ' ', '|');
                foreach (string classTwoCode in prives)
                {
                    alTmpDept = new System.Collections.Generic.List<FS.FrameWork.Models.NeuObject>();
                    string[] prive3s = classTwoCode.Split('+');
                    if (prive3s.Length == 1)
                    {
                        alTmpDept = this.priPowerMgr.QueryUserPriv(priPowerMgr.Operator.ID, classTwoCode);
                        if (alTmpDept == null || alTmpDept.Count == 0)
                        {
                            continue;
                        }
                        alDept.AddRange(alTmpDept);
                    }
                    else
                    {
                        for (int index = 0; index < prive3s.Length; index++)
                        {
                            if (index + 1 < prive3s.Length)
                            {
                                alTmpDept = this.priPowerMgr.QueryUserPriv(priPowerMgr.Operator.ID, prive3s[index], prive3s[index + 1]);
                            }
                            if (alTmpDept == null || alTmpDept.Count == 0)
                            {
                                continue;
                            }
                            alDept.AddRange(alTmpDept);
                        }
                    }
                }

            }
            else
            {
                string[] deptTypes = this.deptType.Split('|', ',', ' ');
                foreach (string dType in deptTypes)
                {

                    alDept.AddRange(deptMgr.GetDeptmentByType(dType));
                }
            }
            //移除重复的科室
            foreach (FS.FrameWork.Models.NeuObject dept in alDept)
            {
                if (!hsDept.Contains(dept.ID))
                {
                    hsDept.Add(dept.ID, dept);
                }
                else
                {
                    //alDept.Remove(dept);
                }
            }
            alDept.RemoveRange(0, alDept.Count);
            string id = "";
            foreach (FS.FrameWork.Models.NeuObject dept in hsDept.Values)
            {
                id += "'" + dept.ID + "',";
                alDept.Add(dept);
            }
            FS.FrameWork.Models.NeuObject deptTmp;

            //添加“全部”
            if (alDept.Count > 0)
            {
                if (this.isAllowAllDept)
                {
                    deptTmp = new FS.FrameWork.Models.NeuObject();
                    if (this.DeptType == "PriveDept")
                    {
                        deptTmp.ID = id.TrimEnd(',', '\'');
                        deptTmp.ID = deptTmp.ID.TrimStart('\'');
                    }
                    else
                    {
                        deptTmp.ID = "All";
                    }
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
            this.cmbDept.AddItems(alDept);
            if (alDept.Count == 1)
            {
                this.cmbDept.SelectedIndex = 0;
                this.cmbDept.Tag = ((FS.FrameWork.Models.NeuObject)alDept[0]).ID;
            }

            return 0;
        }

        /// <summary>
        /// 初始化查询时间
        /// </summary>
        /// <returns>-1失败 0成功</returns>
        private int initTime()
        {
            //时间不可见，不处理
            if (!this.IsTimeAsCondition)
            {
                return 0;
            }
            this.ncmbTime.SelectedIndexChanged -= new EventHandler(ncmbTime_SelectedIndexChanged);
            this.ncmbTime.SelectedIndexChanged += new EventHandler(ncmbTime_SelectedIndexChanged);

            if (!string.IsNullOrEmpty(this.getStaticTimeSQL))
            {
                this.ncmbTime.Visible = true; //获取结算时间段的sql不为空的时候，使用时间段选择
            }
            else
            {
                this.ncmbTime.Visible = false;
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
            //add by cube 2009-10-15 这样可以消除行列的设置
            this.fpSpread1_Sheet1.RowCount = 0;
            this.fpSpread1_Sheet1.ColumnCount = 0;
            //end add

            if (string.IsNullOrEmpty(this.SQLIndexs) && string.IsNullOrEmpty(this.SQL))
            {
                this.ShowBalloonTip(5000, "温馨提示", "查询的SQL语句索引不正确\n请将SqlIndexs属性赋值");
                return -1;
            }
            System.Data.DataSet ds = new DataSet();
            if (!isHavePriPower && this.isJugdePriPower)
            {
                MessageBox.Show("您没有权限，您可以将二级权限代码：" + this.priveClassTwos + "报给信息科以便协助解决！");
                return 0;
            }

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在查询，请稍候...");
            Application.DoEvents();
            if (this.IsNeedCrossData)
            {

                if (this.crossQuery() == -1)
                {
                    return -1;
                }
            }
            else
            {
                if (string.IsNullOrEmpty(this.SQL))
                {
                    string[] sqls = this.SQLIndexs.Split(' ', ',', '|');
                    if (this.isNeedAdditionConditions)
                    {
                        if (this.priPowerMgr.ExecQuery(sqls, ref ds, this.queryAdditionConditions) == -1)
                        {
                            MessageBox.Show("执行sql发生错误>>" + this.priPowerMgr.Err);
                            return -1;
                        }
                    }
                    else if (this.priPowerMgr.ExecQuery(sqls, ref ds, this.QueryConditions) == -1)
                    {
                        MessageBox.Show("执行sql发生错误>>" + this.priPowerMgr.Err);
                        return -1;
                    }
                }
                else
                {
                    string sql = string.Format(this.SQL, this.QueryConditions);
                    if (this.priPowerMgr.ExecQuery(sql, ref ds) == -1)
                    {
                        MessageBox.Show("执行sql发生错误>>" + this.priPowerMgr.Err);
                        return -1;
                    }
                }
            }
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            if (ds == null || ds.Tables.Count == 0)
            {
                return -1;
            }
            this.DataView = new DataView(ds.Tables[0]);
            this.fpSpread1_Sheet1.DataSource = this.DataView;

            this.readSetting(0);
            this.sortTot();
            this.sumTot();
            this.avgTot();
            this.formulaTot();
            this.mergeData();

            if (this.detailDataQueryType == EnumQueryType.同条件同步查询)
            {
                this.queryDetailData();
            }

            if (this.LeftAdditionTitle.IndexOf("统计时间") != -1)
            {
                this.lbAdditionTitleLeft.Text = " 统计时间:" + this.dtStart.Value.ToString()
                + " 到 " + this.dtEnd.Value.ToString();
            }
            else if (LeftAdditionTitle.IndexOf("统计日期") != -1)
            {
                this.lbAdditionTitleLeft.Text = " 统计日期:" + this.dtStart.Value.ToString("yyyy年MM月dd日")
                + " 到 " + this.dtEnd.Value.AddSeconds(1).ToString("yyyy年MM月dd日");
            }
            else if (this.LeftAdditionTitle.IndexOf("打印时间") != -1)
            {
                this.lbAdditionTitleLeft.Text = " 打印时间:" + this.priPowerMgr.GetDateTimeFromSysDateTime().ToString();
            }
            else if (LeftAdditionTitle.IndexOf("打印日期") != -1)
            {
                this.lbAdditionTitleLeft.Text = " 打印日期:" + this.priPowerMgr.GetDateTimeFromSysDateTime().ToString("yyyy年MM月dd日");
            }
            else if (LeftAdditionTitle.IndexOf("年度") != -1)
            {
                this.lbAdditionTitleLeft.Text = " 年度:" + this.dtEnd.Value.AddSeconds(1).ToString("yyyy年");
            }
            if (this.cmbDept.Text != "全部")
            {
                if (this.MidAdditionTitle.IndexOf("科室") != -1)
                {
                    this.lbAdditionTitleMid.Text = "科室:" + this.cmbDept.Text;
                }
                if (this.MidAdditionTitle.IndexOf("部门") != -1)
                {
                    this.lbAdditionTitleMid.Text = "部门:" + this.cmbDept.Text;
                }
            }
            else
            {
                this.lbAdditionTitleMid.Text = "";
            }
            return 0;
        }

        /// <summary>
        /// 查询所有需要合并的数据
        /// </summary>
        /// <returns></returns>
        private int crossQuery()
        {
            FarPoint.Win.Spread.SheetView sv;
            System.Data.DataSet dsTmp;
            System.Collections.Generic.List<FarPoint.Win.Spread.SheetView> sheets = new System.Collections.Generic.List<FarPoint.Win.Spread.SheetView>();
            string[] sqls = this.SQLIndexs.Split(',', '|', ' ');
            foreach (string sql in sqls)
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
            return this.mergeCrossSheet(sheets);
        }

        /// <summary>
        /// 合并数据[完成交叉报表的功能]
        /// </summary>
        /// <param name="sheets"></param>
        /// <returns></returns>
        private int mergeCrossSheet(System.Collections.Generic.List<FarPoint.Win.Spread.SheetView> sheets)
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
                        if (colIndex == crossDataColIndex)
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

        /// <summary>
        /// 获取明细查询的参数
        /// </summary>
        /// <returns></returns>
        private string[] getDetailParm()
        {
            if (this.detailDataQueryType == EnumQueryType.汇总条件活动行)
            {

                int activeRowIndex = this.fpSpread1_Sheet1.ActiveRow.Index;
                string[] parm;
                int totLenth = this.QueryConditions.Length;
                string[] cols = queryConditionColIndexs.Split(' ', ',', '|');
                parm = new string[totLenth + cols.Length];
                int index = 0;
                foreach (string totParm in this.QueryConditions)
                {
                    parm[index] = totParm;
                    index++;
                }
                foreach (string colIndex in cols)
                {
                    string curColValue = this.fpSpread1_Sheet1.Cells[activeRowIndex, int.Parse(colIndex)].Text.Trim();
                    parm[index] = curColValue;
                    index++;
                }
                return parm;
            }
            if (this.detailDataQueryType == EnumQueryType.活动行取参数)
            {

                int activeRowIndex = this.fpSpread1_Sheet1.ActiveRow.Index;
                string[] cols = queryConditionColIndexs.Split(' ', ',', '|');
                string[] parm = new string[cols.Length];
                int index = 0;
                foreach (string colIndex in cols)
                {
                    string curColValue = this.fpSpread1_Sheet1.Cells[activeRowIndex, int.Parse(colIndex)].Text.Trim();
                    parm[index] = curColValue;
                    index++;
                }
                return parm;
            }
            if (this.DetailDataQueryType == EnumQueryType.同条件异步查询 || this.DetailDataQueryType == EnumQueryType.同条件同步查询)
            {
                return this.QueryConditions;
            }
            if (this.detailConditions == null)
            {
                return new string[] { "" };
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
            if (this.isNeedAdditionConditions)
            {
                if (this.priPowerMgr.ExecQuery(this.detailSQLIndexs.Split(' ', ',', '|'), ref ds, this.queryAdditionConditions) == -1)
                {
                    this.ShowBalloonTip(1000, "温馨提示", "发生错误" + this.priPowerMgr.Err);
                    return -1;
                }
            }
            else
            {
                string[] parm = this.getDetailParm();
                if (this.priPowerMgr.ExecQuery(this.detailSQLIndexs.Split(' ', ',', '|'), ref ds, parm) == -1)
                {
                    this.ShowBalloonTip(1000, "温馨提示", "发生错误" + this.priPowerMgr.Err);
                    return -1;
                }
            }
            if (ds == null || ds.Tables.Count == 0)
            {
                return -1;
            }
            this.DetailDataView = new DataView(ds.Tables[0]);
            if (this.fpSpread1.Sheets.Count == 1)
            {
                this.fpSpread1.Sheets.Add(this.fpSpread1_Sheet2);
            }
            this.fpSpread1_Sheet2.DataSource = this.DetailDataView;
            this.sumDetail();
            this.avgDetail();

            this.readSetting(1);
            this.sortDetail();

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
                    this.ShowBalloonTip(5000, "温馨提示", "导出成功");
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
            this.resetTitleLocation(true);
            int width = this.Width;
            this.Dock = System.Windows.Forms.DockStyle.None;
            this.Width = width;
            if (this.isAutoPaper)
            {
                PrintObject.SetPageSize(this.getPaperSize());
                PrintObject.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
                if (this.isNeedPreView)
                {
                    PrintObject.PrintPreview(20, 20, this.panelPrint);
                }
                else
                {
                    PrintObject.PrintPage(20, 20, this.panelPrint);
                }

                this.Width = width;
                this.Dock = System.Windows.Forms.DockStyle.Fill;
            }
            else
            {
                this.printData(this.PaperName, this.PaperWith, this.PaperHeight, this.PrinterName);
            }
            this.resetTitleLocation(false);
            return 0;
        }

        /// <summary>
        /// 打印数据
        /// </summary>
        /// <param name="paperName">纸张名称</param>
        /// <param name="paperWidth">宽度</param>
        /// <param name="paperHeight">高度</param>
        /// <returns></returns>
        private int printData(string paperName, int paperWidth, int paperHeight, string printerName)
        {
            if (paperName == string.Empty)
            {
                this.ShowBalloonTip(5000, "温馨提示", "没有设置纸张\n已经忽略");
                this.IsAutoPaper = true;
                return this.printData();
            }
            int width = this.Width;
            this.Dock = System.Windows.Forms.DockStyle.None;
            PrintObject.IsResetPage = true;
            FS.HISFC.Models.Base.PageSize paperSize = new FS.HISFC.Models.Base.PageSize(paperName, paperWidth, paperHeight);
            if (!string.IsNullOrEmpty(this.PrinterName))
            {
                paperSize.Printer = this.PrinterName;
            }
            PrintObject.SetPageSize(paperSize);
            PrintObject.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            if (this.IsNeedPreView)
            {
                PrintObject.PrintPreview(20, 20, this.panelPrint);
            }
            else
            {
                PrintObject.PrintPage(20, 20, this.panelPrint);
            }
            this.Width = width;
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            return 0;
        }

        /// <summary>
        /// 出库单的纸张高度设置
        /// 默认情况下是三行出库数据的高度
        /// </summary>
        private FS.HISFC.Models.Base.PageSize getPaperSize()
        {
            FS.HISFC.Models.Base.PageSize paperSize = new FS.HISFC.Models.Base.PageSize();
            paperSize.Name = this.priPowerMgr.GetDateTimeFromSysDateTime().ToString();
            try
            {
                int width = 800;

                int curHeight = this.Height;

                int addHeight = (this.fpSpread1.ActiveSheet.RowCount - 1) *
                    (int)this.fpSpread1.ActiveSheet.Rows[0].Height;

                int additionAddHeight = this.PaperAddHeight;
                if (!string.IsNullOrEmpty(this.PrinterName))
                {
                    paperSize.Printer = this.PrinterName;
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
            this.Width = this.MaxWidth;
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
        /// 重新设置标题位置
        /// </summary>
        private void resetTitleLocation(bool isPrint)
        {
            this.lbMainTitle.Dock = DockStyle.None;
            this.panelTitle.Controls.Remove(this.lbMainTitle);
            if (this.IsNeedAdditionTitle)
            {
                this.panelAdditionTitle.Controls.Remove(this.lbAdditionTitleMid);
                this.panelAdditionTitle.Controls.Remove(this.lbAdditionTitleRight);
            }
            int with = 0;
            for (int col = 0; col < this.fpSpread1.ActiveSheet.ColumnCount; col++)
            {
                if (this.fpSpread1.ActiveSheet.Columns[col].Visible)
                {
                    with += (int)this.fpSpread1.ActiveSheet.Columns[col].Width;
                }
            }
            if (!isPrint && with > this.panelTitle.Width)
            {
                with = this.panelTitle.Width;
            }
            this.lbMainTitle.Location = new Point((with - this.lbMainTitle.Size.Width) / 2, this.lbMainTitle.Location.Y);
      
            this.panelTitle.Controls.Add(this.lbMainTitle);

            if (this.IsNeedAdditionTitle)
            {
                this.lbAdditionTitleMid.Location = new Point((with - this.lbAdditionTitleMid.Size.Width) / 2, this.lbAdditionTitleMid.Location.Y);
                this.lbAdditionTitleRight.Location = new Point((with - this.lbAdditionTitleRight.Size.Width), this.lbAdditionTitleRight.Location.Y);
                this.panelAdditionTitle.Controls.Add(this.lbAdditionTitleMid);
                this.panelAdditionTitle.Controls.Add(this.lbAdditionTitleRight);
            }
        }
        private void resetTitleLocation()
        {
            resetTitleLocation(false);
        }

        /// <summary>
        /// 汇总求和
        /// </summary>
        /// <returns></returns>
        private int sumTot()
        {
            try
            {
                if (this.sumColIndexs == null || this.sumColIndexs.Length == 0)
                {
                    return 0;
                }

                if (this.sumType == "Tot" || this.sumType == "Both")
                {
                    this.fpSpread1_Sheet1.AddRows(this.fpSpread1_Sheet1.RowCount, 1);
                    string[] cols = sumColIndexs.Split(' ', ',', '|');
                    foreach (string colLetter in cols)
                    {
                        try
                        {
                            if (string.IsNullOrEmpty(colLetter))
                            {
                                continue;
                            }
                            int colIndex = FS.FrameWork.Function.NConvert.ToInt32(colLetter);
                            if (colIndex > this.fpSpread1_Sheet1.ColumnCount - 1 || this.fpSpread1_Sheet1.RowCount == 1)
                            {
                                continue;
                            }
                            string letter = ((char)(colIndex + 65)).ToString();
                            string formul = "SUM(" + letter + "1:" + letter + (this.fpSpread1_Sheet1.RowCount - 1).ToString() + ")";

                            this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.RowCount - 1, colIndex].Formula = formul;
                        }
                        catch (Exception ex)
                        {
                            this.ShowBalloonTip(5000, "温馨提示", ex.Message + "\n求和时已经忽略列" + colLetter);
                            continue;
                        }
                    }
                }
                else
                {
                    return 0;
                }
                if (this.sumColIndexs[0] != '0')
                {
                    this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.RowCount - 1, 0].Value = "合计：";
                }
            }
            catch (Exception ex)
            {
                //this.ShowBalloonTip(2000, "温馨提示", ex.Message + "\n求和时已经忽略");
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
                if (this.sumDetailColIndexs == null || this.sumDetailColIndexs.Length == 0)
                {
                    return 0;
                }

                if (this.sumType == "Det" || this.sumType == "Both")
                {
                    this.fpSpread1_Sheet2.AddRows(this.fpSpread1_Sheet2.RowCount, 1);
                    string[] cols = this.sumDetailColIndexs.Split(' ', ',', '|');
                    foreach (string colLetter in cols)
                    {
                        try
                        {
                            if (string.IsNullOrEmpty(colLetter))
                            {
                                continue;
                            }
                            int colIndex = FS.FrameWork.Function.NConvert.ToInt32(colLetter);
                            if (colIndex > this.fpSpread1_Sheet2.ColumnCount - 1 || this.fpSpread1_Sheet2.RowCount == 1)
                            {
                                continue;
                            }
                            string letter = ((char)(colIndex + 65)).ToString();

                            this.fpSpread1_Sheet2.Cells[this.fpSpread1_Sheet2.RowCount - 1, colIndex].Formula
                            = "SUM(" + letter + "1:" + letter + (this.fpSpread1_Sheet2.RowCount - 1).ToString() + ")";
                        }
                        catch (Exception ex)
                        {
                            this.ShowBalloonTip(5000, "温馨提示", ex.Message + "\n明细求和时已经忽略列" + colLetter);
                            continue;
                        }
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
                //this.ShowBalloonTip(2000, "温馨提示", ex.Message + "\n明细求和时已经忽略");
                return -1;
            }
            return 0;

        }

        /// <summary>
        /// 汇总求平均数
        /// </summary>
        /// <returns></returns>
        private int avgTot()
        {
            try
            {
                if (this.avgColIndexs == null || this.avgColIndexs.Length == 0)
                {
                    return 0;
                }

                if (this.avgType == "Tot" || this.avgType == "Both")
                {
                    this.fpSpread1_Sheet1.AddRows(this.fpSpread1_Sheet1.RowCount, 1);
                    string[] cols = avgColIndexs.Split(' ', ',');
                    foreach (string colLetter in cols)
                    {
                        try
                        {
                            if (string.IsNullOrEmpty(colLetter))
                            {
                                continue;
                            }
                            int colIndex = FS.FrameWork.Function.NConvert.ToInt32(colLetter.Split('|')[0]);
                            if (colIndex > this.fpSpread1_Sheet1.ColumnCount - 1 || this.fpSpread1_Sheet1.RowCount == 1)
                            {
                                continue;
                            }
                            string letter = ((char)(colIndex + 65)).ToString();
                            int rowCount = this.fpSpread1_Sheet1.RowCount - 1;
                            if (!string.IsNullOrEmpty(this.sumColIndexs) && (this.sumType == "Both" || this.sumType == "Tot"))
                            {
                                rowCount = rowCount - 1;
                                if (rowCount <= 1)
                                {
                                    continue;
                                }
                            }

                            string formula = "AVERAGE(" + letter + "1:" + letter + rowCount.ToString() + ")";

                            int decimalPlaces = 0;
                            if (colLetter.Split('|').Length > 1)
                            {
                                decimalPlaces = FS.FrameWork.Function.NConvert.ToInt32(colLetter.Split('|')[1]);
                            }
                            if (colLetter.Split('|').Length > 2 && colLetter.Split('|')[2] == "%")
                            {
                                FarPoint.Win.Spread.CellType.PercentCellType p = new FarPoint.Win.Spread.CellType.PercentCellType();
                                p.DecimalPlaces = decimalPlaces;
                                this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.RowCount - 1, colIndex].CellType = p;
                            }
                            else
                            {
                                FarPoint.Win.Spread.CellType.NumberCellType p = new FarPoint.Win.Spread.CellType.NumberCellType();
                                p.DecimalPlaces = decimalPlaces;
                                this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.RowCount - 1, colIndex].CellType = p;

                            }
                            this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.RowCount - 1, colIndex].Formula = formula;
                        }
                        catch (Exception ex)
                        {
                            this.ShowBalloonTip(5000, "温馨提示", ex.Message + "\n求平均数时已经忽略列" + colLetter);
                            continue;
                        }
                    }
                }
                else
                {
                    return 0;
                }
                if (this.avgColIndexs[0] != '0')
                {
                    this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.RowCount - 1, 0].Value = "平均：";
                }
            }
            catch (Exception ex)
            {
                //this.ShowBalloonTip(2000, "温馨提示", ex.Message + "\n求和时已经忽略");
                return -1;
            }
            return 0;

        }

        /// <summary>
        /// 明细求平均数
        /// </summary>
        /// <returns></returns>
        private int avgDetail()
        {
            try
            {
                if (this.avgDetailColIndexs == null || this.avgDetailColIndexs.Length == 0)
                {
                    return 0;
                }

                if (this.avgType == "Det" || this.avgType == "Both")
                {
                    this.fpSpread1_Sheet2.AddRows(this.fpSpread1_Sheet1.RowCount, 1);
                    string[] cols = avgDetailColIndexs.Split(' ', ',');
                    foreach (string colLetter in cols)
                    {
                        try
                        {
                            if (string.IsNullOrEmpty(colLetter))
                            {
                                continue;
                            }
                            int colIndex = FS.FrameWork.Function.NConvert.ToInt32(colLetter.Split('|')[0]);
                            if (colIndex > this.fpSpread1_Sheet2.ColumnCount - 1 || this.fpSpread1_Sheet2.RowCount == 1)
                            {
                                continue;
                            }
                            string letter = ((char)(colIndex + 65)).ToString();
                            int rowCount = this.fpSpread1_Sheet2.RowCount - 1;
                            if (!string.IsNullOrEmpty(this.sumDetailColIndexs) && (this.sumType == "Both" || this.sumType == "Det"))
                            {
                                rowCount = rowCount - 1;
                                if (rowCount <= 1)
                                {
                                    continue;
                                }
                            }
                            string formul = "AVERAGE(" + letter + "1:" + letter + rowCount.ToString() + ")";

                            int decimalPlaces = 0;
                            if (colLetter.Split('|').Length > 1)
                            {
                                decimalPlaces = FS.FrameWork.Function.NConvert.ToInt32(colLetter.Split('|')[1]);
                            }
                            if (colLetter.Split('|').Length > 2 && colLetter.Split('|')[2] == "%")
                            {
                                FarPoint.Win.Spread.CellType.PercentCellType p = new FarPoint.Win.Spread.CellType.PercentCellType();
                                p.DecimalPlaces = decimalPlaces;
                                this.fpSpread1_Sheet2.Cells[this.fpSpread1_Sheet2.RowCount - 1, colIndex].CellType = p;
                            }
                            else
                            {
                                FarPoint.Win.Spread.CellType.NumberCellType p = new FarPoint.Win.Spread.CellType.NumberCellType();
                                p.DecimalPlaces = decimalPlaces;
                                this.fpSpread1_Sheet2.Cells[this.fpSpread1_Sheet2.RowCount - 1, colIndex].CellType = p;

                            }
                            this.fpSpread1_Sheet2.Cells[this.fpSpread1_Sheet2.RowCount - 1, colIndex].Formula = formul;
                        }
                        catch (Exception ex)
                        {
                            this.ShowBalloonTip(5000, "温馨提示", ex.Message + "\n求平均数时已经忽略列" + colLetter);
                            continue;
                        }
                    }
                }
                else
                {
                    return 0;
                }
                if (this.avgColIndexs[0] != '0')
                {
                    this.fpSpread1_Sheet2.Cells[this.fpSpread1_Sheet2.RowCount - 1, 0].Value = "平均：";
                }
            }
            catch (Exception ex)
            {
                //this.ShowBalloonTip(2000, "温馨提示", ex.Message + "\n求和时已经忽略");
                return -1;
            }
            return 0;
        }

        /// <summary>
        /// 求比例
        /// </summary>
        /// <returns></returns>
        private int formulaTot()
        {
            try
            {
                if (this.formulaColIndexs == null || this.formulaColIndexs.Length == 0)
                {
                    return 0;
                }

                if (this.formulaType == "Tot" || this.formulaType == "Both")
                {
                    this.fpSpread1_Sheet1.AddRows(this.fpSpread1_Sheet1.RowCount, 1);
                    string[] cols = formulaColIndexs.Split(' ', ',');
                    foreach (string colLetter in cols)
                    {
                        try
                        {
                            if (string.IsNullOrEmpty(colLetter))
                            {
                                continue;
                            }
                            int colIndex = FS.FrameWork.Function.NConvert.ToInt32(colLetter.Split('|')[0]);
                            if (colIndex > this.fpSpread1_Sheet1.ColumnCount - 1)
                            {
                                continue;
                            }
                            string letter = ((char)(colIndex + 65)).ToString();
                            int rowCount = this.fpSpread1_Sheet1.RowCount - 1;
                            if (!string.IsNullOrEmpty(this.sumColIndexs) && (this.sumType == "Both" || this.sumType == "Tot"))
                            {
                                rowCount = rowCount - 1;
                                if (rowCount <= 1)
                                {
                                    continue;
                                }
                            }
                            if (!string.IsNullOrEmpty(this.avgColIndexs) && (this.sumType == "Both" || this.sumType == "Tot"))
                            {
                                rowCount = rowCount - 1;
                                if (rowCount <= 1)
                                {
                                    continue;
                                }
                            }
                            //公式举例
                            //0|sum(A)表示在第0列求列A的和
                            string formula = "";
                            if (colLetter.Split('|').Length > 1)
                            {
                                formula = colLetter.Split('|')[1];
                                formula = formula.ToUpper();
                                for (int col = 0; col < this.fpSpread1_Sheet1.ColumnCount; col++)
                                {
                                    string colstr = ((char)(col + 65)).ToString().ToUpper();
                                    if (formula.Contains("(" + colstr + ")"))
                                    {
                                        formula = formula.Replace("(" + colstr + ")", "(" + colstr + "1:" + colstr + rowCount.ToString() + ")");
                                    }
                                }
                            }
                            if (!string.IsNullOrEmpty(formula))
                            {
                                int decimalPlaces = 0;
                                if (colLetter.Split('|').Length > 2)
                                {
                                    decimalPlaces = FS.FrameWork.Function.NConvert.ToInt32(colLetter.Split('|')[2]);
                                }
                                if (colLetter.Split('|').Length > 3 && colLetter.Split('|')[3] == "%")
                                {
                                    FarPoint.Win.Spread.CellType.PercentCellType p = new FarPoint.Win.Spread.CellType.PercentCellType();
                                    p.DecimalPlaces = decimalPlaces;
                                    this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.RowCount - 1, colIndex].CellType = p;
                                }
                                else
                                {
                                    FarPoint.Win.Spread.CellType.NumberCellType p = new FarPoint.Win.Spread.CellType.NumberCellType();
                                    p.DecimalPlaces = decimalPlaces;
                                    this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.RowCount - 1, colIndex].CellType = p;

                                }
                                this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.RowCount - 1, colIndex].Formula = formula;
                                if (colLetter.Split('|').Length > 4)
                                {
                                    try
                                    {
                                        string t = (colLetter.Split('|')[4]).ToString();
                                        if (t.Split('.').Length > 1)
                                        {
                                            this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.RowCount - 1, FS.FrameWork.Function.NConvert.ToInt32(t.Split('.')[0])].Value = (t.Split('|')[1]).ToString();
                                        }
                                        else
                                        {
                                            this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.RowCount - 1, 0].Value = t;
                                        }
                                    }
                                    catch (Exception e)
                                    {
                                        this.ShowBalloonTip(5000, "温馨提示", e.Message + "\n解析公司说明错误" + colLetter);
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            this.ShowBalloonTip(5000, "温馨提示", ex.Message + "\n公式计算已经忽略列" + colLetter);
                            continue;
                        }
                    }
                }
                else
                {
                    return 0;
                }

            }
            catch (Exception ex)
            {
                //this.ShowBalloonTip(2000, "温馨提示", ex.Message + "\n求和时已经忽略");
                return -1;
            }
            return 0;
        }

        /// <summary>
        /// 排序
        /// </summary>
        private void sortTot()
        {
            if (this.SortColIndexs == null || this.SortColIndexs == string.Empty)
            {
                return;
            }
            string[] cols = this.SortColIndexs.Split(' ', ',', '|');
            foreach (string colLetter in cols)
            {
                try
                {
                    if (string.IsNullOrEmpty(colLetter))
                    {
                        continue;
                    }
                    int colIndex = FS.FrameWork.Function.NConvert.ToInt32(colLetter);
                    if (colIndex > this.fpSpread1_Sheet1.ColumnCount - 1)
                    {
                        continue;
                    }
                    this.fpSpread1_Sheet1.Columns[colIndex].AllowAutoSort = true;
                    this.fpSpread1_Sheet1.Columns[colIndex].ShowSortIndicator = true;
                }
                catch (Exception ex)
                {
                    this.ShowBalloonTip(5000, "温馨提示", ex.Message + "\n排序时已经忽略列" + colLetter);
                }
            }
        }

        /// <summary>
        /// 明细排序
        /// </summary>
        private void sortDetail()
        {
            if (this.DetailSortColIndexs == null || this.DetailSortColIndexs == string.Empty)
            {
                return;
            }
            string[] cols = this.DetailSortColIndexs.Split(' ', ',', '|');
            foreach (string colLetter in cols)
            {
                try
                {
                    if (string.IsNullOrEmpty(colLetter))
                    {
                        continue;
                    }
                    int colIndex = FS.FrameWork.Function.NConvert.ToInt32(colLetter);
                    if (colIndex > this.fpSpread1_Sheet2.ColumnCount - 1)
                    {
                        continue;
                    }
                    this.fpSpread1_Sheet2.Columns[colIndex].AllowAutoSort = true;
                    this.fpSpread1_Sheet2.Columns[colIndex].ShowSortIndicator = true;
                }
                catch (Exception ex)
                {
                    this.ShowBalloonTip(5000, "温馨提示", ex.Message + "\n明细排序时已经忽略列" + colLetter);
                    continue;
                }
            }
        }

        /// <summary>
        /// 合并数据
        /// </summary>
        private void mergeData()
        {
            if (!this.IsNeedMergeData)
            {
                return;
            }
            if (this.MergeDataColIndexs == null || this.MergeDataColIndexs == string.Empty)
            {
                this.ShowBalloonTip(5000, "温馨提示", "没有指定合并数据的列");
            }
            string[] mergeCols = this.MergeDataColIndexs.Split(' ', ',', '|');
            foreach (string colLetter in mergeCols)
            {
                try
                {
                    if (string.IsNullOrEmpty(colLetter))
                    {
                        continue;
                    }
                    int colIndex = FS.FrameWork.Function.NConvert.ToInt32(colLetter);
                    if (colIndex > this.fpSpread1_Sheet1.ColumnCount - 1)
                    {
                        continue;
                    }
                    this.fpSpread1_Sheet1.Columns[colIndex].MergePolicy = FarPoint.Win.Spread.Model.MergePolicy.Always;
                }
                catch (Exception ex)
                {
                    this.ShowBalloonTip(5000, "温馨提示", ex.Message + "\n合并数据时已经忽略列" + colLetter);
                    continue;
                }
            }
        }
        /// <summary>
        /// 读取配置文件
        /// </summary>
        /// <param name="activeSheetIndex"></param>
        private void readSetting(int activeSheetIndex)
        {
            if (activeSheetIndex == 0)
            {
                if (this.SettingFilePatch != string.Empty)
                {
                    if (System.IO.File.Exists(this.SettingFilePatch))
                    {
                        FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.fpSpread1_Sheet1, this.SettingFilePatch);
                    }
                }
            }
            if (activeSheetIndex == 1)
            {
                if (this.DetailSettingFilePatch != string.Empty)
                {
                    if (System.IO.File.Exists(this.DetailSettingFilePatch))
                    {
                        FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.fpSpread1_Sheet2, this.DetailSettingFilePatch);
                    }
                }
            }
        }

        private void Filter(string filterCode)
        {
            if (this.OperationStartHandler != null)
            {
                this.OperationStartHandler("filter");
            }


            if (this.filerType == EnumFilterType.不过滤)
            {
                return;
            }
            if (this.filerType == EnumFilterType.汇总过滤 || this.filerType == EnumFilterType.汇总明细同时过滤)
            {
                if (this.filters == null || this.filters.Length == 0)
                {
                    this.ShowBalloonTip(5000, "温馨提示", "没有设置过滤字段\n已经忽略");
                    return;
                }
                string filter = "1=1";
                string[] tmpFelters = filters.Split(',', ' ', '|');
                foreach (string field in tmpFelters)
                {
                    if (string.IsNullOrEmpty(field))
                    {
                        continue;
                    }
                    try
                    {
                        if (this.fpSpread1_Sheet1.DataSource != null)
                        {
                            this.dataView.RowFilter = "(" + field + " LIKE '%" + filterCode + "%') ";
                        }
                    }
                    catch (Exception ex)
                    {
                        this.ShowBalloonTip(5000, "温馨提示", ex.Message + "\n已经忽略" + field);
                        continue;
                    }
                    if (filter == "1=1")
                    {
                        filter = "(" + field + " LIKE '%" + filterCode + "%') ";
                    }
                    else
                    {
                        filter += " OR (" + field + " LIKE '%" + filterCode + "%') ";
                    }
                    this.dataView.RowFilter = filter;

                    this.readSetting(0);
                }
                try
                {
                    if (this.fpSpread1_Sheet1.DataSource != null)
                    {
                        this.dataView.RowFilter = filter;
                    }
                }
                catch (Exception ex)
                {
                    this.ShowBalloonTip(5000, "温馨提示", ex.Message + "\n已经忽略");
                }
            }
            if (this.filerType == EnumFilterType.明细过滤 || this.filerType == EnumFilterType.汇总明细同时过滤)
            {
                if (this.detailFilters == null || this.detailFilters.Length == 0)
                {
                    this.ShowBalloonTip(5000, "温馨提示", "没有设置明细过滤字段\n已经忽略");
                    return;
                }
                string filter = "1";
                string[] tmpFelters = detailFilters.Split(',', ' ', '|');
                foreach (string field in tmpFelters)
                {
                    if (string.IsNullOrEmpty(field))
                    {
                        continue;
                    }
                    try
                    {
                        if (this.fpSpread1_Sheet2.DataSource != null)
                        {
                            this.detailDataView.RowFilter = "(" + field + " LIKE '%" + filterCode + "%') ";
                        }
                    }
                    catch (Exception ex)
                    {
                        this.ShowBalloonTip(5000, "温馨提示", ex.Message + "\n已经忽略" + field);
                        continue;
                    }
                    if (filter == "1")
                    {
                        filter = "(" + field + " LIKE '%" + filterCode + "%') ";
                    }
                    else
                    {
                        filter += " OR (" + field + " LIKE '%" + filterCode + "%') ";
                    }
                }
                try
                {
                    if (this.fpSpread1_Sheet2.DataSource != null)
                    {
                        this.detailDataView.RowFilter = filter;
                        this.readSetting(1);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(filter + ex.Message);
                }
            }


            if (this.OperationEndHandler != null)
            {
                this.OperationEndHandler("filter");
            }
        }

        
        #endregion

        #region 公有

        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        public int Init()
        {
            this.setControlWithPerporty();

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

            this.fpSpread1.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(fpSpread1_CellDoubleClick);
            this.txtFilter.TextChanged += new EventHandler(txtFilter_TextChanged);
            this.txtFilter.KeyPress += new KeyPressEventHandler(txtFilter_KeyPress);

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
            string deptNO = ((FS.HISFC.Models.Base.Employee)priPowerMgr.Operator).Dept.ID;

            if (this.IsDeptAsCondition && this.IsTimeAsCondition)
            {
                string[] parm = { "", "", "", priPowerMgr.Operator.ID, deptNO };
                if (this.cmbDept.Tag != null && this.cmbDept.Tag.ToString() != "")
                {
                    parm[0] = this.cmbDept.Tag.ToString();
                }
                parm[1] = this.dtStart.Value.ToString();
                parm[2] = this.dtEnd.Value.ToString();

                return parm;
            }
            if (!this.IsDeptAsCondition && this.IsTimeAsCondition)
            {
                string[] parm = { "", "", priPowerMgr.Operator.ID, deptNO };
                parm[0] = this.dtStart.Value.ToString();
                parm[1] = this.dtEnd.Value.ToString();

                return parm;
            }
            if (this.IsDeptAsCondition && !this.IsTimeAsCondition)
            {
                string[] parm = { "", priPowerMgr.Operator.ID, deptNO };
                if (this.cmbDept.Tag != null && this.cmbDept.Tag.ToString() != "")
                {
                    parm[0] = this.cmbDept.Tag.ToString();
                }

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
            this.FilerType = EnumFilterType.不过滤;
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

            this.resetTitleLocation();

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
            return this.PrintData(paperName, paperWidth, paperHeight, this.PrinterName);
        }

        /// <summary>
        /// 打印数据
        /// </summary>
        /// <param name="paperName">纸张名称</param>
        /// <param name="paperWidth">宽度</param>
        /// <param name="paperHeight">高度</param>
        /// <param name="printerName">打印机名称</param>
        /// <returns></returns>
        public int PrintData(string paperName, int paperWidth, int paperHeight, string printerName)
        {
            if (this.OperationStartHandler != null)
            {
                this.OperationStartHandler("print");
            }
            int parm = this.printData(paperName, paperWidth, paperHeight, printerName);
            if (this.OperationEndHandler != null)
            {
                this.OperationEndHandler("print");
            }
            return parm;
        }




        /// <summary>
        /// 汇总求和
        /// </summary>
        /// <returns></returns>
        public int SumTot()
        {
            return this.sumTot();
        }

        /// <summary>
        /// 明细求和
        /// </summary>
        /// <returns></returns>
        public int SumDetail()
        {
            return this.sumDetail();
        }

        /// <summary>
        /// 汇总求平均
        /// </summary>
        /// <returns></returns>
        public int AverageTot()
        {
            return this.avgTot();
        }

        /// <summary>
        /// 明细求平均
        /// </summary>
        /// <returns></returns>
        public int AverageDetail()
        {
            return this.avgDetail();
        }


        /// <summary>
        /// 提示信息
        /// </summary>
        public void ShowBalloonTip(int timeOut, string title, string tipText)
        {
            this.FindForm().ShowInTaskbar = true;
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.ShowBalloonTip(timeOut, title, tipText, ToolTipIcon.Info);
        }
        #endregion

        #endregion

        #region 事件
        protected override void OnLoad(EventArgs e)
        {
            if (this.DesignMode)
            {
                return;
            }
            this.Init();
            this.fpSpread1.ColumnWidthChanged += new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(fpSpread1_ColumnWidthChanged);
            this.fpSpread1.ActiveSheetChanged += new EventHandler(fpSpread1_ActiveSheetChanged);
            base.OnLoad(e);
        }

        void fpSpread1_ActiveSheetChanged(object sender, EventArgs e)
        {
            this.resetTitleLocation();
        }

        void fpSpread1_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            if (e.View.ActiveSheetIndex == 0)
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpSpread1_Sheet1, this.SettingFilePatch);
            }
            else
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpSpread1_Sheet2, this.DetailSettingFilePatch);
            }

            this.resetTitleLocation();
        }
        private void fpSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.detailDataQueryType != EnumQueryType.同条件同步查询 && e.View.ActiveSheetIndex == 0)
            {
                this.queryDetailData();
                this.fpSpread1.ActiveSheetIndex = 1;
            }
            if (this.DetailDoubleClickEnd != null)
            {
                DetailDoubleClickEnd();
            }
        }

        void txtFilter_TextChanged(object sender, EventArgs e)
        {
            if (!this.FilterAfterEnterKey)
            {
                this.Filter(this.txtFilter.Text);
            }
        }

        void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (this.FilterAfterEnterKey && e.KeyChar == (char)13)
            {
                this.Filter(this.txtFilter.Text);
            }
        }

        void ncmbTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] times = this.ncmbTime.Text.Split('到', '|', '至');
            try
            {
                this.dtStart.Value = FS.FrameWork.Function.NConvert.ToDateTime(times[0].Trim());
                this.dtEnd.Value = FS.FrameWork.Function.NConvert.ToDateTime(times[1].Trim());
            }
            catch (Exception ex)
            {
                this.ShowBalloonTip(10, "错误", "时间格式错误：" + this.ncmbTime.SelectedText + "\n正确格式：yyyy-MM-dd hh24:mm:ss 到 yyyy-MM-dd hh24:mm:ss");
            }
        }

        #endregion

        #region 工具栏信息

        protected override int OnQuery(object sender, object neuObject)
        {
            this.QueryData();
            if (QueryEndHandler != null)
            {
                QueryEndHandler();
            }
            return base.OnQuery(sender, neuObject);
        }

        public override int Export(object sender, object neuObject)
        {
            this.ExportData();
            return base.Export(sender, neuObject);
        }

        public override int Print(object sender, object neuObject)
        {
            this.PrintData();
            return base.Print(sender, neuObject);
        }
        #endregion

        #region 枚举

        /// <summary>
        /// 过滤类型
        /// </summary>
        public enum EnumFilterType
        {
            /// <summary>
            /// 汇总过滤
            /// </summary>
            汇总过滤,

            /// <summary>
            /// 明细过滤
            /// </summary>
            明细过滤,

            /// <summary>
            /// 汇总明细同时过滤
            /// </summary>
            汇总明细同时过滤,

            /// <summary>
            /// 不过滤
            /// </summary>
            不过滤
        };

        /// <summary>
        /// 查询类型
        /// </summary>
        public enum EnumQueryType
        {
            同条件同步查询,
            活动行取参数,
            同条件异步查询,
            汇总条件活动行
        }

        ///// <summary>
        ///// 纸张
        ///// </summary>
        //public struct Paper
        //{
        //    public Paper()
        //    {}
        //    /// <summary>
        //    /// 纸张名称
        //    /// </summary>
        //    public string Name;

        //    /// <summary>
        //    /// 纸张宽度
        //    /// </summary>
        //    public int With;

        //    /// <summary>
        //    /// 纸张高度
        //    /// </summary>
        //    public int Height;
        //}
        #endregion
    }

}
