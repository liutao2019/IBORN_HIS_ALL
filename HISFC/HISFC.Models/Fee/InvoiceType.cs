using System;
using System.Collections;
using Neusoft.HISFC.Object.Base;

namespace Neusoft.HISFC.Object.Fee
{
	/// <summary>
	/// InvoiceTypeEnumService<br></br>
	/// [��������: �վ�(��Ʊ)����ö�ٷ�����]<br></br>
	/// [�� �� ��: ����]<br></br>
	/// [����ʱ��: 2006-09-01]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��='yyyy-mm-dd'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
	public class InvoiceTypeEnumService : EnumServiceBase
	{
		static InvoiceTypeEnumService( ) 
		{
			items[EnumInvoiceType.R] = "�Һ��վ�";
			items[EnumInvoiceType.C] = "�����վ�";
			items[EnumInvoiceType.I] = "סԺ�վ�";
			items[EnumInvoiceType.P] = "Ԥ���վ�";
		}

		#region ����

		/// <summary>
		/// ����ö������
		/// </summary>
		protected static Hashtable items = new Hashtable();
		
		/// <summary>
		/// �վ�(��Ʊ)����
		/// </summary>
		private EnumInvoiceType enumInvoiceType;

		#endregion

		#region ����

		/// <summary>
		/// ����ö������
		/// </summary>
		protected override Hashtable Items
		{
			get
			{
				return items;
			}
		}
		
		/// <summary>
		/// �վ�(��Ʊ)����
		/// </summary>
		protected override Enum EnumItem
		{
			get
			{
				return this.enumInvoiceType;
			}
		}

		#endregion

	}
	
	#region ö��

	/// <summary>
	/// �վ�(��Ʊ)����
	/// </summary>
	public enum EnumInvoiceType
	{
		/// <summary>
		/// �Һ��վ�
		/// </summary>
		R=0,

		/// <summary>
		/// �����վ�
		/// </summary>
		C=1,

		/// <summary>
		/// סԺ�վ�
		/// </summary>
		I=2,

		/// <summary>
		/// Ԥ���վ�
		/// </summary>
		P=4
	}

	#endregion

}
