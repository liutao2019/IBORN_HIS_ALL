using System;
using System.Collections.Generic;
using System.Text;

namespace Neusoft.HISFC.BizProcess.Integrate
{
    /// <summary>
    /// [��������: ��ӡ��Һ���ӿ�]<br></br>
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
        /// ��ѯ�������ߵ�ָ��ʱ��Σ�ָ���÷�����Һ��
        /// </summary>
        /// <param name="patients"></param>
        /// <param name="usagecode"></param>
        /// <param name="dtBegin"></param>
        /// <param name="dtEnd"></param>
        void Query(List<Neusoft.HISFC.Models.RADT.PatientInfo> patients, string usagecode, DateTime dtBegin, DateTime dtEnd,bool isPrinted);

        /// <summary>
        /// ��ѯ��������ָ��ʱ���ģ�ָ���÷�����Һ��
        /// </summary>
        /// <param name="patients"></param>
        /// <param name="usagecode"></param>
        /// <param name="dtTime"></param>
        void Query(List<Neusoft.HISFC.Models.RADT.PatientInfo> patients, string usagecode, DateTime dtTime,bool isPrinted);

        /// <summary>
        /// ��ӡ����
        /// </summary>
        void PrintSet();
        

    }
}
