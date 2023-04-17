using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBorn.SI.GuangZhou
{
    public abstract class AbstractService<T, E>
    {

        public abstract bool Transcation
        {
            get;
        }

        //public abstract string ConnectionString
        //{
        //    get;
        //}

        private string errorMsg = string.Empty;

        public string ErrorMsg
        {
            get
            {
                return errorMsg;
            }
            set
            {
                errorMsg = value;
            }
        }



        /// <summary>
        /// 调用接口服务
        /// </summary>
        /// <returns></returns>
        public int CallService(T sendObject, ref E reciverObject, params object[] appendParams)
        {
            //打开连接

            IBorn.SI.GuangZhou.Base.SIDataBase.Open(IBorn.SI.GuangZhou.Base.SIDataBase.SIServer);

            System.Data.DataTable dt = null;
            try
            {
                if (this.Transcation)
                {
                    IBorn.SI.GuangZhou.Base.SIDataBase.BeginTranscation();
                }

                //进行操作
                if (this.Excute(sendObject, ref dt, appendParams) <= 0)
                {
                    if (this.Transcation)
                    {
                        IBorn.SI.GuangZhou.Base.SIDataBase.Rollback();
                    }
                    return -1;
                }

                if (this.Transcation)
                {
                    IBorn.SI.GuangZhou.Base.SIDataBase.Commit();
                }
            }
            catch (Exception e)
            {
                IBorn.SI.GuangZhou.Base.SIDataBase.Rollback();
                this.errorMsg = "执行操作失败，原因：" + e.Message;
                return -1;
            }
            finally
            {
                //关闭连接
                IBorn.SI.GuangZhou.Base.SIDataBase.Close();
            }

            return this.GetResult(dt, sendObject, ref reciverObject, appendParams);
        }

        /// <summary>
        /// 执行操作
        /// </summary>
        /// <param name="m">结果消息</param>
        /// <param name="n">结果对象</param>
        /// <param name="errInfo">转换过程中的错误信息</param>
        /// <param name="appendParams">附件参数</param>
        /// <returns>-1 失败 1 成功</returns>
        protected abstract int Excute(T t, ref System.Data.DataTable dt, params object[] appendParams);

        /// <summary>
        /// 消息转换，将结果消息转换成对象
        /// </summary>
        /// <param name="m">结果消息</param>
        /// <param name="n">结果对象</param>
        /// <param name="errInfo">转换过程中的错误信息</param>
        /// <param name="appendParams">附件参数</param>
        /// <returns>-1 失败 1 成功</returns>
        protected virtual int GetResult(System.Data.DataTable dt, T t, ref E e, params object[] appendParams)
        {
            return 1;
        }

    }
}
