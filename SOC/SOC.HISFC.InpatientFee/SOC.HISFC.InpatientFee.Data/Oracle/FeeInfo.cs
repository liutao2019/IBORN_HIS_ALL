using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.InpatientFee.Data.Oracle
{
    [DataBase(FS.FrameWork.Management.Connection.EnumDBType.ORACLE)]
    class FeeInfo:AbstractFeeInfo
    {
        #region Select

        public override string SelectSumFeeByNotInFeeInfo
        {
            get {
                return
                    @"
                                select inpatient_no,
                                            sum(tot_cost),
                                            sum(own_cost),
                                            sum(pay_cost),
                                            sum(pub_cost),
                                            sum(eco_cost)
                                from 
                                (
                                 select t.inpatient_no,sum(t.tot_cost) tot_cost,sum(t.own_cost) own_cost,sum(t.pay_cost) pay_cost, sum(t.pub_cost) pub_cost,sum(t.eco_cost) eco_cost
                                 from fin_ipb_itemlist t 
                                 where t.inpatient_no='{0}'
                                 and t.balance_state='0'
                                 and ext_flag2 like '%N'
                                 group by t.inpatient_no
                                 union all
                                 select t.inpatient_no,sum(t.tot_cost) tot_cost,sum(t.own_cost) own_cost,sum(t.pay_cost) pay_cost, sum(t.pub_cost) pub_cost,sum(t.eco_cost) eco_cost
                                 from fin_ipb_medicinelist t 
                                 where t.inpatient_no='{0}'
                                 and t.balance_state='0'
                                 and ext_flag2 like '%N'
                                 group by t.inpatient_no
                                 )
                                 group by inpatient_no
                                "
                ; }
        }

        public override string SelectAll
        {
            get { return ""; }
        }

        #endregion

        #region Insert

        public override string Insert
        {
            get { throw new NotImplementedException(); }
        }

        public override string InsertAllDetailNotInFeeInfo
        {
            get
            {
                return @"
                             insert into fin_ipb_feeinfo
                                 select t.recipe_no,
                                 t.fee_code,
                                 t.trans_type,
                                 t.inpatient_no,
                                 t.name,
                                 t.paykind_code,
                                 t.pact_code,
                                 t.inhos_deptcode,
                                 t.nurse_cell_code,
                                 t.recipe_deptcode,
                                 t.execute_deptcode,
                                 t.MEDICINE_DEPTCODE,
                                 t.recipe_doccode,
                                 sum(t.tot_cost) tot_cost,
                                 sum(t.own_cost) own_cost,
                                 sum(t.pay_cost) pay_cost,
                                 sum(t.pub_cost) pub_cost,
                                 sum(t.eco_cost) eco_cost,
                                 t.charge_opercode,
                                 t.charge_date,
                                 '{1}' fee_opercode,
                                 sysdate fee_date,
                                 '',
                                 '',
                                 '',
                                 '0',
                                 t.balance_state,
                                 t.check_no,
                                 t.baby_flag,
                                 t.ext_flag,
                                 min(t.ext_code),
                                 t.ext_date,
                                 t.ext_opercode,
                                 t.feeoper_deptcode,
                                 t.ext_flag1,
                                 t.ext_flag2
                                 from fin_ipb_medicinelist t 
                                 where t.inpatient_no='{0}'
                                 and t.balance_state='0'
                                 and ext_flag2 like '%N'
                                 group by t.recipe_no,t.fee_code,t.trans_type,t.inpatient_no,t.name,t.paykind_code,t.pact_code,t.inhos_deptcode,t.nurse_cell_code,
                                 t.recipe_deptcode,t.execute_deptcode,t.MEDICINE_DEPTCODE,t.recipe_doccode,t.charge_opercode,t.charge_date,t.check_no,t.baby_flag,
                                 t.ext_flag,t.ext_date,t.ext_opercode,t.feeoper_deptcode,t.ext_flag1,t.ext_flag2,t.balance_state
                                 union all
                                 select t.recipe_no,
                                 t.fee_code,
                                 t.trans_type,
                                 t.inpatient_no,
                                 t.name,
                                 t.paykind_code,
                                 t.pact_code,
                                 t.inhos_deptcode,
                                 t.nurse_cell_code,
                                 t.recipe_deptcode,
                                 t.execute_deptcode,
                                 t.stock_deptcode,
                                 t.recipe_doccode,
                                 sum(t.tot_cost) tot_cost,
                                 sum(t.own_cost) own_cost,
                                 sum(t.pay_cost) pay_cost,
                                 sum(t.pub_cost) pub_cost,
                                 sum(t.eco_cost) eco_cost,
                                 t.charge_opercode,
                                 t.charge_date,
                                 '{1}' fee_opercode,
                                 sysdate fee_date,
                                 '',
                                 '',
                                 '',
                                 '0',
                                 t.balance_state,
                                 t.check_no,
                                 t.baby_flag,
                                 t.ext_flag,
                                 min(t.ext_code),
                                 t.ext_date,
                                 t.ext_opercode,
                                 t.feeoper_deptcode,
                                 t.ext_flag1,
                                 t.ext_flag2
                                 from fin_ipb_itemlist t 
                                 where t.inpatient_no='{0}'
                                 and t.balance_state='0'
                                 and ext_flag2 like '%N'
                                 group by t.recipe_no,t.fee_code,t.trans_type,t.inpatient_no,t.name,t.paykind_code,t.pact_code,t.inhos_deptcode,t.nurse_cell_code,
                                 t.recipe_deptcode,t.execute_deptcode,t.stock_deptcode,t.recipe_doccode,t.charge_opercode,t.charge_date,t.check_no,t.baby_flag,
                                 t.ext_flag,t.ext_date,t.ext_opercode,t.feeoper_deptcode,t.ext_flag1,t.ext_flag2,t.balance_state    
                            "
                ;
            }
        }

        #endregion

        #region Update

        public override string UpdateMainFee
        {
            get {

                return @"
                            UPDATE	fin_ipr_inmaininfo	--更新住院主表
							SET tot_cost	= tot_cost + {1},   
								own_cost	= own_cost + {2},   
								pay_cost	= pay_cost + {3},   
								pub_cost	= pub_cost + {4},   
								free_cost = free_cost - {2},
								eco_cost = eco_cost + {5}
						WHERE inpatient_no = '{0}'  
						and in_state <> 'O' and in_state <> 'C'	
                ";

            }
        }

        public override string UpdateFTSource
        {
            get {
                return @"
                                update fin_ipb_feeinfo
                                set ext_flag2=substr(lpad(ext_flag2,3,'0'),1,2)||'1'
                                where inpatient_no='{0}'
                                and balance_state='0'
                                and ext_flag2 like '%N'
                                ";
               }
        }

        #endregion

        #region Procedue
        /// <summary>
        /// 存储过程拆分费用
        /// </summary>
        public override string ProcedueSplitFee
        {
            get
            {
                return @"PKG_IPB.prc_ipb_SplitFeeInfo,P_InpatientNO,22,1,{0}, P_BeginTime,6,1,{1},P_EndTime,6,1,{2},P_OperCode,22,1,{3}, Par_ErrCode,13,2,1,Par_ErrText,22,2,1";
            }
        }

        /// <summary>
        /// 存储过程合并费用
        /// </summary>
        public override string ProcedueCombineFee
        {
            get
            {
                return @"PKG_IPB.prc_ipb_CombineFeeInfo,P_InpatientNO,22,1,{0}, P_BeginTime,6,1,{1},P_EndTime,6,1,{2},P_OperCode,22,1,{3}, Par_ErrCode,13,2,1,Par_ErrText,22,2,1";
            }
        }

        /// <summary>
        /// 存储过程处理身份变更的费用
        /// </summary>
        public override string ProcedueChangePactFee
        {
            get {
                return @"PKG_IPB.prc_ipb_ChangePact,Par_inpateintNo,22,1,{0}, Par_OldPayKind,22,1,{1},Par_OldPact,22,1,{2},Par_NewPayKind,22,1,{3},Par_NewPact,22,1,{4},Par_OperCode,22,1,{5}, Par_ErrCode,13,2,1,Par_ErrText,22,2,1";
            }
        }
        #endregion
    }
}
