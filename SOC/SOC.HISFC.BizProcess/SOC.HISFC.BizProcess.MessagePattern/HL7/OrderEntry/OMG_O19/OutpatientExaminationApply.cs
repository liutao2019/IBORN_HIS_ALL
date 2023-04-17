using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.BizProcess.MessagePatternInterface;
using FS.HL7Message;
using System.Collections;
using FS.SOC.HISFC.BizProcess.CommonInterface;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.HL7.OrderEntry.OMG_O19
{
    public class OutpatientExaminationApply 
    {
        private FS.HISFC.BizProcess.Integrate.Registration.Registration regIntegrate = new FS.HISFC.BizProcess.Integrate.Registration.Registration();
        private FS.HISFC.BizLogic.Order.OutPatient.Order outOrderMgr = new FS.HISFC.BizLogic.Order.OutPatient.Order();
        private FS.HISFC.BizLogic.Fee.Outpatient outFeeMgr = new FS.HISFC.BizLogic.Fee.Outpatient();
        private FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();
        private FS.HISFC.BizProcess.Integrate.Order orderIntegrate = new FS.HISFC.BizProcess.Integrate.Order();

        public int ProcessMessage(NHapi.Model.V24.Message.OMG_O19 omgO19, ref NHapi.Base.Model.IMessage ackMessage, ref string errInfo)
        {
            if (omgO19 == null || omgO19.ORDERRepetitionsUsed <= 0)
            {
                return 1;
            }

            FS.HISFC.Models.Registration.Register regInfo = new FS.HISFC.Models.Registration.Register();
            string clinicCode = omgO19.PATIENT.PATIENT_VISIT.PV1.VisitNumber.ID.Value;
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

            NHapi.Model.V24.Group.OMG_O19_ORDER omgOrder = null;
            FS.HISFC.Models.Fee.Item.Undrug undrgObj = null;
            //获取处方号
            string receipNO = this.outFeeMgr.GetRecipeNO();
            Hashtable hsRecipeNoAndSeq = new Hashtable();
            Dictionary<string, string> applyNO = new Dictionary<string, string>();
            FS.HISFC.Models.Order.OutPatient.Order outOrder = null;
            ArrayList alOrder = new ArrayList();
            ArrayList alDcOrder = new ArrayList();

            for (int i = 0; i < omgO19.ORDERRepetitionsUsed; i++)
            {
                outOrder = new FS.HISFC.Models.Order.OutPatient.Order();
                outOrder.SeeNO = this.outOrderMgr.GetNewSeeNo(regInfo.PID.CardNO).ToString();
                outOrder.ID = this.orderIntegrate.GetNewOrderID();
                outOrder.Patient.ID = regInfo.ID;
                outOrder.Patient.PID = regInfo.PID;
                outOrder.RegTime = regInfo.DoctorInfo.SeeDate;
                outOrder.InDept = regInfo.DoctorInfo.Templet.Dept;

                //获取医嘱信息
                omgOrder = omgO19.GetORDER(i);
                //检查部位
                outOrder.CheckPartRecord = omgOrder.OBR.SpecimenActionCode.Value;
                outOrder.ApplyNo = omgOrder.ORC.PlacerOrderNumber.EntityIdentifier.Value;

                //检查医嘱 重新获取处方号
              

                //非药品
                undrgObj = new FS.HISFC.Models.Fee.Item.Undrug();
                string itemCode = omgOrder.OBR.UniversalServiceIdentifier.Identifier.Value;
                undrgObj = feeIntegrate.GetItem(itemCode);
                if (undrgObj == null)
                {
                    errInfo = "获取项目ID为:" + itemCode + "的项目错误!" + feeIntegrate.Err;
                    return -1;
                }

                if (omgOrder.ORC.OrderControl.Value != "CA")
                {

                    outOrder.Item = undrgObj;
                    outOrder.Item.ItemType = FS.HISFC.Models.Base.EnumItemType.UnDrug;
                    //检查单申请的数量暂时只能是1
                    outOrder.Qty = 1;
                    if (string.IsNullOrEmpty(outOrder.Item.PriceUnit))
                    {
                        outOrder.Item.PriceUnit = "无";
                    }
                    outOrder.HerbalQty = 1;
                    outOrder.FT.PubCost = 0;
                    outOrder.FT.PayCost = 0;
                    outOrder.FT.OwnCost = outOrder.Qty * outOrder.Item.Price;

                    //此处暂时用开立科室作为执行科室
                    outOrder.ExeDept.ID = omgOrder.OBR.DiagnosticServSectID.Value;
                    if (outOrder.ExeDept.ID.Contains(OrderControl.FilterStr))
                    {
                        outOrder.ExeDept.ID = outOrder.ExeDept.ID.Split(OrderControl.FilterStr)[0];
                    }

                    //如果找不到科室，则取默认科室
                    if (string.IsNullOrEmpty(outOrder.ExeDept.ID) || CommonController.CreateInstance().GetDepartment(outOrder.ExeDept.ID) == null)
                    {
                        //去默认执行科室
                        if (undrgObj.ExecDepts.Count > 0)
                        {
                            outOrder.ExeDept.ID = ((FS.FrameWork.Models.NeuObject)undrgObj.ExecDepts[0]).ID;
                        }
                        else
                        {
                            outOrder.ExeDept.ID = omgOrder.ORC.EnteringOrganization.Identifier.Value;
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

                //处方号，检验的重新获取。并在前面加符号R
                outOrder.ReciptNO = receipNO; //omgOrder.ORC.PlacerGroupNumber.EntityIdentifier.Value;
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

                outOrder.Memo = omgOrder.OBR.RelevantClinicalInfo.Value;
                outOrder.ReciptDoctor.ID = omgOrder.ORC.GetOrderingProvider(0).IDNumber.Value;
                outOrder.ReciptDoctor = CommonController.CreateInstance().GetEmployee(outOrder.ReciptDoctor.ID);

                outOrder.ReciptDept.ID = omgOrder.ORC.EnteringOrganization.Identifier.Value;
                outOrder.MOTime = DateTime.ParseExact(omgOrder.ORC.DateTimeOfTransaction.TimeOfAnEvent.Value, "yyyyMMddHHmmss", null);
                outOrder.Status = 0;
                //用来存储检查申请发送系统
                outOrder.Sample.ID = omgOrder.OBR.UniversalServiceIdentifier.NameOfCodingSystem.Value;

                //打包收费状态 0 是 1 否
                outOrder.ChargeOper.Memo = omgOrder.ORC.OrderStatus.Value;
                if (string.IsNullOrEmpty(outOrder.ChargeOper.Memo))
                {
                    outOrder.ChargeOper.Memo = "1";
                }

                //S 最高优先级 A较高优先级 R常规
                outOrder.IsEmergency = (omgOrder.ORC.GetQuantityTiming(0).Priority.Value == "S" || omgOrder.ORC.GetQuantityTiming(0).Priority.Value == "A");
                outOrder.IsHaveCharged = false;
                //检查医嘱
                outOrder.OrderType = FS.HISFC.Models.Base.EnumSysClass.UC.ToString();


                if (omgOrder.ORC.OrderControl.Value == "CA")//取消
                {
                    outOrder.DCOper.ID = omgOrder.ORC.GetOrderingProvider(0).IDNumber.Value;
                    outOrder.DCOper.Dept.ID = omgOrder.ORC.EnteringOrganization.Identifier.Value;
                    outOrder.DCOper.OperTime = DateTime.ParseExact(omgOrder.ORC.DateTimeOfTransaction.TimeOfAnEvent.Value, "yyyyMMddHHmmss", null);
                    outOrder.Status = 3;
                    alDcOrder.Add(outOrder);
                }
                else if (omgOrder.ORC.OrderControl.Value == "RU")//修改
                {
                    FS.HISFC.Models.Order.OutPatient.Order tempOrder = outOrder.Clone();
                    outOrder.DCOper.ID = omgOrder.ORC.GetOrderingProvider(0).IDNumber.Value;
                    outOrder.DCOper.Dept.ID = omgOrder.ORC.EnteringOrganization.Identifier.Value;
                    outOrder.DCOper.OperTime = DateTime.ParseExact(omgOrder.ORC.DateTimeOfTransaction.TimeOfAnEvent.Value, "yyyyMMddHHmmss", null);
                    outOrder.Status = 0;
                    tempOrder.Status = 0;
                    alDcOrder.Add(outOrder);
                    alOrder.Add(tempOrder);
                }
                else if (omgOrder.ORC.OrderControl.Value == "NW")//新开
                {
                    outOrder.Status = 0;
                    alOrder.Add(outOrder);
                }
                else
                {
                    errInfo = "未知的医嘱控制ORC-1：" + omgOrder.ORC.OrderControl.Value;
                    return -1;
                }

            }

            return Function.ClinicFee(regInfo, alOrder, alDcOrder ,omgO19.MSH.SendingApplication.NamespaceID.Value , ref errInfo);
        }

    }
}
