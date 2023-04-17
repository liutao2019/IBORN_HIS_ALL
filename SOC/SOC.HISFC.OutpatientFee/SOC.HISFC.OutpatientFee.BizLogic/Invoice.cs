using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neusoft.SOC.HISFC.OutpatientFee.BizLogic
{
    /// <summary>
    /// [功能描述: 门诊发票管理类]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2012-5]<br></br>
    /// </summary>
    public class Invoice : Neusoft.FrameWork.Management.Database
    {
        /// <summary>
        /// 更新发票主表日结标记
        /// </summary>
        /// <param name="beginTime">日结开始时间</param>
        /// <param name="endTime">日结结束时间</param>
        /// <param name="balanceFlag">日结标记</param>
        /// <param name="balanceNO">日结序号</param>
        /// <param name="balanceDate">日结时间</param>
        /// <returns> >=1成功, 0 没有找到更新的行， -1 失败</returns>
        public int UpdateForDayBalance(DateTime beginTime, DateTime endTime,string operID,string balanceNO)
        {
            return this.ExecNoQuery(string.Format(Neusoft.SOC.HISFC.OutpatientFee.Data.AbstractInvoice.Current.UpdateForDayBalance, beginTime.ToString("yyyy-MM-dd HH:mm:ss"), endTime.ToString("yyyy-MM-dd HH:mm:ss"), operID,  balanceNO));
        }

        /// <summary>
        /// 更新发票主表日结标记
        /// </summary>
        /// <param name="beginTime">日结开始时间</param>
        /// <param name="endTime">日结结束时间</param>
        /// <param name="balanceFlag">日结标记</param>
        /// <param name="balanceNO">日结序号</param>
        /// <param name="balanceDate">日结时间</param>
        /// <returns> >=1成功, 0 没有找到更新的行， -1 失败</returns>
        public int UpdateForCancelDayBalance(string operID, string balanceNO)
        {
            return this.ExecNoQuery(string.Format(Neusoft.SOC.HISFC.OutpatientFee.Data.AbstractInvoice.Current.UpdateForCancelDayBalance, balanceNO, operID));
        }

        /// <summary>
        /// 更新发票明细表日结标记
        /// </summary>
        /// <param name="beginTime">日结开始时间</param>
        /// <param name="endTime">日结结束时间</param>
        /// <param name="balanceFlag">日结标记</param>
        /// <param name="balanceNO">日结序号</param>
        /// <param name="balanceDate">日结时间</param>
        /// <returns> >=1成功, 0 没有找到更新的行， -1 失败</returns>
        public int UpdateDetailForDayBalance(DateTime beginTime, DateTime endTime, string operID, string balanceNO)
        {
            return this.ExecNoQuery(string.Format(Neusoft.SOC.HISFC.OutpatientFee.Data.AbstractInvoice.Current.UpdateDetailForDayBalance, beginTime.ToString("yyyy-MM-dd HH:mm:ss"), endTime.ToString("yyyy-MM-dd HH:mm:ss"), operID, balanceNO));
        }


        /// <summary>
        /// 更新发票明细表日结标记
        /// </summary>
        /// <param name="beginTime">日结开始时间</param>
        /// <param name="endTime">日结结束时间</param>
        /// <param name="balanceFlag">日结标记</param>
        /// <param name="balanceNO">日结序号</param>
        /// <param name="balanceDate">日结时间</param>
        /// <returns> >=1成功, 0 没有找到更新的行， -1 失败</returns>
        public int UpdateDetailForCancelDayBalance(string operID, string balanceNO)
        {
            return this.ExecNoQuery(string.Format(Neusoft.SOC.HISFC.OutpatientFee.Data.AbstractInvoice.Current.UpdateDetailForCancelDayBalance, balanceNO, operID));
        }
    }
}
