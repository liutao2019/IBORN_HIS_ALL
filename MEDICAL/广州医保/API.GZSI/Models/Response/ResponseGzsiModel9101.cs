using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Response
{
    /// <summary>
    /// 文件上传
    /// </summary>
    public class ResponseGzsiModel9101 : ResponseBase
    {
        #region 返回

        #region fileinfo节点
        ///// <summary>
        ///// 文件查询号
        ///// </summary>
        //public string file_qury_no { get; set; }
        ///// <summary>
        ///// 文件名称
        ///// </summary>
        //public string filename { get; set; }
        ///// <summary>
        ///// 下载截止时间 yyyy-MM-dd HH:mm:ss
        ///// </summary>
        //public string dld_endtime { get; set; }
        #endregion
        #region  参数
        public string infcode
        {
            get;
            set;
        }
        public string cainfo
        {
            get;
            set;
        }
        public string inf_refmsgid
        {
            get;
            set;
        }
        public string refmsg_time
        {
            get;
            set;
        }
        public string respond_time
        {
            get;
            set;
        }
        public string err_msg
        {
            get;
            set;
        }
        public string warn_msg
        {
            get;
            set;
        }
        #endregion
        #endregion
    }
}
