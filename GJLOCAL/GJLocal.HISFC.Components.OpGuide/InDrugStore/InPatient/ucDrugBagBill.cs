using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GJLocal.HISFC.Components.OpGuide.InDrugStore.InPatient
{
    public partial class ucDrugBagBill : UserControl, FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IInpatientBill
    {
        public ucDrugBagBill()
        {
            InitializeComponent();
            this.PrintDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(PrintDocumentPrintPage);
        }

        /// <summary>
        /// 打印用
        /// </summary>
        private System.Drawing.Printing.PrintDocument PrintDocument = new System.Drawing.Printing.PrintDocument();

        private System.Drawing.Printing.Margins DrawingMargins = new System.Drawing.Printing.Margins(20, 20, 10, 30);

        FS.SOC.Windows.Forms.PrintPageSelectDialog socPrintPageSelectDialog = new FS.SOC.Windows.Forms.PrintPageSelectDialog();

        #region IInpatientBill 成员

        public FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.InpatientBillType InpatientBillType
        {
            get { throw new NotImplementedException(); }
        }


        public void Init()
        {
            this.Clear();
        }

        /// <summary>
        /// 提供没有范围选择的打印
        /// 一般在摆药保存时调用
        /// </summary>
        /// <param name="alData"></param>
        /// <param name="drugBillClass"></param>
        /// <param name="stockDept"></param>
        public void PrintData(System.Collections.ArrayList alData, FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass, FS.FrameWork.Models.NeuObject stockDept)
        {
            this.ShowBillData(alData, drugBillClass, stockDept);
            this.PrintPage();
        }

        /// <summary>
        /// 这实在没有意义，和汇总单统一罢了
        /// </summary>
        /// <param name="alData"></param>
        /// <param name="drugBillClass"></param>
        /// <param name="stockDept"></param>
        private void ShowBillData(System.Collections.ArrayList alData, FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass, FS.FrameWork.Models.NeuObject stockDept)
        {
            this.Clear();
            this.ShowDetailData(alData, drugBillClass, stockDept);
        }

        /// <summary>
        /// 数据显示
        /// </summary>
        /// <param name="alData"></param>
        /// <param name="drugBillClass"></param>
        private void ShowDetailData(System.Collections.ArrayList alData, FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass, FS.FrameWork.Models.NeuObject stockDept)
        {
            FS.HISFC.BizLogic.RADT.InPatient inpatientManager = new FS.HISFC.BizLogic.RADT.InPatient();
            DateTime dt = inpatientManager.GetDateTimeFromSysDateTime();
            FS.HISFC.Models.Pharmacy.ApplyOut obj = (alData[0] as FS.HISFC.Models.Pharmacy.ApplyOut);
            FS.HISFC.Models.RADT.PatientInfo p = inpatientManager.GetPatientInfoByPatientNO(obj.PatientNO);
            this.lbDept.Text = FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(drugBillClass.ApplyDept.ID);
            this.lbBedNo.Text = obj.BedNO;
            this.lbName.Text = obj.PatientName;
            this.lbPatientNo.Text = p.PID.PatientNO;
            this.lbPoDate.Text = obj.UseTime.ToShortDateString();
            this.lbPoTime.Text = obj.UseTime.ToShortTimeString();
            //this.lbRecipe.Text = "";
            this.lbBillNo.Text = drugBillClass.DrugBillNO;
            this.lbPrintTime.Text = DateTime.Now.ToString();
            int i = 1;

            foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in alData)
            {
                FS.HISFC.Models.Pharmacy.Item item = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(info.Item.ID);
                string doseOnce = "";
                if (((info.DoseOnce / item.BaseDose) - (int)(info.DoseOnce / item.BaseDose)) > 0)
                {
                    doseOnce = info.DoseOnce + info.Item.DoseUnit;
                }
                else
                {
                    doseOnce = ((int)(info.DoseOnce / item.BaseDose)).ToString() + info.Item.MinUnit;
                }
                //{C3AC1F52-230E-4491-A24E-52A9A28E2302}
                //this.lbRecipe.Text = this.lbRecipe.Text + i.ToString() + "." + info.Item.Name + item.BaseDose + info.Item.DoseUnit + "/" + info.Item.MinUnit + "    " + "×" + doseOnce + "\n\r";
              //{E2613CC6-9F59-48b2-9299-D3814C6254A7}
                FS.HISFC.Models.Pharmacy.Item itemInfo = new FS.HISFC.Models.Pharmacy.Item();
                itemInfo = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(info.Item.ID);
                //{E72DE908-1189-4feb-AE2E-4F44F51C5120}
                this.lbRecipe.Text = this.lbRecipe.Text + i + ". "+info.Item.Name + item.BaseDose + info.Item.DoseUnit + "/" + info.Item.MinUnit + "    " + "×" + doseOnce + "\n\r" + "备注：" + itemInfo.Memo + "/" + info.Memo + "\n\r";
                i++;// {F9B890A9-D02C-4e38-BB39-F64251AF8F64}
            }
        }

        /// <summary>
        /// 打印
        /// </summary>
        private void PrintPage()
        {
            System.Drawing.Printing.PaperSize paperSize = null;
            this.SetPaperSize(paperSize);
            if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
            {
                if (this.ChoosePrintPageNO(Graphics.FromHwnd(this.Handle)))
                {
                    this.PrintView();
                }
            }
            else
            {
                if (this.ChoosePrintPageNO(Graphics.FromHwnd(this.Handle)))
                {
                    this.SetPaperSize(paperSize);
                    this.PrintDocument.Print();
                }
            }

        }


        private void SetPaperSize(System.Drawing.Printing.PaperSize paperSize)
        {
            FS.HISFC.BizLogic.Manager.PageSize pageSizeMgr = new FS.HISFC.BizLogic.Manager.PageSize();
            FS.HISFC.Models.Base.PageSize paperSize1 = pageSizeMgr.GetPageSize("InPatientDrugBagBill");
            paperSize = new System.Drawing.Printing.PaperSize("", paperSize1.Width, paperSize1.Height);
            
            //this.PrintDocument.
            this.PrintDocument.PrinterSettings.PrinterName = "InPatientDrugBagBill";
            this.PrintDocument.DefaultPageSettings.PrinterSettings.PrinterName = "InPatientDrugBagBill";
            this.PrintDocument.DefaultPageSettings.Landscape = true;
            this.PrintDocument.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize(paperSize.PaperName, paperSize.Width, paperSize.Height);
            this.PrintDocument.PrinterSettings.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize(paperSize.PaperName, paperSize.Width, paperSize.Height);
        }


        public void Print()
        {
            this.PrintPage();
        }

        public void ShowData(System.Collections.ArrayList alData, FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass, FS.FrameWork.Models.NeuObject stockDept)
        {
            this.Clear();
            this.ShowBillData(alData, drugBillClass, stockDept);
        }

        public DockStyle WinDockStyle
        {
            get
            {
                return this.Dock;
            }
            set
            {
                this.Dock = value;
            }
        }

        /// <summary>
        /// 打印页码选择
        /// </summary>
        private bool ChoosePrintPageNO(Graphics graphics)
        {

            return true;
        }


        public void Clear()
        {
            this.lbDept.Text = "";
            this.lbBedNo.Text = "";
            this.lbName.Text = "";
            this.lbPatientNo.Text = "";
            this.lbPoDate.Text = "";
            this.lbPoTime.Text = "";
            this.lbRecipe.Text = "";
            this.lbBillNo.Text = "";
            this.lbPrintTime.Text = "";
        }


        protected void PrintView()
        {
            this.SetPaperSize(null);
            this.myPrintView();
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

        private void PrintDocumentPrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Graphics graphics = e.Graphics;
            graphics.DrawString(this.lbDept.Text, this.lbDept.Font, new SolidBrush(this.lbDept.ForeColor),  this.lbDept.Location.X, this.lbDept.Location.Y);
            graphics.DrawString(this.lbBedNo.Text, this.lbBedNo.Font, new SolidBrush(this.lbBedNo.ForeColor),  this.lbBedNo.Location.X, this.lbBedNo.Location.Y);
            graphics.DrawString(this.lbName.Text, this.lbName.Font,new SolidBrush(this.lbName.ForeColor),  this.lbName.Location.X,this.lbName.Location.Y);
            graphics.DrawString(this.lbPatientNo.Text, this.lbPatientNo.Font, new SolidBrush(this.lbPatientNo.ForeColor),  this.lbPatientNo.Location.X, this.lbPatientNo.Location.Y);
            graphics.DrawString(this.lbPoDate.Text, this.lbPoDate.Font, new SolidBrush(this.lbPoDate.ForeColor), this.lbPoDate.Location.X,  this.lbPoDate.Location.Y);
            graphics.DrawString(this.lbPoTime.Text, this.lbPoTime.Font, new SolidBrush(this.lbPoTime.ForeColor),  this.lbPoTime.Location.X,this.lbPoTime.Location.Y);
            //{C3AC1F52-230E-4491-A24E-52A9A28E2302}
            graphics.DrawString(this.lbRecipe.Text, this.lbRecipe.Font, new SolidBrush(this.lbRecipe.ForeColor), new RectangleF(this.lbRecipe.Location.X, this.lbRecipe.Location.Y,this.lbRecipe.Width,this.lbRecipe.Height));
            graphics.DrawString(this.lbIndex.Text, this.lbIndex.Font, new SolidBrush(this.lbIndex.ForeColor), this.lbIndex.Location.X, this.lbIndex.Location.Y);
            graphics.DrawString(this.lbBillNo.Text, this.lbBillNo.Font, new SolidBrush(this.lbBillNo.ForeColor), this.lbBillNo.Location.X, this.lbBillNo.Location.Y);
            graphics.DrawString(this.lbPrintTime.Text, this.lbPrintTime.Font, new SolidBrush(this.lbPrintTime.ForeColor), this.lbPrintTime.Location.X, this.lbPrintTime.Location.Y);
        }
        #endregion
    }
}
