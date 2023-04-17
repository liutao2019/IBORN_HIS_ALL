using System;
using System.Collections;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Models.Base
{
	/// <summary>
	/// SexEnumService <br></br>
	/// [��������: ������]<br></br>
	/// [�� �� ��: s lion h]<br></br>
	/// [����ʱ��: 2021-05-26]<br></br>
    /// {6F68AB52-332C-4efa-A6DD-F6BDB37B1283}
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��=''
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    [System.Serializable]
    public class ManageEnumService : EnumServiceBase
	{
        public ManageEnumService()
        {
            items[EnumManageClass.I] = "I��";
            items[EnumManageClass.II] = "II��";
            items[EnumManageClass.III] = "III��";
            items[EnumManageClass.NM] = "����Ϊҽ����е����";
            items[EnumManageClass.N] = "��";
        }

		#region ����

		/// <summary>
		/// �������
		/// </summary>
        EnumManageClass enumManageClass;

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
                return this.enumManageClass;
			}
		}

		#endregion

		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns></returns>
        public new ManageEnumService Clone()
		{
            ManageEnumService enumService = base.Clone() as ManageEnumService;
            
			return enumService;
		}

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public new static ArrayList List()
        {
            return (new ArrayList( GetObjectItems(items)));
        }
	}



    /// <summary>
    /// ���Ĺ������
    /// </summary>
    public enum EnumManageClass
    {
        /// <summary>
        /// I��
        /// </summary>
        I = 1,
        /// <summary>
        /// II��
        /// </summary>
        II = 2,
        /// <summary>
        /// III��
        /// </summary>
        III = 3,
        /// <summary>
        /// ����Ϊҽ����е����
        /// </summary>
        NM = 4,
        /// <summary>
        /// ��
        /// </summary>
        N = 5
    }
}
