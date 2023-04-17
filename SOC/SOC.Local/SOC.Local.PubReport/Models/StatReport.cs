using System;
using System.Collections.Generic;
using System.Text;

namespace FS.SOC.Local.PubReport.Models
{
    public class StatReport : FS.FrameWork.Models.NeuObject
    {
        public StatReport()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        /// <summary>
        /// 统计号
        /// </summary>
        public FS.FrameWork.Models.NeuObject stat = new FS.FrameWork.Models.NeuObject();
        /// <summary>
        /// 卡号
        /// </summary>
        public string Card_No;
        /// <summary>
        /// 克隆
        /// </summary>
        /// <returns></returns>
        public new StatReport Clone()
        {
            StatReport obj = base.MemberwiseClone() as StatReport;
            obj.stat = this.stat.Clone();
            return obj;
        }

    }
}
