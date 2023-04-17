using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GJLocal.HISFC.Components.OpGuide.InDrugStore.InPatient.IBORN
{
    public partial class ucReicpeInfoLabelIBORN : UserControl
    {
        public ucReicpeInfoLabelIBORN()
        {
            InitializeComponent();
        }

        FS.HISFC.BizLogic.Manager.PageSize pageSizeMgr = new FS.HISFC.BizLogic.Manager.PageSize();
        FS.HISFC.Models.Base.PageSize pageSize;
        FS.HISFC.BizProcess.Integrate.Registration.Registration regMgr = new FS.HISFC.BizProcess.Integrate.Registration.Registration();
        FS.HISFC.BizLogic.RADT.InPatient inpatientManager = new FS.HISFC.BizLogic.RADT.InPatient();
            
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
                    pageSize = new FS.HISFC.Models.Base.PageSize("OutPatientDrugLabel", 320, 138);
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
        public int LengthString(string str)
        {
            if (str == null || str.Length == 0) { return 0; }

            int l = str.Length;
            int realLen = l;

            #region 计算长度
            int clen = 0;//当前长度
            while (clen < l)
            {
                //每遇到一个中文，则将实际长度加一。
                if ((int)str[clen] > 128) { realLen++; }
                clen++;
            }
            #endregion

            return realLen;
        }
        /// <summary>
        /// 打印标签
        /// </summary>
        /// <param name="alData"></param>
        /// <param name="drugRecipe"></param>
        /// <param name="drugTerminal"></param>
        /// <returns></returns>
        public int PrintDrugLabel(System.Collections.ArrayList alData, FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass, FS.FrameWork.Models.NeuObject stockDept)
        {
            if (alData == null)
            {
                return -1;
            }
            int index = 1;
            foreach (FS.HISFC.Models.Pharmacy.ApplyOut applyInfo in alData)
            {
                this.Clear();
                if (FS.FrameWork.Function.NConvert.ToInt32(drugBillClass.ApplyState) != 0)
                {
                    this.nlbRePrint.Visible = true;
                }
                else
                {
                    this.nlbRePrint.Visible = false;
                }
                FS.HISFC.Models.Pharmacy.Item item = new FS.HISFC.Models.Pharmacy.Item();
                item = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(applyInfo.Item.ID);
                FS.HISFC.Models.RADT.PatientInfo p = inpatientManager.GetPatientInfoByPatientNO(applyInfo.PatientNO);

                this.lbPatientName.Text = p.Name;
                this.lbPatientName1.Text = p.Name;
                int strLength = this.LengthString(p.Name);
                if (strLength > 14)
                {
                    //{8665E214-D0A2-4e86-8EDE-ABF72F2E762E}
                    this.lbPatientName.Visible = true;
                    //{7A60D820-7DD5-48c8-95C1-F4E52B0C76C1}
                    this.lbPatientName.Text = p.Name.Substring(0, 14 > p.Name.Length ? p.Name.Length:14  ) + "***";
                    this.lbPatientName1.Visible = false;
                }
                else
                {

                    this.lbPatientName.Visible = false;
                    this.lbPatientName1.Visible = true;
                }
                //{2BC933DF-20D2-4566-9AC1-726B90292CF3}
                this.nlbPatientAge.Text = p.Sex.Name + " " + (new FS.FrameWork.Management.DataBaseManger()).GetAge(p.Birthday) + " " + applyInfo.BedNO.Substring(4,applyInfo.BedNO.Length - 4) + "床";
                this.lblMemo.Text = "注意事项：" + item.Memo +" "+applyInfo.Memo; // {54A0035F-EA64-4d51-AA0A-710576DBCE7F}
                //this.nlbSendTerminal.Text = drugTerminal.Name;
                this.nlbDrugName.Text = applyInfo.Item.Name;
                this.nlbSpecs.Text = applyInfo.Item.Specs;
                bool isInjectUsage = FS.SOC.HISFC.BizProcess.Cache.Common.IsInjectUsage(applyInfo.Usage.ID);

                this.nlbUsageName.Text = applyInfo.Usage.Name + "，" + Common.Function.GetFrequenceName(applyInfo.Frequency) + "，" + "每次" + Common.Function.GetOnceDose(applyInfo);

               
                this.nlbPageNO.Text = index + "-" + alData.Count;
                this.lblPrintTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm");

                if (drugBillClass.ID == "R")
                {
                    this.nlbName.Text = "退药单";
                }
                else if (drugBillClass.ID == "O")
                {
                    this.nlbName.Text = "出院带药";
                }
                else
                {
                    this.nlbName.Text = "住院取药";
                }
                this.nlbPlaceNo.Text = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItemPlaceNo(applyInfo.StockDept.ID, applyInfo.Item.ID);//货位号
                this.nlbStockDept.Text = FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(applyInfo.StockDept.ID);
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
                this.nlbTotQty.Text = "×" + applyPackQty;
                //this.npbCardNo.Image = this.CreateBarCode(p.PID.PatientNO);
                //{2BC933DF-20D2-4566-9AC1-726B90292CF3}
                //this.lbRecipeNo.Text = applyInfo.OrderNO;
                this.lbRecipeNo.Text = applyInfo.DrugNO;
                index++;
                this.Print();

            }
           
            return 1;
        }

    }
}
