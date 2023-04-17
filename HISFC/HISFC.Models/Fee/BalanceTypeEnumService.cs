using System;
using System.Collections;

using FS.HISFC.Models.Base;

namespace FS.HISFC.Models.Fee
{

	/// <summary>
	/// BalanceTypeEnumService<br></br>
	/// [��������: ��������ö�ٷ�����]<br></br>
	/// [�� �� ��: ��˹]<br></br>
	/// [����ʱ��: 2006-09-01]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��='yyyy-mm-dd'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    /// 
    [System.Serializable]
	public class BalanceTypeEnumService : EnumServiceBase 
	{
		static BalanceTypeEnumService() 
		{
			items[EnumBalanceType.I] = "��;����";
			items[EnumBalanceType.O] = "��Ժ����";
			items[EnumBalanceType.D] = "ֱ�ӽ���";
			items[EnumBalanceType.S] = "��ת����";
			items[EnumBalanceType.C] = "�������";
			items[EnumBalanceType.E] = "�������";
			items[EnumBalanceType.P] = "������";
            items[EnumBalanceType.Q] = "Ƿ�ѽ���";
            items[EnumBalanceType.J] = "ƽ�˽���";
		}

		#region ����

		/// <summary>
		/// ����ö������
		/// </summary>
		protected static Hashtable items = new Hashtable();
		
		/// <summary>
		/// ��������
		/// </summary>
		private EnumBalanceType enumBalanceType;

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
		/// ��������
		/// </summary>
		protected override Enum EnumItem
		{
			get
			{
				return this.enumBalanceType;
			}
		}

		#endregion

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public new static ArrayList List()
        {
            return (new ArrayList(GetObjectItems(items)));
        }
	}

	#region ö��
	
	/// <summary>
	/// ��������
	/// </summary>
	public enum EnumBalanceType 
	{
		/// <summary>
		/// ��;���� I
		/// </summary>
		I,

		/// <summary>
		/// ��Ժ���� O
		/// </summary>
		O,
		
		/// <summary>
		/// ֱ�ӽ���
		/// </summary>
		D,
		
		/// <summary>
		/// ��ת����
		/// </summary>
		S,
		
		/// <summary>
		/// �������
		/// </summary>
		C,
		
		/// <summary>
		/// �������
		/// </summary>
		E,
		
		/// <summary>
		/// ������
		/// </summary>
		P,
        /// <summary>
        /// Ƿ�ѽ���
        /// </summary>
        Q,
        /// <summary>
        /// Ƿ�ѽ���
        /// </summary>
        J
	}

	#endregion
}
