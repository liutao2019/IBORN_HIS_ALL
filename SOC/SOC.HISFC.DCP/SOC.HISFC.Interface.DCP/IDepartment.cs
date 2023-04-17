using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FS.SOC.HISFC.BizProcess.DCPInterface
{
    /// <summary>
    /// ȡ������Ϣ�ӿ�
    /// </summary>
    public interface IDepartment : IBase
    {
        /// <summary>
        /// ������п���,������Ч��Ч
        /// </summary>
        /// <returns></returns>
        ArrayList QueryDeptAllValidAndUnvalid();

        /// <summary>
        /// ���������Ч����
        /// </summary>
        /// <returns></returns>
        ArrayList QueryDeptAllValid();

        /// <summary>
        /// ���ݿ������Ͳ�ѯ�����б�
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        ArrayList QueryDeptByType(FS.HISFC.Models.Base.EnumDepartmentType type);

        /// <summary>
        /// ���ݿ��ұ��ȡ������Ϣ
        /// </summary>
        /// <param name="deptID"></param>
        /// <returns></returns>
        FS.HISFC.Models.Base.Department GetDeptByID(string deptID);
    }
}
