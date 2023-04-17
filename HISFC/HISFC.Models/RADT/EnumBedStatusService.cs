using System;
using System.Collections;

namespace Neusoft.HISFC.Object.RADT
{
	/// <summary>
	/// EnumBedStatusService ��ժҪ˵����
	/// </summary>
	public class EnumBedStatusService:Base.EnumServiceBase	
	{
		public EnumBedStatusService()
		{
			items[EnumBedStatus.C] = "Closed";
			items[EnumBedStatus.O] = "������";
			items[EnumBedStatus.A] = "�Ӵ�";
			items[EnumBedStatus.F] = "��ͥ����";
		}
        EnumBedStatus enumBedStatus;
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
		
		protected override System.Enum EnumItem
		{
			get
			{
				return this.enumBedStatus;
			}
		}

		#endregion
        
	
	}
	#region ö��
	/// <summary>
	/// ����״̬
	/// </summary>
	public enum EnumBedStatus
	{
		/// <summary>
		/// Closed
		/// </summary>
		C,
		/// <summary>
		/// Unoccupied
		/// </summary>
		U,
		/// <summary>
		/// Contaminated��Ⱦ��
		/// </summary>
		K,
		/// <summary>
		/// �����
		/// </summary>
		I,
		/// <summary>
		/// Occupied
		/// </summary>
		O,
		/// <summary>
		/// �ٴ�  user define
		/// </summary>
		R,
		/// <summary>
		/// ���� user define
		/// </summary>
		W,
		/// <summary>
		/// �Ҵ�
		/// </summary>
		H
	}	

	#endregion
}
