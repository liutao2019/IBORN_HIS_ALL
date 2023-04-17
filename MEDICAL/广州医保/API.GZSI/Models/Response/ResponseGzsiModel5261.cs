using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Response
{
    /// <summary>
    /// 门诊结算撤销
    /// </summary>
    public class ResponseGzsiModel5261 : ResponseBase
    {
        public Output output { get; set; }

        public class Output
        {
            /// <summary>
            /// 结算信息
            /// </summary>
            public List<ResponseGzsiModel5261.Output.Setlinfo> setlinfo { get; set; }

            public class Setlinfo
            {
				/// <summary>
				/// 	受理号	 
				/// </summary>
				public string setl_id { get; set; }
				/// <summary>
				/// 	零报类型	 
				/// </summary>
				public string reim_type { get; set; }
				/// <summary>
				/// 	零星报销原因	 
				/// </summary>
				public string manl_reim_rea { get; set; }
				/// <summary>
				/// 	就诊ID	 
				/// </summary>
				public string mdtrt_id { get; set; }
				/// <summary>
				/// 	人员姓名	 
				/// </summary>
				public string psn_name { get; set; }
				/// <summary>
				/// 	人员证件类型	 
				/// </summary>
				public string psn_cert_type { get; set; }
				/// <summary>
				/// 	证件号码	 
				/// </summary>
				public string certno { get; set; }
				/// <summary>
				/// 	性别	 
				/// </summary>
				public string gend { get; set; }
				/// <summary>
				/// 	民族	 
				/// </summary>
				public string naty { get; set; }
				/// <summary>
				/// 	出生日期	 
				/// </summary>
				public string brdy { get; set; }
				/// <summary>
				/// 	年龄	 
				/// </summary>
				public string age { get; set; }
				/// <summary>
				/// 	险种类型	 
				/// </summary>
				public string insutype { get; set; }
				/// <summary>
				/// 	人员类别	 
				/// </summary>
				public string psn_type { get; set; }
				/// <summary>
				/// 	参保机构医保区划	 
				/// </summary>
				public string insu_optins { get; set; }
				/// <summary>
				/// 	单位名称	 
				/// </summary>
				public string emp_name { get; set; }
				/// <summary>
				/// 	医疗类别	 
				/// </summary>
				public string med_type { get; set; }
				/// <summary>
				/// 	住院/门诊号	 
				/// </summary>
				public string ipt_op_no { get; set; }
				/// <summary>
				/// 	病种编码	 
				/// </summary>
				public string dise_no { get; set; }
				/// <summary>
				/// 	病种名称	 
				/// </summary>
				public string dise_name { get; set; }
				/// <summary>
				/// 	发票号	 
				/// </summary>
				public string invono { get; set; }
				/// <summary>
				/// 	手术操作代码	 
				/// </summary>
				public string oprn_oprt_code { get; set; }
				/// <summary>
				/// 	手术操作名称	 
				/// </summary>
				public string oprn_oprt_name { get; set; }
				/// <summary>
				/// 	主要病情描述	 
				/// </summary>
				public string main_cond_desc { get; set; }
				/// <summary>
				/// 	诊断信息	 
				/// </summary>
				public string dise_info { get; set; }
				/// <summary>
				/// 	科室编码	 
				/// </summary>
				public string adm_dept_code { get; set; }
				/// <summary>
				/// 	科室名称	 
				/// </summary>
				public string adm_dept_name { get; set; }
				/// <summary>
				/// 	入院床位	 
				/// </summary>
				public string adm_bed { get; set; }
				/// <summary>
				/// 	主诊断代码	 
				/// </summary>
				public string dscg_dise_code { get; set; }
				/// <summary>
				/// 	主诊断名称	 
				/// </summary>
				public string dscg_dise_name { get; set; }


				/// <summary>
				/// 	关联流水号	 
				/// </summary>
				public string inpatientNo { get; set; }

				/// <summary>
				/// 	关联住院号	 
				/// </summary>
				public string patientNo { get; set; }


				/// <summary>
				/// 	上传状态
				/// </summary>
				public string updateState { get; set; }


				/// <summary>
				/// 	报销金额
				/// </summary>
				public string fund_pay_sumamt { get; set; }


				/// <summary>
				/// 	总金额
				/// </summary>
				public string medfee_sumamt { get; set; }


				/// <summary>
				/// 	开始时间
				/// </summary>
				public string begntime { get; set; }


				/// <summary>
				/// 	结束时间
				/// </summary>
				public string endtime { get; set; }
			}
		}

    }
}
