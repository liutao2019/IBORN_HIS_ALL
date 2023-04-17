using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neusoft.SOC.Local.RADT.Common
{
    public class EmrInpatientRegister:Neusoft.SOC.HISFC.BizProcess.MessagePatternInterface.IADT
    {

        Neusoft.FrameWork.Management.DataBaseManger dbMgr = new Neusoft.FrameWork.Management.DataBaseManger();

        #region IADT 成员

        public int AssignInfo(Neusoft.HISFC.Models.Nurse.Assign assign, bool positive, int state)
        {
            return 1;
        }

        public int Balance(Neusoft.HISFC.Models.RADT.PatientInfo patientInfo, bool positive)
        {
            return 1;
        }
        private string err = string.Empty;
        public string Err
        {
            get
            {
                return err;
            }
            set
            {
                err = value;
            }
        }

        public int PatientInfo(Neusoft.HISFC.Models.RADT.Patient patient, object patientInfo)
        {
            return 1;
        }

        public int Prepay(Neusoft.HISFC.Models.RADT.PatientInfo patient, System.Collections.ArrayList alprepay, string flag)
        {
            return 1;
        }

        public int QueryBookingNumber(System.Collections.ArrayList alSchema)
        {
            return 1;
        }

        public int Register(object register, bool positive)
        {
            if (register is Neusoft.HISFC.Models.RADT.PatientInfo)
            {
                if (positive)
                {
                    Neusoft.HISFC.Models.RADT.PatientInfo patient = register as Neusoft.HISFC.Models.RADT.PatientInfo;

                    int iEmrReturn = this.updateInpatientEMRID(patient.ID);
                    if (iEmrReturn < 0)
                    {
                        this.err = "更新更新住院信息EMR流水号失败," + this.dbMgr.Err;
                        return -1;
                    }

                     iEmrReturn = this.updateCompatientEMRID(patient.PID.CardNO);
                    if (iEmrReturn < 0)
                    {
                        this.err = "更新患者基本信息EMR流水号失败," + this.dbMgr.Err;
                        return -1;
                    }

                    if (this.insertEMRInpatientInfo(patient) < 0)
                    {
                        this.err = "插入电子病历信息失败," + dbMgr.Err;
                        return -1;
                    }

                    if (this.insertEMRComPatientInfo(patient) < 0)
                    {
                        this.err = "插入电子病历信息失败," + dbMgr.Err;
                        return -1;
                    }
                }
            }
            else if (register is Neusoft.HISFC.Models.Registration.Register)
            {
                Neusoft.HISFC.Models.Registration.Register regObj = register as Neusoft.HISFC.Models.Registration.Register;
                int iEmrReturn = this.updateOutpatientEMRID(regObj.ID);
                if (iEmrReturn < 0)
                {
                    this.err = "更新更新门诊信息EMR流水号失败," + this.dbMgr.Err;
                    return -1;
                }

                iEmrReturn = this.updateCompatientEMRID(regObj.PID.CardNO);
                if (iEmrReturn < 0)
                {
                    this.err = "更新患者基本信息EMR流水号失败," + this.dbMgr.Err;
                    return -1;
                }

                if (this.insertEMROutpatientInfo(regObj) < 0)
                {
                    this.err = "插入电子病历信息失败," + dbMgr.Err;
                    return -1;
                }

                if (this.insertEMRComPatientInfo(regObj) < 0)
                {
                    this.err = "插入电子病历信息失败," + dbMgr.Err;
                    return -1;
                }

            }
            return 1;
        }

        #endregion

        private int updateInpatientEMRID(string inpatientNO)
        {
            string sql = "update FIN_IPR_INMAININFO set EMR_INPATIENTID=INPATIENT_NO where INPATIENT_NO='{0}'";
            return dbMgr.ExecNoQuery(sql, inpatientNO);
        }

        private int updateCompatientEMRID(string cardno)
        {
            string sql = "update COM_PATIENTINFO set EMR_PATID=CARD_NO where CARD_NO='{0}'";
            return dbMgr.ExecNoQuery(sql, cardno);
        }

        private int updateOutpatientEMRID(string clinic_code)
        {
            string sql = "update FIN_OPR_REGISTER set EMR_REGID=CLINIC_CODE where CLINIC_CODE='{0}'";
            return dbMgr.ExecNoQuery(sql, clinic_code);
        }

        public int insertEMRComPatientInfo(Neusoft.HISFC.Models.RADT.Patient patient)
        {
            string sql = @"MERGE INTO PT_PATIENTS t1
                                        USING (SELECT t2.emr_patid FROM com_patientinfo t2 WHERE t2.emr_patid = {0} ) S
                                        ON (t1.ID = s.emr_patid)
                                        WHEN NOT MATCHED THEN
                                        INSERT (t1.id,t1.name,t1.sex,t1.birthday)
                                        VALUES ({0}, '{1}','{2}',to_date('{3}','yyyy-mm-dd'))";
            return dbMgr.ExecNoQuery(string.Format(sql, patient.PID.CardNO, patient.Name, patient.Sex.ID, patient.Birthday.ToString("yyyy-MM-dd")));
        }

        public int insertEMRInpatientInfo(Neusoft.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            string sql = @"insert into PT_INPATIENT_CURE (ID,PATIENT_ID,INPATIENT_CODE) values({0},{1},'{2}')";
            return dbMgr.ExecNoQuery(string.Format(sql, patientInfo.ID, patientInfo.PID.CardNO, patientInfo.ID));
        }

        public int insertEMROutpatientInfo(Neusoft.HISFC.Models.Registration.Register register)
        {
            string sql = @"insert into PT_OUTPATIENT_CURE (ID,PATIENT_ID,OUTPATIENT_CODE)  values({0},{1},'{2}')";
            return dbMgr.ExecNoQuery(string.Format(sql, register.ID, register.PID.CardNO, register.ID));
        }
    }
}
