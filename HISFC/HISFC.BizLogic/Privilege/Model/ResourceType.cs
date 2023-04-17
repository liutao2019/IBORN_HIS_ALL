using System;
using System.Collections.Generic;
using System.Text;
using FS.FrameWork.Models;

namespace FS.HISFC.BizLogic.Privilege.Model
{
    /// <summary>
    /// ��Դ����
    /// </summary>
    [Serializable]
    public class ResourceType:NeuObject
    {
        private string _id;

        /// <summary>
        /// Id
        /// </summary>
        
        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }
        private string _name;

        /// <summary>
        /// ����
        /// </summary>
        
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        private string _implType;

        /// <summary>
        /// ����
        /// </summary>
        
        public string ImplType
        {
            get { return _implType; }
            set { _implType = value; }
        }
        private bool _exclusive;

        /// <summary>
        /// 
        /// </summary>
        
        public bool Exclusive
        {
            get { return _exclusive; }
            set { _exclusive = value; }
        }
    }
}
