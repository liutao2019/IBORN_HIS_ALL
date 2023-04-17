using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SOC.Local.Order.SubFeeSet.GYZL.LisGetTubeSub
{
    /// <summary>
    /// 试管数、每个试管所包含的检验项目、采集次数
    /// </summary>
    public class LisTubeGroupInfo
    {
        /// <summary>
        /// 采集次数
        /// </summary>
        int collectTimes = 0;
        public int CollectTimes
        {
            get
            {
                return this.collectTimes;
            }
            set
            {
                this.collectTimes = value;
            }
        }

        /// <summary>
        /// 容器(试管)信息,  数组长度是容器数量
        /// </summary>
        private LisContainerInfo[] containers;
        public LisContainerInfo[] Containers
        {
            get
            {
                return this.containers;
            }
            set
            {
                this.containers = value;
            }
        }
    }
}
