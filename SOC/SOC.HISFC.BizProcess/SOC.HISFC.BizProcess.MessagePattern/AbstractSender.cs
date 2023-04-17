using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using FS.SOC.HISFC.BizProcess.MessagePatternInterface;

namespace FS.SOC.HISFC.BizProcess.MessagePattern
{
    /// <summary>
    /// [功能描述: 业务消息发送模式：主要是根据消息类型调用不同的抽象类，并且返回确认消息]
    /// [T：发送数据对象
    ///  E：转换后发送消息格式
    ///  M：确认结果消息格式
    ///  N：确认结果数据对象]
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2011-12]<br></br>
    /// <typeparam name="T">需要处理的消息</typeparam>
    /// <typeparam name="E">返回的消息</typeparam>
    /// </summary>
    public abstract class AbstractSender
        <T,E,M,N>:ISender
    {
        #region ISender 成员

        public int Send(System.Collections.ArrayList alInfo, EnumOperType operType, EnumInfoType infoType, ref string errInfo)
        {
           Object o=null;
           return this.Send(alInfo, ref o, ref errInfo, operType, infoType);
        }

        /// <summary>
        ///  消息发送
        /// </summary>
        /// <param name="singleInfo"></param>
        /// <param name="resultSingleInfo"></param>
        /// <param name="errInfo"></param>
        /// <returns></returns>
        public int Send(object singleInfo, ref object resultSingleInfo, ref string errInfo, params object[] appendParams)
        {
            E e = default(E);
            M m = default(M);
            N n = default(N);
            //消息进行转换
            int i = this.ConvertObjectToSendMessage((T)singleInfo, ref e, appendParams);
            if (i == -1)
            {
                errInfo = this.errInfo;
                return -1;
            }

            NHapi.Base.Model.IMessage[] resultMessage = m as NHapi.Base.Model.IMessage[];
            NHapi.Base.Model.IMessage[] messages = null;
            //发送消息
            if (e.GetType().IsArray)
            {
                messages = e as NHapi.Base.Model.IMessage[];
            }
            else
            {
                messages = new NHapi.Base.Model.IMessage[1];
                messages[0] = e as NHapi.Base.Model.IMessage;
            }

            i = FS.HL7Message.ProcessFactory.ProcessSender(messages, this.SendMessageType, ref resultMessage, ref errInfo);

            if (i == -1)
            {
                errInfo = this.errInfo;
                return -1;
            }

            if (resultMessage != null && resultMessage.Length != 0)
            {
                if (typeof(M).IsArray)
                {
                    m = (M)((object)resultMessage);
                }
                else
                {
                    m = (M)resultMessage[0];
                }
            }
            if (m != null)
            {
                //处理结果消息
                i = this.ConvertResultMessageToObject(m, ref n, appendParams);
                resultSingleInfo = n;
                errInfo = this.errInfo;
            }

            return i;
        }

        #endregion

        /// <summary>
        /// 消息转换，将对象转换成发送消息（从T转换到E）
        /// </summary>
        /// <param name="t">原始对象</param>
        /// <param name="e">转换后消息</param>
        /// <param name="errInfo">转换过程中的错误信息</param>
        /// <param name="appendParams">附加参数</param>
        /// <returns>-1 失败 1 成功</returns>
        protected abstract int ConvertObjectToSendMessage(T t, ref E e, params object[] appendParams);

        /// <summary>
        /// 消息转换，将结果消息转换成对象（从M转换到N）
        /// </summary>
        /// <param name="m">结果消息</param>
        /// <param name="n">结果对象</param>
        /// <param name="errInfo">转换过程中的错误信息</param>
        /// <param name="appendParams">附件参数</param>
        /// <returns>-1 失败 1 成功</returns>
        protected virtual int ConvertResultMessageToObject(M m, ref N resultSingleInfo, params object[] appendParams)
        {
            return 1;
        }


        protected virtual FS.HL7Message.SendMessageType SendMessageType
        {
            get
            {
                return FS.HL7Message.SendMessageType.DoSended;
            }

        }

        #region IMessage 成员

        protected string errInfo = string.Empty;
        public string Err
        {
            get
            {
                return this.errInfo;
            }
            set
            {
                this.errInfo = value;
            }
        }

        #endregion
    }
}
