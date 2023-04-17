using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
namespace API.GZSI.Models.Request
{
    /// <summary>
    /// 提取异地清分明细
    /// </summary>
    public class RequestGzsiModel3260
    {
        public Input input { get; set; }

        public class Input
        { 
            public string trt_year {get;set;}  //1 结算年度   Y
            public string trt_month {get;set;}  //2 结算月份   Y 
            public string startrow {get;set;}  //3 开始行数   Y
        }
    }
}