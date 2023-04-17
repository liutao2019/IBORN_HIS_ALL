using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.Order.ZhuHai.ZDWY.InPatient.ExecBill
{
    public partial class ucPrint : UserControl
    {
        public ucPrint()
        {
            InitializeComponent();
        }

        FS.HISFC.BizLogic.Order.ExecBill execBillMgr = new FS.HISFC.BizLogic.Order.ExecBill();

        /// <summary>
        /// 清除数据显示
        /// </summary>
        /// <returns></returns>
        private int Clear()
        {
            this.lblDeptName.Text = string.Empty;
            this.lblTitle.Text = string.Empty;
            this.lblPageNO.Text = string.Empty;
            this.lblPrintTime.Text = string.Empty;

            for (int row = 0; row < neuSpread1_Sheet1.RowCount; row++)
            {
                for (int col = 0; col < neuSpread1_Sheet1.ColumnCount; col++)
                {
                    neuSpread1_Sheet1.Cells[row, col].Text = "";
                }
            }
            return 1;
        }

        /// <summary>
        /// 打印方法
        /// </summary>
        private void printPage()
        {
            FS.FrameWork.WinForms.Classes.Print printer = new FS.FrameWork.WinForms.Classes.Print();
            printer.SetPageSize(new FS.HISFC.Models.Base.PageSize("A4", 827, 1169));
            if (FrameWork.WinForms.Classes.Function.IsManager())
            {
                printer.PrintPreview(5, 5, this.PanelMain);
            }
            else
            {
                printer.PrintPage(5, 5, this.PanelMain);
            }
        }

        /// <summary>
        /// 设置打印数据
        /// </summary>
        /// <param name="allExecOrder"></param>
        /// <param name="execKind"></param>
        /// <param name="printType"></param>
        /// <returns></returns>
        public int Print(System.Collections.ArrayList allExecOrder, string execKind, string printType, Dictionary<string, ArrayList> alExecByOrderNO)
        {
            if (allExecOrder == null || allExecOrder.Count == 0) return 0;

            if (printType == "0")
            {
                #region 按执行单打印

                Hashtable hsDifExecBill = new Hashtable();

                ArrayList allDifExecBill = new ArrayList();

                foreach (FS.HISFC.Models.Order.ExecOrder execOrder in allExecOrder)
                {
                    if (hsDifExecBill.Contains(execOrder.Memo))
                    {
                        ArrayList allDataTmp = hsDifExecBill[execOrder.Memo] as ArrayList;
                        allDataTmp.Add(execOrder);
                    }
                    else
                    {
                        ArrayList allDataTmp = new ArrayList();
                        allDataTmp.Add(execOrder);
                        hsDifExecBill.Add(execOrder.Memo, allDataTmp);
                        allDifExecBill.Add(execOrder.Memo);
                    }
                }

                foreach (string execBill in allDifExecBill)
                {
                    ArrayList allDataByExec = hsDifExecBill[execBill] as ArrayList;
                    this.SetData(execBill, allDataByExec, alExecByOrderNO);
                }
                #endregion
            }
            else if (printType == "1")
            {
                #region 按患者打印

                //allExecOrder.Sort(new FS.SOC.Local.Order.ZhuHai.ZDWY.Common.Comparer.ExecBedCompare());

                //Hashtable hsDifPatient = new Hashtable();

                //ArrayList allDifPatientBill = new ArrayList();

                //foreach (FS.HISFC.Models.Order.ExecOrder execOrder in allExecOrder)
                //{
                //    if (hsDifPatient.Contains(execOrder.Order.Patient.ID))
                //    {
                //        ArrayList allDataTmp = hsDifPatient[execOrder.Order.Patient.ID] as ArrayList;
                //        allDataTmp.Add(execOrder);
                //    }
                //    else
                //    {
                //        ArrayList allDataTmp = new ArrayList();
                //        allDataTmp.Add(execOrder);
                //        hsDifPatient.Add(execOrder.Order.Patient.ID, allDataTmp);
                //        allDifPatientBill.Add(execOrder.Order.Patient.ID);
                //    }
                //}

                //foreach (string patientNO in allDifPatientBill)
                //{
                //    ArrayList allDataByExec = hsDifPatient[patientNO] as ArrayList;
                //    this.SetDataByPatient(patientNO, allDataByExec, alExecByOrderNO);
                //}
                #endregion
            }
            else if (printType == "2")
            {
                #region 药品按照每个患者一张执行单，非药品按照执行单汇总
                //Hashtable difdrug = new Hashtable();//药品执行单
                //ArrayList aldifPatient = new ArrayList();
                //Hashtable difExecBill = new Hashtable();//非药品执行单
                //ArrayList alDifExec = new ArrayList();
                //ArrayList allExecbill = new ArrayList();//记录所有执行单
                //Dictionary<string, string> dicbillType = new Dictionary<string, string>();
                //ArrayList allDrugTypeCons = new ArrayList();
                //string strCons = string.Empty;
                //foreach (FS.HISFC.Models.Order.ExecOrder execOrder in allExecOrder)
                //{
                //    //获取所有执行单
                //    if (allExecbill == null || allExecbill.Count == 0)
                //    {
                //        allExecbill = execBillMgr.QueryExecBill(execOrder.Order.Patient.PVisit.PatientLocation.NurseCell.ID);
                //        foreach (FS.FrameWork.Models.NeuObject execBill in allExecbill)
                //        {
                //            dicbillType.Add(execBill.ID, execBill.User02);
                //        }
                //    }
                //    //获取所有药品用法
                //    if (allDrugTypeCons == null || allDrugTypeCons.Count == 0)
                //    {
                //        allDrugTypeCons = consMgr.GetAllList("ITEMTYPE");
                //        foreach (FS.HISFC.Models.Base.Const consUsage in allDrugTypeCons)
                //        {
                //            strCons += consUsage.ID + "|";
                //        }
                //    }
                //    string type = string.Empty;
                //    try
                //    {
                //        //if (dicbillType.ContainsKey(execOrder.Memo))
                //        //{
                //        type = dicbillType[execOrder.Memo].ToString();
                //        //}
                //    }
                //    catch
                //    { }
                //    if (strCons.Contains(type + "|"))
                //    {

                //        //药品执行单
                //        if (aldifPatient.Contains(execOrder.Order.Patient.ID))
                //        {
                //            ArrayList allDataTmp = difdrug[execOrder.Order.Patient.ID] as ArrayList;
                //            allDataTmp.Add(execOrder);
                //        }
                //        else
                //        {
                //            ArrayList allDataTmp = new ArrayList();
                //            allDataTmp.Add(execOrder);
                //            difdrug.Add(execOrder.Order.Patient.ID, allDataTmp);
                //            aldifPatient.Add(execOrder.Order.Patient.ID);
                //        }
                //    }
                //    else
                //    {
                //        //非药品执行单
                //        if (alDifExec.Contains(execOrder.Memo))
                //        {
                //            ArrayList allDataTmp = difExecBill[execOrder.Memo] as ArrayList;
                //            allDataTmp.Add(execOrder);
                //        }
                //        else
                //        {
                //            ArrayList allDataTmp = new ArrayList();
                //            allDataTmp.Add(execOrder);
                //            difExecBill.Add(execOrder.Memo, allDataTmp);
                //            alDifExec.Add(execOrder.Memo);
                //        }
                //    }
                //}

                ////药品
                //if (aldifPatient != null && aldifPatient.Count > 0)
                //{
                //    this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "每次用量";
                //    this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).ColumnSpan = 1;
                //    aldifPatient.Sort(new FS.SOC.Local.Order.ZhuHai.ZDWY.Common.Comparer.ExecBedCompare());
                //    foreach (string patientNO in aldifPatient)
                //    {
                //        ArrayList allDataByExec = difdrug[patientNO] as ArrayList;
                //        this.SetDataByPatient(patientNO, allDataByExec, alExecByOrderNO);
                //    }
                //}

                ////非药品
                //if (alDifExec != null && alDifExec.Count > 0)
                //{
                //    this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "数量";
                //    this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).ColumnSpan = 2;
                //    alDifExec.Sort(new FS.SOC.Local.Order.ZhuHai.ZDWY.Common.Comparer.ExecBillCompare());
                //    foreach (string execBill in alDifExec)
                //    {
                //        ArrayList allDataByExec = difExecBill[execBill] as ArrayList;
                //        this.SetData(execBill, allDataByExec, alExecByOrderNO);
                //    }
                //}
                #endregion
            }
            return 1;
        }


        private int pageRowCount = 15;

        /// <summary>
        /// 按执行单打印设置
        /// </summary>
        /// <param name="execBill"></param>
        /// <param name="allDifExecBill"></param>
        public void SetData(string execBill, ArrayList allDataByExec, Dictionary<string, ArrayList> alExecByOrderNo)
        {
            this.Clear();

            Hashtable hsPatientExecBill = new Hashtable();

            ArrayList allPatientBed = new ArrayList();

            allDataByExec.Sort(new FS.SOC.Local.Order.ZhuHai.ZDWY.Common.Comparer.OrderCompareByPatientAndUsage());

            foreach (FS.HISFC.Models.Order.ExecOrder execOrder in allDataByExec)
            {
                if (hsPatientExecBill.Contains(execOrder.Order.Patient.ID))
                {
                    ArrayList allDataTmp = hsPatientExecBill[execOrder.Order.Patient.ID] as ArrayList;
                    allDataTmp.Add(execOrder);
                }
                else
                {
                    ArrayList allDataTmp = new ArrayList();
                    allDataTmp.Add(execOrder);
                    hsPatientExecBill.Add(execOrder.Order.Patient.ID, allDataTmp);
                    FS.FrameWork.Models.NeuObject bedObj = new FS.FrameWork.Models.NeuObject();
                    bedObj.ID = execOrder.Order.Patient.ID;
                    bedObj.Name = execOrder.Order.Patient.PVisit.PatientLocation.Bed.ID;
                    allPatientBed.Add(bedObj);
                }
            }

            allPatientBed.Sort(new FS.SOC.Local.Order.ZhuHai.ZDWY.Common.Comparer.BedCompare());

            int curRowNum = 0;
            int curPageNo = 1;
            int maxPagNo = 1;
            //计算总页数
            foreach (FS.FrameWork.Models.NeuObject patientBed in allPatientBed)
            {
                ArrayList allPatientData = hsPatientExecBill[patientBed.ID] as ArrayList;
                if (curRowNum + allPatientData.Count + 5 > 37)
                {
                    if (curRowNum != 0)
                    {
                        maxPagNo++;
                    }
                    curRowNum = 0;
                    double reMainCount = Math.Floor((double)(allPatientData.Count / pageRowCount));
                    maxPagNo += (int)reMainCount;
                    curRowNum = allPatientData.Count % pageRowCount;
                }
                else
                {
                    curRowNum += allPatientData.Count + 5;
                }
            }

            curRowNum = 0;
            foreach (FS.FrameWork.Models.NeuObject patientBed in allPatientBed)
            {
                ArrayList allPatientData = hsPatientExecBill[patientBed.ID] as ArrayList;
                if (curRowNum > 0 && curRowNum + allPatientData.Count + 5 > 37)
                {
                    if (curRowNum != 0)
                    {
                        curPageNo++;
                    }

                    this.printPage();
                    this.Clear();
                    curRowNum = 0;
                }
                string bedNo = patientBed.Name.ToString();

                //FS.HISFC.Models.RADT.PatientInfo inPatient = this.inPatientMgr.QueryPatientInfoByInpatientNO((allPatientData[0] as FS.HISFC.Models.Order.ExecOrder).Order.Patient.ID);

                string strSQL = @"select name from fin_ipr_inmaininfo i where i.inpatient_no='{0}'";
                strSQL = string.Format(strSQL, (allPatientData[0] as FS.HISFC.Models.Order.ExecOrder).Order.Patient.ID);

                string name = this.execBillMgr.ExecSqlReturnOne(strSQL, "");
                double reMainCount = Math.Floor((double)(allPatientData.Count / pageRowCount));

                if (reMainCount > 0)
                {
                    for (int index = 0; index <= reMainCount; index++)
                    {
                        if (index != reMainCount)
                        {
                            this.SetTitl((allPatientData[0] as FS.HISFC.Models.Order.ExecOrder), curPageNo, maxPagNo);
                            this.SetDetail(name, bedNo, allPatientData.GetRange(index * pageRowCount, pageRowCount), alExecByOrderNo, "0");
                            curPageNo++;

                            this.printPage();
                            this.Clear();
                            curRowNum = 0;
                        }
                        else
                        {
                            this.SetTitl((allPatientData[0] as FS.HISFC.Models.Order.ExecOrder), curPageNo, maxPagNo);
                            this.SetDetail(name, bedNo, allPatientData.GetRange(index * pageRowCount, allPatientData.Count - index * pageRowCount), alExecByOrderNo, "0");

                            curRowNum += allPatientData.Count - index * pageRowCount + 5;
                        }
                    }
                }
                else
                {
                    this.SetTitl((allPatientData[0] as FS.HISFC.Models.Order.ExecOrder), curPageNo, maxPagNo);

                    this.SetDetail(name, bedNo, allPatientData, alExecByOrderNo, "0");

                    curRowNum += allPatientData.Count + 5;
                }
            }

            this.printPage();
        }

        /// <summary>
        /// 设置数据明细
        /// </summary>
        /// <param name="name"></param>
        /// <param name="bedNO"></param>
        /// <param name="allExecBill"></param>
        /// <returns></returns>
        private int SetDetail(string name, string bedNO, ArrayList allExecBill, Dictionary<string, ArrayList> alExecByOrderNO, string printType)
        {
            this.neuSpread1_Sheet1.Rows.Default.Height = 24F;
            System.Drawing.Font font1 = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            FarPoint.Win.BevelBorder bottomBorder = new FarPoint.Win.BevelBorder(FarPoint.Win.BevelBorderType.Raised, System.Drawing.Color.Black, System.Drawing.Color.Black, 1, false, false, false, true);
            FarPoint.Win.BevelBorder topBorder = new FarPoint.Win.BevelBorder(FarPoint.Win.BevelBorderType.Raised, System.Drawing.Color.Black, System.Drawing.Color.Black, 1, false, true, false, false);

            allExecBill.Cast<FS.HISFC.Models.Order.ExecOrder>()
                .GroupBy(anOrder => anOrder.Order.Patient.ID)
                .ToList().ForEach(aGroup =>
                {
                    //FS.HISFC.Models.RADT.PatientInfo inPatient = this.inPatientMgr.QueryPatientInfoByInpatientNO((allExecBill[0] as FS.HISFC.Models.Order.ExecOrder).Order.Patient.ID);
                    FS.HISFC.Models.RADT.PatientInfo inPatient = aGroup.First().Order.Patient;
                    this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.Rows.Count, 2);

                    this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.Rows.Count - 2, 0).RowSpan = 2;
                    this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.Rows.Count - 2, 1).RowSpan = 2;
                    this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.Rows.Count - 2, 2).RowSpan = 2;
                    this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.Rows.Count - 2, 3).RowSpan = 2;
                    this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.Rows.Count - 2, 4).RowSpan = 2;
                    this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.Rows.Count - 2, 5).RowSpan = 2;
                    this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.Rows.Count - 2, 6).RowSpan = 2;
                    this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.Rows.Count - 2, 7).RowSpan = 2;
                    this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.Rows.Count - 2, 8).RowSpan = 2;
                    this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.Rows.Count - 2, 9).RowSpan = 2;
                    this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.Rows.Count - 2, 1).ColumnSpan = this.neuSpread1_Sheet1.ColumnCount - 1;

                    this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.Rows.Count - 2, 1).Text = "床号:" + bedNO.Substring(4).ToString() + "    " + "姓名:" + name.ToString();
                    this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.Rows.Count - 2, 1).Font = font1;

                    #region 执行档数据展示
                    int indexCount = 0;
                    string curcomboNo = string.Empty;

                    aGroup.ToList().ForEach(execOrder =>
                    {
                        this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.Rows.Count, 1);

                        if (string.IsNullOrEmpty(execOrder.Order.Combo.ID) || curcomboNo != execOrder.Order.Combo.ID || indexCount == 0)
                        {
                            this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.Rows.Count - 1].Border = topBorder;
                            indexCount++;
                        }

                        this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.Rows.Count - 1].Height = 28F;

                        if (execOrder.Order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                        {
                            this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.Rows.Count - 1, 2).Text = execOrder.Order.DoseOnce + execOrder.Order.DoseUnit + "/" + execOrder.Order.Qty + execOrder.Order.Unit;
                            if (curcomboNo != execOrder.Order.Combo.ID)
                            {
                                this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.Rows.Count - 1, 1).Text = indexCount + "." + execOrder.Order.Item.Name + "(" + ((FS.HISFC.Models.Pharmacy.Item)execOrder.Order.Item).BaseDose + ((FS.HISFC.Models.Pharmacy.Item)execOrder.Order.Item).DoseUnit + "/" + ((FS.HISFC.Models.Pharmacy.Item)execOrder.Order.Item).MinUnit + ")";
                            }
                            else
                            {
                                this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.Rows.Count - 1, 1).Text = "".PadLeft((indexCount + ".").Length, ' ') + execOrder.Order.Item.Name + "(" + ((FS.HISFC.Models.Pharmacy.Item)execOrder.Order.Item).BaseDose + ((FS.HISFC.Models.Pharmacy.Item)execOrder.Order.Item).DoseUnit + "/" + ((FS.HISFC.Models.Pharmacy.Item)execOrder.Order.Item).MinUnit + ")";
                            }
                        }
                        else
                        {
                            this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.Rows.Count - 1, 2).ColumnSpan = 2;

                            this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.Rows.Count - 1, 2).Text = execOrder.Order.Qty + execOrder.Order.Unit + " ";

                            if (curcomboNo != execOrder.Order.Combo.ID)
                            {
                                this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.Rows.Count - 1, 1).Text = indexCount + "." + execOrder.Order.Item.Name + "(" + execOrder.Order.Item.Specs + ")";
                            }
                            else
                            {
                                this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.Rows.Count - 1, 1).Text = "".PadLeft((indexCount + ".").Length, ' ') + execOrder.Order.Item.Name + "(" + execOrder.Order.Item.Specs + ")";
                            }
                        }
                        //同组用法频次不显示
                        if (curcomboNo != execOrder.Order.Combo.ID)
                        {
                            this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.Rows.Count - 1, 3).Text = FS.SOC.HISFC.BizProcess.Cache.Common.GetUsageName(execOrder.Order.Usage.ID);

                            this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.Rows.Count - 1, 4).Text = execOrder.Order.Frequency.ID;
                            if (execOrder.Order.OrderType.IsDecompose)
                            {
                                ArrayList allDataByOrderNo = alExecByOrderNO[execOrder.Order.ID] as ArrayList;

                                if (execOrder.Order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                                {
                                    string hourText = string.Empty;

                                    for (int index = 0; index < allDataByOrderNo.Count; index++)
                                    {
                                        int hour = (allDataByOrderNo[index] as FS.HISFC.Models.Order.ExecOrder).DateUse.Hour;
                                        hourText += (allDataByOrderNo[index] as FS.HISFC.Models.Order.ExecOrder).DateUse.Hour.ToString().PadLeft(2, '0') + ":" + (allDataByOrderNo[index] as FS.HISFC.Models.Order.ExecOrder).DateUse.Minute.ToString().PadLeft(2, '0');
                                        if (index + 1 < allDataByOrderNo.Count) hourText += "-";
                                    }

                                    this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.Rows.Count - 1, 5).Text = hourText;
                                }
                                else
                                {
                                    if (execOrder.Order.MOTime.ToShortDateString() == execOrder.DateUse.ToShortDateString())
                                    {
                                        this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.Rows.Count - 1, 5).Text = "首日" + allDataByOrderNo.Count;
                                    }
                                }
                            }
                            else
                            {
                                this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.Rows.Count - 1, 5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                                this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.Rows.Count - 1, 5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                                this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.Rows.Count - 1, 5).Text = "(临) 开嘱时间：" + execOrder.Order.MOTime.Hour.ToString().PadLeft(2, '0') + ":" + execOrder.Order.MOTime.Minute.ToString().PadLeft(2, '0') + "  备注：" + execOrder.Order.Memo;
                            }
                        }
                        curcomboNo = execOrder.Order.Combo.ID;
                    });
                    #endregion
                });

            #region 行尾设置
            FarPoint.Win.BevelBorder bevelBorder1 = new FarPoint.Win.BevelBorder(FarPoint.Win.BevelBorderType.Lowered, System.Drawing.Color.Black, System.Drawing.Color.Black, 1, false, false, false, true);
            this.neuSpread1_Sheet1.Rows.Get(this.neuSpread1_Sheet1.Rows.Count - 1).Border = bevelBorder1;
            #endregion

            return 1;
        }

        /// <summary>
        /// 设置标题
        /// </summary>
        /// <param name="execOrder"></param>
        /// <param name="curPage"></param>
        /// <param name="maxPage"></param>
        /// <returns></returns>
        private int SetTitl(FS.HISFC.Models.Order.ExecOrder execOrder, int curPage, int maxPage)
        {
            this.lblTitle.Text = this.GetBillName(execOrder.Memo.ToString());

            this.lblTitle.Location = new Point(FS.FrameWork.Function.NConvert.ToInt32((this.Width - this.lblTitle.Width) / 2), this.lblTitle.Location.Y);
            //病区
            this.lblDeptName.Text = "病区：" + FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(execOrder.Order.Patient.PVisit.PatientLocation.NurseCell.ID);
            //页码
            this.lblPageNO.Text = "第" + curPage + "页/共" + maxPage + "页";
            //日期
            this.lblPrintTime.Text = "日期：" + execOrder.DateUse.ToShortDateString();
            return 1;
        }

        private string GetBillName(string billNo)
        {
            string strSql = @"select bill_name from met_ipm_execbill  where bill_no = '{0}'";
            strSql = string.Format(strSql, billNo);
            return this.execBillMgr.ExecSqlReturnOne(strSql, "");
        }
    }
}
