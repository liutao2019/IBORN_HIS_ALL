using System;

namespace neusoft.HISFC.Object.Science
{
	/// <summary>
	/// Result ��ժҪ˵����
	/// </summary>
	public class Result:neusoft.neuFC.Object.neuObject
	{
		public Result()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		#region ����
		private String awardDept = "";
		private String awardLevel = "";
		private DateTime awardTime = System.DateTime.MinValue;
		private String grade = "";
		private String level = "";
		private String mark = "";
		private String operCode = "";
		private DateTime operDate = System.DateTime.MinValue;
		private String primaryId = "";
		private String principal = "";
		private String valid = "";
		private DateTime viseTime = System.DateTime.MinValue;
		#endregion


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
		/// �ɹ�����
		/// </summary>
		public string Level
		{
			get
			{
				return this.level;
			}
			set
			{
				this.level = value;
			}
		}

		/// <summary>
		/// �ɹ��Ǽ�
		/// </summary>
		public string Grade
		{
			get
			{
				return this.grade;
			}
			set
			{
				this.grade = value;
			}
		}

		/// <summary>
		/// ǩ������
		/// </summary>
		public DateTime ViseTime
		{
			get
			{
				return this.viseTime;
			}
			set
			{
				this.viseTime = value;
			}
		}

		/// <summary>
		/// �佱����
		/// </summary>
		public DateTime AwardTime
		{
			get
			{
				return this.awardTime;
			}
			set
			{
				this.awardTime = value;
			}
		}

		/// <summary>
		/// �佱��λ
		/// </summary>
		public string AwardDept
		{
			get
			{
				return this.awardDept;
			}
			set
			{
				this.awardDept = value;
			}
		}

		/// <summary>
		/// ��������
		/// </summary>
		public string AwardLevel
		{
			get
			{
				return this.awardLevel;
			}
			set
			{
				this.awardLevel = value;
			}
		}

		/// <summary>
		/// ������
		/// </summary>
		public string Principal
		{
			get
			{
				return this.principal;
			}
			set
			{
				this.principal = value;
			}
		}

		/// <summary>
		/// ��ע
		/// </summary>
		public string Mark
		{
			get
			{
				return this.mark;
			}
			set
			{
				this.mark = value;
			}
		}

		/// <summary>
		/// ��Ч״̬
		/// </summary>
		public string Valid
		{
			get
			{
				return this.valid;
			}
			set
			{
				this.valid = value;
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
