using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.Fee.Data.Oracle
{
    [DataBase(FS.FrameWork.Management.Connection.EnumDBType.ORACLE)]
    class Undrug : AbstractUndrug
    {
        public override string SelectValidBriefByUnitFlg
        {
            get
            {
                return @"SELECT item_code,  --编码
    				item_name,  --名称
    				sys_class, --系统类别
    				fee_code,  --最小费用代码 
    				input_code,  -- 输入码
    				spell_code,  --拼音码
    				wb_code,  -- 五笔码
    				decode(unitflag,'1',fun_get_packageprice(item_code), unit_price) unit_price,  --三甲价
    				decode(unitflag,'1',fun_get_packageprice1(item_code), UNIT_PRICE1) UNIT_PRICE1, --儿童价
    				decode(unitflag,'1',fun_get_packageprice2(item_code), UNIT_PRICE2) UNIT_PRICE2, --特诊价
    				valid_state,   --有效性标识 0 在用 1 停用 2 废弃    
    				mark ,    --备注
                   GB_CODE  --国家码    
				FROM fin_com_undruginfo
                WHERE   valid_state='1' and unitflag='{0}' ORDER BY INPUT_CODE"
                ;
            }
        }

        /// <summary>
        /// {21a8d31b-c7f1-4ffd-ab81-ced4b7c82c5c}   {c89524f7-3f9e-4a41-a2a6-7cccbf476404}
        /// </summary>
        public override string SelectAll
        {
            get
            {
                return @"SELECT item_code, --编码
                       item_name, --名称
                       sys_class, --系统类别
                       fee_code, --最小费用代码 
                       input_code, -- 输入码
                       spell_code, --拼音码
                       wb_code, -- 五笔码
                       gb_code, -- 国家编码
                       international_code, --国际编码 
                       decode(unitflag, '1', fun_get_packageprice(item_code), unit_price) unit_price, --三甲价
                       stock_unit, --单位
                       emerg_scale, -- 急诊加成比例
                       family_plane, -- 计划生育标记 
                       special_item, --特定诊疗项目
                       item_grade, --甲乙类标志
                       confirm_flag, --确认标志 1 需要确认 0 不需要确认 
                       valid_state, --有效性标识 0 在用 1 停用 2 废弃    
                       specs, --规格
                       exedept_code, --执行科室
                       facility_no, --设备编号 用 | 区分 
                       default_sample, --默认检查部位或标本
                       operate_code, -- 手术编码 
                       operate_kind, -- 手术分类
                       operate_type, --手术规模 
                       collate_flag, --是否有物资项目与之对照(1有，0没有) 
                       mark, --备注     
                       decode(unitflag, '1', fun_get_packageprice1(item_code), UNIT_PRICE1) UNIT_PRICE1, --儿童价
                       decode(unitflag, '1', fun_get_packageprice2(item_code), UNIT_PRICE2) UNIT_PRICE2, --特诊价
                       SPECIAL_FLAG, --省限制
                       SPECIAL_FLAG1, --市限制 
                       SPECIAL_FLAG2, --自费项目
                       SPECIAL_FLAG3, --特殊标识
                       SPECIAL_FLAG4, --特殊标识
                       UNIT_PRICE3, --单价2
                       UNIT_PRICE4, --单价2    
                       DISEASE_CLASS, -- 疾病分类  
                       SPECIAL_DEPT, -- 专科名称  
                       MARK1, --病史及检查
                       MARK2, --检查要求 
                       MARK3, --  注意事项      
                       CONSENT_FLAG,
                       MARK4, --  检查申请单名称  
                       needbespeak, -- 是否需要预约 
                       ITEM_AREA,
                       ITEM_NOAREA,
                       UNITFLAG, -- 单位标识(0,明细; 1,组套) 
                       APPLICABILITYAREA, --有效范围
                       DEPT_LIST, --允许开立科室
                       ITEM_PRICETYPE, --物价费用类别
                       ORDERPRINT_TAG, --医嘱打印标示
                       oper_code, --申请人
                       oper_date, --停用时间
                       OTHER_NAME,
                       other_spell,
                       other_wb,
                       other_custom,
                       english_name,
                       english_other,
                       english_regular,
                       register_code,
                       register_date,
                       producer_info,
                       MTTYPECODE,
                       MTTYPENAME,
                       EXEC_DEPT_OUT,--默认执行科室（门诊）
                       exec_dept_in, --默认执行科室（住院）
                       manage_class, --管理级别
                       ITEM_PRICETYPE2, --物价费用类别二
                       ISDISCOUNT,  --折扣表示
                       YBCODE
                  FROM fin_com_undruginfo";

            }
        }

        public override string WhereValidByItemCode
        {
            get
            {
                return @"	
        WHERE   (item_code ='{0}'  Or 'All'='{0}' ) AND (valid_state ='{1}' Or 'All'='{1}')
        ORDER BY Item_Code,INPUT_CODE";
            }
        }

        public override string WhereValidByUnitFlag
        {
            get
            {
                return @"
                    WHERE   valid_state='1' AND unitflag='1'
                    ORDER BY  GB_CODE
                ";
            }
        }

        public override string WhereExistsByUserCode
        {
            get
            {
                return @"
                    WHERE   INPUT_CODE='{0}'
                    ORDER BY  GB_CODE
                ";
            }
        }

        public override string Insert
        {
            get
            {

                return @"INSERT INTO FIN_COM_UNDRUGINFO
                              (item_code,
                               item_name,
                               sys_class,
                               fee_code,
                               input_code,
                               spell_code,
                               wb_code,
                               gb_code,
                               international_code,
                               unit_price,
                               stock_unit,
                               emerg_scale,
                               family_plane,
                               special_item,
                               item_grade,
                               confirm_flag,
                               valid_state,
                               specs,
                               exedept_code,
                               facility_no,
                               default_sample,
                               operate_code,
                               operate_kind,
                               operate_type,
                               collate_flag,
                               mark,
                               oper_code,
                               oper_date,
                               UNIT_PRICE1,
                               UNIT_PRICE2,
                               SPECIAL_FLAG,
                               SPECIAL_FLAG1,
                               SPECIAL_FLAG2,
                               SPECIAL_FLAG3,
                               SPECIAL_FLAG4,
                               UNIT_PRICE3,
                               UNIT_PRICE4,
                               DISEASE_CLASS,
                               SPECIAL_DEPT,
                               CONSENT_FLAG,
                               MARK1,
                               MARK2,
                               MARK3,
                               MARK4,
                               NEEDBESPEAK,
                               ITEM_AREA,
                               ITEM_NOAREA,
                               unitflag,
                               APPLICABILITYAREA,
                               DEPT_LIST,
                               ITEM_PRICETYPE,
                               ORDERPRINT_TAG,
                               other_name,
                               other_spell,
                               other_wb,
                               other_custom,
                               english_name,
                               english_other,
                               english_regular,
                               register_code,
                               register_date,
                               producer_info,
                               MTTYPECODE,
                               MTTYPENAME,
                               EXEC_DEPT_OUT,
                               EXEC_DEPT_IN,
                               MANAGE_CLASS,
                               ITEM_PRICETYPE2,
                               ISDISCOUNT,
                               YBCODE
                               )
                            VALUES
                              ('{0}',
                               '{1}',
                               '{2}',
                               '{3}',
                               '{4}',
                               '{5}',
                               '{6}',
                               '{7}',
                               '{8}',
                               '{9}',
                               '{10}',
                               '{11}',
                               '{12}',
                               '{13}',
                               '{14}',
                               '{15}',
                               '{16}',
                               '{17}',
                               '{18}',
                               '{19}',
                               '{20}',
                               '{21}',
                               '{22}',
                               '{23}',
                               '{24}',
                               '{25}',
                               '{26}',
                               sysdate,
                               {27},
                               {28},
                               '{29}',
                               '{30}',
                               '{31}',
                               '{32}',
                               '{33}',
                               {34},
                               {35},
                               '{36}',
                               '{37}',
                               '{38}',
                               '{39}',
                               '{40}',
                               '{41}',
                               '{42}',
                               '{43}',
                               '{44}',
                               '{45}',
                               '{46}',
                               '{47}',
                               '{48}',
                               '{49}',
                               '{50}',
                               '{51}',
                               '{52}',
                               '{53}',
                               '{54}',
                               '{55}',
                               '{56}',
                               '{57}',
                               '{58}',
                               to_date('{59}', 'yyyy-mm-dd hh24:mi:ss'),
                               '{60}',
                               '{61}',
                               '{62}',
                               '{63}',
                               '{64}',
                               '{65}',
                               '{66}',
                               '{67}',
                               '{68}'
                               )";
            }
        }

        public override string Invalid
        {
            get
            {
                return @" update fin_com_undruginfo t set t.valid_state ='2' where t.item_code ='{0}'";

            }

        }

        //{6F68AB52-332C-4efa-A6DD-F6BDB37B1283}
        public override string Update
        {
            get
            {
                return @"UPDATE FIN_COM_UNDRUGINFO
                           SET item_name          = '{1}',
                               sys_class          = '{2}',
                               fee_code           = '{3}',
                               input_code         = '{4}',
                               spell_code         = '{5}',
                               wb_code            = '{6}',
                               gb_code            = '{7}',
                               international_code = '{8}',
                               unit_price         = '{9}',
                               stock_unit         = '{10}',
                               emerg_scale        = '{11}',
                               family_plane       = '{12}',
                               special_item       = '{13}',
                               item_grade         = '{14}',
                               confirm_flag       = '{15}',
                               valid_state        = '{16}',
                               specs              = '{17}',
                               exedept_code       = '{18}',
                               facility_no        = '{19}',
                               default_sample     = '{20}',
                               operate_code       = '{21}',
                               operate_kind       = '{22}',
                               operate_type       = '{23}',
                               collate_flag       = '{24}',
                               mark               = '{25}',
                               oper_code          = '{26}',
                               oper_date          = sysdate,
                               UNIT_PRICE1        = {27},
                               UNIT_PRICE2        = {28},
                               SPECIAL_FLAG       = '{29}',
                               SPECIAL_FLAG1      = '{30}',
                               SPECIAL_FLAG2      = '{31}',
                               SPECIAL_FLAG3      = '{32}',
                               SPECIAL_FLAG4      = '{33}',
                               UNIT_PRICE3        = {34},
                               UNIT_PRICE4        = {35},
                               DISEASE_CLASS      = '{36}',
                               SPECIAL_DEPT       = '{37}',
                               CONSENT_FLAG       = '{38}',
                               MARK1              = '{39}',
                               MARK2              = '{40}',
                               MARK3              = '{41}',
                               MARK4              = '{42}', --  检查申请单名称  
                               needbespeak        = '{43}', -- 是否需要预约
                               ITEM_AREA          = '{44}', -- 项目范围
                               ITEM_NOAREA        = '{45}', --项目例外 
                               unitflag           = '{46}', --单位标识(0,明细; 1,组套) 
                               APPLICABILITYAREA  = '{47}',
                               DEPT_LIST          = '{48}',
                               ITEM_PRICETYPE     = '{49}',
                               ORDERPRINT_TAG     = '{50}',
                               other_name         = '{51}',
                               other_spell        = '{52}',
                               other_wb           = '{53}',
                               other_custom       = '{54}',
                               english_name       = '{55}',
                               english_other      = '{56}',
                               english_regular    = '{57}',
                               register_code      = '{58}',
                               register_date      = to_date('{59}', 'yyyy-mm-dd hh24:mi:ss'),
                               producer_info      = '{60}',
                               MTTYPECODE         = '{61}',
                               MTTYPENAME         = '{62}',
                               EXEC_DEPT_OUT      = '{63}',--门诊默认执行科室
                               EXEC_DEPT_IN       = '{64}',   --住院默认执行科室
                               MANAGE_CLASS       = '{65}', --管理级别
                               ITEM_PRICETYPE2    ='{66}',
                               ISDISCOUNT         ='{67}',
                               YBCODE             ='{68}'
                         WHERE item_code = '{0}'
                         ";
            }
        }
    }
}
