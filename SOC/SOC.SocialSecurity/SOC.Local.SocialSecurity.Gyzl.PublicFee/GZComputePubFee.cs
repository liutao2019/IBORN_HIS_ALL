using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using FS.HISFC.Models.Registration;
using FS.HISFC.Models.Base;
using System.Data;
using FS.HISFC.Models.Fee.Outpatient;
using System.Windows.Forms;

namespace FS.SOC.Local.SocialSecurity.Gyzl.PublicFee
{

    /// <summary>
    /// 广州医学院附属肿瘤医院公费算法
    /// </summary>
    class GZComputePubFee : FS.HISFC.BizProcess.Interface.FeeInterface.IMedcare, FS.HISFC.BizProcess.Interface.FeeInterface.IMedcareExtend
    {
        #region 属性
        /// <summary>
        /// 错误编码
        /// </summary>
        private string errCode = "";
        /// <summary>
        /// 错误提示
        /// </summary>
        private string errMsg = "";
        /// <summary>
        /// 数据库事务
        /// </summary>
        private System.Data.IDbTransaction trans;
        /// <summary>
        /// 特殊
        /// </summary>
        private ArrayList alSpecialPact = null;
        /// <summary>
        /// 监护床
        /// </summary>
        private static ArrayList alIcuBedList = null;
        private FS.HISFC.Models.Registration.Register register = new FS.HISFC.Models.Registration.Register();
        private static FS.FrameWork.Public.ObjectHelper helper = new FS.FrameWork.Public.ObjectHelper();
        private FS.HISFC.BizLogic.Manager.Constant conManager = new FS.HISFC.BizLogic.Manager.Constant();

        /// <summary>
        /// 缓存基本项目扩展信息
        /// </summary>
        private Hashtable hsComItemExtendInfo = new Hashtable();

        #endregion

        #region 变量
        #region 公费参数设置

        ArrayList relations = new ArrayList();//限额关系

        FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
        FS.HISFC.BizLogic.Fee.Account accountManager = new FS.HISFC.BizLogic.Fee.Account();
        FS.HISFC.BizProcess.Integrate.Manager interManager = new FS.HISFC.BizProcess.Integrate.Manager();
        #endregion
        #endregion

        #region 接口成员实现

        public int BalanceInpatient(FS.HISFC.Models.RADT.PatientInfo patient, ref System.Collections.ArrayList feeDetails)
        {
            //return this.InsertGFHZ(patient, ref feeDetails);
            return 1;
        }

        public int BalanceOutpatient(FS.HISFC.Models.Registration.Register r, ref System.Collections.ArrayList feeDetails)
        {
            return 1;
        }

        public int CancelBalanceInpatient(FS.HISFC.Models.RADT.PatientInfo patient, ref System.Collections.ArrayList feeDetails)
        {
            return 1;
        }

        public int CancelBalanceOutpatient(FS.HISFC.Models.Registration.Register r, ref System.Collections.ArrayList feeDetails)
        {
            return 1;
        }

        public int CancelBalanceOutpatientHalf(FS.HISFC.Models.Registration.Register r, ref System.Collections.ArrayList feeDetails)
        {
            return 1;
        }

        public int CancelRegInfoInpatient(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            return 1;
        }

        public int CancelRegInfoOutpatient(FS.HISFC.Models.Registration.Register r)
        {
            return 1;
        }

        public int DeleteUploadedFeeDetailInpatient(FS.HISFC.Models.RADT.PatientInfo patient, FS.HISFC.Models.Fee.Inpatient.FeeItemList f)
        {
            return 1;
        }

        public int DeleteUploadedFeeDetailOutpatient(FS.HISFC.Models.Registration.Register r, FS.HISFC.Models.Fee.Outpatient.FeeItemList f)
        {
            return 1;
        }

        public int DeleteUploadedFeeDetailsAllInpatient(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            return 1;
        }

        public int DeleteUploadedFeeDetailsAllOutpatient(FS.HISFC.Models.Registration.Register r)
        {
            return 1;
        }

        public int DeleteUploadedFeeDetailsInpatient(FS.HISFC.Models.RADT.PatientInfo patient, ref System.Collections.ArrayList feeDetails)
        {
            return 1;
        }

        public int DeleteUploadedFeeDetailsOutpatient(FS.HISFC.Models.Registration.Register r, ref System.Collections.ArrayList feeDetails)
        {
            return 1;
        }

        public string Description
        {
            get { return "广州医学院附属肿瘤医院公费算法"; }
        }

        public string ErrCode
        {
            get
            {
                return this.errCode;
            }
        }

        public string ErrMsg
        {
            get
            {
                return this.errMsg;
            }
        }

        public int GetRegInfoInpatient(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            /**
             * 获取公费患者日限额，总限额，床位限额，超标金额
             * */
            //取该项目的合同单位的比率
            FS.HISFC.BizLogic.Fee.PactUnitInfo PactManagment = new FS.HISFC.BizLogic.Fee.PactUnitInfo();
            if (this.trans != null)
            {
                PactManagment.SetTrans(this.trans);
            }
            //获取项目的限额
            FS.HISFC.Models.Base.PactInfo ObjPactInfo = PactManagment.GetPactUnitInfoByPactCode(patient.Pact.ID);
            if (ObjPactInfo == null)
            {
                this.errMsg = "获得合同单位信息出错，" + PactManagment.Err;
                return -1;
            }
            patient.FT.DayLimitCost = ObjPactInfo.DayQuota;//日限额
            patient.FT.BedLimitCost = ObjPactInfo.BedQuota;//床位限额
            patient.FT.AirLimitCost = ObjPactInfo.AirConditionQuota;//监护床

            DateTime dtNow = PactManagment.GetDateTimeFromSysDateTime();
            //根据患者的入院日期判断患者的总限额
            if (patient.PVisit.InTime == null || patient.PVisit.InTime == DateTime.MinValue
                || patient.PVisit.InTime.Date >= dtNow.Date)
            {
                //患者没有入院日期或者当天入院的：
                patient.FT.DayLimitTotCost = ObjPactInfo.DayQuota;
            }
            else if (patient.PVisit.InTime.Date < dtNow.Date)
            {
                TimeSpan tms = dtNow.Date - patient.PVisit.InTime.Date;
                patient.FT.DayLimitTotCost = ObjPactInfo.DayQuota * (tms.Days + 1);
            }
            patient.FT.OvertopCost = -patient.FT.DayLimitTotCost;
            return 1;
        }

        public int GetRegInfoOutpatient(FS.HISFC.Models.Registration.Register r)
        {
            return 1;
        }

        public bool IsInBlackList(FS.HISFC.Models.Registration.Register r)
        {
            //判断合同单位
            //根据病历号查找对应的员工卡号
            if (r.Pact.ID.Equals("M55") || r.Pact.ID.Equals("M56") || r.Pact.ID.Equals("Z55") || r.Pact.ID.Equals("Z56"))
            {

                string markNO = Function.GetEmplCode(r.PID.CardNO);
                string emplCode = string.Empty;
                if (markNO.StartsWith("*"))
                {
                    emplCode = markNO.Remove(0, 1);
                }
                else
                {
                    emplCode = r.SSN;
                }

                //如果卡类型是0，说明是本院职工，判断是否能够享受本院职工

                FS.HISFC.Models.Base.Employee empl = this.interManager.GetEmployeeInfo(emplCode.PadLeft(6, 'A'));
                if (empl == null || string.IsNullOrEmpty(empl.ID))
                {
                    //再根据0找一遍
                    empl = this.interManager.GetEmployeeInfo(emplCode.PadLeft(6, '0'));
                }

                if (empl != null && string.IsNullOrEmpty(empl.ID) == false)
                {
                    if ("1".Equals(empl.User01))
                    {
                        return false;
                    }
                    else if ("2".Equals(empl.User01))
                    {
                        this.errMsg = "您的待遇是本院职工（医保），不能享受本院职工（公费）待遇";
                        return true;
                    }
                    else if ("3".Equals(empl.User01))
                    {
                        this.errMsg = "您的待遇是本院职工（离退休），不能享受本院职工（公费）待遇";
                        return true;
                    }
                    else if ("4".Equals(empl.User01))
                    {
                        this.errMsg = "您的待遇是本院职工（招聘），不能享受本院职工（公费）待遇";
                        return true;
                    }
                    else
                    {
                        this.errMsg = "您的待遇是其他类型，不能享受本院职工（公费）待遇";
                        return true;
                    }
                }
                else
                {
                    this.errMsg = "您不是本院职工，不能享受本院职工待遇";
                    return true;
                }
            }

            return false;
        }

