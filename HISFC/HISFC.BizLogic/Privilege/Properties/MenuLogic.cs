using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using FS.HISFC.BizLogic.Privilege.Model;


namespace FS.HISFC.BizLogic.Privilege
{
    /// <summary>
    /// �˵�����ʽӿ�
    /// </summary>
    public class MenuLogic : DataBase
    {
        #region IMenuDAL ��Ա

        /// <summary>
        /// ����һ���˵���
        /// </summary>
        /// <param name="menuItem"></param>
        /// <returns></returns>
        public int Insert(MenuItem menuItem)
        {

            //AbstractSqlModel _sql = new SqlModel("Security.Menu.Insert");
            //_sql["menuid"] = menuItem.Id;
            //_sql["menuname"] = menuItem.Name;
            //_sql["parentid"] = menuItem.ParentId;
            //_sql["layer"] = menuItem.Layer;
            //_sql["shortcut"] = menuItem.Shortcut;
            //_sql["icon"] = menuItem.Icon;
            //_sql["tooltip"] = menuItem.Tooltip;
            //_sql["param"] = menuItem.Param;
            //_sql["dllname"] = menuItem.DllName;
            //_sql["winname"] = menuItem.WinName;
            //_sql["controltype"] = menuItem.ControlType;
            //_sql["showtype"] = menuItem.ShowType;
            //_sql["isenabled"] = menuItem.Enabled;
            //_sql["description"] = "";
            //_sql["operid"] = menuItem.UserId;
            //_sql["operdate"] = menuItem.OperDate;
            //_sql["sortid"] = menuItem.Order;
            //_sql["treedllname"] = menuItem.TreeDllName;
            //_sql["treename"] = menuItem.TreeName;
            //return base.ExecuteNonQuery(_sql);

            string[] args = new string[]{
                menuItem.Id,
                menuItem.Name,
                menuItem.ParentId,
                menuItem.Layer,
                menuItem.Shortcut,
                menuItem.Icon,
                menuItem.Tooltip,
                menuItem.Param,
                menuItem.DllName,
                menuItem.WinName,
                menuItem.ControlType,
                menuItem.ShowType,
                menuItem.Order.ToString(),
                FrameWork.Function.NConvert.ToInt32( menuItem.Enabled).ToString(),
                menuItem.Description,
                menuItem.UserId,
                menuItem.OperDate.ToString(),
                menuItem.TreeDllName,
                menuItem.TreeName
            };
            string sql = "";
            if (this.GetSQL("SECURITY.MENU.INSERT", ref sql) == -1) return -1;
            try
            {
                sql = string.Format(sql, args);
            }
            catch (Exception ex) { this.Err = ex.Message; return -1; }
            if (this.ExecNoQuery(sql) <= 0) return -1;
            return 0;
        }

        /// <summary>
        /// ��ӽ�ɫ�Ͳ˵���ϵ
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public int InsertRoleMenuMap(string roleId, string menuId)
        {

            //AbstractSqlModel _sql = new SqlModel("Security.Menu.InsertRoleMenuMap");
            //_sql["menuid"] = menuId;
            //_sql["roleid"] = roleId;
            //return base.ExecuteNonQuery(_sql);

            string sql = "";
            if (this.GetSQL("SECURITY.MENU.INSERT", ref sql) == -1) return -1;
            try
            {
                sql = string.Format(sql,roleId,menuId);
            }
            catch (Exception ex) { this.Err = ex.Message; return -1; }
            if (this.ExecNoQuery(sql) <= 0) return -1;
            return 0;
        }

        /// <summary>
        /// ɾ��һ���˵���
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public int Delete(string menuId)
        {
            //AbstractSqlModel _sql = new SqlModel("Security.Menu.Delete");
            //_sql["menuid"] = menuId;
            //return base.ExecuteNonQuery(_sql);

            string sql = "";
            if (this.GetSQL("SECURITY.MENU.DELETE", ref sql) == -1) return -1;
            try
            {
                sql = string.Format(sql, menuId);
            }
            catch (Exception ex) { this.Err = ex.Message; return -1; }
            if (this.ExecNoQuery(sql) <= 0) return -1;
            return 0;
        }

