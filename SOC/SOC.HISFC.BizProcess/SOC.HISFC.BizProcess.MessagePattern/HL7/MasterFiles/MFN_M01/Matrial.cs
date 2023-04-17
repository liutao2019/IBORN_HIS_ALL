using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHapi.Model.V24.Datatype;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.HL7.MasterFiles.MFN_M01
{
    public class Matrial
    {
        public int Receive(NHapi.Model.V24.Message.MFN_M01 MFN, ref string errInfo)
        {
            /* 1	inv_code	物资编码	ST	R	20
                2	comp_code	单位编码	ST	R	20
                3	copy_code	帐套	ST	R	3
                4	inv_name	物资名称	ST	O	100
                5	spell	拼音码	ST	O	100
                6	inv_model	规格型号	ST	O	50
                7	unit_code	计量单位编码	ST	O	10
                8	unit_code_f	辅助计量单位编码	ST	R	10
                9	unit_rate	换算率	ST	O	9
                10	mate_type_code	物资类别代码	ST	R	20
                11	factory_code	生产厂商	ST	O	12
                12	inv_attr_code	物资属性	ST	O	18
                13	batch_no	批号	ST	O	20
                14	cur_stock	现有库存	ST	O	18
                15	price	单价	ST	O	16
                16	price_rate	加价率	ST	O	9
                17	refe_cost	参考成本	ST	O	16
                18	plan_price	计划价	ST	O	16
                19	cus_code	主要供货单位	ST	O	20
                20	buy_ahead	采购提前期	ST	O	20
                21	eco_bat	经济批量	ST	O	20
                22	abc	ABC分类	ST	O	1
                23	stock_secu	安全库存	ST	O	16
                24	low_limit	最低库存	ST	O	16
                25	high_limit	最高库存	ST	O	16
                26	stay_time	呆滞标准	ST	O	9
                27	per_weight	单位重量	ST	O	16
                28	per_volum	单位体积	ST	O	16
                29	sdate	启用日期	TS	O	26
                30	edate	停用日期	TS	O	26
                31	is_batch	是否批次管理	ST	R	1
                32	is_quality	是否保质期管理	ST	R	1
                33	is_dura	是否耐用品管理	ST	R	1
                34	is_overstock	是否呆滞积压	ST	R	1
                35	is_sec_whg	是否做二级库管	ST	R	1
                36	is_shel_make	是否自制品	ST	R	1
                37	is_add_sale	是否为加价销售	ST	R	1
                38	is_pm_high	采购计划制定依据	ST	R	1
                39	cert_code	证件号码	ST	O	40
                40	is_cert	是否证件管理	ST	O	1
                41	brand_name	品牌	ST	O	60
                42	agent_name	代理商	ST	O	60
                43	alias	别名	ST	O	100
                44	inv_ID	网上招标编号	ST	O	10
                45	ref_price	参考单价	ST	O	16
                46	is_bar	是否条形码管理	ST	O	1
                47	bar_code_new 	品种条码	ST	O	50
                48	cus_name	供货单位名称	ST	O	50
                49	is_charge	是否收费	ST	O	1
                50	is_tender	是否招标	ST	O	1
                51	is_com	是否专购	ST	O	1
                52	charge_type	医嘱类型	ST	O	40
                53	doc_kind	医嘱类别	ST	O	40
                54	amortize_type	摊销方式	ST	O	1
                55	oper_code	操作人员编码	ST	O	20
                56  cert_name   注册名称    ST	O	20
                57  factory_code  产地品牌   ST	O	20
             *  58  cert_code  注册证号    ST	O	20
             *  59  unit_name  单位         ST	O	20
             *  60 charge_price  收费价格   ST	O	20
             *  
                
             * 
             * 
             * 
             *  
            */

            NHapi.Model.V24.Segment.ZE0 ZE0 = MFN.GetMF().GetStructure<NHapi.Model.V24.Segment.ZE0>();
            
            //MFE
            NHapi.Model.V24.Group.MFN_M01_MF MF = MFN.GetMF(0);

            string sql = @"insert into mat_com_baseinfo 
                                    (ITEM_CODE, KIND_CODE, STORAGE_CODE, ITEM_NAME, SPELL_CODE, WB_CODE, CUSTOM_CODE, GB_CODE, OTHER_NAME, OTHER_SPELL, OTHER_WB, OTHER_CUSTOM, EFFECT_AREA, EFFECT_DEPT, SPECS, MIN_UNIT,  IN_PRICE, SALE_PRICE, PACK_UNIT, PACK_QTY, PACK_PRICE, ADD_RATE, FEE_CODE, FINANCE_FLAG,  VALID_FLAG, SPECIAL_FLAG, HIGHVALUE_FLAG, FACTORY_CODE, COMPANY_CODE, IN_SOURCE, USAGE,  REGISTER_CODE, SPECIAL_TYPE, REGISTER_DATE, OVER_DATE, PACK_FLAG, EXAMINE_FLAG, NORECYCLE_FLAG, MEMO, OPER_CODE, OPER_DATE, BATCH_FLAG, PLAN,APPROVE_INFO,MADER,REGISTER_NO)
                                    values ('{0}', '{1}', '0000', '{2}', '{3}', '', '{0}', '', '{2}', 
                                    '{3}', '', '{0}', '0', '', '{4}', '{5}', {8}, {8}, '{6}', 1, {8}, 
                                    '{7}', '', '0', '1', '0', '0', '', '', '', '', '', '', null, null, '0', '0', '0', 
                                    '', '000000',sysdate, '0', '0','{9}','{10}','{11}')";
            string deleteSql = "delete from mat_com_baseinfo where item_code='{0}'";

            deleteSql = string.Format(deleteSql, ZE0.Get<ST>(1).Value);

            sql = string.Format(sql, ZE0.Get<ST>(1).Value, ZE0.Get<ST>(10).Value, ZE0.Get<ST>(4).Value, ZE0.Get<ST>(5).Value, ZE0.Get<ST>(6).Value, ZE0.Get<ST>(55).Value, string.IsNullOrEmpty(ZE0.Get<ST>(55).Value) ? "NULL" : ZE0.Get<ST>(55).Value, ZE0.Get<ST>(9).Value, ZE0.Get<ST>(56).Value,ZE0.Get<ST>(53).Value,ZE0.Get<ST>(38).Value,ZE0.Get<ST>(54).Value);

            FS.FrameWork.Management.Database frequencyMgr = new FS.FrameWork.Management.DataBaseManger();
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            frequencyMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            int param = frequencyMgr.ExecNoQuery(deleteSql);
            if (param <0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                errInfo = frequencyMgr.Err;
                return -1;
            }

            param = frequencyMgr.ExecNoQuery(sql);
            if (param <= 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                errInfo = frequencyMgr.Err;
                return -1;
            }

            FS.FrameWork.Management.PublicTrans.Commit();
            return param;
        }
    }
}
