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
    /// [功能描述: 体检费用类]
    /// </summary>
    public class Fee : FS.FrameWork.Management.Database
    {
        #region 变量
        /// <summary>
        /// 门诊业务层
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.Outpatient outpatientManager = new FS.HISFC.BizLogic.Fee.Outpatient();
        /// <summary>
        /// 费用类业务层 {2CEA3B1D-2E59-44ac-9226-7724413173C5} 对业务层的引用全部改为非静态的变量
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.InPatient inpatientManager = new FS.HISFC.BizLogic.Fee.InPatient();
        /// <summary>
        /// 体检挂号管理类
        /// </summary>
        protected FS.SOC.HISFC.PE.BizLogic.Register registerManager = new FS.SOC.HISFC.PE.BizLogic.Register();
        /// <summary>
        /// 医嘱业务层
        /// </summary>
        protected FS.HISFC.BizLogic.Order.Order orderManager = new FS.HISFC.BizLogic.Order.Order();
        /// <summary>
        /// 药品业务层
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Pharmacy pharmarcyManager = new FS.HISFC.BizProcess.Integrate.Pharmacy();
        /// <summary>
        /// 收费业务层
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Fee feeManager = new FS.HISFC.BizProcess.Integrate.Fee();
        /// <summary>
        /// 床位费管理类
        /// </summary>
        private FS.HISFC.BizLogic.Fee.BedFeeItem feeBedFeeItem = new FS.HISFC.BizLogic.Fee.BedFeeItem();
        /// <summary>
        /// 管理业务层
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();
        /// <summary>
        /// 门诊帐户类业务层
        /// </summary>
        protected static FS.HISFC.BizLogic.Fee.Account accountManager = new FS.HISFC.BizLogic.Fee.Account();
        /// <summary>
        /// 发票业务层
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.InvoiceServiceNoEnum invoiceServiceManager = new FS.HISFC.BizLogic.Fee.InvoiceServiceNoEnum();
        /// <summary>
        /// 门诊医嘱业务层
        /// </summary>
        protected FS.HISFC.BizLogic.Order.OutPatient.Order orderOutpatientManager = new FS.HISFC.BizLogic.Order.OutPatient.Order();
        /// <summary>
        /// 体检业务层
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.PhysicalExamination.ExamiManager examiIntegrate = new FS.HISFC.BizProcess.Integrate.PhysicalExamination.ExamiManager();
        /// <summary>
        /// 控制参数业务层
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
        /// <summary>
        /// item
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.Item itemManager = new FS.HISFC.BizLogic.Fee.Item();
        FS.HISFC.BizProcess.Integrate.Terminal.Confirm confirmIntegrate = new FS.HISFC.BizProcess.Integrate.Terminal.Confirm();
        /// <summary>
        /// 复合项目明细业务层
        /// </summary>
        FS.HISFC.BizLogic.Fee.UndrugPackAge undrugPackAgeMgr = new FS.HISFC.BizLogic.Fee.UndrugPackAge();
        /// <summary>
        /// 分处方
        /// </summary>
        FS.HISFC.BizProcess.Interface.FeeInterface.ISplitRecipe iSplitRecipe = null;
        /// <summary>
        /// //是否启用分诊系统 1 启用 其他 不启用
        /// </summary>
        string pValue = "";
        /// <summary>
        /// 每次用量可否为空
        /// </summary>
        public static bool isDoseOnceCanNull = false;
        /// <summary>
        /// 发票方式
        /// </summary>
        public static string invoiceStytle = "0";
        /// <summary>
        /// 分处方号忽略类别
        /// </summary>
        private static bool isDecSysClassWhenGetRecipeNO = false;
        /// <summary>
        /// 已不用，改用参数
        /// {2D61A81A-AA2A-4655-A2EE-5CEA2A38FF8A}
        /// </summary>
        private bool isTempInvoice = false;
        /// <summary>
        /// 是否取临时发票号
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
        /// 针对收费项目列表按照 系统类别，执行科室，付数 声称处方号
        /// 同一系统类别，统一执行科室，同一付数的项目处方号相同
        /// 对已经分配好处方号的项目不进行重新分配
        /// </summary>
        /// <param name="feeDetails">费用信息</param>
        /// <param name="t">数据库Trans</param>
        /// <param name="errText">错误信息</param>
        /// <returns>false失败 true成功</returns>
        public bool SetRecipeNOOutpatient(FS.HISFC.Models.Registration.Register r, ArrayList feeDetails, ref string errText)
        {
            if (iSplitRecipe == null)
            {
                iSplitRecipe = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.FeeInterface.ISplitRecipe)) as FS.HISFC.BizProcess.Interface.FeeInterface.ISplitRecipe;
            }
            if (iSplitRecipe != null)
            {
                //分处方
                return iSplitRecipe.SplitRecipe(r, feeDetails, ref errText);
            }
            else
            {
                #region 默认的实现
                bool isDealCombNO = false; //是否优先处理组合号
                int noteCounts = 0;        //获得单张处方最多的项目数

                //是否优先处理组合号
                isDealCombNO = controlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.Const.DEALCOMBNO, false, true);

                //获得单张处方最多的项目数, 默认项目数 5
                noteCounts = controlParamIntegrate.GetControlParam<int>(FS.HISFC.BizProcess.Integrate.Const.NOTECOUNTS, false, 5);

                //是否忽略系统类别
                isDecSysClassWhenGetRecipeNO = controlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.Const.DEC_SYS_WHENGETRECIPE, false, false);

                //是否优先处理暂存记录
                bool isDecTempSaveWhenGetRecipeNO = controlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.Const.处方号优先考虑分方记录, false, false);

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


                    if (isDealCombNO)//优先处理组合号，将所有的组合号再重新分组
                    {
                        //挑选出没有组合号的项目
                        foreach (FeeItemList f in temp)
                        {
                            if (f.Order.Combo.ID == null || f.Order.Combo.ID == string.Empty)
                            {
                                noCombAll.Add(f);
                            }
                        }
                        //从整体数组中删除没有组合号的项目
                        foreach (FeeItemList f in noCombAll)
                        {
                            temp.Remove(f);
                        }
                        //针对同一处方最多项目数再重新分组
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
                        //如果剩余的项目条目> 0说明还有组合的项目
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
                            string recipeNo = null;//处方流水号
                            int noteSeq = 1;//处方内项目流水号

                            string tempRecipeNO = string.Empty;
                            int tempSequence = 0;
                            this.GetRecipeNoAndMaxSeq(tempNoComb, ref tempRecipeNO, ref tempSequence, r.ID);

                            if (tempRecipeNO != string.Empty && tempSequence > 0)
                            {
                                tempSequence += 1;
                                foreach (FeeItemList f in tempNoComb)
                                {
                                    feeDetails.Add(f);
                                    if (f.RecipeNO != null && f.RecipeNO != string.Empty)//已经分配处方号
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
                                    errText = "获得处方号出错!";
                                    return false;
                                }
                                foreach (FeeItemList f in tempNoComb)
                                {
                                    feeDetails.Add(f);
                                    if (f.RecipeNO != null && f.RecipeNO != string.Empty)//已经分配处方号
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
                            string recipeNo = null;//处方流水号
                            int noteSeq = 1;//处方内项目流水号

                            string tempRecipeNO = string.Empty;
                            int tempSequence = 0;
                            this.GetRecipeNoAndMaxSeq(tempComb, ref tempRecipeNO, ref tempSequence, r.ID);

                            if (tempRecipeNO != string.Empty && tempSequence > 0)
                            {
                                tempSequence += 1;
                                foreach (FeeItemList f in tempComb)
                                {
                                    feeDetails.Add(f);
                                    if (f.RecipeNO != null && f.RecipeNO != string.Empty)//已经分配处方号
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
                                    errText = "获得处方号出错!";
                                    return false;
                                }
                                foreach (FeeItemList f in tempComb)
                                {
                                    feeDetails.Add(f);
                                    if (f.RecipeNO != null && f.RecipeNO != string.Empty)//已经分配处方号
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
                    else //不优先处理组合号
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
                            string recipeNO = null;//处方流水号
                            int recipeSequence = 1;//处方内项目流水号

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
                                    if (f.RecipeNO != null && f.RecipeNO != string.Empty)//已经分配处方号
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
                                    errText = "获得处方号出错!";
                                    return false;
                                }
                                foreach (FeeItemList f in tempCounts)
                                {
                                    feeDetails.Add(f);
                                    if (f.RecipeNO != null && f.RecipeNO != string.Empty)//已经分配处方号
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
        /// 获得[最大的流水号]和处方号
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

        #region 处方明细操作
        /// <summary>
        /// 插入费用明细
        /// </summary>
        /// <param name="feeItemList">费用明细实体</param>
        /// <returns>成功: 1 失败: -1 没有插入数据返回 0</returns>
        public int InsertFeeItemList(FeeItemList feeItemList)
        {
            return this.UpdateSingleTable("Fee.Item.GetFeeItemDetail.Insert", this.GetFeeItemListParams(feeItemList));
        }
        /// <summary>
        /// 更新费用明细
        /// </summary>
        /// <param name="feeItemList">费用明细实体</param>
        /// <returns>成功: 1 失败: -1 没有更新到数据返回 0</returns>
        private int UpdateFeeItemList(FeeItemList feeItemList)
        {
            return this.UpdateSingleTable("Fee.OutPatient.ItemDetail.Update", this.GetFeeItemListParams(feeItemList));
        }
        /// <summary>
        /// 获得insert表的传入参数数组update
        /// </summary>
        /// <param name="feeItemList">费用明细实体</param>
        /// <returns>字符串数组</returns>
        private string[] GetFeeItemListParams(FeeItemList feeItemList)
        {
            //{143CA424-7AF9-493a-8601-2F7B1D635027}
            string[] args = new string[86];	//{3AEB5613-1CB0-4158-89E6-F82F0B643388}				 

            args[0] = feeItemList.RecipeNO;//RECIPE_NO,	--		处方号							0
            args[1] = feeItemList.SequenceNO.ToString();	  //SEQUENCE_NO;	--		处方内项目流水号				1
            args[2] = ((int)feeItemList.TransType).ToString();//TRANS_TYPE;	--		交易类型;1正交易，2反交易		2
            args[3] = feeItemList.Patient.ID;//CLINIC_CODE;	--		门诊号								3	
            args[4] = feeItemList.Patient.PID.CardNO;//CARD_NO;	--		病历卡号									4		
            args[5] = ((FS.HISFC.Models.Registration.Register)feeItemList.Patient).DoctorInfo.SeeDate.ToString();//REG_DATE;	--		挂号日期						5	
            args[6] = ((FS.HISFC.Models.Registration.Register)feeItemList.Patient).DoctorInfo.Templet.Dept.ID;//REG_DPCD;	--		挂号科室							6	
            args[7] = feeItemList.RecipeOper.ID;//DOCT_CODE;	--		开方医师							7
            args[8] = feeItemList.RecipeOper.Dept.ID;//DOCT_DEPT;	--		开方医师所在科室				8
            args[9] = feeItemList.Item.ID;//ITEM_CODE;	--		项目代码									9.
            args[10] = feeItemList.Item.Name;//ITEM_NAME;	--		项目名称									10
            //args[11] = NConvert.ToInt32(feeItemList.Item.IsPharmacy).ToString();//DRUG_FLAG;	--		1药品/0非要					11
            args[11] = ((int)(feeItemList.Item.ItemType)).ToString();
            args[12] = feeItemList.Item.Specs;//SPECS;		--		规格										12
            //if (feeItemList.Item.IsPharmacy)
            if (feeItemList.Item.ItemType == EnumItemType.Drug)
            {
                args[13] = NConvert.ToInt32(((FS.HISFC.Models.Pharmacy.Item)feeItemList.Item).Product.IsSelfMade).ToString();//SELF_MADE;	--		自制药标志					13
                args[14] = ((FS.HISFC.Models.Pharmacy.Item)feeItemList.Item).Quality.ID;//DRUG_QUALITY;	--		药品性质，麻药，普药		14	
                args[15] = ((FS.HISFC.Models.Pharmacy.Item)feeItemList.Item).DosageForm.ID;//DOSE_MODEL_CODE;--		剂型							15.
            }
            args[16] = feeItemList.Item.MinFee.ID;//FEE_CODE;	--		最小费用代码							16	
            args[17] = feeItemList.Item.SysClass.ID.ToString();//CLASS_CODE;	--		系统类别				17	
            args[18] = feeItemList.Item.Price.ToString();//UNIT_PRICE;	--		单价							18	
            args[19] = feeItemList.Item.Qty.ToString();//QTY;		--		数量								19	
            args[20] = feeItemList.Days.ToString();//DAYS;		--		草药的付数，其他药品为1			20	
            args[21] = feeItemList.Order.Frequency.ID;//FREQUENCY_CODE;	--		频次代码						21	
            args[22] = feeItemList.Order.Usage.ID;//USAGE_CODE;	--		用法代码							22	
            args[23] = feeItemList.Order.Usage.Name;//USE_NAME;	--		用法名称							23
            args[24] = feeItemList.InjectCount.ToString();//INJECT_NUMBER;	--		院内注射次数		24	
            args[25] = NConvert.ToInt32(feeItemList.IsUrgent).ToString();//EMC_FLAG;	--		加急标记:1加急/0普通			25	
            args[26] = feeItemList.Order.Sample.ID;//LAB_TYPE;	--		样本类型							26	
            args[27] = feeItemList.Order.CheckPartRecord;//CHECK_BODY;	--		检体								27	
            args[28] = feeItemList.Order.DoseOnce.ToString();//DOSE_ONCE;	--		每次用量					28
            args[29] = feeItemList.Order.DoseUnit;//DOSE_UNIT;	--		每次用量单位							29
            //if (feeItemList.Item.IsPharmacy)
            if (feeItemList.Item.ItemType == EnumItemType.Drug)
            {
                args[30] = ((FS.HISFC.Models.Pharmacy.Item)feeItemList.Item).BaseDose.ToString();//BASE_DOSE;	--		基本剂量					30
            }
            args[31] = feeItemList.Item.PackQty.ToString();//PACK_QTY;	--		包装数量						31	
            args[32] = feeItemList.Item.PriceUnit;//PRICE_UNIT;	--		计价单位							32	
            args[33] = feeItemList.FT.PubCost.ToString();//PUB_COST;	--		可报效金额				33	
            args[34] = feeItemList.FT.PayCost.ToString();//PAY_COST;	--		自付金额				34	
            args[35] = feeItemList.FT.OwnCost.ToString();//OWN_COST;	--		现金金额				35	
            args[36] = feeItemList.ExecOper.Dept.ID;//EXEC_DPCD;	--		执行科室代码					36
            args[37] = feeItemList.ExecOper.Dept.Name;//EXEC_DPNM;	--		执行科室名称					37
            args[38] = feeItemList.Compare.CenterItem.ID;//CENTER_CODE;	--		医保中心项目代码				38	
            args[39] = feeItemList.Compare.CenterItem.ItemGrade;//ITEM_GRADE;	--		项目等级1甲类2乙类3丙类		39	
            args[40] = NConvert.ToInt32(feeItemList.Order.Combo.IsMainDrug).ToString();//MAIN_DRUG;	--		主药标志					40
            args[41] = feeItemList.Order.Combo.ID;//COMB_NO;	--		组合号										41	
            args[42] = feeItemList.ChargeOper.ID;//OPER_CODE;	--		划价人							42
            args[43] = feeItemList.ChargeOper.OperTime.ToString();//OPER_DATE;	--		划价时间					43
            args[44] = ((int)feeItemList.PayType).ToString();// //PAY_FLAG;	--		收费标志，1未收费，2收费	44	
            args[45] = ((int)feeItemList.CancelType).ToString();
            args[46] = feeItemList.FeeOper.ID;//FEE_CPCD;	--		收费员代码							46	
            args[47] = feeItemList.FeeOper.OperTime.ToString();//FEE_DATE;	--		收费日期					47	
            args[48] = feeItemList.Invoice.ID;//INVOICE_NO;	--		票据号								48	
            args[49] = feeItemList.FeeCodeStat.ID;//INVO_CODE;	--		发票科目代码				49
            args[50] = feeItemList.FeeCodeStat.SortID.ToString();//INVO_SEQUENCE;	--		发票内流水号		50
            args[51] = NConvert.ToInt32(feeItemList.IsConfirmed).ToString();//CONFIRM_FLAG;	--		1未确认/2确认				51		
            args[52] = feeItemList.ConfirmOper.ID;//CONFIRM_CODE;	--		确认人						52		
            args[53] = feeItemList.ConfirmOper.Dept.ID;//CONFIRM_DEPT;	--		确认科室					53	
            args[54] = feeItemList.ConfirmOper.OperTime.ToString();//CONFIRM_DATE;	--		确认时间				54	
            args[55] = feeItemList.FT.RebateCost.ToString();// ECO_COST -- 优惠金额 55
            args[56] = feeItemList.InvoiceCombNO;//发票序号，一次结算产生多张发票的combNo  56
            args[57] = feeItemList.NewItemRate.ToString();//新项目比例  57
            args[58] = feeItemList.OrgItemRate.ToString();//原项目比例  58 
            args[59] = feeItemList.ItemRateFlag;//扩展标志 特殊项目标志 1自费 2 记帐 3 特殊  59
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
            //物资出库流水号
            args[76] = NConvert.ToInt32(feeItemList.UpdateSequence).ToString();
            //开立医生所属科室
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
            args[83] = feeItemList.Order.Memo;//处方备注
            args[84] = feeItemList.Memo;//费用备注
            args[85] = feeItemList.FT.FTRate.User03;//Extflag3
            //看诊序号
            //args[79] = feeItemList.SeeNo;

            return args;
        }
        #endregion 

        /// <summary>
        /// 门诊明细数据校验
        /// </summary>
        /// <param name="f">费用实体</param>
        /// <param name="errText">错误信息</param>
        /// <returns>成功 true 失败 false</returns>
        public bool IsFeeItemListDataValid(FeeItemList f, ref string errText)
        {
            string itemName = f.Item.Name;
            if (f == null)
            {
                errText = itemName + "获得费用实体出错!";

                return false;
            }
            if (f.Item.ID == null || f.Item.ID == string.Empty)
            {
                errText = itemName + "项目编码没有付值";

                return false;
            }
            if (f.Item.Name == null || f.Item.Name == string.Empty)
            {
                errText = itemName + "项目名称没有付值";

                return false;
            }
            //if (f.Item.IsPharmacy)
            if (f.Item.ItemType == EnumItemType.Drug && f.FTSource != "0")
            {
                #region 根据参数&& !isDoseOnceCanNull 来判断是否需要输入各个值 刘兴强20070828
                if ((f.Order.Frequency.ID == null || f.Order.Frequency.ID == string.Empty) && (f.FTSource == "1" || (f.FTSource == "0" && !isDoseOnceCanNull)))
                {
                    errText = itemName + "频次代码没有付值";

                    return false;
                }
                if ((f.Order.Usage.ID == null || f.Order.Usage.ID == string.Empty) && (f.FTSource == "1" || (f.FTSource == "0" && !isDoseOnceCanNull)))
                {
                    errText = itemName + "用法代码没有付值";

                    return false;
                }
                if (f.Order.DoseOnce == 0 && (f.FTSource == "1" || (f.FTSource == "0" && !isDoseOnceCanNull)))
                {
                    errText = itemName + "每次用量没有付值";

                    return false;
                }
                if ((f.Order.DoseUnit == null || f.Order.DoseUnit == string.Empty) && (f.FTSource == "1" || (f.FTSource == "0" && !isDoseOnceCanNull)))
                {
                    errText = itemName + "每次用量单位没有付值";

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
                    //    errText = itemName + "的规格没有付值";

                    //    return false;
                    //}
                    if (f.Item.PackQty == 0)
                    {
                        errText = itemName + "包装数量没有付值";

                        return false;
                    }
                }
            }
            if (f.Item.PriceUnit == null || f.Item.PriceUnit == string.Empty)
            {
                errText = itemName + "计价单位没有付值";

                return false;
            }

            if (f.Item.MinFee.ID == null || f.Item.MinFee.ID == string.Empty)
            {
                errText = itemName + "最小费用没有付值";

                return false;
            }
            if (f.Item.SysClass.ID == null || f.Item.SysClass.Name == string.Empty)
            {
                errText = itemName + "系统类别没有付值";

                return false;
            }
            if (f.Item.Qty == 0)
            {
                errText = itemName + "数量没有付值";

                return false;
            }
            //四舍五入费用处理，暂时屏蔽{DE54BEAE-EF40-4aa4-8DF5-8CCB2A3DDA1D}
            //if (f.Item.Qty < 0)
            //{
            //    errText = itemName + "数量不能小于0";

            //    return false;
            //}
            if (f.Item.Qty > 99999)
            {
                errText = itemName + "数量不能大于99999";

                return false;
            }

            if (f.Days == 0)
            {
                errText = itemName + "草药付数没有付值";

                return false;
            }
            if (f.Days < 0)
            {
                errText = itemName + "草药付数不能小于0";

                return false;
            }

            if (f.Item.Price < 0)
            {
                errText = itemName + "单价不能小于0";

                return false;
            }

            //对于自备药等 允许收取费用为0项目
            if (f.Item.ID != "999")
            {
                if (f.Item.Price == 0 && f.Item.User03 != "全免")
                {
                    errText = itemName + "单价没有付值";

                    return false;
                }
                //if (f.FT.OwnCost + f.FT.PayCost + f.FT.PubCost == 0)
                //{
                //    errText = itemName + "项目金额没有付值";

                //    return false;
                //}
            }

            //四舍五入费用处理，暂时屏蔽{DE54BEAE-EF40-4aa4-8DF5-8CCB2A3DDA1D}
            //if (f.FT.OwnCost + f.FT.PayCost + f.FT.PubCost < 0)
            //{
            //    errText = itemName + "项目金额为负";

            //    return false;
            //}
            ////{8DF48FD8-14E9-464a-A368-256B19A0EE54} 修改又会比例
            //if (FS.FrameWork.Public.String.FormatNumber(f.Item.Price * f.Item.Qty / f.Item.PackQty, 2) != FS.FrameWork.Public.String.FormatNumber
            //    (f.FT.OwnCost + f.FT.PayCost + f.FT.PubCost /*+ f.FT.RebateCost*/, 2))
            //{
            //    errText = itemName + "金额与单价数量不符";

            //    return false;
            //}

            if (f.Item.ID == "999" && f.Item.ItemType == EnumItemType.Drug)
            {
            }
            else
            {
                if (f.ExecOper.Dept.ID == null || f.ExecOper.Dept.ID == string.Empty)
                {
                    errText = itemName + "执行科室代码没有付值";

                    return false;
                }
                if (f.ExecOper.Dept.Name == null || f.ExecOper.Dept.Name == string.Empty)
                {
                    errText = itemName + "执行科室名称没有付值";

                    return false;
                }
            }

            return true;
        }
        /// <summary>
        /// 获得非药品信息（不管是否有效）
        /// </summary>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public FS.HISFC.Models.Fee.Item.Undrug GetItem(string itemCode)
        {
            return itemManager.GetItemByUndrugCode(itemCode);
        }

        #region 获取体检项目明细信息
        /// <summary>
        /// 根据处方号和项目流水号获得项目明细实体(已经收费信息)
        /// </summary>
        /// <param name="recipeNO">处方号</param>
        /// <param name="sequenceNO">处方内流水号</param>
        /// <returns>成功:费用明细实体 失败或者没有数据:null</returns>
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
        /// 根据结算序列获得费用明细
        /// </summary>
        /// <param name="invoiceSequence"></param>
        /// <returns></returns>
        public ArrayList QueryFeeItemListsByInvoiceSequence(string invoiceSequence)
        {
            return this.QueryFeeItemLists("Fee.OutPatient.GetInvoInfo.Where.Seq", invoiceSequence);
        }
        /// <summary>
        /// 根据Where条件的索引查询费用明细信息
        /// </summary>
        /// <param name="whereIndex">Where条件索引</param>
        /// <param name="args">参数</param>
        /// <returns>成功:费用明细 失败:null 没有数据:返回元素数为0的ArrayList</returns>
        private ArrayList QueryFeeItemLists(string whereIndex, params string[] args)
        {
            string sql = string.Empty;//SELECT语句
            string where = string.Empty;//WHERE语句

            //获得Where语句
            if (this.Sql.GetCommonSql(whereIndex, ref where) == -1)
            {
                this.Err = "没有找到索引为:" + whereIndex + "的SQL语句";

                return null;
            }

            sql = this.GetSqlFeeDetail();

            return this.QueryFeeDetailBySql(sql + " " + where, args);
        }
        
        #region 明细检索
        /// <summary>
        /// 获得体检明细的sql语句
        /// </summary>
        /// <returns>返回查询费用明细SQL语句</returns>
        private string GetSqlFeeDetail()
        {
            string sql = string.Empty;//查询SQL语句的SELECT部分

            if (this.Sql.GetCommonSql("Fee.Item.GetFeeItem", ref sql) == -1)
            {
                this.Err = "没有找到索引为Fee.Item.GetFeeItem的SQL语句";

                return null;
            }

            return sql;
        }
        /// <summary>
        /// 通过SQL语句获得费用明细信息
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="args">SQL参数</param>
        /// <returns>成功:费用明细集合 失败: null 没有查找到数据: 元素数为0的ArrayList</returns>
        private ArrayList QueryFeeDetailBySql(string sql, params string[] args)
        {
            if (this.ExecQuery(sql, args) == -1)
            {
                return null;
            }

            ArrayList feeItemLists = new ArrayList();//费用明细数组
            FeeItemList feeItemList = null;//费用明细实体

            try
            {
                //循环读取数据
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
                    //物资 {40DFDC91-0EC1-4cd4-81BC-0EAE4DE1D3AB}
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

                    //扣库科室
                    feeItemList.StockOper.Dept.ID = feeItemList.ConfirmOper.Dept.ID;//扣库科室

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
                    //物资出库流水号
                    feeItemList.UpdateSequence = NConvert.ToInt32(this.Reader[76].ToString());

                    //判断77（结算类别）是否存在
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
                }//循环结束

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
        /// 根据原始发票号更新费用明细的有效标志
        /// </summary>
        /// <param name="orgInvoiceNO">原始发票号</param>
        /// <param name="operTime">操作时间</param>
        /// <param name="cancelType">作废类型</param>
        /// <returns>成功; >= 1 失败: -1 没有更新到数据: 0</returns>
        public int UpdateFeeItemListCancelType(string orgInvoiceNO, DateTime operTime, CancelTypes cancelType)
        {
            return this.UpdateSingleTable("Fee.OutPatient.UpdateFeeDetailCancelFlag.1", orgInvoiceNO, operTime.ToString(), ((int)cancelType).ToString());
        }
        /// <summary>
        /// 门诊收费函数
        /// 
        /// {A87CA138-33E8-47d0-92BD-7A65A08DDCF5}
        /// 
        /// </summary>
        /// <param name="type">收费,划价标志</param>
        /// <param name="r">患者挂号基本信息</param>
        /// <param name="invoices">发票主表集合</param>
        /// <param name="invoiceDetails">发票明细表集合</param>
        /// <param name="feeDetails">费用明细集合</param>
        /// <param name="t">Transcation</param>
        /// <param name="payModes">支付方式集合</param>
        /// <param name="errText">错误信息</param>
        /// <returns></returns>
        public bool ClinicFee(FS.HISFC.Models.Base.ChargeTypes type, FS.HISFC.Models.Registration.Register r,
            ArrayList invoices, ArrayList invoiceDetails, ArrayList feeDetails, ArrayList invoiceFeeDetails, ArrayList payModes, ref string errText)
        {
            Employee oper = this.outpatientManager.Operator as Employee;
            return this.ClinicFee(type, "C", false, r, invoices, invoiceDetails, feeDetails, invoiceFeeDetails, payModes, ref errText, oper);
        }
        /// <summary>
        /// 门诊收费函数
        /// 
        /// {69245A77-FB7A-42ed-844B-855E7ABC612F}
        /// 
        /// </summary>
        /// <param name="type">收费,划价标志</param>
        /// <param name="isTempInvoice">是否使用临时发票</param>
        /// <param name="r">患者挂号基本信息</param>
        /// <param name="invoices">发票主表集合</param>
        /// <param name="invoiceDetails">发票明细表集合</param>
        /// <param name="feeDetails">费用明细集合</param>
        /// <param name="invoiceFeeDetails">发票明细信息</param>
        /// <param name="payModes">支付方式集合</param>
        /// <param name="errText">错误信息</param>
        /// <returns></returns>
        public bool ClinicFee(FS.HISFC.Models.Base.ChargeTypes type, bool isTempInvoice, FS.HISFC.Models.Registration.Register r,
            ArrayList invoices, ArrayList invoiceDetails, ArrayList feeDetails, ArrayList invoiceFeeDetails, ArrayList payModes, ref string errText)
        {
            Employee oper = this.outpatientManager.Operator as Employee;
            return this.ClinicFee(type, "C", isTempInvoice, r, invoices, invoiceDetails, feeDetails, invoiceFeeDetails, payModes, ref errText, oper);
        }
        /// <summary>
        /// 门诊收费函数
        /// 
        /// {A87CA138-33E8-47d0-92BD-7A65A08DDCF5} 
        /// 增加参数，允许指定发票类型
        /// </summary>
        /// <param name="type">收费,划价标志</param>
        /// <param name="invoiceType">发票类型R:挂号收据 C:门诊收据 P:预交收据 I:住院收据 A:门诊账户</param>
        /// <param name="isTempInvoice">是否使用临时发票</param>
        /// <param name="r">患者挂号基本信息</param>
        /// <param name="invoices">发票主表集合</param>
        /// <param name="invoiceDetails">发票明细表集合</param>
        /// <param name="feeDetails">费用明细集合</param>
        /// <param name="invoiceFeeDetails">发票明细信息</param>
        /// <param name="payModes">支付方式集合</param>
        /// <param name="errText">错误信息</param>
        /// <param name="oper">操作者</param>
        /// <returns></returns>
        public bool ClinicFee(FS.HISFC.Models.Base.ChargeTypes type, string invoiceType, bool isTempInvoice, FS.HISFC.Models.Registration.Register r,
            ArrayList invoices, ArrayList invoiceDetails, ArrayList feeDetails, ArrayList invoiceFeeDetails, ArrayList payModes, ref string errText, Employee oper)
        {

            //Terminal.Confirm confirmIntegrate = new FS.HISFC.BizProcess.Integrate.Terminal.Confirm();
            FS.HISFC.BizProcess.Integrate.Terminal.Booking bookingIntegrate = new FS.HISFC.BizProcess.Integrate.Terminal.Booking();
            //SOC.HISFC.BizLogic.Pharmacy.Item socItemMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.Item();

            invoiceStytle = controlParamIntegrate.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.Const.GET_INVOICE_NO_TYPE, false, "0");

            isDoseOnceCanNull = controlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.Const.DOSE_ONCE_NULL, false, true);

            //是否才分协定处方
            bool isSplitNostrum = controlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.Const.Split_NostrumDetail, false, false);

            //获得收费时间
            DateTime feeTime = inpatientManager.GetDateTimeFromSysDateTime();

            //获得收费操作员
            string operID = inpatientManager.Operator.ID;

            FS.HISFC.Models.Base.Employee employee = inpatientManager.Operator as FS.HISFC.Models.Base.Employee;
            //返回值
            int iReturn = 0;
            //定义处方号
            string recipeNO = string.Empty;

            //如果是收费，获得发票信息
            if (type == FS.HISFC.Models.Base.ChargeTypes.Fee)//收费
            {
                #region 收费流程
                //发票已经在预览界面分配完毕,直接插入就可以了.

                #region//获得发票序列,多张发票发票号不同

                string invoiceCombNO = outpatientManager.GetInvoiceCombNO();
                if (invoiceCombNO == null || invoiceCombNO == string.Empty)
                {
                    errText = "获得发票流水号失败!" + outpatientManager.Err;

                    return false;
                }
                //获得特殊显示类别
                /////GetSpDisplayValue(myCtrl, t);
                //第一个发票号
                string mainInvoiceNO = string.Empty;
                string mainInvoiceCombNO = string.Empty;
                foreach (Balance balance in invoices)
                {
                    //主发票信息,不插入只做显示用
                    if (balance.Memo == "5")
                    {
                        mainInvoiceNO = balance.ID;

                        continue;
                    }

                    //自费患者不需要显示主发票,那么取第一个发票号作为主发票号
                    if (mainInvoiceNO == string.Empty)
                    {
                        mainInvoiceNO = balance.Invoice.ID;
                        mainInvoiceCombNO = balance.CombNO;
                    }
                }

                #endregion

                #region //插入发票明细表

                foreach (ArrayList tempsInvoices in invoiceDetails)
                {
                    foreach (ArrayList tempDetals in tempsInvoices)
                    {
                        foreach (BalanceList balanceList in tempDetals)
                        {
                            //总发票处理
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

                            //插入发票明细表 fin_opb_invoicedetail
                            iReturn = outpatientManager.InsertBalanceList(balanceList);
                            if (iReturn == -1)
                            {
                                errText = "插入发票明细出错!" + outpatientManager.Err;

                                return false;
                            }
                        }
                    }
                }

                #endregion

                #region 协定处方
                ArrayList noSplitDrugList = new ArrayList();
                if (isSplitNostrum)
                {

                    if (SplitNostrumDetail(r, ref feeDetails, ref noSplitDrugList, ref errText) < 0)
                    {
                        return false;
                    }
                }

                #endregion

                #region//药品信息列表,生成处方号

                ArrayList drugLists = new ArrayList();
                //重新生成处方号,如果已有处方号,明细不重新赋值.
                if (!this.SetRecipeNOOutpatient(r, feeDetails, ref errText))
                {
                    return false;
                }

                #endregion

                #region//插入费用明细
                foreach (FeeItemList f in feeDetails)
                {
                    //验证数据
                    if (!this.IsFeeItemListDataValid(f, ref errText))
                    {
                        return false;
                    }
                    //如果没有处方号,重新赋值
                    if (f.RecipeNO == null || f.RecipeNO == string.Empty)
                    {
                        if (recipeNO == string.Empty)
                        {
                            recipeNO = outpatientManager.GetRecipeNO();
                            if (recipeNO == null || recipeNO == string.Empty)
                            {
                                errText = "获得处方号出错!";

                                return false;
                            }
                        }
                    }

                    #region 2007-8-29 liuq 判断是否已有发票号序号，没有则赋值
                    //{1A5CC61F-01F9-4dee-A6A8-580200C10EB4}
                    if (string.IsNullOrEmpty(f.InvoiceCombNO) || f.InvoiceCombNO == "NULL")
                    {
                        f.InvoiceCombNO = invoiceCombNO;
                    }
                    #endregion
                    //
                    #region 2007-8-28 liuq 判断是否已有发票号，没有初始化为12个0
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
                            errText = "请选择医生";
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

                    //如果患者为团体体检，那么所有项目都插入终端审核。
                    if (r.ChkKind == "2")
                    {
                        if (!f.IsConfirmed)
                        {
                            //如果项目流水号为空，说明没有经过划价流程，那么插入终端审核信息。
                            if (f.Order.ID == null || f.Order.ID == string.Empty)
                            {
                                f.Order.ID = orderManager.GetNewOrderID();
                                if (f.Order.ID == null || f.Order.ID == string.Empty)
                                {
                                    errText = "获得医嘱流水号出错!";
                                    return false;
                                }

                                FS.HISFC.BizProcess.Integrate.Terminal.Result result = confirmIntegrate.ServiceInsertTerminalApply(f, r);

                                if (result != FS.HISFC.BizProcess.Integrate.Terminal.Result.Success)
                                {
                                    errText = "处理终端申请确认表失败!";

                                    return false;
                                }
                            }
                        }
                    }
                    else//其他患者如果项目为需要终端审核项目则插入终端审核信息。
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
                                            errText = "获得医嘱流水号出错!";
                                            return false;
                                        }

                                        FS.HISFC.BizProcess.Integrate.Terminal.Result result = confirmIntegrate.ServiceInsertTerminalApply(f, r);
                                        if (result != FS.HISFC.BizProcess.Integrate.Terminal.Result.Success)
                                        {
                                            errText = "处理终端申请确认表失败!" + confirmIntegrate.Err;
                                            return false;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    //没有付值医嘱流水号,赋值新的医嘱流水号
                    if (f.Order.ID == null || f.Order.ID == string.Empty)
                    {
                        f.Order.ID = orderManager.GetNewOrderID();
                        if (f.Order.ID == null || f.Order.ID == string.Empty)
                        {
                            errText = "获得医嘱流水号出错!";
                            return false;
                        }
                    }

                    if (r.ChkKind == "1")//个人体检更新收费标记
                    {
                        iReturn = examiIntegrate.UpdateItemListFeeFlagByMoOrder("1", f.Order.ID);
                        if (iReturn == -1)
                        {
                            errText = "更新体检收费标记失败!" + examiIntegrate.Err;
                            return false;
                        }
                    }

                    //如果删除划价保存中的组合项目主项目信息,保留明细.
                    if (f.UndrugComb.ID != null && f.UndrugComb.ID.Length > 0)
                    {
                        iReturn = outpatientManager.DeletePackageByMoOrder(f.Order.ID);
                        if (iReturn == -1)
                        {
                            errText = "删除组套失败!" + outpatientManager.Err;
                            return false;
                        }
                        //不知道谁修改的，偶尔删除组套费用失败...
                        //前面已经把组套医嘱的id放入费用的User03，此处再删一次  houwb
                        else if (iReturn == 0)
                        {
                            iReturn = outpatientManager.DeletePackageByMoOrder(f.User03);
                            if (iReturn == -1)
                            {
                                errText = "删除组套失败!" + outpatientManager.Err;
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
                        errText = "查询项目信息失败！";
                        return false;
                    }

                    if (chargeItemCount == 0)//说明不存在
                    //if(feeTemp == null)
                    {
                        if (f.FTSource != "3" && (f.UndrugComb.ID == null || f.UndrugComb.ID == string.Empty))
                        {
                            errText = f.Item.Name + "可能已经被其他操作员删除,请刷新后再收费!";

                            return false;
                        }

                        iReturn = outpatientManager.InsertFeeItemList(f);
                        if (iReturn <= 0)
                        {
                            errText = "插入费用明细失败!" + outpatientManager.Err;

                            return false;
                        }
                    }
                    else
                    {
                        iReturn = outpatientManager.UpdateFeeItemList(f);
                        if (iReturn <= 0)
                        {
                            errText = "更新费用明细失败!" + outpatientManager.Err;

                            return false;
                        }
                    }

                    #region//回写医嘱信息

                    if (f.FTSource == "1")
                    {
                        iReturn = orderOutpatientManager.UpdateOrderChargedByOrderID(f.Order.ID, operID);
                        if (iReturn <= 0 && !f.Item.IsMaterial && f.Item.ItemType == EnumItemType.Drug)
                        {
                            errText = "没有更新到医嘱信息，请向医生确认是否已经删除该医嘱:" + f.Item.Name + ",或重新刷卡调出该患者收费信息." + orderOutpatientManager.Err;

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
                                        errText = "【" + order.Item.Name + "】 收费数量与医生开立数量不同!";

                                        return false;
                                    }
                                }
                                else
                                {
                                    if (order.Qty != f.Item.Qty)
                                    {
                                        errText = "【" + order.Item.Name + "】 收费数量与医生开立数量不同!";

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
                                //如果是复合项目
                                if (!string.IsNullOrEmpty(f.UndrugComb.ID))
                                {
                                    //取复合项目维护的明细数量
                                    FS.HISFC.Models.Fee.Item.UndrugComb undrugCombo = undrugPackAgeMgr.GetUndrugComb(f.UndrugComb.ID, f.Item.ID);
                                    if (undrugCombo == null)
                                    {
                                        errText = "获取复合项目" + f.UndrugComb.ID + "的非药品项目：" + f.Item.ID + "失败，原因：" + itemManager.Err;
                                        return false;
                                    }

                                    if (order.Qty != f.Item.Qty / undrugCombo.Qty)
                                    {
                                        errText = "【" + order.Item.Name + "】 收费数量与医生开立数量不同!";

                                        return false;
                                    }
                                }
                                else
                                {
                                    if (order.Qty != f.Item.Qty)
                                    {
                                        errText = "收费数量与医生开立数量不同!";

                                        return false;
                                    }
                                }
                            }
                        }
                    }

                    #endregion

                    #region 加入发药申请列表

                    //如果是药品,并且没有被确认过,而且不需要终端确认,那么加入发药申请列表.
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

                    #region 插入医技预约表

                    //需要医技预约,插入终端预约信息.
                    if (f.Item.IsNeedBespeak && r.ChkKind != "2")
                    {
                        iReturn = bookingIntegrate.Insert(f);

                        if (iReturn == -1)
                        {
                            errText = "插入医技预约信息出错!" + f.Name + bookingIntegrate.Err;

                            return false;
                        }
                    }
                    #endregion
                }

                #endregion

                #region 集体体检更新收费标记

                if (r.ChkKind == "2")//集体体检
                {
                    ArrayList recipeSeqList = this.GetRecipeSequenceForChk(feeDetails);
                    if (recipeSeqList != null && recipeSeqList.Count > 0)
                    {
                        foreach (string recipeSequenceTemp in recipeSeqList)
                        {
                            iReturn = examiIntegrate.UpdateItemListFeeFlagByRecipeSeq("1", recipeSequenceTemp);
                            if (iReturn == -1)
                            {
                                errText = "更新体检收费标记失败!" + examiIntegrate.Err;

                                return false;
                            }

                        }
                    }
                }

                #endregion

                #region//发药窗口信息

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
                //插入发药申请信息,返回发药窗口,显示在发票上
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
                    errText = "处理药品明细失败!" + pharmarcyManager.Err;

                    return false;
                }

                //'如果有药品,那么设置发票的显示发药窗口信息.
                if (drugLists.Count > 0)
                {
                    //{02F6E9D7-E311-49a4-8FE4-BF2AC88B889B}屏蔽掉小版本代码，采用核心版本的代码
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

                #region//插入发票主表

                foreach (Balance balance in invoices)
                {
                    //主发票信息,不插入只做显示用
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
                    //体检标志
                    string tempExamineFlag = null;
                    //获得体检标志 0 普通患者 1 个人体检 2 团体体检
                    //如果没有赋值,默认为普通患者
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

                    //=====去掉CanceledInvoiceNO=string.Empty 路志鹏================
                    //balance.CanceledInvoiceNO = string.Empty;
                    //==============================================================

                    balance.IsAuditing = false;
                    balance.IsDayBalanced = false;
                    balance.ID = balance.ID.PadLeft(12, '0');
                    balance.Patient.Pact.Memo = r.User03;//限额代码
                    //自费患者不需要显示主发票,那么取第一个发票号作为主发票号
                    if (mainInvoiceNO == string.Empty)
                    {
                        mainInvoiceNO = balance.Invoice.ID;
                    }
                    #region 不在此判断是否存在发票号，造成锁表
                    //if (invoiceType == "0")
                    //{
                    //    string tmpCount = outpatientManager.QueryExistInvoiceCount(balance.Invoice.ID);
                    //    if (tmpCount == "1")
                    //    {
                    //        DialogResult result = MessageBox.Show("已经存在发票号为: " + balance.Invoice.ID +
                    //            " 的发票!,是否继续?", "提示!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    //        if (result == DialogResult.No)
                    //        {
                    //            errText = "因发票号重复暂时取消本次结算!";

                    //            return false;
                    //        }
                    //    }
                    //}
                    //else if (invoiceType == "1")
                    //{
                    //    string tmpCount = outpatientManager.QueryExistInvoiceCount(balance.PrintedInvoiceNO);
                    //    if (tmpCount == "1")
                    //    {
                    //        DialogResult result = MessageBox.Show("已经存在票据号为: " + balance.PrintedInvoiceNO +
                    //            " 的发票!,是否继续?", "提示!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    //        if (result == DialogResult.No)
                    //        {
                    //            errText = "因发票号重复暂时取消本次结算!";

                    //            return false;
                    //        }
                    //    }
                    //}
                    #endregion
                    //插入发票主表fin_opb_invoice
                    iReturn = outpatientManager.InsertBalance(balance);
                    if (iReturn == -1)
                    {
                        errText = "插入结算表出错!" + outpatientManager.Err;

                        return false;
                    }
                }
                #endregion

                #region 发票号走号，最后发票走下一个号码

                if (!isTempInvoice)//临时发票号码不走下一个号码
                {
                    string invoiceNo = ((Balance)invoices[invoices.Count - 1]).Invoice.ID;
                    string realInvoiceNo = ((Balance)invoices[invoices.Count - 1]).PrintedInvoiceNO;

                    if (invoiceNo.Length >= 12 && invoiceNo.StartsWith("9"))
                    {
                        // 为临时发票，记帐患者有可能是临时发票
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
                            {//发票头暂时先保存00
                                errText = this.invoiceServiceManager.Err;
                                return false;
                            }
                        }
                    }
                }

                #endregion

                #region 插入支付方式信息

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
                        errText = "插入支付方式表出错!" + outpatientManager.Err;

                        return false;
                    }

                    //{93E6443C-1FB5-45a7-B89D-F21A92200CF6}
                    //if (p.PayType.ID.ToString() == FS.HISFC.Models.Fee.EnumPayType.YS.ToString())
                    if (p.PayType.ID.ToString() == "YS")
                    {
                        //bool returnValue = this.AccountPay(r.PID.CardNO, p.FT.TotCost, p.Invoice.ID, p.InputOper.Dept.ID);



                        //if (!returnValue)
                        //{
                        //    errText = "扣取门诊账户失败!" + "\n" + this.Err;

                        //    return false;
                        //}
                        //{DA67A335-E85E-46e1-A672-4DB409BCC11B}
                        int returnValue = this.feeManager.AccountPay(r, p.FT.TotCost, p.Invoice.ID, (accountManager.Operator as FS.HISFC.Models.Base.Employee).Dept.ID, "C");
                        if (returnValue < 0)
                        {
                            errText = "扣取门诊账户失败!" + "\n" + this.Err;

                            return false;
                        }
                        if (returnValue == 0)
                        {
                            errText = "取消帐户支付!";
                            return false;
                        }
                    }
                }
                #endregion

                #region 插入挂号记录、更新看诊标记

                string noRegRules = controlParamIntegrate.GetControlParam(FS.HISFC.BizProcess.Integrate.Const.NO_REG_CARD_RULES, false, "9");

                //输入姓名患者收费,那么插入挂号信息,如果已经插入过,那么忽略.
                if (r.PID.CardNO.Substring(0, 1) == noRegRules || r.User01 == "补挂号")
                {
                    r.InputOper.OperTime = DateTime.MinValue;
                    r.InputOper.ID = operID;
                    r.IsFee = true;
                    r.TranType = TransTypes.Positive;
                    iReturn = registerManager.Insert(r);
                    if (iReturn == -1)
                    {
                        if (registerManager.DBErrCode != 1)//不是主键重复
                        {
                            errText = "插入挂号信息出错!" + registerManager.Err;

                            return false;
                        }
                    }
                }
                else
                {
                    if (registerManager.Update(FS.SOC.HISFC.PE.BizLogic.EnumUpdateStatus.PatientInfo, r) <= 0)
                    {
                        errText = "更新挂号信息失败!" + registerManager.Err;
                        return false;
                    }

                    if (registerManager.UpdateRegInfo(r) <= 0)
                    {
                        errText = "更新挂号信息失败!" + registerManager.Err;
                        return false;
                    }
                }

                pValue = controlParamIntegrate.GetControlParam("200018", false, "0");

                if (//r.PID.CardNO.Substring(0, 1) != noRegRules && 
                    r.ChkKind.Length == 0)
                {
                    //按照首诊医生更新患者的看诊医生 {7255EEBE-CF2B-4117-BB2C-196BE75B5965}
                    if (!r.IsSee || string.IsNullOrEmpty(r.SeeDoct.ID) || string.IsNullOrEmpty(r.SeeDoct.Dept.ID))
                    {
                        if (this.registerManager.UpdateSeeDone(r.ID) < 0)
                        {
                            errText = "更新看诊标志出错！" + registerManager.Err;
                            return false;
                        }
                        FeeItemList feeItemObj = feeDetails[0] as FeeItemList;
                        if (this.registerManager.UpdateDept(r.ID, feeItemObj.RecipeOper.Dept.ID, feeItemObj.RecipeOper.ID) < 0)
                        {
                            errText = "更新看诊科室、医生出错！" + registerManager.Err;
                            return false;
                        }
                        if (pValue == "1" && r.IsTriage)
                        {
                            if (this.managerIntegrate.UpdateAssign("", r.ID, feeTime, operID) < 0)
                            {
                                errText = "更新分诊标志出错！";
                                return false;
                            }
                        }
                    }
                }

                #endregion

                #endregion
            }
            else//划价
            {
                #region 划价流程

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
                        if (registerManager.DBErrCode != 1)//不是主键重复
                        {
                            errText = "插入挂号信息出错!" + registerManager.Err;

                            return false;
                        }
                    }
                }
                //处理划价保存信息.
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
        /// 门诊收费函数
        /// </summary>
        /// <param name="type">收费,划价标志</param>
        /// <param name="r">患者挂号基本信息</param>
        /// <param name="invoices">发票主表集合</param>
        /// <param name="invoiceDetails">发票明细表集合</param>
        /// <param name="feeDetails">费用明细集合</param>
        /// <param name="t">Transcation</param>
        /// <param name="payModes">支付方式集合</param>
        /// <param name="errText">错误信息</param>
        /// <returns></returns>
        public bool ClinicFeeSaveFee(FS.HISFC.Models.Base.ChargeTypes type, FS.HISFC.Models.Registration.Register r,
            ArrayList invoices, ArrayList invoiceDetails, ArrayList feeDetails, ArrayList invoiceFeeDetails, ArrayList payModes, ref string errText)
        {

            FS.HISFC.BizProcess.Integrate.Terminal.Confirm confirmIntegrate = new FS.HISFC.BizProcess.Integrate.Terminal.Confirm();
            FS.HISFC.BizProcess.Integrate.Terminal.Booking bookingIntegrate = new FS.HISFC.BizProcess.Integrate.Terminal.Booking();

            invoiceStytle = controlParamIntegrate.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.Const.GET_INVOICE_NO_TYPE, false, "0");

            isDoseOnceCanNull = controlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.Const.DOSE_ONCE_NULL, false, true);

            //获得收费时间
            DateTime feeTime = inpatientManager.GetDateTimeFromSysDateTime();

            //获得收费操作员
            string operID = inpatientManager.Operator.ID;

            FS.HISFC.Models.Base.Employee employee = inpatientManager.Operator as FS.HISFC.Models.Base.Employee;
            //返回值
            int iReturn = 0;
            //定义处方号
            string recipeNO = string.Empty;

            //如果是收费，获得发票信息
            if (type == FS.HISFC.Models.Base.ChargeTypes.Fee)//收费
            {
                #region 收费流程
                //发票已经在预览界面分配完毕,直接插入就可以了.

                #region//获得发票序列,多张发票发票号不同,共享一个发票序列,通过发票序列号,可以查询一次收费的多张发票.

                string invoiceCombNO = outpatientManager.GetInvoiceCombNO();
                if (invoiceCombNO == null || invoiceCombNO == string.Empty)
                {
                    errText = "获得发票流水号失败!" + outpatientManager.Err;

                    return false;
                }
                //获得特殊显示类别
                /////GetSpDisplayValue(myCtrl, t);
                //第一个发票号
                string mainInvoiceNO = string.Empty;
                string mainInvoiceCombNO = string.Empty;
                foreach (Balance balance in invoices)
                {
                    //主发票信息,不插入只做显示用
                    if (balance.Memo == "5")
                    {
                        mainInvoiceNO = balance.ID;

                        continue;
                    }

                    //自费患者不需要显示主发票,那么取第一个发票号作为主发票号
                    if (mainInvoiceNO == string.Empty)
                    {
                        mainInvoiceNO = balance.Invoice.ID;
                        mainInvoiceCombNO = balance.CombNO;
                    }
                }

                #endregion

                #region //插入发票明细表

                foreach (ArrayList tempsInvoices in invoiceDetails)
                {
                    foreach (ArrayList tempDetals in tempsInvoices)
                    {
                        foreach (BalanceList balanceList in tempDetals)
                        {
                            //总发票处理
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

                            //插入发票明细表 fin_opb_invoicedetail
                            iReturn = outpatientManager.InsertBalanceList(balanceList);
                            if (iReturn == -1)
                            {
                                errText = "插入发票明细出错!" + outpatientManager.Err;

                                return false;
                            }
                        }
                    }
                }

                #endregion

                #region//药品信息列表,生成处方号

                ArrayList drugLists = new ArrayList();
                //重新生成处方号,如果已有处方号,明细不重新赋值.
                if (!this.SetRecipeNOOutpatient(r, feeDetails, ref errText))
                {
                    return false;
                }

                #endregion

                #region//插入费用明细

                foreach (FeeItemList f in feeDetails)
                {
                    //验证数据
                    if (!this.IsFeeItemListDataValid(f, ref errText))
                    {
                        return false;
                    }

                    //如果没有处方号,重新赋值
                    if (f.RecipeNO == null || f.RecipeNO == string.Empty)
                    {
                        if (recipeNO == string.Empty)
                        {
                            recipeNO = outpatientManager.GetRecipeNO();
                            if (recipeNO == null || recipeNO == string.Empty)
                            {
                                errText = "获得处方号出错!";

                                return false;
                            }
                        }
                    }

                    #region 2007-8-29 liuq 判断是否已有发票号序号，没有则赋值
                    if (string.IsNullOrEmpty(f.InvoiceCombNO))
                    {
                        f.InvoiceCombNO = invoiceCombNO;
                    }
                    #endregion
                    //
                    #region 2007-8-28 liuq 判断是否已有发票号，没有初始化为12个0
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
                            errText = "请选择医生";
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

                    //如果患者为团体体检，那么所有项目都插入终端审核。
                    if (r.ChkKind == "2")
                    {
                        if (!f.IsConfirmed)
                        {
                            //如果项目流水号为空，说明没有经过划价流程，那么插入终端审核信息。
                            if (f.Order.ID == null || f.Order.ID == string.Empty)
                            {
                                f.Order.ID = orderManager.GetNewOrderID();
                                if (f.Order.ID == null || f.Order.ID == string.Empty)
                                {
                                    errText = "获得医嘱流水号出错!";
                                    return false;
                                }

                                FS.HISFC.BizProcess.Integrate.Terminal.Result result = confirmIntegrate.ServiceInsertTerminalApply(f, r);

                                if (result != FS.HISFC.BizProcess.Integrate.Terminal.Result.Success)
                                {
                                    errText = "处理终端申请确认表失败!";

                                    return false;
                                }
                            }
                        }
                    }
                    else//其他患者如果项目为需要终端审核项目则插入终端审核信息。
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
                                    errText = "获得医嘱流水号出错!";

                                    return false;
                                }

                                FS.HISFC.BizProcess.Integrate.Terminal.Result result = confirmIntegrate.ServiceInsertTerminalApply(f, r);

                                if (result != FS.HISFC.BizProcess.Integrate.Terminal.Result.Success)
                                {
                                    errText = "处理终端申请确认表失败!" + confirmIntegrate.Err;

                                    return false;
                                }
                            }
                        }
                    }
                    //没有付值医嘱流水号,赋值新的医嘱流水号
                    if (f.Order.ID == null || f.Order.ID == string.Empty)
                    {
                        f.Order.ID = orderManager.GetNewOrderID();
                        if (f.Order.ID == null || f.Order.ID == string.Empty)
                        {
                            errText = "获得医嘱流水号出错!";

                            return false;
                        }
                    }

                    if (r.ChkKind == "1")//个人体检更新收费标记
                    {
                        iReturn = examiIntegrate.UpdateItemListFeeFlagByMoOrder("1", f.Order.ID);
                        if (iReturn == -1)
                        {
                            errText = "更新体检收费标记失败!" + examiIntegrate.Err;

                            return false;
                        }
                    }

                    //如果删除划价保存中的组合项目主项目信息,保留明细.
                    if (f.UndrugComb.ID != null && f.UndrugComb.ID.Length > 0)
                    {
                        iReturn = outpatientManager.DeletePackageByMoOrder(f.Order.ID);
                        if (iReturn == -1)
                        {
                            errText = "删除组套失败!" + outpatientManager.Err;

                            return false;
                        }
                    }
                    FeeItemList feeTemp = new FeeItemList();
                    feeTemp = outpatientManager.GetFeeItemList(f.RecipeNO, f.SequenceNO);
                    if (feeTemp == null)//说明不存在
                    {
                        if (f.FTSource != "3" && (f.UndrugComb.ID == null || f.UndrugComb.ID == string.Empty))
                        {
                            errText = f.Item.Name + "可能已经被其他操作员删除,请刷新后再收费!";

                            return false;
                        }

                        iReturn = outpatientManager.InsertFeeItemList(f);
                        if (iReturn <= 0)
                        {
                            errText = "插入费用明细失败!" + outpatientManager.Err;

                            return false;
                        }
                    }
                    else
                    {
                        iReturn = outpatientManager.UpdateFeeItemList(f);
                        if (iReturn <= 0)
                        {
                            errText = "更新费用明细失败!" + outpatientManager.Err;

                            return false;
                        }
                    }

                    #region//回写医嘱信息

                    if (f.FTSource == "1")
                    {
                        iReturn = orderOutpatientManager.UpdateOrderChargedByOrderID(f.Order.ID, operID);
                        if (iReturn == -1)
                        {
                            errText = "更新医嘱信息出错!" + orderOutpatientManager.Err;

                            return false;
                        }
                    }

                    #endregion

                    //如果是药品,并且没有被确认过,而且不需要终端确认,那么加入发药申请列表.
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
                    //需要医技预约,插入终端预约信息.
                    if (f.Item.IsNeedBespeak && r.ChkKind != "2")
                    {
                        iReturn = bookingIntegrate.Insert(f);

                        if (iReturn == -1)
                        {
                            errText = "插入医技预约信息出错!" + f.Name + bookingIntegrate.Err;

                            return false;
                        }
                    }

                }

                #endregion

                #region 集体体检更新收费标记

                if (r.ChkKind == "2")//集体体检
                {
                    ArrayList recipeSeqList = this.GetRecipeSequenceForChk(feeDetails);
                    if (recipeSeqList != null && recipeSeqList.Count > 0)
                    {
                        foreach (string recipeSequenceTemp in recipeSeqList)
                        {
                            iReturn = examiIntegrate.UpdateItemListFeeFlagByRecipeSeq("1", recipeSequenceTemp);
                            if (iReturn == -1)
                            {
                                errText = "更新体检收费标记失败!" + examiIntegrate.Err;

                                return false;
                            }

                        }
                    }
                }

                #endregion

                #region//发药窗口信息

                string drugSendInfo = null;
                //插入发药申请信息,返回发药窗口,显示在发票上
                iReturn = pharmarcyManager.ApplyOut(r, drugLists, string.Empty, feeTime, false, out drugSendInfo);
                if (iReturn == -1)
                {
                    errText = "处理药品明细失败!" + pharmarcyManager.Err;

                    return false;
                }
                //如果有药品,那么设置发票的显示发药窗口信息.
                if (drugLists.Count > 0)
                {
                    foreach (Balance invoice in invoices)
                    {
                        invoice.DrugWindowsNO = drugSendInfo;
                    }
                }

                #region//插入发票主表

                foreach (Balance balance in invoices)
                {
                    //主发票信息,不插入只做显示用
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
                    //体检标志
                    string tempExamineFlag = null;
                    //获得体检标志 0 普通患者 1 个人体检 2 团体体检
                    //如果没有赋值,默认为普通患者
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

                    //=====去掉CanceledInvoiceNO=string.Empty 路志鹏================
                    //balance.CanceledInvoiceNO = string.Empty;
                    //==============================================================

                    balance.IsAuditing = false;
                    balance.IsDayBalanced = false;
                    balance.ID = balance.ID.PadLeft(12, '0');
                    balance.Patient.Pact.Memo = r.User03;//限额代码
                    //自费患者不需要显示主发票,那么取第一个发票号作为主发票号
                    if (mainInvoiceNO == string.Empty)
                    {
                        mainInvoiceNO = balance.Invoice.ID;
                    }
                    if (invoiceStytle == "0")
                    {
                        string tmpCount = outpatientManager.QueryExistInvoiceCount(balance.Invoice.ID);
                        if (tmpCount == "1")
                        {
                            DialogResult result = MessageBox.Show("已经存在发票号为: " + balance.Invoice.ID +
                                " 的发票!,是否继续?", "提示!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                            if (result == DialogResult.No)
                            {
                                errText = "因发票号重复暂时取消本次结算!";

                                return false;
                            }
                        }
                    }
                    else if (invoiceStytle == "1")
                    {
                        string tmpCount = outpatientManager.QueryExistInvoiceCount(balance.PrintedInvoiceNO);
                        if (tmpCount == "1")
                        {
                            DialogResult result = MessageBox.Show("已经存在票据号为: " + balance.PrintedInvoiceNO +
                                " 的发票!,是否继续?", "提示!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                            if (result == DialogResult.No)
                            {
                                errText = "因发票号重复暂时取消本次结算!";

                                return false;
                            }
                        }
                    }
                    //插入发票主表fin_opb_invoice
                    iReturn = outpatientManager.InsertBalance(balance);
                    if (iReturn == -1)
                    {
                        errText = "插入结算表出错!" + outpatientManager.Err;

                        return false;
                    }
                }



                #region 发票号走号，最后发票走下一个号码

                if (!isTempInvoice)//临时发票号码不走下一个号码
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
                        {//发票头暂时先保存00
                            errText = this.invoiceServiceManager.Err;
                            return false;
                        }
                    }
                }

                #endregion


                #endregion

                #endregion

                #region 插入支付方式信息

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
                //        errText = "插入支付方式表出错!" + outpatientManager.Err;

                //        return false;
                //    }

                //    if (p.PayType.ID.ToString() == FS.HISFC.Models.Fee.EnumPayType.YS.ToString())
                //    {
                //        bool returnValue = this.AccountPay(r.PID.CardNO, p.FT.TotCost, p.Invoice.ID, p.InputOper.Dept.ID);
                //        if (!returnValue)
                //        {
                //            errText = "扣取门诊账户失败!" + "\n" + this.Err;

                //            return false;
                //        }
                //    }
                //}
                #endregion

                #region//如果不是直接收费患者和体检患者，更新看诊标志

                string noRegRules = controlParamIntegrate.GetControlParam(FS.HISFC.BizProcess.Integrate.Const.NO_REG_CARD_RULES, false, "9");

                //输入姓名患者收费,那么插入挂号信息,如果已经插入过,那么忽略.
                if (r.PID.CardNO.Substring(0, 1) == noRegRules)
                {
                    r.InputOper.OperTime = feeTime;
                    r.InputOper.ID = operID;
                    r.IsFee = true;
                    r.TranType = TransTypes.Positive;
                    iReturn = registerManager.Insert(r);
                    if (iReturn == -1)
                    {
                        if (registerManager.DBErrCode != 1)//不是主键重复
                        {
                            errText = "插入挂号信息出错!" + registerManager.Err;

                            return false;
                        }
                    }
                }

                pValue = controlParamIntegrate.GetControlParam("200018", false, "0");

                if (//r.PID.CardNO.Substring(0, 1) != noRegRules && 
                    r.ChkKind.Length == 0)
                {
                    //按照首诊医生更新患者的看诊医生 {7255EEBE-CF2B-4117-BB2C-196BE75B5965}
                    if (!r.IsSee || string.IsNullOrEmpty(r.SeeDoct.ID) || string.IsNullOrEmpty(r.SeeDoct.Dept.ID))
                    {
                        if (this.registerManager.UpdateSeeDone(r.ID) < 0)
                        {
                            errText = "更新看诊标志出错！" + registerManager.Err;
                            return false;
                        }
                        FeeItemList feeItemObj = feeDetails[0] as FeeItemList;
                        if (this.registerManager.UpdateDept(r.ID, feeItemObj.RecipeOper.Dept.ID, feeItemObj.RecipeOper.ID) < 0)
                        {
                            errText = "更新看诊科室、医生出错！" + registerManager.Err;
                            return false;
                        }
                        if (pValue == "1" && r.IsTriage)
                        {
                            if (this.managerIntegrate.UpdateAssign("", r.ID, feeTime, operID) < 0)
                            {
                                errText = "更新分诊标志出错！";
                                return false;
                            }
                        }
                    }
                }
                ////如果是医保患者,更新本地医保结算信息表 fin_ipr_siinmaininfo
                //if (r.Pact.PayKind.ID == "02")
                //{
                //    //设置已结算标志
                //    r.SIMainInfo.IsBalanced = true;
                //    // iReturn = interfaceManager.update(r);
                //    if (iReturn < 0)
                //    {
                //        errText = "更新医保患者结算信息出错!" + interfaceManager.Err;
                //        return false;
                //    }
                //}

                #endregion



                #endregion
            }
            else//划价
            {
                #region 划价流程

                #region 防止出错，在该地方赋值 划价保存费用来源
                foreach (FeeItemList f in feeDetails)
                {
                    f.FTSource = "0";//划价保存费用来源
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
                        if (registerManager.DBErrCode != 1)//不是主键重复
                        {
                            errText = "插入挂号信息出错!" + registerManager.Err;

                            return false;
                        }
                    }
                }
                //处理划价保存信息.
                bool returnValue = this.feeManager.SetChargeInfo(r, feeDetails, feeTime, ref errText);
                if (!returnValue)
                {
                    return false;
                }

                #endregion
            }

            //处理适应症{E4C0E5CF-D93F-48f9-A53C-9ADCCED97A7E}
            FS.HISFC.BizProcess.Interface.FeeInterface.IAdptIllnessOutPatient iAdptIllnessOutPatient = null;
            iAdptIllnessOutPatient = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.FeeInterface.IAdptIllnessOutPatient)) as FS.HISFC.BizProcess.Interface.FeeInterface.IAdptIllnessOutPatient;
            if (iAdptIllnessOutPatient != null)
            {
                //保存适应症信息
                int returnValue = iAdptIllnessOutPatient.SaveOutPatientFeeDetail(r, ref feeDetails);
                if (returnValue < 0)
                {
                    return false;
                }

            }

            return true;
        }
        /// <summary>
        /// 拆分协定处方
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
                            errText = f.Item.Name + "是协定处方,但是没有维护明细或者明细已经停用！请与信息科联系！";
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
        /// 拆分协定处方
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        private ArrayList SplitNostrumDetail(FS.HISFC.Models.Registration.Register rInfo, FeeItemList f, ref string errText)
        {
            List<FS.HISFC.Models.Pharmacy.Nostrum> listDetail = this.pharmarcyManager.QueryNostrumDetail(f.Item.ID);
            ArrayList alTemp = new ArrayList();
            if (listDetail == null)
            {
                errText = "获得协定处方明细出错!" + pharmarcyManager.Err;

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
                    errText = "获得医嘱流水号出错!";

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
                    errText = "查找协定处方明细出错!";

                    continue;
                }

                feeDetail = new FeeItemList();
                feeDetail.Item = item;
                feeCode = item.MinFee.ID;
                try
                {
                    DateTime nowTime = this.outpatientManager.GetDateTimeFromSysDateTime();
                    int age = (int)((new TimeSpan(nowTime.Ticks - rInfo.Birthday.Ticks)).TotalDays / 365);
                    //{B9303CFE-755D-4585-B5EE-8C1901F79450}增加获取购入价
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
                //收取的协定处方以最小单位收取,明细数量 = 界面上输入的协定处方数量 * 对应明细项目数量 / 协定处方包装数
                if (f.FeePack == "0")//最小单位
                {
                    count = NConvert.ToDecimal(f.Item.Qty) * nosItem.Qty / f.Item.PackQty;
                }
                else //收取的协定处方以包装单位收取,明细数量 = 界面上输入的协定处方数量 * 对应明细项目数量
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

                //自费如此，如果加上公费需要重新计算!!!
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
                //            if (pactRate.Rate.PayRate == 1)//自费
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
                if (f.FT.RebateCost > 0)//有减免
                {
                    if (rInfo.Pact.PayKind.ID != "01")
                    {
                        errText = "暂时不允许非自费患者减免!";

                        return null;
                    }
                    //减免单独算
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
                if (f.SpecialPrice > 0)//有特殊自费
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
        /// 获得体检所有收费序列
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
    /// 医嘱执行档排序
    /// </summary>
    public class ExecOrderCompare : IComparer
    {
        #region IComparer 成员

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
