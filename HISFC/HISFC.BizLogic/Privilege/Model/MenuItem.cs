using System;
using System.Collections.Generic;
using System.Text;
using FS.FrameWork.Models;


namespace FS.HISFC.BizLogic.Privilege.Model
{
    /// <summary>
    /// �˵���
    /// </summary>
    [Serializable]
    public class MenuItem : NeuObject
    {
        private string _id;
        private string _name;
        private string _parentId;
        private string _shortcut;
        private string _icon;
        private string _dllName;
        private string _winName;
        private string _controlType;
        private string _showType;
        private string _param;
        private string _layer;
        private string _tooltip;
        private bool _enabled;
        private string _userId;
        private DateTime _operDate;
        private string _type;
        private int _order;
        private string _desc;
        private string _treeDllName;

        /// <summary>
        /// ���Ϳؼ�����dll
        /// </summary>
        public string TreeDllName
        {
            get { return _treeDllName; }
            set { _treeDllName = value; }
        }

        private string _treeName;

        /// <summary>
        /// ���Ϳؼ�����
        /// </summary>
        public string TreeName
        {
            get { return _treeName; }
            set { _treeName = value; }
        }

        #region IResource ��Ա

        /// <summary>
        /// �˵���Id
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
        /// �˵�������
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
        /// �˵����Id
        /// </summary>
        
        public string ParentId
        {
            get
            {
                return _parentId;
            }
            set
            {
                _parentId = value;
            }
        }

        /// <summary>
        /// ��ע
        /// </summary>
        public string Description
        {
            get { return _desc; }
            set { _desc = value; }
        }
        #endregion

        /// <summary>
        /// �˵����ݼ�
        /// </summary>
        
        public string Shortcut
        {
            get
            {
                return _shortcut;
            }
            set
            {
                _shortcut = value;
            }
        }

        /// <summary>
        /// �˵���ͼ��
        /// </summary>
        
        public string Icon
        {
            get
            {
                return _icon;
            }
            set
            {
                _icon = value;
            }
        }

        /// <summary>
        /// ���ÿؼ�����Dll����
        /// </summary>
        
        public string DllName
        {
            get
            {
                return _dllName;
            }
            set
            {
                _dllName = value;
            }
        }

        /// <summary>
        /// ���ÿؼ�����
        /// </summary>
        
        public string WinName
        {
            get
            {
                return _winName;
            }
            set
            {
                _winName = value;
            }
        }

        /// <summary>
        /// �ؼ�����
        /// </summary>
        
        public string ControlType
        {
            get
            {
                return _controlType;
            }
            set
            {
                _controlType = value;
            }
        }

        /// <summary>
        /// �ؼ���ʾ����
        /// </summary>
        
        public string ShowType
        {
            get
            {
                return _showType;
            }
            set
            {
                _showType = value;
            }
        }

        /// <summary>
        /// ����ؼ�����
        /// </summary>
        
        public string Param
        {
            get
            {
                return _param;
            }
            set
            {
                _param = value;
            }
        }

        /// <summary>
        /// �˵������ڲ���
        /// </summary>
        
        public string Layer
        {
            get
            {
                return _layer;
            }
            set
            {
                _layer = value;
            }
        }

        /// <summary>
        /// ������ʾ
        /// </summary>
        
        public string Tooltip
        {
            get
            {
                return _tooltip;
            }
            set
            {
                _tooltip = value;
            }
        }

        /// <summary>
        /// �Ƿ����
        /// </summary>
        
        public bool Enabled
        {
            get
            {
                return _enabled;
            }
            set
            {
                _enabled = value;
            }
        }

        /// <summary>
        /// �����û�����
        /// </summary>
        
        public string UserId
        {
            get
            {
                return _userId;
            }
            set
            {
                _userId= value;
            }
        }

        /// <summary>
        /// ����ʱ��
        /// </summary>
        
        public DateTime OperDate
        {
            get
            {
                return _operDate;
            }
            set
            {
                _operDate = value;
            }
        }

        /// <summary>
        /// ��Դ����
        /// </summary>
        
        public string Type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
            }
        }

        /// <summary>
        /// ��ʾ˳��
        /// </summary>
        
        public int Order
        {
            get
            {
                return _order;
            }
            set
            {
                _order = value;
            }
        }

        /// <summary>
        /// �˵���¡
        /// </summary>
        /// <returns></returns>
        public new MenuItem Clone()
        {
            MenuItem obj = base.MemberwiseClone() as MenuItem;
            return obj;
        }
    }
}
