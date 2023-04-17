using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using FS.HISFC.BizProcess.Interface.Common;

namespace GZSI
{
    /// <summary>
    /// 广州医保本地预结算
    /// </summary>
    class GZSILocalBalance : FS.FrameWork.Management.Database, IJob
    {
        #region 变量

        bool isComputed = true;//是否已经按比例计算费用

        FS.HISFC.BizLogic.RADT.InPatient managerIntegrate = new FS.HISFC.BizLogic.RADT.InPatient();
        FS.HISFC.BizLogic.Fee.InPatient feeMgr = new FS.HISFC.BizLogic.Fee.InPatient();
        FS.HISFC.BizProcess.Integrate.Fee feeProcess = new FS.HISFC.BizProcess.Integrate.Fee();
        FS.HISFC.BizLogic.Fee.InPatient inpatientManager = new FS.HISFC.BizLogic.Fee.InPatient();
        FS.HISFC.BizLogic.Fee.PactUnitInfo PactManagment = new FS.HISFC.BizLogic.Fee.PactUnitInfo();
        FS.HISFC.BizLogic.Fee.Interface interfaceMgr = new FS.HISFC.BizLogic.Fee.Interface();

        #endregion

        #region 方法

        public int LocalBalance()
        {
            //在院患者
            ArrayList alPatient = this.managerIntegrate.QueryPatient(FS.HISFC.Models.Base.EnumInState.I);
            if (alPatient == null)
            {
                errInfo = managerIntegrate.Err;
                return -1;
            }
            //出院登记患者
            ArrayList alPatientOut = this.managerIntegrate.QueryPatient(FS.HISFC.Models.Base.EnumInState.B);
            if (alPatient == null)
            {
                errInfo = managerIntegrate.Err;
                return -1;
            }

            alPatient.AddRange(alPatientOut);
            DateTime dtEndTime = this.feeMgr.GetDateTimeFromSysDateTime();

            foreach (FS.HISFC.Models.RADT.PatientInfo obj in alPatient)
            {
              //获取广州医保类型
                FS.HISFC.Models.Base.PactInfo pact = this.PactManagment.GetPactUnitInfoByPactCode(obj.Pact.ID);
                if (pact.PactDllName.ToLower() == "gzsi.dll")
                {
                    //收费时没有按比例计算费用
                    if (!isComputed)
                    {
                        this.LocalComputeInpatient(obj, false);

                    }
                    //if (obj.ID != "22141")
                    //{
                    //    continue;
                    //}
                    FS.HISFC.Models.Base.FT ft = this.feeMgr.QueryPatientSumFee(obj.ID, obj.PVisit.InTime.ToString(), dtEndTime.ToString());
                    if (ft != null)
                    {
                        obj.PVisit.MedicalType.ID = this.GetSiEmplType(obj.ID);
                        if (obj.PVisit.MedicalType.ID == "-1")
                        {
                            ft.RealCost = ft.OwnCost;
                            ft.PayCost = 0;
                            if (-1 == this.UpdateInMainInfo(obj.ID,  ft))
                            {
                                continue;
                            }
                            continue;
                        }
                        //
                        if (-1 == ComputePatientOwnFee(obj.ID, ref ft))
                        {
                            return -1;
                        }                    
                        if (-1 == this.ComputePatientSumFee(obj, ref ft))
                        {
                            continue;
                        }

                            
                    }
                    //更新医保住院主表
                    if (-1 == this.UpdateInMainInfo(obj.ID, ft))
                    {
                        return -1;
                    }
                }
            }
            return 1;

        }

        #endregion

        #region 私用方法

        private string GetSiEmplType(string inpatientNo)
        {
            string sql = "select si.empl_type from fin_ipr_siinmaininfo si where si.inpatient_no='{0}' and si.valid_flag='1'";
            string str = "1";
            try
            {
                sql = string.Format(sql, inpatientNo);
                str = this.managerIntegrate.ExecSqlReturnOne(sql,"-1");
                return str;
            }
            catch (Exception e)
            {
                this.errInfo = "获取医保类型出错！" + e.Message;
                return "1";
            }
        }

