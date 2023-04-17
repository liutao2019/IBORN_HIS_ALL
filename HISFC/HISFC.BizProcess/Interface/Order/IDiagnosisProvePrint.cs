﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.HISFC.BizProcess.Interface.Order
{
    /// <summary>
    /// 诊断证明书打印接口
    /// </summary>
    public interface IDiagnosisProvePrint
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
}
