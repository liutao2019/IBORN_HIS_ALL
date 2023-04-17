using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.BizProcess.CommonInterface.Base;

namespace FS.SOC.HISFC.BizProcess.CommonInterface.Common
{
    /// <summary>
    /// [功能描述: 保存信息后调用接口]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2011-10]<br></br>
    /// </summary>
    public interface ISave<T>:IErr
    {
        /// <summary>
        /// 保存后调用接口
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        int Saved(EnumSaveType saveType,T t);

        /// <summary>
        /// 提交事务前调用接口
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <returns></returns>
        int SaveCommitting(EnumSaveType saveType, T t);

        /// <summary>
        /// 保存前调用接口
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <returns></returns>
        int Saving(EnumSaveType saveType, T t);
    }

    /// <summary>
    /// 保存操作类型
    /// </summary>
    public enum EnumSaveType
    {
        Insert,
        Update,
        Delete,
        InValid,
        Valid
    }
}
