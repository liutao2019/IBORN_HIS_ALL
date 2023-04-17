﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using FS.HISFC.BizProcess.Interface.Common;

namespace FS.SOC.Local.Order.SubFeeSet.SubFee
{
    /// <summary>
    /// 住院附材后台收取
    /// </summary>
    class SubJob : FS.FrameWork.Management.Database, IJob
    {
        #region 变量

        public FS.FrameWork.WinForms.Controls.NeuRichTextBox rtbLogo = null;

        private FS.HISFC.BizProcess.Integrate.RADT radtMgr = new FS.HISFC.BizProcess.Integrate.RADT();

        private FS.HISFC.BizLogic.Order.Order orderMgr = new FS.HISFC.BizLogic.Order.Order();

        ArrayList alExecOrders = new ArrayList();
        ArrayList alOrders = new ArrayList();
        ArrayList alSubOrders = new ArrayList();
        ArrayList alFeeItems = new ArrayList();
        FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItem = new FS.HISFC.Models.Fee.Inpatient.FeeItemList();

        /// <summary>
        /// 住院附材算法接口
        /// </summary>
        static FS.HISFC.BizProcess.Interface.Order.IDealSubjob iDealSubjob = null;

        FS.HISFC.BizProcess.Integrate.Manager inteMgr = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// 附材收费项目
        /// </summary>
        FS.HISFC.Models.Fee.Item.Undrug undrug = new FS.HISFC.Models.Fee.Item.Undrug();

        FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();

        /// <summary>
        /// 要收取的附材的日期
        /// </summary>
        public DateTime FeeDate = new DateTime();

        #endregion

        /// <summary>
        /// 附材收取
        /// </summary>
        /// <returns>1 成功 －1 失败</returns>
        public int SubFeeStart()
        {
            ArrayList alPatient = this.radtMgr.QueryPatient(FS.HISFC.Models.Base.EnumInState.I);
            if (alPatient == null)
            {
                errInfo = radtMgr.Err;
                return -1;
            }

            alExecOrders = new ArrayList();

            foreach (FS.HISFC.Models.RADT.PatientInfo patientInfo in alPatient)
            {
                //if (patientInfo.PID.PatientNO == "0000074312")
                //{
                //    MessageBox.Show("0000074312");
                //}
                //else
                //{
                //    continue;
                //}

                //根据上次收取的附材时间来判断，按天收取
                string sql = @"select max(f.exec_date)
                            from com_job_log f
                            where f.job_code='Sub_Fee'
                            and f.exec_oper='{0}'";
                string date = orderMgr.ExecSqlReturnOne(string.Format(sql, patientInfo.ID), "");
                DateTime dtNow = orderMgr.GetDateTimeFromSysDateTime();
                DateTime execDate = dtNow.AddDays(-1);
                if (!string.IsNullOrEmpty(date))
                {
                    try
                    {
                        execDate = FS.FrameWork.Function.NConvert.ToDateTime(date).AddDays(1);
                    }
                    catch
                    {
                        execDate = dtNow.AddDays(-1);
                    }
                }

                while (execDate.Date < dtNow.Date)
                {
                    FeeDate = execDate.Date;

                    execDate = execDate.AddDays(1);

                    //避免修改固定收费时间 补收费用，此处判断已经收取的不再收费
                    if (this.CheckIsFeeed(patientInfo.ID, FeeDate))
                    {
                        continue;
                    }

                    alOrders = new ArrayList();
                    alSubOrders = new ArrayList();
                    alFeeItems = new ArrayList();

                    alExecOrders = this.orderMgr.QueryExecOrder(patientInfo.ID, "1",
                        FeeDate.Date,
                        FeeDate.AddDays(1).Date);
                    if (alExecOrders == null)
                    {
                        errInfo = orderMgr.Err;
                        this.WriteErrInfo();
                        continue;
                    }
                    else if (alExecOrders.Count == 0)
                    {
                        continue;
                    }

                    string strOrderID = "'";
                    foreach (FS.HISFC.Models.Order.ExecOrder execOrder in alExecOrders)
                    {
                        strOrderID += execOrder.Order.ID + "','";
                    }
                    strOrderID = strOrderID + "'";

                    alOrders = this.orderMgr.QueryOrder(patientInfo.ID, "1", strOrderID);
                    if (alOrders == null)
                    {
                        errInfo = this.orderMgr.Err;
                        this.WriteErrInfo();
                        continue;
                    }
                    else if (alOrders.Count == 0)
                    {
                        continue;
                    }

                    if (alOrders.Count > 0)
                    {
                        int rev = this.DealSubjobByInpatient(patientInfo, (FS.HISFC.Models.Order.Inpatient.Order)alOrders[0],
                            alOrders, ref alSubOrders, ref errInfo);
                        if (rev == -1)
                        {
                            return -1;
                        }
                        else if (rev == 0)
                        {
                            errInfo = "附材收费接口未实现！" + errInfo;
                            this.WriteErrInfo();
                            continue;
                        }
                        else
                        {
                            foreach (FS.HISFC.Models.Base.Item item in alSubOrders)
                            {
                                feeItem = new FS.HISFC.Models.Fee.Inpatient.FeeItemList();
                                feeItem = this.CreateFeeItemList(item, patientInfo);
                                if (feeItem == null)
                                {
                                    this.WriteErrInfo();
                                    continue;
                                }
                                alFeeItems.Add(feeItem);
                            }
                        }
                    }

                    #region 按人单个收费

                    FS.FrameWork.Management.PublicTrans.BeginTransaction();
                    if (alFeeItems != null && alFeeItems.Count > 0)
                    {
                        if (this.feeIntegrate.FeeItem(patientInfo, ref alFeeItems) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            errInfo = this.feeIntegrate.Err;
                            this.WriteErrInfo();
                            continue;
                        }
                    }

                    //foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItem in alFeeItems)
                    //{
                    //    if (this.feeIntegrate.FeeItem(patientInfo, feeItem) == -1)
                    //    {
                    //        FS.FrameWork.Management.PublicTrans.RollBack();
                    //        errInfo = this.feeIntegrate.Err;
                    //        this.WriteErrInfo();
                    //        continue;
                    //    }
                    //}

                    try
                    {
                        string sqlInsert = @"INSERT INTO com_job_log   --定时收费日志
                                          ( job_code,   --任务类型
                                            job_name,   --任务名称
                                            exec_date,   --执行时间
                                            exec_oper )  --执行者
                                     VALUES 
                                          ('{0}' ,   --任务类型
                                           '{1}' ,   --任务名称
                                            to_date('{2}','yyyy-mm-dd hh24:mi:ss'),   --执行时间
                                            '{3}') --执行者";
                        sqlInsert = string.Format(sqlInsert, "Sub_Fee", "住院附材收费", FeeDate.Date, patientInfo.ID);
                        if (this.orderMgr.ExecNoQuery(sqlInsert) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            errInfo = orderMgr.Err;
                            this.WriteErrInfo();
                            return -1;
                        }
                    }
                    catch (Exception ex)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        errInfo = ex.Message;
                        this.WriteErrInfo();
                        return -1;
                    }

                    FS.FrameWork.Management.PublicTrans.Commit();

                    #endregion
                }
            }

