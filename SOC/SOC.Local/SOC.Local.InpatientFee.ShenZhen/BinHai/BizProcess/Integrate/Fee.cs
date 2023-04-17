using System;
using System.Collections;
using System.Collections.Generic;

namespace FS.SOC.Local.InpatientFee.ShenZhen.BinHai.BizProcess.Integrate
{  
    public class Fee : FS.HISFC.BizProcess.Integrate.Fee
    {
        #region 固定费用收取

        public int DoBedItemFee(ArrayList bedItems,
           FS.HISFC.Models.RADT.PatientInfo patient, int days, DateTime operDate, DateTime chargeDate, FS.HISFC.Models.Base.Bed bed)
        {
            //非药品管理类
            FS.HISFC.BizLogic.Fee.Item item = new FS.HISFC.BizLogic.Fee.Item();
            //合同管理类


            FS.HISFC.BizLogic.Fee.PactUnitInfo pactMgr = new FS.HISFC.BizLogic.Fee.PactUnitInfo();
            //常数管理类


            FS.HISFC.BizLogic.Manager.Constant constant = new FS.HISFC.BizLogic.Manager.Constant();
            //事务
            //FS.FrameWork.Management.Transaction trans = new FS.FrameWork.Management.Transaction(this.feeInpatient.Connection);
            //trans.BeginTransaction();

            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            item.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            inpatientManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            pactMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            constant.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            try
            {
                ArrayList alFeeInfo = new ArrayList();
                //床位信息实体
                FS.SOC.Local.InpatientFee.ShenZhen.BinHai.Models.Fee.BedFeeItem bedItem = new FS.SOC.Local.InpatientFee.ShenZhen.BinHai.Models.Fee.BedFeeItem();
                for (int row = 0; row < bedItems.Count; row++)
                {
                    //取待收取的床位信息


                    bedItem = bedItems[row] as FS.SOC.Local.InpatientFee.ShenZhen.BinHai.Models.Fee.BedFeeItem;

                    //如果床位无效，则不进行费用收取


                    if (bedItem.ValidState != FS.HISFC.Models.Base.EnumValidState.Valid) continue;

                    //关闭的床位不收床位费.转科后不释放床位时床位状态设为C . {CA479D1B-BD94-459e-AA19-1AE2C4902DAF}
                    if (bed.Status.ID.ToString() == "C")
                    {
                        continue;
                    }

                    #region 深圳零加成医改政策 自费医保患者收取不同诊查费等  Added By Huangd  2012/09/26

                    switch (bedItem.UseLimit)
                    {
                        case "0":

                            break;
                        case "1":
                            if (patient.Pact.ID != "1") continue;
                            break;
                        case "2":
                            if (patient.Pact.ID != "2") continue;
                            break;
                        default:

                            break;
                    }

                    #endregion

                    #region 判断非在院患者(包床W,挂床H,请假R)是否收取该项目  writed by cuipeng  2005-11
                    if (bed.Status.ID.ToString() == "W" || bed.Status.ID.ToString() == "H" || bed.Status.ID.ToString() == "R")
                    {
                        //如果收费项目对于非在院患者不收取费用,则不处理此项目


                        if (bedItem.ExtendFlag == "0")
                        {
                            continue;
                        }
                        else
                        {
                            //中山特殊 由于数据库有设置所以暂时保留


                            //对于包床患者,固定费用收取名称为"陪人费",金额为床位费的2倍.
                            if (bed.Status.ID.ToString() == "W")
                            {
                                FS.FrameWork.Models.NeuObject obj = constant.GetConstant("FIN_FIXITEM", "BEDWRAP");
                                //if (obj == null)
                                //{
                                //    this.Err = constant.Err;
                                //    this.WriteErr();
                                //    continue;
                                //}

                                ////取原项目(床位费)单价
                                //FS.HISFC.Models.Fee.Item.Undrug tempItem = item.GetValidItemByUndrugCode(bedItem.ID);

                                //if (tempItem == null)
                                //{
                                //    this.Err = item.Err;
                                //    this.WriteErr();
                                //    continue;
                                //}
                                //FS.HISFC.Models.Fee.Item.Undrug peiItem = item.GetValidItemByUndrugCode(obj.Name);
                                //if (peiItem == null)
                                //{
                                //    this.Err = item.Err;
                                //    this.WriteErr();
                                //    continue;
                                //}

                                ////指定收费项目编码(陪人费项目编码)
                                //bedItem.ID = peiItem.ID;
                                //bedItem.Name = peiItem.Name;
                                ////bedItem.ID = obj.Name;

                                ////单价为床位费的2倍


                                //bedItem.User01 = (tempItem.Price * 2).ToString();

                            }
                        }
                    }
                    #endregion

                    #region 判断该项目是否和时间有关，比如空调费、取暖费
                    if (bedItem.IsTimeRelation)
                    {
                        //结束日期 >= 起始日期,认为不跨年度
                        if ((bedItem.EndTime.Month * 100 + bedItem.EndTime.Day) >= (bedItem.BeginTime.Month * 100 + bedItem.BeginTime.Day))
                        {
                            //如果当前时间不在设置时间范围内，则不收取此项目费用


                            if ((operDate.Month * 100 + operDate.Day) > (bedItem.EndTime.Month * 100 + bedItem.EndTime.Day)
                                || (operDate.Month * 100 + operDate.Day) < (bedItem.BeginTime.Month * 100 + bedItem.BeginTime.Day))
                                continue;//--当前日期在计费有效期外


                        }

                        else
                        { //结束日期 < 起始日期 :跨年度


                            //如果当前时间不在设置时间范围内，则不收取此项目费用


                            if ((operDate.Month * 100 + operDate.Day) > (bedItem.EndTime.Month * 100 + bedItem.EndTime.Day)
                                && (operDate.Month * 100 + operDate.Day) < (bedItem.BeginTime.Month * 100 + bedItem.BeginTime.Day))
                                continue;//--当前日期在计费有效期外


                        }
                    }
                    #endregion

                    #region 对于设置跟婴儿有关的固定费用,根据婴儿是否存在而收费


                    bool isBaby = false;//是否婴儿,默认不是婴儿
                    if (bedItem.IsBabyRelation)
                    {
                        if (patient.BabyCount == 0)
                            //婴儿不存在,则不收取此项费用
                            continue;
                        else
                        {
                            //婴儿存在,每个婴儿收取一份


                            isBaby = true;
                            bedItem.Qty = bedItem.Qty * patient.BabyCount;
                        }
                    }
                    #endregion

                    //计算项目数量,如果为0则默认是1
                    if (bedItem.Qty == 0)
                        bedItem.Qty = 1;
                    //根据用户设置的数量倍数,计算应收取数量
                    bedItem.Qty = bedItem.Qty * days;

                    //取收费项目实体信息
                    FS.HISFC.Models.Fee.Item.Undrug undrug = new FS.HISFC.Models.Fee.Item.Undrug();
                    undrug = item.GetValidItemByUndrugCode(bedItem.ID);
                    if (undrug == null)
                    {
                        this.Err = item.Err;
                        continue;
                    }

                    //计算项目价格,根据合同单位和项目计算价格
                    decimal price = 0;
                    decimal orgPrice = 0;
                    //{014680EC-6381-408b-98FB-A549DAA49B82}
                    //if (this.GetPriceForInpatient(patient.Pact.ID, undrug, ref price, ref orgPrice) == -1)
                    if (this.GetPriceForInpatient(patient, undrug, ref price, ref orgPrice) == -1)
                    {
                        this.Rollback();
                        this.Err = "获取项目:" + undrug.ID + "的价格时出错!" + pactMgr.Err;
                        return -1;
                    }

                    //取得的价格不为0,则使用取后的价格
                    if (price != 0) undrug.Price = price;

                    //包床单价固定为床位费的2倍. writed by cuipeng 2005-11
                    if (bed.Status.ID.ToString() == "W")
                    {
                        //undrug.Price = FS.FrameWork.Function.NConvert.ToDecimal(bedItem.User01);
                    }

                    //计费单价为0, 不需要计费
                    if (undrug.Price == 0)
                    {
                        this.Err = "计费单价为0:" + undrug.Name;
                        continue;
                    }


                    undrug.Qty = bedItem.Qty;
                    //医保患者接口
                    //中山一没有需要处理的
                    //实体赋值
                    FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItem = new FS.HISFC.Models.Fee.Inpatient.FeeItemList();
                    feeItem.IsBaby = isBaby;
                    feeItem.Item = undrug;
                    feeItem.NoBackQty = undrug.Qty;
                    feeItem.RecipeNO = inpatientManager.GetUndrugRecipeNO();
                    feeItem.Patient.Pact.PayKind.ID = patient.Pact.PayKind.ID;
                    feeItem.TransType = FS.HISFC.Models.Base.TransTypes.Positive;
                    //feeItem.Order.InDept.ID =
                    feeItem.FeeOper.Dept.ID = patient.PVisit.PatientLocation.Dept.ID;
                    //feeItem.Order.NurseStation.ID = 
                    ((FS.HISFC.Models.RADT.PatientInfo)feeItem.Patient).PVisit.PatientLocation.NurseCell.ID = patient.PVisit.PatientLocation.NurseCell.ID;
                    //feeItem.Order.ReciptDept.ID =
                    feeItem.RecipeOper.Dept.ID = patient.PVisit.PatientLocation.Dept.ID;
                    //feeItem.Order.ExeDept.ID =
                    feeItem.ExecOper.Dept.ID = patient.PVisit.PatientLocation.Dept.ID;
                    if (patient.PVisit.AdmittingDoctor.ID == null || patient.PVisit.AdmittingDoctor.ID == "")
                        patient.PVisit.AdmittingDoctor.ID = "日计费";

                    //feeItem.Order.ReciptDoctor.ID =
                    feeItem.RecipeOper.ID = patient.PVisit.AdmittingDoctor.ID;
                    feeItem.PayType = FS.HISFC.Models.Base.PayTypes.Balanced;
                    //feeItem.IsBrought = "";
                    feeItem.ChargeOper.ID = "日计费";
                    feeItem.ChargeOper.OperTime = chargeDate;
                    feeItem.FeeOper.ID = "日计费";
                    feeItem.FeeOper.OperTime = operDate;
                    feeItem.SequenceNO = row;
                    feeItem.BalanceNO = 0;
                    feeItem.BalanceState = "0";
                    feeItem.FT.TotCost = undrug.Qty * undrug.Price;
                    feeItem.FT.OwnCost = undrug.Qty * undrug.Price;
                    feeItem.FTSource = new FS.HISFC.Models.Fee.Inpatient.FTSource("20");//太长了，费用总表里ext_flag2是2位的
                    //---------------------------公费床位超标调整0818------------------------
                    #region 公费床位超标调整
                    if (patient.Pact.PayKind.ID == "03")
                    {
                        feeItem.FT.OwnCost = 0;//这句一定要加，区别医保收取固定费用后在调整的做法
                        //床位限额
                        decimal BedLimit = FS.FrameWork.Public.String.FormatNumber(patient.FT.BedLimitCost * days, 2);
                        //监护床位限额
                        decimal IcuLimit = FS.FrameWork.Public.String.FormatNumber(patient.FT.AirLimitCost * days, 2);

                        /*字典表中TYPE为BEDLIMITMINFEE
                        CODE为1为普通床，NAME中存的是普通床最小费用CODE
                        CODE为2为监护床，NAME中存的是监护床最小费用CODE
                        */
                        FS.FrameWork.Models.NeuObject conBedMinFee = constant.GetConstant("BEDLIMITMINFEE", "1");
                        string bedMinFeeCode = "";
                        if (conBedMinFee != null)
                        {
                            if (string.IsNullOrEmpty(conBedMinFee.Name))
                            {
                                this.Err = "请在字典表中维护type为BEDLIMITMINFEE,CODE=1,NAME=普通床最小费用代码！";
                            }
                            bedMinFeeCode = conBedMinFee.Name;//普通床最小费用代码
                        }

                        FS.FrameWork.Models.NeuObject conICUBedMinFee = constant.GetConstant("BEDLIMITMINFEE", "2");
                        string icuBedMinFeeCode = "";
                        if (conICUBedMinFee != null)
                        {
                            if (string.IsNullOrEmpty(conICUBedMinFee.Name))
                            {
                                this.Err = "请在字典表中维护type为BEDLIMITMINFEE,CODE=2,NAME=监护床最小费用代码！";
                            }
                            icuBedMinFeeCode = conICUBedMinFee.Name;//监护床最小费用代码
                        }
                        ////判断当天是否已经收过空调费
                        //decimal AirFee = 0;
                        //DateTime FeeBegin = new DateTime(operDate.Year, 10, 26, 0, 0, 0);
                        //DateTime FeeEnd = new DateTime(operDate.Year, 4, 26, 0, 0, 0);
                        //if (operDate > FeeBegin || operDate < FeeEnd)
                        //{
                        //    if (this.inpatientManager.GetAirFee(patient.ID, ref AirFee) > 0)//字典表维护空调费项目type为AIRFEEITEM
                        //    {
                        //        BedLimit = BedLimit - AirFee;
                        //    }
                        //}

                        FS.FrameWork.Models.NeuObject billObj = constant.GetConstant("BILLPACT", patient.Pact.ID);

                        #region 判断超标 处理费用
                        FS.HISFC.Models.Base.FTRate Rate = this.ComputeFeeRate(patient.Pact.ID, feeItem.Item);
                        if (Rate == null)
                        {
                            return -1;
                        }
                        feeItem.User01 = "1";//用作判断标记在FeeManager中不重新调用计算比例函数

                        bool computeLimit = true;//项目是否计算入限额

                        if (billObj != null && billObj.ID.Length >= 0 && billObj.Name == "市公费")
                        {
                            FS.FrameWork.Models.NeuObject unlimitObj = constant.GetConstant("UNLIMITITEM", feeItem.Item.ID);

                            if (unlimitObj != null && unlimitObj.ID.Length >= 1)
                            {
                                computeLimit = false;
                            }
                        }
                        if (feeItem.Item.MinFee.ID == bedMinFeeCode && computeLimit)
                        {
                            if (Rate.OwnRate == 1)
                            {
                                feeItem.FT.OwnCost = feeItem.FT.TotCost;
                            }
                            else
                            {
                                #region 普通床超标处理
                                if (patient.FT.BedOverDeal == "1")
                                {//超标自理
                                    //不超标
                                    if (feeItem.FT.TotCost <= BedLimit)
                                    {
                                        BedLimit = BedLimit - feeItem.FT.TotCost;
                                    }
                                    else
                                    {//超标部分转为自费
                                        feeItem.FT.OwnCost = feeItem.FT.TotCost - BedLimit;
                                        BedLimit = 0;
                                    }
                                }
                                else if (patient.FT.BedOverDeal == "2")
                                {
                                    ////超标不计，报销限额内，剩下的舍掉
                                    if (feeItem.FT.TotCost > BedLimit)
                                    {
                                        feeItem.FT.TotCost = BedLimit;
                                        BedLimit = 0;
                                        if (feeItem.FT.TotCost == 0)
                                        {
                                            continue;
                                        }
                                    }
                                    else
                                    {
                                        BedLimit = BedLimit - feeItem.FT.TotCost;
                                    }
                                }
                                #endregion
                            }
                        }
                        else if (feeItem.Item.MinFee.ID == icuBedMinFeeCode && computeLimit)
                        {

                            if (Rate.OwnRate == 1)
                            {
                                feeItem.FT.OwnCost = feeItem.FT.TotCost;
                            }
                            else
                            {
                                #region 监护床超标处理


                                //调用床位收费函数并且最小费用是010的一定是监护床,如果不是没法处理.
                                //监护床相关床位费也应该维护成010,否则也没法处理

                                //超标自理
                                if (patient.FT.BedOverDeal == "1")
                                {
                                    if (IcuLimit >= feeItem.FT.TotCost)
                                    {
                                        //监护床标准大于监护床费，不超标								
                                        IcuLimit = IcuLimit - feeItem.FT.TotCost;
                                    }
                                    else
                                    {
                                        //超标，超标部分自费
                                        feeItem.FT.OwnCost = feeItem.FT.TotCost - IcuLimit;
                                        IcuLimit = 0;
                                    }
                                }
                                else if (patient.FT.BedOverDeal == "2")
                                {//超标不计，报销限额内，剩下的舍掉
                                    //超标
                                    if (feeItem.FT.TotCost > IcuLimit)
                                    {
                                        feeItem.FT.TotCost = IcuLimit;
                                        IcuLimit = 0;
                                        if (feeItem.FT.TotCost == 0)
                                        {
                                            continue;
                                        }
                                    }
                                    else
                                    {
                                        IcuLimit = IcuLimit - feeItem.FT.TotCost;
                                    }
                                }
                                #endregion
                            }
                        }
                        #endregion
                        this.ComputeCost(feeItem, Rate);
                    }
                    #endregion
                    //-----------------------------------------------------------------------
                    if (this.FeeItem(patient, feeItem) == -1)
                    {
                        this.Rollback();
                        this.Err = "调用住院收费业务层出错!" + this.Err;
                        return -1;
                    }
                    alFeeInfo.Add(feeItem);
                }
                if (inpatientManager.UpdateFixFeeDateByPerson(patient.ID, patient.FT.PreFixFeeDateTime.ToString()) == -1)
                {
                    this.Rollback();
                    this.Err = "更新患者上次收取费用时间时出错!";
                    return -1;
                }


                //发送消息
                #region HL7消息发送
                object curInterfaceImplement = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(FS.SOC.Local.InpatientFee.ShenZhen.BinHai.BizProcess.Integrate.Fee), typeof(FS.SOC.HISFC.BizProcess.MessagePatternInterface.IOrder));
                if (curInterfaceImplement is FS.SOC.HISFC.BizProcess.MessagePatternInterface.IOrder)
                {
                    FS.SOC.HISFC.BizProcess.MessagePatternInterface.IOrder curIOrderControl = curInterfaceImplement as FS.SOC.HISFC.BizProcess.MessagePatternInterface.IOrder;

                    int param = curIOrderControl.SendFeeInfo(patient, alFeeInfo, true);
                    if (param == -1)
                    {
                        this.Rollback();
                        this.Err = curIOrderControl.Err;
                        return -1;
                    }
                }

                #endregion

                this.Commit();
            }
            catch (Exception e)
            {
                this.Rollback();
                this.Err = "姓名为:" + patient.PVisit.Name + "住院流水号为:" +
                    patient.ID + "收取固定费用失败!" + e.Message;
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 求患者费用比例
        /// </summary>
        /// <param name="PactID">合同单位代码</param>
        /// <param name="item">药品费药品信息</param>
        /// <returns>失败null；成功 FS.HISFC.Models.Fee.FtRate</returns>
        private FS.HISFC.Models.Base.FTRate ComputeFeeRate(string PactID, FS.HISFC.Models.Base.Item item)
        {
            FS.HISFC.BizLogic.Fee.PactUnitItemRate PactItemRate = new FS.HISFC.BizLogic.Fee.PactUnitItemRate();

            PactItemRate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

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

                        PactManagment.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);


                        FS.HISFC.Models.Base.PactInfo PactUnitInfo = PactManagment.GetPactUnitInfoByPactCode(PactID);

                        if (PactUnitInfo == null)
                        {
                            this.Err = "获得合同单位信息出错，" + PactManagment.Err;
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
                            this.Err = "获得合同单位信息出错，" + PactManagment.Err;
                            return null;
                        }
                    }
                }
            }
            catch
            {
                this.Err = "获得合同单位信息出错";
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

        #endregion

    }
}
