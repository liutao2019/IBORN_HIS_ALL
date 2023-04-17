using System;
using System.Collections;


namespace FS.HISFC.Models.Admin
{


    /// <summary>
    /// User 的摘要说明。
    /// </summary>
    /// 
    [System.Serializable]
    public class PowerUser : FS.FrameWork.Models.NeuObject
    {
        public PowerUser()
        {
            //
            // TODO: 在此处添加构造函数逻辑

            //

            Department = new FS.FrameWork.Models.NeuObject();
            GrantDepartment = new FS.FrameWork.Models.NeuObject();

            Department.ID = "";
            Department.Name = "";
        }

        //ID;
        //Name;

        /// <summary>
        /// 权限部门
        /// </summary>
        public FS.FrameWork.Models.NeuObject Department;

        public FS.FrameWork.Models.NeuObject GrantDepartment;


        private IList powerDetails;
        private IList roleDetails;


        public string PowerClass1;
        public string PowerClass2;
        public string PowerClass3;


        /// <summary>
        /// 人员的扩展权限
        /// </summary>
        public IList PowerDetails
        {
            get
            {
                return this.powerDetails;
            }
            set
            {
                this.powerDetails = value;
            }
        }

        public IList RoleDetails
        {
            get
            {
                return this.roleDetails;
            }
            set
            {
                this.roleDetails = value;
            }
        }
    }
}
