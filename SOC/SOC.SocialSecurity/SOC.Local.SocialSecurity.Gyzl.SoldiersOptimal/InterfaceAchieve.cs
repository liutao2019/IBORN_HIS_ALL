using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using FS.HISFC.Models.Fee.Outpatient;
using System.Data;

namespace FS.SOC.Local.SocialSecurity.Gyzl.SoldiersOptimal
{
    class InterfaceAchieve : FS.HISFC.BizProcess.Interface.FeeInterface.IMedcare, FS.HISFC.BizProcess.Interface.FeeInterface.IMedcareExtend
    {
        public InterfaceAchieve()
        {
            if (hasLocalFeeRate == null)
            {
                hasLocalFeeRate = new Hashtable();
            }
            if (hasLocalItemRate == null)
            {
                hasLocalItemRate = new Hashtable();
            }
        }
        //错误信息
        private string errMsg = string.Empty;
        #region IMedcare 成员

        public int BalanceInpatient(FS.HISFC.Models.RADT.PatientInfo patient, ref System.Collections.ArrayList feeDetails)
        {
            return 1;
        }

        public int BalanceOutpatient(FS.HISFC.Models.Registration.Register r, ref System.Collections.ArrayList feeDetails)
        {
            decimal totCost = 0;
            decimal payCost = 0;
            decimal pubcost = 0;
            decimal ownCost = 0;

            foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList f in feeDetails)
            {
                totCost += f.FT.TotCost;
                if (f.FT.OwnCost == 0)
                {
                    f.FT.OwnCost = f.FT.TotCost;
                }
                payCost += f.FT.PayCost;
                pubcost += f.FT.PubCost;
                ownCost += f.FT.OwnCost;

            }

            r.SIMainInfo.TotCost = totCost;
            r.SIMainInfo.PayCost = payCost;
            r.SIMainInfo.PubCost = pubcost;
            r.SIMainInfo.OwnCost = ownCost;

            //判断平衡(在确认r.SIMainInfo.TotCost 应该为r.SIMainInfo.PayCost+r.SIMainInfo.PubCost+r.SIMainInfo.OwnCost 的前提下)
            if (r.SIMainInfo.TotCost != r.SIMainInfo.PayCost + r.SIMainInfo.OwnCost + r.SIMainInfo.PubCost)
            {
                this.errMsg = "总金额与账户支付+公费支付+自费支付不等\n,请核对";
                return -1;
            }
            return 1;
        }

        public int CancelBalanceInpatient(FS.HISFC.Models.RADT.PatientInfo patient, ref System.Collections.ArrayList feeDetails)
        {
            return 1;
        }

        public int CancelBalanceOutpatient(FS.HISFC.Models.Registration.Register r, ref System.Collections.ArrayList feeDetails)
        {
            return 1;
        }

        public int CancelBalanceOutpatientHalf(FS.HISFC.Models.Registration.Register r, ref System.Collections.ArrayList feeDetails)
        {
            return 1;
        }
        public int DeleteUploadedFeeDetailInpatient(FS.HISFC.Models.RADT.PatientInfo patient, FS.HISFC.Models.Fee.Inpatient.FeeItemList f)
        {
            return 1;
        }

        public int DeleteUploadedFeeDetailOutpatient(FS.HISFC.Models.Registration.Register r, FS.HISFC.Models.Fee.Outpatient.FeeItemList f)
        {
            return 1;
        }

        public int DeleteUploadedFeeDetailsAllInpatient(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            return 1;
        }

        public int DeleteUploadedFeeDetailsAllOutpatient(FS.HISFC.Models.Registration.Register r)
        {
            return 1;
        }

        public int DeleteUploadedFeeDetailsInpatient(FS.HISFC.Models.RADT.PatientInfo patient, ref System.Collections.ArrayList feeDetails)
        {
            return 1;
        }

        public int DeleteUploadedFeeDetailsOutpatient(FS.HISFC.Models.Registration.Register r, ref System.Collections.ArrayList feeDetails)
        {
            return 1;
        }

        public string Description
        {
            get
            {
                return "自费接口不作任何修改";
            }
        }

        public string ErrCode
        {
            get
            {
                return "";
            }
        }

        public string ErrMsg
        {
            get
            {
                return this.errMsg;
            }
        }

        public int GetRegInfoInpatient(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            return 1;
        }

        public int GetRegInfoOutpatient(FS.HISFC.Models.Registration.Register r)
        {
            return 1;
        }

        public bool IsInBlackList(FS.HISFC.Models.Registration.Register r)
        {
            return false;
        }

        public bool IsInBlackList(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            return false;
        }

