using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.HISFC.BizProcess.Integrate.Account
{
    public class AccountPay : IntegrateBase, FS.HISFC.BizProcess.Interface.Account.IAccountPay
    {
        #region 逻辑管理类

        /// <summary>
        /// 账户管理类
        /// </summary>
        private FS.HISFC.BizLogic.Fee.Account accountManager = null;

        /// <summary>
        /// 账户管理类
        /// </summary>
        public FS.HISFC.BizLogic.Fee.Account AccountManager
        {
            get
            {
                if (accountManager == null)
                {
                    accountManager = new FS.HISFC.BizLogic.Fee.Account();
                }
                return accountManager;
            }
        }

        #endregion

        #region 接口函数实现

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
        public int OutpatientPay(HISFC.Models.RADT.Patient patient, string accountNo, string acountTypeCode, decimal baseCost, decimal donateCost, string payInvoiceNo, HISFC.Models.RADT.Patient empowerPatient, FS.HISFC.Models.Account.PayWayTypes payWayTypes, int aMod)
        {
            int i = this.AccountManager.UpdateAccountAndWriteRecord(patient, accountNo, acountTypeCode, baseCost, donateCost, payInvoiceNo, empowerPatient, payWayTypes, aMod);
            this.Err = AccountManager.Err;
            return i;
        }

        /// <summary>
        /// 根据账号和账户类型查找账户明细
        /// </summary>
        /// <param name="accountNo">账号</param>
        /// <param name="accountTypeCode">账户类型ALL全部</param>
        /// <returns></returns>
        public List<FS.HISFC.Models.Account.AccountDetail> QueryAccountDetail(string accountNo, string accountTypeCode)
        {
            if (string.IsNullOrEmpty(accountNo) || string.IsNullOrEmpty(accountTypeCode))
            {
                return null;
            }
            return this.AccountManager.GetAccountDetail(accountNo, accountTypeCode, "1");
        }
        
        /// <summary>
        /// 消费时更新积分
        /// </summary>
        /// <param name="cardNO">卡号</param>
        /// <param name="money">金额</param>
        /// <param name="invoiceNo">发票号</param>
        /// <returns></returns>
        public int UpdateCoupon(string cardNO, decimal money, string invoiceNo)
        {
            int rev = this.AccountManager.UpdateCouponForPay(cardNO, money, invoiceNo);
            if (rev < 0)
            {
                this.Err = AccountManager.Err;
            }
            return rev;
        }
        #endregion
    }
}
