using System;
using System.Collections.Generic;
using System.Text;
using FS.FrameWork.Management;
using FS.HISFC.Models.Base;
using System.Collections;
using System.Data;
using FS.HISFC.Models.Fee.Outpatient;
using FS.FrameWork.Function;
using FS.FrameWork.Models;
using FS.HISFC.Models.Registration;
using System.Reflection;
using FS.HISFC.Models.Account;
using FS.HISFC.Models.Fee.Item;
using System.Windows.Forms;

namespace FS.SOC.HISFC.PE.BizLogic
{
    /// <summary>
    /// [��������: ��������]
    /// </summary>
    public class Fee : FS.FrameWork.Management.Database
    {
        #region ����
        /// <summary>
        /// ����ҵ���
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.Outpatient outpatientManager = new FS.HISFC.BizLogic.Fee.Outpatient();
        /// <summary>
        /// ������ҵ��� {2CEA3B1D-2E59-44ac-9226-7724413173C5} ��ҵ��������ȫ����Ϊ�Ǿ�̬�ı���
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.InPatient inpatientManager = new FS.HISFC.BizLogic.Fee.InPatient();
        /// <summary>
        /// ���ҺŹ�����
        /// </summary>
        protected FS.SOC.HISFC.PE.BizLogic.Register registerManager = new FS.SOC.HISFC.PE.BizLogic.Register();
        /// <summary>
        /// ҽ��ҵ���
        /// </summary>
        protected FS.HISFC.BizLogic.Order.Order orderManager = new FS.HISFC.BizLogic.Order.Order();
        /// <summary>
        /// ҩƷҵ���
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Pharmacy pharmarcyManager = new FS.HISFC.BizProcess.Integrate.Pharmacy();
        /// <summary>
        /// �շ�ҵ���
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Fee feeManager = new FS.HISFC.BizProcess.Integrate.Fee();
        /// <summary>
        /// ��λ�ѹ�����
        /// </summary>
        private FS.HISFC.BizLogic.Fee.BedFeeItem feeBedFeeItem = new FS.HISFC.BizLogic.Fee.BedFeeItem();
        /// <summary>
        /// ����ҵ���
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();
        /// <summary>
        /// �����ʻ���ҵ���
        /// </summary>
        protected static FS.HISFC.BizLogic.Fee.Account accountManager = new FS.HISFC.BizLogic.Fee.Account();
        /// <summary>
        /// ��Ʊҵ���
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.InvoiceServiceNoEnum invoiceServiceManager = new FS.HISFC.BizLogic.Fee.InvoiceServiceNoEnum();
        /// <summary>
        /// ����ҽ��ҵ���
        /// </summary>
        protected FS.HISFC.BizLogic.Order.OutPatient.Order orderOutpatientManager = new FS.HISFC.BizLogic.Order.OutPatient.Order();
        /// <summary>
        /// ���ҵ���
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.PhysicalExamination.ExamiManager examiIntegrate = new FS.HISFC.BizProcess.Integrate.PhysicalExamination.ExamiManager();
        /// <summary>
        /// ���Ʋ���ҵ���
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
        /// <summary>
        /// item
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.Item itemManager = new FS.HISFC.BizLogic.Fee.Item();
        FS.HISFC.BizProcess.Integrate.Terminal.Confirm confirmIntegrate = new FS.HISFC.BizProcess.Integrate.Terminal.Confirm();
        /// <summary>
        /// ������Ŀ��ϸҵ���
        /// </summary>
        FS.HISFC.BizLogic.Fee.UndrugPackAge undrugPackAgeMgr = new FS.HISFC.BizLogic.Fee.UndrugPackAge();
        /// <summary>
        /// �ִ���
        /// </summary>
        FS.HISFC.BizProcess.Interface.FeeInterface.ISplitRecipe iSplitRecipe = null;
        /// <summary>
        /// //�Ƿ����÷���ϵͳ 1 ���� ���� ������
        /// </summary>
        string pValue = "";
        /// <summary>
        /// ÿ�������ɷ�Ϊ��
        /// </summary>
        public static bool isDoseOnceCanNull = false;
        /// <summary>
        /// ��Ʊ��ʽ
        /// </summary>
        public static string invoiceStytle = "0";
        /// <summary>
        /// �ִ����ź������
        /// </summary>
        private static bool isDecSysClassWhenGetRecipeNO = false;
        /// <summary>
        /// �Ѳ��ã����ò���
        /// {2D61A81A-AA2A-4655-A2EE-5CEA2A38FF8A}
        /// </summary>
        private bool isTempInvoice = false;
        /// <summary>
        /// �Ƿ�ȡ��ʱ��Ʊ��
        /// </summary>
        public bool IsTempInvoice
        {
            get
            {
                return this.isTempInvoice;
            }
            set
            {
                this.isTempInvoice = value;
            }
        }
        /// <summary>
        /// ����շ���Ŀ�б��� ϵͳ���ִ�п��ң����� ���ƴ�����
        /// ͬһϵͳ���ͳһִ�п��ң�ͬһ��������Ŀ��������ͬ
        /// ���Ѿ�����ô����ŵ���Ŀ���������·���
        /// </summary>
        /// <param name="feeDetails">������Ϣ</param>
        /// <param name="t">���ݿ�Trans</param>
        /// <param name="errText">������Ϣ</param>
        /// <returns>falseʧ�� true�ɹ�</returns>
        public bool SetRecipeNOOutpatient(FS.HISFC.Models.Registration.Register r, ArrayList feeDetails, ref string errText)
        {
            if (iSplitRecipe == null)
            {
                iSplitRecipe = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.FeeInterface.ISplitRecipe)) as FS.HISFC.BizProcess.Interface.FeeInterface.ISplitRecipe;
            }
            if (iSplitRecipe != null)
            {
                //�ִ���
                return iSplitRecipe.SplitRecipe(r, feeDetails, ref errText);
            }
            else
            {
                #region Ĭ�ϵ�ʵ��
                bool isDealCombNO = false; //�Ƿ����ȴ�����Ϻ�
                int noteCounts = 0;        //��õ��Ŵ���������Ŀ��

                //�Ƿ����ȴ�����Ϻ�
                isDealCombNO = controlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.Const.DEALCOMBNO, false, true);

                //��õ��Ŵ���������Ŀ��, Ĭ����Ŀ�� 5
                noteCounts = controlParamIntegrate.GetControlParam<int>(FS.HISFC.BizProcess.Integrate.Const.NOTECOUNTS, false, 5);

