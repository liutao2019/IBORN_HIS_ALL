using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Neusoft.SOC.HISFC.RADT.Interface.Register
{
    /// <summary>
    /// [功能描述: 住院登记]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2011-10]<br></br>
    /// </summary>
   public  interface IInpatient
    {
        /// <summary>
        /// 初始化
        /// </summary>
        int Init();

        /// <summary>
        /// 清空
        /// </summary>
        void Clear();

        /// <summary>
        /// 修改患者信息
        /// </summary>
        /// <param name="patientInfo"></param>
        void ModifyPatientInfo(Neusoft.HISFC.Models.RADT.PatientInfo patientInfo);

        /// <summary>
        /// 获取患者信息
        /// </summary>
        Neusoft.HISFC.Models.RADT.PatientInfo GetPatientInfo(Neusoft.HISFC.Models.RADT.PatientInfo patientInfo);

        /// <summary>
        /// 获取患者预交金信息
        /// </summary>
        /// <returns></returns>
        Neusoft.HISFC.Models.Fee.Inpatient.Prepay GetPrepay();

        /// <summary>
        /// 设置患者信息
        /// </summary>
        /// <param name="patientInfo"></param>
        void SetPatientInfo(Neusoft.HISFC.Models.RADT.PatientInfo patientInfo, bool isAll);

        /// <summary>
        /// 保存事件
        /// </summary>
        event EventHandler OnSavePatientInfo;

        /// <summary>
        /// 控件大小
        /// </summary>
        Size ControlSize
        {
            get;
            set;
        }

        /// <summary>
        /// 查询患者信息
        /// </summary>
        event SelectPatientInfo OnQueryPatientInfo;

        /// <summary>
        /// 是否启用接诊流程
        /// </summary>
        bool IsArriveProcess
        {
            get;
            set;
        }

        /// <summary>
        /// 可否可以修改住院日期
        /// </summary>
        bool IsCanModifyInTime
        {
            get;
            set;
        }

        /// <summary>
        /// 判断是否输入完整
        /// </summary>
        /// <returns></returns>
        bool IsInputValid();

        /// <summary>
        /// 是否自动生成住院号
        /// </summary>
        bool IsAutoPatientNO
        {
            get;
            set;
        }

       /// <summary>
       /// 设置配置文件名称
       /// </summary>
        string FileName
        {
            set;
        }
    }
}
