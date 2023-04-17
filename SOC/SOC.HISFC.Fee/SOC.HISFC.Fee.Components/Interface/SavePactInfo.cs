using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.SOC.HISFC.Fee.Components.Interface
{
    /// <summary>
    /// 保存合同单位信息
    /// </summary>
    public class SavePactInfo : FS.SOC.HISFC.BizProcess.CommonInterface.Common.ISave<ArrayList>
    {
        #region ISave<ArrayList> 成员

        public int SaveCommitting(FS.SOC.HISFC.BizProcess.CommonInterface.Common.EnumSaveType saveType, ArrayList t)
        {
            if (saveType == FS.SOC.HISFC.BizProcess.CommonInterface.Common.EnumSaveType.Update)
            {
                if (Function.SendBizMessage(t, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType.Modify, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumInfoType.Pact, ref this.err) == -1)
                {
                    return -1;
                }
            }
            else if (saveType == FS.SOC.HISFC.BizProcess.CommonInterface.Common.EnumSaveType.Insert)
            {
                if (Function.SendBizMessage(t, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType.Add, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumInfoType.Pact, ref this.err) == -1)
                {
                    return -1;
                }
            }
            return 1;
        }

        public int Saved(FS.SOC.HISFC.BizProcess.CommonInterface.Common.EnumSaveType saveType, ArrayList t)
        {
            return 1;
        }

        public int Saving(FS.SOC.HISFC.BizProcess.CommonInterface.Common.EnumSaveType saveType, ArrayList t)
        {
            return 1;
        }

        #endregion

        #region IErr 成员

        private string err = string.Empty;
        public string Err
        {
            get
            {
                return err;
            }
            set
            {
                err = value;
            }
        }

        #endregion
    }
}
