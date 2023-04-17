using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.BizProcess.MessagePatternInterface;
using FS.HISFC.Models.Base;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.MessageReceiver
{
    public class DFT_P03_Receiver : AbstractReceiver<NHapi.Model.V24.Message.DFT_P03, NHapi.Model.V24.Message.ACK>
    {
        private int processMessage(NHapi.Model.V24.Message.DFT_P03 o, ref NHapi.Model.V24.Message.ACK ackMessage)
        {
            FS.HISFC.Models.Base.Employee e = new Employee();
            e.ID = "T00001";
            e.Name = "自助终端机";
            e.UserCode = "99";
            FS.FrameWork.Management.Connection.Operator = e;

            if (o.PV1.PatientClass.Value.Equals("O"))
            {
                if (o.GetFINANCIAL(0).FT1.TransactionType.Value.Equals("RF"))//挂号缴费
                {
                    return new FS.SOC.HISFC.BizProcess.MessagePattern.HL7.FinancialManagement.DFT_P03.RegisterFee().ProcessMessage(o, ref ackMessage,ref this.errInfo);
                }
                else if (o.GetFINANCIAL(0).FT1.TransactionType.Value.Equals("PY"))//收费缴费
                {
                    return new FS.SOC.HISFC.BizProcess.MessagePattern.HL7.FinancialManagement.DFT_P03.RecipeFee().ProcessMessage(o, ref ackMessage, ref this.errInfo);
                }
            }
            return 1;
        }

        #region AbstractAcceptMessage<DFT_P03,IMessage> 成员

        public override int ProcessMessage(NHapi.Model.V24.Message.DFT_P03 o, ref NHapi.Model.V24.Message.ACK ACK_P03)
        {
            ACK_P03 = new NHapi.Model.V24.Message.ACK();
            ACK_P03.MSH.MessageType.TriggerEvent.Value = "P03";
            FS.HL7Message.V24.Function.GenerateMSH(ACK_P03.MSH, o.MSH);

            if (this.processMessage(o, ref ACK_P03) <= 0)
            {
                FS.HL7Message.V24.Function.GenerateErrorMSA(o.MSH, ACK_P03.MSA, this.Err);
                ACK_P03.MSA.TextMessage.Value = this.Err;
                ACK_P03.MSA.ExpectedSequenceNumber.Value = "200";
                return -1;
            }
            else
            {
                FS.HL7Message.V24.Function.GenerateSuccessMSA(o.MSH, ACK_P03.MSA);
                ACK_P03.MSA.ExpectedSequenceNumber.Value = "100";
                return 1;
            }
        }

        #endregion
    }
}
