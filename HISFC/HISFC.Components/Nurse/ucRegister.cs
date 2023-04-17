using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Xml;

namespace FS.HISFC.Components.Nurse
{
    /// <summary>
    /// 门诊注射管理主界面
    /// </summary>
    public partial class ucRegister : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        public ucRegister()
        {
            InitializeComponent();

            if (!DesignMode)
            {
                this.Load += new EventHandler(ucRegister_Load);
            }
        }

        /**  基本功能
         *  1、可以按照系统类别、用法、项目打印
         *  2、可以打印1次、1天、所有院注次数
         *  3、可以按照天数、次数、全部 设置打印张数
         *  4、
         * 
         * */

        #region 变量

        #region 业务层变量

        /// <summary>
        /// 门诊费用管理类
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();

        /// <summary>
        /// 院注管理类
        /// </summary>
        private FS.HISFC.BizLogic.Nurse.Inject InjMgr = new FS.HISFC.BizLogic.Nurse.Inject();

        /// <summary>
        /// 药品管理
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Pharmacy phaIntegrate = new FS.HISFC.BizProcess.Integrate.Pharmacy();

        /// <summary>
        /// 挂号管理
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Registration.Registration regMgr = new FS.HISFC.BizProcess.Integrate.Registration.Registration();

        /// <summary>
        /// 综合业务层
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager inteMgr = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// 医嘱函数
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Order orderMgr = new FS.HISFC.BizProcess.Integrate.Order();

        #endregion

        /// <summary>
        /// 当前患者
        /// </summary>
        private FS.HISFC.Models.Registration.Register myRegInfo = null;

        /// <summary>
        /// 治疗单的数据
        /// </summary>
        private ArrayList alPrint = null;


        /// 最大注射顺序号
        /// </summary>
        private int maxInjectOrder = 0;

        /// <summary>
        /// 注射单的数据
        /// </summary>
        private ArrayList alInject = null;

        /// <summary>
        /// 医生人员帮助类
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper doctHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 频次帮助类
        /// </summary>
        FS.FrameWork.Public.ObjectHelper freqHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 存放科室信息
        /// </summary>
        FS.FrameWork.Public.ObjectHelper deptHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 病人信息集合
        /// </summary>
        Hashtable hsInfos = new Hashtable();

        /// <summary>
        /// 最大注射顺序号
        /// </summary>
        //private int maxInjectOrder = 0;

        /// <summary>
        /// 获取注射号 接口模式
        /// </summary>
        private FS.HISFC.BizProcess.Interface.Nurse.IGetInjectOrderNo IGetOrderNo = null;


        /// <summary>
        /// 工具栏
        /// </summary>
        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        /// <summary>
        /// 配置文件
        /// </summary>
        private string injectRegisterXml = FS.FrameWork.WinForms.Classes.Function.CurrentPath + @".\\Profile\\injectRegister.xml";

        FS.HISFC.Models.Pharmacy.Item drug = null;

        #endregion

        #region 属性

        /// <summary>
        /// 是否显示患者当天可登记的全部处方
        /// </summary>
        private bool isShowAllInject = false;

        /// <summary>
        /// 是否显示患者当天可登记的全部处方
        /// </summary>
        [Description("是否显示患者当天可登记的全部处方"), Category("设置"), DefaultValue("false")]
        public bool IsShowAllInject
        {
            get
            {
                return isShowAllInject;
            }
            set
            {
                isShowAllInject = value;
            }
        }

        /// <summary>
        /// 打印在巡视卡上的用法
        /// </summary>
        private string usage = "iv.drip(门）;";

        /// <summary>
        /// 用法是否打印在巡视卡上
        /// {EE46827D-D081-4aa5-8653-1EF9D176A5DC}
        /// </summary>
        [Description("打印在巡视卡上用法(区分注射用法、输液用法)"), Category("设置")]
        public string Usage
        {
            get
            {
                return this.usage;
            }
            set
            {
                this.usage = value;
            }
        }

        /// <summary>
        /// 是否用special常数过滤院注,否则用右下角设置过滤
        /// </summary>
        private bool isUseSpecialConstant = true;

        /// <summary>
        /// 是否用special常数过滤院注,否则用右下角设置过滤
        /// </summary>
        [Description("是否用special常数过滤院注,否则用右下角设置过滤"), Category("设置")]
        public bool IsUseSpecialConstant
        {
            get
            {
                return this.isUseSpecialConstant;
            }
            set
            {
                this.isUseSpecialConstant = value;
            }
        }

        /// <summary>
        /// 注射用法（不包括输液项目）
        /// </summary>
        private string injectUsage = "";

        /// <summary>
        /// 注射用法（不包括输液项目）
        /// </summary>
        [Description("注射用法维护（不包括输液用法）"), Category("设置")]
        public string InjectUsage
        {
            get
            {
                return this.injectUsage;
            }
            set
            {
                this.injectUsage = value;
            }
        }

        /// <summary>
        /// 是否一次打印所有院注次数,否则按照每次打印一条
        /// </summary>
        private bool isSaveAllInjectCount = true;

        /// <summary>
        /// 是否一次打印所有院注次数,否则按照每次打印一条
        /// </summary>
        [Description("是否一次打印所有院注次数,否则按照每次打印一条"), Category("设置")]
        public bool IsSaveAllInjectCount
        {
            get
            {
                return isSaveAllInjectCount;
            }
            set
            {
                isSaveAllInjectCount = value;
            }
        }

        private bool isSaveDayInjectCount = false;
        [Description("是否一次打印当天院注次数,否则按照每次打印一条"), Category("设置")]
        public bool IsSaveDayInjectCount
        {
            get
            {
                return isSaveDayInjectCount;
            }
            set
            {
                isSaveDayInjectCount = value;
            }
        }

        /// <summary>
        /// 是否自动打印巡视卡
        /// </summary>
        private bool isAutoPrint = true;

        /// <summary>
        /// 是否自动打印巡视卡
        /// </summary>
        [Description("是否自动打印巡视卡"), Category("设置")]
        public bool IsAutoPrint
        {
            get
            {
                return isAutoPrint;
            }
            set
            {
                this.isAutoPrint = value;
            }
        }

        /// <summary>
        /// 是否自动保存数据
        /// </summary>
        private bool isAutoSave = true;

        /// <summary>
        /// 是否自动保存数据
        /// </summary>
        [Description("是否自动保存数据"), Category("设置")]
        public bool IsAutoSave
        {
            get
            {
                return isAutoSave;
            }
            set
            {
                this.isAutoSave = value;
            }
        }

        /// <summary>
        /// 是否使用注射顺序号
        /// </summary>
        private bool isUserOrderNumber = false;

        /// <summary>
        /// 是否使用注射顺序号
        /// </summary>
        [Description("是否使用注射顺序号"), Category("设置")]
        public bool IsUserOrderNumber
        {
            get
            {
                return isUserOrderNumber;
            }
            set
            {
                this.isUserOrderNumber = value;
            }
        }

        /// <summary>
        /// 自动打印时保存是否提示
        /// </summary>
        private bool isMessageOnSave = true;

        /// <summary>
        /// 自动打印时保存是否提示
        /// </summary>
        [Description("自动打印时保存是否提示"), Category("设置")]
        public bool IsMessageOnSave
        {
            get
            {
                return isMessageOnSave;
            }
            set
            {
                this.isMessageOnSave = value;
            }
        }

        /// <summary>
        /// 查询的挂号的日期间隔
        /// </summary>
        private int queryRegDays = 2;

        /// <summary>
        /// 查询的挂号的日期间隔
        /// </summary>
        [Category("查询设置"), Description("查询挂号信息的日期间隔（从界面上的开始时间往前查询几天）")]
        public int QueryRegDays
        {
            get
            {
                return queryRegDays;
            }
            set
            {
                queryRegDays = value;
            }
        }

        /// <summary>
        /// 开始时间的日期间隔(距今天的间隔日期)
        /// </summary>
        private int beginDateIntervalDays = 0;

        /// <summary>
        /// 开始时间的日期间隔(距今天的间隔日期)
        /// </summary>
        [Category("查询设置"), Description("开始时间的日期间隔(距今天的间隔日期)")]
        public int BeginDateIntervalDays
        {
            get
            {
                return beginDateIntervalDays;
            }
            set
            {
                beginDateIntervalDays = value;
            }
        }

        /// <summary>
        /// 是否显示辅材
        /// </summary>
        private bool isShowSubjob = false;

        /// <summary>
        /// 是否显示辅材
        /// </summary>
        [Description("是否显示辅材"), Category("设置")]
        public bool IsShowSubjob
        {
            get
            {
                return this.isShowSubjob;
            }
            set
            {
                this.isShowSubjob = value;
            }
        }
        #endregion

        #region 初始化

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ucRegister_Load(object sender, EventArgs e)
        {
            this.dtpStart.Value = this.InjMgr.GetDateTimeFromSysDateTime().Date.AddDays(0 - this.beginDateIntervalDays);
            this.dtpEnd.Value = this.InjMgr.GetDateTimeFromSysDateTime().Date.AddDays(1).AddSeconds(-1);

            this.InitData();
            this.SetFP();

            this.InitOrder();
            //瓶签补打
            this.ucCureReprint1.Init();

            this.Clear();
        }

        /// <summary>
        /// 初始化医生
        /// </summary>
        private void InitData()
        {
            ArrayList al = this.inteMgr.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.D);

