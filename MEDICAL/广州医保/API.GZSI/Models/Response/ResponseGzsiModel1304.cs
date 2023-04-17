using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Response
{
    public class ResponseGzsiModel1304 : ResponseBase
    {

        public Output output { get; set; }
        public class Output
        {
            public Data data { get; set; }
        }

        public class Data
        {
            /// <summary>
            /// 医疗目录编码1
            /// </summary>
            public string med_list_code { get; set; }
            /// <summary>
            /// 药品商品名2
            /// </summary>
            public string drug_prodname { get; set; }
            /// <summary>
            /// 通用名编号3
            /// </summary>
            public string genname_code { get; set; }
            /// <summary>
            /// 药品通用名4
            /// </summary>
            public string drug_genname { get; set; }
            /// <summary>
            /// 民族药种类5
            /// </summary>
            public string ethdrug_type { get; set; }
            /// <summary>
            /// 化学名称6
            /// </summary>
            public string chemname { get; set; }
            /// <summary>
            /// 别名7
            /// </summary>
            public string alis { get; set; }
            /// <summary>
            /// 英文名称8
            /// </summary>
            public string eng_name { get; set; }
            /// <summary>
            /// 剂型9
            /// </summary>
            public string dosform { get; set; }
            /// <summary>
            /// 每次用量10
            /// </summary>
            public string each_dos { get; set; }
            /// <summary>
            /// 使用频次11
            /// </summary>
            public string used_frqu { get; set; }
            /// <summary>
            /// 国家药品编号12
            /// </summary>
            public string nat_drug_no { get; set; }
            /// <summary>
            /// 用法13
            /// </summary>
            public string used_mtd { get; set; }
            /// <summary>
            /// 成分14
            /// </summary>
            public string ing { get; set; }
            /// <summary>
            /// 性状15
            /// </summary>
            public string chrt { get; set; }
            /// <summary>
            /// 不良反应16
            /// </summary>
            public string defs { get; set; }
            /// <summary>
            /// 禁忌17
            /// </summary>
            public string tabo { get; set; }
            /// <summary>
            /// 注意事项18
            /// </summary>
            public string mnan { get; set; }
            /// <summary>
            /// 贮藏19
            /// </summary>
            public string stog { get; set; }
            /// <summary>
            /// 药品规格20
            /// </summary>
            public string drug_spec { get; set; }
            /// <summary>
            /// 计价单位类型21
            /// </summary>
            public string prcunt_type { get; set; }
            /// <summary>
            /// 非处方药标志22
            /// </summary>
            public string otc_flag { get; set; }
            /// <summary>
            /// 包装材质23
            /// </summary>
            public string pacmatl { get; set; }
            /// <summary>
            /// 包装规格24
            /// </summary>
            public string pacspec { get; set; }
            /// <summary>
            /// 最小使用单位25
            /// </summary>
            public string min_useunt { get; set; }
            /// <summary>
            /// 最小销售单位26
            /// </summary>
            public string min_salunt { get; set; }
            /// <summary>
            /// 说明书27
            /// </summary>
            public string manl { get; set; }
            /// <summary>
            /// 给药途径28
            /// </summary>
            public string rute { get; set; }
            /// <summary>
            /// 开始日期29
            /// </summary>
            public string begndate { get; set; }
            /// <summary>
            /// 结束日期30
            /// </summary>
            public string enddate { get; set; }
            /// <summary>
            /// 药理分类31
            /// </summary>
            public string pham_type { get; set; }
            /// <summary>
            /// 备注32
            /// </summary>
            public string memo { get; set; }
            /// <summary>
            /// 包装数量33
            /// </summary>
            public string pac_cnt { get; set; }
            /// <summary>
            /// 最小计量单位34
            /// </summary>
            public string min_unt { get; set; }
            /// <summary>
            /// 最小包装数量35
            /// </summary>
            public string min_pac_cnt { get; set; }
            /// <summary>
            /// 最小包装单位36
            /// </summary>
            public string min_pacunt { get; set; }
            /// <summary>
            /// 最小制剂单位 37
            /// </summary>
            public string min_prepunt { get; set; }
            /// <summary>
            /// 药品有效期38
            /// </summary>
            public string drug_expy { get; set; }
            /// <summary>
            /// 功能主治 39
            /// </summary>
            public string efcc_atd { get; set; }
            /// <summary>
            /// 最小计价单位 40
            /// </summary>
            public string min_prcunt { get; set; }
            /// <summary>
            /// 五笔助记码 41
            /// </summary>
            public string wubi { get; set; }

            /// <summary>
            /// 拼音助记码 42
            /// </summary>
            public string pinyin { get; set; }
            /// <summary>
            /// 有效标志 43
            /// </summary>
            public string vali_flag { get; set; }
            /// <summary>
            /// 唯一记录号 44
            /// </summary>
            public string rid { get; set; }
            /// <summary>
            /// 数据创建时间 45
            /// </summary>
            public string crte_time { get; set; }
            /// <summary>
            /// 数据更新时间 46
            /// </summary>
            public string updt_time { get; set; }
            /// <summary>
            /// 创建人ID 47
            /// </summary>
            public string crter_id { get; set; }
            /// <summary>
            /// 创建人姓名 48
            /// </summary>
            public string crter_name { get; set; }

            /// <summary>
            /// 创建经办机构 49
            /// </summary>
            public string crte_optins_no { get; set; }
            /// <summary>
            /// 经办人ID 50
            /// </summary>
            public string opter_id { get; set; }
            /// <summary>
            /// 经办人姓名 51
            /// </summary>
            public string opter_name { get; set; }
            /// <summary>
            /// 经办时间 52
            /// </summary>
            public string opt_time { get; set; }
            /// <summary>
            /// 经办机构 53
            /// </summary>
            public string optins_no { get; set; }
            /// <summary>
            /// 版本号 54
            /// </summary>
            public string ver { get; set; }
        }

        #region  参数
        public string infcode
        {
            get;
            set;
        }
        public string cainfo
        {
            get;
            set;
        }
        public string inf_refmsgid
        {
            get;
            set;
        }
        public string refmsg_time
        {
            get;
            set;
        }
        public string respond_time
        {
            get;
            set;
        }
        public string err_msg
        {
            get;
            set;
        }
        public string warn_msg
        {
            get;
            set;
        }
        public string signtype
        {
            get;
            set;
        }
        #endregion
    }
}
