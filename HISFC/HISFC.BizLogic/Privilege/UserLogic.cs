using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using FS.HISFC.BizLogic.Privilege.Model;

namespace FS.HISFC.BizLogic.Privilege
{
    /// <summary>
    /// �û�����������
    /// </summary>
    public class UserLogic : DataBase
    {
        
        #region UserDAL ��Ա

        /// <summary>
        /// �����ɫ��Ϣ
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int Insert(User user)
        {
            //using (DaoManager _dao = new DaoManager())
            //{
            //    AbstractSqlModel _sql = new SqlModel("Security.Org.User.Insert");
            //    _sql["userid"] = user.Id;
            //    _sql["username"] = user.Name;
            //    _sql["account"] = user.Account;
            //    _sql["password"] = user.Password;
            //    _sql["appid"] = user.AppId;
            //    _sql["personid"] = user.PersonId;
            //    _sql["description"] = user.Description;
            //    _sql["islock"] = user.IsLock;
            //    _sql["operid"] = user.UserId;
            //    _sql["operdate"] = user.OperDate;
            //    DbCommand _command = _dao.DataConnection.CreateTextCommand();
            //    SqlMapping _mapping = new FS.Framework.DataAccess.SqlMapping.SqlMapping(_dao, _sql);
            //    _mapping.Mapper(_command);
            //    return _command.ExecuteNonQuery();
            //}

            string[] args = new string[] { 
                user.Id,
                user.Name,
                user.Account,
                user.Password,
                user.AppId,
                user.PersonId,
                user.Description,
                FrameWork.Function.NConvert.ToInt32( user.IsLock).ToString(),
                user.operId,
                user.OperDate.ToString(),
                //{46A2B736-8740-405a-8B0A-6DDF1B705B8D}
                FS.FrameWork.Function.NConvert.ToInt32( user.IsManager).ToString()
                };
            string sql = "";
            if (this.GetSQL("SECURITY.ORG.USER.INSERT", ref sql) == -1) return -1;
            try
            {
                sql = string.Format(sql, args);
            }
            catch (Exception ex) { this.Err = ex.Message; return -1; }
            if (this.ExecNoQuery(sql) <= 0) return -1;
            return 0;
        }
               
        /// <summary>
        /// ɾ���û���Ϣ
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int Delete(string userId)
        {
            //using (DaoManager _dao = new DaoManager())
            //{
            //    AbstractSqlModel _sql = new SqlModel("Security.Org.User.Delete");
            //    _sql["userid"] = userId;
            //    DbCommand _command = _dao.DataConnection.CreateTextCommand();
            //    SqlMapping _mapping = new FS.Framework.DataAccess.SqlMapping.SqlMapping(_dao, _sql);
            //    _mapping.Mapper(_command);
            //    return _command.ExecuteNonQuery();
            //}
            string sql = "";
            if (this.GetSQL("SECURITY.ORG.USER.DELETE", ref sql) == -1) return -1;
            try
            {
                sql = string.Format(sql, userId);
            }
            catch (Exception ex) { this.Err = ex.Message; return -1; }
            if (this.ExecNoQuery(sql) <= 0) return -1;
            return 0;

        }

        /// <summary>
        /// ��ѯ�û��б�
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        public List<User> QueryUsers(List<String> users)
        {
            List<User> userList = new List<User>();
            foreach (String user in users)
            {
                //�ų����û�
                if (Get(user).Id != null)
                {
                    userList.Add(Get(user));
                }
            }
            return userList;
        }

        /// <summary>
        /// �����û�Id��ȡ�û���ϸ��Ϣ���ſ�����
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public User Get(string userId)
        {
            User _user = new User();

            //using (DaoManager _dao = new DaoManager())
            //{
            //    AbstractSqlModel _sql = new SqlModel("Security.Org.User.GetByUserId");
            //    _sql["userid"] = userId;
            //    DbDataReader _reader = this.ExecuteReader(_sql);
            //}

            string sql = "";
            if (this.GetSQL("SECURITY.ORG.USER.GETBYUSERID", ref sql) == -1) return null;
            try
            {
                sql = string.Format(sql, userId);
            }
            catch (Exception ex) { this.Err = ex.Message; return null; }
            if (this.ExecQuery(sql) < 0) return null;
            while (this.Reader.Read())
            {
                _user = new User();
                _user.Id = Reader[0].ToString();
                _user.Name = Reader[1].ToString();
                _user.Account = Reader[2].ToString();
                _user.Password = Reader[3].ToString();
                _user.AppId = Reader[4].ToString();
                _user.PersonId = Reader[5].ToString();
                _user.Description = Reader[6].ToString();
                _user.IsLock = FrameWork.Function.NConvert.ToBoolean(Reader[7]);
                _user.operId = Reader[8].ToString();
                if (!Reader.IsDBNull(9))
                    _user.OperDate = FrameWork.Function.NConvert.ToDateTime(Reader[9].ToString());
                //{46A2B736-8740-405a-8B0A-6DDF1B705B8D}
                if (!Reader.IsDBNull(10))
                    _user.IsManager = FrameWork.Function.NConvert.ToBoolean(Reader[10].ToString());
  
            }
                Reader.Close();
            return _user;   
        }

