using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.HISFC.Models.Base;

namespace FS.HISFC.BizProcess.Integrate
{
    /// <summary>
    /// [功能描述: 整合的医嘱管理类]<br></br>
    /// [创 建 者: wolf]<br></br>
    /// [创建时间: 2004-10-12]<br></br>
    /// <修改记录
    ///		修改人=''
    ///		修改时间=''
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    public class Order : IntegrateBase
    {
        #region 变量
        protected FS.HISFC.BizLogic.Order.Order orderManager = new FS.HISFC.BizLogic.Order.Order();
        private FS.HISFC.BizProcess.Integrate.Pharmacy managerPharmacy = null;

        protected FS.HISFC.BizProcess.Integrate.Pharmacy ManagerPharmacy
        {
            get
            {
                if (managerPharmacy == null)
                {
                    managerPharmacy = new Pharmacy();
                }
                return managerPharmacy;
            }
        }

        private FS.HISFC.BizProcess.Integrate.Fee managerFee = null;

        protected FS.HISFC.BizProcess.Integrate.Fee ManagerFee
        {
            get
            {
                if (managerFee == null)
                {
                    managerFee = new Fee();
                }
                return managerFee;
            }
        }

        protected FS.HISFC.BizLogic.Fee.UndrugPackAge managerPack = new FS.HISFC.BizLogic.Fee.UndrugPackAge();
        protected FS.HISFC.BizLogic.RADT.InPatient managerRADT = new FS.HISFC.BizLogic.RADT.InPatient();
        protected FS.HISFC.BizLogic.Order.OutPatient.Order outOrderManager = new FS.HISFC.BizLogic.Order.OutPatient.Order();
        protected FS.HISFC.BizLogic.Order.OrderBill orderBillManager = new FS.HISFC.BizLogic.Order.OrderBill();
        protected FS.HISFC.BizLogic.Manager.Department deptMangager = new FS.HISFC.BizLogic.Manager.Department();
        //{AC6A5576-BA29-4dba-8C39-E7C5EBC7671E} 增加医疗组处理
        protected FS.HISFC.BizLogic.Order.MedicalTeamForDoct medicalTeamForDoctBizLogic = new FS.HISFC.BizLogic.Order.MedicalTeamForDoct();

        private static FS.HISFC.BizProcess.Integrate.Common.ControlParam ctrlIntegrate = null;

        public static FS.HISFC.BizProcess.Integrate.Common.ControlParam CtrlIntegrate
        {
            get
            {
                if (ctrlIntegrate == null)
                {
                    ctrlIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
                }
                return Order.ctrlIntegrate;
            }
        }


        /// <summary>
        /// 获取价格接口
        /// </summary>
        FS.HISFC.BizProcess.Interface.Fee.IGetItemPrice IGetItemPrice = null;

        /// <summary>
        /// 是否支持更新转科，膳食，护理的自动更新
        /// </summary>
        public bool IsUpdateOther = true;

        /// <summary>
        /// 欠费判断类型
        /// </summary>
        private FS.HISFC.Models.Base.MessType messType = MessType.M;

        /// <summary>
        /// 小时频次
        /// </summary>
        private static string hourFerquenceID = string.Empty;

        private static Hashtable hsDepts = new Hashtable();

        #endregion

        #region 属性

        /// <summary>
        /// 是否判断欠费，欠费是否提示
        /// </summary>
        public FS.HISFC.Models.Base.MessType MessageType
        {
            set
            {
                messType = value;
            }
            get
            {
                return messType;
            }
        }

        #endregion

        #region 函数

        public override void SetTrans(System.Data.IDbTransaction trans)
        {
            managerRADT.SetTrans(trans);
            orderManager.SetTrans(trans);
            outOrderManager.SetTrans(trans);
            ManagerPharmacy.SetTrans(trans);
            fee.SetTrans(trans);
            managerPack.SetTrans(trans);
            orderBillManager.SetTrans(trans);
            deptMangager.SetTrans(trans);

            this.trans = trans;
        }

        /// <summary>
        /// 更新下次分解时间
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <param name="days"></param>
        /// <returns></returns>
        public int UpdateDecoTime(string inpatientNo, int days)
        {
            this.SetDB(orderManager);
            return orderManager.UpdateDecoTime(inpatientNo, days);
        }

        /// <summary>
        /// 更新下次分解时间
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <param name="dtNextTime"></param>
        /// <returns></returns>
        public int UpdateDecoTime(string inpatientNo, DateTime dtNextTime)
        {
            this.SetDB(orderManager);
            return orderManager.UpdateDecoTime(inpatientNo, dtNextTime);
        }

        #endregion

        #region 大函数

        #region 医嘱审核

        /// <summary>
        ///  审核保存，审核医嘱（对临时医嘱进行收费）
        /// 需要对fee进行Commit，RollBack操作
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="alOrders"></param>
        /// <param name="isLong">是否长期医嘱</param>
        /// <param name="isCharge">是否收费？ 欠费患者可以只保存不收费</param>
        //public int SaveChecked(FS.HISFC.Models.RADT.PatientInfo patient, ArrayList alOrders, bool isLong, string nurseCode, bool isCharge, bool iSCDCharge)
        //{

        //    //收费开关 判断是(true)--药房摆药时收费 还是(false)--审核/分解时收费
        //    //True 护士站收费 False 药房收费
        //    bool isNurseCharge = GetIsNurseCharge(ref this.trans);

        //    DateTime dt = orderManager.GetDateTimeFromSysDateTime();

        //    ArrayList alFeeOrder = new ArrayList(); //收费医嘱
        //    ArrayList alSendDrug = new ArrayList(); //需要发药药品
        //    Hashtable hsCombo = new Hashtable();

        //    #region 华南版本已作废 {2AFC76CB-3353-4865-AEB4-AFBEE09DD1D7}
        //    //FS.HISFC.BizProcess.Integrate.Common.ControlParam con = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
        //    ////基本信息维护界面是否维护医嘱单打印属性  华南版本已作废
        //    //bool val = con.GetControlParam<bool>("B00002",false,);
        //    //ArrayList itemList = null;
        //    //if (val)
        //    //{
        //    //    FS.HISFC.BizLogic.Fee.Item item = new FS.HISFC.BizLogic.Fee.Item();
        //    //    itemList = item.SelectAllItemByOrderPrint("1");
        //    //}
        //    #endregion

        //    #region 审核医嘱

        //    //记录上一个组号
        //    string strComboNo = "";

        //    //长期医嘱
        //    for (int i = 0; i < alOrders.Count; i++) //长期医嘱
        //    {
        //        if (isLong)
        //        {
        //            #region 长期医嘱处理
        //            FS.HISFC.Models.Order.Inpatient.Order order = alOrders[i] as FS.HISFC.Models.Order.Inpatient.Order;

        //            if (order.Status == 0)//未审核医嘱
        //            {
        //                #region 未审核医嘱处理

        //                if (order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug) //药品
        //                {
        //                    //已经修改发药申请科室规则，此处不再取所在科室
        //                    //执行科室为护士所在科室
        //                    //order.ExeDept = order.Patient.PVisit.PatientLocation.Dept.Clone();
        //                    order.Patient.Name = patient.Name;
        //                }
        //                if (order.Combo.ID != strComboNo)
        //                {
        //                    ArrayList alSubtbl = orderManager.QuerySubtbl(order.Combo.ID);//查询附材

        //                    for (int f = 0; f < alSubtbl.Count; f++)//附材处理
        //                    {
        //                        if (((FS.HISFC.Models.Order.Order)alSubtbl[f]).Status == 0)
        //                        {
        //                            if (orderManager.ConfirmAndExecOrder((FS.HISFC.Models.Order.Inpatient.Order)alSubtbl[f], false, "", dt) == -1) //更新收费标记
        //                            {
        //                                MessageBox.Show("附材审核错误:" + orderManager.Err);
        //                                this.Err = orderManager.Err;
        //                                return -1;
        //                            }
        //                        }
        //                    }
        //                    strComboNo = order.Combo.ID;
        //                }
        //                if (this.UpdateOther(order) == -1)
        //                    return -1;
        //                //审核医嘱-不收费用
        //                if (orderManager.ConfirmAndExecOrder(order, false, "", dt) == -1)
        //                {
        //                    MessageBox.Show("医嘱审核错误:" + orderManager.Err);
        //                    this.Err = orderManager.Err;
        //                    return -1;
        //                }
        //                #endregion

        //                #region 实现对危重患者的状态更新

        //                if (order.Item.SysClass.ID.ToString() == "UF" && order.Item.Name.IndexOf("病重") != -1)
        //                {
        //                    if (managerRADT.UpdateCondition_Info("1", patient.ID) < 0)
        //                    {
        //                        this.Err = managerRADT.Err;
        //                        return -1;
        //                    }
        //                }
        //                if (order.Item.SysClass.ID.ToString() == "UF" && order.Item.Name.IndexOf("病危") != -1)
        //                {
        //                    if (managerRADT.UpdateCondition_Info("2", patient.ID) < 0)
        //                    {
        //                        this.Err = managerRADT.Err;
        //                        return -1;
        //                    }
        //                }
        //                #endregion
        //            }
        //            else if (order.Status == 3 || order.Status == 4)//作废的
        //            {
        //                if (orderManager.ConfirmOrder(order, false, dt) == -1)
        //                {
        //                    this.Err = orderManager.Err;
        //                    return -1;
        //                }

        //                if (order.Status == 3)
        //                {
        //                    if (this.UpdateOther(order) == -1)
        //                        return -1;//{A921CA7F-6607-406c-9DF2-C2A58C792ED4}

        //                    #region 退费医嘱和附材
        //                    if (!hsCombo.Contains(order.Combo.ID))
        //                    {
        //                        ArrayList alSubtbl = orderManager.QuerySubtbl(order.Combo.ID);//查询附材

        //                        for (int f = 0; f < alSubtbl.Count; f++)//附材处理
        //                        {
        //                            if (orderManager.ConfirmOrder((FS.HISFC.Models.Order.Inpatient.Order)alSubtbl[f], false, dt) == -1)
        //                            {
        //                                this.Err = orderManager.Err;
        //                                return -1;
        //                            }
        //                        }
        //                        if (fee.SaveApply(order.Combo.ID, FS.FrameWork.Function.NConvert.ToInt32(order.Item.User03)) == -1)
        //                        {
        //                            this.Err = fee.Err;
        //                            return -1;
        //                        }

        //                        hsCombo.Add(order.Combo.ID, order.Combo);
        //                    }
        //                    #endregion

        //                    #region 停止医嘱的时候状态变回普通

        //                    if (order.Item.SysClass.ID.ToString() == "UF" && order.Item.Name.IndexOf("病重") != -1
        //                                        || order.Item.SysClass.ID.ToString() == "UF" && order.Item.Name.IndexOf("病危") != -1)
        //                    {

        //                        if (managerRADT.UpdateCondition_Info("0", patient.ID) != -1)
        //                        {
        //                            this.Err = managerRADT.Err;
        //                            return -1;
        //                        }
        //                    }
        //                    #endregion
        //                }
        //            }
        //            else
        //            {
        //                this.Err = "医嘱已经发生变化，请刷新屏幕！";
        //                return -1;
        //            }
        //            #endregion
        //        }
        //        else
        //        {
        //            #region 临时医嘱

        //            ManagerFee.MessageType = messType;
        //            FS.HISFC.Models.Order.Inpatient.Order order = alOrders[i] as FS.HISFC.Models.Order.Inpatient.Order;

        //            if (ConfirmShortOrder(order, patient, isNurseCharge, nurseCode, alFeeOrder, alSendDrug, dt, isCharge, iSCDCharge) == -1)
        //            {
        //                return -1;
        //            }

        //            //退费的自动做退费申请
        //            if (order.Status == 3)
        //            {
        //                if (!hsCombo.Contains(order.Combo.ID))
        //                {
        //                    hsCombo.Add(order.Combo.ID, order.Combo);

        //                    if (fee.SaveApply(order.Combo.ID, FS.FrameWork.Function.NConvert.ToInt32(order.Item.User03)) == -1)
        //                    {
        //                        this.Err = fee.Err;
        //                        return -1;
        //                    }
        //                }
        //            }

        //            #endregion
        //        }
        //    }
        //    #endregion

        //    #region 收费

        //    if (isLong == false && alFeeOrder.Count > 0) //临时医嘱
        //    {
        //        fee.MessageType = messType;
        //        if (fee.FeeItem(patient, ref alFeeOrder) == -1)
        //        {
        //            this.Err = fee.Err;
        //            return -1;
        //        }
        //    }

        //    #endregion

        //    //MessageBox.Show("发送到药房？？");
        //    //添加RecipeNo给药房
        //    System.Collections.Hashtable hsRecipe = new Hashtable();
        //    foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItem in alFeeOrder)
        //    {
        //        if (feeItem.Order.Item.ItemType == EnumItemType.Drug)
        //        {
        //            hsRecipe.Add(feeItem.Order.ID, feeItem);
        //            {
        //                if (!hsRecipe.ContainsKey(feeItem.Order.ID))
        //                {
        //                    hsRecipe.Add(feeItem.Order.ID, feeItem);
        //                }
        //            }
        //        }
        //    }

        //    if (alFeeOrder.Count > 0)
        //    {
        //        foreach (FS.HISFC.Models.Order.Inpatient.Order drugOrder in alSendDrug)
        //        {
        //            //{A8ABA1D3-C025-43d3-A02C-60FFB5A166AF}  需判断HashTable内是否存在
        //            //当设置为药房收费时，alFeeOrder内不包含药品数据
        //            if (hsRecipe.ContainsKey(drugOrder.ID))
        //            {
        //                FS.HISFC.Models.Fee.Inpatient.FeeItemList tempFee = hsRecipe[drugOrder.ID] as FS.HISFC.Models.Fee.Inpatient.FeeItemList;
        //                drugOrder.ReciptNO = tempFee.RecipeNO;
        //                drugOrder.SequenceNO = tempFee.SequenceNO;
        //            }
        //        }
        //    }

        //    #region 发送发药申请

        //    if (alSendDrug.Count > 0)
        //    {
        //        if (this.SendDrugWithOrderList(alSendDrug, isNurseCharge, dt) == -1)
        //        {
        //            return -1;
        //        }
        //    }
        //    #endregion

        //    return 0;
        //}

        /// <summary>
        /// 审核保存，审核医嘱（对临时医嘱进行收费）
        /// 需要对fee进行Commit，RollBack操作
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="alOrders"></param>
        /// <param name="isLong"></param>
        /// <param name="nurseCode"></param>
        /// <param name="quitFee"></param>
        /// <param name="isCharge">是否收费？ 欠费患者可以只保存不收费</param>
        /// <param name="iSCDCharge">出院带药是否判断警戒线？ 欠费患者出院带药可以不判断警戒线</param>
        /// <returns></returns>
        public int SaveChecked(FS.HISFC.Models.RADT.PatientInfo patient, ArrayList alOrders, bool isLong, string nurseCode, bool quitFee, bool isCharge, bool iSCDCharge)
        {
            //收费开关 判断是(true)--药房摆药时收费 还是(false)--审核/分解时收费
            //True 护士站收费 False 药房收费
            bool bCharge = GetIsNurseCharge(ref this.trans);

            DateTime dt = orderManager.GetDateTimeFromSysDateTime();

            string strComboNo = "";

            Hashtable hsCombo = new Hashtable();

            //收费医嘱
            ArrayList alFeeOrder = new ArrayList();
            //需要发药药品
            ArrayList alSendDrug = new ArrayList();

            #region 打印医嘱单属性
            //FS.HISFC.BizProcess.Integrate.Common.ControlParam con = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
            //bool val = con.GetControlParam<bool>("B00002");
            //ArrayList itemList = null;
            //if (val)
            //{
            //    FS.HISFC.BizLogic.Fee.Item item = new FS.HISFC.BizLogic.Fee.Item();
            //    itemList = item.SelectAllItemByOrderPrint("1");
            //}
            #endregion

            //自动退费提示信息 用于部分不能直接退费的提示信息
            Hashtable hsQuitFeeWarning = new Hashtable();

            for (int i = 0; i < alOrders.Count; i++) //长期医嘱
            {
                if (isLong)
                {
                    #region 长期医嘱处理
                    FS.HISFC.Models.Order.Inpatient.Order order = alOrders[i] as FS.HISFC.Models.Order.Inpatient.Order;

                    if (order.Status == 0)//未审核医嘱
                    {
                        #region 未审核医嘱处理

                        if (order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug) //药品
                        {
                            //已经修改发药申请科室规则，此处不再取所在科室
                            //执行科室为护士所在科室
                            //order.ExeDept = order.Patient.PVisit.PatientLocation.Dept.Clone();
                            order.Patient.Name = patient.Name;
                        }
                        else//非药品不处理执行科室
                        {

                        }
                        if (order.Combo.ID != strComboNo)
                        {
                            ArrayList alSubtbl = orderManager.QuerySubtbl(order.Combo.ID);//查询附材

                            for (int f = 0; f < alSubtbl.Count; f++)//附材处理
                            {
                                if (((FS.HISFC.Models.Order.Order)alSubtbl[f]).Status == 0)
                                {
                                    if (orderManager.ConfirmAndExecOrder((FS.HISFC.Models.Order.Inpatient.Order)alSubtbl[f], false, "", dt) == -1) //更新收费标记
                                    {
                                        this.Err = orderManager.Err;
                                        return -1;
                                    }
                                }
                            }
                            strComboNo = order.Combo.ID;
                        }
                        if (this.UpdateOther(order) == -1) return -1;
                        //审核医嘱-不收费用
                        if (orderManager.ConfirmAndExecOrder(order, false, "", dt) == -1)
                        {
                            this.Err = orderManager.Err;
                            return -1;
                        }
                        #endregion

                        #region 实现对危重患者的状态更新

                        if (order.Item.SysClass.ID.ToString() == "UF" && order.Item.Name.IndexOf("病重") != -1)
                        {

                            if (managerRADT.UpdateCondition_Info("1", patient.ID) < 0)
                            {
                                this.Err = managerRADT.Err;
                                return -1;
                            }
                        }
                        if (order.Item.SysClass.ID.ToString() == "UF" && order.Item.Name.IndexOf("病危") != -1)
                        {

                            if (managerRADT.UpdateCondition_Info("2", patient.ID) < 0)
                            {
                                this.Err = managerRADT.Err;
                                return -1;
                            }
                        }
                        #endregion
                    }
                    else if (order.Status == 3 || order.Status == 4)//作废的
                    {
                        if (orderManager.ConfirmOrder(order, false, dt) == -1)
                        {
                            this.Err = orderManager.Err;
                            return -1;
                        }

                        if (order.Status == 3)
                        {
                            if (this.UpdateOther(order) == -1)
                                return -1;//{A921CA7F-6607-406c-9DF2-C2A58C792ED4}

                            #region 退费医嘱和附材

                            if (!hsCombo.Contains(order.Combo.ID))
                            {
                                ArrayList alSubtbl = orderManager.QuerySubtbl(order.Combo.ID);//查询附材

                                for (int f = 0; f < alSubtbl.Count; f++)//附材处理
                                {
                                    if (orderManager.ConfirmOrder((FS.HISFC.Models.Order.Inpatient.Order)alSubtbl[f], false, dt) == -1)
                                    {
                                        this.Err = orderManager.Err;
                                        return -1;
                                    }
                                }
                                if (fee.SaveApply(patient, order.Combo.ID, FS.FrameWork.Function.NConvert.ToInt32(order.Item.User03), quitFee, ref hsQuitFeeWarning) == -1)
                                {
                                    this.Err = fee.Err;
                                    return -1;
                                }

                                hsCombo.Add(order.Combo.ID, order.Combo);
                            }
                            #endregion

                            #region 停止医嘱的时候状态变回普通

                            if (order.Item.SysClass.ID.ToString() == "UF" && order.Item.Name.IndexOf("病重") != -1
                                                || order.Item.SysClass.ID.ToString() == "UF" && order.Item.Name.IndexOf("病危") != -1)
                            {

                                if (managerRADT.UpdateCondition_Info("0", patient.ID) == -1)
                                {
                                    this.Err = managerRADT.Err;
                                    return -1;
                                }
                            }
                            #endregion
                        }
                    }
                    else
                    {
                        this.Err = order.Patient.PVisit.PatientLocation.Bed.ID.Substring(4) + "床 " + order.Patient.Name + "\r\n\r\n组号:" + order.SubCombNO.ToString() + " " + order.Item.Name + "\r\n医嘱已经发生变化，请刷新屏幕！";
                        return -1;
                    }
                    #endregion
                }
                else
                {
                    #region 临时医嘱

                    ManagerFee.MessageType = messType;
                    FS.HISFC.Models.Order.Inpatient.Order order = alOrders[i] as FS.HISFC.Models.Order.Inpatient.Order;

                    if (ConfirmShortOrder(order, patient, bCharge, nurseCode, alFeeOrder, alSendDrug, dt, isCharge, iSCDCharge) == -1)
                    {
                        return -1;
                    }

                    //退费的自动做退费申请
                    if (order.Status == 3)
                    {
                        if (!hsCombo.Contains(order.Combo.ID))
                        {
                            ArrayList alSubtbl = orderManager.QuerySubtbl(order.Combo.ID);//查询附材

                            for (int f = 0; f < alSubtbl.Count; f++)//附材处理
                            {
                                if (ConfirmShortOrder((FS.HISFC.Models.Order.Inpatient.Order)alSubtbl[f], patient, bCharge, nurseCode, alFeeOrder, alSendDrug, dt, isCharge, iSCDCharge) == -1)
                                {
                                    return -1;
                                }
                            }

                            hsCombo.Add(order.Combo.ID, order.Combo);

                            if (fee.SaveApply(patient, order.Combo.ID, FS.FrameWork.Function.NConvert.ToInt32(order.Item.User03), quitFee, ref hsQuitFeeWarning) == -1)
                            {
                                this.Err = fee.Err;
                                return -1;
                            }
                        }
                    }

                    #endregion
                }
            }

            if (isLong == false && alFeeOrder.Count > 0) //临时医嘱
            {
                fee.MessageType = messType;
                if (fee.FeeItem(patient, ref alFeeOrder) == -1)
                {
                    this.Err = fee.Err;
                    return -1;
                }
            }

            //添加RecipeNo给药房
            System.Collections.Hashtable hsRecipe = new Hashtable();
            foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItem in alFeeOrder)
            {
                if (feeItem.Order.Item.ItemType == EnumItemType.Drug)
                {
                    if (!hsRecipe.ContainsKey(feeItem.Order.ID))
                    {
                        hsRecipe.Add(feeItem.Order.ID, feeItem);
                    }
                }
            }
            if (alFeeOrder.Count > 0)
            {
                foreach (FS.HISFC.Models.Order.Inpatient.Order drugOrder in alSendDrug)
                {
                    //{A8ABA1D3-C025-43d3-A02C-60FFB5A166AF}  需判断HashTable内是否存在
                    //当设置为药房收费时，alFeeOrder内不包含药品数据
                    if (hsRecipe.ContainsKey(drugOrder.ID))
                    {
                        FS.HISFC.Models.Fee.Inpatient.FeeItemList tempFee = hsRecipe[drugOrder.ID] as FS.HISFC.Models.Fee.Inpatient.FeeItemList;
                        drugOrder.ReciptNO = tempFee.RecipeNO;
                        drugOrder.SequenceNO = tempFee.SequenceNO;
                    }
                }
            }

            if (alSendDrug.Count > 0)
            {
                if (this.SendDrugWithOrderList(alSendDrug, bCharge, dt) == -1)
                {
                    return -1;
                }
            }

            //此处处理自动退费时的提示信息
            //部分项目由于收费科室不一致，所以只能做申请，则给出提示
            if (hsQuitFeeWarning.Count > 0)
            {
                this.Err = "以下项目为退费申请状态，请联系相关科室确认退费！\r\n";
                foreach (string info in hsQuitFeeWarning.Keys)
                {
                    this.Err = this.Err + "\r\n【" + info + "】";
                }
            }

            return 0;
        }


