using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using FS.HISFC.BizLogic.Privilege.Model;


namespace FS.HISFC.BizLogic.Privilege
{
    /// <summary>
    /// [��������: ��Դҵ������]<br></br>
    /// [������:   �ſ���]<br></br>
    /// [����ʱ��: 2008-07-25]<br></br>
    /// <˵��>
    ///    ��ʵ�ֹ����е�һЩ��Ҫ˵��
    /// </˵��>
    /// </summary>
    public class ResourceProcess : DataBase
    {
        #region Privs ��Ա

        /// <summary>
        /// ����Ȩ��
        /// </summary>
        /// <param name="resourcesItem"></param>
        /// <returns></returns>
        public FS.HISFC.BizLogic.Privilege.Model.Resource SaveResourcesItem(FS.HISFC.BizLogic.Privilege.Model.Resource resourcesItem)
        {
           // FS.Framework.Accessory.Context.ContextProcess _appContext = new FS.Framework.Accessory.Context.ContextProcess();

            //�������˵�,����
            if (string.IsNullOrEmpty(resourcesItem.Id))
            {
                resourcesItem.Id = this.GetSequence("PRIV.SEQ_RESOURCESID");// _appContext.GetSequence("SEQ_RESOURCESID");
                resourcesItem.Order = int.Parse(this.GetSequence("PRIV.SEQ_RESOURCESORDER"));//;_appContext.GetSequence("SEQ_RESOURCESORDER")

                int i = Insert(resourcesItem);
                if (i <0) return null;
            }
            else//����
            {
                int i = Update(resourcesItem);
                if (i <0) return null;
            }

            return resourcesItem;

        }

        /// <summary>
        /// ��ӽ�ɫ��Ȩ
        /// </summary>
        /// <param name="newRoles"></param>
        /// <param name="newResources"></param>
        /// <returns></returns>
        public int AddRoleResourcesMap(List<string> newRoles, List<string> newResources)
        {
            foreach (string roleid in newRoles)
            {
                List<Resource> resources = QueryByRole(roleid);

                foreach (Resource currentRes in resources)
                {
                    if (!newResources.Contains(currentRes.Id))
                    {
                        if (DeleteMap(roleid, currentRes.Id) <= 0) return -1;
                    }
                }

                foreach (string resourcesId in newResources)
                {
                    if (!ResourcesContains(resources, resourcesId))
                    {
                        if (InsertMap(roleid, resourcesId) <= 0) return -1;
                    }
                }
            }

            return 0;
        }

