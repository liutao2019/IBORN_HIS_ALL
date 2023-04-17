using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.HISFC.Components.HealthRecord.UploadGuangDong
{
    public class LocalManager : FS.HISFC.BizLogic.Manager.DataBase
    {
        /// <summary>
        /// 获取病案数据
        /// </summary>
        /// <param name="isUpOnlyCaseInfo"></param>
        /// <param name="sendFlag"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="patientNos"></param>
        /// <returns></returns>
        public ArrayList GetUploadPatient(bool isUpOnlyCaseInfo, string sendFlag, DateTime beginTime, DateTime endTime, string patientNos)
        {
            string sqlID = string.Empty;
            string strSQL = string.Empty;

            if (!string.IsNullOrEmpty(patientNos))
            {
                strSQL = @"select t.inpatient_no,t.card_no,t.in_times
  from fin_ipr_inmaininfo t
 where t.patient_no in '({0})'";

                string[] temp = patientNos.Split(',');
                string strPaientNo = "'',";
                for (int i = 0; i < temp.Length; i++)
                {
                    strPaientNo += temp[i];
                }

                strSQL = string.Format(strSQL, strPaientNo);
            }
            else
            {
                if (isUpOnlyCaseInfo)
                {
                    sqlID = "Case.CaseInfo.UpLoad.MetCasBase";
                }
                else
                {
                    sqlID = "Case.CaseInfo.UpLoad";
                }


                if (this.Sql.GetSql(sqlID, ref strSQL) == -1)
                {
                    this.Err = "获取SQL语句出错[" + sqlID + "]";
                    return null;
                }

                strSQL = string.Format(strSQL, sendFlag, beginTime.ToString(), endTime.ToString());
            }

            this.ExecQuery(strSQL);

            ArrayList al = new ArrayList();
            FS.HISFC.Models.RADT.PatientInfo patientInfo = null;

            try
            {
                while (this.Reader.Read())
                {
                    patientInfo = new FS.HISFC.Models.RADT.PatientInfo();

                    patientInfo.ID = this.Reader[0].ToString();
                    patientInfo.PID.PatientNO = this.Reader[1].ToString();
                    patientInfo.InTimes = FS.FrameWork.Function.NConvert.ToInt32(Reader[2]);

                    al.Add(patientInfo);
                }
                return al;
            }
            catch (Exception ex)
            {
                this.Err += ex.Message;
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
        }
    }
}
