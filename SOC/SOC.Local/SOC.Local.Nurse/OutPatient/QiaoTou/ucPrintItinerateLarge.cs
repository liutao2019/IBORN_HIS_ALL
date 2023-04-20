using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.Nurse.QiaoTou
{
    public partial class ucPrintItinerateLarge : System.Windows.Forms.UserControl, FS.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint
    {
        #region 域

        private ArrayList alPrint = new ArrayList();

        private FS.HISFC.BizLogic.Nurse.Inject injectMgr = new FS.HISFC.BizLogic.Nurse.Inject();

        /// <summary>
        /// 打印纸张设置类
        /// </summary>
        FS.HISFC.BizLogic.Manager.PageSize psManager = new FS.HISFC.BizLogic.Manager.PageSize();

        FS.HISFC.BizLogic.HealthRecord.Diagnose diagManager = new FS.HISFC.BizLogic.HealthRecord.Diagnose();
        FS.HISFC.BizLogic.Registration.Register registerManager=new FS.HISFC.BizLogic.Registration.Register();

        private FS.HISFC.BizLogic.Order.Order OrderBizLogic = new FS.HISFC.BizLogic.Order.Order(); 

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

        ~ucPrintItinerateLarge()
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

        Graphics graphics = null;

        private void PrintDocumentPrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            graphics = e.Graphics;

            //数据FarPoint
            this.DrawControl(graphics, this, 0, 0);

            //画下面签名信息
            this.DrawControl(graphics, this.panel1, this.panel1.Location.X, this.panel1.Location.Y);

            //数据FarPoint
            this.DrawControl(graphics, this.neuPanel1, this.neuPanel1.Location.X, this.neuPanel2.Height);

            //设置页码
            this.nlbPageNo.Text = "页：" + curPageNO + "/" + maxPageNO;

            //标题部分
            this.DrawControl(graphics, this.neuPanel2, this.neuPanel2.Location.X, this.neuPanel2.Location.Y);

            ////标题部分
            //this.DrawControl(graphics, this.neuPanel2, this.neuPanel2.Location.X, this.neuPanel2.Location.Y);
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
                                drawingWidth,drawingHeight
                                //this.PrintDocument.DefaultPageSettings.PaperSize.Width - locationX ,
                                //this.PrintDocument.DefaultPageSettings.PaperSize.Height - locationY - 20 - this.PrintDocument.DefaultPageSettings.Margins.Bottom
                                ), spread.ActiveSheetIndex);
                            maxPageNO = count;
                        }
                        spread.OwnerPrintDraw(graphics, new Rectangle(
                           locationX + c.Location.X,
                                locationY + c.Location.Y,
                                drawingWidth,drawingHeight
                                //this.PrintDocument.DefaultPageSettings.PaperSize.Width - locationX ,
                                //this.PrintDocument.DefaultPageSettings.PaperSize.Height - locationY  - 20 - this.PrintDocument.DefaultPageSettings.Margins.Bottom
                            ), spread.ActiveSheetIndex, this.curPageNO);
                    }
                    else if (c is Panel)
                    {
                        graphics.FillRectangle(new SolidBrush(c.BackColor), locationX + c.Location.X, locationY + c.Location.Y, c.Width, c.Height);
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
                paperSize = new System.Drawing.Printing.PaperSize("xsk", 450, 380);
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
        public ucPrintItinerateLarge()
        {
            InitializeComponent();
            this.PrintDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(PrintDocumentPrintPage);
        }

        private void ucPrintItinerateLarge_Load(object sender, EventArgs e)
        {
            this.neuSpread1_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
        }

        FS.HISFC.Models.Nurse.Inject info = null;


        FS.FrameWork.WinForms.Classes.Print print = null;

        FS.HISFC.BizLogic.Manager.PageSize pgMgr = null;
        FS.HISFC.Models.Base.PageSize pSize = null;
        System.Drawing.Printing.PaperSize curPaperSize = null;
        FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();

        ArrayList alRecipeNo = null;

        public void Init(ArrayList al)
        {
            try
            {
                if (this.neuSpread1_Sheet1.RowCount > 0)
                {
                    this.neuSpread1_Sheet1.RemoveRows(0, this.neuSpread1_Sheet1.RowCount);
                }

                alRecipeNo = new ArrayList();

                //用于处理BID药品只打印一条
                Hashtable hsOrderID = new Hashtable();

                for (int i = 0; i < al.Count; i++)
                {
                    info = (FS.HISFC.Models.Nurse.Inject)al[i];

                    if (!hsOrderID.Contains(info.Item.RecipeNO + info.Item.SequenceNO.ToString()))
                    {
                        hsOrderID.Add(info.Item.RecipeNO + info.Item.SequenceNO.ToString(), null);
                    }
                    else
                    {
                        continue;
                    }

                    this.neuSpread1_Sheet1.Rows.Add(0, 1);
                    if (info.Item.Item.Name != null && info.Item.Item.Name != "")
                    {
                        this.neuSpread1_Sheet1.Cells[0, 0].Text = info.Item.Item.Name;
                    }
                    else
                    {
                        this.neuSpread1_Sheet1.Cells[0, 0].Text = info.Item.Name;
                    }

                    if (!alRecipeNo.Contains(info.Item.RecipeNO))
                    {
                        alRecipeNo.Add(info.Item.RecipeNO);
                    }

                    this.neuSpread1_Sheet1.Cells[0, 1].Tag = info.Item.Order.Combo.ID;//组合号
                    this.neuSpread1_Sheet1.Cells[0, 2].Text = info.Item.Order.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + info.Item.Order.DoseUnit;
                    this.neuSpread1_Sheet1.Cells[0, 3].Text = info.Item.Order.Usage.Name;
                    this.neuSpread1_Sheet1.Cells[0, 4].Text = info.Item.Order.Frequency.ID + "*" + info.Item.Days.ToString("F4").TrimEnd('0').TrimEnd('.');
                    this.neuSpread1_Sheet1.Cells[0, 4].Tag = info.User03;
                    //医嘱备注
                    this.neuSpread1_Sheet1.Cells[0, 5].Text = " ";
                    this.neuSpread1_Sheet1.Cells[0, 6].Text = " ";
                    this.neuSpread1_Sheet1.Cells[0, 7].Text = " ";
                    this.neuSpread1_Sheet1.Cells[0, 8].Text = info.Memo + this.OrderBizLogic.TransHypotest(info.Hypotest);//this.GetHyTestInfo(info.Hypotest);
                }

                if (string.IsNullOrEmpty(this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 8].Text))
                {
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 8].Text = ".";
                }

                this.SetComb(al);

                info = (FS.HISFC.Models.Nurse.Inject)al[0];
                //重新获取患者信息
                FS.HISFC.Models.Registration.Register regsiter = registerManager.GetByClinic(info.Patient.ID);
                if (regsiter != null&&!string.IsNullOrEmpty(regsiter.ID))
                {
                    info.Patient = regsiter;
                }

                this.lblTitle.Text = injectMgr.Hospital.Name + "门急诊注射单";
                this.lbName.Text = info.Patient.Name;
                this.lbTime.Text = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                this.lbAge.Text = this.injectMgr.GetAge(info.Patient.Birthday, System.DateTime.Now);
                this.neuLbDoct.Text = info.Item.Order.ReciptDoctor.Name;
                if (string.IsNullOrEmpty(info.Patient.PID.CardNO))
                {
                    this.neuLbCardNo.Text = info.Patient.Card.ID;
                }
                else
                {
                    this.neuLbCardNo.Text = info.Patient.PID.CardNO;
                }
                if (info.Patient.Sex.ID.ToString() == "M")
                {
                    this.lbSex.Text = "男";
                }
                else if (info.Patient.Sex.ID.ToString() == "F")
                {
                    this.lbSex.Text = "女";
                }
                else
                {
                    this.lbSex.Text = "";
                }
                
                try
                {
                    string invoiceNo = "";
                    if (alRecipeNo != null && al.Count > 0)
                    {
                        ArrayList alFeeDetail = null;
                        foreach (string recipeNo in alRecipeNo)
                        {
                            alFeeDetail = feeIntegrate.QueryFeeDetailFromRecipeNO(recipeNo);
                            if (alFeeDetail != null && alFeeDetail.Count > 0)
                            {
                                if (!invoiceNo.Contains((alFeeDetail[0] as FS.HISFC.Models.Fee.Outpatient.FeeItemList).Invoice.ID))
                                {
                                    if (invoiceNo == "")
                                    {
                                        invoiceNo = (alFeeDetail[0] as FS.HISFC.Models.Fee.Outpatient.FeeItemList).Invoice.ID;
                                    }
                                    else
                                    {
                                        invoiceNo += "/" + (alFeeDetail[0] as FS.HISFC.Models.Fee.Outpatient.FeeItemList).Invoice.ID;
                                    }
                                }
                            }
                        }
                    }
                    lblInvoiceNo.Text = invoiceNo;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                

                this.lblDiag.Text = this.GetDiag(info.Patient.ID);

                this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 1);

                print = new FS.FrameWork.WinForms.Classes.Print();
                print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;

                pgMgr = new FS.HISFC.BizLogic.Manager.PageSize();
                pSize = pgMgr.GetPageSize("MZXSK");
                curPaperSize = new System.Drawing.Printing.PaperSize();
                if (pSize == null)
                {
                    pSize = new FS.HISFC.Models.Base.PageSize("xsk", 450, 400);
                    pSize.Top = 0;
                    pSize.Left = 0;
                }

                curPaperSize.PaperName = pSize.Name;
                curPaperSize.Height = pSize.Height;
                curPaperSize.Width = pSize.Width;
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        /// <summary>
        /// 获取诊断信息
        /// </summary>
        /// <param name="ClinicCode"></param>
        /// <returns></returns>
        private string GetDiag(string ClinicCode)
        {
            try
            {
                ArrayList al = this.diagManager.QueryCaseDiagnoseForClinic(ClinicCode, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC);
                if (al == null)
                {
                    MessageBox.Show("获取诊断信息失败：" + diagManager.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return "";
                }

                string strDiag = "";
                foreach (FS.HISFC.Models.HealthRecord.Diagnose diag in al)
                {
                    if (diag != null && diag.Memo != null && diag.Memo != "")
                    {
                        strDiag += diag.Memo + "、";
                    }
                    else
                    {
                        strDiag += diag.DiagInfo.ICD10.Name;
                    }
                }
                strDiag = strDiag.TrimEnd(new char[] { '、' });
                return strDiag;
            }
            catch (Exception ex)
            {
                MessageBox.Show("获取诊断信息失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }
        }

        /// <summary>
        /// 获取皮试信息
        /// </summary>
        /// <param name="hytestID"></param>
        /// <returns></returns>
        private string GetHyTestInfo(string hytestID)
        {
            switch (hytestID)
            {
                case "1":
                    return "(免试)";
                case "2":
                    return "(需皮试)";
                case "3":
                    return "(＋)";
                case "4":
                    return "(－)";
                case "0":
                default:
                    return "";
            }
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
            this.neuSpread1_Sheet1.SetValue(0, 1, "┓");
            //最后行
            this.neuSpread1_Sheet1.SetValue(myCount - 1, 1, "┛");
            //中间行
            for (i = 1; i < myCount - 1; i++)
            {
                int prior = i - 1;
                int next = i + 1;
                string currentRowCombNo = this.neuSpread1_Sheet1.Cells[i, 1].Tag.ToString();
                string priorRowCombNo = this.neuSpread1_Sheet1.Cells[prior, 1].Tag.ToString();
                string nextRowCombNo = this.neuSpread1_Sheet1.Cells[next, 1].Tag.ToString();

                // 患者当次注射处方信息
                string currentRowInjectTime = this.neuSpread1_Sheet1.Cells[i, 4].Tag.ToString();
                string priorRowInjectTime = this.neuSpread1_Sheet1.Cells[prior, 4].Tag.ToString();
                string nextRowInjectTime = this.neuSpread1_Sheet1.Cells[next, 4].Tag.ToString();

                #region 画组合号
                bool bl1 = true;
                bool bl2 = true;
                if (currentRowCombNo + currentRowInjectTime != priorRowCombNo + priorRowInjectTime)
                {
                    bl1 = false;
                }
                if (currentRowCombNo + currentRowInjectTime != nextRowCombNo + nextRowInjectTime)
                {
                    bl2 = false;
                }

                //  ┃
                if (bl1 && bl2)
                {
                    this.neuSpread1_Sheet1.SetValue(i, 1, "┃");
                }
                //  ┛
                if (bl1 && !bl2)
                {
                    this.neuSpread1_Sheet1.SetValue(i, 1, "┛");
                }
                //  ┓
                if (!bl1 && bl2)
                {
                    this.neuSpread1_Sheet1.SetValue(i, 1, "┓");
                }
                //  ""
                if (!bl1 && !bl2)
                {
                    this.neuSpread1_Sheet1.SetValue(i, 1, "");
                }
                #endregion
            }
            //把没有组号的去掉
            for (i = 0; i < myCount; i++)
            {
                if (this.neuSpread1_Sheet1.Cells[i, 1].Tag.ToString() == "")
                {
                    this.neuSpread1_Sheet1.SetValue(i, 1, "");
                }
            }
            //判断首末行 有组号，且只有自己一组数据的情况
            if (myCount == 1)
            {
                this.neuSpread1_Sheet1.SetValue(0, 1, "");
            }
            //只有首末两行，那么还要判断组号啊
            if (myCount == 2)
            {
                if (this.neuSpread1_Sheet1.Cells[0, 1].Tag.ToString().Trim() != this.neuSpread1_Sheet1.Cells[1, 1].Tag.ToString().Trim())
                {
                    this.neuSpread1_Sheet1.SetValue(0, 1, "");
                    this.neuSpread1_Sheet1.SetValue(1, 1, "");
                }
            }
            if (myCount > 2)
            {
                if (this.neuSpread1_Sheet1.GetValue(1, 1).ToString() != "┃"
                    && this.neuSpread1_Sheet1.GetValue(1, 1).ToString() != "┛")
                {
                    this.neuSpread1_Sheet1.SetValue(0, 1, "");
                }
                if (this.neuSpread1_Sheet1.GetValue(myCount - 2, 1).ToString() != "┃"
                    && this.neuSpread1_Sheet1.GetValue(myCount - 2, 1).ToString() != "┓")
                {
                    this.neuSpread1_Sheet1.SetValue(myCount - 1, 1, "");
                }
            }

            #region 组合下打印横线

            for (int k = 0; k < this.neuSpread1_Sheet1.RowCount; k++)
            {
                if (this.neuSpread1_Sheet1.Cells[k, 1].Text.Trim() == "┛"
                    || string.IsNullOrEmpty(this.neuSpread1_Sheet1.Cells[k, 1].Text.Trim()))
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
        #endregion
    }
}
