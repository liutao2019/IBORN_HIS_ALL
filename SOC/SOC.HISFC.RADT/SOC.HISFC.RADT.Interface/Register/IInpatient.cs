using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace FS.SOC.HISFC.RADT.Interface.Register
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
        void ModifyPatientInfo(FS.HISFC.Models.RADT.PatientInfo patientInfo);

        /// <summary>
        /// 获取患者信息
        /// </summary>
        FS.HISFC.Models.RADT.PatientInfo GetPatientInfo(FS.HISFC.Models.RADT.PatientInfo patientInfo);

        /// <summary>
        /// 获取患者预交金信息
        /// </summary>
        /// <returns></returns>
        FS.HISFC.Models.Fee.Inpatient.Prepay GetPrepay();

        /// <summary>
        /// 设置患者信息
        /// </summary>
        /// <param name="patientInfo"></param>
        void SetPatientInfo(FS.HISFC.Models.RADT.PatientInfo patientInfo, bool isAll);

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
        /// 可否可以修改患者名字
        /// </summary>
        bool IsCanModifyIName
        {
            get;
            set;
        }

        /// <summary>
        /// 是否可以修改住院号
        /// </summary>
        bool IsCanModifyPatientNo
        {
            get;
            set;
        }


        /// <summary>
        /// 是否显示预交金充值信息// {5B3B503C-8CF5-415b-89EB-C11A4FEE8A19}
        /// </summary>
        bool IsShowPrePay
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
        /// 卡号
        /// </summary>
        string CardNo
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



        /// <summary>
        /// 门诊诊断是否可以为空// {D59C1D74-CE65-424a-9CB3-7F9174664504}
        /// </summary>
        bool IsInputDiagnose
        {
            get;
            set;
        }

        /// <summary>
        /// 联系人是否可以为空// {6BF1F99D-7307-4d05-B747-274D24174895}
        /// </summary>
        bool IsInputLinkMan
        {
            get;
            set;
        }
    }
}
