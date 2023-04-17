using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.Fee.Data.Oracle
{
    [DataBase(FS.FrameWork.Management.Connection.EnumDBType.ORACLE)]
    class SiCompareItem : AbstractSiCompareItem
    {
        #region AbstractCompareItem 抽象类
   
        public override string QueryLocalItems
        {
            get
            {

                return @"select '1' as fee_code, --药品
                               p.drug_code 项目编号,
                               p.trade_name 项目名称,
                               p.spell_code 拼音码,
                               p.wb_code 五笔码,
                               --nvl(p.gb_code,p.custom_code) 自定义码
                               p.custom_code 自定义码
                          from pha_com_baseinfo p
                         group by p.drug_code, p.trade_name, p.spell_code, p.wb_code, p.custom_code --nvl(p.gb_code,p.custom_code)
                        union all
                        select '2' as fee_code, --非药品
                               f.item_code 项目编号,
                               f.item_name 项目名称,
                               f.spell_code 拼音码,
                               f.wb_code 五笔码,
                               --nvl(f.gb_code,f.input_code) 自定义码   
                               f.input_code  自定义码  
                          from fin_com_undruginfo f
                         group by f.item_code, f.item_name, f.spell_code, f.wb_code,f.input_code--nvl(f.gb_code,f.input_code)";

                }
        }

        public override string QueryCenterItems
        {

              get{
                  return @"         select  decode(p.Sys_Class,'X','1','Z','1','C','1','2') AS fee_code,
                                               p.item_code 项目编号,
                                               p.name 项目名称,
                                               fun_get_querycode(p.name,1) 拼音码,
                                               fun_get_querycode(p.name,0) 五笔码,
                                               '' as  自定义码,
                                               decode(p.item_grade,'1','甲类','2','乙类','丙类') 医保等级,
																							 p.rate 自付比例
                                    from FIN_COM_SIITEM p
                                    where p.pact_code = '{0}'
                                    group by P.Sys_Class,p.item_code,p.name,p.item_grade,p.rate";
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
                                    group by p.trade_code,p.trade_name, p.spell_code, p.wb_code,nvl(p.gb_code,p.custom_code)
                                    union all
                                    select '2' as type_code,--非药品
                                               f.item_code,
                                               f.item_name,
                                               f.spell_code,
                                               f.wb_code,
                                               nvl(f.gb_code,f.input_code) input_code
                                    fin_com_undruginfo f
                                    group by f.item_code,f.item_name,f.spell_code,f.wb_code,nvl(f.gb_code,f.input_code)
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
                                               fun_get_querycode(pact_name,1),
                                               fun_get_querycode(pact_name,0),
                                                DECODE(t.center_item_grade,'1','甲类','2','乙类','3','丙类','未知')    
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
                                where paykind_code='02'";
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
                return @"insert into fin_com_compare( pact_code,His_Code,his_name,HIS_USER_CODE, center_code,center_name,  center_sys_class,OPER_CODE,OPER_DATE) values 
(  '{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}',sysdate)";
            }
        }

        public override string Update
        {
            get
            {
                return @" update fin_com_compare set His_Code='{1}',his_name='{2}',HIS_USER_CODE='{3}', center_code='{4}',center_name ='{5}',center_sys_class='{6}' ,OPER_CODE='{7}'
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
