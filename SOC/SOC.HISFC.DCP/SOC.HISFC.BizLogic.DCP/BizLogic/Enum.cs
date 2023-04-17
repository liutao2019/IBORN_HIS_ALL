using System;
using System.Collections.Generic;
using System.Text;

namespace FS.SOC.HISFC.DCP.Enum
{
    /// <summary>
    /// ��������
    /// </summary>
    public enum PatientType
    {
        /// <summary>
        /// סԺ����
        /// </summary>
        I,

        /// <summary>
        /// ���ﻼ��
        /// </summary>
        C,

        /// <summary>
        /// ����
        /// </summary>
        O
    }

    /// <summary>
    /// ���濨״̬
    /// </summary>
    public enum ReportState
    {
        /// <summary>
        /// ����
        /// </summary>
        New = 0,

        /// <summary>
        /// �ϸ�
        /// </summary>
        Eligible = 1,

        /// <summary>
        /// ���ϸ�
        /// </summary>
        UnEligible = 2,

        /// <summary>
        /// ����������
        /// </summary>
        OwnCancel = 3,

        /// <summary>
        /// ����������
        /// </summary>
        Cancel = 4
    }

    /// <summary>
    ///  �����������
    /// </summary>
    public enum ReportOperResult
    {
        /// <summary>
        /// ��ɱ���
        /// </summary>
        OK,
        /// <summary>
        /// ȡ���˱���
        /// </summary>
        Cancel,
        /// <summary>
        /// ����[���磺����CheckDiseaseʱ��Ӧ��ϲ���Ҫ����]
        /// </summary>
        Other
    }
}
