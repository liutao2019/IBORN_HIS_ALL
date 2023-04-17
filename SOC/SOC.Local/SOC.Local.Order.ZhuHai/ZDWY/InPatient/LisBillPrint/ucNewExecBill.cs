using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace Neusoft.SOC.Local.Order.ZhuHai.ZDWY.InPatient.ExecBill
{
    public partial class ucNewExecBill : Neusoft.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucNewExecBill()
        {
            InitializeComponent();
        }

        #region 变量

        /// <summary>
        /// 执行单业务层
        /// </summary>
        private Neusoft.HISFC.BizLogic.Order.ExecBill execBillManager = new Neusoft.HISFC.BizLogic.Order.ExecBill();

        /// <summary>
        /// 医嘱业务层
        /// </summary>
        private Neusoft.HISFC.BizLogic.Order.Order orderManager = new Neusoft.HISFC.BizLogic.Order.Order();

        /// <summary>
        /// 常数业务层
        /// </summary>
        private Neusoft.HISFC.BizLogic.Manager.Constant constantManager = new Neusoft.HISFC.BizLogic.Manager.Constant();

        /// <summary>
        /// 住院患者业务层
        /// </summary>
        private Neusoft.HISFC.BizLogic.RADT.InPatient inPatientManager = new Neusoft.HISFC.BizLogic.RADT.InPatient();

        /// <summary>
        /// 执行单
        /// </summary>
        private ArrayList _alExecBill = null;

        /// <summary>
        /// 医嘱流水号对应的执行档信息
        /// </summary>
        private Dictionary<string, ArrayList> _dicExecByOrderNO = new Dictionary<string, ArrayList>();

        /// <summary>
        /// 患者列表
        /// </summary>
        private List<Neusoft.HISFC.Models.RADT.PatientInfo> _patientList = new List<Neusoft.HISFC.Models.RADT.PatientInfo>();

        /// <summary>
        /// 打印控件
        /// </summary>
        private ExecBillPrintControlNew _printControl = new ExecBillPrintControlNew();

        /// <summary>
        /// 打印方式；0按执行单打印，1按患者打印
        /// </summary>
        private string _printModes = "0";

        #endregion

        #region 属性

        /// <summary>
        /// 打印方式；0按执行单打印，1按患者打印
        /// </summary>
        public string PrintModes
        {
            get { return _printModes; }
            set { _printModes = value; }
        }

        /// <summary>
        /// 默认的执行结束时间
        /// </summary>
        private string endTime = "12:01:00";

        /// <summary>
        /// 查询截止时间
        /// </summary>
        [Category("查询设置"), Description("默认的查询结束时间，如 12:01:00")]
        public string EndTime
        {
            get
            {
                return this.endTime;
            }
            set
            {
                this.endTime = value;
            }
        }

        /// <summary>
        /// 开始时间距今的间隔天数
        /// </summary>
        private int beginDateSpanDay = 0;

        /// <summary>
        /// 开始时间距今的间隔天数
        /// </summary>
        [Category("查询设置"), Description("默认的查询开始时间距今的间隔天数")]
        public int BeginDateSpanDay
        {
            get
            {
                return beginDateSpanDay;
            }
            set
            {
                beginDateSpanDay = value;
            }
        }

        /// <summary>
        /// 默认的查询结束时间距今的间隔天数
        /// </summary>
        private int endDateSpanDay = 1;

        /// <summary>
        /// 默认的查询结束时间距今的间隔天数
        /// </summary>
        [Category("查询设置"), Description("默认的查询结束时间距今的间隔天数")]
        public int EndDateSpanDay
        {
            get
            {
                return endDateSpanDay;
            }
            set
            {
                endDateSpanDay = value;
            }
        }

        /// <summary>
        /// 查询开始时间
        /// </summary>
        string beginTime = "12:01:00";

        /// <summary>
        /// 查询开始时间
        /// </summary>
        [Category("查询设置"), Description("默认的查询结束时间,如：12:01:00")]
        public string BeginTime
        {
            get
            {
                return beginTime;
            }
            set
            {
                beginTime = value;
            }
        }

        #endregion

        #region 方法

        private void init()
        {
            DateTime dtNow = constantManager.GetDateTimeFromSysDateTime();

            DateTime dt1 = new DateTime(dtNow.Year, dtNow.Month, dtNow.Day, 0, 0, 0);

            DateTime dt2 = new DateTime(dtNow.AddDays(1).Year, dtNow.AddDays(1).Month, dtNow.AddDays(1).Day, 12, 00, 00);
            if (!string.IsNullOrEmpty(beginTime))
            {
                dt1 = Neusoft.FrameWork.Function.NConvert.ToDateTime(dtNow.AddDays(beginDateSpanDay).ToString("yyyy.MM.dd") + " " + beginTime);
            }
            if (!string.IsNullOrEmpty(endTime))
            {
                dt2 = Neusoft.FrameWork.Function.NConvert.ToDateTime(dtNow.AddDays(endDateSpanDay).ToString("yyyy.MM.dd") + " " + endTime);
            }

            this.dtpTime.Value = dt1;
            this.dtpEnd.Value = dt2;

            this.getExecBill();
        }

        private void getExecBill()
        {
            _alExecBill = execBillManager.QueryExecBill(((Neusoft.HISFC.Models.Base.Employee)Neusoft.FrameWork.Management.Connection.Operator).Nurse.ID);
            if (_alExecBill == null)
            {
                MessageBox.Show("获得执行单设置出错！");
                return;
            }

            for (int i = 0, count = 0; count < _alExecBill.Count && i < this.gbUsage.Controls.Count; i++)
            {
                //除tag为ALL的都隐藏
                if ("ALL".Equals(this.gbUsage.Controls[i].Tag))
                {
                    continue;
                }
                this.gbUsage.Controls[i].Text = ((Neusoft.FrameWork.Models.NeuObject)_alExecBill[count]).Name;
                this.gbUsage.Controls[i].Tag = _alExecBill[count];
                this.gbUsage.Controls[i].Visible = true;
                count++;
            }
            this.cbbPrint.Items.Add(new Neusoft.FrameWork.Models.NeuObject("0", "按执行单汇总打印", ""));
            this.cbbPrint.Items.Add(new Neusoft.FrameWork.Models.NeuObject("1", "按执行单分开患者打印", ""));
            this.cbbPrint.Items.Add(new Neusoft.FrameWork.Models.NeuObject("2", "按患者单独打印", ""));
            this.cbbPrint.SelectedIndex = 0;
        }

        #region 查询

        LocalManager localMgr = new LocalManager();

        /// <summary>
        /// 查询全部患者
        /// </summary>
        /// <param name="list"></param>
        private void Query(List<Neusoft.HISFC.Models.RADT.PatientInfo> patientList)
        {
            this.neuSpread1_Sheet1.RowCount = 0;

            ArrayList alPrintExecBill = new ArrayList();

            string strBillNo = "";
            for (int i = 0; i < this.gbUsage.Controls.Count; i++)
            {
                if (this.gbUsage.Controls[i].Visible == true && !(this.gbUsage.Controls[i].Tag is string))
                {
                    CheckBox temp = this.gbUsage.Controls[i] as CheckBox;
                    if (temp != null && temp.Checked == true)
                    {
                        Neusoft.FrameWork.Models.NeuObject billObj = (Neusoft.FrameWork.Models.NeuObject)gbUsage.Controls[i].Tag;
                        alPrintExecBill.Add(billObj);

                        strBillNo += ",'" + billObj.ID + "'";
                    }
                }
            }

            if (string.IsNullOrEmpty(strBillNo))
            {
                return;
            }
            strBillNo = strBillNo.Substring(1);

            #region 获取住院号

            string strInpatientNo = "";

            foreach (Neusoft.HISFC.Models.RADT.PatientInfo p in patientList)
            {
                strInpatientNo += ",'" + p.ID + "'";
            }

            if (string.IsNullOrEmpty(strInpatientNo))
            {
                return;
            }
            strInpatientNo = strInpatientNo.Substring(1);

            #endregion

            string printType = cbIsPrinted.Checked ? "1" : "0";

            Hashtable hsQuitFeeOrder = new Hashtable();
            Dictionary<string, Neusoft.FrameWork.Models.NeuObject> dicExecBill = new Dictionary<string, Neusoft.FrameWork.Models.NeuObject>();

            ArrayList alExecOrder = localMgr.GetExecBill(strInpatientNo, strBillNo, printType, dtpTime.Value, dtpEnd.Value, ref hsQuitFeeOrder, ref dicExecBill);
            if (alExecOrder == null)
            {
                MessageBox.Show("获取执行单明细出错！\r\n" + localMgr.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            alExecOrder.Sort(new ComparerBySortID());
            ArrayList alShowOrder = new ArrayList();

            //Hashtable hsOrderID = new Hashtable();

            foreach (Neusoft.HISFC.Models.Order.ExecOrder execOrder in alExecOrder)
            {
                if (!execOrder.IsValid)
                {
                    continue;
                }
                if (execOrder.DateUse > execOrder.Order.EndTime
                    && execOrder.Order.EndTime > new DateTime(2000, 1, 1))
                {
                    continue;
                }
                if (hsQuitFeeOrder.Contains(execOrder.ID))
                {
                    continue;
                }

                //if (!hsOrderID.Contains(execOrder.Order.ID))
                //{
                //    hsOrderID.Add(execOrder.Order.ID, null);
                //}
                //else
                //{
                //    continue;
                //}

                if (this.rbtAll.Checked)
                {
                }
                else if (this.rbtLong.Checked)
                {
                    if (!execOrder.Order.OrderType.IsDecompose)
                    {
                        continue;
                    }
                }
                else if (this.rbtShort.Checked)
                {
                    if (execOrder.Order.OrderType.IsDecompose)
                    {
                        continue;
                    }
                }

                if (dicExecBill.ContainsKey(execOrder.ID))
                {
                    execOrder.Memo = dicExecBill[execOrder.ID].ID;
                }

                alShowOrder.Add(execOrder);

                addRow(execOrder.Order.Patient, execOrder);
            }

            //设置组合号
            this.changeCombMark();
        }

        /// <summary>
        /// 查询全部患者
        /// </summary>
        /// <param name="list"></param>
        //private void query(List<Neusoft.HISFC.Models.RADT.PatientInfo> list)
        //{
        //    this.neuSpread1_Sheet1.RowCount = 0;
        //    ArrayList alPrintExecBill = new ArrayList();
        //    for (int i = 0; i < this.gbUsage.Controls.Count; i++)
        //    {
        //        if (this.gbUsage.Controls[i].Visible == true && !(this.gbUsage.Controls[i].Tag is string))
        //        {
        //            CheckBox temp = this.gbUsage.Controls[i] as CheckBox;
        //            if (temp != null && temp.Checked == true)
        //            {
        //                alPrintExecBill.Add(this.gbUsage.Controls[i].Tag);
        //            }
        //        }
        //    }
        //    _dicExecByOrderNO = new Dictionary<string, ArrayList>();
        //    foreach (Neusoft.HISFC.Models.RADT.PatientInfo patient in list)
        //    {
        //        this.query(patient, alPrintExecBill);
        //    }
        //    //设置组合号
        //    this.changeCombMark();
        //}

        /// <summary>
        /// 查询患者
        /// </summary>
        /// <param name="patient"></param>
        private void query(Neusoft.HISFC.Models.RADT.PatientInfo patient, ArrayList alPrintExecBill)
        {
            if (alPrintExecBill == null) return;

            ArrayList alExecOrder = new ArrayList();
            ArrayList alOrderID = new ArrayList();
            ArrayList alRepeatOrder = new ArrayList();
            ArrayList alDiffDeptOrder = new ArrayList();

            foreach (Neusoft.FrameWork.Models.NeuObject obj in alPrintExecBill)
            {
                alExecOrder = orderManager.QueryOrderExecBill(patient.ID, dtpTime.Value, dtpEnd.Value, obj.ID, this.cbIsPrinted.Checked);


                #region 去除非本病区执行,非住院(I)病区(N)除外

                string nurseCellID = patient.PVisit.PatientLocation.NurseCell.ID;
                string DeptId = patient.PVisit.PatientLocation.Dept.ID;

                foreach (Neusoft.HISFC.Models.Order.ExecOrder execOrder in alExecOrder)
                {
                    string deptType = (Neusoft.SOC.HISFC.BizProcess.Cache.Common.GetDept(execOrder.Order.ExeDept.ID)).DeptType.ID.ToString();

                    if (!"I".Equals(deptType) && !"N".Equals(deptType))
                    {
                        continue;
                    }

                    if (!(nurseCellID.Equals(execOrder.Order.NurseStation.ID)))
                    {
                        alDiffDeptOrder.Add(execOrder);
                    }
                }

                foreach (Neusoft.HISFC.Models.Order.ExecOrder execOrder in alDiffDeptOrder)
                {
                    alExecOrder.Remove(execOrder);
                }

                #endregion

                foreach (Neusoft.HISFC.Models.Order.ExecOrder execOrder in alExecOrder)
                {
                    execOrder.Memo = obj.ID;//将执行单号暂时保存

                    if (alOrderID.Contains(execOrder.Order.ID))
                    {
                        alRepeatOrder.Add(execOrder);
                        ArrayList allData = _dicExecByOrderNO[execOrder.Order.ID] as ArrayList;
                        allData.Add(execOrder);
                    }
                    else
                    {
                        ArrayList allData = new ArrayList();
                        allData.Add(execOrder.Clone());
                        alOrderID.Add(execOrder.Order.ID);
                        _dicExecByOrderNO.Add(execOrder.Order.ID, allData);
                    }
                }

                foreach (Neusoft.HISFC.Models.Order.ExecOrder execOrder in alRepeatOrder)
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
            Classes.Function.DrawComboLeft(this.neuSpread1_Sheet1, (int)Columns.组合, (int)Columns.组);
        }

        /// <summary>
        /// 添加数据至表格
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="alExecOrder"></param>
        private void addData(Neusoft.HISFC.Models.RADT.PatientInfo patient, ArrayList alExecOrder)
        {
            alExecOrder.Sort(new Neusoft.SOC.Local.Order.ZhuHai.ZDWY.Common.Comparer.OrderCompareByPatientAndUsage());

            foreach (Neusoft.HISFC.Models.Order.ExecOrder execOrder in alExecOrder)
            {
                execOrder.Order.Patient.PVisit.PatientLocation.Bed.ID = patient.PVisit.PatientLocation.Bed.ID;

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
        private void addRow(Neusoft.HISFC.Models.RADT.PatientInfo patient, Neusoft.HISFC.Models.Order.ExecOrder execOrder)
        {
            int curRow = this.neuSpread1_Sheet1.RowCount++;

            this.neuSpread1_Sheet1.Rows[curRow].Font = new Font("宋体", 10F);
            this.neuSpread1_Sheet1.Rows[curRow].Tag = execOrder;
            this.neuSpread1_Sheet1.Cells[curRow, (int)Columns.姓名].Text = patient.Name;
            this.neuSpread1_Sheet1.Cells[curRow, (int)Columns.姓名].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Cells[curRow, (int)Columns.床号].Text = patient.PVisit.PatientLocation.Bed.ID.Substring(4);
            this.neuSpread1_Sheet1.Cells[curRow, (int)Columns.类型].Text = (execOrder.Order.OrderType.IsDecompose ? "长" : "临") + execOrder.Order.SubCombNO.ToString();
            this.neuSpread1_Sheet1.Cells[curRow, (int)Columns.组合].Text = execOrder.Order.Combo.ID + execOrder.DateUse.ToString();
            this.neuSpread1_Sheet1.Cells[curRow, (int)Columns.组].Tag = execOrder.Order.Combo.ID;
            this.neuSpread1_Sheet1.Cells[curRow, (int)Columns.打印].Text = this.cbIsPrinted.Checked ? "已" : "未";

            //自备、嘱托标记  用于护士打印单据和医嘱单显示区分
            string byoStr = "";

            if (!execOrder.Order.OrderType.IsCharge || execOrder.Order.Item.ID == "999")
            {
                if (execOrder.Order.Item.ItemType == Neusoft.HISFC.Models.Base.EnumItemType.Drug)
                {
                    byoStr = "[自备]";
                }
                else
                {
                    byoStr = "[嘱托]";
                }
            }
            this.neuSpread1_Sheet1.Cells[curRow, (int)Columns.项目名称].Text = byoStr + execOrder.Order.Item.Name + orderManager.TransHypotest(execOrder.Order.HypoTest);

            this.neuSpread1_Sheet1.Cells[curRow, (int)Columns.规格].Text = execOrder.Order.Item.Specs;
            if (execOrder.Order.Item.ItemType == Neusoft.HISFC.Models.Base.EnumItemType.Drug)
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
            this.neuSpread1_Sheet1.Cells[curRow, (int)Columns.频率].Text = execOrder.Order.Frequency.ID;
            this.neuSpread1_Sheet1.Cells[curRow, (int)Columns.执行时间].Text = execOrder.DateUse.ToString("yyyy-MM-dd HH:mm");
            this.neuSpread1_Sheet1.Cells[curRow, (int)Columns.医嘱流水号].Text = execOrder.Order.ID;
            this.neuSpread1_Sheet1.Cells[curRow, (int)Columns.病人号].Text = patient.PID.PatientNO;
            this.neuSpread1_Sheet1.Cells[curRow, (int)Columns.病人号].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
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

        #region 更新打印标志

        private void updatePrintFlag(ArrayList printData)
        {
            Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();
            this.orderManager.SetTrans(Neusoft.FrameWork.Management.PublicTrans.Trans);
            string itemType = string.Empty;
            foreach (Neusoft.HISFC.Models.Order.ExecOrder execOrder in printData)
            {
                if (execOrder.Order.Item.ItemType == Neusoft.HISFC.Models.Base.EnumItemType.Drug)
                {
                    itemType = "1";
                }
                else
                {
                    itemType = "2";
                }
                if (this.orderManager.UpdateExecOrderPrintedByMoOrder(execOrder.Order.ID, dtpTime.Value.ToString(), dtpEnd.Value.ToString(), itemType) == -1)
                {
                    MessageBox.Show("更新打印标记失败!\n" + orderManager.Err);
                    return;
                }
            }
            Neusoft.FrameWork.Management.PublicTrans.Commit();
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
            //try
            //{
            ArrayList needPrintData = new ArrayList();

            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                if ("True".Equals(this.neuSpread1_Sheet1.Cells[i, (int)Columns.是否打印].Text))
                {
                    Neusoft.HISFC.Models.Order.ExecOrder execOrder = this.neuSpread1_Sheet1.Rows[i].Tag as Neusoft.HISFC.Models.Order.ExecOrder;
                    if (execOrder != null)
                    {
                        needPrintData.Add(execOrder);
                    }
                }
            }
            if (needPrintData.Count <= 0) throw new Exception("没有需要打印的数据");

            if (this.cbMemo.Checked)
            {
                _printControl.isMemo = true;
            }

            _printControl.QueryDate = this.dtpTime.Value;
            _printControl.SetExecBill(needPrintData, "", this.PrintModes, _dicExecByOrderNO);                     

            if (!this.cbIsPrinted.Checked)
            {
                updatePrintFlag(needPrintData);
            }

            this.Query(this._patientList);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return -1;
            //}

            return 1;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="alValues"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        protected override int OnSetValues(ArrayList alValues, object e)
        {
            Neusoft.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在查询，请稍后……");
            Application.DoEvents();

            try
            {
                this._patientList.Clear();

                this.clearSheet();

                foreach (Neusoft.HISFC.Models.RADT.PatientInfo p in alValues)
                {
                    this._patientList.Add(p);
                }
                this.Query(this._patientList);
            }
            catch (Exception ex)
            {
                Neusoft.FrameWork.WinForms.Classes.Function.ShowBalloonTip(3, "错误", ex.Message, ToolTipIcon.Error);
            }
            finally
            {
                Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }
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
                    string inPatientNo = this.neuSpread1_Sheet1.Cells[i, (int)Columns.病人号].Text;
                    if (!checkedPatient.Contains(inPatientNo))
                    {
                        checkedPatient.Add(inPatientNo);
                    }
                }
            }
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                string inPatientNo = this.neuSpread1_Sheet1.Cells[i, (int)Columns.病人号].Text;
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
            病人号,
            姓名,
            床号,
            类型,
            组合,
            组,
            打印,
            项目名称,
            规格,
            用量,
            单位,
            用法,
            频率,
            执行时间,
            是否打印
        }

        #endregion
    }

    /// <summary>
    /// 按照床号sortID、住院号、医嘱排序号、执行时间 排序
    /// </summary>
    public class ComparerBySortID : IComparer
    {
        #region IComparer 成员

        public int Compare(object x, object y)
        {
            try
            {
                Neusoft.HISFC.Models.Order.ExecOrder execOrder1 = x as Neusoft.HISFC.Models.Order.ExecOrder;
                Neusoft.HISFC.Models.Order.ExecOrder execOrder2 = y as Neusoft.HISFC.Models.Order.ExecOrder;

                string sort1 = execOrder1.Order.Patient.PVisit.PatientLocation.Bed.ID + execOrder1.Order.Patient.PID.PatientNO + execOrder1.Order.SubCombNO.ToString().PadLeft(5, '0') + execOrder1.DateUse.ToString() + execOrder1.Order.SortID;
                string sort2 = execOrder2.Order.Patient.PVisit.PatientLocation.Bed.ID + execOrder2.Order.Patient.PID.PatientNO + execOrder2.Order.SubCombNO.ToString().PadLeft(5, '0') + execOrder2.DateUse.ToString() + execOrder2.Order.SortID;

                return string.Compare(sort1, sort2);
            }
            catch
            {
                return 0;
            }
        }

        #endregion
    }
}
