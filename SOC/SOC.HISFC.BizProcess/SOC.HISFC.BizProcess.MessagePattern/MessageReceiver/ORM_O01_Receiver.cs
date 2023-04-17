using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.MessageReceiver
{
    public class ORM_O01_Receiver : AbstractReceiver<NHapi.Model.V24.Message.ORM_O01, NHapi.Base.Model.IMessage>
    {
        public override int ProcessMessage(NHapi.Model.V24.Message.ORM_O01 ormO01, ref NHapi.Base.Model.IMessage ackMessage)
        {
            //门诊手术申请
            string patientType = ormO01.PATIENT.PATIENT_VISIT.PV1.PatientClass.Value;
            string orcType = ormO01.GetORDER(0).ORC.OrderControl.Value;
            if (patientType.Equals("O"))
            {
                return new FS.SOC.HISFC.BizProcess.MessagePattern.HL7.OrderEntry.ORM_O01.OutpatientOperationApply().ProcessMessage(ormO01, ref ackMessage, ref this.errInfo);
            }
            else if (patientType.Equals("I"))
            {
                //取消手术申请
                if (orcType.Equals("CA"))
                {
                    return new FS.SOC.HISFC.BizProcess.MessagePattern.HL7.OrderEntry.ORM_O01.InpatientOperationTerminalCancelConfirm().ProcessMessage(ormO01, ref ackMessage, ref this.errInfo);
                }
                else
                {
                    this.Err = "暂时不处理住院手术申请";
                    return 1;
                }
            }

            return 1;
        }
    }
}
