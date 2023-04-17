using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Neusoft.HISFC.Models.Medical
{
    /// <summary>
    /// [��������: ҽ���Ű����ö�ٷ�����]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2006-11]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    [Serializable]
    public class AttendTypeEnumService
    {
        static AttendTypeEnumService()
        {
            items[AttendType.FirstAttend] = "һֵ";
            items[AttendType.SecondAttend] = "��ֵ";
            items[AttendType.ThirdAttend] = "��ֵ";
            items[AttendType.GeneralShift] = "�ܰ�";
            items[AttendType.FestivalAttend] = "�ڼ���";
            items[AttendType.DutyAttend] = "����";
        }

        static Hashtable items = new Hashtable();

        public static ArrayList List()
        {
            ArrayList list=new ArrayList();
            foreach (DictionaryEntry de in items)
            {
                Neusoft.FrameWork.Models.NeuObject obj = new Neusoft.FrameWork.Models.NeuObject();
                obj.ID = de.Key.ToString();
                obj.Name = de.Value.ToString();
                list.Add(obj);
            }
            return list;
        }

        public static String[] StringItems()
        {
            string[] arrStr = new string[items.Count];
            Array arr = List().ToArray();
            for (int i = 0; i < items.Count; i++)
            {
                arrStr[i] = arr.GetValue(i).ToString();
            }

            return arrStr;
        }

        public static string GetName(AttendType attendType)
        {
            return items[attendType].ToString();
        }

        public static AttendType GetID(string Name)
        {
            foreach (DictionaryEntry de in items)
            {
                if (de.Value.ToString() == Name)
                {
                    return (AttendType)de.Key;
                }
            }
            return AttendType.FirstAttend;
        }

    }

    /// <summary>
    /// �Ű����
    /// </summary>
    public enum AttendType
    {
        /// <summary>
        /// һֵ
        /// </summary>
        FirstAttend = 0,
        /// <summary>
        /// ��ֵ
        /// </summary>
        SecondAttend = 1,
        /// <summary>
        /// ��ֵ
        /// </summary>
        ThirdAttend = 2,
        /// <summary>
        /// �ܰ�
        /// </summary>
        GeneralShift = 3,
        /// <summary>
        /// �ڼ���
        /// </summary>
        FestivalAttend = 4,
        /// <summary>
        /// ����
        /// </summary>
        DutyAttend = 5
    }

    /// <summary>
    /// ���ڴ���
    /// </summary>
    public enum AttendanceType
    {
        /// <summary>
        /// ����
        /// </summary>
        Assignment,
        /// <summary>
        /// �Ӱ�
        /// </summary>
        Overtime,
        /// <summary>
        /// ����
        /// </summary>
        OffRest,
        /// <summary>
        /// ����
        /// </summary>
        BreakRest,
        /// <summary>
        /// ҹ��
        /// </summary>
        NightShift,
        /// <summary>
        /// �ڼ���
        /// </summary>
        Holidays,
        /// <summary>
        /// ȱ��
        /// </summary>
        Absence,

        /// <summary>
        /// ��������{D06DDE4E-9595-4b52-A3DE-DA22B4E5A792}
        /// </summary>
        Normal

    }

    /// <summary>
    /// ���ڴ���
    /// </summary>
    public class AttendanceTypeEnumService
    {
        static AttendanceTypeEnumService()
        {
            items[AttendanceType.Assignment] = "����";
            items[AttendanceType.Overtime] = "�Ӱ�";
            items[AttendanceType.OffRest] = "����";
            items[AttendanceType.BreakRest] = "����";
            items[AttendanceType.Holidays] = "�ڼ���";
            items[AttendanceType.NightShift] = "ҹ��";
            items[AttendanceType.Absence] = "ȱ��";
            /// ��������{D06DDE4E-9595-4b52-A3DE-DA22B4E5A792}
            items[AttendanceType.Normal] = "��������";
        }

        static Hashtable items = new Hashtable();

        public static ArrayList List()
        {
            ArrayList list=new ArrayList();
            foreach (DictionaryEntry de in items)
            {
                Neusoft.FrameWork.Models.NeuObject obj = new Neusoft.FrameWork.Models.NeuObject();
                obj.ID = de.Key.ToString();
                obj.Name = de.Value.ToString();
                list.Add(obj);
            }
            return list;
        }

        public static String[] StringItems()
        {
            string[] arrStr = new string[items.Count];
            Array arr = List().ToArray();
            for (int i = 0; i < items.Count; i++)
            {
                arrStr[i] = arr.GetValue(i).ToString();
            }

            return arrStr;
        }

        public static string GetName(AttendanceType ID)
        {
            return items[ID].ToString();
        }

        public static AttendanceType GetID(string Name)
        {
            foreach (DictionaryEntry de in items)
            {
                if (de.Value.ToString() == Name)
                {
                    return (AttendanceType)de.Key;
                }
            }

            return AttendanceType.Assignment;
        }
    }
}
