using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.SOC.Local.OutpatientFee.GuangZhou.Zdly.ILisCalculateTube
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

            //如果是诊金，则
            decimal sumQty = 0.0M;      
            //高级检查不在带出诊金
            bool isAddFeeItem = true;
            foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItemList in alFeeItemList)
            {
                if (feeItemList.Item.ID == "F00002237128")//门诊记账诊出费
                {
                    sumQty += feeItemList.FT.TotCost;
                }

                //高级检查不在带出诊金
                if (feeItemList.FT.FTRate.User03 == "2")
                {
                    isAddFeeItem = false;
                    break;
                }
            }

            //加入公费记账
            if (alFeeItemList.Count > 0)
            {
                if (isAddFeeItem)
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

                    qty = qty - sumQty;
                    if (qty > 0)
                    {
                        decimal sum = 0.00M;


                        FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItemList = diagPubFee.GetFeeItemList(r, qty, recipeSequence, ref errInfo);
                        if (feeItemList == null)
                        {
                            this.errInfo = errInfo;
                            return -1;
                        }

                        alFeeItemList.Add(feeItemList);
                    }
                    else if (qty == 0)
                    {
                        //如果是诊金，则
                        string errorInfo = string.Empty;
                        //省公医，
                        if (Function.IsShenPub(r, ref errorInfo))
                        {
                            foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItemList in alFeeItemList)
                            {
                                if (feeItemList.Item.ID == "F00002237128")//门诊记账诊出费
                                {
                                    feeItemList.NewItemRate = 0;//特殊比例
                                    feeItemList.ItemRateFlag = "3";
                                }
                            }
                        }
                    }

                    #endregion
                }

                #region 高收费项目

                YkHighChargeFee ykHighCharge = new YkHighChargeFee();
                ArrayList al = ykHighCharge.GetYkFeeItemList(alFeeItemList);
                alFeeItemList.Clear();
                alFeeItemList.AddRange(al);

                #endregion
            }

            return 1;
        }

        #endregion
    }
}
