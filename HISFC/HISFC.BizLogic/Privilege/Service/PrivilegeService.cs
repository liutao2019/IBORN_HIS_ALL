using System;
using System.Collections.Generic;
using System.Text;
//using FS.Framework.Wcf;
using System.Configuration;
using FS.HISFC.BizLogic.Privilege.Model;

namespace FS.HISFC.BizLogic.Privilege.Service
{
    /// <summary>
    /// Ȩ�޹���
    /// </summary>
    //[WcfExcetionHandler]
    public class PrivilegeService
    {
       //// private FS.Framework.DataAccess.DaoManager daoMgr = null;
       // public PrivilegeService()
       // {
       //     //daoMgr = new FS.Framework.DataAccess.DaoManager();
       // }

        #region IMenuService ��Ա
        /// <summary>
        /// �����ɫ�˵���Ȩ
        /// </summary>
        /// <param name="newRoles"></param>
        /// <param name="newMenus"></param>
        /// <returns></returns>
        public int AddRoleMenuMap(List<string> newRoles, List<string> newMenus)
        {
            return SecurityService.AddRoleMenuMap(newRoles, newMenus);
        }

        /// <summary>
        ///��ѯ�˵��б�
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public IList<FS.HISFC.BizLogic.Privilege.Model.MenuItem> QueryMenu(string roleId)
        {
            return SecurityService.QueryMenuByRole(roleId);
        }

        /// <summary>
        /// ��ѯ�˵��б�
        /// </summary>
        /// <returns></returns>
        public IList<FS.HISFC.BizLogic.Privilege.Model.MenuItem> QueryMenu()
        {
            return SecurityService.QueryMenu();
        }

        /// <summary>
        /// ��ѯ�˵��б�
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<FS.HISFC.BizLogic.Privilege.Model.MenuItem> QueryMenuByUser(string userId)
        {
            return SecurityService.QueryMenuByUser(userId);
        }

        /// <summary>
        /// ɾ���˵�
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public int RemoveMenuItem(string menuId)
        {
            return SecurityService.DeleteMenu(menuId);
        }

        /// <summary>
        /// ����˵�
        /// </summary>
        /// <param name="menuItem"></param>
        /// <returns></returns>
        public FS.HISFC.BizLogic.Privilege.Model.MenuItem SaveMenuItem(FS.HISFC.BizLogic.Privilege.Model.MenuItem menuItem)
        {
            SecurityService ss = new SecurityService();
            return ss.SaveMenu(menuItem);
        }

        /// <summary>
        /// ��ѯ�˵��б�
        /// </summary>
        /// <param name="typeRes"></param>
        /// <returns></returns>
        public IList<FS.HISFC.BizLogic.Privilege.Model.MenuItem> QueryResByType(string typeRes)
        {
            return SecurityService.QueryResByType(typeRes);
        }

        #endregion

        #region IOrgFactory ��Ա

        /// <summary>
        /// ���AppId
        /// </summary>
        /// <returns></returns>
        public IList<string> QueryAppID()
        {
            IList<string> _list = new List<string>();

            // getOrgProvider();

            //foreach (string key in _orgProviders.Keys)
            //{
            //    _list.Add(key);
            //}

            foreach (string key in OrgFactory.getOrgProvider().Keys)
            {
                _list.Add(key);
            }

            return _list;
        }

        /// <summary>
        /// ��ѯ��Ա��Ϣ
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        public IList<HISFC.Models.Privilege.Person> QueryPerson(string appId)
        {
            //getOrgProvider();
            //return _orgProviders[appId].QueryPerson();
            return OrgFactory.getOrgProvider()[appId].QueryPerson();
        }

        /// <summary>
        /// ��ѯ��֯�ṹ��Ϣ
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        public IList<HISFC.Models.Privilege.Organization> QueryUnit(string appId)
        {
            //getOrgProvider();
            //return _orgProviders[appId].QueryUnit();
            if (!OrgFactory.getOrgProvider().Keys.Contains(appId))
                return null;
            else
                return OrgFactory.getOrgProvider()[appId].QueryUnit();

        }

