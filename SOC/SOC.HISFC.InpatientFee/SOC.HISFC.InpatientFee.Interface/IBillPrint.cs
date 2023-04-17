using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.InpatientFee.Interface
{
    /// <summary>
    /// [功能描述: 打印接口，用于各种单据的打印，重打，补打]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2012-2]<br></br>
    /// </summary>
    public interface IBillPrint
    {
        /// <summary>
        /// 设置需要打印的数据，默认为正常打印
        /// </summary>
        /// <param name="patientInfo">患者信息</param>
        /// <param name="t">需要打印的数据内容</param>
        /// <param name="errInfo">错误信息</param>
        /// <param name="appendParams">附加参数</param>
        /// <returns>1 成功 -1 失败</returns>
        int SetData(FS.HISFC.Models.RADT.PatientInfo patientInfo, Object t, ref string errInfo, params object[] appendParams);

        /// <summary>
        /// 设置需要打印的数据
        /// </summary>
        /// <param name="patientInfo">患者信息</param>
        /// <param name="printType">打印类型</param>
        /// <param name="t">需要打印的数据内容</param>
        /// <param name="errInfo">错误信息</param>
        /// <param name="appendParams">附加参数</param>
        /// <returns>1 成功 -1 失败</returns>
        int SetData(FS.HISFC.Models.RADT.PatientInfo patientInfo, EnumPrintType printType, Object t, ref string errInfo, params object[] appendParams);

        /// <summary>
        /// 打印
        /// </summary>
        /// <returns></returns>
        void Print();
    }
}
