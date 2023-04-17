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

namespace FS.SOC.Local.Nurse.GuangZhou.Gyzl
{
    public partial class ucNurseRegisterGyzl : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        public ucNurseRegisterGyzl()
        {
            InitializeComponent();
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
        /// 当前患者
        /// </summary>
        private FS.HISFC.Models.RADT.Patient patient = null;

        /// <summary>
        /// 当前患者挂号信息
        /// </summary>
        private FS.HISFC.Models.Registration.Register reg = null;

        /// <summary>
        /// 打印注射单数据
        /// </summary>
        private StringBuilder alInjectPrintData = new StringBuilder();

        /// <summary>
        /// 打印临时数据
        /// </summary>
        private string tempDate = string.Empty;

        /// <summary>
        /// 打印数据
        /// </summary>
        private ArrayList printData = new ArrayList();

        /// <summary>
        /// 注射单数据
        /// </summary>
        private ArrayList injectionInfo = new ArrayList();

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
            开方医生,
            开方科室,
            备注
        }
        #endregion

        /// <summary>
        /// 医生帮助类
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper doctHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 科室帮助类
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper deptHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 频次帮助类
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper freqHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 院注管理类
        /// </summary>
        private FS.HISFC.BizLogic.Nurse.Inject injectManager = new FS.HISFC.BizLogic.Nurse.Inject();

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

        private string showUsage = "iv.drip(门）;";

        [Description("打印在巡视卡上用法(区分注射用法、输液用法)"), Category("设置")]
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

        private bool showTable = false;
        [Description("打印是否显示表格"), Category("设置")]
        public bool ShowTable
        {
            get
            {
                return this.showTable;
            }
            set
            {
                this.showTable = value;
            }
        }
        #endregion

        #region 初始化

        private void init()
        {
            this.initData();
            this.initControl();
        }

