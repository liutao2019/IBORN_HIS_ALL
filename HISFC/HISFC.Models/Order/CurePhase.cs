using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.Models.Order
{
    /// <summary>
    /// FS.HISFC.Models.Order.CurePhase<br></br>
    /// [功能描述: 治疗阶段信息实体]<br></br>
    /// [创 建 者: Sunm]<br></br>
    /// [创建时间: 2007-08-23]<br></br>
    /// <修改记录
    ///		修改人=''
    ///		修改时间='yyyy-mm-dd'
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    [Serializable]
    public class CurePhase : FS.FrameWork.Models.NeuObject
    {
        public CurePhase()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region 变量

        /// <summary>
        /// 住院流水号
        /// </summary>
        private string patientID;

        /// <summary>
        /// 科室信息
        /// </summary>
        private FS.FrameWork.Models.NeuObject dept = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// 治疗阶段信息
        /// </summary>
        private FS.FrameWork.Models.NeuObject curePhaseInfo = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// 开始时间
        /// </summary>
        private DateTime startTime;

        /// <summary>
        /// 结束时间
        /// </summary>
        private DateTime endTime;

        /// <summary>
        /// 开立医生
        /// </summary>
        private FS.FrameWork.Models.NeuObject doctor = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// 备注
        /// </summary>
        private string remark;

        /// <summary>
        /// 有效性
        /// </summary>
        private bool isVaild;

        /// <summary>
        /// 操作信息
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment oper = new FS.HISFC.Models.Base.OperEnvironment();

        #endregion

        #region 属性

        /// <summary>
        /// 住院流水号
        /// </summary>
        public string PatientID
        {
            get { return this.patientID; }
            set { this.patientID = value; }
        }

        /// <summary>
        /// 科室信息
        /// </summary>
        public FS.FrameWork.Models.NeuObject Dept
        {
            get { return this.dept; }
            set { this.dept = value; }
        }

        /// <summary>
        /// 治疗阶段信息
        /// </summary>
        public FS.FrameWork.Models.NeuObject CurePhaseInfo
        {
            get { return this.curePhaseInfo; }
            set { this.curePhaseInfo = value; }
        }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime
        {
            get { return this.startTime; }
            set { this.startTime = value; }
        }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime
        {
            get { return this.endTime; }
            set { this.endTime = value; }
        }

        /// <summary>
        /// 开立医生
        /// </summary>
        public FS.FrameWork.Models.NeuObject Doctor
        {
            get { return this.doctor; }
            set { this.doctor = value; }
        }

        /// <summary>
        /// 有效性
        /// </summary>
        public bool IsVaild
        {
            get { return this.isVaild; }
            set { this.isVaild = value; }
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get { return this.remark; }
            set { this.remark = value; }
        }

        /// <summary>
        /// 操作信息
        /// </summary>
        public FS.HISFC.Models.Base.OperEnvironment Oper
        {
            get { return this.oper; }
            set { this.oper = value; }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 克隆
        /// </summary>
        /// <returns></returns>
        public new CurePhase Clone()
        {
            // TODO:  添加 CurePhase.Clone 实现
            CurePhase obj = base.Clone() as CurePhase;
            
            obj.oper = this.oper.Clone();

            return obj;
        }

        #endregion

        
    }
}
