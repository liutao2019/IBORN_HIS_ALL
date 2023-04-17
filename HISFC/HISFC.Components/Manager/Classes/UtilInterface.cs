using System;
using System.Collections.Generic;
using System.Text;

namespace UFC.Manager.Classes
{
    /// <summary>
    /// [��������: ��̬����ʵ�ֽӿڵ���]<br></br>
    /// [�� �� ��: ����ȫ]<br></br>
    /// [����ʱ��: 2006-11-29]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public class UtilInterface
    {
        private static ReportPrintManager rpm = new ReportPrintManager();


        /// <summary>
        /// �����ؼ�����
        /// </summary>
        /// <param name="containerType">�ӿ������ؼ�����</param>
        /// <param name="interfaceType">�ӿ�����</param>
        /// <param name="index">�ӿ�����</param>
        /// <returns></returns>
        public static object CreateObject(Type containerType,Type interfaceType, int index)
        {
            object ret = null;
            ReportPrint reportPrint = rpm.GetReportPrint(containerType.ToString(), interfaceType.ToString(), index);
            string dllName = reportPrint.ReportPrintControls[0].DllName;
            string controlName = reportPrint.ReportPrintControls[0].ControlName;
            try
            {
                ret = System.Reflection.Assembly.LoadFrom(dllName).CreateInstance(controlName);
            }catch
            {
                return null;
            }

            return ret;
        }

        /// <summary>
        /// �����ؼ�����
        /// </summary>
        /// <param name="containerType">�ӿ������ؼ�����</param>
        /// <param name="interfaceType">�ӿ�����</param>        
        /// <returns></returns>
        public static object CreateObject(Type containerType, Type interfaceType)
        {
            object ret = null;
            ReportPrint reportPrint = rpm.GetReportPrint(containerType.ToString(), interfaceType.ToString());
            string dllName = reportPrint.ReportPrintControls[0].DllName;
            string controlName = reportPrint.ReportPrintControls[0].ControlName;
            try
            {
                ret = System.Reflection.Assembly.LoadFrom(dllName).CreateInstance(controlName);
            }
            catch
            {
                return null;
            }

            return ret;
        }
    }
}
