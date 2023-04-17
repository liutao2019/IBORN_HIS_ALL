using System;
using System.Collections;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Models.Fee
{
	/// <summary>
	/// EnumPayTypeService<br></br>
	/// [��������: ֧������ö�ٷ�����]<br></br>
	/// [�� �� ��: ����]<br></br>
	/// [����ʱ��: 2006-09-01]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��='yyyy-mm-dd'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
    /// </summary>{93E6443C-1FB5-45a7-B89D-F21A92200CF6}
    [Obsolete("����", true)]
	public class EnumPayTypeService : EnumServiceBase
	{
		static EnumPayTypeService() 
		{
			items[EnumPayType.CA] = "�ֽ�";
			items[EnumPayType.CH] = "֧Ʊ";
			items[EnumPayType.CD] = "���ÿ�";
			items[EnumPayType.DB] = "��ǿ�";
			items[EnumPayType.AJ] = "תѺ��";
			items[EnumPayType.PO] = "��Ʊ";
			items[EnumPayType.PS] = "�����ʻ�";
            items[EnumPayType.YS] = "Ժ���˻�";
            items[EnumPayType.PB] = "ͳ��(ҽԺ�渶)";
            items[EnumPayType.HP] = "�渶��";

		}

		#region ����

		/// <summary>
		/// ����ö������
		/// </summary>
		protected static Hashtable items = new Hashtable();
		
		/// <summary>
		/// ֧������
		/// </summary>
		private EnumPayType enumPayType;

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
		/// ֧������
		/// </summary>
		protected override Enum EnumItem
		{
			get
			{
				return this.enumPayType;
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
    [Obsolete("����", true)]
	/// <summary>
    /// ֧������{93E6443C-1FB5-45a7-B89D-F21A92200CF6}
	/// </summary>
	public enum EnumPayType
	{
		/// <summary>
		/// �ֽ�
		/// </summary>
		CA = 0,

		/// <summary>
		/// ֧Ʊ
		/// </summary>
		CH = 1,
			
		/// <summary>
		/// ���ÿ�
		/// </summary>
		CD = 2,

		/// <summary>
		/// ��ǿ�
		/// </summary>
		DB = 3,
		
		/// <summary>
		/// תѺ��
		/// </summary>
		AJ = 4,

		/// <summary>
		/// ��Ʊ
		/// </summary>
		PO = 5,

		/// <summary>
		/// �����ʻ�
		/// </summary>
		PS = 6,

        /// <summary>
        /// Ժ���˻�
        /// </summary>
        YS = 7,
        /// <summary>
        /// ͳ��(ҽԺ�渶)
        /// </summary>
        PB = 8,
        /// <summary>
        /// �渶��
        /// </summary>
        HP = 9
	}

	#endregion
}
