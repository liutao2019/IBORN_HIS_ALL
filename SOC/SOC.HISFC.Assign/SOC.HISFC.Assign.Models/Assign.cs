using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.Assign.Models
{
    public class Assign : FS.HISFC.Models.Nurse.Assign
    {

        private FS.FrameWork.Models.NeuObject dept = null;

        /// <summary>
        /// 分诊科室
        /// </summary>
        [System.ComponentModel.DisplayName("分诊科室")]
        [System.ComponentModel.Description("分诊科室")]
        public FS.FrameWork.Models.NeuObject Dept
        {
            get
            {
                if (dept == null)
                {
                    dept = new FS.FrameWork.Models.NeuObject();
                }
                return dept;
            }
            set
            {
                dept = value;
            }
        }

        /// <summary>
        /// 队列信息
        /// </summary>
        private Queue queue = null;

        /// <summary>
        /// 分诊队列
        /// </summary>
        [System.ComponentModel.DisplayName("分诊队列")]
        [System.ComponentModel.Description("分诊队列")]
        public new Queue Queue
        {
            get
            {
                if (queue == null)
                {
                    queue = new Queue();
                    base.Queue = queue;
                }

                return queue;
            }
            set
            {
                base.Queue = value;
                queue = value;
            }
        }

        ///// <summary>
        ///// 病情
        ///// </summary>
        //private string patientConditionType;

        ///// <summary>
        ///// 病情 （危殆、危急、紧急、次紧急、非紧急）
        ///// </summary>
        //[System.ComponentModel.DisplayName("队列显示级别")]
        //[System.ComponentModel.Description("队列显示级别")]
        //public string PatientConditionType
        //{
        //    get
        //    {
        //        return patientConditionType;
        //    }
        //    set
        //    {
        //        patientConditionType = value;
        //    }
        //}

        /// <summary>
        /// 看诊序号
        /// </summary>
        private string seeNO = null;

        /// <summary>
        /// 看诊序号
        /// </summary>
        [System.ComponentModel.DisplayName("看诊序号")]
        [System.ComponentModel.Description("看诊序号")]
        public new string SeeNO
        {
            get
            {
                if (seeNO == null)
                {
                    seeNO = string.Empty;
                }
                return seeNO;
            }
            set
            {
                seeNO = value;
            }
        }
    }
}
