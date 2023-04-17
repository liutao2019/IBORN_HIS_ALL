using System;

namespace FS.HISFC.DCP.Object
{
    /// <summary>
    /// CommonReport<br></br>
    /// [功能描述: CommonReport疾病控制预防报卡实体]<br></br>
    /// [创 建 者: zengft]<br></br>
    /// [创建时间: 2008-8-20]<br></br>
    /// <修改记录 
    ///		修改人='' 
    ///		修改时间='yyyy-mm-dd' 
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    [System.Serializable]
    public class CommonReport : FS.FrameWork.Models.NeuObject
	{
        #region 私有字段

        /// <summary>
        /// 报卡编号
        /// </summary>
        private string reportNO = "";

		/// <summary>
		/// 报告卡类型（C门诊 I住院 O其它）
		/// </summary>
		private string patientType = "";

		/// <summary>
		/// 患者信息
		/// </summary>
        private FS.HISFC.Models.RADT.Patient patient = new FS.HISFC.Models.RADT.Patient();

		/// <summary>
		/// 家长姓名
		/// </summary>
		private string patientParents = "";

		/// <summary>
		/// 年龄单位
		/// </summary>
		private string ageUnit = "";

		/// <summary>
		/// 患者来源
		/// </summary>
		private string homeArea = "";

		/// <summary>
		/// 现住址-省
		/// </summary>
		private FS.FrameWork.Models.NeuObject homeProvince = new FS.FrameWork.Models.NeuObject();

		/// <summary>
		/// 现住址-市
		/// </summary>
		private FS.FrameWork.Models.NeuObject homeCity = new FS.FrameWork.Models.NeuObject();

		/// <summary>
		/// 现住址-县（区）
		/// </summary>
        private FS.FrameWork.Models.NeuObject homeCouty = new FS.FrameWork.Models.NeuObject();

		/// <summary>
		/// 现住址-镇（乡、街道）
		/// </summary>
		private FS.FrameWork.Models.NeuObject homeTown = new FS.FrameWork.Models.NeuObject();

		/// <summary>
		/// 患者科室（住院科室、看诊科室）
		/// </summary>
		private FS.FrameWork.Models.NeuObject patientDept = new FS.FrameWork.Models.NeuObject();

		/// <summary>
		/// 疾病（ID编码 Name名称 Memo分类标志）
		/// </summary>
		private FS.FrameWork.Models.NeuObject disease = new FS.FrameWork.Models.NeuObject();

		/// <summary>
		/// 发病日期
		/// </summary>
		private System.DateTime infectDate;

		/// <summary>
		/// 诊断日期
		/// </summary>
		private System.DateTime diagnosisTime;

		/// <summary>
		/// 死亡日期
		/// </summary>
		private System.DateTime deadDate;

		/// <summary>
		/// 病例分类
		/// </summary>
		private FS.FrameWork.Models.NeuObject caseClass1 = new FS.FrameWork.Models.NeuObject();

		/// <summary>
		/// 病例分类（0急性、1慢性、2未分型）
		/// </summary>
		private string caseClass2 = "";

		/// <summary>
		/// 有无接触其他（1有、0无）
		/// </summary>
		private string infectOtherFlag = "";

		/// <summary>
		/// 报告卡状态（0新加、1合格、2不合格、3报告人作废、4保健科作废）
		/// </summary>
        private string state = "";//enumReportState.新加;

		/// <summary>
		/// 是否有附卡（1是 0否）
		/// </summary>
		private string addtionFlag = "";

        /// <summary>
        /// 性别标记
        /// </summary>
        private string sexDiseaseFlag = "";

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

		/// <summary>
		/// 作废人
		/// </summary>
		private FS.FrameWork.Models.NeuObject cancelOper = new FS.FrameWork.Models.NeuObject();

		/// <summary>
		/// 作废时间
		/// </summary>
		private System.DateTime cancelTime;

