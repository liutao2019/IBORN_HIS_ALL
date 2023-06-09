using System;


namespace FS.HISFC.Models.SIInterface {


	/// <summary>
	/// 广州医保入院患者基本信息
	/// </summary>
    [Serializable]
    public class InpatientInfo:FS.FrameWork.Models.NeuObject
	{
		public InpatientInfo()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		private string regNo;
		/// <summary>
		/// 就医登记号
		/// </summary>
		public string RegNo
		{
			get
			{
				return regNo;
			}
			set
			{
				regNo = value;
			}
		}
		private string hosNo;
		/// <summary>
		/// 医院编号
		/// </summary>
		public string HosNo
		{
			get
			{
				return hosNo;
			}
			set
			{
				hosNo = value;
			}
		}
		private string personId;
		/// <summary>
		/// 公民身份号码
		/// </summary>
		public string PersonId
		{
			get
			{
				return personId;
			}
			set
			{
				personId = value;
			}
		}
		private string personName;
		/// <summary>
		/// 姓名
		/// </summary>
		public string PersonName
		{
			get
			{
				return personName;
			}
			set
			{
				personName = value;
			}
		}
		private string companyName;
		/// <summary>
		/// 单位名称
		/// </summary>
		public string CompanyName
		{
			get
			{
				return companyName;
			}
			set
			{
				companyName = value;
			}
		}
		private DateTime birthday;
		/// <summary>
		/// 出生日期
		/// </summary>
		public DateTime Birthday
		{
			get
			{
				return birthday;
			}
			set
			{
				birthday = value;
			}
		}
		private FS.FrameWork.Models.NeuObject emplType = new FS.FrameWork.Models.NeuObject();
		/// <summary>
		/// 人员类别 1－在职2－退休3－离休4－1－4级工残5－无业7－退职
		/// </summary>
		public FS.FrameWork.Models.NeuObject EmplType
		{
			get
			{
				return emplType;
			}
			set
			{
				emplType = value;
			}
		}
		private string patientNo;
		/// <summary>
		/// 住院号
		/// </summary>
		public string PatientNo
		{
			get
			{
				return patientNo;
			}
			set
			{
				patientNo = value;
			}
		}
		private FS.FrameWork.Models.NeuObject regType = new FS.FrameWork.Models.NeuObject();
		/// <summary>
		/// 就诊类别 1-住院2-门诊特定项目
		/// </summary>
		public FS.FrameWork.Models.NeuObject RegType
		{
			get
			{
				return regType;
			}
			set
			{
				regType = value;
			}
		}
		private DateTime inDate;
		/// <summary>
		/// 入院（就诊）日期
		/// </summary>
		public DateTime InDate
		{
			get
			{
				return inDate;
			}
			set
			{
				inDate = value;
			}
		}
		private FS.FrameWork.Models.NeuObject inDiagnose = new FS.FrameWork.Models.NeuObject();
		/// <summary>
		/// 入院（门诊）诊断
		/// </summary>
		public FS.FrameWork.Models.NeuObject InDiagnose
		{
			get
			{
				return inDiagnose;
			}
			set
			{
				inDiagnose = value;
			}
		}
		private FS.FrameWork.Models.NeuObject inDept = new FS.FrameWork.Models.NeuObject();
		/// <summary>
		/// 住院科室 医药机构在医保客户端维护的科室代码
		/// </summary>
		public FS.FrameWork.Models.NeuObject InDept
		{
			get
			{
				return inDept;
			}
			set
			{
				inDept = value;
			}
		}
		private string bedNo;
		/// <summary>
		/// 床位代号
		/// </summary>
		public string BedNo
		{
			get
			{
				return bedNo;
			}
			set
			{
				bedNo = value;
			}
		}
		private int applyNo;
		/// <summary>
		/// 特诊对象审批号
		/// </summary>
		public int ApplyNo
		{
			get
			{
				return applyNo;
			}
			set
			{
				applyNo = value;
			}
		}
		private int readFlag;
		/// <summary>
		/// 读入标志
		/// </summary>
		public int ReadFlag
		{
			get
			{
				return readFlag;
			}
			set
			{
				readFlag = value;
			}
		}

		public new InpatientInfo Clone()
		{
			InpatientInfo obj = base.MemberwiseClone() as InpatientInfo;

			obj.InDiagnose = this.InDiagnose.Clone();
			obj.InDept = this.InDept.Clone();
			obj.EmplType = this.EmplType.Clone();
			obj.RegType = this.RegType.Clone();

			return obj;
		}

	}
}
