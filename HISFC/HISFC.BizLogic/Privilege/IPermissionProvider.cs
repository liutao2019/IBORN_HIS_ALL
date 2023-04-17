using System;
using System.Collections.Generic;
using System.Text;
using FS.HISFC.BizLogic.Privilege.Model;

namespace FS.HISFC.BizLogic.Privilege
{
    /// <summary>
    /// Ȩ�޹���ӿ�
    /// </summary>
    public interface IPermissionProvider
    {
        /// <summary>
        /// ��ɫ��Ȩ
        /// </summary>
        int Grant(Role role, Priv resource, IList<Operation> operations, object param);

        /// <summary>
        /// ������ɫȨ��
        /// </summary>
        int Revoke(Role role, Priv resource, IList<Operation> operations);

        /// <summary>
        /// ������ɫ��ĳһ��Դȫ��Ȩ��
        /// </summary>
        int Revoke(Role role, Priv resource);
        
        /// <summary>
        /// �Ƿ������ɫ����Դ�Ĳ���
        /// </summary>
        bool IsAllowed(Role role, Priv resource, Operation operation);

        /// <summary>
        /// ��ȡȫ����Դ
        /// </summary>
        IList<Priv> GetResource(string resTypeId);

        /// <summary>
        /// ���ݽ�ɫ����Դ��ȡȨ��
        /// </summary>
        IDictionary<Priv, IList<Operation>> GetPermission(string resTypeId, string resId, Role role);

        /// <summary>
        /// ��ȡ��ɫ��Ȩ��
        /// </summary>
        /// <param name="resTypeId"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        IDictionary<Priv, IList<Operation>> GetPermission(string resTypeId, Role role);


    }
}
