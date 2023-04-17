using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.Assign.BizProcess.InterfaceImplement
{
    /// <summary>
    /// [功能描述: 保存挂号信息后接口实现]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2011-12]<br></br>
    /// </summary>
    internal class SaveRegister : FS.SOC.HISFC.BizProcess.CommonInterface.Common.ISave<FS.HISFC.Models.Registration.Register>
    {
        #region ISave<Register> 成员

        public int Saved(FS.SOC.HISFC.BizProcess.CommonInterface.Common.EnumSaveType saveType, FS.HISFC.Models.Registration.Register t)
        {
            return 1;
        }

        public int SaveCommitting(FS.SOC.HISFC.BizProcess.CommonInterface.Common.EnumSaveType saveType, FS.HISFC.Models.Registration.Register t)
        {
            if (saveType == FS.SOC.HISFC.BizProcess.CommonInterface.Common.EnumSaveType.Update)
            {
                if (InterfaceManager.GetIADTImplement() != null)
                {
                    if (InterfaceManager.GetIADTImplement().PatientInfo(t, t) < 0)
                    {
                        err = "保存患者信息失败，请向系统管理员报告错误信息：" + InterfaceManager.GetIADTImplement().Err;
                        return -1;
                    }
                }
            }
            return 1;
        }

        public int Saving(FS.SOC.HISFC.BizProcess.CommonInterface.Common.EnumSaveType saveType, FS.HISFC.Models.Registration.Register t)
        {
            return 1;
        }

        #endregion

        #region IErr 成员

        private string err = "";
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
