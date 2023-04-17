using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.BizProcess.CommonInterface.Common;

namespace FS.SOC.HISFC.OutpatientFee.BizProcess
{
    /// <summary>
    /// [功能描述: 门诊日结逻辑类]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2012-5]<br></br>
    /// </summary>
    public class DayReport : AbstractBizProcess
    {
        #region 挂号日结

        FS.SOC.HISFC.OutpatientFee.BizLogic.RegisterDayReport registerDayReportMgr = new FS.SOC.HISFC.OutpatientFee.BizLogic.RegisterDayReport();

        /// <summary>
        /// 挂号日结，根据时间范围日结
        /// </summary>
        /// <param name="oper"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="dayReport"></param>
        /// <returns></returns>
        public int Register(FS.FrameWork.Models.NeuObject oper, DateTime beginTime, DateTime endTime)
        {
            FS.SOC.HISFC.OutpatientFee.BizLogic.RegisterDayReport registerDayReportMgr = new FS.SOC.HISFC.OutpatientFee.BizLogic.RegisterDayReport();
            this.BeginTransaction();
            registerDayReportMgr.SetTrans(this.Trans);

            string balanceNO = registerDayReportMgr.GetDayReportID();

            if (string.IsNullOrEmpty(balanceNO) || "-1".Equals(balanceNO))
            {
                this.RollBack();
                this.err = "获取结算序号失败，" + registerDayReportMgr.Err;
                return -1;
            }
            FS.HISFC.Models.Registration.DayReport dayReport = new FS.HISFC.Models.Registration.DayReport();
            dayReport.ID = balanceNO;
            dayReport.Oper.ID = oper.ID;
            dayReport.BeginDate = beginTime;
            dayReport.EndDate = endTime;
            dayReport.Oper.OperTime = registerDayReportMgr.GetDateTimeFromSysDateTime();

            //插入日结信息
            if (registerDayReportMgr.InsertDayReport(dayReport, ref this.err) < 0)
            {
                this.RollBack();
                return -1;
            }

            //更新挂号信息
            int rtn = registerDayReportMgr.UpdateRegisterForDayBalance(beginTime, endTime, oper.ID, balanceNO);
            if (rtn == -1)
            {
                this.RollBack();
                this.err = "更新挂号信息失败，" + registerDayReportMgr.Err;
                return -1;
            }

            if (rtn == 0)
            {
                this.RollBack();
                this.err = "日结信息状态已经变更,请重新日结!";
                return -1;
            }

            //更新挂号费用信息
            rtn = registerDayReportMgr.UpdateRegisterFeeForDayBalance(beginTime, endTime, oper.ID, balanceNO);
            if (rtn < 0)
            {
                this.RollBack();
                this.err = "更新挂号费信息失败，" + registerDayReportMgr.Err;
                return -1;
            }

            this.Commit();
            return 1;
        }

        /// <summary>
        /// 取消当次挂号日结
        /// </summary>
        /// <param name="Oper"></param>
        /// <param name="balanceNO"></param>
        /// <returns></returns>
        public int CancelRegister(FS.FrameWork.Models.NeuObject Oper, string balanceNO)
        {
            this.BeginTransaction();
            FS.SOC.HISFC.OutpatientFee.BizLogic.RegisterDayReport registerDayReportMgr = new FS.SOC.HISFC.OutpatientFee.BizLogic.RegisterDayReport();
            registerDayReportMgr.SetTrans(this.Trans);

            //删除日结表中的数据
            int rtn = registerDayReportMgr.DeleteDayReport(Oper.ID, balanceNO);
            if (rtn == 0)
            {
                this.RollBack();
                this.err = "\n[删除挂号日结失败]日结信息状态已经变更或已审核";
                return -1;
            }
            if (rtn < 0)
            {
                this.RollBack();
                this.err = "\n[删除挂号日结失败]" + registerDayReportMgr.Err;
                return -1;
            }

            //更新挂号已日结记录至未日结记录
            if (registerDayReportMgr.UpdateRegisterForCancelDayBalance(Oper.ID, balanceNO) < 0)
            {
                this.RollBack();
                this.err = "\n[更新挂号已日结记录至未日结记录失败]" + registerDayReportMgr.Err;
                return -1;
            }
            //更新挂号费已日结记录至未日结记录
            if (registerDayReportMgr.UpdateRegisterFeeForCancelDayBalance(Oper.ID, balanceNO) < 0)
            {
                this.RollBack();
                this.err = "\n[更新挂号费已日结记录至未日结记录失败]" + registerDayReportMgr.Err;
                return -1;
            }
            this.Commit();
            return 1;
        }

