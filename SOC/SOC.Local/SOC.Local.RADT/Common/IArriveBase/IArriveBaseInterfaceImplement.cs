using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.HISFC.Models.RADT;

namespace FS.SOC.Local.RADT.Common.IArriveBase
{
    /// <summary>
    /// 住院接诊、转入、召回接口实现
    /// </summary>
    class IArriveBaseInterfaceImplement : FS.SOC.HISFC.BizProcess.NurseInterface.IArriveBase
    {
        /// <summary>
        /// 电子病历住院业务处理
        /// </summary>
        FS.SOC.HISFC.BizLogic.EmrNew.EMRInpatinetLogic emrLogicManager = new FS.SOC.HISFC.BizLogic.EmrNew.EMRInpatinetLogic();

        #region IArriveBase 成员

        /// <summary>
        /// 错误信息
        /// </summary>
        string errInfo = "";

        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrInfo
        {
            get
            {
                return errInfo;
            }
        }

        /// <summary>
        /// 住院护士站接诊、转入、召回操作
        /// </summary>
        /// <param name="arriveType">接诊类别</param>
        /// <param name="oldPatientInfo">旧的患者信息</param>
        /// <param name="newPatientInfo">新的患者信息</param>
        /// <returns></returns>
        public int PatientArrive(EnumArriveType arriveType, FS.HISFC.Models.RADT.PatientInfo oldPatientInfo, FS.HISFC.Models.RADT.PatientInfo newPatientInfo)
        {
            long emrPatID = -1;
            int iEmrReturn = -1;

            if (arriveType == EnumArriveType.Accepts)
            {
                iEmrReturn = emrLogicManager.GetPatientId(newPatientInfo.ID, ref emrPatID);

                if (iEmrReturn < 0)
                {
                    errInfo = "调用EMR接口GetPatientId失败！" + emrLogicManager.Err;
                    return -1;
                }

                iEmrReturn = emrLogicManager.InsertPTDoc(emrPatID, newPatientInfo.PVisit.ConsultingDoctor.ID,
                    newPatientInfo.PVisit.AttendingDoctor.ID, newPatientInfo.PVisit.AdmittingDoctor.ID, newPatientInfo.PVisit.PatientLocation.Dept.ID);
                if (iEmrReturn < 0)
                {
                    errInfo = "调用EMR接口ChangePTDoc失败！" + emrLogicManager.Err;
                    return -1;
                }
            }
            else if (arriveType == EnumArriveType.CallBack)
            {
                if (newPatientInfo.IsBaby == false) //婴儿不需调用EMR接口 {2FC149FF-73E1-4a6b-98EA-411BD999F51A}
                {
                    iEmrReturn = emrLogicManager.GetPatientId(newPatientInfo.ID, ref emrPatID);

                    if (iEmrReturn < 0)
                    {
                        errInfo = "调用EMR接口GetPatientId失败！" + emrLogicManager.Err;
                        return -1;
                    }

                    iEmrReturn = emrLogicManager.ChangePTDocReCall(emrPatID, newPatientInfo.PVisit.ConsultingDoctor.ID,
                        newPatientInfo.PVisit.AttendingDoctor.ID, newPatientInfo.PVisit.AdmittingDoctor.ID, newPatientInfo.PVisit.PatientLocation.Dept.ID);
                    if (iEmrReturn < 0)
                    {
                        errInfo = "调用EMR接口ChangePTDoc失败！" + emrLogicManager.Err;
                        return -1;
                    }
                }
            }
            else if (arriveType == EnumArriveType.ChangeDoct)
            {
                iEmrReturn = emrLogicManager.GetPatientId(newPatientInfo.ID, ref emrPatID);

                if (iEmrReturn < 0)
                {
                    errInfo = "调用EMR接口GetPatientId失败！" + emrLogicManager.Err;
                    return -1;
                }

                //先变更原来的医师信息
                iEmrReturn = emrLogicManager.ChangePTDoc(emrPatID, oldPatientInfo.PVisit.ConsultingDoctor.ID,
                    oldPatientInfo.PVisit.AttendingDoctor.ID, oldPatientInfo.PVisit.AdmittingDoctor.ID, oldPatientInfo.PVisit.PatientLocation.Dept.ID);
                if (iEmrReturn < 0)
                {
                    errInfo = "调用EMR接口ChangePTDoc失败！" + emrLogicManager.Err;
                    return -1;
                }
                //插入变更后的医师信息
                iEmrReturn = emrLogicManager.InsertPTDoc(emrPatID, newPatientInfo.PVisit.ConsultingDoctor.ID,
                    newPatientInfo.PVisit.AttendingDoctor.ID, newPatientInfo.PVisit.AdmittingDoctor.ID, newPatientInfo.PVisit.PatientLocation.Dept.ID);
                if (iEmrReturn < 0)
                {
                    errInfo = "调用EMR接口ChangePTDoc失败！" + emrLogicManager.Err;
                    return -1;
                }
            }
            else if (arriveType == EnumArriveType.ShiftIn)
            {
                iEmrReturn = this.emrLogicManager.GetPatientId(newPatientInfo.ID, ref emrPatID);

                if (iEmrReturn < 0)
                {
                    errInfo = "调用EMR接口GetPatientId失败！" + emrLogicManager.Err;
                    return -1;
                }

                //先变更原来的医师信息
                iEmrReturn = emrLogicManager.ChangePTDoc(emrPatID, oldPatientInfo.PVisit.ConsultingDoctor.ID,
                    oldPatientInfo.PVisit.AttendingDoctor.ID, oldPatientInfo.PVisit.AdmittingDoctor.ID, oldPatientInfo.PVisit.PatientLocation.Dept.ID);
                if (iEmrReturn < 0)
                {
                    errInfo = "调用EMR接口ChangePTDoc失败！" + emrLogicManager.Err;
                    return -1;
                }
                //插入变更后的医师信息
                iEmrReturn = emrLogicManager.InsertPTDoc(emrPatID, oldPatientInfo.PVisit.ConsultingDoctor.ID,
                    oldPatientInfo.PVisit.AttendingDoctor.ID, oldPatientInfo.PVisit.AdmittingDoctor.ID, oldPatientInfo.PVisit.PatientLocation.Dept.ID);
                if (iEmrReturn < 0)
                {
                    errInfo = "调用EMR接口ChangePTDoc失败！" + emrLogicManager.Err;
                    return -1;
                }
            }
            return 1;
        }

        #endregion
    }
}
