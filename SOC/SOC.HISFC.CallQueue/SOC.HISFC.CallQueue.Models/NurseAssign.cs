using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.CallQueue.Models
{
    /// <summary>
    /// 叫号队列信息
    /// </summary>
    [Serializable]
    public class NurseAssign : FS.FrameWork.Models.NeuObject
    {
        private string patientID = string.Empty;
        /// <summary>
        /// 患者的挂号信息
        /// </summary>
        public string PatientID
        {
            get
            {
                return patientID;
            }
            set
            {
                patientID = value;
            }
        }

        private string patientCardNO = string.Empty;
        /// <summary>
        /// 患者就诊号
        /// </summary>
        public string PatientCardNO
        {
            get
            {
                return patientCardNO;
            }
            set
            {
                patientCardNO = value;
            }
        }

        private string patientName = string.Empty;
        /// <summary>
        /// 患者名称
        /// </summary>
        public string PatientName
        {
            get
            {
                return patientName;
            }
            set
            {
                patientName = value;
            }
        }

        private string patientSex = string.Empty;
        /// <summary>
        /// 患者性别
        /// </summary>
        public string PatientSex
        {
            get
            {
                return patientSex;
            }
            set
            {
                patientSex = value;
            }
        }

        private string patientSeeNO = string.Empty;
        /// <summary>
        /// 患者看诊序号
        /// </summary>
        public string PatientSeeNO
        {
            get
            {
                return patientSeeNO;
            }
            set
            {
                patientSeeNO = value;
            }
        }

        private FS.FrameWork.Models.NeuObject dept = null;
        /// <summary>
        /// 看诊科室
        /// </summary>
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

        private FS.FrameWork.Models.NeuObject nurse = null;
        /// <summary>
        /// 分诊护士站
        /// </summary>
        public FS.FrameWork.Models.NeuObject Nurse
        {
            get
            {
                if (nurse == null)
                {
                    nurse = new FS.FrameWork.Models.NeuObject();
                }

                return nurse;
            }
            set
            {
                nurse = value;
            }
        }

        private FS.FrameWork.Models.NeuObject room = null;
        /// <summary>
        /// 诊室
        /// </summary>
        public FS.FrameWork.Models.NeuObject Room
        {
            get
            {
                if (room == null)
                {
                    room = new FS.FrameWork.Models.NeuObject();
                }
                return room;
            }
            set
            {
                room = value;
            }
        }

        private FS.FrameWork.Models.NeuObject console = null;
        /// <summary>
        /// 诊台
        /// </summary>
        public FS.FrameWork.Models.NeuObject Console
        {
            get
            {
                if (console == null)
                {
                    console = new FS.FrameWork.Models.NeuObject();
                }
                return console;
            }
            set
            {
                console = value;
            }
        }

        private FS.HISFC.Models.Base.Noon noon = null;
        public FS.HISFC.Models.Base.Noon Noon
        {
            get
            {
                if (noon == null)
                {
                    noon = new FS.HISFC.Models.Base.Noon();
                }

                return noon;
            }
            set
            {
                noon = value;
            }
        }

        private string callClass=string.Empty;
        /// <summary>
        /// 叫号类型
        /// </summary>
        public string CallClass
        {
            get
            {
                return callClass;
            }
            set
            {
                callClass = value;
            }
        }

        private FS.HISFC.Models.Base.OperEnvironment oper=null;
        /// <summary>
        /// 操作员
        /// </summary>
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
        public new NurseAssign Clone()
        {
            NurseAssign c = base.Clone() as NurseAssign;
            c.dept = this.Dept.Clone();
            c.room = this.Room.Clone();
            c.console = this.Console.Clone();
            c.oper = this.Oper.Clone();
            c.nurse = this.Nurse.Clone();
            c.noon = this.Noon.Clone();

            return c;
        }
    }
}
