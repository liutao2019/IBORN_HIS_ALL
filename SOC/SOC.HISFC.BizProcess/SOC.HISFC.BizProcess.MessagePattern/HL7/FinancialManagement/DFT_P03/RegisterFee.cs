using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.BizProcess.MessagePatternInterface;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.HL7.FinancialManagement.DFT_P03
{
    public class RegisterFee
    {
        private int processMessaege(NHapi.Model.V24.Message.DFT_P03 o, ref NHapi.Model.V24.Message.ACK ackMessage, ref string errInfo)
        {
            FS.HISFC.BizLogic.Registration.Register regMgr = new FS.HISFC.BizLogic.Registration.Register();
            regMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            for (int i = 0; i < o.FINANCIALRepetitionsUsed; i++)
            {
                #region FT1-挂号信息

                //门诊流水号
                string clinicCode = o.GetFINANCIAL(i).FT1.AssignedPatientLocation.PointOfCare.Value;

                if (string.IsNullOrEmpty(clinicCode))
                {
                    errInfo = "门诊流水号为空，DFT_P03-FT1-16";
                    return -1;
                }

                FS.HISFC.Models.Registration.Register register = regMgr.GetByClinic(clinicCode);
                if (register == null)
                {
                    errInfo = "获取挂号信息失败，原因：" + regMgr.Err;
                    return -1;
                }

                if (string.IsNullOrEmpty(register.ID))
                {
                    errInfo = "传入的门诊流水号系统无法找到对应的挂号记录，DFT_P03-FT1-16=" + clinicCode;
                    return -1;
                }

                #endregion

                #region 补充-更新挂号记录的状态

                if (register.IsFee)
                {
                    errInfo = "挂号记录已收费，不允许继续进行收费！";
                    return -1;
                }

                //更新收费标记
                register.IsFee = true;
                register.InputOper.ID = "T00001";
                register.InputOper.Name = "自助终端机";

                #region 更新看诊序号

                int orderNo = 0;

                //1全院流水号	
                #region 全院看诊序号

                //全院是全天大排序，所以午别不生效，默认 1
                if (regMgr.UpdateSeeNo("4", register.DoctorInfo.SeeDate, "ALL", "1") == -1)
                {
                    errInfo = "更新序号失败，原因：" + regMgr.Err;
                    return -1;
                }

                //获取全院看诊序号
                if (regMgr.GetSeeNo("4", register.DoctorInfo.SeeDate, "ALL", "1", ref orderNo) == -1)
                {
                    errInfo = "获取序号失败，原因：" + regMgr.Err;
                    return -1;
                }

                register.OrderNO = orderNo;

                #endregion

                //2看诊序号		
                #region 科室或医生看诊序号
                string Type = "", Subject = "";

                if (string.IsNullOrEmpty(register.DoctorInfo.Templet.Dept.ID) == false)
                {
                    Type = "1";//医生
                    Subject = register.DoctorInfo.Templet.Dept.ID;
                }
                else
                {
                    Type = "2";//科室
                    Subject = register.DoctorInfo.Templet.Doct.ID;
                }

                //更新看诊序号
                if (regMgr.UpdateSeeNo(Type, register.DoctorInfo.SeeDate, Subject, register.DoctorInfo.Templet.Noon.ID) == -1)
                {
                    errInfo = "更新序号失败，原因：" + regMgr.Err;
                    return -1;
                }

                //获取看诊序号		
                if (regMgr.GetSeeNo(Type, register.DoctorInfo.SeeDate, Subject, register.DoctorInfo.Templet.Noon.ID, ref orderNo) == -1)
                {
                    errInfo = "获取序号失败，原因：" + regMgr.Err;
                    return -1;
                }

                register.DoctorInfo.SeeNO = orderNo;

                #endregion

                //3保存挂号信息
                #region 修改挂号记录

                //先删除
                if (regMgr.DeleteByClinic(register) <= 0)
                {
                    errInfo = "更新挂号信息失败，原因：" + regMgr.Err;
                    return -1;
                }

                //再插入
                if (regMgr.Insert(register) <= 0)
                {
                    errInfo = "更新挂号信息失败，原因：" + regMgr.Err;
                    return -1;
                }

                #endregion

                #endregion

                #endregion

                #region FT1-交易信息

                //金额
                decimal Tot_Cost = FS.FrameWork.Function.NConvert.ToDecimal(o.GetFINANCIAL(i).FT1.TransactionAmountExtended.Price.Quantity.Value);
                //社保缴费
                decimal Sb_Cost = FS.FrameWork.Function.NConvert.ToDecimal(o.GetFINANCIAL(i).FT1.InsuranceAmount.Price.Quantity.Value);

                #endregion

                #region ZPY-社保支付项目信息

                for (int num = 0; num < o.GetFINANCIAL(0).FINANCIAL_PAYMENTRepetitionsUsed; num++)
                {
                    //医疗费用支付项目
                    string Zfxm = o.GetFINANCIAL(0).GetFINANCIAL_PAYMENT(num).ZPY.Zfxm.Value;
                    //医疗费用支付项目金额
                    decimal Je = FS.FrameWork.Function.NConvert.ToDecimal(o.GetFINANCIAL(0).GetFINANCIAL_PAYMENT(num).ZPY.Je.Value);

                    //进行支付项目的存储
                }

                //更新挂号记录的OWN_COST,PUB_COST,PAY_COST

                #endregion

                #region ZCT-健康卡交易信息

                NHapi.Model.V24.Segment.ZCT ZCT = o.GetFINANCIAL(i).ZCT;

                #endregion

                #region ACK-返回信息

                ackMessage.MSA.TextMessage.Value = register.DoctorInfo.SeeNO.ToString();

                #endregion

                #region 发送挂号消息

                if (new ADT.PatientADT().Register(register, true) < 0)
                {
                    errInfo = "挂号发送消息失败，原因：" + errInfo;
                    return -1;
                }

                #endregion
            }

            return 1;
        }

        public  int ProcessMessage(NHapi.Model.V24.Message.DFT_P03 o, ref NHapi.Model.V24.Message.ACK ackMessage, ref string errInfo)
        {

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            if (this.processMessaege(o,ref ackMessage,ref errInfo) <= 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                return -1;
            }

            FS.FrameWork.Management.PublicTrans.Commit();
            return 1;
        }

    }
}
