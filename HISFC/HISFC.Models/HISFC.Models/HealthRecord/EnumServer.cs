using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.Models.HealthRecord.EnumServer
{
    [Serializable]
    class EnumServer
    {
    }
    #region ����ö��

    #region ��������
    [Serializable]
    public enum ReportIndexs
    {
        /// <summary>
        ///���������� 
        /// </summary>
        NameIndex,
        /// <summary>
        ///�������࿨
        /// </summary>
        DeathIndex,
        /// <summary>
        /// ��Ժ�ֿƱ�
        /// </summary>
        DepartDept,
        /// <summary>
        /// ְ���첡���ʾ�����
        /// </summary>
        Zhidaoban,
        /// <summary>
        /// ����������
        /// </summary>
        DiseaseClassify,
        /// <summary>
        /// ����������
        /// </summary>
        OperationClassisy,
        /// <summary>
        /// ������ǰƽ��סԺ��ͳ��
        /// </summary>
        BeforeODept,
        /// <summary>
        /// ��������ƽ��סԺ��ͳ��
        /// </summary>
        AfterODept,
        /// <summary>
        ///������������ͳ�Ʊ�
        /// </summary>
        BeforeOperation,
        /// <summary>
        /// һ���ڸ���Ժ
        /// </summary>
        ComeBackInWeek,
        /// <summary>
        /// ��Ⱦ��
        /// </summary>
        Infection,
        /// <summary>
        /// ����ʹ��Ƶ��
        /// </summary>
        CaseUserfrequence,
        /// <summary>
        /// ҽ��ʹ�������Ƶ��ͳ�Ʊ�
        /// </summary>
        DoctorUserfrequence,
        /// <summary>
        /// ¼����Ա������ͳ�� 
        /// </summary>
        InputPerson,
        /// <summary>
        /// ��ϱ�����Ա������ͳ��
        /// </summary>
        ICDDiagPerson,
        /// <summary>
        /// ��ϱ�����Ա������ͳ�Ʊ�2
        /// </summary>
        DiagCoding,
        /// <summary>
        /// ���ַ�����ת��ͳ�Ʊ�
        /// </summary>
        DiseaseAndOutState,
        /// <summary>
        /// ��������ת��ͳ�Ʊ�
        /// </summary>
        TumourDiseaseAndOutState,
        /// <summary>
        /// �������ľ����˹�����ͳ�Ʊ� 
        /// </summary>
        BorrowCase,
        /// <summary>
        /// ��������Ա������ͳ��
        /// </summary>
        BackUpCase,
        /// <summary>
        /// ����������Ա������ͳ�Ʊ�1
        /// </summary>
        OperationCoding1,
        /// <summary>
        /// ����������Ա������ͳ�Ʊ�1
        /// </summary>
        OperationCoding2,

    }
    #endregion

    #region ��������
    [Serializable]
    public enum frmTypes
    {
        /// <summary>
        /// ҽ��վ����
        /// </summary>
        DOC,
        /// <summary>
        /// �����ҵ���
        /// </summary>
        CAS
    }
    #endregion

    #region   ��������
    [Serializable]
    public enum FormStyleInfo
    {
        /// <summary>
        /// ����
        /// </summary>
        Normal,
        /// <summary>
        /// ˫���Զ��ر�
        /// </summary>
        DCAutoClose
    }
    #endregion

    #region  �������ö��  ICD���ͷֱ���ICD10, ICD 9, ICD����
    /// <summary>
    /// ICD���ͷֱ���ICD10, ICD 9, ICD����
    /// </summary>
    /// Creator: zhangjunyi@FS.com  2005/05/30
    [Serializable]
    public enum ICDTypes //ICD���ͷֱ���ICD10, ICD 9, ICD����
    {
        None, //ʲô����
        ICD10, // ICD10
        ICD9,  // ICD 9
        ICDOperation// ����ICD
    }
    #endregion

    #region  ��ѯ����ö�� ,��ѯ���ͷֱ�Ϊ����(All), ��Ч(Valid) ����(Cancel)
    /// <summary>
    /// ��ѯ���ͷֱ�Ϊ����(All), ��Ч(Valid) ����(Cancel)
    /// </summary>
    /// Creator: zhangjunyi@FS.com  2005/05/30
    [Serializable]
    public enum QueryTypes //��ѯ���ͷֱ�Ϊ����(All), ��Ч(Valid) ����(Cancel)
    {
        All,  //����
        Valid, //��Ч
        Cancel //����
    }
    #endregion

    #region ���� ���ӣ��޸ģ�ͣ��
    [Serializable]
    public enum EditTypes
    {
        Add, //����
        Modify,//�޸�
        Delete,//ɾ��
        Disuse //����
    }
    # endregion
    [Serializable]
    public enum SelectTypes
    {
        /// <summary>
        /// ��������
        /// </summary>
        DEPT,
        /// <summary>
        /// ��������
        /// </summary>
        EMPOYE
    }
    /// <summary>
    ///Ҫ��ѯ�ı� 
    /// </summary>
    [Serializable]
    public enum TablesName
    {
        /// <summary>
        /// ��
        /// </summary>
        NONE,
        /// <summary>
        /// ������ҳ
        /// </summary>
        BASE,
        /// <summary>
        /// ��ͬһ����Ժ �и���סԺ��ˮ�Ż�ȡסԺ�ţ��ٲ�ѯסԺ����ͬ��סԺ��ˮ��
        /// </summary>
        BASESUB,
        /// <summary>
        /// ��ϱ�
        /// </summary>
        DIAG,
        /// <summary>
        /// ��ϱ� �ҵ����
        /// </summary>
        DIAGSINGLE,
        /// <summary>
        /// ������
        /// </summary>
        OPERATION,
        /// <summary>
        /// ������
        /// </summary>
        OPERATIONSINGLE,
        /// <summary>
        /// ������ҳ�� ��ϱ� 
        /// </summary>
        BASEANDDIAG,
        /// <summary>
        /// ������ҳ��������
        /// </summary>
        BASEANDOPERATION,
        /// <summary>
        /// ��Ϻ�����
        /// </summary>
        DIAGANDOPERATION,
        /// <summary>
        /// ������ҳ ��������ϱ�
        /// </summary>
        BASEANDDIAGANDOPERATION
    }
    [Serializable]
    public enum LendType
    {
        /// <summary>
        /// �����Ѿ����
        /// </summary>
        O,
        /// <summary>
        /// �����ڼ� 
        /// </summary>
        I
    }
    #endregion 
}
