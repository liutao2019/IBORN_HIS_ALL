using System;

namespace FS.HISFC.Models.SIInterface
{
	/// <summary>
	/// BlackList 的摘要说明。
	/// </summary>
    public class YB_1306 : FS.FrameWork.Models.NeuObject
	{
        public YB_1306()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

        ///<summary>
        ///医疗目录编码
        ///<summary>
        public string wm_dise_id { get; set; }

        ///<summary>
        ///耗材名称
        ///<summary>
        public string cpr { get; set; }

        ///<summary>
        ///医疗器械唯一标识码
        ///<summary>
        public string cpr_code_scp { get; set; }

        ///<summary>
        ///医保通用名代码
        ///<summary>
        public string cpr_name { get; set; }

        ///<summary>
        ///医保通用名
        ///<summary>
        public string sec_code_scp { get; set; }

        ///<summary>
        ///产品型号
        ///<summary>
        public string prod_mol { get; set; }

        ///<summary>
        ///规格代码
        ///<summary>
        public string spec_code { get; set; }

        ///<summary>
        ///规格
        ///<summary>
        public string spec { get; set; }

        ///<summary>
        ///耗材分类
        ///<summary>
        public string mcs_type { get; set; }

        ///<summary>
        ///规格型号
        ///<summary>
        public string spec_mol { get; set; }

        ///<summary>
        ///材质代码
        ///<summary>
        public string dise_code { get; set; }

        ///<summary>
        ///耗材材质
        ///<summary>
        public string mcs_matl { get; set; }

        ///<summary>
        ///包装规格
        ///<summary>
        public string pacspec { get; set; }

        ///<summary>
        ///包装数量
        ///<summary>
        public string pac_cnt { get; set; }

        ///<summary>
        ///产品包装材质
        ///<summary>
        public string prod_pacmatl { get; set; }

        ///<summary>
        ///包装单位
        ///<summary>
        public string pacunt { get; set; }

        ///<summary>
        ///产品转换比
        ///<summary>
        public string prod_convrat { get; set; }

        ///<summary>
        ///最小使用单位
        ///<summary>
        public string min_useunt { get; set; }

        ///<summary>
        ///生产地类别
        ///<summary>
        public string prodplac_type { get; set; }

        ///<summary>
        ///生产地类别名称
        ///<summary>
        public string prodplac_name { get; set; }

        ///<summary>
        ///产品标准
        ///<summary>
        public string cpbz { get; set; }

        ///<summary>
        ///产品有效期
        ///<summary>
        public string prodexpy { get; set; }

        ///<summary>
        ///性能结构与组成
        ///<summary>
        public string xnjgyzc { get; set; }

        ///<summary>
        ///适用范围
        ///<summary>
        public string syfw { get; set; }

        ///<summary>
        ///产品使用方法
        ///<summary>
        public string cpsyff { get; set; }

        ///<summary>
        ///产品图片编号
        ///<summary>
        public string cptpbh { get; set; }

        ///<summary>
        ///产品质量标准
        ///<summary>
        public string cpzlbz { get; set; }

        ///<summary>
        ///说明书
        ///<summary>
        public string manl { get; set; }

        ///<summary>
        ///其他证明材料
        ///<summary>
        public string qtzmcl { get; set; }

        ///<summary>
        ///专机专用标志
        ///<summary>
        public string zjzybz { get; set; }

        ///<summary>
        ///专机名称
        ///<summary>
        public string zj_name { get; set; }

        ///<summary>
        ///组套名称
        ///<summary>
        public string zt_name { get; set; }

        ///<summary>
        ///机套标志
        ///<summary>
        public string zt_flag { get; set; }

        ///<summary>
        ///限制使用标志
        ///<summary>
        public string lmt_used_flag { get; set; }

        ///<summary>
        ///医保限用范围
        ///<summary>
        public string lmt_usescp { get; set; }

