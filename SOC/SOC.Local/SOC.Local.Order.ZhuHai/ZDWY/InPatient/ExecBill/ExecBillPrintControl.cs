using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace FS.SOC.Local.Order.ZhuHai.ZDWY.InPatient.ExecBill
{
    /// <summary>
    /// 执行单打印界面
    /// </summary>
    public partial class ExecBillPrintControl : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ExecBillPrintControl()
        {
            InitializeComponent();
            this.PrintDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(PrintDocumentPrintPage);
        }

        FS.HISFC.BizLogic.RADT.InPatient inPatientMgr = new FS.HISFC.BizLogic.RADT.InPatient();

        FS.HISFC.BizLogic.Order.ExecBill execBillMgr = new FS.HISFC.BizLogic.Order.ExecBill();

        FS.HISFC.BizLogic.Manager.Constant consMgr = new FS.HISFC.BizLogic.Manager.Constant();

        /// <summary>
        /// 医嘱业务层
        /// </summary>
        private FS.HISFC.BizLogic.Order.Order orderManager = new FS.HISFC.BizLogic.Order.Order();

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

        SOC.Windows.Forms.PrintPageSelectDialog socPrintPageSelectDialog = new FS.SOC.Windows.Forms.PrintPageSelectDialog();
        #endregion

        /// <summary>
        /// 是否打印备注
        /// </summary>
        public bool isMemo = false;

        public DateTime QueryDate
        {
            get
            {
                return queryDate;
            }
            set
            {
                queryDate = value;
            }
        }

        private DateTime queryDate;

        /// <summary>
        /// 打印方法
        /// </summary>
        private void printPage()
        {
            FS.FrameWork.WinForms.Classes.Print printer = new FS.FrameWork.WinForms.Classes.Print();

            FS.HISFC.BizLogic.Manager.PageSize pgMgr = new FS.HISFC.BizLogic.Manager.PageSize();
            FS.HISFC.Models.Base.PageSize pSize = pgMgr.GetPageSize("execBillPaper");

            if (pSize == null)
            {
                printer.SetPageSize(new FS.HISFC.Models.Base.PageSize("execBillPaper", 700, 550));
            }
            else
            {
                printer.SetPageSize(pSize);
            }

            if (((FS.HISFC.Models.Base.Employee)this.inPatientMgr.Operator).IsManager)
            {
                printer.PrintPreview(5, 5, this.PanelMain);
            }
            else
            {
                printer.PrintPage(5, 5, this.PanelMain);
            }
        }


        #region IExecBillPrint 成员

        /// <summary>
        /// 设置打印数据
        /// </summary>
        /// <param name="allExecOrder"></param>
        /// <param name="execKind"></param>
        /// <param name="printType"></param>
        /// <returns></returns>
        public int SetExecBill(System.Collections.ArrayList allExecOrder, string execKind, string printType, Dictionary<string, ArrayList> alExecByOrderNO)
        {
            if (allExecOrder == null || allExecOrder.Count == 0) return 0;

            if (printType == "0")
            {
                #region 按执行单打印
                //this.Clear();
                //this.SetTitle(allExecOrder[0] as FS.HISFC.Models.Order.ExecOrder);
                //this.SetDetail("", "", allExecOrder, alExecByOrderNO,printType);
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

                allExecOrder.Sort(new FS.SOC.Local.Order.ZhuHai.ZDWY.Common.Comparer.ExecBedCompare());

                Hashtable hsDifPatient = new Hashtable();

                ArrayList allDifPatientBill = new ArrayList();

                foreach (FS.HISFC.Models.Order.ExecOrder execOrder in allExecOrder)
                {
                    if (hsDifPatient.Contains(execOrder.Order.Patient.ID))
                    {
                        ArrayList allDataTmp = hsDifPatient[execOrder.Order.Patient.ID] as ArrayList;
                        allDataTmp.Add(execOrder);
                    }
                    else
                    {
                        ArrayList allDataTmp = new ArrayList();
                        allDataTmp.Add(execOrder);
                        hsDifPatient.Add(execOrder.Order.Patient.ID, allDataTmp);
                        allDifPatientBill.Add(execOrder.Order.Patient.ID);
                    }
                }

                foreach (string patientNO in allDifPatientBill)
                {
                    ArrayList allDataByExec = hsDifPatient[patientNO] as ArrayList;
                    this.SetDataByPatient(patientNO, allDataByExec, alExecByOrderNO);
                }
                #endregion
            }
            else if (printType == "2")
            {
                #region 药品按照每个患者一张执行单，非药品按照执行单汇总
                Hashtable difdrug = new Hashtable();//药品执行单
                ArrayList aldifPatient = new ArrayList();
                Hashtable difExecBill = new Hashtable();//非药品执行单
                ArrayList alDifExec = new ArrayList();
                ArrayList allExecbill = new ArrayList();//记录所有执行单
                Dictionary<string, string> dicbillType = new Dictionary<string, string>();
                ArrayList allDrugTypeCons = new ArrayList();
                string strCons = string.Empty;
                foreach (FS.HISFC.Models.Order.ExecOrder execOrder in allExecOrder)
                {
                    //获取所有执行单
                    if (allExecbill == null || allExecbill.Count == 0)
                    {
                        allExecbill = execBillMgr.QueryExecBill(execOrder.Order.Patient.PVisit.PatientLocation.NurseCell.ID);
                        foreach (FS.FrameWork.Models.NeuObject execBill in allExecbill)
                        {
                            dicbillType.Add(execBill.ID, execBill.User02);
                        }
                    }
                    //获取所有药品用法
                    if (allDrugTypeCons == null || allDrugTypeCons.Count == 0)
                    {
                        allDrugTypeCons = consMgr.GetAllList("ITEMTYPE");
                        foreach (FS.HISFC.Models.Base.Const consUsage in allDrugTypeCons)
                        {
                            strCons += consUsage.ID + "|";
                        }
                    }
                    string type = string.Empty;
                    try
                    {
                        //if (dicbillType.ContainsKey(execOrder.Memo))
                        //{
                        type = dicbillType[execOrder.Memo].ToString();
                        //}
                    }
                    catch
                    { }
                    if (strCons.Contains(type + "|"))
                    {

                        //药品执行单
                        if (aldifPatient.Contains(execOrder.Order.Patient.ID))
                        {
                            ArrayList allDataTmp = difdrug[execOrder.Order.Patient.ID] as ArrayList;
                            allDataTmp.Add(execOrder);
                        }
                        else
                        {
                            ArrayList allDataTmp = new ArrayList();
                            allDataTmp.Add(execOrder);
                            difdrug.Add(execOrder.Order.Patient.ID, allDataTmp);
                            aldifPatient.Add(execOrder.Order.Patient.ID);
                        }
                    }
                    else
                    {
                        //非药品执行单
                        if (alDifExec.Contains(execOrder.Memo))
                        {
                            ArrayList allDataTmp = difExecBill[execOrder.Memo] as ArrayList;
                            allDataTmp.Add(execOrder);
                        }
                        else
                        {
                            ArrayList allDataTmp = new ArrayList();
                            allDataTmp.Add(execOrder);
                            difExecBill.Add(execOrder.Memo, allDataTmp);
                            alDifExec.Add(execOrder.Memo);
                        }
                    }
                }

                //药品
                if (aldifPatient != null && aldifPatient.Count > 0)
                {
                    this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "每次用量";
                    this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).ColumnSpan = 1;
                    aldifPatient.Sort(new FS.SOC.Local.Order.ZhuHai.ZDWY.Common.Comparer.ExecBedCompare());
                    foreach (string patientNO in aldifPatient)
                    {
                        ArrayList allDataByExec = difdrug[patientNO] as ArrayList;
                        this.SetDataByPatient(patientNO, allDataByExec, alExecByOrderNO);
                    }
                }

                //非药品
                if (alDifExec != null && alDifExec.Count > 0)
                {
                    this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "数量";
                    this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).ColumnSpan = 2;
                    alDifExec.Sort(new FS.SOC.Local.Order.ZhuHai.ZDWY.Common.Comparer.ExecBillCompare());
                    foreach (string execBill in alDifExec)
                    {
                        ArrayList allDataByExec = difExecBill[execBill] as ArrayList;
                        this.SetData(execBill, allDataByExec, alExecByOrderNO);
                    }
                }
                #endregion
            }
            return 1;
        }

        /// <summary>
        /// 按患者打印设置
        /// </summary>
        /// <param name="patientNo"></param>
        /// <param name="allDataByPatient"></param>
        /// <param name="allExecByOrderNo"></param>
        private void SetDataByPatient(string patientNo, ArrayList allDataByPatient, Dictionary<string, ArrayList> allExecByOrderNo)
        {
            this.Clear();
            Hashtable hsPatientExecBill = new Hashtable();

            ArrayList allExecBill = new ArrayList();

            allDataByPatient.Sort(new FS.SOC.Local.Order.ZhuHai.ZDWY.Common.Comparer.OrderCompareByPatientAndUsage());

            foreach (FS.HISFC.Models.Order.ExecOrder execOrder in allDataByPatient)
            {
                if (hsPatientExecBill.Contains(execOrder.Memo))
                {
                    ArrayList allDataTmp = hsPatientExecBill[execOrder.Memo] as ArrayList;
                    allDataTmp.Add(execOrder);
                }
                else
                {
                    ArrayList allDataTmp = new ArrayList();
                    allDataTmp.Add(execOrder);
                    hsPatientExecBill.Add(execOrder.Memo, allDataTmp);
                    FS.FrameWork.Models.NeuObject bedObj = new FS.FrameWork.Models.NeuObject();
                    allExecBill.Add(execOrder.Memo);
                }
            }

            allExecBill.Sort(new FS.SOC.Local.Order.ZhuHai.ZDWY.Common.Comparer.ExecBillCompare());

            int curRowNum = 0;
            int curPageNo = 1;
            int maxPagNo = 1;
            //计算总页数
            foreach (string execBill in allExecBill)
            {
                ArrayList allPatientData = hsPatientExecBill[execBill] as ArrayList;
                if (curRowNum + allPatientData.Count + 5 > 37)
                {
                    maxPagNo++;
                    curRowNum = 0;
                }
                curRowNum += allPatientData.Count + 5;
            }
            curRowNum = 0;
            foreach (string execBill in allExecBill)
            {
                ArrayList allExecData = hsPatientExecBill[execBill] as ArrayList;
                if (curRowNum + allExecData.Count + 5 > 37)
                {
                    curPageNo++;
                    this.printPage();
                    this.Clear();
                    curRowNum = 0;
                }
                this.SetTitl((allExecData[0] as FS.HISFC.Models.Order.ExecOrder), curPageNo, maxPagNo);

                FS.HISFC.Models.RADT.PatientInfo inPatient = this.inPatientMgr.QueryPatientInfoByInpatientNO((allExecData[0] as FS.HISFC.Models.Order.ExecOrder).Order.Patient.ID);
                string name = inPatient.Name;
                string bedNo = inPatient.PVisit.PatientLocation.Bed.ID.ToString();
                this.SetDetail(name, bedNo, allExecData, allExecByOrderNo, "1");

                curRowNum += allExecData.Count + 5;
            }

            this.printPage();
        }

        int pageRowCount = 14;

        private void Print(FS.HISFC.Models.Order.ExecOrder execOrderObj, int curPageNo, int maxPagNo, ArrayList alPrint, Dictionary<string, ArrayList> alExecByOrderNo)
        {
            //this.SetTitl(execOrderObj, curPageNo, maxPagNo);
            this.SetTitle(execOrderObj);
            string bedNo = execOrderObj.Order.Patient.PVisit.PatientLocation.Bed.ID.Substring(4);
            string strSQL = @"select name from fin_ipr_inmaininfo i where i.inpatient_no='{0}'";
            strSQL = string.Format(strSQL, execOrderObj.Order.Patient.ID);
            string name = this.execBillMgr.ExecSqlReturnOne(strSQL, "");

            this.SetDetail(name, bedNo, alPrint, alExecByOrderNo, "0");

            //this.printPage();
            this.Clear();
        }

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

            //int totRowCount = 0;

            //int pageRowIndex = 0;

            Dictionary<int, ArrayList> dicPage = new Dictionary<int, ArrayList>();

            ArrayList alTemp = new ArrayList();

            //int curPageNo = 1;
            allDataByExec.Sort(new ComparerBySortID());

            //FZC ADD 过滤掉不需要的长嘱 并留使用事件
            IList<FS.HISFC.Models.Order.ExecOrder> orderDataList = new List<FS.HISFC.Models.Order.ExecOrder>();
            IDictionary<string, string> makeMessDic = new Dictionary<string, string>();
            DateTime now = this.orderManager.GetDateTimeFromSysDateTime();
            foreach (FS.HISFC.Models.Order.ExecOrder order in allDataByExec)
            {
                //临嘱无需合并
                if (!order.Order.OrderType.IsDecompose)
                {
                    orderDataList.Add(order);
                }
                else
                {
                    if (!makeMessDic.ContainsKey(order.Order.Combo.ID + order.Order.Item.ID))
                    {
                        makeMessDic.Add(order.Order.Combo.ID + order.Order.Item.ID, Function.GetShowTime(order.DateUse, this.queryDate));
                        orderDataList.Add(order);
                    }
                    else if ((order.Order.Frequency.ID.ToString() == "QH" || order.Order.Frequency.ID.ToString() == "Q2H") && makeMessDic[order.Order.Combo.ID + order.Order.Item.ID].IndexOf("始")<0)
                    {
                        makeMessDic[order.Order.Combo.ID + order.Order.Item.ID] += "始";
                    }
                    else if ((order.Order.Frequency.ID.ToString() == "QH" || order.Order.Frequency.ID.ToString() == "Q2H") && makeMessDic[order.Order.Combo.ID + order.Order.Item.ID].IndexOf("始") > 0)
                    {
                        makeMessDic[order.Order.Combo.ID + order.Order.Item.ID] += "";
                    }
                    else
                    {
                        makeMessDic[order.Order.Combo.ID + order.Order.Item.ID] += "," + Function.GetShowTime(order.DateUse, this.queryDate);
                    }
                }
            }

            //去掉重复的长嘱后 将算好的要显示的时间加上并转换回ArrayList
            ArrayList middleList = new ArrayList();
            foreach(var order in orderDataList)
            {
                if (order.Order.OrderType.IsDecompose )//&& !makeMessDic.ContainsKey(order.Order.Combo.ID + order.Order.Item.ID))
                {
                    //征用这个字段存一下
                    order.ChargeOper.Memo = makeMessDic[order.Order.Combo.ID + order.Order.Item.ID];
                }

                middleList.Add(order);
            }

            allDataByExec = middleList;

            //for (int i = 0; i < allDataByExec.Count; i++)
            //{
            //    FS.HISFC.Models.Order.ExecOrder execOrder = allDataByExec[i] as FS.HISFC.Models.Order.ExecOrder;
            //    //不存在的患者
            //    if (!hsPatientExecBill.Contains(execOrder.Order.Patient.ID))
            //    {
            //        //如果仅剩两行或更少 另起新页
            //        if ((pageRowIndex + 2) > pageRowCount - 1)
            //        {
            //            //添加已完成的页
            //            dicPage.Add(curPageNo, alTemp);
            //            //当前页码和总页码+1
            //            curPageNo += 1;
            //            //清空信息
            //            hsPatientExecBill.Clear();
            //            pageRowIndex = 0;
            //            alTemp = new ArrayList();
            //        }

            //        //在新页或当前页 添加患者信息 行数占用2
            //        hsPatientExecBill.Add(execOrder.Order.Patient.ID, null);
            //        alTemp.Add(execOrder);
            //        pageRowIndex += 2;

            //        //但是这是最后一条了 结束了 保存当前页
            //        if (i == allDataByExec.Count - 1)
            //        {
            //            dicPage.Add(curPageNo, alTemp);
            //            //清空信息
            //            hsPatientExecBill.Clear();
            //            pageRowIndex = 0;
            //            alTemp = new ArrayList();
            //        }
            //    }
            //    else//已存在这个患者
            //    {
            //        //这是最后一条了 
            //        if ((pageRowIndex + 1) >= pageRowCount - 1)
            //        {
            //            //保存当前页 另起新页
            //            alTemp.Add(execOrder);
            //            //添加已完成的页
            //            dicPage.Add(curPageNo, alTemp);
            //            //当前页码和总页码+1
            //            curPageNo += 1;
            //            //清空信息
            //            hsPatientExecBill.Clear();
            //            pageRowIndex = 0;
            //            alTemp = new ArrayList();
            //        }
            //        else//还有空间
            //        {
            //            alTemp.Add(execOrder);
            //            pageRowIndex += 1;

            //            //但是这是最后一条了 结束了 保存当前页
            //            if (i == allDataByExec.Count - 1)
            //            {
            //                dicPage.Add(curPageNo, alTemp);
            //                //清空信息
            //                hsPatientExecBill.Clear();
            //                pageRowIndex = 0;
            //                alTemp = new ArrayList();
            //            }
            //        }
            //    }
            //}
            //int totalPage = dicPage.Count();
            this.SetTitle(allDataByExec[0] as FS.HISFC.Models.Order.ExecOrder);
            this.SetDetail(execBill, "", allDataByExec, alExecByOrderNo, "0");

            //foreach (int page in dicPage.Keys)
            //{
            //    FS.HISFC.Models.Order.ExecOrder execOrderObj = dicPage[page][0] as FS.HISFC.Models.Order.ExecOrder;
            //    this.Print(execOrderObj, page, totalPage, dicPage[page], alExecByOrderNo);
            //}
        }

        #endregion

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
            this.lblPrintTime.Text = "日期：" + this.queryDate.ToShortDateString();
            return 1;
        }

        /// <summary>
        /// 设置标题
        /// </summary>
        /// <param name="execOrder"></param>
        /// <param name="curPage"></param>
        /// <param name="maxPage"></param>
        /// <returns></returns>
        private int SetTitle(FS.HISFC.Models.Order.ExecOrder execOrder)
        {
            this.lblTitle.Text = this.GetBillName(execOrder.Memo.ToString());

            //this.lblTitle.Location = new Point(FS.FrameWork.Function.NConvert.ToInt32((this.Width - this.lblTitle.Width) / 2), this.lblTitle.Location.Y);
            //病区
            //this.lblDeptName.Text = "病区：" + FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(execOrder.Order.Patient.PVisit.PatientLocation.NurseCell.ID);
            FS.HISFC.Models.RADT.PatientInfo patientInfo= FS.SOC.HISFC.BizProcess.Cache.BIZManager.InPatientMgr.GetPatientInfoByPatientNO(execOrder.Order.Patient.ID.ToString());
            this.lblDeptName.Text = "病区：" + patientInfo.PVisit.PatientLocation.NurseCell.Name.ToString();
            //页码
            //this.lblPageNO.Text = "第" + curPage + "页/共" + maxPage + "页";
            //日期
            this.lblPrintTime.Text = "日期：" + this.queryDate.ToShortDateString();
            return 1;
        }

        private string GetBillName(string billNo)
        {
            string strSql = @"select bill_name from met_ipm_execbill  where bill_no = '{0}'";
            strSql = string.Format(strSql, billNo);
            return this.execBillMgr.ExecSqlReturnOne(strSql, "");
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
                    this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.Rows.Count, 2);

                    //for (int col = 0; col < neuSpread1_Sheet1.ColumnCount; col++)
                    //{
                    //    this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.Rows.Count - 2, col).RowSpan = 2;
                    //}
                    this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.Rows.Count - 1, 1).ColumnSpan = this.neuSpread1_Sheet1.ColumnCount - 2;

                    this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.Rows.Count - 1, 1).Text = "床号:" + bedNO.ToString() + "    " + "" + name.ToString();


                    this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.Rows.Count - 1, 1).Font = font1;

                    this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.Rows.Count - 1, neuSpread1_Sheet1.ColumnCount - 1).ColumnSpan = 2;

                    int patientRowIndex = this.neuSpread1_Sheet1.Rows.Count - 1;

                    #region 执行档数据展示
                    int indexCount = 0;
                    string curcomboNo = string.Empty;

                    name = string.Empty;

                    aGroup.ToList().ForEach(execOrder =>
                    {
                        string strSubCombNO = (execOrder.Order.OrderType.IsDecompose ? "长" : "临") + execOrder.Order.SubCombNO.ToString() + " ";

                        if (string.IsNullOrEmpty(name))
                        {
                            string bedNo = execOrder.Order.Patient.PVisit.PatientLocation.Bed.ID.Substring(4);
                            string strSQL = @"select name from fin_ipr_inmaininfo i where i.inpatient_no='{0}'";
                            strSQL = string.Format(strSQL, execOrder.Order.Patient.ID);
                            name = this.execBillMgr.ExecSqlReturnOne(strSQL, "");

                            //FZC 修改 
                            this.neuSpread1_Sheet1.Cells.Get(patientRowIndex, 1).Text = "床号:" + bedNo + "    " + "姓名:" + name.ToString();
                        }

                        this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.Rows.Count, 1);

                        if (string.IsNullOrEmpty(execOrder.Order.Combo.ID)
                            || curcomboNo != execOrder.Order.Combo.ID
                            || indexCount == 0)
                        {
                            this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.Rows.Count - 1].Border = topBorder;
                            indexCount++;
                        }

                        this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.Rows.Count - 1].Height = 28F;

                        string memo = "";
                        if (!string.IsNullOrEmpty(execOrder.Order.Memo))
                        {
                            memo = "(" + execOrder.Order.Item.Specs + ")";
                        }

                        if (execOrder.Order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                        {
                            this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.Rows.Count - 1, 2).Text = execOrder.Order.DoseOnce + execOrder.Order.DoseUnit;
                            //this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.Rows.Count - 1, 2).Text = execOrder.Order.DoseOnce + execOrder.Order.DoseUnit + "/" + execOrder.Order.Qty + execOrder.Order.Unit;
                            if (curcomboNo != execOrder.Order.Combo.ID)
                            {
                                this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.Rows.Count - 1, 0).Text = strSubCombNO;
                                if (!execOrder.Order.OrderType.IsCharge)
                                {
                                    if (execOrder.Order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                                    {
                                        this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.Rows.Count - 1, 1).Text = "(自备)" + execOrder.Order.Item.Name + orderManager.TransHypotest(execOrder.Order.HypoTest) + memo;
                                    }
                                    else
                                    {
                                        this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.Rows.Count - 1, 1).Text = "(自备)" + execOrder.Order.Item.Name + orderManager.TransHypotest(execOrder.Order.HypoTest) + memo;
                                    }
                                }
                                else
                                {
                                    this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.Rows.Count - 1, 1).Text = execOrder.Order.Item.Name + orderManager.TransHypotest(execOrder.Order.HypoTest) + memo;
                                }

                                string mess = "";
                                if (execOrder.Order.OrderType.IsDecompose)
                                {
                                    if (SOC.HISFC.BizProcess.Cache.Common.IsPOUsage(execOrder.Order.Usage.ID))
                                    {
                                        mess += "[" + execOrder.Order.Qty + execOrder.Order.Unit + "]";
                                        mess += execOrder.ChargeOper.Memo;
                                    }// {E97273E4-CF5A-47bf-97C6-8025504486C4}
                                    //else if (execOrder.Order.Item.ItemType != FS.HISFC.Models.Base.EnumItemType.Drug)
                                    else
                                    {
                                        mess += execOrder.ChargeOper.Memo;
                                    }
                                }
                                else 
                                {
                                    if (execOrder.Order.Item.ItemType==FS.HISFC.Models.Base.EnumItemType.Drug)
                                    {
                                        mess = "[" + execOrder.Order.Qty + execOrder.Order.Unit + "]";
                                    }
                                }

                                if (this.isMemo)
                                {
                                    mess += execOrder.Order.Memo;
                                }

                                if (string.IsNullOrEmpty(mess))
                                {
                                    mess = ".";
                                }
                                this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.Rows.Count - 1, 5).Text = mess;                                
                            }
                            else
                            {
                                this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.Rows.Count - 1, 0).Text = "";
                                if (!execOrder.Order.OrderType.IsCharge)
                                {
                                    if (execOrder.Order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                                    {
                                        this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.Rows.Count - 1, 1).Text = "(自备)" + execOrder.Order.Item.Name + orderManager.TransHypotest(execOrder.Order.HypoTest) + memo;
                                    }
                                    else
                                    {
                                        this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.Rows.Count - 1, 1).Text = "(自备)" + execOrder.Order.Item.Name + orderManager.TransHypotest(execOrder.Order.HypoTest) + memo;
                                    }
                                }
                                else
                                {
                                    this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.Rows.Count - 1, 1).Text = execOrder.Order.Item.Name + orderManager.TransHypotest(execOrder.Order.HypoTest) + memo;
                                }

                                string mess = "";
                                if (execOrder.Order.OrderType.IsDecompose)
                                {
                                    if (SOC.HISFC.BizProcess.Cache.Common.IsPOUsage(execOrder.Order.Usage.ID))
                                    {
                                        mess += "[" + execOrder.Order.Qty + execOrder.Order.Unit + "]";
                                        mess += execOrder.ChargeOper.Memo;
                                    }// {E97273E4-CF5A-47bf-97C6-8025504486C4}
                                    //else if (execOrder.Order.Item.ItemType != FS.HISFC.Models.Base.EnumItemType.Drug)
                                    else
                                    {
                                        mess += execOrder.ChargeOper.Memo;
                                    }
                                }
                                else
                                {
                                    if (execOrder.Order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                                    {
                                        mess = "[" + execOrder.Order.Qty + execOrder.Order.Unit + "]";
                                    }
                                }

                                if (this.isMemo)
                                {
                                    mess += execOrder.Order.Memo;
                                }

                                if (string.IsNullOrEmpty(mess))
                                {
                                    mess = ".";
                                }
                                this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.Rows.Count - 1, 5).Text = mess;
                            }
                        }
                        else
                        {
                            //this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.Rows.Count - 1, 2).ColumnSpan = 2;

                            this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.Rows.Count - 1, 2).Text = execOrder.Order.Qty + execOrder.Order.Unit + " ";

                            if (curcomboNo != execOrder.Order.Combo.ID)
                            {
                                this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.Rows.Count - 1, 0).Text = strSubCombNO;

                                if (!execOrder.Order.OrderType.IsCharge)
                                {
                                    if (execOrder.Order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                                    {
                                        this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.Rows.Count - 1, 1).Text = "(自备)" + execOrder.Order.Item.Name + memo;
                                    }
                                    else
                                    {
                                        this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.Rows.Count - 1, 1).Text = "(嘱托)" + execOrder.Order.Item.Name + orderManager.TransHypotest(execOrder.Order.HypoTest) + memo;
                                    }
                                }
                                else
                                {
                                    this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.Rows.Count - 1, 1).Text = execOrder.Order.Item.Name + orderManager.TransHypotest(execOrder.Order.HypoTest) + memo;
                                }

                                string mess = "";
                                if (execOrder.Order.OrderType.IsDecompose)
                                {
                                    if (SOC.HISFC.BizProcess.Cache.Common.IsPOUsage(execOrder.Order.Usage.ID))
                                    {
                                        mess += "[" + execOrder.Order.Qty + execOrder.Order.Unit + "]";
                                        mess += execOrder.ChargeOper.Memo;
                                    }// {E97273E4-CF5A-47bf-97C6-8025504486C4}
                                    //else if (execOrder.Order.Item.ItemType != FS.HISFC.Models.Base.EnumItemType.Drug)
                                    else
                                    {
                                        mess += execOrder.ChargeOper.Memo;
                                    }
                                }
                                else
                                {
                                    if (execOrder.Order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                                    {
                                        mess = "[" + execOrder.Order.Qty + execOrder.Order.Unit + "]";
                                    }
                                }

                                if (this.isMemo)
                                {
                                    mess += execOrder.Order.Memo;
                                }

                                if (string.IsNullOrEmpty(mess))
                                {
                                    mess = ".";
                                }
                                this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.Rows.Count - 1, 5).Text = mess;                                
                            }
                            else
                            {
                                this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.Rows.Count - 1, 0).Text = "";

                                if (!execOrder.Order.OrderType.IsCharge)
                                {
                                    if (execOrder.Order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                                    {
                                        this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.Rows.Count - 1, 1).Text = "(自备)" + execOrder.Order.Item.Name + orderManager.TransHypotest(execOrder.Order.HypoTest) + memo;
                                    }
                                    else
                                    {
                                        this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.Rows.Count - 1, 1).Text = "(自备)" + execOrder.Order.Item.Name + orderManager.TransHypotest(execOrder.Order.HypoTest) + memo;
                                    }
                                }
                                else
                                {
                                    this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.Rows.Count - 1, 1).Text = execOrder.Order.Item.Name + orderManager.TransHypotest(execOrder.Order.HypoTest) + memo;
                                }

                                string mess = string.Empty;
                                if (execOrder.Order.OrderType.IsDecompose)
                                {
                                    if (SOC.HISFC.BizProcess.Cache.Common.IsPOUsage(execOrder.Order.Usage.ID))
                                    {
                                        mess += "[" + execOrder.Order.Qty + execOrder.Order.Unit + "]";
                                        mess += execOrder.ChargeOper.Memo;
                                    }// {E97273E4-CF5A-47bf-97C6-8025504486C4}
                                    //else if (execOrder.Order.Item.ItemType != FS.HISFC.Models.Base.EnumItemType.Drug)
                                    else
                                    {
                                        mess += execOrder.ChargeOper.Memo;
                                    }
                                }
                                else
                                {
                                    if (execOrder.Order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                                    {
                                        mess = "[" + execOrder.Order.Qty + execOrder.Order.Unit + "]";
                                    }
                                }

                                if (this.isMemo)
                                {
                                    mess += execOrder.Order.Memo;
                                }

                                if (string.IsNullOrEmpty(mess))
                                {
                                    mess = ".";
                                }
                                this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.Rows.Count - 1, 5).Text = mess;                                
                            }
                        }
                        //同组用法频次不显示
                        if (curcomboNo != execOrder.Order.Combo.ID)
                        {
                            this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.Rows.Count - 1, 3).Text = FS.SOC.HISFC.BizProcess.Cache.Common.GetUsageName(execOrder.Order.Usage.ID);

                            this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.Rows.Count - 1, 4).Text = execOrder.Order.Frequency.ID;
                            string mess = string.Empty;
                            if (execOrder.Order.OrderType.IsDecompose)
                            {
                                if (SOC.HISFC.BizProcess.Cache.Common.IsPOUsage(execOrder.Order.Usage.ID))
                                {
                                    mess += "[" + execOrder.Order.Qty + execOrder.Order.Unit + "]";
                                    mess += execOrder.ChargeOper.Memo;
                                }// {E97273E4-CF5A-47bf-97C6-8025504486C4}
                                //else if (execOrder.Order.Item.ItemType != FS.HISFC.Models.Base.EnumItemType.Drug)
                                else
                                {
                                    mess += execOrder.ChargeOper.Memo;
                                }
                            }
                            else
                            {
                                if (execOrder.Order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                                {
                                    mess = "[" + execOrder.Order.Qty + execOrder.Order.Unit + "]";
                                }
                            }

                            if (this.isMemo)
                            {
                                mess += execOrder.Order.Memo;
                            } 
                            if (string.IsNullOrEmpty(mess))
                            {
                                mess = ".";
                            }

                            this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.Rows.Count - 1, 5).Text = mess;                                
                        }
                        curcomboNo = execOrder.Order.Combo.ID;
                    });
                    #endregion
                });

            #region 行尾设置

            FarPoint.Win.BevelBorder bevelBorder1 = new FarPoint.Win.BevelBorder(FarPoint.Win.BevelBorderType.Lowered, System.Drawing.Color.Black, System.Drawing.Color.Black, 1, false, false, false, true);

            FarPoint.Win.BevelBorder bevelBorder2 = new FarPoint.Win.BevelBorder(FarPoint.Win.BevelBorderType.Lowered, System.Drawing.Color.Black, System.Drawing.Color.Black, 1, false, true, false, true);

            if (neuSpread1_Sheet1.Rows.Get(this.neuSpread1_Sheet1.Rows.Count - 1).Border != null
                && ((FarPoint.Win.BevelBorder)(neuSpread1_Sheet1.Rows.Get(this.neuSpread1_Sheet1.Rows.Count - 1).Border)).Top)
            {
                this.neuSpread1_Sheet1.Rows.Get(this.neuSpread1_Sheet1.Rows.Count - 1).Border = bevelBorder2;
            }
            else
            {
                this.neuSpread1_Sheet1.Rows.Get(this.neuSpread1_Sheet1.Rows.Count - 1).Border = bevelBorder1;
            }
            #endregion

            //this.isMemo = false;
            this.validRowNum = this.neuSpread1_Sheet1.RowCount;
            this.PrintPage();
            return 1;
        }

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
            this.neuSpread1_Sheet1.RowCount = 0;
            return 1;
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
            int mainTitleLocalX = this.DrawingMargins.Left + this.lblTitle.Location.X;
            int mainTitleLoaclY = this.DrawingMargins.Top + this.lblTitle.Location.Y;
            graphics.DrawString(this.lblTitle.Text, this.lblTitle.Font, new SolidBrush(this.lblTitle.ForeColor), mainTitleLocalX, mainTitleLoaclY);

            int additionTitleLocalX = this.DrawingMargins.Left + this.lblDeptName.Location.X;
            int additionTitleLocalY = this.DrawingMargins.Top + this.lblDeptName.Location.Y;

            graphics.DrawString(this.lblDeptName.Text, this.lblDeptName.Font, new SolidBrush(this.lblDeptName.ForeColor), additionTitleLocalX, additionTitleLocalY);

            additionTitleLocalX = this.DrawingMargins.Left + this.lblPrintTime.Location.X;
            additionTitleLocalY = this.DrawingMargins.Top + this.lblPrintTime.Location.Y;

            graphics.DrawString(this.lblPrintTime.Text, this.lblPrintTime.Font, new SolidBrush(this.lblPrintTime.ForeColor), additionTitleLocalX, additionTitleLocalY);

            #endregion

            #region Farpoint绘制
            int drawingWidth = 700 - this.DrawingMargins.Left - this.DrawingMargins.Right;
            int drawingHeight = 550 - this.DrawingMargins.Top - this.DrawingMargins.Bottom - this.panelTitle.Height;
            this.neuSpread1.OwnerPrintDraw(graphics, new Rectangle(this.DrawingMargins.Left, this.DrawingMargins.Top + this.panelTitle.Height, drawingWidth, drawingHeight), 0, this.curPageNO);

            #endregion

            #region 页码绘制

            additionTitleLocalX = this.DrawingMargins.Left + this.lblPageNO.Location.X;
            additionTitleLocalY = this.DrawingMargins.Top + this.lblPageNO.Location.Y;

            graphics.DrawString("页码：" + this.curPageNO.ToString() + "/" + this.maxPageNO.ToString(), this.lblPageNO.Font, new SolidBrush(this.lblPageNO.ForeColor), additionTitleLocalX, additionTitleLocalY);

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
        /// 打印页码选择
        /// </summary>
        private bool ChoosePrintPageNO(Graphics graphics)
        {

            int drawingWidth = 700 - this.DrawingMargins.Left - this.DrawingMargins.Right;
            int drawingHeight = 550 - this.DrawingMargins.Top - this.DrawingMargins.Bottom - this.panelTitle.Height;

            FarPoint.Win.Spread.PrintInfo printInfo = new FarPoint.Win.Spread.PrintInfo();
            printInfo.ShowBorder = false;
            printInfo.PrintType = FarPoint.Win.Spread.PrintType.All;
            printInfo.ShowRowHeaders = this.neuSpread1_Sheet1.RowHeader.Visible;
            this.neuSpread1_Sheet1.PrintInfo = printInfo;
            this.maxPageNO = neuSpread1.GetOwnerPrintPageCount(graphics, new Rectangle(this.DrawingMargins.Left, this.DrawingMargins.Top + this.panelTitle.Height, drawingWidth, drawingHeight), 0);

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

        /// <summary>
        /// 打印
        /// </summary>
        private void PrintPage()
        {


            // {D59C1D74-CE65-424a-9CB3-7F9174664504}
            string printerName = FS.HISFC.Components.Common.Classes.Function.ChoosePrinter();
            if (string.IsNullOrEmpty(printerName))
            {
                return;
            }

            System.Drawing.Printing.PaperSize paperSize = null;
            this.SetPaperSize(paperSize);


            this.PrintDocument.PrinterSettings.PrinterName = printerName;
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

        private void SetPaperSize(System.Drawing.Printing.PaperSize paperSize)
        {
            if (paperSize == null)
            {
                paperSize = new System.Drawing.Printing.PaperSize("execBillPaper", 700, 550);
            }

            this.PrintDocument.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize(paperSize.PaperName, paperSize.Width, paperSize.Height);
            this.PrintDocument.PrinterSettings.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize(paperSize.PaperName, paperSize.Width, paperSize.Height);
        }

        protected void PrintView()
        {
            this.SetPaperSize(null);
            this.myPrintView();
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
        /// 提供可以选择打印范围的打印方法
        /// </summary>
        public void Print()
        {
            this.validRowNum = this.neuSpread1_Sheet1.RowCount;

            this.PrintPage();

            this.validRowNum = this.neuSpread1_Sheet1.RowCount;
        }

    }
}