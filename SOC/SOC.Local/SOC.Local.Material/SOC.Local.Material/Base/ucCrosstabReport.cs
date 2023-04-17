using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.Material.Base
{
    public partial class ucCrosstabReport : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucCrosstabReport()
        {
            InitializeComponent();
        }
        #region 变量

        #region 私有变量

        /// <summary>
        /// 是否使用科室查询条件
        /// </summary>
        protected bool isHaveDept = false;

        /// <summary>
        /// 二级权限代码，通过权限加载科室，为空时加载所有科室
        /// </summary>
        private string privClass2Code = "5510";

        /// <summary>
        /// 标题
        /// </summary>
        private string title = "";

        /// <summary>
        /// sql语句id
        /// </summary>
        private string sqlId = "";


        private int rowHeadCount = 0;


        /// <summary>
        /// 是否包含合计行
        /// </summary>
        private bool haveSum = true;


        private int columnStart = 1;


        private int rowStart = 2;


        /// <summary>
        /// 是否包含合计列
        /// </summary>
        private bool haveRowSum = true;

        /// <summary>
        /// 是否将空值转为零
        /// </summary>
        private bool replaceNullToZero = true;

        /// <summary>
        /// 时间格式
        /// </summary>
        private string dataTiemFromat = "yyyy-MM-dd HH:mm:ss";

        /// <summary>
        /// 时间格式的列
        /// </summary>
        private string timeFormatColumn = string.Empty;


        /// <summary>
        /// 默认查询的时间范围：天
        /// </summary>
        private int queryDays = 1;

        /// <summary>
        /// 打印机名称
        /// </summary>
        private string pageName = "A3T";

        private bool isPreview = false;

        private int paperWidth = 1625;

        private int paperHeight = 1149;

        bool isLandScape = false;

        /// <summary>
        /// 自定义打印区域设置
        /// </summary>
        private int customWidth = 1500;

        private int customHeight = 1130;

        /// <summary>
        /// 是否使用系统打印
        /// </summary>
        private bool isSystemPrint = true;

        /// <summary>
        /// 打印变量
        /// </summary>
        private System.Drawing.Printing.PrintDocument PrintDocument = new System.Drawing.Printing.PrintDocument();
        private FS.HISFC.BizLogic.Manager.UserPowerDetailManager priPowerMgr = new FS.HISFC.BizLogic.Manager.UserPowerDetailManager();
        /// <summary>
        /// 报表统计
        /// </summary>
        public FS.HISFC.BizLogic.Manager.Department reportMgr = new FS.HISFC.BizLogic.Manager.Department();

        private DataBaseLogic logic = new DataBaseLogic();

        private string storage = "2009";

        #endregion 私有变量

        #region 保护变量
        #endregion 保护变量

        #region 公开变量

        #endregion 公开变量

        #endregion 变量

        #region 属性

        [Description("是否打印预览")]
        public bool IsPreview
        {
            get { return this.isPreview; }
            set { this.isPreview = value; }
        }

        [Description("打印机名称")]
        public string PageName
        {
            get { return this.pageName; }
            set { this.pageName = value; }
        }


        [Description("设置打印机像素宽度")]
        public int PaperWidth
        {
            get { return this.paperWidth; }
            set { this.paperWidth = value; }
        }


        [Description("设置打印机像素高度")]
        public int PaperHeight
        {
            get { return this.paperHeight; }
            set { this.paperHeight = value; }
        }


        [Description("是否使用系统打印")]
        public bool IsSystemPrint
        {
            get { return this.isSystemPrint; }
            set { this.isSystemPrint = value; }
        }

        [Description("是否横打")]
        public bool IsLandScape
        {
            get
            {
                return isLandScape;
            }
            set
            {
                isLandScape = value;

            }
        }

        [Description("自定义宽度设置")]
        public int CustomWidth
        {
            get { return this.customWidth; }
            set { this.customWidth = value; }
        }

        [Description("自定义高度设置")]
        public int CustomHeight
        {
            get { return this.customHeight; }
            set { this.customHeight = value; }
        }

        [Description("行表头数量")]
        public virtual int RowHeadCount
        {
            get { return this.rowHeadCount; }
            set { this.rowHeadCount = value; }
        }

        [Category("查询设置"), Description("行合计开始索引")]
        public virtual int RowStart
        {
            get { return this.rowStart; }
            set { this.rowStart = value; }
        }

        [Category("查询设置"), Description("列合计开始索引")]
        public virtual int ColumnStart
        {
            get { return this.columnStart; }
            set { this.columnStart = value; }
        }
        [Category("查询设置"), Description("查询仓库对应的编码")]
        public string Storage
        {
            get { return storage; }
            set { storage = value; }
        }

        /// <summary>
        /// 是否使用科室查询条件
        /// </summary>
        [Category("查询设置"), Description("是否添加科室查询条件,true添加,false不添加"), DefaultValue("false")]
        public bool IsHaveDept
        {
            get
            {
                return isHaveDept;
            }
            set
            {
                isHaveDept = value;
                this.lbDept.Visible = value;
                this.cmbDept.Visible = value;
            }
        }

        /// <summary>
        /// 二级权限代码，通过权限加载科室，为空时加载所有科室
        /// </summary>
        [Category("查询设置"), Description("二级权限代码，通过权限加载科室，为空时加载所有科室")]
        public string PrivClass2Code
        {
            get
            {
                return privClass2Code;
            }
            set
            {
                privClass2Code = value;
            }
        }

        /// <summary>
        /// 标题
        /// </summary>
        [Category("查询设置"), Description("标题")]
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
            }
        }

        /// <summary>
        /// sql语句id
        /// </summary>
        [Category("查询设置"), Description("sql语句id")]
        public string SqlId
        {
            get
            {
                return sqlId;
            }
            set
            {
                sqlId = value;
            }
        }

        [Category("查询设置"), Description("是否添加合计,true添加,false不添加"), DefaultValue("true")]
        public bool HaveSum
        {
            get
            {
                return haveSum;
            }
            set
            {
                haveSum = value;
            }
        }

        /// <summary>
        /// 是否包含合计列
        /// </summary>
        [Category("查询设置"), Description("是否添加行合计,true添加,false不添加"), DefaultValue("true")]
        public bool HaveRowSum
        {
            get
            {
                return haveRowSum;
            }
            set
            {
                haveRowSum = value;
            }
        }

        /// <summary>
        /// 是否将空值转为零
        /// </summary>
        [Category("查询设置"), Description("是否将空值转为零,true转换,false不转换"), DefaultValue("true")]
        public bool ReplaceNullToZero
        {
            get
            {
                return replaceNullToZero;
            }
            set
            {
                replaceNullToZero = value;
            }
        }



        /// <summary>
        /// 时间格式
        /// </summary>
        [Category("查询设置"), Description("时间格式，默认：yyyy-MM-dd HH:mm:ss"), DefaultValue("yyyy-MM-dd HH:mm:ss")]
        public string DataTiemFromat
        {
            get
            {
                return dataTiemFromat;
            }
            set
            {
                dataTiemFromat = value;
            }
        }


        /// <summary>
        /// 时间格式
        /// </summary>
        [Category("查询设置"), Description("按时间格式显示的列")]
        public string TimeFormatColumn
        {
            get
            {
                return timeFormatColumn;
            }
            set
            {
                timeFormatColumn = value;
            }
        }


        /// <summary>
        /// 默认查询的时间范围：天
        /// </summary>
        [Category("查询设置"), Description("默认查询的时间范围，单位：天，默认1天"), DefaultValue("1")]
        public int QueryDays
        {
            get
            {
                return queryDays;
            }
            set
            {
                queryDays = value;
            }
        }

        private string dataFromat = "";

        [Category("查询设置"), Description("数据显示格式"), DefaultValue("")]
        public string DataFromat
        {
            get
            {
                return dataFromat;
            }
            set
            {
                dataFromat = value;
            }
        }

        #endregion 属性

        #region 方法

        #region 资源释放
        #endregion 资源释放

        #region 克隆
        #endregion 克隆

        #region 私有方法

        /// <summary>
        /// 初始化下拉框
        /// </summary>
        protected virtual void InitComBox()
        {
            //科室查询条件
            if (this.isHaveDept)
            {
                if (string.IsNullOrEmpty(this.privClass2Code.Trim()))
                {
                    ArrayList alDept = reportMgr.GetDeptmentAll();

                    FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                    obj.ID = "ALL";
                    obj.Name = "全部";
                    alDept.Insert(0, obj);

                    this.cmbDept.AddItems(alDept);
                }
                else
                {
                    List<FS.FrameWork.Models.NeuObject> alPrivDept = priPowerMgr.QueryUserPriv(FS.FrameWork.Management.Connection.Operator.ID, this.PrivClass2Code.Trim());
                    if (alPrivDept != null)
                    {
                        this.cmbDept.AddItems(new ArrayList(alPrivDept.ToArray()));
                    }
                }
            }



            ArrayList monthlyList = logic.GetAllMonthly(this.Storage);
            if (monthlyList != null && monthlyList.Count > 0)
            {
                this.cmbMonthly.AddItems(monthlyList);
            }

            this.lbDept.Visible = this.isHaveDept;
            this.cmbDept.Visible = this.isHaveDept;
            //加载所有的月结

        }


        private void Init()
        {
            InitComBox();

            //表头
            this.lbTitle.Text = this.title;
            //时间格式
            this.dtpFromDate.CustomFormat = this.dataTiemFromat;
            this.dtpEndDate.CustomFormat = this.dataTiemFromat;

            if (!this.DesignMode)
            {
                this.dtpEndDate.Value = this.reportMgr.GetDateTimeFromSysDateTime();

            }

            this.dtpFromDate.Value = new DateTime(this.dtpFromDate.Value.Year, this.dtpFromDate.Value.Month, this.dtpFromDate.Value.Day, 0, 0, 0);
            this.dtpEndDate.Value = new DateTime(this.dtpEndDate.Value.Year, this.dtpEndDate.Value.Month, this.dtpEndDate.Value.Day, 23, 59, 59);
            this.dtpFromDate.Value = this.dtpEndDate.Value.AddDays(-this.queryDays).AddSeconds(1);
        }

        /// <summary>
        /// 查询
        /// </summary>
        private void Query()
        {
            if (this.cmbMonthly.Tag == null || this.cmbMonthly.Text == "")
            {
                MessageBox.Show("请选择月结日期");
                return;
            }
            try
            {
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在查询数据，请稍等");
                Application.DoEvents();
                //执行sql语句，获取DataTable
                DataTable dt = this.GetDataTableBySql();
                if (dt == null)
                {
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    return;
                }
                //根据sql查询结果的DataTable生成交叉表的DataTable
                //DataTable dtCross = this.GetCrossDataTable(dt);
                //if (dtCross == null)
                //{
                //    FrameWork.WinForms.Classes.Function.HideWaitForm();
                //    return;
                //}

                //添加合计
                this.ComputeSum(dt);
                //将空值转为零
                //this.ConverNullToZero(dt);
                //this.ConverDataFromat(dt);
                //FarPoint赋值，设置格式
                this.SetFp(dt);

                this.SetRowHeadByDataTable(dt);

                this.SetColumnVisible(dt);

                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }
            catch (Exception ex)
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show("查询数据发生错误：" + ex.Message);
            }
        }

        private void ConverDataFromat(DataTable dtCross)
        {
            if (this.dataFromat.Trim() == "")
            {
                return;
            }
            for (int i = 0; i < dtCross.Rows.Count; i++)
            {
                for (int j = 1; j < dtCross.Columns.Count; j++)
                {
                    try
                    {
                        if (string.IsNullOrEmpty(dtCross.Rows[i][j].ToString()))
                        {
                        }
                        else
                        {
                            dtCross.Rows[i][j] = System.Math.Round(FS.FrameWork.Function.NConvert.ToDecimal(dtCross.Rows[i][j].ToString()), 2);
                            //FrameWork.Function.NConvert.ToDecimal(dtCross.Rows[i][j].ToString()).ToString(this.dataFromat);
                        }
                    }
                    catch
                    {
                    }
                }
            }
        }

        /// <summary>
        /// 执行sql语句，获取DataTable
        /// </summary>
        protected virtual DataTable GetDataTableBySql()
        {
            //执行sql语句
            if (string.IsNullOrEmpty(this.sqlId.Trim()))
            {
                return null;
            }
            //if (this.dtpFromDate.Value > this.dtpEndDate.Value)
            //{
            //    MessageBox.Show("开始时间不能大于截止时间，请重新输入");
            //    return null;
            //}


            string[] parm = getParm();


            DataSet ds = new DataSet();
            if (reportMgr.ExecQuery(this.sqlId, ref ds, parm) < 0)
            {
                MessageBox.Show("查询数据出错：" + reportMgr.Err);
                return null;
            }
            return ds.Tables[0];
        }


        //传递参数报表
        protected virtual string[] getParm()
        {
            string[] parm = null;

            //if (this.isHaveDept)
            //{
            //    parm = new string[] { this.dtpFromDate.Value.ToString("yyyy-MM-dd HH:mm:ss"), this.dtpEndDate.Value.ToString("yyyy-MM-dd HH:mm:ss"), (this.cmbDept.Tag.ToString() == string.Empty ? "ALL" : this.cmbDept.Tag.ToString()) };
            //}
            //else
            //{
            //    parm = new string[] { this.dtpFromDate.Value.ToString("yyyy-MM-dd HH:mm:ss"), this.dtpEndDate.Value.ToString("yyyy-MM-dd HH:mm:ss"), "ALL" };
            //}
            parm = new string[] { this.cmbMonthly.Tag.ToString() };
            return parm;

        }


        /// <summary>
        /// 根据sql查询结果的DataTable生成交叉表表的DataTable
        /// </summary>
        /// <param name="dt">原DataTable</param>
        /// <returns>CrossDataTable</returns>
        private DataTable GetCrossDataTable(DataTable dt)
        {
            if (dt.Columns.Count < 3)
            {
                MessageBox.Show("sql语句错误：交叉报表数据列不能少于3列");
                return null;
            }
            DataTable dtCross = new DataTable();
            //添加列
            //第一列、名字为空
            dtCross.Columns.Add(new DataColumn(" "));
            foreach (DataRow drCol in dt.Rows)
            {
                string colName = drCol[1].ToString();
                if (dtCross.Columns.Contains(colName))
                {
                    continue;
                }
                dtCross.Columns.Add(colName);
            }
            //添加数据
            Hashtable htRow = new Hashtable();

            foreach (DataRow drRow in dt.Rows)
            {
                string rowName = drRow[0].ToString();

                if (htRow.ContainsKey(rowName))
                {

                    DataRow drAdded = htRow[rowName] as DataRow;
                    drAdded[drRow[1].ToString()] = (FS.FrameWork.Function.NConvert.ToDecimal(drAdded[drRow[1].ToString()].ToString())
                                                                            + FS.FrameWork.Function.NConvert.ToDecimal(drRow[2].ToString())).ToString("#.####");
                }
                else
                {
                    DataRow drNew = dtCross.NewRow();
                    htRow.Add(rowName, drNew);
                    dtCross.Rows.Add(drNew);
                    drNew[0] = rowName;
                    drNew[drRow[1].ToString()] = FS.FrameWork.Function.NConvert.ToDecimal(drRow[2].ToString()).ToString("#.####");
                }
            }
            return dtCross;
        }



        /// <summary>
        /// 根据datatable，把相应列赋值行头
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public virtual DataTable SetRowHeadByDataTable(DataTable dt)
        {
            int row = dt.Rows.Count;
            this.neuSpread1_Sheet1.RowHeader.Columns[rowHeadCount - 1].Width = 90;
            this.neuSpread1_Sheet1.RowHeader.Columns[RowHeadCount - 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            for (int i = 0; i < dt.Rows.Count - 1; i++)
            {
                for (int j = 0; j < rowHeadCount; j++)
                {
                    this.neuSpread1_Sheet1.Columns[j].Visible = false;
                    this.neuSpread1_Sheet1.RowHeader.Cells[i, j].Text = dt.Rows[i][j].ToString();
                }
            }
            return dt;
        }


        /// <summary>
        /// 添加合计
        /// </summary>
        /// <param name="dtCross">CrossDataTable</param> 
        public virtual void ComputeSum(DataTable dtCross)
        {

            if (dtCross.Rows.Count > 0)
            {

                //列合计
                if (this.haveSum)
                {
                    DataRow drSum = dtCross.NewRow();
                    dtCross.Rows.Add(drSum);
                    drSum[0] = "合计:";
                    for (int i = columnStart; i < dtCross.Columns.Count; i++)
                    {
                        DataColumn dc = dtCross.Columns[i];
                        decimal sum = 0;


                        if (System.Text.RegularExpressions.Regex.IsMatch(dtCross.Rows[0][dc.ColumnName].ToString(), "^\\d+\\.*\\d*$"))
                        {
                            foreach (DataRow dr in dtCross.Rows)
                            {
                                sum += FS.FrameWork.Function.NConvert.ToDecimal(dr[dc.ColumnName].ToString());
                            }
                        }

                        if (sum != 0)
                        {
                            drSum[dc.ColumnName] = sum;
                        }
                    }
                }
                //行合计
                if (this.haveRowSum)
                {
                    DataColumn dcSum = new DataColumn("合计");
                    dtCross.Columns.Add(dcSum);
                    for (int i = 0; i < dtCross.Rows.Count; i++)
                    {
                        DataRow drR = dtCross.Rows[i];
                        decimal rowSum = 0;

                        for (int j = rowStart; j < dtCross.Columns.Count; j++)
                        {
                            rowSum += FS.FrameWork.Function.NConvert.ToDecimal(drR[dtCross.Columns[j].ColumnName].ToString());
                        }
                        drR["合计"] = rowSum.ToString("F2");
                    }

                }


            }


        }

        /// <summary>
        /// FarPoint赋值，设置格式
        /// </summary>
        /// <param name="dtCross">CrossDataTable</param>
        //修改SetFp
        public virtual void SetFp(DataTable dtCross)
        //private void SetFp(DataTable dtCross)
        {
            //查询信息赋值 yyyy-MM-dd HH:mm:ss
            this.lbQueryInfo.Text = "时间范围：" + this.dtpFromDate.Value.ToString("yyyy-MM-dd") + " 至 " + this.dtpEndDate.Value.ToString("yyyy-MM-dd");
            if (isHaveDept)
            {
                this.lbQueryInfo.Text += "  查询科室：" + this.cmbDept.Text;
            }
            //数据源

            this.neuSpread1_Sheet1.RowCount = 0;
            this.neuSpread1_Sheet1.DataSource = dtCross;
            //宽度
            for (int i = 0; i < this.neuSpread1_Sheet1.ColumnCount; i++)
            {
                //+ 8
                this.neuSpread1_Sheet1.Columns[i].Width = this.neuSpread1_Sheet1.Columns[i].GetPreferredWidth();
                if (i < this.ColumnStart)
                {
                    this.neuSpread1_Sheet1.Columns[i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                }
                else
                {
                    this.neuSpread1_Sheet1.Columns[i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                }

            }
            //表头位置
            //int windowX = this.Width;
            decimal spreadWith = 0;
            foreach (FarPoint.Win.Spread.Column fpCol in this.neuSpread1_Sheet1.Columns)
            {
                spreadWith += (decimal)fpCol.Width;
            }
            if (spreadWith > this.plPrint.Width)
            {
                spreadWith = this.plPrint.Width;
            }

            int titleWidth = this.lbTitle.Size.Width;
            int titleX = FS.FrameWork.Function.NConvert.ToInt32((spreadWith - titleWidth) / 2);
            if (titleX <= 0)
            {
                titleX = 1;
            }
            this.lbTitle.Location = new Point(titleX, this.lbTitle.Location.Y);
        }


        /// <summary>
        /// 设置列的可见性
        /// </summary>
        public virtual void SetColumnVisible(DataTable dt)
        {
            FarPoint.Win.Spread.CellType.DateTimeCellType time = new FarPoint.Win.Spread.CellType.DateTimeCellType();
            time.DateTimeFormat = FarPoint.Win.Spread.CellType.DateTimeFormat.UserDefined;
            time.UserDefinedFormat = this.dataTiemFromat;

            if (!string.IsNullOrEmpty(timeFormatColumn))
            {
                string[] column = timeFormatColumn.Split(',');
                for (int i = 0; i < column.Length; i++)
                {
                    this.neuSpread1_Sheet1.Columns[FS.FrameWork.Function.NConvert.ToInt32(column[i])].CellType = time;
                    this.neuSpread1_Sheet1.Columns[FS.FrameWork.Function.NConvert.ToInt32(column[i])].Width = 120;
                }
            }
        }

        /// <summary>
        /// 将空值转为零
        /// </summary>
        /// <param name="dtCross"></param>
        private void ConverNullToZero(DataTable dtCross)
        {
            if (!this.replaceNullToZero)
            {
                return;
            }
            for (int i = 0; i < dtCross.Rows.Count; i++)
            {
                for (int j = 1; j < dtCross.Columns.Count; j++)
                {
                    if (string.IsNullOrEmpty(dtCross.Rows[i][j].ToString()))
                    {
                        dtCross.Rows[i][j] = "0";
                    }
                }
            }
        }

        #endregion 私有方法

        #region 保护方法
        #endregion 保护方法

        #region 公开方法
        #endregion 公开方法

        #endregion 方法

        #region 事件

        /// <summary>
        /// 窗体载入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucCrosstabReport_Load(object sender, EventArgs e)
        {
            this.PrintDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(PrintDocument_PrintPage);
            this.Init();
        }

        /// <summary>
        /// 查询事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnQuery(object sender, object neuObject)
        {
            this.Query();
            return 1;
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnPrint(object sender, object neuObject)
        {
            #region 自定义打印
            if (!isSystemPrint)
            {
                this.PrintDocument.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("A3", 1149, 1625);
                this.PrintDocument.DefaultPageSettings.Landscape = true;

                //this.PrintDocument.PrinterSettings.PaperSizes = new System.Drawing.Printing.PaperSize("A3", 1149, 1625);
                //this.PrintDocument.PrinterSettings.Landscape = true;

                PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();

                printPreviewDialog.Document = this.PrintDocument;


                //PrintPreviewControl previewControl = new PrintPreviewControl();
                //previewControl.Document = this.printDocument1;

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
                    return -1;
                }

                return 0;
            }
            #endregion


            #region 系统打印
            //if (MessageBox.Show("是否打印?", "提示信息", MessageBoxButtons.YesNo) == DialogResult.No)
            //{
            //    return 1;
            //}
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            FS.HISFC.Models.Base.PageSize size = new FS.HISFC.Models.Base.PageSize(this.pageName, this.paperWidth, this.paperHeight);
            print.SetPageSize(size);

            // print.ShowPageSetup();
            //print.SetPageSize(new HISFC.Models.Base.PageSize("asdf", 1145, 800));
            //System.Drawing.Printing.PaperKind paperKind = System.Drawing.Printing.PaperKind.
            //print.SetPageSize(System.Drawing.Printing.PaperKind.Custom);
            //print.PrintPreview(0, 0, this.plPrint);
            if (isPreview)
            {
                print.PrintPreview(0, 0, this.plPrint);
            }
            else
            {
                print.PrintPage(0, 0, this.plPrint);
            }
            return 1;
            #endregion
        }

        void PrintDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString(lbTitle.Text, lbTitle.Font, new SolidBrush(this.lbTitle.ForeColor), this.lbTitle.Location);
            e.Graphics.DrawString(lbQueryInfo.Text, lbQueryInfo.Font, new SolidBrush(this.lbQueryInfo.ForeColor), this.lbQueryInfo.Location);

            this.neuSpread1_Sheet1.PrintInfo.Orientation = FarPoint.Win.Spread.PrintOrientation.Landscape;
            this.neuSpread1_Sheet1.PrintInfo.Preview = true;
            this.neuSpread1_Sheet1.PrintInfo.ShowColumnHeaders = true;
            this.neuSpread1_Sheet1.PrintInfo.ShowRowHeaders = true;
            this.neuSpread1_Sheet1.PrintInfo.PaperSize = PrintDocument.DefaultPageSettings.PaperSize;
            if (maxPageNO == 1)
            {
                maxPageNO = this.neuSpread1.GetOwnerPrintPageCount(e.Graphics, new Rectangle(this.neuSpread1.Location.X, this.neuSpread1.Location.Y, this.customWidth, this.customHeight - this.lbQueryInfo.Location.Y - this.lbQueryInfo.Height - 20), 0);
            }
            this.neuSpread1.OwnerPrintDraw(e.Graphics, new Rectangle(this.neuSpread1.Location.X, this.neuSpread1.Location.Y, this.customWidth, this.customHeight - this.lbQueryInfo.Location.Y - this.lbQueryInfo.Height - 20), 0, this.curPageNO);

            if (this.curPageNO < maxPageNO)
            {
                this.curPageNO++;
                e.HasMorePages = true;
            }
            else
            {
                e.HasMorePages = false;
                curPageNO = 1;
            }
        }
        int curPageNO = 1;
        int maxPageNO = 1;



        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        public override int Export(object sender, object neuObject)
        {
            if (this.neuSpread1.Export() == 1)
            {
                MessageBox.Show("导出成功");
            }

            return base.Export(sender, neuObject);
        }

        /// <summary>
        /// 打印预览
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnPrintPreview(object sender, object neuObject)
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            print.PrintPreview(0, 0, this.plPrint);

            return 1;
        }

        #endregion 事件

        #region 接口实现
        #endregion 接口实现

    }
}
