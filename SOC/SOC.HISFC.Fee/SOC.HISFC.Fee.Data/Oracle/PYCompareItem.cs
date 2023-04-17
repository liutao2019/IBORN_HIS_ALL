using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.Fee.Data.Oracle
{
    [DataBase(FS.FrameWork.Management.Connection.EnumDBType.ORACLE)]
    class PYCompareItem : AbstractPYCompareItem
    {
        #region AbstractCompareItem 抽象类
   
        public override string QueryLocalItems
        {
            get
            {

                return @"select  '1' as type_code,--药品
                                               p.drug_code,
                                               p.trade_name,
                                               p.spell_code,
                                               p.gb_code,
                                               p.custom_code input_code,
                         NVL((select 1 from fin_com_compare  where his_code=P.drug_code and pact_code='611' ),0) flag,
                                               p.SPECS
                                    from pha_com_baseinfo p
                                    union all
                                    select '2' as type_code,--非药品
                                               f.item_code,
                                               f.item_name,
                                               f.spell_code,
                                               f.gb_code,
                                               f.input_code input_code,
                         NVL((select 1 from fin_com_compare  where his_code=F.item_code and pact_code='611' ),0) flag,
                                               '' as SPECS
                                    from fin_com_undruginfo f
                                    where f.UNITFLAG='0'
                                    and f.VALID_STATE='1'";

                }
        }

        public override string QueryCenterItems
        {

              get{
                  return @"        select  decode(p.item_flag,'2','1','3','1','4','1','5','1','2') AS fee_code,
                                               p.item_code 项目编号,
                                               p.item_name 项目名称,
                                               p.SPELL_CODE 拼音码,
                                               p.WB_CODE 五笔码,
                                               '' as 自定义码,
                                               p.item_flag,
											   decode(p.item_rade,0,'1',1,'3',2) 医保等级,
											   p.item_rade 自付比例
                                               
                                    from fin_xnh_siitem p
                                   -- where p.pact_code in ({0})
                                    --group by P.item_flag,p.item_code,p.item_name";
                }

         }
         
        public override string QueryItemsByType
        {
            get
            {
                return @"select * from (


select  '1' as type_code,--药品
                                               p,trade_code,
                                               p.trade_name,
                                               p.spell_code,
                                               p.wb_code,
                                               nvl(p.gb_code,p.custom_code) input_code
                                    pha_com_baseinfo p
                                    --group by p.trade_code,p.trade_name, p.spell_code, p.wb_code,nvl(p.gb_code,p.custom_code)
                                    union all
                                    select '2' as type_code,--非药品
                                               f.item_code,
                                               f.item_name,
                                               f.spell_code,
                                               f.wb_code,
                                               nvl(f.gb_code,f.input_code) input_code
                                    fin_com_undruginfo f
                                    --group by f.item_code,f.item_name,f.spell_code,f.wb_code,nvl(f.gb_code,f.input_code)
) aa where aa.type_code='{0}'";

            }
        }

        public override string QueryCenterItemsByType
        {
            get
            {
                return @"";

            }
        }

        public override string QueryComparedItems
        {
            get
            {
                return @"                                               select  t.pact_code,
                                               p.pact_name as pact_name,
                                               t.his_code,
                                               t.his_name,
                                               t.CENTER_CODE,
                                               t.center_name,
                                               t.his_user_code,
                                               '',--fun_get_querycode(pact_name,1),太慢了，新屏蔽
                                               '' --fun_get_querycode(pact_name,0) 太慢了，新屏蔽
                                              
                                    from fin_com_compare t,fin_com_pactunitinfo p
                                    where t.his_code ='{0}'
                                    and t.pact_code=p.pact_code
                                    and t.pact_code in ({1})
                                    order by t.pact_code";
            }
        }

        public override string  QueryPacts
        {
            get
            {
                return @"  select pact_code,pact_name ,paykind_code ,SIMPLE_NAME,DLL_NAME,DLL_DESCRIPTION, 
                                            fun_get_querycode(pact_name,1),fun_get_querycode(pact_name,0),pactsystype
                                from  fin_com_pactunitinfo
                                where DLL_NAME='{0}'  and paykind_code='02'  order by pact_code";
            }
        }

        public override string QueryCenterTypes
        {
            get
            {
                return @"  select distinct DLL_NAME,DLL_DESCRIPTION, 
                                            fun_get_querycode(DLL_DESCRIPTION,1),fun_get_querycode(DLL_DESCRIPTION,0)
                                from  fin_com_pactunitinfo
                                where paykind_code='02'
                                and dll_name='GZPYSI.dll'";
            }
        }
        #endregion

        #region AbstractSql 抽象类
        public override string Delete
        {
            get
            {
                return @" delete from fin_com_compare where pact_code ='{0}' and His_Code='{1}'  
                ";
            }
        }

        public override string Insert
        {
            get
            {
                return @"insert into fin_com_compare( pact_code,His_Code,his_name,HIS_USER_CODE, center_code,center_name,  center_sys_class,OPER_CODE,OPER_DATE,center_item_grade,center_rate) values 
(  '{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}',sysdate,'{8}','{9}')";
            }
        }

        public override string Update
        {
            get
            {
                return @" update fin_com_compare set His_Code='{1}',his_name='{2}',HIS_USER_CODE='{3}', center_code='{4}',center_name ='{5}',center_sys_class='{6}' ,OPER_CODE='{7}',center_item_grade='{8}',center_rate='{9}'  
 where  pact_code='{0}' and  His_Code='{1}'  ";
            }
        }

        public override string SelectAll
        {
            get { 
                return string.Empty; }
        }
        #endregion
    }
}
