using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Collections;

namespace SOC.Local.EvaluationSystemInterface
{
    /// <summary>
    /// 佛山南庄医院患者评价系统接口实现
    /// </summary>
    public class EvaluationSystemInterface:FS.HISFC.BizProcess.Interface.Order.ISaveOrder,FS.HISFC.BizProcess.Interface.RADT.IPatientOut
    { 
        /// <summary>
        /// 初始化DLL，在程序初始化时调用
        /// </summary>
        /// <returns></returns>
        [DllImport("ISatSys.dll", EntryPoint = "InitDll", CharSet = CharSet.Ansi,CallingConvention = CallingConvention.Cdecl)]
        public static extern int InitDll();

        /// <summary>
        /// 退出DLL，在退出应用程序时必须调用，否则会产生通讯错误
        /// </summary>
        /// <returns></returns>
        [DllImport("ISatSys.dll", EntryPoint = "ExitDll", CharSet = CharSet.Ansi,CallingConvention = CallingConvention.Cdecl)]
        public static extern int ExitDll();

        // 开始一个事务

        /// <summary>
        /// 传入参数：医生姓名、科室名称、社保卡号、患者姓名（医生姓名和科室名称不能为空）
        /// </summary>
        /// <param name="szDoctorName"></param>
        /// <param name="szOfisName"></param>
        /// <param name="szSickCard"></param>
        /// <param name="szSickName"></param>
        [DllImport("ISatSys.dll", EntryPoint = "BeginTransactionEx", CharSet = CharSet.Ansi,CallingConvention = CallingConvention.Cdecl)]
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
            catch (Exception ex)
            {
                errInfo = ex.Message;
            }
            return true;
        }


        #region ISaveOrder 成员

        public int OnSaveOrderForInPatient(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.FrameWork.Models.NeuObject reciptDept, FS.FrameWork.Models.NeuObject reciptDoct, ArrayList alOrder)
        {
            return 1;
        }

        /// <summary>
        /// 门诊保存处方后调用
        /// </summary>
        /// <param name="regObj"></param>
        /// <param name="reciptDept"></param>
        /// <param name="reciptDoct"></param>
        /// <param name="alOrder"></param>
        /// <returns></returns>
        public int OnSaveOrderForOutPatient(FS.HISFC.Models.Registration.Register regObj, FS.FrameWork.Models.NeuObject reciptDept, FS.FrameWork.Models.NeuObject reciptDoct, ArrayList alOrder)
        {
            return FS.FrameWork.Function.NConvert.ToInt32(WorkAppriase(reciptDoct.Name, reciptDept.Name, regObj.PID.CardNO, regObj.Name));
        }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrInfo
        {
            get
            {
                return errInfo;
            }
            set
            {
                errInfo = value;
            }
        }

        #endregion

        #region IPatientOut 成员

        /// <summary>
        /// 出院登记调用
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="oper"></param>
        /// <returns></returns>
        public int OnPatientOut(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.FrameWork.Models.NeuObject oper)
        {
            return FS.FrameWork.Function.NConvert.ToInt32(WorkAppriase("出院登记", ((FS.HISFC.Models.Base.Employee)oper).Dept.Name, patientInfo.PID.PatientNO, patientInfo.Name));
        }

        #endregion

        #region IPatientOut 成员

        public int AfterPatientOut(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.FrameWork.Models.NeuObject oper)
        {
            return 1;
        }

        public int BeforePatientOut(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.FrameWork.Models.NeuObject oper)
        {
            return 1;
        }

        #endregion
    }
}