        public List<FS.FrameWork.Models.NeuObject> GetRegisterBalanceListByMonth(DateTime month)
        {
            this.SetDB(registerDayReportMgr);
            List<FS.FrameWork.Models.NeuObject> list = registerDayReportMgr.QueryBalanceList(month, registerDayReportMgr.Operator.ID);
            this.err = registerDayReportMgr.Err;
            return list;
        }

        public int GetRegisterLastBalanceDateAndNO(string operCode, ref DateTime dtBeginTime, ref  string balanceNO)
        {
            this.SetDB(registerDayReportMgr);
            int i = registerDayReportMgr.GetLastBalanceDate(feeDayReportMgr.Operator.ID, ref dtBeginTime, ref balanceNO);
            this.err = registerDayReportMgr.Err;
            return i;
        }

        public int GetRegisterBalanceDate(string balance, ref DateTime dtBeginTime, ref DateTime dtEndTime, ref string operCode)
        {
            this.SetDB(registerDayReportMgr);
            int i = registerDayReportMgr.GetBalance(balance, ref dtBeginTime, ref dtEndTime, ref operCode);
            this.err = registerDayReportMgr.Err;
            return i;
        }

        public int GetRegisterFeeDate(string operCode, ref DateTime beginFeeDate)
        {
            this.SetDB(feeDayReportMgr);
            int i = registerDayReportMgr.GetLastFeeDate(operCode, ref beginFeeDate);
            this.err = feeDayReportMgr.Err;
            return i;

        }

        #endregion

        #region 挂号日结审核

        /// <summary>
        /// 日结审核
        /// </summary>
        /// <param name="oper">审核人</param>
        /// <param name="balanceNO">日结号</param>
        /// <returns></returns>
        public int RegisterCheck(FS.FrameWork.Models.NeuObject oper, string balanceNO)
        {
            FS.SOC.HISFC.OutpatientFee.BizLogic.RegisterDayReport registerDayReportMgr = new FS.SOC.HISFC.OutpatientFee.BizLogic.RegisterDayReport();
            this.BeginTransaction();
            registerDayReportMgr.SetTrans(this.Trans);

            int rtn = registerDayReportMgr.UpdateRegisterDayBalanceForCheck(oper.ID, balanceNO);
            if (rtn == 0)
            {
                this.RollBack();
                this.err = "日结信息状态已经变更,请重新查询!";
                return -1;
            }

            this.Commit();
            return 1;
        }

        /// <summary>
        /// 取消日结审核
        /// </summary>
        /// <param name="oper"></param>
        /// <param name="balanceNO"></param>
        /// <returns></returns>
        public int RegisterCancelCheck(FS.FrameWork.Models.NeuObject oper, string balanceNO)
        {
            FS.SOC.HISFC.OutpatientFee.BizLogic.RegisterDayReport registerDayReportMgr = new FS.SOC.HISFC.OutpatientFee.BizLogic.RegisterDayReport();
            this.BeginTransaction();
            registerDayReportMgr.SetTrans(this.Trans);

            int rtn = registerDayReportMgr.UpdateRegisterDayBalanceForCancelCheck(oper.ID, balanceNO);
            if (rtn == 0)
            {
                this.RollBack();
                this.err = "审核信息状态已经变更,请重新查询!";
                return -1;
            }

            this.Commit();
            return 1;
        }

