using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace Neusoft.SOC.HISFC.RADT.Interface.Register
{

    /// <summary>
    /// 选择患者委托
    /// </summary>
    /// <param name="patientInfo"></param>
    public delegate int SelectPatientInfo(Neusoft.HISFC.Models.RADT.PatientInfo patientInfo);

    /// <summary>
    /// [功能描述: 登记时显示患者的列表]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2011-10]<br></br>
    /// </summary>
    public interface IPatientList
    {
        /// <summary>
        /// 加载患者
        /// </summary>
        /// <param name="al"></param>
        /// <returns></returns>
        int LoadPatient(ArrayList al);

        /// <summary>
        /// 选择患者事件
        /// </summary>
        event SelectPatientInfo OnSelectPatientInfo;

        /// <summary>
        /// 列表显示类型
        /// </summary>
        View ViewState
        {
            get;
            set;
        }

        /// <summary>
        /// 刷新信息
        /// </summary>
        event EventHandler OnRefresh;
    }
}
