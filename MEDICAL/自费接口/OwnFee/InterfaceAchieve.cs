using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace OwnFee
{
    class InterfaceAchieve : FS.HISFC.BizProcess.Interface.FeeInterface.IMedcare, FS.HISFC.BizProcess.Interface.FeeInterface.IMedcareExtend
    {
        //错误信息
        private string errMsg = string.Empty;

        private FS.HISFC.BizLogic.Fee.PactUnitInfo pactInfoManager = new FS.HISFC.BizLogic.Fee.PactUnitInfo();

        private FS.HISFC.BizLogic.Fee.PactUnitItemRate pactItemRate = new FS.HISFC.BizLogic.Fee.PactUnitItemRate();
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

            foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList f in feeDetails)
            {
               
                //if (f.FT.OwnCost == 0)
                //{
                //    f.FT.OwnCost = f.FT.TotCost;
                //}

                //FS.HISFC.Models.Base.PactItemRate pRate = null;
                //string itemCode = "";
                //if (f.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.UnDrug && f.UndrugComb != null && !string.IsNullOrEmpty(f.UndrugComb.ID))
                //{
                //    itemCode = f.UndrugComb.ID;
                //}
                //else
                //{
                //    itemCode = f.Item.ID;
 
                //}

                // pRate = this.pactItemRate.GetOnepPactUnitItemRateByItem(r.Pact.ID, itemCode);
                //    if (pRate == null)
                //    {
                //        pRate = this.pactItemRate.GetOnePaceUnitItemRateByFeeCode(r.Pact.ID, f.Item.MinFee.ID);
                //        if (pRate == null)
                //        {
                //            FS.HISFC.Models.Base.PactInfo p = this.pactInfoManager.GetPactUnitInfoByPactCode(r.Pact.ID);
                //            if (p == null)
                //            {
                //                this.errMsg = this.pactInfoManager.Err;

                //                return -1;
                //            }

                //            pRate = new FS.HISFC.Models.Base.PactItemRate();

                //            pRate.Rate = p.Rate;
                //        }
                //    }
                    
                //    f.Item.Price = FS.FrameWork.Public.String.FormatNumber(f.Item.Price * pRate.Rate.OwnRate, 4);
                //    if (f.Item.Price == 0)
                //    {
                //        f.Item.User03 = "全免";
                //    }
                //    f.FT.OwnCost = FS.FrameWork.Public.String.FormatNumber(f.FT.TotCost * pRate.Rate.OwnRate, 2);
                //    f.FT.PayCost = 0;
                //    f.FT.PubCost = 0;
                //    f.FT.DrugOwnCost = f.FT.TotCost - f.FT.OwnCost;//存在折扣金额 确保费用明细表  单价*数量=totcost+DRUG_OWNCOST
                //    //用来保存减免金额，打折金额不保存
                //    f.FT.RebateCost = FS.FrameWork.Public.String.FormatNumber(f.FT.OwnCost * pRate.Rate.RebateRate, 2);
                   
                //    f.FT.TotCost = f.FT.OwnCost + f.FT.PubCost + f.FT.PayCost;

                    payCost += f.FT.PayCost;
                    pubcost += f.FT.PubCost;
                    ownCost += f.FT.OwnCost;
                    rebateCost += f.FT.RebateCost;
                    totCost += f.FT.TotCost;
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

        public string GetCodeScanningVerification(FS.HISFC.Models.Registration.Register r, string codeNum)
        { return ""; }

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
            get { return true ; }
        }

        #endregion



        #region IMedcare 成员

        //{D57D577B-5A1C-4232-857A-E160F7E0D126}
        public int QueryFeeDetailsInpatient(FS.HISFC.Models.RADT.PatientInfo patient, ref System.Collections.ArrayList feeDetails)
        {
            //计算费用的总额
            // 公费计算
            FS.HISFC.Models.Base.FT ft = new FS.HISFC.Models.Base.FT();
            if (feeDetails != null)
            {
                foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList in feeDetails)
                {
                    ft.TotCost += feeItemList.FT.TotCost;
                    ft.OwnCost += feeItemList.FT.OwnCost;
                    ft.PayCost += feeItemList.FT.PayCost;
                    ft.PubCost += feeItemList.FT.PubCost;
                    ft.RebateCost += feeItemList.FT.RebateCost;
                    ft.DefTotCost += feeItemList.FT.DefTotCost;
                }

                ft.TotCost = FS.FrameWork.Public.String.FormatNumber(ft.TotCost, 2);
                ft.OwnCost = FS.FrameWork.Public.String.FormatNumber(ft.TotCost, 2);
                ft.PayCost = 0;
                ft.PubCost = ft.TotCost - ft.OwnCost;
                ft.RebateCost = FS.FrameWork.Public.String.FormatNumber(ft.RebateCost, 2);
                ft.DefTotCost = FS.FrameWork.Public.String.FormatNumber(ft.DefTotCost, 2);
            }

            patient.FT = ft;
            return 1;
        }

        public int QueryFeeDetailsOutpatient(FS.HISFC.Models.Registration.Register r, ref System.Collections.ArrayList feeDetails)
        {
            return 1 ;
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

            int intRes = 1;
            try
            {
                intRes = this.ComputeFeeCost(r.Pact.ID, ref feeDetails);

            }
            catch (Exception objEx)
            {
                this.errMsg = objEx.Message;
                intRes = -1;
                return intRes;
            }

            #endregion
            return 1;
        }

        /// <summary>
        /// 本地试算
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <returns></returns>
        public int LocalBalanceInpatient(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            int iRes = 1;
            this.errMsg = "";

            FS.FrameWork.Management.DataBaseManger dbMgr = new FS.FrameWork.Management.DataBaseManger();

            #region 变更非药品所有费用
            string sql = @"update fin_ipb_itemlist a
                                        set a.own_cost=a.tot_cost,
                                               a.pub_cost=0,
                                               a.pay_cost=0,
                                               a.upload_flag='',
                                               a.paykind_code='{1}',
                                               a.pact_code='{2}',
                                               a.ext_flag4=''
                                        where a.inpatient_no='{0}'
                                        and a.balance_state='0'
                                        and a.split_fee_flag='0'";

            iRes= dbMgr.ExecQuery(string.Format(sql, patientInfo.ID, patientInfo.Pact.PayKind.ID, patientInfo.Pact.ID));
            if (iRes <= 0)
            {
                this.errMsg = "变更非药品所有费用失败，" + dbMgr.Err;
                return iRes;
            }
            #endregion

            #region 变更药品所有费用
            sql = @"update fin_ipb_medicinelist a
                                        set a.own_cost=a.tot_cost,
                                               a.pub_cost=0,
                                               a.pay_cost=0,
                                               a.upload_flag='',
                                               a.paykind_code='{1}',
                                               a.pact_code='{2}',
                                               a.ext_flag4=''
                                        where a.inpatient_no='{0}'
                                        and a.balance_state='0'
                                        and a.split_fee_flag='0'";

            iRes = dbMgr.ExecQuery(string.Format(sql, patientInfo.ID, patientInfo.Pact.PayKind.ID, patientInfo.Pact.ID));
            if (iRes <= 0)
            {
                this.errMsg = "变更药品所有费用失败，"+dbMgr.Err;
                return iRes;
            }
            #endregion

            #region 更新住院费用汇总表费用信息

            sql = @"update fin_ipb_feeinfo t
	                        set t.tot_cost=nvl((select sum(a.tot_cost) from v_fin_ipb_feeinfo_cost a  
	                        where a.recipe_no=t.recipe_no  
	                        and a.fee_code=t.fee_code 
	                        and a.execute_deptcode=t.execute_deptcode 
	                        and a.inpatient_no=t.inpatient_no
	                        and a.balance_state='0'),0),
	                        t.own_cost=nvl((select sum(a.own_cost) from v_fin_ipb_feeinfo_cost a 
	                        where a.recipe_no=t.recipe_no  
	                        and a.fee_code=t.fee_code 
	                        and a.execute_deptcode=t.execute_deptcode 
	                        and a.inpatient_no=t.inpatient_no
	                        and a.balance_state='0' ),0),
	                        t.pay_cost=nvl((select sum(a.pay_cost) from v_fin_ipb_feeinfo_cost a 
	                        where a.recipe_no=t.recipe_no  
	                        and a.fee_code=t.fee_code 
	                        and a.execute_deptcode=t.execute_deptcode 
	                        and a.inpatient_no=t.inpatient_no
	                        and a.balance_state='0'  ),0),
	                        t.pub_cost=nvl((select sum(a.pub_cost) from v_fin_ipb_feeinfo_cost a 
	                        where a.recipe_no=t.recipe_no  
	                        and a.fee_code=t.fee_code 
	                        and a.execute_deptcode=t.execute_deptcode 
	                        and a.inpatient_no=t.inpatient_no
	                        and a.balance_state='0'  ),0),
                            t.paykind_code='{1}',
                            t.pact_code='{2}'
	                        where t.inpatient_no='{0}' and t.balance_state='0'";

            iRes = dbMgr.ExecQuery(string.Format(sql, patientInfo.ID, patientInfo.Pact.PayKind.ID, patientInfo.Pact.ID));
            if (iRes <= 0)
            {
                this.errMsg = "更新住院费用汇总表费用信息失败，"+dbMgr.Err;
                return iRes;
            }

            #endregion

            #region 更新住院主表费用信息

            sql = 
                    @"update fin_ipr_inmaininfo t
                    set 
                    t.tot_cost=nvl((select sum(a.tot_cost) from fin_ipb_feeinfo a where a.inpatient_no=t.inpatient_no and a.balance_state='0'),0),
                    t.own_cost=nvl((select sum(a.own_cost) from fin_ipb_feeinfo a where a.inpatient_no=t.inpatient_no and a.balance_state='0'),0),
                    t.pub_cost=nvl((select sum(a.pub_cost) from fin_ipb_feeinfo a where a.inpatient_no=t.inpatient_no and a.balance_state='0'),0),
                    t.pay_cost=nvl((select sum(a.pay_cost) from fin_ipb_feeinfo a where a.inpatient_no=t.inpatient_no and a.balance_state='0'),0),
                    t.paykind_code='{1}',
                    t.pact_code='{2}',
                    t.pact_name='{3}'
                    where t.inpatient_no='{0}'";

            iRes = dbMgr.ExecQuery(string.Format(sql, patientInfo.ID, patientInfo.Pact.PayKind.ID, patientInfo.Pact.ID,patientInfo.Pact.Name));
            if (iRes <= 0)
            {
                this.errMsg = dbMgr.Err;
                return iRes;
            }

            #endregion

            return iRes;
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
           

            return 1;


        }



        #endregion

        #endregion

        #region IMedcare 成员
        /// <summary>
        /// 判断指定门诊病人是否享受此医保
        /// {199EF4E9-EF21-4067-97A7-9AA97AF74CDE}
        /// </summary>
        /// <param name="r"></param>
        /// <returns>
        /// 0：可以享受居民门诊医保报销并且当日无报销记录
        /// -1：不可以享受居民门诊医保报销
        /// -2：失败
        /// 其它值：可以享受居民门诊医保报销并且当日有报销记录（返回值为当日报销次数）
        /// </returns>
        public int QueryCanMedicare(FS.HISFC.Models.Registration.Register r)
        {
            return 1;
        }

        #endregion
    }
}