        /// <summary>
        /// ɾ����ɫ�Ͳ˵��Ĺ�ϵ
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public int DeleteRoleMenuMap(string roleId, string menuId)
        {
            //AbstractSqlModel _sql = new SqlModel("Security.Menu.DeleteRoleMenuMap");
            //_sql["roleid"] = roleId;
            //_sql["menuid"] = menuId;
            //return base.ExecuteNonQuery(_sql);

            string sql = "";
            if (this.GetSQL("SECURITY.MENU.DELETEROLEMENUMAP", ref sql) == -1) return -1;
            try
            {
                sql = string.Format(sql,roleId, menuId,FS.FrameWork.Management.Connection.Hospital.ID);
            }
            catch (Exception ex) { this.Err = ex.Message; return -1; }
            if (this.ExecNoQuery(sql) <= 0) return -1;
            return 0;
        }

        /// <summary>
        /// ���²˵���
        /// </summary>
        /// <param name="menuItem"></param>
        /// <returns></returns>
        public int Update(MenuItem menuItem)
        {
            //AbstractSqlModel _sql = new SqlModel("Security.Menu.Update");
            //_sql["menuid"] = menuItem.Id;
            //_sql["menuname"] = menuItem.Name;
            //_sql["parentid"] = menuItem.ParentId;
            //_sql["layer"] = menuItem.Layer;
            //_sql["shortcut"] = menuItem.Shortcut;
            //_sql["icon"] = menuItem.Icon;
            //_sql["tooltip"] = menuItem.Tooltip;
            //_sql["param"] = menuItem.Param;
            //_sql["dllname"] = menuItem.DllName;
            //_sql["winname"] = menuItem.WinName;
            //_sql["controltype"] = menuItem.ControlType;
            //_sql["showtype"] = menuItem.ShowType;
            //_sql["isenabled"] = FS.Framework.Util.NConvert.ToInt32(menuItem.Enabled).ToString();
            //_sql["description"] = "";
            //_sql["operid"] = menuItem.UserId;
            //_sql["operdate"] = menuItem.OperDate;
            //_sql["sortid"] = menuItem.Order;
            //_sql["treedllname"] = menuItem.TreeDllName;
            //_sql["treename"] = menuItem.TreeName;
            //return base.ExecuteNonQuery(_sql);

            string[] args = new string[]{
                menuItem.Id,
                menuItem.Name,
                menuItem.ParentId,
                menuItem.Layer,
                menuItem.Shortcut,
                menuItem.Icon,
                menuItem.Tooltip,
                menuItem.Param,
                menuItem.DllName,
                menuItem.WinName,
                menuItem.ControlType,
                menuItem.ShowType,
                menuItem.Order.ToString(),
                FrameWork.Function.NConvert.ToInt32(menuItem.Enabled).ToString(),
                menuItem.Description,
                menuItem.UserId,
                menuItem.OperDate.ToString(),
                menuItem.TreeDllName,
                menuItem.TreeName
            };
            string sql = "";
            if (this.GetSQL("SECURITY.MENU.INSERT", ref sql) == -1) return -1;
            try
            {
                sql = string.Format(sql, args);
            }
            catch (Exception ex) { this.Err = ex.Message; return -1; }
            if (this.ExecNoQuery(sql) <= 0) return -1;
            return 0;


        }

