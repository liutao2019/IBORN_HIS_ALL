using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Collections;

namespace FS.SOC.Local.Nurse.ZhuHai.ZDWY.IPrintBar
{
    public class PrintLisCode : FS.HISFC.BizProcess.Interface.Registration.IPrintBar
    {
        #region 业务层

        private FS.HISFC.BizLogic.Fee.Outpatient outpatientManager = new FS.HISFC.BizLogic.Fee.Outpatient();

        #endregion

        #region 方法

        /// <summary>
        /// 打印条码
        /// </summary>
        /// <param name="register"></param>
        private void printBarCode(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            FS.SOC.Local.Nurse.ZhuHai.ZDWY.Common com = new FS.SOC.Local.Nurse.ZhuHai.ZDWY.Common();
            ArrayList sampleList = com.GetULItemSampleCodeByClinicNO(patient.ID);
            bool isAllNotEmpty = true;//条码号是否都不为空
            if (sampleList == null || sampleList.Count == 0)
            {
                //无检验项目
                return;
            }
            else
            {

                foreach (string sampleCode in sampleList)
                {
                    if (sampleCode.Equals(" "))
                    {
                        isAllNotEmpty = false;
                        break;
                    }
                }
            }
            try
            {
                string strPath = FS.FrameWork.WinForms.Classes.Function.CurrentPath + "LisInterface\\EasiLab.Client.BarCode.dll";
                Assembly assembly = Assembly.LoadFrom(strPath);
                Type type = assembly.GetType("EasiLab.Client.BarCode.BarCodeHelper");
                if (isAllNotEmpty == false)
                {
                    //有检验项目,条码号为空,根据患者处方号打印条码
                    MethodInfo method = type.GetMethod("PrintBarcodeByHospNum", new Type[] { typeof(string), typeof(DateTime), typeof(DateTime) });
                    method.Invoke(null, new object[] { patient.PID.CardNO, DateTime.Parse(patient.ExtendFlag1), DateTime.Parse(patient.ExtendFlag2) });//需要与LIS讨论，传入参数
                }
                else
                {
                    //有检验项目,条码号不为空,根据条码号打印条码
                    MethodInfo method = type.GetMethod("PrintBarcodeByLabNum", BindingFlags.Static | BindingFlags.Public);
                    foreach (string sampleCode in sampleList)
                    {
                        if (string.IsNullOrEmpty(sampleCode.Trim()))
                        {
                            //可能部分条码号为空
                            continue;
                        }
                        method.Invoke(null, new object[] { sampleCode });
                    }
                }

            }
            catch (Exception e)
            {
                MessageBox.Show("LIS出错信息:\n" + e.GetBaseException().Message);
            }
        }

        #endregion

        #region IInjectNumberPrint 成员

        public int printBar(FS.HISFC.Models.RADT.Patient patient, ref string strErr)
        {
            printBarCode((FS.HISFC.Models.RADT.PatientInfo)patient);
            //EasiLab.Client.BarCode.BarCode.PrintBarcodeByHospNum("");
            //EasiLab.Client.BarCode.BarCode.PrintBarcodeByLabNum("");
            //string strPath = FS.FrameWork.WinForms.Classes.Function.CurrentPath + "EasiLab.Client.BarCode.dll";
            //Assembly assembly = Assembly.LoadFrom(strPath);
            //Type type = assembly.GetType("EasiLab.Client.BarCode.BarCode");
            //MethodInfo method = type.GetMethod("PrintBarcodeByHospNum", BindingFlags.Static | BindingFlags.Public);
            //method.Invoke(null, new object[] { patient.PID.CardNO });
            return 0;
        }

        #endregion
    }
}