        public int MidBalanceInpatient(FS.HISFC.Models.RADT.PatientInfo patient, ref System.Collections.ArrayList feeDetails)
        {
            return 1;
        }

        public int ModifyUploadedFeeDetailInpatient(FS.HISFC.Models.RADT.PatientInfo patient, FS.HISFC.Models.Fee.Inpatient.FeeItemList f)
        {
            return 1;
        }

        public int ModifyUploadedFeeDetailOutpatient(FS.HISFC.Models.Registration.Register r, FS.HISFC.Models.Fee.Outpatient.FeeItemList f)
        {
            return 1;
        }

        public int ModifyUploadedFeeDetailsInpatient(FS.HISFC.Models.RADT.PatientInfo patient, ref System.Collections.ArrayList feeDetails)
        {
            return 1;
        }

        public int ModifyUploadedFeeDetailsOutpatient(FS.HISFC.Models.Registration.Register r, ref System.Collections.ArrayList feeDetails)
        {
            return 1;
        }

        public int PreBalanceInpatient(FS.HISFC.Models.RADT.PatientInfo patient, ref System.Collections.ArrayList feeDetails)
        {
            decimal totCost = 0m;
            decimal ownCost = 0m;
            decimal pubCost = 0m;
            decimal payCost = 0m;

            foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList f in feeDetails)
            {
                totCost += f.FT.TotCost;
                ownCost += f.FT.OwnCost;
                pubCost += f.FT.PubCost;
                payCost += f.FT.PayCost;
            }

            patient.SIMainInfo.TotCost = totCost;
            patient.SIMainInfo.OwnCost = ownCost;
            patient.SIMainInfo.PubCost = pubCost;
            patient.SIMainInfo.PayCost = payCost;

            //判断平衡(在确认patient.SIMainInfo.TotCost 应该为patient.SIMainInfo.PayCost + patient.SIMainInfo.PubCost + patient.SIMainInfo.OwnCost 的前提下)
            if (patient.SIMainInfo.TotCost != patient.SIMainInfo.OwnCost + patient.SIMainInfo.PubCost + patient.SIMainInfo.PayCost)
            {
                this.errMsg = "总金额与自费支付+公费支付+账户支付不等\n,请核对";
                return -1;
            }

