using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Management;
using FS.HISFC.Models.Fee.Outpatient;
using FS.FrameWork.Function;
using FS.HISFC.Models.Base;

namespace FS.SOC.Local.OutpatientFee.FoSi.Forms
{
    public partial class frmBalanceAddUp : Form
    {
        #region 变量

        FS.HISFC.BizProcess.Integrate.FeeInterface.IOutpatientFeeList outPatientFeeList = null;
        /// <summary>
        /// 门诊费用业务层
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.Outpatient outpatientManager = new FS.HISFC.BizLogic.Fee.Outpatient();
        /// <summary>
        /// 控制参数业务层
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
        /// <summary>
        /// 非药品业务层
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.Item undrugManager = new FS.HISFC.BizLogic.Fee.Item();
        /// <summary>
        /// 非药品组合项目业务层
        /// </summary>
        FS.HISFC.BizLogic.Fee.UndrugPackAge undrugPackAgeManager = new FS.HISFC.BizLogic.Fee.UndrugPackAge();
        /// <summary>
        /// 医嘱业务层
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Order orderIntegrate = new FS.HISFC.BizProcess.Integrate.Order();
        /// <summary>
        /// 费用综合业务层
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();
        /// <summary>
        /// 合同单位比例管理业务层
        /// </summary>
        FS.HISFC.BizLogic.Fee.PactUnitItemRate pactUnitItemRateManager = new FS.HISFC.BizLogic.Fee.PactUnitItemRate();

        /// <summary>
        /// 门诊账户业务层
        /// {8B5D1E31-3BD5-4003-B35C-25D920B0D9EE}
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.Account accountManager = new FS.HISFC.BizLogic.Fee.Account();

        #region 医疗待遇接口变量

        /// <summary>
        /// 医疗待遇接口
        /// </summary>
        FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy medcareInterfaceProxy = new FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy();
        /// <summary>
        /// 费用实体流水号 -- 费用实体
        /// </summary>
        Dictionary<string, FS.FrameWork.Models.NeuObject> dictFeeInfo = new Dictionary<string, FS.FrameWork.Models.NeuObject>();
        /// <summary>
        /// 挂号流水号 -- 费用明细列表
        /// </summary>
        Dictionary<string, ArrayList> dictFeeDetial = new Dictionary<string, ArrayList>();
        /// <summary>
        /// 是否本地结算
        /// </summary>
        bool isLocalProcess = false;

        #endregion

        private System.Data.DataTable dtItem =  null;

        private bool isCanFeeWhenTotCostDiff = false;

        #endregion

