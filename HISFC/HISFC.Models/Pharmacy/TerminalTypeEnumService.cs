using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FS.HISFC.Models.Pharmacy
{
    /// <summary>
    /// TerminalTypeEnumService<br></br>
    /// [��������: �����ն����ö�ٷ�����]<br></br>
    /// [�� �� ��: ��˹]<br></br>
    /// [����ʱ��: 2006-11]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    [Serializable]
    public class TerminalTypeEnumService : FS.HISFC.Models.Base.EnumServiceBase
    {
        static TerminalTypeEnumService()
        {
            items[EnumTerminalType.��ҩ̨] = "��ҩ̨";
            items[EnumTerminalType.��ҩ����] = "��ҩ����";
        }

        EnumTerminalType enumType;

        /// <summary>
        /// ����ö������
        /// </summary>
        protected static Hashtable items = new Hashtable();

        protected override System.Collections.Hashtable Items
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
                return enumType;
            }
        }

        /// <summary>
        /// �����б�
        /// </summary>
        /// <returns></returns>
        public new static ArrayList List()
        {
            return (new ArrayList(GetObjectItems(items)));
        }
    }

    /// <summary>
    /// �����ն�����ö��
    /// </summary>
    public enum EnumTerminalType
    {
        ��ҩ���� = 0,
        ��ҩ̨ = 1
    }
}
