using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.Local.Order.OutPatientOrder.GYZL.Common
{
    /// <summary>
    /// lis试管
    /// </summary>
    public class LisContainerInfo
    {
        /// <summary>
        /// 容器(试管)代码
        /// </summary>
        private string containerCode;
        public string ContainerCode
        {
            get
            {
                return this.containerCode;
            }
            set
            {
                this.containerCode = value;
            }
        }

        /// <summary>
        /// 容器(试管)名称
        /// </summary>
        private string containerName;
        public string ContainerName
        {
            get
            {
                return this.containerName;
            }
            set
            {
                this.containerName = value;
            }
        }

        /// <summary>
        /// 该容器下包含的测试项
        /// </summary>
        private LisTestInfo[] testItems;
        public LisTestInfo[] TestItems
        {
            get
            {
                return this.testItems;
            }
            set
            {
                this.testItems = value;
            }
        }
    }
}
