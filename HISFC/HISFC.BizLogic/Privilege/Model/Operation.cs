using System;
using System.Collections.Generic;
using System.Text;


namespace FS.HISFC.BizLogic.Privilege.Model
{
    /// <summary>
    /// ��Դ����
    /// </summary>
    [Serializable]
    public class Operation 
    {
        private string _id;
        private string _name;
        private string _resourceType;

        #region Operation ��Ա

        /// <summary>
        /// ����id
        /// </summary>
        
        public string Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        /// <summary>
        /// ��Դ����
        /// </summary>
        
        public string ResourceType
        {
            get
            {
                return _resourceType;
            }
            set
            {
                _resourceType = value;
            }
        }

        #endregion
    }
}
