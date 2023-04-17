using System;

namespace FS.HISFC.Models.IMA
{
	/// <summary>
	/// [��������: ҩƷ������ö�ٷ�����]<br></br>
	/// [�� �� ��: ������]<br></br>
	/// [����ʱ��: 2006-09-12]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��='yyyy-mm-dd'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    [Serializable]
    public class ModuelEnumService : FS.HISFC.Models.Base.EnumServiceBase
	{
		static ModuelEnumService()
		{
			moduelHash[EnumModuelType.Phamacy] = "ҩƷ";
			moduelHash[EnumModuelType.Equipment] = "�̶��ʲ�";
			moduelHash[EnumModuelType.Material] = "����";
            moduelHash[EnumModuelType.Blood] = "Ѫ��";
			moduelHash[EnumModuelType.All] = "ȫ��";
		}
		

		EnumModuelType moduelEnum;

		/// <summary>
		/// ö�����ƴ洢
		/// </summary>
		protected static System.Collections.Hashtable moduelHash = new System.Collections.Hashtable();

		/// <summary>
		/// �洢ö������
		/// </summary>
		protected override System.Collections.Hashtable Items
		{
			get
			{
				return moduelHash;
			}
		}

		/// <summary>
		/// ö��
		/// </summary>
		protected override Enum EnumItem
		{
			get
			{
				return this.moduelEnum;
			}
		}
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public new static System.Collections.ArrayList List()
        {
            return (new System.Collections.ArrayList(GetObjectItems(moduelHash)));
        }

	}

	/// <summary>
	/// ҩƷ���ʹ���ģ��ö��
	/// </summary>
    [Serializable]
	public enum EnumModuelType
	{
		/// <summary>
		/// ҩƷ
		/// </summary>
		Phamacy = 1,
		/// <summary>
		/// ����
		/// </summary>
		Material = 2,
		/// <summary>
		/// �̶��ʲ�
		/// </summary>
		Equipment = 3,
        /// <summary>
        /// Ѫ��
        /// </summary>
        Blood = 4,
		/// <summary>
		/// ȫ��
		/// </summary>
		All = 0,
	}
}
