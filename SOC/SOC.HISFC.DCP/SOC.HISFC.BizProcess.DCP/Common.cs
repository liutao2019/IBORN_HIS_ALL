using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FS.SOC.HISFC.BizProcess.DCP
{
    [System.Serializable]
    public class Common : ProcessBase, FS.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        #region 接口变量

        /// <summary>
        /// 查询科室信息接口
        /// </summary>
        FS.SOC.HISFC.BizProcess.DCPInterface.IDepartment IQueryDeptmentInstance;

        /// <summary>
        /// 查询人员信息接口
        /// </summary>
        FS.SOC.HISFC.BizProcess.DCPInterface.IEmployee IQueryEmployeeInstance;
        /// <summary>
        /// 常数信息接口
        /// </summary>
        FS.SOC.HISFC.BizProcess.DCPInterface.IConstant IConstantInstance;

        #endregion

        #region 私有方法

        /// <summary>
        /// 反射读取实现科室查询接口
        /// </summary>
        private void CreateIDeptment()
        {
            if (this.IQueryDeptmentInstance == null)
            {
                this.IQueryDeptmentInstance = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(),
                    typeof(FS.SOC.HISFC.BizProcess.DCPInterface.IDepartment)) as FS.SOC.HISFC.BizProcess.DCPInterface.IDepartment;
            }

            if (this.IQueryDeptmentInstance == null)
            {
                throw new Exception(this.GetType().ToString() + "中的接口\n" + "FS.SOC.HISFC.BizProcess.DCPInterface.IDeptment" + "\n没有维护对照,请与管理员联系");
            }
        }

        /// <summary>
        /// 反射读取实现人员查询接口
        /// </summary>
        private void CreateIEmployee()
        {
            if (this.IQueryEmployeeInstance == null)
            {
                this.IQueryEmployeeInstance = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(),
                    typeof(FS.SOC.HISFC.BizProcess.DCPInterface.IEmployee)) as FS.SOC.HISFC.BizProcess.DCPInterface.IEmployee;
            }

            if (this.IQueryEmployeeInstance == null)
            {
                throw new Exception(this.GetType().ToString() + "中的接口\n" + "FS.SOC.HISFC.BizProcess.DCPInterface.IEmployee" + "\n没有维护对照,请与管理员联系");
            }
        }

        /// <summary>
        /// 反射读取实现常数查询接口
        /// </summary>
        /// <param name="interfaceObj"></param>
        /// <returns></returns>
        private void CreateIConstant()
        {
            if (this.IConstantInstance == null)
            {
                this.IConstantInstance = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(),
                    typeof(FS.SOC.HISFC.BizProcess.DCPInterface.IConstant)) as FS.SOC.HISFC.BizProcess.DCPInterface.IConstant;
            }

            if (this.IConstantInstance == null)
            {
                throw new Exception(this.GetType().ToString() + "中的接口\n" + "FS.SOC.HISFC.BizProcess.DCPInterface.IConstant" + "\n没有维护对照,请与管理员联系");
            }
        }

        #endregion

        #region 科室

        /// <summary>
        /// 查出所有科室,包括有效无效
        /// </summary>
        /// <returns></returns>
        public ArrayList QueryDeptAllValidAndUnvalid()
        {
            this.CreateIDeptment();

            ArrayList alTemp = this.IQueryDeptmentInstance.QueryDeptAllValidAndUnvalid();

            if (alTemp == null)
            {
                this.ErrorMsg = this.IQueryDeptmentInstance.Msg;
            }

            return alTemp;
        }

        /// <summary>
        /// 查出所有有效科室
        /// </summary>
        /// <returns></returns>
        public ArrayList QueryDeptAllValid()
        {
            this.CreateIDeptment();

            ArrayList alTemp = this.IQueryDeptmentInstance.QueryDeptAllValid();

            if (alTemp == null)
            {
                this.ErrorMsg = this.IQueryDeptmentInstance.Msg;
            }

            return alTemp;
        }

        /// <summary>
        /// 根据科室类型查询科室列表
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public ArrayList QueryDeptByType(FS.HISFC.Models.Base.EnumDepartmentType type)
        {
            this.CreateIDeptment();

            ArrayList alTemp = this.IQueryDeptmentInstance.QueryDeptByType(type);

            if (alTemp == null)
            {
                this.ErrorMsg = this.IQueryDeptmentInstance.Msg;
            }

            return alTemp;
        }

        /// <summary>
        /// 根据科室编号取科室信息
        /// </summary>
        /// <param name="deptID"></param>
        /// <returns></returns>
        public FS.HISFC.Models.Base.Department GetDeptByID(string deptID)
        {
            this.CreateIDeptment();

            FS.HISFC.Models.Base.Department tempDept = this.IQueryDeptmentInstance.GetDeptByID(deptID);

            if (tempDept == null)
            {
                this.ErrorMsg = this.IQueryDeptmentInstance.Msg;
            }

            return tempDept;
        }

        #endregion

        #region 人员

        /// <summary>
        /// 查询所有员工包括有效无效
        /// </summary>
        /// <returns></returns>
        public ArrayList QueryEmployeeAllValidAndUnvalid()
        {
            this.CreateIEmployee();

            ArrayList alTemp = this.IQueryEmployeeInstance.QueryEmployeeAllValidAndUnvalid();

            if (alTemp == null)
            {
                this.ErrorMsg = this.IQueryEmployeeInstance.Msg;
            }

            return alTemp;
        }

        /// <summary>
        /// 查询所有有效员工
        /// </summary>
        /// <returns></returns>
        public ArrayList QueryEmployeeAllValid()
        {
            this.CreateIEmployee();

            ArrayList alTemp = this.IQueryEmployeeInstance.QueryEmployeeAllValid();

            if (alTemp == null)
            {
                this.ErrorMsg = this.IQueryEmployeeInstance.Msg;
            }

            return alTemp;
        }

        /// <summary>
        /// 根据人员类型取所有人员
        /// </summary>
        /// <param name="emplType">人员类型</param>
        /// <returns></returns>
        public ArrayList QueryEmployeeByType(FS.HISFC.Models.Base.EnumEmployeeType emplType)
        {
            this.CreateIEmployee();

            ArrayList alTemp = this.IQueryEmployeeInstance.QueryEmployeeByType(emplType);

            if (alTemp == null)
            {
                this.ErrorMsg = this.IQueryEmployeeInstance.Msg;
            }

            return alTemp;
        }

        /// <summary>
        /// 查询某个科室所有人员
        /// </summary>
        /// <param name="deptID">科室编码</param>
        /// <returns></returns>
        public ArrayList QueryEmployeeByDeptID(string deptID)
        {
            this.CreateIEmployee();

            ArrayList alTemp = this.IQueryEmployeeInstance.QueryEmployeeByDeptID(deptID);

            if (alTemp == null)
            {
                this.ErrorMsg = this.IQueryEmployeeInstance.Msg;
            }

            return alTemp;
        }

        /// <summary>
        /// 查询某个科室某种类型的人员
        /// </summary>
        /// <param name="emplType">人员类型</param>
        /// <param name="deptID">科室编码</param>
        /// <returns></returns>
        public ArrayList QueryEmployeeByTypeAndDeptID(FS.HISFC.Models.Base.EnumEmployeeType emplType, string deptID)
        {
            this.CreateIEmployee();

            ArrayList alTemp = this.IQueryEmployeeInstance.QueryEmployeeByTypeAndDeptID(emplType, deptID);

            if (alTemp == null)
            {
                this.ErrorMsg = this.IQueryEmployeeInstance.Msg;
            }

            return alTemp;
        }

        /// <summary>
        /// 根据员工编码取员工基本信息
        /// </summary>
        /// <param name="ID">人员编码</param>
        /// <returns></returns>
        public FS.HISFC.Models.Base.Employee GetEmployeeInfoByID(string ID)
        {
            this.CreateIEmployee();

            FS.HISFC.Models.Base.Employee tempEmployee = this.IQueryEmployeeInstance.GetEmployeeInfoByID(ID);

            if (tempEmployee == null)
            {
                this.ErrorMsg = this.IQueryEmployeeInstance.Msg;
            }

            return tempEmployee;
        }

        #endregion

        #region 常数

        /// <summary>
        /// 根据常数类型取常数列表
        /// </summary>
        /// <param name="constType"></param>
        /// <returns></returns>
        public ArrayList QueryConstantList(string constType)
        {
            this.CreateIConstant();

            ArrayList alTemp = this.IConstantInstance.QueryConstantList(constType);

            if (alTemp == null)
            {
                this.ErrorMsg = this.IConstantInstance.Msg;
            }

            return alTemp;
        }

        /// <summary>
        /// 根据常数类型取常数列表
        /// </summary>
        /// <param name="constType"></param>
        /// <returns></returns>
        public ArrayList QueryConstantList(FS.HISFC.Models.Base.EnumConstant constType)
        {
            ArrayList alTemp = this.QueryConstantList(constType.ToString());

            if (alTemp == null)
            {
                this.ErrorMsg = this.IConstantInstance.Msg;
            }

            return alTemp;
        }

        /// <summary>
        /// 根据常数类型和ID取常数信息
        /// </summary>
        /// <param name="type">常数类型</param>
        /// <param name="ID">ID</param>
        /// <returns></returns>
        public FS.FrameWork.Models.NeuObject GetConstantByTypeAndID(string type, string ID)
        {
            this.CreateIConstant();

            FS.FrameWork.Models.NeuObject temp = this.IConstantInstance.GetConstantByTypeAndID(type, ID);

            if (temp == null)
            {
                this.ErrorMsg = this.IConstantInstance.Msg;
            }

            return temp;
        }

        #endregion

        #region IInterfaceContainer 成员

        public Type[] InterfaceTypes
        {
            get
            {
                Type[] t = new Type[]{     
                    typeof(FS.SOC.HISFC.BizProcess.DCPInterface.IDepartment),
                    typeof(FS.SOC.HISFC.BizProcess.DCPInterface.IEmployee),
                    typeof(FS.SOC.HISFC.BizProcess.DCPInterface.IConstant)           
                                                };

                return t;
            }
        }

        #endregion
    }
}
