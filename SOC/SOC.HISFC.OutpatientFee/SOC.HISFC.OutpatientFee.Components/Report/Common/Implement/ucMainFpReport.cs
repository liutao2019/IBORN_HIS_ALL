using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FS.SOC.HISFC.BizProcess.CommonInterface;
using FS.SOC.HISFC.OutpatientFee.Components.Report.Common.Interface;
using FarPoint.Win.Spread;
using FS.SOC.HISFC.OutpatientFee.Components.Report.Common.Setting;
using FS.SOC.HISFC.OutpatientFee.Components.Report.Common.Enum;

namespace FS.SOC.HISFC.OutpatientFee.Components.Report.Common.Implement
{
    public partial class ucMainFpReport : FS.FrameWork.WinForms.Controls.ucBaseControl, IMainReportForm
    {
        public ucMainFpReport()
        {
            InitializeComponent();

            this.neuSpread1.SelectionChanged += new FarPoint.Win.Spread.SelectionChangedEventHandler(neuSpread1_SelectionChanged);
            this.neuSpread1.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(neuSpread1_CellDoubleClick);
            this.neuSpread1.CellClick += new CellClickEventHandler(neuSpread1_CellClick);
        }

        #region 变量

        private string dataWindowObject = string.Empty;
        private string libraryList = string.Empty;
        FS.FrameWork.Management.DataBaseManger dbMgr = new FS.FrameWork.Management.DataBaseManger();
        private int currentRow = 0;
        private ReportQueryInfo reportQueryInfo = null;

        #endregion

        #region 方法

        private void SetFormat(int copyCount)
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

            if (copyCount > 0)
            {
                for (int i = 0; i < copyCount; i++)
                {
                    SheetView sheet = this.neuSpread1.Sheets[0].Clone();

                    this.neuSpread1.Sheets.Add(sheet);
                }
            }

