using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace FS.SOC.Local.DrugStore.FOSI.Outpatient
{
    public class WorkAppriaseExternInterfaceImplement
    {
        // 初始化DLL，在程序初始化时调用
        [DllImport("ISatSys.dll", EntryPoint = "InitDll", CharSet = CharSet.Ansi,
           CallingConvention = CallingConvention.Cdecl)]
        public static extern int InitDll();
        // 退出DLL，在退出应用程序时必须调用，否则会产生通讯错误
        [DllImport("ISatSys.dll", EntryPoint = "ExitDll", CharSet = CharSet.Ansi,
           CallingConvention = CallingConvention.Cdecl)]
        public static extern int ExitDll();
        // 开始一个事务
        [DllImport("ISatSys.dll", EntryPoint = "BeginTransactionEx", CharSet = CharSet.Ansi,
        CallingConvention = CallingConvention.Cdecl)]
        // 传入参数：医生姓名、科室名称、社保卡号、患者姓名（医生姓名和科室名称不能为空）
        public static extern void BeginTransactionEx(string szDoctorName, string szOfisName, string szSickCard, string szSickName);

        /// <summary>
        /// 初始化完成
        /// </summary>
        public static bool Inited = false;

        /// <summary>
        /// 工作评价
        /// </summary>
        /// <param name="szDoctorName">医生姓名</param>
        /// <param name="szOfisName">科室名称</param>
        /// <param name="szSickCard">社保卡号(CardNO)</param>
        /// <param name="szSickName">患者姓名</param>
        /// <returns></returns>
        public static bool WorkAppriase(string szDoctorName, string szOfisName, string szSickCard, string szSickName)
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
            catch { }
            return true;
        }
    }
}
