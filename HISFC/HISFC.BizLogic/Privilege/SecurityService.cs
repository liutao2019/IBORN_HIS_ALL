using System;
using System.Collections.Generic;
using System.Text;
using FS.HISFC.BizLogic.Privilege.Model;

namespace FS.HISFC.BizLogic.Privilege
{
    /// <summary>
    /// 
    /// </summary>
    public class SecurityService : DataBase
    {

        #region �˵�ҵ��
        /// <summary>
        /// ����˵�
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public  MenuItem SaveMenu(MenuItem menu)
        {
           // FS.Framework.Accessory.Context.ContextProcess _appContext = new FS.Framework.Accessory.Context.ContextProcess();
            
            //using (DaoManager _dao = new FS.Framework.DataAccess.DaoManager())
            //{
            MenuLogic menuLogic = new MenuLogic();
                //�������˵�,����
                if (string.IsNullOrEmpty(menu.Id))
                {
                    menu.Id = this.GetSequence("PRIV.SEQ_MENUID");
                    menu.Order = int.Parse(this.GetSequence("PRIV.SEQ_MENUORDER"));

                    int i = menuLogic.Insert(menu);
                    if (i <= 0) return null;
                }
                else//����
                {
                    int i = menuLogic.Update(menu);
                    if (i <= 0) return null;
                }
            //}

            return menu;
        }

        /// <summary>
        /// ��ӽ�ɫ�˵���Ȩ
        /// </summary>
        /// <param name="newRoles"></param>
        /// <param name="newMenus"></param>
        /// <returns></returns>
        public static int AddRoleMenuMap(List<string> newRoles, List<string> newMenus)
        {
            MenuLogic _logic = new MenuLogic();;

            //using (DaoManager _dao = new FS.Framework.DataAccess.DaoManager())
            //{
                foreach (string roleid in newRoles)
                {
                    IList<MenuItem> _menus = _logic.Query(roleid);

                    //����Ѵ��ڵĲ˵�id����newMenus��,����ɾ���ö��չ�ϵ
                    foreach (MenuItem _menu in _menus)
                    {
                        if (!newMenus.Contains(_menu.Id))
                        {
                            if (_logic.DeleteRoleMenuMap(roleid, _menu.Id) <= 0) return -1;
                        }
                    }

                    //���newMenus�в���_menus��,����
                    foreach (string menuId in newMenus)
                    {
                        if (!menuContains(_menus, menuId))
                        {
                            if (_logic.InsertRoleMenuMap(roleid, menuId) <= 0) return -1;
                        }
                    }
                }
            //}

            return 0;
        }

