using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using FS.HISFC.Models.Fee.Inpatient;
using FS.SOC.HISFC.BizProcess.CommonInterface;

namespace FS.SOC.Local.HISWebService.GuangZhou
{
    /// <summary>
    /// 住院接收终端确认收费信息
    /// </summary>
    public  class TerminalConfirm
    {
        protected FS.HISFC.BizLogic.Order.Order orderManager = new FS.HISFC.BizLogic.Order.Order();
        protected FS.HISFC.BizLogic.RADT.InPatient radtInpatient = new FS.HISFC.BizLogic.RADT.InPatient();
        protected FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();
        protected FS.HISFC.BizLogic.Fee.UndrugPackAge managerPack = new FS.HISFC.BizLogic.Fee.UndrugPackAge();
        protected FS.HISFC.BizLogic.Fee.InPatient inpatientManager = new FS.HISFC.BizLogic.Fee.InPatient();
        FS.HISFC.BizLogic.Terminal.TerminalConfirm terminalMgr = new FS.HISFC.BizLogic.Terminal.TerminalConfirm();
        FS.HISFC.BizLogic.Fee.ReturnApply returnApplyManager = new FS.HISFC.BizLogic.Fee.ReturnApply();
        /// <summary>
        /// 住院终端确认
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="alExecOrder"></param>
        /// <param name="alFeeOrder"></param>
        /// <param name="operDate"></param>
        /// <returns></returns>
        public int ComfirmFee(string inpatientNO, string applyNO, string confirmDeptCode, string confirmOperCode,ref string errInfo)
        {
            FS.FrameWork.Management.Connection.Operator.ID = confirmOperCode;
            FS.FrameWork.Management.Connection.Operator = CommonController.Instance.GetEmployee(FS.FrameWork.Management.Connection.Operator.ID);
            //取对应的操作员
            if (FS.FrameWork.Management.Connection.Operator == null)
            {
                errInfo = "确认失败，原因：传入的操作员编码在HIS系统中不存在";
                return -1;
            }

            //取对应的操作员科室
            FS.HISFC.Models.Base.Department confirmDept = CommonController.Instance.GetDepartment(confirmDeptCode);
             if (confirmDept == null)
            {
                errInfo = "确认失败，原因：传入的科室编码在HIS系统中不存在";
                return -1;
            }

            //设置操作员科室信息为所传科室confirmDeptCode
             FS.HISFC.Models.Base.Employee employee = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
             employee.Dept = confirmDept;

            if (string.IsNullOrEmpty(inpatientNO))
            {
                errInfo = "确认失败，原因：患者流水号为空";
                return -1;
            }
            //获取患者信息
            FS.HISFC.Models.RADT.PatientInfo patientInfo = radtInpatient.QueryPatientInfoByInpatientNO(inpatientNO);
            if (patientInfo == null || string.IsNullOrEmpty(patientInfo.ID))
            {
                errInfo = "确认失败，原因：获取" + inpatientNO + "的患者信息失败，原因：" + this.radtInpatient.Err;
                return -1;
            }

            DateTime operDate = inpatientManager.GetDateTimeFromSysDateTime();

            #region 根据申请单查找执行档信息

            ArrayList alExecOrder = this.orderManager.QueryExecOrderByApplyNO(patientInfo.ID, applyNO);
            if (alExecOrder == null)
            {
                errInfo = "获取申请单：" + applyNO + "对应医嘱信息失败，原因：" + this.orderManager.Err;
                return -1;
            }
            else if (alExecOrder.Count == 0)
            {
                errInfo = "确认失败，原因：没有需要确认的数据";
                return -1;
            }
            #endregion

            #region 拆分复合项目

            ArrayList alFeeOrder = new ArrayList();
            //终端确认标记
            FS.HISFC.Models.Fee.Inpatient.FTSource ftSource = new FS.HISFC.Models.Fee.Inpatient.FTSource("150");
            foreach (FS.HISFC.Models.Order.ExecOrder execOrder in alExecOrder)
            {
                //如果已经收费了，则不再进行处理
                if (execOrder.IsExec)
                {
                    continue;
                }

                execOrder.Order.ReciptNO = string.Empty;
                execOrder.Order.SequenceNO = 0;

                //执行科室默认
                if (execOrder.Order.ExeDept.ID == "")
                {
                    execOrder.Order.ExeDept = patientInfo.PVisit.PatientLocation.Dept.Clone();
                }
                execOrder.Order.User03 = execOrder.ID;
                execOrder.Order.Oper.OperTime = operDate;

                FS.HISFC.Models.Fee.Item.Undrug ug = feeIntegrate.GetItem(execOrder.Order.Item.ID);
                if (ug.UnitFlag == "1")
                {
                    ArrayList al = managerPack.QueryUndrugPackagesBypackageCode(execOrder.Order.Item.ID);
                    int seq= 0;
                    foreach (FS.HISFC.Models.Fee.Item.Undrug undrug in al)
                    {
                        seq++;
                        FS.HISFC.Models.Order.ExecOrder myorder = null;
                        decimal qty = execOrder.Order.Qty;
                        myorder = execOrder.Clone();
                        myorder.Name = undrug.Name;
                        myorder.Order.Item = undrug.Clone();
                        //重新取项目
                        myorder.Order.Item = feeIntegrate.GetItem(undrug.ID);
                        if (myorder.Order.Item == null)
                        {
                            errInfo = "确认失败，原因：获取项目失败" + myorder.Order.Item.ID;
                            return -1;
                        }

                        myorder.Order.Qty = qty * undrug.Qty;//数量==复合项目数量*小项目数量
                        myorder.Order.Item.Qty = qty * undrug.Qty;//数量==复合项目数量*小项目数量
                        myorder.Order.Package.ID = execOrder.Order.Item.ID;
                        myorder.Order.Package.Name = execOrder.Order.Item.Name;
                        myorder.Order.Package.Memo = myorder.Order.SequenceNO.ToString();
                        //需要重新将sequence_no重新赋值
                        myorder.Order.SequenceNO = seq;
                        //复合项目在费用表没有记录执行流水号 add by houwb 2011-4-7
                        myorder.User03 = execOrder.ID;
                        //添加到收费项目里面
                        myorder.Order.Oper.OperTime = operDate;
                        if (myorder.Order.Item.Price > 0)
                        {
                            //添加到收费项目里面
                            FS.HISFC.Models.Fee.Inpatient.FeeItemList itemList = this.ChangeToFeeItemList(patientInfo, myorder, ftSource,ref errInfo);
                            itemList.ConfirmOper.ID = confirmOperCode;
                            itemList.ConfirmOper.Dept.ID = confirmDeptCode;

                            if (string.IsNullOrEmpty(itemList.ChargeOper.ID))
                            {
                                itemList.ChargeOper.ID = confirmOperCode;
                                itemList.FeeOper.ID = itemList.ChargeOper.ID;
                            }

                            //默认确认人为000000
                            if (string.IsNullOrEmpty(itemList.ChargeOper.ID))
                            {
                                itemList.ChargeOper.ID = "T00001";
                                itemList.FeeOper.ID = "T00001";
                                execOrder.ChargeOper.ID = itemList.ChargeOper.ID;
                            }


                            if (string.IsNullOrEmpty(itemList.ChargeOper.Dept.ID))
                            {
                                itemList.ChargeOper.Dept.ID = confirmDeptCode;
                                itemList.FeeOper.Dept.ID = itemList.FeeOper.Dept.ID;
                            }

                            //默认确认科室为0000
                            if (string.IsNullOrEmpty(itemList.ChargeOper.Dept.ID))
                            {
                                itemList.ChargeOper.Dept.ID = "0000";
                                itemList.FeeOper.Dept.ID = itemList.FeeOper.Dept.ID;
                                execOrder.ChargeOper.Dept.ID = itemList.ChargeOper.Dept.ID;
                            }

                            alFeeOrder.Add(itemList);
                        }
                    }
                }
                else
                {
                    execOrder.Order.Item.MinFee.ID = ug.MinFee.ID;
                 
                    //添加到收费项目里面
                    FS.HISFC.Models.Fee.Inpatient.FeeItemList itemList = this.ChangeToFeeItemList(patientInfo, execOrder, ftSource,ref errInfo);
                    itemList.ConfirmOper.ID = confirmOperCode;
                    itemList.ConfirmOper.Dept.ID = confirmDeptCode;

                    if (string.IsNullOrEmpty(itemList.ChargeOper.ID))
                    {
                        itemList.ChargeOper.ID = confirmOperCode;
                        itemList.FeeOper.ID = itemList.ChargeOper.ID;
                    }

                    if (string.IsNullOrEmpty(itemList.ChargeOper.ID))
                    {
                        itemList.ChargeOper.ID = "T00001";
                        itemList.FeeOper.ID = "T00001";
                        execOrder.ChargeOper.ID = itemList.ChargeOper.ID;
                    }

                    if (string.IsNullOrEmpty(itemList.ChargeOper.Dept.ID))
                    {
                        itemList.ChargeOper.Dept.ID = confirmDeptCode;
                        itemList.FeeOper.Dept.ID = itemList.FeeOper.Dept.ID;
                    }

                    if (string.IsNullOrEmpty(itemList.ChargeOper.Dept.ID))
                    {
                        itemList.ChargeOper.Dept.ID = "0000";
                        itemList.FeeOper.Dept.ID = itemList.FeeOper.Dept.ID;
                        execOrder.ChargeOper.Dept.ID = itemList.ChargeOper.Dept.ID;
                    }

                    alFeeOrder.Add(itemList);
                }
            }

            #endregion

            #region 收费并且将可退数量变成0

            if (alFeeOrder.Count > 0)
            {
                if (this.feeIntegrate.FeeItem(patientInfo, ref alFeeOrder) == -1)
                {
                    errInfo = "计费失败，原因：" + this.feeIntegrate.Err;
                    return -1;
                }
                //更新可退数量
                foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItem in alFeeOrder)
                {
                    int iReturn = this.inpatientManager.UpdateConfirmNumForUndrug(feeItem.RecipeNO, feeItem.SequenceNO, feeItem.Item.Qty, feeItem.BalanceState);
                    if (iReturn < 0)
                    {
                        errInfo = "更新费用明细可退数量失败！" + this.inpatientManager.Err;
                        return -1;
                    }
                }
            }

