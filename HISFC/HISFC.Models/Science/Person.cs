using System;

namespace neusoft.HISFC.Object.Science
{
	/// <summary>
	/// employee ��ժҪ˵����
	/// </summary>
	public class Person 
	{
		public Person()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}


		#region ""
		private decimal conferenceCost = 0;
		private string conferenceResource= "";
		private string duty= "";
		private string dutyLevel= "";
		private string educationSchool = "";
		private string extendFlag = "";
		private string extendFlag1 = "";
		private string operCode = "";
		private DateTime operDate = DateTime.MinValue;
		private string organization = "";
		private decimal paperNum = 0;
		private string paperWay = "";
		private string primaryId = "";
		private string specialDept = "";
		#endregion

		/// <summary>
		/// ��Ա��Ϣ
		/// </summary>
		public neusoft.HISFC.Object.RADT.Person Info = new neusoft.HISFC.Object.RADT.Person();
		/// <summary>
		/// ��չ��Ϣ
		/// </summary>
		public neusoft.HISFC.Object.Science.Extend Extend = new Extend();
		/// <summary>
		/// ������
		/// </summary>
		public string PrimaryId
		{
			get
			{
				return this.primaryId;
			}
			set
			{
				this.primaryId = value;
			}
		}
		/// <summary>
		/// ְ�����
		/// </summary>
		public string Duty
		{
			get
			{
				return this.duty;
			}
			set
			{
				this.duty = value;
			}
		}
		/// <summary>
		/// ְ������
		/// </summary>
		public string DutyLevel
		{
			get
			{
				return this.dutyLevel;
			}
			set
			{
				this.dutyLevel = value;
			}
		}
		/// <summary>
		/// ��д����
		/// </summary>
		public string ExtendFlag
		{
			get
			{
				return this.extendFlag;
			}
			set
			{
				this.extendFlag = value;
			}
		}
		/// <summary>
		/// ������ò
		/// </summary>
		public string ExtendFlag1
		{
			get
			{
				return this.extendFlag1;
			}
			set
			{
				this.extendFlag1 = value;
			}
		}
		/// <summary>
		/// ������֯
		/// </summary>
		public string Organization
		{
			get
			{
				return this.organization;
			}
			set
			{
				this.organization = value;
			}
		}
		/// <summary>
		/// ר��
		/// </summary>
		public string SpecialDept
		{
			get
			{
				return this.specialDept;
			}
			set
			{
				this.specialDept = value;
			}
		}
		/// <summary>
		/// ��ҵԺУ
		/// </summary>
		public string EducationSchool
		{
			get
			{
				return this.educationSchool;
			}
			set
			{
				this.educationSchool = value;
			}
		}
		/// <summary>
		/// ���Ľ�����ʽ
		/// </summary>
		public string PaperWay
		{
			get
			{
				return this.paperWay;
			}
			set
			{
				this.paperWay = value;
			}
		}
		/// <summary>
		/// ���龭��
		/// </summary>
		public decimal ConferenceCost
		{
			get
			{
				return this.conferenceCost;
			}
			set
			{
				this.conferenceCost = value;
			}
		}
		/// <summary>
		/// ����ƪ��
		/// </summary>
		public decimal PaperNum
		{
			get
			{
				return this.paperNum;
			}
			set
			{
				this.paperNum = value;
			}
		}
		/// <summary>
		/// ������Դ
		/// </summary>
		public string ConferenceResource
		{
			get
			{
				return this.conferenceResource;
			}
			set
			{
				this.conferenceResource = value;
			}
		}
		/// <summary>
		/// ����Ա
		/// </summary>
		public string OperCode
		{
			get
			{
				return this.operCode;
			}
			set
			{
				this.operCode = value;
			}
		}
		/// <summary>
		/// ����ʱ��
		/// </summary>
		public DateTime OperDate
		{
			get
			{
				return this.operDate;
			}
			set
			{
				this.operDate = value;
			}
		}


	}
}
