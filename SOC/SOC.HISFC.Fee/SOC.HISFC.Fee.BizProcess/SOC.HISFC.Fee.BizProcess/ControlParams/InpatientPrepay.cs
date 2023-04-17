using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neusoft.SOC.HISFC.Fee.BizProcess.ControlParams
{
    [FrameWork.Public.Description("")]
    public class InpatientPrepay
    {
        /// 冲负发票是否走新发票号
        /// </summary>
        public static readonly bool CancelPrepayIsNewInvoiceNO = ControlParam.GetBoolean("100016");

        /// 注销预交金是是否打印冲红发票
        /// </summary>
        public static readonly bool CancelPrepayIsPrintReturnInvoice = ControlParam.GetBoolean("100015");
    }
}
