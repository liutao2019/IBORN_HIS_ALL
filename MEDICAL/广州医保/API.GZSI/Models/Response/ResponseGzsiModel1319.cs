﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Response
{
    public class ResponseGzsiModel1319:ResponseBase
    {
        public List<Output> output { get; set; }

        public class Output
        {
            public string hilist_code { get; set; }//1-医保目录编码     Y
            public string selfpay_prop_psn_type { get; set; }//2-医保目录自付比例人员类别     Y
            public string selfpay_prop_type { get; set; }//3-目录自付比例类别     Y
            public string insu_admdvs { get; set; }//4-参保机构医保区划     Y
            public string begndate { get; set; }//5-开始日期     Y
            public string enddate { get; set; }//6-结束日期     N
            public string selfpay_prop { get; set; }//7-自付比例     Y
            public string vali_flag { get; set; }//8-有效标志     Y
            public string rid { get; set; }//9-唯一记录号     Y
            public string updt_time { get; set; }//10-更新时间     Y
            public string crter_id { get; set; }//11-创建人     N
            public string crter_name { get; set; }//12-创建人姓名     N
            public string crte_time { get; set; }//13-创建时间     Y
            public string crte_optins_no { get; set; }//14-创建机构     N
            public string opter_id { get; set; }//15-经办人     N
            public string opter_name { get; set; }//16-经办人姓名     N
            public string opt_time { get; set; }//17-经办时间     N
            public string optins_no { get; set; }//18-经办机构     N
            public string tabname { get; set; }//19-表名     N
            public string poolarea_no { get; set; }//20-统筹区     N
        }
    }
}
