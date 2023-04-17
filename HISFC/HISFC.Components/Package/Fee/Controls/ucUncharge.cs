using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FarPoint.Win.Spread;

namespace HISFC.Components.Package.Fee.Controls
{
    public partial class ucUncharge : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        #region 属性

        /// <summary>
        /// 患者基本信息
        /// </summary>
        protected FS.HISFC.Models.RADT.PatientInfo patientInfo = null;

        /// <summary>
        /// 患者基本信息
        /// </summary>
        public FS.HISFC.Models.RADT.PatientInfo PatientInfo
        {
            get { return this.patientInfo; }
            set
            {
                this.patientInfo = value;
                this.SetPatientInfo();
                this.SetQuitCostInfo();
            }
        }

        /// <summary>
        /// 当前会员卡信息
        /// </summary>
        private FS.HISFC.Models.Account.AccountCard accountCardInfo = null;

        /// <summary>
        /// 会员卡信息
        /// </summary>
        public FS.HISFC.Models.Account.AccountCard AccountCardInfo
        {
            get { return this.accountCardInfo; }
            set { this.accountCardInfo = value; }
        }

        /// <summary>
        /// 当前发票信息
        /// </summary>
        private FS.HISFC.Models.MedicalPackage.Fee.Invoice invoiceInfo = null;

        /// <summary>
        /// 当前发票信息
        /// </summary>
        public FS.HISFC.Models.MedicalPackage.Fee.Invoice InvoiceInfo
        {
            get { return this.invoiceInfo; }
            set { this.invoiceInfo = value; }
        }

        /// <summary>
        /// 支付方式
        /// </summary>
        private ArrayList payInfoList = new ArrayList();

        /// <summary>
        /// 支付方式统计
        /// </summary>
        private Hashtable hsPayInfo = new Hashtable();

        /// <summary>
        /// 是否启用套餐管理CRM系统
        /// </summary>
        private bool IsPackageDealInCrm = false;

        /// <summary>
        /// 发票内套餐列表
        /// </summary>
        private ArrayList packageList = new ArrayList();

        /// <summary>
        /// 发票内明细，以套餐流水号为Key
        /// </summary>
        private Hashtable detailList = new Hashtable();

        /// <summary>
        /// 重新收费的套餐列表
        /// </summary>
        private ArrayList refeePackageList = new ArrayList();

        /// <summary>
        /// 重新收费的发票明细，以套餐流水号为Key
        /// </summary>
        private Hashtable refeedetailList = new Hashtable();

        /// <summary>
        /// 消费信息
        /// </summary>
        private Hashtable hsCostInfo = new Hashtable();

        /// <summary>
        /// 是否启用积分模块
        /// </summary>
        private bool IsCouponModuleInUse = false;

        /// <summary>
        /// 控制参数业务层
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        #endregion

        #region 控件

        /// <summary>
        /// 左侧发票树
        /// </summary>
        ucInvoiceTree invoiceTree = new ucInvoiceTree();

        #endregion

        #region 业务类
        /// <summary>
        /// 费用业务层
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();
        /// <summary>
        /// 套餐业务管理类
        /// </summary>
        FS.HISFC.BizProcess.Integrate.MedicalPackage.Fee.Package feePackageProcess = new FS.HISFC.BizProcess.Integrate.MedicalPackage.Fee.Package();
        /// <summary>
        /// 发票管理类
        /// </summary>
        FS.HISFC.BizLogic.MedicalPackage.Fee.Invoice invoiceMgr = new FS.HISFC.BizLogic.MedicalPackage.Fee.Invoice();
        /// <summary>
        /// 套餐购买管理类
        /// </summary>
        FS.HISFC.BizLogic.MedicalPackage.Fee.Package packageMgr = new FS.HISFC.BizLogic.MedicalPackage.Fee.Package();
        /// <summary>
        /// 押金业务数据类
        /// </summary>
        private FS.HISFC.BizLogic.MedicalPackage.Fee.Deposit depositMgr = new FS.HISFC.BizLogic.MedicalPackage.Fee.Deposit();
        /// <summary>
        /// 套餐细项购买管理类
        /// </summary>
        FS.HISFC.BizLogic.MedicalPackage.Fee.PackageDetail packageDetailMgr = new FS.HISFC.BizLogic.MedicalPackage.Fee.PackageDetail();
        /// <summary>
        /// 账户业务层
        /// </summary>
        private FS.HISFC.BizLogic.Fee.Account accountMgr = new FS.HISFC.BizLogic.Fee.Account();
        /// <summary>
        /// 套餐购买管理类
        /// </summary>
        FS.HISFC.BizLogic.MedicalPackage.Package pckMgr = new FS.HISFC.BizLogic.MedicalPackage.Package();
        /// <summary>
        /// 支付方式管理类
        /// </summary>
        FS.HISFC.BizLogic.MedicalPackage.Fee.PayMode paymodeMgr = new FS.HISFC.BizLogic.MedicalPackage.Fee.PayMode();
        /// <summary>
        /// 非药品
        /// </summary>
        FS.HISFC.BizLogic.Fee.Item itemMgr = new FS.HISFC.BizLogic.Fee.Item();
        /// <summary>
        /// 药品
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Pharmacy itemIntegrate = new FS.HISFC.BizProcess.Integrate.Pharmacy();

        /// <summary>
        /// 支付方式列表
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper helpPayMode = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 管理业务层
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// 控制管理类
        /// </summary>
        private FS.FrameWork.Management.ControlParam ctlMgr = new FS.FrameWork.Management.ControlParam();

        /// <summary>
        /// 消费记录管理类
        /// </summary>
        private FS.HISFC.BizLogic.MedicalPackage.Fee.PackageCost packageCostMgr = null;

        public FS.HISFC.BizLogic.MedicalPackage.Fee.PackageCost PackageCostMgr
        {
            get
            {
                if (packageCostMgr == null)
                {
                    packageCostMgr = new FS.HISFC.BizLogic.MedicalPackage.Fee.PackageCost();
                }
                return packageCostMgr;
            }
        }

        #endregion


        public ucUncharge()
        {
            InitializeComponent();
            InitControls();
        }

        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControls()
        {
            this.invoiceTree.Dock = DockStyle.Fill;
            this.pnTree.Controls.Add(invoiceTree);

            //FP热键屏蔽
            InputMap im;
            im = this.fpPackageDetail.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            //支付方式
            ArrayList payModes = this.managerIntegrate.GetConstantList(FS.HISFC.Models.Base.EnumConstant.PAYMODES);
            this.helpPayMode.ArrayObject = new ArrayList(payModes.Cast<FS.HISFC.Models.Base.Const>()
                                                                 .Where(t => t.Memo == "true")
                                                                 .Where(t => t.UserCode != "ACD")
                                                                 .Where(t => t.UserCode != "ACY")
                                                                 .Where(t => t.UserCode != "ECO")
                                                                 .ToArray());
            FS.HISFC.Models.Base.Const cst = new FS.HISFC.Models.Base.Const();
            cst.ID = "ADYYSFH";
            cst.Name = "原路返回";
            this.helpPayMode.ArrayObject.Insert(0, cst);
            this.cmbPayModes.AddItems(this.helpPayMode.ArrayObject);

            this.IsCouponModuleInUse = (this.ctlMgr.QueryControlerInfo("CP0001") == "1");

            this.IsPackageDealInCrm = this.controlParamIntegrate.GetControlParam<bool>("CPP001", false, false);
            this.addEvents();
        }

        /// <summary>
        /// 增加事件
        /// </summary>
        private void addEvents()
        {
            this.invoiceTree.SelectInvoice += new DelegateInvoicesSet(invoiceTree_SelectInvoice);
            this.tbInvoiceNO.KeyDown += new KeyEventHandler(tbInvoiceNO_KeyDown);
            this.fpPackageDetail.CellClick += new FarPoint.Win.Spread.CellClickEventHandler(fpPackageDetail_CellClick);
            this.fpPackageDetail.Change += new ChangeEventHandler(fpPackageDetail_Change);
            this.fpPackage.SelectionChanged += new FarPoint.Win.Spread.SelectionChangedEventHandler(fpPackage_SelectionChanged);
            this.fpPackage.ButtonClicked += new EditorNotifyEventHandler(fpPackage_ButtonClicked);
        }

        /// <summary>
        /// 删除时间
        /// </summary>
        private void delEvents()
        {
            this.invoiceTree.SelectInvoice -= new DelegateInvoicesSet(invoiceTree_SelectInvoice);
            this.tbInvoiceNO.KeyDown -= new KeyEventHandler(tbInvoiceNO_KeyDown);
            this.fpPackageDetail.CellClick -= new FarPoint.Win.Spread.CellClickEventHandler(fpPackageDetail_CellClick);
            this.fpPackageDetail.Change -= new ChangeEventHandler(fpPackageDetail_Change);
            this.fpPackage.SelectionChanged -= new FarPoint.Win.Spread.SelectionChangedEventHandler(fpPackage_SelectionChanged);
            this.fpPackage.ButtonClicked -= new EditorNotifyEventHandler(fpPackage_ButtonClicked);
        }

        /// <summary>
        /// 设置患者信息
        /// </summary>
        private void SetPatientInfo()
        {
            //{80B9B163-A9C4-49d3-9BA0-453712CA7B95}
            //if(this.PatientInfo == null || this.InvoiceInfo == null || this.AccountCardInfo == null)
            if (this.PatientInfo == null || this.InvoiceInfo == null)
            {
                this.tbInvoiceNO.Text = string.Empty;
                this.tbCardNo.Text = string.Empty;
                this.tbName.Text = string.Empty;
                this.tbTotCost.Text = string.Empty;
                this.tbRealCost.Text = string.Empty;
                this.tbGiftCost.Text = string.Empty;
                this.tbETCCost.Text = string.Empty;
                this.tbQuitTotFee.Text = "0.00";
                this.tbQuitAccount.Text = "0.00";
                this.tbQuitGift.Text = "0.00";
                this.tbQuitOther.Text = "0.00";
                this.lblSSWR.Text = "0.0";
                this.cmbPayModes.Tag = "CA";
                this.lblPayInfo.Text = "";
                this.ShowInvoiceInfo();
                return;
            }

            this.tbInvoiceNO.Text = this.InvoiceInfo.ID;
            this.tbCardNo.Text = this.PatientInfo.PID.CardNO;
            this.tbName.Text = this.PatientInfo.Name;
            this.tbTotCost.Text = this.InvoiceInfo.Package_Cost.ToString("F2");
            this.tbRealCost.Text = this.InvoiceInfo.Real_Cost.ToString("F2");
            this.tbGiftCost.Text = this.InvoiceInfo.Gift_cost.ToString("F2");
            this.tbETCCost.Text = this.InvoiceInfo.Etc_cost.ToString("F2");
            this.cmbPayModes.Tag = "ADYYSFH";
            this.ShowInvoiceInfo();
        }

