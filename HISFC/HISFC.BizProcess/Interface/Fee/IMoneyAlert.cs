using System.Collections;

namespace FS.HISFC.BizProcess.Interface.Fee
{
    /// <summary>
    /// 患者欠费单据打印接口
    /// </summary>
    public interface IMoneyAlert
    {
        int Print(System.Collections.ArrayList alPatientInfo, decimal payMoney);
    }
}
