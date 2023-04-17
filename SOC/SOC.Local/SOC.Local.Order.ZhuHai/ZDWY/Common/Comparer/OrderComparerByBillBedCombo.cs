using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.Local.Order.ZhuHai.ZDWY.Common.Comparer
{
    /// <summary>
    /// 执行档排序类,按执行单、床号、组号
    /// </summary>
    public class OrderComparer : System.Collections.IComparer
    {
        //执行单、床号、组号
        private FS.HISFC.BizLogic.RADT.InPatient inPatientManager = new FS.HISFC.BizLogic.RADT.InPatient();

        public int Compare(object x, object y)
        {
            FS.HISFC.Models.Order.ExecOrder lExecOrder = x as FS.HISFC.Models.Order.ExecOrder;
            FS.HISFC.Models.Order.ExecOrder rExecOrder = y as FS.HISFC.Models.Order.ExecOrder;
            if (lExecOrder != null && rExecOrder != null)
            {
                int lExecOrderID = FS.FrameWork.Function.NConvert.ToInt32(lExecOrder.Memo);
                int rExecOrderID = FS.FrameWork.Function.NConvert.ToInt32(rExecOrder.Memo);
                if (lExecOrderID == rExecOrderID)
                {
                    int lBedID = FS.FrameWork.Function.NConvert.ToInt32(inPatientManager.GetPatientInfoByPatientNO(lExecOrder.Order.Patient.ID).PVisit.PatientLocation.Bed.ID.Substring(4));
                    int rBedID = FS.FrameWork.Function.NConvert.ToInt32(inPatientManager.GetPatientInfoByPatientNO(rExecOrder.Order.Patient.ID).PVisit.PatientLocation.Bed.ID.Substring(4));
                    if (lBedID == rBedID)
                    {
                        int lExecOrderCombID = FS.FrameWork.Function.NConvert.ToInt32(lExecOrder.Order.Combo.ID);
                        int rExecOrderCombID = FS.FrameWork.Function.NConvert.ToInt32(rExecOrder.Order.Combo.ID);
                        return lExecOrderCombID.CompareTo(rExecOrderCombID);
                    }
                    else
                    {
                        return lBedID.CompareTo(rBedID);
                    }
                }
                else
                {
                    return lExecOrderID.CompareTo(rExecOrderID);
                }
            }
            return 0;
        }
    }
}
