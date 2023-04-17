using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GJLocal.HISFC.Components.OpGuide.RecipePrint.Controls
{
    public static class Function
    {
        private static Neusoft.HISFC.BizLogic.Manager.DataBase dbMgr = null;
        private static Neusoft.HISFC.BizLogic.HealthRecord.Diagnose diagManager = null;

        public static string GetAge(DateTime dt)
        {
            if (dbMgr == null)
            {
                dbMgr = new Neusoft.HISFC.BizLogic.Manager.DataBase();
            }

            return dbMgr.GetAge(dt);
        }

        public static string GetDiag(string clinicCode)
        {
            if (diagManager == null)
            {
                diagManager = new Neusoft.HISFC.BizLogic.HealthRecord.Diagnose();
            }

            System.Collections.ArrayList al = diagManager.QueryCaseDiagnoseForClinic(clinicCode, Neusoft.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC);
            if (al == null)
            {
                return "";
            }

            string strDiag = "";
            foreach (Neusoft.HISFC.Models.HealthRecord.Diagnose diag in al)
            {
                if (diag != null && diag.IsValid)
                {
                    strDiag += diag.DiagInfo.ICD10.Name + "、";
                }
            }

            return strDiag.TrimEnd('、');
        }
    }
}
