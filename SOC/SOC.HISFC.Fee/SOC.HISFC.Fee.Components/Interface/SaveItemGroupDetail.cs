using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.SOC.HISFC.Fee.Components.Interface
{
    public class SaveItemGroupDetail : FS.SOC.HISFC.BizProcess.CommonInterface.Common.ISave<ArrayList>
    {
        #region ISave<List<UndrugComb>> 成员

        public int SaveCommitting(FS.SOC.HISFC.BizProcess.CommonInterface.Common.EnumSaveType saveType,ArrayList t)
        {
            if (Function.SendBizMessage(t, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType.Add, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumInfoType.UndrugGroup, ref this.err) == -1)
            {
                return -1;
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
