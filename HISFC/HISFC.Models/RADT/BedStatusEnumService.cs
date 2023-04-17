using System;
using System.Collections;
using Neusoft.HISFC.Object.Base;

namespace Neusoft.HISFC.Object.RADT
{
	/// <summary>
	/// [��������: ��λ״̬ʵ��]<br></br>
	/// [�� �� ��: ���Ʒ�]<br></br>
	/// [����ʱ��: 2006-09-05]<br></br>
	/// <�޸ļ�¼
	///		�޸���='����ΰ'
	///		�޸�ʱ��='2006-9-12'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary> 
	public class BedStatusEnumService:EnumServiceBase	
	{

		/// <summary>
		/// ö�ٶ��� ��λ״̬ʵ��������
		/// </summary>
		public BedStatusEnumService()
		{
			items[EnumBedStatus.C] = "�ر�";//Closed
			items[EnumBedStatus.U] = "�մ�";//Unoccupied
			items[EnumBedStatus.K] = "��Ⱦ";
			items[EnumBedStatus.I] = "����";
			items[EnumBedStatus.O] = "ռ��";
			items[EnumBedStatus.R] = "�ٴ�";
			items[EnumBedStatus.W] = "����";
			items[EnumBedStatus.H] = "�Ҵ�";
		}

       
		#region ����

		private EnumBedStatus enumBedStatus;	
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
		
		protected override Enum EnumItem
		{
			get
			{
				return this.enumBedStatus;
			}
		}

		#endregion  	
	}
}