        /// <summary>
        /// ��ѯ��֯�ṹ����
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        public List<String> GetOrgType(string appId)
        {
            return OrgFactory.getOrgProvider()[appId].GetOrgType();

        }
        #endregion

        #region IOrgService ��Ա

        /// <summary>
        /// ��ѯ��ɫ
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public FS.HISFC.BizLogic.Privilege.Model.Role GetRole(string roleId)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        /// <summary>
        /// ��ѯ�û�
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public FS.HISFC.BizLogic.Privilege.Model.User GetUserByAccount(string account)
        {
            return SecurityService.GetUserByAccount(account);
        }

        /// <summary>
        /// ��ѯ�û�
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public FS.HISFC.BizLogic.Privilege.Model.User GetUserByID(string userId)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        /// <summary>
        /// ��ѯ�û�
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="personId"></param>
        /// <returns></returns>
        public FS.HISFC.BizLogic.Privilege.Model.User GetUserByPersonId(string appId, string personId)
        {
            return SecurityService.GetUserByPersonId(appId, personId);
        }

        /// <summary>
        /// ��ѯ�ӽ�ɫ�б�
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public IList<FS.HISFC.BizLogic.Privilege.Model.Role> QueryChildRole(string roleId)
        {
            return SecurityService.QueryChildRole(roleId);
        }

        /// <summary>
        /// ��ѯ��ɫ�б�
        /// </summary>
        /// <returns></returns>
        public IList<FS.HISFC.BizLogic.Privilege.Model.Role> QueryRole()
        {
            return SecurityService.QueryAllRole();
        }

        /// <summary>
        /// ��ѯ��ɫ�б�
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<FS.HISFC.BizLogic.Privilege.Model.Role> QueryRole(string userId)
        {
            return SecurityService.QueryRoleByUser(userId);
        }

        /// <summary>
        /// ��ѯ�û��б�
        /// </summary>
        /// <returns></returns>
        public IList<FS.HISFC.BizLogic.Privilege.Model.User> QueryUser()
        {
            return SecurityService.QueryUser();
        }

        /// <summary>
        /// ��ѯ�û��б�
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public IList<FS.HISFC.BizLogic.Privilege.Model.User> QueryUser(string roleId)
        {
            return SecurityService.QueryUser(roleId);
        }

        /// <summary>
        /// ɾ����ɫ
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public int RemoveRole(string roleId)
        {
            return SecurityService.RemoveRole(roleId);
        }

        /// <summary>
        /// ɾ����ɫ
        /// </summary>
        /// <param name="role"></param>
        public void RemoveRole(Role role)
        {
            SecurityService.RemoveRole(role);
        }

        /// <summary>
        /// ɾ����ɫ
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public int RemoveRoleByUnitID(string unitId)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        /// <summary>
        /// ɾ���û���ɫ��Ȩ
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int RemoveRoleUserMap(string roleId, string userId)
        {
            return SecurityService.RemoveRoleUserMap(roleId, userId);
        }

        /// <summary>
        /// ɾ���û�
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int RemoveUser(string userId)
        {
            return SecurityService.RemoveUser(userId);
        }

        /// <summary>
        /// �����ɫ
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        public FS.HISFC.BizLogic.Privilege.Model.Role SaveRole(HISFC.Models.Privilege.Organization unit)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        /// <summary>
        /// �����ɫ
        /// </summary>
        /// <param name="role"></param>
        /// <param name="newUsersId"></param>
        /// <returns></returns>
        public FS.HISFC.BizLogic.Privilege.Model.Role SaveRole(FS.HISFC.BizLogic.Privilege.Model.Role role, IList<string> newUsersId)
        {
            SecurityService ss = new SecurityService();
            return ss.SaveRole(role, newUsersId);
        }

