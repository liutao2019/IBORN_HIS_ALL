using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.MessageSender
{
    public class RGV_O15_Sender : AbstractSender<object, NHapi.Model.V24.Message.RGV_O15[], NHapi.Model.V24.Message.ACK, FS.FrameWork.Models.NeuObject>
    {
        //protected override int ConvertObjectToSendMessage(FS.HISFC.Models.RADT.PatientInfo t, ref NHapi.Model.V24.Message.RGV_O15[] e, params object[] appendParams)
        //{
        //    ArrayList alObj = appendParams[0] as ArrayList;

        //    //发药信息
        //    ArrayList alApplyOut = new ArrayList();
        //    //收费信息
        //    ArrayList alFeeItemList = new ArrayList();

        //    foreach (Object o in alObj)
        //    {
        //        if (o is FS.HISFC.Models.Fee.Inpatient.FeeItemList)
        //        {
        //            FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList = o as FS.HISFC.Models.Fee.Inpatient.FeeItemList;
        //            //说明不是医嘱来的
        //            if (string.IsNullOrEmpty(feeItemList.Order.ID) || string.IsNullOrEmpty(feeItemList.ExecOrder.ID))
        //            {
        //                continue;
        //            }

        //            alFeeItemList.Add(o);
        //        }
        //        else if (o is FS.HISFC.Models.Pharmacy.ApplyOut)
        //        {
        //            FS.HISFC.Models.Pharmacy.ApplyOut applyOut = o as FS.HISFC.Models.Pharmacy.ApplyOut;
        //            //说明不是医嘱来的
        //            if (string.IsNullOrEmpty(applyOut.OrderNO) || string.IsNullOrEmpty(applyOut.ExecNO))
        //            {
        //                continue;
        //            }
        //            alApplyOut.Add(applyOut);
        //        }
        //    }

        //    List<NHapi.Model.V24.Message.RGV_O15> alRGV_O15 =new List<NHapi.Model.V24.Message.RGV_O15>();
        //    if (alFeeItemList.Count > 0)
        //    {
        //        NHapi.Model.V24.Message.RGV_O15 RGV_O15 = null;
        //        if (new FS.SOC.HISFC.BizProcess.MessagePattern.HL7.OrderEntry.RGV_O15.InpatientTerminalConfirm().ProcessMessage(t, alFeeItemList, (Boolean)appendParams[1], ref RGV_O15, ref this.errInfo) == -1)
        //        {
        //            return -1;
        //        }
        //        alRGV_O15.Add(RGV_O15);
        //    }

        //    if (alApplyOut.Count > 0)
        //    {
        //        NHapi.Model.V24.Message.RGV_O15 RGV_O15 = null;
        //        if (new FS.SOC.HISFC.BizProcess.MessagePattern.HL7.OrderEntry.RGV_O15.InpatientDrugStoreConfirm().ProcessMessage(t, alApplyOut, (Boolean)appendParams[1], ref RGV_O15, ref this.errInfo) == -1)
        //        {
        //            return -1;
        //        }
        //        alRGV_O15.Add(RGV_O15);
        //    }

        //    e = alRGV_O15.ToArray<NHapi.Model.V24.Message.RGV_O15>();

        //    return 1;
        //}

        protected override int ConvertObjectToSendMessage(object t, ref NHapi.Model.V24.Message.RGV_O15[] e, params object[] appendParams)
        {
            #region 住院
            if (t is FS.HISFC.Models.RADT.PatientInfo) //住院病人
            {
                ArrayList alObj = appendParams[0] as ArrayList;

                //发药信息
                ArrayList alApplyOut = new ArrayList();
                //收费信息
                ArrayList alFeeItemList = new ArrayList();

                foreach (Object o in alObj)
                {
                    if (o is FS.HISFC.Models.Fee.Inpatient.FeeItemList)
                    {
                        FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList = o as FS.HISFC.Models.Fee.Inpatient.FeeItemList;
                        //说明不是医嘱来的
                        if (string.IsNullOrEmpty(feeItemList.Order.ID) || string.IsNullOrEmpty(feeItemList.ExecOrder.ID))
                        {
                            continue;
                        }

                        alFeeItemList.Add(o);
                    }
                    else if (o is FS.HISFC.Models.Pharmacy.ApplyOut)
                    {
                        FS.HISFC.Models.Pharmacy.ApplyOut applyOut = o as FS.HISFC.Models.Pharmacy.ApplyOut;
                        //说明不是医嘱来的
                        if (string.IsNullOrEmpty(applyOut.OrderNO) || string.IsNullOrEmpty(applyOut.ExecNO))
                        {
                            continue;
                        }
                        alApplyOut.Add(applyOut);
                    }
                }

                List<NHapi.Model.V24.Message.RGV_O15> alRGV_O15 = new List<NHapi.Model.V24.Message.RGV_O15>();
                if (alFeeItemList.Count > 0)
                {
                    NHapi.Model.V24.Message.RGV_O15 RGV_O15 = null;
                    if (new FS.SOC.HISFC.BizProcess.MessagePattern.HL7.OrderEntry.RGV_O15.InpatientTerminalConfirm().ProcessMessage(t as FS.HISFC.Models.RADT.PatientInfo, alFeeItemList, (Boolean)appendParams[1], ref RGV_O15, ref this.errInfo) == -1)
                    {
                        return -1;
                    }
                    alRGV_O15.Add(RGV_O15);
                }

                if (alApplyOut.Count > 0)
                {
                    NHapi.Model.V24.Message.RGV_O15 RGV_O15 = null;
                    if (new FS.SOC.HISFC.BizProcess.MessagePattern.HL7.OrderEntry.RGV_O15.InpatientDrugStoreConfirm().ProcessMessage(t as FS.HISFC.Models.RADT.PatientInfo, alApplyOut, (Boolean)appendParams[1], ref RGV_O15, ref this.errInfo) == -1)
                    {
                        return -1;
                    }
                    alRGV_O15.Add(RGV_O15);
                }

                e = alRGV_O15.ToArray<NHapi.Model.V24.Message.RGV_O15>();

            }
            #endregion

            #region 门诊
            else if (t is FS.HISFC.Models.Registration.Register)
            {
                ArrayList alObj = appendParams[0] as ArrayList;

                //发药信息
                ArrayList alApplyOut = new ArrayList();
                foreach (Object o in alObj)
                {
                  
                    if (o is FS.HISFC.Models.Pharmacy.ApplyOut)
                    {
                        FS.HISFC.Models.Pharmacy.ApplyOut applyOut = o as FS.HISFC.Models.Pharmacy.ApplyOut;
                        //说明不是医嘱来的
                        if (string.IsNullOrEmpty(applyOut.OrderNO) || string.IsNullOrEmpty(applyOut.ExecNO))
                        {
                            continue;
                        }
                        alApplyOut.Add(applyOut);
                    }

                }

                List<NHapi.Model.V24.Message.RGV_O15> alRGV_O15 = new List<NHapi.Model.V24.Message.RGV_O15>();
                if (alApplyOut.Count > 0)
                {
                    NHapi.Model.V24.Message.RGV_O15 RGV_O15 = null;
                    if (new FS.SOC.HISFC.BizProcess.MessagePattern.HL7.OrderEntry.RGV_O15.OutPatientDrugStoreConfirm().ProcessMessage(t as FS.HISFC.Models.Registration.Register, alApplyOut, (Boolean)appendParams[1], ref RGV_O15, ref this.errInfo) == -1)
                    {
                        return -1;
                    }
                    alRGV_O15.Add(RGV_O15);
                }

                e = alRGV_O15.ToArray<NHapi.Model.V24.Message.RGV_O15>();


            }

            #endregion

            return 1;
        }
         
    }
}
