using System;

namespace FS.HISFC.Models.SIInterface
{
	/// <summary>
	/// BlackList 的摘要说明。
	/// </summary>
    public class YB_1301 : FS.FrameWork.Models.NeuObject
	{
		public YB_1301()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

        ///<summary>
        ///医疗目录编码
        ///<summary>
        public string med_list_codg { get; set; }

        ///<summary>
        ///药品商品名
        ///<summary>
        public string drug_prodname { get; set; }

        ///<summary>
        ///通用名编号
        ///<summary>
        public string genname_codg { get; set; }

        ///<summary>
        ///药品通用名
        ///<summary>
        public string drug_genname { get; set; }

        ///<summary>
        ///化学名称
        ///<summary>
        public string chemname { get; set; }

        ///<summary>
        ///别名
        ///<summary>
        public string alis { get; set; }

        ///<summary>
        ///英文名称
        ///<summary>
        public string eng_name { get; set; }

        ///<summary>
        ///注册名称
        ///<summary>
        public string reg_name { get; set; }

        ///<summary>
        ///药监本位码
        ///<summary>
        public string drugadm_strdcod { get; set; }

        ///<summary>
        ///药品剂型
        ///<summary>
        public string drug_dosform { get; set; }

        ///<summary>
        ///药品剂型名称
        ///<summary>
        public string drug_dosform_name { get; set; }

        ///<summary>
        ///药品类别
        ///<summary>
        public string drug_type { get; set; }

        ///<summary>
        ///药品类别名称
        ///<summary>
        public string drug_type_name { get; set; }

        ///<summary>
        ///药品规格
        ///<summary>
        public string drug_spec { get; set; }

        ///<summary>
        ///药品规格代码
        ///<summary>
        public string drug_spec_code { get; set; }

        ///<summary>
        ///注册剂型
        ///<summary>
        public string reg_dosform { get; set; }

        ///<summary>
        ///注册规格
        ///<summary>
        public string reg_spec { get; set; }

        ///<summary>
        ///注册规格代码
        ///<summary>
        public string reg_spec_code { get; set; }

        ///<summary>
        ///每次用量
        ///<summary>
        public string each_dos { get; set; }

        ///<summary>
        ///使用频次
        ///<summary>
        public string used_frqu { get; set; }

        ///<summary>
        ///酸根盐基
        ///<summary>
        public string acdbas { get; set; }

        ///<summary>
        ///国家药品编号
        ///<summary>
        public string nat_drug_no { get; set; }

        ///<summary>
        ///用法
        ///<summary>
        public string used_mtd { get; set; }

        ///<summary>
        ///中成药标志
        ///<summary>
        public string tcmpat_flag { get; set; }

        ///<summary>
        ///生产地类别
        ///<summary>
        public string prodplac_type { get; set; }

        ///<summary>
        ///生产地类别名称
        ///<summary>
        public string prodplac_type_name { get; set; }

        ///<summary>
        ///计价单位类型
        ///<summary>
        public string prcunt_type { get; set; }

        ///<summary>
        ///非处方药标志
        ///<summary>
        public string otc_flag { get; set; }

        ///<summary>
        ///非处方药标志名称
        ///<summary>
        public string otc_flag_name { get; set; }

        ///<summary>
        ///包装材质
        ///<summary>
        public string pacmatl { get; set; }

        ///<summary>
        ///包装材质名称
        ///<summary>
        public string pacmatl_name { get; set; }

        ///<summary>
        ///包装规格
        ///<summary>
        public string pacspec { get; set; }

        ///<summary>
        ///包装数量
        ///<summary>
        public string pac_cnt { get; set; }

        ///<summary>
        ///功能主治
        ///<summary>
        public string efcc_atd { get; set; }

        ///<summary>
        ///给药途径
        ///<summary>
        public string rute { get; set; }

        ///<summary>
        ///说明书
        ///<summary>
        public string manl { get; set; }

        ///<summary>
        ///开始日期
        ///<summary>
        public string begndate { get; set; }

        ///<summary>
        ///结束日期
        ///<summary>
        public string enddate { get; set; }

        ///<summary>
        ///最小使用单位
        ///<summary>
        public string min_useunt { get; set; }

        ///<summary>
        ///最小销售单位
        ///<summary>
        public string min_salunt { get; set; }

        ///<summary>
        ///最小计量单位
        ///<summary>
        public string min_unt { get; set; }

        ///<summary>
        ///最小包装数量
        ///<summary>
        public string min_pac_cnt { get; set; }

        ///<summary>
        ///最小包装单位
        ///<summary>
        public string min_pacunt { get; set; }

        ///<summary>
        ///最小制剂单位
        ///<summary>
        public string min_prepunt { get; set; }

        ///<summary>
        ///最小包装单位名称
        ///<summary>
        public string min_pacunt_name { get; set; }

        ///<summary>
        ///最小制剂单位名称
        ///<summary>
        public string min_prepunt_name { get; set; }

        ///<summary>
        ///转换比
        ///<summary>
        public string convrat { get; set; }

