using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace FS.SOC.Windows.Forms
{
    /// <summary>
    /// Print<br></br>
    /// [功能描述: 打印基类-抽象类]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2012-02]<br></br>
    /// 大多数激光和喷墨打印机不支持无边打印。
    /// 它们必须在页面边缘留有一定的未打印区域，以防止墨粉进入移动纸张的打印机部件。
    /// 对于不支持无边打印的打印机，页面的成像区域小于页面尺寸。
    /// <修改记录
    ///		修改人=''
    ///		修改时间='yyyy-mm-dd'
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    public abstract class PrintBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public PrintBase()
        {
            this.PrintDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(PrintDocument_PrintPage);
        }

        #region 变量
        /// <summary>
        /// 页面区域控件类别
        /// </summary>
        protected enum DrawingRegionType
        {
            Header,
            Data,
            Footer,
            Null
        }

        /// <summary>
        /// 记录当前绘制的页面区域控件类别
        /// </summary>
        protected DrawingRegionType curDrawingRegionType = DrawingRegionType.Null;

        /// <summary>
        /// 当前打印页的页码
        /// 程序自动计算的
        /// </summary>
        protected int curPageNO = 1;

        /// <summary>
        /// 本次打印最大页码
        /// 程序自动计算的
        /// </summary>
        protected int maxPageNO = 1;

        /// <summary>
        /// 打印时间，多页时保证每一页的时间相同
        /// </summary>
        protected DateTime printTime = new DateTime();

        /// <summary>
        /// 打印主控件
        /// </summary>
        protected Control printControl = null;

        /// <summary>
        /// 打印用
        /// </summary>
        protected System.Drawing.Printing.PrintDocument PrintDocument = new System.Drawing.Printing.PrintDocument();

        /// <summary>
        /// 打印页面选择
        /// </summary>
        protected SOC.Windows.Forms.PrintPageSelectDialog socPrintPageSelectDialog = new FS.SOC.Windows.Forms.PrintPageSelectDialog();

        /// <summary>
        /// FarPoint的打印区域
        /// </summary>
        protected Rectangle farPointRectangle = new Rectangle(0, 0, 0, 0);

        protected int[] farPointRowBreaks;

        /// <summary>
        /// 页面数据绘制完成后提供外部接口
        /// </summary>
        /// <param name="curPageNO">当前页码</param>
        /// <param name="maxPageNO">最大页码</param>
        public delegate void DrawPageHandler(Graphics graphics, int curPageNO, int maxPageNO, int curPageFarPointLastRowIndex);

        /// <summary>
        /// 页面数据绘制开始时提供外部接口
        /// </summary>
        public DrawPageHandler DrawPageStartEvent;

        /// <summary>
        /// 页面数据绘制完成后提供外部接口
        /// </summary>
        public DrawPageHandler DrawPageEndEvent;

        #endregion

        #region 属性及其变量
        /// <summary>
        /// 打印是否需要预览
        /// </summary>
        private bool isNeedPreView = false;

        /// <summary>
        /// 纸张设置
        /// </summary>
        private System.Drawing.Printing.PaperSize paperSize = new System.Drawing.Printing.PaperSize("Letter", 850, 1100);

        /// <summary>
        /// 纸张设置
        /// </summary>
        [System.ComponentModel.DisplayName("纸张设置")]
        [System.ComponentModel.Description("纸张设置")]
        public System.Drawing.Printing.PaperSize PaperSize
        {
            get { return paperSize; }
            set { paperSize = value; }
        }

        /// <summary>
        /// 打印是否需要预览
        /// </summary>
        [System.ComponentModel.DisplayName("打印是否需要预览")]
        [System.ComponentModel.Description("打印是否需要预览，默认不需要")]
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
        /// 打印纸张
        /// </summary>
        [System.ComponentModel.DisplayName("打印纸张")]
        [System.ComponentModel.Description("打印纸张名称")]
        public string PaperName
        {
            get { return paperSize.PaperName; }
            set { paperSize.PaperName = value; }
        }

        /// <summary>
        /// 获取打印纸张类型
        /// </summary>
        [System.ComponentModel.DisplayName("获取打印纸张类型")]
        [System.ComponentModel.Description("获取打印纸张类型")]
        public System.Drawing.Printing.PaperKind PaperKind
        {
            get { return paperSize.Kind; }
            //set { paperSize.Kind = value; }
        }

        /// <summary>
        /// 纸张宽度
        /// </summary>
        [System.ComponentModel.DisplayName("纸张宽度")]
        [System.ComponentModel.Description("纸张宽度，像素")]
        public int PaperWidth
        {
            get { return paperSize.Width; }
            set { paperSize.Width = value; }
        }

        /// <summary>
        /// 纸张高度
        /// </summary>
        [System.ComponentModel.DisplayName("纸张高度")]
        [System.ComponentModel.Description("纸张高度，像素")]
        public int PaperHeight
        {
            get { return paperSize.Height; }
            set { paperSize.Height = value; }
        }

        /// <summary>
        /// 打印机名称
        /// </summary>
        private string printerName = "";

        /// <summary>
        /// 打印机名称
        /// </summary>
        [System.ComponentModel.DisplayName("打印机名称")]
        [System.ComponentModel.Description("打印机名称，使用默认打印不需赋值")]
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
        [System.ComponentModel.DisplayName("横向打印")]
        [System.ComponentModel.Description("横向打印，纸张竖放")]
        public bool IsLandscape
        {
            get { return isLandscape; }
            set { isLandscape = value; }
        }

        /// <summary>
        /// 页边距
        /// </summary>
        private System.Drawing.Printing.Margins drawingMargins = new System.Drawing.Printing.Margins(0, 0, 0, 0);

        /// <summary>
        /// 页边距
        /// 每页可绘制打印内容的范围等于给定的pageWidth乘以pageHeight减去页边距
        /// </summary>
        [System.ComponentModel.DisplayName("页边距")]
        [System.ComponentModel.Description("页边距，默认值0, 0, 0, 0")]
        public System.Drawing.Printing.Margins DrawingMargins
        {
            get { return drawingMargins; }
            set { drawingMargins = value; }
        }

        /// <summary>
        /// 页上边距
        /// </summary>
        [System.ComponentModel.DisplayName("页上边距")]
        [System.ComponentModel.Description("页上边距，默认值0")]
        public int DrawingMarginTop
        {
            get { return drawingMargins.Top; }
            set { drawingMargins.Top = value; }
        }

        /// <summary>
        /// 页下边距
        /// </summary>
        [System.ComponentModel.DisplayName("页下边距")]
        [System.ComponentModel.Description("页下边距，默认值0")]
        public int DrawingMarginBottom
        {
            get { return drawingMargins.Bottom; }
            set { drawingMargins.Bottom = value; }
        }

        /// <summary>
        /// 页左边距
        /// </summary>
        [System.ComponentModel.DisplayName("页左边距")]
        [System.ComponentModel.Description("页左边距，默认值0")]
        public int DrawingMarginLeft
        {
            get { return drawingMargins.Left; }
            set { drawingMargins.Left = value; }
        }

        /// <summary>
        /// 页右边距
        /// </summary>
        [System.ComponentModel.DisplayName("页上边距")]
        [System.ComponentModel.Description("页上边距，默认值0")]
        public int DrawingMarginRight
        {
            get { return drawingMargins.Right; }
            set { drawingMargins.Right = value; }
        }


        /// <summary>
        /// 是否显示边框
        /// </summary>
        private bool isShowFarPointBorder = false;

        /// <summary>
        /// 是否显示FarPoint边框
        /// </summary>
        [System.ComponentModel.DisplayName("是否显示FarPoint边框")]
        [System.ComponentModel.Description("是否显示FarPoint边框")]
        public bool IsShowFarPointBorder
        {
            get { return isShowFarPointBorder; }
            set { isShowFarPointBorder = value; }
        }

        /// <summary>
        /// 页眉控件
        /// </summary>
        private Control headerControl = null;

        /// <summary>
        /// 页眉控件
        /// </summary>
        [System.ComponentModel.DisplayName("页眉控件")]
        [System.ComponentModel.Description("页眉控件")]
        public Control HeaderControl
        {
            get { return headerControl; }
            set { headerControl = value; }
        }

        /// <summary>
        /// 页脚控件
        /// </summary>
        private Control footerControl = null;

        /// <summary>
        /// 页脚控件
        /// </summary>
        [System.ComponentModel.DisplayName("页脚控件")]
        [System.ComponentModel.Description("页脚控件")]
        public Control FooterControl
        {
            get { return footerControl; }
            set { footerControl = value; }
        }

        /// <summary>
        /// 页码控件
        /// </summary>
        private Control pageNOControl = null;

        /// <summary>
        /// 页码控件
        /// </summary>
        [System.ComponentModel.DisplayName("页码控件")]
        [System.ComponentModel.Description("页码控件")]
        public Control PageNOControl
        {
            get { return pageNOControl; }
            set { pageNOControl = value; }
        }

        /// <summary>
        /// 打印时间控件
        /// </summary>
        private Control printTimeControl = null;

        /// <summary>
        /// 打印时间控件
        /// </summary>
        [System.ComponentModel.DisplayName("打印时间控件")]
        [System.ComponentModel.Description("打印时间控件")]
        public Control PrintTimeControl
        {
            get { return printTimeControl; }
            set { printTimeControl = value; }
        }

        /// <summary>
        /// 是否自定填充Farpoint，FarPoint的数据将自动扩充到页眉和页脚的中间区域
        /// </summary>
        private bool isAutoFillFarPoint = true;

        /// <summary>
        /// 是否自定填充FarPoint，FarPoint的数据将自动扩充到页眉和页脚的中间区域
        /// </summary>
        [System.ComponentModel.DisplayName("是否自定填充FarPoint")]
        [System.ComponentModel.Description("是否自定填充FarPoint，FarPoint的数据将自动扩充到页眉和页脚的中间区域")]
        public bool IsAutoFillFarPoint
        {
            get { return isAutoFillFarPoint; }
            set { isAutoFillFarPoint = value; }
        }

        /// <summary>
        /// 是否显示页面范围选择控件
        /// </summary>
        private bool isShowPageNOChooseDialog = true;

        /// <summary>
        /// 是否显示页面范围选择控件
        /// </summary>
        [System.ComponentModel.DisplayName("是否显示页面范围选择控件")]
        [System.ComponentModel.Description("是否显示页面范围选择控件，默认值true")]
        public bool IsShowPageNOChooseDialog
        {
            get { return isShowPageNOChooseDialog; }
            set { isShowPageNOChooseDialog = value; }
        }

        #endregion

        #region 预览
        /// <summary>
        /// 预览
        /// </summary>
        protected virtual void PrintView()
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

        #endregion

        #region 打印主函数
        /// <summary>
        /// 申明打印主函数，在子类中实现
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected abstract void PrintDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e);
        
        /// <summary>
        /// 申明页码选择函数，在子类中实现
        /// </summary>
        /// <param name="graphics"></param>
        /// <returns></returns>
        protected abstract bool ChoosePrintPageNO(Graphics graphics);

        /// <summary>
        /// 绘制控件
        /// </summary>
        /// <param name="control">控件</param>
        /// <param name="locationX">相对于纸张的X坐标</param>
        /// <param name="LocationY">相对于纸张的Y坐标</param>
        protected void DrawingControl(Control control, int locationX, int locationY, Graphics graphics)
        {
            if (control == this.PageNOControl)
            {
                string tmp = "";
                if (control.Tag != null)
                {
                    tmp = control.Tag.ToString();
                }
                if (tmp.Contains("{0}"))
                {
                    tmp = string.Format(tmp, this.curPageNO.ToString(), this.maxPageNO.ToString());
                }
                else
                {
                    tmp = tmp + this.curPageNO.ToString() + "/" + this.maxPageNO.ToString();
                }
                if (control is Label)
                {
                    Label l = (Label)control;
                    //对于居中的文本放在页的中间
                    if (l.Parent != null && l.Width == this.printControl.Width && (l.TextAlign == ContentAlignment.BottomCenter || l.TextAlign == ContentAlignment.MiddleCenter || l.TextAlign == ContentAlignment.TopCenter))
                    {
                        Label tmpLabel = new Label();
                        tmpLabel.AutoSize = true;
                        tmpLabel.Text = tmp;
                        locationX = this.DrawingMargins.Left + (this.GetPaperWidth() - this.DrawingMargins.Left - this.DrawingMargins.Right - tmpLabel.PreferredWidth) / 2;
                    }
                }
                graphics.DrawString(tmp, control.Font, new SolidBrush(control.ForeColor), locationX, locationY);
            }
            else if (control == this.PrintTimeControl)
            {
                string tmp = "";
                if (control.Tag != null)
                {
                    tmp = control.Tag.ToString();
                }
                if (tmp.Contains("{0}"))
                {
                    tmp = string.Format(tmp, this.printTime.ToString());
                }
                else
                {
                    tmp = tmp + this.printTime.ToString();
                }
                if (control is Label)
                {
                    Label l = (Label)control;
                    //对于居中的文本放在页的中间
                    if (l.Parent != null && l.Width == this.printControl.Width && (l.TextAlign == ContentAlignment.BottomCenter || l.TextAlign == ContentAlignment.MiddleCenter || l.TextAlign == ContentAlignment.TopCenter))
                    {
                        Label tmpLabel = new Label();
                        tmpLabel.AutoSize = true;
                        tmpLabel.Text = tmp;
                        locationX = this.DrawingMargins.Left + (this.GetPaperWidth() - this.DrawingMargins.Left - this.DrawingMargins.Right - tmpLabel.PreferredWidth) / 2;
                    }
                }
                graphics.DrawString(tmp, control.Font, new SolidBrush(control.ForeColor), locationX, locationY);
            }
            else if (control is Panel && control.BackColor != Color.White && control.BackColor != Color.Transparent && control.BackColor != System.Drawing.SystemColors.Control)
            {
                graphics.FillRectangle(new SolidBrush(control.BackColor), (float)locationX, (float)locationY, (float)control.Width, (float)control.Height);
            }
            else if (control is Label)
            {
                Label l = (Label)control;
                //对于居中的文本放在页的中间
                if (l.Parent != null && l.Width == this.printControl.Width && (l.TextAlign == ContentAlignment.BottomCenter || l.TextAlign == ContentAlignment.MiddleCenter || l.TextAlign == ContentAlignment.TopCenter))
                {
                    Label tmpLabel = new Label();
                    tmpLabel.AutoSize = true;
                    tmpLabel.Text = l.Text;
                    locationX = this.DrawingMargins.Left + (this.GetPaperWidth() - this.DrawingMargins.Left - this.DrawingMargins.Right - tmpLabel.PreferredWidth) / 2;
                }

                //对于label不是自动更改大小的，按照控件zise打印内容
                if (((Label)control).AutoSize)
                {
                    graphics.DrawString(control.Text, control.Font, new SolidBrush(control.ForeColor), locationX, locationY);
                }
                else
                {
                    graphics.DrawString(control.Text, control.Font, new SolidBrush(control.ForeColor), new RectangleF(locationX, locationY, control.Size.Width, control.Size.Height));
                }
            }
            else if (control is ComboBox || control is TextBox)
            {
                //graphics.DrawString(control.Text, control.Font,new SolidBrush(control.ForeColor),new RectangleF(locationX, locationY,control.Size.Width,control.Size.Height));
                graphics.DrawString(control.Text, control.Font, new SolidBrush(control.ForeColor), locationX, locationY);
            }
            else if (control is DateTimePicker)
            {
                graphics.DrawString(((DateTimePicker)control).Value.ToString(), control.Font, new SolidBrush(control.ForeColor), locationX, locationY);
            }
            else if (control is CheckBox)
            {
                if (((CheckBox)control).Checked)
                {
                    graphics.DrawImage((Image)Resource.CheckMarkChecked, locationX, locationY, control.Height - 2, control.Height - 2);
                }
                else
                {
                    graphics.DrawImage((Image)Resource.CheckMarkUnchecked, locationX, locationY, control.Height - 2, control.Height - 2);
                }
                graphics.DrawString(control.Text, control.Font, new SolidBrush(control.ForeColor), locationX + control.Height, locationY);
            }
            else if (control is RadioButton)
            {
                if (((RadioButton)control).Checked)
                {
                    graphics.DrawImage((Image)Resource.RadioMarkChecked, locationX, locationY, control.Height - 2, control.Height - 2);
                }
                else
                {
                    graphics.DrawImage((Image)Resource.RadioMarkUnchecked, locationX, locationY, control.Height-2, control.Height-2);
                }
                graphics.DrawString(control.Text, control.Font, new SolidBrush(control.ForeColor), locationX + control.Height, locationY);
            }
            else if (control is PictureBox)
            {
                if (((PictureBox)control).Image != null)
                {
                    graphics.DrawImage(((PictureBox)control).Image, locationX, locationY, control.Width, control.Height);
                }
            }
        }

        /// <summary>
        /// 获取纸张宽度
        /// </summary>
        /// <returns></returns>
        protected int GetPaperWidth()
        {
            //处理横向打印
            int paperWidth = this.PaperWidth;

            //横向打印只需要将打印纸张的页高赋值给绘制区域的宽，打印纸张的宽赋值给绘制区域的高
            if (this.IsLandscape)
            {
                paperWidth= this.PaperHeight;
            }

            return paperWidth;
        }

        /// <summary>
        /// 获取纸张高度
        /// </summary>
        /// <returns></returns>
        protected int GetPaperHeight()
        {
            //处理横向打印
            int paperHeight = this.PaperHeight;

            //横向打印只需要将打印纸张的页高赋值给绘制区域的宽，打印纸张的宽赋值给绘制区域的高
            if (this.IsLandscape)
            {
                paperHeight= this.PaperWidth;
            }

            return paperHeight;
        }


        #endregion

        #region 公开函数

        /// <summary>
        /// 打印纸张设置
        /// </summary>
        /// <param name="paperSize"></param>
        public virtual void SetPaperSize(System.Drawing.Printing.PaperSize paperSize)
        {
            if (paperSize == null)
            {
                paperSize = new System.Drawing.Printing.PaperSize("Letter", 850, 1100);
            }
            if (!string.IsNullOrEmpty(this.PrinterName))
            {
                this.PrintDocument.DefaultPageSettings.PrinterSettings.PrinterName = this.PrinterName;
            }

            this.paperSize = paperSize;

            this.PrintDocument.DefaultPageSettings.PaperSize = this.paperSize;
            this.PrintDocument.PrinterSettings.DefaultPageSettings.PaperSize = this.paperSize;
            this.PrintDocument.DefaultPageSettings.Landscape = this.IsLandscape;

        }

        /// <summary>
        /// 打印数据
        /// </summary>
        /// <param name="control">打印控件</param>
        /// <param name="paperName">纸张名称</param>
        /// <param name="paperWidth">纸张宽度，像素</param>
        /// <param name="paperHeight">纸张高度，像素</param>
        /// <param name="printerName">打印机名称</param>
        /// <returns></returns>
        public virtual int PrintPage(Control control)
        {
            if (control == null)
            {
                throw new Exception("打印的控件为null");
            }
            if (this.PrintDocument.DefaultPageSettings.PaperSize != this.paperSize)
            {
                this.SetPaperSize(this.paperSize);
            }

            this.PrintDocument.DefaultPageSettings.Landscape = this.IsLandscape;

            int headerHeight = 0;
            int footerHeight = 0;
            if (this.HeaderControl != null)
            {
                headerHeight = this.HeaderControl.Height;
            }
            if (this.FooterControl != null)
            {
                footerHeight = this.FooterControl.Height;
            }
            if (headerHeight + footerHeight + this.DrawingMargins.Top + this.DrawingMargins.Bottom > this.GetPaperHeight())
            {
                throw new Exception("页面页脚高度已经超过纸张高度！");
            }

            this.curPageNO = 1;
            this.maxPageNO = 1;

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在打印...");
            Application.DoEvents();

            this.printControl = control;
            printTime = DateTime.Now;

            if (this.IsNeedPreView)
            {

                if (this.ChoosePrintPageNO(Graphics.FromHwnd(FS.FrameWork.WinForms.Classes.Function.WaitForm.Handle)))
                {
                    this.PrintView();
                }
            }
            else
            {
                if (this.ChoosePrintPageNO(Graphics.FromHwnd(FS.FrameWork.WinForms.Classes.Function.WaitForm.Handle)))
                {
                    try
                    {
                        this.PrintDocument.Print();
                    }
                    catch (System.Drawing.Printing.InvalidPrinterException ex)
                    {
                        MessageBox.Show("无效的打印机！\r\n" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return -1;
                    }
                }
            }

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            return 0;
        }

        /// <summary>
        /// 打印数据
        /// </summary>
        /// <param name="control">打印控件</param>
        /// <param name="paperName">纸张名称</param>
        /// <param name="paperWidth">纸张宽度，像素</param>
        /// <param name="paperHeight">纸张高度，像素</param>
        /// <param name="printerName">打印机名称</param>
        /// <returns></returns>
        public virtual int PrintPageView(Control control)
        {
            bool isCurNeedPreView = this.isNeedPreView;
            this.IsNeedPreView = true;
            int param = this.PrintPage(control);
            this.IsNeedPreView = isCurNeedPreView;

            return param;
        }

        #endregion

    }
}
