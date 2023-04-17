using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FS.HISFC.Models.Pharmacy
{
    /// <summary>
    /// TerminalTypeEnumService<br></br>
    /// [��������: �����ӡ����ö�ٷ�����]<br></br>
    /// [�� �� ��: ��˹]<br></br>
    /// [����ʱ��: 2008-01]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    [Serializable]
    public class ClinicPrintTypeEnumService : FS.HISFC.Models.Base.EnumServiceBase
    {
        static ClinicPrintTypeEnumService()
        {
            items[EnumClinicPrintType.��ǩ] = "��ǩ";
            items[EnumClinicPrintType.��չ] = "��չ";
            items[EnumClinicPrintType.�嵥] = "�嵥";
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
    /// �����ն˴�ӡ����ö��
    /// </summary>
    public enum EnumClinicPrintType
    {
        ��ǩ = 0,
        �嵥 = 1,
        ��չ = 2
    }
}
