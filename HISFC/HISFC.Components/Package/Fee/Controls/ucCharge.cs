using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using HISFC.Components.Package.Components;
using GZ.Components.Delivery.Forms;

namespace HISFC.Components.Package.Fee.Controls
{
    public partial class ucCharge : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        #region 属性

        /// <summary>
        /// 是否年龄过滤
        /// </summary>
        [Description("是否年龄过滤")]
        public bool IsFilter { set; get; }

        /// <summary>
        /// 
        /// </summary>
        [Description("套餐购买最大年龄")]
        public int MaxAge { set; get; }

        /// <summary>
        /// 套餐类型
        /// </summary>
        [Description("套餐类型 多个用逗号")]
        public string PackageType { set; get; }

        [Description("单个套餐收费的项目名称")]
        public string OnePackageName { set; get; }

        /// <summary>
        /// 患者信息控件
        /// </summary>
        private ucPatientInfo ucpatientInfo = null;

        /// <summary>
        /// 划价信息
        /// </summary>
        private ucChargeInfo ucchargeInfo = null;

        /// <summary>
        /// 支付,押金信息
        /// </summary>
        private ucPayInfo ucpayInfo = null;

        /// <summary>
        /// 划价单信息
        /// </summary>
        private Hashtable hsRecipes = new Hashtable();

        /// <summary>
        /// 获取自定义套餐包细项
        /// </summary>
        private Hashtable hsSpecialPackageDetail = new Hashtable();

        /// <summary>
        /// 押金缴纳窗口
        /// </summary>
        private Forms.frmDeposit FrmDeposit = new HISFC.Components.Package.Fee.Forms.frmDeposit();
        /// <summary>
        /// 账户管理类
        /// </summary>
        private FS.HISFC.BizLogic.Fee.Account accountMgr = new FS.HISFC.BizLogic.Fee.Account();
        
        /// <summary>
        /// {2694417D-715F-4ef6-A664-1F92399DC325}
        /// 合同项选择
        /// </summary>
        private Forms.frmAgreementChoose FrmAgreement = new HISFC.Components.Package.Fee.Forms.frmAgreementChoose();

        /// <summary>
        /// 套餐类别
        /// </summary>
        private ArrayList categoryList = new ArrayList();

        //{119F302E-69D9-445c-BF56-4109D975AD98}
        /// <summary>
        /// 费用业务层
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();

        /// <summary>
        /// 是否启用积分模块
        /// </summary>
        private bool IsCouponModuleInUse = false;

        /// <summary>
        /// 等级打折是否启用//{FAE56BB8-F958-411f-9663-CC359D6D494B}
        /// </summary>
        private bool IsLevelModuleInUse = false;

        /// <summary>
        /// 是否启用家庭首次套餐时间时间节点模块
        /// </summary>
        private bool IsFamilyFirstPackageInUse = false;

        /// <summary>
        /// 是否启用套餐管理CRM系统
        /// </summary>
        private bool IsPackageDealInCrm = false;

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

        /// <summary>
        /// 界面默认等级折扣勾选
        /// </summary>
        private bool levelDiscountChecked = true;

        #endregion

        #region 合同列表

        /// <summary>
        /// 合同列表
        /// </summary>
        private ArrayList agreementList = new ArrayList();

        #endregion

        #region 业务类

        /// <summary>
        /// 套餐费用业务类
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.MedicalPackage.Fee.Package packageFeeProcess = new FS.HISFC.BizProcess.Integrate.MedicalPackage.Fee.Package();
        
        /// <summary>
        /// 套餐业务类
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.MedicalPackage.Package packageProcess = new FS.HISFC.BizProcess.Integrate.MedicalPackage.Package();
        
        /// <summary>
        /// 套餐逻辑类
        /// </summary>
        private FS.HISFC.BizLogic.MedicalPackage.Fee.Package package = new FS.HISFC.BizLogic.MedicalPackage.Fee.Package();

        /// <summary>
        /// 套餐基础数据管理类
        /// </summary>
        private FS.HISFC.BizLogic.MedicalPackage.Package packageMgr = new FS.HISFC.BizLogic.MedicalPackage.Package();

        /// <summary>
        /// 常数管理类
        /// </summary>
        FS.HISFC.BizLogic.Manager.Constant constantMgr = new FS.HISFC.BizLogic.Manager.Constant();

        /// <summary>
        /// 合同查询类
        /// </summary>
        private GZ.BizLogic.Delivery.AgreementManager.AgreementBLL agreementMgr = new GZ.BizLogic.Delivery.AgreementManager.AgreementBLL();

        /// <summary>
        /// 控制参数业务层
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        #endregion

        /// <summary>
        /// 套餐购买界面
        /// </summary>
        public ucCharge()
        {
            InitializeComponent();
            init();
        }

        private void init()
        {
            ///患者信息
            if (this.ucpatientInfo == null)
            {
                this.ucpatientInfo = new ucPatientInfo();
                this.ucpatientInfo.Dock = DockStyle.Fill;
                this.plPatientInfo.Height = ucpatientInfo.Height;
                this.plPatientInfo.Controls.Add(this.ucpatientInfo);
            }

            ///收费信息
            if (this.ucchargeInfo == null)
            {
                this.ucchargeInfo = new ucChargeInfo();
                this.ucchargeInfo.Dock = DockStyle.Fill;
                this.plFeeInfo.Controls.Add(this.ucchargeInfo);
            }

            ///支付信息
            if (this.ucpayInfo == null)
            {
                this.ucpayInfo = new ucPayInfo();
                this.ucpayInfo.Dock = DockStyle.Fill;
                this.plPayInfo.Height = ucpayInfo.Height;
                this.plPayInfo.Controls.Add(this.ucpayInfo);
            }

            this.initControlsEvents();
            this.ucpatientInfo.Init();
            this.ucPackageSelector1.Init();
            this.ucpayInfo.InitInvoice(true);
        }

        /// <summary>
        /// 初始化控件委托事件
        /// </summary>
        private void initControlsEvents()
        {
            //患者信息委托事件
            this.ucpatientInfo.PatientInfoChange += new DelegateVoidSet(ucpatientInfo_PatientInfoChange);
            this.ucpatientInfo.GetPatientRecipeList += new DelegatArraListeGet(ucpatientInfo_GetPatientRecipeList);
            this.ucpatientInfo.NewPatientRecipe += new DelegateStringGet(ucpatientInfo_NewPatientRecipe);
            this.ucpatientInfo.DelPatientRecipe += new DelegateStringSet(ucpatientInfo_DelPatientRecipe);
            this.ucpatientInfo.CurrentRecipeChange += new DelegateVoidSet(ucpatientInfo_CurrentRecipeChange);
            this.ucpatientInfo.UseLevelDiscount += new DelegateBoolSet(ucpatientInfo_UseLevelDiscount);

            //划价信息委托事件
            this.ucchargeInfo.SetSelectorPosition += new DelegateBoolPointSet(ucchargeInfo_SetSelectorPosition);
            this.ucchargeInfo.SetSelectorFliter += new DelegateTripleStringSet(ucchargeInfo_SetSelectorFliter);
            this.ucchargeInfo.GetSelectorVisible += new DelegateBoolGet(ucchargeInfo_GetSelectorVisible);
            this.ucchargeInfo.SelectorKeyPress += new DelegateKeysSet(ucchargeInfo_SelectorKeyPress);
            this.ucchargeInfo.GetPackageDetailByID += new DelegateArrayListGetString(ucchargeInfo_GetPackageDetailByID);
            this.ucchargeInfo.SetFeeInfoCost += new DelegateVoidSet(ucchargeInfo_SetFeeInfoCost);
            this.ucchargeInfo.PackageChange += new DelegateVoidSet(ucchargeInfo_PackageChange);
            this.ucchargeInfo.DeleteChildPackage += new DelegateVoidSet(ucchargeInfo_DeleteChildPackage);
            this.ucchargeInfo.GetPackageDetialHsTable += new DelegateHashtableGet(ucchargeInfo_GetPackageDetialHsTable);

            //双击返回当前选中项
            this.ucPackageSelector1.RtnSelectedItem += new DelegateRtnSelectedItem(ucPackageSelector1_RtnSelectedItem);
            this.FrmAgreement.SetArrayListRes += new HISFC.Components.Package.Fee.Forms.DelegateArrayListSet(FrmAgreement_SetArrayListRes);

            //{2694417D-715F-4ef6-A664-1F92399DC325}
            this.categoryList = null;
            this.categoryList = constantMgr.GetList("PACKAGETYPE");

            this.IsCouponModuleInUse = this.controlParamIntegrate.GetControlParam<bool>("CP0001", false, false);

            this.IsLevelModuleInUse = this.controlParamIntegrate.GetControlParam<bool>("CP0002", false, false);

            this.IsFamilyFirstPackageInUse = this.controlParamIntegrate.GetControlParam<bool>("CPP001", false, false);

            this.IsPackageDealInCrm = this.controlParamIntegrate.GetControlParam<bool>("CPP001", false, false);
        }
        #region 患者信息委托事件

