using System;
using System.Collections.Generic;
using System.Text;
using FS.FrameWork.Models;


namespace FS.HISFC.BizLogic.Privilege.Model
{
    /// <summary>
    /// ��Ȩʵ���࣬��¼�û�����ɫ����֯����Ȩ��ϵ
    /// </summary>
    [Serializable]
    public class Authority : NeuObject
    {
        private User user;
        private Role role;
        private List<HISFC.Models.Privilege.Organization> organizations;

        /// <summary>
        /// �û�
        /// </summary>
        public User User
        {
            get { return user; }
            set { user = value; }
        }

        /// <summary>
        /// ��ɫ
        /// </summary>
        public Role Role
        {
            get { return role; }
            set { role = value; }
        }

        /// <summary>
        /// ��֯�б�
        /// </summary>
        public List<HISFC.Models.Privilege.Organization> Organizations
        {
            get { return organizations; }
            set { organizations = value; }
        }
    }
}
