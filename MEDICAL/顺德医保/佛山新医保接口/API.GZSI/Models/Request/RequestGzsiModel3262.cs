using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
namespace API.GZSI.Models.Request
{
    /// <summary>
    /// 异地清分结果确认回退
    /// </summary>
    public class RequestGzsiModel3262
    {
        public Input input { get; set; }

        public class Input
        {
            public string trt_year {get;set;}  //1 结算年度   Y
            public string trt_month {get;set;}  //2 结算月份   Y
            public string otransid {get;set;}  //3 原交易流水号   Y
        }
    }
}