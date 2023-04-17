using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.HISFC.Models.Registration;
using System.Collections;
using FS.HISFC.Models.Fee.Outpatient;

namespace FS.SOC.Local.Order.OutPatientOrder.SplitRecipe
{
    /// <summary>
    /// 默认分处方接口
    /// </summary>
    public class ISplitRecipe : FS.HISFC.BizProcess.Interface.FeeInterface.ISplitRecipe
    {
        #region 变量

        /// <summary>
        /// 费用综合业务层
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();

        /// <summary>
        /// 控制参数业务层
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        /// <summary>
        /// 转换科室帮助类，类似大输液库分方，按照西药房分方
        /// ID为原科室大输液，Name为替换为的科室西药房
        /// </summary>
        private static FS.FrameWork.Public.ObjectHelper hsInvertDept = null;

        /// <summary>
        /// 药品性质转换类
        /// </summary>
        private static FS.FrameWork.Public.ObjectHelper hsDrugQuaulity = null;

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

        /// <summary>
        /// 西药分方规则（1、5个药一张方，不含大输液；2、5个药一张方，含大输液；3、5个组合一张方）
        /// </summary>
        private int splitRule = -1;

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
            return this.SetNoteNo(feeItemList, ref errText, r);
        }
        #endregion

        /// <summary>
        /// 分处方
        /// </summary>
        /// <param name="feeDetails"></param>
        /// <param name="errText"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public bool SetNoteNo(ArrayList alFeeItem, ref string errText, Register r)
        {
            bool isCharge = false;

            if (r.User02 == "收费")
            {
                isCharge = true;
            }

            int feeCount = alFeeItem.Count;

            /*
             * 二、处方颜色与分类标注
             * （一）普通处方的印刷用纸为白色。
             * （二）急诊处方印刷用纸为淡黄色，右上角标注“急诊”。
             * （三）儿科处方印刷用纸为淡绿色，右上角标注 “儿科”。
             * （四）麻醉药品和第一类精神药品处方印刷用纸为淡红色，右上角标注“麻、精一”。
             * （五）第二类精神药品处方印刷用纸为白色，右上角标注“精二”。
            */


            ArrayList feeDetails = new ArrayList();

            foreach (FeeItemList feeItem in alFeeItem)
            {
                //如果是收费界面，只取没有处方号的，以免重新分方导致不能收费！！
                if (isCharge)
                {
                    if (!string.IsNullOrEmpty(feeItem.RecipeNO))
                    {
                        continue;
                    }
                }
                if (feeItem.Item.IsMaterial)
                {
                    continue;
                }

                FS.HISFC.Models.Fee.Outpatient.FeeItemList itemListTemp = feeItem.Clone();

                if (feeItem.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                {
                    itemListTemp.Item = ((FS.HISFC.Models.Pharmacy.Item)feeItem.Item).Clone();
                }
                else
                {
                    try
                    {
                        itemListTemp.Item = ((FS.HISFC.Models.Fee.Item.Undrug)feeItem.Item).Clone();
                    }
                    catch
                    {
                        itemListTemp.Item = feeItem.Item.Clone();
                    }
                }
                feeDetails.Add(itemListTemp);
            }

            #region 变量处理

            //是否优先处理组合号
            //if (isDealCombNo == -1)
            //{
            //    isDealCombNo = controlParamIntegrate.GetControlParam<int>(FS.HISFC.BizProcess.Integrate.Const.DEALCOMBNO, false, 1);
            //}

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

            if (hsDrugQuaulity == null)
            {
                hsDrugQuaulity = new FS.FrameWork.Public.ObjectHelper();

                FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

                //取药品剂型
                ArrayList alDrugQuaulity = managerIntegrate.GetConstantList("DRUGQUALITY");
                if (alDrugQuaulity != null && alDrugQuaulity.Count > 0)
                {
                    hsDrugQuaulity.ArrayObject = alDrugQuaulity;
                }
            }

            if (isAllPCCOneRecipe == -1)
            {
                isAllPCCOneRecipe = controlParamIntegrate.GetControlParam<int>("HNMZ40", false, 0);
            }

            if (splitRule == -1)
            {
                splitRule = controlParamIntegrate.GetControlParam<int>("HNMZ54", false, 1);
            }

            if (hsInvertDept == null)
            {
                hsInvertDept = new FS.FrameWork.Public.ObjectHelper();

                FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();
                //取药房替换科室
                ArrayList alInvertDept = managerIntegrate.GetConstantList("InvertDept");

                if (alInvertDept != null && alInvertDept.Count > 0)
                {
                    hsInvertDept.ArrayObject = alInvertDept;
                }
            }

            #endregion

            #region 排序:按照组号、排序号、辅材标记排序

            //SortByDortID feeSort = new SortByDortID();

            //feeDetails.Sort(feeSort);

            #endregion

            #region 获取已经有的处方号,目的是保证修改时，大部分处方不变

            ArrayList alRecipeNO = new ArrayList();

            foreach (FeeItemList feeItem in feeDetails)
            {
                if (!string.IsNullOrEmpty(feeItem.RecipeNO)
                    && !alRecipeNO.Contains(feeItem.RecipeNO))
                {
                    alRecipeNO.Add(feeItem.RecipeNO);
                }

                feeItem.RecipeNO = "";

                //替换成药系统类别为西药
                if (isDecSysClassWhenGetRecipeNO == 1 && feeItem.Item.SysClass.ID.ToString() == "PCZ")
                {
                    feeItem.Item.SysClass.ID = "P";
                }

                //统一配置药品性质
                if (feeItem.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                {
                    if (GetItemQaulity(feeItem) == 3)
                    {
                        (feeItem.Item as FS.HISFC.Models.Pharmacy.Item).Quality.Name = "精二";
                    }
                    else if (GetItemQaulity(feeItem) == 2)
                    {
                        (feeItem.Item as FS.HISFC.Models.Pharmacy.Item).Quality.Name = "毒麻精一";
                    }
                    else if (GetItemQaulity(feeItem) == 1)
                    {
                        (feeItem.Item as FS.HISFC.Models.Pharmacy.Item).Quality.Name = "普通";
                    }
                    else if (GetItemQaulity(feeItem) == 4)
                    {
                        (feeItem.Item as FS.HISFC.Models.Pharmacy.Item).Quality.Name = "材料";
                    }
                    //{564D4750-E736-432b-A5B6-B0B178AF1A11}
                    else if (GetItemQaulity(feeItem) == 5)
                    {
                        (feeItem.Item as FS.HISFC.Models.Pharmacy.Item).Quality.Name = "毒性药品";
                    }
                }
            }

            #endregion

            #region 按照组合分组

            System.Collections.Generic.Dictionary<string, ArrayList> dicCombItem = new System.Collections.Generic.Dictionary<string, ArrayList>();
            foreach (FeeItemList feeItem in feeDetails)
            {
                if (feeItem.Item.IsMaterial)
                {
                    continue;
                }
                if (!dicCombItem.ContainsKey(feeItem.Order.Combo.ID))
                {
                    ArrayList al = new ArrayList();
                    al.Add(feeItem);
                    dicCombItem.Add(feeItem.Order.Combo.ID, al);
                }
                else
                {
                    ArrayList al = dicCombItem[feeItem.Order.Combo.ID] as ArrayList;
                    al.Add(feeItem);
                    dicCombItem[feeItem.Order.Combo.ID] = al;
                }
            }

            #endregion

            #region 分组

            //分组原则
            //1、药品按照系统类别、取药药房、药品性质、付数分组
            //2、非药品按照最小费用、执行科室分组
            //3、辅材跟药品同组
            //4、组合号相同的也分在一组

            System.Collections.Generic.Dictionary<string, ArrayList> dicReciptComb = new System.Collections.Generic.Dictionary<string, ArrayList>();

            string strComb = "";//组合信息

            foreach (string keys in dicCombItem.Keys)
            {
                ArrayList alComb = dicCombItem[keys] as ArrayList;

                //FeeItemList feeItem = alComb[0] as FeeItemList;
                FeeItemList feeItem = GetSplitItem(alComb);

                if (feeItem.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                {
                    //1、药品按照系统类别、取药药房、药品性质分组
                    strComb = feeItem.Item.SysClass.ID.ToString() + feeItem.ExecOper.Dept.ID
                        + (feeItem.Item as FS.HISFC.Models.Pharmacy.Item).Quality.Name + "付" + feeItem.Order.HerbalQty.ToString();

                    //2018-9-21 23:29:21 草药处方增加按组合号分处方
                    if (feeItem.Item.SysClass.ID.ToString() == "PCC")
                    {
                        strComb += feeItem.Order.Combo.ID;
                    }

                    //重点品种全麻和精二增加1分处方
                    bool isNewItem = false;

                    FS.HISFC.Models.Pharmacy.Item itemNew = new FS.HISFC.Models.Pharmacy.Item();
                    FS.HISFC.BizLogic.Pharmacy.Item item = new FS.HISFC.BizLogic.Pharmacy.Item();
                    itemNew = item.GetItem(feeItem.Item.ID);
                    if (itemNew != null)
                    {

                        if (itemNew.SpecialFlag4 == null)
                            itemNew.SpecialFlag4 = "-";
                        if (itemNew.Quality == null)
                            itemNew.Quality.ID = "-";

                        if ((itemNew.Quality.ID == "P2" || itemNew.Quality.ID == "Q") && itemNew.SpecialFlag4 == "13")
                        {
                            isNewItem = true;
                        }
                    }

                    if (isNewItem) 
                    {
                        strComb += "1";
                    }
                }
                else
                {
                    //为了方便药品计算数量等，辅材暂时不分方，到最后根据药品组合号直接赋值
                    if (feeItem.Item.IsMaterial)
                    {
                        continue;
                    }
                    else
                    {
                        //2、非药品按照最小费用、执行科室分组
                        //strComb = feeItem.Item.MinFee.ID + feeItem.ExecOper.Dept.ID;

                        //考虑到非药品一般会按照系统类别+执行科室分单，所以处方号也按照系统类别和执行科室区分
                        //可用于申请单打印、外部接口中的申请单号等
                        strComb = feeItem.Item.SysClass.ID.ToString() + feeItem.ExecOper.Dept.ID;
                    }
                }

                if (!dicReciptComb.ContainsKey(strComb))
                {
                    ArrayList al = new ArrayList();
                    al.Add(alComb);
                    dicReciptComb.Add(strComb, al);
                }
                else
                {
                    ArrayList al = dicReciptComb[strComb] as ArrayList;
                    al.Add(alComb);
                    dicReciptComb[strComb] = al;
                }
            }


            #region 分配处方号

            int i = 0;

            string recipeNo = "";
            foreach (string key in dicReciptComb.Keys)
            {
                ArrayList al = dicReciptComb[key] as ArrayList;

                try
                {
                    recipeNo = (String)alRecipeNO[i];
                    i++;
                    if (string.IsNullOrEmpty(recipeNo))
                    {
                        recipeNo = feeIntegrate.GetRecipeNO();
                    }
                }
                catch
                {
                    recipeNo = feeIntegrate.GetRecipeNO();
                }
                int phaCount = 0;
                foreach (ArrayList alItems in al)
                {
                    if (GetSubPhaCount(alItems) > noteCounts)
                    {
                        //errText = "开立的组合内包含超过" + noteCounts.ToString() + "个非大输液的药品！";
                        //return false;
                        if (!isCharge)
                        {
                            string warning = "";
                            if (splitRule == 2)
                            {
                                warning = noteCounts.ToString() + "个药品（含大输液）！";
                            }
                            else if (splitRule == 3)
                            {
                                warning = noteCounts.ToString() + "个组合的药品！";
                            }
                            else
                            {
                                warning = noteCounts.ToString() + "个药品（不含大输液）！";
                            }

                            //FS.FrameWork.WinForms.Classes.Function.ShowBalloonTip(3, "开立处方不规范！\r\n\r\n开立的组合内包含超过" + warning + "\r\n可能导致分处方错误！", "警告", System.Windows.Forms.ToolTipIcon.Info);
                        }
                    }

                    if (((FeeItemList)alItems[0]).Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        phaCount += GetSubPhaCount(alItems);
                    }
                    //超出5个处方数的时候，另起一页
                    if (phaCount > noteCounts)
                    {
                        if (((FeeItemList)alItems[0]).Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                        {
                            phaCount = GetSubPhaCount(alItems);
                        }
                        try
                        {
                            recipeNo = (String)alRecipeNO[i];
                            i++;

                            if (string.IsNullOrEmpty(recipeNo))
                            {
                                recipeNo = feeIntegrate.GetRecipeNO();
                            }
                        }
                        catch
                        {
                            recipeNo = feeIntegrate.GetRecipeNO();
                        }
                    }

                    foreach (FeeItemList feeItem in alItems)
                    {
                        feeItem.RecipeNO = recipeNo;
                    }
                }
            }

            #endregion

            #region 分配实际处方号（包括辅材）

            //记录该处方号对应的最大处方内流水号
            System.Collections.Generic.Dictionary<string, int> dicRecipeSeq = new System.Collections.Generic.Dictionary<string, int>();

            Hashtable hsNewRecipeNo = new Hashtable();
            foreach (FeeItemList feeItem in feeDetails)
            {
                if (!hsNewRecipeNo.ContainsKey(feeItem.Order.Combo.ID))
                {
                    hsNewRecipeNo.Add(feeItem.Order.Combo.ID, feeItem.RecipeNO);
                }
            }

            //alFeeItem.Sort(feeSort);

            foreach (FeeItemList feeItem in alFeeItem)
            {
                //如果是收费界面，只取没有处方号的，以免重新分方导致不能收费！！
                if (isCharge)
                {
                    if (!string.IsNullOrEmpty(feeItem.RecipeNO))
                    {
                        continue;
                    }
                }
                feeItem.RecipeNO = (string)hsNewRecipeNo[feeItem.Order.Combo.ID];

                if (string.IsNullOrEmpty(feeItem.RecipeNO))
                {
                    feeItem.RecipeNO = this.feeIntegrate.GetRecipeNO();
                }

                if (dicRecipeSeq.ContainsKey(feeItem.RecipeNO))
                {
                }
                else
                {
                    dicRecipeSeq.Add(feeItem.RecipeNO, 1);
                }
                feeItem.SequenceNO = dicRecipeSeq[feeItem.RecipeNO];

                dicRecipeSeq[feeItem.RecipeNO] = dicRecipeSeq[feeItem.RecipeNO] + 1;
            }

            #endregion

            #endregion

            if (feeCount != alFeeItem.Count)
            {
                errText = "分方返回的费用列表数量不对，请联系电脑中心！";
                return false;
            }

            return true;
        }

        SortByQuaulity sortByQuaulity = new SortByQuaulity();

        /// <summary>
        /// 获取组合列表内用来分方的主要药品
        /// 例如里面包含精二和普通药物时，按照精二分方
        /// </summary>
        /// <param name="alFeeItem"></param>
        /// <returns></returns>
        private FS.HISFC.Models.Fee.Outpatient.FeeItemList GetSplitItem(ArrayList alCombFeeItem)
        {
            //优先顺序：3、精二；2、毒麻精一；1、普通

            try
            {
                alCombFeeItem.Sort(sortByQuaulity);
                return alCombFeeItem[0] as FS.HISFC.Models.Fee.Outpatient.FeeItemList;
            }
            catch
            {
                return alCombFeeItem[0] as FS.HISFC.Models.Fee.Outpatient.FeeItemList;
            }
        }


        /// <summary>
        /// 获取药品性质的分方类别
        /// 3、精二；2、毒麻精一；1、普通；0、非药品
        /// </summary>
        /// <param name="feeItem"></param>
        /// <returns></returns>
        public static int GetItemQaulity(FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItem)
        {
            //3、精二；2、毒麻精一；1、普通；0、非药品

            int quaulityType = 0;
            if (feeItem.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
            {
                FS.HISFC.Models.Base.Const quaulity = hsDrugQuaulity.GetObjectFromID((feeItem.Item as FS.HISFC.Models.Pharmacy.Item).Quality.ID) as FS.HISFC.Models.Base.Const;

                if (quaulity != null && quaulity.ID.Length > 0)
                {
                    if (quaulity.Memo.Contains("精二")

                        || quaulity.UserCode.Contains("P2")//精二
                        )
                    {
                        quaulityType = 3;
                    }
                    else if (quaulity.Memo.Contains("麻")
                        || quaulity.Memo.Contains("精一")

                        || quaulity.UserCode.Contains("P1")//精一
                        || quaulity.UserCode.Contains("P")//精神类
                        //毒药
                        )
                    {
                        quaulityType = 2;
                    }
                    //{564D4750-E736-432b-A5B6-B0B178AF1A11}毒药单独打印
                    else if(quaulity.Memo.Contains("毒") || quaulity.UserCode.Contains("S"))
                    {
                        quaulityType = 5;
                    }
                    else if (quaulity.UserCode.Contains("EM"))//材料// {5A6C578A-D565-42c8-B3FA-B4A52D1FABFB}
                    {
                        quaulityType = 4;
                    }
                    else
                    {
                        quaulityType = 1;
                    }
                }
            }

            return quaulityType;
        }

        /// <summary>
        /// 获取组合列表内药品的种类数
        /// </summary>
        /// <param name="alFeeItem"></param>
        /// <returns></returns>
        private int GetSubPhaCount(ArrayList alCombFeeItem)
        {
            int count = 0;
            Hashtable hsCombNo = new Hashtable();

            foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItem in alCombFeeItem)
            {
                //5个药一张方，含大输液；
                if (splitRule == 2)
                {
                    count++;
                }
                //5个组合一张方
                else if (splitRule == 3)
                {
                    if (!hsCombNo.Contains(feeItem.Order.Combo.ID))
                    {
                        count++;
                        hsCombNo.Add(feeItem.Order.Combo.ID, null);
                    }
                }
                //5个药一张方，不含大输液；
                else
                {
                    bool isSubPha = false;//是否大输液

                    if (feeItem.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        FS.HISFC.Models.Base.Const quaulity = hsDrugQuaulity.GetObjectFromID((feeItem.Item as FS.HISFC.Models.Pharmacy.Item).Quality.ID) as FS.HISFC.Models.Base.Const;

                        if (quaulity != null && quaulity.ID.Length > 0)
                        {
                            if (quaulity.Memo.Contains("大输液") 
                                || quaulity.UserCode.Contains("T")) //或者系统类别属于大输液
                            {
                                isSubPha = true;
                            }
                        }
                    }

                    if (!isSubPha)
                    {
                        count++;
                    }
                }
            }
            return count;
        }
    }


    /// <summary>
    /// 费用按照处方排序号排序
    /// </summary>
    public class SortByDortID : IComparer
    {
        #region IComparer 成员

        public int Compare(object x, object y)
        {
            try
            {
                FeeItemList feeItem1 = x as FeeItemList;
                FeeItemList feeItem2 = y as FeeItemList;

                //把药品排在前面，为了保证优先分配药品处方号，避免修改后分方的方号错乱
                int drugFlag1 = feeItem1.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug ? 1 : 2;
                int drugFlag2 = feeItem2.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug ? 1 : 2;

                int sord1 = feeItem1.Order.SortID;
                int sord2 = feeItem2.Order.SortID;

                if (drugFlag1 > drugFlag2)
                {
                    return 1;
                }
                else if (drugFlag1 == drugFlag2)
                {
                    if (sord1 > sord2)
                    {
                        return 1;
                    }
                    else if (sord1 == sord2)
                    {
                        return 0;
                    }
                    else if (sord1 < sord2)
                    {
                        return -1;
                    }
                }
                else
                {
                    return -1;
                }

                return 0;
            }
            catch
            {
                return 0;
            }
        }

        #endregion
    }


    /// <summary>
    /// 按照药品性质分方
    /// </summary>
    public class SortByQuaulity : IComparer
    {
        #region IComparer 成员

        public int Compare(object x, object y)
        {
            try
            {
                FeeItemList feeItem1 = x as FeeItemList;
                FeeItemList feeItem2 = y as FeeItemList;

                int sort1 = ISplitRecipe.GetItemQaulity(feeItem1);
                int sort2 = ISplitRecipe.GetItemQaulity(feeItem2);
                if (sort1 > sort2)
                {
                    return -1;
                }
                else if (sort1 == sort2)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
            catch
            {
                return 0;
            }
        }

        #endregion
    }
}
