using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Management;

namespace FS.SOC.Local.DrugStore.FuYou.Outpatient
{
    public partial class ucDrugBill : UserControl
    {
        #region 变量
        /// <summary>
        /// 当前显示组号
        /// </summary>
        int showGroupNO = 0;

        /// <summary>
        /// 当前记录的组合号
        /// </summary>
        string rememberComboNO = "";

        /// <summary>
        /// 合并第一列单元格的起始行号
        /// </summary>
        int spanRowIndex = 1;
        #endregion

        public ucDrugBill()
        {
            InitializeComponent();
            this.neuSpread1_Sheet1.ColumnHeaderVisible = true;
        }

        /// <summary>
        /// 清屏操作
        /// </summary>
        public void Clear()
        {
            this.lbName.Text = "";
            this.lbSex.Text = "";
            this.lbAge.Text = "";
            this.lbDate.Text = "";
            this.lbDept.Text = "";
            this.lbDoct.Text = "";

            this.neuSpread1_Sheet1.Rows.Count = 0;
        }

        /// <summary>
        /// 打印
        /// </summary>
        public void PrintPage()
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            print.SetPageSize(new FS.HISFC.Models.Base.PageSize("OutPatientDrugBill", 860, 1110 / 3));
            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            print.IsDataAutoExtend = false;
            if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
            {
                print.PrintPreview(5, 5, this);
            }
            else
            {
                print.PrintPage(5, 5, this);
            }
        }

