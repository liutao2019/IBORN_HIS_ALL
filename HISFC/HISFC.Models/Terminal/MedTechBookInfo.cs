using System;


namespace FS.HISFC.Models.Terminal
{
	/// <summary>
	/// MedTechBookInfo <br></br>
	/// [��������: ҽ��ԤԼ��Ϣ]<br></br>
	/// [�� �� ��: zhouxs]<br></br>
	/// [����ʱ��: 2006-3-8]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��=''
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    [Serializable]
    public class MedTechBookInfo : FS.FrameWork.Models.NeuObject
	{
		public MedTechBookInfo()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		#region ����

		/// <summary>
		/// ԤԼ״̬
		/// </summary>
		private string status;
		
		/// <summary>
		/// ԤԼ��
		/// </summary>
		private string bookID;
		
		/// <summary>
		/// ԤԼʱ��
		/// </summary>
		private DateTime bookTime;

		#endregion

		#region ����

		/// <summary>
		/// ԤԼ״̬
		/// </summary>
		public string Status
		{
			get
			{
				return status;
			}
			set
			{
				status = value;
			}

		}
		
		/// <summary>
		/// ԤԼ����
		/// </summary>
		public string BookID
		{
			get
			{
				return this.bookID;
			}
			set
			{
				this.bookID = value;
			}
		}
		
		/// <summary>
		/// ԤԼʱ��
		/// </summary>
		public DateTime BookTime
		{
			get
			{
				return this.bookTime;
			}
			set
			{
				this.bookTime = value;
			}
		}

		#endregion

		#region ��ʱ

		/// <summary>
		/// ԤԼ����
		/// </summary>
		[Obsolete("�Ѿ���ʱ������ΪBookID", true)]
		public string BookId
		{
			get
			{
				return bookID;
			}
			set
			{
				bookID = value;
			}
		}

		/// <summary>
		/// ԤԼʱ��
		/// </summary>
		[Obsolete("�Ѿ���ʱ������ΪBookTime", true)]
		public DateTime BookDate
		{
			get
			{
				return bookTime;
			}
			set
			{
				bookTime = value;
			}
		}
		
		#endregion

		#region ����

		/// <summary>
		/// ���п�¡
		/// </summary>
		/// <returns>ҽ��ԤԼ��Ϣ</returns>
		public new MedTechBookInfo Clone()
		{
			return base.Clone() as MedTechBookInfo;
		}

		#endregion
	}
}
