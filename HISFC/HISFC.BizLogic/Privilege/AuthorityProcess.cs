using System;
using System.Collections.Generic;
using System.Text;
using FS.HISFC.BizLogic.Privilege.Model;
using FS.HISFC.BizLogic.Privilege.Service;


namespace FS.HISFC.BizLogic.Privilege
{
    /// <summary>
    /// 
    /// </summary>
    public class AuthorityProcess : FrameWork.Management.Database
    {
        AuthorityLogic authorityLogic = new AuthorityLogic();
        /// <summary>
        /// �����û�����Ȩ��Ϣ����ӣ��޸ģ�
        /// </summary>
        /// <param name="currentUser"></param>
        /// <param name="roleOrgDictionary"></param>
        /// <returns></returns>
        public int SaveAuthorityRoleOrg(User currentUser, Dictionary<String, List<String>> roleOrgDictionary)
        {
            List<String[]> roleOrgList = new List<String[]>();//0,id;1,useId;2,roleId;3,orgId;
            //�޸�ʱ��Ҫ�ҵ�����������������Ϣ�Ƿ��Ѿ����ڣ�
            List<String[]> judgeRoleOrgList = new List<string[]>();
            UserLogic userLogic = new UserLogic();
            //using (DaoManager daoMgr = new DaoManager())
            //{
            try
            {
                //daoMgr.BeginTransaction();

                if (userLogic.Update(currentUser) == -1)
                {
                    //currentUser.Id = FS.Framework.Facade.Context.GetSequence("Seq_UserId");
                    currentUser.Id = this.GetSequence("PRIV.SEQ_USERID");
                    if (userLogic.Insert(currentUser) == -1) 
                    {
                        return -1;
                    }
                }

                //�����ݿ��еõ����и��û�����Ϣ,�Ƚ��µ������ֵ��е�Role�Ƿ�ͱ��е�Role������һ������һ����ɾ����
                judgeRoleOrgList = authorityLogic.Query(currentUser.Id);

                foreach (String role in roleOrgDictionary.Keys)
                {
                    //����������֯�ṹʱ
                    if (roleOrgDictionary[role].Count == 0)
                    {
                        string[] newRoleOrg = new string[4];
                        newRoleOrg[1] = currentUser.Id;
                        newRoleOrg[2] = role;
                        //����Ϊ��ɫ����֯�ṹ�ǣ���֯�ṹĬ��Ϊ�ա�
                        newRoleOrg[3] = null;
                        //����������֯�ṹʱ����������ֵ
                        foreach (String[] judge in judgeRoleOrgList)
                        {
                            if (judge[1] == newRoleOrg[1] && judge[2] == newRoleOrg[2] && judge[3] == newRoleOrg[3])
                            {
                                newRoleOrg[0] = judge[0];
                            }
                        }
                        roleOrgList.Add(newRoleOrg);
                    }

                    foreach (String org in roleOrgDictionary[role])
                    {
                        string[] newRoleOrg = new string[4];
                        newRoleOrg[1] = currentUser.Id;
                        newRoleOrg[2] = role;
                        newRoleOrg[3] = org;
                        //��������ֵ
                        foreach (String[] judge in judgeRoleOrgList)
                        {
                            if (judge[1] == newRoleOrg[1] && judge[2] == newRoleOrg[2] && judge[3] == newRoleOrg[3])
                            {
                                newRoleOrg[0] = judge[0];
                            }
                        }
                        roleOrgList.Add(newRoleOrg);
                    }
                }
                foreach (String[] newString in roleOrgList)
                {
                    if (authorityLogic.Update(newString) == -1)
                    {
                        //newString[0] = FS.Framework.Facade.Context.GetSequence("SEQ_COM_AUTHORITY_ROLE");
                        newString[0] = this.GetSequence("PRIV.SEQ_COM_AUTHORITY_ROLE");
                        if (authorityLogic.Insert(newString) == -1)
                        {
                            return -1;
                        }
                    }
                }

                ////�Ƚ��µ������ֵ��е�Role�Ƿ�ͱ��е�Role������һ������һ����ɾ����
                foreach (String[] oldRoleOrg in judgeRoleOrgList)
                {
                    bool Judge = true;
                    foreach (String[] roleOrg in roleOrgList)
                    {
                        if (oldRoleOrg[2] == roleOrg[2] && oldRoleOrg[1] == roleOrg[1] && oldRoleOrg[3] == roleOrg[3])
                        {
                            Judge = false;
                            continue;
                        }
                    }

                    if (Judge)
                    {
                        authorityLogic.Delete(oldRoleOrg[0]);
                    }
                }

                ////ͬʱɸѡ���û���Ӧ�Ľ�ɫ�ı䣬���ɫҲ��Ӧ�ı䣬ɾ���û�Ȩ�ޱ��ж�Ӧ�Ľ�ɫ����Ӧ��Ȩ����Ϣ��
                ////��ǰ�û����н�ɫ�б�
                //List<String> roleIdList = new List<string>();
                //foreach (String roleId in roleOrgDictionary.Keys)
                //{
                //    roleIdList.Add(roleId);
                //}
                //////�õ���ǰ�û���ӵ�еĽ�ɫ������Ӧ��Ȩ���б�
                ////List<Priv> privList = QueryPriv(roleIdList);
                ////�õ���ǰ�û������õ�Ȩ����Ϣ
                //List<String> userPrivIdList = authorityLogic.QueryPrivId(currentUser.Id);

                //foreach (String userPrivId in userPrivIdList)
                //{
                //    bool judge = true;
                //    foreach (Priv priv in privList)
                //    {
                //        if (userPrivId == priv.Id)
                //        {
                //            judge = false;
                //            continue;
                //        }
                //    }
                //    if (judge)
                //    {
                //        authorityLogic.DeletePri(currentUser.Id, userPrivId);
                //    }
                //}



                //daoMgr.CommitTransaction();

                return 0;
            }
            catch (Exception ex)
            {
                //daoMgr.RollBackTransaction();
                throw ex;

            }
            //}
        }

