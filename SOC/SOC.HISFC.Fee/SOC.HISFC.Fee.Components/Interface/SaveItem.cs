using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.BizProcess.CommonInterface.Common;
using System.Collections;

namespace FS.SOC.HISFC.Fee.Components.Interface
{
    /// <summary>
    /// [功能描述: 保存项目接口]<br></br>
    /// [创 建 者: zhaoj]<br></br>
    /// [创建时间: 2011-10]<br></br>
    /// </summary>
    public class SaveItem : FS.SOC.HISFC.BizProcess.CommonInterface.Common.ISave<FS.SOC.HISFC.Fee.Models.Undrug>
    {
        #region ISave<Undrug> 成员

        public int SaveCommitting(FS.SOC.HISFC.BizProcess.CommonInterface.Common.EnumSaveType saveType, FS.SOC.HISFC.Fee.Models.Undrug item)
        {

            ArrayList alItem = new ArrayList();
            alItem.Add(item);

            if (saveType == EnumSaveType.Insert)
            {
                if (Function.SendBizMessage(alItem, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType.Add, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumInfoType.Undrug, ref this.err) == -1)
                {
                    return -1;
                }
            }
            else if (saveType == EnumSaveType.Update)
            {
                if (Function.SendBizMessage(alItem, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType.Modify, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumInfoType.Undrug, ref this.err) == -1)
                {
                    return -1;
                }
            }
            else if (saveType == EnumSaveType.InValid)
            {
                if (Function.SendBizMessage(alItem, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType.InValid, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumInfoType.Undrug, ref this.err) == -1)
                {
                    return -1;
                }
            }
            else if (saveType == EnumSaveType.Valid)
            {
                if (Function.SendBizMessage(alItem, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType.Valid, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumInfoType.Undrug, ref this.err) == -1)
                {
                    return -1;
                }
            }

            return 1;
        }

        public int Saved(FS.SOC.HISFC.BizProcess.CommonInterface.Common.EnumSaveType saveType, FS.SOC.HISFC.Fee.Models.Undrug t)
        {
            return 1;
        }

        public int Saving(FS.SOC.HISFC.BizProcess.CommonInterface.Common.EnumSaveType saveType, FS.SOC.HISFC.Fee.Models.Undrug t)
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
