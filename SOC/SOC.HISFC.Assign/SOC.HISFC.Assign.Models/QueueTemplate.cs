using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.HISFC.Models.Base;
using System.Collections;

namespace FS.SOC.HISFC.Assign.Models
{
    /// <summary>
    /// [功能描述: 队列模板，继承Queue，增加星期]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2011-12]<br></br>
    /// </summary>
    [System.ComponentModel.DisplayName("分诊诊室")]
    [Serializable]
    public class QueueTemplate:Queue
    {
        private DayOfWeek weekday = DayOfWeek.Monday;

        [System.ComponentModel.DisplayName("星期几")]
        [System.ComponentModel.Description("星期几")]
        public DayOfWeek WeekDay
        {
            get
            {
                return weekday;
            }
            set
            {
                weekday = value;
            }
        }

        public new QueueTemplate Clone()
        {
            QueueTemplate qtemplate = base.Clone() as QueueTemplate;

            return qtemplate;
        }
    }
}