        /// <summary>
        /// 设置发票信息显示
        /// </summary>
        private void ShowInvoiceInfo()
        {
            try
            {
                this.delEvents();

                if (this.packageList == null)
                {
                    this.packageList = new ArrayList();
                }

                if (this.detailList == null)
                {
                    this.detailList = new Hashtable();
                }

                this.payInfoList.Clear();
                this.packageList.Clear();
                this.detailList.Clear();

                this.hsPayInfo.Clear();
                this.hsCostInfo.Clear();
                this.refeedetailList.Clear();
                this.refeePackageList.Clear();

                this.fpPackage_Sheet1.RowCount = 0;
                this.fpPackageDetail_Sheet1.RowCount = 0;

                //{80B9B163-A9C4-49d3-9BA0-453712CA7B95}
                //if (this.PatientInfo == null || this.AccountCardInfo == null || this.InvoiceInfo == null)
                if (this.PatientInfo == null || this.InvoiceInfo == null)
                {
                    throw new Exception();
                }

                this.payInfoList = paymodeMgr.QueryByInvoiceNO(this.invoiceInfo.InvoiceNO, "0");

                this.packageList = this.packageMgr.QueryByInvoiceNO(this.InvoiceInfo.ID, "0");

                if (this.packageList == null || this.packageList.Count == 0)
                {
                    MessageBox.Show("未找到此发票的费用详情！");
                    this.PatientInfo = null;
                    throw new Exception();
                }

                foreach (FS.HISFC.Models.MedicalPackage.Fee.Package package in this.packageList)
                {
                    ArrayList details = this.packageDetailMgr.QueryDetailByClinicNO(package.ID, "0");

                    foreach (FS.HISFC.Models.MedicalPackage.Fee.PackageDetail detail in details)
                    {
                        decimal tmpQTY = detail.Item.Qty;
                        if (detail.Item.ID.Substring(0, 1) == "Y")
                        {
                            detail.Item = itemIntegrate.GetItem(detail.Item.ID);
                        }
                        else
                        {
                            detail.Item = itemMgr.GetUndrugByCode(detail.Item.ID);
                        }

                        //{64FC6A63-12D4-4075-A148-5F1C4FDEEB83}
                        //住院消费的套餐只是更新了Cost_Flag标记
                        //因此此处需要进行判断，如果cost_flag == "1"
                        //则所有明细不可再退
                        if (package.Cost_Flag == "1")
                        {
                            detail.ConfirmQTY = tmpQTY;
                            detail.RtnQTY = 0;
                        }

                        detail.Item.Qty = tmpQTY;
                        //在User01中存放退费数量(暂时没有其他字段可用)
                        detail.User01 = (detail.Item.Qty - detail.ConfirmQTY).ToString("F2");
                    }

                    this.detailList.Add(package.ID, details);

                    package.PackageInfo = this.pckMgr.QueryPackageByID(package.PackageInfo.ID);
                    this.fpPackage_Sheet1.AddRows(this.fpPackage_Sheet1.RowCount, 1);
                    this.fpPackage_Sheet1.Rows[this.fpPackage_Sheet1.RowCount - 1].Tag = package;

                    //{9B833B34-AE7F-4013-8F9D-CDE36A738D02}
                    this.fpPackage_Sheet1.Cells[this.fpPackage_Sheet1.RowCount - 1, (int)PackageCols.SpecialFlag].Value = package.SpecialFlag == "1" ? true : false;
                    this.fpPackage_Sheet1.Cells[this.fpPackage_Sheet1.RowCount - 1, (int)PackageCols.SpecialFlag].Locked = true;

                    this.fpPackage_Sheet1.Cells[this.fpPackage_Sheet1.RowCount - 1, (int)PackageCols.Select].Value = true;
                    this.fpPackage_Sheet1.Cells[this.fpPackage_Sheet1.RowCount - 1, (int)PackageCols.Name].Text = package.PackageInfo.Name;
                    this.fpPackage_Sheet1.Cells[this.fpPackage_Sheet1.RowCount - 1, (int)PackageCols.TotCost].Text = package.Package_Cost.ToString("F2");
                    this.fpPackage_Sheet1.Cells[this.fpPackage_Sheet1.RowCount - 1, (int)PackageCols.RealCost].Text = package.Real_Cost.ToString("F2");
                    this.fpPackage_Sheet1.Cells[this.fpPackage_Sheet1.RowCount - 1, (int)PackageCols.GiftCost].Text = package.Gift_cost.ToString("F2");
                    this.fpPackage_Sheet1.Cells[this.fpPackage_Sheet1.RowCount - 1, (int)PackageCols.ETCCost].Text = package.Etc_cost.ToString("F2");
                }

                this.fpPackage_SelectionChanged(null, null);
            }
            catch
            {
            }

            this.addEvents();
        }

        /// <summary>
        /// 设置退费信息
        /// </summary>
        private void SetQuitCostInfo()
        {
            //{80B9B163-A9C4-49d3-9BA0-453712CA7B95}
            //if (this.PatientInfo == null || this.AccountCardInfo == null || this.InvoiceInfo == null)
            if (this.PatientInfo == null || this.InvoiceInfo == null)
            {
                return;
            }

            //重新收费信息
            this.refeePackageList.Clear();
            this.refeedetailList.Clear();
            this.hsCostInfo.Clear();
            this.hsPayInfo.Clear();

            try
            {
                //统计支付方式
                if (this.CountPayType() < 0)
                {
                    throw new Exception("统计支付方式时出错！");
                }
                //统计总收费金额和总退费金额
                if (this.CountCostInfo() < 0)
                {
                    throw new Exception("统计费用信息时出错！");
                }

                //统计账户与赠送金额
                decimal accountpay = hsPayInfo.ContainsKey("YS") ? (decimal)hsPayInfo["YS"] : 0.0m;
                decimal giftpay = hsPayInfo.ContainsKey("DC") ? (decimal)hsPayInfo["DC"] : 0.0m;
                decimal sswrpay = hsPayInfo.ContainsKey("SW") ? (decimal)hsPayInfo["SW"] : 0.0m;

                //统计各退费类型退费金额
                decimal shouldRtnTot = ((decimal)hsCostInfo["REAL"] - (decimal)hsCostInfo["REREAL"]) + ((decimal)hsCostInfo["GIFT"] - (decimal)hsCostInfo["REGIFT"]);      //应退总金额(四舍五入部分不用退)
                decimal shouldRtnAccount = accountpay >= (decimal)hsCostInfo["REREAL"] ? accountpay - (decimal)hsCostInfo["REREAL"] : 0.0m;  //应退账户金额
                decimal shouldRtnOther = (decimal)hsCostInfo["REAL"] - sswrpay - (accountpay >= (decimal)hsCostInfo["REREAL"] ? accountpay : (decimal)hsCostInfo["REREAL"]);  //应退其他
                decimal shouldRtnGift = ((decimal)hsCostInfo["GIFT"] - (decimal)hsCostInfo["REGIFT"]);     //应退赠送

                //界面赋值
                this.tbQuitTotFee.Text = shouldRtnTot.ToString("F2");
                this.tbQuitAccount.Text = shouldRtnAccount.ToString("F2");
                this.tbQuitGift.Text = shouldRtnGift.ToString("F2");
                this.tbQuitOther.Text = shouldRtnOther.ToString("F2");
                this.lblSSWR.Text = sswrpay.ToString("F2");

                string payInfo = string.Empty;
                foreach (string key in hsPayInfo.Keys)
                {
                    if (key == "YS")
                    {
                        payInfo += "账户支付(充值账户)";
                        payInfo += ((decimal)hsPayInfo[key]).ToString("F2");
                        payInfo += ";    ";
                        continue;
                    }

                    if (key == "DC")
                    {
                        payInfo += "账户支付(赠送)";
                        payInfo += ((decimal)hsPayInfo[key]).ToString("F2");
                        payInfo += ";    ";
                        continue;
                    }

                    foreach (FS.HISFC.Models.Base.Const paymode in this.helpPayMode.ArrayObject)
                    {
                        if (paymode.ID == key)
                        {
                            payInfo += paymode.Name;
                            payInfo += ((decimal)hsPayInfo[key]).ToString("F2");
                            payInfo += ";    ";
                            break;
                        }
                    }
                }

                this.lblPayInfo.Text = payInfo;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.PatientInfo = null;
            }
        }

