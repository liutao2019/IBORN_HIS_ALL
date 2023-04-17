using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.SOC.Local.Order.ZhuHai.ZDWY.Common.Comparer
{
    public class ExecBedCompare : IComparer
    {
        private FS.HISFC.BizLogic.RADT.InPatient inPatientManager = new FS.HISFC.BizLogic.RADT.InPatient();
        public int Compare(object x, object y)
        {
            string lBedID = inPatientManager.GetPatientInfoByPatientNO(x.ToString()).PVisit.PatientLocation.Bed.ID.Substring(4);
            string rBedID = inPatientManager.GetPatientInfoByPatientNO(y.ToString()).PVisit.PatientLocation.Bed.ID.Substring(4);
            return lBedID.CompareTo(rBedID);
        }
    }
}
