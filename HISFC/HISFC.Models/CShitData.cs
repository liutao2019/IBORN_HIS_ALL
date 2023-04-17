using System;

namespace neusoft.HISFC.Object
{
	/// <summary>
	/// CShitData ��ժҪ˵����
	/// �����ʵ��
	/// adjust by zhouxs
	/// 2005-6-3
	/// </summary>
	public class CShitData :neusoft.neuFC.Object.neuObject
	{
		public CShitData()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		#region ��
		#endregion
		private string clincNo;
		private string shiftType;
		private UInt32 happenNo;
		private string oldDataCode;
		private string oldDataName;
		private string newDataCode;
		private string newDataName;
		private string shiftCause;
		private string mark;
		private string operCode;
		/// <summary>
		/// ��ˮ��
		/// </summary>
		public string ClinicNo
		{
			get 
			{
				return clincNo;
			}
			set
			{
				clincNo = value;
			}
		}
		/// <summary>
		/// �������
		/// </summary>
		public UInt32 HappenNo
		{
			get
			{
				return happenNo;
			}
			set
			{
				happenNo = value;
			}
		}
		/// <summary>
		/// �������
		/// </summary>
		public string ShitType
		{
			get 
			{
				return shiftType;
			}
			set
			{
				shiftType = value;
			}
		}
		/// <summary>
		/// ԭ���ϴ���
		/// </summary>
		public string OldDataCode
		{
			get
			{
				return oldDataCode;
			}
			set
			{
				oldDataCode = value;
			}
		}
		/// <summary>
		/// ԭ���ϴ���
		/// </summary>
		public string OldDataName
		{
			get
			{
				return oldDataName;
			}
			set
			{
				oldDataName = value;
			}
		}
		/// <summary>
		/// �����ϴ���
		/// </summary>
		public string NewDataCode
		{
			get
			{
				return newDataCode;
			}
			set
			{
				newDataCode = value;
			}
		}
		/// <summary>
		/// ����������
		/// </summary>
		public string NewDataName
		{
			get
			{
				return newDataName;
			}
			set
			{
				newDataName = value;
			}
		}
		/// <summary>
		/// ���ԭ��
		/// </summary>
		public string ShitCause
		{
			get
			{
				return shiftCause;
			}
			set
			{
				shiftCause = value;
			}

		}
		/// <summary>
		/// ��ע
		/// </summary>
		public string Mark
		{
			get
			{
				return mark;
			}
			set
			{
				mark = value;
			}
		}
		/// <summary>
		/// ����Ա����
		/// </summary>
		public string OperCode
		{
			get
			{
				return operCode;
			}
			set
			{
				operCode = value;
			}
		}
		///<summary>
		///��¡����
		///</summary>
		public new CShitData  Clone()
		{
			return this.MemberwiseClone() as CShitData;
		}

	}
}
