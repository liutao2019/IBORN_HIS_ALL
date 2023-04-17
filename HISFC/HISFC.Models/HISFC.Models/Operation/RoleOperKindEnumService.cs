using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace FS.HISFC.Models.Operation
{
    /// <summary>
    /// [��������: ������Ա��ɫ״̬��Ŀǰֻ������������ã�]<br></br>
    /// [�� �� ��: ����ȫ]<br></br>
    /// [����ʱ��: 2006-12-11]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    [Serializable]
    public class RoleOperKindEnumService : Base.EnumServiceBase
    {
        static RoleOperKindEnumService()
		{
            items[EnumRoleOperKind.ZC] = "����";
            items[EnumRoleOperKind.ZL] = "ֱ��";
            items[EnumRoleOperKind.JB] = "�Ӱ�";
		}
		EnumRoleOperKind enumItem;
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
				return this.enumItem;
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

    public enum EnumRoleOperKind
    {
        /// <summary>
        ///����
        /// </summary>
        ZC = 1,
        /// <summary>
        ///ֱ��
        /// </summary>
        ZL = 2,
        /// <summary>
        ///�Ӱ�
        /// </summary>
        JB = 3
    };
}
