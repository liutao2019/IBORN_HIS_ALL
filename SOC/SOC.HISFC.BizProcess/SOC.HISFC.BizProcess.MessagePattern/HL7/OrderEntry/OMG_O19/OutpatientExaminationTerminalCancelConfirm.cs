using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.BizProcess.MessagePatternInterface;
using System.Collections;
using FS.HL7Message;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.HL7.OrderEntry.OMG_O19
{
    /// <summary>
    /// 门诊检查取消确认
    /// </summary>
    public class OutpatientExaminationTerminalCancelConfirm 
    {
        private FS.HISFC.BizLogic.Fee.Outpatient outFeeMgr = new FS.HISFC.BizLogic.Fee.Outpatient();
        private FS.HISFC.BizProcess.Integrate.Registration.Registration regIntegrate = new FS.HISFC.BizProcess.Integrate.Registration.Registration();

        public int ProcessMessage(NHapi.Model.V24.Message.OMG_O19 omgO19, ref NHapi.Base.Model.IMessage ackMessage, ref string errInfo)
        {
            string systemCode=omgO19.MSH.SendingApplication.NamespaceID.Value;

            string clinicCode = omgO19.PATIENT.PATIENT_VISIT.PV1.VisitNumber.ID.Value;
            if (string.IsNullOrEmpty(clinicCode))
            {
                errInfo= "患者流水号为空！";
                return -1;
            }
            FS.HISFC.Models.Registration.Register regInfo = new FS.HISFC.Models.Registration.Register();
            regInfo = regIntegrate.GetByClinic(clinicCode);
            if (regInfo == null || string.IsNullOrEmpty(regInfo.ID))
            {
                errInfo = "获取患者信息" + clinicCode + "失败!" + regIntegrate.Err;
                return -1;
            }
            if (regInfo.Status == FS.HISFC.Models.Base.EnumRegisterStatus.Back)
            {
                errInfo = "获取挂号信息失败，原因：挂号记录已退号" + regInfo.ID;
                return -1;
            }
            else if (regInfo.Status == FS.HISFC.Models.Base.EnumRegisterStatus.Cancel)
            {
                errInfo = "获取挂号信息失败，原因：挂号记录已作废" + regInfo.ID;
                return -1;
            }

            ArrayList alFeeCancelOrder = new ArrayList();
            NHapi.Model.V24.Group.OMG_O19_ORDER omgOrder = null;
            FS.HISFC.Models.Order.OutPatient.Order outOrder = null;

            for (int i = 0; i < omgO19.ORDERRepetitionsUsed; i++)
            {
                outOrder = new FS.HISFC.Models.Order.OutPatient.Order();

                //获取医嘱信息
                omgOrder = omgO19.GetORDER(i);
                if (omgOrder == null)
                {
                    errInfo= "获取消息内容失败!";
                    return -1;
                }

                //检查部位
                outOrder.CheckPartRecord = omgOrder.OBR.SpecimenActionCode.Value;
                outOrder.ApplyNo = omgOrder.ORC.PlacerOrderNumber.EntityIdentifier.Value;
                outOrder.DCOper.ID = omgOrder.ORC.GetOrderingProvider(0).IDNumber.Value;
                outOrder.DCOper.Dept.ID = omgOrder.ORC.EnteringOrganization.Identifier.Value;

                //如果是检查取消登记
                if (omgOrder.ORC.OrderControl.Value == "OC")
                {
                    //检查取消登记消息
                    alFeeCancelOrder.Add(outOrder);
                }
            }

            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            string applyCancelNOS = "";
            ItemCodeMapManager mapMgr = new ItemCodeMapManager();
            DateTime operDate = this.outFeeMgr.GetDateTimeFromSysDateTime();
            foreach (FS.HISFC.Models.Order.OutPatient.Order order in alFeeCancelOrder)
            {
                if (!applyCancelNOS.Contains(order.ApplyNo))
                {
                    string applyNO = order.ApplyNo;
                    string confirmOperCode = order.DCOper.ID;
                    string confirmDeptCode = order.DCOper.Dept.ID;
                    //根据申请单号和类型更新确认人、确认数量等
                    FS.FrameWork.Models.NeuObject obj = mapMgr.GetHISCode(EnumItemCodeMap.OutpatientOrder, new FS.FrameWork.Models.NeuObject(FS.HISFC.Models.Base.EnumSysClass.UC.ToString(), applyNO, ""), systemCode);
                    if (obj == null)
                    {
                        errInfo = "获取HL7对照信息失败，" + mapMgr.Err + "申请单号：" + applyNO;
                        return -1;
                    }
                    //根据医嘱流水号查询费用信息
                    ArrayList alFeeInfo = this.outFeeMgr.QueryFeeDetailFromMOOrder(obj.ID);
                    if (alFeeInfo == null)
                    {
                        errInfo = "获取费用信息失败，" + mapMgr.Err + "申请单号：" + applyNO;
                        return -1;
                    }
                    foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList itemList in alFeeInfo)
                    {
                        //if (itemList.NoBackQty <= 0)
                        //{
                        //    continue;
                        //}

                        //根据申请单号和类型更新确认人、确认数量等
                        int flag = this.outFeeMgr.UpdateConfirmFlag(itemList.RecipeNO, itemList.SequenceNO, "0", confirmOperCode, confirmDeptCode, operDate, itemList.Item.Qty - itemList.NoBackQty, 0);
                        if (flag == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            errInfo = "更新可退数量失败，" + outFeeMgr.Err;
                            return -1;
                        }
                        else if (flag == 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            errInfo = "费用可能已经退费或无效，申请单号：" + applyNO;
                            return -1;
                        }
                    }

                    //此处记录已更新的单号
                    applyCancelNOS = applyCancelNOS + "|" + applyNO;
                }

            }
            FS.FrameWork.Management.PublicTrans.Commit();

            return 1;
        }
    }
}
