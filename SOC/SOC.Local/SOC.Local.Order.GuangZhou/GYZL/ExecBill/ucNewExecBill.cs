using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.Order.GuangZhou.GYZL.ExecBill
{
    public partial class ucNewExecBill : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucNewExecBill()
        {
            InitializeComponent();
        }

        #region 变量

        /// <summary>
        /// 执行单业务层
        /// </summary>
        private FS.HISFC.BizLogic.Order.ExecBill execBillManager = new FS.HISFC.BizLogic.Order.ExecBill();

        /// <summary>
        /// 医嘱业务层
        /// </summary>
        private FS.HISFC.BizLogic.Order.Order orderManager = new FS.HISFC.BizLogic.Order.Order();

        /// <summary>
        /// 常数业务层
        /// </summary>
        private FS.HISFC.BizLogic.Manager.Constant constantManager = new FS.HISFC.BizLogic.Manager.Constant();

        /// <summary>
        /// 住院患者业务层
        /// </summary>
        private FS.HISFC.BizLogic.RADT.InPatient inPatientManager = new FS.HISFC.BizLogic.RADT.InPatient();

        /// <summary>
        /// 执行单
        /// </summary>
        private ArrayList alExecBill = null;

        /// <summary>
        /// 患者列表
        /// </summary>
        private List<FS.HISFC.Models.RADT.PatientInfo> patientList = new List<FS.HISFC.Models.RADT.PatientInfo>();

        /// <summary>
        /// 打印控件
        /// </summary>
        private ucNewExecBillPrint print = new ucNewExecBillPrint();

        #endregion

        #region 方法

        private void init()
        {
            this.getExecBill();
        }

        private void getExecBill()
        {
            alExecBill = execBillManager.QueryExecBill(((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Nurse.ID);
            if (alExecBill == null)
            {
                MessageBox.Show("获得执行单设置出错！");
                return;
            }
            for (int i = 0, count = 0; count < alExecBill.Count && i < this.gbUsage.Controls.Count; i++)
            {
                //除tag为ALL的都隐藏
                if ("ALL".Equals(this.gbUsage.Controls[i].Tag))
                {
                    continue;
                }
                this.gbUsage.Controls[i].Text = ((FS.FrameWork.Models.NeuObject)alExecBill[count]).Name;
                this.gbUsage.Controls[i].Tag = alExecBill[count];
                this.gbUsage.Controls[i].Visible = true;
                count++;
            }
            this.cbbPrint.Items.Add(new FS.FrameWork.Models.NeuObject("0", "按执行单汇总打印", ""));
            this.cbbPrint.Items.Add(new FS.FrameWork.Models.NeuObject("1", "按执行单分开患者打印", ""));
            this.cbbPrint.Items.Add(new FS.FrameWork.Models.NeuObject("2", "按患者单独打印", ""));
            this.cbbPrint.SelectedIndex = 0;
        }

        #region 查询

        /// <summary>
        /// 查询全部患者
        /// </summary>
        /// <param name="list"></param>
        private void query(List<FS.HISFC.Models.RADT.PatientInfo> list)
        {
            this.neuSpread1_Sheet1.RowCount = 0;
            ArrayList alPrintExecBill = new ArrayList();
            for (int i = 0; i < this.gbUsage.Controls.Count; i++)
            {
                if (this.gbUsage.Controls[i].Visible == true && !(this.gbUsage.Controls[i].Tag is string))
                {
                    CheckBox temp = this.gbUsage.Controls[i] as CheckBox;
                    if (temp != null && temp.Checked == true)
                    {
                        alPrintExecBill.Add(this.gbUsage.Controls[i].Tag);
                    }
                }
            }
            foreach (FS.HISFC.Models.RADT.PatientInfo patient in list)
            {
                this.query(patient, alPrintExecBill);
            }
        }

        /// <summary>
        /// 查询患者
        /// </summary>
        /// <param name="patient"></param>
        private void query(FS.HISFC.Models.RADT.PatientInfo patient, ArrayList alPrintExecBill)
        {
            if (alPrintExecBill == null)
            {
                return;
            }
            ArrayList alExecOrder = new ArrayList();
            ArrayList alOrderID = new ArrayList();
            ArrayList alRepeatOrder = new ArrayList();
            foreach (FS.FrameWork.Models.NeuObject obj in alPrintExecBill)
            {
                alExecOrder = orderManager.QueryOrderExecBill(patient.ID,
                    new DateTime(dtpTime.Value.Year, dtpTime.Value.Month, dtpTime.Value.Day, 0, 0, 0),
                    new DateTime(dtpTime.Value.Year, dtpTime.Value.Month, dtpTime.Value.Day, 23, 59, 59),
                    obj.ID, this.cbIsPrinted.Checked);
                foreach (FS.HISFC.Models.Order.ExecOrder execOrder in alExecOrder)
                {
                    execOrder.Memo = obj.ID;//将执行单号暂时保存
                    if (alOrderID.Contains(execOrder.Order.ID))
                    {
                        alRepeatOrder.Add(execOrder);
                    }
                    else
                    {
                        alOrderID.Add(execOrder.Order.ID);
                    }
                }
                foreach (FS.HISFC.Models.Order.ExecOrder execOrder in alRepeatOrder)
                {
                    alExecOrder.Remove(execOrder);
                }
                if (alExecOrder.Count != 0)
                {
                    this.addData(patient, alExecOrder);
                }
            }

        }

        #endregion

        #region 表格赋值

        /// <summary>
        /// 将组号转化为符号
        /// </summary>
        private void changeCombMark()
        {
            int combCount = 0;
            bool isEnd = false;
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount - 1; i++)
            {
                string firstCombID = this.neuSpread1_Sheet1.Cells[i, (int)Columns.组].Tag.ToString();
                string secondCombID = this.neuSpread1_Sheet1.Cells[i + 1, (int)Columns.组].Tag.ToString();
                if (firstCombID.Equals(secondCombID))
                {
                    combCount++;
                }
                else if (combCount > 0)
                {
                    isEnd = true;
                }

                if (combCount == 1 && !isEnd)
                {
                    this.neuSpread1_Sheet1.Cells[i, (int)Columns.组].Text = "┏";
                }
                else if (combCount > 1 && !isEnd)
                {
                    this.neuSpread1_Sheet1.Cells[i, (int)Columns.组].Text = "┃";
                }
                else if (combCount > 0 && isEnd)
                {
                    this.neuSpread1_Sheet1.Cells[i, (int)Columns.组].Text = "┗";
                    combCount = 0;
                    isEnd = false;
                }

                if (i == this.neuSpread1_Sheet1.RowCount - 2)
                {
                    if (firstCombID.Equals(secondCombID))
                    {
                        this.neuSpread1_Sheet1.Cells[i, (int)Columns.组].Text = "┗";
                    }
                }
            }
        }

        /// <summary>
        /// 添加数据至表格
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="alExecOrder"></param>
        private void addData(FS.HISFC.Models.RADT.PatientInfo patient, ArrayList alExecOrder)
        {
            foreach (FS.HISFC.Models.Order.ExecOrder execOrder in alExecOrder)
            {
                if (this.rbtAll.Checked)
                {
                    addRow(patient, execOrder);
                }
                else if (this.rbtLong.Checked)
                {
                    if (execOrder.Order.OrderType.IsDecompose)
                    {
                        addRow(patient, execOrder);
                    }
                }
                else if (this.rbtShort.Checked)
                {
                    if (!execOrder.Order.OrderType.IsDecompose)
                    {
                        addRow(patient, execOrder);
                    }
                }
            }
        }

        /// <summary>
        /// 添加数据至行
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="execOrder"></param>
        private void addRow(FS.HISFC.Models.RADT.PatientInfo patient, FS.HISFC.Models.Order.ExecOrder execOrder)
        {
            int curRow = this.neuSpread1_Sheet1.RowCount++;
            this.neuSpread1_Sheet1.Rows[curRow].Font = new Font("宋体", 10F);
            this.neuSpread1_Sheet1.Rows[curRow].Tag = execOrder;
            this.neuSpread1_Sheet1.Cells[curRow, (int)Columns.姓名].Text = patient.Name;
            this.neuSpread1_Sheet1.Cells[curRow, (int)Columns.姓名].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Cells[curRow, (int)Columns.床号].Text = patient.PVisit.PatientLocation.Bed.ID.Substring(4);
            this.neuSpread1_Sheet1.Cells[curRow, (int)Columns.类型].Text = (execOrder.Order.OrderType.IsDecompose ? "长" : "临");
            this.neuSpread1_Sheet1.Cells[curRow, (int)Columns.组].Tag = execOrder.Order.Combo.ID;
            this.neuSpread1_Sheet1.Cells[curRow, (int)Columns.打印].Text = this.cbIsPrinted.Checked ? "已" : "未";
            this.neuSpread1_Sheet1.Cells[curRow, (int)Columns.项目名称].Text = execOrder.Order.Item.Name;
            this.neuSpread1_Sheet1.Cells[curRow, (int)Columns.规格].Text = execOrder.Order.Item.Specs;
            if (execOrder.Order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
            {
                this.neuSpread1_Sheet1.Cells[curRow, (int)Columns.用量].Text = execOrder.Order.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.');
                this.neuSpread1_Sheet1.Cells[curRow, (int)Columns.单位].Text = execOrder.Order.DoseUnit;
            }
            else
            {
                this.neuSpread1_Sheet1.Cells[curRow, (int)Columns.用量].Text = execOrder.Order.Qty.ToString("F4").TrimEnd('0').TrimEnd('.');
                this.neuSpread1_Sheet1.Cells[curRow, (int)Columns.单位].Text = execOrder.Order.Unit;
            }
            this.neuSpread1_Sheet1.Cells[curRow, (int)Columns.用法].Text = execOrder.Order.Usage.Name;
            this.neuSpread1_Sheet1.Cells[curRow, (int)Columns.频率].Text = execOrder.Order.Frequency.Name;
            this.neuSpread1_Sheet1.Cells[curRow, (int)Columns.医嘱流水号].Text = execOrder.Order.ID;
            this.neuSpread1_Sheet1.Cells[curRow, (int)Columns.住院号].Text = patient.ID;
            this.neuSpread1_Sheet1.Cells[curRow, (int)Columns.住院号].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Cells[curRow, (int)Columns.是否打印].Text = "True";
            this.neuSpread1_Sheet1.Cells[curRow, (int)Columns.是否打印].CellType = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            this.neuSpread1_Sheet1.Cells[curRow, (int)Columns.是否打印].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
        }

        /// <summary>
        /// 清空表格
        /// </summary>
        private void clearSheet()
        {
            this.neuSpread1_Sheet1.RowCount = 0;
        }

        #endregion

        #region 打印相关

        private void printPage()
        {
            FS.FrameWork.WinForms.Classes.Print printer = new FS.FrameWork.WinForms.Classes.Print();
            this.print.ResetSize();
            printer.SetPageSize(new FS.HISFC.Models.Base.PageSize("ExecOrder", this.print.Width + 20, this.print.Height + 10));
            if (((FS.HISFC.Models.Base.Employee)this.constantManager.Operator).IsManager)
            {
                printer.PrintPreview(5, 5, this.print);
            }
            else
            {
                printer.PrintPage(5, 5, this.print);
            }
            this.print.neuSpread1_Sheet1.RowCount = 0;
        }

        private void addPrintData(ArrayList alExecOrder)
        {
            int curRow = 0;
            int count = 0;
            string combID = string.Empty;
            string lastPatientID = string.Empty;
            foreach (FS.HISFC.Models.Order.ExecOrder execOrder in alExecOrder)
            {
                string curPatientID = execOrder.Order.Patient.ID;
                if (!lastPatientID.Equals(curPatientID))
                {
                    string bedNo = inPatientManager.GetPatientInfoByPatientNO(execOrder.Order.Patient.ID).PVisit.PatientLocation.Bed.ID.Substring(4);
                    string name = inPatientManager.GetPatientInfoByPatientNO(execOrder.Order.Patient.ID).Name;
                    addPrintPatientHead(bedNo, name);
                    addPrintHead();
                }
                FS.HISFC.Models.Order.Frequency frequency = SOC.HISFC.BizProcess.Cache.Order.GetFrequency(execOrder.Order.Frequency.ID);
                if (combID.Equals(execOrder.Order.Combo.ID))
                {
                    curRow = this.print.neuSpread1_Sheet1.RowCount - count;

                    this.print.neuSpread1_Sheet1.Rows[curRow].Height += 20;
                    this.print.neuSpread1_Sheet1.Cells[curRow, 0].Text += "\n" + execOrder.Order.Item.Name;
                    this.print.neuSpread1_Sheet1.Cells[curRow, 2].Text += "\n" + execOrder.Order.DoseOnce + execOrder.Order.DoseUnit + " ";
                }
                else
                {
                    combID = execOrder.Order.Combo.ID;
                    count = frequency.Times.Length;
                    curRow = this.print.neuSpread1_Sheet1.RowCount;
                    this.print.neuSpread1_Sheet1.RowCount += count;
                    this.print.neuSpread1_Sheet1.Cells[curRow, 0].Text = execOrder.Order.Item.Name;
                    this.print.neuSpread1_Sheet1.Cells[curRow, 0].RowSpan = count;
                    this.print.neuSpread1_Sheet1.Cells[curRow, 0].Font = new Font("宋体", 10);
                    if (SOC.HISFC.BizProcess.Cache.Common.GetUsage(execOrder.Order.Usage.ID) != null)
                    {
                        this.print.neuSpread1_Sheet1.Cells[curRow, 1].Text = SOC.HISFC.BizProcess.Cache.Common.GetUsage(execOrder.Order.Usage.ID).Name;
                    }
                    this.print.neuSpread1_Sheet1.Cells[curRow, 1].RowSpan = count;
                    this.print.neuSpread1_Sheet1.Cells[curRow, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                    this.print.neuSpread1_Sheet1.Cells[curRow, 1].Font = new Font("宋体", 10);
                    if (execOrder.Order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        this.print.neuSpread1_Sheet1.Cells[curRow, 2].Text = execOrder.Order.DoseOnce + execOrder.Order.DoseUnit + " ";
                    }
                    else
                    {
                        this.print.neuSpread1_Sheet1.Cells[curRow, 2].Text = execOrder.Order.Qty + execOrder.Order.Unit + " ";
                    }
                    this.print.neuSpread1_Sheet1.Cells[curRow, 2].RowSpan = count;
                    this.print.neuSpread1_Sheet1.Cells[curRow, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                    this.print.neuSpread1_Sheet1.Cells[curRow, 2].Font = new Font("宋体", 10);
                    this.print.neuSpread1_Sheet1.Cells[curRow, 3].Text = execOrder.Order.Frequency.ID;
                    this.print.neuSpread1_Sheet1.Cells[curRow, 3].RowSpan = count;
                    this.print.neuSpread1_Sheet1.Cells[curRow, 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                    this.print.neuSpread1_Sheet1.Cells[curRow, 3].Font = new Font("宋体", 10);
                }
                lastPatientID = curPatientID;
            }
        }

        private void addPrintTitle(string billName, string dept, string execDate)
        {
            int curRow = this.print.neuSpread1_Sheet1.RowCount++;
            this.print.neuSpread1_Sheet1.Cells[curRow, 0].ColumnSpan = 7;
            this.print.neuSpread1_Sheet1.Cells[curRow, 0].Text = billName + "    科室:" + dept + "    执行时间:" + execDate + "  核对签名:";
            this.print.neuSpread1_Sheet1.Cells[curRow, 0].Font = new Font("宋体", 10, FontStyle.Bold);
        }

        private void addPrintPatientHead(string bedNo, string name)
        {
            int curRow = this.print.neuSpread1_Sheet1.RowCount++;
            this.print.neuSpread1_Sheet1.Cells[curRow, 0].ColumnSpan = 7;
            this.print.neuSpread1_Sheet1.Cells[curRow, 0].Text = "床号:" + bedNo + "    姓名:" + name;
            this.print.neuSpread1_Sheet1.Cells[curRow, 0].Font = new Font("宋体", 10, FontStyle.Bold);
        }

        private void addPrintHead()
        {
            int curRow = this.print.neuSpread1_Sheet1.RowCount++;
            this.print.neuSpread1_Sheet1.Cells[curRow, 0].Text = "项目名称";
            this.print.neuSpread1_Sheet1.Cells[curRow, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.print.neuSpread1_Sheet1.Cells[curRow, 0].Font = new Font("宋体", 10, FontStyle.Bold);
            this.print.neuSpread1_Sheet1.Cells[curRow, 1].Text = "用法";
            this.print.neuSpread1_Sheet1.Cells[curRow, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.print.neuSpread1_Sheet1.Cells[curRow, 1].Font = new Font("宋体", 10, FontStyle.Bold);
            this.print.neuSpread1_Sheet1.Cells[curRow, 2].Text = "每次用量";
            this.print.neuSpread1_Sheet1.Cells[curRow, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.print.neuSpread1_Sheet1.Cells[curRow, 2].Font = new Font("宋体", 10, FontStyle.Bold);
            this.print.neuSpread1_Sheet1.Cells[curRow, 3].Text = "频次";
            this.print.neuSpread1_Sheet1.Cells[curRow, 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.print.neuSpread1_Sheet1.Cells[curRow, 3].Font = new Font("宋体", 10, FontStyle.Bold);
            this.print.neuSpread1_Sheet1.Cells[curRow, 4].Text = "加药签名";
            this.print.neuSpread1_Sheet1.Cells[curRow, 4].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.print.neuSpread1_Sheet1.Cells[curRow, 4].Font = new Font("宋体", 10, FontStyle.Bold);
            this.print.neuSpread1_Sheet1.Cells[curRow, 5].Text = "执行时间";
            this.print.neuSpread1_Sheet1.Cells[curRow, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.print.neuSpread1_Sheet1.Cells[curRow, 5].Font = new Font("宋体", 10, FontStyle.Bold);
            this.print.neuSpread1_Sheet1.Cells[curRow, 6].Text = "执行签名";
            this.print.neuSpread1_Sheet1.Cells[curRow, 6].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.print.neuSpread1_Sheet1.Cells[curRow, 6].Font = new Font("宋体", 10, FontStyle.Bold);
        }

        private void addBlankRow()
        {
            int curRow = this.print.neuSpread1_Sheet1.RowCount++;
            this.print.neuSpread1_Sheet1.Cells[curRow, 0].ColumnSpan = 8;
        }

        #endregion

        #region 更新打印标志

        private void updatePrintFlag(ArrayList printData)
        {
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.orderManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            string itemType = string.Empty;
            foreach (FS.HISFC.Models.Order.ExecOrder execOrder in printData)
            {
                if (execOrder.Order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                {
                    itemType = "1";
                }
                else
                {
                    itemType = "2";
                }
                if (this.orderManager.UpdateExecOrderPrintedByMoOrder(execOrder.Order.ID,
                    (new DateTime(dtpTime.Value.Year, dtpTime.Value.Month, dtpTime.Value.Day, 0, 0, 0)).ToString(),
                    (new DateTime(dtpTime.Value.Year, dtpTime.Value.Month, dtpTime.Value.Day, 23, 59, 59)).ToString(), itemType) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("更新打印标记失败!\n" + orderManager.Err);
                    return;
                }
            }
        }

        #endregion

        #endregion

        #region 事件

        /// <summary>
        /// 界面加载
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            this.init();
            base.OnLoad(e);
        }

        protected override int OnSetValue(object neuObject, TreeNode e)
        {
            if (tv != null && tv.CheckBoxes == false)
                tv.CheckBoxes = true;
            return base.OnSetValue(neuObject, e);
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnPrint(object sender, object neuObject)
        {
            ArrayList needPrintData = new ArrayList();
            //打印方式,0为按执行单打印,1为按患者打印
            string printModes = ((FS.FrameWork.Models.NeuObject)this.cbbPrint.Items[cbbPrint.SelectedIndex]).ID;
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                if ("True".Equals(this.neuSpread1_Sheet1.Cells[i, (int)Columns.是否打印].Text))
                {
                    FS.HISFC.Models.Order.ExecOrder execOrder = this.neuSpread1_Sheet1.Rows[i].Tag as FS.HISFC.Models.Order.ExecOrder;
                    if (execOrder != null)
                    {
                        needPrintData.Add(execOrder);
                    }
                }
            }
            if (needPrintData.Count == 0)
            {
                return 0;
            }
            if (!printModes.Equals("2"))
            {
                #region 按执行单打印
                needPrintData.Sort(new OrderComparer());
                string lastBillID = string.Empty;
                ArrayList printData = new ArrayList();//单个执行单数据
                string billName = string.Empty;
                for (int i = 0; i < needPrintData.Count; i++)
                {
                    FS.HISFC.Models.Order.ExecOrder tempOrder = needPrintData[i] as FS.HISFC.Models.Order.ExecOrder;
                    string curBillID = tempOrder.Memo;

                    if (!lastBillID.Equals(curBillID))
                    {
                        //执行单号不一致,判断前一个执行单是否为空
                        if (printData.Count > 0)
                        {
                            if ("0".Equals(printModes))
                            {
                                //按执行单打印
                                addPrintTitle(billName, SOC.HISFC.BizProcess.Cache.Common.GetDeptInfo(tempOrder.Order.ReciptDept.ID).Name, tempOrder.DateUse.ToShortDateString());
                                addPrintData(printData);
                                addBlankRow();
                            }
                            else if ("1".Equals(printModes))
                            {
                                //按执行单分患者打印,根据printData中患者再分解
                                int count = 0;
                                string patientID = string.Empty;
                                ArrayList printPatientData = new ArrayList();
                                do
                                {
                                    string nextPatientID = ((FS.HISFC.Models.Order.ExecOrder)printData[count]).Order.Patient.ID;
                                    if (string.IsNullOrEmpty(patientID) || patientID.Equals(nextPatientID))
                                    {
                                        printPatientData.Add(printData[count]);
                                    }
                                    else
                                    {
                                        //打印
                                        addPrintTitle(billName, SOC.HISFC.BizProcess.Cache.Common.GetDeptInfo(tempOrder.Order.ReciptDept.ID).Name, tempOrder.DateUse.ToShortDateString());
                                        addPrintData(printPatientData);
                                        addBlankRow();
                                        printPatientData.Clear();
                                        printPatientData.Add(printData[count]);
                                    }
                                    if (count == printData.Count - 1)
                                    {
                                        //最后一个患者打印
                                        addPrintTitle(billName, SOC.HISFC.BizProcess.Cache.Common.GetDeptInfo(tempOrder.Order.ReciptDept.ID).Name, tempOrder.DateUse.ToShortDateString());
                                        addPrintData(printPatientData);
                                        addBlankRow();
                                        printPatientData.Clear();
                                    }
                                    patientID = nextPatientID;
                                    count++;
                                } while (count < printData.Count);
                            }
                        }
                        printData.Clear();
                        ArrayList alBill = execBillManager.QueryExecBill(((FS.HISFC.Models.Base.Employee)constantManager.Operator).Dept.ID);
                        foreach (FS.FrameWork.Models.NeuObject obj in alBill)
                        {
                            if (obj.ID.Equals(curBillID))
                            {
                                billName = obj.Name;
                                break;
                            }
                        }
                        printData.Add(tempOrder);
                    }
                    else
                    {
                        //执行单号一致,添加到当前执行单
                        printData.Add(tempOrder);
                    }
                    lastBillID = curBillID;
                    if (i == needPrintData.Count - 1)
                    {
                        if ("0".Equals(printModes))
                        {
                            //按执行单打印
                            addPrintTitle(billName, SOC.HISFC.BizProcess.Cache.Common.GetDeptInfo(tempOrder.Order.ReciptDept.ID).Name, tempOrder.DateUse.ToShortDateString());
                            addPrintData(printData);
                            addBlankRow();
                        }
                        else if ("1".Equals(printModes))
                        {
                            //按执行单分患者打印,根据printData中患者再分解
                            int count = 0;
                            string patientID = string.Empty;
                            ArrayList printPatientData = new ArrayList();
                            do
                            {
                                string nextPatientID = ((FS.HISFC.Models.Order.ExecOrder)printData[count]).Order.Patient.ID;
                                if (string.IsNullOrEmpty(patientID) || patientID.Equals(nextPatientID))
                                {
                                    printPatientData.Add(printData[count]);
                                }
                                else
                                {
                                    //打印
                                    addPrintTitle(billName, SOC.HISFC.BizProcess.Cache.Common.GetDeptInfo(tempOrder.Order.ReciptDept.ID).Name, tempOrder.DateUse.ToShortDateString());
                                    addPrintData(printPatientData);
                                    addBlankRow();
                                    printPatientData.Clear();
                                    printPatientData.Add(printData[count]);
                                }
                                if (count == printData.Count - 1)
                                {
                                    //最后一个患者打印
                                    addPrintTitle(billName, SOC.HISFC.BizProcess.Cache.Common.GetDeptInfo(tempOrder.Order.ReciptDept.ID).Name, tempOrder.DateUse.ToShortDateString());
                                    addPrintData(printPatientData);
                                    addBlankRow();
                                    printPatientData.Clear();
                                }
                                patientID = nextPatientID;
                                count++;
                            } while (count < printData.Count);
                        }
                    }
                }
                #endregion
            }
            else
            {
                #region 按患者打印

                needPrintData.Sort(new OrderComparerByPatient());
                string lastPatientID = string.Empty;
                ArrayList printData = new ArrayList();//单个执行单数据
                for (int i = 0; i < needPrintData.Count; i++)
                {
                    FS.HISFC.Models.Order.ExecOrder tempOrder = needPrintData[i] as FS.HISFC.Models.Order.ExecOrder;
                    string curPatientID = tempOrder.Order.Patient.ID;
                    if (!string.IsNullOrEmpty(lastPatientID) && !lastPatientID.Equals(curPatientID))
                    {
                        addPrintTitle("执行单", SOC.HISFC.BizProcess.Cache.Common.GetDeptInfo(tempOrder.Order.ReciptDept.ID).Name, tempOrder.DateUse.ToShortDateString());
                        addPrintData(printData);
                        addBlankRow();
                        printData.Clear();
                    }

                    printData.Add(needPrintData[i]);

                    lastPatientID = curPatientID;
                    if (i == needPrintData.Count - 1)
                    {
                        addPrintTitle("执行单", SOC.HISFC.BizProcess.Cache.Common.GetDeptInfo(tempOrder.Order.ReciptDept.ID).Name, tempOrder.DateUse.ToShortDateString());
                        addPrintData(printData);
                    }
                }

                #endregion
            }
            printPage();
            if (!this.cbIsPrinted.Checked)
            {
                updatePrintFlag(needPrintData);
            }
            return 0;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="alValues"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        protected override int OnSetValues(ArrayList alValues, object e)
        {
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在查询，请稍后……");
            Application.DoEvents();
            this.patientList.Clear();

            this.clearSheet();

            foreach (FS.HISFC.Models.RADT.PatientInfo p in alValues)
            {
                this.patientList.Add(p);
            }
            this.query(this.patientList);
            this.changeCombMark();
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            return 0;
        }

        /// <summary>
        /// 全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbAll_CheckedChanged(object sender, EventArgs e)
        {
            foreach (CheckBox cb in this.gbUsage.Controls)
            {
                cb.Checked = this.cbAll.Checked;
            }
        }

        /// <summary>
        /// 全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                this.neuSpread1_Sheet1.Cells[i, (int)Columns.是否打印].Text = "True";
            }
        }

        /// <summary>
        /// 清空
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                this.neuSpread1_Sheet1.Cells[i, (int)Columns.是否打印].Text = "False";
            }
        }

        /// <summary>
        /// 选择同患者
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPatient_Click(object sender, EventArgs e)
        {
            ArrayList checkedPatient = new ArrayList();
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                if ("True".Equals(this.neuSpread1_Sheet1.Cells[i, (int)Columns.是否打印].Text))
                {
                    string inPatientNo = this.neuSpread1_Sheet1.Cells[i, (int)Columns.住院号].Text;
                    if (!checkedPatient.Contains(inPatientNo))
                    {
                        checkedPatient.Add(inPatientNo);
                    }
                }
            }
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                string inPatientNo = this.neuSpread1_Sheet1.Cells[i, (int)Columns.住院号].Text;
                if (checkedPatient.Contains(inPatientNo))
                {
                    this.neuSpread1_Sheet1.Cells[i, (int)Columns.是否打印].Text = "True";
                }
            }
        }

        /// <summary>
        /// 选取同组
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuSpread1_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            int row = this.neuSpread1_Sheet1.ActiveRowIndex;
            bool isSelect = Convert.ToBoolean(this.neuSpread1_Sheet1.Cells[row, (int)Columns.是否打印].Value);
            string combID = this.neuSpread1_Sheet1.Cells[row, (int)Columns.组].Tag.ToString();
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                if (this.neuSpread1_Sheet1.Cells[i, (int)Columns.组].Tag.ToString() == combID)
                {
                    this.neuSpread1_Sheet1.Cells[i, (int)Columns.是否打印].Value = isSelect;
                }
            }
        }

        /// <summary>
        /// 时间变更时清空表格
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtpTime_ValueChanged(object sender, EventArgs e)
        {
            this.clearSheet();
        }

        #endregion

        #region 表格列

        enum Columns
        {
            医嘱流水号,
            住院号,
            姓名,
            床号,
            类型,
            组,
            打印,
            项目名称,
            规格,
            用量,
            单位,
            用法,
            频率,
            是否打印
        }

        #endregion

        #region 比较器
        /// <summary>
        /// 执行档排序类,按执行单、床号、组号
        /// </summary>
        class OrderComparer : IComparer
        {
            //执行单、床号、组号
            private FS.HISFC.BizLogic.RADT.InPatient inPatientManager = new FS.HISFC.BizLogic.RADT.InPatient();

            public int Compare(object x, object y)
            {
                FS.HISFC.Models.Order.ExecOrder lExecOrder = x as FS.HISFC.Models.Order.ExecOrder;
                FS.HISFC.Models.Order.ExecOrder rExecOrder = y as FS.HISFC.Models.Order.ExecOrder;
                if (lExecOrder != null && rExecOrder != null)
                {
                    int lExecOrderID = FS.FrameWork.Function.NConvert.ToInt32(lExecOrder.Memo);
                    int rExecOrderID = FS.FrameWork.Function.NConvert.ToInt32(rExecOrder.Memo);
                    if (lExecOrderID == rExecOrderID)
                    {
                        int lBedID = FS.FrameWork.Function.NConvert.ToInt32(inPatientManager.GetPatientInfoByPatientNO(lExecOrder.Order.Patient.ID).PVisit.PatientLocation.Bed.ID.Substring(4));
                        int rBedID = FS.FrameWork.Function.NConvert.ToInt32(inPatientManager.GetPatientInfoByPatientNO(rExecOrder.Order.Patient.ID).PVisit.PatientLocation.Bed.ID.Substring(4));
                        if (lBedID == rBedID)
                        {
                            int lExecOrderCombID = FS.FrameWork.Function.NConvert.ToInt32(lExecOrder.Order.Combo.ID);
                            int rExecOrderCombID = FS.FrameWork.Function.NConvert.ToInt32(rExecOrder.Order.Combo.ID);
                            return lExecOrderCombID.CompareTo(rExecOrderCombID);
                        }
                        else
                        {
                            return lBedID.CompareTo(rBedID);
                        }
                    }
                    else
                    {
                        return lExecOrderID.CompareTo(rExecOrderID);
                    }
                }
                return 0;
            }
        }

        /// <summary>
        /// 执行档排序类,按患者、执行单、组号
        /// </summary>
        class OrderComparerByPatient : IComparer
        {
            //患者、组号
            private FS.HISFC.BizLogic.RADT.InPatient inPatientManager = new FS.HISFC.BizLogic.RADT.InPatient();

            public int Compare(object x, object y)
            {
                FS.HISFC.Models.Order.ExecOrder lExecOrder = x as FS.HISFC.Models.Order.ExecOrder;
                FS.HISFC.Models.Order.ExecOrder rExecOrder = y as FS.HISFC.Models.Order.ExecOrder;
                if (lExecOrder != null && rExecOrder != null)
                {
                    int lPatientID = FS.FrameWork.Function.NConvert.ToInt32(lExecOrder.Order.Patient.ID);
                    int rPatientID = FS.FrameWork.Function.NConvert.ToInt32(rExecOrder.Order.Patient.ID);
                    if (lPatientID == rPatientID)
                    {
                        int lExecOrderID = FS.FrameWork.Function.NConvert.ToInt32(lExecOrder.Memo);
                        int rExecOrderID = FS.FrameWork.Function.NConvert.ToInt32(rExecOrder.Memo);
                        if (lExecOrderID == rExecOrderID)
                        {
                            int lExecOrderCombID = FS.FrameWork.Function.NConvert.ToInt32(lExecOrder.Order.Combo.ID);
                            int rExecOrderCombID = FS.FrameWork.Function.NConvert.ToInt32(rExecOrder.Order.Combo.ID);
                            return lExecOrderCombID.CompareTo(rExecOrderCombID);
                        }
                        else
                        {
                            return lExecOrderID.CompareTo(rExecOrderID);
                        }
                    }
                    else
                    {
                        return lPatientID.CompareTo(rPatientID);
                    }
                }
                return 0;
            }
        }

        #endregion
    }
}
