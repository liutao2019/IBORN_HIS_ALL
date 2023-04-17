using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FS.SOC.HISFC.BizProcess.DCPInterface
{
    /// <summary>
    /// 取科室信息接口
    /// </summary>
    public interface IDepartment : IBase
    {
        /// <summary>
        /// 查出所有科室,包括有效无效
        /// </summary>
        /// <returns></returns>
        ArrayList QueryDeptAllValidAndUnvalid();

        /// <summary>
        /// 查出所有有效科室
        /// </summary>
        /// <returns></returns>
        ArrayList QueryDeptAllValid();

        /// <summary>
        /// 根据科室类型查询科室列表
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        ArrayList QueryDeptByType(FS.HISFC.Models.Base.EnumDepartmentType type);

        /// <summary>
        /// 根据科室编号取科室信息
        /// </summary>
        /// <param name="deptID"></param>
        /// <returns></returns>
        FS.HISFC.Models.Base.Department GetDeptByID(string deptID);
    }
}
