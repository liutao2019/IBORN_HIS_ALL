using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.Local.Order.OutPatientOrder.GYZL.Common
{
    public class LisSeparationTube
    {
        /// <summary>
        /// 性别， (M—男; F—女; O—未知) (必填)
        /// </summary>
        private string sexId = string.Empty;

        public string SexId
        {
            get
            {
                return this.sexId;
            }
            set
            {
                this.sexId = value;
            }
        }

        /// <summary>
        /// 院区ID，用硬代码(院本部— 00001) (必填)
        /// </summary>
        private string hospitalId = string.Empty;
        public string HospitalId
        {
            get
            {
                return this.hospitalId;
            }
            set
            {
                this.hospitalId = value;
            }
        }

        /// <summary>
        /// 委托实验室(可空)
        /// </summary>
        private string referralLabId = string.Empty;
        public string ReferralLabId
        {
            get
            {
                return this.referralLabId;
            }
            set
            {
                this.referralLabId = value;
            }
        }

        /// <summary>
        /// 测试项目代码(必填)
        /// </summary>
        private string testId = string.Empty;
        public string TestId
        {
            get
            {
                return this.testId;
            }
            set
            {
                this.testId = value;
            }
        }

        /// <summary>
        /// 检验项目优先级别(R—普通,S—紧急,T—优先) (必填)
        /// </summary>
        private string priorityId = string.Empty;
        public string PriorityId
        {
            get
            {
                return this.priorityId;
            }
            set
            {
                this.priorityId = value;
            }
        }

        /// <summary>
        /// 样本类型(可空，空值时，取LIS的默认标本类型)
        /// </summary>
        private string specimenTypeId = string.Empty;
        public string SpecimenTypeId
        {
            get
            {
                return this.specimenTypeId;
            }
            set
            {
                this.specimenTypeId = value;
            }
        }
    }
}
