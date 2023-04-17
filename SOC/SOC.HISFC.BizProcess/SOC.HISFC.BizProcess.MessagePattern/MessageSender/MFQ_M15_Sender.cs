using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.HL7Message;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.MessageSender
{
    //查询库存消息
    public  class MFQ_M15_Sender:AbstractSender<FS.HISFC.Models.Pharmacy.ApplyOut ,NHapi.Model.V24.Message.MFQ_M15 ,NHapi.Model.V24.Message.MFR_M15,object>
    {
       
        protected override SendMessageType SendMessageType
        {
            get
            {
                return SendMessageType.DoSending;
            }
        }


        protected override int ConvertObjectToSendMessage(FS.HISFC.Models.Pharmacy.ApplyOut t, ref NHapi.Model.V24.Message.MFQ_M15 e, params object[] appendParams)
        {
            return new FS.SOC.HISFC.BizProcess.MessagePattern.HL7.Query.MFQ_M15.QueryDrugRowaStorage().ProcessMessage(t, ref e, ref errInfo);
        }

        protected override int ConvertResultMessageToObject(NHapi.Model.V24.Message.MFR_M15 m, ref object resultSingleInfo, params object[] appendParams)
        {
            FS.HISFC.Models.Pharmacy.ApplyOut ApplyOut = new FS.HISFC.Models.Pharmacy.ApplyOut();

            ApplyOut.ExtFlag = m.IIM.InventoryReceivedQuantity.Value;

            resultSingleInfo = ApplyOut;

            return base.ConvertResultMessageToObject(m, ref resultSingleInfo, appendParams);
        }
    }
}
