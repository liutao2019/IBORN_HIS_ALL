using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.OutpatientFee.ZhuHai.Zdwy.IOutpatientGuide
{
    /// <summary>
    /// 门诊收费清单
    /// </summary>
    public partial class ucFeeDetail : UserControl
    {
        public ucFeeDetail()
        {
            //{AAB27AFA-E54C-4c34-9B53-FC7BF7F59631}修改了设计器的边距
            InitializeComponent();
        }

        enum EnumCol
        {
            kk空空,
            xmdm项目代码,
            xmmc项目名称,
            fpfl发票分类,
            gg规格,
            yblx医保类型,
            dw单位,
            dj单价,
            sl数量,
            yj原价,
            zj折价,
            zhj折后价,
            sftc是否套餐//15E1CDD6-FBA9-4d93-8204-0BC788EBC265 添加一列判断项目是否是套餐
        }

        private Dictionary<string, string> dicInvoceType = new Dictionary<string, string>();

        FS.HISFC.BizLogic.Manager.DataBase dbMgr = new FS.HISFC.BizLogic.Manager.DataBase();

        public void SetValue(FS.HISFC.Models.Registration.Register rInfo, System.Collections.ArrayList invoices, System.Collections.ArrayList feeDetails)
        {
            if (feeDetails.Count > 0)
            {
                FS.HISFC.Models.Base.Department currDept = new FS.HISFC.Models.Base.Department();
                currDept = (FS.HISFC.Models.Base.Department)(((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept);
                if (!string.IsNullOrEmpty(currDept.HospitalName))// {90EE4859-CD33-413c-84B9-A1B3A7C16332}
                {
                    this.label1.Text = currDept.HospitalName + "门诊费用明细清单";
                }
                else
                {
                    this.label1.Text = "爱博恩妇产医院门诊费用明细清单";
                }
                //this.label1.Text = FS.FrameWork.Management.Connection.Hospital.Name + "门诊医药费用明细清单";
                this.lblName.Text = rInfo.Name;
                lblCardNO.Text = rInfo.PID.CardNO;
                this.fpSpreadItemsSheet.RowCount = 1;
                this.fpSpreadItemsSheet.RowCount += feeDetails.Count + 1;
                int row =0;
                FarPoint.Win.LineBorder bottomAndtopBorder = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, false, true, false, true);
                FS.HISFC.Models.Base.PactInfo pact = SOC.HISFC.BizProcess.Cache.Fee.GetPactUnitInfo(rInfo.Pact.ID);
               // int iRow = -1;
                decimal totCost = 0;
                decimal rebateCost = 0m;
                decimal ownCost = 0m;
                //{2044475C-E8CE-454B-B328-90EAAC174D1A} 增加费别统计
                Dictionary<string, List<FS.HISFC.Models.Fee.Outpatient.FeeItemList>> drugDictionary = new Dictionary<string, List<FS.HISFC.Models.Fee.Outpatient.FeeItemList>>();
                foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItemList in feeDetails)
                {
                    List<FS.HISFC.Models.Fee.Outpatient.FeeItemList> feeList = null;
                    if (drugDictionary.ContainsKey(feeItemList.User01))
                    {
                        drugDictionary[feeItemList.User01].Add(feeItemList);
                    }
                    else
                    {
                        feeList = new List<FS.HISFC.Models.Fee.Outpatient.FeeItemList>();
                        feeList.Add(feeItemList);
                        drugDictionary.Add(feeItemList.User01, feeList);
                    }
                }
                if (drugDictionary.Keys.Count <= 0)
                {
                    return ;
                }
                foreach (string recipeNO in drugDictionary.Keys)
                {
                    row++;
                    List<FS.HISFC.Models.Fee.Outpatient.FeeItemList> feeList = new List<FS.HISFC.Models.Fee.Outpatient.FeeItemList>();
                    feeList = drugDictionary[recipeNO];
                    FS.HISFC.Models.Fee.Outpatient.FeeItemList oneFeeInfo = new FS.HISFC.Models.Fee.Outpatient.FeeItemList();
                    oneFeeInfo = drugDictionary[recipeNO][0];

                    //iRow++;
                    this.fpSpreadItemsSheet.Rows.Add(row, 1);
                    this.fpSpreadItemsSheet.Cells[row, 0].ColumnSpan = 2;
                    this.fpSpreadItemsSheet.Cells[row, 2].ColumnSpan = 11;
                    this.fpSpreadItemsSheet.Cells[row, 0].Text = oneFeeInfo.User01;
                    this.fpSpreadItemsSheet.Cells[row, 0].Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                    this.fpSpreadItemsSheet.Cells[row, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                    this.fpSpreadItemsSheet.Cells[row, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                    this.fpSpreadItemsSheet.Cells[row, 0].Border = bottomAndtopBorder;
                    int currInfoLan = row;
                    decimal Total = 0m;//原价
                   
                    decimal donateCost = 0m;//实付金额
                    decimal dereateCost = 0m;//减免金额
                    foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItemList in feeList)
                    {
                        row++;
                        lblInvoiceNo.Text = feeItemList.Invoice.ID;
                        lblFeeDate.Text = feeItemList.FeeOper.OperTime.ToString("yyyy-MM-dd");
                        lblDoctName.Text = SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(feeItemList.RecipeOper.ID);
                        lblDept.Text = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(feeItemList.RecipeOper.Dept.ID);
                        #region 查询结算待遇类型{C948F7BF-3754-47b1-888B-83A21B8D04D1}
                        string strSQLPact = @"select t.pact_name from fin_opb_invoiceinfo t where INVOICE_NO ='{0}'";
                        strSQLPact = string.Format(strSQLPact, feeItemList.Invoice.ID);
                        string pactName = dbMgr.ExecSqlReturnOne(strSQLPact);
                        #endregion
                        lblPactName.Text = pactName;


                        #region 医保标记
                        string SIFlag = "";

                        //if (pact.PactDllName == "OwnFee.dll")
                        //{
                        //    SIFlag = "自费类";
                        //}
                        //else
                        //{
                        string strPactCode = "14";

                        FS.HISFC.Models.SIInterface.Compare compareItem = this.GetCompareItemInfo(strPactCode, feeItemList.Item.ID);

                        if (compareItem == null
                            || compareItem.CenterItem.Rate == 1
                            || string.IsNullOrEmpty(compareItem.CenterItem.ItemGrade)
                            || compareItem.CenterItem.ItemGrade == "3")
                        {
                            SIFlag = "自费类";
                        }
                        else if (compareItem.CenterItem.ItemGrade == "2")
                        {
                            SIFlag = "控制类";
                        }
                        else if (compareItem.CenterItem.ItemGrade == "1")
                        {
                            SIFlag = "基本类";
                        }
                        else
                        {
                            SIFlag = "";
                        }
                        //}

                        #endregion

                        #region 发票分类

                        string strTypeName = "";
                        if (dicInvoceType.ContainsKey(feeItemList.Item.MinFee.ID))
                        {
                            strTypeName = dicInvoceType[feeItemList.Item.MinFee.ID];
                        }
                        else
                        {
                            string strSQL = @"  select d.fee_stat_name from fin_com_feecodestat d
                                        where d.report_code='MZ01'
                                        and d.fee_code='{0}'";
                            strSQL = string.Format(strSQL, feeItemList.Item.MinFee.ID);
                            strTypeName = dbMgr.ExecSqlReturnOne(strSQL);
                            dicInvoceType.Add(feeItemList.Item.MinFee.ID, strTypeName);
                        }
                        #endregion


                        if (feeItemList.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                        {
                            FS.HISFC.Models.Pharmacy.Item phaItem = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(feeItemList.Item.ID);
                            if (phaItem != null)
                            {
                                fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.xmdm项目代码].Text = phaItem.UserCode;
                                //  fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.xmmc项目名称].Text = phaItem.Name;
                                fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.gg规格].Text = phaItem.Specs;
                                fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.dw单位].Text = phaItem.MinUnit;
                            }
                        }
                        else
                        {
                            FS.HISFC.Models.Fee.Item.Undrug undrug = SOC.HISFC.BizProcess.Cache.Fee.GetItem(feeItemList.Item.ID);
                            if (undrug != null)
                            {
                                fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.xmdm项目代码].Text = undrug.UserCode;
                                //fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.xmmc项目名称].Text = undrug.Name;
                                fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.gg规格].Text = undrug.Specs;
                            }
                        }
                        //15E1CDD6-FBA9-4d93-8204-0BC788EBC265 名字如果是复合项目 则【复合项目】+项目名称
                        fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.xmmc项目名称].Text = feeItemList.Item.Name;
                        fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.fpfl发票分类].Text = strTypeName;
                        fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.yblx医保类型].Text = SIFlag;
                        fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.dj单价].Text = (feeItemList.Item.Price / feeItemList.Item.PackQty).ToString("f4");
                        //fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.dw单位].Text = feeItemList.Item.PriceUnit;
                        fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.sl数量].Text = feeItemList.Item.Qty.ToString();
                        fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.yj原价].Text = (feeItemList.FT.OwnCost + feeItemList.FT.PubCost + feeItemList.FT.PayCost).ToString();
                        fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.zj折价].Text = (feeItemList.FT.RebateCost + feeItemList.FT.PubCost + feeItemList.FT.DonateCost).ToString();
                        fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.zhj折后价].Text = ((feeItemList.FT.OwnCost + feeItemList.FT.PubCost + feeItemList.FT.PayCost) - (feeItemList.FT.RebateCost + feeItemList.FT.PubCost+feeItemList.FT.DonateCost)).ToString();
                        //15E1CDD6-FBA9-4d93-8204-0BC788EBC265 添加一列判断项目是否是套餐
                        fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.sftc是否套餐].Text = feeItemList.IsPackage == "0" ? "否" : "是";
                        fpSpreadItemsSheet.Rows[row].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                        totCost += feeItemList.FT.OwnCost + feeItemList.FT.PubCost + feeItemList.FT.PayCost;

                        Total += feeItemList.FT.OwnCost + feeItemList.FT.PubCost + feeItemList.FT.PayCost;
                        rebateCost += feeItemList.FT.RebateCost + feeItemList.FT.PubCost + feeItemList.FT.DonateCost;
                        ownCost += (feeItemList.FT.OwnCost + feeItemList.FT.PubCost + feeItemList.FT.PayCost) - (feeItemList.FT.RebateCost + feeItemList.FT.PubCost + feeItemList.FT.DonateCost);

                        //{309EF30A-542C-4f4b-86EF-8152B16F8012}
                        donateCost += feeItemList.FT.RebateCost + feeItemList.FT.PubCost + feeItemList.FT.DonateCost;
                        dereateCost += feeItemList.FT.RebateCost + feeItemList.FT.PubCost + feeItemList.FT.DonateCost;
                       // fpSpreadItemsSheet.Rows[row].Height = 60;
                       // row += 1;
                    }
                    //{EBA85525-5A8B-4b02-8D5C-22BEE8E104BD}
                    this.fpSpreadItemsSheet.Cells[currInfoLan, 2].Text = "合计：" + Total.ToString("F2") + "  折后价:" + (Total - dereateCost).ToString("F2") + "  减免:" + dereateCost.ToString("F2");
                    this.fpSpreadItemsSheet.Cells[currInfoLan, 2].Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                    this.fpSpreadItemsSheet.Cells[currInfoLan, 2].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                    this.fpSpreadItemsSheet.Cells[currInfoLan, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                    this.fpSpreadItemsSheet.Cells[currInfoLan, 2].Border = bottomAndtopBorder;

                }
                #region
                //                foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItemList in feeDetails)
