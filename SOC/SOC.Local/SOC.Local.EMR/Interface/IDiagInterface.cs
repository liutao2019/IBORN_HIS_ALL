using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace FS.SOC.Local.EMR.Interface
{
    /// <summary>
    /// 住院医嘱与电子病历诊断接口
    /// </summary>
    /// {3745f152-cc8b-45f6-9f63-d6d591f8c654}
    public class IDiagInterface
    {
        FS.HISFC.BizLogic.HealthRecord.Diagnose diagMgr = new FS.HISFC.BizLogic.HealthRecord.Diagnose();
        FS.HISFC.BizProcess.Integrate.RADT interRadt = new FS.HISFC.BizProcess.Integrate.RADT();
        FS.HISFC.BizProcess.Integrate.Registration.Registration outpatientRegisterMgr = new FS.HISFC.BizProcess.Integrate.Registration.Registration();

        #region 门诊

        /// <summary>
        /// 删除门诊诊断
        /// </summary>
        /// <param name="inpatientNO"></param>
        /// <param name="deptCode"></param>
        /// <param name="deptName"></param>
        /// <param name="docCode"></param>
        /// <param name="docName"></param>
        /// <param name="emrDiagnoseKey">EMR诊断主键（HIS里面是发生序号，如，第一个诊断就是1，第二个诊断就是2）</param>
        /// <param name="diagnoseType">诊断类别</param>
        /// <param name="icdCode">ICD编码</param>
        /// <param name="icdName">ICD名称</param>
        /// <param name="hosDiagnoseCode">目前HIS没用，不知道干什么用的</param>
        /// <param name="hosDiagnoseName">目前HIS没用，不知道干什么用的</param>
        /// <param name="mainFlag">主诊断标记</param>
        /// <param name="errText">错误信息</param>
        /// <returns></returns>
        public int DeleteOutpatientDiagnose(string inpatientNO, string deptCode, string deptName, string docCode, string docName, string emrDiagnoseKey, string diagnoseType, string icdCode, string icdName, string hosDiagnoseCode, string hosDiagnoseName, bool mainFlag, out string errText)
        {
            errText = "";
            int rev = diagMgr.DeleteDiagnoseSingle(
                inpatientNO, 
                FS.FrameWork.Function.NConvert.ToInt32(emrDiagnoseKey), 
                FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC,
                FS.HISFC.Models.Base.ServiceTypes.C);
            if (rev == -1)
            {
                errText = diagMgr.Err;
                return -1;
            }

            #region hl7消息发送
            //FS.SOC.HISFC.BizProcess.MessagePatternInterface.IOrder IOrder = InterfaceManager.GetIOrder();
            //if (IOrder != null)
            //{
            //    var regInfo = this.outpatientRegisterMgr.GetByClinic(inpatientNO);
            //    FS.HISFC.Models.HealthRecord.Diagnose diagNose = this.diagMgr.GetCaseDiagnoseByEmrId(inpatientNO, emrDiagnoseKey);
            //    if (diagNose != null && regInfo != null)
            //    {
            //        diagNose.DiagInfo.Patient = regInfo.Clone();

            //        int retrunValue = IOrder.SendDiagInfo(regInfo, diagNose, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType.Delete, false);
            //        return retrunValue;
            //    }
            //}
            #endregion

            return 1;
        }

        /// <summary>
        /// 保存门诊诊断
        /// </summary>
        /// <param name="clinicCode">门诊流水号</param>
        /// <param name="deptCode"></param>
        /// <param name="deptName"></param>
        /// <param name="docCode"></param>
        /// <param name="docName"></param>
        /// <param name="emrDiagnoseKey">EMR诊断主键（HIS里面是发生序号，如，第一个诊断就是1，第二个诊断就是2）</param>
        /// <param name="diagnoseType">诊断类别</param>
        /// <param name="icdCode">ICD编码</param>
        /// <param name="icdName">ICD名称</param>
        /// <param name="hosDiagnoseCode">目前HIS没用，不知道干什么用的</param>
        /// <param name="hosDiagnoseName">目前HIS没用，不知道干什么用的</param>
        /// <param name="mainFlag">主诊断标记</param>
        /// <param name="errText">错误信息</param>
        /// <returns></returns>
        public int SaveOutpatientDiagnose(string clinicCode, string deptCode, string deptName, string docCode,
            string docName, string emrDiagnoseKey, string diagnoseType, string icdCode,
            string icdName, string hosDiagnoseCode, string hosDiagnoseName, bool mainFlag, bool isValid, bool isDoubleFull,
            out string errText)
        {
            errText = string.Empty;

            var regInfo = outpatientRegisterMgr.GetByClinic(clinicCode);
            if (regInfo == null)
            {
                errText = "未找到有效的门诊挂号记录:" + clinicCode + "。";

                return -1;
            }

            var diagNose = new FS.HISFC.Models.HealthRecord.Diagnose();

            diagNose.DiagInfo.HappenNo = FS.FrameWork.Function.NConvert.ToInt32(emrDiagnoseKey);

            diagNose.DiagInfo.ICD10.ID = icdCode;
            diagNose.DiagInfo.ICD10.Name = icdName;
            diagNose.DiagInfo.Patient.ID = clinicCode;
            diagNose.DiagInfo.DiagType.ID = diagnoseType;//诊断类型
            diagNose.DiagInfo.DiagDate = diagMgr.GetDateTimeFromSysDateTime();
            diagNose.DiagInfo.Doctor.ID = docCode;
            diagNose.DiagInfo.Doctor.Name = docName;

            diagNose.SecondICD = "";//第二ICD    
            diagNose.SynDromeID = "";// 并发症类别   
            diagNose.CLPA = "";//病理符合
            diagNose.DubDiagFlag = (isDoubleFull ? "1" : "0");//是否疑诊     
            //diagNose.MainFlag = mainFlag ? "1" : "0";//是否主诊断 1 主诊断 0 其他诊断        
            diagNose.DiagInfo.IsMain = mainFlag;
            //diagNose.Memo;//备注  
            diagNose.ID = diagMgr.Operator.ID;//操作员
            diagNose.OperType = "1";//类别 1 医生站录入诊断  2 病案室录入诊断
            //diagNose.OperationFlag;// 手术标志 1 有手术 0 没有手术  
            //diagNose.Is30Disease; //是否是30种疾病
            diagNose.PerssonType = FS.HISFC.Models.Base.ServiceTypes.C; //患者类别 0 门诊患者 1 住院患者
            diagNose.IsValid = isValid; //是否有效
            //diagNose.Diagnosis_flag;   //是否初诊

            // 先写入，不成功则更新。
            int param = diagMgr.InsertCasDiagnose(diagNose);
            if (param == -1)
            {
                diagNose.IsValid = isValid;
                param = diagMgr.UpdateDiagnose(diagNose);
                if (param == -1)
                {
                    errText = diagMgr.Err;
                    return -1;
                }
            }

            #region hl7消息发送
            //FS.SOC.HISFC.BizProcess.MessagePatternInterface.IOrder IOrder = InterfaceManager.GetIOrder();
            //if (IOrder != null)
            //{
            //    diagNose.DiagInfo.Patient = regInfo.Clone();
            //    int retrunValue = IOrder.SendDiagInfo(regInfo, diagNose, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType.Add, true);
            //    return retrunValue;
            //}
            #endregion


            return 1;
        }

        #endregion

        #region 住院

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inpatientNO">住院流水号</param>
        /// <param name="deptCode"></param>
        /// <param name="deptName"></param>
        /// <param name="docCode"></param>
        /// <param name="docName"></param>
        /// <param name="emrDiagnoseKey">EMR诊断主键（HIS里面是发生序号，如，第一个诊断就是1，第二个诊断就是2）</param>
        /// <param name="diagnoseType">诊断类别</param>
        /// <param name="icdCode">ICD编码</param>
        /// <param name="icdName">ICD名称</param>
        /// <param name="hosDiagnoseCode">目前HIS没用，不知道干什么用的</param>
        /// <param name="hosDiagnoseName">目前HIS没用，不知道干什么用的</param>
        /// <param name="mainFlag">主诊断标记</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="isDubDiagFlag">是否疑诊</param>
        /// <param name="zgCode">治疗情况 0 治愈1 好转 2 未愈3 死亡 4 其他</param>
        /// <param name="errText">错误信息</param>
        /// <returns></returns>
        public int SaveInpatientDiagnose(string inpatientNO, string deptCode, string deptName,
            string docCode, string docName, string emrDiagnoseKey, string diagnoseType,
            string icdCode, string icdName, string hosDiagnoseCode, string hosDiagnoseName, bool mainFlag, bool isValid,
            bool isDubDiagFlag,string zgCode,
            out string errText)
        {
            FS.HISFC.Models.RADT.PatientInfo pInfo = null;

            FS.HISFC.Models.HealthRecord.Diagnose diagNose = null;
            try
            {
                errText = "";

                //FS.FrameWork.WinForms.Classes.Function.ShowBalloonTip(2, "诊断类型", diagnoseType, System.Windows.Forms.ToolTipIcon.Info);
                pInfo = interRadt.QueryPatientInfoByInpatientNO(inpatientNO);

                if (pInfo == null)
                {
                    errText = interRadt.Err;
                    return -1;
                }

                diagNose = new FS.HISFC.Models.HealthRecord.Diagnose();

                diagNose.DiagInfo.HappenNo = FS.FrameWork.Function.NConvert.ToInt32(emrDiagnoseKey);

                diagNose.DiagInfo.ICD10.ID = icdCode;
                diagNose.DiagInfo.ICD10.Name = icdName;
                diagNose.DiagInfo.Patient.ID = inpatientNO;
                diagNose.DiagInfo.DiagType.ID = diagnoseType;//诊断类型
                diagNose.DiagInfo.DiagDate = diagMgr.GetDateTimeFromSysDateTime();
                diagNose.DiagInfo.Doctor.ID = docCode;
                diagNose.DiagInfo.Doctor.Name = docName;

                diagNose.Pvisit.InTime = pInfo.PVisit.InTime;//入院日期 
                diagNose.Pvisit.OutTime = pInfo.PVisit.OutTime;//出院日期 
                diagNose.DiagOutState = zgCode;//pInfo.PVisit.ZG.ID;//治疗情况 0 治愈1 好转 2 未愈3 死亡 4 其他
                diagNose.SecondICD = "";//第二ICD    
                diagNose.SynDromeID = "";// 并发症类别   
                diagNose.CLPA = "";//病理符合
                diagNose.DubDiagFlag = isDubDiagFlag ? "1" : "0";//是否疑诊     
                diagNose.DiagInfo.IsMain = mainFlag;
                //diagNose.Memo;//备注  
                diagNose.ID = diagMgr.Operator.ID;//操作员
                diagNose.OperType = "1";//类别 1 医生站录入诊断  2 病案室录入诊断
                //diagNose.OperationFlag;// 手术标志 1 有手术 0 没有手术  
                //diagNose.Is30Disease; //是否是30种疾病
                diagNose.PerssonType = FS.HISFC.Models.Base.ServiceTypes.I; //患者类别 0 门诊患者 1 住院患者
                diagNose.IsValid = isValid; //是否有效
                //diagNose.Diagnosis_flag;   //是否初诊

                // 先写入，不成功则更新。
                int param = diagMgr.InsertCasDiagnose(diagNose);
                if (param == -1)
                {
                    diagNose.IsValid = isValid;
                    param = diagMgr.UpdateDiagnose(diagNose);
                    if (param == -1)
                    {
                        errText = diagMgr.Err;
                        return -1;
                    }
                }

                #region hl7消息发送
                //FS.SOC.HISFC.BizProcess.MessagePatternInterface.IOrder IOrder = InterfaceManager.GetIOrder();
                //if (IOrder != null)
                //{
                //    diagNose.DiagInfo.Patient = pInfo.Clone();
                //    int retrunValue = IOrder.SendDiagInfo(pInfo, diagNose, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType.Add, true);
                //    return retrunValue;
                //}
                #endregion


                return 1;
            }
            catch (Exception ex)
            {
                errText = ex.Message;
                return -1;
            }
        }

        /// <summary>
        /// 删除住院诊断
        /// </summary>
        /// <param name="inpatientNO">住院流水号</param>
        /// <param name="deptCode">科室编码</param>
        /// <param name="deptName">科室名称</param>
        /// <param name="docCode">医生编码</param>
        /// <param name="docName">医生名称</param>
        /// <param name="emrDiagnoseKey">EMR诊断主键（HIS里面是发生序号，如，第一个诊断就是1，第二个诊断就是2）</param>
        /// <param name="diagnoseType">诊断类别</param>
        /// <param name="icdCode">ICD编码</param>
        /// <param name="icdName">ICD名称</param>
        /// <param name="hosDiagnoseCode">目前HIS没用，不知道干什么用的</param>
        /// <param name="hosDiagnoseName">目前HIS没用，不知道干什么用的</param>
        /// <param name="mainFlag">主诊断标记</param>
        /// <param name="errText">错误信息</param>
        /// <returns></returns>
        public int DeleteInpatientDiagnose(string inpatientNO, string deptCode, string deptName, string docCode, string docName, string emrDiagnoseKey, string diagnoseType, string icdCode, string icdName, string hosDiagnoseCode, string hosDiagnoseName, bool mainFlag, out string errText)
        {
            errText = "";
            //int rev = diagMgr.DeleteDiagnoseSingle(inpatientNO, FS.FrameWork.Function.NConvert.ToInt32(emrDiagnoseKey));
            int rev = diagMgr.DeleteDiagnoseSingle(inpatientNO,
                 FS.FrameWork.Function.NConvert.ToInt32(emrDiagnoseKey),
                  FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC,
                   FS.HISFC.Models.Base.ServiceTypes.I);
            if (rev == -1)
            {
                errText = diagMgr.Err;
                return -1;
            }

            #region hl7消息发送
            //FS.SOC.HISFC.BizProcess.MessagePatternInterface.IOrder IOrder = InterfaceManager.GetIOrder();
            //if (IOrder != null)
            //{
            //    FS.HISFC.Models.RADT.PatientInfo pInfo = interRadt.GetPatientInfoByPatientNO(inpatientNO);

            //    FS.HISFC.Models.HealthRecord.Diagnose diagNose = this.diagMgr.GetCaseDiagnoseByEmrId(inpatientNO, emrDiagnoseKey);
            //    if (pInfo != null && diagNose != null)
            //    {
            //        diagNose.DiagInfo.Patient = pInfo.Clone();

            //        int retrunValue = IOrder.SendDiagInfo(pInfo, diagNose, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType.Delete, false);
            //        return retrunValue;
            //    }
            //}
            #endregion

            return 1;
        }

        /// <summary>
        /// 诊断转归--更新诊断转归
        /// </summary>
        /// <param name="emrPaitentId">住院流水号</param>
        /// <param name="diagId">EMR诊断主键</param>
        /// <param name="zgCode">转归代码</param>
        /// <param name="errText">异常信息</param>
        /// <returns>正常返回1,异常返回-1 以及错误信息</returns>
        public int UpdateInpatientDiagnoseZG(string inpatientNO, long emrDiagnoseKey, string zgCode, out string errText)
        {
            errText = string.Empty;
            var now = this.diagMgr.GetDateTimeFromSysDateTime();
            var rev = this.diagMgr.UpdateCaseDiagnoseZGForEmr(inpatientNO, emrDiagnoseKey.ToString(), zgCode, this.diagMgr.Operator.ID, now);
            if (rev == -1)
            {
                errText = this.diagMgr.Err;

                return -1;
            }

            // hl7消息发送
            //FS.SOC.HISFC.BizProcess.MessagePatternInterface.IOrder IOrder = InterfaceManager.GetIOrder();
            //if (IOrder != null)
            //{
            //    FS.HISFC.Models.RADT.PatientInfo pInfo = interRadt.GetPatientInfoByPatientNO(inpatientNO);
            //    var diagnose = diagMgr.GetCaseDiagnoseByEmrId(inpatientNO, emrDiagnoseKey.ToString());
            //    if (diagnose != null && pInfo != null)
            //    {
            //        diagnose.DiagInfo.Patient = pInfo.Clone();

            //        int retrunValue = IOrder.SendDiagInfo(pInfo, diagnose, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType.Modify, true);
            //        return retrunValue;
            //    }
            //}

            return 1;
        }

        #endregion

        public enum OperType
        {
            Insert,
            Update,
            Delete
        }
    }
}
