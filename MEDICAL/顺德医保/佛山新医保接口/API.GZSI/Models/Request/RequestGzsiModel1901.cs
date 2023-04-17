using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Request
{
    /// <summary>
    /// 码表下载
    /// </summary>
    public class RequestGzsiModel1901
    {
         public Data data { get; set; }

         public class Data
         {
             /// <summary>
             /// 医保区划  440100
             /// </summary>
             public string admdvs { get; set; }
         }
        
    }
}
