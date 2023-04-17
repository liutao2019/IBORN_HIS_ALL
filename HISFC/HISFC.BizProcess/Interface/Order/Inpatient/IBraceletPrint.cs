using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.FrameWork.Models;
using FS.HISFC.Models.Registration;

namespace FS.HISFC.BizProcess.Interface.Order.Inpatient
{


    #region 接口定义

    /// <summary>
    /// 腕带打印接口
    /// </summary>
    public interface IBraceletPrint
    {


        /// <summary>
        /// 患者信息
        /// </summary>
        FS.HISFC.Models.RADT.PatientInfo myPatientInfo
        {
            get;
            set;
        }
        /// <summary>
        /// 打印接口
        /// </summary>
        /// <returns></returns>
        void Print();

    }

    #endregion
}
