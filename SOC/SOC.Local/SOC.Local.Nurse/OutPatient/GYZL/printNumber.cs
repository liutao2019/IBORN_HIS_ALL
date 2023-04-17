using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Collections;

namespace Neusoft.SOC.Local.Nurse.OutPatient.GYZL
{
    /// <summary>
    /// 调用List实现
    /// </summary>
    public class printNumber : Neusoft.HISFC.BizProcess.Interface.Nurse.IInjectNumberPrint
    {
        /// <summary>
        /// 实现接口
        /// </summary>
        /// <param name="al"></param>
        public void Init(ArrayList al)
        {
            try
            {
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        /// <summary>
        /// 动态加载DLL
        /// </summary>
        /// <param name="strDLLPath"></param>
        private void LoadDLL(string strDLLPath)
        {
            Assembly assembly = Assembly.LoadFrom(strDLLPath);
            Type type = assembly.GetType("");
            Object obj = System.Activator.CreateInstance(type);
            MethodInfo methodInfo = type.GetMethod("");
            //methodInfo.Invoke(obj, new Object[5]);
        }
    }
}
