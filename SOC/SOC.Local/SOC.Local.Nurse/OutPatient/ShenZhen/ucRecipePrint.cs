using BarcodeLib;
using FarPoint.Win.Spread;
using FarPoint.Win.Spread.Model;
using Neusoft.FrameWork.WinForms.Classes;
using Neusoft.FrameWork.WinForms.Controls;
using Neusoft.HISFC.BizLogic.Manager;
using Neusoft.HISFC.BizLogic.Nurse;
using Neusoft.HISFC.BizProcess.Interface.Nurse;
using Neusoft.HISFC.Components.Common.Classes;
using Neusoft.HISFC.Models.Nurse;
using Neusoft.SOC.Local.Nurse.Properties;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using ThoughtWorks.QRCode.Codec; //二维条码

namespace Neusoft.SOC.Local.Nurse.ShenZhen
{
  
    partial class ucRecipePrint : UserControl, IInjectPrint
    {
        private ArrayList alPrint = new ArrayList();
       
        private System.Drawing.Printing.PrintDocument PrintDocument = new System.Drawing.Printing.PrintDocument();
        private PageSize psManager = new PageSize();

        public ucRecipePrint()
        {
            this.InitializeComponent();
            this.PrintDocument.PrintPage += new PrintPageEventHandler(this.PrintDocumentPrintPage);
        }

        private int DrawControl(Graphics graphics, Control control, int locationX, int locationY)
        {
            foreach (Control control2 in control.Controls)
            {
                if (control2.Visible && (control2.Height > 0))
                {
                    if ((control2 is FpSpread) && (((FpSpread)control2).Sheets.Count > 0))
                    {
                        FpSpread spread = (FpSpread)control2;
                        int width = control2.Size.Width;
                        int height = control2.Size.Height;
                        if (this.curPageNO == 1)
                        {
                            PrintInfo info = new PrintInfo();
                            info.ShowRowHeaders = spread.Sheets[0].RowHeader.Visible;
                            info.ShowBorder = false;
                            info.PrintType = PrintType.All;
                            spread.ActiveSheet.PrintInfo = info;
                            int num3 = spread.GetOwnerPrintPageCount(graphics, new Rectangle(locationX + control2.Location.X, locationY + control2.Location.Y, this.PrintDocument.DefaultPageSettings.PaperSize.Width - locationX, ((this.PrintDocument.DefaultPageSettings.PaperSize.Height - locationY) - 20) - this.PrintDocument.DefaultPageSettings.Margins.Bottom), spread.ActiveSheetIndex);
                            this.maxPageNO = num3;
                        }
                        spread.OwnerPrintDraw(graphics, new Rectangle(locationX + control2.Location.X, locationY + control2.Location.Y, this.PrintDocument.DefaultPageSettings.PaperSize.Width - locationX, ((this.PrintDocument.DefaultPageSettings.PaperSize.Height - locationY) - 20) - this.PrintDocument.DefaultPageSettings.Margins.Bottom), spread.ActiveSheetIndex, this.curPageNO);
                    }
                    else if (control2 is Panel)
                    {
                        graphics.FillRectangle(new SolidBrush(control2.BackColor), locationX + control2.Location.X, locationY + control2.Location.Y, control2.Width, control2.Height);
                    }
                    else
                    {
                        graphics.DrawString(control2.Text, control2.Font, new SolidBrush(control2.ForeColor), (float)(locationX + control2.Location.X), (float)(locationY + control2.Location.Y));
                    }
                    if (control2.Controls.Count > 0)
                    {
                        this.DrawControl(graphics, control2, locationX + control2.Location.X, locationY + control2.Location.Y);
                    }
                }
            }
            return control.Height;
        }

