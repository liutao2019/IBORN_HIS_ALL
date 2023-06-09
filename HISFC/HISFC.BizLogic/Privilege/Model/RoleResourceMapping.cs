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
        /// 名称
        /// </summary>        
        
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string parent;
        /// <summary>
        /// 父级角色id
        /// </summary>        
        
        public String ParentId
        {
            get { return parent; }
            set { parent = value; }
        }
	
        private Role role=new Role();
        /// <summary>
        /// 角色
        /// </summary>        
        
        public Role Role
        {
            get { return role; }
            set { role = value; }
        }
	
        private Resource resource=new Resource();
        /// <summary>
        /// 资源
        /// </summary>        
        
        public Resource Resource
        {
            get { return resource; }
            set { resource = value; }
        }
	
        private string type;
        /// <summary>
        /// 类型
        /// </summary>        
        
        public string Type
        {
            get { return type; }
            set { type = value; }
        }
	
        private decimal orderNumber;
        /// <summary>
        /// 本级内序号
        /// </summary>        
        
        public decimal OrderNumber
        {
            get { return orderNumber; }
            set { orderNumber = value; }
        }
	
        private string parameter;
        /// <summary>
        /// 参数
        /// </summary>        
        
        public string Parameter
        {
            get { return parameter; }
            set { parameter = value; }
        }

        private string emplSql;
        /// <summary>
        /// 查询报表人员过滤条件的SQL
        /// </summary>        

        public string EmplSql
        {
            get { return emplSql; }
            set { emplSql = value; }
        }

        private string deptSql;
        /// <summary>
        /// 查询报表科室过滤条件的SQL
        /// </summary>        

        public string DeptSql
        {
            get { return deptSql; }
            set { deptSql = value; }
        }
	
        private string validState;
        /// <summary>
        /// 有效性标志 1 在用 0 停用
        /// </summary>        
        
        public string ValidState
        {
            get { return validState; }
            set { validState = value; }
        }
	
        private string operCode;
        /// <summary>
        /// 操作员
        /// </summary>        
        
        public string OperCode
        {
            get { return operCode; }
            set { operCode = value; }
        }
	
        private DateTime operDate;
        /// <summary>
        /// 操作时间
        /// </summary>        
        
        public DateTime OperDate
        {
            get { return operDate; }
            set { operDate = value; }
        }

        private String icon;
        /// <summary>
        /// 菜单项图标
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

