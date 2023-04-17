using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.HISFC.BizProcess.Interface.Registration
{
    /// <summary>
    /// 挂号接口
    /// {0F383ECD-2CB9-40a7-8712-341C44BA56F0}
    /// </summary>
    public interface ISelfRegister
    {
        /// <summary>
        /// 保存挂号
        /// </summary>
        /// <param name="CardNO"></param>
        /// <param name="Name"></param>
        /// <param name="isPre"></param>
        /// <param name="DeptCode"></param>
        /// <param name="DoctCode"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <param name="reglevel"></param>
        /// <param name="payCode"></param>
        /// <param name="amount"></param>
        /// <param name="operCode"></param>
        /// <param name="regObj"></param>
        /// <param name="errText"></param>
        /// <returns></returns>
        int SaveRegister(string CardNO, string Name, bool isPre, string bookID, string DeptCode, string DoctCode, DateTime begin, DateTime end, string reglevel, string payCode, decimal amount, string operCode, ref FS.HISFC.Models.Registration.Register regObj, ref string errText);

        /// <summary>
        /// 保存开始时处理
        /// </summary>
        /// <param name="regObj"></param>
        /// <param name="errText"></param>
        /// <returns></returns>
        int SaveBegin(ref string errText);

        /// <summary>
        /// 保存结束时处理
        /// </summary>
        /// <param name="regObj"></param>
        /// <param name="errText"></param>
        /// <returns></returns>
        int SaveEnd(ref string errText);
    }
}
