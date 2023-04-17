using System;
using System.Collections.Generic;
using System.Text;

namespace Neusoft.HISFC.BizProcess.Integrate.Common
{
    /// <summary>
    /// ����ά���ӿ�
    /// </summary>
    public interface IControlParamMaint 
    {
        /// <summary>
        /// ���Ʋ���ά��UC������
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// ������Ϣ
        /// </summary>
        string ErrText { get; set; }

        /// <summary>
        /// ��ʼ������
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        int Init();

        /// <summary>
        /// Ӧ��
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        int Apply();

        /// <summary>
        /// ����
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        int Save();

        /// <summary>
        /// �Ƿ���ʾUC�Դ�����,�˳���ť��
        /// </summary>
        bool IsShowOwnButtons { get; set;}

        /// <summary>
        /// �Ƿ�����˲�����Ϣ
        /// </summary>
        bool IsModify { get; set; }
    }

    /// <summary>
    /// ��ʾ��Ϣ�ӿڣ�����HIS����/ע������Զ���ʾ��Ϣ
    /// 
    /// {5BE03DF2-25DE-4e7a-9B47-85CE92911277} �޸Ľӿڶ���
    /// </summary>
    public interface INoticeManager
    {
        /// <summary>
        /// ��Ϣ��ʾ
        /// </summary>
        /// <returns></returns>
        int Notice();

        /// <summary>
        /// �ƻ���ʾ
        /// </summary>
        /// <returns></returns>
        int Schedule();

        /// <summary>
        /// ��ʾ��ʾ
        /// </summary>
        /// <returns></returns>
        int Warn();

        /// <summary>
        /// HISϵͳע��ǰ��ʾ��Ϣ
        /// </summary>
        /// <returns></returns>
        int MsgOnLogout();
    }

    /// <summary>
    /// ������ýӿ�
    /// </summary>
    public interface ISetup 
    {
        /// <summary>
        /// �ָ�Ĭ��(����)
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        int Default();
        
        /// <summary>
        /// �Ƿ����ý���Ϊ����
        /// </summary>
        bool IsWindow { get; }

        /// <summary>
        /// ����ƽ���
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        int Show();
    }

    /// <summary>
    /// Ԥ��
    /// </summary>
    public interface IPreview 
    {
        /// <summary>
        /// Ԥ��
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        int Preview();

        /// <summary>
        /// ��ǰԤ���ؼ�
        /// </summary>
        System.Windows.Forms.Control PreviewControl { get; }
    }
}