        /// <summary>
        /// ��õ�ǰ�û���ɫ������Ϣ
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Dictionary<String, List<String>> QueryAuthorityRoleOrg(User user)
        {
            Dictionary<String, List<String>> roleOrgDictionary = new Dictionary<string, List<string>>();
            List<String[]> roleOrg = null;
            List<String> roleList = null;
            //using (DaoManager daoMgr = new DaoManager())
            //{
            try
            {
                //daoMgr.BeginTransaction();

                roleOrg = authorityLogic.Query(user.Id);
                roleList = authorityLogic.QueryRole(user.Id);

                foreach (String roleId in roleList)
                {
                    roleOrgDictionary.Add(roleId, new List<String>());
                    {
                        foreach (String[] roleOrgArray in roleOrg)
                        {
                            if (roleId == roleOrgArray[2])
                            {
                                roleOrgDictionary[roleId].Add(roleOrgArray[3]);
                            }
                        }
                    }
                }

                //daoMgr.CommitTransaction();

            }
            catch (Exception ex)
            {
                //daoMgr.RollBackTransaction();

                throw ex;
            }

            return roleOrgDictionary;
            // }

        }

        /// <summary>
        /// ��ѯ�û���ɫ��Ȩ��Ϣ
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Dictionary<String, List<HISFC.Models.Privilege.Organization>> GetAuthorityRoleOrg(User user)
        {
            Dictionary<String, HISFC.Models.Privilege.Organization> orgIdOrgMapping = new Dictionary<string, HISFC.Models.Privilege.Organization>();
            Dictionary<String, List<HISFC.Models.Privilege.Organization>> roleOrgsDictionary = new Dictionary<string, List<HISFC.Models.Privilege.Organization>>();
            List<String[]> roleOrg = null;
            List<String> roleList = null;


            //����������͵���֯�ṹ
            List<String> appIds = new List<string>();
            PrivilegeService privilegeService = new PrivilegeService();
            appIds = privilegeService.QueryAppID() as List<string>;

            foreach (string appId in appIds)
            {
                if (privilegeService.QueryUnit(appId) == null) continue;
                foreach (HISFC.Models.Privilege.Organization newOrg in privilegeService.QueryUnit(appId))
                {
                    if (newOrg == null || String.IsNullOrEmpty(newOrg.ID)) continue;
                    orgIdOrgMapping.Add(newOrg.ID, newOrg);
                }
            }


            //using (DaoManager daoMgr = new DaoManager())
            //{
            try
            {
                //daoMgr.BeginTransaction();

                roleOrg = authorityLogic.Query(user.Id);
                roleList = authorityLogic.QueryRole(user.Id);

                foreach (String roleId in roleList)
                {
                    roleOrgsDictionary.Add(roleId, new List<HISFC.Models.Privilege.Organization>());
                    {
                        foreach (String[] roleOrgArray in roleOrg)
                        {
                            if (roleId == roleOrgArray[2])
                            {
                                if (!orgIdOrgMapping.ContainsKey(roleOrgArray[3])) continue;
                                roleOrgsDictionary[roleId].Add(orgIdOrgMapping[roleOrgArray[3]]);
                            }
                        }
                    }
                }

                //daoMgr.CommitTransaction();

            }
            catch (Exception ex)
            {
                //daoMgr.RollBackTransaction();

                throw ex;
            }

            return roleOrgsDictionary;
            //}

        }

