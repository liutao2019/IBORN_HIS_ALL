using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Response
{
    public class ResponseGzsiModel5302 : ResponseBase
    {
        public Output output { get; set; }

        public class Output
        {
            public List<Psnfixmedin> psnfixmedin { get; set; }

            public class Psnfixmedin
            {
                /// <summary>
                ///	人员编号	
                /// <summary>
                [API.GZSI.Common.Display("人员编号")]
                public string psn_no { get; set; }

                /// <summary>
                ///	险种类型	
                /// <summary>
                [API.GZSI.Common.Display("险种类型")]
                public string insutype { get; set; }

                /// <summary>
                ///	定点排序号	
                /// <summary>
                [API.GZSI.Common.Display("定点排序号")]
                public string fix_srt_no { get; set; }

                /// <summary>
                ///	定点医药机构编号	
                /// <summary>
                [API.GZSI.Common.Display("定点医药机构编号")]
                public string fixmedins_code { get; set; }

                /// <summary>
                ///	定点医药机构名称	
                /// <summary>
                [API.GZSI.Common.Display("定点医药机构名称")]
                public string fixmedins_name { get; set; }

                /// <summary>
                ///	开始日期	
                /// <summary>
                [API.GZSI.Common.Display("开始日期")]
                public string begndate { get; set; }

                /// <summary>
                ///	结束日期	
                /// <summary>
                [API.GZSI.Common.Display("结束日期")]
                public string enddate { get; set; }

                /// <summary>
                ///	备注	
                /// <summary>
                [API.GZSI.Common.Display("备注")]
                public string memo { get; set; }


            }
        }
    }
}
