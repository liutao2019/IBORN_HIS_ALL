using System;
using System.Collections;
using System.Collections.Generic;

namespace FS.SOC.Local.InpatientFee.ShenZhen.BinHai.BizProcess.Integrate
{  
    public class Fee : FS.HISFC.BizProcess.Integrate.Fee
    {
        #region �̶�������ȡ

        public int DoBedItemFee(ArrayList bedItems,
           FS.HISFC.Models.RADT.PatientInfo patient, int days, DateTime operDate, DateTime chargeDate, FS.HISFC.Models.Base.Bed bed)
        {
            //��ҩƷ������
            FS.HISFC.BizLogic.Fee.Item item = new FS.HISFC.BizLogic.Fee.Item();
            //��ͬ������


            FS.HISFC.BizLogic.Fee.PactUnitInfo pactMgr = new FS.HISFC.BizLogic.Fee.PactUnitInfo();
            //����������


            FS.HISFC.BizLogic.Manager.Constant constant = new FS.HISFC.BizLogic.Manager.Constant();
            //����
            //FS.FrameWork.Management.Transaction trans = new FS.FrameWork.Management.Transaction(this.feeInpatient.Connection);
            //trans.BeginTransaction();

            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            item.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            inpatientManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            pactMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            constant.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            try
            {
                ArrayList alFeeInfo = new ArrayList();
                //��λ��Ϣʵ��
                FS.SOC.Local.InpatientFee.ShenZhen.BinHai.Models.Fee.BedFeeItem bedItem = new FS.SOC.Local.InpatientFee.ShenZhen.BinHai.Models.Fee.BedFeeItem();
                for (int row = 0; row < bedItems.Count; row++)
                {
                    //ȡ����ȡ�Ĵ�λ��Ϣ


                    bedItem = bedItems[row] as FS.SOC.Local.InpatientFee.ShenZhen.BinHai.Models.Fee.BedFeeItem;

                    //�����λ��Ч���򲻽��з�����ȡ


                    if (bedItem.ValidState != FS.HISFC.Models.Base.EnumValidState.Valid) continue;

                    //�رյĴ�λ���մ�λ��.ת�ƺ��ͷŴ�λʱ��λ״̬��ΪC . {CA479D1B-BD94-459e-AA19-1AE2C4902DAF}
                    if (bed.Status.ID.ToString() == "C")
                    {
                        continue;
                    }

                    #region ������ӳ�ҽ������ �Է�ҽ��������ȡ��ͬ���ѵ�  Added By Huangd  2012/09/26

                    switch (bedItem.UseLimit)
                    {
                        case "0":

                            break;
                        case "1":
                            if (patient.Pact.ID != "1") continue;
                            break;
                        case "2":
                            if (patient.Pact.ID != "2") continue;
                            break;
                        default:

                            break;
                    }

                    #endregion

                    #region �жϷ���Ժ����(����W,�Ҵ�H,���R)�Ƿ���ȡ����Ŀ  writed by cuipeng  2005-11
                    if (bed.Status.ID.ToString() == "W" || bed.Status.ID.ToString() == "H" || bed.Status.ID.ToString() == "R")
                    {
                        //����շ���Ŀ���ڷ���Ժ���߲���ȡ����,�򲻴������Ŀ


                        if (bedItem.ExtendFlag == "0")
                        {
                            continue;
                        }
                        else
                        {
                            //��ɽ���� �������ݿ�������������ʱ����


                            //���ڰ�������,�̶�������ȡ����Ϊ"���˷�",���Ϊ��λ�ѵ�2��.
                            if (bed.Status.ID.ToString() == "W")
                            {
                                FS.FrameWork.Models.NeuObject obj = constant.GetConstant("FIN_FIXITEM", "BEDWRAP");
                                //if (obj == null)
                                //{
                                //    this.Err = constant.Err;
                                //    this.WriteErr();
                                //    continue;
                                //}

                                ////ȡԭ��Ŀ(��λ��)����
                                //FS.HISFC.Models.Fee.Item.Undrug tempItem = item.GetValidItemByUndrugCode(bedItem.ID);

                                //if (tempItem == null)
                                //{
                                //    this.Err = item.Err;
                                //    this.WriteErr();
                                //    continue;
                                //}
                                //FS.HISFC.Models.Fee.Item.Undrug peiItem = item.GetValidItemByUndrugCode(obj.Name);
                                //if (peiItem == null)
                                //{
                                //    this.Err = item.Err;
                                //    this.WriteErr();
                                //    continue;
                                //}

                                ////ָ���շ���Ŀ����(���˷���Ŀ����)
                                //bedItem.ID = peiItem.ID;
                                //bedItem.Name = peiItem.Name;
                                ////bedItem.ID = obj.Name;

                                ////����Ϊ��λ�ѵ�2��


                                //bedItem.User01 = (tempItem.Price * 2).ToString();

                            }
                        }
                    }
                    #endregion

                    #region �жϸ���Ŀ�Ƿ��ʱ���йأ�����յ��ѡ�ȡů��
                    if (bedItem.IsTimeRelation)
                    {
                        //�������� >= ��ʼ����,��Ϊ�������
                        if ((bedItem.EndTime.Month * 100 + bedItem.EndTime.Day) >= (bedItem.BeginTime.Month * 100 + bedItem.BeginTime.Day))
                        {
                            //�����ǰʱ�䲻������ʱ�䷶Χ�ڣ�����ȡ����Ŀ����


                            if ((operDate.Month * 100 + operDate.Day) > (bedItem.EndTime.Month * 100 + bedItem.EndTime.Day)
                                || (operDate.Month * 100 + operDate.Day) < (bedItem.BeginTime.Month * 100 + bedItem.BeginTime.Day))
                                continue;//--��ǰ�����ڼƷ���Ч����


                        }

                        else
                        { //�������� < ��ʼ���� :�����


                            //�����ǰʱ�䲻������ʱ�䷶Χ�ڣ�����ȡ����Ŀ����


                            if ((operDate.Month * 100 + operDate.Day) > (bedItem.EndTime.Month * 100 + bedItem.EndTime.Day)
                                && (operDate.Month * 100 + operDate.Day) < (bedItem.BeginTime.Month * 100 + bedItem.BeginTime.Day))
                                continue;//--��ǰ�����ڼƷ���Ч����


                        }
                    }
                    #endregion

                    #region �������ø�Ӥ���йصĹ̶�����,����Ӥ���Ƿ���ڶ��շ�


                    bool isBaby = false;//�Ƿ�Ӥ��,Ĭ�ϲ���Ӥ��
                    if (bedItem.IsBabyRelation)
                    {
                        if (patient.BabyCount == 0)
                            //Ӥ��������,����ȡ�������
                            continue;
                        else
                        {
                            //Ӥ������,ÿ��Ӥ����ȡһ��


                            isBaby = true;
                            bedItem.Qty = bedItem.Qty * patient.BabyCount;
                        }
                    }
                    #endregion

                    //������Ŀ����,���Ϊ0��Ĭ����1
                    if (bedItem.Qty == 0)
                        bedItem.Qty = 1;
                    //�����û����õ���������,����Ӧ��ȡ����
                    bedItem.Qty = bedItem.Qty * days;

                    //ȡ�շ���Ŀʵ����Ϣ
                    FS.HISFC.Models.Fee.Item.Undrug undrug = new FS.HISFC.Models.Fee.Item.Undrug();
                    undrug = item.GetValidItemByUndrugCode(bedItem.ID);
                    if (undrug == null)
                    {
                        this.Err = item.Err;
                        continue;
                    }

                    //������Ŀ�۸�,���ݺ�ͬ��λ����Ŀ����۸�
                    decimal price = 0;
                    decimal orgPrice = 0;
                    //{014680EC-6381-408b-98FB-A549DAA49B82}
                    //if (this.GetPriceForInpatient(patient.Pact.ID, undrug, ref price, ref orgPrice) == -1)
                    if (this.GetPriceForInpatient(patient, undrug, ref price, ref orgPrice) == -1)
                    {
                        this.Rollback();
                        this.Err = "��ȡ��Ŀ:" + undrug.ID + "�ļ۸�ʱ����!" + pactMgr.Err;
                        return -1;
                    }

                    //ȡ�õļ۸�Ϊ0,��ʹ��ȡ��ļ۸�
                    if (price != 0) undrug.Price = price;

                    //�������۹̶�Ϊ��λ�ѵ�2��. writed by cuipeng 2005-11
                    if (bed.Status.ID.ToString() == "W")
                    {
                        //undrug.Price = FS.FrameWork.Function.NConvert.ToDecimal(bedItem.User01);
                    }

                    //�Ʒѵ���Ϊ0, ����Ҫ�Ʒ�
                    if (undrug.Price == 0)
                    {
                        this.Err = "�Ʒѵ���Ϊ0:" + undrug.Name;
                        continue;
                    }


                    undrug.Qty = bedItem.Qty;
                    //ҽ�����߽ӿ�
                    //��ɽһû����Ҫ�����
                    //ʵ�帳ֵ
                    FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItem = new FS.HISFC.Models.Fee.Inpatient.FeeItemList();
                    feeItem.IsBaby = isBaby;
                    feeItem.Item = undrug;
                    feeItem.NoBackQty = undrug.Qty;
                    feeItem.RecipeNO = inpatientManager.GetUndrugRecipeNO();
                    feeItem.Patient.Pact.PayKind.ID = patient.Pact.PayKind.ID;
                    feeItem.TransType = FS.HISFC.Models.Base.TransTypes.Positive;
                    //feeItem.Order.InDept.ID =
                    feeItem.FeeOper.Dept.ID = patient.PVisit.PatientLocation.Dept.ID;
                    //feeItem.Order.NurseStation.ID = 
                    ((FS.HISFC.Models.RADT.PatientInfo)feeItem.Patient).PVisit.PatientLocation.NurseCell.ID = patient.PVisit.PatientLocation.NurseCell.ID;
                    //feeItem.Order.ReciptDept.ID =
                    feeItem.RecipeOper.Dept.ID = patient.PVisit.PatientLocation.Dept.ID;
                    //feeItem.Order.ExeDept.ID =
                    feeItem.ExecOper.Dept.ID = patient.PVisit.PatientLocation.Dept.ID;
                    if (patient.PVisit.AdmittingDoctor.ID == null || patient.PVisit.AdmittingDoctor.ID == "")
                        patient.PVisit.AdmittingDoctor.ID = "�ռƷ�";

                    //feeItem.Order.ReciptDoctor.ID =
                    feeItem.RecipeOper.ID = patient.PVisit.AdmittingDoctor.ID;
                    feeItem.PayType = FS.HISFC.Models.Base.PayTypes.Balanced;
                    //feeItem.IsBrought = "";
                    feeItem.ChargeOper.ID = "�ռƷ�";
                    feeItem.ChargeOper.OperTime = chargeDate;
                    feeItem.FeeOper.ID = "�ռƷ�";
                    feeItem.FeeOper.OperTime = operDate;
                    feeItem.SequenceNO = row;
                    feeItem.BalanceNO = 0;
                    feeItem.BalanceState = "0";
                    feeItem.FT.TotCost = undrug.Qty * undrug.Price;
                    feeItem.FT.OwnCost = undrug.Qty * undrug.Price;
                    feeItem.FTSource = new FS.HISFC.Models.Fee.Inpatient.FTSource("20");//̫���ˣ������ܱ���ext_flag2��2λ��
                    //---------------------------���Ѵ�λ�������0818------------------------
                    #region ���Ѵ�λ�������
                    if (patient.Pact.PayKind.ID == "03")
                    {
                        feeItem.FT.OwnCost = 0;//���һ��Ҫ�ӣ�����ҽ����ȡ�̶����ú��ڵ���������
                        //��λ�޶�
                        decimal BedLimit = FS.FrameWork.Public.String.FormatNumber(patient.FT.BedLimitCost * days, 2);
                        //�໤��λ�޶�
                        decimal IcuLimit = FS.FrameWork.Public.String.FormatNumber(patient.FT.AirLimitCost * days, 2);

                        /*�ֵ����TYPEΪBEDLIMITMINFEE
                        CODEΪ1Ϊ��ͨ����NAME�д������ͨ����С����CODE
                        CODEΪ2Ϊ�໤����NAME�д���Ǽ໤����С����CODE
                        */
                        FS.FrameWork.Models.NeuObject conBedMinFee = constant.GetConstant("BEDLIMITMINFEE", "1");
                        string bedMinFeeCode = "";
                        if (conBedMinFee != null)
                        {
                            if (string.IsNullOrEmpty(conBedMinFee.Name))
                            {
                                this.Err = "�����ֵ����ά��typeΪBEDLIMITMINFEE,CODE=1,NAME=��ͨ����С���ô��룡";
                            }
                            bedMinFeeCode = conBedMinFee.Name;//��ͨ����С���ô���
                        }

                        FS.FrameWork.Models.NeuObject conICUBedMinFee = constant.GetConstant("BEDLIMITMINFEE", "2");
                        string icuBedMinFeeCode = "";
                        if (conICUBedMinFee != null)
                        {
                            if (string.IsNullOrEmpty(conICUBedMinFee.Name))
                            {
                                this.Err = "�����ֵ����ά��typeΪBEDLIMITMINFEE,CODE=2,NAME=�໤����С���ô��룡";
                            }
                            icuBedMinFeeCode = conICUBedMinFee.Name;//�໤����С���ô���
                        }
                        ////�жϵ����Ƿ��Ѿ��չ��յ���
                        //decimal AirFee = 0;
                        //DateTime FeeBegin = new DateTime(operDate.Year, 10, 26, 0, 0, 0);
                        //DateTime FeeEnd = new DateTime(operDate.Year, 4, 26, 0, 0, 0);
                        //if (operDate > FeeBegin || operDate < FeeEnd)
                        //{
                        //    if (this.inpatientManager.GetAirFee(patient.ID, ref AirFee) > 0)//�ֵ��ά���յ�����ĿtypeΪAIRFEEITEM
                        //    {
                        //        BedLimit = BedLimit - AirFee;
                        //    }
                        //}

                        FS.FrameWork.Models.NeuObject billObj = constant.GetConstant("BILLPACT", patient.Pact.ID);

                        #region �жϳ��� �������
                        FS.HISFC.Models.Base.FTRate Rate = this.ComputeFeeRate(patient.Pact.ID, feeItem.Item);
                        if (Rate == null)
                        {
                            return -1;
                        }
                        feeItem.User01 = "1";//�����жϱ����FeeManager�в����µ��ü����������

                        bool computeLimit = true;//��Ŀ�Ƿ�������޶�

                        if (billObj != null && billObj.ID.Length >= 0 && billObj.Name == "�й���")
                        {
                            FS.FrameWork.Models.NeuObject unlimitObj = constant.GetConstant("UNLIMITITEM", feeItem.Item.ID);

                            if (unlimitObj != null && unlimitObj.ID.Length >= 1)
                            {
                                computeLimit = false;
                            }
                        }
                        if (feeItem.Item.MinFee.ID == bedMinFeeCode && computeLimit)
                        {
                            if (Rate.OwnRate == 1)
                            {
                                feeItem.FT.OwnCost = feeItem.FT.TotCost;
                            }
                            else
                            {
                                #region ��ͨ�����괦��
                                if (patient.FT.BedOverDeal == "1")
                                {//��������
                                    //������
                                    if (feeItem.FT.TotCost <= BedLimit)
                                    {
                                        BedLimit = BedLimit - feeItem.FT.TotCost;
                                    }
                                    else
                                    {//���겿��תΪ�Է�
                                        feeItem.FT.OwnCost = feeItem.FT.TotCost - BedLimit;
                                        BedLimit = 0;
                                    }
                                }
                                else if (patient.FT.BedOverDeal == "2")
                                {
                                    ////���겻�ƣ������޶��ڣ�ʣ�µ����
                                    if (feeItem.FT.TotCost > BedLimit)
                                    {
                                        feeItem.FT.TotCost = BedLimit;
                                        BedLimit = 0;
                                        if (feeItem.FT.TotCost == 0)
                                        {
                                            continue;
                                        }
                                    }
                                    else
                                    {
                                        BedLimit = BedLimit - feeItem.FT.TotCost;
                                    }
                                }
                                #endregion
                            }
                        }
                        else if (feeItem.Item.MinFee.ID == icuBedMinFeeCode && computeLimit)
                        {

                            if (Rate.OwnRate == 1)
                            {
                                feeItem.FT.OwnCost = feeItem.FT.TotCost;
                            }
                            else
                            {
                                #region �໤�����괦��


                                //���ô�λ�շѺ���������С������010��һ���Ǽ໤��,�������û������.
                                //�໤����ش�λ��ҲӦ��ά����010,����Ҳû������

                                //��������
                                if (patient.FT.BedOverDeal == "1")
                                {
                                    if (IcuLimit >= feeItem.FT.TotCost)
                                    {
                                        //�໤����׼���ڼ໤���ѣ�������								
                                        IcuLimit = IcuLimit - feeItem.FT.TotCost;
                                    }
                                    else
                                    {
                                        //���꣬���겿���Է�
                                        feeItem.FT.OwnCost = feeItem.FT.TotCost - IcuLimit;
                                        IcuLimit = 0;
                                    }
                                }
                                else if (patient.FT.BedOverDeal == "2")
                                {//���겻�ƣ������޶��ڣ�ʣ�µ����
                                    //����
                                    if (feeItem.FT.TotCost > IcuLimit)
                                    {
                                        feeItem.FT.TotCost = IcuLimit;
                                        IcuLimit = 0;
                                        if (feeItem.FT.TotCost == 0)
                                        {
                                            continue;
                                        }
                                    }
                                    else
                                    {
                                        IcuLimit = IcuLimit - feeItem.FT.TotCost;
                                    }
                                }
                                #endregion
                            }
                        }
                        #endregion
                        this.ComputeCost(feeItem, Rate);
                    }
                    #endregion
                    //-----------------------------------------------------------------------
                    if (this.FeeItem(patient, feeItem) == -1)
                    {
                        this.Rollback();
                        this.Err = "����סԺ�շ�ҵ������!" + this.Err;
                        return -1;
                    }
                    alFeeInfo.Add(feeItem);
                }
                if (inpatientManager.UpdateFixFeeDateByPerson(patient.ID, patient.FT.PreFixFeeDateTime.ToString()) == -1)
                {
                    this.Rollback();
                    this.Err = "���»����ϴ���ȡ����ʱ��ʱ����!";
                    return -1;
                }


                //������Ϣ
                #region HL7��Ϣ����
                object curInterfaceImplement = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(FS.SOC.Local.InpatientFee.ShenZhen.BinHai.BizProcess.Integrate.Fee), typeof(FS.SOC.HISFC.BizProcess.MessagePatternInterface.IOrder));
                if (curInterfaceImplement is FS.SOC.HISFC.BizProcess.MessagePatternInterface.IOrder)
                {
                    FS.SOC.HISFC.BizProcess.MessagePatternInterface.IOrder curIOrderControl = curInterfaceImplement as FS.SOC.HISFC.BizProcess.MessagePatternInterface.IOrder;

                    int param = curIOrderControl.SendFeeInfo(patient, alFeeInfo, true);
                    if (param == -1)
                    {
                        this.Rollback();
                        this.Err = curIOrderControl.Err;
                        return -1;
                    }
                }

                #endregion

                this.Commit();
            }
            catch (Exception e)
            {
                this.Rollback();
                this.Err = "����Ϊ:" + patient.PVisit.Name + "סԺ��ˮ��Ϊ:" +
                    patient.ID + "��ȡ�̶�����ʧ��!" + e.Message;
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// ���߷��ñ���
        /// </summary>
        /// <param name="PactID">��ͬ��λ����</param>
        /// <param name="item">ҩƷ��ҩƷ��Ϣ</param>
        /// <returns>ʧ��null���ɹ� FS.HISFC.Models.Fee.FtRate</returns>
        private FS.HISFC.Models.Base.FTRate ComputeFeeRate(string PactID, FS.HISFC.Models.Base.Item item)
        {
            FS.HISFC.BizLogic.Fee.PactUnitItemRate PactItemRate = new FS.HISFC.BizLogic.Fee.PactUnitItemRate();

            PactItemRate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            FS.HISFC.Models.Base.PactItemRate ObjPactItemRate = null;
            try
            {
                //��Ŀ
                ObjPactItemRate = PactItemRate.GetOnepPactUnitItemRateByItem(PactID, item.ID);
                if (ObjPactItemRate == null)
                {
                    //��С����
                    ObjPactItemRate = PactItemRate.GetOnepPactUnitItemRateByItem(PactID, item.MinFee.ID);
                    if (ObjPactItemRate == null)
                    {
                        //ȡ��ͬ��λ�ı���
                        FS.HISFC.BizLogic.Fee.PactUnitInfo PactManagment = new FS.HISFC.BizLogic.Fee.PactUnitInfo();

                        PactManagment.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);


                        FS.HISFC.Models.Base.PactInfo PactUnitInfo = PactManagment.GetPactUnitInfoByPactCode(PactID);

                        if (PactUnitInfo == null)
                        {
                            this.Err = "��ú�ͬ��λ��Ϣ����" + PactManagment.Err;
                            return null;
                        }
                        try
                        {
                            ObjPactItemRate = new FS.HISFC.Models.Base.PactItemRate();
                            ObjPactItemRate.Rate.PayRate = PactUnitInfo.Rate.PayRate;
                            ObjPactItemRate.Rate.OwnRate = PactUnitInfo.Rate.OwnRate;
                        }
                        catch
                        {
                            this.Err = "��ú�ͬ��λ��Ϣ����" + PactManagment.Err;
                            return null;
                        }
                    }
                }
            }
            catch
            {
                this.Err = "��ú�ͬ��λ��Ϣ����";
                return null;
            }

            return ObjPactItemRate.Rate;
        }