            InputMap im = this.neuSpread1.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Escape, Keys.None), FarPoint.Win.Spread.SpreadActions.None);
        }

        private void SetHeaderValue(Dictionary<String, Object> map, SheetView sheet)
        {
            string cellTag = string.Empty;
            for (int i = 0; i < sheet.ColumnHeader.RowCount; i++)
            {
                int rownum = 0;
                for (int j = 0; j < sheet.ColumnHeader.Columns.Count; j++)
                {
                    if (sheet.ColumnHeader.Cells[i, j].Text != null)
                    {
                        cellTag = sheet.ColumnHeader.Cells[i, j].Text;
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
                                        sheet.ColumnHeader.Rows.Add(i, dt.Rows.Count - 1);
                                    }
                                    if (j + dt.Columns.Count > sheet.ColumnHeader.Columns.Count)
                                    {
                                        sheet.ColumnHeader.Columns.Count += dt.Columns.Count - (sheet.ColumnHeader.Columns.Count - j);
                                    }
                                    for (int row = 0; row < dt.Rows.Count; row++)
                                    {
                                        for (int col = 0; col < dt.Columns.Count; col++)
                                        {
                                            sheet.ColumnHeader.Cells[i + row, j + col].Value = dt.Rows[row][col];
                                        }
                                    }

                                    rownum += dt.Rows.Count - 1;
                                    if (dt.Columns.Count > 0)
                                    {
                                        j = j + dt.Columns.Count - 1;
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
                                    if (j + dr.ItemArray.Length > sheet.ColumnHeader.Columns.Count)
                                    {
                                        sheet.ColumnHeader.Columns.Count += dr.ItemArray.Length + j - sheet.ColumnHeader.Columns.Count;
                                    }
                                    for (int col = 0; col < dr.ItemArray.Length; col++)
                                    {
                                        sheet.ColumnHeader.Cells[i, j + col].Value = dr.ItemArray[col];
                                    }
                                    if (dr.ItemArray.Length > 0)
                                    {
                                        j = j + dr.ItemArray.Length - 1;
                                    }
                                }
                                #endregion
                            }
                            else
                            {
                                #region 数据源赋值
                                sheet.ColumnHeader.Cells[i, j].Value = map[cellTag];
                                #endregion
                            }
                        }
                        //else
                        //{
                        //    cellTag = Function.ReplaceValues(cellTag, map);
                        //    sheet.ColumnHeader.Cells[i, j].Value = cellTag;
                        //}

                    }
                }
                i = i + rownum;
            }

            //{AB4140A5-54B5-4dae-9FE4-E70B4A50A27D}
            #region 多表头时进行表头合并修改

            bool canSpan = true;
            for (int i = 0; i < sheet.ColumnHeader.Columns.Count; i++)
            {
                for (int j = 0; j < sheet.ColumnHeader.RowCount; j++)
                {
                    if (string.IsNullOrEmpty(sheet.ColumnHeader.Cells[j, i].Text))
                    {
                        canSpan = false;
                    }
                }
            }

            if (sheet.ColumnHeader.RowCount == 2 && canSpan)
            {
                for (int i = 0; i < sheet.ColumnHeader.Columns.Count; i++)
                {
                    string text = sheet.ColumnHeader.Cells[1, i].Text.Replace(sheet.ColumnHeader.Cells[0, i].Text, "");
                    sheet.ColumnHeader.Cells[1, i].Text = string.IsNullOrEmpty(text) ? sheet.ColumnHeader.Cells[1, i].Text : text;
                    if (i < sheet.ColumnHeader.Columns.Count - 1)
                    {
                        if (sheet.ColumnHeader.Cells[0, i].Text == sheet.ColumnHeader.Cells[1, i].Text)
                        {
                            sheet.ColumnHeader.Cells[0, i].RowSpan = 2;
                        }
                        else
                        {
                            int k = 1;
                            for (int j = i + 1; j < sheet.ColumnHeader.Columns.Count; j++)
                            {
                                if (sheet.ColumnHeader.Cells[0, i].Text == sheet.ColumnHeader.Cells[0, j].Text)
                                {
                                    string text1 = sheet.ColumnHeader.Cells[1, j].Text.Replace(sheet.ColumnHeader.Cells[0, j].Text, "");
                                    sheet.ColumnHeader.Cells[1, j].Text = string.IsNullOrEmpty(text) ? sheet.ColumnHeader.Cells[1, j].Text : text1;
                                    k += 1;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            sheet.ColumnHeader.Cells[0, i].ColumnSpan = k;
                            i += k - 1;
                        }
                    }
                }
            }

            #endregion
        }

        private void SetCellValue(Dictionary<String, Object> map, SheetView sheet, int beginRow, int endRow)
        {
            string cellTag = string.Empty;
            string cellFormula = string.Empty;

            for (int i = beginRow; i < endRow; i++)
            {
                int rownum = 0;
                int addRowNum = 0;
                for (int j = 0; j < sheet.ColumnCount; j++)
                {
                    #region cellTag
                    if (sheet.Cells[i, j].Tag != null)
                    {
                        cellTag = sheet.Cells[i, j].Tag.ToString();
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
                                    sheet.Rows.Add(i, ds.Tables[0].Rows.Count - 1);
                                }
                                if (j + ds.Tables[0].Columns.Count > sheet.ColumnCount)
                                {
                                    sheet.ColumnCount += ds.Tables[0].Columns.Count + j - sheet.ColumnCount;
                                }
                                for (int row = 0; row < ds.Tables[0].Rows.Count; row++)
                                {
                                    for (int col = 0; col < ds.Tables[0].Columns.Count; col++)
                                    {
                                        sheet.Cells[i + row, j + col].Value = ds.Tables[0].Rows[row][col];
                                    }
                                }

                                rownum += ds.Tables[0].Rows.Count - 1;

                                if (ds.Tables[0].Columns.Count > 0)
                                {
                                    j = j + ds.Tables[0].Columns.Count - 1;
                                }
                            }

                            #endregion
                        }
                        else
                        {
                            if (map.ContainsKey(cellTag))
                            {
                                if (map[cellTag] is KeyValuePair<DataTable, QueryDataSource>)
                                {
                                    #region 分组组数据源
                                    KeyValuePair<DataTable, QueryDataSource> keyValue = (KeyValuePair<DataTable, QueryDataSource>)map[cellTag];
                                    #endregion

                                    #region 数据源赋值
                                    DataTable dt = keyValue.Key;

                                    //判断交叉报表是否有对应的数据源，将每一个数值给予对应的数据源

                                    Object[,] crossDataSource = null;
                                    if (map.ContainsKey(cellTag + "AddMapSourceData"))
                                    {
                                        crossDataSource = map[cellTag + "AddMapSourceData"] as Object[,];
                                    }

                                    if (dt.Rows.Count > 0)
                                    {
                                        //计算数据
                                        cellFormula = sheet.Cells[i, j].Formula;
                                        if (string.IsNullOrEmpty(cellFormula) == false)
                                        {
                                            sheet.Cells[i, j].Formula = "";
                                            sheet.Cells[i, j].Value = dt.Compute(cellFormula, "");
                                        }
                                        else
                                        {
                                            if (dt.Rows.Count - 1 > addRowNum)
                                            {
                                                addRowNum = dt.Rows.Count - 1;
                                                sheet.Rows.Add(i + 1, dt.Rows.Count - 1);
                                            }

                                            if (j + dt.Columns.Count > sheet.ColumnCount)
                                            {
                                                sheet.ColumnCount += dt.Columns.Count - (sheet.ColumnCount - j);
                                            }

                                            for (int row = 0; row < dt.Rows.Count; row++)
                                            {
                                                sheet.Rows[i + row].Height = sheet.Rows[i].Height;
                                                int fpColumnIndex = 0;
                                                for (int col = 0; col < dt.Columns.Count; col++)
                                                {
                                                    sheet.Cells[i + row, j + fpColumnIndex].Value = dt.Rows[row][col];
                                                    if (crossDataSource != null)
                                                    {
                                                        sheet.Cells[i + row, j + fpColumnIndex].Tag = crossDataSource[row, col];
                                                    }

                                                    sheet.Cells[i + row, j + fpColumnIndex].CellType = sheet.Cells[i, j + fpColumnIndex].CellType;
                                                    sheet.Cells[i + row, j + fpColumnIndex].VerticalAlignment = sheet.Cells[i, j + fpColumnIndex].VerticalAlignment;
                                                    sheet.Cells[i + row, j + fpColumnIndex].HorizontalAlignment = sheet.Cells[i, j + fpColumnIndex].HorizontalAlignment;
                                                    sheet.Cells[i + row, j + fpColumnIndex].Font = sheet.Cells[i, j + fpColumnIndex].Font;
                                                    sheet.Cells[i + row, j + fpColumnIndex].ColumnSpan = sheet.Cells[i, j + fpColumnIndex].ColumnSpan;
                                                    sheet.Cells[i + row, j + fpColumnIndex].Border = sheet.Cells[i, j + fpColumnIndex].Border;
                                                    fpColumnIndex += sheet.Cells[i + row, j + fpColumnIndex].ColumnSpan;

                                                    if (fpColumnIndex < col)
                                                    {
                                                        fpColumnIndex = col;
                                                    }
                                                }
                                            }

                                            rownum += dt.Rows.Count - 1;
                                        }
                                    }
                                    #endregion

                                    #region 行分组设置

                                    Dictionary<int, DataRow> dictionaryGroups = this.RowGroup(dt, keyValue.Value);

                                    if (dictionaryGroups.Count > 0)
                                    {
                                        //临时处理，以后在整理 jing.zhao
                                        FarPoint.Win.LineBorder lineBorder = new FarPoint.Win.LineBorder(Color.Black, 1, false, true, false, true);
                                        //保留数据格式
                                        foreach (KeyValuePair<int, DataRow> keyValueGroup in dictionaryGroups)
                                        {
                                            //插入数据，在指定的位置
                                            int groupRow = i + keyValueGroup.Key;
                                            sheet.Rows.Add(groupRow, 1);
                                            if (keyValue.Value.RowGroup.Count > 0)
                                            {
                                                if (keyValue.Value.RowGroup[0].IsUseTableLine)
                                                {
                                                    sheet.Rows[groupRow].Border = lineBorder;
                                                }
                                                sheet.Rows[groupRow].Font = keyValue.Value.RowGroup[0].GroupFont;
                                            }

                                            int fpColumnIndex = 0;
                                            int firstRow = i;
                                            if (i == groupRow)
                                            {
                                                firstRow = i + 1;
                                            }



                                            for (int col = 0; col < dt.Columns.Count; col++)
                                            {
                                                sheet.Cells[groupRow, j + fpColumnIndex].Value = keyValueGroup.Value[col];
                                                sheet.Cells[groupRow, j + fpColumnIndex].CellType = sheet.Cells[firstRow, j + fpColumnIndex].CellType;
                                                sheet.Cells[groupRow, j + fpColumnIndex].VerticalAlignment = sheet.Cells[firstRow, j + fpColumnIndex].VerticalAlignment;
                                                sheet.Cells[groupRow, j + fpColumnIndex].HorizontalAlignment = sheet.Cells[firstRow, j + fpColumnIndex].HorizontalAlignment;
                                                sheet.Cells[groupRow, j + fpColumnIndex].Font = sheet.Cells[firstRow, j + fpColumnIndex].Font;
                                                sheet.Cells[groupRow, j + fpColumnIndex].ColumnSpan = sheet.Cells[firstRow, j + fpColumnIndex].ColumnSpan;
                                                sheet.Cells[groupRow, j + fpColumnIndex].Border = sheet.Cells[firstRow, j + fpColumnIndex].Border;
                                                fpColumnIndex += sheet.Cells[groupRow, j + fpColumnIndex].ColumnSpan;

                                                if (fpColumnIndex < col)
                                                {
                                                    fpColumnIndex = col;
                                                }
                                            }

                                            rownum += 1;
                                        }
                                    }

                                    #endregion
                                    //扩展列宽

                                    if (dt.Columns.Count > 0)
                                    {
                                        j = j + dt.Columns.Count - 1;
                                    }

                                }
                                else if (map[cellTag] is DataTable)
                                {
                                    #region 数据源赋值
                                    DataTable dt = map[cellTag] as DataTable;

                                    //判断交叉报表是否有对应的数据源，将每一个数值给予对应的数据源

                                    Object[,] crossDataSource = null;
                                    if (map.ContainsKey(cellTag + "AddMapSourceData"))
                                    {
                                        crossDataSource = map[cellTag + "AddMapSourceData"] as Object[,];
                                    }

                                    if (dt.Rows.Count > 0)
                                    {
                                        //计算数据
                                        cellFormula = sheet.Cells[i, j].Formula;
                                        if (string.IsNullOrEmpty(cellFormula) == false)
                                        {
                                            sheet.Cells[i, j].Formula = "";
                                            sheet.Cells[i, j].Value = dt.Compute(cellFormula, "");
                                        }
                                        else
                                        {
                                            if (dt.Rows.Count - 1 > addRowNum)
                                            {
                                                addRowNum = dt.Rows.Count - 1;
                                                sheet.Rows.Add(i + 1, dt.Rows.Count - 1);
                                            }
                                            if (j + dt.Columns.Count > sheet.ColumnCount)
                                            {
                                                sheet.ColumnCount += dt.Columns.Count - (sheet.ColumnCount - j);
                                            }
                                            for (int row = 0; row < dt.Rows.Count; row++)
                                            {
                                                sheet.Rows[i + row].Height = sheet.Rows[i].Height;
                                                int fpColumnIndex = 0;
                                                for (int col = 0; col < dt.Columns.Count; col++)
                                                {
                                                    sheet.Cells[i + row, j + fpColumnIndex].Value = dt.Rows[row][col];
                                                    if (crossDataSource != null)
                                                    {
                                                        sheet.Cells[i + row, j + fpColumnIndex].Tag = crossDataSource[row, col];
                                                    }
                                                    sheet.Cells[i + row, j + fpColumnIndex].Locked = sheet.Cells[i, j + fpColumnIndex].Locked;
                                                    sheet.Cells[i + row, j + fpColumnIndex].CellType = sheet.Cells[i, j + fpColumnIndex].CellType;
                                                    sheet.Cells[i + row, j + fpColumnIndex].VerticalAlignment = sheet.Cells[i, j + fpColumnIndex].VerticalAlignment;
                                                    sheet.Cells[i + row, j + fpColumnIndex].HorizontalAlignment = sheet.Cells[i, j + fpColumnIndex].HorizontalAlignment;
                                                    sheet.Cells[i + row, j + fpColumnIndex].Font = sheet.Cells[i, j + fpColumnIndex].Font;
                                                    sheet.Cells[i + row, j + fpColumnIndex].Border = sheet.Cells[i, j + fpColumnIndex].Border;
                                                    sheet.Cells[i + row, j + fpColumnIndex].ColumnSpan = sheet.Cells[i, j + fpColumnIndex].ColumnSpan;
                                                    fpColumnIndex += sheet.Cells[i + row, j + fpColumnIndex].ColumnSpan;

                                                    if (fpColumnIndex < col)
                                                    {
                                                        fpColumnIndex = col;
                                                    }
                                                }
                                            }

                                            rownum += dt.Rows.Count - 1;

                                            if (dt.Columns.Count > 0)
                                            {
                                                j = j + dt.Columns.Count - 1;
                                            }
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
                                        if (j + dr.ItemArray.Length > sheet.ColumnCount)
                                        {
                                            sheet.ColumnCount += dr.ItemArray.Length + j - sheet.ColumnCount;
                                        }

                                        int fpColumnIndex = 0;
                                        for (int col = 0; col < dr.ItemArray.Length; col++)
                                        {
                                            sheet.Cells[i, j + fpColumnIndex].Value = dr.ItemArray[col];
                                            fpColumnIndex += sheet.Cells[i, j + fpColumnIndex].ColumnSpan;
                                            if (fpColumnIndex < col)
                                            {
                                                fpColumnIndex = col;
                                            }
                                        }

                                        if (dr.ItemArray.Length > 0)
                                        {
                                            j = j + dr.ItemArray.Length - 1;
                                        }
                                    }
                                    #endregion
                                }
                                else
                                {
                                    #region 数据源赋值
                                    sheet.Cells[i, j].Value = map[cellTag];
                                    sheet.Cells[i, j].Formula = cellFormula;
                                    #endregion
                                }
                            }
                            else
                            {
                                //执行sql
                                if (Function.HasReplaceableValues(cellTag))
                                {
                                    cellTag = Function.ReplaceValues(cellTag, map);
                                    sheet.Cells[i, j].Value = cellTag;
                                    sheet.Cells[i, j].Formula = cellFormula;
                                }
                            }
                        }
                    }
                    #endregion cellTag
                }

                i = i + rownum;
                endRow = endRow + rownum;
            }


        }

        /// <summary>
        /// 行分组和列合计
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="common"></param>
        private Dictionary<int, DataRow> RowGroup(DataTable dt, QueryDataSource common)
        {
            Dictionary<int, DataRow> rowGroup = new Dictionary<int, DataRow>();

            if (common.RowGroup != null && common.RowGroup.Count > 0)
            {
                string[] columnGroups = common.RowGroup[0].GroupCondition;

                #region 添加小计

                string sampleValues = string.Empty;
                string selectSampleValues = "1=1";
                int i = 0;
                int row = 0;
                while (i < dt.Rows.Count)
                {
                    DataRow dr = dt.Rows[i];

                    string currentSampleValues = string.Empty;
                    string currentSelectSampleValues = "1=1";
                    foreach (string crossGroupColumn in columnGroups)
                    {
                        currentSampleValues += dr[crossGroupColumn].ToString();
                        currentSelectSampleValues += " AND " + crossGroupColumn + " = '" + dr[crossGroupColumn].ToString() + "'";
                    }
                    if (string.Empty.Equals(sampleValues))
                    {
                        sampleValues = currentSampleValues;
                        selectSampleValues = currentSelectSampleValues;
                    }

                    if (!sampleValues.Equals(currentSampleValues))
                    {
                        DataRow drSumRow = dt.NewRow();

                        string value = string.Empty;
                        foreach (string crossGroupColumn in columnGroups)
                        {
                            value += dt.Rows[i - 1][crossGroupColumn].ToString();
                        }

                        this.GetGroupShowInfoAndLocation(drSumRow, common.RowGroup[0], value, "小计：");

                        foreach (DataColumn dc in dt.Columns)
                        {
                            if (dc.DataType.IsValueType)
                            {
                                string columnName = "[" + dc.ColumnName + "]";
                                drSumRow[dc.ColumnName] = dt.Compute("sum(" + columnName + ")", selectSampleValues);
                            }
                        }

                        if (common.RowGroup[0].GroupShowLocation == EnumGroupShowLocation.Footer)
                        {
                            rowGroup.Add(i + rowGroup.Count, drSumRow);
                        }
                        else if (common.RowGroup[0].GroupShowLocation == EnumGroupShowLocation.Header)
                        {
                            rowGroup.Add(row + rowGroup.Count, drSumRow);
                        }

                        row = i;

                        sampleValues = currentSampleValues;
                        selectSampleValues = currentSelectSampleValues;
                    }
                    i++;
                }

                if (dt.Rows.Count > 0)
                {
                    DataRow drValues = dt.Rows[dt.Rows.Count - 1];
                    if (columnGroups.Length > 0)
                    {
                        //添加合计
                        dt.AcceptChanges();
                        DataRow drSumRow = dt.NewRow();


                        string value = string.Empty;
                        foreach (string crossGroupColumn in columnGroups)
                        {
                            value += drValues[crossGroupColumn].ToString();
                        }

                        this.GetGroupShowInfoAndLocation(drSumRow, common.RowGroup[0], value, "小计：");

                        foreach (DataColumn dc in dt.Columns)
                        {
                            if (dc.DataType.IsValueType && dc.DataType == typeof(decimal))
                            {
                                string columnName = "[" + dc.ColumnName + "]";
                                drSumRow[dc.ColumnName] = dt.Compute("sum(" + columnName + ")", selectSampleValues);
                            }
                        }
                        if (common.RowGroup[0].GroupShowLocation == EnumGroupShowLocation.Footer)
                        {
                            rowGroup.Add(dt.Rows.Count + rowGroup.Count, drSumRow);
                        }
                        else if (common.RowGroup[0].GroupShowLocation == EnumGroupShowLocation.Header)
                        {
                            rowGroup.Add(row + rowGroup.Count, drSumRow);
                        }

                    }
                }

                #endregion

                if (common.IsSumRow)
                {
                    #region 组合计

                    //添加列合计
                    DataRow drNewRow = dt.NewRow();

                    if (string.IsNullOrEmpty(common.SumColumns) == false
                      && dt.Columns.Contains(common.SumColumns))
                    {
                        drNewRow[common.SumColumns] = "合计：";
                    }
                    else
                    {
                        drNewRow[0] = "合计：";
                    }
                    foreach (DataColumn dc in dt.Columns)
                    {
                        if (dc.DataType.IsValueType)
                        {
                            string columnName = "[" + dc.ColumnName + "]";
                            drNewRow[dc.ColumnName] = dt.Compute("sum(" + columnName + ")", "");
                        }
                    }

                    rowGroup.Add(dt.Rows.Count + rowGroup.Count, drNewRow);

                    #endregion
                }
            }
            else//纯合计
            {
                if (common.IsSumRow)
                {
                    //添加合计列
                    if (string.IsNullOrEmpty(common.SumColumns) == false
                        && dt.Columns.Contains(common.SumColumns))
                    {
                        #region 普通报表添加合计
                        DataRow drNewRow = dt.NewRow();
                        drNewRow[common.SumColumns] = "合计：";
                        foreach (DataColumn dc in dt.Columns)
                        {
                            if (dc.DataType.IsValueType && dc.DataType == typeof(decimal))
                            {
                                drNewRow[dc.ColumnName] = dt.Compute("sum(" + dc.ColumnName + ")", "");
                            }
                        }
                        rowGroup.Add(dt.Rows.Count + rowGroup.Count, drNewRow);

                        #endregion
                    }
                }
            }

            return rowGroup;
        }

        /// <summary>
        /// 获取组显示的位置
        /// </summary>
        /// <param name="common"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private void GetGroupShowInfoAndLocation(DataRow dr, RowGroupInfo rowGroup, string value, string defaulValue)
        {
            string finalValue = value;
            switch (rowGroup.ShowInfoType)
            {
                case EnumGroupShowInfoType.Custom:
                    finalValue = rowGroup.CustomShowInfo;
                    break;
                case EnumGroupShowInfoType.CustomAndGroupColumn:
                    finalValue = rowGroup.CustomShowInfo + value;
                    break;
                case EnumGroupShowInfoType.GroupColumn:
                    finalValue = value;
                    break;
                case EnumGroupShowInfoType.GroupColumnAndCustom:
                    finalValue = value + rowGroup.CustomShowInfo;
                    break;
            }
            if (string.IsNullOrEmpty(finalValue))
            {
                finalValue = defaulValue;
            }

            string[] groupDependColumns = rowGroup.GroupDependColumn.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string groupDependColumn in groupDependColumns)
            {
                if (dr.Table.Columns.Contains(groupDependColumn))
                {
                    dr[groupDependColumn] = finalValue;
                }
                else
                {
                    dr[rowGroup.GroupCondition[0]] = finalValue;
                }

            }

        }

        private void SetSheetValue(Dictionary<String, Object> map, SheetView sheet, int beginRow, int endRow)
        {
            //通过参数来查询报表，循环每一个单元格查找对应的Tag，如果有则直接将查询结果显示出来
            //如果是多行的结果，则在原来的基础上加上多行的结果
            //挂起
            this.SuspendLayout();
            this.SetHeaderValue(map, sheet);
            this.SetCellValue(map, sheet, beginRow, endRow);
            sheet.PrintInfo.Header = Function.ReplaceValues(sheet.PrintInfo.Header, map);
            sheet.PrintInfo.Footer = Function.ReplaceValues(sheet.PrintInfo.Footer, map);
            //替换表名称
            sheet.SheetName = Function.ReplaceValues(sheet.SheetName, map);
            //锁定数据
            sheet.Rows[0, sheet.RowCount - 1].Locked = true;

            this.neuSpread1.SetMaximumCellOverflowWidth(100);
            //恢复
            this.ResumeLayout(false);
        }

        #endregion

        #region 事件

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        void neuSpread1_SelectionChanged(object sender, FarPoint.Win.Spread.SelectionChangedEventArgs e)
        {
            if (this.neuSpread1.ActiveSheet.ActiveRowIndex != currentRow)
            {
                currentRow = this.neuSpread1.ActiveSheet.ActiveRowIndex;
                if (this.OnSelectRowHandler != null)
                {
                    this.OnSelectRowHandler(this.neuSpread1.ActiveSheet.ActiveRowIndex + 1);
                }
            }
        }

        void neuSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            //取数据源
            if (this.neuSpread1.ActiveSheet.Cells[e.Row, e.Column].Tag is DataRow[])
            {
                if (this.OnDoubleCellClickHandler != null)
                {
                    this.OnDoubleCellClickHandler(this.neuSpread1.ActiveSheet.Cells[e.Row, e.Column].Tag);
                }
            }
        }

        void neuSpread1_CellClick(object sender, CellClickEventArgs e)
        {
            //取数据源
            if (this.neuSpread1.ActiveSheet.Cells[e.Row, e.Column].Tag is DataRow[])
            {
                if (this.OnCellClickHandler != null)
                {
                    this.OnCellClickHandler(this.neuSpread1.ActiveSheet.Cells[e.Row, e.Column].Tag);
                }
            }
        }

        #endregion

        #region IMainReportForm 成员

        public int Init(ReportQueryInfo reportQueryInfo)
        {
            this.neuSpread1.Sheets[0].RowCount = 0;
            this.SetFormat(0);

            this.reportQueryInfo = reportQueryInfo;
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
            //清空所有表格
            this.neuSpread1.Sheets.Clear();

            if (objects != null)
            {
                if (this.reportQueryInfo != null && this.reportQueryInfo.TableGroup.GroupByPage == false)
                {
                    this.SetFormat(0);
                    //固定前面的数据不变化
                    int rowNum = this.neuSpread1.Sheets[0].RowCount;
                    int columnNum = this.neuSpread1.Sheets[0].ColumnCount;
                    int beginRow = rowNum;

                    for (int i = 0; i < objects.Length; i++)
                    {
                        this.neuSpread1.Sheets[0].AddRows(beginRow, rowNum);
                        //行高设置
                        for (int num = 0; num < rowNum; num++)
                        {
                            this.neuSpread1.Sheets[0].Rows[beginRow + num].Height = this.neuSpread1.Sheets[0].Rows[num].Height;
                        }

                        this.neuSpread1.Sheets[0].CopyRange(0, 0, beginRow, 0, rowNum, columnNum, false);
                        //清空所有列表
                        this.SetSheetValue((Dictionary<String, Object>)(objects[i]), this.neuSpread1.Sheets[0],
                            beginRow, this.neuSpread1.Sheets[0].RowCount);
                        beginRow += this.neuSpread1.Sheets[0].RowCount - beginRow;
                    }

                    //删除固定数据
                    this.neuSpread1.Sheets[0].RemoveRows(0, rowNum);
                }
                else
                {
                    //用来处理报表分组的
                    this.SetFormat(objects.Length - 1);

                    for (int i = 0; i < objects.Length; i++)
                    {
                        //清空所有列表
                        this.SetSheetValue((Dictionary<String, Object>)(objects[i]), this.neuSpread1.Sheets[i], 0, this.neuSpread1.Sheets[i].RowCount - 1);
                    }
                }
            }
            return 1;
        }

        public int Retrieve(DataTable dt)
        {
            this.SetFormat(0);
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
            //清空所有表格
            this.neuSpread1.Sheets.Clear();
            this.SetFormat(0);
            this.SetSheetValue(map, this.neuSpread1.Sheets[0], 0, this.neuSpread1.Sheets[0].RowCount);
            return 1;
        }

        public int Export()
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.FileName = this.neuSpread1.Sheets[0].SheetName;
            if (dlg.FileName.ToLower().Contains("sheet1"))
            {
                dlg.FileName = this.ParentForm.Text;
            }
            dlg.Filter = "(*.xls)|*.xls|(*.xlsx)|*.xlsx|(*.txt)|*.txt|(*.dbf)|*.dbf|(用友凭证)|*.txt";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                bool isAddRowHeaderCount = false;
                bool isAddColumnHeaderCount = false;

                bool isAddBeginCount = false;
                bool isAddEndCount = false;
                //var filterItems = ((System.Windows.Forms.FileDialog)(dlg)).FilterItems;

                string fileNameExt = dlg.FileName.Substring(dlg.FileName.LastIndexOf("\\") + 1); //获取文件名，不带路径                
                if (dlg.FileName.Contains(".xls"))
                {
                    //this.neuSpread1.Sheets[0].ColumnCount++;
                    //this.neuSpread1.Sheets[0].Columns[this.neuSpread1.Sheets[0].ColumnCount - 1].Visible = false;
                    //if (this.neuSpread1.Sheets[0].ColumnHeader.RowCount == 0)
                    //{
                    //    isAddRowHeaderCount = true;
                    //    this.neuSpread1.Sheets[0].ColumnHeader.RowCount = 1;
                    //}

                    //this.neuSpread1.Sheets[0].AddColumns(0, 1);
                    //isAddBeginCount = true;
                    //this.neuSpread1.Sheets[0].Columns[0].Visible = false;
                    //this.neuSpread1.Sheets[0].AddColumns(this.neuSpread1.Sheets[0].ColumnCount, 1);
                    //isAddEndCount = true;
                    //this.neuSpread1.Sheets[0].Columns[this.neuSpread1.Sheets[0].ColumnCount - 1].Visible = false;

                    //if (this.neuSpread1.Sheets[0].RowHeader.ColumnCount == 0)
                    //{
                    //    isAddColumnHeaderCount = true;
                    //    this.neuSpread1.Sheets[0].RowHeader.ColumnCount = 1;
                    //}

                    try
                    {
                        this.neuSpread1.SaveExcel(dlg.FileName, FarPoint.Win.Spread.Model.IncludeHeaders.BothCustomOnly);
                    }
                    catch
                    {
                        this.neuSpread1.SaveExcel(dlg.FileName, FarPoint.Excel.ExcelSaveFlags.DataOnly);
                    }
                    finally
                    {
                        //this.neuSpread1.Sheets[0].ColumnCount--;
                        //if (isAddRowHeaderCount)
                        //{
                        //    this.neuSpread1.Sheets[0].ColumnHeader.RowCount = 0;
                        //}
                        //if (isAddColumnHeaderCount)
                        //{
                        //    this.neuSpread1.Sheets[0].RowHeader.ColumnCount = 0;
                        //}


                        //if (isAddBeginCount)
                        //{
                        //    this.neuSpread1.Sheets[0].RemoveColumns(0, 1);
                        //}
                        //if (isAddEndCount)
                        //{
                        //    this.neuSpread1.Sheets[0].RemoveColumns(this.neuSpread1.Sheets[0].ColumnCount, 1);
                        //}
                    }

                }
                else if (dlg.FileName.Contains(".txt") && dlg.FilterIndex != 5)
                {
                    this.neuSpread1.Sheets[0].SaveTextFile(dlg.FileName, true, FarPoint.Win.Spread.Model.IncludeHeaders.None, "\r\n", string.Empty, string.Empty);
                }
                else if (dlg.FilterIndex == 5)
                {   //用友凭证格式
                    this.neuSpread1.Sheets[0].SaveTextFileRange(0, 0, neuSpread1.Sheets[0].RowCount, neuSpread1.Sheets[0].ColumnCount, dlg.FileName, false, FarPoint.Win.Spread.Model.IncludeHeaders.None, "\r\n", ",", ",", System.Text.Encoding.Default);
                }
                else if (dlg.FileName.Contains(".dbf"))
                {
                    if (this.neuSpread1.Sheets[0].RowCount <= 0)
                    {
                        return 0;
                    }
                    //必须删除文件，因为是创建数据库表的形式创建的文件，所以文件覆盖形式是不可靠的
                    if (System.IO.File.Exists(dlg.FileName))
                    {
                        System.IO.File.Delete(dlg.FileName);
                    }
                    // this.neuSpread1.Sheets[0].SaveTextFile(dlg.FileName, true, FarPoint.Win.Spread.Model.IncludeHeaders.None, "\r\n", string.Empty, string.Empty);
                    //this.neuSpread1.SaveExcel(dlg.FileName, FarPoint.Excel.ExcelSaveFlags.DataOnly);

                    //利用odbc创建dbf数据库文件
                    System.Data.Odbc.OdbcConnection conn = new System.Data.Odbc.OdbcConnection();

                    //用户选择的路径作为数据库
                    string path = dlg.FileName.Substring(0, dlg.FileName.LastIndexOf('\\'));

                    //纯粹的文件名用作数据库表名称
                    string tableName = dlg.FileName.Replace(path + "\\", "").Replace(".dbf", "");

                    //建表的字段名称，包含字段数据类型，如果有Sheet列头，取列头的名称，否则取第一行的数据作为字段名称
                    string createfields = "";

                    //插入表数据需要的字段名称，不包含数据类型
                    string fieldNames = "";
                    if (this.neuSpread1.Sheets[0].ColumnHeader.Visible && this.neuSpread1.Sheets[0].ColumnHeader.Rows.Count == 1)
                    {
                        for (int colIndex = 0; colIndex < this.neuSpread1.Sheets[0].ColumnCount; colIndex++)
                        {
                            //数据类型暂时写死了char
                            string headerName = this.neuSpread1.Sheets[0].ColumnHeader.Cells[0, colIndex].Text;
                            if (string.IsNullOrEmpty(headerName))
                            {
                                headerName = this.neuSpread1.Sheets[0].Columns[colIndex].Label;
                            }
                            if (!string.IsNullOrEmpty(headerName))
                            {
                                createfields += headerName + " char(100),";
                                fieldNames += headerName + ",";
                            }
                        }
                    }
                    else
                    {
                        for (int colIndex1 = 0; colIndex1 < this.neuSpread1.Sheets[0].ColumnCount; colIndex1++)
                        {
                            //数据类型暂时写死了char
                            createfields += this.neuSpread1.Sheets[0].Cells[0, colIndex1].Text + " char(100),";
                            fieldNames += this.neuSpread1.Sheets[0].Cells[0, colIndex1].Text + ",";
                        }

                    }
                    createfields = createfields.TrimEnd(',');
                    fieldNames = fieldNames.TrimEnd(',');

                    string cmdCreateTableText = @"Create Table "
                       + tableName
                       + " (" + createfields + ")";
                    //数据库连接
                    string connectStr = @"Driver={Microsoft dBASE Driver (*.dbf)};Driverid=277;Dbq=" + path;
                    conn.ConnectionString = connectStr;
                    conn.Open();

                    //建表
                    System.Data.Odbc.OdbcCommand cmd = new System.Data.Odbc.OdbcCommand(cmdCreateTableText, conn);
                    cmd.ExecuteNonQuery();

                    //根据sheet的值插入表
                    int rowIndex = 1;
                    if (this.neuSpread1.Sheets[0].ColumnHeader.Visible && this.neuSpread1.Sheets[0].ColumnHeader.Rows.Count == 1)
                    {
                        rowIndex = 0;
                    }

                    for (; rowIndex < this.neuSpread1.Sheets[0].RowCount; rowIndex++)
                    {
                        string fieldValues = "";
                        for (int colIndex2 = 0; colIndex2 < this.neuSpread1.Sheets[0].ColumnCount; colIndex2++)
                        {
                            fieldValues += "'" + this.neuSpread1.Sheets[0].Cells[rowIndex, colIndex2].Text + "',";

                        }

                        fieldValues = fieldValues.TrimEnd(',');

                        string cmdInsertValueText = @"insert into "
                                                    + tableName
                                                    + " (" + fieldNames + ")"
                                                    + " values "
                                                    + "(" + fieldValues + ")";

                        cmd = new System.Data.Odbc.OdbcCommand(cmdInsertValueText, conn);
                        cmd.ExecuteNonQuery();
                    }

                    conn.Close();

                    //以下防止8-3短文件格式截取了文件名称
                    if (tableName.Length > 8 && System.IO.File.Exists(path + "\\" + tableName.Substring(0, 8) + ".dbf"))
                    {
                        System.IO.File.Copy(path + "\\" + tableName.Substring(0, 8) + ".dbf", dlg.FileName);
                        System.IO.File.Delete(path + "\\" + tableName.Substring(0, 8) + ".dbf");
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

        public int Print(FS.SOC.HISFC.OutpatientFee.Components.Report.Common.Setting.PrintInfo p)
        {
            if (p != null)
            {
                this.neuSpread1.Sheets[this.neuSpread1.ActiveSheetIndex].PrintInfo.PaperSize = p.PaperSize;
                if (p.SelectPage)
                {
                    int maxCount = this.neuSpread1.GetPrintPageCount(this.neuSpread1.ActiveSheetIndex);
                    PrintPageSelectDialog printPageSelectDialog = new PrintPageSelectDialog();
                    printPageSelectDialog.MaxPageNO = maxCount;
                    printPageSelectDialog.ShowDialog(this);
                    if (printPageSelectDialog.ToPageNO == 0)
                    {
                        return -1;
                    }

                    if (printPageSelectDialog.ToPageNO > 0)
                    {
                        this.neuSpread1.Sheets[this.neuSpread1.ActiveSheetIndex].PrintInfo.PageStart = printPageSelectDialog.FromPageNO;
                        this.neuSpread1.Sheets[this.neuSpread1.ActiveSheetIndex].PrintInfo.PageEnd = printPageSelectDialog.ToPageNO;
                        this.neuSpread1.Sheets[this.neuSpread1.ActiveSheetIndex].PrintInfo.PrintType = FarPoint.Win.Spread.PrintType.PageRange;
                    }
                }
            }
            this.neuSpread1.PrintSheet(this.neuSpread1.ActiveSheetIndex);
            return 1;
        }

        public int PrintPreview(bool isPreview, FS.SOC.HISFC.OutpatientFee.Components.Report.Common.Setting.PrintInfo p)
        {
            this.neuSpread1.Sheets[this.neuSpread1.ActiveSheetIndex].PrintInfo.Preview = isPreview;
            if (p != null)
            {
                this.neuSpread1.Sheets[this.neuSpread1.ActiveSheetIndex].PrintInfo.PaperSize = p.PaperSize;
            }
            this.neuSpread1.PrintSheet(this.neuSpread1.ActiveSheetIndex);
            return 1;
        }

        public int PrintAll(FS.SOC.HISFC.OutpatientFee.Components.Report.Common.Setting.PrintInfo p)
        {
            for (int i = 0; i < this.neuSpread1.Sheets.Count; i++)
            {
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在打印" + this.neuSpread1.Sheets[i].SheetName);
                Application.DoEvents();
                if (p != null)
                {
                    this.neuSpread1.Sheets[i].PrintInfo.PaperSize = p.PaperSize;
                }
                this.neuSpread1.PrintSheet(this.neuSpread1.Sheets[i]);
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }
            return 1;
        }

        public void OnFilter(string filter)
        {
            if (this.neuSpread1.Sheets[0].DataSource is DataView)
            {
                ((DataView)this.neuSpread1.Sheets[0].DataSource).RowFilter = filter;
            }
        }

        public event ICommonReportController.SelectRowHanlder OnSelectRowHandler;

        public event ICommonReportController.SelectCellHanlder OnCellClickHandler;

        public event ICommonReportController.SelectCellHanlder OnDoubleCellClickHandler;

        public DataRow[] GetCellDataRow()
        {
            if (this.neuSpread1.Sheets[0].ActiveRowIndex < 0 && this.neuSpread1.Sheets[0].ActiveColumnIndex < 0)
            {
                return null;
            }
            //取数据源
            if (this.neuSpread1.Sheets[0].Cells[this.neuSpread1.Sheets[0].ActiveRowIndex, this.neuSpread1.Sheets[0].ActiveColumnIndex].Tag is DataRow[])
            {
                return this.neuSpread1.Sheets[0].Cells[this.neuSpread1.Sheets[0].ActiveRowIndex, this.neuSpread1.Sheets[0].ActiveColumnIndex].Tag as DataRow[];
            }

            return null;
        }

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

        #endregion
    }
}
