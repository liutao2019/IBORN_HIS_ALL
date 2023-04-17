using System;

namespace FS.HISFC.Models.Base
{
	/// <summary>
	/// Sequence<br></br>
	/// [��������: ����ʵ��]<br></br>
	/// [�� �� ��: ����ΰ]<br></br>
	/// [����ʱ��: 2006-08-29]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��='yyyy-mm-dd'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    [System.Serializable]
    public class Sequence:FS.FrameWork.Models.NeuObject, ISort
	{
		/// <summary>
		/// ���캯��
		/// </summary>
		public Sequence()
		{
		}

		#region ö��
		/// <summary>
		/// ӵ������
		/// </summary>
		public enum enuType
		{
			/// <summary>
			/// ҽԺ
			/// </summary>
			Hospital,
			/// <summary>
			/// ����
			/// </summary>
			Dept,
			/// <summary>
			/// ��
			/// </summary>
			WorkGroup,
			/// <summary>
			/// ��
			/// </summary>
			Person
		}

		public enuType Type = enuType.Hospital;
		#endregion

		#region ����

		/// <summary>
		/// ����
		/// </summary>
		private int sortid;

		/// <summary>
		/// ��Сֵ
		/// </summary>
		private string minValue = "1";

		/// <summary>
		/// ��ǰֵ
		/// </summary>
		private string currentValue;

		/// <summary>
		/// ����
		/// </summary>
		private string rule ;
		#endregion

        #region ����
		
		/// <summary>
		/// ��Сֵ
		/// </summary>
		public string MinValue
		{
			get 
			{
				return minValue;
			}
			set
			{
				this.minValue = value;
			}
		}

		/// <summary>
		/// ��ǰֵ
		/// </summary>
		public string CurrentValue
		{
			get
			{
				return this.currentValue;
			}
			set
			{
				this.currentValue = value ;
			}
		}

		public string Rule
		{
			get
			{
				return this.rule;
			}
			set
			{
				this.rule = value;
			}
		}
       
		#endregion

		#region �ӿ� ISort ��Ա    

		/// <summary>
		/// ����
		/// </summary>
		public int SortID
		{
			get
			{
				return sortid;
			}
			set
			{
				this.sortid = value;
			}
		}

		#endregion
	}
}