            #endregion

            #region 插入终端确认表

            foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItem in alFeeOrder)
            {
                FS.HISFC.Models.Terminal.TerminalConfirmDetail detail = new FS.HISFC.Models.Terminal.TerminalConfirmDetail();

                #region 构建确认明细
                string applySequence = "";
                int sReturn = terminalMgr.GetNextSequence(ref applySequence);
                if (sReturn == -1)
                {
                    errInfo = "获取确认流水号失败，原因：" + terminalMgr.Err;
                    return -1;
                }
                detail.MoOrder = feeItem.Order.ID;//医嘱流水号 0
                detail.ExecMoOrder = feeItem.ExecOrder.ID;//医嘱内执行单流水号1 
                detail.Sequence = applySequence;//2
                detail.Apply.Item.ID = feeItem.Item.ID;//3
                detail.Apply.Item.Name = feeItem.Item.Name;//4
                detail.Apply.Item.ConfirmedQty = feeItem.Item.Qty;//5
                detail.Apply.ConfirmOperEnvironment.ID = feeItem.ConfirmOper.ID;//6
                detail.Apply.ConfirmOperEnvironment.Dept.ID = feeItem.ConfirmOper.Dept.ID;//7
                detail.Apply.ConfirmOperEnvironment.OperTime = operDate;//8
                //---------------------------------------------------------------------------------
                detail.Status.ID = "0";//9 0-正常，1-取消，2-退费
                detail.Apply.Patient.ID = feeItem.Patient.ID;
                detail.Apply.Item.RecipeNO = feeItem.RecipeNO;
                detail.Apply.Item.SequenceNO = feeItem.SequenceNO;
                //{810581A3-6DF5-49af-8A5F-D7F843CBEA89}
                detail.ExecDevice = feeItem.Item.User01;
                detail.Oper.ID = feeItem.Item.User02;
                #endregion
                if (terminalMgr.InsertInpatientConfirmDetail(detail) == -1)
                {
                    errInfo = "插入终端确认明细失败，原因：" + this.terminalMgr.Err;
                    return -1;
                }
            }

