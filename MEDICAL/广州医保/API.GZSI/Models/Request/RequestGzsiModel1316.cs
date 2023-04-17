using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Request
{
    public class RequestGzsiModel1316
    {
        public Data data { get; set; }

        public class Data
        {
            public string query_date { get; set; }//1-查询时间点     
            public string medins_list_codg { get; set; }//2-定点医药机构目录编号     
            public string hilist_code { get; set; }//3-医保目录编码
            public string hilist_name { get; set; }//3-医保目录名称  
            public string list_type { get; set; }//4-目录类别     
            public string insu_admdvs { get; set; }//5-参保机构医保区划     
            public string begndate { get; set; }//6-开始日期     
            public string vali_flag { get; set; }//7-有效标志     
            public string updt_time { get; set; }//9-更新时间     Y
            public string page_num { get; set; }//10-当前页数     Y
            public string page_size { get; set; }//11-本页数据量     Y

        }
    }
}