        /// <summary>
        /// �����û�
        /// </summary>
        /// <param name="user"></param>
        /// <param name="newRolesId"></param>
        /// <returns></returns>
        public FS.HISFC.BizLogic.Privilege.Model.User SaveUser(FS.HISFC.BizLogic.Privilege.Model.User user, IList<string> newRolesId)
        {
            SecurityService ss = new SecurityService();
            return ss.SaveUser(user, newRolesId);
        }

        #endregion

        #region IPermissionFactory ��Ա

        // private static IDictionary<ResourceType, IList<Operation>> _permissionProviders = null;
        private static IDictionary<string, IPermissionProvider> _providers = new Dictionary<string, IPermissionProvider>();

        //private void LoadPermissionProvider()
        //{
        //    if (_permissionProviders != null) return;

        //    _permissionProviders = ConfigurationFactory.LoadPermission();
        //    if (_permissionProviders == null) throw new Exception("���������ļ��м���Ȩ�޹���ʵ�ֿ�!");
        //}

        private IPermissionProvider GetPermissionProviderByResType(ResourceType resType)
        {
            IPermissionProvider _provider = null;

            if (_providers.TryGetValue(resType.Id, out _provider))
            {
                return _provider;
            }

            try
            {
                string[] array = resType.ImplType.Split(new char[] { ',' });
                _provider = (IPermissionProvider)ConfigurationFactory.Reflect(array[0], array[1]);
            }
            catch (Exception e)
            { throw e; }

            _providers.Add(resType.Id, _provider);

            return _provider;
        }

        private static ResourceType GetResourceTypeByID(string ResourceType)
        {
            ResourceType _resType = null;
            foreach (ResourceType _type in PermissionFactory.LoadPermissionProvider().Keys)
            {
                if (_type.Id == ResourceType)
                {
                    _resType = _type;
                    break;
                }
            }
            return _resType;
        }

        /// <summary>
        /// �����Դ����
        /// </summary>
        /// <returns></returns>
        public IList<ResourceType> GetResourceTypes()
        {
            //LoadPermissionProvider();

            IList<ResourceType> _types = new List<ResourceType>();
            foreach (ResourceType _resType in PermissionFactory.LoadPermissionProvider().Keys)
            {
                _types.Add(_resType);
            }
            return _types;
        }

        /// <summary>
        /// ���Ȩ�޲���
        /// </summary>
        /// <param name="ResourceType"></param>
        /// <returns></returns>
        public IList<FS.HISFC.BizLogic.Privilege.Model.Operation> GetOperation(string ResourceType)
        {
            //LoadPermissionProvider();

            foreach (KeyValuePair<ResourceType, IList<Operation>> _pair in PermissionFactory.LoadPermissionProvider())
            {
                if (_pair.Key.Id == ResourceType)
                {
                    return _pair.Value;
                }
            }

            return new List<Operation>();
        }

        /// <summary>
        /// ���Ȩ���б�
        /// </summary>
        /// <param name="ResourceType"></param>
        /// <returns></returns>
        public IList<FS.HISFC.BizLogic.Privilege.Model.Priv> GetResource(string ResourceType)
        {
            //LoadPermissionProvider();
            PermissionFactory.LoadPermissionProvider();
            ResourceType _resType = GetResourceTypeByID(ResourceType);

            if (_resType == null) throw new Exception("û������Ϊ:" + ResourceType + "����Դ!");

            return GetPermissionProviderByResType(_resType).GetResource(ResourceType);
        }

        /// <summary>
        /// ���Ȩ�޲����б�
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public IDictionary<Priv, IList<Operation>> GetPermission(Role role)
        {
            Dictionary<Priv, IList<Operation>> newDic = new Dictionary<Priv, IList<Operation>>();
            PermissionFactory.LoadPermissionProvider();
            foreach (ResourceType resType in GetResourceTypes())
            {
                foreach (KeyValuePair<Priv, IList<Operation>> selectDic in GetPermissionProviderByResType(resType).GetPermission(resType.Id, role))
                {
                    newDic.Add(selectDic.Key, selectDic.Value);
                }
            }

            return newDic;
        }

