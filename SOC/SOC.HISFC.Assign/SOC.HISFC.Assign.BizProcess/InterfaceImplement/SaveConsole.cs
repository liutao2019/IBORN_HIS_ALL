using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.SOC.HISFC.Assign.BizProcess.InterfaceImplement
{
    internal class SaveConsole : FS.SOC.HISFC.BizProcess.CommonInterface.Common.ISave<FS.HISFC.Models.Nurse.Seat>
    {
        #region ISave<Seat> 成员

        public int Saved(FS.SOC.HISFC.BizProcess.CommonInterface.Common.EnumSaveType saveType, FS.HISFC.Models.Nurse.Seat t)
        {
            return 1;
        }

        public int SaveCommitting(FS.SOC.HISFC.BizProcess.CommonInterface.Common.EnumSaveType saveType, FS.HISFC.Models.Nurse.Seat t)
        {
            if (InterfaceManager.GetISenderImplement() != null)
            {
                ArrayList al = new ArrayList();
                al.Add(t);
                if (InterfaceManager.GetISenderImplement().Send(al,
                    saveType == FS.SOC.HISFC.BizProcess.CommonInterface.Common.EnumSaveType.Insert ?
                    FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType.Add :
                    saveType == FS.SOC.HISFC.BizProcess.CommonInterface.Common.EnumSaveType.Update ?
                    FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType.Modify :
                    FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType.Delete,
                    FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumInfoType.NuoConsole,
                    ref err) < 0)
                {
                    err = "保存诊室信息失败，请向系统管理员报告错误信息：" + err;
                    return -1;
                }
            }
            return 1;
        }

        public int Saving(FS.SOC.HISFC.BizProcess.CommonInterface.Common.EnumSaveType saveType, FS.HISFC.Models.Nurse.Seat t)
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
