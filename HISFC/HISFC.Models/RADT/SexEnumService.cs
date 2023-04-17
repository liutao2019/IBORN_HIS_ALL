using System;
using System.Collections;
using Neusoft.HISFC.Object.Base;

namespace Neusoft.HISFC.Object.RADT
{
	/// <summary>
	/// SexEnumService <br></br>
	/// [��������: �Ա�ö�ٷ���ʵ��]<br></br>
	/// [�� �� ��: ��һ��]<br></br>
	/// [����ʱ��: 2006-09-12]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��=''
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
	public class SexEnumService : EnumServiceBase
	{
		public SexEnumService()
		{
			this.Items[EnumSex.U] = "δ֪";
			this.Items[EnumSex.M] = "��";
			this.Items[EnumSex.F] = "Ů";
			this.Items[EnumSex.O] = "����";
		}

		#region ����

		/// <summary>
		/// �������
		/// </summary>
		EnumSex enumSex;

		/// <summary>
		/// �洢ö�ٶ���
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
		
		/// <summary>
		/// ö����Ŀ
		/// </summary>
		protected override Enum EnumItem
		{
			get
			{
				return this.enumSex;
			}
		}

		#endregion

		
	}

//	/// <summary>
//	/// �Ա�
//	/// </summary>
//	public enum EnumSex
//	{
//		/// <summary>
//		/// ��
//		/// </summary>
//		M=1,
//		/// <summary>
//		/// Ů
//		/// </summary>
//		F=2,
//		/// <summary>
//		/// ����
//		/// </summary>
//		O=3,
//		/// <summary>
//		/// δ֪
//		/// </summary>
//		U=0
//	};
}
