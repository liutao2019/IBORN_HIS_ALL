using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace GJLocal.HISFC.Components.OpGuide.InDrugStore.InPatient.IBORN
{
    /// <summary>
    /// [功能描述: 住院药房明细单打印本地化实例]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2010-12]<br></br>
    /// 说明：
    /// 1、作为一个例子保留下来，不要更改
    /// 2、各项目如果修改不大的话，可以考虑继承方式
    /// </summary>
    public partial class ucAnestheticDrugBill : UserControl, FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IInpatientBill
    {
        /// <summary>
        /// 明细打印摆药单
        /// </summary>
        public ucAnestheticDrugBill()
        {
            InitializeComponent();
            this.PrintDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(PrintDocumentPrintPage);
        }

        #region 变量

        /// <summary>
        /// 每页的行数，这个是按照LetterpageRowNum，行高改变影响分页
        /// </summary>
        int pageRowNum = 14;

        /// <summary>
        /// 最大页码
        /// </summary>
        int totPageNO = 0;

        /// <summary>
        /// 打印的有效行数,当选择页码范围时有效
        /// </summary>
        int validRowNum = 0;

        /// <summary>
        /// 当前打印页的页码
        /// 程序自动计算的
        /// </summary>
        private int curPageNO = 1;

        /// <summary>
        /// 本次打印最大页码
        /// 程序自动计算的
        /// </summary>
        private int maxPageNO = 1;


        /// <summary>
        /// 打印用
        /// </summary>
        private System.Drawing.Printing.PrintDocument PrintDocument = new System.Drawing.Printing.PrintDocument();

        private System.Drawing.Printing.Margins DrawingMargins = new System.Drawing.Printing.Margins(20, 20, 10, 30);
        
        private FS.HISFC.BizLogic.Order.Order orderMgr = new FS.HISFC.BizLogic.Order.Order();
        
        FS.SOC.Windows.Forms.PrintPageSelectDialog socPrintPageSelectDialog = new FS.SOC.Windows.Forms.PrintPageSelectDialog();

        /// <summary>
        /// 获取诊断用
        /// {1BBD2F14-49A6-468b-BB08-19BF0499CEF4}
        /// </summary>
        FS.HISFC.BizLogic.HealthRecord.Diagnose diagManager = new FS.HISFC.BizLogic.HealthRecord.Diagnose();
        #endregion

        #region 摆药单的通用方法

        /// <summary>
        /// 清屏
        /// </summary>
        private void Clear()
        {
            this.nlbTitle.Text = FS.FrameWork.Management.Connection.Hospital.Name;
            this.nlbRowCount.Text = "记录数：";
            this.nlbBillNO.Text = "摆药单号：";
            this.lblOrderDate.Text = "医嘱时间：";
            this.neuSpread1_Sheet1.Rows.Count = 0;
        }

        private void PrintDocumentPrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //跳过选择打印范围外的数据
            while (this.curPageNO < this.socPrintPageSelectDialog.FromPageNO && this.curPageNO < this.maxPageNO)
            {
                curPageNO++;
            }

            if (this.curPageNO > this.maxPageNO || this.curPageNO > socPrintPageSelectDialog.ToPageNO)
            {
                this.curPageNO = 1;
                this.maxPageNO = 1;
                e.HasMorePages = false;
                return;
            }

            Graphics graphics = e.Graphics;

            #region 标题绘制
            int mainTitleLocalX = this.DrawingMargins.Left + this.nlbTitle.Location.X;
            int mainTitleLoaclY = this.DrawingMargins.Top + this.nlbTitle.Location.Y;
            graphics.DrawString(this.nlbTitle.Text, this.nlbTitle.Font, new SolidBrush(this.nlbTitle.ForeColor), mainTitleLocalX, mainTitleLoaclY);

            int lblTitleNameLocalX = this.DrawingMargins.Left + this.lblTitleName.Location.X;
            int lblTitleNameTitleLoaclY = this.DrawingMargins.Top + this.lblTitleName.Location.Y;
            graphics.DrawString(this.lblTitleName.Text, this.lblTitleName.Font, new SolidBrush(this.lblTitleName.ForeColor), lblTitleNameLocalX, lblTitleNameTitleLoaclY);

            //{83DABC8A-E421-45ea-85DC-4C28B97BBBE1}
            if (nlbReprint.Visible)
            {
                int nlbReprintLocalX = this.DrawingMargins.Left + this.nlbReprint.Location.X;
                int nlbReprintLoaclY = this.DrawingMargins.Top + this.nlbReprint.Location.Y;
                graphics.DrawString(this.nlbReprint.Text, this.nlbReprint.Font, new SolidBrush(this.nlbReprint.ForeColor), nlbReprintLocalX, nlbReprintLoaclY);
            }

            int additionTitleLocalX = this.DrawingMargins.Left + this.nlbNurseCell.Location.X;
            int additionTitleLocalY = this.DrawingMargins.Top + this.nlbNurseCell.Location.Y;

            graphics.DrawString(this.nlbNurseCell.Text, this.nlbNurseCell.Font, new SolidBrush(this.nlbNurseCell.ForeColor), additionTitleLocalX, additionTitleLocalY);

            additionTitleLocalX = this.DrawingMargins.Left + this.nlbBillNO.Location.X;
            additionTitleLocalY = this.DrawingMargins.Top + this.nlbBillNO.Location.Y;

            graphics.DrawString(this.nlbBillNO.Text, this.nlbBillNO.Font, new SolidBrush(this.nlbBillNO.ForeColor), additionTitleLocalX, additionTitleLocalY);

            additionTitleLocalX = this.DrawingMargins.Left + this.lblOrderDate.Location.X;
            additionTitleLocalY = this.DrawingMargins.Top + this.lblOrderDate.Location.Y;

            graphics.DrawString(this.lblOrderDate.Text, this.lblOrderDate.Font, new SolidBrush(this.lblOrderDate.ForeColor), additionTitleLocalX, additionTitleLocalY);

            additionTitleLocalX = this.DrawingMargins.Left + this.nlbRowCount.Location.X;
            additionTitleLocalY = this.DrawingMargins.Top + this.nlbRowCount.Location.Y;

            graphics.DrawString(this.nlbRowCount.Text, this.nlbRowCount.Font, new SolidBrush(this.nlbRowCount.ForeColor), additionTitleLocalX, additionTitleLocalY);

            #endregion

            #region Farpoint绘制
            int drawingWidth = 870 - this.DrawingMargins.Left - this.DrawingMargins.Right;
            int drawingHeight = 550 - this.DrawingMargins.Top - this.DrawingMargins.Bottom - this.neuPanel1.Height;
            this.neuSpread1.OwnerPrintDraw(graphics, new Rectangle(this.DrawingMargins.Left, this.DrawingMargins.Top + this.neuPanel1.Height, drawingWidth, drawingHeight), 0, this.curPageNO);

            #endregion

            #region 页码绘制

            additionTitleLocalX = this.DrawingMargins.Left + this.nlbPageNo.Location.X;
            additionTitleLocalY = this.DrawingMargins.Top + this.nlbPageNo.Location.Y;

            graphics.DrawString("页码：" + this.curPageNO.ToString() + "/" + this.maxPageNO.ToString(), this.nlbPageNo.Font, new SolidBrush(this.nlbPageNo.ForeColor), additionTitleLocalX, additionTitleLocalY);

            #endregion

            #region 分页
            if (this.curPageNO < this.socPrintPageSelectDialog.ToPageNO && this.curPageNO < maxPageNO)
            {
                e.HasMorePages = true;
                curPageNO++;
            }
            else
            {
                curPageNO = 1;
                maxPageNO = 1;
                e.HasMorePages = false;
            }
            #endregion
        }

        /// <summary>
        /// 设置列
        /// </summary>
        //private void SetFormat()
        //{
        //    this.neuSpread1.ReadSchema(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\InpatientDrugStoreDetDrugBill.xml");

        //}

        /// <summary>
        /// 这实在没有意义，和汇总单统一罢了
        /// </summary>
        /// <param name="alData"></param>
        /// <param name="drugBillClass"></param>
        /// <param name="stockDept"></param>
        private void ShowBillData(ArrayList alData, FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass, FS.FrameWork.Models.NeuObject stockDept)
        {
            this.Clear();
            this.ShowDetailData(alData, drugBillClass, stockDept);
        }

        /// <summary>
        /// 数据显示
        /// </summary>
        /// <param name="alData"></param>
        /// <param name="drugBillClass"></param>
        public void ShowDetailData(ArrayList alData, FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass, FS.FrameWork.Models.NeuObject stockDept)
        {
            //单元格线形

            #region farpoint设置
            FarPoint.Win.LineBorder noneBorder = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, false, false, false, false);
            FarPoint.Win.LineBorder topBorder = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, false, true, false, false);
            FarPoint.Win.LineBorder bottomBorder = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, false, false, false, true);
            FarPoint.Win.LineBorder noRightBorder = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, true, true, false, false);
            FarPoint.Win.LineBorder noBottomBorder = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, true, true, true, false);
            
            #endregion
            

            string applyDeptName = FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(drugBillClass.ApplyDept.ID);

            string sendType = this.GetSendType(alData[0] as FS.HISFC.Models.Pharmacy.ApplyOut);

            if (drugBillClass.ID == "R")
            {
                this.lblTitleName.Text = "退药单";
            }
            else if (drugBillClass.ID == "O")
            {
                this.lblTitleName.Text = "出院带药";
            }
            else
            {
                if (drugBillClass.ID == "ZDP2") //{AA5A1F7C-9D5E-4cf9-80AB-E86CC29B9CA6}
                    this.lblTitleName.Text = "精二药品重点品种摆药单(明细)";//+ "(" + sendType + ")";
                else
                    this.lblTitleName.Text = drugBillClass.Name + "(明细)";//+ "(" + sendType + ")";
            }
            this.nlbTitle.Location = new Point((this.Width - this.nlbTitle.Width) / 2, this.nlbTitle.Location.Y);
            this.lblTitleName.Location = new Point((this.Width - this.lblTitleName.Width) / 2, this.lblTitleName.Location.Y);
            
            this.nlbBillNO.Text = "摆药单号：" + drugBillClass.DrugBillNO;
           this.nlbNurseCell.Text = "病区：" + applyDeptName;

           if (FS.FrameWork.Function.NConvert.ToInt32(drugBillClass.ApplyState) != 0)
           {
               this.nlbReprint.Visible = true;
           }
           else
           {
               this.nlbReprint.Visible = false;
           }
           //#region 对同一医嘱按用药时间组合显示

           #region 按患者排序
            alData.Sort(new CompareApplyOutByPatientNO());


            //alData.Sort(new CompareApplyOutByPatient());
            #endregion

            FS.HISFC.BizLogic.RADT.InPatient inpatientManager = new FS.HISFC.BizLogic.RADT.InPatient();
            DateTime dt = inpatientManager.GetDateTimeFromSysDateTime();

            string orderId = "";

            #region 设置数据

            int count = 0;
            int iRow = 0;

            Hashtable hsApplyInfo = new Hashtable();

            ArrayList difPatient = new ArrayList();

            ArrayList allOrderDate = new ArrayList();

            ArrayList patientData = new ArrayList();
            //按患者分组申请信息
            foreach (FS.HISFC.Models.Pharmacy.ApplyOut applyInfo in alData)
            {
                if (hsApplyInfo.Contains(applyInfo.PatientNO))
                {
                    ArrayList alPatientData = hsApplyInfo[applyInfo.PatientNO] as ArrayList;
                    alPatientData.Add(applyInfo);
                }
                else
                {
                    ArrayList alPatientData = new ArrayList();
                    alPatientData.Add(applyInfo);
                    hsApplyInfo.Add(applyInfo.PatientNO, alPatientData);
                    difPatient.Add(applyInfo.PatientNO);
                }

                if (!allOrderDate.Contains(applyInfo.UseTime) && applyInfo.UseTime!=DateTime.MinValue)
                {
                    allOrderDate.Add(applyInfo.UseTime);
                }
                else if (applyInfo.UseTime == DateTime.MinValue && !allOrderDate.Contains(applyInfo.Operation.ApplyOper.OperTime))
                {
                    allOrderDate.Add(applyInfo.Operation.ApplyOper.OperTime);
                }
            }


            allOrderDate.Sort(new CompareOrderDate());
            int id = 1;


            //this.SuspendLayout();

            //((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).BeginInit();
            //((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).BeginInit();
            this.neuSpread1_Sheet1.RowCount = 0;
            difPatient.Sort(new CompareStringByPatientNO());

            Dictionary<string,string> patientDiag = new Dictionary<string,string>();

            foreach (string patintid in difPatient)
            {

                FS.HISFC.Models.RADT.PatientInfo p = inpatientManager.QueryPatientInfoByInpatientNO(patintid);

                patientData = hsApplyInfo[patintid] as ArrayList;

                ArrayList newAllData = new ArrayList();
                foreach (FS.HISFC.Models.Pharmacy.ApplyOut applyInfo in patientData)
                {
                    System.Collections.Hashtable hsTotData = new Hashtable();


                    if (hsTotData.Contains(applyInfo.OrderNO))
                    {
                        FS.HISFC.Models.Pharmacy.ApplyOut applyOutTot = hsTotData[applyInfo.OrderNO] as FS.HISFC.Models.Pharmacy.ApplyOut;
                        applyOutTot.Operation.ApplyQty += applyInfo.Operation.ApplyQty;
                        applyOutTot.User03 = this.FormatDateTime(FS.FrameWork.Function.NConvert.ToDateTime(applyInfo.UseTime), dt) + "," + applyOutTot.User03;
                    }
                    else
                    {
                        FS.HISFC.Models.Pharmacy.ApplyOut applyOutTot = applyInfo.Clone();
                        applyOutTot.User01 = FS.FrameWork.Function.NConvert.ToInt32(!FS.SOC.HISFC.BizProcess.Cache.Pharmacy.isValueableItem(applyOutTot.StockDept.ID, applyOutTot.Item.ID)).ToString();
                        applyOutTot.User03 = this.FormatDateTime(FS.FrameWork.Function.NConvert.ToDateTime(applyInfo.UseTime), dt);
                        hsTotData.Add(applyInfo.OrderNO, applyOutTot);
                        newAllData.Add(applyOutTot);
                    }
                }
                count += newAllData.Count;
                int spanCount = newAllData.Count;
                foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in newAllData)
                {

                    FS.HISFC.Models.Order.Inpatient.Order order = new FS.HISFC.Models.Order.Inpatient.Order();
                    order = orderMgr.QueryOneOrder(info.OrderNO);

                    string bedNO = info.BedNO;
                    if (bedNO.Length > 4)
                    {
                        bedNO = bedNO.Substring(4);
                    }

                    if (drugBillClass.ID == "R")
                    {

                        if (p != null && p.PVisit.InState.ID.ToString() == "2")
                        {
                            bedNO = "*" + bedNO;
                        }
                    }

                    string age = "";
                    try
                    {
                        age = inpatientManager.GetAge(p.Birthday);
                    }
                    catch { }

                    decimal applyQty = info.Operation.ApplyQty;
                    string unit = info.Item.MinUnit;
                    decimal price = info.Item.PriceCollection.RetailPrice;


                    int outMinQty;
                    int outPackQty = System.Math.DivRem((int)(info.Operation.ApplyQty * info.Days), (int)info.Item.PackQty, out outMinQty);
                    if (string.IsNullOrEmpty(info.Item.PackUnit))
                    {
                        if (info.Item.PackQty == 1)
                        {
                            info.Item.PackUnit = info.Item.MinUnit;
                        }
                        else
                        {
                            try
                            {
                                info.Item.PackUnit = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(info.Item.ID).PackUnit;
                            }
                            catch { }
                        }
                    }
                    if (outPackQty == 0)
                    {
                        applyQty = info.Operation.ApplyQty;
                        unit = info.Item.MinUnit;
                        price = info.Item.PriceCollection.RetailPrice / info.Item.PackQty;
                    }
                    else if (outMinQty == 0)
                    {
                        applyQty = outPackQty;
                        unit = info.Item.PackUnit;
                    }
                    else
                    {
                        applyQty = outPackQty;
                        unit = info.Item.PackUnit + outMinQty.ToString() + info.Item.MinUnit;
                    }
                    this.neuSpread1_Sheet1.Rows.Add(iRow, 1);
                    this.neuSpread1_Sheet1.Columns.Get(iRow).Font = new Font("宋体", 9.75f);
                    this.neuSpread1.SetCellValue(0, iRow, "序号", id.ToString() + ".");
                    this.neuSpread1.SetCellValue(0, iRow, "床号", bedNO);

                    this.neuSpread1.SetCellValue(0, iRow, "住院号", p.PID.PatientNO.TrimStart('0'));
                    this.neuSpread1.SetCellValue(0, iRow, "姓名", info.PatientName);
                    this.neuSpread1.SetCellValue(0, iRow, "性别", p.Sex.Name);
                    this.neuSpread1.SetCellValue(0, iRow, "年龄", age);
                    this.neuSpread1.SetCellValue(0, iRow, "身份证号", p.IDCard);
                    this.neuSpread1.SetCellValue(0, iRow, "药品名称", info.Item.Name);
                    this.neuSpread1.SetCellValue(0, iRow, "规格", info.Item.Specs);
                    this.neuSpread1.SetCellValue(0, iRow, "数量", applyQty.ToString("F4").TrimEnd('0').TrimEnd('.') + " " + unit);
                    //{738625BE-10F2-41cf-AC76-A2A1AA54307F}
                    if (order != null)
                    {
                        //{49B87E35-4446-4f3b-B6E8-2B891C00A03A}
                        this.neuSpread1.SetCellValue(0, iRow, "处方号", order.ID.ToString());
                        this.neuSpread1.SetCellValue(0, iRow, "处方医生", order.ReciptDoctor.Name);
                    }
                    else
                    {
                        //{49B87E35-4446-4f3b-B6E8-2B891C00A03A}
                        this.neuSpread1.SetCellValue(0, iRow, "处方流水号", "");
                        this.neuSpread1.SetCellValue(0, iRow, "处方医生", FS.SOC.HISFC.BizProcess.Cache.Common.GetEmployeeInfo(info.RecipeInfo.ID).Name);
                    }

                    if (patientDiag.ContainsKey(patintid))
                    {
                        this.neuSpread1.SetCellValue(0, iRow, "诊断", patientDiag[patintid]);

                    }
                    else
                    {
                        FS.FrameWork.Management.PublicTrans.BeginTransaction();
                        try
                        {
                            //{9B667CF2-C2FC-4e91-BCBD-5D458EEE4217}
                            ArrayList al = diagManager.QueryCaseDiagnoseFromEmrView(p.ID);

                            FS.FrameWork.Management.PublicTrans.Commit();

                            if (al != null && al.Count > 0)
                            {
                                FS.HISFC.Models.HealthRecord.Diagnose DiagnoseTemp = al[0] as FS.HISFC.Models.HealthRecord.Diagnose;
                                this.neuSpread1.SetCellValue(0, iRow, "诊断", DiagnoseTemp.DiagInfo.ICD10.Name);
                                patientDiag.Add(patintid, DiagnoseTemp.DiagInfo.ICD10.Name);
                            }
                            //this.neuSpread1.SetCellValue(0, iRow, "诊断", Common.Function.GetInpatientDiagnose(p.PID.ID).Replace("-1", string.Empty));
                        }
                        catch
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                        }
                    }
                    
                    this.neuSpread1_Sheet1.Cells[iRow, 0].Border = noRightBorder;
                    this.neuSpread1_Sheet1.Cells[iRow, 1].Border = noRightBorder;
                    this.neuSpread1_Sheet1.Cells[iRow, 2].Border = noRightBorder;
                    this.neuSpread1_Sheet1.Cells[iRow, 3].Border = noRightBorder;
                    this.neuSpread1_Sheet1.Cells[iRow, 4].Border = noRightBorder;
                    this.neuSpread1_Sheet1.Cells[iRow, 5].Border = noRightBorder;
                    this.neuSpread1_Sheet1.Cells[iRow, 6].Border = noRightBorder;
                    this.neuSpread1_Sheet1.Cells[iRow, 7].Border = noRightBorder;
                    this.neuSpread1_Sheet1.Cells[iRow, 8].Border = noRightBorder;
                    this.neuSpread1_Sheet1.Cells[iRow, 9].Border = noRightBorder;
                    this.neuSpread1_Sheet1.Cells[iRow, 10].Border = noRightBorder;
                    this.neuSpread1_Sheet1.Cells[iRow, 11].Border = noRightBorder;
                    this.neuSpread1_Sheet1.Cells[iRow, 12].Border = noBottomBorder;
                    iRow++;
                    id++;
                }

                this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - spanCount, 1).RowSpan = spanCount;
                this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - spanCount, 2).RowSpan = spanCount;
                this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - spanCount, 3).RowSpan = spanCount;
                this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - spanCount, 4).RowSpan = spanCount;
                this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - spanCount, 5).RowSpan = spanCount;
                this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - spanCount, 6).RowSpan = spanCount;

            }


            //((System.ComponentModel.ISupportInitialize)(this.neuSpread1)).EndInit();
            //((System.ComponentModel.ISupportInitialize)(this.neuSpread1_Sheet1)).EndInit();
            DateTime startTime = new DateTime();
            DateTime enTime = new DateTime();
            if (allOrderDate.Count > 0)
            {
                startTime = DateTime.Parse(allOrderDate[0].ToString());

                enTime = DateTime.Parse(allOrderDate[allOrderDate.Count - 1].ToString());
            }
            this.lblOrderDate.Text = "医嘱时间：" + startTime.ToShortDateString() + " 至 " + enTime.ToShortDateString();

            this.nlbRowCount.Text = "记录数：" + count.ToString();
            #region 设置底部文字
            try
            {
                if (this.neuSpread1_Sheet1.Rows.Count > 0)
                {
                    int index = this.neuSpread1_Sheet1.Rows.Count;
                    totPageNO = (int)Math.Ceiling((double)index / pageRowNum);
                    for (int page = totPageNO; page > 0; page--)
                    {
                        if (page == totPageNO)
                        {

                            this.neuSpread1_Sheet1.AddRows(index, 1);
                            //打印单底部文字
                           
                            this.neuSpread1_Sheet1.Cells[index, 0].ColumnSpan = this.neuSpread1_Sheet1.Columns.Count;
                            this.neuSpread1_Sheet1.Cells[index, 0].Border = topBorder;

                            //{CA91E91E-54C5-4afd-B44D-E9114D53172B}

                            this.neuSpread1_Sheet1.Cells[index, 0].Text = "□医嘱审核/调配药师：                        □医嘱核对/□发药药师：                        取药人：";
                            this.neuSpread1_Sheet1.Cells[index, 0].Font = new Font("宋体", 10f);
                            this.neuSpread1_Sheet1.Cells[index, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                            this.neuSpread1_Sheet1.Rows[index].Tag = page;
                            //this.neuSpread1_Sheet1.Cells[index, 1].Border = topBorder;
                            //this.neuSpread1_Sheet1.Cells[index, 2].Border = topBorder;
                            //this.neuSpread1_Sheet1.Cells[index, 3].Border = topBorder;
                            //this.neuSpread1_Sheet1.Cells[index, 4].Border = topBorder;
                            //this.neuSpread1_Sheet1.Cells[index, 5].Border = topBorder;
                            //this.neuSpread1_Sheet1.Cells[index, 6].Border = topBorder;
                            continue;
                        }

                        //标记页码，补打选择页码时用
                        this.neuSpread1_Sheet1.Rows[index].Tag = page;
                    }
                }
                this.neuSpread1_Sheet1.Rows.Get(0).Border = topBorder;

            #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            #endregion
            //this.ResumeLayout(true);
        }

        /// <summary>
        /// 获取发送类型
        /// </summary>
        /// <param name="applyOut"></param>
        /// <returns></returns>
        private string GetSendType(FS.HISFC.Models.Pharmacy.ApplyOut applyOut)
        {
            string sendType = string.Empty;
            switch (applyOut.SendType)
            { 
                case 1:
                    sendType = "集中";
                    break;
                case 2:
                    sendType = "临时";
                    break;
                case 4:
                    sendType = "紧急";
                    break;
            }
            return sendType;
        }

        private void SetPaperSize(System.Drawing.Printing.PaperSize paperSize)
        {
            if (paperSize == null)
            {
                paperSize = new System.Drawing.Printing.PaperSize("InPatientDrugBillD", 800, 550);
            }

            this.PrintDocument.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize(paperSize.PaperName, paperSize.Width, paperSize.Height);
            this.PrintDocument.PrinterSettings.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize(paperSize.PaperName, paperSize.Width, paperSize.Height);
        }

        private void myPrintView()
        {
            PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();
            printPreviewDialog.Document = this.PrintDocument;
            try
            {
                ((Form)printPreviewDialog).WindowState = FormWindowState.Maximized;
            }
            catch { }
            try
            {
                printPreviewDialog.ShowDialog();
                printPreviewDialog.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("打印机报错！" + ex.Message);
            }
        }

        /// <summary>
        /// 打印页码选择
        /// </summary>
        private bool ChoosePrintPageNO(Graphics graphics)
        {

            int drawingWidth = 870 - this.DrawingMargins.Left - this.DrawingMargins.Right;
            int drawingHeight = 550 - this.DrawingMargins.Top - this.DrawingMargins.Bottom - this.neuPanel1.Height;

            FarPoint.Win.Spread.PrintInfo printInfo = new FarPoint.Win.Spread.PrintInfo();
            printInfo.ShowBorder = false;
            printInfo.PrintType = FarPoint.Win.Spread.PrintType.All;
            printInfo.ShowRowHeaders = this.neuSpread1_Sheet1.RowHeader.Visible;
            this.neuSpread1_Sheet1.PrintInfo = printInfo;
            this.maxPageNO = neuSpread1.GetOwnerPrintPageCount(graphics, new Rectangle(this.DrawingMargins.Left, this.DrawingMargins.Top + this.neuPanel1.Height, drawingWidth, drawingHeight), 0);

            socPrintPageSelectDialog.MaxPageNO = this.maxPageNO;
            if (this.maxPageNO > 1)
            {
                socPrintPageSelectDialog.StartPosition = FormStartPosition.CenterScreen;
                socPrintPageSelectDialog.ShowDialog();
                if (socPrintPageSelectDialog.ToPageNO == 0)
                {
                    return false;
                }
            }

            return true;
        }

        protected void PrintView()
        {
            this.SetPaperSize(null);
            this.myPrintView();
        }

        /// <summary>
        /// 打印
        /// </summary>
        private void PrintPage()
        {
            System.Drawing.Printing.PaperSize paperSize = null;
            this.SetPaperSize(paperSize);
            if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
            {
                if (this.ChoosePrintPageNO(Graphics.FromHwnd(this.Handle)))
                {
                    this.PrintView();
                }
            }
            else
            {
                if (this.ChoosePrintPageNO(Graphics.FromHwnd(this.Handle)))
                {
                    this.SetPaperSize(paperSize);
                    this.PrintDocument.Print();
                }
            }

        }

        #endregion

        #region 明细单的特殊方法

        /// <summary>
        /// 获取频次代表的每天次数
        /// </summary>
        /// <param name="frequencyID"></param>
        /// <returns></returns>
        private int GetFrequencyCount(string frequencyID)
        {
            return 1000;

            //南庄不分行
            if (string.IsNullOrEmpty(frequencyID))
            {
                return 1000;
            }
            string id = frequencyID.ToLower();
            if (id == "qd")//每天一次
            {
                return 1;
            }
            else if (id == "bid")//每天两次
            {
                return 2;
            }
            else if (id == "tid")//每天三次
            {
                return 3;
            }
            else if (id == "hs")//睡前
            {
                return 1;
            }
            else if (id == "qn")//每晚一次
            {
                return 1;
            }
            else if (id == "qid")//每天四次
            {
                return 4;
            }
            else if (id == "pcd")//晚餐后
            {
                return 1;
            }
            else if (id == "pcl")//午餐后
            {
                return 1;
            }
            else if (id == "pcm")//早餐后
            {
                return 1;
            }
            else if (id == "prn")//必要时服用
            {
                return 1;
            }
            else if (id == "遵医嘱")
            {
                return 1;
            }
            else
            {
                FS.HISFC.BizLogic.Manager.Frequency frequencyManagement = new FS.HISFC.BizLogic.Manager.Frequency();
                ArrayList alFrequency = frequencyManagement.GetBySysClassAndID("ROOT", "ALL", frequencyID);
                if (alFrequency != null && alFrequency.Count > 0)
                {
                    FS.HISFC.Models.Order.Frequency obj = alFrequency[0] as FS.HISFC.Models.Order.Frequency;
                    string[] str = obj.Time.Split('-');
                    return str.Length;
                }
                return 100;
            }
        }

        /// <summary>
        /// 按用药时间/当前时间 组合显示
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="sysdate"></param>
        /// <returns></returns>
        private string FormatDateTime(DateTime dt, DateTime sysdate)
        {
            int hours = dt.Hour;// {7DBB85BF-547C-4230-8598-55A2A4AD83F4}
            string hour = "";
            if (hours >= 0 && hours < 12)
            {
                hour = hours + "a";
            }
            else if (hours == 12)
            {
                hour = hours + "p";
            }
            else
            {
                hour = (hours - 12) + "p";
            }
            if (false)
            {
                try
                {
                    if (sysdate.Date.AddDays(-1) == dt.Date)
                    {
                        return "昨" + dt.Hour.ToString().PadLeft(2, '0');
                    }
                    else if (sysdate.Date == dt.Date)
                    {
                        return "今" + dt.Hour.ToString().PadLeft(2, '0');
                    }
                    else if (sysdate.Date.AddDays(1) == dt.Date)
                    {
                        return "明" + dt.Hour.ToString().PadLeft(2, '0');
                    }
                    else if (sysdate.Date.AddDays(2) == dt.Date)
                    {
                        return "后" + dt.Hour.ToString().PadLeft(2, '0');
                    }
                    else
                    {
                        if (dt.Month == sysdate.Month)
                        {
                            return dt.Day.ToString().PadLeft(2, '0') + dt.Hour.ToString().PadLeft(2, '0');
                        }
                        else
                        {
                            return dt.Month.ToString().PadLeft(2, '0') + dt.Day.ToString().PadLeft(2, '0') + dt.Hour.ToString().PadLeft(2, '0');

                        }
                    }
                }
                catch
                {
                    return dt.Month.ToString().PadLeft(2, '0') + dt.Day.ToString().PadLeft(2, '0') + dt.Hour.ToString().PadLeft(2, '0');
                }
            }
            return hour;
        }

        #endregion

        #region 公用方法

        /// <summary>
        /// 初始化设置
        /// </summary>
        public void Init()
        {
            this.Clear();
            //this.SetFormat();
            //this.neuSpread1.ColumnWidthChanged -= new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(neuSpread1_ColumnWidthChanged);
            //this.neuSpread1.ColumnWidthChanged += new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(neuSpread1_ColumnWidthChanged);
        }

        //void neuSpread1_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        //{
        //    this.neuSpread1.SaveSchema(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\InpatientDrugStoreDetDrugBill.xml");
        //}

        /// <summary>
        /// 提供没有范围选择的打印
        /// 一般在摆药保存时调用
        /// </summary>
        /// <param name="alData"></param>
        /// <param name="drugBillClass"></param>
        /// <param name="stockDept"></param>
        public void PrintData(ArrayList alData, FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass, FS.FrameWork.Models.NeuObject stockDept)
        {
            this.ShowBillData(alData, drugBillClass, stockDept);
            this.validRowNum = this.neuSpread1_Sheet1.RowCount;
            this.PrintPage();
        }

        #endregion

        #region 排序类

        /// <summary>
        /// 排序类
        /// </summary>
        private class CompareApplyOutByPatient : IComparer
        {
            /// <summary>
            /// 排序方法
            /// </summary>
            //public int Compare(object x, object y)
            //{
            //    FS.HISFC.Models.Pharmacy.ApplyOut o1 = (x as FS.HISFC.Models.Pharmacy.ApplyOut).Clone();
            //    FS.HISFC.Models.Pharmacy.ApplyOut o2 = (y as FS.HISFC.Models.Pharmacy.ApplyOut).Clone();

            //    string oX = "";          //患者姓名
            //    string oY = "";          //患者姓名


            //    oX = o1.BedNO + o1.PatientName + this.GetFrequencySortNO(o1.Frequency) + o1.UseTime.ToString();
            //    oY = o2.BedNO + o2.PatientName + this.GetFrequencySortNO(o2.Frequency) + o2.UseTime.ToString(); 

            //    return string.Compare(oX, oY);
            //}
            public int Compare(object x, object y)
            {
                FS.HISFC.Models.Pharmacy.ApplyOut o1 = (x as FS.HISFC.Models.Pharmacy.ApplyOut).Clone();
                FS.HISFC.Models.Pharmacy.ApplyOut o2 = (y as FS.HISFC.Models.Pharmacy.ApplyOut).Clone();

                string oX = "";          //患者姓名
                string oY = "";          //患者姓名

                //FZC ADD 临嘱排在前面
                #region

                string oo1 = string.Empty;
                string oo2 = string.Empty;
                if (o1.OrderType.ID == "CZ")
                {
                    oo1 = "2";
                }
                else
                {
                    oo1 = "1";
                }

                if (o2.OrderType.ID == "LZ")
                {
                    oo2 = "1";
                }
                else
                {
                    oo2 = "2";
                }

                #endregion

                oX = o1.BedNO + o1.PatientName + oo1 + o1.User01 + this.GetFrequencySortNO(o1.Frequency) + this.GetOrderNo(o1) + o1.UseTime.ToString();
                oY = o2.BedNO + o2.PatientName + oo2 + o2.User01 + this.GetFrequencySortNO(o2.Frequency) + this.GetOrderNo(o2) + o2.UseTime.ToString();


                return string.Compare(oX, oY);
            }

            private string GetOrderNo(FS.HISFC.Models.Pharmacy.ApplyOut app)
            {
                string id = app.Item.ID.ToString();
                return id;
            }
            private string GetFrequencySortNO(FS.HISFC.Models.Order.Frequency f)
            {
                string id = f.ID.ToLower();
                string sortNO = "";
                if (id == "qd")
                {
                    sortNO = "1";
                }
                else if (id == "bid")
                {
                    sortNO = "2";
                }
                else if (id == "tid")
                {
                    sortNO = "3";
                }
                else
                {
                    sortNO = "4";
                }
                if (f.Name == "另加")
                {
                    sortNO = "9999" + sortNO;
                }
                else
                {
                    sortNO = "0000" + sortNO;
                }
                return sortNO;
            }

        }

        /// <summary>
        /// 排序类
        /// </summary>
        private class CompareApplyOutByOrderNO : IComparer
        {
            /// <summary>
            /// 排序方法
            /// </summary>
            public int Compare(object x, object y)
            {
                FS.HISFC.Models.Pharmacy.ApplyOut o1 = new FS.HISFC.Models.Pharmacy.ApplyOut();
                FS.HISFC.Models.Pharmacy.ApplyOut o2  = new FS.HISFC.Models.Pharmacy.ApplyOut();
                if((x is  FS.HISFC.Models.Pharmacy.ApplyOut)&&(y is FS.HISFC.Models.Pharmacy.ApplyOut))
                {
                    o1 =  (x as FS.HISFC.Models.Pharmacy.ApplyOut).Clone();
                    o2 = (y as FS.HISFC.Models.Pharmacy.ApplyOut).Clone();
                }
                string oX = "1";          //患者姓名
                string oY = "1";          //患者姓名

                //FZC ADD 临嘱排在前面
                #region

                string oo1 = string.Empty;
                string oo2 = string.Empty;
                if (o1.OrderType != null &&o1.OrderType.ID == "CZ")
                {
                    oo1 = "2";
                }
                else
                {
                    oo1 = "1";
                }

                if (o1.OrderType != null && o2.OrderType.ID == "LZ")
                {
                    oo2 = "1";
                }
                else
                {
                    oo2 = "2";
                }

                #endregion

                oX = o1.BedNO + o1.PatientName + oo1 + o1.OrderNO + o1.UseTime.ToString();
                oY = o2.BedNO + o2.PatientName + oo2 + o2.OrderNO + o2.UseTime.ToString();

                return string.Compare(oX, oY);
            }
        }

        private class CompareApplyOutByValuableOrderNO : IComparer
        {
            /// <summary>
            /// 排序方法
            /// </summary>
            public int Compare(object x, object y)
            {
                FS.HISFC.Models.Pharmacy.ApplyOut o1 = (x as FS.HISFC.Models.Pharmacy.ApplyOut).Clone();
                FS.HISFC.Models.Pharmacy.ApplyOut o2 = (y as FS.HISFC.Models.Pharmacy.ApplyOut).Clone();

                string oX = "";          //患者姓名
                string oY = "";          //患者姓名


                oX = o1.User01 + o1.OrderNO + o1.UseTime.ToString();
                oY = o2.User01 + o2.OrderNO + o2.UseTime.ToString();

                return string.Compare(oX, oY);
            }
        }


        private class CompareApplyOutByPatientNO : IComparer
        {
            /// <summary>
            /// 排序方法
            /// </summary>
            public int Compare(object x, object y)
            {
                FS.HISFC.Models.Pharmacy.ApplyOut o1 = (x as FS.HISFC.Models.Pharmacy.ApplyOut).Clone();
                FS.HISFC.Models.Pharmacy.ApplyOut o2 = (y as FS.HISFC.Models.Pharmacy.ApplyOut).Clone();

                string oX = "";          //患者姓名
                string oY = "";          //患者姓名


                oX = o1.PatientNO;
                oY = o2.PatientNO;

                return string.Compare(oX, oY);
            }
        }

        #endregion

        #region IInpatientBill 成员，补打时用

        /// <summary>
        /// 提供摆药单数据显示的方法
        /// 一般在摆药单补打时调用
        /// </summary>
        /// <param name="alData">出库申请applyout</param>
        /// <param name="drugBillClass">摆药单分类</param>
        /// <param name="stockDept">库存科室</param>
        public void ShowData(ArrayList alData, FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass, FS.FrameWork.Models.NeuObject stockDept)
        {
            this.Clear();
            this.ShowBillData(alData, drugBillClass, stockDept);
        }

        /// <summary>
        /// 提供可以选择打印范围的打印方法
        /// </summary>
        public void Print()
        {
            this.validRowNum = this.neuSpread1_Sheet1.RowCount;

            this.PrintPage();

            this.validRowNum = this.neuSpread1_Sheet1.RowCount;
        }

        /// <summary>
        /// 设置Dock属性，补打时用
        /// </summary>
        public DockStyle WinDockStyle
        {
            get
            {
                return this.Dock;
            }
            set
            {
                this.Dock = value;
            }
        }

        /// <summary>
        /// 单据类型，补打时用
        /// </summary>
        public FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.InpatientBillType InpatientBillType
        {
            get
            {
                return FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.InpatientBillType.明细;
            }
        }

        #endregion
    }

    public class CompareStringByPatientNO : IComparer
    {
        /// <summary>
        /// 排序方法
        /// </summary>
        public int Compare(object x, object y)
        {
            string o1 = x.ToString();
            string o2 = y.ToString();

            string oX = "";          //患者姓名
            string oY = "";          //患者姓名


            oX = o1;
            oY = o2;

            return string.Compare(oX, oY);
        }
    }
}