//                {
//                    lblInvoiceNo.Text = feeItemList.Invoice.ID;
//                    lblFeeDate.Text = feeItemList.FeeOper.OperTime.ToString("yyyy-MM-dd");
//                    lblDoctName.Text = SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(feeItemList.RecipeOper.ID);
//                    lblDept.Text = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(feeItemList.RecipeOper.Dept.ID);

                    
//                    #region 医保标记
//                    string SIFlag = "";

//                    //if (pact.PactDllName == "OwnFee.dll")
//                    //{
//                    //    SIFlag = "自费类";
//                    //}
//                    //else
//                    //{
//                        string strPactCode = "14";

//                        FS.HISFC.Models.SIInterface.Compare compareItem = this.GetCompareItemInfo(strPactCode, feeItemList.Item.ID);

//                        if (compareItem == null
//                            || compareItem.CenterItem.Rate == 1
//                            || string.IsNullOrEmpty(compareItem.CenterItem.ItemGrade)
//                            || compareItem.CenterItem.ItemGrade == "3")
//                        {
//                            SIFlag = "自费类";
//                        }
//                        else if (compareItem.CenterItem.ItemGrade == "2")
//                        {
//                            SIFlag = "控制类";
//                        }
//                        else if (compareItem.CenterItem.ItemGrade == "1")
//                        {
//                            SIFlag = "基本类";
//                        }
//                        else
//                        {
//                            SIFlag = "";
//                        }
//                    //}
                    
