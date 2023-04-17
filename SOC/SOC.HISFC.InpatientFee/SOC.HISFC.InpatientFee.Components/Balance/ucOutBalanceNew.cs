using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.Fee.Inpatient;
using FS.SOC.HISFC.BizProcess.CommonInterface;
using FarPoint.Win.Spread;
using FS.HISFC.Models.Base;
using Neusoft.SOC.HISFC.InpatientFee.Components.Balance; //  //{C4231074-D350-4df9-AF7C-C37124B44B80}

namespace FS.SOC.HISFC.InpatientFee.Components.Balance
{
    public partial class ucOutBalanceNew : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        #region 管理类
        /// <summary>
        /// 合同管理类
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.PactUnitInfo PactManagment = new FS.HISFC.BizLogic.Fee.PactUnitInfo();
        /// <summary>
        /// 住院费用管理类
        /// </summary>
        private FS.SOC.HISFC.InpatientFee.BizProcess.Fee FeeManager = new FS.SOC.HISFC.InpatientFee.BizProcess.Fee();
        /// <summary>
        /// 患者信息管理类
        /// </summary>
        private FS.SOC.HISFC.InpatientFee.BizProcess.RADT radtMgr = new FS.SOC.HISFC.InpatientFee.BizProcess.RADT();
        /// <summary>
        /// 诊断管理类
        /// </summary>
        private FS.SOC.HISFC.InpatientFee.BizProcess.Diagnose diagnoseMgr = new FS.SOC.HISFC.InpatientFee.BizProcess.Diagnose();
        /// <summary>
        /// 费用管理类
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();
        /// <summary>
        /// 住院费用管理类
        /// </summary>
        private FS.HISFC.BizLogic.Fee.InPatient inpatientFeeMgr = new FS.HISFC.BizLogic.Fee.InPatient();
        /// <summary>
        /// 非药品管理类
        /// </summary>
        FS.HISFC.BizLogic.Fee.Item undrugMgr = new FS.HISFC.BizLogic.Fee.Item();
        /// <summary>
        /// 药品管理类
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Pharmacy pharmacyMgr = new FS.HISFC.BizProcess.Integrate.Pharmacy();
        /// <summary>
        /// 套餐购买管理类
        /// </summary>
        FS.HISFC.BizLogic.MedicalPackage.Fee.Package packageMgr = new FS.HISFC.BizLogic.MedicalPackage.Fee.Package();
        /// <summary>
        /// 套餐细项购买管理类
        /// </summary>
        FS.HISFC.BizLogic.MedicalPackage.Fee.PackageDetail packageDetailMgr = new FS.HISFC.BizLogic.MedicalPackage.Fee.PackageDetail();
        /// <summary>
        /// 套餐缴费方式管理类
        /// </summary>
        FS.HISFC.BizLogic.MedicalPackage.Fee.PayMode packagePayModeMgr = new FS.HISFC.BizLogic.MedicalPackage.Fee.PayMode();
        /// <summary>
        /// 套餐基础信息管理类
        /// </summary>
        FS.HISFC.BizLogic.MedicalPackage.Package packageBaseMgr = new FS.HISFC.BizLogic.MedicalPackage.Package();
        /// <summary>
        /// 复合项目管理类
        /// </summary>
        FS.HISFC.BizLogic.Fee.UndrugPackAge undrugPackageMgr = new FS.HISFC.BizLogic.Fee.UndrugPackAge();
        /// <summary>
        /// RADT业务类
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.RADT radtIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();

        /// <summary>
        /// 控制参数业务层
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        /// <summary>
        /// 常数管理类　{97fbd080-edb9-f31f-41fd-33f7837851ba}
        /// </summary>
        FS.HISFC.BizLogic.Manager.Constant constantMgr = new FS.HISFC.BizLogic.Manager.Constant();

        /// <summary>
        /// 预结算管理类
        /// </summary>
        /// 
        FS.HISFC.BizLogic.Fee.PreBalanceLogic prebalancelogic = new FS.HISFC.BizLogic.Fee.PreBalanceLogic();

        private ArrayList rangeList = new ArrayList();

        /// <summary>
        /// 选择的套餐　{97fbd080-edb9-f31f-41fd-33f7837851ba}
        /// </summary>
        private FS.HISFC.Models.Base.ServiceTypes selectedrange = FS.HISFC.Models.Base.ServiceTypes.I;

        /// <summary>
        /// {97fbd080-edb9-f31f-41fd-33f7837851ba}
        /// </summary>
        private bool IsSelectedAll = false;
        #endregion

        #region 属性
        /// <summary>
        /// 结算类型
        /// </summary>
        private BalanceType balanceType = BalanceType.Package;
        /// <summary>
        /// 结算时候是否走结算清单封帐
        /// </summary>
        private bool IsCloseAccount = false;

        /// <summary>
        /// {FAE56BB8-F958-411f-9663-CC359D6D494B}
        /// 积分模块是否启用
        /// </summary>
        private bool IsCouponModuleInUse = false;

        //{FAE56BB8-F958-411f-9663-CC359D6D494B}
        /// <summary>
        /// 等级打折是否启用
        /// </summary>
        private bool IsLevelModuleInUse = false;

        /// <summary>
        /// 等级折扣
        /// </summary>
        private decimal levelDiscount = 1.0m;

        /// <summary>
        /// 等级
        /// </summary>
        private string levelID = "0";

        /// <summary>
        /// 等级名称
        /// </summary>
        private string levelName = "普通会员";

        #endregion

        #region 变量
        /// <summary>
        /// 患者实体
        /// </summary>
        private FS.HISFC.Models.RADT.PatientInfo patientInfo = new FS.HISFC.Models.RADT.PatientInfo();
        /// <summary>
        /// 总金额(项目原金额相加)
        /// </summary>
        private decimal BalanceTot = 0.0m;
        /// <summary>
        /// 实收(套餐结算时从套餐支付的支付方式中统计，普通结算和部分结算时打折算出来)
        /// </summary>
        private decimal BalanceReal = 0.0m;
        /// <summary>
        /// 赠送(套餐结算时从套餐支付的支付方式中统计，普通结算和部分结算时为0)
        /// </summary>
        private decimal BalanceDonate = 0.0m;
        /// <summary>
        /// 优惠(套餐结算时从套餐支付的支付方式中统计，普通结算和部分结算时打折算出来)
        /// </summary>
        private decimal BalanceEco = 0.0m;
        /// <summary>
        /// 预交金(套餐结算时为0，普通结算和部分结算时通过勾选预交金记录计算)
        /// </summary>
        private decimal PrepayCost = 0.0m;
        /// <summary>
        /// 应收（套餐结算时为0，普通结算和部分结算时为 实收-预交金，小于0时取零）
        /// </summary>
        private decimal ShouldPay = 0.0m;
        /// <summary>
        /// 应退（套餐结算时为0，普通结算和部分结算时为 预交金-实收，小于0时取零）
        /// </summary>
        private decimal ShouldRtn = 0.0m;
        /// <summary>
        /// 住院药品费用
        /// </summary>
        private ArrayList MedicineList = new ArrayList();
        /// <summary>
        /// 住院非药品费用
        /// </summary>
        private ArrayList UndrugList = new ArrayList();
        /// <summary>
        /// 住院项目费用汇总
        /// </summary>
        private List<FeeItemList> feeItemList = new List<FeeItemList>();
        /// <summary>
        /// 根据住院费用项目统计项目总数
        /// 药品项目以【项目编码-单位】为Key
        /// 非药品项目以【复合项目ID+项目ID】为Key
        /// </summary>
        private Hashtable hsFeeItem = new Hashtable();
        /// <summary>
        /// 根据住院费用项目统计项目总数
        /// 药品项目以【项目编码-单位】为Key
        /// 非药品项目以【复合项目ID+项目ID】为Key
        /// </summary>
        private Hashtable hsSelectedFeeItem = new Hashtable();
        /// <summary>
        /// 选中套餐中包含的项目
        /// </summary>
        private Hashtable hsPackageFeeItem = new Hashtable();
        /// <summary>
        /// 已选中的套餐
        /// </summary>
        private ArrayList SelectedPackage = new ArrayList();
        /// <summary>
        /// 是否在进行套餐匹配
        /// </summary>
        private bool IsMatching = false;
        /// <summary>
        /// 支付方式输入框
        /// </summary>
        private frmBalancePayAccount frmBalancePay = new frmBalancePayAccount();
        /// <summary>
        /// 套餐详情显示框
        /// </summary>
        private frmFeeItems frmPackageDetail = null;

        private frmPreBalanceItem frmprebalancedeta = null;

        #endregion

        /// <summary>
        /// 药品套餐价格
        /// </summary>
        public decimal DrugPackagePrice { set; get; }

        /// <summary>
        /// 器材套餐价格
        /// </summary>
        public decimal MatPakcagePrice { set; get; }

        /// <summary>
        /// 器材编码
        /// </summary>
        [Description("器材编码")]
        public string MatPakcageCode { set; get; }

        //public decimal 

        public ucOutBalanceNew()
        {
            InitializeComponent();
            InitControls();
        }

