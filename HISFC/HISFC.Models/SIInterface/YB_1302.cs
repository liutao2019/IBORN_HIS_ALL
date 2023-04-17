using System;

namespace FS.HISFC.Models.SIInterface
{
	/// <summary>
	/// BlackList 的摘要说明。
	/// </summary>
    public class YB_1302 : FS.FrameWork.Models.NeuObject
	{
		public YB_1302()
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
        ///单味药名称
        ///<summary>
        public string sindrug_name { get; set; }

        ///<summary>
        ///单复方标志
        ///<summary>
        public string cpnd_flag { get; set; }

        ///<summary>
        ///质量等级
        ///<summary>
        public string qlt_lv { get; set; }

        ///<summary>
        ///中草药年份
        ///<summary>
        public string tcmdrug_year { get; set; }

        ///<summary>
        ///药用部位
        ///<summary>
        public string med_part { get; set; }

        ///<summary>
        ///安全计量
        ///<summary>
        public string safe_mtr { get; set; }

        ///<summary>
        ///常规用法
        ///<summary>
        public string cnvl_used { get; set; }

        ///<summary>
        ///性味
        ///<summary>
        public string natfla { get; set; }

        ///<summary>
        ///归经
        ///<summary>
        public string mertpm { get; set; }

        ///<summary>
        ///品种
        ///<summary>
        public string cat { get; set; }

        ///<summary>
        ///开始日期
        ///<summary>
        public string begndate { get; set; }

        ///<summary>
        ///结束日期
        ///<summary>
        public string enddate { get; set; }

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
        ///药材名称
        ///<summary>
        public string drug_name { get; set; }

        ///<summary>
        ///功能主治
        ///<summary>
        public string efcc_atd { get; set; }

        ///<summary>
        ///炮制方法
        ///<summary>
        public string processing_method { get; set; }

        ///<summary>
        ///功效分类
        ///<summary>
        public string functional_classification { get; set; }

        ///<summary>
        ///药材种来源
        ///<summary>
        public string source { get; set; }

        ///<summary>
        ///国家医保支付政策
        ///<summary>
        public string yb_gj_paypolicy { get; set; }

        ///<summary>
        ///省级医保支付政策
        ///<summary>
        public string yb_sj_paypolicy { get; set; }

        ///<summary>
        ///标准名称
        ///<summary>
        public string standard_name { get; set; }

        ///<summary>
        ///标准页码
        ///<summary>
        public string standard_page { get; set; }

        ///<summary>
        ///标准电子档案
        ///<summary>
        public string standard_elcword { get; set; }




    }
}
