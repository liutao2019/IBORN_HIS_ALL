using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FS.SOC.HISFC.BizProcess.DCPInterface
{
    /// <summary>
    /// UnionManager<br></br>
    /// [��������: ������Ϣ��ȡ�ӿ�]<br></br>
    /// [�� �� ��: ]<br></br>
    /// [����ʱ��: 2009]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='' 
    ///		�޸�ʱ��='yyyy-mm-dd' 
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public interface IOutpatient : IBase
    {
        FS.HISFC.Models.Registration.Register GetByClinic(string clinicNO);

        ArrayList QueryValidPatientsByName(string patientName);

        ArrayList Query(string patientNO, DateTime date);
        /// <summary>
        /// ��ѯ���ﻼ����Ϣ
        /// </summary>
        /// <param name="docID"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="isSee"></param>
        /// <returns></returns>
        ArrayList QueryBySeeDoc(string docID, DateTime beginDate, DateTime endDate, bool isSee);
        /// <summary>
        /// �ҺŲ�ѯ
        /// </summary>
        /// <param name="sql">����</param>
        /// <returns></returns>
        ArrayList QueryRegister(string sql);
    }
}
