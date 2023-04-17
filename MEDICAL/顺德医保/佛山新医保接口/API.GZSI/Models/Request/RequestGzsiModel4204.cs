using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Request
{
    public class RequestGzsiModel4204
    {
        #region 自费病人费用明细信息(节点标识：feedetail)
        public Feedetail feedetail { get; set; }
        public class Feedetail
        {
            /// <summary>
            /// 	医药机构就诊ID	
            /// </summary>
            public string fixmedins_mdtrt_id { get; set; }
            /// <summary>
            /// 	定点医药机构编号	
            /// </summary>
            public string fixmedins_code { get; set; }
        }
        #endregion

        #region 自费病人费用明细流水信息(节点标识：feedetI)
        public FeedetI feedetI { get; set; }
        public class FeedetI
        {
            /// <summary>
            /// 	记账流水号	
            /// </summary>
            public string bkkp_sn { get; set; }
        }
        #endregion
    }
}