        /// <summary>
        /// 药品申请发送
        /// 
        /// {F766D3A5-CC25-4dd7-809E-3CBF9B152362}  完成一次医嘱分解的库存统一预扣
        /// </summary>
        /// <param name="execOrderList"></param>
        /// <param name="bCharge"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        protected int SendDrug(ArrayList execOrderList, bool bCharge, DateTime dt)
        {
            ArrayList al = new ArrayList();
            foreach (FS.HISFC.Models.Order.ExecOrder info in execOrderList)
            {
                al.Add(info.Order);
            }
            if (mySendExecDrug(al, bCharge, dt, execOrderList) == -1)
                return -1;

            return 1;

        }

        /// <summary>
        /// 药品申请发送
        /// 
        /// {BA8B6888-3114-4575-8CD9-AA09DBA1A954}  完成一次医嘱审核发送的库存统一预扣
        /// </summary>
        /// <param name="orderList"></param>
        /// <param name="bCharge"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        protected int SendDrugWithOrderList(ArrayList orderList, bool bCharge, DateTime dt)
        {
            List<FS.HISFC.Models.Order.ExecOrder> execOrderCollection = new List<FS.HISFC.Models.Order.ExecOrder>();
            ArrayList ordList = new ArrayList();//{4828DBC4-F5CA-4f1c-A3DE-CEBBAD646910}
            ArrayList exeOrdList = new ArrayList();//{4828DBC4-F5CA-4f1c-A3DE-CEBBAD646910}
            foreach (FS.HISFC.Models.Order.Inpatient.Order info in orderList)
            {
                if (info.Item.ItemType == EnumItemType.Drug) //药品-需要发药
                {
                    ArrayList al = orderManager.QueryExecOrderByOneOrder(info.ID, "1");
                    if (al == null || al.Count == 0)
                    {
                        this.Err = "查询药品执行档出错：" + orderManager.Err;
                        return -1;
                    }

                    foreach (FS.HISFC.Models.Order.ExecOrder exeOrder in al)
                    {
                        if (exeOrder.ID == info.User03)
                        {
                            exeOrder.Order.ReciptNO = info.ReciptNO;
                            exeOrder.Order.SequenceNO = info.SequenceNO;
                        }
                        //add by houwb 存入药房的单位不对...
                        ((FS.HISFC.Models.Pharmacy.Item)exeOrder.Order.Item).PackUnit = ((FS.HISFC.Models.Pharmacy.Item)info.Item).PackUnit;

                        execOrderCollection.Add(exeOrder);
                        exeOrdList.Add(exeOrder);
                    }
                    ordList.Add(info);//{4828DBC4-F5CA-4f1c-A3DE-CEBBAD646910}
                }
            }
            if (mySendExecDrug(ordList, bCharge, dt, exeOrdList) == -1)//{4828DBC4-F5CA-4f1c-A3DE-CEBBAD646910}
            {
                return -1;
            }

            return ManagerPharmacy.InpatientDrugPreOutNum(execOrderCollection, dt, false);
        }


        /// <summary>
        /// 最新发送药品申请函数
        /// </summary>
        /// <param name="orderList"></param>
        /// <param name="bCharge"></param>
        /// <param name="dt"></param>
        /// <param name="al"></param>
        /// <returns></returns>
        private int mySendExecDrug(ArrayList orderList, bool bCharge, DateTime dt, ArrayList al)
        {
            ArrayList myList = new ArrayList();

            if (hsDepts.Count <= 0)
            {
                Manager mangerIntegrate = new Manager();

                ArrayList alDepts = mangerIntegrate.GetDepartment();

                foreach (FS.HISFC.Models.Base.Department dept in alDepts)
                {
                    if (!hsDepts.Contains(dept.ID))
                    {
                        hsDepts.Add(dept.ID, dept);
                    }
                }
            }

            for (int i = 0; i < al.Count; i++)
            {
                FS.HISFC.Models.Order.ExecOrder order = al[i] as FS.HISFC.Models.Order.ExecOrder;

                #region 手术麻醉医嘱处理

                //手术、麻醉医嘱处理，houwb 提到上面处理所有
                //处理开单科室的特殊属性，供药房ApplyOut调用，显示麻醉科等特殊科室
                //的取药科室
                if (hsDepts.Contains(order.Order.ReciptDept.ID))
                {
                    if ("1,2".Contains(((FS.HISFC.Models.Base.Department)hsDepts[order.Order.ReciptDept.ID]).SpecialFlag))
                    {
                        order.Order.ReciptDept = hsDepts[order.Order.ReciptDept.ID] as FS.HISFC.Models.Base.Department;
                    }
                }
                else
                {
                    FS.HISFC.Models.Base.Department newDept = new Department();
                    newDept.ID = order.Order.ReciptDept.ID;
                    newDept.Name = order.Order.ReciptDept.Name;
                    order.Order.ReciptDept = newDept;
                }

                #endregion

                int iSendFlag = -1;//发送标记
                /*取科室发药标记*/
                iSendFlag = 2;//临时发送

                order.DrugFlag = iSendFlag;//0,未发送，1 集中发送 2 临时发送
                if (order.Order.OrderType.IsNeedPharmacy && bCharge) //需要发药和已经收费
                {
                    if (order.Order.OrderType.ID == "QL" || order.Order.OrderType.ID == "CD")//出院带药，请假带药临时发送
                    {
                        order.DrugFlag = 2;
                    }
                    else
                    {
                        order.DrugFlag = iSendFlag;
                        order.IsCharge = bCharge;
                    }
                    myList.Add(order);

                }
                else if (order.Order.OrderType.IsNeedPharmacy == false)//不需要发药的药品
                {
                    order.DrugFlag = 3;//已经配
                }
                else //需要发药，未收费
                {
                    order.DrugFlag = 2;
                    myList.Add(order);
                }
            }
            if (myList.Count != 0)
            {
                if (SendToDrugStore(myList, dt) == -1)
                {
                    return -1;
                }
            }

            foreach (FS.HISFC.Models.Order.ExecOrder exeOrder in al)
            {
                //置执行发药标记
                if (orderManager.SetDrugFlag(exeOrder.ID, exeOrder.DrugFlag) == -1)
                {
                    this.Err = orderManager.Err;

                    return -1;
                }
                //if (hsOrderExecSeq.Contains(exeOrder.ID))
                //{
                //    hsOrderExecSeq[exeOrder.ID] = exeOrder.DrugFlag;
                //}

                //if (hsApplyExecSeq[exeOrder.ID] != null)
                //{
                //    string[] seq = hsApplyExecSeq[exeOrder.ID].ToString().Split('|');
                //    for (int i = 0; i < seq.Length; i++)
                //    {
                //        if (!string.IsNullOrEmpty(seq[i].ToString())
                //            && seq[i].ToString() != exeOrder.ID)
                //        {
                //            if (orderManager.SetDrugFlag(seq.ToString(), FS.FrameWork.Function.NConvert.ToInt32(hsOrderExecSeq[exeOrder.ID])) == -1)
                //            {
                //                this.Err = orderManager.Err;

                //                return -1;
                //            }
                //        }
                //    }
                //}
            }
            return 0;
        }


