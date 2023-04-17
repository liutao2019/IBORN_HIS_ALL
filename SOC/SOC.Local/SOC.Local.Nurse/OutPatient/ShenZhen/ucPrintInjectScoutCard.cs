using BarcodeLib;
using FarPoint.Win;
using FarPoint.Win.Spread;
using FarPoint.Win.Spread.CellType;
using FarPoint.Win.Spread.Model;
using Neusoft.FrameWork.WinForms.Classes;
using Neusoft.FrameWork.WinForms.Controls;
using Neusoft.HISFC.BizLogic.HealthRecord;
using Neusoft.HISFC.BizLogic.Manager;
using Neusoft.HISFC.BizLogic.Nurse;
using Neusoft.HISFC.BizProcess.Integrate;
using Neusoft.HISFC.BizProcess.Interface.Nurse;
using Neusoft.HISFC.Components.Common.Classes;
using Neusoft.HISFC.Models.Base;
using Neusoft.HISFC.Models.Fee.Outpatient;
using Neusoft.HISFC.Models.HealthRecord;
using Neusoft.HISFC.Models.HealthRecord.EnumServer;
using Neusoft.HISFC.Models.Nurse;
using Neusoft.SOC.Local.Nurse.Properties;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using ThoughtWorks.QRCode.Codec;
namespace Neusoft.SOC.Local.Nurse.ShenZhen
{

    partial class ucPrintInjectScoutCard : UserControl, IInjectItineratePrint
    {
        private ArrayList alPrint = new ArrayList();
        private ArrayList alRecipeNo = null;
        private int curPageNO = 1;
        private PaperSize curPaperSize = null;
        private bool isReprint = false;
        private Graphics graphics = null;
        private Neusoft.HISFC.BizLogic.HealthRecord.Diagnose diagManager = new Neusoft.HISFC.BizLogic.HealthRecord.Diagnose();
        private Neusoft.HISFC.BizProcess.Integrate.Fee feeIntegrate = new Neusoft.HISFC.BizProcess.Integrate.Fee();
        private Neusoft.HISFC.Models.Nurse.Inject info = null;
        private Neusoft.HISFC.BizLogic.Nurse.Inject injectMgr = new Neusoft.HISFC.BizLogic.Nurse.Inject();
        private Neusoft.HISFC.BizLogic.Manager.PageSize pgMgr = null;
        private Print print = null;
        private System.Drawing.Printing.PrintDocument PrintDocument = new System.Drawing.Printing.PrintDocument();
        private Neusoft.HISFC.Models.Base.PageSize pSize = null;
        private Neusoft.HISFC.BizLogic.Manager.PageSize psManager = new Neusoft.HISFC.BizLogic.Manager.PageSize();

