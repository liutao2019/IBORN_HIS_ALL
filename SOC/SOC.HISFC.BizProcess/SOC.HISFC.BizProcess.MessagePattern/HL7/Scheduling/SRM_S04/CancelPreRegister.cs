using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.BizProcess.CommonInterface;
using FS.SOC.HISFC.BizProcess.MessagePatternInterface;
using FS.HISFC.Models.Account;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.HL7.Scheduling.SRM_S04
{
     class CancelPreRegister 
    {
        private int processMessage(NHapi.Model.V24.Message.SRM_S04 o, ref NHapi.Model.V24.Message.SRR_S04 ackMessage, ref string errInfo)
        {

            FS.HISFC.Models.Base.Employee e = new FS.HISFC.Models.Base.Employee();
            e.ID = "T00001";
            e.Name = "自助终端机";
            e.UserCode = "99";
            FS.FrameWork.Management.Connection.Operator = e;

            FS.HISFC.BizLogic.Registration.Register regMgr = new FS.HISFC.BizLogic.Registration.Register();
            FS.HISFC.BizLogic.Registration.Schema schemaMgr=new FS.HISFC.BizLogic.Registration.Schema();
            FS.HISFC.BizLogic.Fee.Account accountMgr = new FS.HISFC.BizLogic.Fee.Account();
            FS.HISFC.Models.Registration.Register register = new FS.HISFC.Models.Registration.Register();

            #region PV1

            if (!o.GetPATIENT(0).PV1.PatientClass.Value.Equals("O"))
            {
                errInfo = "患者类型为空或不正确，SRM_S04-PV1-2=" + o.GetPATIENT(0).PV1.PatientClass.Value;
                return -1;
            }

            string clinicCode = o.GetPATIENT(0).PV1.VisitNumber.ID.Value;
            register = regMgr.GetByClinic(clinicCode);
            if (register == null || string.IsNullOrEmpty(register.ID))
            {
                errInfo = "查找患者失败，原因：" + regMgr.Err + "，SRM_S04-PV1-19=" + clinicCode;
                return -1;
            }


            #endregion

            #region 补充

            if (register.IsFee)
            {
                errInfo = "患者的挂号记录已收费，不允许取消挂号，请到人工窗口退号，SRM_S04-PV1-19=" + clinicCode;
                return -1;
            }

            register.CancelOper.ID = "T00001";
            register.Status = FS.HISFC.Models.Base.EnumRegisterStatus.Cancel;

            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            regMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            schemaMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            accountMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            register.CancelOper.OperTime = CommonController.CreateInstance().GetSystemTime();
            if (regMgr.Update(FS.HISFC.BizLogic.Registration.EnumUpdateStatus.Cancel, register) <= 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                errInfo = "取消挂号失败，原因：" + regMgr.Err + "，SRM_S04-PV1-19=" + clinicCode;
                return -1;
            }

            //如果挂号级别是专家，则释放专家号
            if (string.IsNullOrEmpty(register.DoctorInfo.Templet.ID) == false)
            {
                if (schemaMgr.Reduce(register.DoctorInfo.Templet.ID, true, false, false, false) <= 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    errInfo = "更新排班限额失败，原因：" + schemaMgr.Err;
                    return -1;
                }
            }

            //作废费用信息
            if (accountMgr.CancelAccountCardFeeByInvoice(register.InvoiceNO) <= 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                errInfo = "作废挂号费用信息失败，原因：" + schemaMgr.Err;
                return -1;
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            #endregion

            #region SRR_S04

            if (ackMessage == null)
            {
                ackMessage = new NHapi.Model.V24.Message.SRR_S04();
            }

            FS.HL7Message.V24.Function.GenerateMSH(ackMessage.MSH, o.MSH);
            FS.HL7Message.V24.Function.GenerateSuccessMSA(o.MSH, ackMessage.MSA);
            ackMessage.MSA.ExpectedSequenceNumber.Value = "100";

            #endregion

            return 1;
        }

        public  int ProcessMessage(NHapi.Model.V24.Message.SRM_S04 o, ref NHapi.Model.V24.Message.SRR_S04 SRR_S04,ref string errInfo)
        {
            SRR_S04 = new NHapi.Model.V24.Message.SRR_S04();
            FS.HL7Message.V24.Function.GenerateMSH(SRR_S04.MSH, o.MSH);
            if (this.processMessage(o, ref SRR_S04,ref errInfo) <= 0)
            {
                FS.HL7Message.V24.Function.GenerateErrorMSA(o.MSH, SRR_S04.MSA, errInfo);
                SRR_S04.MSA.TextMessage.Value = errInfo;
                SRR_S04.MSA.ExpectedSequenceNumber.Value = "200";
                return -1;
            }
            else
            {
                FS.HL7Message.V24.Function.GenerateSuccessMSA(o.MSH, SRR_S04.MSA);
                SRR_S04.MSA.ExpectedSequenceNumber.Value = "100";
                return 1;
            }
        }

    }
}
