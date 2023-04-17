using System;
using System.Collections.Generic;
using System.Text;

namespace FS.SOC.HISFC.BizProcess.DCPInterfaceAchieve
{
    /// <summary>
    /// 接口实现类
    /// </summary>
    public class CommonArchieve : FS.SOC.HISFC.BizProcess.DCPInterface.IDepartment,FS.SOC.HISFC.BizProcess.DCPInterface.IEmployee,FS.SOC.HISFC.BizProcess.DCPInterface.IConstant
    {
        /// <summary>
        /// 科室管理类
        /// </summary>
        private FS.HISFC.BizLogic.Manager.Department deptManagment = new FS.HISFC.BizLogic.Manager.Department();

        /// <summary>
        /// 人员管理类
        /// </summary>
        private FS.HISFC.BizLogic.Manager.Person personManagment = new FS.HISFC.BizLogic.Manager.Person();

        /// <summary>
        /// 常数管理类
        /// </summary>
        private FS.HISFC.BizLogic.Manager.Constant consManagment = new FS.HISFC.BizLogic.Manager.Constant();

        #region IEmployee 成员

        public FS.HISFC.Models.Base.Employee GetEmployeeInfoByID(string ID)
        {
            FS.HISFC.Models.Base.Employee info = this.personManagment.GetPersonByID(ID);

            this.SetMsg(this.personManagment);

            return info;
        }

        public System.Collections.ArrayList QueryEmployeeAllValid()
        {
            System.Collections.ArrayList al = this.personManagment.GetEmployeeAll();

            this.SetMsg(this.personManagment);

            return al;
        }

        public System.Collections.ArrayList QueryEmployeeAllValidAndUnvalid()
        {
            return this.QueryEmployeeAllValid();
        }

        public System.Collections.ArrayList QueryEmployeeByDeptID(string deptID)
        {
            System.Collections.ArrayList al = this.personManagment.GetPersonsByDeptID(deptID);

            this.SetMsg(this.personManagment);

            return al;
        }

        public System.Collections.ArrayList QueryEmployeeByType(FS.HISFC.Models.Base.EnumEmployeeType emplType)
        {
            System.Collections.ArrayList al = this.personManagment.GetEmployee(emplType);

            this.SetMsg(this.personManagment);

            return al;
        }

        public System.Collections.ArrayList QueryEmployeeByTypeAndDeptID(FS.HISFC.Models.Base.EnumEmployeeType emplType, string deptID)
        {
            System.Collections.ArrayList al = this.QueryEmployeeByDeptID(deptID);
            this.SetMsg(this.personManagment);

            if (al == null)
            {
                return null;
            }

            System.Collections.ArrayList alDeptEmpl = new System.Collections.ArrayList();

            foreach (FS.HISFC.Models.Base.Employee info in al)
            {
                if (info.EmployeeType.ID.ToString() == emplType.ToString())
                {
                    alDeptEmpl.Add(info);
                }
            }

            return alDeptEmpl;
        }

        #endregion

        #region IBase 成员

        private string msg;

        private int msgCode;

        public string Msg
        {
            get
            {
                return this.msg;
            }
            set
            {
                this.msg = value;
            }
        }

        public int MsgCode
        {
            get
            {
                return this.msgCode;
            }
            set
            {
                this.msgCode = value;
            }
        }

        #endregion

        #region IDepartment 成员

        public FS.HISFC.Models.Base.Department GetDeptByID(string deptID)
        {
            FS.HISFC.Models.Base.Department dept = this.deptManagment.GetDeptmentById(deptID);

            this.SetMsg(this.deptManagment);

            return dept;
        }

        public System.Collections.ArrayList QueryDeptAllValid()
        {
            System.Collections.ArrayList al = this.deptManagment.GetDeptmentAll();

            this.SetMsg(this.deptManagment);

            return al;
        }

        public System.Collections.ArrayList QueryDeptAllValidAndUnvalid()
        {
            return this.QueryDeptAllValid();
        }

        public System.Collections.ArrayList QueryDeptByType(FS.HISFC.Models.Base.EnumDepartmentType type)
        {
            System.Collections.ArrayList al = this.deptManagment.GetDeptment(type);

            this.SetMsg(this.deptManagment);

            return al;
        }

        #endregion

        /// <summary>
        /// 设置提示信息
        /// </summary>
        /// <param name="dataManagment"></param>
        private void SetMsg(FS.FrameWork.Management.Database dataManagment)
        {
            this.Msg = dataManagment.Err;
            this.MsgCode = dataManagment.DBErrCode;
        }

        #region IConstant 成员

        public FS.FrameWork.Models.NeuObject GetConstantByTypeAndID(string type, string ID)
        {
            FS.FrameWork.Models.NeuObject info = this.consManagment.GetConstant(type, ID);

            this.SetMsg(this.consManagment);

            return info;
        }

        public System.Collections.ArrayList QueryConstantList(FS.HISFC.Models.Base.EnumConstant constType)
        {
            System.Collections.ArrayList al = this.consManagment.GetList(constType);

            this.SetMsg(this.consManagment);

            return al;
        }

        public System.Collections.ArrayList QueryConstantList(string constType)
        {
            System.Collections.ArrayList al = this.consManagment.GetList(constType);

            this.SetMsg(this.consManagment);

            return al;
        }

        #endregion
    }
}