//                    #endregion

//                    #region 发票分类

//                    string strTypeName = "";
//                    if (dicInvoceType.ContainsKey(feeItemList.Item.MinFee.ID))
//                    {
//                        strTypeName = dicInvoceType[feeItemList.Item.MinFee.ID];
//                    }
//                    else
//                    {
//                        string strSQL = @"  select d.fee_stat_name from fin_com_feecodestat d
//                                        where d.report_code='MZ01'
//                                        and d.fee_code='{0}'";
//                        strSQL = string.Format(strSQL, feeItemList.Item.MinFee.ID);
//                        strTypeName = dbMgr.ExecSqlReturnOne(strSQL);
//                        dicInvoceType.Add(feeItemList.Item.MinFee.ID, strTypeName);
//                    }
//                    #endregion


//                    if (feeItemList.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
//                    {
//                        FS.HISFC.Models.Pharmacy.Item phaItem = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(feeItemList.Item.ID);
//                        if (phaItem != null)
//                        {
//                            fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.xmdm项目代码].Text = phaItem.UserCode;
//                          //  fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.xmmc项目名称].Text = phaItem.Name;
//                            fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.gg规格].Text = phaItem.Specs;
//                            fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.dw单位].Text = phaItem.MinUnit;
//                        }
//                    }
//                    else
//                    {
//                        FS.HISFC.Models.Fee.Item.Undrug undrug = SOC.HISFC.BizProcess.Cache.Fee.GetItem(feeItemList.Item.ID);
//                        if (undrug != null)
//                        {
//                            fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.xmdm项目代码].Text = undrug.UserCode;
//                            //fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.xmmc项目名称].Text = undrug.Name;
//                            fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.gg规格].Text = undrug.Specs;
//                        }
//                    }
//                    //15E1CDD6-FBA9-4d93-8204-0BC788EBC265 名字如果是复合项目 则【复合项目】+项目名称
//                    fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.xmmc项目名称].Text = feeItemList.Item.Name;
//                    fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.fpfl发票分类].Text = strTypeName;
//                    fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.yblx医保类型].Text = SIFlag;
//                    fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.dj单价].Text = (feeItemList.Item.Price / feeItemList.Item.PackQty).ToString("f4");
//                    //fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.dw单位].Text = feeItemList.Item.PriceUnit;
//                    fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.sl数量].Text = feeItemList.Item.Qty.ToString();
//                    fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.yj原价].Text = (feeItemList.FT.OwnCost + feeItemList.FT.PubCost + feeItemList.FT.PayCost).ToString();
//                    fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.zj折价].Text = (feeItemList.FT.RebateCost + feeItemList.FT.PubCost).ToString();
//                    fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.zhj折后价].Text = ((feeItemList.FT.OwnCost + feeItemList.FT.PubCost + feeItemList.FT.PayCost) - (feeItemList.FT.RebateCost + feeItemList.FT.PubCost)).ToString();
//                    //15E1CDD6-FBA9-4d93-8204-0BC788EBC265 添加一列判断项目是否是套餐
//                    fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.sftc是否套餐].Text = feeItemList.IsPackage == "0" ? "否" : "是";
//                    fpSpreadItemsSheet.Rows[row].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

