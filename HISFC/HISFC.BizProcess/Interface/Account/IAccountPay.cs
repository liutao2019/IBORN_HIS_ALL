using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.HISFC.BizProcess.Interface.Account
{
    /// <summary>
    /// 账户消费接口
    /// </summary>
    public interface IAccountPay
    {
        /// <summary>
        /// 门诊消费
        /// </summary>
        /// <param name="patient">授权人</param>
        /// <param name="accountNo">账号</param>
        /// <param name="acountTypeCode">账户类型</param>
        /// <param name="baseCost">基本金额</param>
        /// <param name="donateCost">赠送金额</param>
        /// <param name="payInvoiceNo">对应消费发票号</param>
        /// <param name="empowerPatient">被授权人</param>
        /// <param name="payWayTypes">消费类型：P购买套餐；R门诊挂号；C门诊消费；I住院结算;M套餐押金</param>
        /// <param name="aMod">0退费1消费</param>
        /// <returns></returns>
        int OutpatientPay(HISFC.Models.RADT.Patient patient, string accountNo, string acountTypeCode, decimal baseCost, decimal donateCost, string payInvoiceNo, HISFC.Models.RADT.Patient empowerPatient, FS.HISFC.Models.Account.PayWayTypes payWayTypes, int aMod);

        /// <summary>
        /// 根据账号和账户类型查找账户明细
        /// </summary>
        /// <param name="accountNo">账号</param>
        /// <param name="accountType">账号类型ALL 全部</param>
        /// <returns></returns>
        List<FS.HISFC.Models.Account.AccountDetail> QueryAccountDetail(string accountNo, string accountTypeCode);

        /// <summary>
        /// 消费时的积分
        /// </summary>
        /// <param name="cardNO">卡号</param>
        /// <param name="money">金额，负为退费，否则收费</param>
        /// <param name="invoiceNo">发票号</param>
        /// <returns></returns>
        int UpdateCoupon(string cardNO, decimal money, string invoiceNo);
    }
}
