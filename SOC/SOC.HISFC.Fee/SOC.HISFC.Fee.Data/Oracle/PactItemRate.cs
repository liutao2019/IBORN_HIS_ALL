using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.Fee.Data.Oracle
{
    [DataBase(FS.FrameWork.Management.Connection.EnumDBType.ORACLE)]
    class PactItemRate : AbstractPactItemRate
    {
        public override string SelectItemGroup
        {
            get
            {

                return @"select  t.type_code,
                                               t.fee_code,
                                               r.name,
                                               r.spell_code,
                                               r.wb_code,
                                               r.input_code
                                    from fin_com_pactunitfeecoderate t,com_dictionary r
                                    where t.fee_code=r.code
                                    and r.type='MINFEE'
                                    and t.type_code='0'
                                    group by t.type_code,t.fee_code,r.name,r.spell_code,r.wb_code,r.input_code
                                    union all
                                    select  t.type_code,
                                               t.fee_code,
                                               p.trade_name,
                                               p.spell_code,
                                               p.wb_code,
                                               nvl(p.gb_code,p.custom_code) input_code
                                    from fin_com_pactunitfeecoderate t,pha_com_baseinfo p
                                    where t.fee_code=p.drug_code
                                    and t.type_code='1'
                                    group by t.type_code,t.fee_code,p.trade_name, p.spell_code, p.wb_code,nvl(p.gb_code,p.custom_code)
                                    union all
                                    select  t.type_code,
                                               t.fee_code,
                                               f.item_name,
                                               f.spell_code,
                                               f.wb_code,
                                               nvl(f.gb_code,f.input_code) input_code
                                    from fin_com_pactunitfeecoderate t,fin_com_undruginfo f
                                    where t.fee_code=f.item_code
                                    and t.type_code='2'
                                    group by t.type_code,t.fee_code,f.item_name,f.spell_code,f.wb_code,nvl(f.gb_code,f.input_code)";

            }
        }

        public override string SelectItemGroupByPact
        {
            get
            {
                return @"select  t.type_code,
                                               t.fee_code,
                                               r.name,
                                               r.spell_code,
                                               r.wb_code,
                                               r.input_code
                                    from fin_com_pactunitfeecoderate t,com_dictionary r
                                    where t.fee_code=r.code
                                    and r.type='MINFEE'
                                    and t.type_code='0'
                                    and t.pact_code in  ({0})
                                    group by t.type_code,t.fee_code,r.name,r.spell_code,r.wb_code,r.input_code
                                    union all
                                    select  t.type_code,
                                               t.fee_code,
                                               p.trade_name,
                                               p.spell_code,
                                               p.wb_code,
                                               nvl(p.gb_code,p.custom_code) input_code
                                    from fin_com_pactunitfeecoderate t,pha_com_baseinfo p
                                    where t.fee_code=p.drug_code
                                    and t.type_code='1'
                                    and t.pact_code in  ({0})
                                    group by t.type_code,t.fee_code,p.trade_name, p.spell_code, p.wb_code,nvl(p.gb_code,p.custom_code)
                                    union all
                                    select  t.type_code,
                                               t.fee_code,
                                               f.item_name,
                                               f.spell_code,
                                               f.wb_code,
                                               nvl(f.gb_code,f.input_code) input_code
                                    from fin_com_pactunitfeecoderate t,fin_com_undruginfo f
                                    where t.fee_code=f.item_code
                                    and t.type_code='2'
                                    and t.pact_code in  ({0})
                                    group by t.type_code,t.fee_code,f.item_name,f.spell_code,f.wb_code,nvl(f.gb_code,f.input_code)";
            }
        }

        public override string SelectByItemTypeAndCode
        {
            get
            {
                return @" select 
                                 a.pact_code, 
                                 b.pact_name,
                                 a.type_code,
                                 a.pub_ratio,
                                 a.own_ratio,
                                 a.pay_ratio,
                                 a.eco_ratio,
                                 a.arr_ratio,
                                 a.fee_code ,
                                 a.COST_LIMIT,
                                 fun_get_querycode(b.pact_name,1) SPELL_CODE,
                                 fun_get_querycode(b.pact_name,0) WB_CODE
                                 from fin_com_pactunitfeecoderate a ,fin_com_pactunitinfo b
                                 where a.type_code = '{0}'  and a.fee_code ='{1}'
                                 and a.pact_code=b.pact_code
                                and a.pact_code in  ({2})";
            }
        }

        public override string Delete
        {
            get
            {
                return @"
                 delete from fin_com_pactunitfeecoderate where pact_code ='{0}' and FEE_CODE='{1}'  
                ";
            }
        }

        public override string Insert
        {
            get
            {
                return @"insert into fin_com_pactunitfeecoderate( pact_code,type_code,fee_code, pub_ratio,own_ratio,  pay_ratio,eco_ratio ,arr_ratio,cost_limit,OPER_CODE,OPER_DATE) values 
(  '{0}','{1}','{2}',{3},{4},{5},{6},{7},{8},'{9}',sysdate)";
            }
        }

        public override string Update
        {
            get
            {
                return @" update fin_com_pactunitfeecoderate set type_code='{1}',fee_code='{2}',pub_ratio={3},own_ratio={4}, pay_ratio={5},eco_ratio ={6},arr_ratio={7} ,cost_limit={8},oper_code='{9}'
 where  pact_code='{0}' and  fee_code='{2}'  ";
            }
        }

        public override string SelectAll
        {
            get { 
                return string.Empty; }
        }
    }
}
