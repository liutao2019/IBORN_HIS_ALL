using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore
{
    public interface ICompoundFee
    {
        /// <summary>
        /// 根据传入的药品申请信息，返回收费信息
        /// 按照批次收取配置费
        /// </summary>
        /// <param name="applyList">药品申请信息</param>
        /// <param name="feeList">费用信息</param>
        /// <returns></returns>
        int GetCompoundFeeList(ArrayList applyList, ref ArrayList feeList);

        /// <summary>
        /// 住院收费
        /// </summary>
        /// <param name="feeItemList">费用列表</param>
        /// <returns></returns>
        int SaveFee(ArrayList alApplyOut,FS.FrameWork.Models.NeuObject neuDept, System.Data.IDbTransaction trans);

        /// <summary>
        /// 根据核对项目确定是否进行收费
        /// </summary>
        /// <param name="alApply">核对项目列表</param>
        /// <returns></returns>
        List<string> SetFeeState(ArrayList alApply);

        /// <summary>
        /// 错误返回信息
        /// </summary>
        string Err
        {
            get;
            set;
        }
    }
}

