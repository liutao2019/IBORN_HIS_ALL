using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.Local.OutpatientFee.ZhuHai.Zdwy.ILisCalculateTube
{
    class AddFeeItem : FS.HISFC.BizProcess.Interface.FeeInterface.ILisCalculateTube
    {
        #region ILisCalculateTube 成员

        private string errInfo = string.Empty;
        public string ErrInfo
        {
            get
            {
                return errInfo;
            }
            set
            {
                errInfo = value;
            }
        }

        public int LisCalculateTubeForInPatient(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            return 1;
        }

        public int LisCalculateTubeForOutPatient(FS.HISFC.Models.Registration.Register r, System.Collections.ArrayList alFeeItemList, string recipeSequence, ref decimal owncost, ref System.Collections.ArrayList alTubeList)
        {
            //LIS试管带出

            //加入公费记账
            if (alFeeItemList.Count > 0)
            {
                #region 公费记账
                DiagPubFee diagPubFee = new DiagPubFee();
                string errInfo = "";
                decimal qty = diagPubFee.GetNumber(r, ref errInfo);
                if (qty < 0)
                {
                    this.errInfo = errInfo;
                    return -1;
                }
                else if (qty > 0)
                {
                    FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItemList = diagPubFee.GetFeeItemList(r, qty, recipeSequence, ref errInfo);
                    if (feeItemList == null)
                    {
                        this.errInfo = errInfo;
                        return -1;
                    }

                    alFeeItemList.Add(feeItemList);
                }

                #endregion

            }

            return 1;
        }

        #endregion
    }
}
