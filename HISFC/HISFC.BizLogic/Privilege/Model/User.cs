using System;
using System.Collections.Generic;
using System.Text;
using FS.FrameWork.Models;


namespace FS.HISFC.BizLogic.Privilege.Model
{
    /// <summary>
    /// ��¼�û�ʵ��
    /// </summary>
    [Serializable]
    public class User : NeuObject
    {
        #region User ��Ա

        /// <summary>
        /// �û�Id,�߼�����
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
        /// �û�����
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
        /// ��¼�ʻ�
        /// </summary>
        
        public string Account
        {
            get
            {
                return _account;
            }
            set
            {
                _account = value;
            }
        }

        /// <summary>
        /// ��¼����
        /// </summary>
        
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
            }
        }

        /// <summary>
        /// Ӧ��Id
        /// </summary>
        
        public string AppId
        {
            get
            {
                return _appId;
            }
            set
            {
                _appId = value;
            }
        }

        /// <summary>
        /// ��ԱId
        /// </summary>
        
        public String PersonId
        {
            get
            {
                return _person;
            }
            set
            {
                _person = value;
            }
        }

        /// <summary>
        /// ��ע
        /// </summary>
        
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
            }
        }

        /// <summary>
        /// �ʻ��Ƿ����
        /// </summary>
        
        public bool IsLock
        {
            get
            {
                return _isLock;
            }
            set
            {
                _isLock = value;
            }
        }

        /// <summary>
        /// �����û�Id
        /// </summary>
        
        public string operId
        {
            get
            {
                return _operId;
            }
            set
            {
                _operId = value;
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

        ///// <summary>
        ///// ��Ȩ�б�
        ///// </summary>

        //public List<Authority> Authorities
        //{
        //    get
        //    {
        //        return authorities;
        //    }
        //    set
        //    {
        //        authorities = value;
        //    }
        //}
        #endregion

        /// <summary>
        /// ��¡����
        /// </summary>
        /// <returns></returns>
        public new User Clone()
        {
            User obj = base.MemberwiseClone() as User;

            return obj;
        }

        private string _id;
        private string _name;
        private string _account;
        private string _password;
        private string _appId;
        private string _person;
        private string _description;
        private string _operId;
        private DateTime _operDate;
        private bool _isLock;
        //{46A2B736-8740-405a-8B0A-6DDF1B705B8D}
        private bool _isManager;
        //{46A2B736-8740-405a-8B0A-6DDF1B705B8D}
        public bool IsManager
        {
            get { return _isManager; }
            set { _isManager = value; }
        }
        //private List<Authority> authorities;
    }
}
