using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.BizProcess.MessagePatternInterface;
using System.Collections;
using FS.SOC.HISFC.BizProcess.CommonInterface;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.HL7.OrderEntry.RAS_O17
{
    /// <summary>
    /// 住院接收医嘱信息
    /// </summary>
    public class InpatientOrderApply 
    {
        /// <summary>
        /// 住院费用管理层
        /// </summary>
        private FS.HISFC.BizLogic.Fee.InPatient inpatientManager = new FS.HISFC.BizLogic.Fee.InPatient();
        private FS.HISFC.BizLogic.RADT.InPatient radtInpatient = new FS.HISFC.BizLogic.RADT.InPatient();
        private FS.HISFC.BizLogic.Order.Order orderManager = new FS.HISFC.BizLogic.Order.Order();
        private FS.HISFC.BizProcess.Integrate.Order orderIntegrate = new FS.HISFC.BizProcess.Integrate.Order();
        private FS.HISFC.BizProcess.Integrate.Pharmacy phaIntegrate = new FS.HISFC.BizProcess.Integrate.Pharmacy();
        private FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();
        protected FS.HISFC.BizLogic.Fee.UndrugPackAge managerPack = new FS.HISFC.BizLogic.Fee.UndrugPackAge();
        private FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();

        /// <summary>
        /// 处理医嘱执行信息
        /// </summary>
        /// <param name="rasO17"></param>
        /// <returns></returns>
        private int processMessage(NHapi.Model.V24.Message.RAS_O17 rasO17, ref string errInfo)
        {
            string inpatientNO = rasO17.PATIENT.PATIENT_VISIT.PV1.VisitNumber.ID.Value;
            if (string.IsNullOrEmpty(inpatientNO))
            {
                errInfo = "住院流水号为空！";
                return -1;
            }

            //获取患者信息
            FS.HISFC.Models.RADT.PatientInfo patientInfo = this.radtInpatient.QueryPatientInfoByInpatientNO(inpatientNO);
            if (patientInfo == null || string.IsNullOrEmpty(patientInfo.ID))
            {
                errInfo = "查找住院病人信息失败，流水号：" + inpatientNO;
                return -1;
            }

            ArrayList alFeeInfo = new ArrayList();
            ArrayList alQuitFeeInfo = new ArrayList();


            //保存执行单流水号用
            Dictionary<string, string> dictionExceNO = new Dictionary<string, string>();

            DateTime dtNow = this.radtInpatient.GetDateTimeFromSysDateTime();
            Dictionary<string, string> dicComboID = new Dictionary<string, string>();
            //循环取医嘱
            for (int i = 0; i < rasO17.ORDERRepetitionsUsed; i++)
            {
                NHapi.Model.V24.Group.RAS_O17_ORDER rasO17Order = rasO17.GetORDER(i);
                FS.HISFC.Models.Order.ExecOrder execOrder = new FS.HISFC.Models.Order.ExecOrder();
                execOrder.Order.Patient = patientInfo;
                #region 获取CIS内容

                //申请单号
                execOrder.Order.ApplyNo = rasO17Order.ORC.FillerOrderNumber.EntityIdentifier.Value;

                //收费序列号
                //execOrder.Order.SequenceNO = FS.FrameWork.Function.NConvert.ToInt32(rasO17Order.ORC.PlacerOrderNumber.EntityIdentifier.Value);

                //医嘱流水号
                execOrder.Order.ID = rasO17Order.ORC.PlacerGroupNumber.EntityIdentifier.Value;
                //医嘱组合号
                string parentID = rasO17Order.ORC.Parent.ParentSPlacerOrderNumber.EntityIdentifier.Value;
                if (string.IsNullOrEmpty(parentID))
                {
                    parentID = rasO17Order.ORC.PlacerOrderNumber.EntityIdentifier.Value;
                }
                //如果为空
                if (dicComboID.ContainsKey(parentID) == false)
                {
                    string combNo = this.orderManager.GetNewOrderComboID();
                    execOrder.Order.Combo.ID = combNo;
                    dicComboID.Add(parentID, combNo);
                }
                else
                {
                    execOrder.Order.Combo.ID = dicComboID[parentID];
                }
                //医嘱状态
                //execOrder.Order.Status = rasO17Order.ORC.OrderStatus.Value.Equals("1") ? 0 : rasO17Order.ORC.OrderStatus.Value.Equals("2") ? 1 : rasO17Order.ORC.OrderStatus.Value.Equals("3") ? 2 : rasO17Order.ORC.OrderStatus.Value.Equals("4") ? 2 : 3;
                //医嘱项目编码（医嘱术语）
                //execOrder.Order.ID = rasO17Order.ORDER_DETAIL.RXO.RequestedGiveCode.Identifier.Value;
                //收费编码
                execOrder.Order.Item.ID = rasO17Order.ORDER_DETAIL.RXO.RequestedGiveCode.Text.Value;
                //TQ数量、单位、频次
                NHapi.Model.V24.Datatype.TQ doseTQ = rasO17Order.ORC.GetQuantityTiming(0);

                //开方科室
                execOrder.Order.ReciptDept.ID = rasO17Order.ORC.EnteringOrganization.Identifier.Value;
                execOrder.Order.ReciptDept.Name = CommonController.CreateInstance().GetDepartmentName(execOrder.Order.DoctorDept.ID);


                //收费项目
                string itemCode = execOrder.Order.Item.ID;
                string itemType = rasO17Order.ORDER_DETAIL.RXO.RequestedGiveCode.NameOfCodingSystem.Value;
                if (itemType.Equals("d"))
                {
                    #region 药品
                    //收费处方号（唯一）
                    execOrder.Order.ReciptNO = this.inpatientManager.GetDrugRecipeNO();
                    
                    //药品的申请单号
                    execOrder.Order.ApplyNo = rasO17Order.ORDER_DETAIL.RXO.GetSupplementaryCode(0).Text.Value;

                    FS.HISFC.Models.Pharmacy.Item item = phaIntegrate.GetItem(itemCode);
                    if (item == null)
                    {
                        errInfo = "接收医嘱执行信息失败，原因：传入的项目编码，系统中找不到" + itemCode;
                        return -1;
                    }
                    execOrder.Order.Item = item;
                    execOrder.Order.Item.Price = item.Price;
                    execOrder.Order.Item.ItemType = FS.HISFC.Models.Base.EnumItemType.Drug;
                    if (execOrder.Order.Item.SysClass.ID.ToString() == FS.HISFC.Models.Base.EnumSysClass.PCC.ToString())
                    {
                        //付数
                        execOrder.Order.HerbalQty = FS.FrameWork.Function.NConvert.ToDecimal(rasO17Order.ORDER_DETAIL.RXO.NumberOfRefills.Value);
                    }
                    else
                    {
                        //天数
                        execOrder.Order.HerbalQty = FS.FrameWork.Function.NConvert.ToDecimal(doseTQ.OccurrenceDuration.Identifier.Value);
                    }
                    //默认为1
                    if (execOrder.Order.HerbalQty == 0)
                    {
                        execOrder.Order.HerbalQty = 1;
                    }

                    //每次剂量
                    execOrder.Order.DoseOnce = FS.FrameWork.Function.NConvert.ToDecimal(doseTQ.Quantity.Quantity.Value);
                    //每次剂量单位
                    execOrder.Order.DoseUnit = doseTQ.Quantity.Units.Identifier.Value;  //2012-12-12 nieaj text 改成  Identifier
                    //总量单位
                    execOrder.Order.Unit = rasO17Order.ORDER_DETAIL.RXO.RequestedGiveUnits.Identifier.Value;
                    if (string.IsNullOrEmpty(execOrder.Order.Unit))
                    {
                        errInfo = "接收医嘱执行信息失败，原因：单位不能为空" + execOrder.Order.Item.ID;
                        return -1;
                    }
                    //药房编码
                    string stcokDept = rasO17Order.ORDER_DETAIL.RXO.DeliverToLocation.Facility.UniversalID.Value;
                    if (string.IsNullOrEmpty(stcokDept) || CommonController.CreateInstance().GetDepartment(stcokDept) == null)
                    {
                        string strErr = "";
                        FS.FrameWork.Models.NeuObject stockOjb = itemManager.GetStockDeptByDeptCode(execOrder.Order.ReciptDept.ID, execOrder.Order.Item.ItemType.ToString(), execOrder.Order.Item.ID, execOrder.Order.Qty, null, ref strErr);
                        if (stockOjb != null)
                        {
                            execOrder.Order.StockDept.ID = stockOjb.ID;
                            execOrder.Order.StockDept.Name = stockOjb.Name;
                        }
                        else
                        {
                            errInfo = "接收医嘱执行信息失败，原因：传入的库存编码，系统中找不到" + stcokDept;
                            return -1;
                        }
                    }
                    else
                    {
                        execOrder.Order.StockDept = CommonController.CreateInstance().GetDepartment(stcokDept);
                    }

                    #endregion
                }
                else if (itemType.Equals("n"))
                {
                    #region 非药品
                    //收费处方号（唯一）
                    execOrder.Order.ReciptNO = this.inpatientManager.GetUndrugRecipeNO();

                    execOrder.Order.Item.ItemType = FS.HISFC.Models.Base.EnumItemType.UnDrug;
                    execOrder.Order.Item = feeIntegrate.GetItem(itemCode);
                    if (execOrder.Order.Item == null)
                    {
                        errInfo = "接收医嘱执行信息失败，原因：传入的项目编码，系统中找不到" + itemCode;
                        return -1;
                    }

                    //天数
                    execOrder.Order.HerbalQty = FS.FrameWork.Function.NConvert.ToDecimal(doseTQ.OccurrenceDuration.Identifier.Value);
                    execOrder.Order.Unit = execOrder.Order.Item.PriceUnit;

                    string packageCode= rasO17Order.ORDER_DETAIL.RXO.RequestedGiveCode.Identifier.Value;
                    if (itemCode.Equals(packageCode) == false)
                    {
                        execOrder.Order.Package = CommonController.Instance.GetItem(packageCode);
                        if (execOrder.Order.Package == null)
                        {
                            errInfo = "接收医嘱执行信息失败，原因：传入的项目组套编码，系统中找不到" + packageCode;
                            return -1;
                        }
                    }

                    #endregion
                }
                else
                {
                    errInfo = "接收医嘱执行信息失败，原因：未知的项目类型" + itemType;
                    return -1;
                }

                //加急
                execOrder.Order.IsEmergency = (doseTQ.Priority.Value == "S" || doseTQ.Priority.Value == "A");
                //频次
                string frequencyID = doseTQ.Interval.ExplicitTimeInterval.Value;
                if (string.IsNullOrEmpty(frequencyID) == false)
                {
                    //频次名称
                    execOrder.Order.Frequency = CommonController.CreateInstance().GetFrequency(frequencyID);
                    if (execOrder.Order.Frequency == null)
                    {
                        errInfo = "接收医嘱执行信息失败，原因：传入的频次编码，系统中找不到" + frequencyID;
                        return -1;
                    }
                }

                //次数
                //开方医生
                NHapi.Model.V24.Datatype.XCN orderingProvider = rasO17Order.ORC.GetOrderingProvider(0);
                execOrder.Order.Doctor.ID = orderingProvider.IDNumber.Value;
                execOrder.Order.Doctor.Name = CommonController.CreateInstance().GetEmployeeName(execOrder.Order.Doctor.ID);
                if (CommonController.CreateInstance().GetEmployee(execOrder.Order.Doctor.ID) != null)
                {
                    //开立医生所在科室
                    execOrder.Order.DoctorDept = CommonController.CreateInstance().GetEmployee(execOrder.Order.Doctor.ID).Dept;
                }

                //医嘱停止原因
                execOrder.Order.DcReason.ID = rasO17Order.ORC.OrderControlCodeReason.Identifier.Value;
                execOrder.Order.DcReason.Name = rasO17Order.ORC.OrderControlCodeReason.Text.Value;
                //开方医生
                execOrder.Order.ReciptDoctor.ID = execOrder.Order.Doctor.ID;
                execOrder.Order.ReciptDoctor.Name = execOrder.Order.Doctor.Name;
                
                //医嘱描述
                execOrder.Order.Memo = rasO17Order.ORDER_DETAIL.RXO.GetProviderSPharmacyTreatmentInstructions(0).Text.Value;
                //用法-草药用法
                execOrder.Order.Usage.ID = rasO17Order.RXR.Route.Identifier.Value;
                //用法名称
                execOrder.Order.Usage.Name = CommonController.CreateInstance().GetConstantName(FS.HISFC.Models.Base.EnumConstant.USAGE, execOrder.Order.Usage.ID);

                //医嘱开始时间
                NHapi.Model.V24.Segment.RXA rxa = rasO17Order.GetRXA(0);
                if (string.IsNullOrEmpty(rxa.DateTimeStartOfAdministration.TimeOfAnEvent.Value))
                {
                    errInfo = "接收医嘱执行信息失败，原因：开始时间为空";
                    return -1;
                }
                execOrder.Order.BeginTime = DateTime.ParseExact(rxa.DateTimeStartOfAdministration.TimeOfAnEvent.Value, "yyyyMMddHHmmss", null);
                //医嘱结束时间
                //execOrder.Order.EndTime = DateTime.ParseExact(rxa.DateTimeEndOfAdministration.TimeOfAnEvent.Value, "yyyyMMddHHmmss", null);
                //数量
                execOrder.Order.Qty = FS.FrameWork.Function.NConvert.ToDecimal(rasO17Order.ORDER_DETAIL.RXO.RequestedGiveAmountMinimum.Value);
                if (execOrder.Order.Qty == 0)
                {
                    errInfo = "接收医嘱执行信息失败，原因：数量不能为空" + execOrder.Order.Item.ID;
                    return -1;
                }
                //如果是草药 则总量为数量*付数
                if (execOrder.Order.Item.SysClass.ID.ToString() == FS.HISFC.Models.Base.EnumSysClass.PCC.ToString())
                {
                    execOrder.Order.Qty = execOrder.Order.Qty * execOrder.Order.HerbalQty;
                }
                
                //药品转换成最小单位数量
                if (execOrder.Order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                {
                    //药品存最小单位
                    if (execOrder.Order.Unit != ((FS.HISFC.Models.Pharmacy.Item)execOrder.Order.Item).MinUnit)//最小单位
                    {
                        execOrder.Order.Qty = execOrder.Order.Qty * execOrder.Order.Item.PackQty;//变成最小单位
                        execOrder.Order.Unit = ((FS.HISFC.Models.Pharmacy.Item)execOrder.Order.Item).MinUnit;
                    }
                }
                if (string.IsNullOrEmpty(rxa.SystemEntryDateTime.TimeOfAnEvent.Value))
                {
                    errInfo = "接收医嘱执行信息失败，原因：执行时间为空";
                    return -1;
                }
                //执行时间点
                execOrder.DateUse = DateTime.ParseExact(rxa.SystemEntryDateTime.TimeOfAnEvent.Value, "yyyyMMddHHmmss", null);
                //分解时间
                execOrder.DateDeco = dtNow;

                if (execOrder.Order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                {
                    //执行科室-药品默认为当前科室
                    execOrder.Order.ExeDept = patientInfo.PVisit.PatientLocation.Dept;
                }
                else
                {
                    //执行科室
                    execOrder.Order.ExeDept.ID = rxa.AdministeredAtLocation.PointOfCare.Value;
                    execOrder.Order.ExeDept.Name = CommonController.CreateInstance().GetDepartmentName(execOrder.Order.ExeDept.ID);
                }

                //作废原因
                execOrder.Order.DcReason.ID = rxa.GetSubstanceTreatmentRefusalReason(rxa.SubstanceTreatmentRefusalReasonRepetitionsUsed).Identifier.Value;
                execOrder.Order.DcReason.Name = rxa.GetSubstanceTreatmentRefusalReason(rxa.SubstanceTreatmentRefusalReasonRepetitionsUsed).Text.Value;

                //执行档流水号用 医嘱号+项目编号+执行时间点
                execOrder.ID = rasO17Order.ORC.PlacerOrderNumber.EntityIdentifier.Value;
                // execOrder.Order.ID + execOrder.Order.Item.ID + execOrder.DateUse.ToString("yyyyMMddHHmmss");


                //是否收费
                bool isFee = rxa.CompletionStatus.Value.Equals("2") ? true : false;
                //医嘱类型
                string orderType = rxa.AdministeredCode.Identifier.Value;
                if (string.IsNullOrEmpty(orderType))
                {
                    errInfo = "接收医嘱执行信息失败，原因：医嘱类型不能为空" + execOrder.Order.Item.ID;
                    return -1;
                }
                execOrder.Order.OrderType.IsDecompose = FS.FrameWork.Function.NConvert.ToBoolean(orderType);
                //医嘱标记 0、正常 1、自备 2、嘱托 3、基数药 4、分药 5、计费
                int orderFlag = FS.FrameWork.Function.NConvert.ToInt32(rxa.AdministeredCode.NameOfCodingSystem.Value);
                execOrder.Order.OrderType = this.GetOrdeType(execOrder.Order.OrderType.IsDecompose, orderFlag);
                if (execOrder.Order.OrderType.IsCharge == false)
                {
                    isFee = false;
                }

                //记录基数药标记
                execOrder.Order.IsStock = orderFlag == 3 ? true : false;
                execOrder.DrugFlag = 0; //默认为不需要发送

                //可能先不收费，之后再收费
                execOrder.IsCharge = isFee;
                if (execOrder.IsCharge)
                {
                    //以下为HIS补充内容
                    execOrder.IsExec = true;//已执行
                }
                //执行护士
                NHapi.Model.V24.Datatype.XCN administeringProvider = rxa.GetAdministeringProvider(0);
                //先写死
                execOrder.ExecOper.ID = administeringProvider.IDNumber.Value;
                if (CommonController.CreateInstance().GetEmployee(execOrder.ExecOper.ID) != null)
                {
                    execOrder.ExecOper.Name = CommonController.CreateInstance().GetEmployeeName(execOrder.ExecOper.ID);
                }
                execOrder.ExecOper.Dept = execOrder.Order.ExeDept;
                //计费人--用执行护士
                execOrder.ChargeOper.ID = execOrder.ExecOper.ID;
                execOrder.ChargeOper.Name = execOrder.ExecOper.Name;
                //计费科室默认为执行科室
                execOrder.ChargeOper.Dept = execOrder.Order.ExeDept;
                //计费时间
                execOrder.ChargeOper.OperTime = dtNow;

                //先写死
                if (rxa.ActionCodeRXA.Value.Equals("A"))//说明是新加的医嘱
                {
                    if (execOrder.Order.Qty < 0)
                    {
                        execOrder.ID = rasO17Order.ORC.AdvancedBeneficiaryNoticeCode.AlternateIdentifier.Value; //停止医嘱退药的时候记录对应所退项目的原费用ID（CIS特殊处理）
                        execOrder.Memo = rasO17Order.ORC.PlacerOrderNumber.EntityIdentifier.Value; //记录CIS新的执行流水号
                        execOrder.IsValid = false;
                        execOrder.IsCharge = true;//直接退费
                        //目前处理不了，无法关联出库申请记录
                        //作废的医嘱
                        alQuitFeeInfo.Add(execOrder.Clone());
                    }
                    else
                    {
                        execOrder.IsValid = true;
                        alFeeInfo.Add(execOrder);
                    }
                }
                else if (rxa.ActionCodeRXA.Value.Equals("D"))
                {
                    execOrder.IsValid = false;
                    //execOrder.ID = rasO17Order.ORC.AdvancedBeneficiaryNoticeCode.AlternateIdentifier.Value; //停止医嘱退药的时候记录对应所退项目的原费用ID（CIS特殊处理）
                    //execOrder.Memo = rasO17Order.ORC.PlacerOrderNumber.EntityIdentifier.Value; //记录CIS新的执行流水号
                    //作废的医嘱
                    alQuitFeeInfo.Add(execOrder);
                }
                else
                {
                    errInfo = "未知的执行状态：" + rxa.ActionCodeRXA.Value;
                    return -1;
                }

                #endregion
            }

            return this.ProcessFeeOrder(patientInfo, alFeeInfo, alQuitFeeInfo,rasO17.MSH.SendingApplication.NamespaceID.Value, ref errInfo);
        }

        /// <summary>
        /// 处理费用医嘱和执行医嘱信息
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="alFeeOrder"></param>
        /// <param name="alExecOrder"></param>
        /// <returns></returns>
        private int ProcessFeeOrder(FS.HISFC.Models.RADT.PatientInfo patientInfo, ArrayList alExecOrder, ArrayList alQuitFee,string systemCode ,ref string errInfo)
        {

            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.orderManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            DateTime dtNow = orderManager.GetDateTimeFromSysDateTime();
            ItemCodeMapManager mapMgr = new ItemCodeMapManager();
            mapMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            //需要发药的执行档信息
            List<FS.HISFC.Models.Order.ExecOrder> alSendDrug = new List<FS.HISFC.Models.Order.ExecOrder>();
            ArrayList alDrugFlag = new ArrayList();
            ArrayList alDrugReturn = new ArrayList();
            //需收费的医嘱
            ArrayList alFeeOrder = new ArrayList();
            FS.HISFC.Models.Fee.Inpatient.FTSource ftSource = new FS.HISFC.Models.Fee.Inpatient.FTSource("100");

            #region 处理医嘱执行档
            //先插入执行档
            int sequenceNO = 0;
            foreach (FS.HISFC.Models.Order.ExecOrder execOrder in alExecOrder)
            {
                execOrder.Order.Patient = patientInfo;

                //取执行档流水号
                string cisExecOrderID = execOrder.ID;
                string cisExecOrderName = execOrder.Memo;//原来
                execOrder.ID = this.orderManager.GetNewOrderExecID();
                if (string.IsNullOrEmpty(execOrder.ID))
                {
                    FS.FrameWork.Management.PublicTrans.RollBack(); ;
                    errInfo = "获取执行档流水号失败！" + orderManager.Err;
                    return -1;
                }
                //if (execOrder.Memo == "" || execOrder.Memo == null) //CIS 原来执行单流水为空
                //{
                //    execOrder.Memo = execOrder.ID; //
                //}
                // execOrder.ID 新的存执行档流水号   退药时，HL7_name记录 原来执行档流水号(execOrder.Memo)
                if (mapMgr.Insert(EnumItemCodeMap.InpatientOrder, new FS.FrameWork.Models.NeuObject(execOrder.ID, execOrder.ID, ""), new FS.FrameWork.Models.NeuObject(cisExecOrderID, cisExecOrderID, ""),systemCode) < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack(); ;
                    errInfo = "插入HL7对照表失败！" + mapMgr.Err;
                    return -1;
                }

                if (this.orderManager.InsertExecOrder(execOrder) <= 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    errInfo= "插入医嘱执行档失败，原因：" + this.orderManager.Err;
                    return -1;
                }

                //记录发药的信息
                if (execOrder.Order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                {
                    #region 药品
                    //是否发药
                    //基数药的不进行发药
                    if (!execOrder.Order.IsStock)
                    {
                        execOrder.DrugFlag = 2;
                    }

                    if (execOrder.Order.Qty > 0)
                    {
                        alDrugFlag.Add(execOrder);
                        alSendDrug.Add(execOrder);
                    }
                    if (execOrder.IsCharge)
                    {
                        FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList = Function.ChangeToFeeItemList(patientInfo, execOrder, ftSource, ref errInfo);
                        alFeeOrder.Add(feeItemList);
                    }

                    sequenceNO++;

                    #endregion
                }
                else if (execOrder.Order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.UnDrug)
                {
                    #region 非药品

                    if (execOrder.IsCharge)
                    {
                        if (((FS.HISFC.Models.Fee.Item.Undrug)execOrder.Order.Item).UnitFlag == "1")
                        {
                            #region 如果是复合项目－变成细项
                            /*待添加*/
                            ArrayList al = managerPack.QueryUndrugPackagesBypackageCode(execOrder.Order.Item.ID);
                            if (al == null)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                errInfo = "非药品获取复合项目明细失败，原因：" + managerPack.Err;
                                return -1;
                            }

                         
                            foreach (FS.HISFC.Models.Fee.Item.Undrug undrug in al)
                            {
                                FS.HISFC.Models.Order.ExecOrder myorder = null;
                                decimal qty = execOrder.Order.Qty;
                                myorder = execOrder.Clone();

                                //使用原始处方号，新的处方内流水号
                                myorder.Order.ReciptNO = execOrder.Order.ReciptNO;
                                sequenceNO++;
                                myorder.Order.SequenceNO = sequenceNO;
                                //拆分成明细后记录处方内流水号
                                myorder.Order.Package.Memo = execOrder.Order.SequenceNO.ToString();

                                myorder.Name = undrug.Name;
                                //>>{A405B97A-5027-44b0-81C3-2B1BBF55DF91}重新获取
                                FS.HISFC.Models.Fee.Item.Undrug undrugInfo = feeIntegrate.GetItem(undrug.ID);
                                myorder.Order.Item = undrugInfo;
                                //myorder.Order.Item = undrug.Clone();
                                //<<
                                myorder.Order.Qty = qty * undrug.Qty;//数量==复合项目数量*小项目数量
                                myorder.Order.Item.Qty = qty * undrug.Qty;//数量==复合项目数量*小项目数量

                                myorder.Order.Package.ID = execOrder.Order.Item.ID;
                                myorder.Order.Package.Name = execOrder.Order.Item.Name;
                                //复合项目在费用表没有记录执行流水号 add by houwb 2011-4-7
                                myorder.User03 = execOrder.ID;

                                //添加到收费项目里面
                                myorder.Order.Oper.OperTime = orderManager.GetDateTimeFromSysDateTime();
                                if (myorder.Order.Item.Price > 0)
                                {
                                    alFeeOrder.Add(Function.ChangeToFeeItemList(patientInfo, myorder, ftSource,ref errInfo));
                                }
                            }

                            #endregion
                        }
                        else
                        {
                            #region 收费

                            if (execOrder.Order.ExeDept.ID == "")//执行科室默认
                                execOrder.Order.ExeDept = patientInfo.PVisit.PatientLocation.Dept.Clone();//order.NurseStation;
                            execOrder.Order.User03 = execOrder.ID;//execOrderID
                            //添加到收费项目里面
                            execOrder.Order.Oper.OperTime = orderManager.GetDateTimeFromSysDateTime();
                            alFeeOrder.Add(Function.ChangeToFeeItemList(patientInfo, execOrder, ftSource,ref errInfo));

                            #endregion
                        }
                    }

                    #endregion
                }
            }
            #endregion

            #region 处理收费

            if (alFeeOrder.Count > 0) //需要收费
            {
                if (this.feeIntegrate.FeeItem(patientInfo, ref alFeeOrder) == -1)
                {
                    this.feeIntegrate.Rollback();
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    errInfo = "计费失败，原因：" + this.feeIntegrate.Err;
                    return -1;
                }
            }

            #endregion

            #region 处理发药
            if (alDrugFlag.Count != 0)
            {
                if (this.orderIntegrate.SendToDrugStore(alDrugFlag, dtNow) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    errInfo = "处理摆药申请失败，原因：" + this.orderIntegrate.Err;
                    return -1;
                }
            }
            foreach (FS.HISFC.Models.Order.ExecOrder exeOrder in alDrugFlag)
            {
                //置执行发药标记
                if (orderManager.SetDrugFlag(exeOrder.ID, exeOrder.DrugFlag) == -1)
                {
                    this.feeIntegrate.Rollback();
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    errInfo = "处理发药标记失败，原因：" + orderManager.Err;
                    return -1;
                }
            }

            if (alSendDrug.Count > 0)
            {
                if (this.phaIntegrate.InpatientDrugPreOutNum(alSendDrug, dtNow, false) == -1)
                {
                    this.feeIntegrate.Rollback();
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    errInfo = "处理预扣库存失败，原因：" + this.orderIntegrate.Err;
                    return -1;
                }
            }

            #endregion

            #region 处理退费
            //先找到退费的费用原始记录
            foreach (FS.HISFC.Models.Order.ExecOrder execOrder in alQuitFee)
            {
                //根据流水号查找对应的HIS执行档流水号
                //存执行档流水号
                FS.FrameWork.Models.NeuObject obj = mapMgr.GetHISCode(EnumItemCodeMap.InpatientOrder, new FS.FrameWork.Models.NeuObject(execOrder.ID, execOrder.ID, ""),systemCode);
                if (obj.ID == null||obj.ID =="") 
                {
                    this.feeIntegrate.Rollback();
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    errInfo = "查找CIS执行档流水号为：" + execOrder.ID + "的HIS执行档流水号失败，" + mapMgr.Err;
                    return -1;
                }

                if (string.IsNullOrEmpty(execOrder.Memo) == false) //CIS 原来执行单流水为空
                {
                    string cisExecOrderID = execOrder.ID;
                    string cisExecOrderName = execOrder.Memo;

                    // execOrder.ID 新的存执行档流水号   退药时，HL7_name记录 原来执行档流水号(execOrder.Memo)
                    if (mapMgr.Insert(EnumItemCodeMap.InpatientOrderQuitFee, new FS.FrameWork.Models.NeuObject(obj.ID, obj.ID, ""), new FS.FrameWork.Models.NeuObject(cisExecOrderID, cisExecOrderName, ""),systemCode) < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack(); ;
                        errInfo = "插入HL7对照表失败！" + mapMgr.Err;
                        return -1;
                    }
                }

                execOrder.ID = obj.ID;
                execOrder.Memo = obj.Name;

                //已收费、查找相应的费用进行退费 
                if (execOrder.IsCharge)
                {
                    FS.HISFC.Models.Order.ExecOrder oldExcOrder = this.orderManager.QueryExecOrderByExecOrderID(execOrder.ID, execOrder.Order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug ? "1" : "2");
                    if (oldExcOrder == null)
                    {
                        this.feeIntegrate.Rollback();
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        errInfo = "查找响应的执行档信息失败，原因：" + this.orderManager.Err;
                        return -1;
                    }

                    //查找相应的费用信息
                    ArrayList al = this.inpatientManager.GetItemListByExecSQN(patientInfo.ID, execOrder.ID, execOrder.Order.Item.ItemType);
                    if (al == null)
                    {
                        this.feeIntegrate.Rollback(); 
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        errInfo = "查找响应的费用信息失败，原因：" + this.inpatientManager.Err + "医嘱号：" + execOrder.Order.ID + "执行档流水号：" + execOrder.ID;
                        return -1;
                    }

                    //处理费用信息
                    foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList in al)
                    {
                        if (feeItemList.NoBackQty<= 0)//则过滤
                        {
                            continue;
                        }
                        //传过来就是负记录
                        feeItemList.Item.Qty = Math.Abs(execOrder.Order.Item.Qty);
                        feeItemList.NoBackQty = feeItemList.NoBackQty - feeItemList.Item.Qty;
                        if (feeItemList.Item.PackQty == 0)
                        {
                            feeItemList.Item.PackQty = 1;
                        }
                        feeItemList.FT.TotCost = feeItemList.Item.Price * feeItemList.Item.Qty / feeItemList.Item.PackQty;
                        feeItemList.FT.OwnCost = feeItemList.FT.TotCost;
                        feeItemList.IsNeedUpdateNoBackQty = true;

                        feeItemList.ExecOrder.DateUse = execOrder.DateUse;//用药时间

                        feeItemList.Order.Usage.ID = execOrder.Order.Usage.ID;//用法编码
                        feeItemList.Order.Usage.Name = execOrder.Order.Usage.Name;//用法名称
                        feeItemList.Order.Frequency.ID = execOrder.Order.Frequency.ID;//频次代码
                        feeItemList.Order.Frequency.Name = execOrder.Order.Frequency.Name;//频次名称
                        feeItemList.Order.DoseOnce = execOrder.Order.DoseOnce;
                        feeItemList.Order.DoseUnit = execOrder.Order.DoseUnit; 
                        //记录
                        string msg = "";
                        if (this.feeIntegrate.QuitFeeApply(patientInfo, feeItemList, false, execOrder.Order.ApplyNo, dtNow, ref msg) == -1)
                        {
                            this.feeIntegrate.Rollback();
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            errInfo = "退费失败，原因：" + msg + this.feeIntegrate.Err;
                            return -1;
                        }
                    }
                }
                else
                {
                    //未收费 直接作废执行档
                    int i = this.orderManager.DcExecImmediate(execOrder, execOrder.Order.DcReason);
                    if (i == -1)
                    {
                        this.feeIntegrate.Rollback();
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        errInfo = "作废执行档失败，原因：" + this.orderManager.Err;
                        return -1;
                    }
                    else if (i == 0)
                    {
                        this.feeIntegrate.Rollback();
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        errInfo = "作废执行档失败，原因：已收费，不允许作废执行档";
                        return -1;
                    }

                    //作废摆药申请档
                    if (this.phaIntegrate.CancelApplyOut(execOrder.ID) == -1)
                    {
                        this.feeIntegrate.Rollback();
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        errInfo = "作废摆药申请失败，原因：" + this.phaIntegrate.Err;
                        return -1;
                    }
                }
            }
            #endregion

            FS.FrameWork.Management.PublicTrans.Commit();

            return 1;
        }

        /// <summary>
        /// 根据长/临嘱 医嘱标记获取医嘱类型
        /// </summary>
        /// <param name="IsDecompose"></param>
        /// <param name="orderFlag"></param>
        /// <returns></returns>
        private FS.HISFC.Models.Order.OrderType GetOrdeType(bool IsDecompose, int orderFlag)
        {
            FS.HISFC.Models.Order.OrderType orderType = new FS.HISFC.Models.Order.OrderType();
            orderType.IsDecompose = IsDecompose;
            //获取医嘱类型
            //医嘱标记 0、正常 1、自备 2、嘱托 3、基数药 4、分药 5、计费

            switch (orderFlag)
            {
                //嘱托
                case 2:
                    orderType.ID = IsDecompose ? "ZC" : "ZL";
                    orderType.Name = IsDecompose ? "嘱托长嘱" : "嘱托临嘱";
                    orderType.IsNeedPharmacy = false;
                    orderType.IsConfirm = true;
                    orderType.IsCharge = false;
                    break;
                case 3:
                case 5:
                //自备
                case 1:
                //正常
                case 0:
                default:
                    //长期医嘱
                    orderType.ID = IsDecompose ? "CZ" : "LZ";
                    orderType.Name = IsDecompose ? "长期医嘱" : "临时医嘱";
                    orderType.IsNeedPharmacy = orderFlag == 1 ? false : true;
                    orderType.IsConfirm = orderFlag == 1 ? false : true;
                    orderType.IsCharge = orderFlag == 1 ? false : true;
                    break;
            }

            return orderType;
        }

        #region AbstractAcceptMessage 成员

        public  int ProcessMessage(NHapi.Model.V24.Message.RAS_O17 o, ref NHapi.Base.Model.IMessage ackMessage, ref string errInfo)
        {
            NHapi.Model.V24.Message.RRA_O18 ack = new NHapi.Model.V24.Message.RRA_O18();
            FS.HL7Message.V24.Function.GenerateMSH(ack.MSH, o.MSH);

            if (processMessage(o,ref errInfo) <= 0)
            {
                FS.HL7Message.V24.Function.GenerateErrorMSA(o.MSH, ack.MSA, errInfo);
                ackMessage = ack;
                return -1;
            }
            else
            {
                FS.HL7Message.V24.Function.GenerateSuccessMSA(o.MSH, ack.MSA);
                ackMessage = ack;
                return 1;
            }

        }

        #endregion
    }
}