            return 1;
        }

        /// <summary>
        /// 写错误日志
        /// </summary>
        private void WriteErrInfo()
        {
            this.orderMgr.Err = errInfo;
            this.orderMgr.WriteErr();
        }

        /// <summary>
        /// 是否已收取过附材费用
        /// </summary>
        /// <param name="inPatientNo"></param>
        /// <param name="feeDate"></param>
        /// <returns></returns>
        private bool CheckIsFeeed(string inPatientNo, DateTime feeDate)
        {
            string sql = @"select count(*) from com_job_log t
                                where t.job_code='{0}'
                                and t.exec_oper='{1}'
                                and t.exec_date=to_date('{2}','yyyy-mm-dd hh24:mi:ss')";
            try
            {
                sql = string.Format(sql, "Sub_Fee", inPatientNo, feeDate.Date);

                string rev = this.orderMgr.ExecSqlReturnOne(sql);

                if (FS.FrameWork.Function.NConvert.ToInt32(rev) > 0)
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }

            return false;
        }

        private int GetHosInfo()
        {
            System.Collections.ArrayList alHospitial = new System.Collections.ArrayList();
            string sql = "SELECT HOS_NAME,NUMBER_VALUE ,hos_code,mark,DATABASE_FLAG FROM COM_HOSPITALINFO where valid_Flag = '1'";

            if (this.orderMgr.ExecQuery(sql) == -1)
            {
                MessageBox.Show(orderMgr.Err);
                return -1;
            }
            try
            {
                FS.FrameWork.Models.NeuObject hosObj = null;
                while (orderMgr.Reader.Read())
                {
                    //{174D9248-D2A9-4795-843F-51524E03A64D}
                    hosObj = new FS.FrameWork.Models.NeuObject();
                    hosObj.ID = orderMgr.Reader[2].ToString();
                    hosObj.Name = orderMgr.Reader[0].ToString();
                    hosObj.Memo = orderMgr.Reader[3].ToString();
                    hosObj.User01 = orderMgr.Reader[4].ToString();
                    alHospitial.Add(hosObj);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }
            finally
            {
                orderMgr.Reader.Close();
            }

            if (alHospitial.Count == 0)
            {
                MessageBox.Show("没有在用的医院信息，请查看【医院信息表】");
                return -1;
            }
            if (alHospitial.Count > 0)
            {
                if (string.IsNullOrEmpty(FS.FrameWork.Management.Connection.Hospital.ID))
                {
                    foreach (FS.FrameWork.Models.NeuObject obj in alHospitial)
                    {
                        if (!string.IsNullOrEmpty(obj.ID))
                        {
                            FS.FrameWork.Management.Connection.Hospital.ID = obj.ID;
                        }
                    }
                }
            }

            if (string.IsNullOrEmpty(FS.FrameWork.Management.Connection.Hospital.ID))
            {
                MessageBox.Show("医院编码为空！");
            }

            return 1;
        }

        public int DealSubjobByInpatient(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.HISFC.Models.Order.Inpatient.Order order, ArrayList alOrders, ref ArrayList alSubOrders, ref string errInfo)
        {
            this.GetHosInfo();

            if (iDealSubjob == null)
            {
                iDealSubjob = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(FS.HISFC.BizProcess.Integrate.Order), typeof(FS.HISFC.BizProcess.Interface.Order.IDealSubjob)) as FS.HISFC.BizProcess.Interface.Order.IDealSubjob;
            }
            if (iDealSubjob != null)
            {
                iDealSubjob.FeeDate = FeeDate;
                //附材计算
                return iDealSubjob.DealSubjob(patientInfo, false, order, alOrders, ref alSubOrders, ref errInfo);
            }
            else
            {
                errInfo = FS.FrameWork.WinForms.Classes.UtilInterface.Err;
                return 0;
            }
        }

