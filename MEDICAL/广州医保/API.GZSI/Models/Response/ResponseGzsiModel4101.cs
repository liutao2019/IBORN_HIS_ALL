using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Response
{
    public class ResponseGzsiModel4101 : ResponseBase
    {
        public Output output { get; set; }
        public class Output
        {
            /// <summary>
            ///清单流水号
            /// <summary>
            public string setl_list_id { get; set; }
        }

        #region  参数
        //public string infcode
        //{
        //    get;
        //    set;
        //}
        //public string cainfo
        //{
        //    get;
        //    set;
        //}
        //public string inf_refmsgid
        //{
        //    get;
        //    set;
        //}
        //public string refmsg_time
        //{
        //    get;
        //    set;
        //}
        //public string respond_time
        //{
        //    get;
        //    set;
        //}
        //public string err_msg
        //{
        //    get;
        //    set;
        //}
        //public string warn_msg
        //{
        //    get;
        //    set;
        //}
        //public string signtype
        //{
        //    get;
        //    set;
        //}
        #endregion
    }
}
