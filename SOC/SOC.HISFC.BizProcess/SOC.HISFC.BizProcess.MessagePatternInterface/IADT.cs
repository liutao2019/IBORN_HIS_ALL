using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.SOC.HISFC.BizProcess.MessagePatternInterface
{
    /// <summary>
    /// [功能描述: 业务消息模式：病人入出转]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2011-12]<br></br>
    /// </summary>
    public interface IADT 
    {
        /// <summary>
        /// 错误信息
        /// </summary>
        string Err
        {
            get;
            set;
        }

        ///// <summary>
        ///// 住院登记接口
        ///// </summary>
        ///// <param name="patientInfo"></param>
        ///// <returns></returns>
        //int Register(FS.HISFC.Models.RADT.PatientInfo patientInfo, bool positive);

        ///// <summary>
        ///// 门诊登记
        ///// </summary>
        ///// <param name="register"></param>
        ///// <param name="positive"></param>
        ///// <returns></returns>
        //int Register(FS.HISFC.Models.Registration.Register register, bool positive);

        /// <summary>
        /// 登记信息
        /// </summary>
        /// <param name="register"></param>
        /// <param name="positive"></param>
        /// <returns></returns>
        int Register(object register, bool positive);

        /// <summary>
        /// 修改病人信息
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="patientInfo"></param>
        /// <returns></returns>
        int PatientInfo(FS.HISFC.Models.RADT.Patient patient, object patientInfo);


        /// <summary>
        /// 住院结算接口
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="positive"></param>
        /// <returns></returns>
        int Balance(FS.HISFC.Models.RADT.PatientInfo patientInfo, bool positive);

        ///// <summary>
        ///// 修改住院信息
        ///// </summary>
        ///// <param name="?"></param>
        ///// <returns></returns>
        //int PatientInfo(FS.HISFC.Models.RADT.Patient patient, FS.HISFC.Models.RADT.PatientInfo patientInfo);

        ///// <summary>
        ///// 门诊修改病人信息
        ///// </summary>
        ///// <param name="patient"></param>
        ///// <param name="register"></param>
        ///// <returns></returns>
        //int PatientInfo(FS.HISFC.Models.RADT.Patient patient, FS.HISFC.Models.Registration.Register register);


        /// <summary>
        /// 分诊、取消分诊 进诊 取消进诊
        /// </summary>
        /// <param name="assign"></param>
        /// <param name="positive">正操作 还是反操作</param>
        /// <param name="state">0 分诊 1 进诊</param>
        /// <returns></returns>
        int AssignInfo(FS.HISFC.Models.Nurse.Assign assign, bool positive, int state);

        /// <summary>
        /// 预交金收取,返还-操作标记0收取1返还
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="alprepay"></param>
        /// <returns></returns>
        int Prepay(FS.HISFC.Models.RADT.PatientInfo patient, ArrayList alprepay, string flag);

        /// <summary>
        /// 查询预约人数
        /// </summary>
        /// <param name="alSchema"></param>
        /// <returns></returns>
        int QueryBookingNumber(ArrayList alSchema);
    }
}
