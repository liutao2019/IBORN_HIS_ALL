using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Response
{
    public class ResponseGzsiModel5206 : ResponseBase
    {
        public Output output { get; set; }

        public class Output
        {
            public List<Cuminfo> cuminfo { get; set; }

            public class Cuminfo
            {
                /// <summary>
                ///	险种类型	
                /// <summary>
                [API.GZSI.Common.Display("险种类型")]
                public string insutype { get; set; }

                /// <summary>
                ///	年度	
                /// <summary>
                [API.GZSI.Common.Display("年度")]
                public string year { get; set; }

                /// <summary>
                ///	累计年月	
                /// <summary>
                [API.GZSI.Common.Display("累计年月")]
                public string cum_ym { get; set; }

                /// <summary>
                ///	累计类别代码	
                /// <summary>
                [API.GZSI.Common.Display("累计类别代码")]
                public string cum_type_code { get; set; }

                /// <summary>
                ///	累计值	
                /// <summary>
                [API.GZSI.Common.Display("累计值")]
                public string cum { get; set; }

            }
        }
    }
}
