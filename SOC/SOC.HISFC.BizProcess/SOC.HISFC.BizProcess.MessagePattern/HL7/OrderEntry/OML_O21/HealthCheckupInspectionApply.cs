using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.HL7.OrderEntry.OML_O21
{
    /// <summary>
    /// 体检划价信息
    /// </summary>
    public class HealthCheckupInspectionApply
    {

        public int ProcessMessage(FS.HISFC.HealthCheckup.Object.ChkRegister register, ArrayList alObj, bool isPositive, ref NHapi.Model.V24.Message.OML_O21[] OML_O21S, ref string errInfo)
        {
            if (alObj == null || alObj.Count == 0)
            {
                return 1;
            }

            Dictionary<string, NHapi.Model.V24.Message.OML_O21> applyMessage = new Dictionary<string, NHapi.Model.V24.Message.OML_O21>();
            Dictionary<string, NHapi.Model.V24.Group.OML_O21_ORDER> ApplyNOs = new Dictionary<string, NHapi.Model.V24.Group.OML_O21_ORDER>();
            NHapi.Model.V24.Message.OML_O21 OML_021 = null;

            int seqno = 1;
            
           foreach (FS.HISFC.HealthCheckup.Object.CHKFeeItem feeItemList in alObj)
           {
               FS.SOC.HISFC.BizProcess.MessagePattern.BizLogic.Healthcheckup HealthMgr = new FS.SOC.HISFC.BizProcess.MessagePattern.BizLogic.Healthcheckup();

               feeItemList.UndrugComb.Package.ID =  HealthMgr.GetTjUndrugGroup(feeItemList.PackageCode);
               feeItemList.UndrugComb.Package.Name = FS.SOC.HISFC.BizProcess.Cache.Fee.GetItem(feeItemList.UndrugComb.Package.ID).Name;

               if (applyMessage.ContainsKey(feeItemList.Order.ID))
                {
                    OML_021 = applyMessage[feeItemList.Order.ID];
                }
                else
                {
                    OML_021 = new NHapi.Model.V24.Message.OML_O21();
                    //MSH
                    OML_021.MSH.MessageType.MessageType.Value = "OML";
                    OML_021.MSH.MessageType.TriggerEvent.Value = "O21";
                    FS.HL7Message.V24.Function.GenerateMSH(OML_021.MSH);
                    OML_021.MSH.SendingApplication.NamespaceID.Value = "PEIS";
                    //PID
                    //OML_021.PATIENT.PID.PatientID.ID.Value = register.PatientInfo.PID.CardNO;//卡号
                    //NHapi.Model.V24.Datatype.CX patientList1 = OML_021.PATIENT.PID.GetPatientIdentifierList(0);
                    //patientList1.IdentifierTypeCode.Value = "IDCard";
                    //patientList1.ID.Value = register.PatientInfo.PID.ID;


                    //其他厂商确定用这个IDCard
                    //PID 患者基本信息
                    OML_021.PATIENT.PID.SetIDPID.Value = "1";
                    OML_021.PATIENT.PID.PatientID.ID.Value = register.PatientInfo.PID.CardNO;

                    NHapi.Model.V24.Datatype.CX patientList1 = OML_021.PATIENT.PID.GetPatientIdentifierList(0);
                    patientList1.IdentifierTypeCode.Value = "IDCard";//register.Card.CardType.ID;
                    patientList1.ID.Value = register.PatientInfo.PID.CardNO;
                    if (!string.IsNullOrEmpty(register.Card.CardType.ID) && !register.Card.CardType.ID.Equals("IDCard"))
                    {
                        NHapi.Model.V24.Datatype.CX patientList2 = OML_021.PATIENT.PID.GetPatientIdentifierList(OML_021.PATIENT.PID.PatientIdentifierListRepetitionsUsed);
                        patientList2.IdentifierTypeCode.Value = register.Card.CardType.ID;
                        patientList2.ID.Value = register.Card.ID;
                    }

                    if (!string.IsNullOrEmpty(register.IDCardType.ID))
                    {
                        NHapi.Model.V24.Datatype.CX patientList3 = OML_021.PATIENT.PID.GetPatientIdentifierList(OML_021.PATIENT.PID.PatientIdentifierListRepetitionsUsed);
                        patientList3.IdentifierTypeCode.Value = register.IDCardType.ID;
                        patientList3.ID.Value = register.IDCard;
                    }

                    //姓名
                    NHapi.Model.V24.Datatype.XPN patientName = OML_021.PATIENT.PID.GetPatientName(0);
                    patientName.FamilyName.Surname.Value = register.PatientInfo.Name;
                    OML_021.PATIENT.PID.AdministrativeSex.Value = register.PatientInfo.Sex.ID.ToString();
                                          
                    if (register.PatientInfo.Birthday > DateTime.MinValue)
                    {
                        OML_021.PATIENT.PID.DateTimeOfBirth.TimeOfAnEvent.Value = register.PatientInfo.Birthday.ToString("yyyyMMdd");
                    }
                    /// PV1
                    if (register.CHKKind == "1") //个人体检
                        OML_021.PATIENT.PATIENT_VISIT.PV1.PatientClass.Value = "T";
                    else //集体体检
                        OML_021.PATIENT.PATIENT_VISIT.PV1.PatientClass.Value = "J";
                    OML_021.PATIENT.PATIENT_VISIT.PV1.VisitNumber.ID.Value = register.ChkClinicNo; //体检流水号
                    OML_021.PATIENT.PATIENT_VISIT.PV1.AssignedPatientLocation.Facility.NamespaceID.Value = register.Operator.Dept.ID;//划价科室
                    applyMessage.Add(feeItemList.Order.ID, OML_021);
             }
            //ORC 
         
                string itemCode = (string.IsNullOrEmpty(feeItemList.UndrugComb.Package.ID) ? feeItemList.Item.ID : feeItemList.UndrugComb.Package.ID);
                string itemName = (string.IsNullOrEmpty(feeItemList.UndrugComb.Package.Name) ? feeItemList.Item.Name : feeItemList.UndrugComb.Package.Name);
                string applyno = "";
                if (string.IsNullOrEmpty(feeItemList.UndrugComb.Package.ID))
                {
                     applyno = feeItemList.Order.ID + feeItemList.Item.ID;
                }
                else
                {
                     applyno = feeItemList.Order.ID + itemCode;
                }
                 
                if (ApplyNOs.ContainsKey(applyno))
                {
                    ApplyNOs[applyno].OBSERVATION_REQUEST.OBR.ChargeToPractice.DollarAmount.Quantity.Value = (FS.FrameWork.Function.NConvert.ToDecimal(ApplyNOs[applyno].OBSERVATION_REQUEST.OBR.ChargeToPractice.DollarAmount.Quantity.Value) + feeItemList.FT.TotCost).ToString("F2");
                }
                else
                {
                    NHapi.Model.V24.Group.OML_O21_ORDER_GENERAL GeneralOrder = OML_021.GetORDER_GENERAL();
                    NHapi.Model.V24.Group.OML_O21_ORDER order = GeneralOrder.GetORDER(GeneralOrder.ORDERRepetitionsUsed);
                    order.ORC.OrderControl.Value = isPositive ? "NW" : "CA";
                    order.ORC.PlacerOrderNumber.UniversalID.Value = register.ID;
                    order.ORC.FillerOrderNumber.EntityIdentifier.Value = feeItemList.RecipeNO;
                    order.ORC.PlacerOrderNumber.UniversalIDType.Value = "O";
                    order.ORC.PlacerOrderNumber.EntityIdentifier.Value = feeItemList.Order.ID;
                    order.ORC.PlacerGroupNumber.EntityIdentifier.Value = feeItemList.RecipeNO;
                    order.ORC.DateTimeOfTransaction.TimeOfAnEvent.Value = feeItemList.ChargeOper.OperTime.ToString("yyyyMMddHHmms");//开立时间
                    order.ORC.GetEnteredBy(0).IDNumber.Value = feeItemList.RecipeOper.ID.ToString(); //操作员
                    order.ORC.GetOrderingProvider(0).IDNumber.Value = feeItemList.RecipeOper.ID; //医生
                    order.ORC.OrderEffectiveDateTime.TimeOfAnEvent.Value = feeItemList.ChargeOper.OperTime.ToString("yyyyMMddHHmmss");//开始时间
                    order.ORC.EnteringOrganization.Identifier.Value = feeItemList.RecipeOper.Dept.ID; //录入科室编码
                    order.ORC.GetOrderingFacilityName(0).IDNumber.Value = feeItemList.FeeOper.Dept.ID;  //  患者科室编码

                    // OBR 
                    order.OBSERVATION_REQUEST.OBR.SetIDOBR.Value = seqno.ToString();//序号
                    order.OBSERVATION_REQUEST.OBR.UniversalServiceIdentifier.Identifier.Value = itemCode;// 项目编码
                    order.OBSERVATION_REQUEST.OBR.UniversalServiceIdentifier.Text.Value = itemName;
                    order.OBSERVATION_REQUEST.OBR.UniversalServiceIdentifier.AlternateIdentifier.Value = feeItemList.Order.ID;//医嘱流水号
                    order.OBSERVATION_REQUEST.OBR.SpecimenSource.SpecimenSourceNameOrCode.Text.Value = feeItemList.Order.Sample.Name;//检验样本
                    order.OBSERVATION_REQUEST.OBR.UniversalServiceIdentifier.NameOfCodingSystem.Value = feeItemList.Order.CheckPartRecord;//
                    order.OBSERVATION_REQUEST.OBR.DiagnosticServSectID.Value = feeItemList.ExecOper.Dept.ID;//执行科室

                    ApplyNOs[applyno] = order;
                    seqno++;
                }
           
            }
            OML_O21S = applyMessage.Values.ToArray<NHapi.Model.V24.Message.OML_O21>();
            return 1;

        }
     
 
        
    }
}
