using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Response
{
    public class ResponseGzsiModel1317:ResponseBase
    {
        public List<Output> output { get; set; }

        public class Output
        {
            public string fixmedins_code { get; set; }//1-定点医药机构编号     Y
            public string medins_list_codg { get; set; }//2-定点医药机构目录编号     Y
            public string medins_list_name { get; set; }//3-定点医药机构目录名称     N
            public string insu_admdvs { get; set; }//4-参保机构医保区划     Y
            public string list_type { get; set; }//5-目录类别     Y
            public string med_list_codg { get; set; }//6-医疗目录编码     Y
            public string begndate { get; set; }//7-开始日期     Y
            public string enddate { get; set; }//8-结束日期     N
            public string aprvno { get; set; }//9-批准文号     N
            public string dosform { get; set; }//10-剂型     N
            public string exct_cont { get; set; }//11-除外内容     N
            public string item_cont { get; set; }//12-项目内涵     N
            public string prcunt { get; set; }//13-计价单位     N
            public string spec { get; set; }//14-规格     N
            public string pacspec { get; set; }//15-包装规格     N
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