        /// <summary>
        /// 创建费用信息
        /// </summary>
        /// <param name="item"></param>
        /// <param name="patient"></param>
        /// <returns></returns>
        private FS.HISFC.Models.Fee.Inpatient.FeeItemList CreateFeeItemList(FS.HISFC.Models.Base.Item item, FS.HISFC.Models.RADT.PatientInfo patient)
        {
            undrug = new FS.HISFC.Models.Fee.Item.Undrug();
            undrug = feeIntegrate.GetItem(item.ID);
            if (undrug == null)
            {
                errInfo = "获取非药品失败！" + feeIntegrate.Err;
                return null;
            }
            else if (!undrug.IsValid)
            {
                errInfo = "非药品项目" + undrug.Name + "已经停用！";
                return null;
            }
            undrug.Qty = item.Qty;

            //实体赋值
            FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItem = new FS.HISFC.Models.Fee.Inpatient.FeeItemList();
            feeItem.IsBaby = patient.IsBaby;
            feeItem.Item = undrug;

            decimal price = 0;
            decimal orgPrice = 0;
            if (feeIntegrate.GetPriceForInpatient(patient, feeItem.Item, ref price, ref orgPrice) != -1)
            {
                if (price > 0)
                {
                    feeItem.Item.Price = price;
                    feeItem.Item.DefPrice = orgPrice;
                }
            }

            feeItem.NoBackQty = undrug.Qty;
            feeItem.RecipeNO = feeIntegrate.GetUndrugRecipeNO();
            feeItem.Patient.Pact.PayKind.ID = patient.Pact.PayKind.ID;
            feeItem.TransType = FS.HISFC.Models.Base.TransTypes.Positive;
            feeItem.FeeOper.Dept.ID = patient.PVisit.PatientLocation.Dept.ID;
            ((FS.HISFC.Models.RADT.PatientInfo)feeItem.Patient).PVisit.PatientLocation.NurseCell.ID = patient.PVisit.PatientLocation.NurseCell.ID;
            feeItem.RecipeOper.Dept.ID = patient.PVisit.PatientLocation.Dept.ID;
            feeItem.ExecOper.Dept.ID = patient.PVisit.PatientLocation.Dept.ID;
            if (patient.PVisit.AdmittingDoctor.ID == null || patient.PVisit.AdmittingDoctor.ID == "")
                patient.PVisit.AdmittingDoctor.ID = "日计费";

            feeItem.RecipeOper.ID = patient.PVisit.AdmittingDoctor.ID;
            feeItem.PayType = FS.HISFC.Models.Base.PayTypes.Balanced;

            #region
            DateTime dtNow = orderMgr.GetDateTimeFromSysDateTime();
            //划价时间为医嘱执行的日期当天晚上
            feeItem.ChargeOper.OperTime = this.FeeDate.Date.AddHours(23).AddMinutes(59);
            //避免划价时间早于入院时间
            if (feeItem.ChargeOper.OperTime < patient.PVisit.InTime)
            {
                feeItem.ChargeOper.OperTime = patient.PVisit.InTime.AddHours(1);
            }
            if ((feeItem.ChargeOper.OperTime > patient.PVisit.OutTime
                && patient.PVisit.OutTime > new DateTime(2000, 1, 1))
                || (feeItem.ChargeOper.OperTime > patient.PVisit.PreOutTime
                && patient.PVisit.PreOutTime > new DateTime(2000, 1, 1)))
            {
                feeItem.ChargeOper.OperTime = dtNow;
                //feeItem.ChargeOper.OperTime = this.FeeDate.Date.AddHours(23).AddMinutes(50);
            }

            ////收费时间为医嘱执行的日期当天晚上
            //feeItem.FeeOper.OperTime = this.FeeDate.Date.AddHours(23).AddMinutes(59);
            ////避免划价时间早于入院时间
            //if (feeItem.FeeOper.OperTime < patient.PVisit.InTime)
            //{
            //    feeItem.FeeOper.OperTime = patient.PVisit.InTime.AddHours(1);
            //}
            //if (feeItem.FeeOper.OperTime > patient.PVisit.OutTime || feeItem.FeeOper.OperTime > patient.PVisit.PreOutTime)
            //{
            //    //feeItem.FeeOper.OperTime = dtNow;
            //    feeItem.FeeOper.OperTime = this.FeeDate.Date.AddHours(23).AddMinutes(50);

            //}


            //划价时间记录实际收取的项目的日期
            //feeItem.FeeOper.OperTime = this.FeeDate.Date.AddHours(23).AddMinutes(59);
            //如果收取附材的时间比当前时间早了超过1天，则认为是后来才运行的固定收附材程序
            //为了保证前几天的应收数据不再变化，则前几天的收费时间也修改为昨晚的时间
            if (FeeDate.Date < dtNow.Date.AddDays(-1))
            {
                feeItem.FeeOper.OperTime = dtNow.Date.AddDays(-1).AddHours(23).AddMinutes(59);
            }
            else
            {
                feeItem.FeeOper.OperTime = this.FeeDate.Date.AddHours(23).AddMinutes(59);
            }
            //避免划价时间早于入院时间
            if (feeItem.FeeOper.OperTime < patient.PVisit.InTime)
            {
                feeItem.FeeOper.OperTime = dtNow;
            }


            if ((feeItem.FeeOper.OperTime > patient.PVisit.OutTime
                && patient.PVisit.OutTime > new DateTime(2000, 1, 1))
                || (feeItem.FeeOper.OperTime > patient.PVisit.PreOutTime
                && patient.PVisit.PreOutTime > new DateTime(2000, 1, 1)))
            {
                feeItem.FeeOper.OperTime = dtNow;
                //feeItem.FeeOper.OperTime = this.FeeDate.Date.AddHours(23).AddMinutes(50);
            }

            //这样修改是因为只有这样备注，才能自定义收费时间
            feeItem.FT.User03 = "NOCHANGEDATE";

            #endregion

            //feeItem.ChargeOper.OperTime = this.orderMgr.GetDateTimeFromSysDateTime();
            feeItem.ChargeOper.ID = "日计费";
            feeItem.FeeOper.ID = "日计费";
            //划价时间记录实际收取的项目的日期
            //feeItem.FeeOper.OperTime = this.orderMgr.GetDateTimeFromSysDateTime(); ;
            feeItem.SequenceNO = 0;
            feeItem.BalanceNO = 0;
            feeItem.BalanceState = "0";
            feeItem.FT.TotCost = undrug.Qty * undrug.Price;
            feeItem.FT.OwnCost = undrug.Qty * undrug.Price;
            feeItem.FTSource = new FS.HISFC.Models.Fee.Inpatient.FTSource("200");
            return feeItem;

            feeIntegrate.MessageType = FS.HISFC.Models.Base.MessType.N;
            if (feeIntegrate.FeeItem(patient, feeItem) == -1)
            {
                feeIntegrate.Rollback();
                this.Err = "调用住院收费业务层出错!" + this.feeIntegrate.Err;
                WriteErrInfo();
                return null;
            }
        }

        /// <summary>
        /// 检测用法是否为静脉滴注
        /// </summary>
        /// <param name="usageNO"></param>
        /// <returns></returns>
        private bool CheckIsIVD(string usageNO)
        {
            string usageSysNO = usageNO;
            string usageName = "";
            FS.HISFC.Models.Base.Const con = inteMgr.GetConstansObj("USAGE", usageNO) as FS.HISFC.Models.Base.Const;
            if (con != null)
            {
                usageSysNO = con.UserCode;
                usageName = con.Name;
            }
            if (usageSysNO == "IVD" || usageName.Replace(".", "").ToLower() == "ivd")
            {
                return true;
            }
            return false;
        }

        #region IJob 成员

        private string errInfo = "";

        public string Message
        {
            get
            {
                return errInfo;
            }
        }

        public int Start()
        {
            return this.SubFeeStart();
        }

        #endregion
    }
}
