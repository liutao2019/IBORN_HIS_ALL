using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GJLocal.HISFC.Components.OpGuide.IOutpatientGuide
{
    /// <summary>
    /// 门诊收费清单
    /// </summary>
    public partial class ucFeeDetail2 : UserControl
    {
        public ucFeeDetail2()
        {
            InitializeComponent();
        }

        int row = 0;
        int row2 = 0;
        int num = 0;
        decimal totCost = 0;
        decimal serCost = 0;
        decimal labCost = 0;
        decimal proCost = 0;
        decimal medCost = 0;
        decimal phaCost = 0;
        decimal othCost = 0;

        //enum EnumCol
        //{
        //    kk空空,
        //    xmdm项目代码,
        //    xmmc项目名称,
        //    fpfl发票分类,
        //    gg规格,
        //    yblx医保类型,
        //    dw单位,
        //    dj单价,
        //    sl数量,
        //    je金额
        //}

        private Dictionary<string, string> dicInvoceType = new Dictionary<string, string>();

        FS.HISFC.BizLogic.Manager.DataBase dbMgr = new FS.HISFC.BizLogic.Manager.DataBase();

        public void SetValue(FS.HISFC.Models.Registration.Register rInfo, System.Collections.ArrayList invoices, System.Collections.ArrayList feeDetails)
        {
            #region 作废看看
//            if (feeDetails.Count > 0)
//            {
//                this.lblName.Text = rInfo.Name;

//                this.fpSpreadItemsSheet.RowCount = 1;
//                this.fpSpreadItemsSheet.RowCount += feeDetails.Count + 1;
//                int row = 1;

//                FS.HISFC.Models.Base.PactInfo pact = FS.SOC.HISFC.BizProcess.Cache.Fee.GetPactUnitInfo(rInfo.Pact.ID);

//                decimal totCost = 0;
//                foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItemList in feeDetails)
//                {
//                    lblInvoiceNo.Text = feeItemList.Invoice.ID;
//                    lblFeeDate.Text = feeItemList.FeeOper.OperTime.ToString("yyyy-MM-dd");
//                    lblDoctName.Text = FS.SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(feeItemList.RecipeOper.ID);
//                    lblDept.Text = FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(feeItemList.RecipeOper.Dept.ID);


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
//                        FS.HISFC.Models.Pharmacy.Item phaItem = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(feeItemList.Item.ID);
//                        if (phaItem != null)
//                        {
//                            fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.xmdm项目代码].Text = phaItem.UserCode;
//                            fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.xmmc项目名称].Text = phaItem.Name;
//                            fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.gg规格].Text = phaItem.Specs;
//                            fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.dw单位].Text = phaItem.MinUnit;
//                        }
//                    }
//                    else
//                    {
//                        FS.HISFC.Models.Fee.Item.Undrug undrug = FS.SOC.HISFC.BizProcess.Cache.Fee.GetItem(feeItemList.Item.ID);
//                        if (undrug != null)
//                        {
//                            fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.xmdm项目代码].Text = undrug.UserCode;
//                            fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.xmmc项目名称].Text = undrug.Name;
//                            fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.gg规格].Text = undrug.Specs;
//                        }
//                    }
//                    fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.fpfl发票分类].Text = strTypeName;
//                    fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.yblx医保类型].Text = SIFlag;
//                    fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.dj单价].Text = (feeItemList.Item.Price / feeItemList.Item.PackQty).ToString("f4");
//                    //fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.dw单位].Text = feeItemList.Item.PriceUnit;
//                    fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.sl数量].Text = feeItemList.Item.Qty.ToString();
//                    fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.je金额].Text = (feeItemList.FT.OwnCost + feeItemList.FT.PubCost + feeItemList.FT.PayCost).ToString();

//                    fpSpreadItemsSheet.Rows[row].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

//                    totCost += feeItemList.FT.OwnCost + feeItemList.FT.PubCost + feeItemList.FT.PayCost;

//                    row += 1;
//                }

//                #region 显示设置

//                fpSpreadItemsSheet.Rows[row].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

//                FarPoint.Win.ComplexBorder complexBorder1 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(System.Drawing.SystemColors.WindowFrame, 2));
//                FarPoint.Win.ComplexBorder complexBorder2 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(System.Drawing.SystemColors.WindowFrame, 2));
//                FarPoint.Win.ComplexBorder complexBorder3 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(System.Drawing.SystemColors.WindowFrame, 2));
//                FarPoint.Win.ComplexBorder complexBorder4 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(System.Drawing.SystemColors.WindowFrame, 2));
//                FarPoint.Win.ComplexBorder complexBorder5 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(System.Drawing.SystemColors.WindowFrame, 2));
//                FarPoint.Win.ComplexBorder complexBorder6 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(System.Drawing.SystemColors.WindowFrame, 2));
//                FarPoint.Win.ComplexBorder complexBorder7 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(System.Drawing.SystemColors.WindowFrame, 2));
//                FarPoint.Win.ComplexBorder complexBorder8 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(System.Drawing.SystemColors.WindowFrame, 2));
//                FarPoint.Win.ComplexBorder complexBorder9 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(System.Drawing.SystemColors.WindowFrame, 2));

//                this.fpSpreadItemsSheet.Cells.Get(row - 1, 0).Border = complexBorder1;
//                this.fpSpreadItemsSheet.Cells.Get(row - 1, 1).Border = complexBorder2;
//                this.fpSpreadItemsSheet.Cells.Get(row - 1, 2).Border = complexBorder3;
//                this.fpSpreadItemsSheet.Cells.Get(row - 1, 3).Border = complexBorder4;
//                this.fpSpreadItemsSheet.Cells.Get(row - 1, 4).Border = complexBorder5;
//                this.fpSpreadItemsSheet.Cells.Get(row - 1, 5).Border = complexBorder6;
//                this.fpSpreadItemsSheet.Cells.Get(row - 1, 6).Border = complexBorder7;
//                this.fpSpreadItemsSheet.Cells.Get(row - 1, 7).Border = complexBorder8;
//                this.fpSpreadItemsSheet.Cells.Get(row - 1, 8).Border = complexBorder9;
//                this.fpSpreadItemsSheet.Cells.Get(row - 1, 9).Border = complexBorder9;

//                #endregion
//                fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.xmdm项目代码].ColumnSpan = 3;
//                fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.xmdm项目代码].Text = "打印日期：" + dbMgr.GetDateTimeFromSysDateTime().ToString();

//                fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.dj单价].ColumnSpan = 2;
//                fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.dj单价].Text = "合计：";

//                fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.je金额].ColumnSpan = 1;
//                fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.je金额].Text = totCost.ToString();
            //            }
            #endregion

            this.fpSpreadItemsSheet.ColumnCount = 4;
            this.fpSpreadItemsSheet.Columns[0].Width = 220;
            this.fpSpreadItemsSheet.Columns[1].Width = 120;
            this.fpSpreadItemsSheet.Columns[2].Width = 270;
            this.fpSpreadItemsSheet.Columns[3].Width = 220;
            FarPoint.Win.ComplexBorder complexBorder1 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(Color.Green, 1), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None));
            FarPoint.Win.ComplexBorder complexBorder2 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(Color.Green, 1), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None));
            FarPoint.Win.ComplexBorder complexBorder3 = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(Color.Green, 1));
            
            //this.fpSpreadItemsSheet.Columns[1].Border = new FarPoint.Win.ComplexBorder(new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None), new FarPoint.Win.ComplexBorderSide(System.Drawing.SystemColors.WindowFrame, 1), new FarPoint.Win.ComplexBorderSide(FarPoint.Win.ComplexBorderSideStyle.None));
            this.fpSpreadItemsSheet.Columns[0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpSpreadItemsSheet.Columns[1].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpSpreadItemsSheet.Columns[2].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpSpreadItemsSheet.Columns[3].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            if (feeDetails.Count > 0)
            {
                this.lblName.Text = rInfo.Name;
                this.lblSex.Text = rInfo.Sex.Name.ToString();
                this.lblBirthday.Text = rInfo.Birthday.ToShortDateString();
                //this.lblRecords.Text = rInfo.//病历号
                this.fpSpreadItemsSheet.RowHeader.Visible = false;
                this.fpSpreadItemsSheet.RowCount = 1;
                //this.fpSpreadItemsSheet.RowCount += feeDetails.Count + 1;
                this.fpSpreadItemsSheet.RowCount += feeDetails.Count + 17;

                FS.HISFC.Models.Base.PactInfo pact = FS.SOC.HISFC.BizProcess.Cache.Fee.GetPactUnitInfo(rInfo.Pact.ID);

                //feeDetails.Sort();
                //fpSpreadItemsSheet.Columns[(Int32)EnumCol.jcxm检查项目].Visible = false;
                //fpSpreadItemsSheet.Columns[(Int32)EnumCol.jcfy检查费用].Visible = false;
                //fpSpreadItemsSheet.Columns[2].Visible = false;
                //fpSpreadItemsSheet.Columns[3].Visible = false;

                String Textname1 = "SERVICES 診療項目";
                Project_Cycle(feeDetails, Textname1, "MC", row);

                String Textname2 = "LABORATORY 檢驗項目";
                Project_Cycle(feeDetails, Textname2, "UL", row);

                String Textname3 = "PROCEDURES 治療費";
                Project_Cycle(feeDetails, Textname3, "UZ", row);

                String Textname4 = "MEDICALIMAGING 影像檢查";
                Project_Cycle(feeDetails, Textname4, "UC", row);

                fpSpreadItemsSheet.Cells[row2, 2].ColumnSpan = 2;
                fpSpreadItemsSheet.Cells[row2, 2].Text = "PHARMACY 藥品";
                fpSpreadItemsSheet.Cells[row2, 2].BackColor = Color.Green;
                fpSpreadItemsSheet.Cells[row2, 2].ForeColor = Color.White;
                fpSpreadItemsSheet.Cells[row2, 2].Font = new System.Drawing.Font("宋体", 12F);
                row2 += 1;
                foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItemList in feeDetails)
                {
                    lblFeeDate.Text = feeItemList.FeeOper.OperTime.ToString("yyyy Y/年MM M/月dd D/日");
                    if (feeItemList.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        FS.HISFC.Models.Pharmacy.Item phaItem = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(feeItemList.Item.ID);
                        if (phaItem.SysClass.ID.Equals("P") || phaItem.SysClass.ID.Equals("PCC") || phaItem.SysClass.ID.Equals("PCZ"))
                        {
                            fpSpreadItemsSheet.Cells[row2, 2].Text = phaItem.Name;
                            fpSpreadItemsSheet.Cells[row2, 3].Text = "￥ " + (feeItemList.FT.OwnCost + feeItemList.FT.PubCost + feeItemList.FT.PayCost).ToString();
                            phaCost += feeItemList.FT.OwnCost + feeItemList.FT.PubCost + feeItemList.FT.PayCost;
                            row2 += 1;
                        }
                    }
                }
                row2 += 1;

                fpSpreadItemsSheet.Cells[row2, 2].ColumnSpan = 2;
                fpSpreadItemsSheet.Cells[row2, 2].Text = "OTHERS 其它";
                fpSpreadItemsSheet.Cells[row2, 2].BackColor = Color.Green;
                fpSpreadItemsSheet.Cells[row2, 2].ForeColor = Color.White;
                fpSpreadItemsSheet.Cells[row2, 2].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                fpSpreadItemsSheet.Cells[row2, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                fpSpreadItemsSheet.Cells[row2, 2].Font = new System.Drawing.Font("宋体", 12F);
                row2 += 1;
                foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItemList in feeDetails)
                {
                    lblFeeDate.Text = feeItemList.FeeOper.OperTime.ToString("yyyy Y/年MM M/月dd D/日");
                    if (feeItemList.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        FS.HISFC.Models.Pharmacy.Item phaItem = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(feeItemList.Item.ID);
                        if (phaItem.SysClass.ID.Equals("UT"))
                        {
                            fpSpreadItemsSheet.Cells[row2, 2].Text = phaItem.Name;
                            fpSpreadItemsSheet.Cells[row2, 3].Text = "￥ " + (feeItemList.FT.OwnCost + feeItemList.FT.PubCost + feeItemList.FT.PayCost).ToString();
                            phaCost += feeItemList.FT.OwnCost + feeItemList.FT.PubCost + feeItemList.FT.PayCost;
                            row2 += 1;
                        }
                    }
                }
                this.fpSpreadItemsSheet.Cells.Get(row2 - 1, 2, row2, 3).Border = complexBorder3;
                row2 += 1;

                fpSpreadItemsSheet.Cells[row2, 2].Text = "DIAGNOSIS 診斷";
                fpSpreadItemsSheet.Cells[row2, 2].BackColor = Color.Green;
                fpSpreadItemsSheet.Cells[row2, 2].ForeColor = Color.White;
                fpSpreadItemsSheet.Cells[row2, 2].Font = new System.Drawing.Font("宋体", 12F);
                row2 += 5;
                fpSpreadItemsSheet.Cells[row2, 2].Text = "Doctor's Signature";
                row2 += 1;
                fpSpreadItemsSheet.Cells[row2, 2].Text = "醫生簽名    __________________";
                row2 += 1;
                this.fpSpreadItemsSheet.Cells.Get(row2 - 1, 2, row2, 3).Border = complexBorder3;

                int row2_2 = 0;
                row2_2 = row2;
                this.fpSpreadItemsSheet.Cells.Get(row2, 1, row2 + 9, 2).Border = complexBorder1;
                fpSpreadItemsSheet.Cells[row2, 2].Text = "Consultation 診療費     ￥" + serCost;
                row2 += 1;
                fpSpreadItemsSheet.Cells[row2, 2].Text = "Laboratory 檢驗費     ￥" + labCost;
                row2 += 1;
                fpSpreadItemsSheet.Cells[row2, 2].Text = "Mcdical Imaging 醫學影像費     ￥" + medCost;
                row2 += 1;
                fpSpreadItemsSheet.Cells[row2, 2].Text = "Injection 注射費     ￥" + proCost;
                row2 += 1;
                fpSpreadItemsSheet.Cells[row2, 2].Text = "Pharmacy 藥費     ￥" + phaCost;
                row2 += 1;
                fpSpreadItemsSheet.Cells[row2, 2].Text = "Treatment 治療費     ￥" + proCost;
                row2 += 1;
                fpSpreadItemsSheet.Cells[row2, 2].Text = "Others 其它     ￥" + othCost;
                row2 += 2;
                decimal sumCost = 0;
                sumCost = serCost + labCost + proCost + medCost + medCost + phaCost + othCost;
                fpSpreadItemsSheet.Cells[row2, 2].Text = "Sum Of Charges 總費用   RMB" + sumCost;
                row2 += 1;

                fpSpreadItemsSheet.Cells[row2_2, 3].Text = "PAYMENT METHOD 付款方式";
                fpSpreadItemsSheet.Cells[row2_2, 3].BackColor = Color.Green;
                fpSpreadItemsSheet.Cells[row2_2, 3].ForeColor = Color.White;
                fpSpreadItemsSheet.Cells[row2_2, 3].Font = new System.Drawing.Font("宋体", 12F);
                row2_2 += 1;
                fpSpreadItemsSheet.Cells[row2_2, 3].Text = "Cash現金：US$ 美元";
                row2_2 += 1;


                if (row > row2) { num = row; } else { num = row2; }
                #region 显示设置

                fpSpreadItemsSheet.Rows[row].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

                #endregion

                this.fpSpreadItemsSheet.Cells.Get(0, 0, num - 1, 0).Border = complexBorder1;
                this.fpSpreadItemsSheet.Cells.Get(num - 2, 0, num - 1, 3).Border = complexBorder2;

                fpSpreadItemsSheet.Cells[num, 0].ColumnSpan = 2;
                fpSpreadItemsSheet.Cells[num, 0].Text = "Patient's Signature";
                num += 1;
                fpSpreadItemsSheet.Cells[num, 0].ColumnSpan = 2;   
                fpSpreadItemsSheet.Cells[num, 0].Text = "患者簽名:_______________________";
                this.fpSpreadItemsSheet.RowCount = num+1;
            }
        }

        private void Project_Cycle(System.Collections.ArrayList feeDetails, string text_name, string str_id, int oth)
        {
            fpSpreadItemsSheet.Cells[oth, 0].ColumnSpan = 2;
            fpSpreadItemsSheet.Cells[oth, 0].Text = text_name;
            fpSpreadItemsSheet.Cells[oth, 0].BackColor = Color.Green;
            fpSpreadItemsSheet.Cells[oth, 0].ForeColor = Color.White;
            fpSpreadItemsSheet.Cells[oth, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            fpSpreadItemsSheet.Cells[oth, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            fpSpreadItemsSheet.Cells[oth, 0].Font = new System.Drawing.Font("宋体", 12F);
            oth += 1;
            foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItemList in feeDetails)
            {
                lblFeeDate.Text = feeItemList.FeeOper.OperTime.ToString("yyyy Y/年MM M/月dd D/日");
                if (feeItemList.Item.ItemType != FS.HISFC.Models.Base.EnumItemType.Drug)
                {
                    FS.HISFC.Models.Fee.Item.Undrug undrug = FS.SOC.HISFC.BizProcess.Cache.Fee.GetItem(feeItemList.Item.ID);
                    if (undrug.SysClass.ID.Equals(str_id))
                    {
                        fpSpreadItemsSheet.Cells[oth, 0].Text = undrug.Name;
                        fpSpreadItemsSheet.Cells[oth, 1].Text = "￥ " + (feeItemList.FT.OwnCost + feeItemList.FT.PubCost + feeItemList.FT.PayCost).ToString();
                        serCost += feeItemList.FT.OwnCost + feeItemList.FT.PubCost + feeItemList.FT.PayCost;
                        oth += 1;
                    }
                }
            }
            oth += 1;
            row = oth;
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

                print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
                print.IsDataAutoExtend = false;
                if (FS.FrameWork.WinForms.Classes.Function.IsManager())
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
