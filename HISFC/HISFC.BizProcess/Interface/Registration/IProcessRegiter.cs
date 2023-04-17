using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.BizProcess.Interface.Registration
{
    /// <summary>
    /// �ҺŴ���ӿ�{E43E0363-0B22-4d2a-A56A-455CFB7CF211}
    /// </summary>
    public interface IProcessRegiter
    {
        /// <summary>
        /// ���濪ʼʱ����
        /// </summary>
        /// <param name="regObj"></param>
        /// <param name="errText"></param>
        /// <returns></returns>
        int SaveBegin(ref FS.HISFC.Models.Registration.Register regObj, ref string errText);

        /// <summary>
        /// �������ʱ����
        /// </summary>
        /// <param name="regObj"></param>
        /// <param name="errText"></param>
        /// <returns></returns>
        int SaveEnd(ref FS.HISFC.Models.Registration.Register regObj, ref string errText);
    }

    /// <summary>
    /// �Һ�Ʊ��ӡ
    /// </summary>
    public interface IRegPrint
    {
        ///<summary>
        ///���ݿ�����
        ///</summary>
        System.Data.IDbTransaction Trans
        {
            get;
            set;
        }
        /// <summary>
        /// ��ֵ
        /// </summary>
        /// <param name="register"></param>
        /// <param name="reglvlfee"></param>
        /// <returns></returns>

        int SetPrintValue(FS.HISFC.Models.Registration.Register register);

        /// <summary>
        /// ��ӡԤ��
        /// </summary>
        /// <returns>>�ɹ� 1 ʧ�� -1</returns>
        int PrintView();
        /// <summary>
        /// ��ӡ
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>

        int Print();

        /// <summary>
        /// ��յ�ǰ��Ϣ
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        int Clear();

        /// <summary>
        /// ���ñ������ݿ�����
        /// </summary>
        /// <param name="trans">���ݿ�����</param>
        void SetTrans(System.Data.IDbTransaction trans);
    }

    /// <summary>
    /// �����ӡ// {AD405AA0-3101-46c0-A0B6-846DC3AAB10A}
    /// </summary>
    public interface IBraceletPrint
    {
        /// <summary>
        /// ��ֵ
        /// </summary>
        /// <param name="register"></param>
        /// <param name="reglvlfee"></param>
        /// <returns></returns>

        int SetPrintValue(FS.HISFC.Models.Registration.Register register);

        /// <summary>
        /// ��ӡ
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        void Print();

    }
    /// <summary>
    /// �Һ�Ʊ��ӡ
    /// </summary>
    public interface IShowLED
    {
        ///<summary>
        ///���ݿ�����
        ///</summary>
        //System.Data.IDbTransaction Trans
        //{
        //    get;
        //    set;
        //}
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="register"></param>
        /// <param name="reglvlfee"></param>
        /// <returns></returns>

        string Query();


        /// <summary>
        /// ��ʾfarpoint��ʽ
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>

        int SetFPFormat();

        /// <summary>
        ///  ����LED �ӿ� �����ʾ����LED
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        int CreateString();

        /// <summary>
        /// ���ñ������ݿ�����
        /// </summary>
        /// <param name="trans">���ݿ�����</param>
        //void SetTrans(System.Data.IDbTransaction trans);
    }

    /// <summary>
    /// ���ҿ��Ż��߲�����ʧ�ܴ���
    /// </summary>
    public interface ISearchCardNoFalse
    {
        /// <summary>
        /// ���ݿ���
        /// </summary>
        /// <returns></returns>
        int PostMarkNo(ref string markNo);
    }
}