                //�Ƿ����ϵͳ���
                isDecSysClassWhenGetRecipeNO = controlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.Const.DEC_SYS_WHENGETRECIPE, false, false);

                //�Ƿ����ȴ����ݴ��¼
                bool isDecTempSaveWhenGetRecipeNO = controlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.Const.���������ȿ��Ƿַ���¼, false, false);

                ArrayList sortList = new ArrayList();
                while (feeDetails.Count > 0)
                {
                    ArrayList sameNotes = new ArrayList();
                    FeeItemList compareItem = feeDetails[0] as FeeItemList;
                    foreach (FeeItemList f in feeDetails)
                    {
                        if (isDecSysClassWhenGetRecipeNO)
                        {
                            if (f.ExecOper.Dept.ID == compareItem.ExecOper.Dept.ID
                                && f.Days == compareItem.Days && (isDecTempSaveWhenGetRecipeNO ? f.RecipeSequence == compareItem.RecipeSequence : true))
                            {
                                sameNotes.Add(f);
                            }
                        }
                        else
                        {
                            if (f.Item.SysClass.ID.ToString() == compareItem.Item.SysClass.ID.ToString()
                                && f.ExecOper.Dept.ID == compareItem.ExecOper.Dept.ID
                                && f.Days == compareItem.Days && (isDecTempSaveWhenGetRecipeNO ? f.RecipeSequence == compareItem.RecipeSequence : true))
                            {
                                sameNotes.Add(f);
                            }
                        }

                    }
                    sortList.Add(sameNotes);
                    foreach (FeeItemList f in sameNotes)
                    {
                        feeDetails.Remove(f);
                    }
                }

                foreach (ArrayList temp in sortList)
                {
                    ArrayList combAll = new ArrayList();
                    ArrayList noCombAll = new ArrayList();
                    ArrayList noCombUnits = new ArrayList();
                    ArrayList noCombFinal = new ArrayList();


                    if (isDealCombNO)//���ȴ�����Ϻţ������е���Ϻ������·���
                    {
                        //��ѡ��û����Ϻŵ���Ŀ
                        foreach (FeeItemList f in temp)
                        {
                            if (f.Order.Combo.ID == null || f.Order.Combo.ID == string.Empty)
                            {
                                noCombAll.Add(f);
                            }
                        }
                        //������������ɾ��û����Ϻŵ���Ŀ
                        foreach (FeeItemList f in noCombAll)
                        {
                            temp.Remove(f);
                        }
                        //���ͬһ���������Ŀ�������·���
                        while (noCombAll.Count > 0)
                        {
                            noCombUnits = new ArrayList();
                            foreach (FeeItemList f in noCombAll)
                            {
                                if (noCombUnits.Count < noteCounts)
                                {
                                    noCombUnits.Add(f);
                                }
                                else
                                {
                                    break;
                                }
                            }
                            noCombFinal.Add(noCombUnits);
                            foreach (FeeItemList f in noCombUnits)
                            {
                                noCombAll.Remove(f);
                            }
                        }
                        //���ʣ�����Ŀ��Ŀ> 0˵��������ϵ���Ŀ
                        if (temp.Count > 0)
                        {
                            while (temp.Count > 0)
                            {
                                ArrayList combNotes = new ArrayList();
                                FeeItemList compareItem = temp[0] as FeeItemList;
                                foreach (FeeItemList f in temp)
                                {
                                    if (f.Order.Combo.ID == compareItem.Order.Combo.ID)
                                    {
                                        combNotes.Add(f);
                                    }
                                }
                                combAll.Add(combNotes);
                                foreach (FeeItemList f in combNotes)
                                {
                                    temp.Remove(f);
                                }
                            }
                        }
                        foreach (ArrayList tempNoComb in noCombFinal)
                        {
                            string recipeNo = null;//������ˮ��
                            int noteSeq = 1;//��������Ŀ��ˮ��

                            string tempRecipeNO = string.Empty;
                            int tempSequence = 0;
                            this.GetRecipeNoAndMaxSeq(tempNoComb, ref tempRecipeNO, ref tempSequence, r.ID);

                            if (tempRecipeNO != string.Empty && tempSequence > 0)
                            {
                                tempSequence += 1;
                                foreach (FeeItemList f in tempNoComb)
                                {
                                    feeDetails.Add(f);
                                    if (f.RecipeNO != null && f.RecipeNO != string.Empty)//�Ѿ����䴦����
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        f.RecipeNO = tempRecipeNO;
                                        f.SequenceNO = tempSequence;
                                        tempSequence++;
                                    }
                                }
                            }
                            else
                            {
                                recipeNo = outpatientManager.GetRecipeNO();
                                if (recipeNo == null || recipeNo == string.Empty)
                                {
                                    errText = "��ô����ų���!";
                                    return false;
                                }
                                foreach (FeeItemList f in tempNoComb)
                                {
                                    feeDetails.Add(f);
                                    if (f.RecipeNO != null && f.RecipeNO != string.Empty)//�Ѿ����䴦����
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        f.RecipeNO = recipeNo;
                                        f.SequenceNO = noteSeq;
                                        noteSeq++;
                                    }
                                }
                            }
                        }
                        foreach (ArrayList tempComb in combAll)
                        {
                            string recipeNo = null;//������ˮ��
                            int noteSeq = 1;//��������Ŀ��ˮ��

                            string tempRecipeNO = string.Empty;
                            int tempSequence = 0;
                            this.GetRecipeNoAndMaxSeq(tempComb, ref tempRecipeNO, ref tempSequence, r.ID);

                            if (tempRecipeNO != string.Empty && tempSequence > 0)
                            {
                                tempSequence += 1;
                                foreach (FeeItemList f in tempComb)
                                {
                                    feeDetails.Add(f);
                                    if (f.RecipeNO != null && f.RecipeNO != string.Empty)//�Ѿ����䴦����
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        f.RecipeNO = tempRecipeNO;
                                        f.SequenceNO = tempSequence;
                                        tempSequence++;
                                    }
                                }
                            }
                            else
                            {
                                recipeNo = outpatientManager.GetRecipeNO();
                                if (recipeNo == null || recipeNo == string.Empty)
                                {
                                    errText = "��ô����ų���!";
                                    return false;
                                }
                                foreach (FeeItemList f in tempComb)
                                {
                                    feeDetails.Add(f);
                                    if (f.RecipeNO != null && f.RecipeNO != string.Empty)//�Ѿ����䴦����
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        f.RecipeNO = recipeNo;
                                        f.SequenceNO = noteSeq;
                                        noteSeq++;
                                    }
                                }
                            }
                        }
                    }
                    else //�����ȴ�����Ϻ�
                    {
                        ArrayList counts = new ArrayList();
                        ArrayList countUnits = new ArrayList();
                        while (temp.Count > 0)
                        {
                            countUnits = new ArrayList();
                            foreach (FeeItemList f in temp)
                            {
                                if (countUnits.Count < noteCounts)
                                {
                                    countUnits.Add(f);
                                }
                                else
                                {
                                    break;
                                }
                            }
                            counts.Add(countUnits);
                            foreach (FeeItemList f in countUnits)
                            {
                                temp.Remove(f);
                            }
                        }

                        //{B24B174D-F261-4c6b-94C9-EEED0F736013}
                        Hashtable hs = new Hashtable();


                        foreach (ArrayList tempCounts in counts)
                        {
                            string recipeNO = null;//������ˮ��
                            int recipeSequence = 1;//��������Ŀ��ˮ��

                            string tempRecipeNO = string.Empty;
                            int tempSequence = 0;
                            this.GetRecipeNoAndMaxSeq(tempCounts, ref tempRecipeNO, ref tempSequence, r.ID);
                            //{B24B174D-F261-4c6b-94C9-EEED0F736013}
                            if (hs.Contains(tempRecipeNO))
                            {
                                tempSequence = FS.FrameWork.Function.NConvert.ToInt32((hs[tempRecipeNO] as FS.FrameWork.Models.NeuObject).Name);
                            }
                            else
                            {
                                FS.FrameWork.Models.NeuObject obj = new NeuObject();
                                obj.ID = tempRecipeNO;
                                obj.Name = tempSequence.ToString();
                                hs.Add(tempRecipeNO, obj);
                            }

                            if (tempRecipeNO != string.Empty && tempSequence > 0)
                            {
                                tempSequence += 1;
                                foreach (FeeItemList f in tempCounts)
                                {
                                    feeDetails.Add(f);
                                    if (f.RecipeNO != null && f.RecipeNO != string.Empty)//�Ѿ����䴦����
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        f.RecipeNO = tempRecipeNO;
                                        f.SequenceNO = tempSequence;
                                        tempSequence++;
                                    }
                                }
                                //{B24B174D-F261-4c6b-94C9-EEED0F736013}
                                if (hs.Contains(tempRecipeNO))
                                {
                                    (hs[tempRecipeNO] as FS.FrameWork.Models.NeuObject).Name = tempSequence.ToString();
                                }
                            }
                            else
                            {
                                recipeNO = outpatientManager.GetRecipeNO();
                                if (recipeNO == null || recipeNO == string.Empty)
                                {
                                    errText = "��ô����ų���!";
                                    return false;
                                }
                                foreach (FeeItemList f in tempCounts)
                                {
                                    feeDetails.Add(f);
                                    if (f.RecipeNO != null && f.RecipeNO != string.Empty)//�Ѿ����䴦����
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        f.RecipeNO = recipeNO;
                                        f.SequenceNO = recipeSequence;
                                        recipeSequence++;
                                    }
                                }//{B24B174D-F261-4c6b-94C9-EEED0F736013}
                                if (!hs.Contains(tempRecipeNO))
                                {
                                    FS.FrameWork.Models.NeuObject obj = new NeuObject();
                                    obj.ID = recipeNO;
                                    obj.Name = recipeSequence.ToString();
                                    hs.Add(recipeNO, obj);
                                }
                            }


                        }
                    }
                }
                #endregion
            }
            return true;
        }
        /// <summary>
        /// ���[������ˮ��]�ʹ�����
        /// </summary>
        /// <param name="r"></param>
        /// <param name="feeItemLists"></param>
        /// <param name="recipeNO"></param>
        /// <param name="sequence"></param>
        public void GetRecipeNoAndMaxSeq(ArrayList feeItemLists, ref string recipeNO, ref int sequence, string clinicCode)
        {
            if (feeItemLists == null || feeItemLists.Count <= 0)
            {
                return;
            }

            foreach (FeeItemList feeItem in feeItemLists)
            {
                if (feeItem.RecipeNO != null && feeItem.RecipeNO.Length > 0)
                {
                    recipeNO = feeItem.RecipeNO;

                    sequence = NConvert.ToInt32(outpatientManager.GetMaxSeqByRecipeNO(recipeNO, clinicCode));

                    break;
                }
            }
        }
        #endregion

        #region ������ϸ����
        /// <summary>
        /// ���������ϸ
        /// </summary>
        /// <param name="feeItemList">������ϸʵ��</param>
        /// <returns>�ɹ�: 1 ʧ��: -1 û�в������ݷ��� 0</returns>
        public int InsertFeeItemList(FeeItemList feeItemList)
        {
            return this.UpdateSingleTable("Fee.Item.GetFeeItemDetail.Insert", this.GetFeeItemListParams(feeItemList));
        }
        /// <summary>
        /// ���·�����ϸ
        /// </summary>
        /// <param name="feeItemList">������ϸʵ��</param>
        /// <returns>�ɹ�: 1 ʧ��: -1 û�и��µ����ݷ��� 0</returns>
        private int UpdateFeeItemList(FeeItemList feeItemList)
        {
            return this.UpdateSingleTable("Fee.OutPatient.ItemDetail.Update", this.GetFeeItemListParams(feeItemList));
        }
        /// <summary>
        /// ���insert��Ĵ����������update
        /// </summary>
        /// <param name="feeItemList">������ϸʵ��</param>
        /// <returns>�ַ�������</returns>
        private string[] GetFeeItemListParams(FeeItemList feeItemList)
        {
            //{143CA424-7AF9-493a-8601-2F7B1D635027}
            string[] args = new string[86];	//{3AEB5613-1CB0-4158-89E6-F82F0B643388}				 

            args[0] = feeItemList.RecipeNO;//RECIPE_NO,	--		������							0
            args[1] = feeItemList.SequenceNO.ToString();	  //SEQUENCE_NO;	--		��������Ŀ��ˮ��				1
            args[2] = ((int)feeItemList.TransType).ToString();//TRANS_TYPE;	--		��������;1�����ף�2������		2
            args[3] = feeItemList.Patient.ID;//CLINIC_CODE;	--		�����								3	
            args[4] = feeItemList.Patient.PID.CardNO;//CARD_NO;	--		��������									4		
            args[5] = ((FS.HISFC.Models.Registration.Register)feeItemList.Patient).DoctorInfo.SeeDate.ToString();//REG_DATE;	--		�Һ�����						5	
            args[6] = ((FS.HISFC.Models.Registration.Register)feeItemList.Patient).DoctorInfo.Templet.Dept.ID;//REG_DPCD;	--		�Һſ���							6	
            args[7] = feeItemList.RecipeOper.ID;//DOCT_CODE;	--		����ҽʦ							7
            args[8] = feeItemList.RecipeOper.Dept.ID;//DOCT_DEPT;	--		����ҽʦ���ڿ���				8
            args[9] = feeItemList.Item.ID;//ITEM_CODE;	--		��Ŀ����									9.
            args[10] = feeItemList.Item.Name;//ITEM_NAME;	--		��Ŀ����									10
            //args[11] = NConvert.ToInt32(feeItemList.Item.IsPharmacy).ToString();//DRUG_FLAG;	--		1ҩƷ/0��Ҫ					11
            args[11] = ((int)(feeItemList.Item.ItemType)).ToString();
            args[12] = feeItemList.Item.Specs;//SPECS;		--		���										12
            //if (feeItemList.Item.IsPharmacy)
            if (feeItemList.Item.ItemType == EnumItemType.Drug)
            {
                args[13] = NConvert.ToInt32(((FS.HISFC.Models.Pharmacy.Item)feeItemList.Item).Product.IsSelfMade).ToString();//SELF_MADE;	--		����ҩ��־					13
                args[14] = ((FS.HISFC.Models.Pharmacy.Item)feeItemList.Item).Quality.ID;//DRUG_QUALITY;	--		ҩƷ���ʣ���ҩ����ҩ		14	
                args[15] = ((FS.HISFC.Models.Pharmacy.Item)feeItemList.Item).DosageForm.ID;//DOSE_MODEL_CODE;--		����							15.
            }
            args[16] = feeItemList.Item.MinFee.ID;//FEE_CODE;	--		��С���ô���							16	
            args[17] = feeItemList.Item.SysClass.ID.ToString();//CLASS_CODE;	--		ϵͳ���				17	
            args[18] = feeItemList.Item.Price.ToString();//UNIT_PRICE;	--		����							18	
            args[19] = feeItemList.Item.Qty.ToString();//QTY;		--		����								19	
            args[20] = feeItemList.Days.ToString();//DAYS;		--		��ҩ�ĸ���������ҩƷΪ1			20	
            args[21] = feeItemList.Order.Frequency.ID;//FREQUENCY_CODE;	--		Ƶ�δ���						21	
            args[22] = feeItemList.Order.Usage.ID;//USAGE_CODE;	--		�÷�����							22	
            args[23] = feeItemList.Order.Usage.Name;//USE_NAME;	--		�÷�����							23
            args[24] = feeItemList.InjectCount.ToString();//INJECT_NUMBER;	--		Ժ��ע�����		24	
            args[25] = NConvert.ToInt32(feeItemList.IsUrgent).ToString();//EMC_FLAG;	--		�Ӽ����:1�Ӽ�/0��ͨ			25	
            args[26] = feeItemList.Order.Sample.ID;//LAB_TYPE;	--		��������							26	
            args[27] = feeItemList.Order.CheckPartRecord;//CHECK_BODY;	--		����								27	
            args[28] = feeItemList.Order.DoseOnce.ToString();//DOSE_ONCE;	--		ÿ������					28
            args[29] = feeItemList.Order.DoseUnit;//DOSE_UNIT;	--		ÿ��������λ							29
            //if (feeItemList.Item.IsPharmacy)
            if (feeItemList.Item.ItemType == EnumItemType.Drug)
            {
                args[30] = ((FS.HISFC.Models.Pharmacy.Item)feeItemList.Item).BaseDose.ToString();//BASE_DOSE;	--		��������					30
            }
            args[31] = feeItemList.Item.PackQty.ToString();//PACK_QTY;	--		��װ����						31	
            args[32] = feeItemList.Item.PriceUnit;//PRICE_UNIT;	--		�Ƽ۵�λ							32	
            args[33] = feeItemList.FT.PubCost.ToString();//PUB_COST;	--		�ɱ�Ч���				33	
            args[34] = feeItemList.FT.PayCost.ToString();//PAY_COST;	--		�Ը����				34	
            args[35] = feeItemList.FT.OwnCost.ToString();//OWN_COST;	--		�ֽ���				35	
            args[36] = feeItemList.ExecOper.Dept.ID;//EXEC_DPCD;	--		ִ�п��Ҵ���					36
            args[37] = feeItemList.ExecOper.Dept.Name;//EXEC_DPNM;	--		ִ�п�������					37
            args[38] = feeItemList.Compare.CenterItem.ID;//CENTER_CODE;	--		ҽ��������Ŀ����				38	
            args[39] = feeItemList.Compare.CenterItem.ItemGrade;//ITEM_GRADE;	--		��Ŀ�ȼ�1����2����3����		39	
            args[40] = NConvert.ToInt32(feeItemList.Order.Combo.IsMainDrug).ToString();//MAIN_DRUG;	--		��ҩ��־					40
            args[41] = feeItemList.Order.Combo.ID;//COMB_NO;	--		��Ϻ�										41	
            args[42] = feeItemList.ChargeOper.ID;//OPER_CODE;	--		������							42
            args[43] = feeItemList.ChargeOper.OperTime.ToString();//OPER_DATE;	--		����ʱ��					43
            args[44] = ((int)feeItemList.PayType).ToString();// //PAY_FLAG;	--		�շѱ�־��1δ�շѣ�2�շ�	44	
            args[45] = ((int)feeItemList.CancelType).ToString();
            args[46] = feeItemList.FeeOper.ID;//FEE_CPCD;	--		�շ�Ա����							46	
            args[47] = feeItemList.FeeOper.OperTime.ToString();//FEE_DATE;	--		�շ�����					47	
            args[48] = feeItemList.Invoice.ID;//INVOICE_NO;	--		Ʊ�ݺ�								48	
            args[49] = feeItemList.FeeCodeStat.ID;//INVO_CODE;	--		��Ʊ��Ŀ����				49
            args[50] = feeItemList.FeeCodeStat.SortID.ToString();//INVO_SEQUENCE;	--		��Ʊ����ˮ��		50
            args[51] = NConvert.ToInt32(feeItemList.IsConfirmed).ToString();//CONFIRM_FLAG;	--		1δȷ��/2ȷ��				51		
            args[52] = feeItemList.ConfirmOper.ID;//CONFIRM_CODE;	--		ȷ����						52		
            args[53] = feeItemList.ConfirmOper.Dept.ID;//CONFIRM_DEPT;	--		ȷ�Ͽ���					53	
            args[54] = feeItemList.ConfirmOper.OperTime.ToString();//CONFIRM_DATE;	--		ȷ��ʱ��				54	
            args[55] = feeItemList.FT.RebateCost.ToString();// ECO_COST -- �Żݽ�� 55
            args[56] = feeItemList.InvoiceCombNO;//��Ʊ��ţ�һ�ν���������ŷ�Ʊ��combNo  56
            args[57] = feeItemList.NewItemRate.ToString();//����Ŀ����  57
            args[58] = feeItemList.OrgItemRate.ToString();//ԭ��Ŀ����  58 
            args[59] = feeItemList.ItemRateFlag;//��չ��־ ������Ŀ��־ 1�Է� 2 ���� 3 ����  59
            args[60] = feeItemList.UndrugComb.ID;
            args[61] = feeItemList.UndrugComb.Name;
            args[62] = feeItemList.Item.SpecialFlag1;
            args[63] = feeItemList.Item.SpecialFlag2;
            args[64] = feeItemList.FeePack;
            args[65] = feeItemList.NoBackQty.ToString();
            args[66] = feeItemList.ConfirmedQty.ToString();
            args[67] = feeItemList.ConfirmedInjectCount.ToString();
            args[68] = feeItemList.Order.ID;
            args[69] = feeItemList.RecipeSequence;
            args[70] = feeItemList.SpecialPrice.ToString();
            args[71] = feeItemList.FT.ExcessCost.ToString();
            args[72] = feeItemList.FT.DrugOwnCost.ToString();
            args[73] = feeItemList.FTSource;
            args[74] = NConvert.ToInt32(feeItemList.Item.IsMaterial).ToString();
            args[75] = NConvert.ToInt32(feeItemList.IsAccounted).ToString();
            //���ʳ�����ˮ��
            args[76] = NConvert.ToInt32(feeItemList.UpdateSequence).ToString();
            //����ҽ����������
            args[77] = feeItemList.DoctDeptInfo.ID.ToString();
            args[78] = feeItemList.MedicalGroupCode.ID.ToString();
            if (string.IsNullOrEmpty(feeItemList.Order.Patient.Pact.ID))
            {
                args[79] = feeItemList.Patient.Pact.PayKind.ID;
                args[80] = feeItemList.Patient.Pact.ID;
            }
            else
            {
                args[79] = feeItemList.Order.Patient.Pact.PayKind.ID;
                args[80] = feeItemList.Order.Patient.Pact.ID;
            }

            args[81] = feeItemList.OrgPrice.ToString();
            args[82] = feeItemList.UndrugComb.Qty.ToString();
            args[83] = feeItemList.Order.Memo;//������ע
            args[84] = feeItemList.Memo;//���ñ�ע
            args[85] = feeItemList.FT.FTRate.User03;//Extflag3
            //�������
            //args[79] = feeItemList.SeeNo;

            return args;
        }
        #endregion 

        /// <summary>
        /// ������ϸ����У��
        /// </summary>
        /// <param name="f">����ʵ��</param>
        /// <param name="errText">������Ϣ</param>
        /// <returns>�ɹ� true ʧ�� false</returns>
        public bool IsFeeItemListDataValid(FeeItemList f, ref string errText)
        {
            string itemName = f.Item.Name;
            if (f == null)
            {
                errText = itemName + "��÷���ʵ�����!";

                return false;
            }
            if (f.Item.ID == null || f.Item.ID == string.Empty)
            {
                errText = itemName + "��Ŀ����û�и�ֵ";

                return false;
            }
            if (f.Item.Name == null || f.Item.Name == string.Empty)
            {
                errText = itemName + "��Ŀ����û�и�ֵ";

                return false;
            }
            //if (f.Item.IsPharmacy)
            if (f.Item.ItemType == EnumItemType.Drug && f.FTSource != "0")
            {
                #region ���ݲ���&& !isDoseOnceCanNull ���ж��Ƿ���Ҫ�������ֵ ����ǿ20070828
                if ((f.Order.Frequency.ID == null || f.Order.Frequency.ID == string.Empty) && (f.FTSource == "1" || (f.FTSource == "0" && !isDoseOnceCanNull)))
                {
                    errText = itemName + "Ƶ�δ���û�и�ֵ";

                    return false;
                }
                if ((f.Order.Usage.ID == null || f.Order.Usage.ID == string.Empty) && (f.FTSource == "1" || (f.FTSource == "0" && !isDoseOnceCanNull)))
                {
                    errText = itemName + "�÷�����û�и�ֵ";

                    return false;
                }
                if (f.Order.DoseOnce == 0 && (f.FTSource == "1" || (f.FTSource == "0" && !isDoseOnceCanNull)))
                {
                    errText = itemName + "ÿ������û�и�ֵ";

                    return false;
                }
                if ((f.Order.DoseUnit == null || f.Order.DoseUnit == string.Empty) && (f.FTSource == "1" || (f.FTSource == "0" && !isDoseOnceCanNull)))
                {
                    errText = itemName + "ÿ��������λû�и�ֵ";

                    return false;
                }
                #endregion
            }

            if (f.Item.ID != "999")
            {
                if (f.Item.ItemType == EnumItemType.Drug)
                {
                    //if (f.Item.Specs == null || f.Item.Specs == string.Empty)
                    //{
                    //    errText = itemName + "�Ĺ��û�и�ֵ";

                    //    return false;
                    //}
                    if (f.Item.PackQty == 0)
                    {
                        errText = itemName + "��װ����û�и�ֵ";

                        return false;
                    }
                }
            }
            if (f.Item.PriceUnit == null || f.Item.PriceUnit == string.Empty)
            {
                errText = itemName + "�Ƽ۵�λû�и�ֵ";

                return false;
            }

            if (f.Item.MinFee.ID == null || f.Item.MinFee.ID == string.Empty)
            {
                errText = itemName + "��С����û�и�ֵ";

                return false;
            }
            if (f.Item.SysClass.ID == null || f.Item.SysClass.Name == string.Empty)
            {
                errText = itemName + "ϵͳ���û�и�ֵ";

                return false;
            }
            if (f.Item.Qty == 0)
            {
                errText = itemName + "����û�и�ֵ";

                return false;
            }
            //����������ô�����ʱ����{DE54BEAE-EF40-4aa4-8DF5-8CCB2A3DDA1D}
            //if (f.Item.Qty < 0)
            //{
            //    errText = itemName + "��������С��0";

            //    return false;
            //}
            if (f.Item.Qty > 99999)
            {
                errText = itemName + "�������ܴ���99999";

                return false;
            }

            if (f.Days == 0)
            {
                errText = itemName + "��ҩ����û�и�ֵ";

                return false;
            }
            if (f.Days < 0)
            {
                errText = itemName + "��ҩ��������С��0";

                return false;
            }

            if (f.Item.Price < 0)
            {
                errText = itemName + "���۲���С��0";

                return false;
            }

            //�����Ա�ҩ�� ������ȡ����Ϊ0��Ŀ
            if (f.Item.ID != "999")
            {
                if (f.Item.Price == 0 && f.Item.User03 != "ȫ��")
                {
                    errText = itemName + "����û�и�ֵ";

                    return false;
                }
                //if (f.FT.OwnCost + f.FT.PayCost + f.FT.PubCost == 0)
                //{
                //    errText = itemName + "��Ŀ���û�и�ֵ";

                //    return false;
                //}
            }

            //����������ô�����ʱ����{DE54BEAE-EF40-4aa4-8DF5-8CCB2A3DDA1D}
            //if (f.FT.OwnCost + f.FT.PayCost + f.FT.PubCost < 0)
            //{
            //    errText = itemName + "��Ŀ���Ϊ��";

            //    return false;
            //}
            ////{8DF48FD8-14E9-464a-A368-256B19A0EE54} �޸��ֻ����
            //if (FS.FrameWork.Public.String.FormatNumber(f.Item.Price * f.Item.Qty / f.Item.PackQty, 2) != FS.FrameWork.Public.String.FormatNumber
            //    (f.FT.OwnCost + f.FT.PayCost + f.FT.PubCost /*+ f.FT.RebateCost*/, 2))
            //{
            //    errText = itemName + "����뵥����������";

            //    return false;
            //}

            if (f.Item.ID == "999" && f.Item.ItemType == EnumItemType.Drug)
            {
            }
            else
            {
                if (f.ExecOper.Dept.ID == null || f.ExecOper.Dept.ID == string.Empty)
                {
                    errText = itemName + "ִ�п��Ҵ���û�и�ֵ";

                    return false;
                }
                if (f.ExecOper.Dept.Name == null || f.ExecOper.Dept.Name == string.Empty)
                {
                    errText = itemName + "ִ�п�������û�и�ֵ";

                    return false;
                }
            }

            return true;
        }
        /// <summary>
        /// ��÷�ҩƷ��Ϣ�������Ƿ���Ч��
        /// </summary>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public FS.HISFC.Models.Fee.Item.Undrug GetItem(string itemCode)
        {
            return itemManager.GetItemByUndrugCode(itemCode);
        }

        #region ��ȡ�����Ŀ��ϸ��Ϣ
        /// <summary>
        /// ���ݴ����ź���Ŀ��ˮ�Ż����Ŀ��ϸʵ��(�Ѿ��շ���Ϣ)
        /// </summary>
        /// <param name="recipeNO">������</param>
        /// <param name="sequenceNO">��������ˮ��</param>
        /// <returns>�ɹ�:������ϸʵ�� ʧ�ܻ���û������:null</returns>
        public FeeItemList GetFeeItemListBalanced(string recipeNO, int sequenceNO)
        {
            ArrayList feeItemLists = this.QueryFeeItemLists("Fee.Item.GetDrugItemList.WhereFeed", recipeNO, sequenceNO.ToString());

            if (feeItemLists == null)
            {
                return null;
            }

            if (feeItemLists.Count > 0)
            {
                foreach (FeeItemList f in feeItemLists)
                {
                    if (f.CancelType == CancelTypes.Valid)
                    {
                        return f;
                    }
                }
            }
            else
            {
                return null;
            }

            return null;
        }
        /// <summary>
        /// ���ݽ������л�÷�����ϸ
        /// </summary>
        /// <param name="invoiceSequence"></param>
        /// <returns></returns>
        public ArrayList QueryFeeItemListsByInvoiceSequence(string invoiceSequence)
        {
            return this.QueryFeeItemLists("Fee.OutPatient.GetInvoInfo.Where.Seq", invoiceSequence);
        }
        /// <summary>
        /// ����Where������������ѯ������ϸ��Ϣ
        /// </summary>
        /// <param name="whereIndex">Where��������</param>
        /// <param name="args">����</param>
        /// <returns>�ɹ�:������ϸ ʧ��:null û������:����Ԫ����Ϊ0��ArrayList</returns>
        private ArrayList QueryFeeItemLists(string whereIndex, params string[] args)
        {
            string sql = string.Empty;//SELECT���
            string where = string.Empty;//WHERE���

            //���Where���
            if (this.Sql.GetCommonSql(whereIndex, ref where) == -1)
            {
                this.Err = "û���ҵ�����Ϊ:" + whereIndex + "��SQL���";

                return null;
            }

            sql = this.GetSqlFeeDetail();

            return this.QueryFeeDetailBySql(sql + " " + where, args);
        }
        
        #region ��ϸ����
        /// <summary>
        /// ��������ϸ��sql���
        /// </summary>
        /// <returns>���ز�ѯ������ϸSQL���</returns>
        private string GetSqlFeeDetail()
        {
            string sql = string.Empty;//��ѯSQL����SELECT����

            if (this.Sql.GetCommonSql("Fee.Item.GetFeeItem", ref sql) == -1)
            {
                this.Err = "û���ҵ�����ΪFee.Item.GetFeeItem��SQL���";

                return null;
            }

            return sql;
        }
        /// <summary>
        /// ͨ��SQL����÷�����ϸ��Ϣ
        /// </summary>
        /// <param name="sql">SQL���</param>
        /// <param name="args">SQL����</param>
        /// <returns>�ɹ�:������ϸ���� ʧ��: null û�в��ҵ�����: Ԫ����Ϊ0��ArrayList</returns>
        private ArrayList QueryFeeDetailBySql(string sql, params string[] args)
        {
            if (this.ExecQuery(sql, args) == -1)
            {
                return null;
            }

            ArrayList feeItemLists = new ArrayList();//������ϸ����
            FeeItemList feeItemList = null;//������ϸʵ��

            try
            {
                //ѭ����ȡ����
                while (this.Reader.Read())
                {
                    feeItemList = new FeeItemList();

                    //feeItemList.Item.IsPharmacy = NConvert.ToBoolean(this.Reader[11].ToString());

                    feeItemList.Item.ItemType = (EnumItemType)NConvert.ToInt32(this.Reader[11]);

                    //if (feeItemList.Item.IsPharmacy)
                    if (feeItemList.Item.ItemType == EnumItemType.Drug)
                    {
                        feeItemList.Item = new FS.HISFC.Models.Pharmacy.Item();
                        feeItemList.Item.ItemType = EnumItemType.Drug;
                        //feeItemList.Item.IsPharmacy = true;
                    }
                    //{40DFDC91-0EC1-4cd4-81BC-0EAE4DE1D3AB}
                    else if (feeItemList.Item.ItemType == EnumItemType.UnDrug)
                    {
                        feeItemList.Item = new FS.HISFC.Models.Fee.Item.Undrug();
                        //feeItemList.Item.IsPharmacy = false;
                        feeItemList.Item.ItemType = EnumItemType.UnDrug;
                    }
                    //���� {40DFDC91-0EC1-4cd4-81BC-0EAE4DE1D3AB}
                    else
                    {
                        feeItemList.Item = new FS.HISFC.Models.FeeStuff.MaterialItem();
                        feeItemList.Item.ItemType = EnumItemType.MatItem;

                    }

                    feeItemList.RecipeNO = this.Reader[0].ToString();
                    feeItemList.SequenceNO = NConvert.ToInt32(this.Reader[1].ToString());
                    if (this.Reader[2].ToString() == "1")
                    {
                        feeItemList.TransType = TransTypes.Positive;
                    }
                    else
                    {
                        feeItemList.TransType = TransTypes.Negative;
                    }
                    feeItemList.Patient.ID = this.Reader[3].ToString();
                    feeItemList.Patient.PID.CardNO = this.Reader[4].ToString();
                    ((FS.HISFC.Models.Registration.Register)feeItemList.Patient).DoctorInfo.SeeDate = NConvert.ToDateTime(this.Reader[5].ToString());
                    ((FS.HISFC.Models.Registration.Register)feeItemList.Patient).DoctorInfo.Templet.Dept.ID = this.Reader[6].ToString();
                    feeItemList.RecipeOper.ID = this.Reader[7].ToString();
                    ((FS.HISFC.Models.Registration.Register)feeItemList.Patient).DoctorInfo.Templet.Doct.ID = this.Reader[7].ToString();
                    feeItemList.RecipeOper.Dept.ID = this.Reader[8].ToString();
                    feeItemList.Item.ID = this.Reader[9].ToString();
                    feeItemList.Item.Name = this.Reader[10].ToString();
                    feeItemList.Item.Specs = this.Reader[12].ToString();

                    //if (feeItemList.Item.IsPharmacy)
                    if (feeItemList.Item.ItemType == EnumItemType.Drug)
                    {
                        ((FS.HISFC.Models.Pharmacy.Item)feeItemList.Item).Product.IsSelfMade = NConvert.ToBoolean(this.Reader[13].ToString());
                        ((FS.HISFC.Models.Pharmacy.Item)feeItemList.Item).Quality.ID = this.Reader[14].ToString();
                        ((FS.HISFC.Models.Pharmacy.Item)feeItemList.Item).DosageForm.ID = this.Reader[15].ToString();
                    }
                    feeItemList.Item.MinFee.ID = this.Reader[16].ToString();
                    feeItemList.Item.SysClass.ID = this.Reader[17].ToString();
                    feeItemList.Item.Price = NConvert.ToDecimal(this.Reader[18].ToString());
                    feeItemList.Item.Qty = NConvert.ToDecimal(this.Reader[19].ToString());
                    feeItemList.Days = NConvert.ToDecimal(this.Reader[20].ToString());
                    feeItemList.Order.Frequency.ID = this.Reader[21].ToString();
                    feeItemList.Order.Usage.ID = this.Reader[22].ToString();
                    feeItemList.Order.Usage.Name = this.Reader[23].ToString();
                    feeItemList.InjectCount = NConvert.ToInt32(this.Reader[24].ToString());
                    feeItemList.IsUrgent = NConvert.ToBoolean(this.Reader[25].ToString());
                    feeItemList.Order.Sample.ID = this.Reader[26].ToString();
                    feeItemList.Order.CheckPartRecord = this.Reader[27].ToString();
                    feeItemList.Order.DoseOnce = NConvert.ToDecimal(this.Reader[28].ToString());
                    feeItemList.Order.DoseUnit = this.Reader[29].ToString();
                    //if (feeItemList.Item.IsPharmacy)
                    if (feeItemList.Item.ItemType == EnumItemType.Drug)
                    {
                        ((FS.HISFC.Models.Pharmacy.Item)feeItemList.Item).BaseDose = NConvert.ToDecimal(this.Reader[30].ToString());
                    }
                    feeItemList.Item.PackQty = NConvert.ToDecimal(this.Reader[31].ToString());
                    feeItemList.Item.PriceUnit = this.Reader[32].ToString();
                    feeItemList.FT.PubCost = NConvert.ToDecimal(this.Reader[33].ToString());
                    feeItemList.FT.PayCost = NConvert.ToDecimal(this.Reader[34].ToString());
                    feeItemList.FT.OwnCost = NConvert.ToDecimal(this.Reader[35].ToString());
                    feeItemList.ExecOper.Dept.ID = this.Reader[36].ToString();
                    feeItemList.ExecOper.Dept.Name = this.Reader[37].ToString();
                    feeItemList.Compare.CenterItem.ID = this.Reader[38].ToString();
                    feeItemList.Compare.CenterItem.ItemGrade = this.Reader[39].ToString();
                    feeItemList.Order.Combo.IsMainDrug = NConvert.ToBoolean(this.Reader[40].ToString());
                    feeItemList.Order.Combo.ID = this.Reader[41].ToString();
                    feeItemList.ChargeOper.ID = this.Reader[42].ToString();
                    feeItemList.ChargeOper.OperTime = NConvert.ToDateTime(this.Reader[43].ToString());
                    feeItemList.PayType = (PayTypes)(NConvert.ToInt32(this.Reader[44].ToString()));
                    feeItemList.CancelType = (CancelTypes)(NConvert.ToInt32(this.Reader[45].ToString()));
                    feeItemList.FeeOper.ID = this.Reader[46].ToString();
                    feeItemList.FeeOper.OperTime = NConvert.ToDateTime(this.Reader[47].ToString());
                    feeItemList.Invoice.ID = this.Reader[48].ToString();
                    feeItemList.Invoice.Type.ID = this.Reader[49].ToString();
                    feeItemList.FeeCodeStat.ID = this.Reader[49].ToString();
                    feeItemList.FeeCodeStat.SortID = NConvert.ToInt32(this.Reader[50].ToString());
                    feeItemList.IsConfirmed = NConvert.ToBoolean(this.Reader[51].ToString());
                    feeItemList.ConfirmOper.ID = this.Reader[52].ToString();
                    feeItemList.ConfirmOper.Dept.ID = this.Reader[53].ToString();
                    feeItemList.ConfirmOper.OperTime = NConvert.ToDateTime(this.Reader[54].ToString());

                    //�ۿ����
                    feeItemList.StockOper.Dept.ID = feeItemList.ConfirmOper.Dept.ID;//�ۿ����

                    feeItemList.InvoiceCombNO = this.Reader[55].ToString();
                    feeItemList.NewItemRate = NConvert.ToDecimal(this.Reader[56].ToString());
                    feeItemList.OrgItemRate = NConvert.ToDecimal(this.Reader[57].ToString());
                    feeItemList.ItemRateFlag = this.Reader[58].ToString();
                    feeItemList.Item.SpecialFlag1 = this.Reader[59].ToString();
                    feeItemList.Item.SpecialFlag2 = this.Reader[60].ToString();
                    feeItemList.FeePack = this.Reader[61].ToString();
                    feeItemList.UndrugComb.ID = this.Reader[62].ToString();
                    feeItemList.UndrugComb.Name = this.Reader[63].ToString();
                    feeItemList.NoBackQty = NConvert.ToDecimal(this.Reader[64].ToString());
                    feeItemList.ConfirmedQty = NConvert.ToDecimal(this.Reader[65].ToString());
                    feeItemList.ConfirmedInjectCount = NConvert.ToInt32(this.Reader[66].ToString());
                    feeItemList.Order.ID = this.Reader[67].ToString();
                    feeItemList.RecipeSequence = this.Reader[68].ToString();
                    feeItemList.FT.RebateCost = NConvert.ToDecimal(this.Reader[69].ToString());
                    feeItemList.SpecialPrice = NConvert.ToDecimal(this.Reader[70].ToString());
                    feeItemList.FT.ExcessCost = NConvert.ToDecimal(this.Reader[71].ToString());
                    feeItemList.FT.DrugOwnCost = NConvert.ToDecimal(this.Reader[72].ToString());
                    feeItemList.FTSource = this.Reader[73].ToString();
                    feeItemList.Item.IsMaterial = NConvert.ToBoolean(this.Reader[74].ToString());
                    feeItemList.IsAccounted = NConvert.ToBoolean(this.Reader[75].ToString());
                    //{143CA424-7AF9-493a-8601-2F7B1D635026}
                    //���ʳ�����ˮ��
                    feeItemList.UpdateSequence = NConvert.ToInt32(this.Reader[76].ToString());

                    //�ж�77����������Ƿ����
                    if (this.Reader.FieldCount > 78)
                    {
                        feeItemList.Order.Patient.Pact.PayKind.ID = this.Reader[77].ToString();
                        feeItemList.Order.Patient.Pact.ID = this.Reader[78].ToString();
                    }

                    if (this.Reader.FieldCount > 82)
                    {
                        feeItemList.OrgPrice = NConvert.ToDecimal(this.Reader[79]);
                        feeItemList.UndrugComb.Qty = NConvert.ToDecimal(this.Reader[80]);
                        feeItemList.Order.Memo = this.Reader[81].ToString();
                        feeItemList.Memo = this.Reader[82].ToString();
                    }

                    if (this.Reader.FieldCount > 84)
                    {
                        feeItemList.DoctDeptInfo.ID = this.Reader[83].ToString();
                        feeItemList.MedicalGroupCode.ID = this.Reader[84].ToString();
                    }

                    if (this.Reader.FieldCount > 85)
                    {
                        feeItemList.FT.FTRate.User03 = this.Reader[85].ToString();
                    }

                    feeItemLists.Add(feeItemList);
                }//ѭ������

                this.Reader.Close();

                return feeItemLists;
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                this.WriteErr();

                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }

                return null;
            }
        }
        #endregion
        #endregion
 
        /// <summary>
        /// ����ԭʼ��Ʊ�Ÿ��·�����ϸ����Ч��־
        /// </summary>
        /// <param name="orgInvoiceNO">ԭʼ��Ʊ��</param>
        /// <param name="operTime">����ʱ��</param>
        /// <param name="cancelType">��������</param>
        /// <returns>�ɹ�; >= 1 ʧ��: -1 û�и��µ�����: 0</returns>
        public int UpdateFeeItemListCancelType(string orgInvoiceNO, DateTime operTime, CancelTypes cancelType)
        {
            return this.UpdateSingleTable("Fee.OutPatient.UpdateFeeDetailCancelFlag.1", orgInvoiceNO, operTime.ToString(), ((int)cancelType).ToString());
        }
        /// <summary>
        /// �����շѺ���
        /// 
        /// {A87CA138-33E8-47d0-92BD-7A65A08DDCF5}
        /// 
        /// </summary>
        /// <param name="type">�շ�,���۱�־</param>
        /// <param name="r">���߹ҺŻ�����Ϣ</param>
        /// <param name="invoices">��Ʊ������</param>
        /// <param name="invoiceDetails">��Ʊ��ϸ����</param>
        /// <param name="feeDetails">������ϸ����</param>
        /// <param name="t">Transcation</param>
        /// <param name="payModes">֧����ʽ����</param>
        /// <param name="errText">������Ϣ</param>
        /// <returns></returns>
        public bool ClinicFee(FS.HISFC.Models.Base.ChargeTypes type, FS.HISFC.Models.Registration.Register r,
            ArrayList invoices, ArrayList invoiceDetails, ArrayList feeDetails, ArrayList invoiceFeeDetails, ArrayList payModes, ref string errText)
        {
            Employee oper = this.outpatientManager.Operator as Employee;
            return this.ClinicFee(type, "C", false, r, invoices, invoiceDetails, feeDetails, invoiceFeeDetails, payModes, ref errText, oper);
        }
        /// <summary>
        /// �����շѺ���
        /// 
        /// {69245A77-FB7A-42ed-844B-855E7ABC612F}
        /// 
        /// </summary>
        /// <param name="type">�շ�,���۱�־</param>
        /// <param name="isTempInvoice">�Ƿ�ʹ����ʱ��Ʊ</param>
        /// <param name="r">���߹ҺŻ�����Ϣ</param>
        /// <param name="invoices">��Ʊ������</param>
        /// <param name="invoiceDetails">��Ʊ��ϸ����</param>
        /// <param name="feeDetails">������ϸ����</param>
        /// <param name="invoiceFeeDetails">��Ʊ��ϸ��Ϣ</param>
        /// <param name="payModes">֧����ʽ����</param>
        /// <param name="errText">������Ϣ</param>
        /// <returns></returns>
        public bool ClinicFee(FS.HISFC.Models.Base.ChargeTypes type, bool isTempInvoice, FS.HISFC.Models.Registration.Register r,
            ArrayList invoices, ArrayList invoiceDetails, ArrayList feeDetails, ArrayList invoiceFeeDetails, ArrayList payModes, ref string errText)
        {
            Employee oper = this.outpatientManager.Operator as Employee;
            return this.ClinicFee(type, "C", isTempInvoice, r, invoices, invoiceDetails, feeDetails, invoiceFeeDetails, payModes, ref errText, oper);
        }
        /// <summary>
        /// �����շѺ���
        /// 
        /// {A87CA138-33E8-47d0-92BD-7A65A08DDCF5} 
        /// ���Ӳ���������ָ����Ʊ����
        /// </summary>
        /// <param name="type">�շ�,���۱�־</param>
        /// <param name="invoiceType">��Ʊ����R:�Һ��վ� C:�����վ� P:Ԥ���վ� I:סԺ�վ� A:�����˻�</param>
        /// <param name="isTempInvoice">�Ƿ�ʹ����ʱ��Ʊ</param>
        /// <param name="r">���߹ҺŻ�����Ϣ</param>
        /// <param name="invoices">��Ʊ������</param>
        /// <param name="invoiceDetails">��Ʊ��ϸ����</param>
        /// <param name="feeDetails">������ϸ����</param>
        /// <param name="invoiceFeeDetails">��Ʊ��ϸ��Ϣ</param>
        /// <param name="payModes">֧����ʽ����</param>
        /// <param name="errText">������Ϣ</param>
        /// <param name="oper">������</param>
        /// <returns></returns>
        public bool ClinicFee(FS.HISFC.Models.Base.ChargeTypes type, string invoiceType, bool isTempInvoice, FS.HISFC.Models.Registration.Register r,
            ArrayList invoices, ArrayList invoiceDetails, ArrayList feeDetails, ArrayList invoiceFeeDetails, ArrayList payModes, ref string errText, Employee oper)
        {

            //Terminal.Confirm confirmIntegrate = new FS.HISFC.BizProcess.Integrate.Terminal.Confirm();
            FS.HISFC.BizProcess.Integrate.Terminal.Booking bookingIntegrate = new FS.HISFC.BizProcess.Integrate.Terminal.Booking();
            //SOC.HISFC.BizLogic.Pharmacy.Item socItemMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.Item();

            invoiceStytle = controlParamIntegrate.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.Const.GET_INVOICE_NO_TYPE, false, "0");

            isDoseOnceCanNull = controlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.Const.DOSE_ONCE_NULL, false, true);

            //�Ƿ�ŷ�Э������
            bool isSplitNostrum = controlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.Const.Split_NostrumDetail, false, false);

            //����շ�ʱ��
            DateTime feeTime = inpatientManager.GetDateTimeFromSysDateTime();

            //����շѲ���Ա
            string operID = inpatientManager.Operator.ID;

            FS.HISFC.Models.Base.Employee employee = inpatientManager.Operator as FS.HISFC.Models.Base.Employee;
            //����ֵ
            int iReturn = 0;
            //���崦����
            string recipeNO = string.Empty;

            //������շѣ���÷�Ʊ��Ϣ
            if (type == FS.HISFC.Models.Base.ChargeTypes.Fee)//�շ�
            {
                #region �շ�����
                //��Ʊ�Ѿ���Ԥ������������,ֱ�Ӳ���Ϳ�����.

                #region//��÷�Ʊ����,���ŷ�Ʊ��Ʊ�Ų�ͬ

                string invoiceCombNO = outpatientManager.GetInvoiceCombNO();
                if (invoiceCombNO == null || invoiceCombNO == string.Empty)
                {
                    errText = "��÷�Ʊ��ˮ��ʧ��!" + outpatientManager.Err;

                    return false;
                }
                //���������ʾ���
                /////GetSpDisplayValue(myCtrl, t);
                //��һ����Ʊ��
                string mainInvoiceNO = string.Empty;
                string mainInvoiceCombNO = string.Empty;
                foreach (Balance balance in invoices)
                {
                    //����Ʊ��Ϣ,������ֻ����ʾ��
                    if (balance.Memo == "5")
                    {
                        mainInvoiceNO = balance.ID;

                        continue;
                    }

                    //�Էѻ��߲���Ҫ��ʾ����Ʊ,��ôȡ��һ����Ʊ����Ϊ����Ʊ��
                    if (mainInvoiceNO == string.Empty)
                    {
                        mainInvoiceNO = balance.Invoice.ID;
                        mainInvoiceCombNO = balance.CombNO;
                    }
                }

                #endregion

                #region //���뷢Ʊ��ϸ��

                foreach (ArrayList tempsInvoices in invoiceDetails)
                {
                    foreach (ArrayList tempDetals in tempsInvoices)
                    {
                        foreach (BalanceList balanceList in tempDetals)
                        {
                            //�ܷ�Ʊ����
                            if (balanceList.Memo == "5")
                            {
                                continue;
                            }
                            if (string.IsNullOrEmpty(((Balance)balanceList.BalanceBase).CombNO))
                            {
                                ((Balance)balanceList.BalanceBase).CombNO = invoiceCombNO;
                            }
                            balanceList.BalanceBase.BalanceOper.ID = operID;
                            balanceList.BalanceBase.BalanceOper.OperTime = feeTime;
                            balanceList.BalanceBase.IsDayBalanced = false;
                            balanceList.BalanceBase.CancelType = CancelTypes.Valid;
                            balanceList.ID = balanceList.ID.PadLeft(12, '0');

                            //���뷢Ʊ��ϸ�� fin_opb_invoicedetail
                            iReturn = outpatientManager.InsertBalanceList(balanceList);
                            if (iReturn == -1)
                            {
                                errText = "���뷢Ʊ��ϸ����!" + outpatientManager.Err;

                                return false;
                            }
                        }
                    }
                }

                #endregion

                #region Э������
                ArrayList noSplitDrugList = new ArrayList();
                if (isSplitNostrum)
                {

                    if (SplitNostrumDetail(r, ref feeDetails, ref noSplitDrugList, ref errText) < 0)
                    {
                        return false;
                    }
                }

                #endregion

                #region//ҩƷ��Ϣ�б�,���ɴ�����

                ArrayList drugLists = new ArrayList();
                //�������ɴ�����,������д�����,��ϸ�����¸�ֵ.
                if (!this.SetRecipeNOOutpatient(r, feeDetails, ref errText))
                {
                    return false;
                }

                #endregion

                #region//���������ϸ
                foreach (FeeItemList f in feeDetails)
                {
                    //��֤����
                    if (!this.IsFeeItemListDataValid(f, ref errText))
                    {
                        return false;
                    }
                    //���û�д�����,���¸�ֵ
                    if (f.RecipeNO == null || f.RecipeNO == string.Empty)
                    {
                        if (recipeNO == string.Empty)
                        {
                            recipeNO = outpatientManager.GetRecipeNO();
                            if (recipeNO == null || recipeNO == string.Empty)
                            {
                                errText = "��ô����ų���!";

                                return false;
                            }
                        }
                    }

                    #region 2007-8-29 liuq �ж��Ƿ����з�Ʊ����ţ�û����ֵ
                    //{1A5CC61F-01F9-4dee-A6A8-580200C10EB4}
                    if (string.IsNullOrEmpty(f.InvoiceCombNO) || f.InvoiceCombNO == "NULL")
                    {
                        f.InvoiceCombNO = invoiceCombNO;
                    }
                    #endregion
                    //
                    #region 2007-8-28 liuq �ж��Ƿ����з�Ʊ�ţ�û�г�ʼ��Ϊ12��0
                    if (string.IsNullOrEmpty(f.Invoice.ID))
                    {
                        f.Invoice.ID = mainInvoiceNO.PadLeft(12, '0');
                    }
                    #endregion
                    f.FeeOper.ID = operID;
                    f.FeeOper.OperTime = feeTime;
                    f.PayType = FS.HISFC.Models.Base.PayTypes.Balanced;
                    f.TransType = TransTypes.Positive;
                    f.Patient.PID.CardNO = r.PID.CardNO;

                    //f.Patient = r.Clone();
                    ((FS.HISFC.Models.Registration.Register)f.Patient).DoctorInfo.SeeDate = r.DoctorInfo.SeeDate;
                    if (((FS.HISFC.Models.Registration.Register)f.Patient).DoctorInfo.Templet.Dept.ID == null || ((FS.HISFC.Models.Registration.Register)f.Patient).DoctorInfo.Templet.Dept.ID == string.Empty)
                    {
                        ((FS.HISFC.Models.Registration.Register)f.Patient).DoctorInfo.Templet.Dept = r.DoctorInfo.Templet.Dept.Clone();
                    }
                    if (((FS.HISFC.Models.Registration.Register)f.Patient).DoctorInfo.Templet.Doct.ID == null || ((FS.HISFC.Models.Registration.Register)f.Patient).DoctorInfo.Templet.Doct.ID == string.Empty)
                    {
                        ((FS.HISFC.Models.Registration.Register)f.Patient).DoctorInfo.Templet.Doct = r.DoctorInfo.Templet.Doct.Clone();
                    }
                    if (f.RecipeOper.Dept.ID == null || f.RecipeOper.Dept.ID == string.Empty)
                    {
                        f.RecipeOper.Dept.ID = r.DoctorInfo.Templet.Doct.User01;
                    }

                    if (f.ChargeOper.OperTime == DateTime.MinValue)
                    {
                        f.ChargeOper.OperTime = feeTime;
                    }
                    if (f.ChargeOper.ID == null || f.ChargeOper.ID == string.Empty)
                    {
                        f.ChargeOper.ID = operID;
                    }
                    if (((FS.HISFC.Models.Registration.Register)f.Patient).DoctorInfo.Templet.Doct.ID == null || ((FS.HISFC.Models.Registration.Register)f.Patient).DoctorInfo.Templet.Doct.ID == string.Empty)
                    {
                        if (r.SeeDoct.ID != null && r.SeeDoct.ID != "")
                        {
                            ((FS.HISFC.Models.Registration.Register)f.Patient).DoctorInfo.Templet.Doct.ID = r.SeeDoct.ID;
                        }
                        else
                        {
                            errText = "��ѡ��ҽ��";
                            return false;
                        }
                    }

                    if (f.RecipeOper.ID == null || f.RecipeOper.ID == string.Empty)
                    {
                        f.RecipeOper.ID = ((FS.HISFC.Models.Registration.Register)f.Patient).DoctorInfo.Templet.Doct.ID;
                    }

                    f.CancelType = FS.HISFC.Models.Base.CancelTypes.Valid;
                    f.FeeOper.ID = operID;
                    f.FeeOper.OperTime = feeTime;
                    f.ExamineFlag = r.ChkKind;

                    //�������Ϊ������죬��ô������Ŀ�������ն���ˡ�
                    if (r.ChkKind == "2")
                    {
                        if (!f.IsConfirmed)
                        {
                            //�����Ŀ��ˮ��Ϊ�գ�˵��û�о����������̣���ô�����ն������Ϣ��
                            if (f.Order.ID == null || f.Order.ID == string.Empty)
                            {
                                f.Order.ID = orderManager.GetNewOrderID();
                                if (f.Order.ID == null || f.Order.ID == string.Empty)
                                {
                                    errText = "���ҽ����ˮ�ų���!";
                                    return false;
                                }

                                FS.HISFC.BizProcess.Integrate.Terminal.Result result = confirmIntegrate.ServiceInsertTerminalApply(f, r);

                                if (result != FS.HISFC.BizProcess.Integrate.Terminal.Result.Success)
                                {
                                    errText = "�����ն�����ȷ�ϱ�ʧ��!";

                                    return false;
                                }
                            }
                        }
                    }
                    else//�������������ĿΪ��Ҫ�ն������Ŀ������ն������Ϣ��
                    {
                        if (!f.IsConfirmed)
                        {
                            //if (f.Item.IsNeedConfirm)
                            if (f.Item.ItemType == EnumItemType.UnDrug)
                            {
                                if (f.Item.NeedConfirm == EnumNeedConfirm.Outpatient || f.Item.NeedConfirm == EnumNeedConfirm.All || f.Item.SpecialFlag2 == "1")
                                {
                                    if (f.Item.SpecialFlag2 == "0")
                                    {
                                        f.IsConfirmed = true;
                                    }
                                    else
                                    {
                                        if (f.Order.ID == null || f.Order.ID == string.Empty)
                                        {
                                            f.Order.ID = orderManager.GetNewOrderID();
                                        }
                                        if (f.Order.ID == null || f.Order.ID == string.Empty)
                                        {
                                            errText = "���ҽ����ˮ�ų���!";
                                            return false;
                                        }

                                        FS.HISFC.BizProcess.Integrate.Terminal.Result result = confirmIntegrate.ServiceInsertTerminalApply(f, r);
                                        if (result != FS.HISFC.BizProcess.Integrate.Terminal.Result.Success)
                                        {
                                            errText = "�����ն�����ȷ�ϱ�ʧ��!" + confirmIntegrate.Err;
                                            return false;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    //û�и�ֵҽ����ˮ��,��ֵ�µ�ҽ����ˮ��
                    if (f.Order.ID == null || f.Order.ID == string.Empty)
                    {
                        f.Order.ID = orderManager.GetNewOrderID();
                        if (f.Order.ID == null || f.Order.ID == string.Empty)
                        {
                            errText = "���ҽ����ˮ�ų���!";
                            return false;
                        }
                    }

                    if (r.ChkKind == "1")//�����������շѱ��
                    {
                        iReturn = examiIntegrate.UpdateItemListFeeFlagByMoOrder("1", f.Order.ID);
                        if (iReturn == -1)
                        {
                            errText = "��������շѱ��ʧ��!" + examiIntegrate.Err;
                            return false;
                        }
                    }

                    //���ɾ�����۱����е������Ŀ����Ŀ��Ϣ,������ϸ.
                    if (f.UndrugComb.ID != null && f.UndrugComb.ID.Length > 0)
                    {
                        iReturn = outpatientManager.DeletePackageByMoOrder(f.Order.ID);
                        if (iReturn == -1)
                        {
                            errText = "ɾ������ʧ��!" + outpatientManager.Err;
                            return false;
                        }
                        //��֪��˭�޸ĵģ�ż��ɾ�����׷���ʧ��...
                        //ǰ���Ѿ�������ҽ����id������õ�User03���˴���ɾһ��  houwb
                        else if (iReturn == 0)
                        {
                            iReturn = outpatientManager.DeletePackageByMoOrder(f.User03);
                            if (iReturn == -1)
                            {
                                errText = "ɾ������ʧ��!" + outpatientManager.Err;
                                return false;
                            }
                        }
                    }
                    //FeeItemList feeTemp = new FeeItemList();
                    //feeTemp = outpatientManager.GetFeeItemList(f.RecipeNO, f.SequenceNO);
                    //{39B2599D-2E90-4b3d-A027-4708A70E45C3}
                    int chargeItemCount = outpatientManager.GetChargeItemCount(f.RecipeNO, f.SequenceNO);
                    if (chargeItemCount == -1)
                    {
                        errText = "��ѯ��Ŀ��Ϣʧ�ܣ�";
                        return false;
                    }

                    if (chargeItemCount == 0)//˵��������
                    //if(feeTemp == null)
                    {
                        if (f.FTSource != "3" && (f.UndrugComb.ID == null || f.UndrugComb.ID == string.Empty))
                        {
                            errText = f.Item.Name + "�����Ѿ�����������Աɾ��,��ˢ�º����շ�!";

                            return false;
                        }

                        iReturn = outpatientManager.InsertFeeItemList(f);
                        if (iReturn <= 0)
                        {
                            errText = "���������ϸʧ��!" + outpatientManager.Err;

                            return false;
                        }
                    }
                    else
                    {
                        iReturn = outpatientManager.UpdateFeeItemList(f);
                        if (iReturn <= 0)
                        {
                            errText = "���·�����ϸʧ��!" + outpatientManager.Err;

                            return false;
                        }
                    }

                    #region//��дҽ����Ϣ

                    if (f.FTSource == "1")
                    {
                        iReturn = orderOutpatientManager.UpdateOrderChargedByOrderID(f.Order.ID, operID);
                        if (iReturn <= 0 && !f.Item.IsMaterial && f.Item.ItemType == EnumItemType.Drug)
                        {
                            errText = "û�и��µ�ҽ����Ϣ������ҽ��ȷ���Ƿ��Ѿ�ɾ����ҽ��:" + f.Item.Name + ",������ˢ�������û����շ���Ϣ." + orderOutpatientManager.Err;

                            return false;
                        }

                        bool isCanModifyUnDrug = false;
                        isCanModifyUnDrug = this.controlParamIntegrate.GetControlParam<bool>("MZ9934", true, false);

                        if (f.Item.ItemType == EnumItemType.Drug)
                        {
                            FS.HISFC.Models.Order.OutPatient.Order order = orderOutpatientManager.QueryOneOrder(f.Patient.ID, f.Order.ID, f.RecipeNO);

                            if (order != null && order.ID.Length > 0)
                            {
                                if (f.FeePack == "1")
                                {
                                    if (order.Qty * order.Item.PackQty != f.Item.Qty)
                                    {
                                        errText = "��" + order.Item.Name + "�� �շ�������ҽ������������ͬ!";

                                        return false;
                                    }
                                }
                                else
                                {
                                    if (order.Qty != f.Item.Qty)
                                    {
                                        errText = "��" + order.Item.Name + "�� �շ�������ҽ������������ͬ!";

                                        return false;
                                    }
                                }
                            }
                        }
                        else if (!isCanModifyUnDrug)
                        {

                            FS.HISFC.Models.Order.OutPatient.Order order = orderOutpatientManager.QueryOneOrder(f.Patient.ID, f.Order.ID, f.RecipeNO);

                            if (order != null && order.ID.Length > 0)
                            {
                                //����Ǹ�����Ŀ
                                if (!string.IsNullOrEmpty(f.UndrugComb.ID))
                                {
                                    //ȡ������Ŀά������ϸ����
                                    FS.HISFC.Models.Fee.Item.UndrugComb undrugCombo = undrugPackAgeMgr.GetUndrugComb(f.UndrugComb.ID, f.Item.ID);
                                    if (undrugCombo == null)
                                    {
                                        errText = "��ȡ������Ŀ" + f.UndrugComb.ID + "�ķ�ҩƷ��Ŀ��" + f.Item.ID + "ʧ�ܣ�ԭ��" + itemManager.Err;
                                        return false;
                                    }

                                    if (order.Qty != f.Item.Qty / undrugCombo.Qty)
                                    {
                                        errText = "��" + order.Item.Name + "�� �շ�������ҽ������������ͬ!";

                                        return false;
                                    }
                                }
                                else
                                {
                                    if (order.Qty != f.Item.Qty)
                                    {
                                        errText = "�շ�������ҽ������������ͬ!";

                                        return false;
                                    }
                                }
                            }
                        }
                    }

                    #endregion

                    #region ���뷢ҩ�����б�

                    //�����ҩƷ,����û�б�ȷ�Ϲ�,���Ҳ���Ҫ�ն�ȷ��,��ô���뷢ҩ�����б�.
                    //if (f.Item.IsPharmacy)
                    if (f.Item.ItemType == EnumItemType.Drug)
                    {
                        if (!f.IsConfirmed && f.Item.ID != "999")
                        {
                            if (!f.Item.IsNeedConfirm)
                            {
                                drugLists.Add(f);
                            }
                        }
                    }
                    #endregion

                    #region ����ҽ��ԤԼ��

                    //��Ҫҽ��ԤԼ,�����ն�ԤԼ��Ϣ.
                    if (f.Item.IsNeedBespeak && r.ChkKind != "2")
                    {
                        iReturn = bookingIntegrate.Insert(f);

                        if (iReturn == -1)
                        {
                            errText = "����ҽ��ԤԼ��Ϣ����!" + f.Name + bookingIntegrate.Err;

                            return false;
                        }
                    }
                    #endregion
                }

                #endregion

                #region �����������շѱ��

                if (r.ChkKind == "2")//�������
                {
                    ArrayList recipeSeqList = this.GetRecipeSequenceForChk(feeDetails);
                    if (recipeSeqList != null && recipeSeqList.Count > 0)
                    {
                        foreach (string recipeSequenceTemp in recipeSeqList)
                        {
                            iReturn = examiIntegrate.UpdateItemListFeeFlagByRecipeSeq("1", recipeSequenceTemp);
                            if (iReturn == -1)
                            {
                                errText = "��������շѱ��ʧ��!" + examiIntegrate.Err;

                                return false;
                            }

                        }
                    }
                }

                #endregion

                #region//��ҩ������Ϣ

                string drugSendInfo = null;

                if (isSplitNostrum)
                {
                    drugLists.Clear();
                    foreach (FeeItemList item in noSplitDrugList)
                    {
                        foreach (FeeItemList f in feeDetails)
                        {
                            if (item.Order.ID == f.Order.ID)
                            {
                                item.RecipeNO = f.RecipeNO;
                                item.FeeOper = f.FeeOper;
                                break;
                            }
                        }
                    }
                    drugLists.AddRange(noSplitDrugList);
                }
                FS.FrameWork.Management.ControlParam ctrlManager = new FS.FrameWork.Management.ControlParam();
                int isPartSend = 0;
                try
                {
                    isPartSend = FS.FrameWork.Function.NConvert.ToInt32(ctrlManager.QueryControlerInfo("HNGYZL", false));
                }
                catch
                {
                    isPartSend = 0;
                }
                //���뷢ҩ������Ϣ,���ط�ҩ����,��ʾ�ڷ�Ʊ��
                if (isPartSend == 1)
                {
                    iReturn = pharmarcyManager.ApplyOut(r, drugLists, feeTime, string.Empty, false, out drugSendInfo);
                }
                else
                {
                    iReturn = pharmarcyManager.ApplyOut(r, drugLists, string.Empty, feeTime, false, out drugSendInfo);
                }
                if (iReturn == -1)
                {
                    errText = "����ҩƷ��ϸʧ��!" + pharmarcyManager.Err;

                    return false;
                }

                //'�����ҩƷ,��ô���÷�Ʊ����ʾ��ҩ������Ϣ.
                if (drugLists.Count > 0)
                {
                    //{02F6E9D7-E311-49a4-8FE4-BF2AC88B889B}���ε�С�汾���룬���ú��İ汾�Ĵ���
                    //foreach (Balance invoice in invoices)
                    //{
                    //    invoice.DrugWindowsNO = drugSendInfo;
                    //}
                    foreach (Balance invoice in invoices)
                    {
                        string tempInvoiceNo = string.Empty;
                        for (int i = 0; i < drugLists.Count; i++)
                        {
                            FS.HISFC.Models.Fee.Outpatient.FeeItemList oneFeeItem = new FeeItemList();
                            oneFeeItem = drugLists[i] as FS.HISFC.Models.Fee.Outpatient.FeeItemList;
                            //if (oneFeeItem.Item.IsPharmacy)
                            if (oneFeeItem.Item.ItemType == EnumItemType.Drug)
                            {
                                tempInvoiceNo = oneFeeItem.Invoice.ID;
                            }
                            if (invoice.Invoice.ID == tempInvoiceNo)
                            {
                                invoice.DrugWindowsNO = drugSendInfo;
                            }
                        }
                    }
                }

                #endregion

                #region//���뷢Ʊ����

                foreach (Balance balance in invoices)
                {
                    //����Ʊ��Ϣ,������ֻ����ʾ��
                    if (balance.Memo == "5")
                    {
                        mainInvoiceNO = balance.ID;

                        continue;
                    }
                    if (string.IsNullOrEmpty(balance.CombNO))
                    {
                        balance.CombNO = invoiceCombNO;
                    }
                    balance.BalanceOper.ID = operID;
                    balance.BalanceOper.OperTime = feeTime;
                    balance.Patient.Pact = r.Pact;
                    //����־
                    string tempExamineFlag = null;
                    //�������־ 0 ��ͨ���� 1 ������� 2 �������
                    //���û�и�ֵ,Ĭ��Ϊ��ͨ����
                    if (r.ChkKind.Length > 0)
                    {
                        tempExamineFlag = r.ChkKind;
                    }
                    else
                    {
                        tempExamineFlag = "0";
                    }
                    balance.ExamineFlag = tempExamineFlag;
                    balance.CancelType = FS.HISFC.Models.Base.CancelTypes.Valid;

                    //=====ȥ��CanceledInvoiceNO=string.Empty ·־��================
                    //balance.CanceledInvoiceNO = string.Empty;
                    //==============================================================

                    balance.IsAuditing = false;
                    balance.IsDayBalanced = false;
                    balance.ID = balance.ID.PadLeft(12, '0');
                    balance.Patient.Pact.Memo = r.User03;//�޶����
                    //�Էѻ��߲���Ҫ��ʾ����Ʊ,��ôȡ��һ����Ʊ����Ϊ����Ʊ��
                    if (mainInvoiceNO == string.Empty)
                    {
                        mainInvoiceNO = balance.Invoice.ID;
                    }
                    #region ���ڴ��ж��Ƿ���ڷ�Ʊ�ţ��������
                    //if (invoiceType == "0")
                    //{
                    //    string tmpCount = outpatientManager.QueryExistInvoiceCount(balance.Invoice.ID);
                    //    if (tmpCount == "1")
                    //    {
                    //        DialogResult result = MessageBox.Show("�Ѿ����ڷ�Ʊ��Ϊ: " + balance.Invoice.ID +
                    //            " �ķ�Ʊ!,�Ƿ����?", "��ʾ!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    //        if (result == DialogResult.No)
                    //        {
                    //            errText = "��Ʊ���ظ���ʱȡ�����ν���!";

                    //            return false;
                    //        }
                    //    }
                    //}
                    //else if (invoiceType == "1")
                    //{
                    //    string tmpCount = outpatientManager.QueryExistInvoiceCount(balance.PrintedInvoiceNO);
                    //    if (tmpCount == "1")
                    //    {
                    //        DialogResult result = MessageBox.Show("�Ѿ�����Ʊ�ݺ�Ϊ: " + balance.PrintedInvoiceNO +
                    //            " �ķ�Ʊ!,�Ƿ����?", "��ʾ!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    //        if (result == DialogResult.No)
                    //        {
                    //            errText = "��Ʊ���ظ���ʱȡ�����ν���!";

                    //            return false;
                    //        }
                    //    }
                    //}
                    #endregion
                    //���뷢Ʊ����fin_opb_invoice
                    iReturn = outpatientManager.InsertBalance(balance);
                    if (iReturn == -1)
                    {
                        errText = "�����������!" + outpatientManager.Err;

                        return false;
                    }
                }
                #endregion

                #region ��Ʊ���ߺţ����Ʊ����һ������

                if (!isTempInvoice)//��ʱ��Ʊ���벻����һ������
                {
                    string invoiceNo = ((Balance)invoices[invoices.Count - 1]).Invoice.ID;
                    string realInvoiceNo = ((Balance)invoices[invoices.Count - 1]).PrintedInvoiceNO;

                    if (invoiceNo.Length >= 12 && invoiceNo.StartsWith("9"))
                    {
                        // Ϊ��ʱ��Ʊ�����ʻ����п�������ʱ��Ʊ
                    }
                    else
                    {
                        int invoicesCount = invoices.Count;
                        foreach (Balance invoiceObj in invoices)
                        {
                            if (invoiceObj.Memo == "5")
                            {
                                invoicesCount = invoices.Count - 1;
                                continue;
                            }
                        }
                        if (this.feeManager.UseInvoiceNO(oper, invoiceStytle, invoiceType, invoicesCount, ref invoiceNo, ref realInvoiceNo, ref errText) < 0)
                        {
                            return false;
                        }

                        foreach (Balance invoiceObj in invoices)
                        {
                            if (invoiceObj.Memo == "5")
                            {
                                continue;
                            }
                            if (this.feeManager.InsertInvoiceExtend(invoiceObj.Invoice.ID, invoiceType, invoiceObj.PrintedInvoiceNO, "00") < 1)
                            {//��Ʊͷ��ʱ�ȱ���00
                                errText = this.invoiceServiceManager.Err;
                                return false;
                            }
                        }
                    }
                }

                #endregion

                #region ����֧����ʽ��Ϣ

                int payModeSeq = 1;

                foreach (BalancePay p in payModes)
                {
                    p.Invoice.ID = mainInvoiceNO.PadLeft(12, '0');
                    p.TransType = TransTypes.Positive;
                    p.Squence = payModeSeq.ToString();
                    p.IsDayBalanced = false;
                    p.IsAuditing = false;
                    p.IsChecked = false;
                    p.InputOper.ID = operID;
                    p.InputOper.OperTime = feeTime;
                    if (string.IsNullOrEmpty(p.InvoiceCombNO))
                    {
                        //p.InvoiceCombNO = mainInvoiceCombNO;
                        if (string.IsNullOrEmpty(mainInvoiceCombNO))
                        {
                            p.InvoiceCombNO = invoiceCombNO;
                        }
                        else
                        {
                            p.InvoiceCombNO = mainInvoiceCombNO;
                        }
                    }
                    p.CancelType = CancelTypes.Valid;

                    payModeSeq++;

                    //realCost += p.FT.RealCost;

                    iReturn = outpatientManager.InsertBalancePay(p);
                    if (iReturn == -1)
                    {
                        errText = "����֧����ʽ�����!" + outpatientManager.Err;

                        return false;
                    }

                    //{93E6443C-1FB5-45a7-B89D-F21A92200CF6}
                    //if (p.PayType.ID.ToString() == FS.HISFC.Models.Fee.EnumPayType.YS.ToString())
                    if (p.PayType.ID.ToString() == "YS")
                    {
                        //bool returnValue = this.AccountPay(r.PID.CardNO, p.FT.TotCost, p.Invoice.ID, p.InputOper.Dept.ID);



                        //if (!returnValue)
                        //{
                        //    errText = "��ȡ�����˻�ʧ��!" + "\n" + this.Err;

                        //    return false;
                        //}
                        //{DA67A335-E85E-46e1-A672-4DB409BCC11B}
                        int returnValue = this.feeManager.AccountPay(r, p.FT.TotCost, p.Invoice.ID, (accountManager.Operator as FS.HISFC.Models.Base.Employee).Dept.ID, "C");
                        if (returnValue < 0)
                        {
                            errText = "��ȡ�����˻�ʧ��!" + "\n" + this.Err;

                            return false;
                        }
                        if (returnValue == 0)
                        {
                            errText = "ȡ���ʻ�֧��!";
                            return false;
                        }
                    }
                }
                #endregion

                #region ����Һż�¼�����¿�����

                string noRegRules = controlParamIntegrate.GetControlParam(FS.HISFC.BizProcess.Integrate.Const.NO_REG_CARD_RULES, false, "9");

                //�������������շ�,��ô����Һ���Ϣ,����Ѿ������,��ô����.
                if (r.PID.CardNO.Substring(0, 1) == noRegRules || r.User01 == "���Һ�")
                {
                    r.InputOper.OperTime = DateTime.MinValue;
                    r.InputOper.ID = operID;
                    r.IsFee = true;
                    r.TranType = TransTypes.Positive;
                    iReturn = registerManager.Insert(r);
                    if (iReturn == -1)
                    {
                        if (registerManager.DBErrCode != 1)//���������ظ�
                        {
                            errText = "����Һ���Ϣ����!" + registerManager.Err;

                            return false;
                        }
                    }
                }
                else
                {
                    if (registerManager.Update(FS.SOC.HISFC.PE.BizLogic.EnumUpdateStatus.PatientInfo, r) <= 0)
                    {
                        errText = "���¹Һ���Ϣʧ��!" + registerManager.Err;
                        return false;
                    }

                    if (registerManager.UpdateRegInfo(r) <= 0)
                    {
                        errText = "���¹Һ���Ϣʧ��!" + registerManager.Err;
                        return false;
                    }
                }

                pValue = controlParamIntegrate.GetControlParam("200018", false, "0");

                if (//r.PID.CardNO.Substring(0, 1) != noRegRules && 
                    r.ChkKind.Length == 0)
                {
                    //��������ҽ�����»��ߵĿ���ҽ�� {7255EEBE-CF2B-4117-BB2C-196BE75B5965}
                    if (!r.IsSee || string.IsNullOrEmpty(r.SeeDoct.ID) || string.IsNullOrEmpty(r.SeeDoct.Dept.ID))
                    {
                        if (this.registerManager.UpdateSeeDone(r.ID) < 0)
                        {
                            errText = "���¿����־����" + registerManager.Err;
                            return false;
                        }
                        FeeItemList feeItemObj = feeDetails[0] as FeeItemList;
                        if (this.registerManager.UpdateDept(r.ID, feeItemObj.RecipeOper.Dept.ID, feeItemObj.RecipeOper.ID) < 0)
                        {
                            errText = "���¿�����ҡ�ҽ������" + registerManager.Err;
                            return false;
                        }
                        if (pValue == "1" && r.IsTriage)
                        {
                            if (this.managerIntegrate.UpdateAssign("", r.ID, feeTime, operID) < 0)
                            {
                                errText = "���·����־����";
                                return false;
                            }
                        }
                    }
                }

                #endregion

                #endregion
            }
            else//����
            {
                #region ��������

                string noRegRules = controlParamIntegrate.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.Const.NO_REG_CARD_RULES, false, "9");
                if (r.PID.CardNO.Substring(0, 1) == noRegRules)
                {
                    r.InputOper.OperTime = DateTime.MinValue;
                    r.InputOper.ID = outpatientManager.Operator.ID;
                    r.IsFee = true;
                    r.TranType = TransTypes.Positive;
                    iReturn = registerManager.Insert(r);
                    if (iReturn == -1)
                    {
                        if (registerManager.DBErrCode != 1)//���������ظ�
                        {
                            errText = "����Һ���Ϣ����!" + registerManager.Err;

                            return false;
                        }
                    }
                }
                //�����۱�����Ϣ.
                bool returnValue = this.feeManager.SetChargeInfo(r, feeDetails, feeTime, ref errText);
                if (!returnValue)
                {
                    return false;
                }

                #endregion
            }


            return true;
        }
        /// <summary>
        /// �����շѺ���
        /// </summary>
        /// <param name="type">�շ�,���۱�־</param>
        /// <param name="r">���߹ҺŻ�����Ϣ</param>
        /// <param name="invoices">��Ʊ������</param>
        /// <param name="invoiceDetails">��Ʊ��ϸ����</param>
        /// <param name="feeDetails">������ϸ����</param>
        /// <param name="t">Transcation</param>
        /// <param name="payModes">֧����ʽ����</param>
        /// <param name="errText">������Ϣ</param>
        /// <returns></returns>
        public bool ClinicFeeSaveFee(FS.HISFC.Models.Base.ChargeTypes type, FS.HISFC.Models.Registration.Register r,
            ArrayList invoices, ArrayList invoiceDetails, ArrayList feeDetails, ArrayList invoiceFeeDetails, ArrayList payModes, ref string errText)
        {

            FS.HISFC.BizProcess.Integrate.Terminal.Confirm confirmIntegrate = new FS.HISFC.BizProcess.Integrate.Terminal.Confirm();
            FS.HISFC.BizProcess.Integrate.Terminal.Booking bookingIntegrate = new FS.HISFC.BizProcess.Integrate.Terminal.Booking();

            invoiceStytle = controlParamIntegrate.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.Const.GET_INVOICE_NO_TYPE, false, "0");

            isDoseOnceCanNull = controlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.Const.DOSE_ONCE_NULL, false, true);

            //����շ�ʱ��
            DateTime feeTime = inpatientManager.GetDateTimeFromSysDateTime();

            //����շѲ���Ա
            string operID = inpatientManager.Operator.ID;

            FS.HISFC.Models.Base.Employee employee = inpatientManager.Operator as FS.HISFC.Models.Base.Employee;
            //����ֵ
            int iReturn = 0;
            //���崦����
            string recipeNO = string.Empty;

            //������շѣ���÷�Ʊ��Ϣ
            if (type == FS.HISFC.Models.Base.ChargeTypes.Fee)//�շ�
            {
                #region �շ�����
                //��Ʊ�Ѿ���Ԥ������������,ֱ�Ӳ���Ϳ�����.

                #region//��÷�Ʊ����,���ŷ�Ʊ��Ʊ�Ų�ͬ,����һ����Ʊ����,ͨ����Ʊ���к�,���Բ�ѯһ���շѵĶ��ŷ�Ʊ.

                string invoiceCombNO = outpatientManager.GetInvoiceCombNO();
                if (invoiceCombNO == null || invoiceCombNO == string.Empty)
                {
                    errText = "��÷�Ʊ��ˮ��ʧ��!" + outpatientManager.Err;

                    return false;
                }
                //���������ʾ���
                /////GetSpDisplayValue(myCtrl, t);
                //��һ����Ʊ��
                string mainInvoiceNO = string.Empty;
                string mainInvoiceCombNO = string.Empty;
                foreach (Balance balance in invoices)
                {
                    //����Ʊ��Ϣ,������ֻ����ʾ��
                    if (balance.Memo == "5")
                    {
                        mainInvoiceNO = balance.ID;

                        continue;
                    }

                    //�Էѻ��߲���Ҫ��ʾ����Ʊ,��ôȡ��һ����Ʊ����Ϊ����Ʊ��
                    if (mainInvoiceNO == string.Empty)
                    {
                        mainInvoiceNO = balance.Invoice.ID;
                        mainInvoiceCombNO = balance.CombNO;
                    }
                }

                #endregion

                #region //���뷢Ʊ��ϸ��

                foreach (ArrayList tempsInvoices in invoiceDetails)
                {
                    foreach (ArrayList tempDetals in tempsInvoices)
                    {
                        foreach (BalanceList balanceList in tempDetals)
                        {
                            //�ܷ�Ʊ����
                            if (balanceList.Memo == "5")
                            {
                                continue;
                            }
                            if (string.IsNullOrEmpty(((Balance)balanceList.BalanceBase).CombNO))
                            {
                                ((Balance)balanceList.BalanceBase).CombNO = invoiceCombNO;
                            }
                            balanceList.BalanceBase.BalanceOper.ID = operID;
                            balanceList.BalanceBase.BalanceOper.OperTime = feeTime;
                            balanceList.BalanceBase.IsDayBalanced = false;
                            balanceList.BalanceBase.CancelType = CancelTypes.Valid;
                            balanceList.ID = balanceList.ID.PadLeft(12, '0');

                            //���뷢Ʊ��ϸ�� fin_opb_invoicedetail
                            iReturn = outpatientManager.InsertBalanceList(balanceList);
                            if (iReturn == -1)
                            {
                                errText = "���뷢Ʊ��ϸ����!" + outpatientManager.Err;

                                return false;
                            }
                        }
                    }
                }

                #endregion

                #region//ҩƷ��Ϣ�б�,���ɴ�����

                ArrayList drugLists = new ArrayList();
                //�������ɴ�����,������д�����,��ϸ�����¸�ֵ.
                if (!this.SetRecipeNOOutpatient(r, feeDetails, ref errText))
                {
                    return false;
                }

                #endregion

                #region//���������ϸ

                foreach (FeeItemList f in feeDetails)
                {
                    //��֤����
                    if (!this.IsFeeItemListDataValid(f, ref errText))
                    {
                        return false;
                    }

                    //���û�д�����,���¸�ֵ
                    if (f.RecipeNO == null || f.RecipeNO == string.Empty)
                    {
                        if (recipeNO == string.Empty)
                        {
                            recipeNO = outpatientManager.GetRecipeNO();
                            if (recipeNO == null || recipeNO == string.Empty)
                            {
                                errText = "��ô����ų���!";

                                return false;
                            }
                        }
                    }

                    #region 2007-8-29 liuq �ж��Ƿ����з�Ʊ����ţ�û����ֵ
                    if (string.IsNullOrEmpty(f.InvoiceCombNO))
                    {
                        f.InvoiceCombNO = invoiceCombNO;
                    }
                    #endregion
                    //
                    #region 2007-8-28 liuq �ж��Ƿ����з�Ʊ�ţ�û�г�ʼ��Ϊ12��0
                    if (string.IsNullOrEmpty(f.Invoice.ID))
                    {
                        f.Invoice.ID = mainInvoiceNO.PadLeft(12, '0');
                    }
                    #endregion
                    f.FeeOper.ID = operID;
                    f.FeeOper.OperTime = feeTime;
                    f.PayType = FS.HISFC.Models.Base.PayTypes.Balanced;
                    f.TransType = TransTypes.Positive;
                    f.Patient.PID.CardNO = r.PID.CardNO;
                    //f.Patient = r.Clone();
                    ((FS.HISFC.Models.Registration.Register)f.Patient).DoctorInfo.SeeDate = r.DoctorInfo.SeeDate;
                    if (((FS.HISFC.Models.Registration.Register)f.Patient).DoctorInfo.Templet.Dept.ID == null || ((FS.HISFC.Models.Registration.Register)f.Patient).DoctorInfo.Templet.Dept.ID == string.Empty)
                    {
                        ((FS.HISFC.Models.Registration.Register)f.Patient).DoctorInfo.Templet.Dept = r.DoctorInfo.Templet.Dept.Clone();
                    }
                    if (((FS.HISFC.Models.Registration.Register)f.Patient).DoctorInfo.Templet.Doct.ID == null || ((FS.HISFC.Models.Registration.Register)f.Patient).DoctorInfo.Templet.Doct.ID == string.Empty)
                    {
                        ((FS.HISFC.Models.Registration.Register)f.Patient).DoctorInfo.Templet.Doct = r.DoctorInfo.Templet.Doct.Clone();
                    }
                    if (f.RecipeOper.Dept.ID == null || f.RecipeOper.Dept.ID == string.Empty)
                    {
                        f.RecipeOper.Dept.ID = r.DoctorInfo.Templet.Doct.User01;
                    }

                    if (f.ChargeOper.OperTime == DateTime.MinValue)
                    {
                        f.ChargeOper.OperTime = feeTime;
                    }
                    if (f.ChargeOper.ID == null || f.ChargeOper.ID == string.Empty)
                    {
                        f.ChargeOper.ID = operID;
                    }
                    if (((FS.HISFC.Models.Registration.Register)f.Patient).DoctorInfo.Templet.Doct.ID == null || ((FS.HISFC.Models.Registration.Register)f.Patient).DoctorInfo.Templet.Doct.ID == string.Empty)
                    {
                        if (r.SeeDoct.ID != null && r.SeeDoct.ID != "")
                        {
                            ((FS.HISFC.Models.Registration.Register)f.Patient).DoctorInfo.Templet.Doct.ID = r.SeeDoct.ID;
                        }
                        else
                        {
                            errText = "��ѡ��ҽ��";
                            return false;
                        }
                    }

                    if (f.RecipeOper.ID == null || f.RecipeOper.ID == string.Empty)
                    {
                        f.RecipeOper.ID = ((FS.HISFC.Models.Registration.Register)f.Patient).DoctorInfo.Templet.Doct.ID;
                    }

                    f.CancelType = FS.HISFC.Models.Base.CancelTypes.Valid;
                    f.FeeOper.ID = operID;
                    f.FeeOper.OperTime = feeTime;
                    f.ExamineFlag = r.ChkKind;

                    //�������Ϊ������죬��ô������Ŀ�������ն���ˡ�
                    if (r.ChkKind == "2")
                    {
                        if (!f.IsConfirmed)
                        {
                            //�����Ŀ��ˮ��Ϊ�գ�˵��û�о����������̣���ô�����ն������Ϣ��
                            if (f.Order.ID == null || f.Order.ID == string.Empty)
                            {
                                f.Order.ID = orderManager.GetNewOrderID();
                                if (f.Order.ID == null || f.Order.ID == string.Empty)
                                {
                                    errText = "���ҽ����ˮ�ų���!";
                                    return false;
                                }

                                FS.HISFC.BizProcess.Integrate.Terminal.Result result = confirmIntegrate.ServiceInsertTerminalApply(f, r);

                                if (result != FS.HISFC.BizProcess.Integrate.Terminal.Result.Success)
                                {
                                    errText = "�����ն�����ȷ�ϱ�ʧ��!";

                                    return false;
                                }
                            }
                        }
                    }
                    else//�������������ĿΪ��Ҫ�ն������Ŀ������ն������Ϣ��
                    {
                        if (!f.IsConfirmed)
                        {
                            if (f.Item.IsNeedConfirm)
                            {
                                if (f.Order.ID == null || f.Order.ID == string.Empty)
                                {
                                    f.Order.ID = orderManager.GetNewOrderID();
                                }
                                if (f.Order.ID == null || f.Order.ID == string.Empty)
                                {
                                    errText = "���ҽ����ˮ�ų���!";

                                    return false;
                                }

                                FS.HISFC.BizProcess.Integrate.Terminal.Result result = confirmIntegrate.ServiceInsertTerminalApply(f, r);

                                if (result != FS.HISFC.BizProcess.Integrate.Terminal.Result.Success)
                                {
                                    errText = "�����ն�����ȷ�ϱ�ʧ��!" + confirmIntegrate.Err;

                                    return false;
                                }
                            }
                        }
                    }
                    //û�и�ֵҽ����ˮ��,��ֵ�µ�ҽ����ˮ��
                    if (f.Order.ID == null || f.Order.ID == string.Empty)
                    {
                        f.Order.ID = orderManager.GetNewOrderID();
                        if (f.Order.ID == null || f.Order.ID == string.Empty)
                        {
                            errText = "���ҽ����ˮ�ų���!";

                            return false;
                        }
                    }

                    if (r.ChkKind == "1")//�����������շѱ��
                    {
                        iReturn = examiIntegrate.UpdateItemListFeeFlagByMoOrder("1", f.Order.ID);
                        if (iReturn == -1)
                        {
                            errText = "��������շѱ��ʧ��!" + examiIntegrate.Err;

                            return false;
                        }
                    }

                    //���ɾ�����۱����е������Ŀ����Ŀ��Ϣ,������ϸ.
                    if (f.UndrugComb.ID != null && f.UndrugComb.ID.Length > 0)
                    {
                        iReturn = outpatientManager.DeletePackageByMoOrder(f.Order.ID);
                        if (iReturn == -1)
                        {
                            errText = "ɾ������ʧ��!" + outpatientManager.Err;

                            return false;
                        }
                    }
                    FeeItemList feeTemp = new FeeItemList();
                    feeTemp = outpatientManager.GetFeeItemList(f.RecipeNO, f.SequenceNO);
                    if (feeTemp == null)//˵��������
                    {
                        if (f.FTSource != "3" && (f.UndrugComb.ID == null || f.UndrugComb.ID == string.Empty))
                        {
                            errText = f.Item.Name + "�����Ѿ�����������Աɾ��,��ˢ�º����շ�!";

                            return false;
                        }

                        iReturn = outpatientManager.InsertFeeItemList(f);
                        if (iReturn <= 0)
                        {
                            errText = "���������ϸʧ��!" + outpatientManager.Err;

                            return false;
                        }
                    }
                    else
                    {
                        iReturn = outpatientManager.UpdateFeeItemList(f);
                        if (iReturn <= 0)
                        {
                            errText = "���·�����ϸʧ��!" + outpatientManager.Err;

                            return false;
                        }
                    }

                    #region//��дҽ����Ϣ

                    if (f.FTSource == "1")
                    {
                        iReturn = orderOutpatientManager.UpdateOrderChargedByOrderID(f.Order.ID, operID);
                        if (iReturn == -1)
                        {
                            errText = "����ҽ����Ϣ����!" + orderOutpatientManager.Err;

                            return false;
                        }
                    }

                    #endregion

                    //�����ҩƷ,����û�б�ȷ�Ϲ�,���Ҳ���Ҫ�ն�ȷ��,��ô���뷢ҩ�����б�.
                    //if (f.Item.IsPharmacy)
                    if (f.Item.ItemType == EnumItemType.Drug)
                    {
                        if (!f.IsConfirmed)
                        {
                            if (!f.Item.IsNeedConfirm)
                            {
                                drugLists.Add(f);
                            }
                        }
                    }
                    //��Ҫҽ��ԤԼ,�����ն�ԤԼ��Ϣ.
                    if (f.Item.IsNeedBespeak && r.ChkKind != "2")
                    {
                        iReturn = bookingIntegrate.Insert(f);

                        if (iReturn == -1)
                        {
                            errText = "����ҽ��ԤԼ��Ϣ����!" + f.Name + bookingIntegrate.Err;

                            return false;
                        }
                    }

                }

                #endregion

                #region �����������շѱ��

                if (r.ChkKind == "2")//�������
                {
                    ArrayList recipeSeqList = this.GetRecipeSequenceForChk(feeDetails);
                    if (recipeSeqList != null && recipeSeqList.Count > 0)
                    {
                        foreach (string recipeSequenceTemp in recipeSeqList)
                        {
                            iReturn = examiIntegrate.UpdateItemListFeeFlagByRecipeSeq("1", recipeSequenceTemp);
                            if (iReturn == -1)
                            {
                                errText = "��������շѱ��ʧ��!" + examiIntegrate.Err;

                                return false;
                            }

                        }
                    }
                }

                #endregion

                #region//��ҩ������Ϣ

                string drugSendInfo = null;
                //���뷢ҩ������Ϣ,���ط�ҩ����,��ʾ�ڷ�Ʊ��
                iReturn = pharmarcyManager.ApplyOut(r, drugLists, string.Empty, feeTime, false, out drugSendInfo);
                if (iReturn == -1)
                {
                    errText = "����ҩƷ��ϸʧ��!" + pharmarcyManager.Err;

                    return false;
                }
                //�����ҩƷ,��ô���÷�Ʊ����ʾ��ҩ������Ϣ.
                if (drugLists.Count > 0)
                {
                    foreach (Balance invoice in invoices)
                    {
                        invoice.DrugWindowsNO = drugSendInfo;
                    }
                }

                #region//���뷢Ʊ����

                foreach (Balance balance in invoices)
                {
                    //����Ʊ��Ϣ,������ֻ����ʾ��
                    if (balance.Memo == "5")
                    {
                        mainInvoiceNO = balance.ID;

                        continue;
                    }
                    if (string.IsNullOrEmpty(balance.CombNO))
                    {
                        balance.CombNO = invoiceCombNO;
                    }
                    balance.BalanceOper.ID = operID;
                    balance.BalanceOper.OperTime = feeTime;
                    balance.Patient.Pact = r.Pact;
                    //����־
                    string tempExamineFlag = null;
                    //�������־ 0 ��ͨ���� 1 ������� 2 �������
                    //���û�и�ֵ,Ĭ��Ϊ��ͨ����
                    if (r.ChkKind.Length > 0)
                    {
                        tempExamineFlag = r.ChkKind;
                    }
                    else
                    {
                        tempExamineFlag = "0";
                    }
                    balance.ExamineFlag = tempExamineFlag;
                    balance.CancelType = FS.HISFC.Models.Base.CancelTypes.Valid;

                    //=====ȥ��CanceledInvoiceNO=string.Empty ·־��================
                    //balance.CanceledInvoiceNO = string.Empty;
                    //==============================================================

                    balance.IsAuditing = false;
                    balance.IsDayBalanced = false;
                    balance.ID = balance.ID.PadLeft(12, '0');
                    balance.Patient.Pact.Memo = r.User03;//�޶����
                    //�Էѻ��߲���Ҫ��ʾ����Ʊ,��ôȡ��һ����Ʊ����Ϊ����Ʊ��
                    if (mainInvoiceNO == string.Empty)
                    {
                        mainInvoiceNO = balance.Invoice.ID;
                    }
                    if (invoiceStytle == "0")
                    {
                        string tmpCount = outpatientManager.QueryExistInvoiceCount(balance.Invoice.ID);
                        if (tmpCount == "1")
                        {
                            DialogResult result = MessageBox.Show("�Ѿ����ڷ�Ʊ��Ϊ: " + balance.Invoice.ID +
                                " �ķ�Ʊ!,�Ƿ����?", "��ʾ!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                            if (result == DialogResult.No)
                            {
                                errText = "��Ʊ���ظ���ʱȡ�����ν���!";

                                return false;
                            }
                        }
                    }
                    else if (invoiceStytle == "1")
                    {
                        string tmpCount = outpatientManager.QueryExistInvoiceCount(balance.PrintedInvoiceNO);
                        if (tmpCount == "1")
                        {
                            DialogResult result = MessageBox.Show("�Ѿ�����Ʊ�ݺ�Ϊ: " + balance.PrintedInvoiceNO +
                                " �ķ�Ʊ!,�Ƿ����?", "��ʾ!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                            if (result == DialogResult.No)
                            {
                                errText = "��Ʊ���ظ���ʱȡ�����ν���!";

                                return false;
                            }
                        }
                    }
                    //���뷢Ʊ����fin_opb_invoice
                    iReturn = outpatientManager.InsertBalance(balance);
                    if (iReturn == -1)
                    {
                        errText = "�����������!" + outpatientManager.Err;

                        return false;
                    }
                }



                #region ��Ʊ���ߺţ����Ʊ����һ������

                if (!isTempInvoice)//��ʱ��Ʊ���벻����һ������
                {
                    string invoiceNo = ((Balance)invoices[invoices.Count - 1]).Invoice.ID;
                    string realInvoiceNo = ((Balance)invoices[invoices.Count - 1]).PrintedInvoiceNO;
                    int invoicesCount = invoices.Count;
                    foreach (Balance invoiceObj in invoices)
                    {
                        if (invoiceObj.Memo == "5")
                        {
                            invoicesCount = invoices.Count - 1;
                            continue;
                        }
                    }
                    if (this.feeManager.UseInvoiceNO((FS.HISFC.Models.Base.Employee)this.feeBedFeeItem.Operator, invoiceStytle, "C", invoicesCount, ref invoiceNo, ref realInvoiceNo, ref errText) < 0)
                    {
                        return false;
                    }

                    foreach (Balance invoiceObj in invoices)
                    {
                        if (invoiceObj.Memo == "5")
                        {
                            continue;
                        }
                        if (this.feeManager.InsertInvoiceExtend(invoiceObj.Invoice.ID, "C", invoiceObj.PrintedInvoiceNO, "00") < 1)
                        {//��Ʊͷ��ʱ�ȱ���00
                            errText = this.invoiceServiceManager.Err;
                            return false;
                        }
                    }
                }

                #endregion


                #endregion

                #endregion

                #region ����֧����ʽ��Ϣ

                //int payModeSeq = 1;

                //foreach (BalancePay p in payModes)
                //{
                //    p.Invoice.ID = mainInvoiceNO.PadLeft(12, '0');
                //    p.TransType = TransTypes.Positive;
                //    p.Squence = payModeSeq.ToString();
                //    p.IsDayBalanced = false;
                //    p.IsAuditing = false;
                //    p.IsChecked = false;
                //    p.InputOper.ID = operID;
                //    p.InputOper.OperTime = feeTime;
                //    if (string.IsNullOrEmpty(p.InvoiceCombNO))
                //    {
                //        p.InvoiceCombNO = mainInvoiceCombNO;
                //    }
                //    p.CancelType = CancelTypes.Valid;

                //    payModeSeq++;

                //    //realCost += p.FT.RealCost;

                //    iReturn = outpatientManager.InsertBalancePay(p);
                //    if (iReturn == -1)
                //    {
                //        errText = "����֧����ʽ�����!" + outpatientManager.Err;

                //        return false;
                //    }

                //    if (p.PayType.ID.ToString() == FS.HISFC.Models.Fee.EnumPayType.YS.ToString())
                //    {
                //        bool returnValue = this.AccountPay(r.PID.CardNO, p.FT.TotCost, p.Invoice.ID, p.InputOper.Dept.ID);
                //        if (!returnValue)
                //        {
                //            errText = "��ȡ�����˻�ʧ��!" + "\n" + this.Err;

                //            return false;
                //        }
                //    }
                //}
                #endregion

                #region//�������ֱ���շѻ��ߺ���컼�ߣ����¿����־

                string noRegRules = controlParamIntegrate.GetControlParam(FS.HISFC.BizProcess.Integrate.Const.NO_REG_CARD_RULES, false, "9");

                //�������������շ�,��ô����Һ���Ϣ,����Ѿ������,��ô����.
                if (r.PID.CardNO.Substring(0, 1) == noRegRules)
                {
                    r.InputOper.OperTime = feeTime;
                    r.InputOper.ID = operID;
                    r.IsFee = true;
                    r.TranType = TransTypes.Positive;
                    iReturn = registerManager.Insert(r);
                    if (iReturn == -1)
                    {
                        if (registerManager.DBErrCode != 1)//���������ظ�
                        {
                            errText = "����Һ���Ϣ����!" + registerManager.Err;

                            return false;
                        }
                    }
                }

                pValue = controlParamIntegrate.GetControlParam("200018", false, "0");

                if (//r.PID.CardNO.Substring(0, 1) != noRegRules && 
                    r.ChkKind.Length == 0)
                {
                    //��������ҽ�����»��ߵĿ���ҽ�� {7255EEBE-CF2B-4117-BB2C-196BE75B5965}
                    if (!r.IsSee || string.IsNullOrEmpty(r.SeeDoct.ID) || string.IsNullOrEmpty(r.SeeDoct.Dept.ID))
                    {
                        if (this.registerManager.UpdateSeeDone(r.ID) < 0)
                        {
                            errText = "���¿����־����" + registerManager.Err;
                            return false;
                        }
                        FeeItemList feeItemObj = feeDetails[0] as FeeItemList;
                        if (this.registerManager.UpdateDept(r.ID, feeItemObj.RecipeOper.Dept.ID, feeItemObj.RecipeOper.ID) < 0)
                        {
                            errText = "���¿�����ҡ�ҽ������" + registerManager.Err;
                            return false;
                        }
                        if (pValue == "1" && r.IsTriage)
                        {
                            if (this.managerIntegrate.UpdateAssign("", r.ID, feeTime, operID) < 0)
                            {
                                errText = "���·����־����";
                                return false;
                            }
                        }
                    }
                }
                ////�����ҽ������,���±���ҽ��������Ϣ�� fin_ipr_siinmaininfo
                //if (r.Pact.PayKind.ID == "02")
                //{
                //    //�����ѽ����־
                //    r.SIMainInfo.IsBalanced = true;
                //    // iReturn = interfaceManager.update(r);
                //    if (iReturn < 0)
                //    {
                //        errText = "����ҽ�����߽�����Ϣ����!" + interfaceManager.Err;
                //        return false;
                //    }
                //}

                #endregion



                #endregion
            }
            else//����
            {
                #region ��������

                #region ��ֹ�����ڸõط���ֵ ���۱��������Դ
                foreach (FeeItemList f in feeDetails)
                {
                    f.FTSource = "0";//���۱��������Դ
                }
                #endregion

                string noRegRules = controlParamIntegrate.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.Const.NO_REG_CARD_RULES, false, "9");
                if (r.PID.CardNO.Substring(0, 1) == noRegRules)
                {
                    r.InputOper.OperTime = DateTime.MinValue;
                    r.InputOper.ID = outpatientManager.Operator.ID;
                    r.IsFee = true;
                    r.TranType = TransTypes.Positive;
                    iReturn = registerManager.Insert(r);
                    if (iReturn == -1)
                    {
                        if (registerManager.DBErrCode != 1)//���������ظ�
                        {
                            errText = "����Һ���Ϣ����!" + registerManager.Err;

                            return false;
                        }
                    }
                }
                //�����۱�����Ϣ.
                bool returnValue = this.feeManager.SetChargeInfo(r, feeDetails, feeTime, ref errText);
                if (!returnValue)
                {
                    return false;
                }

                #endregion
            }

            //������Ӧ֢{E4C0E5CF-D93F-48f9-A53C-9ADCCED97A7E}
            FS.HISFC.BizProcess.Interface.FeeInterface.IAdptIllnessOutPatient iAdptIllnessOutPatient = null;
            iAdptIllnessOutPatient = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.FeeInterface.IAdptIllnessOutPatient)) as FS.HISFC.BizProcess.Interface.FeeInterface.IAdptIllnessOutPatient;
            if (iAdptIllnessOutPatient != null)
            {
                //������Ӧ֢��Ϣ
                int returnValue = iAdptIllnessOutPatient.SaveOutPatientFeeDetail(r, ref feeDetails);
                if (returnValue < 0)
                {
                    return false;
                }

            }

            return true;
        }
        /// <summary>
        /// ���Э������
        /// </summary>
        /// <param name="feeItemLists"></param>
        /// <returns></returns>
        private int SplitNostrumDetail(FS.HISFC.Models.Registration.Register rInfo, ref ArrayList feeItemLists, ref ArrayList drugList, ref string errText)
        {
            ArrayList itemList = new ArrayList();
            foreach (FeeItemList f in feeItemLists)
            {
                if (f.Item.ItemType == EnumItemType.Drug)
                {
                    if (!f.IsConfirmed)
                    {
                        if (!f.Item.IsNeedConfirm && f.Item.ID != "999")
                        {
                            drugList.Add(f);
                        }
                    }
                    if (f.IsNostrum)
                    {
                        ArrayList al = this.SplitNostrumDetail(rInfo, f, ref errText);
                        if (al == null)
                        {
                            return -1;
                        }
                        if (al.Count == 0)
                        {
                            errText = f.Item.Name + "��Э������,����û��ά����ϸ������ϸ�Ѿ�ͣ�ã�������Ϣ����ϵ��";
                            return -1;
                        }
                        itemList.AddRange(al);
                        continue;
                    }
                }
                itemList.Add(f);

            }
            feeItemLists.Clear();
            feeItemLists.AddRange(itemList);
            return 1;
        }
        /// <summary>
        /// ���Э������
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        private ArrayList SplitNostrumDetail(FS.HISFC.Models.Registration.Register rInfo, FeeItemList f, ref string errText)
        {
            List<FS.HISFC.Models.Pharmacy.Nostrum> listDetail = this.pharmarcyManager.QueryNostrumDetail(f.Item.ID);
            ArrayList alTemp = new ArrayList();
            if (listDetail == null)
            {
                errText = "���Э��������ϸ����!" + pharmarcyManager.Err;

                return null;
            }
            decimal price = 0;
            decimal count = 0;
            string feeCode = string.Empty;
            string itemType = string.Empty;
            decimal totCost = 0;
            decimal packQty = 0m;
            FeeItemList feeDetail = null;
            if (f.Order.ID == null || f.Order.ID == string.Empty)
            {
                f.Order.ID = this.orderManager.GetNewOrderID();
                if (f.Order.ID == null || f.Order.ID == string.Empty)
                {
                    errText = "���ҽ����ˮ�ų���!";

                    return null;
                }
            }
            string comboNO = string.Empty;
            if (string.IsNullOrEmpty(f.Order.Combo.ID))
            {
                comboNO = f.Order.Combo.ID;
            }
            else
            {
                comboNO = orderManager.GetNewOrderComboID();
            }
            foreach (FS.HISFC.Models.Pharmacy.Nostrum nosItem in listDetail)
            {
                FS.HISFC.Models.Pharmacy.Item item = pharmarcyManager.GetItem(nosItem.Item.ID);
                if (item == null)
                {
                    errText = "����Э��������ϸ����!";

                    continue;
                }

                feeDetail = new FeeItemList();
                feeDetail.Item = item;
                feeCode = item.MinFee.ID;
                try
                {
                    DateTime nowTime = this.outpatientManager.GetDateTimeFromSysDateTime();
                    int age = (int)((new TimeSpan(nowTime.Ticks - rInfo.Birthday.Ticks)).TotalDays / 365);
                    //{B9303CFE-755D-4585-B5EE-8C1901F79450}���ӻ�ȡ�����
                    string priceForm = rInfo.Pact.PriceForm;
                    price = this.feeManager.GetPrice(priceForm, age, item.Price, item.SpecialPrice, item.ChildPrice, item.PriceCollection.PurchasePrice);
                    //if (item.SysClass.ID.ToString() != "PCC")
                    //{
                    //    price = this.GetPrice(priceObj);
                    //}
                    //else
                    //{
                    //    price = item.Price;
                    //}
                    packQty = item.PackQty;
                    price = FS.FrameWork.Public.String.FormatNumber(
                            NConvert.ToDecimal(price / packQty), 4);
                }
                catch (Exception e)
                {
                    errText = e.Message;

                    return null;
                }
                //��ȡ��Э����������С��λ��ȡ,��ϸ���� = �����������Э���������� * ��Ӧ��ϸ��Ŀ���� / Э��������װ��
                if (f.FeePack == "0")//��С��λ
                {
                    count = NConvert.ToDecimal(f.Item.Qty) * nosItem.Qty / f.Item.PackQty;
                }
                else //��ȡ��Э�������԰�װ��λ��ȡ,��ϸ���� = �����������Э���������� * ��Ӧ��ϸ��Ŀ����
                {
                    count = NConvert.ToDecimal(f.Item.Qty) * nosItem.Qty;
                }
                totCost = price * count;

                feeDetail.Patient = f.Patient.Clone();
                feeDetail.Name = feeDetail.Item.Name;
                feeDetail.ID = feeDetail.Item.ID;
                feeDetail.RecipeOper = f.RecipeOper.Clone();
                feeDetail.Item.Price = price;
                feeDetail.Days = NConvert.ToDecimal(f.Days);
                feeDetail.FT.TotCost = totCost;
                feeDetail.Item.Qty = count;
                feeDetail.FeePack = f.FeePack;

                //�Է���ˣ�������Ϲ�����Ҫ���¼���!!!
                feeDetail.FT.OwnCost = totCost;
                feeDetail.ExecOper = f.ExecOper.Clone();
                feeDetail.Item.PriceUnit = item.MinUnit == string.Empty ? "g" : item.MinUnit;
                if (item.IsMaterial)
                {
                    feeDetail.Item.IsNeedConfirm = true;
                }
                else
                {
                    feeDetail.Item.IsNeedConfirm = false;
                }
                feeDetail.Order = f.Order;
                feeDetail.UndrugComb.ID = f.Item.ID;
                feeDetail.UndrugComb.Name = f.Item.Name;
                feeDetail.Order.Combo.ID = f.Order.Combo.ID;
                feeDetail.Item.IsMaterial = f.Item.IsMaterial;
                feeDetail.RecipeSequence = f.RecipeSequence;
                feeDetail.FTSource = f.FTSource;
                feeDetail.FeePack = f.FeePack;
                feeDetail.IsNostrum = true;
                feeDetail.Invoice = f.Invoice;
                feeDetail.InvoiceCombNO = f.InvoiceCombNO;
                feeDetail.NoBackQty = feeDetail.Item.Qty;
                feeDetail.Order.Combo.ID = comboNO;
                //if (this.rInfo.Pact.PayKind.ID == "03")
                //{
                //    FS.HISFC.Models.Base.PactItemRate pactRate = null;

                //    if (pactRate == null)
                //    {
                //        pactRate = this.pactUnitItemRateManager.GetOnepPactUnitItemRateByItem(this.rInfo.Pact.ID, feeDetail.Item.ID);
                //    }
                //    if (pactRate != null)
                //    {
                //        if (pactRate.Rate.PayRate != this.rInfo.Pact.Rate.PayRate)
                //        {
                //            if (pactRate.Rate.PayRate == 1)//�Է�
                //            {
                //                feeDetail.ItemRateFlag = "1";
                //            }
                //            else
                //            {
                //                feeDetail.ItemRateFlag = "3";
                //            }
                //        }
                //        else
                //        {
                //            feeDetail.ItemRateFlag = "2";

                //        }
                //        if (f.ItemRateFlag == "3")
                //        {
                //            feeDetail.OrgItemRate = f.OrgItemRate;
                //            feeDetail.NewItemRate = f.NewItemRate;
                //            feeDetail.ItemRateFlag = "2";
                //        }
                //    }
                //    else
                //    {
                //        if (f.ItemRateFlag == "3")
                //        {

                //            if (rowFindZT["ZF"].ToString() != "1")
                //            {
                //                feeDetail.OrgItemRate = f.OrgItemRate;
                //                feeDetail.NewItemRate = f.NewItemRate;
                //                feeDetail.ItemRateFlag = "2";
                //            }
                //        }
                //        else
                //        {
                //            feeDetail.OrgItemRate = f.OrgItemRate;
                //            feeDetail.NewItemRate = f.NewItemRate;
                //            feeDetail.ItemRateFlag = f.ItemRateFlag;
                //        }
                //    }
                //}

                alTemp.Add(feeDetail);
            }
            if (alTemp.Count > 0)
            {
                if (f.FT.RebateCost > 0)//�м���
                {
                    if (rInfo.Pact.PayKind.ID != "01")
                    {
                        errText = "��ʱ��������Էѻ��߼���!";

                        return null;
                    }
                    //���ⵥ����
                    decimal rebateRate =
                        FS.FrameWork.Public.String.FormatNumber(f.FT.RebateCost / f.FT.OwnCost, 2);
                    decimal tempFix = 0;
                    decimal tempRebateCost = 0;
                    foreach (FeeItemList feeTemp in alTemp)
                    {
                        feeTemp.FT.RebateCost = (feeTemp.FT.OwnCost) * rebateRate;
                        tempRebateCost += feeTemp.FT.RebateCost;
                    }
                    tempFix = f.FT.RebateCost - tempRebateCost;
                    FeeItemList fFix = alTemp[0] as FeeItemList;
                    fFix.FT.RebateCost = fFix.FT.RebateCost + tempFix;
                }
            }
            if (alTemp.Count > 0)
            {
                if (f.SpecialPrice > 0)//�������Է�
                {
                    decimal tempPrice = 0m;
                    string id = string.Empty;
                    foreach (FeeItemList feeTemp in alTemp)
                    {
                        if (feeTemp.Item.Price > tempPrice)
                        {
                            id = feeTemp.Item.ID;
                            tempPrice = feeTemp.Item.Price;
                        }
                    }

                    foreach (FeeItemList fee in alTemp)
                    {
                        if (fee.Item.ID == id)
                        {
                            fee.SpecialPrice = f.SpecialPrice;

                            break;
                        }
                    }
                }
            }

            return alTemp;
        }
        /// <summary>
        /// �����������շ�����
        /// </summary>
        /// <param name="feeItemLists"></param>
        /// <returns></returns>
        private ArrayList GetRecipeSequenceForChk(ArrayList feeItemLists)
        {
            ArrayList list = new ArrayList();

            foreach (FeeItemList f in feeItemLists)
            {
                if (list.IndexOf(f.RecipeSequence) >= 0)
                {
                    continue;
                }
                else
                {
                    list.Add(f.RecipeSequence);
                }
            }

            return list;
        }
    }
    /// <summary>
    /// ҽ��ִ�е�����
    /// </summary>
    public class ExecOrderCompare : IComparer
    {
        #region IComparer ��Ա

        public int Compare(object x, object y)
        {
            FS.HISFC.Models.Order.ExecOrder execOrder1 = x as FS.HISFC.Models.Order.ExecOrder;
            FS.HISFC.Models.Order.ExecOrder execOrder2 = y as FS.HISFC.Models.Order.ExecOrder;

            if (execOrder1.DateUse > execOrder2.DateUse)
            {
                return -1;
            }
            else if (execOrder1.DateUse == execOrder2.DateUse)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }

        #endregion
    }
}
