﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.HISFC.BizProcess.Interface
{
    /// <summary>
    /// 出院登记接口
    /// </summary>
    public interface IucOutPatient
    {
        bool IsSelect
        {
            set;
        }
    }
    /// <summary>
    /// 护士站出院召回接口
    /// </summary>
    public interface ICallBackPatient
    {
        bool IsSelect
        {
            set;
        }
    }

    /// <summary>
    /// 出院、出院召回等地方的判断,是否可以执行下一步操作
    /// </summary>
    public interface IPatientShiftValid
    {
        /// <summary>
        /// 出院、出院召回等地方的判断,是否可以执行下一步操作
        /// </summary>
        /// <param name="p">患者信息</param>
        /// <param name="type">操作类型</param>
        /// <param name="err">错误</param>
        /// <returns>true判断成功 false错误返回错误err</returns>
        bool IsPatientShiftValid(FS.HISFC.Models.RADT.PatientInfo p, FS.HISFC.Models.Base.EnumPatientShiftValid type, ref string err);
    }

    /// <summary>
    /// 打印住院通知单 //{C3AA974A-D98C-455b-ABDC-68781DB0306F}
    /// </summary>
    public interface IPrintInHosNotice
    {
        /// <summary>
        /// 住院通知单赋值
        /// </summary>
        /// <param name="prePatientInfo"></param>
        /// <returns></returns>
        int SetValue(FS.HISFC.Models.RADT.PatientInfo prePatientInfo);

        /// <summary>
        /// 打印
        /// </summary>
        /// <returns></returns>
        int Print();

        /// <summary>
        /// 打印预览
        /// </summary>
        /// <returns></returns>
        int PrintView();

    }

    /// <summary>
    /// 住院登记查询公费人员信息
    /// </summary>
    public interface IQueryGFPatient
    {
        /// <summary>
        /// 住院登记查询公费人员信息
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="errText"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        int QueryGFPatient(FS.HISFC.Models.RADT.PatientInfo patient, ref string errText, params string[] args);

    }
    #region addby xuewj 2010-9-6 {7586C819-B388-4bb2-B5E2-5A82B840A159} dhlocal
    /// <summary>
    ///  出院登记时判断是否可以进行下一步（德惠本地化需求）
    /// </summary>
    public interface IPatientOutCheck
    {
        /// <summary>
        /// 出院登记时判断是否可以进行下一步（德惠本地化需求）
        /// </summary>
        /// <param name="p">患者住院信息</param>
        /// <param name="err">错误日志</param>
        /// <returns>成功：true 失败：false</returns>
        bool IPatientOutCheck(FS.HISFC.Models.RADT.PatientInfo p, ref string err);
    }
    #endregion
}
