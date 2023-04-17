using System;
using System.Collections.Generic;
using System.Text;

namespace Neusoft.HISFC.Models.Base
{
    #region ������Ŀ����

    [System.Serializable]
    public enum EnumNMSKindType
    {
        /// <summary>
        /// �ַ���
        /// </summary>
        String = 1,
        /// <summary>
        /// ��ֵ
        /// </summary>
        Int = 2,
        /// <summary>
        /// ����
        /// </summary>
        Date = 3,
        /// <summary>
        /// ��Ա
        /// </summary>
        Employee = 4,
        /// <summary>
        /// ����
        /// </summary>
        Department = 5,
        /// <summary>
        /// ����
        /// </summary>
        Area = 6,
        /// <summary>
        /// �Ƿ�
        /// </summary>
        IsOrNot = 7,
        /// <summary>
        /// ���
        /// </summary>
        Checks = 8,
        /// <summary>
        /// �Զ���
        /// </summary>
        Custom = 9

    }
    #endregion

    #region ������Ŀ���������ʶ

    [System.Serializable]
    public enum EnumNMSKindGradeState
    {
        /// <summary>
        /// ���������
        /// </summary>
        Not = 0,
        /// <summary>
        /// �ӷ�
        /// </summary>
        Add = 1,
        /// <summary>
        /// �۷�
        /// </summary>
        Reduce = 2
    }

    #endregion
}
