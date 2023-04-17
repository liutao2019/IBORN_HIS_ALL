using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Neusoft.HISFC.Models.Medical
{
    public class AllergyTypeEnumService : Neusoft.HISFC.Models.Base.EnumServiceBase
    {
        #region ���췽��

        static AllergyTypeEnumService()
        {
            items[AllergyType.DA] = "ҩ�����";
            items[AllergyType.FA] = "ʳ�����";
            items[AllergyType.MA] = "����͹���";
            items[AllergyType.MC] = "����ͽ���";
        }

        #endregion

        #region �ֶ�

        AllergyType enumAllergyType;

        protected static Hashtable items = new Hashtable();

        #endregion

        #region ����

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
                return this.enumAllergyType;
            }
        }

        protected override Enum DefaultItem
        {
            get
            {
                return AllergyType.DA;
            }
        }

        #endregion

        public new static ArrayList List()
        {
            return (new ArrayList(GetObjectItems(items)));
        }
    }

    /// <summary>
    /// ��������
    /// </summary>
    public enum AllergyType
    {
        /// <summary>
        /// ҩ�����
        /// </summary>
        DA = 0,
        /// <summary>
        /// ʳ�����
        /// </summary>
        FA = 1,
        /// <summary>
        /// ����͹���
        /// </summary>
        MA = 2,
        /// <summary>
        /// ����ͽ���
        /// </summary>
        MC = 3
    }
}
