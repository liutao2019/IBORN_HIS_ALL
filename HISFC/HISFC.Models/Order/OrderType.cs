using System;
 
//using FS.NF;
using FS.HISFC;
namespace FS.HISFC.Models.Order
{
	/// <summary>
	/// FS.HISFC.Models.Order.OrderType<br></br>
	/// [��������: ҽ��������Ϣʵ��]<br></br>
	/// [�� �� ��: ��˹]<br></br>
	/// [����ʱ��: 2006-09-10]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��='yyyy-mm-dd'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    [Serializable]
    public class OrderType:FS.FrameWork.Models.NeuObject
	{
		public OrderType()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}


		
		#region ����

		/// <summary>
		/// ҽ������
		///		<br>����ҽ��</br>
		///		<br>��ʱҽ��</br>
		/// </summary>
        protected EnumType myType;

		/// <summary>
		/// �Ƿ�ֽ�
		/// </summary>
		protected bool bIsDecompose = false;

		/// <summary>
		/// �Ƿ�Ƿ�
		/// </summary>
		public bool isCharge;

		/// <summary>
		/// �Ƿ���ҩ
		/// </summary>
		public bool isNeedPharmacy;

		/// <summary>
		/// �Ƿ���Ҫȷ��
		/// </summary>
		public bool isConfirm;

		/// <summary>
		/// �Ƿ���Ҫ��ӡҽ����
		/// </summary>
		public bool isPrint;

		#endregion

		#region ����

		/// <summary>
		/// ҽ������
		///		<br>����ҽ��</br>
		///		<br>��ʱҽ��</br>
		/// </summary>
		public EnumType Type
		{
			get
			{
                if (myType == null)
                {
                    myType = EnumType.SHORT;
                }
				return this.myType;
			}
		}

		/// <summary>
		/// �Ƿ�ֽ�
		/// </summary>
		public bool IsDecompose
		{
			get
			{
				return this.bIsDecompose;
			}
			set
			{
				this.bIsDecompose = value;
				if(this.bIsDecompose)
				{
					this.myType = EnumType.LONG;
				}
				else
				{
					this.myType = EnumType.SHORT;
				}
			}
		}

		/// <summary>
		/// �Ƿ�Ƿ�
		/// </summary>
		public bool IsCharge
		{
			get
			{ 
				return this.isCharge; 
			}
			set
			{ 
				this.isCharge = value; 
			}
		}

		/// <summary>
		/// '�Ƿ���ҩ
		/// </summary>
		public bool IsNeedPharmacy
		{
			get
			{ 
				return this.isNeedPharmacy; 
			}
			set
			{ 
				this.isNeedPharmacy = value; 
			}
		}

		/// <summary>
		/// �Ƿ���Ҫȷ��
		/// </summary>
		public bool IsConfirm
		{
			get
			{ 
				return this.isConfirm; 
			}
			set
			{ 
				this.isConfirm = value; 
			}
		}

		/// <summary>
		/// �Ƿ���Ҫ��ӡҽ����
		/// </summary>
		public bool IsPrint
		{
			get
			{ 
				return this.isPrint; 
			}
			set
			{ 
				this.isPrint = value; 
			}
		}

		#endregion

		#region ����

		#region ��¡

		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns></returns>
		public new OrderType Clone()
		{
			OrderType ordertype = base.Clone() as OrderType;
			return ordertype;
		}

		#endregion

		#endregion

	}

	#region ö��

	/// <summary>
	/// ҽ������
	/// </summary>
	public enum EnumType
	{
		/// <summary>
		/// ����ҽ��
		/// </summary>
		LONG = 0,
		/// <summary>
		/// ��ʱҽ��
		/// </summary>
		SHORT = 1
	}

	#endregion
}