        /// <summary>
        /// 将数据加载到FarPoint
        /// </summary>
        /// <param name="al"></param>
        /// <param name="drugRecipe"></param>
        private void ShowBillData(ArrayList al, FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe)
        {
            FS.HISFC.BizLogic.HealthRecord.Diagnose diagMgr = new FS.HISFC.BizLogic.HealthRecord.Diagnose();
            FS.FrameWork.Management.DataBaseManger db = new DataBaseManger();
            FS.HISFC.BizProcess.Integrate.Order orderMgr = new FS.HISFC.BizProcess.Integrate.Order();            
            FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItem = new FS.HISFC.Models.Fee.Outpatient.FeeItemList();
            FS.HISFC.BizLogic.Fee.Outpatient feeMgr = new FS.HISFC.BizLogic.Fee.Outpatient();
            
            decimal dCurPageDrugFee = 0.0M;
            decimal dTotFee = 0.0M;
            FarPoint.Win.LineBorder allBorder = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, true, true, true, true);
            FarPoint.Win.LineBorder topBorder = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1,false,true,false,false);

            //设置对齐
            for (int i = 0; i < this.neuSpread1_Sheet1.Columns.Count; i++)
            {
                if (i==1)
                {
                    this.neuSpread1_Sheet1.Columns[i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;    
                }
                else
                {
                    this.neuSpread1_Sheet1.Columns[i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                }
                
                this.neuSpread1_Sheet1.Columns[i].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            }

            //设置标签
            this.lbName.Text = drugRecipe.PatientName;
            this.lbSex.Text = drugRecipe.Sex.Name;
            this.lbAge.Text = db.GetAge(drugRecipe.Age,true);
            this.lbDate.Text = drugRecipe.FeeOper.OperTime.ToString();
            this.lbDept.Text = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(drugRecipe.DoctDept.ID);
            this.lbDoct.Text = SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(drugRecipe.Doct.ID);
            this.lblInvoiceNO.Text = drugRecipe.InvoiceNO;
            this.nlbRePrint.Visible = !(drugRecipe.RecipeState == "0");

            ArrayList diagList = diagMgr.QueryCaseDiagnoseForClinic(drugRecipe.ClinicNO, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC);
            if (diagList != null && diagList.Count > 0)
            {
                FS.HISFC.Models.HealthRecord.Diagnose diag = diagList[0] as FS.HISFC.Models.HealthRecord.Diagnose;
                this.lblDiagnose.Text = diag.DiagInfo.ICD10.Name;
            }

            ArrayList balanceList = feeMgr.QueryBalancesValidByInvoiceNO(drugRecipe.InvoiceNO);
            if (balanceList != null && balanceList.Count > 0)
            {
                FS.HISFC.Models.Fee.Outpatient.Balance balance = balanceList[0] as FS.HISFC.Models.Fee.Outpatient.Balance;
                dTotFee = balance.FT.TotCost;
            }

            int iIndex = 0;
            //循环添加数据
            
            foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in al)
            {
                FS.HISFC.Models.Order.OutPatient.Order order = orderMgr.GetOneOrder(info.PatientNO, info.OrderNO);             

                //修改组号
                this.neuSpread1_Sheet1.RowCount++;
                if (info.CombNO != rememberComboNO)
                {
                    rememberComboNO = info.CombNO;
                    spanRowIndex = iIndex;
                    showGroupNO++;
                    this.neuSpread1_Sheet1.Rows[spanRowIndex].Border = topBorder;
                    this.neuSpread1_Sheet1.Cells[spanRowIndex, 0].Border = allBorder;
                }
                else
                {
                    this.neuSpread1_Sheet1.Cells[spanRowIndex, 0].RowSpan = iIndex - spanRowIndex + 1;
                }
                
                //分组
                this.neuSpread1_Sheet1.SetValue(iIndex, 0, showGroupNO);
                
                //药品名称
                this.neuSpread1_Sheet1.SetValue(iIndex, 1, info.Item.Name);
                
                //总量
                this.neuSpread1_Sheet1.SetValue(iIndex, 2, info.Operation.ApplyQty.ToString() + info.Item.MinUnit);
                
                //每次量
                if (order!=null)
                {
                    this.neuSpread1_Sheet1.SetValue(iIndex, 3, order.DoseOnceDisplay.ToString() + order.DoseUnit);
                }
                else
                {
                    this.neuSpread1_Sheet1.SetValue(iIndex, 3, info.DoseOnce.ToString() + info.Item.DoseUnit);
                }
                
                //频次
                this.neuSpread1_Sheet1.SetValue(iIndex, 4, info.Frequency.ID);

                //天数
                if (order != null)
                {
                    this.neuSpread1_Sheet1.SetValue(iIndex, 5, order.HerbalQty);
                }
               
                //用法
                this.neuSpread1_Sheet1.SetValue(iIndex, 6, info.Usage.Name);

                //规格
                this.neuSpread1_Sheet1.SetValue(iIndex, 7, info.Item.Specs);
               
                //累计费用总额
                if (info.Item.PackQty==0)
                {
                    info.Item.PackQty = 1;
                }
                dCurPageDrugFee +=FS.FrameWork.Public.String.FormatNumber(info.Operation.ApplyQty * info.Item.Price/info.Item.PackQty,2);


                //口服药加粗显示
                if (SOC.HISFC.BizProcess.Cache.Common.IsPOUsage(info.Usage.ID))
                {
                    this.neuSpread1_Sheet1.Rows[iIndex].Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
                }
                else
                {
                    this.neuSpread1_Sheet1.Rows[iIndex].Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
                }

                iIndex++;
            }
            
            //增加总计行
            this.neuSpread1_Sheet1.Rows.Add(iIndex, 1);
            iIndex = this.neuSpread1_Sheet1.Rows.Count - 1;
            this.neuSpread1_Sheet1.Rows[iIndex].Border = topBorder;
            this.neuSpread1_Sheet1.Cells[iIndex, 0].ColumnSpan = this.neuSpread1_Sheet1.ColumnCount;
            this.neuSpread1_Sheet1.Cells[iIndex, 0].Text = "总费用：" + dTotFee+"元            当前页药费：" + dCurPageDrugFee.ToString() + "元                 配药员：                核对员：                         ";

        }

        #region by cube
        public void PrintDrugBill(System.Collections.ArrayList alData, FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe, FS.HISFC.Models.Pharmacy.DrugTerminal drugTerminal)
        {
            this.Clear();
          
                if (alData.Count > 0)
                {
                    for (int index = alData.Count - 1; index > -1; index--)
                    {
                        FS.HISFC.Models.Pharmacy.ApplyOut applyOut = alData[index] as FS.HISFC.Models.Pharmacy.ApplyOut;

                        FS.HISFC.Models.Order.OutPatient.Order order = FS.SOC.Local.DrugStore.Common.Function.GetOrder(applyOut.OrderNO);
                        if (order != null)
                        {
                            applyOut.CombNO = order.SubCombNO.ToString();
                        }
                        else
                        {
                            applyOut.CombNO = "";
                            return; //手工方，不打印
                        }
                    }

                    //按处方排序
                    alData.Sort(new CompareApplyOutByCombNO());
                }

                this.ShowBillData(alData, drugRecipe);
                this.PrintPage();
            
        }
        #endregion

    }
}