        /// <summary>
        /// 审核确认
        /// </summary>
        /// <param name="order"></param>
        /// <param name="patient"></param>
        /// <param name="isNurseCharge">是否护士站收费？否则是药房发药时收费</param>
        /// <param name="nurseCode"></param>
        /// <param name="alFeeOrder"></param>
        /// <param name="alSendDrug"></param>
        /// <param name="dt"></param>
        /// <param name="isCharge">是否收费？ 欠费患者可以只保存不收费</param>
        /// <returns></returns>
        protected int ConfirmShortOrder(FS.HISFC.Models.Order.Inpatient.Order order, FS.HISFC.Models.RADT.PatientInfo patient, bool isNurseCharge, string nurseCode, ArrayList alFeeOrder, ArrayList alSendDrug, DateTime dt, bool isCharge, bool iSCDCharge)
        {
            //优化的哈希表需要清空...houwb
            htDrug.Clear();
            htItem.Clear();
            //htStorage.Clear();

            //先取得执行当流水号,在收费的同时插入执行当流水号
            string execId = orderManager.GetNewOrderExecID();

            if (execId == "" || execId == "-1")
            {
                return -1;
            }

            //bool myCharge = false;
            bool mySendDrug = false;

            try
            {
                if (order.Status == 0)
                {
                    order.Patient = patient;//患者重新付值

                    bool isNeedConfirm = false;

                    if (order.OrderType.IsCharge) //收费医嘱
                    {
                        if (order.Item.GetType() == typeof(FS.HISFC.Models.Fee.Item.Undrug))//非药品查询终端确认标记
                        {
                            #region 非药品
                            string err = "";
                            if (FillFeeItem(ref order, out err, 1) == -1)
                            {
                                this.Err = err;
                                return -1;
                            }
                            FeeUndrug(order, patient, nurseCode, alFeeOrder, execId, isCharge, ref isNeedConfirm);
                            #endregion

                        }
                        else //药品--根据是否收费进行收费
                        {
                            #region 药品
                            //执行科室为护士所在科室
                            //执行科室不再重新获取，对于取药科室，已经修改申请科室的规则
                            //order.ExeDept = order.Patient.PVisit.PatientLocation.Dept.Clone();//((FS.HISFC.Models.RADT.Person)feeManagement.Operator).Dept.Clone();
                            if (isNurseCharge) //是否护士站收费
                            {
                                string err = "";
                                if (FillPharmacyItem(ref order, out err) == -1)
                                {
                                    this.Err = err;
                                    return -1;
                                }
                                string strProperty = orderManager.GetDrugProperty(order.Item.ID,
                                    ((FS.HISFC.Models.Pharmacy.Item)order.Item).DosageForm.ID,
                                    order.Patient.PVisit.PatientLocation.Dept.ID);

                                if (strProperty == "0")	//不可拆分，获得取整
                                {
                                    order.Qty = (decimal)System.Math.Ceiling((double)order.Qty);
                                }

                                if (order.ExeDept == null || order.ExeDept.ID == "")
                                {
                                    order.ExeDept = order.Patient.PVisit.PatientLocation.Dept.Clone();//order.NurseStation;
                                }
                                order.User03 = execId;

                                //if (isCharge)
                                if (isCharge || (!iSCDCharge && order.OrderType.ID.Equals("CD"))) //Editor by liuww 出院带药不判断
                                {
                                    if (IsFee(patient, order))
                                    {
                                        mySendDrug = true;
                                        //myCharge = true;
                                        //添加到收费项目里面
                                        order.Oper.OperTime = dt;
                                        alFeeOrder.Add(order);
                                    }
                                    else //不收费，待收费
                                    {
                                        mySendDrug = true;
                                        //myCharge = false;
                                    }
                                }
                                else
                                {
                                    mySendDrug = false;
                                    //myCharge = false;
                                }
                            }
                            else
                            {
                                //if (isCharge) 
                                if (isCharge || (!iSCDCharge && order.OrderType.ID.Equals("CD"))) //Editor by liuww 出院带药不判断
                                {
                                    mySendDrug = true;
                                    //myCharge = false;
                                }
                                else
                                {
                                    mySendDrug = false;
                                    //myCharge = false;
                                }
                            }

                            #endregion
                        }
                    }
                    else //非收费项目
                    {
                        //不发药，不收费
                    }

                    #region 审核医嘱

                    if (this.UpdateOther(order) == -1)
                    {
                        return -1;
                    }

                    //{FE127946-53ED-4bec-8223-45AAE5398C6C} 为了处理不同的流程
                    if (order.Item.ItemType == EnumItemType.Drug)             //护士站不收费 且 项目为药品
                    {
                        //护士站不收费或者欠费不收费时，药品为需要确认状态
                        if (!isNurseCharge
                            || (!isCharge && iSCDCharge) 
                            || (!isCharge && !iSCDCharge 
                                && !order.OrderType.ID.Equals("CD")))
                        {
                            isNeedConfirm = true;
                        }
                    }
                    else  //非药品若欠费则不计账（传isNeedConfirm难理解，扩展后的代码难理解）
                    {
                        //isNeedConfirm = !isCharge;
                    }

                    //if (orderManager.ConfirmAndExecOrder(order, isNurseCharge, execId, dt) == -1) //更新执行档标记
                    if (orderManager.ConfirmAndExecOrder(order, !isNeedConfirm, execId, dt) == -1) //更新执行档标记
                    {
                        this.Err = orderManager.Err;
                        return -1;
                    }
                    #endregion

                    #region 发送发药申请
                    if (mySendDrug)
                    {
                        alSendDrug.Add(order);
                    }
                    #endregion

                    #region 附材

                    #region 药品附材是否在药房摆药时计费，随同药品后计费参数一起使用

                    bool bChargeSubtbl = true;
                    if (!isNurseCharge && order.Item.ItemType == EnumItemType.Drug) //护士站不计费
                    {
                        FS.HISFC.BizProcess.Integrate.Common.ControlParam con = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
                        //药品后计费时，药品附材是否与药品同时计费 1 护士站计费 0 药房计费
                        bChargeSubtbl = con.GetControlParam<bool>("200050", false, true);
                    }
                    #endregion

                    ArrayList alSubtbl = orderManager.QuerySubtbl(order.Combo.ID);//附材处理

                    //{C05D5AB9-1ED9-4510-A70C-4E4D131CEA73} 修改临时医嘱附材是组合项目的时候的收费开始
                    FS.HISFC.Models.Order.Inpatient.Order obj = null;
                    for (int f = 0; f < alSubtbl.Count; f++)
                    {
                        obj = (FS.HISFC.Models.Order.Inpatient.Order)alSubtbl[f];
                        string err = string.Empty;

                        if (FillFeeItem(ref obj, out err, 1) == -1)
                        {
                            this.Err = err;
                            return -1;
                        }
                        if (obj.Status == 0) //没审核需要收费
                        {
                            /*******项目添充********/
                            string execIdSub = orderManager.GetNewOrderExecID();
                            if (execIdSub == "" || execIdSub == "-1")
                            {
                                this.Err = "获得附材执行流水号出错!" + orderManager.Err;
                                return -1;
                            }

                            #region 附材收费

                            #region 非药品 (非药品要考虑终端确认的情况，终端确认项目的附材也不需要终端确认收费）

                            if (order.Item.ItemType != FS.HISFC.Models.Base.EnumItemType.Drug)
                            {
                                FeeUndrug(obj, patient, nurseCode, alFeeOrder, execIdSub, isCharge, ref isNeedConfirm);
                            }
                            #endregion

                            #region 药品
                            else
                            {
                                if (bChargeSubtbl)
                                {
                                    if (((FS.HISFC.Models.Fee.Item.Undrug)obj.Item).UnitFlag == "1")
                                        //order.Order.Unit == "[复合项]")
                                    {
                                        ArrayList al = managerPack.QueryUndrugPackagesBypackageCode(obj.Item.ID);
                                        if (al == null)
                                        {
                                            this.Err = "获得细项出错！" + managerPack.Err;

                                            return -1;
                                        }

                                        FS.HISFC.Models.Order.Inpatient.Order myorder = null;

                                        decimal rate = 1;
                                        foreach (FS.HISFC.Models.Fee.Item.Undrug undrug in al)
                                        {
                                            myorder = new FS.HISFC.Models.Order.Inpatient.Order();
                                            decimal qty = obj.Qty;
                                            myorder = obj.Clone();
                                            myorder.Patient = patient.Clone();
                                            myorder.Name = undrug.Name;
                                            myorder.Item.Name = undrug.Name;

                                            myorder.Item = undrug.Clone();
                                            myorder.Qty = qty * undrug.Qty;//数量==复合项目数量*小项目数量
                                            myorder.Item.Qty = qty * undrug.Qty;//数量==复合项目数量*小项目数量
                                            myorder.Oper = obj.Oper.Clone();
                                            myorder.Oper.OperTime = dt;
                                            myorder.User03 = execIdSub;

                                            rate = fee.GetItemRateForZT(obj.Item.ID, undrug.ID);
                                            if (FillFeeItem(ref myorder, out err, rate) == -1)
                                            {
                                                this.Err = err;
                                                return -1;
                                            }
                                            #region {92A0CF31-27BC-472e-9C15-1ED2C8A81F36} by zhang.xs 2010.10.19

                                            //if (myorder.Item.Price > 0)
                                            if (this.IsFee(patient, myorder) && bChargeSubtbl)
                                            {
                                                alFeeOrder.Add(myorder);
                                            }

                                            #endregion

                                        }
                                    }
                                    else
                                    {
                                        if (FillFeeItem(ref obj, out err, 1) == -1)
                                        {
                                            this.Err = err;
                                            return -1;
                                        }
                                        obj.Patient = patient.Clone();


                                        obj.User03 = execIdSub;
                                        if (obj.Item.Price >= 0)
                                        {
                                            if (IsFee(patient, obj) && bChargeSubtbl)
                                            {
                                                obj.Oper.OperTime = dt;
                                                alFeeOrder.Add(obj); //收费
                                            }
                                            else
                                            {
                                                //待收费
                                            }
                                        }
                                    }
                                }
                                isNeedConfirm = false;
                                if (!bChargeSubtbl
                                    || !isCharge)
                                {
                                    isNeedConfirm = true;
                                }
                            }
                            #endregion

                            #endregion

                            if (orderManager.ConfirmAndExecOrder(obj, !isNeedConfirm, execIdSub, dt) == -1)//更新标记
                            {
                                this.Err = orderManager.Err;
                                return -1;
                            }
                        }
                    }
                    #endregion
                }
                else if (order.Status == 3) //作废医嘱
                {
                    if (orderManager.ConfirmOrder(order, false, dt) == -1)
                    {
                        this.Err = orderManager.Err;
                        return -1;
                    }

                    if (order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.UnDrug)
                    {
                        if (orderManager.DcExecImmediate(order, FS.FrameWork.Management.Connection.Operator) < 0)
                        {
                            this.Err = orderManager.Err;
                            return -1;
                        }
                    }

                    //foreach(
                }
                else
                {
                    this.Err = "医嘱已经发生变化，请刷新屏幕！";
                    return -1;
                }
            }
            catch (Exception ex)
            {
                this.Err = "审核医嘱出错：" + ex.Message;
                return -1;
            }

            return 0;
        }

        /// <summary>
        /// 住院审核医嘱，手术和终端确认项目是否收费
        /// 0、手术项目审核不收费，终端确认项目审核不收费
        /// 1、手术项目审核收费，终端确认项目审核收费（本科室执行项目）
        /// 2、手术项目审核不收费，终端确认项目审核收费（本科室执行项目）
        /// 3、手术项目审核收费，终端确认项目审核不收费
        /// </summary>
        private int isCheckConfirmModel = -1;

