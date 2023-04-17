using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder
{
    /// <summary>
    /// 门诊电子申请单接口
    /// </summary>
    public interface IOutPatientPacsApply
    {
        /// <summary>
        /// 错误信息
        /// </summary>
        string ErrInfo
        {
            get;
            set;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <returns></returns>
        int Init(FS.HISFC.Models.Registration.Register patientInfo);

        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <returns></returns>
        int Quit(FS.HISFC.Models.Registration.Register patientInfo);


        /// <summary>
        /// 批量保存
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="alInOrder"></param>
        /// <returns></returns>
        int Save(FS.HISFC.Models.Registration.Register patientInfo, ArrayList alInOrder);

        /// <summary>
        /// 编辑申请单
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="alInOrder"></param>
        /// <returns></returns>
        int Edit(FS.HISFC.Models.Registration.Register patientInfo, FS.HISFC.Models.Order.OutPatient.Order order);

        /// <summary>
        /// 删除申请单
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="inOrder"></param>
        /// <returns></returns>
        int Delete(FS.HISFC.Models.Registration.Register patientInfo, FS.HISFC.Models.Order.OutPatient.Order order);
    }
}
