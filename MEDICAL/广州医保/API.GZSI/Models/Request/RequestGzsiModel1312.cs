using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Request
{
    public class RequestGzsiModel1312
    {
        public Data data { get; set; }

        public class Data
        {
            public string query_date { get; set; }//1-查询时间点     
            public string hilist_code { get; set; }//2-医保目录编码     
            public string insu_admdvs { get; set; }//3-参保机构医保区划     
            public string begndate { get; set; }//4-开始日期     
            public string hilist_name { get; set; }//5-医保目录名称     
            public string wubi { get; set; }//6-五笔助记码     
            public string pinyin { get; set; }//7-拼音助记码     
            public string med_chrgitm_type { get; set; }//8-医疗收费项目类别     
            public string chrgitm_lv { get; set; }//9-收费项目等级     
            public string lmt_used_flag { get; set; }//10-限制使用标志     
            public string list_type { get; set; }//11-目录类别     
            public string med_use_flag { get; set; }//12-医疗使用标志     
            public string matn_used_flag { get; set; }//13-生育使用标志     
            public string hilist_use_type { get; set; }//14-医保目录使用类别     
            public string lmt_cpnd_type { get; set; }//15-限复方使用类型     
            public string vali_flag { get; set; }//17-有效标志     
            public string updt_time { get; set; }//18-更新时间     Y
            public string page_num { get; set; }//19-当前页数     Y
            public string page_size { get; set; }//20-本页数据量     Y
        }
    }
}
