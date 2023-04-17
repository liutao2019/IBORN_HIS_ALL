using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;

namespace FS.SOC.Windows.Forms
{
    /// <summary>
    /// Print<br></br>
    /// [功能描述: 打印类-页眉、页脚分别只在第一和最后一页显示]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2012-02]<br></br>
    /// <修改记录
    ///		修改人=''
    ///		修改时间='yyyy-mm-dd'
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    public class PrintExtendPaper : PrintBase
    {
        private Rectangle rectangleContainsHeader;
        private Rectangle rectangleOnlyDataPage;
        private Rectangle rectangleContainsFooter;
        private int secondPageStartRowIndex = 0;
        private int lastPageStartRowIndex = 0;
        private bool isLastPageOnlyFooter = false;

        private bool isOnlyOneFarPoint = false;

        /// <summary>
        /// 自定义FarPoint打印信息
        /// </summary>
        private FarPoint.Win.Spread.PrintInfo curFarPointPrintInfo = null;

        /// <summary>
        /// 自定义FarPoint打印信息
        /// </summary>
        [System.ComponentModel.DisplayName("自定义FarPoint打印信息")]
        [System.ComponentModel.Description("自定义FarPoint打印信息")]
        protected FarPoint.Win.Spread.PrintInfo FarPointPrintInfo
        {
            get
            {
                return curFarPointPrintInfo;
            }
            set
            {
                curFarPointPrintInfo = value;
            }
        }

        /// <summary>
        /// 页眉控件集合
        /// </summary>
        private List<Control> curHeaderControls = new List<Control>();

        /// <summary>
        /// 页眉控件集合
        /// </summary>
        [System.ComponentModel.DisplayName("页眉控件集合")]
        [System.ComponentModel.Description("页眉控件集合，每页都显示")]
        public List<Control> HeaderControls
        {
            get { return curHeaderControls; }
            set { curHeaderControls = value; }
        }

        /// <summary>
        /// 页脚控件
        /// </summary>
        private List<Control> curFooterControls = new List<Control>();

        /// <summary>
        /// 页脚控件
        /// </summary>
        [System.ComponentModel.DisplayName("页脚控件集合")]
        [System.ComponentModel.Description("页脚控件集合，每页都显示")]
        public List<Control> FooterControls
        {
            get { return curFooterControls; }
            set { curFooterControls = value; }
        }

        /// <summary>
        /// 是否自动移动页脚到数据打印结束的后面，而不是页面底端
        /// </summary>
        private bool isAutoMoveFooter = true;

        /// <summary>
        /// 是否自动移动页脚到数据打印结束的后面，而不是页面底端
        /// </summary>
        [System.ComponentModel.DisplayName("是否自动移动页脚到数据打印结束的后面")]
        [System.ComponentModel.Description("是否自动移动页脚到数据打印结束的后面，而不是页面底端")]
        public bool IsAutoMoveFooter
        {
            get { return isAutoMoveFooter; }
            set { isAutoMoveFooter = value; }
        }

        /// <summary>
        /// 是否自动移动页脚到数据打印结束的后面，而不是页面底端
        /// </summary>
        private bool isAutoMoveFooters = true;

        /// <summary>
        /// 是否自动移动页脚到数据打印结束的后面，而不是页面底端
        /// </summary>
        [System.ComponentModel.DisplayName("是否自动移动页脚到数据打印结束的后面")]
        [System.ComponentModel.Description("是否自动移动页脚到数据打印结束的后面，而不是页面底端")]
        public bool IsAutoMoveFooters
        {
            get { return isAutoMoveFooters; }
            set { isAutoMoveFooters = value; }
        }

        #region 页码计算

        /// <summary>
        /// 打印页码选择
        /// </summary>
        protected override bool ChoosePrintPageNO(Graphics graphics)
        {

            //页眉页脚的高度
            int headerHeight = 0;
            int footerHeight = 0;
            int headersHeight = 0;
            int footersHeight = 0;
            if (this.HeaderControl != null)
            {
                headerHeight = this.HeaderControl.Height;
            }
            headersHeight = this.GetHeaderControlsHeight();

            if (this.FooterControl != null)
            {
                footerHeight = this.FooterControl.Height;
            }
            footersHeight = this.GetFooterControlsHeight();

            if (headersHeight + footersHeight > this.GetPaperHeight() - this.DrawingMargins.Top - this.DrawingMargins.Bottom)
            {
                throw new Exception("页眉页脚高度设置超出页面高度");
            }

            this.maxPageNO = this.GetMaxPageNO(graphics);
            if (this.maxPageNO < 0)
            {
                this.maxPageNO = 1;
                return false;
            }



            socPrintPageSelectDialog.MaxPageNO = this.maxPageNO;
            if (this.IsShowPageNOChooseDialog)
            {
                socPrintPageSelectDialog.StartPosition = FormStartPosition.CenterScreen;
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                socPrintPageSelectDialog.ShowDialog();
                if (socPrintPageSelectDialog.ToPageNO == 0)
                {
                    return false;
                }
            }

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            return true;
        }

        /// <summary>
        /// 计算最大页码
        /// </summary>
        /// <param name="graphics"></param>
        /// <returns></returns>
        private int GetMaxPageNO(Graphics graphics)
        {
            //处理横向打印
            int paperWidth = this.GetPaperWidth();
            int paperHeight = this.GetPaperHeight();


            //页眉页脚的高度
            int headerHeight = 0;
            int footerHeight = 0;
            int headersHeight = 0;
            int footersHeight = 0;
            if (this.HeaderControl != null)
            {
                headerHeight = this.HeaderControl.Height;
            }
            headersHeight = this.GetHeaderControlsHeight();

            if (this.FooterControl != null)
            {
                footerHeight = this.FooterControl.Height;
            }
            footersHeight = this.GetFooterControlsHeight();
            List<FarPoint.Win.Spread.FpSpread> fpList = this.GetFarPoints(this.printControl);
            this.isOnlyOneFarPoint = fpList.Count == 1;
            if (fpList.Count == 0)
            {          
                //在这个地方处理不含FarPoint的控件打印
                int pageNO = this.printControl.Height % (this.GetPaperHeight() - this.DrawingMargins.Top - this.DrawingMargins.Bottom);
                int morePage = 0;
                if (pageNO > 0)
                {
                    morePage = 1;
                }

                return this.printControl.Height / (this.GetPaperHeight() - this.DrawingMargins.Top - this.DrawingMargins.Bottom) + morePage;
            }
            else if (fpList.Count == 1)
            {
                FarPoint.Win.Spread.FpSpread fp = fpList[0];
                return this.GetMaxPageNOWhenOnlyOneFarPoint(graphics, fp, paperWidth, paperHeight, headersHeight, headerHeight, footersHeight, footerHeight);
            }
            else
            {
                //FarPoint过多，暂时不处理
                this.IsAutoFillFarPoint = false;
                int pageNO = this.printControl.Height % (this.GetPaperHeight() - this.DrawingMargins.Top - this.DrawingMargins.Bottom);
                int morePage = 0;
                if (pageNO > 0)
                {
                    morePage = 1;
                }

                return this.printControl.Height / (this.GetPaperHeight() - this.DrawingMargins.Top - this.DrawingMargins.Bottom) + morePage;
  
            }
        }

        /// <summary>
        /// 页面只有一个FarPoint时获取最大页码数
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="fp"></param>
        /// <param name="paperWidth"></param>
        /// <param name="paperHeight"></param>
        /// <param name="headersHeight"></param>
        /// <param name="headerHeight"></param>
        /// <param name="footersHeight"></param>
        /// <param name="footerHeight"></param>
        /// <returns></returns>
        private int GetMaxPageNOWhenOnlyOneFarPoint(Graphics graphics, FarPoint.Win.Spread.FpSpread fp, int paperWidth,int paperHeight,int headersHeight, int headerHeight, int footersHeight, int footerHeight)
        {
            int fpLocationX = 0;
            int fpLocationY = 0;

            this.GetLocation(fp, ref fpLocationX, ref fpLocationY);
            //FarPoint的出现的首页页码
            int farPointFirstPageNO = this.GetPageNOWhenFarPointFirstPrint(fp, fpLocationY);

            int rowStart = fp.ActiveSheet.PrintInfo.RowStart;
            fp.ActiveSheet.PrintInfo.PrintType = FarPoint.Win.Spread.PrintType.CellRange;

            int containsHeaderPageNO = 0;

            #region FarPoint打印的第一页
            //if (farPointFirstPageNO == 1)
            //{
                //第一页，如果数据和页眉、页脚都可以打印完整则返回1
                this.farPointRectangle = this.GetFarPointFirstPageRectangle(fp, fpLocationX, fpLocationY, paperWidth, paperHeight, headersHeight, footersHeight);
                int containsHeaderAndFooterPageNO = this.GetFarPointPage(fp, this.farPointRectangle, graphics);

                //第一页能打印完整，则返回
                if (containsHeaderAndFooterPageNO == 1)
                {
                    fp.ActiveSheet.PrintInfo.RowStart = rowStart;
                    return farPointFirstPageNO;
                }

                //理论上这个代码应该是执行不到的
                if (this.farPointRowBreaks.Length == 0)
                {
                    fp.ActiveSheet.PrintInfo.RowStart = rowStart;
                    return farPointFirstPageNO;
                }

                //超过1页，则在第一页保留页眉及每页打印项目,去掉只需要在最后一页打印的页脚，重新计算第一页FarPoint能打印的行数
                this.rectangleContainsHeader = this.GetFarPointFirstPageRectangle(fp, fpLocationX, fpLocationY, paperWidth, paperHeight, headersHeight, footersHeight - footerHeight);
                containsHeaderPageNO = this.GetFarPointPage(fp, rectangleContainsHeader, graphics);
                //保留页眉页脚一页打印不下，只保留页眉一页又可以打印下，只能将页脚打印在第二页了
                if (this.farPointRowBreaks.Length == 0 && containsHeaderPageNO == 1)
                {
                    fp.ActiveSheet.PrintInfo.RowStart = rowStart;
                    return farPointFirstPageNO + (containsHeaderAndFooterPageNO > 1 ? 1 : 0);
                }
            //}
            //else
            //{
            //    this.farPointRectangle = this.GetFarPointFirstPageRectangle(fp, fpLocationX, fpLocationY, paperWidth, paperHeight, headersHeight, footersHeight);
            //    int containsHeaderAndFooterPageNO = this.GetFarPointPage(fp, this.farPointRectangle, graphics);

            //}
            #endregion

            #region FarPoint第一页和最后一页之间

            //FarPoint第一页最后一行的行号（从1开始的）
            int firstPageFarPointLastRowIndex = 1;
            if (this.farPointRowBreaks.Length > 0)
            {
                firstPageFarPointLastRowIndex = this.farPointRowBreaks[0];
            }

            //重新计算只有数据区域的页码数
            this.secondPageStartRowIndex = this.GetFarPointValidPrintRowStart(fp, firstPageFarPointLastRowIndex);
            fp.ActiveSheet.PrintInfo.RowStart = this.secondPageStartRowIndex;

            this.rectangleOnlyDataPage = this.GetOnlyFarPointDataRectangle(fp, fpLocationX, fpLocationY, paperWidth, paperHeight, headersHeight, headerHeight, footersHeight, footerHeight);
            int onlyDataPageNO = this.GetFarPointPage(fp, rectangleOnlyDataPage, graphics);
            if (onlyDataPageNO < 1)
            {
                fp.ActiveSheet.PrintInfo.RowStart = rowStart;
                return -1;
            }
            //保留页眉一页打印不下，只保留数据一页又可以打印下，只能将页脚打印在第二页了
            if (this.farPointRowBreaks.Length == 0 && onlyDataPageNO == 1)
            {
                fp.ActiveSheet.PrintInfo.RowStart = rowStart;
                return farPointFirstPageNO + (containsHeaderPageNO > 1 ? 1 : 0); ;
            }
            #endregion

            #region 最后一页
            //最后一页应该包含页脚，则有可能大于一页，正常情况最多2页
            int lastPageFarPointLastRowIndex = this.farPointRowBreaks[this.farPointRowBreaks.Length - 1];
            this.lastPageStartRowIndex = this.GetFarPointValidPrintRowStart(fp, lastPageFarPointLastRowIndex);

            //重新计算
            fp.ActiveSheet.PrintInfo.RowStart = this.lastPageStartRowIndex;
            this.rectangleContainsFooter = new Rectangle(this.rectangleOnlyDataPage.X, this.rectangleOnlyDataPage.Y, this.rectangleOnlyDataPage.Width, this.rectangleOnlyDataPage.Height - headerHeight - headersHeight);
            int containsFooterPageNO = this.GetFarPointPage(fp, rectangleContainsFooter, graphics);
            if (containsFooterPageNO < 1)
            {
                fp.ActiveSheet.PrintInfo.RowStart = rowStart;
                return -1;
            }
            else if (containsFooterPageNO == 2)
            {
                isLastPageOnlyFooter = true;
            }
            #endregion

            fp.ActiveSheet.PrintInfo.RowStart = rowStart;
            return farPointFirstPageNO + onlyDataPageNO + containsFooterPageNO - 1;
        }

        /// <summary>
        /// 获取每页打印页眉的控件
        /// </summary>
        /// <returns></returns>
        private int GetHeaderControlsHeight()
        {
            int height = 0;
            if (this.HeaderControls != null)
            {
                foreach (Control c in this.HeaderControls)
                {
                    if (c.Parent != this.printControl)
                    {
                        throw new Exception("页眉的父级必须是打印控件");
                    }
                    int x = 0;
                    int y = 0;
                    this.GetLocation(c, ref x, ref y);
                    if (y + c.Size.Height > height)
                    {
                        height = y + c.Size.Height;
                    }
                }
            }
            if (this.HeaderControl != null)
            {
                int x = 0;
                int y = 0;
                this.GetLocation(this.HeaderControl, ref x, ref y);
                if (y + this.HeaderControl.Size.Height > height)
                {
                    height = y + this.HeaderControl.Size.Height;
                }
            }
            return height;
        }

        /// <summary>
        /// 获取每页打印页眉的控件
        /// </summary>
        /// <returns></returns>
        private int GetFooterControlsHeight()
        {
            int height = 0;
            if (this.FooterControls != null)
            {
                foreach (Control c in this.FooterControls)
                {
                    if (c.Parent != this.printControl)
                    {
                        throw new Exception("页脚的父级必须是打印控件");
                    }
                    int x = 0;
                    int y = 0;
                    this.GetLocation(c, ref x, ref y);
                    if (this.printControl.Height - y > height)
                    {
                        height = this.printControl.Height - y;
                    }
                }
            }
            if (this.FooterControl != null)
            {
                int x = 0;
                int y = 0;
                this.GetLocation(this.FooterControl, ref x, ref y);
                if (this.printControl.Height - y > height)
                {
                    height = this.printControl.Height - y;
                }
            }
            return height;
        }

        /// <summary>
        /// 计算FarPoint打印首页页码
        /// </summary>
        /// <param name="fp">FarPoint</param>
        /// <param name="locationY">FarPoint在打印控件的Y坐标</param>
        /// <returns></returns>
        private int GetPageNOWhenFarPointFirstPrint(FarPoint.Win.Spread.FpSpread fp, int locationY)
        {
            int farPointFirstPageNO = locationY / (this.GetPaperHeight() - this.DrawingMargins.Top - this.DrawingMargins.Bottom);
            if (locationY % (this.GetPaperHeight() - this.DrawingMargins.Top - this.DrawingMargins.Bottom) > 0)
            {
                farPointFirstPageNO = farPointFirstPageNO + 1;
            }

            return farPointFirstPageNO;
        }

        /// <summary>
        /// 获取FarPoint每页打印的第一行行号
        /// </summary>
        /// <param name="fp">FarPoint</param>
        /// <param name="lastPageBreakRowIndex">FarPoint上一页的最后一行</param>
        /// <returns></returns>
        private int GetFarPointValidPrintRowStart(FarPoint.Win.Spread.FpSpread fp, int lastPageBreakRowIndex)
        {
            int index = lastPageBreakRowIndex;
            //第二页的第一行应该是lastPageBreakRowIndex+1，为防止隐藏或者高度为0，重新计算第二页行号
            for (; index < fp.ActiveSheet.RowCount; index++)
            {
                if (fp.ActiveSheet.Rows[index].Visible && fp.ActiveSheet.Rows[index].Height > 0)
                {
                    break;
                }
            }

            return index;
        }

        /// <summary>
        /// 获取FarPoint的首页绘制区域
        /// </summary>
        /// <param name="fp">FarPoint</param>
        /// <param name="LocationX">FarPoint在页面的X坐标</param>
        /// <param name="LocationY">FarPoint在页面的Y坐标</param>
        /// <param name="paperWidth">纸张宽</param>
        /// <param name="paperHeight">纸张高</param>
        /// <param name="headerHeight">页码高</param>
        /// <param name="footerHeight">页脚高</param>
        /// <returns></returns>
        private Rectangle GetFarPointFirstPageRectangle(FarPoint.Win.Spread.FpSpread fp, int LocationX, int LocationY, int paperWidth, int paperHeight, int headerHeight, int footerHeight)
        {
            //FarPoint的绘制区域
            int drawingWidth = 0;
            int drawingHeight = 0;

            //首页页码最小值为0
            int farPointFirstPageNO = this.GetPageNOWhenFarPointFirstPrint(fp, LocationY);
            LocationY = LocationY - this.GetPaperHeight() * (farPointFirstPageNO - 1);


            //自动扩充数据的绘制区域
            if (this.IsAutoFillFarPoint)
            {
                drawingWidth = paperWidth - this.DrawingMargins.Left - LocationX - this.DrawingMargins.Right;
                if (headerHeight > LocationY)
                {
                    drawingHeight = paperHeight - this.DrawingMargins.Top - this.DrawingMargins.Bottom - headerHeight - footerHeight;
                }
                else
                {
                    drawingHeight = paperHeight - this.DrawingMargins.Top - LocationY - this.DrawingMargins.Bottom - footerHeight;
                }
            }
            else
            {
                //不自动扩充的绘制区域是FarPoint本身的大小，但是本身大小超过叶宽是按自动扩充处理
                drawingWidth = fp.Width;
                if (drawingWidth > paperWidth - this.DrawingMargins.Left - LocationX - this.DrawingMargins.Right)
                {
                    drawingWidth = paperWidth - this.DrawingMargins.Left - LocationX - this.DrawingMargins.Right;
                }
                drawingHeight = fp.Height;
                if (drawingHeight > paperHeight - this.DrawingMargins.Top - LocationY - this.DrawingMargins.Bottom - headerHeight - footerHeight)
                {
                    drawingHeight = paperHeight - this.DrawingMargins.Top - LocationY - this.DrawingMargins.Bottom - headerHeight - footerHeight;
                }

            }

            return new Rectangle(this.DrawingMargins.Left + LocationX, this.DrawingMargins.Top + LocationY, drawingWidth, drawingHeight);

        }

        /// <summary>
        /// 获取该页中仅包含FarPoint数据的打印区域
        /// </summary>
        /// <param name="fp">FarPoint</param>
        /// <param name="LocationX">FarPoint在页面的X坐标</param>
        /// <param name="LocationY">FarPoint在页面的Y坐标</param>
        /// <param name="paperWidth">纸张宽度</param>
        /// <param name="paperHeight">纸张高度</param>
        /// <param name="headersHeight">页眉高度</param>
        /// <param name="headerHeight">页头高度</param>
        /// <param name="footersHeight">页尾高度</param>
        /// <param name="footerHeight">页脚高度</param>
        /// <returns></returns>
        private Rectangle GetOnlyFarPointDataRectangle(FarPoint.Win.Spread.FpSpread fp, int LocationX, int LocationY, int paperWidth, int paperHeight, int headersHeight, int headerHeight, int footersHeight, int footerHeight)
        {
            int drawingWidth = 0;
            int drawingHeight = 0;
            int rectangleY = 0;

            if (this.IsAutoFillFarPoint)
            {
                drawingWidth = paperWidth - this.DrawingMargins.Left - LocationX - this.DrawingMargins.Right;
                rectangleY = headersHeight - headerHeight;
                drawingHeight = paperHeight - this.DrawingMargins.Top - this.DrawingMargins.Bottom - headersHeight + headerHeight - footersHeight + footerHeight;
            }
            else
            {
                int farPointFirstPageNO = this.GetPageNOWhenFarPointFirstPrint(fp, LocationY);
                rectangleY = LocationY - this.GetPaperHeight() * (farPointFirstPageNO - 1);

                drawingWidth = fp.Width;
                if (drawingWidth > paperWidth - this.DrawingMargins.Left - LocationX - this.DrawingMargins.Right)
                {
                    drawingWidth = paperWidth - this.DrawingMargins.Left - LocationX - this.DrawingMargins.Right;
                }
                drawingHeight = fp.Height;
                if (drawingHeight > paperHeight - this.DrawingMargins.Top - LocationY - this.DrawingMargins.Bottom - headersHeight + headerHeight - footersHeight + footerHeight)
                {
                    drawingHeight = paperHeight - this.DrawingMargins.Top - LocationY - this.DrawingMargins.Bottom - headersHeight + headerHeight - footersHeight + footerHeight;
                }
            }
            return new Rectangle(this.DrawingMargins.Left + LocationX, this.DrawingMargins.Top + rectangleY, drawingWidth, drawingHeight);
        }

        /// <summary>
        /// 在指定区域内获取Farpoint的分页页码数
        /// </summary>
        /// <param name="fp"></param>
        /// <param name="rectangle"></param>
        /// <param name="graphics"></param>
        /// <returns></returns>
        private int GetFarPointPage(FarPoint.Win.Spread.FpSpread fp, Rectangle rectangle, Graphics graphics)
        {
            if (this.FarPointPrintInfo == null)
            {
                fp.ActiveSheet.PrintInfo.ShowBorder = this.IsShowFarPointBorder;
                fp.ActiveSheet.PrintInfo.ShowRowHeaders = fp.ActiveSheet.RowHeader.Visible;
                fp.ActiveSheet.PrintInfo.ShowColumnHeaders = fp.ActiveSheet.ColumnHeader.Visible;
                fp.ActiveSheet.PrintInfo.ZoomFactor = fp.ZoomFactor;
                fp.ActiveSheet.PrintInfo.PrintType = FarPoint.Win.Spread.PrintType.CellRange;
                if (this.IsLandscape)
                {
                    fp.ActiveSheet.PrintInfo.Orientation = FarPoint.Win.Spread.PrintOrientation.Landscape;
                }
            }
            else
            {
                fp.ActiveSheet.PrintInfo = this.FarPointPrintInfo;
            }
            this.farPointRowBreaks = fp.GetOwnerPrintRowPageBreaks(graphics, rectangle, fp.ActiveSheetIndex, true);

            return fp.GetOwnerPrintPageCount(graphics, rectangle, fp.ActiveSheetIndex);
        }

        /// <summary>
        /// 在控件中查找FarPoint
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        private List<FarPoint.Win.Spread.FpSpread> GetFarPoints(Control control)
        {
            List<FarPoint.Win.Spread.FpSpread> fpList = new List<FarPoint.Win.Spread.FpSpread>();
            if (control == null || !control.Visible)
            {
                return new List<FarPoint.Win.Spread.FpSpread>();
            }

            //处理横向打印
            int paperWidth = this.GetPaperWidth();
            int paperHeight = this.GetPaperHeight();


            foreach (Control c in control.Controls)
            {

                if (c is FarPoint.Win.Spread.FpSpread)
                {
                    fpList.Add(c as FarPoint.Win.Spread.FpSpread);
                }
                //在子类中递归查找FarPoint
                List<FarPoint.Win.Spread.FpSpread> tmpList = this.GetFarPoints(c);
                if (tmpList != null && tmpList.Count > 0)
                {
                    fpList.AddRange(tmpList);
                }
            }

            return fpList;
        }

        /// <summary>
        /// 获取控件在纸张中的坐标
        /// </summary>
        /// <param name="control"></param>
        /// <param name="LocationX"></param>
        /// <param name="LocationY"></param>
        /// <returns></returns>
        private bool GetLocation(Control control, ref int LocationX, ref int LocationY)
        {
            if (control == null)
            {
                return false;
            }

            Control c = control;
            while (c != null)
            {
                if (c == this.printControl)
                {
                    break;
                }
                LocationX += c.Location.X;
                LocationY += c.Location.Y;

                c = c.Parent;
            }

            return true;
        }

        #endregion

        #region 绘制打印内容

        /// <summary>
        /// 绘制控件
        /// 页眉、数据区域的Y坐标是相对于纸张的上边
        /// 而对于页脚的Y坐标计算是相对页下边的
        /// </summary>
        /// <param name="control">控件</param>
        /// <param name="graphics">绘制工具</param>
        /// <param name="offsetX">父级控件相对于纸张边距外有效区域的X坐标</param>
        /// <param name="offsetY">父级控件相对于纸张边距外有效区域的Y坐标</param>
        private void DrawingControls(Control control, Graphics graphics, int offsetX, int offsetY)
        {
            if (control == null || graphics == null || !control.Visible)
            {
                return;
            }

            int footerHeight = 0;
            if (this.FooterControl != null)
            {
                footerHeight = this.FooterControl.Height;
            }

            #region 递归绘制子控件
            foreach (Control c in control.Controls)
            {
                //当前正在绘制页面数据区域时，不绘制页眉
                if (this.curDrawingRegionType == DrawingRegionType.Data && this.CheckIsHeaders(c))
                {
                    continue;
                }

                //当前正在绘制页面数据区域时，不绘制页脚
                if (this.curDrawingRegionType == DrawingRegionType.Data && this.CheckIsFooters(c))
                {
                    continue;
                }

                this.DrawingControls(c, graphics, c.Location.X + offsetX, c.Location.Y + offsetY);
            }
            #endregion

            //处理横向打印
            int paperWidth = this.GetPaperWidth();
            int paperHeight = this.GetPaperHeight();

            //计算控件相对于纸张的坐标
            int LocationX = this.DrawingMargins.Left + offsetX;
            int LocationY = this.DrawingMargins.Top + offsetY;
            if (this.curDrawingRegionType == DrawingRegionType.Data)
            {
                LocationY -= this.GetPaperHeight() * (this.curPageNO - 1);
            }

            if (control is FarPoint.Win.Spread.FpSpread)
            {
                this.farPointRowBreaks = new int[this.maxPageNO];
                FarPoint.Win.Spread.FpSpread fp = (FarPoint.Win.Spread.FpSpread)control;
                if (this.isOnlyOneFarPoint)
                {
                    this.DrawingFarPointWhenOnlyOne(graphics, fp);
                }
                else
                {
                    //页面数据区域越界判断，控件不在本页范围内就不打印
                    if (this.curDrawingRegionType == DrawingRegionType.Data)
                    {
                        if (!this.CheckControlPrintedCurPage(control, offsetX, offsetY, footerHeight))
                        {
                            return;
                        }
                    }
                    this.DrawingFarPointWhenMore(graphics, fp);
                }
            }
            else
            {
                //页面数据区域越界判断，控件不在本页范围内就不打印
                if (this.curDrawingRegionType == DrawingRegionType.Data)
                {
                    if (!this.CheckControlPrintedCurPage(control, offsetX, offsetY, footerHeight))
                    {
                        return;
                    }
                }
                this.DrawingControl(control, LocationX, LocationY, graphics);
            }
        }

        /// <summary>
        /// 判断控件是否应该打印在当前页
        /// </summary>
        /// <param name="control">控件</param>
        /// <param name="offsetX">X偏移</param>
        /// <param name="offsetY">Y偏移</param>
        /// <returns></returns>
        private bool CheckControlPrintedCurPage(Control control, int offsetX, int offsetY, int footerHeight)
        {
            if (offsetY + this.DrawingMargins.Top + footerHeight + this.DrawingMargins.Bottom > this.GetPaperHeight() * this.curPageNO)
            {
                return false;
            }
            if (offsetY + this.DrawingMargins.Top + footerHeight + this.DrawingMargins.Bottom < this.GetPaperHeight() * (this.curPageNO - 1))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="fp"></param>
        private void DrawingFarPointWhenOnlyOne(Graphics graphics, FarPoint.Win.Spread.FpSpread fp)
        {
            if (this.maxPageNO == 1)
            {
                int[] breaks = fp.GetOwnerPrintRowPageBreaks(graphics, this.farPointRectangle, fp.ActiveSheetIndex, true);
                if (breaks.Length > 0)
                {
                    this.farPointRowBreaks[0] = breaks[0];
                }
                fp.OwnerPrintDraw(graphics, this.farPointRectangle, fp.ActiveSheetIndex, this.curPageNO);
            }
            else
            {
                if (this.curPageNO == 1)
                {
                    int[] breaks = fp.GetOwnerPrintRowPageBreaks(graphics, this.rectangleContainsHeader, fp.ActiveSheetIndex, true);
                    if (breaks.Length > 0)
                    {
                        this.farPointRowBreaks[0] = breaks[0];
                    }
                    fp.OwnerPrintDraw(graphics, this.rectangleContainsHeader, fp.ActiveSheetIndex, this.curPageNO);
                }
                else
                {

                    if (this.curPageNO <= this.maxPageNO)
                    {
                        int startRowIndex = fp.ActiveSheet.PrintInfo.RowStart;
                        fp.ActiveSheet.PrintInfo.RowStart = this.secondPageStartRowIndex;
                        int[] breaks = fp.GetOwnerPrintRowPageBreaks(graphics, this.rectangleOnlyDataPage, fp.ActiveSheetIndex, true);
                        if (breaks.Length > 0)
                        {
                            if (this.curPageNO - 1 - 1 >= breaks.Length)
                            {
                                this.farPointRowBreaks[this.curPageNO - 1] = fp.ActiveSheet.RowCount;
                            }
                            else
                            {
                                this.farPointRowBreaks[this.curPageNO - 1] = breaks[this.curPageNO - 1 - 1];
                            }
                        }
                        fp.OwnerPrintDraw(graphics, this.rectangleOnlyDataPage, fp.ActiveSheetIndex, this.curPageNO - 1);
                        fp.ActiveSheet.PrintInfo.RowStart = startRowIndex;
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="fp"></param>
        private void DrawingFarPointWhenMore(Graphics graphics, FarPoint.Win.Spread.FpSpread fp)
        {
            if (this.FarPointPrintInfo == null)
            {
                fp.ActiveSheet.PrintInfo.ShowBorder = this.IsShowFarPointBorder;
                fp.ActiveSheet.PrintInfo.ShowRowHeaders = fp.ActiveSheet.RowHeader.Visible;
                fp.ActiveSheet.PrintInfo.ShowColumnHeaders = fp.ActiveSheet.ColumnHeader.Visible;
                fp.ActiveSheet.PrintInfo.ZoomFactor = fp.ZoomFactor;
                fp.ActiveSheet.PrintInfo.PrintType = FarPoint.Win.Spread.PrintType.CellRange;
                if (this.IsLandscape)
                {
                    fp.ActiveSheet.PrintInfo.Orientation = FarPoint.Win.Spread.PrintOrientation.Landscape;
                }
            }
            else
            {
                fp.ActiveSheet.PrintInfo = this.FarPointPrintInfo;
            }
            fp.OwnerPrintDraw(graphics, new Rectangle(fp.Location.X, fp.Location.Y, fp.Width, fp.Height), fp.ActiveSheetIndex, 1);
        }

        private bool CheckIsHeaders(Control control)
        {
            if (control != null && this.HeaderControls != null)
            {
                if (this.HeaderControl == control)
                {
                    return true;
                }
                if (this.HeaderControls.Contains(control))
                {
                    return true;
                }
            }

            return false;
        }

        private bool CheckIsFooters(Control control)
        {

            if (control != null && this.FooterControls != null)
            {
                if (this.FooterControl == control)
                {
                    return true;
                }
                if (this.FooterControls.Contains(control))
                {
                    return true;
                }
            }

            return false;
        }

        #endregion

        #region 打印主函数（事件）
        /// <summary>
        /// 打印绘制
        /// 如果有预览，在预览时就执行，在预览的打印按钮按下后会再执行一次
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void PrintDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            if (this.DrawPageStartEvent != null)
            {
                int curPageRowCount = -1;
                if (this.farPointRowBreaks.Length > this.curPageNO - 1)
                {
                    curPageRowCount = this.farPointRowBreaks[this.curPageNO - 1];
                }
                this.DrawPageStartEvent(e.Graphics, this.curPageNO, this.maxPageNO, curPageRowCount);
            }
            //跳过选择打印范围外的数据
            while (this.curPageNO < this.socPrintPageSelectDialog.FromPageNO && this.curPageNO < this.maxPageNO)
            {
                this.curPageNO++;
            }

            if (this.curPageNO > this.maxPageNO || this.curPageNO > socPrintPageSelectDialog.ToPageNO)
            {
                e.HasMorePages = false;
                return;
            }

            Graphics graphics = e.Graphics;

            #region 页面数据绘制
            this.curDrawingRegionType = DrawingRegionType.Data;
            this.DrawingControls(this.printControl, graphics, 0, 0);
            #endregion

            #region 页眉绘制
            if (this.HeaderControl != null && this.curPageNO == 1)
            {
                this.curDrawingRegionType = DrawingRegionType.Header;
                this.DrawingControls(this.HeaderControl, graphics, this.HeaderControl.Location.X, this.HeaderControl.Location.Y);
            }
            if (this.HeaderControls != null)
            {
                this.curDrawingRegionType = DrawingRegionType.Header;

                foreach (Control header in this.HeaderControls)
                {
                    int y = 0;
                    if (this.curPageNO > 1 && this.HeaderControl != null)
                    {
                        y = this.HeaderControl.Height;
                    }
                    if (header.Location.Y - y < 0)
                    {
                        y = 0;
                    }
                    this.DrawingControls(header, graphics, header.Location.X, header.Location.Y - y);
                }
            }
            #endregion

            #region 页脚绘制
            //单个页脚绘制还需要知道它在整个页脚区域this.GetFooterControlsHeight()的相对位置y，假设这个页脚到页面底端的高度为H：
            //因为：H=this.GetFooterControlsHeight()-y
            //      H=this.printControl.Height-footer.Location.Y
            //所以：this.GetFooterControlsHeight()-y=this.printControl.Height-footer.Location.Y
            //      y=this.GetFooterControlsHeight()-this.printControl.Height+footer.Location.Y
            //      y=footer.Location.Y - this.printControl.Height + this.GetFooterControlsHeight()

            //不在最后一页的页脚绘制在this.GetPaperHeight()-this.GetFooterControlsHeight()-this.DrawingMargins.Bottom+this.FooterControl.Height+y
            if (this.FooterControls != null && this.curPageNO < this.maxPageNO)
            {
                this.curDrawingRegionType = DrawingRegionType.Footer;

                foreach (Control footer in this.FooterControls)
                {
                    int footerHeight = 0;
                    if (this.FooterControl != null && footer.Location.Y < this.FooterControl.Location.Y)
                    {
                        footerHeight = this.FooterControl.Height;
                    }
                    int locationY = this.GetPaperHeight() - this.DrawingMargins.Top - this.GetFooterControlsHeight() - this.DrawingMargins.Bottom + footerHeight + (this.GetFooterControlsHeight() - this.printControl.Height + footer.Location.Y);
                    this.DrawingControls(footer, graphics, footer.Location.X, locationY);
                }
            }
            if (this.curPageNO == this.maxPageNO)
            {
                this.curDrawingRegionType = DrawingRegionType.Footer;

                //计算页脚上面的数据区域高度
                float height = 0;
                if (!this.isLastPageOnlyFooter)
                {
                    List<FarPoint.Win.Spread.FpSpread> fpList = this.GetFarPoints(this.printControl);
                    if (fpList.Count == 1)
                    {
                        FarPoint.Win.Spread.FpSpread fp = fpList[0];
                        for (int index = this.lastPageStartRowIndex; index < fp.ActiveSheet.RowCount; index++)
                        {
                            if (fp.ActiveSheet.Rows[index].Visible && fp.ActiveSheet.Rows[index].Height > 0)
                            {
                                height += fp.ActiveSheet.Rows[index].Height * fp.ActiveSheet.ZoomFactor;
                            }
                        }
                        for (int index = 0; index < fp.ActiveSheet.ColumnHeader.RowCount; index++)
                        {
                            if (fp.ActiveSheet.ColumnHeader.Rows[index].Visible && fp.ActiveSheet.ColumnHeader.Rows[index].Height > 0)
                            {
                                height += fp.ActiveSheet.ColumnHeader.Rows[index].Height * fp.ActiveSheet.ZoomFactor;
                            }
                        }
                    }
                    height += this.GetHeaderControlsHeight() + this.DrawingMargins.Top;
                    if (height > this.GetPaperHeight() - this.DrawingMarginBottom - this.DrawingMarginTop)
                    {
                        height = this.GetPaperHeight() - this.GetFooterControlsHeight() - this.DrawingMargins.Bottom - this.DrawingMarginTop;
                    }
                }

                //locationY + (int)height是最后一页整个页脚绘制的Y坐标
                if (this.FooterControls != null)
                {
                    foreach (Control footer in this.FooterControls)
                    {
                        if (this.IsAutoMoveFooters)
                        {
                            //this.GetPaperHeight() - this.DrawingMargins.Top - this.GetFooterControlsHeight() - this.DrawingMargins.Bottom + footerHeight + (this.GetFooterControlsHeight() - this.printControl.Height + footer.Location.Y);
                            int footerHeight = 0;
                            if (this.FooterControl != null && footer.Location.Y < this.FooterControl.Location.Y)
                            {
                                footerHeight = this.FooterControl.Height;
                            }
                            this.DrawingControls(footer, graphics, footer.Location.X, (int)height - footerHeight + footer.Location.Y - this.printControl.Height + this.GetFooterControlsHeight());
                        }
                        else
                        {
                            this.DrawingControls(footer, graphics, footer.Location.X, this.GetPaperHeight() - this.DrawingMargins.Bottom - this.GetFooterControlsHeight() + footer.Location.Y - this.printControl.Height + this.GetFooterControlsHeight());
                        }
                    }
                }
                if (this.FooterControl != null)
                {
                    if (this.IsAutoMoveFooter)
                    {
                        this.DrawingControls(FooterControl, graphics, FooterControl.Location.X, (int)height + FooterControl.Location.Y - this.printControl.Height + this.GetFooterControlsHeight());
                    }
                    else
                    {
                        this.DrawingControls(FooterControl, graphics, FooterControl.Location.X, this.GetPaperHeight() - this.DrawingMargins.Bottom - this.GetFooterControlsHeight() + FooterControl.Location.Y - this.printControl.Height + this.GetFooterControlsHeight());
                    }
                }
            }
            #endregion

            if (this.DrawPageEndEvent != null)
            {
                int curPageRowCount = -1;
                if (this.farPointRowBreaks.Length > this.curPageNO - 1)
                {
                    curPageRowCount = this.farPointRowBreaks[this.curPageNO - 1];
                }
                this.DrawPageEndEvent(graphics, this.curPageNO, this.maxPageNO, curPageRowCount);
            }
            this.curDrawingRegionType = DrawingRegionType.Null;

            #region 分页
            if (this.curPageNO < this.socPrintPageSelectDialog.ToPageNO && this.curPageNO < maxPageNO)
            {
                curPageNO++;
                e.HasMorePages = true;
            }
            else
            {
                curPageNO = 1;
                e.HasMorePages = false;
            }
            #endregion
        }

        #endregion
    }
}