using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Collections.Specialized;
using System.Xml;
using FS.HISFC.BizProcess.Interface.Privilege;

namespace FS.HISFC.BizLogic.Privilege
{
    /// <summary>
    /// ���ù���
    /// </summary>
    public class ConfigurationFactory
    {
        #region ��֤����    
        
        /// <summary>
        /// �������ɶ���
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="dll"></param>
        /// <returns></returns>
        public static object Reflect(string typeName, string dll)
        {
            Type _type = Type.GetType(typeName);

            System.Reflection.Assembly _assembly;
            if (_type == null)
            {
                _assembly = System.Reflection.Assembly.LoadFile(AppDomain.CurrentDomain.BaseDirectory + dll);
               // _assembly = System.Reflection.Assembly.LoadFrom(dll);

            }
            else
            {
                _assembly = System.Reflection.Assembly.GetAssembly(_type);
            }
            object obj = _assembly.CreateInstance(typeName);

            return obj;
        }
        #endregion

        #region ��֯��������
        /// <summary>
        /// ������֯����ʵ��ģ��
        /// </summary>
        /// <returns></returns>
        public static IDictionary<string, IPrivInfo> LoadOrgProvider()
        {
            //NeuConfigurationSection section = ConfigurationUtil.GetConfigSection("orgFactory", "orgProvider");

            //FS.Framework.Configuration.ConfigurationViewBase _configFactory = new FS.Framework.Configuration.ConfigurationViewBase();
            //NeuConfigurationSection section = (NeuConfigurationSection)_configFactory.GetConfigurationSection("orgProvider");

            Dictionary<string, string> Collections = new Dictionary<string, string>();
            XmlDocument xml = new XmlDocument();
            string a = AppDomain.CurrentDomain.BaseDirectory + "Xml\\privilege.xml";
            xml.Load(a);
            XmlNodeList xmlNodeList = xml.SelectSingleNode("privilege/orgProvider/collection").ChildNodes;

            foreach (XmlNode node in xmlNodeList)
            {
                Collections.Add(node.Attributes["key"].Value.ToString(), node.Attributes["value"].Value.ToString());
            }

            IDictionary<string, IPrivInfo> _collection = new Dictionary<string, IPrivInfo>();
            foreach(KeyValuePair<string,string> pair in Collections)
            {
                string[] array = pair.Value.Split(new char[] { ',' });
                IPrivInfo _provider = (IPrivInfo)Reflect(array[0], array[1]);
                _collection.Add(pair.Key, _provider);
            }

            return _collection;
        }
        
        #endregion       

        #region ͳһȨ�޹���

        /// <summary>
        /// ����ͳһ��Ȩʵ��ģ��
        /// </summary>
        /// <returns></returns>
        public static IDictionary<FS.HISFC.BizLogic.Privilege.Model.ResourceType, 
            IList<FS.HISFC.BizLogic.Privilege.Model.Operation>> LoadPermission()
        {
            //FS.Framework.Configuration.ConfigurationViewBase _configFactory = new FS.Framework.Configuration.ConfigurationViewBase();
            //PermissionConfigurationSection section = (PermissionConfigurationSection)_configFactory.GetConfigurationSection("permissionFactory");

            Dictionary<string, Dictionary<string, string>> allInfo = new Dictionary<string, Dictionary<string, string>>();

            XmlDocument xml = new XmlDocument();
            xml.LoadXml(AppDomain.CurrentDomain.BaseDirectory + "Xml\\privilege.xml");
            XmlNodeList xmlNodeList = xml.SelectNodes("//privilege/permissionFactory/permissionProviders");

            foreach (XmlNode node in xmlNodeList)
            {
                string key = node.Attributes["id"].ToString() + "|" + node.Attributes["name"].ToString() + "|" + node.Attributes["type"].ToString() + "|" + node.Attributes["exclusive"].ToString();
                Dictionary<string, string> childInfo = new Dictionary<string, string>();
                foreach (XmlNode childNode in node.ChildNodes)
                {
                    childInfo.Add(childNode.Attributes["key"].ToString(), childNode.Attributes["value"].ToString());
                }
                allInfo.Add(key,childInfo);
            }


            IDictionary<FS.HISFC.BizLogic.Privilege.Model.ResourceType,
                IList<FS.HISFC.BizLogic.Privilege.Model.Operation>> _permissions =
                new Dictionary<FS.HISFC.BizLogic.Privilege.Model.ResourceType, IList<FS.HISFC.BizLogic.Privilege.Model.Operation>>();

            foreach (KeyValuePair<string,Dictionary<string,string>> pair in allInfo)
            {
                FS.HISFC.BizLogic.Privilege.Model.ResourceType _resType = new FS.HISFC.BizLogic.Privilege.Model.ResourceType();
                IList<FS.HISFC.BizLogic.Privilege.Model.Operation> _operations = new List<FS.HISFC.BizLogic.Privilege.Model.Operation>();

                string[] permissions = pair.Key.Split(new char[] { '|' });
                _resType.Id = permissions[0].ToString();
                _resType.Name = permissions[1].ToString();
                _resType.ImplType = permissions[2].ToString();
                _resType.Exclusive =FrameWork.Function.NConvert.ToBoolean( permissions[3].ToString());

                foreach (KeyValuePair<string,string> _operElement in pair.Value)
                {
                    FS.HISFC.BizLogic.Privilege.Model.Operation _operation = new FS.HISFC.BizLogic.Privilege.Model.Operation();
                    _operation.Id = _operElement.Key;
                    _operation.Name = _operElement.Value;
                    _operation.ResourceType = permissions[0].ToString(); ;

                    _operations.Add(_operation);
                }

                _permissions.Add(_resType, _operations);
            }

            return _permissions;
        }
               
        #endregion
    }


}
