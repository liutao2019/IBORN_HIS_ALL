using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.BizProcess.MessagePatternInterface;
using System.Collections;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.HL7.Query.MFN_M15
{
    class QueryRowaDrug
    {
        private int processMessage(NHapi.Model.V24.Message.MFN_M15 o, ref NHapi.Model.V24.Message.MFK_M15 ackMessage, ref string errInfo)
        {
            if (o == null)
            {
                errInfo = "查询消息有问题，请核对消息！";
            }

            FS.HISFC.Models.Pharmacy.Item RowaItem = new FS.HISFC.Models.Pharmacy.Item();
             
            FS.SOC.HISFC.BizProcess.MessagePattern.BizLogic.DrugItem itemMgr = new FS.SOC.HISFC.BizProcess.MessagePattern.BizLogic.DrugItem();

            RowaItem = itemMgr.GetItemForRowa(o.GetMF_INV_ITEM().MFE.GetPrimaryKeyValueMFE()[0].Data.ToString());
             NHapi.Model.V24.Message.MFK_M15 mfkm15 = new NHapi.Model.V24.Message.MFK_M15();
            if (RowaItem == null)
            {
               
                mfkm15.MSH.MessageType.MessageType.Value = "MFK";
                mfkm15.MSH.MessageType.TriggerEvent.Value = "M15";
                FS.HL7Message.V24.Function.GenerateMSH(mfkm15.MSH);

                mfkm15.MSA.MessageControlID.Value = o.MSH.MessageControlID.Value;
                mfkm15.MSA.AcknowledgementCode.Value = "AA";
              
                FS.HL7Message.V24.Function.GenerateSuccessMSA(o.MSH, mfkm15.MSA);

                mfkm15.MSA.ExpectedSequenceNumber.Value = "1";  //不可以装药品

                NHapi.Model.V24.Segment.MFA MFA = mfkm15.GetMFA(0);

                MFA.GetPrimaryKeyValueMFA(0).Identifier.Value = o.GetMF_INV_ITEM().MFE.GetPrimaryKeyValueMFE()[0].Data.ToString();
            

            }
            else
            {
               
                mfkm15.MSH.MessageType.MessageType.Value = "MFK";
                mfkm15.MSH.MessageType.TriggerEvent.Value = "M15";
                FS.HL7Message.V24.Function.GenerateMSH(mfkm15.MSH);

                mfkm15.MSA.MessageControlID.Value = o.MSH.MessageControlID.Value;
                mfkm15.MSA.AcknowledgementCode.Value = "AA";
             
                FS.HL7Message.V24.Function.GenerateSuccessMSA(o.MSH, mfkm15.MSA);
                mfkm15.MSA.ExpectedSequenceNumber.Value = "0";//可以装药品

                NHapi.Model.V24.Segment.MFA MFA = mfkm15.GetMFA(0);

                MFA.GetPrimaryKeyValueMFA(0).Identifier.Value = o.GetMF_INV_ITEM().MFE.GetPrimaryKeyValueMFE()[0].Data.ToString();
            
            }
            ackMessage = mfkm15; 

            return 1;
        }

        public int ProcessMessage(NHapi.Model.V24.Message.MFN_M15 o, ref NHapi.Model.V24.Message.MFK_M15 ackMessage, ref string errInfo)
        {
            return this.processMessage(o, ref ackMessage, ref errInfo);
        }
    }
}
