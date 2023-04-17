using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.BizProcess.MessagePatternInterface;
using FS.HL7Message;
using System.Collections;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.HL7.PatientAdministration.ADT_A02
{
    /// <summary>
    /// 住院转科确认
    /// </summary>
    public class InPatientTransfer
    {
        FS.HISFC.BizProcess.Integrate.RADT RADTIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();
        FS.HISFC.BizLogic.RADT.InPatient radtManager = new FS.HISFC.BizLogic.RADT.InPatient();

        /// <summary>
        /// 接收消息
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public int ProcessMessage(NHapi.Model.V24.Message.ADT_A02 adtA02, ref NHapi.Base.Model.IMessage ackMessage, ref string errInfo)
        {
            if (string.IsNullOrEmpty(adtA02.PV1.VisitNumber.ID.Value))
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                errInfo = "转科失败，原因：患者流水号为空";
                return -1;
            }
            //获取患者信息
            FS.HISFC.Models.RADT.PatientInfo patientInfo = RADTIntegrate.QueryPatientInfoByInpatientNO(adtA02.PV1.VisitNumber.ID.Value);
            if (patientInfo == null || string.IsNullOrEmpty(patientInfo.ID))
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                errInfo = "转科获取" + patientInfo.ID + "患者信息失败，原因：" + this.RADTIntegrate.Err;
                return -1;
            }
            //新位置
            FS.HISFC.Models.RADT.Location newLocation = new FS.HISFC.Models.RADT.Location();
            //科室
            newLocation.Dept.ID = adtA02.PV1.AssignedPatientLocation.Facility.NamespaceID.Value;
            if (string.IsNullOrEmpty(newLocation.Dept.ID))
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                errInfo = "转科失败，原因：科室编码为空";
                return -1;
            }

            newLocation.Dept = FS.SOC.HISFC.BizProcess.CommonInterface.CommonController.CreateInstance().GetDepartment(newLocation.Dept.ID);
            if (newLocation.Dept == null)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                errInfo = "转科失败，原因：传入的科室编码，系统中找不到" + adtA02.PV1.AssignedPatientLocation.Facility.NamespaceID.Value;
                return -1;
            }

            //取病区
            newLocation.NurseCell.ID = adtA02.PV1.AssignedPatientLocation.PointOfCare.Value;
            if (string.IsNullOrEmpty(newLocation.Dept.ID))
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                errInfo = "转科失败，原因：病区编码为空";
                return -1;
            }
            newLocation.NurseCell = FS.SOC.HISFC.BizProcess.CommonInterface.CommonController.CreateInstance().GetDepartment(newLocation.NurseCell.ID);
            if (newLocation.NurseCell == null)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                errInfo = "转科失败，原因：传入的病区编码，系统中找不到" + adtA02.PV1.AssignedPatientLocation.PointOfCare.Value;
                return -1;
            }

            //病床
            newLocation.Bed.ID = adtA02.PV1.AssignedPatientLocation.Bed.Value;
            if (string.IsNullOrEmpty(newLocation.Dept.ID))
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                errInfo = "转科失败，原因：病床编码为空";
                return -1;
            }

            //床位号=病区编码+床位编码
            newLocation.Bed.ID = newLocation.NurseCell.ID + newLocation.Bed.ID;
            newLocation.Bed.Status.ID = "U";					//新床的状态
            newLocation.Bed.InpatientNO = "N";					//新床的患者住院流水号
            //记录操作人
            FS.FrameWork.Management.Connection.Operator.ID = adtA02.EVN.GetOperatorID(0).IDNumber.Value;
            if (string.IsNullOrEmpty(FS.FrameWork.Management.Connection.Operator.ID))
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                errInfo = "转科失败，原因：操作员编码为空";
                return -1;
            }
            FS.FrameWork.Management.Connection.Operator = FS.SOC.HISFC.BizProcess.CommonInterface.CommonController.CreateInstance().GetEmployee(FS.FrameWork.Management.Connection.Operator.ID);
            if (FS.FrameWork.Management.Connection.Operator == null)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                errInfo = "转科失败，原因：传入的操作员编码，系统中找不到" + adtA02.EVN.GetOperatorID(0).IDNumber.Value;
                return -1;
            }

            //开启事务
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.RADTIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.radtManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            try
            {
                //去系统时间
                DateTime sysDate = radtManager.GetDateTimeFromSysDateTime();

                //接珍处理(1更新患者信息, 2插入接珍表), 注:只要有接珍操作,都进行此处理
                if (radtManager.RecievePatient(patientInfo, FS.HISFC.Models.Base.EnumInState.I) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    errInfo = "转科失败，原因：" + radtManager.Err;
                    return -1;
                }

                //转科处理 不需要申请
                int parm = radtManager.TransferPatientLocation(patientInfo, newLocation);
                if (parm == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    errInfo = "转科失败，原因：" + radtManager.Err;
                    return -1;
                }
                else if (parm == 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    errInfo = "转科失败，原因：患者信息有变动,请刷新当前窗口";
                    return -1;
                }

                //释放包床和挂床
                ArrayList al = new ArrayList();
                al = radtManager.GetSpecialBedInfo(patientInfo.ID);
                for (int i = 0; i < al.Count; i++)
                {
                    FS.HISFC.Models.Base.Bed obj;
                    obj = (FS.HISFC.Models.Base.Bed)al[i];
                    if (radtManager.UnWrapPatientBed(patientInfo, obj.ID, obj.Memo) < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        errInfo = "转科失败，原因：释放包床和挂床失败！" + radtManager.Err;
                        return -1;
                    }
                }
            }
            catch (Exception ex)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                errInfo = "转科失败，原因：" + ex.Message;
                return -1;
            }

            FS.FrameWork.Management.PublicTrans.Commit();
            return 1;
        }
    }
}
