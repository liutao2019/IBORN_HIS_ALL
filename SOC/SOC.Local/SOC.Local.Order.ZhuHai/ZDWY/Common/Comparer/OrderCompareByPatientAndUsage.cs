using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.SOC.Local.Order.ZhuHai.ZDWY.Common.Comparer
{
    public class OrderCompareByPatientAndUsage : IComparer
    {
        //床号、组号
        private FS.HISFC.BizLogic.RADT.InPatient inPatientManager = new FS.HISFC.BizLogic.RADT.InPatient();

        public int Compare(object x, object y)
        {
            FS.HISFC.Models.Order.ExecOrder lExecOrder = x as FS.HISFC.Models.Order.ExecOrder;
            FS.HISFC.Models.Order.ExecOrder rExecOrder = y as FS.HISFC.Models.Order.ExecOrder;
            if (lExecOrder != null && rExecOrder != null)
            {
                string lBedNo = lExecOrder.Order.Patient.PVisit.PatientLocation.Bed.ID.Substring(4);
                string rBedNo = rExecOrder.Order.Patient.PVisit.PatientLocation.Bed.ID.Substring(4);
                if (lBedNo == rBedNo)
                {
                    //string lExecOrderCombID = lExecOrder.Order.Usage.ID + lExecOrder.Order.Combo.ID;
                    //string rExecOrderCombID = rExecOrder.Order.Usage.ID + rExecOrder.Order.Combo.ID;

                    string lExecOrderCombID = lExecOrder.Order.SubCombNO.ToString();
                    string rExecOrderCombID = rExecOrder.Order.SubCombNO.ToString();
                    if (lExecOrderCombID == rExecOrderCombID)
                    {
                        int xSortid = FS.FrameWork.Function.NConvert.ToInt32(lExecOrder.Order.SortID);
                        int ySortid = FS.FrameWork.Function.NConvert.ToInt32(rExecOrder.Order.SortID);
                        if (xSortid == ySortid)
                        {
                            int xOrderid = FS.FrameWork.Function.NConvert.ToInt32(lExecOrder.Order.ID);
                            int yOrderid = FS.FrameWork.Function.NConvert.ToInt32(rExecOrder.Order.ID);
                            if (xOrderid == yOrderid)
                            {
                                string xUseTime = lExecOrder.DateUse.ToString();
                                string yUseTime = rExecOrder.DateUse.ToString();
                                return xUseTime.CompareTo(yUseTime);
                            }
                            else
                            {
                                return xOrderid.CompareTo(yOrderid);
                            }
                        }
                        else
                        {
                            return xSortid.CompareTo(ySortid);
                        }
                    }
                    else
                    {
                        return lExecOrderCombID.CompareTo(rExecOrderCombID);
                    }
                }
                else
                {
                    return lBedNo.CompareTo(rBedNo);
                }
            }
            return 0;
        }
    }
}
