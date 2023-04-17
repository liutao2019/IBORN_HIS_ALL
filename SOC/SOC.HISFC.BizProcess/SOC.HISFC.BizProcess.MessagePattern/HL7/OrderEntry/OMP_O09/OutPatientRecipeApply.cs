using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.BizProcess.MessagePatternInterface;
using FS.HL7Message;
using FS.SOC.HISFC.BizProcess.CommonInterface;
using System.Collections;
using FS.HISFC.Models.Base;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.HL7.OrderEntry.OMP_O09
{
    public class OutPatientRecipeApply
    {
        private FS.HISFC.BizProcess.Integrate.Order orderIntegrate = new FS.HISFC.BizProcess.Integrate.Order();
        private FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();
        private FS.HISFC.BizLogic.Fee.Outpatient outFeeMgr = new FS.HISFC.BizLogic.Fee.Outpatient();
        private FS.HISFC.BizProcess.Integrate.Pharmacy phaIntegrate = new FS.HISFC.BizProcess.Integrate.Pharmacy();
        private FS.HISFC.BizProcess.Integrate.Registration.Registration regIntegrate = new FS.HISFC.BizProcess.Integrate.Registration.Registration();
        private FS.HISFC.BizProcess.Integrate.Manager inteMgr = new FS.HISFC.BizProcess.Integrate.Manager();
        private FS.HISFC.BizLogic.Order.OutPatient.Order outOrderMgr = new FS.HISFC.BizLogic.Order.OutPatient.Order();

        public int ProcessMessage(NHapi.Model.V24.Message.OMP_O09 omp009, ref NHapi.Base.Model.IMessage ackMessage, ref string errInfo)
        {
            if (omp009 == null || omp009.ORDERRepetitionsUsed <= 0)
            {
                return 1;
            }

            FS.HISFC.Models.Registration.Register regInfo = new FS.HISFC.Models.Registration.Register();
            string clinicCode = omp009.PATIENT.PATIENT_VISIT.PV1.VisitNumber.ID.Value;
            regInfo = regIntegrate.GetByClinic(clinicCode);
            if (regInfo == null || string.IsNullOrEmpty(regInfo.ID))
            {
                errInfo = "获取患者信息" + clinicCode + "失败!" + regIntegrate.Err;
                return -1;
            }
            if (regInfo.Status == FS.HISFC.Models.Base.EnumRegisterStatus.Back)
            {
                errInfo = "获取挂号信息失败，原因：挂号记录已退号" + regInfo.ID;
                return -1;
            }
            else if (regInfo.Status == FS.HISFC.Models.Base.EnumRegisterStatus.Cancel)
            {
                errInfo = "获取挂号信息失败，原因：挂号记录已作废" + regInfo.ID;
                return -1;
            }

            NHapi.Model.V24.Group.OMP_O09_ORDER ompOrder = null;
            FS.HISFC.Models.Pharmacy.Item phaItem = null;
            FS.HISFC.Models.Fee.Item.Undrug undrgObj = null;
            FS.HISFC.Models.Order.OutPatient.Order outOrder = null;
            ArrayList alOrder = new ArrayList();
            ArrayList alDcOrder = new ArrayList();
            string receipNO = this.outFeeMgr.GetRecipeNO();
            Hashtable hsRecipeNoAndSeq = new Hashtable();
            Dictionary<string, string> dicComboID = new Dictionary<string, string>();
            for (int i = 0; i < omp009.ORDERRepetitionsUsed; i++)
            {
                outOrder = new FS.HISFC.Models.Order.OutPatient.Order();
                outOrder.SeeNO = this.outOrderMgr.GetNewSeeNo(regInfo.PID.CardNO).ToString();
                outOrder.ID = this.orderIntegrate.GetNewOrderID();
                outOrder.Patient.ID = regInfo.ID;
                outOrder.Patient.PID = regInfo.PID;
                outOrder.RegTime = regInfo.DoctorInfo.SeeDate;
                outOrder.InDept = regInfo.DoctorInfo.Templet.Dept;

                ompOrder = omp009.GetORDER(i);

                if (ompOrder.ORC.OrderControl.Value != "CA")
                {

                    //药品
                    if (ompOrder.RXO.RequestedGiveCode.NameOfCodingSystem.Value == "d")
                    {
                        phaItem = new FS.HISFC.Models.Pharmacy.Item();

                        string itemCode = ompOrder.RXO.RequestedGiveCode.Identifier.Value;
                        phaItem = phaIntegrate.GetItem(itemCode);
                        if (phaItem == null)
                        {
                            errInfo = "接收处方信息失败，原因：传入的药品编码，系统中找不到" + itemCode;
                            return -1;
                        }

                        // phaItem.Name = ompOrder.RXO.RequestedGiveCode.Text.Value;
                        outOrder.Item = phaItem;
                        outOrder.Item.Price = phaItem.Price;

                        outOrder.Item.ItemType = EnumItemType.Drug;
                        outOrder.Qty = FS.FrameWork.Function.NConvert.ToDecimal(ompOrder.RXO.RequestedGiveAmountMinimum.Value);//用量
                        outOrder.Unit = ompOrder.RXO.RequestedGiveUnits.Text.Value;//用量单位

                        if (phaItem.SysClass.ID.ToString() == "PCC")
                        {
                            outOrder.HerbalQty = FS.FrameWork.Function.NConvert.ToDecimal(ompOrder.RXO.NumberOfRefills.Value);
                        }
                        else
                        {
                            outOrder.HerbalQty = FS.FrameWork.Function.NConvert.ToDecimal(ompOrder.ORC.GetQuantityTiming(0).OccurrenceDuration.Identifier.Value);
                        }

                        outOrder.FT.PubCost = 0;
                        outOrder.FT.PayCost = 0;
                        outOrder.FT.OwnCost = outOrder.Qty * outOrder.Item.Price / outOrder.Item.PackQty;

                        outOrder.DoseOnce = FS.FrameWork.Function.NConvert.ToDecimal(ompOrder.ORC.GetQuantityTiming(0).Quantity.Quantity.Value);
                        outOrder.DoseUnit = ompOrder.ORC.GetQuantityTiming(0).Quantity.Units.Identifier.Value;
                        if (ompOrder.ORC.GetQuantityTiming(0).Interval.ExplicitTimeInterval.Value != null)
                        {
                            outOrder.Frequency.ID = ompOrder.ORC.GetQuantityTiming(0).Interval.ExplicitTimeInterval.Value;
                            outOrder.Frequency.Name = CommonController.CreateInstance().GetFrequencyName(outOrder.Frequency.ID);
                        }
                        else
                        {
                            errInfo = outOrder.Item.ID +"药品需要有频次" ;
                            return -1;
                        
                        }

                        outOrder.Usage.ID = ompOrder.GetRXR(0).Route.Identifier.Value;
                        outOrder.Usage.Name = CommonController.CreateInstance().GetConstantName(EnumConstant.USAGE, outOrder.Usage.ID);
                        //outOrder.Usage.Memo;//用法英文缩写
                        outOrder.Item.PriceUnit = outOrder.Unit;

                        outOrder.HypoTest = (FS.HISFC.Models.Order.EnumHypoTest)Enum.ToObject(typeof(FS.HISFC.Models.Order.EnumHypoTest), ompOrder.RXO.GetIndication(0).Identifier.Value == "Y" ? 2 : 0);
                        outOrder.InjectCount = FS.FrameWork.Function.NConvert.ToInt32(ompOrder.ORC.GetQuantityTiming(0).TotalOccurences.Value);
                        outOrder.NurseStation.User03 = outOrder.Item.PriceUnit == phaItem.MinUnit ? "1" : "0";//开立单位是否是最小单位 1 是 0 不是
                        outOrder.OrderType = FS.HISFC.Models.Base.EnumSysClass.P.ToString();

                        //库房编码
                        string stockDept = ompOrder.RXO.DeliverToLocation.Facility.UniversalID.Value;
                        outOrder.StockDept = CommonController.CreateInstance().GetDepartment(stockDept);
                        if (outOrder.StockDept == null)
                        {
                            errInfo = "接收处方信息失败，原因：传入的库存编码，系统中找不到" + stockDept;
                            return -1;
                        }
                    }
                    //非药品
                    else if (ompOrder.RXO.RequestedGiveCode.NameOfCodingSystem.Value == "n")
                    {
                        undrgObj = new FS.HISFC.Models.Fee.Item.Undrug();
                        string itemCode = ompOrder.RXO.RequestedGiveCode.Identifier.Value;
                        undrgObj = feeIntegrate.GetItem(itemCode);
                        if (undrgObj == null)
                        {
                            errInfo = "接收处方信息失败，原因：传入的项目编码，系统中找不到" + itemCode;
                            return -1;
                        }

                        //undrgObj.Name = ompOrder.RXO.RequestedGiveCode.Text.Value;
                        outOrder.Item = undrgObj;
                        outOrder.Item.ItemType = EnumItemType.UnDrug;
                        outOrder.Qty = FS.FrameWork.Function.NConvert.ToDecimal(ompOrder.RXO.RequestedGiveAmountMinimum.Value);//用量
                        outOrder.Unit = ompOrder.RXO.RequestedGiveUnits.Text.Value;//用量单位
                        outOrder.HerbalQty = FS.FrameWork.Function.NConvert.ToDecimal(ompOrder.ORC.GetQuantityTiming(0).OccurrenceDuration.Identifier.Value);//付数

                        outOrder.FT.PubCost = 0;
                        outOrder.FT.PayCost = 0;
                        outOrder.FT.OwnCost = outOrder.Qty * outOrder.Item.Price;

                        //频次
                        if (ompOrder.ORC.GetQuantityTiming(0).Interval.ExplicitTimeInterval.Value != null)
                        {
                            outOrder.Frequency.ID = ompOrder.ORC.GetQuantityTiming(0).Interval.ExplicitTimeInterval.Value;
                            outOrder.Frequency.Name = CommonController.CreateInstance().GetFrequencyName(outOrder.Frequency.ID);
                        }

                        outOrder.Usage.ID = ompOrder.GetRXR(0).Route.Identifier.Value;
                        outOrder.Usage.Name = CommonController.CreateInstance().GetConstantName(EnumConstant.USAGE, outOrder.Usage.ID);

                        //取默认的执行科室
                        if (undrgObj.ExecDepts.Count > 0)
                        {
                            outOrder.ExeDept.ID = ((FS.FrameWork.Models.NeuObject)undrgObj.ExecDepts[0]).ID;
                        }

                        outOrder.InjectCount = 0;
                        outOrder.OrderType = FS.HISFC.Models.Base.EnumSysClass.U.ToString();
                    }
                    else
                    {
                        errInfo = "接收处方信息失败，原因：未知的项目类型" + ompOrder.RXO.RequestedGiveCode.NameOfCodingSystem.Value;
                        return -1;
                    }

                    //执行科室
                    if (string.IsNullOrEmpty(outOrder.ExeDept.ID) || CommonController.CreateInstance().GetDepartment(outOrder.ExeDept.ID) == null)
                    {
                        //此处暂时用开立科室作为执行科室
                        outOrder.ExeDept.ID = ompOrder.ORC.EnteringOrganization.Identifier.Value;
                    }
                    string execDept = outOrder.ExeDept.ID;
                    outOrder.ExeDept = CommonController.CreateInstance().GetDepartment(outOrder.ExeDept.ID);
                    if (outOrder.ExeDept == null)
                    {
                        execDept = "0000";
                        outOrder.ExeDept = CommonController.CreateInstance().GetDepartment(execDept);

                        errInfo = "接收处方信息失败，原因：传入的执行科室编码，系统中找不到" + execDept;
                        //return -1;
                    }
                }
                else
                {
                    //药品
                    if (ompOrder.RXO.RequestedGiveCode.NameOfCodingSystem.Value == "d")
                    {
                        outOrder.OrderType = FS.HISFC.Models.Base.EnumSysClass.P.ToString();
                    }
                    else
                    {
                        outOrder.OrderType = FS.HISFC.Models.Base.EnumSysClass.U.ToString();
                    }
                }
                //没有备注 暂时不处理
                outOrder.Memo = "";
                //开方医生
                outOrder.ReciptDoctor.ID = ompOrder.ORC.GetOrderingProvider(0).IDNumber.Value;
                outOrder.ReciptDoctor = CommonController.CreateInstance().GetEmployee(outOrder.ReciptDoctor.ID);

                //开方科室
                outOrder.ReciptDept.ID = ompOrder.ORC.EnteringOrganization.Identifier.Value;
                outOrder.MOTime = DateTime.ParseExact(ompOrder.ORC.DateTimeOfTransaction.TimeOfAnEvent.Value, "yyyyMMddHHmmss", null);
                outOrder.Status = 0;

                //打包收费状态 0 是 1 否
                outOrder.ChargeOper.Memo = ompOrder.ORC.OrderStatus.Value;
                if (string.IsNullOrEmpty(outOrder.ChargeOper.Memo))
                {
                    outOrder.ChargeOper.Memo = "1";
                }

                //S 最高优先级 A较高优先级 R常规
                outOrder.IsEmergency = (ompOrder.ORC.GetQuantityTiming(0).Priority.Value == "S" || ompOrder.ORC.GetQuantityTiming(0).Priority.Value == "A");
                outOrder.IsHaveCharged = false;
                outOrder.ReciptNO = receipNO;// ompOrder.ORC.PlacerGroupNumber.EntityIdentifier.Value;
                if (hsRecipeNoAndSeq.Contains(outOrder.ReciptNO))
                {
                    outOrder.SequenceNO = FS.FrameWork.Function.NConvert.ToInt32(hsRecipeNoAndSeq[outOrder.ReciptNO]) + 1;
                    hsRecipeNoAndSeq[outOrder.ReciptNO] = outOrder.SequenceNO;
                }
                else
                {
                    string seq = outFeeMgr.GetMaxSeqByRecipeNO(outOrder.ReciptNO, regInfo.ID);
                    if (string.IsNullOrEmpty(seq))
                    {
                        outOrder.SequenceNO = 1;
                    }
                    else
                    {
                        outOrder.SequenceNO = FS.FrameWork.Function.NConvert.ToInt32(seq) + 1;
                    }
                    hsRecipeNoAndSeq.Add(outOrder.ReciptNO, 1);
                }

                //用ApplyNO保存原始的处方号和处方内流水号，方便收费时回复确认消息
                outOrder.ApplyNo = ompOrder.ORC.PlacerGroupNumber.EntityIdentifier.Value + "|" + ompOrder.ORC.PlacerOrderNumber.EntityIdentifier.Value;

                //组合号
                string parentID = ompOrder.ORC.Parent.ParentSPlacerOrderNumber.EntityIdentifier.Value;
                if (string.IsNullOrEmpty(parentID))
                {
                    parentID = ompOrder.ORC.PlacerOrderNumber.EntityIdentifier.Value;
                }
                //如果为空
                if (dicComboID.ContainsKey(parentID) == false)
                {
                    string combNo = this.outOrderMgr.GetNewOrderComboID();
                    outOrder.Combo.ID = combNo;
                    dicComboID.Add(parentID, combNo);
                }
                else
                {
                    outOrder.Combo.ID = dicComboID[parentID];
                }

                if (ompOrder.ORC.OrderControl.Value == "CA")
                {
                    outOrder.DCOper.ID = ompOrder.ORC.GetEnteredBy(0).IDNumber.Value;
                    outOrder.DCOper.OperTime = DateTime.ParseExact(ompOrder.ORC.DateTimeOfTransaction.TimeOfAnEvent.Value, "yyyyMMddHHmmss", null);
                    outOrder.Status = 3;
                    alDcOrder.Add(outOrder);
                }
                else if (ompOrder.ORC.OrderControl.Value == "RU")
                {
                    FS.HISFC.Models.Order.OutPatient.Order tempOrder = outOrder.Clone();
                    outOrder.DCOper.ID = ompOrder.ORC.GetEnteredBy(0).IDNumber.Value;
                    outOrder.DCOper.OperTime = DateTime.ParseExact(ompOrder.ORC.DateTimeOfTransaction.TimeOfAnEvent.Value, "yyyyMMddHHmmss", null);
                    outOrder.Status = 0;
                    tempOrder.Status = 0;
                    alDcOrder.Add(outOrder);
                    alOrder.Add(tempOrder);
                }
                else if (ompOrder.ORC.OrderControl.Value == "NW")
                {
                    outOrder.Status = 0;
                    alOrder.Add(outOrder);
                }
                else
                {
                    errInfo = "未知的医嘱控制ID：" + ompOrder.ORC.OrderControl.Value;
                    return -1;
                }

            }

            return Function.ClinicFee(regInfo, alOrder, alDcOrder,omp009.MSH.SendingApplication.NamespaceID.Value, ref errInfo);
        }

    }
}
