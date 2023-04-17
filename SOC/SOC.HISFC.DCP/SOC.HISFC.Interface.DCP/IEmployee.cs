using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FS.SOC.HISFC.BizProcess.DCPInterface
{
    /// <summary>
    /// 取人员信息接口
    /// </summary>
    public interface IEmployee : IBase
    {
        /// <summary>
        /// 查询所有员工包括有效无效
        /// </summary>
        /// <returns></returns>
        ArrayList QueryEmployeeAllValidAndUnvalid();

        /// <summary>
        /// 查询所有有效员工
        /// </summary>
        /// <returns></returns>
        ArrayList QueryEmployeeAllValid();

        /// <summary>
        /// 根据人员类型取所有人员
        /// </summary>
        /// <param name="emplType">人员类型</param>
        /// <returns></returns>
        ArrayList QueryEmployeeByType(FS.HISFC.Models.Base.EnumEmployeeType emplType);

        /// <summary>
        /// 查询某个科室所有人员
        /// </summary>
        /// <param name="deptID">科室编码</param>
        /// <returns></returns>
        ArrayList QueryEmployeeByDeptID(string deptID);

        /// <summary>
        /// 查询某个科室某种类型的人员
        /// </summary>
        /// <param name="emplType">人员类型</param>
        /// <param name="deptID">科室编码</param>
        /// <returns></returns>
        ArrayList QueryEmployeeByTypeAndDeptID(FS.HISFC.Models.Base.EnumEmployeeType emplType, string deptID);

        /// <summary>
        /// 根据员工编码取员工基本信息
        /// </summary>
        /// <param name="ID">人员编码</param>
        /// <returns></returns>
        FS.HISFC.Models.Base.Employee GetEmployeeInfoByID(string ID);
    }
}
