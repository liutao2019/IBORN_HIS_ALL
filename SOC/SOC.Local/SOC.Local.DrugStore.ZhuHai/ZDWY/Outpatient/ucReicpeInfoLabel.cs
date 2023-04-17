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
    public partial class ucReicpeInfoLabel : UserControl
    {
        public ucReicpeInfoLabel()
        {
            InitializeComponent();
        }

        FS.HISFC.BizLogic.Manager.PageSize pageSizeMgr = new FS.HISFC.BizLogic.Manager.PageSize();
        FS.HISFC.Models.Base.PageSize pageSize;
        FS.HISFC.BizProcess.Integrate.Registration.Registration regMgr = new FS.HISFC.BizProcess.Integrate.Registration.Registration();

        /// <summary>
        /// 清除显示信息
        /// </summary>
        private void Clear()
        {
            foreach (Control c in this.Controls)
            {
                if ((c is Label))
                {
                    if (c.Name == "nlbHospitalInfo" || c.Name == "nlbPhone" || c.Name == "nlbValueable")
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
            int index = 1;
            foreach (FS.HISFC.Models.Pharmacy.ApplyOut applyInfo in alData)
            {
                this.Clear();

                FS.HISFC.Models.Registration.Register regInfo = regMgr.GetByClinic(drugRecipe.ClinicNO);
                if (regInfo.PatientType == "2")
                {
                    this.lbPatientName.Text = "(V)";
                }
                else
                {
                    this.lbPatientName.Text = string.Empty;
                }
                this.lbPatientName.Text += drugRecipe.PatientName;
                this.nlbPatientAge.Text = drugRecipe.Sex.Name + " " + (new FS.FrameWork.Management.DataBaseManger()).GetAge(drugRecipe.Age);
                this.nlbDocDept.Text = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(drugRecipe.DoctDept.ID);
                this.nlbRePrint.Visible = !(drugRecipe.RecipeState == "0");
                this.nlbPrintTime.Text = printTime.Hour.ToString().PadLeft(2, '0') + ":" + printTime.Minute.ToString().PadLeft(2, '0');
                this.nlbInvoiceNO.Text = drugRecipe.InvoiceNO;
                this.nlbSendTerminal.Text = drugTerminal.Name;
                this.nlbDrugName.Text = applyInfo.Item.Name;
                this.nlbSpecs.Text = applyInfo.Item.Specs;
                bool isInjectUsage = SOC.HISFC.BizProcess.Cache.Common.IsInjectUsage(applyInfo.Usage.ID);
                if (!isInjectUsage)
                {
                    this.nlbUsageName.Text = applyInfo.Usage.Name;
                    this.nlbFrequencyName.Text = Common.Function.GetFrequenceName(applyInfo.Frequency);
                    this.nlbOnceDose.Text = Common.Function.GetOnceDose(applyInfo) + "/次";
                }
               
                this.nlbPageNO.Text = index + "-" + alData.Count;
           

                this.nlbStockDept.Text = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(applyInfo.StockDept.ID);
                this.nlbHospitalInfo.Text = (new FS.FrameWork.Management.DataBaseManger()).Hospital.Name;
                //数量显示处理
                string applyPackQty = "";
                string qtyShowType = "包装单位";
                if (qtyShowType == "包装单位")
                {
                    int applyQtyInt = 0;//这个取得商，是整包装单位的量，必须是整数
                    decimal applyRe = 0;//这个取得余数，是最小单位的量，可能是小数
                    applyQtyInt = (int)(applyInfo.Operation.ApplyQty / applyInfo.Item.PackQty);
                    applyRe = applyInfo.Operation.ApplyQty - applyQtyInt * applyInfo.Item.PackQty;
                    if (applyQtyInt > 0)
                    {
                        applyPackQty += applyQtyInt.ToString() + applyInfo.Item.PackUnit;
                    }
                    if (applyRe > 0)
                    {
                        applyPackQty += applyRe.ToString("F4").TrimEnd('0').TrimEnd('.') + applyInfo.Item.MinUnit;
                    }
                }
                else
                {
                    applyPackQty = (applyInfo.Operation.ApplyQty).ToString("F4").TrimEnd('0').TrimEnd('.') + applyInfo.Item.MinUnit;
                }
                this.nlbTotQty.Text = applyPackQty;
                this.npbRecipeNO.Image = this.CreateBarCode(drugRecipe.RecipeNO);
                this.nlbDocName.Text = "医生:" + SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(drugRecipe.Doct.ID);

                this.nlbDiagnose.Text = "诊断:" + diagnose;
                index++;
                this.Print();

            }
           
            return 1;
        }

    }
}
