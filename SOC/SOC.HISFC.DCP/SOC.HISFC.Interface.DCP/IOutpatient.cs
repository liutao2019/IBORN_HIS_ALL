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
    public interface IOutpatient : IBase
    {
        FS.HISFC.Models.Registration.Register GetByClinic(string clinicNO);

        ArrayList QueryValidPatientsByName(string patientName);

        ArrayList Query(string patientNO, DateTime date);
        /// <summary>
        /// 查询门诊患者信息
        /// </summary>
        /// <param name="docID"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="isSee"></param>
        /// <returns></returns>
        ArrayList QueryBySeeDoc(string docID, DateTime beginDate, DateTime endDate, bool isSee);
        /// <summary>
        /// 挂号查询
        /// </summary>
        /// <param name="sql">条件</param>
        /// <returns></returns>
        ArrayList QueryRegister(string sql);
    }
}
