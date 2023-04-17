using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI
{
    /// <summary>
    /// {5C40227A-FA50-4fcb-A5E9-9B543D9D14FD}
    /// 回滚管理类 by chen.fch@20191231
    /// </summary>
    class RollbackManager
    {

        /// <summary>
        /// 回退链表，通过Add、Clear和Rollback来操作
        /// 1、医保中途业务处理完，需要将对应的回退添加到此链表中
        /// 2、Rollback方法中，按倒序把Request取出回退，并清空此链表。
        /// 3、正常业务退出时清空此链表
        /// 4、Commit方法中清空此链表
        /// </summary>
        private Stack<Models.Request.RequestBase> _rollbackList = new Stack<Models.Request.RequestBase>();

        /// <summary>
        /// 添加回滚业务
        /// </summary>
        /// <param name="request"></param>
        public void Add(Models.Request.RequestBase request)
        {
            //_rollbackList.Push(request);
        }

        /// <summary>
        /// 回滚事务
        /// </summary>
        public void Rollback()
        {
            //Models.Response.ResponseBase<Models.Response.ResponseGzsiModel1101> res = null;
            //string msg = string.Empty;
            //while (_rollbackList.Count > 0)
            //{
            //    Models.Request.RequestBase rb = _rollbackList.Pop();
            //    //rb.Call(out res, out  msg);
            //}
        }

        /// <summary>
        /// 清空回滚链表
        /// </summary>
        public void Clear()
        {
            //_rollbackList.Clear();
        }

    }
}
