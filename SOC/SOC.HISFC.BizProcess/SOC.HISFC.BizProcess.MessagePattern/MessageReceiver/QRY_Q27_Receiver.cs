using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.BizProcess.MessagePatternInterface;
using FS.HISFC.Models.Base;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.MessageReceiver
{
    public class QRY_Q27_Receiver :AbstractReceiver<NHapi.Model.V24.Message.QRY_Q27, NHapi.Base.Model.IMessage>
    {
        private int processMessage(NHapi.Model.V24.Message.QRY_Q27 o, ref NHapi.Model.V24.Message.RAR_RAR ackMessage)
        {
            if (o == null)
            {
                this.Err = "消息内容为空！";
                return -1;
            }

            if (o.QRD.WhatSubjectFilterRepetitionsUsed <= 0)
            {
                this.Err = "消息QRY_Q01的QRD-9数据为空！";
                return -1;
            }

            FS.HISFC.Models.Base.Employee e = new Employee();
            e.ID = "T00001";
            e.Name = "自助终端机";
            e.UserCode = "99";
            FS.FrameWork.Management.Connection.Operator = e;

            string where = o.QRD.GetWhatSubjectFilter(0).Identifier.Value;

            if (where == "BIL")
            {
                return new FS.SOC.HISFC.BizProcess.MessagePattern.HL7.Query.QRY_Q27.QueryRecipe().ProcessMessage(o,ref ackMessage);
            }
            else
            {
                this.Err = "未知的消息QRY_Q01的QRD-9：" + where;
                return -1;
            }

            return 1;
        }

        #region IAcceptMessage<QRY_Q27,IMessage> 成员

        public override int ProcessMessage(NHapi.Model.V24.Message.QRY_Q27 o, ref NHapi.Base.Model.IMessage ackMessage)
        {
            NHapi.Model.V24.Message.RAR_RAR RARRAR = new NHapi.Model.V24.Message.RAR_RAR();
            ackMessage = RARRAR;
            FS.HL7Message.V24.Function.GenerateMSH(RARRAR.MSH, o.MSH);
            if (this.processMessage(o, ref RARRAR) <= 0)
            {
                FS.HL7Message.V24.Function.GenerateErrorMSA(o.MSH, RARRAR.MSA, this.Err);
                RARRAR.MSA.TextMessage.Value = this.Err;
                RARRAR.MSA.ExpectedSequenceNumber.Value = "200";

                return -1;
            }
            else
            {
                FS.HL7Message.V24.Function.GenerateSuccessMSA(o.MSH, RARRAR.MSA);
                RARRAR.MSA.ExpectedSequenceNumber.Value = "100";

            }

            return 1;
        }

        #endregion
    }
}
