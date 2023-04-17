using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.HL7Message;
using FS.SOC.HISFC.BizProcess.MessagePatternInterface;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.HL7.PatientAdministration.ADT_A01
{
    /// <summary>
    /// 婴儿登记
    /// </summary>
    public class BabyInPatientRegister 
    {
        FS.HISFC.BizProcess.Integrate.RADT RADTIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();
        FS.HISFC.BizLogic.RADT.InPatient radtManager = new FS.HISFC.BizLogic.RADT.InPatient();

        public  int ProcessMessage(NHapi.Model.V24.Message.ADT_A01 adta01, ref NHapi.Base.Model.IMessage ackMessage, ref string errInfo)
        {
            //接收婴儿登记信息
            //母亲住院流水号
            string motherID = adta01.PID.GetMotherSIdentifier(0).ID.Value;
            //找到对应的患者信息
            if (string.IsNullOrEmpty(motherID))
            {
                errInfo = "婴儿登记失败，原因：母亲流水号为空";
                return -1;
            }

            FS.HISFC.Models.RADT.PatientInfo motherInfo = RADTIntegrate.QueryPatientInfoByInpatientNO(motherID);
            if (motherInfo == null || string.IsNullOrEmpty(motherInfo.ID))
            {
                errInfo = "婴儿登记获取" + motherID + "患者信息失败，原因：" + this.RADTIntegrate.Err;
                return -1;
            }

            //记录操作人
            FS.FrameWork.Management.Connection.Operator.ID = adta01.EVN.GetOperatorID(0).IDNumber.Value;
            if (string.IsNullOrEmpty(FS.FrameWork.Management.Connection.Operator.ID))
            {
                errInfo = "婴儿登记失败，原因：操作员编码为空";
                return -1;
            }
            FS.FrameWork.Management.Connection.Operator = FS.SOC.HISFC.BizProcess.CommonInterface.CommonController.CreateInstance().GetEmployee(FS.FrameWork.Management.Connection.Operator.ID);
            if (FS.FrameWork.Management.Connection.Operator == null)
            {
                errInfo = "婴儿登记失败，原因：传入的操作员编码，系统中找不到" + adta01.EVN.GetOperatorID(0).IDNumber.Value;
                return -1;
            }

            FS.HISFC.Models.RADT.PatientInfo babyInfo = new FS.HISFC.Models.RADT.PatientInfo();
            //住院流水号
            babyInfo.ID = adta01.PV1.VisitNumber.ID.Value; //this.radtManager.GetNewInpatientNO();
            if (string.IsNullOrEmpty(babyInfo.ID))
            {
                errInfo = "婴儿登记失败，原因：婴儿流水号为空";
                return -1;
            }

            //科室床位信息
            babyInfo.PVisit.PatientLocation = motherInfo.PVisit.PatientLocation.Clone();
            //取控件中住院医生
            babyInfo.PVisit.AdmittingDoctor = motherInfo.PVisit.AdmittingDoctor.Clone();
            //取控件中主治医生
            babyInfo.PVisit.AttendingDoctor = motherInfo.PVisit.AttendingDoctor.Clone();
            //取控件中主任医生
            babyInfo.PVisit.ConsultingDoctor = motherInfo.PVisit.ConsultingDoctor.Clone();
            //取控件中责任护士
            babyInfo.PVisit.AdmittingNurse = motherInfo.PVisit.AdmittingNurse.Clone();
            //入院情况
            babyInfo.PVisit.InSource = motherInfo.PVisit.InSource.Clone();
            //入院途径
            babyInfo.PVisit.Circs = motherInfo.PVisit.Circs.Clone();
            //入院来源
            babyInfo.PVisit.AdmitSource = motherInfo.PVisit.AdmitSource.Clone();
            //取婴儿最大序号
            string happenNo = this.radtManager.GetMaxBabyNO(motherInfo.ID);
            if (happenNo == "-1")
            {
                errInfo = "婴儿登记失败，原因：取婴儿最大序号失败" + this.radtManager.Err;
                return -1;
            }
            //加1得到当前婴儿序号
            happenNo = (FS.FrameWork.Function.NConvert.ToInt32(happenNo) + 1).ToString();
            babyInfo.User01 = happenNo; //用User01来保存婴儿序号
            //姓名
            babyInfo.Name = adta01.PID.GetPatientName(0).FamilyName.Surname.Value;
            //入院时间
            babyInfo.PVisit.InTime = DateTime.ParseExact(adta01.PV1.AdmitDateTime.TimeOfAnEvent.Value.Trim('\r'), "yyyyMMddHHmmss", null);
            //生成住院号
            babyInfo.PID.ID = "B" + happenNo + motherInfo.PID.PatientNO.Substring(2);
            //生成门诊卡号
            babyInfo.PID.CardNO = "TB" + happenNo + motherInfo.PID.PatientNO.Substring(3);
            babyInfo.Pact.PayKind.ID = "01";			//自费
            babyInfo.Pact.ID = "1";		//自费
            babyInfo.Pact.Name = "自费儿童";//自费儿童
            babyInfo.PVisit.InState.ID = "R";		//入院登记
            //性别
            babyInfo.Sex.ID = adta01.PID.AdministrativeSex.Value;
            //出生日期yyyyMMdd
            babyInfo.Birthday = DateTime.ParseExact(adta01.PID.DateTimeOfBirth.TimeOfAnEvent.Value.Trim('\r'), "yyyyMMdd", null);
            babyInfo.PID.MotherInpatientNO = motherInfo.ID;

            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.radtManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            int i = this.radtManager.UpdateBabyInfo(babyInfo);
            if (i == 0)
            {
                //登记婴儿表
                if (this.radtManager.InsertNewBabyInfo(babyInfo) != 1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    errInfo = "婴儿登记失败，原因：插入婴儿信息失败" + this.radtManager.Err;
                    return -1;
                }

                //更新变更记录主表
                if (this.radtManager.SetShiftData(babyInfo.ID, FS.HISFC.Models.Base.EnumShiftType.B, "入院登记",
                    babyInfo.PVisit.PatientLocation.Dept, babyInfo.PVisit.PatientLocation.Dept, true) < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    errInfo = "婴儿登记失败[登记]，原因：" + this.radtManager.Err;
                    return -1;
                }

                //更新变更记录
                babyInfo.PVisit.PatientLocation.Bed.Name = babyInfo.PVisit.PatientLocation.Bed.ID;
                if (this.radtManager.SetShiftData(babyInfo.ID, FS.HISFC.Models.Base.EnumShiftType.K, "接诊",
                    babyInfo.PVisit.PatientLocation.NurseCell, babyInfo.PVisit.PatientLocation.Bed, true) < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    errInfo = "婴儿登记失败[接诊]，原因：" + this.radtManager.Err;
                    return -1;
                }
            }
            else if (i < 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                errInfo = "婴儿登记失败，原因：更新婴儿信息失败" + this.radtManager.Err;
                return -1;
            }
            else//记录修改的变更记录
            {
                babyInfo.PVisit.PatientLocation.Bed.Name = babyInfo.PVisit.PatientLocation.Bed.ID;
                if (this.radtManager.SetShiftData(babyInfo.ID, FS.HISFC.Models.Base.EnumShiftType.F, "变更婴儿信息",
                    babyInfo.PVisit.PatientLocation.NurseCell, babyInfo.PVisit.PatientLocation.Bed, true) < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    errInfo = "婴儿登记失败[变更婴儿信息]，原因：" + this.radtManager.Err;
                    return -1;
                }
            }

            i = this.radtManager.UpdatePatient(babyInfo);
            //登记患者主表
            if (i == 0)
            {
                if (this.radtManager.InsertPatient(babyInfo) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    errInfo = "婴儿登记失败[住院信息]，原因：" + this.radtManager.Err;
                    return -1;
                }

                //更新病案标记,婴儿不登记病案
                if (this.radtManager.UpdateCaseSend(babyInfo.ID, false) != 1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    errInfo = "婴儿登记失败[病案标记]，原因：" + this.radtManager.Err;
                    return -1;
                }

                //更新母亲是否有婴儿标记
                if (this.radtManager.UpdateMumBabyFlag(motherInfo.ID, true) != 1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    errInfo = "婴儿登记失败[婴儿标记]，原因：" + this.radtManager.Err;
                    return -1;
                }
            }
            else if (i < 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                errInfo = "婴儿登记失败，原因：更新婴儿主表信息失败" + this.radtManager.Err;
                return -1;
            }

            //登记婴儿住院主表中的在院状态
            FS.HISFC.Models.RADT.InStateEnumService status = new FS.HISFC.Models.RADT.InStateEnumService();
            status.ID = "I"; //住院登记
            if (this.radtManager.UpdatePatientStatus(babyInfo, status) != 1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                errInfo = "婴儿登记失败[在院状态]，原因：" + this.radtManager.Err;
                return -1;
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            return 1;
        }

    }
}
