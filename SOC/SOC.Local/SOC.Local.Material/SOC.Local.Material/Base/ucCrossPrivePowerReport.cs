using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.Local.Material.Base
{
    public partial class ucCrossPrivePowerReport: FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucCrossPrivePowerReport()
        {
            InitializeComponent();
        }

        #region 变量
        private string deptCode = string.Empty;
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
        /// 开始时间
        /// </summary>
        protected DateTime beginTime = DateTime.MinValue;
        /// <summary>
        /// 结束时间
        /// </summary>
        protected DateTime endTime = DateTime.MinValue;
        /// <summary>
        /// 
        /// </summary>
        protected FS.FrameWork.Management.DataBaseManger dataBaseManager = new FS.FrameWork.Management.DataBaseManger();
        private FS.HISFC.BizLogic.Manager.UserPowerDetailManager priPowerMgr = new FS.HISFC.BizLogic.Manager.UserPowerDetailManager();
        /// <summary>
        /// 登录人员信息
        /// </summary>
        protected FS.HISFC.Models.Base.Employee employee = null;
        /// <summary>
        /// 查询用的语句
        /// </summary>
        private string querySql = string.Empty;
        /// <summary>
        /// 分页符所在的行数10
        /// </summary>
        private int rowPageBreak = -1;
        /// <summary>
        /// 主表格配置文件的位置
        /// </summary>
        private string mainSheetXmlFileName = string.Empty;
        /// <summary>
        /// 显示开始行的索引
        /// </summary>
        private int dataBeginRowIndex = 0;
        /// <summary>
        /// 显示开始列的索引
        /// </summary>
        private int dataBeginColumnIndex = 0;
        /// <summary>
        /// 行标题开始行的索引
        /// </summary>
        private int rowsHeaderBeginRowIndex = 0;
        /// <summary>
        /// 行标题开始列的索引 
        /// </summary>
        private int rowsHeaderBeginColumnIndex = 0;
        /// <summary>
        /// 列标题开始行的索引
        /// </summary>
        private int columnsHeaderBeginRowIndex = 0;
        /// <summary>
        /// 列标题开始列的索引
        /// </summary>
        private int columnsHeaderBeginColumnIndex = 0;
        /// <summary>
        /// 列总计级别
        /// </summary>
        private string[] columnsTotalLevel = new string[0];
        /// <summary>
        /// 列总计级别
        /// </summary>
        [Category("报表参数"), Description("设置数据集中列总计级别！")]
        public string ColumnsTotalLevel
        {
            get
            {
                string rtn = string.Empty;
                if (columnsTotalLevel != null)
                {
                    //Array.Reverse(dataCrossColumns);
                    rtn = string.Join("|", this.columnsTotalLevel);
                }
                return rtn;
            }
            set
            {
                columnsTotalLevel = value.Split('|');

            }
        }
        /// <summary>
        /// 行总计级别
        /// </summary>
        private string[] rowsTotalLevel = new string[0];
        /// <summary>
        /// 行总计级别
        /// </summary>
        [Category("报表参数"), Description("设置数据集中行总计级别！")]
        public string RowsTotalLevel
        {
            get
            {
                string rtn = string.Empty;
                if (rowsTotalLevel != null)
                {
                    //Array.Reverse(dataCrossColumns);
                    rtn = string.Join("|", this.rowsTotalLevel);
                }
                return rtn;
            }
            set
            {
                rowsTotalLevel = value.Split('|');
            }
        }
        /// <summary>
        /// 数据交叉行
        /// </summary>
        private string[] dataCrossColumns = new string[0];

        private bool isRowTotFrontShow = false;

        private bool isColumnTotFrontShow = false;

        private int frozenColumnCount = 0;

        private int frozenRowCount = 0;

        [Category("报表参数"), Description("报表列固定数！（含隐藏）")]
        public int FrozenColumnCount
        {
            get { return frozenColumnCount; }
            set { frozenColumnCount = value; }
        }

        [Category("报表参数"), Description("报表行固定数！（含隐藏）")]
        public int FrozenRowCount
        {
            get { return frozenRowCount; }
            set { frozenRowCount = value; }
        }

        [Category("报表参数"), Description("行合计是否前端显示！")]
        public bool IsRowTotFrontShow
        {
            get { return isRowTotFrontShow; }
            set { isRowTotFrontShow = value; }
        }

        [Category("报表参数"), Description("列合计是否前端显示！")]
        public bool IsColumnTotFrontShow
        {
            get { return isColumnTotFrontShow; }
            set { isColumnTotFrontShow = value; }
        }
        /// <summary>
        /// 设置数据集中用于形成数据交叉行的列，
        /// </summary>
        [Category("报表参数"), Description("设置数据集中用于形成数据交叉行的列！")]
        public string DataCrossColumns
        {
            get
            {
                string rtn = string.Empty;
                if (dataCrossColumns != null)
                {
                    //Array.Reverse(dataCrossColumns);
                    rtn = string.Join("|", this.dataCrossColumns);
                }
                return rtn;
            }
            set
            {
                dataCrossColumns = value.Split('|');
                ComputeIndex();
            }
        }
        /// <summary>
        /// 数据交叉列
        /// </summary>
        private string[] dataCrossRows = new string[0];
        /// <summary>
        /// 设置数据集中用于形成数据交叉列的列，
        /// </summary>
        [Category("报表参数"), Description("设置数据集中用于形成数据交叉列的列！")]
        public string DataCrossRows
        {
            get
            {
                string rtn = string.Empty;
                if (dataCrossRows != null)
                {
                    rtn = string.Join("|", this.dataCrossRows);
                }
                return rtn;
            }
            set
            {
                dataCrossRows = value.Split('|');
                ComputeIndex();
            }
        }
        /// <summary>
        /// 数据交叉值
        /// </summary>
        private string[] dataCrossValues = new string[0];
        /// <summary>
        /// 设置数据集中用于形成数据交叉值的列，
        /// </summary>
        [Category("报表参数"), Description("设置数据集中用于形成数据交叉值的列！")]
        public string DataCrossValues
        {
            get
            {
                string rtn = string.Empty;
                if (dataCrossValues != null)
                {
                    rtn = string.Join("|", this.dataCrossValues);
                }
                return rtn;
            }
            set
            {
                dataCrossValues = value.Split('|');
                ComputeIndex();
            }
        }
        /// <summary>
        /// 查询参数
        /// </summary>
        private System.Collections.Generic.List<FS.FrameWork.Models.NeuObject> queryParams = new List<FS.FrameWork.Models.NeuObject>();
        /// <summary>
        /// 数据行数
        /// </summary>
        protected int dataRowCount = 0;
        /// <summary>
        /// 查询类型
        /// </summary>
        private QuerySqlType querySqlTypeValue = QuerySqlType.id;
        DataBaseLogic db = new DataBaseLogic();
        private FS.HISFC.BizLogic.Manager.Constant con = new FS.HISFC.BizLogic.Manager.Constant();
        private string hospitalName = string.Empty;
        private int useParamCellsCount = 0;
        #endregion

        #region 属性
        [Category("报表参数"), Description("设置报表查询时需要用参数替换的单元格数量！")]
        public int UseParamCellsCount
        {
            get { return useParamCellsCount; }
            set { useParamCellsCount = value; }
        }
        public string HospitalName
        {
            get
            {
                if (DesignMode == false)
                {
                    if (string.IsNullOrEmpty(hospitalName) == true)
                    {
                        hospitalName = con.GetHospitalName();
                    }
                }
                return hospitalName;
            }
            set { hospitalName = value; }
        }
        public DataBaseLogic Db
        {
            get { return db; }
            set { db = value; }
        }

        public FarPoint.Win.Spread.SheetView SvMain
        {
            get
            {
                if (this.fpSpread1.Sheets.Count > 0)
                {
                    return this.fpSpread1.Sheets[0];
                }
                return null;
            }
            set
            {
                if (this.fpSpread1.Sheets.Count > 0)
                {
                    this.fpSpread1.Sheets[0] = value;
                }
            }
        }

        [Category("报表参数"), Description("设置报表查询时的参数！")]
        public System.Collections.Generic.List<FS.FrameWork.Models.NeuObject> QueryParams
        {
            get { return queryParams; }
            set { queryParams = value; }
        }
        [Category("报表参数"), Description("设置报表查询时使用的sql类型！")]
        public QuerySqlType QuerySqlTypeValue
        {
            get { return querySqlTypeValue; }
            set { querySqlTypeValue = value; }
        }
        ///// <summary>
        ///// 设置数据集中用于显示的列，
        ///// </summary>
        //[Category("报表参数"), Description("设置报表需要显示的数据列！")]
        //public string DataDisplayColumns
        //{
        //    get
        //    {
        //        string rtn = string.Empty;
        //        if (dataDisplayColumns != null)
        //        {
        //            rtn = string.Join("|", this.dataDisplayColumns);
        //        }
        //        return rtn;
        //    }
        //    set
        //    {
        //        dataDisplayColumns = value.Split('|');
        //        rowsHeaderBeginColumnIndex = dataBeginRowIndex;
        //        if (this.dataCrossValues.Length > 1)
        //        {
        //            rowsHeaderBeginRowIndex = dataBeginColumnIndex + dataCrossRows.Length + 1;
        //        }
        //        else
        //        {
        //            rowsHeaderBeginRowIndex = dataBeginColumnIndex + dataCrossRows.Length;
        //        }
        //        columnsHeaderBeginColumnIndex = dataBeginRowIndex + dataCrossColumns.Length; ;
        //        columnsHeaderBeginRowIndex = dataBeginRowIndex;
        //    }
        //}
        [Category("报表参数"), Description("设置报表查询出的数据集，显示的起始行的索引！")]
        public int DataBeginRowIndex
        {
            get
            {
                return dataBeginRowIndex;
            }
            set
            {
                dataBeginRowIndex = value;
                ComputeIndex();
            }
        }
        [Category("报表参数"), Description("设置报表查询出的数据集，显示的起始列的索引！")]
        public int DataBeginColumnIndex
        {
            get
            {
                return dataBeginColumnIndex;
            }
            set
            {
                dataBeginColumnIndex = value;
                ComputeIndex();
            }
        }
        [Category("报表参数"), Description("设置报表主表格配置文件名！")]
        public string MainSheetXml
        {
            get { return mainSheetXmlFileName; }
            set { mainSheetXmlFileName = value; }
        }

        [EditorAttribute(typeof(System.ComponentModel.Design.MultilineStringEditor), typeof(System.Drawing.Design.UITypeEditor)), Category("报表参数"), Description("设置报表查询时使用的sql！")]
        public string QuerySql
        {
            get { return querySql; }
            set { querySql = value; }
        }
        /// <summary>
        /// 数据窗报表Title的Text控件名称
        /// </summary>
        [Category("报表参数"), Description("每页显示的数据行")]
        public int PageRowCount
        {
            get { return rowPageBreak; }
            set { rowPageBreak = value; }
        }
        #endregion

        #region 方法

        /// <summary>
        /// 获得查询时间
        /// </summary>
        /// <returns>成功 1 失败 -1</returns>
        protected virtual int GetQueryTime()
        {
            if (this.dtpEndTime.Value < this.dtpBeginTime.Value)
            {
                MessageBox.Show("结束时间不能小于开始时间");

                return -1;
            }

            this.beginTime = this.dtpBeginTime.Value;
            this.endTime = this.dtpEndTime.Value;

            return 1;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns>成功 1 失败 -1</returns>
        protected int Init()
        {
            OpenSpread();

            System.DateTime dtNow = this.dataBaseManager.GetDateTimeFromSysDateTime();
            this.dtpBeginTime.Value = new DateTime(dtNow.Year, dtNow.Month, dtNow.Day, 0, 0, 0);
            this.dtpEndTime.Value = new DateTime(dtNow.Year, dtNow.Month, dtNow.Day, 23, 59, 59);
            if (SvMain.RowCount > this.DataBeginRowIndex + 1)
            {
                SvMain.ClearRange(this.DataBeginRowIndex + 1, 0, SvMain.Rows.Count - 1, SvMain.Columns.Count - 1, false);
            }            
            this.fpSpread1.SelectionChanged += new FarPoint.Win.Spread.SelectionChangedEventHandler(neuSpread1_SelectionChanged);

            this.lbMainTitle.Text = this.MainTitle;
            if (string.IsNullOrEmpty(this.MainTitle))
            {
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

            this.initDeptByPriPower();

            return 1;
        }

        void neuSpread1_SelectionChanged(object sender, FarPoint.Win.Spread.SelectionChangedEventArgs e)
        {
            OnSort();
        }

        void neuSpread1_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            OnSort();
        }

        /// <summary>
        /// 自行设计查询条件的查询,继承用
        /// </summary>
        /// <returns>成功 1 失败 -1</returns>
        protected virtual int OnRetrieve(params object[] objects)
        {
            if (SvMain != null)
            {
                //SvMain.Retrieve(objects);
            }

            return 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        public override int Export(object sender, object neuObject)
        {
            OnExport();
            return base.Export(sender, neuObject);
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <returns>成功 1 失败 -1</returns>
        protected virtual int OnExport()
        {
            if (SvMain == null)
            {
                return -1;
            }

            System.Windows.Forms.SaveFileDialog sfd = new SaveFileDialog();
            sfd.DefaultExt = ".xls";
            sfd.Filter = "Microsoft Excel 工作薄 (*.xls)|*.*";
            if (sfd.ShowDialog() == DialogResult.Cancel)
            {
                return 1;
            }

            this.fpSpread1.SaveExcel(sfd.FileName, FarPoint.Win.Spread.Model.IncludeHeaders.BothCustomOnly);
            return 1;
        }

        /// <summary>
        /// 预览
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnPrintPreview(object sender, object neuObject)
        {
            if (this.SvMain != null)
            {
                //frmPreviewDataWindow frmPreview = new frmPreviewDataWindow();

                ////frmPreview.PreviewDataWindow = SvMain;

                //if (frmPreview.ShowDialog() == DialogResult.OK)
                //{ }
                //    //this.SvMain.PrintProperties.Preview = false;
                //this.neuSpread1.OwnerPrintDraw(
            }

            return base.OnPrintPreview(sender, neuObject);


        }

        public override int Print(object sender, object neuObject)
        {
            this.printData();
            return base.Print(sender, neuObject);
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

        #region 枚举
        public enum QuerySqlType
        {
            /// <summary>
            /// id
            /// </summary>
            id,
            /// <summary>
            /// 
            /// </summary>
            text,
        }
        #endregion

        #region 事件
        protected void OpenSpread()
        {
            if (string.IsNullOrEmpty(this.MainSheetXml) == false)
            {
                this.fpSpread1.Open(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "Report\\" + this.MainSheetXml);
            }
            
            if (this.fpSpread1.Sheets.Count > 0)
            {
                string f = string.Empty;

                SvMain = this.fpSpread1.Sheets[0];
                SvMain.OperationMode = FarPoint.Win.Spread.OperationMode.ReadOnly;
            }
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnQuery(object sender, object neuObject)
        {
            Cursor = Cursors.WaitCursor;

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在查询数据,请等待....");

            this.GetConditions();

            Application.DoEvents();

            this.Query();

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            Cursor = Cursors.Arrow;

            this.SetTitle();

            return 1;
        }
        protected virtual int SpanDisplayedFp(FarPoint.Win.Spread.SheetView sv, DataTable dt, int beginRowIdx, int beginColumnIdx)
        {
            int ColumnSpan = 0;
            int dtRowIdx = 0;
            string tempVal = string.Empty;

            foreach (DataRow dr in dt.Rows)
            {
                dtRowIdx = dt.Rows.IndexOf(dr);
                int dtColumnIdx = 0;
                ColumnSpan = 1;
                tempVal = string.Empty;
                foreach (DataColumn dc in dt.Columns)
                {
                    dtColumnIdx = (dt.Columns.Count - 1) - dt.Columns.IndexOf(dc);
                    tempVal = dr[dtColumnIdx].ToString();
                    if (string.IsNullOrEmpty(tempVal) == false)
                    {
                        break;
                    }
                    else
                    {
                        ColumnSpan++;
                    }
                }
                sv.Cells[dtRowIdx + beginRowIdx, dtColumnIdx + beginColumnIdx].ColumnSpan = ColumnSpan;
                //sv.Cells[dtRowIdx + beginRowIdx, dtColumnIdx + beginColumnIdx].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                sv.Cells[dtRowIdx + beginRowIdx, dtColumnIdx + beginColumnIdx].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

                if (sv.Cells[dtRowIdx + beginRowIdx, dtColumnIdx + beginColumnIdx].Text == "总计")
                {
                    sv.Cells[dtRowIdx + beginRowIdx, dtColumnIdx + beginColumnIdx].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                }
            }
            return 1;
        }
        
        protected virtual int SpanDisplayedFpReverseOneValue(FarPoint.Win.Spread.SheetView sv, DataTable dt, int beginRowIdx, int beginColumnIdx)
        {
            int RowSpan = 0;
            int dtRowIdx = 0;
            string tempVal = string.Empty;
            foreach (DataRow dr in dt.Rows)
            {
                dtRowIdx = dt.Rows.IndexOf(dr);
                int dtColumnIdx = 0;
                RowSpan = 1;
                tempVal = string.Empty;
                foreach (DataColumn dc in dt.Columns)
                {
                    dtColumnIdx = (dt.Columns.Count - 1) - dt.Columns.IndexOf(dc);
                    tempVal = dr[dtColumnIdx].ToString();
                    if (string.IsNullOrEmpty(tempVal) == false)
                    {
                        break;
                    }
                    else
                    {
                        RowSpan++;
                    }
                }
                sv.Cells[dtColumnIdx + beginRowIdx, dtRowIdx + beginColumnIdx].RowSpan = RowSpan;
                sv.Cells[dtColumnIdx + beginRowIdx, dtRowIdx + beginColumnIdx].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                sv.Cells[dtColumnIdx + beginRowIdx, dtRowIdx + beginColumnIdx].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            }
            return 1;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sv"></param>
        /// <param name="dt"></param>
        /// <param name="beginRowIdx"></param>
        /// <param name="beginColumnIdx"></param>
        /// <returns></returns>
        protected virtual int SpanDisplayedFpReverse(FarPoint.Win.Spread.SheetView sv, DataTable dt, int beginRowIdx, int beginColumnIdx)
        {
            int RowSpan = 0;
            int dtRowIdx = 0;
            string tempVal = string.Empty;
            foreach (DataRow dr in dt.Rows)
            {
                dtRowIdx = dt.Rows.IndexOf(dr);
                int dtColumnIdx = 0;
                RowSpan = 1;
                tempVal = string.Empty;
                foreach (DataColumn dc in dt.Columns)
                {
                    dtColumnIdx = (dt.Columns.Count - 1) - dt.Columns.IndexOf(dc);
                    if (dtColumnIdx < dt.Columns.Count - 1)
                    {
                        tempVal = dr[dtColumnIdx].ToString();
                        if (string.IsNullOrEmpty(tempVal) == false)
                        {
                            break;
                        }
                        else
                        {
                            RowSpan++;
                        }
                    }
                }
                sv.Cells[dtColumnIdx + beginRowIdx, dtRowIdx + beginColumnIdx].RowSpan = RowSpan;
                sv.Cells[dtColumnIdx + beginRowIdx, dtRowIdx + beginColumnIdx].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                sv.Cells[dtColumnIdx + beginRowIdx, dtRowIdx + beginColumnIdx].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            }
            return 1;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sv"></param>
        /// <param name="dt"></param>
        /// <param name="beginRowIdx"></param>
        /// <param name="beginColumnIdx"></param>
        /// <returns></returns>
        protected virtual int SpanDisplayedFpReverseRowsOneValue(FarPoint.Win.Spread.SheetView sv, DataTable dt, int beginRowIdx, int beginColumnIdx)
        {
            int ColumnSpan = 0;
            string tempVal = string.Empty;
            int dtColumnIdx = 0;
            int spanRowIdx = 0;
            int spanColumnIdx = 0;
            foreach (DataColumn dc in dt.Columns)
            {
                dtColumnIdx = dt.Columns.IndexOf(dc);
                if (dtColumnIdx < dt.Columns.Count)
                {
                    ColumnSpan = 1;
                    tempVal = string.Empty;
                    int dtRowIdx = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        dtRowIdx = dt.Rows.IndexOf(dr);

                        //tempVal = dr[dtColumnIdx].ToString();
                        if (string.IsNullOrEmpty(tempVal) == false)
                        {
                            if (tempVal == dr[dtColumnIdx].ToString())
                            {
                                ColumnSpan++;
                                if (dtRowIdx == dt.Rows.Count - 1)
                                {
                                    //tempVal = dr[dtColumnIdx].ToString();
                                    //spanRowIdx = dtRowIdx;
                                    //spanColumnIdx = dtColumnIdx;
                                    sv.Cells[spanColumnIdx + beginRowIdx, spanRowIdx + beginColumnIdx].ColumnSpan = ColumnSpan;
                                    sv.Cells[spanColumnIdx + beginRowIdx, spanRowIdx + beginColumnIdx].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                                    sv.Cells[spanColumnIdx + beginRowIdx, spanRowIdx + beginColumnIdx].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                                }
                            }
                            else
                            {
                                sv.Cells[spanColumnIdx + beginRowIdx, spanRowIdx + beginColumnIdx].ColumnSpan = ColumnSpan;
                                sv.Cells[spanColumnIdx + beginRowIdx, spanRowIdx + beginColumnIdx].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                                sv.Cells[spanColumnIdx + beginRowIdx, spanRowIdx + beginColumnIdx].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                                tempVal = dr[dtColumnIdx].ToString();
                                spanRowIdx = dtRowIdx;
                                spanColumnIdx = dtColumnIdx;
                                ColumnSpan = 1;
                                // break;

                            }
                        }
                        else
                        {
                            tempVal = dr[dtColumnIdx].ToString();
                            spanRowIdx = dtRowIdx;
                            spanColumnIdx = dtColumnIdx;
                            ColumnSpan = 1;
                            //ColumnSpan++;
                        }

                    }

                }
            }
            return 1;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sv"></param>
        /// <param name="dt"></param>
        /// <param name="beginRowIdx"></param>
        /// <param name="beginColumnIdx"></param>
        /// <returns></returns>
        protected virtual int SpanDisplayedFpReverseRows(FarPoint.Win.Spread.SheetView sv, DataTable dt, int beginRowIdx, int beginColumnIdx)
        {
            int ColumnSpan = 0;
            string tempVal = string.Empty;
            int dtColumnIdx = 0;
            int spanRowIdx = 0;
            int spanColumnIdx = 0;
            foreach (DataColumn dc in dt.Columns)
            {
                dtColumnIdx = dt.Columns.IndexOf(dc);
                if (dtColumnIdx < dt.Columns.Count - 1)
                {
                    int dtRowIdx = 0;
                    ColumnSpan = 1;
                    tempVal = string.Empty;
                    foreach (DataRow dr in dt.Rows)
                    {
                        dtRowIdx = dt.Rows.IndexOf(dr);


                        //tempVal = dr[dtColumnIdx].ToString();
                        if (string.IsNullOrEmpty(tempVal) == false)
                        {
                            if (tempVal == dr[dtColumnIdx].ToString())
                            {
                                ColumnSpan++;
                                //if (dtRowIdx==dt.Rows.Count -1 && dtColumnIdx==0)
                                if (dtRowIdx == dt.Rows.Count - 1)
                                {
                                    //tempVal = dr[dtColumnIdx].ToString();
                                    //spanRowIdx = dtRowIdx;
                                    //spanColumnIdx = dtColumnIdx;
                                    sv.Cells[spanColumnIdx + beginRowIdx, spanRowIdx + beginColumnIdx].ColumnSpan = ColumnSpan;
                                    sv.Cells[spanColumnIdx + beginRowIdx, spanRowIdx + beginColumnIdx].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                                    sv.Cells[spanColumnIdx + beginRowIdx, spanRowIdx + beginColumnIdx].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                                }
                            }
                            else
                            {
                                sv.Cells[spanColumnIdx + beginRowIdx, spanRowIdx + beginColumnIdx].ColumnSpan = ColumnSpan;
                                sv.Cells[spanColumnIdx + beginRowIdx, spanRowIdx + beginColumnIdx].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                                sv.Cells[spanColumnIdx + beginRowIdx, spanRowIdx + beginColumnIdx].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                                tempVal = dr[dtColumnIdx].ToString();
                                spanRowIdx = dtRowIdx;
                                spanColumnIdx = dtColumnIdx;
                                ColumnSpan = 1;
                                //break;

                            }
                        }
                        else
                        {
                            tempVal = dr[dtColumnIdx].ToString();
                            spanRowIdx = dtRowIdx;
                            spanColumnIdx = dtColumnIdx;
                            ColumnSpan = 1;
                            //ColumnSpan++;
                        }

                    }

                }
            }
            return 1;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sv"></param>
        /// <param name="dt"></param>
        /// <param name="beginRowIdx"></param>
        /// <param name="beginColumnIdx"></param>
        /// <returns></returns>
        protected virtual int SpanDisplayedFpRows(FarPoint.Win.Spread.SheetView sv, DataTable dt, int beginRowIdx, int beginColumnIdx)
        {
            int RowsSpan = 0;
            string tempVal = string.Empty;
            int dtColumnIdx = 0;
            int spanRowIdx = 0;
            int spanColumnIdx = 0;
            foreach (DataColumn dc in dt.Columns)
            {
                dtColumnIdx = dt.Columns.IndexOf(dc);
                //最后一列不合并
                if (dtColumnIdx < dt.Columns.Count - 1)
                {
                    int dtRowIdx = 0;
                    RowsSpan = 1;
                    tempVal = string.Empty;
                    foreach (DataRow dr in dt.Rows)
                    {
                        dtRowIdx = dt.Rows.IndexOf(dr);
                        //不为空
                        if (string.IsNullOrEmpty(tempVal) == false)
                        {
                            //相等
                            if (tempVal == dr[dtColumnIdx].ToString())
                            {
                                //tempVal = dr[dtColumnIdx].ToString();
                                RowsSpan++;
                                if (dtRowIdx == dt.Rows.Count - 1)
                                {
                                    //tempVal = dr[dtColumnIdx].ToString();
                                    //spanRowIdx = dtRowIdx;
                                    //spanColumnIdx = dtColumnIdx;
                                    //合并行
                                    sv.Cells[spanRowIdx + beginRowIdx, spanColumnIdx + beginColumnIdx].RowSpan = RowsSpan;
                                    sv.Cells[spanRowIdx + beginRowIdx, spanColumnIdx + beginColumnIdx].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                                    sv.Cells[spanRowIdx + beginRowIdx, spanColumnIdx + beginColumnIdx].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                                }
                            }
                            //不想等
                            else
                            {
                                //合并行
                                sv.Cells[spanRowIdx + beginRowIdx, spanColumnIdx + beginColumnIdx].RowSpan = RowsSpan;
                                sv.Cells[spanRowIdx + beginRowIdx, spanColumnIdx + beginColumnIdx].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                                sv.Cells[spanRowIdx + beginRowIdx, spanColumnIdx + beginColumnIdx].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                                //存新参数
                                tempVal = dr[dtColumnIdx].ToString();
                                spanRowIdx = dtRowIdx;
                                spanColumnIdx = dtColumnIdx;
                                RowsSpan = 1;
                                //break;
                            }
                        }
                        //为空
                        else
                        {
                            tempVal = dr[dtColumnIdx].ToString();
                            spanRowIdx = dtRowIdx;
                            spanColumnIdx = dtColumnIdx;
                            RowsSpan = 1;
                            //RowsSpan++;
                        }
                    }
                }
            }
            return 1;
        }
        /// <summary>
        /// 合计交叉数据表列
        /// </summary>
        /// <param name="dtCrossValues"></param>
        /// <param name="dtCrossColumns"></param>
        /// <param name="fieldIndexsColumns"></param>
        /// <param name="fieldIndexsValues"></param>
        /// <returns></returns>
        protected virtual DataTable TotalCrossDataTableColumns(DataTable dtCrossValues, DataTable dt, string[] fieldIndexsColumns, string[] fieldIndexsValues)
        {
            DataSetHelper dsh = new DataSetHelper();
            #region 交叉数据，用最直观的算法实现
            DataTable dtCrossValuesColumns = dsh.SelectDistinctByIndexs("", dt, fieldIndexsColumns);
            //交叉列数据集的行值数组；
            object[] tempRowsValue;
            //当前计算的行索引
            int computedIdx;

            #region 交叉列数据集的行值数组,的长度为交叉列的长度
            //tempRowsValue[2]
            //─	─	2	─	─
            //┌	─	┬	─	┐
            //│		│		│
            //└	─	┴	─	┘
            tempRowsValue = new object[fieldIndexsColumns.Length];
            computedIdx = 0;

            ///暂存合计值，第一维的长度是，列字段索引的长度
            ///第二维的长度是，值表的行数
            object[,] tempTotalValues;
            //tempTotalValues[2*2,4]          
            //─	─	2	─	×	─	2	─	┼	─	2	─	×	─	2	─	┼	─	2	─	×	─	2	─	┼	─	2	─	×	─	2	─	─
            //┌	─	┬	─	┬	─	┬	─	┬	─	┬	─	┬	─	┬	─	┬	─	┬	─	┬	─	┬	─	┬	─	┬	─	┬	─	┬	─	┐
            //│		│		│		│		│		│		│		│		│		│		│		│		│		│		│		│		│
            //└	─	┴	─	┴	─	┴	─	┴	─	┴	─	┴	─	┴	─	┴	─	┴	─	┴	─	┴	─	┴	─	┴	─	┴	─	┴	─	┘
            //tempTotalValues[2*2,4]          
            //─	─	2	─	×	─	2	─	─	┐
            //┌	─	┬	─	┬	─	┬	─	┐	│
            //│		│		│		│		│	│
            //│		│		│		│		│	4
            //│		│		│		│		│	│
            //│		│		│		│		│	│
            //└	─	┴	─	┴	─	┴	─	┘	│

            tempTotalValues = new object[fieldIndexsColumns.Length * fieldIndexsValues.Length, dtCrossValues.Rows.Count];
            #region 合计值初始化为0
            int valueRowIdxDefault = 0;
            ///合计后的值表
            DataTable dtCrossValuesTotalCross = new DataTable();
            //遍历所有行
            //初始化为“0”
            //─	─	2	─	×	─	2	─	┼	─	2	─	×	─	2	─	┼	─	2	─	×	─	2	─	┼	─	2	─	×	─	2	─	─
            //┌	─	┬	─	┬	─	┬	─	┬	─	┬	─	┬	─	┬	─	┬	─	┬	─	┬	─	┬	─	┬	─	┬	─	┬	─	┬	─	┐
            //│	0	│	0	│	0	│	0	│	0	│	0	│	0	│	0	│	0	│	0	│	0	│	0	│	0	│	0	│	0	│	0	│
            //└	─	┴	─	┴	─	┴	─	┴	─	┴	─	┴	─	┴	─	┴	─	┴	─	┴	─	┴	─	┴	─	┴	─	┴	─	┴	─	┘
            //─	─	2	─	×	─	2	─	─	┐
            //┌	─	┬	─	┬	─	┬	─	┐	│
            //│	0	│	0	│	0	│	0	│	│
            //│	0	│	0	│	0	│	0	│	4
            //│	0	│	0	│	0	│	0	│	│
            //│	0	│	0	│	0	│	0	│	│
            //└	─	┴	─	┴	─	┴	─	┘	│

            foreach (DataRow dr in dtCrossValues.Rows)
            {
                valueRowIdxDefault = dtCrossValues.Rows.IndexOf(dr);
                //遍历所有列
                for (int i = 0; i < dtCrossValuesColumns.Columns.Count; i++)
                {
                    //遍历所有值
                    for (int j = 0; j < fieldIndexsValues.Length; j++)
                    {
                        //初始化为“0”
                        #region {4B8178AF-8ABF-409d-B241-89EB7E311224}
                        //tempTotalValues[i * fieldIndexsValues.Length + j * (fieldIndexsValues.Length - 1), valueRowIdxDefault] = "0";
                        tempTotalValues[i * fieldIndexsValues.Length + j, valueRowIdxDefault] = "0";
                        #endregion
                    }
                }
                //添加空白行在交叉数据集上
                dtCrossValuesTotalCross.Rows.Add(dtCrossValuesTotalCross.NewRow());
            }
            //dtCrossValuesTotalCross
            //─	─	┐
            //┌	┐	│
            //│	│	│
            //│	│	4
            //│	│	│
            //│	│	│
            //└	┘	│


            #endregion
            //循环所有交叉列数据集的行
            while (computedIdx < dtCrossValuesColumns.Rows.Count)
            {
                //取交叉列数据集当前行值
                //─	─	2	─	─
                //┌	─	┬	─	┐
                //│	A	│	C	│
                //└	─	┴	─	┘
                tempRowsValue = dtCrossValuesColumns.Rows[computedIdx].ItemArray;
                #region 计算每一级别的合计值
                //遍历所有值
                for (int j = 0; j < fieldIndexsValues.Length; j++)
                {
                    //因为是列模式添加，所以要每次新建列
                    //dtCrossValuesTotalCross
                    //┌	─	┬	─	┐+……
                    //└	─	┴	─	┘
                    dtCrossValuesTotalCross.Columns.Add(
                        new DataColumn("", dt.Columns[int.Parse(fieldIndexsValues[j].ToString())].DataType));
                }
                //dtCrossValuesTotalCross while循环完以后的样子
                //─	─	2	─	┼	─	2	─	┼	─	2	─	┼	─	2	─	─	┐
                //┌	─	┬	─	┬	─	┬	─	┬	─	┬	─	┬	─	┬	─	┐	┼
                //│		│		│		│		│		│		│		│		│	│
                //│		│		│		│		│		│		│		│		│	4
                //│		│		│		│		│		│		│		│		│	│
                //│		│		│		│		│		│		│		│		│	│
                //└	─	┴	─	┴	─	┴	─	┴	─	┴	─	┴	─	┴	─	┘	┼
                int valueRowIdx = 0;
                //遍历交叉值表累加

                foreach (DataRow dr in dtCrossValues.Rows)
                {
                    //计算值索引(0-3)
                    valueRowIdx = dtCrossValues.Rows.IndexOf(dr);
                    //遍历交叉行数据集每一列，暂存值到累加数组
                    //i(0-1)
                    for (int i = 0; i < dtCrossValuesColumns.Columns.Count; i++)
                    {
                        //遍历所有值
                        //j(0-1)
                        for (int j = 0; j < fieldIndexsValues.Length; j++)
                        {
                            //tempTotalValues[i * (fieldIndexsValues.Length) + j * (fieldIndexsValues.Length - 1), valueRowIdx] = (int.Parse(tempTotalValues[i * (fieldIndexsValues.Length) + j * (fieldIndexsValues.Length - 1), valueRowIdx].ToString()) + int.Parse(dr[computedIdx * fieldIndexsValues.Length + j].ToString())).ToString();
                            switch (dr.Table.Columns[computedIdx * fieldIndexsValues.Length + j].DataType.ToString())
                            {
                                #region {4B8178AF-8ABF-409d-B241-89EB7E311224}
                                case "System.Decimal":
                                    {
                                        //tempTotalValues[i * (fieldIndexsValues.Length)
                                        //    + j * (fieldIndexsValues.Length - 1), valueRowIdx]
                                        //    = (Decimal.Parse(tempTotalValues[i * (fieldIndexsValues.Length)
                                        //    + j * (fieldIndexsValues.Length - 1), valueRowIdx].ToString())
                                        //    + Decimal.Parse(dr[computedIdx * fieldIndexsValues.Length + j].ToString())).ToString();
                                        tempTotalValues[i * fieldIndexsValues.Length
                                            + j, valueRowIdx]
                                            = (Decimal.Parse(tempTotalValues[i * fieldIndexsValues.Length
                                            + j, valueRowIdx].ToString())
                                            + Decimal.Parse(dr[computedIdx * fieldIndexsValues.Length + j].ToString())).ToString();
                                        break;
                                    }
                                case "System.DateTime":
                                    {
                                        tempTotalValues[i * fieldIndexsValues.Length
                                          + j, valueRowIdx] = dr[computedIdx * fieldIndexsValues.Length + j].ToString();
                                        break;
                                    }
                                case "System.String":
                                    {
                                        tempTotalValues[i * fieldIndexsValues.Length
                                          + j, valueRowIdx] = dr[computedIdx * fieldIndexsValues.Length + j].ToString();
                                        break;
                                    }
                                default:
                                    {
                                        tempTotalValues[i * fieldIndexsValues.Length
                                          + j, valueRowIdx]
                                          = (int.Parse(tempTotalValues[i * fieldIndexsValues.Length
                                          + j, valueRowIdx].ToString())
                                            + int.Parse(dr[computedIdx * fieldIndexsValues.Length + j].ToString())).ToString();
                                        break;
                                    }
                                #endregion
                            }
                        }
                        //tempTotalValues[2*2,4]          
                        //─	─	2	─	×	─	2	─	─	┐
                        //┌	─	┬	─	┬	─	┬	─	┐	│
                        //│	1	│	0	│	0	│	0	│	│
                        //│	1	│	0	│	0	│	0	│	4
                        //│	1	│	0	│	0	│	0	│	│
                        //│	1	│	0	│	0	│	0	│	│
                        //└	─	┴	─	┴	─	┴	─	┘	│
                    }
                    //tempTotalValues[2*2,4]          
                    //─	─	2	─	×	─	2	─	─	┐
                    //┌	─	┬	─	┬	─	┬	─	┐	│
                    //│	1	│	0	│	1	│	0	│	│
                    //│	1	│	0	│	1	│	0	│	4
                    //│	1	│	0	│	1	│	0	│	│
                    //│	1	│	0	│	1	│	0	│	│
                    //└	─	┴	─	┴	─	┴	─	┘	│
                    //遍历所有值，复制原值
                    for (int j = 0; j < fieldIndexsValues.Length; j++)
                    {
                        ///因为结果是空的数据列，且行数相等所以需要复制，交叉值结果集的当前列的每一个值
                        //dtCrossValuesTotalCross.Rows[valueRowIdx][dtCrossValuesTotalCross.Columns.Count - 1 - (fieldIndexsValues.Length - 1 - j)] = (int.Parse(dr[computedIdx * fieldIndexsValues.Length + j].ToString())).ToString();
                        switch (dr.Table.Columns[computedIdx * fieldIndexsValues.Length + j].DataType.ToString())
                        {
                            case "System.Decimal":
                                {
                                    dtCrossValuesTotalCross.Rows[valueRowIdx]
                                        [dtCrossValuesTotalCross.Columns.Count - 1 - (fieldIndexsValues.Length - 1 - j)]
                                        = (Decimal.Parse(dr[computedIdx * fieldIndexsValues.Length + j].ToString()));
                                    break;
                                }
                            case "System.DateTime":
                                {
                                    dtCrossValuesTotalCross.Rows[valueRowIdx]
                                        [dtCrossValuesTotalCross.Columns.Count - 1 - (fieldIndexsValues.Length - 1 - j)]
                                        = dr[computedIdx * fieldIndexsValues.Length + j];
                                    break;
                                }
                            case "System.String":
                                {
                                    dtCrossValuesTotalCross.Rows[valueRowIdx]
                                        [dtCrossValuesTotalCross.Columns.Count - 1 - (fieldIndexsValues.Length - 1 - j)]
                                        = dr[computedIdx * fieldIndexsValues.Length + j];
                                    break;
                                }
                            default:
                                {
                                    dtCrossValuesTotalCross.Rows[valueRowIdx]
                                        [dtCrossValuesTotalCross.Columns.Count - 1 - (fieldIndexsValues.Length - 1 - j)]
                                        = (int.Parse(dr[computedIdx * fieldIndexsValues.Length + j].ToString()));
                                    break;
                                }
                        }
                    }
                }
                #endregion
                #region 遍历所有交叉列集
                for (int i = dtCrossValuesColumns.Columns.Count - 2; i >= 0; i--)
                {
                    //tempTitle = tempTitle + "|" + dtCrossValuesColumns.Rows[computedIdx][i] + "|";
                    //循环所有列，如果已到达最后列或值不同（判断顺序不能颠倒），插入一个合计列              
                    if ((computedIdx < dtCrossValuesColumns.Rows.Count - 1)
                        && (tempRowsValue[i].ToString()
                        == dtCrossValuesColumns.Rows[computedIdx + 1][i].ToString()))
                    {
                        //继续检查下一列
                        continue;
                    }
                    //如果列值不同
                    else
                    {

                        #region {9F609C45-B357-4807-A1E1-3741F08D471A}
                        bool isTotal = false;
                        if (columnsTotalLevel.Length < dtCrossValuesColumns.Columns.Count - 2 - i ||
                        (columnsTotalLevel.Length >= dtCrossValuesColumns.Columns.Count - 2 - i
                        && columnsTotalLevel[dtCrossValuesColumns.Columns.Count - 2 - i] != "0"))
                        {
                            //遍历所有值
                            for (int j = 0; j < fieldIndexsValues.Length; j++)
                            {
                                ///加一个合计列（列名为空）
                                dtCrossValuesTotalCross.Columns.Add(
                                    new DataColumn(
                                        "", dt.Columns[int.Parse(fieldIndexsValues[j].ToString())].DataType));
                            }
                            isTotal = true;
                        }
                        #endregion
                        int valueRowIdxSum = 0;
                        foreach (DataRow dr in dtCrossValues.Rows)
                        {
                            valueRowIdxSum = dtCrossValues.Rows.IndexOf(dr);
                            //遍历所有值
                            for (int j = 0; j < fieldIndexsValues.Length; j++)
                            {
                                #region {4B8178AF-8ABF-409d-B241-89EB7E311224}
                                #region {9F609C45-B357-4807-A1E1-3741F08D471A}
                                if (isTotal == true)
                                {
                                    ///根据当前列索引写入对应合计值
                                    dtCrossValuesTotalCross.Rows[valueRowIdxSum]
                                        [dtCrossValuesTotalCross.Columns.Count - 1 - (fieldIndexsValues.Length - 1 - j)]
                                        = tempTotalValues[i * fieldIndexsValues.Length
                                      + j, valueRowIdxSum];
                                }
                                #endregion
                                //写入一个清空一个
                                tempTotalValues[i * fieldIndexsValues.Length
                                      + j, valueRowIdxSum] = "0";
                                #endregion
                            }
                        }

                    }
                }

                #endregion
                //当前列下移一位
                computedIdx++;
            }
            #region 最后加一组总计
            #region {9F609C45-B357-4807-A1E1-3741F08D471A}
            if (columnsTotalLevel.Length < dataCrossColumns.Length ||
                 (this.columnsTotalLevel.Length >= this.dataCrossColumns.Length
                 && this.columnsTotalLevel[this.dataCrossColumns.Length - 1] != "0"))
            #endregion
            {

                //遍历所有值
                for (int j = 0; j < fieldIndexsValues.Length; j++)
                {
                    ///加一个合计列（列名为空）
                    dtCrossValuesTotalCross.Columns.Add(
                        new DataColumn("", dt.Columns[int.Parse(fieldIndexsValues[j].ToString())].DataType));
                }
                int valueRowIdxGrandTotal = 0;
                foreach (DataRow dr in dtCrossValues.Rows)
                {
                    valueRowIdxGrandTotal = dtCrossValues.Rows.IndexOf(dr);
                    //遍历所有值
                    for (int j = 0; j < fieldIndexsValues.Length; j++)
                    {
                        #region {4B8178AF-8ABF-409d-B241-89EB7E311224}
                        /////根据当前列索引写入对应合计值
                        //dtCrossValuesTotalCross.Rows[valueRowIdxGrandTotal]
                        //    [dtCrossValuesTotalCross.Columns.Count - 1 - (fieldIndexsValues.Length - 1 - j)]
                        //    = tempTotalValues[(fieldIndexsValues.Length) * (dtCrossValuesColumns.Columns.Count - 1)
                        //    + j * (fieldIndexsValues.Length - 1), valueRowIdxGrandTotal];
                        ////写入一个清空一个
                        //tempTotalValues[(fieldIndexsValues.Length) * (dtCrossValuesColumns.Columns.Count - 1)
                        //    + j * (fieldIndexsValues.Length - 1), valueRowIdxGrandTotal] = "0";
                        ///根据当前列索引写入对应合计值
                        dtCrossValuesTotalCross.Rows[valueRowIdxGrandTotal]
                            [dtCrossValuesTotalCross.Columns.Count - 1 - (fieldIndexsValues.Length - 1 - j)]
                            = tempTotalValues[(fieldIndexsValues.Length) * (dtCrossValuesColumns.Columns.Count - 1)
                            + j, valueRowIdxGrandTotal];
                        //写入一个清空一个
                        tempTotalValues[(fieldIndexsValues.Length) * (dtCrossValuesColumns.Columns.Count - 1)
                            + j, valueRowIdxGrandTotal] = "0";

                        #endregion
                    }
                }
            }

            #endregion
            #endregion
            return dtCrossValuesTotalCross;
            #endregion
        }
        /// <summary>
        /// 合计交叉数据表行
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="fieldIndexs"></param>
        /// <returns></returns>
        protected virtual DataTable TotalCrossDataTableRows(DataTable dtCrossValues, DataTable dtCrossRows, string[] fieldIndexs)
        {
            DataSetHelper dsh = new DataSetHelper();

            #region 交叉数据，用最直观的算法实现

            DataTable dtCrossValuesRows = dsh.SelectDistinctByIndexs("", dtCrossRows, fieldIndexs);
            object[] tempRowsValue;
            int computedIdx;

            #region 交叉行集
            tempRowsValue = new object[fieldIndexs.Length];
            computedIdx = 0;

            ///暂存合计值，第一维的长度是，行字段索引的长度
            ///第二维的长度是，值表的列数
            object[,] tempTotalValues;
            tempTotalValues = new object[fieldIndexs.Length, dtCrossValues.Columns.Count];
            #region 合计值初始化为0
            ///合计后的值表
            DataTable dtCrossValuesTotalCross = dtCrossValues.Clone();
            for (int i = 0; i < dtCrossValuesRows.Columns.Count; i++)
            {
                int valueColumnIdxDefault = 0;
                foreach (DataColumn dc in dtCrossValues.Columns)
                {
                    valueColumnIdxDefault = dtCrossValues.Columns.IndexOf(dc);
                    tempTotalValues[i, valueColumnIdxDefault] = "0";
                }
            }
            #endregion
            //循环所有行
            while (computedIdx < dtCrossValuesRows.Rows.Count)
            {
                //取当前行值
                tempRowsValue = dtCrossValuesRows.Rows[computedIdx].ItemArray;
                #region 暂存
                dtCrossValuesTotalCross.Rows.Add(dtCrossValues.Rows[computedIdx].ItemArray);

                for (int i = 0; i < dtCrossValuesRows.Columns.Count; i++)
                {
                    ///遍历所有列，
                    int valueColumnIdx;
                    valueColumnIdx = 0;
                    foreach (DataColumn dc in dtCrossValues.Columns)
                    {
                        valueColumnIdx = dtCrossValues.Columns.IndexOf(dc);
                        //tempTotalValues[i, valueColumnIdx] = (int.Parse(tempTotalValues[i, valueColumnIdx].ToString()) + int.Parse(dtCrossValues.Rows[computedIdx][valueColumnIdx].ToString())).ToString();
                        switch (dtCrossValues.Columns[valueColumnIdx].DataType.ToString())
                        {
                            case "System.Decimal":
                                {
                                    tempTotalValues[i, valueColumnIdx] = (Decimal.Parse(tempTotalValues[i, valueColumnIdx].ToString()) + Decimal.Parse(dtCrossValues.Rows[computedIdx][valueColumnIdx].ToString())).ToString();
                                    break;
                                }
                            case "System.DateTime":
                                {
                                    tempTotalValues[i, valueColumnIdx] = dtCrossValues.Rows[computedIdx][valueColumnIdx].ToString();
                                    break;
                                }
                            case "System.String":
                                {
                                    tempTotalValues[i, valueColumnIdx] = dtCrossValues.Rows[computedIdx][valueColumnIdx].ToString();
                                    break;
                                }
                            default:
                                {
                                    tempTotalValues[i, valueColumnIdx] = (int.Parse(tempTotalValues[i, valueColumnIdx].ToString()) + int.Parse(dtCrossValues.Rows[computedIdx][valueColumnIdx].ToString())).ToString();
                                    break;
                                }
                        }
                    }
                }

                #endregion
                //循环所有列，如果值不同插入一个计算列
                for (int i = dtCrossValuesRows.Columns.Count - 2; i >= 0; i--)
                {
                    //如果不是最后一行且列值相同
                    if ((computedIdx < dtCrossValuesRows.Rows.Count - 1) && (tempRowsValue[i].ToString() == dtCrossValuesRows.Rows[computedIdx + 1][i].ToString()))
                    {
                        //继续检查下一列
                        continue;
                    }
                    //如果列值不同
                    else
                    {
                        #region {9F609C45-B357-4807-A1E1-3741F08D471A}
                        bool isTotal = false;
                        if (rowsTotalLevel.Length < dtCrossValuesRows.Columns.Count - 2 - i ||
                            (rowsTotalLevel.Length >= dtCrossValuesRows.Columns.Count - 2 - i
                            && rowsTotalLevel[dtCrossValuesRows.Columns.Count - 2 - i] != "0"))
                        {
                            ///加一个合计列
                            dtCrossValuesTotalCross.Rows.Add(dtCrossValuesTotalCross.NewRow());
                            isTotal = true;
                        }
                        #endregion
                        int valueColumnIdxSum = 0;
                        foreach (DataColumn dc in dtCrossValues.Columns)
                        {
                            valueColumnIdxSum = dtCrossValues.Columns.IndexOf(dc);
                            #region {9F609C45-B357-4807-A1E1-3741F08D471A}
                            ///根据当前列索引写入对应合计值
                            if (isTotal == true)
                            {
                                dtCrossValuesTotalCross.Rows[dtCrossValuesTotalCross.Rows.Count - 1][valueColumnIdxSum] = tempTotalValues[i, valueColumnIdxSum];
                            }
                            #endregion
                            tempTotalValues[i, valueColumnIdxSum] = "0";
                        }
                    }
                }
                //当前列下移一位
                computedIdx++;
            }
            #region 加总计
            #region {9F609C45-B357-4807-A1E1-3741F08D471A}
            if (rowsTotalLevel.Length < dataCrossRows.Length ||
                (this.rowsTotalLevel.Length >= this.dataCrossRows.Length
                && this.rowsTotalLevel[this.dataCrossRows.Length - 1] != "0"))
            {
                ///加一个合计列
                dtCrossValuesTotalCross.Rows.Add(dtCrossValuesTotalCross.NewRow());
                int valueColumnIdxGrandTotal = 0;
                foreach (DataColumn dc in dtCrossValues.Columns)
                {
                    valueColumnIdxGrandTotal = dtCrossValues.Columns.IndexOf(dc);
                    ///根据当前列索引写入对应合计值
                    dtCrossValuesTotalCross.Rows[dtCrossValuesTotalCross.Rows.Count - 1][valueColumnIdxGrandTotal] = tempTotalValues[dtCrossValuesRows.Columns.Count - 1, valueColumnIdxGrandTotal];
                    tempTotalValues[dtCrossValuesRows.Columns.Count - 1, valueColumnIdxGrandTotal] = "0";
                }
            }
            #endregion
            #endregion
            #endregion

            return dtCrossValuesTotalCross;
            #endregion
        }
        /// <summary>
        /// 交叉数据表
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="fieldIndexs"></param>
        /// <returns></returns>
        protected virtual DataTable CrossDataTable(DataTable dt, string[] fieldIndexs, string[] fieldIndexsTotleLevel)
        {
            DataSetHelper dsh = new DataSetHelper();

            #region 交叉数据，用最直观的算法实现
            #region {0D9300B1-E85A-486d-96CD-D41A508A74F4} 当交叉行与交叉列是数字的时候强制转为字符
            DataTable dtStringRows = dsh.SelectDistinctByIndexs("", dt, fieldIndexs);

            DataTable dtRows = new DataTable();
            foreach (DataColumn dc in dtStringRows.Columns)
            {
                dtRows.Columns.Add(dc.Caption, typeof(System.String));
            }
            foreach (DataRow dr in dtStringRows.Rows)
            {
                dtRows.Rows.Add(dr.ItemArray);
            }
            #endregion
            // DataTable dtColumns = dsh.SelectDistinctByIndexs("", dt, dataCrossColumns);
            object[] tempRowsValue;
            int computedIdx;
            int insertRowIdx;
            #region 交叉行集
            tempRowsValue = new object[fieldIndexs.Length];
            computedIdx = 0;
            insertRowIdx = 0;
            //循环所有行
            while (computedIdx < dtRows.Rows.Count - 1)
            {
                //取当前行值
                tempRowsValue = dtRows.Rows[computedIdx].ItemArray;
                string tempTitle = string.Empty;
                //循环所有列，如果值不同插入一个计算列
                for (int i = dtRows.Columns.Count - 2; i >= 0; i--)
                {
                    //tempTitle = tempTitle + "|" + tempRowsValue[i].ToString();
                    //if (computedIdx < dtRows.Rows.Count-1)
                    //{
                    //如果列值相同
                    if (tempRowsValue[i].ToString() == dtRows.Rows[insertRowIdx + 1][i].ToString())
                    {
                        //继续检查下一列
                        continue;
                    }
                    //如果列值不同
                    else
                    {
                        #region {9F609C45-B357-4807-A1E1-3741F08D471A}
                        if (fieldIndexsTotleLevel.Length < dtRows.Columns.Count - 2 - i ||
                                         (fieldIndexsTotleLevel.Length >= dtRows.Columns.Count - 2 - i
                                         && fieldIndexsTotleLevel[dtRows.Columns.Count - 2 - i] != "0"))
                        {
                            //新建一个行
                            DataRow drNew = dtRows.NewRow();
                            for (int j = i; j >= 0; j--)
                            {
                                drNew[j] = tempRowsValue[j].ToString();
                            }
                            //将新建行的列值不同的列赋值为当前行的对应列值
                            drNew[i + 1] = tempTitle + "小计";
                            //计算行加入位置下移一行
                            insertRowIdx++;
                            ////判断当前插入行的位置是否是小于最大行索引
                            //if (insertRowIdx <= dtRows.Rows.Count - 1)
                            //{
                            //    //插入新行在插入列
                            dtRows.Rows.InsertAt(drNew, insertRowIdx);
                            //}
                            //else
                            //{
                            //    //追加新行在最后
                            //    dtRows.Rows.Add(drNew);
                            //} 
                        }
                        #endregion
                    }
                    //}
                    //else
                    //{

                    //}
                }
                //当前列下移一位
                computedIdx = insertRowIdx + 1;
                insertRowIdx = computedIdx;
            }
            //计算最后一行数据
            if (dtRows.Rows.Count > 0)
            {
                //取当前行值
                tempRowsValue = dtRows.Rows[dtRows.Rows.Count - 1].ItemArray;
                string tempTitle = string.Empty;
                //循环所有列，如果值不同插入一个计算列
                for (int i = dtRows.Columns.Count - 2; i >= 0; i--)
                {

                    #region {9F609C45-B357-4807-A1E1-3741F08D471A}
                    if (fieldIndexsTotleLevel.Length < dtRows.Columns.Count - 2 - i ||
                                      (fieldIndexsTotleLevel.Length >= dtRows.Columns.Count - 2 - i
                                      && fieldIndexsTotleLevel[dtRows.Columns.Count - 2 - i] != "0"))
                    {
                        //tempTitle = tempTitle + "|" + tempRowsValue[i].ToString();
                        //新建一个行
                        DataRow drNew = dtRows.NewRow();
                        for (int j = i; j >= 0; j--)
                        {
                            drNew[j] = tempRowsValue[j].ToString();
                        }
                        //将新建行的列值不同的列赋值为当前行的对应列值
                        drNew[i + 1] = "小计";

                        //追加新行在最后
                        dtRows.Rows.Add(drNew);
                    }

                    #endregion
                }
                #region {9F609C45-B357-4807-A1E1-3741F08D471A}
                if (fieldIndexsTotleLevel.Length < fieldIndexs.Length ||
                          (fieldIndexsTotleLevel.Length >= fieldIndexs.Length
                          && fieldIndexsTotleLevel[fieldIndexs.Length - 1] != "0"))
                {
                    //新建一个行
                    DataRow drNewGrandTotal = dtRows.NewRow();
                    drNewGrandTotal[0] = "总计";
                    dtRows.Rows.Add(drNewGrandTotal);
                }
                #endregion
            }
            #endregion

            return dtRows;
            #endregion
        }
        
        protected virtual int CrossDataTable(DataTable dt, string[] fieldIndexs, ref DataTable dtRows, ref int[] totalAndGrand)
        {
            DataSetHelper dsh = new DataSetHelper();

            #region 交叉数据，用最直观的算法实现
            string totalAndGrandString = string.Empty;
            //DataTable dtRows = dsh.SelectDistinctByIndexs("", dt, fieldIndexs);
            dtRows = dsh.SelectDistinctByIndexs("", dt, fieldIndexs);
            // DataTable dtColumns = dsh.SelectDistinctByIndexs("", dt, dataCrossColumns);
            object[] tempRowsValue;
            int computedIdx;
            int insertRowIdx;
            #region 交叉行集
            tempRowsValue = new object[fieldIndexs.Length];
            computedIdx = 0;
            insertRowIdx = 0;
            //循环所有行
            while (computedIdx < dtRows.Rows.Count - 1)
            {
                //取当前行值
                tempRowsValue = dtRows.Rows[computedIdx].ItemArray;
                string tempTitle = string.Empty;
                //循环所有列，如果值不同插入一个计算列
                for (int i = dtRows.Columns.Count - 2; i >= 0; i--)
                {
                    //tempTitle = tempTitle + "|" + tempRowsValue[i].ToString();
                    //if (computedIdx < dtRows.Rows.Count-1)
                    //{
                    //如果列值相同
                    if (tempRowsValue[i].ToString() == dtRows.Rows[insertRowIdx + 1][i].ToString())
                    {
                        //继续检查下一列
                        continue;
                    }
                    //如果列值不同
                    else
                    {
                        //新建一个行
                        DataRow drNew = dtRows.NewRow();
                        for (int j = i; j >= 0; j--)
                        {
                            drNew[j] = tempRowsValue[j].ToString();
                        }
                        //将新建行的列值不同的列赋值为当前行的对应列值
                        drNew[i + 1] = tempTitle + "小计";
                        //计算行加入位置下移一行
                        insertRowIdx++;
                        ////判断当前插入行的位置是否是小于最大行索引
                        //if (insertRowIdx <= dtRows.Rows.Count - 1)
                        //{
                        //    //插入新行在插入列
                        dtRows.Rows.InsertAt(drNew, insertRowIdx);
                        totalAndGrandString = totalAndGrandString + insertRowIdx.ToString() + ",";
                        //}
                        //else
                        //{
                        //    //追加新行在最后
                        //    dtRows.Rows.Add(drNew);
                        //}
                    }
                    //}
                    //else
                    //{

                    //}
                }
                //当前列下移一位
                computedIdx = insertRowIdx + 1;
                insertRowIdx = computedIdx;
            }
            //计算最后一行数据
            if (dtRows.Rows.Count > 0)
            {
                //取当前行值
                tempRowsValue = dtRows.Rows[dtRows.Rows.Count - 1].ItemArray;
                string tempTitle = string.Empty;
                //循环所有列，如果值不同插入一个计算列
                for (int i = dtRows.Columns.Count - 2; i >= 0; i--)
                {

                    //tempTitle = tempTitle + "|" + tempRowsValue[i].ToString();
                    //新建一个行
                    DataRow drNew = dtRows.NewRow();
                    for (int j = i; j >= 0; j--)
                    {
                        drNew[j] = tempRowsValue[j].ToString();
                    }
                    //将新建行的列值不同的列赋值为当前行的对应列值
                    drNew[i + 1] = "小计";

                    //追加新行在最后
                    dtRows.Rows.Add(drNew);
                    totalAndGrandString = totalAndGrandString + (dt.Rows.Count - 1).ToString() + ",";
                }
                //新建一个行
                DataRow drNewGrandTotal = dtRows.NewRow();
                totalAndGrandString = totalAndGrandString + (dt.Rows.Count - 1).ToString();
                drNewGrandTotal[0] = "总计";
                dtRows.Rows.Add(drNewGrandTotal);
            }
            #endregion
            string[] totalAndGrandStringArr = totalAndGrandString.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            totalAndGrand = new int[totalAndGrandStringArr.Length];
            for (int i = 0; i < totalAndGrandStringArr.Length; i++)
            {
                totalAndGrand[i] = int.Parse(totalAndGrandStringArr[i]);
            }
            return 0;
            #endregion
        }
        
        protected virtual void OnSort()
        {
        }

        private void ComputeIndex()
        {
            rowsHeaderBeginColumnIndex = dataBeginColumnIndex;
            if (this.dataCrossValues.Length > 1)
            {
                rowsHeaderBeginRowIndex = dataBeginRowIndex + dataCrossColumns.Length + 1;
            }
            else
            {
                rowsHeaderBeginRowIndex = dataBeginRowIndex + dataCrossColumns.Length;
            }
            columnsHeaderBeginColumnIndex = dataBeginColumnIndex + dataCrossRows.Length; ;
            columnsHeaderBeginRowIndex = dataBeginRowIndex;
        }

        protected virtual int GetConditions()
        {
            QueryParams.Clear();
            QueryParams.Add(new FS.FrameWork.Models.NeuObject("", deptCode, ""));
            QueryParams.Add(new FS.FrameWork.Models.NeuObject("", this.dtpBeginTime.Value.ToString(), ""));
            QueryParams.Add(new FS.FrameWork.Models.NeuObject("", this.dtpEndTime.Value.ToString(), ""));
            return 1;
        }

        protected override void OnLoad(EventArgs e)
        {
            if (this.DesignMode)
            {
                return;
            }

            this.employee = (FS.HISFC.Models.Base.Employee)this.dataBaseManager.Operator;
            this.Init();
            this.PrintDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(PrintDocumentPrintPage);
            base.OnLoad(e);
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

        #region 科室设置
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
        #endregion

        #region 初始化
        /// <summary>
        /// 根据二级权限初始化查寻科室
        /// </summary>
        /// <returns>-1失败 0成功</returns>
        private int initDeptByPriPower()
        {
            this.isHavePriPower = true;

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
            
            this.deptCode = this.cmbDept.Tag.ToString();

            return 0;
        }
        #endregion

        #region  设置标题
        public void SetTitle()
        {
            if (this.LeftAdditionTitle.IndexOf("统计时间") != -1)
            {
                this.lbAdditionTitleLeft.Text = " 统计时间:" + this.dtpBeginTime.Value.ToString()
                + " 到 " + this.dtpEndTime.Value.ToString();
            }
            else if (LeftAdditionTitle.IndexOf("统计日期") != -1)
            {
                this.lbAdditionTitleLeft.Text = " 统计日期:" + this.dtpBeginTime.Value.ToString("yyyy年MM月dd日")
                + " 到 " + this.dtpEndTime.Value.AddSeconds(1).ToString("yyyy年MM月dd日");
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
                this.lbAdditionTitleLeft.Text = " 年度:" + this.dtpEndTime.Value.AddSeconds(1).ToString("yyyy年");
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
                if (this.RightAdditionTitle.IndexOf("科室") != -1)
                {
                    this.lbAdditionTitleRight.Text = "科室:" + this.cmbDept.Text;
                }
                if (this.RightAdditionTitle.IndexOf("部门") != -1)
                {
                    this.lbAdditionTitleRight.Text = "部门:" + this.cmbDept.Text;
                }
            }
            else
            {
                this.lbAdditionTitleMid.Text = "";
            }
        }
        #endregion

        #region 本报表专用打印处理

        #region 变量
        /// <summary>
        /// 当前打印页的页码
        /// 程序自动计算的
        /// </summary>
        private int curPageNO = 1;

        /// <summary>
        /// 本次打印最大页码
        /// 程序自动计算的
        /// </summary>
        private int maxPageNO = 1;

        /// <summary>
        /// 打印时间，多页时保证每一页的时间相同
        /// </summary>
        DateTime printTime = new DateTime();

        /// <summary>
        /// 打印用
        /// </summary>
        private System.Drawing.Printing.PrintDocument PrintDocument = new System.Drawing.Printing.PrintDocument();

        SOC.Windows.Forms.PrintPageSelectDialog socPrintPageSelectDialog = new FS.SOC.Windows.Forms.PrintPageSelectDialog();

        #endregion

        #region 属性及其变量
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
        private bool isAutoPaper = false;

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
        private int paperWith = 850;

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

        /// <summary>
        /// 横向打印
        /// </summary>
        private bool isLandscape = false;

        /// <summary>
        /// 横向打印
        /// </summary>
        [Description("横向打印，纸张竖放"), Category("Print打印设置"), Browsable(true)]
        public bool IsLandscape
        {
            get { return isLandscape; }
            set { isLandscape = value; }
        }

        /// <summary>
        /// 页边距
        /// </summary>
        private System.Drawing.Printing.Margins drawingMargins = new System.Drawing.Printing.Margins(20, 20, 10, 30);

        /// <summary>
        /// 页边距
        /// 每页可绘制打印内容的范围等于给定的pageWith乘以pageHeight减去页边距
        /// </summary>
        [Description("页边距"), Category("Print打印设置"), Browsable(true)]
        public System.Drawing.Printing.Margins DrawingMargins
        {
            get { return drawingMargins; }
            //set { drawingMargins = value; }
        }

        /// <summary>
        /// 页上边距
        /// </summary>
        [Description("页上边距"), Category("Print打印设置"), Browsable(true)]
        public int DrawingMarginTop
        {
            get { return drawingMargins.Top; }
            set { drawingMargins.Top = value; }
        }

        /// <summary>
        /// 页下边距
        /// </summary>
        [Description("页下边距"), Category("Print打印设置"), Browsable(true)]
        public int DrawingMarginBottom
        {
            get { return drawingMargins.Bottom; }
            set { drawingMargins.Bottom = value; }
        }

        /// <summary>
        /// 页左边距
        /// </summary>
        [Description("页左边距"), Category("Print打印设置"), Browsable(true)]
        public int DrawingMarginLeft
        {
            get { return drawingMargins.Left; }
            set { drawingMargins.Left = value; }
        }

        /// <summary>
        /// 页右边距
        /// </summary>
        [Description("页右边距"), Category("Print打印设置"), Browsable(true)]
        public int DrawingMarginRight
        {
            get { return drawingMargins.Right; }
            set { drawingMargins.Right = value; }
        }

        private bool isPrintPageBottom = true;

        /// <summary>
        /// 是否打印页脚打印时间及页码
        /// </summary>
        [Description("是否打印页脚打印时间及页码"), Category("Print打印设置"), Browsable(true)]
        public bool IsPrintPageBottom
        {
            get { return isPrintPageBottom; }
            set { isPrintPageBottom = value; }
        }

        /// <summary>
        /// 页脚打印的时间及页码字体大小
        /// </summary>
        private int bottomInfoFontSize = 8;

        /// <summary>
        /// 页脚打印的时间及页码字体大小
        /// </summary>
        [Description("页脚打印的时间及页码字体大小"), Category("Print打印设置"), Browsable(true)]
        public int BottomInfoFontSize
        {
            get { return bottomInfoFontSize; }
            set { bottomInfoFontSize = value; }
        }

        /// <summary>
        /// 页脚打印的时间及页码边距
        /// </summary>
        private int bottomInfoMargin = 30;

        /// <summary>
        /// 页脚打印的时间及页码边距
        /// </summary>
        [Description("页脚打印的时间及页码边距"), Category("Print打印设置"), Browsable(true)]
        public int BottomInfoMargin
        {
            get { return bottomInfoMargin; }
            set { bottomInfoMargin = value; }
        }

        /// <summary>
        /// 是否显示边框
        /// </summary>
        private bool isShowBorder = false;

        /// <summary>
        /// 是否显示边框
        /// </summary>
        [Description("是否显示边框"), Category("Print打印设置"), Browsable(true)]
        public bool IsShowBorder
        {
            get { return isShowBorder; }
            set { isShowBorder = value; }
        }

        /// <summary>
        /// 打印列索引
        /// </summary>
        private string printColIndexs = "";

        /// <summary>
        /// 打印列索引
        /// </summary>
        [Description("打印列索引"), Category("Print打印设置"), Browsable(true)]
        public string PrintColIndexs
        {
            get
            {
                return printColIndexs;
            }
            set
            {
                this.printColIndexs = value;

            }
        }

        private bool isShowRowHeader = false;

        [Description("是否显示行头"), Category("Print打印设置"), Browsable(true)]
        public bool IsShowRowHeader
        {
            get { return isShowRowHeader; }
            set { isShowRowHeader = value; }
        }

        private bool isShowColHeader = false;

        [Description("是否显示列头"), Category("Print打印设置"), Browsable(true)]
        public bool IsShowColHeader
        {
            get { return isShowColHeader; }
            set { isShowColHeader = value; }
        }


        #endregion

        #region 函数

        /// <summary>
        /// 打印页码选择
        /// </summary>
        private bool ChoosePrintPageNO(Graphics graphics)
        {
            //处理横向打印
            int paperWith = this.PaperWith;
            int paperHeight = this.PaperHeight;

            if (this.IsLandscape)
            {
                paperWith = this.PaperHeight;
                paperHeight = this.PaperWith;

            }

            int drawingWidth = paperWith - this.DrawingMargins.Left - this.DrawingMargins.Right;
            int drawingHeight = paperHeight - this.DrawingMargins.Top - this.DrawingMargins.Bottom - (this.panelTitle.Height + (this.IsNeedAdditionTitle ? this.panelAdditionTitle.Height : 0));

            FarPoint.Win.Spread.PrintInfo printInfo = new FarPoint.Win.Spread.PrintInfo();
            printInfo.ShowBorder = this.IsShowBorder;
            printInfo.ShowColumnHeaders = this.IsShowColHeader;
            printInfo.ShowRowHeaders = this.IsShowRowHeader;
            printInfo.PrintType = FarPoint.Win.Spread.PrintType.All;
            printInfo.ShowRowHeaders = this.fpSpread1.ActiveSheet.RowHeader.Visible;
            if (this.IsLandscape)
            {
                printInfo.Orientation = FarPoint.Win.Spread.PrintOrientation.Landscape;
            }
            this.fpSpread1.ActiveSheet.PrintInfo = printInfo;
            this.maxPageNO = fpSpread1.GetOwnerPrintPageCount(graphics, new Rectangle(this.DrawingMargins.Left, this.DrawingMargins.Top + this.panelTitle.Height + (this.IsNeedAdditionTitle ? this.panelAdditionTitle.Height : 0), drawingWidth, drawingHeight), fpSpread1.ActiveSheetIndex);

            socPrintPageSelectDialog.MaxPageNO = this.maxPageNO;
            if (this.maxPageNO > 1)
            {
                socPrintPageSelectDialog.StartPosition = FormStartPosition.CenterScreen;
                socPrintPageSelectDialog.ShowDialog();
                if (socPrintPageSelectDialog.ToPageNO == 0)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 打印绘制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrintDocumentPrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //if (this.curPageNO == 1)
            //{
            //    if (!this.ChoosePrintPageNO(e.Graphics))
            //    {
            //        e.Cancel = true;
            //        return;
            //    }
            //}

            //跳过选择打印范围外的数据
            while (this.curPageNO < this.socPrintPageSelectDialog.FromPageNO && this.curPageNO < this.maxPageNO)
            {
                curPageNO++;
            }

            if (this.curPageNO > this.maxPageNO || this.curPageNO > socPrintPageSelectDialog.ToPageNO)
            {
                //this.curPageNO = 1;
                //this.maxPageNO = 1;
                e.HasMorePages = false;
                return;
            }

            Graphics graphics = e.Graphics;

            //处理横向打印
            int paperWith = this.PaperWith;
            int paperHeight = this.PaperHeight;

            if (this.IsLandscape)
            {
                paperWith = this.PaperHeight;
                paperHeight = this.PaperWith;

            }

            #region 标题绘制
            //纸张高度不够打印标题，则返回
            if (paperHeight < this.panelTitle.Height + (this.IsNeedAdditionTitle ? this.panelAdditionTitle.Height : 0))
            {
                this.ShowBalloonTip(10, "温馨提示", "纸张高度不够打印标题");
                return;
            }

            //1pt=1/72in=this.PPI/72px
            int mainTitleLocalX = this.DrawingMargins.Left;
            int mainTitleLoaclY = this.DrawingMargins.Top + (this.panelTitle.Height - this.lbMainTitle.Height) / 2;

            if (paperWith - this.DrawingMargins.Left - this.DrawingMargins.Right - this.lbMainTitle.Width >= 0)
            {
                mainTitleLocalX = this.DrawingMargins.Left + (paperWith - this.DrawingMargins.Left - this.DrawingMargins.Right - this.lbMainTitle.Width) / 2;
            }
            graphics.DrawString(this.lbMainTitle.Text, this.lbMainTitle.Font, new SolidBrush(this.lbMainTitle.ForeColor), mainTitleLocalX, mainTitleLoaclY);

            if (this.IsNeedAdditionTitle)
            {
                int additionTitleLocalX = this.DrawingMargins.Left;
                int additionTitleLocalY = this.DrawingMargins.Top + this.panelTitle.Height + (this.panelAdditionTitle.Height - this.lbAdditionTitleLeft.Height) / 2;
                string additionTitle = this.lbAdditionTitleLeft.Text + "    " + this.lbAdditionTitleMid.Text + "    " + this.lbAdditionTitleRight.Text;
                graphics.DrawString(additionTitle, this.lbAdditionTitleLeft.Font, new SolidBrush(this.lbAdditionTitleLeft.ForeColor), additionTitleLocalX, additionTitleLocalY);

            }
            #endregion

            #region Farpoint绘制
            int drawingWidth = paperWith - this.DrawingMargins.Left - this.DrawingMargins.Right;
            int drawingHeight = paperHeight - this.DrawingMargins.Top - this.DrawingMargins.Bottom - (this.panelTitle.Height + (this.IsNeedAdditionTitle ? this.panelAdditionTitle.Height : 0));
            fpSpread1.OwnerPrintDraw(graphics, new Rectangle(this.DrawingMargins.Left, this.DrawingMargins.Top + this.panelTitle.Height + (this.IsNeedAdditionTitle ? this.panelAdditionTitle.Height : 0), drawingWidth, drawingHeight), fpSpread1.ActiveSheetIndex, this.curPageNO);

            #endregion

            #region 底部打印时间及页码绘制

            if (this.isPrintPageBottom && this.DrawingMargins.Bottom > bottomInfoFontSize && this.DrawingMargins.Bottom >= this.BottomInfoMargin)
            {
                Font pageBottomFont = new Font("宋体", (float)bottomInfoFontSize, FontStyle.Regular);
                SolidBrush pageBottomSolidBrush = new SolidBrush(Color.Black);
                string pageBottomText = "打印时间：" + printTime.ToString() + "  第" + this.curPageNO.ToString() + "页，共" + this.maxPageNO.ToString() + "页";
                int pageBottomTextLenght = (int)(SOC.Public.String.Length(pageBottomText) / 2 * bottomInfoFontSize / 0.75);
                int pageBottomLocationX = this.DrawingMargins.Left + (int)((paperWith - this.DrawingMargins.Left - this.DrawingMargins.Right - pageBottomTextLenght) / 2);
                int pageBottomLocationY = paperHeight - this.BottomInfoMargin;

                graphics.DrawString(pageBottomText, pageBottomFont, pageBottomSolidBrush, pageBottomLocationX, pageBottomLocationY);
            }
            #endregion

            #region 分页
            if (this.curPageNO < this.socPrintPageSelectDialog.ToPageNO && this.curPageNO < maxPageNO)
            {
                e.HasMorePages = true;
                curPageNO++;
            }
            else
            {
                curPageNO = 1;
                //maxPageNO = 1;
                e.HasMorePages = false;
            }
            #endregion
        }

        /// <summary>
        /// 预览
        /// </summary>
        private void myPrintView()
        {
            PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();
            printPreviewDialog.Document = this.PrintDocument;
            try
            {
                ((Form)printPreviewDialog).WindowState = FormWindowState.Maximized;
            }
            catch { }
            try
            {
                printPreviewDialog.ShowDialog();
                printPreviewDialog.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("打印机报错！" + ex.Message);
            }
        }

        /// <summary>
        /// 打印纸张设置
        /// </summary>
        /// <param name="paperSize"></param>
        private void SetPaperSize(System.Drawing.Printing.PaperSize paperSize)
        {
            if (paperSize == null)
            {
                paperSize = new System.Drawing.Printing.PaperSize("Letter", 850, 1100);
            }
            if (!string.IsNullOrEmpty(this.PrinterName))
            {
                this.PrintDocument.DefaultPageSettings.PrinterSettings.PrinterName = this.PrinterName;
            }
            this.PrintDocument.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize(paperSize.PaperName, paperSize.Width, paperSize.Height);
            this.PrintDocument.PrinterSettings.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize(paperSize.PaperName, paperSize.Width, paperSize.Height);
            //this.PrintDocument.PrinterSettings.DefaultPageSettings.Landscape = this.IsLandscape;
            this.PrintDocument.DefaultPageSettings.Landscape = this.IsLandscape;
        }

        /// <summary>
        /// 打印数据
        /// </summary>
        /// <returns></returns>
        private int printData()
        {
            return this.printData(this.PaperName, this.PaperWith, this.PaperHeight, this.PrinterName);
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
            #region 设置的打印列处理

            if (this.fpSpread1.ActiveSheetIndex == 0)
            {
                if (!string.IsNullOrEmpty(this.PrintColIndexs))
                {
                    for (int colIndex = 0; colIndex < this.fpSpread1.ActiveSheet.ColumnCount; colIndex++)
                    {
                        this.fpSpread1.ActiveSheet.Columns[colIndex].Visible = false;
                    }
                    string[] cols = this.PrintColIndexs.Split(' ', ',', '|');
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
                            if (colIndex == -1 && this.RowHeaderVisible)
                            {
                                this.fpSpread1_Sheet1.RowHeader.Visible = false;
                            }
                            this.fpSpread1_Sheet1.Columns[colIndex].Visible = true;
                        }
                        catch (Exception ex)
                        {
                            this.ShowBalloonTip(5000, "温馨提示", ex.Message + "\n打印列设置发生错误" + colLetter);
                            continue;
                        }
                    }
                }
            }
            #endregion

            printTime = DateTime.Now;
            if (paperName == string.Empty)
            {
                this.ShowBalloonTip(5000, "温馨提示", "没有设置纸张\n已经忽略");
                this.IsAutoPaper = true;
            }
            if (this.isAutoPaper)
            {
                this.SetPaperSize(this.getPaperSize());
            }
            else
            {
                System.Drawing.Printing.PaperSize paperSize = new System.Drawing.Printing.PaperSize(paperName, paperWith, paperHeight);
                this.SetPaperSize(paperSize);
            }
            if (this.IsNeedPreView)
            {
                if (this.ChoosePrintPageNO(Graphics.FromHwnd(this.Handle)))
                {
                    this.myPrintView();
                }
            }
            else
            {
                if (this.ChoosePrintPageNO(Graphics.FromHwnd(this.Handle)))
                {
                    this.PrintDocument.Print();
                }
            }

            #region 设置打印列后恢复
            for (int colIndex = 0; colIndex < this.fpSpread1.ActiveSheet.ColumnCount; colIndex++)
            {
                this.fpSpread1.ActiveSheet.Columns[colIndex].Visible = true;
            }
            this.fpSpread1_Sheet1.RowHeader.Visible = this.RowHeaderVisible;
            
            //this.ReadSetting(this.fpSpread1.ActiveSheetIndex);
            #endregion

            this.curPageNO = 1;
            this.maxPageNO = 1;

            return 0;
        }

        /// <summary>
        /// 出库单的纸张高度设置
        /// 默认情况下是三行出库数据的高度
        /// </summary>
        /// <summary>
        /// 出库单的纸张高度设置
        /// 默认情况下是三行出库数据的高度
        /// </summary>
        private System.Drawing.Printing.PaperSize getPaperSize()
        {
            System.Drawing.Printing.PaperSize paperSize = new System.Drawing.Printing.PaperSize();
            paperSize.PaperName = this.priPowerMgr.GetDateTimeFromSysDateTime().ToString();
            try
            {
                int width = 800;

                int curHeight = this.Height;

                int addHeight = (this.fpSpread1.ActiveSheet.RowCount - 1) *
                    (int)this.fpSpread1.ActiveSheet.Rows[0].Height;

                int additionAddHeight = this.PaperAddHeight;

                paperSize.Width = width;
                paperSize.Height = (addHeight + curHeight + additionAddHeight);

            }
            catch (Exception ex)
            {
                MessageBox.Show("设置纸张出错>>" + ex.Message);
            }
            return paperSize;
        }

        #endregion

        #endregion

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

        private void cmbDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.deptCode = this.cmbDept.Tag.ToString();
        }

        public int Query()
        {
            #region  查询
            this.SvMain.RowCount = 500;
            this.SvMain.ColumnCount = 500;
            #region 清空数据表格
            if (SvMain.RowCount >= this.dataBeginRowIndex + 1)
            {
                SvMain.ClearRange(this.dataBeginRowIndex, dataBeginColumnIndex, SvMain.Rows.Count, SvMain.Columns.Count, false);
            }
            #endregion

            #region 参数替换
            FarPoint.Win.Spread.Cell c = null;
            for (int j = 0; j < this.useParamCellsCount; j++)
            {
                string CellText = string.Empty;
                c = SvMain.GetCellFromTag(c, "{QueryParams}");
                if (c != null)
                {
                    CellText = c.Note;
                    for (int i = 0; i < this.QueryParams.Count; i++)
                    {
                        CellText = CellText.Replace("{" + i + "}", this.QueryParams[i].ToString());
                    }
                    c.Text = CellText;
                }
            }
            #endregion

            #region 显示到表格

            DataTable dt = new DataTable();
            dataRowCount = 0;
            switch (this.QuerySqlTypeValue)
            {
                case QuerySqlType.id:
                    if (Db.QueryDataBySqlId(this.QuerySql, ref dt, this.QueryParams) != 1)
                    {
                        Cursor = Cursors.Arrow;
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        return -1;
                    }
                    break;
                case QuerySqlType.text:
                    if (Db.QueryDataBySql(this.QuerySql, ref dt, this.QueryParams) != 1)
                    {
                        Cursor = Cursors.Arrow;
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        return -1;
                    }
                    break;
                default:
                    break;
            }
            ///数据集dt
            //******************
            //┌	─	─	─	─	─	─	─	─	─	─	─	┐
            //│	A	│	C	│	G	│	I	│	1	│	0	│
            //│	A	│	C	│	G	│	J	│	1	│	0	│
            //│	A	│	C	│	H	│	K	│	1	│	0	│
            //│	A	│	C	│	H	│	L	│	1	│	0	│
            //│	A	│	D	│	G	│	I	│	1	│	0	│
            //│	A	│	D	│	G	│	J	│	1	│	0	│
            //│	A	│	D	│	H	│	K	│	1	│	0	│
            //│	A	│	D	│	H	│	L	│	1	│	0	│
            //│	B	│	E	│	G	│	I	│	1	│	0	│
            //│	B	│	E	│	G	│	J	│	1	│	0	│
            //│	B	│	E	│	H	│	K	│	1	│	0	│
            //│	B	│	E	│	H	│	L	│	1	│	0	│
            //│	B	│	F	│	G	│	I	│	1	│	0	│
            //│	B	│	F	│	G	│	J	│	1	│	0	│
            //│	B	│	F	│	H	│	K	│	1	│	0	│
            //│	B	│	F	│	H	│	L	│	1	│	0	│
            //└	─	─	─	─	─	─	─	─	─	─	─	┘
            //******************
            #region 分别交叉行与列
            DataTable dtCrossRows = null;
            DataTable dtCrossColumns = null;
            DataTable dtCrossValues = null;


            #region 先求值后交叉
            DataSetHelper dsh = new DataSetHelper();

            ///生成行数据集，不包括合计行
            dtCrossRows = dsh.SelectDistinctByIndexs("", dt, dataCrossRows);
            ///数据集dtCrossRows
            //******************
            //┌	─	┬	─	┐
            //│	G	│	I	│
            //│	G	│	J	│
            //│	H	│	K	│
            //│	H	│	L	│
            //└	─	┴	─	┘       
            //******************
            ///生成列数据集，不包括合计列
            dtCrossColumns = dsh.SelectDistinctByIndexs("", dt, dataCrossColumns);
            ///数据集dtCrossColumns
            //******************
            //┌	─	┬	─	┐
            //│	A	│	C	│
            //│	A	│	D	│
            //│	B	│	E	│
            //│	B	│	F	│
            //└	─	┴	─	┘          
            //******************
            //SvMain.DataSource = dtCrossColumns;
            //FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            //return 1;
            #region 遍历列数据集，向值数据集添加列,并求计算值
            ///值数据集

            dtCrossValues = new DataTable();
            foreach (DataRow dr in dtCrossColumns.Rows)
            {
                #region 向值数据集添加列
                //string columnsName = string.Empty;
                //foreach (object o in dr.ItemArray)
                //{
                //    columnsName = columnsName + o.ToString() + "|";
                //}
                foreach (string s in dataCrossValues)
                {
                    //值数据集，添加一列
                    dtCrossValues.Columns.Add(new DataColumn("", dt.Columns[int.Parse(s)].DataType));

                }
                #endregion
            }
            ///数据集dtCrossValues
            //********  
            //┌	─	┬	─	┬	─	┬	─	┬	─	┬	─	┬	─	┬	─	┐
            //│		│		│		│		│		│		│		│		│
            //└	─	┴	─	┴	─	┴	─	┴	─	┴	─	┴	─	┴	─	┘
            //********
            #region 遍历行数据集每一行
            ///当前行数据集，行索引
            int currRowIdx = 0;
            //遍历行数据集
            foreach (DataRow drRow in dtCrossRows.Rows)
            {
                ///值数据集，添加一行
                dtCrossValues.Rows.Add(dtCrossValues.NewRow());
                //计算当前行数据集，行索引
                currRowIdx = dtCrossRows.Rows.IndexOf(drRow);
                #region 遍历列数据集每一行
                //当前列数据集，行索引
                int currColIdx = 0;
                foreach (DataRow drColumn in dtCrossColumns.Rows)
                {
                    //计算当前列数据集，行索引
                    currColIdx = dtCrossColumns.Rows.IndexOf(drColumn);

                    //当前值数据集，列数量
                    int valColCnt = 0;
                    //计算当前值数据集，列数量
                    valColCnt = this.dataCrossValues.Length;
                    #region 计算当前单元格的数据值
                    ///当前单元格的数据值表达式
                    string currExp = string.Empty;
                    #region 遍历行集，计算当前单元格值的行表达式部分
                    //当前行集，值索引
                    int expRowsIdx = 0;
                    //string currRowHeader = string.Empty;
                    foreach (string sDataCrossRows in this.dataCrossRows)
                    {
                        //计算当前行集，值索引（因为没有重复值所以可以这么取）
                        expRowsIdx = Array.IndexOf(this.dataCrossRows, sDataCrossRows);
                        //根据原始数据集对应值列的类型计算表达式，详见msdn“ms-help://MS.VSCC.v80/MS.MSDN.v80/MS.NETDEVFX.v20.chs/cpref4/html/P_System_Data_DataColumn_Expression.htm”
                        switch (dt.Columns[int.Parse(sDataCrossRows)].DataType.ToString())
                        {
                            case "System.Decimal":
                                {
                                    currExp = currExp + dt.Columns[int.Parse(sDataCrossRows)].Caption + " = " + dtCrossRows.Rows[currRowIdx][expRowsIdx].ToString() + " AND ";
                                    break;
                                }
                            case "System.DateTime":
                                {
                                    currExp = currExp + dt.Columns[int.Parse(sDataCrossRows)].Caption + " = #" + dtCrossRows.Rows[currRowIdx][expRowsIdx] + "# AND ";
                                    break;
                                }
                            case "System.String":
                                {
                                    currExp = currExp + dt.Columns[int.Parse(sDataCrossRows)].Caption + " = '" + dtCrossRows.Rows[currRowIdx][expRowsIdx] + "' AND ";
                                    break;
                                }
                            default:
                                {
                                    currExp = currExp + dt.Columns[int.Parse(sDataCrossRows)].Caption + " = '" + dtCrossRows.Rows[currRowIdx][expRowsIdx] + "' AND ";
                                    break;
                                }
                        }
                    }
                    #endregion
                    #region 遍历列集，计算当前单元格的,数据值的，列表达式部分
                    //当前列集，值索引
                    int expColumnsIdx = 0;
                    foreach (string sDataCrossColumns in this.dataCrossColumns)
                    {
                        //计算当前列集，值索引（因为没有重复值所以可以这么取）
                        expColumnsIdx = Array.IndexOf(this.dataCrossColumns, sDataCrossColumns);
                        //根据原始数据集对应值列的类型计算表达式，详见msdn“ms-help://MS.VSCC.v80/MS.MSDN.v80/MS.NETDEVFX.v20.chs/cpref4/html/P_System_Data_DataColumn_Expression.htm”
                        switch (dt.Columns[int.Parse(sDataCrossColumns)].DataType.ToString())
                        {
                            case "System.Decimal":
                                {
                                    currExp = currExp + dt.Columns[int.Parse(sDataCrossColumns)].Caption + " = " + dtCrossColumns.Rows[currColIdx][expColumnsIdx].ToString() + " AND ";
                                    break;
                                }
                            case "System.DateTime":
                                {
                                    currExp = currExp + dt.Columns[int.Parse(sDataCrossColumns)].Caption + " = #" + dtCrossColumns.Rows[currColIdx][expColumnsIdx] + "# AND ";
                                    break;
                                }
                            case "System.String":
                                {
                                    currExp = currExp + dt.Columns[int.Parse(sDataCrossColumns)].Caption + " = '" + dtCrossColumns.Rows[currColIdx][expColumnsIdx] + "' AND ";
                                    break;
                                }
                            default:
                                {
                                    currExp = currExp + dt.Columns[int.Parse(sDataCrossColumns)].Caption + " = '" + dtCrossColumns.Rows[currColIdx][expColumnsIdx] + "' AND ";
                                    break;
                                }
                        }
                    }
                    #endregion
                    #region 去尾部的“AND”
                    currExp = currExp.Remove(currExp.Length - 4);
                    #endregion
                    #region 遍历值数据集每一行
                    //当前单元格的数据值
                    string[] currVal = new string[this.dataCrossValues.Length];
                    //当前值集，值索引
                    int currValIdx = 0;
                    DataRow[] arryDr;
                    arryDr = dt.Select(currExp);
                    foreach (string s in this.dataCrossValues)
                    {
                        //计算当前值集，值索引（因为没有重复值所以可以这么取）
                        currValIdx = Array.IndexOf(this.dataCrossValues, s);
                        currVal[currValIdx] = "0";
                        #region 遍历取查询出来的行，计算对应位置的值
                        if (arryDr.Length > 0)
                        {
                            //有可能有多行，遍历取值
                            foreach (DataRow drCurrExp in arryDr)
                            {
                                //TODO:需要添加各种计算方式,现在仅支持sum运算
                                // currVal[currValIdx] = (int.Parse(currVal[currValIdx]) + int.Parse(drCurrExp[int.Parse(s)].ToString())).ToString();
                                switch (drCurrExp.Table.Columns[int.Parse(s)].DataType.ToString())
                                {
                                    case "System.Decimal":
                                        {
                                            currVal[currValIdx] = (decimal.Parse(currVal[currValIdx]) + decimal.Parse(drCurrExp[int.Parse(s)].ToString())).ToString();
                                            break;
                                        }
                                    case "System.DateTime":
                                        {
                                            currVal[currValIdx] = DateTime.Parse(drCurrExp[int.Parse(s)].ToString()).ToString();
                                            break;
                                        }
                                    case "System.String":
                                        {
                                            currVal[currValIdx] = drCurrExp[int.Parse(s)].ToString();
                                            break;
                                        }
                                    default:
                                        {
                                            currVal[currValIdx] = (int.Parse(currVal[currValIdx]) + int.Parse(drCurrExp[int.Parse(s)].ToString())).ToString();
                                            break;
                                        }
                                }
                            }
                        }
                        #endregion
                        dtCrossValues.Rows[currRowIdx][currColIdx * valColCnt + currValIdx] = currVal[currValIdx];
                    }
                    #endregion
                    #endregion
                }
                #endregion
            }
            #endregion
            #endregion
            ///数据集dtCrossValues
            //********  
            //┌	─	┬	─	┬	─	┬	─	┬	─	┬	─	┬	─	┬	─	┐
            //│	1	│	0	│	1	│	0	│	1	│	0	│	1	│	0	│
            //│	1	│	0	│	1	│	0	│	1	│	0	│	1	│	0	│
            //│	1	│	0	│	1	│	0	│	1	│	0	│	1	│	0	│
            //│	1	│	0	│	1	│	0	│	1	│	0	│	1	│	0	│
            //└	─	┴	─	┴	─	┴	─	┴	─	┴	─	┴	─	┴	─	┘          
            //********  
            #region 添加列合计值
            DataTable dtCrossViewColumns = this.TotalCrossDataTableColumns(dtCrossValues, dt, this.dataCrossColumns, this.dataCrossValues);


            //SvMain.DataSource = dtCrossViewColumns;
            //FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            //return 1;
            #endregion
            #region 添加行合计值
            DataTable dtCrossViewRows = this.TotalCrossDataTableRows(dtCrossViewColumns, dt, this.dataCrossRows);
            //SvMain.DataSource = dtCrossViewRows;
            //FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            //return 1;
            #endregion
            #region 计算行列标题，显示行列标题
            #region 添加列标题，列合计标题
            DataTable dtCrossViewColumnsTitle = this.CrossDataTable(dt, this.dataCrossColumns, this.columnsTotalLevel);

            //SvMain.DataSource = dtCrossViewColumnsTitle;
            //FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            //return 1;
            #endregion
            #region 添加行标题，行合计标题
            DataTable dtCrossViewRowsTitle = this.CrossDataTable(dt, this.dataCrossRows, this.rowsTotalLevel);
            #endregion

            #region 合并
            #region 显示合并列
            DataTable dtCrossViewColumnsTitleIncludValues = dtCrossViewColumnsTitle.Clone();
            //如果值列大于一列,就需要加一个值列名显示用的行
            if (this.dataCrossValues.Length > 1)
            {
                int dataCrossValuesIdx = 0;
                dtCrossViewColumnsTitleIncludValues.Columns.Add(
                    new DataColumn("", typeof(string)));
                dataCrossValuesIdx = this.dataCrossColumns.Length;
                int dtCrossViewColumnsTitleRowIdx = 0;
                while (dtCrossViewColumnsTitleRowIdx < dtCrossViewColumnsTitle.Rows.Count)
                {
                    int dataCrossValuesItemIdx = 0;
                    foreach (string s in this.dataCrossValues)
                    {
                        dtCrossViewColumnsTitleIncludValues.Rows.Add(dtCrossViewColumnsTitle.Rows[dtCrossViewColumnsTitleRowIdx].ItemArray);
                        dataCrossValuesItemIdx = Array.IndexOf(dataCrossValues, s);
                        dtCrossViewColumnsTitleIncludValues.Rows
                            [dtCrossViewColumnsTitleRowIdx * dataCrossValues.Length + dataCrossValuesItemIdx]
                            [dataCrossValuesIdx]
                            = dt.Columns[int.Parse(s)].Caption;
                    }
                    dtCrossViewColumnsTitleRowIdx++;
                }
                //SvMain.DataSource = dtCrossViewColumnsTitleIncludValues;
                //FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                //return 1;
                //Funcation.DisplayToFpReverse(SvMain, dtCrossViewColumnsTitleIncludValues, columnsHeaderBeginRowIndex, columnsHeaderBeginColumnIndex);
                Function.DisplayToFpReverse(SvMain, dtCrossViewColumnsTitleIncludValues, columnsHeaderBeginRowIndex, columnsHeaderBeginColumnIndex);
                SpanDisplayedFpReverse(SvMain, dtCrossViewColumnsTitleIncludValues, columnsHeaderBeginRowIndex, columnsHeaderBeginColumnIndex);
                SpanDisplayedFpReverseRows(SvMain, dtCrossViewColumnsTitleIncludValues, columnsHeaderBeginRowIndex, columnsHeaderBeginColumnIndex);
            }
            else
            {
                int dataCrossValuesIdx = 0;
                dataCrossValuesIdx = this.dataCrossColumns.Length;
                int dtCrossViewColumnsTitleRowIdx = 0;
                while (dtCrossViewColumnsTitleRowIdx < dtCrossViewColumnsTitle.Rows.Count)
                {
                    foreach (string s in this.dataCrossValues)
                    {
                        dtCrossViewColumnsTitleIncludValues.Rows.Add(dtCrossViewColumnsTitle.Rows[dtCrossViewColumnsTitleRowIdx].ItemArray);
                    }
                    dtCrossViewColumnsTitleRowIdx++;
                }
                //SvMain.DataSource = dtCrossViewColumnsTitleIncludValues;
                //FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                //return 1;
                Function.DisplayToFpReverse(SvMain, dtCrossViewColumnsTitleIncludValues, columnsHeaderBeginRowIndex, columnsHeaderBeginColumnIndex);
                SpanDisplayedFpReverseOneValue(SvMain, dtCrossViewColumnsTitleIncludValues, columnsHeaderBeginRowIndex, columnsHeaderBeginColumnIndex);
                SpanDisplayedFpReverseRowsOneValue(SvMain, dtCrossViewColumnsTitleIncludValues, columnsHeaderBeginRowIndex, columnsHeaderBeginColumnIndex);
            }

            #endregion
            #region 显示合并行
            Function.DisplayToFp(SvMain, dtCrossViewRowsTitle, rowsHeaderBeginRowIndex, rowsHeaderBeginColumnIndex);
            SpanDisplayedFp(SvMain, dtCrossViewRowsTitle, rowsHeaderBeginRowIndex, rowsHeaderBeginColumnIndex);
            SpanDisplayedFpRows(SvMain, dtCrossViewRowsTitle, rowsHeaderBeginRowIndex, rowsHeaderBeginColumnIndex);
            int colIdx = 0;
            foreach (string s in dataCrossRows)
            {
                SvMain.Cells[rowsHeaderBeginRowIndex - 1, rowsHeaderBeginColumnIndex + colIdx].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                SvMain.Cells[rowsHeaderBeginRowIndex - 1, rowsHeaderBeginColumnIndex + colIdx].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                SvMain.Cells[rowsHeaderBeginRowIndex - 1, rowsHeaderBeginColumnIndex + colIdx].Text = dt.Columns[int.Parse(s)].Caption;
                colIdx++;
            }
            #endregion
            #endregion
            #endregion
            #endregion
            //SvMain.DataSource = dtCrossViewRowsTitle;
            //FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            //return 1;
            #endregion
            this.dataRowCount = dtCrossViewRows.Rows.Count;
            #region 设置表格行数
            if (this.SvMain.Rows.Count < DataBeginRowIndex + 1 + dtCrossViewRows.Rows.Count)
            {
                this.SvMain.RowCount = DataBeginRowIndex + 1 + dtCrossViewRows.Rows.Count;
            }
            #endregion

            #region 逐个单元格填充交叉后的数据数据
            Function.DisplayToFp(SvMain, dtCrossViewRows, rowsHeaderBeginRowIndex, columnsHeaderBeginColumnIndex);
            #region 行总计提前
            if (this.IsColumnTotFrontShow)
            {
                int rowTotIndex = -1;
                //查找行总计
                for (int i = 0; i < SvMain.RowCount; i++)
                {
                    if (SvMain.Cells[i, 0].Text == "总计")
                    {
                        rowTotIndex = i;
                        break;
                    }
                }
                if (rowTotIndex != -1)
                {
                    SvMain.Rows.Add(rowsHeaderBeginRowIndex, 1);
                    for (int i = 0; i < SvMain.ColumnCount; i++)
                    {
                        Function.CloneSheetViewCell(SvMain.Cells[rowsHeaderBeginRowIndex, i], SvMain.Cells[rowTotIndex + 1, i]);
                    }
                    SvMain.Rows.Remove(rowTotIndex + 1, 1);
                }
            }
            #endregion

            #region 列总计提前
            if (this.IsRowTotFrontShow)
            {
                int columnTotIndex = -1;
                //查找列总计
                for (int i = 0; i < SvMain.ColumnCount; i++)
                {
                    if (SvMain.Cells[rowsHeaderBeginRowIndex - 1, i].Text == "总计")
                    {
                        columnTotIndex = i;
                        break;
                    }
                }
                if (columnTotIndex != -1)
                {
                    SvMain.Columns.Add(columnsHeaderBeginColumnIndex, 1);

                    for (int i = 0; i < SvMain.RowCount; i++)
                    {
                        Function.CloneSheetViewCell(SvMain.Cells[i, columnsHeaderBeginColumnIndex], SvMain.Cells[i, columnTotIndex + 1]);
                    }
                    SvMain.Columns.Remove(columnTotIndex + 1, 1);
                }
            }
            #endregion

            #region 设置固定
            SvMain.FrozenColumnCount = this.FrozenColumnCount;
            SvMain.FrozenRowCount = this.FrozenRowCount;
            #endregion
            #endregion

            #endregion

            #region 设置分页符
            if (this.rowPageBreak > 0)
            {
                for (int i = 1; (i * this.rowPageBreak + this.DataBeginRowIndex) + 1 < this.SvMain.Rows.Count; i++)
                {
                    this.SvMain.SetRowPageBreak((i * this.rowPageBreak + this.DataBeginRowIndex) + 1, true);
                }
            }
            #endregion

            Function.DrawGridLine(SvMain, this.dataBeginRowIndex, this.dataBeginColumnIndex, dtCrossViewRows.Rows.Count + dtCrossViewColumnsTitleIncludValues.Columns.Count, dtCrossViewRows.Columns.Count + dtCrossViewRowsTitle.Columns.Count);

            this.SvMain.RowCount = this.DataBeginRowIndex + dtCrossViewRows.Rows.Count + dtCrossViewColumnsTitleIncludValues.Columns.Count;

            this.SvMain.ColumnCount = this.DataBeginColumnIndex + dtCrossViewRows.Columns.Count + dtCrossViewRowsTitle.Columns.Count;
           
            return 1;
            #endregion
        }
    }
}