            #endregion

            #region 更新医嘱确认和收费

            foreach (FS.HISFC.Models.Order.ExecOrder execOrder in alExecOrder)
            {
                execOrder.ExecOper.OperTime = operDate;
                execOrder.ChargeOper.OperTime = operDate;

                if (!execOrder.IsExec)
                {
                    execOrder.IsExec = true;
                    execOrder.IsConfirm = true;

                    int iReturn = this.orderManager.UpdateRecordExec(execOrder);
                    if (iReturn < 0)
                    {
                        errInfo = "更新医嘱确认信息失败，原因：" + this.orderManager.Err;
                        return -1;
                    }
                    else if (iReturn == 0)
                    {
                        errInfo = "更新医嘱确认信息失败！【" + execOrder.Order.Name + "】已经确认，请刷新！";
                        return -1;
                    }

                    iReturn = this.orderManager.UpdateOrderStatus(execOrder.Order.ID, 2);
                    if (iReturn < 0)
                    {
                        errInfo = "更新医嘱主档执行信息失败！" + this.orderManager.Err;
                        return -1;
                    }
                }

                if (!execOrder.IsCharge)
                {
                    execOrder.IsCharge = true;
                    execOrder.IsValid = true;

                    if (this.orderManager.UpdateChargeExec(execOrder) <= 0)
                    {
                        errInfo = "更新医嘱收费信息失败，原因：" + this.orderManager.Err;
                        return -1;
                    }
                }

            }

