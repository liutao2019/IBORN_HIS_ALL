using System;
using System.Collections.Generic;
using System.Text;
using FS.HISFC.BizLogic.Privilege.Model;
using FS.FrameWork.Models;

namespace FS.HISFC.BizLogic.Privilege.Model
{
    /// <summary>
    /// ��Ȩʵ����
    /// </summary>
    [Serializable]
    public class Priv : NeuObject
    {
        private string id;
        /// <summary>
        /// ��ԴId,�߼�����
        /// </summary>
        
        public string Id
        {
            get { return id; }
            set { id=value; }
        }

        private string name;
        /// <summary>
        /// ��Դ����
        /// </summary>
        
        public string Name
        {
            get { return name; }
            set { name=value; }
        }

        private string parentId;
        /// <summary>
        /// ������ԴId
        /// </summary>
        
        public string ParentId
        {
            get { return parentId; }
            set { parentId=value; }
        }

        private string type;
        /// <summary>
        /// ��Դ����
        /// </summary>
        
        public string Type
        {
            get { return type; }
            set { type=value; }
        }

        private string descriotion;
        /// <summary>
        /// ��ע
        /// </summary>
        
        public string Description
        {
            get { return descriotion; }
            set { descriotion=value; }
        }
    }
}
