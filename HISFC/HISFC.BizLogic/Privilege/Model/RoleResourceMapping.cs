using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using FS.FrameWork.Models;

namespace FS.HISFC.BizLogic.Privilege.Model
{
    /// <summary>
    ///
    /// </summary>
    [Serializable]
    public class RoleResourceMapping : NeuObject
    {
	
        private string id;
        /// <summary>
        /// id
        /// </summary>        
        
        public string Id
        {
            get { return id; }
            set { id = value; }
        }
	
        private string name;
        /// <summary>
        /// ����
        /// </summary>        
        
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string parent;
        /// <summary>
        /// ������ɫid
        /// </summary>        
        
        public String ParentId
        {
            get { return parent; }
            set { parent = value; }
        }
	
        private Role role=new Role();
        /// <summary>
        /// ��ɫ
        /// </summary>        
        
        public Role Role
        {
            get { return role; }
            set { role = value; }
        }
	
        private Resource resource=new Resource();
        /// <summary>
        /// ��Դ
        /// </summary>        
        
        public Resource Resource
        {
            get { return resource; }
            set { resource = value; }
        }
	
        private string type;
        /// <summary>
        /// ����
        /// </summary>        
        
        public string Type
        {
            get { return type; }
            set { type = value; }
        }
	
        private decimal orderNumber;
        /// <summary>
        /// ���������
        /// </summary>        
        
        public decimal OrderNumber
        {
            get { return orderNumber; }
            set { orderNumber = value; }
        }
	
        private string parameter;
        /// <summary>
        /// ����
        /// </summary>        
        
        public string Parameter
        {
            get { return parameter; }
            set { parameter = value; }
        }

        private string emplSql;
        /// <summary>
        /// ��ѯ������Ա����������SQL
        /// </summary>        

        public string EmplSql
        {
            get { return emplSql; }
            set { emplSql = value; }
        }

        private string deptSql;
        /// <summary>
        /// ��ѯ������ҹ���������SQL
        /// </summary>        

        public string DeptSql
        {
            get { return deptSql; }
            set { deptSql = value; }
        }
	
        private string validState;
        /// <summary>
        /// ��Ч�Ա�־ 1 ���� 0 ͣ��
        /// </summary>        
        
        public string ValidState
        {
            get { return validState; }
            set { validState = value; }
        }
	
        private string operCode;
        /// <summary>
        /// ����Ա
        /// </summary>        
        
        public string OperCode
        {
            get { return operCode; }
            set { operCode = value; }
        }
	
        private DateTime operDate;
        /// <summary>
        /// ����ʱ��
        /// </summary>        
        
        public DateTime OperDate
        {
            get { return operDate; }
            set { operDate = value; }
        }

        private String icon;
        /// <summary>
        /// �˵���ͼ��
        /// </summary>
        public string Icon
        {
            get
            {
                return icon;
            }
            set
            {
                icon = value;
            }
        }

        private FS.FrameWork.Models.NeuObject hospital = new NeuObject();

        public FS.FrameWork.Models.NeuObject Hospital
        {
            get { return hospital; }
            set { hospital = value; }
        }

        public   new RoleResourceMapping Clone()
        {
            RoleResourceMapping roleResourceMapping = base.Clone() as RoleResourceMapping;
            roleResourceMapping.hospital = this.Hospital.Clone();
            return roleResourceMapping;
        }


    } //end class
} //end namespace

