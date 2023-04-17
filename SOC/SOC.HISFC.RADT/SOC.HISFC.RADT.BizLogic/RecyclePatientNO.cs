using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.RADT.BizLogic
{
    /// <summary>
    /// [功能描述: 住院回收住院号相关的业务类]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2012-03]<br></br>
    /// <修改记录>
    /// </修改记录>
    /// </summary>
    public class RecyclePatientNO:FS.FrameWork.Management.Database
    {
        /// <summary>
        /// 更新旧住院号的状态
        /// </summary>
        /// <param name="oldPatientNO"></param>
        /// <returns></returns>
        public int UpdateState(string oldPatientNO)
        {
            return this.UpdateSingleTable("RADT.InPatient.UpdatePateintNoState", oldPatientNO, this.Operator.ID);
        }

        /// <summary>
        /// 插入回收表
        /// </summary>
        /// <param name="newPatientNO"></param>
        /// <param name="oldPatientNo"></param>
        /// <returns></returns>
        public int Insert(string newPatientNO, string oldPatientNo)
        {
            return this.UpdateSingleTable("RADT.InPatient.SetPatientNoShift", newPatientNO, oldPatientNo, this.Operator.ID);
        }

        /// <summary>
        /// 获取还没有用过的住院号
        /// </summary>
        /// <param name="patientNO">未用住院号</param>
        /// <param name="usedPatientNo">已用住院号</param>
        /// <returns>true,取到；false 没有未使用的住院号</returns>
        public bool GetNoUsedPatientNO(ref string patientNO, ref string usedPatientNo)
        {
            string strSql = string.Empty;
            bool rtn = false;
            try
            {
                if (Sql.GetSql("RADT.InPatient.GetNoUsedPatientNo", ref strSql) == -1)
                {
                    return false;
                }

                if (ExecQuery(strSql) == -1)
                {
                    return false;
                }
                while (Reader.Read())
                {
                    rtn = true;
                    usedPatientNo = Reader[0].ToString();
                    patientNO = Reader[1].ToString();
                }
                return rtn;
            }
            catch (Exception ex)
            {
                Err = ex.Message;
                WriteErr();
                return false;
            }
            finally
            {
                if (this.Reader != null && !this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
            }

        }
    }
}
