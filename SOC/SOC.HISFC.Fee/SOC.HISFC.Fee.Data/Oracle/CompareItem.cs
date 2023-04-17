using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.Fee.Data.Oracle
{
    [DataBase(FS.FrameWork.Management.Connection.EnumDBType.ORACLE)]
    class CompareItem : AbstractCompareItem
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
                                               p.wb_code,
                                               nvl(p.gb_code,p.custom_code) input_code
                                    from pha_com_baseinfo p
                                    group by p.drug_code,p.trade_name, p.spell_code, p.wb_code,nvl(p.gb_code,p.custom_code)
                                    union all
                                    select '2' as type_code,--非药品
                                               f.item_code,
                                               f.item_name,
                                               f.spell_code,
                                               f.wb_code,
                                               nvl(f.gb_code,f.input_code) input_code
                                    from fin_com_undruginfo f
                                    group by f.item_code,f.item_name,f.spell_code,f.wb_code,nvl(f.gb_code,f.input_code)";

                }
        }

        public override string QueryCenterItems
        {

              get{
                  return @"         select distinct decode(p.Sys_Class,'X','1','Z','1','C','1','2') AS type_code,
                                               p.item_code,
                                               p.name,
                                               fun_get_querycode(p.name,1),
                                               fun_get_querycode(p.name,0),
                                               '' as  input_code,
                                               p.Sys_Class
                                    from FIN_COM_SIITEM p
                                    where p.pact_code in ({0})
                                    group by P.Sys_Class,p.item_code,p.name";
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
                                               DECODE(t.center_item_grade,'1','甲类','2','乙类','3','丙类','未知'),
                                               t.center_memo,
                                               t.center_rate  
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
                return @"insert into fin_com_compare( pact_code,His_Code,his_name,HIS_USER_CODE, center_code,center_name,  center_sys_class,OPER_CODE,OPER_DATE,Center_Item_Grade,center_memo,center_rate) values 
(  '{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}',sysdate,'{8}','{9}','{10}')";
            }
        }

        public override string Update
        {
            get
            {
                return @" update fin_com_compare set His_Code='{1}',his_name='{2}',HIS_USER_CODE='{3}', center_code='{4}',center_name ='{5}',center_sys_class='{6}' ,OPER_CODE='{7}',Center_Item_Grade = '{8}',center_memo = '{9}',
       center_rate = '{10}' where  pact_code='{0}' and  His_Code='{1}'  ";
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
