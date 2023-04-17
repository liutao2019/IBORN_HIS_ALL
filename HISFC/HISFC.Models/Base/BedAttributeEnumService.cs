using System.Collections;
namespace Neusoft.HISFC.Object.Base
{
	/// <summary>
	/// BedAttributeEnumService<br></br>
	/// [��������: ��λ����ö�ٷ�����]<br></br>
	/// [�� �� ��: ����ȫ]<br></br>
	/// [����ʱ��: 2006-09-01]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��='yyyy-mm-dd'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
	public class BedAttributeEnumService : EnumServiceBase
	{
		static BedAttributeEnumService()
		{
			items[BedAttribute.I] = "������";
			items[BedAttribute.O] = "������";
			items[BedAttribute.A] = "�Ӵ�";
			items[BedAttribute.F] = "��ͥ����";
		}
		
		#region ����
			
		/// <summary>
		/// ����ö������
		/// </summary>
		protected static Hashtable items = new Hashtable();
		
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

		#endregion
	}
	   

    	#region ö��
		/// <summary>
		/// ��λ����
		/// </summary>
		public enum BedAttribute 
		{
			/// <summary>
			/// ������
			/// </summary>
			I,
			/// <summary>
			/// ������
			/// </summary>
			O,
			/// <summary>
			/// �Ӵ�
			/// </summary>
			A,
			/// <summary>
			/// ��ͥ����
			/// </summary>
			F
		}	

		#endregion
}
