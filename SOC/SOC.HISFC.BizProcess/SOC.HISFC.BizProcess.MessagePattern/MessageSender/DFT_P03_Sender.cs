using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.HL7Message;
using System.Collections;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.MessageSender
{
    public class DFT_P03_Sender : AbstractSender<FS.HISFC.Models.RADT.PatientInfo, NHapi.Model.V24.Message.DFT_P03[], NHapi.Model.V24.Message.ACK, FS.FrameWork.Models.NeuObject>
    {
        /// <summary>
        /// 住院费用业务层
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.InPatient feeInpatient = new FS.HISFC.BizLogic.Fee.InPatient();

        protected override int ConvertObjectToSendMessage(FS.HISFC.Models.RADT.PatientInfo t, ref NHapi.Model.V24.Message.DFT_P03[] e, params object[] appendParams)
        {


            ArrayList alprepay = new ArrayList();  
            alprepay = appendParams[0] as ArrayList;
            if (alprepay == null)
            {
                return 1;
            }

            string flag = appendParams[1] as string;
            string happenNO = feeInpatient.QueryPrepayhappenNO(t.ID);
            List<NHapi.Model.V24.Message.DFT_P03> alDFT_P03 = new List<NHapi.Model.V24.Message.DFT_P03>();
            foreach (FS.HISFC.Models.Fee.Inpatient.Prepay prepay in alprepay)
            {

                if (flag == "0")// 收取
                {
                    NHapi.Model.V24.Message.DFT_P03 DFT_P03 = null;
                    if (new FS.SOC.HISFC.BizProcess.MessagePattern.HL7.FinancialManagement.DFT_P03.InpatientPrepay().ProcessMessage(t, prepay, ref DFT_P03, ref this.errInfo) == -1)
                    {
                        return -1;
                    }
                    alDFT_P03.Add(DFT_P03);
                }
                else if (flag == "1")  //返回
                {
                    //更新预交押金状态

                    prepay.FT.PrepayCost = -prepay.FT.PrepayCost;
                    prepay.PrepayState = "1";

                    NHapi.Model.V24.Message.DFT_P03 DFT_P03 = null;
                    if (new FS.SOC.HISFC.BizProcess.MessagePattern.HL7.FinancialManagement.DFT_P03.InpatientPrepay().ProcessMessage(t, prepay, ref DFT_P03, ref this.errInfo) == -1)
                    {
                        return -1;
                    }
                    alDFT_P03.Add(DFT_P03);

                    // 插入新预交金
                    prepay.FT.PrepayCost = -prepay.FT.PrepayCost;
                    prepay.ID = happenNO;

                    NHapi.Model.V24.Message.DFT_P03 dftp03 = null;
                    if (new FS.SOC.HISFC.BizProcess.MessagePattern.HL7.FinancialManagement.DFT_P03.InpatientPrepay().ProcessMessage(t, prepay, ref dftp03, ref this.errInfo) == -1)
                    {
                        return -1;
                    }
                    alDFT_P03.Add(dftp03);
                }
                else if (flag == "2")//重打
                {
                    if (prepay.PrepayState == "2")
                    {

                        //更新预交押金状态
                        prepay.FT.PrepayCost = -prepay.FT.PrepayCost;
                        NHapi.Model.V24.Message.DFT_P03 DFT_P03 = null;
                        if (new FS.SOC.HISFC.BizProcess.MessagePattern.HL7.FinancialManagement.DFT_P03.InpatientPrepay().ProcessMessage(t, prepay, ref DFT_P03, ref this.errInfo) == -1)
                        {
                            return -1;
                        }
                        alDFT_P03.Add(DFT_P03);
                        // 插入负记录
                        prepay.FT.PrepayCost = -prepay.FT.PrepayCost;
                        prepay.ID = (FS.FrameWork.Function.NConvert.ToInt32(happenNO)-1).ToString();


                        NHapi.Model.V24.Message.DFT_P03 dftp03 = null;
                        if (new FS.SOC.HISFC.BizProcess.MessagePattern.HL7.FinancialManagement.DFT_P03.InpatientPrepay().ProcessMessage(t, prepay, ref dftp03, ref this.errInfo) == -1)
                        {
                            return -1;
                        }
                        alDFT_P03.Add(dftp03);
                    }
                    if (prepay.PrepayState == "0")
                    {
                        prepay.ID = happenNO;
                        
                        //插入
                        NHapi.Model.V24.Message.DFT_P03 DFT_P03 = null;
                        if (new FS.SOC.HISFC.BizProcess.MessagePattern.HL7.FinancialManagement.DFT_P03.InpatientPrepay().ProcessMessage(t, prepay, ref DFT_P03, ref this.errInfo) == -1)
                        {
                            return -1;
                        }
                        alDFT_P03.Add(DFT_P03);
                    }
                }
                else
                {
                    errInfo = "无此预交类型，请系统管理员联系！";
                    return -1;
                }
            }
            e = alDFT_P03.ToArray<NHapi.Model.V24.Message.DFT_P03>();
            return 1;
        }
    }
}
