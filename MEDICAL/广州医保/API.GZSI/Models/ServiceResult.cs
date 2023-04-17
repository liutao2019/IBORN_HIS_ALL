using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models
{
    /// <summary>
    /// 公共返回json
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ServiceResult<T>
    {
        public string code
        {
            get;
            set;
        }

        public string type
        {
            get;
            set;
        }

        public string message
        {
            get;
            set;
        }
        public T data
        {
            get;
            set;
        }
    }
}
