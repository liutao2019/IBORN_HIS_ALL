using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.Local.Registration.ZhuHai.Zdwy.IRegPrint;
using FS.SOC.HISFC.BizProcess.CommonInterface;

namespace FS.SOC.Local.Registration.ZhuHai.Zdwy.IPrintBar
{
    public class RegPrintBarCode : FS.HISFC.BizProcess.Interface.Registration.IPrintBar
    {
        #region IPrintBar 成员

        public int printBar(FS.HISFC.Models.RADT.Patient p, ref string errText)
        {
            if (p is FS.HISFC.Models.Registration.Register)
            {
                FS.HISFC.Models.Registration.Register register=p as FS.HISFC.Models.Registration.Register;
                //判断登陆人员的科室
                FS.HISFC.Models.Base.Employee e = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator);
                if (Function.IsContainYKDept(e.Dept.ID))//宜康病区，打印六张条码
                {
                    //如果宜康病区没有使用卡号，则找到宜康病区的卡号
                    int count = 6;
                    if (CommonController.Instance.MessageBox( "确认打印6张条码？", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        count = 6;
                    }
                    else
                    {
                        ucInputTimes ucInputTimes = new ucInputTimes();
                        ucInputTimes.Times = count;
                        FS.FrameWork.WinForms.Classes.Function.ShowControl(ucInputTimes, System.Windows.Forms.FormBorderStyle.FixedToolWindow, System.Windows.Forms.FormWindowState.Normal);
                        count = ucInputTimes.Times;
                    }

                    if (count > 0)
                    {
                        ucYKBarCode ucMedicalRecord = new ucYKBarCode();
                        ucMedicalRecord.SetPrintValue(register, 1);
                        for (int i = 0; i < count; i++)
                        {
                            ucMedicalRecord.Print();
                        }
                    }
                }
                else
                {
                    //PrintInvoiceCnt=2为补打条码
                    //PrintInvoiceCnt=0为挂号时打印
                    //IsFirst为第一次就诊
                    if (register.PrintInvoiceCnt == 2 || (register.PrintInvoiceCnt == 0 && register.IsFirst))
                    {
                        ucBarCode  ucMedicalRecord = new  ucBarCode();
                        ucMedicalRecord.SetPrintValue(register, 1);
                        ucMedicalRecord.Print();
                    }
                }
            }
            return 1;
        }

        #endregion
    }
}
