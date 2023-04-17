using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.HISFC.Components.Report.DataWindow
{
    public partial class ucMainReportForm : FS.FrameWork.WinForms.Controls.ucBaseControl,ICommonReportController.IMainReportForm
    {
        public ucMainReportForm()
        {
            InitializeComponent();
            this.dwMain.RowFocusChanged += new Sybase.DataWindow.RowFocusChangedEventHandler(this.dwMain_RowFocusChanged);
        }

        #region 域变量
        /// <summary>
        /// 是否支持排序
        /// </summary>
        protected bool isSort = true;

        protected string sortColumn = string.Empty;

        /// <summary>
        /// 升序排序
        /// </summary>
        protected string sortType = "A";

        private string dataWindowObject = string.Empty;
        private string libraryList = string.Empty;

        #endregion

        #region 私有方法

        /// <summary>
        /// 取消掉其他列的排序符号
        /// </summary>
        /// <param name="dwControl">当前数据窗口</param>
        private void DeleleSortFlag(NeuDataWindow dwControl)
        {
            string columnName = string.Empty;
            
            for (int i = 1; i < dwControl.ColumnCount + 1; i++)
            {
                try
                {
                    columnName = dwControl.Describe('#' + i.ToString() + ".name") + "_t";
                    dwControl.Modify(columnName + ".text = '" + this.dwMain.Describe(columnName + ".text").Replace("↑", string.Empty) + "'");
                    dwControl.Modify(columnName + ".text = '" + this.dwMain.Describe(columnName + ".text").Replace("↓", string.Empty) + "'");
                }
                catch { }
            }
        }

        /// <summary>
        /// 排序的方法
        /// </summary>
        /// <param name="dwControl">当前数据窗</param>
        /// <param name="currColumn">当前列</param>
        /// <param name="sortType">排序类型</param>
        /// <returns>成功 true 失败 false</returns>
        private bool DataWindowSort(NeuDataWindow dwControl, string currColumn, string currColumnName, string sortType)
        {
            try
            {
                //排序  
                dwControl.SetSort(currColumn + " " + sortType);
                dwControl.Sort();
                //dwControl.Dv.Sort = currColumnName + " " + (sortType == "A" ? "ASC" : "DESC");
                //创建升序的箭头图形

                DeleleSortFlag(dwControl);

                switch (sortType)
                {
                    case "A":
                        dwControl.Modify(currColumn + "_t" + ".text = '" + this.dwMain.Describe(currColumn + "_t" + ".text") + "↑'");
                        break;
                    case "D":
                        dwControl.Modify(currColumn + "_t" + ".text = '" + this.dwMain.Describe(currColumn + "_t" + ".text") + "↓'");

                        break;
                }

                return true;
            }
            catch
            {
                return false;
            }

            finally
            {

            }
        }
        
        #endregion

        #region 共有方法

        /// <summary>
        /// 排序 成功 1 失败 -1
        /// </summary>
        /// <returns></returns>
        protected int OnSort()
        {
            try
            {
                this.dwMain.RowFocusChanged-=new Sybase.DataWindow.RowFocusChangedEventHandler(dwMain_RowFocusChanged);
                if (this.isSort)
                {
                    string ls_CurObj = "";

                    int ll_CurRowNumber = 0;
                    ls_CurObj = this.dwMain.ObjectUnderMouse.Gob.Name; //得出objectName
                    ll_CurRowNumber = this.dwMain.ObjectUnderMouse.RowNumber; //得出当前Row

                    if (this.dwMain.Describe(ls_CurObj + ".Band") == "header")
                    {
                        if (ll_CurRowNumber == 0 & this.dwMain.Describe(ls_CurObj + ".Text") != "!")
                        {
                            sortColumn = ls_CurObj.Substring(0, ls_CurObj.Length - 2);

                            string sortColumnName =this.dwMain.Describe(sortColumn+".dbname");
                            //{A652EF19-B5B2-4148-AAB1-774C2D3AE1B2}
                            if (sortType == "A")
                            {
                                if (DataWindowSort(this.dwMain, sortColumn,sortColumnName, sortType))
                                {
                                    sortType = "D";
                                }
                                else
                                {
                                    return -1;
                                }
                            }
                            else
                            {
                                if (DataWindowSort(this.dwMain, sortColumn, sortColumnName,sortType))
                                {
                                    sortType = "A";
                                }
                                else
                                {
                                    return -1;
                                }
                            }
                            //{A652EF19-B5B2-4148-AAB1-774C2D3AE1B2}
                            //this.lastSortedColumnName = ls_CurObj;
                        }
                    }
                }
            }
            catch
            {
                return -1;
            }

            finally
            {
                this.dwMain.RowFocusChanged += new Sybase.DataWindow.RowFocusChangedEventHandler(dwMain_RowFocusChanged);
            }

            return 1;
        }

        #endregion

        #region 事件

        private void dwMain_Click(object sender, EventArgs e)
        {
            if (this.dwMain != null)
            {
                this.OnSort();
            }
        }

        private void dwMain_RowFocusChanged(object sender, Sybase.DataWindow.RowFocusChangedEventArgs e)
        {
            int currRow = e.RowNumber;
            this.dwMain.SelectRow(0, false);
            this.dwMain.SelectRow(currRow, true);

            if (this.OnSelectRowHandler != null)
            {
                this.OnSelectRowHandler(currRow);
            }
        }
        #endregion

        #region IMainReportForm 成员

        public int Init()
        {
            this.dwMain.Dispose();
            this.dwMain = new NeuDataWindow();
            this.dwMain.DataWindowObject = "";
            this.dwMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dwMain.LibraryList = "";
            this.dwMain.Location = new System.Drawing.Point(0, 0);
            this.dwMain.Name = "dwMain";
            this.dwMain.ScrollBars = Sybase.DataWindow.DataWindowScrollBars.Both;
            this.dwMain.Size = new System.Drawing.Size(544, 276);
            this.dwMain.TabIndex = 0;
            this.dwMain.Text = "neuDataWindow1";
            this.dwMain.Click += new System.EventHandler(this.dwMain_Click);
            this.Controls.Add(this.dwMain);
            return 1;
        }

        public string DataWindowObject
        {
            get
            {
                return dataWindowObject;
            }
            set
            {
                dataWindowObject = value;
                if (this.dwMain != null)
                {
                    this.dwMain.DataWindowObject = value;
                }
            }
        }

        public string LibraryList
        {
            get
            {
                return libraryList;
            }
            set
            {
                libraryList = value;
                if (this.dwMain != null)
                {
                    this.dwMain.LibraryList = value;
                }
            }
        }

        public int Retrieve(params object[] objects)
        {
            if (dwMain != null)
            {
                this.dwMain.RowFocusChanged -= this.dwMain_RowFocusChanged;
                int i = dwMain.Retrieve(objects);
                //this.neuDataWindow1.SetFullState(dwMain.GetFullState());
                this.dwMain.RowFocusChanged += this.dwMain_RowFocusChanged;
                return i;
            }

            return 1;
        }

        public int Retrieve(DataTable dt)
        {
            if (dwMain != null)
            {
                this.dwMain.RowFocusChanged -= this.dwMain_RowFocusChanged;
                int i= dwMain.Retrieve(dt);
                this.dwMain.RowFocusChanged += this.dwMain_RowFocusChanged;
                return i;
            }

            return 1;
        }

        public int Retrieve(Dictionary<String, Object> map)
        {
            return 1;
        }

        public int Export()
        {
            if (dwMain == null)
            {
                return -1;
            }

            this.DeleleSortFlag(dwMain);

            System.Windows.Forms.SaveFileDialog dd = new SaveFileDialog();
            dd.Filter = "txt files (*.xls)|*.xls";
            if (dd.ShowDialog() == DialogResult.Cancel)
            {
                return 1;
            }
            //dwMain.SaveAs(dd.FileName, Sybase.DataWindow.FileSaveAsType.Excel, true);
            dwMain.SaveAsFormattedText(dd.FileName);
            return 1;
        }

        public int Print()
        {
            if (this.dwMain != null)
            {
                try
                {
                    this.DeleleSortFlag(dwMain);
                    this.dwMain.PrintProperties.Preview = false;
                    //this.dwMain.Print(true,true);
                    if (this.paperSize > 0)
                    {
                        dwMain.Modify("DataWindow.Print.Paper.Size=" + this.paperSize);
                        if (paperSize == 255 || paperSize == 256)
                        {
                            this.dwMain.Modify(string.Format("DataWindow.Print.CustomPage.Length={0}", customPageLength));
                            this.dwMain.Modify(string.Format("DataWindow.Print.CustomPage.Width={0}", customPageWidth));
                        }
                        this.dwMain.Modify(string.Format("DataWindow.Print.PrinterName={0}", printName));
                        this.dwMain.Modify(string.Format("DataWindow.Print.Margin.Bottom={0}", marginBottom));
                        this.dwMain.Modify(string.Format("DataWindow.Print.Margin.Top={0}", marginTop));
                        this.dwMain.Modify(string.Format("DataWindow.Print.Margin.Left={0}", marginLeft));
                        this.dwMain.Modify(string.Format("DataWindow.Print.Margin.Right={0}", marginRight));
                    }

                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    return -1;
                }
                finally
                {
                    this.dwMain.Print();
                }
            }

            return 1;
        }

        public void OnFilter(string filter)
        {
            if (dwMain != null )
            {
                this.dwMain.RowFocusChanged -= new Sybase.DataWindow.RowFocusChangedEventHandler(dwMain_RowFocusChanged);
                dwMain.SetFilter(filter);
                dwMain.Filter();
                //dwMain.Dv.RowFilter = filter;
                this.dwMain.RowFocusChanged += new Sybase.DataWindow.RowFocusChangedEventHandler(dwMain_RowFocusChanged);
            }
        }

        public event ICommonReportController.SelectRowHanlder OnSelectRowHandler;

        public string GetItemString(int row, string columnName)
        {
            if (dwMain != null)
            {
                return dwMain.GetItemString(row, columnName);
            }
            return "";
        }

        public void Reset()
        {
            if(dwMain!=null)
            {
                dwMain.Reset();
            }
        }

        public int RowCount
        {
            get
            {
                if (dwMain != null)
                {
                    return dwMain.RowCount;
                }

                return 0;
            }

        }

        public int PrintPreview(bool isPreview)
        {
            if (this.dwMain != null)
            {
                try
                {
                    this.DeleleSortFlag(dwMain);
                    this.dwMain.PrintProperties.Preview = isPreview;
                    //this.dwMain.Print(true, true);
                }
                catch
                {
                    return -1;
                }
            }

            return 1;
        }

        private int paperSize = -1;
        public int PaperSize
        {
            get
            {
                return paperSize;
            }
            set
            {
                this.paperSize = value;
            }
        }

        private int customPageLength = 1169;
        public int CustomPageLength
        {
            get
            {
                return customPageLength;
            }
            set
            {
                this.customPageLength = value;
            }
        }

        private int customPageWidth = 827;
        public int CustomPageWidth
        {
            get
            {
                return customPageWidth;
            }
            set
            {
                this.customPageWidth = value;
            }
        }

        private string printName = "";
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

        private int marginLeft = 20;
        public int MarginLeft
        {
            get
            {
                return marginLeft;
            }
            set
            {
                marginLeft = value;
            }
        }

        private int marginRight = 0;
        public int MarginRight
        {
            get
            {
                return marginRight;
            }
            set
            {
                marginRight = value;
            }
        }

        private int marginTop = 10;
        public int MarginTop
        {
            get
            {
                return marginTop;
            }
            set
            {
                marginTop = value;
            }
        }

        private int marginBottom = 0;
        public int MarginBottom
        {
            get
            {
                return marginBottom;
            }
            set
            {
                marginBottom = value;
            }
        }

        #endregion
    }
}
