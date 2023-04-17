using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Office.Interop.Excel;

namespace FS.HISFC.Components.Speciment
{
    public static class SpreadToExlHelp
    {

        /// <summary>
        /// 将选中的数据导出到Excel中
        /// </summary>
        /// <param name="view">需要操作的Excel文档</param>
        /// <param name="cols">checkBox 列所在</param>
        /// <param name="noExp">不需要导出的列</param>
        /// <param name="exportHidden">是否导出隐含的列</param>
        /// <returns></returns>
        public static int ExportExl(FarPoint.Win.Spread.SheetView view, int cols, int[] noExp, string strFileName, bool exportHidden)
        {
            DataSet ds = new DataSet();
            System.Data.DataTable dt = new System.Data.DataTable();


            List<int> expCols = new List<int>();
            for (int i = 0; i < view.ColumnCount; i++)
            {
                if (i == cols) continue;
                if (!view.Columns[i].Visible && !exportHidden) continue;
                foreach (int k in noExp)
                {
                    if (k == i)
                    {
                        continue;
                    }
                }
                DataColumn dcl = new DataColumn(view.Columns[i].Label, typeof(string));
                dt.Columns.Add(dcl);
                //将需要导出的列放入列表
                expCols.Add(i);
                
            }

            for (int i = 0; i < view.RowCount; i++)
            {
                object chk = view.Cells[i, cols].Value;
                if (chk == null || chk.ToString().ToUpper() == "FALSE" || chk.ToString() == "0")
                {
                    continue;
                }
                else
                {
                    int index = 0;
                    string[] value = new string[expCols.Count];
                    for (int k = 0; k < view.ColumnCount; k++)
                    {
                        if (expCols.Contains(k))
                        {
                            value[index] = view.Cells[i, k].Text;
                            index++;
                            continue;
                        }
                        else
                        {
                            continue;
                        }
                    }

                    DataRow dr = dt.Rows.Add(value);
                    //dt.Rows.Add(dr);
                }
            }

 
            if (dt == null)
                return -1;

            ApplicationClass excel = new ApplicationClass();
            Workbook workbook = excel.Application.Workbooks.Add(true); // true for object template???

            // Add column headings...
            int iCol = 0;
            foreach (DataColumn c in dt.Columns)
            {
                iCol++;
                excel.Cells[1, iCol] = c.ColumnName;
            }
            // for each row of data...
            int iRow = 0;
            foreach (DataRow r in dt.Rows)
            {
                iRow++;

                // add each row's cell data...
                iCol = 0;
                foreach (DataColumn c in dt.Columns)
                {
                    iCol++;
                    excel.Cells[iRow + 1, iCol] = r[c.ColumnName];
                }
            }

            // Global missing reference for objects we are not defining...
            object missing = System.Reflection.Missing.Value;

            // If wanting to Save the workbook...
            workbook.SaveAs(strFileName ,
                XlFileFormat.xlWorkbookNormal, missing, missing,
                false, false, XlSaveAsAccessMode.xlNoChange,
                missing, missing, missing, missing, missing);

            // If wanting to make Excel visible and activate the worksheet...
            //excel.Visible = true;
            //Worksheet worksheet = (Worksheet)excel.ActiveSheet;
            //((_Worksheet)worksheet).Activate();

            // If wanting excel to shutdown...
            ((_Application)excel).Quit();
            
            //int rowNum = dt.Rows.Count;
            //int columnNum = dt.Columns.Count;
            //int rowIndex = 1;
            //int columnIndex = 0;

            //Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.ApplicationClass();

            //xlApp.DefaultFilePath = "";
            //xlApp.DisplayAlerts = true;
            //xlApp.SheetsInNewWorkbook = 1;
            //Workbook xlBook = xlApp.Workbooks.Add(true);
            ////将DataTable的列名导入Excel表第一行
            //foreach (DataColumn dc in dt.Columns)
            //{
            //    columnIndex++;
            //    xlApp.Cells[rowIndex, columnIndex] = dc.ColumnName;
            //}
            ////将DataTable中的数据导入Excel中
            //for (int i = 0; i < rowNum; i++)
            //{
            //    rowIndex++;
            //    columnIndex = 0;
            //    for (int j = 0; j < columnNum; j++)
            //    {
            //        columnIndex++;
            //        xlApp.Cells[rowIndex, columnIndex] = dt.Rows[i][j].ToString();
            //    }
            //}
            //try
            //{
            //    xlBook.SaveCopyAs(System.Web.HttpUtility.UrlDecode(strFileName, System.Text.Encoding.UTF8));
            //}
            //catch
            //{ }
            ////xlBook.SaveCopyAs(strFileName);
            return 1;
        }
    }
}
