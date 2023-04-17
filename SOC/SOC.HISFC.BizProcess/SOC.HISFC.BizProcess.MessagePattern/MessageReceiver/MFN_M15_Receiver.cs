using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.MessageReceiver
{
     public class MFN_M15_Receiver:AbstractReceiver<NHapi.Model.V24.Message.MFN_M15, NHapi.Model.V24.Message.MFK_M15>
    {
         #region AbstractAcceptMessage 成员

         public override int ProcessMessage(NHapi.Model.V24.Message.MFN_M15 o, ref NHapi.Model.V24.Message.MFK_M15 ackMessage)
         {
             if (o is NHapi.Model.V24.Message.MFN_M15)
             {
                 NHapi.Model.V24.Message.MFN_M15 MFN = o as NHapi.Model.V24.Message.MFN_M15;
                 if (MFN.MFI.MasterFileIdentifier.Identifier.Value == "DMI")
                 {
                     HL7.Query.MFN_M15.QueryRowaDrug QueryMgr = new FS.SOC.HISFC.BizProcess.MessagePattern.HL7.Query.MFN_M15.QueryRowaDrug();

                     return QueryMgr.ProcessMessage(MFN,ref ackMessage, ref this.errInfo);
                 }
             
             }
             else
             {
                 this.errInfo = "目前没有实现：" + o.GetType() + "类型的HL7消息处理";
                 return -1;
             }

             return 1;
         }

         #endregion
    }
}
