using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.Local.RADT.GuangZhou.ZDLY
{
    /// <summary>
    /// 中大六院出院登记
    /// [住院登记自动带出诊断]
    /// </summary>
    public class ZDLYIPatientOut : FS.HISFC.BizProcess.Interface.RADT.IPatientOut
    {

        #region 变量

        /// <summary>
        /// 电子病历住院业务处理
        /// </summary>
        FS.SOC.HISFC.BizLogic.EmrNew.EMRInpatinetLogic emrLogicManager = new FS.SOC.HISFC.BizLogic.EmrNew.EMRInpatinetLogic();

        /// <summary>
        /// 错误
        /// </summary>
        private string errInfo = string.Empty;

        /// <summary>
        /// 出院诊断
        /// </summary>
        string outSql = @" SELECT VALUE
                        FROM RCD_RECORD_ITEM
                        WHERE ELEMENT_ID = '249'
                        AND INPATIENT_ID = '{0}'
                         AND INPATIENT_RECORD = (SELECT ID
                         FROM RCD_INPATIENT_RECORD RR
                         WHERE RR.INPATIENT_RECORD_SET_ID =
                          (SELECT RS.ID
                         FROM RCD_INPATIENT_RECORD_SET RS
                             WHERE RS.INPATIENT_ID = '{0}')
                         AND RECORD_CHILD_TYPE = 'Out_Record'
                         AND RR.NAME LIKE '%出院记录%') ";

        //入院诊断
        // INPATIENT_ID, ELEMENT_ID, INPATIENT_RECORD, VALUE
        string inSql = @" SELECT VALUE
                        FROM RCD_RECORD_ITEM
                        WHERE ELEMENT_ID = '357'
                        AND INPATIENT_ID = '{0}'
                         AND INPATIENT_RECORD = (SELECT ID
                         FROM RCD_INPATIENT_RECORD RR
                         WHERE RR.INPATIENT_RECORD_SET_ID =
                          (SELECT RS.ID
                         FROM RCD_INPATIENT_RECORD_SET RS
                             WHERE RS.INPATIENT_ID = '{0}')
                         AND RECORD_CHILD_TYPE = 'Out_Record'
                         AND RR.NAME LIKE '%出院记录%') ";
        #endregion



        #region IPatientOut 成员

        public int AfterPatientOut(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.FrameWork.Models.NeuObject oper)
        {
            return 1;
        }

        public int BeforePatientOut(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.FrameWork.Models.NeuObject oper)
        {
            #region 出院登记 带出出院诊断

            string returnCode = string.Empty;
            if (string.IsNullOrEmpty(patientInfo.MainDiagnose))
            {
                returnCode = this.emrLogicManager.ExecSqlReturnOne(string.Format(outSql, patientInfo.ID), "-1");
                if (returnCode == "0" || returnCode == "-1")
                {
                    returnCode = emrLogicManager.ExecSqlReturnOne(string.Format(inSql, patientInfo.ID), "-1");
                }

                if (returnCode != "0" && returnCode != "-1")
                {
                    System.Text.RegularExpressions.Match match = System.Text.RegularExpressions.Regex.Match(returnCode, "([^\\u4e00-\\u9fa5]*)([\\u4e00-\\u9fa5]*)", System.Text.RegularExpressions.RegexOptions.None);
                    if (match.Groups.Count > 2)
                    {
                        patientInfo.MainDiagnose = match.Groups[2].Value;
                    }
                    else
                    {
                        patientInfo.MainDiagnose = returnCode;
                    }
                }

            }

            #endregion

            return 1;
        }

        public string ErrInfo
        {
            get
            {
                return this.errInfo;
            }
            set
            {
                this.errInfo = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="oper"></param>
        /// <returns></returns>
        public int OnPatientOut(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.FrameWork.Models.NeuObject oper)
        {
            //这个是过程中调用的， 可以包含事务处理！！！

            #region 出院登记 处理电子病历信息

            long emrPatID = -1;

            int iEmrReturn = emrLogicManager.GetPatientId(patientInfo.ID, ref emrPatID);

            if (iEmrReturn < 0)
            {
                errInfo = "调用EMR接口GetPatientId失败！" + emrLogicManager.Err;
                return -1;
            }

            iEmrReturn = emrLogicManager.OutPTDoc(emrPatID);
            if (iEmrReturn < 0)
            {
                errInfo = "调用EMR接口OutPTDoc失败！" + emrLogicManager.Err;
                return -1;
            }

            #endregion

            return 1;
        }

        #endregion
    }
}
