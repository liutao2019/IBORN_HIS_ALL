using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.BizProcess.MessagePatternInterface;

namespace FS.SOC.HISFC.BizProcess.MessagePattern
{
    /// <summary>
    /// [功能描述: 业务消息模式：主要是根据消息类型调用不同的抽象类，并且返回确认消息]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2011-12]<br></br>
    /// <typeparam name="T">需要处理的消息</typeparam>
    /// <typeparam name="E">返回的消息</typeparam>
    /// </summary>
    public abstract class AbstractReceiver<T, E> : IReceiver
    {
        /// <summary>
        /// 处理接收消息
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public abstract int ProcessMessage(T o, ref E ackMessage);

        #region IReceiver 成员

        public int Receive(object o, ref object ackMessage,ref string errInfo)
        {
            E e = default(E);
            if (ackMessage is E)
            {
                e = (E)ackMessage;
            }

            //赋值操作员
            FS.HISFC.Models.Base.Employee employee = new FS.HISFC.Models.Base.Employee();
            employee.ID = "T00001";
            FS.FrameWork.Management.Connection.Operator = employee;

            int i = this.ProcessMessage((T)o, ref e);
            errInfo = this.errInfo;
            ackMessage = e;

            return i;
        }

        #endregion

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
