using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.Local.InpatientFee.ChaoYang
{
    public class InpatientPrepayPatientSignPrint:AbstractBillPrint<FS.HISFC.Models.Fee.Inpatient.Prepay>
    {
        ucCancelPrepayPatientSign ucCancelPrepayPatientSign = null;

        public override int SetData(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.SOC.HISFC.InpatientFee.Interface.EnumPrintType printType, FS.HISFC.Models.Fee.Inpatient.Prepay prepay, params object[] appendParams)
        {
            ucCancelPrepayPatientSign = null;
            if (prepay.FT.PrepayCost < 0)
            {
                ucCancelPrepayPatientSign = new ucCancelPrepayPatientSign();
                ucCancelPrepayPatientSign.SetValue(patientInfo, prepay);
            }

            return 1;
        }

        public override FS.HISFC.Models.Base.PageSize GetPageSize()
        {
            FS.HISFC.Models.Base.PageSize pSize = pageSizeManager.GetPageSize("ZYYJQM");
            if (pSize == null)
            {
                pSize = new FS.HISFC.Models.Base.PageSize("ZYYJQM", 600, 400);
            }

            return pSize;
        }

        public override System.Windows.Forms.Control[] GetPrintControls()
        {
            return new System.Windows.Forms.Control[] { ucCancelPrepayPatientSign };
        }
    }
}
