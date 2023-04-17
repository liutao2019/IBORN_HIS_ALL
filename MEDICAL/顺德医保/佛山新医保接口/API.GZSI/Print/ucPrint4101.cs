using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace API.GZSI.Print
{
    public partial class ucPrint4101 : UserControl
    {
        private int rowHeight = 30;
        /// <summary>
        /// 行高
        /// </summary>
        public int RowHeight
        {
            get { return rowHeight; }
            set { rowHeight = value; }
        }

        private int printRowCount = 29;
        /// <summary>
        /// 一页打印行数
        /// </summary>
        public int PrintRowCount
        {
            get { return printRowCount; }
            set { printRowCount = value; }
        }

        private int topHeight = 135;
        /// <summary>
        /// 顶栏高度
        /// </summary>
        public int TopHeight
        {
            get { return topHeight; }
            set { topHeight = value; }
        }

        private int bottomHeight = 80;
        /// <summary>
        /// 底栏高度
        /// </summary>
        public int BottomHeight
        {
            get { return bottomHeight; }
            set { bottomHeight = value; }
        }

        public ucPrint4101()
        {
            InitializeComponent();
            InitControls();
        }

        private void InitControls()
        {
            this.RowHeight = (int)this.neuSpread1_Sheet1.Rows.Default.Height;
            this.PrintRowCount = 29;
            this.TopHeight = this.pnlTop.Height;
            this.BottomHeight = this.pnlBottom.Height;
            this.Height = printRowCount * 30 + this.topHeight + this.bottomHeight + 5;
        }

        public int SetValue()
        {
            return 1;
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <returns></returns>
        public int Print()
        {
            try
            {
                FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
                FS.HISFC.BizLogic.Manager.PageSize pgMgr = new FS.HISFC.BizLogic.Manager.PageSize();
                FS.HISFC.Models.Base.PageSize pSize = new FS.HISFC.Models.Base.PageSize("4101", 850, 1102);

                print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
                print.SetPageSize(pSize);

                string printerName = FS.HISFC.Components.Common.Classes.Function.ChoosePrinter();
                if (string.IsNullOrEmpty(printerName)) return 1;
                print.PrintDocument.PrinterSettings.PrinterName = printerName;

                //总共打印的页数
                int pageTotNum = this.neuSpread1_Sheet1.Rows.Count / this.printRowCount;
                if (this.neuSpread1_Sheet1.Rows.Count != this.printRowCount * pageTotNum)
                {
                    pageTotNum++;
                }

                int i = 0;
                while (i < pageTotNum)
                {
                    int printRow = 0;
                    for (int j = 0; j < this.neuSpread1_Sheet1.Rows.Count; j++)
                    {
                        if (j >= i * this.printRowCount && j < (i + 1) * this.PrintRowCount)
                        {
                            this.neuSpread1_Sheet1.Rows[j].Visible = true;
                            printRow++;
                        }
                        else
                        {
                            this.neuSpread1_Sheet1.Rows[j].Visible = false;
                        }
                    }

                    this.Height = this.TopHeight + this.BottomHeight + this.RowHeight * printRow + 5;
                    this.lblPageIndex.Text = string.Format("第{0}页/共{1}页", (i + 1).ToString(), pageTotNum.ToString());

                    if (i == 0)
                    {
                        pnlTopLine.Visible = false;
                    }
                    else
                    {
                        pnlTopLine.Visible = true;
                    }

                    if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
                    {
                        print.PrintPreview(0, 10, this);
                    }
                    else
                    {
                        print.PrintPage(0, 10, this);
                    }

                    i++;
                }
            }
            catch
            {
                MessageBox.Show("打印发生错误,请确认是否有连接好打印机");
                return -1;
            }
            return 1;
        }
    }
}
