using System.Collections;
namespace FS.HISFC.Models.Base
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
    [System.Serializable]
	public class BedRankEnumService : EnumServiceBase
	{
		static BedRankEnumService()
		{
			items[EnumBedRank.I] = "������";
			items[EnumBedRank.O] = "������";
			items[EnumBedRank.A] = "�Ӵ�";
			items[EnumBedRank.F] = "��ͥ����";
		}
		EnumBedRank enumBadRank;
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
				return this.enumBadRank;
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
		/// ��λ����
		/// </summary>
		public enum EnumBedRank
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