        public bool IsInBlackList(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            return false;
        }

        public bool IsUploadAllFeeDetailsOutpatient
        {
            get { return true; }
        }

        public int LogoutInpatient(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            return 1;
        }

        public int MidBalanceInpatient(FS.HISFC.Models.RADT.PatientInfo patient, ref System.Collections.ArrayList feeDetails)
        {
            //return this.InsertGFHZ(patient, ref feeDetails); 
            return 1;
        }

        public int ModifyUploadedFeeDetailInpatient(FS.HISFC.Models.RADT.PatientInfo patient, FS.HISFC.Models.Fee.Inpatient.FeeItemList f)
        {
            return 1;
        }

        public int ModifyUploadedFeeDetailOutpatient(FS.HISFC.Models.Registration.Register r, FS.HISFC.Models.Fee.Outpatient.FeeItemList f)
        {
            return 1;
        }

        public int ModifyUploadedFeeDetailsInpatient(FS.HISFC.Models.RADT.PatientInfo patient, ref System.Collections.ArrayList feeDetails)
        {
            return 1;
        }

        public int ModifyUploadedFeeDetailsOutpatient(FS.HISFC.Models.Registration.Register r, ref System.Collections.ArrayList feeDetails)
        {
            return 1;
        }

        public int PreBalanceInpatient(FS.HISFC.Models.RADT.PatientInfo patient, ref System.Collections.ArrayList feeDetails)
        {
            return 1;
        }

        public int QueryBlackLists(ref System.Collections.ArrayList blackLists)
        {
            return 1;
        }

        public int QueryCanMedicare(FS.HISFC.Models.Registration.Register r)
        {
            return 1;
        }

        public int QueryDrugLists(ref System.Collections.ArrayList drugLists)
        {
            return 1;
        }

        public int QueryFeeDetailsInpatient(FS.HISFC.Models.RADT.PatientInfo patient, ref System.Collections.ArrayList feeDetails)
        {
            return 1;
        }

        public int QueryFeeDetailsOutpatient(FS.HISFC.Models.Registration.Register r, ref System.Collections.ArrayList feeDetails)
        {
            return 1;
        }

        public int QueryUndrugLists(ref System.Collections.ArrayList undrugLists)
        {
            return 1;
        }

        public int RecallRegInfoInpatient(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            return 1;
        }

        public int RecomputeFeeItemListInpatient(FS.HISFC.Models.RADT.PatientInfo patient, FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList)
        {
            if (patient.Pact.PayKind.ID != "03")
            {
                this.errMsg = "非公费患者不允许调用此接口！";
                return -1;
            }
            //最小费用为空 返回
            if (feeItemList.Item.MinFee.ID == null || feeItemList.Item.MinFee.ID.Trim() == "")
            {
                this.errMsg = "最小费用为空,收取没有意义!";
                return -1;
            }


            if (feeItemList.FT.TotCost == 0)
            {
                this.errMsg = "费用总额为0,收取没有意义!";
                return -1;
            }
            //如果是退费。不重新计算比例
            //if (feeItemList.TransType == FS.HISFC.Object.Base.TransTypes.Negative)
            //{
            //    return 1;
            //}

            //if (feeItemList.FT.TotCost < 0)
            //{
            //    return 1;
            //}

            //如果是修改比例则不重新计算比例 返回
            //修改比例
            if (feeItemList.User01 == "ALTERRATE")
            {
                return 1;
            }

            //调用求比例函数，只有Tot_cost有用，其它Cost无效
            //但是在有些收费中给own_cost赋值，在此取消
            feeItemList.FT.OwnCost = 0;
            feeItemList.FT.PayCost = 0;
            feeItemList.FT.PubCost = 0;
            FS.HISFC.Models.Base.FTRate RateLimit = null;

            #region 省限、市限等特殊项目的处理

            if (this.SpecialItemPactLimit(patient.Pact.ID, feeItemList) == -1)
            {
                errMsg = "获取省限、市限，自费项目信息出错！";
                return -1;
            }
            if (feeItemList.Item.SpecialFlag == "1" && feeItemList.FTRate.OwnRate == 1)
            {
                RateLimit = new FS.HISFC.Models.Base.FTRate();
                RateLimit.OwnRate = 1;
            }

            #endregion

            //求费用比例
            FS.HISFC.Models.Base.FTRate Rate = this.ComputeFeeRate(patient.Pact.ID, feeItemList.Item);
            if (Rate == null)
            {
                return -1;
            }
            else
            {
                if (Rate.OwnRate == 1)
                {
                    feeItemList.Item.SpecialFlag = "1";//公费患者使用自费项目标记对应药品或非药品费用明细表的EXT_FLAG
                }
                if (Rate.OwnRate == 0 && RateLimit != null)
                {
                    Rate.OwnRate = RateLimit.OwnRate;
                }
            }

            //----------------------------身份变更时床位超标处理--------------
            #region 床位超标处理

            bool computeLimit = true;//项目是否计算入限额
            //床位限额
            decimal BedLimit = FS.FrameWork.Public.String.FormatNumber(patient.FT.BedLimitCost, 2);
            //监护床位限额
            decimal IcuLimit = FS.FrameWork.Public.String.FormatNumber(patient.FT.AirLimitCost, 2);
            FS.FrameWork.Models.NeuObject conBedMinFee = conManager.GetConstant("BEDLIMITMINFEE", "1");
            string bedMinFeeCode = "";
            if (conBedMinFee != null)
            {
                if (string.IsNullOrEmpty(conBedMinFee.Name))
                {
                    this.errMsg = "请在常数维护【床位限额最小费用代码】中维护普通床限额的最小费用代码！";
                    return -1;
                }
                bedMinFeeCode = conBedMinFee.Name;//普通床最小费用代码
            }

            FS.FrameWork.Models.NeuObject conICUBedMinFee = conManager.GetConstant("BEDLIMITMINFEE", "2");
            string icuBedMinFeeCode = "";
            if (conICUBedMinFee != null)
            {
                if (string.IsNullOrEmpty(conICUBedMinFee.Name))
                {
                    this.errMsg = "请在常数维护【床位限额最小费用代码】中维护监护床限额的最小费用代码！";
                    return -1;
                }
                icuBedMinFeeCode = conICUBedMinFee.Name;//监护床最小费用代码
            }

            if (feeItemList.Item.MinFee.ID == bedMinFeeCode && computeLimit)
            {
                if (Rate.OwnRate == 1)
                {
                    feeItemList.FT.OwnCost = feeItemList.FT.TotCost;
                }
                else
                {
                    #region 普通床超标处理
                    if (patient.FT.BedOverDeal == "1")
                    {//超标自理
                        //不超标
                        if (feeItemList.FT.TotCost <= BedLimit)
                        {
                            BedLimit = BedLimit - feeItemList.FT.TotCost;
                        }
                        else
                        {//超标部分转为自费
                            feeItemList.FT.OwnCost = feeItemList.FT.TotCost - BedLimit;
                            BedLimit = 0;
                        }
                    }
                    else if (patient.FT.BedOverDeal == "2")
                    {
                        ////超标不计，报销限额内，剩下的舍掉
                        if (feeItemList.FT.TotCost > BedLimit)
                        {
                            feeItemList.FT.TotCost = BedLimit;
                            BedLimit = 0;
                            //if (feeItemList.FT.TotCost == 0)
                            //{
                            //    break;
                            //}
                        }
                        else
                        {
                            BedLimit = BedLimit - feeItemList.FT.TotCost;
                        }
                    }
                    #endregion
                }
            }
            else if (feeItemList.Item.MinFee.ID == icuBedMinFeeCode && computeLimit)
            {

                if (Rate.OwnRate == 1)
                {
                    feeItemList.FT.OwnCost = feeItemList.FT.TotCost;
                }
                else
                {
                    #region 监护床超标处理

                    //超标自理
                    if (patient.FT.BedOverDeal == "1")
                    {
                        if (IcuLimit >= feeItemList.FT.TotCost)
                        {
                            //监护床标准大于监护床费，不超标								
                            IcuLimit = IcuLimit - feeItemList.FT.TotCost;
                        }
                        else
                        {
                            //超标，超标部分自费
                            feeItemList.FT.OwnCost = feeItemList.FT.TotCost - IcuLimit;
                            IcuLimit = 0;
                        }
                    }
                    else if (patient.FT.BedOverDeal == "2")
                    {//超标不计，报销限额内，剩下的舍掉
                        //超标
                        if (feeItemList.FT.TotCost > IcuLimit)
                        {
                            feeItemList.FT.TotCost = IcuLimit;
                            IcuLimit = 0;
                            //if (feeItemList.FT.TotCost == 0)
                            //{
                            //    break;
                            //}
                        }
                        else
                        {
                            IcuLimit = IcuLimit - feeItemList.FT.TotCost;
                        }
                    }
                    #endregion
                }
            }
            #endregion
            //----------------------------------------------------------------

            //计算费用金额
            this.ComputeCost(feeItemList, Rate);
            return 1;
        }