        /// <summary>
        /// ɾ���û���Ȩ
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public int CancelAuthority(string userId, string roleId)
        {
            //using (DaoManager daoMgr = new DaoManager())
            //{
            try
            {
                //daoMgr.BeginTransaction();
                int i = new AuthorityLogic().Delete(userId, roleId); ;
                //daoMgr.CommitTransaction();
                return i;
            }
            catch (Exception ex)
            {
                //daoMgr.RollBackTransaction();
                throw ex;
            }
            //}

        }

        /// <summary>
        /// ��õ�ǰ��ɫ��ӵ�е��û���Ϣ
        /// </summary>
        /// <param name="roleId">��ɫId</param>
        /// <returns></returns>
        public List<User> QueryUsers(string roleId)
        {
            List<String> userIdList = authorityLogic.QueryUser(roleId);
            return new UserLogic().QueryUsers(userIdList); ;
        }

        /// <summary>
        /// �����û���ӵ�е�����Ȩ��
        /// </summary>
        /// <param name="roleIdList"></param>
        /// <returns></returns>
        public List<Priv> QueryPriv(List<String> roleIdList)
        {
            List<Priv> privList = new List<Priv>();

            IDictionary<Priv, IList<Operation>> privDictionary = null;
            foreach (String roleId in roleIdList)
            {
                Role newRole = new Role();
                newRole.ID = roleId;
                privDictionary = new PrivilegeService().GetPermission(newRole);
                //���Ȩ���б���û��ֵ��ֱ����ӡ�
                if (privList.Count == 0)
                {
                    foreach (Priv newpriv in privDictionary.Keys)
                    {
                        privList.Add(newpriv);
                    }
                }
                else//�ж�privList���Ƿ���Ҫ��ӵ�ֵ��û�м��в��ӡ�
                {
                    foreach (Priv newpriv in privDictionary.Keys)
                    {
                        bool judge = true;
                        foreach (Priv judgePriv in privList)
                        {
                            if (newpriv.Name == judgePriv.Name && newpriv.Type == judgePriv.Type)
                            {
                                judge = false;
                            }
                        }

                        if (judge)
                        {
                            privList.Add(newpriv);
                        }
                    }

                }
            }
            return privList;
        }

