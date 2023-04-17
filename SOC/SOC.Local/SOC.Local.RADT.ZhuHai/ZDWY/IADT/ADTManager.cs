using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.Local.RADT.ZhuHai.ZDWY.IADT
{
    public class ADTManager : FS.SOC.HISFC.BizProcess.MessagePatternInterface.IADT
    {
        string errInfo = string.Empty;

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
                return errInfo;
            }
            set
            {
                errInfo = value;
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

        RegManager regMagager = new RegManager();

        public int Register(object register, bool positive)
        {
            if (register is FS.HISFC.Models.Registration.Register)
            {

                FS.HISFC.Models.Registration.Register regObj = register as FS.HISFC.Models.Registration.Register;

                if (positive)
                {

                    #region 处理预约的东东

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

                    #endregion
                }
            }
            return 1;
        }

        #endregion
    }
}
