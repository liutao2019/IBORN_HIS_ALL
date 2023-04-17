using System;
using System.Collections.Generic;
using System.Text;
using FS.HISFC.BizProcess.Interface.Privilege;

namespace FS.HISFC.BizLogic.Privilege
{
    /// <summary>
    /// ��֯�ṹ����
    /// </summary>
    public class OrgFactory
    {
        private static IDictionary<string, IPrivInfo> _orgProviders = null;
        /// <summary>
        /// ��֯�����ṩ��
        /// </summary>
        public static IDictionary<string, IPrivInfo> getOrgProvider()
        {
            if (_orgProviders != null)
            {
                return _orgProviders;
            }
            else
            {
                _orgProviders = ConfigurationFactory.LoadOrgProvider();
            }

            if (_orgProviders == null) throw new Exception("���������ļ��м�����֯����ʵ�ֿ�!");

            return _orgProviders;
        }

    }
}
