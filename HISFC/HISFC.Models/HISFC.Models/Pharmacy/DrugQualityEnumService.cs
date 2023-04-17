using System;
using System.Collections;

namespace FS.HISFC.Models.Pharmacy
{
	/// <summary>
	/// [��������: ϵͳҩƷ���ʹ�����]<br></br>
	/// [�� �� ��: ����]<br></br>
	/// [����ʱ��: 2004-12]<br></br>
	/// <�޸ļ�¼
	///		�޸���='������'
	///		�޸�ʱ��='2006-09-13'
	///		�޸�Ŀ��='ϵͳ�ع�'
	///		�޸�����='�����淶����'
	///  />
	///  ID �������
	/// </summary>
    [Serializable]
    public class DrugQualityEnumService : FS.HISFC.Models.Base.EnumServiceBase
	{
        public DrugQualityEnumService()
		{
            this.Items[EnumQuality.O] = "��ҩ";
            this.Items[EnumQuality.S] = "��ҩ";
            this.Items[EnumQuality.V] = "��ҩ";
            this.Items[EnumQuality.T] = "����Һ";
            this.Items[EnumQuality.P] = "����ҩƷ";
            this.Items[EnumQuality.E] = "����";
		}

        #region ����

        /// <summary>
        /// �������
        /// </summary>
        EnumQuality enumQuality;

        /// <summary>
        /// �洢ö�ٶ���
        /// </summary>
        protected static Hashtable items = new Hashtable();

        #endregion

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
                return this.enumQuality;
            }
        }


        /// <summary>
        /// ��������  ���ȫ���б�
        /// </summary>
        /// <returns>ArrayList(Quality)</returns>
        public static ArrayList List()
        {
            return (new ArrayList(GetObjectItems(items)));
        }
}

    /// <summary>
    /// ϵͳ����ҩƷ����
    /// </summary>
    public enum EnumQuality
    {
        /// <summary>
        /// ��ҩ
        /// </summary>
        O = 0,
        /// <summary>
        /// ��ҩ
        /// </summary>
        S = 1,
        /// <summary>
        /// ��ҩ
        /// </summary>
        V = 2,
        /// <summary>
        /// ����Һ
        /// </summary>
        T = 3,
        /// <summary>
        /// ����ҩƷ
        /// </summary>
        P = 4,
        /// <summary>
        /// ����
        /// </summary>
        E = 5
    }	
}
