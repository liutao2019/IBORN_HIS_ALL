using System;
using System.Collections.Generic;
using System.Text;


namespace FS.HISFC.Models.Privilege
{
    /// <summary>
    /// ��Ա�ӿ�ʵ��,�ⲿ����ʹ��
    /// </summary>
    public class Person : FS.FrameWork.Models.NeuObject
    {
        private string id;
        private string name;
        private string remark;
        private string appId;
        #region Person ��Ա

        /// <summary>
        /// Id
        /// </summary>
        
        public string Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        /// <summary>
        /// ��ע
        /// </summary>
        
        public string Remark
        {
            get
            {
                return remark;
            }
            set
            {
                remark = value;
            }
        }

        /// <summary>
        /// ����ϵͳģ��
        /// </summary>
        
        public string AppId
        {
            get
            {
                return appId;
            }
            set
            {
                appId = value;
            }
        }
        #endregion

        /// <summary>
        /// ��Ա��¡
        /// </summary>
        /// <returns></returns>
        public new Person Clone()
        {
            Person obj = base.MemberwiseClone() as Person;
            return obj;
        }
    }
}
