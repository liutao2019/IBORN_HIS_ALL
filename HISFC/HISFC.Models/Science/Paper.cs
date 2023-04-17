using System;

namespace neusoft.HISFC.Object.Science
{
	/// <summary>
	/// Paper ��ժҪ˵����
	/// </summary>
	public class Paper:neusoft.neuFC.Object.neuObject
	{
		public Paper()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		#region ����
		private String primaryId = "";
		private String dept = "";
		private String period = "";
		private String kind = "";
		private String level = "";
		private String mark = "";
		private String operCode = "";
		private DateTime operDate = System.DateTime.MinValue;
		private String page = "";
		private String publication = "";
		private String valid = "";
		private DateTime publishTime = System.DateTime.MinValue;
		private String source = "";
		private String volume = "";
		private String author = "";
		private String specialDept = "";
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
		/// ������
		/// </summary>
		public string Publication
		{
			get
			{
				return this.publication;
			}
			set
			{
				this.publication = value;
			}
		}
		/// <summary>
		/// ��
		/// </summary>
		public string Volume
		{
			get
			{
				return this.volume;
			}
			set
			{
				this.volume = value;
			}
		}
		/// <summary>
		/// ��
		/// </summary>
		public string Period
		{
			get
			{
				return this.period;
			}
			set
			{
				this.period = value;
			}
		}
		/// <summary>
		/// ҳ
		/// </summary>
		public string Page
		{
			get
			{
				return this.page;
			}
			set
			{
				this.page = value;
			}
		}
		/// <summary>
		/// ��������
		/// </summary>
		public DateTime PublishTime
		{
			get
			{
				return this.publishTime;
			}
			set
			{
				this.publishTime = value;
			}
		}
		/// <summary>
		/// ���ļ���
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
		/// �������
		/// </summary>
		public string Kind
		{
			get
			{
				return this.kind;
			}
			set
			{
				this.kind = value;
			}
		}
		/// <summary>
		/// ����
		/// </summary>
		public string Author
		{
			get
			{
				return this.author;
			}
			set
			{
				this.author = value;
			}
		}
		/// <summary>
		/// ����
		/// </summary>
		public string Dept
		{
			get
			{
				return this.dept;
			}
			set
			{
				this.dept = value;
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
		/// ������Դ
		/// </summary>
		public string Source
		{
			get
			{
				return this.source;
			}
			set
			{
				this.source = value;
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