        /// <summary>
        /// 患者信息改变
        /// </summary>
        /// <returns></returns>
        private int ucpatientInfo_PatientInfoChange()
        {
            this.agreementList.Clear();
            //{FAE56BB8-F958-411f-9663-CC359D6D494B}
            this.getAccountDiscount();
            this.ucpayInfo.PatientInfo = this.ucpatientInfo.PatientInfo;
            return 1;
        }

        /// <summary>
        /// 获取划价单列表
        /// </summary>
        /// <returns></returns>
        private ArrayList ucpatientInfo_GetPatientRecipeList()
        {
            this.hsRecipes.Clear();
            this.hsSpecialPackageDetail.Clear();
            ArrayList recipeList =  this.packageFeeProcess.GetRecipesByPatient(this.ucpatientInfo.PatientInfo);
            foreach (FS.FrameWork.Models.NeuObject recipe in recipeList)
            {
                Hashtable hsTmp = new Hashtable();
                ArrayList feeInfo = this.packageFeeProcess.GetFeeInfoByRecipe(recipe.ID);
                if (feeInfo == null)
                {
                    feeInfo = new ArrayList();
                }

                foreach (FS.HISFC.Models.MedicalPackage.Fee.Package package in feeInfo)
                {
                    if (hsTmp.Contains(package.ParentPackageInfo.ID + package.PackageSequenceNO))
                    {
                        FS.HISFC.Models.MedicalPackage.Fee.Package parent = hsTmp[package.ParentPackageInfo.ID + package.PackageSequenceNO] as FS.HISFC.Models.MedicalPackage.Fee.Package;
                        parent.Package_Cost += package.Package_Cost;
                        parent.Real_Cost += package.Real_Cost;
                        parent.Etc_cost += package.Etc_cost;

                        if (parent.PackageSequenceNO != package.PackageSequenceNO || parent.PackageInfo.ID != package.ParentPackageInfo.ID)
                        {
                            MessageBox.Show("获取划价单出现错误！");
                            return null;
                        }
                    }
                    else
                    {
                        FS.HISFC.Models.MedicalPackage.Fee.Package parent = new FS.HISFC.Models.MedicalPackage.Fee.Package();
                        parent.RecipeNO = package.RecipeNO;
                        parent.PackageSequenceNO = package.PackageSequenceNO;
                        parent.PackageInfo = packageProcess.GetPackage(package.ParentPackageInfo.ID);

                        if (parent.PackageInfo == null || string.IsNullOrEmpty(parent.PackageInfo.ID))
                        {
                            MessageBox.Show("获取划价单出现错误！");
                            return null;
                        }

                        parent.Package_Cost = package.Package_Cost;
                        parent.Real_Cost = package.Real_Cost;
                        parent.Etc_cost = package.Etc_cost;

                        hsTmp.Add(package.ParentPackageInfo.ID + package.PackageSequenceNO, parent);
                    }
                }

                feeInfo.AddRange(hsTmp.Values);
                this.hsRecipes.Add(recipe.ID, feeInfo);
            }
            return recipeList;
        }

        /// <summary>
        /// 新建一张划价单
        /// </summary>
        /// <returns></returns>
        private string ucpatientInfo_NewPatientRecipe()
        {
            string recipeNO = this.packageFeeProcess.GetNewRecipeNO();
            ArrayList feeInfo = new ArrayList();
            this.hsRecipes.Add(recipeNO, feeInfo);
            return recipeNO;
        }

        /// <summary>
        /// 删除一张划价单
        /// </summary>
        /// <param name="al"></param>
        /// <returns></returns>
        private int ucpatientInfo_DelPatientRecipe(string str)
        {
            if (string.IsNullOrEmpty(str))
                return 1;
            //{50A7C61C-DE80-415e-BCA9-A6D083510B9B}
            int rtn = this.package.DeleteByRecipe(str);
            if (rtn < 0)
            {
                MessageBox.Show("删除划价单失败！原因：" + this.package.Err);
                return -1;
            }
            else
            {
                MessageBox.Show("当前划价单状态发生了变化，请刷新界面重试！");
                return -1;
            }
            this.hsRecipes.Remove(str);
            return 1;
        }

        /// <summary>
        /// 划价单发生了改变
        /// </summary>
        /// <returns></returns>
        private int ucpatientInfo_CurrentRecipeChange()
        {
            ArrayList recipeList = this.ucpatientInfo.GetCurrentRecipeList();
            ArrayList feeList = new ArrayList();

            foreach (FS.FrameWork.Models.NeuObject obj in recipeList)
            {
                ArrayList recipeFee = hsRecipes[obj.ID] as ArrayList;

                if (recipeFee != null)
                {
                    foreach (FS.HISFC.Models.MedicalPackage.Fee.Package feepackage in recipeFee)
                    {
                        if (feepackage.PackageInfo.PackageClass == "1")
                        {
                            feeList.Add(feepackage);
                        }
                    }
                }
            }

            this.ucchargeInfo.SetRecipePackage(feeList);
            return 1;
        }