        /// <summary>
        /// ��ѯȫ���˵���
        /// </summary>
        public IList<MenuItem> Query()
        {
            IList<MenuItem> _list = new List<MenuItem>();
            MenuItem _menu = null;

            //AbstractSqlModel _sql = new SqlModel("Security.Menu.GetAll");
            //DbDataReader _reader = base.ExecuteReader(_sql);
            string sql = "";
            if (this.GetSQL("SECURITY.MENU.GETALL", ref sql) == -1) return null;
            try
            {
                sql = string.Format(sql);
            }
            catch (Exception ex) { this.Err = ex.Message; return null; }
            if (this.ExecQuery(sql) < 0) return null;
            while (this.Reader.Read())
            {
                _menu = new MenuItem();
                _menu.Id = Reader[0].ToString();
                _menu.Name = Reader[1].ToString();
                _menu.ParentId = Reader[2].ToString();
                _menu.Layer = Reader[3].ToString();
                _menu.DllName = Reader[4].ToString();
                _menu.WinName = Reader[5].ToString();
                _menu.ControlType = Reader[6].ToString();
                _menu.ShowType = Reader[7].ToString();
                _menu.Shortcut = Reader[9].ToString();
                _menu.Icon = Reader[10].ToString();
                _menu.Tooltip = Reader[11].ToString();
                _menu.Param = Reader[12].ToString();
                _menu.Enabled = FrameWork.Function.NConvert.ToBoolean(Reader[13]);
                _menu.UserId = Reader[14].ToString();
                if (!Reader.IsDBNull(15))
                    _menu.OperDate = FrameWork.Function.NConvert.ToDateTime(Reader[15].ToString());
                _menu.Order = int.Parse(Reader[16].ToString());
                _menu.TreeDllName = Reader[17].ToString();
                _menu.TreeName = Reader[18].ToString();

                _list.Add(_menu);
            }
            Reader.Close();

            return _list;
        }

        /// <summary>
        /// ��ѯ��ɫӵ�еĲ˵���
        /// </summary>
        public IList<MenuItem> Query(string roleId)
        {
            IList<MenuItem> _list = new List<MenuItem>();
            MenuItem _menu = null;

            //AbstractSqlModel _sql = new SqlModel("Security.Menu.GetMenuByRole");
            //_sql["roleid"] = roleId;
            //DbDataReader _reader = base.ExecuteReader(_sql);
            string sql = "";
            if (this.GetSQL("SECURITY.MENU.GETMENUBYROLE", ref sql) == -1) return null;
            try
            {
                sql = string.Format(sql, roleId);
            }
            catch (Exception ex) { this.Err = ex.Message; return null; }
            if (this.ExecQuery(sql) <0) return null;
            while (this.Reader.Read())
            {
                _menu = new MenuItem();
                _menu.Id = Reader[0].ToString();
                _menu.Name = Reader[1].ToString();
                _menu.ParentId = Reader[2].ToString();
                _menu.Layer = Reader[3].ToString();
                _menu.DllName = Reader[4].ToString();
                _menu.WinName = Reader[5].ToString();
                _menu.ControlType = Reader[6].ToString();
                _menu.ShowType = Reader[7].ToString();
                _menu.Shortcut = Reader[9].ToString();
                _menu.Icon = Reader[10].ToString();
                _menu.Tooltip = Reader[11].ToString();
                _menu.Param = Reader[12].ToString();
                _menu.Enabled =FrameWork.Function.NConvert.ToBoolean(Reader[13]);
                _menu.UserId = Reader[14].ToString();
                if (!Reader.IsDBNull(15))
                    _menu.OperDate = FrameWork.Function.NConvert.ToDateTime(Reader[15].ToString());
                _menu.Order = int.Parse(Reader[16].ToString());
                _menu.TreeDllName = Reader[17].ToString();
                _menu.TreeName = Reader[18].ToString();

                _list.Add(_menu);
            }
            Reader.Close();

            return _list;
        }

