using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace FS.SOC.Local.OutpatientFee.ShenZhen.BinHai.IOutpatientGuide
{
    public partial class ucFeeDetailGuideList : UserControl
    {
        public ucFeeDetailGuideList()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 初始化
        /// </summary>
        private void Init(string strHospitalName,string strPatientName, ArrayList alFeeDetail)
        {
            this.neuLblHeader.Text = "            "+strHospitalName + "门诊费用清单";
            this.neuLblPatientName.Text = "姓名: "+strPatientName;
            this.neuLblDatetime.Text = DateTime.Now.ToShortDateString();
        }
        private void InitSPread()
        {

            FarPoint.Win.Spread.SheetView SheetView;
            SheetView = this.neuSp_FeeDetailList_Sheet1;
            SheetView.RowHeader.Columns.Default.Resizable = false;
            SheetView.RowCount = 0;
            SheetView.ColumnCount = 5;
            SheetView.Columns[0].Label = " ";
            SheetView.Columns[1].Label = @"费用/药品名称";
            SheetView.Columns[2].Label = "数量";
            SheetView.Columns[3].Label = "单价";
            SheetView.Columns[4].Label = "金额";
            FarPoint.Win.Spread.CellType.TextCellType TextCell = new FarPoint.Win.Spread.CellType.TextCellType();
            TextCell.Multiline = true;
            TextCell.WordWrap = true;
            SheetView.Columns[1].CellType = TextCell;    


           
        }
        private void AddFeeDetail(ArrayList alFeeDetail)
        {
            int i=0,j=0,Order=0;
            Decimal Total = 0;//合计
            FarPoint.Win.Spread.SheetView SheetView;
            SheetView = this.neuSp_FeeDetailList_Sheet1;
            string InvoiceType;
            InvoiceType = (alFeeDetail[0] as FS.HISFC.Models.Fee.Outpatient.FeeItemList).Invoice.Type.Name;  
            foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItemList in alFeeDetail)
            {
                //项目名称
                if (InvoiceType == feeItemList.Invoice.Type.Name)
                {
                    SheetView.RowCount++;
                    Order++;
                    SheetView.Cells[SheetView.RowCount - 1, 0].Text = Order.ToString();
                    SheetView.Cells[SheetView.RowCount - 1, 1].Text = feeItemList.Item.Name;
                    SheetView.Cells[SheetView.RowCount - 1, 2].Text = feeItemList.Item.Qty.ToString("F2")+feeItemList.Item.PriceUnit;
                    SheetView.Cells[SheetView.RowCount - 1, 3].Text = feeItemList.Item.Price.ToString("F2");
                    if (feeItemList.Item.PackQty <= 0)
                    {
                        feeItemList.Item.PackQty = 1;
                    }
                    if (feeItemList.FeePack.Equals("1"))//包装单位
                    {
                        SheetView.Cells[SheetView.RowCount - 1, 2].Text = (feeItemList.Item.Qty / feeItemList.Item.PackQty).ToString() + feeItemList.Item.PriceUnit;
                        SheetView.Cells[SheetView.RowCount - 1, 3].Text = feeItemList.Item.Price.ToString("F2");
                       // SheetView.Cells[SheetView.RowCount - 1, 2].Text = (feeItemList.Item.Qty / feeItemList.Item.PackQty).ToString()+feeItemList.Item.PriceUnit;
                       // SheetView.Cells[SheetView.RowCount - 1, 4].Text = ((feeItemList.Item.Qty / feeItemList.Item.PackQty)*feeItemList.Item.Price).ToString("F2");
                    }
                    else
                    {
                       // SheetView.Cells[SheetView.RowCount - 1, 2].Text = feeItemList.Item.Qty.ToString("F2") + feeItemList.Item.PriceUnit;
                      //  SheetView.Cells[SheetView.RowCount - 1, 4].Text = (feeItemList.Item.Price * feeItemList.Item.Qty).ToString("F2");
                        SheetView.Cells[SheetView.RowCount - 1, 2].Text = feeItemList.Item.Qty.ToString("F2") + feeItemList.Item.PriceUnit;
                        SheetView.Cells[SheetView.RowCount - 1, 3].Text = (feeItemList.Item.Price / feeItemList.Item.PackQty).ToString("F2");
                    }
                    SheetView.Cells[SheetView.RowCount - 1, 4].Text = (feeItemList.Item.Price * feeItemList.Item.Qty / feeItemList.Item.PackQty).ToString("F2");  //(feeItemList.FT.OwnCost + feeItemList.FT.PayCost + feeItemList.FT.PubCost).ToString("F2");
                    SheetView.Rows[SheetView.RowCount - 1].Height = SheetView.GetPreferredCellSize(SheetView.RowCount - 1, 1).Height+5;
                    InvoiceType = feeItemList.Invoice.Type.Name;  
                }
                else
                {
                    SheetView.RowCount++;
                    //取发票科目名称

                    SheetView.Cells[SheetView.RowCount - 1, 1].Text = InvoiceType;// this.GetInvoiceTypeName(InvoiceType);
                    SheetView.Cells[SheetView.RowCount - 1, 1].Font = new Font("宋体", 9,FontStyle.Bold);
                    //计算小计
                    SheetView.Cells[SheetView.RowCount - 1, 3].Text ="小计:";
                    SheetView.Cells[SheetView.RowCount - 1, 3].Font = new Font("宋体", 9, FontStyle.Bold);
                    Decimal num =0;
                    for (i=j; i < SheetView.RowCount-1; i++)
                    {
                        num = num + Convert.ToDecimal(SheetView.Cells[i, 4].Text);
                    }
                    SheetView.Cells[SheetView.RowCount - 1, 4].Text = num.ToString("F2");
                    SheetView.Cells[SheetView.RowCount - 1, 4].Font = new Font("宋体", 9, FontStyle.Bold);
                    Total = Total + num;
                    SheetView.Rows[SheetView.RowCount - 1].Height = SheetView.GetPreferredRowHeight(SheetView.RowCount - 1);  
                    SheetView.RowCount++;
                   // SheetView.Cells[SheetView.RowCount - 1, 0].ColumnSpan = 5;
                    Order++;
                    j = SheetView.RowCount-1;
                    SheetView.Cells[SheetView.RowCount - 1, 0].Text = Order.ToString();
                    SheetView.Cells[SheetView.RowCount - 1, 1].Text = feeItemList.Item.Name;
                    SheetView.Cells[SheetView.RowCount - 1, 1].Font = new Font("宋体", 9);
                    SheetView.Cells[SheetView.RowCount - 1, 2].Text = feeItemList.Item.Qty.ToString("F2") + feeItemList.Item.PriceUnit;
                    SheetView.Cells[SheetView.RowCount - 1, 3].Text = feeItemList.Item.Price.ToString("F2");
                    if (feeItemList.Item.PackQty <= 0)
                    {
                        feeItemList.Item.PackQty = 1;
                    }
                    if (feeItemList.FeePack.Equals("1"))//包装单位
                    {
                        SheetView.Cells[SheetView.RowCount - 1, 2].Text = (feeItemList.Item.Qty / feeItemList.Item.PackQty).ToString() + feeItemList.Item.PriceUnit;
                        SheetView.Cells[SheetView.RowCount - 1, 3].Text = feeItemList.Item.Price.ToString("F2");

                       // SheetView.Cells[SheetView.RowCount - 1, 2].Text = (feeItemList.Item.Qty / feeItemList.Item.PackQty).ToString()+feeItemList.Item.PriceUnit;
                       // SheetView.Cells[SheetView.RowCount - 1, 4].Text = feeItemList.Item.Price.ToString("F2");
                    }
                    else
                    {
                        SheetView.Cells[SheetView.RowCount - 1, 2].Text = feeItemList.Item.Qty.ToString("F2") + feeItemList.Item.PriceUnit;
                        SheetView.Cells[SheetView.RowCount - 1, 3].Text = (feeItemList.Item.Price / feeItemList.Item.PackQty).ToString("F2");
                       // SheetView.Cells[SheetView.RowCount - 1, 2].Text = feeItemList.Item.Qty.ToString("F2") + feeItemList.Item.PriceUnit;
                       // SheetView.Cells[SheetView.RowCount - 1, 4].Text = (feeItemList.Item.Price / feeItemList.Item.PackQty).ToString("F2");
                    }
                    SheetView.Cells[SheetView.RowCount - 1, 4].Text = (feeItemList.Item.Price * feeItemList.Item.Qty / feeItemList.Item.PackQty).ToString("F2"); // (feeItemList.FT.OwnCost + feeItemList.FT.PayCost + feeItemList.FT.PubCost).ToString("F2");
                    SheetView.Rows[SheetView.RowCount - 1].Height = SheetView.GetPreferredCellSize(SheetView.RowCount - 1, 1).Height+5;
                    InvoiceType = feeItemList.Invoice.Type.Name; 

                }
            }

            SheetView.RowCount++;
            SheetView.Cells[SheetView.RowCount - 1, 1].Text = (alFeeDetail[alFeeDetail.Count - 1] as FS.HISFC.Models.Fee.Outpatient.FeeItemList).Invoice.Type.Name;
            SheetView.Cells[SheetView.RowCount - 1, 1].Font = new Font("宋体", 9, FontStyle.Bold);
            //计算小计
            SheetView.Cells[SheetView.RowCount - 1, 3].Text = "小计:";
            SheetView.Cells[SheetView.RowCount - 1, 3].Font = new Font("宋体", 9, FontStyle.Bold);
            Decimal num1 = 0;
            for (i = j; i < SheetView.RowCount-1; i++)
            {
                num1 = num1 + Convert.ToDecimal(SheetView.Cells[i, 4].Text);
            }
            Total = Total + num1;
            SheetView.Cells[SheetView.RowCount - 1, 4].Text = num1.ToString("F2");
            SheetView.Cells[SheetView.RowCount - 1, 4].Font = new Font("宋体", 9, FontStyle.Bold);
            SheetView.Rows[SheetView.RowCount - 1].Height = SheetView.GetPreferredRowHeight(SheetView.RowCount - 1);
            //打印页脚
            SheetView.RowCount++;
            SheetView.Cells[SheetView.RowCount - 1, 1].ColumnSpan = 5;
            SheetView.Cells[SheetView.RowCount - 1, 1].Text = "-----------------------------------------------";
            SheetView.RowCount++;
            SheetView.Cells[SheetView.RowCount - 1, 3].Text = "合计:";
            SheetView.Cells[SheetView.RowCount - 1, 3].Font = new Font("宋体", 9, FontStyle.Bold);
            SheetView.Cells[SheetView.RowCount - 1, 4].Text = Total.ToString("F2");
            SheetView.Cells[SheetView.RowCount - 1, 4].Font = new Font("宋体", 9, FontStyle.Bold);
            SheetView.Rows[SheetView.RowCount - 1].Height = SheetView.GetPreferredRowHeight(SheetView.RowCount - 1);
            SheetView.RowCount++;
            SheetView.Cells[SheetView.RowCount - 1, 1].ColumnSpan = 5;
            SheetView.Cells[SheetView.RowCount - 1, 1].Text = "\n\r            取药时请认真核对电脑小票\n\r                ";//咨询电话:(0755)
            SheetView.Rows[SheetView.RowCount - 1].Height = SheetView.GetPreferredRowHeight(SheetView.RowCount - 1);  

        }
        /// <summary>
        /// 显示费用信息
        /// </summary>
        /// <param name="itemList"></param>
        public void SetValue(FS.HISFC.Models.Registration.Register register, ArrayList balanceList, ArrayList itemList)
        {

            this.Init(FS.FrameWork.Management.Connection.Hospital.Name, register.Name, itemList);
            this.InitSPread();
            this.AddFeeDetail(itemList);
        }



    }
}
