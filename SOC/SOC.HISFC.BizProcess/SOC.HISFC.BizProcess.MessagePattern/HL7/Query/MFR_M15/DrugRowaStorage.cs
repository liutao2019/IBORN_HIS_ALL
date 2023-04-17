using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.HL7.Query.MFR_M15
{
    public class DrugRowaStorage
    {
        internal int ProcessMessage(NHapi.Model.V24.Message.MFR_M15 mfrm15, ref object resultSingleInfo, ref string errInfo)
        {
            FS.HISFC.Models.Pharmacy.ApplyOut applyout = new FS.HISFC.Models.Pharmacy.ApplyOut();
            if (mfrm15 == null)
            {
                errInfo = errInfo + "未有发药机查询确认消息！请核对！";
                return 1;
            }

            applyout.Item.ID = mfrm15.QRD.QueryID.Value;
            applyout.Item.Product.BarCode = mfrm15.IIM.ServiceItemCode.Text.Value;
            applyout.Item.Qty = FS.FrameWork.Function.NConvert.ToDecimal(mfrm15.IIM.InventoryReceivedQuantity.Value);
            applyout.ExtFlag = mfrm15.IIM.InventoryReceivedQuantity.Value;

            resultSingleInfo = applyout;
            return 1;
        
        }
    }
}