            #endregion

            return 1;
        }

        /// <summary>
        /// 住院取消终端确认
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="alExecOrder"></param>
        /// <param name="alFeeOrder"></param>
        /// <param name="oeprDate"></param>
        /// <returns></returns>
        public int CancelConfirmFee(string inpatientNO, string applyNO, string confirmDeptCode, string confirmOperCode, ref string errInfo)
        {
            FS.FrameWork.Management.Connection.Operator.ID = confirmOperCode;
            FS.FrameWork.Management.Connection.Operator = CommonController.Instance.GetEmployee(FS.FrameWork.Management.Connection.Operator.ID);
            //取对应的操作员
            if (FS.FrameWork.Management.Connection.Operator == null)
            {
                errInfo = "确认失败，原因：传入的操作员编码在HIS系统中不存在";
                return -1;
            }

            //取对应的操作员
            if (CommonController.Instance.GetDepartment(confirmDeptCode) == null)
            {
                errInfo = "确认失败，原因：传入的科室编码在HIS系统中不存在";
                return -1;
            }

            if (string.IsNullOrEmpty(inpatientNO))
            {
                errInfo = "取消确认失败，原因：患者流水号为空";
                return -1;
            }
            //获取患者信息
            FS.HISFC.Models.RADT.PatientInfo patientInfo = radtInpatient.QueryPatientInfoByInpatientNO(inpatientNO);
            if (patientInfo == null || string.IsNullOrEmpty(patientInfo.ID))
            {
                errInfo = "取消确认失败，原因：获取" + inpatientNO + "的患者信息失败，原因：" + this.radtInpatient.Err;
                return -1;
            }

            DateTime operDate = inpatientManager.GetDateTimeFromSysDateTime();

            #region 根据申请单号查找医嘱

            //根据申请单号查找对应执行档
            ArrayList alExecOrder = this.orderManager.QueryExecOrderByApplyNO(patientInfo.ID, applyNO);
            if (alExecOrder == null)
            {
                errInfo = "获取申请单：" + applyNO + "对应医嘱信息失败，原因：" + this.orderManager.Err;
                return -1;
            }
            else if (alExecOrder.Count == 0)
            {
                errInfo = "确认失败，原因：没有需要确认的数据";
                return -1;
            }

            #endregion

            List<FS.HISFC.Models.Terminal.TerminalConfirmDetail> alComfirmDetail = null;

            foreach (FS.HISFC.Models.Order.ExecOrder execOrder in alExecOrder)
            {
                if (execOrder.IsExec)//已执行则取消确认
                {
                    //根据医嘱流水号和执行档流水号查询确认信息
                    alComfirmDetail = this.terminalMgr.QueryTerminalConfirmDetailByMoOrderAndExeMoOrder(execOrder.Order.ID, execOrder.ID);
                    if (alComfirmDetail == null)
                    {
                        errInfo = "获取检验确认信息失败，原因：" + this.orderManager.Err;
                        return -1;
                    }

                    foreach (FS.HISFC.Models.Terminal.TerminalConfirmDetail terminalConfirmDetail in alComfirmDetail)
                    {
                        #region 医嘱

                        if (this.terminalMgr.CancelInpatientConfirmMoOrder(terminalConfirmDetail) <= 0)
                        {
                            errInfo = "更新医嘱执行档失败，原因：" + this.terminalMgr.Err;
                            return -1;
                        }

                        #endregion

                        #region 费用

                        if (terminalConfirmDetail.Status.ID.Equals("0"))//正常情况下才退费
                        {
                            #region 费用

                            FeeItemList feeItemListTemp = this.inpatientManager.GetItemListByRecipeNO(terminalConfirmDetail.Apply.Item.RecipeNO, terminalConfirmDetail.Apply.Item.SequenceNO, terminalConfirmDetail.Apply.Item.Item.ItemType);
                            if (feeItemListTemp.Days == 0)
                            {
                                feeItemListTemp.Days = 1;
                            }

                            //更新确认数量
                            if (inpatientManager.UpdateConfirmNumForUndrug(feeItemListTemp.RecipeNO, feeItemListTemp.SequenceNO,
                                0, feeItemListTemp.BalanceState) <= 0)
                            {

                                errInfo = "取消确认失败，原因：更新费用明细确认数量失败，" + this.inpatientManager.Err;
                                return -1;
                            }

                            if (feeItemListTemp.NoBackQty > 0)
                            {
                                feeItemListTemp.Item.Qty = terminalConfirmDetail.Apply.Item.ConfirmedQty;
                                feeItemListTemp.NoBackQty = 0;
                                feeItemListTemp.FT.TotCost = feeItemListTemp.Item.Price * feeItemListTemp.Item.Qty / feeItemListTemp.Item.PackQty;
                                feeItemListTemp.FT.OwnCost = feeItemListTemp.FT.TotCost;
                                feeItemListTemp.IsNeedUpdateNoBackQty = true;

                                string applyBillCode = returnApplyManager.GetReturnApplyBillCode();
                                if (applyBillCode == null || applyBillCode == string.Empty)
                                {
                                    errInfo = "取消确认失败，原因：获取退费申请方号出错!";
                                    return -1;
                                }
                                string msg = string.Empty;

                                if (this.feeIntegrate.QuitFeeApply(patientInfo, feeItemListTemp, false, applyBillCode, operDate, ref msg) == -1)
                                {
                                    errInfo = "取消确认失败，原因：插入退费申请失败，" + feeIntegrate.Err;
                                    return -1;
                                }

                            }
                            else
                            {
                                ArrayList al = returnApplyManager.QueryReturnApplysByRecipeNoSequenceNo(patientInfo.ID, feeItemListTemp.RecipeNO, feeItemListTemp.SequenceNO.ToString());
                                if (al == null)
                                {
                                    errInfo = "取消确认失败，原因：获取退费申请信息失败，" + returnApplyManager.Err;
                                    return -1;
                                }

                                foreach (FS.HISFC.Models.Fee.ReturnApply returnApply in al)
                                {
                                    if (returnApply.CancelType != FS.HISFC.Models.Base.CancelTypes.Canceled)
                                    {
                                        continue;
                                    }

                                    returnApply.CancelType = FS.HISFC.Models.Base.CancelTypes.Valid;
                                    returnApply.ConfirmOper.ID = returnApplyManager.Operator.ID;
                                    returnApply.ConfirmOper.OperTime = operDate;

                                    if (returnApplyManager.ConfirmApply(returnApply) <= 0)
                                    {
                                        errInfo = "取消确认失败，原因：核准退费申请失败!" + returnApplyManager.Err;
                                        return -1;
                                    }

                                    feeItemListTemp.Item.Qty = terminalConfirmDetail.Apply.Item.ConfirmedQty;
                                    feeItemListTemp.NoBackQty = 0;
                                    feeItemListTemp.FT.TotCost = feeItemListTemp.Item.Price * feeItemListTemp.Item.Qty / feeItemListTemp.Item.PackQty;
                                    feeItemListTemp.FT.OwnCost = feeItemListTemp.FT.TotCost;
                                    feeItemListTemp.IsNeedUpdateNoBackQty = false;
                                    if (this.feeIntegrate.QuitItem(patientInfo, feeItemListTemp) == -1)
                                    {
                                        errInfo = "取消确认失败，原因：退费失败，" + this.feeIntegrate.Err;
                                        return -1;
                                    }
                                }
                            }

                            #endregion
                        }

                        #endregion

                        #region 确认明细

                        if (this.terminalMgr.CancelInpatientConfirmDetail(terminalConfirmDetail) <= 0)
                        {
                            errInfo = "更新确认明细失败，原因：" + terminalMgr.Err;
                            return -1;
                        }

                        #endregion

                    }
                }
                //else//未执行则取消执行档
                //{
                //    //未收费 直接作废执行档
                //    int i = this.orderManager.DcExecImmediate(execOrder, execOrder.Order.DcReason);
                //    if (i == -1)
                //    {
                //        this.feeIntegrate.Rollback();
                //        FS.FrameWork.Management.PublicTrans.RollBack();
                //        errInfo = "作废执行档失败，原因：" + this.orderManager.Err;
                //        return -1;
                //    }
                //    else if (i == 0)
                //    {
                //        this.feeIntegrate.Rollback();
                //        FS.FrameWork.Management.PublicTrans.RollBack();
                //        errInfo = "作废执行档失败，原因：已收费，不允许作废执行档";
                //        return -1;
                //    }

                //}

            }

            return 1;

        }

        /// <summary>
        ///  将医嘱转换为费用实体
        /// </summary>
        /// <param name="execOrder"></param>
        /// <returns></returns>
        private FS.HISFC.Models.Fee.Inpatient.FeeItemList ChangeToFeeItemList(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.HISFC.Models.Order.ExecOrder execOrder, FS.HISFC.Models.Fee.Inpatient.FTSource ftSource, ref string errInfo)
        {
            FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList = new FS.HISFC.Models.Fee.Inpatient.FeeItemList();

            feeItemList.Item = execOrder.Order.Item.Clone();

            feeItemList.RecipeNO = execOrder.Order.ReciptNO;
            feeItemList.SequenceNO = execOrder.Order.SequenceNO;


            if (execOrder.Order.HerbalQty == 0)
            {
                execOrder.Order.HerbalQty = 1;
            }

            //{F5477FAB-9832-4234-AC7F-ED49654948B4}
            FS.HISFC.BizLogic.Fee.PactUnitInfo pactManager = new FS.HISFC.BizLogic.Fee.PactUnitInfo();
            decimal price = feeItemList.Item.Price;
            decimal orgPrice = 0;
            //取最新的价格
            FS.HISFC.Models.Fee.Item.Undrug undrug = this.feeIntegrate.GetItem(feeItemList.Item.ID);
            if (undrug == null)
            {
                errInfo = "获取项目" + feeItemList.Item.Name + "的失败，原因：" + feeIntegrate.Err;
                return null;
            }

            if (feeIntegrate.GetPriceForInpatient(patientInfo, undrug, ref price, ref orgPrice) == -1)
            {
                errInfo = "获取项目" + feeItemList.Item.Name + "的价格失败，原因：" + feeIntegrate.Err;
                return null;
            }
            feeItemList.Item.Price = price;
            // {54B0C254-3897-4241-B3BD-17B19E204C8C}
            // 原始价格（本来应收价格，不考虑合同单位因素）
            feeItemList.Item.DefPrice = orgPrice;

            //录入界面已经将QTY 赋值
            feeItemList.Item.Qty = execOrder.Order.Qty;// *order.HerbalQty;
            //增加付数的赋值 {AE53ACB5-3684-42e8-BF28-88C2B4FF2360}
            feeItemList.Days = execOrder.Order.HerbalQty;

            feeItemList.Item.PriceUnit = execOrder.Order.Unit;//单位重新付
            feeItemList.RecipeOper.Dept = execOrder.Order.ReciptDept.Clone();//开方科室
            feeItemList.RecipeOper.ID = execOrder.Order.ReciptDoctor.ID;
            feeItemList.RecipeOper.Name = execOrder.Order.ReciptDoctor.Name;
            feeItemList.ExecOper = execOrder.Order.ExecOper.Clone();//执行人
            feeItemList.StockOper.Dept = execOrder.Order.StockDept.Clone();//扣库科室
            feeItemList.ExecOper.Dept = execOrder.Order.ExeDept;//执行科室

            if (feeItemList.Item.PackQty == 0)
            {
                feeItemList.Item.PackQty = 1;
            }
            feeItemList.FT.TotCost = FS.FrameWork.Public.String.FormatNumber((feeItemList.Item.Price * feeItemList.Item.Qty / feeItemList.Item.PackQty), 2);

            // 原始总费用（本来应收费用，不考虑合同单位因素）
            // {54B0C254-3897-4241-B3BD-17B19E204C8C}
            feeItemList.FT.DefTotCost = FS.FrameWork.Public.String.FormatNumber((feeItemList.Item.DefPrice * feeItemList.Item.Qty / feeItemList.Item.PackQty), 2);

            feeItemList.FT.OwnCost = feeItemList.FT.TotCost;
            feeItemList.IsBaby = execOrder.Order.IsBaby;
            feeItemList.IsEmergency = execOrder.Order.IsEmergency;
            feeItemList.Order = execOrder.Order.Clone();
            feeItemList.ExecOrder = execOrder.Clone();
            feeItemList.NoBackQty = feeItemList.Item.Qty;
            feeItemList.FTRate.OwnRate = 1;
            feeItemList.BalanceState = "0";
            feeItemList.ChargeOper = execOrder.ChargeOper.Clone();//计费人
            feeItemList.FeeOper = execOrder.ChargeOper.Clone();//计费人
            //数量大于零 则为正交易 数量小于零 则为负交易
            feeItemList.TransType = feeItemList.Item.Qty > 0 ? FS.HISFC.Models.Base.TransTypes.Positive : FS.HISFC.Models.Base.TransTypes.Negative;

            //费用数据来源
            feeItemList.FTSource = ftSource.Clone();

            feeItemList.UndrugComb.ID = execOrder.Order.Package.ID;
            feeItemList.UndrugComb.Name = execOrder.Order.Package.Name;

            return feeItemList;
        }

        public void SetTrans(System.Data.IDbTransaction trans)
        {
            this.inpatientManager.SetTrans(trans);
            this.managerPack.SetTrans(trans);
            this.terminalMgr.SetTrans(trans);
            this.feeIntegrate.SetTrans(trans);
            this.radtInpatient.SetTrans(trans);
            this.orderManager.SetTrans(trans);
        }
     
    }
}
