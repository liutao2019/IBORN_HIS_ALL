using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.BizProcess.CommonInterface.Common;

namespace FS.SOC.HISFC.InpatientFee.BizProcess
{
    /// <summary>
    /// [功能描述: 外部药品逻辑业务类]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2012-3]<br></br>
    /// </summary>
    public class Pharmacy:AbstractBizProcess
    {
        FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();

        /// <summary>
        /// 更新摆药申请处方号
        /// </summary>
        /// <param name="oldRecipeNo"></param>
        /// <param name="oldSeqNo"></param>
        /// <param name="newRecipeNo"></param>
        /// <param name="newSeqNo"></param>
        /// <returns></returns>
        public int UpdateApplyOutRecipe(string oldRecipeNo, int oldSeqNo, string newRecipeNo, int newSeqNo)
        {
            this.SetDB(itemManager);
            return itemManager.UpdateApplyOutRecipe(oldRecipeNo, oldSeqNo, newRecipeNo, newSeqNo);
        }

        /// <summary>
        /// 获取未确认的退药申请
        /// </summary>
        /// <param name="inpatientNO"></param>
        /// <returns></returns>
        public int QueryNoConfirmQuitApply(string inpatientNO)
        {
            this.SetDB(itemManager);
            return this.itemManager.QueryNoConfirmQuitApply(inpatientNO);
        }

    }
}
