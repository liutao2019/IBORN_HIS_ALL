using System;

namespace Neusoft.HISFC.Models.Medical

{
	/// <summary>
	/// [��������: �鼮ʵ��]<br></br>
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
	public class Book:Neusoft.FrameWork.Models.NeuObject
	{
		public Book()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		private string primaryId = "";
		private string bookConcern = "";
		private DateTime bookDate = System.DateTime.MinValue;
		private string editor = "";
		private string ext = "";
		private string ext1 = "";
		private string ext2 = "";
		private string ext3 = "";
		private string mark = "";
		private string operCode = "";
		private DateTime operDate = System.DateTime.MinValue;
		private string wordCount = "";

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
		public string BookConcern
		{
			get
			{
				return this.bookConcern;
			}
			set
			{
				this.bookConcern = value;
			}
		}
		/// <summary>
		/// ��������
		/// </summary>
		public DateTime BookDate
		{
			get
			{
				return this.bookDate;
			}
			set
			{
				this.bookDate = value;
			}
		}
		/// <summary>
		/// ����
		/// </summary>
		public string WordCount
		{
			get
			{
				return this.wordCount;
			}
			set
			{
				this.wordCount = value;
			}
		}
		/// <summary>
		/// ����
		/// </summary>
		public string Editor
		{
			get
			{
				return this.editor;
			}
			set
			{
				this.editor = value;
			}
		}
		/// <summary>
		/// ��չ�ֶ�
		/// </summary>
		public string Ext
		{
			get
			{
				return this.ext;
			}
			set
			{
				this.ext = value;
			}
		}
		/// <summary>
		/// ��չ�ֶ�1
		/// </summary>
		public string Ext1
		{
			get
			{
				return this.ext1;
			}
			set
			{
				this.ext1 = value;
			}
		}
		/// <summary>
		/// ��չ�ֶ�2
		/// </summary>
		public string Ext2
		{
			get
			{
				return this.ext2;
			}
			set
			{
				this.ext2 = value;
			}
		}
		/// <summary>
		/// ��չ�ֶ�3
		/// </summary>
		public string Ext3
		{
			get
			{
				return this.ext3;
			}
			set
			{
				this.ext3 = value;
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
