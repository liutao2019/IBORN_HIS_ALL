using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
namespace API.GZSI.Models.Request
{
    /// <summary>
    /// 异地清分结果确认
    /// </summary>
    public class RequestGzsiModel3261
    {
        public Input input { get; set; }

        public class Input
        {
            public Data data { get; set; }
            public List<Detail> detail { get; set; }
        }

        public class Data
        {
            public string trt_year { get; set; }  //1 结算年度   Y
            public string trt_month { get; set; }  //2 结算月份   Y
            public string totalrow { get; set; }  //3 总行数   Y
        }

        public class Detail
        { 
            public string certno {get;set;}  //1 证件号码   Y
            public string mdtrt_id {get;set;}  //2 就诊登记号   Y
            public string mdtrt_setl_time {get;set;}  //3 就诊结算时间   Y
            public string setl_sn {get;set;}  //4 结算流水号   Y
            public string medfee_sumamt {get;set;}  //5 总费用   Y
            public string optins_pay_sumamt {get;set;}  //6 经办机构支付总额   Y
            public string cnfm_flag {get;set;}  //7 确认标志   
        }
    }
}