        /// <summary>
        /// 统计支付类型
        /// </summary>
        /// <returns></returns>
        private int CountPayType()
        {
            try
            {
                foreach (FS.HISFC.Models.MedicalPackage.Fee.PayMode pay in this.payInfoList)
                {
                    //if (pay.Mode_Code == "YS" || pay.Related_ModeCode == "YS" )   //普通支付
                    //{
                    //    if (this.hsPayInfo.ContainsKey("YS"))
                    //    {
                    //        this.hsPayInfo["YS"] = (decimal)this.hsPayInfo["YS"] + pay.Tot_cost;
                    //    }
                    //    else
                    //    {
                    //        this.hsPayInfo.Add("YS", pay.Tot_cost);
                    //    }  
                    //}
                    //else if (pay.Mode_Code == "DC" || pay.Related_ModeCode == "DC")
                    //{
                    //    if (this.hsPayInfo.ContainsKey("DC"))
                    //    {
                    //        this.hsPayInfo["DC"] = (decimal)this.hsPayInfo["DC"] + pay.Tot_cost;
                    //    }
                    //    else
                    //    {
                    //        this.hsPayInfo.Add("DC", pay.Tot_cost);
                    //    }
                    //}
                    //else
                    //{

                    //}
                    if (pay.Mode_Code == "DE")  //用普通方式缴纳的押金
                    {
                        if (this.hsPayInfo.ContainsKey(pay.Related_ModeCode))
                        {
                            this.hsPayInfo[pay.Related_ModeCode] = (decimal)this.hsPayInfo[pay.Related_ModeCode] + pay.Tot_cost;
                        }
                        else
                        {
                            this.hsPayInfo.Add(pay.Related_ModeCode, pay.Tot_cost);
                        }
                    }
                    else
                    {
                        if (this.hsPayInfo.ContainsKey(pay.Mode_Code))
                        {
                            this.hsPayInfo[pay.Mode_Code] = (decimal)this.hsPayInfo[pay.Mode_Code] + pay.Tot_cost;
                        }
                        else
                        {
                            this.hsPayInfo.Add(pay.Mode_Code, pay.Tot_cost);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return -1;
            }

            return 1;
        }

        private int CountCostInfo()
        {
            try
            {
                if (hsCostInfo == null)
                {
                    hsCostInfo = new Hashtable();
                }

                hsCostInfo.Add("TOT", 0.0m);
                hsCostInfo.Add("REAL", 0.0m);
                hsCostInfo.Add("GIFT", 0.0m);
                hsCostInfo.Add("ETC", 0.0m);
                hsCostInfo.Add("RETOT", 0.0m);
                hsCostInfo.Add("REREAL", 0.0m);
                hsCostInfo.Add("REGIFT", 0.0m);
                hsCostInfo.Add("REETC", 0.0m);

                foreach (FS.HISFC.Models.MedicalPackage.Fee.Package package in this.packageList)
                {
                    ArrayList details = this.detailList[package.ID] as ArrayList;
                    decimal packageCost = 0.0m;
                    decimal packageRealCost = 0.0m;
                    decimal packageGiftCost = 0.0m;
                    decimal packageEtcCost = 0.0m;

                    foreach (FS.HISFC.Models.MedicalPackage.Fee.PackageDetail detail in details)
                    {
                        decimal qty = detail.RtnQTY;                 //可退数量
                        decimal rtn = Decimal.Parse(detail.User01);  //退费数量
                        decimal refeertn = qty - rtn;                //重新计费的可退数量
                        decimal refeeConfirmQTY = detail.ConfirmQTY; //重新收费的已确认数量

                        if (refeertn == 0 && refeeConfirmQTY == 0)
                        {
                            continue;
                        }

                        decimal detailCost = Math.Round(((refeertn + refeeConfirmQTY) * detail.Detail_Cost) / (detail.Item.Qty), 2, MidpointRounding.ToEven);
                        decimal realCost = Math.Round(((refeertn + refeeConfirmQTY) * detail.Real_Cost) / (detail.Item.Qty), 2, MidpointRounding.ToEven);
                        decimal giftCost = Math.Round(((refeertn + refeeConfirmQTY) * detail.Gift_cost) / (detail.Item.Qty), 2, MidpointRounding.ToEven);
                        decimal etcCost = Math.Round(((refeertn + refeeConfirmQTY) * detail.Etc_cost) / (detail.Item.Qty), 2, MidpointRounding.ToEven);

                        packageCost += detailCost;
                        packageRealCost += realCost;
                        packageGiftCost += giftCost;
                        packageEtcCost += etcCost;

                        FS.HISFC.Models.MedicalPackage.Fee.PackageDetail refeedetail = detail.Clone();
                        refeedetail.Detail_Cost = detailCost;
                        refeedetail.Real_Cost = realCost;
                        refeedetail.Gift_cost = giftCost;
                        refeedetail.Etc_cost = etcCost;
                        refeedetail.RtnQTY = refeertn;
                        refeedetail.ConfirmQTY = refeeConfirmQTY;
                        refeedetail.Item.Qty = refeertn + refeeConfirmQTY;
                        refeedetail.Cancel_Flag = "0";
                        //划价人，划价日期，收费人,收费日期,发票号，发票序列号
                        if (refeedetailList.ContainsKey(package.ID))
                        {
                            ArrayList refeedetails = this.refeedetailList[package.ID] as ArrayList;
                            refeedetails.Add(refeedetail);
                        }
                        else
                        {
                            ArrayList refeedetails = new ArrayList();
                            refeedetails.Add(refeedetail);
                            this.refeedetailList.Add(package.ID, refeedetails);
                        }
                    }

                    //该套餐存在需要进行重新收费的项目时进行操作
                    if (refeedetailList.ContainsKey(package.ID))
                    {
                        FS.HISFC.Models.MedicalPackage.Fee.Package refeepackage = package.Clone();
                        refeepackage.Package_Cost = packageCost;
                        refeepackage.Real_Cost = packageRealCost;
                        refeepackage.Gift_cost = packageGiftCost;
                        refeepackage.Etc_cost = packageEtcCost;
                        refeepackage.Original_Code = package.InvoiceNO + "-" + package.ID;
                        //cliniccode 划价人，划价日期，收费人收费日期，发票号需要在重新收费的时候赋值
                        this.refeePackageList.Add(refeepackage);

                        hsCostInfo["RETOT"] = (decimal)hsCostInfo["RETOT"] + refeepackage.Package_Cost;
                        hsCostInfo["REREAL"] = (decimal)hsCostInfo["REREAL"] + refeepackage.Real_Cost;
                        hsCostInfo["REGIFT"] = (decimal)hsCostInfo["REGIFT"] + refeepackage.Gift_cost;
                        hsCostInfo["REETC"] = (decimal)hsCostInfo["REETC"] + refeepackage.Etc_cost;
                    }

                    hsCostInfo["TOT"] = (decimal)hsCostInfo["TOT"] + package.Package_Cost;
                    hsCostInfo["REAL"] = (decimal)hsCostInfo["REAL"] + package.Real_Cost;
                    hsCostInfo["GIFT"] = (decimal)hsCostInfo["GIFT"] + package.Gift_cost;
                    hsCostInfo["ETC"] = (decimal)hsCostInfo["ETC"] + package.Etc_cost;
                }
            }
            catch
            {
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 左侧树选择事件
        /// </summary>
        /// <param name="invoice"></param>
        /// <returns></returns>
        private int invoiceTree_SelectInvoice(FS.HISFC.Models.MedicalPackage.Fee.Invoice invoice)
        {
            this.invoiceInfo = invoice;
            this.AccountCardInfo = this.invoiceTree.AccountCardInfo;
            this.PatientInfo = this.invoiceTree.PatientInfo;
            return 0;
        }

        /// <summary>
        /// 发票号回车
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbInvoiceNO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string invoiceStr = this.tbInvoiceNO.Text.Trim();
                this.PatientInfo = null;

                if (string.IsNullOrEmpty(invoiceStr))
                {
                    return;
                }

                this.invoiceInfo = invoiceMgr.QueryByInvoiceNO(invoiceStr, "1");
                if (this.invoiceInfo == null || this.invoiceInfo.Cancel_Flag != "0")
                {
                    MessageBox.Show("发票号不存在或已退费！");
                    return;
                }

                ArrayList packages = this.packageMgr.QueryByInvoiceNO(this.invoiceInfo.ID, "0");
                if (packages == null || packages.Count == 0)
                {
                    MessageBox.Show("发票号不存在费用！");
                    return;
                }

                string CardNO = (packages[0] as FS.HISFC.Models.MedicalPackage.Fee.Package).Patient.PID.CardNO;

                //{80B9B163-A9C4-49d3-9BA0-453712CA7B95}
                //不再判断是否存在实体卡
                //System.Collections.Generic.List<FS.HISFC.Models.Account.AccountCard> cardList = accountMgr.GetMarkList(CardNO, "Account_CARD", "1");
                //if (cardList == null || cardList.Count < 1)
                //{
                //    MessageBox.Show("查询病人信息失败！");
                //    return;
                //}

                //this.AccountCardInfo = cardList[cardList.Count - 1];

                //this.PatientInfo = accountMgr.GetPatientInfoByCardNO(this.AccountCardInfo.Patient.PID.CardNO);

                //{80B9B163-A9C4-49d3-9BA0-453712CA7B95}
                if (string.IsNullOrEmpty(CardNO))
                {
                    MessageBox.Show("查询病人信息失败！");
                    return;
                }
                this.PatientInfo = accountMgr.GetPatientInfoByCardNO(CardNO);

                if (this.PatientInfo == null || string.IsNullOrEmpty(this.PatientInfo.PID.CardNO))
                {
                    MessageBox.Show("查询病人信息失败！");
                    return;
                }
            }
        }

        /// <summary>
        /// 当前选中行改变时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpPackage_SelectionChanged(object sender, FarPoint.Win.Spread.SelectionChangedEventArgs e)
        {
            if (this.fpPackage_Sheet1.ActiveRow == null)
            {
                this.fpPackageDetail_Sheet1.RowCount = 0;
                return;
            }
            FS.HISFC.Models.MedicalPackage.Fee.Package selectPackage = this.fpPackage_Sheet1.ActiveRow.Tag as FS.HISFC.Models.MedicalPackage.Fee.Package;
            this.SetPakcageDetail(selectPackage);
        }

        //{7F45177C-E974-4b8e-A2C0-8BEA82FF8ED8}
        /// <summary>
        /// 勾选框点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpPackage_ButtonClicked(object sender, EditorNotifyEventArgs e)
        {
            bool value = (bool)this.fpPackage_Sheet1.Cells[this.fpPackage_Sheet1.ActiveRow.Index, (int)PackageCols.Select].Value;
            FS.HISFC.Models.MedicalPackage.Fee.Package selectPackage = this.fpPackage_Sheet1.ActiveRow.Tag as FS.HISFC.Models.MedicalPackage.Fee.Package;

            ArrayList details = this.detailList[selectPackage.ID] as ArrayList;
            foreach (FS.HISFC.Models.MedicalPackage.Fee.PackageDetail detail in details)
            {
                if (value)
                {
                    detail.User01 = (detail.Item.Qty - detail.ConfirmQTY).ToString("F2");
                }
                else
                {
                    detail.User01 = "0";
                }
            }

            this.SetPakcageDetail(selectPackage);
            this.SetQuitCostInfo();

        }

        /// <summary>
        /// 单击cell
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void fpPackageDetail_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.fpPackageDetail.SetActiveCell(e.Row, (int)DetailCols.RTN);
        }

        /// <summary>
        /// 设置细项栏显示
        /// </summary>
        /// <param name="package"></param>
        private void SetPakcageDetail(FS.HISFC.Models.MedicalPackage.Fee.Package package)
        {
            this.fpPackageDetail_Sheet1.RowCount = 0;
            ArrayList details = this.detailList[package.ID] as ArrayList;
            foreach (FS.HISFC.Models.MedicalPackage.Fee.PackageDetail detail in details)
            {
                this.fpPackageDetail_Sheet1.AddRows(this.fpPackageDetail_Sheet1.RowCount, 1);
                this.fpPackageDetail_Sheet1.Rows[this.fpPackageDetail_Sheet1.RowCount - 1].Tag = detail;
                this.fpPackageDetail_Sheet1.Cells[this.fpPackageDetail_Sheet1.RowCount - 1, (int)DetailCols.Name].Text = detail.Item.Name + "[" + detail.Item.Specs + "]" + "*" + detail.Item.Qty.ToString() + detail.Unit;
                this.fpPackageDetail_Sheet1.Cells[this.fpPackageDetail_Sheet1.RowCount - 1, (int)DetailCols.Qty].Text = detail.Item.Qty.ToString();
                this.fpPackageDetail_Sheet1.Cells[this.fpPackageDetail_Sheet1.RowCount - 1, (int)DetailCols.AVARTN].Text = detail.RtnQTY.ToString();
                this.fpPackageDetail_Sheet1.Cells[this.fpPackageDetail_Sheet1.RowCount - 1, (int)DetailCols.RTN].Text = string.IsNullOrEmpty(detail.User01.ToString()) ? detail.RtnQTY.ToString() : detail.User01.ToString();
                this.fpPackageDetail_Sheet1.Cells[this.fpPackageDetail_Sheet1.RowCount - 1, (int)DetailCols.TotCost].Text = detail.Detail_Cost.ToString("F2");
                this.fpPackageDetail_Sheet1.Cells[this.fpPackageDetail_Sheet1.RowCount - 1, (int)DetailCols.RealCost].Text = detail.Real_Cost.ToString("F2");
                this.fpPackageDetail_Sheet1.Cells[this.fpPackageDetail_Sheet1.RowCount - 1, (int)DetailCols.GiftCost].Text = detail.Gift_cost.ToString("F2");
                this.fpPackageDetail_Sheet1.Cells[this.fpPackageDetail_Sheet1.RowCount - 1, (int)DetailCols.ETCCost].Text = detail.Etc_cost.ToString("F2");
            }
        }

        /// <summary>
        /// 当编辑发生改变的时候
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpPackageDetail_Change(object sender, ChangeEventArgs e)
        {
            this.delEvents();
            FS.HISFC.Models.MedicalPackage.Fee.PackageDetail detail = this.fpPackageDetail_Sheet1.ActiveRow.Tag as FS.HISFC.Models.MedicalPackage.Fee.PackageDetail;
            if (detail == null)
            {
                return;
            }
            try
            {
                try
                {
                    this.fpPackageDetail_Sheet1.ActiveCell.Value = Decimal.Parse(this.fpPackageDetail_Sheet1.ActiveCell.Value.ToString(), System.Globalization.NumberStyles.Integer);
                }
                catch
                {
                    throw new Exception("请输入整数");
                }

                if (Decimal.Parse(this.fpPackageDetail_Sheet1.ActiveCell.Value.ToString()) < 0)
                {
                    this.fpPackageDetail_Sheet1.ActiveCell.Value = Decimal.Parse(detail.User01);
                    throw new Exception("退费数量不能小于0");
                }

                if (this.fpPackageDetail_Sheet1.ActiveCell.Value == null)
                {
                    this.fpPackageDetail_Sheet1.ActiveCell.Value = 0;
                    detail.User01 = "0";
                }
                else
                {
                    if (detail.RtnQTY < Decimal.Parse(this.fpPackageDetail_Sheet1.ActiveCell.Value.ToString()))
                    {
                        this.fpPackageDetail_Sheet1.ActiveCell.Value = Decimal.Parse(detail.User01);
                        throw new Exception("退费数量大于可退费数量！");
                    }
                    else
                    {
                        detail.User01 = this.fpPackageDetail_Sheet1.ActiveCell.Value.ToString();
                    }
                }

                this.SetQuitCostInfo();

            }
            catch (Exception ex)
            {
                this.fpPackageDetail_Sheet1.ActiveCell.Value = Decimal.Parse(detail.User01);
                MessageBox.Show(ex.Message);
            }
            this.addEvents();
        }

        /// <summary>
        /// 按键处理
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            try
            {
                if (keyData == Keys.Enter)
                {
                    if (this.fpPackageDetail.ContainsFocus)
                    {
                        if (this.fpPackageDetail_Sheet1.ActiveRow != null)
                        {
                            if (this.fpPackageDetail_Sheet1.ActiveRow.Index < this.fpPackageDetail_Sheet1.RowCount - 1)
                            {
                                this.fpPackageDetail_Sheet1.SetActiveCell(this.fpPackageDetail_Sheet1.ActiveRow.Index + 1, (int)DetailCols.RTN);
                            }
                            else
                            {
                                this.fpPackageDetail.StopCellEditing();
                            }
                        }
                        return true;
                    }
                }
            }
            catch
            {
            }

            return base.ProcessDialogKey(keyData);
        }

        protected FS.FrameWork.WinForms.Forms.ToolBarService _toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
        /// <summary>
        /// 初始化工具栏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            _toolBarService.AddToolButton("确认退费", "确认退费", (int)FS.FrameWork.WinForms.Classes.EnumImageList.T退费, true, false, null);
            //{119F302E-69D9-445c-BF56-4109D975AD98}
            _toolBarService.AddToolButton("刷卡", "刷卡", (int)FS.FrameWork.WinForms.Classes.EnumImageList.B报警, true, false, null);
            return _toolBarService;
        }

