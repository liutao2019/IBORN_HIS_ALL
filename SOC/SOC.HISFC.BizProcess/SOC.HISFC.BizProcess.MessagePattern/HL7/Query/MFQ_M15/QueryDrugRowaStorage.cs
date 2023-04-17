using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.HL7.Query.MFQ_M15
{
    public class QueryDrugRowaStorage
    {
        //查询药品库存
        public int ProcessMessage(FS.HISFC.Models.Pharmacy.ApplyOut applyout , ref NHapi.Model.V24.Message.MFQ_M15 mfqm15, ref string errInfo)
        {
            if (applyout == null)
            {
                errInfo = errInfo + "未有需要查询的药品明细，请核对！";
                return -1;
            }
          
            //库存消息
            mfqm15 = new NHapi.Model.V24.Message.MFQ_M15();
            mfqm15.MSH.MessageType.MessageType.Value = "MFQ";
            mfqm15.MSH.MessageType.TriggerEvent.Value = "M15";
            FS.HL7Message.V24.Function.GenerateMSH(mfqm15.MSH);

            //drugMfqm01.QRD.QueryDateTime.TimeOfAnEvent.Value = DateTime.ParseExact(applyout.ApplyDate, "yyyyMMddHHmmss", null);
            mfqm15.QRD.QueryID.Value = applyout.Item.ID;
          
            mfqm15.QRD.GetWhatDataCodeValueQual(0).FirstDataCodeValue.Value = applyout.Item.Product.BarCode;
         
          

            return 1;

        }
    }
}
