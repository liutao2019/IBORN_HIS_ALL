using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.BizProcess.MessagePatternInterface;
using FS.HL7Message;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.HL7.PatientAdministration.ADT_A03
{
    /// <summary>
    /// 门诊医生诊出患者
    /// </summary>
    public class OutPatientSeeOut
    {
        /// <summary>
        /// 挂号业务层
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Registration.Registration regIntegrate = new FS.HISFC.BizProcess.Integrate.Registration.Registration();

        /// <summary>
        /// 分诊业务
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        protected FS.FrameWork.Management.ControlParam controlMgr = new FS.FrameWork.Management.ControlParam();

        public int ProcessMessage(NHapi.Model.V24.Message.ADT_A03 adtA03, ref NHapi.Base.Model.IMessage ackMessage, ref string errInfo)
        {
            FS.HISFC.Models.Registration.Register register = new FS.HISFC.Models.Registration.Register();
            string clinicCode = adtA03.PV1.VisitNumber.ID.Value;

            //获取患者挂号信息
            register = regIntegrate.GetByClinic(clinicCode);
            if (register == null || string.IsNullOrEmpty(register.ID))
            {
                errInfo = "获取挂号信息失败，原因：" + regIntegrate.Err +"PV1-19="+ clinicCode;
                return -1;
            }

            if (register.Status == FS.HISFC.Models.Base.EnumRegisterStatus.Back)
            {
                errInfo = "获取挂号信息失败，原因：挂号记录已退号"+register.ID;
                return -1;
            }
            else if (register.Status == FS.HISFC.Models.Base.EnumRegisterStatus.Cancel)
            {
                errInfo = "获取挂号信息失败，原因：挂号记录已作废" + register.ID;
                return -1;
            }

            //PVI
            NHapi.Model.V24.Datatype.XCN attendingDoctor = adtA03.PV1.GetAttendingDoctor(0); //看诊医生
            register.SeeDoct.ID = attendingDoctor.IDNumber.Value;
            register.SeeDoct.Name = attendingDoctor.FamilyName.Surname.Value;
            register.SeeDoct.Dept.ID = adtA03.PV1.AssignedPatientLocation.Facility.NamespaceID.Value; //看诊科室
            //register.ID = adtA03.PV1.VisitNumber.ID.Value;
            //register.DoctorInfo.SeeDate = FS.FrameWork.Function.NConvert.ToDateTime(adtA03.PV1.GetDischargeDateTime());
            //取得患者分诊信息
            FS.HISFC.Models.Nurse.Assign assign = managerIntegrate.QueryAssignByClinicID(register.ID);

            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.managerIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.regIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            
            int iReturn;

            #region 更新分诊
            if (assign != null && assign.Queue.SRoom != null)
            {
                iReturn = this.managerIntegrate.UpdateAssign(assign.Queue.SRoom.ID, register.ID, controlMgr.GetDateTimeFromSysDateTime(), register.SeeDoct.ID);
                if (iReturn < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    errInfo = FS.FrameWork.Management.Language.Msg("更新分诊标志出错！");
                    return -1;
                }
            }

            #endregion

            #region 更新看诊
            if (!register.IsSee || string.IsNullOrEmpty(register.SeeDoct.ID) || string.IsNullOrEmpty(register.SeeDoct.Dept.ID))
            {
                iReturn = this.regIntegrate.UpdateSeeDone(register.ID);
                if (iReturn < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    errInfo = "更新看诊标志出错！";
                    return -1;
                }

                iReturn = this.regIntegrate.UpdateDept(register.ID, register.SeeDoct.Dept.ID, register.SeeDoct.ID);
                if (iReturn < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    errInfo = "更新看诊科室、医生出错！";
                    return -1;
                }
            }
            #endregion

            FS.FrameWork.Management.PublicTrans.Commit();
            return 1;
        }
        
    }
}
