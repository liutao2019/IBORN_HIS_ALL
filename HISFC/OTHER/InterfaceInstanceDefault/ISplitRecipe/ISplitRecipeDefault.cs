using System;
using System.Collections;
using FS.HISFC.Models.Fee.Outpatient;
using System.Data;
using FS.HISFC.Models.Registration;

namespace InterfaceInstanceDefault.ISplitRecipe
{
    /// <summary>
    /// 默认分处方接口
    /// </summary>
    public class ISplitRecipeDefault : FS.HISFC.BizProcess.Interface.FeeInterface.ISplitRecipe
    {

        #region 变量

        /// <summary>
        /// 费用综合业务层
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();
        /// <summary>
        /// 门诊费用业务类
        /// </summary>
        FS.HISFC.BizLogic.Fee.Outpatient myOutPatient = new FS.HISFC.BizLogic.Fee.Outpatient();
        /// <summary>
        /// 控制参数业务层
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
        /// <summary>
        /// 管理类
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();
        /// <summary>
        /// 转换科室帮助类
        /// </summary>
        private static FS.FrameWork.Public.ObjectHelper hsInvertDept = null;
        /// <summary>
        /// 药品性质转换类
        /// </summary>
        private static FS.FrameWork.Public.ObjectHelper hsDrugQuaulity = null;

        /// <summary>
        /// 是否优先处理组合号
        /// </summary>
        int isDealCombNo = -1;

        /// <summary>
        /// 获得单张处方最多的项目数
        /// </summary>
        int noteCounts = 0;

        /// <summary>
        /// 是否忽略系统类别
        /// </summary>
        int isDecSysClassWhenGetRecipeNO = -1;

        /// <summary>
        /// 是否所有草药一个处方号，即不管是否组合，都是一个组合号
        /// </summary>
        int isAllPCCOneRecipe = -1;

        #endregion
        
        #region ISplitRecipe 成员
        /// <summary>
        /// 分处方函数
        /// </summary>
        /// <param name="r"></param>
        /// <param name="feeItemList"></param>
        /// <param name="errText"></param>
        /// <returns></returns>
        public bool SplitRecipe(Register r, System.Collections.ArrayList feeItemList, ref string errText)
        {
            return this.SetNoteNo(feeItemList, ref errText,r);
        }
        #endregion

