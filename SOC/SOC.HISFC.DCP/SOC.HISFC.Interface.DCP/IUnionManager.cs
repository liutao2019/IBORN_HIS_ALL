using System;
using System.Collections.Generic;
using System.Text;
using FS.SOC.HISFC.DCP.Enum;

namespace FS.SOC.HISFC.BizProcess.DCPInterface
{
    /// <summary>
    /// UnionManager<br></br>
    /// [功能描述: 疾病控制预防报卡接口]<br></br>
    /// [创 建 者: zengft]<br></br>
    /// [创建时间: 2008-8-20]<br></br>
    /// <修改记录 
    ///		修改人='' 
    ///		修改时间='yyyy-mm-dd' 
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    public interface IUnionManager
    {
        /// <summary>
        /// 疾病控制预防管理接口函数
        /// 新建报卡上报疾病到预防保健科
        /// </summary>
        /// <param name="patientType">患者类型</param>
        /// <returns></returns>
        ReportOperResult RegisterReport(System.Windows.Forms.IWin32Window owner, PatientType patientType);

        /// <summary>
        /// 疾病控制预防管理接口报卡函数
        /// 上报某患者疾病情况到预防保健科
        /// </summary>
        /// <param name="patient">患者实体</param>
        /// <param name="patientType">患者类型</param>
        /// <returns></returns>
        ReportOperResult RegisterReport(System.Windows.Forms.IWin32Window owner, FS.HISFC.Models.RADT.Patient patient, PatientType patientType);

        /// <summary>
        /// 疾病控制预防管理接口函数
        /// 根据诊断名称确定是否上报疾病到预防保健科
        /// </summary>
        /// <param name="owner">所有者</param>
        /// <param name="patient">患者实体</param>
        /// <param name="patientType">患者类型</param>
        /// <param name="diagName">诊断名称</param>
        /// <param name="msg">提示信息</param>
        /// <returns></returns>
        ReportOperResult CheckDisease(System.Windows.Forms.IWin32Window owner, FS.HISFC.Models.RADT.Patient patient, PatientType patientType, string diagName, out string msg);

        /// <summary>
        /// 获取疾病控制预防的反馈信息
        /// </summary>
        /// <returns></returns>
        int GetDCPNotice(System.Windows.Forms.IWin32Window owner, PatientType patientType);
    }
}