        /// <summary>
        /// 住院审核医嘱，手术和终端确认项目是否收费
        /// 0、手术项目审核不收费，终端确认项目审核不收费
        /// 1、手术项目审核收费，终端确认项目审核收费（本科室执行项目）
        /// 2、手术项目审核不收费，终端确认项目审核收费（本科室执行项目）
        /// 3、手术项目审核收费，终端确认项目审核不收费
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        private bool CheckCharge(FS.HISFC.Models.RADT.PatientInfo patient, FS.HISFC.Models.Order.Inpatient.Order order)
        {
            if (order.Item.ID == "999" || !order.OrderType.IsCharge)
            {
                return true;
            }

            if (this.isCheckConfirmModel == -1)
            {
                FS.HISFC.BizProcess.Integrate.Common.ControlParam con = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
                isCheckConfirmModel = con.GetControlParam<int>("HNZY10", true, 1);
            }
            if (order.Item.ID != "999")
            {
                //现在存的医嘱状态有问题，所以这个过段时间再打开，2013年1月9日18:07:00
                //if (!order.IsSubtbl)
                //{
                ((FS.HISFC.Models.Fee.Item.Undrug)order.Item).IsNeedConfirm = this.fee.GetItem(order.Item.ID).IsNeedConfirm;
                //}
            }

            FS.HISFC.Models.Base.Department execDept = deptMangager.GetDeptmentById(order.ExeDept.ID);

            switch (isCheckConfirmModel)
            {
                //0、手术项目审核不收费，终端确认项目审核不收费
                case 0:
                    if (order.Item.SysClass.ID.ToString() == "UO")
                    {
                        return false;
                    }
                    if (((FS.HISFC.Models.Fee.Item.Undrug)order.Item).IsNeedConfirm)
                    {
                        return false;
                    }
                    break;
                //1、手术项目审核收费，终端确认项目审核收费（本科室执行项目）
                case 1:
                    if (order.Item.SysClass.ID.ToString() == "UO")
                    {
                        return true;
                    }
                    if (((FS.HISFC.Models.Fee.Item.Undrug)order.Item).IsNeedConfirm)
                    {
                        if (order.ExeDept.ID == ((FS.HISFC.Models.Base.Employee)this.managerRADT.Operator).Dept.ID
                        || (execDept != null && execDept.DeptType.ID.ToString() == "OP")
                        || order.ExeDept.ID == patient.PVisit.PatientLocation.Dept.ID
                        || order.ExeDept.ID == patient.PVisit.PatientLocation.NurseCell.ID) 
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    break;
                //2、手术项目审核不收费，终端确认项目审核收费（本科室执行项目）
                case 2:
                    if (order.Item.SysClass.ID.ToString() == "UO")
                    {
                        return false;
                    }
                    if (((FS.HISFC.Models.Fee.Item.Undrug)order.Item).IsNeedConfirm)
                    {
                        if (order.ExeDept.ID == ((FS.HISFC.Models.Base.Employee)this.managerRADT.Operator).Dept.ID
                        || (execDept != null && execDept.DeptType.ID.ToString() == "OP")
                        || order.ExeDept.ID == patient.PVisit.PatientLocation.Dept.ID
                        || order.ExeDept.ID == patient.PVisit.PatientLocation.NurseCell.ID)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    break;
                //3、手术项目审核收费，终端确认项目审核不收费
                case 3:

                    if (order.Item.SysClass.ID.ToString() == "UO")
                    {
                        return true;
                    }
                    if (((FS.HISFC.Models.Fee.Item.Undrug)order.Item).IsNeedConfirm)
                    {
                        return false;
                    }
                    break;
                default:
                    if (order.Item.SysClass.ID.ToString() == "UO")
                    {
                        return true;
                    }
                    if (((FS.HISFC.Models.Fee.Item.Undrug)order.Item).IsNeedConfirm)
                    {
                        if (order.ExeDept.ID == ((FS.HISFC.Models.Base.Employee)this.managerRADT.Operator).Dept.ID
                        || (execDept != null && execDept.DeptType.ID.ToString() == "OP")
                        || order.ExeDept.ID == patient.PVisit.PatientLocation.Dept.ID
                        || order.ExeDept.ID == patient.PVisit.PatientLocation.NurseCell.ID)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    break;
            }
            return true;
        }

        /// <summary>
        /// 审核非药品计费
        /// </summary>
        /// <param name="order"></param>
        /// <param name="patient"></param>
        /// <param name="nurseCode"></param>
        /// <param name="alFeeOrder"></param>
        /// <param name="execId"></param>
        /// <param name="isCharge">是否收费？欠费患者可以只保存不收费</param>
        /// <param name="isNeedConfirm"></param>
        private void FeeUndrug(FS.HISFC.Models.Order.Inpatient.Order order, FS.HISFC.Models.RADT.PatientInfo patient, string nurseCode, ArrayList alFeeOrder, string execId, bool isCharge, ref bool isNeedConfirm)
        {
            FS.HISFC.Models.Base.Department execDept = deptMangager.GetDeptmentById(order.ExeDept.ID);

            if (execDept == null)
            {
                execDept = new Department();
            }

            //对于手术医嘱不进行收费处理 ,手术执行科室为本科室的，直接收费
            //if ((order.Item.SysClass.ID.ToString() != "UO" &&
            //        ((FS.HISFC.Models.Fee.Item.Undrug)order.Item).IsNeedConfirm == false) ||
            //    (order.Item.SysClass.ID.ToString() == "UO"
            //        && (order.ExeDept.ID == ((FS.HISFC.Models.Base.Employee)this.managerRADT.Operator).Dept.ID
            //        || execDept.DeptType.ID.ToString() == "OP"
            //        || order.ExeDept.ID == patient.PVisit.PatientLocation.Dept.ID
            //        || order.ExeDept.ID == patient.PVisit.PatientLocation.NurseCell.ID)
            //    ))//非手术医嘱

            isNeedConfirm = true;

            if (order.Item.ID == "999" || !order.OrderType.IsCharge)
            {
                isNeedConfirm = false;
            }

            if (isCharge)
            {
                //if (order.Item.ID != "999")
                //{
                //    ((FS.HISFC.Models.Fee.Item.Undrug)order.Item).IsNeedConfirm = this.fee.GetItem(order.Item.ID).IsNeedConfirm;
                //}
                ////非手术项目
                //if ((order.Item.SysClass.ID.ToString() != "UO"
                //    && ((FS.HISFC.Models.Fee.Item.Undrug)order.Item).IsNeedConfirm == false)

                //    //手术项目
                //    || ((order.Item.SysClass.ID.ToString() == "UO" && isUOAutoConfirmed == 1) &&
                //        (order.ExeDept.ID == ((FS.HISFC.Models.Base.Employee)this.managerRADT.Operator).Dept.ID
                //        || execDept.DeptType.ID.ToString() == "OP"
                //        || order.ExeDept.ID == patient.PVisit.PatientLocation.Dept.ID
                //        || order.ExeDept.ID == patient.PVisit.PatientLocation.NurseCell.ID)))

                ////先取消，目前手术项目，要么手术室批费，药品手术室终端确认

                if (CheckCharge(patient, order))
                {
                    isNeedConfirm = false;
                    if (order.OrderType.IsCharge == false && order.IsSubtbl == false)
                    {
                        //医嘱是嘱托，不是附材的不收费。
                    }
                    else if (!IsFee(patient, order))   //order.Item.Price <= 0  /*&& !复合项目*/)
                    {
                        //不是复合项目，价格小于零的不收费
                    }
                    else//收费
                    {
                        #region 如果是复合项目－变成细项
                        if (((FS.HISFC.Models.Fee.Item.Undrug)order.Item).UnitFlag == "1")
                        {
                            /*待添加*/
                            ArrayList al = managerPack.QueryUndrugPackagesBypackageCode(order.Item.ID);
                            if (al == null)
                            {
                                this.Err = "获得细项出错！" + managerPack.Err;

                                return;
                            }

                            decimal rate = 1;
                            foreach (FS.HISFC.Models.Fee.Item.Undrug undrug in al)
                            {
                                FS.HISFC.Models.Order.Inpatient.Order myorder = null;
                                decimal qty = order.Qty;
                                myorder = order.Clone();
                                myorder.Name = undrug.Name;

                                myorder.Item = undrug.Clone();
                                myorder.Qty = qty * undrug.Qty;//数量==复合项目数量*小项目数量
                                myorder.Item.Qty = qty * undrug.Qty;//数量==复合项目数量*小项目数量

                                #region {10C9E65E-7122-4a89-A0BE-0DF62B65C647} 写入复合项目编码、名称

                                myorder.Package.ID = order.Item.ID;
                                myorder.Package.Name = order.Item.Name;
                                myorder.Package.Qty = qty;

                                #endregion

                                rate = fee.GetItemRateForZT(order.Item.ID, undrug.ID);
                                string err = "";
                                if (FillFeeItem(ref myorder, out err, rate) == -1)
                                {
                                    this.Err = err;
                                    return;
                                }
                                //复合项目在费用表没有记录执行流水号 add by houwb 2011-4-7
                                myorder.User03 = execId;
                                if (IsFee(patient, myorder))
                                {
                                    //添加到收费项目里面
                                    myorder.Oper.OperTime = orderManager.GetDateTimeFromSysDateTime();
                                    if (this.IsFee(patient, myorder)) // myorder.Item.Price > 0)
                                    {
                                        alFeeOrder.Add(myorder);
                                    }
                                }
                                else
                                {
                                    /*不收费*/
                                }
                            }
                        }
                        else
                        {
                            #region 收费

                            if (order.ExeDept.ID == "")//执行科室默认
                            {
                                order.ExeDept = order.Patient.PVisit.PatientLocation.Dept.Clone();//order.NurseStation;
                            }
                            order.User03 = execId;//execOrderID
                            if (IsFee(patient, order))
                            {
                                //添加到收费项目里面
                                order.Oper.OperTime = orderManager.GetDateTimeFromSysDateTime();
                                alFeeOrder.Add(order);
                            }
                            else
                            {
                                /*不收费*/
                            }

                            #endregion
                        }
                        #endregion
                    }
                }
            }
        }

        /// <summary>
        /// 根据医嘱更新患者各种状态
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        protected int UpdateOther(FS.HISFC.Models.Order.Inpatient.Order order)
        {
            if (order.Status == 0)//{A921CA7F-6607-406c-9DF2-C2A58C792ED4}
            {
                if (IsUpdateOther == false) return 0;
                if (order.Item.SysClass.ID.ToString() == "MRD")//转科
                {
                    if (order.ExeDept == null || order.ExeDept.ID == order.Patient.PVisit.PatientLocation.Dept.ID) return 0;//自己科室的不动
                    FS.HISFC.Models.RADT.Location newDept = new FS.HISFC.Models.RADT.Location();
                    newDept.Dept = order.ExeDept.Clone();
                    newDept.Memo = order.Memo;
                    if (managerRADT.TransferPatientApply(order.Patient.Clone(), newDept, false, "1") == -1)
                    {
                        this.Err = managerRADT.Err;
                        return -1;
                    }
                }
                else if (order.Item.SysClass.ID.ToString() == "UN")//护理
                {
                    //{36E3CA9D-FD23-42b5-802E-C365C04D93A0}
                    if (order.Item.Name.IndexOf("级护理") >= 0 
                        || order.Item.Name.IndexOf("特护") >= 0
                        || order.Item.Name.IndexOf("病危") >= 0 
                        || order.Item.Name.IndexOf("重症") >= 0)//判断护理级别，没办法
                    {
                        if (managerRADT.UpdatePatientTend(order.Patient.ID, order.Item.Name) == -1)
                        {
                            this.Err = managerRADT.Err;
                            return -1;
                        }

                    }
                }
                else if (order.Item.SysClass.ID.ToString() == "MF")//饮食	his
                {
                    if (managerRADT.UpdatePatientFood(order.Patient.ID, order.Item.Name) == -1)
                    {
                        this.Err = managerRADT.Err;
                        return -1;
                    }
                }
                //有时候告病危告病重不一定是病情医嘱
                //else if (order.Item.SysClass.ID.ToString() == "UF")//{C9F9006D-AE0A-4e73-9ECE-68265A7A583E} 
                //{
                //    int flag = 0;
                //    switch (order.Item.Name)
                //    {
                //        case "病重":
                //            flag = 1;
                //            break;
                //        case "病危":
                //            flag = 2;
                //            break;
                //    }
                //    managerRADT.UpdateCondition_Info(flag.ToString(), order.Patient.ID);
                //}

                if (order.Item.Name.Contains("病重"))
                {
                    managerRADT.UpdateCondition_Info("1", order.Patient.ID);
                }
                else if (order.Item.Name.Contains("病危"))
                {
                    managerRADT.UpdateCondition_Info("2", order.Patient.ID);
                }
            }
            else if (order.Status == 3)//{A921CA7F-6607-406c-9DF2-C2A58C792ED4}
            {
                if (order.Item.SysClass.ID.ToString() == "UN")//护理
                {
                    //{36E3CA9D-FD23-42b5-802E-C365C04D93A0}
                    if (order.Item.Name.IndexOf("级护理") >= 0 || order.Item.Name.IndexOf("特护") >= 0)//判断护理级别，没办法
                    {
                        if (managerRADT.UpdatePatientTend(order.Patient.ID, "") == -1)
                        {
                            this.Err = managerRADT.Err;
                            return -1;
                        }

                    }
                }
            }
            return 0;
        }
        //{2AFC76CB-3353-4865-AEB4-AFBEE09DD1D7}
        /// <summary>
        /// 检索出医嘱项目中所有需要打印医嘱单的项目并判断
        /// </summary>
        /// <param name="tag">项目ID</param>
        /// <returns></returns>
        private bool JudgeInsertOrderPrint(ArrayList itemList, string tag)
        {
            if (itemList != null && itemList.Count > 0)
            {
                foreach (FS.HISFC.Models.Fee.Item.Undrug undrug in itemList)
                {
                    if (undrug.ID == tag)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        #endregion

        #region 获得护士站收费信息
        /// <summary>
        /// 是否护士站收药品
        /// </summary>
        /// <param name="t"></param>
        /// <returns>返回True 说明使用执行、扣费分开流程 护士站计费 返回False 说明执行时扣费</returns>
        public static bool GetIsNurseCharge(ref System.Data.IDbTransaction t)
        {
            //FS.FrameWork.Management.ControlParam controler = new FS.FrameWork.Management.ControlParam();
            //if(t!=null) controler.SetTrans(t);
            //if (controler.QueryControlerInfo("100003") == "1") //摆药收费分开了
            //{
            //    return true;
            //}
            //else //药房收费
            //{
            //    return false;
            //}
            if (t != null)
            {
                CtrlIntegrate.SetTrans(t);
            }
            //返回True 说明使用执行、扣费分开流程 护士站计费 返回False 说明执行时扣费
            return CtrlIntegrate.GetControlParam<bool>(SysConst.Use_Drug_ApartFee, false, true);
        }

        #endregion

        #region 医嘱发送

        public FS.HISFC.BizProcess.Integrate.Fee fee = new Fee();
        Hashtable hsOrderTemp = new Hashtable();

        /// <summary>
        /// 存储发药申请中的执行档流水号对应所有的执行档流水号，用于药房存储更新执行档发药标记
        /// </summary>
        //Hashtable hsApplyExecSeq = new Hashtable();

        /// <summary>
        /// 存储所有的执行档流水号
        /// </summary>
        //Hashtable hsOrderExecSeq = new Hashtable();

        /// <summary>
        /// 分解保存医嘱
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="alExecOrder"></param>
        /// <param name="nurseCode"></param>
        /// <param name="dt"></param>
        /// <param name="isPharmacy"></param>
        /// <param name="isCharge">是否收费？ 欠费患者可以只保存不收费,为false</param>
        /// <param name="isCharge">是否是确认收费？true 为执行档确认收费，false为医嘱分解保存</param>
        /// <returns></returns>
        public int ComfirmExec(FS.HISFC.Models.RADT.PatientInfo patient,
            List<FS.HISFC.Models.Order.ExecOrder> alExecOrder, string nurseCode, DateTime dt, bool isPharmacy,
            bool isCharge, bool isOrderConfirmFee)
        {
            //优化的哈希表需要清空...houwb
            htDrug.Clear();
            htItem.Clear();
            //htStorage.Clear();

            //hsApplyExecSeq.Clear();
            //hsOrderExecSeq.Clear();

            //True 护士站收费 False 药房收费
            bool isNurseCharge = GetIsNurseCharge(ref this.trans); //是否护士站收费
            ArrayList alChargeOrders = new ArrayList();
            ArrayList alDrugSendOrders = new ArrayList(); //发药医嘱

            FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemLists = new FS.HISFC.Models.Fee.Inpatient.FeeItemList();
            FS.HISFC.BizLogic.Fee.InPatient inpatient = new FS.HISFC.BizLogic.Fee.InPatient();
            int iReturn = 0;
            FS.HISFC.Models.Base.Employee oper = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
            string NoteNo = "";

            //来源于无锡的代码，处理累计用药的
            FS.HISFC.BizProcess.Integrate.Pharmacy pharmacyIntegrate = new Pharmacy();

            #region 药品

            if (isPharmacy)
            {
                //存储上一个插入发药申请表的执行档流水号
                //string applyOutExecSeq = "";

                foreach (FS.HISFC.Models.Order.ExecOrder order in alExecOrder)
                {
                    string deptCode = order.Order.StockDept.ID;

                    if (order.Order.Item.ID != "999" && order.Order.OrderType.IsCharge)//非自定义项目，和收费项目重新取信息
                    {
                        #region 填充项目信息 检验有效性
                        FS.HISFC.Models.Order.Inpatient.Order o = order.Order;
                        string err = "";

                        if (FillPharmacyItemWithStockDept(ref o, out err) == -1)
                        {
                            this.Err = err;
                            return -1;
                        }
                        #endregion
                    }

                    order.Order.StockDept.ID = deptCode;

                    order.Order.Patient = patient;
                    order.Order.ExecOper.Dept = order.Order.ExeDept.Clone();

                    #region 收费

                    if (order.Order.OrderType.IsCharge)
                    {
                        if (isCharge)
                        {
                            #region 患者库存管理 首先进行患者库存判断
                            string feeFlag = "";
                            decimal feeNum = 0;
                            bool isManageFee = true;		//是否需要调用收费函数
                            bool isFee = false;				//是否已收费 true 已计费 false 出单未计费
                            decimal phaNum = 0; //是否是单个患者库存，非科室、病区库存

                            //对于科室、病区取整，药房发药取整，但是计费不取整，保证扣费均匀
                            //if (pharmacyIntegrate.PatientStore(order, ref feeFlag, ref feeNum, ref isFee, ref phaNum, ref applyOutExecSeq) == -1)
                            if (pharmacyIntegrate.PatientStore(order, ref feeFlag, ref feeNum, ref isFee, ref phaNum) == -1)
                            {
                                this.Err = ("患者库存出库处理发生错误" + pharmacyIntegrate.Err);
                                return -1;
                            }
                            #endregion

                            if (isNurseCharge) //护士站收费
                            {
                                order.IsCharge = true;
                                order.ChargeOper.Dept = order.Order.NurseStation.Clone();
                                order.ChargeOper.ID = oper.ID;
                                order.ChargeOper.Name = oper.Name;
                                order.ChargeOper.OperTime = dt;
                                order.Order.Oper = order.ChargeOper.Clone();

                                switch (feeFlag)
                                {
                                    case "2":			//正常流程处理																
                                        break;
                                    case "1":			//按照函数返回的计费数量进行计费处理
                                        order.Order.Qty = feeNum;
                                        break;
                                    case "0":			//扣患者库存 不进行计费处理
                                        if (isFee)		//已计费
                                        {
                                            order.DrugFlag = 3;			//已配药
                                            order.IsCharge = true;		//已收费
                                            order.ChargeOper.Dept = order.Order.NurseStation.Clone();
                                            order.ChargeOper.ID = oper.ID;
                                            order.ChargeOper.Name = oper.Name;
                                            order.ChargeOper.OperTime = dt;
                                            order.Order.Oper = order.ChargeOper.Clone();
                                        }
                                        isManageFee = false;
                                        break;
                                    default:
                                        break;
                                }
                                if (isManageFee)
                                {
                                    alChargeOrders.Add(order.Order);
                                }
                            }
                            else //药房收费
                            {
                                switch (feeFlag)
                                {
                                    case "2":			//正常流程处理																
                                        break;
                                    case "1":			//按照函数返回的计费数量进行计费处理
                                        order.Order.Qty = feeNum;
                                        order.Order.User03 = phaNum.ToString();
                                        break;
                                    case "0":			//扣患者库存 不进行计费处理
                                        isManageFee = false;
                                        break;
                                    default:
                                        break;
                                }
                                order.IsCharge = false;
                            }
                            order.Order.User03 = order.ID;

                            #region 插入药品发送表

                            if (order.Order.OrderType.IsNeedPharmacy)
                            {
                                int iSendFlag = 2;/*发药标记 暂时都临时发送*/
                                order.DrugFlag = iSendFlag;

                                if (isManageFee)
                                {
                                    if (feeFlag == "2")
                                    {
                                        alDrugSendOrders.Add(order);
                                        //applyOutExecSeq = order.ID;
                                    }
                                    else
                                    {
                                        if (phaNum >= 0)
                                        {
                                            FS.HISFC.Models.Order.ExecOrder orderTemp = order.Clone();
                                            orderTemp.Order.Qty = phaNum;
                                            alDrugSendOrders.Add(orderTemp);
                                            //applyOutExecSeq = orderTemp.ID;
                                        }
                                    }
                                }
                                //else
                                //{
                                //    string applyNum = "";
                                //    string execSeqAll = "";
                                //    if (pharmacyIntegrate.GetExecSeqAllByExecSeq(applyOutExecSeq, ref applyNum, ref execSeqAll) == -1)
                                //    {
                                //        this.Err = ( pharmacyIntegrate.Err);
                                //        return -1;
                                //    }

                                //    this.ManagerPharmacy.UpdateApplyOutForOrderSeq(applyNum, execSeqAll + "|" + order.ID);
                                //}

                                //if (hsApplyExecSeq.Contains(applyOutExecSeq))
                                //{
                                //    hsApplyExecSeq[applyOutExecSeq] = hsApplyExecSeq[applyOutExecSeq].ToString() + "|" + order.ID;
                                //}
                                //else
                                //{
                                //    hsApplyExecSeq.Add(applyOutExecSeq, "|" + order.ID);
                                //}

                                //if (!hsOrderExecSeq.Contains(order.ID))
                                //{
                                //    hsOrderExecSeq.Add(order.ID, null);
                                //}
                            }
                            else
                            {
                                order.DrugFlag = 3;//配药标志 
                            }
                            #endregion
                        }
                    }

                    #endregion

                    #region 更新执行标记 //更新确认标记及执行标记
                    try
                    {
                        //付执行数据
                        order.ExecOper.ID = oper.ID;
                        order.ExecOper.Name = oper.Name;
                        order.IsExec = true;
                        order.ExecOper.OperTime = dt;

                        if (order.Order.Item.ID == "999" || !order.Order.OrderType.IsCharge)
                        {
                            order.IsCharge = true;
                            order.ChargeOper.Dept = order.Order.NurseStation.Clone();
                            order.ChargeOper.ID = oper.ID;
                            order.ChargeOper.Name = oper.Name;
                            order.ChargeOper.OperTime = dt;
                            order.Order.Oper = order.ChargeOper.Clone();
                        }
                    }
                    catch (Exception ex)
                    {
                        this.Err = "设置执行数据出错！";
                        return -1;
                    }

                    iReturn = orderManager.UpdateForConfirmExecDrug(order, isOrderConfirmFee);

                    if (iReturn == -1)
                    {
                        this.Err = orderManager.Err;
                        return -1;
                    }
                    else if (iReturn == 0)
                    {
                        this.Err = order.Order.Patient.PVisit.PatientLocation.Bed.ID.Substring(4) + "床 " + order.Order.Patient.Name +"\r\n\r\n组号:" + order.SubCombNO.ToString() + " " + order.Order.Item.Name + "\r\n医嘱已经发生变化，请刷新屏幕！";
                        return -1;
                    }
                    #endregion

                    #region 非药品和嘱托医嘱，更新执行标记

                    if (order.Order.Item.ID == "999" || !order.Order.OrderType.IsCharge)
                    {
                        iReturn = orderManager.UpdateOrderStatus(order.Order.ID, 2);
                        if (iReturn == -1) //错误
                        {
                            this.Err = orderManager.Err;
                            return -1;
                        }
                    }
                    #endregion
                }
            }
            #endregion

            #region 非药品

            else
            {
                FS.HISFC.BizProcess.Integrate.FeeInterface.InpatientRuleFee ruleFeeMgr = new FS.HISFC.BizProcess.Integrate.FeeInterface.InpatientRuleFee();
                List<string> strList = ruleFeeMgr.GetFeeRuleItemCode();

                if (strList == null)
                {
                    strList = new List<string>();
                }

                foreach (FS.HISFC.Models.Order.ExecOrder order in alExecOrder)
                {
                    order.Order.Patient = patient;
                    order.Order.ExecOper.Dept = order.Order.ExeDept.Clone();
                    string err = "";
                    #region 收费
                    if (order.Order.Item.ID != "999" 
                        || !order.Order.OrderType.IsCharge)
                    {
                        FS.HISFC.Models.Order.Inpatient.Order o = order.Order;

                        if (FillFeeItem(ref o, out err, 1) == -1)
                        {
                            this.Err = err;
                            return -1;
                        }
                    }

                    //By Maokb 061016
                    bool isNeedConfirm = true;

                    //对于不需要终端确认项目或（执行科室是本科室或病区）的进行收费和确认执行处理
                    if (order.Order.Item.ID != "999")
                    {
                        //现在存的医嘱状态有问题，所以这个过段时间再打开，2013年1月9日18:07:00
                        //if (!order.Order.IsSubtbl)
                        //{
                        ((FS.HISFC.Models.Fee.Item.Undrug)order.Order.Item).IsNeedConfirm = this.fee.GetItem(order.Order.Item.ID).IsNeedConfirm;
                        //}
                    }
                    //if (((FS.HISFC.Models.Fee.Item.Undrug)order.Order.Item).IsNeedConfirm == false ||
                    //    order.Order.ExeDept.ID == order.Order.ReciptDept.ID ||
                    //      order.Order.ExeDept.ID == nurseCode)
                    //{
                    if (CheckCharge(patient, order.Order))
                    {
                        isNeedConfirm = false;
                        order.IsCharge = false;

                        if (order.Order.OrderType.IsCharge == false &&
                            order.Order.IsSubtbl == false)
                        {
                            //医嘱是嘱托，不是附材的不收费。
                        }
                        else if (!this.IsFee(patient, order.Order) &&  //order.Order.Item.Price <= 0 &&
                            ((FS.HISFC.Models.Fee.Item.Undrug)order.Order.Item).UnitFlag != "1")
                        {
                            //不是复合项目，价格小于零的不收费
                            order.IsCharge = true;
                        }
                        else
                        {
                            //规则收费的不处理
                            if (strList.Contains(order.Order.Item.ID.Trim()))
                            {
                                isNeedConfirm = false;
                            }
                            else if (!isCharge)
                            {
                                isNeedConfirm = false;
                            }
                            else
                            {
                                /*收费*/
                                order.IsCharge = true;
                                order.ChargeOper.Dept = order.Order.NurseStation;
                                order.ChargeOper.ID = oper.ID;
                                order.ChargeOper.OperTime = dt;
                                order.Order.Oper = order.ChargeOper.Clone();

                                #region 如果是复合项目－变成细项
                                if (((FS.HISFC.Models.Fee.Item.Undrug)order.Order.Item).UnitFlag == "1")
                                {
                                    ArrayList al = managerPack.QueryUndrugPackagesBypackageCode(order.Order.Item.ID);
                                    if (al == null)
                                    {
                                        this.Err = "获得细项出错！" + managerPack.Err;

                                        return -1;
                                    }

                                    decimal rate = 1;
                                    foreach (FS.HISFC.Models.Fee.Item.Undrug undrug in al)
                                    {
                                        FS.HISFC.Models.Order.ExecOrder myorder = null;
                                        decimal qty = order.Order.Qty;
                                        myorder = order.Clone();
                                        myorder.Name = undrug.Name;
                                        myorder.Order.Name = undrug.Name;
                                        myorder.Order.User03 = order.ID;
                                        /*收费*/
                                        myorder.IsCharge = true;
                                        myorder.ChargeOper.Dept = order.Order.NurseStation;
                                        myorder.ChargeOper.ID = oper.ID;
                                        myorder.ChargeOper.OperTime = dt;

                                        myorder.Order.Oper = myorder.ChargeOper.Clone();

                                        myorder.Order.Item = undrug.Clone();
                                        myorder.Order.Qty = qty * undrug.Qty;//数量==复合项目数量*小项目数量
                                        myorder.Order.Item.Qty = qty * undrug.Qty;//数量==复合项目数量*小项目数量

                                        //写入复合项目编码、名称
                                        myorder.Order.Package.ID = order.Order.Item.ID;
                                        myorder.Order.Package.Name = order.Order.Item.Name;
                                        myorder.Order.Package.Qty = qty;
                                        FS.HISFC.Models.Order.Inpatient.Order o = myorder.Order;

                                        rate = fee.GetItemRateForZT(order.Order.Item.ID, undrug.ID);
                                        if (FillFeeItem(ref o, out err, rate) == -1)
                                        {
                                            this.Err = err;
                                            return -1;
                                        }

                                        if (this.IsFee(patient, myorder.Order))  // myorder.Order.Item.Price > 0)
                                        {
                                            alChargeOrders.Add(myorder.Order);
                                        }
                                    }
                                }
                                #endregion

                                #region 单个项目收费
                                else //普通项目收费
                                {
                                    order.Order.User03 = order.ID;

                                    alChargeOrders.Add(order.Order);
                                }
                                #endregion
                            }
                        }
                    }
                    //}

                    #region //更新确认标记及执行标记
                    try
                    {
                        //付执行数据
                        order.ExecOper.ID = oper.ID;
                        order.ExecOper.Name = oper.Name;
                        //如果需要终端确认 则IsExec为未执行 IsCharge为未收费
                        order.IsExec = !isNeedConfirm;

                        //上面已经赋值
                        //order.IsCharge = !isNeedConfirm;

                        //对于所有的非药品项目 都置确认标记
                        //order.IsConfirm = !isNeedConfirm;

                        //这个标记护士已经处理完医嘱了，终端确认用IsExec来区分是否确认执行
                        order.IsConfirm = true;

                        order.ExecOper.OperTime = dt;

                        if (order.ExecOper.Dept.ID != "")
                        {
                            order.Order.ExecOper = order.ExecOper.Clone();
                        }
                        order.Order.Oper.ID = oper.ID;

                        if (order.Order.Item.ID == "999" || !order.Order.OrderType.IsCharge)
                        {
                            order.IsCharge = true;
                            order.ChargeOper.Dept = order.Order.NurseStation.Clone();
                            order.ChargeOper.ID = oper.ID;
                            order.ChargeOper.Name = oper.Name;
                            order.ChargeOper.OperTime = dt;
                            order.Order.Oper = order.ChargeOper.Clone();
                        }
                    }
                    catch (Exception ex)
                    {
                        this.Err = "设置执行数据出错！" + ex.Message;
                        return -1;
                    }
                    //更新执行档执行标记 
                    iReturn = orderManager.UpdateForConfirmExecUnDrug(order, isOrderConfirmFee);

                    #endregion

                    if (iReturn == -1) //错误
                    {
                        this.Err = orderManager.Err;
                        return -1;
                    }
                    else if (iReturn == 0)
                    {
                        this.Err = order.Order.Patient.PVisit.PatientLocation.Bed.ID.Substring(4) + "床 " + order.Order.Patient.Name + "\r\n\r\n组号:" + order.SubCombNO.ToString() + " " + order.Order.Item.Name + "\r\n医嘱已经发生变化，请刷新屏幕！";
                        return -1;
                    }

                    #region 非药品和嘱托医嘱，更新执行标记

                    if (order.Order.Item.ID == "999" || !order.Order.OrderType.isCharge)
                    {
                        iReturn = orderManager.UpdateOrderStatus(order.Order.ID, 2);
                        if (iReturn == -1) //错误
                        {
                            this.Err = orderManager.Err;
                            return -1;
                        }
                    }
                    #endregion

                    #endregion
                }
            }
            #endregion

            if (alChargeOrders.Count > 0) //临时医嘱
            {
                //{B2E4E2ED-08CF-41a8-BF68-B9DF7454F9BB} 欠费判断
                fee.MessageType = this.messType;

                if (fee.FeeItem(patient, ref alChargeOrders) == -1)
                {
                    this.Err = fee.Err;
                    return -1;
                }
            }

            System.Collections.Hashtable hsRecipe = new Hashtable();

            foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItem in alChargeOrders)
            {
                if (!hsRecipe.ContainsKey(feeItem.ExecOrder.ID))
                {
                    hsRecipe.Add(feeItem.ExecOrder.ID, feeItem);
                }
            }

            if (alDrugSendOrders.Count > 0)
            {
                foreach (FS.HISFC.Models.Order.ExecOrder drugOrder in alDrugSendOrders)
                {
                    FS.HISFC.Models.Fee.Inpatient.FeeItemList tempFee = hsRecipe[drugOrder.ID] as FS.HISFC.Models.Fee.Inpatient.FeeItemList;
                    drugOrder.Order.ReciptNO = tempFee.RecipeNO;
                    drugOrder.Order.SequenceNO = tempFee.SequenceNO;
                }
            }

            if (alDrugSendOrders.Count > 0) //需要发药医嘱
            {
                if (SendDrug(alDrugSendOrders, isNurseCharge, dt) == -1)
                {
                    return -1;
                }
            }

            return 0;
        }
        #endregion

        #region 药品、非药品项目付值

        private static Fee interFee = new Fee();

        /// <summary>
        /// 获得非药品信息
        /// </summary>
        /// <param name="order"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public int FillFeeItem(ref FS.HISFC.Models.Order.Inpatient.Order order, out string err)
        {
            return FillFeeItem(ref order, out err, 1);
        }

        /// <summary>
        /// 获得非药品信息
        /// </summary>
        /// <param name="order"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public int FillFeeItem(ref FS.HISFC.Models.Order.Inpatient.Order order, out string err, decimal rate)
        {
            err = "";
            if (order.Item.ID == "999")
            {
                return 0;
            }

            FS.HISFC.BizProcess.Integrate.Fee tempManagerFee = new Fee();

            //ManagerFee.SetTrans(t);

            //{8F86BB0D-9BB4-4c63-965D-969F1FD6D6B2} 医嘱附材绑定物资 by gengxl
            //FS.HISFC.Models.Fee.Item.Undrug item = tempManagerFee.GetItem(order.Item.ID);
            FS.HISFC.Models.Base.Item item = tempManagerFee.GetUndrugAndMatItem(order.Item.ID, order.Item.Price);

            if (item == null)
            {
                err = order.Patient.PVisit.PatientLocation.Bed.ID.Substring(4) + "床 " + order.Patient.Name + "\r\n\r\n组号:" + order.SubCombNO.ToString() + " " + order.Item.Name + "\r\n已经停用!";
                return -1;
            }

            //{8F86BB0D-9BB4-4c63-965D-969F1FD6D6B2} 医嘱附材绑定物资 by gengxl
            if (item is FS.HISFC.Models.Fee.Item.Undrug)
            {
                //现在存的医嘱状态有问题，所以这个过段时间再打开，2013年1月9日18:07:00
                //if (!order.IsSubtbl)
                //{
                ((FS.HISFC.Models.Fee.Item.Undrug)order.Item).IsNeedConfirm = item.IsNeedConfirm;
                //}
                order.Item.Price = item.Price;
                order.Item.SpecialPrice = item.SpecialPrice;
                order.Item.ChildPrice = item.ChildPrice;
                order.Item.Name = item.Name;
                order.Item.Specs = item.Specs;

                decimal price = 0;
                decimal orgPrice = 0;

                interFee.GetPriceForInpatient(order.Patient, order.Item, ref price, ref orgPrice, rate);
                if (price > 0)
                {
                    order.Item.Price = price;
                }

                order.Item.MinFee = item.MinFee;
                order.Item.SysClass = item.SysClass.Clone();
                //{8F86BB0D-9BB4-4c63-965D-969F1FD6D6B2} 医嘱附材绑定物资 by gengxl
                ((FS.HISFC.Models.Fee.Item.Undrug)order.Item).UnitFlag = ((FS.HISFC.Models.Fee.Item.Undrug)item).UnitFlag;
                ////复制医嘱时，此处可以获取基本信息中存储的执行科室 houwb 2011-4-7
                //if (string.IsNullOrEmpty(order.ExeDept.ID) && !string.IsNullOrEmpty(((FS.HISFC.Models.Fee.Item.Undrug)item).ExecDept))
                //{
                //    order.ExeDept = new FS.FrameWork.Models.NeuObject(((FS.HISFC.Models.Fee.Item.Undrug)item).ExecDept, "", "");
                //}
            }
            else if (item is FS.HISFC.Models.FeeStuff.MaterialItem)
            {
                (item as FS.HISFC.Models.FeeStuff.MaterialItem).IsNeedConfirm = false;
                order.Item.Price = item.Price;
                order.Item.SpecialPrice = item.SpecialPrice;
                order.Item.ChildPrice = item.ChildPrice;
                order.Item.Name = item.Name;
                order.Item.Specs = item.Specs;

                decimal price = 0;
                decimal orgPrice = 0;
                interFee.GetPriceForInpatient(order.Patient, order.Item, ref price, ref orgPrice);
                if (price > 0)
                {
                    order.Item.Price = price;
                }

                order.Item.MinFee = item.MinFee;
                order.Item.SysClass = item.SysClass.Clone();
                order.StockDept.ID = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID;
                order.ExecOper.Dept.ID = order.StockDept.ID;
                order.Item.ItemType = EnumItemType.MatItem;
            }
            return 0;
        }

        #region 性能优化{AD50C155-BE2D-47b8-8AF9-4AF3548A2726}

        private Hashtable htItem = new Hashtable();

        private Hashtable htDrug = new Hashtable();

        /// <summary>
        /// 获得基本信息
        /// </summary>
        /// <param name="order"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public int FillPharmacyItem(ref FS.HISFC.Models.Order.Inpatient.Order order, out string err)
        {
            err = "";
            if (order.Item.ID == "999")
            {
                if (order.Item.ItemType == EnumItemType.Drug)//药品
                {
                    try
                    {
                        ((FS.HISFC.Models.Pharmacy.Item)order.Item).Type.ID = order.Item.SysClass.ID.ToString().Substring(order.Item.SysClass.ID.ToString().Length - 1, 1);
                    }
                    catch { }
                }
                return 0;
            }
            FS.HISFC.Models.Pharmacy.Item item;

            if (htDrug.Contains(order.Item.ID))
            {
                item = htDrug[order.Item.ID] as FS.HISFC.Models.Pharmacy.Item;
            }
            else
            {
                item = ManagerPharmacy.GetItem(order.Item.ID);

                htDrug.Add(order.Item.ID, item);
            }


            if (item == null || item.ValidState != EnumValidState.Valid)
            {
                err = order.Patient.PVisit.PatientLocation.Bed.ID.Substring(4) + "床 " + order.Patient.Name + "\r\n\r\n组号:" + order.SubCombNO.ToString() + " " + order.Item.Name + "\r\n已经停用!";
                return -1;
            }

            //添加重新赋值零售价
            ((FS.HISFC.Models.Pharmacy.Item)order.Item).PriceCollection.RetailPrice = item.Price;

            // {884A444E-D843-4a8f-8264-01C755D93424}
            // 添加第二零售价
            ((FS.HISFC.Models.Pharmacy.Item)order.Item).RetailPrice2 = item.RetailPrice2;

            order.Item.MinFee = item.MinFee;
            order.Item.Price = item.Price;
            order.Item.Name = item.Name;
            order.Item.Specs = item.Specs;
            order.Item.SysClass = item.SysClass.Clone();//付给系统类别
            ((FS.HISFC.Models.Pharmacy.Item)order.Item).IsAllergy = item.IsAllergy;
            ((FS.HISFC.Models.Pharmacy.Item)order.Item).PackUnit = item.PackUnit;
            ((FS.HISFC.Models.Pharmacy.Item)order.Item).MinUnit = item.MinUnit;
            ((FS.HISFC.Models.Pharmacy.Item)order.Item).BaseDose = item.BaseDose;
            ((FS.HISFC.Models.Pharmacy.Item)order.Item).DosageForm = item.DosageForm;
            return 0;
        }

        /// <summary>
        /// 获得药品信息
        /// </summary>
        /// <param name="order"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public int FillPharmacyItemWithStockDept(ref FS.HISFC.Models.Order.Inpatient.Order order, out string err)
        {
            err = "";
            if (order.Item.ID == "999")
            {
                if (order.Item.ItemType == EnumItemType.Drug)//药品
                {
                    try
                    {
                        ((FS.HISFC.Models.Pharmacy.Item)order.Item).MinUnit = order.DoseUnit;

                        //置药品类型给药品?????
                        ((FS.HISFC.Models.Pharmacy.Item)order.Item).Type.ID = order.Item.SysClass.ID.ToString().Substring(order.Item.SysClass.ID.ToString().Length - 1, 1);
                    }
                    catch
                    {
                    }
                }
                return 0;
            }
            else
            {
                if (order.Patient != null)
                {
                    FS.HISFC.Models.Pharmacy.Storage storage;

                    FS.HISFC.Models.Pharmacy.Item item;
                    if (htDrug.Contains(order.Item.ID))
                    {
                        item = htDrug[order.Item.ID] as FS.HISFC.Models.Pharmacy.Item;
                    }
                    else
                    {
                        item = ManagerPharmacy.GetItem(order.Item.ID);

                        htDrug.Add(order.Item.ID, item);
                    }

                    //if (htStorage.Contains(order.Item.ID + order.Patient.PVisit.PatientLocation.Dept.ID))
                    //{
                    //    storage = htStorage[order.Item.ID + order.Patient.PVisit.PatientLocation.Dept.ID] as FS.HISFC.Models.Pharmacy.Storage;
                    //}
                    //else
                    //{
                    try
                    {
                        FS.HISFC.BizProcess.Integrate.Pharmacy ManagerPharmacy = new FS.HISFC.BizProcess.Integrate.Pharmacy();

                        if (!string.IsNullOrEmpty(order.StockDept.ID))
                        {
                            storage = ManagerPharmacy.GetStockInfoByDrugCode(order.StockDept.ID, order.Item.ID);
                        }
                        else
                        {
                            //houwb 2011-5-30 增加发药类型判断
                            if (!string.IsNullOrEmpty(order.ReciptDept.ID))
                            {
                                storage = ManagerPharmacy.GetItemStorage(order, order.ReciptDept.ID, "I", order.Item.ID);
                            }
                            else
                            {
                                storage = ManagerPharmacy.GetItemStorage(order, ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID, "I", order.Item.ID);
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        err = "查询库存出错！\r\n" + ex.Message;
                        err = order.Patient.PVisit.PatientLocation.Bed.ID.Substring(4) + "床 " + order.Patient.Name + "\r\n\r\n组号:" + order.SubCombNO.ToString() + " " + order.Item.Name + " 查询库存出错！\r\n" + ex.Message;
                        return -1;
                    }

                    //htStorage.Add(order.Item.ID + order.Patient.PVisit.PatientLocation.Dept.ID, storage);
                    //}
                    if (storage == null || storage.ValidState != EnumValidState.Valid)
                    {
                        err = "已经停用!";
                        err = order.Patient.PVisit.PatientLocation.Bed.ID.Substring(4) + "床 " + order.Patient.Name + "\r\n\r\n组号:" + order.SubCombNO.ToString() + " " + order.Item.Name + " 已经停用!";

                        return -1;
                    }
                    else if (storage.Item.ID == "" || storage.StoreQty - storage.PreOutQty <= 0)
                    {
                        if (order.OrderType.IsCharge)
                        {
                            err = "库存不足!";
                            err = order.Patient.PVisit.PatientLocation.Bed.ID.Substring(4) + "床 " + order.Patient.Name + "\r\n\r\n组号:" + order.SubCombNO.ToString() + " " + order.Item.Name + " 库存不足!";

                            return -1;
                        }
                    }

                    if (string.IsNullOrEmpty(order.StockDept.ID))
                    {
                        order.StockDept.ID = storage.StockDept.ID;//
                        order.StockDept.Name = storage.StockDept.Name;//
                    }

                    //添加重新赋值零售价
                    ((FS.HISFC.Models.Pharmacy.Item)order.Item).PriceCollection.RetailPrice = storage.Item.Price;

                    // {884A444E-D843-4a8f-8264-01C755D93424}
                    // 添加第二零售价
                    ((FS.HISFC.Models.Pharmacy.Item)order.Item).RetailPrice2 = storage.Item.RetailPrice2;

                     if (IGetItemPrice == null)
                     {
                        IGetItemPrice = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Fee.IGetItemPrice)) as FS.HISFC.BizProcess.Interface.Fee.IGetItemPrice;
                     }

                     if (IGetItemPrice != null)
                     {
                         decimal orgPrice = 0;
                         order.Item.Price =  IGetItemPrice.GetPriceForInpatient(storage.Item.ID, order.Patient, storage.Item.Price, storage.Item.Price, storage.Item.Price, storage.Item.RetailPrice2, ref orgPrice);
                     }
                     else
                     {
                         order.Item.Price = storage.Item.Price;
                     }

                    if (order.Item.ID != "999")
                    {
                        FS.HISFC.Models.Pharmacy.Item phaItem = item;
                        if (phaItem != null)
                        {
                            order.Item.MinFee = phaItem.MinFee;
                            order.Item.Name = phaItem.Name;
                            order.Item.Specs = phaItem.Specs;
                            order.Item.SysClass = phaItem.SysClass.Clone();//付给系统类别
                            ((FS.HISFC.Models.Pharmacy.Item)order.Item).IsAllergy = phaItem.IsAllergy;
                            ((FS.HISFC.Models.Pharmacy.Item)order.Item).PackUnit = phaItem.PackUnit;
                            ((FS.HISFC.Models.Pharmacy.Item)order.Item).MinUnit = phaItem.MinUnit;
                            ((FS.HISFC.Models.Pharmacy.Item)order.Item).BaseDose = phaItem.BaseDose;
                            ((FS.HISFC.Models.Pharmacy.Item)order.Item).DosageForm = phaItem.DosageForm;
                        }
                    }
                }
            }
            return 0;
        }

        #endregion


        #endregion

        #region 医嘱保存

        /// <summary>
        /// 住院附材算法接口
        /// </summary>
        static FS.HISFC.BizProcess.Interface.Order.IDealSubjob iDealSubjob = null;

        /// <summary>
        /// 住院附材处理
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="order"></param>
        /// <param name="alOrders"></param>
        /// <param name="alSubOrders"></param>
        /// <param name="errInfo"></param>
        /// <returns></returns>
        public static int DealSubjobByInpatient(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.HISFC.Models.Order.Inpatient.Order order, ArrayList alOrders, ref ArrayList alSubOrders, ref string errInfo)
        {
            if (iDealSubjob == null)
            {
                iDealSubjob = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(FS.HISFC.BizProcess.Integrate.Order), typeof(FS.HISFC.BizProcess.Interface.Order.IDealSubjob)) as FS.HISFC.BizProcess.Interface.Order.IDealSubjob;
            }
            if (iDealSubjob != null)
            {
                //附材带出
                return iDealSubjob.DealSubjob(patientInfo, true, order, alOrders, ref alSubOrders, ref errInfo);
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 保存医嘱
        /// </summary>
        /// <param name="al"></param>
        /// <param name="deptCode"></param>
        /// <param name="err"></param>
        /// <param name="strNameNotUpdate"></param>
        /// <returns></returns>
        public int SaveOrder(List<FS.HISFC.Models.Order.Inpatient.Order> al, string deptCode,
            out string err, out string strNameNotUpdate, System.Data.IDbTransaction t)
        {
            FS.HISFC.BizLogic.Order.Order orderManager = new FS.HISFC.BizLogic.Order.Order();
            FS.HISFC.BizLogic.Order.AdditionalItem AdditionalItemManagement = new FS.HISFC.BizLogic.Order.AdditionalItem();
            FS.HISFC.BizProcess.Integrate.Fee itemManager = new FS.HISFC.BizProcess.Integrate.Fee();

            itemManager.SetTrans(t);
            AdditionalItemManagement.SetTrans(t);
            orderManager.SetTrans(t);

            string strComboNo = "";//组合号
            FS.HISFC.Models.Order.Inpatient.Order order = null;
            string strID = "";
            strNameNotUpdate = "";
            err = "";

            FS.HISFC.Models.Order.Inpatient.Order orderTemp = null;
            for (int i = 0; i < al.Count; i++)
            {
                order = al[i];

                #region  处理皮试选择
                //if (order.Item.ItemType == EnumItemType.Drug)
                //{
                //    if (order.HypoTest == 1 || order.HypoTest == 4)			//不需皮试或为阴性
                //    {
                //        ((FS.HISFC.Models.Pharmacy.Item)order.Item).IsAllergy = false;
                //    }
                //    else
                //    {
                //        ((FS.HISFC.Models.Pharmacy.Item)order.Item).IsAllergy = true;
                //    }
                //}
                #endregion

                #region 保存医嘱

                //外部获取医嘱流水号
                orderTemp = orderManager.QueryOneOrder(order.ID);
                if (orderTemp == null)
                {
                    if (orderManager.InsertOrder(order) == -1)
                    {
                        err = orderManager.Err;
                        order.ID = "";
                        return -1;
                    }
                }
                else
                {
                    int mystatus = orderTemp.Status;
                    if (mystatus == 0 || mystatus == 5)//判断医嘱状态
                    { }
                    else
                    {
                        strNameNotUpdate += "[" + order.Item.Name + "]";
                        continue;
                    }

                    if (orderManager.UpdateOrder(order) == -1)
                    {
                        err = orderManager.Err;
                        return -1;
                    }
                }

                #region 旧的作废

                //if (order.ID == "")
                //{
                //    try
                //    {
                //        #region 新加的医嘱
                //        strID = GetNewOrderID(orderManager);
                //        if (strID == "")
                //        {
                //            err = FS.FrameWork.Management.Language.Msg("获得医嘱流水号出错！");
                //            order.ID = "";
                //            return -1;
                //        }
                //        order.ID = strID; //获得医嘱流水号

                //        if (orderManager.InsertOrder(order) == -1)
                //        {
                //            err = orderManager.Err;
                //            order.ID = "";
                //            return -1;
                //        }
                //        #endregion
                //    }
                //    catch (Exception ex)
                //    {
                //        err = ex.Message;
                //        order.ID = "";
                //        return -1;
                //    }
                //}
                //else
                //{
                //    #region 更新的医嘱

                //    int mystatus = orderManager.QueryOneOrder(order.ID).Status;
                //    if (mystatus == 0 || mystatus == 5)//判断医嘱状态
                //    { }
                //    else
                //    {
                //        strNameNotUpdate += "[" + order.Item.Name + "]";
                //        continue;
                //    }

                //    #endregion
                //    if (orderManager.UpdateOrder(order) == -1)
                //    {
                //        err = orderManager.Err;
                //        return -1;
                //    }
                //}
                #endregion

                #endregion

                #region 组合医嘱

                if (strComboNo != order.Combo.ID || order.Item.ItemType != EnumItemType.Drug)
                {
                    //药品,非药品
                    strComboNo = order.Combo.ID;
                    #region 获得附材
                    //删除已经有的附材
                    if (orderManager.DeleteOrderSubtbl(order.Combo.ID) == -1)
                    {
                        err = FS.FrameWork.Management.Language.Msg("删除附材项目信息出错！") + orderManager.Err;
                        return -1;
                    }
                    ArrayList alSubtbls = null;


                    string errInfo = "";
                    int rev = DealSubjobByInpatient(order.Patient, order.Clone(), new ArrayList(al), ref alSubtbls, ref errInfo);
                    if (rev == -1)
                    {
                        err = errInfo;
                        return -1;
                    }
                    else if (rev == 0)
                    {
                        if (order.Item.ItemType == EnumItemType.Drug)//药品，根据用法
                        {
                            alSubtbls = AdditionalItemManagement.QueryAdditionalItem(true, order.Usage.ID, deptCode);
                        }
                        else//非药品，根据根据项目编码
                        {
                            alSubtbls = AdditionalItemManagement.QueryAdditionalItem(false, order.Item.ID, deptCode);
                        }
                    }

                    for (int m = 0; m < alSubtbls.Count; m++)
                    {
                        FS.HISFC.Models.Fee.Item.Undrug item = null;

                        item = itemManager.GetItem(((FS.HISFC.Models.Base.Item)alSubtbls[m]).ID);//获得最新项目信息

                        if (item == null || !item.IsValid)
                        {
                            //附材停用，没找到
                        }
                        else
                        {
                            item.Qty = ((FS.HISFC.Models.Base.Item)alSubtbls[m]).Qty;

                            FS.HISFC.Models.Order.Inpatient.Order newOrder = order.Clone();

                            if (!order.OrderType.IsCharge)
                            {
                                if (order.OrderType.Type == FS.HISFC.Models.Order.EnumType.LONG)
                                {
                                    newOrder.OrderType.ID = "CZ";
                                    newOrder.OrderType.Name = "长期医嘱";
                                    newOrder.OrderType.IsCharge = true;
                                }
                                else
                                {
                                    newOrder.OrderType.ID = "LZ";
                                    newOrder.OrderType.Name = "临时医嘱";
                                    newOrder.OrderType.IsCharge = true;
                                }
                            }

                            newOrder.Item = item.Clone();
                            newOrder.Qty = item.Qty;
                            newOrder.Unit = item.PriceUnit;
                            newOrder.IsSubtbl = true;
                            newOrder.Usage = new FS.FrameWork.Models.NeuObject();
                            if (order.Item.SysClass.ID.ToString() == "UL")
                            {
                                newOrder.ExeDept.ID = newOrder.ReciptDept.ID;
                                newOrder.ExeDept.Name = newOrder.ReciptDept.Name;
                            }

                            strID = GetNewOrderID(orderManager);

                            if (order.Item.ItemType != EnumItemType.Drug)//非药品，置非药品附材标记
                            {
                                //附材的执行科室与医嘱相同
                                //newOrder.ExeDept = newOrder.Patient.PVisit.PatientLocation.Dept.Clone();//执行科室为患者所在科室
                            }

                            if (strID == "")
                            {
                                err = FS.FrameWork.Management.Language.Msg("获得医嘱流水号出错！");
                                return -1;
                            }

                            newOrder.ID = strID; //获得医嘱流水号
                            if (orderManager.InsertOrder(newOrder) == -1)
                            {
                                err = orderManager.Err;
                                return -1;
                            }
                        }
                    }
                    #endregion

                }
                #endregion
            }
            return 0;

        }
        #endregion

        #region 获得医嘱流水号
        /// <summary>
        /// 获得医嘱流水号
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static string GetNewOrderID(FS.HISFC.BizLogic.Order.Order o)
        {

            string rtn = o.GetNewOrderID();
            if (rtn == null || rtn == "")
            {
                MessageBox.Show("错误获得医嘱流水号！");
            }
            else
            {
                return rtn;
            }
            return "";
        }
        #endregion

        #region 集中发送
        /// <summary>
        /// 集中发送药品
        /// </summary>
        /// <returns></returns>
        public int SendDrug(List<FS.HISFC.Models.Order.ExecOrder> alExecOrder, int sendFlag)
        {
            FS.HISFC.Models.Order.ExecOrder order = null;
            DateTime dt = orderManager.GetDateTimeFromSysDateTime();
            #region 药品
            for (int i = 0; i < alExecOrder.Count; i++)
            {

                order = alExecOrder[i];
                if (order == null)
                {

                    this.Err = "没查询到医嘱！";
                    return -1;
                }

                #region 填充项目信息 检验有效性
                string err;
                FS.HISFC.Models.Order.Inpatient.Order myOrder = order.Order;
                if (FillPharmacyItem(ref myOrder, out err) == -1)
                {

                    this.Err = err;
                    return -1;
                }
                #endregion

                #region 插入药品发送表
                if (order.IsCharge)
                {
                    order.DrugFlag = sendFlag;
                    int parm = this.SendToDrugStore(order, dt);
                    if (parm == -1)
                    {
                        if (ManagerPharmacy.ErrCode == "-1") //发药返回Oracle错误为零，没找到摆药单
                        {
                            #region 发送摆药单判断

                            FS.HISFC.Models.Pharmacy.Item item = order.Order.Item as FS.HISFC.Models.Pharmacy.Item;
                            if (item == null)
                            {

                                this.Err = "非药品 无法进行集中发送";
                                return -1;
                            }
                            else
                            {
                                this.Err = ("摆药对应的摆药单未进行设置! 请与药学部或信息科联系" +
                                    "\n医嘱类型:" + order.Order.OrderType.ID + " \n药品类型:" + item.Type.ID +
                                    " \n用法:" + order.Order.Usage.Name + " \n药品性质:" + item.Quality.ID +
                                    " \n药品剂型:" + item.DosageForm.ID);
                                return -1;
                            }

                            #endregion
                        }
                        else
                        {

                            this.Err = ("插入药品申请失败！\n" + ManagerPharmacy.Err);
                            return -1;
                        }
                    }
                }
                #endregion

                #region 发药标记
                int iReturn = 0;

                iReturn = orderManager.SetDrugFlag(order.ID, sendFlag);

                if (iReturn == -1) //错误!
                {
                    this.Err = orderManager.Err;
                    return -1;
                }
                if (iReturn == 0) //并发
                {
                    this.Err = ("其中发药信息已经发生变化,请关闭窗口再试!");
                    return -1;
                }
                #endregion

            }
            #endregion
            return 0;
        }

        #endregion

        #region 收费

        /// <summary>
        /// 药品和非药品的价格为0是否收费：0不收取；1收取。默认值为0不收取
        /// </summary>
        private string isFeeWhenPriceZero = "-1";

        /// <summary>
        /// 药品和非药品的价格为0是否收费：0不收取；1收取。默认值为0不收取
        /// </summary>
        public string IsFeeWhenPriceZero
        {
            get
            {
                if (this.isFeeWhenPriceZero == "-1")
                {
                    FS.HISFC.BizProcess.Integrate.Common.ControlParam con = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
                    this.isFeeWhenPriceZero = con.GetControlParam<string>("FEE001", false, "0");
                }
                return this.isFeeWhenPriceZero;
            }
        }

        /// <summary>
        /// 是否收费
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public bool IsFee(FS.HISFC.Models.RADT.PatientInfo patient, FS.HISFC.Models.Order.Inpatient.Order order)
        {
            if (this.IsFeeWhenPriceZero == "1")
            {
                return true;
            }

            if (order.Item.Price > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        #endregion

        #endregion

        #region 对外函数


        /// <summary>
        /// 是否出单
        /// 对不出单的医院，都返回1
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="order"></param>
        /// <returns>0 不收费/出单 1 收费 -1 出错了</returns>
        public int IsCanFee(FS.HISFC.Models.RADT.PatientInfo patient, FS.HISFC.Models.Order.Inpatient.Order order)
        {
            return 1;
        }


        /// <summary>
        /// 执行记录
        /// 更新医嘱执行信息
        /// 对医技开放使用
        /// </summary>
        /// <param name="execOrder">执行档信息</param>
        /// <returns>0 success -1 fail</returns>
        public int UpdateRecordExec(FS.HISFC.Models.Order.ExecOrder execOrder)
        {
            return orderManager.UpdateRecordExec(execOrder);
        }

        /// <summary>
        /// 收费记录
        /// 更新执行医嘱收费人，收费标记，发票号等
        /// 对费用开放使用
        /// </summary>
        /// <param name="execOrder">执行档信息</param>
        /// <returns>0 success -1 fail</returns>
        public int UpdateChargeExec(FS.HISFC.Models.Order.ExecOrder execOrder)
        {
            return orderManager.UpdateChargeExec(execOrder);
        }
        /// <summary>
        /// 配药记录
        /// 对药房开放使用,更新DrugFlag
        /// </summary>
        /// <param name="execOrder">执行档信息</param>
        /// <returns>0 success -1 fail</returns>
        public int UpdateDrugExec(FS.HISFC.Models.Order.ExecOrder execOrder)
        {
            return orderManager.UpdateDrugExec(execOrder);
        }
        /// <summary>
        /// 更新医嘱配药标记
        /// 对药房的接口
        /// 对药房开放使用
        /// </summary>
        /// <param name="execOrderID">执行单ID</param>
        /// <param name="orderNo">主挡ID</param>
        /// <param name="userID">操作员</param>
        /// <param name="deptID">配药科室</param>
        /// <returns>-1 失败 0 成功</returns>
        public int UpdateOrderDruged(string execOrderID, string orderNo, string userID, string deptID)
        {
            return orderManager.UpdateOrderDruged(execOrderID, orderNo, userID, deptID);
        }
        /// <summary>
        /// 更新医嘱配药标记
        /// 对药房开放使用
        /// </summary>
        /// <param name="execOrderID">执行单ID</param>
        /// <param name="userID">操作员</param>
        /// <param name="deptID">配药科室</param>
        /// <returns>-1 失败 0 成功</returns>
        public int UpdateOrderDruged(string execOrderID, string userID, string deptID)
        {
            return UpdateOrderDruged(execOrderID, "", userID, deptID);
        }


        /// <summary>
        /// 发送摆药通知\设置发药方式
        /// 0不需发送/1集中发送/2分散发送/3已配药
        /// 对药房开放使用
        /// </summary>
        /// <param name="execOrderID"></param>
        /// <param name="drugFlag">0不需发送/1集中发送/2分散发送/3已配药</param>
        /// <returns></returns>
        public int SetDrugFlag(string execOrderID, int drugFlag)
        {
            return orderManager.SetDrugFlag(execOrderID, drugFlag);
        }
        /// <summary>
        /// 更新发送通知
        /// 对药房开放使用
        /// </summary>
        /// <param name="nurse"></param>
        /// <returns></returns>
        public int SendInformation(FS.FrameWork.Models.NeuObject nurse)
        {
            return orderManager.SendInformation(nurse);

        }

        /// <summary>
        /// 发药，插入药品申请表
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public int SendToDrugStore(FS.HISFC.Models.Order.ExecOrder order, DateTime dt)
        {
            if (order.DrugFlag == 0) return 0;//未发药不插入药品申请表

            int i = ManagerPharmacy.ApplyOut(order, dt, false);
            if (i == -1) //发药返回Oracle错误为零，没找到摆药单
            {
                if (ManagerPharmacy.ErrCode == "-1")
                {
                    #region 发送摆药单判断

                    FS.HISFC.Models.Pharmacy.Item item = order.Order.Item as FS.HISFC.Models.Pharmacy.Item;
                    if (item == null)
                    {

                        this.Err = "非药品 无法进行集中发送";
                        return -1;
                    }
                    else
                    {
                        FS.HISFC.BizLogic.Manager.Constant consManager = new FS.HISFC.BizLogic.Manager.Constant();
                        consManager.SetTrans(this.trans);
                        FS.FrameWork.Models.NeuObject consDosage = consManager.GetConstant(FS.HISFC.Models.Base.EnumConstant.DOSAGEFORM, item.DosageForm.ID);
                        string dosageForm = consDosage.Name;

                        FS.FrameWork.Models.NeuObject consQuality = consManager.GetConstant(FS.HISFC.Models.Base.EnumConstant.DRUGQUALITY, item.Quality.ID);
                        string drugQuality = consQuality.Name;

                        FS.FrameWork.Models.NeuObject consType = consManager.GetConstant(FS.HISFC.Models.Base.EnumConstant.ITEMTYPE, item.Type.ID);
                        string drugType = consType.Name;

                        FS.HISFC.BizLogic.Manager.OrderType orderTypeManager = new FS.HISFC.BizLogic.Manager.OrderType();
                        orderTypeManager.SetTrans(this.trans);

                        ArrayList alList = orderTypeManager.GetList();
                        string orderType = order.Order.OrderType.ID;
                        if (alList != null)
                        {
                            foreach (FS.HISFC.Models.Order.OrderType tempType in alList)
                            {
                                if (tempType.ID == order.Order.OrderType.ID)
                                {
                                    orderType = tempType.Name;
                                }
                            }
                        }

                        this.Err = ("摆药对应的摆药单未进行设置! 请与药学部或信息科联系" +
                            "\n医嘱类型: " + orderType + " \n药品类型: " + consType +
                            " \n用法:     " + order.Order.Usage.Name + " \n药品性质: " + drugQuality +
                            " \n药品剂型: " + consDosage.Name);
                        return -1;
                    }

                    #endregion
                }
                else
                {
                    this.Err = ManagerPharmacy.Err;
                }
            }

            return i;

        }


        /// <summary>
        /// 发药申请
        /// </summary>
        /// <param name="execOrder"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public int SendToDrugStore(ArrayList execOrder, DateTime dt)
        {
            string err = "";
            for (int i = 0; i < execOrder.Count; i++)
            {
                FS.HISFC.Models.Order.ExecOrder order = execOrder[i] as FS.HISFC.Models.Order.ExecOrder;
                if (order.DrugFlag == 0)
                {
                    execOrder.Remove(order);
                    i--;
                }
            }

            //ManagerPharmacy.HsApplyExecSeq = this.hsApplyExecSeq;

            int j = ManagerPharmacy.ApplyOutByExeOrder(execOrder, dt, false, ref err);
            if (j == -1) //发药返回Oracle错误为零，没找到摆药单
            {
                if (ManagerPharmacy.ErrCode == "-1" && err != "")
                {
                    #region 发送摆药单判断
                    foreach (FS.HISFC.Models.Order.ExecOrder execOrdObj in execOrder)
                    {
                        if (execOrdObj.Order.Item.ID == err)
                        {
                            FS.HISFC.Models.Pharmacy.Item item = execOrdObj.Order.Item as FS.HISFC.Models.Pharmacy.Item;
                            if (item == null)
                            {

                                this.Err = "非药品 无法进行集中发送";
                                return -1;
                            }
                            else
                            {
                                FS.HISFC.BizLogic.Manager.Constant consManager = new FS.HISFC.BizLogic.Manager.Constant();
                                consManager.SetTrans(this.trans);
                                FS.FrameWork.Models.NeuObject consDosage = consManager.GetConstant(FS.HISFC.Models.Base.EnumConstant.DOSAGEFORM, item.DosageForm.ID);
                                string dosageForm = consDosage.Name;

                                FS.FrameWork.Models.NeuObject consQuality = consManager.GetConstant(FS.HISFC.Models.Base.EnumConstant.DRUGQUALITY, item.Quality.ID);
                                string drugQuality = consQuality.Name;

                                FS.FrameWork.Models.NeuObject consType = consManager.GetConstant(FS.HISFC.Models.Base.EnumConstant.ITEMTYPE, item.Type.ID);
                                string drugType = consType.Name;

                                FS.HISFC.BizLogic.Manager.OrderType orderTypeManager = new FS.HISFC.BizLogic.Manager.OrderType();
                                orderTypeManager.SetTrans(this.trans);

                                ArrayList alList = orderTypeManager.GetList();
                                string orderType = execOrdObj.Order.OrderType.ID;
                                if (alList != null)
                                {
                                    foreach (FS.HISFC.Models.Order.OrderType tempType in alList)
                                    {
                                        if (tempType.ID == execOrdObj.Order.OrderType.ID)
                                        {
                                            orderType = tempType.Name;
                                        }
                                    }
                                }

                                this.Err = ("摆药对应的摆药单未进行设置! 请与药学部或信息科联系" +
                                    "\n医嘱类型: " + orderType + " \n药品类型: " + consType +
                                    " \n用法:     " + execOrdObj.Order.Usage.Name + " \n药品性质: " + drugQuality +
                                    " \n药品剂型: " + consDosage.Name);
                                return -1;
                            }
                        }
                    }

                    #endregion
                }
                else
                {
                    this.Err = ManagerPharmacy.Err;
                }
            }

            return j;
        }


        /// <summary>
        /// 查询一条医嘱
        /// </summary>
        /// <param name="clinicCode">患者门诊流水号</param>
        /// <param name="id">医嘱流水号</param>
        /// <returns></returns>
        public FS.HISFC.Models.Order.OutPatient.Order GetOneOrder(string clinicCode, string id)
        {
            return outOrderManager.QueryOneOrder(clinicCode, id);
        }

        /// <summary>
        /// 作废门诊医嘱
        /// </summary>
        /// <param name="outPatientOrder"></param>
        /// <returns></returns>
        public int UpdateOrderBeCaceled(FS.HISFC.Models.Order.OutPatient.Order outPatientOrder)
        {
            this.SetDB(outOrderManager);
            return outOrderManager.UpdateOrderBeCaceled(outPatientOrder);
        }

        /// <summary>
        /// 按医嘱流水号查询医嘱信息-不分有效作废
        /// </summary>
        /// <param name="OrderNO"></param>
        /// <returns></returns>
        public FS.HISFC.Models.Order.Inpatient.Order QueryOneOrder(string OrderNO)
        {
            return orderManager.QueryOneOrder(OrderNO);
        }

        /// <summary>
        /// 按查询执行医嘱信息
        /// </summary>
        /// <param name="execOrderID"></param>
        /// <param name="itemType"></param>
        /// <returns>FS.HISFC.Models.Order.ExecOrder</returns>
        public FS.HISFC.Models.Order.ExecOrder QueryExecOrderByExecOrderID(string execOrderID, string itemType)
        {
            return orderManager.QueryExecOrderByExecOrderID(execOrderID, itemType);
        }

        /// <summary>
        /// 获取最新医嘱流水号
        /// </summary>
        /// <returns>成功 最新医嘱流水号 失败 null</returns>
        public string GetNewOrderID()
        {
            this.SetDB(orderManager);

            return orderManager.GetNewOrderID();
        }

        /// <summary>
        /// 根据执行科室查询需要确认项目的患者的所在科室
        /// </summary>
        /// <param name="deptID"></param>
        /// <returns></returns>
        public ArrayList QueryPatientDeptByConfirmDeptID(string deptID)
        {
            this.SetDB(orderManager);
            return orderManager.QueryPatientDeptByConfirmDeptID(deptID);
        }

        /// <summary>
        /// 根据住院流水号查询 一段时间内的医嘱执行情况
        /// </summary>
        /// <param name="inPatientNo"></param>
        /// <param name="Type">类别 1 药品 2非药品</param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public ArrayList QueryExecOrder(string inPatientNo, string type, DateTime begin, DateTime end)
        {
            this.SetDB(orderManager);
            return orderManager.QueryExecOrder(inPatientNo, type, begin, end);
        }

        /// <summary>
        /// 根据执行科室、患者所在科室查询需要确认项目的患者
        /// </summary>
        /// <param name="confirmDept"></param>
        /// <param name="patientDept"></param>
        /// <returns></returns>
        public ArrayList QueryPatientByConfirmDeptAndPatDept(string confirmDept, string patientDept)
        {
            this.SetDB(orderManager);
            return orderManager.QueryPatientByConfirmDeptAndPatDept(confirmDept, patientDept);
        }

        public ArrayList QueryExecOrderByDept(string inpatientNo, string itemType, bool isExec, string deptCode)
        {
            this.SetDB(orderManager);
            return orderManager.QueryExecOrderByDept(inpatientNo, itemType, isExec, deptCode);
        }

        /// <summary>
        /// 非正常作废执行档
        /// </summary>
        /// <param name="execOrderID">执行档流水号</param>
        /// <param name="isDrug">是否药品</param>
        /// <param name="dc">Neuobject.ID停止人，Neuobject.Name标记</param>
        /// <returns></returns>
        public int DcExecImmediateUnNormal(string execOrderID, bool isDrug, FS.FrameWork.Models.NeuObject dc)
        {
            this.SetDB(orderManager);
            return orderManager.DcExecImmediate(execOrderID, isDrug, dc);
        }

        /// <summary>
        /// 获取出院医嘱（包含“出院”两个字的临嘱）
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public int GetOutOrder(string inpatientNo, ref FS.HISFC.Models.Order.Inpatient.Order order)
        {
            this.SetDB(orderManager);
            return orderManager.GetOutOrder(inpatientNo, ref order);
        }

        /// <summary>
        /// 获取转科医嘱（包含类型）
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public int GetShiftOutOrderType(string inpatientNo, ref FS.HISFC.Models.Order.Inpatient.Order order)
        {
            this.SetDB(orderManager);
            return orderManager.GetShiftOutOrderType(inpatientNo, ref order);

        }
        /// <summary>
        /// 获取病人在病区所产生的数据
        /// add by yerl
        /// </summary>
        public int GetFeeInfoCount(string inpatientNo)
        {
            this.SetDB(orderManager);
            return orderManager.GetFeeInfoCount(inpatientNo);
        }
        /// <summary>
        /// 获取转科医嘱（包含“转科”两个字的临嘱）
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public int GetShiftOutOrder(string inpatientNo, ref FS.HISFC.Models.Order.Inpatient.Order order)
        {
            this.SetDB(orderManager);
            return orderManager.GetShiftOutOrder(inpatientNo, ref order);

        }

        /// <summary>
        /// 检查患者是否有开立过医嘱
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <param name="order"></param>
        /// <returns>-1 出错  0 没有医嘱  1 已开立过医嘱</returns>
        public int IsOwnOrders(string inpatientNo)
        {
            this.SetDB(this.orderManager);
            return this.orderManager.IsOwnOrders(inpatientNo);
        }

        /// <summary>
        /// 护士站出院登记，自动停止全部长嘱
        /// 停止后，为已停止、已审核状态
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <param name="dcDoct">停止医生</param>
        /// <param name="confirmNurse">审核护士</param>
        /// <param name="dcReasonCode">停止原因编码</param>
        /// <param name="dcReasonName">停止原因</param>
        /// <returns></returns>
        public int AutoDcOrder(string inpatientNo, FS.FrameWork.Models.NeuObject dcDoct, FS.FrameWork.Models.NeuObject confirmNurse, string dcReasonCode, string dcReasonName)
        {
            this.SetDB(orderManager);
            return orderManager.AutoDcOrder(inpatientNo, dcDoct, confirmNurse, dcReasonCode, dcReasonName);
        }

        #region {5197289A-AB55-410b-81EE-FC7C1B7CB5D7}
        /// <summary>
        /// 校验长期非药品医嘱执行档护士是否分解保存
        /// </summary>
        /// <param name="execOrderID">执行档流水号</param>
        /// <returns></returns>
        public bool CheckLongUndrugIsConfirm(string execOrderID)
        {
            this.SetDB(orderManager);
            return orderManager.CheckLongUndrugIsConfirm(execOrderID);
        }
        #endregion

        /// <summary>
        ///  按医嘱类型（长期/临时），状态）查询医嘱（不包含附材）
        /// </summary>
        /// <returns>成功 最新医嘱流水号 失败 null</returns>
        public ArrayList QueryOrder(string inpatientNO, FS.HISFC.Models.Order.EnumType type, int status)
        {
            this.SetDB(orderManager);

            return orderManager.QueryOrder(inpatientNO, type, status);
        }

        /// <summary>
        /// 根据住院流水号和医嘱类型查询所有医嘱
        /// </summary>
        /// <param name="inPatientNo"></param>
        /// <param name="type">类型 1药品 2非药品</param>
        /// <param name="strOrderID">所有医嘱流水号组成的字符串 用IN查询</param>
        /// <returns></returns>
        public ArrayList QueryOrder(string inPatientNo, string type, string strOrderID)
        {
            this.SetDB(orderManager);
            return orderManager.QueryOrder(inPatientNo, type, strOrderID);
        }

        #endregion

        /// <summary>
        /// 更新医嘱主档位已执行
        /// </summary>
        /// <param name="orderNo">医嘱流水号</param>
        /// <param name="status">医嘱状态</param>
        /// <returns></returns>
        public int UpdateOrderStatus(string orderNo, int status)
        {

            return orderManager.UpdateOrderStatus(orderNo, status);
        }

        /// <summary>
        /// 更新医嘱皮试结果//{26E88889-B2CF-4965-AFD8-6D9BE4519EBF}
        /// </summary>
        /// <param name="sequenceNO"></param>
        /// <returns></returns>
        public int UpdateOrderHyTest(string hyTestValue, string sequenceNO)
        {
            this.SetDB(outOrderManager);
            return outOrderManager.UpdateOrderHyTest(hyTestValue, sequenceNO);
        }

        /// <summary>
        /// 根据科室查询医疗组
        /// </summary>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        public List<FS.HISFC.Models.Order.Inpatient.MedicalTeamForDoct> QueryQueryMedicalTeamForDoctInfo(string deptCode)
        {
            return this.medicalTeamForDoctBizLogic.QueryQueryMedicalTeamForDoctInfo(deptCode);
        }

        /// <summary>
        /// 翻译皮试信息
        /// </summary>
        /// <param name="i"></param>
        /// <returns>1 [免试] 2 [需皮试] 3 [+] 4 [-]</returns>
        public string TransHypotest(FS.HISFC.Models.Order.EnumHypoTest HypotestCode)
        {
            this.SetDB(outOrderManager);
            return this.orderManager.TransHypotest(HypotestCode);
        }

        /// <summary>
        /// 按查询是否收费医嘱
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <param name="itemType"></param>
        /// <param name="isCharge"></param>
        /// <returns></returns>
        public ArrayList QueryExecOrderIsCharg(string inpatientNo, string itemType, bool isCharge)
        {
            this.SetDB(this.orderManager);
            return orderManager.QueryExecOrderIsCharg(inpatientNo, itemType, isCharge);
        }


        #region 医嘱审核



        #endregion
    }
}
