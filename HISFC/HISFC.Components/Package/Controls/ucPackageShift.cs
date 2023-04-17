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
    /// <summary>
    /// 套餐转移
    /// hyg
    /// 2020-08-12
    /// {3599D82C-0E7E-4628-8FC6-DDAAA1CC6335}
    /// </summary>
    public partial class ucPackageShift : FS.FrameWork.WinForms.Controls.ucBaseControl
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
        /// 患者基本信息
        /// </summary>
        protected FS.HISFC.Models.RADT.PatientInfo patientInfoNew = null;

        /// <summary>
        /// 患者基本信息
        /// </summary>
        public FS.HISFC.Models.RADT.PatientInfo PatientInfoNew
        {
            get { return this.patientInfoNew; }
            set
            {
                this.patientInfoNew = value;
                this.SetNewPatientInfo();
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
        /// 是否启用套餐管理CRM系统
        /// </summary>
        private bool IsPackageDealInCrm = false;

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
        /// 套餐业务管理类2，上面一行旧的那个包不对，所以用新的
        /// </summary>
        FS.HISFC.BizProcess.Integrate.MedicalPackage.Package feePackageProcess2 = new FS.HISFC.BizProcess.Integrate.MedicalPackage.Package();
        
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
        /// 控制参数业务层
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();


        #endregion


        public ucPackageShift()
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
            this.ShowInvoiceInfo();
        }

        /// <summary>
        /// 设置接受套餐患者信息
        /// </summary>
        private void SetNewPatientInfo()
        {
            if (this.PatientInfoNew == null)
            {
                this.tbNewCardNo.Text = string.Empty;
                this.tbNewName.Text = string.Empty;
                this.tbNewIdCard.Text = string.Empty;
                this.tbNewSex.Text = string.Empty;
                this.tbFamilyName.Text = string.Empty;
                this.tbNewBirthday.Text = string.Empty;
                return;
            }

            this.tbNewCardNo.Text = this.PatientInfoNew.PID.CardNO;
            this.tbNewName.Text = this.PatientInfoNew.Name;
            this.tbNewIdCard.Text = this.PatientInfoNew.IDCard;
            this.tbNewSex.Text = this.PatientInfoNew.Sex.Name;
            this.tbFamilyName.Text = this.PatientInfoNew.FamilyInfo.Name;
            this.tbNewBirthday.Text = this.PatientInfoNew.Birthday.ToString("yyyy-MM-dd HH:mm:ss");
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
                    this.fpPackage_Sheet1.Cells[this.fpPackage_Sheet1.RowCount - 1, (int)PackageCols.Select].Locked = true;

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
                
                return;
            }
            FS.HISFC.Models.MedicalPackage.Fee.Package selectPackage = this.fpPackage_Sheet1.ActiveRow.Tag as FS.HISFC.Models.MedicalPackage.Fee.Package;
            
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
                }
                else
                {
                }
            }

            this.SetQuitCostInfo();

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
            _toolBarService.AddToolButton("套餐转移", "套餐转移", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Z召回, true, false, null);
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
                case "套餐转移":
                    this.PackageShift();
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

            FS.FrameWork.Management.PublicTrans.Commit();
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            string QuitMessage = "退费成功！";


            MessageBox.Show(QuitMessage, "退费提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.PatientInfo = null;
            this.invoiceTree.Clear();
        }

        public void PackageShift()
        {
            //患者信息
            FS.HISFC.Models.RADT.PatientInfo PatientInfo = this.PatientInfo;

            if (PatientInfo == null)
            {
                MessageBox.Show("请先检索患者！");
                return;
            }

            if (this.PatientInfoNew == null)
            {
                MessageBox.Show("请先选择接收套餐患者！");
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
                MessageBox.Show("没有需要转移的项目！");
                return;
            }

            string ErrInfo = string.Empty;

            string newInvoiceNO = "";

            DialogResult result = MessageBox.Show("是否将【" + this.PatientInfo.Name+"】的发票【"+this.invoiceInfo.ID+"】转移至【"+this.PatientInfoNew.Name+"】处", "核对转移信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (result == DialogResult.OK)
            {
                
            }
            else
            {
                return;
            }

            int i = this.saveShift(this.PatientInfo, this.PatientInfoNew, this.invoiceInfo);
            if (i == 0)
            {
                MessageBox.Show("转移失败" , "转移提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            #region 疫苗套餐转移时需要同步到crm

            //{AA59299C-6C72-4c61-84E4-C7443E85FFBD}
            if (this.IsPackageDealInCrm)
            {
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在同步转移套餐。。。");
                string req = @"
                        <req>
                          <crmId>{0}</crmId>
                          <invoiceNo>{1}</invoiceNo>
                          <newCardNo>{2}</newCardNo>
                          <newCrmId>{3}</newCrmId>
                          <newName>{4}</newName>
                        </req>";

                try
                {
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

                    req = string.Format(req, this.PatientInfo.CrmID, this.invoiceInfo.ID, this.PatientInfoNew.PID.CardNO, this.PatientInfoNew.CrmID, this.PatientInfoNew.Name);

                    FS.HISFC.BizProcess.Integrate.IbornMobileService server = new FS.HISFC.BizProcess.Integrate.IbornMobileService();
                    
                    string res = FS.HISFC.BizProcess.Integrate.WSHelper.InvokeWebService(url, "PackageShiftInCrm", new string[] { req }) as string;
                   
                }
                catch (Exception e)
                {
                    
                }
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }

            #endregion


            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            string QuitMessage = "转移成功！转移后退费请按发票号退费！";


            MessageBox.Show(QuitMessage, "转移提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.PatientInfo = null;
            this.invoiceTree.Clear();
        }

        public int saveShift(FS.HISFC.Models.RADT.PatientInfo PatientInfo, FS.HISFC.Models.RADT.PatientInfo PatientInfoNew, FS.HISFC.Models.MedicalPackage.Fee.Invoice invoiceInfo) 
        {
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.feePackageProcess2.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            //先转移套餐数据
            int i = feePackageProcess2.ShiftPackageOwner(patientInfo, patientInfoNew, invoiceInfo, packageMgr.Operator.ID);

            if (i < 0)
            {
                return 0;
            }

            //再转移套餐明细数据
            int j = feePackageProcess2.ShiftPackageDetailOwner(patientInfo, patientInfoNew, invoiceInfo, packageMgr.Operator.ID);

            if (i < 0 ||j < 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                return 0;
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            return 1;
        }

        /// <summary>
        /// {FAE56BB8-F958-411f-9663-CC359D6D494B}
        /// 获取账户积分信息
        /// </summary>
        private int GetCouponVacancy(ref decimal vancancy)
        {
            try
            {
                decimal couponAmount = 0.0m;

                string resultCode = "0";
                string errorMsg = "";
                //{6481187A-826A-40d7-8548-026C8C501B3E}
                Dictionary<string, string> dic = FS.HISFC.BizProcess.Integrate.WSHelper.QueryAccountInfoPersonal(this.patientInfo.PID.CardNO, out resultCode, out errorMsg);

                if (dic == null)
                {
                    MessageBox.Show("查询账户出错:" + errorMsg);
                    return -1;
                }

                if (dic.ContainsKey("couponvacancy"))
                {
                    couponAmount = decimal.Parse(dic["couponvacancy"].ToString());
                }
                else
                {
                    MessageBox.Show("查询账户积分出错！");
                    return -1;
                }

                vancancy = couponAmount;
                return 1;
            }
            catch (Exception ex)
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

        private void tbNewPatient_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                FS.HISFC.Components.Common.Forms.frmQueryPatientByConditions frmQuery = new FS.HISFC.Components.Common.Forms.frmQueryPatientByConditions();

                string QueryStr = this.tbNewPatient.Text;

                if (string.IsNullOrEmpty(QueryStr))
                {
                    return;
                }

                frmQuery.QueryByName(QueryStr);
                frmQuery.ShowDialog();

                if (frmQuery.DialogResult == DialogResult.OK)
                {
                    this.PatientInfoNew = frmQuery.PatientInfo;
                    //this.txtCardNO.Text = frmQuery.PatientInfo.PID.CardNO;
                    //this.txtCardNO.Focus();
                    //TextBox queryControl = new TextBox();
                    //queryControl.Text = this.txtCardNO.Text;
                    //txtCardNO_KeyDown(queryControl, new KeyEventArgs(Keys.Enter));
                }
            }
        }
    }
}
