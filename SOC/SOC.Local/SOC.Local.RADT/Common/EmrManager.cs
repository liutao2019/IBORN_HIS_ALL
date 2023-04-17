using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.Local.RADT.Common
{

    public class EmrManager:FS.FrameWork.Management.Database
    {
        private int UpdateInpatientEMRID(string inpatientNO)
        {
            string sql = "update FIN_IPR_INMAININFO set EMR_INPATIENTID=INPATIENT_NO where INPATIENT_NO='{0}'";
            return this.ExecNoQuery(sql, inpatientNO);
        }

        private int UpdateCompatientEMRID(string cardno)
        {
            string sql = "update COM_PATIENTINFO set EMR_PATID=CARD_NO where CARD_NO='{0}'";
            return this.ExecNoQuery(sql, cardno);
        }

        private int UpdateOutpatientEMRID(string clinic_code)
        {
            string sql = "update FIN_OPR_REGISTER set EMR_REGID=CLINIC_CODE where CLINIC_CODE='{0}'";
            return this.ExecNoQuery(sql, clinic_code);
        }

        private int InsertEMRComPatientInfo(FS.HISFC.Models.RADT.Patient patient)
        {
            string sql = @"MERGE INTO PT_PATIENTS t1
                                        USING (SELECT t2.emr_patid FROM com_patientinfo t2 WHERE t2.emr_patid = {0} ) S
                                        ON (t1.ID = s.emr_patid)
                                        WHEN NOT MATCHED THEN
                                        INSERT (t1.id,t1.name,t1.sex,t1.birthday)
                                        VALUES ({0}, '{1}','{2}',to_date('{3}','yyyy-mm-dd'))";
            return this.ExecNoQuery(string.Format(sql, patient.PID.CardNO, patient.Name, patient.Sex.ID, patient.Birthday.ToString("yyyy-MM-dd")));
        }

        private int InsertEMRInpatientInfo(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            string sql = @"insert into PT_INPATIENT_CURE (ID,PATIENT_ID,INPATIENT_CODE) values({0},{1},'{2}')";
            return this.ExecNoQuery(string.Format(sql, patientInfo.ID, patientInfo.PID.CardNO, patientInfo.ID));
        }

        private int InsertEMROutpatientInfo(FS.HISFC.Models.Registration.Register register)
        {
            string sql = @"insert into PT_OUTPATIENT_CURE (ID,PATIENT_ID,OUTPATIENT_CODE)  values({0},{1},'{2}')";
            return this.ExecNoQuery(string.Format(sql, register.ID, register.PID.CardNO, register.ID));
        }

        public int InsertOutPatientInfo(FS.HISFC.Models.Registration.Register regObj)
        {
            int iEmrReturn = this.UpdateOutpatientEMRID(regObj.ID);
            if (iEmrReturn < 0)
            {
                this.Err = "更新更新门诊信息EMR流水号失败," + this.Err;
                return -1;
            }

            iEmrReturn = this.UpdateCompatientEMRID(regObj.PID.CardNO);
            if (iEmrReturn < 0)
            {
                this.Err = "更新患者基本信息EMR流水号失败," + this.Err;
                return -1;
            }

            if (this.InsertEMROutpatientInfo(regObj) < 0)
            {
                this.Err = "插入电子病历信息失败," + this.Err;
                return -1;
            }

            if (this.InsertEMRComPatientInfo(regObj) < 0)
            {
                this.Err = "插入电子病历信息失败," + this.Err;
                return -1;
            }

            return 1;
        }

        public int InsertInPatientInfo(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            int iEmrReturn = this.UpdateInpatientEMRID(patient.ID);
            if (iEmrReturn < 0)
            {
                this.Err = "更新更新住院信息EMR流水号失败," + this.Err;
                return -1;
            }

            iEmrReturn = this.UpdateCompatientEMRID(patient.PID.CardNO);
            if (iEmrReturn < 0)
            {
                this.Err = "更新患者基本信息EMR流水号失败," + this.Err;
                return -1;
            }

            if (this.InsertEMRInpatientInfo(patient) < 0)
            {
                this.Err = "插入电子病历信息失败," + this.Err;
                return -1;
            }

            if (this.InsertEMRComPatientInfo(patient) < 0)
            {
                this.Err = "插入电子病历信息失败," + this.Err;
                return -1;
            }

            return 1;
        }
    }
}
