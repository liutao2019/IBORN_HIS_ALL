using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Request
{
    /// <summary>
    /// 科室上传
    /// </summary>
    public class RequestGzsiModel3401
    {
        public List<Deptinfo> deptinfo { get; set; }

        public class Deptinfo
        {
            /// <summary>
            ///医院科室编码 Y 
            /// <summary>
            public string hosp_dept_codg { get; set; }

            /// <summary>
            ///科别 Y 
            /// <summary>
            public string caty { get; set; }

            /// <summary>
            ///医院科室名称 Y 
            /// <summary>
            public string hosp_dept_name { get; set; }

            /// <summary>
            ///开始时间 Y 
            /// <summary>
            public string begntime { get; set; }

            /// <summary>
            ///结束时间  
            /// <summary>
            public string endtime { get; set; }

            /// <summary>
            ///简介 Y 
            /// <summary>
            public string itro { get; set; }

            /// <summary>
            ///科室负责人姓名 Y 
            /// <summary>
            public string dept_resper_name { get; set; }

            /// <summary>
            ///科室负责人电话 Y 
            /// <summary>
            public string dept_resper_tel { get; set; }

            /// <summary>
            ///是否医技科室  
            /// <summary>
            public string medtech_flag { get; set; }

            /// <summary>
            ///科室医疗服务范围  
            /// <summary>
            public string dept_med_serv_scp { get; set; }

            /// <summary>
            ///科室成立日期 Y 
            /// <summary>
            public string dept_estbdat { get; set; }

            /// <summary>
            ///医疗服务类型  
            /// <summary>
            public string medserv_type { get; set; }

            /// <summary>
            ///诊疗科目类别 Y 
            /// <summary>
            public string dept_type { get; set; }

            /// <summary>
            ///批准床位数量 Y 
            /// <summary>
            public string aprv_bed_cnt { get; set; }

            /// <summary>
            ///医保认可床位数  
            /// <summary>
            public string hi_crtf_bed_cnt { get; set; }

            /// <summary>
            ///统筹区编号 Y 
            /// <summary>
            public string poolarea_no { get; set; }

            /// <summary>
            ///医师人数 Y 
            /// <summary>
            public string dr_psncnt { get; set; }

            /// <summary>
            ///药师人数 Y 
            /// <summary>
            public string phar_psncnt { get; set; }

            /// <summary>
            ///护士人数 Y 
            /// <summary>
            public string nurs_psncnt { get; set; }

            /// <summary>
            ///技师人数 Y 
            /// <summary>
            public string tecn_psncnt { get; set; }

            /// <summary>
            ///备注  
            /// <summary>
            public string memo { get; set; }
        }
    }
    
}
