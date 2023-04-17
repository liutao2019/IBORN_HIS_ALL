using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.BizProcess.MessagePatternInterface;
using System.Collections;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.HL7.Query.SQM_ZQ1
{
    public class QueryBookingConfirm
    {
        //获取预约已看诊
        public int ProcessMessage(NHapi.Model.V24.Message.SQM_ZQ1 sqmzq1,ref NHapi.Model.V24.Message.SQR_ZQ1[] sqrzq1,ref string errInfo)
        {
            
            ArrayList albooking = new ArrayList();
            FS.HISFC.BizLogic.Registration.Booking bookingMgr = new FS.HISFC.BizLogic.Registration.Booking();
            albooking = bookingMgr.Query(DateTime.ParseExact(sqmzq1.QRD.QueryDateTime.TimeOfAnEvent.Value, "yyyyMMddHHmmss", null));
            if (albooking == null)
            {
                errInfo = "获取预约订单确认信息失败，原因：" + bookingMgr.Err;
                return -1;
            }

            List<NHapi.Model.V24.Message.SQR_ZQ1> SQRList = new List<NHapi.Model.V24.Message.SQR_ZQ1>();
            ItemCodeMapManager mapMgr = new ItemCodeMapManager();
            foreach (FS.HISFC.Models.Registration.Booking booking in albooking)
            {
                NHapi.Model.V24.Message.SQR_ZQ1 SQRZQ1 = new NHapi.Model.V24.Message.SQR_ZQ1();

                //SQRZQ1
                SQRZQ1.MSH.MessageType.MessageType.Value = "SQR";
                SQRZQ1.MSH.MessageType.TriggerEvent.Value = "ZQ1";
                FS.HL7Message.V24.Function.GenerateMSH(SQRZQ1.MSH);

                SQRZQ1.MSA.MessageControlID.Value = sqmzq1.MSH.MessageControlID.Value;
                SQRZQ1.MSA.AcknowledgementCode.Value = "AA";
                FS.HL7Message.V24.Function.GenerateSuccessMSA(sqmzq1.MSH, SQRZQ1.MSA);
                //ARQ
                //取预约ID
                FS.FrameWork.Models.NeuObject obj = mapMgr.GetHL7Code(EnumItemCodeMap.RegisterBooking, new FS.FrameWork.Models.NeuObject(booking.ID, booking.ID, ""));
                if (obj == null||string.IsNullOrEmpty(obj.ID))
                {
                    errInfo = "获取预约订单确认信息失败，原因：获取订单对应的预约ID失败，" + mapMgr.Err + booking.ID;
                    return -1;
                }
                SQRZQ1.ARQ.PlacerAppointmentID.EntityIdentifier.Value = obj.ID;//预约流水号
                SQRZQ1.ARQ.AppointmentReason.Identifier.Value = "APPT_REG";

                //ZAI
                SQRZQ1.ZAI.YUYUE_STATE.Value = FS.FrameWork.Function.NConvert.ToInt32(booking.IsSee).ToString();
                SQRList.Add(SQRZQ1);

            }
            sqrzq1 = SQRList.ToArray<NHapi.Model.V24.Message.SQR_ZQ1>();
            return 1;
          
        
        }
    }
}