        /// <summary>
        /// ��ѯ�û�
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public User Get(string account, string password)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        /// <summary>
        /// ����ʺ�
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public User GetByAccount(string account)
        {
            User _user = null;

            //using (DaoManager _dao = new DaoManager())
            //{
            //    AbstractSqlModel _sql = new SqlModel("Security.Org.GetByAccount");
            //    DbCommand _command = _dao.DataConnection.CreateTextCommand();
            //    _sql["Account"] = account;
            //    SqlMapping _map = new FS.Framework.DataAccess.SqlMapping.SqlMapping(_dao, _sql);
            //    _map.Mapper(_command);
            //    DbDataReader _reader = _command.ExecuteReader();
            //}

            string sql = "";
            if (this.GetSQL("SECURITY.ORG.GETBYACCOUNT", ref sql) == -1) return null;
            try
            {
                sql = string.Format(sql, account);
            }
            catch (Exception ex) { this.Err = ex.Message; return null; }
            if (this.ExecQuery(sql) <0) return null;
                while (this.Reader.Read())
                {
                    _user = new User();
                    _user.Id = Reader[0].ToString();
                    _user.Name = Reader[1].ToString();
                    _user.Account = Reader[2].ToString();
                    _user.Password = Reader[3].ToString();
                    _user.AppId = Reader[4].ToString();
                    _user.PersonId = Reader[5].ToString();
                    _user.Description = Reader[6].ToString();
                    _user.IsLock = FrameWork.Function.NConvert.ToBoolean(Reader[7]);
                    _user.operId = Reader[8].ToString();
                    if (!Reader.IsDBNull(9))
                        _user.OperDate = FrameWork.Function.NConvert.ToDateTime(Reader[9].ToString());
                    //{46A2B736-8740-405a-8B0A-6DDF1B705B8D}
                    if (!Reader.IsDBNull(10))
                        _user.IsManager = FrameWork.Function.NConvert.ToBoolean(Reader[10].ToString());
  
                }

                Reader.Close();
            
            return _user;            
        }

        /// <summary>
        /// ��ѯ�û�
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="appId"></param>
        /// <returns></returns>
        public User GetByPsnID(string personId, string appId)
        {
            User _user = null;

            //using (DaoManager _dao = new DaoManager())
            //{
            //    AbstractSqlModel _sql = new SqlModel("Security.Org.User.GetByPsnID");
            //    DbCommand _command = _dao.DataConnection.CreateTextCommand();
            //    _sql["personid"] = personId;
            //    _sql["appid"] = appId;
            //    SqlMapping _map = new FS.Framework.DataAccess.SqlMapping.SqlMapping(_dao, _sql);
            //    _map.Mapper(_command);
            //    DbDataReader _reader = _command.ExecuteReader();
            //}

            string sql = "";
            if (this.GetSQL("SECURITY.ORG.USER.GETBYPSNID", ref sql) == -1) return null;
            try
            {
                sql = string.Format(sql,personId,appId);
            }
            catch (Exception ex) { this.Err = ex.Message; return null; }
            if (this.ExecQuery(sql) < 0) return null;
                while (this.Reader.Read())
                {
                    _user = new User();
                    _user.Id = Reader[0].ToString();
                    _user.Name = Reader[1].ToString();
                    _user.Account = Reader[2].ToString();
                    _user.Password = Reader[3].ToString();
                    _user.AppId = Reader[4].ToString();
                    _user.PersonId = Reader[5].ToString();
                    _user.Description = Reader[6].ToString();
                    _user.IsLock = FrameWork.Function.NConvert.ToBoolean(Reader[7]);
                    _user.operId = Reader[8].ToString();
                    if (!Reader.IsDBNull(9))
                        _user.OperDate = FrameWork.Function.NConvert.ToDateTime(Reader[9].ToString());
                    //{46A2B736-8740-405a-8B0A-6DDF1B705B8D}
                    if (!Reader.IsDBNull(10))
                        _user.IsManager = FrameWork.Function.NConvert.ToBoolean(Reader[10].ToString());
  
                }

                Reader.Close();

            return _user;   
        }

