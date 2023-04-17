using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.BizProcess.Interface
{
    /// <summary>
    /// [��������: ��Һ������ƿ����ƿǩ��ӡ�ӿ�]<br></br>
    /// [�� �� ��: wolf]<br></br>
    /// [����ʱ��: 2004-10-12]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��=''
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public interface IPrintTransFusion
    {
        /// <summary>
        /// ��ӡ
        /// </summary>
        void Print();

        /// <summary>
        /// ��ѯ��������ָ��ʱ���ģ�ָ���÷�����Һ��
        /// </summary>
        /// <param name="patients"></param>
        /// <param name="usagecode"></param>
        /// <param name="dtBegin"></param>
        /// <param name="dtEnd"></param>
        /// <param name="isPrinted">�Ƿ񲹴�</param>
        /// <param name="orderType">ҽ�����:����LONG������SHORT��ȫ��ALL</param>
        /// <param name="isFirst">�Ƿ�������</param>
        void Query(List<FS.HISFC.Models.RADT.PatientInfo> patients, string usagecode, DateTime dtBegin, DateTime dtEnd, bool isRePrint, string orderType, bool isFirst);

        /// <summary>
        /// ��ӡ����
        /// </summary>
        void PrintSet();

        /// <summary>
        /// ����ҽ������
        /// </summary>
        void SetSpeOrderType(string speStr);

        /// <summary>
        /// ֹͣ���Ƿ��ӡ
        /// </summary>
        bool DCIsPrint
        {
            get;
            set;
        }

        /// <summary>
        /// δ�շ�֪���ӡ
        /// </summary>
        bool NoFeeIsPrint
        {
            get;
            set;
        }


        /// <summary>
        /// �˷��Ƿ��ӡ
        /// </summary>
        bool QuitFeeIsPrint
        {
            get;
            set;
        }

    }
}
