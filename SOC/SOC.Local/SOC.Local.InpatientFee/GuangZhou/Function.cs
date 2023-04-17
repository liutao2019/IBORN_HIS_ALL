using System;
using System.Windows.Forms;
using System.Collections;
using System.Text.RegularExpressions;
using FS.HISFC.BizLogic;
using FS.FrameWork.Management;
using System.Collections.Generic;
using FS.SOC.HISFC.BizProcess.CommonInterface;
namespace FS.SOC.Local.InpatientFee.GuangZhou
{
	/// <summary>
	/// 	/// Function 的摘要说明。
	/// </summary>
	///
    public class Function
    {
        //		private FarPoint.Win.Spread.FpSpread fpSpread1;
        //		private FarPoint.Win.Spread.SheetView fpSpread1_Sheet1;
        public Function()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region 结算前检查
        /// <summary>
        /// 判断是否有未确认的退费
        /// 是否有未确认的出单
        /// </summary>
        /// <param name="ID">住院流水号</param>
        /// <returns>-1出错，0通过</returns>
        public int CheckBeforeBalance(string ID)
        {
            FS.HISFC.BizLogic.Order.ChargeBill billMgr = new FS.HISFC.BizLogic.Order.ChargeBill();
            FS.HISFC.BizLogic.Fee.ReturnApply apprMgr = new FS.HISFC.BizLogic.Fee.ReturnApply();
            FS.HISFC.BizLogic.Order.Order order = new FS.HISFC.BizLogic.Order.Order();
            //判断是否还有为确认的出单
            //-暂时先注释掉 xf
            #region 判断是否还有为确认的出单
            /*
            try
            {
                ArrayList allItems = billMgr.QueryChargeBillNotPrinted(ID, true);
                if (allItems == null) return -1;

                #region   Edit by xingz
                ArrayList validItems = new ArrayList();
                for (int i = 0; i < allItems.Count; i++)
                {
                    FS.HISFC.Models.Order.ChargeBill chargebill = (FS.HISFC.Models.Order.ChargeBill)allItems[i];
                    if (chargebill == null)
                    {
                        MessageBox.Show("获取未确认的收费单出错！");
                        return -1;
                    }
                    if (chargebill.IsPharmacy && chargebill.BillID != null && chargebill.BillID != "")
                    {
                        FS.HISFC.Models.Order.ExecOrder execorder = order.QueryExecOrder(chargebill.ExecID, "1");
                        if (execorder != null)
                        {
                            if (!execorder.IsValid)
                            {
                                validItems.Add(execorder);
                            }
                        }
                    }
                    if (!chargebill.IsPharmacy && chargebill.BillID != null && chargebill.BillID != "")
                    {
                        FS.HISFC.Models.Order.ExecOrder execorder = order.QueryExecOrder(chargebill.ExecID, "2");
                        if (execorder != null)
                        {
                            if (!execorder.IsValid)
                            {
                                validItems.Add(execorder);
                            }
                        }
                    }
                }
                #endregion

                if (validItems.Count > 0)
                {
                    DialogResult dr = MessageBox.Show("还有未经确认的收费单,是否继续结算？", "提示"
                        , MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (dr == DialogResult.No)
                    {
                        return -1;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("获取出单项目时出错!" + e.Message, "提示");
                return -1;
            }
            */
            #endregion
            //判断是否还有未确认的退费申请
            try
            {
                ArrayList applys = apprMgr.QueryReturnApplys(ID, false);
                if (applys == null) return -1;
                if (applys.Count > 0)
                {
                    DialogResult dr = MessageBox.Show("还有未确认的退费申请，是否继续结算？", "提示"
                        , MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (dr == DialogResult.No)
                    {
                        return -1;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("获取退费申请项目时出错!" + ex.Message, "提示");
                return -1;
            }
            //判断是否还有收费的手术暂存项目
            try
            {
                FS.HISFC.BizLogic.Manager.Department myDept = new FS.HISFC.BizLogic.Manager.Department();
                FS.HISFC.BizLogic.Fee.TemporaryFee tempFee = new FS.HISFC.BizLogic.Fee.TemporaryFee();
                //获取手术科室列表
                ArrayList alDept = myDept.GetDeptment("1");

                if (alDept != null && alDept.Count > 0)
                {
                    foreach (FS.HISFC.Models.Base.Department dept in alDept)
                    {
                        ArrayList alOpsFee = tempFee.Query(ID, dept.ID);

                        if (alOpsFee == null)
                        {
                            return -1;
                        }
                        if (alOpsFee.Count > 0)
                        {
                            DialogResult dr = MessageBox.Show(dept.Name + "还有已暂存但未收费的项目，是否继续结算？", "提示"
                                , MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                            if (dr == DialogResult.No)
                            {
                                return -1;
                            }
                        }
                    }
                }
            }
            catch
            {
                return -1;
            }
            return 0;
        }
        #endregion

        #region 结算时收取未收取的床位费
        /// <summary>
        /// 结算时收取未收取的床位费
        /// </summary>
        /// <param name="info"></param>		
        /// <returns></returns>
        public int BedFeeForOutpatient(FS.HISFC.Models.RADT.PatientInfo info)
        {
            /*收取床位费函数，用于收取出院患者补收床位费，
             * 尚不能解决的问题：由于患者已经出院登记，对于患者包床费用不能收取，
             * 如果患者未收费期间曾经换过床，只能按照换床后的等级收取床位费
             * 床位等级取主表保存床号
             * */
            //判断是否有分配床位，未接诊患者没有床位费，
            if (info.PVisit.PatientLocation.Bed.ID == "") return 0;
            //婴儿不收床位费
            if (info.Patient.IsBaby) return 0;
            //
            if (info.ExtendFlag1 == "1") return 0;
            //系统切换时的床位号
            if (info.PVisit.PatientLocation.Bed.ID == "SSSS") return 0;
            if (info.PVisit.OutTime == DateTime.MinValue) return 0;
            decimal Amount = 0m; //床位费数量

            FS.HISFC.BizLogic.Manager.Bed bedFee = new FS.HISFC.BizLogic.Manager.Bed();
            DateTime OperDate = bedFee.GetDateTimeFromSysDateTime();
            DateTime chargeDate = OperDate;

            //计算收取床位费数量
            if (info.FT.PreFixFeeDateTime == DateTime.MinValue)
            {
                //如果没收过床位费
                TimeSpan dt = new TimeSpan(info.PVisit.OutTime.Date.Ticks - info.PVisit.InTime.Date.Ticks);
                int days = dt.Days;
                if (days == 0)
                {
                    Amount = 1;
                }
                else
                {
                    Amount = (decimal)days;
                }
            }
            else
            {
                //如果收过床位费,中山一床位费收头不收尾,最后一天不收
                TimeSpan dt = new TimeSpan(info.PVisit.OutTime.Date.Ticks - info.FT.PreFixFeeDateTime.Date.Ticks);
                int days = dt.Days;
                if (days <= 1) return 0;
                Amount = (decimal)dt.Days - 1;

            }

            if (Amount == 0) return 0;//没有要收取的床位费
            if (Amount > 0)
            {
                DialogResult r = MessageBox.Show("该患者有未收取的床位费用,是否收取", "提示", MessageBoxButtons.YesNo);
                if (r == DialogResult.No)
                    return 0;
            }


            //本次固定费用收取时间,因为收头不收尾,本次收费收到昨天
            info.FT.PreFixFeeDateTime = FS.FrameWork.Function.NConvert.ToDateTime(info.PVisit.OutTime.AddDays(-1).ToShortDateString() + " 23:50:00");
            string OperId = "补收";
            //获取床位等级
            FS.HISFC.BizLogic.Fee.BedFeeItem bedMgr = new FS.HISFC.BizLogic.Fee.BedFeeItem();
            FS.HISFC.Models.Base.Bed bed = bedFee.GetBedInfo(info.PVisit.PatientLocation.Bed.ID);
            if (bed == null)
            {
                MessageBox.Show("获取床位出错！" + bedFee.Err);
                return -1;
            }
            if (bed.BedGrade == null || bed.BedGrade.ID == "")
            {

                MessageBox.Show(bed.ID + "床的床位等级没有维护");
                return -1;
            }

            //获取固定费用收费项目
            ArrayList bedItems;
            //bedItems = bedMgr.SelectByFeeCode(bed.BedGrade.ID);
            bedItems = bedMgr.QueryBedFeeItemByMinFeeCode(bed.BedGrade.ID);
            if (bedItems == null || bedItems.Count == 0)
            {
                MessageBox.Show("没有维护床位收费项目" + bedFee.Err);
                return 0;
            }
            #region 收取床位费

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.HISFC.BizLogic.Fee.InPatient feeMgr = new FS.HISFC.BizLogic.Fee.InPatient();
            FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();

            feeIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            if (feeIntegrate.DoBedItemFee(bedItems, info, (int)Amount, OperDate,chargeDate, bed) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("收取床位费出错！" + feeIntegrate.Err);
                return -1;
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            return 0;
         
            #endregion
        }
        #endregion


        private static Dictionary<string, string> dictionaryYKDept = new Dictionary<string, string>();
        /// <summary>
        ///  判断是否是宜康科室
        /// </summary>
        /// <param name="dept"></param>
        /// <returns></returns>
        public static bool IsContainYKDept()
        {

            string dept = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID;

            return IsContainYKDept(dept);
        }

        /// <summary>
        ///  判断是否是宜康科室
        /// </summary>
        /// <param name="dept"></param>
        /// <returns></returns>
        public static bool IsContainYKDept(string dept)
        {
            if (dictionaryYKDept == null || dictionaryYKDept.Count == 0)
            {
                ArrayList al = CommonController.Instance.QueryConstant("YkDept");
                if (al != null)
                {
                    foreach (FS.FrameWork.Models.NeuObject obj in al)
                    {
                        dictionaryYKDept[obj.ID] = obj.Name;
                    }
                }
            }

            return dictionaryYKDept.ContainsKey(dept);
        }

    }
}