        private int SpecialItemPactLimit(string pactCode, FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItem)
        {
            /*获取省市公费类别 省公费，省离休，市公费，市离休，特约单位等
             *特约单位的要根据医疗证号来取
             */
            string pactType = "";

            pactType = this.GetPactLimitType(pactCode);


            if (pactType == null)
            {
                this.errMsg = "获得患者的结算类别出错！";
                return -1;
            }

            //根据合同单位所属，设置需要自费的项目
            switch (pactType)
            {
                case "省公费":
                    {
                        this.SetOwnPayItem(true, false, true, feeItem);
                        break;
                    }
                case "省离休":
                    {
                        /*省离休药品不受限，非药品受限*/
                        //ArrayList alTemp = null;

                        //药品
                        //alTemp = this.FilterFeeItem(feeItemList, true);
                        if (feeItem.Item.ItemType == EnumItemType.Drug)
                        {
                            this.SetOwnPayItem(false, false, true, feeItem);
                        }
                        else
                        {
                            //非药品

                            this.SetOwnPayItem(true, false, true, feeItem);
                        }

                        break;
                    }
                case "市公费":
                    {
                        this.SetOwnPayItem(false, true, true, feeItem);
                        break;
                    }
                case "市离休":
                    {
                        this.SetOwnPayItem(false, true, true, feeItem);
                        break;
                    }
                case "特约单位":
                    {
                        this.SetOwnPayItem(false, false, true, feeItem);
                        break;
                    }
                case "无限制":
                    {
                        this.SetOwnPayItem(false, false, true, feeItem);
                        break;
                    }
                default:
                    {
                        this.SetOwnPayItem(false, false, true, feeItem);
                        break;
                    }

            }
            return 1;
        }

        /// <summary>
        /// 根据参数设置项目为自费
        /// </summary>
        /// <param name="bSHX">省限标志</param>
        /// <param name="bSX">市限标志</param>
        /// /// <param name="bZF">自费标志</param>
        /// <param name="alFee">收费项目</param>
        private void SetOwnPayItem(bool bSHX, bool bSX, bool bZF, FS.HISFC.Models.Fee.Inpatient.FeeItemList feeitem)
        {
            FS.SOC.HISFC.Fee.BizLogic.ComItemExtendInfo comItemExtendMgr = new FS.SOC.HISFC.Fee.BizLogic.ComItemExtendInfo();

            if (this.trans != null)
            {
                comItemExtendMgr.SetTrans(this.trans);
            }


            FS.SOC.HISFC.Fee.Models.ComItemExtend itemExtend = null;
            if (hsComItemExtendInfo.Count > 0 && hsComItemExtendInfo.Contains(feeitem.Item.ID))
            {
                itemExtend = (FS.SOC.HISFC.Fee.Models.ComItemExtend)hsComItemExtendInfo[feeitem.Item.ID];
            }
            else
            {
                List<FS.SOC.HISFC.Fee.Models.ComItemExtend> lst = comItemExtendMgr.QueryItemListByItemCode(feeitem.Item.ID);

                if (lst == null || lst.Count == 0)
                {
                    return;
                }
                else
                {
                    itemExtend = lst[0] as FS.SOC.HISFC.Fee.Models.ComItemExtend;
                    hsComItemExtendInfo.Add(feeitem.Item.ID, itemExtend);
                }
            }

            if (itemExtend.ProvinceFlag == "1")//省限项目
            {
                if (bSHX)
                {
                    feeitem.Item.SpecialFlag = "1";//公费患者使用自费项目
                    feeitem.FTRate.OwnRate = 1;
                }
            }
            if (itemExtend.CityFlag == "1")//市限项目
            {
                if (bSX)
                {
                    feeitem.Item.SpecialFlag = "1";//公费患者使用自费项目
                    feeitem.FTRate.OwnRate = 1;
                }
            }
            if (itemExtend.ZFFlag == "1")//自费项目
            {
                if (bZF)
                {
                    feeitem.Item.SpecialFlag = "1";//公费患者使用自费项目
                    feeitem.FTRate.OwnRate = 1;
                }
            }
        }

        /// <summary>
        /// 求患者费用比例和限额  根据合同单位获取费用比例信息和日限额 
        /// </summary>
        /// <param name="PactID">合同单位代码</param>
        /// <param name="item">药品费药品信息</param>
        /// <returns>失败null；成功 FS.HISFC.Object.Base.FtRate</returns>
        public FS.HISFC.Models.Base.FTRate ComputeFeeRate(string PactID, FS.HISFC.Models.Base.Item item)
        {
            FS.HISFC.BizLogic.Fee.PactUnitItemRate PactItemRate = new FS.HISFC.BizLogic.Fee.PactUnitItemRate();

            if (this.trans != null)
            {
                PactItemRate.SetTrans(this.trans);
            }
            //
            FS.HISFC.Models.Base.PactItemRate ObjPactItemRate = null;
            try
            {
                //项目
                ObjPactItemRate = PactItemRate.GetOnepPactUnitItemRateByItem(PactID, item.ID);
                if (ObjPactItemRate == null)
                {
                    //最小费用
                    ObjPactItemRate = PactItemRate.GetOnepPactUnitItemRateByItem(PactID, item.MinFee.ID);
                    if (ObjPactItemRate == null)
                    {
                        //取合同单位的比例
                        FS.HISFC.BizLogic.Fee.PactUnitInfo PactManagment = new FS.HISFC.BizLogic.Fee.PactUnitInfo();
                        if (this.trans != null)
                        {
                            PactManagment.SetTrans(this.trans);
                        }

                        FS.HISFC.Models.Base.PactInfo PactUnitInfo = PactManagment.GetPactUnitInfoByPactCode(PactID);

                        if (PactUnitInfo == null)
                        {
                            this.errMsg = "获得合同单位信息出错，" + PactManagment.Err;
                            return null;
                        }
                        try
                        {
                            ObjPactItemRate = new FS.HISFC.Models.Base.PactItemRate();
                            ObjPactItemRate.Rate.PayRate = PactUnitInfo.Rate.PayRate;
                            ObjPactItemRate.Rate.OwnRate = PactUnitInfo.Rate.OwnRate;
                        }
                        catch
                        {
                            this.errMsg = "获得合同单位信息出错，" + PactManagment.Err;
                            return null;
                        }
                    }
                }
            }
            catch
            {
                this.errMsg = "获得合同单位信息出错";
                return null;
            }

            return ObjPactItemRate.Rate;
        }

        /// <summary>
        ///  计算总费用的各个组成部分的值
        /// </summary>
        /// <param name="ItemList"></param>
        /// <param name="rate">各部分之间的比例</param>
        /// <returns>-1失败，0成功</returns>
        private int ComputeCost(FS.HISFC.Models.Fee.Inpatient.FeeItemList ItemList, FS.HISFC.Models.Base.FTRate rate)
        {
            if (ItemList.FT.OwnCost == 0)
            {
                ItemList.FT.OwnCost = FS.FrameWork.Public.String.FormatNumber(ItemList.FT.TotCost * rate.OwnRate, 2);
                ItemList.FT.PayCost = FS.FrameWork.Public.String.FormatNumber((ItemList.FT.TotCost - ItemList.FT.OwnCost) * rate.PayRate, 2);
                ItemList.FT.PubCost = ItemList.FT.TotCost - ItemList.FT.OwnCost - ItemList.FT.PayCost;
            }
            else
            {
                ItemList.FT.PayCost = FS.FrameWork.Public.String.FormatNumber((ItemList.FT.TotCost - ItemList.FT.OwnCost) * rate.PayRate, 2);
                ItemList.FT.PubCost = ItemList.FT.TotCost - ItemList.FT.OwnCost - ItemList.FT.PayCost;
            }
            return 0;

        }


        public void SetTrans(System.Data.IDbTransaction t)
        {
            this.trans = t;
        }

        public System.Data.IDbTransaction Trans
        {
            set
            {
                this.trans = value;
            }
        }

