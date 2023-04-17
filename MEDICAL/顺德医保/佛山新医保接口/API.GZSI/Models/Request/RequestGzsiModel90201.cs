using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Request
{
    public class RequestGzsiModel90201
    {
        public Data data { get; set; }

        public class Data
        {
            /// <summary>
            /// 人员编号 Y
            /// </summary>
            public string psn_no {get;set;}
            /// <summary>
            /// 定点医药机构编号 Y
            /// </summary>
            public string fixmedins_code {get;set;}
            /// <summary>
            /// 定点医药机构名称
            /// </summary>
            public string fixmedins_name {get;set;}
            /// <summary>
            /// 联系电话
            /// </summary>
            public string tel {get;set;}
            /// <summary>
            /// 孕周数
            /// </summary>
            public string geso_val {get;set;}
            /// <summary>
            /// 胎次
            /// </summary>
            public string fetts {get;set;}
            /// <summary>
            /// 生育类别 Y
            /// </summary>
            public string matn_type {get;set;}
            /// <summary>
            /// 生育待遇申报人员类别
            /// </summary>
            public string matn_trt_dclaer_type {get;set;}
            /// <summary>
            /// 计划生育服务证号
            /// </summary>
            public string fpsc_no {get;set;}
            /// <summary>
            /// 末次月经日期
            /// </summary>
            public string last_mena_date {get;set;}
            /// <summary>
            /// 预计生育日期
            /// </summary>
            public string plan_matn_date {get;set;}
            /// <summary>
            /// 开始日期
            /// </summary>
            public string begndate {get;set;}
            /// <summary>
            /// 结束日期
            /// </summary>
            public string enddate {get;set;}
            /// <summary>
            /// 代办人姓名
            /// </summary>
            public string agnter_name {get;set;}
            /// <summary>
            /// 代办人证件类型
            /// </summary>
            public string agnter_cert_type {get;set;}
            /// <summary>
            /// 代办人证件号码
            /// </summary>
            public string agnter_certno {get;set;}
            /// <summary>
            /// 代办人联系方式
            /// </summary>
            public string agnter_tel {get;set;}
            /// <summary>
            /// 代办人关系
            /// </summary>
            public string agnter_rlts {get;set;}
            /// <summary>
            /// 代办人联系地址
            /// </summary>
            public string agnter_addr {get;set;}
            /// <summary>
            /// 配偶姓名
            /// </summary>
            public string spus_name {get;set;}
            /// <summary>
            /// 配偶证件类型
            /// </summary>
            public string spus_cert_type { get; set; }
            /// <summary>
            /// 配偶证件号码
            /// </summary>
            public string spus_certno {get;set;}

        }
    }
}
