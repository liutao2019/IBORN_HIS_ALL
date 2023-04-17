using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using FS.HL7Message;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.HL7.OrderEntry.OMG_O19
{
   public class  HealthCheckupExaminationApply
   {
       #region 原来的
       //public int ProcessMessage(FS.HISFC.HealthCheckup.Object.ChkRegister register, ArrayList alObj, bool isPositive, ref NHapi.Model.V24.Message.OMG_O19[] OMG_O19S, ref string errInfo)
       // {

       //     if (alObj == null || alObj.Count == 0)
       //     {
       //         return 1;
       //     }

       //     Dictionary<string, NHapi.Model.V24.Message.OMG_O19> applyMessage = new Dictionary<string, NHapi.Model.V24.Message.OMG_O19>();
       //     Dictionary<string, NHapi.Model.V24.Group.OMG_O19_ORDER> ApplyNOs = new Dictionary<string, NHapi.Model.V24.Group.OMG_O19_ORDER>();
       //     NHapi.Model.V24.Message.OMG_O19 OMG_O19 = null;
       //     int seqno = 1;
       //     foreach (FS.HISFC.HealthCheckup.Object.CHKFeeItem feeItemList in alObj)
       //    {
       //        if (applyMessage.ContainsKey(feeItemList.Order.ID + feeItemList.UndrugComb.ID + feeItemList.Item.ID))
       //        {
       //            OMG_O19 = applyMessage[feeItemList.Order.ID + feeItemList.UndrugComb.ID + feeItemList.Item.ID];
       //        }
       //        else
       //        {
       //             OMG_O19 = new NHapi.Model.V24.Message.OMG_O19();
       //             //MSH
       //             OMG_O19.MSH.MessageType.MessageType.Value = "OMG";
       //             OMG_O19.MSH.MessageType.TriggerEvent.Value = "O19";
       //             FS.HL7Message.V24.Function.GenerateMSH(OMG_O19.MSH);
       //             OMG_O19.MSH.SendingApplication.NamespaceID.Value = "PEIS";
       //             //PID
       //             //OMG_O19.PATIENT.PID.PatientID.ID.Value = register.PatientInfo.PID.CardNO;//卡号
       //             //NHapi.Model.V24.Datatype.CX patientList1 = OMG_O19.PATIENT.PID.GetPatientIdentifierList(0);
       //             //patientList1.IdentifierTypeCode.Value = "IDCard";
       //             //patientList1.ID.Value = register.PatientInfo.PID.ID;
       //             //其他厂商确定用这个IDCard
       //             //PID 患者基本信息
       //             OMG_O19.PATIENT.PID.SetIDPID.Value = "1";
       //             OMG_O19.PATIENT.PID.PatientID.ID.Value = register.PatientInfo.PID.CardNO;

       //             NHapi.Model.V24.Datatype.CX patientList1 = OMG_O19.PATIENT.PID.GetPatientIdentifierList(0);
       //             patientList1.IdentifierTypeCode.Value = "IDCard";//register.Card.CardType.ID;
       //             patientList1.ID.Value = register.PatientInfo.PID.CardNO;
       //             if (!string.IsNullOrEmpty(register.Card.CardType.ID) && !register.Card.CardType.ID.Equals("IDCard"))
       //             {
       //                 NHapi.Model.V24.Datatype.CX patientList2 = OMG_O19.PATIENT.PID.GetPatientIdentifierList(OMG_O19.PATIENT.PID.PatientIdentifierListRepetitionsUsed);
       //                 patientList2.IdentifierTypeCode.Value = register.Card.CardType.ID;
       //                 patientList2.ID.Value = register.Card.ID;
       //             }

       //             if (!string.IsNullOrEmpty(register.IDCardType.ID))
       //             {
       //                 NHapi.Model.V24.Datatype.CX patientList3 = OMG_O19.PATIENT.PID.GetPatientIdentifierList(OMG_O19.PATIENT.PID.PatientIdentifierListRepetitionsUsed);
       //                 patientList3.IdentifierTypeCode.Value = register.IDCardType.ID;
       //                 patientList3.ID.Value = register.IDCard;
       //             }

       //             //姓名
       //             NHapi.Model.V24.Datatype.XPN patientName = OMG_O19.PATIENT.PID.GetPatientName(0);
       //             patientName.FamilyName.Surname.Value = register.PatientInfo.Name;
       //             OMG_O19.PATIENT.PID.AdministrativeSex.Value = register.PatientInfo.Sex.ID.ToString();
       //             if (register.PatientInfo.Birthday > DateTime.MinValue)
       //             {
       //                 OMG_O19.PATIENT.PID.DateTimeOfBirth.TimeOfAnEvent.Value = register.PatientInfo.Birthday.ToString("yyyyMMdd");
       //             }
       //             /// PV1
       //             if (register.CHKKind == "1") //个人体检
       //                 OMG_O19.PATIENT.PATIENT_VISIT.PV1.PatientClass.Value = "T";
       //             else //集体体检
       //                 OMG_O19.PATIENT.PATIENT_VISIT.PV1.PatientClass.Value = "J";
       //             OMG_O19.PATIENT.PATIENT_VISIT.PV1.VisitNumber.ID.Value = register.ChkClinicNo; //体检流水号
       //             OMG_O19.PATIENT.PATIENT_VISIT.PV1.AssignedPatientLocation.Facility.NamespaceID.Value = register.Operator.Dept.ID;//划价科室
       //             applyMessage.Add(feeItemList.Order.ID + feeItemList.UndrugComb.ID + feeItemList.Item.ID, OMG_O19);
             
             
       //      }

       //         string itemCode = (string.IsNullOrEmpty(feeItemList.UndrugComb.Package.ID) ? feeItemList.Item.ID : feeItemList.UndrugComb.Package.ID);
       //         string itemName = (string.IsNullOrEmpty(feeItemList.UndrugComb.Package.Name) ? feeItemList.Item.Name : feeItemList.UndrugComb.Package.Name);        
       //         string applyno = feeItemList.Order.ID + feeItemList.UndrugComb.ID + itemCode;
       //         if (ApplyNOs.ContainsKey(applyno))
       //         {
       //             ApplyNOs[applyno].OBR.ChargeToPractice.DollarAmount.Quantity.Value = (FS.FrameWork.Function.NConvert.ToDecimal(ApplyNOs[applyno].OBR.ChargeToPractice.DollarAmount.Quantity.Value) + feeItemList.FT.TotCost).ToString("F2");
       //         }
       //         else
       //         {
       //            // NHapi.Model.V24.Group.OMG_O19_OBSERVATION GeneralOrder = OMG_O19;
       //             NHapi.Model.V24.Group.OMG_O19_ORDER order = OMG_O19.GetORDER();             
       //             order.ORC.OrderControl.Value = isPositive ? "NW" : "CA";
       //             order.ORC.PlacerOrderNumber.UniversalID.Value = register.ID;
       //             order.ORC.FillerOrderNumber.EntityIdentifier.Value = feeItemList.RecipeNO;
       //             order.ORC.PlacerOrderNumber.UniversalIDType.Value = "O";
       //             order.ORC.PlacerOrderNumber.EntityIdentifier.Value = feeItemList.Order.ID;
       //             order.ORC.PlacerGroupNumber.EntityIdentifier.Value = feeItemList.RecipeNO;
       //             order.ORC.DateTimeOfTransaction.TimeOfAnEvent.Value = feeItemList.ChargeOper.OperTime.ToString("yyyyMMddHHmmss");//开立时间
       //             order.ORC.GetEnteredBy(0).IDNumber.Value = feeItemList.RecipeOper.ID.ToString(); //操作员
       //             order.ORC.GetOrderingProvider(0).IDNumber.Value = feeItemList.ChargeOper.ID; //医生
       //             order.ORC.OrderEffectiveDateTime.TimeOfAnEvent.Value = feeItemList.ChargeOper.OperTime.ToString("yyyyMMddHHmmss");//开始时间
       //             order.ORC.EnteringOrganization.Identifier.Value =  feeItemList.RecipeOper.Dept.ID; //录入科室编码
       //             order.ORC.GetOrderingFacilityName(0).IDNumber.Value =  feeItemList.FeeOper.Dept.ID;  //  患者科室编码
       //             order.ORC.OrderStatus.Value = "0";//默认全部收费 

       //             // OBR 
       //             order.OBR.SetIDOBR.Value = seqno.ToString();//序号
       //             order.OBR.UniversalServiceIdentifier.Identifier.Value =  itemCode;// 项目编码
       //             order.OBR.UniversalServiceIdentifier.Text.Value = itemName;
       //             order.OBR.UniversalServiceIdentifier.AlternateIdentifier.Value = feeItemList.Order.ID;//医嘱流水号
       //             order.OBR.SpecimenSource.SpecimenSourceNameOrCode.Identifier.Value = feeItemList.Order.Sample.Name;//检查部位
       //             order.OBR.UniversalServiceIdentifier.NameOfCodingSystem.Value = feeItemList.Order.CheckPartRecord;
       //             order.OBR.DiagnosticServSectID.Value = feeItemList.ExecOper.Dept.ID;//执行科室
                   

       //             ApplyNOs[applyno] = order;
       //             seqno++;
       //         }
       //     }
       //     OMG_O19S = applyMessage.Values.ToArray<NHapi.Model.V24.Message.OMG_O19>();

       //     return 1;

       // }
       #endregion

       public int ProcessMessage(FS.HISFC.HealthCheckup.Object.ChkRegister register, ArrayList alObj, bool isPositive, ref NHapi.Model.V24.Message.OMG_O19[] OMG_O19S, ref string errInfo)
       {

           if (alObj == null || alObj.Count == 0)
           {
               return 1;
           }

           Dictionary<string, NHapi.Model.V24.Message.OMG_O19> applyMessage = new Dictionary<string, NHapi.Model.V24.Message.OMG_O19>();
           Dictionary<string, NHapi.Model.V24.Group.OMG_O19_ORDER> ApplyNOs = new Dictionary<string, NHapi.Model.V24.Group.OMG_O19_ORDER>();
           NHapi.Model.V24.Message.OMG_O19 OMG_O19 = null;
           int seqno = 1;

           FS.SOC.HISFC.BizProcess.MessagePattern.BizLogic.Healthcheckup HealthMgr = new FS.SOC.HISFC.BizProcess.MessagePattern.BizLogic.Healthcheckup();

           foreach (FS.HISFC.HealthCheckup.Object.CHKFeeItem feeItemList in alObj)
           {
              
               feeItemList.UndrugComb.Package.ID = HealthMgr.GetTjUndrugGroup(feeItemList.PackageCode);
               feeItemList.UndrugComb.Package.Name = FS.SOC.HISFC.BizProcess.Cache.Fee.GetItem(feeItemList.UndrugComb.Package.ID).Name;

               string key = feeItemList.Order.ID + feeItemList.Item.ID;
               if (string.IsNullOrEmpty(feeItemList.UndrugComb.Package.ID) == false)
               {
                   //feeItemList.UndrugComb.Package.ID = HealthMgr.GetTjUndrugGroup(feeItemList.PackageCode);
                   //feeItemList.UndrugComb.Package.Name = FS.SOC.HISFC.BizProcess.Cache.Fee.GetItem(feeItemList.UndrugComb.Package.ID).Name;

                   key = feeItemList.Order.ID + feeItemList.UndrugComb.Package.ID;
               }

               if (applyMessage.ContainsKey(key))
               {
                   OMG_O19 = applyMessage[key];
               }
               else
               {
                   OMG_O19 = new NHapi.Model.V24.Message.OMG_O19();
                   //MSH
                   OMG_O19.MSH.MessageType.MessageType.Value = "OMG";
                   OMG_O19.MSH.MessageType.TriggerEvent.Value = "O19";
                   FS.HL7Message.V24.Function.GenerateMSH(OMG_O19.MSH);
                   OMG_O19.MSH.SendingApplication.NamespaceID.Value = "PEIS";
                   //PID
                   //OMG_O19.PATIENT.PID.PatientID.ID.Value = register.PatientInfo.PID.CardNO;//卡号
                   //NHapi.Model.V24.Datatype.CX patientList1 = OMG_O19.PATIENT.PID.GetPatientIdentifierList(0);
                   //patientList1.IdentifierTypeCode.Value = "IDCard";
                   //patientList1.ID.Value = register.PatientInfo.PID.ID;
                   //其他厂商确定用这个IDCard
                   //PID 患者基本信息
                   OMG_O19.PATIENT.PID.SetIDPID.Value = "1";
                   OMG_O19.PATIENT.PID.PatientID.ID.Value = register.PatientInfo.PID.CardNO;

                   NHapi.Model.V24.Datatype.CX patientList1 = OMG_O19.PATIENT.PID.GetPatientIdentifierList(0);
                   patientList1.IdentifierTypeCode.Value = "IDCard";//register.Card.CardType.ID;
                   patientList1.ID.Value = register.PatientInfo.PID.CardNO;
                   if (!string.IsNullOrEmpty(register.Card.CardType.ID) && !register.Card.CardType.ID.Equals("IDCard"))
                   {
                       NHapi.Model.V24.Datatype.CX patientList2 = OMG_O19.PATIENT.PID.GetPatientIdentifierList(OMG_O19.PATIENT.PID.PatientIdentifierListRepetitionsUsed);
                       patientList2.IdentifierTypeCode.Value = register.Card.CardType.ID;
                       patientList2.ID.Value = register.Card.ID;
                   }

                   if (!string.IsNullOrEmpty(register.IDCardType.ID))
                   {
                       NHapi.Model.V24.Datatype.CX patientList3 = OMG_O19.PATIENT.PID.GetPatientIdentifierList(OMG_O19.PATIENT.PID.PatientIdentifierListRepetitionsUsed);
                       patientList3.IdentifierTypeCode.Value = register.IDCardType.ID;
                       patientList3.ID.Value = register.IDCard;
                   }

                   //姓名
                   NHapi.Model.V24.Datatype.XPN patientName = OMG_O19.PATIENT.PID.GetPatientName(0);
                   patientName.FamilyName.Surname.Value = register.PatientInfo.Name;
                   OMG_O19.PATIENT.PID.AdministrativeSex.Value = register.PatientInfo.Sex.ID.ToString();
                   if (register.PatientInfo.Birthday > DateTime.MinValue)
                   {
                       OMG_O19.PATIENT.PID.DateTimeOfBirth.TimeOfAnEvent.Value = register.PatientInfo.Birthday.ToString("yyyyMMdd");
                   }
                   /// PV1
                   if (register.CHKKind == "1") //个人体检
                       OMG_O19.PATIENT.PATIENT_VISIT.PV1.PatientClass.Value = "T";
                   else //集体体检
                       OMG_O19.PATIENT.PATIENT_VISIT.PV1.PatientClass.Value = "J";
                   OMG_O19.PATIENT.PATIENT_VISIT.PV1.VisitNumber.ID.Value = register.ChkClinicNo; //体检流水号
                   OMG_O19.PATIENT.PATIENT_VISIT.PV1.AssignedPatientLocation.Facility.NamespaceID.Value = feeItemList.RecipeOper.Dept.ID;//划价科室
                   OMG_O19.PATIENT.PATIENT_VISIT.PV1.AssignedPatientLocation.LocationStatus.Value  = feeItemList.RecipeOper.Dept.Name; //划价科室
                   applyMessage.Add(key,OMG_O19);


               }

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
                   ApplyNOs[applyno].OBR.ChargeToPractice.DollarAmount.Quantity.Value = (FS.FrameWork.Function.NConvert.ToDecimal(ApplyNOs[applyno].OBR.ChargeToPractice.DollarAmount.Quantity.Value) + feeItemList.Item.Qty * feeItemList.Item.Price).ToString("F2");
               }
               else
               {
                   // NHapi.Model.V24.Group.OMG_O19_OBSERVATION GeneralOrder = OMG_O19;
                   NHapi.Model.V24.Group.OMG_O19_ORDER order = OMG_O19.GetORDER();
                   order.ORC.OrderControl.Value = isPositive ? "NW" : "CA";
                   order.ORC.PlacerOrderNumber.UniversalID.Value = register.ID;
                   order.ORC.FillerOrderNumber.EntityIdentifier.Value = feeItemList.RecipeNO;
                   order.ORC.PlacerOrderNumber.UniversalIDType.Value = "O";
                   order.ORC.PlacerOrderNumber.EntityIdentifier.Value = feeItemList.Order.ID;
                   order.ORC.PlacerGroupNumber.EntityIdentifier.Value = feeItemList.RecipeNO;
                   order.ORC.DateTimeOfTransaction.TimeOfAnEvent.Value = feeItemList.ChargeOper.OperTime.ToString("yyyyMMddHHmmss");//开立时间
                   order.ORC.GetEnteredBy(0).IDNumber.Value = feeItemList.RecipeOper.ID.ToString(); //操作员
                   order.ORC.GetOrderingProvider(0).IDNumber.Value = feeItemList.ChargeOper.ID; //医生
                   order.ORC.OrderEffectiveDateTime.TimeOfAnEvent.Value = feeItemList.ChargeOper.OperTime.ToString("yyyyMMddHHmmss");//开始时间
                   order.ORC.EnteringOrganization.Identifier.Value = feeItemList.RecipeOper.Dept.ID; //录入科室编码
                   order.ORC.EnteringOrganization.Text.Value = feeItemList.RecipeOper.Dept.Name; //录入科室编码
                   order.ORC.GetOrderingFacilityName(0).IDNumber.Value = feeItemList.FeeOper.Dept.ID;  //  患者科室编码
                   order.ORC.OrderStatus.Value = "0";//默认全部收费 

                   // OBR 
                   order.OBR.SetIDOBR.Value = seqno.ToString();//序号
                   order.OBR.UniversalServiceIdentifier.Identifier.Value = itemCode;// 项目编码
                   order.OBR.UniversalServiceIdentifier.Text.Value = itemName;
                   order.OBR.UniversalServiceIdentifier.AlternateIdentifier.Value = feeItemList.Order.ID;//医嘱流水号
                   order.OBR.SpecimenSource.SpecimenSourceNameOrCode.Identifier.Value = feeItemList.Order.Sample.Name;//检查部位
                   order.OBR.UniversalServiceIdentifier.NameOfCodingSystem.Value = feeItemList.Order.CheckPartRecord;
                   order.OBR.DiagnosticServSectID.Value = feeItemList.ExecOper.Dept.ID;//执行科室
                   
                   order.OBR.ChargeToPractice.DollarAmount.Quantity.Value= (feeItemList.Item.Qty * feeItemList.Item.Price).ToString("F2");

                   ApplyNOs[applyno] = order;
                   seqno++;
               }
           }
           OMG_O19S = applyMessage.Values.ToArray<NHapi.Model.V24.Message.OMG_O19>();

           return 1;

       }
    }
}