		/// <summary>
		/// 修改人
		/// </summary>
		private FS.FrameWork.Models.NeuObject modifyOper = new FS.FrameWork.Models.NeuObject();

		/// <summary>
		/// 修改时间
		/// </summary>
		private System.DateTime modifyTime;

		/// <summary>
		/// 审核人
		/// </summary>
		private FS.FrameWork.Models.NeuObject approveOper = new FS.FrameWork.Models.NeuObject();

		/// <summary>
		/// 审核时间
		/// </summary>
		private System.DateTime approveTime;

        /// <summary>
        /// 订正标志[1已经订正，0未订正]
        /// </summary>
        private string correctFlag = "";

        /// <summary>
        /// 订正卡ID
        /// </summary>
        private string correctReportNO = "";

        /// <summary>
        /// 订正卡的原卡ID
        /// </summary>
        private string correctedReportNO = "";

		/// <summary>
		/// 操作人
		/// </summary>
		private FS.FrameWork.Models.NeuObject oper = new FS.FrameWork.Models.NeuObject();

		/// <summary>
		/// 操作科室
		/// </summary>
		private FS.FrameWork.Models.NeuObject operDept = new FS.FrameWork.Models.NeuObject();

		/// <summary>
		/// 操作时间
		/// </summary>
		private System.DateTime operTime;
		
		/// <summary>
		/// 事由（记录操作原因）
		/// </summary>
		private string operCase = "";

		/// <summary>
		/// 扩展信息
		/// </summary>
		private string extendInfo1 = "";

		/// <summary>
		/// 扩展信息
		/// </summary>
		private string extendInfo2 = "";

		/// <summary>
		/// 扩展信息
		/// </summary>
		private string extendInfo3 = "";
        /// <summary>
        /// 肿瘤卡标志
        /// </summary>
        private string cancer_Flag;
        /// <summary>
        /// 肿瘤卡编号
        /// </summary>
        private string cancer_no = "";
        #endregion

        #region 共有字段

