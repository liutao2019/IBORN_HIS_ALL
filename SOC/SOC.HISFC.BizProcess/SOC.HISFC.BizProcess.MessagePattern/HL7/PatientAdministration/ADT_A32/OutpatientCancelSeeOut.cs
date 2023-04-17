using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.BizProcess.CommonInterface;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.HL7.PatientAdministration.ADT_A32
{
    class OutpatientCancelSeeOut
    {
        /// <summary>
        /// 取消接诊信息处理
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <returns></returns>
        public int ProcessMessage(NHapi.Model.V24.Message.ADT_A32 adtA32, ref NHapi.Base.Model.IMessage ackMessage, ref string errInfo)
        {
            if (string.IsNullOrEmpty(adtA32.PV1.VisitNumber.ID.Value))
            {
                errInfo = "取消接诊失败，原因：患者流水号为空";
                return -1;
            }

            //取消分诊
            FS.SOC.HISFC.Assign.BizLogic.Assign assignMgr = new FS.SOC.HISFC.Assign.BizLogic.Assign();
            FS.HISFC.BizLogic.Registration.Register registerMgr = new FS.HISFC.BizLogic.Registration.Register();

            FS.FrameWork.Management.Connection.Operator.ID = adtA32.EVN.GetOperatorID(0).IDNumber.Value;
            if (string.IsNullOrEmpty(FS.FrameWork.Management.Connection.Operator.ID))
            {
                FS.FrameWork.Management.Connection.Operator.ID = "T00001";
                errInfo = "取消看诊失败，原因：操作员编码为空";
                //return -1;
            }
            if (CommonController.CreateInstance().GetEmployee(FS.FrameWork.Management.Connection.Operator.ID) != null)
            {
                FS.FrameWork.Management.Connection.Operator = CommonController.CreateInstance().GetEmployee(FS.FrameWork.Management.Connection.Operator.ID);

            }
            else
            {
                errInfo = "取消看诊失败，原因：传入的操作员编码，系统中找不到" + adtA32.EVN.GetOperatorID(0).IDNumber.Value;
                //return -1;
            }

            string clinicCode = adtA32.PV1.VisitNumber.ID.Value;
            FS.HISFC.Models.Registration.Register regInfo = registerMgr.GetByClinic(clinicCode);
            if (regInfo == null || string.IsNullOrEmpty(regInfo.ID))
            {
                errInfo = "获取患者信息" + clinicCode + "失败!" + registerMgr.Err;
                return -1;
            }
            if (regInfo.Status == FS.HISFC.Models.Base.EnumRegisterStatus.Back)
            {
                errInfo = "获取挂号信息失败，原因：挂号记录已退号" + regInfo.ID;
                return -1;
            }
            else if (regInfo.Status == FS.HISFC.Models.Base.EnumRegisterStatus.Cancel)
            {
                errInfo = "获取挂号信息失败，原因：挂号记录已作废" + regInfo.ID;
                return -1;
            }

            string sql = @"UPDATE fin_opr_register   
                                        SET see_date = null,
                                                ynsee = '0'
                                       WHERE clinic_code='{0}'";
            if (registerMgr.ExecNoQuery(string.Format(sql, regInfo.ID)) <= 0)
            {
                //更新看诊标记失败
                errInfo = "更新看诊标记失败，原因：" + registerMgr.Err;
                return -1;
            }

            FS.SOC.HISFC.Assign.Models.Assign assign = assignMgr.QueryByClinicID(regInfo.ID);
            if (assign != null)
            {
                if (new FS.SOC.HISFC.Assign.BizProcess.Assign().NoSee(assign, ref errInfo) < 0)
                {
                    return -1;
                }
            }
            return 1;
        }
    }
}
