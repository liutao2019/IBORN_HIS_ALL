using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Response
{
    /// <summary>
    /// 报告明细信息查询
    /// </summary>
    public class ResponseGzsiModel5402:ResponseBase
    {
        #region 结算分类信息checkreportdetails
        public List<Checkreportdetails> checkreportdetails { get; set; }
        public class Checkreportdetails
        {
            /// <summary>
            ///人员编号
            /// <summary>
            public string psn_no { get; set; }

            /// <summary>
            ///报告单号
            /// <summary>
            public string rpotc_no { get; set; }

            /// <summary>
            ///报告日期
            /// <summary>
            public string rpt_date { get; set; }

            /// <summary>
            ///报告单类别代码
            /// <summary>
            public string rpotc_type_code { get; set; }

            /// <summary>
            ///检查报告单名称
            /// <summary>
            public string exam_rpotc_name { get; set; }

            /// <summary>
            ///检查结果阳性标志
            /// <summary>
            public string exam_rslt_poit_flag { get; set; }

            /// <summary>
            ///检查异常标志
            /// <summary>
            public string exam_rslt_abn { get; set; }

            /// <summary>
            ///检查结论
            /// <summary>
            public string exam_ccls { get; set; }

            /// <summary>
            ///报告医师
            /// <summary>
            public string bilgDrName { get; set; }
        }
        #endregion

        #region 检验报告信息inspectionreportinformation
        public List<Inspectionreportinformation> inspectionreportinformation { get; set; }
        public class Inspectionreportinformation
        {
            public string psn_no { get; set; }//人员编号
            public string rpotc_no { get; set; }//报告单号
            public string exam_item_code { get; set; }//检验-项目代码
            public string exam_item_name { get; set; }//检验-项目名称
            public string rpt_date { get; set; }//报告日期
            public string rpot_doc { get; set; }//报告医师

        }
        #endregion

        #region 检验明细信息
        public List<Inspectiondetails> inspectiondetails { get; set; }
        public class Inspectiondetails
        {
            public string rpotc_no { get; set; }//报告单号
            public string exam_mtd { get; set; }//检验方法
            public string ref_val { get; set; }//参考值
            public string exam_unt { get; set; }//检验-计量单位
            public string exam_rslt_val { get; set; }//检验-结果(数值)
            public string exam_rslt_qual { get; set; }//检验 - 结果(定性)
            public string exam_item_detl_code { get; set; }//检验 - 项目明细代码
            public string exam_item_detl_name { get; set; }//检验 - 项目明细名称
            public string exam_rslt_abn { get; set; }//检查 / 检验结果异常标识
        }
        #endregion
    }
}
