using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.Order.GuangZhou.ZDLY.CircuitControl 
{
    /// <summary>
    /// 执行单打印
    /// </summary>
    public partial class ucCircuitCardFP : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.HISFC.BizProcess.Interface.IPrintTransFusion
    {

        public ucCircuitCardFP()
        {
            InitializeComponent();
            this.neuSpread1.ButtonClicked += new FarPoint.Win.Spread.EditorNotifyEventHandler(neuSpread1_ButtonClicked);
        }

        #region IPrintTransFusion 成员

        FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
        
        FS.HISFC.BizLogic.Order.Order orderBizLogic = new FS.HISFC.BizLogic.Order.Order();
        
        FS.HISFC.BizLogic.Manager.Bed bedManager = new FS.HISFC.BizLogic.Manager.Bed();

        ArrayList curValues = null; //当前显示的数据



        /// <summary>
        /// 作废的执行档是否打印
        /// </summary>
        private bool dcIsPrint = false;

        /// <summary>
        /// 作废的执行档是否打印
        /// </summary>
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
        /// 未收费是否允许打印
        /// </summary>
        private bool noFeeIsPrint = false;

        /// <summary>
        /// 未收费是否允许打印
        /// </summary>
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
        private bool quitFeeIsPrint = true;

        /// <summary>
        /// 退费后的是否打印
        /// </summary>
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
        /// 是否补打
        /// </summary>
        bool isRePrint = false;

        List<FS.HISFC.Models.RADT.PatientInfo> myPatients = null;
        
        DateTime dt1;
        
        DateTime dt2;

        string usage = string.Empty;

        string orderType = string.Empty;

        bool isFirst = false;

        /// <summary>
        /// 
        /// </summary>
        protected string inpatientNo;

        #endregion



        #region 方法

        /// <summary>
        /// 画边框
        /// </summary>
        private void AddLine()
        {
            #region 内容

            //只显示下面的边框  bevelBorder1---普通线   bevelBorder2---粗黑线   bevelBorder3---空白线
            FarPoint.Win.BevelBorder bevelBorder1 = new FarPoint.Win.BevelBorder(FarPoint.Win.BevelBorderType.Raised, System.Drawing.SystemColors.ControlLightLight, System.Drawing.Color.Black, 1, false, false, false, true);

            FarPoint.Win.BevelBorder bevelBorder2 = new FarPoint.Win.BevelBorder(FarPoint.Win.BevelBorderType.Raised, System.Drawing.SystemColors.ControlLightLight, System.Drawing.Color.Black, 2, false, false, false, true);

            FarPoint.Win.BevelBorder bevelBorderNone = new FarPoint.Win.BevelBorder(FarPoint.Win.BevelBorderType.Raised, System.Drawing.SystemColors.ControlLightLight, System.Drawing.Color.White, 1, false, false, false, false);

            if (this.neuSpread1_Sheet1.Rows.Count > 0)//有数据
            {
                for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
                {
                    /////初始化时全选/////
                    this.neuSpread1_Sheet1.SetValue(i, (int)ExecBillCols.PrintFlag, this.chkAll.Checked);


                    if (!string.IsNullOrEmpty(neuSpread1_Sheet1.Cells[i, (int)ExecBillCols.ComboMemo].Text.Trim())
                        && neuSpread1_Sheet1.Cells[i, (int)ExecBillCols.ComboMemo].Text.Trim() != "┏")
                    {
                        neuSpread1_Sheet1.Cells[i, (int)ExecBillCols.BedID].Text = "";
                        neuSpread1_Sheet1.Cells[i, (int)ExecBillCols.PatientName].Text = "";
                        //neuSpread1_Sheet1.Cells[i, (int)ExecBillCols.FrequencyID].Text = "";
                        //neuSpread1_Sheet1.Cells[i, (int)ExecBillCols.UsageName].Text = "";
                        //neuSpread1_Sheet1.Cells[i, (int)ExecBillCols.Memo].Text = "";


                        //neuSpread1_Sheet1.Cells[i, (int)ExecBillCols.ComboMemo].Text = "";
                    }

                    if (i == this.neuSpread1_Sheet1.RowCount - 1)//未完
                    {
                        this.neuSpread1_Sheet1.Rows[i].Border = bevelBorder2;
                        break;
                    }

                    //先全都画细线
                    neuSpread1_Sheet1.Rows[i].Border = bevelBorder1;

                    //组内用空白线
                    if (this.neuSpread1_Sheet1.Cells[i, (int)ExecBillCols.ComboMemo].Text.Trim() == "┏"
                        || this.neuSpread1_Sheet1.Cells[i, (int)ExecBillCols.ComboMemo].Text == "┃")
                    {
                        neuSpread1_Sheet1.Rows[i].Border = bevelBorderNone;
                    }

                    //不同床号的画粗线
                    if (this.neuSpread1_Sheet1.Cells[i + 1, (int)ExecBillCols.BedID].Tag.ToString() != this.neuSpread1_Sheet1.Cells[i, (int)ExecBillCols.BedID].Tag.ToString())
                    {
                        this.neuSpread1_Sheet1.Rows[i].Border = bevelBorder2;
                    }
                }
            }

            #endregion


            #region 标题

            FarPoint.Win.BevelBorder headerBorder = new FarPoint.Win.BevelBorder(FarPoint.Win.BevelBorderType.Raised, System.Drawing.SystemColors.ControlLightLight, System.Drawing.Color.Black, 2, false, true, false, true);

            for (int i = 0; i < this.neuSpread1_Sheet1.ColumnHeader.Rows.Count; i++)
            {
                this.neuSpread1_Sheet1.ColumnHeader.Rows[i].Border = headerBorder;
            }

            #endregion
        }

        /// <summary>
        /// 同组一起勾选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void neuSpread1_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (e.Column == (int)ExecBillCols.PrintFlag)
            {
                string combNo = neuSpread1_Sheet1.Cells[e.Row, (int)ExecBillCols.ComboMemo].Tag.ToString();

                for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
                {
                    if (neuSpread1_Sheet1.Cells[i, (int)ExecBillCols.ComboMemo].Tag.ToString() == combNo)
                    {
                        this.neuSpread1_Sheet1.Cells[i, (int)ExecBillCols.PrintFlag].Text = neuSpread1_Sheet1.Cells[e.Row, e.Column].Text;
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        private void AddToFP(ArrayList list)
        {
            this.neuSpread1_Sheet1.Rows.Count = 0;
            if (list == null || list.Count == 0)
            {
                return;
            }
            int index = 0;
            string itemName = "";
            
            int length = System.Text.Encoding.GetEncoding("gb2312").GetBytes("呵呵哈哈哈哈哈哈哈哈哈呵呵呵哈哈").Length;
            //int length = 16;

            FarPoint.Win.Spread.CellType.CheckBoxCellType chb = new FarPoint.Win.Spread.CellType.CheckBoxCellType();

            this.neuSpread1_Sheet1.Columns[(int)ExecBillCols.PrintFlag].CellType = chb;

            foreach (FS.HISFC.Models.Order.ExecOrder execOrder in list)
            {
                this.neuSpread1_Sheet1.Rows.Add(index, 1);
                this.neuSpread1_Sheet1.Rows[index].Tag = execOrder;
                //开始时间
                this.neuSpread1_Sheet1.Cells[index, (int)ExecBillCols.OrderState].Text = (execOrder.IsCharge == false ? "未" : "" )+ (execOrder.Order.OrderType.IsCharge == false ? "[嘱]" : "");
                //床号
                this.neuSpread1_Sheet1.Cells[index, (int)ExecBillCols.BedID].Text = execOrder.Order.Patient.PVisit.PatientLocation.Bed.ID.Substring(4);
                this.neuSpread1_Sheet1.Cells[index, (int)ExecBillCols.BedID].Tag = execOrder.Order.Patient.PVisit.PatientLocation.Bed.ID.Substring(4);

                //姓名
                this.neuSpread1_Sheet1.Cells[index, (int)ExecBillCols.PatientName].Text = execOrder.Order.Patient.Name;
                //if (execOrder.Order.Patient.Name.Length > (int)ExecBillCols.ComboMemo)
                //{
                //    this.neuSpread1_Sheet1.Cells[index, (int)ExecBillCols.PatientName].Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                //}

                //组号
                this.neuSpread1_Sheet1.Cells[index, (int)ExecBillCols.ComboNo].Text = execOrder.Order.Combo.ID + execOrder.DateUse.ToString();

                string type = (execOrder.Order.OrderType.IsDecompose ? "长" : "临") + execOrder.Order.SubCombNO.ToString();
                neuSpread1_Sheet1.Cells[index, (int)ExecBillCols.SubCombNo].Text = type;

                this.neuSpread1_Sheet1.Cells[index, (int)ExecBillCols.ComboMemo].Text = execOrder.Order.Combo.Memo;
                this.neuSpread1_Sheet1.Cells[index, (int)ExecBillCols.ComboMemo].Tag = execOrder.Order.Combo.ID;

                //名称
                this.neuSpread1_Sheet1.Cells[index,  (int)ExecBillCols.ItemName].Text = execOrder.Order.Item.Name;

                if (execOrder.Order.Item.ID != "999")
                {
                    //规格
                    if (execOrder.Order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        this.neuSpread1_Sheet1.Cells[index, (int)ExecBillCols.Specs].Text = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(execOrder.Order.Item.ID).Specs;
                    }
                    else
                    {
                        this.neuSpread1_Sheet1.Cells[index, (int)ExecBillCols.Specs].Text = SOC.HISFC.BizProcess.Cache.Fee.GetItem(execOrder.Order.Item.ID).Specs;
                    }
                }

                //数量
                this.neuSpread1_Sheet1.Cells[index, (int)ExecBillCols.OrderQty].Text = " " + execOrder.Order.Qty.ToString("F4").TrimEnd('0').TrimEnd('.') + execOrder.Order.Unit.TrimEnd(' ');
                //频次
                this.neuSpread1_Sheet1.Cells[index, (int)ExecBillCols.FrequencyID].Text = execOrder.Order.Frequency.ID;
                //用法
                this.neuSpread1_Sheet1.Cells[index, (int)ExecBillCols.UsageName].Text = execOrder.Order.Usage.Name;
                //每次量
                this.neuSpread1_Sheet1.Cells[index, (int)ExecBillCols.DoseOnce].Text = execOrder.Order.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + execOrder.Order.DoseUnit;

                //执行次数
                this.neuSpread1_Sheet1.Cells[index, (int)ExecBillCols.Memo].Text = execOrder.Memo;
                this.neuSpread1_Sheet1.Cells[index, (int)ExecBillCols.Memo].Text = hsMorder_useTime[execOrder.Order.ID];

                //签名
                this.neuSpread1_Sheet1.Cells[index, (int)ExecBillCols.ExecSignature].Text = "";
                //执行时间
                this.neuSpread1_Sheet1.Cells[index, (int)ExecBillCols.ExecTime].Text = ".";

                //医嘱备注
                string memo = (execOrder.Order.IsEmergency ? "[急]" : "") //加急
                    + orderBizLogic.TransHypotest(execOrder.Order.HypoTest) //皮试
                    + execOrder.Order.Memo;//备注
                this.neuSpread1_Sheet1.Cells[index, (int)ExecBillCols.OrderMemo].Text = memo;

                //默认全部打印
                this.neuSpread1_Sheet1.Cells[index, (int)ExecBillCols.PrintFlag].Value = "True";

                if (!string.IsNullOrEmpty(execOrder.Order.Memo))
                {
                    execOrder.Order.Memo = "(" + execOrder.Order.Memo + ")";
                }

                if (execOrder.Order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                {
                    //if (isDisplayRegularName && execOrder.Order.Item.ID != "999")
                    //{
                    //    FS.HISFC.Models.Pharmacy.Item phaItem = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(execOrder.Order.Item.ID) as FS.HISFC.Models.Pharmacy.Item;
                    //    itemName = phaItem.NameCollection.RegularName + execOrder.Order.Memo + "[" + phaItem.BaseDose.ToString("F4").TrimEnd('0').TrimEnd('.') + phaItem.DoseUnit + "]";
                    //}
                    //else
                    {
                        FS.HISFC.Models.Pharmacy.Item phaitem = (execOrder.Order.Item as FS.HISFC.Models.Pharmacy.Item);
                        itemName = execOrder.Order.Item.Name + execOrder.Order.Memo + "[" + phaitem.BaseDose.ToString("F4").TrimEnd('0').TrimEnd('.') + phaitem.DoseUnit + "]";

                    }

                }
                else
                {
                    if (!string.IsNullOrEmpty(execOrder.Order.Item.Specs))
                    {
                        itemName = execOrder.Order.Item.Name + execOrder.Order.Memo + "[" + execOrder.Order.Item.Specs + "]";
                    }
                    else
                    {
                        itemName = execOrder.Order.Item.Name + execOrder.Order.Memo;
                    }
                }

                //名称过长自动换行
                //if (System.Text.Encoding.GetEncoding("gb2312").GetBytes(itemName).Length > length)
                //{
                //    this.neuSpread1_Sheet1.Rows[index].Height = 2 * this.neuSpread1_Sheet1.Rows[index].Height;
                //}
                //this.neuSpread1_Sheet1.Cells[index, (int)ExecBillCols.ItemName].Text = itemName;

                //this.neuSpread1_Sheet1.Cells[index, (int)ExecBillCols.ItemName].Text = FS.SOC.Local.Order.GuangZhou.Classes.Function.GetStrintByLength(itemName,length);

                #region 停用信息

                if (execOrder.Order.Status == 3 || execOrder.Order.Status == 4)
                {
                    if (execOrder.DateUse >= execOrder.Order.EndTime)
                    {
                        neuSpread1_Sheet1.Rows[index].ForeColor = Color.Red;
                        neuSpread1_Sheet1.Cells[index, (int)ExecBillCols.Memo].Text = neuSpread1_Sheet1.Cells[index, (int)ExecBillCols.Memo].Text;
                    }
                }

                #endregion

                index++;
            }
        }

        /// <summary>
        /// 每页打印的行数
        /// </summary>
        int pagePrintCount = 12;

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
                FS.HISFC.Models.Base.PageSize pSize = pgMgr.GetPageSize("execBillPaper1");

                if (pSize == null)
                {
                    pSize = new FS.HISFC.Models.Base.PageSize("Letter", 850, 500);
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
                    if (this.neuSpread1_Sheet1.Cells[index, (int)ExecBillCols.PrintFlag].Text == "False")
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

                //只更新打印的执行单状态
                this.curValues = alPrint;    //实体添加进ArrayList中

                //设置“是否打印”列不显示
                this.neuSpread1_Sheet1.Columns[(int)ExecBillCols.PrintFlag].Visible = false;

                print.SetPageSize(pSize);
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

                            ucPrint.SetHeader(this.lblTitle.Text, this.neuLblExecTime.Text, this.orderBizLogic.GetDateTimeFromSysDateTime().ToString(), page, pageCount);

                            ucPrint.SetPatientInfo(dicPatientInfo[patientID]);

                            if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
                            {
                                print.PrintPreview(20, 0, ucPrint);
                            }
                            else
                            {
                                print.PrintPage(20, 0, ucPrint);
                            }
                        }
                    }

                    #region 按照行数打印

                    /*
                    ArrayList alPagePrint = null;

                    int pageNo = 0;
                    foreach (string key in dicExecOrders.Keys)
                    {
                        ArrayList aa = dicExecOrders[key];

                        int fromPage = 1;
                        int toPage = (Int32)Math.Ceiling((decimal)aa.Count / 12);
                        for (int page = fromPage; page <= toPage; page++)
                        {
                            alPagePrint = new ArrayList();

                            for (int i = 0; i < aa.Count; i++)
                            {
                                if (i >= (page - 1) * 12 && i < page * 12)//每页显示15条
                                {
                                    alPagePrint.Add(aa[i] as string);
                                }
                            }
                            pageNo++;
                            ucCircuitCardPrint ucPrint = new ucCircuitCardPrint();
                            ucPrint.AlOrders = alPagePrint;

                            ucPrint.SetHeader(this.lblTitle.Text, this.neuLblExecTime.Text, this.neuLblExecTime.Text, pageNo, toPage);

                            ucPrint.SetPatientInfo(dicPatientInfo[key]);

                            if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
                            {
                                print.PrintPreview(20, 0, ucPrint);
                            }
                            else
                            {
                                print.PrintPage(20, 0, ucPrint);
                            }
                        }
                    }
                     * */
                    #endregion
                }

                //打印后设置“是否打印”列显示
                this.neuSpread1_Sheet1.Columns[(int)ExecBillCols.PrintFlag].Visible = true;

                #region 更新已经打印标记
                if (!this.isRePrint)//首次打印
                {
                    FS.FrameWork.Management.PublicTrans.BeginTransaction();

                    this.orderBizLogic.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                    if (alPrint == null || alPrint.Count == 0)
                    {
                        return;
                    }
                    foreach (FS.HISFC.Models.Order.ExecOrder exeOrder in alPrint)
                    {
                        if (this.orderBizLogic.UpdateCircultPrinted(exeOrder.Order.ID, dt1.ToString(), dt2.ToString()) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("更新打印标记失败!" + orderBizLogic.Err);
                            return;
                        }
                    }
                    FS.FrameWork.Management.PublicTrans.Commit();

                    this.Query(myPatients, usage, dt1, dt2, isRePrint, orderType, isFirst);

                }
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return;
        }

        /// <summary>
        /// 
        /// </summary>
        public void PrintSet()
        {
            //print.ShowPrintPageDialog();
            //this.Print();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="alValues"></param>
        protected void SetValues(ArrayList alValues)
        {
            // curValues = alValues;
            if (alValues != null)
            {
                if (this.neuSpread1_Sheet1.RowCount > 0)
                {
                    this.neuSpread1_Sheet1.RowCount = 0;
                }

                //ArrayList newList = new ArrayList();
                ////alValues.Sort(new ComparerExecOrder());
                //this.AddConstsToTable(alValues, ref newList);
                ArrayList newList = alValues;


                this.neuSpread1_Sheet1.Columns[(int)ExecBillCols.PrintFlag].Visible = true;

                //SQL已经按照sortID、mo_order排序了，这里再根据床号、住院号排序，保证一个患者的药品非药品在一起
                newList.Sort(new ComparerExecOrder());
                this.AddToFP(newList);
                FS.SOC.Local.Order.GuangZhou.Classes.Function.DrawComboLeft(this.neuSpread1_Sheet1, (int)(int)ExecBillCols.ComboNo, (int)ExecBillCols.ComboMemo);

               this.AddLine();
            }
        }

       

        #endregion

        #region 事件
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
            {
                this.neuSpread1_Sheet1.SetValue(i, (int)ExecBillCols.PrintFlag, this.chkAll.Checked);
            }
        }
        #endregion


        #region IPrintTransFusion 成员

        /// <summary>
        /// 获取日期定位显示：明、昨、..日
        /// </summary>
        /// <param name="dateUse"></param>
        /// <param name="printDate"></param>
        /// <returns></returns>
        private string GetDateSpan(DateTime dateUse, DateTime printDate)
        {
            int hour = dateUse.Hour;
            if (hour == 0)
            {
                hour = 24;
                dateUse = dateUse.AddDays(-1);
            }

            TimeSpan span = new TimeSpan(dateUse.Date.Ticks - printDate.Date.Ticks);

            if (span.Days == -1)
            {
                return "昨" + (hour <= 12 ? hour.ToString() + "a" : (hour - 12).ToString() + "p");
            }
            else if (span.Days == 1)
            {
                return "明" + (hour <= 12 ? hour.ToString() + "a" : (hour - 12).ToString() + "p");
            }
            if (span.Days == 2)
            {
                return "后" + (hour <= 12 ? hour.ToString() + "a" : (hour - 12).ToString() + "p");
            }
            if (span.Days == 0)
            {
                return "" + (hour <= 12 ? hour.ToString() + "a" : (hour - 12).ToString() + "p");
            }

            return "[" + dateUse.Day.ToString() + "日]" + (hour <= 12 ? hour.ToString() + "a" : (hour - 12).ToString() + "p");
        }

        /// <summary>
        /// 存储《moder,useTime》
        /// </summary>
        Dictionary<string, string> hsMorder_useTime = new Dictionary<string, string>();

        public void Query(List<FS.HISFC.Models.RADT.PatientInfo> patients, string usageCode, DateTime dtBegin, DateTime dtEnd, bool isPrinted, string orderType, bool isFirst)
        {
            this.neuLblExecTime.Text = dtBegin.ToString("MM月dd日") + "-" + dtEnd.ToString("MM月dd日");
            lblDept.Text = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(((FS.HISFC.Models.Base.Employee)this.orderBizLogic.Operator).Nurse.ID);

            this.dt1 = dtBegin;
            this.dt2 = dtEnd;
            this.isRePrint = isPrinted;
            this.usage = usageCode;
            this.orderType = orderType;
            this.isFirst = isFirst;
            
            //给患者列表赋值
            this.myPatients = patients;
            //更改治疗单标题
            //this.dwMain.Modify("t_title.text= " + "'" + this.Tag.ToString() + "'");

            this.lblTitle.Text = "住院患者输液卡";

            ArrayList alOrder = new ArrayList();
            //ArrayList al = new ArrayList();
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

            alOrder = this.orderBizLogic.QueryOrderCircult(paramPatient, dtBegin, dtEnd, usageCode, isPrinted);


            FS.HISFC.Models.Order.Inpatient.Order orderObj = new FS.HISFC.Models.Order.Inpatient.Order();
            ArrayList alOrderTemp = new ArrayList();

            hsMorder_useTime.Clear();

            foreach (FS.HISFC.Models.Order.ExecOrder exeOrder in alOrder)
            {
                #region 处理停用、退费、首日量等特殊医嘱

                //医嘱停用后会自动作废停止时间后的执行档，所以只需要判断执行档是否有效即可
                if (!dcIsPrint && !exeOrder.IsValid)
                {
                    continue;
                }

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

                #endregion

                exeOrder.Order.Patient.Name = hsPatientInfo[exeOrder.Order.Patient.ID].Name;

                #region 停用信息
                string strValid_execOrder = "";
                if (exeOrder.Order.Status == 3 || exeOrder.Order.Status == 4)
                {
                    if (exeOrder.DateUse >= exeOrder.Order.EndTime)
                    {
                        strValid_execOrder = "[停]";
                    }
                }

                #endregion

                #region 记录医嘱使用时间

                //长嘱
                if (exeOrder.Order.OrderType.IsDecompose

                    //临嘱每天只有一个频次
                    || Classes.Function.GetFrequencyCountByOneDay(exeOrder.Order.Frequency.ID) <= 1)
                {
                    if (!hsMorder_useTime.ContainsKey(exeOrder.Order.ID))
                    {
                        hsMorder_useTime.Add(exeOrder.Order.ID, GetDateSpan(exeOrder.DateUse, dtBegin) + strValid_execOrder);

                        alOrderTemp.Add(exeOrder);
                    }
                    else
                    {
                        hsMorder_useTime[exeOrder.Order.ID] += "、" + GetDateSpan(exeOrder.DateUse, dtBegin) + strValid_execOrder;
                    }
                }
                //临嘱BID等 显示两个时间点
                else
                {
                    FS.HISFC.Models.Order.Frequency freqObj = SOC.HISFC.BizProcess.Cache.Order.GetFrequency(exeOrder.Order.Frequency.ID);
                    for (int i = 0; i < freqObj.Times.Length; i++)
                    {
                        DateTime time = Convert.ToDateTime(freqObj.Times[i]);
                        if (!hsMorder_useTime.ContainsKey(exeOrder.Order.ID))
                        {
                            hsMorder_useTime.Add(exeOrder.Order.ID, GetDateSpan(time, dtBegin) + strValid_execOrder);

                            alOrderTemp.Add(exeOrder);
                        }
                        else
                        {
                            hsMorder_useTime[exeOrder.Order.ID] += "、" + GetDateSpan(time, dtBegin) + strValid_execOrder;
                        }
                    }

                    //string execTime = orderBizLogic.ExecSqlReturnOne(string.Format("select exec_times from met_ipm_order where mo_order='{0}'", exeOrder.Order.ID), "");
                    //if (!string.IsNullOrEmpty(execTime))
                    //{
                    //    string[] times = execTime.Split('-');
                    //    for (int i = 0; i < times.Length; i++)
                    //    {
                    //        DateTime dt = exeOrder.DateUse.Date.AddHours(FS.FrameWork.Function.NConvert.ToInt32(times[i].Substring(0, times[i].IndexOf(':'))));
                    //        if (!hsMorder_useTime.ContainsKey(exeOrder.Order.ID))
                    //        {
                    //            hsMorder_useTime.Add(exeOrder.Order.ID, GetDateSpan(dt, dtBegin) + strValid_execOrder);

                    //            alOrderTemp.Add(exeOrder);
                    //        }
                    //        else
                    //        {
                    //            hsMorder_useTime[exeOrder.Order.ID] += "、" + GetDateSpan(dt, dtBegin) + strValid_execOrder;
                    //        }
                    //    }
                    //}
                }

                #endregion
            }

            alOrder = alOrderTemp;

            this.curValues = alOrder;

            #region 将同一条医嘱合并在一起

            #region 旧的作废

            /*
            ArrayList alTemp = alOrder.Clone() as ArrayList;
            alOrder = new ArrayList();
            for (int k = 0; k < alTemp.Count; k++)
            {
                bool isHave = false;

                TimeSpan span;
                string sMing = "";
                for (int j = 0; j < alOrder.Count; j++)
                {
                    sMing = "";
                    if (((FS.HISFC.Models.Order.ExecOrder)alOrder[j]).Order.ID == ((FS.HISFC.Models.Order.ExecOrder)alTemp[k]).Order.ID )
                    {
                        if (((FS.HISFC.Models.Order.ExecOrder)alOrder[j]).Order.Memo.Length > 2 &&
                            ((FS.HISFC.Models.Order.ExecOrder)alOrder[j]).Order.Memo.Substring(0, 2) == "时间")
                        {
                            //特殊频次
                        }
                        else
                        {
                            isHave = true;//包含添加时间
                            span = new TimeSpan(((FS.HISFC.Models.Order.ExecOrder)alTemp[k]).DateUse.Date.Ticks - dtBegin.Date.Ticks);
                            if (span.Days == 1) sMing = "明";
                            if (span.Days == 2) sMing = "后";
                            if (span.Days > 2) sMing = "[" + ((FS.HISFC.Models.Order.ExecOrder)alTemp[k]).DateUse.Day.ToString() + "日]";
                            
                            if (((FS.HISFC.Models.Order.ExecOrder)alTemp[k]).DateUse.Hour <= 12)
                            {
                                ((FS.HISFC.Models.Order.ExecOrder)alOrder[j]).Memo += "," +sMing+ ((FS.HISFC.Models.Order.ExecOrder)alTemp[k]).DateUse.Hour.ToString() + "a";
                            }
                            else
                            {
                                ((FS.HISFC.Models.Order.ExecOrder)alOrder[j]).Memo += "," +sMing+ (((FS.HISFC.Models.Order.ExecOrder)alTemp[k]).DateUse.Hour - 12).ToString() + "p";
                            }
                            break;
                        }
                    }
                }
                if (!isHave)
                {
                    span = new TimeSpan(((FS.HISFC.Models.Order.ExecOrder)alTemp[k]).DateUse.Date.Ticks - dtBegin.Date.Ticks);
                    if (span.Days == 1) sMing = "明";
                    if (span.Days == 2) sMing = "后";
                    if (span.Days > 2) sMing = "[" + ((FS.HISFC.Models.Order.ExecOrder)alTemp[k]).DateUse.Day.ToString() + "日]";
                   


                    if (((FS.HISFC.Models.Order.ExecOrder)alTemp[k]).Order.OrderType.Type == FS.HISFC.Models.Order.EnumType.LONG)//临时医嘱暂时不显示执行时间
                    {
                        if (((FS.HISFC.Models.Order.ExecOrder)alTemp[k]).DateUse.Hour <= 12)
                        {
                            ((FS.HISFC.Models.Order.ExecOrder)alTemp[k]).Memo += sMing+((FS.HISFC.Models.Order.ExecOrder)alTemp[k]).DateUse.Hour.ToString() + "a";
                        }
                        else
                        {
                            ((FS.HISFC.Models.Order.ExecOrder)alTemp[k]).Memo += sMing+(((FS.HISFC.Models.Order.ExecOrder)alTemp[k]).DateUse.Hour - 12).ToString() + "p";
                        }
                    }
                    else
                    {
                        //临时打印时间点
                        for (int i = 0; i < ((FS.HISFC.Models.Order.ExecOrder)alTemp[k]).Order.Frequency.Times.Length; i++)
                        {
                            DateTime time = Convert.ToDateTime(((FS.HISFC.Models.Order.ExecOrder)alTemp[k]).Order.Frequency.Times[i]);
                            if (time.Hour <= 12)
                            {
                                ((FS.HISFC.Models.Order.ExecOrder)alTemp[k]).Memo += sMing + time.Hour.ToString() + "a,";
                            }
                            else
                            {
                                ((FS.HISFC.Models.Order.ExecOrder)alTemp[k]).Memo += sMing + time.Hour.ToString() + "p,";
                            }
                        }
                        ((FS.HISFC.Models.Order.ExecOrder)alTemp[k]).Memo = ((FS.HISFC.Models.Order.ExecOrder)alTemp[k]).Memo.TrimEnd(',');


                    }

                    for (int kk = 0; kk < patients.Count; kk++)
                    {
                        if (((FS.FrameWork.Models.NeuObject)patients[kk]).ID == ((FS.HISFC.Models.Order.ExecOrder)alTemp[k]).Order.Patient.ID)
                        {
                            ((FS.HISFC.Models.Order.ExecOrder)alTemp[k]).Order.Patient = ((FS.HISFC.Models.RADT.PatientInfo)patients[kk]).Clone();
                            break;
                        }
                    }

                    alOrder.Add(alTemp[k]);
                }
            }
             * */
            #endregion

            #endregion

            this.SetValues(alOrder);
            return;
        }

        /// <summary>
        /// 设置手术室等特殊医嘱
        /// </summary>
        /// <param name="speStr"></param>
        public void SetSpeOrderType(string speStr)
        {
            //this.speOrderType = speStr;
            //this.speOrderType = "";
        }

        #endregion

        private void ucCircuitCardFP_Load(object sender, EventArgs e)
        {
        }
    }

    /// <summary>
    /// 门诊主界面列
    /// </summary>
    public enum ExecBillCols
    {
        /// <summary>
        /// 医嘱时间
        /// </summary>
        OrderState = 0,
        /// <summary>
        /// 床号
        /// </summary>
        BedID,
        /// <summary>
        /// 患者姓名
        /// </summary>
        PatientName,


        /// <summary>
        /// 组号
        /// </summary>
        SubCombNo,

        /// <summary>
        /// 组合标记
        /// </summary>
        ComboNo,
        /// <summary>
        /// 组合标记
        /// </summary>
        ComboMemo,
        /// <summary>
        /// 医嘱名称
        /// </summary>
        ItemName,
        /// <summary>
        /// 规格
        /// </summary>
        Specs,
        /// <summary>
        /// 医嘱数量
        /// </summary>
        OrderQty,
        /// <summary>
        /// 频次编码
        /// </summary>
        FrequencyID,
        /// <summary>
        /// 用法名称
        /// </summary>
        UsageName,
        /// <summary>
        /// 每次用量
        /// </summary>
        DoseOnce,
        /// <summary>
        /// 预计执行时间
        /// </summary>
        Memo,

        /// <summary>
        /// 备注
        /// </summary>
        OrderMemo,

        /// <summary>
        /// 执行签名
        /// </summary>
        ExecSignature1,

        /// <summary>
        /// 执行签名
        /// </summary>
        ExecSignature,

        /// <summary>
        /// 执行时间
        /// </summary>
        ExecTime1,

        /// <summary>
        /// 执行时间
        /// </summary>
        ExecTime,

        /// <summary>
        /// 打印标记
        /// </summary>
        PrintFlag
    }

    public class ComparerExecOrder : IComparer
    {
        #region IComparer 成员

        FS.HISFC.BizProcess.Integrate.Manager manager = new FS.HISFC.BizProcess.Integrate.Manager();

        public int Compare(object x, object y)
        {
            try
            {
                FS.HISFC.Models.Order.ExecOrder execOrder1 = x as FS.HISFC.Models.Order.ExecOrder;
                FS.HISFC.Models.Order.ExecOrder execOrder2 = y as FS.HISFC.Models.Order.ExecOrder;
                //string aa = manager.GetBed(execOrder1.Order.Patient.PVisit.PatientLocation.Bed.ID).SortID.ToString().PadLeft(4, '0') + execOrder1.Order.Patient.PVisit.PatientLocation.Bed.ID;
                //string bb = manager.GetBed(execOrder2.Order.Patient.PVisit.PatientLocation.Bed.ID).SortID.ToString().PadLeft(4, '0') + execOrder2.Order.Patient.PVisit.PatientLocation.Bed.ID;

                string aa = execOrder1.Order.Patient.PVisit.PatientLocation.Bed.ID.Substring(4).PadLeft(8, '0') + execOrder1.Order.Patient.PVisit.PatientLocation.Bed.ID;
                string bb = execOrder2.Order.Patient.PVisit.PatientLocation.Bed.ID.Substring(4).PadLeft(8, '0') + execOrder2.Order.Patient.PVisit.PatientLocation.Bed.ID;

                string cc = execOrder1.Order.MOTime.ToString("yyyyMMdd") + execOrder1.Order.Combo.ID + execOrder1.DateUse.ToString() + execOrder1.Order.ID;
                string dd = execOrder2.Order.MOTime.ToString("yyyyMMdd") + execOrder2.Order.Combo.ID + execOrder1.DateUse.ToString() + execOrder2.Order.ID;

                if (string.Compare(aa,bb) > 0)
                {
                    return 1;
                }
                else if (string.Compare(aa, bb) == 0)
                {
                    return string.Compare(cc, dd);
                }
                else
                {
                    return -1;
                }
            }
            catch
            {
                return 0;
            }
        }

        #endregion
    }
}