        public List<FS.FrameWork.Models.NeuObject> GetRegisterBalanceListByDate(DateTime date)
        {
            this.SetDB(registerDayReportMgr);
            List<FS.FrameWork.Models.NeuObject> list = registerDayReportMgr.QueryBalanceListForCheck(date);
            this.err = registerDayReportMgr.Err;
            return list;
        }

        #endregion

        #region 门诊收费日结

        FS.SOC.HISFC.OutpatientFee.BizLogic.FeeDayReport feeDayReportMgr = new FS.SOC.HISFC.OutpatientFee.BizLogic.FeeDayReport();

        /// <summary>
        /// 门诊收费日结
        /// </summary>
        /// <returns></returns>
        public int ClinicFee(FS.FrameWork.Models.NeuObject oper, DateTime beginTime, DateTime endTime)
        {
            this.BeginTransaction();
            this.SetDB(this.feeDayReportMgr);

            //插入日结

            if (feeDayReportMgr.DealFeeDayBalance(oper.ID, feeDayReportMgr.Operator.ID, beginTime, endTime) < 0)
            {
                this.RollBack();
                this.err = feeDayReportMgr.Err;
                return -1;
            }

            this.Commit();
            return 1;
        }

        /// <summary>
        /// 门诊收费日结取消
        /// </summary>
        /// <param name="oper"></param>
        /// <param name="balanceNO"></param>
        /// <returns></returns>
        public int CancelClinicFee(FS.FrameWork.Models.NeuObject oper, string balanceNO)
        {
            this.BeginTransaction();
            this.SetDB(this.feeDayReportMgr);

            //插入日结

            if (feeDayReportMgr.DealFeeDayBalanceCancel(oper.ID, balanceNO) < 0)
            {
                this.RollBack();
                this.err = feeDayReportMgr.Err;
                return -1;
            }

            this.Commit();
            return 1;
        }

        public List<FS.FrameWork.Models.NeuObject> GetClinicFeeBalanceListByMonth(DateTime month)
        {
            this.SetDB(feeDayReportMgr);
            //{3C210DC7-ECCA-4b9d-81BB-B4E79F599C6D}
            FS.HISFC.Models.Base.Employee employee = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
            if (employee == null)
            {
                employee = new FS.HISFC.Models.Base.Employee();
            }
            FS.HISFC.Models.Base.Department dept = employee.Dept as FS.HISFC.Models.Base.Department;
            if (dept == null)
            {
                dept = new FS.HISFC.Models.Base.Department();
            }
            string hospitalid = dept.HospitalID;
            //{3C210DC7-ECCA-4b9d-81BB-B4E79F599C6D}
            List<FS.FrameWork.Models.NeuObject> list = feeDayReportMgr.QueryBalanceList(month, feeDayReportMgr.Operator.ID, hospitalid); //{3C210DC7-ECCA-4b9d-81BB-B4E79F599C6D}
            this.err = feeDayReportMgr.Err;
            return list;
        }

        public int GetClinicFeeLastBalanceDateAndNO(string operCode,string hospitalid, ref DateTime dtBeginTime, ref  string balanceNO) //{3C210DC7-ECCA-4b9d-81BB-B4E79F599C6D}
        {
            this.SetDB(feeDayReportMgr);
            int i = feeDayReportMgr.GetLastBalanceDate(feeDayReportMgr.Operator.ID, hospitalid, ref dtBeginTime, ref balanceNO); //{3C210DC7-ECCA-4b9d-81BB-B4E79F599C6D}
            this.err = feeDayReportMgr.Err;
            return i;
        }

        public int GetClinicFeeBalanceDate(string balance, ref DateTime dtBeginTime, ref DateTime dtEndTime, ref string operCode)
        {
            this.SetDB(feeDayReportMgr);
            int i = feeDayReportMgr.GetBalance(balance, ref dtBeginTime, ref dtEndTime, ref operCode);
            this.err = feeDayReportMgr.Err;
            return i;
        }

