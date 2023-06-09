using System;
using System.Collections.Generic;
using System.Text;
using FS.FrameWork.Models;

namespace FS.HISFC.BizLogic.Privilege.Model
{
    /// <summary>
    /// 角色实体
    /// </summary>
    [Serializable]
    public class Role:NeuObject
    {
       
        private string _parentId;
        private string _appId;
        private string _unitId;
        private string _description;
        private string _userId;
        private DateTime _operDate;

        #region Role 成员      

        /// <summary>
        /// 父级角色Id
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
        /// 应用Id
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
        /// 组织单元Id
        /// </summary>        
        public string UnitId
        {
            get
            {
                return _unitId;
            }
            set
            {
                _unitId = value;
            }
        }

        /// <summary>
        /// 备注
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

        #endregion

        /// <summary>
        /// 限制登录此角色的科室类别
        /// </summary>
        private string limitDeptType;

        /// <summary>
        /// 限制登录此角色的科室类别
        /// </summary>
        public string LimitDeptType
        {
            get
            {
                return limitDeptType;
            }
            set
            {
                limitDeptType = value;
            }
        }

        /// <summary>
        /// 操作用户Id
        /// </summary>        
        public string UserId
        {
            get
            {
                return _userId;
            }
            set
            {
                _userId = value;
            }
        }

        /// <summary>
        /// 操作时间
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


        private FS.FrameWork.Models.NeuObject hospital = new NeuObject();

        /// <summary>
        /// 
        /// </summary>
        public FS.FrameWork.Models.NeuObject Hospital
        {
            get { return hospital; }
            set { hospital = value; }
        }


        /// <summary>
        /// 角色克隆
        /// </summary>
        /// <returns></returns>
        public new Role Clone()
        {
            Role obj = base.Clone() as Role;
            obj.Hospital = this.Hospital.Clone();
            return obj;
        }
    }
}
