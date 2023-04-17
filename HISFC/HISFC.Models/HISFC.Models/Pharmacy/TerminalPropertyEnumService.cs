using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.Models.Pharmacy
{
    [Serializable]
    public class TerminalPropertyEnumService : FS.HISFC.Models.Base.EnumServiceBase
    {
        static TerminalPropertyEnumService()
        {
            items[EnumTerminalProperty.��ͨ] = "��ͨ";
            items[EnumTerminalProperty.ר��] = "ר��";
            items[EnumTerminalProperty.����] = "����";
        }

        EnumTerminalProperty enumProperty;

        protected static System.Collections.Hashtable items = new System.Collections.Hashtable();

        protected override System.Collections.Hashtable Items
        {
            get {
                return items;
            }
        }

        protected override Enum EnumItem
        {
            get 
            {
                return enumProperty;
            }
        }

        /// <summary>
        /// �����б�
        /// </summary>
        /// <returns></returns>
        public new static System.Collections.ArrayList List()
        {
            return (new System.Collections.ArrayList(GetObjectItems(items)));
        }
    }

    /// <summary>
    /// �����ն�����ö�� 
    /// </summary>
    public enum EnumTerminalProperty
    {
        ��ͨ = 0,
        ר�� = 1,
        ���� = 2
    };
}