        public int GetClinicFeeDate(string operCode, ref DateTime beginFeeDate)
        {
            this.SetDB(feeDayReportMgr);
            int i = feeDayReportMgr.GetLastFeeDate(operCode, ref beginFeeDate);
            this.err = feeDayReportMgr.Err;
            return i;

        }

        #endregion

        #region 门诊收费日结审核

        /// <summary>
        /// 日结审核
        /// </summary>
        /// <param name="oper"></param>
        /// <param name="balanceNO"></param>
        /// <returns></returns>
        public int ClinicFeeCheck(FS.FrameWork.Models.NeuObject oper, string balanceNO)
        {
            this.BeginTransaction();
            feeDayReportMgr.SetTrans(this.Trans);

            int rtn = feeDayReportMgr.UpdateOutpatientDayBalanceForCheck(oper.ID, balanceNO);
            if (rtn == 0)
            {
                this.RollBack();
                this.err = "日结信息状态已经变更,请重新查询!";
                return -1;
            }

            this.Commit();
            return 1;
        }

        /// <summary>
        /// 取消日结审核
        /// </summary>
        /// <param name="oper"></param>
        /// <param name="balanceNO"></param>
        /// <returns></returns>
        public int ClinicFeeCancelCheck(FS.FrameWork.Models.NeuObject oper, string balanceNO)
        {
            this.BeginTransaction();
            feeDayReportMgr.SetTrans(this.Trans);

            int rtn = feeDayReportMgr.UpdateOutpatientDayBalanceForCancelCheck(oper.ID, balanceNO);
            if (rtn == 0)
            {
                this.RollBack();
                this.err = "审核信息状态已经变更,请重新查询!";
                return -1;
            }

            this.Commit();
            return 1;
        }

        public List<FS.FrameWork.Models.NeuObject> GetClinicFeeBalanceListByDate(DateTime date)
        {
            this.SetDB(feeDayReportMgr);
            List<FS.FrameWork.Models.NeuObject> list = feeDayReportMgr.QueryBalanceListForCheck(date);
            this.err = feeDayReportMgr.Err;
            return list;
        }

        #endregion

        #region 住院收费日结

        FS.SOC.HISFC.OutpatientFee.BizLogic.InpatientDayReport inpatientDayReportMgr = new FS.SOC.HISFC.OutpatientFee.BizLogic.InpatientDayReport();

        /// <summary>
        /// 住院收费日结
        /// </summary>
        /// <returns></returns>
        public int InpatientInvoice(FS.FrameWork.Models.NeuObject oper, DateTime beginTime, DateTime endTime)
        {
            this.BeginTransaction();
            this.SetDB(this.feeDayReportMgr);
            //插入日结

            if (inpatientDayReportMgr.DealInvoiceDayBalance(oper.ID, feeDayReportMgr.Operator.ID, beginTime, endTime) < 0)
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

            if (inpatientDayReportMgr.DealInvoiceDayBalanceCancel(oper.ID, balanceNO) < 0)
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
        public List<FS.FrameWork.Models.NeuObject> GetInpatientBalanceListByMonth(DateTime month)
        {
            this.SetDB(feeDayReportMgr);
            List<FS.FrameWork.Models.NeuObject> list = inpatientDayReportMgr.QueryBalanceList(month, inpatientDayReportMgr.Operator.ID);
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
        public int GetLastInpatientBalanceDateAndNO(string operCode, ref DateTime dtBeginTime, ref  string balanceNO)
        {
            this.SetDB(feeDayReportMgr);
            int i = inpatientDayReportMgr.GetLastBalanceDate(feeDayReportMgr.Operator.ID, ref dtBeginTime, ref balanceNO);
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
        public int GetInpatientBalanceDate(string balance, ref DateTime dtBeginTime, ref DateTime dtEndTime, ref string operCode)
        {
            this.SetDB(feeDayReportMgr);
            int i = inpatientDayReportMgr.GetBalance(balance, ref dtBeginTime, ref dtEndTime, ref operCode);
            this.err = feeDayReportMgr.Err;
            return i;
        }

        #endregion
    }
}
