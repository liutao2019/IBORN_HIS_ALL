using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FS.SOC.HISFC.BizProcess.DCPInterface
{
    /// <summary>
    /// ȡ��Ա��Ϣ�ӿ�
    /// </summary>
    public interface IEmployee : IBase
    {
        /// <summary>
        /// ��ѯ����Ա��������Ч��Ч
        /// </summary>
        /// <returns></returns>
        ArrayList QueryEmployeeAllValidAndUnvalid();

        /// <summary>
        /// ��ѯ������ЧԱ��
        /// </summary>
        /// <returns></returns>
        ArrayList QueryEmployeeAllValid();

        /// <summary>
        /// ������Ա����ȡ������Ա
        /// </summary>
        /// <param name="emplType">��Ա����</param>
        /// <returns></returns>
        ArrayList QueryEmployeeByType(FS.HISFC.Models.Base.EnumEmployeeType emplType);

        /// <summary>
        /// ��ѯĳ������������Ա
        /// </summary>
        /// <param name="deptID">���ұ���</param>
        /// <returns></returns>
        ArrayList QueryEmployeeByDeptID(string deptID);

        /// <summary>
        /// ��ѯĳ������ĳ�����͵���Ա
        /// </summary>
        /// <param name="emplType">��Ա����</param>
        /// <param name="deptID">���ұ���</param>
        /// <returns></returns>
        ArrayList QueryEmployeeByTypeAndDeptID(FS.HISFC.Models.Base.EnumEmployeeType emplType, string deptID);

        /// <summary>
        /// ����Ա������ȡԱ��������Ϣ
        /// </summary>
        /// <param name="ID">��Ա����</param>
        /// <returns></returns>
        FS.HISFC.Models.Base.Employee GetEmployeeInfoByID(string ID);
    }
}
