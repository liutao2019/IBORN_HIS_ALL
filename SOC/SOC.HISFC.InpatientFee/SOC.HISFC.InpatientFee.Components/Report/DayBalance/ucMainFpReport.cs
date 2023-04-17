using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FS.SOC.HISFC.BizProcess.CommonInterface;

namespace FS.SOC.HISFC.InpatientFee.Components.Report.DayBalance
{
    public partial class ucMainFpReport : FS.FrameWork.WinForms.Controls.ucBaseControl, ICommonReportController.IMainReportForm
    {
        public ucMainFpReport()
        {
            InitializeComponent();

            this.neuSpread1.SelectionChanged += new FarPoint.Win.Spread.SelectionChangedEventHandler(neuSpread1_SelectionChanged);
        }

        #region 变量

        private string dataWindowObject = string.Empty;
        private string libraryList = string.Empty;
        FS.FrameWork.Management.DataBaseManger dbMgr = new FS.FrameWork.Management.DataBaseManger();
        #endregion

        #region 方法

        private void SetFormat()
        {
            if (string.IsNullOrEmpty(this.dataWindowObject) == false)
            {
                //读取 
                if (System.IO.File.Exists(FS.FrameWork.WinForms.Classes.Function.CurrentPath + dataWindowObject) == false)
                {
                    System.IO.FileStream fs = System.IO.File.Create(FS.FrameWork.WinForms.Classes.Function.CurrentPath + dataWindowObject);
                    this.neuSpread1.Save(fs, false);
                    fs.Close();
                }
                else
                {
                    this.neuSpread1.Open(FS.FrameWork.WinForms.Classes.Function.CurrentPath + dataWindowObject);
                }
            }
        }

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
                            if (dbMgr.ExecQuery(cellTag, ref ds) < 0)
                            {
                                CommonController.CreateInstance().MessageBox(string.Format("查询{0}行{1}列的数据失败", i, j) + dbMgr.Err, MessageBoxIcon.Warning);
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
                                                for (int col = 0; col < dt.Columns.Count; col++)
                                                {
                                                    this.neuSpread1.Sheets[0].Cells[i + row, j + col].Value = dt.Rows[row][col];
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

        #region 事件

        protected override void OnLoad(EventArgs e)
        {
            this.SetFormat();
            base.OnLoad(e);
        }

        void neuSpread1_SelectionChanged(object sender, FarPoint.Win.Spread.SelectionChangedEventArgs e)
        {
            if (this.OnSelectRowHandler != null)
            {
                this.OnSelectRowHandler(this.neuSpread1.Sheets[0].ActiveRowIndex + 1);
            }
        }
        #endregion

        #region IMainReportForm 成员

        public int Init()
        {
            this.neuSpread1.Sheets[0].RowCount = 0;
            this.SetFormat();
            return 1;
        }

        public string DataWindowObject
        {
            get
            {
                return this.dataWindowObject;
            }
            set
            {
                this.dataWindowObject = value;
            }
        }

        public string LibraryList
        {
            get
            {
                return this.libraryList;
            }
            set
            {
                this.libraryList = value;
            }
        }

        public int Retrieve(params object[] objects)
        {
            //通过参数来查询报表，循环每一个单元格查找对应的Tag，如果有则直接将查询结果显示出来
            //如果是多行的结果，则在原来的基础上加上多行的结果
            //this.Init();
            //string sql = string.Empty;
            //DataSet ds = new DataSet();
            //FS.FrameWork.Management.DataBaseManger dbMgr = new FS.FrameWork.Management.DataBaseManger();
            //for (int i = 0; i < this.neuSpread1.Sheets[0].RowCount; i++)
            //{
            //    for (int j = 0; j < this.neuSpread1.Sheets[0].ColumnCount; j++)
            //    {
            //        if (this.neuSpread1.Sheets[0].Cells[i, j].Tag != null)
            //        {
            //            sql = this.neuSpread1.Sheets[0].Cells[i, j].Tag.ToString();
            //            if (string.IsNullOrEmpty(sql))
            //            {
            //                continue;
            //            }
            //            //执行sql
            //            sql = string.Format(sql, objects);
            //            if (dbMgr.ExecQuery(sql, ref ds) < 0)
            //            {
            //                CommonController.CreateInstance().MessageBox(string.Format("查询{0}行{1}列的数据失败", i, j) + dbMgr.Err, MessageBoxIcon.Warning);
            //                return -1;
            //            }

            //            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            //            {
            //                //每行赋值
            //                this.neuSpread1.Sheets[0].RowCount += ds.Tables[0].Rows.Count-1;
            //                if (j + ds.Tables[0].Columns.Count - 1 > this.neuSpread1.Sheets[0].ColumnCount)
            //                {
            //                    this.neuSpread1.Sheets[0].ColumnCount += ds.Tables[0].Columns.Count - 1 - j;
            //                }
            //                for (int row = 0; row < ds.Tables[0].Rows.Count; row++)
            //                {
            //                    for (int col = 0; col < ds.Tables[0].Columns.Count; col++)
            //                    {
            //                        this.neuSpread1.Sheets[0].Cells[i+row, j+col].Value = ds.Tables[0].Rows[row][col];
            //                    }
            //                }
            //                i = i + ds.Tables[0].Rows.Count;
            //                j = j + ds.Tables[0].Columns.Count;
            //            }
            //        }
            //    }
            //}
            return 1;
        }

        public int Retrieve(DataTable dt)
        {
            //直接显示dt
            //每行赋值
            this.neuSpread1.Sheets[0].RowCount = 0;
            this.neuSpread1.Sheets[0].DataAutoSizeColumns = false;
            this.neuSpread1.Sheets[0].DataSource = dt.Clone();

            for (int row = 0; row < dt.Rows.Count; row++)
            {
                this.neuSpread1.Sheets[0].RowCount += row + 1;
                for (int col = 0; col < dt.Columns.Count; col++)
                {
                    this.neuSpread1.Sheets[0].Cells[row, col].Value = dt.Rows[row][col];
                }
            }

            return 1;
        }

        public int Retrieve(Dictionary<String, Object> map)
        {
            //通过参数来查询报表，循环每一个单元格查找对应的Tag，如果有则直接将查询结果显示出来
            //如果是多行的结果，则在原来的基础上加上多行的结果
            this.Init();
            //挂起
            this.SuspendLayout();

            this.SetHeaderValue(map);
            this.SetCellValue(map);

            //恢复
            this.ResumeLayout(false);

            for (int i = 0; i < this.neuSpread1.Sheets[0].RowCount; i++)
            {
                for (int j = 0; j < this.neuSpread1.Sheets[0].ColumnCount; j++)
                {
                    this.neuSpread1.Sheets[0].Cells[i, j].Locked = true;
                }
            }
            return 1;
        }

        public int Export()
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "(*.xls)|*.xls|(*.xlsx)|*.xlsx";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                bool isAddRowCount = false;
                bool isAddColumnCount = false;
                try
                {
                    this.neuSpread1.Sheets[0].ColumnCount++;
                    this.neuSpread1.Sheets[0].Columns[this.neuSpread1.Sheets[0].ColumnCount - 1].Visible = false;
                    if (this.neuSpread1.Sheets[0].ColumnHeader.RowCount == 0)
                    {
                        isAddRowCount = true;
                        this.neuSpread1.Sheets[0].ColumnHeader.RowCount = 1;
                    }

                    if (this.neuSpread1.Sheets[0].RowHeader.ColumnCount == 0)
                    {
                        isAddColumnCount = true;
                        this.neuSpread1.Sheets[0].RowHeader.ColumnCount = 1;
                    }

                    this.neuSpread1.SaveExcel(dlg.FileName);
                }
                finally
                {
                    this.neuSpread1.Sheets[0].ColumnCount--;
                    if (isAddRowCount)
                    {
                        this.neuSpread1.Sheets[0].ColumnHeader.RowCount = 0;
                    }
                    if (isAddColumnCount)
                    {
                        this.neuSpread1.Sheets[0].RowHeader.ColumnCount = 0;
                    }
                }

                //if (this.neuSpread1.Sheets[0].ColumnHeader.RowCount > 0)
                //{
                //    if (this.neuSpread1.Sheets[0].RowHeader.ColumnCount > 0)
                //    {
                //        this.neuSpread1.SaveExcel(dlg.FileName, FarPoint.Win.Spread.Model.IncludeHeaders.BothCustomOnly);
                //    }
                //    else
                //    {
                //        this.neuSpread1.SaveExcel(dlg.FileName, FarPoint.Win.Spread.Model.IncludeHeaders.ColumnHeadersCustomOnly);
                //    }
                //}
                //else if (this.neuSpread1.Sheets[0].RowHeader.ColumnCount > 0)
                //{
                //    this.neuSpread1.SaveExcel(dlg.FileName, FarPoint.Win.Spread.Model.IncludeHeaders.RowHeadersCustomOnly);
                //}
                //else
                //{
                //    this.neuSpread1.SaveExcel(dlg.FileName, FarPoint.Win.Spread.Model.IncludeHeaders.None);
                //}

                return 1;
            }
            else
            {
                return 1;
            }
        }

