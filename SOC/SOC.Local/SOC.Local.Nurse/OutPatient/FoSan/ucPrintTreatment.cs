using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.Nurse.FoSan
{
    public partial class ucPrintTreatment : System.Windows.Forms.UserControl, FS.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint
    {
        #region 域
        private ArrayList alPrint = new ArrayList();
        int height = 0;
        int width = 0;
        private FS.HISFC.BizLogic.Nurse.Inject injectMgr = new FS.HISFC.BizLogic.Nurse.Inject();
        /// <summary>
        /// 打印纸张设置类
        /// </summary>
        FS.HISFC.BizLogic.Manager.PageSize psManager = new FS.HISFC.BizLogic.Manager.PageSize();

        FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItemList=new FS.HISFC.Models.Fee.Outpatient.FeeItemList();

        FS.HISFC.BizLogic.Fee.Outpatient feeManager = new FS.HISFC.BizLogic.Fee.Outpatient();

        /// <summary>
        /// 是否补打
        /// </summary>
        private bool isReprint = false;

        /// <summary>
        /// 是否补打
        /// </summary>
        public bool IsReprint
        {
            get
            {
                return isReprint;
            }
            set
            {
                isReprint = value;
                this.lblReprint.Visible = value;
            }
        }

        #endregion

        #region 打印处理

        ~ucPrintTreatment()
        {
            this.PrintDocument.Dispose();
        }
        /// <summary>
        /// 当前打印页的页码
        /// 程序自动计算的
        /// </summary>
        private int curPageNO = 1;

        /// <summary>
        /// 本次打印最大页码
        /// 程序自动计算的
        /// </summary>
        private int maxPageNO = 1;

        private void PrintDocumentPrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Graphics graphics = e.Graphics;

            //数据FarPoint
            this.DrawControl(graphics, this, 0, 0);

            //数据FarPoint
            this.DrawControl(graphics, this.neuPanel1, this.neuPanel1.Location.X, this.neuPanel2.Height);

            //画线部分
            this.DrawControl(graphics, this.neuPanel4, this.neuPanel4.Location.X, this.neuPanel4.Location.Y);

            //底部部分
            this.DrawControl(graphics, this.neuPanel3, this.neuPanel3.Location.X, height-this.neuPanel3.Height);


            //清空页码
            this.nlbPageNo.Text = "";
            //设置页码
            this.nlbPageNo.Text = "页：" + curPageNO + "/" + maxPageNO;

            //标题部分
            this.DrawControl(graphics, this.neuPanel2, this.neuPanel2.Location.X, this.neuPanel2.Location.Y);

            #region 分页
            if (this.curPageNO < maxPageNO)
            {
                e.HasMorePages = true;
                curPageNO++;
            }
            else
            {
                curPageNO = 1;
                maxPageNO = 1;
                e.HasMorePages = false;
            }
            #endregion
        }

        private int DrawControl(Graphics graphics, Control control, int locationX, int locationY)
        {
            //控件层次越深消耗越大
            foreach (Control c in control.Controls)
            {

                if (c.Visible && c.Height > 0)
                {
                    if (c is FarPoint.Win.Spread.FpSpread && ((FarPoint.Win.Spread.FpSpread)c).Sheets.Count > 0)
                    {
                        FarPoint.Win.Spread.FpSpread spread = ((FarPoint.Win.Spread.FpSpread)c);
                        int drawingWidth = c.Size.Width;
                        int drawingHeight = c.Size.Height;
                        if (this.curPageNO == 1)
                        {
                            FarPoint.Win.Spread.PrintInfo printInfo = new FarPoint.Win.Spread.PrintInfo();
                            printInfo.ShowRowHeaders = spread.Sheets[0].RowHeader.Visible;
                            printInfo.ShowBorder = false;
                            printInfo.PrintType = FarPoint.Win.Spread.PrintType.All;
                            spread.ActiveSheet.PrintInfo = printInfo;
                            int count = spread.GetOwnerPrintPageCount(graphics, new Rectangle(
                                locationX + c.Location.X,
                                locationY + c.Location.Y,
                                this.PrintDocument.DefaultPageSettings.PaperSize.Width - locationX,
                                this.PrintDocument.DefaultPageSettings.PaperSize.Height - locationY- 40 - this.PrintDocument.DefaultPageSettings.Margins.Bottom
                                ), spread.ActiveSheetIndex);
                            maxPageNO = count;
                        }
                        spread.OwnerPrintDraw(graphics, new Rectangle(
                           locationX + c.Location.X,
                                locationY + c.Location.Y,
                                this.PrintDocument.DefaultPageSettings.PaperSize.Width - locationX,
                                this.PrintDocument.DefaultPageSettings.PaperSize.Height - locationY -40 - this.PrintDocument.DefaultPageSettings.Margins.Bottom
                            ), spread.ActiveSheetIndex, this.curPageNO);

                    }
                    else if (c is Panel)
                    {
                        graphics.FillRectangle(new SolidBrush(c.BackColor), locationX + c.Location.X, locationY + c.Location.Y, c.Width, c.Height);
                    }
                    else if (c is Label && Color.Black.A == c.BackColor.A && Color.Black.B == c.BackColor.B && Color.Black.G == c.BackColor.G)
                    {
                        graphics.FillRectangle(new SolidBrush(c.BackColor), locationX + c.Location.X, locationY + c.Location.Y, c.Width, c.Height);
                    }
                    else if (c is System.Windows.Forms.PictureBox)
                    {
                        graphics.DrawImage(((System.Windows.Forms.PictureBox)c).Image, c.Location);
                    }
                    else
                    {
                        graphics.DrawString(c.Text, c.Font, new SolidBrush(c.ForeColor), locationX + c.Location.X, locationY + c.Location.Y);
                    }
                    //if (c.Controls.Count > 0)
                    //{
                    //    this.DrawControl(graphics, c, locationX + c.Location.X, locationY + c.Location.Y);
                    //}
                }
            }

            return control.Height;
        }

        protected void PrintView(System.Drawing.Printing.PaperSize paperSize)
        {
            this.SetPaperSize(paperSize);
            this.myPrintView();
        }

        protected void PrintView()
        {
            this.SetPaperSize(null);
            this.myPrintView();
        }

        private void SetPaperSize(System.Drawing.Printing.PaperSize paperSize)
        {
            if (paperSize == null)
            {
                paperSize = new System.Drawing.Printing.PaperSize("xsk", 827, 583);
            }

            this.PrintDocument.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize(paperSize.PaperName, paperSize.Width, paperSize.Height);
            this.PrintDocument.PrinterSettings.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize(paperSize.PaperName, paperSize.Width, paperSize.Height);
        }

        private void myPrintView()
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

        protected void PrintPage(System.Drawing.Printing.PaperSize paperSize)
        {
            this.SetPaperSize(paperSize);
            this.PrintDocument.Print();
        }

        protected void PrintPage()
        {
            this.SetPaperSize(null);
            this.PrintDocument.Print();
        }

        /// <summary>
        /// 打印用
        /// </summary>
        private System.Drawing.Printing.PrintDocument PrintDocument = new System.Drawing.Printing.PrintDocument();

        #endregion

        #region 方法
        public ucPrintTreatment()
        {
            InitializeComponent();
            this.PrintDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(PrintDocumentPrintPage);
        }

        private void ucPrintItinerateLarge_Load(object sender, EventArgs e)
        {
            this.neuSpread1_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
        }

        public void Init(ArrayList al)
        {
            if (this.neuSpread1_Sheet1.RowCount > 1)
            {
                this.neuSpread1_Sheet1.RemoveRows(1, this.neuSpread1_Sheet1.RowCount - 1);
            }
            FS.HISFC.Models.Nurse.Inject info = null;

            //用于处理BID药品只打印一条
            Hashtable hsOrderID = new Hashtable();

            string recipeNO = "";
            int sequenceNO = 0;

            for (int i = 0; i < al.Count; i++)
            {
                recipeNO = "";
                sequenceNO = 0;
                info = (FS.HISFC.Models.Nurse.Inject)al[i];

                if (!hsOrderID.Contains(info.Item.RecipeNO + info.Item.SequenceNO.ToString()))
                {
                    hsOrderID.Add(info.Item.RecipeNO + info.Item.SequenceNO.ToString(), null);
                    recipeNO = info.Item.RecipeNO.ToString();
                    sequenceNO = info.Item.SequenceNO;
                }
                else
                {
                    continue;
                }

                int row = this.neuSpread1_Sheet1.RowCount;
                this.neuSpread1_Sheet1.Rows.Add(row, 1);
                this.neuSpread1_Sheet1.Rows[row].Height = 37;
                if (info.Item.Item.Name != null && info.Item.Item.Name != "")
                {
                    this.neuSpread1_Sheet1.Cells[row, 1].Text = info.Item.Item.Name;
                }
                else
                {
                    this.neuSpread1_Sheet1.Cells[row, 1].Text = info.Item.Name;
                }
                this.neuSpread1_Sheet1.Cells[row, 2].Tag = info.Item.Order.Combo.ID;//组合号

                this.neuSpread1_Sheet1.Cells[row, 3].Text = float.Parse(info.Item.Order.DoseOnce.ToString()).ToString() + info.Item.Order.DoseUnit;

                string usagename = info.Item.Order.Usage.Name;
                if (this.SetStr1(usagename))
                {
                    this.neuSpread1_Sheet1.Cells.Get(row, 4).Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                    this.neuSpread1_Sheet1.Cells[row, 4].Text = info.Item.Order.Usage.Name;
                }
                else
                {
                    this.neuSpread1_Sheet1.Cells[row, 4].Text = info.Item.Order.Usage.Name;
                }
                string remark = this.SetRemark(recipeNO, sequenceNO);
                this.neuSpread1_Sheet1.Cells[row, 5].Text = remark;
                this.neuSpread1_Sheet1.Cells[row, 6].Text = info.Item.Order.Frequency.ID.ToLower();
                this.neuSpread1_Sheet1.Cells[row, 6].Tag = info.User03;
                this.neuSpread1_Sheet1.Cells[row, 7].Text = info.Item.Days.ToString();

                feeItemList = feeManager.GetFeeItemListForFee(recipeNO, sequenceNO);

                if (feeItemList == null)
                {
                    return;
                }

                // 不显示单位
                if (feeItemList.FeePack == "0")
                {
                    this.neuSpread1_Sheet1.Cells[row, 8].Text = feeItemList.Item.Qty.ToString();
                }
                else
                {
                    this.neuSpread1_Sheet1.Cells[row, 8].Text = (feeItemList.Item.Qty/feeItemList.Item.PackQty).ToString();
                }
            }
            this.SetComb(al);
            int seq = 1;

            for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
            {
                if (this.neuSpread1_Sheet1.Cells[i, 2].Text.ToString() == "┓")
                {
                    this.neuSpread1_Sheet1.Cells[i, 0].Text = seq + " .";
                }
                else if (this.neuSpread1_Sheet1.Cells[i, 2].Text.ToString() == "┛")
                {
                    this.neuSpread1_Sheet1.Cells[i, 4].Text = "";
                    this.neuSpread1_Sheet1.Cells[i, 6].Text = "";
                    this.neuSpread1_Sheet1.Cells[i, 7].Text = "";
                    seq++;
                }

                else if (this.neuSpread1_Sheet1.Cells[i, 2].Text.ToString() == "┃")
                {
                    this.neuSpread1_Sheet1.Cells[i, 4].Text = "";
                    this.neuSpread1_Sheet1.Cells[i, 6].Text = "";
                    this.neuSpread1_Sheet1.Cells[i, 7].Text = "";
                }
                else if (this.neuSpread1_Sheet1.Cells[i, 2].Text.ToString() == "")
                {
                    this.neuSpread1_Sheet1.Cells[i, 0].Text = seq + " .";
                    seq++;
                }



            }
            //this.SetFP(al);
            info = (FS.HISFC.Models.Nurse.Inject)al[0];
            this.lbTime.Text = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string pName = info.Patient.Name;
            string pAge = this.injectMgr.GetAge(info.Patient.Birthday, System.DateTime.Now);
            string pSex = "";
            if (info.Patient.Sex.ID.ToString() == "M")
            {
                pSex = "男";
            }
            else if (info.Patient.Sex.ID.ToString() == "F")
            {
                pSex = "女";
            }
            else
            {
                pSex = "";
            }
            string doctorName = info.Item.Order.Doctor.Name;
            string doctorNo = info.Item.Order.Doctor.ID;
            if (string.IsNullOrEmpty(info.Patient.PID.CardNO))
            {
                this.npbBarCode.Image = this.CreateBarCode(info.Patient.Card.ID);
                this.neuLblCardNo.Text = info.Patient.Card.ID.TrimStart('0');
            }
            else
            {
                this.npbBarCode.Image = this.CreateBarCode(info.Patient.PID.CardNO);
                this.neuLblCardNo.Text = info.Patient.PID.CardNO.TrimStart('0');
            }
            this.lblInfo.Text = "姓名：" + pName + "     " + "性别：" + pSex + "     " + "年龄：" + pAge;
            this.lblDoct.Text = "医生工号：" + doctorNo + "  " + "医生签名：" + doctorName;
            this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 1);


            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;

            FS.HISFC.BizLogic.Manager.PageSize pgMgr = new FS.HISFC.BizLogic.Manager.PageSize();
            FS.HISFC.Models.Base.PageSize pSize = pgMgr.GetPageSize("MZXSK");
            System.Drawing.Printing.PaperSize curPaperSize = new System.Drawing.Printing.PaperSize();
            if (pSize == null)
            {
                pSize = new FS.HISFC.Models.Base.PageSize("MZXSK", 812, 583);
                pSize.Top = 0;
                pSize.Left = 0;
            }

            curPaperSize.PaperName = pSize.Name;
            curPaperSize.Height = pSize.Height;
            curPaperSize.Width = pSize.Width;
            this.height = pSize.Height;
            this.width = pSize.Width;
            this.PrintDocument.DefaultPageSettings.Margins.Bottom = pSize.Top;

            print.SetPageSize(pSize);

            if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
            {
                this.PrintView(curPaperSize);
            }
            else
            {
                if (!string.IsNullOrEmpty(pSize.Printer))
                {
                    this.PrintDocument.PrinterSettings.PrinterName = pSize.Printer;
                }

                this.PrintPage(curPaperSize);
            }

        }

        /// <summary>
        /// 生成条形码方法
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private Image CreateBarCode(string code)
        {
            BarcodeLib.Barcode b = new BarcodeLib.Barcode();
            BarcodeLib.TYPE type = BarcodeLib.TYPE.CODE128;
            BarcodeLib.AlignmentPositions Align = BarcodeLib.AlignmentPositions.CENTER;
            b.IncludeLabel = false;
            b.Alignment = Align;
            return b.Encode(type, code, System.Drawing.Color.Black, System.Drawing.Color.White, this.npbBarCode.Size.Width, this.npbBarCode.Height);
        }

        /// <summary>
        /// 设置组合号
        /// </summary>
        private void SetComb(ArrayList al)
        {
            //只显示下边框
            FarPoint.Win.BevelBorder bevelBorder1 = new FarPoint.Win.BevelBorder(FarPoint.Win.BevelBorderType.Raised, System.Drawing.SystemColors.ControlLightLight, System.Drawing.Color.Black, 1, false, false, false, true);

            //无边框设置
            FarPoint.Win.BevelBorder bevelBorder2 = new FarPoint.Win.BevelBorder(FarPoint.Win.BevelBorderType.Raised, System.Drawing.SystemColors.ControlLightLight, System.Drawing.Color.Black, 1, false, false, false, false);
            int myCount = this.neuSpread1_Sheet1.RowCount;
            int i;
            //第一行
            this.neuSpread1_Sheet1.SetValue(1, 2, "┓");
            //最后行
            this.neuSpread1_Sheet1.SetValue(myCount - 1, 2, "┛");
            //中间行
            for (i = 2; i < myCount - 1; i++)
            {
                int prior = i - 1;
                int next = i + 1;
                string currentRowCombNo = this.neuSpread1_Sheet1.Cells[i, 2].Tag.ToString();
                string priorRowCombNo = this.neuSpread1_Sheet1.Cells[prior, 2].Tag.ToString();
                string nextRowCombNo = this.neuSpread1_Sheet1.Cells[next, 2].Tag.ToString();

                // 患者当次注射处方信息
                string currentRowInjectTime = this.neuSpread1_Sheet1.Cells[i, 6].Tag.ToString();
                string priorRowInjectTime = this.neuSpread1_Sheet1.Cells[prior, 6].Tag.ToString();
                string nextRowInjectTime = this.neuSpread1_Sheet1.Cells[next, 6].Tag.ToString();

                #region """""
                bool bl1 = true;
                bool bl2 = true;
                if (currentRowCombNo + currentRowInjectTime != priorRowCombNo + priorRowInjectTime)
                    bl1 = false;
                if (currentRowCombNo + currentRowInjectTime != nextRowCombNo + nextRowInjectTime)
                    bl2 = false;
                //  ┃
                if (bl1 && bl2)
                {
                    this.neuSpread1_Sheet1.SetValue(i, 2, "┃");
                }
                //  ┛
                if (bl1 && !bl2)
                {
                    this.neuSpread1_Sheet1.SetValue(i, 2, "┛");
                }
                //  ┓
                if (!bl1 && bl2)
                {
                    this.neuSpread1_Sheet1.SetValue(i, 2, "┓");
                }
                //  ""
                if (!bl1 && !bl2)
                {
                    this.neuSpread1_Sheet1.SetValue(i, 2, "");
                }
                #endregion
            }
            //把没有组号的去掉
            for (i = 1; i < myCount; i++)
            {
                if (this.neuSpread1_Sheet1.Cells[i, 2].Tag.ToString() == "")
                {
                    this.neuSpread1_Sheet1.SetValue(i, 2, "");
                }
            }
            //判断首末行 有组号，且只有自己一组数据的情况
            if (myCount == 2)
            {
                this.neuSpread1_Sheet1.SetValue(1, 2, "");
            }
            if (myCount == 3)
            {
                if (this.neuSpread1_Sheet1.Cells[1, 2].Tag.ToString().Trim() != this.neuSpread1_Sheet1.Cells[2, 2].Tag.ToString().Trim())
                {
                    this.neuSpread1_Sheet1.SetValue(1, 2, "");
                    this.neuSpread1_Sheet1.SetValue(2, 2, "");
                }
                if (this.neuSpread1_Sheet1.Cells[1, 6].Tag.ToString().Trim() != this.neuSpread1_Sheet1.Cells[2, 6].Tag.ToString().Trim())
                {
                    this.neuSpread1_Sheet1.SetValue(1, 2, "");
                    this.neuSpread1_Sheet1.SetValue(2, 2, "");
                }
            }
            if (myCount > 3)
            {
                if (this.neuSpread1_Sheet1.GetValue(2, 2).ToString() != "┃"
                    && this.neuSpread1_Sheet1.GetValue(2, 2).ToString() != "┛")
                {
                    this.neuSpread1_Sheet1.SetValue(1, 2, "");
                }
                if (this.neuSpread1_Sheet1.GetValue(myCount - 2, 2).ToString() != "┃"
                    && this.neuSpread1_Sheet1.GetValue(myCount - 2, 2).ToString() != "┓")
                {
                    this.neuSpread1_Sheet1.SetValue(myCount - 1, 2, "");
                }
            }
            #region 打印横线

            //第一行画横线
            this.neuSpread1_Sheet1.Rows[0].Border = bevelBorder1;

            //组合号画横线
            for (int k = 1; k < this.neuSpread1_Sheet1.RowCount; k++)
            {
                if (this.neuSpread1_Sheet1.Cells[k, 2].Text.Trim() == "┛"
                    || string.IsNullOrEmpty(this.neuSpread1_Sheet1.Cells[k, 2].Text.Trim()))
                {
                    this.neuSpread1_Sheet1.Rows[k].Border = bevelBorder1;
                }
                else
                {
                    this.neuSpread1_Sheet1.Rows[k].Border = bevelBorder2;
                }
            }
            #endregion
        }
        /// <summary>
        /// 获得处方备注
        /// </summary>
        private string SetRemark(string recipeNO, int sequenceNO)
        {
            string remark = null;
            string sql = @"select t.remark from met_ord_recipedetail t
                           where t.recipe_no ='{0}'
                           and   t.recipe_seq ='{1}'";
            try
            {
                sql = string.Format(sql, recipeNO, sequenceNO);
            }
            catch (Exception ex)
            {;
                return null;
            }
            if (this.feeManager.ExecSqlReturnOne(sql)!="-1")
            {
                remark = this.feeManager.ExecSqlReturnOne(sql);
            }
            return remark;
        }
        /// <summary>
        /// 返回5个字符，用以换行。
        /// </summary>
        private string SetStr(string str1)
        {
            if(str1.Length >=8)
            {
                return str1;
            }
            else 
            {
                return str1 = str1.PadLeft(8); 
            }
        }
        /// <summary>
        /// 返回5个字符，用以换行。
        /// </summary>
        private bool  SetStr1(string str1)
        {
            if (string.IsNullOrEmpty(str1))
            {
                return false;
            }
            Regex rex = new Regex("[a-z0-9A-Z_]+");
            Match ma = rex.Match(str1);
            if (ma.Success)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion


    }
}