        /// <summary>
        /// �����û�Ȩ�޺���֯�ṹ��ϵ
        /// </summary>
        /// <param name="currentUser"></param>
        /// <param name="privOrgDictionary"></param>
        /// <returns></returns>
        public int SaveAuthorityPrivOrg(User currentUser, Dictionary<String, List<String>> privOrgDictionary)
        {
            List<String[]> privOrgList = new List<String[]>();//0,id;1,useId;2,privId;3,orgId;
            //�޸�ʱ��Ҫ�ҵ�����������������Ϣ�Ƿ��Ѿ����ڣ�
            List<String[]> judgePrivOrgList = new List<string[]>();

            //using (DaoManager daoMgr = new DaoManager())
            //{
            try
            {
                //daoMgr.BeginTransaction();
                //�����ݿ��еõ����и��û�����Ϣ
                judgePrivOrgList = authorityLogic.QueryPriv(currentUser.Id);

                foreach (String priv in privOrgDictionary.Keys)
                {
                    foreach (String org in privOrgDictionary[priv])
                    {
                        string[] newPrivOrg = new string[4];
                        newPrivOrg[1] = currentUser.Id;
                        newPrivOrg[2] = priv;
                        newPrivOrg[3] = org;
                        //��������ֵ
                        foreach (String[] judge in judgePrivOrgList)
                        {
                            if (judge[1] == newPrivOrg[1] && judge[2] == newPrivOrg[2] && judge[3] == newPrivOrg[3])
                            {
                                newPrivOrg[0] = judge[0];
                            }
                        }
                        privOrgList.Add(newPrivOrg);
                    }
                }
                foreach (String[] newString in privOrgList)
                {
                    if (authorityLogic.UpdatePriv(newString) == 0)
                    {
                        //newString[0] = FS.Framework.Facade.Context.GetSequence("SEQ_COM_AUTHORITY_PRIV");
                        newString[0] = this.GetSequence("PRIV.SEQ_COM_AUTHORITY_PRIV");
                        authorityLogic.InsertPriv(newString);
                    }
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

        /// <summary>
        /// ��õ�ǰ�û�Ȩ�޼�������Ϣ
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Dictionary<String, List<String>> QueryAuthorityPrivOrg(User user)
        {
            Dictionary<String, List<String>> privOrgDictionary = new Dictionary<string, List<string>>();
            List<String[]> privOrg = null;
            List<String> privList = null;
            //using (DaoManager daoMgr = new DaoManager())
            //{
            try
            {
                //daoMgr.BeginTransaction();
                privOrg = authorityLogic.QueryPriv(user.Id);
                privList = authorityLogic.QueryPrivId(user.Id);

                foreach (String privId in privList)
                {
                    privOrgDictionary.Add(privId, new List<String>());
                    {
                        foreach (String[] privOrgArray in privOrg)
                        {
                            if (privId == privOrgArray[2])
                            {
                                privOrgDictionary[privId].Add(privOrgArray[3]);
                            }
                        }
                    }
                }

                //daoMgr.CommitTransaction();
            }
            catch (Exception ex)
            {
                //daoMgr.RollBackTransaction();
                throw ex;
            }

            return privOrgDictionary;
            //}

        }

        /// <summary>
        /// ɾ���û���������Ȩ
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int DeleteAuthority(User user)
        {
            //using (DaoManager daoMgr = new DaoManager())
            //{
            try
            {
                //daoMgr.BeginTransaction();
                authorityLogic.DeletePriAll(user.Id);
                authorityLogic.DeleteRoleAll(user.Id);
                new UserLogic().Delete(user.Id);
                //daoMgr.CommitTransaction();
                return 1;
            }
            catch (Exception ex)
            {
                //daoMgr.RollBackTransaction();
                throw ex;
            }

        }

        //}
    }
}