        public int Print()
        {
            this.neuSpread1.PrintSheet(0);
            return 1;
        }

        public int PrintPreview(bool isPreview)
        {
            return 1;
        }

        public int PaperSize
        {
            get
            {
                return 1;
            }
            set
            {

            }
        }

        public int CustomPageLength
        {
            get
            {
                return 1;
            }
            set
            {
            }
        }

        public int CustomPageWidth
        {
            get
            {
                return 1;
            }
            set
            {
            }
        }

        private string printName = string.Empty;
        public string PrintName
        {
            get
            {
                return printName;
            }
            set
            {
                printName = value;
            }
        }

        public int MarginLeft
        {
            get
            {
                return 1;
            }
            set
            {
            }
        }

        public int MarginRight
        {
            get
            {
                return 1;
            }
            set
            {
            }
        }

        public int MarginTop
        {
            get
            {
                return 1;
            }
            set
            {
            }
        }

        public int MarginBottom
        {
            get
            {
                return 1;
            }
            set
            {
            }
        }

        public void OnFilter(string filter)
        {
            if (this.neuSpread1.Sheets[0].DataSource is DataView)
            {
                ((DataView)this.neuSpread1.Sheets[0].DataSource).RowFilter = filter;
            }
        }

        public event ICommonReportController.SelectRowHanlder OnSelectRowHandler;

        public string GetItemString(int row, string columnName)
        {
            if (row - 1 >= 0)
            {
                for (int columnIndex = 0; columnIndex < this.neuSpread1.Sheets[0].ColumnCount; columnIndex++)
                {
                    if (this.neuSpread1.Sheets[0].Columns[columnIndex].Label.Equals(columnName))
                    {
                        return this.neuSpread1.Sheets[0].Cells[row - 1, columnIndex].Text;
                    }
                }
            }
            return string.Empty;
        }

        public void Reset()
        {
        }

        public int RowCount
        {
            get { return this.neuSpread1.Sheets[0].RowCount; }
        }

        #endregion
    }
}