        /// <summary>
        /// �����û�
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int Update(User user)
        {
            //using (DaoManager _dao = new DaoManager())
            //{
            //    AbstractSqlModel _sql = new SqlModel("Security.Org.User.Update");
            //    _sql["userid"] = user.Id;
            //    _sql["username"] = user.Name;
            //    _sql["account"] = user.Account;
            //    _sql["password"] = user.Password;
            //    _sql["appid"] = user.AppId;
            //    _sql["personid"] = user.PersonId;
            //    _sql["description"] = user.Description;
            //    _sql["islock"] = user.IsLock;
            //    _sql["operid"] = user.UserId;
            //    _sql["operdate"] = user.OperDate;
            //    DbCommand _command = _dao.DataConnection.CreateTextCommand();
            //    SqlMapping _mapping = new FS.Framework.DataAccess.SqlMapping.SqlMapping(_dao, _sql);
            //    _mapping.Mapper(_command);
            //    return _command.ExecuteNonQuery();
            //}

            string[] args = new string[] { 
                user.Id,
                user.Name,
                user.Account,
                user.Password,
                user.AppId,
                user.PersonId,
                user.Description,
                FrameWork.Function.NConvert.ToInt32(user.IsLock).ToString(),
                user.operId,
                user.OperDate.ToString(),
                //{46A2B736-8740-405a-8B0A-6DDF1B705B8D}
                FS.FrameWork.Function.NConvert.ToInt32(user.IsManager).ToString()
                };
            string sql = "";
            if (this.GetSQL("SECURITY.ORG.USER.UPDATE", ref sql) == -1) return -1;
            try
            {
                sql = string.Format(sql, args);
            }
            catch (Exception ex) { this.Err = ex.Message; return -1; }
            if (this.ExecNoQuery(sql) <= 0) return -1;
            return 0;

        }

        /// <summary>
        /// ��ѯ�û��б�
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public IList<User> Query(string roleId)
        {
            User _user = null;
            IList<User> _list = new List<User>();

            //using (DaoManager _dao = new DaoManager())
            //{
            //    AbstractSqlModel _sql = new SqlModel("Security.Org.User.GetByRoleID");
            //    DbCommand _command = _dao.DataConnection.CreateTextCommand();
            //    _sql["roleid"] = roleId;

            //    SqlMapping _map = new FS.Framework.DataAccess.SqlMapping.SqlMapping(_dao, _sql);
            //    _map.Mapper(_command);
            //    DbDataReader _reader = _command.ExecuteReader();
            //}
            string sql = "";
            if (this.GetSQL("SECURITY.ORG.USER.GETBYROLEID", ref sql) == -1) return null;
            try
            {
                sql = string.Format(sql, roleId,FS.FrameWork.Management.Connection.Hospital.ID);
            }
            catch (Exception ex) { this.Err = ex.Message; return null; }
            if (this.ExecQuery(sql) < 0) return null;
                while (this.Reader.Read())
                {
                    _user = new User();
                    _user.Id = Reader[0].ToString();
                    _user.Name = Reader[1].ToString();
                    _user.Account = Reader[2].ToString();
                    _user.Password = Reader[3].ToString();
                    _user.AppId = Reader[4].ToString();
                    _user.PersonId = Reader[5].ToString();
                    _user.Description = Reader[6].ToString();
                    _user.IsLock = FrameWork.Function.NConvert.ToBoolean(Reader[7]);
                    _user.operId = Reader[8].ToString();
                    if (!Reader.IsDBNull(9))
                        _user.OperDate = FrameWork.Function.NConvert.ToDateTime(Reader[9].ToString());
                    //{46A2B736-8740-405a-8B0A-6DDF1B705B8D}
                    if (!Reader.IsDBNull(10))
                        _user.IsManager = FrameWork.Function.NConvert.ToBoolean(Reader[10].ToString());
  

                    _list.Add(_user);
                }

                Reader.Close();


            return _list;            
        }

        /// <summary>
        /// ��ѯ�û��б�
        /// </summary>
        /// <returns></returns>
        public List<User> Query()
        {
            User _user = null;
            List<User> _list = new List<User>();

            //using (DaoManager _dao = new DaoManager())
            //{
            //    AbstractSqlModel _sql = new SqlModel("Security.User.QueryAll");
            //    DbCommand _command = _dao.DataConnection.CreateTextCommand();
            //    _command.CommandText = _sql.Sql;                
            //    DbDataReader _reader = _command.ExecuteReader();
            //}
            string sql = "";
            if (this.GetSQL("SECURITY.USER.QUERYALL", ref sql) == -1) return null;
            try
            {
                sql = string.Format(sql);
            }
            catch (Exception ex) { this.Err = ex.Message; return null; }
            if (this.ExecQuery(sql) < 0) return null;
                while (this.Reader.Read())
                {
                    _user = new User();
                    _user.Id = Reader[0].ToString();
                    _user.Name = Reader[1].ToString();
                    _user.Account = Reader[2].ToString();
                    _user.Password = Reader[3].ToString();
                    _user.AppId = Reader[4].ToString();
                    _user.PersonId = Reader[5].ToString();
                    _user.Description = Reader[6].ToString();
                    _user.IsLock = FrameWork.Function.NConvert.ToBoolean(Reader[7]);
                    _user.operId = Reader[8].ToString();
                    if (!Reader.IsDBNull(9))
                        _user.OperDate = FrameWork.Function.NConvert.ToDateTime(Reader[9].ToString());
                    //{46A2B736-8740-405a-8B0A-6DDF1B705B8D}
                    if (!Reader.IsDBNull(10))
                        _user.IsManager = FrameWork.Function.NConvert.ToBoolean(Reader[10].ToString());
  

                    _list.Add(_user);
                }

                Reader.Close();
            

            return _list;            

        }
        #endregion
    }
}
