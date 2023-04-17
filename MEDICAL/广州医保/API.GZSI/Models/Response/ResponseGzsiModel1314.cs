using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Response
{
    public class ResponseGzsiModel1314:ResponseBase
    {
        public Output output { get; set; }

        public class Output
        {
            public string file_qury_no{get;set;} //文件查询号
            public string filename{get;set;}   //文件名
            public string dld_end_time{get;set;} //下载截止日期
            public string data_cnt{get;set;}   //数据量
        }
    }
}
