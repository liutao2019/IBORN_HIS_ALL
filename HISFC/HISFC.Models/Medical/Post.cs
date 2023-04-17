using System;

namespace Neusoft.HISFC.Models.Medical

{
	/// <summary>
	/// [��������: ��ְʵ��]<br></br>
	/// [�� �� ��: ����ΰ]<br></br>
	/// [����ʱ��: 2006-09-05]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��='yyyy-mm-dd'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    [Serializable]
	public class Post:Neusoft.HISFC.Models.Medical.Person
	{
		public Post()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		private String id = "";
		private String firstName = "";
		private String secondName = "";
		private String scienceDuty = "";
		private DateTime beginTime = System.DateTime.MinValue;
		private DateTime endTime = System.DateTime.MinValue;


		/// <summary>
		/// ��ְ����
		/// </summary>
		public string ID
		{
			get
			{
				return this.id;
			}
			set
			{
				this.id = value;
			}
		}
		/// <summary>
		/// ��ְ����һ������
		/// </summary>
		public string FirstName
		{
			get
			{
				return this.firstName;
			}
			set
			{
				this.firstName = value;
			}
		}
		/// <summary>
		/// ��ְ������������
		/// </summary>
		public string SecondName
		{
			get
			{
				return this.secondName;
			}
			set
			{
				this.secondName = value;
			}
		}
		/// <summary>
		/// ѧ��ְ��
		/// </summary>
		public string ScienceDuty
		{
			get
			{
				return this.scienceDuty;
			}
			set
			{
				this.scienceDuty = value;
			}
		}
		/// <summary>
		/// ��ʼʱ��
		/// </summary>
		public DateTime BeginTime
		{
			get
			{
				return this.beginTime;
			}
			set
			{
				this.beginTime = value;
			}
		}
		/// <summary>
		/// ����ʱ��
		/// </summary>
		public DateTime EndTime
		{
			get
			{
				return this.endTime;
			}
			set
			{
				this.endTime = value;
			}
		}

	}
}
