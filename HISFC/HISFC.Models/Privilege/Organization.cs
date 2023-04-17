using System;
using System.Collections.Generic;
using System.Text;
using FS.FrameWork.Models;


namespace FS.HISFC.Models.Privilege
{

    public class Organization :FS.FrameWork.Models.NeuObject
    {
        /// <summary>
        /// �����е�name��ʾ��������
        /// </summary>
        public new String Name
        {
            get { return this.department.Name; }
            set { this.department.Name = value; }
        }

        private NeuObject department = new NeuObject();
        /// <summary>
        /// ����
        /// </summary>
        public NeuObject Department
        {
            get { return department; }
            set { department = value; }
        }

        private String type;
        /// <summary>
        /// ��֯�ṹ����
        /// </summary>
        public String Type
        {
            get { return type; }
            set { type = value; }
        }

        private Organization parent;
        /// <summary>
        /// �����ڵ�
        /// </summary>
        public Organization Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        private int level;
        /// <summary>
        /// �ڵ����ȣ����㿪ʼ��
        /// </summary>
        public int Level
        {
            get { return level; }
            set { level = value; }
        }

        private bool hasChildren;
        /// <summary>
        /// �Ƿ����ӽڵ�
        /// </summary>
        public bool HasChildren
        {
            get
            {
                return hasChildren;
            }
            set
            {
                hasChildren = value;
            }
        }

        private int orderNumber;
        /// <summary>
        /// ��ͬһ�����ڵ��µ�����˳��
        /// </summary>
        public int OrderNumber
        {
            get { return orderNumber; }
            set { orderNumber = value; }
        }

        List<Organization> organizations;
        /// <summary>
        /// �ӽڵ㼯��
        /// </summary>
        public List<Organization> Organizations
        {
            get { return organizations; }
            set { organizations = value; }
        }

        public string AppId
        {
            get { return "HIS"; }
            set { }
        }

        public string ParentId
        {
            get
            {
                if (parent == null)
                {
                    return null;
                }
                else
                {
                    return this.parent.ID;

                }

            }
        }
    }
}