        /// <summary>
        /// 报卡编号
        /// </summary>
        public string ReportNO
        {
            get
            {
                return this.reportNO;
            }
            set 
            {
                this.reportNO = value;
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
		/// 家长姓名
		/// </summary>
        public string PatientParents
        {
            get
            {
                return this.patientParents;
            }
            set
            {
                this.patientParents = value;
            }
        }

		/// <summary>
		/// 年龄单位
		/// </summary>
        public string AgeUnit
        {
            get
            {
                return this.ageUnit;
            }
            set
            {
                this.ageUnit = value;
            }
        }

		/// <summary>
		/// 患者来源
		/// </summary>
        public string HomeArea
        {
            get
            {
                return this.homeArea;
            }
            set
            {
                this.homeArea = value;
            }
        }

		/// <summary>
		/// 现住址-省
		/// </summary>
        public FS.FrameWork.Models.NeuObject HomeProvince
        {
            get
            {
                return this.homeProvince;
            }
            set 
            {
                this.homeProvince = value;
            }
        }

		/// <summary>
		/// 现住址-市
		/// </summary>
        public FS.FrameWork.Models.NeuObject HomeCity
        {
            get 
            {
                return this.homeCity;
            }
            set
            {
                this.homeCity = value;
            }
        }

		/// <summary>
		/// 现住址-县（区）
		/// </summary>
        public FS.FrameWork.Models.NeuObject HomeCouty
        {
            get
            {
                return this.homeCouty;
            }
            set
            {
                this.homeCouty = value;
            }
        }

		/// <summary>
		/// 现住址-镇（乡、街道）
		/// </summary>
        public FS.FrameWork.Models.NeuObject HomeTown
        {
            get
            {
                return this.homeTown;
            }
            set 
            {
                this.homeTown = value;
            }
        }

		/// <summary>
		/// 患者科室（住院科室、看诊科室）
		/// </summary>
        public FS.FrameWork.Models.NeuObject PatientDept
        {
            get
            {
                return this.patientDept;
            }
            set
            {
                this.patientDept = value;
            }
        }

		/// <summary>
		/// 疾病（ID编码 Name名称 Memo分类标志）
		/// </summary>
        public FS.FrameWork.Models.NeuObject Disease
        {
            get
            {
                return this.disease;
            }
            set
            {
                this.disease = value;
            }
        }

		/// <summary>
		/// 发病日期
		/// </summary>
        public System.DateTime InfectDate
        {
            get
            {
                return this.infectDate;
            }
            set
            {
                this.infectDate = value;
            }
        }

		/// <summary>
		/// 诊断时间
		/// </summary>
        public System.DateTime DiagnosisTime
        {
            get
            {
                return this.diagnosisTime;
            }
            set 
            {
                this.diagnosisTime = value;
            }
        }

		/// <summary>
		/// 死亡日期
		/// </summary>
        public System.DateTime DeadDate
        {
            get 
            {
                return this.deadDate;
            }
            set 
            {
                this.deadDate = value;
            }
        }

		/// <summary>
		/// 病例分类
		/// </summary>
        public FS.FrameWork.Models.NeuObject CaseClass1
        {
            get
            {
                return this.caseClass1;
            }
            set
            {
                this.caseClass1 = value;
            }
        }

		/// <summary>
		/// 病例分类（0急性、1慢性、2未分型）
		/// </summary>
		public string CaseClass2
        {
            get
            {
                return this.caseClass2;
            }
            set
            {
                this.caseClass2 = value;
            }
        }

		/// <summary>
		/// 有无接触其他（1有、0无）
		/// </summary>
		public string InfectOtherFlag
        {
            get
            {
                return this.infectOtherFlag;
            }
            set
            {
                this.infectOtherFlag = value;
            }
        }

		/// <summary>
		/// 报告卡状态（0新加、1合格、2不合格、3报告人作废、4保健科作废）
		/// </summary>
        public string State
        {
            get
            {
                return this.state;
            }
            set
            {
                this.state = value;
            }
        }

		/// <summary>
		/// 是否有附卡（1是 0否）
		/// </summary>
        public string AddtionFlag
        {
            get
            {
                return this.addtionFlag;
            }
            set
            {
                this.addtionFlag = value;
            }
        }

        /// <summary>
        /// 是否性病（1是 0否）
        /// </summary>
        public string SexDiseaseFlag
        {
            get
            {
                return this.sexDiseaseFlag;
            }
            set
            {
                this.sexDiseaseFlag = value;
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

		/// <summary>
		/// 作废人
		/// </summary>
        public FS.FrameWork.Models.NeuObject CancelOper
        {
            get
            {
                return this.cancelOper;
            }
            set
            {
                this.cancelOper = value;
            }
        }

		/// <summary>
		/// 作废时间
		/// </summary>
        public System.DateTime CancelTime
        {
            get
            {
                return this.cancelTime;
            }
            set
            {
                this.cancelTime = value;
            }
        }

		/// <summary>
		/// 修改人
		/// </summary>
        public FS.FrameWork.Models.NeuObject ModifyOper
        {
            get
            {
                return this.modifyOper;
            }
            set
            {
                this.modifyOper = value;
            }
        }

		/// <summary>
		/// 修改时间
		/// </summary>
        public System.DateTime ModifyTime
        {
            get
            {
                return  this.modifyTime;
            }
            set
            {
                this.modifyTime = value;
            }
        }

		/// <summary>
		/// 审核人
		/// </summary>
        public FS.FrameWork.Models.NeuObject ApproveOper
        {
            get
            {
                return this.approveOper;
            }
            set
            {
                this.approveOper = value;
            }
        }

		/// <summary>
		/// 审核时间
		/// </summary>
        public System.DateTime ApproveTime
        {
            get
            {
                return this.approveTime;
            }
            set
            {
                this.approveTime = value;
            }
        }

        /// <summary>
        /// 是否已经订正[1订正，0未订正]
        /// </summary>
        public string CorrectFlag
        {
            get
            {
                return this.correctFlag;
            }
            set 
            {
                this.correctFlag = value;
            }
        }

        /// <summary>
        /// 订正卡ID
        /// </summary>
        public string CorrectReportNO
        {
            get
            {
                return this.correctReportNO;
            }
            set
            {
                this.correctReportNO = value;
            }
        }

        /// <summary>
        /// 订正卡的原卡ID
        /// </summary>
        public string CorrectedReportNO
        {
            get
            {
                return this.correctedReportNO;
            }
            set
            {
                this.correctedReportNO = value;
            }
        }

		/// <summary>
		/// 操作人
		/// </summary>
        public FS.FrameWork.Models.NeuObject Oper
        {
            get 
            {
                return this.oper;
            }
            set 
            {
                this.oper = value;
            }
        }

		/// <summary>
		/// 操作科室
		/// </summary>
        public FS.FrameWork.Models.NeuObject OperDept
        {
            get
            {
                return this.operDept;
            }
            set
            {
                this.operDept = value;
            }
        }

		/// <summary>
		/// 操作时间
		/// </summary>
        public System.DateTime OperTime
        {
            get
            {
                return this.operTime;
            }
            set
            {
                this.operTime = value;
            }
        }
		
		/// <summary>
		/// 事由（记录操作原因）
		/// </summary>
        public string OperCase
        {
            get
            {
                return this.operCase;
            }
            set
            {
                this.operCase = value;
            }
        }

		/// <summary>
		/// 扩展信息
		/// </summary>
        public string ExtendInfo1
        {
            get
            {
                return this.extendInfo1;
            }
            set 
            {
                this.extendInfo1 = value;
            }
        }

		/// <summary>
		/// 扩展信息
		/// </summary>
		public string ExtendInfo2
        {
            get
            {
                return this.extendInfo2;
            }
            set
            {
                this.extendInfo2 = value;
            }
        }

		/// <summary>
		/// 扩展信息
		/// </summary>
		public string ExtendInfo3
        {
            get
            {
                return this.extendInfo3;
            }
            set
            {
                this.extendInfo3 = value;
            }
        }
        /// <summary>
        /// 肿瘤卡标志
        /// </summary>
        public string Cancer_Flag
        {
            get
            {
                return this.cancer_Flag;
            }
            set
            {
                this.cancer_Flag = value;
            }
        }
        /// <summary>
        /// 肿瘤卡编号
        /// </summary>
        public string Cancer_No
        {
            get
            {
                return this.cancer_no;
            }
            set
            {
                this.cancer_no = value;
            }
        }
        #endregion

        public CommonReport()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
        }

        #region 克隆函数
        public new CommonReport Clone()
        {
            CommonReport commonReport = base.Clone() as CommonReport;
            commonReport.patient = this.patient.Clone();
            commonReport.homeCity = this.homeCity.Clone();
            commonReport.homeCouty = this.homeCouty.Clone();
            commonReport.homeProvince = this.homeProvince.Clone();
            commonReport.homeTown = this.homeTown.Clone();
            commonReport.patientDept = this.patientDept.Clone();
            commonReport.disease = this.disease.Clone();
            commonReport.caseClass1 = this.caseClass1.Clone();
            commonReport.reportDoctor = this.reportDoctor.Clone();
            commonReport.doctorDept = this.doctorDept.Clone();
            commonReport.cancelOper = this.cancelOper.Clone();
            commonReport.modifyOper = this.modifyOper.Clone();
            commonReport.approveOper = this.approveOper.Clone();
            commonReport.operDept = this.operDept.Clone();

            return commonReport;
        }
        #endregion
    }

   
}