        /// <summary>
        /// ���Ȩ�޲����б�
        /// </summary>
        /// <param name="ResourceType"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public IDictionary<Priv, IList<Operation>> GetPermission(string ResourceType, Role role)
        {
            // LoadPermissionProvider();
            PermissionFactory.LoadPermissionProvider();
            ResourceType _resType = GetResourceTypeByID(ResourceType);

            if (_resType == null) throw new Exception("û������Ϊ:" + ResourceType + "����Դ!");

            return GetPermissionProviderByResType(_resType).GetPermission(ResourceType, role);
        }

        /// <summary>
        /// ���Ȩ�޲����б�
        /// </summary>
        /// <param name="ResourceType"></param>
        /// <param name="resId"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public IDictionary<Priv, IList<Operation>> GetPermission(string ResourceType, string resId, Role role)
        {
            //LoadPermissionProvider();
            PermissionFactory.LoadPermissionProvider();
            ResourceType _resType = GetResourceTypeByID(ResourceType);

            if (_resType == null) throw new Exception("û������Ϊ:" + ResourceType + "����Դ!");

            return GetPermissionProviderByResType(_resType).GetPermission(ResourceType, resId, role);
        }

        /// <summary>
        /// ��Ȩ
        /// </summary>
        /// <param name="ResourceType"></param>
        /// <param name="role"></param>
        /// <param name="resource"></param>
        /// <param name="operations"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public int Grant(string ResourceType, Role role, Priv resource, IList<Operation> operations, object param)
        {
            // LoadPermissionProvider();
            PermissionFactory.LoadPermissionProvider();
            ResourceType _resType = GetResourceTypeByID(ResourceType);

            if (_resType == null) throw new Exception("û������Ϊ:" + ResourceType + "����Դ!");

            return GetPermissionProviderByResType(_resType).Grant(role, resource, operations, param);
        }

        /// <summary>
        /// �ж�
        /// </summary>
        /// <param name="ResourceType"></param>
        /// <param name="role"></param>
        /// <param name="resource"></param>
        /// <param name="operation"></param>
        /// <returns></returns>
        public bool IsAllowed(string ResourceType, FS.HISFC.BizLogic.Privilege.Model.Role role, FS.HISFC.BizLogic.Privilege.Model.Priv resource, FS.HISFC.BizLogic.Privilege.Model.Operation operation)
        {
            //LoadPermissionProvider();
            PermissionFactory.LoadPermissionProvider();
            ResourceType _resType = GetResourceTypeByID(ResourceType);

            if (_resType == null) throw new Exception("û������Ϊ:" + ResourceType + "����Դ!");

            return GetPermissionProviderByResType(_resType).IsAllowed(role, resource, operation);
        }

        /// <summary>
        /// ����Ȩ��
        /// </summary>
        /// <param name="ResourceType"></param>
        /// <param name="role"></param>
        /// <param name="resource"></param>
        /// <returns></returns>
        public int Revoke(string ResourceType, FS.HISFC.BizLogic.Privilege.Model.Role role, FS.HISFC.BizLogic.Privilege.Model.Priv resource)
        {
            // LoadPermissionProvider();
            PermissionFactory.LoadPermissionProvider();
            ResourceType _resType = GetResourceTypeByID(ResourceType);

            if (_resType == null) throw new Exception("û������Ϊ:" + ResourceType + "����Դ!");

            return GetPermissionProviderByResType(_resType).Revoke(role, resource);
        }

        /// <summary>
        /// ����Ȩ��
        /// </summary>
        /// <param name="ResourceType"></param>
        /// <param name="role"></param>
        /// <param name="resource"></param>
        /// <param name="operations"></param>
        /// <returns></returns>
        public int Revoke(string ResourceType, Role role, Priv resource, IList<Operation> operations)
        {
            // LoadPermissionProvider();
            PermissionFactory.LoadPermissionProvider();
            ResourceType _resType = GetResourceTypeByID(ResourceType);

            if (_resType == null) throw new Exception("û������Ϊ:" + ResourceType + "����Դ!");

            return GetPermissionProviderByResType(_resType).Revoke(role, resource, operations);
        }

