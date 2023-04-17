using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Request
{
    public class RequestGzsiModel1319
    {
        public Data data { get; set; }

        public class Data
        {
            public string query_date { get; set; }//1-查询时间点     
            public string hilist_code { get; set; }//2-医保目录编码     
            public string selfpay_prop_psn_type { get; set; }//3-医保目录自付比例人员类别     
            public string selfpay_prop_type { get; set; }//4-目录自付比例类别     
            public string insu_admdvs { get; set; }//5-参保机构医保区划     
            public string begndate { get; set; }//6-开始日期     
            public string enddate { get; set; }//7-结束日期     
            public string vali_flag { get; set; }//8-有效标志     
            public string rid { get; set; }//9-唯一记录号     
            public string tabname { get; set; }//10-表名     
            public string poolarea_no { get; set; }//11-统筹区     
            public string updt_time { get; set; }//12-更新时间     Y
            public string page_num { get; set; }//13-当前页数     Y
            public string page_size { get; set; }//14-本页数据量     Y

        }
    }
}
