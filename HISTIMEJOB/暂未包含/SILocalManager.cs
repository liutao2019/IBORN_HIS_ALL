using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace HISTIMEJOB
{
    
    class SILocalManager : Neusoft.FrameWork.Management.Database 
    {
        #region 医保本地预约结算
        /// <summary>

        /// <summary>
        /// 获得单条已对照信息
        /// </summary>
        /// <param name="pactCode"></param>
        /// <param name="itemCode"></param>
        /// <param name="objCompare"></param>
        /// <returns></returns>
        public int GetCompareSingleItem(string pactCode, string itemCode, ref Neusoft.HISFC.Models.SIInterface.Compare objCompare)
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
                    Neusoft.HISFC.Models.SIInterface.Compare obj = new  Neusoft.HISFC.Models.SIInterface.Compare();

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
                    obj.CenterItem.Rate = Neusoft.FrameWork.Function.NConvert.ToDecimal(Reader[12].ToString());
                    obj.CenterItem.Price = Neusoft.FrameWork.Function.NConvert.ToDecimal(Reader[13].ToString());
                    obj.CenterItem.Memo = Reader[14].ToString();
                    obj.SpellCode.SpellCode = Reader[15].ToString();
                    obj.SpellCode.WBCode = Reader[16].ToString();
                    obj.SpellCode.UserCode = Reader[17].ToString();
                    obj.Specs = Reader[18].ToString();
                    obj.Price = Neusoft.FrameWork.Function.NConvert.ToDecimal(Reader[19].ToString());
                    obj.DoseCode = Reader[20].ToString();
                    obj.CenterItem.OperCode = Reader[21].ToString();
                    obj.CenterItem.OperDate = Neusoft.FrameWork.Function.NConvert.ToDateTime(Reader[22].ToString());
                    obj.Name = Reader[23].ToString();
                    obj.RegularName = Reader[24].ToString();


                    al.Add(obj);
                }

                Reader.Close();

                if (al.Count > 0)
                {
                    objCompare = (Neusoft.HISFC.Models.SIInterface.Compare)al[0];
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
        public int UpdateMedItemList(Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList item)
        {
            string strSql = @"update fin_ipb_medicinelist a set a.tot_cost={3},a.own_cost={4},a.pub_cost={5},a.pay_cost={6} 
where a.recipe_no='{0}' and a.sequence_no='{1}' and a.trans_type='{2}'";
            try
            {
                string transType = (item.TransType == Neusoft.HISFC.Models.Base.TransTypes.Positive ? "1" : "2");
                strSql = string.Format(strSql, item.RecipeNO, item.SequenceNO, transType, item.FT.TotCost, item.FT.OwnCost, item.FT.PubCost, item.FT.PayCost);
                return this.ExecNoQuery(strSql);

            }
            catch (Exception exe)
            {
                this.Err = "更新药品费用明细表出错！" + exe.Message;
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 更新非药品费用明细表
        /// </summary>
        /// 根据处方号，处方号流水号，交易类型
        /// <returns></returns>
        public int UpdateItemList(Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList item)
        {
            string strSql = @"update fin_ipb_itemlist a set a.tot_cost={3},a.own_cost={4},a.pub_cost={5},a.pay_cost={6} 
where a.recipe_no='{0}' and a.sequence_no='{1}' and a.trans_type='{2}'";
            try
            {
                string transType = (item.TransType == Neusoft.HISFC.Models.Base.TransTypes.Positive ? "1" : "2");
                strSql = string.Format(strSql, item.RecipeNO, item.SequenceNO, transType, item.FT.TotCost, item.FT.OwnCost, item.FT.PubCost, item.FT.PayCost);
                return this.ExecNoQuery(strSql);

            }
            catch (Exception exe)
            {
                this.Err = "更新非药品费用明细表出错！" + exe.Message;
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 获取农保预结算上次执行时间
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="pactCode"></param>
        /// <returns></returns>
        public string GetJobLastExecDate(string inpatientNo, string pactCode)
        {
            string strSql = @"select A.lasttime from FIN_XNH_Localbalance A  WHERE  A.inpatientno='{0}' AND A.pactcode='{1}'";
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
        public int InsertOrUpdateLocalBalanceTime(Neusoft.HISFC.Models.RADT.PatientInfo patient, DateTime dt)
        {
            string strSql = @"insert into FIN_XNH_Localbalance(inpatientno,pactcode,lasttime,instate) values('{0}','{1}',to_date('{2}','yyyy-MM-dd hh24:mi:ss'),'{3}')";
            try
            {
                strSql = string.Format(strSql, patient.ID, patient.Pact.ID, dt, patient.PVisit.InState.ID.ToString());
                //唯一键错误
                if (-1 == this.ExecNoQuery(strSql))
                {
                    strSql = @"update FIN_XNH_Localbalance set lasttime=to_date('{2}','yyyy-MM-dd hh24:mi:ss'),instate='{3}' where inpatientno='{0}' and pactcode='{1}'";
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
        public Neusoft.HISFC.Models.Base.FT QueryFeeInfo(string recipeNo, string feeCode, string execDept)
        {
            string strSql = @"SELECT f.tot_cost,f.own_cost,f.pub_cost,f.pay_cost  from fin_ipb_feeinfo f where f.recipe_no ='{0}' and f.fee_code='{1}' and f.execute_deptcode='{2}'";
            try
            {
                strSql = string.Format(strSql, recipeNo, feeCode, execDept);
                this.ExecQuery(strSql);
                Neusoft.HISFC.Models.Base.FT ft = null;
                if (Reader.Read())
                {
                    ft = new Neusoft.HISFC.Models.Base.FT();
                    ft.TotCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(Reader[0].ToString());
                    ft.OwnCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(Reader[1].ToString());
                    ft.PubCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(Reader[2].ToString());
                    ft.PayCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(Reader[3].ToString());
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
        public int UpdateFeeInfo(Neusoft.HISFC.Models.Base.FT ft, string recipeNo, string feeCode, string execDept)
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
        public Neusoft.HISFC.Models.Base.FT QueryInMainInfo(string inpatientNo)
        {
            string strSql = @"select i.tot_cost,i.own_cost,i.pub_cost,i.pay_cost  from fin_ipr_inmaininfo i where i.inpatient_no='{0}'";
            try
            {
                strSql = string.Format(strSql, inpatientNo);
                this.ExecQuery(strSql);
                Neusoft.HISFC.Models.Base.FT ft = null;
                if (Reader.Read())
                {
                    ft = new Neusoft.HISFC.Models.Base.FT();
                    ft.TotCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(Reader[0].ToString());
                    ft.OwnCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(Reader[1].ToString());
                    ft.PubCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(Reader[2].ToString());
                    ft.PayCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(Reader[3].ToString());
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
        public int UpdateInMainInfo(Neusoft.HISFC.Models.Base.FT ft, string inpatientNo)
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

        #region 获取患者列表

        /// <summary>
        /// 根据合同单位获取在院患者（in_state!=O &&in_state!=N）
        /// </summary>
        /// <param name="pactCode"></param>
        /// <returns></returns>
        public ArrayList QueryXnhPatientInfo(string pactCode)
        {
            string strSql = @"select p.inpatient_no,p.pact_code,p.name,p.in_state,p.in_date ,p.patient_no from fin_ipr_inmaininfo p 
where p.pact_code ='{0}' and p.in_state !='O' and  p.in_state !='N' and p.in_state !='R'";
            try
            {
                strSql = string.Format(strSql, pactCode);
                this.ExecQuery(strSql);
                ArrayList al = new ArrayList();
                Neusoft.HISFC.Models.RADT.PatientInfo  patient = null;
                while (Reader.Read())
                {
                    patient = new Neusoft.HISFC.Models.RADT.PatientInfo();
                    patient.ID =Reader[0].ToString();
                    patient.Pact.ID = Reader[1].ToString();
                    patient.Name = Reader[2].ToString();
                    patient.PVisit.InState.ID = Reader[3].ToString();
                    patient.PVisit.InTime =Neusoft.FrameWork.Function.NConvert.ToDateTime( Reader[4].ToString());
                    patient.PID.PatientNO = Reader[5].ToString();
                    al.Add(patient);
                }
                Reader.Close();
                return al;
            }
            catch (Exception exp)
            {
                this.Err = "获取患者列表出错！" + exp.Message;
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
        /// 根据合同单位获取在院患者（in_state!=O &&in_state!=N）
        /// </summary>
        /// <param name="pactCode"></param>
        /// <returns></returns>
        public ArrayList QueryGZSIPatientInfo(string pactCode)
        {
            string strSql = @"select p.inpatient_no,p.pact_code,p.name,p.in_state,p.in_date from fin_ipr_inmaininfo p 
where p.paykind_code='02' and  p.pact_code !='{0}' and p.in_state !='O' and  p.in_state !='N'  and p.in_state !='R'";
            try
            {
                strSql = string.Format(strSql, pactCode);
                this.ExecQuery(strSql);
                ArrayList al = new ArrayList();
                Neusoft.HISFC.Models.RADT.PatientInfo patient = null;
                while (Reader.Read())
                {
                    patient = new Neusoft.HISFC.Models.RADT.PatientInfo();
                    patient.ID = Reader[0].ToString();
                    patient.Pact.ID = Reader[1].ToString();
                    patient.Name = Reader[2].ToString();
                    patient.PVisit.InState.ID = Reader[3].ToString();
                    patient.PVisit.InTime = Neusoft.FrameWork.Function.NConvert.ToDateTime(Reader[4].ToString());
                    al.Add(patient);
                }
                Reader.Close();
                return al;
            }
            catch (Exception exp)
            {
                this.Err = "获取患者列表出错！" + exp.Message;
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

        #endregion
    }
}
