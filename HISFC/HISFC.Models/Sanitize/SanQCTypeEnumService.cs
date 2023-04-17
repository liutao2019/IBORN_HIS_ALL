using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

/// <summary>
/// SanQCItemTypeEnumService<br></br>
/// [��������: ��������ö��]<br></br>
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
    public class SanQCTypeEnumService:Neusoft.HISFC.Object.Base.EnumServiceBase
    {

        static SanQCTypeEnumService()
        {
            items[QCTypes.����] = "����";
            items[QCTypes.����] = "����";
        }

        QCTypes qcType;

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
                return this.qcType;
            }
        }

        public new static ArrayList List()
        {
            return (new ArrayList(GetObjectItems(items)));
        }
    }

    //<summary>
    //��������01����
    //</summary>
    public enum QCTypes
    {
        /// <summary>
        /// 01����
        /// </summary>
        ���� = 01,
        /// <summary>
        /// 02����
        /// </summary>
        ���� = 02
    }
}
