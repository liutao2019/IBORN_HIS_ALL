using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.HL7Message;
using System.Collections;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.HL7.OrderEntry.OUL_R21
{
    /// <summary>
    /// 体检检验报告
    /// </summary>
    public class HealthCheckupInspectionReport
    {

        public int ProcessMessage(NHapi.Model.V24.Message.OUL_R21 oulr21, ref NHapi.Base.Model.IMessage ackMessage, ref string errInfo)
        {
            if (oulr21 == null)
            {
                return 1;
            }
            FS.HISFC.Models.Registration.Register regInfo = new FS.HISFC.Models.Registration.Register();
           // FS.HISFC.HealthCheckup.LISInterfceBizlogic LisResultMgr = new FS.HISFC.HealthCheckup.LISInterfceBizlogic();
            FS.LISChekupBizProcess.BizProcess LisResultMgr = new FS.LISChekupBizProcess.BizProcess();
            string clinicCode = oulr21.VISIT.PV1.VisitNumber.ID.Value; //门诊病历号
         
            if (regInfo == null)
            {
                errInfo = "获取患者信息";
                return -1;
            }
  
          

            for (int i = 0; i < oulr21.ORDER_OBSERVATIONRepetitionsUsed; i++)
            {
                ArrayList alListResult = new ArrayList(); 
                for (int j = 0; j < oulr21.GetORDER_OBSERVATION(i).OBSERVATIONRepetitionsUsed; j++)
                {
                    FS.LISChekupBizProcess.LisCheckupItem LisResult = new FS.LISChekupBizProcess.LisCheckupItem();

                    

                    //PID 
                    LisResult.ClinicCode = oulr21.VISIT.PV1.VisitNumber.ID.Value;// 就诊流水号
                    LisResult.CardNo = oulr21.PATIENT.PID.PatientID.ID.Value;//病历卡号

                    // OBR
                    LisResult.SequenceNO = oulr21.GetORDER_OBSERVATION(i).OBR.PlacerOrderNumber.EntityIdentifier.Value; //开单者医嘱号码
                   // LisResult.User01   = oulr21.GetORDER_OBSERVATION(i).OBR.FillerOrderNumber.EntityIdentifier.Value;//执行者医嘱号码
                    LisResult.HisItemCode = oulr21.GetORDER_OBSERVATION(i).OBR.UniversalServiceIdentifier.Identifier.Value;// HIS项目编码
                    LisResult.HisItemName = oulr21.GetORDER_OBSERVATION(i).OBR.UniversalServiceIdentifier.Text.Value;// HIS项目名称
                    LisResult.MachineCode = oulr21.GetORDER_OBSERVATION(i).OBR.UniversalServiceIdentifier.NameOfCodingSystem.Value;//项目类型 Ordinary：普通报告 Describe：描述报告  MIC：药敏报告 记录在仪器编码里
                    LisResult.AuditOperCode = oulr21.GetORDER_OBSERVATION(i).OBR.PrincipalResultInterpreter.Name.FamilyName.Value; //审核医生
                    if (oulr21.GetORDER_OBSERVATION(i).OBR.UniversalServiceIdentifier.NameOfCodingSystem.Value == "MTC")  //药敏报告
                    { 
                      //NTE
                       // LisResult.DiagName1 = oulr21.GetORDER_OBSERVATION(i)
                    
                    }

                    //OBX
                    LisResult.LisItemPrintSort = Convert.ToInt32(oulr21.GetORDER_OBSERVATION(i).GetOBSERVATION(j).OBX.SetIDOBX.Value); //打印顺序号u
                    if (oulr21.GetORDER_OBSERVATION(i).GetOBSERVATION().OBX.ValueType.Value == "NM") //数值
                    {
                        LisResult.ItemType = "0";// 定量

                        if (oulr21.GetORDER_OBSERVATION(i).GetOBSERVATION(j).OBX.AbnormalFlags.Value == null)
                        {
                            LisResult.Flag = "正常";
                        }
                        if (oulr21.GetORDER_OBSERVATION(i).GetOBSERVATION(j).OBX.AbnormalFlags.Value == "L")
                        {
                            LisResult.Flag = "低";
                        }
                        if (oulr21.GetORDER_OBSERVATION(i).GetOBSERVATION(j).OBX.AbnormalFlags.Value == "H")
                        {
                            LisResult.Flag = "高";
                        }
                    }
                    else
                    {
                        LisResult.ItemType = "1";  //定性
                        if (oulr21.GetORDER_OBSERVATION(i).GetOBSERVATION(j).OBX.AbnormalFlags.Value == "*")
                        {
                            LisResult.Flag = "异常";
                        }
                        if (oulr21.GetORDER_OBSERVATION(i).GetOBSERVATION(j).OBX.AbnormalFlags.Value == null)
                        {
                            LisResult.Flag = "正常";
                        }


                    }
                    LisResult.LisItemCode = oulr21.GetORDER_OBSERVATION(i).GetOBSERVATION(j).OBX.ObservationIdentifier.Identifier.Value;// LIS项目编码
                    LisResult.LisItemName = oulr21.GetORDER_OBSERVATION(i).GetOBSERVATION(j).OBX.ObservationIdentifier.Text.Value;//LIS项目名称
                    LisResult.LisItemResult = oulr21.GetORDER_OBSERVATION(i).GetOBSERVATION(j).OBX.GetObservationValue()[0].Data.ToString();//值
                    LisResult.Unit = oulr21.GetORDER_OBSERVATION(i).GetOBSERVATION(j).OBX.Units.Identifier.Value;//单位
                   
                    LisResult.Range = oulr21.GetORDER_OBSERVATION(i).GetOBSERVATION(j).OBX.ReferencesRange.Value;//参考值
                    //if (oulr21.GetORDER_OBSERVATION(i).GetOBSERVATION(j).OBX.ReferencesRange.Value != null )
                    //{
                    //    if (oulr21.GetORDER_OBSERVATION(i).GetOBSERVATION(j).OBX.ReferencesRange.Value.Substring(0, 1) == ">")
                    //    {
                    //        LisResult.DownValue = Convert.ToDecimal(oulr21.GetORDER_OBSERVATION(i).GetOBSERVATION(j).OBX.ReferencesRange.Value.Split('>')[1]);// 参考值下限
                    //        //LisResult.UpValue = Convert.ToDecimal(oulr21.GetORDER_OBSERVATION(i).GetOBSERVATION(j).OBX.ReferencesRange.Value.Split('>')[1]);// 参考值上限
                    //    }
                    //    else
                    //    {
                    //        LisResult.DownValue = Convert.ToDecimal(oulr21.GetORDER_OBSERVATION(i).GetOBSERVATION(j).OBX.ReferencesRange.Value.Split('-')[0]);// 参考值下限
                    //        LisResult.UpValue = Convert.ToDecimal(oulr21.GetORDER_OBSERVATION(i).GetOBSERVATION(j).OBX.ReferencesRange.Value.Split('-')[1]);// 参考值上限
                    //    }
                    //}
                  //  LisResult.AbnormalFlag= oulr21.GetORDER_OBSERVATION(i).GetOBSERVATION(j).OBX.ObservationResultStatus.Value;//异常标记
                   // LisResult.u = oulr21.GetORDER_OBSERVATION(i).GetOBSERVATION(j).OBX.ObservationResultStatus.Value;//结果状态
                    LisResult.AuditOperCode2 = oulr21.GetORDER_OBSERVATION(i).OBR.GetAssistantResultInterpreter(0).Name.FamilyName.Value;
                    LisResult.AuditDate = DateTime.ParseExact(oulr21.GetORDER_OBSERVATION(i).GetOBSERVATION(j).OBX.DateTimeOfTheObservation.TimeOfAnEvent.Value, "yyyyMMddHHmmss",null);//
                    
                    alListResult.Add(LisResult);
                }

                if (LisResultMgr.InsertLisResult(alListResult) == -1)
                {
                    errInfo = "保存体检结果数据失败";
                }
                //if(LisResultMgr.QueryLisResultCount(oulr21.VISIT.PV1.VisitNumber.ID.Value,oulr21.GetORDER_OBSERVATION().OBR.PlacerOrderNumber.EntityIdentifier.Value) >=1)
                //{
                //   if(LisResultMgr.DeleteLisResult(oulr21.VISIT.PV1.VisitNumber.ID.Value,oulr21.GetORDER_OBSERVATION().OBR.PlacerOrderNumber.EntityIdentifier.Value)==-1)
                //   {
                //       errInfo = "删除体检结果数据失败"+ LisResultMgr.Err;

                //       return -1;
                //   }
                //}
                //else
                //{
                //    if (LisResultMgr.InsertChkLisResult(alListResult) == -1)
                //    {
                //        errInfo = "插入体检结果失败" + LisResultMgr.Err;
                //        return -1;
                //    }
                //}
              
            }

         
            return 1;
                  
        }
    }
}
