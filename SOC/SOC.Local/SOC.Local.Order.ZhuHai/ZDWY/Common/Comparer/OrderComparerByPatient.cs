using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.Local.Order.ZhuHai.ZDWY.Common.Comparer
{
    /// <summary>
    /// 执行档排序类,按患者、执行单、组号
    /// </summary>
    public class OrderComparerByPatient : System.Collections.IComparer
    {
        //患者、组号
        private FS.HISFC.BizLogic.RADT.InPatient inPatientManager = new FS.HISFC.BizLogic.RADT.InPatient();

        public int Compare(object x, object y)
        {
            FS.HISFC.Models.Order.ExecOrder lExecOrder = x as FS.HISFC.Models.Order.ExecOrder;
            FS.HISFC.Models.Order.ExecOrder rExecOrder = y as FS.HISFC.Models.Order.ExecOrder;
            if (lExecOrder != null && rExecOrder != null)
            {
                int lPatientID = FS.FrameWork.Function.NConvert.ToInt32(lExecOrder.Order.Patient.ID);
                int rPatientID = FS.FrameWork.Function.NConvert.ToInt32(rExecOrder.Order.Patient.ID);
                if (lPatientID == rPatientID)
                {
                    int lExecOrderID = FS.FrameWork.Function.NConvert.ToInt32(lExecOrder.Memo);
                    int rExecOrderID = FS.FrameWork.Function.NConvert.ToInt32(rExecOrder.Memo);
                    if (lExecOrderID == rExecOrderID)
                    {
                        int lExecOrderCombID = FS.FrameWork.Function.NConvert.ToInt32(lExecOrder.Order.Combo.ID);
                        int rExecOrderCombID = FS.FrameWork.Function.NConvert.ToInt32(rExecOrder.Order.Combo.ID);
                        return lExecOrderCombID.CompareTo(rExecOrderCombID);
                    }
                    else
                    {
                        return lExecOrderID.CompareTo(rExecOrderID);
                    }
                }
                else
                {
                    return lPatientID.CompareTo(rPatientID);
                }
            }
            return 0;
        }
    }
}
