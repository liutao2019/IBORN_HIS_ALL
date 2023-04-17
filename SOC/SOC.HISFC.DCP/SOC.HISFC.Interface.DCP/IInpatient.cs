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
    public interface IInpatient : IBase
    {
        FS.HISFC.Models.RADT.Patient GetBasePatientInfomation(string cardNO);

        ArrayList GetPatientInfoByPatientNOAll(string patientNO);

        ArrayList GetPatientInfoByPatientName(string patientName);

        ArrayList QueryPatientInfoBySqlWhere(string strWhere);

        FS.HISFC.Models.RADT.PatientInfo GetPatientInfomation(string inPatientNO);
        /// <summary>
        /// ����סԺ״̬ �Ϳ��Ҳ�ѯ�����б�
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="instate"></param>
        /// <returns></returns>
        System.Collections.ArrayList QueryPatientByDeptCode(string deptCode, FS.HISFC.Models.RADT.InStateEnumService instate);
    }
}
