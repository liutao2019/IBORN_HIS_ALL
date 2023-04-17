using System;

namespace FS.SOC.HISFC.BizLogic.DCP.Object
{
    /// <summary>
    /// ReasonOfNot<br></br>
    /// [功能描述: ReasonOfNot不报卡原因]<br></br>
    /// [创 建 者: yeph]<br></br>
    /// [创建时间: 2014-12-31]<br></br>
    /// <修改记录 
    ///		修改人='' 
    ///		修改时间='yyyy-mm-dd' 
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    [System.Serializable]
    public class ReasonOfNot : FS.FrameWork.Models.NeuObject
	{
        #region 私有字段

        /// <summary>
        /// 报卡编号
        /// </summary>
        private string diagName = "";

		/// <summary>
		/// 报告卡类型（C门诊 I住院 O其它）
		/// </summary>
		private string patientType = "";

		/// <summary>
		/// 患者信息
		/// </summary>
        private FS.HISFC.Models.RADT.Patient patient = new FS.HISFC.Models.RADT.Patient();

		/// <summary>
		/// 不报卡原因
		/// </summary>
		private string reasonOfNot = "";

		/// <summary>
		/// 其他外院名称 
		/// </summary>
		private string otherName = "";

		
		/// <summary>
		/// 报卡人
		/// </summary>
		private FS.FrameWork.Models.NeuObject reportDoctor = new FS.FrameWork.Models.NeuObject();

		/// <summary>
		/// 报卡科室
		/// </summary>
		private FS.FrameWork.Models.NeuObject doctorDept = new FS.FrameWork.Models.NeuObject();

		/// <summary>
		/// 报卡时间
		/// </summary>
		private System.DateTime reportTime;

		
        #endregion

        #region 共有字段

        /// <summary>
        /// 报卡编号
        /// </summary>
        public string DiagName
        {
            get
            {
                return this.diagName;
            }
            set 
            {
                this.diagName = value;
            }
        }

		/// <summary>
		/// 报告卡类型（C门诊 I住院 O其它）
		/// </summary>
        public string PatientType
        {
            get
            {
                return this.patientType;
            }
            set
            {
                this.patientType = value;
            }
        }

		/// <summary>
		/// 患者信息
		/// </summary>
        public FS.HISFC.Models.RADT.Patient Patient
        {
            get
            {
                return this.patient;
            }
            set
            {
                this.patient = value;
            }
        }

		/// <summary>
		/// 不报卡原因
		/// </summary>
        public string ReasonOfNot1
        {
            get
            {
                return this.reasonOfNot;
            }
            set
            {
                this.reasonOfNot = value;
            }
        }

		/// <summary>
		/// 其他外院名称
		/// </summary>
        public string OtherName
        {
            get
            {
                return this.otherName;
            }
            set
            {
                this.otherName = value;
            }
        }

	
		/// <summary>
		/// 报卡人
		/// </summary>
        public FS.FrameWork.Models.NeuObject ReportDoctor
        {
            get
            {
                return this.reportDoctor;
            }
            set
            {
                this.reportDoctor = value;
            }
        }

		/// <summary>
		/// 报卡科室
		/// </summary>
        public FS.FrameWork.Models.NeuObject DoctorDept
        {
            get
            {
                return this.doctorDept;
            }
            set 
            {
                this.doctorDept = value;
            }
        }

		/// <summary>
		/// 报卡时间
		/// </summary>
        public System.DateTime ReportTime
        {
            get            
            {
                return this.reportTime;
            }
            set 
            {
                this.reportTime = value;
            }
        }

		
        #endregion

        public ReasonOfNot()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
        }

        #region 克隆函数
        public new ReasonOfNot Clone()
        {
            ReasonOfNot commonReport = base.Clone() as ReasonOfNot;
            commonReport.patient = this.patient.Clone();
            commonReport.doctorDept = this.doctorDept.Clone();
            commonReport.reportDoctor = this.reportDoctor.Clone();



            return commonReport;
        }
        #endregion
    }

   
}
