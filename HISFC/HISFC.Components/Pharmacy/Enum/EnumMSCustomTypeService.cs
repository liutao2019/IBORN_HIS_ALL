using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace UFC.Pharmacy.Enum
{
    /// <summary>
    /// [��������: ҩƷ�Զ����½�����ö�ٷ�����]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2007-07-30]<br></br>
    /// </summary>
    internal class EnumMSCustomTypeService : Neusoft.HISFC.Object.Base.EnumServiceBase
    {

        static EnumMSCustomTypeService()
        {
            items[EnumMSCustomType.���] = "0310";
            items[EnumMSCustomType.����] = "0320";
            items[EnumMSCustomType.�̵�] = "0305";
            items[EnumMSCustomType.����] = "0304";
            items[EnumMSCustomType.����] = "1000";
            items[EnumMSCustomType.���ﻼ����ҩ] = "2001";
            items[EnumMSCustomType.���ﻼ����ҩ] = "2002";
            items[EnumMSCustomType.סԺ������ҩ] = "2101";
            items[EnumMSCustomType.סԺ������ҩ] = "2102";
            items[EnumMSCustomType.С��] = "SUB";
        }

        EnumMSCustomType msCustomType = EnumMSCustomType.���;

        /// <summary>
        /// ����ö��ID
        /// </summary>
        protected static Hashtable items = new Hashtable();

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
                return items;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public new static ArrayList List()
        {
            return (new ArrayList(GetObjectItems(items)));
        }
    }

    /// <summary>
    /// ҩƷ�Զ����½�����
    /// </summary>
    internal enum EnumMSCustomType
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
