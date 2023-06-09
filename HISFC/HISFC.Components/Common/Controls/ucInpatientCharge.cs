using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FarPoint.Win.Spread;
using FS.FrameWork.Management;
using FS.HISFC.Models.Fee.Inpatient;
using FS.HISFC.Models.RADT;
using FS.HISFC.Models.Base;
using FS.HISFC.Models.Order;
using FS.FrameWork.Function;

namespace FS.HISFC.Components.Common.Controls
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ucInpatientCharge : FS.FrameWork.WinForms.Controls.ucBaseControl,FS.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        public ucInpatientCharge()
        {
            InitializeComponent();
        }

        #region 变量

        /// <summary>
        /// 项目列表
        /// </summary>
        private ucItemList ucItemList = null;

        
        /// <summary>
        /// 执行科室选择列表
        /// </summary>
        private FS.FrameWork.WinForms.Controls.PopUpListBox lbDept = new FS.FrameWork.WinForms.Controls.PopUpListBox();

        /// <summary>
        /// 加载项目类别
        /// </summary>
        private EnumShowItemType itemKind;

        /// <summary>
        /// 不加载的药品性质// {4D67D981-6763-4ced-814E-430B518304E2}
        /// </summary>
        private string noAddDrugQuality = "";

        /// <summary>
        /// 控件功能
        /// </summary>
        private FeeTypes feeType;

        /// <summary>
        /// 当前行
        /// </summary>
        private int rowCount;
      
        /// <summary>
        /// 如果正在进行小计运算，所有触发事件选择性返回
        /// </summary>
        private bool isSubTotal;

        /// <summary>
        /// 成功提示信息
        /// </summary>
        private string sucessMsg = string.Empty;

        /// <summary>
        /// 是否允许划价金额为0的项目
        /// </summary>
        private bool isChargeZero;

        /// <summary>
        /// 默认执行科室
        /// </summary>
        private string defaultExeDept;

        /// <summary>
        /// 当前药品申请科室，保证组套和项目选择列表一致，by cube
        /// </summary>
        private string curDrugApplyDept = "";

        /// <summary>
        /// 开方医生
        /// </summary>
        private string recipeDoctCode;

        /// <summary>
        /// 是否验证及时停用标记
        /// </summary>
        private bool isJudgeValid = false;

        /// <summary>
        /// 患者基本信息实体
        /// </summary>
        private FS.HISFC.Models.RADT.PatientInfo patientInfo = new FS.HISFC.Models.RADT.PatientInfo();

        /// <summary>
        /// 医疗保险,公费接口类
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy medcareInterface = null;

        /// <summary>
        /// 住院费用业务层
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.InPatient inpatientManager = new FS.HISFC.BizLogic.Fee.InPatient();

        /// <summary>
        /// 住院管理业务层
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// 非药品业务层
        /// </summary>
        private FS.HISFC.BizLogic.Fee.Item undrugManager = new FS.HISFC.BizLogic.Fee.Item();

        /// <summary>
        /// 科室业务层
        /// </summary>
        private FS.HISFC.BizLogic.Manager.Department departmentManager = new FS.HISFC.BizLogic.Manager.Department();

        /// <summary>
        /// 组套业务层
        /// </summary>
        private FS.HISFC.BizLogic.Manager.ComGroupTail groupDetailManager = new FS.HISFC.BizLogic.Manager.ComGroupTail();

        /// <summary>
        /// 人员信息业务层
        /// </summary>
        private FS.HISFC.BizLogic.Manager.Person personManager = new FS.HISFC.BizLogic.Manager.Person();

        /// <summary>
        /// 合同单位业务层
        /// </summary>
        private FS.HISFC.BizLogic.Fee.PactUnitInfo pactUnitManager = new FS.HISFC.BizLogic.Fee.PactUnitInfo();

        ///// <summary>
        ///// 非药品组合项目业务层
        ///// </summary>
        private FS.HISFC.BizLogic.Fee.UndrugPackAge undrugPackageManager = new FS.HISFC.BizLogic.Fee.UndrugPackAge();

        /// <summary>
        /// 非药品项目业务层
        /// </summary>
        private FS.HISFC.BizLogic.Fee.Item itemManager = new FS.HISFC.BizLogic.Fee.Item();

        /// <summary>
        /// 药品综合业务层
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Pharmacy pharmacyIntergrate = new FS.HISFC.BizProcess.Integrate.Pharmacy();

        /// <summary>
        /// 费用综合业务层
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Fee feeIntergrate = new FS.HISFC.BizProcess.Integrate.Fee();

        /// <summary>
        /// 医嘱业务层
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Order orderIntegrate = new FS.HISFC.BizProcess.Integrate.Order();
        /// <summary>
        /// 医嘱逻辑层
        /// </summary>
        protected FS.HISFC.BizLogic.Order.Order orderManager = new FS.HISFC.BizLogic.Order.Order();
        /// <summary>
        /// 是否判断欠费，如何提示
        /// </summary>
        private FS.HISFC.Models.Base.MessType messtype = FS.HISFC.Models.Base.MessType.Y;
        FS.HISFC.Models.Base.Employee operObj = null;
        /// <summary>
        /// 处方科室
        /// </summary>
        private FS.FrameWork.Models.NeuObject recipeDept = null;
        private bool isJudgeQty = true; //是否判断数量
        private bool defaultExeDeptIsDeptIn = false;

        /// <summary>
        /// 本次收费项目集合
        /// </summary>
        private List<FeeItemList> feeItemCollection = new List<FeeItemList>();
        //{062CEAA8-16B8-4c25-B4CC-E6B24DE7D331}
        private HISFC.BizProcess.Interface.FeeInterface.IAdptIllnessInPatient IAdptIllnessInPatient = null;

        /// <summary>
        /// 当前物资的扣库科室
        /// </summary>
        private FrameWork.Models.NeuObject tempDept = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// 是否拆分复合项目到界面上{F4912030-EF65-4099-880A-8A1792A3B449}
        /// </summary>
        private bool isSplitUndrugCombItem = false;
        //{F4912030-EF65-4099-880A-8A1792A3B449} 结束

        /// <summary>
        /// 是否显示比例项{2C7FCD3D-D9B4-44f5-A2EE-A7E8C6D85576}
        /// </summary>
        private bool isShowFeeRate = false;

      
        //{CA82280B-51B6-4462-B63E-43F4ECF456A3}
        ArrayList unDrugDeptList = new ArrayList();
        Dictionary<string, FS.FrameWork.Models.NeuObject> dictDept = new Dictionary<string, FS.FrameWork.Models.NeuObject>();

        /// <summary>
        /// 是否弹出发票分类
        /// </summary>
        private bool isPopSHowInvoice = false;

        /// <summary>
        /// 最小费用显示相关
        /// </summary>
        ArrayList allFeecodeStat = new ArrayList();
        Dictionary<string, FS.HISFC.Models.Fee.FeeCodeStat> allFeecodeStatHelper = new Dictionary<string, FS.HISFC.Models.Fee.FeeCodeStat>();
        private FS.HISFC.BizLogic.Fee.FeeCodeStat feeCodeStatMgr = new FS.HISFC.BizLogic.Fee.FeeCodeStat();


        #endregion

        #region 属性
        /// <summary>
        /// 是否拆分复合项目到界面上{F4912030-EF65-4099-880A-8A1792A3B449}
        /// </summary>
        [Category("控件设置"), Description("设置该控件是否在界面上分解复合项目 true分解 false不分解")]
        public bool IsSplitUndrugCombItem 
        {
            get 
            {
                return this.isSplitUndrugCombItem;
            }
            set 
            {
                this.isSplitUndrugCombItem = value;
            }
        }
        //{F4912030-EF65-4099-880A-8A1792A3B449} 结束

        /// <summary>
        /// 患者基本信息实体
        /// </summary>
        public FS.HISFC.Models.RADT.PatientInfo PatientInfo
        {
            set 
            {
                this.patientInfo = value;
            }
        }

        /// <summary>
        /// 当前行
        /// </summary>
        public int RowCount 
        {
            get 
            {
                return this.rowCount;
            }
            set 
            {
                this.rowCount = value;
            }
        }

        /// <summary>
        /// 成功提示信息
        /// </summary>
        public string SucessMsg 
        {
            get 
            {
                return this.sucessMsg;
            }
        }
        
        /// <summary>
        /// 加载的项目类别
        /// </summary>
        [Category("控件设置"), Description("设置该控件加载的项目类别 药品:drug 非药品 undrug 所有: all")]
        public EnumShowItemType 加载项目类别 
        {
            get 
            {
                return this.itemKind;
            }
            set 
            {
                this.itemKind = value;
            }
        }

        /// <summary>
        /// 不加载的药品性质// {4D67D981-6763-4ced-814E-430B518304E2}
        /// </summary>
        [Category("控件设置"), Description("不加载的药品性质，用“,”区分开")]
        public string NoAddDrugQuality
        {
            get
            {
                return this.noAddDrugQuality;
            }
            set
            {
                this.noAddDrugQuality = value;
            }
        }
        /// <summary>
        /// 控件功能
        /// </summary>
        [Category("控件设置"), Description("获得或者设置该控件的主要功能"), DefaultValue(1)]
        public FeeTypes 控件功能
        {
            get 
            {
                return this.feeType;
            }
            set 
            {
                this.feeType = value;
            }
        }

        /// <summary>
        /// 是否可以收费或者划价0单价的项目
        /// </summary>
        [Category("控件设置"), Description("获得或者设置是否可以收费或者划价"), DefaultValue(false)]
        public bool IsChargeZero 
        {
            get 
            {
                return this.isChargeZero;
            }
            set 
            {
                this.isChargeZero = value;
            }
        }
        [Category("控件设置"),Description("是否判断欠费,Y：判断欠费，不允许继续收费,M：判断欠费，提示是否继续收费,N：不判断欠费")]
        public FS.HISFC.Models.Base.MessType MessageType
        {
            get
            {
                return this.messtype;
            }
            set
            {
                this.messtype = value;
            }
        }
        [Category("控件设置"), Description("数量为零是否提示")]
        public bool IsJudgeQty
        {
            get
            {
                return isJudgeQty;
            }
            set
            {
                isJudgeQty = value;
            }
        }

        [Category("控件设置"), Description("执行科室是否默认为登陆科室")]
        public bool DefaultExeDeptIsDeptIn
        {
            get
            {
                return defaultExeDeptIsDeptIn;
            }
            set
            {
                defaultExeDeptIsDeptIn = value;
            }
        }

        /// <summary>
        /// 是否显示比例项{2C7FCD3D-D9B4-44f5-A2EE-A7E8C6D85576}
        /// </summary>
        [Category("控件设置"), Description("是否显示比例项")]
        public bool IsShowFeeRate
        {
            get { return isShowFeeRate; }
            set { isShowFeeRate = value; }
        }

        /// <summary>
        /// 是否弹出发票
        /// </summary>
        [Category("控件设置"), Description("是否显示比例项")]
        public bool IsPopSHowInvoice
        {
            get { return isPopSHowInvoice; }
            set { isPopSHowInvoice = value; }
        }

        /// <summary>
        /// 医疗保险,公费接口类
        /// </summary>
        public FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy MedcareInterface 
        {
            set 
            {
                this.medcareInterface = value;
            }
        }

        /// <summary>
        /// 默认执行科室
        /// </summary>
        public string DefaultExeDept
        {
            get
            {
                return this.defaultExeDept;
            }
            set
            {
                this.defaultExeDept = value;
            }
        }

        /// <summary>
        /// 开方医生
        /// </summary>
        public string RecipeDoctCode
        {
            get
            {
                return this.recipeDoctCode;
            }
            set
            {
                this.recipeDoctCode = value;
            }
        }

        /// <summary>
        /// 是否验证及时停用标记
        /// </summary>
        public bool IsJudgeValid 
        {
            get 
            {
                return this.isJudgeValid;
            }
            set
            {
                this.isJudgeValid = value;
            }
        }

        /// <summary>
        /// 处方科室
        /// </summary>
        public FS.FrameWork.Models.NeuObject RecipeDept
        {
            set
            {
                this.recipeDept = value;
            }
        }

        /// <summary>
        /// 本次收费项目集合
        /// </summary>
        public List<FeeItemList> FeeItemCollection
        {
            get
            {
                return this.feeItemCollection;
            }
        }
        //{CEA2C5D7-4816-4eec-9914-B4C20E6C998F}
        /// <summary>
        /// 默认取药科室
        /// </summary>
        private FS.FrameWork.Models.NeuObject defautStockDept= null;

        public FS.FrameWork.Models.NeuObject DefautStockDept
        {
            get { return defautStockDept; }
            set { defautStockDept = value; }
        }
        //{0604764A-3F55-428f-9064-FB4C53FD8136}
        private string operationNO = string.Empty;

        public string OperationNO
        {
            get { return operationNO; }
            set { operationNO = value; }
        }

        //{FD08EA4B-B5F3-4514-AEEA-44222F5900FB} 增加收费表中保存手术医嘱号 
        private string orderID = string.Empty;

        public string OrderID
        {
            get
            {
                return orderID;
            }
            set
            {
                orderID = value;
            }
        }

        private FS.HISFC.Models.Fee.Inpatient.FTSource ftSource = new FTSource();
        public FS.HISFC.Models.Fee.Inpatient.FTSource FTSource
        {
            set
            {
                ftSource = value;
            }
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 将项目列表中项目添加到划价列表
        /// </summary>
        /// <param name="item"></param>
        /// <param name="row"></param>
        /// <param name="execDeptCode"></param>
        /// <returns></returns>
        protected virtual int AddChargeDetail(FS.HISFC.Models.Base.Item item, int row, string execDeptCode)
        {
            if (this.patientInfo == null || this.patientInfo.ID == null || this.patientInfo.ID == string.Empty)
            {
                MessageBox.Show(Language.Msg("请先选择患者,然后收费!"));

                return -1;
            }
            decimal price = 0;
            decimal orgPrice = 0;
            //if (this.pactUnitManager.GetPrice(this.patientInfo, item.IsPharmacy, item.ID, ref price) == -1)
            if (item.ItemType != EnumItemType.MatItem)
            {
                if (this.feeIntergrate.GetPriceForInpatient(this.patientInfo, item, ref price, ref orgPrice) == -1)
                {
                    MessageBox.Show(Language.Msg("取项目:") + item.Name + Language.Msg("的价格出错!") + this.pactUnitManager.Err);

                    return -1;
                }
                item.Price = price;
                item.DefPrice = orgPrice;
            }
            //药品默认按最小单位收费,显示价格也为最小单位价格,存入数据库的为包装单位价格
            //if (item.IsPharmacy)//药品
            if (item.ItemType == EnumItemType.Drug)//药品
            {
                price = FS.FrameWork.Public.String.FormatNumber(item.Price / item.PackQty, 4);
                this.fpDetail_Sheet.SetValue(row, (int)Columns.Price, price, false);
            }
            else//非药品
            {
                this.fpDetail_Sheet.SetValue(row, (int)Columns.Price, item.Price, false);

                price = item.Price;
            }

            //存储项目实体便于价格等计算{F98CC89C-BE9A-49ca-98E2-4C700A8F5E34}
            this.fpDetail_Sheet.Rows[row].Tag = item;

            //{30B79077-CDC0-4de8-822A-8B04ABB2925C}
            this.fpDetail_Sheet.Cells[row, (int)Columns.feeRate].Tag = item.Clone();

            //国标码
            this.fpDetail_Sheet.SetValue(row, (int)Columns.GbCode, item.GBCode, false);

            //数量
            this.fpDetail_Sheet.SetValue(row, (int)Columns.Qty, item.Qty, false);

            //判断焦点跳转
            if (item.Price != 0 || this.IsChargeZero)
            {
                this.fpDetail_Sheet.Cells[row, (int)Columns.Price].Locked = true;
                this.fpDetail.Focus();
                this.fpDetail_Sheet.SetActiveCell(row, (int)Columns.Qty);
            }
            else
            {
                this.fpDetail_Sheet.Cells[row, (int)Columns.Price].Locked = false;
                this.fpDetail.Focus();
                this.fpDetail_Sheet.SetActiveCell(row, (int)Columns.Price);
            }

            //草药付数
            this.fpDetail_Sheet.SetValue(row, (int)Columns.Day, NConvert.ToInt32(item.User03), false);

            //if (item.IsPharmacy && item.SysClass.ID.ToString() == "PCC")
            if (item.ItemType == EnumItemType.Drug && item.SysClass.ID.ToString() == "PCC")
            {
                this.fpDetail_Sheet.Cells[row, (int)Columns.Day].Locked = false;
                this.fpDetail_Sheet.Cells[row, (int)Columns.Day].ForeColor = Color.Black;
            }
            else
            {
                this.fpDetail_Sheet.Cells[row, (int)Columns.Day].Locked = true;
                this.fpDetail_Sheet.Cells[row, (int)Columns.Day].ForeColor = Color.Transparent;
            }
            //药品可选择药品收费单位,默认为最小单位
            //if (item.IsPharmacy)
            if(item.ItemType == EnumItemType.Drug)
            {
                FarPoint.Win.Spread.CellType.ComboBoxCellType comboType = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
                comboType.Editable = true;
                comboType.Items = (new string[]{(item as FS.HISFC.Models.Pharmacy.Item).MinUnit,
                                                (item as FS.HISFC.Models.Pharmacy.Item).PackUnit});
                this.fpDetail_Sheet.Cells[row, (int)Columns.Unit].CellType = comboType;
                this.fpDetail_Sheet.Cells[row, (int)Columns.Unit].Locked = false;
                if (item.MinFee.User03 == "2")
                {
                    this.fpDetail_Sheet.SetValue(row, (int)Columns.Unit, ((FS.HISFC.Models.Pharmacy.Item)item).PackUnit, false);
                    item.PriceUnit = ((FS.HISFC.Models.Pharmacy.Item)item).PackUnit;
                    price = FS.FrameWork.Public.String.FormatNumber(item.Price,4);
                    this.fpDetail_Sheet.SetValue(row, (int)Columns.Price, price, false);
                }
                else
                {
                    price = FS.FrameWork.Public.String.FormatNumber(item.Price / item.PackQty, 4);
                    this.fpDetail_Sheet.SetValue(row, (int)Columns.Price, price, false);
                    this.fpDetail_Sheet.SetValue(row, (int)Columns.Unit, ((FS.HISFC.Models.Pharmacy.Item)item).MinUnit, false);
                    item.PriceUnit = ((FS.HISFC.Models.Pharmacy.Item)item).MinUnit;
                }
                
            }
            else//非药品
            {
                FarPoint.Win.Spread.CellType.TextCellType textType = new FarPoint.Win.Spread.CellType.TextCellType();
                this.fpDetail_Sheet.Cells[row, (int)Columns.Unit].CellType = textType;
                this.fpDetail_Sheet.Cells[row, (int)Columns.Unit].Locked = true;
                this.fpDetail_Sheet.SetValue(row, (int)Columns.Unit, item.PriceUnit, false);
            }

           //总额
           this.fpDetail_Sheet.SetValue(row, (int)Columns.TotCost, price * item.Qty, false);
           
            //项目名称,和规格显示在一起
            if (item.Specs != null && item.Specs != string.Empty)
            {
                this.fpDetail_Sheet.SetValue(row, (int)Columns.ItemName, item.Name + "{" + item.Specs + "}", false);
            }
            else
            {
                this.fpDetail_Sheet.SetValue(row, (int)Columns.ItemName, item.Name, false);
            }

            //新录入的项目			
            this.fpDetail_Sheet.SetValue(row, (int)Columns.IsNew, "1", false);
            //标识药品、非药品
            //if (item.IsPharmacy)
            if(item.ItemType == EnumItemType.Drug)
            {
                this.fpDetail_Sheet.SetValue(row, (int)Columns.IsDrug, "1", false);
            }
            else
            {
                this.fpDetail_Sheet.SetValue(row, (int)Columns.IsDrug, "0", false);
            }
            //{2C7FCD3D-D9B4-44f5-A2EE-A7E8C6D85576}
            this.fpDetail_Sheet.SetValue(row, (int)Columns.feeRate, 1);
            string deptCode = string.Empty, deptName = string.Empty;
            if (!defaultExeDeptIsDeptIn && item.ItemType != EnumItemType.Drug)
            {
                #region 获取默认执行科室
                //获取项目默认执行科室
                //if (execDeptCode == null || execDeptCode == string.Empty)
                //{
                //    this.GetItemDept(item, ref deptCode, ref deptName);
                //}
                //else
                //{
                //    FS.HISFC.Models.Base.Department dept = this.departmentManager.GetDeptmentById(execDeptCode);
                //    deptCode = execDeptCode;
                //    if (dept == null)
                //    {
                //        deptName = "(无)";
                //    }
                //    else
                //    {
                //        deptName = dept.Name;
                //    }
                //}

                ////{CA82280B-51B6-4462-B63E-43F4ECF456A3}
                //execDeptCode = this.SetExecDept(item.ID);
                //if (execDeptCode != "")
                //{
                //    FS.HISFC.Models.Base.Department dept = this.departmentManager.GetDeptmentById(execDeptCode);
                //    deptCode = dept.ID;
                //    deptName = dept.Name;
                //}

                string ExecdeptCode = string.Empty;
                if ("N,I".Contains(SOC.HISFC.BizProcess.Cache.Common.GetDeptInfo(((FS.HISFC.Models.Base.Employee)this.itemManager.Operator).Dept.ID).DeptType.ID.ToString()))
                {
                    if (this.patientInfo != null)
                    {
                        ExecdeptCode = this.patientInfo.PVisit.PatientLocation.Dept.ID;
                    }
                    else
                    {
                        ExecdeptCode = SOC.HISFC.BizProcess.Cache.Common.GetEmployeeInfo(this.recipeDoctCode).Dept.ID;
                    }
                }
                else
                {
                    ExecdeptCode = this.operObj.Dept.ID;
                }
                
                if (item.ItemType == EnumItemType.Drug)
                {
                    ArrayList alExecDept = null;
                    FS.FrameWork.Models.NeuObject dept = this.managerIntegrate.GetDepartment(ExecdeptCode);
                    deptCode = dept.ID;
                    deptName = dept.Name;
                    lbDept.Items.Clear();
                    SOC.HISFC.BizProcess.Cache.Common.GetDept("1");
                    alExecDept = SOC.HISFC.BizProcess.Cache.Common.deptHelper.ArrayObject;
                    lbDept.AddItems(alExecDept);
                }
                else
                {
                    ArrayList alExecDept = null;
                    string defaultExecDept = string.Empty;
                    SOC.HISFC.BizProcess.Cache.Common.SetExecDept(false, ExecdeptCode, item.ID, ref defaultExecDept, ref alExecDept);
                    if (alExecDept == null || alExecDept.Count == 0)
                    {
                        SOC.HISFC.BizProcess.Cache.Common.GetDept("1");
                        alExecDept = SOC.HISFC.BizProcess.Cache.Common.deptHelper.ArrayObject;
                    }
                    FS.FrameWork.Models.NeuObject dept = this.managerIntegrate.GetDepartment(defaultExecDept);
                    deptCode = dept.ID;
                    deptName = dept.Name;
                    lbDept.Items.Clear();
                    lbDept.AddItems(alExecDept);
                }
                #endregion
            }
            else
            {
                deptCode = this.operObj.Dept.ID;
                deptName = this.operObj.Dept.Name;
            }

            this.fpDetail_Sheet.SetValue(row, (int)Columns.Dept, deptName, false);
            //表示科室未修改
            this.fpDetail_Sheet.SetValue(row, (int)Columns.IsDeptChange, "0", false);

            //计费日期
            this.fpDetail_Sheet.SetValue(row, (int)Columns.feeDate, DateTime.Now, false);

            //赋值给收费实体
            FeeItemList feeitemlist = new FeeItemList();
            feeitemlist.Item = item;
            feeitemlist.ExecOper.Dept.ID = deptCode;
            feeitemlist.ExecOper.Dept.Name = deptName;
            feeitemlist.Days = NConvert.ToInt32(item.User03);//草药付数
            //指定药品的摆药药房
            if (item is FS.HISFC.Models.Pharmacy.Item)
            {
                feeitemlist.StockOper.Dept.ID = item.User02;
            }

            //保存复合项目
            feeitemlist.UndrugComb.ID = item.MinFee.User01;
            feeitemlist.UndrugComb.Name = item.MinFee.User02;

            feeitemlist.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(price * item.Qty, 2);
           

            this.fpDetail_Sheet.SetValue(row, (int)Columns.ItemObject, feeitemlist, false);
            //{062CEAA8-16B8-4c25-B4CC-E6B24DE7D331}
            if (IAdptIllnessInPatient != null)
            {
                int resultValue = IAdptIllnessInPatient.ProcessInpatientFeeDetail(this.patientInfo, ref feeitemlist);
                if (resultValue < 0) return -1;
            }
            
            return 0;
        }

        /// <summary>
        /// 添加患者划价明细
        /// </summary>
        /// <param name="feeItemList">费用信息实体</param>
        /// <param name="row">当前行</param>
        /// <returns>成功 1 失败 -1</returns>
        protected virtual int AddChargeDetail(FeeItemList feeItemList, int row)
        {
            if (feeItemList != null)
            {
                FarPoint.Win.Spread.CellType.TextCellType txtType = new FarPoint.Win.Spread.CellType.TextCellType();
                txtType.ReadOnly = true;

                this.fpDetail_Sheet.Rows[row].BackColor = Color.Blue;

                //显示名称
                if (feeItemList.Item.Specs != null && feeItemList.Item.Specs != string.Empty)
                {
                    this.fpDetail_Sheet.SetValue(row, (int)Columns.ItemName, feeItemList.Item.Name + "{" + feeItemList.Item.Specs + "}", false);
                }
                else
                {
                    this.fpDetail_Sheet.SetValue(row, (int)Columns.ItemName, feeItemList.Item.Name, false);
                }

                this.fpDetail_Sheet.Cells[row, (int)Columns.ItemName].CellType = txtType;

                //显示价格
                decimal price = 0;
                //if (feeItemList.Item.IsPharmacy)
                if(feeItemList.Item.ItemType == EnumItemType.Drug)
                {
                    price = FS.FrameWork.Public.String.FormatNumber(feeItemList.Item.Price / feeItemList.Item.PackQty, 4);
                    this.fpDetail_Sheet.SetValue(row, (int)Columns.Price, price, false);
                }
                else
                {
                    this.fpDetail_Sheet.SetValue(row, (int)Columns.Price, feeItemList.Item.Price, false);
                }

                this.fpDetail_Sheet.Cells[row, (int)Columns.Price].Locked = true;

                //显示付数
                if (feeItemList.Days == 0)
                {
                    feeItemList.Days = 1;
                }
                this.fpDetail_Sheet.SetValue(row, (int)Columns.Day, feeItemList.Days, false);
                this.fpDetail_Sheet.Cells[row, (int)Columns.Day].Locked = true;

                //草药
                //if (feeItemList.Item.IsPharmacy && feeItemList.Item.MinFee.ID == "003")
                if (feeItemList.Item.ItemType == EnumItemType.Drug && feeItemList.Item.MinFee.ID == "003")
                {
                    this.fpDetail_Sheet.Cells[row, (int)Columns.Day].ForeColor = Color.Black;
                }
                else
                {
                    this.fpDetail_Sheet.Cells[row, (int)Columns.Day].ForeColor = this.fpDetail_Sheet.Rows[row].BackColor;
                }

                //数量
                feeItemList.Item.Qty = feeItemList.Item.Qty / feeItemList.Days;

                this.fpDetail_Sheet.SetValue(row, (int)Columns.Qty, feeItemList.Item.Qty, false);

                this.fpDetail_Sheet.SetValue(row, (int)Columns.Unit, feeItemList.Item.PriceUnit, false);
                this.fpDetail_Sheet.SetValue(row, (int)Columns.TotCost, feeItemList.FT.TotCost, false);

                Department dept = this.departmentManager.GetDeptmentById(feeItemList.ExecOper.Dept.ID);

                if (dept == null)
                {
                    dept = new Department();
                    dept.Name = "(无)";
                }

                this.fpDetail_Sheet.SetValue(row, (int)Columns.Dept, dept.Name, false);
                this.fpDetail_Sheet.Cells[row, (int)Columns.Dept].CellType = txtType;

                feeItemList.ExecOper.Dept.ID = dept.ID;
                feeItemList.ExecOper.Dept.Name = dept.Name;
                this.fpDetail_Sheet.SetValue(row, (int)Columns.ItemObject, feeItemList, false);
                this.fpDetail_Sheet.SetValue(row, (int)Columns.IsNew, "0", false);
                this.fpDetail_Sheet.SetValue(row, (int)Columns.IsDeptChange, "0", false);

                this.fpDetail_Sheet.SetValue(row, (int)Columns.feeDate, DateTime.Now, false);

                //if (feeItemList.Item.IsPharmacy)
                if(feeItemList.Item.ItemType == EnumItemType.Drug)
                {
                    this.fpDetail_Sheet.SetValue(row, (int)Columns.IsDrug, "1", false);
                }
                else
                {
                    this.fpDetail_Sheet.SetValue(row, (int)Columns.IsDrug, "0", false);
                }
            }

            return 1;
        }

        /// <summary>
        /// 添加需确认非药品医嘱明细
        /// </summary>
        /// <param name="execOrder">医嘱执行档信息</param>
        /// <param name="row">当前行</param>
        /// <returns>成功 1 失败: -1</returns>
        protected int AddOrderDetail(ExecOrder execOrder, int row)
        {
            if (execOrder != null)
            {
                //未处理复合项目
                FarPoint.Win.Spread.CellType.TextCellType txtType = new FarPoint.Win.Spread.CellType.TextCellType();
                txtType.ReadOnly = true;
                this.fpDetail_Sheet.Rows[row].BackColor = Color.LightSkyBlue;

                //项目名称
                if (execOrder.Order.Item.Specs != null && execOrder.Order.Item.Specs != string.Empty)
                {
                    this.fpDetail_Sheet.SetValue(row, (int)Columns.ItemName, execOrder.Order.Item.Name + "{" + execOrder.Order.Item.Specs + "}", false);
                }
                else
                {
                    this.fpDetail_Sheet.SetValue(row, (int)Columns.ItemName, execOrder.Order.Item.Name, false);
                }
                this.fpDetail_Sheet.Cells[row, (int)Columns.ItemName].CellType = txtType;

                //价格
                decimal price = 0;
                decimal orgPrice = 0;
                if (execOrder.Order.Unit != "[复合项]")
                {

                    //if (this.pactUnitManager.GetPrice(this.patientInfo, false, execOrder.Order.Item.ID, ref price) == -1)
                    if (this.feeIntergrate.GetPriceForInpatient(this.patientInfo, execOrder.Order.Item, ref price, ref orgPrice) == -1)
                    {
                        MessageBox.Show(Language.Msg("获取项目价格出错!"));

                        return -1;
                    }
                    if (price != 0)
                    {
                        execOrder.Order.Item.Price = price;
                        execOrder.Order.Item.DefPrice = orgPrice;
                    }
                }
                this.fpDetail_Sheet.SetValue(row, (int)Columns.Price, execOrder.Order.Item.Price, false);
                this.fpDetail_Sheet.Cells[row, (int)Columns.Price].Locked = true;

                //数量
                this.fpDetail_Sheet.SetValue(row, (int)Columns.Qty, execOrder.Order.Qty, false);
                this.fpDetail_Sheet.Cells[row, (int)Columns.Qty].Locked = true;

                //付数
                this.fpDetail_Sheet.SetValue(row, (int)Columns.Day, "1", false);
                this.fpDetail_Sheet.Cells[row, (int)Columns.Day].Locked = true;
                this.fpDetail_Sheet.Cells[row, (int)Columns.Day].ForeColor = this.fpDetail_Sheet.Rows[row].BackColor;
                execOrder.Order.HerbalQty = 1;

                //单位
                this.fpDetail_Sheet.SetValue(row, (int)Columns.Unit, execOrder.Order.Unit, false);
                //金额
                this.fpDetail_Sheet.SetValue(row, (int)Columns.TotCost, execOrder.Order.Qty * execOrder.Order.Item.Price, false);

                //执行科室
                this.fpDetail_Sheet.SetValue(row, (int)Columns.Dept, execOrder.ExecOper.Dept.Name, false);
                this.fpDetail_Sheet.Cells[row, (int)Columns.Dept].CellType = txtType;

                //项目对象
                this.fpDetail_Sheet.SetValue(row, (int)Columns.ItemObject, execOrder, false);

                //是否新增项目,0原有(数据库中),1新增,2修改
                this.fpDetail_Sheet.SetValue(row, (int)Columns.IsNew, "0", false);

                //执行科室是否修改0,否 1是
                this.fpDetail_Sheet.SetValue(row, (int)Columns.IsDeptChange, "0", false);

                //收费药品，1是0否
                this.fpDetail_Sheet.SetValue(row, (int)Columns.IsDrug, "0", false);
            }

            return 1;
        }

        /// <summary>
        /// 添加组套明细到划价列表
        /// </summary>
        /// <param name="groupID">组套项目ID</param>
        /// <param name="row">当前行</param>
        /// <returns>成功 1 失败 -1</returns>
        public int AddGroupDetail(string groupID, int row)
        {
            if (this.patientInfo == null || this.patientInfo.ID == null || this.patientInfo.ID == string.Empty)
            {
                MessageBox.Show(Language.Msg("请先选择患者,然后收费!"));

                return -1;
            }

            ArrayList groupDetails = new ArrayList();
            //根据组套id获取组套明细
            groupDetails = this.groupDetailManager.GetComGroupTailByGroupID(groupID);
            if (groupDetails == null || groupDetails.Count == 0)
            {
                return -1;
            }
            int count = 0;
            for (int i = 0; i < groupDetails.Count; i++)
            {
                FS.HISFC.Models.Fee.ComGroupTail groupDetail = groupDetails[i] as FS.HISFC.Models.Fee.ComGroupTail;
                if (groupDetail.drugFlag == "1")//药品
                {
                    //根据药品id获取药品实体
                    FS.HISFC.Models.Pharmacy.Storage drugStorate = null;

                    drugStorate = this.pharmacyIntergrate.GetItemForInpatient(this.patientInfo.PVisit.PatientLocation.Dept.ID, groupDetail.itemCode);
                    if (drugStorate == null || drugStorate.Item.ID == string.Empty) continue;
                    count++;
                    //添加到划价列表
                    FS.HISFC.Models.Base.Item drugBase = drugStorate.Item as FS.HISFC.Models.Base.Item;
                    //drugBase.IsPharmacy = true;
                    drugBase.ItemType = EnumItemType.Drug;
                    drugBase.Qty = groupDetail.qty;
                    drugBase.User03 = "1";
                    drugBase.MinFee.User03 = groupDetail.unitFlag;//使用这个暂时存储以下组套内的单位1：最小单位2：包装单位 add by sunm

                    #region 直接在第一行增加组套明细 ,比其他算法简单点
                    if (count > 1)
                    {
                        this.AddRow(row + count - 1);
                    }

                    this.AddChargeDetail(drugBase, row + count - 1, groupDetail.deptCode);    
                    #endregion 
                }
                else//非药品
                {
                    //根据非药品id获取非药品实体
                    FS.HISFC.Models.Fee.Item.Undrug undrug = null;

                    undrug = this.undrugManager.GetValidItemByUndrugCode(groupDetail.itemCode);
                    if (undrug == null) continue;
                    count++;
                    //添加划价项目
                    FS.HISFC.Models.Base.Item undrugBase = undrug as FS.HISFC.Models.Base.Item;
                    //undrugBase.IsPharmacy = false;
                    undrugBase.ItemType = EnumItemType.UnDrug;
                    undrugBase.Qty = groupDetail.qty;//数量
                    undrugBase.User03 = "1";//付数
                    undrugBase.User03 = "1";//付数
                    //{01797533-5D92-4958-A52B-61540022F202}
                    if (undrug.UnitFlag == "1")
                    {
                        undrugBase.User01 = "[复合项]";
                    }
                    #region 直接在第一行增加组套明细 ,比其他算法简单点
                    if (count > 1)
                    {
                        AddRow(row + count - 1);
                    }
                    this.AddChargeDetail(undrugBase, row + count - 1, groupDetail.deptCode); 
                    #endregion 
                }
            }
            return 0;
        }
        /// <summary>
        /// 添加组套明细到划价列表
        /// </summary>
        /// <param name="groupID">组套项目ID</param> 
        /// <returns>成功 1 失败 -1</returns>
        public int AddGroupDetail(string groupID)
        {
            if (this.patientInfo == null || this.patientInfo.ID == null || this.patientInfo.ID == string.Empty)
            {
                MessageBox.Show(Language.Msg("请先选择患者,然后收费!"));

                return -1;
            }

            ArrayList groupDetails = new ArrayList();
            //根据组套id获取组套明细
            groupDetails = this.groupDetailManager.GetComGroupTailByGroupID(groupID);
            if (groupDetails == null || groupDetails.Count == 0)
            {
                return -1;
            } 
            for (int i = 0; i < groupDetails.Count; i++)
            {
                FS.HISFC.Models.Fee.ComGroupTail groupDetail = groupDetails[i] as FS.HISFC.Models.Fee.ComGroupTail;
                if (groupDetail.drugFlag == "1")//药品
                {
                    //根据药品id获取药品实体
                    FS.HISFC.Models.Pharmacy.Storage drugStorate = null;

                    drugStorate = this.pharmacyIntergrate.GetItemForInpatient(this.patientInfo.PVisit.PatientLocation.Dept.ID, groupDetail.itemCode);
                    if (drugStorate == null || drugStorate.Item.ID == string.Empty) continue;
                    //添加到划价列表
                    FS.HISFC.Models.Base.Item drugBase = drugStorate.Item as FS.HISFC.Models.Base.Item;
                    //drugBase.IsPharmacy = true;
                    drugBase.ItemType = EnumItemType.Drug;
                    drugBase.Qty = groupDetail.qty;
                    drugBase.User03 = "1";
                    drugBase.MinFee.User03 = groupDetail.unitFlag;

                    #region 直接在第一行增加组套明细 ,比其他算法简单点 
                    this.AddRow(0);
                    this.AddChargeDetail(drugBase,0,groupDetail.deptCode);
                    #endregion
                }
                else//非药品
                {
                    //根据非药品id获取非药品实体
                    FS.HISFC.Models.Fee.Item.Undrug undrug = null;

                    undrug = this.undrugManager.GetValidItemByUndrugCode(groupDetail.itemCode);
                    if (undrug == null) continue;
                    //添加划价项目
                    FS.HISFC.Models.Base.Item undrugBase = undrug as FS.HISFC.Models.Base.Item;
                    //undrugBase.IsPharmacy = false;
                    undrugBase.ItemType = EnumItemType.UnDrug;
                    undrugBase.Qty = groupDetail.qty;//数量
                    undrugBase.User03 = "1";//付数
                    #region 直接在第一行增加组套明细 ,比其他算法简单点
                    this.AddRow(0);
                    this.AddChargeDetail(undrugBase, 0, groupDetail.deptCode);
                    #endregion
                }
            }
            return 0;
        }

        /// <summary>
        /// 添加组套明细到划价列表
        /// </summary>
        /// <param name="groupID">组套项目ID</param> 
        /// <returns>成功 1 失败 -1</returns>
        public int AddGroupDetail(string groupID,ArrayList deleteGroupsList)
        {
            if (this.patientInfo == null || this.patientInfo.ID == null || this.patientInfo.ID == string.Empty)
            {
                MessageBox.Show(Language.Msg("请先选择患者,然后收费!"));

                return -1;
            }

          
            ArrayList groupDetails = new ArrayList();
            //根据组套id获取组套明细
            groupDetails = this.groupDetailManager.GetComGroupTailByGroupID(groupID);
           

            if (groupDetails == null || groupDetails.Count == 0)
            {
                return -1;
            }
            //{35D18A38-FF4D-47d0-B81F-7EFA0D9DF3F9}
            for (int i = 0; i < deleteGroupsList.Count; i++)
            {
                FS.HISFC.Models.Fee.ComGroupTail deleteGroupDetail = deleteGroupsList[i] as FS.HISFC.Models.Fee.ComGroupTail;
                for (int j = groupDetails.Count - 1; j >= 0; j--)
                {
                    FS.HISFC.Models.Fee.ComGroupTail groupDetail = groupDetails[j] as FS.HISFC.Models.Fee.ComGroupTail;
                    if (deleteGroupDetail.sequenceNo == groupDetail.sequenceNo)
                    {
                        groupDetails.Remove(groupDetail);
                    }

                }
            }

            if (groupDetails == null || groupDetails.Count == 0)
            {
                return -1;
            }

            for (int i = 0; i < groupDetails.Count; i++)
            {
                FS.HISFC.Models.Fee.ComGroupTail groupDetail = groupDetails[i] as FS.HISFC.Models.Fee.ComGroupTail;
                if (groupDetail.drugFlag == "1")//药品
                {
                    //根据药品id获取药品实体
                    FS.HISFC.Models.Pharmacy.Storage drugStorate = null;
                    if (string.IsNullOrEmpty(curDrugApplyDept))
                    {
                        drugStorate = this.pharmacyIntergrate.GetItemForInpatient(this.patientInfo.PVisit.PatientLocation.Dept.ID, groupDetail.itemCode);
                    }
                    else
                    {
                        drugStorate = this.pharmacyIntergrate.GetItemForInpatient(this.curDrugApplyDept, groupDetail.itemCode);
                    }
                    if (drugStorate == null || drugStorate.Item.ID == string.Empty) continue;
                    //添加到划价列表
                    FS.HISFC.Models.Base.Item drugBase = drugStorate.Item as FS.HISFC.Models.Base.Item;
                    //drugBase.IsPharmacy = true;
                    drugBase.ItemType = EnumItemType.Drug;
                    drugBase.Qty = groupDetail.qty;
                    drugBase.User03 = "1";
                    drugBase.MinFee.User03 = groupDetail.unitFlag;

                    #region 直接在第一行增加组套明细 ,比其他算法简单点
                    this.AddRow(0);
                    this.AddChargeDetail(drugBase, 0, groupDetail.deptCode);
                    #endregion
                }
                else//非药品
                {
                    //根据非药品id获取非药品实体
                    FS.HISFC.Models.Fee.Item.Undrug undrug = null;

                    undrug = this.undrugManager.GetValidItemByUndrugCode(groupDetail.itemCode);

                    

                    if (undrug == null) continue;

                    //区分复合项目收费，医保要走细项，所以进行拆分{06EF3804-FA10-4ac1-8D16-D344B46DF3C6}
                    if (undrug.UnitFlag == "1")
                    {
                        undrug.User01 = "[复合项]";
                    }
                    //添加划价项目
                    FS.HISFC.Models.Base.Item undrugBase = undrug as FS.HISFC.Models.Base.Item;
                    //undrugBase.IsPharmacy = false;
                    undrugBase.ItemType = EnumItemType.UnDrug;
                    undrugBase.Qty = groupDetail.qty;//数量
                    undrugBase.User03 = "1";//付数
                    #region 直接在第一行增加组套明细 ,比其他算法简单点
                    this.AddRow(0);
                    this.AddChargeDetail(undrugBase, 0, groupDetail.deptCode);
                    #endregion
                }
            }
            this.Sum();
            return 0;
        }
        /// <summary>
        /// 判断价格、数量、执行科室是否合法
        /// </summary>
        /// <returns>-1不合法,0合法</returns>
        public virtual bool IsValid()
        {
            int count = 0;

            if (this.recipeDoctCode == null || this.recipeDoctCode == string.Empty) 
            {
                MessageBox.Show(Language.Msg("请输入开方医生"));

                return false;
            }

            for (int i = 0; i < this.fpDetail_Sheet.RowCount; i++)
            {
                object obj = this.fpDetail_Sheet.GetValue(i, (int)Columns.ItemObject);
                //如果当前行不是项目,那么继续下一个判断
                if (obj == null)
                {
                    continue;
                }

                count++;

                string itemName = this.fpDetail_Sheet.GetText(i, (int)Columns.ItemName);//项目名称
                //判断数量
                if (isJudgeQty)
                {
                    if (!this.IsInputValid(itemName, i, Columns.Qty, true, "的执行数量不能小于等于零!"))
                    {
                        return false;
                    }
                }
                decimal feeRate = FS.FrameWork.Function.NConvert.ToDecimal(this.fpDetail_Sheet.GetText(i, (int)Columns.feeRate));
                if (feeRate <= 0)
                {
                    MessageBox.Show(Language.Msg("费用比例不能小于0或等于0"));
                    this.fpDetail.Focus();
                    this.fpDetail_Sheet.SetActiveCell(i, (int)Columns.feeRate);


                    return false;
                }


                //读取的医嘱项目不判断执行科室和价格
                if (obj is FeeItemList)
                {
                    //判断价格
                    if (!this.IsChargeZero)
                    {
                        if (!this.IsInputValid(itemName, i, Columns.Price, true, "的项目价格不能小于等于零!"))
                        {
                            return false;
                        }
                    }

                    //判断付数
                    if (!this.IsInputValid(itemName, i, Columns.Day, true, "的付数不能小于等于零!"))
                    {
                        return false;
                    }

                    //药品判断单位
                    //if (((FeeItemList)obj).Item.IsPharmacy && this.fpDetail_Sheet.GetText(i, (int)Columns.IsNew) == "1")
                    if (((FeeItemList)obj).Item.ItemType == EnumItemType.Drug && this.fpDetail_Sheet.GetText(i, (int)Columns.IsNew) == "1")
                    {
                        string tempValue = this.fpDetail_Sheet.GetText(i, (int)Columns.Unit);
                        FarPoint.Win.Spread.CellType.ComboBoxCellType comboType =
                            (FarPoint.Win.Spread.CellType.ComboBoxCellType)this.fpDetail_Sheet.Cells[i, (int)Columns.Unit].CellType;
                        if (tempValue != comboType.Items[0] && tempValue != comboType.Items[1])
                        {
                            MessageBox.Show(itemName + Language.Msg("的发药单位录入错误,请重新录入!"));
                            this.fpDetail.Focus();
                            this.fpDetail_Sheet.SetActiveCell(i, (int)Columns.Unit);

                            return false;
                        }
                    }

                    //判断科室
                    if (((FeeItemList)obj).ExecOper.Dept.ID == string.Empty)
                    {
                        MessageBox.Show(itemName + Language.Msg("的执行科室不能为空!"));
                        this.fpDetail.Focus();
                        this.fpDetail_Sheet.SetActiveCell(i, (int)Columns.Dept);

                        return false;
                    }
                }
            }
            //无明细，返还
            if (count == 0)
            {
                MessageBox.Show(Language.Msg("请录入项目明细!"));
                this.fpDetail.Focus();

                return false;
            }

            return true;
        }

        /// <summary>
        /// 添加复合项目明细到划价列表
        /// </summary>
        /// <param name="undrugCombCode">组合项目编码</param>
        /// <param name="undrugCombName">组合项目名称</param>
        /// <param name="row">当前行</param>
        /// <param name="execDeptCode">执行科室代码</param>
        /// <returns>成功 1 失败: -1</returns>
        protected virtual int AddCompoundDetail(string undrugCombCode, string undrugCombName, int row, string execDeptCode)
        {
            ArrayList details = this.undrugPackageManager.QueryUndrugPackagesBypackageCode(undrugCombCode);

            if (details == null)
            {
                MessageBox.Show(Language.Msg("获得组套信息出错!") + this.undrugPackageManager.Err);

                return -1;
            }

            int count = 0;

            for (int i = 0; i < details.Count; i++)
            {
                FS.HISFC.Models.Fee.Item.UndrugComb undrugComb = details[i] as FS.HISFC.Models.Fee.Item.UndrugComb;

                //特殊组合项目不处理
                //路志鹏 是否可用1是true可用， 0是false不可用 luzhp@FS.com
                if (undrugComb.User01 == "0")
                {
                    continue;
                }

                FS.HISFC.Models.Fee.Item.Undrug undrug = this.itemManager.GetValidItemByUndrugCode(undrugComb.ID);

                if (undrug == null)
                {
                    continue;
                }

                count++;

                //undrug.IsPharmacy = false;
                undrug.ItemType = EnumItemType.UnDrug;

                if (undrugComb.Qty == 0)
                {
                    undrug.Qty = 1;
                }
                else
                {
                    undrug.Qty = undrugComb.Qty;
                }

                undrug.User03 = "1";
                undrug.MinFee.User01 = undrugCombCode;
                undrug.MinFee.User02 = undrugCombName;

                this.AddChargeDetail(undrug, row + count - 1, execDeptCode);
            }

            return 1;
        }

        /// <summary>
        /// 项目划价
        /// </summary>
        /// <returns></returns>
        protected virtual int Charge()
        {
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.inpatientManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.personManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            ////事务管理类
            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(this.inpatientManager.Connection);
            //t.BeginTransaction();
            //this.inpatientManager.SetTrans(t.Trans);
            //this.personManager.SetTrans(t.Trans);

            //操作时间
            DateTime operTime = this.inpatientManager.GetDateTimeFromSysDateTime();
            //开方科室
            FS.HISFC.Models.Base.Employee employee = this.personManager.GetPersonByID(this.recipeDoctCode);
            if (employee == null)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(Language.Msg("人员信息表中无代码为:") + this.recipeDoctCode + Language.Msg("的人员!"));

                return -1;
            }
            string recipeDept = employee.Dept.ID;
            ArrayList feeList = new ArrayList();
            //循环处理
            for (int i = 0; i < this.fpDetail_Sheet.RowCount; i++)
            {
                FeeItemList feeItemList = new FeeItemList();
                bool isNew = false;
                int returnValue = 0;

                //项目信息赋值
                returnValue = this.SetItem(i, PayTypes.Charged, recipeDept, operTime, ref isNew, ref feeItemList);

                //如果获得的项目信息为空,不处理
                if (returnValue == 0)
                {
                    continue;
                }

                //{0604764A-3F55-428f-9064-FB4C53FD8136}
                //增加手术编码
                if (this.OperationNO != string.Empty)
                {
                    feeItemList.OperationNO = this.OperationNO;
                }
                //如果是新录入项目:
                if (isNew)
                {
                    feeList.Add(feeItemList);
                    //if (feeItemList.Item.IsPharmacy)
                    if (feeItemList.Item.ItemType == EnumItemType.Drug)
                    {
                        if (this.inpatientManager.InsertMedItemList(this.patientInfo, feeItemList) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(Language.Msg("插入药品划价信息出错!") + this.inpatientManager.Err);
                            this.fpDetail.Focus();
                            this.fpDetail_Sheet.SetActiveCell(i, (int)Columns.ItemName, false);

                            return -1;
                        }
                    }
                    else
                    {
                        if (this.inpatientManager.InsertFeeItemList(this.patientInfo, feeItemList) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(Language.Msg("插入非药品划价信息出错!") + this.inpatientManager.Err);
                            this.fpDetail.Focus();
                            this.fpDetail_Sheet.SetActiveCell(i, (int)Columns.ItemName, false);

                            return -1;
                        }
                    }
                }
                //修改的划价项目，只能修改数量
                else
                {
                    feeItemList.ChargeOper.OperTime = operTime;
                    feeItemList.ChargeOper.ID = this.inpatientManager.Operator.ID;
                    //更新原有的费用明细记录的金额和数量
                    if (this.inpatientManager.UpdateChargeInfo(feeItemList) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(Language.Msg("更新原有划价信息记录出错!") + this.inpatientManager.Err);
                        this.fpDetail.Focus();
                        this.fpDetail_Sheet.SetActiveCell(i, (int)Columns.ItemName, false);

                        return -1;
                    }
                }
            }
            //{4FF03BBF-763D-4063-A792-A2264999E79A}
            if (IAdptIllnessInPatient != null)
            {
                int resultValue = IAdptIllnessInPatient.SaveInpatientFeeDetail(this.patientInfo, ref feeList);

                if (resultValue < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    return -1;
                }
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            this.sucessMsg = "划价成功!";

            return 1;
        }

        /// <summary>
        /// 非药品收费
        /// </summary>
        /// <returns>成功 1 失败 -1</returns>
        protected virtual int Fee()
        {
            Employee employee = this.personManager.GetPersonByID(this.recipeDoctCode);

            if (employee == null)
            {
                MessageBox.Show(Language.Msg("获得人员基本信息出错!"));

                return -1;
            }

            if (this.recipeDept != null && this.recipeDept.ID != "")
            {
                employee.Dept = this.recipeDept;
            }
            else
            {
                this.recipeDept = employee.Dept;
            }

            this.fpDetail.Change -= new FarPoint.Win.Spread.ChangeEventHandler(this.fpDetail_Change);

            if (!this.IsValid()) 
            {
                this.fpDetail.Change += new FarPoint.Win.Spread.ChangeEventHandler(this.fpDetail_Change);
                return -1;
            }

            this.fpDetail.Change += new FarPoint.Win.Spread.ChangeEventHandler(this.fpDetail_Change);

            if (inpatientManager.GetStopAccount(this.patientInfo.ID) == "1")
            {
                MessageBox.Show(Language.Msg("该患者处于封帐状态，不能进行收费！"));

                return -1;
            }

            if (feeIntergrate.IsPatientLackFee(this.patientInfo))
            {
                //欠费判断
                if (this.messtype == MessType.M)
                {
                    if (MessageBox.Show(this, "此患者已欠费，是否继续？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    {
                        return -1;
                    }
                }
                else if (this.messtype == MessType.Y)
                {
                    MessageBox.Show(this, "此患者已欠费，不允许继续收费！", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    return -1;
                }
            }
            FS.HISFC.BizLogic.Fee.TemporaryFee temFee = new FS.HISFC.BizLogic.Fee.TemporaryFee();
            //事务管理类
            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            this.inpatientManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.feeIntergrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.personManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.departmentManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.pharmacyIntergrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.undrugManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            temFee.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            ArrayList firstInputFeeItemlist = new ArrayList();
            //保存本次收费项目明细信息
            this.feeItemCollection = new List<FeeItemList>();

            //操作时间
            DateTime  operTime = this.inpatientManager.GetDateTimeFromSysDateTime();
            //decimal freeCost = this.patientInfo.FT.LeftCost;//余额
            //decimal moneyAlert = this.patientInfo.PVisit.MoneyAlert;//警戒线
            //decimal totCost = 0m;//总金额

            //{CEA2C5D7-4816-4eec-9914-B4C20E6C998F}传入布尔型判断是否在药品业务层重取取药药房
            bool refreshStockDept = true;
            //循环处理
            for (int i = 0; i < this.fpDetail_Sheet.RowCount; i++)
            {
                FeeItemList feeItemList = new FeeItemList();
                bool isNew = false;              
                int returnValue = 0;

                //项目信息赋值
                returnValue = this.SetItem(i, PayTypes.Balanced, employee.Dept.ID, operTime, ref isNew, ref feeItemList);
               

                //如果获得的项目信息为空,不处理
                if (returnValue == 0)
                {
                    continue;
                }

                if (returnValue == -1) 
                {
                    this.feeIntergrate.Rollback();

                    return -1;
                }

                //{0604764A-3F55-428f-9064-FB4C53FD8136}
                //增加手术编码
                if (this.OperationNO != string.Empty)
                {
                    feeItemList.OperationNO = this.OperationNO;
                }
                //{CEA2C5D7-4816-4eec-9914-B4C20E6C998F}
                if (this.defautStockDept != null && this.defautStockDept.ID != "" 
                    && feeItemList.Item.ItemType == EnumItemType.Drug && this.defautStockDept.ID!="default")
                {
                    feeItemList.ExecOper.Dept.ID= this.defautStockDept.ID;
                    refreshStockDept = false;
                }

                //如果是新录入项目
                if (isNew)
                {                 
                    feeItemList.StockOper.Dept.ID = feeItemList.ExecOper.Dept.ID;
                    firstInputFeeItemlist.Add(feeItemList.Clone());

                    this.feeItemCollection.Add(feeItemList.Clone());
                }
                // 修改的划价项目，只能修改数量
                else
                {
                    //更新原有的费用明细记录的金额和数量
                    if (this.inpatientManager.UpdateChargeInfo(feeItemList) == -1)
                    {
                        feeIntergrate.Rollback();
                        MessageBox.Show(Language.Msg("更新原有划价信息记录出错!") + this.inpatientManager.Err);
                        this.fpDetail.Focus();
                        this.fpDetail_Sheet.SetActiveCell(i, (int)Columns.ItemName, false);

                        return -1;
                    }

                    //插入费用汇总更新主表--调用组合业务
                    if (this.inpatientManager.FeeAfterCharge(this.patientInfo, feeItemList) == -1)
                    {
                        feeIntergrate.Rollback();
                        MessageBox.Show(feeItemList.Item.Name + Language.Msg("更改项目后收费出错!") + this.inpatientManager.Err);
                        this.fpDetail.Focus();
                        this.fpDetail_Sheet.SetActiveCell(i, (int)Columns.ItemName, false);

                        return -1;
                    }

                    //药品加入申请表
                    //if (feeItemList.Item.IsPharmacy)
                    //{CEA2C5D7-4816-4eec-9914-B4C20E6C998F}
                    if(feeItemList.Item.ItemType == EnumItemType.Drug)
                    {
                        if (this.pharmacyIntergrate.ApplyOut(this.patientInfo, feeItemList, operTime, refreshStockDept) == -1)
                        {
                            feeIntergrate.Rollback();
                            MessageBox.Show(Language.Msg(feeItemList.Item.Name + "对应科室") + this.pharmacyIntergrate.Err);
                            this.fpDetail.Focus();
                            this.fpDetail_Sheet.SetActiveCell(i, (int)Columns.ItemName, false);

                            return -1;
                        }
                    }
                }
            }


            //{F4912030-EF65-4099-880A-8A1792A3B449} 如果拆分界面上不拆分复合项目，这里拆分
            if (!this.isSplitUndrugCombItem) 
            {
                SplitUndrugCombItem(ref firstInputFeeItemlist);

                // 手术编码{0604764A-3F55-428f-9064-FB4C53FD8136}
                //foreach ( FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList in firstInputFeeItemlist)
                //{
                //    feeItemList.OperationNO = operationNO;
                //    //{FD08EA4B-B5F3-4514-AEEA-44222F5900FB} 增加收费表中保存手术医嘱号 
                //    feeItemList.Order.ID = orderID;
                //}
            }
            ////{F4912030-EF65-4099-880A-8A1792A3B449}结束

            if (this.IsPopSHowInvoice)
            {
                ArrayList allInvoiceFeeType = new ArrayList();
                Hashtable allFeeByInvoice = this.GetFeeByInvoice(firstInputFeeItemlist,ref allInvoiceFeeType);
                string PopShowMessage = string.Empty;
                allInvoiceFeeType.Sort(new CompareByID());
                if (allInvoiceFeeType.Count > 0)
                {
                    foreach (string statCode in allInvoiceFeeType)
                    {
                        PopShowMessage += (allFeeByInvoice[statCode] as FS.HISFC.Models.Fee.Inpatient.BalanceList).Name + "： " + (allFeeByInvoice[statCode] as FS.HISFC.Models.Fee.Inpatient.BalanceList).BalanceBase.FT.TotCost + "\n";
                    }
                    PopShowMessage = "住院号：" + (firstInputFeeItemlist[0] as FS.HISFC.Models.Fee.Inpatient.FeeItemList).Patient.PID.PatientNO + "         姓名：" + (firstInputFeeItemlist[0] as FS.HISFC.Models.Fee.Inpatient.FeeItemList).Patient.Name + "\n"  + "------------------------------------------\n"+ PopShowMessage + "\n是否确认收费？";
                    if ((DialogResult)MessageBox.Show(PopShowMessage, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) != DialogResult.Yes)
                    {
                        return -1;
                    }
                }
                
            }

            //调用整体收费函数,收取第一次录入的费用
            if (this.feeIntergrate.FeeItem(this.patientInfo, ref firstInputFeeItemlist) == -1)
            {
                feeIntergrate.Rollback();
                MessageBox.Show(this.feeIntergrate.Err);
                this.feeIntergrate.MedcareInterfaceProxy.Disconnect();
                this.fpDetail.Focus();
                
                return -1;
            }
            //对第一次收费的项目插入药品申请信息
            //{CEA2C5D7-4816-4eec-9914-B4C20E6C998F}
            foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList in firstInputFeeItemlist) 
            {
                //药品加入申请表
                //if (feeItemList.Item.IsPharmacy)
                if(feeItemList.Item.ItemType == EnumItemType.Drug)
                {
                    feeItemList.StockOper.Dept.ID = feeItemList.ExecOper.Dept.ID;

                    if (this.pharmacyIntergrate.ApplyOut(this.patientInfo, feeItemList, operTime, refreshStockDept) == -1)
                    {
                        feeIntergrate.Rollback();
                        MessageBox.Show(Language.Msg(feeItemList.Item.Name+"对应科室") + this.pharmacyIntergrate.Err);
                        this.fpDetail.Focus();

                        return -1;
                    }
                }
            }
            //{4FF03BBF-763D-4063-A792-A2264999E79A}
            if (IAdptIllnessInPatient != null)
            {
                ArrayList feeList = new ArrayList(feeItemCollection);
                int resultValue = IAdptIllnessInPatient.SaveInpatientFeeDetail(this.patientInfo, ref feeList);

                if (resultValue < 0)
                {
                    feeIntergrate.Rollback();
                    return -1;
                }
            }
            this.feeIntergrate.MedcareInterfaceProxy.CloseAll();
            #region 删除暂存信息
            //先删除后增加  
            int ret = temFee.Delete(this.patientInfo.ID, recipeDept.ID, this.OperationNO);
            if (ret < 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("删除暂存收费明细出错");
                return -1;
            }
            #endregion
            this.feeIntergrate.Commit();

            this.sucessMsg = "收费成功!";

            return 1;
        }

        /// <summary>
        /// 根据费用明细获取发票分类信息
        /// </summary>
        /// <param name="firstInputFeeItemlist"></param>
        /// <returns></returns>
        private Hashtable GetFeeByInvoice(ArrayList firstInputFeeItemlist, ref ArrayList allInvoiceType)
        {
            Hashtable hsFeeCodeStatByInvoice = new Hashtable();

           allInvoiceType = new ArrayList();
            
            foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList in firstInputFeeItemlist)
            {
                FS.HISFC.Models.Fee.FeeCodeStat feeCodeStat = allFeecodeStatHelper[feeItemList.Item.MinFee.ID] as FS.HISFC.Models.Fee.FeeCodeStat;
                if (hsFeeCodeStatByInvoice.Contains(feeCodeStat.StatCate.ID))
                {
                    FS.HISFC.Models.Fee.Inpatient.BalanceList balanceListTmp = hsFeeCodeStatByInvoice[feeCodeStat.StatCate.ID] as FS.HISFC.Models.Fee.Inpatient.BalanceList;
                    balanceListTmp.BalanceBase.FT.TotCost += feeItemList.FT.TotCost;
                }
                else
                {
                    allInvoiceType.Add(feeCodeStat.StatCate.ID);
                    FS.HISFC.Models.Fee.Inpatient.BalanceList balanceListTmp = new BalanceList();
                    balanceListTmp.Name = feeCodeStat.StatCate.Name;
                    balanceListTmp.BalanceBase.FT.TotCost = feeItemList.FT.TotCost;
                    hsFeeCodeStatByInvoice.Add(feeCodeStat.StatCate.ID, balanceListTmp);
                }
            }
            return hsFeeCodeStatByInvoice;
        }

        
        /// <summary>
        /// 拆分复合项目//{F4912030-EF65-4099-880A-8A1792A3B449}
        /// </summary>
        /// <param name="itemList">当前项目列表</param>
        /// <returns>成功 1 失败 -1</returns>
        private int SplitUndrugCombItem(ref ArrayList itemList) 
        {
            ArrayList undrugCombItemList = new ArrayList();

            foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList f in itemList)
            {
                if (f.Item.ItemType == EnumItemType.UnDrug && (f.Item.User01 == "[复合项]" || f.IsGroup))
                {
                    undrugCombItemList.Add(f);
                }
                else//单条的新加的给加上ID add by yerl
                {
                    if (string.IsNullOrEmpty(f.Order.ID))
                        f.Order.ID = this.orderIntegrate.GetNewOrderID();
                    //如果医嘱执行单流水号为空,则获取 
                    if (string.IsNullOrEmpty(f.ExecOrder.ID))
                        f.ExecOrder.ID = orderManager.GetNewOrderExecID();

                    decimal price = 0;
                    decimal orgPrice = 0;

                    //重新取收费价
                    if (this.feeIntergrate.GetPriceForInpatient(this.patientInfo, f.Item, ref price, ref orgPrice) < 0)
                    {
                        MessageBox.Show(Language.Msg("获取组套明细的价格失败!") + this.feeIntergrate.Err);
                        return -1;
                    }
                    f.Item.Price = price;
                    f.Item.DefPrice = orgPrice;
                }
            }
            foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList f in undrugCombItemList) 
            {
                itemList.Remove(f);
            }

            ArrayList finalCombItemList = new ArrayList();

            foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList f in undrugCombItemList)
            {
                ArrayList details = this.undrugPackageManager.QueryUndrugPackagesBypackageCode(f.Item.ID);
                if (details == null)
                {
                    MessageBox.Show(Language.Msg("获得组套信息出错!") + this.undrugPackageManager.Err);

                    return -1;
                }

                string orderID = this.orderIntegrate.GetNewOrderID();

                decimal rate = 1;
                for (int i = 0; i < details.Count; i++)
                {
                    FS.HISFC.Models.Fee.Item.UndrugComb undrugComb = details[i] as FS.HISFC.Models.Fee.Item.UndrugComb;

                    //特殊组合项目不处理
                    //路志鹏 是否可用1是true可用， 0是false不可用 luzhp@FS.com
                    if (undrugComb.User01 == "0")
                    {
                        continue;
                    }

                    FS.HISFC.Models.Fee.Item.Undrug undrug = this.itemManager.GetValidItemByUndrugCode(undrugComb.ID);
                    if (undrug == null)
                    {
                        continue;
                    }
                    undrug.ItemType = EnumItemType.UnDrug;
                    if (undrugComb.Qty == 0)
                    {
                        undrug.Qty = 1 * f.Item.Qty;
                    }
                    else
                    {
                        undrug.Qty = undrugComb.Qty * f.Item.Qty;
                    }

                    undrug.User03 = "1";
                    undrug.MinFee.User01 = f.Item.ID;
                    undrug.MinFee.User02 = f.Item.Name;
                    

                    FS.HISFC.Models.Fee.Inpatient.FeeItemList fComb = new FeeItemList();
                    fComb = f.Clone();

                    //如果医嘱执行单流水号为空,则获取 add by yerl
                    if (string.IsNullOrEmpty(fComb.ExecOrder.ID))
                        fComb.ExecOrder.ID = orderManager.GetNewOrderExecID();

                    fComb.NoBackQty = undrug.Qty;
                    fComb.Item = undrug;
                    fComb.UndrugComb.ID = undrug.MinFee.User01;
                    fComb.UndrugComb.Name = undrug.MinFee.User02;

                    decimal price = undrug.Price;
                    decimal orgPrice = undrug.DefPrice;

                    rate = feeIntergrate.GetItemRateForZT(f.Item.ID, undrug.ID);

                    //拆分时 重新取收费价
                    if (this.feeIntergrate.GetPriceForInpatient(this.patientInfo, undrug, ref price, ref orgPrice, rate) < 0)
                    {
                        MessageBox.Show(Language.Msg("获取组套明细的价格失败!") + this.feeIntergrate.Err);
                        return -1;
                    }
                    undrug.Price = price;
                    undrug.DefPrice = orgPrice;

                    fComb.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(fComb.Item.Price * fComb.Item.Qty, 2);
                    fComb.FT.OwnCost = fComb.FT.TotCost;
                    fComb.Order.ID = orderID;

                    //复合项目数量
                    fComb.UndrugComb.Qty = f.Item.Qty;

                    finalCombItemList.Add(fComb);
                }
            }

            itemList.AddRange(finalCombItemList);

            return 1;
        }//{F4912030-EF65-4099-880A-8A1792A3B449}结束


        /// <summary>
        /// 显示汇总金额
        /// </summary>
        protected virtual void Sum()
        {
            int count = this.fpDetail_Sheet.RowCount;

            if (count > 1)
            {
                count = count - 1;
                //this.fpDetail_Sheet.Cells[count, (int)Columns.TotCost].Formula = "sum(F1:F" + count.ToString() + ")";
                this.fpDetail_Sheet.Cells[count, (int)Columns.TotCost].Formula = "sum(G1:G" + count.ToString() + ")";
            }
            else if (count > 0)
            {
                this.fpDetail_Sheet.SetValue(count - 1, (int)Columns.TotCost, 0, false);
            }
        }
        
        /// <summary>
        /// 初始化项目显示列表(FarPoint)
        /// </summary>
        protected virtual void InitFP()
        {
            InputMap im;
            im = this.fpDetail.GetInputMap(InputMapMode.WhenAncestorOfFocused);

            im.Put(new Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = this.fpDetail.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Down, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = this.fpDetail.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Up, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = this.fpDetail.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Escape, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = this.fpDetail.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.F2, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = this.fpDetail.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.F3, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = this.fpDetail.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.F4, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            this.fpDetail_Sheet.Columns[(int)Columns.ItemObject].Visible = false;
            this.fpDetail_Sheet.Columns[(int)Columns.IsNew].Visible = false;
            this.fpDetail_Sheet.Columns[(int)Columns.IsDeptChange].Visible = false;
            this.fpDetail_Sheet.Columns[(int)Columns.IsDrug].Visible = false;

            //{2C7FCD3D-D9B4-44f5-A2EE-A7E8C6D85576}
            this.fpDetail_Sheet.Columns[(int)Columns.feeRate].Visible = isShowFeeRate;

        }
        
        /// <summary>
        /// 初始化科室列表
        /// </summary>
        private int InitDept()
        {
            ArrayList deptLists = this.departmentManager.GetDeptmentAll();
            if (deptLists == null) 
            {
                MessageBox.Show(Language.Msg("加载科室列表出错!") + this.departmentManager.Err);
                
                return -1;
            }
            this.lbDept.AddItems(deptLists);
            unDrugDeptList = deptLists;//{CA82280B-51B6-4462-B63E-43F4ECF456A3}
            this.Controls.Add(this.lbDept);
            this.lbDept.Hide();
           
            this.lbDept.BorderStyle = BorderStyle.FixedSingle;
            this.lbDept.BringToFront();

            this.lbDept.SelectItem += new FS.FrameWork.WinForms.Controls.PopUpListBox.MyDelegate(lbDept_SelectItem);
           
            return 1;
        }

        /// <summary>
        /// 科室选择事件
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private int lbDept_SelectItem(Keys key)
        {
            ProcessDept();
            this.fpDetail.Focus();
            this.fpDetail_Sheet.SetActiveCell(this.fpDetail_Sheet.ActiveRowIndex, (int)Columns.Dept, true);

            return 1;
        }

        /// <summary>
        /// 处理this.fpDetail,执行科室的回车
        /// </summary>
        /// <returns>成功 1 失败 -1</returns>
        private int ProcessDept()
        {
            int currRow = this.fpDetail_Sheet.ActiveRowIndex;
            
            if (currRow < 0)
            {
                return 1;
            }
            
            if (this.fpDetail_Sheet.GetText(currRow, (int)Columns.Unit) == "小计")
            {
                return 1;
            }

            string IsDeptChange = this.fpDetail_Sheet.GetText(currRow, (int)Columns.IsDeptChange);

            if ((IsDeptChange == "0" || IsDeptChange == string.Empty) && this.fpDetail_Sheet.GetText(currRow,(int)Columns.Dept) == string.Empty)
            {
                MessageBox.Show(Language.Msg("执行科室不能为空,请输入!"));
                this.fpDetail.Focus();
                this.fpDetail_Sheet.SetActiveCell(currRow, (int)Columns.Dept, true);

                return -1;
            }

            if (IsDeptChange == "1")
            {
                FS.FrameWork.Models.NeuObject item = null;

                int returnValue = this.lbDept.GetSelectedItem(out item);
                if (returnValue == -1 || item == null)
                {
                    return -1;
                }

                object obj = this.fpDetail_Sheet.GetValue(currRow, (int)Columns.ItemObject);
                if (obj == null)
                {
                    MessageBox.Show(Language.Msg("请选择项目!"));
                    this.fpDetail.Focus();
                    this.fpDetail_Sheet.SetActiveCell(currRow, (int)Columns.Dept, true);

                    return -1;
                }
                this.fpDetail.StopCellEditing();
                this.fpDetail_Sheet.SetValue(currRow, (int)Columns.Dept, item.Name);

                FeeItemList feeitemlist = (FeeItemList)obj;
                feeitemlist.ExecOper.Dept.ID = item.ID;
                feeitemlist.ExecOper.Dept.Name = item.Name;

                this.fpDetail_Sheet.SetValue(currRow, (int)Columns.ItemObject, feeitemlist);
                this.fpDetail_Sheet.SetValue(currRow, (int)Columns.IsDeptChange, "0");
            }

            this.lbDept.Visible = false;

            return 1;
        }

        /// <summary>
        /// 设置ucItem/cmbdept的显示位置
        /// </summary>
        /// <returns></returns>
        private int SetLocation()
        {
            Control cell = this.fpDetail.EditingControl;
            if (cell == null)
            {
                return -1;
            }

            if (this.fpDetail_Sheet.ActiveColumnIndex == (int)Columns.ItemName)
            {
                int y = cell.Top + cell.Height + this.ucItemList.Height + 7;
                if (y <= this.Height)
                {
                    this.ucItemList.Location = new Point(cell.Location.X + cell.Left, y - this.ucItemList.Height);
                }
                else
                {

                    if (cell.Top - this.ucItemList.Height - 7 > 0)
                    {
                        this.ucItemList.Location = new Point(cell.Location.X + cell.Left, cell.Top - this.ucItemList.Height - 7);
                    }
                    else
                    {
                        this.ucItemList.Location = new Point(cell.Location.X + cell.Left, 2);
                    }
                }
            }
            else if (this.fpDetail_Sheet.ActiveColumnIndex == (int)Columns.Dept)
            {
                this.lbDept.Size = new Size(cell.Width + SystemInformation.Border3DSize.Width * 2, 150);

                int y = cell.Top + cell.Height + this.lbDept.Height + SystemInformation.Border3DSize.Height * 2;

                if (y <= this.Height)
                {
                    this.lbDept.Location = new Point(cell.Left, y - this.lbDept.Height);
                }
                else
                {
                    this.lbDept.Location = new Point(cell.Left, cell.Top - this.lbDept.Height);
                }
            }

            return 0;
        }		

        /// <summary>
        /// 判断输入的Cell是否合法
        /// </summary>
        /// <param name="itemName">项目名称</param>
        /// <param name="row">当前行</param>
        /// <param name="col">当前列</param>
        /// <param name="isNumber">是否数字列</param>
        /// <param name="errText">错误信息</param>
        /// <returns>合法 true 不合法 false</returns>
        private bool IsInputValid(string itemName, int row, Columns col, bool isNumber, string errText)
        {
            string tempValue = this.fpDetail_Sheet.GetText(row, (int)col);
            if (tempValue == string.Empty)
            {
                if (isNumber)
                {
                    tempValue = "0";
                }
            }

            if (isNumber)
            {
                decimal tempNumber = NConvert.ToDecimal(tempValue);
                if (tempNumber <= 0)
                {
                    MessageBox.Show(itemName + Language.Msg(errText));
                    this.fpDetail.Focus();
                    this.fpDetail_Sheet.SetActiveCell(row, (int)col);

                    return false;
                }
            }
            else
            {
                MessageBox.Show(itemName + Language.Msg(errText));
                this.fpDetail.Focus();
                this.fpDetail_Sheet.SetActiveCell(row, (int)col);

                return false;
            }

            return true;
        }

        #region 默认执行科室列表
        private static Dictionary<string, FS.SOC.HISFC.Fee.Models.Undrug> dicUndrugExec = new Dictionary<string, FS.SOC.HISFC.Fee.Models.Undrug>();

        private static FS.SOC.HISFC.Fee.Models.Undrug GetUndrugExecInfo(string itemCode)
        {
            FS.SOC.HISFC.Fee.Models.Undrug item = null;
            if (dicUndrugExec.ContainsKey(itemCode))
            {
                item = dicUndrugExec[itemCode];
            }
            else
            {
                FS.SOC.HISFC.Fee.BizLogic.Undrug undrugMgr = new FS.SOC.HISFC.Fee.BizLogic.Undrug();
                item = undrugMgr.GetExecInfo(itemCode);

                dicUndrugExec.Add(itemCode, item);
            }

            return item;
        }
        #endregion

        /// <summary>
        /// 根据项目列表中的项目获取该项目的默认执行科室
        /// </summary>
        /// <param name="item"></param>
        /// <param name="deptCode"></param>
        /// <param name="deptName"></param>
        /// <returns></returns>
        private int GetItemDept(FS.HISFC.Models.Base.Item itemInfo, ref string deptCode, ref string deptName)
        {
            ArrayList alExecDept = new ArrayList();
            string defaultExecDept = string.Empty;
            FS.SOC.HISFC.Fee.Models.Undrug item = GetUndrugExecInfo(itemInfo.ID);
            if (string.IsNullOrEmpty(item.ExecDept)
                       || item.ExecDept == "ALL")
            {
                alExecDept = null;
                defaultExecDept = item.DefaultExecDeptForIn;
                deptCode = defaultExecDept;
                deptName = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(deptCode);
            }
            else
            {
                string[] depts = item.ExecDept.Split('|');
                alExecDept = new ArrayList();
                string firstDept = "";
                for (int i = 0; i < depts.Length; i++)
                {
                    FS.HISFC.Models.Base.Department deptObj = SOC.HISFC.BizProcess.Cache.Common.GetDeptInfo(depts[i]);
                    if (deptObj != null)
                    {
                        alExecDept.Add(deptObj);
                        if (string.IsNullOrEmpty(firstDept))
                        {
                            firstDept = deptObj.ID;
                        }
                    }
                }

                if (item.ExecDept.Contains(this.patientInfo.PVisit.PatientLocation.Dept.ID))
                {
                    defaultExecDept = this.patientInfo.PVisit.PatientLocation.Dept.ID;
                    deptName = this.patientInfo.PVisit.PatientLocation.Dept.Name;
                    deptCode = this.patientInfo.PVisit.PatientLocation.Dept.ID;
                }
                else
                {
                    defaultExecDept = item.DefaultExecDeptForIn;
                    deptCode = item.DefaultExecDeptForIn;
                    deptName = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(deptCode);
                }

                if (string.IsNullOrEmpty(defaultExecDept)
                    && !string.IsNullOrEmpty(firstDept))
                {
                    defaultExecDept = firstDept;
                    deptCode = firstDept;
                    deptName = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(deptCode);
                }
            }

            if (string.IsNullOrEmpty(defaultExecDept))
            {
                defaultExecDept = this.patientInfo.PVisit.PatientLocation.Dept.ID;
                deptCode = this.patientInfo.PVisit.PatientLocation.Dept.ID;
                deptName = this.patientInfo.PVisit.PatientLocation.Dept.Name;
            }

            return 0;
        }

        #region 原有方式屏蔽
        /// <summary>
        /// 根据项目列表中的项目获取该项目的默认执行科室
        /// </summary>
        /// <param name="item">项目实体</param>
        /// <param name="deptID">科室编码</param>
        /// <param name="deptName">科室名称</param>
        /// <returns></returns>
        //private int GetItemDept(FS.HISFC.Models.Base.Item item, ref string deptCode, ref string deptName)
        //{
        //    if (item is FS.HISFC.Models.Fee.Item.Undrug)
        //    {
        //        //获得非药品默认的执行科室
        //        deptCode = (item as FS.HISFC.Models.Fee.Item.Undrug).ExecDept;
        //        if (deptCode == null || deptCode == string.Empty)
        //        {
        //            if (this.defaultExeDept != null && this.defaultExeDept != string.Empty)
        //            {
        //                deptCode = this.defaultExeDept;
        //                FS.HISFC.Models.Base.Department dept = this.departmentManager.GetDeptmentById(this.defaultExeDept);
        //                if (dept == null)
        //                {
        //                    deptName = "(无)";
        //                }
        //                else
        //                {
        //                    deptName = dept.Name;
        //                }
        //            }
        //            else
        //            {
        //                if (this.patientInfo != null)
        //                {
        //                    deptName = this.patientInfo.PVisit.PatientLocation.Dept.Name;
        //                    deptCode = this.patientInfo.PVisit.PatientLocation.Dept.ID;
        //                }
        //            }
        //        }
        //        else
        //        {
        //            //拆分科室串，多个科室的话，默认取第一个
        //            int index = deptCode.IndexOf("|");
        //            if (index < 0)
        //            {
        //                index = deptCode.Length;
        //            }
        //            deptCode = deptCode.Substring(0, index);

        //            FS.HISFC.Models.Base.Department dept = this.departmentManager.GetDeptmentById(deptCode);
        //            if (dept == null)
        //            {
        //                deptName = "(无)";
        //            }
        //            else
        //            {
        //                deptName = dept.Name;
        //            }
        //        }
        //    }
        //    else if (item is FS.HISFC.Models.Pharmacy.Item)
        //    {
        //        //获得药品的执行科室
        //        if (this.defaultExeDept != null && this.defaultExeDept != string.Empty)
        //        {
        //            deptCode = this.defaultExeDept;
        //            FS.HISFC.Models.Base.Department dept = this.departmentManager.GetDeptmentById(this.defaultExeDept);
        //            if (dept == null)
        //            {
        //                deptName = "(无)";
        //            }
        //            else
        //            {
        //                deptName = dept.Name;
        //            }
        //        }
        //        else
        //        {
        //            if (this.patientInfo != null)
        //            {
        //                deptName = this.patientInfo.PVisit.PatientLocation.Dept.Name;
        //                deptCode = this.patientInfo.PVisit.PatientLocation.Dept.ID;
        //            }
        //        }
        //    }
        //    else if (item is FS.HISFC.Models.FeeStuff.MaterialItem)
        //    {
        //        if (tempDept != null)
        //        {
        //            deptName = tempDept.Name;
        //            deptCode = tempDept.ID;
        //        }
        //        else if (this.patientInfo != null)
        //        {
        //            deptName = this.patientInfo.PVisit.PatientLocation.Dept.Name;
        //            deptCode = this.patientInfo.PVisit.PatientLocation.Dept.ID;
        //        }
        //        else
        //        {
        //            deptName = this.operObj.Dept.Name;
        //            deptCode = this.operObj.Dept.ID;
        //        }
        //    }
        //    return 0;
        //}
        #endregion

        /// <summary>
        /// 处理this.fpDetail，项目名称的回车
        /// </summary>
        /// <returns>成功 1 失败 -1</returns>
        private int ProcessItem()
        {
            if (this.ucItemList.Visible == false)
            {
                this.ucItemList.Visible = true;

                return 0;
            }
            try
            {
                Item item = new Item();

                int returnValue = this.ucItemList.GetSelectItem(out item);
                if (returnValue == -1 || returnValue == 0)
                {
                    return -1;
                }
                int currRow = this.fpDetail_Sheet.ActiveRowIndex;
                
                if (currRow < 0)
                {
                    return -1;
                }
                if (this.fpDetail_Sheet.GetText(currRow, (int)Columns.IsNew) == "0")
                {
                    return -1;
                }
                //{D18CDB1B-BB1E-422d-9161-65D9CEC79C05}
                string orderitemname = item.Name.ToString();
                if (orderitemname.Contains("新生儿") && (DateTime.Now.Date.Subtract(this.patientInfo.Birthday.Date).Days + 1) > 365 * 16)//非新生儿
                {
                    if (MessageBox.Show("新生儿项目应该开在BB身上,不符合规范", "是否确认要这样操作？", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    {
                        this.fpDetail.StopCellEditing();
                        this.fpDetail_Sheet.Rows.Remove(currRow, 1);
                        return -1;
                    }
                }
             
                if (item.User01 == "[组套]")
                {
                    //组套特殊处理 如果屏蔽掉 将不能调用组套  zhangjunyi@FS.com 修改
                    if (this.AddGroupDetail(item.ID, currRow) == -1)
                    {
                        return -1;
                    }
                }
                else if (item.User01 == "[复合项]")
                {
                    string deptid = string.Empty;
                    if (item.User02 != null && item.User02 != string.Empty)
                    {
                        //拆分复合项执行科室串，多个科室的话，默认取第一个
                        int index = item.User02.IndexOf("|");
                        if (index < 0) index = item.User02.Length;

                        deptid = item.User02.Substring(0, index);
                    }

                    //{F4912030-EF65-4099-880A-8A1792A3B449}
                    if (this.isSplitUndrugCombItem)
                    {
                        this.AddCompoundDetail(item.ID, item.Name, currRow, deptid);
                    }
                    else 
                    {
                        if (item.Price == 0 && !this.isChargeZero)
                        {
                            MessageBox.Show(Language.Msg("价格为0的项目") + "[" + item.Name + "]" + Language.Msg("不允许收费!"));

                            return -1;
                        }
                        item.Qty = 1;
                        item.User03 = "1";//默认所有项目的付数都为1
                        //添加划价明细
                        this.AddChargeDetail(item, currRow, string.Empty);
                    }
                    //{F4912030-EF65-4099-880A-8A1792A3B449}结束
                }
                else
                {
                    if (item.Price == 0 && !this.isChargeZero)
                    {
                        MessageBox.Show(Language.Msg("价格为0的项目") + "[" + item.Name + "]" + Language.Msg("不允许收费!"));

                        return -1;
                    }
                    item.Qty = 1;
                    item.User03 = "1";//默认所有项目的付数都为1
                    //添加划价明细
                    this.AddChargeDetail(item, currRow, string.Empty);
                }

                this.Sum();//显示汇总
                this.ucItemList.Visible = false;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                this.fpDetail.Focus();

                return -1;
            }

            return 0;
        }

        //处理this.fpDetail,价格、数量、草药付数的回车
        private int SetItemProperty()
        {
            int row = this.fpDetail_Sheet.ActiveRowIndex;
            if (row < 0)
            {
                return -1;
            }

            this.fpDetail.StopCellEditing();

            object obj = new object();
            obj = this.fpDetail_Sheet.GetValue(row, (int)Columns.ItemObject);
            if (obj == null)
            {
                return -1;
            }

            ////价格{2C7FCD3D-D9B4-44f5-A2EE-A7E8C6D85576}
            string text = string.Empty;
            //string text = this.fpDetail_Sheet.GetText(row, (int)Columns.Price);
            //if (text == null || text == string.Empty)
            //{
            //    text = "0";
            //}
            //decimal price = NConvert.ToDecimal(text);
            
            //数量
            text = this.fpDetail_Sheet.GetText(row, (int)Columns.Qty);
            if (text == null || text == string.Empty)
            {
                text = "0";
            }
            decimal qty = NConvert.ToDecimal(text);
            
            //付数
            text = this.fpDetail_Sheet.GetText(row, (int)Columns.Day);
            if (text == null || text == string.Empty)
            {
                text = "0";
            }

            decimal day = NConvert.ToDecimal(text);

          



            //与收费、退费等费用的计算方法保持一致{F98CC89C-BE9A-49ca-98E2-4C700A8F5E34}
            //this.fpDetail_Sheet.SetValue(row, (int)Columns.TotCost, FS.FrameWork.Public.String.FormatNumber(price * qty * day, 2), false);
            Item item = this.fpDetail_Sheet.Rows[row].Tag as Item;
            Item itemClone = this.fpDetail_Sheet.Cells[row, (int)Columns.feeRate].Tag as Item;

            //{2C7FCD3D-D9B4-44f5-A2EE-A7E8C6D85576}
            //比例
            text = this.fpDetail_Sheet.GetText(row, (int)Columns.feeRate);
            if (text == null || text == string.Empty)
            {
                text = "1";
            }
            decimal feeRate = NConvert.ToDecimal(text);

            

            if (feeRate <= 0)
            {
                MessageBox.Show("比例不能小于或等于0");
                this.fpDetail.Focus();
                this.fpDetail_Sheet.SetActiveCell(row, (int)Columns.feeRate, true);
                return -1;
            }



            //{2C7FCD3D-D9B4-44f5-A2EE-A7E8C6D85576}
            //根据比例处理单价
           // item.Price = itemClone.Price * feeRate;

            //this.fpDetail_Sheet.Cells[row, (int)Columns.Price].Text = item.Price.ToString();
            //价格{2C7FCD3D-D9B4-44f5-A2EE-A7E8C6D85576}
            text = this.fpDetail_Sheet.GetText(row, (int)Columns.Price);
            if (text == null || text == string.Empty)
            {
                text = "0";
            }
            decimal price = NConvert.ToDecimal(text);


            if (itemClone.Price == 0)
            {
                item.Price = NConvert.ToDecimal(this.fpDetail_Sheet.GetText(row, (int)Columns.Price)) * feeRate;
                itemClone.Price = item.Price;
            }
            else
            {
                item.Price = itemClone.Price * feeRate;
                this.fpDetail_Sheet.Cells[row, (int)Columns.Price].Text = item.Price.ToString();
            }
            if (item.ItemType == EnumItemType.Drug)//药品
            {
             
                this.fpDetail_Sheet.SetValue(row, (int)Columns.TotCost, FS.FrameWork.Public.String.FormatNumber((item.Price * qty / item.PackQty) * day, 2), false);

            }
            else
            {
                
                this.fpDetail_Sheet.SetValue(row, (int)Columns.TotCost, FS.FrameWork.Public.String.FormatNumber((item.Price * qty) * day, 2), false);
            }

            this.Sum();//计算合计

            if (!this.IsChargeZero && price <= 0)
            {
                MessageBox.Show(Language.Msg("项目价格不能小于或者等于零!"));
                this.fpDetail.Focus();
                this.fpDetail_Sheet.SetActiveCell(row, (int)Columns.Price, true);

                return -1;
            }

            if (qty <= 0 && isJudgeQty)
            {
                MessageBox.Show(Language.Msg("开立数量不能小于或者等于零!"));
                this.fpDetail.Focus();
                this.fpDetail_Sheet.SetActiveCell(row, (int)Columns.Qty, true);

                return -1;
            }

            if (day <= 0)
            {
                MessageBox.Show(Language.Msg("草药付数不能小于或者等于零!"));
                this.fpDetail.Focus();
                this.fpDetail_Sheet.SetActiveCell(row, (int)Columns.Day, true);

                return -1;
            }

            return 0;
        }		

        private int SetItem(int row, PayTypes payType, string recipeDeptCode, DateTime operTime, ref bool isNewItem, ref FeeItemList feeItemList)
        {
            object obj = this.fpDetail_Sheet.GetValue(row, (int)Columns.ItemObject);
            if (obj == null)
            {
                return 0;
            }
            FS.HISFC.Models.Base.Employee operObj = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Clone();
            feeItemList = (FeeItemList)obj;
            feeItemList.FeeOper.ID = operObj.ID;
            feeItemList.FeeOper.Dept.ID = operObj.Dept.ID;
            feeItemList.Item.SpecialFlag2 = "0";
            feeItemList.FTSource = this.ftSource;
            //{2C7FCD3D-D9B4-44f5-A2EE-A7E8C6D85576}
            feeItemList.FTRate.ItemRate = FS.FrameWork.Function.NConvert.ToDecimal( this.fpDetail_Sheet.GetText(row, (int)Columns.feeRate));
            if (this.isJudgeValid) 
            {
                //if (feeItemList.Item.IsPharmacy)
                if (feeItemList.Item.ItemType == EnumItemType.Drug)
                {
                    //FS.HISFC.Models.Pharmacy.Item phItem = this.pharmacyIntergrate.GetItem(feeItemList.Item.ID);
                    //if (phItem == null)
                    //{
                    //    MessageBox.Show("获得药品基本信息失败!" + this.pharmacyIntergrate.Err);

                    //    return -1;
                    //}

                    //if (phItem.ValidState != "0")
                    //{
                    //    MessageBox.Show(phItem.Name + "已经停用!请重新选择有效的项目");

                    //    return -1;
                    //}
                }
                else if(feeItemList.Item.ItemType == EnumItemType.UnDrug)
                {
                    FS.HISFC.Models.Fee.Item.Undrug undrug = this.undrugManager.GetItemByUndrugCode(feeItemList.Item.ID);
                    if (undrug == null)
                    {
                        MessageBox.Show("获得非药品基本信息失败!" + this.undrugManager.Err);
                        return -1;
                    }
                    if (undrug.ValidState!="1") 
                    {
                        MessageBox.Show(feeItemList.Item.Name + "已经停用!请重新选择有效的项目");
                        return -1;
                    }

                    feeItemList.IsGroup = undrug.UnitFlag == "1" ? true : false;
                }
            }

            feeItemList.Days = NConvert.ToDecimal(this.fpDetail_Sheet.GetText(row, (int)Columns.Day));

            if (feeItemList.Days == 0) 
            {
                feeItemList.Days = 1;
            }
            //数量,药品转换为最小单位数量
            //if (feeItemList.Item.IsPharmacy)
            if (feeItemList.Item.ItemType == EnumItemType.Drug)    
            {	//项目单位为最小单位,如果和界面上的单位相同,证明收费的单位为最小单位
                if (feeItemList.Item.PriceUnit == this.fpDetail_Sheet.GetText(row, (int)Columns.Unit))
                {
                    feeItemList.Item.Qty = NConvert.ToDecimal(this.fpDetail_Sheet.GetText(row, (int)Columns.Qty));
                }
                else//否则为包装单位,转换为最小单位
                {
                    feeItemList.Item.Qty = NConvert.ToDecimal(this.fpDetail_Sheet.GetText(row, (int)Columns.Qty)) * feeItemList.Item.PackQty;
                }
            }
            else
            {
                feeItemList.Item.Qty = NConvert.ToDecimal(this.fpDetail_Sheet.GetText(row, (int)Columns.Qty));
            }
            //将数量乘以草药付数,保存总量
            feeItemList.Item.Qty = feeItemList.Item.Qty * feeItemList.Days;

            //价格,药品价格在数据库中是包装单位价格
            if (feeItemList.Item.Price == 0)
            {
                feeItemList.Item.Price = NConvert.ToDecimal(this.fpDetail_Sheet.GetText(row, (int)Columns.Price));
            }

            //计算总额
            if (feeItemList.Item.ItemType == EnumItemType.Drug)
            {
                //feeItemList.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(feeItemList.Item.Price * feeItemList.Item.Qty / feeItemList.Item.PackQty, 2);
                //feeItemList.FT.DefTotCost = FS.FrameWork.Public.String.FormatNumber(SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(feeItemList.Item.ID).PriceCollection.RetailPrice * feeItemList.Item.Qty / feeItemList.Item.PackQty, 2);
                
                //{D48F1F6E-8792-4b73-884C-1FD26482DC60}
                //与护士站、退费等费用的计算方法保持一致
                //新计价规则，先计算出单片价格进行4舍5入，再求总价
                //住院结算会针对收费数量进行拆分，若不如此处理，则拆分出来的费用条目金额会有出入
                feeItemList.FT.TotCost = FS.FrameWork.Public.String.FormatNumber((feeItemList.Item.Price / feeItemList.Item.PackQty), 2) * feeItemList.Item.Qty;
                feeItemList.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(feeItemList.FT.TotCost, 2);
                feeItemList.FT.DefTotCost = FS.FrameWork.Public.String.FormatNumber((feeItemList.Item.DefPrice / feeItemList.Item.PackQty), 2) * feeItemList.Item.Qty;
                feeItemList.FT.DefTotCost = FS.FrameWork.Public.String.FormatNumber(feeItemList.FT.DefTotCost, 2);
            
            }
            else
            {
                //feeItemList.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(feeItemList.Item.Price * feeItemList.Item.Qty, 2);

                //{D48F1F6E-8792-4b73-884C-1FD26482DC60}
                //与护士站、退费等费用的计算方法保持一致
                //新计价规则，先计算出单片价格进行4舍5入，再求总价
                //住院结算会针对收费数量进行拆分，若不如此处理，则拆分出来的费用条目金额会有出入
                feeItemList.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(feeItemList.Item.Price, 2) * feeItemList.Item.Qty;
                feeItemList.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(feeItemList.FT.TotCost, 2);
            }

            feeItemList.FT.OwnCost = feeItemList.FT.TotCost;

            if (this.fpDetail_Sheet.GetText(row, (int)Columns.IsNew) == "1")
            {
                feeItemList.TransType = TransTypes.Positive;
                feeItemList.Patient = this.patientInfo.Clone();
                feeItemList.RecipeOper.ID = this.recipeDoctCode;
                feeItemList.RecipeOper.Dept.ID = recipeDeptCode;
                feeItemList.PayType = PayTypes.Charged;
                feeItemList.ChargeOper.ID = this.inpatientManager.Operator.ID;
                feeItemList.ChargeOper.OperTime = operTime;
                feeItemList.BalanceNO = 0;
                feeItemList.BalanceState = "0";
                isNewItem = true;
                if (payType == PayTypes.Balanced)
                {
                    feeItemList.PayType = PayTypes.Balanced;
                    feeItemList.FeeOper.ID = this.inpatientManager.Operator.ID;
                    //feeItemList.FeeOper.OperTime = operTime;
                    feeItemList.FeeOper.OperTime = NConvert.ToDateTime(this.fpDetail_Sheet.GetText(row, (int)Columns.feeDate));//获取界面的计费日期
                    feeItemList.NoBackQty = feeItemList.Item.Qty;
                }
            }
            else
            {
                isNewItem = false;
            }

            return 1;
        }

        /// <summary>
        ///删除一行
        /// </summary>
        /// <param name="row">要删除的行号</param>
        private void RemoveRow(int row) 
        {
            this.fpDetail.EditChange -= new FarPoint.Win.Spread.EditorNotifyEventHandler(this.fpDetail_EditChange);

            for (int i = 0; i < this.fpDetail_Sheet.Columns.Count; i++)
            {
                this.fpDetail_Sheet.Cells[row, i].Tag = string.Empty;
                this.fpDetail_Sheet.Cells[row, i].Text = string.Empty;
            }

            this.fpDetail.EditChange += new FarPoint.Win.Spread.EditorNotifyEventHandler(this.fpDetail_EditChange);

            this.fpDetail_Sheet.Rows.Remove(row, 1);
        }

        /// <summary>
        /// 根据非要品编码获取非药品执行科室{CA82280B-51B6-4462-B63E-43F4ECF456A3}
        /// </summary>
        /// <param name="list"></param>
        public string SetExecDept(string itemID)
        {
            string id = string.Empty;
            ArrayList undrugDept = new ArrayList();
            if (dictDept.Count != 0)
            {
                foreach (string itemCode in dictDept.Keys)
                {
                    if (itemCode.Contains(itemID))
                    {
                        undrugDept.Add(dictDept[itemCode]);
                        if (dictDept[itemCode].User02 == "1")
                        {
                            id = dictDept[itemCode].ID;
                        }
                    }
                }
                if (undrugDept.Count != 0)
                {
                    this.lbDept.AddItems(undrugDept);
                }
                else
                {
                    this.lbDept.AddItems(unDrugDeptList);
                }
            }
            return id;
        }

        #endregion

        #region 公有方法

        /// <summary>
        /// 保存函数
        /// </summary>
        /// <returns></returns>
        public virtual int Save() 
        {
            int returnValue = 0;
            feeIntergrate.MessageType = this.MessageType;
            switch (this.feeType) 
            {
                case FeeTypes.划价:
                    returnValue = this.Charge();
                    break;
                case FeeTypes.收费:
                    returnValue = this.Fee();
                    break;
            }

            return returnValue;
        }
        #region 手术暂存功能 
        /// <summary>
        /// 暂存功能  先将明细存到临时表中
        /// </summary>
        /// <returns></returns>
        public int TemparorySave()
        {
            FS.HISFC.BizLogic.Fee.TemporaryFee temFee = new FS.HISFC.BizLogic.Fee.TemporaryFee();
            //获取明细 
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.inpatientManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.personManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            //操作时间
            DateTime operTime = this.inpatientManager.GetDateTimeFromSysDateTime();
            //执行科室
            string recipeDept = this.recipeDept.ID;
            ArrayList feeList = new ArrayList();
              //循环处理
            for (int i = 0; i < this.fpDetail_Sheet.RowCount; i++)
            {
                FeeItemList feeItemList = new FeeItemList();
                bool isNew = false;
                int returnValue = 0;

                //项目信息赋值
                returnValue = this.SetItem(i, PayTypes.Charged, recipeDept, operTime, ref isNew, ref feeItemList);

                //如果获得的项目信息为空,不处理
                if (returnValue == 0)
                {
                    continue;
                }

                //{0604764A-3F55-428f-9064-FB4C53FD8136}
                //增加手术编码
                if (this.OperationNO != string.Empty)
                {
                    feeItemList.OperationNO = this.OperationNO;
                }
                feeList.Add(feeItemList);
            }
            try
            {
                //先删除后增加  
                int ret = temFee.Delete(this.patientInfo.ID, recipeDept, this.OperationNO);
                if (ret < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("删除旧的收费明细出错");
                    return -1;
                }
                int i = 0;
                foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList obj in feeList)
                {
                    i++;
                    if (temFee.Insert(obj, this.patientInfo.ID, recipeDept, this.OperationNO,i.ToString()) < 1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("插入明细失败" + temFee.Err);
                        return -1;
                    }
                }
                FS.FrameWork.Management.PublicTrans.Commit();
                MessageBox.Show("保存成功");
            }
            catch (Exception ex)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(ex.Message);
                return -1;
            }
            return 1;

        }

        /// <summary>
        /// 显示暂存数据 
        /// </summary>
        /// <returns> 0 没有暂存过数据 ,1 暂存过数据并且正确显示, -1 程序出错</returns>
        public int SetValue()
        {
            FS.HISFC.BizLogic.Fee.TemporaryFee temfee = new FS.HISFC.BizLogic.Fee.TemporaryFee();
            ArrayList list = null;
            //开方科室
           
            string recipeDept = this.recipeDept.ID;
            if (this.OperationNO == "") //手术编号  如果
            {
                list = temfee.Query(this.patientInfo.ID, recipeDept);
            }
            else
            {
                list = temfee.Query(this.patientInfo.ID, recipeDept, this.OperationNO);
            }
            if (list == null)
            {
                MessageBox.Show("查询明细失败" + temfee.Err);
                return -1;
            }
            if (list.Count == 0)
            {
                return 0;
            }
            if (this.fpDetail_Sheet.Rows.Count > 0) //先删除数据　
            {
                this.fpDetail_Sheet.Rows.Remove(0, this.fpDetail_Sheet.Rows.Count);
            }
            decimal price = 0;
            int row = 0;
            foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList item in list)
            {
                if (item.Item.ID.Substring(0, 1).ToUpper() == "Y")
                {
                    #region 药品
                    //根据药品id获取药品实体
                    FS.HISFC.Models.Pharmacy.Item drug = null;
                    FS.HISFC.BizLogic.Pharmacy.Item drugManager = new FS.HISFC.BizLogic.Pharmacy.Item();
                    drug=drugManager.GetItem(item.Item.ID);
                    if (drug == null || drug.ID == "") continue;
                    decimal itemQty = item.Item.Qty;
                    item.Item = drug;
                    item.Days = 1;//付数
                    item.Item.Qty = itemQty;
                    price = drug.Price;
                   
                    #endregion
                }
                else if (item.Item.ID.Substring(0, 1).ToUpper() == "F")
                {
                    #region 非药品
                    //根据非药品id获取非药品实体
                    FS.HISFC.Models.Fee.Item.Undrug undrug = null;
                    FS.HISFC.BizLogic.Fee.Item undrugManager = new FS.HISFC.BizLogic.Fee.Item();
                    undrug = this.feeIntergrate.GetUndrugByCode(item.Item.ID);
                    if (undrug == null) continue;//||undrug.ValidState==false)continue;
                    decimal itemQty = item.Item.Qty;

                    //重新取价格
                    decimal orgPrice = 0;

                    if (this.feeIntergrate.GetPriceForInpatient(this.patientInfo, undrug, ref price, ref orgPrice) == -1)
                    {
                        MessageBox.Show(Language.Msg("取项目:") + item.Name + Language.Msg("的价格出错!") + this.pactUnitManager.Err);

                        return -1;
                    }
                    undrug.Price = price;
                    undrug.DefPrice = orgPrice;

                    item.Item = undrug;
                    item.IsGroup = undrug.UnitFlag == "1" ? true : false;
                    item.Item.Qty = itemQty;
                    item.Days = 1;//付数
                    price = undrug.Price;

                    #endregion
                }
                #region 赋值
                this.fpDetail_Sheet.Rows.Add(row, 1); //增加一行
                //药品默认按最小单位收费,显示价格也为最小单位价格,存入数据库的为包装单位价格
                //if (item.IsPharmacy)//药品
                if (item.Item.ItemType == EnumItemType.Drug)//药品
                {
                    price = FS.FrameWork.Public.String.FormatNumber(item.Item.Price / item.Item.PackQty, 4);
                    this.fpDetail_Sheet.SetValue(row, (int)Columns.Price, price, false);
                }
                else//非药品
                {
                    this.fpDetail_Sheet.SetValue(row, (int)Columns.Price, item.Item.Price, false);
                    price = item.Item.Price;
                }

                //存储项目实体便于价格等计算{F98CC89C-BE9A-49ca-98E2-4C700A8F5E34}
                this.fpDetail_Sheet.Rows[row].Tag = item.Item;
                //{30B79077-CDC0-4de8-822A-8B04ABB2925C}
                this.fpDetail_Sheet.Cells[row, (int)Columns.feeRate].Tag = item.Item.Clone();
                //国标码
                this.fpDetail_Sheet.SetValue(row, (int)Columns.GbCode, item.Item.GBCode, false);
                //项目名称,和规格显示在一起
                if (item.Item.Specs != null && item.Item.Specs != string.Empty)
                {
                    this.fpDetail_Sheet.SetValue(row, (int)Columns.ItemName, item.Item.Name + "{" + item.Item.Specs + "}", false);
                }
                else
                {
                    this.fpDetail_Sheet.SetValue(row, (int)Columns.ItemName, item.Item.Name, false);
                }
                //数量
                this.fpDetail_Sheet.SetValue(row, (int)Columns.Qty, item.Item.Qty, false);

                //药品可选择药品收费单位,默认为最小单位  //单位
                if (item.Item.ItemType == EnumItemType.Drug)
                {
                    FarPoint.Win.Spread.CellType.ComboBoxCellType comboType = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
                    comboType.Editable = true;
                    comboType.Items = (new string[]{(item.Item as FS.HISFC.Models.Pharmacy.Item).MinUnit,
                                                (item.Item as FS.HISFC.Models.Pharmacy.Item).PackUnit});
                    this.fpDetail_Sheet.Cells[row, (int)Columns.Unit].CellType = comboType;
                    this.fpDetail_Sheet.Cells[row, (int)Columns.Unit].Locked = false;
                    if (item.Item.MinFee.User03 == "2")
                    {
                        this.fpDetail_Sheet.SetValue(row, (int)Columns.Unit, ((FS.HISFC.Models.Pharmacy.Item)item.Item).PackUnit, false);
                        item.Item.PriceUnit = ((FS.HISFC.Models.Pharmacy.Item)item.Item).PackUnit;
                        price = FS.FrameWork.Public.String.FormatNumber(item.Item.Price, 4);
                        this.fpDetail_Sheet.SetValue(row, (int)Columns.Price, price, false);
                    }
                    else
                    {
                        price = FS.FrameWork.Public.String.FormatNumber(item.Item.Price / item.Item.PackQty, 4);
                        this.fpDetail_Sheet.SetValue(row, (int)Columns.Price, price, false);
                        this.fpDetail_Sheet.SetValue(row, (int)Columns.Unit, ((FS.HISFC.Models.Pharmacy.Item)item.Item).MinUnit, false);
                        item.Item.PriceUnit = ((FS.HISFC.Models.Pharmacy.Item)item.Item).MinUnit;
                    }

                }
                else//非药品
                {
                    FarPoint.Win.Spread.CellType.TextCellType textType = new FarPoint.Win.Spread.CellType.TextCellType();
                    this.fpDetail_Sheet.Cells[row, (int)Columns.Unit].CellType = textType;
                    this.fpDetail_Sheet.Cells[row, (int)Columns.Unit].Locked = true;
                    this.fpDetail_Sheet.SetValue(row, (int)Columns.Unit, item.Item.PriceUnit, false);
                }
                //草药付数
                this.fpDetail_Sheet.SetValue(row, (int)Columns.Day, 1, false);

                if (item.Item.ItemType == EnumItemType.Drug && item.Item.SysClass.ID.ToString() == "PCC")
                {
                    this.fpDetail_Sheet.Cells[row, (int)Columns.Day].Locked = false;
                    this.fpDetail_Sheet.Cells[row, (int)Columns.Day].ForeColor = Color.Black;
                }
                else
                {
                    this.fpDetail_Sheet.Cells[row, (int)Columns.Day].Locked = true;
                    this.fpDetail_Sheet.Cells[row, (int)Columns.Day].ForeColor = Color.Transparent;
                }
                
                //总额
                this.fpDetail_Sheet.SetValue(row, (int)Columns.TotCost, price * item.Item.Qty, false);
                //新录入的项目			
                this.fpDetail_Sheet.SetValue(row, (int)Columns.IsNew, "1", false);
                //标识药品、非药品
                //if (item.IsPharmacy)
                if (item.Item.ItemType == EnumItemType.Drug)
                {
                    this.fpDetail_Sheet.SetValue(row, (int)Columns.IsDrug, "1", false);
                }
                else
                {
                    this.fpDetail_Sheet.SetValue(row, (int)Columns.IsDrug, "0", false);
                }
                //{2C7FCD3D-D9B4-44f5-A2EE-A7E8C6D85576}
                this.fpDetail_Sheet.SetValue(row, (int)Columns.feeRate, 1);
                string deptCode = string.Empty, deptName = string.Empty;
                if (!defaultExeDeptIsDeptIn)
                {
                    #region 获取默认执行科室
                    //获取项目默认执行科室
                    //string execDeptCode = recipeDept;
                    //if (execDeptCode == null || execDeptCode == string.Empty)
                    //{
                    //    this.GetItemDept(item.Item, ref deptCode, ref deptName);
                    //}
                    //else
                    //{
                    //    FS.HISFC.Models.Base.Department dept = this.departmentManager.GetDeptmentById(execDeptCode);
                    //    deptCode = execDeptCode;
                    //    if (dept == null)
                    //    {
                    //        deptName = "(无)";
                    //    }
                    //    else
                    //    {
                    //        deptName = dept.Name;
                    //    }
                    //}

                    ////{CA82280B-51B6-4462-B63E-43F4ECF456A3}
                    //execDeptCode = this.SetExecDept(item.ID);
                    //if (execDeptCode != "")
                    //{
                    //    FS.HISFC.Models.Base.Department dept = this.departmentManager.GetDeptmentById(execDeptCode);
                    //    deptCode = dept.ID;
                    //    deptName = dept.Name;
                    //}

                    #endregion
                    Employee employee = this.personManager.GetPersonByID(this.recipeDoctCode);

                    if (employee == null)
                    {
                        MessageBox.Show(Language.Msg("获得人员基本信息出错!"));

                        return -1;
                    }

                    if (this.recipeDept != null && this.recipeDept.ID != "")
                    {
                        employee.Dept = this.recipeDept;
                    }
                    else
                    {
                        this.recipeDept = employee.Dept;
                    }
                    if(item.Item.ItemType == EnumItemType.Drug)
                    {
                        ArrayList alExecDept = null;
                        FS.FrameWork.Models.NeuObject dept = this.managerIntegrate.GetDepartment(recipeDept);
                        deptCode = dept.ID;
                        deptName = dept.Name;
                        lbDept.Items.Clear();
                        SOC.HISFC.BizProcess.Cache.Common.GetDept("1");
                        alExecDept = SOC.HISFC.BizProcess.Cache.Common.deptHelper.ArrayObject;
                        lbDept.AddItems(alExecDept);
                    }
                    else
                    {
                        ArrayList alExecDept = null;
                        string defaultExecDept = string.Empty;
                        SOC.HISFC.BizProcess.Cache.Common.SetExecDept(false, recipeDept, item.Item.ID, ref defaultExecDept, ref alExecDept);
                        if (alExecDept == null || alExecDept.Count == 0)
                        {
                            SOC.HISFC.BizProcess.Cache.Common.GetDept("1");
                            alExecDept = SOC.HISFC.BizProcess.Cache.Common.deptHelper.ArrayObject;
                        }
                        FS.FrameWork.Models.NeuObject dept = this.managerIntegrate.GetDepartment(defaultExecDept);
                        deptCode = dept.ID;
                        deptName = dept.Name;
                        lbDept.Items.Clear();
                        lbDept.AddItems(alExecDept);
                    }
                }
                else
                {
                    deptCode = this.operObj.Dept.ID;
                    deptName = this.operObj.Dept.Name;
                }

                this.fpDetail_Sheet.SetValue(row, (int)Columns.Dept, deptName, false);
                //表示科室未修改
                this.fpDetail_Sheet.SetValue(row, (int)Columns.IsDeptChange, "0", false);

                //赋值给收费实体
                FeeItemList feeitemlist = new FeeItemList();
                feeitemlist.Item = item.Item;
                feeitemlist.ExecOper.Dept.ID = deptCode;
                feeitemlist.ExecOper.Dept.Name = deptName;
                feeitemlist.IsGroup = item.IsGroup;
                feeitemlist.Days = NConvert.ToInt32(item.User03);//草药付数
                //指定药品的摆药药房
                if (item is FS.HISFC.Models.Pharmacy.Item)
                {
                    feeitemlist.StockOper.Dept.ID = item.User02;
                }

                //保存复合项目
                feeitemlist.UndrugComb.ID = item.Item.MinFee.User01;
                feeitemlist.UndrugComb.Name = item.Item.MinFee.User02;

                feeitemlist.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(price * item.Item.Qty, 2);

                this.fpDetail_Sheet.SetValue(row, (int)Columns.ItemObject, feeitemlist, false);
                //{062CEAA8-16B8-4c25-B4CC-E6B24DE7D331}
                if (IAdptIllnessInPatient != null)
                {
                    int resultValue = IAdptIllnessInPatient.ProcessInpatientFeeDetail(this.patientInfo, ref feeitemlist);
                    if (resultValue < 0) return -1;
                }
                #endregion
            }

            this.fpDetail_Sheet.Rows.Add(this.fpDetail_Sheet.RowCount, 1);
            this.fpDetail_Sheet.SetValue(this.fpDetail_Sheet.RowCount-1, (int)Columns.Unit, "合计", false);
            this.Sum();//显示汇总
            return 0;
        }
        #endregion
        /// <summary>
        /// 在指定行处添加一行
        /// </summary>
        /// <param name="row">添加行索引</param>
        public virtual void AddRow(int row)
        {
            this.fpDetail_Sheet.Rows.Add(row, 1);
            this.fpDetail_Sheet.ActiveRowIndex = row;

            this.fpDetail_Sheet.Rows[this.fpDetail_Sheet.ActiveRowIndex].Height = 23;
            this.fpDetail.Focus();

            this.fpDetail_Sheet.SetActiveCell(this.fpDetail_Sheet.ActiveRowIndex, (int)Columns.ItemName);

            if (this.fpDetail_Sheet.RowCount > 1 && this.fpDetail_Sheet.GetValue(0, (int)Columns.ItemObject) != null)
            {
                this.rowCount = 1;
            }

            for (int i = 0; i < this.fpDetail_Sheet.RowCount; i++)
            {
                this.fpDetail_Sheet.Rows[i].Locked = false;
            }
            this.fpDetail_Sheet.Rows[this.fpDetail_Sheet.RowCount - 1].Locked = true;
        }

        /// <summary>
        /// 添加一行项目
        /// </summary>
        public virtual void AddRow()
        {
            this.AddRow(this.fpDetail_Sheet.RowCount - 1);
            this.fpDetail.Focus();

            this.fpDetail_Sheet.SetActiveCell(this.fpDetail_Sheet.ActiveRowIndex, (int)Columns.ItemName);
        }

        /// <summary>
        /// 组套// {84B3B88C-6501-495b-9F82-278358EF5DD5}
        /// </summary>
        public virtual void Group()
        {
            FS.HISFC.Components.Common.Forms.frmGroup frmGroup = new FS.HISFC.Components.Common.Forms.frmGroup();

            frmGroup.ShowDialog();

            string groupID = "";
            groupID = frmGroup.GroupID;


            this.InsertGroup(groupID);

        }
        /// <summary>
        /// // {84B3B88C-6501-495b-9F82-278358EF5DD5}
        /// </summary>
        /// <param name="groupID"></param>
        public void InsertGroup(string groupID)
        {
            FS.HISFC.Components.Common.Forms.frmChooseGroupDetails frm = new FS.HISFC.Components.Common.Forms.frmChooseGroupDetails();
            frm.GroupID = groupID;
            DialogResult dr = frm.ShowDialog();
            if (dr == DialogResult.Cancel)
            {
                return;
            }
            else
            {
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在加载组套信息");
                Application.DoEvents();
                this.AddGroupDetail(groupID, frm.AlReturnDetails);
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }

            frm.Dispose();
        }
        /// <summary>
        /// 初始化函数
        /// </summary>
        /// <returns>成功1 失败 -1</returns>
        public virtual int Init(string deptCode)
        {
            this.InitFP();
            //this.ucItemList = new ucItemList(this.itemKind);
            this.ucItemList = new ucItemList();
            this.ucItemList.enuShowItemType = this.itemKind;
            this.ucItemList.NoAddDrugQuality = this.noAddDrugQuality;
            this.Controls.Add(this.ucItemList);
            this.curDrugApplyDept = deptCode;
            this.ucItemList.Init(deptCode);
            //this.ucItemList.AddGroup(deptCode);
            this.ucItemList.Hide();
            this.ucItemList.BringToFront();
            this.ucItemList.SelectItem += new ucItemList.MyDelegate(ucItemList_SelectItem);
            this.fpDetail.CellClick += new CellClickEventHandler(fpDetail_CellClick);//{CA82280B-51B6-4462-B63E-43F4ECF456A3}
            InitDept();

            //{CA82280B-51B6-4462-B63E-43F4ECF456A3}
            ArrayList deptList = this.feeIntergrate.QueryDeptList("ALL", "2");
            foreach (FS.FrameWork.Models.NeuObject neuObj in deptList)
            {
                dictDept.Add(neuObj.Memo + "|" + neuObj.ID, neuObj);
            }

            allFeecodeStat = feeCodeStatMgr.QueryFeeCodeStatByReportCode("ZY01");
            if (allFeecodeStat != null && allFeecodeStat.Count > 0)
            {
                foreach (FS.HISFC.Models.Fee.FeeCodeStat statCode in allFeecodeStat)
                {
                    allFeecodeStatHelper.Add(statCode.MinFee.ID, statCode);
                }
            }

            return 1;
        }

        //{CA82280B-51B6-4462-B63E-43F4ECF456A3}
        void fpDetail_CellClick(object sender, CellClickEventArgs e)
        {
            if (e.Column == (int)Columns.Dept)
            {
                string deptCode = string.Empty;
                if ("N,I".Contains(SOC.HISFC.BizProcess.Cache.Common.GetDeptInfo(((FS.HISFC.Models.Base.Employee)this.itemManager.Operator).Dept.ID).DeptType.ID.ToString()))
                {
                    if (this.patientInfo != null)
                    {
                        deptCode = this.patientInfo.PVisit.PatientLocation.Dept.ID;
                    }
                    else
                    {
                        deptCode = SOC.HISFC.BizProcess.Cache.Common.GetEmployeeInfo(this.recipeDoctCode).Dept.ID;
                    }
                }
                else
                {
                    deptCode = this.operObj.Dept.ID;
                }
                if (this.fpDetail_Sheet.ActiveRow.Tag != null && this.fpDetail_Sheet.ActiveRow.Tag is Item)
                {
                    Item item = this.fpDetail_Sheet.ActiveRow.Tag as Item;
                    if (item.ItemType == EnumItemType.UnDrug)
                    {
                        ArrayList alExecDept = null;

                        string defaultExecDept = string.Empty;
                        lbDept.Items.Clear();
                        SOC.HISFC.BizProcess.Cache.Common.SetExecDept(false, deptCode, item.ID, ref defaultExecDept, ref alExecDept);
                        lbDept.AddItems(alExecDept);
                    }
                }
            }
        }

        public void ChangeDept(FrameWork.Models.NeuObject deptObj)
        {
            
            if (deptObj == null)
            {
                MessageBox.Show("参数传递失败！");
                return;
            }
            if (tempDept != null && tempDept == deptObj) return;

            int resultValue = this.ucItemList.RefreshDataSet(deptObj.ID);
            if (resultValue < 0)
            {
                MessageBox.Show("查找物资信息失败！");
                return;
            }
            tempDept = deptObj;
            this.Focus();
        }
        
        /// <summary>
        /// 删除一行项目
        /// </summary>
        /// <returns>成功: 1 失败: -1</returns>
        public virtual int DelRow()
        {
            int row = this.fpDetail_Sheet.ActiveRowIndex;

            if (this.fpDetail_Sheet.RowCount == 0)
            {
                return 0;
            }
            if (row == this.fpDetail_Sheet.RowCount - 1)
            {
                return 0;
            }

            row++;

            DialogResult result = MessageBox.Show(Language.Msg("是否删除第") + row.ToString() + Language.Msg("行?"),
                Language.Msg("提示"), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.No)
            {
                this.fpDetail.Focus();

                return 1;
            }

            row--;

            //获得该项目是否为新录入项目
            string newItem = this.fpDetail_Sheet.GetText(row, (int)Columns.IsNew);

            //新录入项目直接删除
            if (newItem == string.Empty || newItem == "1")
            {
                this.fpDetail.StopCellEditing();
                this.fpDetail_Sheet.Rows.Remove(row, 1);
                row = this.fpDetail_Sheet.ActiveRowIndex;
                this.fpDetail_Sheet.SetActiveCell(row, 0);
                
                if (this.fpDetail_Sheet.RowCount == 1) 
                {
                    this.AddRow(0);
                }
            }
            else//由数据库内检索的项目
            {
                object obj = this.fpDetail_Sheet.GetValue(row, (int)Columns.ItemObject);

                if (obj == null)
                {
                    return -1;
                }

                //需确认的医嘱，只删除界面明细，不处理数据库
                if (obj is FS.HISFC.Models.Order.ExecOrder)
                {
                    this.fpDetail_Sheet.Rows.Remove(row, 1);
                }
                //收费项目处理
                //操作数据库，删除划价明细
                else if (obj is FeeItemList)
                {
                    FS.FrameWork.Management.PublicTrans.BeginTransaction();
                    //Transaction t = new Transaction(this.inpatientManager.Connection);
                    //t.BeginTransaction();
                    this.inpatientManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                    if (this.inpatientManager.DeleteChargeInfo((FeeItemList)obj) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(Language.Msg("删除明细失败!") + this.inpatientManager.Err);
                        this.fpDetail.Focus();

                        return -1;
                    }

                    FS.FrameWork.Management.PublicTrans.Commit();

                    this.fpDetail_Sheet.Rows.Remove(row, 1);
                }
            }

            //重新计算合计
            this.Sum();

            //不让合计行得到焦点
            if (this.fpDetail_Sheet.RowCount >= 2 && this.fpDetail_Sheet.ActiveRowIndex == this.fpDetail_Sheet.RowCount - 1)
            {
                this.fpDetail_Sheet.ActiveRowIndex = this.fpDetail_Sheet.ActiveRowIndex - 1;
            }

            this.fpDetail.Focus();

            return 1;
        }

        /// <summary>
        /// 小计
        /// </summary>
        /// <returns>成功 1 失败 0</returns>
        public int SubTotal()
        {
            try
            {
                this.isSubTotal = true;//开始进行小计
                if (this.fpDetail_Sheet.RowCount < 2)
                {
                    return 0;
                }
                int curIndex = this.fpDetail_Sheet.ActiveRowIndex;
                if (this.fpDetail_Sheet.Cells[curIndex, 0].Text == string.Empty)
                {
                    if (this.fpDetail_Sheet.Cells[curIndex, (int)Columns.Unit].Text == "小计")
                    {
                        this.isSubTotal = false;
                        return 0;
                    }
                    this.fpDetail_Sheet.Cells[curIndex, (int)Columns.Unit].Text = "小计";
                }
                else
                {
                    if (this.fpDetail_Sheet.GetText(curIndex + 1, (int)Columns.Unit) == "小计")
                    {
                        this.isSubTotal = false;
                        return 0;
                    }
                    this.fpDetail_Sheet.Rows.Add(curIndex + 1, 1);
                    curIndex++;
                    this.fpDetail_Sheet.Cells[curIndex, (int)Columns.Unit].Text = "小计";
                }
            DOStart:
                decimal subTot = 0;
                for (int i = curIndex - 1; i >= 0; i--)
                {
                    if (this.fpDetail_Sheet.Cells[i, (int)Columns.Unit].Text != "小计")
                    {
                        subTot += NConvert.ToDecimal(this.fpDetail_Sheet.Cells[i, (int)Columns.TotCost].Text);
                    }
                    else
                    {
                        break;
                    }
                }
                if (subTot == 0)
                {
                    this.fpDetail_Sheet.Cells[curIndex, (int)Columns.Unit].Text = string.Empty;
                }
                else
                {
                    this.fpDetail_Sheet.Cells[curIndex, (int)Columns.TotCost].Text = subTot.ToString();
                }
                for (int i = curIndex + 1; i < this.fpDetail_Sheet.RowCount - 1; i++)
                {
                    if (this.fpDetail_Sheet.Cells[i, (int)Columns.Unit].Text == "小计")
                    {
                        curIndex = i;
                        goto DOStart;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //结束小计
            this.isSubTotal = false;

            return 1;
        }

        /// <summary>
        /// 清空患者和项目明细
        /// </summary>
        public virtual void Clear()
        {
            if (this.fpDetail_Sheet.RowCount >= 0)
            {
                this.fpDetail_Sheet.Rows.Remove(0, this.fpDetail_Sheet.RowCount);
            }

            this.AddRow(0);
            this.AddRow(0);

            this.fpDetail_Sheet.SetValue(1, (int)Columns.Unit, "合计", false);
            this.fpDetail_Sheet.Rows[1].Locked = true;

            this.Sum();//显示汇总

            this.rowCount = 0;
            this.patientInfo = null;
        }

        /// <summary>
        /// 获得选定的项目信息
        /// </summary>
        /// <param name="recipeDoctCode">开立医生所在科室</param>
        /// <param name="dtNow">执行时间</param>
        /// <param name="itemType">项目类别 1 药品 2 非药品 3 组合项目 4 所有项目</param>
        /// <returns>成功 承载FeeItemList的泛型List集合 失败: null;</returns>
        public List<FeeItemList> QueryFeeItemList(string recipeDoctCode, DateTime dtNow, string itemType) 
        {
            List<FeeItemList> list = new List<FeeItemList>();

            this.fpDetail.StopCellEditing();

            for (int i = 0; i < this.fpDetail_Sheet.RowCount; i++) 
            {
                bool isNewItem = false;

                FeeItemList feeItemList = new FeeItemList();

                int returnValue = this.SetItem(i, PayTypes.Balanced, recipeDoctCode, dtNow, ref isNewItem, ref feeItemList);
                
                //没有获得项目
                if(returnValue != 1)
                {
                    continue;
                }

                switch (itemType) 
                {
                    //药品
                    case "1":
                        //if (feeItemList.Item.IsPharmacy) 
                        if (feeItemList.Item.ItemType == EnumItemType.Drug) 
                        {
                            list.Add(feeItemList);
                        }
                        break;
                    //非药品:
                    case "2":
                        //if (!feeItemList.Item.IsPharmacy)
                        if (feeItemList.Item.ItemType == EnumItemType.UnDrug)
                        {
                            list.Add(feeItemList);
                        }
                        break;
                    //组套
                    case "3":
                        if (feeItemList.IsGroup) 
                        {
                            list.Add(feeItemList);
                        }
                        break;
                    //所有
                    case "0":
                        list.Add(feeItemList);
                        break;
                }
            }

            return list;
        }

        /// <summary>
        /// 获得选定的项目信息
        /// </summary>
        /// <param name="recipeDoctCode">开立医生所在科室</param>
        /// <param name="dtNow">执行时间</param>
        /// <param name="itemType">项目类别 1 药品 2 非药品 3 组合项目 4 所有项目</param>
        /// <returns>成功 承载FeeItemList的ArrayList集合 失败: null;</returns>
        public ArrayList QueryFeeItemArrayList(string recipeDoctCode, DateTime dtNow, string itemType)
        {
            ArrayList list = new ArrayList();

            this.fpDetail.StopCellEditing();

            for (int i = 0; i < this.fpDetail_Sheet.RowCount; i++)
            {
                bool isNewItem = false;

                FeeItemList feeItemList = new FeeItemList();

                int returnValue = this.SetItem(i, PayTypes.Balanced, recipeDoctCode, dtNow, ref isNewItem, ref feeItemList);

                //没有获得项目
                if (returnValue != 1)
                {
                    continue;
                }

                switch (itemType)
                {
                    //药品
                    case "1":
                        //if (feeItemList.Item.IsPharmacy)
                        if(feeItemList.Item.ItemType == EnumItemType.Drug)
                        {
                            list.Add(feeItemList);
                        }
                        break;
                    //非药品:
                    case "2":
                        //if (!feeItemList.Item.IsPharmacy)
                        if (feeItemList.Item.ItemType == EnumItemType.UnDrug)
                        {
                            list.Add(feeItemList);
                        }
                        break;
                    //组套
                    case "3":
                        if (feeItemList.IsGroup)
                        {
                            list.Add(feeItemList);
                        }
                        break;
                    //所有
                    case "0":
                        list.Add(feeItemList);
                        break;
                }
            }

            return list;
        }

        /// <summary>
        /// 获得药品项目信息
        /// </summary>
        /// <param name="recipeDoctCode">开立医生所在科室</param>
        /// <param name="dtNow">执行时间</param>
        /// <returns>成功 承载FeeItemList的泛型List集合 失败: null;</returns>
        public List<FeeItemList> QueryMedItemList(string recipeDoctCode, DateTime dtNow) 
        {
            return this.QueryFeeItemList(recipeDoctCode, dtNow, "1");
        }

        /// <summary>
        /// 获得药品项目信息
        /// </summary>
        /// <param name="recipeDoctCode">开立医生所在科室</param>
        /// <param name="dtNow">执行时间</param>
        /// <returns>成功 承载FeeItemList的ArrayList集合 失败: null;</returns>
        public ArrayList QueryMedItemArrayList(string recipeDoctCode, DateTime dtNow)
        {
            return this.QueryFeeItemArrayList(recipeDoctCode, dtNow, "1");
        }

        /// <summary>
        /// 获得药品项目信息
        /// </summary>
        /// <returns>成功 承载FeeItemList的泛型List集合 失败: null;</returns>
        public List<FeeItemList> QueryMedItemList() 
        {
            return this.QueryMedItemList(string.Empty, this.inpatientManager.GetDateTimeFromSysDateTime());
        }

        /// <summary>
        /// 获得药品项目信息
        /// </summary>
        /// <returns>成功 承载FeeItemList的ArrayList集合 失败: null;</returns>
        public ArrayList QueryMedItemArrayList()
        {
            return this.QueryMedItemArrayList(string.Empty, this.inpatientManager.GetDateTimeFromSysDateTime());
        }

        /// <summary>
        /// 获得非药品项目信息
        /// </summary>
        /// <param name="recipeDoctCode">开立医生所在科室</param>
        /// <param name="dtNow">执行时间</param>
        /// <returns>成功 承载FeeItemList的泛型List集合 失败: null;</returns>
        public List<FeeItemList> QueryUndrugItemList(string recipeDoctCode, DateTime dtNow)
        {
            return this.QueryFeeItemList(recipeDoctCode, dtNow, "2");
        }

        /// <summary>
        /// 获得非药品项目信息
        /// </summary>
        /// <param name="recipeDoctCode">开立医生所在科室</param>
        /// <param name="dtNow">执行时间</param>
        /// <returns>成功 承载FeeItemList的ArrayList集合 失败: null</returns>
        public ArrayList QueryUndrugItemArrayList(string recipeDoctCode, DateTime dtNow)
        {
            return this.QueryFeeItemArrayList(recipeDoctCode, dtNow, "2");
        }

        /// <summary>
        /// 获得非药品项目信息
        /// </summary>
        /// <returns>成功 承载FeeItemList的泛型List集合 失败: null;</returns>
        public List<FeeItemList> QueryUndrugItemList()
        {
            return this.QueryUndrugItemList(string.Empty, this.inpatientManager.GetDateTimeFromSysDateTime());
        }

        /// <summary>
        /// 获得非药品项目信息
        /// </summary>
        /// <returns>成功 承载FeeItemList的ArrayList集合 失败: null</returns>
        public ArrayList QueryUndrugItemArrayList()
        {
            return this.QueryUndrugItemArrayList(string.Empty, this.inpatientManager.GetDateTimeFromSysDateTime());
        }
        #endregion
        #region 事件

        /// <summary>
        /// 按键设置
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            switch (keyData)
            {
                #region enter、up、down事件
                case Keys.Enter:
                    if (this.fpDetail.ContainsFocus)
                    {
                        //项目名称
                        if (this.fpDetail_Sheet.ActiveColumnIndex == (int)Columns.ItemName
                            ||
                            this.fpDetail_Sheet.ActiveColumnIndex == (int)Columns.GbCode)
                        {
                            this.ProcessItem();
                        }
                        //价格
                        else if (this.fpDetail_Sheet.ActiveColumnIndex == (int)Columns.Price)
                        {
                            if (this.SetItemProperty() == -1)
                            {
                                return true;
                            }
                            this.fpDetail_Sheet.SetActiveCell(this.fpDetail_Sheet.ActiveRowIndex, (int)Columns.Qty);
                        }
                        //数量
                        else if (this.fpDetail_Sheet.ActiveColumnIndex == (int)Columns.Qty)
                        {
                            if (this.SetItemProperty() == -1)
                            {
                                return true;
                            }
                            if (this.fpDetail_Sheet.Cells[this.fpDetail_Sheet.ActiveRowIndex, (int)Columns.Day].Locked)
                            {
                                this.fpDetail_Sheet.SetActiveCell(this.fpDetail_Sheet.ActiveRowIndex, (int)Columns.Dept);
                            }
                            else
                            {
                                this.fpDetail_Sheet.SetActiveCell(this.fpDetail_Sheet.ActiveRowIndex, (int)Columns.Day);
                            }
                        }
                        //付数
                        else if (this.fpDetail_Sheet.ActiveColumnIndex == (int)Columns.Day)
                        {
                            if (this.SetItemProperty() == -1)
                            {
                                return true;
                            }
                            this.fpDetail_Sheet.SetActiveCell(this.fpDetail_Sheet.ActiveRowIndex, (int)Columns.Dept);
                        }
                        else if (this.fpDetail_Sheet.ActiveColumnIndex == (int)Columns.Unit)
                        {
                            this.fpDetail_Sheet.SetActiveCell(this.fpDetail_Sheet.ActiveRowIndex, (int)Columns.Dept);
                        }
                        //执行科室
                        else if (this.fpDetail_Sheet.ActiveColumnIndex == (int)Columns.Dept)
                        {
                            if (this.ProcessDept() == -1)
                            {
                                return true;
                            }
                            if (this.fpDetail_Sheet.Columns[(int)Columns.feeRate].Visible == false)
                            {
                                //在最后一行，自动增加一行
                                if (this.fpDetail_Sheet.RowCount == this.fpDetail_Sheet.ActiveRowIndex + 2)
                                {
                                    this.AddRow(this.fpDetail_Sheet.RowCount - 1);
                                }
                                else
                                {
                                    this.fpDetail_Sheet.ActiveRowIndex++;
                                    this.fpDetail_Sheet.SetActiveCell(this.fpDetail_Sheet.ActiveRowIndex, (int)Columns.ItemName, true);
                                }
                            }
                            else
                            {
                                this.fpDetail_Sheet.SetActiveCell(this.fpDetail_Sheet.ActiveRowIndex, (int)Columns.feeRate);
                            }
                        }
                        //{2C7FCD3D-D9B4-44f5-A2EE-A7E8C6D85576}
                        else if (this.fpDetail_Sheet.ActiveColumnIndex == (int)Columns.feeRate)
                        {
                            if (this.SetItemProperty() == -1)
                            {
                                return true;
                            }
                            //在最后一行，自动增加一行
                            if (this.fpDetail_Sheet.RowCount == this.fpDetail_Sheet.ActiveRowIndex + 2)
                            {
                                this.AddRow(this.fpDetail_Sheet.RowCount - 1);
                            }
                            else
                            {
                                this.fpDetail_Sheet.ActiveRowIndex++;
                                this.fpDetail_Sheet.SetActiveCell(this.fpDetail_Sheet.ActiveRowIndex, (int)Columns.ItemName, true);
                            }
                        }
                    }
                    break;
                case Keys.Up:
                    if (this.fpDetail.ContainsFocus)
                    {
                        if (this.ucItemList.Visible)
                        {
                            this.ucItemList.PriorRow();
                        }
                        else if (this.lbDept.Visible)
                        {
                            this.lbDept.PriorRow();
                        }
                        else
                        {
                            int currRow = this.fpDetail_Sheet.ActiveRowIndex;
                            if (currRow > 0)
                            {
                                this.fpDetail_Sheet.ActiveRowIndex = currRow - 1;
                                this.fpDetail_Sheet.AddSelection(currRow - 1, 0, 1, 1);
                            }
                        }
                    }
                    break;
                case Keys.Down:
                    if (this.fpDetail.ContainsFocus)
                    {
                        if (this.ucItemList.Visible)
                        {
                            this.ucItemList.NextRow();
                        }
                        else if (lbDept.Visible)
                        {
                            this.lbDept.NextRow();
                        }
                        else
                        {
                            int currRow = this.fpDetail_Sheet.ActiveRowIndex;

                            if (currRow < this.fpDetail_Sheet.RowCount - 2)
                            {
                                this.fpDetail_Sheet.ActiveRowIndex = currRow + 1;
                                this.fpDetail_Sheet.AddSelection(currRow + 1, 0, 1, 1);
                            }
                            else
                            {
                                this.AddRow();
                            }
                        }
                    }
                    break;
                case Keys.Escape:
                    if (this.ucItemList.Visible)
                    {
                        this.ucItemList.Visible = false;
                    }
                    if (this.lbDept.Visible)
                    {
                        this.lbDept.Visible = false;
                    }
                    break;
                #endregion
                case Keys.F2:
                    if (this.fpDetail.ContainsFocus && this.ucItemList.Visible)
                    {
                        this.ucItemList.SetCurrentRow(1);
                        this.ProcessItem();
                    }
                    break;
                case Keys.F3:
                    if (this.fpDetail.ContainsFocus && this.ucItemList.Visible)
                    {
                        this.ucItemList.SetCurrentRow(2);
                        this.ProcessItem();
                    }
                    break;
                case Keys.F4:
                    if (this.fpDetail.ContainsFocus && this.ucItemList.Visible)
                    {
                        this.ucItemList.SetCurrentRow(3);
                        this.ProcessItem();
                    }
                    break;
                case Keys.F9:
                    if (!this.ucItemList.Visible)
                    {
                        this.isSubTotal = true;
                        this.SubTotal();
                        this.isSubTotal = false;
                    }
                    break;
            }

            return base.ProcessDialogKey(keyData);
        }

        /// <summary>
        /// 更改数量、价格判断事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpDetail_Change(object sender, ChangeEventArgs e)
        {
            if (this.isSubTotal)
            {
                //正在小计返回
                return;
            }
            switch (e.Column)
            {
                case (int)Columns.Price://判断价格合法性
                    this.SetItemProperty();
                    break;
                case (int)Columns.Qty://判断数量合法性
                    this.SetItemProperty();
                    break;
                case (int)Columns.Day:
                    {
                        this.SetItemProperty();
                        break;
                    }
                case (int)Columns.feeRate:
                    {
                        this.SetItemProperty();
                        break;
                    }
                    break;
            }
        }

        /// <summary>
        /// 项目选择事件
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private int ucItemList_SelectItem(Keys key)
        {
            this.ProcessItem();
            return 0;
        }

        /// <summary>
        /// 开始输入事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpDetail_EditModeOn(object sender, EventArgs e)
        {
            this.fpDetail.EditingControl.KeyDown += new KeyEventHandler(EditingControl_KeyDown);
            this.SetLocation();
            if (this.fpDetail_Sheet.ActiveColumnIndex != (int)Columns.Dept)
            {
                this.lbDept.Visible = false;
            }
            if (fpDetail_Sheet.ActiveColumnIndex != (int)Columns.ItemName
                && fpDetail_Sheet.ActiveColumnIndex != (int)Columns.GbCode)
            {
                this.ucItemList.Visible = false;
            }
            if (this.fpDetail_Sheet.ActiveColumnIndex == (int)Columns.Dept)
            {
                string deptCode = string.Empty;
                if ("N,I".Contains(SOC.HISFC.BizProcess.Cache.Common.GetDeptInfo(((FS.HISFC.Models.Base.Employee)this.itemManager.Operator).Dept.ID).DeptType.ID.ToString()))
                {
                    if (this.patientInfo != null)
                    {
                        deptCode = this.patientInfo.PVisit.PatientLocation.Dept.ID;
                    }
                    else
                    {
                        deptCode = SOC.HISFC.BizProcess.Cache.Common.GetEmployeeInfo(this.recipeDoctCode).Dept.ID;
                    }
                }
                else
                {
                    deptCode = this.operObj.Dept.ID;
                }
                if (this.fpDetail_Sheet.ActiveRow.Tag != null && this.fpDetail_Sheet.ActiveRow.Tag is Item)
                {
                    Item item = this.fpDetail_Sheet.ActiveRow.Tag as Item;
                    if (item.ItemType == EnumItemType.UnDrug)
                    {
                        ArrayList alExecDept = null;

                        string defaultExecDept = string.Empty;

                        lbDept.Items.Clear();

                        SOC.HISFC.BizProcess.Cache.Common.SetExecDept(false, deptCode, item.ID, ref defaultExecDept, ref alExecDept);

                        lbDept.AddItems(alExecDept);
                    }
                }
            }
        }

        /// <summary>
        /// 按键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditingControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.fpDetail_Sheet.ActiveColumnIndex == (int)Columns.ItemName)
            {
                switch (e.KeyCode)
                {
                    #region F1~F10快捷选择项目,F2~F4为farpoint内部键，在processdialogkey处理
                    case Keys.F1:
                        if (this.ucItemList.Visible)
                        {
                            this.ucItemList.SetCurrentRow(0);
                            ProcessItem();
                        }
                        break;
                    case Keys.F5:
                        if (this.ucItemList.Visible)
                        {
                            this.ucItemList.SetCurrentRow(4);
                            ProcessItem();
                        }
                        break;
                    case Keys.F6:
                        if (this.ucItemList.Visible)
                        {
                            this.ucItemList.SetCurrentRow(5);
                            ProcessItem();
                        }
                        break;
                    case Keys.F7:
                        if (this.ucItemList.Visible)
                        {
                            this.ucItemList.SetCurrentRow(6);
                            ProcessItem();
                        }
                        break;
                    case Keys.F8:
                        if (this.ucItemList.Visible)
                        {
                            this.ucItemList.SetCurrentRow(7);
                            ProcessItem();
                        }
                        break;
                    case Keys.F9:
                        if (this.ucItemList.Visible)
                        {
                            this.ucItemList.SetCurrentRow(8);
                            ProcessItem();
                        }
                        break;
                    case Keys.F10:
                        if (this.ucItemList.Visible)
                        {
                            this.ucItemList.SetCurrentRow(9);
                            ProcessItem();
                        }
                        break;
                    #endregion
                    case Keys.F11://切换输入法
                        if (this.ucItemList.Visible)
                            this.ucItemList.ChangeQueryType();
                        break;
                    case Keys.PageDown://下一页
                        if (this.ucItemList.Visible)
                            this.ucItemList.NextPage();
                        break;
                    case Keys.PageUp://上一页
                        if (this.ucItemList.Visible)
                            this.ucItemList.PriorPage();

                        break;
                }
            }
        }

        /// <summary>
        /// 输入的内容发生变化时,主要处理项目的过滤码和执行科室的检索条件时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpDetail_EditChange(object sender, EditorNotifyEventArgs e)
        {
            if (e.Row == this.fpDetail_Sheet.RowCount - 1) 
            {
                return;
            }
            
            string text;
            switch (e.Column)
            {
                case (int)Columns.ItemName://项目名称
                case (int)Columns.GbCode:
                    text = this.fpDetail_Sheet.ActiveCell.Text;
                    this.ucItemList.Filter(text);
                    if (!this.ucItemList.Visible)
                    {
                        this.ucItemList.Visible = true;
                        this.ucItemList.BringToFront();
                        this.ucItemList.Show();
                    }
                    //清空当前行变量
                    this.fpDetail_Sheet.SetValue(e.Row, (int)Columns.Price, string.Empty, false);
                    this.fpDetail_Sheet.SetValue(e.Row, (int)Columns.Qty, string.Empty, false);
                    this.fpDetail_Sheet.SetValue(e.Row, (int)Columns.Unit, string.Empty, false);
                    this.fpDetail_Sheet.SetValue(e.Row, (int)Columns.TotCost, string.Empty, false);
                    this.fpDetail_Sheet.SetValue(e.Row, (int)Columns.Dept, string.Empty, false);
                    this.fpDetail_Sheet.SetValue(e.Row, (int)Columns.ItemObject, null, false);
                    this.fpDetail_Sheet.SetValue(e.Row, (int)Columns.IsNew, string.Empty, false);
                    break;
                case (int)Columns.Dept://过滤执行科室			
                    text = this.fpDetail_Sheet.ActiveCell.Text;
                    this.lbDept.Filter(text);
                    //记录执行科室已经修改，要重新赋值
                    this.fpDetail_Sheet.SetValue(e.Row, (int)Columns.IsDeptChange, "1", false);
                    object obj = this.fpDetail_Sheet.GetValue(e.Row, (int)Columns.ItemObject);
                    if (obj != null)//一旦科室发生变化，清空实体里执行科室
                    {
                        FeeItemList f = obj as FeeItemList;
                        f.ExecOper.Dept.ID = string.Empty;
                        f.ExecOper.Dept.Name = string.Empty;
                        this.fpDetail_Sheet.SetValue(e.Row, (int)Columns.ItemObject, f, false);
                    }
                    if (!lbDept.Visible)
                    {
                        this.lbDept.Visible = true;
                    }
                    break;
                case (int)Columns.Qty://记录修改的数量
                    string isNew = this.fpDetail_Sheet.GetText(e.Row, (int)Columns.IsNew);
                    if (isNew == "0")
                    {
                        this.fpDetail_Sheet.SetValue(e.Row, (int)Columns.IsNew, "2", false);
                    }
                    break;
            }
        }

        /// <summary>
        /// 选择包装单位和最小单位时候触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpDetail_ComboSelChange(object sender, EditorNotifyEventArgs e)
        {
            if (e.Column == (int)Columns.Unit)
            {
                FarPoint.Win.Spread.CellType.ComboBoxCellType comboType = (FarPoint.Win.Spread.CellType.ComboBoxCellType)this.fpDetail_Sheet.Cells[e.Row, e.Column].CellType;

                string text = e.EditingControl.Text;
                if (((FarPoint.Win.FpCombo)e.EditingControl).SelectedIndex == 0)
                {
                    //按最小单位收费
                    object obj = fpDetail_Sheet.GetValue(e.Row, (int)Columns.ItemObject);
                    if (obj == null)
                    {
                        return;
                    }
                    decimal price = FS.FrameWork.Public.String.FormatNumber(
                        (obj as FeeItemList).Item.Price /
                        (obj as FeeItemList).Item.PackQty, 4);

                    this.fpDetail_Sheet.SetValue(e.Row, (int)Columns.Price, price, false);
                    //计算总额
                    text = this.fpDetail_Sheet.GetText(e.Row, (int)Columns.Qty);//数量
                    if (text == string.Empty)
                    {
                        text = "0";
                    }
                    decimal qty = NConvert.ToDecimal(text);
                    //付数
                    text = this.fpDetail_Sheet.GetText(e.Row, (int)Columns.Day);
                    if (text == string.Empty)
                    {
                        text = "0";
                    }
                    decimal day = NConvert.ToDecimal(text);

                    this.fpDetail_Sheet.SetValue(e.Row, (int)Columns.TotCost, price * qty * day, false);
                }
                else if (((FarPoint.Win.FpCombo)e.EditingControl).SelectedIndex == 1)
                {
                    //按包装单位收费
                    object obj = fpDetail_Sheet.GetValue(e.Row, (int)Columns.ItemObject);
                    if (obj == null)
                    {
                        return;
                    }
                    decimal price = (obj as FeeItemList).Item.Price;
                    this.fpDetail_Sheet.SetValue(e.Row, (int)Columns.Price, price, false);
                    //计算总额
                    text = this.fpDetail_Sheet.GetText(e.Row, (int)Columns.Qty);//数量
                    if (text == string.Empty)
                    {
                        text = "0";
                    }
                    decimal qty = NConvert.ToDecimal(text);
                    //付数
                    text = this.fpDetail_Sheet.GetText(e.Row, (int)Columns.Day);
                    if (text == string.Empty)
                    {
                        text = "0";
                    }
                    decimal day = NConvert.ToDecimal(text);

                    this.fpDetail_Sheet.SetValue(e.Row, (int)Columns.TotCost, price * qty * day, false);
                }
            }
        }


        #endregion

        #region 枚举

        /// <summary>
        /// 收费类型
        /// </summary>
        public enum FeeTypes 
        {
            /// <summary>
            /// 划价
            /// </summary>
            划价 = 0,

            /// <summary>
            /// 收费
            /// </summary>
            收费,

            /// <summary>
            /// 终端确认
            /// </summary>
            终端确认,

            /// <summary>
            /// 体检收费
            /// </summary>
            体检收费,
        }

        /// <summary>
        /// 项目类别枚举
        /// </summary>
        public enum ItemKind
        {
            /// <summary>
            /// 药品
            /// </summary>
            Drug,

            /// <summary>
            /// 非药品
            /// </summary>
            Undrug,

            /// <summary>
            /// 全部，药品和非药品
            /// </summary>
            All
        }

        /// <summary>
        /// 枚举列，调整列时相应调整该枚举
        /// </summary>
        public enum Columns
        {
            /// <summary>
            /// 国家编码
            /// </summary>
            GbCode,

            /// <summary>
            /// 项目名称
            /// </summary>
            ItemName,

            /// <summary>
            /// 价格
            /// </summary>
            Price,

            /// <summary>
            /// 数量
            /// </summary>
            Qty,

            /// <summary>
            /// 付数
            /// </summary>
            Day,

            /// <summary>
            /// 单位
            /// </summary>
            Unit,

            /// <summary>
            /// 总额
            /// </summary>
            TotCost,

            /// <summary>
            /// 执行科室
            /// </summary>
            Dept,

            /// <summary>
            /// 项目的对象,Item Instance
            /// </summary>
            ItemObject,

            /// <summary>
            /// 是否新增项目,0原有(数据库中),1新增,2修改
            /// </summary>
            IsNew,

            /// <summary>
            /// 执行科室是否修改0,否 1是
            /// </summary>
            IsDeptChange,

            /// <summary>
            /// 收费药品，1是0否
            /// </summary>
            IsDrug,

            /// <summary>
            /// 收费比例{2C7FCD3D-D9B4-44f5-A2EE-A7E8C6D85576}
            /// </summary>
            feeRate ,

            /// <summary>
            /// 批号
            /// </summary>
            BatchNo,
            /// <summary>
            /// 计费日期
            /// </summary>
            feeDate
        }

        #endregion

        private void fpDetail_DragDrop(object sender, DragEventArgs e)
        {
            
        }

        /// <summary>
        /// 双击时删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpDetail_CellDoubleClick(object sender, CellClickEventArgs e)
        {
            this.DelRow();
        }

        private void ucInpatientCharge_Load(object sender, EventArgs e)
        {
            if (!FS.HISFC.Components.Common.Classes.Function.DesignMode)
            {
                operObj = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Clone();
                //{062CEAA8-16B8-4c25-B4CC-E6B24DE7D331}
                IAdptIllnessInPatient = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(HISFC.BizProcess.Interface.FeeInterface.IAdptIllnessInPatient)) as HISFC.BizProcess.Interface.FeeInterface.IAdptIllnessInPatient;
            }
        }

        #region IInterfaceContainer 成员
        //{062CEAA8-16B8-4c25-B4CC-E6B24DE7D331}
        public Type[] InterfaceTypes
        {
            get
            {
                return new Type[] { 
                    typeof(HISFC.BizProcess.Interface.FeeInterface.IAdptIllnessInPatient)
                };
            }
        }

        #endregion
    }

    class CompareByID : IComparer
    {

        #region IComparer 成员

        public int Compare(object x, object y)
        {
            return FS.FrameWork.Function.NConvert.ToInt32(x).CompareTo(FS.FrameWork.Function.NConvert.ToInt32(y));
        }

        #endregion
    }
}
