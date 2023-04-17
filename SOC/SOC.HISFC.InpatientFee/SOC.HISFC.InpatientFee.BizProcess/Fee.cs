using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.HISFC.BizProcess.Integrate.FeeInterface;
using FS.SOC.HISFC.BizProcess.CommonInterface.Common;
using System.Collections;
using FS.SOC.HISFC.BizProcess.CommonInterface;

namespace FS.SOC.HISFC.InpatientFee.BizProcess
{
    /// <summary>
    /// [功能描述: 收费逻辑业务类]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2012-3]<br></br>
    /// </summary>
    public class Fee:AbstractBizProcess
    {
        #region 身份变更处理费用

        /// <summary>
        /// 身份变更处理费用
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <returns></returns>
        public int ProcessChangePact(FS.HISFC.Models.RADT.PatientInfo patientInfo,FS.HISFC.Models.Base.PactInfo oldPactInfo,FS.HISFC.Models.Base.PactInfo newPactInfo)
        {
            FS.SOC.HISFC.InpatientFee.BizLogic.FeeInfo feeInfoManager = new FS.SOC.HISFC.InpatientFee.BizLogic.FeeInfo();

            this.BeginTransaction();
            this.SetDB(feeInfoManager);

            if (feeInfoManager.ProcedueChangePactFee(patientInfo.ID, oldPactInfo, newPactInfo, feeInfoManager.Operator.ID) < 0)
            {
                this.RollBack();
                this.err = feeInfoManager.Err;
                return -1;
            }

            this.Commit();

            return 1;
        }

        #endregion

        #region 退费申请

