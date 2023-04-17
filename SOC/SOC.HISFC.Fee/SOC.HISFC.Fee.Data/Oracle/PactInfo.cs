using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.Fee.Data.Oracle
{
    [DataBase(FS.FrameWork.Management.Connection.EnumDBType.ORACLE)]
    class PactInfo : AbstractPactInfo
    {
        public override string SelectAll
        {
            get
            {

                return @"
                        select     
                                   pact_code,pact_name ,paykind_code,pub_ratio,pay_ratio,own_ratio,eco_ratio ,arr_ratio,
                                   baby_flag,mcard_flag,control_flag ,flag,day_limit,month_limit, year_limit, once_limit,
                                   PRICE_FORM ,BED_LIMIT,AIR_LIMIT ,SORT_ID  ,SIMPLE_NAME,DLL_NAME, 
                                   DLL_DESCRIPTION,  spell_code,wb_code,pactsystype,valid_state,spell_code,wb_code
                        from  fin_com_pactunitinfo
";
            }
        }

        public override string Insert
        {
            get {
                return @"insert into fin_com_pactunitinfo
              (pact_code,
               pact_name,
               paykind_code,
               PRICE_FORM,
               pub_ratio,
               pay_ratio,
               own_ratio,
               eco_ratio,
               arr_ratio,
               baby_flag,
               mcard_flag,
               control_flag,
               flag,
               day_limit,
               month_limit,
               year_limit,
               once_limit,
               BED_LIMIT,
               AIR_LIMIT,
               SORT_ID,
               OPER_CODE,
               OPER_DATE,
               SIMPLE_NAME,
               DLL_NAME,
               DLL_DESCRIPTION,
               pactsystype,
                valid_state,
                spell_code,
                wb_code)
            values
              ('{0}',
               '{1}',
               '{2}',
               '{3}',
               {4},
               {5},
               {6},
               {7},
               {8},
               '{9}',
               '{10}',
               '{11}',
               '{12}',
               {13},
               {14},
               {15},
               {16},
               {17},
               {18},
               {19},
               '{20}',
               sysdate,
               '{21}',
               '{22}',
               '{23}',
               '{24}',
                '{25}',
                '{26}',
                '{27}')";
            
            }
        }

        public override string Update
        {
            get {

                return @"update fin_com_pactunitinfo b
   set paykind_code    = '{1}',
       pub_ratio       = {2},
       pay_ratio       = {3},
       own_ratio       = {4},
       eco_ratio       = {5},
       arr_ratio       = {6},
       baby_flag       = '{7}',
       mcard_flag      = '{8}',
       control_flag    = '{9}',
       flag            = '{10}',
       day_limit       = {11},
       month_limit     = {12},
       year_limit      = {13},
       once_limit      = {14},
       PRICE_FORM      = '{15}',
       BED_LIMIT       = {16},
       AIR_LIMIT       = {17},
       SORT_ID         = {18},
       oper_code       = '{19}',
       SIMPLE_NAME     = '{20}',
       DLL_NAME        = '{21}',
       DLL_DESCRIPTION = '{22}',
       pactsystype     = '{23}',
       valid_state='{24}',
       spell_code='{25}',
       wb_code='{26}',
       pact_name = '{27}'
 where pact_code = '{0}'";
            }
        }

        public override string AutoID
        {
            get {
                return @"select to_number(max(lpad(pact_code,10,'0')))+1 from fin_com_pactunitinfo";
            }
        }

        public override string OrderBySortID
        {
            get {
                return @"
                                  order by SORT_ID ,lpad(pact_code,2,'0'),pact_name
                                --order by pactsystype,pact_name

                        ";
            }
        }

        public override string WhereBySystemType
        {
            get
            {
                return @" 
                                where pactsystype in({0})";
            }
        }
    }
}
