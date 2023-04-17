using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.SOC.HISFC.Components.Project
{
    public class AccountManager : FrameWork.Management.DataBaseManger
    {
        private string GetInsertSQL()
        {
            string curInsertSQL = "";
            if (this.Sql.GetSql("SOC.System.Project.Account.Insert", ref curInsertSQL) == -1)
            {
                curInsertSQL = @"
                                insert into SOC_PROJECT_ACCOUNT
                                (
                                       id,
                                       password,
                                       e_mail,
                                       role_id,
                                       state,
                                       grant_accout,
                                       set_time,
                                       grant_time,
                                       stop_time,
                                       address,
                                       telephone
                                  )
                                values
                                (
                                      '{0}',--id
                                      '{1}',--password
                                      '{2}', --e_mail
                                      '{3}',--role_id
                                      '{4}',--state
                                      '{5}',--grant_accout
                                      to_date('{6}','yyyy-mm-dd hh24:mi:ss'),--set_time
                                      to_date('{7}','yyyy-mm-dd hh24:mi:ss'),--grant_time
                                      to_date('{8}','yyyy-mm-dd hh24:mi:ss'),--stop_time
                                      '{9}',--address
                                      '{10}'--telephone
                                )
        ";
                this.Sql.CacheSql("SOC.System.Project.Account.Insert", curInsertSQL);
            }
            return curInsertSQL;
        }

        private string GetSelectSQL()
        {
            string curSelectSQL = "";
            if (this.Sql.GetSql("SOC.System.Project.Account.Select", ref curSelectSQL) == -1)
            {
                curSelectSQL = @"select id,
                                       password,
                                       e_mail,
                                       role_id,
                                       state,
                                       grant_accout,
                                       set_time,
                                       grant_time,
                                       stop_time,
                                       address,
                                       telephone
                                  from SOC_PROJECT_ACCOUNT
         ";

                this.Sql.CacheSql("SOC.System.Project.Account.Select", curSelectSQL);
            }
            return curSelectSQL;
        }

        private string[] GetParam(AccountInfo accountInfo)
        {
            string[] param = new string[] 
            { 
                accountInfo.AccountID,
                accountInfo.Password,
                accountInfo.EMail,
                accountInfo.RoleID,
                accountInfo.State,
                accountInfo.GrantAccout,
                accountInfo.SetTime.ToString(),
                accountInfo.GrantTime.ToString(),
                accountInfo.StopTime.ToString(),
                accountInfo.Address,
                accountInfo.Telephone
            };

            return param;
        }

        public int InsertAccount(AccountInfo accountInfo)
        {
            string curSQL = string.Format(this.GetInsertSQL(), this.GetParam(accountInfo));
            return this.ExecNoQuery(curSQL);
        }

        private ArrayList QueryAccountInfoList(string SQL)
        {
            if (this.ExecQuery(SQL) == -1)
            {
                return null;
            }
            ArrayList alAccountInfo = new ArrayList();
            try
            {
                while (this.Reader.Read())
                {
                    AccountInfo accountInfo = new AccountInfo();
                    accountInfo.AccountID = this.Reader[0].ToString();
                    accountInfo.Password = this.Reader[1].ToString();
                    accountInfo.EMail = this.Reader[2].ToString();
                    accountInfo.RoleID = this.Reader[3].ToString();
                    accountInfo.State = this.Reader[4].ToString();
                    accountInfo.GrantAccout = this.Reader[5].ToString();
                    accountInfo.SetTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[6]);
                    accountInfo.GrantTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[7]);
                    accountInfo.StopTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[8]);
                    accountInfo.Address = this.Reader[9].ToString();
                    accountInfo.Telephone = this.Reader[10].ToString();

                    alAccountInfo.Add(accountInfo);
                }
            }
            catch(Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = "-1";
                this.WriteErr();
                return alAccountInfo;
            }

            return alAccountInfo;
        }

        public AccountInfo GetAccountInfo(string ID)
        {
            string curSQL = this.GetSelectSQL();
            curSQL += "\nwhere id='{0}'";
            curSQL = string.Format(curSQL, ID);
            ArrayList al = this.QueryAccountInfoList(curSQL);
            if (al != null && al.Count> 0)
            {
                return al[0] as AccountInfo;
            }
            return null;
        }
    }
}