        /// <summary>
        /// 是否存在退费申请（各类与费用相关的申请）
        /// </summary>
        /// <returns></returns>
        public bool IsExistApplyFee(string inpatientNO)
        {
            FS.HISFC.BizLogic.Fee.ReturnApply returnApplyManager = new FS.HISFC.BizLogic.Fee.ReturnApply();
            Pharmacy pharmacyMganager = new Pharmacy();
            returnApplyManager.SetTrans(this.Trans);
            pharmacyMganager.SetTrans(this.Trans);

            //判断是否有未确认掉的退费申请
            ArrayList returnApplysOfPharmacy = returnApplyManager.QueryReturnApplys(inpatientNO, false, true);//未确认的药品退费申请
            ArrayList returnApplysOfUndrug = returnApplyManager.QueryReturnApplys(inpatientNO, false, false);//未确认的非药品退费申请
            //{92BD4A97-79F4-46ea-A0D6-50AD78594DAD}
            int phaApplyCount = pharmacyMganager.QueryNoConfirmQuitApply(inpatientNO);
            if (returnApplysOfPharmacy.Count > 0 || returnApplysOfUndrug.Count > 0 || phaApplyCount > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region 账户管理

        public int CloseAccount(string inpatientNO)
        {
            this.BeginTransaction();
            FS.HISFC.BizLogic.Fee.InPatient inpatientFeeManager = new FS.HISFC.BizLogic.Fee.InPatient();
            inpatientFeeManager.SetTrans(this.Trans);
            //进行封帐操作 ---------------如果有院内帐号对帐号进行封帐
            if (inpatientFeeManager.CloseAccount(inpatientNO) == -1)
            {
                this.RollBack();
                this.err = inpatientFeeManager.Err;
                return -1;
            }
            this.Commit();

            return 1;
        }

        public int OpenAccount(string inpatientNO)
        {
            this.BeginTransaction();
            FS.HISFC.BizLogic.Fee.InPatient inpatientFeeManager = new FS.HISFC.BizLogic.Fee.InPatient();
            inpatientFeeManager.SetTrans(this.Trans);
            //进行封帐操作 ---------------如果有院内帐号对帐号进行封帐
            if (inpatientFeeManager.OpenAccount(inpatientNO) == -1)
            {
                this.RollBack();
                this.err = inpatientFeeManager.Err;
                return -1;
            }
            this.Commit();

            return 1;
        }

        #endregion

        #region 拆分费用

        /// <summary>
        ///  判断是否存在未拆分的费用
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <returns></returns>
        public bool IsExistNoSplitFee(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            return IsExistNoSplitFee(patientInfo, DateTime.MinValue, DateTime.MaxValue);
        }

        /// <summary>
        ///  判断是否存在未拆分的费用
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <returns></returns>
        public bool IsExistNoSplitFee(FS.HISFC.Models.RADT.PatientInfo patientInfo, DateTime beginTime, DateTime endTime)
        {
            FS.SOC.HISFC.InpatientFee.BizLogic.ItemList itemListMgr = new FS.SOC.HISFC.InpatientFee.BizLogic.ItemList();
            itemListMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            return itemListMgr.IsExistNoSplitFeeInfo(patientInfo.ID, beginTime, endTime);
        }

        /// <summary>
        /// 拆分费用信息
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <returns></returns>
        public int ProcessSplitFeeInfo(FS.HISFC.Models.RADT.PatientInfo patientInfo, DateTime beginTime, DateTime endTime)
        {
            //this.BeginTransaction();
            //FS.FrameWork.Management.PublicTrans.BeginTransaction();
            FS.SOC.HISFC.InpatientFee.BizLogic.FeeInfo socFeeInfoMgr = new FS.SOC.HISFC.InpatientFee.BizLogic.FeeInfo();
            socFeeInfoMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            //FS.HISFC.BizLogic.Fee.InPatient inpatientFeeManager = new FS.HISFC.BizLogic.Fee.InPatient();
            //FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();
            ////this.SetDB(inpatientFeeManager);
            //inpatientFeeManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            //feeIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            
            #region 1、判断是否有退费申请

            if (this.IsExistApplyFee(patientInfo.ID))
            {
                //this.RollBack();
                //FS.FrameWork.Management.PublicTrans.RollBack();
                this.err = "患者还存在未处理的退费申请，请重新操作";
                return -1;
            }

            #endregion

            //调用存储过程处理数据
            if (socFeeInfoMgr.ProcedueSplitFee(patientInfo.ID, beginTime, endTime, socFeeInfoMgr.Operator.ID) < 0)
            {
                //FS.FrameWork.Management.PublicTrans.RollBack();
                this.err = socFeeInfoMgr.Err;
                return -1;
            }

           // #region 2、开账

           // if (this.OpenAccount(patientInfo.ID) < 0)
           // {
           //     //this.RollBack();
           //     FS.FrameWork.Management.PublicTrans.RollBack();
           //     this.err = "开账失败，" + this.err;
           //     return -1;
           // }

           // #endregion

           // #region 3、取所有的费用

           // ArrayList alFeeItemList = inpatientFeeManager.QueryFeeItemListsForBalanceSplit(patientInfo.ID, beginTime, endTime);
           // if (alFeeItemList == null)
           // {
           //     FS.FrameWork.Management.PublicTrans.RollBack();
           //     //this.RollBack();
           //     this.err = "获取费用信息失败，"+this.err;
           //     return -1;
           // }

           // #endregion

           // #region 4、拆分并退费

           // //拆分后费用
           //ArrayList listSplitFeeItemLists=new ArrayList();
           //ArrayList listSplitHighFeeItemLists = new ArrayList();
           // ArrayList alQuitFeeItemList = new ArrayList();
           // foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList in alFeeItemList)
           // {
           //     if (feeItemList.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
           //     {
           //         continue;
           //     }

           //     FS.HISFC.Models.Fee.Inpatient.FeeItemList highFeeItemList = null;
           //     FS.HISFC.Models.Fee.Inpatient.FeeItemList normalFeeItemList = null;
           //     string splitID = inpatientFeeManager.GetUndrugRecipeNO();
           //     normalFeeItemList = this.processSplitFeeItemList(feeItemList, splitID, ref highFeeItemList);
           //     listSplitFeeItemLists.Add(normalFeeItemList);
           //     if (highFeeItemList != null)
           //     {
           //         listSplitHighFeeItemLists.Add(highFeeItemList);
           //     }

           //     //处理退费信息
           //     feeItemList.Item.Qty = feeItemList.NoBackQty;
           //     feeItemList.NoBackQty = 0;
           //     feeItemList.IsNeedUpdateNoBackQty = true;

           //     alQuitFeeItemList.Add(feeItemList);
           // }

           // if (alQuitFeeItemList.Count > 0)
           // {
           //     if (feeIntegrate.QuitItem(patientInfo, ref alQuitFeeItemList) < 0)
           //     {
           //         feeIntegrate.Rollback();
           //         FS.FrameWork.Management.PublicTrans.RollBack();
           //         //this.RollBack();
           //         this.err = "退费失败，" + feeIntegrate.Err;
           //         return -1;
           //     }
           // }

           // #endregion

           // #region 5、重收

           // if (listSplitFeeItemLists.Count > 0)
           // {
           //     if (feeIntegrate.FeeItem(patientInfo, ref listSplitFeeItemLists) < 0)
           //     {
           //         feeIntegrate.Rollback();
           //         FS.FrameWork.Management.PublicTrans.RollBack();
           //         //this.RollBack();
           //         this.err = "收费失败，" + feeIntegrate.Err;
           //         return -1;
           //     }
           // }

           // if (listSplitHighFeeItemLists.Count > 0)
           // {
           //     FS.HISFC.Models.Base.PactInfo pact = patientInfo.Pact.Clone();
           //     patientInfo.Pact.PayKind.ID = "01";
           //     patientInfo.Pact.PayKind.Name = "自费";
           //     patientInfo.Pact.ID = "1";
           //     patientInfo.Pact.Name = "自费";
           //     if (feeIntegrate.FeeItem(patientInfo, ref listSplitHighFeeItemLists) < 0)
           //     {
           //         feeIntegrate.Rollback();
           //         FS.FrameWork.Management.PublicTrans.RollBack();
           //         //this.RollBack();
           //         this.err = "收费失败，" + feeIntegrate.Err;
           //         patientInfo.Pact = pact;

           //         return -1;
           //     }
           //     patientInfo.Pact = pact;

           // }
           // #endregion

           // #region 6、关账

           // if (this.CloseAccount(patientInfo.ID) < 0)
           // {
           //     FS.FrameWork.Management.PublicTrans.RollBack();
           //     //this.RollBack();
           //     this.err = "关账失败，" + this.err;
           //     return -1;
           // }

           // #endregion
            //FS.FrameWork.Management.PublicTrans.Commit();
            //this.Commit();
            return 1;
        }

        /// <summary>
        /// 将高收费数据拆分
        /// </summary>
        /// <param name="feeItemLists"></param>
        /// <returns></returns>
        private FS.HISFC.Models.Fee.Inpatient.FeeItemList processSplitFeeItemList(FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList, string splitID, ref  FS.HISFC.Models.Fee.Inpatient.FeeItemList highFeeItemList)
        {

            //正常费用部分
            FS.HISFC.Models.Fee.Inpatient.FeeItemList normalFeeItemList = feeItemList.Clone();
            normalFeeItemList.RecipeNO = string.Empty;
            normalFeeItemList.SequenceNO = 0;
            normalFeeItemList.TransType = FS.HISFC.Models.Base.TransTypes.Positive;
            normalFeeItemList.FeeOper.ID = string.Empty;
            normalFeeItemList.FeeOper.OperTime = DateTime.MinValue;
            normalFeeItemList.FeeOper.Dept.ID = string.Empty;

            normalFeeItemList.SplitFlag = true;
            normalFeeItemList.SplitFeeFlag = false;
            normalFeeItemList.SplitID = splitID;


            if (feeItemList.Item.DefPrice <= 0 || feeItemList.Item.Price <= feeItemList.Item.DefPrice)
            {
                return normalFeeItemList;
            }
            else
            {
                #region 高收费
                //高收费部分
                highFeeItemList = feeItemList.Clone();
                highFeeItemList.RecipeNO = string.Empty;
                highFeeItemList.SequenceNO = 0;
                highFeeItemList.TransType = FS.HISFC.Models.Base.TransTypes.Positive;
                highFeeItemList.FeeOper.ID = string.Empty;
                highFeeItemList.FeeOper.OperTime = DateTime.MinValue;
                highFeeItemList.FeeOper.Dept.ID = string.Empty;

                highFeeItemList.Item.Price = feeItemList.Item.Price - feeItemList.Item.DefPrice;
                highFeeItemList.Item.DefPrice = 0;
                highFeeItemList.FT.TotCost = feeItemList.FT.TotCost - FS.FrameWork.Public.String.FormatNumber(feeItemList.Item.DefPrice * feeItemList.Item.Qty, 2);
                highFeeItemList.FT.OwnCost = highFeeItemList.FT.TotCost;
                highFeeItemList.FT.DefTotCost = 0;
                //自费的患者，计算普通费用的优惠金额
                if (feeItemList.Patient.Pact.PayKind.ID == "01")
                {
                    if (feeItemList.FT.FTRate.ItemRate > 0)
                    {
                        highFeeItemList.FT.RebateCost = FS.FrameWork.Public.String.FormatNumber(feeItemList.FT.RebateCost * feeItemList.FT.FTRate.ItemRate, 2);
                    }
                    else if (feeItemList.FT.RebateCost < highFeeItemList.FT.TotCost)
                    {
                        highFeeItemList.FT.RebateCost = highFeeItemList.FT.TotCost;
                    }
                    else
                    {
                        highFeeItemList.FT.RebateCost = feeItemList.FT.RebateCost;
                    }
                }
                //其他类型的患者，只允许高收费打折
                else if (feeItemList.FT.RebateCost > 0)
                {
                    highFeeItemList.FT.RebateCost = feeItemList.FT.RebateCost;
                }
                highFeeItemList.SplitFlag = true;
                highFeeItemList.SplitFeeFlag = true;
                highFeeItemList.SplitID = splitID;

                highFeeItemList.Patient.Pact.PayKind.ID = "01";
                highFeeItemList.Patient.Pact.PayKind.Name = "自费";
                highFeeItemList.Patient.Pact.ID = "1";
                highFeeItemList.Patient.Pact.Name = "自费";

                #endregion

                #region 正常收费
                normalFeeItemList.Item.Price = feeItemList.Item.DefPrice;
                normalFeeItemList.Item.DefPrice = normalFeeItemList.Item.Price;
                normalFeeItemList.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(normalFeeItemList.Item.Price * normalFeeItemList.Item.Qty, 2);
                normalFeeItemList.FT.OwnCost = normalFeeItemList.FT.TotCost - normalFeeItemList.FT.PayCost - normalFeeItemList.FT.PubCost;
                normalFeeItemList.FT.DefTotCost = normalFeeItemList.FT.TotCost;
                normalFeeItemList.FT.RebateCost = feeItemList.FT.RebateCost - highFeeItemList.FT.RebateCost;
                #endregion
            }


            return normalFeeItemList;
        }

        /// <summary>
        /// 将高收费数据拆分
        /// </summary>
        /// <param name="feeItemLists"></param>
        /// <returns></returns>
        public List<FS.HISFC.Models.Fee.Inpatient.FeeInfo> ProcessSplitFeeInfo(List<FS.HISFC.Models.Fee.Inpatient.FeeInfo> feeInfos)
        {
            if (feeInfos == null)
            {
                return null;
            }

            if (feeInfos.Count == 0)
            {
                return feeInfos;
            }

            List<FS.HISFC.Models.Fee.Inpatient.FeeInfo> newFeeInfos = new List<FS.HISFC.Models.Fee.Inpatient.FeeInfo>();

            FS.HISFC.Models.Fee.Inpatient.FeeInfo feeSpecialInfo = null;


            foreach (FS.HISFC.Models.Fee.Inpatient.FeeInfo feeInfo in feeInfos)
            {
                if (feeInfo.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                {
                    newFeeInfos.Add(feeInfo);
                    continue;
                }

                if (feeInfo.FT.DefTotCost <= 0 || feeInfo.FT.TotCost <= feeInfo.FT.DefTotCost)
                {
                    newFeeInfos.Add(feeInfo);
                }
                else
                {
                    #region 高收费部分
                    //高收费部分
                    FS.HISFC.Models.Fee.Inpatient.FeeInfo highFeeInfo = feeInfo.Clone();
                    highFeeInfo.FT.TotCost = feeInfo.FT.TotCost - feeInfo.FT.DefTotCost;
                    highFeeInfo.FT.OwnCost = highFeeInfo.FT.TotCost;
                    highFeeInfo.FT.PubCost = 0M;
                    highFeeInfo.FT.PayCost = 0M;
                    highFeeInfo.FT.DefTotCost = highFeeInfo.FT.TotCost;
                    //自费的患者，计算普通费用的优惠金额
                    if (feeInfo.Patient.Pact.PayKind.ID == "01")
                    {
                        if (feeInfo.FT.RebateCost < highFeeInfo.FT.RebateCost)
                        {
                            highFeeInfo.FT.RebateCost = highFeeInfo.FT.TotCost;
                        }
                        else
                        {
                            highFeeInfo.FT.RebateCost = feeInfo.FT.RebateCost;
                        }
                    }
                    //其他类型的患者，只允许高收费打折
                    else if (feeInfo.FT.RebateCost > 0)
                    {
                        highFeeInfo.FT.RebateCost = feeInfo.FT.RebateCost;
                    }
                    highFeeInfo.SplitFlag = true;
                    highFeeInfo.Patient.Pact.PayKind.ID = "01";
                    highFeeInfo.Patient.Pact.PayKind.Name = "自费";
                    highFeeInfo.SplitFeeFlag = true;

                    newFeeInfos.Add(highFeeInfo);


                    #endregion


                    //正常费用部分
                    FS.HISFC.Models.Fee.Inpatient.FeeInfo normalFeeInfo = feeInfo.Clone();

                    normalFeeInfo.FT.TotCost = feeInfo.FT.DefTotCost;
                    normalFeeInfo.FT.OwnCost = normalFeeInfo.FT.TotCost - normalFeeInfo.FT.PayCost - normalFeeInfo.FT.PubCost;
                    normalFeeInfo.FT.DefTotCost = normalFeeInfo.FT.TotCost;
                    normalFeeInfo.FT.RebateCost = feeInfo.FT.RebateCost - highFeeInfo.FT.RebateCost;
                    normalFeeInfo.SplitFlag = true;
                    normalFeeInfo.SplitFeeFlag = false;
                    newFeeInfos.Add(normalFeeInfo);
                }
            }

            return newFeeInfos;
        }

        /// <summary>
        /// 合并费用
        /// </summary>
        /// <param name="feeInfos"></param>
        /// <returns></returns>
        public List<FS.HISFC.Models.Fee.Inpatient.FeeInfo> ProcessCombineFeeInfo(List<FS.HISFC.Models.Fee.Inpatient.FeeInfo> feeInfos)
        {
            if (feeInfos == null)
            {
                return null;
            }

            if (feeInfos.Count == 0)
            {
                return feeInfos;
            }

            Dictionary<string, FS.HISFC.Models.Fee.Inpatient.FeeInfo> newDictionaryFeeInfos = new Dictionary<string, FS.HISFC.Models.Fee.Inpatient.FeeInfo>();

            foreach (FS.HISFC.Models.Fee.Inpatient.FeeInfo feeInfo in feeInfos)
            {
                if (newDictionaryFeeInfos.ContainsKey(feeInfo.Item.MinFee.ID))
                {
                    newDictionaryFeeInfos[feeInfo.Item.MinFee.ID].FT.TotCost += feeInfo.FT.TotCost;
                    newDictionaryFeeInfos[feeInfo.Item.MinFee.ID].FT.PubCost += feeInfo.FT.PubCost;
                    newDictionaryFeeInfos[feeInfo.Item.MinFee.ID].FT.PayCost += feeInfo.FT.PayCost;
                    newDictionaryFeeInfos[feeInfo.Item.MinFee.ID].FT.OwnCost += feeInfo.FT.OwnCost;
                    newDictionaryFeeInfos[feeInfo.Item.MinFee.ID].FT.RebateCost += feeInfo.FT.RebateCost;
                    newDictionaryFeeInfos[feeInfo.Item.MinFee.ID].FT.DefTotCost += feeInfo.FT.DefTotCost;
                    newDictionaryFeeInfos[feeInfo.Item.MinFee.ID].FT.DefTotCost += feeInfo.FT.DefTotCost;

                }
                else
                {
                    newDictionaryFeeInfos[feeInfo.Item.MinFee.ID] = feeInfo.Clone();
                    newDictionaryFeeInfos[feeInfo.Item.MinFee.ID].SplitFeeFlag = false;
                }

            }

            return newDictionaryFeeInfos.Values.ToList();
        }

        /// <summary>
        /// 合并已拆分的费用信息
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <returns></returns>
        public int ProcessCombineFeeInfo(FS.HISFC.Models.RADT.PatientInfo patientInfo, DateTime beginTime, DateTime endTime)
        {

            //this.BeginTransaction();
            //FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.HISFC.BizLogic.Fee.InPatient inpatientFeeManager = new FS.HISFC.BizLogic.Fee.InPatient();
            //FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();
            ////this.SetDB(inpatientFeeManager);
            //inpatientFeeManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            //feeIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            //try
            //{
            #region 1、判断是否有退费申请

            if (this.IsExistApplyFee(patientInfo.ID))
            {
                //this.RollBack();
                FS.FrameWork.Management.PublicTrans.RollBack();
                this.err = "患者还存在未处理的退费申请，请重新操作";
                return -1;
            }

            #endregion

            FS.SOC.HISFC.InpatientFee.BizLogic.FeeInfo socFeeInfoMgr = new FS.SOC.HISFC.InpatientFee.BizLogic.FeeInfo();
            socFeeInfoMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            //调用存储过程处理数据
            if (socFeeInfoMgr.ProcedueCombineFee(patientInfo.ID, beginTime, endTime, socFeeInfoMgr.Operator.ID) < 0)
            {
                //FS.FrameWork.Management.PublicTrans.RollBack();
                this.err = socFeeInfoMgr.Err;
                return -1;
            }
            //    #region 2、开账

            //    if (this.OpenAccount(patientInfo.ID) < 0)
            //    {
            //        //this.RollBack();
            //        FS.FrameWork.Management.PublicTrans.RollBack();
            //        this.err = "开账失败，" + this.err;
            //        return -1;
            //    }

            //    #endregion

            //    #region 3、取所有的费用

            //    ArrayList alFeeItemList = inpatientFeeManager.QueryFeeItemListsForBalanceCombine(patientInfo.ID, beginTime, endTime);
            //    if (alFeeItemList == null)
            //    {
            //        FS.FrameWork.Management.PublicTrans.RollBack();
            //        //this.RollBack();
            //        this.err = "获取费用信息失败，" + this.err;
            //        return -1;
            //    }

            //    #endregion

            //    #region 4、拆分并退费

            //    //拆分后费用
            //    ArrayList listCombineFeeItemLists = new ArrayList();
            //    Dictionary<string, FS.HISFC.Models.Fee.Inpatient.FeeItemList> dictionaryCombineFeeItemLists = new Dictionary<string, FS.HISFC.Models.Fee.Inpatient.FeeItemList>();
            //    ArrayList alQuitFeeItemList = new ArrayList();
            //    foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList in alFeeItemList)
            //    {
            //        //合并所有费用
            //        if (dictionaryCombineFeeItemLists.ContainsKey(feeItemList.SplitID))
            //        {
            //            dictionaryCombineFeeItemLists[feeItemList.SplitID].FT.TotCost += feeItemList.FT.TotCost;
            //            dictionaryCombineFeeItemLists[feeItemList.SplitID].FT.OwnCost += feeItemList.FT.OwnCost;
            //            dictionaryCombineFeeItemLists[feeItemList.SplitID].FT.PayCost += feeItemList.FT.PayCost;
            //            dictionaryCombineFeeItemLists[feeItemList.SplitID].FT.PubCost += feeItemList.FT.PubCost;
            //            dictionaryCombineFeeItemLists[feeItemList.SplitID].FT.RebateCost += feeItemList.FT.RebateCost;
            //            dictionaryCombineFeeItemLists[feeItemList.SplitID].FT.DefTotCost += feeItemList.FT.DefTotCost;
            //            dictionaryCombineFeeItemLists[feeItemList.SplitID].Item.Price += feeItemList.Item.Price;
            //            dictionaryCombineFeeItemLists[feeItemList.SplitID].Item.DefPrice += feeItemList.Item.DefPrice;

            //        }
            //        else
            //        {
            //            dictionaryCombineFeeItemLists[feeItemList.SplitID] = feeItemList.Clone();
            //            dictionaryCombineFeeItemLists[feeItemList.SplitID].SplitFeeFlag = false;
            //            dictionaryCombineFeeItemLists[feeItemList.SplitID].SplitFlag = false;
            //            dictionaryCombineFeeItemLists[feeItemList.SplitID].RecipeNO = string.Empty;
            //            dictionaryCombineFeeItemLists[feeItemList.SplitID].SequenceNO = 0;
            //            dictionaryCombineFeeItemLists[feeItemList.SplitID].TransType = FS.HISFC.Models.Base.TransTypes.Positive;
            //            dictionaryCombineFeeItemLists[feeItemList.SplitID].FeeOper.ID = string.Empty;
            //            dictionaryCombineFeeItemLists[feeItemList.SplitID].FeeOper.OperTime = DateTime.MinValue;
            //            dictionaryCombineFeeItemLists[feeItemList.SplitID].FeeOper.Dept.ID = string.Empty;
            //        }


            //        //处理退费信息
            //        feeItemList.Item.Qty = feeItemList.NoBackQty;
            //        feeItemList.NoBackQty = 0;
            //        feeItemList.IsNeedUpdateNoBackQty = true;

            //        alQuitFeeItemList.Add(feeItemList);
            //    }

            //    if (alQuitFeeItemList.Count > 0)
            //    {
            //        if (feeIntegrate.QuitItem(patientInfo, ref alQuitFeeItemList) < 0)
            //        {
            //            feeIntegrate.Rollback();
            //            FS.FrameWork.Management.PublicTrans.RollBack();
            //            //this.RollBack();
            //            this.err = "退费失败，" + feeIntegrate.Err;
            //            return -1;
            //        }
            //    }


            //    #endregion

            //    #region 5、重收

            //    ArrayList alCombineFeeItemLists = new ArrayList(dictionaryCombineFeeItemLists.Values.ToList());
            //    if (alCombineFeeItemLists.Count > 0)
            //    {
            //        if (feeIntegrate.FeeItem(patientInfo, ref alCombineFeeItemLists) < 0)
            //        {
            //            feeIntegrate.Rollback();
            //            FS.FrameWork.Management.PublicTrans.RollBack();
            //            //this.RollBack();
            //            this.err = "收费失败，" + feeIntegrate.Err;
            //            return -1;
            //        }
            //    }

            //    #endregion

            //    #region 6、关账

            //    if (this.CloseAccount(patientInfo.ID) < 0)
            //    {
            //        FS.FrameWork.Management.PublicTrans.RollBack();
            //        //this.RollBack();
            //        this.err = "关账失败，" + this.err;
            //        return -1;
            //    }

            //    #endregion
            //}
            //catch (Exception e)
            //{
            //    FS.FrameWork.Management.PublicTrans.RollBack();
            //    this.err = "关账失败，" + e.Message;
            //    return -1;
            //}
            //FS.FrameWork.Management.PublicTrans.Commit();
            //this.Commit();
            return 1;
        }
        #endregion

        #region 收费函数

        private int FeeManager(FS.HISFC.Models.RADT.PatientInfo patient, FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList,
            FS.HISFC.Models.Base.ChargeTypes payType, FS.HISFC.Models.Base.TransTypes transType)
        {
            return 1;
        }

        #endregion
    }
}
