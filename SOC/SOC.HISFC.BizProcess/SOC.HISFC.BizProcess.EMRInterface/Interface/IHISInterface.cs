using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.BizProcess.EMRInterface.Interface
{
    /// <summary>
    /// EMR用于HIS启动的接口
    /// </summary>
    public interface IHISInterface
    {
        /// <summary>
        /// 初始化MER
        /// </summary>
        /// <returns></returns>
        int EMRInit();

        /// <summary>
        /// 登陆MER
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="deptId"></param>
        /// <returns></returns>
        int EMRLogIn(string employeeId, string deptId);

        /// <summary>
        /// 注销登陆MER
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="deptId"></param>
        /// <returns></returns>
        int EMRLogOut(string employeeId, string deptId);

        /// <summary>
        /// 关闭MER
        /// </summary>
        /// <returns></returns>
        int EMRClose();
    }
}
