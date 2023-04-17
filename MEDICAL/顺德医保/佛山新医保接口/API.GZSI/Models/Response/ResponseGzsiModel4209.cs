using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Response
{
    public class ResponseGzsiModel4209 : ResponseBase
    {
       public Output output { get; set; }
       public class Output
       {
           public string recordCounts { get; set; }

           public string pages { get; set; }

           public List<Data> data { get; set; }
       }

        public class Data {
            /// <summary>
            /// 	诊断信息ID	
            /// </summary>
            public string diagInfoId { get; set; }
            /// <summary>
            /// 	医药机构就诊ID	
            /// </summary>
            public string fixmedinsMdtrtId { get; set; }
            /// <summary>
            /// 	定点医药机构编号	
            /// </summary>
            public string fixmedinsCode { get; set; }
            /// <summary>
            /// 	出入院诊断类别	
            /// </summary>
            public string inoutDiagType { get; set; }
            /// <summary>
            /// 	诊断类别	
            /// </summary>
            public string diagType { get; set; }
            /// <summary>
            /// 	主诊断标志	
            /// </summary>
            public string maindiagFlag { get; set; }
            /// <summary>
            /// 	诊断排序号	
            /// </summary>
            public string diagSrtNo { get; set; }
            /// <summary>
            /// 	诊断代码	
            /// </summary>
            public string diagCode { get; set; }
            /// <summary>
            /// 	诊断名称	
            /// </summary>
            public string diagName { get; set; }
            /// <summary>
            /// 	诊断科室	
            /// </summary>
            public string diagDept { get; set; }
            /// <summary>
            /// 	诊断医师代码	
            /// </summary>
            public string diagDrCode { get; set; }
            /// <summary>
            /// 	诊断医师姓名	
            /// </summary>
            public string diagDrName { get; set; }
            /// <summary>
            /// 	诊断时间	
            /// </summary>
            public string diagTime { get; set; }
            /// <summary>
            /// 	入院病情	
            /// </summary>
            public string admCond { get; set; }
            /// <summary>
            /// 	有效标志	
            /// </summary>
            public string valiFlag { get; set; }
            /// <summary>
            /// 	经办人ID	
            /// </summary>
            public string opterId { get; set; }
            /// <summary>
            /// 	经办人姓名	
            /// </summary>
            public string opterName { get; set; }
            /// <summary>
            /// 	经办时间	
            /// </summary>
            public string optTime { get; set; }
            /// <summary>
            /// 	唯一记录号	
            /// </summary>
            public string rid { get; set; }
            /// <summary>
            /// 	更新时间	
            /// </summary>
            public string updtTime { get; set; }
            /// <summary>
            /// 	创建人	
            /// </summary>
            public string crterId { get; set; }
            /// <summary>
            /// 	创建人姓名	
            /// </summary>
            public string crterName { get; set; }
            /// <summary>
            /// 	创建时间	
            /// </summary>
            public string crteTime { get; set; }
            /// <summary>
            /// 	创建机构	
            /// </summary>
            public string crteOptinsNo { get; set; }
            /// <summary>
            /// 	经办机构	
            /// </summary>
            public string optinsNo { get; set; }
            /// <summary>
            /// 	统筹区编码	
            /// </summary>
            public string poolareaNO { get; set; }

        }

    }
}
