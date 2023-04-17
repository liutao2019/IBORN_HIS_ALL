using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Neusoft.SOC.Local.RADT.GuangZhou.Common;

namespace Neusoft.SOC.Local.RADT.GuangZhou.ZDLY.IADT
{
    public class EmrInpatientRegister:Neusoft.SOC.HISFC.BizProcess.MessagePatternInterface.IADT
    {

        EmrManager emrMgr = new EmrManager();

        private string err = string.Empty;

        #region IADT 成员

        public int AssignInfo(Neusoft.HISFC.Models.Nurse.Assign assign, bool positive, int state)
        {
            return 1;
        }

        public int Balance(Neusoft.HISFC.Models.RADT.PatientInfo patientInfo, bool positive)
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

        RegManager regMagager = new RegManager();

        public int Register(object register, bool positive)
        {
            emrMgr.SetTrans(Neusoft.FrameWork.Management.PublicTrans.Trans);

            if (register is Neusoft.HISFC.Models.RADT.PatientInfo)
            {
                if (positive)
                {
                    Neusoft.HISFC.Models.RADT.PatientInfo patient = register as Neusoft.HISFC.Models.RADT.PatientInfo;

                    if (emrMgr.InsertInPatientInfo(patient) <= 0)
                    {
                        this.err = emrMgr.Err;
                        return -1;
                    }
                    
                }
            }
            else if (register is Neusoft.HISFC.Models.Registration.Register)
            {
                if (positive)
                {
                    Neusoft.HISFC.Models.Registration.Register regObj = register as Neusoft.HISFC.Models.Registration.Register;
                    if (emrMgr.InsertOutPatientInfo(regObj) <= 0)
                    {
                        this.err = emrMgr.Err;
                        return -1;
                    }

                    try
                    {
                        if (regObj.RegExtend != null && !string.IsNullOrEmpty(regObj.RegExtend.BookingTypeId))
                        {
                            regObj.RegExtend.ID = regObj.ID;
                            regMagager.InsertRegExtendInfo(regObj.RegExtend);
                        }
                    }
                    catch
                    { 
                        //DO NOTHING.
                    }
                }

            }
            return 1;
        }

        #endregion
    }
}
