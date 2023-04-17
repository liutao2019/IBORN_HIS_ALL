using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.Local.RADT.GuangZhou.Common
{
    public class EmrInpatientRegister:FS.SOC.HISFC.BizProcess.MessagePatternInterface.IADT
    {

        EmrManager emrMgr = new EmrManager();

        private string err = string.Empty;

        #region IADT 成员

        public int AssignInfo(FS.HISFC.Models.Nurse.Assign assign, bool positive, int state)
        {
            return 1;
        }

        public int Balance(FS.HISFC.Models.RADT.PatientInfo patientInfo, bool positive)
        {
            return 1;
        }
        
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

        public int PatientInfo(FS.HISFC.Models.RADT.Patient patient, object patientInfo)
        {
            return 1;
        }

        public int Prepay(FS.HISFC.Models.RADT.PatientInfo patient, System.Collections.ArrayList alprepay, string flag)
        {
            return 1;
        }

        public int QueryBookingNumber(System.Collections.ArrayList alSchema)
        {
            return 1;
        }

        public int Register(object register, bool positive)
        {
            emrMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            if (register is FS.HISFC.Models.RADT.PatientInfo)
            {
                if (positive)
                {
                    FS.HISFC.Models.RADT.PatientInfo patient = register as FS.HISFC.Models.RADT.PatientInfo;

                    if (emrMgr.InsertInPatientInfo(patient) <= 0)
                    {
                        this.err = emrMgr.Err;
                        return -1;
                    }
                    
                }
            }
            else if (register is FS.HISFC.Models.Registration.Register)
            {
                if (positive)
                {
                    FS.HISFC.Models.Registration.Register regObj = register as FS.HISFC.Models.Registration.Register;
                    if (emrMgr.InsertOutPatientInfo(regObj) <= 0)
                    {
                        this.err = emrMgr.Err;
                        return -1;
                    }
                }

            }
            return 1;
        }

        #endregion
    }
}
