using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.HL7Message;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.HL7.OrderEntry.ORU_R01
{
    /// <summary>
    /// 检查报告
    /// </summary>
   public  class HealthCheckupExaminationReport
    {
       public int ProcessMessage(NHapi.Model.V24.Message.ORU_R01 orur01, ref NHapi.Base.Model.IMessage ackMessage, ref string errInfo)
       {
           if (orur01 == null )
           {
               return 1;
           }
           FS.HISFC.Models.Registration.Register regInfo = new FS.HISFC.Models.Registration.Register();
           FS.HISFC.HealthCheckup.BizLogic.Report ReportManger = new FS.HISFC.HealthCheckup.BizLogic.Report();
           FS.SOC.HISFC.BizProcess.MessagePattern.BizLogic.Healthcheckup healthMgr = new FS.SOC.HISFC.BizProcess.MessagePattern.BizLogic.Healthcheckup();
           string clinicCode = orur01.GetPATIENT_RESULT().PATIENT.VISIT.PV1.VisitNumber.ID.Value;
          // regInfo = regIntegrate.GetByClinic(clinicCode);
           if (regInfo == null)
           {
            //   errInfo = "获取患者信息" + clinicCode + "失败!" + regIntegrate.Err;
               return -1;
           }
          // NHapi.Model.V24.Group.
           FS.HISFC.HealthCheckup.Object.PacsReportItem pacsResult = new FS.HISFC.HealthCheckup.Object.PacsReportItem();//检查报告
           for (int i = 0; i < orur01.GetPATIENT_RESULT().ORDER_OBSERVATIONRepetitionsUsed; i++)
           { 
            //PID
               pacsResult.CLINIC_CODE = orur01.GetPATIENT_RESULT().PATIENT.VISIT.PV1.VisitNumber.ID.Value;// 体检流水号
               pacsResult.CARD_NO = orur01.GetPATIENT_RESULT().PATIENT.PID.PatientID.ID.Value;// 病历号
               
           //OBR
               pacsResult.SEQUENCENO = orur01.GetPATIENT_RESULT().GetORDER_OBSERVATION(i).OBR.PlacerOrderNumber.EntityIdentifier.Value;
               pacsResult.HISITEM_CODE = orur01.GetPATIENT_RESULT().GetORDER_OBSERVATION(i).OBR.UniversalServiceIdentifier.Identifier.Value; //检查ID
               pacsResult.OPERCODE = orur01.GetPATIENT_RESULT().GetORDER_OBSERVATION(i).OBR.GetTranscriptionist(0).Name.IDNumber.Value;//
             
            //OBX1
               pacsResult.DIAGID1 = orur01.GetPATIENT_RESULT().GetORDER_OBSERVATION(i).GetOBSERVATION(0).OBX.ObservationIdentifier.Text.Value;//检查所见
               pacsResult.DIAGNAME1 = ((NHapi.Base.Model.AbstractPrimitive)(orur01.GetPATIENT_RESULT().GetORDER_OBSERVATION().GetOBSERVATION().OBX.GetObservationValue()[0].Data)).Value; //影像所见内容


               pacsResult.DIAGID2 = orur01.GetPATIENT_RESULT().GetORDER_OBSERVATION(i).GetOBSERVATION(1).OBX.ObservationIdentifier.Text.Value; //检查提示
               pacsResult.DIAGNAME2 =  ((NHapi.Base.Model.AbstractPrimitive)(orur01.GetPATIENT_RESULT().GetORDER_OBSERVATION(i).GetOBSERVATION(1).OBX.GetObservationValue()[0].Data)).Value;// 检查提示内容
               pacsResult.User01 = orur01.GetPATIENT_RESULT().GetORDER_OBSERVATION(i).GetOBSERVATION(1).OBX.ResponsibleObserver.FamilyName.ToString();// 检查信息
               if (healthMgr.GetPacsResultInfo(pacsResult)== -1)
               {
                   errInfo = "插入Pacs表失败" + ReportManger.Err; 
                   return -1;
               }

               
           }

           return 1;
       }
    }
}