        /// <summary>
        /// ���û���ȡ�˵�
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<MenuItem> QueryByUserID(string userId)
        {
            IList<MenuItem> _list = new List<MenuItem>();
            MenuItem _menu = null;

            //AbstractSqlModel _sql = new SqlModel("Security.Menu.GetMenuByUser");
            //_sql["userid"] = userId;
            //DbDataReader _reader = base.ExecuteReader(_sql);
            string sql = "";
            if (this.GetSQL("SECURITY.MENU.GETMENUBYUSER", ref sql) == -1) return null;
            try
            {
                sql = string.Format(sql, userId);
            }
            catch (Exception ex) { this.Err = ex.Message; return null; }
            if (this.ExecQuery(sql) < 0) return null;
            while (this.Reader.Read())
            {
                _menu = new MenuItem();
                _menu.Id = Reader[0].ToString();
                _menu.Name = Reader[1].ToString();
                _menu.ParentId = Reader[2].ToString();
                _menu.Layer = Reader[3].ToString();
                _menu.DllName = Reader[4].ToString();
                _menu.WinName = Reader[5].ToString();
                _menu.ControlType = Reader[6].ToString();
                _menu.ShowType = Reader[7].ToString();
                _menu.Shortcut = Reader[9].ToString();
                _menu.Icon = Reader[10].ToString();
                _menu.Tooltip = Reader[11].ToString();
                _menu.Param = Reader[12].ToString();
                _menu.Enabled = FrameWork.Function.NConvert.ToBoolean(Reader[13]);
                _menu.UserId = Reader[14].ToString();
                if (!Reader.IsDBNull(15))
                    _menu.OperDate = FrameWork.Function.NConvert.ToDateTime(Reader[15].ToString());
                _menu.Order = int.Parse(Reader[16].ToString());
                _menu.TreeDllName = Reader[17].ToString();
                _menu.TreeName = Reader[18].ToString();

                _list.Add(_menu);
            }
            Reader.Close();

            return _list;
        }

        /// <summary>
        /// ����Դ���Ͷ�ȡ�˵�
        /// </summary>
        /// <param name="typeRes"></param>
        /// <returns></returns>
        public IList<MenuItem> QueryResByType(string typeRes)
        {
            IList<MenuItem> _list = new List<MenuItem>();
            MenuItem _menu = null;

            //AbstractSqlModel _sql = new SqlModel("Security.Menu.QueryByType");
            //_sql["controltype"] = typeRes;
            //DbDataReader _reader = base.ExecuteReader(_sql);
            string sql = "";
            if (this.GetSQL("SECURITY.MENU.QUERYBYTYPE", ref sql) == -1) return null;
            try
            {
                sql = string.Format(sql, typeRes);
            }
            catch (Exception ex) { this.Err = ex.Message; return null; }
            if (this.ExecQuery(sql) < 0) return null;
            while (this.Reader.Read())
            {
                _menu = new MenuItem();
                _menu.Id = Reader[0].ToString();
                _menu.Name = Reader[1].ToString();
                _menu.ParentId = Reader[2].ToString();
                _menu.Layer = Reader[3].ToString();
                _menu.DllName = Reader[8].ToString();
                _menu.WinName = Reader[9].ToString();
                _menu.ControlType = Reader[10].ToString();
                _menu.ShowType = Reader[11].ToString();
                _menu.Shortcut = Reader[4].ToString();
                _menu.Icon = Reader[5].ToString();
                _menu.Tooltip = Reader[6].ToString();
                _menu.Param = Reader[7].ToString();
                _menu.Order = int.Parse(Reader[12].ToString());
                _menu.Enabled = FrameWork.Function.NConvert.ToBoolean(Reader[13]);
                _menu.UserId = Reader[15].ToString();
                if (!Reader.IsDBNull(16))
                    _menu.OperDate = FrameWork.Function.NConvert.ToDateTime(Reader[16].ToString());
                _menu.TreeDllName = Reader[17].ToString();
                _menu.TreeName = Reader[18].ToString();

                _list.Add(_menu);
            }
            Reader.Close();

            return _list;
        }
        #endregion
    }
}
