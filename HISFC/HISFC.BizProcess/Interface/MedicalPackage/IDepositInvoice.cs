using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.HISFC.BizProcess.Interface.MedicalPackage
{
    public interface IDepositInvoice
    {
        ///<summary>
        ///数据库连接
        ///</summary>
        System.Data.IDbTransaction Trans
        {
            get;
            set;
        }

        /// <summary>
        /// 填值
        /// </summary>
        /// <param name="InvoiceNO">发票号</param>
        /// <returns></returns>
        int SetPrintValue(System.Collections.ArrayList InvoiceList);

        /// <summary>
        /// 打印预览
        /// </summary>
        /// <returns>>成功 1 失败 -1</returns>
        int PrintView();
        /// <summary>
        /// 打印
        /// </summary>
        /// <returns>成功 1 失败 -1</returns>

        int Print();

        /// <summary>
        /// 清空当前信息
        /// </summary>
        /// <returns>成功 1 失败 -1</returns>
        int Clear();

        /// <summary>
        /// 设置本地数据库连接
        /// </summary>
        /// <param name="trans">数据库连接</param>
        void SetTrans(System.Data.IDbTransaction trans);
    }
}
