using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Response
{
    public class ResponseGzsiModel3260 : ResponseBase
    {
        public Output output { get; set; }

        /// <summary>
        /// output类
        /// </summary>
        public class Output
        {
            public string seqno {get;set;}  //1 顺序号   Y
            public string mdtrtarea {get;set;}  //2 就医地医保区划   Y
            public string medins_no {get;set;}  //3 医疗机构编号   Y
            public string certno {get;set;}  //4 证件号码   Y
            public string mdtrt_id {get;set;}  //5 就诊登记号   Y
            public string mdtrt_setl_time {get;set;}  //6 就诊结算时间   Y
            public string setl_sn {get;set;}  //7 结算流水号   Y  
            public string fulamt_advpay_flag {get;set;}  //8 全额垫付标志   Y
            public string medfee_sumamt {get;set;}  //9 总费用   Y
            public string optins_pay_sumamt {get;set;}  //10 经办机构支付总额   Y
        }
    }
}
