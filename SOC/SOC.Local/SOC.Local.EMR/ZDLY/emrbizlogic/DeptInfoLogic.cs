using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Neusoft.SOC.Local.EMR.ZDLY.emrbizlogic
{
    class DeptInfoLogic : Neusoft.FrameWork.Management.Database
    {
        string strExceptions = "";

        #region 或取有效科室
        /// <summary>
        /// 或取有效科室
        /// </summary>
        /// <returns></returns>
        public DataTable GetDept()
        {
            try
            {
                DataSet ds = new DataSet();
                string strSql =@"select a.id,
                           a.dept_code      code,
                           a.dept_name      name,
                           a.spell_code     py,
                           a.wb_code        wb,
                           a.dept_type_code type                         
                      from org_department a
                     where a.valid_state = 1";
                strExceptions = strSql;
                this.ExecQuery(strSql, ref ds);
                if (ds.Tables.Count > 0)
                {
                    return ds.Tables[0];
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Neusoft.SOC.Local.EMR.ZDLY.Class.GetEmrLog.WriteException(ex.Message + " \n " + strExceptions);
                return null;
            }
        }
        #endregion
    }
}
