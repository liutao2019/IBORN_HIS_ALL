using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace FS.SOC.Local.OutpatientFee.NanZhuang
{
    /// <summary>
    /// 佛山南庄医院患者评价系统接口实现--费用
    /// </summary>
    public class PatientEvaluation : FS.HISFC.BizProcess.Interface.Fee.IOutpatientEvaluation
    {
        /// <summary>
        /// 初始化DLL，在程序初始化时调用
        /// </summary>
        /// <returns></returns>
        [DllImport("ISatSys.dll", EntryPoint = "InitDll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int InitDll();

        /// <summary>
        /// 退出DLL，在退出应用程序时必须调用，否则会产生通讯错误
        /// </summary>
        /// <returns></returns>
        [DllImport("ISatSys.dll", EntryPoint = "ExitDll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int ExitDll();

        // 开始一个事务

        /// <summary>
        /// 传入参数：医生姓名、科室名称、社保卡号、患者姓名（医生姓名和科室名称不能为空）
        /// </summary>
        /// <param name="szDoctorName"></param>
        /// <param name="szOfisName"></param>
        /// <param name="szSickCard"></param>
        /// <param name="szSickName"></param>
        [DllImport("ISatSys.dll", EntryPoint = "BeginTransactionEx", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void BeginTransactionEx(string szDoctorName, string szOfisName, string szSickCard, string szSickName);

        /// <summary>
        /// 初始化完成
        /// </summary>
        public static bool Inited = false;

        /// <summary>
        /// 错误信息
        /// </summary>
        private static string errInfo = "";

        /// <summary>
        /// 工作评价
        /// </summary>
        /// <param name="szDoctorName">操作员名称</param>
        /// <param name="szOfisName">科室名称</param>
        /// <param name="szSickCard">社保卡号(CardNO)</param>
        /// <param name="szSickName">患者姓名</param>
        /// <returns></returns>
        public bool WorkAppriaseEvaluation(string szDoctorName, string szOfisName, string szSickCard, string szSickName)
        {
            try
            {
                if (!Inited)
                {
                    InitDll();
                    Inited = true;
                }

                BeginTransactionEx(szDoctorName, szOfisName, szSickCard, szSickName);
            }
            catch (Exception ex)
            {
                errInfo = ex.Message;
            }
            return true;
        }
        /// <summary>
        /// 工作评价
        /// </summary>
        /// <param name="szSickCard">社保卡号(CardNO)</param>
        /// <param name="szSickName">患者姓名</param>
        /// <returns></returns>
        public bool WorkAppriaseEvaluation(string szSickCard, string szSickName)
        {
            FS.HISFC.Models.Base.Employee empl = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
            return WorkAppriaseEvaluation(empl.Name, empl.Dept.Name, szSickCard, szSickName);
        }

        public string ErrInfo
        {
            get { return errInfo; }
            set { errInfo = value; }
        }
    }
}
