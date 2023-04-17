using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.HISFC.Models.Base;
using System.Collections;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.HL7.OrderEntry
{
    public class Function
    {
        private static FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();

        /// <summary>
        ///  将医嘱转换为费用实体
        /// </summary>
        /// <param name="execOrder"></param>
        /// <returns></returns>
        public static FS.HISFC.Models.Fee.Inpatient.FeeItemList ChangeToFeeItemList(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.HISFC.Models.Order.ExecOrder execOrder, FS.HISFC.Models.Fee.Inpatient.FTSource ftSource,ref string errInfo)
        {
            FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList = new FS.HISFC.Models.Fee.Inpatient.FeeItemList();

            feeItemList.Item = execOrder.Order.Item.Clone();

            feeItemList.RecipeNO = execOrder.Order.ReciptNO;
            feeItemList.SequenceNO = execOrder.Order.SequenceNO;


            if (execOrder.Order.HerbalQty == 0)
            {
                execOrder.Order.HerbalQty = 1;
            }

            //{F5477FAB-9832-4234-AC7F-ED49654948B4}
            FS.HISFC.BizLogic.Fee.PactUnitInfo pactManager = new FS.HISFC.BizLogic.Fee.PactUnitInfo();
            decimal price = feeItemList.Item.Price;
            decimal orgPrice = 0;
            if (feeIntegrate.GetPriceForInpatient(patientInfo, feeItemList.Item, ref price, ref orgPrice) == -1)
            {
                errInfo = "获取项目" + feeItemList.Item.Name + "的价格失败，原因：" + feeIntegrate.Err;
                return null;
            }
            feeItemList.Item.Price = price;

            // {54B0C254-3897-4241-B3BD-17B19E204C8C}
            // 原始价格（本来应收价格，不考虑合同单位因素）
            feeItemList.Item.DefPrice = orgPrice;

            //录入界面已经将QTY 赋值
            feeItemList.Item.Qty = execOrder.Order.Qty;// *order.HerbalQty;
            //增加付数的赋值 {AE53ACB5-3684-42e8-BF28-88C2B4FF2360}
            feeItemList.Days = execOrder.Order.HerbalQty;

            feeItemList.Item.PriceUnit = execOrder.Order.Unit;//单位重新付
            feeItemList.RecipeOper.Dept = execOrder.Order.ReciptDept.Clone();//开方科室
            feeItemList.RecipeOper.ID = execOrder.Order.ReciptDoctor.ID;
            feeItemList.RecipeOper.Name = execOrder.Order.ReciptDoctor.Name;
            feeItemList.ExecOper = execOrder.Order.ExecOper.Clone();//执行人
            feeItemList.StockOper.Dept = execOrder.Order.StockDept.Clone();//扣库科室
            feeItemList.ExecOper.Dept = execOrder.Order.ExeDept;//执行科室

            if (feeItemList.Item.PackQty == 0)
            {
                feeItemList.Item.PackQty = 1;
            }
            feeItemList.FT.TotCost = FS.FrameWork.Public.String.FormatNumber((feeItemList.Item.Price * feeItemList.Item.Qty / feeItemList.Item.PackQty), 2);

            // 原始总费用（本来应收费用，不考虑合同单位因素）
            // {54B0C254-3897-4241-B3BD-17B19E204C8C}
            feeItemList.FT.DefTotCost = FS.FrameWork.Public.String.FormatNumber((feeItemList.Item.DefPrice * feeItemList.Item.Qty / feeItemList.Item.PackQty), 2);

            feeItemList.FT.OwnCost = feeItemList.FT.TotCost;
            feeItemList.IsBaby = execOrder.Order.IsBaby;
            feeItemList.IsEmergency = execOrder.Order.IsEmergency;
            feeItemList.Order = execOrder.Order.Clone();
            feeItemList.ExecOrder = execOrder.Clone();
            feeItemList.NoBackQty = feeItemList.Item.Qty;
            feeItemList.FTRate.OwnRate = 1;
            feeItemList.BalanceState = "0";
            feeItemList.ChargeOper = execOrder.ChargeOper.Clone();//计费人
            feeItemList.FeeOper = execOrder.ChargeOper.Clone();//计费人
            //数量大于零 则为正交易 数量小于零 则为负交易
            feeItemList.TransType = feeItemList.Item.Qty > 0 ? TransTypes.Positive : TransTypes.Negative;

            //费用数据来源
            feeItemList.FTSource = ftSource.Clone();

            feeItemList.UndrugComb.ID = execOrder.Order.Package.ID;
            feeItemList.UndrugComb.Name = execOrder.Order.Package.Name;

            return feeItemList;
        }

        /// <summary>
        /// 门诊收费函数
        /// </summary>
        /// <param name="regInfo"></param>
        /// <param name="alOrderTemp"></param>
        /// <returns></returns>
        public static int ClinicFee(FS.HISFC.Models.Registration.Register regInfo, ArrayList alOrderTemp, ArrayList alDcOrder,string systemCode, ref string errInfo)
        {
            if (alOrderTemp == null || alDcOrder == null)
            {
                return 1;
            }

            ArrayList alOrder = new ArrayList();
            ArrayList alFeeItemList = new ArrayList();
            FS.HISFC.Models.Order.OutPatient.Order tempOrder = null;
            FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee(); 
            FS.HISFC.BizProcess.Integrate.Pharmacy phaIntegrate = new FS.HISFC.BizProcess.Integrate.Pharmacy();
             FS.HISFC.BizLogic.Order.OutPatient.Order outOrderMgr = new FS.HISFC.BizLogic.Order.OutPatient.Order();
             ItemCodeMapManager mapMgr = new ItemCodeMapManager();

            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            feeIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            outOrderMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            mapMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            #region 先作废医嘱
            foreach (FS.HISFC.Models.Order.OutPatient.Order outOrder in alDcOrder)
            {
                //删除医嘱
                //需要先查询医嘱
                FS.HISFC.Models.Order.OutPatient.Order orderTemp = null;
                if (outOrder.OrderType == FS.HISFC.Models.Base.EnumSysClass.UT.ToString())
                {
                    continue;
                }
                else
                {
                    //先查询对应的医嘱流水号号
                    FS.FrameWork.Models.NeuObject obj = mapMgr.GetHISCode(EnumItemCodeMap.OutpatientOrder, new FS.FrameWork.Models.NeuObject(outOrder.OrderType, outOrder.ApplyNo, ""), systemCode);
                    if (obj == null)
                    {
                        //找不到不报错了
                        continue;
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        errInfo = "查找医嘱类型为：" + outOrder.OrderType + "申请单号为：" + outOrder.ApplyNo + "的医嘱流水号失败，" + mapMgr.Err;
                        return -1;
                    }

                    orderTemp = outOrderMgr.QueryOneOrder(outOrder.Patient.ID, obj.ID);
                    if (orderTemp == null)
                    {
                        //找不到不报错了
                        continue;
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        errInfo = "查找医嘱类型为：" + outOrder.OrderType + "申请单号为：" + outOrder.ApplyNo + "的医嘱失败，" + outOrderMgr.Err;
                        return -1;
                    }

                    if (string.IsNullOrEmpty(orderTemp.ID))
                    {
                        errInfo = "查找医嘱类型为：" + outOrder.OrderType + "申请单号为：" + outOrder.ApplyNo + "的医嘱失败";
                        continue;
                    }
                }

                if (orderTemp != null)
                {
                    if (orderTemp.Status != 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        errInfo = outOrder.Item.Name + "可能已经收费，不允许删除医嘱" + orderTemp.ApplyNo;
                        return -1;
                    }

                    int i = outOrderMgr.DeleteOrder(orderTemp.SeeNO, FS.FrameWork.Function.NConvert.ToInt32(orderTemp.ID));
                    if ((outOrder.Status == 0 && i < 0) || (outOrder.Status == 3 && i <= 0))
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        errInfo = outOrder.Item.Name + "可能已经收费，不允许删除医嘱，" + outOrderMgr.Err;
                        return -1;
                    }

                    //删除费用
                    if (feeIntegrate.DeleteFeeItemListByMoOrder(orderTemp.ID) < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        errInfo = outOrder.Item.Name + "可能已经收费，不允许删除医嘱" + feeIntegrate.Err;
                        return -1;
                    }

                    //删除对照信息
                    if (mapMgr.Delete(EnumItemCodeMap.OutpatientOrder, new FS.FrameWork.Models.NeuObject(outOrder.OrderType, outOrder.ApplyNo, ""),systemCode) < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        errInfo = "申请单类型：" + outOrder.OrderType + "申请单号：" + outOrder.ApplyNo + "删除对照表信息失败，" + mapMgr.Err;
                        return -1;
                    }

                    //删除发药申请
                    phaIntegrate.DelApplyOut(orderTemp.ReciptNO, orderTemp.SequenceNO.ToString());
                    //删除调剂表
                    phaIntegrate.DeleteDrugStoRecipe(orderTemp.ReciptNO, "ALL");
                }

            }
            #endregion

            #region 再插入医嘱
            foreach (FS.HISFC.Models.Order.OutPatient.Order outOrder in alOrderTemp)
            {
                FS.HISFC.Models.Fee.Outpatient.FeeItemList itemListObj = ChangeToFeeItemList(outOrder, ref errInfo);
                if (itemListObj == null)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    alFeeItemList = null;
                    return -1;
                }
                alFeeItemList.Add(itemListObj);
                alOrder.Add(outOrder);
            }
            #endregion


            if (alFeeItemList.Count > 0)
            {
                if (!feeIntegrate.SetChargeInfo(regInfo, alFeeItemList, outOrderMgr.GetDateTimeFromSysDateTime(), ref errInfo))
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    return -1;
                }

                #region 回馈处方号和处方流水号

                for (int k = 0; k < alOrder.Count; k++)
                {
                    tempOrder = alOrder[k] as FS.HISFC.Models.Order.OutPatient.Order;

                    foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList feeitem in alFeeItemList)
                    {
                        if (tempOrder.ID == feeitem.Order.ID)
                        {
                            tempOrder.ReciptNO = feeitem.RecipeNO;
                            tempOrder.SequenceNO = feeitem.SequenceNO;
                            tempOrder.ReciptSequence = feeitem.RecipeSequence;

                            break;
                        }
                    }
                }
                #endregion

                #region 保存医嘱 插入或更新处方表
                for (int j = 0; j < alOrder.Count; j++)
                {
                    tempOrder = alOrder[j] as FS.HISFC.Models.Order.OutPatient.Order;

                    if (tempOrder == null)
                    {
                        continue;
                    }

                    //插入对照表
                    string[] str = tempOrder.ApplyNo.Split('|');
                    string recipeNo = str[0].ToString();
                    string recipeSeq = string.Empty;
                    if (str.Length > 1)
                    {
                        recipeSeq = str[1].ToString();
                    }
                    if (mapMgr.InsertSeparate(EnumItemCodeMap.OutpatientOrder, new FS.FrameWork.Models.NeuObject(tempOrder.ID, tempOrder.ID, ""), new FS.FrameWork.Models.NeuObject(tempOrder.OrderType, tempOrder.ApplyNo, ""), new FS.FrameWork.Models.NeuObject(recipeNo, recipeSeq, "")) < 0)
                    {

                        FS.FrameWork.Management.PublicTrans.RollBack(); ;
                        errInfo = "插入HL7对照表失败！" + mapMgr.Err;
                        return -1;
                    }

                    if (outOrderMgr.UpdateOrder(tempOrder) == -1) //保存医嘱档
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack(); ;
                        errInfo = "插入医嘱失败！" + outOrderMgr.Err;
                        return -1;
                    }
                }
                #endregion
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            return 1;

        }

        /// <summary>
        /// 将医嘱实体转成费用实体
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        private static FS.HISFC.Models.Fee.Outpatient.FeeItemList ChangeToFeeItemList(FS.HISFC.Models.Order.OutPatient.Order order,ref string errInfo)
        {
            FS.HISFC.Models.Fee.Outpatient.FeeItemList feeitemlist = new FS.HISFC.Models.Fee.Outpatient.FeeItemList();

            if (order.Item.ItemType == EnumItemType.Drug)
            {
                feeitemlist.Item = new FS.HISFC.Models.Pharmacy.Item();

            }
            else
            {
                feeitemlist.Item = new FS.HISFC.Models.Fee.Item.Undrug();
            }

            feeitemlist.Item.Qty = order.Qty;
            feeitemlist.CancelType = FS.HISFC.Models.Base.CancelTypes.Valid;
            feeitemlist.Patient.ID = order.Patient.ID;//门诊流水号
            feeitemlist.Patient.PID.CardNO = order.Patient.PID.CardNO;//门诊卡号 
            feeitemlist.Order.ID = order.ID;//医嘱流水号
            feeitemlist.Order.SortID = order.SortID;
            feeitemlist.Order.SubCombNO = order.SubCombNO;
            feeitemlist.Order.Sample.ID = order.Sample.ID;//检查用来存储申请单类型（发送系统号）

            feeitemlist.ChargeOper.ID = FS.FrameWork.Management.Connection.Operator.ID;
            feeitemlist.Order.CheckPartRecord = order.CheckPartRecord;//检体 
            feeitemlist.Order.Combo.ID = order.Combo.ID;//组合号
            if (order.Unit == "[复合项]")
            {
                feeitemlist.IsGroup = true;
                feeitemlist.UndrugComb.ID = order.User01;
                feeitemlist.UndrugComb.Name = order.User02;
            }

            if (order.Item.ItemType == EnumItemType.Drug && !((FS.HISFC.Models.Pharmacy.Item)order.Item).IsSubtbl)
            {
                feeitemlist.ExecOper.Dept.ID = order.StockDept.Clone().ID;//传扣库科室
                feeitemlist.ExecOper.Dept.Name = order.StockDept.Clone().Name;
            }
            else
            {
                feeitemlist.ExecOper.Dept.ID = order.ExeDept.Clone().ID;
                feeitemlist.ExecOper.Dept.Name = order.ExeDept.Clone().Name;
            }
            feeitemlist.InjectCount = order.InjectCount;//院内次数

            if (order.Item.PackQty <= 0)
            {
                order.Item.PackQty = 1;
            }

            if (order.Item.ItemType == EnumItemType.Drug)
            {
                feeitemlist.Item.SpecialFlag = order.Item.SpecialFlag;

                if (order.NurseStation.User03 == "")//user03为空,说明不知道开立的什么单位 默认为最小单位
                {
                    order.NurseStation.User03 = "1";//默认
                }
                if (order.NurseStation.User03 != "1")//开立最小单位 !=((FS.HISFC.Models.Pharmacy.Item)order.Item).MinUnit)
                {
                    feeitemlist.Item.Qty = order.Item.PackQty * order.Qty;
                    order.FT.OwnCost = order.Qty * order.Item.Price;

                    order.Item.PriceUnit = order.Unit;
                    feeitemlist.FeePack = "1";//开立单位 1:包装单位 0:最小单位
                    order.MinunitFlag = "0";
                }
                else
                {
                    if (order.Item.PackQty == 0)
                    {
                        order.Item.PackQty = 1;
                    }
                    order.FT.OwnCost = order.Qty * order.Item.Price / order.Item.PackQty;

                    order.Item.PriceUnit = order.Unit;
                    feeitemlist.FeePack = "0";//开立单位 1:包装单位 0:最小单位
                    order.MinunitFlag = "1";
                }
            }
            else
            {
                order.FT.OwnCost = order.Qty * order.Item.Price;
                feeitemlist.FeePack = "1";
                order.MinunitFlag = "0";
            }

            if (order.HerbalQty == 0)
            {
                order.HerbalQty = 1;
            }

            feeitemlist.Days = order.HerbalQty;//草药付数
            feeitemlist.RecipeOper.Dept = order.ReciptDept;//开方科室信息
            feeitemlist.RecipeOper.Name = order.ReciptDoctor.Name;//开方医生信息
            feeitemlist.RecipeOper.ID = order.ReciptDoctor.ID;
            feeitemlist.Order.DoseUnit = order.DoseUnit;//用量单位
            //if (order.Item.IsPharmacy)
            if (order.Item.ItemType == EnumItemType.Drug)
            {
                if (((FS.HISFC.Models.Pharmacy.Item)order.Item).SysClass.ID.ToString() == "PCC")
                {
                    if (order.HerbalQty == 0)
                    {
                        order.HerbalQty = 1;
                    }

                    feeitemlist.Order.DoseOnce = order.DoseOnce;

                }
                else
                {
                    feeitemlist.Order.DoseOnce = order.DoseOnce;//每次用量
                }
            }
            feeitemlist.Order.Frequency = order.Frequency;//频次信息

            feeitemlist.Order.Combo.IsMainDrug = order.Combo.IsMainDrug;//是否主药
            feeitemlist.Item.ID = order.Item.ID;
            feeitemlist.Item.Name = order.Item.Name;
            //if (order.Item.IsPharmacy)//是否药品
            if (order.Item.ItemType == EnumItemType.Drug)//是否药品
            {
                ((FS.HISFC.Models.Pharmacy.Item)feeitemlist.Item).BaseDose = ((FS.HISFC.Models.Pharmacy.Item)order.Item).BaseDose;//基本计量
                ((FS.HISFC.Models.Pharmacy.Item)feeitemlist.Item).Quality = ((FS.HISFC.Models.Pharmacy.Item)order.Item).Quality;//药品性质
                ((FS.HISFC.Models.Pharmacy.Item)feeitemlist.Item).DosageForm = ((FS.HISFC.Models.Pharmacy.Item)order.Item).DosageForm;//剂型

                feeitemlist.IsConfirmed = false;//是否终端确认
                feeitemlist.Item.PackQty = order.Item.PackQty;//包装数量
            }
            else
            {
                if (order.ReTidyInfo != "SUBTBL")
                {
                    feeitemlist.IsConfirmed = false;
                    feeitemlist.Item.PackQty = 1;//包装数量
                }
                else//附材中的复合项目
                {
                    feeitemlist.IsConfirmed = false;//FS.neHISFC.Components.Function.NConvert.ToBoolean(order.Mark2);
                    feeitemlist.Item.PackQty = 1;
                }
            }

            //feeitemlist.Order.Item.IsPharmacy = order.Item.IsPharmacy;//是否药品
            feeitemlist.Order.Item.ItemType = order.Item.ItemType;//是否药品
            //if (order.Item.IsPharmacy)//药品
            if (order.Item.ItemType == EnumItemType.Drug)//药品
            {
                feeitemlist.Item.Specs = order.Item.Specs;
            }
            feeitemlist.IsUrgent = order.IsEmergency;//是否加急
            feeitemlist.Order.Sample = order.Sample;//样本信息
            feeitemlist.Memo = order.Memo;//备注
            feeitemlist.Item.MinFee = order.Item.MinFee;//最小费用
            feeitemlist.PayType = FS.HISFC.Models.Base.PayTypes.Charged;//划价状态
            feeitemlist.Item.Price = order.Item.Price;//价格

            feeitemlist.Item.PriceUnit = order.Item.PriceUnit;//价格单位
            if (order.Item.SysClass.ID.ToString() == "PCC" && order.HerbalQty > 0)
            {

            }
            order.FT.TotCost = order.FT.TotCost;
            order.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(order.FT.TotCost, 2);
            order.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(order.FT.TotCost, 2);
            feeitemlist.FT = Round(order, 2, ref errInfo);//取两位

            if (feeitemlist.FT == null)
            {
                return null;
            }

            //{B9303CFE-755D-4585-B5EE-8C1901F79450} 用药品超标金额保存原来的总费用
            if (feeitemlist.Item.ItemType == EnumItemType.Drug)
            {
                if (order.NurseStation.User03 != "1")//开立最小单位
                {
                    feeitemlist.FT.ExcessCost = order.Qty * ((FS.HISFC.Models.Pharmacy.Item)order.Item).ChildPrice;
                }
                else
                {
                    feeitemlist.FT.ExcessCost = order.Qty * ((FS.HISFC.Models.Pharmacy.Item)order.Item).ChildPrice / order.Item.PackQty;
                    feeitemlist.FT.ExcessCost = FS.FrameWork.Public.String.FormatNumber(feeitemlist.FT.ExcessCost, 2);
                }
            }
            ((FS.HISFC.Models.Registration.Register)feeitemlist.Patient).DoctorInfo.SeeDate = order.RegTime;//登记日期
            ((FS.HISFC.Models.Registration.Register)feeitemlist.Patient).DoctorInfo.Templet.Dept = order.ReciptDept;//登记科室
            feeitemlist.Item.SysClass = order.Item.SysClass;//系统类别
            feeitemlist.TransType = FS.HISFC.Models.Base.TransTypes.Positive;//交易类型
            feeitemlist.Order.Usage = order.Usage;//用法
            feeitemlist.RecipeSequence = order.ReciptSequence;//收费序列
            feeitemlist.RecipeNO = order.ReciptNO;//处方号
            feeitemlist.SequenceNO = order.SequenceNO;//处方流水号

            if (feeitemlist.Item.IsMaterial)
            {
                //附材的费用来源为收费
                feeitemlist.FTSource = "0";
            }
            else
            {
                feeitemlist.FTSource = "1";//来自医嘱
            }

            if (order.IsSubtbl)
            {
                feeitemlist.Item.IsMaterial = true;//是附材
            }

            //{6FC43DF1-86E1-4720-BA3F-356C25C74F16}
            feeitemlist.Item.IsNeedConfirm = order.Item.IsNeedConfirm;
            feeitemlist.Order.ApplyNo = order.ApplyNo;
            feeitemlist.Order.OrderType = order.OrderType;

            //打包收费标记 
            if (!FS.FrameWork.Function.NConvert.ToBoolean(order.ChargeOper.Memo))
            {
                feeitemlist.ItemRateFlag = "3";
            }
            return feeitemlist;
        }

        /// <summary>
        /// 为费用取整
        /// </summary>
        /// <param name="order"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        private static FS.HISFC.Models.Base.FT Round(FS.HISFC.Models.Order.OutPatient.Order order, int i,ref string errInfo)
        {
            try
            {
                FS.HISFC.Models.Base.FT ft = new FS.HISFC.Models.Base.FT();
                //为NULL返回新实体
                if (order == null || order.FT == null)
                {
                    return ft;
                }

                ft.AdjustOvertopCost = FS.FrameWork.Public.String.FormatNumber(order.FT.AdjustOvertopCost, i);
                ft.AirLimitCost = FS.FrameWork.Public.String.FormatNumber(order.FT.AirLimitCost, i);
                ft.BalancedCost = FS.FrameWork.Public.String.FormatNumber(order.FT.BalancedCost, i);
                ft.BalancedPrepayCost = FS.FrameWork.Public.String.FormatNumber(order.FT.BalancedPrepayCost, i);
                ft.BedLimitCost = FS.FrameWork.Public.String.FormatNumber(order.FT.BedLimitCost, i);
                ft.BedOverDeal = order.FT.BedOverDeal;
                ft.BloodLateFeeCost = FS.FrameWork.Public.String.FormatNumber(order.FT.BloodLateFeeCost, i);
                ft.BoardCost = FS.FrameWork.Public.String.FormatNumber(order.FT.BoardCost, i);
                ft.BoardPrepayCost = FS.FrameWork.Public.String.FormatNumber(order.FT.BoardPrepayCost, i);
                ft.DrugFeeTotCost = FS.FrameWork.Public.String.FormatNumber(order.FT.DrugFeeTotCost, i);
                ft.TransferPrepayCost = FS.FrameWork.Public.String.FormatNumber(order.FT.TransferPrepayCost, i);
                ft.TransferTotCost = FS.FrameWork.Public.String.FormatNumber(order.FT.TransferTotCost, i);
                ft.DayLimitCost = FS.FrameWork.Public.String.FormatNumber(order.FT.DayLimitCost, i);
                ft.DerateCost = FS.FrameWork.Public.String.FormatNumber(order.FT.DerateCost, i);
                ft.FixFeeInterval = order.FT.FixFeeInterval;
                ft.ID = order.FT.ID;
                ft.LeftCost = FS.FrameWork.Public.String.FormatNumber(order.FT.LeftCost, i);
                ft.OvertopCost = FS.FrameWork.Public.String.FormatNumber(order.FT.OvertopCost, i);
                ft.DayLimitTotCost = FS.FrameWork.Public.String.FormatNumber(order.FT.DayLimitTotCost, i);
                ft.Memo = order.FT.Memo;
                ft.Name = order.FT.Name;
                ft.OwnCost = FS.FrameWork.Public.String.FormatNumber(order.FT.OwnCost, i);
                ft.FTRate.OwnRate = order.FT.FTRate.OwnRate;
                ft.PayCost = FS.FrameWork.Public.String.FormatNumber(order.FT.PayCost, i);
                ft.FTRate.PayRate = order.FT.FTRate.PayRate;
                ft.PreFixFeeDateTime = order.FT.PreFixFeeDateTime;
                ft.PrepayCost = FS.FrameWork.Public.String.FormatNumber(order.FT.PrepayCost, i);
                ft.PubCost = FS.FrameWork.Public.String.FormatNumber(order.FT.PubCost, i);
                ft.RebateCost = FS.FrameWork.Public.String.FormatNumber(order.FT.RebateCost, i);
                ft.ReturnCost = FS.FrameWork.Public.String.FormatNumber(order.FT.ReturnCost, i);
                ft.SupplyCost = FS.FrameWork.Public.String.FormatNumber(order.FT.SupplyCost, i);
                ft.TotCost = FS.FrameWork.Public.String.FormatNumber(order.FT.TotCost, i);

                ft.User01 = order.FT.User01;
                ft.User02 = order.FT.User02;
                ft.User03 = order.FT.User03;
                return ft;
            }
            catch (Exception ex)
            {
                errInfo = ex.Message;
                return null;
            }
        }
    }
}
