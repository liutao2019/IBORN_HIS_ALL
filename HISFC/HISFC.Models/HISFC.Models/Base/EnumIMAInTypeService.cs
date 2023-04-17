using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FS.HISFC.Models.Base
{
    /// <summary>
    /// [��������: ��������ϵͳ����ö�ٷ�����]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2007-08]<br></br>
    /// </summary>
    [System.Serializable]
    public class EnumIMAInTypeService : FS.HISFC.Models.Base.EnumServiceBase
    {
        static EnumIMAInTypeService()
        {
            itemCollection[EnumIMAInType.InnerApply] = "13";
            itemCollection[EnumIMAInType.OuterApply] = "12";
            itemCollection[EnumIMAInType.InnerBackApply] = "18";
            itemCollection[EnumIMAInType.OuterBackApply] = "17";
            itemCollection[EnumIMAInType.BorrowApply] = "14";
            itemCollection[EnumIMAInType.BorrowBack] = "15";
            itemCollection[EnumIMAInType.CommonInput] = "11";
            itemCollection[EnumIMAInType.ExamInput] = "1A";
            itemCollection[EnumIMAInType.ApproveInput] = "16";
            itemCollection[EnumIMAInType.BackInput] = "19";
            itemCollection[EnumIMAInType.SpecialInput] = "1C";
            itemCollection[EnumIMAInType.ProduceInput] = "1B";
        }

        EnumIMAInType inTypeEnum = EnumIMAInType.CommonInput;

        /// <summary>
        /// ����ö��ID
        /// </summary>
        protected static Hashtable itemCollection = new Hashtable();

        protected override System.Enum EnumItem
        {
            get
            {
                return this.inTypeEnum;
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
        public static EnumIMAInType GetEnumFromID(string typeID)
        {
            return (EnumIMAInType)(Enum.Parse(typeof(EnumIMAInType), typeID));
        }

        /// <summary>
        /// ����Name��ȡ��Ӧö����
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public static EnumIMAInType GetEnumFromName(string typeName)
        {
            foreach (EnumIMAInType inType in itemCollection.Keys)
            {
                if (itemCollection[inType].ToString() == typeName)
                {
                    return inType;
                }
            }

            return EnumIMAInType.CommonInput;
        }

        /// <summary>
        /// ����ö�ٻ�ȡ 
        /// </summary>
        /// <param name="inType"></param>
        /// <returns></returns>
        public static string GetNameFromEnum(EnumIMAInType inType)
        {
            return itemCollection[inType].ToString();
        }
    }

    public enum EnumIMAInType
    {
        /// <summary>
        /// �ڲ��������
        /// </summary>
        InnerApply,
        /// <summary>
        /// �ⲿ�������
        /// </summary>
        OuterApply,
        /// <summary>
        /// �ڲ�����˿�����
        /// </summary>
        InnerBackApply,
        /// <summary>
        /// �ⲿ����˿�����
        /// </summary>
        OuterBackApply,
        /// <summary>
        /// ������ҩ����
        /// </summary>
        BorrowApply,
        /// <summary>
        /// ������ҩȷ��
        /// </summary>
        BorrowBack,
        /// <summary>
        /// һ�����
        /// </summary>
        CommonInput,
        /// <summary>
        /// ��Ʊ���
        /// </summary>
        ExamInput,
        /// <summary>
        /// ��׼���
        /// </summary>
        ApproveInput,
        /// <summary>
        /// ����˿�
        /// </summary>
        BackInput,
        /// <summary>
        /// �������
        /// </summary>
        SpecialInput,
        /// <summary>
        /// �������
        /// </summary>
        ProduceInput
    }
}