        ///<summary>
        ///最小销售单位
        ///<summary>
        public string min_salunt { get; set; }

        ///<summary>
        ///高值耗材标志
        ///<summary>
        public string highval_mcs_flag { get; set; }

        ///<summary>
        ///医用材料分类代码
        ///<summary>
        public string yyclfl_code { get; set; }

        ///<summary>
        ///植入材料和人体器官标志
        ///<summary>
        public string impt_matl_hmorgn_flag { get; set; }

        ///<summary>
        ///灭菌标志
        ///<summary>
        public string mj_flag { get; set; }

        ///<summary>
        ///灭菌标志名称
        ///<summary>
        public string mj_name { get; set; }

        ///<summary>
        ///植入或介入类标志
        ///<summary>
        public string impt_itvt_clss_flag { get; set; }

        ///<summary>
        ///植入或介入类名称
        ///<summary>
        public string impt_itvt_clss_name { get; set; }

        ///<summary>
        ///一次性使用标志
        ///<summary>
        public string dspo_used_flag { get; set; }

        ///<summary>
        ///一次性使用标志名称
        ///<summary>
        public string dspo_used_name { get; set; }

        ///<summary>
        ///注册备案人名称
        ///<summary>
        public string rzcbar_name { get; set; }

        ///<summary>
        ///开始日期
        ///<summary>
        public string begndate { get; set; }

        ///<summary>
        ///结束日期
        ///<summary>
        public string enddate { get; set; }

        ///<summary>
        ///医疗器械管理类别
        ///<summary>
        public string ylqxgllb_flag { get; set; }

        ///<summary>
        ///医疗器械管理类别名称
        ///<summary>
        public string ylqxgllb_name { get; set; }

        ///<summary>
        ///注册备案号
        ///<summary>
        public string reg_fil_no { get; set; }

        ///<summary>
        ///注册备案产品名称
        ///<summary>
        public string reg_fil_name { get; set; }

        ///<summary>
        ///结构及组成
        ///<summary>
        public string jgjzc { get; set; }

        ///<summary>
        ///其他内容
        ///<summary>
        public string qtnr { get; set; }

        ///<summary>
        ///批准日期
        ///<summary>
        public string aprv_date { get; set; }

        ///<summary>
        ///注册备案人住所
        ///<summary>
        public string zcbar_addr { get; set; }

        ///<summary>
        ///注册证有效期开始时间
        ///<summary>
        public string zcz_begndate { get; set; }

        ///<summary>
        ///注册证有效期结束时间
        ///<summary>
        public string zcz_enddate { get; set; }

        ///<summary>
        ///生产企业编号
        ///<summary>
        public string scqy_code { get; set; }

        ///<summary>
        ///生产企业名称
        ///<summary>
        public string scqy_name { get; set; }

        ///<summary>
        ///生产地址
        ///<summary>
        public string sc_addr { get; set; }

        ///<summary>
        ///代理人企业
        ///<summary>
        public string dlrqy { get; set; }

        ///<summary>
        ///代理人企业地址
        ///<summary>
        public string dlrqy_addr { get; set; }

        ///<summary>
        ///生产国或地区
        ///<summary>
        public string scghdq { get; set; }

        ///<summary>
        ///售后服务机构
        ///<summary>
        public string shfwjg { get; set; }

        ///<summary>
        ///注册或备案证电子档案
        ///<summary>
        public string zchbazdzda { get; set; }

        ///<summary>
        ///产品影像
        ///<summary>
        public string cpyx { get; set; }

        ///<summary>
        ///有效标志
        ///<summary>
        public string valid_state { get; set; }

        ///<summary>
        ///唯一记录号
        ///<summary>
        public string wyjlh { get; set; }

        ///<summary>
        ///版本号
        ///<summary>
        public string ver { get; set; }

        ///<summary>
        ///版本名称
        ///<summary>
        public string ver_name { get; set; }




}
}
