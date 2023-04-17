using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.InpatientFee.Data.Oracle
{
    [DataBase(FS.FrameWork.Management.Connection.EnumDBType.ORACLE)]
    class ItemList : AbstractItemList
    {
        #region Select

        public override string SelectAllNewFee
        {
            get
            {

                return @"
                    select 
                                ext_code,
                                recipe_no,
                                sequence_no,
                                trans_type,
                                inpatient_no,
                                unit_price,
                                1 pack_qty,
                                qty,
                                tot_cost,
                                own_cost,
                                pay_cost,
                                pub_cost,
                                eco_cost,
                                send_flag
                        from fin_ipb_itemlist
                        where inpatient_no='{0}'
                        and balance_state='0'
                        and ext_flag2 like '%N'
                ";
            }
        }

        public override string SelectAll
        {
            get { throw new NotImplementedException(); }
        }

        public override string SelectNoSplitFeeItemListCount
        {
            get {
                return @"select count(1) from fin_ipb_itemlist t 
                                    where  t.inpatient_no='{0}' 
                                    and charge_date >= to_date('{1}','yyyy-mm-dd hh24:mi:ss')
                                    and charge_date <= to_date('{2}','yyyy-mm-dd hh24:mi:ss')
                                    and t.noback_num>0
                                    and t.balance_state='0'
                                    and ((t.unit_price > t.org_unit_price 
                                    and t.org_unit_price>0) or (t.split_flag='1'  and t.paykind_code='01'))";

            }
        }

        #endregion

        #region Insert

        public override string Insert
        {
            get
            {
                return string.Empty;
            }
        }

        public override string InsertOldFee
        {
            get
            {
                return @"insert into fin_ipb_itemlist
                                --插入负记录
                                select  'F'||lpad(seq_fin_undrugrecipe.nextval,13,'0') recipe_no,
                                         t.sequence_no,
                                         '2'/* t.trans_type*/,      t.inpatient_no, t.name,
                                         t.paykind_code,    t.pact_code,
                                         t.update_sequenceno,            t.inhos_deptcode,
                                         t.nurse_cell_code,              t.recipe_deptcode,
                                         t.execute_deptcode,         t.stock_deptcode,
                                         t.recipe_doccode,         t.item_code,
                                         t.fee_code,         t.center_code,
                                         t.item_name,         t.unit_price,
                                         -t.noback_num,/* 数量=可退数量等于退费数量 t.qty*/        
                                         t.current_unit,
                                         t.package_code,         t.package_name,
                                        - (t.unit_price*t.noback_num)/1,/* 总金额=用可退数量乘以价格除以包装数量*/        
                                        - (t.unit_price*t.noback_num)/1,/* 自费金额=用可退数量乘以价格除以包装数量*/    
                                        - t.pay_cost,        
                                        -t.pub_cost,
                                        - t.eco_cost,
                                         t.sendmat_sequence,         t.send_flag,
                                         t.baby_flag,         t.jzqj_flag,
                                         t.brought_flag,         t.ext_flag,
                                         t.invoice_no,         t.balance_no,
                                         t.balance_state,         
                                         0,/*可退数量=0*/
                                         t.recipe_no ext_code,
                                         t.ext_opercode,
                                         t.ext_date,         t.apprno,
                                         t.charge_opercode,         t.charge_date,
                                         t.confirm_num,         t.machine_no,
                                         t.exec_opercode,         t.exec_date,
                                         t.send_opercode,         
                                         '{1}',/*t.fee_opercode=传入的操作员*/
                                         sysdate,/*t.fee_date=当前时间*/
                                         t.send_date,
                                         t.check_opercode,         t.check_no,
                                         t.mo_order,         t.mo_exec_sqn,
                                         t.fee_rate,         t.feeoper_deptcode,
                                         t.upload_flag,         t.ext_flag1,
                                         substr(lpad(t.ext_flag2,3,'0'),1,2)||'N', /* t费用来源第三位=N ,用于区别是新插入的费用*/
                                          t.ext_flag3,
                                         t.item_flag,         t.medicalteam_code,
                                         t.operationno,         t.transaction_sequence_number,
                                         t.si_transaction_datetime,         t.his_recipe_no,
                                         t.si_recipe_no,         t.his_cancel_recipe_no,
                                         t.si_cancel_recipe_no,        
                                         t.ext_flag4,
                                         t.ext_flag5,         t.balance_opercode,
                                         t.balance_date
                                from fin_ipb_itemlist t
                                where t.inpatient_no='{0}'
                                and t.balance_state='0'
                                and t.noback_num>0
                                ";
            }
        }

        public override string InsertNewFee
        {
            get
            {
                return @"insert into fin_ipb_itemlist
                                  --插入正记录
                                select  'F'||lpad(seq_fin_undrugrecipe.nextval,13,'0')  recipe_no,
                                         t.sequence_no,
                                         '1'/* t.trans_type*/,      t.inpatient_no, t.name,
                                         '{2}',/*结算类别*/
                                         '{3}',/*合同单位*/
                                         t.update_sequenceno,            t.inhos_deptcode,
                                         t.nurse_cell_code,              t.recipe_deptcode,
                                         t.execute_deptcode,         t.stock_deptcode,
                                         t.recipe_doccode,         t.item_code,
                                         t.fee_code,         t.center_code,
                                         t.item_name,         t.unit_price,
                                         t.noback_num,/* 数量=可退数量等于退费数量 t.qty*/        
                                         t.current_unit,
                                         t.package_code,         t.package_name,
                                         (t.unit_price*t.noback_num)/1,/* 总金额=用可退数量乘以价格除以包装数量*/        
                                         (t.unit_price*t.noback_num)/1,/* 自费金额=用可退数量乘以价格除以包装数量*/    
                                         t.pay_cost,        
                                         t.pub_cost,
                                         t.eco_cost,
                                         t.sendmat_sequence,         t.send_flag,
                                         t.baby_flag,         t.jzqj_flag,
                                         t.brought_flag,         t.ext_flag,
                                         t.invoice_no,         t.balance_no,
                                         t.balance_state,         
                                         t.noback_num,
                                         t.recipe_no ext_code,         
                                         t.ext_opercode,
                                         t.ext_date,         t.apprno,
                                         t.charge_opercode,         t.charge_date,
                                         t.confirm_num,         t.machine_no,
                                         t.exec_opercode,         t.exec_date,
                                         t.send_opercode,         
                                         '{1}',/*t.fee_opercode=传入的操作员*/
                                         sysdate,/*t.fee_date=当前时间*/
                                         t.send_date,
                                         t.check_opercode,         t.check_no,
                                         t.mo_order,         t.mo_exec_sqn,
                                         t.fee_rate,         t.feeoper_deptcode,
                                         t.upload_flag,         t.ext_flag1,
                                         substr(lpad(t.ext_flag2,3,'0'),1,2)||'N', /* t费用来源第三位=1*/
                                          t.ext_flag3,
                                         t.item_flag,         t.medicalteam_code,
                                         t.operationno,         t.transaction_sequence_number,
                                         t.si_transaction_datetime,         t.his_recipe_no,
                                         t.si_recipe_no,         t.his_cancel_recipe_no,
                                         t.si_cancel_recipe_no,            
                                         t.ext_flag4,
                                         t.ext_flag5,    
                                         t.balance_opercode,
                                         t.balance_date
                                from fin_ipb_itemlist t
                                where  t.inpatient_no='{0}'
                                and t.balance_state='0'
                                and t.noback_num>0";
            }
        }

        #endregion

        #region Update

        public override string UpdateNoBackNumByInPatientNO
        {
            get
            {
                return 
                @"
                 UPDATE fin_ipb_itemlist   --住院非药品明细表
                   SET noback_num= noback_num - qty  --可退数量
                 WHERE   inpatient_no='{0}'
                   AND noback_num>=abs(qty)
                   and pact_code='{1}'
                   AND BALANCE_STATE = '0'
                ";
            }
        }

        public override string UpdateFTSource
        {
            get
            {
                return @"
                                update fin_ipb_itemlist
                                set ext_flag2=substr(lpad(ext_flag2,3,'0'),1,2)||'1'
                                where inpatient_no='{0}'
                                and balance_state='0'
                                and ext_flag2 like '%N'
                                ";
            }
        }

        public override string UpdateFTCost
        {
            get
            {
                return @" update fin_ipb_itemlist set tot_cost={4},own_cost={5},pay_cost={6},pub_cost={7},eco_cost={8} where ext_code='{0}' and recipe_no='{1}' and sequence_no={2} and trans_type='{3}'";
            }
        }

        #endregion
    }
}
