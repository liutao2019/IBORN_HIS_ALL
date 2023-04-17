using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

/// <summary>
/// SanQCItemTypeEnumService<br></br>
/// [��������: ����������Ŀ����ö�ٷ�����]<br></br>
/// [�� �� ��: SHIZJ]<br></br>
/// [����ʱ��: 2008-08]<br></br>
/// <�޸ļ�¼
///		�޸���=''
///		�޸�ʱ��='yyyy-mm-dd'
///		�޸�Ŀ��=''
///		�޸�����=''
///  />
/// </summary>
namespace Neusoft.HISFC.Object.Sanitize
{
    public class SanQCItemTypeEnumService : Neusoft.HISFC.Object.Base.EnumServiceBase
    {
        static SanQCItemTypeEnumService()
        {
            items[QCItemTypes.�ַ���] = "�ַ���";
            items[QCItemTypes.����] = "����";
            items[QCItemTypes.С��] = "С��";
            items[QCItemTypes.����] = "����";
            items[QCItemTypes.��Ա] = "��Ա";
            items[QCItemTypes.����] = "����";
        }

        QCItemTypes qcItemType;

        protected static Hashtable items = new Hashtable();

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
                return this.qcItemType;
            }
        }

        public new static ArrayList List()
        {
            return (new ArrayList(GetObjectItems(items)));
        }


    }
      
             //<summary>
         //��Ŀ����1�ַ���2����3С��4����5��Ա6����
         //</summary>
        public enum QCItemTypes
        {
            /// <summary>
            /// 1�ַ���
            /// </summary>
            �ַ��� = 1,
            /// <summary>
            /// 2����
            /// </summary>
            ���� = 2,
            /// <summary>
            /// 3С��
            /// </summary>
            С�� = 3,
            /// <summary>
            /// 4����
            /// </summary>
            ���� = 4,
            /// <summary>
            /// 5��Ա
            /// </summary>
            ��Ա = 5,
            /// <summary>
            /// 6����
            /// </summary>
            ���� = 6
        }
}

