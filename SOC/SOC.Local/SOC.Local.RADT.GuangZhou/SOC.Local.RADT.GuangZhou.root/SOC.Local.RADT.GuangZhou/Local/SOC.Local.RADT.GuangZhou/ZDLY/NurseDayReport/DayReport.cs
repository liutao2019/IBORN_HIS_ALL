using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neusoft.SOC.Local.RADT.GuangZhou.ZDLY.NurseDayReport
{
    /// <summary>
    /// 中六本地化床位日报表管理
    /// </summary>
    public class DayReport : Neusoft.FrameWork.Management.Database 
    {
        /* 病区动态表
         * 一、特殊情况考虑
         *      1、婴儿是否计算在院、出院人数
         *      2、隔日召回数据
         *      3、包床、
         * 二、
         *      1、床位使用率，是按照人计算还是按照床位计算，（编制内床位、加床）
         *      2、每次入院都要记录入院方式：门诊入院、急诊入院、转科入院？ 如果在院期间修改了怎么办？
         *      3、每次出院都记录转归情况，主要用于统计死亡人数
         *      
         * DLL:HISFC.BizLogic.RADT,HISFC.Models
类修改：1>Neusoft.HISFC.Models.Base.EnumShiftType中添加  包床（ABD）, 解除包床（RBD）（行643）
2>Neusoft.HISFC.Models.RADT.InPatientBedTransReord 新增床位记录实体
3>Neusoft.HISFC.BizLogic.RADT.InpatientDayReport 新增三个函数
ArriveBed(Neusoft.HISFC.Models.RADT.PatientInfo patientInfo, Neusoft.HISFC.Models.Base.EnumShiftType shiftType) 
接诊用法：入院登记，护士站接诊，无费退院、出院登记、出院召回用，传入操作时的病人信息实体即可，类型为对应枚举，如入院登记 Neusoft.HISFC.Models.Base.EnumShiftType.B，每个操作对应各自的枚举，不能相同。
TransBed(Neusoft.HISFC.Models.RADT.PatientInfo patientInfoOld,Neusoft.HISFC.Models.RADT.PatientInfo patientInfoNew) 
转科用方法，转科前病人信息实体，转科后病人信息实体
AddExtentBed(Neusoft.HISFC.Models.RADT.PatientInfo patientInfo, Neusoft.HISFC.Models.Base.Bed bedInfo,bool isAdd)   
包床用方法：包床患者信息实体，被包的床位信息，是否包床（true-包床，false-解除包床）
         * */

        #region 新日报处理 2012-8-5 以后新日报以此为准

        //2013-2-22 9:33:43

        /// <summary>
        /// 上床下床标识，1-上床，0-下床
        /// </summary>
        private string strType = string.Empty;

        /// <summary>
        /// 操作来源编码
        /// </summary>
        private string strSource = string.Empty;

        /// <summary>
        /// 床位表处理
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="type">场景:入院登记（接诊）、护士站接诊、无费退院、出院登记、出院召回 </param>
        /// <returns></returns>
        public int ArriveBed(Neusoft.HISFC.Models.RADT.PatientInfo patientInfo, Neusoft.HISFC.Models.Base.EnumShiftType shiftType)
        {
            if (patientInfo == null)
            {
                return -1;
            }
            this.strSource = shiftType.ToString();
            switch (shiftType)
            {
                case Neusoft.HISFC.Models.Base.EnumShiftType.B://入院登记
                    this.strType = "0";
                    break;
                case Neusoft.HISFC.Models.Base.EnumShiftType.K://接诊
                    this.strType = "0";
                    break;
                case Neusoft.HISFC.Models.Base.EnumShiftType.OF://无费退院
                    this.strType = "1";
                    break;
                case Neusoft.HISFC.Models.Base.EnumShiftType.O://出院
                    this.strType = "1";
                    break;
                case Neusoft.HISFC.Models.Base.EnumShiftType.C://出院召回
                    this.strType = "0";
                    break;
                default:
                    break;
            }
            //插入包/解床记录
            Neusoft.HISFC.Models.RADT.InPatientBedTransReord objBedInfo = this.TransBedInfo(patientInfo);
            if (objBedInfo == null)
            {
                return -1;
            }
            return this.InSertInPatientBedTransReord(objBedInfo);
        }

        /// <summary>
        /// 床位表处理:转科
        /// </summary>
        /// <param name="patientInfoOld">转科前病人信息</param>
        /// <param name="patientInfoNew">转科前病人信息</param>
        /// <returns></returns>
        public int TransBed(Neusoft.HISFC.Models.RADT.PatientInfo patientInfoOld, Neusoft.HISFC.Models.RADT.PatientInfo patientInfoNew)
        {
            if (patientInfoOld == null || patientInfoNew == null)
            {
                return -1;
            }

            //插入原科室的下床记录
            this.strSource = Neusoft.HISFC.Models.Base.EnumShiftType.RO.ToString();
            this.strType = "1";
            Neusoft.HISFC.Models.RADT.InPatientBedTransReord objBedInfo = this.TransBedInfoForChangeDept(patientInfoOld, patientInfoNew);
            if (objBedInfo == null)
            {
                return -1;
            }
            if (this.InSertInPatientBedTransReord(objBedInfo) == -1)
            {
                return -1;
            }
            //插入新科室的上床记录
            this.strType = "0";
            this.strSource = Neusoft.HISFC.Models.Base.EnumShiftType.RI.ToString();
            objBedInfo = this.TransBedInfoForChangeDept(patientInfoOld, patientInfoNew);
            if (objBedInfo == null)
            {
                return -1;
            }
            return this.InSertInPatientBedTransReord(objBedInfo);
        }

        /// <summary>
        /// 床位表处理:包床
        /// </summary>
        /// <param name="patientInfo">包床病人信息</param>
        /// <param name="bedInfo">被包的床位信息 </param>
        /// <param name="isAdd">是否包床，true-包床，false-解除包床 </param>
        /// <returns></returns>
        public int AddExtentBed(Neusoft.HISFC.Models.RADT.PatientInfo patientInfo, Neusoft.HISFC.Models.Base.Bed bedInfo, bool isAdd)
        {
            if (patientInfo == null || bedInfo == null)
            {
                return -1;
            }
            if (isAdd)
            {
                this.strType = "0";
                this.strSource = Neusoft.HISFC.Models.Base.EnumShiftType.ABD.ToString();
            }
            else
            {
                this.strType = "1";
                this.strSource = Neusoft.HISFC.Models.Base.EnumShiftType.RBD.ToString();
            }
            Neusoft.HISFC.Models.RADT.InPatientBedTransReord objBedInfo = this.TransBedInfoAddBed(patientInfo, bedInfo);
            if (objBedInfo == null)
            {
                return -1;
            }
            return this.InSertInPatientBedTransReord(objBedInfo);
        }

        /// <summary>
        /// 转换各种信息城标准的床位记录表插入格式
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <returns></returns>
        private Neusoft.HISFC.Models.RADT.InPatientBedTransReord TransBedInfo(Neusoft.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            Neusoft.HISFC.Models.RADT.InPatientBedTransReord objBedInfo = null;
            if (patientInfo == null)
            {
                return objBedInfo;
            }
            objBedInfo = new Neusoft.HISFC.Models.RADT.InPatientBedTransReord();
            objBedInfo.INPATIENT_NO = patientInfo.ID;
            objBedInfo.PATIENT_NO = patientInfo.PID.PatientNO;
            objBedInfo.OLD_DEPT_ID = string.Empty;
            objBedInfo.OLD_DEPT_NAME = string.Empty;
            objBedInfo.TARGET_DEPT_ID = patientInfo.PVisit.PatientLocation.Dept.ID;
            objBedInfo.TARGET_DEPT_NAME = patientInfo.PVisit.PatientLocation.Dept.Name;
            objBedInfo.BED_NO = patientInfo.PVisit.PatientLocation.Bed.ID;
            objBedInfo.TRANS_TYPE = this.strType;
            objBedInfo.TRANS_CODE = this.strSource;
            objBedInfo.MEDICAL_GROUP_CODE = string.Empty;
            objBedInfo.CARE_GROUP_CODE = string.Empty;
            objBedInfo.IN_DOCT_CODE = patientInfo.PVisit.AdmittingDoctor.ID;
            objBedInfo.NURSE_STATION_CODE = patientInfo.PVisit.PatientLocation.NurseCell.ID;
            objBedInfo.ZG = patientInfo.PVisit.ZG.ID;
            objBedInfo.SEQUENCE_NO = string.Empty;
            objBedInfo.OPER_CODE = "009999";
            //objBedInfo.OPER_DATE = this.GetDateTimeFromSysDateTime();
            objBedInfo.DEPT_CODE = patientInfo.PVisit.PatientLocation.Dept.ID;
            objBedInfo.OLD_NURSE_ID = string.Empty;
            objBedInfo.OLD_NURSE_NAME = string.Empty;
            objBedInfo.TARGET_NURSE_ID = patientInfo.PVisit.PatientLocation.NurseCell.ID;
            objBedInfo.TARGET_NURSE_NAME = patientInfo.PVisit.PatientLocation.NurseCell.Name;
            return objBedInfo;

        }

        /// <summary>
        /// 转换各种信息城标准的床位记录表插入格式(转科操作专用)
        /// </summary>
        /// <param name="patientInfoOld"></param>
        /// <param name="patientInfoNew"></param>
        /// <returns></returns>
        private Neusoft.HISFC.Models.RADT.InPatientBedTransReord TransBedInfoForChangeDept(Neusoft.HISFC.Models.RADT.PatientInfo patientInfoOld, Neusoft.HISFC.Models.RADT.PatientInfo patientInfoNew)
        {
            Neusoft.HISFC.Models.RADT.InPatientBedTransReord objBedInfo = null;
            if (patientInfoOld == null || patientInfoNew == null)
            {
                return objBedInfo;
            }
            objBedInfo = new Neusoft.HISFC.Models.RADT.InPatientBedTransReord();
            objBedInfo.INPATIENT_NO = patientInfoOld.ID;
            objBedInfo.PATIENT_NO = patientInfoOld.PID.PatientNO;
            //科室
            objBedInfo.OLD_DEPT_ID = patientInfoOld.PVisit.PatientLocation.Dept.ID;
            objBedInfo.OLD_DEPT_NAME = patientInfoOld.PVisit.PatientLocation.Dept.Name;
            objBedInfo.TARGET_DEPT_ID = patientInfoNew.PVisit.PatientLocation.Dept.ID;
            objBedInfo.TARGET_DEPT_NAME = patientInfoNew.PVisit.PatientLocation.Dept.Name;
            //护士站
            objBedInfo.OLD_NURSE_ID = patientInfoOld.PVisit.PatientLocation.NurseCell.ID;
            objBedInfo.OLD_NURSE_NAME = patientInfoOld.PVisit.PatientLocation.NurseCell.Name;
            objBedInfo.TARGET_NURSE_ID = patientInfoNew.PVisit.PatientLocation.NurseCell.ID;
            objBedInfo.TARGET_NURSE_NAME = patientInfoNew.PVisit.PatientLocation.NurseCell.Name;
            if (this.strType == "1")
            {
                objBedInfo.BED_NO = patientInfoOld.PVisit.PatientLocation.Bed.ID;
                objBedInfo.DEPT_CODE = patientInfoOld.PVisit.PatientLocation.Dept.ID;
                objBedInfo.IN_DOCT_CODE = patientInfoOld.PVisit.AdmittingDoctor.ID;
                objBedInfo.NURSE_STATION_CODE = patientInfoOld.PVisit.PatientLocation.NurseCell.ID;
            }
            else
            {

                objBedInfo.BED_NO = patientInfoNew.PVisit.PatientLocation.Bed.ID;
                objBedInfo.DEPT_CODE = patientInfoNew.PVisit.PatientLocation.Dept.ID;
                objBedInfo.IN_DOCT_CODE = patientInfoNew.PVisit.AdmittingDoctor.ID;
                objBedInfo.NURSE_STATION_CODE = patientInfoNew.PVisit.PatientLocation.NurseCell.ID;
            }
            objBedInfo.TRANS_TYPE = this.strType;
            objBedInfo.TRANS_CODE = this.strSource;
            objBedInfo.MEDICAL_GROUP_CODE = string.Empty;
            objBedInfo.CARE_GROUP_CODE = string.Empty;

            objBedInfo.ZG = string.Empty;
            objBedInfo.SEQUENCE_NO = string.Empty;
            objBedInfo.OPER_CODE = "009999";
            //objBedInfo.OPER_DATE = this.GetDateTimeFromSysDateTime();

            return objBedInfo;

        }

        /// <summary>
        /// 转换各种信息城标准的床位记录表插入格式
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="bedInfo"></param>
        /// <returns></returns>
        private Neusoft.HISFC.Models.RADT.InPatientBedTransReord TransBedInfoAddBed(Neusoft.HISFC.Models.RADT.PatientInfo patientInfo, Neusoft.HISFC.Models.Base.Bed bedInfo)
        {
            Neusoft.HISFC.Models.RADT.InPatientBedTransReord objBedInfo = null;
            if (patientInfo == null)
            {
                return objBedInfo;
            }
            objBedInfo = new Neusoft.HISFC.Models.RADT.InPatientBedTransReord();
            objBedInfo.INPATIENT_NO = patientInfo.ID;
            objBedInfo.PATIENT_NO = patientInfo.PID.PatientNO;
            objBedInfo.OLD_DEPT_ID = string.Empty;
            objBedInfo.OLD_DEPT_NAME = string.Empty;
            objBedInfo.TARGET_DEPT_ID = patientInfo.PVisit.PatientLocation.Dept.ID;
            objBedInfo.TARGET_DEPT_NAME = patientInfo.PVisit.PatientLocation.Dept.Name;
            if (this.strType == "1")
            {
                objBedInfo.BED_NO = patientInfo.PVisit.PatientLocation.Bed.ID;
            }
            else
            {
                objBedInfo.BED_NO = bedInfo.ID;
            }
            objBedInfo.TRANS_TYPE = this.strType;
            objBedInfo.TRANS_CODE = this.strSource;
            objBedInfo.MEDICAL_GROUP_CODE = string.Empty;
            objBedInfo.CARE_GROUP_CODE = string.Empty;
            objBedInfo.IN_DOCT_CODE = patientInfo.PVisit.AdmittingDoctor.ID;
            objBedInfo.NURSE_STATION_CODE = patientInfo.PVisit.PatientLocation.NurseCell.ID;
            objBedInfo.ZG = patientInfo.PVisit.ZG.ID;
            objBedInfo.SEQUENCE_NO = string.Empty;
            objBedInfo.OPER_CODE = "009999";
            //objBedInfo.OPER_DATE = this.GetDateTimeFromSysDateTime();
            objBedInfo.DEPT_CODE = patientInfo.PVisit.PatientLocation.Dept.ID;

            objBedInfo.OLD_NURSE_ID = string.Empty;
            objBedInfo.OLD_NURSE_NAME = string.Empty;
            objBedInfo.TARGET_NURSE_ID = patientInfo.PVisit.PatientLocation.NurseCell.ID;
            objBedInfo.TARGET_NURSE_NAME = patientInfo.PVisit.PatientLocation.NurseCell.Name;
            return objBedInfo;
        }

        /// <summary>
        /// 插入床位变更记录
        /// </summary>
        /// <param name="p_objBedInfo">床位记录信息</param>
        /// <returns></returns>
        private int InSertInPatientBedTransReord(Neusoft.HISFC.Models.RADT.InPatientBedTransReord p_objBedInfo)
        {
            if (p_objBedInfo == null)
            {
                return -1;
            }
            string strSql = string.Empty;

            if (Sql.GetCommonSql("RADT.InPatient.InSertInPatientBedTransReord", ref strSql) == 0)
            {
                #region SQL
                /*insert into met_cas_bedinfodailyreport
                  (inpatient_no,
                   patient_no,
                   old_dept_id,
                   old_dept_name,
                   target_dept_id,
                   target_dept_name,
                   bed_no,
                   trans_type,
                   trans_code,
                   medical_group_code,
                   care_group_code,
                   in_doct_code,
                   nurse_station_code,
                   zg,
                   sequence_no,
                   oper_code,
                   oper_date,
                   OLD_NURSTATION_CODE,
                   OLD_NURSTATION_NAME,
                   TARGET_NURSTATION_CODE,
                   TARGET_NURSTATION_NAME)*/
                #endregion
                try
                {
                    strSql = string.Format(strSql,
                                           p_objBedInfo.INPATIENT_NO,
                                           p_objBedInfo.PATIENT_NO,
                                           p_objBedInfo.OLD_DEPT_ID,
                                           p_objBedInfo.OLD_DEPT_NAME,
                                           p_objBedInfo.TARGET_DEPT_ID,
                                           p_objBedInfo.TARGET_DEPT_NAME,
                                           p_objBedInfo.BED_NO,
                                           p_objBedInfo.TRANS_TYPE,
                                           p_objBedInfo.TRANS_CODE,
                                           p_objBedInfo.MEDICAL_GROUP_CODE,
                                           p_objBedInfo.CARE_GROUP_CODE,
                                           p_objBedInfo.IN_DOCT_CODE,
                                           p_objBedInfo.NURSE_STATION_CODE,
                                           p_objBedInfo.ZG,
                                           p_objBedInfo.SEQUENCE_NO,
                                           p_objBedInfo.OPER_CODE,
                                           p_objBedInfo.DEPT_CODE,
                                           p_objBedInfo.OLD_NURSE_ID,
                                           p_objBedInfo.OLD_NURSE_NAME,
                                           p_objBedInfo.TARGET_NURSE_ID,
                                           p_objBedInfo.TARGET_NURSE_NAME); //备注
                }
                catch
                {
                    Err = "传入参数错误！RADT.InPatient.InSertInPatientBedTransReord!";
                    WriteErr();
                    return -1;
                }
            }
            return ExecNoQuery(strSql);

        }
        #endregion
    }
}