        /// <summary>
        /// 载入事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucOutBalanceNew_Load(object sender, EventArgs e)
        {
            this.GetNextInvoiceNO();
            FS.HISFC.Models.Base.Department currDept = (FS.HISFC.Models.Base.Department)(((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept);
            string hospitalname = "";
            string hospitalybcode = "";
            if (currDept.HospitalName.Contains("顺德"))
            {
                hospitalname = "顺德爱博恩妇产医院";
                hospitalybcode = "H44060600494";
                this.btnCKSelect.Visible = false; 
            }
            else
            {
                hospitalname = "广州爱博恩妇产医院";
                hospitalybcode = "H44010600124";
            }

            base.OnStatusBarInfo(null, " 机构名称：" + hospitalname + "  国家医保编码：" + hospitalybcode);
        }

        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControls()
        {
            //是否启用积分模块
            this.IsCouponModuleInUse = this.controlParamIntegrate.GetControlParam<bool>("CP0001", false, false);

            //{FAE56BB8-F958-411f-9663-CC359D6D494B}
            //会员等级是否启用
            this.IsLevelModuleInUse = this.controlParamIntegrate.GetControlParam<bool>("CP0002", false, false);

            this.AddEvents();
            this.SetViewStyle(BalanceType.Normal);

            //初始化套餐类型
            Showtctype();
        }
        /// <summary>
        /// 套餐区分显示 {97fbd080-edb9-f31f-41fd-33f7837851ba}
        /// </summary>
        private void Showtctype()
        {
            //初始化套餐类型
            this.rangeList = constantMgr.GetList("PACKAGERANGE");
            if (rangeList.Count == 0)
            {
                MessageBox.Show("初始化套餐类型列表出错!" + this.constantMgr.Err);

                return;
            }
            FS.FrameWork.Models.NeuObject objAllTmp = new FS.FrameWork.Models.NeuObject();
            objAllTmp.ID = "ALL";
            objAllTmp.Name = "全部";
            rangeList.Insert(0, objAllTmp);
            this.cmbTctype.AddItems(this.rangeList);
            this.cmbTctype.Text = FS.FrameWork.Management.Language.Msg("住院套餐");
        }



        /// <summary>
        /// 添加事件
        /// </summary>
        private void AddEvents()
        {
            this.Load += new EventHandler(ucOutBalanceNew_Load);
            this.chkNormal.Click += new EventHandler(chkNormal_Click);
            this.chkPart.Click += new EventHandler(chkPart_Click);
            this.chkPackage.Click += new EventHandler(chkPackage_Click);
            this.btnPackageMatch.Click += new EventHandler(btnPackageMatch_Click);
            this.btnPackageDetail.Click += new EventHandler(btnPackageDetail_Click);
            this.ucQueryInfo.myEvent += new FS.HISFC.Components.Common.Controls.myEventDelegate(ucQueryInfo_myEvent);
            this.btnAllChoose.Click += new EventHandler(btnAllChoose_Click);
            this.btnAllClear.Click += new EventHandler(btnAllClear_Click);
            this.btnDiscount.Click += new EventHandler(btnDiscount_Click);
            this.btnEcoSet.Click += new EventHandler(btnEcoSet_Click);
            this.btnRealSet.Click += new EventHandler(btnRealSet_Click);
            this.btnDisaccount.Click += new EventHandler(btnDisaccount_Click);
            this.btnCKSelect.Click += new EventHandler(btnCKSelect_Click);

            this.btnprebalancedetail.Click += new EventHandler(btnprebalancedetail_Click);  //{C4231074-D350-4df9-AF7C-C37124B44B80}
            this.btnPreBalancePP.Click += new EventHandler(btnPreBalancePP_Click);  //{C4231074-D350-4df9-AF7C-C37124B44B80}
            this.FpTotFee_Sheet1.CellChanged += new SheetViewEventHandler(FpTotFee_Sheet1_CellChanged);
            this.FpPrepay.ButtonClicked += new EditorNotifyEventHandler(FpPrepay_ButtonClicked);
            this.FpPackage.ButtonClicked += new EditorNotifyEventHandler(FpPackage_ButtonClicked);
            this.FpSelectedFee.CellDoubleClick += new CellClickEventHandler(FpSelectedFee_CellDoubleClick);

        }

        /// <summary>
        /// 删除事件
        /// </summary>
        private void DelEvents()
        {
            this.FpSelectedFee.CellDoubleClick += new CellClickEventHandler(FpSelectedFee_CellDoubleClick);
            this.FpPackage.ButtonClicked -= new EditorNotifyEventHandler(FpPackage_ButtonClicked);
            this.FpPrepay.ButtonClicked -= new EditorNotifyEventHandler(FpPrepay_ButtonClicked);
            this.FpTotFee_Sheet1.CellChanged -= new SheetViewEventHandler(FpTotFee_Sheet1_CellChanged);


            this.btnEcoSet.Click -= new EventHandler(btnEcoSet_Click);
            this.btnRealSet.Click -= new EventHandler(btnRealSet_Click);
            this.btnDisaccount.Click -= new EventHandler(btnDisaccount_Click);
            this.btnAllClear.Click -= new EventHandler(btnAllClear_Click);
            this.btnAllChoose.Click -= new EventHandler(btnAllChoose_Click); //btnDiscount_Click
            this.btnDiscount.Click -= new EventHandler(btnDiscount_Click);
            this.ucQueryInfo.myEvent -= new FS.HISFC.Components.Common.Controls.myEventDelegate(ucQueryInfo_myEvent);
            this.btnPackageDetail.Click -= new EventHandler(btnPackageDetail_Click);
            this.btnprebalancedetail.Click -= new EventHandler(btnprebalancedetail_Click);
            this.btnPreBalancePP.Click -= new EventHandler(btnPreBalancePP_Click);
            this.btnPackageMatch.Click -= new EventHandler(btnPackageMatch_Click);
            this.btnCKSelect.Click -= new EventHandler(btnCKSelect_Click);
            this.chkPackage.Click -= new EventHandler(chkPackage_Click);
            this.chkPart.Click -= new EventHandler(chkPart_Click);
            this.chkNormal.Click -= new EventHandler(chkNormal_Click);
            this.Load -= new EventHandler(ucOutBalanceNew_Load);
        }

        #region 添加事件

        /// <summary>
        /// 费用全清
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAllClear_Click(object sender, EventArgs e)
        {
            //普通结算部存在部分选择
            if (this.balanceType == BalanceType.Normal)
            {
                return;
            }

            this.IsMatching = true;

            foreach (Row row in this.FpTotFee_Sheet1.Rows)
            {
                this.FpTotFee_Sheet1.Cells[row.Index, (int)TotFeeCols.CostQty].Value = 0;
                row.ForeColor = Color.Black;
            }

            this.IsMatching = false;
            this.SetCostInfo(this.BalanceEco);
        }


        private void btnDiscount_Click(object sender, EventArgs e)
        {
            //普通结算部存在部分选择
            if (this.balanceType != BalanceType.Part)
            {
                MessageBox.Show("该功能只能在部分结算中使用!");
                return;
            }

            this.IsMatching = true;

            foreach (Row row in this.FpTotFee_Sheet1.Rows)
            {
                FeeItemList feeItem = row.Tag as FeeItemList;
               
                if (feeItem.IsDiscount)
                {
                    this.FpTotFee_Sheet1.Cells[row.Index, (int)TotFeeCols.CostQty].Value =
                    Decimal.Parse(this.FpTotFee_Sheet1.Cells[row.Index, (int)TotFeeCols.Qty].Value.ToString());
                    row.ForeColor = Color.Red;
                }
            }

            this.IsMatching = false;
            this.SetCostInfo(this.BalanceEco);
        }

        /// <summary>
        /// 产康减免全选
        /// </summary>
        private void btnCKSelect_Click(object sender, EventArgs e)
        {
            //普通结算部存在部分选择
            if (this.balanceType != BalanceType.Part)
            {
                MessageBox.Show("该功能只能在部分结算中使用!");
                return;
            }

            this.IsMatching = true;

            ArrayList CKItems = constantMgr.GetList("CKJMITEMS");
            List<string> itemIDs = new List<string>();
            foreach (var item in CKItems.ToArray())
            {
                itemIDs.Add((item as FS.HISFC.Models.Base.Const).ID);
            }

            foreach (Row row in this.FpTotFee_Sheet1.Rows)
            {
                FeeItemList feeItem = row.Tag as FeeItemList;

                if (itemIDs.Contains(feeItem.Item.ID))
                {
                    this.FpTotFee_Sheet1.Cells[row.Index, (int)TotFeeCols.CostQty].Value =
                    Decimal.Parse(this.FpTotFee_Sheet1.Cells[row.Index, (int)TotFeeCols.Qty].Value.ToString());
                    row.ForeColor = Color.Red;
                }
            }

            this.IsMatching = false;
            this.SetCostInfo(this.BalanceEco);
        }

        /// <summary>
        /// 费用全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAllChoose_Click(object sender, EventArgs e)
        {
            //普通结算部存在部分选择
            if (this.balanceType == BalanceType.Normal)
            {
                return;
            }

            this.IsMatching = true;

            foreach (Row row in this.FpTotFee_Sheet1.Rows)
            {
                this.FpTotFee_Sheet1.Cells[row.Index, (int)TotFeeCols.CostQty].Value =
                    Decimal.Parse(this.FpTotFee_Sheet1.Cells[row.Index, (int)TotFeeCols.Qty].Value.ToString());
                row.ForeColor = Color.Red;
            }

            this.IsMatching = false;
            this.SetCostInfo(this.BalanceEco);
        }


        /// <summary>
        /// 按比例折扣
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDisaccount_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.balanceType == BalanceType.Normal && this.hsFeeItem.Keys.Count == 0)
                {
                    MessageBox.Show("请先检索费用！");
                    return;
                }

                if (this.balanceType != BalanceType.Normal && this.hsSelectedFeeItem.Keys.Count == 0)
                {
                    MessageBox.Show("请先选择费用！");
                    return;
                }

                decimal disAccount = decimal.Parse(this.tbDisaccount.Text);
                if (disAccount < 0 || disAccount > 100)
                {
                    MessageBox.Show("折扣率不能小于零或者大于100");
                    return;
                }
                else
                {
                    decimal real = Math.Round((this.BalanceTot * disAccount) / 100, 2, MidpointRounding.AwayFromZero);
                    decimal eco = this.BalanceTot - real;
                    this.SetCostInfo(eco);
                    this.tbEcoSet.Text = "";
                    this.tbDisaccount.Text = "";
                    this.tbRealSet.Text = "";
                }
            }
            catch (Exception ex)
            {
                this.tbEcoSet.Text = "";
                this.tbDisaccount.Text = "";
                this.tbRealSet.Text = "";
                MessageBox.Show("请输入正确的折扣比例！");
            }
        }

        /// <summary>
        /// 手动输入实收金额
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRealSet_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.balanceType == BalanceType.Normal && this.hsFeeItem.Keys.Count == 0)
                {
                    MessageBox.Show("请先检索费用！");
                    return;
                }

                if (this.balanceType != BalanceType.Normal && this.hsSelectedFeeItem.Keys.Count == 0)
                {
                    MessageBox.Show("请先选择费用！");
                    return;
                }

                decimal real = decimal.Parse(this.tbRealSet.Text);
                if (real < 0 || real > this.BalanceTot)
                {
                    MessageBox.Show("实收金额不能小于零或者大于总金额");
                    return;
                }
                else
                {
                    decimal eco = this.BalanceTot - real;
                    this.SetCostInfo(eco);
                    this.tbEcoSet.Text = "";
                    this.tbDisaccount.Text = "";
                    this.tbRealSet.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("请输入正确的金额！");
            }
        }

        /// <summary>
        /// 手动输入优惠金额
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEcoSet_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.balanceType == BalanceType.Normal && this.hsFeeItem.Keys.Count == 0)
                {
                    MessageBox.Show("请先检索费用！");
                    return;
                }

                if (this.balanceType != BalanceType.Normal && this.hsSelectedFeeItem.Keys.Count == 0)
                {
                    MessageBox.Show("请先选择费用！");
                    return;
                }

                decimal eco = decimal.Parse(this.tbEcoSet.Text);
                if (eco < 0 || eco > this.BalanceTot)
                {
                    MessageBox.Show("优惠金额不能小于零或者大于总金额");
                    return;
                }
                else
                {
                    this.SetCostInfo(eco);
                    this.tbEcoSet.Text = "";
                    this.tbDisaccount.Text = "";
                    this.tbRealSet.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("请输入正确的金额！");
            }
        }

        #region 结算类型切换
        /// <summary>
        /// 普通结算
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkNormal_Click(object sender, EventArgs e)
        {
            this.chkNormal.Checked = true;
            this.chkPart.Checked = false;
            this.chkPackage.Checked = false;
            this.SetViewStyle(BalanceType.Normal);
        }

        /// <summary>
        /// 部分结算
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkPart_Click(object sender, EventArgs e)
        {
            this.chkNormal.Checked = false;
            this.chkPart.Checked = true;
            this.chkPackage.Checked = false;
            this.SetViewStyle(BalanceType.Part);
        }

        /// <summary>
        /// 套餐结算
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkPackage_Click(object sender, EventArgs e)
        {
            this.chkNormal.Checked = false;
            this.chkPart.Checked = false;
            this.chkPackage.Checked = true;
            this.SetViewStyle(BalanceType.Package);
        }

        /// <summary>
        /// 根据不同的结算类别设置显示信息
        /// </summary>
        /// <param name="type"></param>
        private void SetViewStyle(BalanceType type)
        {
            try
            {
                this.DelEvents();
                if (this.balanceType == type)
                {
                    return;
                }

                this.balanceType = type;
                this.SelectedPackage.Clear();
                this.hsPackageFeeItem.Clear();

                //{14CCBD16-9A45-42f8-896C-5A2CB00DAB1B}
                foreach (Row row in this.FpPackage_Sheet1.Rows)
                {
                    this.FpPackage_Sheet1.Cells[row.Index, (int)PackageCols.Check].Value = false;
                }

                //普通结算
                if (this.balanceType == BalanceType.Normal)
                {
                    this.SetCostSetControls(true);
                    this.btnAllChoose.Visible = false;
                    this.btnAllClear.Visible = false;
                    this.gbFeePackage.Visible = false;    //套餐与选择的费用容器
                    this.tabPrepay.Visible = true;        //预交金
                    this.tabSelectedFee.Visible = false;  //选择的费用
                    this.tabPackage.Visible = false;      //套餐
                    this.SetFPPrepay();
                    this.FpSelectedFee_Sheet1.RowCount = 0;
                    this.FpTotFee_Sheet1.Columns[(int)TotFeeCols.CostQty].Visible = false;
                    this.FpTotFee_Sheet1.Columns[(int)TotFeeCols.LeftQty].Visible = false;
                    foreach (Row row in this.FpTotFee_Sheet1.Rows)
                    {
                        this.FpTotFee_Sheet1.Rows[row.Index].ForeColor = Color.Black;
                        this.FpTotFee_Sheet1.Cells[row.Index, (int)TotFeeCols.CostQty].Value = 0.0m;
                        this.FpTotFee_Sheet1.Cells[row.Index, (int)TotFeeCols.LeftQty].Value = this.FpTotFee_Sheet1.Cells[row.Index, (int)TotFeeCols.Qty].Value;
                    }
                }
                else
                {
                    this.FpTotFee_Sheet1.Columns[(int)TotFeeCols.CostQty].Visible = true;
                    this.FpTotFee_Sheet1.Columns[(int)TotFeeCols.LeftQty].Visible = true;
                    this.gbFeePackage.Visible = true;
                    this.btnAllChoose.Visible = true;
                    this.btnAllClear.Visible = true;

                    //套餐结算
                    if (this.balanceType == BalanceType.Package)
                    {
                        this.SetCostSetControls(false);
                        this.tabPackage.Visible = true;
                        this.tabSelectedFee.Visible = true;
                        this.tabPrepay.Visible = false;
                        this.plPackgeOpe.Visible = true;

                        this.FpPrepay_Sheet1.RowCount = 0;
                        this.tbPrepayCost.Text = "0.00";
                    }

                    //部分结算
                    if (this.balanceType == BalanceType.Part)
                    {
                        this.SetCostSetControls(true);
                        this.SetFPPrepay();
                        this.tabPackage.Visible = false;
                        this.tabSelectedFee.Visible = true;
                        this.tabPrepay.Visible = true;
                        this.plPackgeOpe.Visible = false;
                    }
                }

                this.SetCostInfo(0.0m);

            }
            catch
            { }
            finally
            {
                this.AddEvents();
            }
        }
        #endregion

        #region 查询设置患者信息

        /// <summary>
        /// 查询患者信息
        /// </summary>
        private void ucQueryInfo_myEvent()
        {
            string errText = "";
            if (this.QueryByPatientNO(this.ucQueryInfo.InpatientNo, ref errText) < 0)
            {
                this.Clear();
                CommonController.CreateInstance().MessageBox(errText, MessageBoxIcon.Warning);
                this.ucQueryInfo.Focus();
            }
        }

        /// <summary>
        /// 住院号回车处理
        /// </summary>
        protected virtual int QueryByPatientNO(string inpatientNO, ref string errText)
        {
            try
            {
                this.Clear();
                if (string.IsNullOrEmpty(inpatientNO.Trim()))
                {
                    throw new Exception("住院号错误，没有找到该患者");
                }

                this.patientInfo = this.radtMgr.GetPatientInfo(inpatientNO);
                if (!string.IsNullOrEmpty(this.patientInfo.Memo))
                {
                    if (DialogResult.No == MessageBox.Show("此患者标记备注信息为:\"" + patientInfo.Memo + "\"" + System.Environment.NewLine + "\r\n是否继续结算？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2))
                    {
                        return 0;
                    }
                }

                #region 获取医保登记信息
                //医保暂时不处理
                if (false)
                {
                    if (patientInfo.Pact.PayKind.ID == "02")
                    {
                        //医保患者，获取医保主表信息
                        FS.HISFC.BizLogic.Fee.Interface siMsg = new FS.HISFC.BizLogic.Fee.Interface();
                        FS.HISFC.Models.RADT.PatientInfo siPatient = siMsg.GetSIPersonInfo(this.patientInfo.ID);
                        if (siPatient != null && !string.IsNullOrEmpty(siPatient.ID) && siPatient.SIMainInfo.IsValid)
                        {
                            this.patientInfo.SIMainInfo.RegNo = siPatient.SIMainInfo.RegNo;
                            this.patientInfo.SIMainInfo.HosNo = siPatient.SIMainInfo.HosNo;
                            this.patientInfo.SIMainInfo.EmplType = siPatient.SIMainInfo.EmplType;
                            this.patientInfo.SIMainInfo.InDiagnose.Name = siPatient.SIMainInfo.InDiagnose.Name;
                            this.patientInfo.SIMainInfo.InDiagnose.ID = siPatient.SIMainInfo.InDiagnose.ID;
                            this.patientInfo.SIMainInfo.AppNo = siPatient.SIMainInfo.AppNo;
                            this.patientInfo.IDCard = siPatient.IDCard;
                            this.patientInfo.CompanyName = siPatient.CompanyName;
                            this.patientInfo.Birthday = siPatient.Birthday;
                            this.patientInfo.Sex = siPatient.Sex;
                            if (!string.IsNullOrEmpty(this.patientInfo.Name) && this.patientInfo.Name.Trim() != siPatient.Name.Trim())
                            {
                                if (MessageBox.Show(this, "医保登记患者名字和在院登记名字不一致，是否继续？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
                                {
                                    throw new Exception("已取消操作！");
                                }
                            }
                        }
                    }
                }
                #endregion

                this.ucQueryInfo.Text = this.patientInfo.PID.PatientNO;
                this.SetPatientInfo(patientInfo);

                if (this.patientInfo.PVisit.OutTime < new DateTime(1900, 1, 1))
                {
                    throw new Exception("该患者未做出院登记，请联系护士站！");
                }

                if (this.FeeManager.CloseAccount(this.patientInfo.ID) < 0)
                {
                    throw new Exception("关账失败，原因：" + this.FeeManager.Err);
                }

                ///获取未结算的药品信息和非药品信息
                this.MedicineList = this.inpatientFeeMgr.QueryMedicineListsForBalance(this.patientInfo.ID);
                this.UndrugList = this.inpatientFeeMgr.QueryItemListsForBalance(this.patientInfo.ID);

                if (this.DisplayPatientCost(ref errText) == -1)
                {
                    return -1;
                }
            }
            catch (Exception ex)
            {
                errText = ex.Message;
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 患者信息赋值
        /// </summary>
        /// <param name="patientInfo">患者信息实体</param>
        protected virtual void SetPatientInfo(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            if (patientInfo == null)
            {
                this.lbPatientInfo.Text = "姓名".PadRight(6, ' ') + "入院日期:".PadRight(6, ' ') + "出院日期:".PadRight(6, ' ') + "结算类别:".PadRight(6, ' ');
                this.tbIDNO.Text = "";
            }
            else
            {
                string diagnoseName = this.diagnoseMgr.QueryDiagnoseName(patientInfo.ID);
                ArrayList alBabyInfo = radtMgr.QueryBabies(patientInfo.ID);
                string strBabyInfo = string.Empty;
                if (alBabyInfo != null && alBabyInfo.Count != 0)
                {
                    strBabyInfo = " 婴儿住院号:";
                    foreach (FS.HISFC.Models.RADT.PatientInfo babyInfo in alBabyInfo)
                    {
                        strBabyInfo += babyInfo.PID.PatientNO.Substring(0, 2) + ",";
                    }
                    strBabyInfo = strBabyInfo.TrimEnd(',');
                }
                //住院天数
                int days = radtMgr.GetInDays(patientInfo.ID);
                if (days > 1)
                {
                    days -= 1;
                }
                string outDate = (patientInfo.PVisit.OutTime < new DateTime(1900, 1, 1)) ? "         " : patientInfo.PVisit.OutTime.ToString("yyyy-MM-dd");
                if (patientInfo.Pact.PayKind.ID == "03")
                {
                    FS.HISFC.Models.Base.PactInfo pact = this.PactManagment.GetPactUnitInfoByPactCode(patientInfo.Pact.ID);
                    string pactName = patientInfo.Pact.Name + " 自付比例：" + pact.Rate.PayRate * 100 + "%";//合同单位
                    this.lbPatientInfo.Text = "姓名:" + patientInfo.Name + "  入院日期:" + patientInfo.PVisit.InTime.ToString("yyyy-MM-dd") + "  出院日期:" + outDate + "  天数:" + days + "\r\n" + "结算类别:" + pactName + "  日限额：" + patientInfo.FT.DayLimitCost + "  出院诊断:" + diagnoseName + strBabyInfo;
                }
                else
                {
                    this.lbPatientInfo.Text = "姓名:" + patientInfo.Name + "  入院日期:" + patientInfo.PVisit.InTime.ToString("yyyy-MM-dd") + "  出院日期:" + outDate + "  天数:" + days + "  结算类别:" + patientInfo.Pact.Name + "  出院诊断:" + diagnoseName + strBabyInfo;
                }
                //{C22E94C1-78A0-493c-8FFB-5BB0BF51D6AE添加账户余额 赠送余额
                FS.HISFC.BizLogic.Fee.Account accountMgr = new FS.HISFC.BizLogic.Fee.Account();
                FS.HISFC.Models.Account.Account account = accountMgr.GetAccountByCardNoEX(patientInfo.PID.CardNO);
                if (account != null)
                {
                    this.lbPatientInfo.Text += "  卡上余额:" + account.BaseVacancy.ToString("F2") + "  赠送余额:" + account.DonateVacancy.ToString("F2");

                }
                else
                {
                    //MessageBox.Show("该患者没有账户信息！");
                    this.lbPatientInfo.Text += "  卡上余额:0.00  赠送余额:0.00";
                }
                this.tbIDNO.Text = patientInfo.IDCard;

                string resultCode = "0";
                string errMsg = string.Empty;
                this.levelID = "0";
                this.levelName = "普通会员";
                this.levelDiscount = 1m;

                if (FS.HISFC.BizProcess.Integrate.WSHelper.QueryAccountDiscount(patientInfo.PID.CardNO, out levelDiscount, out levelID, out levelName, out resultCode, out errMsg) < 0)
                {
                    MessageBox.Show("查询会员等级失败,患者无法享受等级折扣,错误详情:" + errMsg);
                    return;
                }

                this.SetDiscountInfo(this.levelDiscount, this.levelID, this.levelName);
            }
        }

        public void SetDiscountInfo(decimal levelDiscount, string levelID, string levelName)
        {

            string accountInfo = "当前患者为【{0}】用户,会员折扣为{1}折";

            //{5B7CD01E-2DDB-499d-9F49-DA8A2F7E0AAC}
            if (levelDiscount.Equals(1))
            {
                accountInfo = "当前患者为【{0}】用户,无会员折扣";
                accountInfo = string.Format(accountInfo, levelName);
            }
            else
            {
                accountInfo = string.Format(accountInfo, levelName, (levelDiscount * 10).ToString().Trim());
            }
            this.lbAccountInfo.Text = accountInfo;
        }

        #region 套餐匹配
        /// <summary>
        /// 进行套餐匹配
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPackageMatch_Click(object sender, EventArgs e)
        {
            this.hsSelectedFeeItem.Clear();
            this.FpSelectedFee_Sheet1.RowCount = 0;

            //{14CCBD16-9A45-42f8-896C-5A2CB00DAB1B}
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在进行匹配，请等待。。。");
            Application.DoEvents();
            try
            {
                foreach (FS.HISFC.Models.MedicalPackage.Fee.Package row in this.SelectedPackage)
                {
                    this.ConvertPackageInFeeItem(row);
                }

                this.IsMatching = true;
                ArrayList packageItemKeys = new ArrayList(this.hsPackageFeeItem.Keys);
                packageItemKeys.Sort();

                foreach (Row row in this.FpTotFee_Sheet1.Rows)
                {
                    this.FpTotFee_Sheet1.Cells[row.Index, (int)TotFeeCols.CostQty].Value = 0;
                    row.ForeColor = Color.Black;
                }
                List<FeeItemList> listRed = new List<FeeItemList>();
                List<FeeItemList> listBlue = new List<FeeItemList>();
                List<FeeItemList> listBlack = new List<FeeItemList>();
                //{A808BC0E-10AA-4d6e-B1E7-838F92714F49}  套餐匹配更改优化方式
                foreach (Row row in this.FpTotFee_Sheet1.Rows)
                {
                    FeeItemList rowFee = row.Tag as FeeItemList;
                    bool isPackage = false;
                    foreach (string key in packageItemKeys)  //
                    {
                        FeeItemList packageFee = this.hsPackageFeeItem[key] as FeeItemList;
                        decimal leftQty = packageFee.Item.Qty;

                        if (packageFee.Item.ID == rowFee.Item.ID && packageFee.UndrugComb.ID == rowFee.UndrugComb.ID)
                        {
                            isPackage = true;
                            decimal cost = Decimal.Parse(this.FpTotFee_Sheet1.Cells[row.Index, (int)TotFeeCols.CostQty].Value.ToString());
                            if (rowFee.Item.Qty - cost >= leftQty)
                            {

                                if (rowFee.Item.Qty == cost + leftQty)
                                {
                                    row.ForeColor = Color.Red;
                                    rowFee.Item.Extend1 = rowFee.Item.Qty.ToString();
                                    listRed.Add(rowFee);
                                }
                                else
                                {
                                    row.ForeColor = Color.Blue;
                                    rowFee.Item.Extend1 = leftQty.ToString();
                                    listBlue.Add(rowFee);
                                }

                                this.FpTotFee_Sheet1.Cells[row.Index, (int)TotFeeCols.CostQty].Value = cost + leftQty;
                                leftQty = 0;
                                break;
                            }
                            else
                            {
                                this.FpTotFee_Sheet1.Cells[row.Index, (int)TotFeeCols.CostQty].Value = rowFee.Item.Qty;
                                rowFee.Item.Extend1 = rowFee.Item.Qty.ToString();
                                row.ForeColor = Color.Red;
                                leftQty -= (rowFee.Item.Qty - cost);
                                isPackage = true;
                                listRed.Add(rowFee);
                                break;
                            }
                        }

                    }
                    if (isPackage == false)
                    {
                        if (cbxIsPackage.Checked)
                        {

                            if (rowFee.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                            {
                                if (rowFee.FT.TotCost < DrugPackagePrice)//更改套餐匹配由单价更改为总额CE52F42E-C6BB-43AA-9CC5-7B7165F99823
                                {
                                    this.FpTotFee_Sheet1.Cells[row.Index, (int)TotFeeCols.CostQty].Value = rowFee.Item.Qty;
                                    row.ForeColor = Color.Red;
                                    rowFee.Item.Extend1 = rowFee.Item.Qty.ToString();
                                    listRed.Add(rowFee);
                                }
                                else
                                {
                                    row.ForeColor = Color.Black;
                                    rowFee.Item.Extend1 = "0";
                                    listBlack.Add(rowFee);
                                }
                            }
                            else
                            {
                                if ((MatPakcageCode.Contains(rowFee.Item.MinFee.ID) && rowFee.FT.TotCost < MatPakcagePrice) || rowFee.Item.Price <= 0)//CE52F42E-C6BB-43AA-9CC5-7B7165F99823更改套餐匹配由单价更改为总额
                                {
                                    this.FpTotFee_Sheet1.Cells[row.Index, (int)TotFeeCols.CostQty].Value = rowFee.Item.Qty;

                                    //this.FpTotFee_Sheet1.Cells[row.Index, (int)TotFeeCols.Spec].Value = rowFee.UndrugComb.Name;


                                    row.ForeColor = Color.Red;
                                    rowFee.Item.Extend1 = rowFee.Item.Qty.ToString();
                                    listRed.Add(rowFee);
                                }
                                else
                                {
                                    //this.FpTotFee_Sheet1.Cells[row.Index, (int)TotFeeCols.Spec].Value = rowFee.UndrugComb.Name;

                                    row.ForeColor = Color.Black;
                                    rowFee.Item.Extend1 = "0";
                                    listBlack.Add(rowFee);
                                }
                            }

                            //else if (rowFee.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.MatItem && rowFee.Item.Price < MatPakcagePrice)
                            //{
                            //    this.FpTotFee_Sheet1.Cells[row.Index, (int)TotFeeCols.CostQty].Value = rowFee.Item.Qty;
                            //    row.ForeColor = Color.Red;
                            //    rowFee.Item.Extend1 = rowFee.Item.Qty.ToString();
                            //    listRed.Add(rowFee);
                            //}
                            //else
                            //{
                            //    if (rowFee.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.UnDrug && rowFee.Item.Price <= 0)
                            //    {
                            //        this.FpTotFee_Sheet1.Cells[row.Index, (int)TotFeeCols.CostQty].Value = rowFee.Item.Qty;
                            //        row.ForeColor = Color.Red;
                            //        rowFee.Item.Extend1 = rowFee.Item.Qty.ToString();
                            //        listRed.Add(rowFee);
                            //    }
                            //    else
                            //    {
                            //        row.ForeColor = Color.Black;
                            //        rowFee.Item.Extend1 = "0";
                            //        listBlack.Add(rowFee);
                            //    }
                            //}

                        }
                        else
                        {
                            row.ForeColor = Color.Black;
                            rowFee.Item.Extend1 = 0.ToString();
                            listBlack.Add(rowFee);
                        }
                    }


                }
                this.FpTotFee_Sheet1.CellChanged -= new SheetViewEventHandler(FpTotFee_Sheet1_CellChanged);
                this.FpTotFee_Sheet1.RowCount = 0;
                // listBlack = listBlack.Union(listBlue).Union(listRed).ToList();
                FarPoint.Win.Spread.CellType.NumberCellType nCost = new FarPoint.Win.Spread.CellType.NumberCellType();
                nCost.DecimalPlaces = 2;
                // this.FpTotFee_Sheet1.Rows.Add(0, listBlack.Count);
                foreach (FeeItemList feeItem in listBlack)
                {
                    this.FpTotFee_Sheet1.Rows.Add(this.FpTotFee_Sheet1.RowCount, 1);
                    this.FpTotFee_Sheet1.Cells[this.FpTotFee_Sheet1.RowCount - 1, (int)TotFeeCols.ItemName].Text = feeItem.Item.Name;
                    this.FpTotFee_Sheet1.Cells[this.FpTotFee_Sheet1.RowCount - 1, (int)TotFeeCols.Spec].Text = feeItem.Item.Specs;  //Spec;
                    this.FpTotFee_Sheet1.Cells[this.FpTotFee_Sheet1.RowCount - 1, (int)TotFeeCols.Price].Value = feeItem.Item.Price;
                    //{8EC220B1-2794-41DC-9511-674664B3AB33}添加单位换算
                    string Unit = string.Empty;
                    if (feeItem.Item.ItemType == EnumItemType.Drug)
                    {
                        FS.HISFC.Models.Pharmacy.Item pharmacy = feeItem.Item as FS.HISFC.Models.Pharmacy.Item;
                        Unit = pharmacy.PackUnit;
                    }
                    else Unit = feeItem.Item.PriceUnit;
                    this.FpTotFee_Sheet1.Cells[this.FpTotFee_Sheet1.RowCount - 1, (int)TotFeeCols.Unit].Text = Unit; //Unit;
                    this.FpTotFee_Sheet1.Cells[this.FpTotFee_Sheet1.RowCount - 1, (int)TotFeeCols.Qty].Text = feeItem.Item.Qty.ToString();
                    this.FpTotFee_Sheet1.Cells[this.FpTotFee_Sheet1.RowCount - 1, (int)TotFeeCols.PriceUnit].Text = feeItem.Item.PriceUnit;
                    decimal costValue = 0;
                    // if(feeItem.
                    if (feeItem.Item.PriceUnit == Unit)
                    {
                        costValue = feeItem.Item.Price * feeItem.Item.Qty;
                    }
                    else
                    {
                        costValue = Math.Round((feeItem.Item.Price / feeItem.Item.PackQty), 2) * feeItem.Item.Qty;
                    }
                    this.FpTotFee_Sheet1.Cells[this.FpTotFee_Sheet1.RowCount - 1, (int)TotFeeCols.Cost].Value = costValue; //feeItem.Item.Price * feeItem.Item.Qty; //costValue;
                    this.FpTotFee_Sheet1.Cells[this.FpTotFee_Sheet1.RowCount - 1, (int)TotFeeCols.Cost].CellType = nCost;
                    this.FpTotFee_Sheet1.Cells[this.FpTotFee_Sheet1.RowCount - 1, (int)TotFeeCols.LeftQty].Value = feeItem.Item.Qty - 0;
                    this.FpTotFee_Sheet1.Cells[this.FpTotFee_Sheet1.RowCount - 1, (int)TotFeeCols.CostQty].Value = 0;
                    if (this.FpTotFee_Sheet1.Cells[this.FpTotFee_Sheet1.RowCount - 1, (int)TotFeeCols.Spec].Text == null || this.FpTotFee_Sheet1.Cells[this.FpTotFee_Sheet1.RowCount - 1, (int)TotFeeCols.Spec].Text == "")
                        this.FpTotFee_Sheet1.Cells[this.FpTotFee_Sheet1.RowCount - 1, (int)TotFeeCols.Spec].Value = feeItem.UndrugComb.Name;

                    this.FpTotFee_Sheet1.Rows[this.FpTotFee_Sheet1.RowCount - 1].Height = this.FpTotFee_Sheet1.Rows[this.FpTotFee_Sheet1.RowCount - 1].GetPreferredHeight();
                    this.FpTotFee_Sheet1.Rows[this.FpTotFee_Sheet1.RowCount - 1].ForeColor = Color.Black;
                    this.FpTotFee_Sheet1.Rows[this.FpTotFee_Sheet1.RowCount - 1].Tag = feeItem;
                }

                foreach (FeeItemList feeItem in listBlue)
                {
                    this.FpTotFee_Sheet1.Rows.Add(this.FpTotFee_Sheet1.RowCount, 1);
                    this.FpTotFee_Sheet1.Cells[this.FpTotFee_Sheet1.RowCount - 1, (int)TotFeeCols.ItemName].Text = feeItem.Item.Name;
                    this.FpTotFee_Sheet1.Cells[this.FpTotFee_Sheet1.RowCount - 1, (int)TotFeeCols.Spec].Text = feeItem.Item.Specs;  //Spec;
                    this.FpTotFee_Sheet1.Cells[this.FpTotFee_Sheet1.RowCount - 1, (int)TotFeeCols.Price].Value = feeItem.Item.Price;
                    //{8EC220B1-2794-41DC-9511-674664B3AB33}添加单位换算
                    string Unit = string.Empty;
                    if (feeItem.Item.ItemType == EnumItemType.Drug)
                    {
                        FS.HISFC.Models.Pharmacy.Item pharmacy = feeItem.Item as FS.HISFC.Models.Pharmacy.Item;
                        Unit = pharmacy.PackUnit;
                    }
                    else Unit = feeItem.Item.PriceUnit;
                    decimal costValue = 0;
                    if (feeItem.Item.PriceUnit == Unit)
                    {
                        costValue = feeItem.Item.Price * feeItem.Item.Qty;
                    }
                    else
                    {
                        costValue = Math.Round((feeItem.Item.Price / feeItem.Item.PackQty), 2) * feeItem.Item.Qty;
                    }
                    this.FpTotFee_Sheet1.Cells[this.FpTotFee_Sheet1.RowCount - 1, (int)TotFeeCols.Unit].Text = Unit; //Unit;
                    this.FpTotFee_Sheet1.Cells[this.FpTotFee_Sheet1.RowCount - 1, (int)TotFeeCols.Qty].Text = feeItem.Item.Qty.ToString();
                    this.FpTotFee_Sheet1.Cells[this.FpTotFee_Sheet1.RowCount - 1, (int)TotFeeCols.PriceUnit].Text = feeItem.Item.PriceUnit;
                    this.FpTotFee_Sheet1.Cells[this.FpTotFee_Sheet1.RowCount - 1, (int)TotFeeCols.Cost].Value = costValue; //feeItem.Item.Price * feeItem.Item.Qty; //costValue;
                    this.FpTotFee_Sheet1.Cells[this.FpTotFee_Sheet1.RowCount - 1, (int)TotFeeCols.Cost].CellType = nCost;
                    this.FpTotFee_Sheet1.Cells[this.FpTotFee_Sheet1.RowCount - 1, (int)TotFeeCols.LeftQty].Value = feeItem.Item.Qty - Convert.ToDecimal(feeItem.Item.Extend1);
                    this.FpTotFee_Sheet1.Cells[this.FpTotFee_Sheet1.RowCount - 1, (int)TotFeeCols.CostQty].Value = Convert.ToDecimal(feeItem.Item.Extend1); ;
                    //{F4BED02C-9181-427f-9D26-CF3FF7364E1A}
                    if (this.FpTotFee_Sheet1.Cells[this.FpTotFee_Sheet1.RowCount - 1, (int)TotFeeCols.Spec].Text == null || this.FpTotFee_Sheet1.Cells[this.FpTotFee_Sheet1.RowCount - 1, (int)TotFeeCols.Spec].Text == "")
                        this.FpTotFee_Sheet1.Cells[this.FpTotFee_Sheet1.RowCount - 1, (int)TotFeeCols.Spec].Value = feeItem.UndrugComb.Name;

                    this.FpTotFee_Sheet1.Rows[this.FpTotFee_Sheet1.RowCount - 1].Height = this.FpTotFee_Sheet1.Rows[this.FpTotFee_Sheet1.RowCount - 1].GetPreferredHeight();
                    this.FpTotFee_Sheet1.Rows[this.FpTotFee_Sheet1.RowCount - 1].ForeColor = Color.Blue;
                    this.FpTotFee_Sheet1.Rows[this.FpTotFee_Sheet1.RowCount - 1].Tag = feeItem;
                }

                foreach (FeeItemList feeItem in listRed)
                {
                    this.FpTotFee_Sheet1.Rows.Add(this.FpTotFee_Sheet1.RowCount, 1);
                    this.FpTotFee_Sheet1.Cells[this.FpTotFee_Sheet1.RowCount - 1, (int)TotFeeCols.ItemName].Text = feeItem.Item.Name;
                    this.FpTotFee_Sheet1.Cells[this.FpTotFee_Sheet1.RowCount - 1, (int)TotFeeCols.Spec].Text = feeItem.Item.Specs;  //Spec;
                    this.FpTotFee_Sheet1.Cells[this.FpTotFee_Sheet1.RowCount - 1, (int)TotFeeCols.Price].Value = feeItem.Item.Price;
                    // FS.HISFC.Models.Pharmacy.Item pharmacy = feeItem.Item as FS.HISFC.Models.Pharmacy.Item;
                    //{8EC220B1-2794-41DC-9511-674664B3AB33}添加单位换算
                    string Unit = string.Empty;
                    if (feeItem.Item.ItemType == EnumItemType.Drug)
                    {
                        FS.HISFC.Models.Pharmacy.Item pharmacy = feeItem.Item as FS.HISFC.Models.Pharmacy.Item;
                        Unit = pharmacy.PackUnit;
                    }
                    else Unit = feeItem.Item.PriceUnit;
                    decimal costValue = 0;
                    if (feeItem.Item.PriceUnit == Unit)
                    {
                        costValue = feeItem.Item.Price * feeItem.Item.Qty;
                    }
                    else
                    {
                        costValue = Math.Round((feeItem.Item.Price / feeItem.Item.PackQty), 2) * feeItem.Item.Qty;
                    }
                    this.FpTotFee_Sheet1.Cells[this.FpTotFee_Sheet1.RowCount - 1, (int)TotFeeCols.Unit].Text = Unit; //Unit;
                    this.FpTotFee_Sheet1.Cells[this.FpTotFee_Sheet1.RowCount - 1, (int)TotFeeCols.Qty].Text = feeItem.Item.Qty.ToString();
                    this.FpTotFee_Sheet1.Cells[this.FpTotFee_Sheet1.RowCount - 1, (int)TotFeeCols.PriceUnit].Text = feeItem.Item.PriceUnit;
                    this.FpTotFee_Sheet1.Cells[this.FpTotFee_Sheet1.RowCount - 1, (int)TotFeeCols.Cost].Value = costValue; //feeItem.Item.Price * feeItem.Item.Qty; //costValue;
                    this.FpTotFee_Sheet1.Cells[this.FpTotFee_Sheet1.RowCount - 1, (int)TotFeeCols.Cost].CellType = nCost;
                    this.FpTotFee_Sheet1.Cells[this.FpTotFee_Sheet1.RowCount - 1, (int)TotFeeCols.LeftQty].Value = feeItem.Item.Qty - Convert.ToDecimal(feeItem.Item.Extend1);
                    this.FpTotFee_Sheet1.Cells[this.FpTotFee_Sheet1.RowCount - 1, (int)TotFeeCols.CostQty].Value = Convert.ToDecimal(feeItem.Item.Extend1);
                    this.FpTotFee_Sheet1.Rows[this.FpTotFee_Sheet1.RowCount - 1].Height = this.FpTotFee_Sheet1.Rows[this.FpTotFee_Sheet1.RowCount - 1].GetPreferredHeight();
                    if (this.FpTotFee_Sheet1.Cells[this.FpTotFee_Sheet1.RowCount - 1, (int)TotFeeCols.Spec].Text == null || this.FpTotFee_Sheet1.Cells[this.FpTotFee_Sheet1.RowCount - 1, (int)TotFeeCols.Spec].Text == "")
                        this.FpTotFee_Sheet1.Cells[this.FpTotFee_Sheet1.RowCount - 1, (int)TotFeeCols.Spec].Value = feeItem.UndrugComb.Name;
                    this.FpTotFee_Sheet1.Rows[this.FpTotFee_Sheet1.RowCount - 1].ForeColor = Color.Red;
                    this.FpTotFee_Sheet1.Rows[this.FpTotFee_Sheet1.RowCount - 1].Tag = feeItem;

                }

                this.FpTotFee_Sheet1.CellChanged += new SheetViewEventHandler(FpTotFee_Sheet1_CellChanged);




                //foreach (string key in packageItemKeys)
                //{
                //    FeeItemList packageFee = this.hsPackageFeeItem[key] as FeeItemList;
                //    decimal leftQty = packageFee.Item.Qty;
                //    foreach (Row row in this.FpTotFee_Sheet1.Rows)
                //    {
                //        FeeItemList rowFee = row.Tag as FeeItemList;
                //        if (packageFee.Item.ID == rowFee.Item.ID && packageFee.UndrugComb.ID == rowFee.UndrugComb.ID)
                //        {
                //            decimal cost = Decimal.Parse(this.FpTotFee_Sheet1.Cells[row.Index, (int)TotFeeCols.CostQty].Value.ToString());
                //            if (rowFee.Item.Qty - cost >= leftQty)
                //            {

                //                if (rowFee.Item.Qty == cost + leftQty)
                //                {
                //                    row.ForeColor = Color.Red;
                //                }
                //                else
                //                {
                //                    row.ForeColor = Color.Blue;
                //                }

                //                this.FpTotFee_Sheet1.Cells[row.Index, (int)TotFeeCols.CostQty].Value = cost + leftQty;
                //                leftQty = 0;
                //                break;
                //            }
                //            else
                //            {
                //                this.FpTotFee_Sheet1.Cells[row.Index, (int)TotFeeCols.CostQty].Value = rowFee.Item.Qty;
                //                row.ForeColor = Color.Red;
                //                leftQty -= (rowFee.Item.Qty - cost);
                //            }
                //        }
                //        else if (rowFee.Item.IsPharmacy && rowFee.Item.Price < 100)
                //        {
                //            this.FpTotFee_Sheet1.Cells[row.Index, (int)TotFeeCols.CostQty].Value = rowFee.Item.Qty;
                //        }
                //        else if (rowFee.Item.IsPharmacy == false && rowFee.Item.Price < 1000)
                //        {
                //            this.FpTotFee_Sheet1.Cells[row.Index, (int)TotFeeCols.CostQty].Value = rowFee.Item.Qty;
                //        }
                //    }
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show("匹配出错：" + ex.Message);
            }

            this.IsMatching = false;
            //{14CCBD16-9A45-42f8-896C-5A2CB00DAB1B}
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            this.SetCostInfo(this.BalanceEco);
        }

        /// <summary>
        /// 显示选中套餐的详情
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPackageDetail_Click(object sender, EventArgs e)
        {
            if (frmPackageDetail == null || frmPackageDetail.IsDisposed)
            {
                frmPackageDetail = new frmFeeItems();

                //{14CCBD16-9A45-42f8-896C-5A2CB00DAB1B}
                this.hsSelectedFeeItem.Clear();
                foreach (FS.HISFC.Models.MedicalPackage.Fee.Package row in this.SelectedPackage)
                {
                    this.ConvertPackageInFeeItem(row);
                }

                if (this.hsPackageFeeItem.Keys.Count == 0)
                {
                    MessageBox.Show("请先选择套餐并进行匹配！");
                    return;
                }

                this.frmPackageDetail.SetFeeItemList(new ArrayList(this.hsPackageFeeItem.Values), "");
                this.frmPackageDetail.Show();
            }
            else
            {
                frmPackageDetail.Activate();
            }
        }

        /// <summary>
        /// 显示选中预结算的详情  //{C4231074-D350-4df9-AF7C-C37124B44B80}
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnprebalancedetail_Click(object sender, EventArgs e)
        {
            if (FpPreBalance_Sheet1.RowCount > 0)
            {

                frmprebalancedeta = new frmPreBalanceItem();

                PreBalance head = FpPreBalance_Sheet1.ActiveRow.Tag as PreBalance;
                if (head != null)
                {
                    this.frmprebalancedeta.SetFeeItemList(head.PREBLANCENO);
                    this.frmprebalancedeta.Show();
                }
            }
        }

        /// <summary>
        /// 预结算匹配  //{C4231074-D350-4df9-AF7C-C37124B44B80}
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPreBalancePP_Click(object sender, EventArgs e)
        {
            btnAllClear_Click(sender, e);
            PreBalanceMatching();
        }


        #endregion

        #region FarPoint各种事件

        /// <summary>
        /// 费用明细数目变化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FpTotFee_Sheet1_CellChanged(object sender, SheetViewEventArgs e)
        {
            #region
            try
            {
                this.DelEvents();
                FarPoint.Win.Spread.CellType.NumberCellType nCost = new FarPoint.Win.Spread.CellType.NumberCellType();
                nCost.DecimalPlaces = 2;

                SheetView SheetObj = sender as SheetView;
                if (SheetObj == null)
                {
                    return;
                }

                //此处的FeeItemList为统计时的复合串
                FeeItemList curFee = SheetObj.Rows[e.Row].Tag as FeeItemList;

                if (curFee == null)
                {
                    return;
                }

                if (e.Column != (int)TotFeeCols.CostQty)
                {
                    return;
                }

                decimal tot = Decimal.Parse(SheetObj.Cells[e.Row, (int)TotFeeCols.Qty].Value.ToString());
                decimal cost = Decimal.Parse(SheetObj.Cells[e.Row, (int)TotFeeCols.CostQty].Value.ToString());

                if (cost > tot || cost < 0)
                {
                    SheetObj.Cells[e.Row, (int)TotFeeCols.CostQty].Value = 0.0m;
                    SheetObj.Rows[e.Row].ForeColor = Color.Black;
                    cost = 0;
                    MessageBox.Show("分配失败：数量不足！");
                }

                decimal left = tot - cost;
                SheetObj.Cells[e.Row, (int)TotFeeCols.LeftQty].Value = left;

                if (left == 0)
                {
                    SheetObj.Rows[e.Row].ForeColor = Color.Red;
                }
                else if (left == tot)
                {
                    SheetObj.Rows[e.Row].ForeColor = Color.Black;
                }
                else
                {
                    SheetObj.Rows[e.Row].ForeColor = Color.Blue;
                }

                FeeItemList SelectedItem = null;
                if (this.hsSelectedFeeItem.ContainsKey(curFee.ID))
                {
                    SelectedItem = this.hsSelectedFeeItem[curFee.ID] as FeeItemList;
                    SelectedItem.Item.Qty = cost;

                    SelectedItem.FT.TotCost = 0.0m;
                    SelectedItem.FT.OwnCost = 0.0m;

                    decimal leftQty = cost;
                    //区分药品和非药品计算费用
                    if (curFee.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                    {

                        //针对药品按数量进行升序排列，得到确定的金额，和结账的时候一样，不能随意改动
                        //此处跟balance函数SplitMedicineItem,SplitUndrugItem里的算法相对应，此处改动，结算会有问题
                        foreach (FeeItemList drug in this.MedicineList.Cast<FeeItemList>()
                                                                      .Where<FeeItemList>(t => t.Item.ID == SelectedItem.Item.ID)
                                                                      .Where<FeeItemList>(t => t.Item.Price == SelectedItem.Item.Price)
                                                                      .Where<FeeItemList>(t => t.Item.PriceUnit == SelectedItem.Item.PriceUnit)
                                                                      .OrderBy(t => t.Item.Qty))
                        {
                            if (leftQty == 0)
                                break;

                            if (leftQty >= drug.Item.Qty)
                            {
                                SelectedItem.FT.TotCost += drug.FT.TotCost;
                                SelectedItem.FT.OwnCost += drug.FT.OwnCost;
                                leftQty -= drug.Item.Qty;
                            }
                            else
                            {
                                SelectedItem.FT.TotCost += Math.Round((drug.FT.TotCost * leftQty) / drug.Item.Qty, 2, MidpointRounding.AwayFromZero);//23费用金额
                                SelectedItem.FT.OwnCost += Math.Round((drug.FT.OwnCost * leftQty) / drug.Item.Qty, 2, MidpointRounding.AwayFromZero);//24自费金额
                                leftQty = 0.0m;
                            }
                        }
                    }
                    else
                    {
                        //针对药品按数量进行升序排列，得到确定的金额，和结账的时候一样，不能随意改动
                        //此处跟balance函数SplitMedicineItem,SplitUndrugItem里的算法相对应，此处改动，结算会有问题
                        foreach (FeeItemList item in this.UndrugList.Cast<FeeItemList>()
                                                                    .Where<FeeItemList>(t => t.Item.ID == SelectedItem.Item.ID)
                                                                    .Where<FeeItemList>(t => t.Item.Price == SelectedItem.Item.Price)
                                                                    .Where<FeeItemList>(t => t.UndrugComb.ID == SelectedItem.UndrugComb.ID)
                                                                    .OrderBy(t => t.Item.Qty))
                        {
                            if (leftQty == 0)
                                break;


                            if (leftQty >= item.Item.Qty)
                            {
                                SelectedItem.FT.TotCost += item.FT.TotCost;
                                SelectedItem.FT.OwnCost += item.FT.OwnCost;
                                leftQty -= item.Item.Qty;
                            }
                            else
                            {
                                SelectedItem.FT.TotCost += Math.Round((item.FT.TotCost * leftQty) / item.Item.Qty, 2, MidpointRounding.AwayFromZero);//23费用金额
                                SelectedItem.FT.OwnCost += Math.Round((item.FT.OwnCost * leftQty) / item.Item.Qty, 2, MidpointRounding.AwayFromZero);//24自费金额
                                leftQty = 0.0m;
                            }
                        }
                    }

                    foreach (Row row in this.FpSelectedFee_Sheet1.Rows)
                    {
                        FeeItemList updateFee = row.Tag as FeeItemList;
                        if (updateFee.ID == SelectedItem.ID)
                        {
                            this.FpSelectedFee_Sheet1.Cells[row.Index, (int)SelectedFeeCols.Qty].Value = SelectedItem.Item.Qty;
                            //{8EC220B1-2794-41DC-9511-674664B3AB33}添加单位换算
                            string unit = string.Empty;
                            if (SelectedItem.Item.ItemType == EnumItemType.Drug)
                            {
                                FS.HISFC.Models.Pharmacy.Item pharmacy = SelectedItem.Item as FS.HISFC.Models.Pharmacy.Item;
                                unit = pharmacy.PackUnit;
                            }
                            else unit = SelectedItem.Item.PriceUnit;
                            decimal costValue = 0;

                            //{79BCE70C-92E3-4d05-82E8-39A063904B90}
                            //if (SelectedItem.Item.PriceUnit == unit)
                            //{
                            //    costValue = SelectedItem.Item.Price * SelectedItem.Item.Qty;
                            //}
                            //else
                            //{
                            //    costValue = Math.Round((SelectedItem.Item.Price / SelectedItem.Item.PackQty), 2) * SelectedItem.Item.Qty;
                            //}
                            costValue = SelectedItem.FT.TotCost;

                            this.FpSelectedFee_Sheet1.Cells[row.Index, (int)SelectedFeeCols.Cost].Value = costValue; //SelectedItem.Item.Price * SelectedItem.Item.Qty;// {F53BD032-1D92-4447-8E20-6C38033AA607}
                            row.Tag = SelectedItem;
                            if (SelectedItem.Item.Qty == 0)
                            {
                                this.FpSelectedFee_Sheet1.Rows.Remove(row.Index, 1);
                                this.hsSelectedFeeItem.Remove(curFee.ID);
                            }
                            break;
                        }
                    }
                }
                else
                {
                    if (cost <= 0)
                        return;

                    SelectedItem = new FeeItemList();
                    SelectedItem.ID = curFee.ID;

                    //区分药品和非药品计算费用
                    if (curFee.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        SelectedItem.Item = this.pharmacyMgr.GetItem(curFee.Item.ID);
                        SelectedItem.Item.Qty = cost;
                        SelectedItem.Item.Price = curFee.Item.Price;
                        SelectedItem.Item.Specs = curFee.Item.Specs;
                        SelectedItem.Item.PriceUnit = curFee.Item.PriceUnit;

                        SelectedItem.FT.TotCost = 0.0m;
                        SelectedItem.FT.OwnCost = 0.0m;
                        decimal leftQty = cost;

                        //针对药品按数量进行升序排列，得到确定的金额，和结账的时候一样，不能随意改动
                        //此处跟balance函数SplitMedicineItem,SplitUndrugItem里的算法相对应，此处改动，结算会有问题
                        foreach (FeeItemList drug in this.MedicineList.Cast<FeeItemList>()
                                                                      .Where<FeeItemList>(t => t.Item.ID == SelectedItem.Item.ID)
                                                                      .Where<FeeItemList>(t => t.Item.Price == SelectedItem.Item.Price)
                                                                      .Where<FeeItemList>(t => t.Item.PriceUnit == SelectedItem.Item.PriceUnit)
                                                                      .OrderBy(t => t.Item.Qty))
                        {
                            if (leftQty == 0)
                                break;

                            //{394A481D-6C5C-4419-BEC5-AE1EA0D1E96B}
                            if (drug.Item.Qty <= 0)
                            {
                                continue;
                            }

                            if (leftQty >= drug.Item.Qty)
                            {
                                SelectedItem.FT.TotCost += drug.FT.TotCost;
                                SelectedItem.FT.OwnCost += drug.FT.OwnCost;
                                leftQty -= drug.Item.Qty;
                            }
                            else
                            {
                                SelectedItem.FT.TotCost += Math.Round((drug.FT.TotCost * leftQty) / drug.Item.Qty, 2, MidpointRounding.AwayFromZero);//23费用金额
                                SelectedItem.FT.OwnCost += Math.Round((drug.FT.OwnCost * leftQty) / drug.Item.Qty, 2, MidpointRounding.AwayFromZero);//24自费金额
                                leftQty = 0.0m;
                            }
                        }
                    }
                    else
                    {
                        SelectedItem.UndrugComb = curFee.UndrugComb;
                        SelectedItem.Item = this.undrugMgr.GetUndrugByCode(curFee.Item.ID);
                        SelectedItem.Item.Qty = cost;
                        SelectedItem.Item.Specs = curFee.Item.Specs;
                        SelectedItem.Item.Price = curFee.Item.Price;
                        SelectedItem.Item.PriceUnit = curFee.Item.PriceUnit;

                        SelectedItem.FT.TotCost = 0.0m;
                        SelectedItem.FT.OwnCost = 0.0m;
                        decimal leftQty = cost;

                        foreach (FeeItemList item in this.UndrugList.Cast<FeeItemList>()
                                                                    .Where<FeeItemList>(t => t.Item.ID == SelectedItem.Item.ID)
                                                                    .Where<FeeItemList>(t => t.Item.Price == SelectedItem.Item.Price)
                                                                    .Where<FeeItemList>(t => t.UndrugComb.ID == SelectedItem.UndrugComb.ID)
                                                                    .OrderBy(t => t.Item.Qty))
                        {
                            if (leftQty == 0)
                                break;

                            //{394A481D-6C5C-4419-BEC5-AE1EA0D1E96B}
                            if (item.Item.Qty <= 0)
                            {
                                continue;
                            }

                            if (leftQty >= item.Item.Qty)
                            {
                                SelectedItem.FT.TotCost += item.FT.TotCost;
                                SelectedItem.FT.OwnCost += item.FT.OwnCost;
                                leftQty -= item.Item.Qty;
                            }
                            else
                            {
                                SelectedItem.FT.TotCost += Math.Round((item.FT.TotCost * leftQty) / item.Item.Qty, 2, MidpointRounding.AwayFromZero);//23费用金额
                                SelectedItem.FT.OwnCost += Math.Round((item.FT.OwnCost * leftQty) / item.Item.Qty, 2, MidpointRounding.AwayFromZero);//24自费金额
                                leftQty = 0.0m;
                            }
                        }
                    }

                    this.hsSelectedFeeItem.Add(curFee.ID, SelectedItem);

                    int insertIndex = 0;
                    foreach (Row row in this.FpSelectedFee_Sheet1.Rows)
                    {
                        FeeItemList rowFee = row.Tag as FeeItemList;
                        if (SelectedItem.ID.CompareTo(rowFee.ID) > 0)
                        {
                            insertIndex++;
                            continue;
                        }
                        break;
                    }

                    this.FpSelectedFee_Sheet1.Rows.Add(insertIndex, 1);
                    this.FpSelectedFee_Sheet1.Rows[insertIndex].Tag = SelectedItem;
                    this.FpSelectedFee_Sheet1.Cells[insertIndex, (int)SelectedFeeCols.ItemName].Text = SelectedItem.Item.Name;
                    this.FpSelectedFee_Sheet1.Cells[insertIndex, (int)SelectedFeeCols.Spec].Text = SheetObj.Cells[e.Row, (int)TotFeeCols.Spec].Text;
                    this.FpSelectedFee_Sheet1.Cells[insertIndex, (int)SelectedFeeCols.Price].Value = SelectedItem.Item.Price;
                    this.FpSelectedFee_Sheet1.Cells[insertIndex, (int)SelectedFeeCols.Unit].Text = SheetObj.Cells[e.Row, (int)TotFeeCols.Unit].Text;
                    this.FpSelectedFee_Sheet1.Cells[insertIndex, (int)SelectedFeeCols.Qty].Value = SelectedItem.Item.Qty;
                    this.FpSelectedFee_Sheet1.Cells[insertIndex, (int)SelectedFeeCols.PriceUnit].Text = SelectedItem.Item.PriceUnit;
                    //{8EC220B1-2794-41DC-9511-674664B3AB33}添加单位换算
                    string unit = string.Empty;
                    if (SelectedItem.Item.ItemType == EnumItemType.Drug)
                    {
                        FS.HISFC.Models.Pharmacy.Item pharmacy = SelectedItem.Item as FS.HISFC.Models.Pharmacy.Item;
                        unit = pharmacy.PackUnit;
                    }
                    else unit = SelectedItem.Item.PriceUnit;
                    decimal costValue = 0;

                    //{79BCE70C-92E3-4d05-82E8-39A063904B90}
                    //if (SelectedItem.Item.PriceUnit == unit)
                    //{
                    //    costValue = SelectedItem.Item.Price * SelectedItem.Item.Qty;
                    //}
                    //else
                    //{
                    //    costValue = Math.Round((SelectedItem.Item.Price / SelectedItem.Item.PackQty), 2) * SelectedItem.Item.Qty;
                    //}
                    costValue = SelectedItem.FT.TotCost;

                    this.FpSelectedFee_Sheet1.Cells[insertIndex, (int)SelectedFeeCols.Cost].Value = costValue; //SelectedItem.Item.Price * SelectedItem.Item.Qty;
                    this.FpSelectedFee_Sheet1.Cells[insertIndex, (int)SelectedFeeCols.Cost].CellType = nCost;
                }

                //如果是正在进行套餐匹配，则暂时不进行设置
                if (!this.IsMatching)
                {
                    this.SetCostInfo(this.BalanceEco);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("操作失败：" + ex.Message);
            }
            finally
            {
                this.AddEvents();
            }
            #endregion
        }

        /// <summary>
        /// 预交金勾选事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FpPrepay_ButtonClicked(object sender, EditorNotifyEventArgs e)
        {
            if (patientInfo == null || string.IsNullOrEmpty(this.patientInfo.ID))
            {
                return;
            }

            this.SetCostInfo(this.BalanceEco);
        }

        /// <summary>
        /// 套餐勾选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FpPackage_ButtonClicked(object sender, EditorNotifyEventArgs e)
        {
            //{14CCBD16-9A45-42f8-896C-5A2CB00DAB1B}
            this.hsPackageFeeItem.Clear();
            this.SelectedPackage = this.GetPackageInfo();
            this.SetCostInfo(this.BalanceEco);

            //{14CCBD16-9A45-42f8-896C-5A2CB00DAB1B}
            return;

            //foreach (Row row in this.FpPackage_Sheet1.Rows)
            //{
            //    if ((bool)(this.FpPackage_Sheet1.Cells[row.Index, (int)PackageCols.Check].Value))
            //    {
            //        FS.HISFC.Models.MedicalPackage.Fee.Package package = row.Tag as FS.HISFC.Models.MedicalPackage.Fee.Package;
            //        this.ConvertPackageInFeeItem(package);
            //    }
            //}
        }

        /// <summary>
        /// 双击定位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FpSelectedFee_CellDoubleClick(object sender, CellClickEventArgs e)
        {
            FeeItemList slec = this.FpSelectedFee_Sheet1.ActiveRow.Tag as FeeItemList;
            if (slec == null)
            {
                return;
            }
            foreach (Row row in this.FpTotFee_Sheet1.Rows)
            {
                FeeItemList tmp = row.Tag as FeeItemList;
                if (tmp != null && slec.ID == tmp.ID)
                {
                    this.FpTotFee_Sheet1.SetActiveCell(row.Index, (int)TotFeeCols.CostQty);
                    this.FpTotFee.ShowActiveCell(VerticalPosition.Nearest, HorizontalPosition.Nearest);
                    break;
                }
            }
        }

        #endregion

        #endregion

        #endregion

        #region 私有函数

        /// <summary>
        /// 设置金额比例优惠控件是否可见
        /// </summary>
        /// <param name="visible"></param>
        private void SetCostSetControls(bool visible)
        {
            this.tbEcoSet.Visible = visible;
            this.tbDisaccount.Visible = visible;
            this.tbRealSet.Visible = visible;
            this.btnEcoSet.Visible = visible;
            this.btnDisaccount.Visible = visible;
            this.btnRealSet.Visible = visible;
        }

        /// <summary>
        /// 显示患者费用信息
        /// </summary>
        /// <returns>1成功 －1失败</returns>
        protected virtual int DisplayPatientCost(ref string errText)
        {
            try
            {
                this.DelEvents();
                this.SortItemInfo();
                this.SetFpShowInfo();
                this.SetCostInfo(this.BalanceEco);
            }
            catch
            { }
            finally
            {
                this.AddEvents();
            }
            return 1;
        }

        /// <summary>
        /// 对费用项目进行统计
        /// </summary>
        /// <returns></returns>
        private int SortItemInfo()
        {
            try
            {
                this.ClearFee();

                #region 屏蔽
                ////针对项目名称进行排序
                //IOrderedEnumerable < FeeItemList> tmpMed = this.MedicineList.Cast<FeeItemList>().OrderBy(t => t.Item.Name);
                //IOrderedEnumerable < FeeItemList> tmpUndrug = this.UndrugList.Cast<FeeItemList>().OrderByDescending(t => t.UndrugComb.Name).OrderBy(t => t.Item.Name);

                ////药品费用【药品ID+计价单位】作为Key
                //foreach (FeeItemList feeItem in MedicineList)
                //{
                //    feeItemList.Add(feeItem);
                //    FS.HISFC.Models.Pharmacy.Item pharmacyItem = feeItem.Item as FS.HISFC.Models.Pharmacy.Item;
                //    string key = pharmacyItem.ID + pharmacyItem.PriceUnit;  //购买单位

                //    if (this.hsFeeItem.ContainsKey(key))
                //    {
                //        ArrayList sameItem = this.hsFeeItem[key] as ArrayList;
                //        sameItem.Add(feeItem);
                //    }
                //    else
                //    {
                //        ArrayList sameItem = new ArrayList();
                //        sameItem.Add(feeItem);
                //        this.hsFeeItem.Add(key, sameItem);
                //    }
                //}

                //foreach (FeeItemList feeItem in UndrugList)
                //{
                //    feeItemList.Add(feeItem);
                //    string itemKey = feeItem.UndrugComb.ID + feeItem.Item.ID;

                //    if (this.hsFeeItem.ContainsKey(itemKey))
                //    {
                //        ArrayList sameItem = this.hsFeeItem[itemKey] as ArrayList;
                //        sameItem.Add(feeItem);
                //    }
                //    else
                //    {
                //        ArrayList sameItem = new ArrayList();
                //        sameItem.Add(feeItem);
                //        this.hsFeeItem.Add(itemKey, sameItem);
                //    }

                //}
                #endregion

                //药品费用【药品ID+计价单位+价格(小数点后四位)】作为Key
                foreach (FeeItemList feeItem in this.MedicineList)
                {
                    feeItemList.Add(feeItem);
                    FS.HISFC.Models.Base.Item item = null;
                    FS.HISFC.Models.Pharmacy.Item pharmacyItem = feeItem.Item as FS.HISFC.Models.Pharmacy.Item;
                    string key = pharmacyItem.ID + pharmacyItem.PriceUnit + pharmacyItem.Price.ToString("F4");  //购买单位

                    if (this.hsFeeItem.ContainsKey(key))
                    {
                        FeeItemList fee = this.hsFeeItem[key] as FeeItemList;
                        item = fee.Item;
                        item.Qty += feeItem.Item.Qty;
                        fee.FT.TotCost += feeItem.FT.TotCost;
                        fee.FT.OwnCost += feeItem.FT.OwnCost;
                        fee.FT.PayCost += feeItem.FT.PayCost;
                        fee.FT.PubCost += feeItem.FT.PubCost;
                        fee.FT.RebateCost += feeItem.FT.RebateCost;
                    }
                    else
                    {
                        FeeItemList fee = new FeeItemList();
                        fee.ID = key;
                        fee.FT.TotCost = feeItem.FT.TotCost;
                        fee.FT.OwnCost = feeItem.FT.OwnCost;
                        fee.FT.PayCost = feeItem.FT.PayCost;
                        fee.FT.PubCost = feeItem.FT.PubCost;
                        fee.FT.RebateCost = feeItem.FT.RebateCost;

                        item = this.pharmacyMgr.GetItem(feeItem.Item.ID);
                        item.Qty = pharmacyItem.Qty;
                        item.Price = pharmacyItem.Price;
                        item.Specs = pharmacyItem.Specs;
                        item.PriceUnit = pharmacyItem.PriceUnit;

                        fee.Item = item;
                        this.hsFeeItem.Add(key, fee);
                    }
                }

                //非药品费用【复合项目ID+项目ID+价格(小数点后四位)】作为Key
                foreach (FeeItemList feeItem in this.UndrugList)
                {
                    feeItemList.Add(feeItem);
                    FS.HISFC.Models.Base.Item item = null;
                    string itemKey = feeItem.UndrugComb.ID + feeItem.Item.ID + feeItem.Item.Price.ToString("F4");
                    if (this.hsFeeItem.ContainsKey(itemKey))
                    {
                        FeeItemList fee = this.hsFeeItem[itemKey] as FeeItemList;
                        item = fee.Item;
                        item.Qty += feeItem.Item.Qty;
                        fee.FT.TotCost += feeItem.FT.TotCost;
                        fee.FT.OwnCost += feeItem.FT.OwnCost;
                        fee.FT.PayCost += feeItem.FT.PayCost;
                        fee.FT.PubCost += feeItem.FT.PubCost;
                        fee.FT.RebateCost += feeItem.FT.RebateCost;
                    }
                    else
                    {
                        FeeItemList fee = new FeeItemList();
                        fee.ID = itemKey;
                        fee.UndrugComb = feeItem.UndrugComb;
                        fee.FT.TotCost = feeItem.FT.TotCost;
                        fee.FT.OwnCost = feeItem.FT.OwnCost;
                        fee.FT.PayCost = feeItem.FT.PayCost;
                        fee.FT.PubCost = feeItem.FT.PubCost;
                        fee.FT.RebateCost = feeItem.FT.RebateCost;

                        item = this.undrugMgr.GetUndrugByCode(feeItem.Item.ID);
                        item.Qty = feeItem.Item.Qty;
                        item.Specs = feeItem.Item.Specs;
                        item.Price = feeItem.Item.Price;
                        item.PriceUnit = feeItem.Item.PriceUnit;
                       
                        fee.Item = item;
                        fee.IsDiscount = feeItem.IsDiscount;
                        this.hsFeeItem.Add(itemKey, fee);
                    }
                }
                //{15E1CDD6-FBA9-4d93-8204-0BC788EBC265} 为查找收费项目下拉框赋值
                ArrayList arr = new ArrayList();
                foreach (string item in hsFeeItem.Keys)
                {
                    FeeItemList fee = hsFeeItem[item] as FeeItemList;
                    Const cst = new Const();
                    cst.ID = fee.Item.ID;
                    cst.Name = fee.Item.Name;
                    arr.Add(cst);
                }
                ncbxItem.AddItems(arr);

            }
            catch
            {
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 设置Fp显示信息
        /// </summary>
        /// <returns></returns>
        private int SetFpShowInfo()
        {
            string ErrInfo = string.Empty;
            SetFpTot();
            SetFPPrepay();
            SetPackageInfo();
            GetPreBalance();
            return 1;
        }

        /// <summary>
        /// 设置总费用列表
        /// </summary>
        private void SetFpTot()
        {

            ArrayList feeItemKeys = new ArrayList(hsFeeItem.Keys);
            feeItemKeys.Sort();
            FarPoint.Win.Spread.CellType.NumberCellType nCost = new FarPoint.Win.Spread.CellType.NumberCellType();
            nCost.DecimalPlaces = 2;

            foreach (string key in feeItemKeys)
            {
                FeeItemList feeItem = this.hsFeeItem[key] as FeeItemList;

                //正负记录相互抵消的不显示
                if (feeItem.Item.Qty == 0)
                {
                    continue;
                }

                this.FpTotFee_Sheet1.Rows.Add(this.FpTotFee_Sheet1.RowCount, 1);
                this.FpTotFee_Sheet1.Rows[this.FpTotFee_Sheet1.RowCount - 1].Tag = feeItem;

                string Spec = string.Empty;
                string Unit = string.Empty;
                if (!string.IsNullOrEmpty(feeItem.UndrugComb.ID) && feeItem.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.UnDrug &&
                    (feeItem.Item.SysClass.ID.ToString() == "UL" || feeItem.Item.SysClass.ID.ToString() == "UC"))
                {
                    Spec = feeItem.UndrugComb.Name;
                }
                else
                {
                    Spec = feeItem.Item.Specs;
                }

                //{658EC0E6-CA03-4160-9561-99DA7057D0C9}
                decimal costValue = 0.0m;
                if (feeItem.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                {
                    FS.HISFC.Models.Pharmacy.Item pharmacy = feeItem.Item as FS.HISFC.Models.Pharmacy.Item;
                    Unit = pharmacy.PackUnit;

                    if (feeItem.Item.PriceUnit == Unit)
                    {
                        costValue = feeItem.Item.Price * feeItem.Item.Qty;
                    }
                    else
                    {
                        costValue = Math.Round((feeItem.Item.Price / feeItem.Item.PackQty), 2)
                            * feeItem.Item.Qty;
                    }
                }
                else
                {
                    costValue = feeItem.Item.Price * feeItem.Item.Qty;
                }


                this.FpTotFee_Sheet1.Cells[this.FpTotFee_Sheet1.RowCount - 1, (int)TotFeeCols.ItemName].Text = feeItem.Item.Name;
                this.FpTotFee_Sheet1.Cells[this.FpTotFee_Sheet1.RowCount - 1, (int)TotFeeCols.Spec].Text = Spec;
                this.FpTotFee_Sheet1.Cells[this.FpTotFee_Sheet1.RowCount - 1, (int)TotFeeCols.Price].Value = feeItem.Item.Price;
                this.FpTotFee_Sheet1.Cells[this.FpTotFee_Sheet1.RowCount - 1, (int)TotFeeCols.Unit].Text = Unit;
                this.FpTotFee_Sheet1.Cells[this.FpTotFee_Sheet1.RowCount - 1, (int)TotFeeCols.Qty].Text = feeItem.Item.Qty.ToString();
                this.FpTotFee_Sheet1.Cells[this.FpTotFee_Sheet1.RowCount - 1, (int)TotFeeCols.PriceUnit].Text = feeItem.Item.PriceUnit;
                //{658EC0E6-CA03-4160-9561-99DA7057D0C9}
                this.FpTotFee_Sheet1.Cells[this.FpTotFee_Sheet1.RowCount - 1, (int)TotFeeCols.Cost].Value = costValue;
                this.FpTotFee_Sheet1.Cells[this.FpTotFee_Sheet1.RowCount - 1, (int)TotFeeCols.Cost].CellType = nCost;
                this.FpTotFee_Sheet1.Cells[this.FpTotFee_Sheet1.RowCount - 1, (int)TotFeeCols.LeftQty].Value = feeItem.Item.Qty;
                this.FpTotFee_Sheet1.Cells[this.FpTotFee_Sheet1.RowCount - 1, (int)TotFeeCols.CostQty].Value = 0;
                this.FpTotFee_Sheet1.Rows[this.FpTotFee_Sheet1.RowCount - 1].Height = this.FpTotFee_Sheet1.Rows[this.FpTotFee_Sheet1.RowCount - 1].GetPreferredHeight();
            }
        }

        /// <summary>
        /// 检索患者预交金信息
        /// </summary>
        /// <returns>1成功 －1失败</returns>
        protected virtual int SetFPPrepay()
        {

            ArrayList al = new ArrayList();

            if (patientInfo == null)
            {
                this.FpPrepay_Sheet1.RowCount = 0;
                return -1;
            }

            al = this.inpatientFeeMgr.QueryPrepaysBalanced(patientInfo.ID);
            if (al == null)
            {
                MessageBox.Show("获取预交金失败：" + this.inpatientFeeMgr.Err);
                return -1;
            }
            //检索是否有转入预交金--出院结算处理
            decimal ChangePrepay = this.inpatientFeeMgr.GetTotChangePrepayCost(patientInfo.ID);
            if (ChangePrepay < 0)
            {
                MessageBox.Show("检索转入预交金出错!" + this.inpatientFeeMgr.Err);
                return -1;
            }
            if (ChangePrepay > 0)
            {
                FS.HISFC.Models.Fee.Inpatient.Prepay prepay = new FS.HISFC.Models.Fee.Inpatient.Prepay();
                prepay.FT.PrepayCost = ChangePrepay;
                prepay.PayType.Name = "转入预交金";
                al.Add(prepay);
            }

            this.FpPrepay_Sheet1.RowCount = 0;
            for (int i = 0; i < al.Count; i++)
            {
                FS.HISFC.Models.Fee.Inpatient.Prepay prepay = (FS.HISFC.Models.Fee.Inpatient.Prepay)al[i];
                this.FpPrepay_Sheet1.RowCount++;
                this.FpPrepay_Sheet1.Cells[i, (int)PrepayCols.Check].Value = false;
                this.FpPrepay_Sheet1.Cells[i, (int)PrepayCols.Cost].Value = prepay.FT.PrepayCost;
                this.FpPrepay_Sheet1.Cells[i, (int)PrepayCols.Payway].Value = prepay.PayType.Name;
                this.FpPrepay_Sheet1.Cells[i, (int)PrepayCols.Date].Value = prepay.PrepayOper.OperTime;
                this.FpPrepay_Sheet1.Rows[i].Tag = prepay;
            }
            return 1;
        }

        /// <summary>
        /// 初始化套餐选择项
        /// 
        /// </summary>
        private void SetPackageInfo()
        {
            this.FpPackage_Sheet1.RowCount = 0;
            this.hsPackageFeeItem.Clear();
            ArrayList packageList = this.packageMgr.QueryByCardNO(this.patientInfo.PID.CardNO, "1", "0");
            if (packageList == null)
                return;

            for (int i = 0; i < packageList.Count; i++)
            {
                FS.HISFC.Models.MedicalPackage.Fee.Package FeePackage = (FS.HISFC.Models.MedicalPackage.Fee.Package)packageList[i];
                if (FeePackage.Cost_Flag == "1")
                {
                    continue;
                }

                FeePackage.PackageInfo = this.packageBaseMgr.QueryPackageByID(FeePackage.PackageInfo.ID);

                //套餐包分开显示 {97fbd080-edb9-f31f-41fd-33f7837851ba} 
                if (!IsSelectedAll && FeePackage.PackageInfo.UserType != selectedrange)
                {
                    continue;
                }

                //{659B3053-64A3-4b5d-947E-12C6ECBE9A55}
                this.FpPackage_Sheet1.RowCount++;
                this.FpPackage_Sheet1.Cells[this.FpPackage_Sheet1.RowCount - 1, (int)PackageCols.Check].Value = false;
                this.FpPackage_Sheet1.Cells[this.FpPackage_Sheet1.RowCount - 1, (int)PackageCols.PackageName].Value = FeePackage.PackageInfo.Name;
                this.FpPackage_Sheet1.Cells[this.FpPackage_Sheet1.RowCount - 1, (int)PackageCols.TotCost].Value = FeePackage.Package_Cost;
                this.FpPackage_Sheet1.Cells[this.FpPackage_Sheet1.RowCount - 1, (int)PackageCols.RealCost].Value = FeePackage.Real_Cost;
                this.FpPackage_Sheet1.Cells[this.FpPackage_Sheet1.RowCount - 1, (int)PackageCols.GiftCost].Value = FeePackage.Gift_cost;
                this.FpPackage_Sheet1.Cells[this.FpPackage_Sheet1.RowCount - 1, (int)PackageCols.EcoCost].Value = FeePackage.Etc_cost;
                this.FpPackage_Sheet1.Cells[this.FpPackage_Sheet1.RowCount - 1, (int)PackageCols.InvoiceNo].Value = FeePackage.InvoiceNO;//{795AA18A-0093-492b-AAB9-DE654606A309}
                this.FpPackage_Sheet1.Rows[this.FpPackage_Sheet1.RowCount - 1].Tag = FeePackage;
            }
        }

        /// <summary>
        /// 将套餐转化为FeeItemList
        /// </summary>
        /// <param name="FeePackage"></param>
        private void ConvertPackageInFeeItem(FS.HISFC.Models.MedicalPackage.Fee.Package FeePackage)
        {
            if (FeePackage == null || string.IsNullOrEmpty(FeePackage.ID))
            {
                this.hsPackageFeeItem.Clear();
                return;
            }

            //查询状态为有效的数据
            ArrayList FeeDetail = this.packageDetailMgr.QueryDetailByClinicNO(FeePackage.ID, "0");

            foreach (FS.HISFC.Models.MedicalPackage.Fee.PackageDetail detail in FeeDetail)
            {
                decimal tmpQty = 0.0m;
                if (detail.Item.ID[0] == 'F')
                {
                    tmpQty = detail.Item.Qty;
                    detail.Item = this.undrugMgr.GetUndrugByCode(detail.Item.ID);
                    detail.Item.Qty = tmpQty;
                    if (((FS.HISFC.Models.Fee.Item.Undrug)detail.Item).UnitFlag == "1") //复合项目需要继续拆分
                    {
                        ArrayList al = this.undrugPackageMgr.QueryUndrugPackagesBypackageCode(detail.Item.ID);
                        if (al == null)
                        {
                            continue;
                        }
                        else
                        {
                            foreach (FS.HISFC.Models.Fee.Item.UndrugComb undrugComb in al)
                            {
                                //复合项目ID+项目ID
                                string itemKey = detail.Item.ID + undrugComb.ID;
                                FS.HISFC.Models.Base.Item item = null;
                                if (this.hsPackageFeeItem.ContainsKey(itemKey))
                                {
                                    FeeItemList fee = this.hsPackageFeeItem[itemKey] as FeeItemList;
                                    item = fee.Item;
                                    item.Qty += detail.Item.Qty * undrugComb.Qty;
                                }
                                else
                                {
                                    FeeItemList fee = new FeeItemList();
                                    fee.ID = itemKey;

                                    item = this.undrugMgr.GetUndrugByCode(undrugComb.ID);
                                    item.Qty = detail.Item.Qty * undrugComb.Qty;
                                    fee.Item = item;
                                    if (fee.UndrugComb == null)
                                    {
                                        fee.UndrugComb = new FS.HISFC.Models.Fee.Item.UndrugComb();
                                    }
                                    fee.UndrugComb.ID = detail.Item.ID;
                                    fee.UndrugComb.Name = detail.Item.Name;
                                    this.hsPackageFeeItem.Add(itemKey, fee);
                                }
                            }
                        }
                    }
                    else  //普通项目
                    {

                        string itemKey = detail.Item.ID;
                        FS.HISFC.Models.Base.Item item = null;
                        if (this.hsPackageFeeItem.ContainsKey(itemKey))
                        {
                            FeeItemList fee = this.hsPackageFeeItem[itemKey] as FeeItemList;
                            item = fee.Item;
                            item.Qty += detail.Item.Qty;
                        }
                        else
                        {
                            FeeItemList fee = new FeeItemList();
                            fee.ID = itemKey;

                            item = this.undrugMgr.GetUndrugByCode(detail.Item.ID);
                            item.Qty = detail.Item.Qty;
                            fee.Item = item;
                            this.hsPackageFeeItem.Add(itemKey, fee);
                        }
                    }
                }
                else
                {
                    tmpQty = detail.Item.Qty;
                    detail.Item = this.pharmacyMgr.GetItem(detail.Item.ID);
                }
            }
        }

        /// <summary>
        /// 设置金额显示
        /// </summary>
        /// <param name="EcoCost">优惠金额</param>
        private void SetCostInfo(decimal EcoCost)
        {
            this.BalanceTot = 0.0m;
            this.BalanceReal = 0.0m;
            this.BalanceDonate = 0.0m;
            this.BalanceEco = 0.0m;
            this.PrepayCost = 0.0m;
            this.ShouldPay = 0.0m;
            this.ShouldRtn = 0.0m;

            //正常结算时统计总金额和实收，从总费用明细列表统计
            if (this.balanceType == BalanceType.Normal)
            {
                foreach (Row row in this.FpTotFee_Sheet1.Rows)
                {
                    FeeItemList feeItem = row.Tag as FeeItemList;
                    this.BalanceTot += feeItem.FT.TotCost;
                }

                if (EcoCost > this.BalanceTot)
                {
                    this.BalanceEco = EcoCost = this.BalanceTot;
                }

                this.BalanceEco = EcoCost;
                this.BalanceReal = this.BalanceTot - EcoCost;
                this.BalanceDonate = 0.0m;


                this.PrepayCost = this.GetPrepay();
                this.ShouldPay = BalanceReal - PrepayCost >= 0 ? BalanceReal - PrepayCost : 0.0m;
                this.ShouldRtn = PrepayCost - BalanceReal >= 0 ? PrepayCost - BalanceReal : 0.0m;
            }
            //部分结算统计已选择费用列表
            else if (this.balanceType == BalanceType.Part)
            {
                foreach (Row row in this.FpSelectedFee_Sheet1.Rows)
                {
                    FeeItemList fee = row.Tag as FeeItemList;
                    this.BalanceTot += fee.FT.TotCost;
                }

                if (EcoCost > this.BalanceTot)
                {
                    this.BalanceEco = EcoCost = this.BalanceTot;
                }

                this.BalanceEco = EcoCost;
                this.BalanceReal = this.BalanceTot - EcoCost;
                this.BalanceDonate = 0.0m;

                this.PrepayCost = this.GetPrepay();
                this.ShouldPay = BalanceReal - PrepayCost >= 0 ? BalanceReal - PrepayCost : 0.0m;
                this.ShouldRtn = PrepayCost - BalanceReal >= 0 ? PrepayCost - BalanceReal : 0.0m;
            }
            //套餐结算
            else
            {
                foreach (Row row in this.FpSelectedFee_Sheet1.Rows)
                {
                    FeeItemList fee = row.Tag as FeeItemList;
                    this.BalanceTot += fee.FT.TotCost;
                }

                if (this.GetPackagePay(ref this.BalanceReal, ref this.BalanceDonate) < 0)
                {
                    MessageBox.Show("获取套餐实收和赠送金额失败！");
                    this.ClearFee();
                    return;
                }

                if (this.SelectedPackage.Count > 0)
                {
                    this.BalanceEco = this.BalanceTot - this.BalanceReal - this.BalanceDonate;
                }
                else
                {
                    this.BalanceEco = 0.0m;
                }

                this.PrepayCost = 0.00m;
                this.ShouldPay = 0.00m;
                this.ShouldRtn = 0.00m;
            }

            this.tbBalanceTot.Text = this.BalanceTot.ToString();
            this.tbBalanceReal.Text = this.BalanceReal.ToString();
            this.tbBalanceDonate.Text = this.BalanceDonate.ToString();
            this.tbBalanceEco.Text = this.BalanceEco.ToString();
            this.tbPrepayCost.Text = this.PrepayCost.ToString();
            this.tbShouldPay.Text = this.ShouldPay.ToString();
            this.tbShouldRtn.Text = this.ShouldRtn.ToString();
        }

        /// <summary>
        /// 获取预交金缴纳金额
        /// </summary>
        /// <returns></returns>
        private decimal GetPrepay()
        {
            decimal prepay = 0.0m;
            FS.HISFC.Models.Fee.Inpatient.Prepay prepayItem;
            for (int i = 0; i < this.FpPrepay_Sheet1.RowCount; i++)
            {
                if ((bool)this.FpPrepay_Sheet1.Cells[i, (int)PrepayCols.Check].Value == true)
                {
                    prepayItem = (FS.HISFC.Models.Fee.Inpatient.Prepay)this.FpPrepay_Sheet1.Rows[i].Tag;
                    prepay += prepayItem.FT.PrepayCost;
                }
            }
            return prepay;
        }

        /// <summary>
        /// 获取预交金明细
        /// </summary>
        /// <returns></returns>
        private List<FS.HISFC.Models.Fee.Inpatient.Prepay> GetPrepayItem()
        {
            List<FS.HISFC.Models.Fee.Inpatient.Prepay> listPrepay = new List<FS.HISFC.Models.Fee.Inpatient.Prepay>();
            FS.HISFC.Models.Fee.Inpatient.Prepay prepay;
            for (int i = 0; i < this.FpPrepay_Sheet1.RowCount; i++)
            {
                if ((bool)this.FpPrepay_Sheet1.Cells[i, (int)PrepayCols.Check].Value == true)
                {
                    prepay = (FS.HISFC.Models.Fee.Inpatient.Prepay)this.FpPrepay_Sheet1.Rows[i].Tag;
                    prepay.BalanceState = "1";
                    listPrepay.Add(prepay);
                }
            }

            return listPrepay;
        }

        /// <summary>
        /// 获取已勾选的套餐
        /// </summary>
        /// <returns></returns>
        private ArrayList GetPackageInfo()
        {
            ArrayList listPackage = new ArrayList();
            FS.HISFC.Models.MedicalPackage.Fee.Package package = null;
            for (int i = 0; i < this.FpPackage_Sheet1.RowCount; i++)
            {
                if ((bool)this.FpPackage_Sheet1.Cells[i, (int)PackageCols.Check].Value == true)
                {
                    package = (FS.HISFC.Models.MedicalPackage.Fee.Package)this.FpPackage_Sheet1.Rows[i].Tag;
                    listPackage.Add(package);
                }
            }
            return listPackage;
        }

        /// <summary>
        /// 获取实收金额赠送金额
        /// </summary>
        /// <param name="RealCost"></param>
        /// <param name="DonateCost"></param>
        /// <returns></returns>
        private int GetPackagePay(ref decimal RealCost, ref decimal DonateCost)
        {
            RealCost = 0;
            DonateCost = 0;
            ArrayList listPackage = this.GetPackageInfo();
            if (listPackage == null)
            {
                return -1;
            }

            foreach (FS.HISFC.Models.MedicalPackage.Fee.Package package in listPackage)
            {
                RealCost += package.Real_Cost;
                DonateCost += package.Gift_cost;
            }

            return 1;
        }

        /// <summary>
        /// 获取下一打印发票号
        /// </summary>
        private void GetNextInvoiceNO()
        {
            this.lbInvoice.Text = "";
            FS.HISFC.Models.Base.Employee oper = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
            string invoiceNO = "";
            string realInvoiceNO = "";
            string errText = "";

            this.feeIntegrate.GetInvoiceNO(oper, "I", ref invoiceNO, ref realInvoiceNO, ref errText);

            if (string.IsNullOrEmpty(invoiceNO))
            {
                //未领取发票则弹出窗口输入
                FS.HISFC.Components.Common.Forms.frmUpdateInvoice frm = new FS.HISFC.Components.Common.Forms.frmUpdateInvoice();
                frm.InvoiceType = "I";
                frm.ShowDialog(this);

                int iReturn = this.feeIntegrate.GetInvoiceNO(oper, "I", ref invoiceNO, ref realInvoiceNO, ref errText);
                if (iReturn == -1)
                {
                    CommonController.CreateInstance().MessageBox(errText, MessageBoxIcon.Warning);
                    return;
                }
            }

            lbInvoice.Text = "电脑号： " + invoiceNO + ", 印刷号：" + realInvoiceNO;
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnSave(object sender, object neuObject)
        {
            if (this.Save() > 0)
            {
                this.Clear();
                this.patientInfo = null;
                this.ucQueryInfo.Focus();
                this.GetNextInvoiceNO();
            }
            return base.OnSave(sender, neuObject);
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        public int Save()
        {
            this.FpTotFee.StopCellEditing();
            if (this.patientInfo == null || string.IsNullOrEmpty(this.patientInfo.ID))
            {
                return 1;
            }
            string errText = "";
            this.patientInfo = this.radtMgr.GetPatientInfo(this.patientInfo.ID);//{CF1CA262-3582-46ac-8D3C-DFA75C974C70}
            if (this.ValidPatient(this.patientInfo, ref errText) < 0)
            {
                CommonController.CreateInstance().MessageBox(errText, MessageBoxIcon.Warning);
                return -1;
            }
            if (!this.ValidCost(ref errText))
            {
                CommonController.CreateInstance().MessageBox(errText, MessageBoxIcon.Warning);
                return -1;
            }

            //{FAE56BB8-F958-411f-9663-CC359D6D494B}
            decimal shouldPayCost = this.ShouldPay;//应收金额大于零
            decimal returnCost = this.ShouldRtn; //返还金额
            decimal ecoCost = this.BalanceEco; //优惠金额

            if (this.balanceType != BalanceType.Package && this.IsLevelModuleInUse)
            {
                string accountInfoTip = "当前患者为【{0}】用户,会员折扣为{1}折,是否执行会员折扣？";

                //{5B7CD01E-2DDB-499d-9F49-DA8A2F7E0AAC}
                if (this.levelDiscount.Equals(1))
                {
                    accountInfoTip = "当前患者为【{0}】用户,无会员折扣";
                    accountInfoTip = string.Format(accountInfoTip, this.levelName);
                }
                else
                {
                    accountInfoTip = string.Format(accountInfoTip, this.levelName, (this.levelDiscount * 10).ToString().Trim());
                }

                //{5B7CD01E-2DDB-499d-9F49-DA8A2F7E0AAC}
                if (!this.levelDiscount.Equals(1) && MessageBox.Show(accountInfoTip, "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    //处理打折事务
                    //shouldPayCost = shouldPayCost * this.levelDiscount;
                    //returnCost = this.PrepayCost - shouldPayCost;
                    //{592F363C-D236-41d8-9CE8-C1782DAAC1A1}
                    shouldPayCost = (Math.Ceiling(BalanceReal * 100 * this.levelDiscount)) / 100 - PrepayCost >= 0 ? (Math.Ceiling(BalanceReal * 100 * this.levelDiscount)) / 100 - PrepayCost : 0.0m;
                    returnCost = PrepayCost - (Math.Ceiling(BalanceReal * 100 * this.levelDiscount)) / 100 >= 0 ? PrepayCost - (Math.Ceiling(BalanceReal * 100 * this.levelDiscount)) / 100 : 0.0m;
                    ecoCost = this.BalanceTot - shouldPayCost - PrepayCost + returnCost;


                }
            }

            this.frmBalancePay.Clear();
            List<FS.HISFC.Models.Fee.Inpatient.BalancePay> listBalancePay = new List<FS.HISFC.Models.Fee.Inpatient.BalancePay>();

            if (shouldPayCost > 0)
            {
                frmBalancePay.IsCouponModuleInUse = this.IsCouponModuleInUse;
                frmBalancePay.ReturnPay = 0;
                frmBalancePay.ArrearBalance = 0;
                frmBalancePay.ShouldPay = shouldPayCost;
                frmBalancePay.PatientInfo = this.patientInfo;

                frmBalancePay.BalancePayCost = 0;
                frmBalancePay.BalanceMZCost = 0;
                frmBalancePay.ShowDialog(this);
                if (!frmBalancePay.IsOK)
                {
                    return -1;
                }
                if (frmBalancePay.ListBalancePay.Count > 0)
                {
                    listBalancePay.AddRange(frmBalancePay.ListBalancePay);
                }////{B7E5CA30-EC53-4fea-BB60-55B8D8DE3CAB} 判断应收其他是否填写了备注信息
                else
                {
                    return -1;
                }
            }

            //套餐结算将套餐自动插入支付方式
            if (this.balanceType == BalanceType.Package)
            {
                if (this.SelectedPackage == null || this.SelectedPackage.Count == 0)
                {
                    MessageBox.Show("请选择套餐并进行匹配！");
                    return -1;
                }
                decimal packageReal = 0.0m;
                decimal packageGift = 0.0m;
                if (this.GetPackagePay(ref packageReal, ref packageGift) < 0)
                {
                    CommonController.Instance.MessageBox("获取套餐实收金额与赠送金额错误！", MessageBoxIcon.Warning);
                    return -1;
                }

                //先写死吧，后期再改
                BalancePay balancePayReal = new BalancePay();
                balancePayReal.PayType.ID = "PR";
                balancePayReal.PayType.Name = "套餐实收";
                balancePayReal.RetrunOrSupplyFlag = "1";
                balancePayReal.TransKind.ID = "0";
                balancePayReal.FT.TotCost = packageReal;
                balancePayReal.TransType = FS.HISFC.Models.Base.TransTypes.Positive;
                balancePayReal.Qty = 1;

                BalancePay balancePayGift = new BalancePay();
                balancePayGift.PayType.ID = "PD";
                balancePayGift.PayType.Name = "套餐赠送";
                balancePayGift.RetrunOrSupplyFlag = "1";
                balancePayGift.TransKind.ID = "0";
                balancePayGift.FT.TotCost = packageGift;
                balancePayGift.TransType = FS.HISFC.Models.Base.TransTypes.Positive;
                balancePayGift.Qty = 1;

                BalancePay balancePayEco = new BalancePay();
                balancePayEco.PayType.ID = "PY";
                balancePayEco.PayType.Name = "套餐优惠";
                balancePayEco.RetrunOrSupplyFlag = "1";
                balancePayEco.TransKind.ID = "0";
                balancePayEco.FT.TotCost = this.BalanceEco;
                balancePayEco.TransType = FS.HISFC.Models.Base.TransTypes.Positive;
                balancePayEco.Qty = 1;

                if (packageReal > 0)
                {
                    listBalancePay.Add(balancePayReal);
                }

                if (packageGift > 0)
                {
                    listBalancePay.Add(balancePayGift);
                }

                if (packageReal < 0 || packageGift < 0)
                {
                    CommonController.Instance.MessageBox("套餐金额小于0！", MessageBoxIcon.Warning);
                    return -1;
                }

                if (this.BalanceEco >= 0)
                {
                    listBalancePay.Add(balancePayEco);
                }
                else
                {
                    balancePayEco.PayType.ID = "PYZZ";
                    balancePayEco.PayType.Name = "套餐宰账";
                    balancePayEco.RetrunOrSupplyFlag = "2";
                    balancePayEco.TransKind.ID = "1";
                    balancePayEco.FT.TotCost = -this.BalanceEco;
                    listBalancePay.Add(balancePayEco);
                }
            }
            else
            {

                BalancePay balancePayEco = new BalancePay();
                balancePayEco.PayType.ID = "RC";
                balancePayEco.PayType.Name = "优惠";
                balancePayEco.RetrunOrSupplyFlag = "1";
                balancePayEco.TransKind.ID = "1";
                balancePayEco.FT.TotCost = ecoCost;
                balancePayEco.TransType = FS.HISFC.Models.Base.TransTypes.Positive;
                balancePayEco.Qty = 1;
                if (balancePayEco.FT.TotCost > 0)
                {
                    listBalancePay.Add(balancePayEco);
                }
            }

            #region 结算费用

            List<FS.HISFC.Models.Fee.Inpatient.FeeItemList> feeItemList = new List<FeeItemList>();

            if (this.balanceType == BalanceType.Package || this.balanceType == BalanceType.Part)
            {
                foreach (string key in this.hsSelectedFeeItem.Keys)
                {
                    FeeItemList fee = this.hsSelectedFeeItem[key] as FeeItemList;
                    feeItemList.Add(fee);
                }
            }
            else
            {
                foreach (string key in this.hsFeeItem.Keys)
                {
                    FeeItemList fee = this.hsFeeItem[key] as FeeItemList;
                    feeItemList.Add(fee);
                }
            }

            //{7818A172-B6C1-429f-A92D-94DBD99A150E}
            //if (feeItemList.Count == 0)
            //{
            //    CommonController.Instance.MessageBox("请选择需要结算的费用", MessageBoxIcon.Warning);
            //    return -1;
            //}

            #endregion

            #region 预交金及支付方式

            List<FS.HISFC.Models.Fee.Inpatient.Prepay> listPrepay = new List<FS.HISFC.Models.Fee.Inpatient.Prepay>();
            FS.HISFC.Models.Fee.Inpatient.Prepay prepay;
            for (int i = 0; i < this.FpPrepay_Sheet1.RowCount; i++)
            {
                if ((bool)this.FpPrepay_Sheet1.Cells[i, (int)ColumnPrepay.Check].Value == true)
                {
                    prepay = (FS.HISFC.Models.Fee.Inpatient.Prepay)this.FpPrepay_Sheet1.Rows[i].Tag;
                    prepay.BalanceState = "1";
                    listPrepay.Add(prepay);
                }
            }

            string payTrace = decimal.Parse(this.tbShouldPay.Text) > 0 ? "1" : "2";
            //结算预交金
            if (listPrepay.Count > 0)
            {
                //{559B2319-F4C0-49a9-9D4F-1DD6F152DE23}
                //将每一个s预付金额插入付款表，原来的付款金额为一个合计的现金，现在按照支付类型生成多个支付记录
                foreach (FS.HISFC.Models.Fee.Inpatient.Prepay prepayDetail in listPrepay)
                {
                    FS.HISFC.Models.Fee.Inpatient.BalancePay payDetail = new FS.HISFC.Models.Fee.Inpatient.BalancePay();
                    payDetail.TransKind.ID = "0";
                    payDetail.TransType = FS.HISFC.Models.Base.TransTypes.Positive;
                    payDetail.PayType.ID = prepayDetail.PayType.ID;
                    payDetail.FT.TotCost = prepayDetail.FT.PrepayCost;
                    payDetail.Qty = 1;// listPrepay.Count;
                    payDetail.RetrunOrSupplyFlag = payTrace;
                    listBalancePay.Add(payDetail);
                }

                /*
                 * 原来的付款金额为一个合计的现金，现在按照支付类型生成多个支付记录
                FS.HISFC.Models.Fee.Inpatient.BalancePay pay = new FS.HISFC.Models.Fee.Inpatient.BalancePay();
                pay.TransKind.ID = "0";
                pay.TransType = FS.HISFC.Models.Base.TransTypes.Positive;
                pay.PayType.ID = "CA";
                pay.FT.TotCost = FS.FrameWork.Function.NConvert.ToDecimal(this.tbPrepayCost.Text);
                pay.Qty = listPrepay.Count;
                pay.RetrunOrSupplyFlag = payTrace;
                listBalancePay.Add(pay);
                 * */
            }

            //返还金额
            //decimal returnCost = decimal.Parse(this.tbShouldRtn.Text);//应收金额大于零
            if (returnCost > 0)
            {
                //套餐的话用宰账
                if (balanceType == BalanceType.Package)
                {
                    FS.HISFC.Models.Fee.Inpatient.BalancePay pay = new FS.HISFC.Models.Fee.Inpatient.BalancePay();
                    pay.TransKind.ID = "1";
                    pay.TransType = FS.HISFC.Models.Base.TransTypes.Positive;
                    pay.PayType.ID = "ZZ";
                    pay.PayType.Name = "宰账";
                    pay.FT.TotCost = returnCost;
                    pay.RetrunOrSupplyFlag = payTrace;
                    listBalancePay.Add(pay);
                }
                else
                {
                    frmBalancePay.Clear();
                    frmBalancePay.PatientInfo = this.patientInfo;
                    frmBalancePay.ArrearBalance = 0;
                    frmBalancePay.ShouldPay = 0;
                    frmBalancePay.ReturnPay = returnCost;
                    frmBalancePay.BalancePayCost = 0;
                    frmBalancePay.BalanceMZCost = 0;
                    frmBalancePay.ShowDialog(this);
                    if (!frmBalancePay.IsOK)
                    {
                        return -1;
                    }

                    if (frmBalancePay.ListBalancePay.Count > 0)
                    {
                        listBalancePay.AddRange(frmBalancePay.ListBalancePay);
                    }
                    else
                    {
                        CommonController.Instance.MessageBox("请选择退款的支付方式！", MessageBoxIcon.Warning);
                        return -1;
                    }
                }
            }

            #endregion

            List<FS.HISFC.Models.Fee.Inpatient.Balance> listBalance = new List<FS.HISFC.Models.Fee.Inpatient.Balance>();
            FS.SOC.HISFC.InpatientFee.BizProcess.Balance balanceMgr = new FS.SOC.HISFC.InpatientFee.BizProcess.Balance();


            if (this.balanceType == BalanceType.Normal)
            {

                if (FpPrepay_Sheet1.Rows.Count > 0)//{CE52F42E-C6BB-43AA-9CC5-7B7165F99823} 添加押金提醒功能和限制有押金未使用则不能结算
                {
                    for (int i = 0; i < this.FpPrepay_Sheet1.RowCount; i++)
                    {
                        if ((bool)this.FpPrepay_Sheet1.Cells[i, (int)ColumnPrepay.Check].Value == false)
                        {
                            MessageBox.Show("该患者住院押金未使用！请使用或退还押金再结算！");
                            return -1;
                        }
                    }
                }
                ArrayList al = this.inpatientFeeMgr.QueryFeeInfosAndChangeCostGroupByMinFeeByInpatientNO(patientInfo.ID, "0");
                if (al == null)
                {
                    errText = inpatientFeeMgr.Err;
                    return -1;
                }

                if (Function.SplitFeeItem(patientInfo, al, ref errText))
                {
                    al = this.inpatientFeeMgr.QueryFeeInfosAndChangeCostGroupByMinFeeByInpatientNO(patientInfo.ID, "0");
                }
                else
                {
                    MessageBox.Show(this, errText, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return -1;
                }

                List<FS.HISFC.Models.Fee.Inpatient.FeeInfo> listFeeInfo = new List<FS.HISFC.Models.Fee.Inpatient.FeeInfo>();

                foreach (FS.HISFC.Models.Fee.Inpatient.FeeInfo feeInfo in al)
                {
                    feeInfo.FT.BalancedCost = feeInfo.FT.TotCost;
                    listFeeInfo.Add(feeInfo);
                }

                //出院结算
                //if (balanceMgr.OutBalance(this.patientInfo, FS.HISFC.Models.Base.EBlanceType.Out, listFeeInfo, listPrepay, listBalancePay, false, 0, ref listBalance) < 0)
                //{
                //    CommonController.CreateInstance().MessageBox(balanceMgr.Err, MessageBoxIcon.Warning);
                //    return -1;
                //}

                //出院结算
                if (balanceMgr.NewBalance(this.patientInfo, FS.HISFC.Models.Base.EBlanceType.Out, false, feeItemList, listPrepay, listBalancePay, 0.0m, new ArrayList(), ref listBalance) < 0)
                {
                    CommonController.CreateInstance().MessageBox(balanceMgr.Err, MessageBoxIcon.Warning);
                    return -1;
                }
            }
            else if (this.balanceType == BalanceType.Part)
            {
                bool totBalance = true;
                foreach (Row row in this.FpTotFee_Sheet1.Rows)
                {
                    if (Decimal.Parse(this.FpTotFee_Sheet1.Cells[row.Index, (int)TotFeeCols.LeftQty].Text) > 0)
                    {
                        totBalance = false;
                        break;
                    }
                }
                if (totBalance)//{CE52F42E-C6BB-43AA-9CC5-7B7165F99823} 添加押金提醒功能和限制有押金未使用则不能结算
                {
                    if (FpPrepay_Sheet1.Rows.Count > 0)
                    {
                        for (int i = 0; i < this.FpPrepay_Sheet1.RowCount; i++)
                        {
                            if ((bool)this.FpPrepay_Sheet1.Cells[i, (int)ColumnPrepay.Check].Value == false)
                            {
                                MessageBox.Show("该患者住院押金未使用！请使用或退还押金再结算！");
                                return -1;
                            }
                        }
                    }
                }
                else
                {
                    if (FpPrepay_Sheet1.Rows.Count > 0)
                    {
                        for (int i = 0; i < this.FpPrepay_Sheet1.RowCount; i++)
                        {
                            if ((bool)this.FpPrepay_Sheet1.Cells[i, (int)ColumnPrepay.Check].Value == false)
                            {
                                DialogResult dr = MessageBox.Show("该患者还有住院押金或者请先退了押金再结算！请选择是否继续操作", "提醒", MessageBoxButtons.YesNo);
                                if (dr == DialogResult.No)
                                {
                                    return -1;
                                }
                            }
                        }
                    }
                }

                //部分结算
                if (balanceMgr.NewBalance(this.patientInfo, totBalance ? FS.HISFC.Models.Base.EBlanceType.Out : FS.HISFC.Models.Base.EBlanceType.Mid, false, feeItemList, listPrepay, listBalancePay, 0.0m, new ArrayList(), ref listBalance) < 0)
                {
                    CommonController.CreateInstance().MessageBox(balanceMgr.Err, MessageBoxIcon.Warning);
                    return -1;
                }
            }
            else if (this.balanceType == BalanceType.Package)
            {
                bool totBalance = true;
                foreach (Row row in this.FpTotFee_Sheet1.Rows)
                {
                    if (Decimal.Parse(this.FpTotFee_Sheet1.Cells[row.Index, (int)TotFeeCols.LeftQty].Text) > 0)
                    {
                        totBalance = false;
                        break;
                    }
                }
                ArrayList al = new ArrayList();//{CE52F42E-C6BB-43AA-9CC5-7B7165F99823} 添加押金提醒功能和限制有押金未使用则不能结算
                al = this.inpatientFeeMgr.QueryPrepaysBalanced(patientInfo.ID);
                if (al == null)
                {
                    MessageBox.Show("获取预交金失败：" + this.inpatientFeeMgr.Err);
                    return -1;
                }
                if (al.Count > 0)
                {
                    if (totBalance)
                    {
                        MessageBox.Show("该患者还有住院押金未使用，请先退了押金再结算！");
                        return -1;
                    }
                    else
                    {
                        DialogResult dr = MessageBox.Show("该患者还有住院押金或者请先退了押金再结算！请选择是否继续操作", "提醒", MessageBoxButtons.YesNo);
                        if (dr == DialogResult.No)
                        {
                            return -1;
                        }
                    }
                }
                //套餐结算
                if (balanceMgr.NewBalance(this.patientInfo, totBalance ? FS.HISFC.Models.Base.EBlanceType.Out : FS.HISFC.Models.Base.EBlanceType.Mid, true, feeItemList, listPrepay, listBalancePay, 0.0m, this.SelectedPackage, ref listBalance) < 0)
                {
                    CommonController.CreateInstance().MessageBox(balanceMgr.Err, MessageBoxIcon.Warning);
                    return -1;
                }
            }

            //打印发票
            if (Function.PrintInvoice(this.patientInfo, listBalance, ref errText) < 0)
            {
                CommonController.CreateInstance().MessageBox(errText, MessageBoxIcon.Warning);
                return -1;
            }
            //{C3F605B6-9281-4068-98BB-393ADE0DF06C}提示是否查看住院费用根据单据号汇总
            // 是否打印清单
            if (MessageBox.Show("是否打印发票清单汇总？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                foreach (FS.HISFC.Models.Fee.Inpatient.Balance balance in listBalance)
                {
                    FS.HISFC.Components.Common.Controls.ucBillPrint ucBill = new FS.HISFC.Components.Common.Controls.ucBillPrint();
                    ucBill.ShowIBill = 1;
                    ucBill.lblInvioceNo.Text = balance.Invoice.ID;
                    if (ucBill.QueryByInvoiceNoIn())
                    {

                        ucBill.IBillPrint.PrintPreview();
                    }
                    //ucBill.Print(
                    //balance.PrintedInvoiceNO 
                }
                //删除
            }
            return 1;
        }

        /// <summary>
        /// 套餐预结算  //{C4231074-D350-4df9-AF7C-C37124B44B80}
        /// </summary>
        /// <returns></returns>
        public int PreBalance()
        {
            //插入预结算头表
            FS.HISFC.Models.Base.Employee oper = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
            PreBalance prehead = new PreBalance();
            prehead.PREBLANCENO = prebalancelogic.GetNewPreBalanceNo();
            prehead.INPATIENTNO = this.patientInfo.ID;
            prehead.NAME = this.patientInfo.Name;
            prehead.BALANCEPRICE = FS.FrameWork.Function.NConvert.ToDecimal(tbBalanceTot.Text);
            prehead.BALANCEDATE = (DateTime.Now);
            prehead.OPERCODE = oper.ID;
            prehead.OPERNAME = oper.Name;
            prehead.PACKAGEIDS = GetPackageids();
            prehead.ISVALID = "1";
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            if (prebalancelogic.InsertPreBalance(prehead) < 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("主表插入失败");
                return -1;
            }
            else
            {
                //插入预结算明细表
                for (int i = 0; i < FpSelectedFee_Sheet1.Rows.Count; i++)
                {
                    PreBalanceList detail = new PreBalanceList();
                    FeeItemList updateFee = FpSelectedFee_Sheet1.Rows[i].Tag as FeeItemList;
                    detail.PREBLANCENO = prehead.PREBLANCENO;
                    detail.SEQUENCE_NO = prebalancelogic.GetNewDetailSequenceNo();
                    detail.ITEM_CODE = updateFee.Item.ID;
                    detail.ITEM_NAME = updateFee.Item.Name;
                    detail.FEE_CODE = updateFee.Item.MinFee.ID;
                    detail.UNIT_PRICE = updateFee.Item.Price;
                    detail.QTY = updateFee.Item.Qty;

                    detail.PACKAGE_CODE = updateFee.UndrugComb.ID;
                    detail.PACKAGE_NAME = updateFee.UndrugComb.Name;
                    detail.Spec = updateFee.Item.Specs;
                    detail.CURRENT_UNIT = updateFee.Item.PriceUnit;//单位1
                    string Unit = updateFee.Item.PriceUnit;//单位2
                    if (updateFee.Item.ItemType == EnumItemType.Drug)
                    {
                        FS.HISFC.Models.Pharmacy.Item pharmacy = updateFee.Item as FS.HISFC.Models.Pharmacy.Item;
                        Unit = pharmacy.PackUnit;
                    }
                    detail.PRICEUNIT = Unit;//计价单位

                    if (prebalancelogic.InsertPreBalanceList(detail) < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("明细插入失败");
                        return -1;
                    }
                }
                FS.FrameWork.Management.PublicTrans.Commit();
                return 1;
            }

        }

        ////
        ///获取预结算记录  //{C4231074-D350-4df9-AF7C-C37124B44B80}
        ///
        private int GetPreBalance()
        {
            FpPreBalance_Sheet1.RowCount = 0;
            ArrayList headlist = this.prebalancelogic.QueryPreBalanceByInNoAndValid(this.patientInfo.ID, "1");
            if (headlist != null)
            {
                foreach (PreBalance head in headlist)
                {

                    this.FpPreBalance_Sheet1.Rows.Add(this.FpPreBalance_Sheet1.RowCount, 1);
                    this.FpPreBalance_Sheet1.Cells[this.FpPreBalance_Sheet1.RowCount - 1, (int)PreBalanceCols.prebalanceno].Text = head.PREBLANCENO;
                    this.FpPreBalance_Sheet1.Cells[this.FpPreBalance_Sheet1.RowCount - 1, (int)PreBalanceCols.patientname].Text = head.NAME;
                    this.FpPreBalance_Sheet1.Cells[this.FpPreBalance_Sheet1.RowCount - 1, (int)PreBalanceCols.balanceprice].Text = head.BALANCEPRICE.ToString();
                    this.FpPreBalance_Sheet1.Cells[this.FpPreBalance_Sheet1.RowCount - 1, (int)PreBalanceCols.balancedate].Text = head.BALANCEDATE.ToString();
                    this.FpPreBalance_Sheet1.Cells[this.FpPreBalance_Sheet1.RowCount - 1, (int)PreBalanceCols.opername].Text = head.OPERNAME;
                    this.FpPreBalance_Sheet1.Cells[this.FpPreBalance_Sheet1.RowCount - 1, (int)PreBalanceCols.memo].Text = head.Memo;
                    this.FpPreBalance_Sheet1.Rows[this.FpPreBalance_Sheet1.RowCount - 1].Tag = head;


                }
            }
            return 1;
        }

        /// <summary>
        /// 获取勾选的套餐主键
        /// </summary>
        /// <returns></returns>
        private string GetPackageids()
        {
            List<string> packageids = new List<string>();
            for (int i = 0; i < this.FpPackage_Sheet1.Rows.Count; i++)
            {
                if (FS.FrameWork.Function.NConvert.ToBoolean(this.FpPackage_Sheet1.Cells[i, (int)PackageCols.Check].Value) == true)
                {

                    FS.HISFC.Models.MedicalPackage.Fee.Package FeePackage = this.FpPackage_Sheet1.Rows[i].Tag as FS.HISFC.Models.MedicalPackage.Fee.Package;
                    packageids.Add(FeePackage.ID);
                }
            }
            string packageidstr = string.Join(",", packageids.ToArray());
            return packageidstr;
        }


        /// <summary>
        /// 结算有效性判断
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <returns></returns>
        protected virtual int ValidPatient(FS.HISFC.Models.RADT.PatientInfo patientInfo, ref string errText)
        {
            //已经出院的返回
            if (patientInfo.PVisit.InState.ID.ToString().Equals(FS.HISFC.Models.Base.EnumInState.O.ToString()))
            {
                errText = "患者已经出院结算!";
                return -1;
            }

            //判断目前是否有婴儿未出院
            if (patientInfo.IsHasBaby)
            {
                FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParamMgr = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

                string motherPayAllFee = controlParamMgr.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.SysConst.Use_Mother_PayAllFee, false, "0");
                if (motherPayAllFee == "0")//婴儿的费用收在妈妈的身上 
                {
                    //根据流水号查询所有的婴儿信息
                    ArrayList alBabies = this.radtIntegrate.QueryBabiesByMother(patientInfo.ID);
                    if (alBabies == null)
                    {
                        errText = "查询患者的婴儿失败，" + this.radtIntegrate.Err;
                        return -1;
                    }

                    foreach (FS.HISFC.Models.RADT.PatientInfo p in alBabies)
                    {
                        FS.HISFC.Models.RADT.PatientInfo baby = this.radtIntegrate.QueryPatientInfoByInpatientNO(p.ID);
                        if (baby == null)
                        {
                            errText = "查询患者的婴儿失败，" + this.radtIntegrate.Err;
                            return -1;
                        }
                        //说明有费用未结
                        if (p.FT.TotCost > 0)
                        {
                            errText = p.Name + "还存在未结费用，请先结算婴儿的费用";
                            return -1;
                        }
                    }
                }
            }

            //结算时候是否走结算清单封帐
            if (IsCloseAccount)
            {
                if (patientInfo.IsStopAcount == false)
                {
                    errText = "患者出院结算前，请先到结算清单处进行封帐!";
                    return -1;
                }
            }

            //出院结算判断状态
            string suretyCost = this.inpatientFeeMgr.GetSurtyCost(patientInfo.ID);
            if (FS.FrameWork.Function.NConvert.ToDecimal(suretyCost) > 0)
            {
                errText = "患者有未返还的担保金:" + suretyCost + "元,请返还后再进行出院结算！";
                return -1;
            }

            if (patientInfo.PVisit.InState.ID.ToString().Equals(FS.HISFC.Models.Base.EnumInState.B.ToString()) ||
                patientInfo.PVisit.InState.ID.ToString().Equals(FS.HISFC.Models.Base.EnumInState.C.ToString()))
            {
            }
            else
            {
                errText = "患者状态不是出院登记,不能进行出院结算！";
                return -1;
            }

            //转押金
            ArrayList alForegift = this.inpatientFeeMgr.QueryForegif(patientInfo.ID);
            if (alForegift == null)
            {
                errText = this.inpatientFeeMgr.Err;
                return -1;
            }
            if (alForegift.Count > 0)
            {
                errText = "患者存在没有打印的中结转押金票据,请打印!";
                return -1;
            }

            //退费申请、退药申请、终端确认等
            FS.SOC.HISFC.InpatientFee.BizProcess.Fee feeManager = new FS.SOC.HISFC.InpatientFee.BizProcess.Fee();
            if (feeManager.IsExistApplyFee(this.patientInfo.ID))
            {
                if (CommonController.CreateInstance().MessageBox("存在未确认的退费申请，是否继续？", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return -1;
                }
            }

            return 1;
        }

        /// <summary>
        /// 验证金额是否正确
        /// </summary>
        /// <param name="errText"></param>
        /// <returns></returns>
        protected virtual bool ValidCost(ref string errText)
        {
            decimal balanceTot = 0m;
            decimal shouldPay = 0m;
            decimal RtnCost = 0m;
            RtnCost = decimal.Parse(this.tbShouldRtn.Text);
            shouldPay = decimal.Parse(this.tbShouldPay.Text);
            balanceTot = decimal.Parse(this.tbBalanceTot.Text);

            //{7818A172-B6C1-429f-A92D-94DBD99A150E}
            if (balanceTot < 0 || shouldPay < 0 || RtnCost < 0)
            {
                errText = "结算金额必须大于零!";
                return false;
            }
            return true;
        }

        #endregion

        #region 公共函数

        /// <summary>
        /// 清空界面信息
        /// </summary>
        public void Clear()
        {
            this.ClearFee();
            this.patientInfo = null;
            if (this.MedicineList != null)
            {
                this.MedicineList.Clear();
            }
            else
            {
                this.MedicineList = new ArrayList();
            }
            if (this.UndrugList != null)
            {
                this.UndrugList.Clear();
            }
            else
            {
                this.UndrugList = new ArrayList();
            }
            this.SetPatientInfo(null);
            this.feeItemList.Clear();
            this.hsFeeItem.Clear();
            this.hsSelectedFeeItem.Clear();

            this.ucQueryInfo.Focus();
        }

        private void OpenAccount()
        {
            if (this.patientInfo == null || string.IsNullOrEmpty(this.patientInfo.ID))
            {
                MessageBox.Show("请先检索患者！");
                return;
            }

            if (this.FeeManager.OpenAccount(this.patientInfo.ID) < 0)
            {
                MessageBox.Show("开帐失败，原因：" + this.FeeManager.Err);
            }
            else
            {
                MessageBox.Show("开帐成功");
            }
        }

        /// <summary>
        /// 清空费用信息
        /// </summary>
        public void ClearFee()
        {
            this.FpTotFee_Sheet1.RowCount = 0;
            this.FpSelectedFee_Sheet1.RowCount = 0;
            this.FpPrepay_Sheet1.RowCount = 0;
            this.FpPackage_Sheet1.RowCount = 0;
            this.hsFeeItem.Clear();
            this.hsSelectedFeeItem.Clear();
            this.feeItemList.Clear();

            this.BalanceTot = 0.0m;
            this.BalanceReal = 0.0m;
            this.BalanceDonate = 0.0m;
            this.BalanceEco = 0.0m;
            this.PrepayCost = 0.0m;
            this.ShouldPay = 0.0m;
            this.ShouldRtn = 0.0m;

            this.tbBalanceTot.Text = "0.00";
            this.tbBalanceReal.Text = "0.00";
            this.tbBalanceDonate.Text = "0.00";
            this.tbBalanceEco.Text = "0.00";
            this.tbPrepayCost.Text = "0.00";
            this.tbShouldPay.Text = "0.00";
            this.tbShouldRtn.Text = "0.00";
        }

        /// <summary>
        /// toolBarService
        /// </summary>
        FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        /// <summary>
        /// 增加ToolBar控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("确定", "结算患者费用", (int)FS.FrameWork.WinForms.Classes.EnumImageList.B保存, true, false, null);
            toolBarService.AddToolButton("更新发票号", "更新下一发票号", (int)FS.FrameWork.WinForms.Classes.EnumImageList.F分票, true, false, null);
            toolBarService.AddToolButton("计算器", "调用计算器", (int)FS.FrameWork.WinForms.Classes.EnumImageList.J计算器, true, false, null);
            ////{14CCBD16-9A45-42f8-896C-5A2CB00DAB1B}
            toolBarService.AddToolButton("开帐", "开帐", (int)FS.FrameWork.WinForms.Classes.EnumImageList.K开帐, true, false, null);
            toolBarService.AddToolButton("备注", "设置患者备注信息", (int)FS.FrameWork.WinForms.Classes.EnumImageList.B报警, true, false, null);
            toolBarService.AddToolButton("结算明细清单", "打印结算明细清单", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D打印清单, true, false, null);
            toolBarService.AddToolButton("医保结算单", "打印医保结算单", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D打印执行单, true, false, null);
            return this.toolBarService;
        }

        /// <summary>
        /// 定义toolbar按钮click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "确定":
                    this.OnSave(sender, e);
                    break;
                case "更新发票号":
                    FS.HISFC.Components.Common.Forms.frmUpdateUsedInvoiceNo frmUpdate = new FS.HISFC.Components.Common.Forms.frmUpdateUsedInvoiceNo();
                    frmUpdate.InvoiceType = "I";
                    frmUpdate.ShowDialog();
                    GetNextInvoiceNO();
                    break;
                case "计算器":
                    //this.DisplayCalc();
                    break;
                case "开帐":
                    this.OpenAccount();
                    break;
                //2012-08-07
                case "结算明细清单":
                    //this.PrintIBillPrint();
                    break;
                case "结算汇总清单":
                    //this.PrintIBillPrintCollect();
                    break;
                case "医保结算单":
                    //this.PrintIBillPrintSI();
                    break;
                case "备注":
                    //this.SetMemo();
                    break;
            }

            base.ToolStrip_ItemClicked(sender, e);
        }

        #endregion

        /// <summary>
        /// 结算类型枚举
        /// </summary>
        private enum BalanceType
        {
            /// <summary>
            /// 普通结算
            /// </summary>
            Normal = 1,

            /// <summary>
            /// 部分结算
            /// </summary>
            Part = 2,

            /// <summary>
            /// 套餐结算
            /// </summary>
            Package = 3
        }

        /// <summary>
        /// 费用明细列枚举
        /// </summary>
        private enum TotFeeCols
        {
            /// <summary>
            /// 项目名称
            /// </summary>
            ItemName = 0,
            /// <summary>
            /// 规格
            /// </summary>
            Spec = 1,
            /// <summary>
            /// 单价
            /// </summary>
            Price = 2,
            /// <summary>
            /// 单位
            /// </summary>
            Unit = 3,
            /// <summary>
            /// 总数量
            /// </summary>
            Qty = 4,
            /// <summary>
            /// 计价单位
            /// </summary>
            PriceUnit = 5,
            /// <summary>
            /// 金额// {F53BD032-1D92-4447-8E20-6C38033AA607}
            /// </summary>
            Cost = 6,
            /// <summary>
            /// 待分配
            /// </summary>
            LeftQty = 7,
            /// <summary>
            /// 已分配
            /// </summary>
            CostQty = 8
        }

        /// <summary>
        /// 选中明细列枚举
        /// </summary>
        private enum SelectedFeeCols
        {
            /// <summary>
            /// 项目名称
            /// </summary>
            ItemName = 0,
            /// <summary>
            /// 规格
            /// </summary>
            Spec = 1,
            /// <summary>
            /// 价格
            /// </summary>
            Price = 2,
            /// <summary>
            /// 单位
            /// </summary>
            Unit = 3,
            /// <summary>
            /// 数量
            /// </summary>
            Qty = 4,
            /// <summary>
            /// 单位
            /// </summary>
            PriceUnit = 5,
            /// <summary>
            /// 金额// {F53BD032-1D92-4447-8E20-6C38033AA607}
            /// </summary>
            Cost = 6
        }

        /// <summary>
        /// 预交金列枚举
        /// </summary>
        private enum PrepayCols
        {
            /// <summary>
            /// 是否选择
            /// </summary>
            Check = 0,
            /// <summary>
            /// 金额
            /// </summary>
            Cost = 1,
            /// <summary>
            /// 收取方式
            /// </summary>
            Payway,
            /// <summary>
            /// 日期
            /// </summary>
            Date
        }

        /// <summary>
        /// 套餐列枚举
        /// </summary>
        private enum PackageCols
        {
            /// <summary>
            /// 是否选择
            /// </summary>
            Check = 0,
            /// <summary>
            /// 套餐名称
            /// </summary>
            PackageName = 1,
            /// <summary>
            /// 套餐金额
            /// </summary>
            TotCost = 2,
            /// <summary>
            /// 实收金额
            /// </summary>
            RealCost = 3,
            /// <summary>
            /// 赠送金额
            /// </summary>
            GiftCost = 4,
            /// <summary>
            /// 优惠金额
            /// </summary>
            EcoCost = 5,

            /// <summary>
            ///发票号
            /// </summary>
            InvoiceNo = 6
        }


        /// <summary>
        /// 套餐列枚举  //{C4231074-D350-4df9-AF7C-C37124B44B80}
        /// </summary>
        private enum PreBalanceCols
        {
            /// <summary>
            /// 预结算号
            /// </summary>
            prebalanceno = 0,
            /// <summary>
            /// 姓名
            /// </summary>
            patientname = 1,
            /// <summary>
            /// 预结金额
            /// </summary>
            balanceprice = 2,
            /// <summary>
            /// 预结时间
            /// </summary>
            balancedate = 3,
            /// <summary>
            /// 预结人
            /// </summary>
            opername = 4,
            /// <summary>
            /// 备注
            /// </summary>
            memo = 5
        }

        private void cbxPass_CheckedChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 下拉选择收费项目定位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>{15E1CDD6-FBA9-4d93-8204-0BC788EBC265}
        private void ncbxItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ncbxItem.Tag.ToString()))
            {
                foreach (Row row in this.FpTotFee_Sheet1.Rows)
                {
                    FeeItemList tmp = row.Tag as FeeItemList;
                    if (tmp != null && ncbxItem.Tag.ToString() == tmp.Item.ID)
                    {
                        this.FpTotFee_Sheet1.SetActiveCell(row.Index, (int)TotFeeCols.CostQty);
                        this.FpTotFee.ShowActiveCell(VerticalPosition.Nearest, HorizontalPosition.Nearest);
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// 套餐区分显示 {97fbd080-edb9-f31f-41fd-33f7837851ba}
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbTctype_SelectedValueChanged(object sender, EventArgs e)
        {
            ComboBox obx = sender as ComboBox;
            if (obx != null)
            {
                IsSelectedAll = false;
                if (obx.SelectedItem.ToString().Trim() == "门诊套餐")
                {
                    selectedrange = FS.HISFC.Models.Base.ServiceTypes.C;
                }
                else if (obx.SelectedItem.ToString().Trim() == "住院套餐")
                {
                    selectedrange = FS.HISFC.Models.Base.ServiceTypes.I;
                }
                else if (obx.SelectedItem.ToString().Trim() == "体检套餐")
                {
                    selectedrange = FS.HISFC.Models.Base.ServiceTypes.T;
                }
                else if (obx.SelectedItem.ToString().Trim() == "全院套餐")
                {
                    selectedrange = FS.HISFC.Models.Base.ServiceTypes.A;
                }
                else
                {
                    IsSelectedAll = true;
                }
                if (!string.IsNullOrEmpty(this.patientInfo.PID.CardNO))
                {
                    SetPackageInfo();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.PreBalance();  //{C4231074-D350-4df9-AF7C-C37124B44B80}
        }


        private bool ValidPreBalancePackage()  //{C4231074-D350-4df9-AF7C-C37124B44B80}
        {
            FS.HISFC.Models.Fee.Inpatient.PreBalance head = this.FpPreBalance_Sheet1.ActiveRow.Tag as FS.HISFC.Models.Fee.Inpatient.PreBalance;
            string packageids = head.PACKAGEIDS;
            string[] packidsarr = packageids.Split(new char[1] { ',' });
            bool packageExit = false;
            foreach (string item in packidsarr.ToArray<string>())
            {
                packageExit = false;
                for (int i = 0; i < FpPackage_Sheet1.Rows.Count; i++)
                {
                    FS.HISFC.Models.MedicalPackage.Fee.Package pack = FpPackage_Sheet1.Rows[i].Tag as FS.HISFC.Models.MedicalPackage.Fee.Package;
                    if (item == pack.ID)
                    {
                        packageExit = true;
                        FpPackage_Sheet1.Cells[i, (int)(PackageCols.Check)].Value = true;
                        this.IsMatching = true;
                        break;
                    }
                }
                if (packageExit == false)
                {
                    return false;
                }
            }
            return packageExit;


        }
        private void PreBalanceMatching()  //{C4231074-D350-4df9-AF7C-C37124B44B80}
        {
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在进行匹配，请等待。。。");
            Application.DoEvents();
            //判断预结算套餐是否存在
            if (ValidPreBalancePackage() == false)
            {
                if (chkPackage.Checked)
                {
                    // MessageBox.Show("预结算套餐不存在");   //{34a15202-a3f9-4d3e-9bad-c7e6783b540c}
                }
            }
            FS.HISFC.Models.Fee.Inpatient.PreBalance head = this.FpPreBalance_Sheet1.ActiveRow.Tag as FS.HISFC.Models.Fee.Inpatient.PreBalance;
            ArrayList arList = prebalancelogic.QueryPreBalanceDetailByPreBalanceNo(head.PREBLANCENO);
            for (int i = 0; i < FpTotFee_Sheet1.Rows.Count; i++)
            {
                FeeItemList rowFee = FpTotFee_Sheet1.Rows[i].Tag as FeeItemList;
                foreach (FS.HISFC.Models.Fee.Inpatient.PreBalanceList detail in arList)
                {
                    if (detail.ITEM_CODE == rowFee.Item.ID && detail.PACKAGE_CODE == rowFee.UndrugComb.ID)
                    {
                        if (FS.FrameWork.Function.NConvert.ToDecimal(FpTotFee_Sheet1.Cells[i, (int)(TotFeeCols.Qty)].Value) < FS.FrameWork.Function.NConvert.ToDecimal(detail.QTY))
                        {
                            FpTotFee_Sheet1.Cells[i, (int)(TotFeeCols.CostQty)].Text = FpTotFee_Sheet1.Cells[i, (int)(TotFeeCols.Qty)].Text;
                        }
                        else
                        {
                            FpTotFee_Sheet1.Cells[i, (int)(TotFeeCols.CostQty)].Text = detail.QTY.ToString("0.00");
                        }
                    }
                }

            }
            this.IsMatching = false;
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            this.SetCostInfo(this.BalanceEco);

        }

    }
}