        /// <summary>
        /// 计算患者费用汇总信息（处理起付线和报销比例）
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="ft">根据fin_ipb_feeinfo 来获取费用信息</param>
        /// <returns></returns>
        private int ComputePatientSumFee(FS.HISFC.Models.RADT.PatientInfo patientInfo, ref FS.HISFC.Models.Base.FT ft)
        {
            //暂时用广州医保（2） 来查询起伏线
            ArrayList alInsurancedeal = this.inpatientManager.QueryInsurancedeal("2", patientInfo.PVisit.MedicalType.ID);
            if (alInsurancedeal != null && alInsurancedeal.Count > 0)
            {
                foreach (FS.HISFC.Models.SIInterface.Insurance insurance in alInsurancedeal)
                {
                    //满足区间条件
                    if (ft.PubCost > insurance.BeginCost && ft.PubCost <= insurance.EndCost)
                    {
                        //按区间比例
                        decimal dtKJZtot = ft.PubCost - insurance.BeginCost;
                        ft.FTRate.PayRate = insurance.Rate;
                        ft.SupplyCost = FS.FrameWork.Public.String.FormatNumber(dtKJZtot * insurance.Rate, 2);//个人自付部分= （总费用-自费-乙类自付-起伏线）* 自付比 
                        ft.PubCost = ft.TotCost - ft.OwnCost-ft.PayCost - ft.SupplyCost;//记账金额
                        ft.FTRate.PayRate = insurance.Rate;
                        ft.DefTotCost = insurance.BeginCost;//起伏线
                        ft.RealCost = ft.SupplyCost + ft.OwnCost+ft.PayCost + insurance.BeginCost;//实付金额 = 个人自付部门+纯自费+乙类自付+起伏线
                        break;
                    }
                }
                return 1;
            }
            else
            {
                return -1;
            }

        }

        /// <summary>
        /// 计算患者乙类费用金额
        /// </summary>
        /// <param name="?"></param>
        private int ComputePatientOwnFee(string inpatientNo, ref FS.HISFC.Models.Base.FT ft)
        {
            string sql = @"select sum(totTot),sum(totPub),sum(totPay) from 
                                (
                                select sum(tot_cost) totTot,sum(pub_cost) as totPub,sum(own_cost) as totPay from fin_ipb_medicinelist m 
                                where m.inpatient_no='{0}'
                                and m.pub_cost!=0
                                and m.tot_cost<>m.pub_cost

                                union 

                                select sum(tot_cost) totTot,sum(pub_cost) as totPub,sum(own_cost) as totPay from fin_ipb_itemlist m 
                                where m.inpatient_no='{0}'
                                and m.pub_cost!=0
                                and m.tot_cost<>m.pub_cost
                                )";
            try
            {
                sql = string.Format(sql, inpatientNo);
                this.ExecQuery(sql);
                while (this.Reader.Read())
                {
                    ft.PayCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[2].ToString());//乙类自付
                    ft.OwnCost = ft.OwnCost - ft.PayCost;  //纯自费
                }
                this.Reader.Close();
                return 1;

            }
            catch (Exception e)
            {
                this.errInfo = "计算患者乙类费用金额！" + e.Message;
                return -1;
            }

        }

        private int UpdateInMainInfo(string inpatientNo, FS.HISFC.Models.Base.FT ft)
        {
            string sql = "update fin_ipr_inmaininfo set FREE_COST=PREPAY_COST-{1}, EXT_NUMBER={2} where INPATIENT_NO='{0}' ";

            try
            {
                sql = string.Format(sql, inpatientNo, ft.RealCost,ft.PayCost);
                return this.managerIntegrate.ExecNoQuery(sql);

            }
            catch (Exception e)
            {
                this.errInfo = "更新医保住院的paycost出错！" + e.Message;
                return -1;
            }
        }

        #region 本地预结算

        /// <summary>
        /// 计算费用项目的费用金额（包括 TOT_COST,PUB_COST,PAY_COST,OWN_COST）
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        private int RecomputeFeeItemListInpatient(FS.HISFC.Models.RADT.PatientInfo patient, ref FS.HISFC.Models.Fee.Inpatient.FeeItemList item)
        {

            int returnValue = 0;

            FS.HISFC.Models.SIInterface.Compare objCompare = new FS.HISFC.Models.SIInterface.Compare();

            returnValue = interfaceMgr.GetCompareSingleItem(patient.Pact.ID, item.Item.ID, ref objCompare);

            if (returnValue == -1)
            {
                this.errInfo = interfaceMgr.Err;
                return returnValue;
            }
            if (returnValue == -2)
            {
                objCompare.CenterItem.Rate = 1;
            }

            item.FT.OwnCost = FS.FrameWork.Public.String.FormatNumber(item.FT.TotCost * objCompare.CenterItem.Rate, 2);
            item.FT.PubCost = item.FT.TotCost - item.FT.OwnCost;
            item.FT.PayCost = 0;
            item.FT.DerateCost = 0;//重算不考虑减免金额

            return 0;
        }
        
