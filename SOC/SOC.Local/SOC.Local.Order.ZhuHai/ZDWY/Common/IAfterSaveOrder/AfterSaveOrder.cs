using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.Local.Order.ZhuHai.ZDWY.IAfterSaveOrder
{
    class AfterSaveOrder : FS.HISFC.BizProcess.Interface.Order.ISaveOrder
    {
        #region ISaveOrder 成员

        string errInfo = "";

        public string ErrInfo
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

        public int OnSaveOrderForInPatient(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.FrameWork.Models.NeuObject reciptDept, FS.FrameWork.Models.NeuObject reciptDoct, System.Collections.ArrayList alOrder)
        {
            return 1;
        }

        public int OnSaveOrderForOutPatient(FS.HISFC.Models.Registration.Register regObj, FS.FrameWork.Models.NeuObject reciptDept, FS.FrameWork.Models.NeuObject reciptDoct, System.Collections.ArrayList alOrder)
        {
            return 1;
        }

        #endregion
    }
}
