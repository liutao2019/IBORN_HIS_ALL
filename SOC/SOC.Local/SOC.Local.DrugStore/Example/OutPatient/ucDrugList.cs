using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
//using FS.FrameWork.Management;
using System.Collections;
//using FS.HISFC.BizLogic.Pharmacy;
//using FS.HISFC.Models.Pharmacy;
//using FS.HISFC.Models.HealthRecord.EnumServer;

namespace FS.SOC.Local.DrugStore.Example.Outpatient
{
    public partial class ucDrugList : UserControl
    {
        /// <summary>
        /// [功能描述: 门诊药房打印本地化实例]<br></br>
        /// [创 建 者: cube]<br></br>
        /// [创建时间: 2011-1]<br></br>
        /// 说明：
        /// 1、增强的分页处理，页码显示处理
        /// 2、直观的FarPoint列设置，可以根据列名称赋Cell的值
        /// </summary>>
        public ucDrugList()
        {
            InitializeComponent();
        }

        private void PrintAllData(System.Collections.ArrayList al, FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe, string hospitalName)
        {
            
            decimal totCost = 0;
            int rowIndex = 0;
            this.Clear();
            
            SetLableValue(drugRecipe, hospitalName);
            this.neuSpread1_Sheet1.RowCount = al.Count + 1;
            
            foreach ( FS.HISFC.Models.Pharmacy.ApplyOut info in al)
            {
                //自定义码
                this.neuSpread1.SetCellValue(0,rowIndex, "编码", SOC.HISFC.BizProcess.Cache.Pharmacy.GetItemCustomNO(info.Item.ID));
                //药品名
                this.neuSpread1.SetCellValue(0, rowIndex, "药品名称", info.Item.Name);
               
                this.neuSpread1.SetCellValue(0,rowIndex, "规格", info.Item.SpecialFlag1 + info.Item.Specs);
                //数量
                int outMinQty;
                int outPackQty = System.Math.DivRem((int)(info.Operation.ApplyQty * info.Days), (int)info.Item.PackQty, out outMinQty);
                if (outPackQty == 0)
                {
                    this.neuSpread1.SetCellValue(0,rowIndex, "数量", ((int)(info.Operation.ApplyQty * info.Days)).ToString("0.00"));
                    //单位
                    this.neuSpread1.SetCellValue(0,rowIndex, "单位", info.Item.MinUnit);
                }
                else if (outMinQty == 0)
                {
                    this.neuSpread1.SetCellValue(0, rowIndex, "数量", outPackQty.ToString("0.00"));
                    //单位
                    this.neuSpread1.SetCellValue(0, rowIndex, "单位", info.Item.PackUnit);
                }
                else
                {
                    this.neuSpread1.SetCellValue(0, rowIndex, "数量", ((int)(info.Operation.ApplyQty * info.Days)).ToString("0.00"));
                    //单位
                    this.neuSpread1.SetCellValue(0, rowIndex, "单位", info.Item.MinUnit);
                }

                //单价
                this.neuSpread1.SetCellValue(0, rowIndex, "单价", ((decimal)(info.Item.PriceCollection.RetailPrice / info.Item.PackQty)).ToString("0.00"));
                //金额
                this.neuSpread1.SetCellValue(0, rowIndex, "金额", ((decimal)((info.Item.PriceCollection.RetailPrice / info.Item.PackQty) * (info.Operation.ApplyQty * info.Days))).ToString("0.00"));
                totCost += FS.FrameWork.Function.NConvert.ToDecimal((info.Item.PriceCollection.RetailPrice * (info.Operation.ApplyQty * info.Days / info.Item.PackQty)).ToString("F2"));
                
                //用法
                this.neuSpread1.SetCellValue(0, rowIndex, "用法", SOC.HISFC.BizProcess.Cache.Common.GetUsageName(info.Usage.ID));
                //每次用量
                if (info.DoseOnce == 0)
                {
                    this.neuSpread1.SetCellValue(0, rowIndex, "每次用量", "遵医嘱");
                }
                else
                {
                    this.neuSpread1.SetCellValue(0, rowIndex, "每次用量", info.DoseOnce.ToString() + info.Item.DoseUnit);
                }

                if (!string.IsNullOrEmpty(info.Frequency.ID))
                {
                    this.neuSpread1.SetCellValue(0, rowIndex, "频次", info.Frequency.ID.ToLower());
                }
                this.neuSpread1.SetCellValue(0, rowIndex, "备注", "无");

                rowIndex++;
            }

            this.SetTotInfo(totCost, drugRecipe.SendTerminal.Name);

            try
            {
                //暂停：打印机针头或纸张太薄或太厚都可能引起暂停，恢复打印
                FS.FrameWork.WinForms.Classes.Print.ResumePrintJob(0);
            }
            catch { }

            //this.PaperName = "OutPatientDrugBill";
            //this.PageHeight = 1100/3;
            //this.PageWith = 860;
            //this.IsPrintPageBottom = true;
            //this.DrawingMargins = new System.Drawing.Printing.Margins(0, 0, 0, 10);

            //if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
            //{
            //    this.PrintView();
            //}
            //else
            //{
            //    this.PrintPage();
            //}
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
        /// 清除控件文字
        /// </summary>
        private void Clear()
        {
            lbName.Text = "姓名:";
            lbCardNo.Text = "病历卡号:";
            lbSex.Text = "性别:";
            lbDiagnose.Text = "诊断:";
            lbAge.Text = "年龄:";
            lbDeptName.Text = "科室名称:";
            lbInvoice.Text = "发票号:";
            lbRecipe.Text = "处方号:";
            lbDoctor.Text = "医师:";

            this.neuSpread1_Sheet1.RowCount = 0;
        }      

        /// <summary>
        /// 设置Lable的值
        /// </summary>
        /// <param name="info"></param>
        private void SetLableValue(FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe, string hospitalName)
        {

            this.neuPanName.Text = hospitalName + "药品清单";
            //姓名
            lbName.Text = "姓名：" + drugRecipe.PatientName;
            //病案号
            this.lbCardNo.Text = "病历卡号：" + drugRecipe.CardNO;
            //发票号
            lbInvoice.Text = "发票号：" + drugRecipe.InvoiceNO;
            //性别
            lbSex.Text = "性别：" + drugRecipe.Sex.Name;
            //年龄
            FS.FrameWork.Management.DataBaseManger db = new FS.FrameWork.Management.DataBaseManger();
            lbAge.Text = "年龄：" + db.GetAge(drugRecipe.Age);
            //医师
            this.lbDoctor.Text = "医师：" + SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(drugRecipe.Doct.ID);
            //处方号
            this.lbRecipe.Text = "处方号：" + drugRecipe.RecipeNO;
            //科室名称
            lbDeptName.Text = "科室名称：" + SOC.HISFC.BizProcess.Cache.Common.GetDeptName(drugRecipe.DoctDept.ID);
          
            //处方日期
            lbRecipeDate.Text = "处方日期：" + FS.FrameWork.Function.NConvert.ToDateTime(drugRecipe.FeeOper.OperTime).ToString("yyyy-MM-dd");
           
        }    

        /// <summary>
        /// 增加最后一行汇总信息
        /// </summary>
        private void SetTotInfo(decimal totCost, string sendWindows)
        {
            this.neuSpread1_Sheet1.Models.Span.Add(this.neuSpread1_Sheet1.RowCount-1, 0, 1, this.neuSpread1_Sheet1.ColumnCount);
            try
            {
                this.neuSpread1_Sheet1.SetText(this.neuSpread1_Sheet1.RowCount - 1, 0, string.Format("总药价：￥{0}                  {1}",
                           new object[] { totCost.ToString("0.00"), sendWindows }));
            }
            catch { }
            this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - 1, 0).Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - 1, 0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
        }       

        /// <summary>
        /// 打印配药清单
        /// </summary>
        /// <param name="alData">出库申请实体</param>
        /// <param name="diagnose">诊断</param>
        /// <param name="drugRecipe">处方调剂信息</param>
        /// <param name="drugTerminal">终端信息</param>
        /// <returns></returns>
        public int PrintDrugBill(ArrayList alData, string diagnose, FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe, FS.HISFC.Models.Pharmacy.DrugTerminal drugTerminal,string hospitalName)
        {
            if (alData == null || drugRecipe == null)
            {
                return -1;
            }
            
            //this.npbBarCode.Image = this.CreateBarCode(drugRecipe.RecipeNO);
            this.PrintAllData(alData, drugRecipe, hospitalName);

            return 0;
        }

       
    }
}
