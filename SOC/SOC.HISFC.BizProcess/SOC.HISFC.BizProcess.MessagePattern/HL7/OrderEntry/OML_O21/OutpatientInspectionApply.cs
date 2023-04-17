using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.BizProcess.MessagePatternInterface;
using FS.HL7Message;
using System.Collections;
using FS.SOC.HISFC.BizProcess.CommonInterface;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.HL7.OrderEntry.OML_O21
{
    public class OutpatientInspectionApply
    {

        private FS.HISFC.BizProcess.Integrate.Registration.Registration regIntegrate = new FS.HISFC.BizProcess.Integrate.Registration.Registration();
        private FS.HISFC.BizLogic.Order.OutPatient.Order outOrderMgr = new FS.HISFC.BizLogic.Order.OutPatient.Order();
        private FS.HISFC.BizLogic.Fee.Outpatient outFeeMgr = new FS.HISFC.BizLogic.Fee.Outpatient();
        private FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();
        private FS.HISFC.BizProcess.Integrate.Order orderIntegrate = new FS.HISFC.BizProcess.Integrate.Order();

        public int ProcessMessage(NHapi.Model.V24.Message.OML_O21 omlO21, ref NHapi.Base.Model.IMessage ackMessage, ref string errInfo)
        {
            if (omlO21 == null || omlO21.ORDER_GENERALRepetitionsUsed <= 0)
            {
                return 1;
            }

            FS.HISFC.Models.Registration.Register regInfo = new FS.HISFC.Models.Registration.Register();
            string clinicCode = omlO21.PATIENT.PATIENT_VISIT.PV1.VisitNumber.ID.Value;
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

            NHapi.Model.V24.Group.OML_O21_ORDER omlOrder = null;

            FS.HISFC.Models.Fee.Item.Undrug undrgObj = null;
            FS.HISFC.Models.Order.OutPatient.Order outOrder = null;

            //获取处方号
            string receipNO = this.outFeeMgr.GetRecipeNO();
            //组合号
            Dictionary<string, string> applyNO = new Dictionary<string, string>();

            Hashtable hsRecipeNoAndSeq = new Hashtable();
            ArrayList alOrder = new ArrayList();
            ArrayList alDcOrder = new ArrayList();
            for (int i = 0; i < omlO21.GetORDER_GENERAL().ORDERRepetitionsUsed; i++)
            {
                omlOrder = omlO21.GetORDER_GENERAL().GetORDER(i);
                if (omlOrder == null)
                {
                    errInfo = "获取医嘱消息内容失败!";
                    return -1;
                }

                outOrder = new FS.HISFC.Models.Order.OutPatient.Order();
                outOrder.SeeNO = this.outOrderMgr.GetNewSeeNo(regInfo.PID.CardNO).ToString();
                outOrder.ID = this.orderIntegrate.GetNewOrderID();
                outOrder.Patient.ID = regInfo.ID;
                outOrder.Patient.PID = regInfo.PID;
                outOrder.RegTime = regInfo.DoctorInfo.SeeDate;
                outOrder.InDept = regInfo.DoctorInfo.Templet.Dept;

                //非药品
                //检验样本
                outOrder.Sample.ID = omlOrder.OBSERVATION_REQUEST.OBR.SpecimenActionCode.Value;
                //申请单号
                outOrder.ApplyNo = omlOrder.ORC.PlacerOrderNumber.EntityIdentifier.Value;
                

                if (omlOrder.ORC.OrderControl.Value != "CA")
                {
                    //非药品
                    undrgObj = new FS.HISFC.Models.Fee.Item.Undrug();
                    //获取项目编码
                    string itemCode = omlOrder.OBSERVATION_REQUEST.OBR.UniversalServiceIdentifier.Identifier.Value;
                    undrgObj = feeIntegrate.GetItem(itemCode);
                    if (undrgObj == null)
                    {
                        errInfo = "获取项目ID为:" + itemCode + "的项目错误!" + feeIntegrate.Err;
                        return -1;
                    }
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

                    //执行科室
                    outOrder.ExeDept.ID = omlOrder.OBSERVATION_REQUEST.OBR.DiagnosticServSectID.Value;
                    if (string.IsNullOrEmpty(outOrder.ExeDept.ID) || CommonController.CreateInstance().GetDepartment(outOrder.ExeDept.ID) == null)
                    {
                        //去默认执行科室
                        if (undrgObj.ExecDepts.Count > 0)
                        {
                            outOrder.ExeDept.ID = ((FS.FrameWork.Models.NeuObject)undrgObj.ExecDepts[0]).ID;
                        }
                        else
                        {
                            outOrder.ExeDept.ID = omlOrder.ORC.EnteringOrganization.Identifier.Value;
                        }
                    }
                    //执行科室名称
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
                //处方号也需要重新获取
                if (applyNO.ContainsKey(outOrder.ApplyNo))
                {
                    receipNO = this.outFeeMgr.GetRecipeNO();
                    outOrder.Combo.ID = applyNO[outOrder.ApplyNo];
                }
                else
                {
                    string combNo = this.outOrderMgr.GetNewOrderComboID();
                    outOrder.Combo.ID = combNo;
                    applyNO[outOrder.ApplyNo] = combNo;
                }

                //处方号，检验的重新获取。并在前面加符号R
                outOrder.ReciptNO = receipNO; //omlOrder.ORC.PlacerGroupNumber.EntityIdentifier.Value;
                //形成处方内流水号
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
                //备注
                outOrder.Memo = omlOrder.OBSERVATION_REQUEST.OBR.RelevantClinicalInfo.Value;
                //开单医生
                outOrder.ReciptDoctor.ID = omlOrder.ORC.GetOrderingProvider(0).IDNumber.Value;
                outOrder.ReciptDoctor = CommonController.CreateInstance().GetEmployee(outOrder.ReciptDoctor.ID);
                //开单科室
                outOrder.ReciptDept.ID = omlOrder.ORC.EnteringOrganization.Identifier.Value;

                //开立时间
                outOrder.MOTime = DateTime.ParseExact(omlOrder.ORC.DateTimeOfTransaction.TimeOfAnEvent.Value, "yyyyMMddHHmmss", null);
                outOrder.Status = 0;

                //作废申请单
                if (omlOrder.ORC.OrderControl.Value == "CA")
                {
                    outOrder.DCOper.ID = omlOrder.ORC.GetEnteredBy(0).IDNumber.Value;
                    outOrder.DCOper.OperTime = DateTime.ParseExact(omlOrder.ORC.DateTimeOfTransaction.TimeOfAnEvent.Value, "yyyyMMddHHmmss", null);
                }

                //打包收费状态 0 是 1 否
                outOrder.ChargeOper.Memo = omlOrder.ORC.OrderStatus.Value;
                if (string.IsNullOrEmpty(outOrder.ChargeOper.Memo))
                {
                    outOrder.ChargeOper.Memo = "1";
                }

                //S 最高优先级 A较高优先级 R常规
                outOrder.IsEmergency = (omlOrder.ORC.GetQuantityTiming(0).Priority.Value == "S" || omlOrder.ORC.GetQuantityTiming(0).Priority.Value == "A");
                outOrder.IsHaveCharged = false;
                outOrder.OrderType = FS.HISFC.Models.Base.EnumSysClass.UL.ToString();


                if (omlOrder.ORC.OrderControl.Value == "CA")
                {
                    outOrder.DCOper.ID = omlOrder.ORC.GetEnteredBy(0).IDNumber.Value;
                    outOrder.DCOper.OperTime = DateTime.ParseExact(omlOrder.ORC.DateTimeOfTransaction.TimeOfAnEvent.Value, "yyyyMMddHHmmss", null);
                    outOrder.Status = 3;
                    alDcOrder.Add(outOrder);
                }
                else if (omlOrder.ORC.OrderControl.Value == "RU")
                {
                    FS.HISFC.Models.Order.OutPatient.Order tempOrder = outOrder.Clone();
                    outOrder.DCOper.ID = omlOrder.ORC.GetEnteredBy(0).IDNumber.Value;
                    outOrder.DCOper.OperTime = DateTime.ParseExact(omlOrder.ORC.DateTimeOfTransaction.TimeOfAnEvent.Value, "yyyyMMddHHmmss", null);
                    outOrder.Status = 0;
                    tempOrder.Status = 0;
                    alDcOrder.Add(outOrder);
                    alOrder.Add(tempOrder);
                }
                else if (omlOrder.ORC.OrderControl.Value == "NW")
                {
                    outOrder.Status = 0;
                    alOrder.Add(outOrder);
                }

                else
                {
                    errInfo = "未知的医嘱控制ID：" + omlOrder.ORC.OrderControl.Value;
                    return -1;
                }
            }

            return Function.ClinicFee(regInfo, alOrder, alDcOrder,omlO21.MSH.SendingApplication.NamespaceID.Value,ref errInfo);
        }

    }
}