        /// <summary>
        /// 计算所有的费用，并更新费用明细（fin_ipb_medicinelist，fin_ipb_itemlist,fin_ipb_feeinfo、fin_ipr_inmaininfo）
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="feeItemLists"></param>
        /// <returns></returns>
        public int ComputeFeeCostInpatient(FS.HISFC.Models.RADT.PatientInfo patient, ref System.Collections.ArrayList feeItemLists)
        {
            for (int i = 0; i <= feeItemLists.Count - 1; ++i)
            {
                FS.HISFC.Models.Fee.Inpatient.FeeItemList item = feeItemLists[i] as FS.HISFC.Models.Fee.Inpatient.FeeItemList;

                FS.HISFC.Models.Fee.Inpatient.FeeItemList itemOld = item.Clone();

                #region 重新计算费用

                //判断身份变更费用明细
                if (item.Patient.Pact.ID != patient.Pact.ID) continue;

                if (RecomputeFeeItemListInpatient(patient, ref item) < 0)
                {
                    return -1;
                }
                #endregion

                #region 更新费用明细

                //更新费用明细表（fin_ipb_itemlist,fin_ipb_medicinelist）
                if (item.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                {
                    //更新药品费用明细表（fin_ipb_medicinelist）
                    if (UpdateMedItemList(item) < 0)
                    {
                        return -1;
                    }
                }
                else
                {
                    //更新非药品费用明细表（fin_ipb_itemlist）
                    if (UpdateItemList(item) < 0)
                    {
                        return -1;
                    }
                }
                #endregion

                #region 更新费用汇总
                //根据处方号、最小费用、执行科室 获取费用汇总信息
                FS.HISFC.Models.Base.FT ft = QueryFeeInfo(itemOld.RecipeNO, itemOld.Item.MinFee.ID, itemOld.ExecOper.Dept.ID);
                if (ft == null)
                {
                    return -1;
                }

                //更新费用汇总
                //增加重新计算的费用
                ft.TotCost = ft.TotCost - itemOld.FT.TotCost + item.FT.TotCost;
                ft.OwnCost = ft.OwnCost - itemOld.FT.OwnCost + item.FT.OwnCost;
                ft.PubCost = ft.PubCost - itemOld.FT.PubCost + item.FT.PubCost;
                ft.PayCost = ft.PayCost - itemOld.FT.PayCost + item.FT.PayCost;


                //更新费用汇总操作
                if (-1 == UpdateFeeInfo(ft, itemOld.RecipeNO, itemOld.Item.MinFee.ID, itemOld.ExecOper.Dept.ID))
                {
                    return -1;

                }
                #endregion

                #region 更新住院主表
                //根据住院流水号，获取住院主表费用
                FS.HISFC.Models.Base.FT ftMain = QueryInMainInfo(patient.ID);
                if (ftMain == null) return -1;


                //更新费用汇总
                //增加重新计算的费用
                ftMain.TotCost = ftMain.TotCost - itemOld.FT.TotCost + item.FT.TotCost;
                ftMain.OwnCost = ftMain.OwnCost - itemOld.FT.OwnCost + item.FT.OwnCost;
                ftMain.PubCost = ftMain.PubCost - itemOld.FT.PubCost + item.FT.PubCost;
                ftMain.PayCost = ftMain.PayCost - itemOld.FT.PayCost + item.FT.PayCost;


                //更新费用汇总操作
                if (-1 == UpdateInMainInfo(ftMain, patient.ID))
                {
                    return -1;
                }
                #endregion
            }
            return 1;
        }

        /// <summary>
        /// 本地预结算
        /// 预计广州医保预结算Job执行时间晚上 2:00
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="isChangePact">身份变更 true 不是身份变更 false</param>
        /// <returns></returns>
        public int LocalComputeInpatient(FS.HISFC.Models.RADT.PatientInfo patient, bool isChangePact)
        {
            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            this.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            inpatientManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            //查询广州医保预结算JOB 的上次执行时间
            DateTime dtLast = DateTime.MinValue;
            string lastTime = GetJobLastExecDate(patient.ID, patient.Pact.ID);
            if (string.IsNullOrEmpty(lastTime))
            {
                dtLast = patient.PVisit.InTime;
            }
            else
            {
                dtLast = FS.FrameWork.Function.NConvert.ToDateTime(lastTime);
            }

            if (dtLast == DateTime.MinValue)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                return -1;
            }

            DateTime dtNow = GetDateTimeFromSysDateTime();

            if (isChangePact)
            {
                //获取该患者所有在院未结费用，按每天费用处理

            }
            else
            {
                //非身份变更预结算
                if (patient.PVisit.InState.ID.ToString() == "C" || patient.PVisit.InState.ID.ToString() == "B")
                {
                    #region 出院预结算
                    DateTime dtFirst = dtLast.Date;
                    if (dtLast != patient.PVisit.InTime && dtLast.AddDays(1) > dtNow)
                    {
                        //出院当天费用的预结算
                        if (-1 == ExtcutePreBalance(patient, dtFirst))
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            return -1;
                        }
                    }
                    else
                    {

                        try
                        {
                            //出院前几天未跑广州医保预结算处理
                            System.TimeSpan interval = dtNow.Date - dtLast.Date;
                            if (interval.Days >= 1)
                            {
                                //多天未执行广州医保预结算，按每天来预结算费用                           
                                for (int i = 0; i <= interval.Days; i++)
                                {

                                    if (-1 == ExtcutePreBalance(patient, dtFirst))
                                    {
                                        FS.FrameWork.Management.PublicTrans.RollBack();
                                        return -1;
                                    }
                                    dtFirst = dtFirst.AddDays(1);

                                }


                            }

                        }
                        catch { }
                    }

                    #endregion //出院预结算

                    #region 更新广州医保预结算时间
                    //更新新农合预结算时间
                    if (-1 == InsertOrUpdateLocalBalanceTime(patient, GetDateTimeFromSysDateTime()))
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        return -1;
                    }
                    #endregion
                }
                else
                {
                    #region 在院预结算
                    //在院预结算
                    if (dtLast != patient.PVisit.InTime && dtLast.AddDays(1) > dtNow)
                    {
                        //已经预结算
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        return 1;
                    }
                    else
                    {
                        try
                        {
                            DateTime dtFirst = dtLast.Date;
                            System.TimeSpan interval = dtNow.Date - dtLast.Date;
                            if (interval.Days >= 1)
                            {
                                //多天未执行农保预结算，按每天来预结算费用

                                for (int i = 0; i < interval.Days; i++)
                                {

                                    if (-1 == ExtcutePreBalance(patient, dtFirst))
                                    {
                                        FS.FrameWork.Management.PublicTrans.RollBack();
                                        return -1;
                                    }
                                    dtFirst = dtFirst.AddDays(1);

                                }
                                #region 更新新农合预结算时间
                                //更新新农合预结算时间
                                DateTime dt = new DateTime(dtNow.Year, dtNow.Month, dtNow.Day, 2, 0, 0);
                                if (-1 == InsertOrUpdateLocalBalanceTime(patient, dt))
                                {
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                    return -1;
                                }
                                #endregion

                            }

                        }
                        catch { }

                    }

                    #endregion //end 在院预结算
                }
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            return 1;
        }

        /// <summary>
        /// 按时间执行当日本地预结算
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        private int ExtcutePreBalance(FS.HISFC.Models.RADT.PatientInfo patient, DateTime date)
        {
            DateTime dtBegin = date.Date;
            DateTime dtEnd = new DateTime(dtBegin.Year, dtBegin.Month, dtBegin.Day, 23, 59, 59);

            ArrayList alFeeItemList = new ArrayList();
            alFeeItemList = inpatientManager.QueryMedicineListsForBalance(patient.ID, dtBegin, dtEnd);

            alFeeItemList.AddRange(inpatientManager.QueryItemListsForBalance(patient.ID, dtBegin, dtEnd));

            if (alFeeItemList != null && alFeeItemList.Count > 0)
            {
                return this.ComputeFeeCostInpatient(patient, ref alFeeItemList);
            }
            return 1;
        }

        #region 医保本地预约结算
        /// <summary>

        /// <summary>
        /// 获得单条已对照信息
        /// </summary>
        /// <param name="pactCode"></param>
        /// <param name="itemCode"></param>
        /// <param name="objCompare"></param>
        /// <returns></returns>
        public int GetCompareSingleItem(string pactCode, string itemCode, ref FS.HISFC.Models.SIInterface.Compare objCompare)
        {
            string strSql = @"SELECT pact_code,   --合同单位
                                 his_code,   --本地项目编码
                                 center_code,   --医保收费项目编码
                                -- center_sys_class,   --项目类别 X-西药 Z-中药 L-诊疗项目 F-医疗服务设施
		                             (SELECT fin_xnh_siitem.item_flag FROM fin_xnh_siitem WHERE  fin_xnh_siitem.item_code = FIN_COM_COMPARE.center_code) AS item_flag,
                                 center_name,   --医保收费项目中文名称
                                 center_ename,   --医保收费项目英文名称
                                 center_specs,   --医保规格
                                 center_dose,   --医保剂型编码
                                 center_spell,   --医保拼音代码
                                 center_fee_code,   --医保费用分类代码 1 床位费 2西药费3中药费4中成药5中草药6检查费7治疗费8放射费9手术费10化验费11输血费12输氧费13其他
                                 center_item_type,   --医保目录级别 1 基本医疗范围 2 广东省厅补充
                                 center_item_grade,   --医保目录等级 1 甲类(统筹全部支付) 2 乙类(准予部分支付) 3 自费
                                 center_rate,   --自负比例
                                 center_price,   --基准价格
                                 center_memo,   --限制使用说明(医保备注)
                                 his_spell,   --医院拼音
                                 his_wb_code,   --医院五笔码
                                 his_user_code,   --医院自定义码
                                 his_specs,   --医院规格
                                 his_price,   --医院基本价格
                                 his_dose,   --医院剂型
                                 oper_code,   --操作员
                                 oper_date,
                                 his_name,
                                 REGULARNAME    --操作时间

                            FROM fin_com_compare   --医疗保险对照表
                            WHERE   pact_code = '{0}'
                            AND    his_code = '{1}'
                            ";
            try
            {
                strSql = string.Format(strSql, pactCode, itemCode);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            try
            {
                ArrayList al = new ArrayList();
                this.ExecQuery(strSql);
                while (Reader.Read())
                {
                    FS.HISFC.Models.SIInterface.Compare obj = new FS.HISFC.Models.SIInterface.Compare();

                    obj.CenterItem.PactCode = Reader[0].ToString();
                    obj.HisCode = Reader[1].ToString();
                    obj.CenterItem.ID = Reader[2].ToString();
                    obj.CenterItem.SysClass = Reader[3].ToString();
                    obj.CenterItem.Name = Reader[4].ToString();
                    obj.CenterItem.EnglishName = Reader[5].ToString();
                    obj.CenterItem.Specs = Reader[6].ToString();
                    obj.CenterItem.DoseCode = Reader[7].ToString();
                    obj.CenterItem.SpellCode = Reader[8].ToString();
                    obj.CenterItem.FeeCode = Reader[9].ToString();
                    obj.CenterItem.ItemType = Reader[10].ToString();
                    obj.CenterItem.ItemGrade = Reader[11].ToString();
                    obj.CenterItem.Rate = FS.FrameWork.Function.NConvert.ToDecimal(Reader[12].ToString());
                    obj.CenterItem.Price = FS.FrameWork.Function.NConvert.ToDecimal(Reader[13].ToString());
                    obj.CenterItem.Memo = Reader[14].ToString();
                    obj.SpellCode.SpellCode = Reader[15].ToString();
                    obj.SpellCode.WBCode = Reader[16].ToString();
                    obj.SpellCode.UserCode = Reader[17].ToString();
                    obj.Specs = Reader[18].ToString();
                    obj.Price = FS.FrameWork.Function.NConvert.ToDecimal(Reader[19].ToString());
                    obj.DoseCode = Reader[20].ToString();
                    obj.CenterItem.OperCode = Reader[21].ToString();
                    obj.CenterItem.OperDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[22].ToString());
                    obj.Name = Reader[23].ToString();
                    obj.RegularName = Reader[24].ToString();

                    al.Add(obj);
                }

                Reader.Close();

                if (al.Count > 0)
                {
                    objCompare = (FS.HISFC.Models.SIInterface.Compare)al[0];
                    return 0;
                }
                else
                {
                    return -2;
                }
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }
        }

        /// <summary>
        /// 更新药品费用明细表
        /// 根据处方号，处方号，交易类型
        /// </summary>
        /// <returns></returns>
        public int UpdateMedItemList(FS.HISFC.Models.Fee.Inpatient.FeeItemList item)
        {
            string strSql = @"update fin_ipb_medicinelist a set a.tot_cost={3},a.own_cost={4},a.pub_cost={5},a.pay_cost={6} 
                                where a.recipe_no='{0}' and a.sequence_no='{1}' and a.trans_type='{2}'";
            try
            {
                string transType = (item.TransType == FS.HISFC.Models.Base.TransTypes.Positive ? "1" : "2");
                strSql = string.Format(strSql, item.RecipeNO, item.SequenceNO, transType, item.FT.TotCost, item.FT.OwnCost, item.FT.PubCost, item.FT.PayCost);
                return this.ExecNoQuery(strSql);

            }
            catch (Exception exe)
            {
                this.Err = "更新药品费用明细表出错！" + exe.Message;
                return -1;
            }
        }

        /// <summary>
        /// 更新非药品费用明细表
        /// </summary>
        /// 根据处方号，处方号流水号，交易类型
        /// <returns></returns>
        public int UpdateItemList(FS.HISFC.Models.Fee.Inpatient.FeeItemList item)
        {
            string strSql = @"update fin_ipb_itemlist a set a.tot_cost={3},a.own_cost={4},a.pub_cost={5},a.pay_cost={6} 
                                where a.recipe_no='{0}' and a.sequence_no='{1}' and a.trans_type='{2}'";
            try
            {
                string transType = (item.TransType == FS.HISFC.Models.Base.TransTypes.Positive ? "1" : "2");
                strSql = string.Format(strSql, item.RecipeNO, item.SequenceNO, transType, item.FT.TotCost, item.FT.OwnCost, item.FT.PubCost, item.FT.PayCost);
                return this.ExecNoQuery(strSql);

            }
            catch (Exception exe)
            {
                this.Err = "更新非药品费用明细表出错！" + exe.Message;
                return -1;
            }
        }

        /// <summary>
        /// 获取农保预结算上次执行时间
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="pactCode"></param>
        /// <returns></returns>
        public string GetJobLastExecDate(string inpatientNo, string pactCode)
        {
            string strSql = @"select A.lasttime from FIN_Localbalance A  WHERE  A.inpatientno='{0}' AND A.pactcode='{1}'";
            try
            {
                strSql = string.Format(strSql, inpatientNo, pactCode);
                return this.ExecSqlReturnOne(strSql, "");

            }
            catch (Exception exe)
            {
                this.Err = "获取农保预结算上次执行时间出错！" + exe.Message;
                return "";
            }
        }

        /// <summary>
        /// 获取农保预结算下次执行时间
        /// </summary>
        /// <param name="jobCode"></param>
        /// <returns></returns>
        public string GetJobNextExecDate(string jobCode)
        {
            string strSql = @"select j.next_dtime from com_job j where j.job_code='{0}'";
            try
            {
                strSql = string.Format(strSql, jobCode);
                return this.ExecSqlReturnOne(strSql, "");

            }
            catch (Exception exe)
            {
                this.Err = "获取农保预结算下次执行时间出错！" + exe.Message;
                return "";
            }
        }

        /// <summary>
        /// 插入或更新预结算时间表
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public int InsertOrUpdateLocalBalanceTime(FS.HISFC.Models.RADT.PatientInfo patient, DateTime dt)
        {
            string strSql = @"insert into FIN_Localbalance(inpatientno,pactcode,lasttime,instate) values('{0}','{1}',to_date('{2}','yyyy-MM-dd hh24:mi:ss'),'{3}')";
            try
            {
                strSql = string.Format(strSql, patient.ID, patient.Pact.ID, dt, patient.PVisit.InState.ID.ToString());
                //唯一键错误
                if (-1 == this.ExecNoQuery(strSql))
                {
                    strSql = @"update FIN_Localbalance set lasttime=to_date('{2}','yyyy-MM-dd hh24:mi:ss'),instate='{3}' where inpatientno='{0}' and pactcode='{1}'";
                    strSql = string.Format(strSql, patient.ID, patient.Pact.ID, dt, patient.PVisit.InState.ID.ToString());
                    return this.ExecNoQuery(strSql);
                }

            }
            catch (Exception exe)
            {
                this.Err = "插入或更新预结算时间表出错！" + exe.Message;
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 获取费用汇总记录
        /// </summary>
        /// <param name="recipeNo"></param>
        /// <param name="feeCode"></param>
        /// <param name="execDept"></param>
        /// <returns></returns>
        public FS.HISFC.Models.Base.FT QueryFeeInfo(string recipeNo, string feeCode, string execDept)
        {
            string strSql = @"SELECT f.tot_cost,f.own_cost,f.pub_cost,f.pay_cost  from fin_ipb_feeinfo f where f.recipe_no ='{0}' and f.fee_code='{1}' and f.execute_deptcode='{2}'";
            try
            {
                strSql = string.Format(strSql, recipeNo, feeCode, execDept);
                this.ExecQuery(strSql);
                FS.HISFC.Models.Base.FT ft = null;
                if (Reader.Read())
                {
                    ft = new FS.HISFC.Models.Base.FT();
                    ft.TotCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[0].ToString());
                    ft.OwnCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[1].ToString());
                    ft.PubCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[2].ToString());
                    ft.PayCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[3].ToString());
                }
                Reader.Close();
                return ft;
            }
            catch (Exception exp)
            {
                this.Err = "获取费用汇总记录出错" + exp.Message;
                return null;
            }
            finally
            {
                if (!Reader.IsClosed)
                {
                    Reader.Close();
                }
            }
        }

        /// <summary>
        /// 更新费用汇总
        /// </summary>
        /// <param name="ft"></param>
        /// <param name="recipeNo"></param>
        /// <param name="feeCode"></param>
        /// <param name="execDept"></param>
        /// <returns></returns>
        public int UpdateFeeInfo(FS.HISFC.Models.Base.FT ft, string recipeNo, string feeCode, string execDept)
        {
            string strSql = @"update fin_ipb_feeinfo f set f.tot_cost={3},f.own_cost={4},f.pub_cost={5},f.pay_cost={6} 
                                where f.recipe_no ='{0}' and f.fee_code='{1}' and f.execute_deptcode='{2}'";
            try
            {
                strSql = string.Format(strSql, recipeNo, feeCode, execDept, ft.TotCost, ft.OwnCost, ft.PubCost, ft.PayCost);
                return this.ExecNoQuery(strSql);

            }
            catch (Exception exp)
            {
                this.Err = "更新费用汇总记录出错" + exp.Message;
                return -1;
            }
        }

        /// <summary>
        /// 获取住院主表信息
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <returns></returns>
        public FS.HISFC.Models.Base.FT QueryInMainInfo(string inpatientNo)
        {
            string strSql = @"select i.tot_cost,i.own_cost,i.pub_cost,i.pay_cost  from fin_ipr_inmaininfo i where i.inpatient_no='{0}'";
            try
            {
                strSql = string.Format(strSql, inpatientNo);
                this.ExecQuery(strSql);
                FS.HISFC.Models.Base.FT ft = null;
                if (Reader.Read())
                {
                    ft = new FS.HISFC.Models.Base.FT();
                    ft.TotCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[0].ToString());
                    ft.OwnCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[1].ToString());
                    ft.PubCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[2].ToString());
                    ft.PayCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[3].ToString());
                }
                Reader.Close();
                return ft;
            }
            catch (Exception exp)
            {
                this.Err = "获取住院主表记录出错" + exp.Message;
                return null;
            }
            finally
            {
                if (!Reader.IsClosed)
                {
                    Reader.Close();
                }
            }
        }

        /// <summary>
        /// 更新住院主表记录
        /// </summary>
        /// <param name="ft"></param>
        /// <param name="inpatientNo"></param>
        /// <returns></returns>
        public int UpdateInMainInfo(FS.HISFC.Models.Base.FT ft, string inpatientNo)
        {
            string strSql = @"update fin_ipr_inmaininfo i set i.tot_cost={1},i.own_cost={2},i.pub_cost={3},i.pay_cost={4} where  i.inpatient_no='{0}'";
            try
            {
                strSql = string.Format(strSql, inpatientNo, ft.TotCost, ft.OwnCost, ft.PubCost, ft.PayCost);
                return this.ExecNoQuery(strSql);

            }
            catch (Exception exp)
            {
                this.Err = "更新住院主表记录出错" + exp.Message;
                return -1;
            }
        }

        #endregion

        #endregion

        #endregion

        #region IJob 成员

        private string errInfo = "";

        public string Message
        {
            get
            {
                return errInfo;
            }
        }

        public int Start()
        {

            return this.LocalBalance();

        }

        #endregion
    }
}
