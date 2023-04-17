using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.BizProcess.MessagePatternInterface;
using System.Collections;
using FS.SOC.HISFC.BizProcess.CommonInterface;
using FS.HL7Message;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.HL7.OrderEntry.ORM_O01
{
    public class OutpatientOperationApply 
    {
        private FS.HISFC.BizProcess.Integrate.Registration.Registration regIntegrate = new FS.HISFC.BizProcess.Integrate.Registration.Registration();
        private FS.HISFC.BizLogic.Order.OutPatient.Order outOrderMgr = new FS.HISFC.BizLogic.Order.OutPatient.Order();
        private FS.HISFC.BizLogic.Fee.Outpatient outFeeMgr = new FS.HISFC.BizLogic.Fee.Outpatient();
        private FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();
        private FS.HISFC.BizProcess.Integrate.Order orderIntegrate = new FS.HISFC.BizProcess.Integrate.Order();

        public int ProcessMessage(NHapi.Model.V24.Message.ORM_O01 ormO01, ref NHapi.Base.Model.IMessage ackMessage, ref string errInfo)
        {
            if (ormO01 == null || ormO01.ORDERRepetitionsUsed <= 0)
            {
                return 1;
            }

            FS.HISFC.Models.Registration.Register regInfo = new FS.HISFC.Models.Registration.Register();
            string clinicCode = ormO01.PATIENT.PATIENT_VISIT.PV1.VisitNumber.ID.Value;
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
            NHapi.Model.V24.Group.ORM_O01_ORDER ormOrder = null;
            FS.HISFC.Models.Fee.Item.Undrug undrgObj = null;
            FS.HISFC.Models.Order.OutPatient.Order outOrder = null;
            Hashtable hsRecipeNoAndSeq = new Hashtable();

            Dictionary<string, string> applyNO = new Dictionary<string, string>();
            string receipNO = this.outFeeMgr.GetRecipeNO();
            ArrayList alOrder = new ArrayList();
            ArrayList alDcOrder = new ArrayList();
            for (int i = 0; i < ormO01.ORDERRepetitionsUsed; i++)
            {
                outOrder = new FS.HISFC.Models.Order.OutPatient.Order();
                outOrder.SeeNO = this.outOrderMgr.GetNewSeeNo(regInfo.PID.CardNO).ToString();
                outOrder.ID = this.orderIntegrate.GetNewOrderID();
                outOrder.Patient.ID = regInfo.ID;
                outOrder.Patient.PID = regInfo.PID;
                outOrder.RegTime = regInfo.DoctorInfo.SeeDate;
                outOrder.InDept = regInfo.DoctorInfo.Templet.Dept;

                ormOrder = ormO01.GetORDER(i);
                if (ormOrder == null)
                {
                    errInfo= "获取消息内容失败!";
                    return -1;
                }

                outOrder.ApplyNo = ormOrder.ORC.PlacerOrderNumber.EntityIdentifier.Value;
               

                //非药品
                undrgObj = new FS.HISFC.Models.Fee.Item.Undrug();
                string itemCode = ((NHapi.Model.V24.Datatype.CE)ormOrder.ZOP.GetField(16, 0)).Identifier.Value;

                if (string.IsNullOrEmpty(itemCode))
                {
                    errInfo= "手术项目编码为空！";
                    return -1;
                }

                if (itemCode.Contains(OrderControl.FilterStr))//CIS暂时用#号分开。可能会变
                {
                    itemCode = itemCode.Split(OrderControl.FilterStr)[0];
                }

                undrgObj = feeIntegrate.GetItem(itemCode);
                if (undrgObj == null)
                {
                    errInfo= "获取项目ID为:" + itemCode + "的项目错误!" + feeIntegrate.Err;
                    return -1;
                }

                outOrder.Item = undrgObj;
                outOrder.Item.ItemType = FS.HISFC.Models.Base.EnumItemType.UnDrug;
                outOrder.Qty = 1;
                outOrder.HerbalQty = 1;
                if (string.IsNullOrEmpty(outOrder.Item.PriceUnit))
                {
                    outOrder.Item.PriceUnit = "无";
                }
                outOrder.FT.PubCost = 0;
                outOrder.FT.PayCost = 0;
                outOrder.FT.OwnCost = outOrder.Qty * outOrder.Item.Price;

                outOrder.ReciptDoctor.ID = ormOrder.ORC.GetOrderingProvider(0).IDNumber.Value;

                outOrder.ReciptDoctor = CommonController.CreateInstance().GetEmployee(outOrder.ReciptDoctor.ID);

                outOrder.ReciptDept.ID = ormOrder.ORC.EnteringOrganization.Identifier.Value;

                //此处暂时用开立科室作为执行科室
                outOrder.ExeDept.ID = ((NHapi.Model.V24.Datatype.PL)ormOrder.ZOP.GetField(2, 0)).PointOfCare.Value;
                if (string.IsNullOrEmpty(outOrder.ExeDept.ID) || CommonController.CreateInstance().GetDepartment(outOrder.ExeDept.ID) == null)
                {
                    //去默认执行科室
                    if (undrgObj.ExecDepts.Count > 0)
                    {
                        outOrder.ExeDept.ID = ((FS.FrameWork.Models.NeuObject)undrgObj.ExecDepts[0]).ID;
                    }
                    else
                    {
                        outOrder.ExeDept.ID = ormOrder.ORC.EnteringOrganization.Identifier.Value;
                    }
                }

                string execDept = outOrder.ExeDept.ID;
                outOrder.ExeDept = CommonController.CreateInstance().GetDepartment(outOrder.ExeDept.ID);
                if (outOrder.ExeDept == null)
                {
                    execDept = "0000";
                    outOrder.ExeDept = CommonController.CreateInstance().GetDepartment(outOrder.ExeDept.ID);

                    errInfo = "接收处方信息失败，原因：传入的执行科室编码，系统中找不到" + execDept;
                    //return -1;
                }

                //由于CIS一个消息里面包含多个申请单，所以对于申请单不同的需要重新取组合号
                if (applyNO.ContainsKey(outOrder.ApplyNo))
                {
                    outOrder.Combo.ID = applyNO[outOrder.ApplyNo];
                     receipNO = this.outFeeMgr.GetRecipeNO();
                }
                else
                {
                    string combNo = this.outOrderMgr.GetNewOrderComboID();
                    outOrder.Combo.ID = combNo;
                    applyNO[outOrder.ApplyNo] = combNo;
                }
                outOrder.ReciptNO = receipNO; //omlOrder.ORC.PlacerGroupNumber.EntityIdentifier.Value;
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



                //没有备注 暂时不处理
                outOrder.Memo = ((NHapi.Model.V24.Datatype.ST)ormOrder.ZOP.GetField(22, 0)).Value;
                outOrder.MOTime = DateTime.ParseExact(ormOrder.ORC.DateTimeOfTransaction.TimeOfAnEvent.Value, "yyyyMMddHHmmss", null);
                outOrder.Status = 0;

                if (ormOrder.ORC.OrderControl.Value == "CA")
                {
                    outOrder.DCOper.ID = ormOrder.ORC.GetEnteredBy(0).IDNumber.Value;
                    outOrder.DCOper.OperTime = DateTime.ParseExact(ormOrder.ORC.DateTimeOfTransaction.TimeOfAnEvent.Value, "yyyyMMddHHmmss", null);
                }

                //打包收费状态 0 是 1 否
                outOrder.ChargeOper.Memo = ormOrder.ORC.OrderStatus.Value;
                if (string.IsNullOrEmpty(outOrder.ChargeOper.Memo))
                {
                    outOrder.ChargeOper.Memo = "1";
                }

                //S 最高优先级 A较高优先级 R常规
                outOrder.IsEmergency = (ormOrder.ORC.GetQuantityTiming(0).Priority.Value == "S" || ormOrder.ORC.GetQuantityTiming(0).Priority.Value == "A");
                outOrder.IsHaveCharged = false;
                outOrder.OrderType = FS.HISFC.Models.Base.EnumSysClass.UO.ToString();
                alOrder.Add(outOrder);

                if (ormOrder.ORC.OrderControl.Value == "CA")
                {
                    outOrder.DCOper.ID = ormOrder.ORC.GetEnteredBy(0).IDNumber.Value;
                    outOrder.DCOper.OperTime = DateTime.ParseExact(ormOrder.ORC.DateTimeOfTransaction.TimeOfAnEvent.Value, "yyyyMMddHHmmss", null);
                    outOrder.Status = 3;
                    alDcOrder.Add(outOrder);
                }
                else if (ormOrder.ORC.OrderControl.Value == "RU")
                {
                    FS.HISFC.Models.Order.OutPatient.Order tempOrder = outOrder.Clone();
                    outOrder.DCOper.ID = ormOrder.ORC.GetEnteredBy(0).IDNumber.Value;
                    outOrder.DCOper.OperTime = DateTime.ParseExact(ormOrder.ORC.DateTimeOfTransaction.TimeOfAnEvent.Value, "yyyyMMddHHmmss", null);
                    outOrder.Status = 0;
                    tempOrder.Status = 0;
                    alDcOrder.Add(outOrder);
                    alOrder.Add(tempOrder);
                }
                else if (ormOrder.ORC.OrderControl.Value == "NW")
                {
                    outOrder.Status = 0;
                    alOrder.Add(outOrder);
                }
                else
                {
                    errInfo= "未知的医嘱控制ID：" + ormOrder.ORC.OrderControl.Value;
                    return -1;
                }

            }


            return Function.ClinicFee(regInfo, alOrder, alDcOrder,ormO01.MSH.SendingApplication.NamespaceID.Value, ref errInfo);
        }

    }
}
