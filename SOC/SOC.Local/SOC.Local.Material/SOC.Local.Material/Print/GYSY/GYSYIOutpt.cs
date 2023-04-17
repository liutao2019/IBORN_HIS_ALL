using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace FS.SOC.Local.Material.Print.GYSY
{   
    /// <summary>
    /// 广医四院虚拟出库验证接口
    /// </summary>
    public class GYSYIOutpt:FS.HISFC.Interface.Material.MatProcess.IOutput
    {

        #region IOutput 成员

        public int CheckOutputList(List<FS.HISFC.BizLogic.Material.Object.Output> outputList, ref string msg)
        {
            foreach (FS.HISFC.BizLogic.Material.Object.Output output in outputList)
            {
                if (output.OutClass3 == "11") 
                {
                    if (string.IsNullOrEmpty(output.GetPersonID))
                    {
                        msg = "高值耗材虚拟出库请选择使用人！";
                        return -1;
                    }
                }
            }

            return 1;
        }

        #endregion
    }
}
