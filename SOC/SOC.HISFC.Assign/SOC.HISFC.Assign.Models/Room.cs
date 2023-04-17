using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.Assign.Models
{
    /// <summary>
    /// [功能描述: 诊室实体]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2011-12]<br></br>
    /// </summary>
    [System.ComponentModel.DisplayName("分诊诊室")]
    [Serializable]
    public class Room : FS.HISFC.Models.Nurse.Room
    {

        private FS.HISFC.Models.Base.OperEnvironment oper = null;

        [System.ComponentModel.DisplayName("分诊诊室操作环境")]
        [System.ComponentModel.Description("分诊诊室操作环境")]
        public FS.HISFC.Models.Base.OperEnvironment Oper
        {
            get
            {
                if (oper == null)
                {
                    oper = new FS.HISFC.Models.Base.OperEnvironment();
                }

                return oper;
            }
            set
            {
                oper = value;
            }
        }

        /// <summary>
        /// 克隆
        /// </summary>
        /// <returns></returns>
        public new Room Clone()
        {
            Room u = base.Clone() as Room;
            u.oper = this.Oper.Clone();

            return u;
        }
    }
}
