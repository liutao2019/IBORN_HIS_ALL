using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Response
{
    public class ResponseGzsiModel1312:ResponseBase
    {
        public List <Output> output { get; set; }

        public class Output
        {
            public string hilist_code { get; set; }//1-医保目录编码     Y
            public string hilist_name { get; set; }//2-医保目录名称     Y
            public string insu_admdvs { get; set; }//3-参保机构医保区划     Y
            public string begndate { get; set; }//4-开始日期     Y
            public string enddate { get; set; }//5-结束日期     N
            public string med_chrgitm_type { get; set; }//6-医疗收费项目类别     Y
            public string chrgitm_lv { get; set; }//7-收费项目等级     Y
            public string lmt_used_flag { get; set; }//8-限制使用标志     Y
            public string list_type { get; set; }//9-目录类别     Y
            public string med_use_flag { get; set; }//10-医疗使用标志     Y
            public string matn_used_flag { get; set; }//11-生育使用标志     Y
            public string hilist_use_type { get; set; }//12-医保目录使用类别     Y
            public string lmt_cpnd_type { get; set; }//13-限复方使用类型     Y
            public string wubi { get; set; }//14-五笔助记码     N
            public string pinyin { get; set; }//15-拼音助记码     N
            public string memo { get; set; }//16-备注     N
            public string vali_flag { get; set; }//17-有效标志     Y
            public string rid { get; set; }//18-唯一记录号     Y
            public string updt_time { get; set; }//19-更新时间     Y
            public string crter_id { get; set; }//20-创建人     N
            public string crter_name { get; set; }//21-创建人姓名     N
            public string crte_time { get; set; }//22-创建时间     Y
            public string crte_optins_no { get; set; }//23-创建机构     N
            public string opter_id { get; set; }//24-经办人     N
            public string opter_name { get; set; }//25-经办人姓名     N
            public string opt_time { get; set; }//26-经办时间     N
            public string optins_no { get; set; }//27-经办机构     N
            public string poolarea_no { get; set; }//28-统筹区     N
        }
    }
}
