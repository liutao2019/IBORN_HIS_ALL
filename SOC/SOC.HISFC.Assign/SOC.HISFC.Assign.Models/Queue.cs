using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.Assign.Models
{
    /// <summary>
    /// [功能描述: 队列实体，增加挂号级别，队列类型，并且将队列类型变成枚举]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2011-12]<br></br>
    /// </summary>
    [System.ComponentModel.DisplayName("分诊队列")]
    [Serializable]
    public class Queue : FS.HISFC.Models.Nurse.Queue
    {
        private FS.FrameWork.Models.NeuObject regLevel = null;
        
        /// <summary>
        /// 挂号级别
        /// </summary>
        [System.ComponentModel.DisplayName("挂号级别")]
        [System.ComponentModel.Description("挂号级别")]
        public FS.FrameWork.Models.NeuObject RegLevel
        {
            get
            {
                if (regLevel == null)
                {
                    regLevel = new FS.FrameWork.Models.NeuObject();
                }

                return regLevel;
            }
            set
            {
                regLevel = value;
            }
        }

        private EnumQueueType queueType =  EnumQueueType.Custom;

        /// <summary>
        /// 队列类型
        /// </summary>
        [System.ComponentModel.DisplayName("队列类型")]
        [System.ComponentModel.Description("队列类型")]
        public EnumQueueType QueueType
        {
            get
            {
                return queueType;
            }
            set
            {
                queueType = value;
            }
        }

        /// <summary>
        /// 分诊护士站（已作废，请使用AssignNurse）
        /// </summary>
        [System.ComponentModel.DisplayName("分诊护士站")]
        [System.ComponentModel.Description("分诊护士站（已作废，请使用AssignNurse）")]
        [Obsolete("Dept已作废，请使用AssignNurse", true)]
        public new FS.FrameWork.Models.NeuObject Dept
        {
            get
            {
                return this.assignNurse;
            }
            set
            {
                this.assignNurse = value;
            }
        }

        private FS.FrameWork.Models.NeuObject assignNurse = null;
        /// <summary>
        /// 分诊护士站
        /// </summary>
        [System.ComponentModel.DisplayName("分诊护士站")]
        [System.ComponentModel.Description("分诊护士站")]
        public FS.FrameWork.Models.NeuObject AssignNurse
        {
            get
            {
                if (assignNurse == null)
                {
                    assignNurse = new FS.FrameWork.Models.NeuObject();
                }

                return assignNurse;
            }
            set
            {
                assignNurse = value;
                base.AssignDept = assignNurse;
            }
        }

        /// <summary>
        /// 是否专家（已作废，请使用IsExpert）
        /// </summary>
        [System.ComponentModel.DisplayName("是否专家")]
        [System.ComponentModel.Description("是否专家（已作废，请使用IsExpert）")]
        [Obsolete("ExpertFlag已作废，请使用IsExpert",true)]
        public new string ExpertFlag
        {
            get
            {
                return base.ExpertFlag;
            }
            set
            {
                base.ExpertFlag = value;
            }
        }

        private bool isExpert = false;
        [System.ComponentModel.DisplayName("是否专家")]
        [System.ComponentModel.Description("是否专家")]
        public bool IsExpert
        {
            get
            {
                return isExpert;
            }
            set
            {
                isExpert = value;
                base.ExpertFlag = FS.FrameWork.Function.NConvert.ToInt32(value).ToString();
            }
        }

        /// <summary>
        /// 病情
        /// </summary>
        private string patientConditionType;

        /// <summary>
        /// 病情 （危殆、危急、紧急、次紧急、非紧急）
        /// </summary>
        [System.ComponentModel.DisplayName("队列显示级别")]
        [System.ComponentModel.Description("队列显示级别")]
        public string PatientConditionType
        {
            get
            {
                return patientConditionType;
            }
            set
            {
                patientConditionType = value;
            }
        }

        public new Queue Clone()
        {
            Queue queue = base.Clone() as Queue;
            queue.regLevel = this.RegLevel.Clone();
            queue.assignNurse = this.AssignNurse.Clone();
            return queue;
        }
    }
}
