using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore
{
    /// <summary>
    /// 住院工作量接口
    /// </summary>
    public interface IInpatientWorkLoad
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="dept">当前操作的库房</param>
        /// <param name="type">业务类型0门诊1住院</param>
        /// <param name="drugControl">当前登录的摆药台</param>
        /// <returns></returns>
        string Init(FS.FrameWork.Models.NeuObject dept, string type, FS.HISFC.Models.Pharmacy.DrugControl drugControl);

        /// <summary>
        /// 工作量设置
        /// </summary>
        /// <param name="dept">当前操作的库房</param>
        /// <param name="type">业务类型0门诊1住院</param>
        /// <param name="drugBillClass">当前操作的摆药单</param>
        /// <param name="alApplyOutData">摆药单对应的药品信息</param>
        /// <returns></returns>
        string Set(FS.FrameWork.Models.NeuObject dept, string type, FS.FrameWork.Models.NeuObject drugBillClass, ArrayList alApplyOutData); 
    }
}