        #endregion

        #region IDisposable ��Ա

        ///// <summary>
        ///// �ͷ���Դ
        ///// </summary>
        //public void Dispose()
        //{
        //    using (daoMgr) { }
        //    GC.Collect();
        //    GC.SuppressFinalize(this);
        //}

        #endregion

        #region Resource ��Ա

        /// <summary>
        /// ɾ����Դ
        /// </summary>
        /// <param name="resId"></param>
        /// <returns></returns>
        public int RemoveResource(string resId)
        {
            return SecurityService.RemoveResource(resId);
        }

        /// <summary>
        /// ɾ����Դ
        /// </summary>
        /// <param name="priv"></param>
        /// <returns></returns>
        public int RemoveResource(Priv priv)
        {
            return SecurityService.RemoveResource(priv);
        }

        /// <summary>
        /// ����Ȩ��
        /// </summary>
        /// <param name="resType"></param>
        /// <param name="role"></param>
        /// <param name="resource"></param>
        /// <param name="pmsExp"></param>
        /// <returns></returns>
        public int SavePermission(string resType, Role role, Priv resource, string pmsExp)
        {
            return SecurityService.SavePermission(resType, role, resource, pmsExp);
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
        public int SavePermission(Role role, List<Priv> allResource, List<Priv> resource, string pmsExp, List<Priv> deleteResList, List<Role> deleteRoleList)
        {
            return SecurityService.SavePermission(role, allResource, resource, pmsExp, deleteResList, deleteRoleList);
        }

        /// <summary>
        /// ������Դ
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        public int SaveResource(Priv res)
        {
            return SecurityService.SaveResource(res);
        }

        #endregion

        #region IFacadeService ��Ա

        /// <summary>
        /// ��ȡϵͳʱ��
        /// </summary>
        /// <returns></returns>
        public DateTime GetDateTime()
        {
            //FS.Framework.Accessory.Context.ContextProcess _appContext = new FS.Framework.Accessory.Context.ContextProcess();
            //return _appContext.GetServerDateTime();
            return this.GetDateTime();
        }

        #endregion

        #region IAuthenticationProvider ��Ա

        /// <summary>
        /// ��֤
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <param name="domain"></param>
        /// <returns></returns>
        public NeuIdentity Authenticate(string name, string password, string domain)
        {


            NeuIdentity _identity = new DBAuthenticationProvider().Authenticate(name, password, domain);

            return _identity;
        }

        #endregion

        #region Privs ��Ա

        /// <summary>
        /// ����Ȩ��
        /// </summary>
        /// <param name="resourcesItem"></param>
        /// <returns></returns>
        public Resource SaveResourcesItem(Resource resourcesItem)
        {

            return new ResourceProcess().SaveResourcesItem(resourcesItem);

        }

        /// <summary>
        /// ��ӽ�ɫ��Ȩ
        /// </summary>
        /// <param name="newRoles"></param>
        /// <param name="newResources"></param>
        /// <returns></returns>
        public int AddRoleResourcesMap(List<string> newRoles, List<string> newResources)
        {
            return new ResourceProcess().AddRoleResourcesMap(newRoles, newResources);
        }

        /// <summary>
        /// ɾ��Ȩ��
        /// </summary>
        /// <param name="resourcesId"></param>
        /// <returns></returns>
        public int RemoveResourcesItem(string resourcesId)
        {
            return new ResourceProcess().RemoveResourcesItem(resourcesId);
        }

        /// <summary>
        /// ɾ��Ȩ��
        /// </summary>
        /// <param name="parentResId"></param>
        /// <param name="childReslist"></param>
        /// <returns></returns>
        public int RemoveResourcesItem(string parentResId, List<Resource> childReslist)
        {
            return new ResourceProcess().RemoveResourcesItem(parentResId, childReslist);
        }

        /// <summary>
        /// ��ѯȨ��
        /// </summary>
        /// <returns></returns>
        public List<Resource> QueryResources()
        {
            return new ResourceProcess().QueryResources();
        }

        /// <summary>
        /// ��ѯȨ��
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public List<Resource> QueryResourcesByRole(string roleId)
        {
            return new ResourceProcess().QueryResourcesByRole(roleId);
        }

        /// <summary>
        /// ��ѯȨ��
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<Resource> QueryResourcesByUser(string userId)
        {
            return new ResourceProcess().QueryResourcesByUser(userId);
        }

        /// <summary>
        /// ��ѯȨ��
        /// </summary>
        /// <param name="typeRes"></param>
        /// <returns></returns>
        public List<Resource> QueryResourcesByType(string typeRes)
        {
            return new ResourceProcess().QueryResourcesByType(typeRes);
        }

        /// <summary>
        /// ��ѯȨ��
        /// </summary>
        /// <param name="resourceId"></param>
        /// <returns></returns>
        public List<Resource> QueryResourcesById(string resourceId)
        {
            return new ResourceProcess().QueryResourcesById(resourceId);
        }

        /// <summary>
        /// ɾ��Ȩ��
        /// </summary>
        /// <param name="res"></param>
        public void RemoveResourcesItem(Resource res)
        {
            new ResourceProcess().RemoveResourcesItem(res);
        }
        #endregion

        #region RoleResource
        /// <summary>
        /// �����ɫ��Ȩ
        /// </summary>
        /// <param name="roleResourceMapping"></param>
        public void InsertRoleResource(RoleResourceMapping roleResourceMapping)
        {
            new RoleResourceProcess().InsertRoleResource(roleResourceMapping);
        }

        /// <summary>
        /// ��ѯ��ɫ��Ȩ
        /// </summary>
        /// <param name="type"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public List<RoleResourceMapping> QueryRoleResource(string type, string parentId)
        {
            return new RoleResourceProcess().QueryByTypeParentId(type, parentId);
        }

        /// <summary>
        /// ��ѯ��ɫ��Ȩ
        /// </summary>
        /// <param name="type"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public List<RoleResourceMapping> QueryByTypeRoleId(String type, String roleId)
        {
            return new RoleResourceProcess().QueryByTypeRoleId(type, roleId);
        }

        /// <summary>
        /// ɾ����ɫ��Ȩ
        /// </summary>
        /// <param name="roleResource"></param>
        public void DeleteRoleResource(RoleResourceMapping roleResource)
        {
            new RoleResourceProcess().DeleteRoleResource(roleResource);
        }

        /// <summary>
        /// ɾ����ɫ��Ȩ
        /// </summary>
        /// <param name="roleResourceList"></param>
        public void DeleteRoleResource(List<RoleResourceMapping> roleResourceList)
        {
            new RoleResourceProcess().DeleteRoleResource(roleResourceList);
        }

        /// <summary>
        /// ɾ����Դ��Ȩ��ͬʱɾ����ǰ��ɫ���ӽ�ɫ��ӵ�еĳ�Ա
        /// </summary>
        /// <param name="roleResource"></param>
        /// <param name="childRoleList"></param>
        public void DeleteRoleResource(RoleResourceMapping roleResource, List<Role> childRoleList)
        {
            new RoleResourceProcess().DeleteRoleResource(roleResource, childRoleList);
        }

        /// <summary>
        /// ���½�ɫ��Ȩ
        /// </summary>
        /// <param name="roleResource"></param>
        /// <returns></returns>
        public RoleResourceMapping UpdateRoleResource(RoleResourceMapping roleResource)
        {
            return new RoleResourceProcess().UpdateRoleResource(roleResource);
        }

        /// <summary>
        /// �ƶ�����
        /// </summary>
        /// <param name="roleResourceList"></param>
        public void MoveSequence(List<RoleResourceMapping> roleResourceList)
        {
            new RoleResourceProcess().MoveSequence(roleResourceList);
        }

        /// <summary>
        /// �����ɫ��֯������Ϣ
        /// </summary>
        /// <param name="SaveList"></param>
        /// <param name="OldList"></param>
        /// <returns></returns>
        public int SaveRoleOrg(List<RoleResourceMapping> SaveList, List<RoleResourceMapping> OldList)
        {
            return new RoleResourceProcess().SaveRoleOrg(SaveList, OldList);
        }

        /// <summary>
        /// ����������ɫ��ӵ�е�Ȩ��
        /// </summary>
        /// <param name="parentRole">������ɫ</param>
        /// <param name="currentRole">��ǰ��ɫ</param>
        /// <param name="pagetype">��Դ����</param>
        /// <returns></returns>
        public int CopyParentRes(Role parentRole, Role currentRole, string pagetype)
        {
            return new RoleResourceProcess().CopyParentRes(parentRole, currentRole, pagetype);
        }

        /// <summary>
        /// ����ͬ��ɫͬ������ORDER_NUMBUER
        /// </summary>
        /// <param name="parentRoleres"></param>
        public List<RoleResourceMapping> QueryByTypeParentRole(RoleResourceMapping parentRoleres)
        {
            return new RoleResourceProcess().QueryByTypeParentRole(parentRoleres);
        }

        /// <summary>
        /// ��ȡ����Դ���������еĽ�ɫ��Ϣ
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<String> QueryByResType(string type)
        {
            return new RoleResourceProcess().QueryByResType(type);
        }
        #endregion

        #region IAuthority ��Ա

        /// <summary>
        /// ������Ȩ��Ϣ
        /// </summary>
        /// <param name="currentUser"></param>
        /// <param name="roleOrgDictionary"></param>
        /// <returns></returns>
        public int SaveAuthorityRoleOrg(User currentUser, Dictionary<String, List<String>> roleOrgDictionary)
        {
            return new AuthorityProcess().SaveAuthorityRoleOrg(currentUser, roleOrgDictionary);
        }

        /// <summary>
        /// ��ѯ��Ȩ��Ϣ
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Dictionary<string, List<string>> QueryAuthorityRoleOrg(User user)
        {
            return new AuthorityProcess().QueryAuthorityRoleOrg(user);
        }

        /// <summary>
        /// ��ѯ��Ȩ��Ϣ
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Dictionary<String, List<HISFC.Models.Privilege.Organization>> GetAuthorityRoleOrg(User user)
        {
            return new AuthorityProcess().GetAuthorityRoleOrg(user);
        }

        /// <summary>
        /// ��ѯ��ɫ���û���ȫ���û�
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public List<User> QueryUsers(string roleId)
        {
            return new AuthorityProcess().QueryUsers(roleId);
        }

        /// <summary>
        /// ��ѯ��ɫ��ӵ�е�����Ȩ��
        /// </summary>
        /// <param name="roleIdList"></param>
        /// <returns></returns>
        public List<Priv> QueryPriv(List<String> roleIdList)
        {
            return new AuthorityProcess().QueryPriv(roleIdList);
        }

        /// <summary>
        /// �����û�Ȩ����Ȩ��Ϣ
        /// </summary>
        /// <param name="currentUser"></param>
        /// <param name="privOrgDictionary"></param>
        /// <returns></returns>
        public int SaveAuthorityPrivOrg(User currentUser, Dictionary<String, List<String>> privOrgDictionary)
        {
            return new AuthorityProcess().SaveAuthorityPrivOrg(currentUser, privOrgDictionary);
        }

        /// <summary>
        /// ��ѯ�û������е�Ȩ����Ϣ
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Dictionary<String, List<String>> QueryAuthorityPrivOrg(User user)
        {
            return new AuthorityProcess().QueryAuthorityPrivOrg(user);
        }

        /// <summary>
        /// ɾ���û���������Ȩ
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int DeleteAuthority(User user)
        {
            return new AuthorityProcess().DeleteAuthority(user);
        }

        /// <summary>
        /// ɾ��Ȩ��
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public int CancelAuthority(string userId, string roleId)
        {
            return new AuthorityProcess().CancelAuthority(userId, roleId);
        }
        #endregion
    }
}
