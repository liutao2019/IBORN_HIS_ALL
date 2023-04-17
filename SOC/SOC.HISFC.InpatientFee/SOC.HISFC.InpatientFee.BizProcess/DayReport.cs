using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.BizProcess.CommonInterface.Common;

namespace FS.SOC.HISFC.InpatientFee.BizProcess
{
    /// <summary>
    /// [功能描述: 住院日结逻辑类]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2012-5]<br></br>
    /// </summary>
    public class DayReport : AbstractBizProcess
    {
        FS.SOC.HISFC.InpatientFee.BizLogic.DayReport feeDayReportMgr = new FS.SOC.HISFC.InpatientFee.BizLogic.DayReport();

        #region 收费日结

        /// <summary>
        /// 住院收费日结
        /// </summary>
        /// <returns></returns>
        public int InpatientInvoice(FS.FrameWork.Models.NeuObject oper, DateTime beginTime, DateTime endTime)
        {
            this.BeginTransaction();
            this.SetDB(this.feeDayReportMgr);
            //插入日结

            if (feeDayReportMgr.DealInvoiceDayBalance(oper.ID, feeDayReportMgr.Operator.ID, beginTime, endTime) < 0)
            {
                this.RollBack();
                this.err = feeDayReportMgr.Err;
                return -1;
            }

            this.Commit();
            return 1;
        }

        /// <summary>
        /// 住院收费日结取消
        /// </summary>
        /// <param name="oper"></param>
        /// <param name="balanceNO"></param>
        /// <returns></returns>
        public int CancelInpatientInvoice(FS.FrameWork.Models.NeuObject oper, string balanceNO)
        {

            this.BeginTransaction();
            this.SetDB(this.feeDayReportMgr);
            //插入日结

            if (feeDayReportMgr.DealInvoiceDayBalanceCancel(oper.ID, balanceNO) < 0)
            {
                this.RollBack();
                this.err = feeDayReportMgr.Err;
                return -1;
            }

            this.Commit();
            return 1;
        }

        /// <summary>
        /// 获取日结信息
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        public List<FS.FrameWork.Models.NeuObject> GetBalanceListByMonth(DateTime month)
        {
            this.SetDB(feeDayReportMgr);
            List<FS.FrameWork.Models.NeuObject> list = feeDayReportMgr.QueryBalanceList(month, feeDayReportMgr.Operator.ID);
            this.err = feeDayReportMgr.Err;
            return list;
        }

        /// <summary>
        /// 获取最新的日结时间和日结号
        /// </summary>
        /// <param name="operCode"></param>
        /// <param name="dtBeginTime"></param>
        /// <param name="balanceNO"></param>
        /// <returns></returns>
        public int GetLastBalanceDateAndNO(string operCode, ref DateTime dtBeginTime, ref  string balanceNO)
        {
            this.SetDB(feeDayReportMgr);
            int i = feeDayReportMgr.GetLastBalanceDate(feeDayReportMgr.Operator.ID, ref dtBeginTime, ref balanceNO);
            this.err = feeDayReportMgr.Err;
            return i;
        }

        /// <summary>
        /// 获取日结日期
        /// </summary>
        /// <param name="balance"></param>
        /// <param name="dtBeginTime"></param>
        /// <param name="dtEndTime"></param>
        /// <param name="operCode"></param>
        /// <returns></returns>
        public int GetBalanceDate(string balance, ref DateTime dtBeginTime, ref DateTime dtEndTime, ref string operCode)
        {
            this.SetDB(feeDayReportMgr);
            int i = feeDayReportMgr.GetBalance(balance, ref dtBeginTime, ref dtEndTime, ref operCode);
            this.err = feeDayReportMgr.Err;
            return i;
        }

        #endregion

        #region 收费日结审核

        public int InpatientFeeCheck(FS.FrameWork.Models.NeuObject oper, string balanceNO)
        {
            this.BeginTransaction();
            feeDayReportMgr.SetTrans(this.Trans);
            int ret;
            ret = feeDayReportMgr.UpdateInpatientDayBalancePrepayForCheck(oper.ID, balanceNO);
            if (ret < 0)
            {
                this.err = "预交金日结审核错误：\n" + feeDayReportMgr.Err;
                this.RollBack();
                return -1;
            }
            if (ret == 0)
            {
                this.err = "已审核或审核信息状态已经变更,请重新查询!";
                this.RollBack();
                return -1;
            }

            ret = feeDayReportMgr.UpdateInpatientDayBalanceInvoiceForCheck(oper.ID, balanceNO);
            if (ret < 0)
            {
                this.err = "结算发票日结审核错误：\n" + feeDayReportMgr.Err;
                this.RollBack();
                return -1;
            }
            if (ret == 0)
            {
                this.err = "已审核或审核信息状态已经变更,请重新查询!";
                this.RollBack();
                return -1;
            }
            this.Commit();
            return 1;
        }

        public int InpatientFeeCancelCheck(FS.FrameWork.Models.NeuObject oper, string balanceNO)
        {
            this.BeginTransaction();
            feeDayReportMgr.SetTrans(this.Trans);
            int ret;
            ret = feeDayReportMgr.UpdateInpatientDayBalancePrepayForCancelCheck(oper.ID, balanceNO);
            if (ret < 0)
            {
                this.err = "预交金日结取消审核错误：\n" + feeDayReportMgr.Err;
                this.RollBack();
                return -1;
            }
            if (ret == 0)
            {
                this.err = "预交金审核信息状态已经变更,请重新查询!";
                this.RollBack();
                return -1;
            }

            ret = feeDayReportMgr.UpdateInpatientDayBalanceInvoiceForCancelCheck(oper.ID, balanceNO);
            if (ret < 0)
            {
                this.err = "结算发票日结取消审核错误：\n" + feeDayReportMgr.Err;
                this.RollBack();
                return -1;
            }
            if (ret == 0)
            {
                this.err = "结算发票审核信息状态已经变更,请重新查询!";
                this.RollBack();
                return -1;
            }
            this.Commit();
            return 1;
        }

        public List<FS.FrameWork.Models.NeuObject> GetInpatientFeeBalanceListByDate(DateTime date)
        {
            this.SetDB(feeDayReportMgr);
            List<FS.FrameWork.Models.NeuObject> list = feeDayReportMgr.QueryBalanceListForCheck(date);
            this.err = feeDayReportMgr.Err;
            return list;
        }

        #endregion
    }
}