        public ucPrintInjectScoutCard()
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
                            int num3 = spread.GetOwnerPrintPageCount(graphics, new Rectangle(locationX + control2.Location.X, locationY + control2.Location.Y, width, height), spread.ActiveSheetIndex);
                            this.maxPageNO = num3;
                        }
                        spread.OwnerPrintDraw(graphics, new Rectangle(locationX + control2.Location.X, locationY + control2.Location.Y, width, height), spread.ActiveSheetIndex, this.curPageNO);
                    }
                    else if (control2 is Panel)
                    {
                        graphics.FillRectangle(new SolidBrush(control2.BackColor), locationX + control2.Location.X, locationY + control2.Location.Y, control2.Width, control2.Height);
                    }
                    else
                    {
                        graphics.DrawString(control2.Text, control2.Font, new SolidBrush(control2.ForeColor), (float)(locationX + control2.Location.X), (float)(locationY + control2.Location.Y));
                    }
                }
            }
            return control.Height;
        }

        ~ucPrintInjectScoutCard()
        {
            this.PrintDocument.Dispose();
        }

        private string GetDiag(string ClinicCode)
        {
            try
            {
                ArrayList list = this.diagManager.QueryCaseDiagnoseForClinic(ClinicCode, frmTypes.DOC);
                if (list == null)
                {
                    MessageBox.Show("获取诊断信息失败：" + this.diagManager.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return "";
                }
                string str = "";
                foreach (Neusoft.HISFC.Models.HealthRecord.Diagnose diagnose in list)
                {
                    if (((diagnose != null) && (diagnose.Memo != null)) && (diagnose.Memo != ""))
                    {
                        str = str + diagnose.Memo + "、";
                    }
                    else
                    {
                        str = str + diagnose.DiagInfo.ICD10.Name;
                    }
                }
                return str.TrimEnd(new char[] { '、' });
            }
            catch (Exception exception)
            {
                MessageBox.Show("获取诊断信息失败：" + exception.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return "";
            }
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

        private void neuPictureBox2_Click(object sender, EventArgs e)
        {
        }

        void IInjectItineratePrint.Init(ArrayList al)
        {
            Exception exception;
            try
            {
                if (this.neuSpread1_Sheet1.RowCount > 0)
                {
                }
                this.alRecipeNo = new ArrayList();
                Hashtable hashtable = new Hashtable();
                for (int i = 0; i < al.Count; i++)
                {
                    this.info = (Neusoft.HISFC.Models.Nurse.Inject)al[i];
                    if (!hashtable.Contains(this.info.Item.RecipeNO + this.info.Item.SequenceNO.ToString()))
                    {
                        hashtable.Add(this.info.Item.RecipeNO + this.info.Item.SequenceNO.ToString(), null);
                    }
                    else
                    {
                        continue;
                    }
                    this.neuSpread1_Sheet1.Cells[i, 0].Tag = this.info.Item.Order.Combo.ID;
                    this.neuSpread1_Sheet1.Cells[i, 0].Value = this.info.Item.Order.Combo.ID;
                    if ((this.info.Item.Item.Name != null) && (this.info.Item.Item.Name != ""))
                    {
                        this.neuSpread1_Sheet1.Cells[i, 1].Text = this.info.Item.Item.Name;
                    }
                    else
                    {
                        this.neuSpread1_Sheet1.Cells[i, 1].Text = this.info.Item.Name;
                    }
                    if (!this.alRecipeNo.Contains(this.info.Item.RecipeNO))
                    {
                        this.alRecipeNo.Add(this.info.Item.RecipeNO);
                    }
                    this.neuSpread1_Sheet1.Cells[i, 2].Text = this.info.Item.Order.DoseOnce.ToString("F4").TrimEnd(new char[] { '0' }).TrimEnd(new char[] { '.' }) + this.info.Item.Order.DoseUnit;
                    this.neuSpread1_Sheet1.Cells[i, 3].Text = this.info.Item.Order.Usage.Name;
                    this.neuSpread1_Sheet1.Cells[i, 4].Text = this.info.Item.Order.Frequency.ID + "*" + this.info.Item.Days.ToString("F4").TrimEnd(new char[] { '0' }).TrimEnd(new char[] { '.' });
                    this.neuSpread1_Sheet1.Cells[i, 4].Tag = this.info.User03;
                    this.neuSpread1_Sheet1.Cells[i, 5].Text = " ";
                    this.neuSpread1_Sheet1.Cells[i, 6].Text = " ";
                }
                this.SetComb(al);
                this.info = (Neusoft.HISFC.Models.Nurse.Inject)al[0];
                this.lblTitle.Text = this.injectMgr.Hospital.Name + "  静脉输液（血）巡视卡";
                this.lbName.Text = this.info.Patient.Name;
                this.lbTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                this.lbAge.Text = this.injectMgr.GetAge(this.info.Patient.Birthday, DateTime.Now);
                this.neuLbDoct.Text = this.info.Item.Order.Doctor.Name;
                Barcode barcode = new Barcode();
                barcode.IncludeLabel = true;
                this.neuPictureBox1.Image = barcode.Encode(TYPE.CODE128, this.info.Item.Patient.PID.CardNO, 140, 50);
                QRCodeEncoder encoder = new QRCodeEncoder();
                encoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
                encoder.QRCodeScale = 4;
                encoder.QRCodeVersion = 7;
                encoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
                this.neuPictureBox2.Image = encoder.Encode(this.info.Item.Patient.PID.CardNO);
                try
                {
                    string iD = "";
                    if ((this.alRecipeNo != null) && (al.Count > 0))
                    {
                        ArrayList list = null;
                        foreach (string str2 in this.alRecipeNo)
                        {
                            list = this.feeIntegrate.QueryFeeDetailFromRecipeNO(str2);
                            if (((list != null) && (list.Count > 0)) && !iD.Contains((list[0] as FeeItemList).Invoice.ID))
                            {
                                if (iD == "")
                                {
                                    iD = (list[0] as FeeItemList).Invoice.ID;
                                }
                                else
                                {
                                    iD = iD + "/" + (list[0] as FeeItemList).Invoice.ID;
                                }
                            }
                        }
                    }
                }
                catch (Exception exception1)
                {
                    exception = exception1;
                    MessageBox.Show(exception.Message);
                }
                if (this.info.Patient.Sex.ID.ToString() == "M")
                {
                    this.lbSex.Text = "男";
                }
                else if (this.info.Patient.Sex.ID.ToString() == "F")
                {
                    this.lbSex.Text = "女";
                }
                else
                {
                    this.lbSex.Text = "";
                }
                this.lblDiag.Text = this.GetDiag(this.info.Patient.ID);
                this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 1);
                this.printInject();
            }
            catch (Exception exception2)
            {
                exception = exception2;
                MessageBox.Show(exception.Message);
            }
        }

        private void PrintDocumentPrintPage(object sender, PrintPageEventArgs e)
        {
            this.graphics = e.Graphics;
            this.DrawControl(this.graphics, this, 0, 0);
            this.DrawControl(this.graphics, this.panel1, this.panel1.Location.X, this.panel1.Location.Y);
            this.DrawControl(this.graphics, this.neuPanel1, this.neuPanel1.Location.X, this.neuPanel2.Height);
            this.nlbPageNo.Text = string.Concat(new object[] { "页：", this.curPageNO, "/", this.maxPageNO });
            this.DrawControl(this.graphics, this.neuPanel2, this.neuPanel2.Location.X, this.neuPanel2.Location.Y);
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

        private void printInject()
        {
            Print print = null;
            if (print == null)
            {
                print = new Print();
                Neusoft.HISFC.Components.Common.Classes.Function.GetPageSize("MZXSK", ref print);
            }
            Control control = this;
            print.ControlBorder = enuControlBorder.None;
            control.Width = base.Width;
            control.Height = base.Height;
            print.PrintPage(12, 1, new Control[] { control });
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
            BevelBorder border = new BevelBorder(BevelBorderType.Raised, SystemColors.ControlLightLight, Color.Black, 1, false, false, false, true);
            BevelBorder border2 = new BevelBorder(BevelBorderType.Raised, SystemColors.ControlLightLight, Color.Black, 1, false, false, false, false);
            int count = al.Count;
            this.neuSpread1_Sheet1.SetValue(0, 0, "┓");
            this.neuSpread1_Sheet1.SetValue(count - 1, 0, "┛");
            for (num2 = 1; num2 < (count - 1); num2++)
            {
                int num3 = num2 - 1;
                int num4 = num2 + 1;
                string str = this.neuSpread1_Sheet1.Cells[num2, 0].Value.ToString();
                string str2 = this.neuSpread1_Sheet1.Cells[num3, 0].Value.ToString();
                string str3 = this.neuSpread1_Sheet1.Cells[num4, 0].Value.ToString();
                string str4 = this.neuSpread1_Sheet1.Cells[num2, 5].Value.ToString();
                string str5 = this.neuSpread1_Sheet1.Cells[num3, 5].Value.ToString();
                string str6 = this.neuSpread1_Sheet1.Cells[num4, 5].Value.ToString();
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
                    this.neuSpread1_Sheet1.SetValue(num2, 0, "┃");
                }
                if (!(!flag || flag2))
                {
                    this.neuSpread1_Sheet1.SetValue(num2, 0, "┛");
                }
                if (!(flag || !flag2))
                {
                    this.neuSpread1_Sheet1.SetValue(num2, 0, "┓");
                }
                if (!(flag || flag2))
                {
                    this.neuSpread1_Sheet1.SetValue(num2, 0, "");
                }
            }
            for (num2 = 0; num2 < count; num2++)
            {
                if (this.neuSpread1_Sheet1.Cells[num2, 0].Tag == "")
                {
                    this.neuSpread1_Sheet1.SetValue(num2, 0, "");
                }
            }
            if (count == 1)
            {
                this.neuSpread1_Sheet1.SetValue(0, 0, "");
            }
            if ((count == 2) && (this.neuSpread1_Sheet1.Cells[0, 0].Tag != this.neuSpread1_Sheet1.Cells[1, 0].Tag))
            {
                this.neuSpread1_Sheet1.SetValue(0, 0, "");
                this.neuSpread1_Sheet1.SetValue(1, 0, "");
            }
            if (count > 2)
            {
                if ((this.neuSpread1_Sheet1.GetValue(1, 0).ToString() != "┃") && (this.neuSpread1_Sheet1.GetValue(1, 0).ToString() != "┛"))
                {
                    this.neuSpread1_Sheet1.SetValue(0, 0, "");
                }
                if ((this.neuSpread1_Sheet1.GetValue(count - 2, 0).ToString() != "┃") && (this.neuSpread1_Sheet1.GetValue(count - 2, 0).ToString() != "┓"))
                {
                    this.neuSpread1_Sheet1.SetValue(count - 1, 0, "");
                }
            }
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                if ((this.neuSpread1_Sheet1.Cells[i, 0].Text.Trim() == "┛") || string.IsNullOrEmpty(this.neuSpread1_Sheet1.Cells[i, 0].Text.Trim()))
                {
                    this.neuSpread1_Sheet1.Rows[i].Border = border;
                }
                else
                {
                    this.neuSpread1_Sheet1.Rows[i].Border = border2;
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

        private void ucPrintItinerateLarge_Load(object sender, EventArgs e)
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

