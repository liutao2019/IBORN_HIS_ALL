using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Neusoft.HISFC.BizProcess.Interface.Common;

namespace HISTIMEJOB
{
    /// <summary>
    /// LIS试管收取
    /// </summary>
    class LisTubeFee : Neusoft.FrameWork.Management.Database, IJob
    {
        private Neusoft.HISFC.BizProcess.Integrate.RADT radtMgr = new Neusoft.HISFC.BizProcess.Integrate.RADT();

        /// <summary>
        /// 收取LIS试管费用接口
        /// </summary>
        private Neusoft.HISFC.BizProcess.Interface.FeeInterface.ILisCalculateTube iLisCalculateTube = null;
        
        public int LisTubeFeeStart()
        {
            this.iLisCalculateTube = Neusoft.FrameWork.WinForms.Classes.UtilInterface.CreateObject<Neusoft.HISFC.BizProcess.Interface.FeeInterface.ILisCalculateTube>(this.GetType());
            ArrayList alPatient = this.radtMgr.QueryPatient(Neusoft.HISFC.Models.Base.EnumInState.I);
            if (alPatient == null)
            {
                errInfo = radtMgr.Err;
                return -1;
            }

            foreach (Neusoft.HISFC.Models.RADT.PatientInfo patientInfo in alPatient)
            {
                if (this.iLisCalculateTube != null)
                {
                    if (this.iLisCalculateTube.LisCalculateTubeForInPatient(patientInfo) == -1)
                    {
                        this.Err = this.iLisCalculateTube.ErrInfo;
                        return -1;
                    }
                }
            }
            return 1;
        }

        #region IJob 成员
        private string errInfo = "";

        public string Message
        {
            get { return errInfo; }
        }

        public int Start()
        {
            return this.LisTubeFeeStart();
        }

        #endregion
    }
}