            return 1;
        }

        public int PreBalanceOutpatient(FS.HISFC.Models.Registration.Register r, ref System.Collections.ArrayList feeDetails)
        {
            //{6C0AA776-45DA-48e6-9612-4722B360A8A5}门诊预结算代码采用结算代码,不能直接return 1;

            decimal totCost = 0;
            decimal payCost = 0;
            decimal pubcost = 0;
            decimal ownCost = 0;
            decimal rebateCost = 0;

            this.ComputeFeeCost(r.Pact.ID, ref feeDetails);

            foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList f in feeDetails)
            {
                totCost += f.FT.TotCost;
                if (f.FT.OwnCost == 0)
                {
                    f.FT.OwnCost = f.FT.TotCost;
                }
                payCost += f.FT.PayCost;
                pubcost += f.FT.PubCost;
                ownCost += f.FT.OwnCost;
                rebateCost += f.FT.RebateCost;
            }

            r.SIMainInfo.TotCost = totCost;
            r.SIMainInfo.PayCost = payCost;
            r.SIMainInfo.PubCost = pubcost;
            r.SIMainInfo.OwnCost = ownCost;

            //判断平衡(在确认r.SIMainInfo.TotCost 应该为r.SIMainInfo.PayCost+r.SIMainInfo.PubCost+r.SIMainInfo.OwnCost 的前提下)
            if (r.SIMainInfo.TotCost != r.SIMainInfo.PayCost + r.SIMainInfo.OwnCost + r.SIMainInfo.PubCost)
            {
                this.errMsg = "总金额与账户支付+公费支付+自费支付不等\n,请核对";
                return -1;
            }

            //{6C0AA776-45DA-48e6-9612-4722B360A8A5}完毕
            return 1;
        }

        public int QueryBlackLists(ref System.Collections.ArrayList blackLists)
        {
            return 1;
        }

        public int QueryDrugLists(ref System.Collections.ArrayList drugLists)
        {
            return 1;
        }

        public int QueryUndrugLists(ref System.Collections.ArrayList undrugLists)
        {
            return 1;
        }

        public int RecomputeFeeItemListInpatient(FS.HISFC.Models.RADT.PatientInfo patient, FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList)
        {
            feeItemList.FT.OwnCost = feeItemList.FT.TotCost;
            feeItemList.FT.PayCost = 0m;
            feeItemList.FT.PubCost = 0m;
            return 1;
        }

        public void SetTrans(System.Data.IDbTransaction t)
        {
            return;
        }

        public System.Data.IDbTransaction Trans
        {
            set
            {
            }
        }

        public int UpdateFeeItemListInpatient(FS.HISFC.Models.RADT.PatientInfo patient, FS.HISFC.Models.Fee.Inpatient.FeeItemList f)
        {
            return 1;
        }

        public int UploadFeeDetailInpatient(FS.HISFC.Models.RADT.PatientInfo patient, FS.HISFC.Models.Fee.Inpatient.FeeItemList f)
        {
            return 1;
        }

        public int UploadFeeDetailOutpatient(FS.HISFC.Models.Registration.Register r, FS.HISFC.Models.Fee.Outpatient.FeeItemList f)
        {
            return 1;
        }

        public int UploadFeeDetailsInpatient(FS.HISFC.Models.RADT.PatientInfo patient, ref System.Collections.ArrayList feeDetails)
        {
            return 1;
        }

        public int UploadFeeDetailsOutpatient(FS.HISFC.Models.Registration.Register r, ref System.Collections.ArrayList feeDetails)
        {
            return 1;
        }

        public int UploadRegInfoInpatient(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            return 1;
        }

        public int UploadRegInfoOutpatient(FS.HISFC.Models.Registration.Register r)
        {
            r.SIMainInfo.OwnCost = r.OwnCost;  //自费金额 
            r.SIMainInfo.PubCost = r.PubCost;  //统筹金额 
            r.SIMainInfo.PayCost = r.PayCost;  //帐户金额 

            return 1;
        }

        #endregion

        #region IMedcareTranscation 成员

        public void BeginTranscation()
        {
            return;
        }

        public long Commit()
        {
            return 1;
        }

        public long Connect()
        {
            return 1;
        }

        public long Disconnect()
        {
            return 1;
        }

        public long Rollback()
        {
            return 1;
        }

        #endregion

        #region IMedcare 成员


        public int CancelRegInfoInpatient(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            return 1;
        }

        public int LogoutInpatient(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            return 1;
        }
        public int RecallRegInfoInpatient(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            return 1;
        }

        #endregion

        #region IMedcare 成员


        public int CancelRegInfoOutpatient(FS.HISFC.Models.Registration.Register r)
        {
            return 1;
        }

        #endregion

        #region IMedcare 成员


        public bool IsUploadAllFeeDetailsOutpatient
        {
            get { return true; }
        }

        #endregion

        #region IMedcare 成员

        //{D57D577B-5A1C-4232-857A-E160F7E0D126}
        public int QueryFeeDetailsInpatient(FS.HISFC.Models.RADT.PatientInfo patient, ref System.Collections.ArrayList feeDetails)
        {
            return 1;
        }

        public int QueryFeeDetailsOutpatient(FS.HISFC.Models.Registration.Register r, ref System.Collections.ArrayList feeDetails)
        {
            return 1;
        }

        #endregion

        #region IMedcareExtend 成员

        bool blnLocalProcess = false;
        /// <summary>
        /// 获取或设置结算方式
        /// </summary>
        public bool IsLocalProcess
        {
            get
            {
                return blnLocalProcess;
            }
            set
            {
                blnLocalProcess = value;
            }
        }
        /// <summary>
        /// HIS内部医保结算
        /// {293FDD11-FC10-4ceb-8E4C-1A4304F22592}
        /// </summary>
        /// <param name="r">患者挂号信息</param>
        /// <param name="feeDetails">患者费用信息</param>
        /// <param name="arlOther">其他信息</param>
        /// <returns>-1 失败 0 没有记录 >=1 成功</returns>
        public int LocalBalanceOutpatient(FS.HISFC.Models.Registration.Register r, ref System.Collections.ArrayList feeDetails, ArrayList arlOther)
        {
            // {293FDD11-FC10-4ceb-8E4C-1A4304F22592}
            if (!blnLocalProcess)
            {
                return -1;
            }

            #region 校验

            if (string.IsNullOrEmpty(r.ID))
            {
                this.errMsg = "没有找到患者的挂号流水号!";
                return -1;
            }

            if (feeDetails == null || feeDetails.Count <= 0)
            {
                this.errMsg = "费用明细为空!";
                return -1;
            }

            if (string.IsNullOrEmpty(r.Pact.ID))
            {
                this.errMsg = "患者合同单位为空！";
                return -1;
            }

            #endregion

            #region 计算费用

            int intRes = this.ComputeFeeCost(r.Pact.ID, ref feeDetails);

            #endregion
            return 1;
        }

        #region 计算费用
        /// <summary>
        /// 计算费用
        /// pub_cost、 pay_cost、 own_cost、 eco_cost
        /// </summary>
        /// <param name="strPactCost"></param>
        /// <param name="feeDetails"></param>
        /// <returns></returns>
        private int ComputeFeeCost(string strPactCost, ref System.Collections.ArrayList feeDetails)
        {
            int intRes = 1;
            if (string.IsNullOrEmpty(strPactCost))
            {
                return intRes;
            }

            DataTable dtFeeRate = GetLocalFeeRate(strPactCost);
            DataTable dtItemRate = GetLocalItemRate(strPactCost);

            if (dtFeeRate == null && dtItemRate == null)
                return intRes;

            decimal decTotal = 0;
            string strItemID = null;
            DataRow[] drArr = null;
            DataRow drTemp = null;
            foreach (FeeItemList f in feeDetails)
            {
                // 未进行待遇算法处理前总费用
                decTotal = f.FT.OwnCost + f.FT.PubCost + f.FT.PayCost;
                strItemID = f.Item.ID;
                drArr = dtItemRate.Select("fee_code = '" + strItemID + "'");
                if (drArr == null || drArr.Length <= 0)
                {
                    strItemID = f.Item.MinFee.ID;
                    drArr = dtFeeRate.Select("fee_code = '" + strItemID + "'");
                    if (drArr == null || drArr.Length <= 0)
                    {
                        continue;
                    }
                    else
                    {
                        drTemp = drArr[0];

                        f.FT.RebateCost = FS.FrameWork.Public.String.FormatNumber(decTotal * FS.FrameWork.Function.NConvert.ToDecimal(drTemp[strEco_Cost]), 2);
                        f.FT.PubCost = 0;
                        f.FT.PayCost = 0;
                        f.FT.OwnCost = decTotal - f.FT.PubCost - f.FT.PayCost;
                        f.FT.TotCost = f.FT.PubCost + f.FT.PayCost + f.FT.OwnCost;

                    }
                }
                else
                {
                    drTemp = drArr[0];

                    f.FT.RebateCost = FS.FrameWork.Public.String.FormatNumber(decTotal * FS.FrameWork.Function.NConvert.ToDecimal(drTemp[strEco_Cost]), 2);
                    f.FT.PubCost = 0;
                    f.FT.PayCost = 0;
                    f.FT.OwnCost = decTotal - f.FT.PubCost - f.FT.PayCost;
                    f.FT.TotCost = f.FT.PubCost + f.FT.PayCost + f.FT.OwnCost;
                }
            }

            return intRes;

        }

        #endregion

        #endregion

        #region IMedcare 成员
        /// <summary>
        /// 判断指定门诊病人是否享受此医保
        /// </summary>
        /// <param name="r"></param>
        /// <returns>
        /// </returns>
        public int QueryCanMedicare(FS.HISFC.Models.Registration.Register r)
        {
            return 1;
        }

        #endregion


        /// <summary>
        /// 获取本地预结算费用比例 -- 最小费用类别
        /// </summary>
        /// <param name="strPactCode"></param>
        /// <returns></returns>
        public DataTable GetLocalFeeRate(string strPactCode)
        {
            DataTable dtTemp = null;
            if (!hasLocalFeeRate.ContainsKey(strPactCode))
            {
                 objSIManage.QueryLocalFeeRate(strPactCode, out dtTemp);
                hasLocalFeeRate[strPactCode] = dtTemp;
            }
            else
            {
                dtTemp = hasLocalFeeRate[strPactCode] as DataTable;
            }

            return dtTemp;
        }
        /// <summary>
        /// 获取本地预结算费用比例 -- 项目
        /// </summary>
        /// <param name="strPactCode"></param>
        /// <returns></returns>
        public DataTable GetLocalItemRate(string strPactCode)
        {
            DataTable dtTemp = null;
            if (!hasLocalItemRate.ContainsKey(strPactCode))
            {
                objSIManage.QueryLocalItemRate(strPactCode, out dtTemp);
                hasLocalItemRate[strPactCode] = dtTemp;
            }
            else
            {
                dtTemp = hasLocalItemRate[strPactCode] as DataTable;
            }

            return dtTemp;
        }


        /// <summary>
        /// 本地预结算费用比例 -- 最小费用类别
        /// </summary>
        private static Hashtable hasLocalFeeRate;
        /// <summary>
        /// 本地预结算费用比例 -- 项目
        /// </summary>
        private static Hashtable hasLocalItemRate;
        /// <summary>
        /// 使用自付比例
        /// </summary>
        private string strEco_Cost = "pay_ratio";

        private PublicLocalSIManage objSIManage = new PublicLocalSIManage();
    }
}
