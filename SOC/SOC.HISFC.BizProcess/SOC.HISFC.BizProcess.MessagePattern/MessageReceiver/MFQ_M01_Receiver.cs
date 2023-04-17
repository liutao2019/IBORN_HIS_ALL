using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.BizProcess.MessagePatternInterface;
using FS.HISFC.Models.Base;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.MessageReceiver
{
    public class MFQ_M01_Receiver : AbstractReceiver<NHapi.Model.V24.Message.MFQ_M01, NHapi.Model.V24.Message.MFR_M01>
    {
        private int processMessage(NHapi.Model.V24.Message.MFQ_M01 o, ref NHapi.Model.V24.Message.MFR_M01 ackMessage)
        {
            if (o == null)
            {
                this.Err = "消息内容为空！";
                return -1;
            }

            if (o.QRD.WhatSubjectFilterRepetitionsUsed <= 0)
            {
                this.Err = "消息MFQ^M01的QRD-9数据为空！";
                return -1;
            }


            FS.HISFC.Models.Base.Employee e = new Employee();
            e.ID = "T00001";
            e.Name = "自助终端机";
            e.UserCode = "99";
            FS.FrameWork.Management.Connection.Operator = e;


            string where= o.QRD.GetWhatSubjectFilter(0).Identifier.Value;

            if (where == "DPT")
            {
                return new FS.SOC.HISFC.BizProcess.MessagePattern.HL7.Query.MFQ_M01.Department().ProcessMessage(o, ref ackMessage,ref this.errInfo);
            }
            else if (where == "PLV")
            {
                return new FS.SOC.HISFC.BizProcess.MessagePattern.HL7.Query.MFQ_M01.RegLevel().ProcessMessage(o, ref ackMessage,ref this.errInfo);
            }
            else if (where == "APP")
            {
                return new FS.SOC.HISFC.BizProcess.MessagePattern.HL7.Query.MFQ_M01.DoctorSchema().ProcessMessage(o, ref ackMessage,ref this.errInfo);
            }
            else if (where == "Drug")
            {
                return new FS.SOC.HISFC.BizProcess.MessagePattern.HL7.Query.MFQ_M01.QueryDrug().ProcessMessage(o, ref ackMessage, ref this.errInfo);
            }
            else
            {
                this.Err = "未知的消息MFQ^M01的QRD-9：" + where;
                return -1;
            }

        }

        #region AbstractAcceptMessage<MFQ_M01,IMessage> 成员

        public override int ProcessMessage(NHapi.Model.V24.Message.MFQ_M01 o, ref NHapi.Model.V24.Message.MFR_M01 mfrM01)
        {
            mfrM01 = new NHapi.Model.V24.Message.MFR_M01();
            FS.HL7Message.V24.Function.GenerateMSH(mfrM01.MSH, o.MSH);

            if (this.processMessage(o, ref mfrM01) <= 0)
            {
                FS.HL7Message.V24.Function.GenerateErrorMSA(o.MSH, mfrM01.MSA, this.Err);
                mfrM01.MSA.TextMessage.Value = this.Err;
                mfrM01.MSA.ExpectedSequenceNumber.Value = "200";

                return -1;
            }
            else
            {
                FS.HL7Message.V24.Function.GenerateSuccessMSA(o.MSH, mfrM01.MSA);
                mfrM01.MSA.ExpectedSequenceNumber.Value = "100";
            }

            return 1;
        }

        #endregion

    }
}