        private static bool ResourcesContains(List<Resource> resources, string resroucesId)
        {
            foreach (Resource currentRes in resources)
            {
                if (currentRes.Id == resroucesId)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// ɾ����ɫ��Ȩ
        /// </summary>
        /// <param name="resourcesId"></param>
        /// <returns></returns>
        public int RemoveResourcesItem(string resourcesId)
        {
            return Delete(resourcesId);
        }

        /// <summary>
        /// ɾ����ɫ��Ȩ
        /// </summary>
        /// <param name="parentResId"></param>
        /// <param name="childReslist"></param>
        /// <returns></returns>
        public int RemoveResourcesItem(string parentResId, List<Resource> childReslist)
        {
            //using (DaoManager daoMge = new DaoManager())
            //{
                try
                {
                    //daoMge.BeginTransaction();
                    Delete(parentResId);
                    if (childReslist != null)
                    {
                        foreach (Resource childRes in childReslist)
                        {
                            Delete(childRes.Id);
                        }
                    }
                    //daoMge.CommitTransaction();
                    return 1 + childReslist.Count;
                }
                catch
                {
                    //daoMge.RollBackTransaction();
                    return 0;
                }

            //}
        }

        /// <summary>
        /// ɾ����ɫ��Ȩ
        /// </summary>
        /// <param name="res"></param>
        public void RemoveResourcesItem(Resource res)
        {
            //using (DaoManager daoMge = new DaoManager())
            //{
                try
                {
                    //daoMge.BeginTransaction();
                    Delete(res.Id);
                    RoleResourceProcess resourceProcess = new RoleResourceProcess();
                    List<RoleResourceMapping> currentRoleResList = resourceProcess.QueryRoleRes(res.Type, res.Id);
                    resourceProcess.DeleteRoleResource(currentRoleResList);
                    //daoMge.CommitTransaction();
                }
                catch
                {
                    //daoMge.RollBackTransaction();
                }

            //}
        }

        /// <summary>
        /// ��ѯ��Դ�б�
        /// </summary>
        /// <returns></returns>
        public List<FS.HISFC.BizLogic.Privilege.Model.Resource> QueryResources()
        {
            return Query();
        }

        /// <summary>
        /// ��ѯ��Դ�б�
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public List<FS.HISFC.BizLogic.Privilege.Model.Resource> QueryResourcesByRole(string roleId)
        {
            return QueryByRole(roleId);
        }

        /// <summary>
        /// ��ѯ��Դ�б�
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<FS.HISFC.BizLogic.Privilege.Model.Resource> QueryResourcesByUser(string userId)
        {
            return QueryByUserID(userId);
        }

        /// <summary>
        /// ��ѯ��Դ�б�
        /// </summary>
        /// <param name="typeRes"></param>
        /// <returns></returns>
        public List<FS.HISFC.BizLogic.Privilege.Model.Resource> QueryResourcesByType(string typeRes)
        {
            return QueryByType(typeRes);
        }

        /// <summary>
        /// ��ѯ��Դ�б�
        /// </summary>
        /// <param name="resourceId"></param>
        /// <returns></returns>
        public List<FS.HISFC.BizLogic.Privilege.Model.Resource> QueryResourcesById(string resourceId)
        {
            return QueryById(resourceId);
        }

        #endregion

        #region  ResourcesBizLogic

        /// <summary>
        /// ����һ����Դ��
        /// </summary>
        public int Insert(Resource resourcesItem)
        {

            //AbstractSqlModel _sql = new SqlModel("Security.Resources.Insert");
            //_sql["resourcesid"] = resourcesItem.Id;
            //_sql["resourcesname"] = resourcesItem.Name;
            //_sql["parentid"] = resourcesItem.ParentId;
            //_sql["layer"] = resourcesItem.Layer;
            //_sql["shortcut"] = resourcesItem.Shortcut;
            //_sql["icon"] = resourcesItem.Icon;
            //_sql["tooltip"] = resourcesItem.Tooltip;
            //_sql["param"] = resourcesItem.Param;
            //_sql["dllname"] = resourcesItem.DllName;
            //_sql["winname"] = resourcesItem.WinName;
            //_sql["controltype"] = resourcesItem.ControlType;
            //_sql["showtype"] = resourcesItem.ShowType;
            //_sql["sortid"] = resourcesItem.Order;
            //_sql["isenabled"] = FS.Framework.Util.NConvert.ToInt32(resourcesItem.Enabled).ToString();
            //_sql["description"] = "";
            //_sql["operid"] = resourcesItem.UserId;
            //_sql["operdate"] = resourcesItem.OperDate;
            //_sql["treedllname"] = resourcesItem.TreeDllName;
            //_sql["treename"] = resourcesItem.TreeName;
            //return base.ExecuteNonQuery(_sql);

            string[] args = new string[]{
            resourcesItem.Id,
            resourcesItem.Name,
            resourcesItem.ParentId,
            resourcesItem.Layer,
            resourcesItem.Shortcut,
            resourcesItem.Icon,
            resourcesItem.Tooltip,
            resourcesItem.Param,
            resourcesItem.DllName,
            resourcesItem.WinName,
            resourcesItem.ControlType,
            resourcesItem.ShowType,
            resourcesItem.Order.ToString(),
            FrameWork.Function.NConvert.ToInt32(resourcesItem.Enabled).ToString(),
            resourcesItem.Description,
            resourcesItem.UserId,
            resourcesItem.OperDate.ToString("yyyy-MM-dd HH:mm:ss"),
            resourcesItem.TreeDllName,
            resourcesItem.TreeName,
            resourcesItem.Hospital.ID
            };

            string sql = "";
            if (this.GetSQL("SECURITY.RESOURCES.INSERT", ref sql) == -1) return -1;
            try
            {
                sql = string.Format(sql, args);
            }
            catch (Exception ex) { this.Err = ex.Message; return -1; }
            if (this.ExecNoQuery(sql) <= 0) return -1;
            return 0;
           
        }

        /// <summary>
        /// ������Դ��Ȩ
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="ResId"></param>
        /// <returns></returns>
        public int InsertMap(string roleId, string ResId)
        {

            //AbstractSqlModel _sql = new SqlModel("Security.ResourcesRoleMap.Insert");
            //_sql["resourcesid"] = ResId;
            //_sql["roleid"] = roleId;
            //return base.ExecuteNonQuery(_sql);

            string[] args = new string[]{
            roleId,
            ResId,
            FS.FrameWork.Management.Connection.Hospital.ID
            };

            string sql = "";
            if (this.GetSQL("SECURITY.RESOURCESROLEMAP.INSERT", ref sql) == -1) return -1;
            try
            {
                sql = string.Format(sql, args);
            }
            catch (Exception ex) { this.Err = ex.Message; return -1; }
            if (this.ExecNoQuery(sql) <= 0) return -1;
            return 0;
        }

        /// <summary>
        /// ɾ��һ����Դ��
        /// </summary>
        public int Delete(string ResId)
        {
            //AbstractSqlModel _sql = new SqlModel("Security.Resources.Delete");
            //_sql["resourcesid"] = ResId;
            //return base.ExecuteNonQuery(_sql);
            string sql = "";
            if (this.GetSQL("SECURITY.RESOURCES.DELETE", ref sql) == -1) return -1;
            try
            {
                sql = string.Format(sql, ResId);
            }
            catch (Exception ex) { this.Err = ex.Message; return -1; }
            if (this.ExecNoQuery(sql) <= 0) return -1;
            return 0;

        }

        /// <summary>
        /// ɾ����Դ��Ȩ
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="ResId"></param>
        /// <returns></returns>
        public int DeleteMap(string roleId, string ResId)
        {
            //AbstractSqlModel _sql = new SqlModel("Security.ResourcesRoleMap.Delete");
            //_sql["roleid"] = roleId;
            //_sql["resourcesid"] = ResId;
            //return base.ExecuteNonQuery(_sql);

            string sql = "";
            if (this.GetSQL("SECURITY.RESOURCESROLEMAP.DELETE", ref sql) == -1) return -1;
            try
            {
                sql = string.Format(sql,roleId,ResId,FS.FrameWork.Management.Connection.Hospital.ID);
            }
            catch (Exception ex) { this.Err = ex.Message; return -1; }
            if (this.ExecNoQuery(sql) <= 0) return -1;
            return 0;
        }

        /// <summary>
        /// ������Դ��
        /// </summary>
        public int Update(Resource resourcesItem)
        {
            //AbstractSqlModel _sql = new SqlModel("Security.Resources.Update");
            //_sql["resourcesid"] = resourcesItem.Id;
            //_sql["resourcesname"] = resourcesItem.Name;
            //_sql["parentid"] = resourcesItem.ParentId;
            //_sql["layer"] = resourcesItem.Layer;
            //_sql["shortcut"] = resourcesItem.Shortcut;
            //_sql["icon"] = resourcesItem.Icon;
            //_sql["tooltip"] = resourcesItem.Tooltip;
            //_sql["param"] = resourcesItem.Param;
            //_sql["dllname"] = resourcesItem.DllName;
            //_sql["winname"] = resourcesItem.WinName;
            //_sql["controltype"] = resourcesItem.ControlType;
            //_sql["showtype"] = resourcesItem.ShowType;
            //_sql["sortid"] = resourcesItem.Order;
            //_sql["isenabled"] = FS.Framework.Util.NConvert.ToInt32(resourcesItem.Enabled).ToString();
            //_sql["description"] = "";
            //_sql["operid"] = resourcesItem.UserId;
            //_sql["operdate"] = resourcesItem.OperDate;
            //_sql["treedllname"] = resourcesItem.TreeDllName;
            //_sql["treename"] = resourcesItem.TreeName;
            //return base.ExecuteNonQuery(_sql);

            string[] args = new string[]{
            resourcesItem.Id,
            resourcesItem.Name,
            resourcesItem.ParentId,
            resourcesItem.Layer,
            resourcesItem.Shortcut,
            resourcesItem.Icon,
            resourcesItem.Tooltip,
            resourcesItem.Param,
            resourcesItem.DllName,
            resourcesItem.WinName,
            resourcesItem.ControlType,
            resourcesItem.ShowType,
            resourcesItem.Order.ToString(),
            FrameWork.Function.NConvert.ToInt32(resourcesItem.Enabled).ToString(),
            resourcesItem.Description,
            resourcesItem.UserId,
            resourcesItem.OperDate.ToString(),
            resourcesItem.TreeDllName,
            resourcesItem.TreeName,
            resourcesItem.Hospital.ID
            };

            string sql = "";
            if (this.GetSQL("SECURITY.RESOURCES.UPDATE", ref sql) == -1) return -1;
            try
            {
                sql = string.Format(sql, args);
            }
            catch (Exception ex) { this.Err = ex.Message; return -1; }
            if (this.ExecNoQuery(sql) <= 0) return -1;
            return 0;

        }

        /// <summary>
        /// ��ѯȫ����Դ��
        /// </summary>
        public List<Resource> Query()
        {
            //AbstractSqlModel sqlMode = new SqlModel("Security.Resources.Query");
            //return GetData(sqlMode);

            string sql = "";
            if (this.GetSQL("SECURITY.RESOURCES.QUERY", ref sql) == -1) return null;
            try
            {
                sql = string.Format(sql,this.Hospital.ID);
            }
            catch (Exception ex) { this.Err = ex.Message; return null; }
            return GetData(sql);
        }

        /// <summary>
        /// ��ѯ��ɫӵ�е���Դ��
        /// </summary>
        public List<Resource> QueryByRole(string roleId)
        {
            //AbstractSqlModel sqlMode = new SqlModel("Security.Resources.QueryByRole");
            //sqlMode["roleid"] = roleId;
            //return GetData(sqlMode);

            string sql = "";
            if (this.GetSQL("SECURITY.RESOURCES.QUERYBYROLE", ref sql) == -1) return null;
            try
            {
                sql = string.Format(sql, roleId,this.Hospital.ID);
            }
            catch (Exception ex) { this.Err = ex.Message; return null; }
            return GetData(sql);
        }

        /// <summary>
        /// ��ѯ��Դ��
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<Resource> QueryByUserID(string userId)
        {
            //AbstractSqlModel sqlMode = new SqlModel("Security.Resources.QueryByUser");
            //sqlMode["userid"] = userId;

            string sql = "";
            if (this.GetSQL("SECURITY.RESOURCES.QUERYBYUSER", ref sql) == -1) return null;
            try
            {
                sql = string.Format(sql, userId,this.Hospital.ID);
            }
            catch (Exception ex) { this.Err = ex.Message; return null; }
            return GetData(sql);
        }

        /// <summary>
        /// ��ѯ��Դ��
        /// </summary>
        /// <param name="typeRes"></param>
        /// <returns></returns>
        public List<Resource> QueryByType(string typeRes)
        {
            //AbstractSqlModel sqlMode = new SqlModel("Security.Resources.QueryByType");
            //sqlMode["controltype"] = typeRes;
            //return GetData(sqlMode);
            string sql = "";
            if (this.GetSQL("SECURITY.RESOURCES.QUERYBYTYPE", ref sql) == -1) return null;
            try
            {
                sql = string.Format(sql, typeRes,this.Hospital.ID);
            }
            catch (Exception ex) { this.Err = ex.Message; return null; }
            return GetData(sql);

        }

        /// <summary>
        /// ��ѯ��Դ��
        /// </summary>
        /// <param name="typeRes"></param>
        /// <returns></returns>
        public List<Resource> QueryNoneRoot()
        {
            //AbstractSqlModel sqlMode = new SqlModel("Security.Resources.QueryByType");
            //sqlMode["controltype"] = typeRes;
            //return GetData(sqlMode);
            string sql = "";
            if (this.GetSQL("SECURITY.RESOURCES.QUERYNONEROOT", ref sql) == -1) return null;
            return GetData(sql);

        }

        /// <summary>
        /// ��ѯ��Դ��
        /// </summary>
        /// <param name="resourceId"></param>
        /// <returns></returns>
        public List<Resource> QueryById(string resourceId)
        {
            //AbstractSqlModel sqlMode = new SqlModel("Security.Resources.QueryById");
            //sqlMode["resourcesid"] = resourceId;
            //return GetData(sqlMode);
            string sql = "";
            if (this.GetSQL("SECURITY.RESOURCES.QUERYBYID", ref sql) == -1) return null;
            try
            {
                sql = string.Format(sql, resourceId,this.Hospital.ID);
            }
            catch (Exception ex) { this.Err = ex.Message; return null; }
            return GetData(sql);
        }


        /// <summary>
        /// ��ѯ��Դ��
        /// </summary>
        /// <param name="resourceId"></param>
        /// <returns></returns>
        public List<Resource> QueryResource(string resourceIds)
        {
            //AbstractSqlModel sqlMode = new SqlModel("Security.Resources.QueryById");
            //sqlMode["resourcesid"] = resourceId;
            //return GetData(sqlMode);
            string sql = "";
            if (this.GetSQL("SOC.SECURITY.RESOURCES.QUERYBYPARENTID", ref sql) == -1)
            {
                sql = @"
                        SELECT  
	                        RESOURCESID,                            --��ԴID
	                        RESOURCESNAME,                          --��Դ����
	                        PARENTID,                               --������Դ����
	                        LAYER,                                  --����
	                        SHORTCUT,                               --��ݼ�
	                        ICON,                                   --ͼ��
	                        TOOLTIP,                                --������ʾ
	                        PARAM,                                  --�������
	                        DLLNAME,                                --����DLL����
	                        WINNAME,                                --���ÿؼ�����,ȫ����
	                        CONTROLTYPE,                            --�ؼ�����
	                        SHOWTYPE,                               --��ʾ����
	                        SORTID,                                 --��ʾ˳��
	                        ISENABLED,                              --�Ƿ����
	                        DESCRIPTION,                            --��ע
	                        OPERID,                                 --������ID
	                        OPERDATE,                               --����ʱ��
	                        TREEDLLNAME,                            --��DLL����
	                        TREENAME,                                --������
                          HOS_CODE                                 --ҽԺ����
                        FROM PRIV_COM_RESOURCES
                         WHERE RESOURCESID in ({0}) and	HOS_CODE='{1}'
                       ";
            }
            try
            {
                sql = string.Format(sql, resourceIds, this.Hospital.ID);
            }
            catch (Exception ex) { this.Err = ex.Message; return null; }
            return GetData(sql);
        }


        /// <summary>
        /// ��ѯ��Դ��
        /// </summary>
        /// <param name="resourceId"></param>
        /// <returns></returns>
        public List<Resource> QueryByTypeParentId(string type,string parentId)
        {
            //AbstractSqlModel sqlMode = new SqlModel("Security.Resources.QueryById");
            //sqlMode["resourcesid"] = resourceId;
            //return GetData(sqlMode);
            string sql = "";
            if (this.GetSQL("SECURITY.RESOURCES.QUERYBYTYPEPARENTID", ref sql) == -1) return null;
            try
            {
                sql = string.Format(sql, type, parentId,this.Hospital.ID);
            }
            catch (Exception ex) { this.Err = ex.Message; return null; }
            return GetData(sql);
        }

        private List<Resource> GetData(string sql)
        {
            List<Resource> _list = new List<Resource>();
            Resource resources = null;
            
            //DbDataReader _reader = base.ExecuteReader(sqlMode);
            if (this.ExecQuery(sql) < 0) return null;
            while (this.Reader.Read())
            {
                resources = new Resource();
                resources.Id = Reader[0].ToString();
                resources.Name = Reader[1].ToString();
                resources.ParentId = Reader[2].ToString();
                resources.Layer = Reader[3].ToString();
                resources.Shortcut = Reader[4].ToString();
                resources.Icon = Reader[5].ToString();
                resources.Tooltip = Reader[6].ToString();
                resources.Param = Reader[7].ToString();
                resources.DllName = Reader[8].ToString();
                resources.WinName = Reader[9].ToString();
                resources.ControlType = Reader[10].ToString();
                resources.ShowType = Reader[11].ToString();
                resources.Order = FrameWork.Function.NConvert.ToInt32(Reader[12].ToString());
                resources.Enabled = FrameWork.Function.NConvert.ToBoolean(Reader[13]);
                resources.Description = Reader[14].ToString();
                resources.UserId = Reader[15].ToString();
                if (!Reader.IsDBNull(16))
                    resources.OperDate = FrameWork.Function.NConvert.ToDateTime(Reader[16].ToString());
                resources.TreeDllName = Reader[17].ToString();
                resources.TreeName = Reader[18].ToString();
                resources.Hospital.ID = Reader[19].ToString();
                _list.Add(resources);
            }
            Reader.Close();

            return _list;
        }

        #endregion

    }
}