//                    totCost += feeItemList.FT.OwnCost + feeItemList.FT.PubCost + feeItemList.FT.PayCost;
//                    rebateCost += feeItemList.FT.RebateCost + feeItemList.FT.PubCost;
//                    ownCost += (feeItemList.FT.OwnCost + feeItemList.FT.PubCost + feeItemList.FT.PayCost) - (feeItemList.FT.RebateCost + feeItemList.FT.PubCost);
//                    row += 1;
                //                }
                #endregion

                #region 显示设置

                fpSpreadItemsSheet.Rows[row].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

                FarPoint.Win.ComplexBorder complexBorder1 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(System.Drawing.SystemColors.WindowFrame, 2));
                FarPoint.Win.ComplexBorder complexBorder2 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(System.Drawing.SystemColors.WindowFrame, 2));
                FarPoint.Win.ComplexBorder complexBorder3 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(System.Drawing.SystemColors.WindowFrame, 2));
                FarPoint.Win.ComplexBorder complexBorder4 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(System.Drawing.SystemColors.WindowFrame, 2));
                FarPoint.Win.ComplexBorder complexBorder5 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(System.Drawing.SystemColors.WindowFrame, 2));
                FarPoint.Win.ComplexBorder complexBorder6 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(System.Drawing.SystemColors.WindowFrame, 2));
                FarPoint.Win.ComplexBorder complexBorder7 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(System.Drawing.SystemColors.WindowFrame, 2));
                FarPoint.Win.ComplexBorder complexBorder8 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(System.Drawing.SystemColors.WindowFrame, 2));
                FarPoint.Win.ComplexBorder complexBorder9 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(System.Drawing.SystemColors.WindowFrame, 2));

                this.fpSpreadItemsSheet.Cells.Get(row , 0).Border = complexBorder1;
                this.fpSpreadItemsSheet.Cells.Get(row, 1).Border = complexBorder2;
                this.fpSpreadItemsSheet.Cells.Get(row, 2).Border = complexBorder3;
                this.fpSpreadItemsSheet.Cells.Get(row, 3).Border = complexBorder4;
                this.fpSpreadItemsSheet.Cells.Get(row, 4).Border = complexBorder5;
                this.fpSpreadItemsSheet.Cells.Get(row, 5).Border = complexBorder6;
                this.fpSpreadItemsSheet.Cells.Get(row, 6).Border = complexBorder7;
                this.fpSpreadItemsSheet.Cells.Get(row, 7).Border = complexBorder8;
                this.fpSpreadItemsSheet.Cells.Get(row, 8).Border = complexBorder9;
                this.fpSpreadItemsSheet.Cells.Get(row, 9).Border = complexBorder9;
                this.fpSpreadItemsSheet.Cells.Get(row, 10).Border = complexBorder9;
                this.fpSpreadItemsSheet.Cells.Get(row, 11).Border = complexBorder9;
                this.fpSpreadItemsSheet.Cells.Get(row, 12).Border = complexBorder9;
                //{ADF172DF-BFC3-459b-B0D8-511D59654573} 添加支付方式
                FS.HISFC.BizLogic.Fee.Outpatient outpatientManager = new FS.HISFC.BizLogic.Fee.Outpatient();
                ArrayList payModes = outpatientManager.QueryBalancePaysByInvoiceSequence((invoices[0] as FS.HISFC.Models.Fee.Outpatient.Balance).CombNO);
                if (payModes == null || payModes.Count <= 0)
                {
                    MessageBox.Show("获取支付方式信息失败！");
                    return;
                }
                string payKind = "";
               
                 /// 常数业务层
        
                FS.HISFC.BizLogic.Manager.Constant constantMgr = new FS.HISFC.BizLogic.Manager.Constant();
                FS.FrameWork.Public.ObjectHelper payModesHelper = new FS.FrameWork.Public.ObjectHelper();
                //支付方式
                if (payModesHelper.ArrayObject == null || payModesHelper.ArrayObject.Count == 0)
                {
                    payModesHelper.ArrayObject = constantMgr.GetList(FS.HISFC.Models.Base.EnumConstant.PAYMODES);
                }
                for (int i = 0; i < payModes.Count; i++)
                {
                    FS.HISFC.Models.Fee.Outpatient.BalancePay payMode = payModes[i] as FS.HISFC.Models.Fee.Outpatient.BalancePay;

                  
                    payKind += " " + payModesHelper.GetObjectFromID(payMode.PayType.ID).Name
                        + " " + FS.FrameWork.Public.String.FormatNumber(payMode.FT.TotCost, 2);
                }
                
                //lbPactName.Font = new Font("宋体", 8);
                //lbPactName.Text = payKind;
                row = row + 1;
                fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.xmdm项目代码].ColumnSpan = 3;
                fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.xmdm项目代码].Text = payKind;
                //fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.gg规格].ColumnSpan = 2;
                //fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.gg规格].Text = payKind;

                fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.gg规格].ColumnSpan = 3;
                fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.gg规格].Text = "打印日期:" + dbMgr.GetDateTimeFromSysDateTime().ToShortDateString();
                //{ADF172DF-BFC3-459b-B0D8-511D59654573} 添加支付方式 结束
                fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.dj单价].ColumnSpan = 2;
                fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.dj单价].Text = "   合计:";

                fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.yj原价].ColumnSpan = 1;
                fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.yj原价].Text = totCost.ToString();
                fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.zj折价].Text = rebateCost.ToString();// {D55B4DFA-DA91-42b0-8163-27036100E89E}
                fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.zhj折后价].Text = ownCost.ToString();
                this.fpSpreadItemsSheet.Rows[row].Height = 120;
            }
        }

        /// <summary>
        /// 存放对照项目
        /// </summary>
        Dictionary<string, Dictionary<string, FS.HISFC.Models.SIInterface.Compare>> dicCompare = null;

        FS.HISFC.BizLogic.Fee.Interface interfaceMgr = new FS.HISFC.BizLogic.Fee.Interface();

        /// <summary>
        /// 获取医保对照项目信息
        /// </summary>
        /// <param name="item.ID">项目编码</param>
        /// <param name="compareItem">对照项目信息</param>
        /// <returns></returns>
        public FS.HISFC.Models.SIInterface.Compare GetCompareItemInfo(string pactCode, string itemCode)
        {
            if (dicCompare == null)
            {
                dicCompare = new Dictionary<string, Dictionary<string, FS.HISFC.Models.SIInterface.Compare>>();
            }

            FS.HISFC.Models.SIInterface.Compare compareItem = null;

            if (dicCompare.ContainsKey(pactCode))
            {
                if (dicCompare[pactCode].ContainsKey(itemCode))
                {
                    return dicCompare[pactCode][itemCode];
                }
                else
                {
                    int rev = interfaceMgr.GetCompareSingleItem(pactCode, itemCode, ref compareItem);
                    if (rev == -1)
                    {
                        //errInfo = "获取医保对照项目失败：" + interfaceMgr.Err;
                        compareItem = null;
                    }
                    else
                    {
                        dicCompare[pactCode].Add(itemCode, compareItem);
                    }
                    return compareItem;
                }
            }
            else
            {
                int rev = interfaceMgr.GetCompareSingleItem(pactCode, itemCode, ref compareItem);
                if (rev == -1)
                {
                    //errInfo = "获取医保对照项目失败：" + interfaceMgr.Err;
                    compareItem = null;
                }
                else
                {
                    Dictionary<string, FS.HISFC.Models.SIInterface.Compare> dicPactCompare = new Dictionary<string, FS.HISFC.Models.SIInterface.Compare>();
                    dicPactCompare.Add(itemCode, compareItem);
                    dicCompare.Add(pactCode, dicPactCompare);
                }
                return compareItem;
            }
        }

        /// <summary>
        /// 打印
        /// </summary>
        public void PrintPage()
        {
            if (fpSpreadItemsSheet.RowCount > 0)
            {
                FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
                //FF883061-A89B-4236-8DEE-8D066FA4F040 设置纸张
                FS.HISFC.BizLogic.Manager.PageSize pMgr = new FS.HISFC.BizLogic.Manager.PageSize();
                FS.HISFC.Models.Base.PageSize ps = null;
                ps = pMgr.GetPageSize("A4");
                if (ps != null)
                {
                    print.SetPageSize(ps);
                }
                //{3E7EFECA-5375-420b-A435-323463A0E56C}

                string printerName = FS.HISFC.Components.Common.Classes.Function.ChoosePrinter();
                
                if (string.IsNullOrEmpty(printerName))
                {
                    return ;
                }
                print.PrintDocument.PrinterSettings.PrinterName = printerName;
                print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
                print.IsDataAutoExtend = false;
                if (FrameWork.WinForms.Classes.Function.IsManager())
                {
                   
                    print.PrintPreview(5, 5, this);
                }
                else
                {
                    //print.PrintPage(5, 5, this);
                    print.PrintPreview(5, 5, this);
                }
            }
        }
    }
}
                #endregion