        /// <summary>
        /// 初始化控件
        /// </summary>
        private void initControl()
        {
            this.dtpStart.Value = injectManager.GetDateTimeFromSysDateTime().Date.AddDays(0);
            this.dtpEnd.Value = injectManager.GetDateTimeFromSysDateTime().Date.AddDays(1).AddSeconds(-1);
            this.txtInput.Clear();
            this.txtCard.Clear();
            this.txtName.Clear();
            this.txtAge.Clear();
            this.txtSex.Clear();
            this.txtDiagnoses.Clear();
            this.clearSheet();
            this.setSheetStyle();
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        private void initData()
        {
            ArrayList al = this.inteManager.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.D);
            if (al == null)
            {
                MessageBox.Show("查询医生列表出错！\r\n" + inteManager.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.doctHelper.ArrayObject = al;

            al = this.inteManager.GetDepartment();
            if (al == null)
            {
                MessageBox.Show("查询科室列表出错！\r\n" + inteManager.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.deptHelper.ArrayObject = al;

            al = this.inteManager.QuereyFrequencyList();
            if (al == null)
            {
                MessageBox.Show("查询频次列表出错！\r\n" + inteManager.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.freqHelper.ArrayObject = al;
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
            int injectCountPerDay = itemInfo.Order.Frequency.Times.Length;  //每天院注次数
            int confirmedCount = (int)itemInfo.ConfirmedInjectCount;        //已经确认院注数
            int totalDay = (int)Math.Ceiling((decimal)totalInject / injectCountPerDay);          //总确认天数
            int confirmedDay = (int)(confirmedCount / injectCountPerDay);   //已经确认天数
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

        private void add(ArrayList itemList)
        {
            
            this.clearSheet();
            this.alInjectPrintData = new StringBuilder();
            foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList detail in itemList)
            {
                #region 非药品不显示
                if (detail.Item.ItemType != FS.HISFC.Models.Base.EnumItemType.Drug)
                {
                    continue;
                }
                #endregion

                //#region 没有院注用法
                //if (!showUsage.Contains(detail.Order.Usage.Name))
                //{
                //    continue;
                //}
                //#endregion

                #region 无院注不显示
                if (detail.InjectCount == 0)
                {
                    continue;
                }
                #endregion

                #region 是否显示0次数的
                if (!chkIsNullNum.Checked && detail.ConfirmedQty == 0)
                {
                    continue;
                }
                #endregion

                #region 是否显示已经注射完
                if (!chFinish.Checked && detail.ConfirmedInjectCount == detail.InjectCount)
                {
                    continue;
                }
                #endregion

                this.addDetail(detail);
                
            }
            //if (this.neuSpread1_Sheet1.RowCount == 0)
            //{
            //    MessageBox.Show("该患者没有需要确认的医嘱信息!", "提示");
            //    this.txtInput.SelectAll();
            //}
        }

        /// <summary>
        /// 增加明细
        /// </summary>
        /// <param name="itemInfo"></param>
        private void addDetail(FS.HISFC.Models.Fee.Outpatient.FeeItemList itemInfo)
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
            this.neuSpread1_Sheet1.Cells[curRow, (int)EnumColSet.频次].Text = this.freqHelper.GetName(itemInfo.Order.Frequency.ID);
            this.neuSpread1_Sheet1.Cells[curRow, (int)EnumColSet.用法].Text = itemInfo.Order.Usage.Name;
            this.neuSpread1_Sheet1.Cells[curRow, (int)EnumColSet.开方医生].Text = this.doctHelper.GetName(itemInfo.RecipeOper.ID);
            this.neuSpread1_Sheet1.Cells[curRow, (int)EnumColSet.开方科室].Text = this.deptHelper.GetName(itemInfo.RecipeOper.Dept.ID);
            this.neuSpread1_Sheet1.Cells[curRow, (int)EnumColSet.备注].Text = itemInfo.Item.Memo;
            alInjectPrintData.Append("#" + curRow + "#" + itemInfo.Item.Name + "[" + itemInfo.Item.Specs + "]\n");
            alInjectPrintData.Append("  每次用量:" + itemInfo.Order.DoseOnce.ToString() + itemInfo.Order.DoseUnit);
            alInjectPrintData.Append("%" + curRow + "%\n  用法:" + string.Format("{0,-" + (12 - itemInfo.Order.Usage.Name.Length) + "}", itemInfo.Order.Usage.Name));
            alInjectPrintData.Append(string.Format("{0,-8}", this.freqHelper.GetName(itemInfo.Order.Frequency.ID)));
            alInjectPrintData.Append("￥" + string.Format("{0,-8}", this.getInjectDay(itemInfo, false)) + "￥~" + string.Format("{0,-8}*", this.getInjectDay(itemInfo, true)) + "~");
            alInjectPrintData.Append(string.Format("{0,10}", " 执行者:        时间:") + "%" + curRow + "%\n!" + curRow + "!");
            FS.HISFC.Models.Order.OutPatient.Order ordInfo = new FS.HISFC.Models.Order.OutPatient.Order();
            ordInfo.ID = itemInfo.Order.ID;
            ordInfo.Item.Name = itemInfo.Item.Name;
            ordInfo.Item.Specs = "[" + itemInfo.Item.Specs + "]";
            ordInfo.DoseOnce = itemInfo.Order.DoseOnce;
            ordInfo.DoseUnit = itemInfo.Order.DoseUnit;
            ordInfo.Combo.ID = itemInfo.Order.Combo.ID;
            ordInfo.Frequency.ID = itemInfo.Order.Frequency.ID;
            ordInfo.Frequency.Name = this.freqHelper.GetName(itemInfo.Order.Frequency.ID);
            ordInfo.Usage.ID = itemInfo.Order.Usage.ID;
            ordInfo.Usage.Name = itemInfo.Order.Usage.Name;
            ordInfo.Memo = itemInfo.Item.Memo;
            ordInfo.Sample.Memo = this.getInjectDay(itemInfo, false);
            this.injectionInfo.Add(ordInfo);
        }

        /// <summary>
        /// 设置表格字体
        /// </summary>
        private void setCellsFont()
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
        private void setComb()
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

            for (int j = 0; j < this.neuSpread1_Sheet1.RowCount; j++)
            {
                Regex rex = new Regex("%" + j + "%.*\n.*%" + j + "%");
                if ("┓".Equals(this.neuSpread1_Sheet1.Cells[j, (int)EnumColSet.组合].Text))
                {
                    alInjectPrintData = alInjectPrintData.Replace("#" + j + "#", "┏");
                    alInjectPrintData = alInjectPrintData.Replace("!" + j + "!", "");
                    alInjectPrintData = new StringBuilder(rex.Replace(alInjectPrintData.ToString(), "\n"));
                }
                else if ("┃".Equals(this.neuSpread1_Sheet1.Cells[j, (int)EnumColSet.组合].Text))
                {
                    alInjectPrintData = alInjectPrintData.Replace("#" + j + "#", "┃");
                    alInjectPrintData = alInjectPrintData.Replace("!" + j + "!", "");
                    alInjectPrintData = new StringBuilder(rex.Replace(alInjectPrintData.ToString(), "\n"));
                }
                else if ("┛".Equals(this.neuSpread1_Sheet1.Cells[j, (int)EnumColSet.组合].Text))
                {
                    alInjectPrintData = alInjectPrintData.Replace("#" + j + "#", "┗");
                    alInjectPrintData = alInjectPrintData.Replace("!" + j + "!", "\n");
                    alInjectPrintData = alInjectPrintData.Replace("%" + j + "%", "");
                }
                else
                {
                    alInjectPrintData = alInjectPrintData.Replace("#" + j + "#", "  ");
                    alInjectPrintData = alInjectPrintData.Replace("!" + j + "!", "\n");
                    alInjectPrintData = alInjectPrintData.Replace("%" + j + "%", "");
                }
            }
        }

        /// <summary>
        /// 全选
        /// </summary>
        private void selectAll(bool isSelected)
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
        private void setPatient()
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
        private void clearSheet()
        {
            this.neuSpread1_Sheet1.RowCount = 0;
        }

        /// <summary>
        /// 设置表格样式
        /// </summary>
        private void setSheetStyle()
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
        }

        /// <summary>
        /// 选择组合
        /// </summary>
        private void selectedComb(int row, bool isSelected)
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

        private void confirmALL()
        {
            if (this.neuSpread1_Sheet1.RowCount <= 0)
            {
                MessageBox.Show("没有要保存的数据", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            this.neuSpread1.StopCellEditing();

            int selectNum = 0;
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                if (this.neuSpread1_Sheet1.GetValue(i, 0).ToString().ToUpper() == "FALSE"
                    || this.neuSpread1_Sheet1.GetValue(i, 0).ToString() == "")
                {
                    selectNum++;
                }
            }
            if (selectNum >= this.neuSpread1_Sheet1.RowCount)
            {
                MessageBox.Show("请选择要保存的数据", "提示");
                return;
            }
            if (alInjectPrintData.ToString().Contains("(*)"))
            {
                MessageBox.Show("存在已经注射完的项目", "提示");
                return;
            }
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

                        info.Patient = reg;
                        info.Patient.ID = detail.Patient.ID;
                        info.Patient.Name = reg.Name;
                        info.Patient.Sex.ID = reg.Sex.ID;
                        info.Patient.Birthday = reg.Birthday;
                        info.Patient.PID.CardNO = reg.PID.CardNO;

                        info.Item = detail;
                        info.Item.ID = detail.Item.ID;
                        info.Item.Name = detail.Item.Name;

                        info.Item.InjectCount = detail.InjectCount;

                        info.Item.Order.DoctorDept.Name = deptHelper.GetName(detail.RecipeOper.Dept.ID);
                        info.Item.Order.DoctorDept.ID = detail.RecipeOper.Dept.ID;

                        info.Item.Order.Doctor.Name = doctHelper.GetName(detail.RecipeOper.ID);
                        info.Item.Order.Doctor.ID = detail.RecipeOper.ID;
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

                    updateInjectCount = detail.Order.Frequency.Times.Length;

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
                this.initControl();
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
        private void QueryInjectItem()
        {
            string cardNo = patient.PID.CardNO;
            ArrayList itemList = this.feeIntegrate.QueryOutpatientFeeItemListsZs(cardNo, this.dtpStart.Value, this.dtpEnd.Value);
            if (itemList == null)
            {
                MessageBox.Show("该患者没有需要确认的医嘱信息!", "提示");
                this.txtInput.SelectAll();
                return;
            }
            this.injectionInfo = new ArrayList();
            this.add(itemList);
        }

        /// <summary>
        /// 查询病人
        /// </summary>
        private void QyeryPatient()
        {
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
                    return;
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
                    return;
                }
                cardNo = tempCard.Patient.PID.CardNO;//重新获取卡号
                ArrayList regs = this.registerManager.Query(cardNo, this.dtpStart.Value.AddDays(-1));
                if (regs == null || regs.Count == 0)
                {
                    MessageBox.Show("没有病历号为:" + cardNo + "的患者!", "提示");
                    this.txtInput.SelectAll();
                    return;
                }
                this.patient = regs[0] as FS.HISFC.Models.RADT.Patient;
                this.reg = regs[0] as FS.HISFC.Models.Registration.Register;

                if (patient == null || patient.ID == "")
                {
                    MessageBox.Show("没有病历号为:" + cardNo + "的患者!", "提示");

                    this.txtInput.SelectAll();
                    return;
                }
            }
            this.setPatient();
            this.QueryInjectItem();
            this.setComb();
            this.setCellsFont();
            this.selectAll(true);
            this.txtInput.SelectAll();
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
        private void ucNurseRegisterGyzl_Load(object sender, EventArgs e)
        {
            this.init();
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
                this.QyeryPatient();
            }
            //this.txtCardNo.SelectAll();
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
            this.selectedComb(row, isSelect);
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
            this.toolBarService.AddToolButton("打印注射单", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D打印, true, false, null);
            this.toolBarService.AddToolButton("补打注射单", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D打印, true, false, null);
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
                    this.confirmALL();
                    break;
                case "全选":
                    this.selectAll(true);
                    break;
                case "取消全选":
                    this.selectAll(false);
                    break;
                case "打印瓶签":
                    this.printCure();
                    break;
                case "打印注射单":
                    this.printInject();
                    break;
                case "补打注射单":
                    this.rePrintInject();
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region 打印

        /// <summary>
        /// 打印瓶签
        /// </summary>
        private void printCure()
        {
            printData.Clear();

            #region 按照勾选的打印

            int rowCount = this.neuSpread1_Sheet1.RowCount;
            bool isSelect = false;
            for (int i = 0; i < rowCount; i++)
            {
                isSelect = Convert.ToBoolean(this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.选择].Value);
                if (isSelect)
                {
                    printData.Add(this.neuSpread1_Sheet1.Rows[i].Tag);
                }
            }
            if (printData == null || printData.Count == 0)
            {
                MessageBox.Show("请选择打印的数据");
                return;
            }
            #endregion

            PrintCure();
        }


        /// <summary>
        /// 打印注射单
        /// </summary>
        private void printInject()
        {
            printData.Clear();

            #region 按照勾选的打印

            int rowCount = this.neuSpread1_Sheet1.RowCount;
            bool isSelect = false;
            for (int i = 0; i < rowCount; i++)
            {
                isSelect = Convert.ToBoolean(this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.选择].Value);
                if (isSelect)
                {
                    printData.Add(this.neuSpread1_Sheet1.Rows[i].Tag);
                }
            }
            if (printData == null || printData.Count == 0)
            {
                MessageBox.Show("请选择打印的数据");
                return;
            }
            #endregion

            //tempDate = alInjectPrintData.ToString();
            //Regex rex = new Regex("~.*~");
            //tempDate = rex.Replace(tempDate, "");
            //tempDate = tempDate.Replace("￥", "");
            if (showTable)
            {
                this.PrintNew();
            }
            else
            {
                this.print();
            }
        }

        private void rePrintInject()
        {
            tempDate = alInjectPrintData.ToString();
            Regex rex = new Regex("￥.*￥");
            tempDate = rex.Replace(tempDate.ToString(), "");
            tempDate = tempDate.Replace("~", "");
            if (showTable)
            {
                this.PrintNew();
            }
            else
            {
                this.print();
            }
        }
       
        private void print()
        {
            FS.SOC.Local.Nurse.GuangZhou.Gyzl.IInjectPrint.ucInjectPrint injectPrint = new FS.SOC.Local.Nurse.GuangZhou.Gyzl.IInjectPrint.ucInjectPrint();
            ArrayList al = new ArrayList();
            al.Add(false);
            al.Add(this.reg);
            al.Add(this.printData);
            injectPrint.Init(al);
        }

        private void PrintCure()
        {
            FS.SOC.Local.Nurse.GuangZhou.Gyzl.IInjectCurePrint.PrintCure PrintCure = new FS.SOC.Local.Nurse.GuangZhou.Gyzl.IInjectCurePrint.PrintCure();
            ArrayList al = new ArrayList();
            //al.Add(false);
            al.Add(this.reg);
            al.Add(this.printData);
            PrintCure.Init(al);
        }

        private void PrintNew()
        {
            FS.SOC.Local.Nurse.GuangZhou.Gyzl.IInjectPrint.ucInjectPrintNew injectPrint = new FS.SOC.Local.Nurse.GuangZhou.Gyzl.IInjectPrint.ucInjectPrintNew();
            ArrayList al = new ArrayList();
            al.Add(tempDate);
            injectPrint.setPatient(reg);
            injectPrint.PrintOutPatientOrderBill(this.injectionInfo,true);
        }
        #endregion
    }
}