        public int UpdateFeeItemListInpatient(FS.HISFC.Models.RADT.PatientInfo patient, FS.HISFC.Models.Fee.Inpatient.FeeItemList f)
        {
            /* 如果不是公费患者 返回
            * 如果不是药品返回
            * pub_cost+pay_cost=0 返回
            * 特殊标志 标志不扣限额 返回 
            * 以上： 扣限额
            *update fin_ipr_inmaininfo f
            *  set f.limit_overtop = f.limit_overtop + pub+pay,
            *     f.bursary_totmedfee = f.bursary_totmedfee+ pub+pay
            * where inpatient_no=''
            */

            //非公费患者无需更新限额
            if (patient.Pact.PayKind.ID != "03")
            {
                this.errMsg = "非公费患者不允许调用此接口！";
                return -1;
            }
            //非药品无需更新限额
            if (f.Item.MinFee.ID != "001" && f.Item.MinFee.ID != "002" && f.Item.MinFee.ID != "003")
            {
                return 1;
            }
            //记账金额为 0 无需更新限额
            if (f.FT.PubCost + f.FT.PayCost == 0)
            {
                return 1;
            }
            //如果标志为不扣限额
            if (f.User02 == "NODEDUCTLIMIT")
            {
                return 1;
            }
            //更新住院主表超标金额及公费药金额
            FS.HISFC.BizLogic.Fee.InPatient feeMgr = new FS.HISFC.BizLogic.Fee.InPatient();
            if (this.trans != null)
            {
                feeMgr.SetTrans(this.trans);
            }
            if (feeMgr.UpdateBursaryTotMedFee(patient.ID, f.FT.PayCost + f.FT.PubCost) == -1)
            {
                this.errMsg = "更新药品超标金额出错" + feeMgr.Err;
                return -1;
            }

            return 1;
        }

        public int UploadFeeDetailInpatient(FS.HISFC.Models.RADT.PatientInfo patient, FS.HISFC.Models.Fee.Inpatient.FeeItemList f)
        {
            return 1;
        }

        public int UploadFeeDetailOutpatient(FS.HISFC.Models.Registration.Register r, FS.HISFC.Models.Fee.Outpatient.FeeItemList f)
        {
            return 1;
        }

        public int UploadFeeDetailsInpatient(FS.HISFC.Models.RADT.PatientInfo patient, ref System.Collections.ArrayList feeDetails)
        {
            return 1;
        }

        public int UploadFeeDetailsOutpatient(FS.HISFC.Models.Registration.Register r, ref System.Collections.ArrayList feeDetails)
        {
            return 1;
        }

        public int UploadRegInfoInpatient(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            return 1;
        }

        public int UploadRegInfoOutpatient(FS.HISFC.Models.Registration.Register r)
        {
            return 1;
        }

        #endregion

        #region 接口事务实现

        public void BeginTranscation()
        {

        }

        public long Commit()
        {
            return 1;
        }

        public long Connect()
        {
            return 1;
        }

        public long Disconnect()
        {
            return 1;
        }

        public long Rollback()
        {
            return 1;
        }

        #endregion

        public int Clear()
        {
            helper = new FS.FrameWork.Public.ObjectHelper();
            return 1;
        }

        /// <summary>
        /// 门诊公费核心算法
        /// </summary>
        /// <param name="r">挂号信息</param>
        /// <param name="feeDetails">费用信息</param>
        /// <returns>1正常返回</returns>
        public int PreBalanceOutpatient(Register r, ref ArrayList feeDetails)
        {
            this.register = r;
            return GetCommomPactItemPubCost(r, ref feeDetails);
        }