        private static bool menuContains(IList<MenuItem> menus, string menuId)
        {
            foreach (MenuItem _menu in menus)
            {
                if (_menu.Id == menuId)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// ɾ���˵�
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public static int DeleteMenu(string menuId)
        {
            //ɾ���˵�����            
            using (MenuLogic _logic = new MenuLogic())
            {
                return _logic.Delete(menuId);
            }
            //ɾ���Ӳ˵�
        }

        /// <summary>
        /// ��ѯȫ���˵�
        /// </summary>
        /// <returns></returns>
        public static IList<MenuItem> QueryMenu()
        {
            using (MenuLogic _logic = new MenuLogic())
            {
                return _logic.Query();
            }
        }

        /// <summary>
        /// ���ݽ�ɫ��ѯӵ�в˵�
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public static IList<MenuItem> QueryMenuByRole(string roleId)
        {
            using (MenuLogic _logic = new MenuLogic())
            {
                return _logic.Query(roleId);
            }
        }

        /// <summary>
        /// ��ѯ�û��˵��б�
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static IList<MenuItem> QueryMenuByUser(string userId)
        {
            using (MenuLogic _logic = new MenuLogic())
            {
                return _logic.QueryByUserID(userId);
            }
        }

        /// <summary>
        /// ��ѯ�˵��б��б�
        /// </summary>
        /// <param name="typeRes"></param>
        /// <returns></returns>
        public static IList<MenuItem> QueryResByType(string typeRes)
        {
            using (MenuLogic _logic = new MenuLogic())
            {
                return _logic.QueryResByType(typeRes);
            }
        }

        #endregion

        #region ��¼�ʻ�ҵ��
        /// <summary>
        /// ��ѯ�û��б�
        /// </summary>
        /// <returns></returns>
        public static IList<User> QueryUser()
        {
            return new UserLogic().Query();
        }

        /// <summary>
        /// ��ѯ�û��б�
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public static IList<User> QueryUser(string roleId)
        {
            return new UserLogic().Query(roleId);
        }

        /// <summary>
        /// ��ѯ�û�
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="personId"></param>
        /// <returns></returns>
        public static User GetUserByPersonId(string appId, string personId)
        {
            return new UserLogic().GetByPsnID(personId, appId);
        }

        /// <summary>
        /// ��ѯ�û�
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public static User GetUserByAccount(string account)
        {
            return new UserLogic().GetByAccount(account);
        }

        /// <summary>
        /// �����û���Ϣ
        /// </summary>
        /// <param name="user"></param>
        /// <param name="newRolesId"></param>
        /// <returns></returns>
        public  User SaveUser(User user, IList<string> newRolesId)
        {
            //�����ɫ��Ϣ
            int _rtn;
            //FS.Framework.Accessory.Context.ContextProcess _appContext = new FS.Framework.Accessory.Context.ContextProcess();

            //using (DaoManager _dao = new FS.Framework.DataAccess.DaoManager())
            //{
            UserLogic userLogic = new UserLogic();
                if (string.IsNullOrEmpty(user.Id))//����
                {
                    user.Id = this.GetSequence("PRIV.SEQ_USERID");
                    _rtn = userLogic.Insert(user);
                }
                else//�޸�
                {
                    _rtn = userLogic.Update(user);
                }

                if (_rtn <= 0) return null;

                //�����ɫ�û����ձ�

                foreach (string roleId in newRolesId)
                {
                    _rtn = new RoleLogic().InsertRoleUserMap(roleId, user.Id);
                    if (_rtn <= 0) return null;
                }
            //}

            return user;
        }

        /// <summary>
        /// ɾ���û���Ϣ
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static int RemoveUser(string userId)
        {
            return new UserLogic().Delete(userId);
        }
        #endregion

        #region ��ɫҵ��

        /// <summary>
        /// �����ɫ
        /// </summary>
        /// <param name="role"></param>
        /// <param name="newUsersId"></param>
        /// <returns></returns>
        public  Role SaveRole(Role role, IList<string> newUsersId)
        {
            //�����ɫ��Ϣ
            int _rtn;
           // FS.Framework.Accessory.Context.ContextProcess _appContext = new FS.Framework.Accessory.Context.ContextProcess();

            //using (DaoManager _dao = new FS.Framework.DataAccess.DaoManager())
            //{
            RoleLogic roleLogic = new RoleLogic();
            if (string.IsNullOrEmpty(role.ID))//����
                {
                    role.ID = GetSequence("PRIV.SEQ_ROLEID");
                    _rtn = roleLogic.Insert(role);
                }
                else//�޸�
                {
                    _rtn = roleLogic.Update(role);
                }

                if (_rtn <0) return null;

                //�����ɫ�û����ձ�

                foreach (string userId in newUsersId)
                {
                    _rtn = roleLogic.InsertRoleUserMap(role.ID, userId);
                    if (_rtn < 0) return null;
                }
            //}

            return role;
        }

        /// <summary>
        /// ɾ���û���ɫ��ϵ
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static int RemoveRoleUserMap(string roleId, string userId)
        {
            return new RoleLogic().DelRoleUserMap(roleId, userId);
        }

        /// <summary>
        /// ɾ����ɫ
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public static int RemoveRole(string roleId)
        {
            return new RoleLogic().Delete(roleId);
        }

        /// <summary>
        /// ɾ����ɫ
        /// </summary>
        /// <param name="role"></param>
        public static void RemoveRole(FS.HISFC.BizLogic.Privilege.Model.Role role)
        {
            //using (DaoManager daoMgr = new DaoManager())
            //{
                try
                {
                    RoleLogic roleLogic = new RoleLogic();
                    //daoMgr.BeginTransaction();
                    //ɾ����ɫ��Ϣ��
                    roleLogic.Delete(role.ID);
                    //ɾ����ɫȨ�޶�Ӧ��
                    RoleResourceProcess roleRes = new RoleResourceProcess();
                    List<RoleResourceMapping> roleResList = roleRes.QueryByRole(role.ID);
                    roleRes.DeleteRoleResource(roleResList);

                    //ɾ����ɫ��Ȩ��Ϣ
                    AuthorityLogic authorityLogic = new AuthorityLogic();
                    authorityLogic.DeleteRole(role.ID);

                    //daoMgr.CommitTransaction();

                }
                catch
                {
                    //daoMgr.RollBackTransaction();
                    throw;
                }

            //}

        }

        /// <summary>
        /// ��ѯ��ɫ�б�
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static IList<Role> QueryRoleByUser(string userId)
        {
            return new RoleLogic().Query(userId);
        }

        /// <summary>
        /// ��ѯ��ɫ�б�
        /// </summary>
        /// <returns></returns>
        public static IList<Role> QueryAllRole()
        {
            return new RoleLogic().Query();
        }

        /// <summary>
        /// ��ѯ��ɫ�б�
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public static IList<Role> QueryChildRole(string roleId)
        {
            return new RoleLogic().QueryChildRole(roleId);
        }
        #endregion

        #region ��Դ����ҵ��
        /// <summary>
        /// ����Ȩ����Ϣ
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        public static int SaveResource(Priv res)
        {
            PrivLogic privlogic = new PrivLogic();
            //using (DaoManager _dao = new DaoManager())
            //{
                int rtn = privlogic.Update(res);

                if (rtn == 0)
                {
                    return privlogic.Insert(res);
                }
            //}
            return 0;
        }

        /// <summary>
        /// ɾ��Ȩ����Ϣ
        /// </summary>
        /// <param name="resId"></param>
        /// <returns></returns>
        public static int RemoveResource(string resId)
        {
            return new PrivLogic().Delete(resId);
        }

        /// <summary>
        /// һ�ֲ�����Ȩ��ɾ�� 
        /// </summary>
        /// <param name="priv"></param>
        /// <returns></returns>
        public static int RemoveResource(Priv priv)
        {
            //using (DaoManager dao = new DaoManager())
            //{
            PrivLogic privlogic = new PrivLogic();
                try
                {
                    //dao.BeginTransaction();
                    //ɾ��Ȩ�ޱ���Ϣ
                    privlogic.Delete(priv);
                    //ɾ��Ȩ�޺ͽ�ɫ���ձ�
                    privlogic.DelResRoleMap(priv.Id);
                    //ɾ���û�Ȩ�޶��ձ�
                    new AuthorityLogic().DeletePriByPriv(priv.Id);

                    //dao.CommitTransaction();
                    return 1;
                }
                catch 
                {
                    //dao.RollBackTransaction();
                    return 0;
                }

            //}

        }

        /// <summary>
        /// ����Ȩ��
        /// </summary>
        /// <param name="resType"></param>
        /// <param name="role"></param>
        /// <param name="resource"></param>
        /// <param name="pmsExp"></param>
        /// <returns></returns>
        public static int SavePermission(string resType, Role role, Priv resource, string pmsExp)
        {
            //using (DaoManager _dao = new DaoManager())
            //{
            PrivLogic privlogic = new PrivLogic();
                IDictionary<Priv, IList<Operation>> _permission = privlogic.QueryPermission(resType, resource.Id, role);

                //����
                if (_permission.Count > 0)
                {
                    int rtn = privlogic.DelResRoleMap(resource.Id, role.ID);
                    if (rtn <= 0) return -1;
                }

                return privlogic.InsertResRoleMap(resource, role.ID, pmsExp, "");
            //}
        }

        /// <summary>
        /// ����Ȩ��
        /// </summary>
        /// <param name="role"></param>
        /// <param name="allResource"></param>
        /// <param name="resource"></param>
        /// <param name="pmsExp"></param>
        /// <param name="deleteResList"></param>
        /// <param name="deleteRoleList"></param>
        /// <returns></returns>
        public static int SavePermission(Role role, List<Priv> allResource, List<Priv> resource, string pmsExp,List<Priv> deleteResList,List<Role> deleteRoleList)
        {
            //using (DaoManager daoMgr = new DaoManager())
            //{
                try
                {
                    //daoMgr.BeginTransaction();
                    PrivLogic privlogic = new PrivLogic();
                    foreach (Priv res in allResource)
                    {
                        privlogic.DelResRoleMap(res.Id, role.ID);
                    }
                    //����ɾ�������ӽ�ɫ��Ӧ����Դ
                    foreach (Role childrole in deleteRoleList)
                    {
                        foreach (Priv delRes in deleteResList)
                        {
                            privlogic.DelResRoleMap(delRes.Id, childrole.ID);
                        }
                    }
                    foreach (Priv newRes in resource)
                    {
                        privlogic.InsertResRoleMap(newRes, role.ID, pmsExp, "");
                    }
                    //daoMgr.CommitTransaction();
                    return 1;
                }
                catch (Exception ex)
                {
                    //daoMgr.RollBackTransaction();
                    throw ex;
                }
            //}
        }

        #endregion
    }
}
