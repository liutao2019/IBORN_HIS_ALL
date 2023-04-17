using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.BizProcess.MessagePatternInterface;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.HL7
{
    public  class ReceiveMessage:FS.HL7Message.Interface.IReceiveMessage
    {
        #region IReceiveMessage 成员

        public int ProcessMessage(NHapi.Base.Model.IMessage[] o, ref NHapi.Base.Model.IMessage[] ackMessage, ref string error)
        {
            List<NHapi.Base.Model.IMessage> list = new List<NHapi.Base.Model.IMessage>();
            int ret = -1;
            

            for (int i = 0; i < o.Length; i++)
            {
                
                    FS.FrameWork.Management.PublicTrans.BeginTransaction();
                    IReceiver ireceiver = CommonInterface.ControllerFactroy.CreateFactory().CreateInferface<IReceiver>(o[i].GetType(), null);
                    if (ireceiver == null)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        error = "处理消息失败，请检查是否配置" + o[i].GetType();
                        return -1;
                    }
                    try
                    {

                    Object ack = new object();
                    ret = ireceiver.Receive(o[i], ref ack, ref error);
                    if (ack is NHapi.Base.Model.IMessage)
                    {
                        list.Add(ack as NHapi.Base.Model.IMessage);
                    }
                    else if (ack is NHapi.Base.Model.IMessage[])
                    {
                        list.AddRange(ack as NHapi.Base.Model.IMessage[]);
                    }
                    FS.FrameWork.Management.PublicTrans.Commit();
                }
                catch(Exception e)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    Object ack = new object();
                    ret = ireceiver.Receive(o[i], ref ack, ref error);
                    if (ack is NHapi.Base.Model.IMessage)
                    {
                        list.Add(ack as NHapi.Base.Model.IMessage);
                    }
                    else if (ack is NHapi.Base.Model.IMessage[])
                    {
                        list.AddRange(ack as NHapi.Base.Model.IMessage[]);
                    }
                }
            }

            ackMessage = list.ToArray();

            return ret;
        }

        #endregion
    }
}