        /// <summary>
        /// 特殊合同单位算法。
        /// </summary>
        /// <param name="r"></param>
        /// <param name="feeDetails"></param>
        /// <returns></returns>
        public int GetSpecialPactItemPubCost(Register r, ref ArrayList feeDetails)
        {
            this.Clear();
            if (r == null || r.ID == "")
            {
                errMsg = "患者基本信息为空！";
                return -1;
            }
            if (feeDetails == null)
            {
                errMsg = "费用明细集合为空！";
                return -1;
            }

            if (r.Pact == null || r.Pact.ID == "")
            {
                errMsg = "患者合同单位为空！";
                return -1;
            }
            foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList f in feeDetails)
            {
                if (f == null)
                {
                    continue;
                }
                //bigai f.FT.User03;
                f.FT.OwnCost = FS.FrameWork.Public.String.FormatNumber(FS.FrameWork.Function.NConvert.ToDecimal(f.FT.User03), 2);
                f.FT.PayCost = FS.FrameWork.Public.String.FormatNumber((f.FT.TotCost - f.FT.OwnCost) * f.NewItemRate, 2);
                f.FT.PubCost = f.FT.TotCost - f.FT.OwnCost - f.FT.PayCost;
                f.FT.ExcessCost = 0;
                f.FT.DrugOwnCost = 0;
            }
            //将费用信息汇总后存放在挂号信息里
            ComputeFee(r, feeDetails);
            return 1;
        }
        /// <summary>
        /// 普通合同单位算法
        /// </summary>
        /// <param name="r"></param>
        /// <param name="feeDetails"></param>
        /// <returns></returns>
        public int GetCommomPactItemPubCost(Register r, ref ArrayList feeDetails)
        {
            decimal tot = 0m;
            bool isDrgLmt = controlParamIntegrate.GetControlParam<string>("DrgLmt", true, "") == "1";

            FS.HISFC.BizLogic.Fee.Outpatient MgrOutPatient = new FS.HISFC.BizLogic.Fee.Outpatient();
            decimal DayDrugFee = MgrOutPatient.GetDayDrugFee(r.SSN,r.Name);
            if (DayDrugFee == -1)
            {
                return -1;
            }


            this.Clear();

            #region 有效性检查
            if (r == null || r.ID == "")
            {
                errMsg = "患者基本信息为空！";
                return -1;
            }
            if (feeDetails == null)
            {
                errMsg = "费用明细集合为空！";
                return -1;
            }

            if (r.Pact == null || r.Pact.ID == "")
            {
                errMsg = "患者合同单位为空！";
                return -1;
            }
            #endregion

            #region 事务设置

            FS.HISFC.BizLogic.Fee.Outpatient outPatient = new FS.HISFC.BizLogic.Fee.Outpatient();
            FS.HISFC.BizLogic.Fee.FeeCodeStat feeMgr = new FS.HISFC.BizLogic.Fee.FeeCodeStat();
            FS.HISFC.BizLogic.Manager.PactStatRelation myRelation = new FS.HISFC.BizLogic.Manager.PactStatRelation();

            if (this.trans != null)
            {
                outPatient.SetTrans(this.trans);
                feeMgr.SetTrans(this.trans);
                myRelation.SetTrans(this.trans);
            }

            #endregion

            #region 省限、市限等特殊项目的处理

            //------修改ItemRateFlag(判断自费、记账、特殊)和NewItemRate
            if (this.SpecialItemPactLimit(r, ref feeDetails) == -1)
            {
                errMsg = "获取省限、市限，自费项目信息出错！";
                return -1;
            }

            #endregion

            #region 获取所选的限额等级

            DataSet dsRelation = new DataSet();
            int iReturn = myRelation.GetStatRelation(r.Pact.ID, r.User03, ref dsRelation);
            if (iReturn < 0)
            {
                MessageBox.Show("获得限额出错!" + myRelation.Err);
                return -1;
            }
            if (dsRelation != null)
            {
                this.relations = new ArrayList();
                FS.FrameWork.Models.NeuObject obj = null;
                foreach (DataRow row in dsRelation.Tables[0].Rows)
                {
                    obj = new FS.FrameWork.Models.NeuObject();
                    obj.ID = row["FEE_STAT_CODE"].ToString();
                    obj.Name = row["FEE_STAT_NAME"].ToString();
                    obj.Memo = row["COST_LIMIT"].ToString();
                    obj.User01 = "0";
                    obj.User02 = "0";
                    obj.User03 = "0";
                    this.relations.Add(obj);
                }
            }
            dsRelation = null;

            #endregion

            #region 处理费用明细

            //用来存储项目的限额对象集合
            ArrayList ItemFeeLimitList = new ArrayList();

            foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList f in feeDetails)
            {
                if (f == null)
                {
                    continue;
                }

                #region 清空各项金额 只有tot_cost有用

                f.FT.OwnCost = 0;
                f.FT.PayCost = 0;
                f.FT.PubCost = 0;

                #endregion

                #region 特批项目处理

                if (f.ItemRateFlag == "3")
                {
                    f.FT.OwnCost = FS.FrameWork.Public.String.FormatNumber(FS.FrameWork.Function.NConvert.ToDecimal(f.FT.User03), 2);//修改比例设置的自费金额
                    //先算记账金额，在算自付金额

                    f.FT.PayCost = FS.FrameWork.Public.String.FormatNumber((f.FT.TotCost - f.FT.OwnCost) * f.NewItemRate, 2);
                    f.FT.PubCost = f.FT.TotCost - f.FT.OwnCost - f.FT.PayCost;
                    //f.FT.PubCost = FS.FrameWork.Public.String.FormatNumber((f.FT.TotCost - f.FT.OwnCost) * (1 - f.NewItemRate), 4);
                    //f.FT.PayCost = f.FT.TotCost - f.FT.OwnCost - f.FT.PubCost;
                    f.FT.ExcessCost = 0;
                    f.FT.DrugOwnCost = 0;
                    continue;
                }

                #endregion

                #region 基本项目<--->合同单位<--->最小费用 获取维护的自费项目

                //当前项目在项目限额记录集合中的项目信息
                FS.FrameWork.Models.NeuObject CurrentItemLimit = new FS.FrameWork.Models.NeuObject();

                //获取当前项目自负比例，找不到维护去最小费用的，找不到取合同单位的
                GetCurrentItemFeeLimitCostByItem(r, ItemFeeLimitList, f, ref CurrentItemLimit);

                if (!string.IsNullOrEmpty(CurrentItemLimit.User02))
                {
                    f.NewItemRate = FS.FrameWork.Function.NConvert.ToDecimal(CurrentItemLimit.User02);//自负比例
                }

                #endregion

                #region 自费处理

                if (FS.FrameWork.Function.NConvert.ToDecimal(f.NewItemRate) == 1)
                {
                    f.FT.OwnCost = f.FT.TotCost;
                    f.FT.PayCost = 0;// FS.NFC.Public.String.FormatNumber(f.FT.TotCost * f.NewItemRate, 2);
                    f.FT.PubCost = 0;
                    f.FT.ExcessCost = 0;
                    f.FT.DrugOwnCost = f.FT.TotCost;
                    continue;
                }

                #endregion



                //------------------------获取自负金额
                //f.FT.PayCost = FS.FrameWork.Public.String.FormatNumber((f.FT.TotCost - f.FT.OwnCost) * f.NewItemRate, 2);
                //先算记账金额，在算自付金额
                f.FT.PubCost = FS.FrameWork.Public.String.FormatNumber((f.FT.TotCost - f.FT.OwnCost) * (1 - f.NewItemRate), 4);
                //------------------------获取自负金额


                //根据最小费用代码获取公费待遇的费用大类代码和名称
                ArrayList stats = feeMgr.GetGFDYFeeCodeStatByFeeCode(f.Item.MinFee.ID);

                //不存在超标限制
                if (stats == null)
                {
                    //省略不超标处理---获取项目或者最小费用有没有维护

                    //f.FT.PubCost = f.FT.TotCost - f.FT.PayCost;
                    f.FT.PayCost = f.FT.TotCost - f.FT.PubCost;
                    f.FT.ExcessCost = 0;
                    f.FT.DrugOwnCost = 0;

                }
                else
                {

                    //获得限定额度关系
                    FS.FrameWork.Models.NeuObject relation = GetRelation(stats, relations);

                    if (relation == null)
                    {

                        //f.FT.PubCost = f.FT.TotCost - f.FT.PayCost;
                        f.FT.PayCost = f.FT.TotCost - f.FT.PubCost;
                        f.FT.ExcessCost = 0;
                        f.FT.DrugOwnCost = 0;

                        continue;
                    }
                    else
                    {

                        //最小费用累加器
                        decimal tempTotCost = FS.FrameWork.Function.NConvert.ToDecimal(relation.User01);//临时总费用
                        decimal limitCost = FS.FrameWork.Function.NConvert.ToDecimal(relation.Memo);//临时总限额
                        //----------------------0914记得加参数其他医院不这么走

                        if (f.Item.ItemType == EnumItemType.Drug && isDrgLmt)
                        {
                            if (DayDrugFee > 0)
                            {
                                limitCost = limitCost - DayDrugFee;
                            }

                            if (limitCost < 0)
                            {
                                limitCost = 0;
                            }
                        }

                        //公费报销部分金额+已经累计的公费金额大于当前限额，说明超标
                        if (f.FT.TotCost - f.FT.OwnCost + tempTotCost > limitCost)
                        {
                            //------------------超标处理
                            //DealItemOverLoad(f, ref relation, tempTotCost, limitCost);

                            //做超标处理
                            FS.FrameWork.Models.NeuObject OverLoadObj = null;
                            //如果没有则说明是第一次超标，否则不是第一次超标
                            OverLoadObj = helper.GetObjectFromID(relation.ID);
                            //第一次遇到超标
                            if (OverLoadObj == null)
                            {
                                //创建超标实体，用来标识已经遇到了超标
                                OverLoadObj = new FS.FrameWork.Models.NeuObject();
                                OverLoadObj.ID = relation.ID;
                                this.AddObjectToHelper(ref  OverLoadObj, relation.Name);

                                f.FT.OwnCost = FS.FrameWork.Public.String.FormatNumber(f.FT.TotCost + tempTotCost - limitCost, 4);
                                //f.FT.PayCost = FS.FrameWork.Public.String.FormatNumber((limitCost - tempTotCost) * f.NewItemRate, 4);
                                //f.FT.PubCost = f.FT.TotCost - f.FT.OwnCost - f.FT.PayCost;

                                f.FT.PubCost = FS.FrameWork.Public.String.FormatNumber((limitCost - tempTotCost) * (1 - f.NewItemRate), 4);
                                f.FT.PayCost = f.FT.TotCost - f.FT.OwnCost - f.FT.PubCost;

                                f.FT.ExcessCost = f.FT.OwnCost;//非真正的药品超标金额,所有的超标金额
                                relation.User01 = limitCost.ToString();//下次超标
                                //累加当前公费记帐金额
                                //obj.User01= System.Convert.ToString(f.FT.PayCost + f.FT.PubCost + FS.FrameWork.Function.NConvert.ToDecimal(obj.User01));
                            }
                            else
                            {
                                f.FT.OwnCost = f.FT.TotCost;
                                f.FT.PayCost = 0;
                                f.FT.PubCost = 0;
                                relation.User01 = limitCost.ToString();
                                f.FT.ExcessCost = f.FT.OwnCost;
                            }
                        }
                        else
                        {
                            //------------------正常处理

                            //计算公费报销金额
                            //f.FT.PubCost = f.FT.TotCost - f.FT.OwnCost - f.FT.PayCost;//门诊公费自费部分大多数情况为0
                            f.FT.PayCost = f.FT.TotCost - f.FT.OwnCost - f.FT.PubCost;//门诊公费自费部分大多数情况为0
                            //超标部分金额为0
                            f.FT.ExcessCost = 0;
                            //累加当前公费记帐金额
                            relation.User01 = System.Convert.ToString(f.FT.PayCost + f.FT.PubCost + FS.FrameWork.Function.NConvert.ToDecimal(relation.User01));
                            //设置药品自费金额
                            f.FT.DrugOwnCost = 0;
                        }
                        //用来记录该患者所有的超标部分的金额
                        tot += FS.FrameWork.Function.NConvert.ToDecimal(f.FT.ExcessCost);//目前视乎没用到
                    }
                }
            }

            #endregion
            //将费用信息汇总后存放在挂号信息里
            ComputeFee(r, feeDetails);