        private void fpSpread1_CellClick(object sender, CellClickEventArgs e)
        {
        }

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
            }
            return "";
        }


        private void myPrintView()
        {
            PrintPreviewDialog dialog = new PrintPreviewDialog();
            dialog.Document = this.PrintDocument;
            try
            {
                dialog.WindowState = FormWindowState.Maximized;
            }
            catch
            {
            }
            try
            {
                dialog.ShowDialog();
                dialog.Dispose();
            }
            catch (Exception exception)
            {
                MessageBox.Show("打印机报错！" + exception.Message);
            }
        }

        void IInjectPrint.Init(ArrayList al)
        {
            if (this.neuSpread1_Sheet1.RowCount > 0)
            {
                this.neuSpread1_Sheet1.RemoveRows(0, this.neuSpread1_Sheet1.RowCount);
            }
            Neusoft.HISFC.Models.Nurse.Inject inject = null;
            Barcode barcode = new Barcode();
            barcode.IncludeLabel = true;
            for (int i = 0; i < al.Count; i++)
            {
                inject = (Neusoft.HISFC.Models.Nurse.Inject)al[i];
                this.neuSpread1_Sheet1.Rows.Add(0, 1);
                this.neuSpread1_Sheet1.Cells[i, 0].Column.Width = 150f;
                this.neuSpread1_Sheet1.Cells[i, 1].Column.Width = 75f;
                if ((inject.Item.Item.Name != null) && (inject.Item.Item.Name != ""))
                {
                    this.neuSpread1_Sheet1.Cells[i, 0].Text = inject.Item.Item.Name;
                }
                else
                {
                    this.neuSpread1_Sheet1.Cells[i, 0].Text = inject.Item.Name;
                }
                this.neuSpread1_Sheet1.Cells[i, 1].Text = inject.Item.Order.DoseOnce + inject.Item.Order.DoseUnit;
                this.neuSpread1_Sheet1.Cells[i, 2].Tag = inject.Item.Order.Combo.ID;
                this.neuSpread1_Sheet1.Cells[i, 2].Text = inject.Item.Order.Combo.ID;
                this.neuSpread1_Sheet1.Cells[i, 3].Text = inject.Item.Order.Usage.Name;
                this.neuSpread1_Sheet1.Cells[i, 4].Text = inject.Item.Order.Frequency.ID + "*" + inject.Item.Days.ToString("F2");
            }
            this.SetComb(al);
            inject = (Neusoft.HISFC.Models.Nurse.Inject)al[0];
            this.lbName.Text = inject.Patient.Name;
            this.lbAge.Text = this.injectMgr.GetAge(inject.Patient.Birthday, DateTime.Now);
            this.neuPictureBox1.Image = barcode.Encode(TYPE.CODE128, inject.Item.Patient.PID.CardNO, 140, 50);
            QRCodeEncoder encoder = new QRCodeEncoder();
            encoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            encoder.QRCodeScale = 4;
            encoder.QRCodeVersion = 7;
            encoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
            this.neuPictureBox2.Image = encoder.Encode(inject.Item.Patient.PID.CardNO);
            if (string.IsNullOrEmpty(inject.Patient.PID.CardNO))
            {
                this.neuLbCardNo.Text = inject.Patient.Card.ID;
            }
            else
            {
                this.neuLbCardNo.Text = inject.Patient.PID.CardNO;
            }
            if (inject.Patient.Sex.ID.ToString() == "M")
            {
                this.lbSex.Text = "男";
            }
            else if (inject.Patient.Sex.ID.ToString() == "F")
            {
                this.lbSex.Text = "女";
            }
            else
            {
                this.lbSex.Text = "";
            }
            this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 1);
            this.Print();
        }

        private void Print()
        {
            Neusoft.FrameWork.WinForms.Classes.Print print = null;
            if (print == null)
            {
                print = new Neusoft.FrameWork.WinForms.Classes.Print();
                Neusoft.HISFC.Components.Common.Classes.Function.GetPageSize("MZXSK", ref print);
            }
            Control control = this;
            print.ControlBorder = enuControlBorder.None;
            control.Width = base.Width;
            control.Height = base.Height;
            print.PrintPage(12, 1, new Control[] { control });
        }

        private void PrintDocumentPrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics graphics = e.Graphics;
            this.DrawControl(graphics, this, 0, 0);
            if (this.curPageNO < this.maxPageNO)
            {
                e.HasMorePages = true;
                this.curPageNO++;
            }
            else
            {
                this.curPageNO = 1;
                this.maxPageNO = 1;
                e.HasMorePages = false;
            }
        }

        protected void PrintPage()
        {
            this.SetPaperSize(null);
            this.PrintDocument.Print();
        }

        protected void PrintPage(PaperSize paperSize)
        {
            this.SetPaperSize(paperSize);
            this.PrintDocument.Print();
        }

        protected void PrintView()
        {
            this.SetPaperSize(null);
            this.myPrintView();
        }

        protected void PrintView(PaperSize paperSize)
        {
            this.SetPaperSize(paperSize);
            this.myPrintView();
        }

        private void SetComb(ArrayList al)
        {
            int num2;
            int rowCount = this.neuSpread1_Sheet1.RowCount;
            this.neuSpread1_Sheet1.SetValue(0, 2, "┓");
            this.neuSpread1_Sheet1.SetValue(rowCount - 1, 2, "┛");
            for (num2 = 1; num2 < (rowCount - 1); num2++)
            {
                int num3 = num2 - 1;
                int num4 = num2 + 1;
                string str = this.neuSpread1_Sheet1.Cells[num2, 2].Tag.ToString();
                string str2 = this.neuSpread1_Sheet1.Cells[num3, 2].Tag.ToString();
                string str3 = this.neuSpread1_Sheet1.Cells[num4, 2].Tag.ToString();
                string str4 = this.neuSpread1_Sheet1.Cells[num2, 4].Tag.ToString();
                string str5 = this.neuSpread1_Sheet1.Cells[num3, 4].Tag.ToString();
                string str6 = this.neuSpread1_Sheet1.Cells[num4, 4].Tag.ToString();
                bool flag = true;
                bool flag2 = true;
                if ((str + str4) != (str2 + str5))
                {
                    flag = false;
                }
                if ((str + str4) != (str3 + str6))
                {
                    flag2 = false;
                }
                if (flag && flag2)
                {
                    this.neuSpread1_Sheet1.SetValue(num2, 2, "┃");
                }
                if (!(!flag || flag2))
                {
                    this.neuSpread1_Sheet1.SetValue(num2, 2, "┛");
                }
                if (!(flag || !flag2))
                {
                    this.neuSpread1_Sheet1.SetValue(num2, 2, "┓");
                }
                if (!(flag || flag2))
                {
                    this.neuSpread1_Sheet1.SetValue(num2, 2, "");
                }
            }
            for (num2 = 0; num2 < rowCount; num2++)
            {
                if (this.neuSpread1_Sheet1.Cells[num2, 2].Tag == "")
                {
                    this.neuSpread1_Sheet1.SetValue(num2, 2, "");
                }
            }
            if (rowCount == 1)
            {
                this.neuSpread1_Sheet1.SetValue(0, 2, "");
            }
            if ((rowCount == 2) && (this.neuSpread1_Sheet1.Cells[0, 2].Tag != this.neuSpread1_Sheet1.Cells[1, 2].Tag))
            {
                this.neuSpread1_Sheet1.SetValue(0, 2, "");
                this.neuSpread1_Sheet1.SetValue(1, 2, "");
            }
            if (rowCount > 2)
            {
                if ((this.neuSpread1_Sheet1.GetValue(1, 2).ToString() != "┃") && (this.neuSpread1_Sheet1.GetValue(1, 2).ToString() != "┛"))
                {
                    this.neuSpread1_Sheet1.SetValue(0, 2, "");
                }
                if ((this.neuSpread1_Sheet1.GetValue(rowCount - 2, 2).ToString() != "┃") && (this.neuSpread1_Sheet1.GetValue(rowCount - 2, 2).ToString() != "┓"))
                {
                    this.neuSpread1_Sheet1.SetValue(rowCount - 1, 2, "");
                }
            }
        }

        private void SetPaperSize(PaperSize paperSize)
        {
            if (paperSize == null)
            {
                paperSize = new PaperSize("xsk", 450, 380);
            }
            this.PrintDocument.DefaultPageSettings.PaperSize = new PaperSize(paperSize.PaperName, paperSize.Width, paperSize.Height);
            this.PrintDocument.PrinterSettings.DefaultPageSettings.PaperSize = new PaperSize(paperSize.PaperName, paperSize.Width, paperSize.Height);
        }

        private void ucRecipePrint_Load(object sender, EventArgs e)
        {
            this.neuSpread1_Sheet1.GrayAreaBackColor = Color.White;
        }

        public bool IsReprint
        {
            get
            {
                return this.isReprint;
            }
            set
            {
                this.isReprint = value;
            }
        }
    }
}

