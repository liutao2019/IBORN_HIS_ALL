using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Request
{
    public class RequestGzsiModel1317
    {
        public Data data { get; set; }

        public class Data
        {
            public string query_date { get; set; }//1-查询时间点     
            public string fixmedins_code { get; set; }//2-定点医药机构编号     
            public string medins_list_codg { get; set; }//3-定点医药机构目录编号     
            public string medins_list_name { get; set; }//4-定点医药机构目录名称     
            public string insu_admdvs { get; set; }//5-参保机构医保区划     
            public string list_type { get; set; }//6-目录类别     
            public string med_list_codg { get; set; }//7-医疗目录编码     
            public string begndate { get; set; }//8-开始日期     
            public string vali_flag { get; set; }//9-有效标志     
            public string updt_time { get; set; }//10-更新时间     Y
            public string page_num { get; set; }//11-当前页数     Y
            public string page_size { get; set; }//12-本页数据量     Y

        }
    }
}