        /// <summary>
        ///  �����ܷ��õĸ�����ɲ��ֵ�ֵ
        /// </summary>
        /// <param name="ItemList"></param>
        /// <param name="rate">������֮��ı���</param>
        /// <returns>-1ʧ�ܣ�0�ɹ�</returns>
        private int ComputeCost(FS.HISFC.Models.Fee.Inpatient.FeeItemList ItemList, FS.HISFC.Models.Base.FTRate rate)
        {
            if (ItemList.FT.OwnCost == 0)
            {
                ItemList.FT.OwnCost = FS.FrameWork.Public.String.FormatNumber(ItemList.FT.TotCost * rate.OwnRate, 2);
                ItemList.FT.PayCost = FS.FrameWork.Public.String.FormatNumber((ItemList.FT.TotCost - ItemList.FT.OwnCost) * rate.PayRate, 2);
                ItemList.FT.PubCost = ItemList.FT.TotCost - ItemList.FT.OwnCost - ItemList.FT.PayCost;
            }
            else
            {
                ItemList.FT.PayCost = FS.FrameWork.Public.String.FormatNumber((ItemList.FT.TotCost - ItemList.FT.OwnCost) * rate.PayRate, 2);
                ItemList.FT.PubCost = ItemList.FT.TotCost - ItemList.FT.OwnCost - ItemList.FT.PayCost;
            }
            return 0;

        }

        #endregion

    }
}
