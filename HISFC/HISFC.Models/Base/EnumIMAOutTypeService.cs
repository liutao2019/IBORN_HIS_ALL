using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace  FS.HISFC.Models.Base
{
    /// <summary>
    /// [��������: ��������ϵͳ����ö�ٷ�����]<br></br>
    /// [�� �� ��: ��˹]<br></br>
    /// [����ʱ��: 2007-08]<br></br>
    /// </summary>
    [System.Serializable]
    public class EnumIMAOutTypeService : EnumServiceBase
    {
        static EnumIMAOutTypeService()
        {
            itemCollection[EnumIMAOutType.CommonOutput] = "21";
            itemCollection[EnumIMAOutType.BackOutput] = "22";
            itemCollection[EnumIMAOutType.BorrowExamOutput] = "23";
            itemCollection[EnumIMAOutType.ApplyOutput] = "24";
            itemCollection[EnumIMAOutType.ExamOutput] = "25";

            itemCollection[EnumIMAOutType.SpecialOutput] = "26";
            itemCollection[EnumIMAOutType.InnerBackOutput] = "27";

            itemCollection[EnumIMAOutType.TransferOutput] = "29";
            itemCollection[EnumIMAOutType.ChangeOutput] = "30";
            itemCollection[EnumIMAOutType.ProduceOutput] = "31";

            itemCollection[EnumIMAOutType.OutpatientOutput] = "M1";
            itemCollection[EnumIMAOutType.OutpatientBackOutput] = "M2";
            itemCollection[EnumIMAOutType.InpatientOutput] = "Z1";
            itemCollection[EnumIMAOutType.InpatientBackOutput] = "Z2";
        }

        EnumIMAOutType outTypeEnum = EnumIMAOutType.CommonOutput;

        /// <summary>
        /// ����ö��ID
        /// </summary>
        protected static Hashtable itemCollection = new Hashtable();

        protected override System.Enum EnumItem
        {
            get
            {
                return this.outTypeEnum;
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
        public static EnumIMAOutType GetEnumFromID(string typeID)
        {
            return (EnumIMAOutType)(Enum.Parse(typeof(EnumIMAOutType), typeID));
        }

        /// <summary>
        /// ����Name��ȡ��Ӧö����
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public static EnumIMAOutType GetEnumFromName(string typeName)
        {
            foreach (EnumIMAOutType inType in itemCollection.Keys)
            {
                if (itemCollection[inType].ToString() == typeName)
                {
                    return inType;
                }
            }

            return EnumIMAOutType.CommonOutput;
        }

        /// <summary>
        /// ����ö�ٻ�ȡ 
        /// </summary>
        /// <param name="inType"></param>
        /// <returns></returns>
        public static string GetNameFromEnum(EnumIMAOutType inType)
        {
            return itemCollection[inType].ToString();
        }
    }

    public enum EnumIMAOutType
    {
        /// <summary>
        /// һ����� 21
        /// </summary>
        CommonOutput,
        /// <summary>
        /// �����˿� 22
        /// </summary>
        BackOutput,
        /// <summary>
        /// ��ҩ��� 23
        /// </summary>
        BorrowExamOutput,
        /// <summary>
        /// �������� 24
        /// </summary>
        ApplyOutput,
        /// <summary>
        /// �������� 25
        /// </summary>
        ExamOutput,
        /// <summary>
        /// ������� 26
        /// </summary>
        SpecialOutput,
        /// <summary>
        /// �ڲ���ҩȷ�� 27
        /// </summary>
        InnerBackOutput,
        /// <summary>
        /// ���ó���    29 
        /// </summary>
        TransferOutput,
        /// <summary>
        /// ��ҩ����    30 ��ʵ��
        /// </summary>
        ChangeOutput,
        /// <summary>
        /// ��������    31
        /// </summary>
        ProduceOutput,
        /// <summary>
        /// �����ҩ M1
        /// </summary>
        OutpatientOutput,
        /// <summary>
        /// ������ҩ M2
        /// </summary>
        OutpatientBackOutput,
        /// <summary>
        /// סԺ��ҩ Z1
        /// </summary>
        InpatientOutput,
        /// <summary>
        /// סԺ��ҩ Z2
        /// </summary>
        InpatientBackOutput
    }
}
