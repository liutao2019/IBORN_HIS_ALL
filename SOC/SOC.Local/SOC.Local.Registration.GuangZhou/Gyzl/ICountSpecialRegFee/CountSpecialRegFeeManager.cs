using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.SOC.Local.Registration.GuangZhou.Gyzl.ICountSpecialRegFee
{
    public class CountSpecialRegFeeManager : FS.FrameWork.Management.Database
    {
        public CountSpecialRegFeeManager()
        {
        }

        public bool VailRetailed(string name, string idenno)
        {
            string sql = string.Empty;
            int result = this.Sql.GetCommonSql("", ref sql);
            if (result == -1)
            {
                sql = @"select name
                          from com_patientinfo_retired
                         where name = '{0}'
                           and idenno = '{1}'";
            }
            sql = string.Format(sql, name, idenno);
            if (this.ExecQuery(sql) == -1)
            {
                return false;
            }
            else
            {
                try
                {
                    if (this.Reader.Read())
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }
    }
}
