using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.FrameWork.Models;
using FS.HISFC.Models.Registration;

namespace FS.HISFC.BizProcess.Interface.Order
{


    #region 接口定义

    /// <summary>
    /// 门诊病历打印接口
    /// </summary>
    public interface IMedicalReportPrint
    {


        /// <summary>
        /// 患者挂号流水号
        /// </summary>
        string RegId
        {
            get;
            set;
        }


        /// <summary>
        /// 是否打印
        /// </summary>
        bool IsPrint
        {
            get;
            set;
        }
        /// <summary>
        /// 打印接口
        /// </summary>
        /// <returns></returns>
        void Print();


        /// <summary>
        /// 打印接口
        /// </summary>
        /// <returns></returns>
        void PrintView();


    }

    #endregion
}
