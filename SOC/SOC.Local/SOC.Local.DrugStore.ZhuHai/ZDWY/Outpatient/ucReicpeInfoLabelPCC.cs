using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.Local.DrugStore.ZhuHai.ZDWY.Outpatient
{
    public partial class ucReicpeInfoLabelPCC : UserControl
    {
        public ucReicpeInfoLabelPCC()
        {
            InitializeComponent();
        }

        FS.HISFC.BizLogic.Manager.PageSize pageSizeMgr = new FS.HISFC.BizLogic.Manager.PageSize();
        FS.HISFC.Models.Base.PageSize pageSize;

        /// <summary>
        /// 清除显示信息
        /// </summary>
        private void Clear()
        {
            foreach (Control c in this.Controls)
            {
                if ((c is Label))
                {
                    if (c.Name == "nlbHospitalInfo" || c.Name == "nlbPhone" || c.Name == "label1"||c.Name == this.label2.Name)
                    {
                        continue;
                    }
                    (c as Label).Text = string.Empty;
                }
                if(c is FS.FrameWork.WinForms.Controls.NeuPictureBox)
                { 
                    (c as FS.FrameWork.WinForms.Controls.NeuPictureBox).Image = null;
                }
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
            b.IncludeLabel = true;
            b.Alignment = Align;
            return b.Encode(type, code, System.Drawing.Color.Black, System.Drawing.Color.White, 150, 50);
        }

        /// <summary>
        /// 打印
        /// </summary>
        private void Print()
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            if (pageSize == null)
            {
                pageSize = pageSizeMgr.GetPageSize("OutPatientDrugLabel");
                if (pageSize != null && pageSize.Printer.ToLower() == "default")
                {
                    pageSize.Printer = "";
                }
                if (pageSize == null)
                {
                    pageSize = new FS.HISFC.Models.Base.PageSize("OutPatientDrugLabel", 315, 138);
                }
            }
            print.SetPageSize(pageSize);
            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            print.IsDataAutoExtend = false;

            if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
            {
                print.PrintPreview(0, 0, this);
            }
            else
            {
                print.PrintPage(0, 0, this);
            }
        }

        /// <summary>
        /// 打印标签
        /// </summary>
        /// <param name="alData"></param>
        /// <param name="drugRecipe"></param>
        /// <param name="drugTerminal"></param>
        /// <returns></returns>
        public int PrintDrugLabel(System.Collections.ArrayList alData, FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe, FS.HISFC.Models.Pharmacy.DrugTerminal drugTerminal, bool isPrintRegularName, string hospitalName, bool isPrintMemo, string diagnose, DateTime printTime)
        {
            if (alData == null || drugRecipe == null || drugTerminal == null)
            {
                return -1;
            }

            printTime = this.pageSizeMgr.GetDateTimeFromSysDateTime();
           
          
                this.Clear();

                FS.HISFC.Models.Pharmacy.ApplyOut applyInfo = alData[0] as FS.HISFC.Models.Pharmacy.ApplyOut;
               
                this.lbPatientName.Text = drugRecipe.PatientName;
                this.nlbPatientAge.Text = drugRecipe.Sex.Name + " " + (new FS.FrameWork.Management.DataBaseManger()).GetAge(drugRecipe.Age);
                this.nlbDocDept.Text = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(drugRecipe.DoctDept.ID);
                this.nlbRePrint.Visible = !(drugRecipe.RecipeState == "0");
                this.nlbPrintTime.Text = printTime.Hour.ToString().PadLeft(2, '0') + ":" + printTime.Minute.ToString().PadLeft(2, '0');
                this.nlbSendTerminal.Text = drugTerminal.Name;
                bool isInjectUsage = SOC.HISFC.BizProcess.Cache.Common.IsInjectUsage(applyInfo.Usage.ID);
                this.nlbStockDept.Text = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(applyInfo.StockDept.ID);
                this.nlbHospitalInfo.Text = (new FS.FrameWork.Management.DataBaseManger()).Hospital.Name;
                this.npbRecipeNO.Image = this.CreateBarCode(applyInfo.RecipeNO);
                this.Print();

            

            return 1;
        }
    }
}
