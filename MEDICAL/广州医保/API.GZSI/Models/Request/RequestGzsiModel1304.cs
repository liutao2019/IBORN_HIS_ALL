using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Request
{
    public class RequestGzsiModel1304
    {
        public Data data { get; set; }

        public class Data
        {
            /// <summary>
            /// 医疗目录编码
            /// </summary>
            public string med_list_codg { get; set; }
            /// <summary>
            /// 通用名编号
            /// </summary>
            public string genname_codg { get; set; }
            /// <summary>
            /// 药品通用名
            /// </summary>
            public string drug_genname { get; set; }
            /// <summary>
            /// 药品商品名
            /// </summary>
            public string drug_prodname { get; set; }
            /// <summary>
            /// 注册名称
            /// </summary>
            public string reg_name { get; set; }
            /// <summary>
            /// 中草药名称
            /// </summary>
            public string tcmherb_name { get; set; }
            /// <summary>
            /// 药材名称
            /// </summary>
            public string mlms_name { get; set; }
            /// <summary>
            /// 有效标志
            /// </summary>
            public string vali_flag { get; set; }
            /// <summary>
            /// 唯一记录号
            /// </summary>
            public string rid { get; set; }
            /// <summary>
            /// 版本号
            /// </summary>
            public string ver { get; set; }
            /// <summary>
            /// 版本名称
            /// </summary>
            public string ver_name { get; set; }
            /// <summary>
            /// 经办开始时间
            /// </summary>
            public string opt_begn_time { get; set; }
            /// <summary>
            /// 经办结束时间
            /// </summary>
            public string opt_end_time { get; set; }
            /// <summary>
            /// 更新时间
            /// </summary>
            public string updt_time { get; set; }
            /// <summary>
            /// 当前页数
            /// </summary>
            public int page_num { get; set; }
            /// <summary>
            /// 本页数据量
            /// </summary>
            public int page_size { get; set; }
        }
    }
}
