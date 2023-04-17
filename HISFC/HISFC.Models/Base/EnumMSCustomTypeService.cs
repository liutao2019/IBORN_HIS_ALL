using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FS.HISFC.Models.Base
{
    /// <summary>
    /// [��������: ҩƷ�Զ����½�����ö�ٷ�����]<br></br>
    /// [�� �� ��: ��˹]<br></br>
    /// [����ʱ��: 2007-07-30]<br></br>
    /// </summary>

    [System.Serializable]
    public class EnumMSCustomTypeService : EnumServiceBase
    {

        static EnumMSCustomTypeService()
        {
            itemCollection[EnumMSCustomType.���] = "0310";
            itemCollection[EnumMSCustomType.����] = "0320";
            itemCollection[EnumMSCustomType.�̵�] = "0305";
            itemCollection[EnumMSCustomType.����] = "0304";
            itemCollection[EnumMSCustomType.����] = "1000";
            itemCollection[EnumMSCustomType.���ﻼ����ҩ] = "2001";
            itemCollection[EnumMSCustomType.���ﻼ����ҩ] = "2002";
            itemCollection[EnumMSCustomType.סԺ������ҩ] = "2101";
            itemCollection[EnumMSCustomType.סԺ������ҩ] = "2102";
            itemCollection[EnumMSCustomType.С��] = "SUB";
        }

        EnumMSCustomType msCustomType = EnumMSCustomType.���;

        /// <summary>
        /// ����ö��ID
        /// </summary>
        protected static Hashtable itemCollection = new Hashtable();

        protected override System.Enum EnumItem
        {
            get
            {
                return this.msCustomType;
            }
        }

        protected override System.Collections.Hashtable Items
        {
            get
            {
                return itemCollection;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public new static ArrayList List()
        {
            return (new ArrayList(GetObjectItems(itemCollection)));
        }

        /// <summary>
        /// ����ID��ȡ��Ӧö����
        /// </summary>
        /// <param name="typeID"></param>
        /// <returns></returns>
        public static EnumMSCustomType GetEnumFromID(string typeID)
        {
            return (EnumMSCustomType)(Enum.Parse(typeof(EnumMSCustomType), typeID));
        }

        /// <summary>
        /// ����Name��ȡ��Ӧö����
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public static EnumMSCustomType GetEnumFromName(string typeName)
        {
            foreach (EnumMSCustomType customType in itemCollection.Keys)
            {
                if (itemCollection[customType].ToString() == typeName)
                {
                    return customType;
                }
            }

            return EnumMSCustomType.���;
        }

        /// <summary>
        /// ����ö�ٻ�ȡ 
        /// </summary>
        /// <param name="msCustomType"></param>
        /// <returns></returns>
        public static string GetNameFromEnum(EnumMSCustomType msCustomType)
        {
            return itemCollection[msCustomType].ToString();
        }
    }

    /// <summary>
    /// ҩƷ�Զ����½�����
    /// </summary>
    public enum EnumMSCustomType
    {
        ���,
        ����,
        �̵�,
        ����,
        ����,
        ���ﻼ����ҩ,
        ���ﻼ����ҩ,
        סԺ������ҩ,
        סԺ������ҩ,
        С��
    }
}
