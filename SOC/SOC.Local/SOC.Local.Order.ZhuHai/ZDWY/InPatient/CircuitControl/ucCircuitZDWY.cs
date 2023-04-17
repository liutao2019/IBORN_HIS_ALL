using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.Order.ZhuHai.ZDWY.InPatient.CircuitControl
{
    /// <summary>
    /// 中五输液巡回卡打印
    /// </summary>
    public partial class ucCircuitZDWY : UserControl, FS.HISFC.BizProcess.Interface.IPrintTransFusion
    {
        public ucCircuitZDWY()
        {
            InitializeComponent();
            this.neuSpread1_Sheet1.Columns[(int)EnumCol.Z组合号].Visible = false;
            this.neuSpread1.ButtonClicked += new FarPoint.Win.Spread.EditorNotifyEventHandler(neuSpread1_ButtonClicked);
            this.chkAll.CheckedChanged += new EventHandler(chkAll_CheckedChanged);
        }

        private FS.HISFC.BizLogic.Order.Order orderMgr = new FS.HISFC.BizLogic.Order.Order();

        /// <summary>
        /// 是否补打
        /// </summary>
        private bool isReprint = false;

        private DateTime dtBegin;
        private DateTime dtEnd;

        /// <summary>
        /// 同组一起勾选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void neuSpread1_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (e.Column == (int)EnumCol.S是否打印)
            {
                string combNo = neuSpread1_Sheet1.Cells[e.Row, (int)EnumCol.Z组合号].Tag.ToString();

                for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
                {
                    if (neuSpread1_Sheet1.Cells[i, (int)EnumCol.Z组合号].Tag.ToString() == combNo)
                    {
                        this.neuSpread1_Sheet1.Cells[i, (int)EnumCol.S是否打印].Text = neuSpread1_Sheet1.Cells[e.Row, e.Column].Text;
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
            {
                this.neuSpread1_Sheet1.SetValue(i, (int)EnumCol.S是否打印, this.chkAll.Checked);
            }
        }

        #region IPrintTransFusion 成员

        /// <summary>
        /// 停止后的医嘱是否打印
        /// </summary>
        private bool dcIsPrint = false;

        public bool DCIsPrint
        {
            get
            {
                return dcIsPrint;
            }
            set
            {
                dcIsPrint = value;
            }
        }

        /// <summary>
        /// 不收费的是否打印
        /// </summary>
        private bool noFeeIsPrint = false;

        public bool NoFeeIsPrint
        {
            get
            {
                return noFeeIsPrint;
            }
            set
            {
                noFeeIsPrint = value;
            }
        }

        /// <summary>
        /// 退费后的是否打印
        /// </summary>
        private bool quitFeeIsPrint = false;

        public bool QuitFeeIsPrint
        {
            get
            {
                return quitFeeIsPrint;
            }
            set
            {
                quitFeeIsPrint = value;
            }
        }

        /// <summary>
        /// 每页打印的行数
        /// </summary>
        int pagePrintCount = 15;

        /// <summary>
        /// 打印
        /// </summary>
        public void Print()
        {
            try
            {
                FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
                print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.Line;

                FS.HISFC.BizLogic.Manager.PageSize pgMgr = new FS.HISFC.BizLogic.Manager.PageSize();
                FS.HISFC.Models.Base.PageSize pSize = pgMgr.GetPageSize("CircuitCardPaper");

                if (pSize == null)
                {
                    pSize = new FS.HISFC.Models.Base.PageSize("Letter", 850, 550);
                }

                ArrayList alPrint = new ArrayList();

                //《患者ID,《组合号,执行档列表》》
                Dictionary<string, Dictionary<string, ArrayList>> dicExecOrders = new Dictionary<string, Dictionary<string, ArrayList>>();
                //<患者ID，打印的执行档列表>
                Dictionary<string, ArrayList> dicPrintExecOrders = new Dictionary<string, ArrayList>();

                //存储患者实体
                Dictionary<string, FS.HISFC.Models.RADT.PatientInfo> dicPatientInfo = new Dictionary<string, FS.HISFC.Models.RADT.PatientInfo>();
                FS.HISFC.Models.Order.ExecOrder execOrder = new FS.HISFC.Models.Order.ExecOrder();


                //还需完善
                //保证同一个组合号 打印在同一张上面
                for (int index = 0; index < this.neuSpread1_Sheet1.RowCount; index++)
                {
                    if (this.neuSpread1_Sheet1.Cells[index, (int)EnumCol.S是否打印].Text == "False")
                    {
                        this.neuSpread1_Sheet1.Rows[index].Visible = false;//仅设为不可见====导致无数据全选仍可打印
                    }
                    else
                    {
                        if (!object.Equals(this.neuSpread1_Sheet1.Rows[index].Tag, null))//行TAG不为空则转换赋值
                        {
                            execOrder = this.neuSpread1_Sheet1.Rows[index].Tag as FS.HISFC.Models.Order.ExecOrder;

                            alPrint.Add(execOrder);//实体添加至ArrayList中

                            //构造数据----大字符串
                            string str = "";
                            for (int j = 0; j < this.neuSpread1_Sheet1.Columns.Count; j++)
                            {
                                str += this.neuSpread1_Sheet1.Cells[index, j].Text + "|"; //列循环,分隔符
                            }
                            str = str.Substring(0, str.Length - 1);

                            if (dicPrintExecOrders.ContainsKey(execOrder.Order.Patient.ID))
                            {
                                dicPrintExecOrders[execOrder.Order.Patient.ID].Add(str);
                            }
                            else
                            {
                                ArrayList alItems = new ArrayList();
                                alItems.Add(str);
                                dicPrintExecOrders.Add(execOrder.Order.Patient.ID, alItems);
                            }

                            if (dicExecOrders.ContainsKey(execOrder.Order.Patient.ID))
                            {
                                if (dicExecOrders[execOrder.Order.Patient.ID].ContainsKey(execOrder.Order.Combo.ID + execOrder.DateUse.ToString()))
                                {
                                    dicExecOrders[execOrder.Order.Patient.ID][execOrder.Order.Combo.ID + execOrder.DateUse.ToString()].Add(str);
                                }
                                else
                                {
                                    ArrayList alItems = new ArrayList();
                                    alItems.Add(str);
                                    dicExecOrders[execOrder.Order.Patient.ID].Add(execOrder.Order.Combo.ID + execOrder.DateUse.ToString(), alItems);
                                }
                            }
                            else
                            {
                                ArrayList alItems = new ArrayList();
                                alItems.Add(str);
                                Dictionary<string, ArrayList> dic = new Dictionary<string, ArrayList>();
                                dic.Add(execOrder.Order.Combo.ID + execOrder.DateUse.ToString(), alItems);
                                dicExecOrders.Add(execOrder.Order.Patient.ID, dic);

                                dicPatientInfo.Add(execOrder.Order.Patient.ID, execOrder.Order.Patient);
                            }
                        }
                    }
                }

                //设置“是否打印”列不显示
                this.neuSpread1_Sheet1.Columns[(int)EnumCol.S是否打印].Visible = false;

                print.SetPageSize(pSize);
                print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
                print.IsShowFarPointBorder = false;

                if (alPrint == null || alPrint.Count == 0)
                {
                    MessageBox.Show("没有可打印的数据，请重新选择！");
                }
                else //打印程序段
                {
                    foreach (string patientID in dicExecOrders.Keys)
                    {
                        //每页打印pagePrintCount行
                        Dictionary<string, ArrayList> dicComb = dicExecOrders[patientID];

                        //标尺而已
                        int count = 0;
                        //总页数
                        int pageCount = 0;

                        //<页码,每页行数>
                        Dictionary<int, int> dicPrintCount = new Dictionary<int, int>();

                        dicPrintCount.Add(0, 0);
                        pageCount += 1;

                        foreach (string combNo in dicComb.Keys)
                        {
                            if (count + dicComb[combNo].Count > pagePrintCount)
                            {
                                if (count > 0)
                                {
                                    dicPrintCount.Add(pageCount, dicPrintCount[pageCount - 1] + count);

                                    pageCount += 1;
                                }
                                count = dicComb[combNo].Count;
                                //一个组合超过pagePrintCount条
                                if (count > pagePrintCount)
                                {
                                    int tempPage = FS.FrameWork.Function.NConvert.ToInt32(Math.Ceiling(count / (decimal)pagePrintCount));
                                    for (int i = 0; i < tempPage; i++)
                                    {
                                        if (i == tempPage - 1)
                                        {
                                            dicPrintCount.Add(pageCount, dicPrintCount[pageCount - 1] + (count - (tempPage - 1) * pagePrintCount));
                                        }
                                        else
                                        {
                                            dicPrintCount.Add(pageCount, dicPrintCount[pageCount - 1] + pagePrintCount);
                                        }
                                        pageCount += 1;
                                    }
                                    count = 0;
                                }
                            }
                            else
                            {
                                count = count + dicComb[combNo].Count;
                            }
                        }
                        if (count > 0)
                        {
                            dicPrintCount.Add(pageCount, dicPrintCount[pageCount - 1] + count);
                        }


                        for (int page = 1; page <= pageCount; page++)
                        {
                            ArrayList alPagePrint = new ArrayList();
                            for (int i = 0; i < dicPrintExecOrders[patientID].Count; i++)
                            {
                                if (i >= dicPrintCount[page - 1] && i < dicPrintCount[page])
                                {
                                    alPagePrint.Add(dicPrintExecOrders[patientID][i]);
                                }
                            }

                            ucCircuitCardPrint ucPrint = new ucCircuitCardPrint();
                            ucPrint.AlOrders = alPagePrint;

                            ucPrint.SetHeader(this.lblTitle.Text, this.lblExecTime.Text, this.orderMgr.GetDateTimeFromSysDateTime().ToString(), page, pageCount);

                            ucPrint.SetPatientInfo(dicPatientInfo[patientID]);

                            if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
                            {
                                print.PrintPreview(0, 0, ucPrint);
                            }
                            else
                            {
                                print.PrintPage(0, 0, ucPrint);
                            }
                        }
                    }
                }

                #region 更新已经打印标记

                if (!this.isReprint)//首次打印
                {
                    FS.FrameWork.Management.PublicTrans.BeginTransaction();

                    this.orderMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                    if (alPrint == null || alPrint.Count == 0)
                    {
                        return;
                    }
                    foreach (FS.HISFC.Models.Order.ExecOrder exeOrder in alPrint)
                    {
                        if (orderMgr.UpdateCircultPrinted(exeOrder.Order.ID, dtBegin.ToString(), dtEnd.ToString()) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("更新打印标记失败!\r\n" + orderMgr.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    FS.FrameWork.Management.PublicTrans.Commit();
                }

                #endregion

                this.neuSpread1_Sheet1.RowCount = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return;
        }

        /// <summary>
        /// 查询显示数据
        /// </summary>
        /// <param name="patients"></param>
        /// <param name="usagecode"></param>
        /// <param name="dtBegin"></param>
        /// <param name="dtEnd"></param>
        /// <param name="isRePrint"></param>
        /// <param name="orderType"></param>
        /// <param name="isFirst"></param>
        public void Query(List<FS.HISFC.Models.RADT.PatientInfo> patients, string usagecode, DateTime dtBegin, DateTime dtEnd, bool isRePrint, string orderType, bool isFirst)
        {
            this.isReprint = isRePrint;
            this.dtBegin = dtBegin;
            this.dtEnd = dtEnd;

            #region 标题信息
            if (dtBegin.Date == dtEnd.Date)
            {
                this.lblExecTime.Text = dtBegin.ToString("yyyy年MM月dd日");
            }
            else
            {
                this.lblExecTime.Text = dtBegin.ToString("yyyy年MM月dd日") + "-" + dtEnd.ToString("yyyy年MM月dd日");
            }
            lblNurse.Text = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(((FS.HISFC.Models.Base.Employee)this.orderMgr.Operator).Nurse.ID);

            this.lblTitle.Text = "住院患者输液卡";

            this.neuSpread1_Sheet1.RowCount = 0;

            #endregion

            #region 组合患者流水号，用于批量查询

            string paramPatient = "";

            //获得in的患者id参数
            Dictionary<string, FS.HISFC.Models.RADT.PatientInfo> hsPatientInfo = new Dictionary<string, FS.HISFC.Models.RADT.PatientInfo>();
            for (int i = 0; i < patients.Count; i++)
            {
                FS.HISFC.Models.RADT.PatientInfo p = patients[i] as FS.HISFC.Models.RADT.PatientInfo;
                paramPatient = "'" + p.ID + "'," + paramPatient;

                if (!hsPatientInfo.ContainsKey(p.ID))
                {
                    hsPatientInfo.Add(p.ID, p);
                }
            }

            if (paramPatient == "")
            {
                paramPatient = "''";
            }
            else
            {
                paramPatient = paramPatient.Substring(0, paramPatient.Length - 1);//去掉后面的逗号
            }
            #endregion

            ArrayList alOrder = this.orderMgr.QueryOrderCircult(paramPatient, dtBegin, dtEnd, usagecode, isRePrint);
            if (alOrder == null)
            {
                MessageBox.Show("查询患者医嘱数据出现错误！请联系管理员！\r\n" + orderMgr.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (alOrder.Count == 0)
            {
                return;
            }

            ArrayList alShowOrder = new ArrayList();

            FS.HISFC.Models.Order.Inpatient.Order orderObj = new FS.HISFC.Models.Order.Inpatient.Order();
            foreach (FS.HISFC.Models.Order.ExecOrder exeOrder in alOrder)
            {
                #region 停止后的医嘱是否打印

                //医嘱停用后会自动作废停止时间后的执行档，所以只需要判断执行档是否有效即可
                if (!dcIsPrint &&
                    (!exeOrder.IsValid
                    || (exeOrder.Order.Status == 3
                    && exeOrder.DateUse > exeOrder.Order.EndTime)))
                {
                    continue;
                }
                #endregion

                //长嘱
                if (orderType == "1")
                {
                    if (!exeOrder.Order.OrderType.IsDecompose)
                    {
                        continue;
                    }
                }
                //临嘱
                else if (orderType == "0")
                {
                    if (exeOrder.Order.OrderType.IsDecompose)
                    {
                        continue;
                    }
                }

                //只打印首日量
                if (isFirst)
                {
                    if (exeOrder.Order.MOTime.Date != exeOrder.DateUse.Date)
                    {
                        continue;
                    }
                }

                alShowOrder.Add(exeOrder);
            }

            if (alShowOrder.Count == 0)
            {
                return;
            }

            this.ShowOrder(alShowOrder);
            return;
        }

        private int ShowOrder(ArrayList alOrder)
        {
            this.neuSpread1_Sheet1.Columns[(int)EnumCol.S是否打印].Visible = true;

            //SQL已经按照sortID、mo_order排序了，这里再根据床号、住院号排序，保证一个患者的药品非药品在一起
            alOrder.Sort(new ExecComparer());

            int rowcount = 0;
            foreach (FS.HISFC.Models.Order.ExecOrder execOrder in alOrder)
            {
                rowcount = neuSpread1_Sheet1.RowCount;
                neuSpread1_Sheet1.Rows.Add(rowcount, 1);

                this.neuSpread1_Sheet1.Cells[rowcount, (int)EnumCol.C床号].Text = execOrder.Order.Patient.PVisit.PatientLocation.Bed.ID.Substring(4);

                neuSpread1_Sheet1.Cells[rowcount, (int)EnumCol.X姓名].Text = execOrder.Order.Patient.Name;
                neuSpread1_Sheet1.Cells[rowcount, (int)EnumCol.Z组号].Text = execOrder.Order.SubCombNO.ToString();
                neuSpread1_Sheet1.Cells[rowcount, (int)EnumCol.Z组合号].Text = execOrder.Order.Combo.ID + execOrder.DateUse.ToString();
                neuSpread1_Sheet1.Cells[rowcount, (int)EnumCol.Z组标记].Text = "";

                //自备、嘱托标记  用于护士打印单据和医嘱单显示区分
                string byoStr = "";
                if (!execOrder.Order.OrderType.IsCharge || execOrder.Order.Item.ID == "999")
                {
                    if (execOrder.Order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        byoStr = "[自备]";
                    }
                    else
                    {
                        byoStr = "[嘱托]";
                    }
                }
                neuSpread1_Sheet1.Cells[rowcount, (int)EnumCol.Y医嘱名称].Text = byoStr + execOrder.Order.Item.Name;

                //规格
                if (execOrder.Order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                {
                    neuSpread1_Sheet1.Cells[rowcount, (int)EnumCol.G规格].Text = execOrder.Order.Item.Specs;
                }
                else
                {
                    neuSpread1_Sheet1.Cells[rowcount, (int)EnumCol.G规格].Text = SOC.HISFC.BizProcess.Cache.Fee.GetItem(execOrder.Order.Item.ID).Specs;
                }

                neuSpread1_Sheet1.Cells[rowcount, (int)EnumCol.S数量].Text = execOrder.Order.Qty.ToString("F2").TrimEnd('0').TrimEnd('.') + execOrder.Order.Unit;

                neuSpread1_Sheet1.Cells[rowcount, (int)EnumCol.Y用量].Text = execOrder.Order.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('0') + execOrder.Order.DoseUnit;
                neuSpread1_Sheet1.Cells[rowcount, (int)EnumCol.P频次].Text = execOrder.Order.Frequency.ID;
                neuSpread1_Sheet1.Cells[rowcount, (int)EnumCol.Y用法].Text = execOrder.Order.Usage.Name;
                neuSpread1_Sheet1.Cells[rowcount, (int)EnumCol.Z执行时间].Text = execOrder.DateUse.ToString("yyyy年MM月dd日 HH:mm");

                //医嘱备注
                string memo = (execOrder.Order.IsEmergency ? "[急]" : "") //加急
                    + this.orderMgr.TransHypotest(execOrder.Order.HypoTest) //皮试
                    + execOrder.Order.Memo;//备注

                if (execOrder.Order.Status == 3 || execOrder.Order.Status == 4)
                {
                    if (execOrder.DateUse >= execOrder.Order.EndTime)
                    {
                        neuSpread1_Sheet1.Rows[rowcount].ForeColor = Color.Red;
                    }
                }

                neuSpread1_Sheet1.Cells[rowcount, (int)EnumCol.B备注].Text = memo;
                neuSpread1_Sheet1.Cells[rowcount, (int)EnumCol.S是否打印].Value = "True";

                this.neuSpread1_Sheet1.Rows[rowcount].Tag = execOrder;
            }

            FS.SOC.Local.Order.ZhuHai.Classes.Function.DrawComboLeft(this.neuSpread1_Sheet1, (int)EnumCol.Z组合号, (int)EnumCol.Z组标记);

            return 1;
        }

        /// <summary>
        /// 处理手术室医嘱等，特殊医嘱（目前无用）
        /// </summary>
        /// <param name="speStr"></param>
        public void SetSpeOrderType(string speStr)
        {
        }

        /// <summary>
        /// 打印设置（无用）
        /// </summary>
        public void PrintSet()
        {

        }

        #endregion
    }

    public class ExecComparer : IComparer
    {
        #region IComparer 成员

        FS.HISFC.BizProcess.Integrate.Manager manager = new FS.HISFC.BizProcess.Integrate.Manager();

        public int Compare(object x, object y)
        {
            try
            {
                FS.HISFC.Models.Order.ExecOrder execOrder1 = x as FS.HISFC.Models.Order.ExecOrder;
                FS.HISFC.Models.Order.ExecOrder execOrder2 = y as FS.HISFC.Models.Order.ExecOrder;

                //排序优先级：床号、组号、排序号、使用时间
                string aa = execOrder1.Order.Patient.PVisit.PatientLocation.Bed.ID.Substring(4).PadLeft(8, '0') + execOrder1.Order.SubCombNO.ToString().PadLeft(4, '0') + execOrder1.Order.SortID.ToString().PadLeft(8, '0') + execOrder1.DateUse.ToString();

                string bb = execOrder2.Order.Patient.PVisit.PatientLocation.Bed.ID.Substring(4).PadLeft(8, '0') + execOrder2.Order.SubCombNO.ToString().PadLeft(4, '0') + execOrder2.Order.SortID.ToString().PadLeft(8, '0') + execOrder2.DateUse.ToString();

                return string.Compare(aa, bb);
            }
            catch
            {
                return 0;
            }
        }

        #endregion
    }



    /// <summary>
    /// 列枚举
    /// </summary>
    enum EnumCol
    {
        C床号,
        X姓名,
        Z组号,
        Z组标记,
        Y医嘱名称,
        G规格,
        S数量,
        Y用量,
        P频次,
        Y用法,
        Z执行时间,
        B备注,
        S是否打印,
        Z组合号
    };
}
