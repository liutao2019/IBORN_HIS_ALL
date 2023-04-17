using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neusoft.SOC.HISFC.OutpatientFee.BizLogic
{
    /// <summary>
    /// [功能描述: 挂号管理类]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2012-5]<br></br>
    /// </summary>
    public class Register:Neusoft.FrameWork.Management.Database
    {
        /// <summary>
        /// 取消挂号日结
        /// </summary>
        /// <param name="balanceNO"></param>
        /// <returns></returns>
        public int UpdateForCancelDayBalance(string operID, string balanceNO)
        {
            return UpdateSingleTable("", operID, balanceNO);
        }

        /// <summary>
        /// 挂号日结
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <param name="OperID"></param>
        /// <param name="BalanceID"></param>
        /// <returns></returns>
        public int UpdateForDayBalance(DateTime begin, DateTime end, string operID, string balanceID)
        {
            return UpdateSingleTable("Registration.Register.Update.DayBalance", begin.ToString("yyyy-MM-dd HH:mm:ss"), end.ToString("yyyy-MM-dd HH:mm:ss"), operID, balanceID);
        }
    }
}