        ///<summary>
        ///药品有效期
        ///<summary>
        public string drug_expy { get; set; }

        ///<summary>
        ///最小计价单位
        ///<summary>
        public string min_prcunt { get; set; }

        ///<summary>
        ///五笔助记码
        ///<summary>
        public string wubi { get; set; }

        ///<summary>
        ///拼音助记码
        ///<summary>
        public string pinyin { get; set; }

        ///<summary>
        ///分包装厂家
        ///<summary>
        public string subpck_fcty { get; set; }

        ///<summary>
        ///生产企业编号
        ///<summary>
        public string prodentp_code { get; set; }

        ///<summary>
        ///生产企业名称
        ///<summary>
        public string prodentp_name { get; set; }

        ///<summary>
        ///特殊限价药品标志
        ///<summary>
        public string sp_lmtpric_drug_flag { get; set; }

        ///<summary>
        ///特殊药品标志
        ///<summary>
        public string sp_drug_flag { get; set; }

        ///<summary>
        ///限制使用范围
        ///<summary>
        public string lmt_usescp { get; set; }

        ///<summary>
        ///限制使用标志
        ///<summary>
        public string lmt_used_flag { get; set; }

        ///<summary>
        ///药品注册证号
        ///<summary>
        public string drug_regcertno { get; set; }

        ///<summary>
        ///药品注册证号开始日期
        ///<summary>
        public string drug_regcert_begndate { get; set; }

        ///<summary>
        ///药品注册证号结束日期
        ///<summary>
        public string drug_regcert_enddate { get; set; }

        ///<summary>
        ///批准文号
        ///<summary>
        public string aprvno { get; set; }

        ///<summary>
        ///批准文号开始日期
        ///<summary>
        public string aprvno_begndate { get; set; }

        ///<summary>
        ///批准文号结束日期
        ///<summary>
        public string aprvno_enddate { get; set; }

        ///<summary>
        ///市场状态
        ///<summary>
        public string market_condition_code { get; set; }

        ///<summary>
        ///市场状态名称
        ///<summary>
        public string market_condition_name { get; set; }

        ///<summary>
        ///药品注册批件电子档案
        ///<summary>
        public string regdrug_elcword { get; set; }

        ///<summary>
        ///药品补充申请批件电子档案
        ///<summary>
        public string regdrug_add_elcword { get; set; }

        ///<summary>
        ///国家医保药品目录备注
        ///<summary>
        public string yb_drug_memo { get; set; }

        ///<summary>
        ///基本药物标志名称
        ///<summary>
        public string drugbase_flagname { get; set; }

        ///<summary>
        ///基本药物标志
        ///<summary>
        public string drugbase_flag { get; set; }

        ///<summary>
        ///增值税调整药品标志
        ///<summary>
        public string vat__adjust_drugflag { get; set; }

        ///<summary>
        ///增值税调整药品名称
        ///<summary>
        public string vat__adjust_drugname { get; set; }

        ///<summary>
        ///上市药品目录集药品
        ///<summary>
        public string drugslist_onmarket { get; set; }

        ///<summary>
        ///医保谈判药品标志
        ///<summary>
        public string yb_negotiatedrug_flag { get; set; }

        ///<summary>
        ///医保谈判药品名称
        ///<summary>
        public string yb_negotiatedrug_name { get; set; }

        ///<summary>
        ///卫健委药品编码
        ///<summary>
        public string nhc_drugcode { get; set; }

        ///<summary>
        ///备注
        ///<summary>
        public string memo { get; set; }

        ///<summary>
        ///有效标志
        ///<summary>
        public string vali_flag { get; set; }

        ///<summary>
        ///唯一记录号
        ///<summary>
        public string record_num { get; set; }

        ///<summary>
        ///数据创建时间
        ///<summary>
        public string create_time { get; set; }

        ///<summary>
        ///数据更新时间
        ///<summary>
        public string update_time { get; set; }

        ///<summary>
        ///版本号
        ///<summary>
        public string ver_num { get; set; }

        ///<summary>
        ///版本名称
        ///<summary>
        public string ver_name { get; set; }

        ///<summary>
        ///儿童用药
        ///<summary>
        public string child_drug { get; set; }

        ///<summary>
        ///公司名称
        ///<summary>
        public string company { get; set; }

        ///<summary>
        ///仿制药一致性评价药品
        ///<summary>
        public string fda_about { get; set; }

        ///<summary>
        ///经销企业
        ///<summary>
        public string distribution_enterprise { get; set; }

        ///<summary>
        ///经销企业联系人
        ///<summary>
        public string de_linkname { get; set; }

        ///<summary>
        ///经销企业授权书电子档案
        ///<summary>
        public string elefile_authorization_de { get; set; }

        ///<summary>
        ///国家医保药品目录剂型
        ///<summary>
        public string yb_drug_catalogue{get;set;}

    ///<summary>
    ///国家医保药品目录甲乙类标识
    ///<summary>
    public string yb_drug_abtype { get; set; }


}
}
