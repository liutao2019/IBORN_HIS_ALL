using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.BizProcess.MessagePatternInterface;
using FS.HL7Message;
using System.Collections;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.HL7.OrderEntry.ORG_O20
{
    public class OutpatientExaminationTerminalConfirm
    {
        private FS.HISFC.BizLogic.Fee.Outpatient outFeeMgr = new FS.HISFC.BizLogic.Fee.Outpatient();
        FS.HISFC.BizProcess.Integrate.Registration.Registration regIntegrate = new FS.HISFC.BizProcess.Integrate.Registration.Registration();
        OutPatientBalanceFee outPatientBalanceFee = new OutPatientBalanceFee();

        public int ProcessMessage(NHapi.Model.V24.Message.ORG_O20 orgO20, ref NHapi.Base.Model.IMessage ackMessage, ref string errInfo)
        {
            string systemCode = orgO20.MSH.SendingApplication.NamespaceID.Value;
            string clinicCode = orgO20.RESPONSE.GetORDER(0).ORC.PlacerOrderNumber.UniversalID.Value;
            if (string.IsNullOrEmpty(clinicCode))
            {
                errInfo= "患者流水号为空！";
                return -1;
            }

            FS.HISFC.Models.Registration.Register regInfo  = regIntegrate.GetByClinic(clinicCode);
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

            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            string applyNOS = "";
            ItemCodeMapManager mapMgr = new ItemCodeMapManager();
            DateTime operDate = this.outFeeMgr.GetDateTimeFromSysDateTime();
            for (int i = 0; i < orgO20.RESPONSE.ORDERRepetitionsUsed; i++)
            {
                NHapi.Model.V24.Group.ORG_O20_ORDER orgO20Order = orgO20.RESPONSE.GetORDER(i);
                if (orgO20Order.ORC.OrderControl.Value.Equals("OR"))//检查登记
                {
                    if (!applyNOS.Contains(orgO20Order.ORC.PlacerOrderNumber.EntityIdentifier.Value))
                    {
                        string applyNO = orgO20Order.ORC.PlacerOrderNumber.EntityIdentifier.Value;
                        string confirmOperCode = orgO20Order.ORC.GetOrderingProvider(0).IDNumber.Value;
                        string confirmDeptCode = orgO20Order.ORC.EnteringOrganization.Identifier.Value;
                        FS.FrameWork.Models.NeuObject obj = mapMgr.GetHISCode(EnumItemCodeMap.OutpatientOrder, new FS.FrameWork.Models.NeuObject(FS.HISFC.Models.Base.EnumSysClass.UC.ToString(), applyNO, ""), systemCode);
                        if (obj == null||string.IsNullOrEmpty(obj.ID))
                        {
                            errInfo = "获取HL7对照信息失败，" + mapMgr.Err + "申请单号：" + applyNO;
                            return -1;
                        }
                        //根据医嘱流水号查询费用信息
                        ArrayList alFeeInfo = this.outFeeMgr.QueryChargedFeeItemListsByClinicNO(regInfo.ID);
                        if (alFeeInfo == null)
                        {
                            errInfo = "获取费用信息失败，" + outFeeMgr.Err + "申请单号：" + applyNO;
                            return -1;
                        }

                        ArrayList alNoFeedInfo = new ArrayList();
                        foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList itemList in alFeeInfo)
                        {
                            if (itemList.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.UnDrug)
                            {
                                if (obj.ID.Equals(itemList.Order.ID))
                                {
                                    alNoFeedInfo.Add(itemList);
                                }
                            }
                        }

                        //根据查询的费用信息进行确认
                        if (outPatientBalanceFee.Balance(regInfo, alNoFeedInfo, ref errInfo) < 0)
                        {
                            errInfo = "打包确认收费失败，" + errInfo + "申请单号：" + applyNO;
                            return -1;
                        }

                        //根据医嘱流水号查询费用信息
                        alFeeInfo = this.outFeeMgr.QueryFeeDetailFromMOOrder(obj.ID);

                        foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList itemList in alFeeInfo)
                        {
                            if (itemList.NoBackQty <= 0)
                            {
                                continue;
                            }

                            int flag = this.outFeeMgr.UpdateConfirmFlag(itemList.RecipeNO, itemList.Order.ID, "1", confirmOperCode, confirmDeptCode, operDate, 0, itemList.Item.Qty);
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
                        applyNOS = applyNOS + "|" + applyNO;

                    }
                }
                else
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    errInfo= "未知的确认类型" + orgO20Order.ORC.OrderControl.Value;
                    return -1;
                }
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            return 1;
        }

    }
}
