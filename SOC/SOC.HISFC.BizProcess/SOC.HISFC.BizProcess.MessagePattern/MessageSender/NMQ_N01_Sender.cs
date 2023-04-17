using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.HL7Message;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.MessageSender
{
    //查询库存消息
    public class NMQ_N01_Sender : AbstractSender<FS.HISFC.Models.Pharmacy.ApplyOut, NHapi.Model.V24.Message.NMQ_N01, NHapi.Model.V24.Message.NMR_N01, object>
    {

        protected override SendMessageType SendMessageType
        {
            get
            {
                return SendMessageType.DoSending;
            }
        }


        protected override int ConvertObjectToSendMessage(FS.HISFC.Models.Pharmacy.ApplyOut t, ref NHapi.Model.V24.Message.NMQ_N01 e, params object[] appendParams)
        {
            return new FS.SOC.HISFC.BizProcess.MessagePattern.HL7.Query.NMQ_N01.QueryRowaMachineState().ProcessMessage(t, ref e, ref errInfo);
        }

        protected override int ConvertResultMessageToObject(NHapi.Model.V24.Message.NMR_N01 m, ref object resultSingleInfo, params object[] appendParams)
        {
            FS.HISFC.Models.Pharmacy.ApplyOut ApplyOut = new FS.HISFC.Models.Pharmacy.ApplyOut();

            ApplyOut.State = m.GetCLOCK_AND_STATS_WITH_NOTES_ALT().NSC.ApplicationChangeType.Value.ToString();
            if (ApplyOut.State.ToString() == "0")
                ApplyOut.User01 = "准备好";
            else if (ApplyOut.State == "1")
                ApplyOut.User01 = "没有准备好";
            else if (ApplyOut.State == "2")
                ApplyOut.User01 = "部件损坏";
            else if (ApplyOut.State == "6")
                ApplyOut.User01 = "队列已满";

            resultSingleInfo = ApplyOut;

            return base.ConvertResultMessageToObject(m, ref resultSingleInfo, appendParams);
        }
    }
}
