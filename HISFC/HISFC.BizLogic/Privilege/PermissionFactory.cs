using System;
using System.Collections.Generic;
using System.Text;
using FS.HISFC.BizLogic.Privilege.Model;


namespace FS.HISFC.BizLogic.Privilege
{
    /// <summary>
    /// Ȩ�޹���
    /// </summary>
   public class PermissionFactory
    {
        private static IDictionary<ResourceType, IList<Operation>> _permissionProviders = null;
       /// <summary>
       /// Ȩ���ṩ��
       /// </summary>
       /// <returns></returns>
        public static IDictionary<ResourceType, IList<Operation>> LoadPermissionProvider()
        {
            if (_permissionProviders != null)
            {
                return _permissionProviders;
            }
            else
            {
                _permissionProviders = ConfigurationFactory.LoadPermission();
            }
            if (_permissionProviders == null) throw new Exception("���������ļ��м���Ȩ�޹���ʵ�ֿ�!");
            return _permissionProviders;
        }
    }
}
