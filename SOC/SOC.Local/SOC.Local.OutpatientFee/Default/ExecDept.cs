using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.SOC.Local.OutpatientFee.Default
{
    public class ExecDept:FS.HISFC.BizProcess.Interface.Fee.IExecDept
    {
        FS.SOC.HISFC.Fee.BizProcess.DefaultExecDept execDeptMgr = new FS.SOC.HISFC.Fee.BizProcess.DefaultExecDept();
        #region IExecDept 成员

        public System.Collections.ArrayList GetExecDept(FS.FrameWork.Models.NeuObject recipeDept, FS.HISFC.Models.Fee.Item.Undrug item, ref string errorInfo)
        {
            List<FS.FrameWork.Models.NeuObject> dept = execDeptMgr.GetExecDept(recipeDept, item);
            errorInfo = execDeptMgr.Err;
            return new ArrayList(dept);
        }

        #endregion
    }
}
