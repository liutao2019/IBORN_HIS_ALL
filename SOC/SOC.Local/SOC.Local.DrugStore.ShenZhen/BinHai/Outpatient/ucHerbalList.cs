using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.DrugStore.ShenZhen.BinHai.Outpatient
{
    /// <summary>
    /// [功能描述: 门诊药房打印本地化实例:药品清单]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2010-12]<br></br>
    /// 说明：
    /// 1、根据中文查找列,赋值方便,同名称的列前后有空格，修改时注意
    /// 2、根据数据自动计算页码,分页方便
    /// 3、根据标题长度自动居中
    /// </summary>>
    public partial class ucHerbalList : UserControl
    {
        public ucHerbalList()
        {
            InitializeComponent();
        }
        FS.HISFC.Models.Base.PageSize pageSize = null;
        FS.HISFC.BizLogic.Manager.PageSize pageSizeMgr = new FS.HISFC.BizLogic.Manager.PageSize();

        /// <summary>
        /// 清空赋值的数据
        /// 对界面所有的label的Text赋值""
        /// </summary>
        /// <returns></returns>
        public int Clear()
        {
            foreach (Control c in this.Controls)
            {
                if (c is Label)
                {
                    c.Text = "";
                }
            }
            this.lbRePringFlag.Text = "补打";
            this.fpSpread1_Sheet1.RowCount = 0;

            return 1;
        }

        /// <summary>
        /// 设置药品信息
        /// </summary>
        /// <param name="applyOut"></param>
        /// <param name="isPrintRegularName"></param>
        /// <param name="isPrintMemo"></param>
        /// <param name="qtyShowType"></param>
        /// <returns></returns>
        private int SetDrugInfo
            (
            ArrayList alApplyOut,
            bool isPrintRegularName,
            bool isPrintMemo,
            string qtyShowType)
        {
            this.fpSpread1_Sheet1.RowCount = (alApplyOut.Count + 1) / 2 + 1;
            decimal days =0;
            decimal totCost = 0;
            for (int curIndex = 0; curIndex < alApplyOut.Count; )
            {

                FS.HISFC.Models.Pharmacy.ApplyOut applyOut1 = alApplyOut[curIndex] as FS.HISFC.Models.Pharmacy.ApplyOut;
                this.fpSpread1.SetCellValue(0, curIndex, "编码", SOC.HISFC.BizProcess.Cache.Pharmacy.GetItemCustomNO(applyOut1.Item.ID));
                this.fpSpread1.SetCellValue(0, curIndex, "名称", applyOut1.Item.Name);
                this.fpSpread1.SetCellValue(0, curIndex, "每剂量", applyOut1.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + applyOut1.Item.DoseUnit);
                this.fpSpread1.SetCellValue(0, curIndex, "总量", (applyOut1.Operation.ApplyQty / applyOut1.Days).ToString("F4").TrimEnd('0').TrimEnd('.') + applyOut1.Item.MinUnit);
                this.fpSpread1.SetCellValue(0, curIndex, "用法", SOC.HISFC.BizProcess.Cache.Common.GetUsageName(applyOut1.Usage.Name));
                totCost += applyOut1.Operation.ApplyQty / applyOut1.Item.PackQty * applyOut1.Item.PriceCollection.RetailPrice;
                
                //同名称的列前后有空格，修改时注意
                if (curIndex + 1 < alApplyOut.Count)
                {
                    FS.HISFC.Models.Pharmacy.ApplyOut applyOut2 = alApplyOut[curIndex + 1] as FS.HISFC.Models.Pharmacy.ApplyOut;
                    this.fpSpread1.SetCellValue(0, curIndex, " 编码 ", SOC.HISFC.BizProcess.Cache.Pharmacy.GetItemCustomNO(applyOut2.Item.ID));
                    this.fpSpread1.SetCellValue(0, curIndex, " 名称 ", applyOut2.Item.Name);
                    this.fpSpread1.SetCellValue(0, curIndex, " 每剂量 ", applyOut2.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + applyOut1.Item.DoseUnit);
                    this.fpSpread1.SetCellValue(0, curIndex, " 总量 ", (applyOut1.Operation.ApplyQty / applyOut1.Days).ToString("F4").TrimEnd('0').TrimEnd('.') + applyOut1.Item.DoseUnit);
                    this.fpSpread1.SetCellValue(0, curIndex, " 用法 ", SOC.HISFC.BizProcess.Cache.Common.GetUsageName(applyOut2.Usage.Name));
                    totCost += applyOut2.Operation.ApplyQty / applyOut2.Item.PackQty * applyOut2.Item.PriceCollection.RetailPrice;

                }
                days = applyOut1.Days;
                curIndex += 2;
            }

            this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.RowCount - 1, 0].Font = new Font(this.Font.FontFamily, this.Font.Size * this.fpSpread1_Sheet1.ZoomFactor + 2, FontStyle.Bold);
            this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.RowCount - 1, 0].Text = "共 " + days.ToString("F0") + " 剂";
            this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.RowCount - 1, 0].ColumnSpan = this.fpSpread1_Sheet1.ColumnCount - 1;
            this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.RowCount - 1, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;

            this.lbCost.Text = "总药价：" + totCost.ToString("F2");
            
            return 1;
        }

        /// <summary>
        /// 设置患者信息
        /// 同一张处方只设置一次
        /// </summary>
        /// <param name="drugRecipe"></param>
        /// <returns></returns>
        private int SetPatientInfo(FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe)
        {
            this.lbPatientInfo.Text = "姓名：" + drugRecipe.PatientName
                + "  " + drugRecipe.Sex.Name
                + "  " + SOC.Local.DrugStore.ShenZhen.Common.Function.GetAge(drugRecipe.Age)
                + "      " + SOC.Local.DrugStore.ShenZhen.Common.Function.GetPactUnitName(drugRecipe.PayKind.ID)
                + "";
            return 1;
        }

        /// <summary>
        /// 设置其他信息：诊断等
        /// 同一张处方只设置一次
        /// </summary>
        /// <param name="hospitalName"></param>
        /// <param name="diagnose"></param>
        /// <param name="drugRecipe"></param>
        /// <param name="windowName"></param>
        /// <returns></returns>
        private int SetOtherInfo(string hospitalName, string diagnose, FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe, string windowName)
        {
            this.lbTitle.Text = FS.FrameWork.Management.Connection.Hospital.Name +"配药清单";
            this.lbRecipeNO.Text = "方号：" + drugRecipe.RecipeNO;
            this.lbInvoiceNO.Text = "发票：" + drugRecipe.InvoiceNO;
            this.lbRePringFlag.Visible = (drugRecipe.RecipeState == "0");

            this.lbRecipeDeptName.Text = "科室：" + SOC.HISFC.BizProcess.Cache.Common.GetDeptName(drugRecipe.DoctDept.ID);
            this.lbRecipeDoctName.Text = "" + SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(drugRecipe.Doct.ID);
            this.lbDiagnose.Text = "诊断：" + diagnose;
            this.lbWindowName.Text = windowName;
            this.lbPrintTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            return 1;
        }

       
        /// <summary>
        /// 打印
        /// </summary>
        private void Print()
        {
            //启用华南打印基类打印
            FS.SOC.Windows.Forms.PrintExtendPaper print = new FS.SOC.Windows.Forms.PrintExtendPaper();


            //获取维护的纸张
            if (pageSize == null)
            {
                //在系统纸张设置中维护，OutPatientDrugList为编号，OutPatientDrugList为名称，使用默认打印机时打印机不维护
                //将维护的上边距作为下边距，这样控制页尾打印的空白，保证数据完全打印
                pageSize = pageSizeMgr.GetPageSize("OutPatientDrugList");
                //指定打印处理，default说明使用默认打印机的处理
                if (pageSize != null && pageSize.Printer.ToLower() == "default")
                {
                    pageSize.Printer = "";
                }
                //没有维护时默认一个纸张
                if (pageSize == null)
                {
                    pageSize = new FS.HISFC.Models.Base.PageSize("OutPatientDrugList", 850, 1100);
                }
            }

            //打印边距处理：将维护的上边距作为下边距，这样控制页尾打印的空白，保证数据完全打印
            print.DrawingMargins = new System.Drawing.Printing.Margins(pageSize.Left, 0, pageSize.Top, 0);

            //纸张处理
            print.PaperName = pageSize.Name;
            print.PaperHeight = pageSize.Height;
            print.PaperWidth = pageSize.Width;

            //打印机名称
            print.PrinterName = pageSize.Printer;

            //页码显示
            this.lbPageNO.Tag = "页码：{0}/{1}";
            print.PageNOControl = this.lbPageNO;

            //页眉控件，表示每页都打印
            print.HeaderControls.Add(this.paneltitle);
            //页脚控件，表示每页都打印
            print.FooterControls.Add(this.panelBottom);


            //不显示页码选择
            print.IsShowPageNOChooseDialog = false;

            this.SetUI();
           
            //管理员使用预览功能
            if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
            {
                print.PrintPageView(this);
            }
            else
            {
                print.PrintPage(this);
            }

        }

        /// <summary>
        /// 打印时设置UserInterface
        /// </summary>
        private void SetUI()
        {
            if (pageSize != null)
            {
                this.lbTitle.Location = new Point((pageSize.Width - this.lbTitle.PreferredWidth) / 2, this.lbTitle.Location.Y);
            }
            this.fpSpread1.SetActiveSkin(0, FS.SOC.Windows.Forms.FpSpread.EnumSkinType.简单一分线);

        }

        public int PrintDrugList
           (
           System.Collections.ArrayList alApplyOut,
           FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe,
           FS.HISFC.Models.Pharmacy.DrugTerminal drugTerminal,
           bool isPrintRegularName,
           bool isPrintMemo,
           string qtyShowType,
           string hospitalName,
           string diagnose
           )
        {
            if (alApplyOut == null || alApplyOut.Count == 0 || drugRecipe == null || drugTerminal == null)
            {
                return -1;
            }

            this.Clear();
            this.SetPatientInfo(drugRecipe);
            this.SetOtherInfo(hospitalName, diagnose, drugRecipe, drugTerminal.Name);
            this.SetDrugInfo(alApplyOut, isPrintRegularName, isPrintMemo, qtyShowType);
            this.Print();


            return 1;
        }

    }
}
