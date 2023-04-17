using System;
using System.Collections.Generic;
using System.Text;
using FS.HISFC.BizLogic.Privilege.Model;


namespace FS.HISFC.BizLogic.Privilege
{
    /// <summary>
    /// Ȩ�޽ӿ�ʵ����
    /// </summary>
    public class PermissionProvider : IPermissionProvider
    {

        #region IPermissionProvider ��Ա

        /// <summary>
        /// ��ɫ��Ȩ
        /// </summary>
        public int Grant(Role role, Priv resource, IList<Operation> operations, object param)
        {
            //using (DaoManager _dao = new DaoManager())
            //{
            PrivLogic privlogic = new PrivLogic();
            IDictionary<Priv, IList<Operation>> _permission = privlogic.QueryPermission(resource.Type, resource.Id, role);
                string _pmsExp = "";

                //����
                if (_permission.Count == 0)
                {
                    foreach (Operation _opera in operations)
                    {
                        _pmsExp = _pmsExp + _opera.Id + "|";
                    }

                    return privlogic.InsertResRoleMap(resource, role.ID, _pmsExp, (param == null ? "" : param.ToString()));
                }
                else//�޸�
                {
                    int rtn = privlogic.DelResRoleMap(resource.Id, role.ID);
                    if (rtn <= 0) return -1;

                    foreach (KeyValuePair<Priv, IList<Operation>> _pair in _permission)
                    {
                        foreach (Operation _opera in _pair.Value)
                        {
                            _pmsExp = _pmsExp + _opera.Id + "|";
                        }
                        break;
                    }

                    foreach (Operation _opera in operations)
                    {
                        if (_pmsExp.IndexOf(_opera.Id) < 0)
                        {
                            _pmsExp = _pmsExp + _opera.Id + "|";
                        }
                    }

                    return privlogic.InsertResRoleMap(resource, role.ID, _pmsExp, (param == null ? "" : param.ToString()));
                }
            //}
        }

        /// <summary>
        /// ������ɫȨ��
        /// </summary>
        public int Revoke(Role role, Priv resource, IList<Operation> operations)
        {
        //    using (DaoManager _dao = new DaoManager())
        //    {
            PrivLogic privlogic = new PrivLogic();
                IDictionary<Priv, IList<Operation>> _permission = privlogic.QueryPermission(resource.Type, resource.Id, role);
                string _pmsExp = "";

                if (_permission != null && _permission.Count > 0)//�޸�
                {
                    int rtn = privlogic.DelResRoleMap(resource.Id, role.ID);
                    if (rtn <= 0) return -1;

                    foreach (KeyValuePair<Priv, IList<Operation>> _pair in _permission)
                    {
                        foreach (Operation _opera in _pair.Value)
                        {
                            if (!IsContainOperation(_opera, operations))
                                _pmsExp = _pmsExp + _opera.Id + "|";
                        }
                        break;
                    }

                    return privlogic.InsertResRoleMap(resource, role.ID, _pmsExp, "");
                }

                return 0;
            //}
        }

        private bool IsContainOperation(Operation opera, IList<Operation> operations)
        {
            bool _isFound = false;

            foreach (Operation _opera in operations)
            {
                if (_opera.Id == opera.Id)
                {
                    _isFound = true;
                    break;
                }
            }

            return _isFound;
        }

        /// <summary>
        /// ������ɫ��ĳһ��Դȫ��Ȩ��
        /// </summary>
        public int Revoke(Role role, Priv resource)
        {
            //using (DaoManager _dao = new DaoManager())
            //{
            PrivLogic privlogic = new PrivLogic();
                IDictionary<Priv, IList<Operation>> _permission = privlogic.QueryPermission(resource.Type, resource.Id, role);
                
                if (_permission != null && _permission.Count > 0)//�޸�
                {
                    int rtn = privlogic.DelResRoleMap(resource.Id, role.ID);
                    if (rtn <= 0) return -1;
                }

                return 0;
            //}
        }

        /// <summary>
        /// �Ƿ������ɫ����Դ�Ĳ���
        /// </summary>
        public bool IsAllowed(Role role, Priv resource, Operation operation)
        {
            PrivLogic privlogic = new PrivLogic();
            IDictionary<Priv, IList<Operation>> _permission = privlogic.QueryPermission(resource.Type, resource.Id, role);
            if (_permission == null || _permission.Count == 0) return false;

            foreach (KeyValuePair<Priv, IList<Operation>> _pair in _permission)
            {
                foreach (Operation _opera in _pair.Value)
                {
                    if (_opera.Id == operation.Id) return true;
                }
                break;
            }

            return false;
        }

        /// <summary>
        /// ��ȡȫ����Դ
        /// </summary>
        public IList<Priv> GetResource(string resTypeId)
        {
            return new PrivLogic().Query(resTypeId);
        }

        /// <summary>
        /// ���ݽ�ɫ����Դ��ȡȨ��
        /// </summary>
        public IDictionary<Priv, IList<Operation>> GetPermission(string resTypeId, string resId, Role role)
        {
            return new PrivLogic().QueryPermission(resTypeId, resId, role);
        }

        /// <summary>
        /// ��ȡ��ɫ��Ȩ��
        /// </summary>
        /// <param name="resTypeId"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public IDictionary<Priv, IList<Operation>> GetPermission(string resTypeId, Role role)
        {
            return new PrivLogic().QueryPermission(resTypeId, role);
        }

        #endregion
    }
}
