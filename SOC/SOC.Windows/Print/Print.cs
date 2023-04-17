using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace FS.SOC.Windows.Forms
{
    /// <summary>
    /// Print<br></br>
    /// [功能描述: 打印类]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2012-02]<br></br>
    /// <修改记录
    ///		修改人=''
    ///		修改时间='yyyy-mm-dd'
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    [Obsolete("cube说已经作废了...", false)]
    public class Print : PrintBase
    {
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


        #region 页码计算

        /// <summary>
        /// 打印页码选择
        /// </summary>
        protected override bool ChoosePrintPageNO(Graphics graphics)
        {
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
            if (this.HeaderControl != null)
            {
                headerHeight = this.HeaderControl.Height;
            }
            if (this.FooterControl != null)
            {
                footerHeight = this.FooterControl.Height;
            }

           
            FarPoint.Win.Spread.FpSpread fp = this.GetFarPoint(this.printControl);
            if (fp == null || !fp.Visible)
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
            else
            {
                this.farPointRectangle = this.GetFarPointRectangle(fp, paperWidth, paperHeight, headerHeight, footerHeight);
                return this.GetFarPointPage(fp, this.farPointRectangle, graphics);
            }

        }

        /// <summary>
        /// 获取FarPoint的绘制区域
        /// </summary>
        /// <param name="fp">FarPoint</param>
        /// <param name="paperWidth">纸张宽</param>
        /// <param name="paperHeight">纸张高</param>
        /// <param name="headerHeight">页码高</param>
        /// <param name="footerHeight">页脚高</param>
        /// <returns></returns>
        private Rectangle GetFarPointRectangle(FarPoint.Win.Spread.FpSpread fp, int paperWidth, int paperHeight, int headerHeight, int footerHeight)
        {
            int LocationX = 0;
            int LocationY = 0;

            //获取fp在打印纸张上的位置
            this.GetLocation(fp, ref LocationX, ref LocationY);

            //FarPoint的绘制区域
            int drawingWidth = 0;
            int drawingHeight = 0;

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
        /// 获取Farpoint的分页页码
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
        /// 在控件中查找FarPoint，返回第一个控件的第一个FarPoint
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        private FarPoint.Win.Spread.FpSpread GetFarPoint(Control control)
        {
            if (control == null || !control.Visible)
            {
                return null;
            }

            //处理横向打印
            int paperWidth = this.GetPaperWidth();
            int paperHeight = this.GetPaperHeight();
           

            foreach (Control c in control.Controls)
            {

                if (c is FarPoint.Win.Spread.FpSpread)
                {
                    return c as FarPoint.Win.Spread.FpSpread;
                }
                //在子类中递归查找FarPoint
                FarPoint.Win.Spread.FpSpread fp = GetFarPoint(c);
                if (fp is FarPoint.Win.Spread.FpSpread)
                {
                    return fp;
                }
            }

            return null;
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

            #region 递归绘制子控件
            foreach (Control c in control.Controls)
            {
                int footerHeight = 0;
                if (this.FooterControl != null)
                {
                    footerHeight = this.FooterControl.Height;
                }

                //当前正在绘制页面数据区域时，不绘制页眉
                if (this.curDrawingRegionType == DrawingRegionType.Data && c == this.HeaderControl)
                {
                    continue;
                }

                //当前正在绘制页面数据区域时，不绘制页脚
                if (this.curDrawingRegionType == DrawingRegionType.Data && c == this.FooterControl)
                {
                    continue;
                }

                //页面数据区域越界判断，控件不在本页范围内就不打印
                if (!(c is FarPoint.Win.Spread.FpSpread) && this.curDrawingRegionType == DrawingRegionType.Data)
                {
                    if (c.Location.Y + offsetY + c.Height + this.DrawingMargins.Top + footerHeight + this.DrawingMargins.Bottom > this.GetPaperHeight() * this.curPageNO)
                    {
                        continue;
                    }
                    if (c.Location.Y + offsetY + c.Height + this.DrawingMargins.Top + footerHeight + this.DrawingMargins.Bottom < this.GetPaperHeight() * (this.curPageNO - 1))
                    {
                        continue;
                    }
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
                FarPoint.Win.Spread.FpSpread fp = (FarPoint.Win.Spread.FpSpread)control;
                fp.OwnerPrintDraw(graphics, this.farPointRectangle, fp.ActiveSheetIndex, this.curPageNO);
            }
            else
            {
                this.DrawingControl(control, LocationX, LocationY, graphics);
            }
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

            //跳过选择打印范围外的数据
            while (this.curPageNO < this.socPrintPageSelectDialog.FromPageNO && this.curPageNO < this.maxPageNO)
            {
                curPageNO++;
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

            #region 页眉、页脚绘制
            if (this.HeaderControl != null)
            {
                this.curDrawingRegionType = DrawingRegionType.Header;
                this.DrawingControls(this.HeaderControl, graphics, this.HeaderControl.Location.X, this.HeaderControl.Location.Y);
            }
            if (this.FooterControl != null && this.GetPaperHeight() - this.FooterControl.Height > 0)
            {
                this.curDrawingRegionType = DrawingRegionType.Footer;
                this.DrawingControls(this.FooterControl, graphics, this.FooterControl.Location.X, this.GetPaperHeight() - this.FooterControl.Height - this.DrawingMargins.Bottom);
            }
            #endregion

            if (this.DrawPageEndEvent != null)
            {
                int curPageLastRowIndex = -1;
                if (this.farPointRowBreaks.Length > this.curPageNO - 1)
                {
                    curPageLastRowIndex = this.farPointRowBreaks[this.curPageNO - 1];
                }
                this.DrawPageEndEvent(graphics, this.curPageNO, this.maxPageNO, curPageLastRowIndex);
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