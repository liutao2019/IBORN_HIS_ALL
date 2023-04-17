using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.Local.Order.OutPatientOrder.GYZL.Common
{
    /// <summary>
    /// 该容器(试管)下包含的测试项
    /// </summary>
    public class LisTestInfo
    {
        /// <summary>
        /// 测试项目代码
        /// </summary>
        private string testCode;
        public string TestCode
        {
            get
            {
                return this.testCode;
            }
            set
            {
                this.testCode = value;
            }
        }

        /// <summary>
        /// 测试项目名称(描述)
        /// </summary>
        private string testDesc;
        public string TestDesc
        {
            get
            {
                return this.testDesc;
            }
            set
            {
                this.testDesc = value;
            }
        }

        /// <summary>
        /// 采集时间(分钟)
        /// </summary>
        private int collectTime;
        public int CollectTime
        {
            get
            {
                return this.collectTime;
            }
            set
            {
                this.collectTime = value;
            }
        }
    }
}