            return 1;
        }
        /// <summary>
        /// 根据合同单位获取该合同单位维护的最小费用限额信息
        /// </summary>
        /// <param name="PactID"></param>
        /// <returns></returns>
        public ArrayList GetMinFeeLimitCostByPactID(string PactID)
        {
            FS.HISFC.BizLogic.Fee.PactUnitItemRate PactItemRate = new FS.HISFC.BizLogic.Fee.PactUnitItemRate();
            FS.HISFC.BizLogic.Fee.PactUnitInfo PactManagmentForLimit = new FS.HISFC.BizLogic.Fee.PactUnitInfo();
            if (this.trans != null)
            {
                PactItemRate.SetTrans(this.trans);
                PactManagmentForLimit.SetTrans(this.trans);
            }
            string phaLimitType = "0";
            FS.HISFC.Models.Base.PactInfo PactUnitInfolimit = PactManagmentForLimit.GetPactUnitInfoByPactCode(PactID);
            if (PactUnitInfolimit == null)
            {
                this.errMsg = "获得合同单位信息出错，" + PactManagmentForLimit.Err;
                return null;
            }
            try
            {
                if (PactUnitInfolimit.ShortName == "0" || PactUnitInfolimit.ShortName == "")
                {
                    phaLimitType = "0";
                }
                else if (PactUnitInfolimit.ShortName == "1")
                {
                    phaLimitType = "1";
                }
                else if (PactUnitInfolimit.ShortName == "2")
                {
                    phaLimitType = "2";
                }
            }
            catch
            {
                this.errMsg = "获得合同单位信息出错，" + PactManagmentForLimit.Err;
                return null;
            }
            //用来保存项目的限额信息
            FS.FrameWork.Models.NeuObject objLimit = null;

            try
            {
                //最小费用集合
                ArrayList MinFeelist = new ArrayList();

                //维护了限额的最小费用集合
                ArrayList MinFeeLimitList = new ArrayList();

                //查询某一合同单位下的最小费用信息 
                MinFeelist = PactItemRate.GetPactUnitItemRate(PactID, 0);

                //该合同单位下没有维护最小费用
                if (MinFeelist.Count == 0)
                {
                    return null;
                }

                //记录该合同单位下已经维护限额的最小费用
                foreach (FS.HISFC.Models.Base.PactItemRate ObjPactItemRate in MinFeelist)
                {
                    //该最小费用维护了限额
                    //if (ObjPactItemRate.Rate.DerateRate.ToString() != "0") //del xf 2012-02-16 限额DerateRate->Quota
                    if (ObjPactItemRate.Rate.Quota.ToString() != "0")
                    {
                        //创建一条记录维护限额的最小费用
                        objLimit = new FS.FrameWork.Models.NeuObject();
                        //标识获取的限额为最小费用的限额
                        objLimit.ID = ObjPactItemRate.PactItem.ID;//用来标识获取的比率为最小费用的比率
                        objLimit.Name = ObjPactItemRate.PactItem.Name;//记录有限额控制的最小费用名称
                        //记录项目最小费用的限额
                        //objLimit.User01 = ObjPactItemRate.Rate.DerateRate.ToString();del xf 2012-02-16
                        objLimit.User01 = ObjPactItemRate.Rate.Quota.ToString();
                        //设置维护限额的方式
                        objLimit.Memo = phaLimitType;
                        //保存该最小费用
                        MinFeeLimitList.Add(objLimit);
                    }
                }
                return MinFeeLimitList;
            }
            catch (Exception ex)
            {
                this.errMsg = "获得合同单位信息出错" + ex.Message;
                return null;
            }
        }
        /// <summary>
        /// 获取项目自负比例,取到返回，取不到取所属的最小费用维护的比例
        /// </summary>
        /// <param name="r"></param>
        /// <param name="ItemFeeLimitList"></param>
        /// <param name="f"></param>
        /// <param name="CurrentItemLimit"></param>
        private void GetCurrentItemFeeLimitCostByItem(FS.HISFC.Models.Registration.Register r, ArrayList ItemFeeLimitList, FS.HISFC.Models.Fee.Outpatient.FeeItemList f, ref FS.FrameWork.Models.NeuObject CurrentItemLimit)
        {
            //该项目限记额录中已经存在该项目的限额信息记录，则不需要重新读取项目限额信息，直接从项目限额记录中读取即可。    
            for (int i = 0; i < ItemFeeLimitList.Count; i++)
            {
                FS.FrameWork.Models.NeuObject ItemFeeLimit = ItemFeeLimitList[i] as FS.FrameWork.Models.NeuObject;
                if (f.Item.ID == ItemFeeLimit.ID)//当前项目限额记录表中存在该项目的限额信息，直接读取
                {
                    CurrentItemLimit = ItemFeeLimit;
                    break;
                }
            }
            if (CurrentItemLimit.ID == "")//当前项目限额列表中不存在该项目的限额信息
            {
                //获取该项目的限额信息
                FS.HISFC.Models.Base.FTRate ItemlimitCost = this.GetItemFeeRate(r.Pact.ID, f.Item);
                //表示存在该项目的限额信息  
                CurrentItemLimit.ID = f.Item.ID;
                CurrentItemLimit.Name = f.Item.Name;
                if (ItemlimitCost != null)
                {
                    CurrentItemLimit.User01 = ItemlimitCost.OwnRate.ToString();//自费比例
                    CurrentItemLimit.User02 = ItemlimitCost.PayRate.ToString();//自负比例，页面层payratio=1 自费
                    CurrentItemLimit.User03 = ItemlimitCost.RebateRate.ToString();//优惠比例
                }

                ItemFeeLimitList.Add(CurrentItemLimit);
            }
        }
        /// <summary>
        /// 获取项目的比率、限额（不考虑最小费用和合同单位的限额，最小费用和合同单位的限额在别的地方获取）
        /// </summary>
        /// <param name="PactID">合同单位</param>
        /// <param name="item">项目信息</param>
        /// <returns>限额</returns>
        public FS.HISFC.Models.Base.FTRate GetItemRateLimit(string PactID, FS.HISFC.Models.Base.Item item)
        {

            FS.HISFC.BizLogic.Fee.PactUnitItemRate PactItemRate = new FS.HISFC.BizLogic.Fee.PactUnitItemRate();
            PactItemRate.SetTrans(this.trans);

            FS.HISFC.Models.Base.PactItemRate ObjPactItemRate = null;
            decimal ItemLimit = 0;
            try
            {

                //获取项目的限额
                ObjPactItemRate = PactItemRate.GetOnepPactUnitItemRateByItem(PactID, item.ID);
                if (ObjPactItemRate != null)
                {
                    //ItemLimit = ObjPactItemRate.Rate.DerateRate;
                    ItemLimit = ObjPactItemRate.Rate.Quota;
                }

                //该合同单位没有维护这个项目
                if (ObjPactItemRate == null || ObjPactItemRate.Rate.OwnRate == 0)
                {
                    //获取该项目的最小费用比率
                    ObjPactItemRate = PactItemRate.GetOnePaceUnitItemRateByFeeCode(PactID, item.MinFee.ID);
                    if (ObjPactItemRate == null || ObjPactItemRate.Rate.OwnRate == 0)
                    {
                        //取该项目的合同单位的比率
                        FS.HISFC.BizLogic.Fee.PactUnitInfo PactManagment = new FS.HISFC.BizLogic.Fee.PactUnitInfo();

                        if (this.trans != null)
                        {
                            PactManagment.SetTrans(this.trans);
                        }

                        FS.HISFC.Models.Base.PactInfo PactUnitInfo = PactManagment.GetPactUnitInfoByPactCode(PactID);

                        if (PactUnitInfo == null)
                        {
                            this.errMsg = "获得合同单位信息出错，" + PactManagment.Err;
                            return null;
                        }
                        try
                        {
                            ObjPactItemRate = new FS.HISFC.Models.Base.PactItemRate();
                            ObjPactItemRate.Rate.OwnRate = PactUnitInfo.Rate.OwnRate;
                            //ObjPactItemRate.Rate.DerateRate = 0;mdf xf
                            ObjPactItemRate.Rate.Quota = 0;
                        }
                        catch
                        {
                            this.errMsg = "获得合同单位信息出错，" + PactManagment.Err;
                            return null;
                        }
                    }

                }
                //ObjPactItemRate.Rate.DerateRate = ItemLimit;mdf xf
                ObjPactItemRate.Rate.Quota = ItemLimit;

            }
            catch (Exception ex)
            {
                this.errMsg = "获得合同单位信息出错" + ex.Message;
                return null;
            }
            return ObjPactItemRate.Rate;
        }

        /// <summary>
        /// 根据当前项目的最小费用限额
        /// </summary>
        /// <param name="ItemID"></param>
        /// <param name="MinFeeLimitList"></param>
        /// <returns></returns>
        /// bigai
        public FS.FrameWork.Models.NeuObject GetCurrentMinFeeLimitCostByItem(string ItemID, ArrayList MinFeeLimitList)
        {
            FS.FrameWork.Models.NeuObject MinFeeObj = null;
            foreach (FS.FrameWork.Models.NeuObject obj in MinFeeLimitList)
            {
                if (ItemID == obj.ID)
                {
                    MinFeeObj = obj;
                    break;
                }
                else if (obj.Memo == "1" && (obj.ID == "002" || obj.ID == "003"))
                {
                    if (ItemID == "002" || ItemID == "003")
                    {
                        MinFeeObj = obj;
                        break;
                    }
                }
                else if (obj.Memo == "2" && (obj.ID == "001" || obj.ID == "002" || obj.ID == "003"))
                {
                    if (ItemID == "001" || ItemID == "002" || ItemID == "003")
                    {
                        MinFeeObj = obj;
                        break;
                    }
                }
            }
            return MinFeeObj;
        }

        /// <summary>
        /// 不存在限额时，设置公费部分金额 
        /// </summary>
        /// <param name="f"></param>
        private static void SetItemPubCostWhenItemNoLimitCost(FS.HISFC.Models.Fee.Outpatient.FeeItemList f)
        {
            f.FT.PubCost = f.FT.TotCost - f.FT.PayCost;
            f.FT.ExcessCost = 0;
            f.FT.DrugOwnCost = 0;
        }

        /// <summary>
        /// 根据限额获取公费金额
        /// </summary>
        /// <param name="isDeal"></param>
        /// <param name="f"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        private void GetItemPubCost(FS.HISFC.Models.Fee.Outpatient.FeeItemList f, ref FS.FrameWork.Models.NeuObject obj)
        {
            //最小费用累加器
            decimal tempTotCost = FS.FrameWork.Function.NConvert.ToDecimal(obj.User02);
            decimal limitCost = FS.FrameWork.Function.NConvert.ToDecimal(obj.User01);
            if (this.IsOverLoad(f, tempTotCost, limitCost))
            {
                DealItemOverLoad(f, ref obj, tempTotCost, limitCost);
            }
            else
            {
                //计算公费报销金额
                f.FT.PubCost = f.FT.TotCost - f.FT.OwnCost - f.FT.PayCost;
                //超标部分金额为0
                f.FT.ExcessCost = 0;
                //累加当前公费记帐金额
                obj.User02 = System.Convert.ToString(f.FT.PayCost + f.FT.PubCost + FS.FrameWork.Function.NConvert.ToDecimal(obj.User02));
                //设置药品自费金额
                f.FT.DrugOwnCost = 0;
            }
        }
        /// <summary>
        /// 是否超标
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        private bool IsOverLoad(FS.HISFC.Models.Fee.Outpatient.FeeItemList f, decimal totTempCost, decimal limitCost)
        {
            //公费报销部分金额+已经累计的公费金额大于当前限额，说明超标
            if (f.FT.TotCost - f.FT.OwnCost + totTempCost > limitCost)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 处理超标问题
        /// </summary>
        /// <param name="f"></param>
        /// <param name="obj"></param>
        /// <param name="tempTotCost"></param>
        /// <param name="limitCost"></param>
        /// <returns></returns>
        private void DealItemOverLoad(FS.HISFC.Models.Fee.Outpatient.FeeItemList f,
            ref  FS.FrameWork.Models.NeuObject obj, decimal tempTotCost, decimal limitCost)
        {
            //做超标处理
            FS.FrameWork.Models.NeuObject OverLoadObj = null;
            //如果没有则说明是第一次超标，否则不是第一次超标
            OverLoadObj = helper.GetObjectFromID(obj.ID);
            //第一次遇到超标
            if (OverLoadObj == null)
            {
                //创建超标实体，用来标识已经遇到了超标
                OverLoadObj = new FS.FrameWork.Models.NeuObject();
                OverLoadObj.ID = obj.ID;
                this.AddObjectToHelper(ref  OverLoadObj, obj.Name);

                f.FT.OwnCost = FS.FrameWork.Public.String.FormatNumber(f.FT.TotCost + tempTotCost - limitCost, 2);
                f.FT.PayCost = FS.FrameWork.Public.String.FormatNumber((limitCost - tempTotCost) * f.NewItemRate, 2);
                f.FT.PubCost = f.FT.TotCost - f.FT.OwnCost - f.FT.PayCost;
                f.FT.ExcessCost = f.FT.OwnCost;//非真正的药品超标金额,所有的超标金额
                obj.User01 = limitCost.ToString();//下次超标
                //累加当前公费记帐金额
                //obj.User01= System.Convert.ToString(f.FT.PayCost + f.FT.PubCost + FS.FrameWork.Function.NConvert.ToDecimal(obj.User01));
            }
            else
            {
                f.FT.OwnCost = f.FT.TotCost;
                f.FT.PayCost = 0;
                f.FT.PubCost = 0;
                obj.User01 = limitCost.ToString();
                f.FT.ExcessCost = f.FT.OwnCost;
            }
        }

        /// <summary>
        /// 将对大类的超标处理保存
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="FEE_STAT_NAME"></param>
        private void AddObjectToHelper(ref FS.FrameWork.Models.NeuObject obj, string FEE_STAT_NAME)
        {
            ArrayList al = helper.ArrayObject;

            DialogResult r;
            r = MessageBox.Show(FEE_STAT_NAME + "的限额已经超标，超标部分将按自费处理。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            //按照超标处理
            if (r == DialogResult.OK)
            {
                obj.Memo = "TRUE";
                al.Add(obj);
                helper.ArrayObject = al;
            }
        }

        /// <summary>
        /// 根据项目类别获取项目自费部分金额 
        /// </summary>
        /// <param name="r"></param>
        /// <param name="f"></param>
        private void GetItemOwnCostByItemRateFlag(FS.HISFC.Models.Registration.Register r, FS.HISFC.Models.Fee.Outpatient.FeeItemList f)
        {
            //先判断项目类别标志
            //如果为自费项目
            if (f.ItemRateFlag == "1")
            {
                f.FT.OwnCost = f.FT.TotCost;//全部自费
                f.FT.PubCost = 0;//公费部分为0
                f.FT.ExcessCost = 0;//超标金额为0
                f.FT.DrugOwnCost = f.FT.OwnCost;//设置药物自费金额 
            }
            else if (f.ItemRateFlag == "2")//处理记帐部分，需要读取维护的合同单位相关的项目比率
            {
                FS.HISFC.Models.Base.FTRate ItemRate = this.GetItemRateLimit(r.Pact.ID, f.Item);
                //该项目没有维护项目比率
                if (ItemRate == null)
                {
                    //continue;
                }
                else
                {
                    f.FT.OwnCost = FS.FrameWork.Public.String.FormatNumber(f.FT.TotCost * ItemRate.OwnRate, 2);
                }
            }
            else if (f.ItemRateFlag == "3")//特殊项目，则不取项目比率，直接取界面维护的比率
            {
                //特殊项目，则自费部分为总金额*新维护的自费比例，这一部分不计入日限额中
                f.FT.OwnCost = FS.FrameWork.Public.String.FormatNumber(f.FT.TotCost * f.NewItemRate, 2);
            }
        }

        #region 获取经过计算后门诊收费的各项费用汇总信息

        /// <summary>
        /// 获取经过计算后门诊收费的各项费用汇总信息
        /// </summary>
        /// <param name="r"></param>
        /// <param name="feeDetails"></param>
        private void ComputeFee(FS.HISFC.Models.Registration.Register r, System.Collections.ArrayList feeDetails)
        {
            decimal totCost = 0;
            decimal payCost = 0;
            decimal pubcost = 0;
            decimal ownCost = 0;

            decimal detailTotCost = 0;
            decimal detailPayCost = 0;
            decimal detailPubcost = 0;
            decimal detailOwnCost = 0;

            FS.HISFC.Models.Fee.Outpatient.FeeItemList fPubMax = null;
            FS.HISFC.Models.Fee.Outpatient.FeeItemList fPayMax = null;

            foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList f in feeDetails)
            {
                totCost += f.FT.TotCost;
                payCost += f.FT.PayCost;
                pubcost += f.FT.PubCost;
                ownCost += f.FT.OwnCost;

                f.FT.PubCost = FS.FrameWork.Public.String.FormatNumber(f.FT.PubCost, 2);
                f.FT.OwnCost = FS.FrameWork.Public.String.FormatNumber(f.FT.OwnCost, 2);
                if (Function.IsDrug(f.Item.ItemType, f.Item.MinFee))
                {
                    f.FT.ExcessCost = FS.FrameWork.Public.String.FormatNumber(f.FT.ExcessCost, 2);
                    f.FT.DrugOwnCost = FS.FrameWork.Public.String.FormatNumber(f.FT.DrugOwnCost, 2);
                }
                else
                {
                    f.FT.ExcessCost = 0;
                    f.FT.DrugOwnCost = 0;
                }
                f.FT.PayCost = f.FT.TotCost - f.FT.OwnCost - f.FT.PubCost;

                detailPayCost += f.FT.PayCost;
                detailPubcost += f.FT.PubCost;
                detailOwnCost += f.FT.OwnCost;



            }

            pubcost = FS.FrameWork.Public.String.FormatNumber(pubcost, 2);
            payCost = totCost - ownCost - pubcost;

            decimal chaePubCost = pubcost - detailPubcost;
            decimal chaePayCost = payCost - detailPayCost;

            if (chaePubCost != 0 && chaePayCost != 0)
            {
                FeeItemList feeItemList = new FeeItemList();
                RoundItemInfo roundItemInfo = new RoundItemInfo();
                roundItemInfo.OutPatientFeeRoundOff(r, chaePayCost, chaePubCost, ref feeItemList, ((FeeItemList)feeDetails[0]).RecipeSequence);

                feeDetails.Add(feeItemList);
            }



            r.SIMainInfo.TotCost = totCost;
            r.SIMainInfo.PayCost = payCost;
            r.SIMainInfo.PubCost = pubcost;
            r.SIMainInfo.OwnCost = ownCost;
        }

        #endregion

        /// <summary>
        /// 获取当前费用代码对应的限额实体
        /// </summary>
        /// <param name="stats">当前费用代码对应的统计大类</param>
        /// <param name="relations">限额等级实体集合</param>
        /// <returns>当前合同单位的限额等级实体</returns>
        private FS.FrameWork.Models.NeuObject GetRelation(ArrayList stats, ArrayList relations)
        {
            foreach (FS.FrameWork.Models.NeuObject stat in stats)
            {
                foreach (FS.FrameWork.Models.NeuObject obj in relations)
                {
                    if (stat.ID == obj.ID)
                    {
                        return obj;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 获取合同单位下项目、最小费用所设置的比例
        /// </summary>
        /// <param name="PactID">合同单位</param>
        /// <param name="item">项目信息</param>
        /// <returns>设置的比例</returns>
        public FS.HISFC.Models.Base.FTRate GetItemFeeRate(string PactID, FS.HISFC.Models.Base.Item item)
        {

            FS.HISFC.BizLogic.Fee.PactUnitItemRate PactItemRate = new FS.HISFC.BizLogic.Fee.PactUnitItemRate();
            PactItemRate.SetTrans(this.trans);

            FS.HISFC.Models.Base.PactItemRate ObjPactItemRate = null;
            try
            {

                //获取合同单位与项目关联所设置的比率
                ObjPactItemRate = PactItemRate.GetOnepPactUnitItemRateByItem(PactID, item.ID);

                //--------待考虑有无这种设置

                //该合同单位没有维护这个项目
                if (ObjPactItemRate == null)
                {
                    //获取该项目的最小费用比率
                    ObjPactItemRate = PactItemRate.GetOnePaceUnitItemRateByFeeCode(PactID, item.MinFee.ID);
                }
                if (ObjPactItemRate == null)
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                this.errMsg = "获得合同单位信息出错" + ex.Message;
                return null;
            }
            return ObjPactItemRate.Rate;
        }

        private int SpecialItemPactLimit(Register r, ref ArrayList feeDetails)
        {
            /*获取省市公费类别 省公费，省离休，市公费，市离休，特约单位等
             *特约单位的要根据医疗证号来取
             */
            string pactType = "";

            pactType = this.GetPactLimitType(r.Pact.ID);


            if (pactType == null)
            {
                this.errMsg = "获得患者的结算类别出错！";
                return -1;
            }

            //根据合同单位所属，设置需要自费的项目
            switch (pactType)
            {
                case "省公费":
                    {
                        this.SetOwnPayItem(true, false, true, ref feeDetails);
                        break;
                    }
                case "省离休":
                    {
                        /*省离休药品不受限，非药品受限*/
                        ArrayList alTemp = null;

                        //药品
                        alTemp = this.FilterFeeItem(feeDetails, true);
                        this.SetOwnPayItem(false, false, true, ref alTemp);

                        //非药品
                        alTemp = this.FilterFeeItem(feeDetails, false);
                        this.SetOwnPayItem(true, false, true, ref alTemp);

                        break;
                    }
                case "市公费":
                    {
                        this.SetOwnPayItem(false, true, true, ref feeDetails);
                        break;
                    }
                case "市离休":
                    {
                        this.SetOwnPayItem(false, true, true, ref feeDetails);
                        break;
                    }
                case "特约单位":
                    {
                        this.SetOwnPayItem(false, false, true, ref feeDetails);
                        break;
                    }
                case "无限制":
                    {
                        this.SetOwnPayItem(false, false, true, ref feeDetails);
                        break;
                    }
                default:
                    {
                        this.SetOwnPayItem(false, false, true, ref feeDetails);
                        break;
                    }

            }
            return 1;
        }

        /// <summary>
        /// 获取当前特约单位的处理办法 1 省公医方式 2 市、区公医方式
        /// </summary>
        /// <param name="pactCode">合同单位编码</param>
        /// <returns>null 失败 处理方法名称</returns>
        private string GetPactLimitType(string pactCode)
        {
            FS.HISFC.BizProcess.Integrate.Manager conMgr = new FS.HISFC.BizProcess.Integrate.Manager();
            if (this.trans != null)
            {
                conMgr.SetTrans(this.trans);
            }
            FS.FrameWork.Models.NeuObject limitType = null;
            limitType = conMgr.GetConstant("BILLPACT", pactCode);
            if (limitType == null)
            {
                return null;
            }
            return limitType.Name;
        }

        /// <summary>
        /// 区分药品和非药品项目
        /// </summary>
        /// <param name="alFee"></param>
        /// <returns></returns>
        private ArrayList FilterFeeItem(ArrayList alFee, bool isPhamarcy)
        {
            ArrayList al = new ArrayList();

            //过滤药品项目
            if (isPhamarcy)
            {
                foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList item in alFee)
                {
                    if (item.Item.ItemType == EnumItemType.Drug)
                    {
                        al.Add(item);
                    }
                }
                return al;
            }
            else//非药品项目
            {
                foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList item in alFee)
                {
                    if (item.Item.ItemType != EnumItemType.Drug)
                    {
                        al.Add(item);
                    }
                }
                return al;
            }
        }

        /// <summary>
        /// 根据参数设置项目为自费
        /// </summary>
        /// <param name="bSHX">省限标志</param>
        /// <param name="bSX">市限标志</param>
        /// /// <param name="bZF">自费标志</param>
        /// <param name="alFee">收费项目</param>
        private void SetOwnPayItem(bool bSHX, bool bSX, bool bZF, ref ArrayList alFee)
        {
            FS.SOC.HISFC.Fee.BizLogic.ComItemExtendInfo comItemExtendMgr = new FS.SOC.HISFC.Fee.BizLogic.ComItemExtendInfo();

            if (this.trans != null)
            {
                comItemExtendMgr.SetTrans(this.trans);
            }

            foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList feeitem in alFee)
            {
                FS.SOC.HISFC.Fee.Models.ComItemExtend itemExtend = null;

                List<FS.SOC.HISFC.Fee.Models.ComItemExtend> lst = comItemExtendMgr.QueryItemListByItemCode(feeitem.Item.ID);

                if (lst == null || lst.Count == 0)
                {
                    continue;
                }
                else
                {
                    itemExtend = lst[0] as FS.SOC.HISFC.Fee.Models.ComItemExtend;
                }

                if (itemExtend.ProvinceFlag == "1")//省限项目
                {
                    if (bSHX)
                    {
                        feeitem.OrgItemRate = 1;//修改原始比例为100%
                        if (feeitem.ItemRateFlag != "3")
                        {
                            feeitem.NewItemRate = 1;//修改新比例为100%
                            feeitem.ItemRateFlag = "1";
                        }
                    }
                }
                if (itemExtend.CityFlag == "1")//市限项目
                {
                    if (bSX)
                    {
                        feeitem.OrgItemRate = 1;//修改原始比例为100%
                        if (feeitem.ItemRateFlag != "3")
                        {
                            feeitem.NewItemRate = 1;//修改新比例为100%
                            feeitem.ItemRateFlag = "1";
                        }
                    }
                }
                if (itemExtend.ZFFlag == "1")//自费项目
                {
                    if (bZF)
                    {
                        feeitem.OrgItemRate = 1;//修改原始比例为100%
                        if (feeitem.ItemRateFlag != "3")
                        {
                            feeitem.NewItemRate = 1;//修改新比例为100%
                            feeitem.ItemRateFlag = "1";
                        }
                    }
                }
            }
        }

        #region IMedcareExtend 成员

        public bool IsLocalProcess
        {
            get
            {
                return false;
            }
            set
            {

            }
        }

        public int LocalBalanceOutpatient(Register r, ref ArrayList feeDetails, ArrayList arlOther)
        {
            return 1;
        }

        #endregion
    }
}
