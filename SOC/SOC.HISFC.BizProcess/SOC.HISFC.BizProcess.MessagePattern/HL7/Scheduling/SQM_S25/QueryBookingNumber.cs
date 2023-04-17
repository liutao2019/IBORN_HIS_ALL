using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.HL7.Scheduling.SQM_S25
{
    /// <summary>
    /// 查询预约挂号
    /// </summary>
    public class QueryBookingNumber 
    {
        public int ProcessMessage(FS.HISFC.Models.Registration.Schema Schema ,ref NHapi.Model.V24.Message.SQM_S25 SQMS25,ref string errInfo )
        {
            if (Schema == null)
            {
                errInfo = "没有修改排班信息，请选择排班信息";
                return 1;
            }

          //  NHapi.Model.V24.Message.SQM_S25 sqms25 = new NHapi.Model.V24.Message.SQM_S25();
            //SQMS25
            SQMS25 = new NHapi.Model.V24.Message.SQM_S25();
            SQMS25.MSH.MessageType.MessageType.Value = "SQM";
            SQMS25.MSH.MessageType.TriggerEvent.Value = "S25";
            FS.HL7Message.V24.Function.GenerateMSH(SQMS25.MSH);

            //ARQ 排班编码
            SQMS25.REQUEST.ARQ.ScheduleID.Identifier.Value = Schema.Templet.ID;
            
            //List<NHapi.Model.V24.Message.SQM_S25> schemaMessage = new List<NHapi.Model.V24.Message.SQM_S25>();
            ////Dictionary<string, NHapi.Model.V24.Message.SQM_S25> schemaMessage = new Dictionary<string, NHapi.Model.V24.Message.SQM_S25>();
            //foreach (FS.HISFC.Models.Registration.Schema schema in alSchema)
            //{
            //    NHapi.Model.V24.Message.SQM_S25 sqms25 = new NHapi.Model.V24.Message.SQM_S25();
            
            //    //SQMS25
            //    sqms25.MSH.MessageType.MessageType.Value = "SQM";
            //    sqms25.MSH.MessageType.TriggerEvent.Value = "S25";
            //    FS.HL7Message.V24.Function.GenerateMSH(sqms25.MSH);

            //    //ARQ 排班编码
            //    sqms25.REQUEST.ARQ.ScheduleID.Identifier.Value = schema.Templet.ID;
            //    schemaMessage.Add(sqms25);
               
            
            //}
            //SQMS25 = schemaMessage.ToArray<NHapi.Model.V24.Message.SQM_S25>();
           // sqms25 = SQMS25;
            return 1;
        
        }

    }
}