        /// <summary>
        /// 分处方
        /// </summary>
        /// <param name="feeDetails"></param>
        /// <param name="errText"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public bool SetNoteNo(ArrayList feeDetails, ref string errText,Register r)
        {
            //存储已有的处方号和最大的处方流水号
            Hashtable hsRecipeNo = new Hashtable();
            //精二为一张处方、麻醉和精一为一张处方

            int feeCount = feeDetails.Count;

            #region 变量处理

            //是否优先处理组合号
            if (isDealCombNo == -1)
            {
                isDealCombNo = controlParamIntegrate.GetControlParam<int>(FS.HISFC.BizProcess.Integrate.Const.DEALCOMBNO, false, 1);
            }

            //获得单张处方最多的项目数, 默认项目数 5
            if (noteCounts == 0)
            {
                noteCounts = controlParamIntegrate.GetControlParam<int>(FS.HISFC.BizProcess.Integrate.Const.NOTECOUNTS, false, 5);
            }

            //是否忽略系统类别
            if (isDecSysClassWhenGetRecipeNO == -1)
            {
                isDecSysClassWhenGetRecipeNO = controlParamIntegrate.GetControlParam<int>(FS.HISFC.BizProcess.Integrate.Const.DEC_SYS_WHENGETRECIPE, false, 0);
            }

            if (isAllPCCOneRecipe == -1)
            {
                isAllPCCOneRecipe = controlParamIntegrate.GetControlParam<int>("HNMZ40", false, 0);
            }

            if (hsInvertDept == null)
            {
                hsInvertDept = new FS.FrameWork.Public.ObjectHelper();
                //取药房替换科室
                ArrayList alInvertDept = managerIntegrate.GetConstantList("InvertDept");

                if (alInvertDept != null && alInvertDept.Count > 0)
                {
                    hsInvertDept.ArrayObject = alInvertDept;
                }
            }

            if (hsDrugQuaulity == null)
            {
                hsDrugQuaulity = new FS.FrameWork.Public.ObjectHelper();

                //取药品剂型
                ArrayList alDrugQuaulity = managerIntegrate.GetConstantList("DRUGQUALITY");
                if (alDrugQuaulity != null && alDrugQuaulity.Count > 0)
                {
                    hsDrugQuaulity.ArrayObject = alDrugQuaulity;
                }
            }

            #endregion

            #region 根据常数 暂时替换发药药房和药品性质

            foreach (FeeItemList feeItem in feeDetails)
            {
                if (!string.IsNullOrEmpty(feeItem.RecipeNO))
                {
                    if (!hsRecipeNo.Contains(feeItem.RecipeNO))
                    {
                        hsRecipeNo.Add(feeItem.RecipeNO, feeItem.SequenceNO);
                    }
                    else
                    {
                        int seq = (Int32)hsRecipeNo[feeItem.RecipeNO];

                        if (seq < feeItem.SequenceNO)
                        {
                            hsRecipeNo[feeItem.RecipeNO] = feeItem.SequenceNO;
                        }
                    }
                }

                if (hsInvertDept.GetObjectFromID(feeItem.ExecOper.Dept.ID) != null && feeItem.Item.ItemType ==  FS.HISFC.Models.Base.EnumItemType.Drug)
                {
                    feeItem.UndrugComb.MedicalRecord = feeItem.ExecOper.Dept.ID;
                    feeItem.ExecOper.Dept.ID = hsInvertDept.GetObjectFromID(feeItem.ExecOper.Dept.ID).Name;
                    feeItem.UndrugComb.Memo = feeItem.ExecOper.Dept.ID;
                }

                if (feeItem.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                {
                    FS.HISFC.Models.Base.Const quaulity = hsDrugQuaulity.GetObjectFromID((feeItem.Item as FS.HISFC.Models.Pharmacy.Item).Quality.ID) as FS.HISFC.Models.Base.Const;

                    if (quaulity != null && quaulity.ID.Length > 0)
                    {
                        if ((quaulity.Memo.Trim().IndexOf("毒") < 0
                                 && quaulity.Memo.Trim().IndexOf("麻") < 0
                                 && quaulity.Memo.Trim().IndexOf("精") < 0)
                                 || (!quaulity.UserCode.Trim().Contains("S")
                                 && !quaulity.UserCode.Trim().Contains("P")))
                        {
                            feeItem.UndrugComb.NationCode = (feeItem.Item as FS.HISFC.Models.Pharmacy.Item).Quality.ID;
                            (feeItem.Item as FS.HISFC.Models.Pharmacy.Item).Quality.ID = "O";
                        }
                    }
                }
            }
            #endregion

            #region 项目分组

            ArrayList sortList = new ArrayList();

            while (feeDetails.Count > 0)
            {
                ArrayList sameNotes = new ArrayList();
                FeeItemList compareItem = feeDetails[0] as FeeItemList;
                foreach (FeeItemList f in feeDetails)
                {
                    /* 现有规则： houwb 2011-5-22
                     * 1、药品和非药品暂不组合
                     * 2、药品和药品：执行科室相同、系统类别相同（参数控制）、付数（天数）相同、药品性质项目，分在一组
                     * 3、非药品和非药品：执行科室相同、付数（天数）相同、分在一组
                     * 4、非药品的组合号 忽略了
                     * 
                     * 5、注意之前 如果第一个项目是非药品，则判断错误
                     * */

                    if (isDecSysClassWhenGetRecipeNO == 1)
                    {
                        if (compareItem.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                        {
                            if (f.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                            {
                                if (f.ExecOper.Dept.ID == compareItem.ExecOper.Dept.ID
                                    && (compareItem.Item.SysClass.ID.ToString() == "PCC" ? f.Days == compareItem.Days : true)
                                    && (f.Item as FS.HISFC.Models.Pharmacy.Item).Quality.ID == (compareItem.Item as FS.HISFC.Models.Pharmacy.Item).Quality.ID)
                                {
                                    sameNotes.Add(f);
                                }
                            }
                        }
                        else
                        {
                            if (f.Item.ItemType != FS.HISFC.Models.Base.EnumItemType.Drug)
                            {
                                if (f.ExecOper.Dept.ID == compareItem.ExecOper.Dept.ID
                                    && f.Days == compareItem.Days)
                                {
                                    sameNotes.Add(f);
                                }
                            }
                        }
                    }
                    else
                    {
                        if (compareItem.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                        {
                            if (f.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                            {
                                if (f.Item.SysClass.ID.ToString() == compareItem.Item.SysClass.ID.ToString()
                                    && f.ExecOper.Dept.ID == compareItem.ExecOper.Dept.ID
                                    && (compareItem.Item.SysClass.ID.ToString() == "PCC" ? f.Days == compareItem.Days : true)
                                    && (f.Item as FS.HISFC.Models.Pharmacy.Item).Quality.ID == (compareItem.Item as FS.HISFC.Models.Pharmacy.Item).Quality.ID)
                                {
                                    sameNotes.Add(f);
                                }
                            }
                        }
                        else
                        {
                            if (f.Item.ItemType != FS.HISFC.Models.Base.EnumItemType.Drug)
                            {
                                if (f.ExecOper.Dept.ID == compareItem.ExecOper.Dept.ID
                                    && f.Days == compareItem.Days)
                                {
                                    sameNotes.Add(f);
                                }
                            }
                        }
                    }
                }
                sortList.Add(sameNotes);
                foreach (FeeItemList f in sameNotes)
                {
                    feeDetails.Remove(f);
                }
            }
            #endregion

            #region 还原药品性质

            foreach (ArrayList temp in sortList)
            {
                foreach (FeeItemList feeItem in temp)
                {
                    if (feeItem.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        if (!string.IsNullOrEmpty((feeItem.Item as FS.HISFC.Models.Pharmacy.Item).Quality.ID))
                        {
                            (feeItem.Item as FS.HISFC.Models.Pharmacy.Item).Quality.ID = (hsDrugQuaulity.GetObjectFromID((feeItem.Item as FS.HISFC.Models.Pharmacy.Item).Quality.ID) as FS.HISFC.Models.Base.Const).ID;
                        }
                        FS.HISFC.Models.Base.Const quaulity = hsDrugQuaulity.GetObjectFromID((feeItem.Item as FS.HISFC.Models.Pharmacy.Item).Quality.ID) as FS.HISFC.Models.Base.Const;


                        if (quaulity != null && quaulity.ID.Length > 0)
                        {
                            if ((quaulity.Memo.Trim().IndexOf("毒") < 0
                                     && quaulity.Memo.Trim().IndexOf("麻") < 0
                                     && quaulity.Memo.Trim().IndexOf("精") < 0)
                                     || (!quaulity.UserCode.Trim().Contains("S")
                                     && !quaulity.UserCode.Trim().Contains("P")))
                            {
                                feeItem.UndrugComb.NationCode = (feeItem.Item as FS.HISFC.Models.Pharmacy.Item).Quality.ID;
                                (feeItem.Item as FS.HISFC.Models.Pharmacy.Item).Quality.ID = "O";
                            }
                        }
                    }
                }
            }

            #endregion

            //分配处方号
            foreach (ArrayList temp in sortList)
            {
                ArrayList counts = new ArrayList();
                ArrayList countUnits = new ArrayList();

                ArrayList speTemp = new ArrayList();

                //挑选出不是草药和复合项目的所有费用
                foreach (FeeItemList f in temp)
                {
                    if (f.Item.SysClass.ID.ToString() == "PCC")
                    {

                    }
                    else if (f.UndrugComb != null && f.UndrugComb.ID != null && f.UndrugComb.ID != "")
                    {

                    }
                    else
                    {
                        speTemp.Add(f);
                    }
                }

                foreach (FeeItemList f in speTemp)
                {
                    temp.Remove(f);
                }

                //对草药和复合项目进行分类
                while (temp.Count > 0)
                {
                    countUnits = new ArrayList();

                    //用于区分多组草药
                    string combID = "";

                    foreach (FeeItemList f in temp)
                    {
                        if (f.Item.SysClass.ID.ToString() == "PCC")
                        {
                            if (isAllPCCOneRecipe == 1)
                            {
                                combID = f.Order.Combo.ID;
                            }
                            else
                            {
                                if (string.IsNullOrEmpty(combID))
                                {
                                    combID = f.Order.Combo.ID;
                                }
                            }
                            if (combID == f.Order.Combo.ID)
                            {
                                countUnits.Add(f);
                            }
                        }
                        else if (f.UndrugComb != null && f.UndrugComb.ID != null && f.UndrugComb.ID != "")
                        {
                            countUnits.Add(f);
                        }
                        else
                        {

                        }
                    }

                    counts.Add(countUnits);

                    foreach (FeeItemList f in countUnits)
                    {
                        temp.Remove(f);
                    }
                }

                ArrayList speCounts = new ArrayList();
                ArrayList speCountUnits = new ArrayList();
                ArrayList noRecipeUnits = new ArrayList();

                //特殊项目再按照处方号进行分类
                while (speTemp.Count > 0)
                {
                    speCountUnits = new ArrayList();
                    FeeItemList compareItem = speTemp[0] as FeeItemList;

                    foreach (FeeItemList f in speTemp)
                    {
                        if (f.RecipeNO == compareItem.RecipeNO)
                        {
                            speCountUnits.Add(f);

                            if (f.RecipeNO.Length <= 0)
                            {
                                noRecipeUnits.Add(f);
                            }
                        }
                    }

                    speCounts.Add(speCountUnits);

                    foreach (FeeItemList f in speCountUnits)
                    {
                        speTemp.Remove(f);
                    }
                }

                //合并同一组合号的处方项目
                foreach (ArrayList tempCounts in speCounts)
                {
                    if (tempCounts.Count <= 0)
                        continue;

                    if ((tempCounts[0] as FS.HISFC.Models.Fee.Outpatient.FeeItemList).RecipeNO.Length > 0)
                    {
                        ArrayList tempArray = new ArrayList();

                        while(tempCounts.Count > 0)
                        {
                            tempArray.Add(tempCounts[0]);

                            foreach (FeeItemList f in noRecipeUnits)
                            {
                                //if (f.Order.Combo.ID.Length > 0 && f.Order.Combo.ID == (tempCounts[0] as FS.HISFC.Models.Fee.Outpatient.FeeItemList).Order.Combo.ID && !tempCounts.Contains(f))
                                //同一组合可能有些有处方号有些没有，此处增加判断 houwb 2011-3-18 {43E5F80B-633B-4cc4-A23B-EA61EA775022}
                                if (f.Order.Combo.ID.Length > 0 && f.Order.Combo.ID == (tempCounts[0] as FS.HISFC.Models.Fee.Outpatient.FeeItemList).Order.Combo.ID && !tempCounts.Contains(f) && f.RecipeNO == (tempCounts[0] as FS.HISFC.Models.Fee.Outpatient.FeeItemList).RecipeNO)
                                {
                                    tempArray.Add(f);
                                }
                            }

                            foreach (FeeItemList f in tempArray)
                            {
                                if (noRecipeUnits.Contains(f))
                                {
                                    noRecipeUnits.Remove(f);
                                }
                            }

                            tempCounts.Remove(tempCounts[0]);
                        }


                        while (tempArray.Count > 0)
                        {
                            tempCounts.Add(tempArray[0]);

                            tempArray.Remove(tempArray[0]);
                        }
                    }

                }

                //对有处方号的数据进行补充
                foreach (ArrayList tempCounts in speCounts)
                {
                    if (tempCounts.Count <= 0)
                        continue;

                    if ((tempCounts[tempCounts.Count-1] as FS.HISFC.Models.Fee.Outpatient.FeeItemList).RecipeNO.Length > 0 )
                        //这里为什么这么处理？？ 容易导致收费成功后没有更新费用明细和医嘱状态 houwb 2011-4-14
                        //&& tempCounts.Count < noteCounts)
                    {
                        foreach (FeeItemList f in noRecipeUnits)
                        {
                            int sameCombCount = this.GetArrayHaveSameCombNo(f, noRecipeUnits, tempCounts);

                            if (tempCounts.Count + sameCombCount <= noteCounts)
                            {
                                if (f.Order.Combo.ID.Length <= 0)
                                {
                                    tempCounts.Add(f);
                                }
                                else
                                {
                                    foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList tmpItem in noRecipeUnits)
                                    {
                                        if (f.Order.Combo.ID.Length > 0 && f.Order.Combo.ID == tmpItem.Order.Combo.ID && !tempCounts.Contains(tmpItem))
                                        {
                                            tempCounts.Add(tmpItem);
                                        }
                                    }
                                }
                            }
                        }

                        foreach (FeeItemList f in tempCounts)
                        {
                            if (noRecipeUnits.Contains(f))
                            {
                                noRecipeUnits.Remove(f);
                            }
                        }

                        counts.Add(tempCounts);
                    }

                }

                //还剩余的没有处方号的项目进行分组
                while (noRecipeUnits.Count > 0)
                {
                    countUnits = new ArrayList();

                    foreach (FeeItemList f in noRecipeUnits)
                    {
                        if (f.Item.SysClass.ID.ToString() == "PCC")
                        {
                            countUnits.Add(f);
                        }
                        else if (f.UndrugComb != null && f.UndrugComb.ID != null && f.UndrugComb.ID != "")
                        {
                            countUnits.Add(f);
                        }
                        else
                        {
                            int sameCombCount = this.GetArrayHaveSameCombNo(f, noRecipeUnits, countUnits);

                            //组合内数量大于限制数量,单独列为一组
                            if (sameCombCount >= noteCounts)
                            {
                                countUnits.Clear();

                                foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList tmpItem in noRecipeUnits)
                                {
                                    if (f.Order.Combo.ID.Length > 0 && f.Order.Combo.ID == tmpItem.Order.Combo.ID && !countUnits.Contains(tmpItem))
                                    {
                                        countUnits.Add(tmpItem);
                                    }
                                }
                            }
                            else
                            {
                                if (countUnits.Count + sameCombCount <= noteCounts)
                                {
                                    if (f.Order.Combo.ID.Length <= 0)
                                    {
                                        countUnits.Add(f);
                                    }
                                    else
                                    {
                                        foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList tmpItem in noRecipeUnits)
                                        {
                                            if (f.Order.Combo.ID.Length > 0 && f.Order.Combo.ID == tmpItem.Order.Combo.ID && !countUnits.Contains(tmpItem))
                                            {
                                                countUnits.Add(tmpItem);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                    }

                    counts.Add(countUnits);

                    foreach (FeeItemList f in countUnits)
                    {
                        noRecipeUnits.Remove(f);
                    }
                }

                //按上面分好的数据进行处方号分配
                foreach (ArrayList tempCounts in counts)
                {
                    string recipeNo = null;//处方流水号
                    int noteSeq = 1;//处方内项目流水号
                    string newRecipeNo = null;
                    int newSeq = 1;//处方内项目流水号

                    string tempRecipeNo = "";
                    int tempSeq = 0;
                    GetRecipeNoAndMaxSeq(tempCounts, ref tempRecipeNo, ref tempSeq, r.ID);
                    tempSeq = tempSeq + 1;
                    if (tempRecipeNo != "" && tempSeq > 0)
                    {
                        foreach (FeeItemList f in tempCounts)
                        {
                            feeDetails.Add(f);
                            if (f.RecipeNO != null && f.RecipeNO != "")//已经分配处方号
                            {
                                continue;
                            }
                            else
                            {
                                #region 修改分处方bug zuowy

                                if (f.Item.SysClass.ID.ToString() == "PCC")
                                {
                                    f.RecipeNO = tempRecipeNo;
                                    //f.SequenceNO = tempSeq;
                                    //tempSeq++;
                                    f.SequenceNO = GetRecipeSeq(ref hsRecipeNo, f.RecipeNO);
                                }
                                else if (f.UndrugComb != null && f.UndrugComb.ID != null && f.UndrugComb.ID != "")
                                {
                                    f.RecipeNO = tempRecipeNo;
                                    //f.SequenceNO = tempSeq;
                                    //tempSeq++;
                                    f.SequenceNO = GetRecipeSeq(ref hsRecipeNo, f.RecipeNO);
                                }
                                else
                                {
                                    if (tempSeq > noteCounts + 5)
                                    {
                                        if (newRecipeNo == null)
                                        {
                                            newRecipeNo = this.feeIntegrate.GetRecipeNO();
                                            f.RecipeNO = newRecipeNo;
                                            //f.SequenceNO = newSeq;
                                            //newSeq++;
                                            f.SequenceNO = GetRecipeSeq(ref hsRecipeNo, f.RecipeNO);
                                        }
                                        else
                                        {
                                            f.RecipeNO = newRecipeNo;
                                            //f.SequenceNO = newSeq;
                                            //newSeq++;
                                            f.SequenceNO = GetRecipeSeq(ref hsRecipeNo, f.RecipeNO);
                                        }
                                    }
                                    else
                                    {
                                        f.RecipeNO = tempRecipeNo;
                                        //f.SequenceNO = tempSeq;
                                        //tempSeq++;
                                        f.SequenceNO = GetRecipeSeq(ref hsRecipeNo, f.RecipeNO);
                                    }
                                }
                                #endregion
                            }
                        }
                    }
                    else
                    {
                        recipeNo = feeIntegrate.GetRecipeNO();
                        if (recipeNo == null || recipeNo == "")
                        {
                            errText = "获得处方号出错!";
                            return false;
                        }
                        foreach (FeeItemList f in tempCounts)
                        {
                            feeDetails.Add(f);
                            if (f.RecipeNO != null && f.RecipeNO != "")//已经分配处方号
                            {
                                continue;
                            }
                            else
                            {
                                f.RecipeNO = recipeNo;
                                //f.SequenceNO = noteSeq;
                                //noteSeq++;
                                f.SequenceNO = GetRecipeSeq(ref hsRecipeNo, f.RecipeNO);
                            }
                        }
                    }
                }
            }

            #region 根据常数 还原发药药房
            foreach (FeeItemList feeItem in feeDetails)
            {
                if (!string.IsNullOrEmpty(feeItem.UndrugComb.ID)
                    && hsInvertDept.GetObjectFromID(feeItem.UndrugComb.ID) != null 
                    && feeItem.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                {
                    feeItem.ExecOper.Dept.ID = hsInvertDept.GetObjectFromID(feeItem.UndrugComb.MedicalRecord).ID;
                    feeItem.UndrugComb.MedicalRecord = "";
                }
            }
            #endregion

            if (feeCount != feeDetails.Count)
            {
                errText = "分方返回的费用列表数量不对，请联系电脑中心！";
                return false;
            }

            return true;
        }

        /// <summary>
        /// 获取处方流水号
        /// </summary>
        /// <param name="hsRecipeNo"></param>
        /// <param name="recipeNo"></param>
        /// <returns></returns>
        private int GetRecipeSeq(ref Hashtable hsRecipeNo, string recipeNo)
        {
            try
            {
                if (string.IsNullOrEmpty(recipeNo))
                {
                    return 1;
                }

                if (hsRecipeNo.Contains(recipeNo))
                {
                    hsRecipeNo[recipeNo] = (Int32)hsRecipeNo[recipeNo] + 1;
                    return (Int32)hsRecipeNo[recipeNo];
                }
                else
                {
                    hsRecipeNo.Add(recipeNo, 1);
                    return 1;
                }
            }
            catch
            {
                return 1;
            }
        }

        /// <summary>
        /// 根据处方号获取最大流水号
        /// </summary>
        /// <param name="alFeeItem"></param>
        /// <param name="recipeNo"></param>
        /// <param name="seq"></param>
        /// <param name="clinicCode"></param>
        public void GetRecipeNoAndMaxSeq(ArrayList alFeeItem, ref string recipeNo, ref int seq, string clinicCode)
        {
            if (alFeeItem == null || alFeeItem.Count <= 0)
            {
                return;
            }

            int recipeSeq = 0;
            int seqTemp = 0;
            foreach (FeeItemList feeItem in alFeeItem)
            {
                if (feeItem.RecipeNO != null && feeItem.RecipeNO.Length > 0)
                {
                    recipeNo = feeItem.RecipeNO;
                    seqTemp = FS.FrameWork.Function.NConvert.ToInt32(myOutPatient.GetMaxSeqByRecipeNO(recipeNo, clinicCode));
                    if (recipeSeq < seqTemp)
                    {
                        recipeSeq = seqTemp;
                    }
                    if (recipeSeq < feeItem.SequenceNO)
                    {
                        recipeSeq = feeItem.SequenceNO;
                    }

                    //break;
                }
            }
            seq = recipeSeq;
        }

        /// <summary>
        /// 获取具有相同组合号的项目列表数量
        /// </summary>
        /// <param name="feeItem"></param>
        /// <param name="alFeeDetails"></param>
        /// <returns></returns>
        private int GetArrayHaveSameCombNo(FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItem, ArrayList alFeeDetails,ArrayList countUnits)
        {
            int combCount = 0;

            if (countUnits.Contains(feeItem))
            {
                return combCount;
            }

            foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList temp in alFeeDetails)
            {
                if (feeItem.Order.Combo.ID.Length > 0 && feeItem.Order.Combo.ID == temp.Order.Combo.ID)
                {
                    bool isSubPha = false;//是否大输液

                    if (temp.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        FS.HISFC.Models.Base.Const quaulity = hsDrugQuaulity.GetObjectFromID((temp.Item as FS.HISFC.Models.Pharmacy.Item).Quality.ID) as FS.HISFC.Models.Base.Const;

                        if (quaulity != null && quaulity.ID.Length > 0)
                        {
                            if (quaulity.Memo.Trim().IndexOf("大输液") >= 0
                                //或者系统类别属于大输液
                                || quaulity.UserCode.Trim() == "T"
                                )
                            {
                                isSubPha = true;
                            }
                        }
                    }

                    if (!isSubPha)
                        combCount++;
                }
            }

            return combCount;
        }

    }
}
