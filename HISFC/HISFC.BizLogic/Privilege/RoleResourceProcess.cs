using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using FS.HISFC.BizLogic.Privilege.Model;



namespace FS.HISFC.BizLogic.Privilege
{
    /// <summary>
    /// [��������: ��ɫ��Ȩ��Դҵ��������]<br></br>
    /// [������:   �ſ���]<br></br>
    /// [����ʱ��: 2008-07-23]<br></br>
    /// <˵��>
    ///    
    /// </˵��>
    ///</summary>
    public class RoleResourceProcess : DataBase
    {
        #region RoleResource ��Ա

        /// <summary>
        /// �ƶ�˳��
        /// </summary>
        /// <param name="roleResourceList"></param>
        public void MoveSequence(List<RoleResourceMapping> roleResourceList)
        {
            //using (DaoManager daoMgr = new DaoManager())
            //{
                try
                {
                    //daoMgr.BeginTransaction();
                    
                    decimal changInt = new decimal();
                    changInt = roleResourceList[0].OrderNumber;
                    roleResourceList[0].OrderNumber = roleResourceList[1].OrderNumber;
                    roleResourceList[1].OrderNumber = changInt;
                    Update(roleResourceList);


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
        /// ����
        /// </summary>
        /// <param name="roleResourceMapping"></param>
        public void InsertRoleResource(RoleResourceMapping roleResourceMapping)
        {
            Insert(roleResourceMapping);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="roleResourceList"></param>
        public void DeleteRoleResource(List<RoleResourceMapping> roleResourceList)
        {
            //using (DaoManager daoMgr = new DaoManager())
            //{
                //try
                //{
                    //daoMgr.BeginTransaction();
                    foreach (RoleResourceMapping roleRes in roleResourceList)
                    {
                        DeleteRoleResource(roleRes);
                    }

                    //daoMgr.CommitTransaction();
                //}
                //catch (Exception ex)
                //{

                //    //daoMgr.RollBackTransaction();
                //    throw ex;
                //}

            //}

        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="roleResource"></param>
        public void DeleteRoleResource(RoleResourceMapping roleResource)
        {
            //using (DaoManager daoMgr = new DaoManager())
            //{
                //try
                //{
                    //daoMgr.BeginTransaction();
                  
                    Delete(roleResource);
                    //ɾ���˵���Ȩʱ�����䴰�ڵ�������ϢҲͬʱɾ��// {EA8B2DAB-A171-49df-8C3F-087EB627A032}

                    //Framework.Facade.ConfigurationManager.Delete(roleResource.Id, "FormSetting");
                    //���ýڵ������

                    List<RoleResourceMapping> currentRoleResList = QueryByTypeParentId(roleResource.Type, roleResource.ParentId);
                    if (currentRoleResList != null)
                    {
                        foreach (RoleResourceMapping currentRoleRes in currentRoleResList)
                        {
                            if (currentRoleRes.OrderNumber > roleResource.OrderNumber)
                            {
                                currentRoleRes.OrderNumber = currentRoleRes.OrderNumber - 1;
                                UpdateRoleResource(currentRoleRes);
                            }
                        }
                    }

                    //daoMgr.CommitTransaction();
                //}
                //catch (Exception ex)
                //{
                //    //daoMgr.RollBackTransaction();
                //    throw ex;
                //}

            //}
        }

        /// <summary>
        /// ɾ����Դ��Ȩ��ͬʱɾ����ǰ��ɫ���ӽ�ɫ��ӵ�еĳ�Ա
        /// </summary>
        /// <param name="roleResource"></param>
        /// <param name="childRoleList"></param>
        public void DeleteRoleResource(RoleResourceMapping roleResource, List<Role> childRoleList)
        {
            //using (DaoManager daoMgr = new DaoManager())
            //{
                //try
                //{
                    //daoMgr.BeginTransaction();

                    DeleteRoleResource(roleResource);
                    foreach (Role role in childRoleList)
                    {
                        List<RoleResourceMapping> selectList = QueryByTypeResRole(roleResource.Type, roleResource.Resource.Id, role.ID);
                        if (selectList != null && selectList.Count != 0)
                        {
                            DeleteRoleResource(selectList[0]);
                        }
                    }

                    //daoMgr.CommitTransaction();
                //}
                //catch (Exception ex)
                //{

                //    //daoMgr.RollBackTransaction();
                //    throw ex;
                //}

            //}


        }

        /// <summary>
        /// ��ѯ
        /// </summary>
        /// <param name="type"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public List<RoleResourceMapping> QueryRoleResource(string type, string parentId)
        {
            return QueryByTypeParentId(type, parentId);
        }

        /// <summary>
        /// ��ѯ
        /// </summary>
        /// <param name="type"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public List<RoleResourceMapping> QueryByTypeRoleId(String type, String roleId)
        {
            return QueryByTypeRole(type, roleId);
        }

        /// <summary>
        /// ����ͬ��ɫͬ������ORDER_NUMBUER
        /// </summary>
        /// <param name="roleRes"></param>
        /// <returns></returns>
        public List<RoleResourceMapping> QueryByTypeParentRole(RoleResourceMapping roleRes)
        {
            return Query(roleRes);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="roleResourceMapping"></param>
        /// <returns></returns>
        public RoleResourceMapping UpdateRoleResource(RoleResourceMapping roleResourceMapping)
        {
            return Update(roleResourceMapping);
        }

        /// <summary>
        /// ��ѯ
        /// </summary>
        /// <param name="type"></param>
        /// <param name="resId"></param>
        /// <returns></returns>
        public List<RoleResourceMapping> QueryRoleRes(string type, string resId)
        {
            return QueryByTypeRes(type, resId);
        }

        /// <summary>
        /// ��ѯ
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public List<RoleResourceMapping> QueryByRole(string roleId)
        {
            return QueryByRoleId(roleId);
        }

        /// <summary>
        /// ��ȡ����Դ���������еĽ�ɫ��Ϣ
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<String> QueryByResType(string type)
        {
            List<String> roleIds = new List<string>();
            //AbstractSqlModel sqlModel = new SqlModel("Privilege.RoleResourceMapping.QueryRoleId");
            //sqlModel["type"] = type;
            //DbDataReader reader = this.ExecuteReader(sqlModel);
            string sql = "";
            if (this.GetSQL("PRIVILEGE.ROLERESOURCEMAPPING.QUERYROLEID", ref sql) == -1) return null;
            try
            {
                sql = string.Format(sql, type,FS.FrameWork.Management.Connection.Hospital.ID);
            }
            catch (Exception ex) { this.Err = ex.Message; return null; }
            if (this.ExecQuery(sql) < 0) return null;
            while (this.Reader.Read())
            {
                roleIds.Add(Reader[0].ToString());
            }
            return roleIds;
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="SaveList"></param>
        /// <param name="OldList"></param>
        /// <returns></returns>
        public int SaveRoleOrg(List<RoleResourceMapping> SaveList, List<RoleResourceMapping> OldList)
        {
            //using (DaoManager daoMgr = new DaoManager())
            //{
                try
                {
                    //daoMgr.BeginTransaction();

                    if (OldList != null || OldList.Count != 0)
                    {
                        Delete(OldList);
                    }
                    Insert(SaveList);
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

        /// <summary>
        /// ����������ɫ��ӵ�е�Ȩ��
        /// </summary>
        /// <param name="parentRole">������ɫ</param>
        /// <param name="pagetype">��Դ����</param>
        /// <param name="currentRole">��ǰ��ɫ</param>
        /// <returns></returns>
        public int CopyParentRes(Role parentRole, Role currentRole, string pagetype)
        {
            //using (DaoManager dao = new DaoManager())
            //{
                //try
                //{
                    //dao.BeginTransaction();
            List<RoleResourceMapping> roleParentResList = QueryByTypeRole(pagetype, parentRole.ID);
                    if (roleParentResList != null && roleParentResList.Count != 0)
                    {
                        ChangeParentList(roleParentResList, currentRole);
                        Insert(roleParentResList);


                    }

                    //dao.CommitTransaction();
                    return 1;

                //}
                //catch (Exception ex)
                //{
                //    //dao.RollBackTransaction();
                //    throw ex;
                //}

            //}

        }

        private List<RoleResourceMapping> ChangeParentList(List<RoleResourceMapping> parentList, Role currentRole)
        {
            //�¾ɶ��ձ�
            Dictionary<String, RoleResourceMapping> compareDictionary = new Dictionary<string, RoleResourceMapping>();

            foreach (RoleResourceMapping newRoleRes in parentList)
            {
                string oldId = newRoleRes.Id;
                //newRoleRes.Id = FS.Framework.Facade.Context.GetSequence("seq_role_resource ");
                newRoleRes.Id =this.GetSequence("PRIV.SEQ_ROLE_RESOURCE");
                newRoleRes.Role = currentRole as Role;

                ////copy�����˵���Ȩ��ʱ������Copy�������ڵ�����5FF1854B-8DBA-4e0e-B66A-F34ED797AAC0
                //ConfigurationManagerEntity newSetting = Framework.Facade.ConfigurationManager.Get(oldId, "FormSetting");
                //if (newSetting != null)
                //{
                //    newSetting.Id = newRoleRes.Id;
                //    newSetting.OperDate = Framework.Facade.Context.GetServerDateTime();
                //    Framework.Facade.ConfigurationManager.Save(newSetting);
                
                //}

                
                compareDictionary.Add(oldId, newRoleRes);
            }

            //���ĸ����ڵ�
            foreach (RoleResourceMapping newRoleRes in parentList)
            {
                if (newRoleRes.ParentId != "root")
                {
                    newRoleRes.ParentId = compareDictionary[newRoleRes.ParentId].Id;
                }
            }

            return parentList;
        }

        #endregion


        #region RoleResourceMapping BizLogic

        private int Insert(RoleResourceMapping info)
        {
            //AbstractSqlModel sqlInsert = new SqlModel("Privilege.RoleResourceMapping.Insert");
            //this.SetInfo(sqlInsert, info);
            //base.ExecuteNonQuery(sqlInsert);

            string[] args = new string[]{
            info.Id,
            info.Name,
            info.ParentId,
            info.Role.ID,
            info.Resource.Id,
            info.Type,
            info.OrderNumber.ToString(),
            info.Parameter,
            info.ValidState,
            info.OperCode,
            info.OperDate.ToString(),
            info.Icon,
            FS.FrameWork.Management.Connection.Hospital.ID,
            info.EmplSql,
            info.DeptSql
            };

            string sql = "";
            if (this.GetSQL("PRIVILEGE.ROLERESOURCEMAPPING.INSERT", ref sql) == -1) return -1;
            //try
            //{
            //    sql = string.Format(sql, args);
            //}
            //catch (Exception ex) { this.Err = ex.Message; return -1; }
            if (this.ExecNoQuery(sql,args) <= 0) return -1;
            return 0;
        }

        private int Insert(List<RoleResourceMapping> infoList)
        {
            try
            {

                foreach (RoleResourceMapping info in infoList)
                {
                    this.Insert(info);
                }

                return 0;
            }
            catch (Exception e)
            {
                return -1;
            }

        }

        private List<RoleResourceMapping> QueryAll()
        {
            //AbstractSqlModel sqlMode = new SqlModel("Privilege.RoleResourceMapping.QueryAll");
            string sql = "";
            if (this.GetSQL("PRIVILEGE.ROLERESOURCEMAPPING.QUERYALL", ref sql) == -1) return null;
            try
            {
                sql = string.Format(sql);
            }
            catch (Exception ex) { this.Err = ex.Message; return null; }
            return this.QueryList(sql);
        }

        private List<RoleResourceMapping> QueryByType(String type)
        {
            //AbstractSqlModel sqlModel = new SqlModel("Privilege.RoleResourceMapping.QueryByType");
            //sqlModel["type"] = type;
            string sql = "";
            if (this.GetSQL("PRIVILEGE.ROLERESOURCEMAPPING.QUERYBYTYPE", ref sql) == -1) return null;
            try
            {
                sql = string.Format(sql, type,FS.FrameWork.Management.Connection.Hospital.ID);
            }
            catch (Exception ex) { this.Err = ex.Message; return null; }
            return this.QueryList(sql);
        }


        public List<Resource> QueryByParentId(String parentId)
        {
            List<Resource> resourceList=new List<Resource>();
            string sql = "";
            if (this.GetSQL("PRIVILEGE.ROLERESOURCEMAPPING.QUERYBYPARENTID", ref sql) == -1) return null;
            try
            {
                sql = string.Format(sql, parentId);
            }
            catch (Exception ex) { this.Err = ex.Message; return null; }
              if (this.ExecQuery(sql) < 0) return null;
              while (this.Reader.Read())
              {
                  Resource info = new Resource();

                  info.Id = Reader[0].ToString();	//id
                  info.Name = Reader[1].ToString();	//����
                  resourceList.Add(info);
              }
              return resourceList;
        }
        /// <summary>
        /// ��ѯ
        /// </summary>
        /// <param name="type"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public List<RoleResourceMapping> QueryByTypeParentId(String type, String parentId)
        {
            //AbstractSqlModel sqlModel = new SqlModel("Privilege.RoleResourceMapping.QueryByTypeParentId");
            //sqlModel["type"] = type;
            //sqlModel["parent_id"] = parentId;
            //return this.QueryList(sqlModel);
            string sql = "";
            if (this.GetSQL("PRIVILEGE.ROLERESOURCEMAPPING.QUERYBYTYPEPARENTID", ref sql) == -1) return null;
            try
            {
                sql = string.Format(sql, type, parentId,FS.FrameWork.Management.Connection.Hospital.ID);
            }
            catch (Exception ex) { this.Err = ex.Message; return null; }
            return this.QueryList(sql);
        }

        private List<RoleResourceMapping> QueryByTypeRole(String type, String roleId)
        {
            //AbstractSqlModel sqlModel = new SqlModel("Privilege.RoleResourceMapping.QueryByTypeRoleId");
            //sqlModel["type"] = type;
            //sqlModel["role_id"] = roleId;
            //return this.QueryList(sqlModel);
            string sql = "";
            if (this.GetSQL("PRIVILEGE.ROLERESOURCEMAPPING.QUERYBYTYPEROLEID", ref sql) == -1) return null;
            try
            {
                sql = string.Format(sql, type, roleId,FS.FrameWork.Management.Connection.Hospital.ID);
            }
            catch (Exception ex) { this.Err = ex.Message; return null; }
            return this.QueryList(sql);
        }
        /// <summary>
        /// ����ͬ��ɫͬ������ORDER_NUMBUER
        /// </summary>
        /// <param name="roleRes"></param>
        /// <returns></returns>
        private List<RoleResourceMapping> Query(RoleResourceMapping roleRes)
        {
            //AbstractSqlModel sqlModel = new SqlModel("Privilege.RoleResourceMapping.QueryByTypeParentRole");
            //sqlModel["type"] = roleRes.Type;
            //sqlModel["parent_id"] = roleRes.Id;
            //sqlModel["role_id"] = roleRes.Role.Id;
            //return this.QueryList(sqlModel);

            string[] args = new string[] { 
            roleRes.Role.ID,
            roleRes.Id,
            roleRes.Type,
            FS.FrameWork.Management.Connection.Hospital.ID
            };
            string sql = "";
            if (this.GetSQL("PRIVILEGE.ROLERESOURCEMAPPING.QUERYBYTYPEPARENTROLE", ref sql) == -1) return null;
            try
            {
                sql = string.Format(sql, args);
            }
            catch (Exception ex) { this.Err = ex.Message; return null; }
            return this.QueryList(sql);
        }

        private List<RoleResourceMapping> QueryByTypeRes(String type, String ResId)
        {
            //AbstractSqlModel sqlModel = new SqlModel("Privilege.RoleResourceMapping.QueryByTypeResId");
            //sqlModel["type"] = type;
            //sqlModel["resource_id"] = ResId;
            //return this.QueryList(sqlModel);

            string[] args = new string[] { 
            type,
            ResId,
            FS.FrameWork.Management.Connection.Hospital.ID
            };
            string sql = "";
            if (this.GetSQL("PRIVILEGE.ROLERESOURCEMAPPING.QUERYBYTYPERESID", ref sql) == -1) return null;
            try
            {
                sql = string.Format(sql, args);
            }
            catch (Exception ex) { this.Err = ex.Message; return null; }
            return this.QueryList(sql);
        }

        private List<RoleResourceMapping> QueryByRoleId(String roleId)
        {
            //AbstractSqlModel sqlModel = new SqlModel("Privilege.RoleResourceMapping.QueryByRoleId");
            //sqlModel["role_id"] = roleId;
            //return this.QueryList(sqlModel);

            string sql = "";
            if (this.GetSQL("PRIVILEGE.ROLERESOURCEMAPPING.QUERYBYROLEID", ref sql) == -1) return null;
            try
            {
                sql = string.Format(sql, roleId,FS.FrameWork.Management.Connection.Hospital.ID);
            }
            catch (Exception ex) { this.Err = ex.Message; return null; }
            return this.QueryList(sql);
        }

        private int Delete(RoleResourceMapping info)
        {
            //AbstractSqlModel sqlMode = new SqlModel("Privilege.RoleResourceMapping.delete");
            //sqlMode["id"] = info.Id;
            //if (base.ExecuteNonQuery(sqlMode) == 0)
            //{
            //    new Exception();
            //}
           
            string sql = "";
            if (this.GetSQL("PRIVILEGE.ROLERESOURCEMAPPING.DELETE", ref sql) == -1) return -1;
            try
            {
                sql = string.Format(sql, info.Id,FS.FrameWork.Management.Connection.Hospital.ID);
            }
            catch (Exception ex) { this.Err = ex.Message; return -1; }
            if (this.ExecNoQuery(sql) <= 0) return -1;
            return 0;
        }

        private List<RoleResourceMapping> QueryByTypeResRole(String type, String resId, String roleId)
        {
            //AbstractSqlModel sqlModel = new SqlModel("Privilege.RoleResourceMapping.QueryByTypeResRole");
            //sqlModel["type"] = type;
            //sqlModel["resource_id"] = resId;
            //sqlModel["role_id"] = roleId;
            //return this.QueryList(sqlModel);
            string[] args = new string[] { 
            roleId,
            resId,
            type,
            FS.FrameWork.Management.Connection.Hospital.ID
            };
            string sql = "";
            if (this.GetSQL("PRIVILEGE.ROLERESOURCEMAPPING.QUERYBYTYPERESROLE", ref sql) == -1) return null;
            try
            {
                sql = string.Format(sql, args);
            }
            catch (Exception ex) { this.Err = ex.Message; return null; }
            return this.QueryList(sql);

        }

        private int Delete(List<RoleResourceMapping> infoList)
        {
            try
            {


                foreach (RoleResourceMapping info in infoList)
                {
                    this.Delete(info);
                }


                return 0;
            }
            catch (Exception e)
            {

                return -1;
            }

        }

        private RoleResourceMapping Update(RoleResourceMapping info)
        {
            //AbstractSqlModel sqlModel = new SqlModel("Privilege.RoleResourceMapping.update");
            //this.SetInfo(sqlModel, info);
            //base.ExecuteNonQuery(sqlModel);

            string[] args = new string[]{
            info.Id,
            info.Name,
            info.ParentId,
            info.Role.ID,
            info.Resource.Id,
            info.Type,
            info.OrderNumber.ToString(),
            info.Parameter,
            info.ValidState,
            info.OperCode,
            info.OperDate.ToString(),
            info.Icon,
            FS.FrameWork.Management.Connection.Hospital.ID,
            info.EmplSql,
            info.DeptSql
            };

            string sql = "";
            if (this.GetSQL("PRIVILEGE.ROLERESOURCEMAPPING.UPDATE", ref sql) == -1) return null;
            //try
            //{
            //    sql = string.Format(sql, args);
            //}
            //catch (Exception ex) { this.Err = ex.Message; return null; }
            if (this.ExecNoQuery(sql,args) <= 0) return null;

            return info;
        }

        private int Update(List<RoleResourceMapping> infoList)
        {
            try
            {


                foreach (RoleResourceMapping info in infoList)
                {
                    this.Update(info);
                }


                return 0;
            }
            catch (Exception e)
            {

                return -1;
            }

        }

        List<Resource> newResList = new List<Resource>();

        private List<RoleResourceMapping> QueryList(string sql)
        {
            List<RoleResourceMapping> infoList = new List<RoleResourceMapping>();
            //using (DbDataReader dbReader = base.ExecuteReader(sqlModel))
            //{
            string ids = "";
            if (this.ExecQuery(sql) < 0) return null;
            while (this.Reader.Read())
            {
                RoleResourceMapping info = new RoleResourceMapping();

                info.Id = Reader[0].ToString();	//id
                info.Name = Reader[1].ToString();	//����
                info.ParentId = Reader[2].ToString();	//������ɫid

                info.Role.ID = Reader[3].ToString();	//��ɫid
                info.Resource.Id = Reader[4].ToString();
                if (Reader[4] != null)
                {
                    if (Reader[4].ToString() != "")
                    {
                        ids += "'" + Reader[4].ToString() + "',";
                    }
                    //ResourceProcess resProcess = new ResourceProcess();
                    //newResList = resProcess.QueryById(Reader[4].ToString());

                    //if (newResList.Count != 0)
                    //{
                    //    info.Resource = newResList[0];	//��Դid
                    //}
                    //else
                    //{
                    //    if (Reader[4].ToString() != string.Empty)
                    //    {
                    //        info.Resource.Id = Reader[4].ToString();
                    //    }
                    //    else
                    //    {
                    //        info.Resource.Id = null;
                    //    }
                    //}
                }
                else
                {
                    info.Resource = new Resource();
                }
                info.Type = Reader[5].ToString();	//����
                info.OrderNumber = FrameWork.Function.NConvert.ToDecimal(Reader[6].ToString());	//���������
                info.Parameter = Reader[7].ToString();	//����
                info.ValidState = Reader[8].ToString();	//��Ч�Ա�־ 1 ���� 0 ͣ��
                info.OperCode = Reader[9].ToString();	//����Ա
                info.OperDate = FrameWork.Function.NConvert.ToDateTime(Reader[10].ToString());	//����ʱ��
                info.Icon = Reader[11].ToString();
                info.Hospital.ID = Reader[12].ToString();

                if (Reader.FieldCount >= 14)
                    info.EmplSql = Reader[13].ToString();

                if (Reader.FieldCount >= 15)
                    info.DeptSql = Reader[14].ToString();
                infoList.Add(info);
            }

            Reader.Close();

            //}
            ResourceProcess resProcess = new ResourceProcess();
            if (!string.IsNullOrEmpty(ids) && !string.IsNullOrEmpty(ids.TrimEnd(',')))
            {
                List<Resource> listR = resProcess.QueryResource(ids.TrimEnd(','));
                if (listR != null)
                {
                    foreach (Resource r in listR)
                    {
                        foreach (RoleResourceMapping rm in infoList)
                        {
                            if (rm.Resource.Id == r.Id)
                            {
                                rm.Resource = r;
                            }
                        }
                    }
                }
            }
            return infoList;
        }

        //private void SetInfo(AbstractSqlModel sqlModel, RoleResourceMapping info)
        //{
        //    //sqlModel["ID"] = info.Id;//id;
        //    //sqlModel["NAME"] = info.Name;//����;
        //    //sqlModel["PARENT_ID"] = info.ParentId;//������ɫid;
        //    //sqlModel["ROLE_ID"] = info.Role.Id;//��ɫid;
        //    //sqlModel["RESOURCE_ID"] = info.Resource.Id;//��Դid;
        //    //sqlModel["TYPE"] = info.Type;//����;
        //    //sqlModel["ORDER_NUMBER"] = info.OrderNumber;//���������;
        //    //sqlModel["PARAMETER"] = info.Parameter;//����;
        //    //sqlModel["VALID_STATE"] = info.ValidState;//��Ч�Ա�־ 1 ���� 0 ͣ��;
        //    //sqlModel["OPER_CODE"] = info.OperCode;//����Ա;
        //    //sqlModel["OPER_DATE"] = info.OperDate;//����ʱ��;

        //}
        #endregion


    }
}
