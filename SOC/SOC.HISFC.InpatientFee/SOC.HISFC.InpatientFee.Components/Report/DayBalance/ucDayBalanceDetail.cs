using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.HISFC.InpatientFee.Components.Report.DayBalance
{
    public partial class ucDayBalanceDetail : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucDayBalanceDetail()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        private Dictionary<string, Object> allMap = null;

        /// <summary>
        /// 数据库管理层
        /// </summary>
        FS.FrameWork.Management.DataBaseManger dbManager = new FS.FrameWork.Management.DataBaseManger();

        #region 枚举

        /// <summary>
        /// 显示数据的类型
        /// </summary>
        public enum EnumShowType
        {
            FarPoint,
            Datawindow
        }

        public enum EnumDetailCondition
        {
            UseMainCondition,
            UseMainData,
            Custom
        }

        #endregion

        #region 属性

        /// <summary>
        /// 当前登陆员
        /// </summary>
        private FS.HISFC.Models.Base.Employee oper = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;

        #region A.参数设置
        private EnumShowType showType = EnumShowType.FarPoint;
        [Category("A.参数设置"), Description("数据窗口显示的类型")]
        public EnumShowType ShowType
        {
            get
            {
                return showType;
            }
            set
            {
                showType = value;
            }
        }

        private ReportQueryInfo report = new ReportQueryInfo();
        [Category("A.参数设置"), Description("设置查询参数")]
        public ReportQueryInfo Report
        {
            get
            {
                return report;
            }
            set
            {
                report = value;
            }
        }

        private string mainDWLabrary = string.Empty;
        [Category("A.参数设置"), Description("设置主窗口的PBD/PBL的路径")]
        public string MainDWLabrary
        {
            get
            {
                return mainDWLabrary;
            }
            set
            {
                mainDWLabrary = value;
            }
        }

        /// <summary>
        /// 主数据窗DataObject
        /// </summary>
        protected string mainDWDataObject = string.Empty;
        [Category("A.参数设置"), Description("设置主数据窗DataObject")]
        [Editor(typeof(FarPointUITypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string MainDWDataObject
        {
            get
            {
                return this.mainDWDataObject;
            }
            set
            {
                this.mainDWDataObject = value;
            }
        }

        #endregion

        /// <summary>
        /// 开始日期
        /// </summary>
        public string dtBegin = string.Empty;

        /// <summary>
        /// 结束日期
        /// </summary>
        public string dtEnd = string.Empty;
        

        #endregion

        #region  私有方法

        /// <summary>
        /// 读取配置文件
        /// </summary>
        private void LoadReportFile()
        {
            if (this.report != null)
            {
                if (string.IsNullOrEmpty(this.report.QueryFilePath))
                {
                    this.report.QueryFilePath = "\\Report\\住院收费报表\\(查询)设置.xml";
                }

                if (string.IsNullOrEmpty(this.report.QueryFilePath) == false)
                {
                    //读取 
                    if (System.IO.File.Exists(FS.FrameWork.WinForms.Classes.Function.CurrentPath + this.report.QueryFilePath) == false)
                    {
                        System.IO.File.Create(FS.FrameWork.WinForms.Classes.Function.CurrentPath + this.report.QueryFilePath).Close();

                        //保存
                        System.IO.StreamWriter swa = new System.IO.StreamWriter(FS.FrameWork.WinForms.Classes.Function.CurrentPath + this.report.QueryFilePath);
                        ReportQueryInfo reportQueryInfo = new ReportQueryInfo();
                        this.report.List = reportQueryInfo.GetDefaults();
                        //取默认值
                        swa.Write(ICommonReportController.XmlSerialization(this.report));
                        swa.Close();
                    }

                    else
                    {
                        System.IO.StreamReader sr = new System.IO.StreamReader(FS.FrameWork.WinForms.Classes.Function.CurrentPath + this.report.QueryFilePath);
                        string xml = sr.ReadToEnd();
                        ReportQueryInfo r = ICommonReportController.XmlDeSerialization<ReportQueryInfo>(xml);
                        this.report.List = r.List;
                        this.report.DataSourceType = r.DataSourceType;
                        sr.Close();
                    }
                }
            }

            if (this.showType == EnumShowType.FarPoint)
            {
                if (string.IsNullOrEmpty(this.mainDWDataObject))
                {
                    this.mainDWDataObject = "\\Report\\住院收费报表\\(报表)设置.xml";
                }
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        private int Query()
        {
            //获取参数
            Dictionary<string, Object> map = new Dictionary<string, object>();
            if (this.report != null)
            {
                map = this.report.GetDefualtSetting();
                if (this.report.List != null)
                {
                    map.Add("dtBeginTime", this.dtBegin);
                    map.Add("dtEndTime", this.dtEnd);
                }
            }

            return this.Query(map);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        private int Query(Dictionary<string, Object> map)
        {
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在查询，请稍后...");
            Application.DoEvents();

            if (this.report != null)
            {
                if (this.report.DataSourceType != null)
                {
                    string sql = string.Empty;
                    DataSet ds = new DataSet();
                    foreach (DataSourceType common in this.report.DataSourceType)
                    {
                        sql = Function.ReplaceValues(common.Sql, map);

                        if (Function.HasSelect(sql))
                        {
                            //使用参数
                            dbManager.ExecQuery(sql, ref ds);
                            if (dbManager.ExecQuery(sql, ref ds) == -1 || ds == null || ds.Tables.Count == 0)
                            {
                                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                                MessageBox.Show(this, "执行数据源" + common.Name + "的sql出错！" + dbManager.Err, "提示>>", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return -1;
                            }
                            DataTable dt = ds.Tables[0];
                            //如果是交叉报表
                            if (common.IsCross)
                            {
                                //dt = this.queryCross(ds.Tables[0], common, map);
                            }

                            #region AddMapData
                            if (common.AddMapData)
                            {
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    //添加数据
                                    for (int j = 0; j < dt.Columns.Count; j++)
                                    {
                                        map.Add(common.Name + ".Rows[" + i + "][" + j + "]", dt.Rows[i][j]);
                                        map.Add(common.Name + ".Rows[" + i + "][" + dt.Columns[j].ColumnName + "]", dt.Rows[i][j]);
                                    }
                                }
                            }
                            #endregion

                            #region AddMapRow
                            if (common.AddMapRow)
                            {
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {

                                    map.Add(common.Name + ".Rows[" + i + "]", dt.Rows[i]);
                                }
                            }
                            #endregion

                            #region AddMapColumn
                            if (common.AddMapColumn)
                            {
                                DataTable dtColumns = new DataTable();

                                for (int i = 0; i < dt.Columns.Count; i++)
                                {
                                    dtColumns.Columns.Add(dt.Columns[i].ColumnName);
                                }
                                DataRow dr = dtColumns.NewRow();
                                for (int i = 0; i < dt.Columns.Count; i++)
                                {
                                    dr[i] = dt.Columns[i].ColumnName;
                                    map.Add(common.Name + ".Columns[" + i + "]", dt.Columns[i].ColumnName);
                                }
                                dtColumns.Rows.Add(dr);
                                map.Add(common.Name + ".Columns", dr);
                            }
                            #endregion

                            map.Add(common.Name, dt);
                        }
                        else
                        {
                            map.Add(common.Name, sql);
                        }
                    }
                }
            }

            allMap = map;

            int ret = this.Retrieve(map);

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();


            return ret;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        private int Retrieve(Dictionary<String, Object> map)
        {
            //通过参数来查询报表，循环每一个单元格查找对应的Tag，如果有则直接将查询结果显示出来
            //如果是多行的结果，则在原来的基础上加上多行的结果
            this.Init();
            //挂起
            this.SuspendLayout();

            this.SetHeaderValue(map);
            this.SetCellValue(map);

            ////恢复
            this.ResumeLayout(false);
            return 1;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        private int Init()
        {
            this.neuSpread1.Sheets[0].RowCount = 0;
            this.SetFormat();
            return 1;
        }

        /// <summary>
        /// 设置FP格式
        /// </summary>
        private void SetFormat()
        {
            if (string.IsNullOrEmpty(this.mainDWDataObject) == false)
            {
                //读取 
                if (System.IO.File.Exists(FS.FrameWork.WinForms.Classes.Function.CurrentPath + this.mainDWDataObject) == false)
                {
                    System.IO.FileStream fs = System.IO.File.Create(FS.FrameWork.WinForms.Classes.Function.CurrentPath + this.mainDWDataObject);
                    this.neuSpread1.Save(fs, false);
                    fs.Close();
                }
                else
                {
                    this.neuSpread1.Open(FS.FrameWork.WinForms.Classes.Function.CurrentPath + this.mainDWDataObject);
                }
            }
        }

        /// <summary>
        /// 设置头值
        /// </summary>
        /// <param name="map"></param>
        private void SetHeaderValue(Dictionary<String, Object> map)
        {
            string cellTag = string.Empty;
            for (int i = 0; i < this.neuSpread1.Sheets[0].ColumnHeader.RowCount; i++)
            {
                int rownum = 0;
                for (int j = 0; j < this.neuSpread1.Sheets[0].ColumnHeader.Columns.Count; j++)
                {
                    if (this.neuSpread1.Sheets[0].ColumnHeader.Cells[i, j].Text != null)
                    {
                        cellTag = this.neuSpread1.Sheets[0].ColumnHeader.Cells[i, j].Text;
                        //执行sql
                        if (map.ContainsKey(cellTag))
                        {
                            if (map[cellTag] is DataTable)
                            {
                                #region 数据源赋值
                                DataTable dt = map[cellTag] as DataTable;
                                if (dt.Rows.Count > 0)
                                {
                                    if (dt.Rows.Count > 1)
                                    {
                                        this.neuSpread1.Sheets[0].ColumnHeader.Rows.Add(i, dt.Rows.Count - 1);
                                    }
                                    if (j + dt.Columns.Count > this.neuSpread1.Sheets[0].ColumnHeader.Columns.Count)
                                    {
                                        this.neuSpread1.Sheets[0].ColumnHeader.Columns.Count += dt.Columns.Count - (this.neuSpread1.Sheets[0].ColumnHeader.Columns.Count - j);
                                    }
                                    for (int row = 0; row < dt.Rows.Count; row++)
                                    {
                                        for (int col = 0; col < dt.Columns.Count; col++)
                                        {
                                            this.neuSpread1.Sheets[0].ColumnHeader.Cells[i + row, j + col].Value = dt.Rows[row][col];
                                        }
                                    }

                                    rownum += dt.Rows.Count - 1;
                                    j = j + dt.Columns.Count;

                                }
                                #endregion
                            }
                            else if (map[cellTag] is DataRow)
                            {
                                #region 数据源赋值
                                DataRow dr = map[cellTag] as DataRow;
                                if (dr.ItemArray.Length > 0)
                                {
                                    //每行赋值
                                    if (j + dr.ItemArray.Length > this.neuSpread1.Sheets[0].ColumnHeader.Columns.Count)
                                    {
                                        this.neuSpread1.Sheets[0].ColumnHeader.Columns.Count += dr.ItemArray.Length + j - this.neuSpread1.Sheets[0].ColumnHeader.Columns.Count;
                                    }
                                    for (int col = 0; col < dr.ItemArray.Length; col++)
                                    {
                                        this.neuSpread1.Sheets[0].ColumnHeader.Cells[i, j + col].Value = dr.ItemArray[col];
                                    }
                                    j = j + dr.ItemArray.Length;
                                }
                                #endregion
                            }
                            else
                            {
                                #region 数据源赋值
                                this.neuSpread1.Sheets[0].ColumnHeader.Cells[i, j].Value = map[cellTag];
                                #endregion
                            }
                        }
                        else
                        {
                            cellTag = Function.ReplaceValues(cellTag, map);
                            this.neuSpread1.Sheets[0].ColumnHeader.Cells[i, j].Value = cellTag;
                        }

                    }
                }
                i = i + rownum;
            }
        }

        /// <summary>
        /// 设置具体值
        /// </summary>
        /// <param name="map"></param>
        private void SetCellValue(Dictionary<String, Object> map)
        {
            string cellTag = string.Empty;
            string cellFormula = string.Empty;

            for (int i = 0; i < this.neuSpread1.Sheets[0].RowCount; i++)
            {
                int rownum = 0;
                for (int j = 0; j < this.neuSpread1.Sheets[0].ColumnCount; j++)
                {
                    #region cellTag
                    if (this.neuSpread1.Sheets[0].Cells[i, j].Tag != null)
                    {
                        cellTag = this.neuSpread1.Sheets[0].Cells[i, j].Tag.ToString();
                        if (string.IsNullOrEmpty(cellTag))
                        {
                            continue;
                        }
                        if (Function.HasSelect(cellTag))
                        {
                            //执行sql
                            cellTag = Function.ReplaceValues(cellTag, map);
                            #region 查询赋值
                            DataSet ds = new DataSet();
                            if (dbManager.ExecQuery(cellTag, ref ds) < 0)
                            {
                                MessageBox.Show(string.Format("查询{0}行{1}列的数据失败", i, j) + this.dbManager.Err, "提示");
                                return;
                            }

                            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                            {
                                //每行赋值
                                if (ds.Tables[0].Rows.Count > 1)
                                {
                                    this.neuSpread1.Sheets[0].Rows.Add(i, ds.Tables[0].Rows.Count - 1);
                                }
                                if (j + ds.Tables[0].Columns.Count > this.neuSpread1.Sheets[0].ColumnCount)
                                {
                                    this.neuSpread1.Sheets[0].ColumnCount += ds.Tables[0].Columns.Count + j - this.neuSpread1.Sheets[0].ColumnCount;
                                }
                                for (int row = 0; row < ds.Tables[0].Rows.Count; row++)
                                {
                                    for (int col = 0; col < ds.Tables[0].Columns.Count; col++)
                                    {
                                        this.neuSpread1.Sheets[0].Cells[i + row, j + col].Value = ds.Tables[0].Rows[row][col];
                                    }
                                }

                                rownum += ds.Tables[0].Rows.Count - 1;
                                j = j + ds.Tables[0].Columns.Count;
                            }

                            #endregion
                        }
                        else
                        {
                            if (map.ContainsKey(cellTag))
                            {
                                if (map[cellTag] is DataTable)
                                {
                                    #region 数据源赋值
                                    DataTable dt = map[cellTag] as DataTable;
                                    if (dt.Rows.Count > 0)
                                    {
                                        //计算数据
                                        cellFormula = this.neuSpread1.Sheets[0].Cells[i, j].Formula;
                                        if (string.IsNullOrEmpty(cellFormula) == false)
                                        {
                                            this.neuSpread1.Sheets[0].Cells[i, j].Formula = "";
                                            this.neuSpread1.Sheets[0].Cells[i, j].Value = dt.Compute(cellFormula, "");
                                        }
                                        else
                                        {
                                            if (dt.Rows.Count > 1)
                                            {
                                                this.neuSpread1.Sheets[0].Rows.Add(i, dt.Rows.Count - 1);
                                            }
                                            if (j + dt.Columns.Count > this.neuSpread1.Sheets[0].ColumnCount)
                                            {
                                                this.neuSpread1.Sheets[0].ColumnCount += dt.Columns.Count - (this.neuSpread1.Sheets[0].ColumnCount - j);
                                            }
                                            for (int row = 0; row < dt.Rows.Count; row++)
                                            {
                                                int fpColumnSpanIndex = 0;
                                                for (int col = 0; col < dt.Columns.Count; col++)
                                                {
                                                    this.neuSpread1.Sheets[0].Cells[i + row, j + fpColumnSpanIndex].Value = dt.Rows[row][col];
                                                    this.neuSpread1.Sheets[0].Cells[i + row, j + fpColumnSpanIndex].Locked = true;

                                                    //ColumnSpan
                                                    fpColumnSpanIndex += this.neuSpread1.Sheets[0].Cells[i + row, j + fpColumnSpanIndex].ColumnSpan;
                                                    if (fpColumnSpanIndex < col)
                                                    {
                                                        fpColumnSpanIndex = col;
                                                    }
                                                }
                                            }

                                            rownum += dt.Rows.Count - 1;
                                            j = j + dt.Columns.Count;
                                        }
                                    }
                                    #endregion
                                }
                                else if (map[cellTag] is DataRow)
                                {
                                    #region 数据源赋值
                                    DataRow dr = map[cellTag] as DataRow;
                                    if (dr.ItemArray.Length > 0)
                                    {
                                        //每行赋值
                                        if (j + dr.ItemArray.Length > this.neuSpread1.Sheets[0].ColumnCount)
                                        {
                                            this.neuSpread1.Sheets[0].ColumnCount += dr.ItemArray.Length + j - this.neuSpread1.Sheets[0].ColumnCount;
                                        }
                                        for (int col = 0; col < dr.ItemArray.Length; col++)
                                        {
                                            this.neuSpread1.Sheets[0].Cells[i, j + col].Value = dr.ItemArray[col];
                                        }
                                        j = j + dr.ItemArray.Length;
                                    }
                                    #endregion
                                }
                                else
                                {
                                    #region 数据源赋值
                                    this.neuSpread1.Sheets[0].Cells[i, j].Value = map[cellTag];
                                    this.neuSpread1.Sheets[0].Cells[i, j].Formula = cellFormula;
                                    #endregion
                                }
                            }
                            else
                            {
                                //执行sql
                                cellTag = Function.ReplaceValues(cellTag, map);
                                this.neuSpread1.Sheets[0].Cells[i, j].Value = cellTag;
                                this.neuSpread1.Sheets[0].Cells[i, j].Formula = cellFormula;
                            }
                        }
                    }
                    #endregion cellTag
                }

                i = i + rownum;
            }
        }

        #endregion

        #region 重载

        /// <summary>
        /// 加载事件
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            this.LoadReportFile();
            this.Query();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnQuery(object sender, object neuObject)
        {
            return this.Query();
        }

        public override int Export(object sender, object neuObject)
        {
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
            this.neuSpread1.PrintSheet(0);
            return 1;
        }

        private void btPrint_Click(object sender, EventArgs e)
        {
            this.neuSpread1.PrintSheet(0);

        }

        /// <summary>
        /// 打印预览
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnPrintPreview(object sender, object neuObject)
        {
            return 1;
        }

        #endregion

    }
}