            if (al == null)
            {
                MessageBox.Show("查询医生列表出错！\r\n" + inteMgr.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            doctHelper.ArrayObject = al;

            //频次
            ArrayList alFrequency = this.inteMgr.QuereyFrequencyList();
            if (al == null)
            {
                MessageBox.Show("查询频次列表出错！\r\n" + inteMgr.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            freqHelper.ArrayObject = alFrequency;

            al = new ArrayList();
            al = this.inteMgr.GetDepartment();
            if (al == null)
            {
                MessageBox.Show("查询科室列表出错！\r\n" + inteMgr.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.deptHelper.ArrayObject = al;
        }

        /// <summary>
        /// 设置格式
        /// </summary>
        private void SetFP()
        {
            FarPoint.Win.Spread.CellType.TextCellType txtOnly = new FarPoint.Win.Spread.CellType.TextCellType();
            txtOnly.ReadOnly = true;

            FarPoint.Win.Spread.CellType.NumberCellType numType = new FarPoint.Win.Spread.CellType.NumberCellType();
            numType.DecimalPlaces = 0;

            this.neuSpread1_Sheet1.Columns[(int)EnumColSet.院注次数].CellType = txtOnly;
            this.neuSpread1_Sheet1.Columns[(int)EnumColSet.确认次数].CellType = txtOnly;
            this.neuSpread1_Sheet1.Columns[(int)EnumColSet.开单医生].CellType = txtOnly;
            this.neuSpread1_Sheet1.Columns[(int)EnumColSet.科别].CellType = txtOnly;
            this.neuSpread1_Sheet1.Columns[(int)EnumColSet.医嘱].CellType = txtOnly;
            this.neuSpread1_Sheet1.Columns[(int)EnumColSet.组合].CellType = txtOnly;
            this.neuSpread1_Sheet1.Columns[(int)EnumColSet.每次用量].CellType = txtOnly;
            this.neuSpread1_Sheet1.Columns[(int)EnumColSet.频次].CellType = txtOnly;
            this.neuSpread1_Sheet1.Columns[(int)EnumColSet.用法].CellType = txtOnly;
            this.neuSpread1_Sheet1.Columns[(int)EnumColSet.皮试].CellType = txtOnly;
            this.neuSpread1_Sheet1.Columns[(int)EnumColSet.注射时间].CellType = txtOnly;


            this.neuSpread1_Sheet1.Columns[(int)EnumColSet.序号].CellType = numType;
            this.neuSpread1_Sheet1.Columns[(int)EnumColSet.院注次数].CellType = numType;
            this.neuSpread1_Sheet1.Columns[(int)EnumColSet.确认次数].CellType = numType;
            this.neuSpread1_Sheet1.Columns[(int)EnumColSet.注射序号].CellType = numType;

            if (System.IO.File.Exists(injectRegisterXml))
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnFormatProperty(this.neuSpread1_Sheet1, injectRegisterXml);
            }
            else
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnFormatProperty(this.neuSpread1_Sheet1, injectRegisterXml);
            }

            this.neuSpread1.ColumnWidthChanged += new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(neuSpread1_ColumnWidthChanged);
        }

        /// <summary>
        /// 初始化注射顺序号
        /// </summary>
        private void InitOrder()
        {
            //读取是否自动生成注射顺序
            try
            {
                //需要完善
                //修改为右下角设置

                bool isAutoInjectOrder = false;

                //门诊护士－是否生成注射顺序
                isAutoInjectOrder = FS.FrameWork.Function.NConvert.ToBoolean(this.inteMgr.QueryControlerInfo("900005"));
                if (isAutoInjectOrder)
                {
                    this.chkIsOrder.Checked = true;
                    this.SetOrder();
                    this.lbLastOrder.Text = "今天最后一次注射号:" +
                        (FS.FrameWork.Function.NConvert.ToInt32(this.txtOrder.Text.Trim()) - 1).ToString();
                }
                else
                {
                    this.chkIsOrder.Checked = false;
                    this.lbLastOrder.Text = "本机不自动生成注射顺序号!";
                    this.txtOrder.Text = "0";
                }
            }
            catch //无配置文件
            {
                this.chkIsOrder.Checked = false;
                this.lbLastOrder.Text = "本机不自动生成注射顺序号!";
                this.txtOrder.Text = "0";
            }


        }

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
            this.toolBarService.AddToolButton("全选", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q全选, true, false, null);
            this.toolBarService.AddToolButton("取消全选", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q取消, true, false, null);
            this.toolBarService.AddToolButton("打印瓶签", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D打印, true, false, null);
            this.toolBarService.AddToolButton("打印签名卡", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D打印, true, false, null);
            this.toolBarService.AddToolButton("打印注射单", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D打印, true, false, null);
            this.toolBarService.AddToolButton("打印巡视单", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D打印, true, false, null);
            this.toolBarService.AddToolButton("打印患者卡", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D打印, true, false, null);
            this.toolBarService.AddToolButton("打印号码条", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D打印, true, false, null);
            this.toolBarService.AddToolButton("修改皮试", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.X修改, true, false, null);
            this.toolBarService.AddToolButton("打印LIS条码", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D打印, true, false, null);
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
                case "全选":
                    this.SelectAll(true);
                    break;
                case "取消全选":
                    this.SelectAll(false);
                    break;
                case "打印瓶签":
                    this.PrintCure();
                    break;
                case "打印签名卡":
                    this.PrintItinerate();
                    break;
                case "打印注射单":
                    this.PrintInject();
                    break;
                case "打印巡视单":
                    this.PrintInjectScoutCard();
                    break;
                case "打印患者卡":
                    this.PrintPatient();
                    break;
                case "打印号码条":
                    this.PrintNumber();
                    break;
                case "修改皮试":
                    this.ModifyHytest();
                    break;
                case "打印LIS条码":
                    this.PrintLisBarCode();
                    break;
                default:
                    break;
            }
        }

        #endregion

        #endregion

        #region 公共

        /// <summary>
        /// 设置颜色(高亮度显示最后一条clinic医嘱)
        /// </summary>
        /// <returns></returns>
        private int ShowColor()
        {
            //取得最大clinic_code
            int maxClinic = 0;
            if (this.neuSpread1_Sheet1.RowCount <= 0)
            {
                return -1;
            }
            FS.HISFC.Models.Fee.Outpatient.FeeItemList item = null;
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                item = (FS.HISFC.Models.Fee.Outpatient.FeeItemList)this.neuSpread1_Sheet1.Rows[i].Tag;
                if (FS.FrameWork.Function.NConvert.ToInt32(item.ID) > maxClinic)
                {
                    maxClinic = FS.FrameWork.Function.NConvert.ToInt32(item.ID);
                }
            }

            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                item = (FS.HISFC.Models.Fee.Outpatient.FeeItemList)this.neuSpread1_Sheet1.Rows[i].Tag;
                if (item.ID == maxClinic.ToString())
                {
                    this.neuSpread1_Sheet1.Rows[i].ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    this.neuSpread1_Sheet1.SetValue(i, 0, false);
                }
            }
            return 0;
        }

        /// <summary>
        /// 获得当前界面上最大注射顺序
        /// </summary>
        /// <returns></returns>
        private int GetMaxInjectOrder()
        {
            if (this.neuSpread1_Sheet1.RowCount <= 0)
            {
                return 0;
            }

            this.neuSpread1.StopCellEditing();

            maxInjectOrder = 0;

            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                if (this.neuSpread1_Sheet1.GetText(i, (int)EnumColSet.选择).ToUpper() == "FALSE" ||
                    this.neuSpread1_Sheet1.GetText(i, (int)EnumColSet.选择) == "")
                {
                    continue;
                }
                if (FS.FrameWork.Function.NConvert.ToInt32(this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.序号].Text) > maxInjectOrder)
                {
                    maxInjectOrder = FS.FrameWork.Function.NConvert.ToInt32(this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.序号].Text);
                }
            }
            return maxInjectOrder;
        }

        /// <summary>
        /// 设置病人信息
        /// </summary>
        /// <param name="reg"></param>
        private void SetPatient(FS.HISFC.Models.Registration.Register reg)
        {
            if (reg == null || reg.ID == "")
            {
                return;
            }
            else
            {
                this.txtName.Text = reg.Name;
                this.txtSex.Text = reg.Sex.Name;
                this.txtBirthday.Text = reg.Birthday.ToString("yyyy-MM-dd");
                this.txtAge.Text = this.InjMgr.GetAge(reg.Birthday);
                this.txtCardNo.Text = reg.PID.CardNO;

                #region 诊断

                ArrayList alDiagnoses = new ArrayList();
                alDiagnoses = (new FS.HISFC.BizLogic.HealthRecord.Diagnose()).QueryDiagnoseNoOps(reg.ID);
                this.txtDiagnoses.Text = "";

                if (alDiagnoses != null)
                {
                    foreach (FS.HISFC.Models.HealthRecord.Diagnose dg in alDiagnoses)
                    {
                        if (dg.Memo == reg.ID || !dg.IsValid)
                        {
                            //把非本次挂号的诊断过滤掉，如果以后诊断很多，通过这种方式很慢的话，要重写一个业务层
                            continue;
                        }
                        else
                        {
                            //把本次挂号的诊断连接起来放到lblDiagnose里
                            this.txtDiagnoses.Text += dg.DiagInfo.ICD10.Name + " ";
                        }
                    }
                }
                else
                {
                    this.txtDiagnoses.Text = "";
                }
                #endregion
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

        #endregion

        #region  打印

        /// <summary>
        /// 打印患者卡
        /// </summary>
        private void PrintPatient()
        {
            ArrayList al = this.GetPrintInjectList();
            if (al == null || al.Count <= 0)
            {
                MessageBox.Show("没有选择数据!");
                return;
            }
            FS.HISFC.BizProcess.Interface.Nurse.IInjectPatientPrint patientPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Nurse.IInjectPatientPrint)) as FS.HISFC.BizProcess.Interface.Nurse.IInjectPatientPrint;
            if (patientPrint == null)
            {
                patientPrint = new Nurse.Print.ucPrintPatient() as FS.HISFC.BizProcess.Interface.Nurse.IInjectPatientPrint;
            }
            patientPrint.Init(al);
        }

        /// <summary>
        /// 打印瓶签
        /// </summary>
        private void PrintCure()
        {
            ArrayList al = this.GetPrintInjectList();
            if (al == null || al.Count <= 0)
            {
                MessageBox.Show("没有选择数据!");
                return;
            }
            FS.HISFC.BizProcess.Interface.Nurse.IInjectCurePrint curePrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Nurse.IInjectCurePrint)) as FS.HISFC.BizProcess.Interface.Nurse.IInjectCurePrint;
            if (curePrint == null)
            {
                curePrint = new Nurse.Print.ucPrintCure() as FS.HISFC.BizProcess.Interface.Nurse.IInjectCurePrint;
            }
            curePrint.Init(al);
        }

        /// <summary>
        /// 打印注射单.
        /// </summary>
        private void PrintInject()
        {
            ArrayList al = this.GetPrintInjectList();
            if (al == null || al.Count <= 0)
            {
                MessageBox.Show("没有选择数据!");
                return;
            }
            FS.HISFC.BizProcess.Interface.Nurse.IInjectPrint injectPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Nurse.IInjectPrint)) as FS.HISFC.BizProcess.Interface.Nurse.IInjectPrint;
            if (injectPrint == null)
            {
                injectPrint = new Nurse.Print.ucPrintInject() as FS.HISFC.BizProcess.Interface.Nurse.IInjectPrint;
            }
            injectPrint.Init(al);
        }

        /// <summary>
        /// 打印签名卡 单张打印？
        /// </summary>
        private void PrintItinerate()
        {
            ArrayList al = this.GetPrintInjectList();
            if (al == null || al.Count <= 0)
            {
                MessageBox.Show("没有选择数据!");
                return;
            }

            FS.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint itineratePrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint)) as FS.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint;
            if (itineratePrint == null)
            {
                itineratePrint = new Nurse.Print.ucPrintItinerate() as FS.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint;
            }
            itineratePrint.IsReprint = false;
            itineratePrint.Init(al);
        }

        /// <summary>
        /// 打印签名卡  续打
        /// </summary>
        private void PrintItinerateLarge()
        {
            ArrayList al = this.GetPrintInjectList();
            if (al == null || al.Count <= 0)
            {
                MessageBox.Show("没有选择数据!");
                return;
            }
            FS.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint itineratePrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint)) as FS.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint;
            if (itineratePrint == null)
            {
                itineratePrint = new Nurse.Print.ucPrintItinerateLarge() as FS.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint;
            }
            itineratePrint.IsReprint = false;
            itineratePrint.Init(al);
        }

        /// <summary>
        /// 打印静脉输液巡视卡
        /// </summary>
        private void PrintInjectScoutCard()
        {
            int intReturn = this.GetAllPrintInjectList();
            if (intReturn == -1)
            {
                MessageBox.Show("没有选择数据!");
                return;
            }
            foreach (ArrayList al in hsInfos.Values)
            {
                FS.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint itineratePrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint)) as FS.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint;
                if (itineratePrint == null)
                {
                    return;
                }
                //itineratePrint.Init(al);
                if (string.IsNullOrEmpty(injectUsage))
                {
                    itineratePrint.IsReprint = false;
                    itineratePrint.Init(al);
                }
                else
                {
                    ArrayList alZS = new ArrayList();
                    ArrayList alSY = new ArrayList();
                    for (int i = 0; i < al.Count; i++)
                    {
                        FS.HISFC.Models.Nurse.Inject info = al[i] as FS.HISFC.Models.Nurse.Inject;
                        if (injectUsage.Contains(info.Item.Order.Usage.Name.ToString()))
                        {
                            alZS.Add(info);
                        }
                        else
                        {
                            alSY.Add(info);
                        }
                    }
                    if (alZS.Count > 0)
                    {
                        itineratePrint.IsReprint = false;
                        itineratePrint.Init(alZS);
                    }
                    if (alSY.Count > 0)
                    {
                        itineratePrint.IsReprint = false;
                        itineratePrint.Init(alSY);
                    }
                }
            }

        }

        /// <summary>
        /// 打印注射条码号
        /// </summary>
        private void PrintNumber()
        {
            ArrayList al = this.GetPrintInjectList();
            if (al == null || al.Count == 0)
            {
                MessageBox.Show("没有选择数据!");
                return;
            }
            FS.HISFC.BizProcess.Interface.Nurse.IInjectNumberPrint numberPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Nurse.IInjectNumberPrint)) as FS.HISFC.BizProcess.Interface.Nurse.IInjectNumberPrint;
            if (numberPrint == null)
            {
                numberPrint = new Nurse.Print.ucPrintNumber() as FS.HISFC.BizProcess.Interface.Nurse.IInjectNumberPrint;
            }
            numberPrint.Init(al);
        }

        /// <summary>
        /// 打印Lis条码
        /// </summary>
        private void PrintLisBarCode()
        {
            //if (myRegInfo == null)
            //{
            //    MessageBox.Show("没有患者信息");
            //    return;
            //}
            FS.HISFC.BizProcess.Interface.Registration.IPrintBar lisBarPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Registration.IPrintBar)) as FS.HISFC.BizProcess.Interface.Registration.IPrintBar;
            if (lisBarPrint == null)
            {
                return;
            }
            string err = "";
            lisBarPrint.printBar(this.myRegInfo, ref err);
        }


        /// <summary>
        /// 获取要打印的数据
        /// </summary>
        /// <returns></returns>
        private ArrayList GetPrintInjectList()
        {
            ArrayList al = new ArrayList();
            ArrayList alJiePing = new ArrayList();
            this.neuSpread1.StopCellEditing();

            FS.HISFC.Models.Fee.Outpatient.FeeItemList detail = null;
            FS.HISFC.Models.Order.OutPatient.Order orderinfo = null;
            FS.HISFC.Models.Nurse.Inject info = null;
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                if (this.neuSpread1_Sheet1.GetValue(i, 0).ToString().ToUpper() == "FALSE")
                {
                    continue;
                }

                detail = (FS.HISFC.Models.Fee.Outpatient.FeeItemList)this.neuSpread1_Sheet1.Rows[i].Tag;
                orderinfo = (FS.HISFC.Models.Order.OutPatient.Order)this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.皮试].Tag;
                info = new FS.HISFC.Models.Nurse.Inject();

                info.Patient = myRegInfo;
                //info.Patient.Name = reg.Name;
                //info.Patient.Sex.ID = reg.Sex.ID;
                //info.Patient.Birthday = reg.Birthday;

                info.Item = detail;
                info.Item.InjectCount = FS.FrameWork.Function.NConvert.ToInt32(this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.院注次数].Text);
                info.OrderNO = this.txtOrder.Text.ToString();
                info.Item.Order.Combo.ID = this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.组合].Tag.ToString();

                info.Item.Order.ReciptDoctor.Name = doctHelper.GetName(detail.RecipeOper.ID);
                info.Item.Order.ReciptDoctor.ID = detail.RecipeOper.ID;
                info.Item.Name = detail.Item.Name;
                string strOrder = "";
                if (this.neuSpread1_Sheet1.GetValue(i, 1) == null || this.neuSpread1_Sheet1.GetValue(i, 1).ToString() == "")
                {
                    strOrder = "";
                }
                else
                {
                    strOrder = this.neuSpread1_Sheet1.GetValue(i, 1).ToString();
                }
                info.InjectOrder = strOrder;


                if (orderinfo != null)
                {
                    //备注应该是Memo+皮试
                    info.Memo = orderinfo.Memo;
                    info.Hypotest = orderinfo.HypoTest;
                }
                info.PrintNo = detail.User02;

                al.Add(info);

                #region 接瓶好像没用
                ////判断接瓶,如果是则添加到alJiePing中
                //if (orderinfo.ExtendFlag1 == null || orderinfo.ExtendFlag1.Length < 1)
                //    orderinfo.ExtendFlag1 = "1|";
                ////				string[] str = orderinfo.Mark1.Split('|');
                //int inum = FS.FrameWork.Function.NConvert.ToInt32(orderinfo.ExtendFlag1.Substring(0, 1));
                //info.Memo = inum.ToString();
                ////FS.HISFC.Components.Function.NConvert.ToInt32(str[0]);
                ////				if(inum > 1)
                ////				{
                ////						FS.HISFC.Models.Nurse.Inject inj = new FS.HISFC.Models.Nurse.Inject();
                ////						inj = info.Clone();
                ////						inj.InjectOrder = (this.GetMaxInjectOrder() + 1).ToString();
                ////						maxInjectOrder++;
                ////						alJiePing.Add(inj);
                ////					}
                ////				}
                #endregion

                //{EB016FFE-0980-479c-879E-225462ECA6D0}

            }
            return al;
        }

        /// <summary>
        /// 获取要打印的数据（可维护用法）
        /// </summary>
        /// <returns></returns>
        private int GetAllPrintInjectList()
        {
            this.SelectAll(true);
            this.neuSpread1.StopCellEditing();
            hsInfos.Clear();
            FS.HISFC.Models.Fee.Outpatient.FeeItemList detail = null;
            FS.HISFC.Models.Order.OutPatient.Order orderinfo = null;
            FS.HISFC.Models.Nurse.Inject info = null;
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                if (this.neuSpread1_Sheet1.GetValue(i, 0).ToString().ToUpper() == "FALSE")
                    continue;
                detail = (FS.HISFC.Models.Fee.Outpatient.FeeItemList)this.neuSpread1_Sheet1.Rows[i].Tag;
                orderinfo = (FS.HISFC.Models.Order.OutPatient.Order)this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.皮试].Tag;
                info = new FS.HISFC.Models.Nurse.Inject();
                if (!usage.Contains(detail.Order.Usage.Name))
                {
                    continue;
                }

                info.Patient.ID = detail.Patient.ID;
                info.Patient.Name = myRegInfo.Name;
                info.Patient.Sex.ID = myRegInfo.Sex.ID;
                info.Patient.Birthday = myRegInfo.Birthday;
                info.Patient.Card.ID = this.txtCardNo.Text.Trim().PadLeft(10, '0');

                info.Item = detail;
                info.Item.InjectCount = FS.FrameWork.Function.NConvert.ToInt32(this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.院注次数].Text);
                info.OrderNO = this.txtOrder.Text.ToString();
                info.Item.Order.Combo.ID = this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.组合].Tag.ToString();

                info.Item.Order.ReciptDoctor.Name = doctHelper.GetName(detail.RecipeOper.ID);
                info.Item.Order.ReciptDoctor.ID = detail.RecipeOper.ID;

                info.Item.Name = detail.Item.Name;
                string strOrder = "";
                if (this.neuSpread1_Sheet1.GetValue(i, 1) == null || this.neuSpread1_Sheet1.GetValue(i, 1).ToString() == "")
                {
                    strOrder = "";
                }
                else
                {
                    strOrder = this.neuSpread1_Sheet1.GetValue(i, 1).ToString();
                }
                info.Item.Days = detail.Days;
                //患者当次注射处方时间
                info.InjectOrder = strOrder;

                info.User03 = this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.注射时间].Text;

                string hypoTest = string.IsNullOrEmpty(this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.皮试].Text) ? "" : "(" + this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.皮试].Text + ")";

                if (orderinfo != null)
                {
                    //备注应该是Memo+皮试
                    //info.Memo = orderinfo.ExtendFlag1;
                    info.Memo = orderinfo.Memo;
                    info.Hypotest = orderinfo.HypoTest;
                }

                if (!hsInfos.ContainsKey(info.Item.Order.ReciptDoctor.ID))
                {
                    ArrayList al = new ArrayList();
                    al.Add(info);
                    hsInfos.Add(info.Item.Order.ReciptDoctor.ID, al);
                }
                else
                {
                    ((ArrayList)hsInfos[info.Item.Order.ReciptDoctor.ID]).Add(info);
                }
            }
            return 1;
        }

        /// <summary>
        /// 获取最优先的使用方法
        /// </summary>
        /// <returns></returns>
        private FS.HISFC.Models.Base.Const GetFirstUsage()
        {
            FS.HISFC.Models.Fee.Outpatient.FeeItemList info = new FS.HISFC.Models.Fee.Outpatient.FeeItemList();
            if (this.neuSpread1_Sheet1.RowCount <= 0) return new FS.HISFC.Models.Base.Const();

            int FirstCodeNum = 10000;
            FS.HISFC.Models.Base.Const retobj = new FS.HISFC.Models.Base.Const();
            try
            {
                for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
                {
                    info = (FS.HISFC.Models.Fee.Outpatient.FeeItemList)this.neuSpread1_Sheet1.Rows[i].Tag;
                    FS.FrameWork.Models.NeuObject obj = this.inteMgr.GetConstant("SPECIAL", info.Order.Usage.ID);
                    FS.HISFC.Models.Base.Const conobj = (FS.HISFC.Models.Base.Const)obj;

                    if (conobj.SortID < FirstCodeNum)
                    {
                        FirstCodeNum = conobj.SortID;
                        retobj = conobj;
                    }
                }
            }
            catch
            {
                return retobj;
            }

            return retobj;
        }

        #endregion

        #region 注射顺序号的处理

        /// <summary>
        /// 设置默认注射顺序
        /// </summary>
        private void SetInject()
        {
            #region  没有数据就不管了,直接返回
            if (this.neuSpread1_Sheet1.RowCount <= 0) return;
            #endregion

            #region 设置患者今天的注射顺序号
            if (this.chkIsOrder.Checked)
            {
                this.SetOrder();
            }
            else
            {
                this.txtOrder.Text = "0";
                //{30E1EF7D-1236-4e38-A8E3-7567C9E33B0B}
                for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
                {
                    this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.注射序号].Text = this.txtOrder.Text;
                }
            }
            #endregion

            #region 设置每组项目的注射顺序
            int InjectOrder = 1;
            this.neuSpread1_Sheet1.SetValue(0, 1, 1, false);
            for (int i = 1; i < this.neuSpread1_Sheet1.RowCount; i++)
            {

                if (this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.组合].Text == null || this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.组合].Text.Trim() == "")
                {
                    InjectOrder++;
                    this.neuSpread1_Sheet1.SetValue(i, 1, InjectOrder, false);
                }
                else if (this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.组合].Text != null && this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.组合].Text.Trim() != ""
                    //{24A47206-F111-4817-A7B4-353C21FC7724} 患者可以登记全天所有注射处方
                    && this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.组合].Tag.ToString() + this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.注射时间].Text == this.neuSpread1_Sheet1.Cells[i - 1, (int)EnumColSet.组合].Tag.ToString() + this.neuSpread1_Sheet1.Cells[i - 1, (int)EnumColSet.注射时间].Text)
                {
                    this.neuSpread1_Sheet1.SetValue(i, 1, InjectOrder, false);
                }
                else
                {
                    InjectOrder++;
                    this.neuSpread1_Sheet1.SetValue(i, 1, InjectOrder, false);
                }
            }
            #endregion
        }

        /// <summary>
        /// 设置注射号
        /// </summary>
        private void SetOrder()
        {
            if (!this.chkIsOrder.Checked)
            {
                this.txtOrder.Text = "0";
                this.lbLastOrder.Text = "现在本机没有设置自动生成序号!";
                return;
            }
            //如果自动生成,设置第一个序号,并赋值this.currentOrder
            //改为通过接口实现，如果没有配置则按原代码生程序号

            this.CreateInterface();

            if (IGetOrderNo != null)
            {
                string orderNo = IGetOrderNo.GetOrderNo(this.myRegInfo);

                this.txtOrder.Text = orderNo;
                if (this.neuSpread1_Sheet1.Rows.Count == 0)
                {
                    return;
                }

                string comboAndInjectTime = this.neuSpread1_Sheet1.Cells[0, (int)EnumColSet.组合].Tag.ToString() + this.neuSpread1_Sheet1.Cells[0, (int)EnumColSet.注射时间].Text;

                for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
                {
                    string rowComboAndInjectTime = this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.组合].Tag.ToString() + this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.注射时间].Text;
                    if (comboAndInjectTime != rowComboAndInjectTime)
                    {
                        comboAndInjectTime = rowComboAndInjectTime;
                        orderNo = IGetOrderNo.GetSamePatientNextOrderNo(orderNo);
                    }
                    this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.注射序号].Text = orderNo;
                }
                return;
            }
            else
            {
                FS.HISFC.Models.Nurse.Inject info = this.InjMgr.QueryLast();
                if (info != null && info.Booker.OperTime != System.DateTime.MinValue)
                {
                    if (info.Booker.OperTime.Date == this.InjMgr.GetDateTimeFromSysDateTime().Date)
                    {
                        this.txtOrder.Text = (FS.FrameWork.Function.NConvert.ToInt32(info.OrderNO) + 1).ToString();
                    }
                    else
                    {
                        this.txtOrder.Text = "1";
                    }
                }
                else
                {
                    this.txtOrder.Text = "1";
                }

                for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
                {
                    this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.注射序号].Text = this.txtOrder.Text;
                }
            }
        }

        /// <summary>
        /// 创建接口
        /// </summary>
        private void CreateInterface()
        {
            if (this.IGetOrderNo == null)
            {
                this.IGetOrderNo = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Nurse.IGetInjectOrderNo)) as FS.HISFC.BizProcess.Interface.Nurse.IGetInjectOrderNo;
            }
        }
        #endregion

        #region 方法

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnSave(object sender, object neuObject)
        {
            return this.Save();
        }

        /// <summary>
        /// 确认保存( 1.met_nuo_inject插入记录  2.fin_ipb_feeitemlist更新已确认院注数量，确认标志)
        /// </summary>
        private int Save()
        {
            if (this.neuSpread1_Sheet1.Rows.Count <= 0)
            {
                MessageBox.Show("没有要保存的数据", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return -1;
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
                return -1;
            }

            alInject = new ArrayList();
            alPrint = new ArrayList();

            if (this.isUserOrderNumber)
            {
                #region 判断输入队列号的有效性
                if (this.txtOrder.Text == null || this.txtOrder.Text.Trim().ToString() == "")
                {
                    MessageBox.Show("没有输入队列顺序号!");
                    this.txtOrder.Focus();
                    return -1;
                }
                else if (this.InjMgr.QueryInjectOrder(this.txtOrder.Text.Trim().ToString()).Count > 0)
                {
                    if (MessageBox.Show("该序列号已经使用,是否继续!", "提示", System.Windows.Forms.MessageBoxButtons.YesNo,
                        System.Windows.Forms.MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                    {
                        this.txtOrder.Focus();
                        return -1;
                    }
                }
                #endregion


                #region 检查注射顺序号的有效性（组号相同的，注射顺序号也必须相同）
                for (int i = 1; i < this.neuSpread1_Sheet1.RowCount; i++)
                {
                    if (this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.组合].Tag != null
                        && this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.组合].Tag.ToString() != "" &&
                        //{24A47206-F111-4817-A7B4-353C21FC7724} 患者可以登记全天所有注射处方
                        this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.组合].Tag.ToString() + this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.注射时间].Text == this.neuSpread1_Sheet1.Cells[i - 1, (int)EnumColSet.组合].Tag.ToString() + this.neuSpread1_Sheet1.Cells[i - 1, (int)EnumColSet.注射时间].Text
                        && this.neuSpread1_Sheet1.GetValue(i, 1).ToString() != this.neuSpread1_Sheet1.GetValue(i - 1, 1).ToString()
                        )
                    {
                        MessageBox.Show("相同组号的注射顺序号必须相同!", "第" + (i + 1).ToString() + "行");
                        return -1;
                    }
                }
                #endregion
            }

            #region 检查院注次数的有效性（组号相同的，注射顺序号也必须相同）
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                string strnum = this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.院注次数].Text;
                if (strnum == null || strnum == "")
                {
                    MessageBox.Show("院注次数不能为空!", "第" + (i + 1).ToString() + "行");
                    return -1;
                }
                string completenum = this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.确认次数].Text;
                if (this.neuSpread1_Sheet1.GetValue(i, 0).ToString().ToUpper() == "TRUE")
                {
                    if (FS.FrameWork.Function.NConvert.ToInt32(strnum) == 0)
                    {
                        continue;
                    }

                    if (FS.FrameWork.Function.NConvert.ToInt32(strnum) <= FS.FrameWork.Function.NConvert.ToInt32(completenum))
                    {
                        MessageBox.Show("院注次数已满!", "第" + (i + 1).ToString() + "行");
                        return -1;
                    }
                }
                //if (this.fpSpread1_Sheet1.Cells[i, (int)EnumColSet.组合].Tag != null && this.fpSpread1_Sheet1.Cells[i, (int)EnumColSet.组合].Tag.ToString() != "" &&
                //    this.fpSpread1_Sheet1.Cells[i, (int)EnumColSet.组合].Tag.ToString() == this.fpSpread1_Sheet1.Cells[i - 1, (int)EnumColSet.组合].Tag.ToString()
                //    && this.fpSpread1_Sheet1.GetValue(i, 2).ToString() != this.fpSpread1_Sheet1.GetValue(i - 1, 2).ToString()
                //    )
                //{
                //    MessageBox.Show("相同组号的院注次数必须相同!", "第" + (i + 1).ToString() + "行");
                //    return -1;
                //}
            }
            #endregion

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            try
            {
                this.InjMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                this.feeIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                this.phaIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                this.inteMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                this.inteMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                DateTime confirmDate = this.InjMgr.GetDateTimeFromSysDateTime();

                FS.HISFC.Models.Fee.Outpatient.FeeItemList detail = null;
                FS.HISFC.Models.Nurse.Inject info = null;

                for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
                {
                    if (this.neuSpread1_Sheet1.GetText(i, 0).ToUpper() == "FALSE" ||
                        this.neuSpread1_Sheet1.GetText(i, 0) == "")
                    {
                        continue;
                    }
                    detail = (FS.HISFC.Models.Fee.Outpatient.FeeItemList)this.neuSpread1_Sheet1.Rows[i].Tag;

                    //					#region 判断是否需要打印注射单
                    //					if(detail.ConfirmedInject == 0)
                    //					{
                    //						IsFirstTime = true;
                    //						countInject = detail.InjectCount;
                    //					}
                    //					#endregion

                    info = new FS.HISFC.Models.Nurse.Inject();

                    #region 实体转化（门诊项目收费明细实体FeeItemList－->注射实体Inject）

                    info.Patient = myRegInfo;
                    info.Patient.ID = detail.Patient.ID;
                    info.Patient.Name = myRegInfo.Name;
                    info.Patient.Sex.ID = myRegInfo.Sex.ID;
                    info.Patient.Birthday = myRegInfo.Birthday;
                    info.Patient.PID.CardNO = myRegInfo.PID.CardNO;

                    info.Item = detail;
                    info.Item.ID = detail.Item.ID;
                    info.Item.Name = detail.Item.Name;

                    info.Item.InjectCount = FS.FrameWork.Function.NConvert.ToInt32(this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.院注次数].Text);

                    info.Item.Order.DoctorDept.Name = deptHelper.GetName(detail.RecipeOper.Dept.ID);
                    info.Item.Order.DoctorDept.ID = detail.RecipeOper.Dept.ID;

                    info.Item.Order.ReciptDoctor.Name = doctHelper.GetName(detail.RecipeOper.ID);
                    info.Item.Order.ReciptDoctor.ID = detail.RecipeOper.ID;
                    //是否皮试
                    if (this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.皮试].Tag.ToString().ToUpper() == "TRUE")
                    {
                        info.Hypotest = FS.HISFC.Models.Order.EnumHypoTest.NeedHypoTest;
                    }
                    else
                    {
                        info.Hypotest = FS.HISFC.Models.Order.EnumHypoTest.FreeHypoTest;
                    }
                    #endregion

                    info.ID = this.InjMgr.GetSequence("Nurse.Inject.GetSeq");

                    info.OrderNO = this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.注射序号].Text;
                    //{24A47206-F111-4817-A7B4-353C21FC7724}
                    info.PrintNo = detail.User02;
                    info.Item.Order.Combo.ID = this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.组合].Tag.ToString();
                    info.Booker.ID = FS.FrameWork.Management.Connection.Operator.ID;
                    info.Booker.OperTime = confirmDate;
                    info.Item.ExecOper.ID = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID;
                    string strOrder = "";
                    if (this.neuSpread1_Sheet1.GetValue(i, 1) == null || this.neuSpread1_Sheet1.GetValue(i, 1).ToString() == "")
                    {
                        strOrder = "";
                    }
                    else
                    {
                        strOrder = this.neuSpread1_Sheet1.GetValue(i, 1).ToString();
                    }
                    info.InjectOrder = strOrder;
                    info.Item.Days = detail.Days;
                    string hypoTest = this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.皮试].Text;

                    //备注--(取医嘱备注)
                    FS.HISFC.Models.Order.OutPatient.Order orderinfo =
                        (FS.HISFC.Models.Order.OutPatient.Order)this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.皮试].Tag;
                    if (orderinfo != null)
                    {
                        //备注应该是Memo+皮试
                        //info.Memo = orderinfo.ExtendFlag1;
                        info.Memo = orderinfo.Memo;
                        info.Hypotest = orderinfo.HypoTest;
                    }

                    #region 向met_nuo_inject中，插入记录
                    if (this.InjMgr.Insert(info) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(this.InjMgr.Err, "提示");
                        return -1;
                    }
                    #endregion

                    #region 向fin_ipb_feeitemlist中，插入数量

                    //更新的院注次数
                    int updateInjectCount = 1;
                    if (isSaveAllInjectCount)
                    {
                        if (isSaveDayInjectCount)
                        {
                            //更新一天的院注
                            updateInjectCount = detail.InjectCount / detail.Order.Frequency.Times.Length;
                        }
                        else
                        {
                            //更新全部院注
                            updateInjectCount = detail.InjectCount;
                        } 
                    }

                    if (this.feeIntegrate.UpdateConfirmInject(detail.Order.ID, detail.RecipeNO, detail.SequenceNO.ToString(), updateInjectCount) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(this.feeIntegrate.Err, "提示");
                        return -1;
                    }
                    #endregion
                    info.Item.InjectCount = info.Item.InjectCount;
                    //打吊瓶的才打印治疗单---先写死-------------此段程序不用,改为由操作员选择是否打印
                    if (info.Item.Order.Usage.ID == "03" ||
                        info.Item.Order.Usage.ID == "04")
                    {
                        alPrint.Add(info);
                    }
                    alInject.Add(info);
                    this.lbLastOrder.Text = "今天最后一次注射号:" + info.OrderNO;

                }
                FS.FrameWork.Management.PublicTrans.Commit();

            }
            catch (Exception e)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(e.Message, "提示");
                return -1;
            }

            if (this.isMessageOnSave)
            {
                MessageBox.Show("保存成功!", "提示");
            }
            this.Clear();
            this.lblWarning.Text = "";

            this.txtCardNo.SelectAll();
            this.txtRecipe.Text = "";
            this.txtCardNo.Text = "";
            this.txtCardNo.Focus();
            return 0;
        }

        /// <summary>
        /// 清空
        /// </summary>
        private void Clear()
        {
            neuSpread1_Sheet1.RowCount = 0;

            this.txtOrder.Text = "";
            this.txtRecipe.Text = "";
            this.lblWarning.Text = "";
            this.txtCardNo.Text = "";
            this.myRegInfo = null;

            this.txtCardNo.Focus();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnQuery(object sender, object neuObject)
        {
            this.Query();
            return 1;
        }

        /// <summary>
        /// 查询
        /// </summary>
        private void Query()
        {
            if (this.neuSpread1_Sheet1.RowCount > 0)
            {
                this.neuSpread1_Sheet1.Rows.Remove(0, this.neuSpread1_Sheet1.RowCount);
            }
            string cardNo = this.txtCardNo.Text.Trim().PadLeft(10, '0');

            //获取医生开立的处方信息（没有全部执行完的）
            DateTime dtFrom = this.dtpStart.Value.Date;
            DateTime dtTo = this.dtpEnd.Value.Date.AddDays(1);
            ArrayList al = new ArrayList();

            if (/*this.txtRecipe.Text == null ||*/ this.txtRecipe.Text.Trim() == "")
            {
                if (isShowSubjob)
                {
                    al = this.feeIntegrate.QueryOutpatientFeeItemListsZsSubjob(cardNo, dtFrom, dtTo);
                }
                else
                {
                    al = this.feeIntegrate.QueryOutpatientFeeItemListsZs(cardNo, dtFrom, dtTo);
                }
            }
            else
            {
                al = this.feeIntegrate.QueryFeeDetailFromRecipeNO(this.txtRecipe.Text.Trim());

                if (al == null)
                {
                    return;
                }

                if (al.Count > 0)
                {
                    FS.HISFC.Models.Fee.Outpatient.FeeItemList item = (FS.HISFC.Models.Fee.Outpatient.FeeItemList)al[0];
                    myRegInfo = this.regMgr.GetByClinic(item.Patient.ID);
                    if (myRegInfo == null || myRegInfo.ID == "")
                    {
                        MessageBox.Show("没有病历号为:" + item.Patient.ID + "的患者!", "提示");

                        this.txtCardNo.Focus();
                        return;
                    }

                    this.txtCardNo.Text = item.Patient.PID.CardNO;
                    this.SetPatient(myRegInfo);
                    this.txtRecipe.Text = "";
                    this.Query();
                    return;
                }

            }

            this.Query(al);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        private void Query(ArrayList al)
        {
            if (al == null || al.Count == 0)
            {
                MessageBox.Show("该患者没有需要确认的医嘱信息!", "提示");
                this.txtCardNo.Focus();
                return;
            }

            this.AddDetail(al);

            if (this.neuSpread1_Sheet1.RowCount <= 0)
            {
                MessageBox.Show("该时间段内没有该患者信息!", "提示");
                this.txtCardNo.Focus();
                return;
            }

            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                int confirmNum = FS.FrameWork.Function.NConvert.ToInt32(this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.确认次数].Text);
                if (confirmNum == 0)
                {
                    this.lblWarning.Text = "首次院注,请核对院注次数!";
                    this.lblWarning.ForeColor = System.Drawing.Color.Magenta;
                }
            }

            this.SelectAll(true);
            this.SetComb();
            this.ShowColor();
            this.txtOrder.Focus();
            if (this.isUserOrderNumber)
            {
                this.SetInject();
            }

            //打印巡视卡
            if (this.isAutoPrint)
            {
                this.PrintInjectScoutCard();
            }

            if (this.isAutoSave)
            {
                this.SelectAll(true);
                this.Save();
            }
        }

        /// <summary>
        /// 参数控制业务层
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Common.ControlParam controlIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        /// <summary>
        /// 添加项目明细
        /// </summary>
        /// <param name="detail"></param>
        private void AddDetail(ArrayList details)
        {
            if (this.neuSpread1_Sheet1.RowCount > 0)
            {
                this.neuSpread1_Sheet1.Rows.Remove(0, this.neuSpread1_Sheet1.RowCount);
            }

            //{24A47206-F111-4817-A7B4-353C21FC7724} 患者可以登记全天所有注射处方
            List<FS.HISFC.Models.Fee.Outpatient.FeeItemList> tmpFeeList = new List<FS.HISFC.Models.Fee.Outpatient.FeeItemList>();
            if (details != null)
            {
                foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList detail in details)
                {
                    #region  非药品不显示
                    //非药品不显示
                    //if (detail.Item.IsPharmacy == false) continue;
                    if (detail.Item.ItemType != HISFC.Models.Base.EnumItemType.Drug)
                    {
                        if (!isShowSubjob)
                        {
                            continue;
                        }
                        else
                        {
                            tmpFeeList.Add(detail);
                            continue;
                        }
                    }
                    #endregion

                    #region 不是院注的不显示
                    if (IsUseSpecialConstant)
                    {
                        //不是院注的不显示-------权益之计
                        FS.HISFC.BizProcess.Integrate.Manager con = new FS.HISFC.BizProcess.Integrate.Manager();
                        FS.FrameWork.Models.NeuObject obj = con.GetConstant("SPECIAL", detail.Order.Usage.ID);
                        if (obj == null || obj.ID == null || obj.ID == "")
                        {
                            continue;
                        }
                    }
                    else
                    {
                        if (!usage.Contains(detail.Order.Usage.Name))
                        {
                            continue;
                        }
                    }
                    #endregion

                    #region  院注次数 <= 已确认院注次数 的不显示

                    //院注次数 <= 已确认院注次数 的不显示
                    if (detail.InjectCount != 0 && detail.InjectCount <= detail.ConfirmedInjectCount && !this.cbFinish.Checked)
                    {
                        continue;
                    }
                    #endregion

                    #region 是否显示0次数的
                    if (!chkNullNum.Checked && detail.InjectCount == 0)
                    {
                        continue;
                    }
                    #endregion

                    #region 今天已经登记的QD不显示，注射两次的BID不显示，当前午别注射一次的BID不再显示。(根据今天的登记时间)
                    DateTime dt = FS.FrameWork.Function.NConvert.ToDateTime(
                        this.InjMgr.GetDateTimeFromSysDateTime().ToString("yyyy-MM-dd 00:00:00"));
                    //{24A47206-F111-4817-A7B4-353C21FC7724} 患者可以登记全天所有注射处方
                    ArrayList alTodayInject = this.InjMgr.Query(detail.Patient.PID.CardNO, detail.RecipeNO, detail.SequenceNO.ToString(), dt);

                    FS.HISFC.Models.Order.Frequency frequence = this.freqHelper.GetObjectFromID(detail.Order.Frequency.ID) as FS.HISFC.Models.Order.Frequency;

                    string[] injectTime = frequence.Time.Split('-');
                    //当天的已经全部注射完毕后跳过
                    if (alTodayInject.Count >= injectTime.Length)
                    {
                        continue;
                    }
                    if (this.isShowAllInject)
                    {
                        for (int i = alTodayInject.Count; i < injectTime.Length; i++)
                        {
                            FS.HISFC.Models.Fee.Outpatient.FeeItemList newDetail = detail.Clone();
                            newDetail.User03 = injectTime[i];
                            tmpFeeList.Add(newDetail);
                        }
                    }
                    else
                    {
                        //未过上次注射时间的话不允许再次登记
                        if (alTodayInject.Count > 0)
                        {
                            DateTime lastInjectTime = FrameWork.Function.NConvert.ToDateTime(dt.ToString("yyyy-MM-dd ") + injectTime[alTodayInject.Count - 1] + ":00");
                            if (this.InjMgr.GetDateTimeFromSysDateTime() < lastInjectTime)
                            {
                                continue;
                            }
                        }
                        detail.User03 = injectTime[alTodayInject.Count];
                        tmpFeeList.Add(detail);
                    }
                    //if (detail.Order.Frequency.ID == "QD")
                    //{
                    //    ArrayList alTemp = this.InjMgr.Query(detail.Patient.PID.CardNO, detail.RecipeNO,
                    //        detail.SequenceNO.ToString(), dt);
                    //    if (alTemp != null)
                    //    {
                    //        if (alTemp.Count > 0) continue;
                    //    }
                    //}
                    //if (detail.Order.Frequency.ID == "BID")
                    //{
                    //    ArrayList alTemp = this.InjMgr.Query(detail.Patient.PID.CardNO, detail.RecipeNO,
                    //        detail.SequenceNO.ToString(), dt);
                    //    if (alTemp != null && alTemp.Count > 0)
                    //    {
                    //        if (alTemp.Count >= 2) continue;
                    //        //当前午别注射一次的BID不再显示
                    //        FS.HISFC.Models.Nurse.Inject item = (FS.HISFC.Models.Nurse.Inject)alTemp[0];
                    //        bool bl1 = true;
                    //        bool bl2 = true;
                    //        if (FS.FrameWork.Function.NConvert.ToInt32(item.Booker.OperTime.ToString("HH")) > 12) bl1 = false;
                    //        if (FS.FrameWork.Function.NConvert.ToInt32(this.InjMgr.GetDateTimeFromSysDateTime().ToString("HH")) > 12) bl2 = false;
                    //        if (bl1 == bl2) continue;
                    //    }
                    //}
                    #endregion
                    //{24A47206-F111-4817-A7B4-353C21FC7724} 患者可以登记全天所有注射处方
                    //this.AddDetail(detail);
                }

                //排序
                tmpFeeList.Sort(new FeeItemListSort());
                //获取打印序号
                this.CreateInterface();
                if (this.IGetOrderNo != null)
                {
                    this.IGetOrderNo.SetPrintNo(new ArrayList(tmpFeeList.ToArray()));
                }
                foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItem in tmpFeeList)
                {
                    this.AddDetail(feeItem);
                }
            }
        }

        FarPoint.Win.Spread.CellType.TextCellType tcell = new FarPoint.Win.Spread.CellType.TextCellType();

        /// <summary>
        /// 添加明细
        /// </summary>
        /// <param name="detail"></param>
        private void AddDetail(FS.HISFC.Models.Fee.Outpatient.FeeItemList info)
        {
            this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 1);
            int row = this.neuSpread1_Sheet1.RowCount - 1;
            this.neuSpread1_Sheet1.Rows[row].Tag = info;

            #region "窗口赋值"

            #region 获取皮试信息

            string strTest = "否";

            if (info.Item.ID != "999" && info.Item.ItemType == HISFC.Models.Base.EnumItemType.Drug)
            {
                //获取皮试信息
                drug = this.phaIntegrate.GetItem(info.Item.ID);
                if (drug == null)
                {
                    MessageBox.Show("获取药品皮试信息失败!\r\n" + phaIntegrate.Err, "错误", MessageBoxButtons.OK);
                    this.neuSpread1_Sheet1.Rows.Remove(0, this.neuSpread1_Sheet1.RowCount);
                    return;
                }
                if (drug.IsAllergy)
                {
                    strTest = "是";
                }
            }
            else
            {
                drug = null;
            }
            //
            #endregion

            info.Order.DoctorDept.Name = deptHelper.GetName(info.RecipeOper.Dept.ID);

            this.neuSpread1_Sheet1.SetValue(row, 1, "", false);//注射顺序号

            if (info.Item.ItemType != FS.HISFC.Models.Base.EnumItemType.Drug)
            {
                //this.neuSpread1_Sheet1.Cells[row, 0].CellType = tcell;
                //this.neuSpread1_Sheet1.Cells[row, 0].Text = " ";
                //this.neuSpread1_Sheet1.Cells[row, 0].Value = " ";
                this.neuSpread1_Sheet1.Rows[row].BackColor = Color.Silver;
                this.neuSpread1_Sheet1.SetValue(row, 0, false);
                this.neuSpread1_Sheet1.Rows[row].Locked = true;
            }

            this.neuSpread1_Sheet1.SetValue(row, 2, info.InjectCount.ToString(), false);//院注次数
            this.neuSpread1_Sheet1.SetValue(row, 3, info.ConfirmedInjectCount.ToString(), false);//已经确认的院注次数
            this.neuSpread1_Sheet1.SetValue(row, 4, doctHelper.GetName(info.RecipeOper.ID), false);//开单医生
            this.neuSpread1_Sheet1.Cells[row, (int)EnumColSet.开单医生].Tag = info.Order.ReciptDoctor.ID;
            this.neuSpread1_Sheet1.SetValue(row, 5, info.Order.DoctorDept.Name, false);//科别
            this.neuSpread1_Sheet1.Cells[row, (int)EnumColSet.科别].Tag = info.Order.DoctorDept.ID;
            this.neuSpread1_Sheet1.SetValue(row, 6, info.Item.Name, false);//药品名称
            this.neuSpread1_Sheet1.Cells[row, (int)EnumColSet.组合].Tag = info.Order.Combo.ID;//组合号
            this.neuSpread1_Sheet1.SetValue(row, 8, info.Order.DoseOnce.ToString() + info.Order.DoseUnit, false);//每次量
            this.neuSpread1_Sheet1.SetValue(row, 9, info.Order.Frequency.ID, false);//频次
            this.neuSpread1_Sheet1.Cells[row, (int)EnumColSet.频次].Tag = info.Order.Frequency.ID.ToString();
            this.neuSpread1_Sheet1.SetValue(row, 10, info.Order.Usage.Name, false);//用法
            this.neuSpread1_Sheet1.SetValue(row, 11, strTest, false);//皮试？
            if (drug != null)
            {
                this.neuSpread1_Sheet1.Cells[row, (int)EnumColSet.皮试].Tag = drug.IsAllergy.ToString().ToUpper();
            }
            else
            {
                this.neuSpread1_Sheet1.Cells[row, (int)EnumColSet.皮试].Tag = "false".ToUpper();
                this.neuSpread1_Sheet1.SetText(row, 12, "共" + info.Item.Qty * info.Days + info.Item.PriceUnit);
            }

            FS.HISFC.Models.Order.OutPatient.Order orderinfo = new FS.HISFC.Models.Order.OutPatient.Order();

            orderinfo = orderMgr.GetOneOrder(info.Patient.ID, info.Order.ID);
            if (orderinfo != null)
            {
                this.neuSpread1_Sheet1.SetText(row, 12, string.IsNullOrEmpty(orderinfo.Memo) ? " " : orderinfo.Memo);

                //if (orderinfo.HypoTest == 1)
                //{
                //    if (drug != null && !drug.IsAllergy)
                //    {
                //        orderinfo.HypoTest = 0;//免试的值为零
                //    }
                //    else
                //    {
                //        orderinfo.HypoTest = 0;
                //    }
                //}
                this.neuSpread1_Sheet1.Cells[row, (int)EnumColSet.皮试].Text = this.orderMgr.TransHypotest(orderinfo.HypoTest);
                this.neuSpread1_Sheet1.Cells[row, (int)EnumColSet.皮试].Tag = orderinfo;
            }
            else
            {
                orderinfo = new FS.HISFC.Models.Order.OutPatient.Order();
                if (drug != null && drug.IsAllergy)
                {
                    orderinfo.Item = drug;
                    orderinfo.HypoTest = FS.HISFC.Models.Order.EnumHypoTest.NoHypoTest;
                }
                else
                {
                    orderinfo.HypoTest = 0;
                }

                this.neuSpread1_Sheet1.Cells[row, (int)EnumColSet.皮试].Text = this.orderMgr.TransHypotest(orderinfo.HypoTest);
                this.neuSpread1_Sheet1.Cells[row, (int)EnumColSet.皮试].Tag = orderinfo;

            }
            //{24A47206-F111-4817-A7B4-353C21FC7724} 患者可以登记全天所有注射处方
            this.neuSpread1_Sheet1.Cells[row, (int)EnumColSet.注射时间].Text = info.User03;


            //显示摆药人信息
            if (!string.IsNullOrEmpty(info.ConfirmOper.ID))
            {
                //applyOutObj = drugMgr.GetApplyOut(info.RecipeNO, info.SequenceNO);
                //if (applyOutObj == null)
                //{
                //    MessageBox.Show("获取摆药人信息失败:" + drugMgr.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //}

                //if (hsEmpl.Contains(applyOutObj.Operation.ApproveOper.ID))
                //{
                //    empl = hsEmpl[applyOutObj.Operation.ApproveOper.ID] as FS.HISFC.Models.Base.Employee;
                //}
                //{
                //    empl = this.inteMgr.GetEmployeeInfo(applyOutObj.Operation.ApproveOper.ID);
                //    if (empl == null)
                //    {
                //        MessageBox.Show("获取摆药人信息失败:" + inteMgr.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    }
                //    else
                //    {
                //        this.neuSpread1_Sheet1.Cells[row, 17].Text = empl.Name;
                //    }
                //}
            }

            #endregion
        }

        /// <summary>
        /// 设置组合号
        /// </summary>
        private void SetComb()
        {
            int myCount = this.neuSpread1_Sheet1.RowCount;
            int i;
            //第一行
            this.neuSpread1_Sheet1.SetValue(0, 7, "┓");
            //最后行
            this.neuSpread1_Sheet1.SetValue(myCount - 1, 7, "┛");
            //中间行
            for (i = 1; i < myCount - 1; i++)
            {
                int prior = i - 1;
                int next = i + 1;
                string currentRowCombNo = this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.组合].Tag.ToString();
                string priorRowCombNo = this.neuSpread1_Sheet1.Cells[prior, (int)EnumColSet.组合].Tag.ToString();
                string nextRowCombNo = this.neuSpread1_Sheet1.Cells[next, (int)EnumColSet.组合].Tag.ToString();

                //{24A47206-F111-4817-A7B4-353C21FC7724} 患者可以登记全天所有注射处方
                string currentRowInjectTime = this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.注射时间].Text.ToString();
                string priorRowInjectTime = this.neuSpread1_Sheet1.Cells[prior, (int)EnumColSet.注射时间].Text.ToString();
                string nextRowInjectTime = this.neuSpread1_Sheet1.Cells[next, (int)EnumColSet.注射时间].Text.ToString();

                #region """""
                bool bl1 = true;
                bool bl2 = true;
                //{24A47206-F111-4817-A7B4-353C21FC7724} 患者可以登记全天所有注射处方
                if (currentRowCombNo + currentRowInjectTime != priorRowCombNo + priorRowInjectTime)
                    bl1 = false;
                if (currentRowCombNo + currentRowInjectTime != nextRowCombNo + nextRowInjectTime)
                    bl2 = false;
                //  ┃
                if (bl1 && bl2)
                {
                    this.neuSpread1_Sheet1.SetValue(i, 7, "┃");
                }
                //  ┛
                if (bl1 && !bl2)
                {
                    this.neuSpread1_Sheet1.SetValue(i, 7, "┛");
                }
                //  ┓
                if (!bl1 && bl2)
                {
                    this.neuSpread1_Sheet1.SetValue(i, 7, "┓");
                }
                //  ""
                if (!bl1 && !bl2)
                {
                    this.neuSpread1_Sheet1.SetValue(i, 7, "");
                }
                #endregion
            }
            //把没有组号的去掉
            for (i = 0; i < myCount; i++)
            {
                if (this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.组合].Tag.ToString() == "")
                {
                    this.neuSpread1_Sheet1.SetValue(i, 7, "");
                }
            }
            //判断首末行 有组号，且只有自己一组数据的情况
            if (myCount == 1)
            {
                this.neuSpread1_Sheet1.SetValue(0, 7, "");
            }
            //只有首末两行，那么还要判断组号啊
            if (myCount == 2)
            {
                if (this.neuSpread1_Sheet1.Cells[0, (int)EnumColSet.组合].Tag.ToString().Trim() != this.neuSpread1_Sheet1.Cells[1, (int)EnumColSet.组合].Tag.ToString().Trim())
                {
                    this.neuSpread1_Sheet1.SetValue(0, 7, "");
                    this.neuSpread1_Sheet1.SetValue(1, 7, "");
                }
                //防止一个药bid组合在一起
                if (this.neuSpread1_Sheet1.Cells[0, (int)EnumColSet.注射时间].Text.ToString().Trim() != this.neuSpread1_Sheet1.Cells[1, (int)EnumColSet.注射时间].Text.ToString().Trim())
                {
                    this.neuSpread1_Sheet1.SetValue(0, 7, "");
                    this.neuSpread1_Sheet1.SetValue(1, 7, "");
                }
            }
            if (myCount > 2)
            {
                if (this.neuSpread1_Sheet1.GetValue(1, 7).ToString() != "┃"
                    && this.neuSpread1_Sheet1.GetValue(1, 7).ToString() != "┛")
                {
                    this.neuSpread1_Sheet1.SetValue(0, 7, "");
                }
                if (this.neuSpread1_Sheet1.GetValue(myCount - 2, 7).ToString() != "┃"
                    && this.neuSpread1_Sheet1.GetValue(myCount - 2, 7).ToString() != "┓")
                {
                    this.neuSpread1_Sheet1.SetValue(myCount - 1, 7, "");
                }
            }

        }

        /// <summary>
        /// 打印
        /// </summary>
        private void Print()
        {
            if (this.alPrint == null || this.alPrint.Count <= 0)
            {
                MessageBox.Show("没有需要打印的数据!");
                return;
            }

            //打印贴凭卡
            Nurse.Print.ucPrintCure uc = new Nurse.Print.ucPrintCure();
            uc.Init(alPrint);

            //打印注射单
            Nurse.Print.ucPrintInject uc2 = new Nurse.Print.ucPrintInject();
            uc2.Init(alInject);

            alPrint.Clear();
            alInject.Clear();
        }

        private void SelectedComb(bool isSelect)
        {
            int row = this.neuSpread1_Sheet1.ActiveRowIndex;
            string combID = this.neuSpread1_Sheet1.Cells[row, (int)EnumColSet.组合].Tag.ToString();
            //{24A47206-F111-4817-A7B4-353C21FC7724} 患者可以登记全天所有注射处方
            string injectTime = this.neuSpread1_Sheet1.Cells[row, (int)EnumColSet.注射时间].Text;
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                //{24A47206-F111-4817-A7B4-353C21FC7724} 患者可以登记全天所有注射处方
                if (this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.组合].Tag.ToString() == combID && this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.注射时间].Text == injectTime)
                {
                    this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.选择].Value = isSelect;
                }
            }

        }

        /// <summary>
        /// 修改皮试信息
        /// </summary>
        private void ModifyHytest()
        {
            ArrayList al = new ArrayList();
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                bool isSelected = FS.FrameWork.Function.NConvert.ToBoolean(this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.选择].Value);
                if (isSelected)
                {
                    FS.HISFC.Models.Order.OutPatient.Order orderinfo = this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.皮试].Tag as FS.HISFC.Models.Order.OutPatient.Order;
                    if (orderinfo.HypoTest == FS.HISFC.Models.Order.EnumHypoTest.FreeHypoTest)
                    {
                        continue;
                    }
                    al.Add(orderinfo);
                }

            }

            if (al.Count == 0)
            {
                return;
            }
            Forms.frmHypoTest frmHypoTest = new FS.HISFC.Components.Nurse.Forms.frmHypoTest();
            frmHypoTest.AlOrderList = al;
            DialogResult d = frmHypoTest.ShowDialog();
            if (d == DialogResult.OK)
            {
                for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
                {
                    bool isSelected = FS.FrameWork.Function.NConvert.ToBoolean(this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.选择].Value);
                    if (!isSelected)
                    {
                        continue;
                    }
                    FS.HISFC.Models.Order.OutPatient.Order orderinfo = this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.皮试].Tag as FS.HISFC.Models.Order.OutPatient.Order;
                    this.neuSpread1_Sheet1.Cells[i, (int)EnumColSet.皮试].Text = this.orderMgr.TransHypotest(orderinfo.HypoTest);

                }
            }
        }

        #endregion

        #region 事件

        /// <summary>
        /// 按卡号查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCardNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtRecipe.Text = string.Empty;
                if (this.txtCardNo.Text.Trim() == "")
                {
                    MessageBox.Show("请输入病历号!", "提示");
                    this.txtCardNo.Focus();
                    return;
                }

                HISFC.Models.Account.AccountCard objCard = new FS.HISFC.Models.Account.AccountCard();
                string strCardNO = this.txtCardNo.Text.Trim();
                
                int iTemp = this.feeIntegrate.ValidMarkNO(strCardNO, ref objCard);
                if (iTemp <= 0 || objCard == null)
                {
                    MessageBox.Show("无效卡号，请联系管理员！");
                    return;
                }
                this.Clear();

                string cardNo = objCard.Patient.PID.CardNO;
                ArrayList alRegs = this.regMgr.Query(cardNo, this.dtpStart.Value.AddDays(0 - queryRegDays));
                if (alRegs == null || alRegs.Count == 0)
                {
                    MessageBox.Show("没有病历号为:" + cardNo + "的患者!", "提示");
                    this.txtCardNo.Focus();
                    return;
                }
                myRegInfo = alRegs[0] as FS.HISFC.Models.Registration.Register;
                if (myRegInfo == null || myRegInfo.ID == "")
                {
                    MessageBox.Show("没有病历号为:" + cardNo + "的患者!", "提示");

                    this.txtCardNo.Focus();
                    return;
                }

                this.txtCardNo.Text = cardNo;
                this.SetPatient(myRegInfo);

                //分解注射项目
                //if (al != null)
                //{
                this.Query();
                //}
                this.txtCardNo.Focus();
                this.txtRecipe.SelectAll();
            }
        }

        /// <summary>
        /// 按处方号查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtRecipe_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txtCardNo.Text = string.Empty;
                if (this.txtRecipe.Text.Trim() == "")
                {
                    this.txtRecipe.Focus();
                    return;
                }
                this.Query();
                if (this.isUserOrderNumber)
                {
                    this.SetInject();
                }
                this.txtRecipe.Focus();
                this.txtRecipe.SelectAll();
            }
        }

        /// <summary>
        /// 打印序号跳转
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtOrder_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtCardNo.Focus();
            }
        }

        #region 先作废

        ///// <summary>
        ///// 快捷键设置
        ///// </summary>
        ///// <param name="keyData"></param>
        ///// <returns></returns>
        //protected override bool ProcessDialogKey(Keys keyData)
        //{
        //    int altKey = Keys.Alt.GetHashCode();

        //    if (keyData == Keys.F1)
        //    {
        //        this.SelectAll(true);
        //        return true;
        //    }
        //    if (keyData == Keys.F2)
        //    {
        //        this.SelectAll(false);
        //        return true;
        //    }
        //    if (keyData.GetHashCode() == altKey + Keys.S.GetHashCode())
        //    {
        //        if (this.Save() == 0)
        //        {
        //            this.Print();
        //        }
        //        return true;
        //    }
        //    if (keyData.GetHashCode() == altKey + Keys.Q.GetHashCode())
        //    {
        //        this.Query();
        //        return true;
        //    }
        //    if (keyData.GetHashCode() == altKey + Keys.P.GetHashCode())
        //    {
        //        //
        //        return true;
        //    }
        //    if (keyData.GetHashCode() == altKey + Keys.X.GetHashCode())
        //    {
        //        this.FindForm().Close();
        //        return true;
        //    }
        //    return base.ProcessDialogKey(keyData);
        //}
        #endregion

        private void neuSpread1_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            int row = this.neuSpread1_Sheet1.ActiveRowIndex;
            bool isSelect = Convert.ToBoolean(this.neuSpread1_Sheet1.Cells[row, (int)EnumColSet.选择].Value);
            this.SelectedComb(isSelect);
        }

        /// <summary>
        /// 保存显示设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void neuSpread1_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnFormatProperty(this.neuSpread1_Sheet1, injectRegisterXml);
        }

        #endregion

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

        /// <summary>
        /// 列设置枚举
        /// </summary>
        enum EnumColSet
        {
            选择,
            序号,
            院注次数,
            确认次数,
            开单医生,
            科别,
            医嘱,
            组合,
            每次用量,
            频次,
            用法,
            皮试,
            医嘱备注,
            注射时间,
            注射序号,
            发药人
        }
    }

    /// <summary>
    /// 排序
    /// 患者可以登记全天所有注射处方
    /// </summary>
    public class FeeItemListSort : IComparer<FS.HISFC.Models.Fee.Outpatient.FeeItemList>
    {
        public int Compare(FS.HISFC.Models.Fee.Outpatient.FeeItemList x, FS.HISFC.Models.Fee.Outpatient.FeeItemList y)
        {
            //先按照处方排序
            if (x.RecipeNO != y.RecipeNO)
            {
                return -y.RecipeNO.CompareTo(x.RecipeNO);
            }
            //按注射时间排序
            if (x.User03 != y.User03)
            {
                string a = x.User03;
                string b = y.User03;
                if (a.Length < "12:00".Length)
                {
                    a = a.PadLeft("12:00".Length, '0');
                }
                if (b.Length < "12:00".Length)
                {
                    b = b.PadLeft("12:00".Length, '0');
                }
                return -b.CompareTo(a);
            }

            //按组合号排序
            if (x.Order.Combo.ID != y.Order.Combo.ID)
            {
                return -y.Order.Combo.ID.CompareTo(x.Order.Combo.ID);
            }
            //处方内序号
            if (x.SequenceNO != y.SequenceNO)
            {
                return -y.SequenceNO.CompareTo(x.SequenceNO);
            }
            //药品编码
            if (x.Item.ID != y.Item.ID)
            {
                return -y.Item.ID.CompareTo(x.Item.ID);
            }
            return 0;
        }
    }
}