        private int ucpatientInfo_UseLevelDiscount(bool bo)
        {
            try
            {
                levelDiscountChecked = bo;
                if (this.hsRecipes.Keys.Count > 0)
                {
                    MessageBox.Show("等级折扣已发生变化，请注意重新进行折扣！", "系统提示");
                }
                return 1;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        #endregion

        #region 划价信息委托事件

        /// <summary>
        /// 设置
        /// </summary>
        /// <param name="isShow"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        private int ucchargeInfo_SetSelectorPosition(bool isShow, Point location)
        {
            this.ucPackageSelector1.Visible = isShow;
            if (isShow)
            {
                this.ucPackageSelector1.Location = this.PointToClient(new Point(location.X + 5, location.Y + 110));
            }
            return 1;
        }

        private int ucchargeInfo_SetSelectorFliter(string str,string strclass,string parent)
        {
            this.ucPackageSelector1.Filter(str, strclass,parent);
            return 1;
        }

        private bool ucchargeInfo_GetSelectorVisible()
        {
            return this.ucPackageSelector1.Visible;
        }

        private int ucchargeInfo_SelectorKeyPress(Keys keyData)
        {
            if (keyData == Keys.Up)
            {
                this.ucPackageSelector1.PreRow();
            }
            if (keyData == Keys.Down)
            {
                this.ucPackageSelector1.NextRow();
            }
            if (keyData == Keys.Enter)
            {
                this.ucPackageSelector1.GetCurrentPackage();
            }
            return 1;
        }

        /// <summary>
        /// 根据packageID查询套餐细项
        /// </summary>
        /// <param name="PackageID"></param>
        /// <returns></returns>
        private ArrayList ucchargeInfo_GetPackageDetailByID(string PackageID)
        {
            return this.packageProcess.GetPackageItemByPackageID(PackageID);
        }

        /// <summary>
        /// 设置划价单价格信息
        /// </summary>
        /// <returns></returns>
        private int ucchargeInfo_SetFeeInfoCost()
        {
            FS.HISFC.Models.MedicalPackage.Fee.Package currentParent = this.ucchargeInfo.GetCurrentParentFeeRow();
            if (currentParent != null && !string.IsNullOrEmpty(currentParent.RecipeNO))
            {

                ArrayList feeList = this.hsRecipes[currentParent.RecipeNO] as ArrayList;

                currentParent.Package_Cost = 0.0m;
                currentParent.Real_Cost = 0.0m;
                currentParent.Gift_cost = 0.0m;
                currentParent.Etc_cost = 0.0m;

                foreach (FS.HISFC.Models.MedicalPackage.Fee.Package obj in feeList)
                {
                    if (obj.PackageInfo.PackageClass == "2" &&
                        obj.PackageSequenceNO == currentParent.PackageSequenceNO &&
                        obj.ParentPackageInfo.ID == currentParent.PackageInfo.ID)
                    {
                        currentParent.Package_Cost += obj.Package_Cost;
                        currentParent.Real_Cost += obj.Real_Cost;
                        currentParent.Gift_cost += obj.Gift_cost;
                        currentParent.Etc_cost += obj.Etc_cost;
                    }
                }

                this.ucchargeInfo.RefreshCurrentPackageData();
            }

            //{ECECDF2F-BA74-4615-A240-C442BE0A0074}
            this.ucpayInfo.PackageLists = this.ucchargeInfo.GetAllFeeRow();
            this.ucpayInfo.HsPayCost = this.ucchargeInfo.GetCostInfo();
            this.ucpatientInfo.SetRecipeCost(this.hsRecipes);

            return 1;
        }

        public int ucchargeInfo_PackageChange()
        {
            ArrayList packageList = this.ucchargeInfo.GetCurrentPackageFeeRow(true);
            ArrayList feeList = new ArrayList();

            foreach (FS.HISFC.Models.MedicalPackage.Fee.Package obj in packageList)
            {
                try
                {
                    ArrayList recipeFee = hsRecipes[obj.RecipeNO] as ArrayList;

                    foreach (FS.HISFC.Models.MedicalPackage.Fee.Package fee in recipeFee)
                    {
                        if (fee.PackageInfo.PackageClass == "2" &&
                            fee.PackageSequenceNO == obj.PackageSequenceNO &&
                            fee.ParentPackageInfo.ID == obj.PackageInfo.ID)
                        {
                            feeList.Add(fee);
                        }
                    }
                }
                catch
                { }
            }

            this.ucchargeInfo.SetChildPackage(feeList);
            return 1;
        }

        /// <summary>
        /// 删除子套餐
        /// </summary>
        /// <returns></returns>
        public int ucchargeInfo_DeleteChildPackage()
        {
            this.DelPriceRow();
            return 1;
        }



        private Hashtable ucchargeInfo_GetPackageDetialHsTable()
        {
            return this.hsSpecialPackageDetail;
        }


        #endregion

        #region 选择器选择器委托事件

        /// <summary>
        /// 选择器选择器双击
        /// </summary>
        /// <param name="package"></param>
        private void ucPackageSelector1_RtnSelectedItem(FS.HISFC.Models.MedicalPackage.Package package)
        {
            string recipeNO = string.Empty;
            FS.HISFC.Models.MedicalPackage.Fee.Package PackageFee = new FS.HISFC.Models.MedicalPackage.Fee.Package();
            //FF883061-A89B-4236-8DEE-8D066FA4F040 设置套餐年龄过滤
            if (IsFilter)
            {
                if (!string.IsNullOrEmpty(PackageType))
                {
                    string[] arr = PackageType.Split(',');
                    bool isFilterPackage = false;
                    for (int i = 0; i < arr.Length; i++)
                    {
                        if (arr[i] == package.PackageType)
                            isFilterPackage = true;
                    }
                    if (isFilterPackage)
                    {
                        DateTime date = Convert.ToDateTime(agreementMgr.GetSysDate("yyyy-MM-dd"));
                        int year = date.Year - this.ucpatientInfo.PatientInfo.Birthday.Year;
                        if (year > MaxAge)
                        {
                            this.ucchargeInfo_SetSelectorPosition(false, new Point());
                            DialogResult dr = MessageBox.Show("该患者已经超过了套餐年龄限制！是否继续", "提醒", MessageBoxButtons.YesNo);
                            if (dr == DialogResult.No)
                            {
                                this.ucchargeInfo_SetSelectorPosition(false, new Point());
                                return;
                            }
                        }
                        else
                        {
                            if (year == MaxAge)
                            {
                                if (date.Month - this.ucpatientInfo.PatientInfo.Birthday.Month > 0)
                                {
                                    DialogResult dr = MessageBox.Show("该患者已经超过了套餐年龄限制！是否继续", "提醒", MessageBoxButtons.YesNo);
                                    if (dr == DialogResult.No)
                                    {
                                        this.ucchargeInfo_SetSelectorPosition(false, new Point());
                                        return;
                                    }
                                }
                            }
                        }
                       // FS.FrameWork.Management.
                    }
                }
            }
            //  this.ucpatientInfo.PatientInfo.Birthday.Year
            recipeNO = this.ucpatientInfo.GetCurrentRecipe();
            FS.HISFC.Models.MedicalPackage.Fee.Package oldParentPackage = this.ucchargeInfo.GetCurrentParentFeeRow();
            FS.HISFC.Models.MedicalPackage.Fee.Package oldChildPackage = this.ucchargeInfo.GetCurrentFeeRow();

            if (string.IsNullOrEmpty(recipeNO))
            {
                this.ucchargeInfo_SetSelectorPosition(false, new Point());
                MessageBox.Show("未能获取划价单号，请检索患者或勾选当前右上角的划价单据！");
                return;
            }
            
            if (package.PackageClass == "2")
            {
                if (oldParentPackage == null || string.IsNullOrEmpty(oldParentPackage.PackageInfo.ID))
                {
                    this.ucchargeInfo_SetSelectorPosition(false, new Point());
                    MessageBox.Show("请先选择套餐！");
                    return;
                }
                else
                {
                    //recipeNO一定要去取当前套餐的RecipeNO
                    recipeNO = oldParentPackage.RecipeNO;
                }
            }

            PackageFee.PackageInfo = package;

            if (package.SpecialFlag == "1")
            {
                package.User01 = "0";
            }

            PackageFee.Package_Cost = Decimal.Parse(package.User01);
            PackageFee.Real_Cost = Decimal.Parse(package.User01);
            PackageFee.Gift_cost = 0.0m;
            PackageFee.Etc_cost = 0.0m;
            PackageFee.RecipeNO = recipeNO;
            // PackageFee.Consultant = package.User02;
            PackageFee.DelimitOper = FS.FrameWork.Management.Connection.Operator.ID;
            PackageFee.DelimitTime = System.DateTime.Now;


            if (PackageFee.PackageInfo.PackageClass == "1")
            {
                if (oldParentPackage != null)
                {
                    ArrayList al = this.hsRecipes[oldParentPackage.RecipeNO] as ArrayList;

                    for (int i = 0; i < al.Count; i++)
                    {
                        FS.HISFC.Models.MedicalPackage.Fee.Package tmp = al[i] as FS.HISFC.Models.MedicalPackage.Fee.Package;
                        //移除旧套餐
                        if (tmp.RecipeNO == oldParentPackage.RecipeNO &&
                            tmp.DelimitOper == oldParentPackage.DelimitOper &&
                            tmp.DelimitTime == oldParentPackage.DelimitTime &&
                            tmp.ID == oldParentPackage.ID &&
                            tmp.PackageInfo.PackageClass == "1")
                        {
                            //判断套餐状态
                            if (!string.IsNullOrEmpty(tmp.ID))
                            {
                                FS.HISFC.Models.MedicalPackage.Fee.Package packagetmp = new FS.HISFC.Models.MedicalPackage.Fee.Package();
                                packagetmp = this.package.QueryByID(tmp.ID);
                                if (packagetmp == null || packagetmp.Pay_Flag == "1" || packagetmp.Cancel_Flag == "1" || packagetmp.Cancel_Flag == "2")
                                {
                                    MessageBox.Show("当前套餐状态已发生改变，请刷新后重试！");
                                    return;
                                }

                                if (this.package.DeleteByID(packagetmp.ID, packagetmp.Trans_Type) < 1)
                                {
                                    MessageBox.Show("删除划价明细失败，请刷新，原因：" + this.package.Err);
                                    return;
                                }
                            }

                            al.Remove(tmp);
                            i--;
                        }

                        //移除套餐包
                        if (tmp.RecipeNO == oldParentPackage.RecipeNO &&
                           tmp.PackageSequenceNO == oldParentPackage.PackageSequenceNO &&
                           tmp.PackageInfo.PackageClass == "2")
                        {
                            //判断套餐状态
                            if (!string.IsNullOrEmpty(tmp.ID))
                            {
                                FS.HISFC.Models.MedicalPackage.Fee.Package packagetmp = new FS.HISFC.Models.MedicalPackage.Fee.Package();
                                packagetmp = this.package.QueryByID(tmp.ID);
                                if (packagetmp == null || packagetmp.Pay_Flag == "1" || packagetmp.Cancel_Flag == "1" || packagetmp.Cancel_Flag == "2")
                                {
                                    MessageBox.Show("当前套餐包状态已发生改变，请刷新后重试！");
                                    return;
                                }

                                if (this.package.DeleteByID(packagetmp.ID, packagetmp.Trans_Type) < 1)
                                {
                                    MessageBox.Show("删除划价套餐包失败，请刷新，原因：" + this.package.Err);
                                    return;
                                }
                            }
                            al.Remove(tmp);
                            i--;
                        }
                    }
                }
            }
            else if (PackageFee.PackageInfo.PackageClass == "2")
            {
                if (oldChildPackage != null)
                {
                    ArrayList al = this.hsRecipes[oldChildPackage.RecipeNO] as ArrayList;

                    for (int i = 0; i < al.Count; i++)
                    {
                        FS.HISFC.Models.MedicalPackage.Fee.Package tmp = al[i] as FS.HISFC.Models.MedicalPackage.Fee.Package;
                        //移除套餐
                        if (tmp.RecipeNO == oldChildPackage.RecipeNO &&
                            tmp.DelimitOper == oldChildPackage.DelimitOper &&
                            tmp.DelimitTime == oldChildPackage.DelimitTime &&
                            tmp.ID == oldChildPackage.ID)
                        {
                            //判断套餐状态
                            if (!string.IsNullOrEmpty(tmp.ID))
                            {
                                FS.HISFC.Models.MedicalPackage.Fee.Package packagetmp = new FS.HISFC.Models.MedicalPackage.Fee.Package();
                                packagetmp = this.package.QueryByID(tmp.ID);
                                if (packagetmp == null || packagetmp.Pay_Flag == "1" || packagetmp.Cancel_Flag == "1" || packagetmp.Cancel_Flag == "2")
                                {
                                    MessageBox.Show("当前套餐包状态已发生改变，请刷新后重试！");
                                    return;
                                }

                                if (this.package.DeleteByID(packagetmp.ID, packagetmp.Trans_Type) < 1)
                                {
                                    MessageBox.Show("删除划价套餐包失败，请刷新，原因：" + this.package.Err);
                                    return;
                                }
                            }

                            al.Remove(tmp);
                            break;
                        }
                    }
                }
            }
            if (!string.IsNullOrEmpty(OnePackageName))
            {
                if (package.ID == OnePackageName)
                {
                    ArrayList feepackage1 = this.hsRecipes[recipeNO] as ArrayList;
                    if (feepackage1.Count > 0)
                    {
                        MessageBox.Show(package.Name+ "该套餐只能单独收费！");
                        return;
                    }
                }
                else
                {
                    ArrayList feepackage1 = this.hsRecipes[recipeNO] as ArrayList;
                    foreach (FS.HISFC.Models.MedicalPackage.Fee.Package tmp in feepackage1)
                    {
                        if (tmp.PackageInfo.ID == OnePackageName)
                        {
                            MessageBox.Show(tmp.PackageInfo.Name+ "该套餐只能单独结算！");
                            return;
                        }
                    }
                }
            }

            if (this.hsRecipes.Contains(recipeNO))
            {
                ArrayList feepackageList = this.hsRecipes[recipeNO] as ArrayList;

                //分配临时套餐序号
                if (PackageFee.PackageInfo.PackageClass == "1")
                {

                    int packageSequence = 0;

                    foreach (FS.HISFC.Models.MedicalPackage.Fee.Package feepackage in feepackageList)
                    {
                        int i = FS.FrameWork.Function.NConvert.ToInt32(feepackage.PackageSequenceNO);
                        if (i > packageSequence)
                        {
                            packageSequence = i;
                        }
                    }

                    PackageFee.PackageSequenceNO = (packageSequence + 1).ToString();
                }
                else if (PackageFee.PackageInfo.PackageClass == "2")
                {
                    PackageFee.PackageSequenceNO = oldParentPackage.PackageSequenceNO;
                    PackageFee.ParentPackageInfo = oldParentPackage.PackageInfo.Clone();
                }

                feepackageList.Add(PackageFee);
            }
            else
            {
                ArrayList feepackage = new ArrayList();
                feepackage.Add(PackageFee);

                //分配临时套餐序号
                if (PackageFee.PackageInfo.PackageClass == "1")
                {
                    PackageFee.PackageSequenceNO = "1";
                }
                else if (PackageFee.PackageInfo.PackageClass == "2")
                {
                    PackageFee.PackageSequenceNO = oldParentPackage.PackageSequenceNO;
                    PackageFee.ParentPackageInfo = oldParentPackage.PackageInfo.Clone();
                }

                this.hsRecipes.Add(recipeNO, feepackage);
            }

            //{FAE56BB8-F958-411f-9663-CC359D6D494B}
            if (this.IsLevelModuleInUse && this.levelDiscountChecked && PackageFee.PackageInfo.PackageClass == "2")
            {
                PackageFee.Real_Cost = PackageFee.Real_Cost * this.levelDiscount;
                PackageFee.Etc_cost = PackageFee.Package_Cost - PackageFee.Real_Cost - PackageFee.Gift_cost;
            }


            this.ucchargeInfo.SetPackageInfo(PackageFee);
            if (PackageFee.PackageInfo.PackageClass == "1")
            {
                this.setChildPackageInfo(PackageFee);
                this.ucchargeInfo_PackageChange();
            }
            this.ucpatientInfo.SetRecipeCost(this.hsRecipes);
            this.ucchargeInfo_SetSelectorPosition(false, new Point());
            this.ucchargeInfo.Focus();
        }

        private void setChildPackageInfo(FS.HISFC.Models.MedicalPackage.Fee.Package PackageFee)
        {
            FS.HISFC.BizProcess.Integrate.MedicalPackage.Package packageProcess = new FS.HISFC.BizProcess.Integrate.MedicalPackage.Package();

            ArrayList childrenPackage = packageProcess.QueryPackageByParentCode(PackageFee.PackageInfo.ID);

            ArrayList feepackageList = this.hsRecipes[PackageFee.RecipeNO] as ArrayList;

            foreach (FS.HISFC.Models.MedicalPackage.Package pck in childrenPackage)
            {
                if (pck.MainFlag != "1" || !pck.IsValid)
                {
                    continue;
                }

                FS.HISFC.Models.MedicalPackage.Fee.Package feepackage = new FS.HISFC.Models.MedicalPackage.Fee.Package();
                feepackage.PackageInfo = pck;
                feepackage.Package_Cost = packageProcess.QueryTotFeeByPackge(pck.ID);

                if (this.IsLevelModuleInUse && this.levelDiscountChecked)
                {
                    feepackage.Real_Cost = feepackage.Package_Cost * this.levelDiscount;
                }
                else
                {
                    feepackage.Real_Cost = feepackage.Package_Cost;
                }
                feepackage.Gift_cost = 0.0m;
                feepackage.Etc_cost = feepackage.Package_Cost - feepackage.Real_Cost;
                feepackage.RecipeNO = PackageFee.RecipeNO;
                // PackageFee.Consultant = package.User02;
                feepackage.DelimitOper = FS.FrameWork.Management.Connection.Operator.ID;
                feepackage.DelimitTime = System.DateTime.Now;
                feepackage.PackageSequenceNO = PackageFee.PackageSequenceNO;
                feepackage.ParentPackageInfo = PackageFee.PackageInfo.Clone();

                feepackageList.Add(feepackage);
            }
        }

        #endregion

        #region 合同选择委托时间

        /// <summary>
        /// {2694417D-715F-4ef6-A664-1F92399DC325}
        /// 合同划价
        /// </summary>
        /// <param name="agreementArray"></param>
        /// <returns></returns>
        private int FrmAgreement_SetArrayListRes(ArrayList agreementArray)
        {
            this.agreementList = agreementArray;

            try
            {
                foreach (GZ.Model.Agreement model in agreementList)
                {
                    //套餐选择划价时不覆盖当前行
                    this.AddPriceRow();

                    FS.HISFC.Models.MedicalPackage.Package package = packageMgr.QueryPackageByID(model.PackageContext);
                    package.User01 = this.packageProcess.QueryTotFeeByPackge(package.ID).ToString();
                    package.User02 = getKind(package.PackageType);
                    package.User03 = getRange(package.UserType);


                    string recipeNO = string.Empty;
                    FS.HISFC.Models.MedicalPackage.Fee.Package PackageFee = new FS.HISFC.Models.MedicalPackage.Fee.Package();
                    recipeNO = this.ucpatientInfo.GetCurrentRecipe();

                    if (string.IsNullOrEmpty(recipeNO))
                    {
                        this.ucchargeInfo_SetSelectorPosition(false, new Point());
                        throw new Exception("未能获取划价单号，请检索患者或勾选当前右上角的划价单据！");
                    }

                    PackageFee.PackageInfo = package;
                    PackageFee.Package_Cost = Decimal.Parse(package.User01);
                    PackageFee.Real_Cost = model.AgreementMoney;
                    PackageFee.Gift_cost = 0.0m;
                    PackageFee.Etc_cost = PackageFee.Package_Cost - PackageFee.Real_Cost;
                    PackageFee.RecipeNO = recipeNO;
                    PackageFee.DelimitOper = FS.FrameWork.Management.Connection.Operator.ID;
                    PackageFee.DelimitTime = System.DateTime.Now;


                   // PackageFee.PackageInfo = package;
                    //PackageFee.Package_Cost = model .PackageMoney;
                    //PackageFee.Real_Cost = model.AgreementMoney;
                    //PackageFee.Gift_cost = 0.0m;
                    //PackageFee.Etc_cost = PackageFee.Package_Cost - PackageFee.Real_Cost;
                    //PackageFee.RecipeNO = recipeNO;
                    //PackageFee.DelimitOper = FS.FrameWork.Management.Connection.Operator.ID;
                    //PackageFee.DelimitTime = System.DateTime.Now;


                    FS.HISFC.Models.MedicalPackage.Fee.Package oldPackage = this.ucchargeInfo.GetCurrentFeeRow();
                    if (oldPackage != null)
                    {
                        ArrayList al = this.hsRecipes[oldPackage.RecipeNO] as ArrayList;

                        foreach (FS.HISFC.Models.MedicalPackage.Fee.Package tmp in al)
                        {
                            if (tmp.RecipeNO == oldPackage.RecipeNO &&
                                tmp.DelimitOper == oldPackage.DelimitOper &&
                                tmp.DelimitTime == oldPackage.DelimitTime &&
                                tmp.ID == oldPackage.ID)
                            {
                                al.Remove(tmp);
                                break;
                            }
                        }
                    }

                    if (this.hsRecipes.Contains(recipeNO))
                    {
                        ArrayList feepackage = this.hsRecipes[recipeNO] as ArrayList;
                        feepackage.Add(PackageFee);
                    }
                    else
                    {
                        ArrayList feepackage = new ArrayList();
                        feepackage.Add(PackageFee);
                        this.hsRecipes.Add(recipeNO, feepackage);
                    }

                    this.ucpatientInfo.SetRecipeCost(this.hsRecipes);
                    this.ucchargeInfo.SetPackageInfo(PackageFee);
                    this.ucchargeInfo_SetSelectorPosition(false, new Point());
                    this.ucchargeInfo.Focus();

                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }

            return 1;
        }



        private string getKind(string kindID)
        {
            foreach (FS.HISFC.Models.Base.Const cst in categoryList)
            {
                if (kindID == cst.ID)
                {
                    return cst.Name;
                }
            }

            return string.Empty;
        }

        private string getRange(FS.HISFC.Models.Base.ServiceTypes type)
        {
            string rtn = string.Empty;
            switch (type)
            {
                case FS.HISFC.Models.Base.ServiceTypes.C:
                    rtn = "门诊";
                    break;
                case FS.HISFC.Models.Base.ServiceTypes.I:
                    rtn = "住院";
                    break;
                case FS.HISFC.Models.Base.ServiceTypes.T:
                    rtn = "体检";
                    break;
                case FS.HISFC.Models.Base.ServiceTypes.A:
                    rtn = "全部";
                    break;
                default:
                    break;
            }

            return rtn;
        }

        #endregion

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
            _toolBarService.AddToolButton("刷卡", "刷卡", (int)FS.FrameWork.WinForms.Classes.EnumImageList.B报警, true, false, null);
            _toolBarService.AddToolButton("身份证", "身份证", (int)FS.FrameWork.WinForms.Classes.EnumImageList.S设备, true, false, null);
            //{CCA8788A-A686-4309-81AA-3EB4D74EBCE4} 添加合同计算器
            _toolBarService.AddToolButton("合同查看", "合同查看", (int)FS.FrameWork.WinForms.Classes.EnumImageList.B病历, true, false, null);
            _toolBarService.AddToolButton("合同计算器", "合同计算器", (int)FS.FrameWork.WinForms.Classes.EnumImageList.J计算器, true, false, null);
            _toolBarService.AddToolButton("确认收费", "确认收费", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q确认收费, true, false, null);
            _toolBarService.AddToolButton("划价保存", "划价保存", (int)FS.FrameWork.WinForms.Classes.EnumImageList.H划价保存, true, false, null);
            _toolBarService.AddToolButton("缴纳押金", "缴纳押金", (int)FS.FrameWork.WinForms.Classes.EnumImageList.J计划单, true, false, null);
            _toolBarService.AddToolButton("套餐折扣", "套餐折扣", (int)FS.FrameWork.WinForms.Classes.EnumImageList.J计算器, true, false, null);
            _toolBarService.AddToolButton("增加", "增加", (int)FS.FrameWork.WinForms.Classes.EnumImageList.T添加, true, false, null);
            _toolBarService.AddToolButton("删除", "删除", (int)FS.FrameWork.WinForms.Classes.EnumImageList.S删除, true, false, null);
            _toolBarService.AddToolButton("清屏", "清屏", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q清空, true, false, null);
            _toolBarService.AddToolButton("套餐查询", "套餐查询", (int)FS.FrameWork.WinForms.Classes.EnumImageList.T套餐, true, false, null);
            return _toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            this.ucpayInfo.Commit();

            switch (e.ClickedItem.Text)
            {
                case "刷卡":
                    string cardNo = "";
                    string error = "";
                    if (FS.HISFC.Components.Registration.Function.OperMCard(ref cardNo, ref error) == -1)
                    {
                        MessageBox.Show(error,"错误");
                        return;
                    }
                    cardNo = ";" + cardNo;
                    FS.HISFC.Models.Account.AccountCard accountCard = new FS.HISFC.Models.Account.AccountCard();
                    if (feeIntegrate.ValidMarkNO(cardNo, ref accountCard) > 0)
                    {
                        this.ucpatientInfo.setCardNO(accountCard.Patient.PID.CardNO);
                    }
                    else 
                    {
                        MessageBox.Show("未找到患者信息！");
                    }
                    break;
                case "身份证":
                    MessageBox.Show("暂未开通接口！");
                    break;
                case "合同查看":
                    this.ChooseAgreement();
                    break;
                case "合同计算器":////{CCA8788A-A686-4309-81AA-3EB4D74EBCE4}添加合同计算
                    FrmCalc fc = new FrmCalc();
                    fc.Show();
                    break;
                case "确认收费":
                    this.Save();
                    break;
                case "划价保存":
                    this.SavePrice();
                    break;
                case "缴纳押金":
                    this.SaveDeposit();
                    break;
                case "套餐折扣":
                    this.Discount();
                    break;
                case "增加":
                    this.AddPackageRow();
                    break;
                case "删除":
                    this.DelPackageRow();
                    break;
                case "清屏":
                    this.Clear();
                    break;
                case "套餐查询":
                    this.PackageQuery();// {F53BD032-1D92-4447-8E20-6C38033AA607}
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 按键
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.ucchargeInfo_SetSelectorPosition(false, new Point());
            }
            return base.ProcessDialogKey(keyData);
        }
        /// <summary>
        /// 套餐查询// {F53BD032-1D92-4447-8E20-6C38033AA607}
        /// </summary>
        private void PackageQuery()
        {
            if (this.ucpatientInfo.PatientInfo == null || string.IsNullOrEmpty(this.ucpatientInfo.PatientInfo.PID.CardNO))
            {
                MessageBox.Show("请先检索患者！");
                return;
            }

            FS.HISFC.Components.Common.Forms.frmPackageQuery frmpackage = new FS.HISFC.Components.Common.Forms.frmPackageQuery();
            frmpackage.PatientInfo = accountMgr.GetPatientInfoByCardNO(this.ucpatientInfo.PatientInfo.PID.CardNO);
            frmpackage.ShowDialog();
        }

        #region 菜单按钮事件

        /// <summary>
        /// 收费
        /// </summary>
        private void Save()
        {
            //患者信息
            FS.HISFC.Models.RADT.PatientInfo PatientInfo = this.ucpatientInfo.PatientInfo;
            bool isIncludeUncouponPackage = false;  //是否包含不进行积分的套餐
            //{3B9D0100-377E-48ac-AF31-4412CDCBF4B0}
            bool isIncludeUncouponPackageDLR = false;  //是否包含不进行积分的套餐
           
            PatientInfo.Memo = this.ucpatientInfo.Memo;

            //{E8D52AF4-08BF-489e-A303-1587BEDAEB72}
            FS.FrameWork.Models.NeuObject uncouponPackage = constantMgr.GetConstant("UNCOUPONPACKAGE", "1");
            //{3B9D0100-377E-48ac-AF31-4412CDCBF4B0}
            FS.FrameWork.Models.NeuObject unconponPackageDLR = constantMgr.GetConstant("UNCOUPONPACKAGEDLR", "1");

            if (PatientInfo == null)
            {
                MessageBox.Show("请先检索患者！");
                return;
            }

            if (string.IsNullOrEmpty(PatientInfo.Pact.User01))
            {
                //MessageBox.Show("请选择患者的会员等级！");
                //return;
                PatientInfo.Pact.User01 = "1";
            }

            if (string.IsNullOrEmpty(PatientInfo.Pact.ID))
            {
                //MessageBox.Show("请选择患者的合同单位！");
                //return;
                PatientInfo.Pact.ID = "1";
            }

            //当前记录
            //{409E5137-13A9-4ee0-8AF7-01BCD3B09D36}
            ArrayList checkedpackages = this.ucchargeInfo.GetFeeRow();

            //{A777B7DF-AB62-4603-A0F6-B3643AD442F0}
            //{56809DCA-CD5A-435e-86F0-93DE99227DF4}
            ArrayList checkedParentPackages = this.ucchargeInfo.GetPackageSelected();

            foreach (FS.HISFC.Models.MedicalPackage.Fee.Package parentPackage in checkedParentPackages)
            {
                foreach (FS.HISFC.Models.MedicalPackage.Fee.Package childPackage in checkedpackages)
                {
                    if (childPackage.PackageSequenceNO == parentPackage.PackageSequenceNO &&
                       childPackage.ParentPackageInfo.ID == parentPackage.PackageInfo.ID &&
                        childPackage.RecipeNO == parentPackage.RecipeNO)
                    {
                        childPackage.SpecialFlag = parentPackage.SpecialFlag;
                    }
                }
            }

            if (checkedpackages == null)
            {
                MessageBox.Show("获取结算套餐出错！");
                return;
            }

            string consultant = this.ucpatientInfo.GetConsultant();//07ECD432-CA49-42f8-AFCB-596D3B670CB6
            if (string.IsNullOrEmpty(consultant))
            {

                MessageBox.Show("现场咨询人员必填！");   //{e004697d-c4ec-4191-8b73-3d9ea2542f99}
                return;

                //if (MessageBox.Show("现场咨询人员为空！是否继续？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                //{

                //    //{77DEDA90-DB9A-4fdd-9487-2F9E5341A208}
                //    //MessageBox.Show("现场咨询人员必填！");

                //    return;
                //}
            }

            foreach (FS.HISFC.Models.MedicalPackage.Fee.Package package in checkedpackages)
            {
                package.Consultant = consultant;

                if (this.ucpatientInfo.IsBLPackage)
                {
                    package.Memo = "TCBL";
                }
                else
                {
                    package.Memo = "";
                }

                //{EBF40A28-2C88-4391-B4DC-A85D423729F8}
                if (uncouponPackage.Name.Contains("|" + package.PackageInfo.PackageType + "|"))
                {
                    isIncludeUncouponPackage = true;
                }

                //{3B9D0100-377E-48ac-AF31-4412CDCBF4B0}
                if (unconponPackageDLR.Name.Contains("|" + package.PackageInfo.PackageType + "|") || package.SpecialFlag == "1")
                {
                    isIncludeUncouponPackageDLR = true;
                }
            }

            if (checkedpackages.Count == 0)
            {
                MessageBox.Show("没有找到需要进行收费明细！");
                return;
            }

            ArrayList payModeInfo = this.ucpayInfo.GetPayModeInfo();
            if (payModeInfo == null)//{B7E5CA30-EC53-4fea-BB60-55B8D8DE3CAB} 判断应收其他是否填写了备注信息
            {
                return;
            }
            ArrayList depositInfo = this.ucpayInfo.GetDepositInfo();
            Hashtable costInfo = this.ucpayInfo.HsPayCost;
            string ErrInfo = string.Empty;
            //{F31B0DE2-C48A-4a86-A917-43930C602D52}
            string InvoiceNO = string.Empty;

            ArrayList depositPay = new ArrayList();

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在保存。。。");
            //{F31B0DE2-C48A-4a86-A917-43930C602D52}
            if (this.packageFeeProcess.SaveFee(PatientInfo, checkedpackages, payModeInfo, depositInfo, costInfo,this.hsSpecialPackageDetail, ref depositPay,ref ErrInfo, ref InvoiceNO) < 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show(ErrInfo);
                return;
            }

            //更新合同状态
            if (this.agreementList != null)
            {
                foreach (GZ.Model.Agreement model in this.agreementList)
                {
                    if (model == null)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        MessageBox.Show("更新合同状态失败");
                        return;
                    }

                    model.AgreementStatus = "3";
                    model.TollCollectorID = FS.FrameWork.Management.Connection.Operator.ID;
                    model.TollCollectorDate = this.agreementMgr.GetSysDateTime();
                    if (agreementMgr.UpdateToll(model) != 1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        MessageBox.Show("更新合同状态失败");
                        return;
                    }
                }

                this.agreementList.Clear();
            }


            //{333D2AD8-DC4A-4c30-A14E-D6815AC858F9}
            decimal costCouponAmount = 0.0m;
            decimal operateCouponAmount = 0.0m;

            FS.FrameWork.Models.NeuObject cashCouponPayMode = constantMgr.GetConstant("XJLZFFS", "1");

            //{3B9D0100-377E-48ac-AF31-4412CDCBF4B0}
            decimal delegateCouponAmount = 0.0m;
            FS.FrameWork.Models.NeuObject delegateCouponPayMode = constantMgr.GetConstant("DLRZFFS", "1");

            foreach (FS.HISFC.Models.MedicalPackage.Fee.PayMode pay in payModeInfo)
            {
                if (pay.Mode_Code == "CO")
                {
                    costCouponAmount += pay.Tot_cost;
                }

                if (cashCouponPayMode.Name.Contains(pay.Mode_Code.ToString()) || (pay.Mode_Code == "DE" && cashCouponPayMode.Name.Contains(pay.Related_ModeCode.ToString())))
                {
                    operateCouponAmount += pay.Tot_cost;
                }

                if (delegateCouponPayMode.Name.Contains(pay.Mode_Code.ToString()) || (pay.Mode_Code == "DE" && delegateCouponPayMode.Name.Contains(pay.Related_ModeCode.ToString())))
                {
                    delegateCouponAmount += pay.Tot_cost;
                }
            }

            foreach (FS.HISFC.Models.MedicalPackage.Fee.PayMode pay in depositPay)
            {
                if (pay.Mode_Code == "CO")
                {
                    costCouponAmount += pay.Tot_cost;
                }

                if (cashCouponPayMode.Name.Contains(pay.Mode_Code.ToString()) || (pay.Mode_Code == "DE" && cashCouponPayMode.Name.Contains(pay.Related_ModeCode.ToString())))
                {
                    operateCouponAmount += pay.Tot_cost;
                }
            }

            //{3B9D0100-377E-48ac-AF31-4412CDCBF4B0}
            //若套餐中包含不进行代理人积分的套餐类型，则整单不算
            if (isIncludeUncouponPackageDLR)
            {
                delegateCouponAmount = 0;
            }

            if (IsCouponModuleInUse)
            {
                //{333D2AD8-DC4A-4c30-A14E-D6815AC858F9}
                string resultCode = "0";
                string errorMsg = "";
                int reqRtn = -1;

                //消费的积分
                if (costCouponAmount != 0)
                {
                    reqRtn = FS.HISFC.BizProcess.Integrate.WSHelper.CostCoupon(PatientInfo.PID.CardNO, PatientInfo.Name, PatientInfo.PID.CardNO, PatientInfo.Name, "TCSF", InvoiceNO, costCouponAmount, 0.0m, out resultCode, out errorMsg);
                    if (reqRtn < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        MessageBox.Show("处理会员积分出错：" + errorMsg);
                        return;
                    }
                }

                //{E8D52AF4-08BF-489e-A303-1587BEDAEB72}
                //本单产生的积分
                if (operateCouponAmount != 0 && !isIncludeUncouponPackage)
                {
                    //{3B9D0100-377E-48ac-AF31-4412CDCBF4B0}
                    reqRtn = FS.HISFC.BizProcess.Integrate.WSHelper.OperateCoupon(PatientInfo.PID.CardNO, PatientInfo.Name, "TCSF", InvoiceNO, operateCouponAmount, delegateCouponAmount, out resultCode, out errorMsg);
                    if (reqRtn < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        MessageBox.Show("处理会员积分出错:" + errorMsg);

                        //回滚消费的积分
                        if (costCouponAmount != 0)
                        {
                            reqRtn = FS.HISFC.BizProcess.Integrate.WSHelper.CostCoupon(PatientInfo.PID.CardNO, PatientInfo.Name, PatientInfo.PID.CardNO, PatientInfo.Name, "TCTF", InvoiceNO, -costCouponAmount, 0.0m, out resultCode, out errorMsg);

                            if (reqRtn < 0)
                            {
                                MessageBox.Show("回滚会员积分出错，请联系信息科处理，错误详情:" + errorMsg);
                            }
                        }

                        return;
                    }
                }
            }

            

            FS.FrameWork.Management.PublicTrans.Commit();
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            MessageBox.Show("收费成功！");

            #region 收费数据统计节点{7930AB5C-6E33-4855-87E8-B87749639B88}
            //套餐收费数据统计节点是否启用-CPP001
            if (this.IsFamilyFirstPackageInUse)
            {
                FS.HISFC.BizProcess.Interface.StatisticsPoint.IStatisticsPoint packageChargeStatistics = new FS.HISFC.BizProcess.Integrate.StatisticsPoint.PackageChargeStatisticsPoint();
                packageChargeStatistics.SetPatient(PatientInfo);
                packageChargeStatistics.DoStatistics();
            }

            if (this.IsPackageDealInCrm)
            {
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在处理套餐核销。。。");
                string req = @"
                        <req>
                          <crmId>{0}</crmId>
                          <invoiceNo>{1}</invoiceNo>
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

                    req = string.Format(req, PatientInfo.CrmID, InvoiceNO);

                    FS.HISFC.BizProcess.Integrate.IbornMobileService server = new FS.HISFC.BizProcess.Integrate.IbornMobileService();
                    //server.Url = url;
                    //server.Url = "http://localhost:8080/ibornMobileService.asmx";
                    //string res = server.PackageExeceteInCrm(req);

                    //换成传统的调用方式{14B9C3EE-70B6-46df-B279-B8F3487519C4}
                    //string res = FS.HISFC.BizProcess.Integrate.WSHelper.InvokeWebService("http://localhost:8080/ibornMobileService.asmx", "PackageExeceteInCrm", new string[] { req }) as string;
                    string res = FS.HISFC.BizProcess.Integrate.WSHelper.InvokeWebService(url, "PackageExeceteInCrm", new string[] { req }) as string;
                    //MessageBox.Show(res);
                    //{27FF6780-BD63-4b8f-B7F0-38623EF0BB46}
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
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }
            #endregion

            ////{F31B0DE2-C48A-4a86-A917-43930C602D52}
            this.Print(InvoiceNO);

            if (MessageBox.Show("是否进行入院登记", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                FS.SOC.Local.Order.ZhuHai.ZDWY.OutPatient.PrePayIn.ucPrepayIn ucprepayin = new FS.SOC.Local.Order.ZhuHai.ZDWY.OutPatient.PrePayIn.ucPrepayIn();
                ucprepayin.PatientInfo = this.ucpatientInfo.PatientInfo;
                ucprepayin.IsShowButton = true;
                FS.FrameWork.WinForms.Classes.Function.PopShowControl(ucprepayin);
            }

            ArrayList tmp = this.ucpatientInfo_GetPatientRecipeList();
            if (tmp != null && tmp.Count > 0)
            {
                if (MessageBox.Show("存在未收费的划价单，是否继续收费！", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    this.ucpatientInfo.PatientInfo = this.ucpatientInfo.PatientInfo;
                    return;
                }
            }

            this.Clear();
        }

        /// <summary>
        /// {F31B0DE2-C48A-4a86-A917-43930C602D52}
        /// 打印挂号发票
        /// </summary>
        /// <param name="regObj"></param>
        private void Print(string InvoiceNO)
        {
            FS.HISFC.BizProcess.Interface.MedicalPackage.IPackageInvoice packageinvoiceprint = null;
            packageinvoiceprint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.MedicalPackage.IPackageInvoice)) as FS.HISFC.BizProcess.Interface.MedicalPackage.IPackageInvoice;
            if (packageinvoiceprint == null)
            {
                MessageBox.Show(FS.FrameWork.WinForms.Classes.UtilInterface.Err, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            packageinvoiceprint.SetPrintValue(InvoiceNO);
            packageinvoiceprint.Print();
        }

        /// <summary>
        /// 划价
        /// </summary>
        private void SavePrice()
        {
            //患者信息
            FS.HISFC.Models.RADT.PatientInfo PatientInfo = this.ucpatientInfo.PatientInfo;

            if (PatientInfo == null)
            {
                MessageBox.Show("请先检索患者！");
                return;
            }

            if (string.IsNullOrEmpty(PatientInfo.Pact.User01))
            {
                //MessageBox.Show("请选择患者的会员等级！");
                //return;
                PatientInfo.Pact.User01 = "1";
            }

            if (string.IsNullOrEmpty(PatientInfo.Pact.ID))
            {
                //MessageBox.Show("请选择患者的合同单位！");
                //return;
                PatientInfo.Pact.ID = "1";
            }

            //当前勾选的划价记录
            ArrayList checkedpackages = this.ucchargeInfo.GetCurrentPackageFeeRow(true);
            ArrayList uncheckpackages = this.ucchargeInfo.GetCurrentPackageFeeRow(false);
            string ErrInfo = string.Empty;

            if (checkedpackages.Count == 0 && uncheckpackages.Count == 0)
            {
                MessageBox.Show("没有找到需要进行划价的费用！");
                return;
            }

            ArrayList checkedChildPackages = new ArrayList();
            ArrayList uncheckChildPackages = new ArrayList();

            foreach (FS.HISFC.Models.MedicalPackage.Fee.Package checkedPackage in checkedpackages)
            {
                ArrayList checkedList = hsRecipes[checkedPackage.RecipeNO] as ArrayList;

                foreach (FS.HISFC.Models.MedicalPackage.Fee.Package detail in checkedList)
                {
                    if (detail.RecipeNO == checkedPackage.RecipeNO && detail.PackageSequenceNO == checkedPackage.PackageSequenceNO && detail.PackageInfo.PackageClass == "2")
                    {
                        checkedChildPackages.Add(detail);
                    }
                }
            }

            foreach (FS.HISFC.Models.MedicalPackage.Fee.Package uncheckedPackage in uncheckpackages)
            {
                ArrayList uncheckedList = hsRecipes[uncheckedPackage.RecipeNO] as ArrayList;

                foreach (FS.HISFC.Models.MedicalPackage.Fee.Package detail in uncheckedList)
                {
                    if (detail.RecipeNO == uncheckedPackage.RecipeNO && detail.PackageSequenceNO == uncheckedPackage.PackageSequenceNO && detail.PackageInfo.PackageClass == "2")
                    {
                        uncheckChildPackages.Add(detail);
                    }
                }
            }

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在划价。。。");
            if (this.packageFeeProcess.SavePrice(PatientInfo, checkedChildPackages, uncheckChildPackages, ref ErrInfo) < 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show(ErrInfo);
                return;
            }

            //更新合同状态
            if (this.agreementList != null)
            {
                foreach (GZ.Model.Agreement model in this.agreementList)
                {
                    if (model == null)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        MessageBox.Show("更新合同状态失败");
                        return;
                    }

                    model.AgreementStatus = "3";
                    model.TollCollectorID = FS.FrameWork.Management.Connection.Operator.ID;
                    model.TollCollectorDate = this.agreementMgr.GetSysDateTime();
                    if (agreementMgr.UpdateToll(model) != 1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        MessageBox.Show("更新合同状态失败");
                        return;
                    }
                }

                this.agreementList.Clear();
            }

            FS.FrameWork.Management.PublicTrans.Commit();
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            MessageBox.Show("划价成功！");

            this.Clear();
        }

        /// <summary>
        /// 交押金
        /// </summary>
        private void SaveDeposit()
        {
            this.FrmDeposit.Clear();
            if (this.ucpatientInfo.AccountCardInfo == null ||
               this.ucpatientInfo.PatientInfo == null)
            {
                MessageBox.Show("请输入患者信息");
                return;
            }

            this.FrmDeposit.AccountCardInfo = this.ucpatientInfo.AccountCardInfo;
            this.FrmDeposit.PatientInfo = this.ucpatientInfo.PatientInfo;
            this.FrmDeposit.ShowDialog();
            this.ucpayInfo.PatientInfo = this.ucpatientInfo.PatientInfo;
        }

        private void ChooseAgreement()
        {
            this.FrmAgreement.Clear();
            if (this.ucpatientInfo.AccountCardInfo == null ||
               this.ucpatientInfo.PatientInfo == null)
            {
                MessageBox.Show("请先检索患者信息");
                return;
            }
            this.FrmAgreement.PatientInfo = this.ucpatientInfo.PatientInfo;
            this.FrmAgreement.ShowDialog();
        }

        /// <summary>
        /// 折扣
        /// </summary>
        private void Discount()
        {
            Forms.frmDiscount frmdiscount = new HISFC.Components.Package.Fee.Forms.frmDiscount();

            frmdiscount.packageList = this.ucchargeInfo.GetCurrentFeeRow(true);

            if (frmdiscount.packageList == null || frmdiscount.packageList.Count == 0)
            {
                MessageBox.Show("不存在需要划价的单据！");
                return;
            }

            if (frmdiscount.Init())
            {
                if (frmdiscount.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
            }
            else
            {
                MessageBox.Show("初始化折扣窗体处错！");
                return;
            }

            //{FAE56BB8-F958-411f-9663-CC359D6D494B}
            if (this.IsLevelModuleInUse && this.levelDiscountChecked)
            {

                foreach (FS.HISFC.Models.MedicalPackage.Fee.Package obj in frmdiscount.packageList)
                {
                    if (obj.PackageInfo.PackageClass == "2")
                    {
                        obj.Real_Cost = obj.Real_Cost * this.levelDiscount;
                        obj.Etc_cost = obj.Package_Cost - obj.Real_Cost - obj.Gift_cost;
                    }
                }
            }

            ArrayList parents = this.ucchargeInfo.GetAllPackageFeeRow();

            foreach (FS.HISFC.Models.MedicalPackage.Fee.Package currentParent in parents)
            {
                if (currentParent != null && !string.IsNullOrEmpty(currentParent.RecipeNO))
                {

                    ArrayList feeList = this.hsRecipes[currentParent.RecipeNO] as ArrayList;

                    currentParent.Package_Cost = 0.0m;
                    currentParent.Real_Cost = 0.0m;
                    currentParent.Gift_cost = 0.0m;
                    currentParent.Etc_cost = 0.0m;

                    foreach (FS.HISFC.Models.MedicalPackage.Fee.Package obj in feeList)
                    {
                        if (obj.PackageInfo.PackageClass == "2" &&
                            obj.PackageSequenceNO == currentParent.PackageSequenceNO &&
                            obj.ParentPackageInfo.ID == currentParent.PackageInfo.ID)
                        {
                            currentParent.Package_Cost += obj.Package_Cost;
                            currentParent.Real_Cost += obj.Real_Cost;
                            currentParent.Gift_cost += obj.Gift_cost;
                            currentParent.Etc_cost += obj.Etc_cost;
                        }
                    }
                }
            }

            this.ucchargeInfo.RefreshParentData();
            this.ucchargeInfo.RefreshData();
            //this.ucpatientInfo_CurrentRecipeChange();
            this.ucchargeInfo_SetFeeInfoCost();
        }
        
        //{FAE56BB8-F958-411f-9663-CC359D6D494B}
        /// <summary>
        /// 获取患者会员等级折扣
        /// </summary>
        /// <param name="discount"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        private int getAccountDiscount()
        {
            if (this.ucpatientInfo.PatientInfo == null)
            {
                return 1;
            }

            string resultCode = "0";
            string errMsg = string.Empty;
            this.levelID = "0";
            this.levelName = "普通会员";
            this.levelDiscount = 1m;

            if (FS.HISFC.BizProcess.Integrate.WSHelper.QueryAccountDiscount(this.ucpatientInfo.PatientInfo.PID.CardNO, out levelDiscount, out levelID, out levelName, out resultCode, out errMsg) < 0)
            {
                MessageBox.Show("查询会员等级失败,患者无法享受等级折扣,错误详情:" + errMsg);
                return -1;
            }

            this.ucpatientInfo.SetDiscountInfo(this.levelDiscount, this.levelID, this.levelName);

            return 1;
        }

        /// <summary>
        /// 增加套餐记录
        /// </summary>
        private void AddPackageRow()
        {
            this.ucchargeInfo.AddPackageRow();
        }

        /// <summary>
        /// 增加套餐记录
        /// </summary>
        private void DelPackageRow()
        {
            FS.HISFC.Models.MedicalPackage.Fee.Package currFee = this.ucchargeInfo.GetCurrentParentFeeRow();
            if (currFee != null)
            {
                ArrayList feeList = this.hsRecipes[currFee.RecipeNO] as ArrayList;
                if (feeList == null || feeList.Count == 0)
                {
                    MessageBox.Show("删除划价套餐失败,请清空操作界面重试！");
                    return;
                }

                for (int i = 0; i < feeList.Count; i++ )
                {
                    FS.HISFC.Models.MedicalPackage.Fee.Package package = feeList[i] as FS.HISFC.Models.MedicalPackage.Fee.Package;
                    if (package.RecipeNO == currFee.RecipeNO &&
                        package.PackageSequenceNO == currFee.PackageSequenceNO)
                    {
                        if (!string.IsNullOrEmpty(package.ID))
                        {
                            //{50A7C61C-DE80-415e-BCA9-A6D083510B9B}
                            //判断明细状态
                            FS.HISFC.Models.MedicalPackage.Fee.Package packagecheck = new FS.HISFC.Models.MedicalPackage.Fee.Package();
                            packagecheck = this.package.QueryByID(package.ID);
                            if (packagecheck == null || packagecheck.Pay_Flag == "1" || packagecheck.Cancel_Flag == "1" || packagecheck.Cancel_Flag == "2")
                            {
                                MessageBox.Show("当前明细状态已发生改变，请刷新后重试！");
                                return;
                            }

                            if (this.package.DeleteByID(package.ID, package.Trans_Type) < 1)
                            {
                                MessageBox.Show("删除划价明细失败，原因：" + this.package.Err);
                                return;
                            }
                        }
                        feeList.Remove(package);
                        i--;
                    }
                }
            }

            this.ucpatientInfo.SetRecipeCost(this.hsRecipes);
            this.ucchargeInfo.DelPackageRow();
        }

        /// <summary>
        /// 增加划价记录
        /// </summary>
        private void AddPriceRow()
        {
            this.ucchargeInfo.AddPriceRow();
        }


        /// <summary>
        /// 删除划价记录
        /// </summary>
        private void DelPriceRow()
        {
            FS.HISFC.Models.MedicalPackage.Fee.Package currFee = this.ucchargeInfo.GetCurrentFeeRow();
            if (currFee != null)
            {
                ArrayList feeList = this.hsRecipes[currFee.RecipeNO] as ArrayList;
                if (feeList == null || feeList.Count == 0)
                {
                    MessageBox.Show("删除划价明细失败,请清空操作界面重试！");
                    return;
                }

                foreach (FS.HISFC.Models.MedicalPackage.Fee.Package package in feeList)
                {
                    if (package.RecipeNO == currFee.RecipeNO &&
                        package.DelimitOper == currFee.DelimitOper &&
                        package.DelimitTime == currFee.DelimitTime &&
                        package.ID == currFee.ID)
                    {
                        if (!string.IsNullOrEmpty(currFee.ID))
                        {
                            //{50A7C61C-DE80-415e-BCA9-A6D083510B9B}
                            //判断明细状态
                            FS.HISFC.Models.MedicalPackage.Fee.Package packagecheck = new FS.HISFC.Models.MedicalPackage.Fee.Package();
                            packagecheck = this.package.QueryByID(package.ID);
                            if (packagecheck == null || packagecheck.Pay_Flag == "1" || packagecheck.Cancel_Flag == "1" || packagecheck.Cancel_Flag == "2")
                            {
                                MessageBox.Show("当前明细状态已发生改变，请刷新后重试！");
                                return;
                            }

                            if (this.package.DeleteByID(package.ID, package.Trans_Type) < 1)
                            {
                                MessageBox.Show("删除划价明细失败，原因：" + this.package.Err);
                                return;
                            }
                        }
                        feeList.Remove(package);
                        break;
                    }
                }
            }

            this.ucpatientInfo.SetRecipeCost(this.hsRecipes);
            this.ucchargeInfo.DelPriceRow();
        }

        /// <summary>
        /// 清屏
        /// </summary>
        private void Clear()
        {
            this.ucpatientInfo.Clear();
            this.ucchargeInfo.Clear();
            this.ucpayInfo.Clear();
        }

        #endregion
    }
}
