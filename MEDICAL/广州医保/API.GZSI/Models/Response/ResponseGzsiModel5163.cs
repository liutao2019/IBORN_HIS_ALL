using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Response
{
    public class ResponseGzsiModel5163 : ResponseBase
    {
        public Output output { get; set; }

        public class Output
        {
            public Result result { get; set; }
        }

        public class Result
        {
            /// <summary>
            /// 总记录数
            /// </summary>
            public string totlcnt { get; set; }

            /// <summary>
            /// 目录对照详情
            /// </summary>
            public List<CatalogMatchInfo> catalogmatchinfo { get; set; }
        }

        public class CatalogMatchInfo
        {
            /// <summary>
            /// 顺序号 Y
            /// </summary>
            public string seq { get; set; }

            /// <summary>
            /// 医疗机构目录编号 Y
            /// </summary>
            public string medins_hilist_id { get; set; }

            /// <summary>
            /// 医疗机构目录名称 Y
            /// </summary>
            public string medins_hilist_name { get; set; }

            /// <summary>
            /// 医疗目录类别 Y
            /// </summary>
            public string list_type { get; set; }

            /// <summary>
            /// 医疗目录编码 Y
            /// </summary>
            public string med_list_codg { get; set; }

            /// <summary>
            /// 开始日期 Y
            /// </summary>
            public string begndate { get; set; }

            /// <summary>
            /// 结束日期
            /// </summary>
            public string enddate { get; set; }

            /// <summary>
            /// 药监本位码
            /// </summary>
            public string drugadm_strdcod { get; set; }

            /// <summary>
            /// 通用名编号
            /// </summary>
            public string genname_no { get; set; }

            /// <summary>
            /// 批准文号 Y
            /// </summary>
            public string aprvno { get; set; }

            /// <summary>
            /// 剂型
            /// </summary>
            public string dosform { get; set; }

            /// <summary>
            /// 除外内容 
            /// </summary>
            public string exept_cont { get; set; }

            /// <summary>
            /// 项目内涵 
            /// </summary>
            public string item_cont { get; set; }

            /// <summary>
            /// 计价单位 
            /// </summary>
            public string prcunt { get; set; }

            /// <summary>
            /// 规格 
            /// </summary>
            public string spec { get; set; }

            /// <summary>
            /// 包装规格 
            /// </summary>
            public string pacspec { get; set; }

            /// <summary>
            /// 单价  
            /// </summary>
            public string pric { get; set; }

            /// <summary>
            /// 备注   
            /// </summary>
            public string memo { get; set; }

            /// <summary>
            /// 审核状态 Y   
            /// </summary>
            public string chk_stas  { get; set; }
        }
    }
}