using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.Local.RADT.ZhuHai.Common
{
    public class ADTManager : FS.SOC.HISFC.BizProcess.MessagePatternInterface.IADT
    {
        #region IADT 成员

        public int AssignInfo(FS.HISFC.Models.Nurse.Assign assign, bool positive, int state)
        {
            return 1;
        }

        public int Balance(FS.HISFC.Models.RADT.PatientInfo patientInfo, bool positive)
        {
            return 1;
        }

        string err = "";

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
            
            return 1;
        }

        #endregion
    }
}