        /// <summary>
        /// 菜单栏按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "确认退费":
                    this.ReturnFee();
                    break;
                //{892FDDD4-0CD2-4306-87E5-ACDEF6829C76}
                case "刷卡":
                    string cardNo = "";
                    string error = "";
                    if (FS.HISFC.Components.Registration.Function.OperMCard(ref cardNo, ref error) == -1)
                    {
                        MessageBox.Show("读卡失败：" + error, "提示");
                        return;
                    }
                    cardNo = ";" + cardNo;
                    this.invoiceTree.QueryByMCardNO(cardNo);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 确认退费
        /// </summary>
        public void ReturnFee()
        {
            //患者信息
            FS.HISFC.Models.RADT.PatientInfo PatientInfo = this.PatientInfo;

            if (PatientInfo == null)
            {
                MessageBox.Show("请先检索患者！");
                return;
            }

            bool isHaveRtnFee = false;

            //判断是否有退费项目
            foreach (string key in this.detailList.Keys)
            {
                ArrayList details = detailList[key] as ArrayList;

                foreach (FS.HISFC.Models.MedicalPackage.Fee.PackageDetail detail in details)
                {
                    try
                    {
                        decimal rtnNumb = decimal.Parse(detail.User01);

                        if (rtnNumb > 0)
                        {
                            isHaveRtnFee = true;
                            break;
                        }
                    }
                    catch
                    { }
                }
            }

            if (!isHaveRtnFee)
            {
                MessageBox.Show("没有需要退费的项目！");
                return;
            }

            string ErrInfo = string.Empty;

            decimal quitCostCouponAmount = 0.0m;
            decimal quitOperateCouponAmount = 0.0m;
            decimal refeeCostCouponAmount = 0.0m;
            decimal refeeOperateCouponAmount = 0.0m;
            //{3B9D0100-377E-48ac-AF31-4412CDCBF4B0}
            decimal refeeOperateDCouponAmount = 0.0m;
            string newInvoiceNO = "";

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在保存。。。");
            if (this.saveQuit(this.PatientInfo,
                                                this.packageList,
                                                this.detailList,
                                                this.payInfoList,
                                                this.invoiceInfo,
                                                this.cmbPayModes.Tag.ToString(),
                                                this.refeePackageList,
                                                this.refeedetailList,
                                                this.hsCostInfo,
                                                this.hsPayInfo,
                                                ref ErrInfo,
                                                ref quitCostCouponAmount,
                                                ref quitOperateCouponAmount,
                                                ref refeeCostCouponAmount,
                                                ref refeeOperateCouponAmount,
                                                ref refeeOperateDCouponAmount,
                                                ref newInvoiceNO) < 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                //FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show(ErrInfo);
                return;
            }

            #region 处理积分数据

            if (IsCouponModuleInUse)
            {
                //{333D2AD8-DC4A-4c30-A14E-D6815AC858F9}
                string resultCode = "0";
                string errorMsg = "";
                int reqRtn = -1;

                //{77F4F0F1-F2F0-45b1-9EE3-B39EE3C252C7}
                //对【原交易积分处理】和【重新收费积分处理】的顺序进行了调整
                //防止原交易产生了10000的积分，患者消费了2000剩余 8000积分，此时产生半退需退款 3000
                //此时应退款3000，剩余积分4000；
                //原先的逻辑是 退款 10000时需用户先支付2000的积分金额后，再收款7000，产生积分7000
                //重新收费产生的积分   

                //{c2d43b9d-eda4-4f87-92c1-f6fdb08d9d04}取多倍积分
                if (refeeOperateCouponAmount != 0)
                {
                    //{3B9D0100-377E-48ac-AF31-4412CDCBF4B0}
                    reqRtn = FS.HISFC.BizProcess.Integrate.WSHelper.OperateCoupon(PatientInfo.PID.CardNO, PatientInfo.Name, "TCSF", newInvoiceNO, refeeOperateCouponAmount, refeeOperateDCouponAmount, invoiceInfo.InvoiceNO, "", out resultCode, out errorMsg);
                    if (reqRtn < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        MessageBox.Show("【产生重新收费积分】重新收费积分出错：" + errorMsg);
                        return;
                    }
                }

                //重新收费消费的积分
                if (refeeCostCouponAmount != 0)
                {
                    reqRtn = FS.HISFC.BizProcess.Integrate.WSHelper.CostCoupon(PatientInfo.PID.CardNO, PatientInfo.Name, PatientInfo.PID.CardNO, PatientInfo.Name, "TCSF", newInvoiceNO, refeeCostCouponAmount, 0.0m, out resultCode, out errorMsg);
                    if (reqRtn < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        MessageBox.Show("【1、扣除重新收费消费积分】扣除重新收费时的消费积分出错：" + errorMsg);

                        //回滚重新收费产生的积分
                        if (refeeOperateCouponAmount != 0)
                        {
                            reqRtn = FS.HISFC.BizProcess.Integrate.WSHelper.OperateCoupon(PatientInfo.PID.CardNO, PatientInfo.Name, "TCTF", newInvoiceNO, -refeeOperateCouponAmount, 0.0m, out resultCode, out errorMsg);
                            if (reqRtn < 0)
                            {
                                MessageBox.Show("【1、扣除重新收费消费积分】回滚重新收费产生的积分出错，请联系信息科处理，错误详情:" + errorMsg);
                            }
                        }

                        return;
                    }
                }

                //退回原交易消费的积分
                if (quitCostCouponAmount != 0)
                {
                    reqRtn = FS.HISFC.BizProcess.Integrate.WSHelper.CostCoupon(PatientInfo.PID.CardNO, PatientInfo.Name, PatientInfo.PID.CardNO, PatientInfo.Name, "TCTF", invoiceInfo.InvoiceNO, quitCostCouponAmount, 0.0m, out resultCode, out errorMsg);
                    if (reqRtn < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        MessageBox.Show("【2、退回原交易消费的积分】退回原交易消费的积分出错：" + errorMsg);

                        //回滚重新收费产生的积分
                        if (refeeOperateCouponAmount != 0)
                        {
                            reqRtn = FS.HISFC.BizProcess.Integrate.WSHelper.OperateCoupon(PatientInfo.PID.CardNO, PatientInfo.Name, "TCTF", newInvoiceNO, -refeeOperateCouponAmount, 0.0m, out resultCode, out errorMsg);
                            if (reqRtn < 0)
                            {
                                MessageBox.Show("【2、退回原交易消费的积分】回滚重新收费产生的积分出错，请联系信息科处理，错误详情:" + errorMsg);
                            }
                        }

                        //回滚重新收费消费的积分
                        if (refeeCostCouponAmount != 0)
                        {
                            reqRtn = FS.HISFC.BizProcess.Integrate.WSHelper.CostCoupon(PatientInfo.PID.CardNO, PatientInfo.Name, PatientInfo.PID.CardNO, PatientInfo.Name, "TCTF", newInvoiceNO, -refeeCostCouponAmount, 0.0m, out resultCode, out errorMsg);
                            if (reqRtn < 0)
                            {
                                MessageBox.Show("【2、退回原交易消费的积分】回滚重新收费消费的积分出错，请联系信息科处理，错误详情:" + errorMsg);
                            }
                        }

                        return;
                    }
                }

                //扣除原交易产生的积分
                //{416C9A04-3A94-4d41-8F0A-14B73622C65E}
                //{3B9D0100-377E-48ac-AF31-4412CDCBF4B0}
                //退费时，无论是否有积分需要扣除，都需要调用，因为涉及到老带新与代理人积分，需要通过这部分信息去进行退积分
                //if (quitOperateCouponAmount != 0)

                ////{c2d43b9d-eda4-4f87-92c1-f6fdb08d9d04}取多倍积分
                {
                    reqRtn = FS.HISFC.BizProcess.Integrate.WSHelper.OperateCoupon(PatientInfo.PID.CardNO, PatientInfo.Name, "TCTF", invoiceInfo.InvoiceNO, quitOperateCouponAmount, 0.0m, "", "1", out resultCode, out errorMsg);
                    if (reqRtn < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        MessageBox.Show("【3、扣除原交易产生的积分】扣除原交易产生的积分出错：" + errorMsg);

                        //回滚重新收费产生的积分
                        if (refeeOperateCouponAmount != 0)
                        {
                            reqRtn = FS.HISFC.BizProcess.Integrate.WSHelper.OperateCoupon(PatientInfo.PID.CardNO, PatientInfo.Name, "TCTF", newInvoiceNO, -refeeOperateCouponAmount, 0.0m, out resultCode, out errorMsg);
                            if (reqRtn < 0)
                            {
                                MessageBox.Show("【3、扣除原交易产生的积分】回滚重新收费产生的积分出错，请联系信息科处理，错误详情:" + errorMsg);
                            }
                        }

                        //回滚重新收费消费的积分
                        if (refeeCostCouponAmount != 0)
                        {
                            reqRtn = FS.HISFC.BizProcess.Integrate.WSHelper.CostCoupon(PatientInfo.PID.CardNO, PatientInfo.Name, PatientInfo.PID.CardNO, PatientInfo.Name, "TCTF", newInvoiceNO, -refeeCostCouponAmount, 0.0m, out resultCode, out errorMsg);
                            if (reqRtn < 0)
                            {
                                MessageBox.Show("【3、扣除原交易产生的积分】回滚重新收费消费的积分出错，请联系信息科处理，错误详情:" + errorMsg);
                            }
                        }

                        //回滚原交易消费的积分
                        if (quitCostCouponAmount != 0)
                        {
                            reqRtn = FS.HISFC.BizProcess.Integrate.WSHelper.CostCoupon(PatientInfo.PID.CardNO, PatientInfo.Name, PatientInfo.PID.CardNO, PatientInfo.Name, "TCSF", invoiceInfo.InvoiceNO, -quitCostCouponAmount, 0.0m, out resultCode, out errorMsg);

                            if (reqRtn < 0)
                            {
                                MessageBox.Show("【3、扣除原交易产生的积分】回滚退回原交易消费的积分出错，请联系信息科处理，错误详情:" + errorMsg);
                            }
                        }

                        return;
                    }
                }
            }


            #endregion



            FS.FrameWork.Management.PublicTrans.Commit();
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            #region 会员系统套餐核销相关

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在处理套餐核销。。。");
            FS.HISFC.Models.Base.Employee empl = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
            FS.HISFC.Models.Base.Department dept = empl.Dept as FS.HISFC.Models.Base.Department;

            string hospitalCode = string.Empty;

            if (dept.HospitalName.Contains("顺德"))
            {
                hospitalCode = "IBORNSD";
            }
            else
            {
                hospitalCode = "IBORNGZ";
            }

            string url = FS.HISFC.BizProcess.Integrate.WSHelper.GetUrl(hospitalCode, FS.HISFC.BizProcess.Integrate.URLTYPE.HIS);


            //先作废原本套餐项目的执行记录
            string reqCancel = @"<req>
                                <invoiceNo>{0}</invoiceNo>
                            </req>";
            reqCancel = string.Format(reqCancel, invoiceInfo.InvoiceNO);
            //string resCancel = FS.HISFC.BizProcess.Integrate.WSHelper.InvokeWebService("http://localhost:8080/ibornMobileService.asmx", "CancelCrmPackageDeal", new string[] { reqCancel }) as string;
            string resCancel = FS.HISFC.BizProcess.Integrate.WSHelper.InvokeWebService(url, "CancelCrmPackageDeal", new string[] { reqCancel }) as string;

            //再将重收的，新的这个发票添加到套餐执行记录
            if (this.IsPackageDealInCrm && !string.IsNullOrEmpty(newInvoiceNO))
            {
                string req = @"
                        <req>
                          <crmId>{0}</crmId>
                          <invoiceNo>{1}</invoiceNo>
                        </req>";
                try
                {
                    //{14B9C3EE-70B6-46df-B279-B8F3487519C4}

                    req = string.Format(req, PatientInfo.CrmID, newInvoiceNO);

                    FS.HISFC.BizProcess.Integrate.IbornMobileService server = new FS.HISFC.BizProcess.Integrate.IbornMobileService();
                    //server.Url = url;
                    //server.Url = "http://localhost:8080/ibornMobileService.asmx";
                    //string res = server.PackageExeceteInCrm(req);

                    //{27FF6780-BD63-4b8f-B7F0-38623EF0BB46}
                    //换成传统的调用方式
                    string res = FS.HISFC.BizProcess.Integrate.WSHelper.InvokeWebService(url, "PackageExeceteInCrm", new string[] { req }) as string;
                    System.Xml.XmlDocument docResult = new System.Xml.XmlDocument();
                    docResult.LoadXml(res);
                    try
                    {
                        string resultCode = docResult.SelectSingleNode("/res/res/resultCode") != null ? docResult.SelectSingleNode("/res/res/resultCode").InnerText : "";
                        string resultDesc = docResult.SelectSingleNode("/res/res/resultDesc") != null ? docResult.SelectSingleNode("/res/res/resultDesc").InnerText : "";

                        if (resultCode == "0")
                        {
                            //MessageBox.Show("CRM分解套餐成功！");
                        }
                        else
                        {
                            MessageBox.Show("CRM分解套餐失败，请在会员系统手动分解，或联系信息部！\n" + resultDesc);
                        }
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show("同步crm套餐项目异常！" + e.Message);
                }
            }

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            #endregion

            string QuitMessage = "退费成功！";


            //string QuitMessage = "退费成功，请按如下提示退费：\n";


            //if (this.cmbPayModes.Tag.ToString() == "ADYYSFH")
            //{
            //    foreach (string key in this.hsPayInfo.Keys)
            //    {
            //        //账户或者四舍五入不提示
            //        if (key == "YS" || key == "DC" || key == "SW" || key == "RC")
            //        {
            //            continue;
            //        }

            //        foreach (FS.HISFC.Models.Base.Const paymode in this.helpPayMode.ArrayObject)
            //        {
            //            if (paymode.ID == key)
            //            {
            //                QuitMessage += paymode.Name;
            //                QuitMessage += "------------------------￥";
            //                QuitMessage += ((decimal)hsPayInfo[key]).ToString("F2");
            //                QuitMessage += "\n";
            //                break;
            //            }
            //        }
            //    }
            //}
            //else
            //{
            //    decimal totRtn = 0.0m;
            //    foreach (string key in this.hsPayInfo.Keys)
            //    {
            //        //账户或者四舍五入不提示
            //        if (key == "YS" || key == "DC" || key == "SW" || key == "RC")
            //        {
            //            continue;
            //        }
            //        totRtn += (decimal)this.hsPayInfo[key];
            //    }

            //    foreach (FS.HISFC.Models.Base.Const paymode in this.helpPayMode.ArrayObject)
            //    {
            //        if (paymode.ID == this.cmbPayModes.Tag.ToString())
            //        {
            //            QuitMessage += paymode.Name;
            //            QuitMessage += "------------------------￥";
            //            QuitMessage += totRtn.ToString("F2");
            //            QuitMessage += "; ";
            //            break;
            //        }
            //    }
            //}

            MessageBox.Show(QuitMessage, "退费提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.PatientInfo = null;
            this.invoiceTree.Clear();
        }




        /// <summary>
        /// 套餐退费
        /// </summary>
        /// <param name="PatientInfo">患者信息</param>
        /// <param name="QuitFee">退费套餐</param>
        /// <param name="HSQuitDetail">退费明细</param>
        /// <param name="QuitPayModeList">退费支付方式</param>
        /// <param name="invoiceInfo">发票信息</param>
        /// <param name="QuitWay">退费途径</param>
        /// <param name="QuitWay">费用类别</param>
        /// <param name="ErrInfo">错误信息</param>
        /// <returns></returns>
        public int saveQuit(FS.HISFC.Models.RADT.PatientInfo PatientInfo,
                            ArrayList QuitFee,
                            Hashtable HSQuitDetail,
                            ArrayList QuitPayModeList,
                            FS.HISFC.Models.MedicalPackage.Fee.Invoice invoiceInfo,
                            string QuitWay,
                            ArrayList ReFeePackage,
                            Hashtable HSReFeeDetail,
                            Hashtable HSCostInfo,
                            Hashtable HSPayInfo,
                            ref string ErrInfo,
                            ref decimal quitCostCouponAmount,
                            ref decimal quitOperateCouponAmount,
                            ref decimal refeeCostCouponAmount,
                            ref decimal refeeOperateCouponAmount,
                            ref decimal refeeOperateDCouponAmount,
                            ref string newInvoiceNO)
        {
            quitCostCouponAmount = 0.0m;
            quitOperateCouponAmount = 0.0m;
            refeeCostCouponAmount = 0.0m;
            refeeOperateCouponAmount = 0.0m;
            //{3B9D0100-377E-48ac-AF31-4412CDCBF4B0}
            refeeOperateDCouponAmount = 0.0m;

            //{E8D52AF4-08BF-489e-A303-1587BEDAEB72}
            FS.HISFC.BizLogic.Manager.Constant constManager = new FS.HISFC.BizLogic.Manager.Constant();
            FS.FrameWork.Models.NeuObject unCouponPackage = constManager.GetConstant("UNCOUPONPACKAGE", "1");
            bool isQuitIncludeUncouponPackage = false;  //退费套餐中是否存在代收套餐
            bool isRefeeIncludeUncouponPacage = false;  //重新收费的套餐中是否存在代收套餐


            FS.FrameWork.Models.NeuObject unCouponPackageDLR = constManager.GetConstant("UNCOUPONPACKAGEDLR", "1");
            bool isRefeeIncludeUncouponPacageDLR = false;  //重新收费的套餐中是否存在不计代理人积分套餐


            FS.HISFC.Models.Base.Employee employee = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee; //{CB0CA2C8-1DAE-4a6a-B5F8-DCC227A7AF90}
            FS.HISFC.Models.Base.Department dept2 = employee.Dept as FS.HISFC.Models.Base.Department;
            string hospitalid = dept2.HospitalID;
            string hospitalname = dept2.HospitalName;
            #region 处理套餐主表和明细表

            foreach (FS.HISFC.Models.MedicalPackage.Fee.Package package in QuitFee)
            {
                //{E8D52AF4-08BF-489e-A303-1587BEDAEB72}
                if (unCouponPackage.Name.Contains("|" + package.PackageInfo.PackageType + "|"))
                {
                    isQuitIncludeUncouponPackage = true;
                }

                FS.HISFC.Models.MedicalPackage.Fee.Package tmppackage = this.packageMgr.QueryByID(package.ID);
                if (tmppackage == null || tmppackage.Cancel_Flag != "0")
                {
                    ErrInfo = "费用已发生改变，请刷新后重试！";
                    return -1;
                }

                tmppackage.Cancel_Flag = "1";
                tmppackage.CancelOper = FS.FrameWork.Management.Connection.Operator.ID;
                tmppackage.CancelTime = this.packageMgr.GetDateTimeFromSysDateTime();

                //更新旧记录
                if (this.packageMgr.Update(tmppackage) < 1)
                {
                    ErrInfo = this.packageMgr.Err;
                    return -1;
                }

                //插入负记录
                FS.HISFC.Models.MedicalPackage.Fee.Package newPackage = tmppackage.Clone();
                newPackage.Trans_Type = "2";
                newPackage.Package_Cost = -package.Package_Cost;
                newPackage.Real_Cost = -package.Real_Cost;
                newPackage.Gift_cost = -package.Gift_cost;
                newPackage.Etc_cost = -package.Etc_cost;
                newPackage.Cancel_Flag = "1";
                newPackage.CancelOper = FS.FrameWork.Management.Connection.Operator.ID;
                newPackage.CancelTime = this.packageMgr.GetDateTimeFromSysDateTime();
                //{030CEF69-9D8D-4a12-8158-1AB14920B7D5}{CB0CA2C8-1DAE-4a6a-B5F8-DCC227A7AF90}
                newPackage.Oper = FS.FrameWork.Management.Connection.Operator.ID;
                newPackage.OperTime = this.packageMgr.GetDateTimeFromSysDateTime();


                newPackage.HospitalID = hospitalid;
                newPackage.HospitalName = hospitalname;
                if (this.packageMgr.Insert(newPackage) < 1)
                {
                    ErrInfo = this.packageMgr.Err;
                    return -1;
                }


                ArrayList detailList = HSQuitDetail[package.ID] as ArrayList;

                if (detailList == null)
                {
                    ErrInfo = "套餐明细为空！";
                    return -1;
                }

                foreach (FS.HISFC.Models.MedicalPackage.Fee.PackageDetail detail in detailList)
                {

                    FS.HISFC.Models.MedicalPackage.Fee.PackageDetail tmpdetail = this.packageDetailMgr.QueryDetailByClinicSeq(detail.ID, "1", detail.SequenceNO);
                    if (tmpdetail == null || tmpdetail.Cancel_Flag != "0")
                    {
                        ErrInfo = "费用已发生改变，请刷新后重试！";
                        return -1;
                    }
                    if (detail.RtnQTY != 0 && detail.RtnQTY != tmpdetail.RtnQTY)
                    {
                        ErrInfo = "可退数量发生变化，请刷新后重试！"; //{F5DF63A0-BC16-4bb3-85FB-42DE03CFD906}
                        return -1;
                    }

                    tmpdetail.Cancel_Flag = "1";
                    tmpdetail.CancelOper = FS.FrameWork.Management.Connection.Operator.ID;
                    tmpdetail.CancelTime = this.packageMgr.GetDateTimeFromSysDateTime();

                    //更新旧记录
                    if (this.packageDetailMgr.Update(tmpdetail) < 1)
                    {
                        ErrInfo = this.packageDetailMgr.Err;
                        return -1;
                    }

                    FS.HISFC.Models.MedicalPackage.Fee.PackageDetail newPackageDetail = tmpdetail.Clone();
                    newPackageDetail.Trans_Type = "2";
                    newPackageDetail.Detail_Cost = -detail.Detail_Cost;
                    newPackageDetail.Real_Cost = -detail.Real_Cost;
                    newPackageDetail.Gift_cost = -detail.Gift_cost;
                    newPackageDetail.Etc_cost = -detail.Etc_cost;
                    newPackageDetail.Item.Qty = -detail.Item.Qty;
                    newPackageDetail.Cancel_Flag = "1";
                    newPackageDetail.CancelOper = FS.FrameWork.Management.Connection.Operator.ID;
                    newPackageDetail.CancelTime = this.packageMgr.GetDateTimeFromSysDateTime();
                    //{030CEF69-9D8D-4a12-8158-1AB14920B7D5}{CB0CA2C8-1DAE-4a6a-B5F8-DCC227A7AF90}
                    newPackageDetail.Oper = FS.FrameWork.Management.Connection.Operator.ID;
                    newPackageDetail.OperTime = this.packageMgr.GetDateTimeFromSysDateTime();
                    newPackageDetail.HospitalID = hospitalid;
                    newPackageDetail.HospitalName = hospitalname;
                    //插入负记录
                    if (this.packageDetailMgr.Insert(newPackageDetail) < 1)
                    {
                        ErrInfo = this.packageDetailMgr.Err;
                        return -1;
                    }
                }

            }

            #endregion

            #region 处理退费发票

            FS.HISFC.Models.MedicalPackage.Fee.Invoice tmpInvoice = this.invoiceMgr.QueryByInvoiceNO(invoiceInfo.InvoiceNO, "1");
            if (tmpInvoice == null || tmpInvoice.Cancel_Flag != "0")
            {
                ErrInfo = "费用已发生改变，请刷新后重试！";
                return -1;
            }

            tmpInvoice.Cancel_Flag = "1";
            tmpInvoice.CancelOper = FS.FrameWork.Management.Connection.Operator.ID;
            tmpInvoice.CancelTime = this.packageMgr.GetDateTimeFromSysDateTime();

            //更新旧记录
            if (this.invoiceMgr.Update(tmpInvoice) < 1)
            {
                ErrInfo = this.invoiceMgr.Err;
                return -1;
            }

            FS.HISFC.Models.MedicalPackage.Fee.Invoice newInvoice = tmpInvoice.Clone(false);
            newInvoice.Trans_Type = "2";
            newInvoice.Package_Cost = -invoiceInfo.Package_Cost;
            newInvoice.Real_Cost = -invoiceInfo.Real_Cost;
            newInvoice.Gift_cost = -invoiceInfo.Gift_cost;
            newInvoice.Etc_cost = -invoiceInfo.Etc_cost;
            newInvoice.Cancel_Flag = "1";
            newInvoice.CancelOper = FS.FrameWork.Management.Connection.Operator.ID;
            newInvoice.CancelTime = this.packageMgr.GetDateTimeFromSysDateTime();
            //{030CEF69-9D8D-4a12-8158-1AB14920B7D5}
            newInvoice.Oper = FS.FrameWork.Management.Connection.Operator.ID;
            newInvoice.OperTime = this.packageMgr.GetDateTimeFromSysDateTime();
            newInvoice.Hospital_ID = hospitalid;
            newInvoice.HospitalName = hospitalname;
            //插入负记录
            if (this.invoiceMgr.Insert(newInvoice) < 1)
            {
                ErrInfo = this.invoiceMgr.Err;
                return -1;
            }

            #endregion

            #region 处理支付方式

            decimal couponVacancy = 0.0m;
            decimal couponNum = 0.0m;
            if (this.GetCouponVacancy(ref couponVacancy) < 0)
            {
                ErrInfo = "获取患者积分余额失败";
                return -1;
            }

            if (this.GetCouponNum(invoiceInfo.InvoiceNO, ref couponNum) < 0)
            {
                ErrInfo = "获取患者积分倍数失败";
                return -1;
            }

            //{F166B18B-62E3-4835-A729-4CA384F9ADEE}
            FS.FrameWork.Models.NeuObject cashCouponPayMode = constManager.GetConstant("XJLZFFS", "1");


            //{3B9D0100-377E-48ac-AF31-4412CDCBF4B0}
            FS.FrameWork.Models.NeuObject dlrCouponPayMode = constManager.GetConstant("DLRZFFS", "1");

            //decimal cashCouponAmount = 0.0m;

            FS.HISFC.BizProcess.Integrate.Account.AccountPay accountPay = new FS.HISFC.BizProcess.Integrate.Account.AccountPay();

            ArrayList QuitPayModeListForChoose = new ArrayList();
            foreach (FS.HISFC.Models.MedicalPackage.Fee.PayMode pay in QuitPayModeList)
            {
                FS.HISFC.Models.MedicalPackage.Fee.PayMode tmppay = new FS.HISFC.Models.MedicalPackage.Fee.PayMode();
                tmppay = this.paymodeMgr.QueryByInvoiceSeq(pay.InvoiceNO, pay.Trans_Type, pay.SequenceNO, pay.Cancel_Flag);
                if (tmppay == null || tmppay.Cancel_Flag != "0")
                {
                    ErrInfo = "费用已发生改变，请刷新后重试！";
                    return -1;
                }

                //{333D2AD8-DC4A-4c30-A14E-D6815AC858F9}
                if (pay.Mode_Code == "CO")
                {
                    quitCostCouponAmount -= pay.Tot_cost;
                }

                if (cashCouponPayMode.Name.Contains(pay.Mode_Code.ToString()) || (pay.Mode_Code == "DE" && cashCouponPayMode.Name.Contains(pay.Related_ModeCode.ToString())))
                {
                    //cashCouponAmount -= pay.Tot_cost;
                    quitOperateCouponAmount -= pay.Tot_cost;
                }

                tmppay.CancelOper = FS.FrameWork.Management.Connection.Operator.ID;
                tmppay.CancelTime = this.packageMgr.GetDateTimeFromSysDateTime();
                tmppay.Cancel_Flag = "1";

                //更新旧记录
                if (this.paymodeMgr.Update(tmppay) < 1)
                {
                    ErrInfo = this.invoiceMgr.Err;
                    return -1;
                }

                FS.HISFC.Models.MedicalPackage.Fee.PayMode newpaymode = tmppay.Clone(false);
                newpaymode.Trans_Type = "2";
                newpaymode.Real_Cost = -pay.Real_Cost;
                newpaymode.Tot_cost = -pay.Tot_cost;
                newpaymode.CancelOper = FS.FrameWork.Management.Connection.Operator.ID;
                newpaymode.CancelTime = this.packageMgr.GetDateTimeFromSysDateTime();
                newpaymode.Cancel_Flag = "1";
                //{030CEF69-9D8D-4a12-8158-1AB14920B7D5}{CB0CA2C8-1DAE-4a6a-B5F8-DCC227A7AF90}
                newpaymode.Oper = FS.FrameWork.Management.Connection.Operator.ID;
                newpaymode.OperTime = this.packageMgr.GetDateTimeFromSysDateTime();
                newpaymode.Hospital_ID = hospitalid;
                newpaymode.HospitalName = hospitalname;
                //押金转化为充值方式退费
                if (newpaymode.Mode_Code == "DE")
                {
                    newpaymode.Mode_Code = pay.Related_ModeCode;
                }

                QuitPayModeListForChoose.Add(newpaymode);

                /*
                //账户,优惠金额以及四舍五入以外的支付方式选择用指定退费方式进行退费
                if (newpaymode.Mode_Code != "RC" &&
                    newpaymode.Mode_Code != "YS" &&
                    newpaymode.Mode_Code != "DC" &&
                    QuitWay != "ADYYSFH" &&
                    newpaymode.Mode_Code != "SW")
                {
                    newpaymode.Mode_Code = QuitWay;
                }

                //插入负记录
                if (this.paymodeMgr.Insert(newpaymode) < 1)
                {
                    ErrInfo = this.invoiceMgr.Err;
                    return -1;
                }
                 */
            }


            //{E8D52AF4-08BF-489e-A303-1587BEDAEB72}
            if (isQuitIncludeUncouponPackage)
            {
                quitOperateCouponAmount = 0.0m;
            }

            //{77F4F0F1-F2F0-45b1-9EE3-B39EE3C252C7}
            //是否用退款金额支付缺少的积分
            bool isPayCouponWithMoney = true;

            //将消费的积分金额转化为积分数量
            decimal quitCostCouponAmountToCoupon = quitCostCouponAmount * 100;
            decimal refeeCoupon = ((decimal)HSCostInfo["REREAL"]);

            //{C1F0DDD7-B651-47f1-8CB7-61467C9B3A5F}
            decimal shouldPayCoupon = 0.0m;

            //退费时积分要加上倍数 {c2d43b9d-eda4-4f87-92c1-f6fdb08d9d04}取多倍积分
            quitOperateCouponAmount = quitOperateCouponAmount * couponNum;


            if (IsCouponModuleInUse)
            {

                //在未使用积分的情况下不作判断积分够不够扣（存在家庭积分为负数的情况）
                if (refeeCoupon == 0 && quitCostCouponAmountToCoupon == 0 && quitOperateCouponAmount == 0)
                {
                    isPayCouponWithMoney = false;
                }
                else
                {
                    if (couponVacancy + refeeCoupon + (-quitCostCouponAmountToCoupon) - (-quitOperateCouponAmount) < 0)
                    {
                        if (MessageBox.Show("家庭账户积分不够，是否扣款进行抵扣？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            shouldPayCoupon = Math.Floor((-quitOperateCouponAmount) - couponVacancy - (-quitCostCouponAmountToCoupon) - refeeCoupon) / 100;
                            quitOperateCouponAmount = -(-quitCostCouponAmountToCoupon) - couponVacancy - refeeCoupon;
                            isPayCouponWithMoney = true;
                        }
                        else
                        {
                            isPayCouponWithMoney = false;
                        }
                    }
                }


            }

            ArrayList QuitPayModeListForChooseHandle = new ArrayList();
            int paySquence = 100;
            foreach (FS.HISFC.Models.MedicalPackage.Fee.PayMode pay in QuitPayModeListForChoose)
            {
                FS.HISFC.Models.MedicalPackage.Fee.PayMode payObj = pay.Clone(false);

                if (shouldPayCoupon > 0)
                {
                    if (cashCouponPayMode.Name.Contains(payObj.Mode_Code.ToString()) || (payObj.Mode_Code == "DE" && cashCouponPayMode.Name.Contains(payObj.Related_ModeCode.ToString())))
                    {
                        if (shouldPayCoupon >= (-payObj.Tot_cost))
                        {
                            shouldPayCoupon -= (-payObj.Tot_cost);
                            payObj.Mode_Code = "TCO";
                        }
                        else
                        {
                            paySquence--;
                            FS.HISFC.Models.MedicalPackage.Fee.PayMode couponPay = new FS.HISFC.Models.MedicalPackage.Fee.PayMode();
                            couponPay = payObj.Clone(false);
                            couponPay.SequenceNO = paySquence.ToString();
                            couponPay.Tot_cost = -shouldPayCoupon;
                            couponPay.Real_Cost = -shouldPayCoupon;
                            couponPay.Mode_Code = "TCO";

                            payObj.Tot_cost += shouldPayCoupon;
                            payObj.Real_Cost += shouldPayCoupon;

                            shouldPayCoupon = 0;
                            QuitPayModeListForChooseHandle.Add(couponPay);
                        }
                    }
                }

                QuitPayModeListForChooseHandle.Add(payObj);
            }

            if (shouldPayCoupon > 0)
            {
                ErrInfo = "患者没有足够的退款金额支付所消耗的积分!缺少积分数量：" + (shouldPayCoupon * 100).ToString();
                return -1;
            }

            Forms.frmChooseBalancePay frmTemp = new Forms.frmChooseBalancePay();
            frmTemp.IsLockPayMode = true;
            frmTemp.Init();
            frmTemp.QuitPayModes = QuitPayModeListForChooseHandle;
            frmTemp.InitQuitPayModes();
            frmTemp.StartPosition = FormStartPosition.CenterScreen;
            frmTemp.ShowDialog();

            if (frmTemp.IsSelect == false)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("没有选择退费的支付方式，请重新退费!");
                return -1;
            }

            ArrayList choosedPayMode = frmTemp.ModifiedPayModes;

            foreach (FS.HISFC.Models.MedicalPackage.Fee.PayMode newpay in choosedPayMode)
            {
                if (this.paymodeMgr.Insert(newpay) < 1)
                {
                    ErrInfo = this.invoiceMgr.Err;
                    return -1;
                }

                #region 账户新增(账户冲掉扣费金额)

                //账户退费
                if (newpay.Mode_Code == "YS" || (newpay.Mode_Code == "DE" && newpay.Related_ModeCode == "YS"))
                {
                    if (accountPay.OutpatientPay(PatientInfo,
                                                 newpay.Account,
                                                 newpay.AccountType,
                                                 Math.Abs(newpay.Tot_cost),
                                                 0,
                                                 newpay.InvoiceNO, PatientInfo,
                                                 FS.HISFC.Models.Account.PayWayTypes.M,
                                                 0) < 1)
                    {
                        //{2694417D-715F-4ef6-A664-1F92399DC325}
                        ErrInfo = "账户退费失败！" + accountPay.Err;
                        return -1;
                    }
                }

                //账户退费
                if (newpay.Mode_Code == "DC" || (newpay.Mode_Code == "DE" && newpay.Related_ModeCode == "DC"))
                {
                    if (accountPay.OutpatientPay(PatientInfo,
                                                 newpay.Account,
                                                 newpay.AccountType,
                                                 0,
                                                 Math.Abs(newpay.Tot_cost),
                                                 newpay.InvoiceNO, PatientInfo,
                                                 FS.HISFC.Models.Account.PayWayTypes.M,
                                                 0) < 1)
                    {
                        //{2694417D-715F-4ef6-A664-1F92399DC325}
                        ErrInfo = "账户退费失败！" + accountPay.Err;
                        return -1;
                    }
                }

                #endregion

            }

            //{F166B18B-62E3-4835-A729-4CA384F9ADEE}
            //if (cashCouponAmount > 0 || cashCouponAmount < 0)
            //{
            //    FS.HISFC.BizProcess.Integrate.Account.CashCoupon cashCouponPrc = new FS.HISFC.BizProcess.Integrate.Account.CashCoupon();
            //    string errText1 = string.Empty;
            //    if (cashCouponPrc.CashCouponSave("TCTF", PatientInfo.PID.CardNO, invoiceInfo.InvoiceNO, cashCouponAmount, ref errText1) <= 0)
            //    {
            //        ErrInfo = "计算现金流积分出错!" + errText1;
            //        return -1;
            //    }

            //}

            #endregion

            //不存在重新收费的项目时 直接返回
            if (ReFeePackage == null || ReFeePackage.Count == 0)
            {
                return 1;
            }

            //获取发票号
            string invoiceNO = string.Empty;
            string realInvoiceNO = string.Empty;
            string errText = string.Empty;
            FS.HISFC.Models.Base.Employee oper = this.invoiceMgr.Operator as FS.HISFC.Models.Base.Employee;
            FS.HISFC.Models.Base.Department dept = oper.Dept as FS.HISFC.Models.Base.Department;
            if (this.feeIntegrate.GetInvoiceNO(oper, "M", ref invoiceNO, ref realInvoiceNO, ref errText) == -1)
            {
                ErrInfo = errText;
                return -1;
            }
            newInvoiceNO = invoiceNO;
            string InvoiceSeq = this.invoiceMgr.GetNewInvoiceSeq();

            decimal reTot = (decimal)HSCostInfo["RETOT"];
            decimal reReal = (decimal)HSCostInfo["REREAL"];
            decimal reGift = (decimal)HSCostInfo["REGIFT"];
            decimal reEtc = (decimal)HSCostInfo["REETC"];


            #region 处理新的套餐主表和明细表

            int packageSeq = 1;
            foreach (FS.HISFC.Models.MedicalPackage.Fee.Package repackage in ReFeePackage)
            {
                //{E8D52AF4-08BF-489e-A303-1587BEDAEB72}
                if (unCouponPackage.Name.Contains("|" + repackage.PackageInfo.PackageType + "|"))
                {
                    isRefeeIncludeUncouponPacage = true;
                }

                //{3B9D0100-377E-48ac-AF31-4412CDCBF4B0}
                if (unCouponPackageDLR.Name.Contains("|" + repackage.PackageInfo.PackageType + "|") || repackage.SpecialFlag == "1")
                {
                    isRefeeIncludeUncouponPacageDLR = true;
                }

                //划价人，划价日期，收费人,收费日期,发票号，发票序列号
                FS.HISFC.Models.MedicalPackage.Fee.Package newpackage = repackage.Clone();
                newpackage.ID = this.packageMgr.GetNewClinicNO();
                newpackage.InvoiceNO = invoiceNO;
                newpackage.Invoiceseq = InvoiceSeq;
                newpackage.DelimitOper = oper.ID;
                newpackage.DelimitTime = this.packageMgr.GetDateTimeFromSysDateTime();
                newpackage.Oper = oper.ID;
                newpackage.OperTime = this.packageMgr.GetDateTimeFromSysDateTime();
                newpackage.Pay_Flag = "1";
                newpackage.Cancel_Flag = "0";
                newpackage.SequenceNO = packageSeq.ToString();
                newpackage.HospitalID = hospitalid;
                newpackage.HospitalName = hospitalname;//{CB0CA2C8-1DAE-4a6a-B5F8-DCC227A7AF90}
                if (this.packageMgr.Insert(newpackage) < 1)
                {
                    ErrInfo = packageMgr.Err;
                    return -1;
                }

                ArrayList al = HSReFeeDetail[repackage.ID] as ArrayList;
                if (al == null || al.Count == 0)
                {
                    ErrInfo = "查找明细项目失败！";
                    return -1;
                }

                int detailSeq = 1;
                foreach (FS.HISFC.Models.MedicalPackage.Fee.PackageDetail redetail in al)
                {
                    FS.HISFC.Models.MedicalPackage.Fee.PackageDetail newdetail = redetail.Clone();
                    newdetail.ID = newpackage.ID;
                    newdetail.Trans_Type = "1";
                    newdetail.PayFlag = "1";
                    newdetail.Cancel_Flag = "0";
                    newdetail.Oper = oper.ID;
                    newdetail.OperTime = this.packageMgr.GetDateTimeFromSysDateTime();
                    newdetail.InvoiceNO = invoiceNO;
                    newdetail.SequenceNO = detailSeq.ToString();
                    newdetail.HospitalID = hospitalid;
                    newdetail.HospitalName = hospitalname;//{CB0CA2C8-1DAE-4a6a-B5F8-DCC227A7AF90}
                    if (this.packageDetailMgr.Insert(newdetail) < 1)
                    {
                        ErrInfo = packageMgr.Err;
                        return -1;
                    }
                    ArrayList costlist = this.PackageCostMgr.QueryByID(redetail.ID, redetail.SequenceNO);
                    if (costlist.Count > 0)
                    {
                        FS.HISFC.Models.MedicalPackage.Fee.PackageCost cost = costlist[0] as FS.HISFC.Models.MedicalPackage.Fee.PackageCost;
                        cost.NewPackageClinic = newdetail.ID;
                        cost.NewDetailSeq = newdetail.SequenceNO;
                        if (this.PackageCostMgr.UpdateByCostTypeForCrmCostId(cost) < 0)
                        {
                            ErrInfo = this.PackageCostMgr.Err;
                            return -1;
                        }
                    }
                    detailSeq++;
                }
                packageSeq++;
            }

            #endregion

            #region 处理新的发票

            FS.HISFC.Models.MedicalPackage.Fee.Invoice invoice = new FS.HISFC.Models.MedicalPackage.Fee.Invoice();
            invoice.InvoiceNO = invoiceNO;
            invoice.Trans_Type = "1";
            invoice.Paykindcode = invoiceInfo.Paykindcode;
            invoice.Card_Level = invoiceInfo.Card_Level;
            invoice.Package_Cost = reTot;
            invoice.Real_Cost = reReal;
            invoice.Gift_cost = reGift;
            invoice.Etc_cost = reEtc;
            invoice.InvoiceSeq = InvoiceSeq;
            invoice.PrintInvoiceNO = realInvoiceNO;
            invoice.Oper = FS.FrameWork.Management.Connection.Operator.ID;
            invoice.OperTime = this.invoiceMgr.GetDateTimeFromSysDateTime();
            invoice.Cancel_Flag = "0";
            invoice.Hospital_ID = hospitalid;
            invoice.HospitalName = hospitalname;//{CB0CA2C8-1DAE-4a6a-B5F8-DCC227A7AF90}
            if (this.invoiceMgr.Insert(invoice) < 1)
            {
                ErrInfo = this.invoiceMgr.Err;
                return -1;
            }

            #endregion

            #region 处理重新收费支付方式
            Hashtable HSRefeeCostInfo = new Hashtable();

            HSRefeeCostInfo.Add("TOT", reTot);  //套餐金额
            HSRefeeCostInfo.Add("REAL", reReal + reGift); //实收金额
            HSRefeeCostInfo.Add("ETC", reEtc);  //优惠金额

            HSRefeeCostInfo.Add("ACTU", 0.0m);  //实际支付
            HSRefeeCostInfo.Add("GIFT", 0);  //赠送支付
            HSRefeeCostInfo.Add("COU", 0.0m);  //积分支付
            HSRefeeCostInfo.Add("DEPO", 0.0m);  //押金支付
            HSRefeeCostInfo.Add("ROUND", 0.0m); //四舍五入

            Forms.frmPayModeForRefee paymodeForRefee = new HISFC.Components.Package.Fee.Forms.frmPayModeForRefee();
            paymodeForRefee.PatientInfo = PatientInfo;
            paymodeForRefee.GiftPay = reGift;
            paymodeForRefee.HsPayCost = HSRefeeCostInfo;

            if (paymodeForRefee.ShowDialog() != DialogResult.OK)
            {
                ErrInfo = "未选择支付方式！";
                return -1;
            }

            ArrayList payModeInfo = paymodeForRefee.PayModeInfo;
            ArrayList depositInfo = paymodeForRefee.DepositInfo;
            Hashtable costInfo = paymodeForRefee.HsPayCost;

            //获取所有的费用类别信息
            decimal totCost = (decimal)costInfo["TOT"];           //套餐原价
            decimal actuCost = (decimal)costInfo["ACTU"];         //实收金额
            //{333D2AD8-DC4A-4c30-A14E-D6815AC858F9}
            decimal giftCost = (decimal)costInfo["GIFT"] + (decimal)costInfo["COU"];         //赠送金额与积分金额
            decimal depoCost = (decimal)costInfo["DEPO"];         //押金金额
            decimal etcCost = (decimal)costInfo["ETC"];           //优惠金额
            decimal roundCost = (decimal)costInfo["ROUND"];       //四舍五入

            //正常情况下不会存在支付总额大于应付总额的情况,但如果患者缴纳
            //的单笔押金额大于套餐的总金额时，需要进行处理
            if (actuCost + giftCost + depoCost + etcCost > totCost)
            {
                if (actuCost > 0 || giftCost > 0)
                {
                    ErrInfo = "缴纳的金额多余应缴金额，请调整金额！";
                    return -1;
                }
            }

            if (actuCost + giftCost + depoCost + etcCost < totCost)
            {
                if (totCost - (actuCost + giftCost + depoCost + etcCost) > 0.1m)  //存在一分钱的误差
                {
                    ErrInfo = "缴纳的金额不足，请调整金额！";
                    return -1;
                }
               
            }

            #region 押金处理

            //实际应该缴纳的押金额
            decimal shouldDepo = totCost - giftCost - actuCost - etcCost;
            //将押金记录转化为支付记录并且计算出真正的 实付金额，赠送金额
            foreach (FS.HISFC.Models.MedicalPackage.Fee.Deposit deposit in depositInfo)
            {
                if (shouldDepo == 0)
                    break;

                FS.HISFC.Models.MedicalPackage.Fee.PayMode depositpay = new FS.HISFC.Models.MedicalPackage.Fee.PayMode();
                depositpay.Mode_Code = "DE";
                depositpay.Related_ID = deposit.ID;               //押金记录号
                depositpay.Related_ModeCode = deposit.Mode_Code;  //押金缴纳方式
                depositpay.Account = deposit.Account;             //押金的缴纳账号
                depositpay.AccountFlag = deposit.AccountFlag;     //押金的缴纳账户标识
                depositpay.AccountType = deposit.AccountType;     //押金的缴纳账户类型
                depositpay.Cancel_Flag = "0";
                depositpay.Trans_Type = "1";
                depositpay.Oper = FS.FrameWork.Management.Connection.Operator.ID;
                depositpay.OperTime = this.packageMgr.GetDateTimeFromSysDateTime();

                //部分支付
                if (shouldDepo <= deposit.Amount)
                {
                    depositpay.Tot_cost = depositpay.Real_Cost = shouldDepo;
                    shouldDepo = 0;
                }
                else  //整条押金支付
                {
                    depositpay.Tot_cost = depositpay.Real_Cost = deposit.Amount;
                    shouldDepo -= deposit.Amount;
                }

                if (depositpay.Related_ModeCode == "DC" && depositpay.AccountFlag == "1")
                {
                    giftCost += depositpay.Tot_cost;
                }
                else
                {
                    actuCost += depositpay.Tot_cost;
                }

                payModeInfo.Add(depositpay);

                //消费押金的函数
                if (this.depositMgr.DepositCost(deposit, depositpay.Tot_cost) < 0)
                {
                    ErrInfo = this.depositMgr.Err;
                    return -1;
                }
            }

            if (totCost != actuCost + giftCost + etcCost)
            {
                ErrInfo = "费用类别计算错误！";
                return -1;
            }

            #endregion

            //cashCouponAmount = 0.0m;
            int payinfoSequence = 1;
            foreach (FS.HISFC.Models.MedicalPackage.Fee.PayMode pay in payModeInfo)
            {
                pay.InvoiceNO = invoiceNO;
                pay.Trans_Type = "1";
                pay.SequenceNO = payinfoSequence.ToString();
                pay.Oper = FS.FrameWork.Management.Connection.Operator.ID;
                pay.OperTime = this.packageMgr.GetDateTimeFromSysDateTime();
                //  {D59EF243-868D-41a0-9827-5E2E608522CA}
                pay.Hospital_ID = dept.HospitalID;
                pay.HospitalName = dept.HospitalName;
                //账户扣费
                if (pay.Mode_Code == "YS")
                {
                    if (accountPay.OutpatientPay(PatientInfo,
                                                pay.Account,
                                                pay.AccountType,
                                                -pay.Tot_cost,
                                                0,
                                                invoiceNO, PatientInfo,
                                                FS.HISFC.Models.Account.PayWayTypes.P,
                                                1) < 1)
                    {
                        ErrInfo = "账户扣费失败！";
                        return -1;
                    }

                }

                if (pay.Mode_Code == "DC")
                {
                    if (accountPay.OutpatientPay(PatientInfo,
                                                pay.Account,
                                                pay.AccountType,
                                                0,
                                                -pay.Tot_cost,
                                                invoiceNO, PatientInfo,
                                                FS.HISFC.Models.Account.PayWayTypes.P,
                                                1) < 1)
                    {
                        ErrInfo = "账户扣费失败！";
                        return -1;
                    }

                }

                //插入支付表
                if (this.paymodeMgr.Insert(pay) < 0)
                {
                    ErrInfo = "插入支付方式失败！";
                    return -1;
                }

                //{333D2AD8-DC4A-4c30-A14E-D6815AC858F9}
                if (pay.Mode_Code == "CO")
                {
                    refeeCostCouponAmount += pay.Tot_cost;
                }


                if (cashCouponPayMode.Name.Contains(pay.Mode_Code.ToString()) || (pay.Mode_Code == "DE" && cashCouponPayMode.Name.Contains(pay.Related_ModeCode.ToString())))
                {
                    //cashCouponAmount += pay.Tot_cost;
                    refeeOperateCouponAmount += pay.Tot_cost;
                }

                //{3B9D0100-377E-48ac-AF31-4412CDCBF4B0}
                if (dlrCouponPayMode.Name.Contains(pay.Mode_Code.ToString()) || (pay.Mode_Code == "DE" && cashCouponPayMode.Name.Contains(pay.Related_ModeCode.ToString())))
                {
                    //cashCouponAmount += pay.Tot_cost;
                    refeeOperateDCouponAmount += pay.Tot_cost;
                }

                payinfoSequence++;
            }

            if (roundCost != 0)
            {
                FS.HISFC.Models.MedicalPackage.Fee.PayMode pay = new FS.HISFC.Models.MedicalPackage.Fee.PayMode();
                pay.InvoiceNO = invoiceNO;
                pay.Trans_Type = "1";
                pay.SequenceNO = payinfoSequence.ToString();
                pay.Mode_Code = "SW";
                pay.Oper = FS.FrameWork.Management.Connection.Operator.ID;
                pay.OperTime = this.packageMgr.GetDateTimeFromSysDateTime();
                pay.Tot_cost = pay.Real_Cost = roundCost;
                pay.Cancel_Flag = "0";


                //插入支付表
                if (this.paymodeMgr.Insert(pay) < 0)
                {
                    ErrInfo = "插入四舍五入位失败！";
                    return -1;
                }
            }

            #endregion

            #region 处理重新收费支付方式[作废]
            /*

            //{F166B18B-62E3-4835-A729-4CA384F9ADEE}
            cashCouponAmount = 0.0m;

            //第一次，消耗账户和赠送账户
            int paySeq = 1;
            foreach (FS.HISFC.Models.MedicalPackage.Fee.PayMode pay in QuitPayModeList)
            {
                //全部扣费完毕，无需再扣
                if (reGift == 0 && reReal == 0)
                {
                    break;
                }

                if (pay.Mode_Code == "YS" || pay.Related_ModeCode == "YS" || pay.Mode_Code == "DC" || pay.Related_ModeCode == "DC")
                {
                    if (pay.AccountFlag == "0" && reReal > 0)
                    {
                        FS.HISFC.Models.MedicalPackage.Fee.PayMode newpay = new FS.HISFC.Models.MedicalPackage.Fee.PayMode();
                        newpay.InvoiceNO = invoiceNO;
                        newpay.Mode_Code = "YS";
                        newpay.Trans_Type = "1";
                        newpay.Cancel_Flag = "0";
                        newpay.SequenceNO = paySeq.ToString();
                        newpay.Account = pay.Account;
                        newpay.AccountFlag = pay.AccountFlag;
                        newpay.AccountType = pay.AccountType;
                        newpay.Tot_cost = newpay.Real_Cost = reReal > pay.Real_Cost ? pay.Real_Cost : reReal;
                        newpay.Oper = FS.FrameWork.Management.Connection.Operator.ID;
                        newpay.OperTime = this.packageMgr.GetDateTimeFromSysDateTime();

                        if (reReal > pay.Real_Cost)
                        {
                            reReal -= pay.Real_Cost;
                        }
                        else
                        {
                            reReal = 0;
                        }


                        //用于返回提示
                        HSPayInfo["YS"] = (decimal)HSPayInfo["YS"] - newpay.Tot_cost;
                        if (accountPay.OutpatientPay(PatientInfo,
                                                     newpay.Account,
                                                     newpay.AccountType,
                                                     -newpay.Tot_cost,
                                                     0,
                                                     invoiceNO, PatientInfo,
                                                     FS.HISFC.Models.Account.PayWayTypes.M,
                                                     1) < 1)
                        {
                            //{2694417D-715F-4ef6-A664-1F92399DC325}
                            ErrInfo = "账户消费失败！" + accountPay.Err;
                            return -1;
                        }

                        if (this.paymodeMgr.Insert(newpay) < 1)
                        {
                            ErrInfo = this.paymodeMgr.Err;
                            return -1;
                        }



                        //{333D2AD8-DC4A-4c30-A14E-D6815AC858F9}
                        if (newpay.Mode_Code == "CO")
                        {
                            refeeCostCouponAmount += pay.Tot_cost;
                        }

                        if (cashCouponPayMode.Name.Contains(newpay.Mode_Code.ToString()) || (newpay.Mode_Code == "DE" && cashCouponPayMode.Name.Contains(newpay.Related_ModeCode.ToString())))
                        {
                            cashCouponAmount += newpay.Tot_cost;
                            refeeOperateCouponAmount += pay.Tot_cost;
                        }

                        paySeq++;
                    }

                    if (pay.AccountFlag == "1" && reGift > 0)
                    {
                        FS.HISFC.Models.MedicalPackage.Fee.PayMode newpay = new FS.HISFC.Models.MedicalPackage.Fee.PayMode();
                        newpay.InvoiceNO = invoiceNO;
                        newpay.Mode_Code = "DC";
                        newpay.Trans_Type = "1";
                        newpay.Cancel_Flag = "0";
                        newpay.SequenceNO = paySeq.ToString();
                        newpay.Account = pay.Account;
                        newpay.AccountFlag = pay.AccountFlag;
                        newpay.AccountType = pay.AccountType;
                        newpay.Tot_cost = newpay.Real_Cost = reGift > pay.Real_Cost ? pay.Real_Cost : reGift;
                        newpay.Oper = FS.FrameWork.Management.Connection.Operator.ID;
                        newpay.OperTime = this.packageMgr.GetDateTimeFromSysDateTime();
                        if (reGift > pay.Real_Cost)
                        {
                            reGift -= pay.Real_Cost;
                        }
                        else
                        {
                            reGift = 0;
                        }


                        //用于返回提示
                        HSPayInfo["DC"] = (decimal)HSPayInfo["DC"] - newpay.Tot_cost;
                        if (accountPay.OutpatientPay(PatientInfo,
                                                     newpay.Account,
                                                     newpay.AccountType,
                                                     0,
                                                     -newpay.Tot_cost,
                                                     newpay.InvoiceNO, PatientInfo,
                                                     FS.HISFC.Models.Account.PayWayTypes.M,
                                                     1) < 1)
                        {
                            //{2694417D-715F-4ef6-A664-1F92399DC325}
                            ErrInfo = "账户消费失败！" + accountPay.Err;
                            return -1;
                        }

                        if (this.paymodeMgr.Insert(newpay) < 1)
                        {
                            ErrInfo = this.paymodeMgr.Err;
                            return -1;
                        }

                        //{333D2AD8-DC4A-4c30-A14E-D6815AC858F9}
                        if (newpay.Mode_Code == "CO")
                        {
                            refeeCostCouponAmount += pay.Tot_cost;
                        }

                        if (cashCouponPayMode.Name.Contains(newpay.Mode_Code.ToString()) || (newpay.Mode_Code == "DE" && cashCouponPayMode.Name.Contains(newpay.Related_ModeCode.ToString())))
                        {
                            cashCouponAmount += newpay.Tot_cost;
                            refeeOperateCouponAmount += newpay.Tot_cost;
                        }

                        paySeq++;
                    }
                }
            }

            //到此步，重收的赠送账户应该全部收完，如果没有，则产生了错误
            if (reGift > 0)
            {
                ErrInfo = "费用扣除失败！";
                return -1;
            }

            //消耗其他支付
            foreach (FS.HISFC.Models.MedicalPackage.Fee.PayMode pay in QuitPayModeList)
            {
                if (reReal == 0)
                {
                    break;
                }

                if (pay.Mode_Code == "YS" || pay.Related_ModeCode == "YS" || pay.Mode_Code == "DC" || pay.Related_ModeCode == "DC")
                {
                    continue;
                }

                FS.HISFC.Models.MedicalPackage.Fee.PayMode newpay = new FS.HISFC.Models.MedicalPackage.Fee.PayMode();
                newpay.InvoiceNO = invoiceNO;
                newpay.Mode_Code = pay.Mode_Code;
                newpay.Trans_Type = "1";
                newpay.Cancel_Flag = "0";
                newpay.SequenceNO = paySeq.ToString();
                newpay.Account = pay.Account;
                newpay.AccountFlag = pay.AccountFlag;
                newpay.AccountType = pay.AccountType;
                newpay.InvoiceNO = invoiceNO;
                newpay.Tot_cost = newpay.Real_Cost = reReal > pay.Real_Cost ? pay.Real_Cost : reReal;
                newpay.Oper = FS.FrameWork.Management.Connection.Operator.ID;
                newpay.Related_ID = pay.Related_ID;
                newpay.Related_ModeCode = pay.Related_ModeCode;
                newpay.OperTime = this.packageMgr.GetDateTimeFromSysDateTime();
                if (newpay.Mode_Code == "RC")//{791F1D67-AE8A-4bc8-AC1F-2FB13204A862} 如果是套餐优惠则不扣除实付金额
                {
                    newpay.Tot_cost = newpay.Real_Cost = reEtc;
                }
                else
                {
                    if (reReal > pay.Real_Cost)
                    {
                        reReal -= pay.Real_Cost;
                    }
                    else
                    {
                        reReal = 0;
                    }
                }
                //用于返回提示
                if (newpay.Mode_Code == "DE")
                {
                    HSPayInfo[newpay.Related_ModeCode] = (decimal)HSPayInfo[newpay.Related_ModeCode] - newpay.Tot_cost;
                }
                else
                {
                    HSPayInfo[newpay.Mode_Code] = (decimal)HSPayInfo[newpay.Mode_Code] - newpay.Tot_cost;
                }

                if (this.paymodeMgr.Insert(newpay) < 1)
                {
                    ErrInfo = this.paymodeMgr.Err;
                    return -1;
                }

                //{333D2AD8-DC4A-4c30-A14E-D6815AC858F9}
                if (newpay.Mode_Code == "CO")
                {
                    refeeCostCouponAmount += pay.Tot_cost;
                }

                if (cashCouponPayMode.Name.Contains(newpay.Mode_Code.ToString()) || (newpay.Mode_Code == "DE" && cashCouponPayMode.Name.Contains(newpay.Related_ModeCode.ToString())))
                {
                    cashCouponAmount += newpay.Tot_cost;
                    refeeOperateCouponAmount += newpay.Tot_cost;
                }

                //{146FEAC3-5B89-4966-AAA2-C8D86FACB35F}交易流水号bug修复
                paySeq++;
            }  */


            #endregion


            //{F166B18B-62E3-4835-A729-4CA384F9ADEE}
            //if (cashCouponAmount > 0 || cashCouponAmount < 0)
            //{
            //    FS.HISFC.BizProcess.Integrate.Account.CashCoupon cashCouponPrc = new FS.HISFC.BizProcess.Integrate.Account.CashCoupon();
            //    string errText2 = string.Empty;
            //    if (cashCouponPrc.CashCouponSave("TCSF", PatientInfo.PID.CardNO, invoiceNO, cashCouponAmount, ref errText2) <= 0)
            //    {
            //        ErrInfo = "计算现金流积分出错!" + errText2;
            //        return -1;
            //    }

            //}

            //{E8D52AF4-08BF-489e-A303-1587BEDAEB72}
            if (isRefeeIncludeUncouponPacage)
            {
                refeeOperateCouponAmount = 0.0m;
            }

            //{3B9D0100-377E-48ac-AF31-4412CDCBF4B0}
            if (isRefeeIncludeUncouponPacageDLR)
            {
                refeeOperateDCouponAmount = 0.0m;
            }

            #region 发票走号

            if (this.feePackageProcess.UseInvoiceNO(oper, "1", "M", 1, ref invoiceNO, ref realInvoiceNO, ref ErrInfo) < 0)
            {
                ErrInfo = this.feePackageProcess.Err;
                return -1;
            }

            if (this.feePackageProcess.InsertInvoiceExtend(invoiceNO, "M", realInvoiceNO, "00") < 0)
            {
                ErrInfo = this.feePackageProcess.Err;
                return -1;
            }

            #endregion

            return 1;

        }

        /// <summary>
        /// {71d2d8aa-d310-4ec5-8b5a-429a26d235a9}
        /// 获取账户积分信息
        /// </summary>
        private int GetCouponVacancy(ref decimal vancancy)
        {
            try
            {
                vancancy = 0.0m;
                decimal donateCouponVacancy = 0.0m;
                string resultCode = "0";
                string errorMsg = "";
                //{6481187A-826A-40d7-8548-026C8C501B3E}
                //Dictionary<string, string> dic = FS.HISFC.BizProcess.Integrate.WSHelper.QueryAccountInfoPersonal(this.patientInfo.PID.CardNO, out resultCode, out errorMsg);
                Dictionary<string, string> dic = FS.HISFC.BizProcess.Integrate.WSHelper.QueryAccountInfo(this.patientInfo.PID.CardNO, out resultCode, out errorMsg);
                if (dic == null)
                {
                    MessageBox.Show("查询账户出错:" + errorMsg);
                    return -1;
                }

                if (dic.ContainsKey("couponvacancy"))
                {
                    vancancy = decimal.Parse(dic["couponvacancy"].ToString());
                }
                else
                {
                    MessageBox.Show("查询账户积分出错！");
                    return -1;
                }
                return 1;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        /// <summary>
        /// 获取积分倍数  {c2d43b9d-eda4-4f87-92c1-f6fdb08d9d04}取多倍积分
        /// </summary>
        /// <param name="invoiceNO"></param>
        /// <param name="couponnum"></param>
        /// <returns></returns>
        private int GetCouponNum(string invoiceNO, ref decimal couponnum)
        {
            try
            {
                couponnum = 0.0m;
                string resultCode = "0";
                string errorMsg = "";
                Dictionary<string, string> dic = FS.HISFC.BizProcess.Integrate.WSHelper.QueryCouponNum(invoiceNO, out resultCode, out errorMsg);
                if (dic == null)
                {
                    couponnum = 1;
                }
                else
                {
                    if (dic.ContainsKey("couponNum"))
                    {
                        couponnum = decimal.Parse(dic["couponNum"].ToString());
                    }
                    else
                    {
                        couponnum = 1;
                    }
                }
                return 1;
            }
            catch (Exception)
            {
                return -1;
            }

        }



        //{DE811397-687D-4725-AA88-A7153B24FB8A}
        protected override int OnPrint(object sender, object neuObject)
        {
            FS.HISFC.BizProcess.Interface.MedicalPackage.IPackageInvoice packageinvoiceprint = null;
            packageinvoiceprint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.MedicalPackage.IPackageInvoice)) as FS.HISFC.BizProcess.Interface.MedicalPackage.IPackageInvoice;
            if (packageinvoiceprint == null)
            {
                MessageBox.Show(FS.FrameWork.WinForms.Classes.UtilInterface.Err, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return -1;
            }

            if (this.invoiceInfo == null || string.IsNullOrEmpty(this.invoiceInfo.ID))
            {
                return -1;
            }

            packageinvoiceprint.SetPrintValue(this.invoiceInfo.ID);
            packageinvoiceprint.Print();
            return -1;
        }

        /// <summary>
        /// 列枚举
        /// </summary>
        private enum PackageCols
        {
            /// <summary>
            /// 全退勾选框
            /// </summary>
            Select = 0,

            /// <summary>
            /// {9B833B34-AE7F-4013-8F9D-CDE36A738D02}
            /// {56809DCA-CD5A-435e-86F0-93DE99227DF4}
            /// 是否特殊折扣
            /// </summary>
            SpecialFlag = 1,

            /// <summary>
            /// 套餐名称
            /// </summary>
            Name = 2,

            /// <summary>
            /// 总金额
            /// </summary>
            TotCost = 3,

            /// <summary>
            /// 实收金额
            /// </summary>
            RealCost = 4,

            /// <summary>
            /// 赠送金额
            /// </summary>
            GiftCost = 5,

            /// <summary>
            /// 优惠金额
            /// </summary>
            ETCCost = 6
        }

        /// <summary>
        /// 列枚举
        /// </summary>
        private enum DetailCols
        {
            /// <summary>
            /// 套餐名称
            /// </summary>
            Name = 0,

            /// <summary>
            /// 总数量
            /// </summary>
            Qty = 1,

            /// <summary>
            /// 可退数量
            /// </summary>
            AVARTN = 2,

            /// <summary>
            /// 退费数量
            /// </summary>
            RTN = 3,

            /// <summary>
            /// 总金额
            /// </summary>
            TotCost = 4,

            /// <summary>
            /// 实收金额
            /// </summary>
            RealCost = 5,

            /// <summary>
            /// 赠送金额
            /// </summary>
            GiftCost = 6,

            /// <summary>
            /// 优惠金额
            /// </summary>
            ETCCost = 7
        }
    }
}
