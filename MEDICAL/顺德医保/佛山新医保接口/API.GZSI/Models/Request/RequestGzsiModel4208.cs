using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Request
{
    public class RequestGzsiModel4208
    {
        #region 自费病人就诊信息(节点标识：input)
        /// <summary>
        /// 	人员证件类型	
        /// </summary>
        public string psn_cert_type { get; set; }
        /// <summary>
        /// 	证件号码	
        /// </summary>
        public string certno { get; set; }
        /// <summary>
        /// 	人员姓名	
        /// </summary>
        public string psn_name { get; set; }
        /// <summary>
        /// 	开始时间	
        /// </summary>
        public string begntime { get; set; }
        /// <summary>
        /// 	结束时间	
        /// </summary>
        public string endtime { get; set; }
        /// <summary>
        /// 	医疗类别	
        /// </summary>
        public string med_type { get; set; }
        /// <summary>
        /// 	医疗总费用	
        /// </summary>
        public string medfee_sumam_t { get; set; }
        /// <summary>
        /// 	电子票据号码	
        /// </summary>
        public string elec_billno_code { get; set; }
        /// <summary>
        /// 	当前页数	
        /// </summary>
        public string page_num { get; set; }
        /// <summary>
        /// 	本页数据量	
        /// </summary>
        public string page_size { get; set; }

        #endregion
    }
}
