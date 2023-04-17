using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Neusoft.HISFC.Object.Sanitize
{
    /// <summary>
    /// SanLogOperTypeEnumService<br></br>
    /// [��������: ��������ö�ٷ�����]<br></br>
    /// [�� �� ��: SHIZJ]<br></br>
    /// [����ʱ��: 2008-08]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public class SanLogOperTypeEnumService : Neusoft.HISFC.Object.Base.EnumServiceBase
    {
        static SanLogOperTypeEnumService()
        {
            items[LogType.Add] = "���";
            items[LogType.Del] = "ɾ��";
            items[LogType.Edit] = "�޸�";
            items[LogType.Query] = "��ѯ";
        }

        LogType logType;

        protected static Hashtable items = new Hashtable();

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
                return this.logType;
            }
        }

        public new static ArrayList list()
        {
            return (new ArrayList(GetObjectItems(items)));
        }
    }
    /// <summary>
    /// ��������1���2ɾ��3�޸�4��ѯ
    /// </summary>
    public enum LogType
    {
        /// <summary>
        /// ���
        /// </summary>
        Add = 1,
        /// <summary>
        /// ɾ��
        /// </summary>
        Del = 2,
        /// <summary>
        /// �޸�
        /// </summary>
        Edit = 3,
        /// <summary>
        /// ��ѯ
        /// </summary>
        Query = 4
    }
}
