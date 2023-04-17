using System;
using System.Collections.Generic;
using System.Text;

namespace FS.SOC.HISFC.BizProcess.DCPInterfaceAchieve
{
    /// <summary>
    /// 传染病报告实现
    /// </summary>
    public class DCPArchieve : FS.HISFC.BizProcess.Interface.DCP.IDCP
    {

        FS.SOC.HISFC.Components.DCP.UnionManager dcpInstance = new FS.SOC.HISFC.Components.DCP.UnionManager();

        #region IDCP 成员

        public int RegisterDiseaseReport(System.Windows.Forms.IWin32Window owner, FS.HISFC.Models.RADT.Patient patient, FS.HISFC.Models.Base.ServiceTypes patientType)
        {
            FS.SOC.HISFC.DCP.Enum.PatientType dcpPatientType = FS.SOC.HISFC.DCP.Enum.PatientType.I;
            if (patientType == FS.HISFC.Models.Base.ServiceTypes.I)
            {
                dcpPatientType = FS.SOC.HISFC.DCP.Enum.PatientType.I;
            }
            else
            {
                dcpPatientType = FS.SOC.HISFC.DCP.Enum.PatientType.C;
            }

            //if (patient == null)
            //{
            //    dcpInstance.RegisterReport(dcpPatientType);
            //}

            dcpInstance.RegisterReport(owner,patient, dcpPatientType);

            return 1;
        }

        #endregion

        #region IDCP 成员

        public int CheckDiseaseReport(System.Windows.Forms.IWin32Window owner, FS.HISFC.Models.RADT.Patient patient, FS.HISFC.Models.Base.ServiceTypes patientType, string diagName, out string msg)
        {
            msg = "";
            FS.SOC.HISFC.DCP.Enum.PatientType dcpPatientType = FS.SOC.HISFC.DCP.Enum.PatientType.I;
            if (patientType == FS.HISFC.Models.Base.ServiceTypes.I)
            {
                dcpPatientType = FS.SOC.HISFC.DCP.Enum.PatientType.I;
            }
            else
            {
                dcpPatientType = FS.SOC.HISFC.DCP.Enum.PatientType.C;
            }

            dcpInstance.CheckDisease(owner, patient, dcpPatientType, diagName, out msg);

            return 1;
        }

        #endregion

        #region IDCP 成员


        public int LoadNotice(System.Windows.Forms.IWin32Window owner, FS.HISFC.Models.Base.ServiceTypes patientType)
        {
            FS.SOC.HISFC.DCP.Enum.PatientType dcpPatientType = FS.SOC.HISFC.DCP.Enum.PatientType.I;
            if (patientType == FS.HISFC.Models.Base.ServiceTypes.I)
            {
                dcpPatientType = FS.SOC.HISFC.DCP.Enum.PatientType.I;
            }
            else
            {
                dcpPatientType = FS.SOC.HISFC.DCP.Enum.PatientType.C;
            }

            return dcpInstance.GetDCPNotice(owner,dcpPatientType);
        }

        #endregion
    }
}
