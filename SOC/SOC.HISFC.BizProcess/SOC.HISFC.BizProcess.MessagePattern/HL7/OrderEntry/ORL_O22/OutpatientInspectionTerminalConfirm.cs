using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.BizProcess.MessagePatternInterface;
using FS.HL7Message;
using System.Collections;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.HL7.OrderEntry.ORL_O22
{
    /// <summary>
    /// 检验终端确认
    /// </summary>
    public class OutpatientInspectionTerminalConfirm
    {
        private FS.HISFC.BizLogic.Fee.Outpatient outFeeMgr = new FS.HISFC.BizLogic.Fee.Outpatient();
        FS.HISFC.BizProcess.Integrate.Registration.Registration regIntegrate = new FS.HISFC.BizProcess.Integrate.Registration.Registration();
        OutPatientBalanceFee outPatientBalanceFee = new OutPatientBalanceFee();

        public int ProcessMessage(NHapi.Model.V24.Message.ORL_O22 orlO22, ref NHapi.Base.Model.IMessage ackMessage, ref string errInfo)
        {
            string systemCode = orlO22.MSH.SendingApplication.NamespaceID.Value;
            string clinicCode = orlO22.RESPONSE.PATIENT.GetGENERAL_ORDER(0).GetORDER(0).ORC.PlacerOrderNumber.UniversalID.Value;
            if (string.IsNullOrEmpty(clinicCode))
            {
                errInfo= "患者流水号为空！";
                return -1;
            }

            FS.HISFC.Models.Registration.Register regInfo = regIntegrate.GetByClinic(clinicCode);
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
            string applyCancelNOS = "";

            DateTime operDate = this.outFeeMgr.GetDateTimeFromSysDateTime();
            ItemCodeMapManager mapMgr = new ItemCodeMapManager();

            for (int i = 0; i < orlO22.RESPONSE.PATIENT.GetGENERAL_ORDER().ORDERRepetitionsUsed; i++)
            {
                NHapi.Model.V24.Group.ORL_O22_ORDER orlO22Order = orlO22.RESPONSE.PATIENT.GetGENERAL_ORDER().GetORDER(i);
                if (orlO22Order.ORC.OrderControl.Value.Equals("OR"))
                {
                    if (!applyNOS.Contains(orlO22Order.ORC.PlacerOrderNumber.EntityIdentifier.Value))
                    {
                        string applyNO = orlO22Order.ORC.PlacerOrderNumber.EntityIdentifier.Value;
                        string confirmOperCode = orlO22Order.ORC.GetEnteredBy(0).IDNumber.Value;
                        string confirmDeptCode = orlO22Order.ORC.EnteringOrganization.Identifier.Value;
                        //根据申请单号和类型更新确认人、确认数量等
                        FS.FrameWork.Models.NeuObject obj = mapMgr.GetHISCode(EnumItemCodeMap.OutpatientOrder, new FS.FrameWork.Models.NeuObject(FS.HISFC.Models.Base.EnumSysClass.UL.ToString(), applyNO, ""),systemCode);
                        if (obj == null || string.IsNullOrEmpty(obj.ID))
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
                        if (alFeeInfo == null)
                        {
                            errInfo = "获取费用信息失败，" + outFeeMgr.Err + "申请单号：" + applyNO;
                            return -1;
                        }

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
                        
                        //根据申请单号和类型更新确认人、确认数量等
                        applyNOS += "|" + applyNO;
                    }
                }
                else if (orlO22Order.ORC.OrderControl.Value.Equals("OC"))//取消登记
                {
                    if (!applyCancelNOS.Contains(orlO22Order.ORC.PlacerOrderNumber.EntityIdentifier.Value))
                    {
                        string applyNO = orlO22Order.ORC.PlacerOrderNumber.EntityIdentifier.Value;
                        string confirmOperCode = orlO22Order.ORC.GetOrderingProvider(0).IDNumber.Value;
                        string confirmDeptCode = orlO22Order.ORC.EnteringOrganization.Identifier.Value;

                        //根据申请单号和类型更新确认人、确认数量等
                        FS.FrameWork.Models.NeuObject obj = mapMgr.GetHISCode(EnumItemCodeMap.OutpatientOrder, new FS.FrameWork.Models.NeuObject(FS.HISFC.Models.Base.EnumSysClass.UL.ToString(), applyNO, ""),systemCode);
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
                            //if (itemList.NoBackQty > 0)
                            //{
                            //    continue;
                            //}
                            //chenxin 2012-10-24 没有确认过的不需要取消
                            if (itemList.IsConfirmed == false)
                            {
                                continue;
                            }
                            //根据申请单号和类型更新确认人、确认数量等
                            int flag = this.outFeeMgr.UpdateConfirmFlag(itemList.RecipeNO, obj.ID, "0", confirmOperCode, confirmDeptCode, operDate, itemList.Item.Qty-itemList.NoBackQty, 0);
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
                else
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    errInfo= "未知的确认类型" + orlO22Order.ORC.OrderControl.Value;
                    return -1;
                }
            }
            FS.FrameWork.Management.PublicTrans.Commit();

            return 1;
        }

    }
}
