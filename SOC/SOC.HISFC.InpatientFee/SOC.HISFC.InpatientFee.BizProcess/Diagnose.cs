using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.BizProcess.CommonInterface.Common;
using System.Collections;

namespace FS.SOC.HISFC.InpatientFee.BizProcess
{
    /// <summary>
    /// [功能描述: 诊断逻辑业务类]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2012-3]<br></br>
    /// </summary>
    public class Diagnose:AbstractBizProcess
    {
        FS.HISFC.BizLogic.HealthRecord.Diagnose diagNose = new FS.HISFC.BizLogic.HealthRecord.Diagnose();

        public string QueryDiagnoseName(string inpatientNO)
        {
            string diagnose = "";
            ArrayList all = new ArrayList();

            //这里不再取MET_COM_DIAGNOSE里面的诊断信息，统一取met_cas_diagnose里面医生录入的诊断信息
            //all = diagNose.QueryDiagnoseNoOps(inpatientNO);
            if (all != null && all.Count != 0)
            {
                FS.HISFC.Models.HealthRecord.Diagnose diag = (FS.HISFC.Models.HealthRecord.Diagnose)all[0];
                diagnose = diag.DiagInfo.Name.ToString();
            }
            //如果住院医生站没有输入诊断，就从病案首页取；如果病案首页中没有,从出院志中取
            else
            {
                //病案首页的主要诊断
                ArrayList alCasMain = diagNose.QueryCaseDiagnose(inpatientNO, "1", FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC, FS.HISFC.Models.Base.ServiceTypes.I);
                if (alCasMain != null && alCasMain.Count > 0)
                {
                    FS.HISFC.Models.HealthRecord.Diagnose diag = (FS.HISFC.Models.HealthRecord.Diagnose)alCasMain[0];
                    diagnose = diag.DiagInfo.Name.ToString();
                    if (string.IsNullOrEmpty(diagnose))
                    {
                        diagnose = diag.DiagInfo.ICD10.Name;
                    }
                }
                else
                {
                    //病案首页的其他诊断
                    ArrayList alcasOther = diagNose.QueryCaseDiagnose(inpatientNO, "%", FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC, FS.HISFC.Models.Base.ServiceTypes.I);
                    if (alcasOther != null && alcasOther.Count > 0)
                    {
                        foreach (FS.HISFC.Models.HealthRecord.Diagnose diag in alcasOther)
                        {
                            if (diag.MainFlag == "1"
                                || diag.DiagInfo.IsMain)
                            {
                                diagnose = diag.DiagInfo.ICD10.Name;
                                break;
                            }
                        }
                        if (string.IsNullOrEmpty(diagnose))
                        {
                            FS.HISFC.Models.HealthRecord.Diagnose diag = (FS.HISFC.Models.HealthRecord.Diagnose)alcasOther[0];
                            diagnose = diag.DiagInfo.ICD10.Name;
                        }
                    }
                    else
                    {
                        //出院志中的诊断
                        System.Collections.Generic.List<FS.FrameWork.Models.NeuObject> listCYZ = diagNose.QueryDiagnoseFromOutCaseByInpatient(inpatientNO);
                        if (listCYZ != null && listCYZ.Count > 0)
                        {
                            FS.FrameWork.Models.NeuObject objTemp = (FS.FrameWork.Models.NeuObject)listCYZ[0];
                            diagnose = objTemp.Memo;
                            diagnose = diagnose.Replace("\n", "  ");
                        }
                    }
                }
            }

            return diagnose;
        }
    }
}