        #region 属性
        /// <summary>
        /// 收费项目字典内容
        /// </summary>
        public System.Data.DataTable DTItem
        {
            get { return dtItem; }
            set { dtItem = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsCanFeeWhenTotCostDiff
        {
            get { return isCanFeeWhenTotCostDiff; }
            set { isCanFeeWhenTotCostDiff = value; }
        }

        public FS.HISFC.BizProcess.Integrate.FeeInterface.IOutpatientFeeList OutPatientFeeList
        {
            get { return this.outPatientFeeList; }
            set { this.outPatientFeeList = value;}
        }

        public bool IsLocalProcess
        {
            get { return this.isLocalProcess; }
            set { this.isLocalProcess = value; }
        }
        #endregion 

        #region 接口
        /// <summary>
        /// 外屏接口
        /// </summary>
        //protected FS.HISFC.BizProcess.Interface.FeeInterface.IMultiScreen iMultiScreen = null;

         #endregion

        #region IOutpatientOtherInfomationRight 成员

        private FS.HISFC.BizProcess.Interface.FeeInterface.IMultiScreen iMultiScreen;
        public FS.HISFC.BizProcess.Interface.FeeInterface.IMultiScreen MultiScreen
        {
            set { iMultiScreen = value; }
        }

        /// <summary>
        /// 费用取整接口
        /// </summary>
        protected FS.HISFC.BizProcess.Interface.FeeInterface.IOutPatientFeeRoundOff iOutPatientFeeRoundOff = null;
        /// <summary>
        /// 计算Lis试管接口
        /// </summary>
        protected FS.HISFC.BizProcess.Interface.FeeInterface.ILisCalculateTube iLisCalculateTube = null;

        #endregion

        public frmBalanceAddUp()
        {
            InitializeComponent();
        }

        private void frmBalanceAddUp_Load(object sender, EventArgs e)
        {
            txtCount.Text = "2";
            SetFocus();
        }

        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Left:
                case Keys.Right:
                case Keys.Down:
                case Keys.Up:
                    ProcessKey(keyData);
                    return true;

                default:
                    return base.ProcessCmdKey(ref msg, keyData);
            }


            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void ProcessKey(Keys keyData)
        {
            string strCount = txtCount.Text.Trim();

            int iCount = 0;
            int.TryParse(strCount, out iCount);
            if (iCount < 0)
            {
                iCount = 0;
            }

            switch (keyData)
            {
                case Keys.Up:
                case Keys.Right:
                    iCount++;
                    break;

                case Keys.Down:
                case Keys.Left:
                    iCount--;
                    if (iCount <= 0)
                    {
                        iCount = 1;
                    }
                    break;
            }

            txtCount.Text = iCount.ToString();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Clear()
        {
            tbPubCost.Text = "";
            tbOwnCost.Text = "";
            tbRealOwnCost.Text = "";
            tbReturnCost.Text = "";

            SetFocus();
        }
        private void SetView(decimal decPubCost, decimal decOwnCost)
        {
            tbPubCost.Text = decPubCost.ToString();
            tbOwnCost.Text = decOwnCost.ToString();
            tbRealOwnCost.Text = decOwnCost.ToString();

            SetFocus();
        }

        private void SetFocus()
        {
            tbRealOwnCost.Focus();
            tbRealOwnCost.SelectAll();
        }

        /// <summary>
        /// 累计费用
        /// </summary>
        /// <param name="arlFeeInfo"></param>
        /// <returns></returns>
        private bool blnComputeOwnCost(ArrayList arlFeeInfo)
        {
            if (arlFeeInfo == null || arlFeeInfo.Count <= 0)
            {
                return false;
            }

            ArrayList arlFeeList = null;
            ArrayList arlFeeListTemp = null;
            ArrayList arlFeeDetial = null;

            FS.HISFC.Models.Registration.Register register = null;
            FS.HISFC.Models.Fee.Outpatient.Balance balance = null;

            string strTemp = "";

            for(int idx = 0; idx < arlFeeInfo.Count; idx++)
            {
                balance = arlFeeInfo[idx] as FS.HISFC.Models.Fee.Outpatient.Balance;
                if(balance != null)
                {
                    strTemp = balance.Invoice.ID + "|" + balance.CombNO + "|" + balance.TransType.ToString();
                    if (dictFeeInfo.ContainsKey(strTemp))
                    {
                        continue;
                    }
                    else
                    {
                        dictFeeInfo.Add(strTemp, balance);
                        continue;
                    }
                }


                register = arlFeeInfo[idx] as FS.HISFC.Models.Registration.Register;
                if (register == null)
                {
                    continue;
                }

                if (dictFeeInfo.ContainsKey(register.ID))
                {
                    continue;
                }

                arlFeeList = this.outpatientManager.QueryChargedFeeItemListsByClinicNO(register.ID);
                if (arlFeeList == null || arlFeeList.Count <= 0)
                {
                    MessageBox.Show( register.Name + " 无费用信息！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                string strErrText = "";
                arlFeeDetial = new ArrayList();
                foreach (FeeItemList feeItem in arlFeeList)
                {
                    feeItem.FT.TotCost = feeItem.FT.PubCost + feeItem.FT.PayCost + feeItem.FT.OwnCost;
                    if (feeItem.Item.ItemType == EnumItemType.UnDrug)
                    {
                        feeItem.IsGroup = this.IsGroud(feeItem.Item.ID);
                    }
                    if (feeItem.IsGroup)
                    {
                        arlFeeListTemp = this.ConvertGroupToDetail(register, feeItem, out strErrText);
                        if (arlFeeListTemp == null)
                        {
                            MessageBox.Show(register.Name + " -- " + feeItem.Item.Name + " 拆分成明细出错！" + strErrText, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }

                        arlFeeDetial.AddRange(arlFeeListTemp);
                    }
                    else
                    {
                        arlFeeDetial.Add(feeItem);
                    }
                }

                strErrText = "";
                bool blnCompute = this.blnUploadAndPerBalance(ref register, ref arlFeeDetial, out strErrText);
                if (!blnCompute)
                {
                    MessageBox.Show(register.Name + " -- 费用预结算出错！" + strErrText, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                if (dictFeeInfo.ContainsKey(register.ID))
                {
                    dictFeeInfo[register.ID] = register;
                }
                else
                {
                    dictFeeInfo.Add(register.ID, register);
                }

                if (dictFeeDetial.ContainsKey(register.ID))
                {
                    dictFeeDetial[register.ID] = arlFeeDetial;
                }
                else
                {
                    dictFeeDetial.Add(register.ID, arlFeeDetial);
                }

            }

            return true;
        }
        /// <summary>
        /// 上传费用并进行预结算处理
        /// </summary>
        /// <param name="register"></param>
        /// <param name="arlFeeList"></param>
        /// <param name="strErrText"></param>
        /// <returns></returns>
        private bool blnUploadAndPerBalance(ref FS.HISFC.Models.Registration.Register register, ref ArrayList arlFeeList, out string strErrText)
        {
            strErrText = "";
            if (register == null)
            {
                return false;
            }
            if (arlFeeList == null || arlFeeList.Count <= 0)
            {
                return false;
            }

            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.medcareInterfaceProxy.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            //开始待遇事务
            this.medcareInterfaceProxy.BeginTranscation();
            //设置待遇的合同单位参数
            this.medcareInterfaceProxy.SetPactCode(register.Pact.ID);
            this.medcareInterfaceProxy.IsLocalProcess = this.isLocalProcess;
            //连接待遇接口
            long returnValue = this.medcareInterfaceProxy.Connect();
            if (returnValue == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                //医保回滚可能出错，此处提示
                if (this.medcareInterfaceProxy.Rollback() == -1)
                {
                    strErrText = this.medcareInterfaceProxy.ErrMsg;
                    return false;
                }
                strErrText = "医疗待遇接口连接失败!" + this.medcareInterfaceProxy.ErrMsg;

                return false;
            }

            //黑名单判断，南庄用于判断当日报销次数
            if (this.medcareInterfaceProxy.IsInBlackList(register))
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                // 医保回滚可能出错，此处提示
                if (this.medcareInterfaceProxy.Rollback() == -1)
                {
                    strErrText = this.medcareInterfaceProxy.ErrMsg;
                    return false;
                }
                this.medcareInterfaceProxy.Disconnect();

                strErrText = this.medcareInterfaceProxy.ErrMsg;
                return false;
            }

            //调用医保预结算前,清空保存预结算金额字段.
            register.SIMainInfo.OwnCost = 0;
            register.SIMainInfo.PayCost = 0;
            register.SIMainInfo.PubCost = 0;
            register.SIMainInfo.TotCost = 0;

            //删除本次因为错误或者其他原因上传的明细
            returnValue = this.medcareInterfaceProxy.DeleteUploadedFeeDetailsAllOutpatient(register);

            //重新上传所有明细
            returnValue = this.medcareInterfaceProxy.UploadFeeDetailsOutpatient(register, ref arlFeeList);
            if (returnValue == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                //医保回滚可能出错，此处提示
                if (this.medcareInterfaceProxy.Rollback() == -1)
                {
                    strErrText = this.medcareInterfaceProxy.ErrMsg;
                    return false;
                }
                this.medcareInterfaceProxy.Disconnect();

                strErrText = "上传费用明细失败!" + this.medcareInterfaceProxy.ErrMsg;
                return false;
            }

            //待遇接口结算计算,应用公费和医保
            returnValue = this.medcareInterfaceProxy.PreBalanceOutpatient(register, ref arlFeeList);
            if (returnValue == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                //医保回滚可能出错，此处提示
                if (this.medcareInterfaceProxy.Rollback() == -1)
                {
                    strErrText = this.medcareInterfaceProxy.ErrMsg;
                    return false;
                }
                this.medcareInterfaceProxy.Disconnect();
                strErrText = "获得医保预结算信息失败!" + this.medcareInterfaceProxy.ErrMsg;

                return false;
            }
            //断开待遇接口连接 
            this.medcareInterfaceProxy.Rollback();
            this.medcareInterfaceProxy.Disconnect();
            FS.FrameWork.Management.PublicTrans.RollBack();


            decimal ownCost = 0;//自费金额
            decimal pubCost = 0;//社保支付金额
            decimal totCost = 0;//总金额
            decimal payCost = 0;//自付金额
            decimal rebateRate = 0; // 减免金额

            decimal formerTotCost = 0;//对比的总金额

            //获得当前系统时间
            DateTime nowTime = this.undrugManager.GetDateTimeFromSysDateTime();

            //汇总没有进行待遇计算时的费用总金额
            foreach (FeeItemList f in arlFeeList)
            {
                //如果有已经有明细账户支付了,首先考虑只是自费患者,那么将自费调整为0, 账户支付调整为自费金额.
                if (register.Pact.ID == "1" && f.IsAccounted)
                {
                    if (f.FT.OwnCost > 0)
                    {
                        f.FT.PayCost += f.FT.OwnCost;
                        f.FT.OwnCost = 0;
                    }
                }

                f.FeeOper.OperTime = nowTime;

                // 通过待遇算法处理，可能产生减免费用
                formerTotCost += f.FT.OwnCost + f.FT.PubCost + f.FT.PayCost;
            }

            
            foreach (FeeItemList f in arlFeeList)
            {
                // 通过待遇算法处理，可能产生减免费用
                totCost += f.FT.OwnCost + f.FT.PubCost + f.FT.PayCost;
                
                f.NoBackQty = f.Item.Qty;
                rebateRate += f.FT.RebateCost;
            }
            ownCost = totCost - register.SIMainInfo.PubCost - register.SIMainInfo.PayCost;
            payCost += register.SIMainInfo.PayCost;
            pubCost += register.SIMainInfo.PubCost;

            //判断待遇计算前和计算后是否相等
            if (!this.IsCanFeeWhenTotCostDiff && register.Pact.PayKind.ID == "02" && register.SIMainInfo.TotCost != formerTotCost)//参数设置
            {
                // 需要回滚事务
                strErrText = "本院收费系统的总费用与医保系统的总金额不符合,请认真核对！";
                //FS.FrameWork.Management.PublicTrans.RollBack();
                ////医保回滚可能出错，此处提示
                //if (this.medcareInterfaceProxy.Rollback() == -1)
                //{
                //    MessageBox.Show(this.medcareInterfaceProxy.ErrMsg + " " + strMsg);
                //    return false;
                //}
                //this.medcareInterfaceProxy.Disconnect();

                return false;
            }

            //所有金额保留2位小数
            ownCost = FS.FrameWork.Public.String.FormatNumber(ownCost, 2);
            payCost = FS.FrameWork.Public.String.FormatNumber(payCost, 2);
            pubCost = FS.FrameWork.Public.String.FormatNumber(pubCost, 2);
            totCost = FS.FrameWork.Public.String.FormatNumber(totCost, 2);

            #region 计算Lis试管
            ArrayList alLisTube = new ArrayList();
            decimal dCost = 0;
            this.iLisCalculateTube = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject<
                            FS.HISFC.BizProcess.Interface.FeeInterface.ILisCalculateTube>(this.GetType());
            if (this.iLisCalculateTube != null)
            {
                this.iLisCalculateTube.LisCalculateTubeForOutPatient(register, arlFeeList,  (arlFeeList[0] as FeeItemList).RecipeSequence, ref dCost, ref alLisTube);
                if (alLisTube != null && alLisTube.Count > 0)
                {
                    ownCost += dCost;
                    totCost = ownCost + payCost + pubCost;
                    register.SIMainInfo.OwnCost = ownCost;
                    register.SIMainInfo.TotCost = totCost;
                    arlFeeList.AddRange(alLisTube);
                }
            }
            #endregion

            #region 收费金额取整

            iOutPatientFeeRoundOff = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject<
                    FS.HISFC.BizProcess.Interface.FeeInterface.IOutPatientFeeRoundOff>(this.GetType());
            if (iOutPatientFeeRoundOff != null)
            {
                FeeItemList feeItemList = new FeeItemList();

                // 凑整费最小费用，拿费用列表第一条记录最小费用
                string drugFeeCode = "";

                foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList item in arlFeeList)
                {
                    if (string.IsNullOrEmpty(item.Item.MinFee.ID))
                    {
                        continue;
                    }

                    drugFeeCode = item.Item.MinFee.ID;
                    break;
                }
                if (!string.IsNullOrEmpty(drugFeeCode))
                {
                    feeItemList.User03 = drugFeeCode;
                }

                iOutPatientFeeRoundOff.OutPatientFeeRoundOff(register, ref ownCost, ref feeItemList, (arlFeeList[0] as FeeItemList).RecipeSequence);
                if (feeItemList.Item.ID != "")
                {
                    totCost = ownCost + payCost + pubCost;
                    register.SIMainInfo.OwnCost = ownCost;
                    register.SIMainInfo.TotCost = totCost;

                    arlFeeList.Add(feeItemList);
                }
            }

            #endregion

            //// 当减免金额大于 ownCost 时，rebateRate = ownCost
            //if (rebateRate > ownCost)
            //{
            //    rebateRate = ownCost;
            //}

            return true;
        }
        /// <summary>
        /// 把组套拆分成明细
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        private ArrayList ConvertGroupToDetail(FS.HISFC.Models.Registration.Register register, FeeItemList f, out string errText)
        {
            errText = "";
            ArrayList undrugCombList = this.undrugPackAgeManager.QueryUndrugPackagesBypackageCode(f.Item.ID);
            ArrayList alTemp = new ArrayList();
            if (undrugCombList == null)
            {
                errText = "获得组套明细出错!" + undrugPackAgeManager.Err;

                return null;
            }
            decimal price = 0;
            decimal priceSecond = 0;
            decimal count = 0;
            string feeCode = string.Empty;
            string itemType = string.Empty;
            decimal totCost = 0;
            FeeItemList feeDetail = null;
            if (f.Order.ID == null || f.Order.ID == string.Empty)
            {
                f.Order.ID = this.orderIntegrate.GetNewOrderID();
                if (f.Order.ID == null || f.Order.ID == string.Empty)
                {
                    errText = "获得医嘱流水号出错!";

                    return null;
                }
            }
            foreach (FS.HISFC.Models.Fee.Item.UndrugComb undrugCombo in undrugCombList)
            {
                DataRow rowFindZT;
                DataRow[] rowFindZTs = this.DTItem.Select("ITEM_CODE = " + "'" + undrugCombo.ID + "'");
                if (rowFindZTs == null || rowFindZTs.Length == 0)
                {
                    errText = "查找组套明细项目出错! 未找到项目 " + undrugCombo.Name + "【" + undrugCombo.ID + "】";

                    return null;
                }
                rowFindZT = rowFindZTs[0];

                feeDetail = new FeeItemList();

                feeCode = rowFindZT["FEE_CODE"].ToString();
                try
                {
                    DateTime nowTime = this.outpatientManager.GetDateTimeFromSysDateTime();
                    int age = (int)((new TimeSpan(nowTime.Ticks - register.Birthday.Ticks)).TotalDays / 365);

                    // 增加获取购入价
                    string priceForm = register.Pact.PriceForm;
                    decimal unitPrice = NConvert.ToDecimal(rowFindZT["UNIT_PRICE"]);
                    decimal childPrice = NConvert.ToDecimal(rowFindZT["CHILD_PRICE"]);
                    decimal SPPrice = NConvert.ToDecimal(rowFindZT["SP_PRICE"]);
                    decimal purchasePrice = NConvert.ToDecimal(rowFindZT["PURCHASE_PRICE"]);

                    // 保存原始默认价格
                    feeDetail.Item.ChildPrice = unitPrice;

                    price = this.feeIntegrate.GetPrice(priceForm, age, unitPrice, childPrice, SPPrice, purchasePrice);
                }
                catch (Exception e)
                {
                    errText = e.Message;

                    return null;
                }

                //根据优惠比例重新计算单价------------------------- 
                string errMsg = string.Empty;
                PactItemRate myRate = FS.HISFC.Components.OutpatientFee.Class.Function.PactRate(register, feeDetail, ref errMsg);
                if (myRate == null)
                {
                    errText = errMsg;
                    return null;
                }

                price *= 1 - myRate.Rate.RebateRate;
                //--------------------------------------------------
                count = NConvert.ToDecimal(f.Item.Qty) * undrugCombo.Qty;

                //组套拆分成明细的时候，也保存两位小数
                //totCost = price * count;
                totCost = FS.FrameWork.Public.String.FormatNumber(price * count, 2);

                feeDetail.Patient = f.Patient.Clone();
                feeDetail.Item.ID = rowFindZT["ITEM_CODE"].ToString();
                feeDetail.Item.Name = rowFindZT["ITEM_NAME"].ToString();
                feeDetail.Name = feeDetail.Item.Name;
                feeDetail.ID = feeDetail.Item.ID;
                itemType = rowFindZT["DRUG_FLAG"].ToString();
                if (itemType == "0")
                {
                    //feeDetail.Item.IsPharmacy = false;
                    feeDetail.Item.ItemType = EnumItemType.UnDrug;
                    feeDetail.IsGroup = false;
                }
                if (itemType == "1")
                {
                    //feeDetail.Item.IsPharmacy = true;
                    feeDetail.Item.ItemType = EnumItemType.Drug;
                    feeDetail.IsGroup = false;
                }
                if (itemType == "2")
                {
                    //feeDetail.Item.IsPharmacy = false;
                    feeDetail.Item.ItemType = EnumItemType.UnDrug;
                    feeDetail.IsGroup = true;
                }
                feeDetail.RecipeOper = f.RecipeOper.Clone();
                feeDetail.Item.Price = price;
                feeDetail.OrgPrice = price;
                feeDetail.Item.Specs = rowFindZT["SPECS"].ToString();
                feeDetail.Item.SysClass.ID = rowFindZT["SYS_CLASS"].ToString();
                feeDetail.Item.MinFee.ID = feeCode;
                feeDetail.Item.PackQty = NConvert.ToDecimal(rowFindZT["PACK_QTY"].ToString());
                feeDetail.Item.Qty = count;
                feeDetail.Days = NConvert.ToDecimal(f.Days);
                feeDetail.FT.TotCost = totCost;
                //自费如此，如果加上公费需要重新计算!!!
                feeDetail.FT.OwnCost = totCost;
                feeDetail.ExecOper = f.ExecOper.Clone();
                feeDetail.Item.PriceUnit = rowFindZT["MIN_UNIT"].ToString() == string.Empty ? "次" : rowFindZT["MIN_UNIT"].ToString();
                if (rowFindZT["CONFIRM_FLAG"].ToString() == "2" || rowFindZT["CONFIRM_FLAG"].ToString() == "3" || rowFindZT["CONFIRM_FLAG"].ToString() == "1")
                {
                    feeDetail.Item.IsNeedConfirm = true;
                }
                else
                {
                    feeDetail.Item.IsNeedConfirm = false;
                }

                feeDetail.Item.IsNeedBespeak = NConvert.ToBoolean(rowFindZT["NEEDBESPEAK"].ToString());

                feeDetail.Order.ID = f.Order.ID;
                feeDetail.UndrugComb.ID = f.Item.ID;
                feeDetail.UndrugComb.Name = f.Item.Name;
                feeDetail.Order.Combo.ID = f.Order.Combo.ID;
                feeDetail.Item.IsMaterial = f.Item.IsMaterial;
                feeDetail.RecipeSequence = f.RecipeSequence;
                feeDetail.FTSource = f.FTSource;
                feeDetail.FeePack = f.FeePack;
                if (register.Pact.PayKind.ID == "03")
                {
                    FS.HISFC.Models.Base.PactItemRate pactRate = null;

                    if (pactRate == null)
                    {
                        pactRate = this.pactUnitItemRateManager.GetOnepPactUnitItemRateByItem(register.Pact.ID, feeDetail.Item.ID);
                    }
                    if (pactRate != null)
                    {
                        if (pactRate.Rate.PayRate != register.Pact.Rate.PayRate)
                        {
                            if (pactRate.Rate.PayRate == 1)//自费
                            {
                                feeDetail.ItemRateFlag = "1";
                            }
                            else
                            {
                                feeDetail.ItemRateFlag = "3";
                            }
                        }
                        else
                        {
                            feeDetail.ItemRateFlag = "2";

                        }
                        if (f.ItemRateFlag == "3")
                        {
                            feeDetail.OrgItemRate = f.OrgItemRate;
                            feeDetail.NewItemRate = f.NewItemRate;
                            feeDetail.ItemRateFlag = "2";
                        }
                    }
                    else
                    {
                        if (f.ItemRateFlag == "3")
                        {

                            if (rowFindZT["ZF"].ToString() != "1")
                            {
                                feeDetail.OrgItemRate = f.OrgItemRate;
                                feeDetail.NewItemRate = f.NewItemRate;
                                feeDetail.ItemRateFlag = "2";
                            }
                        }
                        else
                        {
                            feeDetail.OrgItemRate = f.OrgItemRate;
                            feeDetail.NewItemRate = f.NewItemRate;
                            feeDetail.ItemRateFlag = f.ItemRateFlag;
                        }
                    }
                }

                //复合项目的用法赋给明细项目
                feeDetail.Order.Usage = f.Order.Usage;

                alTemp.Add(feeDetail);
            }
            if (alTemp.Count > 0)
            {
                if (f.FT.RebateCost > 0)//有减免
                {
                    if (register.Pact.PayKind.ID != "01")
                    {
                        errText = "暂时不允许非自费患者减免!";

                        return null;
                    }
                    //decimal rebateRate =
                    //    FS.FrameWork.Public.String.FormatNumber(
                    //    f.FT.RebateCost / (f.FT.OwnCost + f.FT.RebateCost), 2);
                    //decimal tempFix = 0;
                    //decimal tempRebateCost = 0;
                    //foreach (FeeItemList feeTemp in alTemp)
                    //{
                    //    feeTemp.FT.RebateCost = (feeTemp.FT.OwnCost + feeTemp.FT.RebateCost) * rebateRate;
                    //    tempRebateCost += feeTemp.FT.RebateCost;
                    //    feeTemp.FT.OwnCost = feeTemp.FT.OwnCost - feeTemp.FT.RebateCost;
                    //    feeTemp.FT.TotCost = feeTemp.FT.TotCost - feeTemp.FT.RebateCost;
                    //}
                    //tempFix = f.FT.RebateCost - tempRebateCost;
                    //FeeItemList fFix = alTemp[0] as FeeItemList;
                    //fFix.FT.RebateCost = fFix.FT.RebateCost + tempFix;
                    //fFix.FT.OwnCost = fFix.FT.OwnCost - tempFix;
                    //fFix.FT.TotCost = fFix.FT.TotCost - tempFix;
                    //减免单独算
                    decimal rebateRate =
                        FS.FrameWork.Public.String.FormatNumber(f.FT.RebateCost / f.FT.OwnCost, 2);
                    decimal tempFix = 0;
                    decimal tempRebateCost = 0;
                    foreach (FeeItemList feeTemp in alTemp)
                    {
                        feeTemp.FT.RebateCost = (feeTemp.FT.OwnCost) * rebateRate;
                        tempRebateCost += feeTemp.FT.RebateCost;
                        //feeTemp.FT.OwnCost = feeTemp.FT.OwnCost - feeTemp.FT.RebateCost;
                        //feeTemp.FT.TotCost = feeTemp.FT.TotCost - feeTemp.FT.RebateCost;
                    }
                    tempFix = f.FT.RebateCost - tempRebateCost;
                    FeeItemList fFix = alTemp[0] as FeeItemList;
                    fFix.FT.RebateCost = fFix.FT.RebateCost + tempFix;
                    //fFix.FT.OwnCost = fFix.FT.OwnCost - tempFix;
                    //fFix.FT.TotCost = fFix.FT.TotCost - tempFix;
                }
            }
            if (alTemp.Count > 0)
            {
                if (f.SpecialPrice > 0)//有特殊自费
                {
                    decimal tempPrice = 0m;
                    string id = string.Empty;
                    foreach (FeeItemList feeTemp in alTemp)
                    {
                        if (feeTemp.Item.Price > tempPrice)
                        {
                            id = feeTemp.Item.ID;
                            tempPrice = feeTemp.Item.Price;
                        }
                    }

                    foreach (FeeItemList fee in alTemp)
                    {
                        if (fee.Item.ID == id)
                        {
                            fee.SpecialPrice = f.SpecialPrice;

                            break;
                        }
                    }
                }
            }

            return alTemp;
        }

        private void txtCount_TextChanged(object sender, EventArgs e)
        {
            this.Clear();

            string strTemp = txtCount.Text.Trim();
            if (string.IsNullOrEmpty(strTemp))
            {
                return;
            }

            int iCount = 0;
            int.TryParse(strTemp, out iCount);
            if (iCount <= 0)
            {
                return;
            }

            ArrayList arlFeeInfo = null;
            this.outPatientFeeList.GetSelectRowUpByCount(iCount, out arlFeeInfo);
            if (arlFeeInfo == null || arlFeeInfo.Count <= 0)
            {
                return;
            }

            bool blnRes = this.blnComputeOwnCost(arlFeeInfo);
            if (blnRes)
            {
                decimal ownCost = 0;//自费金额
                decimal pubCost = 0;//社保支付金额
                decimal totCost = 0;//总金额
                decimal rebateRate = 0; // 减免金额
                decimal decTemp = 0;

                FS.HISFC.Models.Registration.Register regTemp = null;
                FS.HISFC.Models.Fee.Outpatient.Balance balance = null;
                ArrayList arlFeeDetial = null;
                //外屏显示的姓名
                ArrayList allFeeList = new ArrayList();
                for (int idx = 0; idx < arlFeeInfo.Count; idx++)
                {
                    if (arlFeeInfo[idx] is FS.HISFC.Models.Fee.Outpatient.Balance)
                    {
                        balance = arlFeeInfo[idx] as FS.HISFC.Models.Fee.Outpatient.Balance;
                        if (balance == null)
                        {
                            continue;
                        }
                        string strInvoiceKey = balance.Invoice.ID + "|" + balance.CombNO + "|" + balance.TransType.ToString();
                        if (!dictFeeInfo.ContainsKey(strInvoiceKey))
                        {
                            continue;
                        }

                        balance = dictFeeInfo[strInvoiceKey] as FS.HISFC.Models.Fee.Outpatient.Balance;
                        if (balance == null)
                        {
                            continue;
                        }

                        totCost += balance.FT.TotCost;
                        pubCost += balance.FT.PubCost;

                        decTemp = 0;
                        decimal.TryParse(balance.User01, out decTemp);
                        if (balance.TransType == TransTypes.Negative)
                        {
                            rebateRate += -1 * decTemp;
                        }
                        else
                        {
                            rebateRate += decTemp;
                        }
                    }
                    else
                    {
                        regTemp = arlFeeInfo[idx] as FS.HISFC.Models.Registration.Register;

                        if (!dictFeeInfo.ContainsKey(regTemp.ID) || !dictFeeDetial.ContainsKey(regTemp.ID))
                        {
                            return;
                        }

                        arlFeeDetial = dictFeeDetial[regTemp.ID];
                        regTemp = dictFeeInfo[regTemp.ID] as FS.HISFC.Models.Registration.Register;
                        

                        foreach (FeeItemList f in arlFeeDetial)
                        {
                            // 通过待遇算法处理，可能产生减免费用
                            totCost += f.FT.OwnCost + f.FT.PubCost + f.FT.PayCost;
                            rebateRate += f.FT.RebateCost;
                            //用于外屏显示费用信息
                            allFeeList.Add(f);
                        }
                        pubCost += regTemp.SIMainInfo.PubCost;

                    }
                }

                //foreach (FS.HISFC.Models.Registration.Register register in arlFeeInfo)
                //{
                //    if (!dictFeeInfo.ContainsKey(register.ID) || !dictFeeDetial.ContainsKey(register.ID))
                //    {
                //        return;
                //    }

                //    regTemp = dictFeeInfo[register.ID];
                //    arlFeeDetial = dictFeeDetial[register.ID];

                //    foreach (FeeItemList f in arlFeeDetial)
                //    {
                //        // 通过待遇算法处理，可能产生减免费用
                //        totCost += f.FT.OwnCost + f.FT.PubCost + f.FT.PayCost;
                //        rebateRate += f.FT.RebateCost;
                //    }

                //    pubCost += regTemp.SIMainInfo.PubCost;
                //}

                pubCost += rebateRate;
                ownCost = totCost - pubCost;

                this.SetView(pubCost, ownCost);


                //外屏显示
                if (iMultiScreen == null)
                {
                    iMultiScreen = new Forms.frmMiltScreen();

                }
                if (regTemp != null)
                {
                    FS.HISFC.Models.Registration.Register addRegTemp = null;
                    addRegTemp = regTemp.Clone();
                    addRegTemp.Name = "合计费用";
                    this.SetInfomation(addRegTemp, null, allFeeList, null, "3");
                }
            }


        }
        /// <summary>
        /// 外屏显示累计的费用
        /// </summary>
        private  void SetInfomation(FS.HISFC.Models.Registration.Register patient, FS.HISFC.Models.Base.FT ft, ArrayList feeItemLists, ArrayList diagLists,
            params string[] otherInfomations)
        {
            System.Collections.Generic.List<Object> lo = new System.Collections.Generic.List<object>();
            lo.Add(patient);
            lo.Add(ft);
            lo.Add(feeItemLists);
            lo.Add(diagLists);
            lo.Add(otherInfomations);
            string strTemp = otherInfomations[0];
            if (this.iMultiScreen != null && (strTemp == "1" || strTemp == "2" || strTemp == "3" || strTemp == "4"))
            {
                this.iMultiScreen.ListInfo = lo;
            }
           
        }
        /// <summary>
        /// 获得处方备注
        /// </summary>
        private bool  IsGroud(string ItemId)
        {
            string flag = null ;
            bool isGroud =false;
            string sql = @"select t.unitflag from fin_com_undruginfo t
                           where t.item_code ='{0}'
                           and t.valid_state ='1'
                          ";
            try
            {
                sql = string.Format(sql, ItemId);
            }
            catch (Exception ex)
            {
                return false;
            }
            if (this.outpatientManager.ExecSqlReturnOne(sql) != "-1")
            {
                flag = this.outpatientManager.ExecSqlReturnOne(sql);
            }
            if (flag == "1")
            {
                isGroud = true;
            }
            else
            {
                isGroud = false;
            }
            return isGroud;

        }
        private void tbRealOwnCost_TextChanged(object sender, EventArgs e)
        {
            tbReturnCost.Text = "0.0";
            string strTemp = tbRealOwnCost.Text.Trim();
            decimal decRealOwnCost = 0;
            decimal.TryParse(strTemp, out decRealOwnCost);

            if (decRealOwnCost <= 0)
            {
                tbReturnCost.Text = "0.0";
            }

            decimal decOwnCost = 0;
            decimal.TryParse(tbOwnCost.Text.Trim(), out decOwnCost);

            decimal decReturnCost = decRealOwnCost - decOwnCost;
            if (decReturnCost > 0)
            {
                tbReturnCost.Text = decReturnCost.ToString();
            }

        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
    }
}
