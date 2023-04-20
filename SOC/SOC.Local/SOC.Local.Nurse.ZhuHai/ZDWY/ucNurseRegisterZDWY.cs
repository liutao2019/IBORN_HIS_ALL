using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Text.RegularExpressions;

namespace FS.SOC.Local.Nurse.ZhuHai.ZDWY
{
    public partial class ucNurseRegisterZDWY : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        public ucNurseRegisterZDWY()
        {
            InitializeComponent();

            this.timer1.Tick += new EventHandler(timer1_Tick);
            tvInjectPatientList1.AfterSelect += new TreeViewEventHandler(tvInjectPatientList1_AfterSelect);
            this.neuSpread1.ColumnWidthChanged += new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(neuSpread1_ColumnWidthChanged);
        }

        void neuSpread1_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.neuSpread1_Sheet1, this.showXML);
        }

        void tvInjectPatientList1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node != null
                && e.Node.Tag != null)
            {
                if (e.Node.Tag is FS.HISFC.Models.Registration.Register)
                {
                    FS.HISFC.Models.Registration.Register reg = e.Node.Tag as FS.HISFC.Models.Registration.Register;

                    this.txtInput.Text = reg.PID.CardNO;
                    this.txtCardNo_KeyDown(new object(), new KeyEventArgs(Keys.Enter));

                    this.tvInjectPatientList1.Refresh(this.dtpStart.Value, this.dtpEnd.Value);

                }
            }
        }

        /// <summary>
        /// 定时刷新患者列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void timer1_Tick(object sender, EventArgs e)
        {
            this.tvInjectPatientList1.Refresh(this.dtpStart.Value, this.dtpEnd.Value);
        }

        #region IInterfaceContainer 成员

        public Type[] InterfaceTypes
        {
            get
            {
                Type[] types = new Type[1];
                types[0] = typeof(FS.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint);
                return types;
            }
        }

        #endregion

        #region 变量

        /// <summary>
        /// 当前患者挂号信息
        /// </summary>
        private FS.HISFC.Models.Registration.Register myReg = null;

        /// <summary>
        /// 当前患者
        /// </summary>
        private FS.HISFC.Models.RADT.Patient patient = null;

        /// <summary>
        /// 用法常数
        /// </summary>
        private Dictionary<string, FS.HISFC.Models.Base.Const> dicConst = null;

        /// <summary>
        /// 是否显示明细（按照执行时间点显示）
        /// </summary>
        private bool isShowDetail = false;

        /// <summary>
        /// 是否显示明细（按照执行时间点显示）
        /// </summary>
        [Description("是否显示明细（按照执行时间点显示）"), Category("设置")]
        public bool IsShowDetail
        {
            get
            {
                return isShowDetail;
            }
            set
            {
                isShowDetail = value;
            }
        }

        private string showXML = FS.FrameWork.WinForms.Classes.Function.CurrentPath + "Profile\\InjectShowSetting.xml";

        #region 列设置

        enum EnumColSet
        {
            选择,
            处方号,
            院注天数,
            医嘱,
            组合,
            每次量,
            频次,
            用法,

            //用药时间,

            开方医生,
            开方科室,
            备注
                //,

            //组合号
        }
        #endregion

        /// <summary>
        /// 院注管理类
        /// </summary>
        private FS.HISFC.BizLogic.Nurse.Inject injectManager = new FS.HISFC.BizLogic.Nurse.Inject();

        /// <summary>
        /// 瓶签打印接口
        /// </summary>
        private SOC.HISFC.BizProcess.NurseInterface.OutPatient.IInjectBottleLabel IInjectBottleLabel = null;

        /// <summary>
        /// 输液卡打印接口
        /// </summary>
        private SOC.HISFC.BizProcess.NurseInterface.OutPatient.IInjectInfusionCardPrint IInjectInfusionCardPrint = null;
        /// <summary>
        /// 注射单打印接口
        /// </summary>
        private SOC.HISFC.BizProcess.NurseInterface.OutPatient.IInjectReceiptPrint IInjectReceiptPrint = null;
        /// <summary>
        /// 门诊费用管理类
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();

        /// <summary>
        /// 挂号管理
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Registration.Registration registerManager = new FS.HISFC.BizProcess.Integrate.Registration.Registration();

        /// <summary>
        /// 综合业务层
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager inteManager = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// 医嘱函数
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Order orderManager = new FS.HISFC.BizProcess.Integrate.Order();

        /// <summary>
        /// 工具栏
        /// </summary>
        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        #endregion

        #region 属性

        /// <summary>
        /// 设置显示在界面上的用法类别（取用法维护的系统类别）
        /// </summary>
        private string showUsage = "";

        /// <summary>
        /// 设置显示在界面上的用法类别（取用法维护的系统类别）
        /// </summary>
        [Description("设置显示在界面上的用法类别（取用法维护的系统类别）"), Category("设置")]
        public string ShowUsage
        {
            get
            {
                return this.showUsage;
            }
            set
            {
                this.showUsage = value;
            }
        }

        /// <summary>
        /// 设置打印的用法类别（取用法维护的系统类别），中五要求：只要是有要打印的用法，则所有显示的项目都打印出来
        /// </summary>
        private string printUsage = "IVD;";

        /// <summary>
        /// 设置打印的用法类别（取用法维护的系统类别），中五要求：只要是有要打印的用法，则所有显示的项目都打印出来
        /// </summary>
        [Description("设置打印的用法类别（取用法维护的系统类别），中五要求：只要是有要打印的用法，则所有显示的项目都打印出来"), Category("设置")]
        public string PrintUsage
        {
            get
            {
                return printUsage;
            }
            set
            {
                printUsage = value;
            }
        }

        /// <summary>
        /// 回车是否自动打印并确认
        /// </summary>
        private bool enterConfirm = true;

        /// <summary>
        /// 回车是否自动打印并确认
        /// </summary>
        [Description("回车是否自动打印并确认"), Category("设置")]
        public bool EnterConfirm
        {
            get
            {
                return this.enterConfirm;
            }
            set
            {
                this.enterConfirm = value;
            }
        }

        /// <summary>
        /// 查询的日期间隔
        /// </summary>
        private int dayInterval = 0;

        /// <summary>
        /// 查询的日期间隔
        /// </summary>
        [Description("查询的日期间隔"), Category("设置")]
        public int DayInterval
        {
            get
            {
                return dayInterval;
            }
            set
            {
                dayInterval = value;
            }
        }

        #endregion

        #region 初始化

        /// <summary>
        /// 清空
        /// </summary>
        private void Clear()
        {
            this.dtpStart.Value = injectManager.GetDateTimeFromSysDateTime().Date.AddDays(0 - this.dayInterval);

            this.dtpEnd.Value = injectManager.GetDateTimeFromSysDateTime().Date.AddDays(1).AddSeconds(-1);

            this.txtInput.Clear();
            this.txtCard.Clear();
            this.txtName.Clear();
            this.txtAge.Clear();
            this.txtSex.Clear();
            this.txtDiagnoses.Clear();
            this.ClearSheet();
            this.SetSheetStyle();
        }

        #endregion

        #region 显示转换方法

        /// <summary>
        /// 计算院注天数
        /// </summary>
        /// <param name="itemInfo"></param>
        /// <returns></returns>
        private string getInjectDay(FS.HISFC.Models.Fee.Outpatient.FeeItemList itemInfo, bool isReprint)
        {
            string result = string.Empty;

            int totalInject = (int)itemInfo.InjectCount;                    //总注射次数
            //int injectCountPerDay = itemInfo.Order.Frequency.Times.Length;  //每天院注次数
            int injectCountPerDay = FS.FrameWork.Function.NConvert.ToInt32(SOC.HISFC.BizProcess.Cache.Order.GetFrequencyCount(itemInfo.Order.Frequency.ID));//每天院注次数
            int confirmedCount = (int)itemInfo.ConfirmedInjectCount;        //已经确认院注数
            int totalDay = (int)Math.Ceiling((decimal)totalInject / injectCountPerDay);          //总确认天数
            int confirmedDay = (int)Math.Ceiling((decimal)confirmedCount / injectCountPerDay);   //已经确认天数
            bool isConfirmedAll = true;
            
            if (isReprint)
            {
                isConfirmedAll = false;
                if (confirmedDay == 0)
                {
                    confirmedDay = 1;
                }
            }
            else if (confirmedDay < totalDay)
            {
                //未注射完的情况
                confirmedDay += 1;
                isConfirmedAll = false;
            }
            result += "第" + confirmedDay.ToString() + "天/共" + totalDay.ToString() + "天";
            if (isConfirmedAll)
            {
                result += "(*)";
            }
            return result;
        }

        #endregion

        #region 赋值

        private void Add(ArrayList itemList)
        {
            this.ClearSheet();
            foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList detail in itemList)
            {
                //#region 含有带*号的不显示
                //if (detail.ToString().Contains("(*)"))
                //{
                //    return;
                //}
                //#endregion
                #region 院注次数与已确认次数不显示
                if (detail.InjectCount == detail.ConfirmedInjectCount)
                {
                    continue;
                }
                #endregion
                #region 非药品不显示
                if (detail.Item.ItemType != FS.HISFC.Models.Base.EnumItemType.Drug)
                {
                    continue;
                }
                #endregion

                #region 设置不显示的用法

                if (!string.IsNullOrEmpty(showUsage)
                    && !showUsage.Contains(dicConst[detail.Order.Usage.ID].UserCode))
                {
                    continue;
                }

                #endregion

                #region 无院注不显示
                if (detail.InjectCount == 0)
                {
                    continue;
                }
                #endregion

                #region 是否显示0次数的
                if (!chkIsNullNum.Checked
                    && detail.ConfirmedQty == 0)
                {
                    continue;
                }
                #endregion

                #region 是否显示已经注射完
                if (!chFinish.Checked 
                    && detail.ConfirmedInjectCount == detail.InjectCount)
                {
                    continue;
                }
                #endregion

                this.AddDetail(detail);
            }
            this.SetComb();
        }

        /// <summary>
        /// 增加明细
        /// </summary>
        /// <param name="itemInfo"></param>
        private void AddDetail(FS.HISFC.Models.Fee.Outpatient.FeeItemList itemInfo)
        {
            if (itemInfo.ConfirmedInjectCount < itemInfo.InjectCount)
            {
                this.neuSpread1_Sheet1.RowCount++;
                int curRow = this.neuSpread1_Sheet1.RowCount - 1;
                this.neuSpread1_Sheet1.Rows[curRow].Tag = itemInfo;

                this.neuSpread1_Sheet1.Cells[curRow, (int)EnumColSet.选择].CellType = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
                this.neuSpread1_Sheet1.Cells[curRow, (int)EnumColSet.处方号].Text = itemInfo.RecipeNO;
                this.neuSpread1_Sheet1.Cells[curRow, (int)EnumColSet.院注天数].Text = this.getInjectDay(itemInfo, false);
                this.neuSpread1_Sheet1.Cells[curRow, (int)EnumColSet.医嘱].Text = itemInfo.Item.Name + "[" + itemInfo.Item.Specs + "]";
                this.neuSpread1_Sheet1.Cells[curRow, (int)EnumColSet.组合].Tag = itemInfo.Order.Combo.ID;

                this.neuSpread1_Sheet1.Cells[curRow, (int)EnumColSet.每次量].Text = itemInfo.Order.DoseOnce.ToString() + itemInfo.Order.DoseUnit;
                this.neuSpread1_Sheet1.Cells[curRow, (int)EnumColSet.频次].Text = itemInfo.Order.Frequency.ID;
                this.neuSpread1_Sheet1.Cells[curRow, (int)EnumColSet.用法].Text = itemInfo.Order.Usage.Name;
                this.neuSpread1_Sheet1.Cells[curRow, (int)EnumColSet.开方医生].Text = SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(itemInfo.RecipeOper.ID);
                this.neuSpread1_Sheet1.Cells[curRow, (int)EnumColSet.开方科室].Text = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(itemInfo.RecipeOper.Dept.ID);

                this.neuSpread1_Sheet1.Cells[curRow, (int)EnumColSet.备注].Text = itemInfo.Item.Memo;


                //this.neuSpread1_Sheet1.Cells[curRow, (int)EnumColSet.用药时间].Text = "";

                if (this.isShowDetail)
                {
                    //this.neuSpread1_Sheet1.Cells[curRow, (int)EnumColSet.组合号].Text = itemInfo.Order.Combo.ID;
                }
                else
                {
                    //this.neuSpread1_Sheet1.Cells[curRow, (int)EnumColSet.组合号].Text = itemInfo.Order.Combo.ID;
                }

                neuSpread1_Sheet1.Rows[curRow].Tag = itemInfo;
            }
        }

        /// <summary>
        /// 设置表格字体
        /// </summary>
        private void SetCellsFont()
        {
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                for (int j = 0; j < this.neuSpread1_Sheet1.ColumnCount; j++)
                {
                    this.neuSpread1_Sheet1.Cells[i, j].Font = new Font("宋体", 10F);
                }
            }
        }

        /// <summary>
        /// 设置组合号
        /// </summary>
        private void SetComb()
        {
            int totalCount = this.neuSpread1_Sheet1.RowCount;
            if (totalCount == 0)
            {
                return;
            }
            int i;
            //第一行
            this.neuSpread1_Sheet1.Cells[0, (int)EnumColSet.组合].Text = "┓";
            //最后行
            this.neuSpread1_Sheet1.Cells[totalCount - 1, (int)EnumColSet.组合].Text = "┛";
            //中间行
            for (i = 1; i < totalCount - 1; i++)
            {
                int prior = i - 1;
                int next = i + 1;
                string currentRowCombNo = this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.组合].Tag.ToString();
                string priorRowCombNo = this.neuSpread1_Sheet1.Cells[prior, (int)EnumColSet.组合].Tag.ToString();
                string nextRowCombNo = this.neuSpread1_Sheet1.Cells[next, (int)EnumColSet.组合].Tag.ToString();

                #region
                bool bl1 = true;
                bool bl2 = true;
                if (currentRowCombNo != priorRowCombNo)
                    bl1 = false;
                if (currentRowCombNo != nextRowCombNo)
                    bl2 = false;
                //  ┃
                if (bl1 && bl2)
                {
                    this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.组合].Text = "┃";
                }
                //  ┛
                if (bl1 && !bl2)
                {
                    this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.组合].Text = "┛";
                }
                //  ┓
                if (!bl1 && bl2)
                {
                    this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.组合].Text = "┓";
                }
                //  ""
                if (!bl1 && !bl2)
                {
                    this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.组合].Text = "";
                }
                #endregion
            }
            //把没有组号的去掉
            for (i = 0; i < totalCount; i++)
            {
                if (this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.组合].Tag.ToString() == "")
                {
                    this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.组合].Text = "";
                }
            }
            //判断首末行 有组号，且只有自己一组数据的情况
            if (totalCount == 1)
            {
                this.neuSpread1_Sheet1.Cells[0, (int)EnumColSet.组合].Text = "";
            }
            //只有首末两行，那么还要判断组号啊
            if (totalCount == 2)
            {
                if (this.neuSpread1_Sheet1.Cells[0, (int)EnumColSet.组合].Tag.ToString().Trim() != this.neuSpread1_Sheet1.Cells[1, (int)EnumColSet.组合].Tag.ToString().Trim())
                {
                    this.neuSpread1_Sheet1.Cells[0, (int)EnumColSet.组合].Text = "";
                    this.neuSpread1_Sheet1.Cells[1, (int)EnumColSet.组合].Text = "";
                }
            }
            if (totalCount > 2)
            {
                if (this.neuSpread1_Sheet1.Cells[1, (int)EnumColSet.组合].Text != "┃"
                    && this.neuSpread1_Sheet1.Cells[1, (int)EnumColSet.组合].Text != "┛")
                {
                    this.neuSpread1_Sheet1.Cells[0, (int)EnumColSet.组合].Text = "";
                }
                if (this.neuSpread1_Sheet1.Cells[totalCount - 2, (int)EnumColSet.组合].Text != "┃"
                    && this.neuSpread1_Sheet1.Cells[totalCount - 2, (int)EnumColSet.组合].Text != "┓")
                {
                    this.neuSpread1_Sheet1.Cells[totalCount - 1, (int)EnumColSet.组合].Text = "";
                }
            }
        }

        /// <summary>
        /// 全选
        /// </summary>
        private void SelectAll(bool isSelected)
        {
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                if (!this.neuSpread1_Sheet1.Rows[i].Locked)
                {
                    this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.选择].Value = isSelected;
                }
            }
        }

        /// <summary>
        /// 患者信息赋值
        /// </summary>
        private void SetPatient()
        {
            this.txtCard.Text = patient.PID.CardNO;
            this.txtName.Text = patient.Name;
            this.txtSex.Text = patient.Sex.Name;
            this.txtAge.Text = this.injectManager.GetAge(patient.Birthday);
            //诊断暂时不显示
        }

        private void setPatient(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            this.patient = patient;
            if (this.patient == null || string.IsNullOrEmpty(this.patient.PID.CardNO))
            {
                return;
            }
            else
            {
                this.QueryInjectItem();
            }

        }

        #endregion

        #region 表格相关
        /// <summary>
        /// 清空表格
        /// </summary>
        private void ClearSheet()
        {
            this.neuSpread1_Sheet1.RowCount = 0;
        }

        /// <summary>
        /// 设置表格样式
        /// </summary>
        private void SetSheetStyle()
        {
            FarPoint.Win.Spread.CellType.TextCellType txtType = new FarPoint.Win.Spread.CellType.TextCellType();
            txtType.ReadOnly = true;

            FarPoint.Win.Spread.CellType.NumberCellType numType = new FarPoint.Win.Spread.CellType.NumberCellType();
            numType.DecimalPlaces = 0;
            numType.ReadOnly = true;

            this.neuSpread1_Sheet1.Columns[(int)EnumColSet.处方号].CellType = numType;
            this.neuSpread1_Sheet1.Columns[(int)EnumColSet.院注天数].CellType = txtType;
            this.neuSpread1_Sheet1.Columns[(int)EnumColSet.医嘱].CellType = txtType;
            this.neuSpread1_Sheet1.Columns[(int)EnumColSet.组合].CellType = txtType;
            this.neuSpread1_Sheet1.Columns[(int)EnumColSet.每次量].CellType = txtType;
            this.neuSpread1_Sheet1.Columns[(int)EnumColSet.频次].CellType = txtType;
            this.neuSpread1_Sheet1.Columns[(int)EnumColSet.用法].CellType = txtType;
            this.neuSpread1_Sheet1.Columns[(int)EnumColSet.开方医生].CellType = txtType;
            this.neuSpread1_Sheet1.Columns[(int)EnumColSet.开方科室].CellType = txtType;
            this.neuSpread1_Sheet1.Columns[(int)EnumColSet.备注].CellType = txtType;

            this.neuSpread1_Sheet1.Columns[(int)EnumColSet.处方号].Visible = false;
            //this.neuSpread1_Sheet1.Columns[(int)EnumColSet.组合号].Visible = false;
        }

        /// <summary>
        /// 选择组合
        /// </summary>
        private void SelectedComb(int row, bool isSelected)
        {
            string combID = this.neuSpread1_Sheet1.Cells[row, (int)EnumColSet.组合].Tag.ToString();
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                if (this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.组合].Tag.ToString() == combID)
                {
                    this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.选择].Value = isSelected;
                }
            }
        }
        #endregion

        #region 确认

        private void ConfirmALL()
        {
            if (this.neuSpread1_Sheet1.RowCount <= 0)
            {
                MessageBox.Show("没有要保存的数据", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            this.neuSpread1.StopCellEditing();

            //if (alInjectPrintData.ToString().Contains("(*)"))
            //{
            //    //MessageBox.Show("存在已经注射完的项目", "提示");
            //    //return;
            //}
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            try
            {
                this.injectManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                this.feeIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                this.inteManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                DateTime confirmDate = this.injectManager.GetDateTimeFromSysDateTime();

                FS.HISFC.Models.Fee.Outpatient.FeeItemList detail = null;
                FS.HISFC.Models.Nurse.Inject info = null;

                for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
                {
                    if (this.neuSpread1_Sheet1.GetText(i, 0).ToUpper() == "FALSE" ||
                        this.neuSpread1_Sheet1.GetText(i, 0) == "")
                    {
                        continue;
                    }
                    detail = this.neuSpread1_Sheet1.Rows[i].Tag as FS.HISFC.Models.Fee.Outpatient.FeeItemList;
                    info = new FS.HISFC.Models.Nurse.Inject();

                    #region 第1次注射记录met_nuo_inject,否则不记录

                    if (detail.ConfirmedInjectCount == 0)
                    {
                        #region 实体转化（门诊项目收费明细实体FeeItemList－->注射实体Inject）

                        info.Patient = myReg;
                        info.Patient.ID = detail.Patient.ID;
                        info.Patient.Name = myReg.Name;
                        info.Patient.Sex.ID = myReg.Sex.ID;
                        info.Patient.Birthday = myReg.Birthday;
                        info.Patient.PID.CardNO = myReg.PID.CardNO;

                        info.Item = detail;
                        info.Item.ID = detail.Item.ID;
                        info.Item.Name = detail.Item.Name;

                        info.Item.InjectCount = detail.InjectCount;

                        info.Item.Order.DoctorDept.Name = SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(detail.RecipeOper.Dept.ID);
                        info.Item.Order.DoctorDept.ID = detail.RecipeOper.Dept.ID;

                        info.Item.Order.ReciptDoctor.Name = SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(detail.RecipeOper.ID);
                        info.Item.Order.ReciptDoctor.ID = detail.RecipeOper.ID;
                        #endregion

                        info.ID = this.injectManager.GetSequence("Nurse.Inject.GetSeq");
                        info.PrintNo = detail.User02;
                        info.Item.Order.Combo.ID = this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.组合].Tag.ToString();
                        info.Booker.ID = FS.FrameWork.Management.Connection.Operator.ID;
                        info.Booker.OperTime = confirmDate;
                        info.Item.ExecOper.ID = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID;

                        string strOrder = "";
                        info.InjectOrder = strOrder;
                        info.Item.Days = detail.Days;

                        #region 向met_nuo_inject中，插入记录
                        if (this.injectManager.Insert(info) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(this.injectManager.Err, "提示");
                            return;
                        }
                        #endregion

                    }
                    #endregion

                    #region 向fin_ipb_feeitemlist中，插入数量

                    //更新的院注次数
                    int updateInjectCount = 1;

                    //updateInjectCount = detail.Order.Frequency.Times.Length;
                    updateInjectCount = FS.FrameWork.Function.NConvert.ToInt32(SOC.HISFC.BizProcess.Cache.Order.GetFrequencyCount(detail.Order.Frequency.ID));//每天院注次数

                    if (this.feeIntegrate.UpdateConfirmInject(detail.Order.ID, detail.RecipeNO, detail.SequenceNO.ToString(), updateInjectCount) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(this.feeIntegrate.Err, "提示");
                        return;
                    }
                    #endregion
                    FS.FrameWork.Management.PublicTrans.Commit();
                }
                MessageBox.Show("成功确认");
                this.Clear();
            }
            catch (Exception e)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(e.Message, "提示");
                return;
            }
        }

        #endregion

        #region 查询

        /// <summary>
        /// 查询注射项目
        /// </summary>
        private int QueryInjectItem()
        {
            string cardNo = patient.PID.CardNO;
            ArrayList itemList = this.feeIntegrate.QueryOutpatientFeeItemListsZs(cardNo, this.dtpStart.Value, this.dtpEnd.Value);
            if (itemList == null)
            {
                MessageBox.Show("该患者没有需要确认的医嘱信息!", "提示");
                this.txtInput.SelectAll();
                return 0;
            }
            this.Add(itemList);
            this.SetCellsFont();
            this.SelectAll(true);

            return 1;
        }

        /// <summary>
        /// 回车专用查询病人,去除注射界面查询没有卡号为XXXX的患者后还会提示是否打印的bug
        /// </summary>
        private int QyeryPatient()
        {
            #region 查找患者

            string cardNo = this.txtInput.Text;

            //若全为数字,则按照旧逻辑
            //否则可以根据姓名查询
            Regex regex = new Regex("^\\d+$");
            if (!regex.IsMatch(cardNo))
            {
                if (string.IsNullOrEmpty(cardNo))
                {
                    cardNo = "%";
                }
                int result = this.QueryPatientWithInjectItemByNameAndDate(this.txtInput.Text, this.dtpStart.Value, this.dtpEnd.Value);
                if (result != 1 || this.patient == null)
                {
                    MessageBox.Show("该患者没有需要确认的医嘱信息!", "提示");
                    this.txtInput.SelectAll();
                    return -1;
                }
                this.txtInput.Text = this.patient.PID.CardNO;
                this.QyeryPatient();
            }
            else
            {
                FS.HISFC.Models.Account.AccountCard tempCard = new FS.HISFC.Models.Account.AccountCard();
                int result = this.feeIntegrate.ValidMarkNO(cardNo, ref tempCard);
                if (result <= 0 || tempCard == null)
                {
                    MessageBox.Show("无效卡号，请联系管理员！");
                    this.txtInput.SelectAll();
                    return -1;
                }
                cardNo = tempCard.Patient.PID.CardNO;//重新获取卡号
                ArrayList regs = this.registerManager.Query(cardNo, this.dtpStart.Value.AddDays(-1));
                if (regs == null || regs.Count == 0)
                {
                    MessageBox.Show("没有病历号为:" + cardNo + "的患者!", "提示");
                    this.txtInput.SelectAll();
                    return -1;
                }
                this.patient = regs[0] as FS.HISFC.Models.RADT.Patient;
                this.myReg = regs[0] as FS.HISFC.Models.Registration.Register;

                if (patient == null || patient.ID == "")
                {
                    MessageBox.Show("没有病历号为:" + cardNo + "的患者!", "提示");

                    this.txtInput.SelectAll();
                }
            }
            #endregion

            this.SetPatient();
            return this.QueryInjectItem();
        }


        private int QueryPatientWithInjectItemByNameAndDate(string name, DateTime start, DateTime end)
        {
            int result = 0;
            frmQueryPatientByName f = new frmQueryPatientByName();
            f.SelectedPatient += new frmQueryPatientByName.GetPatient(setPatient);
            if (f.QueryByNameAndDate(name, start, end, "Inject") > 0)
            {
                DialogResult dr = f.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    result = 1;
                }
                f.Dispose();//释放资源
            }
            return result;
        }

        #endregion

        #region 事件
        /// <summary>
        /// 窗体加载初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucNurseRegisterZDWY_Load(object sender, EventArgs e)
        {
            this.Clear();

            this.tvInjectPatientList1.Refresh(this.dtpStart.Value, this.dtpEnd.Value);

            this.timer1.Interval = 10000;
            timer1.Enabled = true;
            timer1.Start();

            if (System.IO.File.Exists(showXML))
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.neuSpread1_Sheet1, showXML);
            }

            dicConst = new Dictionary<string, FS.HISFC.Models.Base.Const>();
            ArrayList alCon = this.inteManager.GetConstantList("USAGE");
            foreach (FS.HISFC.Models.Base.Const con in alCon)
            {
                if (!dicConst.ContainsKey(con.ID))
                {
                    dicConst.Add(con.ID, con);
                }
            }
        }

        /// <summary>
        /// 是否需要打印
        /// </summary>
        /// <returns></returns>
        private bool IsPrint(ref ArrayList alPrintData)
        {
            alPrintData = new ArrayList();

            if (GetPrintData(ref alPrintData) != 1)
            {
                return false;
            }

            if (alPrintData == null
                || alPrintData.Count == 0)
            {
                return false;
            }
            foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItemList in alPrintData)
            {
                if (string.IsNullOrEmpty(printUsage)
                    || printUsage.Contains(dicConst[feeItemList.Order.Usage.ID].UserCode))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 卡号回车激活查询患者
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCardNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (QyeryPatient() != 1)
                {
                    return;
                }

                if (enterConfirm)
                {
                    ArrayList alPrintData = new ArrayList();

                    //判断是否需要打印
                    if (!IsPrint(ref alPrintData))
                    {
                        MessageBox.Show("没有需要打印的数据！\r\n\r\n如有疑问请联系管理员！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    #region 回车打印输液卡、瓶签


                    //还需完善：如果患者先开立一些药品打印了注射单，重新开立后再打印也会提示这个东东
                    string maxDate = injectManager.ExecSqlReturnOne("select max(REGISTER_DATE) FROM met_nuo_inject where CARD_NO='" + this.patient.PID.CardNO + "'");

                    if (!string.IsNullOrEmpty(maxDate) && (maxDate != "-1"))
                    {
                        DateTime printTime = Convert.ToDateTime(maxDate);

                        DateTime dtNow = injectManager.GetDateTimeFromSysDateTime();
                        if (printTime.Date == dtNow.Date)
                        {
                            if (MessageBox.Show("今天已经打印过，是否继续打印？\r\n\r\n如需补打，请到补打界面！", "询问", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) != DialogResult.Yes)
                            {
                                return;
                            }
                        }
                    }


                    bool isPrinted = false;

                    if (MessageBox.Show("是否打印输液卡", "询问", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        this.PrintInfusionCard(alPrintData);
                        isPrinted = true;
                    }
                    if (MessageBox.Show("是否打瓶签", "询问", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        this.PrintBottleLabel(alPrintData);
                        isPrinted = true;
                    }

                    if (isPrinted)
                    {
                        this.ConfirmALL();
                    }
                    #endregion

                    this.txtInput.SelectAll();
                }
            }
        }

        /// <summary>
        /// 表格点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuSpread1_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            int row = this.neuSpread1_Sheet1.ActiveRowIndex;
            bool isSelect = Convert.ToBoolean(this.neuSpread1_Sheet1.Cells[row, (int)EnumColSet.选择].Value);
            this.SelectedComb(row, isSelect);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnQuery(object sender, object neuObject)
        {
            this.QyeryPatient();
            return 1;
        }
        #endregion

        #region 工具栏

        /// <summary>
        /// 定义工具栏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.toolBarService.AddToolButton("确认", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.B保存, true, false, null);
            this.toolBarService.AddToolButton("全选", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q全选, true, false, null);
            this.toolBarService.AddToolButton("取消全选", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q取消, true, false, null);
            this.toolBarService.AddToolButton("打印瓶签", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D打印, true, false, null);
            this.toolBarService.AddToolButton("打印输液卡", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D打印, true, false, null);
            this.toolBarService.AddToolButton("打印注射单", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D打印, true, false, null);
            return this.toolBarService;
        }

        /// <summary>
        /// 菜单事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text.Trim())
            {
                case "确认":
                    this.ConfirmALL();
                    break;
                case "全选":
                    this.SelectAll(true);
                    break;
                case "取消全选":
                    this.SelectAll(false);
                    break;
                case "打印瓶签":
                    this.PrintBottleLabel();
                    break;
                case "打印输液卡":
                    this.PrintInfusionCard();
                    break;
                case "打印注射单":
                    this.PrintInjectReceipt();
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region 打印

        /// <summary>
        /// 打印输液卡
        /// </summary>
        private void PrintInfusionCard()
        {
            ArrayList alPrintData = new ArrayList();

            //判断是否需要打印
            if (!IsPrint(ref alPrintData))
            {
                MessageBox.Show("没有需要打印的数据！\r\n\r\n如有疑问请联系管理员！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            PrintInfusionCard(alPrintData);
        }

        /// <summary>
        /// 打印输液卡
        /// </summary>
        private void PrintInfusionCard(ArrayList alPrintData)
        {
            if (IInjectInfusionCardPrint == null)
            {
                IInjectInfusionCardPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.SOC.HISFC.BizProcess.NurseInterface.OutPatient.IInjectInfusionCardPrint)) as FS.SOC.HISFC.BizProcess.NurseInterface.OutPatient.IInjectInfusionCardPrint;
            }
            if (IInjectInfusionCardPrint == null)
            {
                //MessageBox.Show("打印接口未实现，无法打印，请联系管理员!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                //return;

                ZDWY.IInjectInfusionCardPrint.ucInfusionCardPrint ucInfusionCardPrint = new FS.SOC.Local.Nurse.ZhuHai.ZDWY.IInjectInfusionCardPrint.ucInfusionCardPrint();

                ucInfusionCardPrint.RegInfo = myReg;
                ucInfusionCardPrint.Print(alPrintData);
            }
            else
            {
                IInjectInfusionCardPrint.RegInfo = this.myReg;
                IInjectInfusionCardPrint.Print(alPrintData);
            }
        }

        /// <summary>
        /// 打印注射单
        /// </summary>
        private void PrintInjectReceipt()
        {
            ArrayList alPrintData = new ArrayList();

            //判断是否需要打印
            if (!IsPrint(ref alPrintData))
            {
                MessageBox.Show("没有需要打印的数据！\r\n\r\n如有疑问请联系管理员！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            PrintInjectReceipt(alPrintData);
        }

        /// <summary>
        /// 打印注射单
        /// </summary>
        private void PrintInjectReceipt(ArrayList alPrintData)
        {
            if (IInjectReceiptPrint == null)
            {
                IInjectReceiptPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.SOC.HISFC.BizProcess.NurseInterface.OutPatient.IInjectReceiptPrint)) as FS.SOC.HISFC.BizProcess.NurseInterface.OutPatient.IInjectReceiptPrint;
            }
            if (IInjectReceiptPrint == null)
            {
                MessageBox.Show("打印接口未实现，无法打印，请联系管理员!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            IInjectReceiptPrint.RegInfo = this.myReg;
            IInjectReceiptPrint.Print(alPrintData);
        }

        private int GetPrintData(ref ArrayList alPrintData)
        {
            int rowCount = this.neuSpread1_Sheet1.RowCount;
            bool isSelect = false;
            
            alPrintData = new ArrayList();
            for (int i = 0; i < rowCount; i++)
            {
                isSelect = Convert.ToBoolean(this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.选择].Value);
                if (isSelect)
                {
                    alPrintData.Add(this.neuSpread1_Sheet1.Rows[i].Tag);
                }
            }

            if (alPrintData == null || alPrintData.Count == 0)
            {
                MessageBox.Show("请选择打印的数据!");
                return 0;
            }
            return 1;
        }

        /// <summary>
        /// 打印瓶签
        /// </summary>
        private void PrintBottleLabel()
        {
            ArrayList alPrintData = new ArrayList();

            //判断是否需要打印
            if (!IsPrint(ref alPrintData))
            {
                MessageBox.Show("没有需要打印的数据！\r\n\r\n如有疑问请联系管理员！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            PrintBottleLabel(alPrintData);
        }

        /// <summary>
        /// 打印瓶签
        /// </summary>
        private int PrintBottleLabel(ArrayList alPrintData)
        {
            if (IInjectBottleLabel == null)
            {
                IInjectBottleLabel = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.SOC.HISFC.BizProcess.NurseInterface.OutPatient.IInjectBottleLabel)) as FS.SOC.HISFC.BizProcess.NurseInterface.OutPatient.IInjectBottleLabel;
            }
            if (IInjectBottleLabel == null)
            {
                ZDWY.IInjectBottleLabel.PrintBottleLabel PrintBottleLabel = new FS.SOC.Local.Nurse.ZhuHai.ZDWY.IInjectBottleLabel.PrintBottleLabel();

                PrintBottleLabel.RegInfo = myReg;
                return PrintBottleLabel.Print(alPrintData);

                //MessageBox.Show("打印接口未实现，无法打印，请联系管理员!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                //return;
            }
            else
            {
                IInjectBottleLabel.RegInfo = this.myReg;
                return IInjectBottleLabel.Print(alPrintData);
            }
        }
        #endregion
    }
}
