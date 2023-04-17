using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.HISFC.BizProcess.Integrate.Account
{
    public class CashCoupon
    {
        #region 逻辑管理类

        /// <summary>
        /// 账户相关业务管理类
        /// </summary>
        HISFC.BizLogic.Fee.Account accountManager = new FS.HISFC.BizLogic.Fee.Account();

        #endregion

        /// <summary>
        /// 保存现金流积分
        /// </summary>
        /// <returns></returns>
        public int CashCouponSave(string couponType,string remark,string cardno,string invoice_no,decimal coupon,ref string errInfo)
        {
            //{9D149575-AF92-437a-B95C-27083F96DD7F}
            int rtn = accountManager.UpdateCashCouponVacancy(cardno, coupon);

            if(rtn < 0)
            {
                errInfo = accountManager.Err;
                return -1;
            }

            if(rtn == 0)
            {
                HISFC.Models.Account.CashCoupon cashCouponObj = new FS.HISFC.Models.Account.CashCoupon();
                cashCouponObj.CardNo = cardno;
                cashCouponObj.CouponVacancy = coupon;
                cashCouponObj.CouponAccumulate = coupon;

                rtn = accountManager.InsertCashCouponVacancy(cashCouponObj);

                if(rtn <= 0)
                {
                    errInfo = accountManager.Err;
                    return -1;
                }
            }

            HISFC.Models.Account.CashCouponRecord cashCouponRecordObj = new FS.HISFC.Models.Account.CashCouponRecord();
            cashCouponRecordObj.ID = this.accountManager.GetCashConponVacancySeq();
            cashCouponRecordObj.CardNo = cardno;
            cashCouponRecordObj.Coupon = coupon;
            cashCouponRecordObj.CouponVacancy = 0.0m;
            cashCouponRecordObj.InvoiceNo = invoice_no;
            cashCouponRecordObj.CouponType = couponType;
            cashCouponRecordObj.Memo = remark;
            cashCouponRecordObj.OperEnvironment.ID = FrameWork.Management.Connection.Operator.ID;
            cashCouponRecordObj.OperEnvironment.OperTime = this.accountManager.GetDateTimeFromSysDateTime();

            rtn = this.accountManager.InsertCashCouponRecord(cashCouponRecordObj);

            if (rtn <= 0)
            {
                errInfo = accountManager.Err;
                return -1;
            }

            return rtn;
        }
    }
}
