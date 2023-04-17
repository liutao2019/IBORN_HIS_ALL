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
    public partial class ucFeeDetailPricedList : UserControl
    {
        /// <summary>
        /// 门诊记账单打印
        /// </summary>
        public ucFeeDetailPricedList()
        {
            InitializeComponent();
        }
        FS.HISFC.BizLogic.Manager.Constant conMger = new FS.HISFC.BizLogic.Manager.Constant();
        FS.FrameWork.Models.NeuObject neuObj = new FS.FrameWork.Models.NeuObject();
        FS.FrameWork.Models.NeuObject empStatusObj = new FS.FrameWork.Models.NeuObject();//人员状态

        /// <summary>
        /// 是否指引单（默认true，false记账单打印)
        /// </summary>
        private bool isGuidePrint = true;
        /// <summary>
        /// 是否指引单（默认true，false记账单打印)
        /// </summary>
        public bool IsGuidePrint
        {
            get { return isGuidePrint; }
            set { isGuidePrint = value; }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void Init(string strHospitalName, FS.HISFC.Models.Registration.Register register, ArrayList alFeeDetail)
        {
            if (this.IsGuidePrint)
            {
                this.neuLblHeader.Text = "            " + strHospitalName + "门诊费用清单";
                this.neuLblPatientName.Text = "姓名: " + register.Name;
                this.lblDateInfo.Text = DateTime.Now.ToShortDateString();
            }
            else
            {
                FS.HISFC.Models.Base.Employee operObj = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Clone();
                this.Size = new Size(700, 1000);
                this.neuLblHeader.Text = strHospitalName + operObj .Dept.Name+"记账单";
                this.neuLblPatientName.Text = "姓名: " + register.Name;
                if (!register .Pact .PayKind.ID.Equals("01"))//非自费患者,
                {
                    if (register.SIMainInfo.MedicalType.ID == "11")
                    {
                        neuObj = conMger.GetConstant("PersonType", register.SIMainInfo.PersonType.ID);//人员类别
                        empStatusObj = conMger.GetConstant("EmplType", register.SIMainInfo.EmplType);//人员状态
                        this.lblPayKind.Text = neuObj.Name + "(" + empStatusObj.Name + ")";
                    }
                    else
                    {
                        neuObj = conMger.GetConstant("SZPACTUNIT", register.PVisit.MedicalType.ID);
                        this.lblPayKind.Text = neuObj.Name;
                    }
                }
                else
                {
                    this.lblPayKind.Text = "自费";
                }
                this.lblSex.Text = "性别:"+register.Sex.Name;
                this.lblDateInfo.Text = "记账日期:"+DateTime.Now.ToShortDateString();
            }
        }
        private void InitSPread()
        {
            if (this.IsGuidePrint)//指引单样式
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
            else//记账单
            {
                FarPoint.Win.Spread.SheetView SheetView;
                SheetView = this.neuSp_FeeDetailList_Sheet1;
                SheetView.RowHeader.Columns.Default.Resizable = false;
                SheetView.RowCount = 0;
                //SheetView.ColumnCount = 7;
                //SheetView.Columns[0].Label = "序号";
                //SheetView.Columns[0].Width = 50F;
                //SheetView.Columns[1].Label = "收费项目规格";
                //SheetView.Columns[1].Width = 150F;
                //SheetView.Columns[2].Label = "单位";
                //SheetView.Columns[2].Width = 150F;
                //SheetView.Columns[3].Label = "数量";
                //SheetView.Columns[3].Width = 150F;
                //SheetView.Columns[4].Label = "单价";
                //SheetView.Columns[4].Width = 150F;
                //SheetView.Columns[4].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                //SheetView.Columns[5].Label = "总金额(元)";
                //SheetView.Columns[5].Width = 150F;
                //SheetView.Columns[6].Label = "费用科室";
                //SheetView.Columns[6].Width = 150F;
                FarPoint.Win.Spread.CellType.TextCellType TextCell = new FarPoint.Win.Spread.CellType.TextCellType();
                TextCell.Multiline = true;
                TextCell.WordWrap = true;
                SheetView.Columns[1].CellType = TextCell;
            }

           
        }
        private void AddFeeDetail(ArrayList alFeeDetail)
        {
            if (!this.IsGuidePrint)
            {
                int i = 0, j = 0, Order = 0;
                Decimal Total = 0;//合计
                FarPoint.Win.Spread.SheetView SheetView;
                SheetView = this.neuSp_FeeDetailList_Sheet1;
                string InvoiceType;
                InvoiceType = (alFeeDetail[0] as FS.HISFC.Models.Fee.Outpatient.FeeItemList).Invoice.Type.Name;
                FS.HISFC.BizLogic.Fee.Interface myInterface = new FS.HISFC.BizLogic.Fee.Interface();
                FS.HISFC.Models.SIInterface.Compare compareObj = new FS.HISFC.Models.SIInterface.Compare();
                FS.HISFC.BizProcess.Integrate.Manager  deptManager = new  FS.HISFC.BizProcess.Integrate.Manager();
                FS.HISFC.BizLogic.Manager.Department deptMgr = new FS.HISFC.BizLogic.Manager.Department();
                ArrayList alDept = deptManager.QueryDeptmentsInHos(true);
            alDept.AddRange(deptMgr.GetDeptmentAll());
            FS.FrameWork.Public.ObjectHelper deptHelper = new FS.FrameWork.Public.ObjectHelper();
            deptHelper.ArrayObject = alDept;
                int SIRate = 0;
                 
                
                foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItemList in alFeeDetail)
                {
                    //项目名称
                    if (InvoiceType == feeItemList.Invoice.Type.Name)
                    {
                        SheetView.RowCount++;
                        Order++;
                        SheetView.Cells[SheetView.RowCount - 1, 0].Text = Order.ToString();
                        if (feeItemList.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                        {
                            SIRate = myInterface.GetCompareSingleItem("2", feeItemList.Item.ID, ref compareObj);
                            SheetView.Cells[SheetView.RowCount - 1, 1].Text = compareObj.CenterItem.ID;//统一编码
                        }
                        else
                        {
                            SheetView.Cells[SheetView.RowCount - 1, 1].Text = FS.SOC.HISFC.BizProcess.Cache.Fee.GetItem(feeItemList.Item.ID).GBCode;//统一编码
                        }
                        SheetView.Cells[SheetView.RowCount - 1, 2].Text = feeItemList.Item.Name;
                        SheetView.Cells[SheetView.RowCount - 1, 3].Text = feeItemList.Item.PriceUnit;
                        SheetView.Cells[SheetView.RowCount - 1, 4].Text = feeItemList.Item.Qty.ToString("F2");
                        SheetView.Cells[SheetView.RowCount - 1, 5].Text = feeItemList.Item.Price.ToString("F2");
                        if (feeItemList.Item.PackQty <= 0)
                        {
                            feeItemList.Item.PackQty = 1;
                        }
                        if (feeItemList.FeePack.Equals("1"))//包装单位
                        {
                            SheetView.Cells[SheetView.RowCount - 1, 3].Text = feeItemList.Item.PriceUnit;
                            SheetView.Cells[SheetView.RowCount - 1, 4].Text = (feeItemList.Item.Qty / feeItemList.Item.PackQty).ToString();
                            SheetView.Cells[SheetView.RowCount - 1, 5].Text = feeItemList.Item.Price.ToString("F2");
                        }
                        else
                        {
                            SheetView.Cells[SheetView.RowCount - 1, 3].Text = feeItemList.Item.PriceUnit;
                            SheetView.Cells[SheetView.RowCount - 1, 4].Text = feeItemList.Item.Qty.ToString("F2");
                            SheetView.Cells[SheetView.RowCount - 1, 5].Text = (feeItemList.Item.Price / feeItemList.Item.PackQty).ToString("F2");
                        }
                        SheetView.Cells[SheetView.RowCount - 1, 6].Text = (feeItemList.Item.Price * feeItemList.Item.Qty / feeItemList.Item.PackQty).ToString("F2");  //(feeItemList.FT.OwnCost + feeItemList.FT.PayCost + feeItemList.FT.PubCost).ToString("F2");
                        SheetView.Cells[SheetView.RowCount - 1, 7].Text = deptHelper.GetName(feeItemList.ExecOper.Dept.ID);
                        SheetView.Rows[SheetView.RowCount - 1].Height = SheetView.GetPreferredCellSize(SheetView.RowCount - 1, 1).Height + 5;
                        InvoiceType = feeItemList.Invoice.Type.Name;
                    }
                    else
                    {
                        SheetView.RowCount++;
                        //取发票科目名称

                        SheetView.Cells[SheetView.RowCount - 1, 1].Text = InvoiceType;// this.GetInvoiceTypeName(InvoiceType);
                        SheetView.Cells[SheetView.RowCount - 1, 1].Font = new Font("宋体", 9, FontStyle.Bold);
                        //计算小计
                        SheetView.Cells[SheetView.RowCount - 1, 5].Text = "小计:";
                        SheetView.Cells[SheetView.RowCount - 1, 5].Font = new Font("宋体", 9, FontStyle.Bold);
                        Decimal num = 0;
                        for (i = j; i < SheetView.RowCount - 1; i++)
                        {
                            num = num + Convert.ToDecimal(SheetView.Cells[i, 6].Text);
                        }
                        SheetView.Cells[SheetView.RowCount - 1, 6].Text = num.ToString("F2");
                        SheetView.Cells[SheetView.RowCount - 1, 6].Font = new Font("宋体", 9, FontStyle.Bold);
                        Total = Total + num;
                        SheetView.Rows[SheetView.RowCount - 1].Height = SheetView.GetPreferredRowHeight(SheetView.RowCount - 1);
                        SheetView.RowCount++;
                        Order++;
                        j = SheetView.RowCount - 1;
                        SheetView.Cells[SheetView.RowCount - 1, 0].Text = Order.ToString();
                        if (feeItemList.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                        {
                            SIRate = myInterface.GetCompareSingleItem("2", feeItemList.Item.ID, ref compareObj);
                            SheetView.Cells[SheetView.RowCount - 1, 1].Text = compareObj.CenterItem.ID;//统一编码
                        }
                        else
                        {
                            SheetView.Cells[SheetView.RowCount - 1, 1].Text = FS.SOC.HISFC.BizProcess.Cache.Fee.GetItem(feeItemList.Item.ID).GBCode;//统一编码
                        }
                        SheetView.Cells[SheetView.RowCount - 1, 2].Text = feeItemList.Item.Name;
                        SheetView.Cells[SheetView.RowCount - 1, 2].Font = new Font("宋体", 9);
                        SheetView.Cells[SheetView.RowCount - 1, 3].Text = feeItemList.Item.PriceUnit;
                        SheetView.Cells[SheetView.RowCount - 1, 4].Text = feeItemList.Item.Qty.ToString("F2");
                        SheetView.Cells[SheetView.RowCount - 1, 5].Text = feeItemList.Item.Price.ToString("F2");
                        if (feeItemList.Item.PackQty <= 0)
                        {
                            feeItemList.Item.PackQty = 1;
                        }
                        if (feeItemList.FeePack.Equals("1"))//包装单位
                        {
                            SheetView.Cells[SheetView.RowCount - 1, 3].Text = feeItemList.Item.PriceUnit;
                            SheetView.Cells[SheetView.RowCount - 1, 4].Text = (feeItemList.Item.Qty / feeItemList.Item.PackQty).ToString();
                            SheetView.Cells[SheetView.RowCount - 1, 5].Text = feeItemList.Item.Price.ToString("F2");
                        }
                        else
                        {
                            SheetView.Cells[SheetView.RowCount - 1, 3].Text = feeItemList.Item.PriceUnit;
                            SheetView.Cells[SheetView.RowCount - 1, 4].Text = feeItemList.Item.Qty.ToString("F2");
                            SheetView.Cells[SheetView.RowCount - 1, 5].Text = (feeItemList.Item.Price / feeItemList.Item.PackQty).ToString("F2");
                        }
                        SheetView.Cells[SheetView.RowCount - 1, 6].Text = (feeItemList.Item.Price * feeItemList.Item.Qty / feeItemList.Item.PackQty).ToString("F2"); // (feeItemList.FT.OwnCost + feeItemList.FT.PayCost + feeItemList.FT.PubCost).ToString("F2");
                        SheetView.Cells[SheetView.RowCount - 1, 7].Text = deptHelper.GetName(feeItemList.ExecOper.Dept.ID);
                        SheetView.Rows[SheetView.RowCount - 1].Height = SheetView.GetPreferredCellSize(SheetView.RowCount - 1, 1).Height + 5;
                        InvoiceType = feeItemList.Invoice.Type.Name;

                    }
                }

                SheetView.RowCount++;
                SheetView.Cells[SheetView.RowCount - 1, 1].Text = (alFeeDetail[alFeeDetail.Count - 1] as FS.HISFC.Models.Fee.Outpatient.FeeItemList).Invoice.Type.Name;
                SheetView.Cells[SheetView.RowCount - 1, 1].Font = new Font("宋体", 9, FontStyle.Bold);
                //计算小计
                SheetView.Cells[SheetView.RowCount - 1, 5].Text = "小计:";
                SheetView.Cells[SheetView.RowCount - 1, 5].Font = new Font("宋体", 9, FontStyle.Bold);
                Decimal num1 = 0;
                for (i = j; i < SheetView.RowCount - 1; i++)
                {
                    num1 = num1 + Convert.ToDecimal(SheetView.Cells[i, 6].Text);
                }
                Total = Total + num1;
                SheetView.Cells[SheetView.RowCount - 1, 6].Text = num1.ToString("F2");
                SheetView.Cells[SheetView.RowCount - 1, 6].Font = new Font("宋体", 9, FontStyle.Bold);
                SheetView.Rows[SheetView.RowCount - 1].Height = SheetView.GetPreferredRowHeight(SheetView.RowCount - 1);
                //打印页脚
                SheetView.RowCount++;
                SheetView.Cells[SheetView.RowCount - 1, 0].ColumnSpan = 8;
                SheetView.Cells[SheetView.RowCount - 1, 0].Text = "      ----------------------------------------------------------------------------------------------";
                SheetView.RowCount++;
                SheetView.Cells[SheetView.RowCount - 1, 5].Text = "合计:";
                SheetView.Cells[SheetView.RowCount - 1, 5].Font = new Font("宋体", 9, FontStyle.Bold);
                SheetView.Cells[SheetView.RowCount - 1, 6].Text = Total.ToString("F2");
                this.lblSum.Text = "费用总额:"+Total.ToString("F2");
                SheetView.Cells[SheetView.RowCount - 1, 6].Font = new Font("宋体", 9, FontStyle.Bold);
                SheetView.Rows[SheetView.RowCount - 1].Height = SheetView.GetPreferredRowHeight(SheetView.RowCount - 1);
                SheetView.RowCount++;
                SheetView.Cells[SheetView.RowCount - 1, 1].ColumnSpan = 8;
                //SheetView.Cells[SheetView.RowCount - 1, 1].Text = "\n\r            取药时请认真核对电脑小票\n\r                ";//咨询电话:(0755)
                SheetView.Rows[SheetView.RowCount - 1].Height = SheetView.GetPreferredRowHeight(SheetView.RowCount - 1);

            }
            else
            {
                //int i = 0, j = 0, Order = 0;
                //Decimal Total = 0;//合计
                //FarPoint.Win.Spread.SheetView SheetView;
                //SheetView = this.neuSp_FeeDetailList_Sheet1;
                //string InvoiceType;
                //InvoiceType = (alFeeDetail[0] as FS.HISFC.Models.Fee.Outpatient.FeeItemList).Invoice.Type.Name;
                //foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItemList in alFeeDetail)
                //{
                //    //项目名称
                //    if (InvoiceType == feeItemList.Invoice.Type.Name)
                //    {
                //        SheetView.RowCount++;
                //        Order++;
                //        SheetView.Cells[SheetView.RowCount - 1, 0].Text = Order.ToString();

                //        SheetView.Cells[SheetView.RowCount - 1, 1].Text = feeItemList.Item.Name;
                //        SheetView.Cells[SheetView.RowCount - 1, 2].Text = feeItemList.Item.Qty.ToString("F2") + feeItemList.Item.PriceUnit;
                //        SheetView.Cells[SheetView.RowCount - 1, 3].Text = feeItemList.Item.Price.ToString("F2");
                //        if (feeItemList.Item.PackQty <= 0)
                //        {
                //            feeItemList.Item.PackQty = 1;
                //        }
                //        if (feeItemList.FeePack.Equals("1"))//包装单位
                //        {
                //            SheetView.Cells[SheetView.RowCount - 1, 2].Text = (feeItemList.Item.Qty / feeItemList.Item.PackQty).ToString() + feeItemList.Item.PriceUnit;
                //            SheetView.Cells[SheetView.RowCount - 1, 3].Text = feeItemList.Item.Price.ToString("F2");
                //        }
                //        else
                //        {
                //            SheetView.Cells[SheetView.RowCount - 1, 2].Text = feeItemList.Item.Qty.ToString("F2") + feeItemList.Item.PriceUnit;
                //            SheetView.Cells[SheetView.RowCount - 1, 3].Text = (feeItemList.Item.Price / feeItemList.Item.PackQty).ToString("F2");
                //        }
                //        SheetView.Cells[SheetView.RowCount - 1, 4].Text = (feeItemList.Item.Price * feeItemList.Item.Qty / feeItemList.Item.PackQty).ToString("F2");  //(feeItemList.FT.OwnCost + feeItemList.FT.PayCost + feeItemList.FT.PubCost).ToString("F2");
                //        SheetView.Rows[SheetView.RowCount - 1].Height = SheetView.GetPreferredCellSize(SheetView.RowCount - 1, 1).Height + 5;
                //        InvoiceType = feeItemList.Invoice.Type.Name;
                //    }
                //    else
                //    {
                //        SheetView.RowCount++;
                //        //取发票科目名称

                //        SheetView.Cells[SheetView.RowCount - 1, 1].Text = InvoiceType;
                //        SheetView.Cells[SheetView.RowCount - 1, 1].Font = new Font("宋体", 9, FontStyle.Bold);
                //        //计算小计
                //        SheetView.Cells[SheetView.RowCount - 1, 3].Text = "小计:";
                //        SheetView.Cells[SheetView.RowCount - 1, 3].Font = new Font("宋体", 9, FontStyle.Bold);
                //        Decimal num = 0;
                //        for (i = j; i < SheetView.RowCount - 1; i++)
                //        {
                //            num = num + Convert.ToDecimal(SheetView.Cells[i, 4].Text);
                //        }
                //        SheetView.Cells[SheetView.RowCount - 1, 4].Text = num.ToString("F2");
                //        SheetView.Cells[SheetView.RowCount - 1, 4].Font = new Font("宋体", 9, FontStyle.Bold);
                //        Total = Total + num;
                //        SheetView.Rows[SheetView.RowCount - 1].Height = SheetView.GetPreferredRowHeight(SheetView.RowCount - 1);
                //        SheetView.RowCount++;
                //        // SheetView.Cells[SheetView.RowCount - 1, 0].ColumnSpan = 5;
                //        Order++;
                //        j = SheetView.RowCount - 1;
                //        SheetView.Cells[SheetView.RowCount - 1, 0].Text = Order.ToString();
                //        SheetView.Cells[SheetView.RowCount - 1, 1].Text = feeItemList.Item.Name;
                //        SheetView.Cells[SheetView.RowCount - 1, 1].Font = new Font("宋体", 9);
                //        SheetView.Cells[SheetView.RowCount - 1, 2].Text = feeItemList.Item.PriceUnit;
                //        SheetView.Cells[SheetView.RowCount - 1, 3].Text = feeItemList.Item.Qty.ToString("F2"); 
                //        if (feeItemList.Item.PackQty <= 0)
                //        {
                //            feeItemList.Item.PackQty = 1;
                //        }
                //        if (feeItemList.FeePack.Equals("1"))//包装单位
                //        {
                //            SheetView.Cells[SheetView.RowCount - 1, 2].Text = feeItemList.Item.PriceUnit;
                //            SheetView.Cells[SheetView.RowCount - 1, 3].Text = (feeItemList.Item.Qty / feeItemList.Item.PackQty).ToString();
                //            SheetView.Cells[SheetView .RowCount -1,4].Text=feeItemList.Item.Price.ToString("F2");

                //            // SheetView.Cells[SheetView.RowCount - 1, 2].Text = (feeItemList.Item.Qty / feeItemList.Item.PackQty).ToString()+feeItemList.Item.PriceUnit;
                //            // SheetView.Cells[SheetView.RowCount - 1, 4].Text = feeItemList.Item.Price.ToString("F2");
                //        }
                //        else
                //        {
                //            SheetView.Cells[SheetView.RowCount - 1, 2].Text = feeItemList.Item.PriceUnit;
                //            SheetView.Cells[SheetView.RowCount - 1, 3].Text = feeItemList.Item.Qty.ToString("F2");
                //                SheetView.Cells[SheetView.RowCount -1,4].Text=(feeItemList.Item.Price / feeItemList.Item.PackQty).ToString("F2");
                //            // SheetView.Cells[SheetView.RowCount - 1, 2].Text = feeItemList.Item.Qty.ToString("F2") + feeItemList.Item.PriceUnit;
                //            // SheetView.Cells[SheetView.RowCount - 1, 4].Text = (feeItemList.Item.Price / feeItemList.Item.PackQty).ToString("F2");
                //        }
                //        //SheetView.Cells[SheetView.RowCount - 1, 4].Text =feeItemList.Item.Price.ToString("F2");
                //        SheetView .Cells[SheetView .RowCount-1,5].Text=(feeItemList.Item.Price * feeItemList.Item.Qty / feeItemList.Item.PackQty).ToString("F2"); // (feeItemList.FT.OwnCost + feeItemList.FT.PayCost + feeItemList.FT.PubCost).ToString("F2");
                //        //SheetView.Cells[SheetView.RowCount - 1, 6].Text = deptHelper.GetName(feeItemList.ExecOper.Dept.ID);
                //        SheetView.Rows[SheetView.RowCount - 1].Height = SheetView.GetPreferredCellSize(SheetView.RowCount - 1, 1).Height + 5;
                //        InvoiceType = feeItemList.Invoice.Type.Name;

                //    }
                //}

                //SheetView.RowCount++;
                //SheetView.Cells[SheetView.RowCount - 1, 1].Text = (alFeeDetail[alFeeDetail.Count - 1] as FS.HISFC.Models.Fee.Outpatient.FeeItemList).Invoice.Type.Name;
                //SheetView.Cells[SheetView.RowCount - 1, 1].Font = new Font("宋体", 9, FontStyle.Bold);
                ////计算小计
                //SheetView.Cells[SheetView.RowCount - 1, 3].Text = "小计:";
                //SheetView.Cells[SheetView.RowCount - 1, 3].Font = new Font("宋体", 9, FontStyle.Bold);
                //Decimal num1 = 0;
                //for (i = j; i < SheetView.RowCount - 1; i++)
                //{
                //    num1 = num1 + Convert.ToDecimal(SheetView.Cells[i, 4].Text);
                //}
                //Total = Total + num1;
                //SheetView.Cells[SheetView.RowCount - 1, 4].Text = num1.ToString("F2");
                //SheetView.Cells[SheetView.RowCount - 1, 4].Font = new Font("宋体", 9, FontStyle.Bold);
                //SheetView.Rows[SheetView.RowCount - 1].Height = SheetView.GetPreferredRowHeight(SheetView.RowCount - 1);
                ////打印页脚
                //SheetView.RowCount++;
                //SheetView.Cells[SheetView.RowCount - 1, 1].ColumnSpan = 7;
                //SheetView.Cells[SheetView.RowCount - 1, 1].Text = "-----------------------------------------------";
                //SheetView.RowCount++;
                //SheetView.Cells[SheetView.RowCount - 1, 3].Text = "合计:";
                //SheetView.Cells[SheetView.RowCount - 1, 3].Font = new Font("宋体", 9, FontStyle.Bold);
                //SheetView.Cells[SheetView.RowCount - 1, 4].Text = Total.ToString("F2");
                //SheetView.Cells[SheetView.RowCount - 1, 4].Font = new Font("宋体", 9, FontStyle.Bold);
                //SheetView.Rows[SheetView.RowCount - 1].Height = SheetView.GetPreferredRowHeight(SheetView.RowCount - 1);
                //SheetView.RowCount++;
                //SheetView.Cells[SheetView.RowCount - 1, 1].ColumnSpan = 5;
                //SheetView.Cells[SheetView.RowCount - 1, 1].Text = "\n\r            取药时请认真核对电脑小票\n\r                ";//咨询电话:(0755)
                //SheetView.Rows[SheetView.RowCount - 1].Height = SheetView.GetPreferredRowHeight(SheetView.RowCount - 1);

            }
        }
        /// <summary>
        /// 显示费用信息
        /// </summary>
        /// <param name="itemList"></param>
        public void SetValue(FS.HISFC.Models.Registration.Register register, ArrayList balanceList, ArrayList itemList)
        {
            this.Init(FS.FrameWork.Management.Connection.Hospital.Name, register, itemList);
            this.InitSPread();            
            this.AddFeeDetail(itemList);
        }

        

    }
}
