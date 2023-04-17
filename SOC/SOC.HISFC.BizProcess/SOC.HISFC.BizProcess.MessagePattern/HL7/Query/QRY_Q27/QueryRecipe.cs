using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.BizProcess.MessagePatternInterface;
using System.Collections;
using FS.SOC.HISFC.BizProcess.CommonInterface;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.HL7.Query.QRY_Q27
{
    public class QueryRecipe : AbstractReceiver<NHapi.Model.V24.Message.QRY_Q27, NHapi.Model.V24.Message.RAR_RAR>
    {
        #region AbstractAcceptMessage<QRY_Q27,IMessage> 成员

        public override int ProcessMessage(NHapi.Model.V24.Message.QRY_Q27 o, ref NHapi.Model.V24.Message.RAR_RAR ackMessage)
        {
            FS.HISFC.BizLogic.Fee.Account accountMgr = new FS.HISFC.BizLogic.Fee.Account();
            string MarkNO=o.QRD.GetWhoSubjectFilter(0).SecondAndFurtherGivenNamesOrInitialsThereof.Value;
            if (string.IsNullOrEmpty(MarkNO))
            {
                this.Err = "传入的卡号为空，QRY_Q27.8.4";
                return -1;
            }
            string cardNO="";
            //取病历号
            if (accountMgr.GetCardNoByMarkNo(MarkNO, ref cardNO) == false)
            {
                this.Err = "获取病历号错误，原因：" + accountMgr.Err + "，健康卡号=" + MarkNO;
                return -1;
            }

            FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParamMgr=new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
            int Days = controlParamMgr.GetControlParam<int>("MZ0014");
            //查找挂号记录
            FS.HISFC.BizLogic.Registration.Register regMgr = new FS.HISFC.BizLogic.Registration.Register();
            ArrayList alReg = regMgr.Query(cardNO, CommonController.CreateInstance().GetSystemTime().AddDays(-Days));

            if (alReg == null)
            {
                this.Err = "获取挂号信息失败，原因：" + regMgr.Err + "，病历号=" + cardNO;
                return -1;
            }

            if (alReg.Count == 0)
            {
                this.Err = "患者不存在有效的挂号信息！";
                return -1;
            }

            #region PID
            ackMessage.GetDEFINITION(0).PATIENT.PID.PatientID.ID.Value=cardNO;
            #endregion

            

            FS.HISFC.BizLogic.Fee.Outpatient outpatientMgr = new FS.HISFC.BizLogic.Fee.Outpatient();
            FS.HISFC.BizLogic.Manager.Sequence sequenceMgr = new FS.HISFC.BizLogic.Manager.Sequence();
            for (int i = 0; i < alReg.Count; i++)
            {
                FS.HISFC.Models.Registration.Register register = alReg[i] as FS.HISFC.Models.Registration.Register;

                #region NTE

                ackMessage.GetDEFINITION(0).PATIENT.GetNTE(0).SourceOfComment.Value = register.ID;
                ackMessage.GetDEFINITION(0).PATIENT.GetNTE(0).CommentType.Identifier.Value = "O";

                #endregion

                ArrayList alFeeDetail = outpatientMgr.QueryChargedFeeItemListsByClinicNO(register.ID);
                if (alFeeDetail == null)
                {
                    this.Err = "获取费用信息失败，原因：" + outpatientMgr.Err + "，门诊流水号=" + register.ID;
                    return -1;
                }

                if (alFeeDetail.Count <= 0)
                {
                    continue;
                }
                Dictionary<string, NHapi.Model.V24.Group.RAR_RAR_ORDER> dictionaryItems = new Dictionary<string, NHapi.Model.V24.Group.RAR_RAR_ORDER>();

                for (int num = 0; num < alFeeDetail.Count; num++)
                {
                    FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItemList = alFeeDetail[num] as FS.HISFC.Models.Fee.Outpatient.FeeItemList;
                    feeItemList.FT.TotCost = feeItemList.FT.OwnCost + feeItemList.FT.PayCost + feeItemList.FT.PubCost;
                    NHapi.Model.V24.Group.RAR_RAR_ORDER RAR_ORDER = null;

                    #region ORC
                    if (!dictionaryItems.ContainsKey(feeItemList.RecipeSequence + feeItemList.Patient.ID))
                    {
                      RAR_ORDER=  ackMessage.GetDEFINITION(0).GetORDER(ackMessage.GetDEFINITION(0).ORDERRepetitionsUsed);

                        //获取临时发票号
                        string invoiceNO = sequenceMgr.GetNewMzInvoiceNO();
                        //收费序列
                        RAR_ORDER.ORC.PlacerOrderNumber.EntityIdentifier.Value = feeItemList.RecipeSequence;
                        //临时发票号
                        RAR_ORDER.ORC.PlacerOrderNumber.NamespaceID.Value = invoiceNO;
                        //患者流水号
                        RAR_ORDER.ORC.PlacerOrderNumber.UniversalID.Value = feeItemList.Patient.ID;
                        //患者类型
                        RAR_ORDER.ORC.PlacerOrderNumber.UniversalIDType.Value = "O";

                        //医生
                        RAR_ORDER.ORC.GetEnteredBy(0).IDNumber.Value = feeItemList.RecipeOper.ID;
                        RAR_ORDER.ORC.GetEnteredBy(0).FamilyName.Surname.Value = CommonController.CreateInstance().GetEmployeeName(feeItemList.RecipeOper.ID);
                        //科室
                        RAR_ORDER.ORC.EntererSLocation.PointOfCare.Value = feeItemList.RecipeOper.Dept.ID;
                        RAR_ORDER.ORC.EntererSLocation.Room.Value = CommonController.CreateInstance().GetDepartmentName(feeItemList.RecipeOper.Dept.ID);

                        RAR_ORDER.ORC.AdvancedBeneficiaryNoticeCode.Identifier.Value = feeItemList.FT.TotCost.ToString("F2");
                        //社保流水号
                        RAR_ORDER.ORC.FillerOrderNumber.EntityIdentifier.Value = feeItemList.Patient.ID;
                        //社保单号-同发票号
                        RAR_ORDER.ORC.FillerOrderNumber.NamespaceID.Value = invoiceNO;

                        dictionaryItems[feeItemList.RecipeSequence + feeItemList.Patient.ID] = RAR_ORDER;
                    }
                    else
                    {
                        RAR_ORDER = dictionaryItems[feeItemList.RecipeSequence + feeItemList.Patient.ID];
                        decimal tot_cost = FS.FrameWork.Function.NConvert.ToDecimal(RAR_ORDER.ORC.AdvancedBeneficiaryNoticeCode.Identifier.Value) + feeItemList.FT.TotCost;
                        RAR_ORDER.ORC.AdvancedBeneficiaryNoticeCode.Identifier.Value = tot_cost.ToString("F2");

                    }
                    #endregion

                    #region RXA

                    //项目编码
                    NHapi.Model.V24.Segment.RXA RXA = RAR_ORDER.GetRXA(dictionaryItems[feeItemList.RecipeSequence + feeItemList.Patient.ID].RXARepetitionsUsed);
                    //序号
                    RXA.GiveSubIDCounter.Value = dictionaryItems[feeItemList.RecipeSequence + feeItemList.Patient.ID].RXARepetitionsUsed.ToString();
                    //单价
                    RXA.AdministrationSubIDCounter.Value = feeItemList.Item.Price.ToString("F4");
                    //项目
                    RXA.AdministeredCode.Identifier.Value = feeItemList.Item.ID;
                    RXA.AdministeredCode.Text.Value = feeItemList.Item.Name;
                    //数量
                    if (feeItemList.Item.PackQty <= 0)
                    {
                        feeItemList.Item.PackQty = 1;
                    }
                    RXA.AdministeredAmount.Value = (feeItemList.Item.Qty/feeItemList.Item.PackQty).ToString("F2");
                    //计价单位
                    RXA.AdministeredUnits.Identifier.Value = feeItemList.Item.PriceUnit;
                    //规格
                    RXA.AdministeredUnits.Text.Value = feeItemList.Item.Specs;

                    //医保对照码和医保结算项目
                    if (register.Pact.PayKind.ID == "02")
                    {
                        FS.HISFC.BizLogic.Fee.Interface interfaceMgr = new FS.HISFC.BizLogic.Fee.Interface();
                        FS.HISFC.Models.SIInterface.Compare compare = new FS.HISFC.Models.SIInterface.Compare();
                        if (interfaceMgr.GetCompareSingleItem(feeItemList.Patient.Pact.ID, feeItemList.Item.ID, ref compare) == -1)
                        {
                            this.Err = interfaceMgr.Err;
                            return -1;
                        }

                        if (compare == null || compare.HisCode == null || compare.HisCode == "")
                        {
                            compare = new FS.HISFC.Models.SIInterface.Compare();
                        }


                        RXA.AdministeredStrengthUnits.Identifier.Value = compare.CenterItem.ID;
                        RXA.AdministeredStrengthUnits.Text.Value = compare.CenterItem.FeeCode;
                    }

                    //合计金额
                    RXA.AdministeredStrength.Value = feeItemList.FT.TotCost.ToString("F2");

                    #endregion
                }

            }

            return 1;
        }

        #endregion
    }
}
