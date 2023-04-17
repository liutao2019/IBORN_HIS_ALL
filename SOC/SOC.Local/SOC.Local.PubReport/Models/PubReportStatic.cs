using System;
using System.Collections.Generic;
using System.Text;

namespace FS.SOC.Local.PubReport.Models
{
    /// <summary>
    /// 公费汇总统计
    /// </summary>
    public class PubReportStatic:FS.FrameWork.Models.NeuObject
    {
        public PubReportStatic()
        {
            InPub.User01 = "0";//住院天数
        }
        /// <summary>
        /// 门诊统计部分
        /// </summary>
        public PubReport ClinicPub = new PubReport();
        /// <summary>
        /// 住院统计部分
        /// </summary>
        public PubReport InPub = new PubReport();

        public new PubReportStatic Clone()
        {
            PubReportStatic obj = base.Clone() as PubReportStatic;
            obj.ClinicPub = this.ClinicPub.Clone();
            obj.InPub = this.InPub.Clone();
            return obj;
        }

    }
}
