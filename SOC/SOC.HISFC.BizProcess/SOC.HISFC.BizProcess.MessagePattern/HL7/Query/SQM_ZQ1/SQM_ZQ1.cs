using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Neusoft.SOC.HISFC.BizProcess.MessagePatternInterface;
using System.Collections;

namespace Neusoft.SOC.HISFC.BizProcess.MessagePattern.HL7.Query.SQM_ZQ1
{
    public class SQM_ZQ1
    {
        //获取预约已看诊
        public int ProcessMessage(NHapi.Model.V24.Message.SQM_ZQ1 sqmzq1,ref NHapi.Model.V24.Message.SQR_ZQ1[] sqrzq1,ref string errInfo)
        {
            
            ArrayList albooking = new ArrayList();
            Neusoft.HISFC.BizLogic.Registration.Booking bookingMgr = new Neusoft.HISFC.BizLogic.Registration.Booking();
            albooking = bookingMgr.Query(DateTime.ParseExact(sqmzq1.QRD.QueryDateTime.TimeOfAnEvent.Value, "yyyy-mm-dd hh:mi:ss", null));
            if (albooking == null)
            {
                return -1;
            }
            List<NHapi.Model.V24.Message.SQR_ZQ1> SQRList = new List<NHapi.Model.V24.Message.SQR_ZQ1>();
            foreach (Neusoft.HISFC.Models.Registration.Booking booking in albooking)
            {
                NHapi.Model.V24.Message.SQR_ZQ1 SQRZQ1 = new NHapi.Model.V24.Message.SQR_ZQ1();

                //SQRZQ1
                SQRZQ1.MSH.MessageType.MessageType.Value = "SQR";
                SQRZQ1.MSH.MessageType.TriggerEvent.Value = "ZQ1";
                Neusoft.HL7Message.V24.Function.GenerateMSH(SQRZQ1.MSH);
                //ARQ
                SQRZQ1.ARQ.PlacerAppointmentID.EntityIdentifier.Value = booking.ID; //预约流水号
                SQRZQ1.ARQ.AppointmentReason.Identifier.Value = "APPT_REG";

                //ZAI
                SQRZQ1.ZAI.YUYUE_STATE.Value = Neusoft.FrameWork.Function.NConvert.ToInt32(booking.IsSee).ToString();
                SQRList.Add(SQRZQ1);

            }
            sqrzq1 = SQRList.ToArray<NHapi.Model.V24.Message.SQR_ZQ1>();
            return 1;
          
        
        }
    }
}
