using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FS.SOC.HISFC.BizProcess.DCPInterface
{
    /// <summary>
    /// UnionManager<br></br>
    /// [功能描述: 患者信息获取接口]<br></br>
    /// [创 建 者: ]<br></br>
    /// [创建时间: 2009]<br></br>
    /// <修改记录 
    ///		修改人='' 
    ///		修改时间='yyyy-mm-dd' 
    ///		修改目的=''
    ///		修改描述=''
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
        /// 按照住院状态 和科室查询患者列表
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="instate"></param>
        /// <returns></returns>
        System.Collections.ArrayList QueryPatientByDeptCode(string deptCode, FS.HISFC.Models.RADT.InStateEnumService instate);
    }
}
